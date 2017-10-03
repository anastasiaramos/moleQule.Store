using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class LineaFomentoViewForm : LineaFomentoForm
    {

        #region Attributes & Properties
		
        public new const string ID = "LineaFomentoViewForm";
		public new static Type Type { get { return typeof(LineaFomentoViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private LineaFomentoInfo _entity;

        public override LineaFomentoInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public LineaFomentoViewForm(long oid) 
			: this(oid, null) {}

        public LineaFomentoViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = LineaFomentoInfo.Get(oid);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		#endregion
		
		#region Source
		
        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			
			
            base.RefreshMainData();
        }
		
        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }
}
