using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class InputDeliveries : BusinessListBaseEx<InputDeliveries, InputDelivery>
    {		
		#region Root Business Methods

		public InputDelivery NewItem()
        {
			this.AddItem(InputDelivery.NewChild());
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
        private InputDeliveries() { }

		#endregion		
		
		#region Root Factory Methods
		
        public static InputDeliveries NewList() 
		{ 	
			if (!InputDelivery.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			InputDeliveries list = new InputDeliveries();
            list.SessionCode = InputDelivery.OpenSession();
            list.BeginTransaction();

            return list;
		}

		public static InputDeliveries GetList(string query, bool childs)
		{
			if (!InputDelivery.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}

		public static InputDeliveries GetList(bool childs = false)
		{
			return GetList(SELECT(), childs);
		}
		public static InputDeliveries GetList(HashOidList oidList, bool childs, int sessionCode)
		{
			QueryConditions conditions = new QueryConditions
			{
				OidList = oidList.ToList()
			};

			return GetList(InputDelivery.SELECT(conditions), childs, sessionCode);
		}
		public static InputDeliveries GetList(HashOidList oidList, bool childs, bool cache, int sessionCode)
		{
			InputDeliveries list;

			if (!Cache.Instance.Contains(typeof(InputDeliveries)))
			{
				list = InputDeliveries.GetList(oidList, childs, sessionCode);
				Cache.Instance.Save(typeof(InputDeliveries), list);
			}
			else
				list = Cache.Instance.Get(typeof(InputDeliveries)) as InputDeliveries;

			return list;
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
                    InputDelivery.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(InputDelivery.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
            foreach (InputDelivery obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (InputDelivery obj in this)
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
				if (!SharedTransaction) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
		public static string SELECT(QueryConditions conditions) { return InputDelivery.SELECT(conditions); }

		#endregion
    }
}

