using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class ProductRecord : RecordBase
    {
        #region Attributes

        private long _oid_ayuda;
        private long _oid_familia;
        private long _oid_impuesto_compra;
        private long _oid_impuesto_venta;
        private long _serial;
        private string _codigo = string.Empty;
		private string _external_code = string.Empty;
        private bool _bulto = false;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private Decimal _precio_compra;
        private Decimal _precio_venta;
        private Decimal _ayuda_kilo;
        private string _cuenta_contable_compra = string.Empty;
        private string _cuenta_contable_venta = string.Empty;
        private string _codigo_aduanero = string.Empty;
        private string _observaciones = string.Empty;
        private bool _unitario = false;
        private long _estado;
        private Decimal _kilos_bulto;
        private Decimal _stock_minimo;
        private long _tipo_venta;
        private bool _avisar_stock = false;
        private bool _beneficio_cero = false;
        private bool _avisar_beneficio_minimo = false;
        private Decimal _p_beneficio_minimo;
		private bool _is_kit;
        private bool _no_stock_sale;

        #endregion

        #region Properties

        public virtual long OidAyuda { get { return _oid_ayuda; } set { _oid_ayuda = value; } }
        public virtual long OidFamilia { get { return _oid_familia; } set { _oid_familia = value; } }
        public virtual long OidImpuestoCompra { get { return _oid_impuesto_compra; } set { _oid_impuesto_compra = value; } }
        public virtual long OidImpuestoVenta { get { return _oid_impuesto_venta; } set { _oid_impuesto_venta = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual string ExternalCode { get { return _external_code; } set { _external_code = value; } }
        public virtual bool Bulto { get { return _bulto; } set { _bulto = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual Decimal PrecioCompra { get { return _precio_compra; } set { _precio_compra = value; } }
        public virtual Decimal PrecioVenta { get { return _precio_venta; } set { _precio_venta = value; } }
        public virtual Decimal AyudaKilo { get { return _ayuda_kilo; } set { _ayuda_kilo = value; } }
        public virtual string CuentaContableCompra { get { return _cuenta_contable_compra; } set { _cuenta_contable_compra = value; } }
        public virtual string CuentaContableVenta { get { return _cuenta_contable_venta; } set { _cuenta_contable_venta = value; } }
        public virtual string CodigoAduanero { get { return _codigo_aduanero; } set { _codigo_aduanero = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual bool Unitario { get { return _unitario; } set { _unitario = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual Decimal KilosBulto { get { return _kilos_bulto; } set { _kilos_bulto = value; } }
        public virtual Decimal StockMinimo { get { return _stock_minimo; } set { _stock_minimo = value; } }
        public virtual long TipoVenta { get { return _tipo_venta; } set { _tipo_venta = value; } }
        public virtual bool AvisarStock { get { return _avisar_stock; } set { _avisar_stock = value; } }
        public virtual bool BeneficioCero { get { return _beneficio_cero; } set { _beneficio_cero = value; } }
        public virtual bool AvisarBeneficioMinimo { get { return _avisar_beneficio_minimo; } set { _avisar_beneficio_minimo = value; } }
        public virtual Decimal PBeneficioMinimo { get { return _p_beneficio_minimo; } set { _p_beneficio_minimo = value; } }
		public virtual bool IsKit { get { return _is_kit; } set { _is_kit = value; } }
        public virtual bool NoStockSale { get { return _no_stock_sale; } set { _no_stock_sale = value; } }

        #endregion

        #region Business Methods

        public ProductRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_ayuda = Format.DataReader.GetInt64(source, "OID_AYUDA");
            _oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");
            _oid_impuesto_compra = Format.DataReader.GetInt64(source, "OID_IMPUESTO_COMPRA");
            _oid_impuesto_venta = Format.DataReader.GetInt64(source, "OID_IMPUESTO_VENTA");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
			_external_code = Format.DataReader.GetString(source, "EXTERNAL_CODE");
            _bulto = Format.DataReader.GetBool(source, "BULTO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
            _precio_venta = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
            _ayuda_kilo = Format.DataReader.GetDecimal(source, "AYUDA_KILO");
            _cuenta_contable_compra = Format.DataReader.GetString(source, "CUENTA_CONTABLE_COMPRA");
            _cuenta_contable_venta = Format.DataReader.GetString(source, "CUENTA_CONTABLE_VENTA");
            _codigo_aduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _unitario = Format.DataReader.GetBool(source, "UNITARIO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _kilos_bulto = Format.DataReader.GetDecimal(source, "KILOS_BULTO");
            _stock_minimo = Format.DataReader.GetDecimal(source, "STOCK_MINIMO");
            _tipo_venta = Format.DataReader.GetInt64(source, "TIPO_VENTA");
            _avisar_stock = Format.DataReader.GetBool(source, "AVISAR_STOCK");
            _beneficio_cero = Format.DataReader.GetBool(source, "BENEFICIO_CERO");
            _avisar_beneficio_minimo = Format.DataReader.GetBool(source, "AVISAR_BENEFICIO_MINIMO");
            _p_beneficio_minimo = Format.DataReader.GetDecimal(source, "P_BENEFICIO_MINIMO");
            _is_kit = Format.DataReader.GetBool(source, "IS_KIT");
			_no_stock_sale = Format.DataReader.GetBool(source, "NO_STOCK_SALE");
        }
        public virtual void CopyValues(ProductRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_ayuda = source.OidAyuda;
            _oid_familia = source.OidFamilia;
            _oid_impuesto_compra = source.OidImpuestoCompra;
            _oid_impuesto_venta = source.OidImpuestoVenta;
            _serial = source.Serial;
            _codigo = source.Codigo;
			_external_code = source.ExternalCode;
            _bulto = source.Bulto;
            _nombre = source.Nombre;
            _descripcion = source.Descripcion;
            _precio_compra = source.PrecioCompra;
            _precio_venta = source.PrecioVenta;
            _ayuda_kilo = source.AyudaKilo;
            _cuenta_contable_compra = source.CuentaContableCompra;
            _cuenta_contable_venta = source.CuentaContableVenta;
            _codigo_aduanero = source.CodigoAduanero;
            _observaciones = source.Observaciones;
            _unitario = source.Unitario;
            _estado = source.Estado;
            _kilos_bulto = source.KilosBulto;
            _stock_minimo = source.StockMinimo;
            _tipo_venta = source.TipoVenta;
            _avisar_stock = source.AvisarStock;
            _beneficio_cero = source.BeneficioCero;
            _avisar_beneficio_minimo = source.AvisarBeneficioMinimo;
            _p_beneficio_minimo = source.PBeneficioMinimo;
			_is_kit = source.IsKit;
            _no_stock_sale = source.NoStockSale;
        }

        #endregion
    }
}