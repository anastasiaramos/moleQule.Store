using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class LineaFomentoMap : ClassMapping<LineaFomentoRecord>
    {
        public LineaFomentoMap()
        {
            Table("`LineaFomento`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`LineaFomento_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidPartida, map => { map.Column("`OID_PARTIDA`"); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.OidNaviera, map => { map.Column("`OID_NAVIERA`"); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IdSolicitud, map => { map.Column("`ID_SOLICITUD`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.IdEnvio, map => { map.Column("`ID_ENVIO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Conocimiento, map => { map.Column("`CONOCIMIENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaConocimiento, map => { map.Column("`FECHA_CONOCIMIENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Dua, map => { map.Column("`DUA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Kilos, map => { map.Column("`KILOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FleteNeto, map => { map.Column("`FLETE_NETO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Baf, map => { map.Column("`BAF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Teus20, map => { map.Column("`TEUS20`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Teus40, map => { map.Column("`TEUS40`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.T3Origen, map => { map.Column("`T3_ORIGEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.T3Destino, map => { map.Column("`T3_DESTINO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ThcOrigen, map => { map.Column("`THC_ORIGEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ThcDestino, map => { map.Column("`THC_DESTINO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Isps, map => { map.Column("`ISPS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}