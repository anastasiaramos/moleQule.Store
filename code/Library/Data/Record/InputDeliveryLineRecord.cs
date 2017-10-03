using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class InputDeliveryLineRecord : RecordBase
    {
        #region Attributes

        private long _oid_albaran;
        private long _oid_batch;
        private long _oid_expediente;
        private long _oid_producto;
        private long _oid_kit;
        private long _oid_impuesto;
        private string _codigo_expediente = string.Empty;
        private string _concepto = string.Empty;
        private bool _facturacion_bulto = false;
        private Decimal _cantidad_kilos;
        private Decimal _cantidad_bultos;
        private Decimal _p_igic;
        private Decimal _p_descuento;
        private Decimal _total;
        private Decimal _precio;
        private Decimal _subtotal;
        private Decimal _gastos;
        private long _oid_linea_pedido;
        private long _oid_almacen;
        private string _codigo_producto_proveedor = string.Empty;
        private Decimal _p_irpf;

        #endregion

        #region Properties

        public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
        public virtual long OidBatch { get { return _oid_batch; } set { _oid_batch = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
        public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
        public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
        public virtual string CodigoExpediente { get { return _codigo_expediente; } set { _codigo_expediente = value; } }
        public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
        public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
        public virtual Decimal CantidadKilos { get { return _cantidad_kilos; } set { _cantidad_kilos = value; } }
        public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
        public virtual Decimal PIgic { get { return _p_igic; } set { _p_igic = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
        public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
        public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
        public virtual long OidLineaPedido { get { return _oid_linea_pedido; } set { _oid_linea_pedido = value; } }
        public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
        public virtual string CodigoProductoProveedor { get { return _codigo_producto_proveedor; } set { _codigo_producto_proveedor = value; } }
        public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }

        #endregion

        #region Business Methods

        public InputDeliveryLineRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
            _oid_batch = Format.DataReader.GetInt64(source, "OID_BATCH");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            _oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
            _codigo_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
            _concepto = Format.DataReader.GetString(source, "CONCEPTO");
            _facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
            _cantidad_kilos = Format.DataReader.GetDecimal(source, "CANTIDAD");
            _cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
            _p_igic = Format.DataReader.GetDecimal(source, "P_IGIC");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");
            _subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
            _gastos = Format.DataReader.GetDecimal(source, "GASTOS");
            _oid_linea_pedido = Format.DataReader.GetInt64(source, "OID_LINEA_PEDIDO");
            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
            _codigo_producto_proveedor = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_PROVEEDOR");
            _p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");

        }

        public virtual void CopyValues(InputDeliveryLineRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_albaran = source.OidAlbaran;
            _oid_batch = source.OidBatch;
            _oid_expediente = source.OidExpediente;
            _oid_producto = source.OidProducto;
            _oid_kit = source.OidKit;
            _oid_impuesto = source.OidImpuesto;
            _codigo_expediente = source.CodigoExpediente;
            _concepto = source.Concepto;
            _facturacion_bulto = source.FacturacionBulto;
            _cantidad_kilos = source.CantidadKilos;
            _cantidad_bultos = source.CantidadBultos;
            _p_igic = source.PIgic;
            _p_descuento = source.PDescuento;
            _total = source.Total;
            _precio = source.Precio;
            _subtotal = source.Subtotal;
            _gastos = source.Gastos;
            _oid_linea_pedido = source.OidLineaPedido;
            _oid_almacen = source.OidAlmacen;
            _codigo_producto_proveedor = source.CodigoProductoProveedor;
            _p_irpf = source.PIrpf;
        }
        #endregion
    }
}