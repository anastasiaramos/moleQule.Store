using System;
using System.Linq;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PayrollPaymentEditForm : PayrollPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "PayrollPaymentEditForm";
		public new static Type Type { get { return typeof(PayrollPaymentEditForm); } }

		#endregion
		
        #region Factory Methods

        public PayrollPaymentEditForm()
            : this(-1, ETipoPago.Todos, null) {}

        public PayrollPaymentEditForm(Form parent, IAcreedor acreedor, long oid, bool locked)
            : base(oid, new object[2] { ETipoPago.Nomina, acreedor }, true, parent)
        {
            InitializeComponent();

            _locked = locked;

            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public PayrollPaymentEditForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[2] { tipo, null }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null & _holder == null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoPago tipo = (ETipoPago)parameters[0];

            if (parameters[1] != null)
            {
                _holder = (IAcreedor)parameters[1];
                _holder.LoadChilds(typeof(Payment), true);
                _entity = _holder.Pagos.GetItem(oid);
            }
            else
                _entity = Payment.Get(oid, tipo);
            _entity.BeginEdit();

            if (_holder != null)
                _payrolls = PayrollList.GetByPagoAndPendientesList(_entity.GetInfo(false), (_holder as Employee).Oid, false);
            else
				_payrolls = PayrollList.GetByPagoAndPendientesList(_entity.GetInfo(false), false);
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            Payment_GB.Enabled = !new moleQule.Base.EEstado[] { moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Anulado }.Contains(_entity.EEstado);

            base.FormatControls();
        }

        #endregion

        #region Actions

		protected override void SaveAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                MessageBox.Show(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!ValidateDueDate()) return;

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
		}

        #endregion
    }
}