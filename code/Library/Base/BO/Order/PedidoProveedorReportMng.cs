using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Proveedor;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PedidoProveedorReportMng : BaseReportMng
    {

        #region Factory Methods

        public PedidoProveedorReportMng() {}

        public PedidoProveedorReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) {}

        public PedidoProveedorReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}


		public PedidoProveedorReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }

        #endregion

		#region Style

		/*private static void FormatReport(PedidoProveedorRpt rpt, string logo)
		{

		}*/

		#endregion


		#region Business Methods PedidoProveedor

		/*public PedidoProveedorRpt GetDetailReport(PedidoProveedorInfo item)
        {
            if (item == null) return null;
			
            PedidoProveedorRpt doc = new PedidoProveedorRpt();
            
            List<PedidoProveedorPrint> pList = new List<PedidoProveedorPrint>();

            pList.Add(PedidoProveedorPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<LineaPedidoPrint> pLineaPedidos = new List<LineaPedidoPrint>();
            
			foreach (LineaPedidoInfo child in item.LineaPedidos)
			{
				pLineaPedidos.Add(LineaPedidoPrint.New(child));
			}

			doc.Subreports["LineaPedidoSubRpt"].SetDataSource(pLineaPedidos);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public PedidoProveedorListRpt GetListReport(PedidoProveedorList list)
		{
			if (list.Count == 0) return null;

			PedidoProveedorListRpt doc = new ClienteListRpt();

			List<PedidoProveedorPrint> pList = new List<PedidoProveedorPrint>();
			
			foreach (PedidoProveedorInfo item in list)
			{
				pList.Add(PedidoProveedorPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);

			return doc;
		}*/

		#endregion

    }
}
