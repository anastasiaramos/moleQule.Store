using System;
using System.Linq;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class CreditCardPaymentEditForm : CreditCardPaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CreditCardPaymentEditForm";
        public new static Type Type { get { return typeof(CreditCardPaymentEditForm); } }

		#endregion
		
        #region Factory Methods

        public CreditCardPaymentEditForm()
            : this(-1, ETipoPago.Todos, null) {}

        public CreditCardPaymentEditForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[3] { tipo, null, null }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public CreditCardPaymentEditForm(PaymentInfo root, Payment item, Form parent)
            : base(item.Oid, new object[3] { ETipoPago.ExtractoTarjeta, root, item }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null && _root == null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(object[] parameters)
        {
            GetFormSourceData(-1, parameters);
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoPago tipo = (ETipoPago)parameters[0];

            if (parameters[1] != null)
            {
                _root = (PaymentInfo)parameters[1];
                _entity = (Payment)parameters[2];

                PaymentInfo info = _entity.GetInfo(true);
                info.Oid = -1;

                _statements = CreditCardStatementList.GetByPaymentAndUnpaidList(info.Oid, info.OidTarjetaCredito, false);
            }
            else
            {
                _entity = Payment.Get(oid, tipo);
                _entity.BeginEdit();
                _entity.BeginTransaction();

                _statements = CreditCardStatementList.GetByPaymentAndUnpaidList(_entity.Oid, _entity.OidTarjetaCredito, false);
            }
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            Payment_GB.Enabled = !new moleQule.Base.EEstado[] { moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Anulado }.Contains(_entity.EEstado); 

            base.FormatControls();

            Tarjeta_BT.Enabled = false;
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                PgMng.ShowInfoException(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

			_action_result = _root == null ? (SaveObject() ? DialogResult.OK : DialogResult.Ignore) : DialogResult.OK;
		}

        protected override void SetCreditCardAction()
        {
        }

        #endregion
    }
}