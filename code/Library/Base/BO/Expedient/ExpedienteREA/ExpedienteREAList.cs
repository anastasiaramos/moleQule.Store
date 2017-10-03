using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class ExpedienteREAList : ReadOnlyListBaseEx<ExpedienteREAList, ExpedienteREAInfo>
	{	
		#region Business Methods

		public decimal TotalCobrada()
		{
			decimal total = 0;
			foreach (ExpedienteREAInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.AyudaCobrada;
			}

			return total;
		}

		public decimal TotalPendiente()
		{
			decimal total = 0;
			foreach (ExpedienteREAInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.AyudaPendiente;
			}

			return total;
		}

        public decimal GetTotalAyudas(string codigoAduanero)
        {
            decimal ayudas = 0;

            foreach (ExpedienteREAInfo item in this)
            {
                if (item.CodigoAduanero == codigoAduanero)
                    ayudas += item.AyudaCobrada;
            }

            return ayudas;
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ExpedienteREAList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ExpedienteREAList(IList<REAExpedient> list, bool retrieve_childs)
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
		private ExpedienteREAList(IList<ExpedienteREAInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static ExpedienteREAList NewList() { return new ExpedienteREAList(); }

		public static ExpedienteREAList GetList() { return ExpedienteREAList.GetList(true); }
		public static ExpedienteREAList GetList(bool childs)
		{
			return GetList(SELECT(), childs);
		}
		public static ExpedienteREAList GetList(DateTime fIni, DateTime fFin, EEstado estado, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = fIni,
				FechaFin = fFin,
				Estado = estado
			};
			return GetList(SELECT(conditions), childs);
		}
		public static ExpedienteREAList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SELECT(conditions), childs);
		}

		public static ExpedienteREAList GetListByREA(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = REAExpedient.GetCriteria(REAExpedient.OpenSession());
			criteria.Childs = childs;

			criteria.Query = REAExpedient.SELECT_CONTROL_REA(conditions);

			ExpedienteREAList list = DataPortal.Fetch<ExpedienteREAList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		private static ExpedienteREAList GetList(string query, bool childs)
		{
			CriteriaEx criteria = REAExpedient.GetCriteria(REAExpedient.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			ExpedienteREAList list = DataPortal.Fetch<ExpedienteREAList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static ExpedienteREAList GetList(IList<REAExpedient> list) { return new ExpedienteREAList(list,false); }
        public static ExpedienteREAList GetList(IList<ExpedienteREAInfo> list) { return new ExpedienteREAList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ExpedienteREAInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<ExpedienteREAInfo> sortedList = new SortedBindingList<ExpedienteREAInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<ExpedienteREAInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ExpedienteREAInfo> sortedList = new SortedBindingList<ExpedienteREAInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Child Factory Methods
						
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>		
		private ExpedienteREAList(int sessionCode, IDataReader reader, bool childs)
        {
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
				
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static ExpedienteREAList GetChildList(IList<REAExpedient> list) { return new ExpedienteREAList(list, false); }
		public static ExpedienteREAList GetChildList(IList<REAExpedient> list, bool childs) { return new ExpedienteREAList(list, childs); }
		public static ExpedienteREAList GetChildList(IList<ExpedienteREAInfo> list) { return new ExpedienteREAList(list, false); }
		public static ExpedienteREAList GetChildList(int sessionCode, IDataReader reader) { return new ExpedienteREAList(sessionCode, reader, false); }
		public static ExpedienteREAList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new ExpedienteREAList(sessionCode, reader, childs); }
		public static ExpedienteREAList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = REAExpedient.GetCriteria(REAExpedient.OpenSession());
			criteria.Query = ExpedienteREAList.SELECT(parent);
			criteria.Childs = childs;

			ExpedienteREAList list = DataPortal.Fetch<ExpedienteREAList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<REAExpedient> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (REAExpedient item in lista)
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
                this.AddItem(ExpedienteREAInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(ExpedienteREAInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
			
			this.RaiseListChangedEvents = true;
		}
				
		#endregion
		
        #region SQL

        public static string SELECT() { return ExpedienteREAInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return REAExpedient.SELECT(conditions, false); }
		public static string SELECT(ExpedientInfo parent) { return SELECT(new QueryConditions{ Expedient = parent }); }
		
		#endregion		
	}
}
