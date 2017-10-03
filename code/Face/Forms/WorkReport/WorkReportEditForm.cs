using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class WorkReportEditForm : WorkReportUIForm
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		#endregion

		#region Factory Methods

		public WorkReportEditForm(long oid, Form parent)
			: this(oid, new object[1] { null }, parent) {}

		public WorkReportEditForm(WorkReport entity, Form parent)
			: this(-1, new object[1] { entity }, parent) {}

		public WorkReportEditForm(long oid, object[] parameters, Form parent)
			: base(oid, parameters, true, parent)
		{
			InitializeComponent();
			if (_entity != null) SetFormData();
            this.Text = String.Format(Library.Store.Resources.Labels.WORK_REPORT_TITLE, _entity.Code);
			_mf_type = ManagerFormType.MFEdit;
		}

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			_entity = WorkReport.Get(oid, true);
			_entity.BeginEdit();
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = (WorkReport)parameters[0];
			_entity.BeginEdit();
		}

        #endregion
    }
}

