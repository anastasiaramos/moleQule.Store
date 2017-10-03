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
	public class PrecioTrayectoList : ReadOnlyListBaseEx<PrecioTrayectoList, PrecioTrayectoInfo>
    {

        #region Business Methods

        public PrecioTrayectoInfo GetByPorts(string origen, string destino)
        {
            foreach (PrecioTrayectoInfo precio in this)
            {
                if (precio.PuertoDestino.Equals(destino) &&
                    precio.PuertoOrigen.Equals(origen))
                    return precio;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private PrecioTrayectoList() { }
		
		private PrecioTrayectoList(IList<PrecioTrayecto> lista)
		{
            Fetch(lista);
        }

        private PrecioTrayectoList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a PrecioTrayectoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PrecioTrayectoList</returns>
		public static PrecioTrayectoList GetList(bool childs)
		{
			CriteriaEx criteria = PrecioTrayecto.GetCriteria(PrecioTrayecto.OpenSession());
            criteria.Childs = childs;			
			
			criteria.Query = SELECT();			

			PrecioTrayectoList list = DataPortal.Fetch<PrecioTrayectoList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a PrecioTrayectoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>PrecioTrayectoList</returns>
		public static PrecioTrayectoList GetList()
		{ 
			return PrecioTrayectoList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static PrecioTrayectoList GetList(CriteriaEx criteria)
        {
			return PrecioTrayectoList.RetrieveList(typeof(PrecioTrayecto), AppContext.ActiveSchema.SchemaCode, criteria);
        }
		
		/// <summary>
        /// Builds a PrecioTrayectoList from a IList<!--<PrecioTrayectoInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PrecioTrayectoList</returns>
        public static PrecioTrayectoList GetChildList(IList<PrecioTrayectoInfo> list)
        {
            PrecioTrayectoList flist = new PrecioTrayectoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (PrecioTrayectoInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a PrecioTrayectoList from IList<!--<PrecioTrayecto>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PrecioTrayectoList</returns>
        public static PrecioTrayectoList GetChildList(IList<PrecioTrayecto> list) { return new PrecioTrayectoList(list); }

        public static PrecioTrayectoList GetChildList(IDataReader reader) { return new PrecioTrayectoList(reader); }

		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<PrecioTrayecto> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (PrecioTrayecto item in lista)
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
                this.AddItem(PrecioTrayecto.GetChild(reader).GetInfo());

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
						this.AddItem(PrecioTrayectoInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (PrecioTrayecto item in list)
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
		public static string SELECT(QueryConditions conditions) { return PrecioTrayecto.SELECT(conditions, false); }
		public static string SELECT(NavieraInfo parent) { return SELECT(new QueryConditions { Naviera = parent }); }

		#endregion	
	}
}

