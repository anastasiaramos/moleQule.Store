using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Nomina;
using moleQule.Hipatia;

namespace moleQule.Face.Store
{
    public partial class EmployeePaymentForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "EmployeePaymentForm";
        public static Type Type { get { return typeof(EmployeePaymentForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public IAcreedor Entity { get { return _entity; } set { _entity = value; } }
        public virtual PaymentSummary Summary { get { return _summary; } set { _summary = value; } }
        protected Payment Pago { get { return Payments_BS.Current as Payment; } }
        protected NominaInfo Payroll { get { return Payrolls_BS.Current as NominaInfo; } }

        protected IAcreedor _entity;
        protected PaymentSummary _summary;
		protected PayrollList _payrolls;

        #endregion

        #region Factory Methods

        public EmployeePaymentForm() 
            : this(null, -1, null) {}

        public EmployeePaymentForm(Form parent, long oidAgente, PaymentSummary summary)
			: this(oidAgente, summary, true, parent) { }

		public EmployeePaymentForm(long oidAgente, PaymentSummary summary, bool isModal, Form parent)
			: base(oidAgente, new object[1] { summary }, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

		#region Authorization

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() 
		{
			UnlockItem_TMI.Visible = AutorizationRulesControler.CanEditObject(moleQule.Resources.SecureItems.ESTADO);
			UnlockItem_TMI.Visible = AutorizationRulesControler.CanEditObject(moleQule.Resources.SecureItems.ESTADO);

			LockItem_TMI.Visible = AutorizationRulesControler.CanEditObject(moleQule.Resources.SecureItems.ESTADO);
			LockItem_TMI.Visible = AutorizationRulesControler.CanEditObject(moleQule.Resources.SecureItems.ESTADO);
		}

		#endregion

        #region Business Methods

		/// <summary>
		/// Selecciona un elemento de la tabla
		/// </summary>
		/// <param name="oid">Identificador del elemento</param>
		public new void Select()
		{
			if (Pago == null) return;
			int foundIndex = Payments_BS.IndexOf(Entity.Pagos.GetItem(Pago.Oid));
			Payments_BS.Position = foundIndex;
		}

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			cols.Clear();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Pagos_DGW, cols);
		}

        public override void FormatControls()
        {
			if (Pagos_DGW == null) return;

			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1350) ? 1350 : Screen.PrimaryScreen.WorkingArea.Width;

			MaximizeForm(new Size(maxWidth, 0));
			Content_SC.SplitterDistance = maxWidth;
			Pendientes_SC.SplitterDistance = maxWidth;

            base.FormatControls();

			HideAction(molAction.ShowDocuments);
			
            Pagos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Nominas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Pendientes_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		protected virtual void SetGridColors(string grid_name)
		{
			if (grid_name == Pagos_DGW.Name)
			{
                Payment item;

				foreach (DataGridViewRow row in Pagos_DGW.Rows)
				{
					if (row.IsNewRow) return;

                    item = row.DataBoundItem as Payment;
					if (item == null) continue;

					Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);

					row.Cells[PendienteAsignacion.Index].Style.BackColor = (item.Pendiente > 0) ? Color.LightGreen : row.Cells[Codigo.Index].Style.BackColor;
				}
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            UpdatePendientes();
            PgMng.Grow(string.Empty, "Nóminas Pendientes");
        }

        protected override void SetUnlinkedGridValues(string grid_name)
        {
            if (grid_name == Nominas_DGW.Name)
            {
                Payment Pago = (Payment)Payments_BS.Current;
                NominaInfo item = null;

                foreach (DataGridViewRow row in Nominas_DGW.Rows)
                {
                    if (row.IsNewRow) return;

                    item = row.DataBoundItem as NominaInfo;
                    if (item == null) continue;

                    row.Cells[FacturaAsignado.Index].Value = Pago.Operations.GetItemByFactura(item.Oid).Cantidad;
                    row.Cells[FacturaAnteriores.Index].Value = item.TotalPagado - Pago.Operations.GetItemByFactura(item.Oid).Cantidad;
                }
            }
        }

        protected void UpdatePendientes()
        {
			Unpaids_BS.DataSource = PayrollList.GetPendientesList(Entity.Oid, false);            
        }

        #endregion

        #region Validation & Format

        #endregion

        #region Actions

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
		protected virtual void AddPagoAction() { }
		protected virtual void ViewPagoAction() { }
		protected virtual void EditPagoAction() { }
		protected virtual void ViewCobroAction() { }
		protected virtual void EditCobroAction() { }
		protected virtual void DeletePagoAction() { }
		protected virtual void LockPagoAction() { }
		protected virtual void UnlockPagoAction() { }
		protected virtual void PrintPagoAction() { }

		protected virtual void PagosDefaultAction() { EditPagoAction(); }
        
		protected virtual void EditPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			NominaInfo nomina = Pendientes_DGW.CurrentRow.DataBoundItem as NominaInfo;

			PayrollBatchEditForm form = new PayrollBatchEditForm(nomina.OidRemesa, nomina.Oid, this);
			form.ShowDialog(this);

			UpdatePendientes();
		}

		protected virtual void VerPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			NominaInfo nomina = Pendientes_DGW.CurrentRow.DataBoundItem as NominaInfo;

            PayrollBatchViewForm form = new PayrollBatchViewForm(nomina.OidRemesa, nomina.Oid, this);
			form.ShowDialog(this);
		}

