using System;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class WorkSelectForm : ExpedienteSelectForm
    {
        #region Factory Methods

        public WorkSelectForm(Form parent)
			: this(parent, ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Work, false)) { }

		public WorkSelectForm(Form parent, ExpedienteList list)
			: base(parent, list, moleQule.Store.Structs.ETipoExpediente.Work)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Layout

        #endregion
    }
}
