using System;
using System.Windows.Forms;

using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenEditForm : InventarioAlmacenUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "InventarioAlmacenEditForm";
		public new static Type Type { get { return typeof(InventarioAlmacenEditForm); } }

		#endregion
		
        #region Factory Methods

        public InventarioAlmacenEditForm(long oid)
            : this(oid, null) { }

        public InventarioAlmacenEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();

			if (_entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = InventarioAlmacen.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Layout & Source
        
        public override void FormatControls()
        {
            base.FormatControls();           
            Almacen_CB.Enabled = false;
        }
        
        #endregion
    }
}
