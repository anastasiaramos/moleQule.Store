using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class ExpenseRecord : RecordBase
    {
        #region Attributes

        private long _oid_tipo;
        private long _oid_expediente;
        private long _oid_factura;
        private long _oid_empleado;
        private long _oid_nomina;
        private long _serial;
        private string _codigo = string.Empty;
        private DateTime _fecha;
        private string _descripcion = string.Empty;
        private string _facturas = string.Empty;
        private Decimal _total;
        private DateTime _prevision_pago;
        private long _estado;
        private string _observaciones = string.Empty;
        private long _oid_albaran;
        private long _oid_concepto_factura;
        private long _oid_concepto_albaran;
        private long _oid_usuario;
        private long _categoria_gasto;

        #endregion

        #region Properties

        public virtual long CategoriaGasto { get { return _categoria_gasto; } set { _categoria_gasto = value; } }
        public virtual long OidTipo { get { return _oid_tipo; } set { _oid_tipo = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidFactura { get { return _oid_factura; } set { _oid_factura = value; } }
        public virtual long OidEmpleado { get { return _oid_empleado; } set { _oid_empleado = value; } }
        public virtual long OidNomina { get { return _oid_nomina; } set { _oid_nomina = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual string Facturas { get { return _facturas; } set { _facturas = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
        public virtual long OidConceptoFactura { get { return _oid_concepto_factura; } set { _oid_concepto_factura = value; } }
        public virtual long OidConceptoAlbaran { get { return _oid_concepto_albaran; } set { _oid_concepto_albaran = value; } }
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

        #endregion

        #region Business Methods

        public ExpenseRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_tipo = Format.DataReader.GetInt64(source, "OID_TIPO");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
            _oid_empleado = Format.DataReader.GetInt64(source, "OID_EMPLEADO");
            _oid_nomina = Format.DataReader.GetInt64(source, "OID_REMESA_NOMINA");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _facturas = Format.DataReader.GetString(source, "FACTURAS");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
            _oid_concepto_factura = Format.DataReader.GetInt64(source, "OID_CONCEPTO_FACTURA");
            _oid_concepto_albaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _categoria_gasto = Format.DataReader.GetInt64(source, "TIPO");
        }
        public virtual void CopyValues(ExpenseRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_tipo = source.OidTipo;
            _oid_expediente = source.OidExpediente;
            _oid_factura = source.OidFactura;
            _oid_empleado = source.OidEmpleado;
            _oid_nomina = source.OidNomina;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _fecha = source.Fecha;
            _descripcion = source.Descripcion;
            _facturas = source.Facturas;
            _total = source.Total;
            _prevision_pago = source.PrevisionPago;
            _estado = source.Estado;
            _observaciones = source.Observaciones;
            _oid_albaran = source.OidAlbaran;
            _oid_concepto_factura = source.OidConceptoFactura;
            _oid_concepto_albaran = source.OidConceptoAlbaran;
            _oid_usuario = source.OidUsuario;
            _categoria_gasto = source.CategoriaGasto;
        }

        #endregion
    }
}