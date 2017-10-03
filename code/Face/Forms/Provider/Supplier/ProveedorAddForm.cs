using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ProveedorAddForm : SupplierUIForm
    {
        #region Factory Methods

        public ProveedorAddForm() 
			: this((Form)null) {}

        public ProveedorAddForm(Form parent)
            : base(-1, ETipoAcreedor.Proveedor, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public ProveedorAddForm(Proveedor source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData(object[] parameters)
        {
			_entity = Proveedor.New();
            _entity.BeginEdit();
        }

        #endregion
	}
}

