using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Loan.Reports.Loan;
using moleQule.Library.Reports;
using moleQule.Library.Store;

namespace moleQule.Library.Loan
{
    [Serializable()]
    public class LoanReportMng : BaseReportMng
    {	
        #region Factory Methods

        public LoanReportMng() {}

        public LoanReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public LoanReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public LoanReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(PrestamoRpt rpt, string logo)
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
		
        //public PrestamoRpt GetDetailReport(LoanInfo item)
        //{
        //    if (item == null) return null;
			
        //    PrestamoRpt doc = new PrestamoRpt();
            
        //    List<PrestamoPrint> pList = new List<PrestamoPrint>();

        //    pList.Add(PrestamoPrint.New(item));
        //    doc.SetDataSource(pList);
        //    doc.SetParameterValue("Empresa", Schema.Name);

        //    List<PaymentPrint> pPagos = new List<PaymentPrint>();
            
        //    foreach (PaymentInfo child in item.Payments)
        //    {
        //        pPagos.Add(PagoPrint.New(child));
        //    }

        //    doc.Subreports["PagoSubRpt"].SetDataSource(pPagos);
			
        //    //FormatReport(doc, empresa.Logo);

        //    return doc;
        //}

		public LoanListRpt GetListReport(LoanList list)
		{
			if (list.Count == 0) return null;

            LoanListRpt doc = new LoanListRpt();

			List<PrestamoPrint> pList = new List<PrestamoPrint>();
			
			foreach (LoanInfo item in list)
			{
				pList.Add(PrestamoPrint.New(item));
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion
    }
}
