using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ConceptoKitAddForm : ConceptoKitUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoKitAddForm";
        public static Type Type { get { return typeof(ConceptoKitAddForm); } }

        protected ExpedienteList _expedientes;
        protected ExpedientInfo _almacen;

        #endregion
        
        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public ConceptoKitAddForm(Product product, FamiliaInfo familia)
            : base(product, familia) 
        {
            InitializeComponent();

            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Layout & Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            _entity = _product.Partida.Componentes.NewItem(_product.Partida);
            Datos.DataSource = _entity;
            Bar.Grow();

            base.RefreshMainData();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
            switch (_familia.ETipoFamilia)
            {
                case ETipoFamilia.Todas:
                    _expedientes = ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Todos, true);
                    break;

                case ETipoFamilia.Ganado:
                    _expedientes = ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Ganado, true);
                    break;

                case ETipoFamilia.Maquinaria:
                    _expedientes = ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Maquinaria, true);
                    break;

                case ETipoFamilia.Alimentacion:
                    _expedientes = ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Alimentacion, true);
                    break;
            }

            _almacen = ExpedientInfo.GetAlmacen(true);
        }

        #endregion

        #region Business Methods

        protected void SelectProducto()
        {
            BatchList lista = BatchList.GetListByFamiliaNoKits(_familia.Oid, false);

            BatchSelectForm form = new BatchSelectForm(this, null, lista);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _batch = form.Selected as BatchInfo;
                _expedient = (_batch.OidExpediente == 1) ? _almacen : _expedientes.GetItem(_batch.OidExpediente);
                Datos_Partida.DataSource = _batch;
            }
        }

        protected override void ActualizaConcepto()
        {
            if (ProExp == null) return;
            _entity.CopyFrom(ProExp);
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
			_product.Partida.Componentes.Remove(_entity);

            if (!IsModal)
                _entity.CancelEdit();

            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void DoSubmitAction()
        {
            _product.CalculaPrecioKit();
        }

        private void Productos_BT_Click(object sender, EventArgs e)
        {
            SelectProducto();
        }

        #endregion

        #region Events


        #endregion
    }
}