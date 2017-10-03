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
using moleQule.Library.Store.Reports.Expense;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    public class ExpenseReportMng : BaseReportMng
    {
		#region Attributes & Properties

		public bool ShowQRCode { get; set; }

		#endregion

		#region Factory Methods

		public ExpenseReportMng() { }

		public ExpenseReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public ExpenseReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) { }

		#endregion

		#region Style

		private void FormatReport(ExpensesReportRpt rpt, ReportFilter filter, ReportFormat format)
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
		}

		#endregion

		#region Business Methods

        public ExpenseListRpt GetListReport(ExpenseList list)
		{
			if (list.Count == 0) return null;

            ExpenseListRpt doc = new ExpenseListRpt();

			List<GastoPrint> pList = new List<GastoPrint>();

			foreach (ExpenseInfo item in list)
				pList.Add(GastoPrint.New(item));

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

		private void CheckGasto(ECategoriaGasto category, ETipoAcreedor providerType, ExpedientInfo expedient, ExpenseList list, List<GastoPrint> pList)
		{
            ExpenseInfo info = list.GetItemByExpediente(expedient.Oid, providerType, category);
			if (info == null)
			{
				Expense item = Expense.New();
				item.Codigo = "---";
				item.OidExpediente = expedient.Oid;
				item.CodigoExpediente = expedient.Codigo;
				item.ECategoriaGasto = ECategoriaGasto.GeneralesExpediente;
                item.Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(providerType);
				item.ETipoAcreedor = providerType;
				pList.Add(GastoPrint.New(item.GetInfo(false)));
			}
		}

        public ExpensesReportRpt GetInformeGastoListReport(ExpenseList list, 
																ExpedienteList expedients, 
																ReportFilter filter,
																ReportFormat format)
        {
            if (list == null) return null;

            ExpensesReportRpt doc = new ExpensesReportRpt();

            List<GastoPrint> pList = new List<GastoPrint>();

			//long oid_exp = 0;

			foreach (ExpenseInfo item in list)
			{
				if (filter.SoloIncompletos)
					if (list.ExpedienteIsComplete(item.OidExpediente)) continue;

				pList.Add(GastoPrint.New(item));

                //if ((oid_exp != item.OidExpediente) && (item.OidExpediente != 0))
                //{
                //    oid_exp = item.OidExpediente;

                //    ExpedienteInfo expediente = expedientes.GetItem(item.OidExpediente);

                //    CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.Naviera, expediente, list, pList);
                //    CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.Despachante, expediente, list, pList);
                //    CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.TransportistaOrigen, expediente, list, pList);
                //    CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.TransportistaDestino, expediente, list, pList);
                //    CheckGasto(ECategoriaGasto.Stock, ETipoAcreedor.Proveedor, expediente, list, pList);
                //}
			}

            //foreach (ExpedienteInfo item in expedientes)
            //{
            //    if (!list.ExistsExpediente(item.Oid))
            //    {
            //        CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.Naviera, item, list, pList);
            //        CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.Despachante, item, list, pList);
            //        CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.TransportistaOrigen, item, list, pList);
            //        CheckGasto(ECategoriaGasto.GeneralesExpediente, ETipoAcreedor.TransportistaDestino, item, list, pList);
            //        CheckGasto(ECategoriaGasto.Stock, ETipoAcreedor.Proveedor, item, list, pList);
            //    }
            //}

            doc.SetDataSource(pList);

			FormatHeader(doc);
			FormatReport(doc, filter, format);

            return doc;
        }

		public ExpensesReportVerticalRpt GetInformeGastoVerticalListReport(ExpenseList list, 
																				ExpedienteList expedientes, 
																				ReportFilter filter,
                                                                                InputDeliveryLineList conceptos)
		{
			if (list == null) return null;

            ExpensesReportVerticalRpt doc = new ExpensesReportVerticalRpt();

            List<ResumenGastoPrint> pList = new List<ResumenGastoPrint>();
            InputDeliveryList albaranes = InputDeliveryList.GetList(false);
            
			foreach (ExpedientInfo item in expedientes)
			{
				if (filter.SoloIncompletos)
					if (list.ExpedienteIsComplete(item.Oid)) continue;
                
                ResumenGastoPrint registro = ResumenGastoPrint.New(item, list, conceptos, albaranes);

                if (registro.Proveedor != null
                    || registro.Naviera != null
                    || registro.Despachante != null
                    || registro.TransportistaDestino != null
                    || registro.TransportistaOrigen != null)
				    pList.Add(registro);
			}

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

        #endregion
    }
}