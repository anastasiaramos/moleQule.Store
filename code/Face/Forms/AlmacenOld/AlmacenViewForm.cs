using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class AlmacenViewForm : AlmacenForm
    {

        #region Attributes & Properties
		
        public new const string ID = "AlmacenViewForm";
		public new static Type Type { get { return typeof(AlmacenViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private AlmacenInfo _entity;

        public override AlmacenInfo EntityInfo { get { return _entity; } }
        public virtual InventarioAlmacenInfo InventarioInfo { get { return Datos_InventarioAlmacenes.Current as InventarioAlmacenInfo; } }

		#endregion
		
        #region Factory Methods

        public AlmacenViewForm(long oid) : this(oid, null) {}

        public AlmacenViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.ALMACEN_DETAIL_TITLE + " " + EntityInfo.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = AlmacenInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style & Source

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Bar.Grow();
			
            //Datos_InventarioAlmacenes.DataSource = InventarioAlmacenList.SortList(_entity.InventarioAlmacenes, "NOMBRE", ListSortDirection.Ascending);
            //Bar.Grow();
			
			Datos_LineaAlmacenes.DataSource = LineaAlmacenList.SortList(_entity.LineaAlmacenes, "CONCEPTO", ListSortDirection.Ascending);
            Bar.Grow();
			
			
            base.RefreshMainData();
        }
		
        protected override void SetUnlinkedGridValues(string gridName)
        {
            /*switch (gridName)
            {
                
				case "_Grid":
                    {
                        InventarioAlmacenList inventarioalmacen = InventarioAlmacenList.GetList(false);
                        foreach (DataGridViewRow row in InventarioAlmacen_Grid.Rows)
                        {
                            if (row.IsNewRow) continue;
                            Alumno_ExamenInfo info = (Alumno_ExamenInfo)row.DataBoundItem;
                            if (info != null)
                            {
                                InventarioAlmacenInfo inventarioalmacen = inventarioalmacenes.GetItem(info.OidInventarioAlmacen);
                                if (examen != null)
                                    row.Cells[InventarioAlmacen.Name].Value = inventarioalmacen.Titulo;
                            }
                        }

                    } break;
				
				case "_Grid":
                    {
                        LineaAlmacenList lineaalmacen = LineaAlmacenList.GetList(false);
                        foreach (DataGridViewRow row in LineaAlmacen_Grid.Rows)
                        {
                            if (row.IsNewRow) continue;
                            Alumno_ExamenInfo info = (Alumno_ExamenInfo)row.DataBoundItem;
                            if (info != null)
                            {
                                LineaAlmacenInfo lineaalmacen = lineaalmacenes.GetItem(info.OidLineaAlmacen);
                                if (examen != null)
                                    row.Cells[LineaAlmacen.Name].Value = lineaalmacen.Titulo;
                            }
                        }

                    } break;
				
            }*/
        }
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Asigna formato deseado a los controles del objeto cuando éste es modificado
        /// </summary>
        protected override void FormatData()
        {
        }

        #endregion

        #region Print

        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Buttons

        /// <summary>
        /// Abre el formulario para ver item
        /// <returns>void</returns>
        /// </summary>
        protected override void InventarioAlmacenes_Grid_DoubleClick()
        {
            if (InventarioInfo == null) return;
            InventarioAlmacenViewForm form = new InventarioAlmacenViewForm(InventarioInfo.Oid, this);
            form.ShowDialog(this);
        }

        #endregion

        #region Events

        #endregion

    }
}