        protected virtual void EditEmpleadoAction()
        {
            EmployeeEditForm form = new EmployeeEditForm(Entity, this);
            form.ShowDialog();

            _summary.Refresh(Entity);
            Summary_BS.DataSource = _summary;
            Summary_BS.ResetBindings(false);
        }

		protected virtual void PrintPendienteAction()
		{
			/*if (Pendientes_DGW.CurrentRow == null) return;

			InputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as InputInvoiceInfo;

			PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, string.Empty);

			InputInvoiceInfo item = InputInvoiceInfo.Get(factura.Oid, factura.ETipoAcreedor);
			PgMng.Grow();

			FacturaProveedorRpt report = reportMng.GetFacturaReport(item, conf);
			PgMng.FillUp();

			ShowReport(report);*/
		}

		protected virtual void PrintPendienteListAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			NominaInfo nomina = Pendientes_DGW.CurrentRow.DataBoundItem as NominaInfo;

			NominaReportMng reportMng = new NominaReportMng(AppContext.ActiveSchema, this.Text, "Nómina = " + nomina.Empleado);

			NominaListRpt report = reportMng.GetListReport(Unpaids_BS.DataSource as PayrollList);

			ShowReport(report);
		}

        #endregion

        #region Buttons

		private void AddPago_TI_Click(object sender, EventArgs e)
		{
			AddPagoAction();
		}

		private void EditPago_TI_Click(object sender, EventArgs e)
		{
			EditPagoAction();
		}

		private void ViewPago_TI_Click(object sender, EventArgs e)
		{
			ViewPagoAction();
		}

		private void UnlockItem_TMI_Click(object sender, EventArgs e)
		{
			UnlockPagoAction();
		}

		private void LockItem_TMI_Click(object sender, EventArgs e)
		{
			LockPagoAction();
		}

		private void NullItem_TMI_Click(object sender, EventArgs e)
		{
			DeletePagoAction();
		}

		private void PrintPago_TI_Click(object sender, EventArgs e)
		{
			PrintPagoAction();
		}

		private void EditPendiente_TI_Click(object sender, EventArgs e)
		{
			EditPendienteAction();
		}

		private void VerPendiente_TI_Click(object sender, EventArgs e)
		{
			VerPendienteAction();
		}

		private void PrintPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteAction();
		}

		private void PrintListPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteListAction();
		}

        #endregion

        #region Events

        private void PagoForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Nominas_DGW.Name);
			SetGridColors(Pagos_DGW.Name);
            Select();
        }

        private void Pagos_DGW_DoubleClick(object sender, EventArgs e) { PagosDefaultAction(); }

        private void Empleado_BT_Click(object sender, EventArgs e)
        {
            EditEmpleadoAction();
        }
        
        #endregion
    }
}