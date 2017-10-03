using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class WorkReportResourceInfo : ReadOnlyBaseEx<WorkReportResourceInfo, WorkReportResource>
	{	
		#region Attributes

		protected WorkReportResourceBase _base = new WorkReportResourceBase();

		
		#endregion
		
		#region Properties
		
		public WorkReportResourceBase Base { get { return _base; } }
				
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; }}
		public long OidWorkReport { get { return _base.Record.OidWorkReport; } }
		public long OidResource { get { return _base.Record.OidResource; } }
		public long EntityType { get { return _base.Record.EntityType; } }
		public Decimal Amount { get { return _base.Record.Amount; } }
		public Decimal Cost { get { return _base.Record.Cost; } }
		public DateTime From { get { return _base.Record.From; } }
		public DateTime Till { get { return _base.Record.Till; } }
		public Decimal Hours { get { return _base.Record.Hours; } }
		public Decimal ExtraCost { get { return _base.Record.ExtraCost; } }
        public Decimal Total { get { return _base.Record.Total; } }
		public string Comments { get { return _base.Record.Comments; } }

		public moleQule.Common.Structs.ETipoEntidad EEntityType { get { return _base.EEntityType; } }
		public string EntityTypeLabel { get { return _base.EntityTypeLabel; } }
        public long OidCategory { get { return _base.OidCategory; } set { _base.OidCategory = value; } }
        public string Category { get { return _base.Category; } set { _base.Category = value; } }
		public string Resource { get { return _base.Resource; } set { _base.Resource = value; } }
		public string ResourceID { get { return _base.ResourceID; } set { _base.ResourceID = value; } }
        public string WorkReportID { get { return _base.WorkReportID; } set { _base.WorkReportID = value; } }
        public string ExpedientID { get { return _base.ExpedientID; } set { _base.ExpedientID = value; } }
        public string Month { get { return _base.Month; } }
        public string Year { get { return _base.Year; } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(WorkReportResource source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected WorkReportResourceInfo() { /* require use of factory methods */ }
		private WorkReportResourceInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal WorkReportResourceInfo(WorkReportResource item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static WorkReportResourceInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new WorkReportResourceInfo(sessionCode, reader, childs);
		}
		
		public static WorkReportResourceInfo New(long oid = 0) { return new WorkReportResourceInfo(){ Oid = oid}; }
		
 		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion			
	}
}
