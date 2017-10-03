using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class TransactionPaymentRecord : RecordBase
    {
        #region Attributes

        private long _oid_pago;
        private long _oid_operacion;
        private long _oid_expediente;
        private long _tipo_pago;
        private long _tipo_agente;
        private Decimal _cantidad;

        #endregion

        #region Properties

        public virtual long OidPago { get { return _oid_pago; } set { _oid_pago = value; } }
        public virtual long OidOperacion { get { return _oid_operacion; } set { _oid_operacion = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long TipoPago { get { return _tipo_pago; } set { _tipo_pago = value; } }
        public virtual long TipoAgente { get { return _tipo_agente; } set { _tipo_agente = value; } }
        public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }

        #endregion

        #region Business Methods

        public TransactionPaymentRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
            _oid_operacion = Format.DataReader.GetInt64(source, "OID_OPERACION");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _tipo_pago = Format.DataReader.GetInt64(source, "TIPO_PAGO");
            _tipo_agente = Format.DataReader.GetInt64(source, "TIPO_AGENTE");
            _cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");

        }
        public virtual void CopyValues(TransactionPaymentRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_pago = source.OidPago;
            _oid_operacion = source.OidOperacion;
            _oid_expediente = source.OidExpediente;
            _tipo_pago = source.TipoPago;
            _tipo_agente = source.TipoAgente;
            _cantidad = source.Cantidad;
        }

        #endregion
    }
}