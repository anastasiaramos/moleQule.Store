using System;
using System.Collections.Generic;

using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.BankLine;
using moleQule.Common;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	#region Querys

    public class QueryConditions : moleQule.QueryConditions
    {
        public IQueryableEntity Entity { get { return Entities[0]; } }
        public QueryableEntityList Entities = new QueryableEntityList();
		public long Oid = 0;
		public moleQule.Common.Structs.ETipoEntidad EntityType = moleQule.Common.Structs.ETipoEntidad.Todos;
		public EEstado[] Status = null;

		public StoreInfo Almacen = null;
        public IQueryableEntity Client { get { return Get(ETipoEntidad.Cliente); } set { if (value != null) Add(value); } }
        public CreditCardInfo TarjetaCredito = null;
        public CreditCardStatementInfo CreditCardStatement = null;
        public IBankLineInfo IBankLine = null;
        public InputDeliveryLineInfo ConceptoAlbaranProveedor = null;
		public InputInvoiceLineInfo ConceptoFacturaRecibida = null;
        public IStockable IStockable = null;
        public ITitular Holder = null;
        public BankAccountInfo CuentaBancaria = null;
        public ExpedientInfo Expedient = null;
		public ExpedienteREAInfo ExpedienteREA = null;
		public InputDeliveryInfo InputDelivery = null;
		public LivestockBookInfo LibroGanadero = null;
		public LineaFomentoInfo ExpedienteFomento = null;
		public LivestockBookLineInfo LineaLibroGanadero = null;
		public LineaPedidoProveedorInfo LineaPedidoProveedor = null;
        public LoanInfo Loan = null;
        public InputInvoiceInfo FacturaRecibida = null;
        public FamiliaInfo Familia = null;
		public ExpenseInfo Gasto = null;
		public IAcreedorInfo Acreedor = null;
        public InventarioAlmacenInfo InventarioAlmacen = null;
		public ProductInfo Kit = null;
		public MaquinariaInfo Maquinaria = null;
		public Modelo Modelo = null;
		public BankLineInfo BankLine = null;
		public NavieraInfo Naviera = null;
		public NominaInfo Nomina = null;
        public IQueryableEntity OutputDelivery { get { return Get(ETipoEntidad.OutputDelivery); } set { if (value != null) Add(value); } }
        public IQueryableEntity OutputDeliveryLine { get { return Get(ETipoEntidad.OutputDeliveryLine); } set { if (value != null) Add(value); } }
		public PaymentInfo Payment = null;
        public BatchInfo Partida = null;
		public PedidoProveedorInfo PedidoProveedor = null;
		public PrecioDestinoInfo PrecioDestino = null;
        public LoanInfo Prestamo = null;
		public ProductInfo Producto = null;
		public ProductoProveedorInfo ProductoProveedor = null;
		public PuertoInfo Puerto = null;
		public PuertoDespachanteInfo PuertoDespachante = null;
        public ProveedorInfo Proveedor = null;
		public PayrollBatchInfo RemesaNomina = null;
		public RazaAnimalInfo RazaAnimal = null;
        public IQueryableEntity Serie { get { return Get(ETipoEntidad.Serie); } set { if (value != null) Add(value); } }
		public StockInfo Stock = null;
		public TipoAnimalInfo TipoAnimal = null;
		public TipoGanadoInfo TipoGanado = null;
		public TipoGastoInfo TipoGasto = null;
		public ToolInfo Tool = null;
		public UserInfo Usuario = null;
		public WorkReportInfo WorkReport = null;
		public WorkReportCategoryInfo WorkReportCategory = null;

        public EBankAccountLevel BankAccountLevel = EBankAccountLevel.Principal;
        public EBankLineType BankLineType = EBankLineType.Todos;
		public ECategoriaGasto CategoriaGasto = ECategoriaGasto.Todos;
		public EEstado Estado  = EEstado.Todos;
        public ELoanType LoanType = ELoanType.All;
		public EMedioPago MedioPago = EMedioPago.Todos;
		public ETipoAcreedor[] TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Todos };
		public ETipoAlbaranes TipoAlbaranes = ETipoAlbaranes.Todos;
        public ETipoAyudaContabilidad TipoAyudasContabilidad = ETipoAyudaContabilidad.Todas;
		public ETipoFacturas TipoFacturas = ETipoFacturas.Todas;
        public ETipoFactura TipoFactura = ETipoFactura.Todas;
        public ETipoLineaLibroGanadero TipoLineaLibroGanadero = ETipoLineaLibroGanadero.Todos;
		public EModelo EModelo = EModelo.Modelo111;
		public ETipoPago PaymentType = ETipoPago.Todos;
        public ETipoProducto TipoProducto = ETipoProducto.Todos;
        public ETipoExpediente TipoExpediente = ETipoExpediente.Todos;
        public ETipoFamilia TipoFamilia = ETipoFamilia.Todas;
        public ETipoSerie TipoSerie = ETipoSerie.Todas;
		public ETipoStock TipoStock = ETipoStock.Todos;
        public ETipoTarjeta TipoTarjeta = ETipoTarjeta.Todos;
        public ETipoTitular TipoTitular = ETipoTitular.Todos;

        public List<EMedioPago> MedioPagoList = null;

        public QueryConditions()
            : this(0, 0) { }
        public QueryConditions(long oidEntity, ETipoEntidad entityType)
            : this(new QueryableEntity() { Oid = oidEntity, EntityType = (long)entityType }) { }
        public QueryConditions(IQueryableEntity entity)
        {
            Entities.Add(entity);
        }

        public void Add(IQueryableEntity entity) { Entities.Add(entity); }
        public void Add(long oidEntity, ETipoEntidad entityType) { Entities.Add(oidEntity, (long)entityType); }
        public bool Contains(ETipoEntidad entityType) { return Entities.Contains((long)entityType); }
        public IQueryableEntity Get(ETipoEntidad entityType) { return Entities.Get((long)entityType); }

		public static Common.QueryConditions ConvertToCommonQuery(Store.QueryConditions conditions)
		{
			Common.QueryConditions conds = new Common.QueryConditions
			{
                Entity = conditions.Entity,
				Oid = conditions.Oid,
                OidEntity = conditions.OidEntity,
				EntityType = conditions.EntityType,
				Status = conditions.Status,

				FechaIni = conditions.FechaIni,
				FechaFin = conditions.FechaFin,
				FechaAuxIni = conditions.FechaAuxIni,
				FechaAuxFin = conditions.FechaAuxFin,
				Estado = conditions.Estado
			};

			return conds;
		}

        public static Store.QueryConditions ConvertTo(BankLine.QueryConditions conditions)
        {
            Store.QueryConditions conds = new Store.QueryConditions
            {
                FechaIni = conditions.FechaIni,
                FechaFin = conditions.FechaFin,
                FechaAuxIni = conditions.FechaAuxIni,
                FechaAuxFin = conditions.FechaAuxFin,
                Estado = conditions.Estado,

                BankAccountLevel = conditions.BankAccountLevel,
                CuentaBancaria = conditions.BankAccount,
                IBankLine = conditions.IBankLine,
                MedioPago = conditions.PaymentMethod,
                TipoTitular = conditions.TipoTitular,
                BankLineType = conditions.BankLineType
            };

            return conds;
        }
	}

	public delegate string SelectCaller(QueryConditions conditions);

	#endregion
   
	#region Formats & Reports

	public struct ReportFilter
	{
		public DateTime FechaIni, FechaFin;
		public object objeto_detallado;
		public EPagos tipo;
		public DateTime fecha_fac_inicio, fecha_fac_final, fecha_pago_inicio, fecha_pago_final, prevision_ini, prevision_fin;
		public ETipoExpediente tipo_expediente;
		public string exp_inicial, exp_final;
		public ETipoInforme tipo_informe;
		public bool SoloMermas;
		public bool SoloStock;
		public bool SoloIncompletos;
	}

	#endregion

	#region Enum Functions

	public static class EnumFunctions
	{
	}

	#endregion
}
