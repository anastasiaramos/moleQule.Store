using System;
using System.Windows.Forms;

using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpenseSelectForm : ExpenseMngForm
    {
        #region Factory Methods

        public ExpenseSelectForm(Form parent)
            : this(parent, null) {}

        public ExpenseSelectForm(Form parent, ExpenseList list)
			: base(true, parent, ECategoriaGasto.Todos, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

		public ExpenseSelectForm(Form parent, ECategoriaGasto categoria, ExpenseList list)
			: base(true, parent, categoria, list)
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