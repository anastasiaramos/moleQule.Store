using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule;
using moleQule.Common;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Store;

namespace moleQule.Library.Store
{
    public class StoreReportMng : BaseReportMng
    {
		#region Factory Methods

		public StoreReportMng() { }

		public StoreReportMng(ISchemaInfo company)
            : this(company, string.Empty, string.Empty) { }

        public StoreReportMng(ISchemaInfo company, string title, string filter)
            : base(company, title, filter) { }

		#endregion

		#region Style

        private void FormatReport(StoreFileRpt rpt, ReportFilter filter, ReportFormat format)
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
        }

		#endregion

        #region Business Methods

        public StoreFileRpt GetStoreFile(StockList list, ReportFilter filter
                                            , ReportFormat format
                                            , bool byKg = false
                                            , string stockPurchasePriceType = "Average")
        {
            StoreFileRpt doc = new StoreFileRpt();

            //Si no existen elementos no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (list.Count <= 0) return null;

            List<StockPrint> print_list = new List<StockPrint>();

            foreach (StockInfo item in list)
                print_list.Add(StockPrint.New(item));

            doc.SetDataSource(print_list);

            FormatHeader(doc);
            FormatReport(doc, filter, format);

            doc.SetParameterValue("ByKg", byKg);
            doc.SetParameterValue("StockPurchasePriceType", stockPurchasePriceType);

            return doc;
        }

        #endregion
    }
}