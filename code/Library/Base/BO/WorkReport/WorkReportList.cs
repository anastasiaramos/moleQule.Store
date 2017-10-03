using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class WorkReportList : ReadOnlyListBaseEx<WorkReportList, WorkReportInfo, WorkReport>
	{	
		#region Business Methods

		public decimal GetTotal()
		{
			decimal total = Items.Sum(x => x.Total);
			return total;
		}
		
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private WorkReportList() {}
		private WorkReportList(IList<WorkReport> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private WorkReportList(IList<WorkReportInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static WorkReportList NewList() { return new WorkReportList(); }
		
		private static WorkReportList GetList(string query, bool childs)
		{
			CriteriaEx criteria = WorkReport.GetCriteria(WorkReport.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			WorkReportList list = DataPortal.Fetch<WorkReportList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static WorkReportList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }
				
		public static WorkReportList GetList(bool childs = true) { return GetList(SELECT(), childs); }
		public static WorkReportList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static WorkReportList GetList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(SELECT(conditions), childs);
		}

        public static WorkReportList GetList(IList<WorkReport> list) { return new WorkReportList(list,false); }
        public static WorkReportList GetList(IList<WorkReportInfo> list) { return new WorkReportList(list, false); }

		public static WorkReportList GetByExpedientList(long oidExpedient, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expedient = ExpedientInfo.New(oidExpedient) };
			return GetList(SELECT(conditions), childs);
		}
        public static WorkReportList GetByExpedientList(List<long> oidExpedientList, bool childs)
        {
            QueryConditions conditions = new QueryConditions 
            { 
                OidList = oidExpedientList
            };
            return GetList(WorkReport.SELECT_BY_EXPEDIENTS(conditions, false), childs);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<WorkReportInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<WorkReportInfo> sortedList = new SortedBindingList<WorkReportInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<WorkReportInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<WorkReportInfo> sortedList = new SortedBindingList<WorkReportInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<WorkReport> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (WorkReport item in lista)
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
                this.AddItem(WorkReportInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(WorkReportInfo.GetChild(SessionCode, reader, Childs));

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(WorkReport.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions) { return WorkReport.SELECT(conditions, false); }
		
		#endregion		
	}
}
