using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ProductEditForm : ProductUIForm
    {
        #region Factory Methods

        public ProductEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
			
			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = Product.Get(oid, false);
			_entity.BeginEdit();

			if (_entity.IsKit) _entity.LoadChilds(typeof(Kit), false);
		}

        #endregion

		#region Buttons

		#endregion 
    }
}

