using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx;
using NHibernate;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PrecioTrayectoRecord : RecordBase
    {
        #region Attributes

        private string _puerto_destino = string.Empty;
        private string _puerto_origen = string.Empty;
        private Decimal _precio;
        private long _oid_naviera;

        #endregion

        #region Properties
        public virtual string PuertoDestino { get { return _puerto_destino; } set { _puerto_destino = value; } }
        public virtual string PuertoOrigen { get { return _puerto_origen; } set { _puerto_origen = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
        public virtual long OidNaviera { get { return _oid_naviera; } set { _oid_naviera = value; } }

        #endregion

        #region Business Methods

        public PrecioTrayectoRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _puerto_destino = Format.DataReader.GetString(source, "PUERTO_DESTINO");
            _puerto_origen = Format.DataReader.GetString(source, "PUERTO_ORIGEN");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");
            _oid_naviera = Format.DataReader.GetInt64(source, "OID_NAVIERA");

        }

        public virtual void CopyValues(PrecioTrayectoRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _puerto_destino = source.PuertoDestino;
            _puerto_origen = source.PuertoOrigen;
            _precio = source.Precio;
            _oid_naviera = source.OidNaviera;
        }
        #endregion
    }

    [Serializable()]
    public class PrecioTrayectoBase
    {
        #region Attributes

        private PrecioTrayectoRecord _record = new PrecioTrayectoRecord();

        #endregion

        #region Properties

        public PrecioTrayectoRecord Record { get { return _record; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(PrecioTrayecto source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(PrecioTrayectoInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class PrecioTrayecto : BusinessBaseEx<PrecioTrayecto>
	{	
	    #region Business Methods

        public PrecioTrayectoBase _base = new PrecioTrayectoBase();

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
					
		public virtual string PuertoOrigen
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PuertoOrigen;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.PuertoOrigen.Equals(value))
				{
					_base.Record.PuertoOrigen = value;
					PropertyHasChanged();
				}
			}
		}
		
					
		public virtual string PuertoDestino
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PuertoDestino;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.PuertoDestino.Equals(value))
				{
					_base.Record.PuertoDestino = value;
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
		
					
		public virtual long OidNaviera
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidNaviera;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidNaviera.Equals(value))
				{
					_base.Record.OidNaviera = value;
					PropertyHasChanged();
				}
			}
		}
				
			
		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		//Descomentar en caso de existir reglas de validación
		/*protected override void AddBusinessRules()
        {	
			//Agregar reglas de validación
        }*/
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return Transporter.CanAddObject();
		}
		
		public static bool CanGetObject()
		{
			return Transporter.CanGetObject();
		}
		
		public static bool CanDeleteObject()
		{
			return Transporter.CanDeleteObject();
		}
		
		public static bool CanEditObject()
		{
			return Transporter.CanEditObject();
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public PrecioTrayecto() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private PrecioTrayecto(PrecioTrayecto source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private PrecioTrayecto(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static PrecioTrayecto NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioTrayecto();
		}
		
		public static PrecioTrayecto NewChild(Naviera parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PrecioTrayecto obj = new PrecioTrayecto();
			obj.OidNaviera = parent.Oid;
			
			return obj;
		}
		
		
		internal static PrecioTrayecto GetChild(PrecioTrayecto source)
		{
			return new PrecioTrayecto(source);
		}
		
		internal static PrecioTrayecto GetChild(IDataReader reader)
		{
			return new PrecioTrayecto(reader);
		}
		
		public virtual PrecioTrayectoInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioTrayectoInfo(this);
		}
			
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override PrecioTrayecto Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(PrecioTrayecto source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Naviera parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			this.OidNaviera = parent.Oid;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Naviera parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			this.OidNaviera = parent.Oid; 
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				PrecioTrayectoRecord obj = Session().Get<PrecioTrayectoRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Naviera parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PrecioTrayectoRecord>(Oid));
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

		//public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT PO.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string pu = nHManager.Instance.GetSQLTable(typeof(PrecioTrayectoRecord));

			string query;

			query = @"
				FROM " + pu + @" AS PO";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "PO", ForeignFields());

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "PO");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "PO");

			if (conditions.Naviera != null)
				query += @"
					AND PO.""OID_NAVIERA"" = " + conditions.Naviera.Oid;

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
				query += ORDER(conditions.Orders, "PO", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("PO", lockTable);

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

		/*internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Municipio = MunicipioInfo.New(oid) }, lockTable);
		}*/

		#endregion	
	
	}
}

