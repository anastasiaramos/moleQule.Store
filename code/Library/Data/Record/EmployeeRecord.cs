using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Store.Data
{
	[Serializable()]
	public class EmployeeRecord : ProviderBaseRecord
	{
		#region Attributes

		private string _apellidos = string.Empty;
		private string _nivel_estudios = string.Empty;
		private long _perfil;
		private string _foto = string.Empty;
		private DateTime _inicio_contrato;
		private DateTime _fin_contrato;
		private decimal _sueldo_bruto;
		private decimal _sueldo_neto;
		private decimal _base_irpf;
		private decimal _descuentos;
		private decimal _seguro;
		private long _oid_crew;
        private long _payroll_method;

		#endregion

		#region Properties

		public virtual string Apellidos { get { return _apellidos; } set { _apellidos = value; } }
		public virtual string Foto { get { return _foto; } set { _foto = value; } }
		public virtual long Perfil { get { return _perfil; } set { _perfil = value; } }
		public virtual DateTime InicioContrato { get { return _inicio_contrato; } set { _inicio_contrato = value; } }
		public virtual DateTime FinContrato { get { return _fin_contrato; } set { _fin_contrato = value; } }
		public virtual string NivelEstudios { get { return _nivel_estudios; } set { _nivel_estudios = value; } }
		public virtual Decimal SueldoBruto { get { return _sueldo_bruto; } set { _sueldo_bruto = value; } }
		public virtual Decimal SueldoNeto { get { return _sueldo_neto; } set { _sueldo_neto = value; } }
		public virtual Decimal BaseIrpf { get { return _base_irpf; } set { _base_irpf = value; } }
		public virtual Decimal Descuentos { get { return _descuentos; } set { _descuentos = value; } }
		public virtual Decimal Seguro { get { return _seguro; } set { _seguro = value; } }
		public virtual long OidCrew { get { return _oid_crew; } set { _oid_crew = value; } }
        public virtual long PayrollMethod { get { return _payroll_method; } set { _payroll_method = value; } }

		#endregion

		#region Business Methods

		public EmployeeRecord() { }

        public override void CopyValues(IDataReader source)
        {
            if (source == null) return;

            base.CopyValues(source);

            _apellidos = Format.DataReader.GetString(source, "APELLIDOS");
            _nivel_estudios = Format.DataReader.GetString(source, "NIVEL_ESTUDIOS");
            _perfil = Format.DataReader.GetInt64(source, "PERFIL");
            _foto = Format.DataReader.GetString(source, "FOTO");
            _inicio_contrato = Format.DataReader.GetDateTime(source, "INICIO_CONTRATO");
            _fin_contrato = Format.DataReader.GetDateTime(source, "FIN_CONTRATO");
            _sueldo_bruto = Format.DataReader.GetDecimal(source, "SUELDO_BRUTO");
            _sueldo_neto = Format.DataReader.GetDecimal(source, "SUELDO_NETO");
            _base_irpf = Format.DataReader.GetDecimal(source, "BASE_IRPF");
            _descuentos = Format.DataReader.GetDecimal(source, "DESCUENTOS");
            _seguro = Format.DataReader.GetDecimal(source, "SEGURO");
			_oid_crew = Format.DataReader.GetInt64(source, "OID_CREW");
            _payroll_method = Format.DataReader.GetInt64(source, "PAYROLL_METHOD");
        }
        public virtual void CopyValues(EmployeeRecord source)
        {
            if (source == null) return;

            base.CopyValues(source);

            _apellidos = source.Apellidos;
            _nivel_estudios = source.NivelEstudios;
            _perfil = source.Perfil;
            _inicio_contrato = source.InicioContrato;
            _fin_contrato = source.FinContrato;
            _sueldo_bruto = source.SueldoBruto;
            _sueldo_neto = source.SueldoNeto;
            _base_irpf = source.BaseIrpf;
            _descuentos = source.Descuentos;
            _seguro = source.Seguro;
			_oid_crew = source.OidCrew;
            _payroll_method = source.PayrollMethod;
        }

		#endregion
	}
}