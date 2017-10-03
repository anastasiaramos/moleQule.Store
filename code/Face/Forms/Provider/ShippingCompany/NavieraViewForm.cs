using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;

using moleQule.Face;
using moleQule;

using moleQule.Library.Store;
using moleQule.Library.Invoice;

namespace moleQule.Face.Store
{
	public partial class NavieraViewForm : ShippingCompanyForm
	{
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata de la Naviera actual y que se va a editar.
        /// </summary>
        private NavieraInfo _entity;

        public override NavieraInfo EntityInfo
        {
            get { return _entity; }
        }

        #endregion

        #region Factory Methods

		public NavieraViewForm()
			: this(-1, null) { }

        public NavieraViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = NavieraInfo.Get(oid, true);
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

			Datos_Productos.DataSource = _entity.Productos;

			base.RefreshMainData();
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

