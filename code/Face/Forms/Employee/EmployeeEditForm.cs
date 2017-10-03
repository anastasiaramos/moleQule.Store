using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store; 

namespace moleQule.Face.Store
{
    public partial class EmployeeEditForm : EmployeeUIForm
    {
        #region Factory Methods

		public EmployeeEditForm()
			: this(-1, null) {}

		public EmployeeEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();
            this.Text = string.Format(Resources.Labels.EMPLOYEE_TITLE, _entity.NombreCompleto);
			_mf_type = ManagerFormType.MFEdit;
        }

        public EmployeeEditForm(IAcreedor item, Form parent) 
			: base(item, parent)
		{
			InitializeComponent();
			SetFormData();
            this.Text = string.Format(Resources.Labels.EMPLOYEE_TITLE, _entity.NombreCompleto);
			_mf_type = ManagerFormType.MFEdit;
		}

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = (Employee)parameters[0];

            if (_entity == null)
            {
                _entity = Employee.Get(oid);
                _entity.BeginEdit();                
            }
            _mf_type = ManagerFormType.MFEdit;
        }

        #endregion
	}
}

