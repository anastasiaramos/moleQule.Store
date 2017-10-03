using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using CslaEx;

using moleQule.Library;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Store;
using moleQule.Library.Invoice;

namespace moleQule.Face.Store
{
    public partial class ProductoAlmacenNewForm : InputSkinForm
    {

        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public const string ID = "ProductoAlmacenNewForm";
        public static Type Type { get { return typeof(ProductoAlmacenNewForm); } }

        private Partida _entity;
        private Expediente _expediente;
        private ProductoList _productos;

        public Partida Entity
        {
            get { return (Partida)Datos.Current; }
        }

        public ProductoProveedorInfo ProductoActual { get { return Datos_ProductoProveedor.Current as ProductoProveedorInfo; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductoAlmacenNewForm(Expediente exp)
            : this(true, exp) {}

        public ProductoAlmacenNewForm(bool is_modal, Expediente exp)
            : this(is_modal, null, exp) {}

        public ProductoAlmacenNewForm(Form parent, Expediente exp)
            : this(true, parent, exp) { }

        public ProductoAlmacenNewForm(bool is_modal, Form parent, Expediente exp)
            : base(true, parent)
        {
            InitializeComponent();
            this.Text = Resources.Labels.LISTA_PRODUCTOS_TITLE;
            _expediente = exp;
            _entity = _expediente.Partidas.NewItem(null, _expediente, ETipoStock.Compra);
            _entity.FechaCompra = DateTime.Today;
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
                case "GastoKilo":
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

        private void AsignaPrecios()
        {
            if (Datos_ProductoProveedor.Current == null) return;

            ProductoProveedorInfo ppi = (ProductoProveedorInfo)Datos_ProductoProveedor.Current;
            ProductoInfo pi = _productos.GetItem(ppi.OidProducto);

            _entity.CalculaValores(_expediente, pi, ppi);

            Datos.ResetBindings(false);
        }

        private void CalculaCostes()
        {
            if (Datos_ProductoProveedor.Current == null) return;

            _entity.CalculaCostes(null);

            Datos.ResetBindings(false);
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            Source_GB.Enabled = false;
            Source_GB.Visible = false;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            ColumnaProductoNombre.Tag = 1;
            //ColumnaProductoObs.Tag = 0.3;

            cols.Add(ColumnaProductoNombre);
            //cols.Add(ColumnaProductoObs);

            ControlsMng.MaximizeColumns(Productos_Grid, cols);
            Productos_Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Bar.Grow();

            ProductoProveedorList ppl = ProductoProveedorList.GetList();
            Datos_ProductoProveedor.DataSource = ProductoProveedorList.GetSortedList(ppl, "Nombre", ListSortDirection.Ascending);
            Bar.Grow();

            AsignaPrecios();
            Bar.Grow();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
            _productos = ProductoList.GetList(false);
            Bar.Grow();
        }

        #endregion

        #region Actions

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
                MessageBox.Show("Es necesario poner el numero de kilos y de bultos.",
                                Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            _entity.CopyFrom(ProductoActual, _expediente);
            _expediente.UpdateTotalesProductos();

            _action_result = DialogResult.OK;
        }

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            _expediente.Partidas.Remove(_expediente, _entity.Oid);

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

