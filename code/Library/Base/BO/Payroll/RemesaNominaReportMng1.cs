using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Library.moleQule.Libary.Store
{
    [Serializable()]
    public class RemesaNominaReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public RemesaNominaReportMng() {}

        public RemesaNominaReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public RemesaNominaReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public RemesaNominaReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(RemesaNominaRpt rpt, string logo)
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

        #region Business Methods RemesaNomina
		
        public RemesaNominaRpt GetDetailReport(RemesaNominaInfo item)
        {
            if (item == null) return null;
			
            RemesaNominaRpt doc = new RemesaNominaRpt();
            
            List<RemesaNominaPrint> pList = new List<RemesaNominaPrint>();

            pList.Add(RemesaNominaPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public RemesaNominaListRpt GetListReport(RemesaNominaList list)
		{
			if (list.Count == 0) return null;

			RemesaNominaListRpt doc = new ClienteListRpt();

			List<RemesaNominaPrint> pList = new List<RemesaNominaPrint>();
			
			foreach (RemesaNominaInfo item in list)
			{
				pList.Add(RemesaNominaPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
