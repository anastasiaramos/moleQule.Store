using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class KitMap : ClassMapping<KitRecord>
	{	
		public KitMap()
		{
			Table("`STKit`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STKit_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidKit, map => { map.Column("`OID_KIT`"); map.NotNullable(false); });
			Property(x => x.OidProduct, map => { map.Column("`OID_PRODUCT`"); map.NotNullable(false); });
			Property(x => x.Amount, map => { map.Column("`AMOUNT`"); map.NotNullable(false); });
		}
	}
}
