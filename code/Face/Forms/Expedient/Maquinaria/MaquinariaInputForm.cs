using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class MaquinariaInputForm : Skin01.InputSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        public const string ID = "MaquinariaInputForm";
        public static Type Type { get { return typeof(MaquinariaInputForm); } }

        private Batch _entity;
        private Maquinaria _maquina;
        private Expedient _expedient;

        public Batch Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        public ProductoProveedorInfo CurrentProducto { get { return Datos_Productos.Current != null ? Datos_Productos.Current as ProductoProveedorInfo : null; } }

        #endregion

        #region Factory Methods

        public MaquinariaInputForm(Form parent, Expedient exp)
            : this(true, parent, exp) {}

        public MaquinariaInputForm(bool is_modal, Form parent, Expedient exp)
			: base(is_modal, parent)
        {
            InitializeComponent();
            _expedient = exp;
            _entity = _expedient.Partidas.NewItem(null, _expedient);
            _maquina = _expedient.Maquinarias.NewItem(_expedient);
            SetFormData();
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            ColumnaProductoNombre.Tag = 1;

            cols.Add(ColumnaProductoNombre);

            ControlsMng.MaximizeColumns(ProductoTabla, cols);
            ControlsMng.MarkGridColumn(ProductoTabla, ControlsMng.GetCurrentColumn(ProductoTabla));
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Datos_Maquinaria.DataSource = _maquina;
			PgMng.Grow();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
            ProveedorInfo proveedor = ProveedorInfo.Get(_expedient.OidProveedor, ETipoAcreedor.Proveedor, true);
			Datos_Productos.DataSource = proveedor.Productos.GetSubList(new FCriteria<long>("OidFamilia", (long)ETipoFamilia.Maquinaria));
            _entity.Proveedor = proveedor.Nombre;
			PgMng.Grow();
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (CurrentProducto == null)
            {
                _action_result = DialogResult.OK;
                return;
            }

            _entity.CopyFrom(_maquina, CurrentProducto, _expedient, true);
            _expedient.UpdateTotalesProductos(_expedient.Partidas, true);

            _action_result = DialogResult.OK;
        }

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            if (_maquina != null) _expedient.Maquinarias.Remove(_expedient, _maquina);

            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void Datos_Productos_CurrentChanged(object sender, EventArgs e)
        {
            if (CurrentProducto == null) return;
            _entity.CopyFrom(_maquina, CurrentProducto, _expedient, true);
        }

        private void PVDKilo_NTB_TextChanged(object sender, EventArgs e)
        {
            _entity.CalculaCostes(_expedient.GastoPorKilo, 0);
        }

        private void PVPKilo_NTB_TextChanged(object sender, EventArgs e)
        {
            _entity.CalculaCostes(_expedient.GastoPorKilo, 0);
        }

        #endregion
        
    }
}

