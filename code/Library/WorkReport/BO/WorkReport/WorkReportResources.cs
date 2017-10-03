using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.WorkReport
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class WorkReportResources : BusinessListBaseEx<WorkReportResources, WorkReportResource>
    {
		#region Business Methods

		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public WorkReportResource NewItem(WorkReport parent)
		{
			this.NewItem(WorkReportResource.NewChild(parent));
			WorkReportResource item = this[Count - 1];
			return item;
		}
        public WorkReportResource NewItem(WorkReport parent, IWorkResource item)
        {
            this.AddItem(WorkReportResource.NewChild(parent, item));
            return this[Count - 1];
        }
		public WorkReportResource NewItem(WorkReport parent, WorkReportResourceInfo item)
		{
			this.NewItem(WorkReportResource.NewChild(parent, item));
			return this[Count - 1];
		}

		public WorkReportResource GetItem(IWorkResource item)
		{
			return this.FirstOrDefault<WorkReportResource>(x => (x.OidResource == item.Oid && x.EEntityType == item.EEntityType));
		}

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private WorkReportResources() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static WorkReportResources NewList() 
		{ 	
			if (!WorkReportResource.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new WorkReportResources(); 
		}

		private static WorkReportResources GetList(string query, bool childs)
		{
			if (!WorkReportResource.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}		
		
		public static WorkReportResources GetList(bool childs = true) 
		{
			return GetList(WorkReportResources.SELECT(), childs);
		}
		public static WorkReportResources GetList(QueryConditions conditions, bool childs)
		{
			return GetList(WorkReportResources.SELECT(conditions), childs);
		}
		
        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private WorkReportResources(IList<WorkReportResource> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private WorkReportResources(int sessionCode, IDataReader reader, bool childs)
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
        public static WorkReportResources NewChildList() 
        { 
            WorkReportResources list = new WorkReportResources(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static WorkReportResources GetChildList(IList<WorkReportResource> lista) { return new WorkReportResources(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static WorkReportResources GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new WorkReportResources(sessionCode, reader, childs); }		
		
		public static WorkReportResources GetChildList(WorkReport parent, bool childs)
		{
			CriteriaEx criteria = WorkReport.GetCriteria(parent.SessionCode);

			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<WorkReportResources>(criteria);
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
                    WorkReportResource.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(WorkReportResource.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
            foreach (WorkReportResource obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (WorkReportResource obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

                if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if ((!SharedTransaction) && (Transaction() != null)) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<WorkReportResource> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (WorkReportResource item in lista)
				this.AddItem(WorkReportResource.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(WorkReportResource.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }

		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(WorkReport parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;
				
				// update (thus deleting) any deleted child objects
				foreach (WorkReportResource obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (WorkReportResource obj in this)
				{	
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return WorkReportResource.SELECT(conditions, true); }
		
		public static string SELECT(WorkReport parent) { return WorkReportResource.SELECT(new QueryConditions{ WorkReport = parent.GetInfo(false) }, true); }
			
		#endregion
    }
}

