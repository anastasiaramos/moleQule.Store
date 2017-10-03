using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Store.Data
{
	[Serializable()]
	public class FamilySerieRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_familia;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidFamilia { get { return _oid_familia; } set { _oid_familia = value; } }

		#endregion

		#region Business Methods

        public FamilySerieRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");

		}
        public virtual void CopyValues(FamilySerieRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_familia = source.OidFamilia;
		}

		#endregion
	}
}