using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using moleQule;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class PuertoRecord : RecordBase
	{
		#region Attributes

		private string _valor = string.Empty;
		private Decimal _precio;

		#endregion

		#region Properties

		public virtual string Valor { get { return _valor; } set { _valor = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }

		#endregion

		#region Business Methods

		public PuertoRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");

		}
		public virtual void CopyValues(PuertoRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_valor = source.Valor;
			_precio = source.Precio;
		}

		#endregion
	}

	[Serializable()]
	public class PuertoBase
	{

		#region Attributes

		private PuertoRecord _record = new PuertoRecord();

		#endregion

		#region Properties

		public PuertoRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(Puerto source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(PuertoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

    /// <summary>
    /// Tabla auxiliar con hijos
    /// </summary>
    [Serializable()]
    public class Puerto : BusinessBaseEx<Puerto>
    {
		#region Attributes

		protected PuertoBase _base = new PuertoBase();

		private PuertoDespachantes _puerto_despachantes = PuertoDespachantes.NewChildList();

		#endregion

		#region Properties

		public PuertoBase Base { get { return _base; } }

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
		public virtual string Valor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Valor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Valor.Equals(value))
				{
					_base.Record.Valor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Precio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Precio.Equals(value))
				{
					_base.Record.Precio = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual PuertoDespachantes PuertoDespachantes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _puerto_despachantes;
            }

            set
            {
                _puerto_despachantes = value;
            }

        }

        //Para añadir una lista: && _lista.IsValid
        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _puerto_despachantes.IsValid;
            }
        }
        //Para agregar una lista: || _lista.IsDirty
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _puerto_despachantes.IsDirty;
            }
        }

		#endregion

		#region Business Methods

		/// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual Puerto CloneAsNew()
        {
            Puerto clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.SessionCode = Puerto.OpenSession();
            Puerto.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.PuertoDespachantes.MarkAsNew();

            return clon;
        }
        
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, "Valor");
        }

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);

        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);

        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);

        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);

        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public Puerto()
        {
            MarkAsChild();
            Random r = new Random();
            Oid = (long)r.Next();
            //Rellenar si hay más campos que deban ser inicializados aquí
        }

        private Puerto(Puerto source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private Puerto(IDataReader reader)
        {
            MarkAsChild();
            Fetch(reader);
        }

        //Por cada padre que tenga la clase
        public static Puerto NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new Puerto();
        }


        internal static Puerto GetChild(Puerto source)
        {
            return new Puerto(source);
        }
        internal static Puerto GetChild(IDataReader reader)
        {
            return new Puerto(reader);
        }

        public virtual PuertoInfo GetInfo(bool get_childs = true)
        {
				return new PuertoInfo(this, get_childs);
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
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override Puerto Save()
        {
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }
		
        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Random r = new Random();
            Oid = (long)r.Next();
            _puerto_despachantes = Store.PuertoDespachantes.NewChildList();
        }

        #endregion

        #region Child Data Access

        private void Fetch(Puerto source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

				IDataReader reader;
				string query = string.Empty;

				PuertoDespachante.DoLOCK(Session());
				query = PuertoDespachantes.SELECT(this);
				reader = nHMng.SQLNativeSelect(query, Session());
				_puerto_despachantes = PuertoDespachantes.GetChildList(reader);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
            MarkOld();
        }

        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
					IDataReader reader;
					string query = string.Empty;

					PuertoDespachante.DoLOCK(Session());
					query = PuertoDespachantes.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_puerto_despachantes = PuertoDespachantes.GetChildList(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }
        
        internal void Insert(Puertos parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
                _puerto_despachantes.Update(this);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Update(Puertos parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;


            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                PuertoRecord obj = Session().Get<PuertoRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);

                _puerto_despachantes.Update(this);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(Puertos parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<PuertoRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT PU.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string pu = nHManager.Instance.GetSQLTable(typeof(PuertoRecord));

			string query;

			query = @"
				FROM " + pu + @" AS PU";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "PU", ForeignFields());

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "PU");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "PU");

			if (conditions.Puerto != null)
				query += @"
					AND PU.""OID"" = " + conditions.Puerto.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "PU", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("PU", lockTable);

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
			return SELECT(new QueryConditions { Puerto = PuertoInfo.New(oid) }, lockTable);
		}

		#endregion

    }
}

