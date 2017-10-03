using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Nomina;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PayrollBatchReportMng : BaseReportMng
    {
		#region Factory Methods

		public PayrollBatchReportMng() { }

		public PayrollBatchReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public PayrollBatchReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public PayrollBatchReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

		#region Layout

		/*private static void FormatReport(NominaRpt rpt, string logo)
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

        public RemesaNominasListRpt GetListReport(PayrollBatchList list)
        {
            if (list.Count == 0) return null;

            RemesaNominasListRpt doc = new RemesaNominasListRpt();

            List<RemesaNominaPrint> pList = new List<RemesaNominaPrint>();

            foreach (PayrollBatchInfo item in list)
                pList.Add(RemesaNominaPrint.New(item));

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion

		#region Business Methods Nomina

		/*public NominaRpt GetDetailReport(NominaInfo item)
        {
            if (item == null) return null;
			
            NominaRpt doc = new NominaRpt();
            
            List<NominaPrint> pList = new List<NominaPrint>();

            pList.Add(NominaPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<GastoPrint> pGastos = new List<GastoPrint>();
            
			foreach (GastoInfo child in item.Gastos)
			{
				pGastos.Add(GastoPrint.New(child));
			}

			doc.Subreports["GastoSubRpt"].SetDataSource(pGastos);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public NominaListRpt GetListReport(NominaList list)
		{
			if (list.Count == 0) return null;

			NominaListRpt doc = new ClienteListRpt();

			List<NominaPrint> pList = new List<NominaPrint>();
			
			foreach (NominaInfo item in list)
			{
				pList.Add(NominaPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}*/

		#endregion
    }
}
