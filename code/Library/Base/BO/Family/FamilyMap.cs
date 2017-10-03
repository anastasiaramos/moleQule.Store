using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class FamilyMap : ClassMapping<FamilyRecord>
    {
		public FamilyMap()
        {
            Table("`STFamily`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STFamily_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContableCompra, map => { map.Column("`CUENTA_CONTABLE_COMPRA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContableVenta, map => { map.Column("`CUENTA_CONTABLE_VENTA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.AvisarBeneficioMinimo, map => { map.Column("`AVISAR_BENEFICIO_MINIMO`"); map.NotNullable(false); });
			Property(x => x.PBeneficioMinimo, map => { map.Column("`P_BENEFICIO_MINIMO`"); map.NotNullable(false); });
        }
    }
}

