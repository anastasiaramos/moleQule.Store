using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Reports;
using moleQule.Library.Store.Reports.Albaran;
using moleQule.Serie;

namespace moleQule.Library.Store
{
    public class InputDeliveryReportMng : BaseReportMng
    {
		#region Factory Methods

		public InputDeliveryReportMng() { }

		public InputDeliveryReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public InputDeliveryReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) { }

		#endregion

		#region Business Methods

        public AlbaranRecibidoRpt GetDetailRpt(InputDeliveryInfo item, FormatConfFacturaAlbaranReport conf)
        {
            if (item == null) return null;

			AlbaranRecibidoRpt doc = new AlbaranRecibidoRpt();

            List<InputDeliveryLinePrint> conceptos = new List<InputDeliveryLinePrint>();
            List<InputDeliveryPrint> pList = new List<InputDeliveryPrint>();

            foreach (InputDeliveryLineInfo cfi in item.ConceptoAlbaranes)
                conceptos.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(item.GetPrintObject());
            
            ProductList productos = ProductList.GetList(false);
            foreach (InputDeliveryLinePrint cfp in conceptos)
            {
                if (cfp.OidProducto == 0) continue;
                ProductInfo prod = productos.GetItem(cfp.OidProducto);
                if ( prod != null)
                {
                    if (prod.AyudaKilo > 0)
                        cfp.Concepto += " *";
                }
            }

            doc.Subreports["ConceptosRpt"].SetDataSource(conceptos);

            doc.SetDataSource(pList);
			CompanyInfo empresa = CompanyInfo.Get(Schema.Oid);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("nota", (conf.nota != null) ? conf.nota : string.Empty);

            return doc;
        }

        public AlbaranRecibidoListRpt GetListReport(InputDeliveryList list,                                                                           
                                                    SerieList series,
                                                    ProviderBaseList acreedores)
        {
            if (list.Count == 0) return null;

            AlbaranRecibidoListRpt doc = new AlbaranRecibidoListRpt();

            List<InputDeliveryPrint> pList = new List<InputDeliveryPrint>();

            foreach (InputDeliveryInfo item in list)
            {
                pList.Add(InputDeliveryPrint.New(item,
													acreedores.GetItem(item.OidAcreedor, item.ETipoAcreedor), 
													series.GetItem(item.OidSerie)));
            }

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        /*public AlbaranDetailListRpt GetDetailListReport(AlbaranList list,
                                                        SerieList series,
                                                        ClienteList clientes,
                                                        ETipoAlbaran tipo,
                                                        DateTime fini,
                                                        DateTime ffin)
        {
            if (list.Count == 0) return null;

            AlbaranDetailListRpt doc = new AlbaranDetailListRpt();

            List<AlbaranPrint> pList = new List<AlbaranPrint>();
            List<ConceptoAlbaranPrint> conceptos = new List<ConceptoAlbaranPrint>();

            foreach (InputDeliveryInfo item in list)
            {
                pList.Add(AlbaranPrint.New(item,
                                           clientes.GetItem(item.OidCliente),
                                           null,
                                           series.GetItem(item.OidSerie)));

                foreach (ConceptoAlbaranInfo cp in item.ConceptoAlbaranes)
                    conceptos.Add(cp.GetPrintObject());
            }

            doc.SetDataSource(pList);
            doc.Subreports["Conceptos"].SetDataSource(conceptos);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Tipo", tipo.ToString());
            doc.SetParameterValue("FIni", fini);
            doc.SetParameterValue("FFin", ffin);

            return doc;
        }

        public AlbaranRpt GetAlbaranReport(InputDeliveryInfo item, FormatConfFacturaAlbaranReport conf)
        {
            if (item == null) return null;

            AlbaranRpt doc = new AlbaranRpt();

            List<ConceptoAlbaranPrint> conceptos = new List<ConceptoAlbaranPrint>();
            List<AlbaranPrint> pList = new List<AlbaranPrint>();

            foreach (ConceptoAlbaranInfo cfi in item.ConceptoAlbaranes)
                conceptos.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(item.GetPrintObject());
            
            doc.SetDataSource(pList);
            doc.Subreports["ConceptosCliente"].SetDataSource(conceptos);
            doc.Subreports["ConceptosEmpresa"].SetDataSource(conceptos);

            EmpresaInfo empresa = EmpresaInfo.Get(Schema.Oid);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("CIFEmpresa", empresa.ID);
            doc.SetParameterValue("nota", conf.nota);

            return doc;
        }*/

        #endregion
   }
}
