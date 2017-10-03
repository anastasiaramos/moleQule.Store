using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule;
using moleQule.Reports;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class TipoGastoReportMng : BaseReportMng
    {
		#region Factory Methods

		public TipoGastoReportMng() { }

		public TipoGastoReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public TipoGastoReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public TipoGastoReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

        #region Business Methods TipoGasto
		
        /*public TipoGastoRpt GetDetailReport(TipoGastoInfo item)
        {
            if (item == null) return null;
			
            TipoGastoRpt doc = new TipoGastoRpt();
            
            List<TipoGastoPrint> pList = new List<TipoGastoPrint>();

            pList.Add(TipoGastoPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public TipoGastoListRpt GetListReport(TipoGastoList list)
		{
			if (list.Count == 0) return null;

			TipoGastoListRpt doc = new ClienteListRpt();

			List<TipoGastoPrint> pList = new List<TipoGastoPrint>();
			
			foreach (TipoGastoInfo item in list)
			{
				pList.Add(TipoGastoPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}*/
		
        #endregion
       
        #region Style

        /*private static void FormatReport(TipoGastoRpt rpt, string logo)
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

    }
}
