using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public partial class PagoFraccionadoUIForm : PagoFraccionadoForm
    {
        #region Attributes & Properties

        public new const string ID = "PagoFraccionadoUIForm";
        public new static Type Type { get { return typeof(PagoFraccionadoUIForm); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Payment _entity;
        protected ExpenseList _gastos;
        protected CreditCardInfo _tarjeta = null;

        public override Payment Entity { get { return _entity; } set { _entity = value; } }
        public override PaymentInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected PagoFraccionadoUIForm()
            : this(-1, null, true, null) { }

        public PagoFraccionadoUIForm(long oid, object[] parameters, bool isModal, Form parent)
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
            else if (control.Name == Payments_DGW.Name)
            {
                Payment item;

                foreach (DataGridViewRow row in Payments_DGW.Rows)
                {
                    item = row.DataBoundItem as Payment;

                    if (item == null) continue;

                    if (item.EEstado != moleQule.Base.EEstado.Abierto)
                        Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
                    else
                        Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstadoPago);

                    if (item.EEstado == moleQule.Base.EEstado.Anulado) continue;

                    if (item.Pendiente != 0)
                        row.Cells[PendienteAsignacion.Name].Style = Common.ControlTools.Instance.CobradoStyle;
                    else
                        row.Cells[PendienteAsignacion.Name].Style = row.Cells[Importe.Index].Style;
                }
            }
        }

        protected override void MarkControl(Control ctl)
        {
            if (ctl.Name == NoAsignado_TB.Name)
            {
                if (_entity.Importe > 0)
                    NoAsignado_TB.BackColor = (_no_asignado == 0) ? Color.LightGray : (_no_asignado > 0) ? Color.LightGreen : Color.Red;
                else
                    NoAsignado_TB.BackColor = (_no_asignado == 0) ? Color.LightGray : (_no_asignado < 0) ? Color.LightGreen : Color.Red;
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
            Payments_BS.DataSource = _entity.Pagos;
            PgMng.Grow();

            Expenses_BS.DataSource = ExpenseList.GetSortedList(_gastos, "PrevisionPago", ListSortDirection.Ascending);
            PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;
            Vencimiento_DTP.Value = _entity.Vencimiento;

            base.RefreshMainData();
        }

        protected override void SetUnlinkedGridValues(Control control)
        {
            if (_gastos != null) _gastos.UpdatePagoValues(_entity);
            Expenses_BS.DataSource = ExpenseList.GetSortedList(_gastos, "PrevisionPago", ListSortDirection.Ascending);
            UpdateAllocated();
            RefreshAction();
        }

        #endregion

        #region Business Methods

        protected bool ValidateAllocation()
        {
            if (_entity.EMedioPago == EMedioPago.CompensacionFactura)
            {
                decimal importe = 0;

                Expenses_BS.MoveFirst();
                foreach (DataGridViewRow row in Lineas_DGW.Rows)
                {
                    InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

                    if (item.Vinculado == Library.Store.Resources.Labels.RESET_PAGO)
                        importe += item.Asignado;

                    Expenses_BS.MoveNext();
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
                    if (_no_asignado < _entity.Pendiente)
                    {
                        PgMng.ShowInfoException(string.Format("La asignación {0:C2} es inferior a la cantidad pendiente en el cobro {1:C2}.", _no_asignado, _entity.Pendiente));

                        _action_result = DialogResult.Ignore;
                        return false;
                    }
                }
                else
                {
                    if (_no_asignado > _entity.Pendiente)
                    {
                        PgMng.ShowInfoException(string.Format("La asignación {0:C2} es superior a la cantidad pendiente en el cobro {1:C2}.", _no_asignado, _entity.Pendiente));

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

            SortedBindingList<ExpenseInfo> expenses = Expenses_BS.DataSource as SortedBindingList<ExpenseInfo>;

            foreach (ExpenseInfo item in expenses)
                _allocated += item.Asignado;

            if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
            {
                _no_asignado = _entity.Importe - _allocated;

                if (_entity.Importe >= 0) _no_asignado = (_no_asignado < 0) ? 0 : _no_asignado;
                else _no_asignado = (_no_asignado > 0) ? 0 : _no_asignado;
            }
            else
            {
                _no_asignado = -_allocated;
                _entity.Importe = _allocated;
            }

            NoAsignado_TB.Text = _no_asignado.ToString("N2");
            MarkControl(NoAsignado_TB);
        }

        protected void UpdateAmount()
        {
            decimal _allocated = 0;

            ExpenseList expenses = Expenses_BS.DataSource as ExpenseList;

            foreach (ExpenseInfo item in expenses)
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

            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                LinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
        }

        protected override void CreatePaymentsAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                MessageBox.Show(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!ValidateAllocation()) return;

            int n_pagos = NPagos_NTB.Text != string.Empty ? Convert.ToInt32(NPagos_NTB.Text) : 1;
            int periodicidad = Periodicidad_NTB.Text != string.Empty ? Convert.ToInt32(Periodicidad_NTB.Text) : 1;

            _entity.GeneraPagosFraccionados(n_pagos, periodicidad, ETipoPago.Gasto);

            Payments_BS.ResetBindings(true);
            RefreshAction();
        }

        protected override void EditLineAllocationAction(DataGridViewRow row)
        {
            InputDecimalForm form = new InputDecimalForm();
            form.Message = Resources.Labels.IMPORTE_PAGO_GASTO;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExpenseInfo item = row.DataBoundItem as ExpenseInfo;

                _no_asignado += item.Asignado;

                _entity.EditTransactionPayment(item, form.Value);

                LinkLineAction(row);
                SetUnlinkedGridValues(Lineas_DGW.Name);
                Expenses_BS.ResetBindings(false);
                SetGridColors(Lineas_DGW);
            }
        }

        protected override void EditPaymentAction()
        {
            Payment currentPago = Payments_BS.Current as Payment;

            if (currentPago != null)
            {
                ExpensePaymentEditForm form = new ExpensePaymentEditForm(_entity.GetInfo(true), currentPago, this);
                form.ShowDialog(this);
            }

            RefreshAction();
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
                if (_no_asignado == 0)
                {
                    UnlinkLineAction(row);
                    return;
                }
            }

            ExpenseInfo item = row.DataBoundItem as ExpenseInfo;

            if (item == null) return;

            _entity.InsertNewTransactionPayment(item, _no_asignado);

            UpdateAllocated();

            MarkAsNoActiva(row);
        }

        protected override void RefreshAction()
        {
            SetGridColors(Payments_DGW);
        }

        protected void ReleaseAction()
        {
            foreach (DataGridViewRow row in Lineas_DGW.Rows)
                UnlinkLineAction(row);

            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
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

                    _tarjeta = (_entity.EMedioPago != EMedioPago.Tarjeta) ? null : _tarjeta;
                    _entity.OidTarjetaCredito = 0;
                    _entity.TarjetaCredito = string.Empty;

                    switch (_entity.EMedioPago)
                    {
                        case EMedioPago.CompensacionFactura:
                            {
                                Importe_NTB.Text = _entity.Importe.ToString("N2");
                                ReleaseAction();
                                Importe_NTB.Enabled = false;
                            }
                            break;

                        case EMedioPago.Tarjeta:
                            {
                                Tarjeta_BT.Enabled = true;
                                Cuenta_BT.Enabled = false;
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
                }
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;
            }
        }

        protected virtual void SetCreditCardAction()
        {
            CreditCardSelectForm form = new CreditCardSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _tarjeta = form.Selected as CreditCardInfo;

                _entity.OidTarjetaCredito = _tarjeta.Oid;
                _entity.TarjetaCredito = _tarjeta.Nombre;
                _entity.OidCuentaBancaria = _tarjeta.OidCuentaBancaria;
                _entity.CuentaBancaria = _tarjeta.CuentaBancaria;
                Cuenta_TB.Text = _entity.CuentaBancaria;
                Tarjeta_TB.Text = _entity.TarjetaCredito;

                _entity.SetFechas(Fecha_DTP.Value, _tarjeta);
                Vencimiento_DTP.Value = _entity.Vencimiento;
            }
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

        protected override void ViewPaymentAction()
        {
            Payment currentPago = Payments_BS.Current as Payment;

            if (currentPago != null)
            {
                ExpensePaymentViewForm form = new ExpensePaymentViewForm(_entity.GetInfo(true), currentPago.GetInfo(true), this);
                form.ShowDialog();
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

        private void PagoFraccionadoUIForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Lineas_DGW);
            SetGridColors(Lineas_DGW);
            UpdateAllocated();
        }

        private void MedioPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMedioPago();
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.SetFechas(Fecha_DTP.Value, _tarjeta);
            Vencimiento_DTP.Value = _entity.Vencimiento;
        }

        private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.SetVencimiento(Vencimiento_DTP.Value);
        }

        #endregion
    }
}