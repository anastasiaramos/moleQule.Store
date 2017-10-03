using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class LivestockBookLines : BusinessListBaseEx<LivestockBookLines, LivestockBookLine>
	{
		#region Business Methods

        public void SetMaxSerial()
        {
            foreach (LivestockBookLine item in this)
                if (item.Serial > MaxSerial) MaxSerial = item.Serial;
        }

		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public LivestockBookLine NewItem(LivestockBook parent)
		{
			this.NewItem(LivestockBookLine.NewChild(parent));
			LivestockBookLine item = this[Count - 1];
            if (MaxSerial == 0) MaxSerial = item.Serial - 1;
			SetNextCode(parent, item);
			return item;
		}

		public LivestockBookLine GetItemByExpediente(long oidExpedient)
		{
            return Items.FirstOrDefault(x => x.OidExpediente == oidExpedient);
		}
        public LivestockBookLine GetItemByBatch(long oidBatch, ETipoLineaLibroGanadero type)
        {
            return Items.FirstOrDefault(x => x.OidPartida == oidBatch && x.ETipo == type);
        }
        public LivestockBookLine GetItemByPartidaByConceptoAlbaran(long oidBatch, long oidDeliveryLine, ETipoLineaLibroGanadero type)
		{
            return Items.FirstOrDefault(x => x.OidPartida == oidBatch && (x.OidConceptoAlbaran == oidDeliveryLine) && x.ETipo == type);
		}

		public void SetNextCode(LivestockBook parent, LivestockBookLine item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode(parent.Oid);
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
				MaxSerial++;
			}
		}

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Crea una nueva lista vacía
		/// </summary>
		/// <returns>Lista vacía</returns>
		public static LivestockBookLines NewList()
		{
			if (!LivestockBookLine.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new LivestockBookLines();
		}

		public static LivestockBookLines GetList() { return GetList(true); }
		public static LivestockBookLines GetList(bool childs)
		{
			return GetList(LivestockBookLines.SELECT(), childs);
		}

        public static LivestockBookLines GetByExpedienteList(long oidExpedient, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
                Expedient = ExpedientInfo.New(oidExpedient)
			};

			return GetList(SELECT_BY_EXPEDIENTE(conditions), false);
		}

		public static LivestockBookLines GetList(QueryConditions conditions, bool childs)
		{
			return GetList(LivestockBookLines.SELECT(conditions), childs);
		}
		private static LivestockBookLines GetList(string query, bool childs)
		{
			if (!LivestockBookLine.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<LivestockBookLines>(criteria);
		}

		#endregion
	
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LivestockBookLines() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private LivestockBookLines(IList<LivestockBookLine> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private LivestockBookLines(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
				
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LivestockBookLines NewChildList() 
        { 
            LivestockBookLines list = new LivestockBookLines(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static LivestockBookLines GetChildList(IList<LivestockBookLine> lista) { return new LivestockBookLines(lista); }
        public static LivestockBookLines GetChildList(int sessionCode,IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static LivestockBookLines GetChildList(int sessionCode,IDataReader reader, bool childs) { return new LivestockBookLines(sessionCode, reader, childs); }

		public static LivestockBookLines GetChildListByExpediente(LivestockBook parent, long oidExpediente, bool childs)
		{
			CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				LibroGanadero = parent.GetInfo(false),
                Expedient = ExpedientInfo.New(oidExpediente)
			};

			criteria.Query = LivestockBookLines.SELECT(conditions);

			return DataPortal.Fetch<LivestockBookLines>(criteria);
		}

		#endregion

		#region Root Data Access

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="criteria">Criterios de la consulta</param>
		/// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			Fetch(criteria);
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="criteria">Criterios de la consulta</param>
		private void Fetch(CriteriaEx criteria)
		{
			try
			{
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					LivestockBook.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(LivestockBookLine.GetChild(SessionCode, reader, Childs));

					SetMaxSerial();
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}

		/// <summary>
		/// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
		protected override void DataPortal_Update()
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (LivestockBookLine obj in DeletedList)
				obj.DeleteSelf(this);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (LivestockBookLine obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				BeginTransaction();
				this.RaiseListChangedEvents = true;
			}
		}

		#endregion		

		#region Child Data Access

        private void Fetch(IList<LivestockBookLine> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (LivestockBookLine item in lista)
				this.AddItem(LivestockBookLine.GetChild(item, Childs));

			SetMaxSerial();

			this.RaiseListChangedEvents = true;
		}
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(LivestockBookLine.GetChild(SessionCode, reader, Childs));

			SetMaxSerial();

            this.RaiseListChangedEvents = true;
        }
				
		internal void Update(LivestockBook parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (LivestockBookLine obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (LivestockBookLine obj in this)
			{	
				if (obj.IsNew)
				{
					//SetNextCode(parent, obj);
					obj.Insert(parent);
				}
				else
					obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return LivestockBookLine.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LivestockBookLine.SELECT(conditions, true); }
		public static string SELECT(LivestockBook parent) { return LivestockBookLine.SELECT(new QueryConditions{ LibroGanadero = parent.GetInfo(false) }, true); }

		public static string SELECT_BY_EXPEDIENTE(QueryConditions conditions) { return LivestockBookLine.SELECT_BY_EXPEDIENTE(conditions, true); }

		#endregion
    }
}

