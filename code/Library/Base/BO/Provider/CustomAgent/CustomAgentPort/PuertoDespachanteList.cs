using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class PuertoDespachanteList : ReadOnlyListBaseEx<PuertoDespachanteList, PuertoDespachanteInfo>
    {
        #region Bussines Methods

        public bool ContainsPuerto(long oid)
        {
            foreach (PuertoDespachanteInfo obj in this)
                if (obj.OidPuerto == oid)
                    return true;

            return false;
        }

        public bool ContainsDespachante(long oid)
        {
            foreach (PuertoDespachanteInfo obj in this)
                if (obj.OidDespachante == oid)
                    return true;
            return false;
        }

        #endregion

        #region Factory Methods

        private PuertoDespachanteList() { }
		
		private PuertoDespachanteList(IList<PuertoDespachante> lista)
		{
            Fetch(lista);
        }
        private PuertoDespachanteList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a PuertoDespachanteList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PuertoDespachanteList</returns>
		public static PuertoDespachanteList GetList(bool childs)
		{
			CriteriaEx criteria = PuertoDespachante.GetCriteria(PuertoDespachante.OpenSession());
            criteria.Childs = childs;			
			
			criteria.Query = SELECT();			

			PuertoDespachanteList list = DataPortal.Fetch<PuertoDespachanteList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a PuertoDespachanteList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PuertoDespachanteList</returns>
		public static PuertoDespachanteList GetList()
		{ 
			return PuertoDespachanteList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static PuertoDespachanteList GetList(CriteriaEx criteria)
        {
            return PuertoDespachanteList.RetrieveList(typeof(PuertoDespachante), AppContext.ActiveSchema.SchemaCode, criteria);
        }
		
		/// <summary>
        /// Builds a PuertoDespachanteList from a IList<!--<PuertoDespachanteInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PuertoDespachanteList</returns>
        public static PuertoDespachanteList GetChildList(IList<PuertoDespachanteInfo> list)
        {
            PuertoDespachanteList flist = new PuertoDespachanteList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (PuertoDespachanteInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static PuertoDespachanteList GetChildList(IList<PuertoDespachante> list) { return new PuertoDespachanteList(list); }

        public static PuertoDespachanteList GetChildList(IDataReader reader) { return new PuertoDespachanteList(reader); }

        /// <summary>
        /// Devuelve una lista de despachantes asociados a un puerto
        /// </summary>
        /// <param name="oid">Oid del puerto</param>
        /// <returns>DespachanteList</returns>
        public static DespachanteList GetDespachanteList(long oidPuerto)
        {
            CriteriaEx criteria = Puerto.GetCriteria(Puerto.OpenSession());
            criteria.Childs = false;

			criteria.Query = PuertoDespachanteList.SELECT_BY_PUERTO(oidPuerto);

            DespachanteList list = DataPortal.Fetch<DespachanteList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static PuertoList GetPuertoList(long oidDespachante)
        {
            CriteriaEx criteria = Puerto.GetCriteria(Despachante.OpenSession());
            criteria.Childs = false;

			criteria.Query = PuertoDespachanteList.SELECT_BY_DESPACHANTE(oidDespachante);

            PuertoList list = DataPortal.Fetch<PuertoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<PuertoDespachante> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (PuertoDespachante item in lista)
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
                this.AddItem(PuertoDespachante.GetChild(reader).GetInfo());

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
						this.AddItem(PuertoDespachanteInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (PuertoDespachante item in list)
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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions) { return PuertoDespachante.SELECT(conditions, false); }
		internal static string SELECT(PuertoInfo source) { return SELECT(new QueryConditions() { Puerto = source }); }
		internal static string SELECT(IAcreedorInfo source) { return SELECT(new QueryConditions() { Acreedor = source }); }

		public static string SELECT_BY_PUERTO(long oid)
		{
            QueryConditions conditions = new QueryConditions() {Puerto = PuertoInfo.New()};

            conditions.Puerto.Oid = oid;

            return ProviderBaseInfo.SELECT(conditions, ETipoAcreedor.Despachante);
		}

		public static string SELECT_BY_DESPACHANTE(long oid)
		{
			string pd = nHManager.Instance.GetSQLTable(typeof(PuertoDespachanteRecord));
			string pt = nHManager.Instance.GetSQLTable(typeof(PuertoRecord));

			string query;

			query = "SELECT *" +
					" FROM " + pt + " AS PT" +
					" INNER JOIN " + pd + " AS PD ON PD.\"OID_PUERTO\" = PT.\"OID\"" +
					" WHERE PD.\"OID_DESPACHANTE\" = " + oid;

			return query;
		}


		#endregion
	}
}

