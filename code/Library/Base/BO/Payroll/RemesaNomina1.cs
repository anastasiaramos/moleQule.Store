using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.moleQule.Libary.Store
{
	[Serializable()]
	public class RemesaNominaRecord : RecordBase
	{
		#region Attributes

		private long _serial;
		private string _codigo = string.Empty;
		private DateTime _fecha;
		private string _descripcion = string.Empty;
		private Decimal _total;
		private Decimal _irpf;
		private Decimal _seguro_empresa;
		private Decimal _seguro_personal;
		private DateTime _prevision_pago;
		private long _estado;
		private string _observaciones = string.Empty;
		private Decimal _base_irpf;
		private Decimal _descuentos;
  
		#endregion
		
		#region Properties
		
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
public virtual Decimal Total { get { return _total; } set { _total = value; } }
public virtual Decimal Irpf { get { return _irpf; } set { _irpf = value; } }
public virtual Decimal SeguroEmpresa { get { return _seguro_empresa; } set { _seguro_empresa = value; } }
public virtual Decimal SeguroPersonal { get { return _seguro_personal; } set { _seguro_personal = value; } }
public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
public virtual long Estado { get { return _estado; } set { _estado = value; } }
public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
public virtual Decimal BaseIrpf { get { return _base_irpf; } set { _base_irpf = value; } }
public virtual Decimal Descuentos { get { return _descuentos; } set { _descuentos = value; } }

		#endregion
		
		#region Business Methods
		
		public RemesaNominaRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_irpf = Format.DataReader.GetDecimal(source, "IRPF");
			_seguro_empresa = Format.DataReader.GetDecimal(source, "SEGURO_EMPRESA");
			_seguro_personal = Format.DataReader.GetDecimal(source, "SEGURO_PERSONAL");
			_prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_base_irpf = Format.DataReader.GetDecimal(source, "BASE_IRPF");
			_descuentos = Format.DataReader.GetDecimal(source, "DESCUENTOS");

		}		
		public virtual void CopyValues(RemesaNominaRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_fecha = source.Fecha;
			_descripcion = source.Descripcion;
			_total = source.Total;
			_irpf = source.Irpf;
			_seguro_empresa = source.SeguroEmpresa;
			_seguro_personal = source.SeguroPersonal;
			_prevision_pago = source.PrevisionPago;
			_estado = source.Estado;
			_observaciones = source.Observaciones;
			_base_irpf = source.BaseIrpf;
			_descuentos = source.Descuentos;
		}
		
		#endregion	
	}

    [Serializable()]
	public class RemesaNominaBase 
	{	 
		#region Attributes
		
		private RemesaNominaRecord _record = new RemesaNominaRecord();
		
		#endregion
		
		#region Properties
		
		public RemesaNominaRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}		
		internal void CopyValues(RemesaNomina source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(RemesaNominaInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class RemesaNomina : BusinessBaseEx<RemesaNomina>
	{	 
		#region Attributes
		
		protected RemesaNominaBase _base = new RemesaNominaBase();
		

		#endregion
		
		#region Properties
		
		public RemesaNominaBase Base { get { return _base; } }
		
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
		public virtual Decimal Irpf
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Irpf;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Irpf.Equals(value))
				{
					_base.Record.Irpf = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SeguroEmpresa
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SeguroEmpresa;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.SeguroEmpresa.Equals(value))
				{
					_base.Record.SeguroEmpresa = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SeguroPersonal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SeguroPersonal;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.SeguroPersonal.Equals(value))
				{
					_base.Record.SeguroPersonal = value;
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
		public virtual Decimal BaseIrpf
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
					PropertyHasChanged();
				}
			}
		}
	
		
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		
		
		#endregion
		
		#region Business Methods
		
		public virtual RemesaNomina CloneAsNew()
		{
			RemesaNomina clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = RemesaNomina.OpenSession();
			RemesaNomina.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(RemesaNominaInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			Serial = source.Serial;
			Codigo = source.Codigo;
			Fecha = source.Fecha;
			Descripcion = source.Descripcion;
			Total = source.Total;
			Irpf = source.Irpf;
			SeguroEmpresa = source.SeguroEmpresa;
			SeguroPersonal = source.SeguroPersonal;
			PrevisionPago = source.PrevisionPago;
			Estado = source.Estado;
			Observaciones = source.Observaciones;
			BaseIrpf = source.BaseIrpf;
			Descuentos = source.Descuentos;
		}
		
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(RemesaNomina));
            Codigo = Serial.ToString(Resources.Defaults.REMESANOMINA_CODE_FORMAT);
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
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.SECUREITEM);
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.SECUREITEM);
        }
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.SECUREITEM);
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.SECUREITEM);
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				RemesaNomina = RemesaNominaInfo.New(oid),
			};


		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected RemesaNomina () 
		{
			Oid = (long)(new Random()).Next();
			EStatus = EEstado.Active;	
		}
				
		private RemesaNomina(RemesaNomina source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private RemesaNomina(int sessionCode, IDataReader source, bool childs)
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
		public static RemesaNomina NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			RemesaNomina obj = DataPortal.Create<RemesaNomina>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">RemesaNomina con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(RemesaNomina source, bool childs)
		/// <remarks/>
		internal static RemesaNomina GetChild(RemesaNomina source) { return new RemesaNomina(source, false); }
		internal static RemesaNomina GetChild(RemesaNomina source, bool childs) { return new RemesaNomina(source, childs); }
        internal static RemesaNomina GetChild(int sessionCode, IDataReader source) { return new RemesaNomina(sessionCode, source, false); }
        internal static RemesaNomina GetChild(int sessionCode, IDataReader source, bool childs) { return new RemesaNomina(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual RemesaNominaInfo GetInfo() { return GetInfo(true); }	
		public virtual RemesaNominaInfo GetInfo (bool childs) { return new RemesaNominaInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static RemesaNomina New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			RemesaNomina obj = DataPortal.Create<RemesaNomina>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
		
		public new static RemesaNomina Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<RemesaNomina>.Get(query, childs, -1);
		}
		
		public static RemesaNomina Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
		
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
		/// Elimina todos los RemesaNomina. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = RemesaNomina.OpenSession();
			ISession sess = RemesaNomina.Session(sessCode);
			ITransaction trans = RemesaNomina.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from RemesaNomina");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				RemesaNomina.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override RemesaNomina Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
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
				
				if (!SharedTransaction) Transaction().Commit();
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
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
				}
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
		private void Fetch(RemesaNomina source)
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
		internal void Insert(RemesaNominas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(this);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(RemesaNominas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			RemesaNominaRecord obj = Session().Get<RemesaNominaRecord>(Oid);
			obj._base.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(RemesaNominas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<RemesaNominaRecord>(Oid));
		
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
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					//RemesaNomina.DoLOCK(Session());
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
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			if (!SharedTransaction)
			{
				SessionCode = OpenSession();
				BeginTransaction();
			}			
			
			GetNewCode();
			
			Session().Save(this);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			RemesaNominaRecord obj = Session().Get<RemesaNominaRecord>(Oid);
			obj.CopyValues(this.Base.Record);
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
				Session().Delete((RemesaNominaRecord)(criterio.UniqueResult()));
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

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT RE.*";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string re = nHManager.Instance.GetSQLTable(typeof(RemesaNominaRecord));

			string query;

            query = @"
				FROM " + re + @" AS RE";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			string query;

            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "SU", ForeignFields());
				
			query = @" 
				AND (RE.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
 
			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "RE");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "RE");
			
            if (conditions.RemesaNomina != null)
				query += @"
					AND R.""OID"" = " + conditions.RemesaNomina.Oid;
				
			
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
				SELECT_FIELDS() + 
				JOIN(conditions) +
				WHERE(conditions) +		
                ORDER(conditions.Orders, "RE", ForeignFields());

            if (conditions.PagingInfo != null) query += LIMIT(conditions.PagingInfo);

			query += Common.EntityBase.LOCK("RE", lockTable);

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
		
		internal static string SELECT(long oid, bool lockTable)
        {			
			return SELECT(new QueryConditions { RemesaNomina = RemesaNominaInfo.New(oid) }, lockTable);
        }
		
		#endregion
	}
	
	[Serializable()]
	public class RemesaNominaMap : ClassMapping<RemesaNominaRecord>
	{	
		public RemesaNominaMap()
		{
			Table("`RemesaNomina`");
			//Schema("``");
			Lazy(true);	
			
			Id(x => x.Oid, map =>
            {
                map.Generator(Generators.Sequence,
								gmap => gmap.Params(new { max_low = 100 }));
                map.Column("`OID`");
            });
			Property(x => x.Serial, map =>
			{
				map.Column("`SERIAL`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Codigo, map =>
			{
				map.Column("`CODIGO`");
				map.NotNullable(false);
				map.Length(255);
			});
			Property(x => x.Fecha, map =>
			{
				map.Column("`FECHA`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Descripcion, map =>
			{
				map.Column("`DESCRIPCION`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Total, map =>
			{
				map.Column("`TOTAL`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Irpf, map =>
			{
				map.Column("`IRPF`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.SeguroEmpresa, map =>
			{
				map.Column("`SEGURO_EMPRESA`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.SeguroPersonal, map =>
			{
				map.Column("`SEGURO_PERSONAL`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.PrevisionPago, map =>
			{
				map.Column("`PREVISION_PAGO`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Estado, map =>
			{
				map.Column("`ESTADO`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Observaciones, map =>
			{
				map.Column("`OBSERVACIONES`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.BaseIrpf, map =>
			{
				map.Column("`BASE_IRPF`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.Descuentos, map =>
			{
				map.Column("`DESCUENTOS`");
				map.NotNullable(false);
				map.Length(32768);
				});
				}
	}
}
