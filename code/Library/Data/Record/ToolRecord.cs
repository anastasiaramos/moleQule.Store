using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class ToolRecord : RecordBase
	{
		#region Attributes

		private long _serial;
		private string _code = string.Empty;
		private long _status;
		private string _name = string.Empty;
		private string _description = string.Empty;
		private DateTime _from;
		private DateTime _till;
		private Decimal _cost;
		private string _location = string.Empty;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string ID { get { return _code; } set { _code = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual string Name { get { return _name; } set { _name = value; } }
		public virtual string Description { get { return _description; } set { _description = value; } }
		public virtual DateTime From { get { return _from; } set { _from = value; } }
		public virtual DateTime Till { get { return _till; } set { _till = value; } }
		public virtual Decimal Cost { get { return _cost; } set { _cost = value; } }
		public virtual string Location { get { return _location; } set { _location = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public ToolRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_code = Format.DataReader.GetString(source, "CODE");
			_status = Format.DataReader.GetInt64(source, "STATUS");
			_name = Format.DataReader.GetString(source, "NAME");
			_description = Format.DataReader.GetString(source, "DESCRIPTION");
			_from = Format.DataReader.GetDateTime(source, "FROM");
			_till = Format.DataReader.GetDateTime(source, "TILL");
			_cost = Format.DataReader.GetDecimal(source, "COST");
			_location = Format.DataReader.GetString(source, "LOCATION");
			_comments = Format.DataReader.GetString(source, "COMMENTS");
		}		
		public virtual void CopyValues(ToolRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_code = source.ID;
			_status = source.Status;
			_name = source.Name;
			_description = source.Description;
			_from = source.From;
			_till = source.Till;
			_cost = source.Cost;
			_location = source.Location;
			_comments = source.Comments;
		}
		
		#endregion	
	}
}
