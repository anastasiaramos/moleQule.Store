using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Common.Structs;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class SupplierForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		public override Type EntityType { get { return typeof(Proveedor); } }

        public virtual Proveedor Entity { get { return null; } set { } }
        public virtual ProveedorInfo EntityInfo { get { return null; } }

        protected bool _refresh_list = false;

        #endregion

        #region Factory Methods

        public SupplierForm() 
			: this(true) { }

        public SupplierForm(bool isModal)
			: this(-1, ETipoAcreedor.Proveedor, isModal, null) { }

		public SupplierForm(long oid, ETipoAcreedor providerType)
			: this(oid, providerType, true, null) { }

        public SupplierForm(long oid, ETipoAcreedor providerType, bool isModal, Form parent)
            : base(oid, new object[2]{ providerType, null }, isModal, parent)
        {
            InitializeComponent();
        }

        public SupplierForm(IAcreedor item, bool isModal, Form parent)
            : base(item.Oid, new object[2]{ item.ETipoAcreedor, item }, isModal, parent)
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
		}

        public override void FormatControls()
        {
			//IDE COMPATIBILITY
			if (AppContext.User == null) return;

			MaximizeForm(new Size(950, 725));
			ShowAction(molAction.ShowDocuments);

			base.FormatControls();

            CuentaContable_TB.Mask = (EntityInfo.CuentaContable != moleQule.Accounting.Resources.Defaults.NO_CONTABILIZAR)
							? Library.Invoice.ModuleController.GetCuentasMask()
							: string.Empty;
			PrecioProducto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

			Productos_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
					break;
			}
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

			ETipoAcreedor[] tipos = { ETipoAcreedor.Proveedor, ETipoAcreedor.Acreedor };
            ProviderType_BS.DataSource = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetList(tipos);
			PgMng.Grow();
        }

        #endregion

        #region Actions

        protected override void DocumentsAction()
        {
            try
            {
				AgenteEditForm form = new AgenteEditForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
					AgenteAddForm form = new AgenteAddForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

		protected virtual void AddProductoAction() {}
		protected virtual void DeleteProductoAction() { }
		protected virtual void EditProductoAction() {}
		protected virtual void SelectImpuestoLineaAction() {}
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

		private void SendMail_BT_Click(object sender, EventArgs e) { SendMailAction(); }

		private void AddProducto_TI_Click(object sender, EventArgs e) {	AddProductoAction(); }

		private void EditProducto_TI_Click(object sender, EventArgs e) { EditProductoAction();	}

		private void DeleteProducto_TI_Click(object sender, EventArgs e) {	DeleteProductoAction(); }

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
