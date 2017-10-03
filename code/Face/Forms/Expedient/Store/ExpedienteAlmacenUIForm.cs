using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Face;
using moleQule.Face.Controls;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Face.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpedienteAlmacenUIForm : ExpedienteAlmacenForm
    {

        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 7; } }
        public const string ID = "ExpedienteAlmacenUIForm";

        /// <summary>
        /// Se trata de la Expedient actual y que se va a editar.
        /// </summary>
        protected Expedient _entity;

        protected NavieraInfo _naviera;
        protected DespachanteInfo _despachante;
        protected TransporterInfo _trans_origen;
        protected TransporterInfo _trans_destino;
        protected ProveedorInfo _proveedor;

        protected decimal _precio_naviera = 0;
        protected decimal _precio_puerto = 0;

        public override Expedient Entity { get { return _entity; } set { _entity = value; } }
        public override ExpedientInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }
        public override ExpedientInfo EntityInfoNoChilds { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        public ExpedienteAlmacenUIForm() 
			: this(-1) {}

		public ExpedienteAlmacenUIForm(long oid)
            : this(oid, null) {}

        public ExpedienteAlmacenUIForm(long oid, Form parent) 
            : base(oid, true, parent)
        {
            InitializeComponent();
            //if (_entity != null) _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Expedient temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex.Message);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            if (Stock_DGW == null) return;

            base.FormatControls();

            int index = StockTitulo_LB.Text.IndexOf("(") - 1;

            StockTitulo_LB.Text = (index > 0 ? StockTitulo_LB.Text.Substring(0, index) : StockTitulo_LB.Text) + " (" + _entity.Partidas.Count + ")";
            
        }

		protected override void SetGastosFormat()
		{
			GastoTOringen_NTB.ReadOnly = (_entity.OidFacturaTor != 0);
			GastoNaviera_NTB.ReadOnly = (_entity.OidFacturaNav != 0);
			GastoDespachante_NTB.ReadOnly = (_entity.OidFacturaDes != 0);
			GastoTDestino_NTB.ReadOnly = (_entity.OidFacturaTde != 0);

			GastoTOringen_NTB.ForeColor = (_entity.OidFacturaTor != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoNaviera_NTB.ForeColor = (_entity.OidFacturaNav != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoDespachante_NTB.ForeColor = (_entity.OidFacturaDes != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoTDestino_NTB.ForeColor = (_entity.OidFacturaTde != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
		}

		#endregion

		#region Source

		protected override void UpdateExpensesList()
		{
			Expenses_BS.DataSource = Expenses.GetListAgrupada(_entity.Gastos.GetSubList(ECategoriaGasto.OtrosExpediente));

			ExpensesInvoices_BS.DataSource = _entity.Facturas;

			//SetGastosFormat();
		}

		protected override void HideComponentes(TabPage page)
		{
			if (General_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Partidas_DGW.Rows)
                    if ((row.DataBoundItem as Batch).IsKitComponent)
						row.Visible = false;
			}
			else if (Stock_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Stock_DGW.Rows)
					if ((row.DataBoundItem as Stock).IsKitComponent)
						row.Visible = false;
			}
			else if (Benefits_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Benefits_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Benefits_DGW.Rows)
                    if ((row.DataBoundItem as Batch).IsKitComponent)
						row.Visible = false;
			}
		}

		protected override void LoadIncomes() { LoadIncomes(_entity.Oid); }

		protected override void LoadStock()
		{
			if (_entity.Stocks.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(Stock), true, false);
                    string stock_warning = _entity.GetStockWarning();
                    if (stock_warning != string.Empty)
                        PgMng.ShowInfoException(stock_warning);
					PgMng.Grow();
				}
				finally
				{
					PgMng.FillUp();
                }
            }
            SelectStockAction(Datos_Productos.Current as ProductInfo);
		}

		protected override void RefreshMainData()
		{
			if (_entity == null) return;

			Datos.DataSource = _entity;
			PgMng.Grow(string.Empty, "Datos");

			Stock_BS.DataSource = _entity.Stocks;
			PgMng.Grow(string.Empty, "Stocks");

			Batchs_BS.RaiseListChangedEvents = false;
			Batchs_BS.DataSource = _entity.Partidas;
			Batchs_BS.RaiseListChangedEvents = true;
			PgMng.Grow(string.Empty, "Partidas");

			Datos_Gastos.DataSource = _entity.Gastos;
			UpdateExpensesList();
			PgMng.Grow(string.Empty, "Gastos");

			ExpensesInvoices_BS.DataSource = _entity.Facturas;
			PgMng.Grow(string.Empty, "Facturas de Acreedores");

			EstimarDespachante_CkB.Checked = _entity.EstimarDespachante;
			EstimarNaviera_CkB.Checked = _entity.EstimarNaviera;
			EstimarTOrigen_CkB.Checked = _entity.EstimarTOrigen;
			EstimarTDestino_CkB.Checked = _entity.EstimarTDestino;

			/*if (_entity.Teus20)
				Teus20_RB.Checked = _entity.Teus20;
			else
				Teus40_RB.Checked = _entity.Teus40;

			Datos_Fomento.DataSource = _entity.ExpedientesFomento;*/
		}

		public override void RefreshSecondaryData()
		{
			Datos_Productos.DataSource = ProductList.GetSortedList(ProductList.GetListByExpediente(_entity.Oid, false),
																		"Nombre",
																		ListSortDirection.Ascending);
			PgMng.Grow(string.Empty, "Productos");

			base.RefreshSecondaryData();
		}

        #endregion

		#region Business Methods

		protected override void AddFacturaGastos(InputInvoiceInfo fac) { AddFacturaGastos(_entity, fac); }

		protected override void CalculateTotales()
		{
			base.CalculateTotales();

			BeIngresosEstimado_NTB.Text = _entity.IngresosEstimados().ToString("C2");
		}

		protected void ChangeEstadoFomento(DataGridViewRow row, moleQule.Base.EEstado estado)
		{
			LineaFomento item = row.DataBoundItem as LineaFomento;
			item.EEstado = estado;
		}

		public void CheckPrecioNaviera()
		{
			if ((_precio_puerto != _entity.GNavTotal) || (_precio_naviera != _entity.GNavTotal))
			{
				PgMng.ShowInfoException("El precio introducido no coincide con los precios almacenados:" + Environment.NewLine +
								"   Precio del Puerto: " + _precio_puerto.ToString("C2") + Environment.NewLine +
								"   Precio de la Naviera: " + _precio_naviera.ToString("C2") + Environment.NewLine +
								"   Precio del Expedient: " + _entity.GNavTotal.ToString("C2"));
				return;
			}
		}

		protected override void EditFacturaGastos(InputInvoiceInfo fac) { EditFacturaGastos(_entity, fac); }
		
		protected override void RemoveFacturaGastos(InputInvoiceInfo fac) { RemoveFacturaGastos(_entity, fac); }

		protected override void RemoveStock(Stock stock) { RemoveStock(_entity, stock); }

        #endregion

        #region Actions

		protected override void SaveAction()
		{
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

			if (_action_result == DialogResult.OK)
			{
				//Actualizamos la cache
				ExpedienteList cache = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;
				if (cache != null) cache.Change(_entity.Oid, _entity.GetInfo(false), true);
			}
		}

		protected override void SelectClientAction()
		{
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _entity.NombreCliente = ((ClienteInfo)form.Selected).Nombre;
            }
		}

		protected override void SelectSupplierAction()
		{
			ProveedorList list = ProveedorList.GetList(moleQule.Base.EEstado.Active, ETipoAcreedor.Proveedor, false);
			SupplierSelectForm form = new SupplierSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_proveedor = (ProveedorInfo)form.Selected;
				_entity.OidProveedor = _proveedor.Oid;
				_entity.Proveedor = _proveedor.Nombre;

				_entity.SetCode(ETipoAcreedor.Proveedor);
			}
		}

		protected override void SelectProductAction(ProductInfo producto)
		{
			if (producto == null) return;

			FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
			Batchs_BS.DataSource = _entity.Partidas.GetSubList(criteria);
			Batchs_BS.ResetBindings(true);

			SelectStockAction(producto);
		}

        protected override void SelectProductNameAction()
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ProductInfo producto = (ProductInfo)form.Selected;
                _entity.TipoMercancia = producto.Nombre;
            }
        }

		protected override void SelectStockAction(ProductInfo producto)
		{
			if (_entity.ETipoExpediente == moleQule.Store.Structs.ETipoExpediente.Almacen)
			{
				if (producto == null) return;

				FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
				Stock_BS.DataSource = _entity.Stocks.GetSubList(criteria);
			}
			else
				Stock_BS.DataSource = _entity.Stocks;

			Stock_BS.ResetBindings(true);
		}

		protected override void ReparteFGastoAction() { ReparteGasto(_entity); }

		protected override void AddStockAction()
		{
			if (_entity.Partidas.Count == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_PRODUCTS_ASSOC);
				return;
			}

			AddStockInputForm form = new AddStockInputForm(_entity);
			form.ShowDialog(this);

			Stock_BS.ResetBindings(false);
		}

		protected override void EditStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock s = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if ((s.OidAlbaran != 0) ||
				((s.OidAlbaran == 0) && (s.Observaciones == Library.Store.Resources.Defaults.STOCK_INICIAL)))
			{
				PgMng.ShowInfoException(Resources.Messages.STOCK_FACTURADO);
				return;
			}

			EditStockActionForm form = new EditStockActionForm(s, _entity);
			form.ShowDialog(this);

			Stock_BS.ResetBindings(false);
		}

		protected override void DeleteStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock st = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if ((st.OidAlbaran != 0) || (st.Inicial)) return;

			RemoveStock(st);

			Stock_BS.ResetBindings(false);
		}

		protected override void PrintStockAction()
		{
			Library.Store.ReportFilter filter = new Library.Store.ReportFilter();
			ReportFormat format = new ReportFormat();

			format.Vista = EReportVista.Detallado;

			filter.FechaIni = DateTime.MinValue;
			filter.FechaFin = DateTime.MaxValue;
			filter.SoloMermas = false;
			filter.SoloStock = false;

			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema);
			MovimientosStockListPorExpedienteRpt rpt = reportMng.GetMovimientosStockListAgrupado(_entity.GetInfo(true), null, null, filter, format, false);

			ShowReport(rpt);
		}

        protected override void PrintMermaAction()
        {
            if (Stock_DGW.CurrentRow == null) return;
            if (Stock_DGW.CurrentRow.Index < 0) return;
            if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

            Stock s = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

            if (s.ETipoStock != ETipoStock.Merma) return;
            ReportFormat format = new ReportFormat();

            format.Vista = EReportVista.Detallado;

            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema);
            MermaStockRpt rpt = reportMng.GetMermaDetailReport(s.GetInfo());

            ShowReport(rpt);
        }

		protected void SetGastoPrincipalAction(InputInvoiceInfo item)
		{
			_entity.SetGasto(item);
            _entity.UpdateGastosPartidas(true);
		}

        #endregion

        #region Buttons

        #endregion

        #region Events
		
		private void ExpedienteAlmacenUIForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_action_result != DialogResult.OK)
				if (DialogResult.No == ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.CANCEL_CONFIRM))
				{
					e.Cancel = true;
					return;
				}
		}
 
        #endregion

    }
}

