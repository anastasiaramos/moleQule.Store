using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PuertoUIForm : ItemMngSkinForm
    {

        #region Business Methods

        public const string ID = "PuertoUIForm";
        public static Type Type { get { return typeof(WorkReportCategoryUIForm); } }

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
        private Puertos _puertos;

        public Puertos Puertos
        {
            get { return _puertos; }
            set { _puertos = value; }
        }

        #endregion

        #region Factory Methods

        public PuertoUIForm()
            : this(false)
        {
        }

		public PuertoUIForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            Local_BS = Datos;
            List_DGW.DataSource = Local_BS;

            this.Text = Resources.Labels.PUERTO_TITLE;
        }

        protected override void GetFormSourceData()
        {
            _puertos = Puertos.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
                _puertos.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine +
                                ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (List_DGW == null) return;

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Valor.Tag = 1;

            cols.Add(Valor);

            ControlsMng.MaximizeColumns(List_DGW, cols);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = Puertos.SortList(_puertos, "Valor", ListSortDirection.Ascending);
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

        #region Events

        private void PuertoUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _puertos.CloseSession();
        }

        #endregion

    }
}