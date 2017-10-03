using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class WorkReportCategoryList : ReadOnlyListBaseEx<WorkReportCategoryList, WorkReportCategoryInfo, WorkReportCategory>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private WorkReportCategoryList() {}
		private WorkReportCategoryList(IList<WorkReportCategory> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private WorkReportCategoryList(IList<WorkReportCategoryInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static WorkReportCategoryList NewList() { return new WorkReportCategoryList(); }
		
		private static WorkReportCategoryList GetList(string query, bool childs)
		{
			CriteriaEx criteria = WorkReportCategory.GetCriteria(WorkReportCategory.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			WorkReportCategoryList list = DataPortal.Fetch<WorkReportCategoryList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static WorkReportCategoryList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }				
		public static WorkReportCategoryList GetList(bool childs = true)
		{
			
            return GetList(SELECT(), childs);
            
		}
		
        public static WorkReportCategoryList GetList(IList<WorkReportCategory> list) { return new WorkReportCategoryList(list,false); }
        public static WorkReportCategoryList GetList(IList<WorkReportCategoryInfo> list) { return new WorkReportCategoryList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<WorkReportCategoryInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<WorkReportCategoryInfo> sortedList = new SortedBindingList<WorkReportCategoryInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<WorkReportCategoryInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<WorkReportCategoryInfo> sortedList = new SortedBindingList<WorkReportCategoryInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<WorkReportCategory> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (WorkReportCategory item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(WorkReportCategoryInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion
		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
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
						this.AddItem(WorkReportCategoryInfo.GetChild(SessionCode, reader, Childs));

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(WorkReportCategory.SELECT_COUNT(criteria), criteria.Session);
                        if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
                    }
					
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
		
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return WorkReportCategory.SELECT(conditions, false); }
		
		#endregion		
	}
}
