using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class TipoGastoList : ReadOnlyListBaseEx<TipoGastoList, TipoGastoInfo>
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
		private TipoGastoList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TipoGastoList(IList<TipoGasto> list, bool retrieve_childs)
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
		private TipoGastoList(IDataReader reader, bool retrieve_childs)
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
		private TipoGastoList(IList<TipoGastoInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static TipoGastoList NewList() { return new TipoGastoList(); }

		public static TipoGastoList GetList() {	return TipoGastoList.GetList(true);	}
		public static TipoGastoList GetList(bool childs)
		{
			CriteriaEx criteria = TipoGasto.GetCriteria(TipoGasto.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TipoGastoList.SELECT();
            
			TipoGastoList list = DataPortal.Fetch<TipoGastoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static TipoGastoList GetList(bool childs, bool cache)
		{
			TipoGastoList list;

			if (!Cache.Instance.Contains(typeof(TipoGastoList)))
			{
				list = TipoGastoList.GetList(childs);
				Cache.Instance.Save(typeof(TipoGastoList), list);
			}
			else
				list = Cache.Instance.Get(typeof(TipoGastoList)) as TipoGastoList;

			return list;
		}
        public static TipoGastoList GetSelectList(bool childs)
        {
            CriteriaEx criteria = TipoGasto.GetCriteria(TipoGasto.OpenSession());
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions { CategoriaGasto = ECategoriaGasto.Seleccione };

            //No criteria. Retrieve all de List

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TipoGastoList.SELECT(conditions);

            TipoGastoList list = DataPortal.Fetch<TipoGastoList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }
	
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static TipoGastoList GetList(IList<TipoGasto> list) { return new TipoGastoList(list,false); }
        public static TipoGastoList GetList(IList<TipoGastoInfo> list) { return new TipoGastoList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TipoGastoInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<TipoGastoInfo> sortedList = new SortedBindingList<TipoGastoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<TipoGastoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<TipoGastoInfo> sortedList = new SortedBindingList<TipoGastoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<TipoGasto> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (TipoGasto item in lista)
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
                this.AddItem(TipoGastoInfo.GetChild(reader, Childs));

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
						this.AddItem(TipoGastoInfo.GetChild(reader, Childs));

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

        public static string SELECT() { return TipoGastoInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return TipoGasto.SELECT(conditions, false); }
		public static string SELECT(TipoGastoInfo parent) { return  TipoGasto.SELECT(new QueryConditions{ TipoGasto = parent }, true); }
		
		#endregion		
	}
}
