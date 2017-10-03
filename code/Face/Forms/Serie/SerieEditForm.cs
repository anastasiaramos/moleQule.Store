using System;
using System.Windows.Forms;

using moleQule.Library.Store;
using moleQule;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class SerieEditForm : SerieUIForm
    {
        #region Factory Methods

		public SerieEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null)
            {
                SetFormData();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = moleQule.Serie.Serie.Get(oid);
			_entity.BeginEdit();
		}

        #endregion

		#region Buttons

     	#endregion 
    }
}

