using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class PayrollBatchMap : ClassMapping<PayrollBatchRecord>
	{
		public PayrollBatchMap()
		{
			Table("`STPayrollBatch`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STPayrollBatch_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Irpf, map => { map.Column("`IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.SeguroEmpresa, map => { map.Column("`SEGURO_EMPRESA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.SeguroPersonal, map => { map.Column("`SEGURO_PERSONAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrevisionPago, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.BaseIrpf, map => { map.Column("`BASE_IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuentos, map => { map.Column("`DESCUENTOS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}