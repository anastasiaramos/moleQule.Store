using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class LivestockBooks : BusinessListBaseEx<LivestockBooks, LivestockBook>
    {		
		#region Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public LivestockBook NewItem()
        {
            this.AddItem(LivestockBook.NewChild());
            return this[Count - 1];
        }
       
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LivestockBooks() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LivestockBooks NewList() 
		{ 	
			if (!LivestockBook.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LivestockBooks(); 
		}

        public static LivestockBooks GetList() { return GetList(true); }		
		public static LivestockBooks GetList(bool childs) 
		{
			return GetList(LivestockBooks.SELECT(), childs);
		}
		public static LivestockBooks GetList(QueryConditions conditions, bool childs)
		{
			return GetList(LivestockBooks.SELECT(conditions), childs);
		}

		public static LivestockBooks GetByExpedienteList(long oidExpediente, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
                Expedient = ExpedientInfo.New(oidExpediente),
				TipoExpediente = ETipoExpediente.Ganado
			};

			return GetList(LivestockBooks.SELECT(conditions), childs);
		}

		private static LivestockBooks GetList(string query, bool childs)
		{
            if (!LivestockBook.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = LivestockBook.GetCriteria(LivestockBook.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			LivestockBook.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<LivestockBooks>(criteria);
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
                        this.AddItem(LivestockBook.GetChild(SessionCode, reader, Childs));
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
            foreach (LivestockBook obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (LivestockBook obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

				if (!SaveAsChildList) Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (!SaveAsChildList) if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
				if (!SaveAsChildList) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region SQL

        public static string SELECT() { return LivestockBook.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LivestockBook.SELECT(conditions, true); }
		public static string SELECT(LivestockBook parent) { return LivestockBook.SELECT(new QueryConditions{ LibroGanadero = parent.GetInfo(false) }, true); }
				
		#endregion
    }
}