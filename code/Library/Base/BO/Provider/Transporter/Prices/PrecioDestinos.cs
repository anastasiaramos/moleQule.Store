using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 

using moleQule;

using NHibernate;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class PrecioDestinos : BusinessListBaseEx<PrecioDestinos, PrecioDestino>
    {
        #region Business Methods

        public PrecioDestino GetByClientAndPort(string nombre, string puerto)
        {
            foreach (PrecioDestino precio in this)
            {
                if (precio.NombreCliente.Equals(nombre) && precio.Puerto.Equals(puerto))
                    return precio;
            }
            return null;
        }

        public PrecioDestino NewItem(Transporter parent)
        {
            this.NewItem(PrecioDestino.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private PrecioDestinos()
        {
            MarkAsChild();
        }
        private PrecioDestinos(IList<PrecioDestino> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private PrecioDestinos(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static PrecioDestinos NewChildList() { return new PrecioDestinos(); }

        public static PrecioDestinos GetChildList(IList<PrecioDestino> lista) { return new PrecioDestinos(lista); }
		public static PrecioDestinos GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static PrecioDestinos GetChildList(int sessionCode, IDataReader reader, bool childs) { return new PrecioDestinos(sessionCode, reader, childs); }


        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<PrecioDestino> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (PrecioDestino item in lista)
                this.AddItem(PrecioDestino.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(PrecioDestino.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

		
        internal void Update(Transporter parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PrecioDestino obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (PrecioDestino obj in this)
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
		public static string SELECT(QueryConditions conditions) { return PrecioDestino.SELECT(conditions, true); }
		public static string SELECT(Transporter transportista) { return SELECT(new QueryConditions { Acreedor = transportista.GetInfo(false) }); }

		#endregion
    }
}
