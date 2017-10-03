using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class WorkReportRecord : RecordBase
	{
		#region Attributes

		private long _oid_owner;
		private long _oid_expedient;
		private long _serial;
		private string _code = string.Empty;
		private long _status;
		private DateTime _date;
		private DateTime _from;
		private DateTime _till;
		private Decimal _hours;
		private Decimal _total;
		private long _category;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual long OidOwner { get { return _oid_owner; } set { _oid_owner = value; } }
		public virtual long OidExpedient { get { return _oid_expedient; } set { _oid_expedient = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Code { get { return _code; } set { _code = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual DateTime Date { get { return _date; } set { _date = value; } }
		public virtual DateTime From { get { return _from; } set { _from = value; } }
		public virtual DateTime Till { get { return _till; } set { _till = value; } }
		public virtual Decimal Hours { get { return _hours; } set { _hours = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual long Category { get { return _category; } set { _category = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public WorkReportRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_owner = Format.DataReader.GetInt64(source, "OID_OWNER");
			_oid_expedient = Format.DataReader.GetInt64(source, "OID_EXPEDIENT");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_code = Format.DataReader.GetString(source, "CODE");
			_status = Format.DataReader.GetInt64(source, "STATUS");
			_date = Format.DataReader.GetDateTime(source, "DATE");
			_from = Format.DataReader.GetDateTime(source, "FROM");
			_till = Format.DataReader.GetDateTime(source, "TILL");
			_hours = Format.DataReader.GetDecimal(source, "HOURS");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_category = Format.DataReader.GetInt64(source, "CATEGORY");
			_comments = Format.DataReader.GetString(source, "COMMENTS");
		}		
		public virtual void CopyValues(WorkReportRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_owner = source.OidOwner;
			_oid_expedient = source.OidExpedient;
			_serial = source.Serial;
			_code = source.Code;
			_status = source.Status;
			_date = source.Date;
			_from = source.From;
			_till = source.Till;
			_hours = source.Hours;
			_total = source.Total;
			_category = source.Category;
			_comments = source.Comments;
		}
		
		#endregion	
	}
}