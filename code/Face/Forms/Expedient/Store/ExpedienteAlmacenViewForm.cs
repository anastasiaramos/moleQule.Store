using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.CslaEx;
using moleQule;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Face;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ExpedienteAlmacenViewForm : ExpedienteAlmacenForm
	{

        #region Business Methods

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        private ExpedientInfo _entity;

        public override ExpedientInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public ExpedienteAlmacenViewForm(long oid)
			: this(oid, null) { }

		public ExpedienteAlmacenViewForm(long oid, Form parent)
            : base(oid,true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = String.Format(Resources.Labels.EXPEDIENT_TITLE, _entity.Codigo);
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = ExpedientInfo.Get(oid, false);
			PgMng.Grow();

            _entity.LoadChilds(typeof(Batch), true, true);
			PgMng.Grow(string.Empty, "Partidas");

            _entity.LoadChilds(typeof(Expense), true, true);
			PgMng.Grow(string.Empty, "Gastos");
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
			HideAction(molAction.Cancel);
			SetReadOnlyControls(this.Controls);

			base.FormatControls();
        }

		protected override void SetGastosFormat()
		{
			GastoTOringen_NTB.ForeColor = (_entity.OidFacturaTor != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoNaviera_NTB.ForeColor = (_entity.OidFacturaNav != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoDespachante_NTB.ForeColor = (_entity.OidFacturaDes != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoTDestino_NTB.ForeColor = (_entity.OidFacturaTde != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
		}

		#endregion

		#region Source

		protected override void HideComponentes(TabPage page)
		{
			if (General_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Partidas_DGW.Rows)
					if ((row.DataBoundItem as BatchInfo).IsKitComponent)
						row.Visible = false;
			}
			else if (Stock_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Stock_DGW.Rows)
					if ((row.DataBoundItem as StockInfo).IsKitComponent)
						row.Visible = false;
			}
			else if (Benefits_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Benefits_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Benefits_DGW.Rows)
					if ((row.DataBoundItem as BatchInfo).IsKitComponent)
						row.Visible = false;
			}
		}

		protected override void LoadIncomes() { LoadIncomes(_entity.Oid); }

		protected override void LoadStock()
		{
			if (_entity.Stocks == null || _entity.Stocks.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(Stock), true, true);
					PgMng.Grow();
					SelectStockAction(Datos_Productos.Current as ProductInfo);
				}
				finally
				{
					PgMng.FillUp();
				}
			}
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


            /*Datos.DataSource = _entity;
            Datos_Stock.DataSource = _entity.Stocks;

            switch (_entity.ETipoExpediente)
            {
                case moleQule.Store.Structs.ETipoExpediente.Ganado:
                    Datos_Cabeza.RaiseListChangedEvents = false;
                    Datos_Cabeza.DataSource = _entity.Cabezas;
                    Datos_Cabeza.RaiseListChangedEvents = true;
                    break;

                case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
                    Datos_Maquinaria.RaiseListChangedEvents = false;
                    Datos_Maquinaria.DataSource = _entity.Maquinarias;
                    Datos_Maquinaria.RaiseListChangedEvents = false;
                    break;
            }

			Datos_Partidas.RaiseListChangedEvents = false;
			Datos_Partidas.DataSource = _entity.Partidas;
			Datos_Partidas.RaiseListChangedEvents = true;
			PgMng.Grow(string.Empty, "Partidas");

            Datos_FacturasR.DataSource = InputInvoiceList.GetListByExpediente(_entity.Oid, false);
            Bar.Grow();

			ActualizaListaGastos();

            Datos_Gastos.DataSource = _entity.Gastos;

            Ayuda_CkB.Checked = _entity.Ayuda;
			EstimarDespachante_CkB.Checked = _entity.EstimarDespachante;
			EstimarNaviera_CkB.Checked = _entity.EstimarNaviera;
			EstimarTOrigen_CkB.Checked = _entity.EstimarTOrigen;
			EstimarTDestino_CkB.Checked = _entity.EstimarTDestino;

            Datos_Fomento.DataSource = _entity.ExpedientesFomento;*/

            base.RefreshMainData();
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

		protected override void UpdateExpensesList()
		{
			InvoicedExpenses_BS.DataSource = _entity.Gastos.GetSubListByTipo(ECategoriaGasto.GeneralesExpediente);
			Expenses_BS.DataSource = _entity.Gastos.GetSubListByTipo(ECategoriaGasto.OtrosExpediente);

			SetGastosFormat();
		}

		protected override void CalculateTotales()
		{
			base.CalculateTotales();

			BeIngresosEstimado_NTB.Text = _entity.IngresosEstimados().ToString("C2");
		}

		#endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

		protected override void SelectProductAction(ProductInfo producto)
		{
			if (producto == null) return;

			FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
			Batchs_BS.DataSource = _entity.Partidas.GetSubList(criteria);
			Batchs_BS.ResetBindings(true);

			SelectStockAction(producto);
		}

		protected override void DefaultFGastoAction() { ViewFGastoAction(); }

		protected override void SelectStockAction(ProductInfo producto)
		{
			if (producto == null) return;

			FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
			Stock_BS.DataSource = _entity.Stocks.GetSubList(criteria);
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
            MovimientosStockListPorExpedienteRpt rpt = reportMng.GetMovimientosStockListAgrupado(_entity, null, null, filter, format, false);

			ShowReport(rpt);
        }

        protected override void PrintMermaAction()
        {
            if (Stock_DGW.CurrentRow == null) return;
            if (Stock_DGW.CurrentRow.Index < 0) return;
            if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

            StockInfo s = (StockInfo)Stock_DGW.CurrentRow.DataBoundItem;

            if (s.ETipoStock != ETipoStock.Merma) return;
            ReportFormat format = new ReportFormat();

            format.Vista = EReportVista.Detallado;

            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema);
            MermaStockRpt rpt = reportMng.GetMermaDetailReport(s);

            ShowReport(rpt);
        }

		#endregion

        #region Events

        #endregion

	}
}

