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
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TransporterForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

		public virtual Transporter Entity { get { return null; } set { } }
		public virtual TransporterInfo EntityInfo { get { return null; } }

        protected bool _refresh_list = false;

        #endregion

        #region Factory Methods

        public TransporterForm() 
			: this(-1, ETipoAcreedor.Todos, null) {}

		public TransporterForm(long oid, ETipoAcreedor providerType, Form parent)
			: this(oid, providerType, true, null) { }

		public TransporterForm(long oid, ETipoAcreedor providerType, bool isModal, Form parent)
			: base(oid, new object[2] { providerType, null }, isModal, parent)
        {
            InitializeComponent();
        }

        public TransporterForm(IAcreedor item, Form parent)
            : base(item.Oid, new object[2] { item.ETipoAcreedor, item }, true, parent)
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

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Producto.Tag = 1;

			cols.Add(Producto);

			ControlsMng.MaximizeColumns(Productos_DGW, cols);

			// Precios de Destino
			cols.Clear();
			DestinoNombreCliente.Tag = 1;

			cols.Add(DestinoNombreCliente);

			ControlsMng.MaximizeColumns(PrecioDestino_DGW, cols);

			PrecioDestino_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			// Precios de Origen
			cols.Clear();
			OrigenPuerto.Tag = 0.5;
			OrigenProveedor.Tag = 0.5;

			cols.Add(OrigenPuerto);
			cols.Add(OrigenProveedor);

			ControlsMng.MaximizeColumns(PrecioOrigen_DGW, cols);
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

            PrecioOrigen_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		#endregion

		#region Source

		public override void RefreshSecondaryData()
		{
			Datos_FormaPago.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList();
			PgMng.Grow();

			Datos_MedioPago.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList();
			PgMng.Grow();

			Datos_TipoID.DataSource = moleQule.Common.Structs.EnumText<ETipoID>.GetList();
			PgMng.Grow();

			ETipoAcreedor[] tipos = { ETipoAcreedor.TransportistaOrigen, ETipoAcreedor.TransportistaDestino };
            ProviderType_BS.DataSource = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetList(tipos);
			PgMng.Grow();
		}

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        /// <summary>
        /// Implementa Docs_BT_Click
        /// </summary>
        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Transporter), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Transporter), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

		protected virtual void AddProductoAction() { }
		protected virtual void EditProductoAction() { }
		protected virtual void DeleteProductoAction() { }
        protected virtual void AgregarPrecioOrigenAction() { }
        protected virtual void EditarPrecioOrigenAction() { }
        protected virtual void EliminarPrecioOrigenAction() { }
        protected virtual void DefaultOrigenAction() { }
        protected virtual void AgregarPrecioDestinoAction() { }
        protected virtual void EditarPrecioDestinoAction() { }
        protected virtual void EliminarPrecioDestinoAction() { }
        protected virtual void DefaultDestinoAction() { }
		protected virtual void SelectImpuestoLineaAction() { }
		protected virtual void SelectTipoDescuentoLineaAction() { }
        protected virtual void SelectTarjetaAsociadaAction() { }

        #endregion

        #region Buttons

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

        private void AddOrigen_TI_Click(object sender, EventArgs e)
        {
            AgregarPrecioOrigenAction();
        }

        private void EditOrigen_TI_Click(object sender, EventArgs e)
        {
            EditarPrecioOrigenAction();
        }

        private void DeleteOrigen_TI_Click(object sender, EventArgs e)
        {
            EliminarPrecioOrigenAction();
        }

        private void AddDestino_TI_Click(object sender, EventArgs e)
        {
            AgregarPrecioDestinoAction();
        }

        private void EditDestino_TI_Click(object sender, EventArgs e)
        {
            EditarPrecioDestinoAction();
        }

        private void DeleteDestino_TI_Click(object sender, EventArgs e)
        {
            EliminarPrecioDestinoAction();
        }

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

		#region Events

		private void SendMail_BT_Click(object sender, EventArgs e)
		{
			SendMailAction();
		}

		private void Productos_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Productos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

            if (Productos_DGW.Columns[e.ColumnIndex].Name == Impuesto.Name) { SelectImpuestoLineaAction(); }
            else if (Productos_DGW.Columns[e.ColumnIndex].Name == TipoDescuentoLabel.Name) SelectTipoDescuentoLineaAction();
		}

        private void PrecioOrigen_DG_DoubleClick(object sender, EventArgs e)
        {
            DefaultOrigenAction();
        }

        private void PrecioDestino_DG_DoubleClick(object sender, EventArgs e)
        {
            DefaultDestinoAction();
        }

        #endregion
    }
}

