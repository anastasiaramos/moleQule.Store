using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class AlmacenSelectForm : AlmacenMngForm
    {
        #region Factory Methods

        public AlmacenSelectForm()
            : this(null) {}

        public AlmacenSelectForm(Form parent)
            : this(parent, null) {}

        public AlmacenSelectForm(Form parent, StoreList lista)
            : base(true, parent, lista)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        public override void SelectAll()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

            List<StoreInfo> list = new List<StoreInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
                list.Add(row.DataBoundItem as StoreInfo);

            _selected = list;
            _action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion
    }
}
