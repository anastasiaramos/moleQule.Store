using System;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineSelectForm : LivestockBookLineMngForm
    {
        #region Factory Methods

        public LivestockBookLineSelectForm()
            : this(null) {}

        public LivestockBookLineSelectForm(Form parent)
            : this(parent, null) {}

        public LivestockBookLineSelectForm(Form parent, LivestockBookLineList list)
            : base(true, parent, list, 0)
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
