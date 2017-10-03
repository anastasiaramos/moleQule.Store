using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class SerieViewForm : SerieForm
	{
		#region Attributes & Properties

		/// <summary>
        /// Se trata de la Serie actual y que se va a editar.
        /// </summary>
        private SerieInfo _entity;

        public override SerieInfo EntityInfo
        {
            get { return _entity; }
        }

        #endregion

        #region Factory Methods

		public SerieViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = SerieInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
			base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			Bar.Grow();

            TipoSerie_CMB.Text = _entity.ETipoSerie.ToString();
            Bar.Grow();

			base.RefreshMainData();
            Bar.Grow();
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
	}
}