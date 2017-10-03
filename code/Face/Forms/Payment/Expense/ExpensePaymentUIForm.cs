using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Csla;
using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpensePaymentUIForm : ExpensePaymentForm
    {
        #region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Payment _entity;
		protected ExpenseList _expenses;
		protected CreditCardInfo _credit_card = null;

        public override Payment Entity { get { return _entity; } set { _entity = value; } }
        public override PaymentInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected ExpensePaymentUIForm() 
			: this(-1, null, true, null) { }
		
		public ExpensePaymentUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Payment temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();					
				
                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowWarningException(ex.Message);
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

            Tarjeta_BT.Enabled = _entity.EMedioPago == EMedioPago.Tarjeta;
            Cuenta_BT.Enabled = _entity.EMedioPago != EMedioPago.Tarjeta && _entity.EMedioPago != EMedioPago.Efectivo;
        }

		protected override void SetGridColors(Control control)
		{
			if (control.Name == Lineas_DGW.Name)
			{
				ExpenseInfo item;

				foreach (DataGridViewRow row in Lineas_DGW.Rows)
				{
					item = row.DataBoundItem as ExpenseInfo;
					if (item == null) continue;

					// Si ya estaba asignado entonces lo marcamos como asignado
                    if (_entity.Operations.GetItemByFactura(item.Oid) == null)
						MarkAsActiva(row);
					else
						MarkAsNoActiva(row);
				}
			}
		}

		protected override void MarkControl(Control ctl)
		{
			if (ctl.Name == NoAsignado_TB.Name)
			{
				if (_entity.Importe > 0)
					NoAsignado_TB.BackColor = (_deallocated == 0) ? Color.LightGray : (_deallocated > 0) ? Color.LightGreen : Color.Red;
				else
					NoAsignado_TB.BackColor = (_deallocated == 0) ? Color.LightGray : (_deallocated < 0) ? Color.LightGreen : Color.Red;
			}
		}

		protected override void MarkAsNoActiva(DataGridViewRow row)
		{
			ExpenseInfo item = row.DataBoundItem as ExpenseInfo;
			item.Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
			base.MarkAsNoActiva(row);
		}

		protected override void MarkAsActiva(DataGridViewRow row)
		{
			ExpenseInfo item = row.DataBoundItem as ExpenseInfo;
			item.Vinculado = Library.Store.Resources.Labels.SET_PAGO;
			base.MarkAsActiva(row);
		}
		
		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Datos_Lineas.DataSource = ExpenseList.GetSortedList(_expenses, "PrevisionPago", ListSortDirection.Ascending);
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			Vencimiento_DTP.Value = _entity.Vencimiento;

            base.RefreshMainData();
        }

        protected override void SetUnlinkedGridValues(string gridName)
		{
			SortedBindingList<ExpenseInfo> gastos = Datos_Lineas.DataSource as Csla.SortedBindingList<ExpenseInfo>;
            ExpenseList list = ExpenseList.GetList(gastos);
			if (gastos != null) list.UpdatePagoValues(_entity);
            Datos_Lineas.DataSource = list.GetSortedList();
			UpdateAllocated();
		}		
		
        #endregion

		#region Business Methods

        protected bool ValidateAllocation()
        {
            SortedBindingList<ExpenseInfo> invoices = Datos_Lineas.DataSource as SortedBindingList<ExpenseInfo>;
            IEnumerable<ITransactionPayment> lines = invoices.Cast<ITransactionPayment>();

            _action_result = PaymentCommon.ValidateAllocation(_entity, _deallocated, lines);
            return _action_result == System.Windows.Forms.DialogResult.OK;
        }
        protected bool ValidateDueDate()
        {
            _action_result = PaymentCommon.ValidateDueDate(_entity, _credit_card, Vencimiento_DTP.Value);
            return _action_result == System.Windows.Forms.DialogResult.OK;
        }

		protected override void UpdateAllocated()
		{
			SortedBindingList<ExpenseInfo> lines = Datos_Lineas.DataSource as SortedBindingList<ExpenseInfo>;

            decimal allocated = lines.Sum(item => item.Asignado);

			if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
			{
				_deallocated = _entity.Importe - allocated;

				if (_entity.Importe >= 0) _deallocated = (_deallocated < 0) ? 0 : _deallocated;
				else _deallocated = (_deallocated > 0) ? 0 : _deallocated;
			}
			else
			{
				_deallocated = -allocated;
				_entity.Importe = allocated;
			}

			NoAsignado_TB.Text = _deallocated.ToString("N2");
			MarkControl(NoAsignado_TB);
		}

		protected void UpdateAmount()
		{
			ExpenseList lines = Datos_Lineas.DataSource as ExpenseList;

            decimal asignado = lines.Sum(item => item.Asignado);

			if (_entity.Importe >= 0)
				_entity.Importe = (_entity.Importe) > asignado ? _entity.Importe : asignado;
			else
				_entity.Importe = (_entity.Importe) < asignado ? _entity.Importe : asignado;
		}

		#endregion

        #region Actions

        protected void AllocateAction()
        {
            ReleaseAction();

            Datos_Lineas.MoveFirst();

            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                LinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
        }

        protected override void EditLineAssignmentAction(DataGridViewRow row)
        {
            InputDecimalForm form = new InputDecimalForm();
            form.Message = Resources.Labels.IMPORTE_PAGO_GASTO;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExpenseInfo item = row.DataBoundItem as ExpenseInfo;

                _deallocated += item.Asignado;

                _entity.EditTransactionPayment(item, form.Value);

                LinkLineAction(row);
                SetUnlinkedGridValues(Lineas_DGW.Name);
                Datos_Lineas.ResetBindings(false);
                SetGridColors(Lineas_DGW);
            }
        }

		protected override void LinkAction()
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

		protected void LinkLineAction(DataGridViewRow row)
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

			ExpenseInfo item = row.DataBoundItem as ExpenseInfo;

			if (item == null) return;

            _entity.InsertNewTransactionPayment(item, _deallocated);

			UpdateAllocated();

			MarkAsNoActiva(row);
		}

        protected void ReleaseAction()
        {
            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
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
                _credit_card = form.Selected as CreditCardInfo;

                _entity.OidTarjetaCredito = _credit_card.Oid;
                _entity.TarjetaCredito = _credit_card.Nombre;
                _entity.OidCuentaBancaria = _credit_card.OidCuentaBancaria;
                _entity.CuentaBancaria = _credit_card.CuentaBancaria;
                Cuenta_TB.Text = _entity.CuentaBancaria;
                Tarjeta_TB.Text = _entity.TarjetaCredito;

                EstadoPago_BT.Enabled = (_credit_card.ETipoTarjeta == ETipoTarjeta.Debito);
                if (_credit_card.ETipoTarjeta == ETipoTarjeta.Credito) _entity.EEstadoPago = moleQule.Base.EEstado.Pendiente;

                _entity.SetFechas(Fecha_DTP.Value, _credit_card);
                Vencimiento_DTP.Value = _entity.Vencimiento;

                EstadoPago_TB.Text = _entity.EstadoPagoLabel;
            }
        }

        protected virtual void SetPaymentMethodAction()
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(moleQule.Common.Structs.EnumText<EMedioPago>.GetList(false));

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
                    EstadoPago_BT.Enabled = false;

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
                                EstadoPago_BT.Enabled = false;
                            }
                            break;

                        case EMedioPago.Tarjeta:
                            {
                                Tarjeta_BT.Enabled = true;
                                Cuenta_BT.Enabled = false;
                                EstadoPago_BT.Enabled = false;
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

                    EstadoPago_TB.Text = _entity.EstadoPagoLabel;
                }
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;
            }
        }

        protected override void SetPaymentStatusAction()
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
                    EstadoPago_TB.Text = _entity.EstadoPagoLabel;
                }
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;
            }
        }
        
		protected void UnlinkLineAction(DataGridViewRow row)
		{
			if (row == null) return;

			ExpenseInfo item = row.DataBoundItem as ExpenseInfo;

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

		private void CuentaAjena_BT_Click(object sender, EventArgs e)
		{
			SetBankAccountAction();
		}

		private void Tarjeta_BT_Click(object sender, EventArgs e)
		{
			SetCreditCardAction();
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
        }

		private void PagoGastoUIForm_Shown(object sender, EventArgs e)
		{
			SetUnlinkedGridValues(Lineas_DGW.Name);
			SetGridColors(Lineas_DGW);
			UpdateAllocated();
		}

		private void MedioPago_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetMedioPago();
		}

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.SetFechas(Fecha_DTP.Value, _credit_card);
			Vencimiento_DTP.Value = _entity.Vencimiento;
		}

		private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.SetVencimiento(Vencimiento_DTP.Value);
		}

        #endregion
    }
}