using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class PuertoDespachantes : BusinessListBaseEx<PuertoDespachantes, PuertoDespachante>
    {
        #region Business Methods
		
        public PuertoDespachante NewItem(Despachante parent)
        {
            this.NewItem(PuertoDespachante.NewChild(parent));
            return this[Count - 1];
        }
		
        public PuertoDespachante NewItem(Puerto parent)
        {
            this.NewItem(PuertoDespachante.NewChild(parent));
            return this[Count - 1];
        }

        public PuertoDespachante NewItem(DespachanteInfo parent)
        {
            this.NewItem(PuertoDespachante.NewChild(parent));
            return this[Count - 1];
        }

        public PuertoDespachante NewItem(PuertoInfo parent)
        {
            this.NewItem(PuertoDespachante.NewChild(parent));
            return this[Count - 1];
        }

        public void RemovePuerto(long oid)
        {
            foreach (PuertoDespachante obj in this)
            {
                if (obj.OidPuerto == oid)
                {
                    Remove(obj);
                    break;
                }
            }
        }

        public void RemoveDespachante(long oid)
        {
            foreach (PuertoDespachante obj in this)
            {
                if (obj.OidDespachante == oid)
                {
                    Remove(obj);
                    break;
                }
            }
        }

        public bool ContainsPuerto(long oid)
        {
            foreach (PuertoDespachante obj in this)
                if (obj.OidPuerto == oid)
                    return true;

            return false;
        }

        public bool ContainsDespachante(long oid)
        {
            foreach (PuertoDespachante obj in this)
                if (obj.OidDespachante == oid)
                    return true;
            return false;
        }

        #endregion

        #region Factory Methods

        private PuertoDespachantes()
        {
            MarkAsChild();
        }
        private PuertoDespachantes(IList<PuertoDespachante> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private PuertoDespachantes(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static PuertoDespachantes NewChildList() { return new PuertoDespachantes(); }

        public static PuertoDespachantes GetChildList(IList<PuertoDespachante> lista) { return new PuertoDespachantes(lista); }

        public static PuertoDespachantes GetChildList(IDataReader reader, bool childs) { return new PuertoDespachantes(reader, childs); }

        public static PuertoDespachantes GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<PuertoDespachante> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (PuertoDespachante item in lista)
                this.AddItem(PuertoDespachante.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(PuertoDespachante.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

		
        internal void Update(Despachante parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PuertoDespachante obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (PuertoDespachante obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Puerto parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PuertoDespachante obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (PuertoDespachante obj in this)
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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions) { return PuertoDespachante.SELECT(conditions, true); }
		internal static string SELECT(Puerto source) { return SELECT(new QueryConditions() { Puerto = source.GetInfo(false) }); }
		internal static string SELECT(IAcreedor source) { return SELECT(new QueryConditions() { Acreedor = source.IGetInfo() }); }

		#endregion
	}
}
