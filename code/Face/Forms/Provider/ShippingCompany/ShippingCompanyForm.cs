using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.CslaEx;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Hipatia;

namespace moleQule.Face.Store
{
    public partial class ShippingCompanyForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		public virtual Naviera Entity { get { return null; } set { } }
		public virtual NavieraInfo EntityInfo { get { return null; } }

        protected bool _refresh_list = false;
        #endregion

        #region Factory Methods

        public ShippingCompanyForm()
            : this(-1, null) {}

        public ShippingCompanyForm(long oid, Form parent)
            : base(oid, new object[1]{null}, true, parent)
        {
            InitializeComponent();
        }

        public ShippingCompanyForm(IAcreedor item, Form parent)
            : base(item.Oid, new object[1] { item }, true, parent)
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

			cols.Clear();
			PuertoOrigen.Tag = 0.5;
			PuertoDestino.Tag = 0.5;

			cols.Add(PuertoOrigen);
			cols.Add(PuertoDestino);

			ControlsMng.MaximizeColumns(Precios_DG, cols);
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

            Precios_DG.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public override void RefreshSecondaryData()
        {
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

        #region Actions

        /// <summary>
        /// Implementa Docs_BT_Click
        /// </summary>
        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Naviera), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Naviera), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

        protected virtual void AgregarPrecioAction() {}
        protected virtual void EditarPrecioAction() {}
        protected virtual void EliminarPrecioAction() {}
        protected virtual void DefaultPrecioAction() {}
		protected virtual void AddProductoAction() {}
		protected virtual void EditProductoAction() {}
		protected virtual void DeleteProductoAction() {}
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

        private void Add_Button_Click(object sender, EventArgs e)
        {
            AgregarPrecioAction();
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            EditarPrecioAction();
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            EliminarPrecioAction();
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

        private void Precios_DG_DoubleClick(object sender, EventArgs e)
        {
            DefaultPrecioAction();
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
    }
}

