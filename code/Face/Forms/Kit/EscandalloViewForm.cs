using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class EscandalloViewForm : EscandalloForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata de la Albaran actual y que se va a editar.
        /// </summary>
        private ProductInfo _entity;

        public override ProductInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public EscandalloViewForm(long oid) 
			: this(oid, null) { }

        public EscandalloViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = ProductInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
			base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Datos_PExpediente.DataSource = _entity.Partida;
            Datos_Componentes.DataSource = _entity.Partida.Componentes;
            Bar.Grow();

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

