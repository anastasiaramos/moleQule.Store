using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class PedidoProveedorList : ReadOnlyListBaseEx<PedidoProveedorList, PedidoProveedorInfo>
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
		private PedidoProveedorList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PedidoProveedorList(IList<PedidoProveedor> list, bool retrieve_childs)
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
		private PedidoProveedorList(IDataReader reader, bool retrieve_childs)
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
		private PedidoProveedorList(IList<PedidoProveedorInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static PedidoProveedorList NewList() { return new PedidoProveedorList(); }

		public static PedidoProveedorList GetList() { return PedidoProveedorList.GetList(true); }
		public static PedidoProveedorList GetList(bool childs) { return GetList(new QueryConditions(), childs); }
		public static PedidoProveedorList GetList(ETipoAcreedor tipo, int year, bool childs)
		{
			return GetList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static PedidoProveedorList GetList(ETipoAcreedor providerType, DateTime from, DateTime till, bool childs)
		{
			return GetList(providerType, 0, from, till, childs);
		}
		public static PedidoProveedorList GetList(ETipoAcreedor providerType,
													long oidProvider,
													DateTime from,
													DateTime till,
													bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
				TipoAcreedor =  new ETipoAcreedor[1] {providerType },
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(conditions, childs);
		}

		public static PedidoProveedorList GetByAcreedorList(long oid, ETipoAcreedor providerType, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = (oid != 0) ? ProviderBaseInfo.New(oid, providerType) : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
			};

			return GetList(conditions, childs);
		}

		public static PedidoProveedorList GetPendientesList(long oid, ETipoAcreedor providerType, long oidSerie, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = (oid != 0) ? ProviderBaseInfo.New(oid, providerType) : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
			};

			return GetList(PedidoProveedor.SELECT_PENDIENTES(conditions, false), childs);
		}

		public static PedidoProveedorList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(PedidoProveedorList.SELECT(conditions), childs);
		}
		private static PedidoProveedorList GetList(string query, bool childs)
		{
			CriteriaEx criteria = PedidoProveedor.GetCriteria(PedidoProveedor.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			PedidoProveedorList list = DataPortal.Fetch<PedidoProveedorList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static PedidoProveedorList GetList(CriteriaEx criteria)
		{
			return PedidoProveedorList.RetrieveList(typeof(PedidoProveedor), AppContext.ActiveSchema.Code, criteria);
		}
        public static PedidoProveedorList GetList(IList<PedidoProveedor> list) { return new PedidoProveedorList(list,false); }
        public static PedidoProveedorList GetList(IList<PedidoProveedorInfo> list) { return new PedidoProveedorList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<PedidoProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<PedidoProveedorInfo> sortedList = new SortedBindingList<PedidoProveedorInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<PedidoProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<PedidoProveedorInfo> sortedList = new SortedBindingList<PedidoProveedorInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<PedidoProveedor> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (PedidoProveedor item in lista)
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
                this.AddItem(PedidoProveedorInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(PedidoProveedorInfo.GetChild(SessionCode, reader, Childs));

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

		protected static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return PedidoProveedor.SELECT(conditions); }

		#endregion
	}
}