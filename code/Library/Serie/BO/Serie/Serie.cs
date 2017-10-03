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
using moleQule.CslaEx; 
using moleQule;
using moleQule.Common;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Serie
{
	[Serializable()]
	public class SerieBase
	{
		#region Attributes

		private SerieRecord _record = new SerieRecord();

		private string _impuesto = string.Empty;
		private decimal _p_impuesto;

		#endregion

		#region Properties

		public SerieRecord Record { get { return _record; } }

		public virtual ETipoSerie ETipoSerie { get { return (ETipoSerie)_record.Tipo; } set { _record.Tipo = (long)value; } }
		public virtual string TipoSerieLabel { get { return moleQule.Store.Structs.EnumText<ETipoSerie>.GetLabel(ETipoSerie); } }
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
		internal void CopyValues(Serie source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;
		}
		internal void CopyValues(SerieInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Serie : BusinessBaseEx<Serie>
	{	 
		#region Attributes

		protected SerieBase _base = new SerieBase();

        private SerieFamilias _serie_familias = SerieFamilias.NewChildList();

		#endregion
		
		#region Properties

		public SerieBase Base { get { return _base; } }

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
		public virtual string Identificador
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Identificador;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Identificador.Equals(value))
				{
					_base.Record.Identificador = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Tipo
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
		public virtual string Cabecera
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Cabecera;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Cabecera.Equals(value))
				{
					_base.Record.Cabecera = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Resumen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Resumen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Resumen.Equals(value))
				{
					_base.Record.Resumen = value;
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

        public virtual SerieFamilias SerieFamilias
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _serie_familias;
            }
        }

		public virtual ETipoSerie ETipoSerie { get { return _base.ETipoSerie; } set { Tipo = (long)value; } }
		public virtual string TipoSerieLabel { get { return _base.TipoSerieLabel; } }
		public virtual string Impuesto { get { return _base.Impuesto; } }
        public virtual decimal PImpuesto { get { return _base.PImpuesto; } }

		public override bool IsValid
		{
			get { return base.IsValid && _serie_familias.IsValid; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty || _serie_familias.IsDirty; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual Serie CloneAsNew()
		{
			Serie clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = Serie.OpenSession();
			Serie.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
            clon.SerieFamilias.MarkAsNew();

			return clon;
		}

		protected virtual void CopyFrom(SerieInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			_base.Record.Nombre = source.Nombre;
			_base.Record.Identificador = source.Identificador;
			_base.Record.Observaciones = source.Observaciones;
			_base.Record.Resumen = source.Resumen;
			_base.Record.Cabecera = source.Cabecera;
			//_tipo_serie = source.TipoSerie;
			_base.Record.Tipo = source.Tipo;
			_base.Record.OidImpuesto = source.OidImpuesto;

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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.SERIE);
		}
		
		public static bool CanGetObject()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanGetObject(Resources.SecureItems.SERIE);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.SERIE);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.SERIE);
		}

		public static void IsPosibleDelete(long oid)
		{
            //QueryConditions conditions = new QueryConditions
            //{
            //    Serie = SerieInfo.New(oid),
            //    Estado = EEstado.NoAnulado,
            //};
            //InputDeliveryList in_invoices = InputDeliveryList.GetList(conditions, false);

            //if (in_invoices.Count > 0)
            //    throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);

            //Library.Invoice.QueryConditions inv_conditions = new Library.Invoice.QueryConditions
            //{
            //    Serie = SerieInfo.New(oid),
            //    Estado = EEstado.NoAnulado,
            //};

            //OutputDeliveryList out_invoices = OutputDeliveryList.GetList(inv_conditions, false);

            //if (out_invoices.Count > 0)
            //    throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);

            //BudgetList proformas = BudgetList.GetList(inv_conditions, false);

            //if (proformas.Count > 0)
            //	throw new iQException(Library.Invoice.Resources.Messages.PROFORMAS_ASOCIADAS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Serie () {}		
		private Serie(Serie source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Serie(IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Serie NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
                    moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Serie>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Serie con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Serie source, bool retrieve_childs)
		/// <remarks/>
		internal static Serie GetChild(Serie source)
		{
			return new Serie(source, false);
		}
		internal static Serie GetChild(Serie source, bool childs)
		{
			return new Serie(source, childs);
		}
        internal static Serie GetChild(IDataReader source)
        {
            return new Serie(source, false);
        }
        internal static Serie GetChild(IDataReader source, bool childs)
        {
            return new Serie(source, childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual SerieInfo GetInfo (bool childs = false) { return new SerieInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static Serie New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Serie>(new CriteriaCs(-1));
		}

		public static Serie Get(string query, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return Get(query, childs, -1);
		}

		public static Serie Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
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
		/// Elimina todos los Serie. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Serie.OpenSession();
			ISession sess = Serie.Session(sessCode);
			ITransaction trans = Serie.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from SerieRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Serie.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Serie Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
                throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(                    moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(                    moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
			{
				throw new System.Security.SecurityException(                moleQule.Resources.Messages.USER_NOT_ALLOWED);
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

                _serie_familias.Update(this);

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
				if (!SharedTransaction) BeginTransaction();
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
            Random r = new Random();
            Oid = (long)r.Next();

            //GetNewCode();

            _serie_familias = SerieFamilias.NewChildList();				
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Serie source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    string query = string.Empty;

                    SerieFamilia.DoLOCK(Session());
                    query = SerieFamilias.SELECT(this);
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _serie_familias = SerieFamilias.GetChildList(SessionCode, reader, Childs);
                }				
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

                if (Childs)
                {
                    string query = string.Empty;

                    if (Childs)
                    {
                        SerieFamilia.DoLOCK(Session());
                        query = SerieFamilias.SELECT(this);
                        IDataReader reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _serie_familias = SerieFamilias.GetChildList(SessionCode, reader, Childs);
                    }				
                }                
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
		internal void Insert(Series parent)
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
		internal void Update(Series parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				SerieRecord obj = Session().Get<SerieRecord>(Oid);
				obj.CopyValues(Base.Record);
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
		internal void DeleteSelf(Series parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<SerieRecord>(Oid));
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
					Serie.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        SerieFamilia.DoLOCK(Session());
                        query = SerieFamilias.SELECT(this);
                         reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _serie_familias = SerieFamilias.GetChildList(SessionCode, reader, Childs);
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
					SerieRecord obj = Session().Get<SerieRecord>(Oid);
					obj.CopyValues(Base.Record);
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
				Session().Delete((SerieRecord)(criterio.UniqueResult()));
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

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() {};
		}

        public new static string SELECT(long oid) { return SELECT(oid, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT S.*
						,IP.""NOMBRE"" AS ""IMPUESTO""
						,IP.""PORCENTAJE"" AS ""P_IMPUESTO""";

            return query;
        }

		internal static string INNER(QueryConditions conditions)
		{
			string s = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
			string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

			string query = @"
				FROM " + s + @" AS S
				LEFT JOIN " + ip + @" AS IP ON IP.""OID"" = S.""OID_IMPUESTO""";

			return query += " " + conditions.ExtraJoin;			
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "S", ForeignFields());

			query += moleQule.Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "S");

			if (conditions.Serie != null) 
				query += @"
					AND S.""OID"" = " + conditions.Serie.Oid;

			switch (conditions.SerieType)
			{
				case ETipoSerie.Todas: break;

				case ETipoSerie.Compra:
				case ETipoSerie.Venta:
					query += @"
						AND (S.""TIPO"" IN (" + (long)conditions.SerieType + @"," + (long)ETipoSerie.CompraVenta + "))";
					break;

				default:
					query += @"
						AND S.""TIPO"" = " + (long)conditions.SerieType;
					break;
			}

			return query += " " + conditions.ExtraWhere;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query = 
			SELECT_FIELDS() +
			INNER(conditions) +
			WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "S", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += moleQule.Common.EntityBase.LOCK("S", lockTable);

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
			INNER(conditions) +
			WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Serie = SerieInfo.New(oid) }, lockTable);
		}

		#endregion
	}
}

