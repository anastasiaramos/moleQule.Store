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
    public class ToolReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public ToolReportMng() {}

        public ToolReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public ToolReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public ToolReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(ToolRpt rpt, string logo)
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

        #region Business Methods Tool
		
        public ToolRpt GetDetailReport(ToolInfo item)
        {
            if (item == null) return null;
			
            ToolRpt doc = new ToolRpt();
            
            List<ToolPrint> pList = new List<ToolPrint>();

            pList.Add(ToolPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public ToolListRpt GetListReport(ToolList list)
		{
			if (list.Count == 0) return null;

			ToolListRpt doc = new ClienteListRpt();

			List<ToolPrint> pList = new List<ToolPrint>();
			
			foreach (ToolInfo item in list)
			{
				pList.Add(ToolPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
