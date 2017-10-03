using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
    public class ExpenseMap : ClassMapping<ExpenseRecord>
    {
        public ExpenseMap()
        {
			Table("`STExpense`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STExpense_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.OidTipo, map => { map.Column("`OID_TIPO`"); map.NotNullable(false); map.Length(32768); }); 
            Property(x => x.CategoriaGasto, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.OidFactura, map => { map.Column("`OID_FACTURA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidEmpleado, map => { map.Column("`OID_EMPLEADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidNomina, map => { map.Column("`OID_REMESA_NOMINA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Facturas, map => { map.Column("`FACTURAS`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrevisionPago, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidAlbaran, map => { map.Column("`OID_ALBARAN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidConceptoFactura, map => { map.Column("`OID_CONCEPTO_FACTURA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidConceptoAlbaran, map => { map.Column("`OID_CONCEPTO_ALBARAN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
		}
    }
}

