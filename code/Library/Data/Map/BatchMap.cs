using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{ 
    [Serializable()]
    public class BatchMap : ClassMapping<BatchRecord>
    {
        public BatchMap()
        {
			Table("`STBatch`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STBatch_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); });
			Property(x => x.TipoMercancia, map => { map.Column("`TIPO_MERCANCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.BultosIniciales, map => { map.Column("`BULTOS_INICIALES`"); map.NotNullable(false); });
			Property(x => x.KilosIniciales, map => { map.Column("`KILOS_INICIALES`"); map.NotNullable(false); });
			Property(x => x.PrecioVentaKilo, map => { map.Column("`PRECIO_VENTA_KILO`"); map.NotNullable(false); });
			Property(x => x.BeneficioKilo, map => { map.Column("`BENEFICIO_KILO`"); map.NotNullable(false); });
			Property(x => x.CosteKilo, map => { map.Column("`COSTE_KILO`"); map.NotNullable(false); });
			Property(x => x.ReaFechaCobro, map => { map.Column("`REA_FECHA_COBRO`"); map.NotNullable(false); });
			Property(x => x.ReaCobrada, map => { map.Column("`REA_COBRADA`"); map.NotNullable(false); });
			Property(x => x.AyudaRecibidaKilo, map => { map.Column("`AYUDA_RECIBIDA_KILO`"); map.NotNullable(false); });
			Property(x => x.OidProveedor, map => { map.Column("`OID_PROVEEDOR`"); map.NotNullable(false); });
			Property(x => x.PrecioCompraKilo, map => { map.Column("`PRECIO_COMPRA_KILO`"); map.NotNullable(false); });
			Property(x => x.PrecioVentaBulto, map => { map.Column("`PRECIO_VENTA_BULTO`"); map.NotNullable(false); });
			Property(x => x.OidBatch, map => { map.Column("`OID_BATCH`"); });
			Property(x => x.OidKit, map => { map.Column("`OID_KIT`"); });
			Property(x => x.Proporcion, map => { map.Column("`PROPORCION`"); map.NotNullable(false); });
			Property(x => x.FechaCompra, map => { map.Column("`FECHA_COMPRA`"); map.NotNullable(false); });
			Property(x => x.Ubicacion, map => { map.Column("`UBICACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.GastoKilo, map => { map.Column("`GASTO_KILO`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.NotNullable(false); });
			Property(x => x.Ayuda, map => {	map.Column("`AYUDA`"); map.NotNullable(false); });
        }
    }
}