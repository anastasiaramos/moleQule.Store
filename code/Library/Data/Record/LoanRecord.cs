using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Store.Data
{
	[Serializable()]
	public class LoanRecord : RecordBase
	{
		#region Attributes

		private long _oid_cuenta;
		private DateTime _fecha_firma;
		private DateTime _fecha_ingreso;
		private DateTime _fecha_vencimiento;
		private string _nombre = string.Empty;
		private Decimal _importe;
		private string _observaciones = string.Empty;
		private long _serial;
		private string _codigo = string.Empty;
		private long _oid_pago;
		private long _n_cuotas;
		private DateTime _inicio_pago;
		private long _periodo_pago;
		private Decimal _importe_cuota;
        private string _cuenta_contable = string.Empty;
        private Decimal _gastos_bancarios;
        private bool _gastos_inicio = false;
        private long _estado;

		#endregion

		#region Properties

		public virtual long OidCuenta { get { return _oid_cuenta; } set { _oid_cuenta = value; } }
		public virtual DateTime FechaFirma { get { return _fecha_firma; } set { _fecha_firma = value; } }
		public virtual DateTime FechaIngreso { get { return _fecha_ingreso; } set { _fecha_ingreso = value; } }
		public virtual DateTime FechaVencimiento { get { return _fecha_vencimiento; } set { _fecha_vencimiento = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long OidPago { get { return _oid_pago; } set { _oid_pago = value; } }
		public virtual long NCuotas { get { return _n_cuotas; } set { _n_cuotas = value; } }
		public virtual DateTime InicioPago { get { return _inicio_pago; } set { _inicio_pago = value; } }
		public virtual long PeriodoPago { get { return _periodo_pago; } set { _periodo_pago = value; } }
		public virtual Decimal ImporteCuota { get { return _importe_cuota; } set { _importe_cuota = value; } }
        public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
        public virtual Decimal GastosBancarios { get { return _gastos_bancarios; } set { _gastos_bancarios = value; } }
        public virtual bool GastosInicio { get { return _gastos_inicio; } set { _gastos_inicio = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }

		#endregion

		#region Business Methods

		public LoanRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cuenta = Format.DataReader.GetInt64(source, "OID_CUENTA");
			_fecha_firma = Format.DataReader.GetDateTime(source, "FECHA_FIRMA");
			_fecha_ingreso = Format.DataReader.GetDateTime(source, "FECHA_INGRESO");
			_fecha_vencimiento = Format.DataReader.GetDateTime(source, "FECHA_VENCIMIENTO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
			_n_cuotas = Format.DataReader.GetInt64(source, "N_CUOTAS");
			_inicio_pago = Format.DataReader.GetDateTime(source, "INICIO_PAGO");
			_periodo_pago = Format.DataReader.GetInt64(source, "PERIODO_PAGO");
			_importe_cuota = Format.DataReader.GetDecimal(source, "IMPORTE_CUOTA");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
            _gastos_bancarios = Format.DataReader.GetDecimal(source, "GASTOS_BANCARIOS");
            _gastos_inicio = Format.DataReader.GetBool(source, "GASTOS_INICIO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");

		}
		public virtual void CopyValues(LoanRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cuenta = source.OidCuenta;
			_fecha_firma = source.FechaFirma;
			_fecha_ingreso = source.FechaIngreso;
			_fecha_vencimiento = source.FechaVencimiento;
			_nombre = source.Nombre;
			_importe = source.Importe;
			_observaciones = source.Observaciones;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_oid_pago = source.OidPago;
			_n_cuotas = source.NCuotas;
			_inicio_pago = source.InicioPago;
			_periodo_pago = source.PeriodoPago;
			_importe_cuota = source.ImporteCuota;
            _cuenta_contable = source.CuentaContable;
            _gastos_bancarios = source.GastosBancarios;
            _gastos_inicio = source.GastosInicio;
            _estado = source.Estado;
		}

		#endregion
	}
}