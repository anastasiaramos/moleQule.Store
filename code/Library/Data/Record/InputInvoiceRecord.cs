using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Store.Data
{
    [Serializable()]
    public class InputInvoiceRecord : RecordBase
    {
        #region Attributes

        private long _oid_serie;
        private long _oid_acreedor;
        private long _tipo_acreedor;
        private long _serial;
        private string _codigo = string.Empty;
        private string _n_factura = string.Empty;
        private string _vat_number = string.Empty;
        private string _acreedor = string.Empty;
        private string _direccion = string.Empty;
        private string _codigo_postal = string.Empty;
        private string _provincia = string.Empty;
        private string _municipio = string.Empty;
        private long _ano;
        private DateTime _fecha;
        private DateTime _fecha_registro;
        private long _forma_pago;
        private long _dias_pago;
        private long _medio_pago;
        private DateTime _prevision_pago;
        private Decimal _base_imponible;
        private Decimal _p_irpf;
        private Decimal _p_igic;
        private Decimal _p_descuento;
        private Decimal _descuento;
        private Decimal _total;
        private string _cuenta_bancaria = string.Empty;
        private bool _nota = false;
        private string _observaciones = string.Empty;
        private bool _albaran = false;
        private bool _rectificativa = false;
        private long _estado;
        private string _id_mov_contable = string.Empty;
        private string _albaranes = string.Empty;
        private long _oid_usuario;
        private long _oid_expediente;
        private Decimal _irpf;
        private Decimal _impuestos;

        #endregion

        #region Properties

        public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
        public virtual long OidAcreedor { get { return _oid_acreedor; } set { _oid_acreedor = value; } }
        public virtual long TipoAcreedor { get { return _tipo_acreedor; } set { _tipo_acreedor = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string NFactura { get { return _n_factura; } set { _n_factura = value; } }
        public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
        public virtual string Acreedor { get { return _acreedor; } set { _acreedor = value; } }
        public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
        public virtual string CodigoPostal { get { return _codigo_postal; } set { _codigo_postal = value; } }
        public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
        public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
        public virtual long Ano { get { return _ano; } set { _ano = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual DateTime FechaRegistro { get { return _fecha_registro; } set { _fecha_registro = value; } }
        public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
        public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
        public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
        public virtual DateTime Prevision { get { return _prevision_pago; } set { _prevision_pago = value; } }
        public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
        public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }
        public virtual Decimal PIgic { get { return _p_igic; } set { _p_igic = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual Decimal Descuento { get { return Decimal.Round(_descuento, 2); } set { _descuento = value; } }
        public virtual Decimal Total { get { return Decimal.Round(_total, 2); } set { _total = value; } }
        public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
        public virtual bool Nota { get { return _nota; } set { _nota = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual bool Albaran { get { return _albaran; } set { _albaran = value; } }
        public virtual bool Rectificativa { get { return _rectificativa; } set { _rectificativa = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string IdMovContable { get { return _id_mov_contable; } set { _id_mov_contable = value; } }
        public virtual string Albaranes { get { return _albaranes; } set { _albaranes = value; } }
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual Decimal Irpf { get { return _irpf; } set { _irpf = value; } }
        public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }

        #endregion

        #region Business Methods

        public InputInvoiceRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
            _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
            _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _n_factura = Format.DataReader.GetString(source, "N_FACTURA");
            _vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
            _acreedor = Format.DataReader.GetString(source, "EMISOR");
            _direccion = Format.DataReader.GetString(source, "DIRECCION");
            _codigo_postal = Format.DataReader.GetString(source, "CODIGO_POSTAL");
            _provincia = Format.DataReader.GetString(source, "PROVINCIA");
            _municipio = Format.DataReader.GetString(source, "MUNICIPIO");
            _ano = Format.DataReader.GetInt64(source, "ANO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _fecha_registro = Format.DataReader.GetDateTime(source, "FECHA_REGISTRO");
            _forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
            _dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
            _medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
            _prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
            _base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
            _p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
            _p_igic = Format.DataReader.GetDecimal(source, "P_IGIC");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
            _nota = Format.DataReader.GetBool(source, "NOTA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _albaran = Format.DataReader.GetBool(source, "ALBARAN");
            _rectificativa = Format.DataReader.GetBool(source, "RECTIFICATIVA");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _id_mov_contable = Format.DataReader.GetString(source, "ID_MOV_CONTABLE");
            _albaranes = Format.DataReader.GetString(source, "ALBARANES");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _irpf = Format.DataReader.GetDecimal(source, "IRPF");
            _impuestos = Format.DataReader.GetDecimal(source, "IMPUESTOS");

        }
        public virtual void CopyValues(InputInvoiceRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_serie = source.OidSerie;
            _oid_acreedor = source.OidAcreedor;
            _tipo_acreedor = source.TipoAcreedor;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _n_factura = source.NFactura;
            _vat_number = source.VatNumber;
            _acreedor = source.Acreedor;
            _direccion = source.Direccion;
            _codigo_postal = source.CodigoPostal;
            _provincia = source.Provincia;
            _municipio = source.Municipio;
            _ano = source.Ano;
            _fecha = source.Fecha;
            _fecha_registro = source.FechaRegistro;
            _forma_pago = source.FormaPago;
            _dias_pago = source.DiasPago;
            _medio_pago = source.MedioPago;
            _prevision_pago = source.Prevision;
            _base_imponible = source.BaseImponible;
            _p_irpf = source.PIrpf;
            _p_igic = source.PIgic;
            _p_descuento = source.PDescuento;
            _descuento = source.Descuento;
            _total = source.Total;
            _cuenta_bancaria = source.CuentaBancaria;
            _nota = source.Nota;
            _observaciones = source.Observaciones;
            _albaran = source.Albaran;
            _rectificativa = source.Rectificativa;
            _estado = source.Estado;
            _id_mov_contable = source.IdMovContable;
            _albaranes = source.Albaranes;
            _oid_usuario = source.OidUsuario;
            _oid_expediente = source.OidExpediente;
            _irpf = source.Irpf;
            _impuestos = source.Impuestos;
        }

        #endregion
    }
}