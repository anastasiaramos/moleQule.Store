using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TipoAnimalUIForm : ItemMngSkinForm
    {

        #region Business Methods

        public const string ID = "TipoAnimalUIForm";
        public static Type Type { get { return typeof(TipoAnimalUIForm); } }

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
        private TipoAnimales _tipo_animales;

        public TipoAnimales TipoAnimales
        {
            get { return _tipo_animales; }
            set { _tipo_animales = value; }
        }

        #endregion

        #region Factory Methods

        public TipoAnimalUIForm()
            : this(false)
        {
        }

        public TipoAnimalUIForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_Grid.DataSource = DatosLocal_BS;

            this.Text = Resources.Labels.TIPOANIMAL_TITLE;
        }

        protected override void GetFormSourceData()
        {
            _tipo_animales = TipoAnimales.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false; ;

                // do the save
                try
                {
                    _tipo_animales.Save();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
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
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (Datos_Grid == null) return;

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Valor.Tag = 1;

            cols.Add(Valor);

            ControlsMng.MaximizeColumns(Datos_Grid, cols);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = TipoAnimales.SortList(_tipo_animales, "Valor", ListSortDirection.Ascending);
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

        private void TipoAnimalUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tipo_animales.CloseSession();
        }

        #endregion

    }
}

