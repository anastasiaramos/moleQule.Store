using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;

using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class AlbaranFacturasProveedores : BusinessListBaseEx<AlbaranFacturasProveedores, AlbaranFacturaProveedor>
    {

        #region Business Methods

        public AlbaranFacturaProveedor NewItem(InputInvoice factura, InputDeliveryInfo AlbaranProveedor)
        {
            this.AddItem(AlbaranFacturaProveedor.NewChild(factura, AlbaranProveedor));
			factura.SetAlbaranes();
            return this[Count - 1];
        }

        public bool AlbaranProveedorExists(long oid)
        {
            foreach (AlbaranFacturaProveedor obj in this)
                if (obj.OidAlbaran == oid)
                    return true;

            return false;
        }

        public AlbaranFacturaProveedor GetItemByFactura(long oid_factura)
        {
            foreach (AlbaranFacturaProveedor obj in this)
            {
                if (obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public bool GetItem(long oid_factura, long oid_albaran)
        {
            foreach (AlbaranFacturaProveedor obj in this)
            {
                if (obj.OidFactura == oid_factura && obj.OidAlbaran == oid_albaran)
                    return true;
            }
            return false;
        }

        public AlbaranFacturaProveedor GetItemByAlbaran(long oid_albaran)
        {
            foreach (AlbaranFacturaProveedor obj in this)
            {
                if (obj.OidAlbaran == oid_albaran)
                    return obj;
            }
            return null;
        }

        public void Remove(InputInvoice factura, InputDeliveryInfo AlbaranProveedor)
        {
            foreach (AlbaranFacturaProveedor item in this)
                if (item.OidFactura == factura.Oid && item.OidAlbaran == AlbaranProveedor.Oid)
                {
                    this.Remove(item);
                    break;
                }

			factura.SetAlbaranes();
        }

        #endregion

        #region Factory Methods

        private AlbaranFacturasProveedores()
        {
            MarkAsChild();
        }

        private AlbaranFacturasProveedores(IList<AlbaranFacturaProveedor> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private AlbaranFacturasProveedores(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static AlbaranFacturasProveedores NewChildList() { return new AlbaranFacturasProveedores(); }

        public static AlbaranFacturasProveedores GetChildList(IList<AlbaranFacturaProveedor> lista) { return new AlbaranFacturasProveedores(lista); }

        public static AlbaranFacturasProveedores GetChildList(IDataReader reader, bool childs) { return new AlbaranFacturasProveedores(reader, childs); }

        public static AlbaranFacturasProveedores GetChildList(IDataReader reader) { return GetChildList(reader, true); }

		public static AlbaranFacturasProveedores GetChildList(InputInvoice parent, bool childs)
		{
			CriteriaEx criteria = AlbaranFacturaProveedor.GetCriteria(parent.SessionCode);

			criteria.Query = AlbaranFacturasProveedores.SELECT_BY_FACTURA(parent.Oid);
			criteria.Childs = childs;

			return DataPortal.Fetch<AlbaranFacturasProveedores>(criteria);
		}

        #endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			Fetch(criteria);
		}

		private void Fetch(CriteriaEx criteria)
		{
			try
			{
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					AlbaranFacturaProveedor.DoLOCK(Session());
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						AlbaranFacturaProveedor obj = AlbaranFacturaProveedor.GetChild(reader);
						this.AddItem(obj);
					}
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}

		#endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<AlbaranFacturaProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (AlbaranFacturaProveedor item in lista)
                this.AddItem(AlbaranFacturaProveedor.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(AlbaranFacturaProveedor.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(InputDelivery parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (AlbaranFacturaProveedor obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (AlbaranFacturaProveedor obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        internal void Update(InputInvoice parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (AlbaranFacturaProveedor obj in DeletedList)
            {
                if (!GetItem(obj.OidFactura, obj.OidAlbaran))
                    obj.DeleteSelf(parent);
            }

            // add/update any current child objects
            foreach (AlbaranFacturaProveedor obj in this)
            {
                bool existe = false;

                if (obj.IsNew)
                {
                    //Si el albarán se ha eliminado y se ha vuelto a insertar no hay que volver a guardarlo
                    foreach (AlbaranFacturaProveedor albaran in DeletedList)
                    {
                        if (albaran.OidAlbaran == obj.OidAlbaran)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe)
                        continue;
                    obj.Insert(parent);
                }
                else
                    obj.Update(parent);

            }

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        /// <summary>
        /// Construye el SELECT para traer todos los AlbaranProveedors asociados a una factura
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
        /// Construye el SELECT para traer todos los AlbaranProveedors asociados a una factura
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
        /// Construye el SELECT para traer todas las facturas asociadas a un AlbaranProveedor.
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
