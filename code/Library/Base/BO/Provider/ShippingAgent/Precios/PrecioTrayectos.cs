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
    public class PrecioTrayectos : BusinessListBaseEx<PrecioTrayectos, PrecioTrayecto>
    {
        #region Business Methods

        public PrecioTrayecto GetByPorts(string origen, string destino)
        {
            foreach (PrecioTrayecto precio in this)
            {
                if (precio.PuertoDestino.Equals(destino) &&
                    precio.PuertoOrigen.Equals(origen))
                    return precio;
            }
            return null;
        }
		
        public PrecioTrayecto NewItem(Naviera parent)
        {
            this.NewItem(PrecioTrayecto.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private PrecioTrayectos()
        {
            MarkAsChild();
        }

        private PrecioTrayectos(IList<PrecioTrayecto> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private PrecioTrayectos(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }


        public static PrecioTrayectos NewChildList() { return new PrecioTrayectos(); }

        public static PrecioTrayectos GetChildList(IList<PrecioTrayecto> lista) { return new PrecioTrayectos(lista); }

        public static PrecioTrayectos GetChildList(IDataReader reader, bool childs) { return new PrecioTrayectos(reader, childs); }

        public static PrecioTrayectos GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<PrecioTrayecto> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (PrecioTrayecto item in lista)
                this.AddItem(PrecioTrayecto.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(PrecioTrayecto.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

		
        internal void Update(Naviera parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PrecioTrayecto obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (PrecioTrayecto obj in this)
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
		public static string SELECT(QueryConditions conditions) { return PrecioTrayecto.SELECT(conditions, true); }
		public static string SELECT(Naviera parent) { return SELECT(new QueryConditions { Naviera = parent.GetInfo(false) }); }

		#endregion

    }
}
