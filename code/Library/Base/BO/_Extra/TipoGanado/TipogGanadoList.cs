using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.CslaEx; 
using moleQule;
using NHibernate;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class TipoGanadoList : ReadOnlyListBaseEx<TipoGanadoList, TipoGanadoInfo>
	{		 
		#region Factory Methods

		private TipoGanadoList() { }
		
		/// <summary>
		/// Builds a TipoganadoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoganadoList</returns>
		public static TipoGanadoList GetList(bool childs)
		{
			CriteriaEx criteria = TipoGanado.GetCriteria(TipoGanado.OpenSession());
            criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

			TipoGanadoList list = DataPortal.Fetch<TipoGanadoList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a TipoganadoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoganadoList</returns>
		public static TipoGanadoList GetList() {  return TipoGanadoList.GetList(true); }

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static TipoGanadoList GetList(CriteriaEx criteria)
        {
            return TipoGanadoList.RetrieveList(typeof(TipoGanado), AppContext.CommonSchema, criteria);
        }

		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TipoGanadoInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
            SortedBindingList<TipoGanadoInfo> sortedList =
                new SortedBindingList<TipoGanadoInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access

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
						this.AddItem(TipoGanadoInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (TipoGanado item in list)
							this.AddItem(item.GetInfo());

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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoGanado));
			conditions.Orders = orders;
			return TipoGanado.SELECT(conditions, false);
		}

		#endregion
	}
}

