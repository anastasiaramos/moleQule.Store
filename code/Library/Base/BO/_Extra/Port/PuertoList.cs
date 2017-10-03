using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Tabla auxiliar con hijos
	/// </summary>
    [Serializable()]
    public class PuertoList : ReadOnlyListBaseEx<PuertoList, PuertoInfo>
    {
        #region Bussines Methods

        public bool ExistOtherItem(Puerto Puerto)
        {
            foreach (PuertoInfo obj in this)
                if ((obj.Oid != Puerto.Oid) && (obj.Valor.Equals(Puerto.Valor)))
                    return true;
            return false;
        }

        #endregion

        #region Factory Methods

        private PuertoList() { }

        private PuertoList(IDataReader reader)
        {
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>PuertoList</returns>
        public static PuertoList GetChildList(bool childs)
        {
			CriteriaEx criteria = Puerto.GetCriteria(Puerto.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT();
			
			PuertoList list = DataPortal.Fetch<PuertoList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
        }
		
		#endregion
		
		#region Root Factory Methods

		public static PuertoList NewList() { return new PuertoList(); }

		public static PuertoList GetList(bool childs)
		{
			CriteriaEx criteria = Puerto.GetCriteria(Puerto.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT();
			
			PuertoList list = DataPortal.Fetch<PuertoList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Default call for GetList(bool get_childs)
		/// </summary>
		/// <returns></returns>
		public static PuertoList GetList()
		{
			return PuertoList.GetList(true);
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static PuertoList GetList(CriteriaEx criteria)
		{
			return PuertoList.RetrieveList(typeof(Puerto), AppContext.CommonSchema, criteria);
		}
		
		/// <summary>
		/// Builds a PuertoList from a IList<!--<PuertoInfo>-->.
		/// Doesnt retrieve child data from DB.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static PuertoList GetList(IList<PuertoInfo> list)
		{
			PuertoList flist = new PuertoList();
			
			if (list.Count > 0)
			{
				flist.IsReadOnly = false;
				
				foreach (PuertoInfo item in list)
					flist.AddItem(item);
				
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
		public static SortedBindingList<PuertoInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<PuertoInfo> sortedList = new SortedBindingList<PuertoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		/// <summary>
        /// Devuelve una lista ordenada de todos los elementos y sus hijos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <param name="childs">Traer hijos</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<PuertoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<PuertoInfo> sortedList = new SortedBindingList<PuertoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
		
		/// <summary>
        /// Builds a PuertoList from a IList<!--<Puerto>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PuertoList</returns>
        public static PuertoList GetList(IList<Puerto> list)
        {
            PuertoList flist = new PuertoList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Puerto item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }

        
		
		#endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static PuertoList GetChildList()
        {
            return PuertoList.GetChildList(true);
        }

		/// <summary>
		/// Builds a PuertoList from a IList<!--<PuertoInfo>-->
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PuertoList</returns>
		public static PuertoList GetChildList(IList<PuertoInfo> list)
		{
			PuertoList flist = new PuertoList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (PuertoInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

		/// <summary>
		/// Builds a PuertoList from IList<!--<Puerto>--> and retrieve PuertoInfo Childs from DB
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PuertoList</returns>
		public static PuertoList GetChildList(IList<Puerto> list)
		{
			PuertoList flist = new PuertoList();

			if (list != null)
			{
				int sessionCode = Puerto.OpenSession();
				CriteriaEx criteria = null;

				flist.IsReadOnly = false;

				foreach (Puerto item in list)
				{
					
					criteria = PuertoDespachante.GetCriteria(sessionCode);
					criteria.AddEq("OidPuerto", item.Oid);
					criteria.AddOrder("Codigo", true);
					item.PuertoDespachantes = PuertoDespachantes.GetChildList(criteria.List<PuertoDespachante>());
					
					
					flist.AddItem(item.GetInfo());
				}

				flist.IsReadOnly = true;

				Puerto.CloseSession(sessionCode);
			}
			
			return flist;
		}

        public static PuertoList GetChildList(IDataReader reader) { return new PuertoList(reader); }

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
                    IDataReader reader = null;
                    /*if (criteria.Query == string.Empty)
                        reader = Puertos.DoSELECT(AppContext.CommonSchema, Session());
                    else*/
                        reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(PuertoInfo.Get(SessionCode, reader, Childs));

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

					foreach (Puerto item in list)
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
                    this.AddItem(PuertoInfo.Get(SessionCode, reader,Childs));

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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Puerto));
			conditions.Orders = orders;
			return Puerto.SELECT(conditions, false);
		}

		#endregion
    }
}



