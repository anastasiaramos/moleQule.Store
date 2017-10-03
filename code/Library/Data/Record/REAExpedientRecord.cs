using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class REAExpedientRecord : RecordBase
    {
        #region Attributes

        private long _oid_expediente;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private string _codigo_aduanero = string.Empty;
        private DateTime _fecha;
        private string _expediente_rea = string.Empty;
        private string _certificado_rea = string.Empty;
        private string _n_dua = string.Empty;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties

        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string CodigoAduanero { get { return _codigo_aduanero; } set { _codigo_aduanero = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string ExpedienteRea { get { return _expediente_rea; } set { _expediente_rea = value; } }
        public virtual string CertificadoRea { get { return _certificado_rea; } set { _certificado_rea = value; } }
        public virtual string NDua { get { return _n_dua; } set { _n_dua = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public REAExpedientRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _codigo_aduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _expediente_rea = Format.DataReader.GetString(source, "EXPEDIENTE_REA");
            _certificado_rea = Format.DataReader.GetString(source, "CERTIFICADO_REA");
            _n_dua = Format.DataReader.GetString(source, "N_DUA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }
        public virtual void CopyValues(REAExpedientRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_expediente = source.OidExpediente;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _codigo_aduanero = source.CodigoAduanero;
            _fecha = source.Fecha;
            _expediente_rea = source.ExpedienteRea;
            _certificado_rea = source.CertificadoRea;
            _n_dua = source.NDua;
            _observaciones = source.Observaciones;
        }
 
        #endregion
    }
}