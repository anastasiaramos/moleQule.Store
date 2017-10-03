using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class FamilyRecord : RecordBase
    {
        #region Attributes

        private long _oid_impuesto;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _cuenta_contable_compra = string.Empty;
        private string _cuenta_contable_venta = string.Empty;
        private string _observaciones = string.Empty;
        private bool _avisar_beneficio_minimo = false;
        private Decimal _p_beneficio_minimo;

        #endregion

        #region Properties
        public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string CuentaContableCompra { get { return _cuenta_contable_compra; } set { _cuenta_contable_compra = value; } }
        public virtual string CuentaContableVenta { get { return _cuenta_contable_venta; } set { _cuenta_contable_venta = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual bool AvisarBeneficioMinimo { get { return _avisar_beneficio_minimo; } set { _avisar_beneficio_minimo = value; } }
        public virtual Decimal PBeneficioMinimo { get { return _p_beneficio_minimo; } set { _p_beneficio_minimo = value; } }

        #endregion

        #region Business Methods

        public FamilyRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _cuenta_contable_compra = Format.DataReader.GetString(source, "CUENTA_CONTABLE_COMPRA");
            _cuenta_contable_venta = Format.DataReader.GetString(source, "CUENTA_CONTABLE_VENTA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _avisar_beneficio_minimo = Format.DataReader.GetBool(source, "AVISAR_BENEFICIO_MINIMO");
            _p_beneficio_minimo = Format.DataReader.GetDecimal(source, "P_BENEFICIO_MINIMO");

        }

        public virtual void CopyValues(FamilyRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_impuesto = source.OidImpuesto;
            _codigo = source.Codigo;
            _nombre = source.Nombre;
            _cuenta_contable_compra = source.CuentaContableCompra;
            _cuenta_contable_venta = source.CuentaContableVenta;
            _observaciones = source.Observaciones;
            _avisar_beneficio_minimo = source.AvisarBeneficioMinimo;
            _p_beneficio_minimo = source.PBeneficioMinimo;
        }
        #endregion
    }
}