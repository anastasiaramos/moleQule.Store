using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class BatchRecord : RecordBase
    {
        #region Attributes

        private long _oid_expediente;
        private long _oid_producto;
        private string _tipo_mercancia = string.Empty;
        private Decimal _bultos_iniciales;
        private Decimal _kilos_iniciales;
        private Decimal _precio_venta_kilo;
        private Decimal _beneficio_kilo;
        private Decimal _coste_kilo;
        private DateTime _rea_fecha_cobro;
        private bool _rea_cobrada = false;
        private Decimal _ayuda_recibida_kilo;
        private long _oid_proveedor;
        private Decimal _precio_compra_kilo;
        private Decimal _precio_venta_bulto;
        private long _oid_batch;
        private long _oid_kit;
        private Decimal _proporcion;
        private DateTime _fecha_compra;
        private string _ubicacion = string.Empty;
        private string _observaciones = string.Empty;
        private Decimal _gasto_kilo;
        private long _serial;
        private string _codigo = string.Empty;
        private long _oid_almacen;
        private bool _ayuda = false;

        #endregion

        #region Properties

        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
        public virtual string TipoMercancia { get { return _tipo_mercancia; } set { _tipo_mercancia = value; } }
        public virtual Decimal BultosIniciales { get { return _bultos_iniciales; } set { _bultos_iniciales = value; } }
        public virtual Decimal KilosIniciales { get { return _kilos_iniciales; } set { _kilos_iniciales = value; } }
        public virtual Decimal PrecioVentaKilo { get { return _precio_venta_kilo; } set { _precio_venta_kilo = value; } }
        public virtual Decimal BeneficioKilo { get { return _beneficio_kilo; } set { _beneficio_kilo = value; } }
        public virtual Decimal CosteKilo { get { return _coste_kilo; } set { _coste_kilo = value; } }
        public virtual DateTime ReaFechaCobro { get { return _rea_fecha_cobro; } set { _rea_fecha_cobro = value; } }
        public virtual bool ReaCobrada { get { return _rea_cobrada; } set { _rea_cobrada = value; } }
        public virtual Decimal AyudaRecibidaKilo { get { return _ayuda_recibida_kilo; } set { _ayuda_recibida_kilo = value; } }
        public virtual long OidProveedor { get { return _oid_proveedor; } set { _oid_proveedor = value; } }
        public virtual Decimal PrecioCompraKilo { get { return _precio_compra_kilo; } set { _precio_compra_kilo = value; } }
        public virtual Decimal PrecioVentaBulto { get { return _precio_venta_bulto; } set { _precio_venta_bulto = value; } }
        public virtual long OidBatch { get { return _oid_batch; } set { _oid_batch = value; } }
        public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
        public virtual Decimal Proporcion { get { return _proporcion; } set { _proporcion = value; } }
        public virtual DateTime FechaCompra { get { return _fecha_compra; } set { _fecha_compra = value; } }
        public virtual string Ubicacion { get { return _ubicacion; } set { _ubicacion = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual Decimal GastoKilo { get { return _gasto_kilo; } set { _gasto_kilo = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
        public virtual bool Ayuda { get { return _ayuda; } set { _ayuda = value; } }

        #endregion

        #region Business Methods

        public BatchRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            _tipo_mercancia = Format.DataReader.GetString(source, "TIPO_MERCANCIA");
            _bultos_iniciales = Format.DataReader.GetDecimal(source, "BULTOS_INICIALES");
            _kilos_iniciales = Format.DataReader.GetDecimal(source, "KILOS_INICIALES");
            _precio_venta_kilo = Format.DataReader.GetDecimal(source, "PRECIO_VENTA_KILO");
            _beneficio_kilo = Format.DataReader.GetDecimal(source, "BENEFICIO_KILO");
            _coste_kilo = Format.DataReader.GetDecimal(source, "COSTE_KILO");
            _rea_fecha_cobro = Format.DataReader.GetDateTime(source, "REA_FECHA_COBRO");
            _rea_cobrada = Format.DataReader.GetBool(source, "REA_COBRADA");
            _ayuda_recibida_kilo = Format.DataReader.GetDecimal(source, "AYUDA_RECIBIDA_KILO");
            _oid_proveedor = Format.DataReader.GetInt64(source, "OID_PROVEEDOR");
            _precio_compra_kilo = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA_KILO");
            _precio_venta_bulto = Format.DataReader.GetDecimal(source, "PRECIO_VENTA_BULTO");
            _oid_batch = Format.DataReader.GetInt64(source, "OID_BATCH");
            _oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
            _proporcion = Format.DataReader.GetDecimal(source, "PROPORCION");
            _fecha_compra = Format.DataReader.GetDateTime(source, "FECHA_COMPRA");
            _ubicacion = Format.DataReader.GetString(source, "UBICACION");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _gasto_kilo = Format.DataReader.GetDecimal(source, "GASTO_KILO");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
            _ayuda = Format.DataReader.GetBool(source, "AYUDA");
        }

        public virtual void CopyValues(BatchRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_expediente = source.OidExpediente;
            _oid_producto = source.OidProducto;
            _tipo_mercancia = source.TipoMercancia;
            _bultos_iniciales = source.BultosIniciales;
            _kilos_iniciales = source.KilosIniciales;
            _precio_venta_kilo = source.PrecioVentaKilo;
            _beneficio_kilo = source.BeneficioKilo;
            _coste_kilo = source.CosteKilo;
            _rea_fecha_cobro = source.ReaFechaCobro;
            _rea_cobrada = source.ReaCobrada;
            _ayuda_recibida_kilo = source.AyudaRecibidaKilo;
            _oid_proveedor = source.OidProveedor;
            _precio_compra_kilo = source.PrecioCompraKilo;
            _precio_venta_bulto = source.PrecioVentaBulto;
            _oid_batch = source.OidBatch;
            _oid_kit = source.OidKit;
            _proporcion = source.Proporcion;
            _fecha_compra = source.FechaCompra;
            _ubicacion = source.Ubicacion;
            _observaciones = source.Observaciones;
            _gasto_kilo = source.GastoKilo;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _oid_almacen = source.OidAlmacen;
            _ayuda = source.Ayuda;
        }
        #endregion
    }
}