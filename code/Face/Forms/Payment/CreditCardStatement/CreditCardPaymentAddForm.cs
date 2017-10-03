using System;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class CreditCardPaymentAddForm : CreditCardPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CreditCardPaymentAddForm";
        public new static Type Type { get { return typeof(CreditCardPaymentAddForm); } }

		#endregion
		
        #region Factory Methods

        public CreditCardPaymentAddForm() 
			: this(ETipoPago.Todos, null) { }

        public CreditCardPaymentAddForm(ETipoPago tipo, Form parent)
            : this(new object[2] { null, tipo} , parent) {}

        public CreditCardPaymentAddForm(Payment entity, Form parent)
			: this(new object[1] { entity }, parent) {}

        public CreditCardPaymentAddForm(object[] parameters, Form parent)
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
                _entity.ETipoPago = ETipoPago.ExtractoTarjeta;
			}
			else
			{
                _entity = (Payment)parameters[0];
				_entity.BeginEdit();
				_entity.Oid = -1;
			}
        }

        #endregion

        #region Source

        protected override void LoadCreditCardStatements()
        {
            _statements = CreditCardStatementList.GetUnpaidList(_entity.OidTarjetaCredito, false);

            //Asociamos los gastos previamente vinculados
            foreach (TransactionPayment item in _entity.Operations)
            {
                CreditCardStatementInfo statement = _statements.GetItem(item.OidOperation);
                statement.Vincula();
            }

            base.LoadCreditCardStatements();
        }

        protected override void RefreshMainData()
        {
            base.RefreshMainData();

            SetPaymentMethodAction();
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

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
		}

        protected override void SetCreditCardAction()
        {
            if (_entity.Operations.Count > 0)
            {
                PgMng.ShowInfoException("No es posible cambiar la tarjeta de crédito de un pago con extractos vinculados");
                return;
            }

            base.SetCreditCardAction();
        }

        #endregion
    }
}