using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class PayrollMap : ClassMapping<PayrollRecord>
    {
        public PayrollMap()
        {
			Table("`STPayroll`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STPayroll_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidRemesa, map => { map.Column("`OID_REMESA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidTipo, map => { map.Column("`OID_TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidEmpleado, map => { map.Column("`OID_EMPLEADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Bruto, map => { map.Column("`BRUTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.BaseIrpf, map => { map.Column("`BASE_IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Neto, map => { map.Column("`NETO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PIrpf, map => { map.Column("`P_IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Seguro, map => { map.Column("`SEGURO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuentos, map => { map.Column("`DESCUENTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrevisionPago, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}