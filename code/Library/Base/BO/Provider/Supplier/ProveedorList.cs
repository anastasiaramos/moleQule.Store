using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ProveedorList : ReadOnlyListBaseEx<ProveedorList, ProveedorInfo>
    {
        #region Business Methods

        #endregion

        #region Factory Methods

        internal ProveedorList() { }

		public static ProveedorList NewList() { return new ProveedorList(); }

		public static ProveedorList GetList() {	return ProveedorList.GetList(true); }
		public static ProveedorList GetList(bool childs) { return GetList(EEstado.Todos, childs); }
		public static ProveedorList GetList(EEstado status, bool childs)
		{
			return GetList(status, new ETipoAcreedor[3] { ETipoAcreedor.Acreedor, ETipoAcreedor.Partner, ETipoAcreedor.Proveedor }, childs);
		}
		public static ProveedorList GetList(EEstado status, ETipoAcreedor providerType, bool childs)
		{
			return GetList(status, new ETipoAcreedor[1] { providerType }, childs);
		}
		public static ProveedorList GetList(EEstado status, ETipoAcreedor[] providerType, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Estado = status,
				TipoAcreedor = providerType
			};

			return GetList(ProveedorList.SELECT(conditions), childs);
		}

		public static ProveedorList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Proveedor.GetCriteria(Proveedor.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			ProveedorList list = DataPortal.Fetch<ProveedorList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

        public static ProveedorList GetList(CriteriaEx criteria)
        {
			return ProveedorList.RetrieveList(typeof(Proveedor), AppContext.ActiveSchema.SchemaCode, criteria);
        }
        public static ProveedorList GetList(IList<ProveedorInfo> list)
        {
            ProveedorList flist = new ProveedorList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ProveedorInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static ProveedorList GetList(IList<Proveedor> list)
		{
			ProveedorList flist = new ProveedorList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (Proveedor item in list)
					flist.AddItem(item.GetInfo());

				flist.IsReadOnly = true;
			}

			return flist;
		}

        public static SortedBindingList<ProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<ProveedorInfo> sortedList = new SortedBindingList<ProveedorInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<ProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ProveedorInfo> sortedList = new SortedBindingList<ProveedorInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

		public static IDataReader GetPrices() { return GetPrices(new QueryConditions()); }
        public static IDataReader GetPrices(QueryConditions conditions)
        {
            CriteriaEx criteria = Proveedor.GetCriteria(Proveedor.OpenSession());
            criteria.Childs = false;

            criteria.Query = ProveedorList.SELECT_PRICES(conditions);
            IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, criteria.Session);

            CloseSession(criteria.SessionCode);
            return reader;
        }

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
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                    {
                        this.AddItem(ProveedorInfo.Get(SessionCode, reader, Childs));
                    }
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return ProviderBaseInfo.SELECT(conditions, conditions.TipoAcreedor);	}

        /// <summary>
        /// Construye el SELECT para traer todos los precios de un PROVEEDOR
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT_PRICES() { return ProveedorList.SELECT_PRICES(new QueryConditions()); }
        public static string SELECT_PRICES(QueryConditions conditions)
        {
			string spr = nHManager.Instance.GetSQLTable(typeof(ProductoProveedorRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            
            string query = @"
            SELECT SP.*
                    ,SP.""PRECIO"" AS ""PRECIO_ASOCIADO""
                    ,SU.""CODIGO"" || ' - ' || SU.""NOMBRE"" AS ""TITULAR""
                    ,PR.""NOMBRE"" AS ""PRODUCTO""
                    ,PR.""PRECIO_COMPRA"" AS ""PRECIO_PRODUCTO""
                    ,PR.""PRECIO_COMPRA"" AS ""PRECIO_MEDIO""
            FROM " + spr + @" AS SP
            INNER JOIN " + su + @" AS SU ON SU.""OID"" = SP.""OID_ACREEDOR"" AND SU.""TIPO"" = SP.""TIPO_ACREEDOR"" 
            INNER JOIN " + pr + @" AS PR ON PR.""OID"" = SP.""OID_PRODUCTO""
            WHERE TRUE";

            if (conditions.Familia != null)
                query += @"
                AND PR.""OID_FAMILIA"" = " + conditions.Familia.Oid;

            if (conditions.Producto != null)
                query += @"
                AND PR.""OID"" = " + conditions.Producto.Oid;

            query += @"
            ORDER BY SU.""NOMBRE"" " + ((conditions.Order == ListSortDirection.Ascending) ? "ASC" : "DESC");

            return query;
        }

        #endregion
    }
}