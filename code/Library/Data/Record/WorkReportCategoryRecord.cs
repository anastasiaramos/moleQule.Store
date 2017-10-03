using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class WorkReportCategoryRecord : RecordBase
	{
		#region Attributes

		private string _name = string.Empty;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual string Name { get { return _name; } set { _name = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public WorkReportCategoryRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_name = Format.DataReader.GetString(source, "NAME");
			_comments = Format.DataReader.GetString(source, "COMMENTS");

		}		
		public virtual void CopyValues(WorkReportCategoryRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_name = source.Name;
			_comments = source.Comments;
		}
		
		#endregion	
	}
}
