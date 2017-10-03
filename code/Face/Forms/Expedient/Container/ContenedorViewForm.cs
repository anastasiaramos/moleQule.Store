using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ContenedorViewForm : ContenedorForm
	{
        #region Business Methods

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        private ExpedientInfo _entity;

        protected LivestockBookLineList _livestock_lines;

        public override ExpedientInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public ContenedorViewForm(long oid)
			: this(oid, moleQule.Store.Structs.ETipoExpediente.Todos) { }

        public ContenedorViewForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo) 
            : this(oid, tipo, null) {}

		public ContenedorViewForm(long oid, Form parent)
			: this(oid, moleQule.Store.Structs.ETipoExpediente.Todos, parent) {}

        public ContenedorViewForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo, Form parent)
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
            SetFormData();

            switch (tipo)
            {
                case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
                    this.Text = String.Format(Resources.Labels.CONTAINER_TITLE, _entity.Codigo);
                    break;

                default:
                    this.Text = String.Format(Resources.Labels.EXPEDIENT_TITLE, _entity.Codigo);
                    break;
            }

            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = ExpedientInfo.Get(oid, false);
			PgMng.Grow();

            _entity.LoadChilds(typeof(Batch), true, true);
			PgMng.Grow(string.Empty, "Partidas");

            try
            {
                _entity.LoadChilds(typeof(Expense), true, true);
                PgMng.Grow(string.Empty, "Gastos");
            }
            catch
            {
                PgMng.ShowWarningException(Resources.Messages.DUPLICATED_EXPENSE_LINE);
            }
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

		protected override void SetFomentoFormat()
		{
			foreach (DataGridViewRow row in Fomento_DGW.Rows)
			{
				if (row.IsNewRow) return;

				LineaFomentoInfo item = (LineaFomentoInfo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

				SetRowFormat(row, "FechaSolicitudLFomento", item.FechaSolicitud);
			}
		}

		protected override void SetExpedienteREAFormat()
		{
			foreach (DataGridViewRow row in ExpedienteREA_DGW.Rows)
			{
				if (row.IsNewRow) return;

				ExpedienteREAInfo item = (ExpedienteREAInfo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

				SetRowFormat(row, "FechaDespachoREA", item.Fecha);
				SetRowFormat(row, "FechaCobroRea", item.FechaCobro);
			}
		}

		#endregion

		#region Source

		protected override void LoadAyudas()
		{
			if (_entity.ExpedientesFomento == null || _entity.ExpedientesFomento.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(REAExpedient), true, true);
					Datos_REA.DataSource = _entity.ExpedientesREA;
					Datos_REA.ResetBindings(true);
					PgMng.Grow();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
            else
            {
                Datos_REA.DataSource = _entity.ExpedientesREA;
                Datos_REA.ResetBindings(false);
            }

            AyudaTotal_NTB.Text = _entity.AyudaExpediente.ToString("C2");
            CalculateBeneficios();
            SetExpedienteREAFormat();
		}

		protected override void LoadLibroGanadero()
		{
			try
			{
				if (_livestock_lines != null) return;

				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

                _livestock_lines = LivestockBookLineList.GetByExpedienteList(_entity.Oid, false);

				LivestockBook_BS.DataSource = _livestock_lines;
				PgMng.Grow(string.Empty);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected override void LoadCostes() { LoadCostes(_entity.Oid); }

		protected override void LoadFomento()
		{
			if (_entity.ExpedientesFomento == null || _entity.ExpedientesFomento.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(LineaFomento), true, true);
					Datos_Fomento.DataSource = _entity.ExpedientesFomento;
					Datos_Fomento.ResetBindings(true);
					PgMng.Grow();
				}
				finally
				{
					PgMng.FillUp();
				}
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
            Datos.DataSource = _entity;
            Stock_BS.DataSource = _entity.Stocks;

			switch (_entity.ETipoExpediente)
			{
				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					Datos_Maquinaria.RaiseListChangedEvents = false;
					Datos_Maquinaria.DataSource = _entity.Maquinarias;
					Datos_Maquinaria.RaiseListChangedEvents = false;
					break;
			}

			Batchs_BS.RaiseListChangedEvents = false;
			Batchs_BS.DataSource = _entity.Partidas;
			Batchs_BS.RaiseListChangedEvents = true;
			PgMng.Grow(string.Empty, "Partidas");

            ExpensesInvoices_BS.DataSource = InputInvoiceList.GetListByExpediente(_entity.Oid, false);
            Bar.Grow();

			UpdateExpensesList();

            Datos_Gastos.DataSource = _entity.Gastos;

			Ayuda_CkB.Checked = _entity.Ayuda;
			EstimarDespachante_CkB.Checked = _entity.EstimarDespachante;
			EstimarNaviera_CkB.Checked = _entity.EstimarNaviera;
			EstimarTOrigen_CkB.Checked = _entity.EstimarTOrigen;
			EstimarTDestino_CkB.Checked = _entity.EstimarTDestino;

			Datos_Fomento.DataSource = _entity.ExpedientesFomento;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			base.RefreshSecondaryData();
		}

        protected override void SetDependentControlSource(string controlName)
        {
            switch (controlName)
            {
                case "teus20RadioButton":
                    {
                        Teus20_RB.Checked = _entity.Teus20;
                    } break;
                case "teus40RadioButton":
                    {
                        Teus40_RB.Checked = _entity.Teus40;
                    } break;
            }
        }

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

        #endregion

        #region Business Methods

		protected override void UpdateExpensesList()
		{
			InvoicedExpenses_BS.DataSource = _entity.Gastos.GetSubListByTipo(ECategoriaGasto.GeneralesExpediente);
			Expenses_BS.DataSource = _entity.Gastos.GetSubListByTipo(ECategoriaGasto.OtrosExpediente);

			SetGastosFormat();
		}

		protected override decimal GetEstimatedIncome()
		{
			return _entity.IngresosEstimados() + AyudaTotal_NTB.DecimalValue + AyudaMermas_NTB.DecimalValue;
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