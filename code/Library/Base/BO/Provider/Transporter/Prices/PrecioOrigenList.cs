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

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class PrecioOrigenList : ReadOnlyListBaseEx<PrecioOrigenList, PrecioOrigenInfo>
	{
        #region Business Methods

        public PrecioOrigenInfo GetByProvAndPort(long oid_prov, string puerto)
        {
            foreach (PrecioOrigenInfo precio in this)
            {
                if (precio.OidProveedor == oid_prov && precio.Puerto.Equals(puerto))
                    return precio;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private PrecioOrigenList() { }		
		private PrecioOrigenList(IList<PrecioOrigen> lista)
		{
            Fetch(lista);
        }
        private PrecioOrigenList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static PrecioOrigenList GetList() { return PrecioOrigenList.GetList(true); }
		public static PrecioOrigenList GetList(bool childs)
		{
			CriteriaEx criteria = PrecioOrigen.GetCriteria(PrecioOrigen.OpenSession());
            criteria.Childs = childs;			
			
			criteria.Query = SELECT();			

			PrecioOrigenList list = DataPortal.Fetch<PrecioOrigenList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
        /// Builds a PrecioOrigenList from a IList<!--<PrecioOrigenInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PrecioOrigenList</returns>
        public static PrecioOrigenList GetChildList(IList<PrecioOrigenInfo> list)
        {
            PrecioOrigenList flist = new PrecioOrigenList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (PrecioOrigenInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static PrecioOrigenList GetChildList(IList<PrecioOrigen> list) { return new PrecioOrigenList(list); }
        public static PrecioOrigenList GetChildList(int sessionCode, IDataReader reader) { return new PrecioOrigenList(sessionCode, reader); }
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<PrecioOrigen> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (PrecioOrigen item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(PrecioOrigenInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
		// called to retrieve data from db
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
					{
						this.AddItem(PrecioOrigenInfo.GetChild(SessionCode, reader, Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (PrecioOrigen item in list)
							this.AddItem(item.GetInfo());

						IsReadOnly = true;
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion	

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return PrecioOrigen.SELECT(conditions, false); }
		public static string SELECT(TransporterInfo parent) { return SELECT(new QueryConditions { Acreedor = parent }); }

		#endregion	

	}
}

