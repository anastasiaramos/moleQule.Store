using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InputDeliverySelectForm : InputDeliveryMngForm
    {
        #region Factory Methods

        public InputDeliverySelectForm()
            : this(null) {}

        public InputDeliverySelectForm(Form parent)
            : this(parent, ETipoAlbaranes.Todos, ETipoAcreedor.Todos, 0, 0) {}

        public InputDeliverySelectForm(Form parent, ETipoAlbaranes tipo, ETipoAcreedor tipo_acreedor, long oid_serie, long oid_cliente)
            : base(true, parent, tipo, tipo_acreedor)
        {
            InitializeComponent();
			_view_mode = molView.Select;

            _oid_cliente = oid_cliente;
            _oid_serie = oid_serie;

			_action_result = DialogResult.Cancel;		
        }

        public InputDeliverySelectForm(Form parent, InputDeliveryList lista)
            : base(true, parent, ETipoAlbaranes.Todos, ETipoAcreedor.Todos, lista)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Actions

        protected override void DefaultAction() 
        {
			SelectAction();
        }

        public override void SelectAll()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

            List<InputDeliveryInfo> list = new List<InputDeliveryInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
                list.Add(row.DataBoundItem as InputDeliveryInfo);

            _selected = list;
            _action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion
    }
}
