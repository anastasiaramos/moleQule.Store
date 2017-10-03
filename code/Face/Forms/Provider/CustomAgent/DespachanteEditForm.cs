using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class DespachanteEditForm : CustomAgentUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "DespachanteEditForm";
		public new static Type Type { get { return typeof(DespachanteEditForm); } }

		#endregion
		
        #region Factory Methods

        public DespachanteEditForm(long oid)
            : this(oid, null) { }

        public DespachanteEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (Entity != null)
                SetFormData();

			_mf_type = ManagerFormType.MFEdit;
        }

        public DespachanteEditForm(IAcreedor item, Form parent)
            : base(item, parent)
        {
            InitializeComponent();
            if (Entity != null)
                SetFormData();

            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid, object[]parameters)
        {
            _entity = (Despachante)parameters[0];

            if (_entity == null)
            {
                _entity = Despachante.Get(oid);
                _entity.BeginEdit();
            }
        }

        #endregion

        #region Buttons

        #endregion

	}
}
