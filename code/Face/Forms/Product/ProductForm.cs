using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Face.Common;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ProductForm : Skin04.ItemMngSkinForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps; } }

		public override Type EntityType { get { return typeof(Product); } }

		public virtual Product Entity { get { return null; } set { } }
		public virtual ProductInfo EntityInfo { get { return null; } }

		#endregion

		#region Factory Methods

		public ProductForm()
			: this(-1, null) { }

		public ProductForm(Form parent)
			: this(-1, parent) { }

		public ProductForm(long oid, Form parent)
			: base(oid, parent)
		{
			InitializeComponent();
		}

		public override void DisposeForm()
		{
			Cache.Instance.Remove(typeof(BatchList));
		}

		#endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
			if (AppContext.User == null) return;

			ExternalCode_TB.Enabled = AppContext.User.IsAdmin;
			ExternalCode_TB.ReadOnly = !AppContext.User.IsAdmin;

			CuentaContableCompra_TB.Enabled = Product.CanEditCuentaContable();
			CuentaContableCompra_TB.ReadOnly = !Product.CanEditCuentaContable();
			CuentaContableCompra_BT.Enabled = Product.CanEditCuentaContable();
			CuentaContableVenta_TB.Enabled = Product.CanEditCuentaContable();
			CuentaContableVenta_TB.ReadOnly = !Product.CanEditCuentaContable();
			CuentaContableVenta_BT.Enabled = Product.CanEditCuentaContable();
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			MaximizeForm(new Size(1200, 0));

			base.FormatControls();

			HideComponentes(Components_TP);

			try
			{
				CuentaContableVenta_TB.Mask = Library.Invoice.ModuleController.GetCuentasMask();
				CuentaContableCompra_TB.Mask = Library.Invoice.ModuleController.GetCuentasMask();

				string price_format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

				PurchasePrice_NTB.DataBindings[0].FormatString = price_format;
				SalePrice_NTB.DataBindings[0].FormatString = price_format;
				Grant_NTB.DataBindings[0].FormatString = price_format;

				PrecioCompraKilo.DefaultCellStyle.Format = price_format;
				//PrecioCompraBulto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
				CosteKgAyuda.DefaultCellStyle.Format = price_format;
				//PrecioVentaBulto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();	
				PBeneficioMinimo_NTB.Text = EntityInfo.PBeneficioMinimo.ToString("N2");
			}
			catch { }
		}

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			//Partidas
			cols.Clear();
			BAUbicacion.Tag = 1;

			cols.Add(BAUbicacion);

			ControlsMng.MaximizeColumns(Partidas_DGW, cols);

			//Stock
			cols.Clear();
			Proveedor.Tag = 0.25;
			Observaciones.Tag = 0.75;

			cols.Add(Observaciones);
			cols.Add(Proveedor);

			ControlsMng.MaximizeColumns(Stock_DGW, cols);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.ShowDocuments);
					break;
			}
		}

		protected void HideComponentes(TabPage page)
		{
			if (Stock_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Stock_DGW.Rows)
					if ((row.DataBoundItem as StockInfo).IsKitComponent)
						row.Visible = false;
			}
			else if (Partidas_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Partidas_DGW.Rows)
					if ((row.DataBoundItem as BatchInfo).IsKitComponent)
						row.Visible = false;
			}
			else if (Components_TP.Equals(page))
			{
				if (Kit_CkB.Checked)
				{
					if (!Parts_TC.TabPages.Contains(Components_TP)) Parts_TC.TabPages.Add(Components_TP); 
				}
				else
				{
					if (Parts_TC.TabPages.Contains(Components_TP)) Parts_TC.TabPages.Remove(Components_TP);
				}
			}
		}

		#endregion

		#region Actions

		protected override void DocumentsAction()
		{
			try
			{
				AgenteEditForm form = new AgenteEditForm(typeof(Product), EntityInfo as IAgenteHipatia);
				form.ShowDialog(this);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(typeof(Product), EntityInfo as IAgenteHipatia);
					form.ShowDialog(this);
				}
			}
		}

		protected virtual void LoadComponentsAction()
		{
			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				PgMng.Grow();

				EntityInfo.LoadChilds(typeof(Kit), false);
				Datos_Components.DataSource = EntityInfo.Components;
				PgMng.Grow();

				ControlsMng.UpdateBinding(Datos_Components);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadPartidasAction()
		{
			try
			{
				if (Cache.Instance.Get(typeof(BatchList)) != null) return;

				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				PgMng.Grow();

                Batch_BS.DataSource = BatchList.GetByProductList(EntityInfo.Oid, false);
				Cache.Instance.Save(typeof(BatchList), Batch_BS.DataSource as BatchList);
				PgMng.Grow();

				HideComponentes(Partidas_TP);

				ControlsMng.UpdateBinding(Batch_BS);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadStockAction()
		{
			try
			{
				if (Partidas_DGW.CurrentRow == null) return;
				if (Partidas_DGW.CurrentRow.DataBoundItem == null) return;

				BatchInfo batch = Partidas_DGW.CurrentRow.DataBoundItem as BatchInfo;

				//if (Cache.Instance.Get(typeof(StockList)) != null) return;

				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				PgMng.Grow();

				StockList list = StockList.GetByBatchList(batch.Oid, false, true);
				list.UpdateStocksByBatch(false);
				Stock_BS.DataSource = list;
				//Cache.Instance.Save(typeof(StockList), Stock_BS.DataSource as StockList);
				PgMng.Grow();

				HideComponentes(Stock_TP);

				ControlsMng.UpdateBinding(Stock_BS);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void AddComponentAction() { }
		protected virtual void DeleteComponentAction() { }

        protected virtual void ShowProductAction() { }

		#endregion

		#region Buttons

		private void AddComponent_TI_Click(object sender, EventArgs e) { AddComponentAction(); }
		private void DeleteComponent_TI_Click(object sender, EventArgs e) { DeleteComponentAction(); }

		#endregion

		#region Events

		private void Ficha_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Parts_TC.SelectedTab == Partidas_TP) { LoadPartidasAction(); }
            else if (Parts_TC.SelectedTab == Stock_TP) { LoadPartidasAction(); }
		}

		private void Kit_CkB_CheckedChanged(object sender, EventArgs e)
		{
			HideComponentes(Components_TP);
		}

		private void Partidas_DGW_SelectionChanged(object sender, EventArgs e)
		{
			LoadStockAction();
		}
        
        private void Components_DGW_DoubleClick(object sender, EventArgs e)
        {
            ShowProductAction();
        }

		#endregion
	}
}