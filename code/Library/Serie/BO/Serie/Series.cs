using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Serie
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Series : BusinessListBaseEx<Series, Serie>
    {
        #region Business Methods

        /// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Serie NewItem()
        {
            this.AddItem(Serie.NewChild());
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private Series() { }

        public static Series NewList() { return new Series(); }

        public static Series GetList()
        {
            CriteriaEx criteria = Serie.GetCriteria(Serie.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Series.SELECT();

            Serie.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Series>(criteria);
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
                    Serie.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(Serie.GetChild(reader));
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
            foreach (Serie obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Serie obj in this)
                {
                    if (obj.IsNew)
                        obj.Insert(this);
                    else
                        obj.Update(this);
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

        public static string SELECT() { return SELECT(new QueryConditions()) + " ORDER BY S.\"IDENTIFICADOR\""; }
        public static string SELECT(QueryConditions conditions) { return Serie.SELECT(conditions, true); }
        public static string SELECT(ETipoSerie tipo)
        {
            string query =
            SELECT(new QueryConditions { SerieType = tipo }) + @"
            ORDER BY S.""NOMBRE""";

            return query;
        }

        #endregion
    }
}