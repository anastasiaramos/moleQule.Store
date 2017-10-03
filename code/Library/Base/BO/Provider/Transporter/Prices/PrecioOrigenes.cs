using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class PrecioOrigenes : BusinessListBaseEx<PrecioOrigenes, PrecioOrigen>
    {
        #region Business Methods

        public PrecioOrigen GetByProvAndPort(long oid_prov, string puerto)
        {
            foreach (PrecioOrigen precio in this)
            {
                if (precio.OidProveedor == oid_prov && precio.Puerto.Equals(puerto))
                    return precio;
            }
            return null;
        }
		
        public PrecioOrigen NewItem(Transporter parent)
        {
            this.NewItem(PrecioOrigen.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private PrecioOrigenes()
        {
            MarkAsChild();
        }
        private PrecioOrigenes(IList<PrecioOrigen> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private PrecioOrigenes(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static PrecioOrigenes NewChildList() { return new PrecioOrigenes(); }

        public static PrecioOrigenes GetChildList(IList<PrecioOrigen> lista) { return new PrecioOrigenes(lista); }

		public static PrecioOrigenes GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static PrecioOrigenes GetChildList(int sessionCode, IDataReader reader, bool childs) { return new PrecioOrigenes(sessionCode, reader, childs); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<PrecioOrigen> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (PrecioOrigen item in lista)
                this.AddItem(PrecioOrigen.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(PrecioOrigen.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

		
        internal void Update(Transporter parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PrecioOrigen obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (PrecioOrigen obj in this)
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
		public static string SELECT(QueryConditions conditions) { return PrecioOrigen.SELECT(conditions, true); }
		public static string SELECT(Transporter parent) { return SELECT(new QueryConditions { Acreedor = parent.GetInfo(false) }); }

		#endregion	
    }
}
