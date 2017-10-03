using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InventarioAlmacenViewForm : InventarioAlmacenForm
    {
        #region Attributes & Properties
		
        public new const string ID = "InventarioAlmacenViewForm";
		public new static Type Type { get { return typeof(InventarioAlmacenViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private InventarioAlmacenInfo _entity;

        public override InventarioAlmacenInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public InventarioAlmacenViewForm(long oid) 
			: this(oid, null) {}

        public InventarioAlmacenViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = InventarioAlmacenInfo.Get(oid, true);
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
		
            Datos_LineaInventarios.DataSource = _entity.LineaInventarios;
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
