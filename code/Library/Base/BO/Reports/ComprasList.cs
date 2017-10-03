using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ComprasList : ReadOnlyListBaseEx<ComprasList, ComprasInfo>
    {

        #region Business Methods

        #endregion

        #region Factory Methods

        private ComprasList() { }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
		public static ComprasList GetListByProveedor(QueryConditions conditions)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
            criteria.Childs = false;

            criteria.Query = ComprasList.SELECT_BY_PROVEEDOR(conditions);

            ComprasList list = DataPortal.Fetch<ComprasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static ComprasList GetListByProducto(QueryConditions conditions)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
            criteria.Childs = false;

            criteria.Query = ComprasList.SELECT_BY_PRODUCTO(conditions);

            ComprasList list = DataPortal.Fetch<ComprasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Data Access

        // called to retrieve data from database
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
                        this.AddItem(ComprasInfo.Get(reader));
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

        public static string SELECT(QueryConditions conditions)
        {
			string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
			string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
			string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
			string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

            //LOS PRODUCTOS QUE HAN SIDO COMPRADOS CON CONTROL DE STOCK
            string query = @"
            SELECT SU.""OID"" AS ""OID_PROVEEDOR""
                    ,SU.""CODIGO"" AS ""CODIGO_PROVEEDOR""
                    ,SU.""NOMBRE"" AS ""PROVEEDOR""
                    ,PR.""OID"" AS ""OID_PRODUCTO""
                    ,PR.""NOMBRE"" AS ""PRODUCTO""
                    ,SUM(BA.""KILOS_INICIALES"") AS ""KILOS""
                    ,AVG(BA.""PRECIO_COMPRA_KILO"") AS ""PCD""
                    ,SUM(BA.""PRECIO_COMPRA_KILO"" * BA.""KILOS_INICIALES"") AS ""COMPRA_TOTAL""
                    ,SUM(BA.""COSTE_KILO"" * BA.""KILOS_INICIALES"") AS ""COSTE_TOTAL""
            FROM " + ba + @" AS BA
            LEFT JOIN " + idl + @" AS CAP ON CAP.""OID_BATCH"" = BA.""OID""
            LEFT JOIN " + id + @" AS AP ON AP.""OID"" = CAP.""OID_ALBARAN""
            INNER JOIN " + su + @" AS SU ON BA.""OID_PROVEEDOR"" = SU.""OID""
            INNER JOIN " + ex + @" AS E ON E.""OID"" = BA.""OID_EXPEDIENTE""
            INNER JOIN " + pr + @" AS PR ON PR.""OID"" = BA.""OID_PRODUCTO""
            WHERE (BA.""FECHA_COMPRA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"')";

			if (conditions.Proveedor != null)
				query += @" 
                AND SU.""OID"" = " + conditions.Proveedor.Oid;

			if (conditions.Expedient != null)
				query += @" 
                AND E.""OID"" = " + conditions.Expedient.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += @" 
                AND E.""TIPO_EXPEDIENTE"" = " + (long)conditions.TipoExpediente;

			if (conditions.Producto != null)
				query += @" 
                AND PR.""OID"" = " + conditions.Producto.Oid;

			//LOS PRODUCTOS QUE HAN SIDO COMPRADOS
			/*query += " UNION" +
					" SELECT '0' AS \"OID_PROVEEDOR\"" +
					"		,'VARIOS' AS \"CODIGO_PROVEEDOR\"" +
					"		,'VARIOS' AS \"PROVEEDOR\"" +
					"       ,PR.\"OID\" AS \"OID_PRODUCTO\"" +
					"		,PR.\"NOMBRE\" AS \"PRODUCTO\"" +
					"       ,SUM(BA.\"KILOS_INICIALES\") AS \"KILOS\"" +
					"       ,AVG(CAP.\"PRECIO\") AS \"PCD\"" +
					"       ,SUM(CAP.\"TOTAL\") AS \"COMPRA_TOTAL\"" +
					"       ,SUM(0) AS \"COSTE_TOTAL\"" +
					" FROM " + pr + " AS PR" +
					" INNER JOIN " + cap + " AS CAP ON CAP.\"OID_PRODUCTO\" = PR.\"OID\"" +
					" INNER JOIN " + ap + " AS AP ON AP.\"OID\" = CAP.\"OID_ALBARAN\"" +
					" INNER JOIN " + afp + " AS AFP ON AFP.\"OID_ALBARAN\" = AP.\"OID\"" +
					" INNER JOIN " + fp + " AS FP ON FP.\"OID\" = AFP.\"OID_FACTURA\"" +
					" WHERE (FP.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.Producto != null)
				query += " AND PR.\"OID\" = " + conditions.Producto.Oid;*/

			query += @"
            GROUP BY SU.""OID"", SU.""CODIGO"", SU.""NOMBRE"", PR.""OID"", PR.""NOMBRE""";

            return query;
        }

        public static string SELECT_BY_PROVEEDOR(QueryConditions conditions)
        {
            string query =
            SELECT(conditions);

            query += @"
            ORDER BY SU.""NOMBRE"", PR.""NOMBRE""";

            return query;
        }

        public static string SELECT_BY_PRODUCTO(QueryConditions conditions)
        {
            string query = 
            SELECT(conditions);

			query += @"
            ORDER BY PR.""NOMBRE"", SU.""NOMBRE""";

            return query;
        }

        #endregion
    }
}