using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class EscandalloAddForm : EscandalloUIForm
    {
        #region Factory Methods

		public EscandalloAddForm()
			: this(null) { }		

		public EscandalloAddForm(Form parent)
			: base(-1, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Product.NewElaborado();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	

        #region Events

        #endregion
    }
}

