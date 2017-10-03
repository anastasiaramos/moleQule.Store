using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class PayrollBatchRecord : RecordBase
	{
		#region Attributes

		private long _serial;
		private string _codigo = string.Empty;
		private DateTime _fecha;
		private string _descripcion = string.Empty;
		private Decimal _total;
		private Decimal _irpf;
		private Decimal _seguro_empresa;
		private Decimal _seguro_personal;
		private DateTime _prevision_pago;
		private long _estado;
		private string _observaciones = string.Empty;
		private Decimal _base_irpf;
		private Decimal _descuentos;

		#endregion

		#region Properties

		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual Decimal Irpf { get { return _irpf; } set { _irpf = value; } }
		public virtual Decimal SeguroEmpresa { get { return _seguro_empresa; } set { _seguro_empresa = value; } }
		public virtual Decimal SeguroPersonal { get { return _seguro_personal; } set { _seguro_personal = value; } }
		public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual Decimal BaseIrpf { get { return _base_irpf; } set { _base_irpf = value; } }
		public virtual Decimal Descuentos { get { return _descuentos; } set { _descuentos = value; } }

		#endregion

		#region Business Methods

		public PayrollBatchRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_irpf = Format.DataReader.GetDecimal(source, "IRPF");
			_seguro_empresa = Format.DataReader.GetDecimal(source, "SEGURO_EMPRESA");
			_seguro_personal = Format.DataReader.GetDecimal(source, "SEGURO_PERSONAL");
			_prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_base_irpf = Format.DataReader.GetDecimal(source, "BASE_IRPF");
			_descuentos = Format.DataReader.GetDecimal(source, "DESCUENTOS");

		}
		public virtual void CopyValues(PayrollBatchRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_fecha = source.Fecha;
			_descripcion = source.Descripcion;
			_total = source.Total;
			_irpf = source.Irpf;
			_seguro_empresa = source.SeguroEmpresa;
			_seguro_personal = source.SeguroPersonal;
			_prevision_pago = source.PrevisionPago;
			_estado = source.Estado;
			_observaciones = source.Observaciones;
			_base_irpf = source.BaseIrpf;
			_descuentos = source.Descuentos;
		}

		#endregion
	}
}