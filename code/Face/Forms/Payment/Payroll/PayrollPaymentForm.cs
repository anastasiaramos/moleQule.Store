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
	public partial class PayrollPaymentForm : Skin01.ItemMngSkinForm
	{
		#region Attributes & Properties

        public const string ID = "PayrollPaymentForm";
		public static Type Type { get { return typeof(PayrollPaymentForm); } }

		protected override int BarSteps { get { return base.BarSteps + 0; } }

        public virtual Payment Entity { get { return null; } set { } }
		public virtual PaymentInfo EntityInfo { get { return null; } }

		protected string LinkedBTValue { get { return Lineas_DGW.CurrentRow.Cells[VinculadoLinea.Index].Value.ToString(); } }

		protected decimal _deallocated = 0;

		#endregion

		#region Factory Methods

		public PayrollPaymentForm()
			: this(-1, null, true, null) { }

		public PayrollPaymentForm(long oid, object[] parameters, bool isModal, Form parent)
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
			DescripcionLinea.Tag = 0.6;

			cols.Add(DescripcionLinea);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);
		}

		public override void FormatControls()
		{
			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1250) ? 1250 : Screen.PrimaryScreen.WorkingArea.Width;
			MaximizeForm(new Size(maxWidth, 0));
			ControlsMng.Center(Source_Panel);

			base.FormatControls();
		}

		protected virtual void SetGridColors(Control control) { }

		protected virtual void MarkControl(Control ctl) {}

		protected virtual void MarkAsNoActiva(DataGridViewRow row)
		{
			row.Cells[AsignadoLinea.Index].Style.BackColor = Color.LightGreen;
		}

		protected virtual void MarkAsActiva(DataGridViewRow row)
		{
			row.Cells[AsignadoLinea.Index].Style.BackColor = row.Cells[TotalLinea.Index].Style.BackColor;
		}

		#endregion

		#region Business Methods

		protected virtual void SetPaymentMethod() {}
		protected virtual void UpdateAllocated() {}

		#endregion

		#region Actions

		protected virtual void EditLineAllocationAction(DataGridViewRow row) {}
		protected virtual void LinkAction() {}
        protected virtual void SetPaymentStatusAction() { }

		#endregion

		#region Events
        
        private void EstadoPago_BT_Click(object sender, EventArgs e)
        {
            SetPaymentStatusAction();
        }

		private void Nominas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Lineas_DGW.Columns[e.ColumnIndex].Name == VinculadoLinea.Name)
			{
				LinkAction();
			}
		}

		private void Nominas_DGW_DoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.CurrentRow == null) return;
			EditLineAllocationAction(Lineas_DGW.CurrentRow);
		}

		#endregion
	}
}