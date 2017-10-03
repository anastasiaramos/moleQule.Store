using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class ExpenseTypeRecord : RecordBase
	{
		#region Attributes

		private long _serial;
		private string _codigo = string.Empty;
		private long _categoria;
		private string _nombre = string.Empty;
		private long _medio_pago;
		private long _forma_pago;
		private long _dias_pago;
		private long _oid_cuenta_asociada;
		private string _cuenta_bancaria = string.Empty;
		private string _cuenta_contable = string.Empty;
		private string _observaciones = string.Empty;
		#endregion

		#region Properties

		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Categoria { get { return _categoria; } set { _categoria = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual long OidCuentaAsociada { get { return _oid_cuenta_asociada; } set { _oid_cuenta_asociada = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

		#endregion

		#region Business Methods

		public ExpenseTypeRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_categoria = Format.DataReader.GetInt64(source, "CATEGORIA");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
			_dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
			_oid_cuenta_asociada = Format.DataReader.GetInt64(source, "OID_CUENTA_ASOCIADA");
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}
		public virtual void CopyValues(ExpenseTypeRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_categoria = source.Categoria;
			_nombre = source.Nombre;
			_medio_pago = source.MedioPago;
			_forma_pago = source.FormaPago;
			_dias_pago = source.DiasPago;
			_oid_cuenta_asociada = source.OidCuentaAsociada;
			_cuenta_bancaria = source.CuentaBancaria;
			_cuenta_contable = source.CuentaContable;
			_observaciones = source.Observaciones;
		}

		#endregion
	}
}