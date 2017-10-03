using System;
using System.Linq;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpensePaymentEditForm : ExpensePaymentUIForm
    {
        #region Attributes & Properties

        public new const string ID = "ExpensePaymentEditForm";
		public new static Type Type { get { return typeof(ExpensePaymentEditForm); } }

		#endregion
		
        #region Factory Methods

        public ExpensePaymentEditForm()
            : this(-1, ETipoPago.Todos, null) {}

        public ExpensePaymentEditForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[3] { tipo, null, null }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public ExpensePaymentEditForm(PaymentInfo root, Payment item, Form parent)
            : base(item.Oid, new object[3] { ETipoPago.Gasto, root, item }, true, parent)
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

                _expenses = ExpenseList.GetByPagoAndPendientesList(moleQule.Store.Structs.EnumConvert.ToECategoriaGasto(tipo), info, false);
            }
            else
            {
                _entity = Payment.Get(oid, tipo);
                _entity.BeginEdit();
                _entity.BeginTransaction();

                _expenses = ExpenseList.GetByPagoAndPendientesList(moleQule.Store.Structs.EnumConvert.ToECategoriaGasto(tipo), _entity.GetInfo(true), false);
            }

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
                PgMng.ShowInfoException(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!ValidateDueDate()) return;

			_action_result = _root == null ? (SaveObject() ? DialogResult.OK : DialogResult.Ignore) : DialogResult.OK;
		}

        #endregion
    }
}