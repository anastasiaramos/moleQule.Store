using System;
using System.Windows.Forms;

using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ProductSelectForm : ProductMngForm
    {
        #region Factory Methods

        public ProductSelectForm(Form parent)
            : this(parent, null) {}

        public ProductSelectForm(Form parent, ProductList list)
            : base(true, parent, list, ETipoProducto.Todos)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

		#region Layout

		#endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}