using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class InputDeliveryLineMap : ClassMapping<InputDeliveryLineRecord>
    {
        public InputDeliveryLineMap()
        {
			Table("`STDeliveryLine`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STDeliveryLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAlbaran, map => { map.Column("`OID_ALBARAN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidBatch, map => { map.Column("`OID_BATCH`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidKit, map => { map.Column("`OID_KIT`"); map.Length(32768); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CodigoExpediente, map => { map.Column("`CODIGO_EXPEDIENTE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Concepto, map => { map.Column("`CONCEPTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.FacturacionBulto, map => { map.Column("`FACTURACION_BULTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CantidadKilos, map => { map.Column("`CANTIDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CantidadBultos, map => { map.Column("`CANTIDAD_BULTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PIgic, map => { map.Column("`P_IGIC`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Subtotal, map => { map.Column("`SUBTOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Gastos, map => { map.Column("`GASTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidLineaPedido, map => { map.Column("`OID_LINEA_PEDIDO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CodigoProductoProveedor, map => { map.Column("`CODIGO_PRODUCTO_PROVEEDOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PIrpf, map => { map.Column("`P_IRPF`"); map.NotNullable(false); map.Length(32768); }); 
        }
    }
}

