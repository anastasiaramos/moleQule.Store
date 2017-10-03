using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class LineaFomentoForm : Skin01.ItemMngSkinForm
    {

        #region Attributes & Properties
		
        public const string ID = "LineaFomentoForm";
		public static Type Type { get { return typeof(LineaFomentoForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual LineaFomento Entity { get { return null; } set { } }
        public virtual LineaFomentoInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public LineaFomentoForm() 
			: this(-1) {}

        public LineaFomentoForm(long oid) 
			: this(oid, true, null) {}

		public LineaFomentoForm(bool is_modal) 
		: this(-1, is_modal, null) {}

        public LineaFomentoForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Style

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
        }

		#endregion
		
		#region Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
			
        }

        /// <summary>
        /// Asigna los datos a los origenes de datos secundarios
        /// </summary>
        public override void RefreshSecondaryData()
		{
			
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    LineaFomentoReportMng reportMng = new LineaFomentoReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }

		
        #endregion
    }
}
