using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class PaymentMap : ClassMapping<PaymentRecord>
    {
        public PaymentMap()
        {
			Table("`STPayment`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STPayment_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAgente, map => { map.Column("`OID_AGENTE`"); map.NotNullable(false); });
			Property(x => x.OidTarjetaCredito, map => { map.Column("`OID_TARJETA_CREDITO`"); map.NotNullable(false); });
			Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); map.NotNullable(false); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); });
			Property(x => x.TipoAgente, map => { map.Column("`TIPO_AGENTE`"); });
			Property(x => x.IdPago, map => { map.Column("`ID_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); });
			Property(x => x.Importe, map => { map.Column("`IMPORTE`"); map.NotNullable(false); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); });
			Property(x => x.Vencimiento, map => { map.Column("`VENCIMIENTO`"); map.NotNullable(false); });
			Property(x => x.GastosBancarios, map => { map.Column("`GASTOS_BANCARIOS`"); map.NotNullable(false); });
			Property(x => x.IdMovContable, map => { map.Column("`ID_MOV_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); });
			Property(x => x.OidRoot, map => { map.Column("`OID_ROOT`"); map.NotNullable(false); });
            Property(x => x.OidLink, map => { map.Column("`OID_LINK`"); map.NotNullable(false); });
			Property(x => x.EstadoPago, map => { map.Column("`ESTADO_PAGO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}