using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;
using moleQule.Library.Store; 

namespace moleQule.Face.Store
{
    public partial class EmployeeSelectForm : EmployeeMngForm
    {
        #region Factory Methods

        public EmployeeSelectForm()
            : this(null, moleQule.Base.EEstado.Todos) {}

        public EmployeeSelectForm(Form parent, moleQule.Base.EEstado estado)
            : this(parent, null) {}

        public EmployeeSelectForm(Form parent, EmployeeList list)
            : this(parent, moleQule.Base.EEstado.Todos, list) {}

        protected EmployeeSelectForm(Form parent, moleQule.Base.EEstado estado, EmployeeList list)
			: base(true, parent, estado, list)
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
