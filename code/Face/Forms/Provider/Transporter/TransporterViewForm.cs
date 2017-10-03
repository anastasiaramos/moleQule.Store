using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class TransporterViewForm : TransporterForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata de la Transporter actual y que se va a editar.
        /// </summary>
        private TransporterInfo _entity;

        public override TransporterInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public TransporterViewForm()
			: this(-1, ETipoAcreedor.Todos, null) { }

        public TransporterViewForm(long oid, ETipoAcreedor providerType, Form parent)
            : base(oid, providerType, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = TransporterInfo.Get(oid, (ETipoAcreedor)parameters[0], true);
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
            Datos_PrecioDestino.DataSource = _entity.PrecioDestinos;
            Datos_PrecioOrigen.DataSource = _entity.PrecioOrigenes;
            
			base.RefreshMainData();
        }

        #endregion

		#region Validation & Format

		#endregion

		#region Actions

		/// <summary>
		/// Implementa Save_button_Click
		/// </summary>
		protected override void SaveAction()
		{
            _action_result = DialogResult.Cancel;
			Close();
		}

        protected override void DefaultOrigenAction()
        {
            EditarPrecioOrigenAction();
        }

        protected override void DefaultDestinoAction()
        {
            EditarPrecioDestinoAction();
        }

		#endregion

        #region Events

        private void TransportistaViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cerrar();
        }

        #endregion
	}
}

