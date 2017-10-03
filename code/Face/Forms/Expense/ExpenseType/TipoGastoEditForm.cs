using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TipoGastoEditForm : TipoGastoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TipoGastoEditForm";
		public new static Type Type { get { return typeof(TipoGastoEditForm); } }

		#endregion
		
        #region Factory Methods

        public TipoGastoEditForm(long oid)
            : this(oid, null) {}

        public TipoGastoEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = TipoGasto.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

    }
}
