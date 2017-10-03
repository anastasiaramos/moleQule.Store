using System;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollBatchSelectForm : PayrollBatchMngForm
    {
        #region Factory Methods

        public PayrollBatchSelectForm()
            : this(null) {}

        public PayrollBatchSelectForm(Form parent)
            : this(parent, null) {}
		
		public PayrollBatchSelectForm(Form parent, PayrollBatchList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }
		
        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
