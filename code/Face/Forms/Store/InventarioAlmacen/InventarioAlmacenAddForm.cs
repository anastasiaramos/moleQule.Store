using System;
using System.Windows.Forms;

using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenAddForm : InventarioAlmacenUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "InventarioAlmacenAddForm";
		public new static Type Type { get { return typeof(InventarioAlmacenAddForm); } }

		#endregion
		
        #region Factory Methods

        public InventarioAlmacenAddForm() 
			: this(null) {}

        public InventarioAlmacenAddForm(Form parent)
            : base(-1, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public InventarioAlmacenAddForm(InventarioAlmacen source, Form parent)
            : base(-1, parent)
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = InventarioAlmacen.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Buttons

        private void CrearInventario_B_Click(object sender, EventArgs e)
        {
            //Entity.CreateInventario(EntityInfo.OidAlmacen);
        }

        #endregion     
    }
}
