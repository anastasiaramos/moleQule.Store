using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

using moleQule.CslaEx;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store.Properties;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ModuleController
	{
		#region Attributes & Properties

		Dictionary<ETipoAcreedor, TProviderBase> _active_acreedores = new Dictionary<ETipoAcreedor, TProviderBase>();

		public Dictionary<ETipoAcreedor, TProviderBase> ActiveAcreedores { get { return _active_acreedores; } }

		#endregion

		#region Settings

		public static string FOTOS_EMPLEADOS_PATH { get { return Properties.Settings.Default.FOTO_EMPLEADO_PATH; } }

		#endregion

		#region Factory Methods

		/// <summary>
		/// Única instancia de la clase ControlerBase (Singleton)
		/// </summary>
		protected static ModuleController _main;

		/// <summary>
		/// Unique Controler Class Instance
		/// </summary>
		public static ModuleController Instance { get { return (_main != null) ? _main : new ModuleController(); } }

		/// <summary>
		/// Contructor 
		/// </summary>
		protected ModuleController()
		{
			// Singleton
			_main = this;

			Init();
		}

		private void Init() {}

		public static void CheckDBVersion()
		{
			ApplicationSettingInfo dbVersion = ApplicationSettingInfo.Get(Settings.Default.DB_VERSION_VARIABLE);

			//Version de base de datos equivalente o no existe la variable
			if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) == 0))
			{
				return;
			}
			//Version de base de datos superior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) > 0)
			{
				throw new iQException(String.Format(moleQule.Resources.Messages.DB_VERSION_HIGHER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
			//Version de base de datos inferior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) < 0)
			{
				throw new iQException(String.Format(moleQule.Resources.Messages.DB_VERSION_LOWER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
		}

		public static void UpgradeSettings() { ModulePrincipal.UpgradeSettings(); }

		#endregion

		#region Settings

		#endregion

		#region Business Methods

		public void AutoPilot()
		{
			//ShowApuntesPendientes();
		}

		public void ClearAcreedores()
		{
			if (_active_acreedores != null)
				_active_acreedores.Clear();
		}
		
		public void ActivateAcreedor(ETipoAcreedor tipo, string table)
		{
			TProviderBase acreedor = new TProviderBase();
			acreedor.Table = table;
			acreedor.Tipo = tipo;
			_active_acreedores.Add(tipo, acreedor);
		}
		
		public bool IsActive(ETipoAcreedor tipo)
		{
			return _active_acreedores[tipo].Active;
		}
		
		#endregion

		#region Scripts

        public static void CreateApuntesBancarios(PaymentList pagos)
        {
            List<PaymentInfo> list = new List<PaymentInfo>();

            CreditCardList tarjetas = CreditCardList.GetList();
            Payments pagos_tarjeta = Payments.NewList();

            foreach (PaymentInfo item in pagos)
            {
                if (!Common.EnumFunctions.NeedsCuentaBancaria(item.EMedioPago)) continue;
                if (item.Vencimiento > DateTime.Today) continue;

                if (item.EMedioPago != EMedioPago.Tarjeta)
                {
                    //Apunte bancario del pagaré, talón, etc..
                    BankLine.BankLine.InsertItem(item, true);

                    list.Add(item);
                }
                else
                {
                    Payment pago_tarjeta = pagos_tarjeta.GetItemByTarjetaCredito(item.OidTarjetaCredito, item.Vencimiento);

                    if (pago_tarjeta == null)
                    {
                        pago_tarjeta = pagos_tarjeta.NewItem(item, ETipoPago.ExtractoTarjeta);
                        TransactionPayment pf = pago_tarjeta.Operations.NewItem(pago_tarjeta, item, item.ETipoPago);
                        pf.Cantidad = item.Total;
                        pago_tarjeta.EEstadoPago = EEstado.Pagado;
                    }
                    else
                    {
                        pago_tarjeta.Importe += item.Importe;
                        pago_tarjeta.GastosBancarios += item.GastosBancarios;
                        TransactionPayment pf = pago_tarjeta.Operations.NewItem(pago_tarjeta, item, item.ETipoPago);
                        pf.Cantidad = item.Total;
                    }

                    list.Add(item);
                }
            }

            Payments pagos_fraccionados = Payments.NewList();
            pagos_fraccionados.OpenNewSession();

            //Apunte bancario de la tarjeta
            foreach (Payment item in pagos_tarjeta)
            {
                Payment root = pagos_fraccionados.NewItem(item.GetInfo(false), ETipoPago.FraccionadoTarjeta);
                root.Pagos.AddItem(item);

                //if (item.Importe != 0)
                //    MovimientoBanco.InsertItemTarjeta(item, tarjetas.GetItem(item.OidTarjetaCredito));						
            }

            pagos_fraccionados.BeginTransaction();
            pagos_fraccionados.Save();

            Payment.UpdatePagadoFromList(list, true);
        }

        public static void CreateCreditCardStatements()
        {
            QueryConditions conditions = new QueryConditions { MedioPago = EMedioPago.Tarjeta };
            Payments payments = Payments.GetList(conditions, false);
            CreditCardList credit_cards = CreditCardList.GetList(false);

            string message = string.Empty;

            foreach (Payment payment in payments)
            {
                if (payment.EEstado == EEstado.Anulado) continue;

                CreditCardInfo card = credit_cards.GetItem(payment.OidTarjetaCredito);

                if (card.ETipoTarjeta != ETipoTarjeta.Credito) continue;

                payment.MarkItemDirty();
            }

            payments.Save();
        }

        public static void CreateCreditCardStatementsPayments()
        {
            Payments statement_payments = Payments.GetCreditCardStatementsList(0, false);
            CreditCardList credit_cards = CreditCardList.GetList(false);

            foreach (Payment payment in statement_payments)
            {
                //Payment Transactions 
                payment.LoadChilds(typeof(TransactionPayment), false);

                CreditCardInfo card = credit_cards.GetItem(payment.OidTarjetaCredito);

                if (card.ETipoTarjeta != ETipoTarjeta.Credito) continue;

                //Extractos
                if (card.Statements == null)
                    card.LoadChilds(typeof(CreditCardStatement), false);

                foreach (CreditCardStatementInfo statement in card.Statements)
                {
                    if (statement.Amount == payment.Importe && statement.DueDate == payment.Vencimiento)
                    {
                        TransactionPayment operation = payment.Operations.NewItem(payment, statement, ETipoPago.ExtractoTarjeta);
                        operation.Cantidad = payment.Importe;                        
                    }
                }
            }

            statement_payments.Save();
        }

		public static ExpenseList GetGastosPendientes()
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Store.ModulePrincipal.GetNotifyPlazoGastos());
			ExpenseList list = ExpenseList.GetPendientesList(ECategoriaGasto.Generales, DateTime.MinValue, f_fin, false);

			return list;
		}

		public static InputInvoiceList GetFacturasRecibidasPendientes()
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Store.ModulePrincipal.GetNotifyPlazoFacturasRecibidas());
			InputInvoiceList list = InputInvoiceList.GetPendientesList(DateTime.MinValue, f_fin, EMedioPago.Giro, false);

			return list;
		}

        public static PaymentList GetPagosPendientesVencimiento(BankAccountInfo account)
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Store.ModulePrincipal.GetNotifyPlazoPagos());

			QueryConditions conditions = new QueryConditions
			{
				CuentaBancaria = account,
				FechaAuxIni = DateTime.MinValue,
				FechaAuxFin = f_fin,
			};

			PaymentList list = PaymentList.GetListByVencimiento(conditions, false);

			return list;
		}

        public static PaymentList GetPagosComercioExterior(BankAccountInfo account)
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Store.ModulePrincipal.GetNotifyPlazoPagos());

			QueryConditions conditions = new QueryConditions
			{
				CuentaBancaria = account,
				FechaAuxIni = DateTime.Now,
				FechaAuxFin = f_fin,
			};

			PaymentList list = PaymentList.GetListByVencimientoPrestamo(conditions, false);

			return list;
		}

		public static PaymentList GetPagosTarjetaVencidosSinApunte(DateTime fecha, CreditCardInfo tarjeta)
		{
			QueryConditions conditions = new QueryConditions
			{
				TarjetaCredito = tarjeta,
				MedioPago = EMedioPago.Tarjeta,
				FechaAuxFin = fecha
			};
			PaymentList list = PaymentList.GetListByVencimientoSinApunte(conditions, false);

			return list;
		}

		public static PaymentList GetPagosVencidosSinApunte(DateTime fecha)
		{
			QueryConditions conditions = new QueryConditions
			{
				MedioPago = EMedioPago.NoTarjeta,
				FechaAuxFin = fecha
			};
			PaymentList list = PaymentList.GetListByVencimientoSinApunte(conditions, false);

			return list;
		}

        public static void RellenaAlbaranesEnFacturas()
        {
            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
            {
                FechaIni = DateAndTime.FirstDay(2010),
                FechaFin = DateAndTime.LastDay(2010)
            };

            InputInvoices list_p = InputInvoices.GetList(conditions, false);

            foreach (InputInvoice item in list_p)
            {
                item.LoadChilds(typeof(AlbaranFacturaProveedor), false);
                item.SetAlbaranes();
            }

            list_p.Save();
            list_p.CloseSession();
        }

		#endregion
	}

	public class ModuleDef : IModuleDef
	{
		public string Name { get { return "Store"; } }
		public Type Type { get { return typeof(Library.Store.ModuleController); } }
		public Type[] Mappings
		{
			get
			{
				return new Type[] 
                {   
					typeof(moleQule.Store.Data.BatchMap),
                    typeof(moleQule.Store.Data.ClientProductMap),
					typeof(moleQule.Store.Data.CustomAgentMap),
                    typeof(CustomAgentPortMap),
                    typeof(DestinationPriceMap),
					typeof(moleQule.Store.Data.EmployeeMap),
					typeof(moleQule.Store.Data.ExpedientMap),
					typeof(moleQule.Store.Data.REAExpedientMap),
					typeof(moleQule.Store.Data.ExpenseTypeMap),
					typeof(moleQule.Store.Data.FamilyMap),
					typeof(moleQule.Store.Data.FamilySerieMap),
					typeof(moleQule.Store.Data.ExpenseMap),
                    typeof(moleQule.Store.Data.InterestRateMap),
   					typeof(moleQule.Store.Data.InputDeliveryMap),
					typeof(InputDeliveryInvoiceMap),
                    typeof(moleQule.Store.Data.InputDeliveryLineMap),
                    typeof(moleQule.Store.Data.InputInvoiceMap),
					typeof(moleQule.Store.Data.InputInvoiceLineMap),
                    typeof(InputOrderMap),
                    typeof(InputOrderLineMap),
                    typeof(InventarioAlmacenMap),
					typeof(KitMap),
					typeof(LivestockBookMap),
					typeof(moleQule.Store.Data.LineaFomentoMap),
					typeof(LineaInventarioMap),
                    typeof(LivestockBookLineMap),
                    typeof(moleQule.Store.Data.LoanMap),
					typeof(MaquinariaMap),                    
                    typeof(OriginPricenMap),
                    typeof(moleQule.Store.Data.PayrollMap),
					typeof(moleQule.Store.Data.PayrollBatchMap),
                    typeof(moleQule.Store.Data.PaymentMap),
					typeof(moleQule.Store.Data.ProductMap),
					typeof(PuertoMap),
                    typeof(RoutePriceMap),
					typeof(moleQule.Store.Data.SerieMap),
                    typeof(moleQule.Store.Data.ShippingCompanyMap),
					typeof(moleQule.Store.Data.StockMap),
                    typeof(StoreMap),
                    typeof(SupplierProductMap),
					typeof(moleQule.Store.Data.SupplierMap),
                    typeof(moleQule.Store.Data.TransactionPaymentMap),
                    typeof(moleQule.Store.Data.ToolMap),
					typeof(moleQule.Store.Data.TransporterMap),
					typeof(moleQule.Store.Data.WorkReportMap),
					typeof(moleQule.Store.Data.WorkReportCategoryMap),
					typeof(moleQule.Store.Data.WorkReportResourceMap),
                    
                    typeof(RazaAnimalMap),
    				typeof(TipoAnimalMap),
					typeof(TipoGanadoMap),
                };
			}
		}

		public void GetEntities(Dictionary<Type, Type> recordEntities)
		{
			if (recordEntities.ContainsKey(typeof(AlbaranFacturaProveedor))) return;

			recordEntities.Add(typeof(AlbaranFacturaProveedor), typeof(InputDeliveryInvoiceRecord));
            recordEntities.Add(typeof(InputDelivery), typeof(moleQule.Store.Data.InputDeliveryRecord));
			recordEntities.Add(typeof(Almacen), typeof(AlmacenRecord));
            recordEntities.Add(typeof(ProductoCliente), typeof(moleQule.Store.Data.ClientProductRecord));
            recordEntities.Add(typeof(InputDeliveryLine), typeof(moleQule.Store.Data.InputDeliveryLineRecord));
			recordEntities.Add(typeof(InputInvoiceLine), typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            recordEntities.Add(typeof(Despachante), typeof(moleQule.Store.Data.CustomAgentRecord));
            recordEntities.Add(typeof(Employee), typeof(moleQule.Store.Data.EmployeeRecord));
			recordEntities.Add(typeof(Expedient), typeof(moleQule.Store.Data.ExpedientRecord));
            recordEntities.Add(typeof(REAExpedient), typeof(moleQule.Store.Data.REAExpedientRecord));
			recordEntities.Add(typeof(InputInvoice), typeof(moleQule.Store.Data.InputInvoiceRecord));
			recordEntities.Add(typeof(moleQule.Serie.Familia), typeof(moleQule.Store.Data.FamilyRecord));
			recordEntities.Add(typeof(Expense), typeof(moleQule.Store.Data.ExpenseRecord));
            recordEntities.Add(typeof(InterestRate), typeof(moleQule.Store.Data.InterestRateRecord));
			recordEntities.Add(typeof(InventarioAlmacen), typeof(InventarioAlmacenRecord));
			recordEntities.Add(typeof(Kit), typeof(KitRecord));
			recordEntities.Add(typeof(LivestockBook), typeof(LivestockBookRecord));
            recordEntities.Add(typeof(LineaFomento), typeof(moleQule.Store.Data.LineaFomentoRecord));
			recordEntities.Add(typeof(LineaInventario), typeof(LineaInventarioRecord));
            recordEntities.Add(typeof(LivestockBookLine), typeof(LivestockBookLineRecord));
			recordEntities.Add(typeof(LineaPedidoProveedor), typeof(InputOrderLineRecord));
            recordEntities.Add(typeof(Loan), typeof(moleQule.Store.Data.LoanRecord));
            recordEntities.Add(typeof(Maquinaria), typeof(MaquinariaRecord));
			recordEntities.Add(typeof(Naviera), typeof(moleQule.Store.Data.ShippingCompanyRecord));
			recordEntities.Add(typeof(Payroll), typeof(moleQule.Store.Data.PayrollRecord));
			recordEntities.Add(typeof(Payment), typeof(moleQule.Store.Data.PaymentRecord));
            recordEntities.Add(typeof(TransactionPayment), typeof(moleQule.Store.Data.TransactionPaymentRecord));
			recordEntities.Add(typeof(Batch), typeof(moleQule.Store.Data.BatchRecord));
			recordEntities.Add(typeof(PedidoProveedor), typeof(InputOrderRecord));
			recordEntities.Add(typeof(PrecioTrayecto), typeof(PrecioTrayectoRecord));
			recordEntities.Add(typeof(Product), typeof(moleQule.Store.Data.ProductRecord));
			recordEntities.Add(typeof(ProductoProveedor), typeof(ProductoProveedorRecord));
			recordEntities.Add(typeof(Proveedor), typeof(moleQule.Store.Data.SupplierRecord));
			recordEntities.Add(typeof(Puerto), typeof(PuertoRecord));
			recordEntities.Add(typeof(PuertoDespachante), typeof(PuertoDespachanteRecord));
			recordEntities.Add(typeof(RazaAnimal), typeof(RazaAnimalRecord));
			recordEntities.Add(typeof(PayrollBatch), typeof(moleQule.Store.Data.PayrollBatchRecord));
			recordEntities.Add(typeof(moleQule.Serie.Serie), typeof(moleQule.Store.Data.SerieRecord));
			recordEntities.Add(typeof(moleQule.Serie.SerieFamilia), typeof(moleQule.Store.Data.FamilySerieRecord));
			recordEntities.Add(typeof(Stock), typeof(moleQule.Store.Data.StockRecord));
			recordEntities.Add(typeof(TipoAnimal), typeof(TipoAnimalRecord));
			recordEntities.Add(typeof(TipoGanado), typeof(TipoGanadoRecord));
            recordEntities.Add(typeof(TipoGasto), typeof(moleQule.Store.Data.ExpenseTypeRecord));
            recordEntities.Add(typeof(Tool), typeof(moleQule.Store.Data.ToolRecord));
			recordEntities.Add(typeof(Transporter), typeof(moleQule.Store.Data.TransporterRecord));
            recordEntities.Add(typeof(WorkReport), typeof(moleQule.Store.Data.WorkReportRecord));
            recordEntities.Add(typeof(WorkReportCategory), typeof(moleQule.Store.Data.WorkReportCategoryRecord));
            recordEntities.Add(typeof(WorkReportResource), typeof(moleQule.Store.Data.WorkReportResourceRecord));
		}
	}
}