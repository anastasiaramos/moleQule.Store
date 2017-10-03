using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using moleQule;
using NHibernate;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class RazaAnimalList : ReadOnlyListBaseEx<RazaAnimalList, RazaAnimalInfo>
	{			 
		#region Factory Methods

		private RazaAnimalList() { }
		
		private RazaAnimalList(IList<RazaAnimal> lista)
		{
            Fetch(lista);
        }

        private RazaAnimalList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a RazaAnimalList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>RazaAnimalList</returns>
		public static RazaAnimalList GetList(bool childs)
		{
			CriteriaEx criteria = RazaAnimal.GetCriteria(RazaAnimal.OpenSession());
            criteria.Childs = childs;			
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();			

			RazaAnimalList list = DataPortal.Fetch<RazaAnimalList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a RazaAnimalList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>RazaAnimalList</returns>
		public static RazaAnimalList GetList()
		{ 
			return RazaAnimalList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static RazaAnimalList GetList(CriteriaEx criteria)
        {
            return RazaAnimalList.RetrieveList(typeof(RazaAnimal), AppContext.CommonSchema, criteria);
        }
		
		/// <summary>
        /// Builds a RazaAnimalList from a IList<!--<RazaAnimalInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>RazaAnimalList</returns>
        public static RazaAnimalList GetChildList(IList<RazaAnimalInfo> list)
        {
            RazaAnimalList flist = new RazaAnimalList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (RazaAnimalInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a RazaAnimalList from IList<!--<RazaAnimal>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>RazaAnimalList</returns>
        public static RazaAnimalList GetChildList(IList<RazaAnimal> list) { return new RazaAnimalList(list); }

        public static RazaAnimalList GetChildList(IDataReader reader) { return new RazaAnimalList(reader); }

		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<RazaAnimalInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<RazaAnimalInfo> sortedList =
				new SortedBindingList<RazaAnimalInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<RazaAnimal> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (RazaAnimal item in lista)
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
                this.AddItem(RazaAnimal.GetChild(reader).GetInfo());

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
						this.AddItem(RazaAnimalInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (RazaAnimal item in list)
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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(RazaAnimal));
			conditions.Orders = orders;
			return RazaAnimal.SELECT(conditions, false);
		}

		#endregion
	}
}

