using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.CslaEx; 
using NHibernate;

using moleQule;

namespace moleQule.Serie
{
    /// <summary>
    /// Read Only Child Collection of Business Objects
    /// </summary>
    [Serializable()]
    public class SerieFamiliaList : ReadOnlyListBaseEx<SerieFamiliaList, SerieFamiliaInfo>
    {
        #region Bussines Methods

		public SerieFamiliaInfo GetItemByFamilia(long oidFamilia)
		{
			try { return Items.Single(item => item.OidFamilia == oidFamilia); }
			catch { return null; }
		}

        #endregion

        #region Factory Methods

        private SerieFamiliaList() { }

        private SerieFamiliaList(IList<SerieFamilia> lista)
        {
            Fetch(lista);
        }

        private SerieFamiliaList(int sessionCode, IDataReader reader, bool childs)
        {
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static SerieFamiliaList GetList() { return SerieFamiliaList.GetList(true); }
        public static SerieFamiliaList GetList(bool childs)
        {
            CriteriaEx criteria = SerieFamilia.GetCriteria(SerieFamilia.OpenSession());
            criteria.Childs = childs;

            criteria.Query = SerieFamiliaList.SELECT();

            SerieFamiliaList list = DataPortal.Fetch<SerieFamiliaList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static SerieFamiliaList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SerieFamiliaList.SELECT(conditions), childs);
		}
		private static SerieFamiliaList GetList(string query, bool childs)
		{
			CriteriaEx criteria = SerieFamilia.GetCriteria(SerieFamilia.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			SerieFamiliaList list = DataPortal.Fetch<SerieFamiliaList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static SerieFamiliaList GetList(CriteriaEx criteria)
        {
            return SerieFamiliaList.RetrieveList(typeof(SerieFamilia), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Builds a ProductoClienteList from a IList<!--<ProductoClienteInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ProductoClienteList</returns>
        public static SerieFamiliaList GetChildList(IList<SerieFamiliaInfo> list)
        {
            SerieFamiliaList flist = new SerieFamiliaList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (SerieFamiliaInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static SerieFamiliaList GetChildList(IList<SerieFamilia> list) { return new SerieFamiliaList(list); }
        public static SerieFamiliaList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new SerieFamiliaList(sessionCode, reader, childs); }
		public static SerieFamiliaList GetChildList(SerieInfo parent, bool childs)
		{
			CriteriaEx criteria = SerieFamilia.GetCriteria(SerieFamilia.OpenSession());

			criteria.Query = SerieFamiliaList.SELECT(parent);
			criteria.Childs = childs;

			SerieFamiliaList list = DataPortal.Fetch<SerieFamiliaList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

        #endregion

        #region Data Access

        // called to copy objects data from list
        private void Fetch(IList<SerieFamilia> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (SerieFamilia item in lista)
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
                this.AddItem(SerieFamiliaInfo.GetChild(SessionCode, reader, true));

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
                        this.AddItem(SerieFamiliaInfo.GetChild(SessionCode, reader, Childs));

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
        public static string SELECT(QueryConditions conditions) { return SerieFamilia.SELECT(conditions, false); }
        public static string SELECT(SerieInfo item) { return SELECT(new QueryConditions { Serie = item }); }
        public static string SELECT(FamiliaInfo item) { return SELECT(new QueryConditions { Family = item }); }

        #endregion
    }
}

