using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class CabezaList : ReadOnlyListBaseEx<CabezaList, CabezaInfo>
	{		 
		 
		#region Factory Methods

		private CabezaList() { }

        private CabezaList(IDataReader reader)
        {
            Fetch(reader);
        }

        private CabezaList(IList<Cabeza> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }

        private CabezaList(IList<CabezaInfo> list, bool retrieve_childs)
        {
            Childs = retrieve_childs;
            Fetch(list);
        }

		#endregion

        #region Root Factory Methods

        public static CabezaList GetList()
        {
            return CabezaList.GetList(true);
        }

        public static CabezaList GetList(bool childs)
        {
            CriteriaEx criteria = Cabeza.GetCriteria(Cabeza.OpenSession());
            criteria.Childs = childs;

            criteria.Query = CabezaList.SELECT();

            CabezaList list = DataPortal.Fetch<CabezaList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static CabezaList GetList(CriteriaEx criteria)
        {
            return CabezaList.RetrieveList(typeof(Cabeza), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static CabezaList GetList(IList<Cabeza> list) { return new CabezaList(list, false); }

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static CabezaList GetList(IList<CabezaInfo> list) { return new CabezaList(list, false); }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<CabezaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<CabezaInfo> sortedList = new SortedBindingList<CabezaInfo>(GetList());

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
        public static SortedBindingList<CabezaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<CabezaInfo> sortedList = new SortedBindingList<CabezaInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Child Factory Methods

        public static CabezaList GetChildList(IList<CabezaInfo> list)
        {
            CabezaList flist = new CabezaList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (CabezaInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static CabezaList GetChildList(IList<Cabeza> list) { return new CabezaList(list, false); }
        public static CabezaList GetChildList(IDataReader reader) { return new CabezaList(reader); }
		public static CabezaList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = Cabeza.GetCriteria(Cabeza.OpenSession());
			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			CabezaList list = DataPortal.Fetch<CabezaList>(criteria);
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
                        this.AddItem(CabezaInfo.GetChild(reader, Childs));

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

		#region Child Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<Cabeza> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Cabeza item in lista)
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
                this.AddItem(Cabeza.GetChild(reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

		#endregion

        #region SQL
        
        public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Cabeza.SELECT(conditions, false); }
		public static string SELECT(ExpedientInfo source) { return Cabeza.SELECT(new QueryConditions { Expedient = source }, false); }

        #endregion

    }
}

