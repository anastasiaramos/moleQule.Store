using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class WorkReportMap : ClassMapping<WorkReportRecord>
	{	
		public WorkReportMap()
		{
			Table("`STWorkReport`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STWorkReport_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidOwner, map => { map.Column("`OID_OWNER`"); map.NotNullable(false); });
			Property(x => x.OidExpedient, map => { map.Column("`OID_EXPEDIENT`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); });
			Property(x => x.Code, map => { map.Column("`CODE`"); map.NotNullable(false); map.Length(255);  });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(false); });
			Property(x => x.Date, map => { map.Column("`DATE`"); map.NotNullable(false); });
			Property(x => x.From, map => { map.Column("`FROM`"); map.NotNullable(false); });
			Property(x => x.Till, map => { map.Column("`TILL`"); map.NotNullable(false); });
			Property(x => x.Hours, map => { map.Column("`HOURS`"); map.NotNullable(false); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); });
			Property(x => x.Category, map => { map.Column("`CATEGORY`"); map.NotNullable(false); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}