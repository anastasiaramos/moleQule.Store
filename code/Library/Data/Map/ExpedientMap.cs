using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class ExpedientMap : ClassMapping<ExpedientRecord>
    {
        public ExpedientMap()
        {
			Table("`STExpedient`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STExpedient_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidProveedor, map => { map.Column("`OID_PROVEEDOR`"); map.NotNullable(false); });
			Property(x => x.OidNaviera, map => { map.Column("`OID_NAVIERA`"); map.NotNullable(false); });
			Property(x => x.OidTransOrigen, map => { map.Column("`OID_TRANS_ORIGEN`"); map.NotNullable(false); });
			Property(x => x.OidTransDestino, map => { map.Column("`OID_TRANS_DESTINO`"); map.NotNullable(false); });
			Property(x => x.OidDespachante, map => { map.Column("`OID_DESPACHANTE`"); map.NotNullable(false); });
			Property(x => x.OidFacturaPro, map => { map.Column("`OID_FACTURA_PRO`"); map.NotNullable(false); });
			Property(x => x.OidFacturaNav, map => { map.Column("`OID_FACTURA_NAV`"); map.NotNullable(false); });
			Property(x => x.OidFacturaDes, map => { map.Column("`OID_FACTURA_DES`"); map.NotNullable(false); });
			Property(x => x.OidFacturaTor, map => { map.Column("`OID_FACTURA_TOR`"); map.NotNullable(false); });
			Property(x => x.OidFacturaTde, map => { map.Column("`OID_FACTURA_TDE`"); map.NotNullable(false); });
			Property(x => x.TipoExpediente, map => { map.Column("`TIPO_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PuertoOrigen, map => { map.Column("`PUERTO_ORIGEN`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PuertoDestino, map => { map.Column("`PUERTO_DESTINO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Buque, map => { map.Column("`BUQUE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Ano, map => { map.Column("`ANO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaPedido, map => { map.Column("`FECHA_PEDIDO`"); map.NotNullable(false);  });
			Property(x => x.FechaFacProveedor, map => { map.Column("`FECHA_FAC_PROVEEDOR`"); map.NotNullable(false); });
			Property(x => x.FechaEmbarque, map => { map.Column("`FECHA_EMBARQUE`"); map.NotNullable(false); });
			Property(x => x.FechaLlegadaMuelle, map => { map.Column("`FECHA_LLEGADA_MUELLE`"); map.NotNullable(false); });
			Property(x => x.FechaDespachoDestino, map => { map.Column("`FECHA_DESPACHO_DESTINO`"); map.NotNullable(false); });
			Property(x => x.FechaSalidaMuelle, map => { map.Column("`FECHA_SALIDA_MUELLE`"); map.NotNullable(false); });
			Property(x => x.FechaRegresoMuelle, map => { map.Column("`FECHA_REGRESO_MUELLE`"); map.NotNullable(false); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FleteNeto, map => { map.Column("`FLETE_NETO`"); map.NotNullable(false); });
			Property(x => x.Baf, map => { map.Column("`BAF`"); map.NotNullable(false); });
			Property(x => x.Teus20, map => { map.Column("`TEUS20`"); map.NotNullable(false); });
			Property(x => x.Teus40, map => { map.Column("`TEUS40`"); map.NotNullable(false); });
			Property(x => x.T3Origen, map => { map.Column("`T3_ORIGEN`"); map.NotNullable(false); });
			Property(x => x.T3Destino, map => { map.Column("`T3_DESTINO`"); map.NotNullable(false); });
			Property(x => x.ThcOrigen, map => { map.Column("`THC_ORIGEN`"); map.NotNullable(false); });
			Property(x => x.ThcDestino, map => { map.Column("`THC_DESTINO`"); map.NotNullable(false);  });
			Property(x => x.Isps, map => { map.Column("`ISPS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TotalImpuestos, map => { map.Column("`TOTAL_IMPUESTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstimarDespachante, map => { map.Column("`ESTIMAR_DESPACHANTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstimarNaviera, map => { map.Column("`ESTIMAR_NAVIERA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstimarTorigen, map => { map.Column("`ESTIMAR_TORIGEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstimarTdestino, map => { map.Column("`ESTIMAR_TDESTINO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.GTransFac, map => { map.Column("`G_TRANS_FAC`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GTransTotal, map => { map.Column("`G_TRANS_TOTAL`"); map.NotNullable(false); });
			Property(x => x.GNavFac, map => { map.Column("`G_NAV_FAC`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GNavTotal, map => { map.Column("`G_NAV_TOTAL`"); map.NotNullable(false); });
			Property(x => x.GDespFac, map => { map.Column("`G_DESP_FAC`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GDespTotal, map => { map.Column("`G_DESP_TOTAL`"); map.NotNullable(false); });
			Property(x => x.GDespIgic, map => { map.Column("`G_DESP_IGIC`"); map.NotNullable(false); });
			Property(x => x.GDespIgicServ, map => { map.Column("`G_DESP_IGIC_SERV`"); map.NotNullable(false); });
			Property(x => x.GTransDestFac, map => { map.Column("`G_TRANS_DEST_FAC`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GTransDestTotal, map => { map.Column("`G_TRANS_DEST_TOTAL`"); map.NotNullable(false); });
			Property(x => x.GTransDestIgic, map => { map.Column("`G_TRANS_DEST_IGIC`"); map.NotNullable(false); });
			Property(x => x.Contenedor, map => { map.Column("`CONTENEDOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GProvFac, map => { map.Column("`G_PROV_FAC`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GProvTotal, map => { map.Column("`G_PROV_TOTAL`"); map.NotNullable(false); });
			Property(x => x.Ayuda, map => { map.Column("`AYUDA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoMercancia, map => { map.Column("`TIPO_MERCANCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NombreCliente, map => { map.Column("`NOMBRE_CLIENTE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodigoArticulo, map => { map.Column("`CODIGO_ARTICULO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NombreTransDest, map => { map.Column("`NOMBRE_TRANS_DEST`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NombreTransOrig, map => { map.Column("`NOMBRE_TRANS_ORIG`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Ayudas, map => { map.Column("`AYUDAS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); });
        }
    }
}