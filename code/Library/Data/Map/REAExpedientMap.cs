using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class REAExpedientMap : ClassMapping<REAExpedientRecord>
    {
        public REAExpedientMap()
        {
            Table("`ExpedienteREA`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`ExpedienteREA_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CodigoAduanero, map => { map.Column("`CODIGO_ADUANERO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ExpedienteRea, map => { map.Column("`EXPEDIENTE_REA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CertificadoRea, map => { map.Column("`CERTIFICADO_REA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NDua, map => { map.Column("`N_DUA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}