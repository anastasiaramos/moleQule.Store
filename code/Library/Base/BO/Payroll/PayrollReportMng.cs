using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.CslaEx; 
 
using moleQule;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Nomina;

namespace moleQule.Library.Store
{
    public class NominaReportMng : BaseReportMng
    {
		#region Attributes & Properties

		public bool ShowQRCode { get; set; }

		#endregion

		#region Factory Methods

		public NominaReportMng() { }

		public NominaReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public NominaReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) { }

		#endregion

		#region Style

		/*private void FormatReport(InformeNominaListRpt rpt, ReportFilter filter, ReportFormat format)
		{
			switch (format.Vista)
			{
				case EReportVista.Agrupado:
					{
						rpt.HeaderListado.SectionFormat.EnableSuppress = true;
						rpt.DetailListado.SectionFormat.EnableSuppress = true;
					}
					break;

				case EReportVista.ListaCompleta:
					{
						rpt.HeaderExpediente.SectionFormat.EnableSuppress = true;
						rpt.HeaderGrupoExpediente.SectionFormat.EnableSuppress = true;
						rpt.DetailExpediente.SectionFormat.EnableSuppress = true;
						rpt.FooterExpediente.SectionFormat.EnableSuppress = true;
					}
					break;
			}
		}*/

		#endregion

		#region Business Methods

		public NominaListRpt GetListReport(PayrollList list)
		{
			if (list.Count == 0) return null;

			NominaListRpt doc = new NominaListRpt();

			List<NominaPrint> pList = new List<NominaPrint>();

			foreach (NominaInfo item in list)
				pList.Add(NominaPrint.New(item));

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

        #endregion
    }
}
