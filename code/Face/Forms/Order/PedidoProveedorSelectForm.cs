using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;
using moleQule.Library.Store; 

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorSelectForm : PedidoProveedorMngForm
    {

        #region Factory Methods

        public PedidoProveedorSelectForm()
            : this(null, moleQule.Base.EEstado.Todos) {}

        public PedidoProveedorSelectForm(Form parent, moleQule.Base.EEstado estado)
            : this(parent, null) {}

		public PedidoProveedorSelectForm(Form parent, PedidoProveedorList lista)
            : this(parent, moleQule.Base.EEstado.Todos, lista) {}

		protected PedidoProveedorSelectForm(Form parent, moleQule.Base.EEstado estado, PedidoProveedorList lista)
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
