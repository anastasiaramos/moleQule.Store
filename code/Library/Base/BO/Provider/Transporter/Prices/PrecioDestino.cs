using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using NHibernate;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PrecioDestinoRecord : RecordBase
    {
        #region Attributes

        private long _oid_transportista;
        private long _oid_cliente;
        private long _numero_cliente;
        private string _codigo_cliente = string.Empty;
        private string _nombre_cliente = string.Empty;
        private string _puerto = string.Empty;
        private Decimal _precio;

        #endregion

        #region Properties

        public virtual long OidTransportista { get { return _oid_transportista; } set { _oid_transportista = value; } }
        public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
        public virtual long NumeroCliente { get { return _numero_cliente; } set { _numero_cliente = value; } }
        public virtual string CodigoCliente { get { return _codigo_cliente; } set { _codigo_cliente = value; } }
        public virtual string NombreCliente { get { return _nombre_cliente; } set { _nombre_cliente = value; } }
        public virtual string Puerto { get { return _puerto; } set { _puerto = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }

        #endregion

        #region Business Methods

        public PrecioDestinoRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_transportista = Format.DataReader.GetInt64(source, "OID_TRANSPORTISTA");
            _oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
            _numero_cliente = Format.DataReader.GetInt64(source, "NUMERO_CLIENTE");
            _codigo_cliente = Format.DataReader.GetString(source, "CODIGO_CLIENTE");
            _nombre_cliente = Format.DataReader.GetString(source, "NOMBRE_CLIENTE");
            _puerto = Format.DataReader.GetString(source, "PUERTO");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");

        }

        public virtual void CopyValues(PrecioDestinoRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_transportista = source.OidTransportista;
            _oid_cliente = source.OidCliente;
            _numero_cliente = source.NumeroCliente;
            _codigo_cliente = source.CodigoCliente;
            _nombre_cliente = source.NombreCliente;
            _puerto = source.Puerto;
            _precio = source.Precio;
        }
        #endregion
    }

    [Serializable()]
    public class PrecioDestinoBase
    {
        #region Attributes

        private PrecioDestinoRecord _record = new PrecioDestinoRecord();

        #endregion

        #region Properties

        public PrecioDestinoRecord Record { get { return _record; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(PrecioDestino source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(PrecioDestinoInfo source)
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
	public class PrecioDestino : BusinessBaseEx<PrecioDestino>
    {
        #region Attributes

        public PrecioDestinoBase _base = new PrecioDestinoBase();

        #endregion

        #region Properties

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
        public virtual long OidCliente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCliente;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidCliente.Equals(value))
                {
                    _base.Record.OidCliente = value;
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

		public virtual string NumeroCliente { get { return _base.Record.NumeroCliente.ToString(); } set { _base.Record.NumeroCliente = Convert.ToInt64(value); } }
		public virtual string CodigoCliente { get { return _base.Record.CodigoCliente; } set { _base.Record.CodigoCliente = value; } }
		public virtual string NombreCliente { get { return _base.Record.NombreCliente; } set { _base.Record.NombreCliente = value; } }
		public virtual string NClienteLabel { get { return _base.Record.NumeroCliente.ToString(); } } /*DEPRECATED*/

        #endregion 

        #region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _base.CopyValues(source);

			_base.Record.NumeroCliente = Format.DataReader.GetInt64(source, "ID_CLIENTE");
			_base.Record.CodigoCliente = Format.DataReader.GetString(source, "VAT_NUMBER_CLIENTE");
			_base.Record.NombreCliente = Format.DataReader.GetString(source, "NOMBRE_CLIENTE");
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
		public PrecioDestino() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private PrecioDestino(PrecioDestino source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private PrecioDestino(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static PrecioDestino NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioDestino();
		}		
		public static PrecioDestino NewChild(Transporter parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PrecioDestino obj = new PrecioDestino();
			obj.OidTransportista = parent.Oid;
			
			return obj;
		}		
		
		internal static PrecioDestino GetChild(PrecioDestino source)
		{
			return new PrecioDestino(source);
		}
		
		internal static PrecioDestino GetChild(int sessionCode, IDataReader reader)
		{
			return new PrecioDestino(sessionCode, reader);
		}
		
		public virtual PrecioDestinoInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PrecioDestinoInfo(this);
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
		public override PrecioDestino Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(PrecioDestino source)
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
				PrecioDestinoRecord obj = Session().Get<PrecioDestinoRecord>(Oid);
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
				Session().Delete(Session().Get<PrecioDestinoRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion	

		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT PD.*" +
				   "        ,CL.\"NOMBRE\" AS \"NOMBRE_CLIENTE\"" +
				   "        ,CL.\"CODIGO\" AS \"ID_CLIENTE\"" +
				   "        ,CL.\"VAT_NUMBER\" AS \"VAT_NUMBER_CLIENTE\"";

			return query;
		}

		internal static string WHERE(Library.Store.QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query += " WHERE TRUE";

			if (conditions.PrecioDestino != null) query += " AND PD.\"OID\" = " + conditions.PrecioDestino.Oid;
			if (conditions.Acreedor != null) query += " AND PD.\"OID_TRANSPORTISTA\" = " + conditions.Acreedor.Oid;

            if (conditions.Client != null)
            {
                query += @"
                    AND PD.""OID_CLIENTE"" = " + conditions.Client.Oid;
            }

			return query;
		}

		internal static string SELECT(Library.Store.QueryConditions conditions, bool lockTable)
		{
			string pd = nHManager.Instance.GetSQLTable(typeof(PrecioDestinoRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ClientRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + pd + " AS PD" +
					" LEFT JOIN " + cl + " AS CL ON CL.\"OID\" = PD.\"OID_CLIENTE\"";

			query += WHERE(conditions);

			if (lockTable) query += " FOR UPDATE OF PD NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query;

			QueryConditions conditions = new QueryConditions { PrecioDestino = PrecioDestino.NewChild().GetInfo() };
			conditions.PrecioDestino.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

		#endregion
	}
}