using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamiliaViewForm : FamiliaForm
	{
        #region Attributes & Properties

        private FamiliaInfo _entity;

        public override FamiliaInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods
		
		public FamiliaViewForm()
			: this(-1, null) { }

        public FamiliaViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = FamiliaInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
			base.FormatControls();
        }

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

