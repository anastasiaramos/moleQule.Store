using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class SerieFamilias : BusinessListBaseEx<SerieFamilias, SerieFamilia>
    {
        #region Business Methods

        public SerieFamilia NewItem(Serie parent, FamiliaInfo familia)
        {
            if (GetItemByFamilia(familia.Oid) != null) return null;            

            this.NewItem(SerieFamilia.NewChild(parent));

            SerieFamilia newItem = this[Count - 1];

            newItem.OidFamilia = familia.Oid;
            newItem.Familia = familia.Nombre;

            return newItem;
        }

        public SerieFamilia GetItemByFamilia(long oid_familia)
        {
            foreach (SerieFamilia item in this)
                if (item.OidFamilia == oid_familia)
                    return item;

            return null;
        }

        #endregion

        #region Factory Methods

        private SerieFamilias()
        {
            MarkAsChild();
        }

        private SerieFamilias(IList<SerieFamilia> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private SerieFamilias(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static SerieFamilias NewChildList() { return new SerieFamilias(); }

        public static SerieFamilias GetChildList(IList<SerieFamilia> lista) { return new SerieFamilias(lista); }
        public static SerieFamilias GetChildList(int sessionCode, IDataReader reader, bool childs) { return new SerieFamilias(sessionCode, reader, childs); }
		public static SerieFamilias GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<SerieFamilia> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (SerieFamilia item in lista)
                this.AddItem(SerieFamilia.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(SerieFamilia.GetChild(SessionCode, reader, true));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(Serie parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (SerieFamilia obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (SerieFamilia obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region SQL

        public static string SELECT() { return SerieFamilia.SELECT(0, true); }
        public static string SELECT(Library.Store.QueryConditions conditions) { return SerieFamilia.SELECT(conditions, true); }
        public static string SELECT(Serie item) { return SELECT(new Library.Store.QueryConditions { Serie = item.GetInfo() }); }
        public static string SELECT(Familia item) { return SELECT(new Library.Store.QueryConditions { Familia = item.GetInfo() }); }

        #endregion
    }
}
