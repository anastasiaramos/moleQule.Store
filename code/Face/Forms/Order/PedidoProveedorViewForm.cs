using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorViewForm : PedidoProveedorForm
    {

        #region Attributes & Properties
		
        public const string ID = "PedidoProveedorViewForm";
		public static Type Type { get { return typeof(PedidoProveedorViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        private PedidoProveedorInfo _entity;

        public override PedidoProveedorInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public PedidoProveedorViewForm(long oid, ETipoAcreedor tipo) 
			: this(oid, tipo, null) {}

        public PedidoProveedorViewForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.PEDIDOPROVEEDOR_DETAIL_TITLE + " " + EntityInfo.Codigo.ToUpper();
            _mf_type = ManagerFormType.MFView;
        }

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];

            _entity = PedidoProveedorInfo.Get(oid, tipo, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            base.FormatControls();
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.DataGridView == Lineas_DGW)
			{
				LineaPedidoProveedorInfo item = row.DataBoundItem as LineaPedidoProveedorInfo;
				row.DefaultCellStyle = (item.Pendiente != 0) ? ControlTools.Instance.LineaPendienteStyle : ControlTools.Instance.LineaCerradaStyle;
			}
		}

		#endregion

		#region Source
		
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
			
			Datos_Lineas.DataSource = _entity.Lineas;
			PgMng.Grow();			
			
            base.RefreshMainData();
        }
		
        #endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

		#endregion
     }
}
