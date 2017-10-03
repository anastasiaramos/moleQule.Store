using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ProviderBaseList : ReadOnlyListBaseEx<ProviderBaseList, ProviderBaseInfo>
    {
        #region Business Methods

        public ProviderBaseInfo GetItem(long oid, ETipoAcreedor tipo)
        {
            foreach (ProviderBaseInfo item in this)
            {
                if ((item.ETipoAcreedor == tipo) && (item.OidAcreedor == oid))
                    return item;
            }

            return null;
        }

        #endregion

        #region Factory Methods

        private ProviderBaseList() { }

        private ProviderBaseList(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static ProviderBaseList GetList(bool childs = false) { return GetList(EEstado.Todos, childs); }
		public static ProviderBaseList GetList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = Proveedor.GetCriteria(Proveedor.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ProviderBaseList.SELECT(new QueryConditions { Estado = estado });

			ProviderBaseList list = DataPortal.Fetch<ProviderBaseList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static ProviderBaseList GetLockedOutList(CriteriaEx criteria, bool childs = false)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};

			string query = ProviderBaseInfo.SELECT_LOCKEDOUT(conditions);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}

		public static ProviderBaseList GetList(CriteriaEx criteria, bool childs = false)
		{
			CriteriaEx criteriaex = Proveedor.GetCriteria(Proveedor.OpenSession());

			criteriaex.Childs = childs;
			criteriaex.PagingInfo = criteria.PagingInfo;
			criteriaex.Filters = criteria.Filters;
			criteriaex.Orders = criteria.Orders;

			criteriaex.Query = ProviderBaseInfo.SELECT(criteriaex, false);

			ProviderBaseList list = DataPortal.Fetch<ProviderBaseList>(criteriaex);
			CloseSession(criteriaex.SessionCode);

			return list;
		}
		public static ProviderBaseList GetList(string query, CriteriaEx criteria, bool childs = false)
		{
			CriteriaEx criteriaex = Proveedor.GetCriteria(Proveedor.OpenSession());

			criteriaex.Childs = childs;
			criteriaex.PagingInfo = (criteria != null) ? criteria.PagingInfo : null;
			criteriaex.Filters = (criteria != null) ? criteria.Filters : null;
			criteriaex.Orders = (criteria != null) ? criteria.Orders : null;

			criteriaex.Query = query;

			ProviderBaseList list = DataPortal.Fetch<ProviderBaseList>(criteriaex);
			CloseSession(criteriaex.SessionCode);

			return list;
		}
        public static ProviderBaseList GetList(IList<ProviderBaseInfo> list)
        {
            ProviderBaseList flist = new ProviderBaseList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ProviderBaseInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
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
                IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(ProviderBaseInfo.Get(reader, false));

                IsReadOnly = true;

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
                {
                    this.AddItem(ProviderBaseInfo.Get(reader, false));
                }

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

        public static string SELECT() { return ProviderBaseInfo.SELECT_BUILDER(ProviderBaseInfo.provider_caller, new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return ProviderBaseInfo.SELECT(conditions); }

        #endregion
    }
}



