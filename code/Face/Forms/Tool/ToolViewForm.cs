using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ToolViewForm : ToolForm
	{
        #region Attributes & Properties

        private ToolInfo _entity;

        public override ToolInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods
		
		public ToolViewForm()
			: this(-1, null) { }

		public ToolViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = ToolInfo.Get(oid, true);
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
			base.FormatControls();
        }

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			PgMng.Grow();

			base.RefreshMainData();
            PgMng.Grow();
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
	}
}

