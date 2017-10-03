using System;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PaymentSelectForm : PaymentMngForm
    {
        #region Factory Methods

        public PaymentSelectForm(Form parent, ETipoPago tipo)
            : base(true, parent, tipo, null)
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