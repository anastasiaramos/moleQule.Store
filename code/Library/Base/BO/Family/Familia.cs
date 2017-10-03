using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Library.CslaEx; 
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class FamilyRecord : RecordBase
    {
        #region Attributes

        private long _oid_impuesto;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _cuenta_contable_compra = string.Empty;
        private string _cuenta_contable_venta = string.Empty;
        private string _observaciones = string.Empty;
        private bool _avisar_beneficio_minimo = false;
        private Decimal _p_beneficio_minimo;

        #endregion

        #region Properties
        public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string CuentaContableCompra { get { return _cuenta_contable_compra; } set { _cuenta_contable_compra = value; } }
        public virtual string CuentaContableVenta { get { return _cuenta_contable_venta; } set { _cuenta_contable_venta = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual bool AvisarBeneficioMinimo { get { return _avisar_beneficio_minimo; } set { _avisar_beneficio_minimo = value; } }
        public virtual Decimal PBeneficioMinimo { get { return _p_beneficio_minimo; } set { _p_beneficio_minimo = value; } }

        #endregion

        #region Business Methods

        public FamilyRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _cuenta_contable_compra = Format.DataReader.GetString(source, "CUENTA_CONTABLE_COMPRA");
            _cuenta_contable_venta = Format.DataReader.GetString(source, "CUENTA_CONTABLE_VENTA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _avisar_beneficio_minimo = Format.DataReader.GetBool(source, "AVISAR_BENEFICIO_MINIMO");
            _p_beneficio_minimo = Format.DataReader.GetDecimal(source, "P_BENEFICIO_MINIMO");

        }

        public virtual void CopyValues(FamilyRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_impuesto = source.OidImpuesto;
            _codigo = source.Codigo;
            _nombre = source.Nombre;
            _cuenta_contable_compra = source.CuentaContableCompra;
            _cuenta_contable_venta = source.CuentaContableVenta;
            _observaciones = source.Observaciones;
            _avisar_beneficio_minimo = source.AvisarBeneficioMinimo;
            _p_beneficio_minimo = source.PBeneficioMinimo;
        }
        #endregion
    }

    [Serializable()]
    public class FamilyBase
    {
        #region Attributes

        private FamilyRecord _record = new FamilyRecord();

        private string _impuesto = string.Empty;
        private decimal _p_impuesto;

        #endregion

        #region Properties

        public FamilyRecord Record { get { return _record; } }

        public virtual ETipoFamilia ETipoFamilia { get { return (ETipoFamilia)_record.Oid; } }

        public virtual string Impuesto { get { return (_record.OidImpuesto != 0) ? _impuesto : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } set { _impuesto = value; } }
        public virtual decimal PImpuesto { get { return _p_impuesto; } set { _p_impuesto = value; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _impuesto = Format.DataReader.GetString(source, "IMPUESTO");
            _p_impuesto = Format.DataReader.GetDecimal(source, "P_IMPUESTO");
        }

        internal void CopyValues(Familia source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _impuesto = source.Impuesto;
            _p_impuesto = source.PImpuesto;
        }
        internal void CopyValues(FamiliaInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _impuesto = source.Impuesto;
            _p_impuesto = source.PImpuesto;
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Familia : BusinessBaseEx<Familia>
	{	 
		#region Attributes

        public FamilyBase _base = new FamilyBase();

		#endregion
		
		#region Properties

        public FamilyBase Base { get { return _base; } }

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
        public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Nombre;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Nombre.Equals(value))
				{
                    _base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string CuentaContableCompra
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CuentaContableCompra;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
				if (!CanEditCuentaContable()) return;
                if (value == null) value = string.Empty;
                if (!_base.Record.CuentaContableCompra.Equals(value))
                {
                    _base.Record.CuentaContableCompra = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string CuentaContableVenta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.CuentaContableVenta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!CanEditCuentaContable()) return;
				if (value == null) value = string.Empty;
                if (!_base.Record.CuentaContableVenta.Equals(value))
				{
                    _base.Record.CuentaContableVenta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidImpuesto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidImpuesto;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidImpuesto.Equals(value))
                {
                    _base.Record.OidImpuesto = value;
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
        public virtual bool AvisarBeneficioMinimo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.AvisarBeneficioMinimo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.AvisarBeneficioMinimo.Equals(value))
                {
                    _base.Record.AvisarBeneficioMinimo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual decimal PBeneficioMinimo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PBeneficioMinimo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.PBeneficioMinimo.Equals(value))
                {
                    _base.Record.PBeneficioMinimo = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual ETipoFamilia ETipoFamilia { get { return (ETipoFamilia)Oid; } }

        public virtual string Impuesto { get { return (_base.Record.OidImpuesto != 0) ? _base.Impuesto : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } }
        public virtual decimal PImpuesto { get { return _base.PImpuesto; } }

		#endregion
		
		#region Business Methods
		
		public virtual Familia CloneAsNew()
		{
			Familia clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = Familia.OpenSession();
			Familia.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
        
        protected virtual void CopyFrom(FamiliaInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.Codigo = source.Codigo;
            _base.Record.Nombre = source.Nombre;
            _base.Record.CuentaContableCompra = source.CuentaContableCompra;
            _base.Record.CuentaContableVenta = source.CuentaContableVenta;
            _base.Record.Observaciones = source.Observaciones;
            _base.Record.AvisarBeneficioMinimo = source.AvisarBeneficioMinimo;
            _base.Record.PBeneficioMinimo = source.PBeneficioMinimo;

            _base.Impuesto = source.Impuesto;
            _base.PImpuesto = source.PImpuesto;
        }

        public virtual void SetImpuesto(ImpuestoInfo source)
        {
            if (source == null)
            {
                OidImpuesto = 0;
                _base.Impuesto = moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto);
                _base.PImpuesto = 0;
            }
            else
            {
                OidImpuesto = source.Oid;
                _base.Impuesto = source.Nombre;
                _base.PImpuesto = source.Porcentaje;
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
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PRODUCTO);
		}

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Familia = Familia.New().GetInfo(false),
				Estado = EEstado.NoAnulado,
			};
			conditions.Familia.Oid = oid;

			ProductList productos = ProductList.GetList(conditions, false);

			if (productos.Count > 0)
				throw new iQException(Resources.Messages.PRODUCTOS_ASOCIADOS);

			SerieFamiliaList series = SerieFamiliaList.GetList(conditions, false);

			if (series.Count > 0)
				throw new iQException(Resources.Messages.SERIES_ASOCIADAS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Familia () {}
		private Familia(Familia source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private Familia(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Familia NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Familia obj = DataPortal.Create<Familia>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Familia con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Familia source, bool retrieve_childs)
		/// <remarks/>
		internal static Familia GetChild(Familia source) { return new Familia(source, false); }
		internal static Familia GetChild(Familia source, bool childs) { return new Familia(source, childs); }

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="reader">DataReader con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool retrieve_childs)
		/// <remarks/>
        internal static Familia GetChild(int sessionCode, IDataReader source) { return new Familia(sessionCode, source, false); }
		internal static Familia GetChild(int sessionCode, IDataReader source, bool childs) { return new Familia(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual FamiliaInfo GetInfo() { return GetInfo(true); }
		public virtual FamiliaInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException( Library.Resources.Messages.USER_NOT_ALLOWED);
	
			return new FamiliaInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Familia New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Familia>(new CriteriaCs(-1));
		}
		
		public static Familia Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Familia.GetCriteria(Familia.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Familia.SELECT(oid);
			
			Familia.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Familia>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Familia. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Familia.OpenSession();
			ISession sess = Familia.Session(sessCode);
			ITransaction trans = Familia.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from FamilyRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Familia.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Familia Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
                throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

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
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor
			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Familia source)
		{
            try
            {
                SessionCode = source.SessionCode;

				_base.CopyValues(source);
				
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Familias parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Familias parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
                FamilyRecord obj = Session().Get<FamilyRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Familias parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<FamilyRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Familia.DoLOCK(Session());
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
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
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
				//si hay codigo o serial, hay que obtenerlos aquí por si ha habido
				//inserciones de otros usuarios en la tabla
				
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					FamilyRecord obj = Session().Get<FamilyRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
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
				Session().Delete((FamilyRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion
				
        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT FM.*" +
                   "        ,IP.\"NOMBRE\" AS \"IMPUESTO\"" +
                   "        ,IP.\"PORCENTAJE\" AS \"P_IMPUESTO\"";

            return query;
        }

        internal static string SELECT(long oid, bool lockTable)
        {
            string fm = nHManager.Instance.GetSQLTable(typeof(FamilyRecord));
            string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
            string query;

            query = SELECT_FIELDS() +
                    " FROM " + fm + " AS FM" +
                    " LEFT JOIN " + ip + " AS IP ON IP.\"OID\" = FM.\"OID_IMPUESTO\"";

            if (oid > 0) query += " WHERE FM.\"OID\" = " + oid.ToString();

            if (lockTable) query += " FOR UPDATE OF FM NOWAIT";

            return query;
        }
	
		#endregion

	}
}

