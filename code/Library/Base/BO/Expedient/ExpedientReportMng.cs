using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    public class ExpedientReportMng : BaseReportMng
    {
		#region Factory Methods

		public ExpedientReportMng() { }

		public ExpedientReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public ExpedientReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

		#region Style

		private void FormatReport(MovimientosStockListPorExpedienteRpt rpt, ReportFilter filter, ReportFormat format)
		{
			switch (format.Vista)
			{
				case EReportVista.Detallado:
					{
						rpt.HeaderDetallado.SectionFormat.EnableSuppress = false;
						rpt.HeaderResumido.SectionFormat.EnableSuppress = true;
						rpt.FooterExpediente.SectionFormat.EnableNewPageAfter = true;

						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["HeaderDetallado"].SectionFormat.EnableSuppress = false;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["Detalle"].SectionFormat.EnableSuppress = false;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["FooterDetallado"].SectionFormat.EnableSuppress = false;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["FooterResumido"].SectionFormat.EnableSuppress = true;
					}
					break;

				case EReportVista.Resumido:
					{
						rpt.HeaderDetallado.SectionFormat.EnableSuppress = true;
						rpt.HeaderResumido.SectionFormat.EnableSuppress = false;
						rpt.FooterExpediente.SectionFormat.EnableNewPageAfter = false;

						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["HeaderDetallado"].SectionFormat.EnableSuppress = true;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["Detalle"].SectionFormat.EnableSuppress = true;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["FooterDetallado"].SectionFormat.EnableSuppress = true;
						rpt.Subreports["StocksSubReport"].ReportDefinition.Sections["FooterResumido"].SectionFormat.EnableSuppress = false;
					}
					break;
			}

			rpt.ReportDefinition.ReportObjects["FacturaLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["AlbaranLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["BultosActualesLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["KgActualesLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.Subreports["StocksSubReport"].ReportDefinition.ReportObjects["Albaran_TB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.Subreports["StocksSubReport"].ReportDefinition.ReportObjects["Factura_TB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.Subreports["StocksSubReport"].ReportDefinition.ReportObjects["BultosActuales_TB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.Subreports["StocksSubReport"].ReportDefinition.ReportObjects["KilosActuales_TB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
		}

		private void FormatReport(StockLineListRpt rpt, ReportFilter filter, ReportFormat format)
		{
			switch (format.Vista)
			{
				case EReportVista.Detallado:
					{
						rpt.Detalle.SectionFormat.EnableSuppress = false;
					}
					break;

				case EReportVista.Resumido:
					{
						rpt.Detalle.SectionFormat.EnableSuppress = true;
					}
					break;
			}

			rpt.ReportDefinition.ReportObjects["FacturaLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["AlbaranLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["BultosActualesLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
			rpt.ReportDefinition.ReportObjects["KgActualesLB"].ObjectFormat.EnableSuppress = filter.SoloMermas;
		}

		private void FormatReport(InformeControlCobrosREARpt rpt, ReportFormat format)
		{
			FieldDefinition fdes = rpt.Database.Tables[0].Fields["CodigoExpediente"];

			switch (format.CampoOrdenacion)
			{
				case "Expediente":
					{
						fdes = rpt.Database.Tables[0].Fields["CodigoExpediente"];
					}
					break;
				case "Cobrada":
					{
						fdes = rpt.Database.Tables[0].Fields["Cobrado"];
					}
					break;
				case "Fecha de Cobro":
					{
						fdes = rpt.Database.Tables[0].Fields["FechaCobro"];
					}
					break;
				case "Total Cobrado":
					{
						fdes = rpt.Database.Tables[0].Fields["AyudaCobrada"];
					}
					break;
				case "Certificado":
					{
						fdes = rpt.Database.Tables[0].Fields["CertificadoREA"];
					}
					break;
			}

			rpt.DataDefinition.SortFields[0].Field = fdes;
			rpt.DataDefinition.SortFields[0].SortDirection = format.Orden;
		}

		#endregion

		#region Business Methods

		public ExpedienteAlListRpt GetAlListReport(ExpedienteList list)
        {
            if (list.Count == 0) return null;

            List<ExpedientePrint> pList = new List<ExpedientePrint>();

            foreach (ExpedientInfo item in list)
            {
                pList.Add(ExpedientePrint.New(item));            
            }

            ExpedienteAlListRpt doc = new ExpedienteAlListRpt();
            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Title", Title);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        public ExpedienteMaListRpt GetMaListReport(ExpedienteList list)
        {
            if (list.Count == 0) return null;

            List<ExpedientePrint> pList = new List<ExpedientePrint>();

            foreach (ExpedientInfo item in list)
            {
                pList.Add(ExpedientePrint.New(item));
            }

            ExpedienteMaListRpt doc = new ExpedienteMaListRpt();
            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        public ExpedienteGaListRpt GetGaListReport(ExpedienteList list)
        {
            if (list.Count == 0) return null;

            List<ExpedientePrint> pList = new List<ExpedientePrint>();

            foreach (ExpedientInfo item in list)
            {
                pList.Add(ExpedientePrint.New(item));
            }

            ExpedienteGaListRpt doc = new ExpedienteGaListRpt();
            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        public MermaStockRpt GetMermaDetailReport(StockInfo item)
        {
            if (item == null) return null;

            List<StockPrint> pList = new List<StockPrint>();

            pList.Add(item.GetPrintObject());

            MermaStockRpt doc = new MermaStockRpt();

            doc.SetDataSource(pList);

			CompanyInfo empresa = CompanyInfo.Get(Schema.Oid);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);

            return doc;
        }

        public LineaFomentoListRpt GetLineaFomentoListReport(LineaFomentoList list)
        {
            if (list.Count == 0) return null;

            List<LineaFomentoPrint> pList = new List<LineaFomentoPrint>();

            foreach (LineaFomentoInfo item in list)
            {
                pList.Add(LineaFomentoPrint.New(item));
            }

            LineaFomentoListRpt doc = new LineaFomentoListRpt();
            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion

        #region Business Methods Informe Pagos

        protected bool CompFechas(ReportFilter conf, DateTime fecha_factura, DateTime fecha_pago)
        {
            if (fecha_factura < conf.fecha_fac_inicio) return false;
            if (fecha_factura > conf.fecha_fac_final) return false;
            if (fecha_pago < conf.fecha_pago_inicio) return false;
            if (fecha_pago > conf.fecha_pago_final) return false;
            return true;
        }

        protected bool CompFechaFac(ReportFilter conf, DateTime date)
        {
            switch (conf.tipo)
            {
                case EPagos.Pendientes:
                    if (date != DateTime.MinValue)
                        return false;
                    else
                        return true;
                case EPagos.Pagados:
                    if (date != DateTime.MinValue)
                        return true;
                    else
                        return false;
                case EPagos.Todos:
                default:
                    return true;
            }
        }

        #endregion

        #region Business Methods Informe Cobros REA

        public InformeControlCobrosREARpt GetControlCobrosREAReport(ExpedienteREAList expedientes, 
                                                                    ReportFormat format)
        {
            if (expedientes.Count == 0) return null;

            List<ExpedienteREAPrint> pList = new List<ExpedienteREAPrint>();

			foreach (ExpedienteREAInfo item in expedientes)
			{
				if (item.ETipoExpediente == ETipoExpediente.Almacen) continue;
				pList.Add(ExpedienteREAPrint.New(item));
			}

            InformeControlCobrosREARpt doc = new InformeControlCobrosREARpt();

			doc.SetDataSource(pList);

			FormatHeader(doc);

			FormatReport(doc, format);

            return doc;
        }

        #endregion

        #region Business Methods Informe Movimiento Stock

        public PartidaListRpt GetPartidaListReport(BatchList list)
        {
            if (list.Count == 0) return null;

            List<PartidaPrint> pList = new List<PartidaPrint>();

            foreach (BatchInfo item in list)
            {
                pList.Add(PartidaPrint.New(item));
            }

			PartidaListRpt doc = new PartidaListRpt();
            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public MovimientosStockListPorExpedienteRpt GetMovimientosStockListAgrupado(ExpedientInfo item,
                                                                ProductInfo producto,
                                                                SerieInfo serie,
                                                                ReportFilter filter,
																ReportFormat format,
                                                                bool throwStockException)
        {
			MovimientosStockListPorExpedienteRpt doc = new MovimientosStockListPorExpedienteRpt();
            List<ExpedientePrint> pList = new List<ExpedientePrint>();
            List<StockPrint> movs = new List<StockPrint>();
            StockList stocks = null;

			if (filter.SoloStock)
				if ((item.StockKilos == 0) || (item.StockBultos == 0)) return null;

            stocks = (item.Stocks == null) ? StockList.GetListByExpediente(item.Oid, throwStockException) : item.Stocks;

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (stocks.Count <= 0) return null;

            pList.Add(item.GetPrintObject());

            foreach (StockInfo stock in stocks)
            {
                if (filter.SoloMermas)
                    if ((stock.OidAlbaran != 0) || (stock.Kilos >= 0)) 
                        continue;

                if ((filter.FechaIni <= stock.Fecha) && (stock.Fecha <= filter.FechaFin))
                {
                    if ((producto == null) && (serie == null))
                        movs.Add(StockPrint.New(stock));
                    else if (producto != null)
                    {
                        if ((producto.Oid == stock.OidProducto) && (serie == null))
                            movs.Add(StockPrint.New(stock));
                        else if ((producto.Oid == stock.OidProducto) && (serie.Oid == stock.OidSerie))
                            movs.Add(StockPrint.New(stock));
                    }
                    else if (serie.Oid == stock.OidSerie)
                        movs.Add(StockPrint.New(stock));
                }
            }

            doc.SetDataSource(pList);
            doc.Subreports["StocksSubReport"].SetDataSource(movs);

			FormatHeader(doc);
			FormatReport(doc, filter, format);

            return doc;
        }

		public MovimientosStockListPorExpedienteRpt GetMovimientosStockListAgrupado(ExpedienteList items,
                                                                ProductInfo producto,
                                                                SerieInfo serie,
                                                                ReportFilter filter,
																ReportFormat format)
        {
			MovimientosStockListPorExpedienteRpt doc = new MovimientosStockListPorExpedienteRpt();
            List<ExpedientePrint> pList = new List<ExpedientePrint>();
            List<StockPrint> movs = new List<StockPrint>();
            StockList stocks = null;

            int movsCount = 0;

            foreach (ExpedientInfo item in items)
            {
				if (filter.SoloStock)
					if ((item.StockKilos == 0) || (item.StockBultos == 0)) continue;

                movsCount = movs.Count;
                stocks = (item.Stocks == null) ? StockList.GetListByExpediente(item.Oid, false) : item.Stocks;
                foreach (StockInfo stock in stocks)
                {
					if (filter.SoloMermas)
                        if ((stock.OidAlbaran != 0) || (stock.Kilos >= 0)) 
                            continue;

                    if ((filter.FechaIni <= stock.Fecha) && (stock.Fecha <= filter.FechaFin))
                    {
                        if ((producto == null) && (serie == null))
                            movs.Add(StockPrint.New(stock));
                        else if (producto != null)
                        {
                            if ((producto.Oid == stock.OidProducto) && (serie == null))
                                movs.Add(StockPrint.New(stock));
                            else if ((producto.Oid == stock.OidProducto) && (serie.Oid == stock.OidSerie))
                                movs.Add(StockPrint.New(stock));
                        }
                        else if (serie.Oid == stock.OidSerie)
                            movs.Add(StockPrint.New(stock));
                    }
                }

                if (movsCount < movs.Count)
                    pList.Add(item.GetPrintObject());
            }

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (movs.Count <= 0) return null;

            doc.SetDataSource(pList);
            doc.Subreports["StocksSubReport"].SetDataSource(movs);

			FormatHeader(doc);
			FormatReport(doc, filter, format);

            return doc;
        }

		public StockLineListRpt GetStockLineList(StockList list, ReportFilter filter, ReportFormat format)
		{
			StockLineListRpt doc = new StockLineListRpt();
			
			//Si no existen elementos no tiene sentido un informe detallado. Además, falla en Crystal Reports
			if (list.Count <= 0) return null;

			List<StockPrint> print_list = new List<StockPrint>();

			foreach (StockInfo item in list)
				print_list.Add(StockPrint.New(item));

			doc.SetDataSource(print_list);

			FormatHeader(doc);
			FormatReport(doc, filter, format);	

			return doc;
		}

        #endregion

        #region Business Methods Informes REA
        
        public  ExpedienteREAListRpt GetExpedienteREAListReport(ExpedienteREAList expedientes)
        {
            if (expedientes.Count == 0) return null;

            List<ExpedienteREAPrint> pList = new List<ExpedienteREAPrint>();

            foreach (ExpedienteREAInfo item in expedientes)
                pList.Add(ExpedienteREAPrint.New(item));

            ExpedienteREAListRpt doc = new ExpedienteREAListRpt();

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}