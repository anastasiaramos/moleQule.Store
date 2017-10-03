using System;
using System.Collections.Generic;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class WorkReportAddForm : WorkReportUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        #endregion

        #region Factory Methods

		public WorkReportAddForm(Form parent)
			: this((WorkReport)null, parent) { }

		public WorkReportAddForm(WorkReport entity, Form parent)
			: this(new object [1] { entity }, parent) {}

		public WorkReportAddForm(Expedient source, Form parent)
			: this(new object[2] { null, source }, parent) { }

		public WorkReportAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
            this.Text = Library.Store.Resources.Labels.WORK_REPORT_ADD_TITLE;
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			if (parameters[0] == null)
			{
				_entity = WorkReport.New();
				_entity.BeginEdit();

				if (parameters.Length >= 2)
				{
					Expedient expedient = (Expedient)parameters[1];
					_entity.OidExpedient = expedient.Oid;
				}
			}
			else
			{
				_entity = (WorkReport)parameters[0];
				_entity.BeginEdit();
			}
		}

		#endregion
    }
}

