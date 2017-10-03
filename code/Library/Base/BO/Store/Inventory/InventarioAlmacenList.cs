using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 

using moleQule;

using NHibernate;

namespace moleQule.Library.Store
{
	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// ReadOnly Business Object With Childs Child Collection
	/// </summary>
    [Serializable()]
	public class InventarioAlmacenList : ReadOnlyListBaseEx<InventarioAlmacenList, InventarioAlmacenInfo>
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
		private InventarioAlmacenList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private InventarioAlmacenList(IList<InventarioAlmacen> list, bool retrieve_childs)
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
		private InventarioAlmacenList(IDataReader reader, bool retrieve_childs)
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
		private InventarioAlmacenList(IList<InventarioAlmacenInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static InventarioAlmacenList NewList() { return new InventarioAlmacenList(); }

		public static InventarioAlmacenList GetList() {	return InventarioAlmacenList.GetList(true);	}
		public static InventarioAlmacenList GetList(bool retrieve_childs)
		{
			CriteriaEx criteria = InventarioAlmacen.GetCriteria(InventarioAlmacen.OpenSession());
			criteria.Childs = retrieve_childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = InventarioAlmacenList.SELECT();
            
			InventarioAlmacenList list = DataPortal.Fetch<InventarioAlmacenList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}		
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static InventarioAlmacenList GetList(IList<InventarioAlmacen> list) { return new InventarioAlmacenList(list,false); }
        public static InventarioAlmacenList GetList(IList<InventarioAlmacenInfo> list) { return new InventarioAlmacenList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<InventarioAlmacenInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<InventarioAlmacenInfo> sortedList = new SortedBindingList<InventarioAlmacenInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<InventarioAlmacenInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<InventarioAlmacenInfo> sortedList = new SortedBindingList<InventarioAlmacenInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Child Factory Methods
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static InventarioAlmacenList GetChildList(IList<InventarioAlmacen> list) { return new InventarioAlmacenList(list, false); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		public static InventarioAlmacenList GetChildList(IList<InventarioAlmacen> list, bool retrieve_childs) { return new InventarioAlmacenList(list, retrieve_childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static InventarioAlmacenList GetChildList(IDataReader reader) { return new InventarioAlmacenList(reader, false); } 
		
		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        public static InventarioAlmacenList GetChildList(IDataReader reader, bool retrieve_childs) { return new InventarioAlmacenList(reader, retrieve_childs); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static InventarioAlmacenList GetChildList(IList<InventarioAlmacenInfo> list) { return new InventarioAlmacenList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<InventarioAlmacen> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (InventarioAlmacen item in lista)
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
                this.AddItem(InventarioAlmacenInfo.GetChild(reader, Childs));

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
						this.AddItem(InventarioAlmacenInfo.GetChild(reader, Childs));

					IsReadOnly = true;
				}
				else 
				{
					IList list = criteria.List();
					
					if (list.Count > 0)
					{
						IsReadOnly = false;
						foreach(InventarioAlmacen item in list)
							this.Add(item.GetInfo(false));
							
						IsReadOnly = true;
					}
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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return InventarioAlmacen.SELECT(conditions, false); }
		public static string SELECT(StoreInfo parent) { return SELECT(new QueryConditions { Almacen = parent }); }

        #endregion		
	}
}

