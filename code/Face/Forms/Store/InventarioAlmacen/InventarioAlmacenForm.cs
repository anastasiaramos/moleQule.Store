using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "InventarioAlmacenForm";
		public static Type Type { get { return typeof(InventarioAlmacenForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }
		
        public virtual InventarioAlmacen Entity { get { return null; } set { } }
        public virtual InventarioAlmacenInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public InventarioAlmacenForm()
			: this(-1) {}

        public InventarioAlmacenForm(long oid) 
			: this(oid, true, null) {}

        public InventarioAlmacenForm(long oid, Form parent) 
			: this(oid, true, parent) { }
        
		public InventarioAlmacenForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout & Source

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {           
            base.MaximizeForm(new Size(this.Width, 0));
            
			base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 1;

            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(LineaInventario_Grid, cols);
            ControlsMng.MarkGridColumn(LineaInventario_Grid, ControlsMng.GetCurrentColumn(LineaInventario_Grid));
        }

        /// <summary>
        /// Asigna los datos a los origenes de datos secundarios
        /// </summary>
        public override void RefreshSecondaryData()
		{
            Datos_Almacen.DataSource = StoreList.GetList(false);
            Bar.Grow(string.Empty, "Almacenes");

            //Almacen_CB.SelectedItem = (Datos_Almacen.DataSource as StoreList).GetItem(EntityInfo.OidAlmacen);
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    InventarioAlmacenReportMng reportMng = new InventarioAlmacenReportMng(AppContext.ActiveSchema);
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

        #endregion        
    }
}
