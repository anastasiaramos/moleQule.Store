using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.Face;
using moleQule.Face.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice;

namespace moleQule.Face.Store
{
    public partial class PrecioDestinoForm : Skin01.InputSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2 ; } }

        public const string ID = "PrecioDestinoForm";
        public static Type Type { get { return typeof(PrecioDestinoForm); } }

        private PrecioDestino _entity;
        private Transporter _transportista;

        public PrecioDestino Entity
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
        public PrecioDestinoForm(Transporter transportista)
            : this(true, transportista) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IsModal"></param>
        public PrecioDestinoForm(bool IsModal, Transporter transportista)
            : base(IsModal)
        {
            InitializeComponent();
            _transportista = transportista;
            _entity = PrecioDestino.NewChild(_transportista);
            SetFormData();
        }

        /// <summary>
        /// Constructo para editar un precio
        /// </summary>
        /// <param name="precio">Precio a editar</param>
        public PrecioDestinoForm(PrecioDestino precio)
            : this(true, precio) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IsModal"></param>
        public PrecioDestinoForm(bool IsModal, PrecioDestino precio)
            : base(IsModal)
        {
            InitializeComponent();
            _entity = precio;
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
            Datos_PuertoDestino.DataSource = PuertoList.GetList(false);
            PgMng.Grow();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (Datos_PuertoDestino.Current == null)
            {
                MessageBox.Show(Resources.Messages.SELECT_PORT, 
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                _action_result = DialogResult.Ignore;
                return;
            }

            if (_entity.OidCliente == 0)
            {
                MessageBox.Show(Resources.Messages.SELECT_CLIENT,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);

                _action_result = DialogResult.Ignore;
                return;
            }

            _entity.Puerto = ((PuertoInfo)Datos_PuertoDestino.Current).Valor;

            if (_transportista != null)
                _transportista.PrecioDestinos.AddItem(_entity);

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

        #endregion

        #region Events

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ClienteInfo cliente = ((ClienteInfo)form.Selected);
                _entity.OidCliente = cliente.Oid;
                _entity.CodigoCliente = cliente.Codigo;
                _entity.NombreCliente = cliente.Nombre;
            }
        }

        #endregion
    }
}

