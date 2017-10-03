using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class NavieraEditForm : ShippingCompanyUIForm
    {
        #region Factory Methods

		public NavieraEditForm() 
			: this(-1, null) {}

        public NavieraEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (Entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

        public NavieraEditForm(IAcreedor item, Form parent)
            : base(item, parent)
        {
            InitializeComponent();
            if (Entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
            _entity = (Naviera)parameters[0];

            if (_entity == null)
            {
                _entity = Naviera.Get(oid);
                _entity.BeginEdit();
            }
		}

        #endregion

		#region Buttons

		#endregion 
    }
}

