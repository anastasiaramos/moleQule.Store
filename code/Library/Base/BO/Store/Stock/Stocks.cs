using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Resources;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Stocks : BusinessListBaseEx<Stocks, Stock>
    {
        #region Business Methods

        public Stock NewItem(IStockable parent)
        {
            this.NewItem(Stock.NewChild(parent));
            return this[Count - 1];
        }

        //public Stock NewItem(InputDeliveryLine parent)
        //{
        //    this.NewItem(Stock.NewChild(parent));
        //    return this[Count - 1];
        //}
        //public Stock NewItem(OutputDeliveryLine parent)
        //{
        //    this.NewItem(Stock.NewChild(parent));
        //    return this[Count - 1];
        //}
        //public Stock NewItem(LineaPedido parent)
        //{
        //    this.NewItem(Stock.NewChild(parent));
        //    return this[Count - 1];
        //}
		
        public Stock NewItem(Batch parent, ETipoStock tipo)
		{
			return NewItem(parent, null, tipo);
		}
        public Stock NewItem(Batch parent, Stock stock, ETipoStock tipo)
        {
            this.NewItem(Stock.NewChild(parent, stock, tipo));
            return this[Count - 1];
        }
        public Stock NewItem(Expedient parent)
        {
            this.NewItem(Stock.NewChild(parent));
            return this[Count - 1];
        }
        public Stock NewItem(Product parent)
        {
            this.NewItem(Stock.NewChild(parent));
            return this[Count - 1];
        }

        public Stock GetInitialStock(long oidPartida)
        {
            return this.FirstOrDefault(x => x.OidPartida == oidPartida && x.Inicial);
        }

        public Stock GetItemByBatch(long oidBatch)
        {
            return this.FirstOrDefault(x => x.OidPartida == oidBatch);
        }

        public Stock GetItemByInputDeliveryLine(long oid)
        {
            return this.FirstOrDefault(x => x.OidConceptoAlbaran == oid && x.ETipoStock == ETipoStock.Compra);
        }

        public Stock GetItemByOutputDeliveryLine(long oid)
        {
            return this.FirstOrDefault(x => x.OidConceptoAlbaran == oid && x.ETipoStock == ETipoStock.Venta);
        }

        public Stock GetItemByProduct(long oidProduct)
        {
            return this.FirstOrDefault(x => x.OidProducto == oidProduct);
        }

		public Stock GetItemByOutputOrderLine(long oid)
		{
            return this.FirstOrDefault(x => x.OidLineaPedido == oid && x.ETipoStock == ETipoStock.Reserva);
		}

		public decimal TotalGastosMermas(Batchs partidas)
		{
			decimal total = 0;
			Batch partida = null;

			foreach(Stock item in this)
			{
				if (item.ETipoStock != ETipoStock.Merma) continue;
				
				partida = partidas.GetItem(item.OidPartida);
				total += (partida.PrecioCompraKilo + partida.GastoKilo) * item.Kilos;
			}

			return Math.Abs(total);
		}

		public decimal TotalGastosSalidas(Batchs partidas)
		{
			decimal total = 0;
			Batch partida = null;

			foreach (Stock item in this)
			{
				if ((item.ETipoStock != ETipoStock.Consumo) &&
					(item.ETipoStock != ETipoStock.MovimientoSalida))
					continue;

				partida = partidas.GetItem(item.OidPartida);
				total += (partida.PrecioCompraKilo + partida.GastoKilo) * item.Kilos;
			}

			return Math.Abs(total);
		}

		public decimal TotalAyudaMermas(Batchs partidas)
		{
			decimal total = 0;
			Batch partida = null;

			foreach (Stock item in this)
			{
				if (item.ETipoStock != ETipoStock.Merma) continue;

				partida = partidas.GetItem(item.OidPartida);
				total += partida.AyudaKilo * item.Kilos;
			}

			return Math.Abs(total);
		}

		public decimal TotalAyudaSalidas(Batchs partidas)
		{
			decimal total = 0;
			Batch partida = null;

			foreach (Stock item in this)
			{
				if ((item.ETipoStock != ETipoStock.Consumo) &&
					(item.ETipoStock != ETipoStock.MovimientoSalida))
					continue;

				partida = partidas.GetItem(item.OidPartida);
				total += partida.AyudaKilo * item.Kilos;
			}

			return Math.Abs(total);
		}

        public virtual decimal TotalKgs()
        {
            return this.Sum(x => x.Kilos);
        }
        public virtual decimal TotalUds()
        {
            return this.Sum(x => x.Bultos);
        }

        public bool ContainsProduct(long oid)
        {
            foreach (Stock obj in this)
                if (obj.OidProducto == oid)
                    return true;

            return false;
        }

        public bool ContainsNotOnlyFirstProduct(long oid)
        {
            foreach (Stock obj in this)
                if ((obj.OidProducto == oid) && (!obj.Inicial))
                    return true;

            return false;
        }

        public bool ContainsNotOnlyFirstEntry(long oid_producto_expediente)
        {
            foreach (Stock obj in this)
                if ((obj.OidPartida == oid_producto_expediente) && (!obj.Inicial))
                    return true;

            return false;
        }

        public bool ContainsNotOnlyAlliedEntries(long oid_partida)
        {
            foreach (Stock obj in this)
                if (obj.OidPartida == oid_partida && obj.OidEnlace == 0)
                    return true;
            return false;
        }

		public bool HasAlbaran(Batch pe)
		{
			foreach (Stock obj in this)
				if ((obj.OidPartida == pe.Oid) && (obj.OidAlbaran != 0))
					return true;

			return false;
		}

        /// <summary>
        /// Actualiza el Stock del producto y el de cada linea de stock de ese producto
        /// </summary>
        public virtual void UpdateStocks(Batch partida, bool throwException)
        {
            if (partida == null) return;

            List<Stock> stocks = GetSubList(new FCriteria<long>("OidPartida", partida.Oid));

            partida.StockKilos = 0;
            partida.StockBultos = 0;

            foreach (Stock item in stocks)
            {
                partida.StockKilos += item.Kilos;
                partida.StockBultos += item.Bultos;

                item.KilosActuales = partida.StockKilos;
                item.BultosActuales = partida.StockBultos;

                //Ajuste a 0 por posibles errores de redondeo
                if (item.KilosActuales == 0) item.BultosActuales = 0;
                if (item.BultosActuales == 0) item.KilosActuales = 0;
            }

            //Ajuste a 0 por posibles errores de redondeo
            if (partida.StockKilos == 0) partida.StockBultos = 0;
            if (partida.StockBultos == 0) partida.StockKilos = 0;

            if (throwException && (partida.StockKilos < 0 || partida.StockBultos < 0))
                throw new iQException(Resources.Messages.AVISO_STOCK_NEGATIVO);
        }

		public virtual void UpdateStocks(Almacen parent, bool throwStockException)
		{
			if (parent.Partidas == null) return;

			foreach(Batch item in parent.Partidas)
				UpdateStocks(item, throwStockException);
		}

        public virtual void UpdateStocks(Expedient parent, bool throwStockException)
		{
			if (parent.Partidas == null) return;

			foreach (Batch item in parent.Partidas)
                UpdateStocks(item, throwStockException);
		}

        public virtual void RemoveStocksByOidEnlace(Expedient parent, long oidStock)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this[i].OidEnlace == oidStock)
                {
                    //if (!this[i].Inicial) Remove(this[i].Oid);
                    //else
                    {
                        if (parent.Partidas.Contains(this[i].OidPartida))
                            parent.Partidas.Remove(parent, this[i].OidPartida);
                    }
                }
            }
        }

        #endregion

        #region Common Factory Methods

        private Stocks() { }

        public Stocks GetByProductList(long oidProduct)
        {
            List<Stock> sublist = new List<Stock>(
                                             from st in Items
                                             where st.OidProducto == oidProduct
                                             select st
                                        );

            return new Stocks(sublist);
        }
        public Stocks GetByOutputDeliveryLineList(long oidOrder)
        {
            List<Stock> sublist = new List<Stock>(
                                             from st in Items
                                             where st.OidConceptoAlbaran == oidOrder
                                                && new[] {(long)ETipoStock.Venta, (long)ETipoStock.Consumo}.Contains(st.Tipo) 
                                             select st
                                        );

            return new Stocks(sublist);
        }
        public Stocks GetByOutputOrderLineList(long oidOrder)
        {
            List<Stock> sublist = new List<Stock>(
                                             from st in Items
                                             where st.OidLineaPedido == oidOrder
                                                && new[] {(long)ETipoStock.Reserva}.Contains(st.Tipo) 
                                             select st
                                        );

            return new Stocks(sublist);
        }

        #endregion

        #region Child Factory Methods

        private Stocks(IList<Stock> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Stocks(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static Stocks NewChildList() 
        { 
            Stocks lista = new Stocks();
            lista.MarkAsChild();
            return lista;
        }

        public static Stocks GetChildList(IList<Stock> lista) { return new Stocks(lista); }
		public static Stocks GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Stocks(sessionCode, reader, childs); }
		public static Stocks GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static Stocks GetChildList(Almacen parent, bool childs, bool throwStockException)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Query = Stocks.SELECT(parent);
			criteria.Childs = childs;

			Stocks list = DataPortal.Fetch<Stocks>(criteria);
            parent.UpdateStocks(throwStockException);

			return list;
		}
        public static Stocks GetChildList(Expedient parent, bool childs, bool throwStockException)
        {
            CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
            criteria.Query = Stocks.SELECT(parent);
            criteria.Childs = childs;

            Stocks list = DataPortal.Fetch<Stocks>(criteria);
            parent.UpdateStocks(throwStockException);

			return list;
        }
        public static Stocks GetChildList(IStockable parent, bool childs)
        {
            CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
            criteria.Childs = childs;

            criteria.Query = Stocks.SELECT(parent);

            return DataPortal.Fetch<Stocks>(criteria);
        }

		public static Stocks GetChildListByPartida(Almacen parent, long oidPartida, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent.GetInfo(false),
				Partida = Batch.NewChild().GetInfo(false)
			};
			conditions.Partida.Oid = oidPartida;
			criteria.Query = Stocks.SELECT(conditions);

			return DataPortal.Fetch<Stocks>(criteria);
		}
        public static Stocks GetChildListByPartida(Expedient parent, long oidPartida, bool childs)
        {
            CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions 
			{
				Expedient = parent.GetInfo(false),     
                Partida = Batch.NewChild().GetInfo(false) 
            };
            conditions.Partida.Oid = oidPartida;
            criteria.Query = Stocks.SELECT(conditions);

            return DataPortal.Fetch<Stocks>(criteria);
        }
		public static Stocks GetChildListByProducto(Almacen parent, long oidPartida, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent.GetInfo(false),
				Producto = Product.New().GetInfo(false)
			};
			conditions.Producto.Oid = oidPartida;
			criteria.Query = Stocks.SELECT(conditions);

			return DataPortal.Fetch<Stocks>(criteria);
		}

		public static Stocks GetChildListByPartidaFromList(Almacen parent, string list, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Query = Stocks.SELECT_FROM_LIST_BY_PARTIDA(parent, list);
			criteria.Childs = childs;

			return DataPortal.Fetch<Stocks>(criteria);
		}
        public static Stocks GetChildListByPartidaFromList(Expedient parent, string list, bool childs)
        {
            CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
            criteria.Query = Stocks.SELECT_FROM_LIST_BY_PARTIDA(parent, list);
            criteria.Childs = childs;

            return DataPortal.Fetch<Stocks>(criteria);
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Stocks NewList()
        {
            if (!Stock.CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new Stocks();
        }

        public static Stocks GetListByAlbaran(long oid)
        {
            if (!Stock.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
            criteria.Childs = false;

            QueryConditions conditions = new QueryConditions
            {
                IStockable = new StockableBase()
                                {
                                    EEntityType = moleQule.Common.Structs.ETipoEntidad.OutputDelivery,
                                    Oid = oid
                                }
            };
            criteria.Query = Stocks.SELECT(conditions);

            Stock.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Stocks>(criteria);
        }

        public static Stocks GetListByExpediente(long oidExpedient)
        {
            if (!Stock.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
            criteria.Childs = false;

            QueryConditions conditions = new QueryConditions { Expedient = ExpedientInfo.New(oidExpedient) };

            criteria.Query = Stocks.SELECT(conditions);

            Stock.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Stocks>(criteria);
        }

        public static Stocks GetListByExpedienteByPartida(long oidExpedient, long oidBatch)
        {
            if (!Stock.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Stock.GetCriteria(Stock.OpenSession());
            criteria.Childs = false;

            QueryConditions conditions = new QueryConditions
            {
                Expedient = ExpedientInfo.New(oidExpedient),
                Partida = BatchInfo.New(oidBatch) 
            };

            criteria.Query = Stocks.SELECT(conditions);

            Stock.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Stocks>(criteria);
        }

        #endregion

        #region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                Stock.DoLOCK(Session());
                IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                while (reader.Read())
                    this.AddItem(Stock.GetChild(SessionCode, reader));
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
        /// de los flags de cada objeto de la lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Stock obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Stock obj in this)
                {
                    if (obj.IsNew)
                        obj.Insert(this);
                    else
                        obj.Update(this);
                }

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Stock> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Stock item in lista)
                this.AddItem(Stock.GetChild(item));

            this.RaiseListChangedEvents = true;
        }
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Stock.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(IStockable parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Stock obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Stock obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
		internal void Update(Almacen parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (Stock obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (Stock obj in this)
			{
				if (obj.IsNew)
					obj.Insert(parent);
				else
					obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}
        internal void Update(Expedient parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Stock obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Stock obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
        internal void Update(Product parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Stock obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Stock obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Stock.SELECT(conditions, true); }
        public static string SELECT(Almacen item)
		{
			string query = SELECT(new QueryConditions { Almacen = item.GetInfo() });
			query += " ORDER BY ST.\"OID_BATCH\", ST.\"FECHA\", ST.\"OID\"";
			return query;
		}
		public static string SELECT(Expedient item)
        {
            string query = 
            SELECT(new QueryConditions { Expedient = item.GetInfo() }) + @"
            ORDER BY ST.""OID_BATCH"", ST.""FECHA"" ASC, ST.""OID""";

            return query;
        }
		public static string SELECT(IStockable item)
        {
            string query = 
            SELECT(new QueryConditions { IStockable = item }) + @"
            ORDER BY ST.""OID_BATCH"", ST.""OID""";

            return query;
        }
		public static string SELECT_FROM_LIST_BY_PARTIDA(Almacen item, string list)
		{
			QueryConditions conditions = new QueryConditions { Almacen = item.GetInfo(false) };

			string query = Stock.SELECT(conditions, false) +
							" AND ST.\"OID_ALMACEN\" IN (" + list + ")";

			return query;
		}
        public static string SELECT_FROM_LIST_BY_PARTIDA(Expedient item, string list)
        {
			QueryConditions conditions = new QueryConditions { Expedient = item.GetInfo(false) };

			string query = Stock.SELECT(conditions, false) +
							" AND ST.\"OID_BATCH\" IN (" + list + ")";
            
            return query;
        }
		public static string SELECT(InputDeliveryLine item)
		{
			string query = SELECT(new QueryConditions { ConceptoAlbaranProveedor = item.GetInfo() });
			query += " ORDER BY ST.\"OID_BATCH\", ST.\"OID\"";
			return query;
		}
        
        #endregion
    }
}