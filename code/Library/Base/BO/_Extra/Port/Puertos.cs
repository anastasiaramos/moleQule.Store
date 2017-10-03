using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using NHibernate;
using Csla;
using moleQule.CslaEx; 
using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Tabla auxiliar con hijos
    /// </summary>
    [Serializable()]
    public class Puertos : BusinessListBaseEx<Puertos, Puerto>
    {
        #region Business Methods

        /// <summary>
        /// Crea y añade un elemento a la lista
        /// </summary>
        /// <returns>Nuevo item</returns>
        public Puerto NewItem()
        {
            this.NewItem(Puerto.NewChild());
            return this[Count - 1];
        }

        public override void Remove(long oid)
        {
            Puerto obj = GetItem(oid);
            if (obj != null)
            {
                obj.PuertoDespachantes.Clear();
                base.Remove(oid);
            }
        }

        public bool ExistOtherItem(Puerto Puerto)
        {
            foreach (Puerto obj in this)
                if ((obj.Oid != Puerto.Oid) && (obj.Valor.Equals(Puerto.Valor)))
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

        private Puertos() { }

        public static Puertos NewList() { return new Puertos(); }

        public static Puertos GetList()
        {
            CriteriaEx criteria = Puerto.GetCriteria(Puerto.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            Puerto.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Puertos>(criteria);
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
                    Puerto.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(Puerto.GetChild(reader));
                    }
                }
                else
                {
                    IList list = criteria.List();

                    foreach (Puerto item in list)
                    {
                        //Bloqueamos todos los elementos de la lista
                        Session().Lock(Session().Get<PuertoRecord>(item.Oid), LockMode.UpgradeNoWait);
                        this.AddItem(Puerto.GetChild(item));
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
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Puerto obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Puerto obj in this)
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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Puerto));
			conditions.Orders = orders;
			return Puerto.SELECT(conditions, true);
		}

		#endregion
    }
}


