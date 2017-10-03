using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Cabezas : BusinessListBaseEx<Cabezas, Cabeza>
    {

        #region Business Methods

        public Cabeza NewItem(Expedient parent)
        {
            this.NewItem(Cabeza.NewChild(parent));
            return this[Count - 1];
        }

        public override void Remove(long oid)
        {
            throw new iQException(Library.Resources.Messages.REMOVE_NOT_ALLOWED);
        }

        public new void Remove(Cabeza item)
        {
            throw new iQException(Library.Resources.Messages.REMOVE_NOT_ALLOWED);
        }

        public void Remove(Expedient parent, Cabeza item)
        {
            base.Remove(item);
            parent.Partidas.Remove(parent, item.OidPartida);
        }

		public Cabeza GetItemByOidPartida(long oid)
		{
			foreach (Cabeza item in this)
				if (item.OidPartida == oid)
					return item;
			return null;
		}

        #endregion

        #region Factory Methods

        private Cabezas()
        {
            MarkAsChild();
        }

        private Cabezas(IList<Cabeza> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private Cabezas(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static Cabezas NewChildList() { return new Cabezas(); }

        public static Cabezas GetChildList(IList<Cabeza> lista) { return new Cabezas(lista); }

        public static Cabezas GetChildList(IDataReader reader, bool childs) { return new Cabezas(reader, childs); }

        public static Cabezas GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        public static Cabezas GetChildList(Expedient parent, bool childs)
        {
            CriteriaEx criteria = Cabeza.GetCriteria(parent.SessionCode);
            criteria.Query = Cabezas.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<Cabezas>(criteria);
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
                    Cabeza.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Cabeza.GetChild(reader));
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
        private void Fetch(IList<Cabeza> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Cabeza item in lista)
                this.AddItem(Cabeza.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Cabeza.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }
        		
        internal void Update(Expedient parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Cabeza obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Cabeza obj in this)
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
        public static string SELECT(QueryConditions conditions) { return Cabeza.SELECT(conditions, true); }
		public static string SELECT(Expedient source) { return Cabeza.SELECT(new QueryConditions { Expedient = source.GetInfo() }, true); }

        #endregion

    }
}