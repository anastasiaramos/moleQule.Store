using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class RoutePriceMap : ClassMapping<PrecioTrayectoRecord>
    {
        public RoutePriceMap()
        {
            Table("`STRoutePrice`");
            Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STRoutePrice_OID_seq`" })); map.Column("`OID`"); });

			Property(x => x.PuertoDestino, map => { map.Column("`PUERTO_DESTINO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PuertoOrigen, map => { map.Column("`PUERTO_ORIGEN`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); });
			Property(x => x.OidNaviera, map => { map.Column("`OID_NAVIERA`"); map.NotNullable(false); });
        }
    }
}