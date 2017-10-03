using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Globalization;

using Csla;
using NHibernate;
using moleQule;
using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class BatchInfo : ReadOnlyBaseEx<BatchInfo>
	{	
		#region Attributes

		public BatchBase _base = new BatchBase();

        private BatchList _componentes;

        #endregion

        #region Properties

        public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
        public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidProveedor { get { return _base.Record.OidProveedor; } }
        public long OidKit { get { return _base.Record.OidKit; }  }
        public long OidPartida { get { return _base.Record.OidBatch; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Tipo { get { return _base.Tipo; } }
		public DateTime FechaCompra { get { return _base.Record.FechaCompra; } }
		public string TipoMercancia { get { return _base.Record.TipoMercancia; } }
        public Decimal BultosIniciales { get { return _base.Record.BultosIniciales; } }
		public Decimal KilosIniciales { get { return _base.Record.KilosIniciales; } }
        public Decimal StockBultos { get { return _base.StockBultos; } set { _base.StockBultos = value; } }
		public Decimal StockKilos { get { return _base.StockKilos; } set { _base.StockKilos = value; } }
        public Decimal PrecioVentaKilo { get { return _base.Record.PrecioVentaKilo; } }
		public Decimal PrecioCompraKilo { get { return _base.Record.PrecioCompraKilo; } }
		public Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public Decimal PrecioVentaBulto { get { return _base.PrecioVentaBulto; } }
		public Decimal CosteKilo { get { return _base.CosteKilo; } }
        public Decimal GastoKilo { get { return _base.Record.GastoKilo; } }
		public DateTime ReaFechaCobro { get { return _base.Record.ReaFechaCobro; } }
		public bool ReaCobrada { get { return _base.Record.ReaCobrada; } }
		public bool Ayuda { get { return _base.Record.Ayuda; } }
		public Decimal AyudaRecibidaKilo { get { return _base.Record.AyudaRecibidaKilo; } }
		public Decimal Proporcion { get { return _base._proporcion; } }
		public string Ubicacion { get { return _base.Record.Ubicacion; } }
		public string ObservacionesPE { get { return _base.Record.Observaciones; } }
		
        public BatchList Componentes { get { return _componentes; } }

        //NO ENLAZADAS
		public virtual ETipoPartida ETipoPartida { get { return (ETipoPartida)_base.Tipo; } set { _base.Tipo = (long)value; } }
		public virtual string TipoPartidaLabel { get { return moleQule.Store.Structs.EnumText<ETipoPartida>.GetLabel(ETipoPartida); } }
		public virtual long OidAlbaran { get { return _base._oid_albaran; } set { _base._oid_albaran = value; } }
		public virtual string NAlbaran { get { return _base._n_albaran; } set { _base._n_albaran = value; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual string Proveedor { get { return _base._proveedor; } set { _base._proveedor = value; } }
        public virtual string CodigoProducto { get { return _base._codigo_producto; } set { _base._codigo_producto = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
        public virtual string Family { get { return _base.Family; } set { _base.Family = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Expediente { get { return _base.Expedient; } set { _base.Expedient = value; } }
		public virtual string Observaciones { get { return _base._obs_expediente; } set { _base._obs_expediente = value; } }
		public virtual decimal KilosPorBulto { get { return _base.KilosPorBulto; } }
		public virtual decimal PrecioCompraProducto
		{
			get
			{
				return _base._precio_compra_producto;
			}
			set
			{
				if (!_base._precio_compra_producto.Equals(value))
				{
					_base._precio_compra_producto = value;
				}
			}
		}
		public virtual decimal PrecioCompraProveedor
		{
			get
			{
				return _base._precio_compra_proveedor;
			}
			set
			{
				if (!_base._precio_compra_proveedor.Equals(value))
				{
					_base._precio_compra_proveedor = value;
				}
			}
		}
		public virtual Decimal PrecioCompraBulto { get { return _base.PrecioCompraBulto; } }
		public virtual Decimal PrecioCompraKgAyuda { get { return _base.PrecioCompraKgAyuda; } }
		public virtual Decimal AyudaKiloEstimada { get { return _base._ayuda_kilo_estimada; } set { _base._ayuda_kilo_estimada = value; } }
		public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } }
		public virtual Decimal BeneficioTotalEstimado { get { return _base.BeneficioTotalEstimado; } }
		public virtual Decimal CosteBulto { get { return _base.CosteBulto; } }
		//Coste Neto = Coste + Gasto - Ayudas
		public virtual Decimal CosteNetoKg { get { return _base.CosteNetoKg; } }
		public virtual Decimal CosteNetoUd { get { return _base.CosteNetoUd; } }
		public virtual Decimal CosteNeto { get { return _base.CosteNeto; } }
		public virtual Decimal KilosMezcla
		{
			get
			{
				decimal value = 0;
                if (Componentes != null)
                {
                    foreach (BatchInfo item in Componentes)
                        value += item.KilosIniciales;
                }
				return value;
			}
		}
		public bool IsKit { get { return Componentes != null ? Componentes.Count > 0 : false; } }
		public bool IsKitComponent { get { return _base.IsKitComponent; } }
		public long OidImpuestoVenta { get { return _base._oid_impuesto_venta; } }
		public decimal PImpuestoVenta { get { return _base._p_impuesto_venta; } }
		public long OidSerie { get { return _base._oid_serie; } set { _base._oid_serie = value; } }
		public string CodigoAduanero { get { return _base._codigo_aduanero; } set { _base._codigo_aduanero = value; } }
		public decimal CosteTotal { get { return _base.CosteTotal; } }
		public decimal ValoracionTotal { get { return _base.ValoracionTotal; } }
        public decimal StockMinimo { get { return _base._stock_minimo; } }

		#endregion
		
		#region Business Methods
		
        public void CopyFrom(Batch source) { _base.CopyValues(source); }

        public bool CheckStock(IStockable source, ProductInfo product)
        {
            switch (source.EEntityType)
            {
                case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:
                    return CheckStockOutputDeliveryLine(source, product);

                default:
                    throw new iQImplementationException(string.Format("CheckStock para {0}", source.EEntityType.ToString()));
            }
        }
        public bool CheckStockOutputDeliveryLine(IStockable source, ProductInfo product)
        {
            //Si es un componente de kit no controlamos el stock porque ya lo hace el 
            //concepto asociado al kit
            if (!IsKitComponent)
            {
                if (source.InvoicingMode == ETipoFacturacion.Peso)
                {
                    if (StockKilos - product.StockMinimo - source.Kilos < 0)
                        return false;
                }
                else
                {
                    //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                    if (StockBultos - product.StockMinimo - source.Pieces < 0)
                        return false;
                }
            }
            return true;
        }

        public void CalculaCostes(decimal gastoPorKilo, decimal ayudaKilo)
        {
            _base.Record.GastoKilo = gastoPorKilo;
            _base.Record.AyudaRecibidaKilo = ayudaKilo;

            ///if (KilosIniciales > 0)
            //{
            //CosteKilo = PrecioCompraKilo + GastoKilo;
            //BeneficioKilo = PrecioVentaKilo + AyudaKilo - PrecioCompraKilo - GastoKilo;
            //}
            //else
            //{
            //CosteKilo = 0;
            //BeneficioKilo = 0;
            //}

            //PrecioVentaBulto = PrecioVentaKilo * KilosPorBulto;
        }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected BatchInfo() { /* require use of factory methods */ }
		private BatchInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="BusinessBaseEx"/> origen</param>
        /// <param name="childs">Flag para copiar los hijos</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		internal BatchInfo(Batch item, bool childs)
		{
			_base.CopyValues(item);

            if (childs)
            {
                if (item.Componentes != null)
                    _componentes = BatchList.GetChildList(item.Componentes);
            }
		}
	
		public static BatchInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }		
		public static BatchInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new BatchInfo(sessionCode, reader, childs);
		}

        public PartidaPrint GetPrintObject() { return PartidaPrint.New(this); }

		public static BatchInfo New(long oid = 0) { return new BatchInfo() { Oid = oid }; }

 		#endregion

        #region Root Factory Methods

        public static BatchInfo Get(long oid, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BatchInfo.SELECT(oid);
            else
                criteria.AddOidSearch(oid);

            criteria.Childs = childs;
            BatchInfo obj = DataPortal.Fetch<BatchInfo>(criteria);
            Batch.CloseSession(criteria.SessionCode);
            return obj;
        }
		public static BatchInfo Get(long oid, bool childs, bool cache)
		{
			BatchInfo item;

            if (!cache) return Get(oid, childs);

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(BatchList)))
			{
				BatchList items = BatchList.NewList();

				item = BatchInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(BatchList), items);
			}
			else
			{
				BatchList items = Cache.Instance.Get(typeof(BatchList)) as BatchList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = BatchInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(BatchList), items);
				}
			}

			return item;
		}

        #endregion

		#region Child Factory Methods

		#endregion

		#region Child Data Access

		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

                if (Childs)
                {
                    string query;
                    IDataReader reader;

                    query = BatchList.SELECT_BY_KIT(Oid);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_componentes = BatchList.GetChildList(SessionCode, reader, true);
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
        }

        #endregion

        #region Root Data Access

        // called to retrieve data from db
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

                        query = BatchList.SELECT_BY_KIT(this.Oid);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_componentes = BatchList.GetChildList(SessionCode, reader, true);
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

        public static string SELECT(long oid) { return Batch.SELECT(oid, false); }

        #endregion
    }

	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class BatchSerialInfo : SerialInfo
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
		protected BatchSerialInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		public static BatchSerialInfo Get(long oidProduct, int year) 
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = false;

			QueryConditions conditions = new QueryConditions 
			{
				Producto = ProductInfo.New(oidProduct),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(conditions);

			BatchSerialInfo obj = DataPortal.Fetch<BatchSerialInfo>(criteria);
			Payment.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static long GetLast(long oidProduct, int year) { return Get(oidProduct, year).Value; }
		public static long GetNext(long oidProduct, int year) { return Get(oidProduct, year).Value + 1; }

		#endregion

		#region SQL

		public static string SELECT(QueryConditions conditions)
		{
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
            
			string a = string.Empty;
			string query = string.Empty;

			query = @"
			SELECT 0 AS ""OID"", MAX(BA.""SERIAL"") AS ""SERIAL""
			FROM " + ba + @" AS BA
			INNER JOIN " + idl + @" AS IDL ON IDL.""OID_BATCH"" = BA.""OID""
			INNER JOIN " + id + @" AS ID ON ID.""OID"" = IDL.""OID_ALBARAN"" AND ID.""RECTIFICATIVO"" = FALSE";

			query += @"
			WHERE BA.""FECHA_COMPRA"" BETWEEN '" + conditions.FechaIniLabel + @"' AND '" + conditions.FechaFinLabel + "'";

			query += @"
				AND BA.""OID_PRODUCTO"" = " + (long)conditions.Producto.Oid;

			return query;
		}

		#endregion
	}
}