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
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class InputDeliveryLineBase
	{
		#region Attributes

        private InputDeliveryLineRecord _record = new InputDeliveryLineRecord();

		protected string _store = string.Empty;
        protected string _store_id = string.Empty;
		internal string _expediente = string.Empty;
		internal Decimal _ayuda_kilo;
		internal string _ubicacion = string.Empty;
		internal long _oid_stock;
		internal long _oid_pedido;
		internal string _id_batch = string.Empty;

		#endregion

		#region Properties

        public InputDeliveryLineRecord Record { get { return _record; } set { _record = value; } }

		//Campos no enlazados
        internal virtual string Almacen { get { return _store; } set { _store = value; } }
        internal virtual string IDAlmacen { get { return _store_id; } set { _store_id = value; } }
        internal virtual string IDAlmacenAlmacen { get { return (_record.OidAlmacen != 0) ? _store_id + " - " + _store : string.Empty; } }
		internal virtual bool IsKitComponent { get { return _record.OidKit > 0; } }
		internal virtual Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
		internal virtual Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		internal virtual Decimal Impuestos { get { return Decimal.Round((BaseImponible * _record.PIgic) / 100, 4); } }
		internal virtual Decimal AyudaKilo { get { return Decimal.Round(_ayuda_kilo, 5); } set { _ayuda_kilo = Decimal.Round(value, 5); } }
		internal virtual Decimal Beneficio { get { return _record.CantidadKilos * BeneficioKilo; } }
		internal virtual Decimal BeneficioKilo
		{
			get
			{
				if (_record.FacturacionBulto)
					return (_record.CantidadKilos > 0) ? (_record.Precio / (_record.CantidadKilos / _record.CantidadBultos)) - _record.Gastos : 0;
				else
					return _record.Precio - _record.Gastos;
			}
		}
		internal virtual bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		internal ETipoFacturacion ETipoFacturacion 
        { 
            get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; }
            set
            {
                switch (value)
                {
                    case ETipoFacturacion.Peso: _record.FacturacionBulto = false; break;
                    case ETipoFacturacion.Unidad: _record.FacturacionBulto = true; break;
                    case ETipoFacturacion.Unitaria: _record.FacturacionBulto = true; break;
                }
            }
        }
        internal string SaleMethodLabel { get { return moleQule.Common.Structs.EnumText<ETipoFacturacion>.GetLabel(ETipoFacturacion); } }
        internal virtual Decimal IRPF { get { return Decimal.Round((BaseImponible * _record.PIrpf) / 100, 4); } }
		internal virtual string IDBatch { get { return _id_batch; } set { _id_batch = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_store = Format.DataReader.GetString(source, "ALMACEN");
            _store_id = Format.DataReader.GetString(source, "STORE_ID");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
			_oid_stock = Format.DataReader.GetInt64(source, "OID_STOCK");
			_oid_pedido = Format.DataReader.GetInt64(source, "OID_PEDIDO");
			_id_batch = Format.DataReader.GetString(source, "ID_BATCH");
		}
		internal void CopyValues(InputDeliveryLine source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_store = source.Almacen;
            _store_id = source.IDAlmacen;
			_expediente = source.Expediente;
			_ayuda_kilo = source.AyudaKilo;
			_oid_stock = source.OidStock;
			_oid_pedido = source.OidPedido;
			_id_batch = source.IDBatch;
		}
		internal void CopyValues(InputDeliveryLineInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source._base.Record);

			_store = source.Almacen;
            _store_id = source.IDAlmacen;
			_expediente = source.Expediente;
			_ayuda_kilo = source.AyudaKilo;
			_oid_stock = source.OidStock;
			_oid_pedido = source.OidPedido;
			_id_batch = source.IDBatch;
		}

		#endregion
	}

    [Serializable()]
    public class InputDeliveryLineSQL : SQLBuilder
    {
        #region FIELDS

        internal static string SELECT_FIELDS(QueryConditions conditions)
        {
            string query = string.Empty;

            switch (conditions.CategoriaGasto)
            {
                case ECategoriaGasto.Expediente:
                    query = "SELECT MAX(CA.\"OID\") AS \"OID\"" +
                            " , CA.\"OID_ALBARAN\"" +
                            " , MAX(CA.\"OID_BATCH\") AS \"OID_BATCH\"" +
                            " , COALESCE(BA.\"CODIGO\", '') AS \"ID_BATCH\"" +
                            " , MAX(CA.\"OID_EXPEDIENTE\") AS \"OID_EXPEDIENTE\"" +
                            " , MAX(CA.\"OID_PRODUCTO\") AS \"OID_PRODUCTO\"" +
                            " , MAX(CA.\"OID_KIT\") AS \"OID_KIT\"" +
                            " , MAX(CA.\"CODIGO_EXPEDIENTE\") AS \"CODIGO_EXPEDIENTE\"" +
                            " , MAX(CA.\"CONCEPTO\") AS \"CONCEPTO\"" +
                            " , FALSE AS \"FACTURACION_BULTO\"" +
                            " , MAX(CA.\"CANTIDAD\") AS \"CANTIDAD\"" +
                            " , MAX(CA.\"CANTIDAD_BULTOS\") AS \"CANTIDAD_BULTOS\"" +
                            " , MAX(CA.\"P_IGIC\") AS \"P_IGIC\"" +
                            " , MAX(CA.\"P_DESCUENTO\") AS \"P_DESCUENTO\"" +
                            " , MAX(CA.\"TOTAL\") AS \"TOTAL\"" +
                            " , MAX(CA.\"PRECIO\") AS \"PRECIO\"" +
                            " , MAX(CA.\"SUBTOTAL\") AS \"SUBTOTAL\"" +
                            " , MAX(CA.\"GASTOS\") AS \"GASTOS\"" +
                            " , MAX(CA.\"OID_IMPUESTO\") AS \"OID_IMPUESTO\"" +
                            " , MAX(CA.\"OID_LINEA_PEDIDO\") AS \"OID_LINEA_PEDIDO\"" +
                            " , MAX(CA.\"OID_ALMACEN\") AS \"OID_ALMACEN\"" +
                            " , MAX(CA.\"CODIGO_PRODUCTO_PROVEEDOR\") AS \"CODIGO_PRODUCTO_PROVEEDOR\" " +
                            " , MAX(COALESCE(ST.\"OID\", 0)) AS \"OID_STOCK\" " +
                            " , MAX(COALESCE(AL.\"CODIGO\", '')) AS \"STORE_ID\"" +
                            " , MAX(COALESCE(AL.\"NOMBRE\", '')) AS \"ALMACEN\"" +
                            " , MAX(COALESCE(LP.\"OID_PEDIDO\", 0)) AS \"OID_PEDIDO\" " +
                            " , COALESCE(GA.\"CODIGO\", '') AS \"EXPEDIENTE\" " +
                            " , MAX(CA.\"P_IRPF\") AS \"P_IRPF\"";
                    break;
                default:
                    query = "SELECT CA.*" +
                            "       ,COALESCE(ST.\"OID\", 0) AS \"OID_STOCK\"" +
                            "       ,COALESCE(AL.\"CODIGO\", '') AS \"STORE_ID\"" +
                            "       ,COALESCE(AL.\"NOMBRE\", '') AS \"ALMACEN\"" +
                            "       ,COALESCE(BA.\"CODIGO\", '') AS \"ID_BATCH\"" +
                            "       ,COALESCE(LP.\"OID_PEDIDO\", 0) AS \"OID_PEDIDO\"" +
                            "       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"";
                    break;
            }

            return query;
        }

        #endregion

        #region SELECT

        internal static string SELECT_BASE(Library.Store.QueryConditions conditions)
        {
            string ca = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string lp = nHManager.Instance.GetSQLTable(typeof(InputOrderLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
            string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
            string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

            string query;

            query = SELECT_FIELDS(conditions) +
                    " FROM " + ca + " AS CA" +
                    " LEFT JOIN " + st + " AS ST ON (ST.\"OID_CONCEPTO_ALBARAN\" = CA.\"OID\" AND ST.\"TIPO\" = " + (long)ETipoStock.Compra + ")" +
                    " LEFT JOIN " + al + " AS AL ON AL.\"OID\" = CA.\"OID_ALMACEN\"" +
                    " LEFT JOIN " + ba + " AS BA ON BA.\"OID\" = CA.\"OID_BATCH\"" +
                    " LEFT JOIN " + lp + " AS LP ON LP.\"OID\" = CA.\"OID_LINEA_PEDIDO\"";

            switch (conditions.CategoriaGasto)
            {
                case ECategoriaGasto.Expediente:
                    query += " LEFT JOIN (  SELECT DISTINCT CA.\"OID\", EX.\"CODIGO\"" +
                             "              FROM " + ca + " AS CA" +
                             "              INNER JOIN " + id + " AS AP ON AP.\"OID\" = CA.\"OID_ALBARAN\"" +
                             "              LEFT JOIN " + idi + " AS AF ON AP.\"OID\" = AF.\"OID_ALBARAN\"" +
                             "              LEFT JOIN " + ii + " AS FP ON FP.\"OID\" = AF.\"OID_FACTURA\"" +
                             "              LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = CASE WHEN CA.\"OID_EXPEDIENTE\" != 0 THEN CA.\"OID_EXPEDIENTE\" ELSE (CASE WHEN AP.\"OID_EXPEDIENTE\" != 0 THEN AP.\"OID_EXPEDIENTE\" ELSE FP.\"OID_EXPEDIENTE\" END) END" +
                             "              WHERE EX.\"OID\" != 0) AS GA ON GA.\"OID\" = CA.\"OID\"";
                    break;

                default:
                    query += " LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = CA.\"OID_EXPEDIENTE\"";
                    break;
            }

            return query;
        }

        internal static string SELECT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions { ConceptoAlbaranProveedor = InputDeliveryLineInfo.New(oid) };
            return SELECT(conditions, lockTable);
        }

        internal static string SELECT(Library.Store.QueryConditions conditions, bool lockTable)
        {
            string query =
            SELECT_BASE(conditions) +
            WHERE(conditions);

            query += Common.EntityBase.LOCK("CA", lockTable);

            return query;
        }

        internal static string SELECT_STOCK(Library.Store.QueryConditions conditions, bool lockTable)
        {
            string query =
            SELECT_BASE(conditions) +
            WHERE(conditions) + @"
                AND CA.""OID_ALMACEN"" != 0";

            query += Common.EntityBase.LOCK("CA", lockTable);

            return query;
        }

        #endregion

        #region WHERE

        internal static string WHERE(Library.Store.QueryConditions conditions)
        {
            if (conditions == null) return string.Empty;

            string query = string.Empty;

            query += @"
            WHERE TRUE";

            if (conditions.ConceptoAlbaranProveedor != null) 
                query += @"
                    AND CA.""OID"" = " + conditions.ConceptoAlbaranProveedor.Oid;

            if (conditions.InputDelivery != null) 
                query += @" 
                    AND CA.""OID_ALBARAN"" = " + conditions.InputDelivery.Oid;

            if (conditions.Producto != null)
                query += @"
                    AND CA.""OID_PRODUCTO"" = " + conditions.Producto.Oid;

            if (conditions.Expedient != null) 
                query += @"
                    AND CA.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;

            if (conditions.Almacen != null) 
                query += @"
                    AND CA.""OID_ALMACEN"" = " + conditions.Almacen.Oid;

            return query + " " + conditions.ExtraWhere;
        }

        #endregion
    }
}