using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Payment;

namespace moleQule.Face.Store
{
    public partial class EmployeePaymentUIForm : EmployeePaymentForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        #endregion

        #region Factory Methods

        public EmployeePaymentUIForm()
            : this(null, -1, null) { }

        public EmployeePaymentUIForm(Form parent, long oidAgent, PaymentSummary summary)
			: base(parent, oidAgent, summary)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                IAcreedor temp = _entity.IClone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity.ApplyEdit();
                    _entity.ISave(Pago);
                    _entity.BeginEdit();

                    return true;
                }
                catch (Exception ex)
                {
                    PgMng.ShowInfoException(iQExceptionHandler.GetAllMessages(ex));
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }

        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
			_payrolls = PayrollList.GetListByEmpleado(_entity.Oid, false);
            PgMng.Grow();

            Datos.DataSource = _entity;
            PgMng.Grow();

            _entity.LoadChilds(typeof(Payment), true);
            Payments_BS.DataSource = _entity.Pagos.GetSortedList("Fecha", ListSortDirection.Descending);
            PgMng.Grow();

            Summary_BS.DataSource = _summary;

            base.RefreshMainData();
        }

        public void Select(Payment pago)
        {
            if (pago == null) return;
            int foundIndex = Payments_BS.IndexOf(pago);
            Payments_BS.Position = foundIndex;
        }
        public void Select(PaymentInfo pago)
        {
            Payment item = _entity.Pagos.GetItem(pago.Oid);

            if (pago == null) return;
            int foundIndex = Payments_BS.IndexOf(item);
            Payments_BS.Position = foundIndex;
        }

        protected override void RefreshAction()
        {
            Payment current = Pago;

            Payments_BS.DataSource = _entity.Pagos.GetSortedList("Fecha", ListSortDirection.Descending);
            Pagos_DGW.Refresh();
            Payments_BS.ResetBindings(false);

			_payrolls = PayrollList.GetListByEmpleado(_entity.Oid, false);
            UpdatePendientes();

            _summary.Refresh(_entity);
            Summary_BS.ResetBindings(false);

            SetGridColors(Pagos_DGW.Name);

            Select(current);

        }

        #endregion

        #region Business Methods

        protected void ChangeState(moleQule.Base.EEstado estado)
        {
            if (Pago == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
                return;
            }

            if (Pago.EEstado == moleQule.Base.EEstado.Anulado)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
                return;
            }

            switch (estado)
            {
                case moleQule.Base.EEstado.Anulado:
                    {
                        if (Pago.EEstado == moleQule.Base.EEstado.Contabilizado)
                        {
                            PgMng.ShowInfoException(moleQule.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
                            return;
                        }

                        if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    break;
            }

            try
            {
                _entity.Pagos.ChangeState(estado, Pago, _entity);
                _entity.ApplyEdit();
                _entity.ISave(Pago);
                _entity.BeginEdit();

                Payment current = Pago;
                Payments_BS.ResetBindings(false);
                _summary.Refresh(_entity);
                Summary_BS.ResetBindings(false);
                SetGridColors(Pagos_DGW.Name);
				_payrolls = PayrollList.GetListByEmpleado(_entity.Oid, false);
                Select(current);
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
            }
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            if (!_entity.CloseSessions)
                _entity.CloseSession();
            _action_result = DialogResult.OK;
        }

        protected override void PrintAction() { PrintObject(); }

        public override void PrintObject()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.BUILDING_REPORT, this);

            PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, Resources.Labels.PAGOS, "Empleado = " + Entity.Nombre);
            PgMng.Grow();

            PagoAcreedorDetailRpt report = reportMng.GetPagoAcreedorDetailReport(Summary, _entity.Pagos);

            PgMng.FillUp();

            ShowReport(report);
        }

        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Payment), Pago.GetInfo() as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Payment), Pago.GetInfo() as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

        protected override void AddPagoAction()
        {
            PayrollPaymentAddForm form = new PayrollPaymentAddForm(this, _entity);
            form.ShowDialog(this);

            RefreshAction();
            Payments_BS.MoveFirst();
        }

        protected override void ViewPagoAction()
        {
            if (Pago == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
                return;
            }

            PayrollPaymentEditForm form = new PayrollPaymentEditForm(this, _entity, Pago.Oid, false);
            form.SetReadOnly();
            form.ShowDialog(this);
        }

        protected override void EditPagoAction()
        {
            bool locked = false;

            if (Pago == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
                return;
            }

            if (Pago.EEstado == moleQule.Base.EEstado.Anulado)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
                return;
            }

            PayrollPaymentEditForm form = new PayrollPaymentEditForm(this, _entity, Pago.Oid, locked);
            form.MedioPago_BT.Enabled = false;
            form.ShowDialog(this);

            Payment current = Pago;
            Payments_BS.ResetBindings(false);
            _summary.Refresh(_entity);
            Summary_BS.ResetBindings(false);
            SetGridColors(Pagos_DGW.Name);
			_payrolls = PayrollList.GetListByEmpleado(_entity.Oid, false);
            Select(current);
        }

        protected override void DeletePagoAction()
        {
            ChangeState(moleQule.Base.EEstado.Anulado);
        }

        protected override void UnlockPagoAction()
        {
            ChangeState(moleQule.Base.EEstado.Abierto);
        }

        protected override void LockPagoAction()
        {
            ChangeState(moleQule.Base.EEstado.Contabilizado);
        }

        protected override void PrintPagoAction()
        {
            if (Pago == null) return;

            PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema);

            PagoNominaDetailRpt report = reportMng.GetNominaDetailReport(Pago, _entity, _payrolls);

            ShowReport(report);
        }

        #endregion

        #region Events

        void Datos_Pago_CurrentChanged(object sender, EventArgs e)
        {
            List<NominaInfo> lista = new List<NominaInfo>();

            if (Payments_BS.Current == null)
            {
                Payrolls_BS.DataSource = lista;
                return;
            }

            foreach (TransactionPayment item in Pago.Operations)
            {
                NominaInfo exp = _payrolls.GetItem(item.OidOperation);
                if (exp != null) lista.Add(exp);
            }

            Payrolls_BS.DataSource = lista;
        }

        private void Datos_Factura_DataSourceChanged(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Nominas_DGW.Name);
        }

        #endregion
    }
}