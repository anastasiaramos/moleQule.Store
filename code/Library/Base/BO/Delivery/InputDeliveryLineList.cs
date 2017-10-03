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

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class InputDeliveryLineList : ReadOnlyListBaseEx<InputDeliveryLineList, InputDeliveryLineInfo>
	{
		#region Business Methods

		public InputDeliveryLineInfo GetItemByPartida(long oid)
		{
			foreach (InputDeliveryLineInfo obj in this)
				if (obj.OidPartida == oid)
					return obj;
			return null;
		}

		#endregion

		#region Common Factory Methods

		private InputDeliveryLineList() { }

        #endregion

        #region Root Factory Methods

		public static InputDeliveryLineList NewList() { return new InputDeliveryLineList(); }

		public static InputDeliveryLineList GetList() { return InputDeliveryLineList.GetList(true); }
        public static InputDeliveryLineList GetList(bool childs)
        {
			return GetList(InputDeliveryLineList.SELECT(), childs);
        }

        public static InputDeliveryLineList GetByAlbaranList(long oid, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                InputDelivery = InputDeliveryInfo.New(oid)
            };

            return GetList(InputDeliveryLineSQL.SELECT(conditions, false), childs);
        }

		public static InputDeliveryLineList GetByExpedienteList(long oid, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Expedient = ExpedientInfo.New(oid),
				TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Proveedor }
			};

            return GetList(InputDeliveryLineSQL.SELECT(conditions, false), childs);
		}
		public static InputDeliveryLineList GetByExpedienteList(long oid, bool childs, bool cache)
		{
			InputDeliveryLineList list;

			if (!Cache.Instance.Contains(typeof(InputDeliveryLineList)))
			{
				list = GetByExpedienteList(oid, childs);
				Cache.Instance.Save(typeof(InputDeliveryLineList), list);
			}
			else
				list = Cache.Instance.Get(typeof(InputDeliveryLineList)) as InputDeliveryLineList;

			return list;	
		}

        public static InputDeliveryLineList GetByExpedienteList(QueryConditions conditions
                                                                        , ExpedientInfo ini
                                                                        , ExpedientInfo fin)
        {
            CriteriaEx criteria = InputDeliveryLine.GetCriteria(InputDeliveryLine.OpenSession());
            criteria.Childs = false;

            criteria.Query = InputDeliveryLineList.SELECT_BY_EXPEDIENTE(conditions, ini, fin);

            InputDeliveryLineList list = DataPortal.Fetch<InputDeliveryLineList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static InputDeliveryLineList GetByExpedienteStockList(long oid, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Expedient = ExpedientInfo.New(oid),
				TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Proveedor }
            };

            return GetList(InputDeliveryLineSQL.SELECT_STOCK(conditions, false), childs);
        }

        public static InputDeliveryLineList GetByExpedienteStockList(long oid, bool childs, InputDeliveryInfo albaran)
        {
            QueryConditions conditions = new QueryConditions
            {
                Expedient = ExpedientInfo.New(oid),
				TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Proveedor }
            };

            InputDeliveryLineList list = GetList(InputDeliveryLineSQL.SELECT_STOCK(conditions, false), childs);

            List<InputDeliveryLineInfo> merge = new List<InputDeliveryLineInfo>();

            foreach (InputDeliveryLineInfo ca in albaran.ConceptoAlbaranes)
            {
                if (ca.OidExpediente == oid)
                    merge.Add(ca);
            }

            foreach (InputDeliveryLineInfo ca in list)
            {
                if (ca.OidAlbaran != albaran.Oid)
                    merge.Add(ca);
            }

            return InputDeliveryLineList.GetChildList(merge);

        }
        public static InputDeliveryLineList GetByExpedienteStockList(long oid, bool childs, bool cache)
        {
            InputDeliveryLineList list;

            if (!Cache.Instance.Contains(typeof(InputDeliveryLineList)))
            {
                list = GetByExpedienteStockList(oid, childs);
                Cache.Instance.Save(typeof(InputDeliveryLineList), list);
            }
            else
                list = Cache.Instance.Get(typeof(InputDeliveryLineList)) as InputDeliveryLineList;

            return list;
        }

        public static InputDeliveryLineList GetByExpedienteStockList(QueryConditions conditions
                                                                        , ExpedientInfo ini
                                                                        , ExpedientInfo fin)
        {
            CriteriaEx criteria = InputDeliveryLine.GetCriteria(InputDeliveryLine.OpenSession());
            criteria.Childs = false;

            criteria.Query = InputDeliveryLineList.SELECT_BY_EXPEDIENTE_STOCK(conditions, ini, fin);

            InputDeliveryLineList list = DataPortal.Fetch<InputDeliveryLineList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static InputDeliveryLineList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(InputDeliveryLineList.SELECT(conditions), childs);
		}
		private static InputDeliveryLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = InputDeliveryLine.GetCriteria(InputDeliveryLine.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			InputDeliveryLineList list = DataPortal.Fetch<InputDeliveryLineList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static InputDeliveryLineList GetList(CriteriaEx criteria)
        {
            return InputDeliveryLineList.RetrieveList(typeof(InputDeliveryLine), AppContext.ActiveSchema.Code, criteria);
        }
        public static InputDeliveryLineList GetList(IList<InputDeliveryLineInfo> list)
        {
            InputDeliveryLineList flist = new InputDeliveryLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputDeliveryLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static InputDeliveryLineList GetList(IList<InputDeliveryLine> list)
		{
			InputDeliveryLineList flist = new InputDeliveryLineList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (InputDeliveryLine item in list)
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
        public static SortedBindingList<InputDeliveryLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<InputDeliveryLineInfo> sortedList = new SortedBindingList<InputDeliveryLineInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<InputDeliveryLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<InputDeliveryLineInfo> sortedList = new SortedBindingList<InputDeliveryLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

		#endregion

        #region Child Factory Methods

		private InputDeliveryLineList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

        /// <summary>
        /// Builds a ConceptoAlbaranList from a IList<!--<ConceptoAlbaranProveedorInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ConceptoAlbaranList</returns>
        public static InputDeliveryLineList GetChildList(IList<InputDeliveryLineInfo> list)
        {
            InputDeliveryLineList flist = new InputDeliveryLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputDeliveryLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static InputDeliveryLineList GetChildList(IList<InputDeliveryLine> list)
        {
            InputDeliveryLineList flist = new InputDeliveryLineList();

            if (list != null)
            {
                //int sessionCode = ConceptoAlbaranProveedor.OpenSession();

                flist.IsReadOnly = false;

                foreach (InputDeliveryLine item in list)
                {
                    /*IDataReader reader;
                    string query = string.Empty;
                    ConceptoAlbaranProveedorInfo info = item.GetInfo();

                    query = PartidaInfo.SELECT(item.OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, ConceptoAlbaranProveedor.Session(sessionCode));
                    info.Partidas = PartidaList.GetChildList(reader, false);

                    query = StockList.SELECT_BY_ConceptoAlbaran(item.Oid);
                    reader = nHManager.Instance.SQLNativeSelect(query, ConceptoAlbaranProveedor.Session(sessionCode));
                    info.Stocks = StockList.GetChildList(reader);*/

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                //ConceptoAlbaranProveedor.CloseSession(sessionCode);
            }

            return flist;
        }

		public static InputDeliveryLineList GetChildList(int sessionCode, IDataReader reader) { return new InputDeliveryLineList(sessionCode, reader); }

		public static InputDeliveryLineList GetChildList(InputDeliveryInfo parent, bool childs)
		{
			CriteriaEx criteria = InputDeliveryLine.GetCriteria(InputDeliveryLine.OpenSession());

			criteria.Query = InputDeliveryLineList.SELECT(parent);
			criteria.Childs = childs;

			InputDeliveryLineList list = DataPortal.Fetch<InputDeliveryLineList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
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
                        this.AddItem(InputDeliveryLineInfo.Get(SessionCode, reader, Childs));

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
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (InputDeliveryLine item in list)
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
                    this.AddItem(InputDeliveryLineInfo.Get(SessionCode, reader, Childs));

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

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return InputDeliveryLineSQL.SELECT(conditions, false); }
        public static string SELECT(InputDeliveryInfo delivery) 
        {
            string query;

			QueryConditions conditions = new QueryConditions { InputDelivery = delivery };
            query = InputDeliveryLineSQL.SELECT(conditions, false);

            return query;
        }

        public static string SELECT_BY_EXPEDIENTE(QueryConditions conditions) { return InputDeliveryLineSQL.SELECT(conditions, false); }
        public static string SELECT_BY_EXPEDIENTE(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
        {
            string query = string.Empty;

            query = SELECT_BY_EXPEDIENTE(conditions);

            if (ini != null)
                query += " AND GA.\"CODIGO\" >='" + ini.Codigo + "'";

            if (fin != null)
                query += " AND GA.\"CODIGO\" <='" + fin.Codigo + "'";
            
            query += " GROUP BY CA.\"OID_ALBARAN\", GA.\"CODIGO\"" +
                " ORDER BY GA.\"CODIGO\", \"OID\"";

            return query;
        }
        
        public static string SELECT_BY_EXPEDIENTE_STOCK(QueryConditions conditions) { return InputDeliveryLineSQL.SELECT_STOCK(conditions, false); }
        public static string SELECT_BY_EXPEDIENTE_STOCK(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
        {
            string query = string.Empty;

            query = SELECT_BY_EXPEDIENTE_STOCK(conditions);

            if (ini != null)
                query += " AND GA.\"CODIGO\" >='" + ini.Codigo + "'";

            if (fin != null)
                query += " AND GA.\"CODIGO\" <='" + fin.Codigo + "'";

            query += " GROUP BY CA.\"OID_ALBARAN\", GA.\"CODIGO\"" +
                " ORDER BY GA.\"CODIGO\", \"OID\"";

            return query;
        }

        #endregion
    }
}



