using System;
using System.Windows.Forms;

using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class AlmacenEditForm : AlmacenUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "AlmacenEditForm";
		public new static Type Type { get { return typeof(AlmacenEditForm); } }

		#endregion
		
        #region Factory Methods

        public AlmacenEditForm(long oid)
            : this(oid, true) { }

        public AlmacenEditForm(long oid, bool ismodal)
            : base(oid, ismodal)
        {
            InitializeComponent();
            if (Entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.ALMACEN_EDIT_TITLE + " " + Entity.Nombre.ToUpper();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Almacen.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Buttons

        #endregion

	}
}
