using System;
using System.Windows.Forms;

using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class AlmacenAddForm : AlmacenUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "AlmacenAddForm";
		public new static Type Type { get { return typeof(AlmacenAddForm); } }

		#endregion
		
        #region Factory Methods

        public AlmacenAddForm() : this(true) {}

        public AlmacenAddForm(bool isModal)
            : base(isModal)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.ALMACEN_ADD_TITLE;
            _mf_type = ManagerFormType.MFAdd;
        }

        public AlmacenAddForm(Almacen source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            this.Text = Resources.Labels.ALMACEN_ADD_TITLE;
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = Almacen.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Buttons

        #endregion
    }
}
