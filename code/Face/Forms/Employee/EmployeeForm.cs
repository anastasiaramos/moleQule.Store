using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class EmployeeForm : Skin04.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        public override Type EntityType { get { return typeof(Employee); } }

        public EmployeeList _instructors;
        protected WorkReportResourceList _work_reports_month = null;
        protected WorkReportResourceList _work_reports = null;

        protected List<ComboBoxSourceList> lista_sources = new List<ComboBoxSourceList>();
        protected List<ComboBoxSourceList> lista_sources_a = new List<ComboBoxSourceList>();

        public virtual Employee Entity { get { return null; } set { } }
        public virtual EmployeeInfo EntityInfo { get { return null; } }

        protected bool _cerrado = true;

        #endregion

        #region Factory Methods

        public EmployeeForm() 
			: this(-1, null) { }

		public EmployeeForm(long oid, Form parent)
			: base(oid, new object[1]{null}, true, parent) 
        {
            InitializeComponent();
        }

		public EmployeeForm(IAcreedor item, Form parent)
			: base(item.Oid, new object[1]{item}, true, parent)
		{
			InitializeComponent();
		}

        #endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
            CuentaContable_TB.Enabled = Employee.CanEditCuentaContable();
            CuentaContable_TB.ReadOnly = !Employee.CanEditCuentaContable();
            CuentaContable_BT.Enabled = Employee.CanEditCuentaContable();
		}

		#endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Producto.Tag = 1;

			cols.Add(Producto);

			ControlsMng.MaximizeColumns(Productos_DGW, cols);
		}

		public override void FormatControls()
		{
			//IDE COMPATIBILITY
			if (AppContext.User == null) return;

			MaximizeForm(new Size(1000, 725));

			base.FormatControls();

            CuentaContable_TB.Mask = (EntityInfo.CuentaContable != moleQule.Accounting.Resources.Defaults.NO_CONTABILIZAR)
							? Library.Invoice.ModuleController.GetCuentasMask()
							: string.Empty;
			PrecioProducto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

			Main_TC.TabPages.Remove(Productos_TP);

			Productos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WorkReportMonth_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            WorkReport_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			SetDependentControlSource(Perfil_GB);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
					break;
			}
		}

		#endregion

		#region Source

        protected virtual void LoadData()
        {
            EnableEvents(false);

            if (Main_TC.SelectedTab == WorkReport_TP)
            {
                LoadWorkReportsMonth();
                LoadWorkReports();
            }

            EnableEvents(true);
        }

        protected virtual void LoadWorkReportsMonth()
        {
            if (_work_reports_month == null)
            {
                try
                {
                    PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

                    int year = moleQule.Common.ModulePrincipal.GetUseActiveYear()
                                    ? moleQule.Common.ModulePrincipal.GetActiveYear().Year
                                    : 0;

                    _work_reports_month = WorkReportResourceList.GetByEmployeeList(EntityInfo.Oid, year, 0, true, false);
                    WorkReportMonth_BS.DataSource = _work_reports_month;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }
        protected virtual void LoadWorkReports(bool reload = false)
        {
            if (WorkReportMonth_DGW.CurrentRow == null)
            {
                _work_reports = null;
                WorkReport_BS.DataSource = _work_reports;
            }
            else if (_work_reports == null || reload)
            {
                try
                {
                    PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

                    WorkReportResourceInfo work_report_month = WorkReportMonth_DGW.CurrentRow.DataBoundItem as WorkReportResourceInfo;

                    _work_reports = WorkReportResourceList.GetByEmployeeList(EntityInfo.Oid, work_report_month.From.Year, work_report_month.From.Month, false, false);
                    WorkReport_BS.DataSource = _work_reports;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }
         
        public override void RefreshSecondaryData()
		{
			Datos_FormaPago.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList();
			PgMng.Grow();

			Datos_MedioPago.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList();
			PgMng.Grow();

			ETipoID[] list = { ETipoID.DNI, ETipoID.CIF, ETipoID.OTROS };
			Datos_TipoID.DataSource = moleQule.Common.Structs.EnumText<ETipoID>.GetList(list);
			PgMng.Grow();
		}

		protected override void RefreshMainData()
		{
			Images.Show(EntityInfo.Foto, Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH, Foto_PB);
			PgMng.Grow();
		}

        #endregion

        #region Validation & Format

        #endregion

        #region Print

        #endregion

        #region Actions

		protected override void DocumentsAction()
		{
			try
			{
				AgenteEditForm form = new AgenteEditForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia);
				form.ShowDialog(this);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia);
					form.ShowDialog(this);
				}
			}
		}

        protected virtual void SetImpuestoAction() {}
        protected virtual void SetImpuestoDefectoAction() {}
        protected virtual void SetCuentaAsociadaAction() {}
        protected virtual void AddProductoAction() {}
		protected virtual void EditProductoAction() {}
        protected virtual void EditWorkReportAction() { }
        protected virtual void DeleteProductAction() {}
        protected virtual void PrintWorkReportListAction() 
        {
            if (!ControlsMng.IsCurrentItemValid(WorkReportMonth_DGW)) return;
            WorkReportResourceInfo item = ControlsMng.GetCurrentItem(WorkReportMonth_DGW) as WorkReportResourceInfo;

            PgMng.Reset(2, 1, Face.Resources.Messages.LOADING_DATA, this);

            string title = Resources.Labels.EMPLOYEE_WORKREPORTS_LIST;
            string filter = Resources.Labels.EMPLOYEE + " = " + item.Resource + "; " + Resources.Labels.YEAR + " = " + item.Year + "; " + Resources.Labels.MONTH + " = " + item.Month;
  
            WorkReportReportMng rptMng = new WorkReportReportMng(AppContext.ActiveSchema, title, filter);

            ReportClass report = rptMng.GetWorkReportResourceList(WorkReportResourceList.GetList(WorkReport_BS.DataSource as IList<WorkReportResourceInfo>));
            PgMng.FillUp();

            ShowReport(report);
        }
		protected virtual void SelectLineTaxAction() { }
		protected virtual void SelectLineTaxTypeAction() { }
        protected virtual void SelectTarjetaAsociadaAction() { }
		protected void SendMailAction()
		{
			PgMng.Reset(3, 1, moleQule.Face.Resources.Messages.OPENING_EMAIL_CLIENT, this);

			MailParams mail = new MailParams();

			mail.To = EntityInfo.Email;

			try
			{
				PgMng.Grow();

				EMailSender.MailTo(mail);
				PgMng.Grow();
			}
			catch
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

        #endregion

        #region Buttons
        
        private void SendMail_BT_Click(object sender, EventArgs e) { SendMailAction(); }
		private void AddProducto_TI_Click(object sender, EventArgs e) {	AddProductoAction(); }
		private void EditProducto_TI_Click(object sender, EventArgs e) { EditProductoAction(); }
        private void DeleteProducto_TI_Click(object sender, EventArgs e) { DeleteProductAction(); }
        private void EditWorkReport_TI_Click(object sender, EventArgs e) { EditWorkReportAction(); }
        private void PrintWorkReport_TI_Click(object sender, EventArgs e) { PrintWorkReportListAction(); }

        #endregion

        #region Events

        private void Main_TC_SelectedIndexChanged(object sender, EventArgs e) { LoadData(); }

        private void Productos_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Productos_DGW.CurrentRow == null) return;
            if (e.ColumnIndex == -1) return;

			if (Productos_DGW.Columns[e.ColumnIndex].Name == Impuesto.Name) { SelectLineTaxAction(); }
			else if (Productos_DGW.Columns[e.ColumnIndex].Name == TipoDescuentoLabel.Name) SelectLineTaxTypeAction();
        }

        private void Tarjeta_BT_Click(object sender, EventArgs e)
        {
            SelectTarjetaAsociadaAction();
        }

        private void WorkReportMonth_DGW_SelectionChanged(object sender, EventArgs e) { if (!EventsEnabled) return; LoadWorkReports(true); }

        private void WorkReport_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditWorkReportAction();
        }

        #endregion
    }
}


