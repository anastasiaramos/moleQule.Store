using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store.Reports.Invoice;
using moleQule.Library.Store.Reports.Payment;
using moleQule.Reports;

namespace moleQule.Library.Store
{
    public class InputInvoiceReportMng : Reports.ReportMng
    {
        #region Business Methods

		public QRCodeRpt GetQRCodeReport(InputInvoiceInfo item)
		{
			if (item == null) return null;

			List<InputInvoicePrint> pList = new List<InputInvoicePrint>();

			pList.Add(InputInvoicePrint.New(item, null));

			QRCodeRpt doc = new QRCodeRpt();

			doc.SetDataSource(pList);

			return doc;
		}
		public QRCodeRpt GetQRCodeReport(InputInvoiceList list)
		{
			if (list.Count == 0) return null;

			List<InputInvoicePrint> pList = new List<InputInvoicePrint>();

			foreach (InputInvoiceInfo item in list)
				pList.Add(InputInvoicePrint.New(item, null));
			
			QRCodeRpt doc = new QRCodeRpt();

			doc.SetDataSource(pList);

			return doc;
		}

        public ReportClass GetDetailReport(InputInvoiceInfo item, FormatConfFacturaAlbaranReport conf)
        {
            if (item == null) return null;

            List<InputInvoiceLinePrint> conceptos = new List<InputInvoiceLinePrint>();
            List<InputInvoicePrint> pList = new List<InputInvoicePrint>();

            foreach (InputInvoiceLineInfo cfi in item.Conceptos)
                conceptos.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(item.GetPrintObject());

            ProductList productos = ProductList.GetList(false);
            foreach (InputInvoiceLinePrint cfp in conceptos)
            {
                if (cfp.OidProducto == 0) continue;
                ProductInfo prod = productos.GetItem(cfp.OidProducto);
                if (prod != null)
                {
                    if (prod.AyudaKilo > 0)
                        cfp.Concepto += " *";
                }
            }
            
            List<ImpuestoResumen> irpf_list = new List<ImpuestoResumen>();

			foreach (DictionaryEntry irpf in item.GetIRPF())
				irpf_list.Add((ImpuestoResumen)irpf.Value);

            ReportClass doc = null;

            try
            {
                doc = GetReportFromName("Invoice", "InputInvoiceRpt");
            }
            catch
            {
                doc = new InputInvoiceRpt();
            }

            doc.Subreports["LinesSubRpt"].SetDataSource(conceptos);
            if (doc.Subreports["IRPFSubListRpt"] != null)
                doc.Subreports["IRPFSubListRpt"].SetDataSource(irpf_list);

            doc.SetDataSource(pList);
			CompanyInfo company = CompanyInfo.Get(Schema.Oid, false);
            doc.SetParameterValue("nombreEmpresa", company.Name);
            doc.SetParameterValue("dirEmpresa", company.Direccion);
            doc.SetParameterValue("dir2Empresa", company.CodPostal + ". " + company.Municipio + ". " + company.Provincia);
            doc.SetParameterValue("CIFEmpresa", company.VatNumber);
            doc.SetParameterValue("nota", conf.nota);
            doc.SetParameterValue("copia", (conf.copia != null) ? conf.copia : string.Empty);
            doc.SetParameterValue("cuentaBancaria", (conf.cuenta_bancaria != string.Empty) ? conf.cuenta_bancaria : company.CuentaBancaria);

            return doc;
        }

        public InputInvoiceListRpt GetListReport(InputInvoiceList list)
        {
            if (list.Count == 0) return null;

            InputInvoiceListRpt doc = new InputInvoiceListRpt();

            List<InputInvoicePrint> pList = new List<InputInvoicePrint>();

            foreach (InputInvoiceInfo item in list)
            {
                pList.Add(InputInvoicePrint.New(item, null, false));
            }

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        public InputInvoiceListRpt GetListReport(InputInvoiceList list,
                                                    ProviderBaseList acreedores)
        {
            if (list.Count == 0) return null;

            InputInvoiceListRpt doc = new InputInvoiceListRpt();

            List<InputInvoicePrint> pList = new List<InputInvoicePrint>();

            foreach (InputInvoiceInfo item in list)
            {
                pList.Add(InputInvoicePrint.New(item,
                                                    acreedores.GetItem(item.Oid, item.ETipoAcreedor),
                                                    false));
            }

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion

        #region Factory Methods

        public InputInvoiceReportMng() {}

        public InputInvoiceReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) {}

        public InputInvoiceReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}


        public InputInvoiceReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }

        #endregion
    }
}
