using System;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpensePaymentAddForm : ExpensePaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "ExpensePayment";
		public new static Type Type { get { return typeof(ExpensePaymentAddForm); } }

		#endregion
		
        #region Factory Methods

        public ExpensePaymentAddForm() 
			: this(ETipoPago.Todos, null) { }

        public ExpensePaymentAddForm(ETipoPago tipo, Form parent)
            : this(new object[2] { null, tipo} , parent) {}

        public ExpensePaymentAddForm(Payment entity, Form parent)
			: this(new object[1] { entity }, parent) {}

		public ExpensePaymentAddForm(object[] parameters, Form parent)
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
                _entity = Payment.New((ETipoPago)parameters[1]);
				_entity.BeginEdit();
				_entity.Oid = -1;
			}
			else
			{
                _entity = (Payment)parameters[0];
				_entity.BeginEdit();
				_entity.Oid = -1;
			}

			_expenses = ExpenseList.GetPendientesList(moleQule.Store.Structs.EnumConvert.ToECategoriaGasto(_entity.ETipoPago), _entity.GetInfo(false), false);

			//Asociamos los gastos previamente vinculados
            foreach (TransactionPayment item in _entity.Operations)
			{
                ExpenseInfo gasto = _expenses.GetItem(item.OidOperation);
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