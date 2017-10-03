using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamiliaAddForm : FamiliaUIForm
    {
        #region Factory Methods

		public FamiliaAddForm()
			: this(null) { }

		public FamiliaAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Familia.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}