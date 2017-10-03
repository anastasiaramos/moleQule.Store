using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class CabezaMap : ClassMapping<CabezaRecord>
    {
        public CabezaMap()
        {
            Table("`Cabeza`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`Cabeza_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidProductoExpediente, map => { map.Column("`OID_BATCH`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
			Property(x => x.Identificador, map => { map.Column("`IDENTIFICADOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Raza, map => { map.Column("`RAZA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Sexo, map => { map.Column("`SEXO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

