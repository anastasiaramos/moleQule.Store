using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class TipoAnimalMap : ClassMapping<TipoAnimalRecord>
	{
		public TipoAnimalMap()
		{
			Table("`TIPO_ANIMAL`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`TIPO_ANIMAL_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.NotNullable(false); map.Length(255); });
		}
	}

}

