using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class WorkReportResourceRecord : RecordBase
	{
		#region Attributes

		private long _oid_work_report;
		private long _oid_resource;
		private long _entity_type;
		private DateTime _from;
		private DateTime _till;
		private Decimal _amount;
		private Decimal _cost;
		private Decimal _hours;
		private Decimal _extra_cost;
        private Decimal _total;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties

		public virtual long OidWorkReport { get { return _oid_work_report; } set { _oid_work_report = value; } }
		public virtual long OidResource { get { return _oid_resource; } set { _oid_resource = value; } }
		public virtual long EntityType { get { return _entity_type; } set { _entity_type = value; } }
		public virtual Decimal Amount { get { return _amount; } set { _amount = value; } }
		public virtual DateTime From { get { return _from; } set { _from = value; } }
		public virtual DateTime Till { get { return _till; } set { _till = value; } }
		public virtual Decimal Cost { get { return _cost; } set { _cost = value; } }
		public virtual Decimal Hours { get { return _hours; } set { _hours = value; } }
		public virtual Decimal ExtraCost { get { return _extra_cost; } set { _extra_cost = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public WorkReportResourceRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_work_report = Format.DataReader.GetInt64(source, "OID_WORK_REPORT");
			_oid_resource = Format.DataReader.GetInt64(source, "OID_RESOURCE");
			_entity_type = Format.DataReader.GetInt64(source, "ENTITY_TYPE");
			_amount = Format.DataReader.GetDecimal(source, "AMOUNT");
			_cost = Format.DataReader.GetDecimal(source, "COST");
			_from = Format.DataReader.GetDateTime(source, "FROM");
			_till = Format.DataReader.GetDateTime(source, "TILL");
			_hours = Format.DataReader.GetDecimal(source, "HOURS");
			_extra_cost = Format.DataReader.GetDecimal(source, "EXTRA_COST");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
			_comments = Format.DataReader.GetString(source, "COMMENTS");
		}		
		public virtual void CopyValues(WorkReportResourceRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_work_report = source.OidWorkReport;
			_oid_resource = source.OidResource;
			_entity_type = source.EntityType;
			_amount = source.Amount;
			_cost = source.Cost;
			_from = source.From;
			_till = source.Till;
			_hours = source.Hours;
			_extra_cost = source.ExtraCost;
            _total = source.Total;
			_comments = source.Comments;
		}
		
		#endregion	
	}
}