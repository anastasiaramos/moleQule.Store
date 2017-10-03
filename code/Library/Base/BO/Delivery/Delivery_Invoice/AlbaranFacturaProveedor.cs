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
    public class InputDeliveryInvoiceRecord : RecordBase
    {
        #region Attributes

        private long _oid_albaran;
        private long _oid_factura;
        private DateTime _fecha_asignacion;

        #endregion

        #region Properties
        public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
        public virtual long OidFactura { get { return _oid_factura; } set { _oid_factura = value; } }
        public virtual DateTime FechaAsignacion { get { return _fecha_asignacion; } set { _fecha_asignacion = value; } }

        #endregion

        #region Business Methods

        public InputDeliveryInvoiceRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
            _oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
            _fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

        }

        public virtual void CopyValues(InputDeliveryInvoiceRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_albaran = source.OidAlbaran;
            _oid_factura = source.OidFactura;
            _fecha_asignacion = source.FechaAsignacion;
        }
        #endregion
    }

    [Serializable()]
	public class InputDeliveryInvoiceBase
    {
        #region Attributes

        private InputDeliveryInvoiceRecord _record = new InputDeliveryInvoiceRecord();

        //Datos INNER JOIN
        protected Decimal _importe;
        protected string _codigo_factura;
        protected string _codigo_albaran;

        #endregion

        #region Properties

        public InputDeliveryInvoiceRecord Record { get { return _record; } }

        // CAMPOS NO ENLAZADOS
        public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
        public virtual string CodigoFactura { get { return _codigo_factura; } set { _codigo_factura = value; } }
        public virtual string CodigoAlbaran { get { return _codigo_albaran; } set { _codigo_albaran = value; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _importe = Format.DataReader.GetDecimal(source, "IMPORTE");
            _codigo_factura = Format.DataReader.GetString(source, "CODIGO_FACTURA");
            _codigo_albaran = Format.DataReader.GetString(source, "CODIGO_ALBARAN");
        }

        internal void CopyValues(AlbaranFacturaProveedor source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            //INNER JOIN
            _importe = source.Importe;
            _codigo_factura = source.CodigoFactura;
            _codigo_albaran = source.CodigoAlbaran;
        }
        internal void CopyValues(AlbaranFacturaProveedorInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
            
            //INNER JOIN
            _importe = source.Importe;
            _codigo_factura = source.CodigoFactura;
            _codigo_albaran = source.CodigoAlbaran;
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class AlbaranFacturaProveedor : BusinessBaseEx<AlbaranFacturaProveedor>
	{	
	    #region Attributes

		public InputDeliveryInvoiceBase _base = new InputDeliveryInvoiceBase();
        
        #endregion

        #region Properties

		public InputDeliveryInvoiceBase Base { get { return _base; } }

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
		public virtual long OidAlbaran
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlbaran;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
                if (!_base.Record.OidAlbaran.Equals(value))
				{
                    _base.Record.OidAlbaran = value;
					PropertyHasChanged();
				}
			}
		}					
		public virtual long OidFactura
        {			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidFactura;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
                if (!_base.Record.OidFactura.Equals(value))
				{
                    _base.Record.OidFactura = value;
					PropertyHasChanged();
				}
			}
		}					
        public virtual DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } set { _base.Record.FechaAsignacion = value; } }

        // CAMPOS NO ENLAZADOS
        public virtual Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
        public virtual string CodigoFactura { get { return _base.CodigoFactura; } set { _base.CodigoFactura = value; } }
        public virtual string CodigoAlbaran { get { return _base.CodigoAlbaran; } set { _base.CodigoAlbaran = value; } }

        #endregion

        #region Business Methods
			
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public AlbaranFacturaProveedor() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private AlbaranFacturaProveedor(AlbaranFacturaProveedor source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private AlbaranFacturaProveedor(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static AlbaranFacturaProveedor NewChild()
		{
			if (!CanAddObject())
                throw new System.Security.SecurityException(
                    moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AlbaranFacturaProveedor();
		}
		
		public static AlbaranFacturaProveedor NewChild(InputInvoice factura, InputDeliveryInfo albaran)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
                    moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			AlbaranFacturaProveedor obj = new AlbaranFacturaProveedor();
			obj.OidFactura = factura.Oid;
            obj.OidAlbaran = albaran.Oid;
            obj.CodigoFactura = factura.Codigo;
            obj.CodigoAlbaran = albaran.Codigo;
            obj.FechaAsignacion = DateTime.Today;
			
			return obj;
		}

		internal static AlbaranFacturaProveedor GetChild(AlbaranFacturaProveedor source)
		{
			return new AlbaranFacturaProveedor(source);
		}
		
		internal static AlbaranFacturaProveedor GetChild(IDataReader reader)
		{
			return new AlbaranFacturaProveedor(reader);
		}

        public virtual AlbaranFacturaProveedorInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
                    moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new AlbaranFacturaProveedorInfo(this, false);
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
		public override AlbaranFacturaProveedor Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(AlbaranFacturaProveedor source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(InputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidAlbaran = parent.Oid;

			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void Update(InputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            this.OidAlbaran = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				InputDeliveryInvoiceRecord obj = Session().Get<InputDeliveryInvoiceRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void DeleteSelf(InputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<InputDeliveryInvoiceRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(InputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidFactura = parent.Oid;

			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void Update(InputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
			
			try
			{
				SessionCode = parent.SessionCode;
				InputDeliveryInvoiceRecord obj = Session().Get<InputDeliveryInvoiceRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void DeleteSelf(InputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<InputDeliveryInvoiceRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion
	
	}
}

