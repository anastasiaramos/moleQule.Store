using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;

using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class TipoGastos : BusinessListBaseEx<TipoGastos, TipoGasto>
    {		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public TipoGasto NewItem()
        {
            this.AddItem(TipoGasto.NewChild());
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
        private TipoGastos() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static TipoGastos NewList() 
		{ 	
			if (!TipoGasto.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TipoGastos(); 
		}

        public static TipoGastos GetList() { return GetList(true); }		
		public static TipoGastos GetList(bool childs) 
		{
			return GetList(childs, TipoGastos.SELECT());
		}
		public static TipoGastos GetList(QueryConditions conditions, bool childs)
		{
			return GetList(childs, TipoGastos.SELECT(conditions));
		}

		internal static TipoGastos GetList(bool childs, string query)
		{
            if (!TipoGasto.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = TipoGasto.GetCriteria(TipoGasto.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			TipoGasto.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<TipoGastos>(criteria);
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
                    TipoGasto.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(TipoGasto.GetChild(reader, Childs));
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
            foreach (TipoGasto obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (TipoGasto obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
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
					
        #region SQL

        public static string SELECT() { return TipoGasto.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return TipoGasto.SELECT(conditions, true); }
		public static string SELECT(TipoGasto parent) { return TipoGasto.SELECT(new QueryConditions{ TipoGasto = parent.GetInfo(false) }, true); }
				
		#endregion
    }
}

