using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class WorkReportCategoryUIForm : Skin02.ListMngSkinForm
    {
        #region Attributes & Properties

		public const string ID = "WorkReportCategoryUIForm";
		public static Type Type { get { return typeof(WorkReportCategoryUIForm); } }

		private WorkReportCategories _list;

		public WorkReportCategories List
        {
            get { return _list; }
            set { _list = value; }
        }

        #endregion

        #region Factory Methods

        public WorkReportCategoryUIForm()
            : this(null) {}

		public WorkReportCategoryUIForm(Form parent)
			: base(true, parent)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_DGW.DataSource = DatosLocal_BS;
        }

        protected override void GetFormSourceData()
        {
			_list = WorkReportCategories.GetList();
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
                    _list.Save();
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
                    RefreshMainData();
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }

        protected virtual void CloseSession()
        {
            if (_list != null) _list.CloseSession();
        }
        
        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            OBComments.Tag = 1;

			cols.Add(OBComments);

            ControlsMng.MaximizeColumns(Datos_DGW, cols);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _list;
        }

		public override void RefreshSecondaryData()
		{
		}

        #endregion

        #region Business Methods

        #endregion

        #region Buttons

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void AddAction()
        {
            _list.NewItem();
            Datos.ResetBindings(false);
        }

        protected override void DeleteAction()
        {
            throw new Exception("Comprobar que no hay partes asociados");
        }

        protected override void CancelAction()
        {
            _list.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void TarjetaCreditoUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           CloseSession();
        }

        private void TarjetaCreditoUIForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Datos_DGW.Name);
        }

        private void Datos_DG_DoubleClick(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Default);
        }

        #endregion

    }
}
