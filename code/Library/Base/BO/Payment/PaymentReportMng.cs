using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Reports;
using moleQule.Library.Store.Reports.Payment;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    public class PaymentReportMng : BaseReportMng
    {
		#region Attributes & Properties

		public bool ShowQRCode { get; set; }

		#endregion

		#region Factory Methods

		public PaymentReportMng() { }

		public PaymentReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public PaymentReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) { }

		#endregion

        #region Business Methods

        public PaymentRpt GetDetailReport(Payment item, 
                                                IAcreedor acreedor, 
                                                InputInvoiceList facturas)
        {
            if (item == null) return null;

            PaymentRpt doc = new PaymentRpt();

            List<PaymentPrint> pList = new List<PaymentPrint>();
            List<TransactionPaymentPrint> pagosList = new List<TransactionPaymentPrint>();

            foreach (TransactionPayment pagoFactura in item.Operations)
                pagosList.Add(TransactionPaymentPrint.New(pagoFactura.GetInfo(), facturas.GetItem(pagoFactura.OidOperation)));

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (pagosList.Count <= 0) return null;

            pList.Add(PaymentPrint.New(item.GetInfo(false), acreedor, false));

            doc.SetDataSource(pList);
            doc.Subreports["Cuerpo"].SetDataSource(pagosList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        public PagoNominaDetailRpt GetNominaDetailReport(Payment item,
                                                IAcreedor empleado,
                                                PayrollList nominas)
        {
            if (item == null) return null;

            PagoNominaDetailRpt doc = new PagoNominaDetailRpt();

            List<PaymentPrint> pList = new List<PaymentPrint>();
            List<TransactionPaymentPrint> pagosList = new List<TransactionPaymentPrint>();

            foreach (TransactionPayment pagoFactura in item.Operations)
            {
                if (nominas.Contains(pagoFactura.OidOperation))
                    pagosList.Add(TransactionPaymentPrint.New(pagoFactura.GetInfo(), nominas.GetItem(pagoFactura.OidOperation)));
            }

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (pagosList.Count <= 0) return null;

            pList.Add(PaymentPrint.New(item.GetInfo(false), empleado, false));

            doc.SetDataSource(pList);
            doc.Subreports["Cuerpo"].SetDataSource(pagosList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        public PaymentListRpt GetListReport(PaymentList list, TransactionPaymentList p_facturas)
        {
            if (list == null) return null;

            PaymentListRpt doc = new PaymentListRpt();

            List<PaymentPrint> pList = new List<PaymentPrint>();

			foreach (PaymentInfo pago in list)
			{
				if (ShowQRCode)
					pago.LoadChilds(p_facturas.GetSubList(new FCriteria<long>("OidPago", pago.Oid, Operation.Equal)));

				pList.Add(PaymentPrint.New(pago, null, ShowQRCode));
			}

            doc.SetDataSource(pList);

			FormatHeader(doc);

			doc.QRCodeSection.SectionFormat.EnableSuppress = !ShowQRCode;

            return doc;
        }

        public PagoAcreedorListRpt GetPagoAcreedorListReport(IList<PaymentSummary> list)
        {
            if (list == null) return null;

            PagoAcreedorListRpt doc = new PagoAcreedorListRpt();

            doc.SetDataSource(list);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Title", Title);
            doc.SetParameterValue("Filter", Filter);

            return doc;
        }

		public PagoAcreedorListRpt GetPagoAcreedorListReport(PaymentSummaryList list)
		{
			if (list == null) return null;

			PagoAcreedorListRpt doc = new PagoAcreedorListRpt();

			doc.SetDataSource(list);
			doc.SetParameterValue("Empresa", Schema.Name);
			doc.SetParameterValue("Title", Title);
			doc.SetParameterValue("Filter", Filter);

			return doc;
		}

        public PagoAcreedorDetailRpt GetPagoAcreedorDetailReport(PaymentSummary item, Payments pagos)
        {
            if (item == null) return null;

			PagoAcreedorDetailRpt doc = new PagoAcreedorDetailRpt();

            List<PaymentPrint> pagosList = new List<PaymentPrint>();
            List<PaymentSummary> pList = new List<PaymentSummary>();

            foreach (Payment pago in pagos)
                pagosList.Add(PaymentPrint.New(pago.GetInfo(true), null, false));

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (pagosList.Count <= 0)
                return null;

            pList.Add(item);

            doc.SetDataSource(pList);
            doc.Subreports["Cuerpo"].SetDataSource(pagosList);

			FormatHeader(doc);

            return doc;
        }
        
        #endregion

        #region Informe de Pagos

        public PaymentsReportRpt GetInformeControlPagosReport(InputInvoiceList facturas,
															ExpedienteList expedientes,
															PaymentSummaryList resumen,
															ReportFilter filter,
															ReportFormat format)
        {
            List<ExpedientInfo> explist = new List<ExpedientInfo>();

            foreach (ExpedientInfo exp in expedientes)
            {
                if ((filter.exp_inicial.Length == 0 || exp.Codigo.CompareTo(filter.exp_inicial) >= 0) && 
					(filter.exp_final.Length == 0 || exp.Codigo.CompareTo(filter.exp_final) <= 0))
                    explist.Add(exp);
            }

            List<ControlPagosPrint> lista = new List<ControlPagosPrint>();
            ExpedientInfo obj = null;
            DateTime fecha_pago = DateTime.MinValue;

            string oidS = string.Empty;
            long oid = 0;

            foreach (InputInvoiceInfo item in facturas)
            {
                if (!CompFechas(filter, item.Prevision, item.FechaPagoFactura)) continue;
                if ((filter.tipo == EPagos.Pagados) && !item.Pagada) continue;
                if ((filter.tipo == EPagos.Pendientes) && item.Pagada) continue;

                switch (filter.tipo_informe)
                {
                    case ETipoInforme.Despachante:

                        if (item.ETipoAcreedor != ETipoAcreedor.Despachante) continue;

                        if (filter.objeto_detallado != null)
                            if (!item.OidAcreedor.Equals(((DespachanteInfo)filter.objeto_detallado).Oid)) continue;

                        obj = expedientes.GetItem(item.OidExpediente);
                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;

                    case ETipoInforme.Naviera:

                        if (item.ETipoAcreedor != ETipoAcreedor.Naviera) continue;

                        if (filter.objeto_detallado != null)
                            if (!item.OidAcreedor.Equals(((NavieraInfo)filter.objeto_detallado).Oid)) continue;

                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);

                        obj = expedientes.GetItem(item.OidExpediente);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;

                    case ETipoInforme.Proveedor:

                        if (item.ETipoAcreedor != ETipoAcreedor.Proveedor) continue;

                        obj = expedientes.GetItem(item.OidExpediente);

                        if (filter.objeto_detallado != null)
                            if (!item.OidAcreedor.Equals(((ProveedorInfo)filter.objeto_detallado).Oid)) continue;

                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);

                        obj = expedientes.GetItem(item.OidExpediente);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;

                    case ETipoInforme.TransportistaDestino:

                        if (item.ETipoAcreedor != ETipoAcreedor.TransportistaDestino) continue;

                        if (filter.objeto_detallado != null)
                            if (item.OidAcreedor.Equals(((Transporter)filter.objeto_detallado).Oid)) continue;

                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);

                        obj = expedientes.GetItem(item.OidExpediente);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;

                    case ETipoInforme.TransportistaOrigen:

                        if (item.ETipoAcreedor != ETipoAcreedor.TransportistaOrigen) continue;

                        if (filter.objeto_detallado != null)
                            if (item.OidAcreedor.Equals(((TransporterInfo)filter.objeto_detallado).Oid)) continue;

                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);

                        obj = expedientes.GetItem(item.OidExpediente);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;

                    case ETipoInforme.Todos:

                        oidS = ((long)(item.TipoAcreedor + 1)).ToString("00") + "00000" + item.OidAcreedor.ToString();
                        oid = Convert.ToInt64(oidS);

                        obj = expedientes.GetItem(item.OidExpediente);
                        lista.Add(item.GetControlPagosPrintObject(obj, resumen.GetItem(oid)));

                        break;
                }
            }

            PaymentsReportRpt doc = new PaymentsReportRpt();

            if (format.Vista == EReportVista.Resumido)
            {
                doc.TitulosDetalle.SectionFormat.EnableSuppress = true;
                doc.Detalle.SectionFormat.EnableSuppress = true;
                doc.GHElemento.SectionFormat.EnableSuppress = true;
                doc.ReportDefinition.ReportObjects["SubtotalesAcreedorLB"].ObjectFormat.EnableSuppress = true;
            }
            else
            {
                doc.TitulosResumido.SectionFormat.EnableSuppress = true;
                doc.ReportDefinition.ReportObjects["NombreAcreedor"].ObjectFormat.EnableSuppress = true;
            }

            doc.SetDataSource(lista);

            FormatHeader(doc);

            return doc;
        }

        #endregion

        #region Sergio

        protected bool CompFechas(ReportFilter conf,
                                    DateTime fecha_prevision,
                                    DateTime fecha_pago)
        {
            if (fecha_pago < conf.fecha_pago_inicio) return false;
            if (fecha_pago > conf.fecha_pago_final) return false;
            if (fecha_prevision < conf.prevision_ini) return false;
            if (fecha_prevision > conf.prevision_fin) return false;

            return true;
        }

        #endregion
    }
}
