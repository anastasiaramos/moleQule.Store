using System;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ProviderSelectForm : ProviderMngForm
    {
        #region Factory Methods

        public ProviderSelectForm(Form parent, moleQule.Base.EEstado estado)
            : base(true, parent, ETipoAcreedor.Todos, estado)
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
