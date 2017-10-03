using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LineaInventarioMap : ClassMapping<LineaInventarioRecord>
    {
        public LineaInventarioMap()
        {
            Table("`LineaInventario`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`LineaInventario_OID_seq`" })); map.Column("`OID`"); });

			Property(x => x.OidInventario, map => { map.Column("`OID_INVENTARIO`"); map.Length(32768); });
			Property(x => x.OidLineaalmacen, map => { map.Column("`OID_LINEAALMACEN`"); map.Length(32768); });
			Property(x => x.Concepto, map => { map.Column("`CONCEPTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Cantidad, map => { map.Column("`CANTIDAD`"); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}

