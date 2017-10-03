using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ProductAddForm : ProductUIForm
    {
        #region Factory Methods

        public ProductAddForm(Form parent) 
			: base(parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

        public ProductAddForm(Product source, Form parent) 
			: base(parent)
		{
			InitializeComponent();
			_entity = source.Clone();
			_entity.BeginEdit();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Product.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}

