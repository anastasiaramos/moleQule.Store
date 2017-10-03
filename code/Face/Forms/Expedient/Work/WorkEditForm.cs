using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class WorkEditForm : WorkUIForm
    {
        #region Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        #endregion

        #region Factory Methods

        public WorkEditForm(long oid)
            : this(oid, null) {}
		
        public WorkEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null)
            {
                SetFormData();
				this.Text = String.Format(Resources.Labels.WORK_TITLE, _entity.Codigo);
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

            _entity.UpdateGastosPartidas(true);
            _entity.UpdateTotalesProductos(_entity.Partidas, true);
            PgMng.Grow(string.Empty, "Updates");

			_work_reports = WorkReportList.GetByExpedientList(_entity.Oid, false);

            _entity.BeginEdit();
        }

        #endregion
    }
}

