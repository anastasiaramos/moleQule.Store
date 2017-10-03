using System;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ExpedienteSelectForm : ExpedienteMngForm
    {
        #region Factory Methods

        public ExpedienteSelectForm(Form parent)
            : this(parent, null) {}

		public ExpedienteSelectForm(Form parent, ExpedienteList list)
			: this(parent, list, moleQule.Store.Structs.ETipoExpediente.Todos) { }
		
		public ExpedienteSelectForm(Form parent, ExpedienteList list, moleQule.Store.Structs.ETipoExpediente expedientType)
			: base(true, parent, list, expedientType)
		{
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Layout

        protected override void SetRowFormat(DataGridViewRow row) {}

        #endregion

        #region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
