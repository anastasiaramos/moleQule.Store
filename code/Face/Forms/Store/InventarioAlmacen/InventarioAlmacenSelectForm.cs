using System;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenSelectForm : InventarioAlmacenMngForm
    {
        #region Factory Methods

        public InventarioAlmacenSelectForm()
            : this(null) {}

        public InventarioAlmacenSelectForm(Form parent)
            : this(parent, null) {}

		public InventarioAlmacenSelectForm(Form parent, InventarioAlmacenList lista)
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
