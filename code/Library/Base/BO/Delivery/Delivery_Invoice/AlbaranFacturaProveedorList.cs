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
	public class AlbaranFacturaProveedorList : ReadOnlyListBaseEx<AlbaranFacturaProveedorList, AlbaranFacturaProveedorInfo>
    {
        #region Business Methods

        public AlbaranFacturaProveedorInfo GetItemByFactura(long oid_factura)
        {
            foreach (AlbaranFacturaProveedorInfo obj in this)
            {
                if (obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public AlbaranFacturaProveedorInfo GetItemByAlbaran(long oid_Albaran)
        {
            foreach (AlbaranFacturaProveedorInfo obj in this)
            {
                if (obj.OidAlbaran == oid_Albaran)
                    return obj;
            }
            return null;
        }

        /*public bool AlbaranExists(long oid)
        {
            foreach (AlbaranFacturaProveedorInfo obj in this)
                if (obj.OidAlbaran == oid)
                    return true;
            return false;
        }*/
        
        public AlbaranFacturaProveedorInfo GetItem(long oid_albaran, long oid_factura)
        {
            foreach (AlbaranFacturaProveedorInfo obj in this)
            {
                if (obj.OidAlbaran == oid_albaran && obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private AlbaranFacturaProveedorList() { }
		
		private AlbaranFacturaProveedorList(IList<AlbaranFacturaProveedor> lista)
		{
            Fetch(lista);
        }

        private AlbaranFacturaProveedorList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a AlbaranFacturaProveedorList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>AlbaranFacturaProveedorList</returns>
		public static AlbaranFacturaProveedorList GetList(bool childs)
		{
			CriteriaEx criteria = AlbaranFacturaProveedor.GetCriteria(AlbaranFacturaProveedor.OpenSession());
            criteria.Childs = childs;
			
			criteria.Query = SELECT();

			AlbaranFacturaProveedorList list = DataPortal.Fetch<AlbaranFacturaProveedorList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a AlbaranFacturaProveedorList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>AlbaranFacturaProveedorList</returns>
		public static AlbaranFacturaProveedorList GetList()
		{ 
			return AlbaranFacturaProveedorList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static AlbaranFacturaProveedorList GetList(CriteriaEx criteria)
        {
            return AlbaranFacturaProveedorList.RetrieveList(typeof(AlbaranFacturaProveedor), AppContext.ActiveSchema.Code, criteria);
        }
		
		/// <summary>
        /// Builds a AlbaranFacturaProveedorList from a IList<!--<AlbaranFacturaProveedorInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranFacturaProveedorList</returns>
        public static AlbaranFacturaProveedorList GetChildList(IList<AlbaranFacturaProveedorInfo> list)
        {
            AlbaranFacturaProveedorList flist = new AlbaranFacturaProveedorList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (AlbaranFacturaProveedorInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a AlbaranFacturaProveedorList from IList<!--<AlbaranFacturaProveedor>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranFacturaProveedorList</returns>
        public static AlbaranFacturaProveedorList GetChildList(IList<AlbaranFacturaProveedor> list) { return new AlbaranFacturaProveedorList(list); }

        public static AlbaranFacturaProveedorList GetChildList(IDataReader reader) { return new AlbaranFacturaProveedorList(reader); }
        		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<AlbaranFacturaProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (AlbaranFacturaProveedor item in lista)
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
                this.AddItem(AlbaranFacturaProveedor.GetChild(reader).GetInfo());

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
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
					{
						this.AddItem(AlbaranFacturaProveedorInfo.GetChild(reader,Childs));
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

        /// <summary>
        /// Construye el SELECT para traer todos los Albarans asociados a una factura
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT()
        {
            string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
            string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            
			string query;

            query = "SELECT *," +
                    "       A.\"CODIGO\" AS \"CODIGO_ALBARAN\"," +
                    "       F.\"CODIGO\" AS \"CODIGO_FACTURA\"," +
                    "       F.\"TOTAL\" AS \"IMPORTE\"" +
                    " FROM " + idi + " AS AF" +
                    " INNER JOIN " + id + " AS A ON AF.\"OID_ALBARAN\" = A.\"OID\"" +
                    " INNER JOIN " + ii + " AS F ON AF.\"OID_FACTURA\" = F.\"OID\"";

            return query;
        }

        /// <summary>
        /// Construye el SELECT para traer todos los Albarans asociados a una factura
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT_BY_FACTURA(long oid)
        {
            string query = SELECT();
            query += " WHERE AF.\"OID_FACTURA\" = " + oid.ToString() + ";";

            return query;
        }

        /// <summary>
        /// Construye el SELECT para traer todas las facturas asociadas a un Albaran.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT_BY_ALBARAN(long oid)
        {
            string query = SELECT();
            query += " WHERE AF.\"OID_ALBARAN\" = " + oid.ToString() + ";";

            return query;
        }

        #endregion
	
	}
}

