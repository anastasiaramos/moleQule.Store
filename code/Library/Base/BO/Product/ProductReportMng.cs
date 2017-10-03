using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Producto;

namespace moleQule.Library.Store
{
    public class ProductReportMng : BaseReportMng
    {
		#region Factory Methods

		public ProductReportMng() { }

		public ProductReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public ProductReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }


		#endregion

		#region Business Methods

        public ProductoListRpt GetListReport(ProductList list)
        {
            if (list.Count == 0) return null;

            ProductoListRpt doc = new ProductoListRpt();

            List<ProductoPrint> pList = new List<ProductoPrint>();

            foreach (ProductInfo item in list)
            {
                pList.Add(ProductoPrint.New(item));
            }

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public InventarioValoradoRpt GetInventarioValoradoReport(InventarioValoradoList list, DateTime fecha)
        {
            if (list.Count == 0) return null;

            InventarioValoradoRpt doc = new InventarioValoradoRpt();

            doc.SetDataSource(list);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        public InformeComprasProductosRpt GetComprasProductosReport(ComprasList list)
        {
            InformeComprasProductosRpt doc = new InformeComprasProductosRpt();

            List<ComprasInfo> pList = new List<ComprasInfo>();

            foreach (ComprasInfo item in list)
                pList.Add(item);

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        #endregion
    }
}