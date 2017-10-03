using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class LivestockBookLineList : ReadOnlyListBaseEx<LivestockBookLineList, LivestockBookLineInfo>
	{	
		#region Business Methods

		public void NewItem(LivestockBookLineInfo item)
		{
			base.AddItem(item);
			UpdateBalance();
		}

		public override void RemoveItem(long oid)
		{
			base.RemoveItem(oid);
			UpdateBalance();
		}

		/// <summary>
		/// Actualiza el balance de cada linea
		/// </summary>
		public virtual void UpdateBalance()
		{
			if (Items.Count == 0) return;

			int balance = 0;

			foreach (LivestockBookLineInfo item in this)
			{
				switch (item.EEstado)
				{
					case EEstado.Alta:
						balance++;
						item.Balance = balance;
						break;

					case EEstado.Baja:
						balance--;
						item.Balance = balance;
						break;

					default:
						item.Balance = 0;
						break;

				}
			}
		}

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LivestockBookLineList() {}
		private LivestockBookLineList(IList<LivestockBookLine> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private LivestockBookLineList(IList<LivestockBookLineInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Child Factory Methods
						
		private LivestockBookLineList(int sessionCode, IDataReader reader, bool childs)
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
		public static LivestockBookLineList GetChildList(IList<LivestockBookLine> list) { return new LivestockBookLineList(list, false); }
		public static LivestockBookLineList GetChildList(IList<LivestockBookLine> list, bool childs) { return new LivestockBookLineList(list, childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>

		public static LivestockBookLineList GetChildList(int sessionCode, IDataReader reader) { return new LivestockBookLineList(sessionCode, reader, false); }
		public static LivestockBookLineList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new LivestockBookLineList(sessionCode, reader, childs); }
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static LivestockBookLineList GetChildList(IList<LivestockBookLineInfo> list) { return new LivestockBookLineList(list, false); }
		
		#endregion

		#region Root Factory Methods

		public static LivestockBookLineList NewList() { return new LivestockBookLineList(); }

		public static LivestockBookLineList GetList(long oidLibro) { return LivestockBookLineList.GetList(oidLibro, false); }
		public static LivestockBookLineList GetList(long oidLibro, bool childs)
		{
			QueryConditions conditions = new QueryConditions { LibroGanadero = LivestockBook.New().GetInfo(false) };
			conditions.LibroGanadero.Oid = oidLibro;

			return GetList(SELECT(conditions), childs);
		}

        public static LivestockBookLineList GetAvailableList(long oidBook, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                LibroGanadero = LivestockBookInfo.New(oidBook)
            };

            return GetList(LivestockBookLine.SELECT_AVAILABLE(conditions, false), false);
        }

		public static LivestockBookLineList GetByExpedienteList(long oidExpedient, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
                Expedient = ExpedientInfo.New(oidExpedient)
			};

			return GetList(SELECT_BY_EXPEDIENTE(conditions), false);
		}

        public static LivestockBookLineList GetByStatusList(EEstado status, bool childs)
        {
            return GetList(LivestockBookLine.SELECT(new QueryConditions { Status = new EEstado[] { status } }, false), false);
        }

		private static LivestockBookLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = LivestockBookLine.GetCriteria(LivestockBookLine.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;

			LivestockBookLineList list = DataPortal.Fetch<LivestockBookLineList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a ExpedienteFomentoList from a IList<!--<>-->
		/// </summary>
		/// <param name="list"></param>
		/// <returns>ExpedienteFomentoList</returns>
		public static LivestockBookLineList GetList(IList<LivestockBookLineInfo> list)
		{
			LivestockBookLineList flist = new LivestockBookLineList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (LivestockBookLineInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

		public static LivestockBookLineList GetList(IList<LivestockBookLine> list)
		{
			LivestockBookLineList flist = new LivestockBookLineList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (LivestockBookLine item in list)
					flist.AddItem(item.GetInfo());

				flist.IsReadOnly = true;
			}

			return flist;
		}

		#endregion

		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<LivestockBookLine> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (LivestockBookLine item in lista)
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
                this.AddItem(LivestockBookLineInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(LivestockBookLineInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					UpdateBalance();
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				CloseSession();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

        public static string SELECT() { return LivestockBookLineInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LivestockBookLine.SELECT(conditions, false); }
		public static string SELECT(LivestockBookInfo parent) { return  LivestockBookLine.SELECT(new QueryConditions{ LibroGanadero = parent }, true); }

		public static string SELECT_BY_EXPEDIENTE(QueryConditions conditions) { return LivestockBookLine.SELECT_BY_EXPEDIENTE(conditions, false); }

		#endregion		
	}
}