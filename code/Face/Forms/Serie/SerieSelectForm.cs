using System;
using System.Windows.Forms;

using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class SerieSelectForm : SerieMngForm
    {

        #region Factory Methods

        public SerieSelectForm(Form parent)
            : this(parent, null) { }

        public SerieSelectForm(Form parent, SerieList list)
            : base(true, parent, list)
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
