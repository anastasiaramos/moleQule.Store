using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class BatchSelectForm : BatchMngForm
    {
        #region Factory Methods

        public BatchSelectForm(Form parent)
            : this(parent, null, null) {}

		public BatchSelectForm(Form parent, SerieInfo serie)
			: this(parent, serie, null) {}

		public BatchSelectForm(Form parent, BatchList lista)
			: this(parent, null, lista) {}

		public BatchSelectForm(Form parent, SerieInfo serie, BatchList lista)
            : base(true, parent, serie, lista)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			HideAction(molAction.SelectAll);

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
