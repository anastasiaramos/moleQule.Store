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
	public partial class ExpensePaymentForm : Skin01.ItemMngSkinForm
	{
		#region Attributes & Properties

        public const string ID = "ExpensePaymentForm";
		public static Type Type { get { return typeof(ExpensePaymentForm); } }

		protected override int BarSteps { get { return base.BarSteps + 0; } }

        public virtual Payment Entity { get { return null; } set { } }
		public virtual PaymentInfo EntityInfo { get { return null; } }

		protected string VinculadoBTValue { get { return Lineas_DGW.CurrentRow.Cells[Vinculado.Index].Value.ToString(); } }

		protected decimal _deallocated = 0;
        protected PaymentInfo _root = null;

		#endregion

		#region Factory Methods

		public ExpensePaymentForm()
			: this(-1, null, true, null) { }

		public ExpensePaymentForm(long oid, object[] parameters, bool isModal, Form parent)
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
			DescripcionCol.Tag = 1;

			cols.Add(DescripcionCol);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);
		}

		public override void FormatControls()
		{
			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1250) ? 1250 : Screen.PrimaryScreen.WorkingArea.Width;
			MaximizeForm(new Size(maxWidth, 0));
			base.FormatControls();

			ControlsMng.Center(Source_Panel);
		}

		protected virtual void SetGridColors(Control control) { }

		protected virtual void MarkControl(Control ctl) {}

		protected virtual void MarkAsNoActiva(DataGridViewRow row)
		{
			row.Cells[Asignado.Index].Style.BackColor = Color.LightGreen;
		}

		protected virtual void MarkAsActiva(DataGridViewRow row)
		{
			row.Cells[Asignado.Index].Style.BackColor = row.Cells[TotalPagado.Index].Style.BackColor;
		}

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

		protected virtual void SetMedioPago() {}
		protected virtual void UpdateAllocated() {}

		#endregion

		#region Actions

        protected virtual void EditLineAssignmentAction(DataGridViewRow row) { }
		protected virtual void LinkAction() {}
        protected virtual void SetPaymentStatusAction() { }

		#endregion

		#region Events

		private void Lineas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Lineas_DGW.Columns[e.ColumnIndex].Name == Vinculado.Name)
			{
				LinkAction();
			}
		}

		private void Lineas_DGW_DoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
            EditLineAssignmentAction(Lineas_DGW.CurrentRow);
		}

        private void EstadoPago_BT_Click(object sender, EventArgs e)
        {
            SetPaymentStatusAction();
        }

		#endregion
	}
}
