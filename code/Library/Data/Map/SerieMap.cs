using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class SerieMap : ClassMapping<SerieRecord>
	{
		public SerieMap()
		{
			Table("`STSerie`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STSerie_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Identificador, map => { map.Column("`IDENTIFICADOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Cabecera, map => { map.Column("`CABECERA`"); map.NotNullable(false); map.Length(512); });
			Property(x => x.Resumen, map => { map.Column("`RESUMEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}