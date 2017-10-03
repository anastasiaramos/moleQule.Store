using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class WorkReportSelectForm : WorkReportMngForm
    {
        #region Factory Methods

        public WorkReportSelectForm()
            : this(null) {}

        public WorkReportSelectForm(Form parent)
            : this(parent, null) {}

        public WorkReportSelectForm(Form parent, WorkReportList lista)
            : base(true, parent, lista)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

		#endregion
    }
}
