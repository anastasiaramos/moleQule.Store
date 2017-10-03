using System;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class LivestockBookSelectForm : LivestockBookMngForm
    {
        #region Factory Methods

        public LivestockBookSelectForm()
            : this(null) {}

        public LivestockBookSelectForm(Form parent)
            : this(parent, null) {}

        public LivestockBookSelectForm(Form parent, LivestockBookList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			
			SetView(molView.Select);
			
            DialogResult = DialogResult.Cancel;
        }
		
        #endregion

        #region Layout & Source

        /// <summary>Formatea los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            SetSelectView();
            base.FormatControls();
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
