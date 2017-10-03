using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class SupplierProductMap : ClassMapping<ProductoProveedorRecord>
    {
        public SupplierProductMap()
        {
            Table("`STSupplier_Product`");
            Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STSupplier_Product_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAcreedor, map => { map.Column("`OID_ACREEDOR`"); map.NotNullable(false); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); });
			Property(x => x.TipoAcreedor, map => { map.Column("`TIPO_ACREEDOR`"); map.NotNullable(false); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); });
			Property(x => x.FacturacionBulto, map => { map.Column("`FACTURACION_BULTO`"); map.NotNullable(false); });
			Property(x => x.CodigoProductoAcreedor, map => { map.Column("`CODIGO_PRODUCTO_ACREEDOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); });
			Property(x => x.TipoDescuento, map => { map.Column("`TIPO_DESCUENTO`"); map.NotNullable(false); });
			Property(x => x.Automatico, map => { map.Column("`AUTOMATICO`"); map.NotNullable(false); });
		}
    }
}