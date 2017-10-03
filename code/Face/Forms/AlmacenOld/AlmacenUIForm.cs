using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class AlmacenUIForm : AlmacenForm
    {

        #region Attributes & Properties
		
        public new const string ID = "AlmacenUIForm";
		public new static Type Type { get { return typeof(AlmacenUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Almacen _entity;

        public override Almacen Entity { get { return _entity; } set { _entity = value; } }
        public override AlmacenInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }
        public virtual InventarioAlmacen Inventario { get { return Datos_InventarioAlmacenes.Current as InventarioAlmacen; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected AlmacenUIForm() : this(-1, true) { }

        public AlmacenUIForm(bool isModal) : this(-1, isModal) { }

        public AlmacenUIForm(long oid) : this(oid, true) { }

        public AlmacenUIForm(long oid, bool ismodal)
            : base(oid, ismodal)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                Almacen temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();
					

                    //Decomentar si se va a mantener en memoria
                    //_entity.BeginEdit();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex) +
                                    Environment.NewLine + ex.SysMessage,
                                    ControlerBase.GetApplicationTitle(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),
                                    ControlerBase.GetApplicationTitle(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }

        }

        #endregion

        #region Style & Source

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Bar.Grow();
			
            //Datos_InventarioAlmacenes.DataSource = InventarioAlmacenes.SortList(_entity.InventarioAlmacenes, "NOMBRE", ListSortDirection.Ascending);
            //Bar.Grow();
			
			Datos_LineaAlmacenes.DataSource = LineaAlmacenes.SortList(_entity.LineaAlmacenes, "CONCEPTO", ListSortDirection.Ascending);
            Bar.Grow();
						
            base.RefreshMainData();
            Bar.FillUp();
        }
		
		/// <summary>
        /// Asigna los datos de origen para controles que dependen de otros
        /// </summary>
        /// <param name="controlName"></param>
        protected override void SetDependentControlSource(string controlName)
        {
            /*switch (controlName)
            {
                case "ID_GB":
                    {
                        NIF_RB.Checked = (EntityInfo.TipoId == (long)TipoId.NIF);
                        NIE_RB.Checked = (EntityInfo.TipoId == (long)TipoId.NIE);
                        DNI_RB.Checked = (EntityInfo.TipoId == (long)TipoId.DNI);

                    } break;
            }*/
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
                            Alumno_Examen info = (Alumno_Examen)row.DataBoundItem;
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
                            Alumno_Examen info = (Alumno_Examen)row.DataBoundItem;
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
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

        #region Events

        /// <summary>
        /// Abre el formulario para editar item
        /// <returns>void</returns>
        /// </summary>
        protected override void InventarioAlmacenes_Grid_DoubleClick()
        {                      
            if (Inventario == null) return;
            InventarioAlmacenEditForm form = new InventarioAlmacenEditForm(Inventario.Oid, true, this);
            form.ShowDialog(this);                                       
        }

        #endregion
    }
}
