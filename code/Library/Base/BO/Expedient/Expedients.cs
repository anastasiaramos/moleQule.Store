using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Resources;
using moleQule.Library.Store.Resources;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class Expedients : BusinessListBaseEx<Expedients, Expedient>
    {		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Expedient NewItem()
        {
            this.AddItem(Expedient.NewChild());
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
        private Expedients() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Expedients NewList() 
		{ 	
			if (!Expedient.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new Expedients(); 
		}

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        /// <remarks>No obtiene los hijos de los elementos de la lista</remarks>
		public static Expedients GetList() { return GetList(false); }
		public static Expedients GetList(bool childs)
		{
			return GetList(Expedients.SELECT(), childs);
		}
		public static Expedients GetList(HashOidList oidList, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				OidList = oidList.ToList()
			};

			return GetList(Expedient.SELECT(conditions, false), childs);
		}
		public static Expedients GetList(HashOidList oidList, bool childs, bool cache)
		{
			Expedients list;

			if (!Cache.Instance.Contains(typeof(Expedients)))
			{
				list = Expedients.GetList(oidList, childs);
				Cache.Instance.Save(typeof(Expedients), list);
			}
			else
				list = Cache.Instance.Get(typeof(Expedients)) as Expedients;

			return list;
		}

		public static Expedients GetList(string query, bool childs)
		{
			if (!Expedient.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;

			Expedient.BeginTransaction(criteria.SessionCode);

			//No criteria. Retrieve all de List
			return DataPortal.Fetch<Expedients>(criteria);
		}

        public static Expedients GetListByOidEnlaceStock(long oidEnlaceStock, bool childs = false, int sessionCode = -1)
        {
            return GetList(Expedient.SELECT_BY_STOCKS_ENLAZADOS(oidEnlaceStock), childs, sessionCode);
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
                    Expedient.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Expedient.GetChild(SessionCode, reader, Childs));
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
            foreach (Expedient obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Expedient obj in this)
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

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Expedient.SELECT(conditions, true); }

       /* public static string SELECT_FROM_LIST(string list) 
        {
            string query = ExpedienteInfo.SELECT(0) +
                            " WHERE E.\"OID\" IN (" + list + ")" +
                            " FOR UPDATE OF E NOWAIT;";

            return query;
        }*/

		#endregion
    }
}

