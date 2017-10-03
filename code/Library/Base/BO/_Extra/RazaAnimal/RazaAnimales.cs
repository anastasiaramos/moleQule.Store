using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using moleQule;
using NHibernate;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class RazaAnimales : BusinessListBaseEx<RazaAnimales, RazaAnimal>
    {
        #region Business Methods

        public RazaAnimal NewItem()
        {
            this.NewItem(RazaAnimal.NewChild());
            return this[Count - 1];
        }

        public bool ExistOtherItem(RazaAnimal RazaAnimal)
        {
            foreach (RazaAnimal obj in this)
                if ((obj.Oid != RazaAnimal.Oid) && (obj.Valor == RazaAnimal.Valor))
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

        private RazaAnimales() { }

        public static RazaAnimales NewList() { return new RazaAnimales(); }

        public static RazaAnimales GetList()
        {
            CriteriaEx criteria = RazaAnimal.GetCriteria(RazaAnimal.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            RazaAnimal.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<RazaAnimales>(criteria);
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
                    RazaAnimal.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(RazaAnimal.GetChild(reader));
                    }
                }
                else
                {
                    IList list = criteria.List();

                    foreach (RazaAnimal item in list)
                    {
                        //Bloqueamos todos los elementos de la lista
                        Session().Lock(Session().Get<RazaAnimalRecord>(item.Oid), LockMode.UpgradeNoWait);
                        this.AddItem(RazaAnimal.GetChild(item));
                    }
                }

            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
            foreach (RazaAnimal obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (RazaAnimal obj in this)
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
                    if (!SharedTransaction && Transaction() != null) Transaction().Rollback();

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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(RazaAnimal));
			conditions.Orders = orders;
			return RazaAnimal.SELECT(conditions, true);
		}

		#endregion

    }
}
