using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PagoFraccionadoEditForm : PagoFraccionadoUIForm
    {
        #region Attributes & Properties

        public new const string ID = "PagoFraccionadoEditForm";
        public new static Type Type { get { return typeof(PagoFraccionadoEditForm); } }

        #endregion

        #region Factory Methods

        public PagoFraccionadoEditForm()
            : this(-1, ETipoPago.Todos, null) { }

        public PagoFraccionadoEditForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[1] { tipo }, true, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        public override void DisposeForm()
        {
            if (_entity != null) _entity.CloseSession();
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            ETipoPago tipo = (ETipoPago)parameters[0];

            _entity = Payment.Get(oid, tipo);
            _entity.BeginEdit();

            _gastos = ExpenseList.GetByPagoAndPendientesList(moleQule.Store.Structs.EnumConvert.ToECategoriaGasto(tipo), _entity.GetInfo(false), false);
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
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion
    }
}
