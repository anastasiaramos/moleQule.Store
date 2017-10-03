using System;
using System.Collections;
using System.Collections.Generic;
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
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class TransactionPaymentBase
    {
        #region Attributes

        private TransactionPaymentRecord _record = new TransactionPaymentRecord();

        //Campo no enlazado 
        private string _n_expediente = string.Empty;
        private string _n_serie = string.Empty;
        private DateTime _fecha_factura;
        private string _n_factura = string.Empty;
        private Decimal _importe_factura;
        private Decimal _other_payments;

        #endregion

        #region Properties

        public TransactionPaymentRecord Record { get { return _record; } }

        //Campo no enlazdo
        public virtual ETipoPago ETipoPago { get { return (ETipoPago)_record.TipoPago; } set { _record.TipoPago = (long)value; } }
        public virtual string TipoPagoLabel { get { return moleQule.Store.Structs.EnumText<ETipoPago>.GetLabel(ETipoPago); } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAgente; } }
        public virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
        public virtual string NSerie
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _n_serie;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_n_serie.Equals(value))
                {
                    _n_serie = value;
                }
            }
        }
        public virtual string NExpediente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _n_expediente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_n_expediente.Equals(value))
                {
                    _n_expediente = value;
                }
            }
        }
        public virtual DateTime FechaFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _fecha_factura;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_fecha_factura.Equals(value))
                {
                    _fecha_factura = value;
                }
            }
        }
        public virtual string NFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _n_factura;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_n_factura.Equals(value))
                {
                    _n_factura = value;
                }
            }
        }
        public virtual Decimal ImporteFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _importe_factura;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_importe_factura.Equals(value))
                {
                    _importe_factura = value;
                }
            }
        }
        public decimal OtherPayments { get { return _other_payments; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _n_serie = Format.DataReader.GetString(source, "N_SERIE");
            //_codigo_factura = Format.DataReader.GetString(source, "CODIGO_FACTURA");
            _n_factura = Format.DataReader.GetString(source, "NUMERO_FACTURA");
            _fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
            _importe_factura = Format.DataReader.GetDecimal(source, "IMPORTE_FACTURA");
            _n_expediente = Format.DataReader.GetString(source, "NEXPEDIENTE");
            _other_payments = Format.DataReader.GetDecimal(source, "OTHER_PAYMENTS");
        }
        internal void CopyValues(TransactionPayment source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _n_serie = source.NSerie;
            _fecha_factura = source.FechaFactura;
            _n_factura = source.NFactura;
            _importe_factura = source.ImporteFactura;
            _n_expediente = source.NExpediente;
            _other_payments = source.OtherPayments;
        }
        internal void CopyValues(TransactionPaymentInfo source)
        {
            if (source == null) return;

            _n_serie = source.NSerie;
            _fecha_factura = source.FechaFactura;
            _n_factura = source.NFactura;
            _importe_factura = source.ImporteFactura;
            _n_expediente = source.NExpediente;
            _other_payments = source.OtherPayments;
        }

        #endregion
    }
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class TransactionPayment : BusinessBaseEx<TransactionPayment>
	{	 
		#region Attributes

        public TransactionPaymentBase _base = new TransactionPaymentBase();

        #endregion

        #region Properties

        public TransactionPaymentBase Base { get { return _base; } }

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

        public long OidPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPago;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidPago.Equals(value))
				{
					_base.Record.OidPago = value;
					PropertyHasChanged();
				}
			}
		}
		public long TipoPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoPago.Equals(value))
				{
					_base.Record.TipoPago = value;
					PropertyHasChanged();
				}
			}
		}
        public long OidOperation
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidOperacion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidOperacion.Equals(value))
                {
                    _base.Record.OidOperacion = value;
                    PropertyHasChanged();
                }
            }
        }
        public long OidExpediente
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
        public long TipoAcreedor
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
		public Decimal Cantidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Cantidad;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Cantidad.Equals(value))
				{
					_base.Record.Cantidad = value;
					PropertyHasChanged();
				}
			}
		}

		//Campo no enlazdo
		public ETipoPago ETipoPago { get { return (ETipoPago)_base.Record.TipoPago; } set { _base.Record.TipoPago = (long)value; } }
		public string TipoPagoLabel { get { return moleQule.Store.Structs.EnumText<ETipoPago>.GetLabel(ETipoPago); } }
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { TipoAcreedor = (long)value; } }
		public string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public string NSerie
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.NSerie;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.NSerie.Equals(value))
                {
                    _base.NSerie = value;
                }
            }
        }
        public string NExpediente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.NExpediente;
            }
        }
        public DateTime FechaFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.FechaFactura;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.FechaFactura.Equals(value))
                {
                    _base.FechaFactura = value;
                }
            }
        }
        public string NFactura
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.NFactura;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.NFactura.Equals(value))
                {
                    _base.NFactura = value;
                }
            }
        }
        public Decimal ImporteFactura { get { return _base.ImporteFactura; } set { _base.ImporteFactura = value; } }
        public Decimal OtherPayments { get { return _base.OtherPayments; } } 
			 
		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual TransactionPayment CloneAsNew()
		{
			TransactionPayment clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = TransactionPayment.OpenSession();
			TransactionPayment.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
        
		protected virtual void CopyFrom(Payment parent, TransactionPaymentInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			_base.Record.OidPago = source.OidPago;
            _base.Record.OidOperacion = source.OidOperation;
            _base.Record.OidExpediente = source.OidExpediente;
            _base.Record.TipoAgente = source.TipoAcreedor;
            _base.NExpediente = source.NExpediente;

            _base.NSerie = source.NSerie;
            _base.FechaFactura = source.FechaFactura;
			_base.NFactura = source.NFactura;
			_base.ImporteFactura = source.ImporteFactura;
			_base.Record.Cantidad = source.Cantidad;
		}
		public virtual void CopyFrom(Payment parent, ITransactionPayment iPagoFactura, ETipoPago tipo)
		{
			_base.Record.OidPago = parent.Oid;
			_base.Record.TipoPago = (long)tipo;
			_base.Record.OidOperacion = iPagoFactura.Oid;
			_base.Record.OidExpediente = iPagoFactura.OidExpediente;
			_base.Record.TipoAgente = (long)iPagoFactura.ETipoAcreedor;
			_base.NFactura = iPagoFactura.NFactura;
			_base.Record.Cantidad = iPagoFactura.Asignado;
			_base.ImporteFactura = iPagoFactura.Total;
		}

        public virtual void CopyFrom(TransactionPayment source, int n_pagos)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidPago = source.OidPago;
            _base.Record.OidOperacion = source.OidOperation;
            _base.Record.OidExpediente = source.OidExpediente;
            _base.Record.TipoAgente = source.TipoAcreedor;
            _base.NExpediente = source.NExpediente;

            _base.NSerie = source.NSerie;
            _base.FechaFactura = source.FechaFactura;
            _base.NFactura = source.NFactura;
            _base.ImporteFactura = source.ImporteFactura;
            _base.Record.Cantidad = n_pagos > 1 ? Decimal.Round(source.Cantidad / n_pagos, 2) : source.Cantidad;
        }

        public virtual void CopyFrom(PaymentInfo source, int n_pagos)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidPago = source.Oid;
            _base.Record.OidOperacion = source.Oid;
            _base.Record.OidExpediente = source.OidExpediente;
            _base.Record.TipoAgente = source.TipoAgente;

            _base.NFactura = source.NFactura;
            _base.ImporteFactura = source.Importe;
            _base.Record.Cantidad = n_pagos > 1 ? Decimal.Round(source.Pendiente / n_pagos, 2) : source.Pendiente;
        }

		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			
			//Código para valores requeridos o que haya que validar
			
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

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected TransactionPayment ()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí		
		}		
		private TransactionPayment(TransactionPayment source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private TransactionPayment(int sessionCode, IDataReader source, bool childs)
        {
			sessionCode = SessionCode;
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static TransactionPayment NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<TransactionPayment>(new CriteriaCs(-1));
		}
		
		internal static TransactionPayment GetChild(TransactionPayment source, bool childs = false)
		{
			return new TransactionPayment(source, childs);
		}
        internal static TransactionPayment GetChild(int sessionCode, IDataReader source, bool childs = false)
		{
			return new TransactionPayment(sessionCode, source, childs);
		}

		public virtual TransactionPaymentInfo GetInfo (bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TransactionPaymentInfo(this, childs);
		}
		
		#endregion				
		
		#region Child Factory Methods
		
        /// <summary>
        /// Crea un nuevo objeto hijo
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <returns>Nuevo objeto creado</returns>
        internal static TransactionPayment NewChild(Payment parent, ITransactionPayment iPagoFactura, ETipoPago tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			TransactionPayment obj = new TransactionPayment();
			obj.CopyFrom(parent, iPagoFactura, tipo);
			obj.MarkAsChild();

			return obj;
        }

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
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override TransactionPayment Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
		#endregion
		
		#region Common Data Access
		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{			
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor
		}
		
		private void Fetch(TransactionPayment source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);				
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

			MarkOld();
		}
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);                
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

		internal void Insert(TransactionPayments parent)
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
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}

		internal void Update(TransactionPayments parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				TransactionPaymentRecord obj = Session().Get<TransactionPaymentRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}
		
		internal void DeleteSelf(TransactionPayments parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<TransactionPaymentRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		internal void Insert(Payment parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			_base.Record.OidPago = parent.Oid;
			_base.Record.TipoPago = parent.TipoPago;

			try
			{
				ValidationRules.CheckRules();
				
				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				this.Oid = (long)parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void Update(Payment parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			_base.Record.OidPago = parent.Oid;
			_base.Record.TipoPago = parent.TipoPago;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				TransactionPaymentRecord obj = parent.Session().Get<TransactionPaymentRecord>(Oid);
				obj.CopyValues(this._base.Record);
				parent.Session().Update(obj);				
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void DeleteSelf(Payment parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<TransactionPaymentRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

        internal static string FIELDS(QueryConditions conditions)
        {
            string query;

			query = @"
            SELECT PF.*";

			switch (conditions.PaymentType)
			{
				case ETipoPago.Factura: query += FIELDS_FACTURA(); break;
				case ETipoPago.Gasto: query += FIELDS_GASTOS(); break;
				case ETipoPago.Nomina: query += FIELDS_NOMINAS(); break;
                case ETipoPago.Prestamo: query += FIELDS_PRESTAMOS(); break;
                case ETipoPago.ExtractoTarjeta: query += FIELDS_EXTRACTO(); break;
			}

            return query;
        }

        internal static string FIELDS_EXTRACTO()
        {
            string query;

            query = @"
                ,PG.""CODIGO"" AS ""CODIGO_FACTURA""
                ,PG.""ID_PAGO"" AS ""NUMERO_FACTURA""
                ,PG.""IMPORTE"" AS ""IMPORTE_FACTURA""
                ,PG.""FECHA"" AS ""FECHA_FACTURA""
                ,'' AS ""NEXPEDIENTE""
                ,'' AS ""N_SERIE""
                ,0 AS ""OTHER_PAYMENTS""";

            return query;
        }

		internal static string FIELDS_FACTURA()
		{
			string query = @" 
                ,FP.""CODIGO"" AS ""CODIGO_FACTURA""
                ,FP.""N_FACTURA"" AS ""NUMERO_FACTURA""
                ,FP.""TOTAL"" AS ""IMPORTE_FACTURA""
                ,FP.""FECHA"" AS ""FECHA_FACTURA""
                ,EX.""CODIGO"" AS ""NEXPEDIENTE""
                ,SE.""IDENTIFICADOR"" AS ""N_SERIE""
                ,COALESCE(PF1.""TOTAL_PAGADO"", 0) - COALESCE(PF2.""ASIGNADO_PAGO"", 0) AS ""OTHER_PAYMENTS""";

			return query;
		}

		internal static string FIELDS_GASTOS()
		{
			string query;

			query = @"
                ,GT.""CODIGO"" AS ""CODIGO_FACTURA""
                ,GT.""CODIGO"" AS ""NUMERO_FACTURA""
                ,GT.""TOTAL"" AS ""IMPORTE_FACTURA""
                ,GT.""FECHA"" AS ""FECHA_FACTURA""
                ,'' AS ""NEXPEDIENTE""
                ,'' AS ""N_SERIE""
                ,COALESCE(PF1.""TOTAL_PAGADO"", 0) - COALESCE(PF2.""ASIGNADO_PAGO"", 0) AS ""OTHER_PAYMENTS""";

			return query;
		}

		internal static string FIELDS_NOMINAS()
		{
			string query = @"
                ,NM.""CODIGO"" AS ""CODIGO_FACTURA""
                ,NM.""CODIGO"" AS ""NUMERO_FACTURA""
                ,NM.""NETO"" AS ""IMPORTE_FACTURA""
                ,NM.""FECHA"" AS ""FECHA_FACTURA""
                ,'' AS ""NEXPEDIENTE""
                ,'' AS ""N_SERIE""
                ,COALESCE(PF1.""TOTAL_PAGADO"", 0) - COALESCE(PF2.""ASIGNADO_PAGO"", 0) AS ""OTHER_PAYMENTS""";

			return query;
		}

        internal static string FIELDS_PRESTAMOS()
        {
            string query = @"
                ,LO.""CODIGO"" AS ""CODIGO_FACTURA""
                ,LO.""CODIGO"" AS ""NUMERO_FACTURA""
                ,LO.""IMPORTE"" AS ""IMPORTE_FACTURA""
                ,LO.""FECHA_INGRESO"" AS ""FECHA_FACTURA""
                ,'' AS ""NEXPEDIENTE""
                ,'' AS ""N_SERIE""
                ,COALESCE(PF1.""TOTAL_PAGADO"", 0) - COALESCE(PF2.""ASIGNADO_PAGO"", 0) AS ""OTHER_PAYMENTS""";

            return query;
        }

        public static string JOIN_PAYMENTS(Common.QueryConditions conditions, long[] paymentTypes, string tableAlias)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string tipos = "(" + String.Join(",", paymentTypes) + ")";
            long oid_pago = conditions.OidEntity;

            // IMPORTE DE PAGOS ASOCIADOS
            string query = @"                        
			LEFT JOIN (SELECT PF.""OID_OPERACION""
							,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
							,PF.""TIPO_PAGO""
							,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
						FROM " + tp + @" AS PF
						INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
						WHERE PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF1 ON PF1.""OID_OPERACION"" = " + tableAlias + @".""OID""";

            // IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTE GASTO
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO_PAGO""
                            ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
                        WHERE PA.""ESTADO"" != " + (long)EEstado.Anulado;

            if (oid_pago != 0)
                query += @"
                            AND PA.""OID"" = " + oid_pago;

            query += @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF2 ON PF2.""OID_OPERACION"" = " + tableAlias + @".""OID""";

            // IMPORTE TOTAL ASIGNADO A ESTE GASTO POR TODOS LOS PAGOS
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                            ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
                        WHERE PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND (PA.""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @" OR PA.""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"')
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF3 ON PF3.""OID_OPERACION"" = " + tableAlias + @".""OID""";

            // IMPORTE LIQUIDADO
            query += @"                        
			LEFT JOIN (SELECT PF.""OID_OPERACION""
							,SUM(PF.""CANTIDAD"") AS ""TOTAL_LIQUIDADO""
							,PF.""TIPO_PAGO""
							,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
						FROM " + tp + @" AS PF
						INNER JOIN " + pa + @" AS PA ON PA.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipos + @"
						WHERE PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
							AND PA.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
						GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
				AS PF4 ON PF4.""OID_OPERACION"" = " + tableAlias + @".""OID""";

            // PAGO ASOCIADO (EL ULTIMO) 
            query += @"
            LEFT JOIN " + pa + @" AS PA ON PA.""OID"" = PF1.""OID_PAGO""";

            return query;
        }

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string p = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

			string query = 
            FIELDS(conditions) + @"
			FROM " + pf + @" AS PF
			INNER JOIN " + p + @" AS P ON PF.""OID_PAGO"" = P.""OID""";

			return query;
		}

        internal static string SELECT_BASE_EXTRACTO(QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query = 
            SELECT_BASE(conditions) + @"
            INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_OPERACION"" AND PF.""TIPO_PAGO"" = " + (long)conditions.PaymentType;

            return query;
        }

		internal static string SELECT_BASE_FACTURA(QueryConditions conditions)
		{
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
            string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));

            conditions.OidEntity = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

			string query = 
            SELECT_BASE(conditions) + @"
            INNER JOIN " + fp + @" AS FP ON PF.""OID_OPERACION"" = FP.""OID"" AND PF.""TIPO_PAGO"" = " + (long)conditions.PaymentType + @"
			LEFT JOIN " + s + @" AS SE ON SE.""OID"" = FP.""OID_SERIE""
			LEFT JOIN " + gt + @" AS GT ON GT.""OID_FACTURA"" = FP.""OID""
			LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""" +
            JOIN_PAYMENTS(QueryConditions.ConvertToCommonQuery(conditions), new long[] { (long)ETipoPago.Factura }, "FP");            

			return query;
		}
		
		internal static string SELECT_BASE_GASTO(QueryConditions conditions)
		{
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));

            conditions.OidEntity = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

			string query =  
			SELECT_BASE(conditions) + @"
			INNER JOIN " + ex + @" AS GT ON GT.""OID"" = PF.""OID_OPERACION"" AND PF.""TIPO_PAGO"" = " + (long)conditions.PaymentType +
            JOIN_PAYMENTS(QueryConditions.ConvertToCommonQuery(conditions), new long[] { (long)ETipoPago.Gasto }, "GT");            

			return query;
		}

        internal static string SELECT_BASE_FRACCIONADO(QueryConditions conditions)
        {
            string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string query = string.Empty;

            query = "SELECT MAX(PF.\"OID\") AS \"OID\"" +
                    "   ,P.\"OID\" AS \"OID_PAGO\"" +
                    "   ,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
                    "   ,PF.\"OID_EXPEDIENTE\"" +
                    "   ,PF.\"TIPO_AGENTE\"" +
                    "   ,PF.\"OID_OPERACION\"" +
                    "   ," + (long)ETipoPago.Fraccionado + " AS \"TIPO_PAGO\"" +
                    " FROM " + pf + " AS PF" +
                    " INNER JOIN " + pg + " AS PG ON PF.\"OID_PAGO\" = PG.\"OID\"" +
                    " INNER JOIN " + pg + " AS P ON P.\"OID\" = PG.\"OID_ROOT\"" +
                    " INNER JOIN " + gt + " AS GT ON GT.\"OID\" = PF.\"OID_OPERACION\" AND P.\"TIPO\" = " + (long)ETipoPago.Fraccionado;
            return query;
        }

		internal static string SELECT_BASE_NOMINA(QueryConditions conditions)
		{
            string py = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));

            conditions.OidEntity = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

			string query = 
            SELECT_BASE(conditions) + @"
			INNER JOIN " + py + @" AS NM ON NM.""OID"" = PF.""OID_OPERACION"" AND PF.""TIPO_PAGO"" = " + (long)ETipoPago.Nomina +
            JOIN_PAYMENTS(QueryConditions.ConvertToCommonQuery(conditions), new long[] { (long)ETipoPago.Nomina }, "NM");            

			return query;
		}

        internal static string SELECT_BASE_PRESTAMO(QueryConditions conditions)
        {
            string lo = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.LoanRecord));

            conditions.OidEntity = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

            string query = 
			SELECT_BASE(conditions) + @"
            INNER JOIN " + lo + @" AS LO ON LO.""OID"" = PF.""OID_OPERACION"" AND PF.""TIPO_PAGO"" = " + (long)ETipoPago.Prestamo +
            JOIN_PAYMENTS(QueryConditions.ConvertToCommonQuery(conditions), new long[] { (long)ETipoPago.Prestamo }, "LO");            

            return query;
        }

		internal static string SELECT_BASE(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			switch (conditions.PaymentType)
			{
				case ETipoPago.Factura:
					{
						query = SELECT_BASE_FACTURA(conditions);
					}
					break;

				case ETipoPago.Nomina:
					{
						query = SELECT_BASE_NOMINA(conditions);
					}
                    break;

				case ETipoPago.Gasto:
					{
						query = SELECT_BASE_GASTO(conditions);
					}
                    break;
                case ETipoPago.Prestamo:
                    {
                        query = SELECT_BASE_PRESTAMO(conditions);
                    }
                    break;
                case ETipoPago.Fraccionado:
                    {
                        query = SELECT_BASE_FRACCIONADO(conditions);
                    }
                    break;
                case ETipoPago.ExtractoTarjeta:
                    {
                        query = SELECT_BASE_EXTRACTO(conditions);
                    }
                    break;
			}

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = " WHERE (P.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			switch (conditions.Estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += " AND P.\"ESTADO\" != " + ((long)EEstado.Anulado).ToString();
					break;

				default:
					query += " AND P.\"ESTADO\" = " + ((long)conditions.Estado).ToString();
					break;
			}

			if (conditions.PaymentType != ETipoPago.Todos) query += " AND P.\"TIPO\" = " + ((long)conditions.PaymentType).ToString();
			if (conditions.Payment != null) query += " AND P.\"OID\" = " + conditions.Payment.Oid.ToString();
			if (conditions.FacturaRecibida != null) query += " AND PF.\"OID_OPERACION\" = " + conditions.FacturaRecibida.Oid.ToString();
			if (conditions.Gasto != null) query += " AND PF.\"OID_OPERACION\" = " + conditions.Gasto.Oid.ToString();
			if (conditions.RemesaNomina != null) query += " AND RM.\"OID\" = " + conditions.RemesaNomina.Oid.ToString();

			if (conditions.Serie != null) query += " AND F.\"OID_SERIE\" = " + conditions.Serie.Oid.ToString();

			switch (conditions.MedioPago)
			{
				case EMedioPago.NoEfectivo:
					query += " AND P.\"MEDIO_PAGO\" != " + ((long)EMedioPago.Efectivo);
					break;

				default:
					if (conditions.MedioPago != EMedioPago.Todos)
						query += " AND P.\"MEDIO_PAGO\" = " + ((long)conditions.MedioPago).ToString();
					break;
            } 
            
            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "P", "MEDIO_PAGO");

			return query;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            if (conditions.RemesaNomina == null)
            {
                query = SELECT_BASE(conditions, lockTable);

                query += WHERE(conditions);

                if (conditions.PaymentType == ETipoPago.Fraccionado)
                    query += " GROUP BY P.\"OID\", PF.\"OID_EXPEDIENTE\", PF.\"TIPO_AGENTE\", PF.\"OID_OPERACION\", PF.\"TIPO_PAGO\"";

                //if (lockTable) query += " FOR UPDATE OF PF NOWAIT"; 
            }
            else
            {
                string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
                string rm = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollBatchRecord));

                string tipos = "(" + (long)ECategoriaGasto.Nomina +
                                "," + (long)ECategoriaGasto.SeguroSocial +
                                "," + (long)ECategoriaGasto.Impuesto + ")";

                query = SELECT_BASE(conditions, lockTable);

                query += @" 
                INNER JOIN " + gt + @" AS GT2 ON GT2.""OID"" = PF.""OID_OPERACION"" AND GT2.""TIPO"" IN " + tipos + @"
                INNER JOIN " + rm + @" AS RM ON RM.""OID"" = GT2.""OID_REMESA_NOMINA""";

                query += WHERE(conditions);

                query += " UNION ";

                query += SELECT_BASE(conditions, lockTable);

                query += " INNER JOIN " + rm + " AS RM ON RM.\"OID\" = NM.\"OID_REMESA\"";

                query += WHERE(conditions);

                if (conditions.PaymentType == ETipoPago.Fraccionado)
                    query += " GROUP BY P.\"OID\", PF.\"OID_EXPEDIENTE\", PF.\"TIPO_AGENTE\", PF.\"OID_OPERACION\", PF.\"TIPO_PAGO\"";

                //if (lockTable) query += " FOR UPDATE OF PF NOWAIT";
            }

            return query;
        }

		public static string UPDATE_TIPO(QueryConditions conditions)
		{
			string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
			
			string query = string.Empty;
			long tipo = (long)conditions.TipoAcreedor[0];
			long tipo_pago = (long)conditions.PaymentType;

			query = "UPDATE " + pf + " AS PF SET \"TIPO_AGENTE\" = " + conditions.Acreedor.TipoAcreedor +
					" FROM " + pg + " AS P " +
					" INNER JOIN " + pr + " AS PR ON (PR.\"OID\" = P.\"OID_AGENTE\" AND PR.\"OID\" = " + conditions.Acreedor.Oid + ")";

			query += WHERE(conditions);

			query += " AND P.\"OID\" = PF.\"OID_PAGO\" AND P.\"TIPO_AGENTE\" = " + tipo + " AND PF.\"TIPO_PAGO\" = " + tipo_pago;

			return query;
		}

        #endregion 
	}
}