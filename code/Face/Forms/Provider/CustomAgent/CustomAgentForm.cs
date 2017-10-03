using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class CustomAgentForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "DespachanteForm";
		public static Type Type { get { return typeof(CustomAgentForm); } }

        protected override int BarSteps { get { return base.BarSteps + 4; } }
		
        public virtual Despachante Entity { get { return null; } set { } }
        public virtual DespachanteInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public CustomAgentForm() 
			: this(-1) {}

        public CustomAgentForm(long oid) 
			: this(oid, true, null) {}

		public CustomAgentForm(bool isModal) 
			: this(-1, isModal, null) {}

        public CustomAgentForm(long oid, bool isModal, Form parent)
            : base(oid, new object[1] {null}, isModal, parent)
        {
            InitializeComponent();
        }

        public CustomAgentForm(IAcreedor item, bool isModal, Form parent)
            : base(item.Oid, new object[1] { item }, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
			CuentaContable_TB.Enabled = ProviderBase.CanEditCuentaContable();
			CuentaContable_TB.ReadOnly = !ProviderBase.CanEditCuentaContable();
			CuentaContable_BT.Enabled = ProviderBase.CanEditCuentaContable();
		}

		#endregion

        #region Layout & Source

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Producto.Tag = 1;

			cols.Add(Producto);

			ControlsMng.MaximizeColumns(Productos_DGW, cols);
		}

        public override void FormatControls()
        {
			//IDE Compatibility
			if (AppContext.User == null) return;

			MaximizeForm(new Size(950, 700));
			ShowAction(molAction.ShowDocuments);

            base.FormatControls();

            CuentaContable_TB.Mask = (EntityInfo.CuentaContable != moleQule.Accounting.Resources.Defaults.NO_CONTABILIZAR)
							? Library.Invoice.ModuleController.GetCuentasMask()
							: string.Empty;
			PrecioProducto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
           
        }

        public override void RefreshSecondaryData()
		{
            Datos_Puertos.DataSource = PuertoList.GetList(false); ;
            Puertos_CLB.DataSource = Datos_Puertos;
            Puertos_CLB.DisplayMember = "Valor";
            Puertos_CLB.ValueMember = "";
			PgMng.Grow();

			Datos_FormaPago.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList();
			PgMng.Grow();

			Datos_MedioPago.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList();
			PgMng.Grow();

			Datos_TipoID.DataSource = moleQule.Common.Structs.EnumText<ETipoID>.GetList();
			PgMng.Grow();
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    DespachanteReportMng reportMng = new DespachanteReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Docs_BT_Click
        /// </summary>
        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Despachante), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Despachante), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

		protected virtual void AddProductoAction() {}
		protected virtual void EditProductoAction() { }
		protected virtual void DeleteProductoAction() { }
		protected virtual void SelectImpuestoLineaAction() { }
		protected virtual void SelectTipoDescuentoLineaAction() { }
        protected virtual void SelectTarjetaAsociadaAction() { }

		protected void SendMailAction()
		{
			PgMng.Reset(3, 1, moleQule.Face.Resources.Messages.OPENING_EMAIL_CLIENT, this);

			MailParams mail = new MailParams();

			mail.To = EntityInfo.Email;

			try
			{
				PgMng.Grow();

				EMailSender.MailTo(mail);
				PgMng.Grow();
			}
			catch
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		#endregion

		#region Buttons

		private void SendMail_BT_Click(object sender, EventArgs e)
		{
			SendMailAction();
		}

		private void AddProducto_TI_Click(object sender, EventArgs e)
		{
			AddProductoAction();
		}

		private void EditProducto_TI_Click(object sender, EventArgs e)
		{
			EditProductoAction();
		}

		private void DeleteProducto_TI_Click(object sender, EventArgs e)
		{
			DeleteProductoAction();
		}        

        #endregion

        #region Events

		private void Productos_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Productos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Productos_DGW.Columns[e.ColumnIndex].Name == Impuesto.Name) { SelectImpuestoLineaAction(); }
			else if (Productos_DGW.Columns[e.ColumnIndex].Name == TipoDescuentoLabel.Name) SelectTipoDescuentoLineaAction();
		}

	    #endregion
    }
}
