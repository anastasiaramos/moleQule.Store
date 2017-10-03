using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule;
using moleQule.Cash;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Invoice;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class CreditCardPaymentUIForm : CreditCardPaymentForm
    {
        #region Attributes & Properties

        public new const string ID = "CreditCardPaymentUIForm";
        public new static Type Type { get { return typeof(CreditCardPaymentUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Payment _entity;
		protected CreditCardStatementList _statements;
		protected CreditCardInfo _credit_card = null;

        public override Payment Entity { get { return _entity; } set { _entity = value; } }
        public override PaymentInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected CreditCardPaymentUIForm() 
			: this(-1, null, true, null) { }

        public CreditCardPaymentUIForm(long oid, object[] parameters, bool isModal, Form parent)
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

		protected override void MarkAsUnlinked(DataGridViewRow row)
		{
            CreditCardStatementInfo item = row.DataBoundItem as CreditCardStatementInfo;
			item.Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
			base.MarkAsUnlinked(row);
		}

		protected override void MarkAsLinked(DataGridViewRow row)
		{
            CreditCardStatementInfo item = row.DataBoundItem as CreditCardStatementInfo;
			item.Vinculado = Library.Store.Resources.Labels.SET_PAGO;
			base.MarkAsLinked(row);
		}

        protected override void SetGridColors(Control control)
        {
            if (control.Name == Lines_DGW.Name)
            {
                CreditCardStatementInfo item;

                foreach (DataGridViewRow row in Lines_DGW.Rows)
                {
                    item = row.DataBoundItem as CreditCardStatementInfo;
                    if (item == null) continue;

                    // Si ya estaba asignado entonces lo marcamos como asignado
                    if (_entity.Operations.GetItemByFactura(item.Oid) == null)
                        MarkAsLinked(row);
                    else
                        MarkAsUnlinked(row);
                }
            }
        }

		#endregion

		#region Source

        protected override void LoadCreditCardStatements()
        {
            Lines_BS.DataSource = (_statements != null) ? CreditCardStatementList.GetSortedList(_statements, "DueDate", ListSortDirection.Ascending) : null;           
        }

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            LoadCreditCardStatements();
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			Vencimiento_DTP.Value = _entity.Vencimiento;
        }

        protected override void SetUnlinkedGridValues(string gridName)
		{
            SortedBindingList<CreditCardStatementInfo> statements = Lines_BS.DataSource as Csla.SortedBindingList<CreditCardStatementInfo>;
            CreditCardStatementList list = CreditCardStatementList.GetList(statements);
            if (statements != null) list.UpdatePaymentValues(_entity.Oid);
            Lines_BS.DataSource = list.GetSortedList();
			UpdateAllocated();
		}		
		        
        #endregion

		#region Business Methods

		protected bool ValidateAllocation()
		{
			if (_entity.EMedioPago == EMedioPago.CompensacionFactura)
			{
				decimal importe = 0;

				Lines_BS.MoveFirst();
				foreach (DataGridViewRow row in Lines_DGW.Rows)
				{
					InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

					if (item.Vinculado == Library.Store.Resources.Labels.RESET_PAGO)
						importe += item.Asignado;

					Lines_BS.MoveNext();
				}

				if (importe != 0)
				{
					PgMng.ShowInfoException(Resources.Messages.IMPORTE_PAGO_COMPENSACION);

					_action_result = DialogResult.Ignore;
					return false;
				}
			}
			else
			{
				if (_entity.Pendiente == 0) return true;

				if (_entity.Importe < 0)
				{
					if (_deallocated < _entity.Pendiente)
					{
						PgMng.ShowInfoException(string.Format("La asignación {0:C2} es inferior a la cantidad pendiente en el cobro {1:C2}.", _deallocated, _entity.Pendiente));

						_action_result = DialogResult.Ignore;
						return false;
					}
				}
				else
				{
					if (_deallocated > _entity.Pendiente)
					{
						PgMng.ShowInfoException(string.Format("La asignación {0:C2} es superior a la cantidad pendiente en el cobro {1:C2}.", _deallocated, _entity.Pendiente));

						_action_result = DialogResult.Ignore;
						return false;
					}
				}
			}

			return true;
		}

		protected override void UpdateAllocated()
		{
			decimal _allocated = 0;

            SortedBindingList<CreditCardStatementInfo> statements = Lines_BS.DataSource as SortedBindingList<CreditCardStatementInfo>;

            foreach (CreditCardStatementInfo item in statements)
				_allocated += item.Asignado;

			if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
			{
				_deallocated = _entity.Importe - _allocated;

				if (_entity.Importe >= 0) _deallocated = (_deallocated < 0) ? 0 : _deallocated;
				else _deallocated = (_deallocated > 0) ? 0 : _deallocated;
			}
			else
			{
				_deallocated = -_allocated;
				_entity.Importe = _allocated;
			}

			NoAsignado_TB.Text = _deallocated.ToString("N2");
			MarkControl(NoAsignado_TB);
		}

		protected void UpdateAmount()
		{
			decimal _allocated = 0;

            CreditCardStatementList statements = Lines_BS.DataSource as CreditCardStatementList;

            foreach (CreditCardStatementInfo item in statements)
				_allocated += item.Asignado;

			if (_entity.Importe >= 0)
				_entity.Importe = (_entity.Importe) > _allocated ? _entity.Importe : _allocated;
			else
				_entity.Importe = (_entity.Importe) < _allocated ? _entity.Importe : _allocated;
		}

		#endregion

        #region Actions

        protected void AllocateAction()
        {
            ReleaseAction();

            foreach (DataGridViewRow row in Lines_DGW.Rows)
                LinkLineAction(row);

            SetUnlinkedGridValues(Lines_DGW);
            SetGridColors(Lines_DGW);
        }

        protected override void EditLineAllocationAction(DataGridViewRow row)
        {
            InputDecimalForm form = new InputDecimalForm();
            form.Message = Resources.Labels.IMPORTE_PAGO_GASTO;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                CreditCardStatementInfo item = row.DataBoundItem as CreditCardStatementInfo;

                _deallocated += item.Asignado;

                _entity.EditTransactionPayment(item, form.Value);

                LinkLineAction(row);
                SetUnlinkedGridValues(Lines_DGW.Name);
                Lines_BS.ResetBindings(false);
                SetGridColors(Lines_DGW);
            }
        }
        
        protected override void LinkAction()
        {
            DataGridViewRow row = Lines_DGW.CurrentRow;

            UpdateAllocated();

            if (VinculadoBTValue == Library.Invoice.Resources.Labels.SET_COBRO)
                LinkLineAction(row);
            else
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lines_DGW);
            SetGridColors(Lines_DGW);
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

            CreditCardStatementInfo item = row.DataBoundItem as CreditCardStatementInfo;

            if (item == null) return;

            _entity.InsertNewTransactionPayment(item, _deallocated);

            UpdateAllocated();

            MarkAsUnlinked(row);
        }

        protected void ReleaseAction()
        {
            foreach (DataGridViewRow row in Lines_DGW.Rows)
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lines_DGW);
            SetGridColors(Lines_DGW);
        }

        protected virtual void SetBankAccountAction()
        {
            //BankAccountSelectForm form;

            //if (_entity.EMedioPago == EMedioPago.ComercioExterior)
            //{
            //    form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.ComercioExterior, moleQule.Base.EEstado.Active, false));
            //}
            //else
            //    form = new BankAccountSelectForm(this, BankAccountList.GetNoAsociadasList(moleQule.Base.EEstado.Active, false));

            //if (form.ShowDialog(this) == DialogResult.OK)
            //{
            //    BankAccountInfo cuenta = form.Selected as BankAccountInfo;

            //    _entity.OidCuentaBancaria = cuenta.Oid;
            //    _entity.CuentaBancaria = cuenta.Valor;
            //    Cuenta_TB.Text = _entity.CuentaBancaria;
            //}
        }

        protected override void SetPaymentStatusAction()
        {
            moleQule.Base.EEstado[] estados = new moleQule.Base.EEstado[] { moleQule.Base.EEstado.Pendiente, moleQule.Base.EEstado.Pagado };

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

		protected virtual void SetPaymentMethodAction()
		{
			try
			{
				Datos.RaiseListChangedEvents = false;

				_entity.SetMedioPago((long)EMedioPago.Giro);
				MedioPago_TB.Text = _entity.MedioPagoLabel;

				Importe_NTB.Enabled = true;
                EstadoPago_BT.Enabled = true;

				_credit_card = null;
				_entity.OidTarjetaCredito = 0;
				_entity.TarjetaCredito = string.Empty;

                EstadoPago_TB.Text = _entity.EstadoPagoLabel;
			}
			finally
			{
				Datos.RaiseListChangedEvents = true;
			}
		}

		protected override void SetCreditCardAction()
		{
			CreditCardSelectForm form = new CreditCardSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_credit_card = form.Selected as CreditCardInfo;

                if (_credit_card.ETipoTarjeta != ETipoTarjeta.Credito)
                {
                    PgMng.ShowInfoException("Solo son válidas las tarjetas de crédito");
                    return;
                }

				_entity.OidTarjetaCredito = _credit_card.Oid;
				_entity.TarjetaCredito = _credit_card.Nombre;
				_entity.OidCuentaBancaria = _credit_card.OidCuentaBancaria;
				_entity.CuentaBancaria = _credit_card.CuentaBancaria;
                _entity.Agente = _credit_card.Nombre;
				Cuenta_TB.Text = _entity.CuentaBancaria;
				Tarjeta_TB.Text = _entity.TarjetaCredito;

				_entity.SetFechas(Fecha_DTP.Value, _credit_card);
				Vencimiento_DTP.Value = _entity.Vencimiento;

                EstadoPago_TB.Text = _entity.EstadoPagoLabel;

                LoadCreditCardStatements();
			}
		}
        
		protected void UnlinkLineAction(DataGridViewRow row)
		{
			if (row == null) return;

            CreditCardStatementInfo item = row.DataBoundItem as CreditCardStatementInfo;

            _entity.DeleteTransactionPayment(item);

			UpdateAllocated();

			MarkAsLinked(row);
		}

        protected override void ViewStatementAction() 
        {
            if (!ControlsMng.IsCurrentItemValid(Lines_DGW)) return;

            CreditCardStatementInfo item = ControlsMng.GetCurrentItem(Lines_DGW) as CreditCardStatementInfo;

            PaymentList list = PaymentList.GetByCreditCardStatement(item.Oid, false);
			PaymentMngForm form = new PaymentMngForm(true, _parent, ETipoPago.Todos, list);
            form.ViewMode = molView.Enbebbed;
            form.Text = String.Format("Extracto de tarjeta {0}: {1} - {2})", _entity.TarjetaCredito, item.From.ToShortDateString(), item.Till.ToShortDateString());
            form.Width = form.Width / 5 * 4;
            form.Height = form.Height / 5 * 4;
            form.StartPosition = FormStartPosition.CenterScreen;

			FormMngBase.Instance.ShowFormulario(form, this);
                
            form.FitColumns();
        }

        protected override void ViewCashLinesAction()
        {
            if (!ControlsMng.IsCurrentItemValid(Lines_DGW)) return;

            CreditCardStatementInfo item = ControlsMng.GetCurrentItem(Lines_DGW) as CreditCardStatementInfo;

            CashLineList list = CashLineList.GetByCreditCardStatement(item.Oid, false);
            CashLineMngForm form = new CashLineMngForm(true, _parent, list, 1);
            form.ViewMode = molView.Enbebbed;
            form.Text = String.Format("Disposiciones de efectivo de tarjeta {0}: {1} - {2})", _entity.TarjetaCredito, item.From.ToShortDateString(), item.Till.ToShortDateString());
            form.Width = form.Width / 5 * 4;
            form.Height = form.Height / 5 * 4;
            form.StartPosition = FormStartPosition.CenterScreen;

            FormMngBase.Instance.ShowFormulario(form, this);
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

        private void CreditCardPaymentUIForm_Shown(object sender, EventArgs e)
		{
			SetUnlinkedGridValues(Lines_DGW.Name);
			SetGridColors(Lines_DGW);
			UpdateAllocated();
		}

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

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.SetFechas(Fecha_DTP.Value, _credit_card);
			Vencimiento_DTP.Value = _entity.Vencimiento;
		}

        private void Importe_NTB_Validated(object sender, EventArgs e)
        {
            UpdateAllocated();
        }

        private void MedioPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPaymentMethod();
        }

		private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.SetVencimiento(Vencimiento_DTP.Value);
		}

        #endregion
    }
}