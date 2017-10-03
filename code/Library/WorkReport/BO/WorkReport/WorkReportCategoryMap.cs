using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.WorkReport
{
	[Serializable()]
	public class WorkReportCategoryMap : ClassMapping<WorkReportCategoryRecord>
	{	
		public WorkReportCategoryMap()
		{
			Table("`STWorkReportCategory`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STWorkReportCategory_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Name, map => { map.Column("`NAME`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
