using System;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PayrollPaymentAddForm : PayrollPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "PayrollPaymentAddForm";
		public new static Type Type { get { return typeof(PayrollPaymentAddForm); } }

		#endregion
		
        #region Factory Methods

        public PayrollPaymentAddForm() 
			: this(ETipoPago.Todos, null) { }

        public PayrollPaymentAddForm(ETipoPago tipo, Form parent)
            : this(new object[3] { null, tipo, null} , parent) {}

        public PayrollPaymentAddForm(Payment entity, Form parent)
			: this(new object[3] { entity, null, null }, parent) {}

        public PayrollPaymentAddForm(Form parent, IAcreedor acreedor)
            : this(new object[3] { null, ETipoPago.Nomina, acreedor }, parent) { }

		public PayrollPaymentAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();            
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

        protected override void GetFormSourceData(object[] parameters)
        {
			if (parameters[0] == null)
            {
                if (parameters[2] != null)
                {
                    _holder = (IAcreedor)parameters[2];
                    _holder.LoadChilds(typeof(Payment), true);
                    _entity = _holder.Pagos.NewItem(_holder, ETipoPago.Nomina);
                    _entity.CopyFrom(_holder, ETipoPago.Nomina);
                }
                else
                    _entity = Payment.New((ETipoPago)parameters[1]);

                _entity.ETipoAcreedor = ETipoAcreedor.Empleado;
				_entity.BeginEdit();
				_entity.Oid = -1;
			}
			else
			{
                _entity = (Payment)parameters[0];
				_entity.BeginEdit();
				_entity.Oid = -1;
			}

            if (_holder != null)
                _payrolls = PayrollList.GetPendientesList((_holder as Employee).Oid, false);
            else
				_payrolls = PayrollList.GetPendientesList(_entity.GetInfo(false), false);

			//Asociamos los gastos previamente vinculados
            foreach (TransactionPayment item in _entity.Operations)
			{
                NominaInfo gasto = _payrolls.GetItem(item.OidOperation);
				gasto.Vincula();
			}
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

            if (!ValidateAllocation()) return;
            if (!ValidateDueDate()) return;

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
		}

        #endregion
    }
}