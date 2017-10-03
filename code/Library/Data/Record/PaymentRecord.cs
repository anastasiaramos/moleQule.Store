using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class PaymentRecord : RecordBase
    {
        #region Attributes

        private long _oid_agente;
        private long _oid_tarjeta_credito;
        private long _oid_cuenta_bancaria;
        private long _estado;
        private long _serial;
        private string _codigo = string.Empty;
        private long _tipo;
        private long _tipo_agente;
        private long _id_pago;
        private DateTime _fecha;
        private Decimal _importe;
        private long _medio_pago;
        private DateTime _vencimiento;
        private Decimal _gastos_bancarios;
        private string _id_mov_contable = string.Empty;
        private string _observaciones = string.Empty;
        private long _oid_usuario;
        private long _oid_root;
        private long _oid_link;
        private long _estado_pago;

        #endregion

        #region Properties

        public virtual long OidAgente { get { return _oid_agente; } set { _oid_agente = value; } }
        public virtual long OidTarjetaCredito { get { return _oid_tarjeta_credito; } set { _oid_tarjeta_credito = value; } }
        public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual long TipoAgente { get { return _tipo_agente; } set { _tipo_agente = value; } }
        public virtual long IdPago { get { return _id_pago; } set { _id_pago = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
        public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
        public virtual DateTime Vencimiento { get { return _vencimiento; } set { _vencimiento = value; } }
        public virtual Decimal GastosBancarios { get { return _gastos_bancarios; } set { _gastos_bancarios = value; } }
        public virtual string IdMovContable { get { return _id_mov_contable; } set { _id_mov_contable = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual long OidRoot { get { return _oid_root; } set { _oid_root = value; } }
        public virtual long OidLink { get { return _oid_link; } set { _oid_link = value; } }
        public virtual long EstadoPago { get { return _estado_pago; } set { _estado_pago = value; } }

        #endregion

        #region Business Methods

        public PaymentRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_agente = Format.DataReader.GetInt64(source, "OID_AGENTE");
            _oid_tarjeta_credito = Format.DataReader.GetInt64(source, "OID_TARJETA_CREDITO");
            _oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _tipo = Format.DataReader.GetInt64(source, "TIPO");
            _tipo_agente = Format.DataReader.GetInt64(source, "TIPO_AGENTE");
            _id_pago = Format.DataReader.GetInt64(source, "ID_PAGO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _importe = Format.DataReader.GetDecimal(source, "IMPORTE");
            _medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
            _vencimiento = Format.DataReader.GetDateTime(source, "VENCIMIENTO");
            _gastos_bancarios = Format.DataReader.GetDecimal(source, "GASTOS_BANCARIOS");
            _id_mov_contable = Format.DataReader.GetString(source, "ID_MOV_CONTABLE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _oid_usuario = Format.DataReader.GetInt32(source, "OID_USUARIO");
            _oid_root = Format.DataReader.GetInt64(source, "OID_ROOT");
            _oid_link = Format.DataReader.GetInt64(source, "OID_LINK");
            _estado_pago = Format.DataReader.GetInt64(source, "ESTADO_PAGO");
        }
        public virtual void CopyValues(PaymentRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_agente = source.OidAgente;
            _oid_tarjeta_credito = source.OidTarjetaCredito;
            _oid_cuenta_bancaria = source.OidCuentaBancaria;
            _estado = source.Estado;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _tipo = source.Tipo;
            _tipo_agente = source.TipoAgente;
            _id_pago = source.IdPago;
            _fecha = source.Fecha;
            _importe = source.Importe;
            _medio_pago = source.MedioPago;
            _vencimiento = source.Vencimiento;
            _gastos_bancarios = source.GastosBancarios;
            _id_mov_contable = source.IdMovContable;
            _observaciones = source.Observaciones;
            _oid_usuario = source.OidUsuario;
            _oid_root = source.OidRoot;
            _oid_link = source.OidLink;
            _estado_pago = source.EstadoPago;
        }

        #endregion
    }
}