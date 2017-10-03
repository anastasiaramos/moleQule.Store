using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PrecioOrigenRecord : RecordBase
    {
        #region Attributes

        private long _oid_transportista;
        private long _oid_proveedor;
        private string _proveedor = string.Empty;
        private string _puerto = string.Empty;
        private Decimal _precio;

        #endregion

        #region Properties
        public virtual long OidTransportista { get { return _oid_transportista; } set { _oid_transportista = value; } }
        public virtual long OidProveedor { get { return _oid_proveedor; } set { _oid_proveedor = value; } }
        public virtual string Proveedor { get { return _proveedor; } set { _proveedor = value; } }
        public virtual string Puerto { get { return _puerto; } set { _puerto = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }

        #endregion

        #region Business Methods

        public PrecioOrigenRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_transportista = Format.DataReader.GetInt64(source, "OID_TRANSPORTISTA");
            _oid_proveedor = Format.DataReader.GetInt64(source, "OID_PROVEEDOR");
            _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
            _puerto = Format.DataReader.GetString(source, "PUERTO");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");

        }

        public virtual void CopyValues(PrecioOrigenRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_transportista = source.OidTransportista;
            _oid_proveedor = source.OidProveedor;
            _proveedor = source.Proveedor;
            _puerto = source.Puerto;
            _precio = source.Precio;
        }
        #endregion
    }

    [Serializable()]
    public class PrecioOrigenBase
    {
        #region Attributes

        private PrecioOrigenRecord _record = new PrecioOrigenRecord();

        #endregion

        #region Properties

        public PrecioOrigenRecord Record { get { return _record; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(PrecioOrigen source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(PrecioOrigenInfo source)
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
	public class PrecioOrigen : BusinessBaseEx<PrecioOrigen>
	{
	
	    #region Business Methods

        public PrecioOrigenBase _base = new PrecioOrigenBase();

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
					
		public virtual long OidTransportista
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTransportista;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidTransportista.Equals(value))
				{
					_base.Record.OidTransportista = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual long OidProveedor
        {

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidProveedor;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidProveedor.Equals(value))
                {
                    _base.Record.OidProveedor = value;
                    PropertyHasChanged();
                }
            }
        }
		
					
		public virtual string Proveedor
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Proveedor;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Proveedor.Equals(value))
				{
					_base.Record.Proveedor = value;
					PropertyHasChanged();
				}
			}
		}
		
					
		public virtual string Puerto
		{
			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Puerto;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Puerto.Equals(value))
				{
					_base.Record.Puerto = value;
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
		public PrecioOrigen() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private PrecioOrigen(PrecioOrigen source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private PrecioOrigen(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static PrecioOrigen NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioOrigen();
		}		
		public static PrecioOrigen NewChild(Transporter parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PrecioOrigen obj = new PrecioOrigen();
			obj.OidTransportista = parent.Oid;
			
			return obj;
		}		
		
		internal static PrecioOrigen GetChild(PrecioOrigen source)
		{
			return new PrecioOrigen(source);
		}		
		internal static PrecioOrigen GetChild(int sessionCode, IDataReader reader)
		{
			return new PrecioOrigen(sessionCode, reader);
		}
		
		public virtual PrecioOrigenInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioOrigenInfo(this);
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
		public override PrecioOrigen Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(PrecioOrigen source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Transporter parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			this.OidTransportista = parent.Oid;
			
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

		internal void Update(Transporter parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			this.OidTransportista = parent.Oid; 
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				PrecioOrigenRecord obj = Session().Get<PrecioOrigenRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Transporter parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PrecioOrigenRecord>(Oid));
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
			string pu = nHManager.Instance.GetSQLTable(typeof(PrecioOrigenRecord));

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

			/*if (conditions.Municipio != null)
				query += @"
					AND PO.""OID"" = " + conditions.Municipio.Oid;*/

			if (conditions.Acreedor != null)
				query += @"
					AND PO.""OID_TRANSPORTISTA"" = " + conditions.Acreedor.Oid;

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

