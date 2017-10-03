using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class CustomAgentPortMap : ClassMapping<PuertoDespachanteRecord>
	{
		public CustomAgentPortMap()
		{
            Table("`STCustomAgent_Port`");
			Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STCustomAgent_Port_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidPuerto, map => { map.Column("`OID_PUERTO`"); map.NotNullable(false); });
			Property(x => x.OidDespachante, map => { map.Column("`OID_DESPACHANTE`"); map.NotNullable(false); });
		}
	}
}