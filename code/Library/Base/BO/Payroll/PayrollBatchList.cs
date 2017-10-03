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
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class PayrollBatchList : ReadOnlyListBaseEx<PayrollBatchList, PayrollBatchInfo>
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
		private PayrollBatchList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PayrollBatchList(IList<PayrollBatch> list, bool retrieve_childs)
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
		private PayrollBatchList(IDataReader reader, bool retrieve_childs)
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
		private PayrollBatchList(IList<PayrollBatchInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static PayrollBatchList NewList() { return new PayrollBatchList(); }

		public static PayrollBatchList GetList() { return PayrollBatchList.GetList(true); }
		public static PayrollBatchList GetList(bool childs)
		{
			CriteriaEx criteria = PayrollBatch.GetCriteria(PayrollBatch.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = PayrollBatchList.SELECT();
            
			PayrollBatchList list = DataPortal.Fetch<PayrollBatchList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static PayrollBatchList GetList(Library.Store.QueryConditions conditions, bool childs)
		{
			return GetList(PayrollBatchList.SELECT(conditions), childs);
		}

		/// <summary>
		/// Devuelve una lista de elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		private static PayrollBatchList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Payment.GetCriteria(PayrollBatch.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			PayrollBatchList list = DataPortal.Fetch<PayrollBatchList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static PayrollBatchList GetList(CriteriaEx criteria)
		{
			return PayrollBatchList.RetrieveList(typeof(PayrollBatch), AppContext.ActiveSchema.Code, criteria);
		}
        public static PayrollBatchList GetList(IList<PayrollBatch> list) { return new PayrollBatchList(list,false); }
        public static PayrollBatchList GetList(IList<PayrollBatchInfo> list) { return new PayrollBatchList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<PayrollBatchInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<PayrollBatchInfo> sortedList = new SortedBindingList<PayrollBatchInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<PayrollBatchInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<PayrollBatchInfo> sortedList = new SortedBindingList<PayrollBatchInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<PayrollBatch> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (PayrollBatch item in lista)
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
                this.AddItem(PayrollBatchInfo.GetChild(reader, Childs));

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
						this.AddItem(PayrollBatchInfo.GetChild(reader, Childs));

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

        public static string SELECT() { return PayrollBatchInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return PayrollBatch.SELECT(conditions, false); }
		public static string SELECT(PayrollBatchInfo parent) { return PayrollBatch.SELECT(new QueryConditions{ RemesaNomina = parent }, true); }
		
		#endregion		
	}
}
