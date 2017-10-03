using System;
using System.Windows.Forms;

using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InputInvoiceSelectForm : InputInvoiceMngForm
    {
        #region Factory Methods

        public InputInvoiceSelectForm()
            : this(null) {}

        public InputInvoiceSelectForm(Form parent)
            : this(parent, null) {}

        public InputInvoiceSelectForm(Form parent, InputInvoiceList lista)
            : base(true, parent, ETipoFacturas.Todas, lista)
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
