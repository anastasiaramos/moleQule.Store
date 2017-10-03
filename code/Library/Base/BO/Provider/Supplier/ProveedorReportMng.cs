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
    public class ProveedorReportMng : BaseReportMng
    {
        #region Business Methods Proveedor

        public ProveedorListRpt GetListReport(ProveedorList list)
        {
            if (list.Count == 0) return null;

            ProveedorListRpt doc = new ProveedorListRpt();

            List<ProveedorPrint> pList = new List<ProveedorPrint>();

            foreach (ProveedorInfo item in list)
            {
                pList.Add(ProveedorPrint.New(item));
            }

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        public InformeComprasRpt GetComprasReport(ComprasList list)
        {
            InformeComprasRpt doc = new InformeComprasRpt();

            List<ComprasInfo> pList = new List<ComprasInfo>();

            foreach (ComprasInfo item in list)
                pList.Add(item);

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

        #endregion

        #region Factory Methods

        public ProveedorReportMng()
            : this(null) {}

        public ProveedorReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty, string.Empty) {}

        public ProveedorReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) {}

        #endregion
    }
}
