using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Store.Reports.Almacen;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class AlmacenReportMng : BaseReportMng
    {

        #region Business Methods Almacen
		
        public AlmacenRpt GetDetailReport(AlmacenInfo item)
        {
            if (item == null) return null;
			
            AlmacenRpt doc = new AlmacenRpt();
            
            List<AlmacenPrint> pList = new List<AlmacenPrint>();

            pList.Add(AlmacenPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<InventarioAlmacenPrint> pInventarioAlmacenes = new List<InventarioAlmacenPrint>();
            
			foreach (InventarioAlmacenInfo child in item.InventarioAlmacenes)
			{
				pInventarioAlmacenes.Add(InventarioAlmacenPrint.New(child));
			}

			doc.Subreports["InventarioAlmacenSubRpt"].SetDataSource(pInventarioAlmacenes);
			
			List<LineaAlmacenPrint> pLineaAlmacenes = new List<LineaAlmacenPrint>();
            
			foreach (LineaAlmacenInfo child in item.LineaAlmacenes)
			{
				pLineaAlmacenes.Add(LineaAlmacenPrint.New(child));
			}

			doc.Subreports["LineaAlmacenSubRpt"].SetDataSource(pLineaAlmacenes);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public AlmacenListRpt GetListReport(AlmacenList list)
		{
			if (list.Count == 0) return null;

			AlmacenListRpt doc = new ClienteListRpt();

			List<AlmacenPrint> pList = new List<AlmacenPrint>();
			
			foreach (AlmacenInfo item in list)
			{
				pList.Add(AlmacenPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);

			return doc;
		}
		
        #endregion

        #region Factory Methods

        public AlmacenReportMng() {}

        public AlmacenReportMng(ISchemaInfo empresa)
            : base(empresa) { }

        #endregion
        
        #region Style

        private static void FormatReport(AlmacenRpt rpt, string logo)
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
