using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule.Face;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ProjectAddForm : ProjectUIForm
    {
        #region Factory Methods

        public ProjectAddForm()
            : this(null) {}

        public ProjectAddForm(Form parent)
            : base(-1, parent)
        {
            InitializeComponent();

            SetFormData();

            _mf_type = ManagerFormType.MFAdd;

            this.Text = Resources.Labels.PROJECT_ADD_TITLE;
        }

		protected override void GetFormSourceData(object[] parameters)
		{
			moleQule.Store.Structs.ETipoExpediente tipo = (moleQule.Store.Structs.ETipoExpediente)parameters[0];

            _entity = Expedient.New(tipo);
            _entity.BeginEdit();

			_work_reports = WorkReportList.NewList();
		}

		#endregion

		#region Business Methods

		protected override void LoadStock() { }

		#endregion
	}
}

