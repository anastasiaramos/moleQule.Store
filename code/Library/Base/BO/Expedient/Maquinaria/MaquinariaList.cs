using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{

	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class MaquinariaList : ReadOnlyListBaseEx<MaquinariaList, MaquinariaInfo>
	{		 
		 
		#region Factory Methods

		private MaquinariaList() {}
		
		private MaquinariaList(IList<Maquinaria> lista)
		{
            Fetch(lista);
        }

        private MaquinariaList(IDataReader reader)
		{
			Fetch(reader);
		}

		public static MaquinariaList GetList() { return MaquinariaList.GetList(true); }
		public static MaquinariaList GetList(bool childs)
		{
			CriteriaEx criteria = Maquinaria.GetCriteria(Maquinaria.OpenSession());
            criteria.Childs = childs;			
			
			criteria.Query = MaquinariaList.SELECT();			

			MaquinariaList list = DataPortal.Fetch<MaquinariaList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static MaquinariaList GetList(CriteriaEx criteria)
        {
            return MaquinariaList.RetrieveList(typeof(Maquinaria), AppContext.ActiveSchema.Code, criteria);
        }

		#endregion

		#region Child Factory Methods

        public static MaquinariaList GetChildList(IList<MaquinariaInfo> list)
        {
            MaquinariaList flist = new MaquinariaList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (MaquinariaInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static MaquinariaList GetChildList(IList<Maquinaria> list) { return new MaquinariaList(list); }
        public static MaquinariaList GetChildList(IDataReader reader) { return new MaquinariaList(reader); }
		public static MaquinariaList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = Maquinaria.GetCriteria(Maquinaria.OpenSession());
			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			MaquinariaList list = DataPortal.Fetch<MaquinariaList>(criteria);
			list.CloseSession();

			return list;
		}
		
		#endregion

		#region Root Data Access

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
						this.AddItem(MaquinariaInfo.Get(reader, Childs));

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

		#region Child Data Access

		// called to copy objects data from list
        private void Fetch(IList<Maquinaria> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Maquinaria item in lista)
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
                this.AddItem(Maquinaria.GetChild(reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Maquinaria.SELECT(conditions, false); }
		public static string SELECT(ExpedientInfo source) { return Maquinaria.SELECT(new QueryConditions { Expedient = source }, false); }

        #endregion
	}
}

