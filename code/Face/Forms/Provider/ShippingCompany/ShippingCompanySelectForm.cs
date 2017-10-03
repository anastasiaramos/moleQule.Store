using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;

namespace moleQule.Face.Store
{
    public partial class ShippingCompanySelectForm : ShippingCompanyMngForm
    {
        #region Factory Methods

        public ShippingCompanySelectForm(Form parent, moleQule.Base.EEstado estado)
            : base(true, parent, estado)
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
