using System;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LoanPaymentAddForm : LoanPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "LoanPaymentAddForm";
		public new static Type Type { get { return typeof(LoanPaymentAddForm); } }

		#endregion
		
        #region Factory Methods

        public LoanPaymentAddForm()
            : this(ETipoPago.Prestamo, null) { }

        public LoanPaymentAddForm(ETipoPago tipo, Form parent)
            : this(ETipoPago.Prestamo, null, null) { }

        public LoanPaymentAddForm(ETipoPago tipo, Loan source, Form parent)
            : this(new object[3] { null, tipo, source} , parent) {}

        public LoanPaymentAddForm(Payment entity, Form parent)
			: this(new object[1] { entity }, parent) {}

        public LoanPaymentAddForm(object[] parameters, Form parent)
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
                _loan = (Loan)parameters[2];
                _entity = Payment.New(_loan.GetInfo(false));
				_entity.BeginEdit();
                _entity.Oid = -1;
                _loans = LoanList.NewList(_loan);

                PaymentList pagos = PaymentList.GetListByPrestamo(_loans[0], false);

                _loans[0].TotalPagado = 0;
                _loans[0].Asignado = 0;

                foreach (PaymentInfo pago in pagos)
                {
                    if (pago.EEstado == moleQule.Base.EEstado.Anulado) continue;

                    _loans[0].TotalPagado += pago.Importe;
                }

                _loans[0].Pendiente = _loans[0].Importe - _loans[0].TotalPagado;
                _loans[0].PendienteAsignar = _loans[0].Pendiente;

			}
			else
			{
                _entity = (Payment)parameters[0];
				_entity.BeginEdit();
                _entity.Oid = -1;
                _loans = LoanList.GetPendientesList(_entity.GetInfo(false));
			}


			//Asociamos los gastos previamente vinculados
            foreach (TransactionPayment item in _entity.Operations)
			{
                LoanInfo prestamo = _loans.GetItem(item.OidOperation);
				prestamo.Vincula();
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

        protected override void SetParametrosPagoComercioExteriorAction()
        {
            if (_loan == null) return;

            BankAccountInfo cuenta = BankAccountInfo.Get(_loan.OidCuenta, false);

            if (cuenta.ETipoCuenta != ETipoCuenta.CuentaCorriente)
            {
                _entity.OidCuentaBancaria = cuenta.OidCuentaAsociada;
                _entity.CuentaBancaria = cuenta.CuentaAsociada;
            }
            else
            {
                _entity.OidCuentaBancaria = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
            }

            _entity.EMedioPago = EMedioPago.Transferencia;
            _entity.EEstadoPago = moleQule.Base.EEstado.Pagado;

            base.SetParametrosPagoComercioExteriorAction();
            Cuenta_TB.Text = cuenta.Valor;
        }

        #endregion
    }
}