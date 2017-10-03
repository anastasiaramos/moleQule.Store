using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Expedient;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LivestockBookReportMng : BaseReportMng
    {	
        #region Factory Methods

        public LivestockBookReportMng() {}

        public LivestockBookReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public LivestockBookReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public LivestockBookReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        #endregion

        #region Business Methods

		public ReportClass GetListReport(LivestockBookLineList list)
		{
			if (list.Count == 0) return null;

            LivestockBookRpt doc = new LivestockBookRpt();

			List<LivestockBookLinePrint> pList = new List<LivestockBookLinePrint>();
			
			foreach (LivestockBookLineInfo item in list)
			{
                pList.Add(LineaLibroGanaderoPrint.New(item));
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion
    }
}
