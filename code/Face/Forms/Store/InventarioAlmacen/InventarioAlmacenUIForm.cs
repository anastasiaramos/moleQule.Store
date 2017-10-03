using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenUIForm : InventarioAlmacenForm
    {
        #region Attributes & Properties
		
        public new const string ID = "InventarioAlmacenUIForm";
		public new static Type Type { get { return typeof(InventarioAlmacenUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected InventarioAlmacen _entity;

        public override InventarioAlmacen Entity { get { return _entity; } set { _entity = value; } }
        public override InventarioAlmacenInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected InventarioAlmacenUIForm() 
			: this(-1) { }

        public InventarioAlmacenUIForm(long oid) 
			: this(oid, null) { }

        public InventarioAlmacenUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {            
            this.Datos.RaiseListChangedEvents = false;

            InventarioAlmacen temp = _entity.Clone();
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
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout & Source

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

            Datos_LineaInventarios.DataSource = _entity.LineaInventarios;
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
		
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

   }
}
