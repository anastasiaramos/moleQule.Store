using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Root Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class FamiliaList : ReadOnlyListBaseEx<FamiliaList, FamiliaInfo>
	{		 
		#region Factory Methods

		private FamiliaList() { }
		
		private FamiliaList(IList<Familia> lista)
		{
            Fetch(lista);
        }

        private FamiliaList(IDataReader reader)
		{
			Fetch(reader);
		}

		public static FamiliaList NewList() { return new FamiliaList(); }

		public static FamiliaList GetList() { return FamiliaList.GetList(true); }
		public static FamiliaList GetList(bool childs)
		{
			CriteriaEx criteria = Familia.GetCriteria(Familia.OpenSession());
            criteria.Childs = childs;
		
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = FamiliaList.SELECT();

			FamiliaList list = DataPortal.Fetch<FamiliaList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}
		public static FamiliaList GetList(bool childs, bool cache)
		{
			FamiliaList list;

			if (!Cache.Instance.Contains(typeof(FamiliaList)))
			{
				list = FamiliaList.GetList(childs);
				Cache.Instance.Save(typeof(FamiliaList), list);
			}
			else
				list = Cache.Instance.Get(typeof(FamiliaList)) as FamiliaList;

			return list;
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static FamiliaList GetList(CriteriaEx criteria)
        {
			return FamiliaList.RetrieveList(typeof(Familia), AppContext.ActiveSchema.SchemaCode, criteria);
        }
        public static FamiliaList GetList(IList<FamiliaInfo> list)
        {
            FamiliaList flist = new FamiliaList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (FamiliaInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static FamiliaList GetList(IList<Familia> list) { return new FamiliaList(list); }
        public static FamiliaList GetList(IDataReader reader) { return new FamiliaList(reader); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<FamiliaInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<FamiliaInfo> sortedList =
				new SortedBindingList<FamiliaInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<Familia> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Familia item in lista)
                this.Add(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.Add(FamiliaInfo.GetChild(SessionCode, reader));

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
						this.AddItem(FamiliaInfo.GetChild(SessionCode, reader, Childs));

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

        public static string SELECT()
        {
            return FamiliaInfo.SELECT(0) +
                    " ORDER BY FM.\"NOMBRE\"";
        }

        #endregion
	}
}

