using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Sales;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class WorkForm : ExpedienteForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		protected WorkReportList _work_reports = null;
		protected WorkReportResourceList _staff_expenses = null;
		protected WorkReportResourceList _tools_expenses = null;
		protected WorkReportResourceList _deliveries_expenses = null;
        protected WorkReportResourceList _categories_expenses = null;
		protected WorkReportResourceList _categories_detail_expenses = null;

        #endregion

        #region Factory Methods

        public WorkForm() 
			: this(-1) {}

		public WorkForm(long oid) 
			: this(oid, true, null) { }

        public WorkForm(long oid, bool isModal, Form parent)
			: base(oid, moleQule.Store.Structs.ETipoExpediente.Work, isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			base.FitColumns();

			List<DataGridViewColumn> cols;
			CurrencyManager cm;

			cm = (CurrencyManager)BindingContext[WorkReport_DGW.DataSource];
			cm.SuspendBinding();

			cols = new List<DataGridViewColumn>();
			WRComments.Tag = 1;

			cols.Add(WRComments);

			ControlsMng.MaximizeColumns(WorkReport_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRStaff_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRStaffResource.Tag = 1;

			cols.Add(WRStaffResource);

			ControlsMng.MaximizeColumns(WRStaff_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRTools_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRToolsResource.Tag = 1;

			cols.Add(WRToolsResource);

			ControlsMng.MaximizeColumns(WRTools_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRDeliveries_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRDeliveriesComments.Tag = 1;

			cols.Add(WRDeliveriesComments);

			ControlsMng.MaximizeColumns(WRDeliveries_DGW, cols);

            cm = (CurrencyManager)BindingContext[WRCategories_DGW.DataSource];
            cm.SuspendBinding();

            cols.Clear();
            WRCategoriesCategory.Tag = 1;

            cols.Add(WRCategoriesCategory);

            ControlsMng.MaximizeColumns(WRCategories_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRCategoriesDetail_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRCategoriesDetailCategory.Tag = 1;

			cols.Add(WRCategoriesDetailCategory);

			ControlsMng.MaximizeColumns(WRCategoriesDetail_DGW, cols);
		}

		public override void FormatControls()
        {
            if (Stock_DGW == null) return;
			if (EntityInfoNoChilds == null) return;

			base.MaximizeForm(new Size(1200, 0));

			Pestanas_TC.TabPages.Remove(Productos_TP);
			Pestanas_TC.TabPages.Remove(Stock_TP);
			Pestanas_TC.TabPages.Remove(Costes_TP);

			base.FormatControls();

            WorkReport_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRCategories_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRCategoriesDetail_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRDeliveries_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRStaff_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRTools_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
       }

        protected override void SetTabsOrder()
        {
            Pestanas_TC.TabPages.Clear();

            Pestanas_TC.TabPages.Add(General_TP);
            Pestanas_TC.TabPages.Add(WorkReports_TP);
            Pestanas_TC.TabPages.Add(WRStaff_TP);
            Pestanas_TC.TabPages.Add(WRTools_TP);
            Pestanas_TC.TabPages.Add(WRDeliveries_TP);
            Pestanas_TC.TabPages.Add(WRCategories_TP);
            Pestanas_TC.TabPages.Add(Expenses_TP);
            Pestanas_TC.TabPages.Add(Incomes_TP);
            Pestanas_TC.TabPages.Add(Benefits_TP);
        }

		#endregion

		#region Source

		protected override void LoadData()
		{
            base.LoadData();

            EnableEvents(false);

            if (Pestanas_TC.SelectedTab == WRCategories_TP)
            {
                LoadCategoriesExpenses();
                LoadCategoriesDetailsExpenses();
            }
            else if (Pestanas_TC.SelectedTab == WRStaff_TP)
            {
                LoadStaffExpenses();
            }
            else if (Pestanas_TC.SelectedTab == WRDeliveries_TP)
            {
                LoadDeliveriesExpenses();
            }
            else if (Pestanas_TC.SelectedTab == WRTools_TP)
            {
                LoadToolsExpenses();
            }

            EnableEvents(true);
		}

		protected virtual void LoadCategoriesExpenses(bool reload = false)
		{
			if (_categories_expenses == null || reload)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
                    _categories_expenses = WorkReportResourceList.GetByCategoryList(0, EntityInfoNoChilds.Oid, true, false);
                    WRCategories_BS.DataSource = _categories_expenses;
                    PgMng.Grow(string.Empty);

					CalculateTotales();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}
		protected virtual void LoadCategoriesExpenses(long oidExpedient)
		{
			if (_categories_detail_expenses != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				/*_facturas_costes_list = InputInvoiceList.GetCostesByExpedienteList(oidExpediente, false);
				Datos_FCostes.DataSource = _facturas_costes_list;
				PgMng.Grow();

				PendienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalPendiente().ToString("N2");
				TotalFacturasCostes_NTB.Text = _facturas_costes_list.Total().ToString("N2");
				ExpedienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalExpediente().ToString("N2");
				ImpuestosFacturasCostes_NTB.Text = _facturas_costes_list.TotalImpuestos().ToString("N2");

				_conceptos_costes_list = InputInvoiceLineList.GetCostesByExpedienteList(oidExpediente, false, true);
				Datos_CCostes.DataSource = _conceptos_costes_list;
				TotalConceptosCostes_NTB.Text = _conceptos_costes_list.Total().ToString("N2");
				ImpuestosConceptosCostes_NTB.Text = _conceptos_costes_list.TotalImpuestos().ToString("N2");

				Costes_NTB.Text = TotalConceptosCostes_NTB.DecimalValue.ToString("C2");

				CalculateBeneficios();*/

				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}
        protected virtual void LoadCategoriesDetailsExpenses()
        {
            if (WRCategories_DGW.CurrentRow == null)
            {
                _categories_detail_expenses = null;
            }
            else
            {
                PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
                try
                {
                    WorkReportResourceInfo category = WRCategories_DGW.CurrentRow.DataBoundItem as WorkReportResourceInfo;

                    _categories_detail_expenses = WorkReportResourceList.GetByCategoryAndResourceList(category.OidCategory, EntityInfoNoChilds.Oid, true, false);
                    WRCategoriesDetail_BS.DataSource = _categories_detail_expenses;
                    PgMng.Grow(string.Empty);
                }
                finally
                {
                    _categories_detail_expenses = null;

                    PgMng.FillUp();
                }
            }
        }

		protected virtual void LoadDeliveriesExpenses() 
		{
			if (_deliveries_expenses == null)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					_deliveries_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.OutputDelivery, false, false);
					WRDeliveries_BS.DataSource = _deliveries_expenses;
					PgMng.Grow(string.Empty);

					CalculateTotales();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}
		protected virtual void LoadDeliveriesExpenses(long oidExpedient)
		{
			if (_staff_expenses != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				/*_facturas_costes_list = InputInvoiceList.GetCostesByExpedienteList(oidExpediente, false);
				Datos_FCostes.DataSource = _facturas_costes_list;
				PgMng.Grow();

				PendienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalPendiente().ToString("N2");
				TotalFacturasCostes_NTB.Text = _facturas_costes_list.Total().ToString("N2");
				ExpedienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalExpediente().ToString("N2");
				ImpuestosFacturasCostes_NTB.Text = _facturas_costes_list.TotalImpuestos().ToString("N2");

				_conceptos_costes_list = InputInvoiceLineList.GetCostesByExpedienteList(oidExpediente, false, true);
				Datos_CCostes.DataSource = _conceptos_costes_list;
				TotalConceptosCostes_NTB.Text = _conceptos_costes_list.Total().ToString("N2");
				ImpuestosConceptosCostes_NTB.Text = _conceptos_costes_list.TotalImpuestos().ToString("N2");

				Costes_NTB.Text = TotalConceptosCostes_NTB.DecimalValue.ToString("C2");

				CalculateBeneficios();*/

				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadStaffExpenses()
		{
			if (_staff_expenses == null)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					_staff_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.Empleado, true, false);
					WRStaff_BS.DataSource = _staff_expenses;
					PgMng.Grow(string.Empty);

					CalculateTotales();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}
		protected virtual void LoadStaffExpenses(long oidExpedient)
		{
			if (_staff_expenses != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				/*_facturas_costes_list = InputInvoiceList.GetCostesByExpedienteList(oidExpediente, false);
				Datos_FCostes.DataSource = _facturas_costes_list;
				PgMng.Grow();

				PendienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalPendiente().ToString("N2");
				TotalFacturasCostes_NTB.Text = _facturas_costes_list.Total().ToString("N2");
				ExpedienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalExpediente().ToString("N2");
				ImpuestosFacturasCostes_NTB.Text = _facturas_costes_list.TotalImpuestos().ToString("N2");

				_conceptos_costes_list = InputInvoiceLineList.GetCostesByExpedienteList(oidExpediente, false, true);
				Datos_CCostes.DataSource = _conceptos_costes_list;
				TotalConceptosCostes_NTB.Text = _conceptos_costes_list.Total().ToString("N2");
				ImpuestosConceptosCostes_NTB.Text = _conceptos_costes_list.TotalImpuestos().ToString("N2");

				Costes_NTB.Text = TotalConceptosCostes_NTB.DecimalValue.ToString("C2");

				CalculateBeneficios();*/

				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadToolsExpenses() 
		{
			if (_tools_expenses == null)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					_tools_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.Tool, true, false);
					WRTools_BS.DataSource = _tools_expenses;
					PgMng.Grow(string.Empty);

					CalculateTotales();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}
		protected virtual void LoadToolsExpenses(long oidExpedient)
		{
			if (_staff_expenses != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				/*_facturas_costes_list = InputInvoiceList.GetCostesByExpedienteList(oidExpediente, false);
				Datos_FCostes.DataSource = _facturas_costes_list;
				PgMng.Grow();

				PendienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalPendiente().ToString("N2");
				TotalFacturasCostes_NTB.Text = _facturas_costes_list.Total().ToString("N2");
				ExpedienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalExpediente().ToString("N2");
				ImpuestosFacturasCostes_NTB.Text = _facturas_costes_list.TotalImpuestos().ToString("N2");

				_conceptos_costes_list = InputInvoiceLineList.GetCostesByExpedienteList(oidExpediente, false, true);
				Datos_CCostes.DataSource = _conceptos_costes_list;
				TotalConceptosCostes_NTB.Text = _conceptos_costes_list.Total().ToString("N2");
				ImpuestosConceptosCostes_NTB.Text = _conceptos_costes_list.TotalImpuestos().ToString("N2");

				Costes_NTB.Text = TotalConceptosCostes_NTB.DecimalValue.ToString("C2");

				CalculateBeneficios();*/

				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		#endregion

		#region Business Methods

		protected override void CalculateTotales()
		{
			base.CalculateTotales();

			WorkReportsExpenses_NTB.Text = TotalWorkReports_NTB.Text;
			WRStaffTotal_NTB.Text = (_staff_expenses != null) ? _staff_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
			WRToolsTotal_NTB.Text = (_tools_expenses != null) ? _tools_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
			WRDeliveriesTotal_NTB.Text = (_deliveries_expenses != null) ? _deliveries_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
			WRCategoriesTotal_NTB.Text = (_categories_expenses != null) ? _categories_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
		}

		protected override decimal GetExpenses()
		{
			return base.GetExpenses() + TotalWorkReports_NTB.DecimalValue;
		}

        #endregion

        #region Actions

		protected virtual void AddWorkReportAction() { }
		protected virtual void DeleteWorkReportAction() { }

        protected virtual void EditEmployeeAction() { }
        protected virtual void EditToolAction() { }
        protected virtual void EditWorkReportAction() { }
        protected virtual void EditWorkDeliveryAction() { }

        #endregion

        #region Buttons
        
        private void Producto_BT_Click(object sender, EventArgs e)
        {
            SelectProductNameAction();
        }

        #endregion

        #region Events

		private void AddWorkReport_TI_Click(object sender, EventArgs e) { AddWorkReportAction(); }
		private void EditWorkReport_TI_Click(object sender, EventArgs e) { EditWorkReportAction(); }
		private void DeleteWorkReport_TI_Click(object sender, EventArgs e) { DeleteWorkReportAction(); }
        private void WorkReport_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditWorkReportAction(); }
        private void WRCategories_DGW_SelectionChanged(object sender, EventArgs e) { if (!EventsEnabled) return; LoadCategoriesDetailsExpenses(); }
        private void WRDeliveries_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditWorkDeliveryAction(); }
        private void WRTools_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditToolAction(); }
        private void WRStaff_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditEmployeeAction(); }

        #endregion  
	}
}

