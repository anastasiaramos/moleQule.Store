using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PrecioTrayectoForm : InputSkinForm
    {

        #region Business Methods

        protected override int BarSteps { get { return 15; } }

        public const string ID = "PrecioTrayectoForm";
        public static Type Type { get { return typeof(PrecioTrayectoForm); } }

        private PrecioTrayecto _entity;

        public PrecioTrayecto Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor para un precio nuevo
        /// </summary>
        public PrecioTrayectoForm()
            : this(true, null)
        {
        }

        /// <summary>
        /// Constructo para editar un precio
        /// </summary>
        /// <param name="precio">Precio a editar</param>
        public PrecioTrayectoForm(PrecioTrayecto precio)
            : this(true, precio)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IsModal"></param>
        /// <param name="precio">Si es null, se creara un precio nuevo</param>
        public PrecioTrayectoForm(bool IsModal, PrecioTrayecto precio)
            : base(IsModal)
        {
            InitializeComponent();
            if (precio == null)
            {
                _entity = PrecioTrayecto.NewChild();
                this.Text = Resources.Labels.PRECIO_NEW_TITLE;
            }
            else
            {
                _entity = precio;
                this.Text = Resources.Labels.PRECIO_EDIT_TITLE;
            }
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
            Bar.FillUp();
        }

        public override void RefreshSecondaryData()
        {
            Datos_PuertoDestino.DataSource = PuertoList.GetList(false);

            if (_entity.PuertoDestino.Length > 0)
                PuertoDestino_CB.SelectedValue = _entity.PuertoDestino;
            Bar.Grow();

            Datos_PuertoOrigen.DataSource = PuertoList.GetList(false);

            if (_entity.PuertoOrigen.Length > 0)
                PuertoOrigen_CB.SelectedValue = _entity.PuertoOrigen;
            Bar.FillUp();
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
                MessageBox.Show(Resources.Messages.SELECT_PORT, moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                _action_result = DialogResult.Ignore;
                return;
            }

            if (Datos_PuertoDestino.Current == null)
            {
                MessageBox.Show(Resources.Messages.SELECT_PORT, moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _action_result = DialogResult.Ignore;
                return;
            }

            _entity.PuertoOrigen = ((PuertoInfo)Datos_PuertoOrigen.Current).Valor;
            _entity.PuertoDestino = ((PuertoInfo)Datos_PuertoDestino.Current).Valor;

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

        #endregion

    }
}

