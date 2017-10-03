using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.WorkReport
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class WorkReports : BusinessListBaseEx<WorkReports, WorkReport>
    {
		#region Business Methods

		public void SetMaxSerial()
		{
			foreach (WorkReport item in this)
				if (item.Serial > _max_serial) MaxSerial = item.Serial;
		}
		
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public WorkReport NewItem()
        {
            this.AddItem(WorkReport.NewChild());
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
        private WorkReports() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static WorkReports NewList() 
		{ 	
			if (!WorkReport.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new WorkReports(); 
		}

		private static WorkReports GetList(string query, bool childs)
		{
			if (!WorkReport.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}		
		
		public static WorkReports GetList(bool childs = true) 
		{
			return GetList(WorkReports.SELECT(), childs);
		}
		public static WorkReports GetList(QueryConditions conditions, bool childs)
		{
			return GetList(WorkReports.SELECT(conditions), childs);
		}

		public static WorkReports GetByExpedientList(long oidExpedient, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expedient = ExpedientInfo.New(oidExpedient) };
			return GetList(SELECT(conditions), childs);
		}

        #endregion
		
		#region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

		private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    WorkReport.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(WorkReport.GetChild(SessionCode, reader, Childs));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (WorkReport obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (WorkReport obj in this)
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
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return WorkReport.SELECT(conditions, true); }
			
		#endregion
    }
}

