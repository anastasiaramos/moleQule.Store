using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.CslaEx; 
using NHibernate;

using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Root Collection
    /// </summary>
    [Serializable()]
    public class TipoGanados : BusinessListBaseEx<TipoGanados, TipoGanado>
    {
        #region Business Methods

        public TipoGanado NewItem()
        {
            this.NewItem(TipoGanado.NewChild());
            return this[Count - 1];
        }

        public bool ExistOtherItem(TipoGanado Tipoganado)
        {
            foreach (TipoGanado obj in this)
                if ((obj.Oid != Tipoganado.Oid) && (obj.Valor == Tipoganado.Valor))
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

        private TipoGanados() { }

        public static TipoGanados NewList() { return new TipoGanados(); }

        public static TipoGanados GetList()
        {
            CriteriaEx criteria = TipoGanado.GetCriteria(TipoGanado.OpenSession());

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT();

            TipoGanado.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<TipoGanados>(criteria);
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
                    TipoGanado.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(TipoGanado.GetChild(reader));
                    }
                }
                else
                {
                    IList list = criteria.List();

                    foreach (TipoGanado item in list)
                    {
                        //Bloqueamos todos los elementos de la lista
                        Session().Lock(Session().Get<TipoGanadoRecord>(item.Oid), LockMode.UpgradeNoWait);
                        this.AddItem(TipoGanado.GetChild(item));
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
            foreach (TipoGanado obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (TipoGanado obj in this)
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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoGanado));
			conditions.Orders = orders;
			return TipoGanado.SELECT(conditions, true);
		}

		#endregion
    }
}
