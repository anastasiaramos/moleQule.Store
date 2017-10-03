using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ProjectEditForm : ProjectUIForm
    {
        #region Properties

        protected override int BarSteps { get { return base.BarSteps + 5; } }

        #endregion

        #region Factory Methods

        public ProjectEditForm(long oid)
            : this(oid, null) {}
		
        public ProjectEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null)
            {
                SetFormData();
                this.Text = String.Format(Resources.Labels.PROJECT_TITLE, _entity.Codigo);
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
            PgMng.Grow(string.Empty, "Expedient");

            _entity.LoadChilds(typeof(Batch), true, true);
            PgMng.Grow(string.Empty, "Partidas");

            _entity.LoadChilds(typeof(Expense), true, true);
            PgMng.Grow(string.Empty, "Gastos");

            _entity.LoadChilds(typeof(Relation), true, true);
            PgMng.Grow(string.Empty, "Works");

            _entity.UpdateGastosPartidas(true);
            _entity.UpdateTotalesProductos(_entity.Partidas, true);
            PgMng.Grow(string.Empty, "Updates");         

            _entity.BeginEdit();
        }

        #endregion
    }
}

