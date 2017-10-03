using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class SupplierMap : ClassMapping<SupplierRecord>
	{
		public SupplierMap()
		{
			Table("`STSupplier`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STSupplier_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
            Property(x => x.Identificador, map => { map.Column("`ID`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.TipoId, map => { map.Column("`TIPO_ID`"); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Alias, map => { map.Column("`ALIAS`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodPostal, map => { map.Column("`COD_POSTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Localidad, map => { map.Column("`LOCALIDAD`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Municipio, map => { map.Column("`MUNICIPIO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Telefono, map => { map.Column("`TELEFONO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Pais, map => { map.Column("`PAIS`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaBancaria, map => { map.Column("`CUENTA_BANCARIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Swift, map => { map.Column("`SWIFT`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Contacto, map => { map.Column("`CONTACTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Email, map => { map.Column("`EMAIL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Direccion, map => { map.Column("`DIRECCION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCuentaBAsociada, map => { map.Column("`OID_CUENTA_BANCARIA_ASOCIADA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); });
			Property(x => x.TipoAcreedor, map => { map.Column("`TIPO`"); map.NotNullable(false); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.OidTarjetaAsociada, map => { map.Column("`OID_TARJETA_ASOCIADA`"); map.NotNullable(false); });
			Property(x => x.PIRPF, map => { map.Column("`P_IRPF`"); map.NotNullable(false); });
		}
	}
}