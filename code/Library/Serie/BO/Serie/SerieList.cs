using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Serie
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class SerieList : ReadOnlyListBaseEx<SerieList, SerieInfo>
	{		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private SerieList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private SerieList(IList<Serie> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private SerieList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private SerieList(IList<SerieInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		 
		#region Factory Methods

		public static SerieList NewList() { return new SerieList(); }

		public static SerieList GetList(bool childs)
		{
			CriteriaEx criteria = Serie.GetCriteria(Serie.OpenSession());
            criteria.Childs = childs;
						
            criteria.Query = SerieList.SELECT();

			SerieList list = DataPortal.Fetch<SerieList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}
		public static SerieList GetList() { return SerieList.GetList(true); }
        public static SerieList GetList(bool childs, ETipoSerie tipo)
        {
			QueryConditions conditions = new QueryConditions { SerieType = tipo };

            return GetList(SELECT(conditions), childs);
        }

		public static SerieList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SerieList.SELECT(conditions), childs);
		}
		private static SerieList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Serie.GetCriteria(Serie.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			SerieList list = DataPortal.Fetch<SerieList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static SerieList GetList(CriteriaEx criteria)
        {
			return SerieList.RetrieveList(typeof(Serie), AppContext.ActiveSchema.SchemaCode, criteria);
        }
		public static SerieList GetList(IList<SerieInfo> list) { return new SerieList(list, false); }

		/// <summary>
        /// Builds a SerieList from a IList<!--<SerieInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>SerieList</returns>
        public static SerieList GetChildList(IList<SerieInfo> list)
        {
            SerieList flist = new SerieList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (SerieInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static SerieList GetChildList(IList<Serie> list) { return new SerieList(list, false); }
        public static SerieList GetChildList(IDataReader reader) { return new SerieList(reader, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<SerieInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<SerieInfo> sortedList =
				new SortedBindingList<SerieInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<Serie> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Serie item in lista)
                this.Add(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.Add(Serie.GetChild(reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
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
					{
						this.AddItem(SerieInfo.GetChild(SessionCode, reader, Childs));
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

        public static string SELECT() 
        {
            string query = SELECT(new QueryConditions());

            return query.Substring(0, query.Length-2) + " ORDER BY S.\"IDENTIFICADOR\""; 
        }
        public static string SELECT(QueryConditions conditions) { return Serie.SELECT(conditions, false); }
        
        #endregion

    }
}