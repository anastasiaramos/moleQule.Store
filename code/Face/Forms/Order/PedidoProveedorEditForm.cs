using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorEditForm : PedidoProveedorUIForm
    {
        #region Attributes & Properties
		
        public const string ID = "PedidoProveedorEditForm";
		public static Type Type { get { return typeof(PedidoProveedorEditForm); } }

		#endregion
		
        #region Factory Methods

        public PedidoProveedorEditForm(long oid, ETipoAcreedor tipo)
            : this(oid, tipo, null) { }

        public PedidoProveedorEditForm(long oid, ETipoAcreedor tipo, Form parent)
			: base(oid, tipo, true, parent)
        {
            InitializeComponent();

            if (Entity != null)
            {
                SetFormData();
            }

            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];
            _entity = PedidoProveedor.Get(oid, tipo);
            _entity.BeginEdit();
        }

        #endregion
    }
}
