using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PagoFraccionadoViewForm : PagoFraccionadoForm
    {
        #region Attributes & Properties

        public new const string ID = "PagoFraccionadoViewForm";
        public new static Type Type { get { return typeof(PagoFraccionadoViewForm); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private PaymentInfo _entity;

        public override PaymentInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public PagoFraccionadoViewForm()
            : this(-1, ETipoPago.Todos, null) { }

        public PagoFraccionadoViewForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[1] { tipo }, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            ETipoPago tipo = (ETipoPago)parameters[0];

            _entity = PaymentInfo.Get(oid, tipo, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        protected override void SetGridColors(Control control)
        {
            if (control.Name == Payments_DGW.Name)
            {
                PaymentInfo item;

                foreach (DataGridViewRow row in Payments_DGW.Rows)
                {
                    item = row.DataBoundItem as PaymentInfo;

                    if (item == null) continue;

                    if (item.EEstado != moleQule.Base.EEstado.Abierto)
                        Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
                    else
                        Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstadoPago);

                    if (item.EEstado == moleQule.Base.EEstado.Anulado) return;

                    if (item.Pendiente != 0)
                        row.Cells[PendienteAsignacion.Name].Style = Common.ControlTools.Instance.CobradoStyle;
                    else
                        row.Cells[PendienteAsignacion.Name].Style = row.Cells[Importe.Index].Style;
                }
            }
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Expenses_BS.DataSource = ExpenseList.GetByPagoList(_entity, false);
            PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;
            Vencimiento_DTP.Value = _entity.Vencimiento;

            base.RefreshMainData();
        }

        #endregion

        #region Print

        #endregion

        #region Business Methods

        protected override void UpdateAllocated()
        {
            decimal _asignado = 0;

            ExpenseList gastos = Expenses_BS.DataSource as ExpenseList;

            foreach (ExpenseInfo item in gastos)
                _asignado += item.Asignado;

            if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
            {
                _no_asignado = _entity.Importe - _asignado;

                if (_entity.Importe >= 0) _no_asignado = (_no_asignado < 0) ? 0 : _no_asignado;
                else _no_asignado = (_no_asignado > 0) ? 0 : _no_asignado;
            }
            else
            {
                _no_asignado = -_asignado;
                _entity.Importe = _asignado;
            }

            NoAsignado_TB.Text = _no_asignado.ToString("N2");
            MarkControl(NoAsignado_TB);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override void EditPaymentAction()
        {
            ViewPaymentAction();
        }

        protected override void ViewPaymentAction()
        {
            Payment currentPago = Payments_BS.Current as Payment;

            if (currentPago != null)
            {
                ExpensePaymentViewForm form = new ExpensePaymentViewForm(_entity, currentPago.GetInfo(true), this);
                form.ShowDialog();
            }
        }

        #endregion

        #region Events

        private void PagoFraccionadoViewForm_Shown(object sender, EventArgs e)
        {
            UpdateAllocated();
        }

        #endregion
    }
}
