using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Common;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class LineaFomentoList : ReadOnlyListBaseEx<LineaFomentoList, LineaFomentoInfo>
	{	
		#region Business Methods

		public decimal TotalSubvencionable()
		{
			decimal total = 0;
			foreach (LineaFomentoInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				if (item.EEstado == EEstado.Desestimado) continue;
				total += item.Total;
			}

			return total;
		}

		public decimal TotalPendiente()
		{
			decimal total = 0;
			foreach (LineaFomentoInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				if (item.EEstado == EEstado.Charged) continue;
				if (item.EEstado == EEstado.Desestimado) continue;
				total += item.Subvencion;
			}

			return total;
		}

		public decimal TotalCobrado()
		{
			decimal total = 0;
			foreach (LineaFomentoInfo item in this)
			{
				if (item.EEstado == EEstado.Charged) continue;
				total += item.Subvencion;
			}

			return total;
		}

		public decimal TotalSolicitado()
		{
			decimal total = 0;
			foreach (LineaFomentoInfo item in this)
			{
				if ((item.EEstado == EEstado.EnSolicitud) 
					|| (item.EEstado == EEstado.Aceptado))
					total += item.Subvencion;
			}

			return total;
		}

		public decimal TotalSubvencionSolicitada()
		{
			decimal total = 0;
			foreach (LineaFomentoInfo item in this)
				total += item.Subvencion;

			return total;
		}

        public List<int> GetAnyosList()
        {
            List<int> anyos = new List<int>();
            SortedBindingList<LineaFomentoInfo> ordered_list = GetSortedList(this, "FechaConocimiento", ListSortDirection.Ascending);

            foreach (LineaFomentoInfo item in ordered_list)
            {
                if (!anyos.Contains(item.FechaConocimiento.Year))
                    anyos.Add(item.FechaConocimiento.Year);
            }

            return anyos;
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LineaFomentoList() {}
		private LineaFomentoList(IList<LineaFomento> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private LineaFomentoList(int sessionCode, IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		private LineaFomentoList(IList<LineaFomentoInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static LineaFomentoList NewList() { return new LineaFomentoList(); }

        public static LineaFomentoList GetList() { return LineaFomentoList.GetList(false); }
        public static LineaFomentoList GetList(bool childs)
        {
			return LineaFomentoList.GetList(SELECT(), childs);
        }
		public static LineaFomentoList GetList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till,
			};

			return LineaFomentoList.GetList(SELECT(conditions), childs);
		}
		public static LineaFomentoList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SELECT(conditions), childs);
		}

		public static LineaFomentoList GetPendientesList(DateTime fFin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaFin = fFin
			};

			return LineaFomentoList.GetList(SELECT_PENDIENTES(conditions), childs);
		}

        public static LineaFomentoList GetByCobroAndPendientesList(long oidCharge)
        {
            QueryConditions conditions = new QueryConditions(oidCharge, ETipoEntidad.Cobro);
            return LineaFomentoList.GetList(SELECT_BY_COBRO_AND_PENDIENTES(conditions), false);
        }

        public static LineaFomentoList GetByCobroList(long oidCharge)
        {
            QueryConditions conditions = new QueryConditions(oidCharge, ETipoEntidad.Cobro);
            return LineaFomentoList.GetList(SELECT_BY_COBRO(conditions), false);
        }

		public static LineaFomentoList GetInformeFomentoList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = childs;

			//No criteria. Retrieve all de List

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LineaFomentoList.SELECT_INFORME_FOMENTO(conditions);

			LineaFomentoList list = DataPortal.Fetch<LineaFomentoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		private static LineaFomentoList GetList(string query, bool childs)
		{
			CriteriaEx criteria = LineaFomento.GetCriteria(LineaFomento.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			LineaFomentoList list = DataPortal.Fetch<LineaFomentoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a ExpedienteFomentoList from a IList<!--<>-->
		/// </summary>
		/// <param name="list"></param>
		/// <returns>ExpedienteFomentoList</returns>
        public static LineaFomentoList GetList(IList<LineaFomentoInfo> list)
        {
            LineaFomentoList flist = new LineaFomentoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (LineaFomentoInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static LineaFomentoList GetList(IList<LineaFomento> list)
		{
			LineaFomentoList flist = new LineaFomentoList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (LineaFomento item in list)
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
        public static SortedBindingList<LineaFomentoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<LineaFomentoInfo> sortedList = new SortedBindingList<LineaFomentoInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<LineaFomentoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<LineaFomentoInfo> sortedList = new SortedBindingList<LineaFomentoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

		#region Child Factory Methods
		
		public static LineaFomentoList GetChildList(IList<LineaFomento> list) { return new LineaFomentoList(list, false); }
		public static LineaFomentoList GetChildList(IList<LineaFomento> list, bool childs) { return new LineaFomentoList(list, childs); }
		public static LineaFomentoList GetChildList(int sessionCode, IDataReader reader) { return new LineaFomentoList(sessionCode, reader, false); }
		public static LineaFomentoList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new LineaFomentoList(sessionCode, reader, childs); }
        public static LineaFomentoList GetChildList(IList<LineaFomentoInfo> list) { return new LineaFomentoList(list, false); }
		public static LineaFomentoList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = LineaFomento.GetCriteria(LineaFomento.OpenSession());
			criteria.Query = LineaFomentoList.SELECT(parent);
			criteria.Childs = childs;

			LineaFomentoList list = DataPortal.Fetch<LineaFomentoList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion
	
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<LineaFomento> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (LineaFomento item in lista)
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
                this.AddItem(LineaFomentoInfo.GetChild(reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion		

		#region Root Data Access

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
						this.AddItem(LineaFomentoInfo.GetChild(reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				CloseSession();
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

        public static string SELECT() { return LineaFomentoInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LineaFomento.SELECT(conditions, false); }
		public static string SELECT(ExpedientInfo parent) { return  LineaFomento.SELECT(new QueryConditions{ Expedient = parent }, true); }
        public static string SELECT_PENDIENTES(QueryConditions conditions) { return LineaFomento.SELECT_PENDIENTES(conditions, false); }
        public static string SELECT_BY_COBRO_AND_PENDIENTES(QueryConditions conditions) { return LineaFomento.SELECT_BY_COBRO_AND_PENDIENTES(conditions, false); }
        public static string SELECT_BY_COBRO(QueryConditions conditions) { return LineaFomento.SELECT_BY_COBRO(conditions, false); }
		public static string SELECT_INFORME_FOMENTO(QueryConditions conditions) { return LineaFomento.SELECT_INFORME_FOMENTO(conditions, false); }

		#endregion		
	}
}