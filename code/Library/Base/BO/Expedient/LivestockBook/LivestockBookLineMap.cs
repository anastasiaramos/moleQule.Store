using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LivestockBookLineMap : ClassMapping<LivestockBookLineRecord>
    {
        public LivestockBookLineMap()
        {
            Table("`STLivestockBookLine`");
            Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STLivestockBookLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidBook, map => { map.Column("`OID_LIBRO`"); });
			Property(x => x.OidBatch, map => { map.Column("`OID_PARTIDA`"); });
			Property(x => x.OidDeliveryLine, map => { map.Column("`OID_CONCEPTO`"); });
            Property(x => x.OidPair, map => { map.Column("`OID_PAIR`"); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
			Property(x => x.Crotal, map => { map.Column("`CROTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); });
			Property(x => x.Sexo, map => { map.Column("`SEXO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Edad, map => { map.Column("`EDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Raza, map => { map.Column("`RAZA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Causa, map => { map.Column("`CAUSA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Procedencia, map => { map.Column("`PROCEDENCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Balance, map => { map.Column("`BALANCE`"); map.NotNullable(false); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); });
            Property(x => x.Explotacion, map => { map.Column("`EXPLOTACION`"); map.NotNullable(false); });
        }
    }
}
