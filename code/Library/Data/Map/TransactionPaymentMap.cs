using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{	
    [Serializable()]
    public class TransactionPaymentMap : ClassMapping<TransactionPaymentRecord>
    {
        public TransactionPaymentMap()
        {
			Table("`STTransactionPayment`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STTransactionPayment_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.OidPago, map => { map.Column("`OID_PAGO`"); });
            Property(x => x.OidOperacion, map => { map.Column("`OID_OPERACION`"); map.NotNullable(false); });
            Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); });
			Property(x => x.TipoPago, map => { map.Column("`TIPO_PAGO`"); map.NotNullable(false); });
            Property(x => x.TipoAgente, map => { map.Column("`TIPO_AGENTE`"); });
            Property(x => x.Cantidad, map => { map.Column("`CANTIDAD`"); map.NotNullable(false); });
        }
    }
}