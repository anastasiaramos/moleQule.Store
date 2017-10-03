using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class DestinationPriceMap : ClassMapping<PrecioDestinoRecord>
    {
        public DestinationPriceMap()
        {
            Table("`STDestinationPrice`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STDestinationPrice_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.OidTransportista, map => { map.Column("`OID_TRANSPORTISTA`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.NumeroCliente, map => { map.Column("`NUMERO_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CodigoCliente, map => { map.Column("`CODIGO_CLIENTE`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.NombreCliente, map => { map.Column("`NOMBRE_CLIENTE`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.Puerto, map => { map.Column("`PUERTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}