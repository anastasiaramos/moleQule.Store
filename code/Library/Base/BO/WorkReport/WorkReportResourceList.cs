using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class WorkReportResourceList : ReadOnlyListBaseEx<WorkReportResourceList, WorkReportResourceInfo, WorkReportResource>
	{	
		#region Business Methods

        public decimal GetExtras()
        {
            return Items.Sum(x => x.ExtraCost);
        }

		public decimal GetTotal()
		{
			return Items.Sum(x => x.Total);
		}

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private WorkReportResourceList() {}
		private WorkReportResourceList(IList<WorkReportResource> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private WorkReportResourceList(IList<WorkReportResourceInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static WorkReportResourceList NewList() { return new WorkReportResourceList(); }
		
		private static WorkReportResourceList GetList(string query, bool childs)
		{
			CriteriaEx criteria = WorkReportResource.GetCriteria(WorkReportResource.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			WorkReportResourceList list = DataPortal.Fetch<WorkReportResourceList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static WorkReportResourceList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }

		public static WorkReportResourceList GetList(bool childs = true)
		{
			return GetList(SELECT(), childs);
		}
		public static WorkReportResourceList GetList(long oidExpedient, ETipoEntidad entityType, bool clustered, bool childs = true)
		{
			QueryConditions conditions = new QueryConditions 
			{
				Expedient = ExpedientInfo.New(oidExpedient),
				EntityType = entityType
			};

			if (clustered)
			{
				conditions.Groups = new GroupList();
				conditions.Groups.NewGroup("OidResource", typeof(WorkReportResource));
				conditions.Groups.NewGroup("EntityType", typeof(WorkReportResource));
			}

			return GetList(SELECT(conditions), childs);
		}
        public static WorkReportResourceList GetList(List<long> oidExpedients, ETipoEntidad entityType, bool clustered, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidList = oidExpedients,
                EntityType = entityType
            };

            if (clustered)
            {
                conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidResource", typeof(WorkReportResource));
                conditions.Groups.NewGroup("EntityType", typeof(WorkReportResource));
            }

            return GetList(WorkReportResource.SELECT_BY_EXPEDIENTS(conditions, false), childs);
        }

		public static WorkReportResourceList GetByCategoryList(long oidCategory, long oidExpedient, bool clustered, bool childs = true)
		{
			QueryConditions conditions = new QueryConditions
			{
				Expedient = (oidExpedient !=  0) ? ExpedientInfo.New(oidExpedient) : null,
				WorkReportCategory = (oidCategory != 0) ? WorkReportCategoryInfo.New(oidCategory) : null,
				Orders = new OrderList()
			};

			conditions.Orders.NewOrder("CategoryMax", ListSortDirection.Ascending, typeof(WorkReportCategory));

			if (clustered)
			{
				conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidCategory", typeof(WorkReportResource));			
			}			

			return GetList(SELECT(conditions), childs);
		}
        public static WorkReportResourceList GetByCategoryList(long oidCategory, List<long> oidExpedients, bool clustered, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidList = oidExpedients,
                WorkReportCategory = (oidCategory != 0) ? WorkReportCategoryInfo.New(oidCategory) : null,
                Orders = new OrderList()
            };

            conditions.Orders.NewOrder("CategoryMax", ListSortDirection.Ascending, typeof(WorkReportCategory));

            if (clustered)
            {
                conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidCategory", typeof(WorkReportResource));
            }

            return GetList(WorkReportResource.SELECT_BY_EXPEDIENTS(conditions, false), childs);
        }
        
        public static WorkReportResourceList GetByCategoryAndResourceList(long oidCategory, long oidExpedient, bool clustered, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions
            {
                Expedient = (oidExpedient != 0) ? ExpedientInfo.New(oidExpedient) : null,
                WorkReportCategory = (oidCategory != 0) ? WorkReportCategoryInfo.New(oidCategory) : null,
                Orders = new OrderList()
            };

            conditions.Orders.NewOrder("CategoryMax", ListSortDirection.Ascending, typeof(WorkReportCategory));
            conditions.Orders.NewOrder("EntityTypeMax", ListSortDirection.Ascending, typeof(WorkReportResource));

            if (clustered)
            {
                conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidCategory", typeof(WorkReportResource));
                conditions.Groups.NewGroup("EntityType", typeof(WorkReportResource));
            }

            return GetList(SELECT(conditions), childs);
        }
        public static WorkReportResourceList GetByCategoryAndResourceList(long oidCategory, List<long> oidExpedients, bool clustered, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidList = oidExpedients,
                WorkReportCategory = (oidCategory != 0) ? WorkReportCategoryInfo.New(oidCategory) : null,
                Orders = new OrderList()
            };

            conditions.Orders.NewOrder("CategoryMax", ListSortDirection.Ascending, typeof(WorkReportCategory));
            conditions.Orders.NewOrder("EntityTypeMax", ListSortDirection.Ascending, typeof(WorkReportResource));

            if (clustered)
            {
                conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidCategory", typeof(WorkReportResource));
                conditions.Groups.NewGroup("EntityType", typeof(WorkReportResource));
            }

            return GetList(WorkReportResource.SELECT_BY_EXPEDIENTS(conditions, false), childs);
        }

        public static WorkReportResourceList GetByEmployeeList(long oidEmployee, int year, int month, bool clustered, bool childs = true)
        {
            return GetByEmployeeList(new List<long>() { oidEmployee }, year, month, clustered, childs);
        }

        public static WorkReportResourceList GetByEmployeeList(List<long> oidEmployees, int year, int month, bool clustered, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidList = oidEmployees,
                Orders = new OrderList(),
                FechaIni = (year == 0)
                            ? DateTime.MinValue
                            : (month == 0) ? DateAndTime.FirstDay(year) : DateAndTime.FirstDay(month, year),
                FechaFin = (year == 0)
                            ? DateTime.MaxValue
                            : (month == 0) ? DateAndTime.LastDay(year) : DateAndTime.LastDay(month, year),
            };

            if (clustered)
            {
                conditions.Groups = new GroupList();
                conditions.Groups.NewGroup("OidResource", typeof(WorkReportResource));
                conditions.Groups.NewGroup("Year", typeof(WorkReportResource));
                conditions.Groups.NewGroup("Month", typeof(WorkReportResource));

                conditions.Orders.NewOrder("Year", ListSortDirection.Descending, typeof(WorkReportResource));
                conditions.Orders.NewOrder("Month", ListSortDirection.Descending, typeof(WorkReportResource));
            }
            else 
            {
                conditions.Orders.NewOrder("From", ListSortDirection.Descending, typeof(WorkReportResource));
                conditions.Orders.NewOrder("WorkReportID", ListSortDirection.Descending, typeof(WorkReportResource));
            }

            return GetList(WorkReportResource.SELECT_BY_EMPLOYEES(conditions, false), childs);
        }

        public static WorkReportResourceList GetList(IList<WorkReportResource> list) { return new WorkReportResourceList(list,false); }
        public static WorkReportResourceList GetList(IList<WorkReportResourceInfo> list) { return new WorkReportResourceList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<WorkReportResourceInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<WorkReportResourceInfo> sortedList = new SortedBindingList<WorkReportResourceInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<WorkReportResourceInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<WorkReportResourceInfo> sortedList = new SortedBindingList<WorkReportResourceInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Child Factory Methods
						
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>	
		private WorkReportResourceList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static WorkReportResourceList GetChildList(int sessionCode, IDataReader reader, bool childs = false) { return new WorkReportResourceList(sessionCode, reader, childs); }
		public static WorkReportResourceList GetChildList(IList<WorkReportResource> list, bool childs = false) { return new WorkReportResourceList(list, childs); }
        public static WorkReportResourceList GetChildList(IList<WorkReportResourceInfo> list, bool childs = false) { return new WorkReportResourceList(list, childs); }
		
		public static WorkReportResourceList GetChildList(WorkReportInfo parent, bool childs)
		{
			CriteriaEx criteria = WorkReportResource.GetCriteria(WorkReportResource.OpenSession());

			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			WorkReportResourceList list = DataPortal.Fetch<WorkReportResourceList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}		

		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<WorkReportResource> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (WorkReportResource item in lista)
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
                this.AddItem(WorkReportResourceInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(WorkReportResourceInfo.GetChild(SessionCode, reader, Childs));

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(WorkReportResource.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions) { return WorkReportResource.SELECT(conditions, false); }		
		public static string SELECT(WorkReportInfo parent) { return  WorkReportResource.SELECT(new QueryConditions{ WorkReport = parent }, false); }
		
		#endregion		
	}
}
