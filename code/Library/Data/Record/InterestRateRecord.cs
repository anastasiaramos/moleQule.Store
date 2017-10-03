using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class InterestRateRecord : RecordBase
	{
		#region Attributes

		private long _oid_prestamo;
		private Decimal _tipo_interes;
		private DateTime _fecha_inicio;
		private DateTime _fecha_fin;
		private Decimal _importe_cuota;
		#endregion

		#region Properties

		public virtual long OidPrestamo { get { return _oid_prestamo; } set { _oid_prestamo = value; } }
		public virtual Decimal TipoInteres { get { return _tipo_interes; } set { _tipo_interes = value; } }
		public virtual DateTime FechaInicio { get { return _fecha_inicio; } set { _fecha_inicio = value; } }
		public virtual DateTime FechaFin { get { return _fecha_fin; } set { _fecha_fin = value; } }
		public virtual Decimal ImporteCuota { get { return _importe_cuota; } set { _importe_cuota = value; } }

		#endregion

		#region Business Methods

		public InterestRateRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_prestamo = Format.DataReader.GetInt64(source, "OID_PRESTAMO");
			_tipo_interes = Format.DataReader.GetDecimal(source, "TIPO_INTERES");
			_fecha_inicio = Format.DataReader.GetDateTime(source, "FECHA_INICIO");
			_fecha_fin = Format.DataReader.GetDateTime(source, "FECHA_FIN");
			_importe_cuota = Format.DataReader.GetDecimal(source, "IMPORTE_CUOTA");

		}
		public virtual void CopyValues(InterestRateRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_prestamo = source.OidPrestamo;
			_tipo_interes = source.TipoInteres;
			_fecha_inicio = source.FechaInicio;
			_fecha_fin = source.FechaFin;
			_importe_cuota = source.ImporteCuota;
		}

		#endregion
	}
}