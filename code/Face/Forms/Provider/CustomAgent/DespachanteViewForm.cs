using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Store;
using moleQule.Library.Invoice;

namespace moleQule.Face.Store
{
    public partial class DespachanteViewForm : CustomAgentForm
    {
        #region Attributes & Properties
		
        public new const string ID = "DespachanteViewForm";
		public new static Type Type { get { return typeof(DespachanteViewForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private DespachanteInfo _entity;

        public override DespachanteInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public DespachanteViewForm(long oid) 
			: this(oid, null) {}

        public DespachanteViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = DespachanteInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout & Source

        /// <summary>Da formato visual a los Controles del formulario
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

            PuertoList puertos = Datos_Puertos.DataSource as PuertoList;
            foreach (PuertoInfo pi in puertos)
            {
                if (_entity.PuertoDespachantes.ContainsPuerto(pi.Oid))
                {
                    int index = Puertos_CLB.Items.IndexOf(pi);
                    if (index != -1)
                        Puertos_CLB.SetItemChecked(index, true);
                }
            }
            Bar.Grow();

            base.RefreshMainData();	
        }
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Asigna formato deseado a los controles del objeto cuando éste es modificado
        /// </summary>
        protected override void FormatData()
        {
        }

        #endregion

        #region Print

        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }
}
