using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.BankLine;
using moleQule.Base;
using moleQule.Cash;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using moleQule.Invoice.Structs;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PaymentBase
    {
        #region Attributes

        private PaymentRecord _record = new PaymentRecord();

        internal TransactionPayments _pago_facturas = TransactionPayments.NewChildList();
        internal Expenses _gastos = Expenses.NewChildList();
        internal Payments _pagos = Payments.NewChildList();

        //Campos no enlazados
		internal DateTime _step_date;
		internal Decimal _total;
        private string _bank_account = string.Empty;
        private string _bank = string.Empty;
        private string _credit_card = string.Empty;
        private string _agent = string.Empty;
        private string _agent_id = string.Empty;
        private string _bank_line_id = string.Empty;
        private string _cash_line_id;
        private string _accounting_line_id;
        private decimal _pendiente;
        private string _owner = string.Empty;

        internal string _vinculado = moleQule.Library.Store.Resources.Labels.SET_PAGO;
        internal Decimal _asignado;
        internal Decimal _acumulado;
        internal Decimal _total_pagado;
        internal DateTime _fecha_asignacion;
        internal Decimal _pendiente_asignar = 0;
        internal DateTime _fecha_ordenacion;

        #endregion

        #region Properties

        public PaymentRecord Record { get { return _record; } set { _record = value; } }

        public ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAgente; } }
        public ETipoPago ETipoPago { get { return (ETipoPago)_record.Tipo; } set { _record.Tipo = (long)value; } }
        public string TipoPagoLabel { get { return moleQule.Store.Structs.EnumText<ETipoPago>.GetLabel(ETipoPago); } }
		public DateTime StepDate { get { return _step_date; } set { _step_date = value; } }
		public Decimal Total { get { return _record.Importe; } set { _record.Importe = value; } }
        public Decimal Pendiente { get { return _pendiente; } set { _pendiente = value; } }
		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EStatus); } }
		public EEstado EPaymentStatus { get { return (EEstado)_record.EstadoPago; } }
		public string PaymentStatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EPaymentStatus); } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } set { _record.MedioPago = (long)value; } }
		public string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
        public string Agent
        {
            get
            {
                return new ETipoPago[] 
                {
                    ETipoPago.Factura, 
                    ETipoPago.FraccionadoTarjeta,
                    ETipoPago.ExtractoTarjeta, 
                    ETipoPago.Nomina,
                    ETipoPago.Prestamo
                }.Contains(ETipoPago) ? _agent : TipoPagoLabel.ToUpper();
            }

            set { _agent = value; }
        }
        public string AgentID { get { return _agent_id; } set { _agent_id = value; } }
        public string BankLineID { get { return _bank_line_id; } set { _bank_line_id = value; } }
        public string AccountingLineID { get { return _accounting_line_id; } set { _accounting_line_id = value; } }
        public string CashLineID 
        { 
            get { return (_cash_line_id != null) ? String.Format(_cash_line_id, Cash.Resources.Defaults.CASHLINE_CODE_FORMAT) : string.Empty; }
            set { _cash_line_id = value; }
        }
        public string CreditCard { get { return _credit_card; } set { _credit_card = value; } }
        public string PagoIDLabel 
        { 
            get { return (ETipoPago == ETipoPago.Factura) 
                                    ? AgentID + "/" + Record.IdPago.ToString(Resources.Defaults.PAGO_ID_FORMAT) 
                                    : string.Empty; } 
       }
        public string BankAccount { get { return _bank_account; } set { _bank_account = value; } }
        public string Bank { get { return _bank; } set { _bank = value; } }
        public string Owner { get { return _owner; } set { _owner = value; } }
        public Dictionary<string, object> Tags { get; set; }

        #endregion

        #region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Payment.ETipoQuery tipo = (Payment.ETipoQuery)Format.DataReader.GetInt64(source, "TIPO_QUERY");

			switch (tipo)
			{
				case Payment.ETipoQuery.AGRUPADO:

					_record.Oid = Format.DataReader.GetDateTime(source, "STEP").ToBinary();
					_record.Importe = Format.DataReader.GetDecimal(source, "TOTAL");
					_step_date = Format.DataReader.GetDateTime(source, "STEP");

					break;

				case Payment.ETipoQuery.GENERAL:
				default:
					{
						_record.CopyValues(source);

						_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");

                        _accounting_line_id = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");
                        _accounting_line_id = (_accounting_line_id == "/") ? string.Empty : _accounting_line_id;
						_owner = Format.DataReader.GetString(source, "USUARIO");
						_bank_account = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
						_bank = Format.DataReader.GetString(source, "ENTIDAD");
						_credit_card = Format.DataReader.GetString(source, "TARJETA_CREDITO");
						_cash_line_id = Format.DataReader.GetString(source, "ID_LINEA_CAJA");
						_bank_line_id = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");

						_agent = Format.DataReader.GetString(source, "AGENTE");
						_agent_id = Format.DataReader.GetString(source, "COD_AGENTE");
						_total_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
						_asignado = Format.DataReader.GetDecimal(source, "TOTAL_ASIGNADO");

						_fecha_ordenacion = Format.DataReader.GetDateTime(source, "FECHA_ORDENACION");
					}
					break;
			}
		}
        public void CopyValues(Payment source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _bank_account = source.CuentaBancaria;
            _bank = source.Entidad;
            _credit_card = source.TarjetaCredito;
            _agent = source.Agente;
            _agent_id = source.CodigoAgente;
            _bank_line_id = source.IDMovimientoBanco;
            _cash_line_id = source.IDLineaCaja;
            _accounting_line_id = source.IDMovimientoContable;
            _owner = source.Usuario;
            _pendiente = source.Pendiente;

            Tags = source.Tags;
        }
        public void CopyValues(PaymentInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _bank_account = source.CuentaBancaria;
            _bank = source.Entidad;
            _credit_card = source.TarjetaCredito;
            _agent = source.Agente;
            _agent_id = source.CodigoAgente;
            _bank_line_id = source.IDMovimientoBanco;
            _cash_line_id = source.IDLineaCaja;
            _accounting_line_id = source.IDMovimientoContable;
            _owner = source.Usuario;

            _vinculado = source.Vinculado;
            _asignado = source.Asignado;
            _acumulado = source.Acumulado;
            _total_pagado = source.TotalPagado;

			_total = source.Total;
			_step_date = source.StepDate;

            Tags = source.Tags;
        }

        #endregion
    }

    /// <summary>
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Payment : BusinessBaseEx<Payment>, BankLine.IBankLine, IEntidadRegistro, IEntityBase
    {
        #region IEntityBase

        public long EntityType { get { return (long)ETipoEntidad.Pago; } }
        public virtual DateTime FechaReferencia { get { return _base.Record.Vencimiento; } set { Vencimiento = value; } }
        public virtual long OidCreditCard { get { return OidTarjetaCredito; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((Payment)source); }
        public void DifferentYearChecks() { }
        public virtual void DifferentYearTasks(IEntityBase oldItem)
        {
            //Editamos o Anulamos la Linea de Caja
            moleQule.Cash.Cash.EditItem(this, ((Payment)oldItem).GetInfo(false), SessionCode);

            switch (EMedioPago)
            {
                case EMedioPago.Tarjeta:

                    BankLine.BankLine.EditItemTarjeta(this, ((Payment)oldItem).GetInfo(false), SessionCode);
                    break;

                default:

                    AnnulRelateds(((Payment)oldItem).GetInfo(false), SessionCode);
                    EditRelateds(this, SessionCode);
                    BankLine.BankLine.EditItem(this, ((Payment)oldItem).GetInfo(false), SessionCode);
                    break;
            }                
        }
        public void SameYearTasks(IEntityBase newItem)
        {
            try
            {
                Payment obj = (Payment)this;

                EditRelateds((Payment)newItem, newItem.SessionCode);
            }
            catch (iQException ex)
            {
                if (ex.Code != iQExceptionCode.WARNING) throw ex;

                //Se ha producido un WARNING que no impide que actualicemos
            }
        }
        public virtual void IEntityBaseSave(object parent)
        {
            if (parent != null)
            {
                if (parent is IAcreedor)
                {
                    //((IAcreedor)parent).Pagos.AddItem(this);
                    Insert((IAcreedor)parent);
                }
                else if (parent.GetType() == typeof(Payments))
                {
                    //((Payments)parent).AddItem(this);
                    Insert((Payments)parent);
                }
                else if (parent.GetType() == typeof(Loan))
                {
                    //((Loan)parent).Payments.AddItem(this);
                    Insert((Loan)parent);
                }
                else if (parent.GetType() == typeof(Payment))
                {
                    //((Payment)parent).Pagos.AddItem(this);
                    Insert((Payment)parent);
                }
                else
                    Save();
            }
            else
                Save();
        }

        #endregion

        #region IBankLine

        public ETipoEntidad EEntityType { get { return ETipoEntidad.Pago; } }
        public virtual long TipoMovimiento { get { return (long)ETipoMovimientoBanco; } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return moleQule.Store.Structs.EnumConvert.ToETipoMovimientoBanco(ETipoPago); } }
        public virtual ETipoTitular ETipoTitular { get { return moleQule.Store.Structs.EnumConvert.ToETipoTitular(ETipoAcreedor); } }
        public virtual string CodigoTitular { get { return CodigoAgente; } set { } }
        public virtual string Titular { get { return Agente; } set { } }
        public virtual long OidCuenta { get { return _base.Record.OidCuentaBancaria; } set { } }
        public virtual bool Confirmado { get { return EEstadoPago == EEstado.Pagado; } }
        public EEstado EEstadoOperacion { get { return EEstadoPago; } }
        public Dictionary<string, object> Tags { get { return _base.Tags; } set { _base.Tags = value; } }

        public virtual BankLine.IBankLineInfo IGetInfo(bool childs) { return (BankLine.IBankLineInfo)GetInfo(childs); }

        #endregion

        #region IEntidadRegistro

        public virtual ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.Pago; } }
        public string DescripcionRegistro { get { return "PAGO Nº " + IDPagoLabel + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + Agente; } }

        public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
        public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

        public void Update(Registro parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            SessionCode = parent.SessionCode;
            PaymentRecord obj = Session().Get<PaymentRecord>(Oid);
            obj.CopyValues(this._base.Record);
            Session().Update(obj);

            MarkOld();
        }

        #endregion

        #region Attributes

        public PaymentBase _base = new PaymentBase();

        protected long _last_bank_line_serial = -1;

        protected TransactionPayments _operations = TransactionPayments.NewChildList();
        protected Expenses _expenses = Expenses.NewChildList();
        protected Payments _payments = Payments.NewChildList();

        #endregion

        #region Properties

		PaymentBase Base { get { return _base; } }

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
				//CanWriteProperty(true);

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
        public virtual long OidCuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidCuentaBancaria.Equals(value))
                {
                    _base.Record.OidCuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidTarjetaCredito
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidTarjetaCredito;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidTarjetaCredito.Equals(value))
                {
                    _base.Record.OidTarjetaCredito = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidAgente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidAgente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidAgente.Equals(value))
                {
                    _base.Record.OidAgente = value;
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
        public virtual long TipoPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Tipo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Tipo.Equals(value))
                {
                    _base.Record.Tipo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long TipoAgente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoAgente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.TipoAgente.Equals(value))
                {
                    _base.Record.TipoAgente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long IdPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.IdPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.IdPago.Equals(value))
                {
                    _base.Record.IdPago = value;
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
        public virtual Decimal Importe
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Importe;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Importe.Equals(value))
                {
                    _base.Record.Importe = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GastosBancarios
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GastosBancarios;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GastosBancarios.Equals(value))
                {
                    _base.Record.GastosBancarios = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long MedioPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.MedioPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.MedioPago.Equals(value))
                {
                    _base.Record.MedioPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime Vencimiento
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Vencimiento;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.Vencimiento.Equals(value))
                {
                    _base.Record.Vencimiento = value;
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
        public virtual long OidRoot
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidRoot;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidRoot.Equals(value))
                {
                    _base.Record.OidRoot = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidLink
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidLink;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidLink.Equals(value))
                {
                    _base.Record.OidLink = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long EstadoPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.EstadoPago;
            }



            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.EstadoPago.Equals(value))
                {
                    _base.Record.EstadoPago = value;
                    PropertyHasChanged();
                }
            }
        }

        public long LastBankLineSerial { get { return _last_bank_line_serial; } set { _last_bank_line_serial = value; } }

        public virtual TransactionPayments Operations
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _operations;
            }

            set
            {
                _operations = value;
            }
        }
        public virtual Expenses Gastos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _expenses;
            }

            set
            {
                _expenses = value;
            }
        }
        public virtual Payments Pagos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _payments;
            }

            set
            {
                _payments = value;
            }
        }

        public virtual decimal PendienteAsignacion { get { return _base.Pendiente; } }
        public virtual decimal Pendiente
        {
            get
            {
                return (EEstado == EEstado.Anulado) 
                    ? 0 
                    : (Operations == null) 
                        ? _base.Record.Importe 
                        : _base.Record.Importe - Operations.GetTotal();
            }

            set { _base.Pendiente = value; }
        }
        public virtual bool Pagado { get { return EEstadoPago == EEstado.Pagado; } }

        //Campos no enlazados
        public virtual string IDPagoLabel { get { return _base.PagoIDLabel; } }
        public virtual string CuentaBancaria { get { return _base.BankAccount; } set { _base.BankAccount = value; } }
        public virtual string Entidad { get { return _base.Bank; } set { _base.Bank = value; } }
        public virtual string TarjetaCredito { get { return _base.CreditCard; } set { _base.CreditCard = value; } }
        public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
        public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
        public virtual string Agente { get { return _base.Agent; } set { _base.Agent = value; } }
        public virtual string CodigoAgente { get { return _base.AgentID; } set { _base.AgentID = value; } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { TipoAgente = (long)value; } }
        public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.StatusLabel; } } /*DEPRECATED*/
        public virtual string SatusLabel { get { return _base.StatusLabel; } }
        public virtual EEstado EEstadoPago { get { return _base.EPaymentStatus; } set { EstadoPago = (long)value; } }
        public virtual string EstadoPagoLabel { get { return _base.PaymentStatusLabel; } }
        public virtual string IDLineaCaja { get { return _base.CashLineID; } }
        public virtual string IDMovimientoBanco { get { return _base.BankLineID; } set { _base.BankLineID = value; } }
        public virtual string IDMovimientoContable { get { return _base.AccountingLineID; } }
        public virtual ETipoPago ETipoPago { get { return (ETipoPago)_base.Record.Tipo; } set { _base.Record.Tipo = (long)value; } }
        public virtual string TipoPagoLabel { get { return moleQule.Store.Structs.EnumText<ETipoPago>.GetLabel(ETipoPago); } }
        public virtual string Usuario { get { return _base.Owner; } set { _base.Owner = value; } }
        public virtual DateTime FechaOrdenacion { get { return _base._fecha_ordenacion; } }
        public Decimal InterestCharges { get { return (_payments != null && ETipoPago == ETipoPago.FraccionadoTarjeta) ? _payments.Charges() : 0; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _operations.IsValid
                   && _expenses.IsValid
                   && _payments.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _operations.IsDirty
                   || _expenses.IsDirty
                   || _payments.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual Payment CloneAsNew()
        {
            Payment clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();
            clon.EEstado = EEstado.Abierto;
            clon.SetPagado(); 

            clon.SessionCode = Payment.OpenSession();
            Payment.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);
        }
        public void CopyFrom(IAcreedor parent, ETipoPago tipo)
        {
            ETipoPago = tipo;
            OidAgente = parent.Oid;
            TipoAgente = parent.TipoAcreedor;
            Agente = parent.Nombre;
            Fecha = DateTime.Now;
            Vencimiento = DateTime.Today;

            GetNewCode(tipo, parent.Oid);

            SetMedioPago(parent.MedioPago);
            
            if (moleQule.Common.EnumFunctions.NeedsCuentaBancaria(EMedioPago))
            {
                if (EMedioPago != EMedioPago.Tarjeta)
                {
                    OidCuentaBancaria = parent.OidCuentaBAsociada;
                    CuentaBancaria = parent.CuentaAsociada;
                }
                else
                { 
                    OidTarjetaCredito = parent.OidTarjetaAsociada;
                    TarjetaCredito = parent.TarjetaAsociada;
                    CreditCardInfo tarjeta = CreditCardInfo.Get(OidTarjetaCredito, false);
                    CuentaBancaria = tarjeta.CuentaBancaria;
                    OidCuentaBancaria = tarjeta.OidCuentaBancaria;
                    if (tarjeta.ETipoTarjeta == ETipoTarjeta.Credito) EEstadoPago = EEstado.Pendiente;

                    SetFechas(Fecha, tarjeta);
                }
                SetPagado();
            }

            _operations = TransactionPayments.NewChildList();
        }
        public void CopyFrom(ExpenseInfo source, ETipoPago tipo)
        {
            ETipoPago = tipo;
            OidAgente = source.OidAcreedor;
            ETipoAcreedor = source.ECategoriaGasto == ECategoriaGasto.Nomina ? ETipoAcreedor.Empleado : ETipoAcreedor.Todos;
            Agente = string.Empty;
            Fecha = source.PrevisionPago;
            Vencimiento = source.PrevisionPago;
            Importe = source.Total;
            Observaciones = source.Descripcion;

            GetNewCode(ETipoPago);

            TipoGastoInfo tipo_gasto = TipoGastoInfo.Get(source.OidTipo, false);
            if (tipo_gasto != null)
            {
                OidCuentaBancaria = tipo_gasto.OidCuentaBAsociada;
                CuentaBancaria = tipo_gasto.CuentaAsociada;
                EMedioPago = tipo_gasto.EMedioPago != 0 ? tipo_gasto.EMedioPago : EMedioPago.Seleccione;
                SetPagado();
            }

            _operations = TransactionPayments.NewChildList();

            InsertNewTransactionPayment(source, Importe);
        }
        public void CopyFrom(NominaInfo source)
        {
            ETipoPago = ETipoPago.Nomina;
            OidAgente = source.OidEmpleado;
            ETipoAcreedor = ETipoAcreedor.Empleado;
            Agente = source.Empleado;
            Fecha = source.PrevisionPago;
            Vencimiento = source.PrevisionPago;
            Importe = source.Neto;
            Observaciones = source.Descripcion;

            GetNewCode(ETipoPago);

            TipoGastoInfo tipo_gasto = TipoGastoInfo.Get(source.OidTipo, false);
            if (tipo_gasto != null)
            {
                OidCuentaBancaria = tipo_gasto.OidCuentaBAsociada;
                CuentaBancaria = tipo_gasto.CuentaAsociada;
                EMedioPago = tipo_gasto.EMedioPago;
                SetPagado();
            }

            _operations = TransactionPayments.NewChildList();

            InsertNewTransactionPayment(source, Importe);
        }
        public void CopyFrom(LoanInfo source, DateTime date)
        {
            ETipoPago = ETipoPago.Prestamo;
            OidAgente = source.Oid;
            ETipoAcreedor = ETipoAcreedor.Todos;
            Fecha = date;

            SetCuota(source);

            OidCuentaBancaria = source.OidCuenta;
            EMedioPago = EMedioPago.Transferencia;
            OidUsuario = AppContext.User.Oid;
            CuentaBancaria = source.CuentaBancaria;
            Entidad = source.Entidad;
            Vencimiento = Fecha.Date;

            GetNewCode(ETipoPago);
        }

        internal void GetNewCode() { GetNewCode(ETipoPago); }
        internal void GetNewCode(ETipoPago paymentType) { GetNewCode(paymentType, 0); }
        internal void GetNewCode(ETipoPago paymentType, long oidAgent)
        {
            if (paymentType == ETipoPago.Factura)
            {
				Serial = SerialInfo.GetNextByYear(typeof(Payment), Fecha.Year);
                IdPago = PaymentSerialInfo.GetNext(ETipoAcreedor, oidAgent);
            }
            else
            {
				Serial = SerialInfo.GetNextByYear(typeof(Payment), Fecha.Year); //SerialPagoInfo.GetNext(tipo, Fecha.Year);
                IdPago = 0;
            }

            Codigo = Serial.ToString(Resources.Defaults.PAGO_CODE_FORMAT);

        }
        internal void GetNewCode(ETipoPago paymentType, Payment parent)
        {
			Serial = SerialInfo.GetNextByYear(typeof(Payment), Fecha.Year); 
            IdPago = 0;

            if (parent.ETipoPago == ETipoPago.FraccionadoTarjeta)
            {
                Serial = parent.Serial >= Serial ? parent.Serial + 1 : Serial;

                foreach (Payment pg in parent.Pagos)
                {
                    if (pg != this)
                        Serial = pg.Serial > Serial ? pg.Serial + 1 : Serial;
                }

            }

            Codigo = Serial.ToString(Resources.Defaults.PAGO_CODE_FORMAT);
        }
        internal void GetNewCode(ETipoPago paymentType, Payments parent)
        {
            Serial = SerialInfo.GetNextByYear(typeof(Payment), Fecha.Year);
            IdPago = 0;

            if (ETipoPago == ETipoPago.FraccionadoTarjeta)
            {
                if (parent.IndexOf(this) != 0) //Se respeta el Serial para el primero
                {
                    foreach (Payment pg in parent)
                    {
                        if (pg != this)
                        {
                            Serial = pg.Serial >= Serial ? pg.Serial + 1 : Serial;

                            foreach (Payment item in pg.Pagos)
                                Serial = item.Serial >= Serial ? item.Serial + 1 : Serial;
                        }
                    }
                }
            }

            Codigo = Serial.ToString(Resources.Defaults.PAGO_CODE_FORMAT);
        }

        public void ChangeEstado(EEstado status)
        {
            if (!CanChangeState())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            if ((EEstado == EEstado.Contabilizado || EEstado == EEstado.Exportado) &&
                (!AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE)))
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Common.EntityBase.CheckChangeState(EEstado, status);

            switch (status)
            {
                case EEstado.Anulado:
                    {
                        switch (EMedioPago)
                        {
                            case EMedioPago.Efectivo:
                                {
                                    CashLineInfo line = CashLineInfo.GetByPago(Oid);

                                    if (line.IsInCashCount)
                                        throw new iQValidationException(String.Format(Library.Store.Resources.Messages.CIERRE_CAJA_PAGO, line.Codigo, line.Fecha));

                                    if (line.Oid != 0)
                                    {
                                        moleQule.Cash.Cash cash = moleQule.Cash.Cash.Get(1, true);
                                        cash.Lines.Remove(line.Oid);
                                        cash.UpdateSaldo();
                                        cash.Save();
                                    }
                                }
                                break;

                            case EMedioPago.Tarjeta:
                                SetRoots(null, null);
                                break;
                        }

                        foreach (Payment pg in Pagos)
                            pg.Base.CopyValues(ChangeEstado(pg.Oid, EEstado.Anulado, SessionCode));

                        AnnulRelateds(GetInfo(false), SessionCode);
                    }
                    break;
            }

            EEstado = status;
        }
        public static Payment ChangeEstado(long oid, EEstado status, int sessionCode)
        {
            Payment item = null;

            try
            {
                item = Payment.Get(oid, true, sessionCode);

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) &&
                    (!AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE)))
                    throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

                switch (status)
                {
                    case EEstado.Anulado:
                        {
                            switch (item.EMedioPago)
                            {
                                case EMedioPago.Efectivo:
                                    {
                                        CashLineInfo line = CashLineInfo.GetByPago(item.Oid);

                                        if (line != null && (line.Oid != 0) &&
                                            (line.OidCierre != 0) &&
                                            (line.EEstado != EEstado.Anulado))
                                            throw new iQValidationException(String.Format(Resources.Messages.CIERRE_CAJA_PAGO, line.Codigo, line.Fecha));

                                        moleQule.Cash.Cash cash = moleQule.Cash.Cash.Get(1, true, sessionCode);
                                        cash.Lines.Remove(line.Oid);
                                        cash.UpdateSaldo();
                                        cash.Save();
                                    }
                                    break;

                                case EMedioPago.Tarjeta:
                                    item.SetRoots(null, null);
                                    break;
                            }

                            foreach (Payment pg in item.Pagos)
                                ChangeEstado(pg.Oid, EEstado.Anulado, item.SessionCode);
                        }
                        break;
                }

                Common.EntityBase.CheckChangeState(item.EEstado, status);

                item.BeginEdit();
                item.EEstado = status;
                item.ApplyEdit();
                item.Save();
            }
            finally
            {
                if (item != null && !item.SharedTransaction) item.CloseSession();
            }

            return item;
        }
        public static Payment ChangeEstado(long oid, ETipoPago paymentType, ETipoAcreedor providerType, EEstado status, int sessionCode = -1)
        {
            if (!CanChangeState())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment item = null;

            try
            {
                item = Payment.Get(oid, paymentType, providerType, true, sessionCode);

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) &&
                    (!AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE)))
                    throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

                switch (status)
                {
                    case EEstado.Anulado:
                        {
                            switch (item.EMedioPago)
                            {
                                case EMedioPago.Efectivo:
                                    {
                                        CashLineInfo line = CashLineInfo.GetByPago(item.Oid);

                                        if (line != null && (line.Oid != 0) &&
                                            (line.OidCierre != 0) &&
                                            (line.EEstado != EEstado.Anulado))
                                            throw new iQValidationException(String.Format(Library.Store.Resources.Messages.CIERRE_CAJA_PAGO, line.Codigo, line.Fecha));

                                        moleQule.Cash.Cash cash = moleQule.Cash.Cash.Get(1, true, sessionCode);
                                        cash.Lines.Remove(line.Oid);
                                        cash.UpdateSaldo();
                                        cash.Save();
                                    }
                                    break;

                                case EMedioPago.Tarjeta:
                                    item.SetRoots(null, null);
                                    break;
                            }

                            foreach (Payment pg in item.Pagos)
                                pg.Base.CopyValues(ChangeEstado(pg.Oid, EEstado.Anulado, item.SessionCode));
                        }
                        break;
                }

                Common.EntityBase.CheckChangeState(item.EEstado, status);

                item.BeginEdit();
                item.EEstado = status;
                item.ApplyEdit();
                item.Save();
            }
            finally
            {
                if (item != null && !item.SharedTransaction) item.CloseSession();
            }

            return item;
        }

        public TransactionPayment InsertNewTransactionPayment(ITransactionPayment item, decimal maximo)
        {
            //Se le ha asignado algo a mano
            if (item.Asignado != 0)
            {
                if (EMedioPago != EMedioPago.CompensacionFactura)
                {
                    //Pagos en positivo
                    if (Importe >= 0)
                    {
                        if (item.Asignado > maximo) item.Asignado = maximo;
                        if (item.Asignado > item.PendienteAsignar) item.Asignado = item.PendienteAsignar;
                    }
                    //Pagos en negativo. Abonos
                    else
                    {
                        if (item.Asignado < maximo) item.Asignado = maximo;
                        if (item.Asignado < item.PendienteAsignar) item.Asignado = item.PendienteAsignar;
                    }
                }
            }
            else
            {
                if (EMedioPago != EMedioPago.CompensacionFactura)
                {
                    //Pagos en positivo
                    if (Importe >= 0)
                    {
                        if (item.PendienteAsignar <= maximo) item.Asignado = item.PendienteAsignar;
                        else item.Asignado = maximo;
                    }
                    //Pagos en negativo. Abonos
                    else
                    {
                        if (item.PendienteAsignar >= maximo) item.Asignado = item.PendienteAsignar;
                        else item.Asignado = maximo;
                    }
                }
                else
                {
                    item.Asignado = item.PendienteAsignar;
                }
            }

            TransactionPayment tr_payment = _operations.GetItemByITransaction(item, ETipoPago);

            if (tr_payment == null)
            {
                tr_payment = _operations.NewItem(this, item, ETipoPago);
                item.FechaAsignacion = DateTime.Now.ToShortDateString();
                item.TotalPagado += tr_payment.Cantidad;
                item.PendienteAsignar -= tr_payment.Cantidad;
                /*if (EEstadoPago == EEstado.Pagado)*/ //item.Pendiente -= pago_factura.Cantidad;
            }
            else
            {
                item.TotalPagado -= tr_payment.Cantidad;
                item.PendienteAsignar += tr_payment.Cantidad;
                //if (EEstadoPago == EEstado.Pagado) item.Pendiente += pago_factura.Cantidad;

                tr_payment.CopyFrom(this, item, ETipoPago);

                item.TotalPagado += tr_payment.Cantidad;
                item.PendienteAsignar -= tr_payment.Cantidad;
                /*if (EEstadoPago == EEstado.Pagado)*/ //item.Pendiente -= pago_factura.Cantidad;

                item.FechaAsignacion = DateTime.Now.ToShortDateString();
            }

            return tr_payment;
        }
        public void DeleteTransactionPayment(ITransactionPayment item)
        {
            item.Asignado = 0;

            TransactionPayment pago_factura = _operations.GetItemByITransaction(item, ETipoPago);
            if (pago_factura != null)
            {
                _operations.Remove(pago_factura);
                item.TotalPagado -= pago_factura.Cantidad;
                item.PendienteAsignar += pago_factura.Cantidad;

                /*if (EEstadoPago == EEstado.Pagado)*/ //item.Pendiente += pago_factura.Cantidad;
            }
        }
        public void EditTransactionPayment(ITransactionPayment item, decimal amount)
        {
            TransactionPayment pago_factura = _operations.GetItemByITransaction(item, ETipoPago);
            if (pago_factura != null) pago_factura.Cantidad = 0;

            item.PendienteAsignar += item.Asignado;

            //if (EEstadoPago == EEstado.Pagado)
            {
                item.TotalPagado -= item.Asignado;
                //item.Pendiente += item.Asignado;
            }

            item.Asignado = amount;
        }

        public void SetFechas(DateTime date, CreditCardInfo creditCard)
        {
            Fecha = date;

            Vencimiento = (EMedioPago == EMedioPago.Tarjeta && creditCard != null)
                            ? moleQule.Common.EnumFunctions.GetPrevisionPago(creditCard.EFormaPago, Fecha, creditCard.DiasPago, creditCard.DiaExtracto)
                            : Fecha;

            SetPagado();
        }
        public void SetMedioPago(long paymentMethod)
        {
            MedioPago = paymentMethod;

            switch (EMedioPago)
            {
                case EMedioPago.Cheque:
                case EMedioPago.Efectivo:
                case EMedioPago.Ingreso:
                case EMedioPago.Domiciliacion:
                case EMedioPago.Transferencia:
                case EMedioPago.Tarjeta:
                case EMedioPago.Pagare:
                case EMedioPago.Giro:
                    {
                        EEstadoPago = (Vencimiento.Date <= DateTime.Today.Date) ? EEstado.Pagado : EEstado.Pendiente;
                    }
                    break;

                case EMedioPago.ComercioExterior:
                    {
                        EEstadoPago = EEstado.Pagado;
                    }
                    break;

                case EMedioPago.CompensacionFactura:
                    {
                        Importe = 0;
                        EEstadoPago = EEstado.Pagado;
                    }
                    break;
            }

            if (!moleQule.Common.EnumFunctions.NeedsCuentaBancaria(EMedioPago))
            {
                OidCuentaBancaria = 0;
                CuentaBancaria = string.Empty;
            }
        }
        public void SetPagado()
        {
            switch (EMedioPago)
            {
                case EMedioPago.Cheque:
                case EMedioPago.Efectivo:
                case EMedioPago.Ingreso:
                case EMedioPago.Domiciliacion:
                case EMedioPago.Transferencia:
                case EMedioPago.Tarjeta:
                case EMedioPago.Pagare:
                case EMedioPago.Giro:
                    {
                        CreditCardInfo tarjeta = CreditCardInfo.Get(OidTarjetaCredito, false);

                        if (tarjeta != null && tarjeta.Oid != 0 && tarjeta.ETipoTarjeta == ETipoTarjeta.Credito)
                            EEstadoPago = EEstado.Pagado;
                        else if (ETipoPago != ETipoPago.Prestamo)
                            EEstadoPago = (Vencimiento.Date <= DateTime.Today.Date) ? EEstado.Pagado : EEstado.Pendiente;
                    }
                    break;
            }
        }
        public void SetVencimiento(DateTime dueDate)
        {
            Vencimiento = dueDate;
            SetPagado();
        }

        public void GeneraPagosFraccionados(int nPayments, int frequency, ETipoPago paymentType)
        {
            //Se eliminan todos los pagos creados con anterioridad si existía alguno
            Pagos.RemoveAll();

            for (int i = 0; i < nPayments; i++)
            {
                Payment fraccion = Pagos.NewItem(this.GetInfo(true));
                fraccion.Fecha = Fecha;
                fraccion.Vencimiento = Vencimiento.AddMonths(frequency * i);
                fraccion.ETipoPago = paymentType;
                fraccion.Importe = 0;
                fraccion.Observaciones = Observaciones + " -  PLAZO " + (i + 1).ToString() + "/" + nPayments.ToString();
                fraccion.SetPagado();

                foreach (TransactionPayment item in Operations)
                {
                    TransactionPayment pf = TransactionPayment.NewChild();
                    pf.MarkItemChild();
                    pf.CopyFrom(item, nPayments - i);
                    pf.ETipoPago = paymentType;
                    fraccion.Importe += pf.Cantidad;
                    fraccion.Operations.NewItem(pf);
                    item.Cantidad -= pf.Cantidad;
                }

                fraccion.Pendiente = 0;
            }

            Operations.RemoveAll();
        }
        public void GeneraPagosFraccionados(PaymentList payments, int nPayments, int frequency, ETipoPago paymentType)
        {
            //Se eliminan todos los pagos creados con anterioridad si existía alguno
            Pagos.RemoveAll();

            foreach (PaymentInfo item in payments)
                item.Pendiente = item.Importe + item.GastosBancarios;

            for (int i = 0; i < nPayments; i++)
            {
                Payment fraccion = Pagos.NewItem(this.GetInfo(true));
                fraccion.Fecha = Fecha;
                fraccion.Vencimiento = Vencimiento.AddMonths(frequency * i);
                fraccion.ETipoPago = paymentType;
                fraccion.Importe = 0;
                fraccion.Observaciones = Observaciones + " -  PLAZO " + (i + 1).ToString() + "/" + nPayments.ToString();
                fraccion.SetPagado();

                foreach (PaymentInfo item in payments)
                {
                    TransactionPayment pf = TransactionPayment.NewChild();
                    pf.MarkItemChild();
                    pf.CopyFrom(item, nPayments - i);
                    pf.ETipoPago = paymentType;
                    fraccion.Importe += pf.Cantidad;
                    fraccion.Operations.NewItem(pf);
                    item.Pendiente -= pf.Cantidad;
                }

                fraccion.Pendiente = 0;
            }
        }

        protected void SetRoots(Payment parent, PaymentRecord oldPayment)
        {
            OidRoot = (parent != null) ? parent.Oid : 0;

            switch (ETipoPago)
            {
                case ETipoPago.Fraccionado:
                    OidLink = 0;
                    break;

                case ETipoPago.ExtractoTarjeta:
                    OidLink = 0;
                    break;

                default:

                    if (EMedioPago == EMedioPago.Tarjeta)
                    {
                        //No tiene extracto asociado
                        if (OidLink == 0)
                        {
                            CreditCardStatement st = CreditCardService.GetOrCreateStatementFromOperationDueDate(OidTarjetaCredito, Vencimiento, SessionCode);

                            if (st.OidCreditCard == 0) return;

                            st.Amount = st.Amount + Importe + GastosBancarios;
                            st.Save();

                            OidLink = st.Oid;
                        }
                        else 
                        {
                            CreditCardStatement st = CreditCardService.GetOrCreateStatementFromOperationDueDate(OidTarjetaCredito, Vencimiento, SessionCode);

                            if (st.OidCreditCard == 0) return;

                            //Se ha eliminado el pago
                            if (oldPayment == null)
                            {
                                st.Amount = st.Amount - (Importe + GastosBancarios);
                                st.Save();
                            }
                            else
                            {
                                //No ha cambiado el extracto asociado
                                if (OidLink == st.Oid)
                                {
                                    st.Amount = st.Amount - (oldPayment.Importe + oldPayment.GastosBancarios) + Importe + GastosBancarios;
                                    st.Save();
                                }
                                else
                                {
                                    st.Amount = st.Amount + Importe + GastosBancarios;
                                    st.Save();

                                    CreditCardStatement old_st = CreditCardService.GetOrCreateStatementFromOperationDueDate(oldPayment.OidTarjetaCredito, oldPayment.Vencimiento, SessionCode);

                                    if (st.OidCreditCard == 0) return;

                                    old_st.Amount = old_st.Amount - (oldPayment.Importe + oldPayment.GastosBancarios);
                                    old_st.Save();

                                    OidLink = st.Oid;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void SetCuota(LoanInfo source)
        {
            InterestRateInfo tipo_interes = null;

            if (source.InterestRates != null)
            {
                foreach (InterestRateInfo tipo in source.InterestRates)
                {
                    if (tipo.FechaInicio <= Fecha && tipo.FechaFin >= Fecha)
                    {
                        tipo_interes = tipo;
                        break;
                    }
                }

                if (tipo_interes != null)
                {
                    Importe = Decimal.Round(tipo_interes.ImporteCuota / (1 + tipo_interes.Tipo / 100), 2);
                    GastosBancarios = Decimal.Round(tipo_interes.ImporteCuota - Importe, 2);
                }
                else
                {
                    Importe = source.ImporteCuota;
                    GastosBancarios = 0;
                }
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
            //Agente
            switch (ETipoPago)
            {
                case ETipoPago.Factura:
                    e.Description = Resources.Messages.NO_AGENTE_SELECTED;
                    if (OidAgente == 0) throw new iQValidationException(e.Description, string.Empty);
                    break;
            }

            //Medio Pago
            if ((EMedioPago == EMedioPago.Seleccione) ||
                (EMedioPago == EMedioPago.Todos))
            {
                e.Description = Resources.Messages.NO_MEDIO_PAGO_SELECTED;
                throw new iQValidationException(e.Description, string.Empty);
            }

            //Cuenta Bancaria
            switch (EMedioPago)
            {
                case EMedioPago.Cheque:
                case EMedioPago.Giro:
                case EMedioPago.Ingreso:
                case EMedioPago.Pagare:
                case EMedioPago.Tarjeta:
                case EMedioPago.Transferencia:
                case EMedioPago.Domiciliacion:
                case EMedioPago.ComercioExterior:
                    e.Description = Resources.Messages.NO_CUENTA_BANCARIA_SELECTED;
                    if (OidCuentaBancaria == 0) throw new iQValidationException(e.Description, string.Empty);
                    break;
            }

            //Tarjeta de Credito
            switch (EMedioPago)
            {
                case EMedioPago.Tarjeta:
                    e.Description = Resources.Messages.NO_TARJETA_CREDITO_SELECTED;
                    if (OidTarjetaCredito == 0) throw new iQValidationException(e.Description, string.Empty);
                    break;
            }

            return true;
        }

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }
        public static bool CanChangeState()
        {
            return AutorizationRulesControler.CanGetObject(moleQule.Common.Resources.SecureItems.ESTADO);
        }

        #endregion

        #region Common Factory Methods

        protected Payment()
        {
            Random r = new Random();
            Oid = (long)r.Next();
            _base.Record.Fecha = DateTime.Now;
            _base.Record.MedioPago = (long)EMedioPago.Seleccione;
            EEstado = EEstado.Abierto;
            EEstadoPago = EEstado.Pendiente;
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
        }

        public PaymentInfo GetInfo(bool childs = true) { return new PaymentInfo(this, childs); }

        private void AnnulRelateds(PaymentInfo source, int sessionCode)
        {
            //Anulamos la Linea de Caja 
            moleQule.Cash.Cash.AnnulItem(this, SessionCode);

            switch (ETipoPago)
            {
                case ETipoPago.Factura:
                case ETipoPago.Gasto:
                case ETipoPago.Nomina:

                    switch (EMedioPago)
                    {
                        case EMedioPago.ComercioExterior:

                            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
                            {
                                IBankLine = (IBankLineInfo)source,
                                Estado = EEstado.Abierto,
                                Payment = source,
                                LoanType = ELoanType.Merchant
                            };

                            Loans loans = Loans.GetList(conditions, sessionCode);

                            if (loans != null && loans.Count > 0)
                            {
                                loans[0].LoadChilds(typeof(Payment), true);
                                loans[0].ChangeEstado(EEstado.Anulado);
                                loans.SaveAsChild();
                            }

                            break;
                    }
                    break;
            }

            BankLine.BankLine.AnnulItem(source, sessionCode);
        }

        private void EditRelateds(Payment source, int sessionCode)
        {
            //Se encarga AnnulRelateds
            if (source.EEstado == EEstado.Anulado) return;

            if ((EMedioPago == EMedioPago.Tarjeta) || (source.EMedioPago == EMedioPago.Tarjeta))
            {
                BankLine.BankLine.EditItemTarjeta(source.GetInfo(false), GetInfo(false), source.SessionCode);
            }
            else
            {

                switch (source.ETipoPago)
                {
                    case ETipoPago.Factura:
                    case ETipoPago.Gasto:
                    case ETipoPago.Nomina:

                        switch (EMedioPago)
                        {
                            case EMedioPago.ComercioExterior:
                                {
                                    Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
                                    {
                                        Estado = EEstado.Abierto,
                                        Payment = source.GetInfo(false),
                                        LoanType = ELoanType.Merchant
                                    };

                                    Loans loans = Loans.GetList(conditions, sessionCode);

                                    if (loans != null && loans.Count > 0)
                                    {
                                        Loan loan = loans[0];
                                        loan.CopyFrom(source.GetInfo(false));
                                        loans.SaveAsChild();

                                        BankAccountInfo bank_account = BankAccountInfo.Get(OidCuenta, false);
                                        source.Tags = new Dictionary<string, object> { { "OidMainBankAccount", bank_account.OidCuentaAsociada } };
                                    }
                                    else
                                    {
                                        //Por compatibilidad hacia atrás avisamos a la linea de banco que es un pago viejo
                                        source.Tags = new Dictionary<string, object>() { { "Deprecated", true } };
                                    }
                                }
                                break;

                            default:
                                {
                                    BankLine.QueryConditions conditions = new BankLine.QueryConditions
                                    {
                                        IBankLine = source.GetInfo(false),
                                        Estado = EEstado.Abierto,
                                        TipoTitular = source.ETipoTitular
                                    };

                                    BankLine.BankLineInfo bankline = BankLine.BankLineInfo.Get(conditions, false);

                                    //No tiene linea de banco asociada porque estaba pendiente de pago cuando se creó
                                    if (bankline == null) break;

                                    if (bankline.EMedioPago == EMedioPago.ComercioExterior && source.EMedioPago != EMedioPago.ComercioExterior)
                                    {
                                        Loans loans = Loans.GetList(Library.Store.QueryConditions.ConvertTo(conditions), sessionCode);
                                        Loan pr = loans[0];
                                        PaymentList payments = PaymentList.GetListByPrestamo(pr.GetInfo(), false);
                                        if (payments.Count > 0)
                                            throw new iQException("No es posible modificar un préstamo con pagos asociados");

                                        pr.CopyFrom(source.GetInfo());
                                        loans.SaveAsChild();
                                    }
                                }
                                break;
                        }
                        break;

                    case ETipoPago.Prestamo:

                        //El movimiento de banco necesita la cuenta corriente asociada al prestamo
                        source.Tags = new Dictionary<string, object>() { { "Loan", LoanInfo.Get(source.OidAgente) } };
                        Tags = source.Tags;

                        break;
                }

                BankLine.BankLine.EditItem(source, GetInfo(false), source.SessionCode);
            }

            //Editamos o Anulamos la Linea de Caja si la hubiera
            moleQule.Cash.Cash.EditItem(source, GetInfo(false), source.SessionCode);
        }

        private void InsertRelateds()
        {
            Tags = new Dictionary<string, object>();

            switch (ETipoPago)
            {
                case ETipoPago.Factura:
                case ETipoPago.Gasto:
                case ETipoPago.Nomina:

                    switch (EMedioPago)
                    {
                        case EMedioPago.ComercioExterior:

                            //Al pagar con una cuenta de comercio exterior se genera automáticamente 
                            //un préstamo de comercio exterior y un pago asociado a fecha de vencimiento del prestamo
                            Loans loans = Loans.NewList();
                            loans.SessionCode = SessionCode;
                            Loan loan = loans.NewItem(GetInfo(false));
                            loan.FechaVencimiento = Vencimiento;

                            loans.SaveAsChild();

                            // Hay que hacerlo aquí porque antes no tenemos el OID del Loan hasta
                            // que lo guardamos y al guardar las Operaciones del pago se queda a 0 el OID_OPERACION
                            Payment payment = loan.Payments.NewItem(loan.GetInfo(false));
                            payment.Importe = loan.Importe;
                            payment.Fecha = loan.Vencimiento;
                            payment.Vencimiento = loan.Vencimiento;
                            TransactionPayment tr = payment.InsertNewTransactionPayment(loan, loan.Importe);
                            tr.Cantidad = loan.Importe;

                            //Le asociamos al pago de cancelación la cuenta asociada a la de comercio exterior
                            BankAccountInfo bank_account = BankAccountInfo.Get(loan.OidCuenta);
                            payment.OidCuentaBancaria = bank_account.OidCuentaAsociada;

                            loans.SaveAsChild();

                            break;
                    }
                    break;

                case ETipoPago.Prestamo:

                    //El movimiento de banco necesita la cuenta corriente asociada al prestamo
                    Tags.Add("Loan", LoanInfo.Get(OidAgente));

                    break;
            }

            switch (EMedioPago)
            {
                case EMedioPago.Efectivo:
                    
                    //Insertamos la linea de caja asociada
                    moleQule.Cash.Cash.InsertItem(this, SessionCode);
                    IDMovimientoBanco = string.Empty;
                    break;

                case EMedioPago.Tarjeta:

                    BankLine.BankLine bank_line = BankLine.BankLine.New();
                    bank_line.Serial = 0;

                    CreditCardInfo credit_card = CreditCardInfo.Get(OidTarjetaCredito, false);

                    if (credit_card.ETipoTarjeta == ETipoTarjeta.Debito)
                        bank_line = BankLine.BankLine.InsertItem(this, Confirmado, SessionCode);

                    IDMovimientoBanco = bank_line.Codigo;
                    LastBankLineSerial = bank_line.Serial;

                    break;

                default:

                    BankAccountInfo bank_account = BankAccountInfo.Get(OidCuenta, false);
                    Tags.Add("OidMainBankAccount", bank_account.OidCuentaAsociada);
                    IDMovimientoBanco = BankLine.BankLine.InsertItem(this, SessionCode).Codigo;
                    break;
            }

            Tags.Clear();
        }

        public void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(TransactionPayment)))
            {
                _operations = TransactionPayments.GetChildList(this, childs);
            }
        }

        #endregion

        #region Root Factory Methods

        public static Payment New(ETipoPago paymentType)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = DataPortal.Create<Payment>(new CriteriaCs(-1));
            obj.ETipoPago = paymentType;
            obj.GetNewCode(paymentType);
            return obj;
        }
        public static Payment New(ExpenseInfo source, ETipoPago paymentType)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = DataPortal.Create<Payment>(new CriteriaCs(-1));
            obj.CopyFrom(source, paymentType);
            return obj;
        }
        public static Payment New(NominaInfo source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = DataPortal.Create<Payment>(new CriteriaCs(-1));
            obj.CopyFrom(source);
            return obj;
        }
        public static Payment New(LoanInfo source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = DataPortal.Create<Payment>(new CriteriaCs(-1));
            obj.CopyFrom(source, DateTime.Now);
            return obj;
        }

        public static Payment Get(long oid, bool childs = true) { return Get(oid, ETipoPago.Todos, ETipoAcreedor.Todos, childs); }
        public static Payment Get(long oid, ETipoAcreedor paymentType) { return Get(oid, ETipoPago.Factura, paymentType, true); }
        public static Payment Get(long oid, ETipoAcreedor paymentType, bool childs) { return Get(oid, ETipoPago.Factura, paymentType, childs); }
        public static Payment Get(long oid, ETipoPago paymentType) { return Get(oid, paymentType, ETipoAcreedor.Todos, true); }
        public static Payment Get(long oid, ETipoPago paymentType, int sessionCode) { return Get(oid, paymentType, ETipoAcreedor.Todos, true, sessionCode); }
        public static Payment Get(long oid, ETipoPago paymentType, bool childs) { return Get(oid, paymentType, ETipoAcreedor.Todos, childs); }
        public static Payment Get(long oid, ETipoPago paymentType, ETipoAcreedor providerType, bool childs, int sessionCode = -1)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return Get(Payment.SELECT(oid, paymentType, providerType), childs, sessionCode);
        }

        public static Payment GetByCreditCardDueDate(CreditCardInfo creditCard, DateTime dueDate, bool childs, int sessionCode)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Payment.GetCriteria(sessionCode);
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions() 
            {
                FechaIni = dueDate,
                FechaFin = dueDate,
                PaymentType = ETipoPago.FraccionadoTarjeta,
                MedioPago = EMedioPago.Tarjeta,
                TarjetaCredito = creditCard
            };

            criteria.Query = Payment.SELECT(conditions, true);

            Payment.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<Payment>(criteria);
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

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todos los Despachante. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Payment.OpenSession();
            ISession sess = Payment.Session(sessCode);
            ITransaction trans = Payment.BeginTransaction(sessCode);

            try
            {
				sess.Delete("from PaymentRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                Payment.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override Payment Save()
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

                _operations.Update(this);
                _payments.Update(this);

                if (!SharedTransaction) Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback(); 
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction) 
                {
                    if (CloseSessions) CloseSession();
                    else BeginTransaction();
                }
            }

            return this;
        }

        public static void UpdatePagadoFromList(List<PaymentInfo> list, bool payed)
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Payment.OpenSession();
            ITransaction trans = Payment.BeginTransaction(Payment.Session(sessCode));

            try
            {
                List<long> oids = new List<long>();

                oids.Add(0);

                foreach (PaymentInfo item in list)
                    oids.Add(item.Oid);

                nHManager.Instance.SQLNativeExecute(UPDATE_PAGADO(oids, payed));

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                Payment.CloseSession(sessCode);
            }
        }

        #endregion

        #region Child Factory Methods

        private Payment(Payment source)
        {
            MarkAsChild();
            Fetch(source);
        }
        private Payment(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            SessionCode = sessionCode;
            Fetch(reader);
        }
        public static Payment NewChild(IAcreedor parent, ETipoPago tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = new Payment();
            obj.MarkAsChild();
            obj.CopyFrom(parent, tipo);
            obj.GetNewCode(tipo, parent.Oid);

            if (parent.Pagos.Count > 0)
            {
                SortedBindingList<PaymentInfo> sortedList = new SortedBindingList<PaymentInfo>(PaymentList.GetChildList(parent.Pagos, false));
                sortedList.ApplySort("IdPago", ListSortDirection.Ascending);
                Int64 lastid = sortedList[sortedList.Count - 1].IdPago;
                obj.IdPago = lastid + 1;
            }
            else
                obj.IdPago = 1;

            return obj;
        }
        public static Payment NewChild(PaymentInfo pago)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = new Payment();
            obj.MarkAsChild();
            obj._base.CopyValues(pago);

            return obj;
        }
        public static Payment NewChild(PaymentInfo pago, ETipoPago tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = new Payment();
            obj.MarkAsChild();

            obj.OidTarjetaCredito = pago.OidTarjetaCredito;
            obj.OidCuentaBancaria = pago.OidCuentaBancaria;
            obj.ETipoPago = tipo;
            obj.Fecha = pago.Vencimiento;
            obj.Importe = pago.Importe;
            obj.MedioPago = pago.MedioPago;
            obj.Vencimiento = pago.Vencimiento;
            obj.GastosBancarios = pago.GastosBancarios;
            obj.OidUsuario = AppContext.User.Oid;
            obj.OidRoot = 0;
            obj.CuentaBancaria = pago.CuentaBancaria;
            obj.Entidad = pago.Entidad;
            obj.TarjetaCredito = pago.TarjetaCredito;
            obj.GetNewCode();

            return obj;
        }
        public static Payment NewChild(LoanInfo source, DateTime date)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Payment obj = new Payment();
            obj.MarkAsChild();

            obj.CopyFrom(source, date);

            return obj;
        }

        internal static Payment GetChild(Payment source) { return new Payment(source); }
        internal static Payment GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, true); }
        internal static Payment GetChild(int sessionCode, IDataReader reader, bool childs) { return new Payment(sessionCode, reader, childs); }

        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            _base.Record.Oid = (long)(new Random()).Next();
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
            Fecha = DateTime.Now;
            EMedioPago = EMedioPago.Seleccione;
            Vencimiento = DateTime.Today;
            EEstado = EEstado.Abierto;
            EEstadoPago = EEstado.Pendiente;

            _operations = TransactionPayments.NewChildList();
        }

        private void Fetch(IDataReader source)
        {
            string query = string.Empty;

            try
            {
                CopyValues(source);

                if (Childs)
                {                    
                    IDataReader reader;

                    TransactionPayment.DoLOCK(Session());
                    query = TransactionPayments.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _operations = TransactionPayments.GetChildList(SessionCode, reader, false);

                    if (ETipoPago == ETipoPago.Fraccionado)
                    {
                        Payment.DoLOCK(Session());
                        query = Payment.SELECT_BY_ROOT(Oid, true);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _payments = Payments.GetChildList(SessionCode, reader, false);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { query });
            }

            MarkOld();
        }

        internal void Insert(Payments parent)
        {
            try
            {
                // if we're not dirty then don't update the database
                if (!this.IsDirty) return;

                ValidationRules.CheckRules();

                SessionCode = parent.SessionCode;

                GetNewCode(ETipoPago, parent);
                OidUsuario = AppContext.User.Oid;
                Usuario = AppContext.User.Name;

                SetRoots(null, Base.Record);

                parent.Session().Save(Base.Record);

                _operations.Update(this);
                _payments.Update(this);

                MarkOld();
            }
            catch (Exception ex)
            {
                throw new iQException(ex, String.Format(moleQule.Resources.Errors.INSERT
                                                            ,moleQule.Common.Structs.EnumText<ETipoEntidad>.GetLabel(moleQule.Common.Structs.ETipoEntidad.Pago)
                                                            ,Codigo));
            }
        }

        internal void Update(Payments parent)
        {
            try
            {
                // if we're not dirty then don't update the database
                if (!this.IsDirty) return;

                ValidationRules.CheckRules();

                SessionCode = parent.SessionCode;
                PaymentRecord record = Session().Get<PaymentRecord>(Oid);

                SetRoots(null, record);

                record.CopyValues(Base.Record);
                Session().Update(record); 
            
                _operations.Update(this);
                _payments.Update(this);

                MarkOld();
            }          
            catch (Exception ex)
            {
                throw new iQException(ex, String.Format(moleQule.Resources.Errors.UPDATE
                                                            ,moleQule.Common.Structs.EnumText<ETipoEntidad>.GetLabel(moleQule.Common.Structs.ETipoEntidad.Pago)
                                                            ,Oid));
            }
        }

        internal void DeleteSelf(Payments parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;

                SetRoots(null, null);
                   
                Session().Delete(Session().Get<PaymentRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            string query = string.Empty;

            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;
                query = criteria.Query;

                if (nHMng.UseDirectSQL)
                {
                    Payment.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(query, Session());

                    if (reader.Read())
                        CopyValues(reader);

                    if (Childs)
                    {
                        switch (ETipoPago)
                        {
                            case ETipoPago.Fraccionado:
                                {
                                    Payment.DoLOCK(Session());
									query = Payment.SELECT_BY_ROOT(Oid, true);
                                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                    _payments = Payments.GetChildList(SessionCode, reader, true);

                                    TransactionPayment.DoLOCK(Session());
                                    query = TransactionPayments.SELECT(this);
                                    reader = nHMng.SQLNativeSelect(query, Session());
                                    _operations = TransactionPayments.GetChildList(SessionCode, reader, false);
                                } break;

                            case ETipoPago.FraccionadoTarjeta:
                                {
                                    Payment.DoLOCK(Session());
                                    query = Payment.SELECT_BY_ROOT(Oid, true);
                                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                    _payments = Payments.GetChildList(SessionCode, reader, true);
                                }
                                break;

                            default:
                                {
                                    TransactionPayment.DoLOCK(Session());
                                    query = TransactionPayments.SELECT(this);
                                    reader = nHMng.SQLNativeSelect(query, Session());
                                    _operations = TransactionPayments.GetChildList(SessionCode, reader, false);
                                }
                                break;
                        }
                    }
                }

                MarkOld();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { query });
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

                GetNewCode(ETipoPago, this.OidAgente);
                OidUsuario = AppContext.User.Oid;
                Usuario = AppContext.User.Name;

                SetRoots(null, Base.Record);

                Session().Save(Base.Record);

                //Insertamos la linea de caja o el movimiento de banco asociado
                moleQule.Cash.Cash.InsertItem(this, SessionCode);

                IDMovimientoBanco = BankLine.BankLine.InsertItem(this, SessionCode).Codigo;
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (!IsDirty) return;

            try
            {
                PaymentRecord record = Session().Get<PaymentRecord>(Oid);

                try
                {
                    //Si no tratamos de cambiar el estado chequeamos que podamos actualizar el objeto
                    if ((EEstado)record.Estado == this.EEstado)
                        moleQule.Common.EntityBase.CheckEditLockedEstado((EEstado)record.Estado, EEstado.Contabilizado);
                }
                catch
                {
                    return;
                }

                SetRoots(null, record);

                Payment obj = Payment.Get(Oid, (ETipoPago)record.Tipo, SessionCode);

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
                }
            }
            catch (iQInfoException ex)
            {
                if (ex.Code != iQExceptionCode.WARNING) throw ex;
            }
        }

        //Deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criteria)
        {
            try
            {
                PaymentInfo obj = PaymentInfo.Get(criteria.Oid);

                // Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Nunca se borran, se anulan
                EEstado = EEstado.Anulado;

                SetRoots(null, null);

                AnnulRelateds(obj, SessionCode);
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                CloseSession();
            }
        }

        #endregion

        #region Child Data Access

        private void Fetch(Payment source)
        {
            string query = string.Empty;

            try
            {
                SessionCode = source.SessionCode;
				_base.CopyValues(source);

                if (Childs)
                {
                    IDataReader reader = null;

                    switch (ETipoPago)
                    {
                        case ETipoPago.Factura:
                            {
                                TransactionPayment.DoLOCK(Session());
                                query = TransactionPayments.SELECT(this);
                                reader = nHMng.SQLNativeSelect(query, Session());
                                _operations = TransactionPayments.GetChildList(SessionCode, reader, false);
                            } break;

                        case ETipoPago.Fraccionado:
                            {
                                Payment.DoLOCK(Session());
								query = Payment.SELECT_BY_ROOT(Oid, true);
                                reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                _payments = Payments.GetChildList(SessionCode, reader, false);
                            } break;

                        default:
                            {
                                Expense.DoLOCK(Session());
                                query = Expenses.SELECT(this);
                                reader = nHMng.SQLNativeSelect(query, Session());
                                _expenses = Expenses.GetChildList(SessionCode, reader, false);
                            } break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { query });
            }

            MarkOld();
        }

        internal void Insert(IAcreedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;
            OidAgente = parent.Oid;

            GetNewCode(ETipoPago, Base.Record.OidAgente);
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
            SetRoots(null, Base.Record);

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);
            _operations.Update(this);
            _payments.Update(this);

            InsertRelateds();

            MarkOld();
        }

        internal void Update(IAcreedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            Payment obj = null;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;
            OidAgente = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            PaymentRecord record = Session().Get<PaymentRecord>(this.Oid);

            SetRoots(null, record);

            obj = Payment.Get(this.Oid, true, SessionCode);

            try
            {
                if (Common.EntityBase.UpdateByYear(obj, this, parent))
                {
                    obj.Save();

                    _operations.Update(this);
                    //Para los pagos fraccionados
                    _payments.Update(this);

                    parent.Transaction().Commit();
                    parent.NewTransaction();
                    parent.LoadChilds(typeof(Payment), true);
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    parent.Session().Update(record);
                    _operations.Update(this);
                    //Para los pagos fraccionados
                    _payments.Update(this);
                }
            }
            catch (iQInfoException ex)
            {
                if (ex.Code != iQExceptionCode.WARNING) throw ex;

                //Se ha producido un WARNING que no impide que actualicemos a los hijos

                _operations.Update(this);
                //Para los pagos fraccionados
                _payments.Update(this);
            }

            MarkOld();
        }

        internal void DeleteSelf(IAcreedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            Payment obj = Payment.Get(Oid, true, SessionCode);
            obj.Base.CopyValues(this);

            //Nunca se borran, se anulan
            obj.EEstado = EEstado.Anulado;

            SetRoots(null, null);

            parent.Session().Update(obj.Base.Record);

            AnnulRelateds(obj.GetInfo(false), SessionCode);

            MarkNew();
        }

        internal void Insert(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;
            OidAgente = parent.Oid;

            GetNewCode(ETipoPago, Base.Record.OidAgente);
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
            SetRoots(null, Base.Record);

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            foreach (TransactionPayment tr in Operations)
                tr.OidOperation = parent.Oid;

            _operations.Update(this);            
            _payments.Update(this);

            InsertRelateds();

            MarkOld();
        }

        internal void Update(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;
            OidAgente = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            PaymentRecord record = parent.Session().Get<PaymentRecord>(Oid);

            SetRoots(null, record);

            Payment obj = Payment.Get(Oid, true, SessionCode);

            try
            {
                if (Common.EntityBase.UpdateByYear(obj, this, null))
                {
                    obj.Save();

                    _operations.Update(this);
                    _payments.Update(this);

                    parent.Transaction().Commit();
                    parent.NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    parent.Session().Update(record);

                    _operations.Update(this);
                    _payments.Update(this);
                }
            }
            catch (iQInfoException ex)
            {
                if (ex.Code != iQExceptionCode.WARNING) throw ex;

                //Se ha producido un WARNING que no impide que actualicemos a los hijos

                _operations.Update(this);
                //Para los pagos fraccionados
                _payments.Update(this);
            }

            MarkOld();
        }

        internal void DeleteSelf(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            Payment obj = Payment.Get(Oid, true, SessionCode);
            obj.Base.CopyValues(this);

            //Nunca se borran, se anulan
            obj.EEstado = EEstado.Anulado;

            SetRoots(null, null);

            parent.Session().Update(obj._base.Record);

            AnnulRelateds(obj.GetInfo(false), SessionCode);

            //Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
            //Cash.AnnulItem(this, SessionCode);

            //if (EMedioPago != EMedioPago.Tarjeta)
            //    AnnulRelateds(obj.GetInfo(false), SessionCode);
            //else
            //    BankLine.BankLine.AnnulItemTarjeta(obj, CreditCardInfo.Get(this.OidTarjetaCredito, false), SessionCode);

            MarkNew();
        }

        internal void Insert(Payment parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;
            
            GetNewCode(ETipoPago, parent);
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
            SetRoots(parent, Base.Record);

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);
            _operations.Update(this);
            _payments.Update(this);

            InsertRelateds();

            MarkOld();
        }

        internal void Update(Payment parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);                

            PaymentRecord record = parent.Session().Get<PaymentRecord>(Oid);

            SetRoots(parent, record);

            Payment obj = Payment.Get(Oid, true, SessionCode);

            try
            {
                if (Common.EntityBase.UpdateByYear(obj, this, null))
                {
                    obj.Save();

                    _operations.Update(this);
                    _payments.Update(this);

                    parent.Transaction().Commit();
                    parent.NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    parent.Session().Update(record);

                    _operations.Update(this);
                    _payments.Update(this);
                }
            }
            catch (iQInfoException ex)
            {
                if (ex.Code != iQExceptionCode.WARNING) throw ex;

                //Se ha producido un WARNING que no impide que actualicemos a los hijos

                _operations.Update(this);
                //Para los pagos fraccionados
                _payments.Update(this);
            }

            MarkOld();
        }

        internal void DeleteSelf(Payment parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            Payment obj = Payment.Get(Oid, true, SessionCode);
            obj.Base.CopyValues(this);

            //Nunca se borran, se anulan
            obj.EEstado = EEstado.Anulado;

            SetRoots(parent, null);

            parent.Session().Update(obj.Base.Record);

            AnnulRelateds(obj.GetInfo(false), SessionCode);

            //Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
            //Cash.AnnulItem(this, SessionCode);

            //if (EMedioPago != EMedioPago.Tarjeta)
            //    AnnulRelateds(obj.GetInfo(false), SessionCode);
            //else
            //    BankLine.BankLine.AnnulItemTarjeta(obj, CreditCardInfo.Get(this.OidTarjetaCredito, false), SessionCode);

            MarkNew();
        }

        #endregion

        #region SQL

		internal enum ETipoQuery { GENERAL = 0, AGRUPADO = 1, FACTURAS = 2, NOMINAS = 3, PRESTAMOS = 4, EXTRACTOS = 5, GASTOS = 6 }

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

        public static ProviderBaseInfo.SelectCaller local_caller_BASE = new ProviderBaseInfo.SelectCaller(SELECT_BASE);
        public static ProviderBaseInfo.SelectCaller local_caller_VENCIMIENTO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_VENCIMIENTO);
        public static ProviderBaseInfo.SelectCaller local_caller_VENCIMIENTO_SIN_APUNTE = new ProviderBaseInfo.SelectCaller(SELECT_BASE_VENCIMIENTO_SIN_APUNTE);
        public static ProviderBaseInfo.SelectCaller local_caller_VENCIMIENTO_PRESTAMO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_VENCIMIENTO_PRESTAMO);
        public static ProviderBaseInfo.SelectCaller local_caller_VENCIMIENTO_TARJETA = new ProviderBaseInfo.SelectCaller(SELECT_BASE_VENCIMIENTO_TARJETA);
        public static ProviderBaseInfo.SelectCaller local_caller_VENCIMIENTO_TARJETA_BY_PAGO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_VENCIMIENTO_TARJETA_BY_PAGO);
        public static ProviderBaseInfo.SelectCaller local_caller_CREDIT_CARD_STATEMENT = new ProviderBaseInfo.SelectCaller(SELECT_BASE_CREDIT_CARD_STATEMENTS);

        public new static string SELECT(long oid) { return SELECT(oid, ETipoPago.Todos, ETipoAcreedor.Todos); }
        public static string SELECT(long oid, ETipoPago tipo, ETipoAcreedor providerType) { return SELECT(oid, tipo, providerType, true); }
        internal static string SELECT(long oid, ETipoPago paymentType, ETipoAcreedor providerType, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                PaymentType = paymentType,
                TipoAcreedor = new ETipoAcreedor[1] { providerType },
                Payment = Payment.New(ETipoPago.Todos).GetInfo(false)
            };
            conditions.Payment.Oid = oid;

            query = SELECT(conditions, lockTable);

            return query;
        }

        internal static string FIELDS(ETipoQuery tipo = ETipoQuery.GENERAL, QueryConditions conditions = null)
        {
			string select = @"
			SELECT " + (long)tipo + @" AS ""TIPO_QUERY""";

			string query = 
            select + @"
				,P.*
				,CASE WHEN P.""MEDIO_PAGO"" IN (" + (long)EMedioPago.ComercioExterior + ", " + (long)EMedioPago.Tarjeta + ", " + (long)EMedioPago.Pagare + @") 
					THEN P.""FECHA"" 
					ELSE P.""VENCIMIENTO"" END AS ""FECHA_ORDENACION""
				,COALESCE(US.""NAME"", '') AS ""USUARIO""
				,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""
				,COALESCE(CB.""ENTIDAD"", '') AS ""ENTIDAD""
				,COALESCE(TC.""NOMBRE"", '') AS ""TARJETA_CREDITO""
				,COALESCE(LC.""CODIGO"", '') AS ""ID_LINEA_CAJA""
				,COALESCE(MV.""CODIGO"", '') AS ""ID_MOVIMIENTO_BANCO""
				,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOVIMIENTO_CONTABLE""";

			switch (tipo)
			{
				case ETipoQuery.FACTURAS:

					if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos)
						query += @"
							,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
							,0 AS ""TOTAL_PAGADO""
							,0 AS ""TOTAL_ASIGNADO""
							,COALESCE(A.""NOMBRE"", '') AS ""AGENTE""
							,COALESCE(A.""CODIGO"",'') AS ""COD_AGENTE""";
					else
						query += @"
							,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
							,0 AS ""TOTAL_PAGADO""
							,0 AS ""TOTAL_ASIGNADO""
							,'' AS ""AGENTE""
							,'' AS ""COD_AGENTE""";
					break;

				case ETipoQuery.GASTOS:

					query += @" 
						,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
						,0 AS ""TOTAL_PAGADO""
						,0 AS ""TOTAL_ASIGNADO""
						,'GASTO' AS ""AGENTE""
						,'' AS ""COD_AGENTE""";
					break;

				case ETipoQuery.NOMINAS:

						query += @"
							,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
							,0 AS ""TOTAL_PAGADO""
							,0 AS ""TOTAL_ASIGNADO""
							,COALESCE(A.""NOMBRE"", '') || ' ' || COALESCE(A.""APELLIDOS"", '') AS ""AGENTE""
							,COALESCE(A.""CODIGO"",'') AS ""COD_AGENTE""";
					break;

				case ETipoQuery.PRESTAMOS:

					query += @" 
							,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
							,0 AS ""TOTAL_PAGADO""
							,0 AS ""TOTAL_ASIGNADO""
							,LBK.""ENTIDAD"" AS ""AGENTE""
							,'' AS ""COD_AGENTE""";
					break;

				case ETipoQuery.EXTRACTOS:

                    if (conditions.PaymentType == ETipoPago.ExtractoTarjeta)
                    {
                        if (conditions.Payment != null && conditions.Payment.ETipoPago == ETipoPago.ExtractoTarjeta)
                            query += @"
                                ,(P.""IMPORTE"" + P.""GASTOS_BANCARIOS"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""";
                        else
                            query += @"
                                ,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""";

                        query += @"
                            ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_PAGADO""
                            ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_ASIGNADO""
                            ,COALESCE(TC.""NOMBRE"", '') AS ""AGENTE""
                            ,'' AS ""COD_AGENTE""";
                    }
                    else
                    {
                        query += @"
                            ,(P.""IMPORTE"" + P.""GASTOS_BANCARIOS"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
                            ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_PAGADO""
                            ,COALESCE(""ASIGNADO_PAGO"", 0) AS ""TOTAL_ASIGNADO""
                            ,COALESCE(A.""NOMBRE"", '') AS ""AGENTE""
                            ,COALESCE(A.""CODIGO"",'') AS ""COD_AGENTE""";
                    }


//                    if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos)
//                    {
//                        if (conditions.Pago != null && conditions.Pago.ETipoPago == ETipoPago.ExtractoTarjeta)
//                            query += @"
//                                ,(P.""IMPORTE"" + P.""GASTOS_BANCARIOS"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
//                                ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_PAGADO""
//                                ,COALESCE(""ASIGNADO_PAGO"", 0) AS ""TOTAL_ASIGNADO""
//                                ,COALESCE(A.""NOMBRE"", '') AS ""AGENTE""
//                                ,COALESCE(A.""CODIGO"",'') AS ""COD_AGENTE""";
//                        else
//                            query += @"
//                                ,(P.""IMPORTE"" + P.""GASTOS_BANCARIOS"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
//                                ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_PAGADO""
//                                ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_ASIGNADO""
//                                ,COALESCE(A.""NOMBRE"", '') AS ""AGENTE""
//                                ,COALESCE(A.""CODIGO"",'') AS ""COD_AGENTE""";
//                    }
//                    else
//                    {
//                        if (conditions.Pago != null && conditions.Pago.ETipoPago == ETipoPago.ExtractoTarjeta)
//                            query += @"
//                                ,(P.""IMPORTE"" + P.""GASTOS_BANCARIOS"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""";
//                        else
//                            query += @"
//                                ,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""";

//                        query += @"
//                            ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_PAGADO""
//                            ,COALESCE(""TOTAL_ASIGNADO"", 0) AS ""TOTAL_ASIGNADO""
//                            ,COALESCE(TC.""NOMBRE"", '') AS ""AGENTE""
//                            ,'' AS ""COD_AGENTE""";
//                    }

					break;

				case ETipoQuery.AGRUPADO:
					query = 
                    select + @"
                        ,DATE_TRUNC('" + conditions.Step.ToString() + @"', P.""FECHA"") AS ""STEP""
						,SUM(F.""TOTAL"") AS ""TOTAL""";
					break;

                default:
                    query += @" 
						,(P.""IMPORTE"" - COALESCE(""TOTAL_ASIGNADO"", 0)) AS ""PENDIENTE_ASIGNAR""
						,0 AS ""TOTAL_PAGADO""
						,0 AS ""TOTAL_ASIGNADO""
						,'' AS ""AGENTE""
						,'' AS ""COD_AGENTE""";
					break;
			}

            return query;
        }
        
        internal static string JOIN_BASE() { return JOIN_BASE(new QueryConditions()); }
		internal static string JOIN_BASE(QueryConditions conditions)
		{
			string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
			string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string cc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.CashLineRecord));
            string bl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));
			string rl = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string line_type = "(" + (long)EBankLineType.PagoFactura +
                                "," + (long)EBankLineType.PagoGasto +
                                "," + (long)EBankLineType.PagoNomina +
                                "," + (long)EBankLineType.ExtractoTarjeta + ")";

			string query = @"
			FROM " + pa + @" AS P
			LEFT JOIN " + us + @" AS US ON US.""OID"" = P.""OID_USUARIO""
			LEFT JOIN " + bk + @" AS CB ON P.""OID_CUENTA_BANCARIA"" = CB.""OID""
			LEFT JOIN " + cc + @" AS TC ON P.""OID_TARJETA_CREDITO"" = TC.""OID""
			LEFT JOIN " + cl + @" AS LC ON P.""OID"" = LC.""OID_PAGO"" AND LC.""ESTADO"" != " + (long)EEstado.Anulado + @"
			LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO""
							,MIN(MV.""OID"") AS ""OID""
							,MV.""OID_OPERACION"", MV.""TIPO_OPERACION""
						FROM " + bl + @" AS MV
						WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"")
				AS MV ON P.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" IN " + line_type + @"
			LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                                ,""OID_ENTIDAD""
                                ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
						FROM " + rl + @" AS LR
						WHERE LR.""TIPO_ENTIDAD"" = " + (long)moleQule.Common.Structs.ETipoEntidad.Pago + @"
							AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
						GROUP BY ""OID_ENTIDAD"")
				AS LR ON P.""OID"" = LR.""OID_ENTIDAD""
			LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

			return query + " " + conditions.ExtraJoin;
		}

        internal static string JOIN_AGENT(QueryConditions conditions)
        {
            string query = string.Empty;

            switch (conditions.PaymentType)
            {
                case ETipoPago.Prestamo:

                    string lo = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.LoanRecord));
                    string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

                    query = @"
			        INNER JOIN " + lo + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                    INNER JOIN " + bk + @" AS LBK ON LBK.""OID"" = A.""OID_CUENTA""";

                    break;

                case ETipoPago.Nomina:

                    if (conditions.Acreedor != null)
                    {
                        query = @"
						LEFT JOIN " + ProviderBaseInfo.TABLE(conditions.TipoAcreedor[0]) + @" AS A ON CASE WHEN PF.""OID_EMPLEADO"" != 0 THEN 
                                                                                                            PF.""OID_EMPLEADO"" 
                                                                                                        ELSE 
                                                                                                            P.""OID_AGENTE"" END = A.""OID"" 
                                                                                                    AND A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];
                    }
                    else
                    {
                        query = @"
						LEFT JOIN " + ProviderBaseInfo.TABLE(conditions.TipoAcreedor[0]) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                                                                                                    AND A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];
                    }

                    break;

                default:

                    switch (conditions.TipoAcreedor[0])
                    {
                        case ETipoAcreedor.Empleado:

                            query = @"
						    INNER JOIN " + ProviderBaseInfo.TABLE(conditions.TipoAcreedor[0]) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                                                                                                        AND  A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];

                            break;

                        case ETipoAcreedor.Todos: break;

                        default:

                            query = @"
					        INNER JOIN " + ProviderBaseInfo.TABLE(conditions.TipoAcreedor[0]) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                                                                                                        AND  A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];

                            break;
                    }

                    break;
            }

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
			if (conditions == null) return string.Empty;

            string query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "P", ForeignFields());

            query += @" 
				AND (P.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += @"
				AND (P.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')";

            query += Common.EntityBase.NO_NULL_RECORDS_CONDITION("P");
            query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "P");

            if (conditions.Payment != null && conditions.Payment.Oid > 0 
                && conditions.Payment.ETipoPago != ETipoPago.FraccionadoTarjeta
                && conditions.Payment.ETipoPago != ETipoPago.ExtractoTarjeta) 
				query += @"
					AND P.""OID"" = " + conditions.Payment.Oid;

            if (conditions.OidList != null)
                query += @"
					AND P.""OID"" IN " + Common.EntityBase.GET_IN_STRING(conditions.OidList);

            if ((conditions.Acreedor != null) && (conditions.Acreedor.OidAcreedor != 0))
            {
                query += @"
					AND P.""OID_AGENTE"" = " + conditions.Acreedor.OidAcreedor + @"
                    AND P.""TIPO_AGENTE"" = " + conditions.Acreedor.TipoAcreedor;
            }

            if (conditions.TipoAcreedor != null && conditions.TipoAcreedor[0] != ETipoAcreedor.Todos) 
				query += @"
					AND P.""TIPO_AGENTE"" = " + (long)conditions.TipoAcreedor[0];

            if (conditions.MedioPago != EMedioPago.Todos)
            {
                switch (conditions.MedioPago)
                {
                    case EMedioPago.NoEfectivo:
                        query += @"
							AND P.""MEDIO_PAGO"" != " + (long)EMedioPago.Efectivo;
                        break;

                    case EMedioPago.NoTarjeta:
                        query += @"
							AND P.""MEDIO_PAGO"" != " + (long)EMedioPago.Tarjeta;
                        break;

                    default:
                        query += @"
							AND P.""MEDIO_PAGO"" = " + (long)conditions.MedioPago;
                        break;
                }
            }

            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "P", "MEDIO_PAGO");

            if (conditions.PaymentType != ETipoPago.Todos) 
				query += @"
					AND P.""TIPO"" = " + (long)conditions.PaymentType;

            if (conditions.TarjetaCredito != null)
                query += @"
					AND P.""OID_TARJETA_CREDITO"" = " + conditions.TarjetaCredito.Oid;

            if (conditions.CuentaBancaria != null) 
				query += @"
					AND P.""OID_CUENTA_BANCARIA"" = " + conditions.CuentaBancaria.Oid;

            if (conditions.Prestamo != null) 
				query += @"
					AND P.""OID_AGENTE"" = " + conditions.Prestamo.Oid;

            /*if (AppContext.User.IsPartner)
            {
                query += Common.EntityBase.GET_IN_LIST_CONDITION(AppContext.Principal.Partners, "A");
                query += " AND A.\"TIPO\" = " + (long)ETipoAcreedor.Partner;
            }*/

            return query + " " + conditions.ExtraWhere;
        }

        internal static string ORDER(QueryConditions conditions)
        {
            string query = string.Empty;

            if (conditions.OrderFields != null)
            {
                string subquery_order = string.Empty;

                foreach (string field in conditions.OrderFields)
                {
                    try
                    {
                        subquery_order += "\"" + field + "\"";

                        if (conditions.Order == ListSortDirection.Descending)
                            subquery_order += " DESC";

                        subquery_order += ",";
                    }
                    catch { }
                }

                if (subquery_order != string.Empty)
                    query += @"
							ORDER BY " + subquery_order.Substring(0, subquery_order.Length - 1);         
			}
            else
			{							
				if (conditions.Orders == null)
				{
					conditions.Orders = new OrderList();
					conditions.Orders.Add(FilterMng.BuildOrderItem("Vencimiento", ListSortDirection.Descending, typeof(Payment)));
					conditions.Orders.Add(FilterMng.BuildOrderItem("Codigo", ListSortDirection.Descending, typeof(Payment)));
				}

				query += ORDER(conditions.Orders, string.Empty, ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

            return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string query = string.Empty;

            switch (conditions.PaymentType)
            {
                case ETipoPago.Nomina:
                    {
                        query =
                        SELECT_BASE_NOMINAS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);
                    } break;

                case ETipoPago.ExtractoTarjeta:
                    {
                        query =
                        SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);
                    } break;

                case ETipoPago.FraccionadoTarjeta:
                    {
                        query =
                        SELECT_BASE_EXTRACTOS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);
                    } break;

                case ETipoPago.Prestamo:
                    {
                        query =
                        SELECT_BASE_PRESTAMOS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);
                    } break;

                default:
                    {
                        query =
                        SELECT_BASE_FACTURAS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);
                    } break;
            }

            switch (conditions.PaymentType)
            {
                case ETipoPago.Nomina:
                case ETipoPago.ExtractoTarjeta:
                case ETipoPago.FraccionadoTarjeta:
                case ETipoPago.Prestamo:
                    {
                        query += @"
                            AND P.""TIPO"" IN (" + (long)conditions.PaymentType + ")";
                    } break;

                default:
                    {
                        query += @"
                            AND P.""TIPO"" IN (" + (long)ETipoPago.Factura + ")";
                    } break;
            }

            return query;
        }

        internal static string SELECT_BASE_CREDIT_CARD_STATEMENTS(QueryConditions conditions)
        {
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            
            conditions.ExtraWhere = @"
                AND P.""ESTADO"" != " + (long)EEstado.Anulado + @"                
                AND (P.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"')";

            if (conditions.CreditCardStatement != null)
                conditions.ExtraWhere += @"
                AND P.""OID_LINK"" = " + conditions.CreditCardStatement.Oid;

            string query = string.Empty;

            if (conditions.Step != EStepGraph.None)
            {
                query =
                FIELDS(ETipoQuery.AGRUPADO, conditions) +
                JOIN_BASE(conditions) +
                WHERE(conditions);
            }
            else
            {
                conditions.ExtraJoin = @"
                LEFT JOIN (SELECT ""OID_PAGO""
                                    ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                            FROM " + tp + @" AS PF1
                            INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF1.""OID_PAGO"" AND PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.ExtractoTarjeta + @"
                            GROUP BY ""OID_PAGO"")
                    AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + @")
                LEFT JOIN (SELECT ""OID_PAGO""
                                    ,SUM(""CANTIDAD"") AS ""ASIGNADO_PAGO""
                            FROM " + tp + @" AS PF1
                            INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF1.""OID_PAGO"" AND PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.ExtractoTarjeta;

                if (conditions.Payment != null && conditions.Payment.Oid > 0)
                    conditions.ExtraJoin += @"
                                AND PA.""OID"" = " + conditions.Payment.Oid;

                conditions.ExtraJoin += @"
                            GROUP BY ""OID_PAGO"")
                    AS PF2 ON PF2.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + ")";

                switch (conditions.PaymentType)
                {
                    case ETipoPago.ExtractoTarjeta:

                        query =
                        FIELDS(ETipoQuery.EXTRACTOS, conditions) +
                        JOIN_BASE(conditions) +
                        WHERE(conditions);

                        break;

                    case ETipoPago.Factura:
                        query +=
                        SELECT_BASE_FACTURAS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);

                        break;

                    case ETipoPago.Gasto:
                        query +=
                        SELECT_BASE_GASTOS(ETipoPago.Gasto, conditions) +
                        WHERE(conditions);

                        break;

                    case ETipoPago.Nomina:

                        ETipoAcreedor acreedor = conditions.TipoAcreedor[0];
                        conditions.TipoAcreedor[0] = ETipoAcreedor.Empleado;

                        query += 
                        SELECT_BASE_NOMINAS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);

                        conditions.TipoAcreedor[0] = acreedor;

                        break;

                    case ETipoPago.Prestamo:

                        query +=
                        SELECT_BASE_PRESTAMOS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);

                        break;
                }
            }

            conditions.ExtraWhere = string.Empty;
            conditions.ExtraJoin = string.Empty;

            return query;
        }

        internal static string SELECT_BASE_EXTRACTOS(QueryConditions conditions)
        {
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query = string.Empty;

			if (conditions.Step != EStepGraph.None)
			{
				query =
				FIELDS(ETipoQuery.AGRUPADO, conditions) +
				JOIN_BASE(conditions) +
				WHERE(conditions);
			}
			else
			{
				query = 
                FIELDS(ETipoQuery.EXTRACTOS, conditions) +
				JOIN_BASE(conditions);

				switch (conditions.PaymentType)
				{
					case ETipoPago.FraccionadoTarjeta:
						
                        query += @"
                        LEFT JOIN (SELECT PG.""OID""
                                            ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                    FROM " + tp + @" AS PF1
                                    INNER JOIN " + pa + @" AS PG1 ON PG1.""OID"" = PF1.""OID_PAGO""
                                    INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PG1.""OID_ROOT""
                                    WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.ExtractoTarjeta + @"
                                        AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        AND PG1.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                    GROUP BY PG.""OID"")
                            AS PF ON PF.""OID"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + ")";
						break;

					default:
						
                        query += @"
                        LEFT JOIN (SELECT ""OID_OPERACION""
                                            ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                    FROM " + tp + @" AS PF1
                                    INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF1.""OID_PAGO"" AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                    WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.ExtractoTarjeta + @"
                                    GROUP BY ""OID_OPERACION"")
                            AS PF ON PF.""OID_OPERACION"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + @")
                        LEFT JOIN (SELECT ""OID_OPERACION"" AS ""OID""
                                            ,SUM(""CANTIDAD"") AS ""ASIGNADO_PAGO""
                                    FROM " + tp + @" AS PF1
                                    INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF1.""OID_PAGO"" AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                    WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.ExtractoTarjeta;

                        if (conditions.Payment != null && conditions.Payment.Oid > 0)
	                        query += @"
                                        AND PG.""OID"" = " + conditions.Payment.Oid;

                        query += @"
                                    GROUP BY ""OID_OPERACION"")
                            AS PF2 ON PF2.""OID"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + ")";

						break;
				}
			}

            return query;
        }

        internal static string SELECT_BASE_FACTURAS(QueryConditions conditions)
        {
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            if (conditions.ExtraJoin == string.Empty)
            {
                conditions.ExtraJoin = @"
			    LEFT JOIN (SELECT ""OID_PAGO"", SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
						FROM " + pf + @" AS PF1
						GROUP BY ""OID_PAGO"")
				    AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)ETipoPago.Factura + ")";
            }
			
            string query =
            FIELDS(ETipoQuery.FACTURAS, conditions) +
			JOIN_BASE(conditions);

            conditions.ExtraJoin = string.Empty;

            return query;
        }

        internal static string SELECT_BASE_PRESTAMOS(QueryConditions conditions)
        {
            ETipoPago paymentType = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Prestamo;

            if (conditions.ExtraJoin == string.Empty)
            {
                string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

                conditions.ExtraJoin = @"
                LEFT JOIN (SELECT ""OID_PAGO""
                                ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
						    FROM " + tp + @" AS TP
						    GROUP BY ""OID_PAGO"")
				    AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)conditions.PaymentType + ")";
            }

            string query =
            FIELDS(ETipoQuery.PRESTAMOS, conditions) +
			JOIN_BASE(conditions);

            conditions.PaymentType = paymentType;
            conditions.ExtraJoin = string.Empty;

            return query;
        }

		internal static string SELECT_BASE_PRESTAMOS()
		{
			string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            QueryConditions conditions = new QueryConditions()
            {
                PaymentType = ETipoPago.Prestamo
            };

            conditions.ExtraJoin = @"
			LEFT JOIN (SELECT ""OID_PAGO""
                            ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
						FROM " + pf + @" AS PF1
						GROUP BY ""OID_PAGO"")
				AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)ETipoPago.Prestamo + ")";

            string query =
            FIELDS(ETipoQuery.PRESTAMOS, conditions) +
            JOIN_BASE(conditions) +
            JOIN_AGENT(conditions);

			return query;
		}

        internal static string SELECT_BASE_GASTOS(ETipoPago tipo, QueryConditions conditions)
        {
            if (conditions.ExtraJoin == string.Empty)
            {
                string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
                string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

                switch (tipo)
                {
                    case ETipoPago.Fraccionado:

                        conditions.ExtraJoin = @"
                        LEFT JOIN (SELECT PG.""OID"" AS ""OID_PAGO""
                                        ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                    FROM " + pf + @" AS PF1
                                    INNER JOIN " + pg + @" AS PG1 ON PG1.""OID"" = PF1.""OID_PAGO""
                                    INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PG1.""OID_ROOT""
                                    GROUP BY PG.""OID"")
                            AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)tipo + ")";

                        break;

                    default:

                        conditions.ExtraJoin = @"
                        LEFT JOIN (SELECT ""OID_PAGO""
                                        ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                    FROM " + pf + @" AS PF1
                                    GROUP BY ""OID_PAGO"")
                            AS PF ON PF.""OID_PAGO"" = P.""OID""";

                        if (tipo != ETipoPago.Todos)
                            conditions.ExtraJoin += @"
                            AND P.""TIPO"" IN (" + (long)tipo + ")";

                        break;
                }
            }            

            string query =  
			FIELDS(ETipoQuery.GASTOS, new QueryConditions()) +
			JOIN_BASE(conditions);

            conditions.ExtraJoin = string.Empty;

            return query;
        }

        internal static string SELECT_BASE_NOMINAS(QueryConditions conditions)
        {
            ETipoPago paymentType = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Nomina;

            if (conditions.ExtraJoin == string.Empty)
            {
                string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
                string py = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));

                if (conditions.Acreedor != null && conditions.Acreedor.OidAcreedor != 0)
                {
                    conditions.ExtraJoin += @"
                    LEFT JOIN (SELECT ""OID_PAGO""
                                    ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                                    ,NM.""OID_EMPLEADO""
				                FROM " + tp + @" AS PF1
				                LEFT JOIN " + py + @" AS NM ON NM.""OID"" = PF1.""OID_OPERACION""
				                WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.Nomina + @" 
                                    AND NM.""OID_EMPLEADO"" = " + conditions.Acreedor.OidAcreedor + @"
				                GROUP BY ""OID_PAGO"", NM.""OID_EMPLEADO"")
				        AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)ETipoPago.Nomina + @")";
                }
                else
                {
                    conditions.ExtraJoin += @"
                    LEFT JOIN (SELECT ""OID_PAGO""
                                    ,SUM(""CANTIDAD"") AS ""TOTAL_ASIGNADO""
				                FROM " + tp + @" AS PF1
				                WHERE PF1.""TIPO_PAGO"" = " + (long)ETipoPago.Nomina + @"
				                GROUP BY ""OID_PAGO"")
				        AS PF ON PF.""OID_PAGO"" = P.""OID"" AND P.""TIPO"" IN (" + (long)ETipoPago.Nomina + ")";
                }
            }

            string query = 
			FIELDS(ETipoQuery.NOMINAS, conditions) +
			JOIN_BASE(conditions);

            conditions.PaymentType = paymentType;
            conditions.ExtraJoin = string.Empty;

            return query;
        }

        internal static string SELECT_BASE_VENCIMIENTO(QueryConditions conditions)
        {
            string where = string.Empty;
            string query = string.Empty;

            where = @"
                AND P.""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @"
                AND (P.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')
                AND P.""ESTADO"" != " + (long)EEstado.Anulado;

            query =
            SELECT_FACTURAS(conditions) +
            where;

            query += @"
            UNION " +
            SELECT_GASTOS(conditions) +
            where;

            /*query += 
            "	UNION " +
                SELECT_NOMINAS(conditions) +
                where;*/

            return query;
        }

        internal static string SELECT_BASE_VENCIMIENTO_TARJETA(QueryConditions conditions)
        {
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));

            string where = string.Empty;
            string query = string.Empty;

            where = " AND (P.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')" +
                     " AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                     " AND COALESCE(MV.\"OID\", 0) = 0" +
                     " AND COALESCE(MV2.\"OID\", 0) = 0" +
                     " AND COALESCE(MV3.\"OID\", 0) = 0";

            query =
                SELECT_FACTURAS(conditions) +
                where;

            query +=
            "	UNION " +
                SELECT_GASTOS(conditions) +
                where;

            query +=
            "	UNION " +
                SELECT_NOMINAS(conditions) +
                where;

            return query;
        }

        internal static string SELECT_BASE_VENCIMIENTO_TARJETA_BY_PAGO(QueryConditions conditions)
        {
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));

            string where = string.Empty;
            string query = string.Empty;

            where = " AND (P.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')" +
                     " AND P.\"ESTADO\" != " + (long)EEstado.Anulado;
            
            query =
                SELECT_FACTURAS_BY_PAGO(conditions) +
                where;

            query +=
            "	UNION " +
                SELECT_GASTOS_BY_PAGO(conditions) +
                where;

            query +=
            "	UNION " +
                SELECT_NOMINAS_BY_PAGO(conditions) +
                where;

            return query;
        }

        internal static string SELECT_BASE_VENCIMIENTO_PRESTAMO(QueryConditions conditions)
        {
            string where = string.Empty;
            string query = string.Empty;

            where =
            "	AND P.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado +
            "	AND (P.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')" +
            "	AND P.\"ESTADO\" != " + (long)EEstado.Anulado;

            query =
                SELECT_FACTURAS(conditions) +
                where;

            query +=
            "	UNION " +
                SELECT_GASTOS(conditions) +
                where;

            return query;
        }

        internal static string SELECT_BASE_VENCIMIENTO_SIN_APUNTE(QueryConditions conditions)
        {
            string mv = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.BankLineRecord));

            string tipos = (long)EBankLineType.PagoFactura +
                            "," + (long)EBankLineType.PagoGasto +
                            "," + (long)EBankLineType.PagoNomina;

            string query = string.Empty;
            string where = string.Empty;

            where = " AND P.\"ESTADO_PAGO\" != " + (long)EEstado.Pagado +
                     " AND (P.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')" +
                     " AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                     " AND P.\"OID\" NOT IN (SELECT \"OID_OPERACION\"" +
                     "						FROM " + mv + " AS MV " +
                     "						WHERE MV.\"ESTADO\" != " + (long)EEstado.Anulado +
                     "						AND \"TIPO_OPERACION\" IN (" + tipos + "))" +
                     " AND P.\"MEDIO_PAGO\" NOT IN " + Common.EnumFunctions.SQL_IN_MEDIO_PAGO_NOT_NEEDS_CUENTA_BANCARIA();

            query = SELECT_FACTURAS(conditions) +
                    where;

            query += " UNION " +
                    SELECT_GASTOS(conditions) +
                    where;

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query = string.Empty;

            if (conditions.Step != EStepGraph.None)
            {
                query =
                    FIELDS(ETipoQuery.AGRUPADO, conditions) +
                    JOIN_BASE(conditions) +
                    WHERE(conditions);
            }
            else
            {
                switch (conditions.PaymentType)
                {
                    case ETipoPago.Todos:

                        query =
                        SELECT_FACTURAS(conditions) + @"
                        UNION " +
                        SELECT_GASTOS(conditions) + @"
                        UNION " +
                        SELECT_NOMINAS(conditions) + @"
                        UNION " +
                        SELECT_PRESTAMOS(conditions) + @"
                        UNION " +
                        SELECT_CREDIT_CARD_STATEMENTS(conditions);

                        ETipoPago paymentType = conditions.PaymentType;
                        conditions.PaymentType = ETipoPago.Fraccionado;

                        query += @"
                        UNION " +
                        SELECT_GASTOS(conditions);

                        conditions.PaymentType = paymentType;

                        /*conditions.TipoPago = ETipoPago.ExtractoTarjeta;
                        query += 
                            SELECT_EXTRACTOS(conditions) +
                            ORDER(conditions);

                        conditions.TipoPago = old;*/

                        break;

                    case ETipoPago.Factura:

                        query =
                        SELECT_FACTURAS(conditions) +
                        ORDER(conditions);

                        if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos)
                            if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;

                    case ETipoPago.Gasto:
                    case ETipoPago.Fraccionado:

                        query =
                        SELECT_GASTOS(conditions) +
                        ORDER(conditions);

                        if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;

                    case ETipoPago.Nomina:

                        query =
                        SELECT_NOMINAS(conditions) +
                        ORDER(conditions);

                        if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;

                    case ETipoPago.Prestamo:

                        query =
                        SELECT_PRESTAMOS(conditions) +
                        ORDER(conditions);

                        if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;

                    case ETipoPago.ExtractoTarjeta:

                        query =
                        SELECT_CREDIT_CARD_STATEMENTS(conditions) +
                        ORDER(conditions);

                        if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;

                    /*case ETipoPago.FraccionadoTarjeta:

                        query =
                        SELECT_EXTRACTOS(conditions) +
                        ORDER(conditions);

                        if (lockTable) query += " FOR UPDATE OF P NOWAIT";

                        break;*/
                }
            }

            return query;
        }

        internal static string SELECT_BY_CREDIT_CARD_STATEMENT(QueryConditions conditions, bool lockTable)
        {
            string query;

            conditions.MedioPago = EMedioPago.Tarjeta;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
            {
                conditions.PaymentType = ETipoPago.Factura;
                query = 
                ProviderBaseInfo.SELECT_BUILDER(local_caller_CREDIT_CARD_STATEMENT, conditions);

                conditions.PaymentType = ETipoPago.Gasto;
                query += @"
                UNION " +
                SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions);

                conditions.PaymentType = ETipoPago.Nomina;
                query += @"
                UNION " +
                SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions);

                conditions.PaymentType = ETipoPago.Prestamo;
                query += @"
                UNION " +
                SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions);
            }
            else
                query = SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions);

            return query;
        }

        internal static string SELECT_CREDIT_CARD_STATEMENTS(QueryConditions conditions)
        {
            ETipoPago paymentType = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.ExtractoTarjeta;

            string query =
            SELECT_BASE_CREDIT_CARD_STATEMENTS(conditions);

            conditions.PaymentType = paymentType;

            return query;
        }

        internal static string SELECT_FACTURAS(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_BASE, conditions);
            else
                query = SELECT_BASE(conditions);

            conditions.PaymentType = tipo;

            return query;
        }

        internal static string SELECT_FACTURAS_BY_PAGO(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_BASE, conditions);
            else
                query = SELECT_BASE_EXTRACTOS(conditions) +
                            JOIN_AGENT(conditions) +
                            WHERE(conditions);

            conditions.PaymentType = tipo;

            return query;
        }

        internal static string SELECT_GASTOS(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            ETipoAcreedor acreedor = conditions.TipoAcreedor[0];

            conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;

            conditions.PaymentType = conditions.PaymentType != ETipoPago.Fraccionado ? ETipoPago.Gasto : conditions.PaymentType;

            query = SELECT_BASE_GASTOS(conditions.PaymentType, conditions);

            query += WHERE(conditions);

            conditions.PaymentType = tipo;
            conditions.TipoAcreedor[0] = acreedor;

            return query;
        }

        internal static string SELECT_GASTOS_BY_PAGO(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            ETipoAcreedor acreedor = conditions.TipoAcreedor[0];

            conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;
            conditions.PaymentType = conditions.PaymentType != ETipoPago.Fraccionado ? ETipoPago.Gasto : conditions.PaymentType;

            query = SELECT_BASE_EXTRACTOS(conditions) +
                        JOIN_AGENT(conditions) +
                        WHERE(conditions);

            conditions.PaymentType = tipo;
            conditions.TipoAcreedor[0] = acreedor;

            return query;
        }

        internal static string SELECT_NOMINAS(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Nomina;
            ETipoAcreedor acreedor = conditions.TipoAcreedor[0];

            conditions.TipoAcreedor[0] = ETipoAcreedor.Empleado;

            query =
            SELECT_BASE_NOMINAS(conditions) + @"
            LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Empleado) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" AND  A.""TIPO"" = " + (long)ETipoAcreedor.Empleado +
            WHERE(conditions);

            conditions.PaymentType = tipo;
            conditions.TipoAcreedor[0] = acreedor;

            return query;
        }

        internal static string SELECT_NOMINAS_BY_PAGO(QueryConditions conditions)
        {
            string query;

            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Nomina;
            ETipoAcreedor acreedor = conditions.TipoAcreedor[0];

            conditions.TipoAcreedor[0] = ETipoAcreedor.Empleado;

            query = 
            SELECT_BASE_EXTRACTOS(conditions) + @"
            LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Empleado) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                                                            AND  A.""TIPO"" = " + (long)ETipoAcreedor.Empleado +
            WHERE(conditions);

            conditions.PaymentType = tipo;
            conditions.TipoAcreedor[0] = acreedor;

            return query;
        }

        internal static string SELECT_PRESTAMOS(QueryConditions conditions)
        {
            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Prestamo;

            //conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;

            string query = 
            SELECT_BASE_PRESTAMOS() +
            WHERE(conditions);

            conditions.PaymentType = tipo;

            return query;
        }

        internal static string SELECT_BY_PRESTAMO(QueryConditions conditions)
        {
            string query;
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            ETipoPago tipo = conditions.PaymentType;
            conditions.PaymentType = ETipoPago.Prestamo;

            conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;

            query = 
            SELECT_BASE_PRESTAMOS() + @"
            LEFT JOIN " + pf + @" AS PR ON PR.""OID_PAGO"" = P.""OID""";

            query += WHERE(conditions);

            conditions.PaymentType = tipo;

            query += @"
            ORDER BY ""VENCIMIENTO""";

            return query;
        }

        internal static string SELECT_VENCIMIENTO(QueryConditions conditions)
        {
            string query;

            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_VENCIMIENTO, conditions);
            else
                query = SELECT_BASE_VENCIMIENTO(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }

        internal static string SELECT_VENCIMIENTO_TARJETA(QueryConditions conditions)
        {
            string query;

            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_VENCIMIENTO_TARJETA, conditions);
            else
                query = SELECT_BASE_VENCIMIENTO_TARJETA(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }

        internal static string SELECT_VENCIMIENTO_TARJETA_BY_PAGO(QueryConditions conditions)
        {
            string query;

            conditions.PaymentType = ETipoPago.ExtractoTarjeta;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_VENCIMIENTO_TARJETA_BY_PAGO, conditions);
            else
                query = SELECT_BASE_VENCIMIENTO_TARJETA_BY_PAGO(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }

        internal static string SELECT_VENCIMIENTO_PRESTAMO(QueryConditions conditions)
        {
            string query;

            conditions.MedioPago = EMedioPago.ComercioExterior;
            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_VENCIMIENTO_PRESTAMO, conditions);
            else
                query = SELECT_BASE_VENCIMIENTO_PRESTAMO(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }

        internal static string SELECT_VENCIMIENTO_SIN_APUNTE(QueryConditions conditions)
        {
            string query;

            conditions.PaymentType = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_VENCIMIENTO_SIN_APUNTE, conditions);
            else
                query = SELECT_BASE_VENCIMIENTO_SIN_APUNTE(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }

        /*internal static string SELECT_EXTRACTO(QueryConditions conditions)
        {
            string query;

            conditions.TipoPago = ETipoPago.Factura;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_EXTRACTO, conditions);
            else
                query = SELECT_BASE_EXTRACTO(conditions);

            query += @"
            ORDER BY ""VENCIMIENTO"" DESC";

            return query;
        }*/

        internal static string SELECT_IN_NOMINA(QueryConditions conditions, bool lockTable)
        {
            string query = 
            SELECT_BASE_NOMINAS(conditions) + @"
            LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Empleado) + @" AS A ON P.""OID_AGENTE"" = A.""OID"" 
                                                            AND  A.""TIPO"" = " + (long)ETipoAcreedor.Empleado +
            WHERE(conditions);

			if (conditions.Orders == null)
			{
				conditions.Orders = new OrderList();
				conditions.Orders.Add(FilterMng.BuildOrderItem("Fecha", ListSortDirection.Ascending, typeof(Payment)));
				conditions.Orders.Add(FilterMng.BuildOrderItem("Codigo", ListSortDirection.Ascending, typeof(Payment)));
			}

			query += ORDER(conditions.Orders, string.Empty, ForeignFields());
			query += Common.EntityBase.LOCK("P", lockTable);

            return query;
        }

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query = @"
            SELECT COUNT(*) AS ""TOTAL_ROWS""" +
			JOIN_BASE(conditions) +
			WHERE(conditions);

			return query;
		}

        internal static string SELECT(IAcreedorInfo source, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions
            {
                TipoAcreedor = new ETipoAcreedor[] { source.ETipoAcreedor },
                PaymentType = source.ETipoAcreedor == ETipoAcreedor.Empleado ? ETipoPago.Nomina : ETipoPago.Factura,
                Acreedor = source
            };

            string query = 
            SELECT_BASE(conditions);

            if (conditions.PaymentType == ETipoPago.Nomina)
                query += @"
                ORDER BY ""VENCIMIENTO"" DESC";
            else
                query += @"
                ORDER BY ""ID_PAGO"" DESC";

            //if (lockTable) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }

        internal static string SELECT(LoanInfo source, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions
            {
                Prestamo = source,
                PaymentType = ETipoPago.Prestamo,
            };

            string query = 
            SELECT_BASE(conditions);

            query += @"
            ORDER BY ""ID_PAGO""";

            query += Common.EntityBase.LOCK("P", lockTable);

            return query;
        }

		public static string SELECT_BY_ROOT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions();

            string query = SELECT_BASE_FACTURAS(conditions) +
                            JOIN_AGENT(conditions) +
                            WHERE(conditions);

			query += @"
				AND ""OID_ROOT"" = " + oid;

			query += @"
				ORDER BY ""VENCIMIENTO""";

			//if (lockTable) query += " FOR UPDATE OF P NOWAIT";

			return query;
		}

        public static string UPDATE_TIPO(QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query = @"
            UPDATE " + pg + @" AS P SET ""TIPO_AGENTE"" = " + conditions.Acreedor.TipoAcreedor + 
            WHERE(conditions);

            return query;
        }

        internal static string UPDATE_PAGADO(List<long> oidList, bool pagado)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query = @" 
            UPDATE " + pg + @" SET ""ESTADO_PAGO"" = " + (pagado ? (long)EEstado.Pagado : (long)EEstado.Pendiente) + @"
            WHERE ""OID"" IN " + Common.EntityBase.GET_IN_STRING(oidList);

            return query;
        }

        #endregion
    }
}