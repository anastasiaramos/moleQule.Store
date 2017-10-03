using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class TipoGanadoMap : ClassMapping<TipoGanadoRecord>
	{
		public TipoGanadoMap()
		{
            Table("`TIPOGANADO`");
            Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`TIPOGANADO_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.NotNullable(false); map.Length(255); });
		}
	}
}

