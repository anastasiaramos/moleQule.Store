using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;  
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.BankLine;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ExpenseBase
	{
		#region Business Methods

        private ExpenseRecord _record = new ExpenseRecord();

        internal long _oid_pago;

		//NO ENLAZADOS
		internal string _usuario = string.Empty;
		internal string _id_expediente = string.Empty;
		internal string _id_pago = string.Empty;
		internal string _id_remesa_nomina = string.Empty;
		internal string _empleado = string.Empty;
		internal DateTime _fecha_pago;
		internal bool _pagado = false;
		internal long _medio_pago;
		internal long _oid_acreedor;
		internal long _tipo_acreedor;
		internal string _nombre_acreedor = string.Empty;
		internal string _numero_factura = string.Empty;
		internal DateTime _fecha_factura;
		internal Decimal _base_factura;
		internal Decimal _impuestos_factura;
		internal DateTime _prevision_factura;
        internal string _id_mov_banco = string.Empty;
        internal string _id_linea_caja = string.Empty;

		internal string _tipo = string.Empty;
		internal string _vinculado = Resources.Labels.SET_PAGO;
		internal Decimal _asignado;
		internal Decimal _acumulado;
		internal Decimal _pendiente;
		internal Decimal _pendiente_asignar;
		internal Decimal _total_pagado;
		internal Decimal _total_liquidado;
		internal DateTime _fecha_asignacion;

		#endregion

		#region Properties

        public ExpenseRecord Record { get { return _record; } set { _record = value; } }

		//NO ENLAZADAS
		internal virtual EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		internal virtual string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_tipo_acreedor; } set { _tipo_acreedor = (long)value; } }
        internal virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		internal virtual ECategoriaGasto ECategoriaGasto { get { return (ECategoriaGasto)_record.CategoriaGasto; } set { _record.CategoriaGasto = (long)value; } }
		internal virtual string CategoriaGastoLabel { get { return EnumText<ECategoriaGasto>.GetLabel(ECategoriaGasto); } }
		internal virtual EMedioPago EMedioPago { get { return (EMedioPago)_medio_pago; } }
		internal virtual string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual EEstado EEstadoPago { get { return (TotalPteLiquidacion == 0) ? EEstado.Pagado : EEstado.Pendiente; } }
		public virtual string EstadoPagoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstadoPago); } }

		public virtual string FechaAsignacion { get { return (_fecha_asignacion != DateTime.MinValue) ? _fecha_asignacion.ToShortDateString() : "---"; } set { _fecha_asignacion = DateTime.Parse(value); } }
		internal virtual decimal PendienteAsignar { get { return Math.Min(_pendiente, _pendiente_asignar); } set { _pendiente_asignar = value; } }
		public virtual Decimal TotalLiquidado { get { return _total_liquidado; } set { _total_liquidado = value; } }
		public virtual Decimal TotalPteLiquidacion { get { return _record.Total - _total_liquidado; } }
		
		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            int tipo_query = Format.DataReader.GetInt32(source, "TIPO_QUERY");

            switch ((Expense.ETipoQuery)tipo_query)
            {
                case Expense.ETipoQuery.GENERAL:
                    {
                        _record.Oid = Format.DataReader.GetInt64(source, "OID");
                        _record.OidUsuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
                        _record.OidTipo = Format.DataReader.GetInt64(source, "OID_TIPO");
                        _record.OidEmpleado = Format.DataReader.GetInt64(source, "OID_EMPLEADO");
                        _record.OidExpediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
                        _record.OidFactura = Format.DataReader.GetInt64(source, "OID_FACTURA");
                        _record.OidAlbaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
                        _record.OidConceptoFactura = Format.DataReader.GetInt64(source, "OID_CONCEPTO_FACTURA");
                        _record.OidConceptoAlbaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
						_record.OidNomina = Format.DataReader.GetInt64(source, "OID_REMESA_NOMINA");
                        _record.Serial = Format.DataReader.GetInt64(source, "SERIAL");
                        _record.Codigo = Format.DataReader.GetString(source, "CODIGO");
                        _record.Fecha = Format.DataReader.GetDateTime(source, "FECHA");
                        _record.Descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
                        _record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
                        _record.PrevisionPago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
                        _record.Observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
                        _record.Estado = Format.DataReader.GetInt64(source, "ESTADO");
                        _record.CategoriaGasto = Format.DataReader.GetInt64(source, "TIPO");

                        _usuario = Format.DataReader.GetString(source, "USUARIO");
                        _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
                        _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
                        _nombre_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
                        _numero_factura = Format.DataReader.GetString(source, "N_FACTURA");
                        _fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
                        _base_factura = Format.DataReader.GetDecimal(source, "BASE_FACTURA");
                        _fecha_pago = Format.DataReader.GetDateTime(source, "FECHA_PAGO");
                        _medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
                        _oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
                        _id_pago = Format.DataReader.GetString(source, "ID_PAGO");
                        _id_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
						_id_remesa_nomina = Format.DataReader.GetString(source, "ID_REMESA_NOMINA");
                        _empleado = Format.DataReader.GetString(source, "EMPLEADO");
                        _tipo = Format.DataReader.GetString(source, "TIPO_GASTO");
                        _asignado = Format.DataReader.GetDecimal(source, "ASIGNADO_PAGO");
                        _pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
                        _total_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
						_total_liquidado = Format.DataReader.GetDecimal(source, "TOTAL_LIQUIDADO");						
						_pendiente_asignar = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
                        _id_mov_banco = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");
                        _id_linea_caja = Format.DataReader.GetString(source, "ID_LINEA_CAJA");

						_pendiente_asignar = Math.Min(_pendiente, _pendiente_asignar);

                        switch (_record.Estado)
                        {
                            case (long)EEstado.Contabilizado:
                            case (long)EEstado.Anulado:
                                break;

                            default:
                                _record.Estado = (_pendiente != 0) ? (long)EEstado.Abierto : (long)EEstado.Pagado;
                                break;
                        }

                        _vinculado = (_asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
                    }
                    break;

                case Expense.ETipoQuery.FACTURA:
                    {
                        _record.Oid = Format.DataReader.GetInt64(source, "OID");
                        _record.OidUsuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
                        _record.OidExpediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
                        _record.OidFactura = Format.DataReader.GetInt64(source, "OID_FACTURA");
                        _record.Fecha = Format.DataReader.GetDateTime(source, "FECHA");
                        _record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
                        _record.PrevisionPago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
                        _record.CategoriaGasto = Format.DataReader.GetInt64(source, "TIPO");
                        _record.Estado = Format.DataReader.GetInt64(source, "ESTADO");

                        _usuario = Format.DataReader.GetString(source, "USUARIO");
                        _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
                        _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
                        _nombre_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
                        _numero_factura = Format.DataReader.GetString(source, "N_FACTURA");
                        _fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
                        _base_factura = Format.DataReader.GetDecimal(source, "BASE_FACTURA");
                        _id_pago = Format.DataReader.GetString(source, "ID_PAGO");
                        _id_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
						_id_remesa_nomina = Format.DataReader.GetString(source, "ID_REMESA_NOMINA");
                        _empleado = Format.DataReader.GetString(source, "EMPLEADO");
                        _tipo = Format.DataReader.GetString(source, "TIPO_GASTO");
                        _asignado = Format.DataReader.GetDecimal(source, "ASIGNADO_PAGO");
                        _pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
						_pendiente_asignar = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
                        _total_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
                        _id_mov_banco = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");
                        _id_linea_caja = Format.DataReader.GetString(source, "ID_LINEA_CAJA");

						_pendiente_asignar = Math.Min(_pendiente, _pendiente_asignar);

                        switch (_record.Estado)
                        {
                            case (long)EEstado.Contabilizado:
                            case (long)EEstado.Anulado:
                                break;

                            default:
								_record.Estado = (_pendiente != 0) ? (long)EEstado.Abierto : (long)EEstado.Pagado;
                                break;
                        }

                        _vinculado = (_asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
                    }
                    break;
            }
		}
		internal void CopyValues(Expense source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

            _oid_pago = source.OidPago;
			_pagado = source.Pagado;
			_medio_pago = source.MedioPago;

			_usuario = source.Usuario;
			_oid_acreedor = source.OidAcreedor;
			_tipo_acreedor = (long)source.ETipoAcreedor;
			_nombre_acreedor = source.Acreedor;
			_numero_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_base_factura = source.BaseFactura;
			_fecha_pago = source.FechaPago;
			_id_expediente = source.CodigoExpediente;
			_empleado = source.Empleado;
			_tipo = source.Tipo;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
			_medio_pago = source.MedioPago;
            _id_mov_banco = source.IDMovimientoBanco;
            _id_linea_caja = source.IDLineaCaja;
		}
		internal void CopyValues(ExpenseInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_oid_pago = source.OidPago;
			_pagado = source.Pagado;
			_medio_pago = source.MedioPago;

			_usuario = source.Usuario;
			_oid_acreedor = source.OidAcreedor;
			_tipo_acreedor = (long)source.ETipoAcreedor;
			_nombre_acreedor = source.Acreedor;
			_numero_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_base_factura = source.BaseFactura;
			_fecha_pago = source.FechaPago;
			_id_expediente = source.CodigoExpediente;
			_empleado = source.Empleado;
			_tipo = source.Tipo;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
            _medio_pago = source.MedioPago;
            _id_mov_banco = source.IDMovimientoBanco;
            _id_linea_caja = source.IDLineaCaja;
		}
		
		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class Expense : BusinessBaseEx<Expense>, ITransactionPayment, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.Fecha; } set { Fecha = value; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
		public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((Expense)source); }
        public void DifferentYearChecks() { }
        public virtual void DifferentYearTasks(IEntityBase oldItem) { }
        public void SameYearTasks(IEntityBase newItem) { }

        public virtual void IEntityBaseSave(object parent)
        {
            if (parent != null)
            {
                if (parent.GetType() == typeof(Expenses))
                    Insert((Expenses)parent);
                else if (parent.GetType() == typeof(Expedient))
                    Insert((Expedient)parent);
                else
                    Save();
            }
            else
                Save();
        }

        #endregion

		#region ITransactionPayment

		public virtual decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
		public virtual decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
		public virtual string FechaAsignacion { get { return _base.FechaAsignacion; } set { _base.FechaAsignacion = value; } }
		public virtual string NFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
		public virtual decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public virtual decimal PendienteAsignar { get { return _base.PendienteAsignar; } set { _base.PendienteAsignar = value; } }
		public virtual decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }		
		public virtual string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }		
		
		#endregion

	    #region Attributes

		public ExpenseBase _base = new ExpenseBase();

        #endregion

        #region Properties

        public ExpenseBase Base { get { return _base; } }

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
		public virtual long OidRemesaNomina
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidNomina;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidNomina.Equals(value))
				{
					_base.Record.OidNomina = value;
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
        public virtual long OidFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFactura;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidFactura.Equals(value))
                {
                    _base.Record.OidFactura = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual long OidAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlbaran;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidAlbaran.Equals(value))
				{
					_base.Record.OidAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidConceptoFactura
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidConceptoFactura;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidConceptoFactura.Equals(value))
				{
					_base.Record.OidConceptoFactura = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidConceptoAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidConceptoAlbaran;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidConceptoAlbaran.Equals(value))
				{
					_base.Record.OidConceptoAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base._oid_pago;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base._oid_pago.Equals(value))
				{
					_base._oid_pago = value;
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
		public virtual Decimal Total
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Total;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Total.Equals(value))
				{
					_base.Record.Total = value;
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
		public virtual long CategoriaGasto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CategoriaGasto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.CategoriaGasto.Equals(value))
				{
					_base.Record.CategoriaGasto = value;
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
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string Tipo { get { return _base._tipo; } set { _base._tipo = value; PropertyHasChanged(); } }
		public virtual string CodigoExpediente { get { return _base._id_expediente; } set { _base._id_expediente = value; } }
		public virtual DateTime FechaPago { get { return _base._fecha_pago; } set { _base._fecha_pago = value; } }
		public virtual string IDPago { get { return _base._id_pago; } set { _base._id_pago = value; } }
		public virtual string IDRemesaNomina { get { return _base._id_remesa_nomina; } set { _base._id_remesa_nomina = value; } }
		public virtual string Empleado { get { return _base._empleado; } set { _base._empleado = value; } }
		public virtual long MedioPago { get { return _base._medio_pago; } set { _base._medio_pago = value; } }
        public virtual long OidAcreedor { get { return _base._oid_acreedor; } set { _base._oid_acreedor = value; } }
		public virtual string Acreedor { get { return _base._nombre_acreedor; } set { _base._nombre_acreedor = value; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { _base._tipo_acreedor = (long)value; } }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual Decimal BaseFactura { get { return _base._base_factura; } set { _base._base_factura = value; } }
		public virtual Decimal ImpuestosFactura { get { return _base._impuestos_factura; } set { _base._impuestos_factura = value; } }
		public virtual DateTime PrevisionFactura { get { return _base._fecha_factura; } set { _base._prevision_factura = value; } }
		public virtual ECategoriaGasto ECategoriaGasto { get { return _base.ECategoriaGasto; } set { _base.Record.CategoriaGasto = (long)value; } }
		public virtual string CategoriaGastoLabel { get { return _base.CategoriaGastoLabel; } }
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
        public virtual string IDMovimientoBanco { get { return _base._id_mov_banco; } }
        public virtual string IDLineaCaja { get { return _base._id_linea_caja; } }
		public virtual Decimal TotalLiquidado { get { return _base.TotalLiquidado; } set { _base.TotalLiquidado = value; } }

        #endregion

        #region Business Methods

		public static Expense CloneAsNew(ExpenseInfo source)
		{
			Expense clon = Expense.New();
			clon.Base.CopyValues(source);

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

        public virtual Expense CloneAsNew()
        {
            Expense clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.Codigo = (0).ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
            clon.SessionCode = Expense.OpenSession();
            Expense.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

		public virtual void CopyFrom(Expense source) { _base.CopyValues(source); }
		public virtual void CopyFrom(Expedient exp, InputInvoice source) 
		{
			OidExpediente = exp.Oid;
			OidFactura = source.Oid;
			OidConceptoAlbaran = 0;
			OidConceptoFactura = 0;
			PrevisionPago = source.Prevision;
			Pagado = (source.Pendiente > 0);
			NFactura = source.NFactura;
			Fecha = source.Fecha;
			CodigoExpediente = exp.Codigo;
			OidAcreedor = source.OidAcreedor;
			ETipoAcreedor = source.ETipoAcreedor;
			Acreedor = source.Acreedor;
			NFactura = source.NFactura;
			FechaFactura = source.Fecha;
			BaseFactura = source.BaseImponible;
			Total = source.BaseImponible;
			ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
            Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(source.ETipoAcreedor);
		}
		public virtual void CopyFrom(Expedient exp, InputInvoiceInfo source) 
		{
			OidExpediente = exp.Oid;
			OidFactura = source.Oid;
			OidConceptoAlbaran = 0;
			OidConceptoFactura = 0;
			PrevisionPago = source.Prevision;
			Pagado = (source.Pendiente > 0);
			NFactura = source.NFactura;
			Fecha = source.Fecha;
			CodigoExpediente = exp.Codigo;
			OidAcreedor = source.OidAcreedor;
			ETipoAcreedor = source.ETipoAcreedor;
			Acreedor = source.Acreedor;
			NFactura = source.NFactura;
			FechaFactura = source.Fecha;
			BaseFactura = source.BaseImponible;
			Total = source.BaseImponible;
			ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
            Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(source.ETipoAcreedor);
		}
		public virtual void CopyFrom(Expedient exp, InputInvoice source, ECategoriaGasto categoria)
        {
            if (source == null) return;

			//CopyFrom(exp, source);

			switch (source.ETipoAcreedor)
			{
				case ETipoAcreedor.Proveedor:
					if (categoria == ECategoriaGasto.Stock)
					{
						ECategoriaGasto = ECategoriaGasto.Stock;
						Total += source.Conceptos.GetSubTotalByExpediente(exp.Oid);
					}
					else
					{
						ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
						Total = source.Conceptos.GetSubTotalByExpediente(0);
					}

					Descripcion = EnumText<ECategoriaGasto>.GetLabel(ECategoriaGasto);

					break;

				default:
					ECategoriaGasto = ECategoriaGasto.GeneralesExpediente;
					Total = source.BaseImponible;
                    Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(source.ETipoAcreedor);
					break;
			}
        }
        public virtual void CopyFrom(Expedient exp, InputInvoiceInfo source, ECategoriaGasto categoria)
        {
            if (source == null) return;

			//CopyFrom(exp, source);

			switch (source.ETipoAcreedor)
			{
				case ETipoAcreedor.Proveedor:

					if (categoria == ECategoriaGasto.Stock)
					{
						ECategoriaGasto = ECategoriaGasto.Stock;
						Total += source.Conceptos.GetSubTotalByExpediente(exp.Oid);
					}
					else
					{
						ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
						Total = source.Conceptos.GetSubTotalByExpediente(0);
					}					

					Descripcion = EnumText<ECategoriaGasto>.GetLabel(ECategoriaGasto);

					break;

				case ETipoAcreedor.Despachante:
				case ETipoAcreedor.Naviera:
				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					ECategoriaGasto = ECategoriaGasto.GeneralesExpediente;
					Total = source.BaseImponible;
                    Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(source.ETipoAcreedor);
					break;

				default:
					ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
					Total = source.BaseImponible;
                    Descripcion = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(source.ETipoAcreedor);
					break;
			}
        }
		public virtual void CopyFrom(Expedient exp, InputInvoiceInfo fac, InputInvoiceLineInfo cf, InputDeliveryLineInfo ca)
		{
			OidAlbaran = (ca != null) ? ca.OidAlbaran : 0;
            OidConceptoAlbaran = (ca != null) ? ca.Oid : 0;
            OidFactura = fac.Oid;
			OidConceptoFactura = cf.Oid;
			OidAcreedor = fac.OidAcreedor;
			ETipoAcreedor = fac.ETipoAcreedor;
			Acreedor = fac.Acreedor;
			NFactura = fac.NFactura;
			FechaFactura = fac.Fecha;
			BaseFactura = fac.BaseImponible;
			PrevisionPago = fac.Prevision;

			if (exp != null)
			{
				OidExpediente = exp.Oid;
				CodigoExpediente = exp.Codigo;

				switch (exp.ETipoExpediente)
				{
					case ETipoExpediente.Alimentacion:
					case ETipoExpediente.Ganado:
					case ETipoExpediente.Maquinaria:

						switch (fac.ETipoAcreedor)
						{
							case ETipoAcreedor.Despachante:
							case ETipoAcreedor.Naviera:
							case ETipoAcreedor.TransportistaDestino:
							case ETipoAcreedor.TransportistaOrigen:
								ECategoriaGasto = ECategoriaGasto.GeneralesExpediente;
								Total = fac.BaseImponible;
								Descripcion = cf.Concepto;
								break;

							default:
								ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
								Total = fac.BaseImponible;
								Descripcion = cf.Concepto;
								break;
						}

						break;

					default:
						ECategoriaGasto = ECategoriaGasto.OtrosExpediente;
						Total = cf.BaseImponible;
						Descripcion = cf.Concepto;

						break;
				}
			}
			else
			{
				ECategoriaGasto = ECategoriaGasto.Generales;
				Total = cf.BaseImponible;
				Descripcion = cf.Concepto;
			}
		}
        public virtual void CopyFrom(Expedient exp, InputInvoiceLineInfo cf)
		{
			OidExpediente = exp.Oid;
			Total = cf.BaseImponible;
		}
		public virtual void CopyFrom(Expedient exp, Expense gasto, InputDeliveryLineInfo ca)
		{
			ECategoriaGasto = gasto.ECategoriaGasto;
			OidFactura = 0;
			OidAlbaran = (ca != null) ? ca.OidAlbaran : 0;
			OidConceptoFactura = 0;
			OidConceptoAlbaran = (ca != null) ? ca.Oid : 0;
			OidAcreedor = 0;
			ETipoAcreedor = ETipoAcreedor.Todos;
			Acreedor = string.Empty;
			NFactura = string.Empty;
			Fecha = gasto.Fecha;
			BaseFactura = 0;
			OidExpediente = exp.Oid;
			CodigoExpediente = exp.Codigo;
			Total = gasto.Total;
			Descripcion = gasto.Descripcion;
			Serial = gasto.Serial;
			Codigo = gasto.Codigo;
		}
		public virtual void CopyFrom(InputInvoiceInfo fac, InputInvoiceLineInfo cf, InputDeliveryLineInfo ca)
		{
			CopyFrom(null, fac, cf, ca);
		}
		public virtual void CopyFrom(PayrollBatch source, EmployeeInfo empleado, ECategoriaGasto categoria)
		{
			if (source == null) return;

			OidRemesaNomina = source.Oid;
			PrevisionPago = source.PrevisionPago;
			Fecha = source.Fecha;
			ECategoriaGasto = categoria;
			OidEmpleado = (empleado != null) ? empleado.Oid : 0;
			Empleado = (empleado != null) ? empleado.NombreCompleto : string.Empty;
			Total = (empleado != null) ? empleado.SueldoBruto : 0;

			Observaciones = String.Format(Resources.Messages.GASTO_ASOCIADO, source.Descripcion);

			string argumento = (empleado != null) ? empleado.NombreCompleto.ToUpper() : source.Descripcion;

			switch (categoria)
			{
				case ECategoriaGasto.SeguroSocial:

					Descripcion = String.Format(Resources.Messages.GASTO_NOMINA_SEGURO, argumento);

					break;

				case ECategoriaGasto.Impuesto:

					Descripcion = String.Format(Resources.Messages.GASTO_NOMINA_IRPF_EMPLEADO, argumento);

					break;

				default:

					Descripcion = String.Format(Resources.Messages.GASTO_NOMINA, argumento);

					break;
			}
		}

		public virtual void GetNewCode() { GetNewCode(null); }
		public virtual void GetNewCode(Expedient expediente)
		{
			switch (ECategoriaGasto)
			{
				case ECategoriaGasto.Expediente:
				case ECategoriaGasto.GeneralesExpediente:
				case ECategoriaGasto.Stock:
					{
						Serial = 0;
						Codigo = string.Empty;
					}
					break;

				case ECategoriaGasto.OtrosExpediente:
					{
						Serial = SerialGastoInfo.GetNext(ECategoriaGasto, expediente, Fecha.Year);
						Codigo = Serial.ToString(Resources.Defaults.GASTO_CODE_FORMAT);
					}
					break;

				default:
					{
						Serial = SerialInfo.GetNextByYear(typeof(Expense), Fecha.Year);
						Codigo = Serial.ToString(Resources.Defaults.GASTO_CODE_FORMAT);
					}
					break;
			}
		}

		public virtual void ChangeEstado(EEstado estado)
		{
			Common.EntityBase.CheckChangeState(EEstado, estado);
			EEstado = estado;
		}
		public static Expense ChangeEstado(long oid, ECategoriaGasto categoria, EEstado estado)
		{
			if ((categoria == ECategoriaGasto.GeneralesExpediente) ||
				(categoria == ECategoriaGasto.OtrosExpediente) ||
				(categoria == ECategoriaGasto.Stock))
				return null;

			Expense item = null;

			try
			{
				item = Expense.Get(oid, false);

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

		public static void IsPosibleDelete(long oid, ETipoPago tipo)
		{
			QueryConditions conditions = new QueryConditions
			{
				Gasto = Expense.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.Gasto.Oid = oid;
			conditions.PaymentType = tipo;

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
		public Expense()
		{
			_base.Record.Oid = (long)(new Random()).Next();
			Fecha = DateTime.Now;
			PrevisionPago = DateTime.Now;
			ECategoriaGasto = ECategoriaGasto.Otros;
			EEstado = EEstado.Abierto;
            OidUsuario = AppContext.User != null ? AppContext.User.Oid : 0;
            Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
			GetNewCode();
		}

		public virtual ExpenseInfo GetInfo() { return GetInfo(false); }
		public virtual ExpenseInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new ExpenseInfo(this, childs);
		}
			
		#endregion

		#region Child Factory Methods
	
		private Expense(Expense source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private Expense(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		internal static Expense NewChild(Expenses parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Expense obj  = new Expense();
			obj.MarkAsChild();

			if (parent != null) parent.SetNextCode(obj);

			return obj;
		}
		internal static Expense NewChild(Expedient exp) { return NewChild(exp, ECategoriaGasto.OtrosExpediente); }
		internal static Expense NewChild(Expedient exp, ECategoriaGasto categoria)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.OidExpediente = exp.Oid;
			obj.ECategoriaGasto = categoria;
			obj.GetNewCode(exp);

			return obj;
		}
		internal static Expense NewChild(Expedient exp, InputInvoice fac)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(exp, fac);

			return obj;
		}
		internal static Expense NewChild(Expedient exp, InputInvoice fac, ECategoriaGasto tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
            obj.CopyFrom(exp, fac, tipo);

            return obj;
        }
		internal static Expense NewChild(Expedient exp, InputInvoiceInfo fac)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(exp, fac);

			return obj;
		}
		internal static Expense NewChild(Expedient exp, InputInvoiceInfo fac, ECategoriaGasto tipo)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Expense obj = new Expense();
			obj.MarkAsChild();
            obj.CopyFrom(exp, fac, tipo);
			
			return obj;
		}
		internal static Expense NewChild(Expedient exp, InputInvoiceInfo fac, InputInvoiceLineInfo cf, InputDeliveryLineInfo ca)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(exp, fac, cf, ca);

			return obj;
		}
		internal static Expense NewChild(Expedient exp, Expense gasto, InputDeliveryLineInfo ca)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(exp, gasto, ca);

			return obj;
		}
		internal static Expense NewChild(PayrollBatch nomina, ECategoriaGasto tipo)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(nomina, null, tipo);

			return obj;
		}
		internal static Expense NewChild(PayrollBatch nomina, EmployeeInfo empleado)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Expense obj = new Expense();
			obj.MarkAsChild();
			obj.CopyFrom(nomina, empleado, ECategoriaGasto.Nomina);

			return obj;
		}
		
		internal static Expense GetChild(Expense source)
		{
			return new Expense(source);
		}		
		internal static Expense GetChild(int sessionCode, IDataReader reader) { return new Expense(sessionCode, reader); } 
		
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

		public static Expense New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<Expense>(new CriteriaCs(-1));
		}

		public static Expense Get(long oid) { return Get(oid, true); }
		public static Expense Get(long oid, bool childs)
		{
			if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Expense.SELECT(oid);

			BeginTransaction(criteria.Session);

			return DataPortal.Fetch<Expense>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid, ETipoPago tipo)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid, tipo);

			DataPortal.Delete(new CriteriaCs(oid));
		}

		public override Expense Save()
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
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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

		private void Fetch(Expense source)
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

		internal void Insert(Expenses parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		internal void Update(Expenses parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				ExpenseRecord obj = Session().Get<ExpenseRecord>(Oid);
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
		internal void DeleteSelf(Expense parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ExpenseRecord>(Oid));
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
		internal void DeleteSelf(Expenses parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ExpenseRecord>(Oid));
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
					Expense.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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

				Session().Save(Base.Record);
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
                Expense obj = null;

				try
				{
                    ExpenseRecord record = Session().Get<ExpenseRecord>(Oid);
                    obj = Expense.Get(Oid, true, SessionCode);

                    if (Common.EntityBase.UpdateByYear(obj, this, null))
                    {
                        obj.Save();
                        Transaction().Commit();
                        CloseSession();
                        NewTransaction();
                    }
                    else
                    {
                        record.CopyValues(this.Base.Record);
                        Session().Update(record);
                        //obj.CloseSession();
                    }

					MarkOld();
				}
				catch (Exception ex)
				{
                    //if (obj != null) obj.CloseSession();
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
				Session().Delete((ExpenseRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
            GetNewCode();

			try
			{	
				parent.Session().Save(Base.Record);
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
				ExpenseRecord obj = Session().Get<ExpenseRecord>(Oid);
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
				Session().Delete(Session().Get<ExpenseRecord>(Oid));
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

			OidRemesaNomina = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
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

			OidRemesaNomina = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				ExpenseRecord obj = Session().Get<ExpenseRecord>(Oid);
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
				Session().Delete(Session().Get<ExpenseRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}
		
		#endregion

        #region SQL

		internal enum ETipoQuery { GENERAL = 0, FACTURA = 1 }

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
			{
				/*{ 
					"Gross", 
					new ForeignField() {                        
						Property = "GROSS", 
						TableAlias = String.Empty, 
						Column = null
					}
				}*/
			};
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		internal static string FIELDS(ETipoQuery tipoQuery)
		{
			string query;

			query = "SELECT " + (long)tipoQuery + " AS \"TIPO_QUERY\"";

			switch (tipoQuery)
			{
				case ETipoQuery.GENERAL:

                    query +=
                    "       ,GT.*" +
                    "		,COALESCE(US.\"NAME\", '') AS \"USUARIO\"" +
                    "		,TG.\"NOMBRE\" AS \"TIPO_GASTO\"" +
                    "       ,COALESCE(FP.\"OID_ACREEDOR\", 0) AS \"OID_ACREEDOR\"" +
                    "       ,COALESCE(FP.\"TIPO_ACREEDOR\", 0) AS \"TIPO_ACREEDOR\"" +
                    "       ,COALESCE(FP.\"EMISOR\", '') AS \"ACREEDOR\"" +
                    "		,COALESCE(FP.\"FECHA\", NULL) AS \"FECHA_FACTURA\"" +
                    "		,COALESCE(FP.\"N_FACTURA\", '') AS \"N_FACTURA\"" +
                    "		,COALESCE(FP.\"BASE_IMPONIBLE\", 0) AS \"BASE_FACTURA\"" +
                    "       ,COALESCE(EX.\"CODIGO\", '') AS \"CODIGO_EXPEDIENTE\"" +
                    "		,COALESCE(PG.\"CODIGO\", '') AS \"ID_PAGO\"" +
                    "		,COALESCE(PG.\"FECHA\", NULL) AS \"FECHA_PAGO\"" +
                    "		,COALESCE(PG.\"MEDIO_PAGO\", 0) AS \"MEDIO_PAGO\"" +
                    "       ,COALESCE(PG.\"OID\", 0) AS \"OID_PAGO\"" +
                    "		,COALESCE(PF1.\"TOTAL_PAGADO\", 0) AS \"TOTAL_PAGADO\"" +
					"		,COALESCE(PF4.\"TOTAL_LIQUIDADO\", 0) AS \"TOTAL_LIQUIDADO\"" +
                    "       ,COALESCE(GT.\"TOTAL\" - (PF1.\"TOTAL_PAGADO\" - COALESCE(PF2.\"ASIGNADO_PAGO\", 0)), GT.\"TOTAL\") AS \"PENDIENTE\"" +
                    "		,COALESCE(PF2.\"ASIGNADO_PAGO\", 0) AS \"ASIGNADO_PAGO\"" +
                    "		,COALESCE(GT.\"TOTAL\", 0) - COALESCE(PF1.\"TOTAL_PAGADO\", 0) AS \"PENDIENTE_ASIGNAR\"" +
                    "		,COALESCE(RN.\"CODIGO\", '') AS \"ID_REMESA_NOMINA\"" +
                    "		,COALESCE(EM.\"NOMBRE\", '') || ' ' || COALESCE(EM.\"APELLIDOS\", '') AS \"EMPLEADO\"" +
                    "		,COALESCE(LC.\"CODIGO\", '') AS \"ID_LINEA_CAJA\"" +
                    "		,COALESCE(MV.\"CODIGO\", COALESCE(MV2.\"CODIGO\", '')) AS \"ID_MOVIMIENTO_BANCO\"";

					break;

				case ETipoQuery.FACTURA:

					query +=
					"       ,MAX(GT.\"OID\") AS \"OID\"" +
					"       ,GT.\"OID_EXPEDIENTE\"" +
					"       ,SUM(GT.\"TOTAL\") AS \"TOTAL\"" +
					"       ,MAX(GT.\"PREVISION_PAGO\") AS \"PREVISION_PAGO\"" +
					"       ,GT.\"OID_FACTURA\" AS \"OID_FACTURA\"" +
					"       ,GT.\"TIPO\" AS \"TIPO\"" +
					"       ,MAX(GT.\"FECHA\") AS \"FECHA\"" +
					"       ,GT.\"OID_USUARIO\" AS \"OID_USUARIO\"" +
					"       ,0 AS \"ESTADO\"" +
					"		,COALESCE(US.\"NAME\", '') AS \"USUARIO\"" +
					"		,TG.\"NOMBRE\" AS \"TIPO_GASTO\"" +
					"       ,COALESCE(FP.\"OID_ACREEDOR\", 0) AS \"OID_ACREEDOR\"" +
					"       ,COALESCE(FP.\"TIPO_ACREEDOR\", 0) AS \"TIPO_ACREEDOR\"" +
					"       ,COALESCE(FP.\"EMISOR\", '') AS \"ACREEDOR\"" +
					"		,COALESCE(FP.\"FECHA\", NULL) AS \"FECHA_FACTURA\"" +
					"		,COALESCE(FP.\"N_FACTURA\", '') AS \"N_FACTURA\"" +
					"		,COALESCE(FP.\"BASE_IMPONIBLE\", 0) AS \"BASE_FACTURA\"" +
					"       ,COALESCE(EX.\"CODIGO\", '') AS \"CODIGO_EXPEDIENTE\"" +
					"		,'' AS \"ID_PAGO\"" +
					"		,NULL AS \"FECHA_PAGO\"" +
					"		,0 AS \"MEDIO_PAGO\"" +
					"       ,0 AS \"OID_PAGO\"" +
					"       ,0 AS \"PENDIENTE\"" +
					"		,0 AS \"ASIGNADO_PAGO\"" +
                    "		,0 AS \"PENDIENTE_ASIGNAR\"" +
                    "		,'' AS \"ID_REMESA_NOMINA\"" +
					"		,'' AS \"EMPLEADO\"" +
                    "		,0 AS \"TOTAL_PAGADO\"" +
					"		,0 AS \"TOTAL_LIQUIDADO\"" +
                    "		,COALESCE(LC.\"CODIGO\", '') AS \"ID_LINEA_CAJA\"" +
                    "		,COALESCE(MV.\"CODIGO\", COALESCE(MV2.\"CODIGO\", '')) AS \"ID_MOVIMIENTO_BANCO\"";

					break;
			}

			return query;
		}

		private static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query = @"
            WHERE (GT.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			switch (conditions.Estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += @"
                    AND GT.""ESTADO"" != " + (long)EEstado.Anulado;
					break;

				default:
					query += @"
                    AND GT.""ESTADO"" = " + (long)conditions.Estado;
					break;
			}

            if (conditions.Acreedor != null)
            {
                switch (conditions.Acreedor.ETipoAcreedor)
                {
                    case ETipoAcreedor.Empleado:
                        query += @"
                        AND GT.""OID_EMPLEADO"" = " + conditions.Acreedor.OidAcreedor;
                        break;
                }
            }

            if (conditions.ConceptoAlbaranProveedor != null)
                query += " AND GT.\"OID_CONCEPTO_ALBARAN\" = " + conditions.ConceptoAlbaranProveedor.Oid;
			if (conditions.Gasto != null) query += " AND GT.\"OID\" = " + conditions.Gasto.Oid;
			if (conditions.TipoGasto != null) query += " AND GT.\"OID_TIPO\" = " + conditions.TipoGasto.Oid;
			if (conditions.Expedient != null) query += " AND GT.\"OID_EXPEDIENTE\" = " + conditions.Expedient.Oid;
			if (conditions.TipoExpediente != ETipoExpediente.Todos) query += " AND EX.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;
			if (conditions.RemesaNomina != null) query += " AND GT.\"OID_REMESA_NOMINA\" = " + conditions.RemesaNomina.Oid;
			if (conditions.Payment != null && conditions.Payment.Oid > 0) query += " AND PG.\"OID\" = " + conditions.Payment.Oid;
			if (conditions.FacturaRecibida != null) query += " AND GT.\"OID_FACTURA\" = " + conditions.FacturaRecibida.Oid;
            if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos) query += " AND FP.\"TIPO_ACREEDOR\" = " + (long)conditions.TipoAcreedor[0];
			switch (conditions.CategoriaGasto)
			{
                case ECategoriaGasto.Todos:
                    query += @"
						AND GT.""TIPO"" NOT IN (" +
                            (long)ECategoriaGasto.GeneralesExpediente + "," +
                            (long)ECategoriaGasto.OtrosExpediente + ")";
					break;

				case ECategoriaGasto.Expediente:
					query += @"
						AND GT.""TIPO"" IN (" +
							(long)ECategoriaGasto.GeneralesExpediente + "," +
							(long)ECategoriaGasto.Stock + "," +
							(long)ECategoriaGasto.OtrosExpediente + ")";
					break;

				case ECategoriaGasto.Generales:
					query += @"
						AND GT.""TIPO"" NOT IN (" +
							(long)ECategoriaGasto.GeneralesExpediente + "," +
							(long)ECategoriaGasto.Stock + "," +
							(long)ECategoriaGasto.OtrosExpediente + ")";
					break;

				case ECategoriaGasto.IRPFNominas:
					query += @"
						AND GT.""TIPO"" IN (" + (long)ECategoriaGasto.Impuesto + @")
						AND GT.""OID_TIPO"" = " + ModulePrincipal.GetDefaultIRPFSetting();						
						//AND GT.""OID_REMESA_NOMINA"" != 0";
					break;

				case ECategoriaGasto.SeguroSocialNominas:
					query += @"
						AND GT.""TIPO"" IN ("+ (long)ECategoriaGasto.SeguroSocial + @")
						AND GT.""OID_REMESA_NOMINA"" != 0";
					break;

				case ECategoriaGasto.OtrosBalance:
					query += @"
						AND (GT.""TIPO"" NOT IN (" +
								(long)ECategoriaGasto.GeneralesExpediente + "," +
								(long)ECategoriaGasto.Stock + "," +
								(long)ECategoriaGasto.OtrosExpediente + "," +
								(long)ECategoriaGasto.Nomina + "," +
								(long)ECategoriaGasto.SeguroSocial + @")
							OR (GT.""TIPO"" IN (" +
								(long)ECategoriaGasto.Impuesto + "," +
								(long)ECategoriaGasto.SeguroSocial + @") 
							AND GT.""OID_REMESA_NOMINA"" = 0))
						AND (GT.""TIPO"" IN (" + (long)ECategoriaGasto.Impuesto + @")
							AND GT.""OID_TIPO"" != " + ModulePrincipal.GetDefaultIRPFSetting() + @")";	
					break;

				default:
					query += @"
						AND GT.""TIPO"" = " + (long)conditions.CategoriaGasto;
					break;
			}

            if (conditions.OidList != null)
            {
                string subquery = @" 
                    AND GT.""OID"" IN (";

                foreach (long oid in conditions.OidList)
                    subquery += oid.ToString() + ", ";

                query += subquery.Substring(0, subquery.Length - 2) + ")";
            }

			return query + " " + conditions.ExtraWhere;
		}

		internal static string INNER_BASE(QueryConditions conditions)
		{
			string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string tg = nHManager.Instance.GetSQLTable(typeof(ExpenseTypeRecord));
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string rn = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollBatchRecord));
            string em = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.EmployeeRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

			string tipos = "(" + (long)ETipoPago.Gasto + ")";
			long oid_pago = (conditions.Payment != null) ? conditions.Payment.Oid : -1;

			string query;

            query = @"
				FROM " + gt + @" AS GT
				LEFT JOIN " + us + @" AS US ON US.""OID"" = GT.""OID_USUARIO""
				LEFT JOIN " + tg + @" AS TG ON TG.""OID"" = GT.""OID_TIPO""
				LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = GT.""OID_FACTURA""
				LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""
				LEFT JOIN " + rn + @" AS RN ON RN.""OID"" = GT.""OID_REMESA_NOMINA""
				LEFT JOIN " + em + @" AS EM ON EM.""OID"" = GT.""OID_EMPLEADO""";

            switch(conditions.PaymentType)
            {
                case ETipoPago.Fraccionado:

					// IMPORTE LIQUIDADO
                    query += @"                   
					LEFT JOIN ( SELECT ""OID_OPERACION"", SUM(""TOTAL_PAGADO"") AS ""TOTAL_PAGADO""
								FROM (	SELECT PF.""OID_OPERACION""
												,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
										FROM " + pf + @" AS PF
										INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
										INNER JOIN " + pg + @" AS P ON P.""OID"" = PG.""OID_ROOT"" AND P.""TIPO"" = " + (long)ETipoPago.Fraccionado + @"
										WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @" AND P.""ESTADO"" != " + (long)EEstado.Anulado + @"
											AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
										GROUP BY PF.""OID_OPERACION""
										UNION
										SELECT PF.""OID_OPERACION""
												,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
										FROM " + pf + @" AS PF
										INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PG.""TIPO"" = " + (long)ETipoPago.Gasto + @" AND PG.""OID_ROOT"" = 0
										WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
										AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
										GROUP BY PF.""OID_OPERACION"") AS AUX
								GROUP BY ""OID_OPERACION"")
					AS PF1 ON PF1.""OID_OPERACION"" = GT.""OID""";

					// IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTE GASTO
					query +=
					"	LEFT JOIN ( SELECT \"OID_OPERACION\", SUM(\"ASIGNADO_PAGO\") AS \"ASIGNADO_PAGO\", \"OID_PAGO\"" +
					"               FROM (  SELECT PF.\"OID_OPERACION\"" +
					"					        ,SUM(PF.\"CANTIDAD\") AS \"ASIGNADO_PAGO\"" +
					"					        ,P.\"OID\" AS \"OID_PAGO\"" +
					"				        FROM " + pf + " AS PF" +
					"				        INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\"" +
					"                       INNER JOIN " + pg + " AS P ON P.\"OID\" = PG.\"OID_ROOT\" AND P.\"TIPO\" = " + (long)ETipoPago.Fraccionado +
					"				        WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado + " AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
					"				        GROUP BY PF.\"OID_OPERACION\", P.\"OID\"" +
					"                       UNION" +
					"                       SELECT PF.\"OID_OPERACION\"" +
					"					        ,SUM(PF.\"CANTIDAD\") AS \"ASIGNADO_PAGO\"" +
					"					        ,PG.\"OID\" AS \"OID_PAGO\"" +
					"				        FROM " + pf + " AS PF" +
					"				        INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\" AND PG.\"TIPO\" = " + (long)ETipoPago.Gasto + " AND PG.\"OID_ROOT\" = 0" +
					"				        WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado +
					"				        GROUP BY PF.\"OID_OPERACION\", PG.\"OID\") AS AUX" +
					"               GROUP BY \"OID_OPERACION\", \"OID_PAGO\")" +
					"		AS PF2 ON PF2.\"OID_OPERACION\" = GT.\"OID\"";

                    // IMPORTE TOTAL ASIGNADO A ESTE GASTO POR TODOS LOS PAGOS
					query += 
                    "	LEFT JOIN ( SELECT \"OID_OPERACION\", SUM(\"TOTAL_ASIGNADO\") AS \"TOTAL_ASIGNADO\"" +
                    "               FROM (  SELECT PF.\"OID_OPERACION\"" +
                    "					        ,SUM(PF.\"CANTIDAD\") AS \"TOTAL_ASIGNADO\"" +
                    "				        FROM " + pf + " AS PF" +
                    "				        INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\"" +
                    "                       INNER JOIN " + pg + " AS P ON P.\"OID\" = PG.\"OID_ROOT\" AND P.\"TIPO\" = " + (long)ETipoPago.Fraccionado +
                    "				        WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado + " AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                           AND (PG.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " OR PG.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "')" +
                    "				        GROUP BY PF.\"OID_OPERACION\"" +
                    "                       UNION" +
                    "                       SELECT PF.\"OID_OPERACION\"" +
                    "					        ,SUM(PF.\"CANTIDAD\") AS \"TOTAL_ASIGNADO\"" +
                    "				        FROM " + pf + " AS PF" +
                    "				        INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\" AND PG.\"TIPO\" = " + (long)ETipoPago.Gasto + " AND PG.\"OID_ROOT\" = 0" +
                    "				        WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado + 
                    "                           AND (PG.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " OR PG.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "')" +
                    "				        GROUP BY PF.\"OID_OPERACION\") AS AUX" +
                    "               GROUP BY \"OID_OPERACION\")" +
                    "		AS PF3 ON PF3.\"OID_OPERACION\" = GT.\"OID\"";

					// IMPORTE LIQUIDADO
                    query += @"                   
					LEFT JOIN ( SELECT ""OID_OPERACION"", SUM(""TOTAL_LIQUIDADO"") AS ""TOTAL_LIQUIDADO""
								FROM (	SELECT PF.""OID_OPERACION""
												,SUM(PF.""CANTIDAD"") AS ""TOTAL_LIQUIDADO""
										FROM " + pf + @" AS PF
										INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
										INNER JOIN " + pg + @" AS P ON P.""OID"" = PG.""OID_ROOT"" AND P.""TIPO"" = " + (long)ETipoPago.Fraccionado + @"
										WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @" AND P.""ESTADO"" != " + (long)EEstado.Anulado + @"
											AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
										GROUP BY PF.""OID_OPERACION""
										UNION
										SELECT PF.""OID_OPERACION""
												,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
										FROM " + pf + @" AS PF
										INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PG.""TIPO"" = " + (long)ETipoPago.Gasto + @" AND PG.""OID_ROOT"" = 0
										WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
										AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
										GROUP BY PF.""OID_OPERACION"") AS AUX
								GROUP BY ""OID_OPERACION"")
					AS PF4 ON PF4.""OID_OPERACION"" = GT.""OID""";

                    break;

                default:

					// IMPORTE DE PAGOS ASOCIADOS
                    query += @"                        
					LEFT JOIN (SELECT PF.""OID_OPERACION""
									,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
									,PF.""TIPO_PAGO""
									,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
								FROM " + pf + @" AS PF
								INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
								WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado;
                    //if (conditions.Pago != null && conditions.Pago.Oid > 0)
                    //    query +=
                    //"                   AND PG.""OID"" = " + conditions.Pago.Oid;
					query += @"
									GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
							AS PF1 ON PF1.""OID_OPERACION"" = GT.""OID""";

					// IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTE GASTO
					query +=                        
                    "	LEFT JOIN (SELECT PF.\"OID_OPERACION\"" +
                    "					,SUM(PF.\"CANTIDAD\") AS \"ASIGNADO_PAGO\"" +
                    "					,MAX(PF.\"OID_PAGO\") AS \"OID_PAGO\"" +
                    "				FROM " + pf + " AS PF" +
                    "				INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\" AND PF.\"TIPO_PAGO\" IN " + tipos +
                    "				WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado;
                    if (oid_pago != 0)
                        query += @"
										AND PG.""OID"" = " + oid_pago;
                    query +=
                    "				GROUP BY PF.\"OID_OPERACION\", PF.\"TIPO_PAGO\")" +
                    "		AS PF2 ON PF2.\"OID_OPERACION\" = GT.\"OID\"" +
                        // IMPORTE TOTAL ASIGNADO A ESTE GASTO POR TODOS LOS PAGOS
                    "	LEFT JOIN (SELECT PF.\"OID_OPERACION\"" +
                    "					,SUM(PF.\"CANTIDAD\") AS \"TOTAL_ASIGNADO\"" +
                    "					,MAX(PF.\"OID_PAGO\") AS \"OID_PAGO\"" +
                    "				FROM " + pf + " AS PF" +
                    "				INNER JOIN " + pg + " AS PG ON PG.\"OID\" = PF.\"OID_PAGO\" AND PF.\"TIPO_PAGO\" IN " + tipos +
                    "				WHERE PG.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                   AND (PG.\"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " OR PG.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "')" +
                    "				GROUP BY PF.\"OID_OPERACION\", PF.\"TIPO_PAGO\")" +
                    "		AS PF3 ON PF3.\"OID_OPERACION\" = GT.\"OID\"";

					// IMPORTE LIQUIDADO
					query += @"                        
					LEFT JOIN (SELECT PF.""OID_OPERACION""
									,SUM(PF.""CANTIDAD"") AS ""TOTAL_LIQUIDADO""
									,PF.""TIPO_PAGO""
									,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
								FROM " + pf + @" AS PF
								INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
								WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
									AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
								GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
						AS PF4 ON PF4.""OID_OPERACION"" = GT.""OID""";
                    break;
			}

			return query;
		}
        
		private static string SELECT_BASE(QueryConditions conditions)
        {
			string query = 
			FIELDS(ETipoQuery.GENERAL) +
            INNER_BASE(conditions);
			
			return query;
		}

        private static string SELECT_BASE_FACTURA_EXPEDIENTE(QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

			string query =
				FIELDS(ETipoQuery.FACTURA) +
				INNER_BASE(conditions) +
				// PAGO ASOCIADO (EL ULTIMO)
			"	LEFT JOIN " + pg + " AS PG ON PG.\"OID\" = PF1.\"OID_PAGO\"" +
            "   LEFT JOIN " + lc + " AS LC ON PG.\"OID\" = LC.\"OID_PAGO\" AND LC.\"ESTADO\" != " + (long)EEstado.Anulado +
            "   LEFT JOIN (SELECT MIN(MV.\"CODIGO\") AS \"CODIGO\"" +
            "					,MIN(MV.\"OID\") AS \"OID\"" +
            "					,MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\"" +
            "			FROM " + mv + " AS MV" +
            "			WHERE MV.\"ESTADO\" != " + (long)EEstado.Anulado +
            "			GROUP BY MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\")" +
            "	AS MV ON PG.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)EBankLineType.PagoGasto +
            "   LEFT JOIN (SELECT MIN(MV.\"CODIGO\") AS \"CODIGO\"" +
            "					,MIN(MV.\"OID\") AS \"OID\"" +
            "					,MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\", MV.\"FECHA_OPERACION\"" +
            "			FROM " + mv + " AS MV" +
            "			WHERE MV.\"ESTADO\" != " + (long)EEstado.Anulado +
            "			GROUP BY MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\", MV.\"FECHA_OPERACION\")" +
            "	AS MV2 ON PG.\"OID_TARJETA_CREDITO\" = MV2.\"OID_OPERACION\" AND PG.\"VENCIMIENTO\" = CAST(MV2.\"FECHA_OPERACION\" AS DATE) AND MV2.\"TIPO_OPERACION\" = " + (long)EBankLineType.ExtractoTarjeta;
            
			return query;
        }
		
		private static string SELECT_BASE_PENDIENTES(QueryConditions conditions)
		{
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

			string query = string.Empty;

            query =
            SELECT_BASE(conditions) + 
            // PAGO ASOCIADO (EL ULTIMO) 
			@"
            LEFT JOIN " + pg + @" AS PG ON PG.""OID"" = PF1.""OID_PAGO""
			LEFT JOIN " + lc + @" AS LC ON PG.""OID"" = LC.""OID_PAGO"" AND LC.""ESTADO"" != " + (long)EEstado.Anulado + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
            				,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION""
                            ,MV.""TIPO_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"")
				AS MV ON PG.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.PagoGasto + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
							,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION""
                            ,MV.""TIPO_OPERACION""
                            ,MV.""FECHA_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
					GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"", MV.""FECHA_OPERACION"")
				AS MV2 ON PG.""OID_TARJETA_CREDITO"" = MV2.""OID_OPERACION"" 
                            AND PG.""VENCIMIENTO"" = CAST(MV2.""FECHA_OPERACION"" AS DATE) 
                            AND MV2.""TIPO_OPERACION"" = " + (long)EBankLineType.ExtractoTarjeta + 
			WHERE(conditions) + @"
			    AND (PF1.""TOTAL_PAGADO"" != GT.""TOTAL"" OR PF1.""TOTAL_PAGADO"" IS NULL)
			    AND (GT.""PREVISION_PAGO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')
			    AND GT.""ESTADO"" != " + (long)EEstado.Anulado + @"
			    AND COALESCE(GT.""TOTAL"" - PF1.""TOTAL_PAGADO"", GT.""TOTAL"") != 0";

			return query;
		}

		private static string SELECT_BASE_PENDIENTES_LIQUIDACION(QueryConditions conditions)
		{
			string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

			string query = string.Empty;

			query =
			SELECT_BASE(conditions) +
			// PAGO ASOCIADO (EL ULTIMO) 
			@"
            LEFT JOIN " + pg + @" AS PG ON PG.""OID"" = PF1.""OID_PAGO""
			LEFT JOIN " + lc + @" AS LC ON PG.""OID"" = LC.""OID_PAGO"" AND LC.""ESTADO"" != " + (long)EEstado.Anulado + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
            				,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION"", MV.""TIPO_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"")
				AS MV ON PG.""OID"" = MV.""OID_OPERACION"" 
                        AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.PagoGasto + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
							,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION""
                            ,MV.""TIPO_OPERACION""
                            ,MV.""FECHA_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
					GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"", MV.""FECHA_OPERACION"")
				AS MV2 ON PG.""OID_TARJETA_CREDITO"" = MV2.""OID_OPERACION"" 
                        AND PG.""VENCIMIENTO"" = CAST(MV2.""FECHA_OPERACION"" AS DATE) 
                        AND MV2.""TIPO_OPERACION"" = " + (long)EBankLineType.ExtractoTarjeta +
			WHERE(conditions) + @"
			    AND GT.""ESTADO"" != " + (long)EEstado.Anulado + @"
			    AND GT.""TOTAL"" != 0 				
			    AND (COALESCE(PF4.""TOTAL_LIQUIDADO"", 0) != GT.""TOTAL"")
			    AND (GT.""PREVISION_PAGO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')";				

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Gasto = ExpenseInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

			string query = string.Empty;

            query =
            SELECT_BASE(conditions);

            // PAGO ASOCIADO (EL ULTIMO)
			query += @"
			LEFT JOIN " + pg + @" AS PG ON PG.""OID"" = PF1.""OID_PAGO""
			LEFT JOIN " + lc + @" AS LC ON PG.""OID"" = LC.""OID_PAGO"" AND LC.""ESTADO"" != " + (long)EEstado.Anulado + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
							,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION"", MV.""TIPO_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"")
				AS MV ON PG.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.PagoGasto + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
							,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION"", MV.""TIPO_OPERACION"", MV.""FECHA_OPERACION""
						FROM " + mv + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"", MV.""FECHA_OPERACION"")
				AS MV2 ON PG.""OID_TARJETA_CREDITO"" = MV2.""OID_OPERACION"" AND PG.""VENCIMIENTO"" = CAST(MV2.""FECHA_OPERACION"" AS DATE) AND MV2.""TIPO_OPERACION"" = " + (long)EBankLineType.ExtractoTarjeta +
			WHERE(conditions);

			if (conditions.Orders == null)
			{
				conditions.Orders = new OrderList();
				conditions.Orders.Add(FilterMng.BuildOrderItem("Fecha", ListSortDirection.Descending, typeof(Expense)));
			}

			query += ORDER(conditions.Orders, string.Empty, ForeignFields());
			query += LIMIT(conditions.PagingInfo);
			query += Common.EntityBase.LOCK("GT", lockTable);
			
			return query;
		}

        internal static string SELECT_ASOCIADO(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
                AND ""ASIGNADO_PAGO"" > 0";

            string query = SELECT(conditions, lockTable);

            return query;
        }

        internal static string SELECT_BY_PAGO(QueryConditions conditions, bool lockTable)
        {
            string query = string.Empty;

            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));

            string tipos = string.Empty;
            
            if (conditions.PaymentType == ETipoPago.Fraccionado)
                tipos = "(" + (long)ETipoPago.Fraccionado + ", " + (long)ETipoPago.Gasto + ")";
            else
                tipos = "(" + (long)ETipoPago.Nomina + ", " + (long)ETipoPago.Gasto + ")";

            long oid_pago = (conditions.Payment != null) ? conditions.Payment.Oid : -1;

            query =
                SELECT_BASE(conditions) +
                // PAGO ASOCIADO
            "	LEFT JOIN " + pg + " AS PG ON PG.\"OID\" = PF2.\"OID_PAGO\" AND PG.\"TIPO\" IN " + tipos +
            "   LEFT JOIN " + lc + " AS LC ON PG.\"OID\" = LC.\"OID_PAGO\" AND LC.\"ESTADO\" != " + (long)EEstado.Anulado +
            "   LEFT JOIN (SELECT MIN(MV.\"CODIGO\") AS \"CODIGO\"" +
            "					,MIN(MV.\"OID\") AS \"OID\"" +
            "					,MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\"" +
            "			FROM " + mv + " AS MV" +
            "			WHERE MV.\"ESTADO\" != " + (long)EEstado.Anulado +
            "			GROUP BY MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\")" +
            "	AS MV ON PG.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)EBankLineType.PagoGasto +
            "   LEFT JOIN (SELECT MIN(MV.\"CODIGO\") AS \"CODIGO\"" +
            "					,MIN(MV.\"OID\") AS \"OID\"" +
            "					,MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\", MV.\"FECHA_OPERACION\"" +
            "			FROM " + mv + " AS MV" +
            "			WHERE MV.\"ESTADO\" != " + (long)EEstado.Anulado +
            "			GROUP BY MV.\"OID_OPERACION\", MV.\"TIPO_OPERACION\", MV.\"FECHA_OPERACION\")" +
            "	AS MV2 ON PG.\"OID_TARJETA_CREDITO\" = MV2.\"OID_OPERACION\" AND PG.\"VENCIMIENTO\" = CAST(MV2.\"FECHA_OPERACION\" AS DATE) AND MV2.\"TIPO_OPERACION\" = " + (long)EBankLineType.ExtractoTarjeta +
                WHERE(conditions);
            
            if (oid_pago != 0)
                query += @"
			        AND PF2.""OID_PAGO"" = " + oid_pago; 

            //if (lockTable) query += " FOR UPDATE OF G NOWAIT";

			query += @"
			ORDER BY GT.""FECHA"" ASC";

            return query;
        }

        internal static string SELECT_BY_FACTURA_EXPEDIENTE(QueryConditions conditions, bool lockTable)
        {
            string query = string.Empty;

            query = 
				SELECT_BASE_FACTURA_EXPEDIENTE(conditions) +
				WHERE(conditions);

            //if (lockTable) query += " FOR UPDATE OF G NOWAIT";

            return query;
        }

		internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lockTable)
		{
            string query = string.Empty;

			query =
			SELECT_BASE_PENDIENTES(conditions);

			//if (lockTable) query += " FOR UPDATE OF G NOWAIT";

			query += @"
			ORDER BY GT.""FECHA"" ASC";

			return query;
		}
		internal static string SELECT_PENDIENTES_NOMINAS(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			query =
			SELECT_BASE_PENDIENTES(conditions) + @"
            AND GT.""OID_REMESA_NOMINA"" != 0";

			//if (lockTable) query += " FOR UPDATE OF G NOWAIT";

            query += @"
			ORDER BY GT.""FECHA"" ASC";

			return query;
		}
		internal static string SELECT_PENDIENTES_LIQUIDACION(QueryConditions conditions, bool lockTable)
		{
			string query =
			SELECT_BASE_PENDIENTES_LIQUIDACION(conditions);

			query += @"
			ORDER BY GT.""FECHA"" ASC";

			return query;
		}
		
        #endregion	
	}
}