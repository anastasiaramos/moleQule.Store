using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class NavieraAddForm : ShippingCompanyUIForm
    {
        #region Factory Methods

		public NavieraAddForm() 
			:this(null) { }

        public NavieraAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = Naviera.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}

