using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class InputDeliveryRecord : RecordBase
    {
        #region Attributes

        private long _oid_serie;
        private long _oid_acreedor;
        private long _tipo_acreedor;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private long _ano;
        private DateTime _fecha;
        private DateTime _fecha_registro;
        private long _forma_pago;
        private long _dias_pago;
        private long _medio_pago;
        private DateTime _prevision_pago;
        private Decimal _p_irpf;
        private Decimal _p_descuento;
        private Decimal _descuento;
        private Decimal _base_imponible;
        private Decimal _igic;
        private Decimal _total;
        private string _cuenta_bancaria = string.Empty;
        private bool _nota = false;
        private string _observaciones = string.Empty;
        private bool _contado = false;
        private bool _rectificativo = false;
        private long _oid_almacen;
        private long _oid_expediente;
        private long _oid_usuario;
        private Decimal _irpf;

        #endregion

        #region Properties

        public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
        public virtual long OidAcreedor { get { return _oid_acreedor; } set { _oid_acreedor = value; } }
        public virtual long TipoAcreedor { get { return _tipo_acreedor; } set { _tipo_acreedor = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual long Ano { get { return _ano; } set { _ano = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual DateTime FechaRegistro { get { return _fecha_registro; } set { _fecha_registro = value; } }
        public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
        public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
        public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
        public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
        public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
        public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
        public virtual Decimal Igic { get { return _igic; } set { _igic = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
        public virtual bool Nota { get { return _nota; } set { _nota = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual bool Contado { get { return _contado; } set { _contado = value; } }
        public virtual bool Rectificativo { get { return _rectificativo; } set { _rectificativo = value; } }
        public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual Decimal Irpf { get { return _irpf; } set { _irpf = value; } }

        #endregion

        #region Business Methods

        public InputDeliveryRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
            _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
            _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _ano = Format.DataReader.GetInt64(source, "ANO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _fecha_registro = Format.DataReader.GetDateTime(source, "FECHA_REGISTRO");
            _forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
            _dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
            _medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
            _prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
            _p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
            _base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
            _igic = Format.DataReader.GetDecimal(source, "IGIC");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
            _nota = Format.DataReader.GetBool(source, "NOTA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _contado = Format.DataReader.GetBool(source, "CONTADO");
            _rectificativo = Format.DataReader.GetBool(source, "RECTIFICATIVO");
            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _irpf = Format.DataReader.GetDecimal(source, "IRPF");

        }

        public virtual void CopyValues(InputDeliveryRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_serie = source.OidSerie;
            _oid_acreedor = source.OidAcreedor;
            _tipo_acreedor = source.TipoAcreedor;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _ano = source.Ano;
            _fecha = source.Fecha;
            _fecha_registro = source.FechaRegistro;
            _forma_pago = source.FormaPago;
            _dias_pago = source.DiasPago;
            _medio_pago = source.MedioPago;
            _prevision_pago = source.PrevisionPago;
            _p_irpf = source.PIrpf;
            _p_descuento = source.PDescuento;
            _descuento = source.Descuento;
            _base_imponible = source.BaseImponible;
            _igic = source.Igic;
            _total = source.Total;
            _cuenta_bancaria = source.CuentaBancaria;
            _nota = source.Nota;
            _observaciones = source.Observaciones;
            _contado = source.Contado;
            _rectificativo = source.Rectificativo;
            _oid_almacen = source.OidAlmacen;
            _oid_expediente = source.OidExpediente;
            _oid_usuario = source.OidUsuario;
            _irpf = source.Irpf;
        }
        #endregion
    }
}