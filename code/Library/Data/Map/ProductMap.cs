using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class ProductMap : ClassMapping<ProductRecord>
    {
        public ProductMap()
        {
            Table("`STProduct`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STProduct_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAyuda, map => { map.Column("`OID_AYUDA`"); map.NotNullable(false); });
			Property(x => x.OidFamilia, map => { map.Column("`OID_FAMILIA`"); map.NotNullable(false); });
			Property(x => x.OidImpuestoCompra, map => { map.Column("`OID_IMPUESTO_COMPRA`"); map.NotNullable(false); });
			Property(x => x.OidImpuestoVenta, map => { map.Column("`OID_IMPUESTO_VENTA`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
			Property(x => x.ExternalCode, map => { map.Column("`EXTERNAL_CODE`"); map.Length(255); });
			Property(x => x.Bulto, map => { map.Column("`BULTO`"); map.NotNullable(false); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PrecioCompra, map => { map.Column("`PRECIO_COMPRA`"); map.NotNullable(false); });
			Property(x => x.PrecioVenta, map => { map.Column("`PRECIO_VENTA`"); map.NotNullable(false); });
			Property(x => x.AyudaKilo, map => { map.Column("`AYUDA_KILO`"); map.NotNullable(false); });
			Property(x => x.CuentaContableCompra, map => { map.Column("`CUENTA_CONTABLE_COMPRA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContableVenta, map => { map.Column("`CUENTA_CONTABLE_VENTA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodigoAduanero, map => { map.Column("`CODIGO_ADUANERO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Unitario, map => { map.Column("`UNITARIO`"); map.NotNullable(false); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.KilosBulto, map => { map.Column("`KILOS_BULTO`"); map.NotNullable(false); });
			Property(x => x.StockMinimo, map => { map.Column("`STOCK_MINIMO`"); map.NotNullable(false); });
			Property(x => x.TipoVenta, map => { map.Column("`TIPO_VENTA`"); map.NotNullable(false); });
			Property(x => x.AvisarStock, map => { map.Column("`AVISAR_STOCK`"); map.NotNullable(false); });
			Property(x => x.BeneficioCero, map => { map.Column("`BENEFICIO_CERO`"); map.NotNullable(false); });
			Property(x => x.AvisarBeneficioMinimo, map => { map.Column("`AVISAR_BENEFICIO_MINIMO`"); map.NotNullable(false); });
			Property(x => x.PBeneficioMinimo, map => { map.Column("`P_BENEFICIO_MINIMO`"); map.NotNullable(false); });
			Property(x => x.IsKit, map => { map.Column("`IS_KIT`"); map.NotNullable(false); });
            Property(x => x.NoStockSale, map => { map.Column("`NO_STOCK_SALE`"); map.NotNullable(false); });
        }
    }
}