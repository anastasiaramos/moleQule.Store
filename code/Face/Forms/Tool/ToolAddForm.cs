using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ToolAddForm : ToolUIForm
    {
        #region Factory Methods

		public ToolAddForm()
			: this(null) { }

		public ToolAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Tool.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}

