using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Library.Store;
using moleQule.Common;

namespace moleQule.Face.Store
{
    public partial class CustomAgentSelectForm : CustomAgentMngForm
    {
        #region Factory Methods

        public CustomAgentSelectForm()
            : this(null, moleQule.Base.EEstado.Todos) {}

        public CustomAgentSelectForm(Form parent, moleQule.Base.EEstado estado)
            : this(parent, null) {}

        public CustomAgentSelectForm(Form parent, DespachanteList lista)
            : this(parent, moleQule.Base.EEstado.Todos, lista) {}

		protected CustomAgentSelectForm(Form parent, moleQule.Base.EEstado estado, DespachanteList lista)
			: base(true, parent, estado, lista)
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
