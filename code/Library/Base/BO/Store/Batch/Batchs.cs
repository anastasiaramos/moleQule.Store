using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Batchs : BusinessListBaseEx<Batchs, Batch>
    {
        #region Business Methods

		public void ResetMaxSerial(long oidProduct, int year)
		{
			MaxSerial = BatchSerialInfo.GetLast(oidProduct, year);
		}
		
		public void SetNextCode(Batch item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode(item.OidProducto);
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT) + "/" + item.FechaCompra.Year.ToString().Substring(2, 2);
				MaxSerial++;
			}
		}

		public Batch GetItemByAlmacen(long oid)
		{
			foreach (Batch obj in this)
				if (obj.OidAlmacen == oid)
					return obj;
			return null;
		}

        public Batch GetItemByExpediente(long oid_expediente)
        {
            foreach (Batch obj in this)
                if (obj.OidExpediente == oid_expediente)
                    return obj;
            return null;
        }

        public Batch GetItemByProducto(long oid)
        {
            foreach (Batch obj in this)
                if (obj.OidProducto == oid)
                    return obj;
            return null;
        }

        public bool ContainsProducto(long oid)
        {
            foreach (Batch obj in this)
                if (obj.OidProducto == oid)
                    return true;

            return false;
        }

        public bool ContainsProveedor(long oid)
        {
            foreach (Batch obj in this)
                if (obj.OidProveedor == oid)
                    return true;

            return false;
        }

		public decimal GetTotalKilos(string codigoAduanero)
		{
			decimal kilos = 0;

			foreach (Batch item in this)
			{
				if (item.CodigoAduanero == codigoAduanero)
					kilos += item.KilosIniciales;
			}

			return kilos;
		}

		public Batch NewItem(Almacen parent, Expedient expedient, InputDelivery delivery, InputDeliveryLine source)
		{
			Batch item = Batch.NewChild(parent, expedient, delivery, source);
			this.AddItem(item);
			if (MaxSerial == 0) ResetMaxSerial(item.OidProducto, delivery.Fecha.Year);
			SetNextCode(item);
			item.FechaCompra = item.FechaCompra.AddSeconds(item.Serial);
			
			//Entrada de stock asociada a la partida
			Stock stock = parent.Stocks.NewItem(this[Count - 1], ETipoStock.Compra);
			stock.Inicial = true;
			stock.Kilos = source.CantidadKilos;
			stock.Bultos = source.CantidadBultos;
			stock.OidKit = source.OidKit;
			stock.OidAlbaran = delivery.Oid;
			stock.OidConceptoAlbaran = source.Oid;
			stock.OidLineaPedido = source.OidLineaPedido;
			stock.OidExpediente = source.OidExpediente;
			stock.Fecha = delivery.Fecha;
			stock.Observaciones = String.Format(Resources.Messages.ENTRADA_POR_ALBARAN, delivery.Codigo);
			
			if (expedient != null)
			{
				switch (expedient.ETipoExpediente)
				{
					//Cabeza de ganado para expedientes de Ganado
					case ETipoExpediente.Ganado:
						item.KilosIniciales = 1;
						item.BultosIniciales = 1;
						//Cabeza cabeza = expediente.Cabezas.NewItem(expediente);
						//cabeza.CopyFrom(item);
						//cabeza.Observaciones = source.Concepto;
						break;

					//Maquina para expedientes de Maquinaria
					case ETipoExpediente.Maquinaria:
						item.KilosIniciales = 1;
						item.BultosIniciales = 1;
						Maquinaria maquina = expedient.Maquinarias.NewItem(expedient);
						maquina.CopyFrom(this[Count - 1]);
						maquina.Observaciones = source.Concepto;
						item.Machine = maquina;
						break;
				}

				expedient.TipoMercancia = item.Producto;
			}

			return item;
		}
		public Batch NewItem(Almacen parent, Expedient expediente) { return NewItem(parent, expediente, ETipoStock.Compra); }
		public Batch NewItem(Almacen parent, Expedient expediente, ETipoStock tipo)
        {
			Batch item = Batch.NewChild(expediente);
			this.AddItem(item);
			SetNextCode(item);

			Stock stock = parent.Stocks.NewItem(item, tipo);
            stock.Inicial = true;
			//expediente.UpdateTotalesProductos();
            return item;
        }
        //Para el producto_expediente asociado al producto Kit
        public Batch NewItem(Product parent)
        {
            this.AddItem(Batch.NewChild(parent));
            return this[Count - 1];
        }
        //Para productos_expedientes que pertenecen a un Kit
        public new Batch NewItem(Batch parent)
        {
            this.AddItem(Batch.NewChild(parent));
            return this[Count - 1];
        }
		public Batch NewItem(Batch partida, Stock stock, Expedient expediente, ETipoStock tipo)
		{
			Batch item = Batch.NewChild(partida, stock, expediente, tipo);
			this.AddItem(item);
			SetNextCode(item);
			return item;
		}

        public override void Remove(long oid)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
        public new void Remove(Batch item)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
        public void Remove(Almacen almacen, long oid)
        {
            base.Remove(oid);
        }
		public void Remove(Expedient expediente, long oid)
		{
			base.Remove(oid);
			if (expediente != null) expediente.UpdateTotalesProductos(this, true);

			//El stock inicial asociado se borra en el Delete
		}
		public void Remove(Expedient expediente, Batch item)        {
            base.Remove(item);
			if (expediente != null) expediente.UpdateTotalesProductos(this, true);

			//El stock inicial asociado se borra en el Delete
        }

        #endregion

        #region Common Factory Methods

        private Batchs() {}

		public List<BatchInfo> GetListByAlbaran(long oid_albaran)
		{
			List<BatchInfo> list = new List<BatchInfo>();

			foreach (Batch item in this)
			{
				if (item.OidAlbaran == oid_albaran)
					list.Add(item.GetInfo(false));
			}

			return list;
		}

        public Batchs GetByProductList(long oidProduct)
        {
            List<Batch> sublist = new List<Batch>(
                                             from ba in Items
                                             where ba.OidProducto == oidProduct
                                             select ba
                                        );

            return new Batchs(sublist);
        }

        #endregion

        #region Child Factory Methods

        private Batchs(IList<Batch> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private Batchs(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

        public static Batchs NewChildList() 
        { 
            Batchs list = new Batchs();
            list.MarkAsChild();
            return list;
        }

        public static Batchs GetChildList(IList<Batch> lista) { return new Batchs(lista); }

		public static Batchs GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static Batchs GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Batchs(sessionCode, reader, childs); }

		public static Batchs GetChildList(Almacen parent, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
			criteria.Query = Batchs.SELECT(parent, true);
			criteria.Childs = childs;

			return DataPortal.Fetch<Batchs>(criteria);
		}
        public static Batchs GetChildList(Expedient parent, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
            criteria.Query = Batchs.SELECT(parent, true);
            criteria.Childs = childs;

            return DataPortal.Fetch<Batchs>(criteria);
        }
        public static Batchs GetChildList(IStockable parent, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
            criteria.Query = Batch.SELECT(parent.OidBatch);
            criteria.Childs = childs;

            return DataPortal.Fetch<Batchs>(criteria);
        }
		public static Batchs GetChildListByProducto(Almacen parent, long oidProduct, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent.GetInfo(false),
				Producto = ProductInfo.New(oidProduct)
			};
			criteria.Query = Batchs.SELECT(conditions);

			return DataPortal.Fetch<Batchs>(criteria);
		}
		public static Batchs GetChildListByExpediente(Almacen parent, long oidExpedient, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent.GetInfo(false),
				Expedient = ExpedientInfo.New(oidExpedient)
			};
			criteria.Query = Batchs.SELECT(conditions);

			return DataPortal.Fetch<Batchs>(criteria);
		}
		public static Batchs GetChildListByAlbaranRecibido(Almacen parent, long oidDelivery, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Almacen = parent.GetInfo(false),
				InputDelivery = InputDeliveryInfo.New(oidDelivery)
			};
			criteria.Query = Batchs.SELECT(conditions);

			return DataPortal.Fetch<Batchs>(criteria);
		}

		public static Batchs GetChildListFromList(Almacen parent, string list, bool childs)
		{
			CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
			criteria.Query = Batchs.SELECT_FROM_LIST(parent, list);
			criteria.Childs = childs;

			return DataPortal.Fetch<Batchs>(criteria);
		}
		public static Batchs GetChildListFromList(Expedient parent, string list, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(parent.SessionCode);
            criteria.Query = Batchs.SELECT_FROM_LIST(parent, list);
            criteria.Childs = childs;

            return DataPortal.Fetch<Batchs>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

				MaxSerial = 0;

                if (nHMng.UseDirectSQL)
                {
                    Batch.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						Batch item = Batch.GetChild(SessionCode, reader, Childs);
						this.AddItem(item);
						if (item.Serial > MaxSerial) MaxSerial = item.Serial;
					}
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Batch> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Batch item in lista)
                this.AddItem(Batch.GetChild(item));

            this.RaiseListChangedEvents = true;
        }
        private void Fetch(IDataReader reader)
        {
			this.RaiseListChangedEvents = false;

			MaxSerial = 0;

			while (reader.Read())
			{
				Batch item = Batch.GetChild(SessionCode, reader, Childs);
				this.AddItem(item);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

			this.RaiseListChangedEvents = true;
        }

		internal void Update(Almacen parent)
		{
			this.RaiseListChangedEvents = false;

			//parent.UpdateTotalesProductos();

			// update (thus deleting) any deleted child objects
			foreach (Batch obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (Batch obj in this)
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

            parent.UpdateTotalesProductos(this, true);

            // update (thus deleting) any deleted child objects
            foreach (Batch obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Batch obj in this)
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
            foreach (Batch obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Batch obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }
        internal void Update(Batch parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Batch obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Batch obj in this)
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

        public static string SELECT() { return Batch.SELECT(0); }
        public static string SELECT(Library.Store.QueryConditions conditions) { return Batch.SELECT(conditions, true); }
        public static string SELECT(Product item) { return SELECT(new Library.Store.QueryConditions { Producto = item.GetInfo() }); }
		public static string SELECT(Almacen item, bool get_kit_components) { return Batch.SELECT(item, get_kit_components, true); }
		public static string SELECT(Expedient item, bool get_kit_components) { return Batch.SELECT(item, get_kit_components, true); }
		public static string SELECT_FROM_LIST(Almacen parent, string list)
		{
			string query = 
			Batch.SELECT_BASE(new QueryConditions()) + @"
			WHERE BA.""OID"" IN (" + list + @")
			    AND BA.""OID_ALMACEN"" = " + parent.Oid;

			//query += " FOR UPDATE OF PE NOWAIT;";

			return query;
		}
		public static string SELECT_FROM_LIST(Expedient parent, string list)
        {
            string query = 
            Batch.SELECT_BASE(new QueryConditions()) + @"
            WHERE BA.""OID"" IN (" + list + @")
                AND BA.""OID_EXPEDIENTE"" = " + parent.Oid;
                            
            //query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }
        public static string SELECT_BY_KIT(long oid_kit)
        {
            string query = 
            Batch.SELECT_BASE(new QueryConditions()) + @"
            WHERE BA.""OID_KIT"" = " + oid_kit;

            //query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        #endregion
    }
}
