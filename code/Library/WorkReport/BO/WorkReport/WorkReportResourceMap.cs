using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.WorkReport
{
	[Serializable()]
	public class WorkReportResourceMap : ClassMapping<WorkReportResourceRecord>
	{	
		public WorkReportResourceMap()
		{
			Table("`STWorkReportResource`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STWorkReportResource_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidWorkReport, map => { map.Column("`OID_WORK_REPORT`"); map.NotNullable(false); });
			Property(x => x.OidResource, map => { map.Column("`OID_RESOURCE`"); map.NotNullable(false); });
			Property(x => x.EntityType, map => { map.Column("`ENTITY_TYPE`"); map.NotNullable(false); });
			Property(x => x.Amount, map => { map.Column("`AMOUNT`"); map.NotNullable(false); });
			Property(x => x.Cost, map => { map.Column("`COST`"); map.NotNullable(false);  });
			Property(x => x.From, map => { map.Column("`FROM`"); map.NotNullable(false); });
			Property(x => x.Till, map => { map.Column("`TILL`"); map.NotNullable(false); });
			Property(x => x.Hours, map => { map.Column("`HOURS`"); map.NotNullable(false); });
			Property(x => x.ExtraCost, map => { map.Column("`EXTRA_COST`"); map.NotNullable(false); });
            Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
