using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.CslaEx;
using moleQule;
using NHibernate;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LineaInventarioRecord : RecordBase
    {
        #region Attributes

        private long _oid_inventario;
        private long _oid_lineaalmacen;
        private string _concepto = string.Empty;
        private Decimal _cantidad;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual long OidInventario { get { return _oid_inventario; } set { _oid_inventario = value; } }
        public virtual long OidLineaalmacen { get { return _oid_lineaalmacen; } set { _oid_lineaalmacen = value; } }
        public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
        public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public LineaInventarioRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_inventario = Format.DataReader.GetInt64(source, "OID_INVENTARIO");
            _oid_lineaalmacen = Format.DataReader.GetInt64(source, "OID_LINEAALMACEN");
            _concepto = Format.DataReader.GetString(source, "CONCEPTO");
            _cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(LineaInventarioRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_inventario = source.OidInventario;
            _oid_lineaalmacen = source.OidLineaalmacen;
            _concepto = source.Concepto;
            _cantidad = source.Cantidad;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

    [Serializable()]
    public class LineaInventarioBase
    {
        #region Attributes

        private LineaInventarioRecord _record = new LineaInventarioRecord();

        #endregion

        #region Properties

        public LineaInventarioRecord Record { get { return _record; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(LineaInventario source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(LineaInventarioInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class LineaInventario : BusinessBaseEx<LineaInventario>
	{	 
		#region Attributes

        public LineaInventarioBase _base = new LineaInventarioBase();

		#endregion
		
		#region Properties

		public LineaInventarioBase Base { get { return _base; } }

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
				}
			}
		}
		public virtual long OidInventario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidInventario;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.OidInventario.Equals(value))
				{
                    _base.Record.OidInventario = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidLineaAlmacen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidLineaalmacen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidLineaalmacen.Equals(value))
                {
                    _base.Record.OidLineaalmacen = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string Concepto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Concepto;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Concepto.Equals(value))
				{
                    _base.Record.Concepto = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual decimal Cantidad
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

                if (_base.Record.Cantidad.Equals(value))
				{
                    _base.Record.Cantidad = value;
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
	
		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual LineaInventario CloneAsNew()
		{
			LineaInventario clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = LineaInventario.OpenSession();
			LineaInventario.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.STOCK);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected LineaInventario () {}
		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private LineaInventario(LineaInventario source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
        private LineaInventario(IDataReader source, bool retrieve_childs)
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
		public static LineaInventario NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<LineaInventario>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">LineaInventario con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(LineaInventario source, bool retrieve_childs)
		/// <remarks/>
		internal static LineaInventario GetChild(LineaInventario source)
		{
			return new LineaInventario(source, false);
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">LineaInventario con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static LineaInventario GetChild(LineaInventario source, bool retrieve_childs)
		{
			return new LineaInventario(source, retrieve_childs);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="reader">DataReader con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool retrieve_childs)
		/// <remarks/>
        internal static LineaInventario GetChild(IDataReader source)
        {
            return new LineaInventario(source, false);
        }
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static LineaInventario GetChild(IDataReader source, bool retrieve_childs)
        {
            return new LineaInventario(source, retrieve_childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual LineaInventarioInfo GetInfo()
		{
			return GetInfo(true);
		}
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual LineaInventarioInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LineaInventarioInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static LineaInventario New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<LineaInventario>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static LineaInventario Get(long oid)
		{
			return Get(oid, true);
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto con los valores del registro</returns>
		public static LineaInventario Get(long oid, bool retrieve_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = LineaInventario.GetCriteria(LineaInventario.OpenSession());
			criteria.Childs = retrieve_childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LineaInventario.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
			
			LineaInventario.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<LineaInventario>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los LineaInventario. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = LineaInventario.OpenSession();
			ISession sess = LineaInventario.Session(sessCode);
			ITransaction trans = LineaInventario.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from LineaInventarioRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				LineaInventario.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override LineaInventario Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
			{
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			}

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
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private LineaInventario(InventarioAlmacen parent)
        {
            OidInventario = parent.Oid;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static LineaInventario NewChild(InventarioAlmacen parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new LineaInventario(parent);
		}

        /// <summary>
        /// Crea un nuevo objeto hijo
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <returns>Nuevo objeto creado</returns>
        //internal static LineaInventario NewChild(InventarioAlmacen parent, LineaAlmacenInfo source)
        //{
        //    if (!CanAddObject())
        //        throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

        //    LineaInventario item = new LineaInventario(parent);
        //    item.Concepto = source.Concepto;
        //    item.OidLineaAlmacen = source.Oid;
        //    item.Cantidad = 0;

        //    return item;
        //}

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
			
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor
			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(LineaInventario source)
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
		internal void Insert(LineaInventarios parent)
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
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(LineaInventarios parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				LineaInventarioRecord obj = Session().Get<LineaInventarioRecord>(Oid);
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
		internal void DeleteSelf(LineaInventarios parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<LineaInventarioRecord>(Oid));
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
					LineaInventario.DoLOCK( Session());
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
					LineaInventarioRecord obj = Session().Get<LineaInventarioRecord>(Oid);
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
				Session().Delete((LineaInventarioRecord)(criterio.UniqueResult()));
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
		
		#region Child Data Access

		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(InventarioAlmacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidInventario = parent.Oid;	
			

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

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(InventarioAlmacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidInventario = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				LineaInventarioRecord obj = parent.Session().Get<LineaInventarioRecord>(Oid);
				obj.CopyValues(this._base.Record);
				parent.Session().Update(obj);

				
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(InventarioAlmacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<LineaInventarioRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}
		
		#endregion

        #region SQL


        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            query = SELECT_BASE(conditions) +
                    WHERE(conditions);

            //if (lockTable) query += " FOR UPDATE OF ST NOWAIT";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
			if (conditions == null) return string.Empty;

            string query;

            query = " WHERE (LI.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "' OR LI.\"FECHA\" ISNULL)";

            if (conditions.InventarioAlmacen != null) query += " AND LI.\"OID_INVENTARIO\" = " + conditions.InventarioAlmacen.Oid;

            return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string li = nHManager.Instance.GetSQLTable(typeof(LineaInventarioRecord));
            string query;

            query = " SELECT LI.*" +
                    " FROM " + li + " AS LI";
                    

            return query;
        }

        #endregion
    }
}

