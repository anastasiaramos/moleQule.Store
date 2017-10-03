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
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class BatchNewForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

		public const string ID = "BatchNewForm";
        public static Type Type { get { return typeof(BatchNewForm); } }

        private Batch _entity;
        private Expedient _expediente;
        private ProveedorInfo _proveedor;
        private ProductList _productos;

        public Batch Entity { get { return _entity; } }

        public ProductoProveedorInfo CurrentProductoProveedor { get { return Datos_ProductoProveedor.Current != null ? ((ProductoProveedorInfo)Datos_ProductoProveedor.Current) : null; } }

		#endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchNewForm(Form parent, Expedient exp)
            : this(true, parent, exp) {}

        public BatchNewForm(bool isModal, Form parent, Expedient exp)
            : base(isModal, parent)
        {
            InitializeComponent();
            this.Text = Resources.Labels.LISTAPRODPROV_TITLE;
            _expediente = exp;
            _proveedor = ProveedorInfo.Get(exp.OidProveedor, ETipoAcreedor.Proveedor, true);
            _entity = _expediente.Partidas.NewItem(null, _expediente);

            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
            SetFormData();
        }

        void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PrecioCompraKilo":
                    CalculaCostes();
                    break;
                case "KilosIniciales":
                    CalculaCostes();
                    break;
                case "BultosIniciales":
                    CalculaCostes();
                    break;
            }
        }
        
        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            Source_GB.Enabled = false;
            Source_GB.Visible = false;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            ColumnaProductoNombre.Tag =1;

            cols.Add(ColumnaProductoNombre);

            ControlsMng.MaximizeColumns(ProductoTabla, cols);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;

            Batchs pes = _expediente.Partidas;
			ProductoProveedorList ppl = _proveedor.Productos;
			PgMng.Grow();

            List<ProductoProveedorInfo> lista = new List<ProductoProveedorInfo>();

            foreach (ProductoProveedorInfo ppi in ppl)
            {
                if (!(pes.ContainsProducto(ppi.OidProducto) && pes.ContainsProveedor(_proveedor.Oid)))
                {
                    lista.Add(ppi);
                }
            }

            Datos_ProductoProveedor.DataSource = lista;
			PgMng.Grow();

            AsignaPrecios();
			PgMng.Grow();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
            _productos = ProductList.GetListBySupplier(_proveedor.Oid, false);
            PgMng.Grow();
        }

        #endregion

		#region Business Methods

		private void AsignaPrecios()
		{
			if (Datos_ProductoProveedor.Current == null) return;

			ProductoProveedorInfo ppi = (ProductoProveedorInfo)Datos_ProductoProveedor.Current;
			ProductInfo pi = _productos.GetItem(ppi.OidProducto);

			_entity.CalculaValores(_expediente, pi, ppi);

			Datos.ResetBindings(false);
		}

		private void CalculaCostes()
		{
			if (Datos_ProductoProveedor.Current == null) return;

			_entity.CalculaCostes(_expediente.GastoPorKilo, 0);

			Datos.ResetBindings(false);
		}

		#endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (Datos_ProductoProveedor.Current == null)
            {
                MessageBox.Show("Debe elegir un producto.",
                                Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (_entity.KilosIniciales == 0 || _entity.BultosIniciales == 0)
            {
                MessageBox.Show("Es necesario poner el número de kilos y de bultos.",
                                Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            _entity.CopyFrom(CurrentProductoProveedor, _expediente, true);

            if (_expediente.Partidas.Count == 1)
            {
                if (_expediente.TipoMercancia == string.Empty)
                    _expediente.TipoMercancia = CurrentProductoProveedor.Producto;
                else
                    _expediente.TipoMercancia += "\n" + CurrentProductoProveedor.Producto;
            }

            _expediente.UpdateTotalesProductos(_expediente.Partidas, true);

            _action_result = DialogResult.OK;
        }

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
             if (_entity != null) _expediente.Partidas.Remove(_expediente, _entity.Oid);

            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void Datos_ProductoProveedor_CurrentChanged(object sender, EventArgs e)
        {
            AsignaPrecios();
        }

        #endregion
    }
}

