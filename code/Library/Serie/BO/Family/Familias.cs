using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using NHibernate;

using moleQule;

namespace moleQule.Serie
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Familias : BusinessListBaseEx<Familias, Familia>
    {
        #region Business Methods

        /// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Familia NewItem()
        {
            this.AddItem(Familia.NewChild());
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private Familias() { }

        public static Familias NewList() { return new Familias(); }

        public static Familias GetList()
        {
            CriteriaEx criteria = Familia.GetCriteria(Familia.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Familias.SELECT();
            else
                criteria.AddOrder("Identificador", true);

            Familia.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Familias>(criteria);
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
                    Familia.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Familia.GetChild(SessionCode, reader));
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
            foreach (Familia obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Familia obj in this)
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

        public static string SELECT() 
        { 
            return Familia.SELECT(0) +
                " ORDER BY FM.\"NOMBRE\"";
        }

        #endregion
    }
}
