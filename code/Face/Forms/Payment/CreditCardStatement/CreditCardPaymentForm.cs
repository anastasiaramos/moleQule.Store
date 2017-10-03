using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class CreditCardPaymentForm : Skin01.ItemMngSkinForm
	{
		#region Attributes & Properties

        public const string ID = "CreditCardPaymentForm";
        public static Type Type { get { return typeof(CreditCardPaymentForm); } }

		protected override int BarSteps { get { return base.BarSteps + 0; } }

        public virtual Payment Entity { get { return null; } set { } }
		public virtual PaymentInfo EntityInfo { get { return null; } }

		protected string VinculadoBTValue { get { return Lines_DGW.CurrentRow.Cells[Vinculado.Index].Value.ToString(); } }

		protected decimal _deallocated = 0;
        protected PaymentInfo _root = null;

		#endregion

		#region Factory Methods

		public CreditCardPaymentForm()
			: this(-1, null, true, null) { }

        public CreditCardPaymentForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			cols.Clear();
            LiComments.Tag = 1;

            cols.Add(LiComments);

			ControlsMng.MaximizeColumns(Lines_DGW, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

            MaximizeForm(new Size(1024, 0));

			ControlsMng.Center(Payment_Panel);
		}

		protected virtual void SetGridColors(Control control) { }

		protected virtual void MarkControl(Control ctl) {}

		protected virtual void MarkAsUnlinked(DataGridViewRow row)
		{
			row.Cells[Asignado.Index].Style.BackColor = Color.LightGreen;
		}

		protected virtual void MarkAsLinked(DataGridViewRow row)
		{
			row.Cells[Asignado.Index].Style.BackColor = row.Cells[TotalPagado.Index].Style.BackColor;
		}

		#endregion

        #region Source

        protected virtual void LoadCreditCardStatements() {}

        #endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
		//{
		//    PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema);
		//    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
		//    ReportViewer.ShowDialog();
		//}

		#endregion

		#region Business Methods

		protected virtual void SetPaymentMethod() {}
		protected virtual void UpdateAllocated() {}

		#endregion

		#region Actions

        protected virtual void EditLineAllocationAction(DataGridViewRow row) { }
		protected virtual void LinkAction() {}
        protected virtual void SetCreditCardAction() { }
        protected virtual void SetPaymentStatusAction() { }
        protected virtual void ViewCashLinesAction() { }
        protected virtual void ViewStatementAction() { }

		#endregion

		#region Events

        private void CashLines_TS_Click(object sender, EventArgs e) { ViewCashLinesAction(); }

        private void CreditCardPaymentForm_Load(object sender, EventArgs e)
        {
            if (PgMng != null) PgMng.FillUp();
            SetCreditCardAction();
        }

		private void Lineas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Lines_DGW.Columns[e.ColumnIndex].Name == Vinculado.Name)
			{
				LinkAction();
			}
		}

		private void Lineas_DGW_DoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.CurrentRow == null) return;
            EditLineAllocationAction(Lines_DGW.CurrentRow);
		}

        private void EstadoPago_BT_Click(object sender, EventArgs e)
        {
            SetPaymentStatusAction();
        }

        private void ViewStatement_TI_Click(object sender, EventArgs e) { ViewStatementAction(); }

		#endregion
	}
}