using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class LineaFomentoRecord : RecordBase
    {
        #region Attributes

        private long _oid_partida;
        private long _oid_expediente;
        private long _oid_naviera;
        private long _serial;
        private string _codigo = string.Empty;
        private DateTime _fecha;
        private string _descripcion = string.Empty;
        private string _id_solicitud = string.Empty;
        private string _id_envio = string.Empty;
        private string _conocimiento = string.Empty;
        private DateTime _fecha_conocimiento;
        private string _dua = string.Empty;
        private Decimal _kilos;
        private Decimal _flete_neto;
        private Decimal _baf;
        private bool _teus20 = false;
        private bool _teus40 = false;
        private Decimal _t3_origen;
        private Decimal _t3_destino;
        private Decimal _thc_origen;
        private Decimal _thc_destino;
        private Decimal _isps;
        private Decimal _total;
        private long _estado;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidNaviera { get { return _oid_naviera; } set { _oid_naviera = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual string IdSolicitud { get { return _id_solicitud; } set { _id_solicitud = value; } }
        public virtual string IdEnvio { get { return _id_envio; } set { _id_envio = value; } }
        public virtual string Conocimiento { get { return _conocimiento; } set { _conocimiento = value; } }
        public virtual DateTime FechaConocimiento { get { return _fecha_conocimiento; } set { _fecha_conocimiento = value; } }
        public virtual string Dua { get { return _dua; } set { _dua = value; } }
        public virtual Decimal Kilos { get { return _kilos; } set { _kilos = value; } }
        public virtual Decimal FleteNeto { get { return _flete_neto; } set { _flete_neto = value; } }
        public virtual Decimal Baf { get { return _baf; } set { _baf = value; } }
        public virtual bool Teus20 { get { return _teus20; } set { _teus20 = value; } }
        public virtual bool Teus40 { get { return _teus40; } set { _teus40 = value; } }
        public virtual Decimal T3Origen { get { return _t3_origen; } set { _t3_origen = value; } }
        public virtual Decimal T3Destino { get { return _t3_destino; } set { _t3_destino = value; } }
        public virtual Decimal ThcOrigen { get { return _thc_origen; } set { _thc_origen = value; } }
        public virtual Decimal ThcDestino { get { return _thc_destino; } set { _thc_destino = value; } }
        public virtual Decimal Isps { get { return _isps; } set { _isps = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public LineaFomentoRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_partida = Format.DataReader.GetInt64(source, "OID_PARTIDA");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_naviera = Format.DataReader.GetInt64(source, "OID_NAVIERA");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _id_solicitud = Format.DataReader.GetString(source, "ID_SOLICITUD");
            _id_envio = Format.DataReader.GetString(source, "ID_ENVIO");
            _conocimiento = Format.DataReader.GetString(source, "CONOCIMIENTO");
            _fecha_conocimiento = Format.DataReader.GetDateTime(source, "FECHA_CONOCIMIENTO");
            _dua = Format.DataReader.GetString(source, "DUA");
            _kilos = Format.DataReader.GetDecimal(source, "KILOS");
            _flete_neto = Format.DataReader.GetDecimal(source, "FLETE_NETO");
            _baf = Format.DataReader.GetDecimal(source, "BAF");
            _teus20 = Format.DataReader.GetBool(source, "TEUS20");
            _teus40 = Format.DataReader.GetBool(source, "TEUS40");
            _t3_origen = Format.DataReader.GetDecimal(source, "T3_ORIGEN");
            _t3_destino = Format.DataReader.GetDecimal(source, "T3_DESTINO");
            _thc_origen = Format.DataReader.GetDecimal(source, "THC_ORIGEN");
            _thc_destino = Format.DataReader.GetDecimal(source, "THC_DESTINO");
            _isps = Format.DataReader.GetDecimal(source, "ISPS");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(LineaFomentoRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_partida = source.OidPartida;
            _oid_expediente = source.OidExpediente;
            _oid_naviera = source.OidNaviera;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _fecha = source.Fecha;
            _descripcion = source.Descripcion;
            _id_solicitud = source.IdSolicitud;
            _id_envio = source.IdEnvio;
            _conocimiento = source.Conocimiento;
            _fecha_conocimiento = source.FechaConocimiento;
            _dua = source.Dua;
            _kilos = source.Kilos;
            _flete_neto = source.FleteNeto;
            _baf = source.Baf;
            _teus20 = source.Teus20;
            _teus40 = source.Teus40;
            _t3_origen = source.T3Origen;
            _t3_destino = source.T3Destino;
            _thc_origen = source.ThcOrigen;
            _thc_destino = source.ThcDestino;
            _isps = source.Isps;
            _total = source.Total;
            _estado = source.Estado;
            _observaciones = source.Observaciones;
        }
        #endregion
    }
}