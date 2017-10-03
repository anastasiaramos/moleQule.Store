using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LivestockBookRecord : RecordBase
    {
        #region Attributes

        private long _serial;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private Decimal _balance;
        private long _estado;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual Decimal Balance { get { return _balance; } set { _balance = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public LivestockBookRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _balance = Format.DataReader.GetDecimal(source, "BALANCE");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(LivestockBookRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _nombre = source.Nombre;
            _balance = source.Balance;
            _estado = source.Estado;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

    [Serializable()]
    public class LivestockBookBase
    {
        #region Attributes

        private LivestockBookRecord _record = new LivestockBookRecord();

        #endregion

        #region Properties

        public LivestockBookRecord Record { get { return _record; } }

        public EEstado EStatus { get { return (EEstado)_record.Estado; } }
        public string StatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EStatus); } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(LivestockBook source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(LivestockBookInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class LivestockBook : BusinessBaseEx<LivestockBook>
	{	 
		#region Attributes

        public LivestockBookBase _base = new LivestockBookBase();

		private LivestockBookLines _lineas = LivestockBookLines.NewChildList();

		#endregion
		
		#region Properties

        public LivestockBookBase Base { get { return _base; } }

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
		public virtual Decimal Balance
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Balance;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Balance.Equals(value))
				{
					_base.Record.Balance = value;
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
		
		public virtual LivestockBookLines Lineas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _lineas;
			}
		}

		public virtual EEstado EEstado { get { return (EEstado)_base.Record.Estado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return moleQule.Base.EnumText<EEstado>.GetLabel(EEstado); } }

		/// <summary>
        /// Indica si el objeto está validado
        /// </summary>
		/// <remarks>Para añadir una lista: && _lista.IsValid<remarks/>
		public override bool IsValid
		{
			get { return base.IsValid
						 && _lineas.IsValid ; }
		}
		
        /// <summary>
        /// Indica si el objeto está "sucio" (se ha modificado) y se debe actualizar en la base de datos
        /// </summary>
		/// <remarks>Para añadir una lista: || _lista.IsDirty<remarks/>
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lineas.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual LivestockBook CloneAsNew()
		{
			LivestockBook clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = LivestockBook.OpenSession();
			LivestockBook.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Lineas.MarkAsNew();
			
			return clon;
		}	
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(LivestockBook));
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
				e.Description = String.Format(moleQule.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
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
		/// </summary>
		protected LivestockBook () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private LivestockBook(LivestockBook source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private LivestockBook(int sessionCode, IDataReader source, bool childs)
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
		public static LivestockBook NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			LivestockBook obj = DataPortal.Create<LivestockBook>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">LibroGanadero con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(LibroGanadero source, bool childs)
		/// <remarks/>
		internal static LivestockBook GetChild(long oid, bool childs)
		{
			LivestockBook obj = Get(oid, childs);
			obj.MarkAsChild();

			return obj;
		}
		internal static LivestockBook GetChild(LivestockBook source) { return new LivestockBook(source, false); }
		internal static LivestockBook GetChild(LivestockBook source, bool childs) { return new LivestockBook(source, childs); }
        internal static LivestockBook GetChild(int sessionCode, IDataReader source) { return new LivestockBook(sessionCode, source, false); }
        internal static LivestockBook GetChild(int sessionCode, IDataReader source, bool childs) { return new LivestockBook(sessionCode, source, childs); }

		internal static LivestockBook GetChild(int sessionCode, long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = LivestockBook.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LivestockBook.SELECT(oid);

			LivestockBook obj = DataPortal.Fetch<LivestockBook>(criteria);
			obj.MarkAsChild();

			return obj;
		}

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual LivestockBookInfo GetInfo() { return GetInfo(true); }	
		public virtual LivestockBookInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LivestockBookInfo(this, childs);
		}

		public virtual LivestockBookLines LoadLineasByExpediente(long oid, bool childs)
		{
			LivestockBookLines lineas = LivestockBookLines.GetChildListByExpediente(this, oid, childs);

			foreach (LivestockBookLine item in lineas)
				if ((Lineas.GetItem(item.Oid) == null) && (!Lineas.ContainsDeleted(item.Oid)))
					Lineas.AddItem(item);

			return lineas;
		}

		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static LivestockBook New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<LivestockBook>(new CriteriaCs(-1));
		}
		
		public static LivestockBook Get(long oid) { return Get(oid, true); }
		public static LivestockBook Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = LivestockBook.GetCriteria(LivestockBook.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LivestockBook.SELECT(oid);
				
			LivestockBook.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<LivestockBook>(criteria);
		}
		public static LivestockBook Get(long oid, bool childs, bool cache)
		{
			LivestockBook item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(LivestockBooks)))
			{
				LivestockBooks items = LivestockBooks.NewList();

				item = LivestockBook.GetChild(oid, childs);
				items.AddItem(item);
				items.SessionCode = item.SessionCode;
				Cache.Instance.Save(typeof(LivestockBooks), items);
			}
			else
			{
				LivestockBooks items = Cache.Instance.Get(typeof(LivestockBooks)) as LivestockBooks;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = LivestockBook.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(LivestockBooks), items);
				}
			}

			return item;
		}
		public static LivestockBook Get(long oid, bool childs, bool cache, int sessionCode)
		{
			LivestockBook item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(LivestockBooks)))
			{
				LivestockBooks items = LivestockBooks.NewList();

				item = LivestockBook.GetChild(sessionCode, oid, childs);
				items.AddItem(item);
				items.SessionCode = sessionCode;
				Cache.Instance.Save(typeof(LivestockBooks), items);
			}
			else
			{
				LivestockBooks items = Cache.Instance.Get(typeof(LivestockBooks)) as LivestockBooks;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = LivestockBook.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(LivestockBooks), items);
				}
			}

			return item;
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
		/// Elimina todos los LibroGanadero. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = LivestockBook.OpenSession();
			ISession sess = LivestockBook.Session(sessCode);
			ITransaction trans = LivestockBook.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from LibroGanaderoRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				LivestockBook.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override LivestockBook Save()
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
				
				_lineas.Update(this);				
				
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

		public override LivestockBook SaveAsChild()
		{
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				base.SaveAsChild();

				if (_save_childs)
				{
					_lineas.Update(this);
				}

				return this;
			}
			catch (Exception ex)
			{
				//if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally	{}
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
		private void Fetch(LivestockBook source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{					
					LivestockBookLine.DoLOCK(Session());
					string query = LivestockBookLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lineas = LivestockBookLines.GetChildList(SessionCode, reader);					
				}
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
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{					
					LivestockBookLine.DoLOCK(Session());
					string query = LivestockBookLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lineas = LivestockBookLines.GetChildList(SessionCode, reader);					
				}
			}   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(LivestockBooks parent)
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
		internal void Update(LivestockBooks parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			LivestockBookRecord obj = Session().Get<LivestockBookRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(LivestockBooks parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<LivestockBookRecord>(Oid));
		
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
					LivestockBook.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						LivestockBookLine.DoLOCK(Session());
						query = LivestockBookLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_lineas = LivestockBookLines.GetChildList(SessionCode, reader);						
 					} 
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
			
			LivestockBookRecord obj = Session().Get<LivestockBookRecord>(Oid);
			obj.CopyValues(this._base.Record);
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
				Session().Delete((LivestockBookRecord)(criterio.UniqueResult()));
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

            query = "SELECT L.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = " WHERE TRUE";
 
            if (conditions.LibroGanadero != null)
		       if (conditions.LibroGanadero.Oid != 0)
                   query += " AND L.\"OID\" = " + conditions.LibroGanadero.Oid;

			return query;
		}
		
        internal static string SELECT(long oid, bool lockTable)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { LibroGanadero = LivestockBook.New().GetInfo(false) };
			conditions.LibroGanadero.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string l = nHManager.Instance.GetSQLTable(typeof(LivestockBookRecord));
            
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + l + " AS L";
					
			query += WHERE(conditions);	
		
			//if (lockTable) query += " FOR UPDATE OF L NOWAIT";

            return query;
        }
		
		#endregion
	}
}
