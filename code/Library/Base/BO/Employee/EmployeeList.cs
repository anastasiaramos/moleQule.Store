using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class EmployeeList : ReadOnlyListBaseEx<EmployeeList, EmployeeInfo, Employee>
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
		private EmployeeList() {}
		private EmployeeList(IList<Employee> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private EmployeeList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		private EmployeeList(IList<EmployeeInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static EmployeeList NewList() { return new EmployeeList(); }

		public static EmployeeList GetList(bool childs = true)
		{
			CriteriaEx criteria = Employee.GetCriteria(Employee.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = EmployeeList.SELECT();
            
			EmployeeList list = DataPortal.Fetch<EmployeeList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static EmployeeList GetList(bool childs, bool cache)
		{
			EmployeeList list;

			if (!Cache.Instance.Contains(typeof(EmployeeList)))
			{
				list = EmployeeList.GetList(childs);
				Cache.Instance.Save(typeof(EmployeeList), list);
			}
			else
				list = Cache.Instance.Get(typeof(EmployeeList)) as EmployeeList;

			return list;
		}
		public static EmployeeList GetList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = Employee.GetCriteria(Employee.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = EmployeeList.SELECT(new QueryConditions { Estado = estado });

			EmployeeList list = DataPortal.Fetch<EmployeeList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}        
        public static EmployeeList GetList(QueryConditions conditions, bool childs)
        {
            return GetList(EmployeeList.SELECT(conditions), childs);
        }

        private static EmployeeList GetList(string query, bool childs)
        {
            CriteriaEx criteria = Employee.GetCriteria(Employee.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            EmployeeList list = DataPortal.Fetch<EmployeeList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static EmployeeList GetList(CriteriaEx criteria)
		{
			return EmployeeList.RetrieveList(typeof(Employee), AppContext.ActiveSchema.Code, criteria);
		}
        public static EmployeeList GetList(IList<Employee> list) { return new EmployeeList(list, false); }
        public static EmployeeList GetList(IList<EmployeeInfo> list) { return new EmployeeList(list, false); }

        public static EmployeeList GetAvailableList( bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Status = new EEstado[] { EEstado.Alta, EEstado.Active, EEstado.Inactive }
            };

            return GetList(conditions, childs);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<EmployeeInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<EmployeeInfo> sortedList = new SortedBindingList<EmployeeInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<EmployeeInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<EmployeeInfo> sortedList = new SortedBindingList<EmployeeInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Employee> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Employee item in lista)
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
				this.AddItem(EmployeeInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(EmployeeInfo.GetChild(SessionCode, reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return ProviderBaseInfo.SELECT_BASE(conditions, ETipoAcreedor.Empleado); }

		#endregion
	}
}

