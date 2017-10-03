using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class InputDeliveryMap : ClassMapping<InputDeliveryRecord>
    {
        public InputDeliveryMap()
        {
            Table("`STDelivery`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STDelivery_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); });
			Property(x => x.OidAcreedor, map => { map.Column("`OID_ACREEDOR`"); map.NotNullable(false); });
			Property(x => x.TipoAcreedor, map => { map.Column("`TIPO_ACREEDOR`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.Ano, map => { map.Column("`ANO`"); map.NotNullable(false); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); });
			Property(x => x.FechaRegistro, map => { map.Column("`FECHA_REGISTRO`"); map.NotNullable(false); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false);  });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false);  });
			Property(x => x.PrevisionPago, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); });
			Property(x => x.PIrpf, map => { map.Column("`P_IRPF`"); map.NotNullable(false); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); });
			Property(x => x.Descuento, map => { map.Column("`DESCUENTO`"); map.NotNullable(false); });
			Property(x => x.BaseImponible, map => { map.Column("`BASE_IMPONIBLE`"); map.NotNullable(false); });
			Property(x => x.Igic, map => { map.Column("`IGIC`"); map.NotNullable(false); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); });
			Property(x => x.CuentaBancaria, map => { map.Column("`CUENTA_BANCARIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Nota, map => { map.Column("`NOTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Contado, map => { map.Column("`CONTADO`"); map.Length(32768); });
			Property(x => x.Rectificativo, map => { map.Column("`RECTIFICATIVO`"); map.NotNullable(false); });
			Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.NotNullable(false); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); });
			Property(x => x.Irpf, map => { map.Column("`IRPF`"); map.NotNullable(false); });
        }
    }
}