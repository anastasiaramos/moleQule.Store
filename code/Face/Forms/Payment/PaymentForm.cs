using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Invoice;

namespace moleQule.Face.Store
{
    public partial class PaymentForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public IAcreedor Entity { get { return _entity; } set { _entity = value; } }
        public virtual PaymentSummary Summary { get { return _summary; } set { _summary = value; } }

		public override Type EntityType { get { return typeof(Proveedor); } }

        protected Payment Pago { get { return Datos_Pago.Current != null ? Datos_Pago.Current as Payment : null; } }
        protected InputInvoiceInfo Factura { get { return Datos_Factura.Current as InputInvoiceInfo; } }

        protected IAcreedor _entity;
        protected PaymentSummary _summary;
        protected InputInvoiceList _invoices;

        #endregion

        #region Factory Methods

        public PaymentForm() 
            : this(null, -1, null) {}

        public PaymentForm(Form parent, long oidAgent, PaymentSummary summary)
			: this(oidAgent, summary, true, parent) {}

		public PaymentForm(long oid_agente, PaymentSummary summary, bool isModal, Form parent)
			: base(oid_agente, new object[1] { summary }, isModal, parent)
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
			int foundIndex = Datos_Pago.IndexOf(Entity.Pagos.GetItem(Pago.Oid));
			Datos_Pago.Position = foundIndex;
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
			//IDE COMPATIBILITY
			if (AppContext.User == null) return;

			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1350) ? 1350 : Screen.PrimaryScreen.WorkingArea.Width;

			MaximizeForm(new Size(maxWidth, 0));
			Content_SC.SplitterDistance = maxWidth;
			Pendientes_SC.SplitterDistance = maxWidth;

            base.FormatControls();

			HideAction(molAction.ShowDocuments);
			
            Pagos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Facturas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

					row.Cells[PendienteAsignacion.Index].Style.BackColor = (item.Pendiente > 0) ? Color.LightGreen : row.Cells[NPago.Index].Style.BackColor;
				}
			}
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.ShowDocuments);
					ShowAction(molAction.Print);
					ShowAction(molAction.Refresh);
					HideAction(molAction.Cancel);
					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            UpdatePendientes();
            PgMng.Grow(string.Empty, "Facturas Pendientes");
        }

        protected override void SetUnlinkedGridValues(string grid_name)
        {
            if (grid_name == Facturas_DGW.Name)
            {
                Payment Pago = (Payment)Datos_Pago.Current;
                InputInvoiceInfo item = null;

                foreach (DataGridViewRow row in Facturas_DGW.Rows)
                {
                    if (row.IsNewRow) return;

                    item = row.DataBoundItem as InputInvoiceInfo;
                    if (item == null) continue;

                    row.Cells[FacturaAsignado.Index].Value = Pago.Operations.GetItemByFactura(item.Oid).Cantidad;
                    row.Cells[FacturaAnteriores.Index].Value = item.Pagado - Pago.Operations.GetItemByFactura(item.Oid).Cantidad;
                }
            }
        }

        protected void UpdatePendientes()
        {
            Datos_Pendientes.DataSource = InputInvoiceList.GetPendientesList(Entity.OidAcreedor, Entity.ETipoAcreedor, false);            
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

        protected void EditProveedorAction()
        {
            switch (Entity.ETipoAcreedor)
            {
                case ETipoAcreedor.Acreedor:
                case ETipoAcreedor.Proveedor:
                    {
                        ProveedorEditForm form = new ProveedorEditForm(Entity, this);
                        form.ShowDialog();
                    }
                    break;

                case ETipoAcreedor.Despachante:
                    {
                        DespachanteEditForm form = new DespachanteEditForm(Entity, this);
                        form.ShowDialog();
                    }
                    break;

                case ETipoAcreedor.Naviera:
                    {
                        NavieraEditForm form = new NavieraEditForm(Entity, this);
                        form.ShowDialog();
                    }
                    break;

                case ETipoAcreedor.TransportistaDestino:
                case ETipoAcreedor.TransportistaOrigen:
                    {
                        TransporterEditForm form = new TransporterEditForm(Entity, this);
                        form.ShowDialog();
                    }
                    break;
            }

            _summary.Refresh(Entity);
            Datos_Resumen.DataSource = _summary;
            Datos_Resumen.ResetBindings(false);
        }

		protected void FacturasDefaultAction()
		{
			if (Factura == null) return;

			ContenedorViewForm form = new ContenedorViewForm(Factura.OidExpediente, this);
			form.Show();
		}

		protected virtual void EditPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			InputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as InputInvoiceInfo;

			InputInvoiceEditForm form = new InputInvoiceEditForm(factura.Oid, factura.ETipoAcreedor, this);
			form.ShowDialog(this);

			UpdatePendientes();
		}

		protected virtual void VerPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			InputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as InputInvoiceInfo;

			InputInvoiceViewForm form = new InputInvoiceViewForm(factura.Oid, factura.ETipoAcreedor, this);
			form.ShowDialog(this);
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

			InputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as InputInvoiceInfo;

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, "Acreedor = " + factura.Acreedor);

			InputInvoiceListRpt report = reportMng.GetListReport(Datos_Pendientes.DataSource as InputInvoiceList,
																		ProviderBaseList.GetList(false));

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
            SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Pagos_DGW.Name);
            Select();
        }

		private void Pagos_DGW_DoubleClick(object sender, EventArgs e) { PagosDefaultAction(); }

        private void Facturas_DGW_DoubleClick(object sender, EventArgs e) { FacturasDefaultAction(); }

        private void Proveedor_BT_Click(object sender, EventArgs e)
        {
            EditProveedorAction();
        }

        #endregion
    }
}