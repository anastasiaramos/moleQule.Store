using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Store.Data
{
	[Serializable()]
	public class SerieRecord : RecordBase
	{
		#region Attributes

		private long _oid_impuesto;

		private string _nombre = string.Empty;
		private string _identificador = string.Empty;
		private long _tipo;
		private string _cabecera = string.Empty;
		private bool _resumen = false;
		private string _observaciones = string.Empty;

		#endregion

		#region Properties

		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string Identificador { get { return _identificador; } set { _identificador = value; } }
		public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual string Cabecera { get { return _cabecera; } set { _cabecera = value; } }
		public virtual bool Resumen { get { return _resumen; } set { _resumen = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

		#endregion

		#region Business Methods

		public SerieRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_identificador = Format.DataReader.GetString(source, "IDENTIFICADOR");
			_tipo = Format.DataReader.GetInt64(source, "TIPO");
			_cabecera = Format.DataReader.GetString(source, "CABECERA");
			_resumen = Format.DataReader.GetBool(source, "RESUMEN");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}
		public virtual void CopyValues(SerieRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_impuesto = source.OidImpuesto;
			_nombre = source.Nombre;
			_identificador = source.Identificador;
			_tipo = source.Tipo;
			_cabecera = source.Cabecera;
			_resumen = source.Resumen;
			_observaciones = source.Observaciones;
		}

		#endregion
	}
}