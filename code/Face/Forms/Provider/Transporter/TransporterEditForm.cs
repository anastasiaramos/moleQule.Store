using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class TransporterEditForm : TransporterUIForm
    {
        #region Factory Methods

		public TransporterEditForm()
			: this(-1, ETipoAcreedor.Todos, null) { }

        public TransporterEditForm(long oid, ETipoAcreedor providerType, Form parent)
            : base(oid, providerType, parent)
		{
			InitializeComponent();
            if (Entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
            SetFormData();
        }

        public TransporterEditForm(IAcreedor item, Form parent)
            : base(item, parent)
        {
            InitializeComponent();
            if (Entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
            SetFormData();
        }

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
            _entity = (Transporter)parameters[1];

            if (_entity == null)
            {
                _entity = Transporter.Get(oid, (ETipoAcreedor)parameters[0]);
                _entity.BeginEdit();
            }
		}

        #endregion

		#region Buttons

		#endregion 
    }
}

