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
    public partial class RazaAnimalUIForm : ItemMngSkinForm
    {

        #region Business Methods

        public const string ID = "RazaAnimalUIForm";
        public static Type Type { get { return typeof(RazaAnimalUIForm); } }

        private RazaAnimales _raza_animales;

        public RazaAnimales RazaAnimales
        {
            get { return _raza_animales; }
            set { _raza_animales = value; }
        }

        #endregion

        #region Factory Methods

        public RazaAnimalUIForm()
            : this(false)
        {
        }

        public RazaAnimalUIForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_Grid.DataSource = DatosLocal_BS;

            this.Text = Resources.Labels.RAZAANIMAL_TITLE;
        }

        protected override void GetFormSourceData()
        {
            _raza_animales = RazaAnimales.GetList();
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
                    _raza_animales.Save();
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

        public override void FormatControls()
        {
            if (Datos_Grid == null) return;

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Valor.Tag = 1;

            cols.Add(Valor);

            ControlsMng.MaximizeColumns(Datos_Grid, cols);
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = RazaAnimales.SortList(_raza_animales, "Valor", ListSortDirection.Ascending);
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

        private void RazaAnimalUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _raza_animales.CloseSession();
        }

        #endregion

    }
}

