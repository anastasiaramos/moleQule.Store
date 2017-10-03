using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ExpenseTypeBase
	{
		#region Attributes

		private ExpenseTypeRecord _record = new ExpenseTypeRecord();

		private string _cuenta_asociada = string.Empty;

		#endregion

		#region Properties

		public ExpenseTypeRecord Record { get { return _record; } }

		public virtual ECategoriaGasto ECategoriaGasto { get { return (ECategoriaGasto)_record.Categoria; } }
		public virtual string CategoriaGastoLabel { get { return EnumText<ECategoriaGasto>.GetLabel(ECategoriaGasto); } }
		public virtual EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
		public virtual string FormaPagoLabel { get { return moleQule.Common.Structs.EnumText<EFormaPago>.GetLabel(EFormaPago); ; } }
		public virtual EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
		public virtual string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual string CuentaAsociada { get { return _cuenta_asociada; } set { _cuenta_asociada = value;  } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_cuenta_asociada = Format.DataReader.GetString(source, "CUENTA_ASOCIADA");
		}
		internal void CopyValues(TipoGasto source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_asociada = source.CuentaAsociada;
		}
		internal void CopyValues(TipoGastoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_asociada = source.CuentaAsociada;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class TipoGasto : BusinessBaseEx<TipoGasto>
	{	 
		#region Attributes

		protected ExpenseTypeBase _base = new ExpenseTypeBase();

		#endregion
		
		#region Properties

		public ExpenseTypeBase Base { get { return _base; } }

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
		public virtual long Categoria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Categoria;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Categoria.Equals(value))
				{
					_base.Record.Categoria = value;
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
		public virtual long FormaPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FormaPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FormaPago.Equals(value))
				{
					_base.Record.FormaPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long DiasPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.DiasPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.DiasPago.Equals(value))
				{
					_base.Record.DiasPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCuentaBAsociada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaAsociada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaAsociada.Equals(value))
				{
					_base.Record.OidCuentaAsociada = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaBancaria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaBancaria;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaBancaria.Equals(value))
				{
					_base.Record.CuentaBancaria = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContable.Equals(value))
				{
					_base.Record.CuentaContable = value;
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
		public virtual ECategoriaGasto ECategoriaGasto { get { return _base.ECategoriaGasto; } }
		public virtual string CategoriaGastoLabel { get { return _base.CategoriaGastoLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual string CuentaAsociada { get { return _base.CuentaAsociada; } set { _base.CuentaAsociada = value; PropertyHasChanged(); } }

		#endregion
		
		#region Business Methods
		
		public virtual TipoGasto CloneAsNew()
		{
			TipoGasto clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = TipoGasto.OpenSession();
			TipoGasto.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
	
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(TipoGasto));
            Codigo = Serial.ToString(Resources.Defaults.TIPOGASTO_CODE_FORMAT);
        }				
			
		#endregion
		 
	    #region Validation Rules

        protected override void AddBusinessRules()
        {
			ValidationRules.AddRule(CheckValidation, "Oid");
        }

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Categoria
			if (_base.Record.Categoria <= 0)
			{
				e.Description = Resources.Messages.NO_CATEGORIA_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
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
					if (_base.Record.OidCuentaAsociada == 0) throw new iQValidationException(e.Description, string.Empty);
					break;
			}

			//Tarjeta de Credito
			/*switch (EMedioPago)
            {
                case EMedioPago.Tarjeta:
                    e.Description = Resources.Messages.NO_TARJETA_CREDITO_SELECTED;
                    if (_oid_tarjeta_credito == 0) throw new iQValidationException(e.Description, string.Empty);
                    break;
            }*/

            return true;
        }	
		 
		#endregion
		 
		#region Autorization Rules
		
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

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoGasto = TipoGasto.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.TipoGasto.Oid = oid;

			ExpenseList gastos = ExpenseList.GetList(conditions, false);

			if (gastos.Count > 0)
				throw new iQException(Resources.Messages.GASTOS_ASOCIADOS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected TipoGasto () {}

		private TipoGasto(TipoGasto source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
        private TipoGasto(IDataReader source, bool retrieve_childs)
        {
            MarkAsChild();	
			Childs = retrieve_childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static TipoGasto NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			TipoGasto obj = DataPortal.Create<TipoGasto>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">TipoGasto con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(TipoGasto source, bool retrieve_childs)
		/// <remarks/>
		internal static TipoGasto GetChild(TipoGasto source)
		{
			return new TipoGasto(source, false);
		}
		internal static TipoGasto GetChild(TipoGasto source, bool retrieve_childs)
		{
			return new TipoGasto(source, retrieve_childs);
		}
        internal static TipoGasto GetChild(IDataReader source)
        {
            return new TipoGasto(source, false);
        }
        internal static TipoGasto GetChild(IDataReader source, bool retrieve_childs)
        {
            return new TipoGasto(source, retrieve_childs);
        }

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TipoGastoInfo GetInfo() { return GetInfo(true); }
		public virtual TipoGastoInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TipoGastoInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static TipoGasto New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<TipoGasto>(new CriteriaCs(-1));
		}
		
		public static TipoGasto Get(long oid) { return Get(oid, true); }
		public static TipoGasto Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = TipoGasto.GetCriteria(TipoGasto.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TipoGasto.SELECT(oid);
				
			TipoGasto.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<TipoGasto>(criteria);
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
		
		/// <summary>
		/// Elimina todos los TipoGasto. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = TipoGasto.OpenSession();
			ISession sess = TipoGasto.Session(sessCode);
			ITransaction trans = TipoGasto.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from ExpenseTypeRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				TipoGasto.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override TipoGasto Save()
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
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			GetNewCode();			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(TipoGasto source)
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
		internal void Insert(TipoGastos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
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
		internal void Update(TipoGastos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			ExpenseTypeRecord obj = Session().Get<ExpenseTypeRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(TipoGastos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<ExpenseTypeRecord>(Oid));
		
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
					TipoGasto.DoLOCK(Session());
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
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			//Borrar si no hay código
			GetNewCode();

			Session().Save(Base.Record);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			ExpenseTypeRecord obj = Session().Get<ExpenseTypeRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			MarkOld();
			
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
				Session().Delete((ExpenseTypeRecord)(criterio.UniqueResult()));
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
		
        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

			query = "SELECT TG.*" +
					"       ,COALESCE(CB.\"VALOR\", '') AS \"CUENTA_ASOCIADA\"";
					//"       ,COALESCE(TC.\"NOMBRE\", '') AS \"TARJETA_CREDITO\"" +

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = " WHERE TRUE";
 
            if (conditions.TipoGasto != null) query += " AND TG.\"OID\" = " + conditions.TipoGasto.Oid.ToString();
            if (conditions.CategoriaGasto == ECategoriaGasto.Seleccione) query += " AND TG.\"CATEGORIA\" != " + (long)ECategoriaGasto.Nomina;

			return query;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string t = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseTypeRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + t + " AS TG" +
					" LEFT JOIN " + cb + " AS CB ON CB.\"OID\" = TG.\"OID_CUENTA_ASOCIADA\"" +
					WHERE(conditions);	
		
			if (lockTable) query += " FOR UPDATE OF TG NOWAIT";

            return query;
        }
		
		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { TipoGasto = TipoGasto.New().GetInfo(false) };
			conditions.TipoGasto.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}	
	
		#endregion
	}
}