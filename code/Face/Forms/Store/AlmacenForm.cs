using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.CslaEx;

using moleQule;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class AlmacenForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public override Type EntityType { get { return typeof(Almacen); } }

        public virtual Almacen Entity { get { return null; } set { } }
        public virtual StoreInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

		public AlmacenForm()
			: this(null) {}

        public AlmacenForm(Form parent)
            : this(-1, parent) {}

		public AlmacenForm(long oid, Form parent)
			: base(oid, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			//Productos_DGW
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			ProductoObservaciones.Tag = 1;

			cols.Add(ProductoObservaciones);

			ControlsMng.MaximizeColumns(Productos_DGW, cols);
			Productos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			//Partidas_DGW
			cols.Clear();
			Ubicacion.Tag = 1;

			cols.Add(Ubicacion);

			ControlsMng.MaximizeColumns(Partidas_DGW, cols);
			Partidas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			//Stock_DGW
			cols.Clear();
			ColumnaObservaciones.Tag = 1;

			cols.Add(ColumnaObservaciones);

			ControlsMng.MaximizeColumns(Stock_DGW, cols);

			//Resumen_DGW
			cols.Clear();
			CResumenProducto.Tag = 1;

			cols.Add(CResumenProducto);

			ControlsMng.MaximizeColumns(Resumen_DGW, cols);
		}

		public override void FormatControls()
        {
            if (Stock_DGW == null) return;

            base.MaximizeForm(new Size(1200,0));
            base.FormatControls();			

            HideComponentes(TabProductos);

            Partidas_DGW.Columns[ColumnaPEStockKilos.Name].DefaultCellStyle.BackColor = Color.DarkGray;
        }

        protected virtual void HideComponentes(TabPage page) { }

        #endregion

        #region Source
		
		protected virtual void ActualizaBindings()
		{
			Datos_Partidas.ResetBindings(false);
			Partidas_DGW.Refresh();

			Datos_Stock.ResetBindings(true);
			Stock_DGW.Refresh();
		}

		protected virtual void LoadPartidas(ProductInfo producto) { }
		protected virtual void LoadProductos()
		{
			if (Datos_Productos.DataSource as ProductList != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				PgMng.Grow();

				Datos_Productos.RaiseListChangedEvents = false;
				Datos_Productos.DataSource = ProductList.GetSortedList(ProductList.GetListByAlmacen(EntityInfo.Oid, false),
																		ProductoNombre.DataPropertyName,
																		ListSortDirection.Ascending);
				Datos_Productos.RaiseListChangedEvents = true;
				PgMng.Grow();

				Datos_Productos.ResetBindings(false);
			}
			finally
			{
				PgMng.FillUp();
			}
		}
		protected virtual void LoadStock(ProductInfo producto) { }

        public override void RefreshSecondaryData()
        {
        }

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		protected virtual void GastosTotales() { }

		protected virtual void Beneficios()
		{
			if (EntityInfo == null) return;

			OutputInvoiceLineList list = OutputInvoiceLineList.GetListByExpediente(EntityInfo.Oid, false);

			decimal suma = 0.0m;
			foreach (OutputInvoiceLineInfo c in list)
			{
				suma += (c.Precio - c.Gastos) * c.CantidadKilos;
			}

			//BeneficioReal_NTB.Text = suma.ToString("N2");
		}

		protected virtual void UpdateStocks() {}

		#endregion

        #region Actions

        protected virtual void PrintStockAction() {}

		protected virtual void SelectProductoAction(ProductInfo producto) {}
		protected virtual void SelectStockAction(ProductInfo producto) {}

		protected virtual void EditStockAction() {}
		protected virtual void NuevoStockAction() { }
		protected virtual void RemoveStockAction() { }
		
        #endregion

        #region Buttons

        private void PrintStock_BT_Click(object sender, EventArgs e) { PrintStockAction(); }

		private void AddStock_TI_Click(object sender, EventArgs e) { NuevoStockAction(); }

		private void EditStock_TI_Click(object sender, EventArgs e) { EditStockAction(); }

		private void DeleteStock_TI_Click(object sender, EventArgs e) { RemoveStockAction(); }
       
        #endregion

        #region Events
		
		private void Ficha_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Ficha.SelectedTab == TabProductos)
			{
				LoadProductos();
			}
			if (Ficha.SelectedTab == TabStock)
			{
				LoadStock(Datos_Productos.Current as ProductInfo);
			}

			HideComponentes(Ficha.SelectedTab);
		}

		private void Productos_DGW_SelectionChanged(object sender, EventArgs e)
		{
			if (Datos_Productos.RaiseListChangedEvents == false) return;
			if (Datos_Productos.Current == null) return;

			SelectProductoAction(Datos_Productos.Current as ProductInfo);
		}
		
        #endregion
    }
}

