using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class TipoGastoViewForm : TipoGastoForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TipoGastoViewForm";
		public new static Type Type { get { return typeof(TipoGastoViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private TipoGastoInfo _entity;

        public override TipoGastoInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public TipoGastoViewForm(long oid) 
			: this(oid, null) {}

        public TipoGastoViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = ": " + EntityInfo.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = TipoGastoInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();						
			
            base.RefreshMainData();
        }
		
        #endregion

        #region Actions

        protected override void SaveAction()
        {
			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        #endregion
    }
}
