using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Library.Store.Reports.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class MovsStockActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

		public const string ID = "MovsStockActionForm";
		public static Type Type { get { return typeof(MovsStockActionForm); } }

		ProductInfo _producto;
		ExpedientInfo _expediente;
		ExpedientInfo _expediente_ini;
		ExpedientInfo _expediente_fin;
		SerieInfo _serie;
		Library.Store.ReportFilter _report_filter = new Library.Store.ReportFilter();
		ReportFormat _report_format = new ReportFormat();

        #endregion

        #region Factory Methods

        public MovsStockActionForm()
            : this(null) { }

		public MovsStockActionForm(Form parent)
			: base(true, parent)
		{
			InitializeComponent();
			SetFormData();
		}

        #endregion

        #region Source

        public override void RefreshSecondaryData()
        {
			Datos_TiposExp.DataSource = moleQule.Store.Structs.EnumText<moleQule.Store.Structs.ETipoExpediente>.GetList(false);
			TipoExpediente_CB.SelectedItem = (long)moleQule.Store.Structs.ETipoExpediente.Todos;
			PgMng.Grow();

			ETipoStock[] list = { ETipoStock.Todos, ETipoStock.Compra, ETipoStock.Merma, ETipoStock.MovimientoEntrada, ETipoStock.MovimientoSalida, ETipoStock.Venta };
			Datos_TiposMov.DataSource = moleQule.Store.Structs.EnumText<ETipoStock>.GetList(list, false);
			TipoMovimiento_CB.SelectedItem = (long)ETipoStock.Todos;
			PgMng.Grow();
        }

        #endregion

		#region Business Methods

		private string GetFilterValues()
		{
			string filtro = string.Empty;

			if (!TodosSerie_CkB.Checked)
				filtro += "Serie " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _serie.Nombre + "; ";

			if (!TodosProducto_CkB.Checked)
				filtro += "Producto " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _producto.Nombre + "; ";

			if (!AllCustomCode_CkB.Checked)
				filtro += "Código Aduanero " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _producto.CodigoAduanero + "; ";

			if (Seleccion_RB.Checked)
				filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _expediente.Codigo + "; ";
			else if (Rango_RB.Checked)
			{
				filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + _expediente_ini.Codigo + "; ";
				filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + _expediente_fin.Codigo + "; ";
			}
			else
				filtro += "Tipo Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoExpediente_CB.SelectedItem as ComboBoxSource).Texto + "; ";

			if (Stock_CkB.Checked)
				filtro += "Sólo expedientes con stock; ";

			if ((ETipoStock)(long)TipoMovimiento_CB.SelectedValue != ETipoStock.Todos)
				filtro += "Tipo de Movimiento " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + (TipoMovimiento_CB.SelectedItem as ComboBoxSource).Texto + "; ";
			
			if (FInicial_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

            if (AveragePrice_RB.Checked)
                filtro += "Precio Compra " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + "Precio medio;";
            else if (LastPurchasePrice_RB.Checked)
                filtro += "Precio Compra " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + "Ultima compra;";

			return filtro;
		}

		#endregion

        #region Actions

        protected override void PrintAction()
        {
			try
			{
				if (Seleccion_RB.Checked && (_expediente == null)) return;
				if (Rango_RB.Checked && ((_expediente_ini == null) || (_expediente_fin == null))) return;
				if (!TodosProducto_CkB.Checked && (_producto == null)) return;
				if (!AllCustomCode_CkB.Checked && (_producto == null)) return;
				if (!TodosSerie_CkB.Checked && (_serie == null)) return;

				PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

				Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
				{
					Expedient = Seleccion_RB.Checked ? _expediente : null,
					TipoExpediente = (moleQule.Store.Structs.ETipoExpediente)(long)TipoExpediente_CB.SelectedValue,
					TipoStock = (ETipoStock)(long)TipoMovimiento_CB.SelectedValue,
					Producto = _producto,
					Serie = TodosSerie_CkB.Checked ? null : _serie,
					FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue,
					FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue
				};

				if (TodosProducto_CkB.Checked)
					if (conditions.Producto != null)
						conditions.Producto.Oid = 0;

				if (AllCustomCode_CkB.Checked)
					if (conditions.Producto != null)
						conditions.Producto.CodigoAduanero = string.Empty;

				string filtro = GetFilterValues();
				PgMng.Grow();

				ExpedientInfo expediente_ini = Rango_RB.Checked ? _expediente_ini : null;
				ExpedientInfo expediente_fin = Rango_RB.Checked ? _expediente_fin : null;

				_report_format.Vista = (Detallado_RB.Checked) ? EReportVista.Detallado : EReportVista.Resumido;

				_report_filter.SoloMermas = ((ETipoStock)(long)TipoMovimiento_CB.SelectedValue == ETipoStock.Merma);
				_report_filter.SoloStock = Stock_CkB.Checked;
				_report_filter.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
				_report_filter.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

                if (Standard_RB.Checked)
                    ShowStandardReport(conditions);
                else
                    ShowStoreFileReport(conditions);

				_action_result = DialogResult.Ignore;
			}
			catch (Exception ex)
			{
				PgMng.FillUp();
				throw ex;
			}
        }

        protected void ShowStandardReport(Library.Store.QueryConditions conditions)
        {
            ExpedientInfo expediente_ini = Rango_RB.Checked ? _expediente_ini : null;
			ExpedientInfo expediente_fin = Rango_RB.Checked ? _expediente_fin : null;

            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, GetFilterValues());

            if (PorExpediente_RB.Checked)
            {
                MovimientosStockListPorExpedienteRpt rpt = null;

                if (TodosExpediente_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedienteList.GetList(conditions.TipoExpediente, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format);
                else if (Seleccion_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedientInfo.Get(conditions.Expedient.Oid, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format, false);
                else if (Rango_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedienteList.GetListByRango(expediente_ini, expediente_fin, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format);

                PgMng.FillUp();

                ShowReport(rpt);
            }
            else if (PorProducto_RB.Checked)
            {
                StockList stocks = null;

                if (TodosExpediente_RB.Checked)
                    stocks = StockList.GetReportList(conditions, null, null, _report_filter.SoloStock, false);
                else if (Seleccion_RB.Checked)
                    stocks = StockList.GetReportList(conditions, null, null, _report_filter.SoloStock, false);
                else if (Rango_RB.Checked)
                    stocks = StockList.GetReportList(conditions, expediente_ini, expediente_fin, _report_filter.SoloStock, false);

                StockLineListRpt rpt = reportMng.GetStockLineList(stocks, _report_filter, _report_format);

                PgMng.FillUp();

                ShowReport(rpt);
            }
        }

        protected void ShowStoreFileReport(Library.Store.QueryConditions conditions)
        {
            ExpedientInfo expediente_ini = Rango_RB.Checked ? _expediente_ini : null;
            ExpedientInfo expediente_fin = Rango_RB.Checked ? _expediente_fin : null;

            if (PorExpediente_RB.Checked)
            {
                ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, GetFilterValues());

                MovimientosStockListPorExpedienteRpt rpt = null;

                if (TodosExpediente_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedienteList.GetList(conditions.TipoExpediente, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format);
                else if (Seleccion_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedientInfo.Get(conditions.Expedient.Oid, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format, false);
                else if (Rango_RB.Checked)
                    rpt = reportMng.GetMovimientosStockListAgrupado(ExpedienteList.GetListByRango(expediente_ini, expediente_fin, false), conditions.Producto, SerieInfo.New(conditions.Serie.Oid), _report_filter, _report_format);

                PgMng.FillUp();

                ShowReport(rpt);
            }
            else if (PorProducto_RB.Checked)
            {
                StoreReportMng reportMng = new StoreReportMng(AppContext.ActiveSchema, this.Text, GetFilterValues());

                StockList stocks = null;

                if (TodosExpediente_RB.Checked)
                    stocks = StockList.GetReportList(conditions, null, null, _report_filter.SoloStock, false);
                else if (Seleccion_RB.Checked)
                    stocks = StockList.GetReportList(conditions, null, null, _report_filter.SoloStock, false);
                else if (Rango_RB.Checked)
                    stocks = StockList.GetReportList(conditions, expediente_ini, expediente_fin, _report_filter.SoloStock, false);

                stocks.FillPurchasePrices();

                string stock_purchase_price_type = string.Empty;

                if (LastPurchasePrice_RB.Checked) stock_purchase_price_type = "Last";
                else if (AveragePrice_RB.Checked) stock_purchase_price_type = "Average";

                StoreFileRpt rpt = reportMng.GetStoreFile(stocks
                                                            ,_report_filter
                                                            ,_report_format
                                                            ,StoreFileKg_RB.Checked
                                                            ,stock_purchase_price_type);

                PgMng.FillUp();

                ShowReport(rpt);
            }
        }

        #endregion

        #region Buttons

		private void CustomCode_BT_Click(object sender, EventArgs e)
		{
			ProductList list = ProductList.GetByCustomCodeList(string.Empty, true, false);
			ProductSelectForm form = new ProductSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo item = form.Selected as ProductInfo;
				if (_producto == null)
				{
					_producto = item;
					_producto.Oid = 0;
				}
				else
					_producto.CodigoAduanero = item.CodigoAduanero;

				CustomCode_TB.Text = _producto.CodigoAduanero;
			}
		}

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				ProductInfo item = form.Selected as ProductInfo;
				if (_producto == null)
				{
					_producto = item;
					_producto.CodigoAduanero = string.Empty;
				}
				else
					_producto.Oid = item.Oid;

                Producto_TB.Text = _producto.Nombre;
            }
        }

		private void Serie_BT_Click(object sender, EventArgs e)
		{
			SerieSelectForm form = new SerieSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_serie = form.Selected as SerieInfo;
				Serie_TB.Text = _serie.Nombre;
			}
		}

		private void Expediente_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente = form.Selected as ExpedientInfo;
				Expediente_TB.Text = _expediente.Codigo;
			}
		}

		private void ExpedienteFin_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente_fin = form.Selected as ExpedientInfo;
				ExpedienteFin_TB.Text = _expediente_fin.Codigo;
			}
		}

		private void ExpedienteIni_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente_ini = form.Selected as ExpedientInfo;
				ExpedienteIni_TB.Text = _expediente_ini.Codigo;
			}
		}

		private void TodosExpediente_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = false;
			RangoExp_GB.Enabled = false;
			TipoExpediente_CB.Enabled = true;
		}

		private void Seleccion_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = true;
			RangoExp_GB.Enabled = false;
			TipoExpediente_CB.Enabled = false;
		}

		private void Rango_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = false;
			RangoExp_GB.Enabled = true;
			TipoExpediente_CB.Enabled = false;
		}

        #endregion

        #region Events

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
        }

		private void AllCustomCode_CkB_CheckedChanged(object sender, EventArgs e)
		{
			CustomCode_BT.Enabled = !AllCustomCode_CkB.Checked;
		}

		private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
		{
			Serie_BT.Enabled = !TodosSerie_CkB.Checked;
		}

        #endregion
    }
}