using System;
using System.Linq;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LoanPaymentEditForm : LoanPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "LoanPaymentEditForm";
		public new static Type Type { get { return typeof(LoanPaymentEditForm); } }
        
		#endregion
		
        #region Factory Methods

        public LoanPaymentEditForm()
            : this(-1, ETipoPago.Prestamo, null) {}

        public LoanPaymentEditForm(long oid, ETipoPago tipo, Form parent)
            : this(oid, tipo, null, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public LoanPaymentEditForm(long oid, ETipoPago tipo, Loan source, Form parent)
            : base(oid, new object[2] { tipo, source }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public LoanPaymentEditForm(Payment payment, ETipoPago tipo, Loan source, Form parent)
            : base(-1, new object[3] { tipo, source, payment }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            try
            {
                ETipoPago tipo = ETipoPago.Prestamo;

                if (parameters[1] != null)
                    _loan = (Loan)parameters[1];

                if (parameters.Length >= 3)
                    _entity = (Payment)parameters[2];
                else
                {
                    _entity = Payment.Get(oid, tipo, true);
                    _entity.BeginEdit();
                }

                _loans = _loan != null ?
                        LoanList.NewList(_loan)
                        : LoanList.GetByPagoAndPendientesList(_entity.GetInfo(false));

                if (_loans.Count > 0)
                {
                    PaymentList pagos = PaymentList.GetListByPrestamo(_loans[0], false);

                    TransactionPayment pf = _entity.Operations.GetItemByFactura(_loans[0].Oid);

                    if (pf != null)
                    {
                        _loans[0].Asignado = pf.Cantidad;
                        _loans[0].TotalPagado = 0;

                        foreach (PaymentInfo pago in pagos)
                        {
                            if (pago.EEstado == moleQule.Base.EEstado.Anulado) continue;

                            _loans[0].TotalPagado += pago.Importe;
                        }
                    }

                    _loans[0].Pendiente = _loans[0].Importe - _loans[0].TotalPagado + _loans[0].Asignado;
                    _loans[0].PendienteAsignar = _loans[0].Pendiente - _loans[0].Asignado;
                }
            }
            catch (Exception ex)
            {
                if (_entity != null) _entity.CloseSession();
                throw ex;
            }
        }
        protected override void GetFormSourceData(object[] parameters)
        {
            _loan = (Loan)parameters[1];
            _entity = (Payment)parameters[2];
            _loans = LoanList.NewList(_loan);

            PaymentList pagos = PaymentList.GetListByPrestamo(_loans[0], false);

            TransactionPayment pf = _entity.Operations.GetItemByFactura(_loans[0].Oid);

            if (pf != null)
            {
                _loans[0].Asignado = pf.Cantidad;
                _loans[0].TotalPagado = 0;

                foreach (PaymentInfo pago in pagos)
                {
                    if (pago.EEstado == moleQule.Base.EEstado.Anulado) continue;

                    _loans[0].TotalPagado += pago.Importe;
                }
            }

            _loans[0].Pendiente = _loans[0].Importe - _loans[0].TotalPagado + _loans[0].Asignado;
            _loans[0].PendienteAsignar = _loans[0].Pendiente - _loans[0].Asignado;
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