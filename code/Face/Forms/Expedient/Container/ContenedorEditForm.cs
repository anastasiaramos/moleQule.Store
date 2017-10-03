using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule.Face;
using moleQule.Library.Store;
using moleQule;

namespace moleQule.Face.Store
{
    public partial class ContenedorEditForm : ContenedorUIForm
    {
        #region Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        #endregion

        #region Factory Methods

        public ContenedorEditForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo)
            : this(oid, tipo, null) {}
		
		public ContenedorEditForm(long oid, Form parent)
			: this(oid, moleQule.Store.Structs.ETipoExpediente.Todos, parent) { }

        public ContenedorEditForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo, Form parent)
            : base(oid, tipo, parent)
        {
            InitializeComponent();
            if (_entity != null)
            {
                SetFormData();

                switch (tipo)
                {
                    case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
                        this.Text = String.Format(Resources.Labels.CONTAINER_TITLE, _entity.Codigo);
                        break;

                    default:
                        this.Text = String.Format(Resources.Labels.EXPEDIENT_TITLE, _entity.Codigo);
                        break;
                }
            }

            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = Expedient.Get(oid, false);
            PgMng.Grow(string.Empty, "Expediente");

            _entity.LoadChilds(typeof(Batch), true, true);
            PgMng.Grow(string.Empty, "Partidas");

            try
            {
                _entity.LoadChilds(typeof(Expense), true, true);
                PgMng.Grow(string.Empty, "Gastos");
            }
            catch
            {
                PgMng.ShowWarningException(Resources.Messages.DUPLICATED_EXPENSE_LINE);
            }

            _entity.UpdateTotalesProductos(_entity.Partidas, true);
            PgMng.Grow(string.Empty, "Updates");

            _entity.BeginEdit();
        }

        #endregion
    }
}

