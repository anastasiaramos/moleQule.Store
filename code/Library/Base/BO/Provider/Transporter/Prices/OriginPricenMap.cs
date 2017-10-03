using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class OriginPricenMap : ClassMapping<PrecioOrigenRecord>
    {
        public OriginPricenMap()
        {
            Table("`STOriginPrice`");
            Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STOriginPrice_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidTransportista, map => { map.Column("`OID_TRANSPORTISTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidProveedor, map => { map.Column("`OID_PROVEEDOR`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Proveedor, map => { map.Column("`PROVEEDOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Puerto, map => { map.Column("`PUERTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); map.Length(32768); });
		}
    }
}