using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class StockList : ReadOnlyListBaseEx<StockList, StockInfo>
    {
        #region Business Methods

		public bool HasAlbaran()
		{
			return Items.FirstOrDefault(x => x.OidAlbaran != 0) != null;

			//foreach (StockInfo obj in this)
			//    if (obj.OidAlbaran != 0)
			//        return true;

			//return false;
		}

        public void FillPurchasePrices()
        {
            Dictionary<long, decimal> average = new Dictionary<long,decimal>();
            decimal current_stock_percent = 0;
            decimal item_stock_percent = 0;

            foreach (StockInfo item in Items)
            {
                if (item.ETipoStock == ETipoStock.Todos) continue;

                if (!average.ContainsKey(item.OidProducto))
                    average.Add(item.OidProducto, item.PurchasePrice);

                if (item.ETipoStock == ETipoStock.Compra)
                {
                    item_stock_percent = (item.KilosActuales != 0)
                                            ? item.Kilos / item.KilosActuales
                                            : 1;
                    
                    if ((item.KilosActuales - item.Kilos) != 0)
                        current_stock_percent = (item.KilosActuales - item.Kilos) / item.KilosActuales;
                    else
                        current_stock_percent = 0;

                    decimal[] prices = { item.PurchasePrice * item_stock_percent, average[item.OidProducto] * current_stock_percent };

                    average[item.OidProducto] = prices.Sum();
                }

                item.AvgPurchasePrice = average[item.OidProducto];

                DateTime last = Items.Where(x =>
                                        x.OidProducto == item.OidProducto &&
                                        x.ETipoStock == ETipoStock.Compra
                                    ).Max(y => y.Fecha);

                item.LastPurchasePrice = Items.Where(x => 
                                            x.OidProducto == item.OidProducto && 
                                            x.ETipoStock == ETipoStock.Compra &&
                                            x.Fecha == last
                                        ).Average(x => x.PurchasePrice);
            }
        }

		public virtual void InsertInitialLine(int pos)
		{
			if (this.Count <= pos) return;
			
			StockInfo initialLine = StockInfo.New();

			initialLine.Oid = -pos;
			initialLine.OidProducto = this[pos].OidProducto;
			initialLine.Producto = this[pos].Producto;
			initialLine.OidCliente = this[pos].OidCliente;
			initialLine.Observaciones = "Existencias anteriores";
			initialLine.KilosActuales = this[pos].KilosAcumulados;
			initialLine.KilosAcumulados = initialLine.KilosActuales;
			initialLine.BultosActuales = this[pos].BultosAcumulados;
			initialLine.BultosAcumulados = initialLine.BultosActuales;
			initialLine.Fecha = this[pos].Fecha.AddSeconds(-1);

			this.AddItem(initialLine, pos);
		}

		public virtual void InsertInitialLines()
		{
			if (this.Count == 0) return;

			List<long> products = new List<long>(
													(from st in Items
													select st.OidProducto).Distinct()
												);

			foreach (long item in products)
			{
				InsertInitialLine(Items.IndexOf(Items.FirstOrDefault(x => x.OidProducto == item)));
			}
		}

        public virtual decimal TotalKgs()
        {
            return this.Sum(x => x.Kilos);
        }
        public virtual decimal TotalUds()
        {
            return this.Sum(x => x.Bultos);
        }

        public virtual void UpdateStocksByBatch(bool throwException)
        {
			long oidPartida = -1;
            decimal stockKilos = 0;
            decimal stockBultos = 0;

            foreach (StockInfo item in this)
            {
				if (oidPartida != item.OidPartida)
				{
					stockKilos = item.KilosAcumulados;
					stockBultos = item.BultosAcumulados;
					oidPartida = item.OidPartida;
				}

                stockKilos += item.Kilos;
                stockBultos += item.Bultos;

                item.KilosActuales = stockKilos;
                item.BultosActuales = stockBultos;
            }

            if (throwException && (stockKilos < 0 || stockBultos < 0))
                throw new iQException(Resources.Messages.AVISO_STOCK_NEGATIVO);
        }

		public virtual void UpdateStocksByProduct(bool throwException = false)
		{
			long oidProduct = -1;
			decimal stockKilos = 0;
			decimal stockBultos = 0;

			foreach (StockInfo item in this)
			{
				if (oidProduct != item.OidProducto)
				{
					stockKilos = item.KilosAcumulados;
					stockBultos = item.BultosAcumulados;
					oidProduct = item.OidProducto;
				}

				stockKilos += item.Kilos;
				stockBultos += item.Bultos;

				item.KilosActuales = stockKilos;
				item.BultosActuales = stockBultos;
			}

			/*if (throwException && (stockKilos < 0 || stockBultos < 0))
				throw new iQException(Resources.Messages.AVISO_STOCK_NEGATIVO);*/
		}

        #endregion

		#region Common Factory Methods

		private StockList() {}
		private StockList(IDataReader reader)
		{
			Fetch(reader);
		}

		public static StockList GetChildList(IDataReader reader) { return new StockList(reader); }

        #endregion

		#region Child Factory Methods

		private StockList(IList<Stock> lista)
		{
			Fetch(lista);
		}

		public static StockList GetChildList(IList<StockInfo> list)
		{
			StockList flist = new StockList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (StockInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
		public static StockList GetChildList(IList<Stock> list) { return new StockList(list); }
        public static StockList GetChildList(ExpedientInfo parent, bool childs, bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Query = StockList.SELECT(parent);
			criteria.Childs = childs;

			StockList list = DataPortal.Fetch<StockList>(criteria);
			list.UpdateStocksByBatch(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion

		#region Root Factory Methods

		public static StockList NewList() { return new StockList(); }

		public static StockList GetList(bool childs = true)
		{
			return GetList(StockList.SELECT(), childs);
		}
		public static StockList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			StockList list = DataPortal.Fetch<StockList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static StockList GetList(QueryConditions conditions, bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = true;

			criteria.Query = StockList.SELECT(conditions);

			StockList list = DataPortal.Fetch<StockList>(criteria);
            list.UpdateStocksByBatch(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static StockList GetList(QueryConditions conditions, 
										ExpedientInfo from, 
										ExpedientInfo till,
										bool soloStock,
                                        bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = true;

			criteria.Query = StockList.SELECT_BATCH(conditions, from, till, soloStock);

			StockList list = DataPortal.Fetch<StockList>(criteria);

            list.UpdateStocksByBatch(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}
		
		public static StockList GetReportList(QueryConditions conditions,
								        ExpedientInfo from,
								        ExpedientInfo till,
								        bool soloStock,
								        bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = true;

			criteria.Query = StockList.SELECT_PRODUCT(conditions, from, till, soloStock);

			StockList list = DataPortal.Fetch<StockList>(criteria);

			list.InsertInitialLines();
			list.UpdateStocksByProduct(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static StockList GetByBatchList(long oidBatch, bool childs, bool throwStockException)
		{
			QueryConditions conditions = new QueryConditions { Partida = (oidBatch != 0) ? BatchInfo.New(oidBatch) : null };
			return GetList(StockList.SELECT_BATCH(conditions, null, null, false), false);
		}

        public static StockList GetListByExpediente(long oid, bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = true;

			Expedient item = Expedient.New();
			item.Oid = oid;
			criteria.Query = StockList.SELECT(item.GetInfo(false));

			StockList list = DataPortal.Fetch<StockList>(criteria);
            list.UpdateStocksByBatch(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}
        public static StockList GetListByProducto(long oid, bool childs, bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
			criteria.Childs = childs;

			Product item = Product.New();
			item.Oid = oid;
			criteria.Query = StockList.SELECT(item.GetInfo(false));

			StockList list = DataPortal.Fetch<StockList>(criteria);
            list.UpdateStocksByBatch(throwStockException);

			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion

		#region Root Data Access

		// called to retrieve data from db
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(StockInfo.GetChild(reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Child Data Access

		// called to copy objects data from list
        private void Fetch(IList<Stock> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Stock item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(StockInfo.GetChild(reader));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Stock.SELECT(conditions, false); }
		public static string SELECT(StoreInfo item)
		{
			string query = SELECT(new QueryConditions { Almacen = item });
			query += " ORDER BY \"ID_PARTIDA\", ST.\"FECHA\", ST.\"OID\"";
			return query;
		}
        public static string SELECT(ExpedientInfo item)
        {
            string query = SELECT(new QueryConditions { Expedient = item });
            query += " ORDER BY \"ID_PARTIDA\", ST.\"FECHA\", ST.\"OID\"";
            return query;
        }
		public static string SELECT(ProductInfo item)
		{
			string query = SELECT(new QueryConditions { Producto = item });
			query += " ORDER BY \"ID_PARTIDA\", ST.\"FECHA\", ST.\"OID\"";
			return query;
		}
		public static string SELECT_BATCH(QueryConditions conditions, ExpedientInfo from, ExpedientInfo till, bool soloStock)
		{
			string query = string.Empty;

			query = Stock.SELECT_MOVEMENTS_BATCH(conditions);

			if (from != null)
				query += " AND EX.\"CODIGO\" >='" + from.Codigo + "'";
			
			if (till != null)
				query += " AND EX.\"CODIGO\" <='" + till.Codigo + "'";

			if (soloStock)
				query += " AND (ST2.\"STOCK_KG\" > 0) AND (ST2.\"STOCK_BULTO\" > 0)";

			query += " ORDER BY \"ID_PARTIDA\", ST.\"FECHA\", ST.\"OID\"";

			return query;
		}
		public static string SELECT_PRODUCT(QueryConditions conditions, ExpedientInfo from, ExpedientInfo till, bool soloStock)
		{
			string query = string.Empty;

			query = Stock.SELECT_MOVEMENTS_PRODUCT(conditions);

			if (from != null)
				query += " AND EX.\"CODIGO\" >='" + from.Codigo + "'";

			if (till != null)
				query += " AND EX.\"CODIGO\" <='" + till.Codigo + "'";

			if (soloStock)
				query += " AND (ST2.\"STOCK_KG\" > 0) AND (ST2.\"STOCK_BULTO\" > 0)";

			query += " ORDER BY ST.\"OID_PRODUCTO\", ST.\"FECHA\", ST.\"OID\"";

			return query;
		}

		public static string SELECT(IStockable item)
        {
            string query = SELECT(new QueryConditions { IStockable = item });
            query += " ORDER BY ST.\"OID_BATCH\", ST.\"OID\"";
            return query;
        }
        public static string SELECT(InputDeliveryInfo item)
        {
            string query = SELECT(new QueryConditions { InputDelivery = item });
            query += " ORDER BY ST.\"OID_BATCH\", ST.\"OID\"";
            return query;
        }
        public static string SELECT(InputDeliveryLineInfo item)
        {
            string query = SELECT(new QueryConditions { ConceptoAlbaranProveedor = item });
            query += " ORDER BY ST.\"OID_BATCH\", ST.\"OID\"";
            return query;
        }
        
        #endregion	
	}
}