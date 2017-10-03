using System;
using System.Data;

using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class ProviderBaseRecord : RecordBase
	{
		#region Attributes

		protected long _tipo_acreedor = 0;
		protected long _estado = 10;
		protected long _serial;
		protected string _codigo = string.Empty;
		protected string _id = string.Empty;
		protected long _tipo_id;
		protected string _nombre = string.Empty;
		protected string _alias = string.Empty;
		protected string _direccion = string.Empty;
		protected string _cod_postal = string.Empty;
		protected string _localidad = string.Empty;
		protected string _municipio = string.Empty;
		protected string _provincia = string.Empty;
		protected string _pais = string.Empty;
		protected string _contacto = string.Empty;
		protected string _telefono = string.Empty;
		protected string _email = string.Empty;
		protected string _observaciones = string.Empty;
		protected long _dias_pago;
		protected long _forma_pago = (long)EFormaPago.Contado;
		protected long _medio_pago = (long)EMedioPago.Efectivo;
		protected long _oid_cuentab_asociada;
		protected string _cuenta_bancaria = string.Empty;
		protected string _swift = string.Empty;
		protected string _cuenta_contable = string.Empty;
		protected long _oid_impuesto;
		protected long _oid_tarjeta_asociada;
		protected decimal _p_irpf;

		#endregion

		#region Properties

		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual string Identificador { get { return _id; } set { _id = value; } }
		public virtual long TipoId { get { return _tipo_id; } set { _tipo_id = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string Alias { get { return _alias; } set { _alias = value; } }
		public virtual string CodPostal { get { return _cod_postal; } set { _cod_postal = value; } }
		public virtual string Localidad { get { return _localidad; } set { _localidad = value; } }
		public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
		public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
		public virtual string Telefono { get { return _telefono; } set { _telefono = value; } }
		public virtual string Pais { get { return _pais; } set { _pais = value; } }
		public virtual string Contacto { get { return _contacto; } set { _contacto = value; } }
		public virtual string Email { get { return _email; } set { _email = value; } }
		public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual string Swift { get { return _swift; } set { _swift = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidCuentaBAsociada { get { return _oid_cuentab_asociada; } set { _oid_cuentab_asociada = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public virtual long TipoAcreedor { get { return _tipo_acreedor; } set { _tipo_acreedor = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long OidTarjetaAsociada { get { return _oid_tarjeta_asociada; } set { _oid_tarjeta_asociada = value; } }
		public virtual Decimal PIRPF { get { return Decimal.Round(_p_irpf, 2); } set { _p_irpf = value; } }
					
		#endregion

		#region Business Methods

		public ProviderBaseRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_id = Format.DataReader.GetString(source, "ID");
			_tipo_id = Format.DataReader.GetInt64(source, "TIPO_ID");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_alias = Format.DataReader.GetString(source, "ALIAS");
			_direccion = Format.DataReader.GetString(source, "DIRECCION");
			_telefono = Format.DataReader.GetString(source, "TELEFONO");
			_localidad = Format.DataReader.GetString(source, "LOCALIDAD");
			_municipio = Format.DataReader.GetString(source, "MUNICIPIO");
			_cod_postal = Format.DataReader.GetString(source, "COD_POSTAL");
			_provincia = Format.DataReader.GetString(source, "PROVINCIA");
			_pais = Format.DataReader.GetString(source, "PAIS");
			_contacto = Format.DataReader.GetString(source, "CONTACTO");
			_email = Format.DataReader.GetString(source, "EMAIL");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
			_dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_oid_cuentab_asociada = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA_ASOCIADA");
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_swift = Format.DataReader.GetString(source, "SWIFT");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_oid_tarjeta_asociada = Format.DataReader.GetInt64(source, "OID_TARJETA_ASOCIADA");
			_p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
		}
		public virtual void CopyValues(ProviderBaseRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_tipo_acreedor = source.TipoAcreedor;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_estado = source.Estado;
            _id = source.Identificador;
			_tipo_id = source.TipoId;
			_nombre = source.Nombre;
			_alias = source.Alias;
			_cod_postal = source.CodPostal;
			_direccion = source.Direccion;
			_localidad = source.Localidad;
			_municipio = source.Municipio;
			_provincia = source.Provincia;
			_telefono = source.Telefono;
			_observaciones = source.Observaciones;
			_forma_pago = source.FormaPago;
			_medio_pago = source.MedioPago;
			_dias_pago = source.DiasPago;
			_pais = source.Pais;
			_contacto = source.Contacto;
			_email = source.Email;
			_oid_cuentab_asociada = source.OidCuentaBAsociada;
			_cuenta_bancaria = source.CuentaBancaria;
			_swift = source.Swift;
			_cuenta_contable = source.CuentaContable;
			_oid_impuesto = source.OidImpuesto;
			_oid_tarjeta_asociada = source.OidTarjetaAsociada;
			_p_irpf = source.PIRPF;
		}

		#endregion
	}
}

