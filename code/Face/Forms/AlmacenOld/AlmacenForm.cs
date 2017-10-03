using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Library;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class AlmacenForm : Skin01.ItemMngSkinForm
    {

        #region Attributes & Properties
		
        public const string ID = "AlmacenForm";
		public static Type Type { get { return typeof(AlmacenForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual Almacen Entity { get { return null; } set { } }
        public virtual AlmacenInfo EntityInfo { get { return null; } }
              
        #endregion

        #region Factory Methods

        public AlmacenForm() : this(-1) {}

        public AlmacenForm(long oid) : this(oid, true, null) {}

		public AlmacenForm(bool isModal) : this(-1, isModal, null) {}

        public AlmacenForm(long oid, bool isModal) : this(oid, isModal, null) {}

        public AlmacenForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Style & Source

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.MaximizeForm(new Size(this.Width,0));
            
			base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 1;

            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(LineaAlmacenes_Grid, cols);
            ControlsMng.MarkGridColumn(LineaAlmacenes_Grid, ControlsMng.GetCurrentColumn(LineaAlmacenes_Grid));

            List<DataGridViewColumn> colsInventario = new List<DataGridViewColumn>();
            ObservacionesInventario.Tag = 1;

            colsInventario.Add(ObservacionesInventario);

            ControlsMng.MaximizeColumns(InventarioAlmacenes_Grid, colsInventario);
            ControlsMng.MarkGridColumn(InventarioAlmacenes_Grid, ControlsMng.GetCurrentColumn(InventarioAlmacenes_Grid));
        }

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
        //    AlmacenReportMng reportMng = new AlmacenReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Buttons

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }
		
		private void InventarioAlmacenes_Grid_DataError(object sender, DataGridViewDataErrorEventArgs e) {}

        private void InventarioAlmacenes_Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetUnlinkedGridValues(InventarioAlmacenes_Grid.Name);
        }
		
		private void LineaAlmacenes_Grid_DataError(object sender, DataGridViewDataErrorEventArgs e) {}

        private void LineaAlmacenes_Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetUnlinkedGridValues(LineaAlmacenes_Grid.Name);
        }
		
        #endregion

        #region Actions

        protected virtual void InventarioAlmacenes_Grid_DoubleClick() { }
        /// <summary>
        /// Abre el formulario para editar item
        /// <returns>void</returns>
        /// </summary>
        private void InventarioAlmacenes_Grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                InventarioAlmacenes_Grid_DoubleClick();
            }
            catch (Csla.DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }        
        }
        #endregion

    }
}
