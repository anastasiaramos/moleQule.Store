using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
    public class FamilySerieMap : ClassMapping<FamilySerieRecord>
	{
		public FamilySerieMap()
		{
			Table("`STFamilySerie`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STFamilySerie_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidFamilia, map => { map.Column("`OID_FAMILIA`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}