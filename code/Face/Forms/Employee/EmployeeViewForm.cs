using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class EmployeeViewForm : EmployeeForm
    {
        #region Business Methods

        private EmployeeInfo _entity;

        public override EmployeeInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public EmployeeViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            SetFormData();
			_mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = EmployeeInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

		public override void FormatControls()
		{
			SetReadOnlyControls(this.Controls);
			base.FormatControls();
		}

		protected override void RefreshMainData()
		{
			Datos.DataSource = _entity;
			PgMng.Grow();

			Products_BS.DataSource = _entity.Productos;

			base.RefreshMainData();
		}

		protected override void SetDependentControlSource(Control control)
		{
			if (control.Name == Perfil_GB.Name)
			{
				Gerente_CB.Checked = (_entity.Perfil >> 8) % 2 == 1;
				Administrador_CB.Checked = (_entity.Perfil >> 9) % 2 == 1;
				Empleado_CB.Checked = (_entity.Perfil >> 10) % 2 == 1;
			}
		}
	
        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        protected override void EditWorkReportAction()
        {
            if (WorkReport_DGW.CurrentRow == null) return;
            if (WorkReport_DGW.CurrentRow.Index < 0) return;
            if (WorkReport_DGW.CurrentRow.DataBoundItem == null) return;

            WorkReportResourceInfo item = (WorkReportResourceInfo)WorkReport_DGW.CurrentRow.DataBoundItem;

            WorkReportViewForm form = new WorkReportViewForm(item.OidWorkReport, this);
            form.ShowDialog(this);
        }

        #endregion    
    }
}
