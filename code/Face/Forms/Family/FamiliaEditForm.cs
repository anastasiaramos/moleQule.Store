using System;
using System.Windows.Forms;

using moleQule.Library.Store;
using moleQule;
using moleQule.Face;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamiliaEditForm : FamiliaUIForm
    {
        #region Factory Methods

		public FamiliaEditForm()
			: this(-1, null) {}

		public FamiliaEditForm(long oid, Form parent)
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
			_entity = Familia.Get(oid);
			_entity.BeginEdit();
		}

        #endregion
    }
}

