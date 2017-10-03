using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class TipoAnimales : BusinessListBaseEx<TipoAnimales, TipoAnimal>
    {
        #region Business Methods

        public TipoAnimal NewItem()
        {
            this.NewItem(TipoAnimal.NewChild());
            return this[Count - 1];
        }

        public bool ExistOtherItem(TipoAnimal TipoAnimal)
        {
            foreach (TipoAnimal obj in this)
                if ((obj.Oid != TipoAnimal.Oid) && (obj.Valor == TipoAnimal.Valor))
                    return true;
            return false;
        }

        #endregion

        #region Authorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);
        }

        #endregion

        #region Factory Methods

        private TipoAnimales() { }

        public static TipoAnimales NewList() { return new TipoAnimales(); }

        public static TipoAnimales GetList()
        {
            CriteriaEx criteria = TipoAnimal.GetCriteria(TipoAnimal.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT();

            TipoAnimal.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<TipoAnimales>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    TipoAnimal.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(TipoAnimal.GetChild(reader));
                    }
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

        protected override void DataPortal_Update()
        {
            bool success = false;

            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (TipoAnimal obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (TipoAnimal obj in this)
                {
                    if (!ExistOtherItem(obj))
                    {
                        if (obj.IsNew)
                            obj.Insert(this);
                        else
                            obj.Update(this);
                    }
                }

                Transaction().Commit();

                success = true;
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!success)
                    if (Transaction() != null) Transaction().Rollback();

                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region SQL

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoAnimal));
			conditions.Orders = orders;
			return TipoAnimal.SELECT(conditions, true);
		}

		#endregion
	}
}
