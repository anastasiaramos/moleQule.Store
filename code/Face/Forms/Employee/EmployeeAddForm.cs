using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store; 

namespace moleQule.Face.Store
{
    public partial class EmployeeAddForm : EmployeeUIForm
    {
        #region Factory Methods

		public EmployeeAddForm(Form parent)
			: base(-1, parent) 
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

		public EmployeeAddForm(IAcreedor entity, Form parent) 
			: base(entity, parent) 
        {
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
        {
			if (parameters[0] == null)
			{
                _entity = Employee.New();
				_entity.BeginEdit();
			}
			else
			{
                _entity = (Employee)parameters[0];
				_entity.BeginEdit();
			}

            Text = Resources.Labels.EMPLOYEE_ADD_TITLE;
        }
      
        #endregion
    }
}