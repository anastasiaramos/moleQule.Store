using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.WorkReport.Report.WorkReport;

namespace moleQule.Library.WorkReport
{
    [Serializable()]
    public class WorkReportReportMng : BaseReportMng
    {	
        #region Factory Methods

        public WorkReportReportMng() {}

        public WorkReportReportMng(ISchemaInfo company)
            : this(company, string.Empty) { }

        public WorkReportReportMng(ISchemaInfo company, string title)
            : this(company, title, string.Empty) { }

        public WorkReportReportMng(ISchemaInfo company, string title, string filter)
            : base(company, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(WorkReportRpt rpt, string logo)
        {
            string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                int width = rpt.Section1.ReportObjects["Logo"].Width;
                int height = rpt.Section1.ReportObjects["Logo"].Height;

                rpt.Section1.ReportObjects["Logo"].Width = 15 * image.Width;
                rpt.Section1.ReportObjects["Logo"].Height = 15 * image.Height;
                rpt.Section1.ReportObjects["Logo"].Left += (width - 15 * image.Width) / 2;
                rpt.Section1.ReportObjects["Logo"].Top += (height - 15 * image.Height) / 2;
            }
        }*/

        #endregion

        #region Business Methods

        public ReportClass GetDetailReport(WorkReportInfo item)
        {
            if (item == null) return null;
			
            WorkReportRpt doc = new WorkReportRpt();
            
            List<WorkReportPrint> pList = new List<WorkReportPrint>();

            pList.Add(WorkReportPrint.New(item));
            doc.SetDataSource(pList);		
			
			List<WorkReportResourcePrint> pLines = new List<WorkReportResourcePrint>();
            
			foreach (WorkReportResourceInfo child in item.Lines)
			{
                pLines.Add(WorkReportResourcePrint.New(child));
			}

            doc.Subreports["LinesSubRpt"].SetDataSource(pLines);

            return doc;
        }

        public ReportClass GetListReport(WorkReportList list)
		{
			if (list.Count == 0) return null;

            WorkReportListRpt doc = new WorkReportListRpt();

			List<WorkReportPrint> pList = new List<WorkReportPrint>();
			
			foreach (WorkReportInfo item in list)
			{
				pList.Add(WorkReportPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}

        public ReportClass GetWorkReportResourceList(WorkReportResourceList list)
        {
            if (list.Count == 0) return null;

            WorkReportResourceListRpt doc = new WorkReportResourceListRpt();

            List<WorkReportResourcePrint> pList = new List<WorkReportResourcePrint>();

            foreach (WorkReportResourceInfo item in list)
            {
                pList.Add(WorkReportResourcePrint.New(item));
            }

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
