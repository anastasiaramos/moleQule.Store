using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class PayrollRecord : RecordBase
    {
        #region Attributes

        private long _oid_usuario;
        private long _oid_remesa;
        private long _oid_tipo;
        private long _oid_expediente;
        private long _oid_empleado;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private DateTime _fecha;
        private string _descripcion = string.Empty;
        private Decimal _bruto;
        private Decimal _base_irpf;
        private Decimal _neto;
        private Decimal _p_irpf;
        private Decimal _seguro;
        private Decimal _descuentos;
        private DateTime _prevision_pago;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties

        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual long OidRemesa { get { return _oid_remesa; } set { _oid_remesa = value; } }
        public virtual long OidTipo { get { return _oid_tipo; } set { _oid_tipo = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidEmpleado { get { return _oid_empleado; } set { _oid_empleado = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual Decimal Bruto { get { return _bruto; } set { _bruto = value; } }
        public virtual Decimal BaseIrpf { get { return _base_irpf; } set { _base_irpf = value; } }
        public virtual Decimal Neto { get { return _neto; } set { _neto = value; } }
        public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }
        public virtual Decimal Seguro { get { return _seguro; } set { _seguro = value; } }
        public virtual Decimal Descuentos { get { return _descuentos; } set { _descuentos = value; } }
        public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public PayrollRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _oid_remesa = Format.DataReader.GetInt64(source, "OID_REMESA");
            _oid_tipo = Format.DataReader.GetInt64(source, "OID_TIPO");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_empleado = Format.DataReader.GetInt64(source, "OID_EMPLEADO");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _bruto = Format.DataReader.GetDecimal(source, "BRUTO");
            _base_irpf = Format.DataReader.GetDecimal(source, "BASE_IRPF");
            _neto = Format.DataReader.GetDecimal(source, "NETO");
            _p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
            _seguro = Format.DataReader.GetDecimal(source, "SEGURO");
            _descuentos = Format.DataReader.GetDecimal(source, "DESCUENTOS");
            _prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(PayrollRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_usuario = source.OidUsuario;
            _oid_remesa = source.OidRemesa;
            _oid_tipo = source.OidTipo;
            _oid_expediente = source.OidExpediente;
            _oid_empleado = source.OidEmpleado;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _fecha = source.Fecha;
            _descripcion = source.Descripcion;
            _bruto = source.Bruto;
            _base_irpf = source.BaseIrpf;
            _neto = source.Neto;
            _p_irpf = source.PIrpf;
            _seguro = source.Seguro;
            _descuentos = source.Descuentos;
            _prevision_pago = source.PrevisionPago;
            _observaciones = source.Observaciones;
        }
        #endregion
    }
}