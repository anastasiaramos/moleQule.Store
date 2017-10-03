using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{	
    [Serializable()]
    public class InventarioAlmacenMap : ClassMapping<InventarioAlmacenRecord>
    {
        public InventarioAlmacenMap()
        {
            Table("`InventarioAlmacen`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`InventarioAlmacen_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.Length(32768); });
            Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}

