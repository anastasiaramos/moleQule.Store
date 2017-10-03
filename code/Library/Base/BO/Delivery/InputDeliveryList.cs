using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class InputDeliveryList : ReadOnlyListBaseEx<InputDeliveryList, InputDeliveryInfo>
    {
        #region Business Methods

        public decimal TotalPendiente()
        {
            decimal pendiente = 0;

            foreach (InputDeliveryInfo item in this)
                pendiente += item.Facturado ? item.Total : 0;

            return pendiente;
        }

        #endregion

        #region Factory Methods

        private InputDeliveryList() { }

        private InputDeliveryList(IDataReader reader)
        {
            Fetch(reader);
        }

        private InputDeliveryList(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>AlbaranProveedorList</returns>
        public static InputDeliveryList GetChildList(bool childs)
        {
            CriteriaEx criteria = InputDelivery.GetCriteria(InputDelivery.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            InputDeliveryList list = DataPortal.Fetch<InputDeliveryList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static InputDeliveryList NewList() { return new InputDeliveryList(); }

		public static InputDeliveryList GetList() { return InputDeliveryList.GetList(true); }
		public static InputDeliveryList GetList(bool childs)
		{
			return GetList(childs, ETipoAcreedor.Todos, DateTime.MinValue, DateTime.MaxValue);
		}
		public static InputDeliveryList GetList(bool childs, InputInvoice factura)
		{
			return GetList(childs, factura.ETipoAcreedor, factura.OidAcreedor, factura.OidSerie);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, int year)
		{
			return GetList(childs, tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, DateTime f_ini, DateTime f_fin)
		{
			return GetList(childs, tipo, 0, 0, f_ini, f_fin);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_serie, ETipoFactura tipo_albaran)
		{
			return GetList(childs, tipo, 0, oid_serie, tipo_albaran);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_serie, DateTime f_ini, DateTime f_fin)
		{
			return GetList(childs, tipo, 0, oid_serie, f_ini, f_fin);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoFactura.Todas);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, ETipoFactura tipo_albaran)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.Todos, tipo_albaran, DateTime.MinValue, DateTime.MaxValue);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, DateTime f_ini, DateTime f_fin)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.Todos, ETipoFactura.Todas, f_ini, f_fin);
		}
		public static InputDeliveryList GetList(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, ETipoAlbaranes tipo_albaranes, ETipoFactura tipo_albaran, int year)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, tipo_albaranes, tipo_albaran, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}

		public static InputDeliveryList GetList(bool childs,
											ETipoAcreedor providerType,
											long oidProvider,
											long oidSerie,
											ETipoAlbaranes deliveryType,
											ETipoFactura invoiceType,
											DateTime from,
											DateTime till)
		{
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Serie = (oidSerie != 0) ? Serie.SerieInfo.New(oidSerie) : null,
				TipoAlbaranes = deliveryType,
				TipoFactura = invoiceType,
				FechaIni = from,
				FechaFin = till,
			};
			if (oidSerie != 0) conditions.Serie.Oid = oidSerie;

			return GetList(childs, SELECT(conditions));
		}

		public static InputDeliveryList GetNoFacturados(bool childs)
		{
			return GetNoFacturados(childs, ETipoAcreedor.Todos, DateTime.MinValue, DateTime.MaxValue);
		}
		public static InputDeliveryList GetNoFacturados(bool childs, InputInvoice factura)
		{
			return GetNoFacturados(childs, factura.ETipoAcreedor, factura.OidAcreedor, factura.OidSerie, factura.Rectificativa ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria);
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, int year)
		{
			return GetNoFacturados(childs, tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, DateTime f_ini, DateTime f_fin)
		{
			return GetNoFacturados(childs, tipo, 0, 0, f_ini, f_fin);
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie)
		{
			return GetNoFacturados(childs, tipo, oid_acreedor, oid_serie, ETipoFactura.Todas);
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, ETipoFactura tipo_albaran)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.NoFacturados, tipo_albaran, DateTime.MinValue, DateTime.MaxValue);
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, int year)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.NoFacturados, ETipoFactura.Todas, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static InputDeliveryList GetNoFacturados(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, DateTime f_ini, DateTime f_fin)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.NoFacturados, ETipoFactura.Todas, f_ini, f_fin);
		}

		public static InputDeliveryList GetFacturados(bool childs)
		{
			return GetFacturados(childs, ETipoAcreedor.Todos, DateTime.MinValue, DateTime.MaxValue);
		}
		public static InputDeliveryList GetFacturados(bool childs, ETipoAcreedor tipo, int year)
		{
			return GetFacturados(childs, tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static InputDeliveryList GetFacturados(bool childs, ETipoAcreedor tipo, DateTime f_ini, DateTime f_fin)
		{
			return GetFacturados(childs, tipo,0, 0, f_ini, f_fin);
		}
		public static InputDeliveryList GetFacturados(bool childs, ETipoAcreedor tipo, long oid_acreedor, long oid_serie, DateTime f_ini, DateTime f_fin)
		{
			return GetList(childs, tipo, oid_acreedor, oid_serie, ETipoAlbaranes.Facturados, ETipoFactura.Todas, f_ini, f_fin);
		}

        public static InputDeliveryList GetListByAcreedor(bool childs, ETipoAcreedor tipo, long oid_acreedor)
        {
            return GetListByAcreedor(childs, tipo, oid_acreedor, ETipoAlbaranes.Todos, DateTime.MinValue, DateTime.MaxValue);
        }
        public static InputDeliveryList GetListByAcreedor(bool childs, ETipoAcreedor tipo, long oid_acreedor, ETipoAlbaranes tipo_albaranes, DateTime from, DateTime till)
        {
			return GetList(childs, tipo, oid_acreedor, 0, tipo_albaranes, ETipoFactura.Todas, from, till); 
        }

		public static InputDeliveryList GetListByProducto(bool childs, long oidProduct)
		{
			QueryConditions conditions = new QueryConditions
			{
				Producto = ProductInfo.New(oidProduct)
			};
			
			return GetList(childs, SELECT(conditions));		  
		}
		
		public static InputDeliveryList GetListByPartida(bool childs, long oidBatch)
		{
			QueryConditions conditions = new QueryConditions
			{
                Partida = BatchInfo.New(oidBatch)
			};
			
			return GetList(childs, SELECT(conditions));
		}

        public static InputDeliveryList GetListByPartidasExpediente(bool childs)
        {
            return GetListByPartidasExpediente(childs, -1);
        }

        public static InputDeliveryList GetListByPartidasExpediente(bool childs, long oidExp)
        {
            QueryConditions conditions = new QueryConditions
            {
                Expedient = ExpedientInfo.New(oidExp)
            };

            return GetList(childs, SELECT(conditions));
        }

        public static InputDeliveryList GetListByFactura(bool childs, long oidInvoice)
		{
			QueryConditions conditions = new QueryConditions
			{
				FacturaRecibida = InputInvoiceInfo.New(oidInvoice)
			};

			return GetList(childs, SELECT(conditions));
		}

		public static InputDeliveryList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(childs, InputDeliveryList.SELECT(conditions));
		}
		private static InputDeliveryList GetList(bool childs, string query)
        {
            CriteriaEx criteria = InputDelivery.GetCriteria(InputDelivery.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            InputDeliveryList list = DataPortal.Fetch<InputDeliveryList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static InputDeliveryList GetList(IList<InputDeliveryInfo> list)
        {
            InputDeliveryList flist = new InputDeliveryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputDeliveryInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static InputDeliveryList GetList(IList<InputDelivery> list)
		{
			InputDeliveryList flist = new InputDeliveryList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (InputDelivery item in list)
					flist.AddItem(item.GetInfo());

				flist.IsReadOnly = true;
			}

			return flist;
		}

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<InputDeliveryInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<InputDeliveryInfo> sortedList = new SortedBindingList<InputDeliveryInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<InputDeliveryInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<InputDeliveryInfo> sortedList = new SortedBindingList<InputDeliveryInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Builds a AlbaranProveedorList from a IList<!--<AlbaranProveedor>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranProveedorList</returns>
        public List<InputDeliveryInfo> GetListInfo()
        {
            List<InputDeliveryInfo> flist = new List<InputDeliveryInfo>();

            foreach (InputDeliveryInfo item in this)
                flist.Add(item);

            return flist;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static InputDeliveryList GetChildList()
        {
            return InputDeliveryList.GetChildList(true);
        }

        /// <summary>
        /// Builds a AlbaranProveedorList from a IList<!--<AlbaranProveedorInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranProveedorList</returns>
        public static InputDeliveryList GetChildList(IList<InputDeliveryInfo> list)
        {
            InputDeliveryList flist = new InputDeliveryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputDeliveryInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a AlbaranProveedorList from IList<!--<AlbaranProveedor>--> and retrieve AlbaranProveedorInfo Childs from DB
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranProveedorList</returns>
        public static InputDeliveryList GetChildList(IList<InputDelivery> list)
        {
            InputDeliveryList flist = new InputDeliveryList();

            if (list != null)
            {
                int sessionCode = InputDelivery.OpenSession();
                CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (InputDelivery item in list)
                {
                    criteria = InputDeliveryLine.GetCriteria(sessionCode);
                    criteria.AddEq("OidAlbaran", item.Oid);
                    item.Conceptos = InputDeliveryLines.GetChildList(criteria.List<InputDeliveryLine>());

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                InputDelivery.CloseSession(sessionCode);
            }

            return flist;
        }

        public static InputDeliveryList GetChildList(IDataReader reader) { return new InputDeliveryList(reader); }
        public static InputDeliveryList GetChildList(IDataReader reader, bool childs) { return new InputDeliveryList(reader, childs); }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(InputDeliveryInfo.GetChild(SessionCode, reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (InputDelivery item in list)
                        this.AddItem(item.GetInfo(false));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(InputDeliveryInfo.GetChild(SessionCode, reader, Childs));

                IsReadOnly = true;
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		protected static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return InputDelivery.SELECT(conditions); }
                
        #endregion
    }
}



