using System;
using System.Windows.Forms;

using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PrecioOrigenForm : Skin01.InputSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        public const string ID = "PrecioOrigenForm";
        public static Type Type { get { return typeof(PrecioOrigenForm); } }

        private PrecioOrigen _entity;
        private Transporter _transporter;

        public PrecioOrigen Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructo para crear un precio
        /// </summary>
        /// <param name="transportista">Transporter para agregar un precio</param>
        public PrecioOrigenForm(Transporter transportista)
            : this(true, transportista) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IsModal"></param>
        public PrecioOrigenForm(bool IsModal, Transporter transportista)
            : base(IsModal)
        {
            InitializeComponent();
            _transporter = transportista;
            _entity = PrecioOrigen.NewChild(_transporter);
            SetFormData();
        }

        /// <summary>
        /// Constructo para editar un precio
        /// </summary>
        /// <param name="precio">Precio a editar</param>
        public PrecioOrigenForm(PrecioOrigen precio)
            : this(true, precio) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IsModal"></param>
        public PrecioOrigenForm(bool IsModal, PrecioOrigen precio)
            : base(IsModal)
        {
            InitializeComponent();
            _entity = precio;
            this.Text = Resources.Labels.PRECIO_EDIT_TITLE;
            SetFormData();
        }

        #endregion

        #region Layout & Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
        }

        public override void RefreshSecondaryData()
        {
            Datos_PuertoOrigen.DataSource = PuertoList.GetList(false);
            PgMng.Grow();
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (Datos_PuertoOrigen.Current == null)
            {
                MessageBox.Show(Resources.Messages.SELECT_PORT, 
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (_entity.OidProveedor == 0)
            {
                MessageBox.Show(Resources.Messages.NO_PROV_SELECTED, 
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _action_result = DialogResult.Ignore;
                return;
            }

            _entity.Puerto = ((PuertoInfo)Datos_PuertoOrigen.Current).Valor;
            
            if (_transporter != null)
                _transporter.PrecioOrigenes.AddItem(_entity);

            _action_result = DialogResult.OK;
        }

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            if (!IsModal)
                _entity.CancelEdit();

            _action_result = DialogResult.Cancel;
        }

        private void Proveedor_BT_Click(object sender, EventArgs e)
        {
			ProveedorList list = ProveedorList.GetList(moleQule.Base.EEstado.Active, false);
            SupplierSelectForm form = new SupplierSelectForm(this, list);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ProveedorInfo prov = (ProveedorInfo)form.Selected;
                _entity.Proveedor = prov.Nombre;
                _entity.OidProveedor = prov.Oid;
            }
        }

        #endregion

        #region Events

        #endregion        
    }
}

