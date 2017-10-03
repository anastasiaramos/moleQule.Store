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
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
    public class ComprasInfo : ReadOnlyBaseEx<ComprasInfo>
    {
        #region Attributes

        //Proveedor
        protected long _oid_proveedor;
        protected string _codigo_proveedor = string.Empty;
        protected string _proveedor = string.Empty;

        //Producto
        protected long _oid_producto;
        protected string _codigo_producto = string.Empty;
        protected string _producto = string.Empty;
        protected long _n_serie;

        //Producto_Expediente
        protected decimal _precio_compra;
        protected decimal _coste;
        protected decimal _beneficio;
       
        //Factura
        protected decimal _kilos;
        protected decimal _total;

        #endregion

        #region Propiedades

        public long OidProveedor { get { return _oid_proveedor; } }
        public string CodigoProveedor { get { return _codigo_proveedor; } }
        public string Proveedor { get { return _proveedor; } }

        public long OidProducto { get { return _oid_producto; } }
        public string CodigoProducto { get { return _codigo_producto; } }
        public string Producto { get { return _producto; } }
        public long NSerie { get { return _n_serie; } }

        public Decimal PrecioCompra { get { return _precio_compra; } }
        public Decimal Coste { get { return _coste; } }
        public Decimal Beneficio { get { return _beneficio; } }

        public Decimal Kilos { get { return _kilos; } }
        public Decimal Total { get { return _total; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            string oid = Format.DataReader.GetInt64(source, "OID_PROVEEDOR").ToString("00000") + Format.DataReader.GetInt64(source, "OID_PRODUCTO").ToString("00000");
            Oid = Convert.ToInt64(oid);

            _oid_proveedor = Format.DataReader.GetInt64(source, "OID_PROVEEDOR");
            _codigo_proveedor = Format.DataReader.GetString(source, "CODIGO_PROVEEDOR");
            _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");

            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            //_codigo_producto = Format.DataReader.GetString(source, "CODIGO_PRODUCTO");
            _producto = Format.DataReader.GetString(source, "PRODUCTO");

            _kilos = Format.DataReader.GetDecimal(source, "KILOS");
            _total = Format.DataReader.GetDecimal(source, "COMPRA_TOTAL");

            _precio_compra = Format.DataReader.GetDecimal(source, "PCD");
            _coste = Format.DataReader.GetDecimal(source, "COSTE_TOTAL");
            //_beneficio = Format.DataReader.GetDecimal(source, "BENEFICIO");

            //_p_beneficio = (_total - _coste) * 100 / _total;

        }

        #endregion
        
        #region Factory Methods

        protected ComprasInfo() { /* require use of factory methods */ }

        public static ComprasInfo Get(IDataReader reader)
        {
            ComprasInfo item = new ComprasInfo();
            item.CopyValues(reader);
            return item;
        }

        #endregion
    }
}



