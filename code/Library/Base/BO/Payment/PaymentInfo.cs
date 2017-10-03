using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Globalization;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Hipatia;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
    public class PaymentInfo : ReadOnlyBaseEx<PaymentInfo>, IAgenteHipatia, BankLine.IBankLineInfo, IEntidadRegistroInfo, ITransactionPayment
	{
        #region IAgenteHipatia

        public string IDHipatia { get { return IdPago.ToString(Resources.Defaults.PAGO_CODE_FORMAT); } }
        public string NombreHipatia { get { return "Pago nº " + IDHipatia + " a " + Agente; ; } }
		public Type TipoEntidad { get { return typeof(Payment); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion
        
        #region IBankLineInfo

        public ETipoEntidad EEntityType { get { return moleQule.Common.Structs.ETipoEntidad.Pago; } }
		public long TipoMovimiento { get { return (long)ETipoMovimientoBanco; } }
        public EBankLineType ETipoMovimientoBanco { get { return moleQule.Store.Structs.EnumConvert.ToETipoMovimientoBanco(ETipoPago); } }
		public ETipoTitular ETipoTitular { get { return moleQule.Store.Structs.EnumConvert.ToETipoTitular(ETipoAcreedor); } }
        public EEstado EEstadoMov { get { return (EEstado)_base.Record.Estado; } set { _base.Record.Estado = (long)value; } }
		public string Titular { get { return Agente; } set {} }
        public long OidCuenta { get { return _base.Record.OidCuentaBancaria; } }
        public bool Confirmado { get { return EEstadoPago == EEstado.Pagado; } }
        public EEstado EEstadoOperacion { get { return EEstadoPago; } }
        public virtual long OidCreditCard { get { return OidTarjetaCredito; } }
        public Dictionary<string, object> Tags { get { return _base.Tags; } set { _base.Tags = value; } }

        #endregion

        #region ITransactionPayment

        public decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
        public decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
        public decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
        public decimal PendienteAsignar { get { return _base.Pendiente; } set { _base.Pendiente = value; } }
        public string FechaAsignacion { get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; } set { _base._fecha_asignacion = DateTime.Parse(value); } }
        public string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
        public decimal Total { get { return _base.Record.Importe; } set { } }
        public long OidExpediente { get { return 0; } set { } }

        public string NFactura { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }

        #endregion

		#region IEntidadRegistroInfo

		public moleQule.Common.Structs.ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.Pago; } }
		public string DescripcionRegistro { get { return "PAGO Nº " + IDPagoLabel + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + Agente; } }

        #endregion

        #region Attributes

        public PaymentBase _base = new PaymentBase();

        protected TransactionPaymentList _operations = null;
        protected ExpenseList _gastos = null;
        protected PaymentList _pagos = null;

        #endregion
        		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidUsuario { get { return _base.Record.OidUsuario; } }
        public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
        public long OidTarjetaCredito { get { return _base.Record.OidTarjetaCredito; } }
        public long OidAgente { get { return _base.Record.OidAgente; } }
        public long TipoPago { get { return _base.Record.Tipo; } }
        public string Codigo { get { return _base.Record.Codigo; } }
        public long Serial { get { return _base.Record.Serial; } }
        public long IdPago { get { return _base.Record.IdPago; } }
        public DateTime Fecha { get { return _base.Record.Fecha; } }
        public Decimal Importe { get { return _base.Record.Importe; } set { } }
        public Decimal GastosBancarios { get { return _base.Record.GastosBancarios; } }
        public long MedioPago { get { return _base.Record.MedioPago; } }
        public DateTime Vencimiento { get { return _base.Record.Vencimiento; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }
        public long TipoAgente { get { return _base.Record.TipoAgente; } }
        public long Estado { get { return _base.Record.Estado; } }
        public long OidRoot { get { return _base.Record.OidRoot; } }
        public long OidLink { get { return _base.Record.OidLink; } }
        public long EstadoPago { get { return _base.Record.EstadoPago; } }
		
		public TransactionPaymentList Operations { get { return _operations; } }
		public ExpenseList Gastos { get { return _gastos; } }
        public PaymentList Pagos { get { return _pagos; } }

        //Campos no enlazados
		public DateTime StepDate { get { return _base.StepDate; } }
        public string IDPagoLabel { get { return _base.PagoIDLabel; } }
        public string CuentaBancaria { get { return _base.BankAccount; } }
        public string Entidad { get { return _base.Bank; } }
        public string TarjetaCredito { get { return _base.CreditCard; } }
        public string IDLineaCaja { get { return _base.CashLineID; } }
        public string IDMovimientoBanco { get { return _base.BankLineID; } }
        public string IDMovimientoContable { get { return _base.AccountingLineID; } }
        public decimal Pendiente 
        { 
            get 
            { 
                return (EEstado == EEstado.Anulado)
                    ? 0
                    : (Operations == null) 
                        ? _base.Pendiente 
                        : _base.Record.Importe - Operations.GetTotal(); 
            } 
            set { _base.Pendiente = value; } 
        }
        public string Agente { get { return _base.Agent; } }
        public string CodigoAgente { get { return _base.AgentID; } }
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }
        public string NombreTipoAgente { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
        public EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public EEstado EEstado { get { return _base.EStatus; } }
		public string EstadoLabel { get { return _base.StatusLabel; } } /*DEPRECATED*/
        public string SatusLabel { get { return _base.StatusLabel; } }
        public EEstado EEstadoPago { get { return _base.EPaymentStatus; } }
        public string EstadoPagoLabel { get { return _base.PaymentStatusLabel; } }
        public ETipoPago ETipoPago { get { return _base.ETipoPago; } set { _base.ETipoPago = value; } }
		public string TipoPagoLabel { get { return _base.TipoPagoLabel; } }
        public string Usuario { get { return _base.Owner; } }
        public virtual bool Pagado { get { return EEstadoPago == EEstado.Pagado; } }
        public virtual DateTime FechaOrdenacion { get { return _base._fecha_ordenacion; } }
        public Decimal InterestCharges { get { return (_pagos != null && ETipoPago == ETipoPago.FraccionadoTarjeta) ? _pagos.Charges() : 0; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Payment source) 
        { 
            _base.CopyValues(source);
            Tags = Tags;
        }

        public void Vincula()
        {
            Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
            Asignado = Importe;
            Pendiente = 0;
        }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected PaymentInfo() { /* require use of factory methods */ }
		private PaymentInfo(int sessionCode, IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal PaymentInfo(Payment item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				switch (ETipoPago)
				{
					case ETipoPago.Factura:
						{
							_operations = (item.Operations != null) ? TransactionPaymentList.GetChildList(item.Operations) : null;
						} 
						break;

                    case ETipoPago.Fraccionado:
                        {
                            _pagos = (item.Pagos != null) ? PaymentList.GetChildList(item.Pagos) : null;
                        }
                        break;
                    case ETipoPago.Gasto:
                        {
                            _operations = (item.Operations != null) ? TransactionPaymentList.GetChildList(item.Operations) : null;
                            _gastos = (item.Gastos != null) ? ExpenseList.GetChildList(item.Gastos) : null;
                        }
                        break;

					default:
						{
							_gastos = (item.Gastos != null) ? ExpenseList.GetChildList(item.Gastos) : null;
						}
						break;
				}
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static PaymentInfo GetChild(int sessionCode, IDataReader reader)
        {
			return GetChild(sessionCode, reader, false);
		}
		public static PaymentInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new PaymentInfo(sessionCode, reader, childs);
		}

		public virtual void LoadChilds(IList<TransactionPaymentInfo> list) { _operations = TransactionPaymentList.GetChildList(list); }

		public static PaymentInfo New(long oid = 0) { return new PaymentInfo() { Oid = oid }; }
        public static PaymentInfo New(long oid, ETipoPago paymentType) { return new PaymentInfo() { Oid = oid, ETipoPago = paymentType }; }

 		#endregion
		
		#region Root Factory Methods

		public static PaymentInfo Get(long oid) { return Get(oid, false); }
		public static PaymentInfo Get(long oid, bool childs) { return Get(oid, ETipoAcreedor.Todos, childs); }
        public static PaymentInfo Get(long oid, ETipoAcreedor tipo) { return Get(oid, ETipoPago.Factura, tipo, false); }
		public static PaymentInfo Get(long oid, ETipoAcreedor tipo, bool childs) { return Get(oid, ETipoPago.Todos, tipo, childs); }
		public static PaymentInfo Get(long oid, ETipoPago tipo) { return Get(oid, tipo, ETipoAcreedor.Todos, false); }
		public static PaymentInfo Get(long oid, ETipoPago tipo, bool childs) { return Get(oid, tipo, ETipoAcreedor.Todos, childs); }
		public static PaymentInfo Get(long oid, ETipoPago tipo, ETipoAcreedor tipo_acreedor, bool childs)
		{
			CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PaymentInfo.SELECT(oid, tipo, tipo_acreedor);

			PaymentInfo obj = DataPortal.Fetch<PaymentInfo>(criteria);
			Payment.CloseSession(criteria.SessionCode);
			return obj;
		}

        public static PaymentInfo GetByVencimientoTarjeta(CreditCardInfo tarjeta, DateTime vencimiento, bool childs)
        {
            CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions()
            {
                FechaIni = vencimiento,
                FechaFin = vencimiento,
                PaymentType = ETipoPago.FraccionadoTarjeta,
                MedioPago = EMedioPago.Tarjeta,
                TarjetaCredito = tarjeta
            };

            criteria.Query = Payment.SELECT(conditions, true);

            PaymentInfo obj = DataPortal.Fetch<PaymentInfo>(criteria);
            Payment.CloseSession(criteria.SessionCode);
            return obj;
        }
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

						switch (ETipoPago)
						{
							case ETipoPago.Factura:
                            case ETipoPago.ExtractoTarjeta:
								{
									query = TransactionPaymentList.SELECT(this);
									reader = nHMng.SQLNativeSelect(query, Session());
									_operations = TransactionPaymentList.GetChildList(reader);
								}
								break;

                            case ETipoPago.Fraccionado:
                            case ETipoPago.FraccionadoTarjeta:
                                {
                                    query = Library.Store.Payment.SELECT_BY_ROOT(Oid, false);
                                    reader = nHMng.SQLNativeSelect(query, Session());
                                    _pagos = PaymentList.GetChildList(this.SessionCode, reader);
                                }
                                break;

							default:
								{
									query = ExpenseList.SELECT(this);
									reader = nHMng.SQLNativeSelect(query, Session());
									_gastos = ExpenseList.GetChildList(reader);
								}
								break;
						}
                    }
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion
		
		#region Child Data Access
		
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					switch (ETipoPago)
					{
                        case ETipoPago.Factura:
							{
								query = TransactionPaymentList.SELECT(this);
								reader = nHMng.SQLNativeSelect(query, Session());
								_operations = TransactionPaymentList.GetChildList(reader);
							}
							break;

                        case ETipoPago.Nomina:
                            {
                                query = TransactionPaymentList.SELECT(this);
                                reader = nHMng.SQLNativeSelect(query, Session());
                                _operations = TransactionPaymentList.GetChildList(reader);

                                query = ExpenseList.SELECT(this);
                                reader = nHMng.SQLNativeSelect(query, Session());
                                _gastos = ExpenseList.GetChildList(reader);
                            }
                            break;

                        case ETipoPago.Fraccionado:
                            {
								query = Library.Store.Payment.SELECT_BY_ROOT(Oid, false);
                                reader = nHMng.SQLNativeSelect(query, Session());
                                _pagos = PaymentList.GetChildList(this.SessionCode, reader);
                            }
                            break;

						default:
							{
								query = ExpenseList.SELECT(this);
								reader = nHMng.SQLNativeSelect(query, Session());
								_gastos = ExpenseList.GetChildList(reader);
							} 
							break;
					}
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid, ETipoPago tipo, ETipoAcreedor tipo_acreedor) { return Payment.SELECT(oid, tipo, tipo_acreedor, false); }

        #endregion 		
	}

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class PaymentSerialInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected PaymentSerialInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

		public static PaymentSerialInfo Get(ETipoAcreedor agente, long oid_agente) { return Get(agente, oid_agente, DateTime.MinValue.Year); }
		public static PaymentSerialInfo Get(ETipoAcreedor agente, long oid_agente, int year)
		{
			CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(agente, oid_agente, year);

			PaymentSerialInfo obj = DataPortal.Fetch<PaymentSerialInfo>(criteria);
			Payment.CloseSession(criteria.SessionCode);
			return obj;
		}
		public static PaymentSerialInfo Get(ETipoPago tipo, int year)
        {
            CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(tipo, year);

            PaymentSerialInfo obj = DataPortal.Fetch<PaymentSerialInfo>(criteria);
            Payment.CloseSession(criteria.SessionCode);
            return obj;
        }

		public static long GetNext(ETipoAcreedor agente, long oid_agente) { return Get(agente, oid_agente).Value + 1; }
		public static long GetNext(ETipoAcreedor agente, long oid_agente, int year) { return Get(agente, oid_agente, year).Value + 1; }
		public static long GetNext(ETipoPago tipo, int year) { return Get(tipo, year).Value + 1; }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(ETipoAcreedor agente, long oid_agente, int year)
        {
            string p = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string a = ProviderBaseInfo.TABLE(agente);
            string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				Acreedor = ProviderBaseInfo.New(oid_agente, agente),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			query = "SELECT 0 AS \"OID\", MAX(\"ID_PAGO\") AS \"SERIAL\"" +
					" FROM " + p + " AS P" +
					" INNER JOIN " + a + " AS A ON P.\"OID_AGENTE\" = A.\"OID\"" +
					" WHERE \"TIPO_AGENTE\" = " + conditions.Acreedor.TipoAcreedor +
					" AND P.\"OID_AGENTE\" = " + conditions.Acreedor.Oid;

			if (year != DateTime.MinValue.Year)
				query += " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query;
        }

		public static string SELECT(ETipoPago tipo, int year)
		{
            string p = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions 
			{ 
				PaymentType = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
					" FROM " + p + " AS P";

			query += " WHERE TRUE";

			if (conditions.PaymentType != ETipoPago.Todos)
				query += " AND \"TIPO\" = " + (long)conditions.PaymentType;

			if (year != DateTime.MinValue.Year)
				query += " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			return query;
		}
        
		#endregion
    }
}