using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
    /// Read Only Root Collection of Business Objects
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class BatchList : ReadOnlyListBaseEx<BatchList, BatchInfo>
    {
        #region Business Methods

		public BatchInfo GetItemByProducto(long oid)
		{
			foreach (BatchInfo obj in this)
				if (obj.OidProducto == oid)
					return obj;
			return null;
		}

        public BatchInfo GetItemByExpediente(long oid_expediente)
        {
            foreach (BatchInfo obj in this)
                if (obj.OidExpediente == oid_expediente)
                    return obj;
            return null;
        }

        public BatchList GetSubListByExpediente(long oid_expediente)
        {
            BatchList lista = new BatchList();

            lista.IsReadOnly = false;
            foreach (BatchInfo pei in this)
            {
                if (pei.OidExpediente == oid_expediente)
                {
                    lista.AddItem(pei);
                }
            }
            lista.IsReadOnly = true;

            return lista;
        }

		public BatchInfo GetPartidaAgrupada(BatchInfo source)
		{
			Batch partida = Batch.NewChild();
			partida.CopyFrom(source);
			partida.Oid = source.Oid;

			foreach (BatchInfo item in this)
			{
				if ((item.CodigoAduanero == source.CodigoAduanero) &&
					(item.OidExpediente == source.OidExpediente))
				{
					partida.KilosIniciales += item.KilosIniciales;
				}
			}

			return partida.GetInfo(false);
		}

		public decimal TotalValorado()
		{
			decimal total = 0;
			foreach (BatchInfo item in this)
			{
				total += (item.StockKilos * item.PrecioVentaKilo);
			}

			return total;
		}

        public decimal GetTotalKilos(string codigoAduanero)
        {
            decimal kilos = 0;

            foreach (BatchInfo item in this)
            {
                if (item.CodigoAduanero == codigoAduanero)
                    kilos += item.KilosIniciales;
            }

            return kilos;
        }

        #endregion

        #region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BatchList() {}		
		private BatchList(IList<Batch> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private BatchList(IList<BatchInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }

		public List<BatchInfo> GetListByAlbaran(long oid_albaran)
		{
			List<BatchInfo> list = new List<BatchInfo>();

			foreach (BatchInfo item in this)
			{
				if (item.OidAlbaran == oid_albaran)
					list.Add(item);
			}

			return list;
		}

		#endregion
		
		#region Root Factory Methods

		public static BatchList NewList() { return new BatchList(); }

        public static BatchList GetList(bool childs = true)
        {
			return GetList(DateTime.MinValue, DateTime.MaxValue, childs);
        }
		public static BatchList GetList(int year, bool childs = true)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static BatchList GetList(	DateTime from,
											DateTime till, 
											bool childs = true)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(SELECT(conditions), childs);
		}

		public static BatchList GetList(string query, bool childs = true)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;

			BatchList list = DataPortal.Fetch<BatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static BatchList GetElaboradosList(bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

            criteria.Query = BatchList.SELECT_ELABORADOS();

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static BatchList GetByProductList(long oidProduct, bool childs)
        {
            return GetByProductList(oidProduct, 0, ETipoAcreedor.Todos, childs);
        }
        public static BatchList GetByProductList(long oidProduct, long oidProvider, ETipoAcreedor providerType, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions 
            { 
                Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
                Producto = ProductInfo.New(oidProduct) 
            };
            criteria.Query = SELECT(conditions);

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static BatchList GetByProductStockList(long oidProduct, long oidProvider, ETipoAcreedor providerType, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions
            {
                Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
                Producto = ProductInfo.New(oidProduct)
            };
            criteria.Query = Batch.SELECT_STOCK(conditions, false);

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        /// <summary>
        /// Partidas que no sean Componentes
        /// </summary>
        /// <returns></returns>
		public static BatchList GetListBySerieAndStock(long oidSerie, bool available, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BatchList.SELECT_BY_SERIE_AND_STOCK(oidSerie, false, available, false);

			BatchList list = DataPortal.Fetch<BatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BatchList GetListBySerieAndStockAgrupado(long oidSerie, bool available, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BatchList.SELECT_BY_SERIE_AND_STOCK(oidSerie, true, available, false);

			BatchList list = DataPortal.Fetch<BatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
        public static BatchList GetListBySerieAndStockAgrupadoStockNegativo(long oidSerie, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

            criteria.Query = BatchList.SELECT_BY_SERIE_AND_STOCK(oidSerie, true, false, true);

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static BatchList GetListBySerieAndStock(long oidSerie, bool available, bool childs, bool cache)
        {
            BatchList list;

            if (!Cache.Instance.Contains(typeof(BatchList)))
            {
                list = BatchList.GetListBySerieAndStock(oidSerie, available, childs);
                Cache.Instance.Save(typeof(BatchList), list);
            }
            else
                list = Cache.Instance.Get(typeof(BatchList)) as BatchList;

            return list;
        }

        /// <summary>
        /// Partidas que no sean Componentes
        /// </summary>
        /// <returns></returns>
		public static BatchList GetListBySerie(long oidSerie, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

			criteria.Query = BatchList.SELECT_BY_SERIE(oidSerie);

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static BatchList GetListBySerie(long oidSerie, bool childs, bool cache)
        {
            BatchList list;

            if (!Cache.Instance.Contains(typeof(BatchList)))
            {
				list = BatchList.GetListBySerie(oidSerie, childs);
                Cache.Instance.Save(typeof(BatchList), list);
            }
            else
                list = Cache.Instance.Get(typeof(BatchList)) as BatchList;

            return list;
        }

		public static BatchList GetListByAlmacen(long oid, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BatchList.SELECT_BY_ALMACEN(oid);

			BatchList list = DataPortal.Fetch<BatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BatchList GetListByAlmacen(long oid, bool childs, bool cache)
		{
			BatchList list;

			if (!Cache.Instance.Contains(typeof(BatchList)))
			{
				list = BatchList.GetListByFamilia(oid, childs);
				Cache.Instance.Save(typeof(BatchList), list);
			}
			else
				list = Cache.Instance.Get(typeof(BatchList)) as BatchList;

			return list;
		}

		public static BatchList GetListByFamilia(long oid, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BatchList.SELECT_BY_FAMILIA(oid);

			BatchList list = DataPortal.Fetch<BatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BatchList GetListByFamilia(long oid, bool childs, bool cache)
		{
			BatchList list;

			if (!Cache.Instance.Contains(typeof(BatchList)))
			{
				list = BatchList.GetListByFamilia(oid, childs);
				Cache.Instance.Save(typeof(BatchList), list);
			}
			else
				list = Cache.Instance.Get(typeof(BatchList)) as BatchList;

			return list;
		}
		
        /// <summary>
        /// Partidas que no sean Kits ni Componentes
        /// </summary>
        /// <returns></returns>
        public static BatchList GetListByFamiliaNoKits(long oidFamily, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
            criteria.Childs = childs;

            criteria.Query = BatchList.SELECT_BY_FAMILIA_NO_KITS(oidFamily);

            BatchList list = DataPortal.Fetch<BatchList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static BatchList GetList(CriteriaEx criteria)
        {
            return BatchList.RetrieveList(typeof(Batch), AppContext.ActiveSchema.Code, criteria);
        }
        public static BatchList GetList(IList<Batch> list) { return new BatchList(list,false); }
        public static BatchList GetList(IList<BatchInfo> list) { return new BatchList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<BatchInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<BatchInfo> sortedList = new SortedBindingList<BatchInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<BatchInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<BatchInfo> sortedList = new SortedBindingList<BatchInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion

        #region Child Factory Methods

		private BatchList(int session, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = session;
			Fetch(reader);
		}
		
		/// <summary>
        /// Builds a PartidaList from a IList<!--<PartidaInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PartidaList</returns>
        public static BatchList GetChildList(IList<BatchInfo> list)
        {
            BatchList flist = new BatchList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (BatchInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static BatchList GetChildList(IList<Batch> list) { return new BatchList(list, false); }
        public static BatchList GetChildList(int session, IDataReader reader, bool childs) { return new BatchList(session, reader, childs); }
		public static BatchList GetChildList(StoreInfo parent, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			BatchList list = DataPortal.Fetch<BatchList>(criteria);
			list.CloseSession();

			return list;
		}
		public static BatchList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			BatchList list = DataPortal.Fetch<BatchList>(criteria);
			list.CloseSession();

			return list;			
		}

		public static BatchList GetChildListByProducto(StoreInfo parent, long oidProducto, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(Batch.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent,
				Producto = Product.New().GetInfo(false)
			};
			conditions.Producto.Oid = oidProducto;
			criteria.Query = Batchs.SELECT(conditions);

			BatchList list = DataPortal.Fetch<BatchList>(criteria);
			list.CloseSession();

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
                        this.AddItem(BatchInfo.GetChild(SessionCode, reader, Childs));

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
        private void Fetch(IList<Batch> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Batch item in lista)
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
                this.AddItem(BatchInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return Batch.SELECT(0, false); }
        public static string SELECT(Library.Store.QueryConditions conditions) { return Batch.SELECT(conditions, false); }
        public static string SELECT(ProductInfo item) { return SELECT(new Library.Store.QueryConditions { Producto = item }); }
		public static string SELECT(StoreInfo item) { return SELECT(new Library.Store.QueryConditions { Almacen = item }); }
		public static string SELECT(ExpedientInfo item) { return SELECT(new Library.Store.QueryConditions { Expedient = item }); }

		public static string SELECT_BY_ALMACEN(long oid) { return Batch.SELECT_BY_STORE(oid, false); }
		public static string SELECT_BY_FAMILIA(long oid) { return Batch.SELECT_BY_FAMILY(oid, false); }
        public static string SELECT_BY_SERIE(long oid) { return Batch.SELECT_BY_SERIE(oid, false); }
		public static string SELECT_BY_FAMILIA_AND_STOCK(long oid) { return Batch.SELECT_BY_FAMILY_AND_STOCK(oid, false); }
		public static string SELECT_BY_SERIE_AND_STOCK(long oid, bool clustered, bool available, bool noStock) { return Batch.SELECT_BY_SERIE_AND_STOCK(oid, clustered, available, noStock, false); }
		public static string SELECT_BY_FAMILIA_NO_KITS(long oidFamily)
        {
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

            string query = string.Empty;

            query = 
            Batch.SELECT_BASE(new QueryConditions()) + @"
            WHERE BA.""OID"" NOT IN (SELECT DISTINCT ""OID_KIT"" FROM " + ba + @")
                AND BA.""OID_KIT"" = 0
                AND PR.""OID_FAMILIA"" = " + oidFamily + @"
            ORDER BY BA.""TIPO_MERCANCIA"", ""STOCK_K""";

            return query;
        }
        public static string SELECT_ELABORADOS()
        {
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

            string query = 
            Batch.SELECT_BASE(new QueryConditions()) + @"       
            INNER JOIN " + ba + " AS BA2 ON BA2.\"OID_KIT\" = BA.\"OID\"";                    

            return query;
        }
        public static string SELECT_BY_KIT(long oid_kit)
        {
            string query = 
            Batch.SELECT_BASE(new QueryConditions()) + @"      
            WHERE BA.""OID_KIT"" != 0 AND BA.""OID_KIT"" = " + oid_kit;

            return query;
        }

        #endregion	
	}
}

