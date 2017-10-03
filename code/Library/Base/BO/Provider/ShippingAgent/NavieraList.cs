using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule.Common;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
	public class NavieraList : ReadOnlyListBaseEx<NavieraList, NavieraInfo>
	{
		#region Business Methods
			
		#endregion
		 
		#region Factory Methods
		 
		private NavieraList() {}

		public static NavieraList NewList() { return new NavieraList(); }

		public static NavieraList GetList() { return NavieraList.GetList(true);	}
		public static NavieraList GetList(bool childs) { return GetList(EEstado.Todos, childs); }
		public static NavieraList GetList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = Proveedor.GetCriteria(Naviera.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = NavieraList.SELECT(new QueryConditions { Estado = estado });

			NavieraList list = DataPortal.Fetch<NavieraList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static NavieraList GetList(CriteriaEx criteria)
		{
			return NavieraList.RetrieveList(typeof(Naviera), AppContext.ActiveSchema.SchemaCode, criteria);
		}
		public static NavieraList GetList(IList<NavieraInfo> list)
		{
			NavieraList flist = new NavieraList();
			
			if (list.Count > 0)
			{
				flist.IsReadOnly = false;
				
				foreach (NavieraInfo item in list)
					flist.AddItem(item);
				
				flist.IsReadOnly = true;
			}
			
			return flist;
		}
		public static NavieraList GetList(IList<Naviera> list)
		{
			NavieraList flist = new NavieraList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (Naviera item in list)
					flist.AddItem(item.GetInfo());

				flist.IsReadOnly = true;
			}

			return flist;
		}
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<NavieraInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<NavieraInfo> sortedList = new SortedBindingList<NavieraInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<NavieraInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<NavieraInfo> sortedList = new SortedBindingList<NavieraInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }	
			
		#endregion
		
		#region Data Access
		 
		 	// called to retrieve data from database
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
							this.AddItem(NavieraInfo.GetChild(SessionCode, reader, Childs));
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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return ProviderBaseInfo.SELECT_BASE(conditions, ETipoAcreedor.Naviera); }

        #endregion
	}
}

