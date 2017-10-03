using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TransporterSelectForm : TransporterMngForm
    {
        #region Factory Methods

        public TransporterSelectForm()
            : this(null, moleQule.Base.EEstado.Todos) {}

        public TransporterSelectForm(Form parent, moleQule.Base.EEstado estado)
            : this(parent, estado, null) { }

        public TransporterSelectForm(Form parent, TransporterList lista)
            : base(true, parent, moleQule.Base.EEstado.Todos, lista) {  }

		protected TransporterSelectForm(Form parent, moleQule.Base.EEstado estado, TransporterList lista)
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
