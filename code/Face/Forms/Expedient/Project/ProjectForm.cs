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
    public partial class ProjectForm : ExpedienteForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        protected OutputInvoiceList _works_output_invoices;
        protected ExpedienteList _works = null;
		protected WorkReportList _work_reports = null;
        protected WorkReportList _work_reports_works = null;
		protected WorkReportResourceList _staff_expenses = null;
        protected WorkReportResourceList _staff_works_expenses = null;
		protected WorkReportResourceList _tools_expenses = null;
        protected WorkReportResourceList _tools_works_expenses = null;
		protected WorkReportResourceList _deliveries_expenses = null;
        protected WorkReportResourceList _deliveries_works_expenses = null;
        protected WorkReportResourceList _categories_expenses = null;
        protected WorkReportResourceList _categories_detail_expenses = null;        

        #endregion

        #region Factory Methods

        public ProjectForm() 
			: this(-1) {}

		public ProjectForm(long oid) 
			: this(oid, true, null) { }

        public ProjectForm(long oid, bool isModal, Form parent)
			: base(oid, moleQule.Store.Structs.ETipoExpediente.Project, isModal, parent)
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

            cm = (CurrencyManager)BindingContext[Works_DGW.DataSource];
            cm.SuspendBinding();

            cols = new List<DataGridViewColumn>();
            WoDescription.Tag = 0.4;
            WoComments.Tag = 0.6;

            cols.Add(WoDescription);
            cols.Add(WoComments);

            ControlsMng.MaximizeColumns(Works_DGW, cols);

			cm = (CurrencyManager)BindingContext[WorkReport_DGW.DataSource];
			cm.SuspendBinding();

            cols.Clear();
			WRComments.Tag = 1;

			cols.Add(WRComments);

			ControlsMng.MaximizeColumns(WorkReport_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRStaff_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRStResource.Tag = 1;

			cols.Add(WRStResource);

			ControlsMng.MaximizeColumns(WRStaff_DGW, cols);

            cm = (CurrencyManager)BindingContext[WRStaffWorks_DGW.DataSource];
            cm.SuspendBinding();

            cols.Clear();
            WRStWoResource.Tag = 1;

            cols.Add(WRStWoResource);

            ControlsMng.MaximizeColumns(WRStaffWorks_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRTools_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRToResource.Tag = 1;

			cols.Add(WRToResource);

			ControlsMng.MaximizeColumns(WRTools_DGW, cols);

            cm = (CurrencyManager)BindingContext[WRToolsWorks_DGW.DataSource];
            cm.SuspendBinding();

            cols.Clear();
            WRToWoResource.Tag = 1;

            cols.Add(WRToWoResource);

            ControlsMng.MaximizeColumns(WRToolsWorks_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRDeliveries_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRDeComments.Tag = 1;

			cols.Add(WRDeComments);

			ControlsMng.MaximizeColumns(WRDeliveries_DGW, cols);

            cm = (CurrencyManager)BindingContext[WRDeliveriesWorks_DGW.DataSource];
            cm.SuspendBinding();

            cols.Clear();
            WRDeWoComments.Tag = 1;

            cols.Add(WRDeWoComments);

            ControlsMng.MaximizeColumns(WRDeliveriesWorks_DGW, cols);

			cm = (CurrencyManager)BindingContext[WRCategories_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			WRCaCategory.Tag = 1;

			cols.Add(WRCaCategory);

            ControlsMng.MaximizeColumns(WRCategories_DGW, cols);
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

            Works_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WorkReport_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRDeliveries_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRDeliveriesWorks_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRStaff_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRStaffWorks_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			WRTools_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRToolsWorks_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRCategories_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WRCategoriesDetail_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Works_TP
       }

        protected override void SetTabsOrder()
        {
            Pestanas_TC.TabPages.Clear();

            Pestanas_TC.TabPages.Add(General_TP);
            Pestanas_TC.TabPages.Add(Works_TP);
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

        protected override void LoadData() { LoadData(false); }
        protected void LoadData(bool reload)
        {
            EnableEvents(false);

            LoadWorks();
            LoadWorkReports(reload);

            if (Pestanas_TC.SelectedTab == WRCategories_TP || reload)
            {
                LoadCategoriesExpenses(reload);
                LoadCategoriesDetailsExpenses();
            }
            else if (Pestanas_TC.SelectedTab == WRStaff_TP || reload)
            {
                LoadStaffExpenses(reload);
            }
            else if (Pestanas_TC.SelectedTab == WRDeliveries_TP || reload)
            {
                LoadDeliveriesExpenses(reload);
            }
            else if (Pestanas_TC.SelectedTab == WRTools_TP || reload)
            {
                LoadToolsExpenses(reload);
            }

            EnableEvents(true);

            base.LoadData();
        }

        protected virtual void LoadCategoriesExpenses(bool reload = false)
        {
            if (_categories_expenses == null || reload)
            {
                PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
                try
                {
                    _categories_expenses = WorkReportResourceList.GetByCategoryList(0, EntityInfo.Relations.ToChildsOidList(), true, false);
                    WRCategories_BS.DataSource = _categories_expenses;

                    WRCategoriesTotal_NTB.Text = (_categories_expenses != null) ? _categories_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    PgMng.Grow(string.Empty);
                }
                finally
                {
                    PgMng.FillUp();
                }
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

                    _categories_detail_expenses = WorkReportResourceList.GetByCategoryAndResourceList(category.OidCategory, EntityInfo.Relations.ToChildsOidList(), true, false);
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

		protected virtual void LoadDeliveriesExpenses(bool reload = false) 
		{
            if (_deliveries_expenses == null || reload)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
                    _deliveries_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.OutputDelivery, false, false);
					WRDeliveries_BS.DataSource = _deliveries_expenses;

                    WRDeliveriesProjectTotal_NTB.Text = (_deliveries_expenses != null) ? _deliveries_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    _deliveries_works_expenses = WorkReportResourceList.GetList(EntityInfo.GetExpedientsOidList(), ETipoEntidad.OutputDelivery, false, false);
                    WRDeliveriesWorks_BS.DataSource = _deliveries_works_expenses;

                    WRDeliveriesWorksTotal_NTB.Text = (_deliveries_works_expenses != null) ? _deliveries_works_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    PgMng.Grow(string.Empty);

                    WRDeliveriesTotal_NTB.Text = (WRDeliveriesProjectTotal_NTB.DecimalValue + WRDeliveriesWorksTotal_NTB.DecimalValue).ToString("C2");
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

        protected override void LoadIncomes() { LoadIncomes(true); }
        protected virtual void LoadIncomes(bool reload = false)
        {
            LoadIncomes(EntityInfoNoChilds.Oid);
            
            if (_works_output_invoices == null || reload)
            {
                try
                {
                    PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

                    _works_output_invoices = OutputInvoiceList.GetByExpedienteList(EntityInfo.Relations.ToChildsOidList(), false);
                    PgMng.Grow();

                    BeWorksPurchases_NTB.Text = (_works_output_invoices != null) ? _works_output_invoices.TotalExpediente().ToString("C2") : (0).ToString("C2");

                    CalculateBeneficios();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        protected virtual void LoadStaffExpenses(bool reload = false)
		{
			if (_staff_expenses == null || reload)
			{
				PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
                    _staff_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.Empleado, true, false);
					WRStaff_BS.DataSource = _staff_expenses;

                    WRStaffProjectTotal_NTB.Text = (_staff_expenses != null) ? _staff_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    PgMng.Grow(string.Empty);

                    _staff_works_expenses = WorkReportResourceList.GetList(EntityInfo.Relations.ToChildsOidList(), ETipoEntidad.Empleado, true, false);
                    WRStaffWorks_BS.DataSource = _staff_works_expenses;

                    PgMng.Grow(string.Empty);

                    WRStaffWorksTotal_NTB.Text = (_staff_works_expenses != null) ? _staff_works_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    WRStaffTotal_NTB.Text = (WRStaffProjectTotal_NTB.DecimalValue + WRStaffWorksTotal_NTB.DecimalValue).ToString("C2");

					PgMng.Grow(string.Empty);
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

        protected virtual void LoadToolsExpenses(bool reload = false) 
		{
            if (_tools_works_expenses == null || reload)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
                    _tools_expenses = WorkReportResourceList.GetList(EntityInfoNoChilds.Oid, ETipoEntidad.Tool, true, false);
					WRTools_BS.DataSource = _tools_expenses;

                    WRToolsProjectTotal_NTB.Text = (_tools_works_expenses != null) ? _tools_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    PgMng.Grow(string.Empty);

                    _tools_works_expenses = WorkReportResourceList.GetList(EntityInfo.GetExpedientsOidList(), ETipoEntidad.Tool, true, false);
                    WRToolsWorks_BS.DataSource = _tools_works_expenses;

                    WRToolsWorksTotal_NTB.Text = (_tools_works_expenses != null) ? _tools_works_expenses.GetTotal().ToString("C2") : (0).ToString("C2");

                    PgMng.Grow(string.Empty);

                    WRToolsTotal_NTB.Text = (WRToolsProjectTotal_NTB.DecimalValue + WRToolsWorksTotal_NTB.DecimalValue).ToString("C2");
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

        protected virtual void LoadWorks(bool reload = false) { }

        protected virtual void LoadWorkReports(bool reload = false) { }

		#endregion

		#region Business Methods

		protected override void CalculateTotales()
		{
            BeWorkReportWorksExpenses_NTB.Text = WorkReportsWorksTotal_NTB.Text;
            BeWorkReportProjectExpenses_NTB.Text = WorkReportsProjectTotal_NTB.Text;			
            BeWorksExpenses_NTB.Text = (_works != null) ? _works.GetTotalExpenses().ToString("N2") : (0).ToString("N2");

            base.CalculateTotales();
		}

		protected override decimal GetExpenses()
		{
            return base.GetExpenses() + BeWorksExpenses_NTB.DecimalValue + BeWorkReportProjectExpenses_NTB.DecimalValue + BeWorkReportWorksExpenses_NTB.DecimalValue;
		}

        protected override decimal GetIncome()
        {
            return base.GetIncome() + BeWorksPurchases_NTB.DecimalValue;
        }

        protected void UpdateWorkReportsTotals()
        {
            WorkReportsProjectTotal_NTB.Text = (_work_reports != null) ? _work_reports.GetTotal().ToString("C2") : (0).ToString("C2");
            WorkReportsWorksTotal_NTB.Text = (_work_reports_works != null) ? _work_reports_works.GetTotal().ToString("C2") : (0).ToString("C2");
            WorkReportsTotal_NTB.Text = (WorkReportsProjectTotal_NTB.DecimalValue + WorkReportsWorksTotal_NTB.DecimalValue).ToString("C2");

            WorkReportsTotal_NTB.Text = (_work_reports != null) ? _work_reports.GetTotal().ToString("C2") : (0).ToString("N2");
            
            WRDeliveriesTotal_NTB.Text = (_deliveries_expenses != null) ? _deliveries_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
            
            WRCategoriesTotal_NTB.Text = (_categories_expenses != null) ? _categories_expenses.GetTotal().ToString("N2") : (0).ToString("N2");
            
            WRStaffProjectTotal_NTB.Text = (_staff_expenses != null) ? _staff_expenses.GetTotal().ToString("C2") : (0).ToString("C2");
            WRStaffWorksTotal_NTB.Text = (_staff_works_expenses != null) ? _staff_works_expenses.GetTotal().ToString("C2") : (0).ToString("C2");
            WRStaffTotal_NTB.Text = (WRStaffProjectTotal_NTB.DecimalValue + WRStaffWorksTotal_NTB.DecimalValue).ToString("C2");

            WRToolsProjectTotal_NTB.Text = (_tools_works_expenses != null) ? _tools_expenses.GetTotal().ToString("C2") : (0).ToString("C2");
            WRToolsWorksTotal_NTB.Text = (_tools_works_expenses != null) ? _tools_works_expenses.GetTotal().ToString("C2") : (0).ToString("C2");
            WRToolsTotal_NTB.Text = (WRToolsProjectTotal_NTB.DecimalValue + WRToolsWorksTotal_NTB.DecimalValue).ToString("C2");

            CalculateTotales();
        }

        #endregion

        #region Actions

        protected virtual void AddWorkAction() { }
		protected virtual void AddWorkReportAction() { }

        protected virtual void DeleteWorkAction() { }
        protected virtual void DeleteWorkReportAction() { }
		
        protected virtual void EditEmployeeAction() { }
        protected virtual void EditToolAction() { }
        protected virtual void EditWorkAction() { }
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

        private void AddWork_TI_Click(object sender, EventArgs e) { AddWorkAction(); }
		private void AddWorkReport_TI_Click(object sender, EventArgs e) { AddWorkReportAction(); }
        private void DeleteWork_TI_Click(object sender, EventArgs e) { DeleteWorkAction(); }
		private void EditWork_TI_Click(object sender, EventArgs e) { EditWorkAction(); } 
        private void EditWorkReport_TI_Click(object sender, EventArgs e) { EditWorkReportAction(); }
		private void DeleteWorkReport_TI_Click(object sender, EventArgs e) { DeleteWorkReportAction(); }
        private void Works_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditWorkAction(); }
        private void WorkReport_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditWorkReportAction(); }
        private void WRCategories_DGW_SelectionChanged(object sender, EventArgs e) { if (!EventsEnabled) return; LoadCategoriesDetailsExpenses(); }
        private void WRDeliveries_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditWorkDeliveryAction(); }
        private void WRTools_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditToolAction(); }
        private void WRStaff_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditEmployeeAction(); }

        #endregion  
	}
}