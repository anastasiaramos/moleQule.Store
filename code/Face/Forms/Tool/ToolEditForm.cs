using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;
using moleQule;

namespace moleQule.Face.Store
{
	public partial class ToolEditForm : ToolUIForm
    {
        #region Factory Methods

		public ToolEditForm()
			: this(-1, null) {}

		public ToolEditForm(long oid, Form parent)
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
			_entity = Tool.Get(oid);
			_entity.BeginEdit();
		}

        #endregion
    }
}

