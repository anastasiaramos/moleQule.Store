using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using Csla;
using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InvoicePaymentUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        protected Payment _entity;
        protected IAcreedor _holder;
        protected List<TransactionPayment> _payments = new List<TransactionPayment>();
		protected decimal _deallocated = 0;
		protected CreditCardInfo _credit_card = null;

        public Payment Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        protected bool _updating = false;
        protected bool _locked = false;

		protected string VinculadoBTValue { get { return Lineas_DGW.CurrentRow.Cells[Vinculado.Index].Value.ToString(); } }

        protected InputInvoiceInfo FacturaActual { get { return (Lineas_DGW.CurrentRow != null) ? Lineas_DGW.CurrentRow.DataBoundItem as InputInvoiceInfo : null; } }

        #endregion

        #region Factory Methods

        public InvoicePaymentUIForm() 
            : this(null, null)  {}

        public InvoicePaymentUIForm(Form parent, IAcreedor provider)
            : base(true, parent)
        {
            InitializeComponent();
            _holder = provider;
        }

        protected bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
				_holder.ApplyEdit();
                _holder.ISave(_entity);
                _holder.BeginEdit();

                return true;
            }
			catch (Exception ex)
			{
				PgMng.ShowErrorException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			MaximizeForm(new Size(this.Width, 0));

			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            Tarjeta_BT.Enabled = _entity.EMedioPago == EMedioPago.Tarjeta;
            Cuenta_BT.Enabled = _entity.EMedioPago != EMedioPago.Tarjeta && _entity.EMedioPago != EMedioPago.Efectivo;
		}

		protected virtual void SetGridColors(Control control)
		{
			if (control.Name == Lineas_DGW.Name)
			{
				InputInvoiceInfo item;

				foreach (DataGridViewRow row in Lineas_DGW.Rows)
				{
					item = row.DataBoundItem as InputInvoiceInfo;
					if (item == null) continue;

					// Si ya estaba asignado entonces lo marcamos como asignado
                    if (_entity.Operations.GetItemByFactura(item.Oid) == null) 
						MarkAsActiva(row);
					else 
						MarkAsNoActiva(row);
				}
			}
		}

		public void SetReadOnly()
		{
			Source_GB.Enabled = false;
            Datos_Lineas.DataSource = InputInvoiceList.GetListByPago(_entity.Oid, true).GetSortedList();
		}

		protected void MarkAsNoActiva(DataGridViewRow row)
		{
			InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;
			item.Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
			row.Cells[Asignacion.Index].Style.BackColor = Color.LightGreen;
		}

		protected void MarkAsActiva(DataGridViewRow row)
		{
			InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;
			item.Vinculado = Library.Store.Resources.Labels.SET_PAGO;
			row.Cells[Asignacion.Index].Style.BackColor = row.Cells[Pendiente.Index].Style.BackColor;
		}

		private void MarkControl(Control ctl)
		{
			if (ctl.Name == NoAsignado_TB.Name)
			{
				if (_entity.Importe > 0)
					NoAsignado_TB.BackColor = (_deallocated == 0) ? Color.LightGray : (_deallocated > 0) ? Color.LightGreen : Color.Red;
				else
					NoAsignado_TB.BackColor = (_deallocated == 0) ? Color.LightGray : (_deallocated < 0) ? Color.LightGreen : Color.Red;
			}
		}

		#endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			Vencimiento_DTP.Value = _entity.Vencimiento;
        }

		public override void RefreshSecondaryData()
		{
			base.RefreshSecondaryData();

			if (_entity.OidTarjetaCredito > 0) SetCreditCard(CreditCardInfo.Get(_entity.OidTarjetaCredito, false));
			PgMng.Grow();
		}

        protected virtual ComboBoxList<EMedioPago> GetPaymentMethods()
        {
            return moleQule.Common.Structs.EnumText<EMedioPago>.GetList(false);
        }
		
        protected virtual void SetUnlinkedGridValues(Control control)
		{
            SortedBindingList<InputInvoiceInfo> invoices = Datos_Lineas.DataSource as SortedBindingList<InputInvoiceInfo>;
            InputInvoiceList list = InputInvoiceList.GetList(invoices);
			if (invoices != null) list.UpdatePagoValues(_entity);
            Datos_Lineas.DataSource = list.GetSortedList();
			UpdateAllocated();
		}

        #endregion

        #region Business Methods
	
        protected void SetCreditCard(CreditCardInfo source)
		{
			if (source == null) return;

			_credit_card = source;

			_entity.OidTarjetaCredito = _credit_card.Oid;
			_entity.TarjetaCredito = _credit_card.Nombre;
			_entity.OidCuentaBancaria = _credit_card.OidCuentaBancaria;
			_entity.CuentaBancaria = _credit_card.CuentaBancaria;
			Cuenta_TB.Text = _entity.CuentaBancaria;
			Tarjeta_TB.Text = _entity.TarjetaCredito;
		}

        protected bool ValidateAllocation()
        {
            SortedBindingList<InputInvoiceInfo> invoices = Datos_Lineas.DataSource as SortedBindingList<InputInvoiceInfo>;
            IEnumerable<ITransactionPayment> lines = invoices.Cast<ITransactionPayment>();

            _action_result = PaymentCommon.ValidateAllocation(_entity, _deallocated, lines);
            return _action_result == System.Windows.Forms.DialogResult.OK;
        }
        protected bool ValidateDueDate()
        {
            _action_result = PaymentCommon.ValidateDueDate(_entity, _credit_card, Vencimiento_DTP.Value);
            return _action_result == System.Windows.Forms.DialogResult.OK;
        }

        //protected bool ValidateAllocation()
        //{            
        //    if (_entity.EMedioPago == EMedioPago.CompensacionFactura)
        //    {
        //        decimal importe = 0;

        //        Datos_Lineas.MoveFirst();
        //        foreach (DataGridViewRow row in Lineas_DGW.Rows)
        //        {
        //            InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

        //            if (item.Vinculado == Library.Store.Resources.Labels.RESET_PAGO)
        //                importe += item.Asignado;

        //            Datos_Lineas.MoveNext();
        //        }

        //        if (importe != 0)
        //        {
        //            PgMng.ShowInfoException(Resources.Messages.IMPORTE_PAGO_COMPENSACION);

        //            _action_result = DialogResult.Ignore;
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        if (_entity.Pendiente == 0) return true;

        //        if (_entity.Importe < 0)
        //        {
        //            if (_unallocated < _entity.Pendiente)
        //            {
        //                PgMng.ShowInfoException(string.Format(Resources.Messages.PAYMENT_LESS_ALLOCATION, _unallocated, _entity.Pendiente));

        //                _action_result = DialogResult.Ignore;
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            if (_unallocated > _entity.Pendiente)
        //            {
        //                PgMng.ShowInfoException(string.Format(Resources.Messages.PAYMENT_BIGGER_ALLOCATION, _unallocated, _entity.Pendiente));

        //                _action_result = DialogResult.Ignore;
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

		protected void UpdateAllocated()
		{
			decimal _asignado = 0;

            if (Datos_Lineas.DataSource == null) return;

			SortedBindingList<InputInvoiceInfo> facturas = Datos_Lineas.DataSource as SortedBindingList<InputInvoiceInfo>;

			foreach (InputInvoiceInfo item in facturas)
				_asignado += item.Asignado;

			if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
			{
				_deallocated = Entity.Importe - _asignado;

				if (_entity.Importe >= 0) _deallocated = (_deallocated < 0) ? 0 : _deallocated;
				else _deallocated = (_deallocated > 0) ? 0 : _deallocated;
			}
			else
			{
				_deallocated = -_asignado;
				_entity.Importe = _asignado;
			}

			NoAsignado_TB.Text = _deallocated.ToString("N2");
			MarkControl(NoAsignado_TB);
		}

		protected void UpdateAmount()
		{
			decimal _asignado = 0;

			InputInvoiceList facturas = Datos_Lineas.DataSource as InputInvoiceList;

			foreach (InputInvoiceInfo item in facturas)
				_asignado += item.Asignado;

			if (_entity.Importe >= 0)
				_entity.Importe = (_entity.Importe) > _asignado ? _entity.Importe : _asignado;
			else
				_entity.Importe = (_entity.Importe) < _asignado ? _entity.Importe : _asignado;
		}

		#endregion

        #region Actions

        protected void AllocateAction()
        {
            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                LinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
        }

        protected virtual void EditAmountAction(DataGridViewRow row)
        {
            InputDecimalForm form = new InputDecimalForm();
            form.Message = Resources.Labels.IMPORTE_PAGO_FACTURA;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

                _deallocated += item.Asignado;

                _entity.EditTransactionPayment(item, form.Value);

                LinkLineAction(row);
                SetUnlinkedGridValues(Lineas_DGW);
                Datos_Lineas.ResetBindings(false);
                SetGridColors(Lineas_DGW);
            }
        }

        protected virtual void LinkAction()
        {
            DataGridViewRow row = Lineas_DGW.CurrentRow;

            UpdateAllocated();

            if (VinculadoBTValue == Library.Invoice.Resources.Labels.SET_COBRO)
                LinkLineAction(row);
            else
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
        }

        protected virtual void LinkLineAction(DataGridViewRow row)
        {
            if (row == null) return;

            if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
            {
                if (_deallocated == 0)
                {
                    UnlinkLineAction(row);
                    return;
                }
            }

            InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

            if (item == null) return;

            _entity.InsertNewTransactionPayment(item, _deallocated);

            UpdateAllocated();

            MarkAsNoActiva(row);
        }

        protected virtual void SetBankAccountAction()
        {
            BankAccountSelectForm form;

            if (_entity.EMedioPago == EMedioPago.ComercioExterior)
            {
                form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.ComercioExterior, moleQule.Base.EEstado.Active, false));
            }
            else
                form = new BankAccountSelectForm(this, BankAccountList.GetNoAsociadasList(moleQule.Base.EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                _entity.OidCuentaBancaria = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
                Cuenta_TB.Text = _entity.CuentaBancaria;
            }
        }

        protected virtual void SetCreditCardAction()
        {
            CreditCardSelectForm form = new CreditCardSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                SetCreditCard(form.Selected as CreditCardInfo);

                EstadoPago_BT.Enabled = (_credit_card.ETipoTarjeta == ETipoTarjeta.Debito);
                if (_credit_card.ETipoTarjeta == ETipoTarjeta.Credito) _entity.EEstadoPago = moleQule.Base.EEstado.Pendiente;

                _entity.SetFechas(Fecha_DTP.Value, _credit_card);
                Vencimiento_DTP.Value = _entity.Vencimiento;

                PaymentStatus_TB.Text = _entity.EstadoPagoLabel;
            }
        }

        protected virtual void SetExpenses()
        {
            if (_entity.EMedioPago != EMedioPago.ComercioExterior) return;

            BankAccountInfo cuenta = BankAccountInfo.Get(_entity.OidCuentaBancaria, false);
            if (cuenta.PagoGastosInicio)
            {
                int dias_pago = (_entity.Vencimiento - _entity.Fecha).Days;

                _entity.GastosBancarios = _entity.Importe * cuenta.TipoInteres * dias_pago / 36000;
            }
        }

		protected virtual void SetPaymentMethodAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(GetPaymentMethods());

			try
			{
				Datos.RaiseListChangedEvents = false;

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					ComboBoxSource item = form.Selected as ComboBoxSource;

					_entity.SetMedioPago(item.Oid);
					MedioPago_TB.Text = _entity.MedioPagoLabel;

					Importe_NTB.Enabled = true;
					Tarjeta_BT.Enabled = false;
					Cuenta_BT.Enabled = true;
                    PaymentStatus_TB.Text = _entity.EstadoPagoLabel;

					_credit_card = (_entity.EMedioPago != EMedioPago.Tarjeta) ? null : _credit_card;
					_entity.OidTarjetaCredito = 0;
					_entity.TarjetaCredito = string.Empty;

					switch (_entity.EMedioPago)
					{
						case EMedioPago.CompensacionFactura:
							{
								Importe_NTB.Text = _entity.Importe.ToString("N2");
								ReleaseAction();
                                Importe_NTB.Enabled = false;
                                PaymentStatus_TB.Text = _entity.EstadoPagoLabel;
							}
							break;

						case EMedioPago.Tarjeta:
							{
								Tarjeta_BT.Enabled = true;
                                Cuenta_BT.Enabled = false;
                                PaymentStatus_TB.Text = _entity.EstadoPagoLabel;
							}
                            break;
                        case EMedioPago.ComercioExterior:
                            {
                                SetBankAccountAction();
                                SetExpenses();
                            }
                            break;
					}

					if (moleQule.Common.EnumFunctions.NeedsCuentaBancaria(_entity.EMedioPago))
					{
						if ((_entity.EMedioPago == EMedioPago.Tarjeta) && (_entity.OidTarjetaCredito == 0))
							SetCreditCardAction();
						else if (_entity.OidCuentaBancaria == 0)
							SetBankAccountAction();
					}
					else
						Cuenta_TB.Text = string.Empty;

                    PaymentStatus_TB.Text = _entity.EstadoPagoLabel;
				}
			}
			finally
			{
				Datos.RaiseListChangedEvents = true;
			}
		}

        protected virtual void SetPaymentStatusAction()
        {
            moleQule.Base.EEstado[] estados = new moleQule.Base.EEstado[3] { moleQule.Base.EEstado.Pendiente, moleQule.Base.EEstado.Pagado, moleQule.Base.EEstado.Devuelto };

            SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(estados));

            try
            {
                Datos.RaiseListChangedEvents = false;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ComboBoxSource item = form.Selected as ComboBoxSource;

                    _entity.EstadoPago = item.Oid;
                    PaymentStatus_TB.Text = _entity.EstadoPagoLabel;
                }
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;
            }
        }

        protected void ReleaseAction()
        {
            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
        }

        protected virtual void UnlinkLineAction(DataGridViewRow row)
		{
			if (row == null) return;

			InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

            _entity.DeleteTransactionPayment(item);

			UpdateAllocated();

			MarkAsActiva(row);
		}

        #endregion

        #region Buttons

		private void Repartir_BT_Click(object sender, EventArgs e)
		{
			AllocateAction();
		}

		private void Liberar_BT_Click(object sender, EventArgs e)
		{
			ReleaseAction();
		}

		private void MedioPago_BT_Click(object sender, EventArgs e)
		{
			SetPaymentMethodAction();
		}

        private void SetCuenta_BT_Click(object sender, EventArgs e)
        {
			SetBankAccountAction();
        }

        private void Tarjeta_BT_Click(object sender, EventArgs e)
        {
			SetCreditCardAction();
        }

		private void EditFactura_TI_Click(object sender, EventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			EditAmountAction(Lineas_DGW.CurrentRow);
		}

		private void ViewFactura_TI_Click(object sender, EventArgs e)
		{
			if (FacturaActual == null) return;

			InputInvoiceViewForm form = new InputInvoiceViewForm(FacturaActual.Oid, FacturaActual.ETipoAcreedor, this);
			form.ShowDialog(this);
		}

        private void EstadoPago_BT_Click(object sender, EventArgs e)
        {
            SetPaymentStatusAction();
        }

        #endregion

        #region Events

        protected override void EnableEvents(bool enable)
        {
            if (enable)
            {
                this.Fecha_DTP.ValueChanged += new System.EventHandler(this.Fecha_DTP_ValueChanged);
                this.Vencimiento_DTP.ValueChanged += new System.EventHandler(this.Vencimiento_DTP_ValueChanged);
            }
            else
            {
                this.Fecha_DTP.ValueChanged -= new System.EventHandler(this.Fecha_DTP_ValueChanged);
                this.Vencimiento_DTP.ValueChanged -= new System.EventHandler(this.Vencimiento_DTP_ValueChanged);
            }
        }

        private void Importe_NTB_Validated(object sender, EventArgs e)
        {
            UpdateAllocated();
            SetExpenses();
        }

		private void PagoFUIForm_Shown(object sender, EventArgs e)
		{
			SetUnlinkedGridValues(Lineas_DGW);
			SetGridColors(Lineas_DGW);
		}

		private void Lineas_DGW_DoubleClick(object sender, EventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			EditAmountAction(Lineas_DGW.CurrentRow);
		}

		private void Lineas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Lineas_DGW.Columns[e.ColumnIndex].Name == Vinculado.Name)
			{
				LinkAction();
			}
		}

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			if ((_entity.EMedioPago == EMedioPago.Tarjeta) && (_credit_card == null))
				SetCreditCard(CreditCardInfo.Get(_entity.OidTarjetaCredito, false));

			_entity.SetFechas(Fecha_DTP.Value, _credit_card);
			Vencimiento_DTP.Value = _entity.Vencimiento;
		}

		private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
		{
            _entity.SetVencimiento(Vencimiento_DTP.Value);
            
            if (_entity.EMedioPago == EMedioPago.ComercioExterior)
                SetExpenses();
		}

        #endregion  
    }
}