using System;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;  
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class PayrollBase
	{
		#region Business Methods

        private PayrollRecord _record = new PayrollRecord();

		//NO ENLAZADOS
		internal string _usuario = string.Empty;
		internal string _empleado = string.Empty;
		internal long _oid_pago;
		internal string _id_pago = string.Empty;
		internal string _id_remesa = string.Empty;
		internal string _id_expediente = string.Empty; 
		internal DateTime _fecha_pago;
		internal bool _pagado = false;
		internal long _medio_pago;

		internal string _vinculado = Resources.Labels.SET_PAGO;
		internal Decimal _asignado;
		internal Decimal _acumulado;
		internal Decimal _pendiente;
		internal decimal _pendiente_asignar = 0;
		internal Decimal _total_pagado;
		internal DateTime _fecha_asignacion;

		#endregion

		#region Properties

        public PayrollRecord Record { get { return _record; } set { _record = value; } }

		//NO ENLAZADAS
		internal EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado= (long)value; } }
		internal string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal EMedioPago EMedioPago { get { return (EMedioPago)_medio_pago; } }
		internal string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
        internal Decimal IRPF { get { return Decimal.Round(_record.BaseIrpf * _record.PIrpf / 100, 2); } }
        internal bool Pagado { get { return (_total_pagado >= _record.Neto); } }
        internal long DiasTranscurridos
        {
            get
            {
                if (Pagado)
                    return (_fecha_pago != DateTime.MinValue) ? _fecha_pago.Subtract(_record.Fecha).Days : 0;
                else
                    return DateTime.Today.Subtract(_record.Fecha).Days;
            }
        }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            int tipo_query = Format.DataReader.GetInt32(source, "TIPO_QUERY");

			switch ((Payroll.ETipoQuery)tipo_query)
            {
				case Payroll.ETipoQuery.GENERAL:
                    {
                        _record.CopyValues(source);

                        _usuario = Format.DataReader.GetString(source, "USUARIO");
                        _fecha_pago = Format.DataReader.GetDateTime(source, "FECHA_PAGO");
                        _medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
                        _oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
                        _id_pago = Format.DataReader.GetString(source, "ID_PAGO");
                        _id_expediente = Format.DataReader.GetString(source, "ID_EXPEDIENTE");
                        _id_remesa = Format.DataReader.GetString(source, "ID_REMESA");
                        _empleado = Format.DataReader.GetString(source, "EMPLEADO");
                        _asignado = Format.DataReader.GetDecimal(source, "ASIGNADO");
                        _pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
						_pendiente_asignar = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
                        _total_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");

                        switch (_record.Estado)
                        {
                            case (long)EEstado.Contabilizado:
                            case (long)EEstado.Anulado:
                                break;

                            default:
                                _record.Estado = (_pendiente != 0) ? _record.Estado : (long)EEstado.Pagado;
                                break;
                        }

                        _vinculado = (_asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
                    }
                    break;
            }
		}
		internal void CopyValues(Payroll source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_remesa = source.IDRemesa;
			_usuario = source.Usuario;
			_fecha_pago = source.FechaPago;
			_id_expediente = source.IDExpediente;
			_empleado = source.Empleado;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
			_medio_pago = source.MedioPago;
		}
		internal void CopyValues(NominaInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_remesa = source.IDRemesa;
			_usuario = source.Usuario;
			_fecha_pago = source.FechaPago;
			_id_expediente = source.IDExpediente;
			_empleado = source.Empleado;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
			_medio_pago = source.MedioPago;
		}
		
		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class Payroll : BusinessBaseEx<Payroll>, ITransactionPayment
	{
		#region ITransactionPayment

		public virtual decimal Total { get { return Neto; } set { Neto = value; } }
		public virtual decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
		public virtual decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
		public virtual decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public virtual decimal PendienteAsignar { get { return Math.Min(_base._pendiente_asignar, _base._pendiente); } set { _base._pendiente_asignar = value; } }
		public virtual decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
		public virtual string FechaAsignacion { get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; } set { _base._fecha_asignacion = DateTime.Parse(value); } }
		public virtual string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
		public virtual string NFactura { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
		public virtual string Acreedor { get { return Empleado; } set { Empleado = value; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return ETipoAcreedor.Empleado; }  set {} }
        public virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		
		#endregion

	    #region Attributes

		public PayrollBase _base = new PayrollBase();

        #endregion

        #region Properties

        public override long Oid
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Oid;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                // CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }

		public virtual long OidRemesa
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidRemesa;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.OidRemesa.Equals(value))
				{
					_base.Record.OidRemesa = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.OidUsuario.Equals(value))
				{
					_base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidTipo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTipo;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidTipo.Equals(value))
				{
					_base.Record.OidTipo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidEmpleado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidEmpleado;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidEmpleado.Equals(value))
				{
					_base.Record.OidEmpleado = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidExpediente
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpediente;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidExpediente.Equals(value))
				{
					_base.Record.OidExpediente = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}		
		public virtual string Descripcion
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descripcion;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Descripcion.Equals(value))
				{
					_base.Record.Descripcion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Bruto
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Bruto;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Bruto.Equals(value))
				{
					_base.Record.Bruto = value;
                    BaseIRPF = _base.Record.BaseIrpf == 0 ? value : _base.Record.BaseIrpf;
                    Neto = _base.Record.BaseIrpf - _base.IRPF - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BaseIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BaseIrpf;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.BaseIrpf.Equals(value))
				{
                    _base.Record.BaseIrpf = value;
                    Neto = _base.Record.BaseIrpf - _base.IRPF - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Neto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Neto;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Neto.Equals(value))
				{
					_base.Record.Neto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PIrpf;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.PIrpf.Equals(value))
				{
                    _base.Record.PIrpf = value;
                    Neto = _base.Record.BaseIrpf - _base.IRPF - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Seguro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Seguro;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Seguro.Equals(value))
				{
                    _base.Record.Seguro = value;
                    Neto = _base.Record.BaseIrpf - _base.IRPF - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Descuentos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descuentos;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Descuentos.Equals(value))
				{
                    _base.Record.Descuentos = value;
                    Neto = _base.Record.BaseIrpf - _base.IRPF - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime PrevisionPago
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrevisionPago;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.PrevisionPago.Equals(value))
				{
					_base.Record.PrevisionPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}		
		public virtual bool Pagado
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base._pagado;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base._pagado.Equals(value))
				{
					_base._pagado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		
        //NO ENLAZADAS
		public virtual string IDRemesa { get { return _base._id_remesa; } set { _base._id_remesa = value; } }
		public virtual string Empleado { get { return _base._empleado; } set { _base._empleado = value; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string IDExpediente { get { return _base._id_expediente; } set { _base._id_expediente = value; } }
		public virtual long OidPago { get { return _base._oid_pago; } set { _base._oid_pago = value; } }
		public virtual DateTime FechaPago { get { return _base._fecha_pago; } set { _base._fecha_pago = value; } }
		public virtual string IDPago { get { return _base._id_pago; } set { _base._id_pago = value; } }
		public virtual long MedioPago { get { return _base._medio_pago; } set { _base._medio_pago = value; } }
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }

        #endregion

        #region Business Methods

		public static Payroll CloneAsNew(NominaInfo source)
		{
			Payroll clon = Payroll.New();
			clon._base.CopyValues(source);

			clon.GetNewCode();
			clon.OidUsuario = AppContext.User.Oid;
			clon.Usuario = AppContext.User.Name;
			clon.EEstado = EEstado.Abierto;
			clon.Fecha = DateTime.Today;

			clon.OidPago = 0;
			clon.IDPago = string.Empty;
			clon.FechaPago = DateTime.MinValue;
						
			clon.MarkNew();

			return clon;
		}
        
		public virtual void CopyFrom(Payroll source) { _base.CopyValues(source); }
		public virtual void CopyFrom(PayrollBatch source, EmployeeInfo employee)
		{
			if (source == null) return;

			OidRemesa = source.Oid;
			PrevisionPago = source.PrevisionPago;
			Fecha = source.Fecha;
			
            OidEmpleado = (employee != null) ? employee.Oid : 0;
			Empleado = (employee != null) ? employee.NombreCompleto : string.Empty;

            Observaciones = String.Format(Resources.Messages.GASTO_ASOCIADO, source.Descripcion);

			TipoGastoInfo tipo = TipoGastoInfo.Get(ModulePrincipal.GetDefaultNominasSetting(), false);
			if (tipo.Oid == 0) throw new iQException(Resources.Messages.NO_TIPOGASTO_NOMINAS);
			OidTipo = tipo.Oid;
			PrevisionPago = Common.EnumFunctions.GetPrevisionPago(tipo.EFormaPago, Fecha, tipo.DiasPago);

            CalculateValues(employee, Fecha);
		}

		public virtual void GetNewCode()
		{
			Serial = SerialInfo.GetNextByYear(typeof(Payroll), Fecha.Year);
            Codigo = Serial.ToString("0000") + "/" + Fecha.ToString("yy");
		}

		public virtual void ChangeEstado(EEstado estado)
		{
			Common.EntityBase.CheckChangeState(EEstado, estado);
			EEstado = estado;
		}
		public static Payroll ChangeEstado(long oid, EEstado estado)
		{
			Payroll item = null;

			try
			{
				item = Payroll.Get(oid, false);

				Common.EntityBase.CheckChangeState(item.EEstado, estado);

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE)))
					throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

				item.BeginEdit();
				item.EEstado = estado;
				item.ApplyEdit();
				item.Save();
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}

        public void CalculateNeto(EPayrollMethod method)
        {
            switch (method)
            {
                case EPayrollMethod.Month:

                    Neto = Bruto - IRPF - Descuentos - Seguro;

                    break;

                case EPayrollMethod.ByHour:

                    Neto = Bruto - IRPF;

                    break;

                case EPayrollMethod.MonthPlusHours:

                    Neto = Bruto - Descuentos;

                    break;

                case EPayrollMethod.MonthPlusExtras:

                    Neto = Bruto - Descuentos;
                    
                    break;
            }
        }
        public virtual void CalculateValues(EmployeeInfo employee, DateTime date)
        {
            OidEmpleado = (employee != null) ? employee.Oid : 0;
            Empleado = (employee != null) ? employee.NombreCompleto : string.Empty;
            Fecha = date;

            if (employee != null)
            {
                switch (employee.EPayrollMethod)
                {
                    case EPayrollMethod.Month:

                        PIRPF = employee.PIRPF;
                        Bruto = employee.SueldoBruto;
                        BaseIRPF = employee.BaseIRPF;
                        Descuentos = employee.Descuentos;
                        Seguro = employee.Seguro;

                        break;

                    case EPayrollMethod.ByHour:

                        Bruto = employee.Seguro + WorkReportResourceList.GetByEmployeeList(employee.Oid, Fecha.Year, Fecha.Month, true, false).GetTotal();
                        PIRPF = employee.PIRPF;
                        Descuentos = employee.Descuentos;
                        Seguro = employee.Seguro;
                        BaseIRPF = Bruto - Descuentos;
    
                        break;

                    case EPayrollMethod.MonthPlusHours:

                        Bruto = employee.SueldoBruto + employee.Seguro + WorkReportResourceList.GetByEmployeeList(employee.Oid, Fecha.Year, Fecha.Month, true, false).GetTotal();
                        PIRPF = employee.PIRPF;      
                        BaseIRPF = Bruto - employee.Descuentos;
                        Descuentos = employee.Descuentos;
                        Seguro = employee.Seguro;

                        break;

                    case EPayrollMethod.MonthPlusExtras:

                        Bruto = employee.SueldoBruto + employee.Seguro + WorkReportResourceList.GetByEmployeeList(employee.Oid, Fecha.Year, Fecha.Month, true, false).GetExtras();
                        PIRPF = employee.PIRPF;
                        BaseIRPF = Bruto - employee.Descuentos;
                        Descuentos = employee.Descuentos;
                        Seguro = employee.Seguro;

                        break;
                }

                CalculateNeto(employee.EPayrollMethod);
                Descripcion = String.Format(Resources.Messages.GASTO_NOMINA, employee.NombreCompleto);
            }
            else
            {
                PIRPF = 0;
                Bruto = 0;
                BaseIRPF = 0;
                Descuentos = 0;
                Seguro = 0;
                Neto = 0;

                Descripcion = string.Empty;
            }            
        }

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.GASTO);
		}		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.GASTO);
		}		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.GASTO);
		}		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.GASTO);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Nomina = Payroll.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.Nomina.Oid = oid;
			conditions.PaymentType = ETipoPago.Nomina;

			TransactionPaymentList pagos = TransactionPaymentList.GetList(conditions, false);

			if (pagos.Count > 0)
				throw new iQException(Resources.Messages.PAGOS_ASOCIADOS);
		}

		#endregion

		#region Common Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public Payroll()
		{
			_base.Record.Oid = (long)(new Random()).Next();
			Fecha = DateTime.Now;
			PrevisionPago = DateTime.Now;
			EEstado = EEstado.Abierto;
			OidTipo = ModulePrincipal.GetDefaultNominasSetting();
            OidUsuario = AppContext.User != null ? AppContext.User.Oid : 0;
            Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
			GetNewCode();
		}

		public virtual NominaInfo GetInfo(bool childs = false)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new NominaInfo(this, childs);
		}
			
		#endregion

		#region Child Factory Methods
	
		private Payroll(Payroll source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private Payroll(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		internal static Payroll NewChild(Payrolls parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Payroll obj  = new Payroll();
			obj.MarkAsChild();

			if (parent != null) parent.SetNextCode(obj);

			return obj;
		}
		internal static Payroll NewChild(Expedient exp) 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Payroll obj = new Payroll();
			obj.MarkAsChild();
			obj.OidExpediente = exp.Oid;
			obj.GetNewCode();

			return obj;
		}
		internal static Payroll NewChild(PayrollBatch nomina)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Payroll obj = new Payroll();
			obj.MarkAsChild();
			obj.CopyFrom(nomina, null);

			return obj;
		}
		internal static Payroll NewChild(PayrollBatch payrollBatch, EmployeeInfo employee)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Payroll obj = new Payroll();
			obj.MarkAsChild();
			obj.CopyFrom(payrollBatch, employee);

			return obj;
		}
		
		internal static Payroll GetChild(Payroll source)
		{
			return new Payroll(source);
		}		
		internal static Payroll GetChild(int sessionCode, IDataReader reader) { return new Payroll(sessionCode, reader); } 
		
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
			
		#endregion

		#region Root Factory Methods

		public static Payroll New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<Payroll>(new CriteriaCs(-1));
		}

		public static Payroll Get(long oid) { return Get(oid, true); }
		public static Payroll Get(long oid, bool childs)
		{
			if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Payroll.SELECT(oid);

			BeginTransaction(criteria.Session);

			return DataPortal.Fetch<Payroll>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}

		public override Payroll Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

				Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}

		#endregion	
			
		#region Common Data Access

		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
		}

		private void Fetch(Payroll source)
		{
			SessionCode = source.SessionCode;
			_base.CopyValues(source);
			MarkOld();
		}

		private void Fetch(IDataReader source)
		{
			_base.CopyValues(source);
			MarkOld();
		}

		internal void Insert(Payrolls parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(_base.Record);
			}
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		internal void Update(Payrolls parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				PayrollRecord obj = Session().Get<PayrollRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Payroll parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PayrollRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}

		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Payrolls parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PayrollRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}
		
		#endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					Payroll.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

				GetNewCode();

				Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					PayrollRecord obj = Session().Get<PayrollRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
				}
			}
		}

		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}

		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((PayrollRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
		}

		#endregion

		#region Child Data Access
	
		internal void Insert(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidExpediente = parent.Oid;

			try
			{	
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidExpediente = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				PayrollRecord obj = Session().Get<PayrollRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PayrollRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		internal void Insert(PayrollBatch parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidRemesa = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(PayrollBatch parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidRemesa = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				PayrollRecord obj = Session().Get<PayrollRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(PayrollBatch parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PayrollRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}
		
		#endregion

        #region SQL

		internal enum ETipoQuery { GENERAL = 0 }

		public new static string SELECT(long oid) { return SELECT(oid, true); }
        internal static string SELECT(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { Nomina = NominaInfo.New(oid) };
            query = SELECT(conditions, lockTable);

            return query;
        }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		internal static string FIELDS(ETipoQuery queryType)
		{
			string query = @"
            SELECT " + (long)queryType + @" AS ""TIPO_QUERY""
                ,NM.*
                ,COALESCE(EM.""APELLIDOS"", '') || ', ' || COALESCE(EM.""NOMBRE"", '') AS ""EMPLEADO""
                ,COALESCE(RN.""CODIGO"", '') AS ""ID_REMESA""
                ,COALESCE(US.""NAME"", '') AS ""USUARIO""
                ,COALESCE(TG.""NOMBRE"",'') AS ""TIPO_GASTO""
                ,COALESCE(EX.""CODIGO"", '') AS ""ID_EXPEDIENTE""
                ,COALESCE(PG.""CODIGO"", '0') AS ""ID_PAGO""
                ,COALESCE(PG.""FECHA"", NULL) AS ""FECHA_PAGO""
                ,COALESCE(PG.""MEDIO_PAGO"", 0) AS ""MEDIO_PAGO""
                ,COALESCE(PG.""OID"", 0) AS ""OID_PAGO""
                ,COALESCE(PR.""TOTAL_PAGADO"", 0) AS ""TOTAL_PAGADO""
                ,COALESCE(NM.""NETO"" - (PR.""TOTAL_PAGADO"" - COALESCE(PF.""ASIGNADO_PAGO"", 0)), NM.""NETO"") AS ""PENDIENTE""
                ,COALESCE(PF.""ASIGNADO_PAGO"", 0) AS ""ASIGNADO""
                ,COALESCE(NM.""NETO"", 0) - COALESCE(PF2.""TOTAL_ASIGNADO"", 0) AS ""PENDIENTE_ASIGNAR""";

			return query;
		}

		private static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @"
			WHERE (NM.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "NM");

			if (conditions.Nomina != null) 
                query += @" 
                AND NM.""OID"" = " + conditions.Nomina.Oid;

            if (conditions.Acreedor != null) 
                query += @" 
                AND NM.""OID_EMPLEADO"" = " + conditions.Acreedor.Oid;

			if (conditions.RemesaNomina != null) 
                query += @" 
                AND NM.""OID_REMESA"" = " + conditions.RemesaNomina.Oid;

			if (conditions.Expedient != null) 
                query += @" 
                AND NM.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;

			if (conditions.Payment != null) if (conditions.Payment.Oid > 0) 
                query += @" 
                AND PG.""OID"" = " + conditions.Payment.Oid;

			if (conditions.FacturaRecibida != null) 
                query += @" 
                AND G.""OID_FACTURA"" = " + conditions.FacturaRecibida.Oid;

			query += " " + conditions.ExtraWhere;

            conditions.ExtraWhere = string.Empty;

            return query;
		}

		private static string SELECT_BASE(QueryConditions conditions)
        {
            string query = 
			FIELDS(ETipoQuery.GENERAL) +
            JOIN(conditions);

			return query;
		}

        internal static string JOIN(QueryConditions conditions)
        {
            string nm = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string rn = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollBatchRecord));
            string em = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.EmployeeRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string tg = nHManager.Instance.GetSQLTable(typeof(ExpenseTypeRecord));

            string tipos = "(" + (long)ETipoPago.Nomina + ")";
            long oid_pago = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

            string query = @"
            FROM " + nm + @" AS NM
            LEFT JOIN " + us + @" AS US ON US.""OID"" = NM.""OID_USUARIO""
            LEFT JOIN " + tg + @" AS TG ON TG.""OID"" = NM.""OID_TIPO""
            LEFT JOIN " + rn + @" AS RN ON RN.""OID"" = NM.""OID_REMESA""
            LEFT JOIN " + em + @" AS EM ON EM.""OID"" = NM.""OID_EMPLEADO""
            LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = NM.""OID_EXPEDIENTE""
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
                                ,PF.""TIPO_PAGO""
                                ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
                        WHERE PF.""TIPO_PAGO"" IN " + tipos + @"
                            AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @" 
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PR ON PR.""OID_OPERACION"" = NM.""OID""";
            
            // IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTA NOMINA
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO_PAGO""
                                ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND PG.""OID"" = " + oid_pago + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF ON PF.""OID_OPERACION"" = NM.""OID""";

            // IMPORTE TOTAL ASIGNADO A ESTA NOMINA POR TODOS LOS PAGOS
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + pf + @" AS PF 
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
            AS PF2 ON PF2.""OID_OPERACION"" = NM.""OID""";

            query += " " + conditions.ExtraJoin;

            conditions.ExtraJoin = string.Empty;

            return query;
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            conditions.ExtraJoin = @"
			LEFT JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""";

			string query = 
			SELECT_BASE(conditions) +
            WHERE(conditions);

            query += Common.EntityBase.LOCK("NM", lockTable);

			return query;
		}

        internal static string SELECT_ASOCIADO(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere +=  @"
				AND ""ASIGNADO"" > 0";

            string query = 
            SELECT(conditions, lockTable);

            return query;
        }

        internal static string SELECT_BY_PAGO(QueryConditions conditions, bool lockTable)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string tipos = "(" + (long)ETipoPago.Nomina + ")";

            conditions.ExtraJoin = @"
				INNER JOIN " + tp + @" AS P_F ON P_F.""OID_OPERACION"" = NM.""OID""
				INNER JOIN " + pa + @" AS PG ON PG.""OID"" = P_F.""OID_PAGO"" AND PG.""TIPO"" IN " + tipos;

            string query =
		    SELECT_BASE(conditions) +
            WHERE(conditions);

            query += Common.EntityBase.LOCK("NM", lockTable);

            return query;
        }

		internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lockTable)
		{
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            conditions.ExtraJoin = @"
			LEFT JOIN " + pg + @" AS PG ON PG.""OID"" = PR.""OID_PAGO""";

            conditions.ExtraWhere += @"
				AND (PR.""TOTAL_PAGADO"" != NM.""NETO"" OR PR.""TOTAL_PAGADO"" IS NULL)
				AND (NM.""PREVISION_PAGO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')
				AND NM.""ESTADO"" != " + (long)EEstado.Anulado;

            string query = 
			SELECT_BASE(conditions) +
            WHERE(conditions);

            query += Common.EntityBase.LOCK("NM", lockTable);

			return query;
		}

        #endregion	
	}
}