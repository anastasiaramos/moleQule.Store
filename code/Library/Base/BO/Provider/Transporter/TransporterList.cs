using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
	public class TransporterList : ReadOnlyListBaseEx<TransporterList, TransporterInfo>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Factory Methods
		 
		private TransporterList() {}

		public static TransporterList NewList() { return new TransporterList(); }

		public static TransporterList GetList() { return TransporterList.GetList(true); }
		public static TransporterList GetList(bool childs) { return GetList(EEstado.Todos, childs); }
		public static TransporterList GetList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = Transporter.GetCriteria(Transporter.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TransporterList.SELECT(new QueryConditions { Estado = estado });

			TransporterList list = DataPortal.Fetch<TransporterList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
        public static TransporterList GetList(ETipoTransportista tipo, bool childs)
        {
            CriteriaEx criteria = Transporter.GetCriteria(Transporter.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TransporterList.SELECT(tipo);

            TransporterList list = DataPortal.Fetch<TransporterList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static TransporterList GetList(CriteriaEx criteria)
		{
			return TransporterList.RetrieveList(typeof(Transporter), AppContext.ActiveSchema.SchemaCode, criteria);
		}
		public static TransporterList GetList(IList<TransporterInfo> list)
		{
			TransporterList flist = new TransporterList();
			
			if (list.Count > 0)
			{
				flist.IsReadOnly = false;
				
				foreach (TransporterInfo item in list)
					flist.AddItem(item);
				
				flist.IsReadOnly = true;
			}
			
			return flist;
		}
		public static TransporterList GetList(IList<Transporter> list)
		{
			TransporterList flist = new TransporterList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (Transporter item in list)
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
		public static SortedBindingList<TransporterInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<TransporterInfo> sortedList = new SortedBindingList<TransporterInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<TransporterInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<TransporterInfo> sortedList = new SortedBindingList<TransporterInfo>(GetList(childs));

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
							this.AddItem(TransporterInfo.GetChild(SessionCode, reader, Childs));
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

        public static string SELECT(ETipoTransportista tipo) { return Transporter.SELECT(tipo, false); }
		public static string SELECT(QueryConditions conditions) { return ProviderBaseInfo.SELECT_BASE(conditions, ETipoAcreedor.TransportistaDestino); }

		#endregion
    }
}

