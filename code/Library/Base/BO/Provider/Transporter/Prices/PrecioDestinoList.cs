using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 

using moleQule;

using NHibernate;

namespace moleQule.Library.Store
{

	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class PrecioDestinoList : ReadOnlyListBaseEx<PrecioDestinoList, PrecioDestinoInfo>
    {
        #region Business Methods

        public PrecioDestinoInfo GetByPort(string puerto)
        {
            foreach (PrecioDestinoInfo precio in this)
            {
                if (precio.Puerto.Equals(puerto))
                    return precio;
            }
            return null;
        }

        public PrecioDestinoInfo GetByClientAndPort(string nombre, string puerto)
        {
            foreach (PrecioDestinoInfo precio in this)
            {
                if (precio.NombreCliente.Equals(nombre) && precio.Puerto.Equals(puerto))
                    return precio;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private PrecioDestinoList() { }		
		private PrecioDestinoList(IList<PrecioDestino> lista)
		{
            Fetch(lista);
        }
        private PrecioDestinoList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		public static PrecioDestinoList GetList() { return PrecioDestinoList.GetList(true); }
		public static PrecioDestinoList GetList(bool childs)
		{
			CriteriaEx criteria = PrecioDestino.GetCriteria(PrecioDestino.OpenSession());
            criteria.Childs = childs;			
			
			criteria.Query = PrecioDestinoList.SELECT();			

			PrecioDestinoList list = DataPortal.Fetch<PrecioDestinoList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
        /// Builds a PrecioDestinoList from a IList<!--<PrecioDestinoInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PrecioDestinoList</returns>
        public static PrecioDestinoList GetChildList(IList<PrecioDestinoInfo> list)
        {
            PrecioDestinoList flist = new PrecioDestinoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (PrecioDestinoInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static PrecioDestinoList GetChildList(IList<PrecioDestino> list) { return new PrecioDestinoList(list); }
        public static PrecioDestinoList GetChildList(int sessionCode, IDataReader reader) { return new PrecioDestinoList(sessionCode, reader); }
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<PrecioDestino> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (PrecioDestino item in lista)
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
                this.AddItem(PrecioDestinoInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(PrecioDestinoInfo.GetChild(SessionCode, reader, Childs));
					}

					IsReadOnly = true;
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
		public static string SELECT(QueryConditions conditions) { return PrecioDestino.SELECT(conditions, false); }
		public static string SELECT(TransporterInfo transportista) { return SELECT(new QueryConditions { Acreedor = transportista }); }

		#endregion	
	}
}

