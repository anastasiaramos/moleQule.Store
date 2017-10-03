using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollBatchForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual PayrollBatch Entity { get { return null; } set { } }
        public virtual PayrollBatchInfo EntityInfo { get { return null; } }

        public DataGridViewTextBoxColumn ImporteNomina { get { return NetoPersonal; } }
        protected long _oid_nomina = 0;
		
        #endregion

        #region Factory Methods

        public PayrollBatchForm() 
			: this(-1) {}

        public PayrollBatchForm(long oid) 
			: this(oid, true, null) {}

		public PayrollBatchForm(bool isModal)
			: this(-1, isModal, null) { }

        public PayrollBatchForm(long oid, bool isModal, Form parent)
            : this(oid, -1, isModal, parent) { }

        public PayrollBatchForm(long oid_remesa, long oid_nomina, bool isModal, Form parent)
            : base(oid_remesa, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout & Source

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			PREmployee.Tag = 0.3;
			PRComments.Tag = 0.7;

			cols.Add(PREmployee);
			cols.Add(PRComments);

			ControlsMng.MaximizeColumns(Payrolls_DGW, cols);
		}

        public override void FormatControls()
        {
			SetGridFormat();

            MaximizeForm(new Size(1200, 0));
            base.FormatControls();

            General_GB.Left = (Width - General_GB.Width - Summary_GB.Width - 10) / 2;
            Summary_GB.Left = General_GB.Right + 9;
            ControlsMng.CenterLeft(Payrolls_LB);
		}

		protected virtual void SetGridFormat() 
		{
		
		}
		
		#endregion

		#region Business Methods

        protected virtual void UpdatePayroll() { }
		protected virtual void UpdateTotalRemesa() {}

		#endregion

		#region Actions

        protected virtual void CalculatePayrollAction() { }
        protected virtual void EditEmployeeAction() { }
        protected virtual void DeletePayrollAction() { }
        protected virtual void NewPayrollAction() { }
		protected virtual void SetEmployeeAction() {}
		protected virtual void SetStatusAction() {}

        #endregion

		#region Buttons

        private void NewPayroll_TI_Click(object sender, EventArgs e) { NewPayrollAction(); }
        private void EditEmployee_TI_Click(object sender, EventArgs e) { EditEmployeeAction(); }
        private void DeletePayroll_TI_Click(object sender, EventArgs e) { DeletePayrollAction(); }
        private void CalculatePayroll_TI_Click(object sender, EventArgs e) { CalculatePayrollAction(); }

		#endregion

		#region Events

		private void Payrolls_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Payrolls_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if ((e.ColumnIndex == PREmployee.Index))
			{
				SetEmployeeAction();
			}
			if ((e.ColumnIndex == Estado.Index))
			{
				SetStatusAction();
			}
		}

		private void Payrolls_DGW_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (Payrolls_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

            if (new int[] { Bruto.Index, BaseIRPF.Index, Descuentos.Index, Seguro.Index }.Contains(e.ColumnIndex))
            {
                UpdatePayroll();
            }
			else if ((e.ColumnIndex == NetoPersonal.Index))
			{
				UpdateTotalRemesa();
			}
		}

        #endregion
    }
}