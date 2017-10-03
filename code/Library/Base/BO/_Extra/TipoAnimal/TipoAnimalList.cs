using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class TipoAnimalList : ReadOnlyListBaseEx<TipoAnimalList, TipoAnimalInfo>
	{	 
		#region Factory Methods

		private TipoAnimalList() { }
		
		private TipoAnimalList(IList<TipoAnimal> lista)
		{
            Fetch(lista);
        }

        private TipoAnimalList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a TipoAnimalList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoAnimalList</returns>
		public static TipoAnimalList GetList(bool childs)
		{
			CriteriaEx criteria = TipoAnimal.GetCriteria(TipoAnimal.OpenSession());
            criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

			TipoAnimalList list = DataPortal.Fetch<TipoAnimalList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a TipoAnimalList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoAnimalList</returns>
		public static TipoAnimalList GetList()
		{ 
			return TipoAnimalList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static TipoAnimalList GetList(CriteriaEx criteria)
        {
            return TipoAnimalList.RetrieveList(typeof(TipoAnimal), AppContext.CommonSchema, criteria);
        }
		
		/// <summary>
        /// Builds a TipoAnimalList from a IList<!--<TipoAnimalInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipoAnimalList</returns>
        public static TipoAnimalList GetChildList(IList<TipoAnimalInfo> list)
        {
            TipoAnimalList flist = new TipoAnimalList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (TipoAnimalInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a TipoAnimalList from IList<!--<TipoAnimal>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipoAnimalList</returns>
        public static TipoAnimalList GetChildList(IList<TipoAnimal> list) { return new TipoAnimalList(list); }

        public static TipoAnimalList GetChildList(IDataReader reader) { return new TipoAnimalList(reader); }

		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TipoAnimalInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<TipoAnimalInfo> sortedList =
				new SortedBindingList<TipoAnimalInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<TipoAnimal> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (TipoAnimal item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(TipoAnimal.GetChild(reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
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
						this.AddItem(TipoAnimalInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (TipoAnimal item in list)
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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoAnimal));
			conditions.Orders = orders;
			return TipoAnimal.SELECT(conditions, false);
		}

		#endregion	
	}
}

