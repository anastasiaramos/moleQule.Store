using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class StockMap : ClassMapping<StockRecord>
	{
		public StockMap()
		{
			Table("`STStock`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STStock_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidPartida, map => { map.Column("`OID_BATCH`"); map.NotNullable(false); }); 
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); }); 
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); }); 
			Property(x => x.OidAlbaran, map => { map.Column("`OID_ALBARAN`"); map.NotNullable(false); }); 
			Property(x => x.OidConceptoAlbaran, map => { map.Column("`OID_CONCEPTO_ALBARAN`"); map.NotNullable(false); }); 
			Property(x => x.OidStock, map => { map.Column("`OID_STOCK`"); }); 
			Property(x => x.OidKit, map => { map.Column("`OID_KIT`"); }); 
			Property(x => x.Concepto, map => { map.Column("`CONCEPTO`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Bultos, map => { map.Column("`BULTOS`"); map.NotNullable(false); }); 
			Property(x => x.Kilos, map => { map.Column("`KILOS`"); map.NotNullable(false); }); 
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); }); 
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); }); 
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); }); 
			Property(x => x.Inicial, map => { map.Column("`INICIAL`"); map.NotNullable(false); }); 
			Property(x => x.OidLineaPedido, map => {  map.Column("`OID_LINEA_PEDIDO`"); map.NotNullable(false); }); 
			Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.NotNullable(false); }); 
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); }); 
			Property(x => x.OidEnlace, map => { map.Column("`OID_ENLACE`"); map.NotNullable(false); });
		}
	}
}