using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ProveedorEditForm : SupplierUIForm
    {
        #region Factory Methods

		public ProveedorEditForm(long oid, ETipoAcreedor providerType)
            : this(oid, providerType, null) { }

        public ProveedorEditForm(long oid, ETipoAcreedor providerType, Form parent)
            : base(oid, providerType, parent)
        {
            InitializeComponent();
            if (_entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

        public ProveedorEditForm(IAcreedor item, Form parent)
            : base(item, parent)
        {
            InitializeComponent();
            if (_entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			_entity = (Proveedor)parameters[1];

            if (_entity == null)
            {
				_entity = Proveedor.Get(oid, (ETipoAcreedor)parameters[0]);
                _entity.BeginEdit();
            }
        }

        #endregion
    }
}