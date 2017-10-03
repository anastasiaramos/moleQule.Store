using System;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Base;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class SupplierSelectForm : SupplierMngForm
    {
        #region Factory Methods

		public SupplierSelectForm(Form parent, ProveedorList list)
			: base(true, parent, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos, list)
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
