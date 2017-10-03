using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Maquinarias : BusinessListBaseEx<Maquinarias, Maquinaria>
    {

        #region Business Methods

        public Maquinaria NewItem(Expedient parent)
        {
            this.NewItem(Maquinaria.NewChild(parent));
            return this[Count - 1];
        }

        public override void Remove(long oid)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }

        public new void Remove(Maquinaria item)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }

        public void Remove(Expedient parent, Maquinaria item)
        {
            base.Remove(item);
            parent.Partidas.Remove(parent, item.OidPartida);
        }

		public Maquinaria GetItemByOidPartida(long oid)
		{
			foreach (Maquinaria item in this)
				if (item.OidPartida == oid)
					return item;
			return null;
		}

        #endregion

        #region Factory Methods

        private Maquinarias()
        {
            MarkAsChild();
        }

        private Maquinarias(IList<Maquinaria> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private Maquinarias(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static Maquinarias NewChildList() { return new Maquinarias(); }

        public static Maquinarias GetChildList(IList<Maquinaria> lista) { return new Maquinarias(lista); }

        public static Maquinarias GetChildList(IDataReader reader, bool childs) { return new Maquinarias(reader, childs); }

        public static Maquinarias GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        public static Maquinarias GetChildList(Expedient parent, bool childs)
        {
            CriteriaEx criteria = Maquinaria.GetCriteria(parent.SessionCode);
            criteria.Query = Maquinarias.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<Maquinarias>(criteria);
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
                    Maquinaria.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Maquinaria.GetChild(reader));
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

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Maquinaria> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Maquinaria item in lista)
                this.AddItem(Maquinaria.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Maquinaria.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

		
        internal void Update(Expedient parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Maquinaria obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Maquinaria obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }
		

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Maquinaria.SELECT(conditions, true); }
		public static string SELECT(Expedient source) { return Maquinaria.SELECT(new QueryConditions { Expedient = source.GetInfo() }, true); }

        #endregion
    }
}