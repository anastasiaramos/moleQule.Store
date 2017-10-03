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
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class REAExpedientBase
	{
		#region Attributes

        private REAExpedientRecord _record = new REAExpedientRecord();

		//NO ENLAZADAS
		internal string _cuenta_cobro = string.Empty;
		internal DateTime _fecha_cobro;
		internal Decimal _ayuda_estimada = 0;
		internal Decimal _ayuda_cobrada = 0;
		internal Decimal _ayuda_pendiente = 0;
		internal string _id_expediente = string.Empty;
		internal long _tipo_expediente = 0;

		#endregion

		#region Properties

        public REAExpedientRecord Record { get { return _record; } set { _record = value; } }

		internal EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		internal string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_tipo_expediente; } }
		internal string TipoExpedienteLabel { get { return moleQule.Store.Structs.EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }
		internal decimal AyudaPendiente { get { return (EEstado == EEstado.Charged) ? 0 : _ayuda_pendiente; } }
		internal decimal AyudaDesestimada { 
			get 
			{
				switch (EEstado)
				{
					case EEstado.Charged: return _ayuda_estimada - _ayuda_cobrada;
					case EEstado.Desestimado: return _ayuda_estimada;
					default: return 0;
				}
			} 
		}

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
			_cuenta_cobro = Format.DataReader.GetString(source, "CUENTA_COBRO");
			_ayuda_estimada = Format.DataReader.GetDecimal(source, "TOTAL_ESTIMADO");
			_ayuda_cobrada = Format.DataReader.GetDecimal(source, "TOTAL_COBRADO");
			_ayuda_pendiente = Format.DataReader.GetDecimal(source, "TOTAL_PENDIENTE");
			_id_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
			_tipo_expediente = Format.DataReader.GetInt64(source, "TIPO_EXPEDIENTE");

			//if ((_ayuda_estimada == _ayuda_cobrada) && (_fecha_cobro != DateTime.MinValue)) EEstado = EEstado.Charged;
		}
		internal void CopyValues(REAExpedient source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_fecha_cobro = source.FechaCobro;
			_cuenta_cobro = source.CuentaCobro;
			_ayuda_estimada = source.AyudaEstimada;
			_ayuda_cobrada = source.AyudaCobrada;
			_ayuda_pendiente = source.AyudaPendiente;	
		}
		internal void CopyValues(ExpedienteREAInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_fecha_cobro = source.FechaCobro;
			_cuenta_cobro = source.CuentaCobro;
			_ayuda_estimada = source.AyudaEstimada;
			_ayuda_cobrada = source.AyudaCobrada;
			_ayuda_pendiente = source.AyudaPendiente;
            _id_expediente = source.IDExpediente;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class REAExpedient : BusinessBaseEx<REAExpedient>, IEntidadRegistro
	{
		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.ExpedienteREA; } }
		public string DescripcionRegistro { get { return "EXPEDIENTE REA Nº " + Codigo + ". Exp: " + IDExpediente; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public virtual void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			REAExpedientRecord obj = Session().Get<REAExpedientRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

	 	#region Attributes

		public REAExpedientBase _base = new REAExpedientBase();

		#endregion
		
		#region Properties

        public REAExpedientBase Base { get { return _base; } }

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
		public virtual string CodigoAduanero
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoAduanero;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoAduanero.Equals(value))
				{
					_base.Record.CodigoAduanero = value;
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
		public virtual string NExpedienteREA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ExpedienteRea;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

				if (!_base.Record.ExpedienteRea.Equals(value))
				{
					_base.Record.ExpedienteRea = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CertificadoREA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CertificadoRea;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

				if (!_base.Record.CertificadoRea.Equals(value))
				{
					_base.Record.CertificadoRea = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string NDUA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NDua;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

				if (!_base.Record.NDua.Equals(value))
				{
					_base.Record.NDua = value;
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
		
		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string CuentaCobro { get { return _base._cuenta_cobro; } }
		public virtual DateTime FechaCobro { get { return _base._fecha_cobro; } }
		public virtual decimal AyudaEstimada { get { return _base._ayuda_estimada; } set { _base._ayuda_estimada = value; } }
		public virtual decimal AyudaCobrada { get { return _base._ayuda_cobrada; } set { _base._ayuda_cobrada = value; } }
		public virtual decimal AyudaPendiente { get { return _base.AyudaPendiente; } set { _base._ayuda_pendiente = value; } }
		public virtual decimal AyudaDesestimada { get { return _base.AyudaDesestimada; } }
		public virtual string IDExpediente { get { return _base._id_expediente; } }
        public virtual bool Cobrado { get { return (EEstado == EEstado.Charged || EEstado == EEstado.Exportado); } }

		#endregion
		
		#region Business Methods
		
		public virtual REAExpedient CloneAsNew()
		{
			REAExpedient clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = REAExpedient.OpenSession();
			REAExpedient.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
        
		protected virtual void CopyFrom(ExpedienteREAInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			_base.CopyValues(source);
		}
		protected virtual void CopyFrom(Expedient source, BatchInfo partida)
		{
			if (source == null) return;

			Oid = partida.Oid;
			OidExpediente = source.Oid;
			Fecha = source.FechaDespachoDestino;

			CodigoAduanero = partida.CodigoAduanero;
			
			decimal kilos = source.Partidas.GetTotalKilos(partida.CodigoAduanero);
			AyudaEstimada = partida.AyudaRecibidaKilo * kilos;
			AyudaPendiente = AyudaEstimada;
		}
		internal virtual void CopyFrom(InputInvoiceInfo source)
		{
			Fecha = (source != null) ? source.Fecha : DateTime.MinValue;
		}

        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(REAExpedient));
            Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }				
			
		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
						
			
			//Propiedad
			/*if (Propiedad <= 0)
			{
				e.Description = string.Format(Resources.Messages.NO_PROPIEDAD_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules

		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EXPEDIENTE);
		}

		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EXPEDIENTE);
		}

		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EXPEDIENTE);
		}

		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EXPEDIENTE);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected REAExpedient()
		{
			if (this.Parent != null)
				((REAExpedients)this.Parent).SetNextCode(this);
			else
				GetNewCode();
		}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private REAExpedient(REAExpedient source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private REAExpedient(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static REAExpedient NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			REAExpedient obj = DataPortal.Create<REAExpedient>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">ExpedienteREA con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(ExpedienteREA source, bool childs)
		/// <remarks/>
		internal static REAExpedient GetChild(REAExpedient source) { return new REAExpedient(source, false); }
		internal static REAExpedient GetChild(REAExpedient source, bool childs) { return new REAExpedient(source, childs); }
        internal static REAExpedient GetChild(int sessionCode, IDataReader source) { return new REAExpedient(sessionCode, source, false); }
        internal static REAExpedient GetChild(int sessionCode, IDataReader source, bool childs) { return new REAExpedient(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual ExpedienteREAInfo GetInfo() { return GetInfo(true); }	
		public virtual ExpedienteREAInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new ExpedienteREAInfo(this, childs);
		}
		
		#endregion				
		
		#region Root Factory Methods

		public static REAExpedient Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = REAExpedient.GetCriteria(REAExpedient.OpenSession());
			criteria.Childs = childs;

			criteria.Query = REAExpedient.SELECT(oid);

			REAExpedient.BeginTransaction(criteria.Session);

			return DataPortal.Fetch<REAExpedient>(criteria);
		}

		public override REAExpedient Save()
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

		#region Child Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static REAExpedient NewChild(Expedient parent, BatchInfo partida)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			REAExpedient obj = DataPortal.Create<REAExpedient>(new CriteriaCs(-1));	
			obj.CopyFrom(parent, partida);
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
	
		#endregion
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			EEstado = EEstado.Solicitado;
			Fecha = DateTime.Now;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(REAExpedient source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);			   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(REAExpedients parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
				
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(REAExpedients parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			REAExpedientRecord obj = Session().Get<REAExpedientRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(REAExpedients parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<REAExpedientRecord>(Oid));
		
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
					REAExpedient.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
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
				iQExceptionHandler.TreatException(ex);
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			REAExpedientRecord obj = Session().Get<REAExpedientRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
		}

		#endregion

		#region Child Data Access
		
		internal void Insert(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidExpediente = parent.Oid;
			Fecha = parent.FechaDespachoDestino;
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);	

			MarkOld();
		}

		internal void Update(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidExpediente = parent.Oid;
			Fecha = parent.FechaDespachoDestino;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			REAExpedientRecord obj = parent.Session().Get<REAExpedientRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);			

			MarkOld();
		}

		internal void DeleteSelf(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<REAExpedientRecord>(Oid));

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

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
        internal static string SELECT(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { ExpedienteREA = REAExpedient.NewChild().GetInfo(false) };
            conditions.ExpedienteREA.Oid = oid;

            query = SELECT(conditions, lockTable);

            return query;
        }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }		
		public static string SELECT(Expedient item) 
		{ 
			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Expedient = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}			
		
        internal static string FIELDS(long queryType)
        {
            string query = @"
            SELECT " + queryType + @" AS ""QUERY""
                    ,ER.*
                    ,EX.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
                    ,EX.""TIPO_EXPEDIENTE"" AS ""TIPO_EXPEDIENTE""
                    ,C.""FECHA"" AS ""FECHA_COBRO""
                    ,CB.""VALOR"" AS ""CUENTA_COBRO""
                    ,CR.""TOTAL_COBRADO"" AS ""TOTAL_COBRADO""
                    ,PT.""TOTAL_ESTIMADO"" AS ""TOTAL_ESTIMADO""
                    ,(COALESCE(PT.""TOTAL_ESTIMADO"", 0) - COALESCE(CR.""TOTAL_COBRADO"", 0)) AS ""TOTAL_PENDIENTE""";

            return query;
        }

		internal static string FIELDS_REA()
		{
			string query = @"
            SELECT 1 AS ""QUERY""
                    ,ER.*
                    ,EX.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
                    ,EX.""TIPO_EXPEDIENTE"" AS ""TIPO_EXPEDIENTE""
                    ,C.""FECHA_COBRO"" AS ""FECHA_COBRO""
                    ,C.""CUENTA_COBRO"" AS ""CUENTA_COBRO""
                    ,C.""TOTAL_COBRADO"" AS ""TOTAL_COBRADO""
                    ,PT.""TOTAL_ESTIMADO"" AS ""TOTAL_ESTIMADO""
                    ,(COALESCE(PT.""TOTAL_ESTIMADO"", 0) - COALESCE(C.""TOTAL_COBRADO"", 0)) AS ""TOTAL_PENDIENTE""";

			return query;
		}

        internal static string JOIN(QueryConditions conditions)
        {
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.REAChargeRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));

            string query = @"
            FROM " + er + @" AS ER
            LEFT JOIN (SELECT CR.""OID_EXPEDIENTE_REA""
                            ,MAX(CR.""OID_COBRO"") AS ""OID_COBRO""
                            ,SUM(CR.""CANTIDAD"") AS ""TOTAL_COBRADO""
                        FROM " + cr + @" AS CR
                        INNER JOIN " + ch + @" AS CB ON CB.""OID"" = CR.""OID_COBRO"" 
                                                        AND CB.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                                                        AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY CR.""OID_EXPEDIENTE_REA"")
                AS CR ON CR.""OID_EXPEDIENTE_REA"" = ER.""OID""
            LEFT JOIN " + ch + @" AS C ON C.""OID"" = CR.""OID_COBRO"" 
                                        AND C.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                                        AND C.""ESTADO"" != " + (long)EEstado.Anulado + @"
            LEFT JOIN " + bk + @" AS CB ON CB.""OID"" = C.""OID_CUENTA_BANCARIA""
            LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = ER.""OID_EXPEDIENTE""
            LEFT JOIN (SELECT PT1.""OID_EXPEDIENTE""
                                ,PR.""CODIGO_ADUANERO""
                                ,SUM(PR.""AYUDA_KILO"" * ""KILOS_INICIALES"") AS ""TOTAL_ESTIMADO""
                        FROM " + ba + @" AS PT1
                        INNER JOIN " + pr + @" AS PR ON PR.""OID"" = PT1.""OID_PRODUCTO""
                        GROUP BY PT1.""OID_EXPEDIENTE"", PR.""CODIGO_ADUANERO"")
                AS PT ON PT.""OID_EXPEDIENTE"" = ER.""OID_EXPEDIENTE"" 
                        AND PT.""CODIGO_ADUANERO"" = ER.""CODIGO_ADUANERO""";

            return query + " " + conditions.ExtraJoin;
        }	

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @"
            WHERE ((ER.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"') OR ER.""FECHA"" ISNULL)";

            query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "ER");

            if (conditions.Estado != EEstado.Todos)
			    query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "ER");

            foreach (IQueryableEntity entity in conditions.Entities)
            {
                switch ((ETipoEntidad)entity.EntityType)
                {
                    case ETipoEntidad.CobroREA:
                        query += @"
                        AND C.""OID"" = " + entity.Oid;
                        break;
                }
            }

            if (conditions.ExpedienteREA != null)
		       if (conditions.ExpedienteREA.Oid != 0)
                   query += @"
                    AND ER.""OID"" = " + conditions.ExpedienteREA.Oid;				
			
            if (conditions.Expedient != null) 
                query += @"
                AND ER.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos) 
                query += @"
                AND EX.""TIPO_EXPEDIENTE"" = " + (long)conditions.TipoExpediente;

            if (conditions.TipoAyudasContabilidad != ETipoAyudaContabilidad.Todas)
            {
                if (conditions.TipoAyudasContabilidad == ETipoAyudaContabilidad.REA)
                    query += @"
                    AND EX.""TIPO_EXPEDIENTE"" = " + (long)ETipoExpediente.Alimentacion;
                else if (conditions.TipoAyudasContabilidad==ETipoAyudaContabilidad.POSEI)
                    query += @"
                    AND EX.""TIPO_EXPEDIENTE"" = " + (long)ETipoExpediente.Ganado;
            }

            return query + " " + conditions.ExtraWhere;
		}

        internal static string ORDER(QueryConditions conditions)
        {            
            if (conditions.Orders == null)
            {
                conditions.Orders = new OrderList();
                conditions.Orders.Add(FilterMng.BuildOrderItem("Fecha", ListSortDirection.Descending, typeof(PayrollBatch)));
                conditions.Orders.Add(FilterMng.BuildOrderItem("Codigo", ListSortDirection.Descending, typeof(PayrollBatch)));
            }

            return ORDER(conditions.Orders, string.Empty, ForeignFields());
        }

 	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query =
            FIELDS(0) +
            JOIN(conditions) +
            WHERE(conditions) +
            ORDER(conditions);

			query += LIMIT(conditions.PagingInfo);

			//query += Common.EntityBase.LOCK("ER", lockTable);

            return query;
        }		
		
		internal static string SELECT_CONTROL_REA(QueryConditions conditions)
		{
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.REAChargeRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string cbn = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));

			string query =  
            FIELDS_REA() + @"
                ,EX.""OID"" AS ""OID_EXPEDIENTE_EX""
            FROM " + ex + @" AS EX
            LEFT JOIN " + er + @" AS ER ON ER.""OID_EXPEDIENTE"" = EX.""OID""
            LEFT JOIN (SELECT PA.""OID_EXPEDIENTE""
                                ,PR.""CODIGO_ADUANERO""
                                ,SUM(CASE WHEN (PA.""AYUDA_RECIBIDA_KILO"" > 0) 
                                        THEN PA.""AYUDA_RECIBIDA_KILO"" 
                                        ELSE PR.""AYUDA_KILO"" END * PA.""KILOS_INICIALES"") AS ""TOTAL_ESTIMADO""
                        FROM " + pa + @" AS PA
                        INNER JOIN " + pr + @" AS PR ON PR.""OID"" = PA.""OID_PRODUCTO""
                        WHERE PA.""AYUDA"" = TRUE
                        GROUP BY PA.""OID_EXPEDIENTE"", PR.""CODIGO_ADUANERO"")
                AS PT ON EX.""OID"" = PT.""OID_EXPEDIENTE"" 
                        AND PT.""CODIGO_ADUANERO"" = ER.""CODIGO_ADUANERO"" 
                        AND EX.""TIPO_EXPEDIENTE"" != " + ((long)ETipoExpediente.Almacen) + @"
            LEFT JOIN (SELECT CR.""OID_EXPEDIENTE_REA""
                                ,MAX(C1.""FECHA"") AS ""FECHA_COBRO""
                                ,MAX(CB.""VALOR"") AS ""CUENTA_COBRO""
                                ,SUM(CR.""CANTIDAD"") AS ""TOTAL_COBRADO""
                        FROM " + cr + @" AS CR
                        INNER JOIN " + ch + @" AS C1 ON C1.""OID"" = CR.""OID_COBRO"" AND C1.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @" AND C1.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        LEFT JOIN " + cbn + @" AS CB ON CB.""OID"" = C1.""OID_CUENTA_BANCARIA""
                        WHERE C1.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                        GROUP BY CR.""OID_EXPEDIENTE_REA"")
                AS C ON C.""OID_EXPEDIENTE_REA"" = ER.""OID""";

			query += @"
            WHERE EX.""AYUDA"" = TRUE
			    AND (EX.""FECHA_DESPACHO_DESTINO"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";
            
            if (conditions.FechaIni <= DateTime.MinValue && conditions.FechaFin >= DateTime.MaxValue)
                query += @" OR EX.""FECHA_DESPACHO_DESTINO"" IS NULL)";
            else 
                query += @")";

			switch (conditions.Estado)
			{
				case EEstado.Pagado:
					query += @"
                        AND (C.""FECHA_COBRO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')
                        AND (round(C.""TOTAL_COBRADO"", 2) >= round(PT.""TOTAL_ESTIMADO"", 2) OR ER.""ESTADO"" IN (" + (long)EEstado.Charged + ", " + (long)EEstado.Exportado + "))";
					break;

				case EEstado.Pendiente:
//                    query += @"
//                        AND (C.""FECHA_COBRO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"' OR C.""FECHA_COBRO"" IS NULL)
//                        AND ((round(PT.""TOTAL_ESTIMADO"" - C.""TOTAL_COBRADO"", 2) > 0 OR C.""TOTAL_COBRADO"" IS NULL)
//                        AND ER.""ESTADO"" IN (" + (long)EEstado.Abierto + ", " + (long)EEstado.Solicitado + ", " + (long)EEstado.Aceptado + "))";

                    query += @"
                        AND (C.""FECHA_COBRO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"' OR C.""FECHA_COBRO"" IS NULL)
                        AND ER.""ESTADO"" IN (" + (long)EEstado.Abierto + ", " + (long)EEstado.Solicitado + ", " + (long)EEstado.Aceptado + ")";

                    break;

				case EEstado.Todos:
					query += @"
                        AND (C.""FECHA_COBRO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "'";
                    
                    if (conditions.FechaAuxIni <= DateTime.MinValue && conditions.FechaAuxFin >= DateTime.MaxValue)
                        query += @" OR C.""FECHA_COBRO"" IS NULL)";
                    else
                        query += @")";
					break;
			}

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += @"
                    AND EX.""TIPO_EXPEDIENTE"" = " + (long)conditions.TipoExpediente;

			return query;
		}

        internal static string SELECT_BY_CHARGE_AND_UNLINKED(QueryConditions conditions, bool lockTable)
        {
            string query =
            FIELDS(0) +
            JOIN(conditions) +
            WHERE(conditions) + @"
            UNION" +
            SELECT_UNLINKED(conditions, lockTable);

            return query;
        }

        internal static string SELECT_UNLINKED(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
            AND COALESCE(C.""OID"", 0) = 0";

            string query =
            FIELDS(0) +
            JOIN(conditions) +
            WHERE(conditions) +
            ORDER(conditions);

            conditions.ExtraWhere = string.Empty;

            return query;
        }

		#endregion
	}
}