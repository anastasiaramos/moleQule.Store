using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.WorkReport;

namespace moleQule.Face.Store
{
    public partial class WorkReportForm : Skin04.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		public override Type EntityType { get { return typeof(InputDelivery); } }

		public virtual WorkReport Entity { get { return null; } set { } }
		public virtual WorkReportInfo EntityInfo { get { return null; } }

		protected StoreInfo _almacen = null;
		protected ExpedientInfo _expedient = null;

        #endregion

        #region Factory Methods

        public WorkReportForm() 
			: this(-1, null, true, null ) {}

		public WorkReportForm(long oid, Form parent)
			: base(oid, new object[1] { null }, true, parent)
        {
            InitializeComponent();
        }

		public WorkReportForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			WRResource.Tag = 0.5;
			WRComments.Tag = 0.5;

			cols.Add(WRResource);
			cols.Add(WRComments);

			ControlsMng.MaximizeColumns(Lines_DGW, cols);

			Lines_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

        public override void FormatControls()
        {
            MaximizeForm(1200,0);
            base.FormatControls();

			From_DTP.Checked = true;
			Till_DTP.Checked = true;

			Lines_DGW.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			Lines_DGW.AllowUserToResizeRows = true;
			Lines_DGW.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
			Lines_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
			Lines_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            HideComponentes();
        }

		protected virtual void RefreshLines()
		{
			Lines_BS.ResetBindings(true);
			Lines_DGW.Refresh();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					break;
			}
		}

		#endregion

		#region Source

		public override void RefreshSecondaryData()
		{
		}

        protected virtual void HideComponentes() { }

        #endregion

		#region Validation & Format

		#endregion

        #region Print

        public override void PrintObject()
        {
            WorkReportReportMng reportMng = new WorkReportReportMng(AppContext.ActiveSchema);

            ReportViewer.SetReport(reportMng.GetDetailReport(EntityInfo));
            ReportViewer.ShowDialog();
        }

        #endregion

        #region Actions

        protected override void PrintAction() { PrintObject(); }

        protected virtual void RectificativoAction() {}
        protected virtual void AddEmployeeLineAction() {}
		protected virtual void AddDeliveryLineAction() { }
		protected virtual void AddToolLineAction() { }
        protected virtual void EditLineAction() {}
        protected virtual void DeleteLineAction() {}
		protected virtual void SelectAlmacenLineaAction() { }
		protected virtual void SelectExpedienteLineaAction() { }
		protected virtual void SelectImpuestoLineaAction() { }
        protected virtual void SetIRPFAction() { }

		protected virtual void UpdateWorkReportAction() { }

        #endregion

        #region Buttons

		private void AddEmployeeLine_TI_Click(object sender, EventArgs e) { AddEmployeeLineAction(); }
		private void AddToolLine_TI_Click(object sender, EventArgs e) { AddToolLineAction(); }
		private void AddDeliveryLine_TI_Click(object sender, EventArgs e) { AddDeliveryLineAction(); }
        private void EditLine_TI_Click(object sender, EventArgs e) { EditLineAction(); }
        private void DeleteLine_TI_Click(object sender, EventArgs e) { DeleteLineAction(); }

        #endregion

        #region Events

		private void Conceptos_DGW_CellValidated(object sender, DataGridViewCellEventArgs e) { UpdateWorkReportAction(); }

		private void Conceptos_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.Columns[e.ColumnIndex].Name == Almacen.Name) SelectAlmacenLineaAction();
		}

        private void Conceptos_DGW_DoubleClick(object sender, EventArgs e) { DefaultAction(); }
        
        #endregion
    }
}