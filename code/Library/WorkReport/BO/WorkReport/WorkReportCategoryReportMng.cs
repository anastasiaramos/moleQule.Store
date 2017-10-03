using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class WorkReportCategoryReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public WorkReportCategoryReportMng() {}

        public WorkReportCategoryReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public WorkReportCategoryReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public WorkReportCategoryReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(WorkReportCategoryRpt rpt, string logo)
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

        #region Business Methods WorkReportCategory
		
        public WorkReportCategoryRpt GetDetailReport(WorkReportCategoryInfo item)
        {
            if (item == null) return null;
			
            WorkReportCategoryRpt doc = new WorkReportCategoryRpt();
            
            List<WorkReportCategoryPrint> pList = new List<WorkReportCategoryPrint>();

            pList.Add(WorkReportCategoryPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public WorkReportCategoryListRpt GetListReport(WorkReportCategoryList list)
		{
			if (list.Count == 0) return null;

			WorkReportCategoryListRpt doc = new ClienteListRpt();

			List<WorkReportCategoryPrint> pList = new List<WorkReportCategoryPrint>();
			
			foreach (WorkReportCategoryInfo item in list)
			{
				pList.Add(WorkReportCategoryPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
