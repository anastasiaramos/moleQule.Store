using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Store.Reports;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InventarioAlmacenReportMng : BaseReportMng
    {

        #region Business Methods InventarioAlmacen
		
        public InventarioAlmacenRpt GetDetailReport(InventarioAlmacenInfo item)
        {
            if (item == null) return null;
			
            InventarioAlmacenRpt doc = new InventarioAlmacenRpt();
            
            List<InventarioAlmacenPrint> pList = new List<InventarioAlmacenPrint>();

            pList.Add(InventarioAlmacenPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<LineaInventarioPrint> pLineaInventarios = new List<LineaInventarioPrint>();
            
			foreach (LineaInventarioInfo child in item.LineaInventarios)
			{
				pLineaInventarios.Add(LineaInventarioPrint.New(child));
			}

			doc.Subreports["LineaInventarioSubRpt"].SetDataSource(pLineaInventarios);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public InventarioAlmacenListRpt GetListReport(InventarioAlmacenList list)
		{
			if (list.Count == 0) return null;

			InventarioAlmacenListRpt doc = new ClienteListRpt();

			List<InventarioAlmacenPrint> pList = new List<InventarioAlmacenPrint>();
			
			foreach (InventarioAlmacenInfo item in list)
			{
				pList.Add(InventarioAlmacenPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);

			return doc;
		}
		
        #endregion

        #region Factory Methods

        public InventarioAlmacenReportMng() {}

        public InventarioAlmacenReportMng(ISchemaInfo empresa)
            : base(empresa) { }

        #endregion
        
        #region Style

        private static void FormatReport(InventarioAlmacenRpt rpt, string logo)
        {
            /*string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                int width = rpt.Section1.ReportObjects["Logo"].Width;
                int height = rpt.Section1.ReportObjects["Logo"].Height;

                rpt.Section1.ReportObjects["Logo"].Width = 15 * image.Width;
                rpt.Section1.ReportObjects["Logo"].Height = 15 * image.Height;
                rpt.Section1.ReportObjects["Logo"].Left += (width - 15 * image.Width) / 2;
                rpt.Section1.ReportObjects["Logo"].Top += (height - 15 * image.Height) / 2;
            }*/
        }

        #endregion

    }
}
