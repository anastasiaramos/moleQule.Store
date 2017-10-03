using System;
using System.Windows.Forms;
using System.Reflection;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class EmployeePaymentEditForm : EmployeePaymentUIForm
    {
        #region Factory Methods

        public EmployeePaymentEditForm()
            : this(null, -1, null) { }

        public EmployeePaymentEditForm(Form parent, long oidAgent, PaymentSummary summary)
			: base(parent, oidAgent, summary)
        {
            InitializeComponent();

            if (_entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.PAGO_EDIT_TITLE + " " + _entity.Nombre.ToUpper();
            }

            _mf_type = ManagerFormType.MFEdit;
        }

        public override void DisposeForm()
        {
            if (_entity != null) _entity.CloseSession();
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _summary = (PaymentSummary)parameters[0];

            switch (_summary.ETipoAcreedor)
            {
                case ETipoAcreedor.Empleado:
                    _entity = Employee.Get(_summary.OidAgente);
                    break;
            }

            _entity.CloseSessions = false;
            _entity.BeginEdit();
        }

        #endregion
    }
}