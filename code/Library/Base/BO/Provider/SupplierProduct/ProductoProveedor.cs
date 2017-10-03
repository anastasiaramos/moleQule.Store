using System;
using System.ComponentModel;
using System.Data;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.Common.Structs; 
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ProductoProveedorRecord : RecordBase
    {
        #region Attributes

        private long _oid_acreedor;
        private long _oid_impuesto;
        private long _tipo_acreedor;
        private long _oid_producto;
        private Decimal _precio;
        private bool _facturacion_bulto = false;
        private string _codigo_producto_acreedor = string.Empty;
        private Decimal _p_descuento;
        private long _tipo_descuento;
        private bool _automatico = false;

        #endregion

        #region Properties

        public virtual long OidAcreedor { get { return _oid_acreedor; } set { _oid_acreedor = value; } }
        public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
        public virtual long TipoAcreedor { get { return _tipo_acreedor; } set { _tipo_acreedor = value; } }
        public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
        public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
        public virtual string CodigoProductoAcreedor { get { return _codigo_producto_acreedor; } set { _codigo_producto_acreedor = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual long TipoDescuento { get { return _tipo_descuento; } set { _tipo_descuento = value; } }
        public virtual bool Automatico { get { return _automatico; } set { _automatico = value; } }

        #endregion

        #region Business Methods

        public ProductoProveedorRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
            _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");
            _facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
            _codigo_producto_acreedor = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_ACREEDOR");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _tipo_descuento = Format.DataReader.GetInt64(source, "TIPO_DESCUENTO");
            _automatico = Format.DataReader.GetBool(source, "AUTOMATICO");

        }

        public virtual void CopyValues(ProductoProveedorRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_acreedor = source.OidAcreedor;
            _oid_impuesto = source.OidImpuesto;
            _tipo_acreedor = source.TipoAcreedor;
            _oid_producto = source.OidProducto;
            _precio = source.Precio;
            _facturacion_bulto = source.FacturacionBulto;
            _codigo_producto_acreedor = source.CodigoProductoAcreedor;
            _p_descuento = source.PDescuento;
            _tipo_descuento = source.TipoDescuento;
            _automatico = source.Automatico;
        }
        #endregion
    }

	[Serializable()]
	public class ProductoProveedorBase
	{
		#region Attributes

        private ProductoProveedorRecord _record = new ProductoProveedorRecord();

		// Campo no enlazados
		internal string _producto;
		internal string _acreedor;
		internal long _oid_familia;
		internal string _familia;
		internal Decimal _precio_compra;
		internal Decimal _precio_venta;
		internal Decimal _ayuda;
		internal string _observaciones;
		internal bool _bulto;
		internal Decimal _p_impuestos;
		internal string _impuesto;

		#endregion

		#region Properties

        public ProductoProveedorRecord Record { get { return _record; } set { _record = value; } }

		internal ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } }
        internal string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		internal ETipoDescuento ETipoDescuento { get { return (ETipoDescuento)_record.TipoDescuento; } set { _record.TipoDescuento = (long)value; } }
		internal string TipoDescuentoLabel { get { return moleQule.Common.Structs.EnumText<ETipoDescuento>.GetLabel(ETipoDescuento); } }
		internal bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		internal ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_p_impuestos = Format.DataReader.GetDecimal(source, "P_IMPUESTOS");
			_impuesto = Format.DataReader.GetString(source, "IMPUESTO");
			_producto = Format.DataReader.GetString(source, "PRODUCTO");
			_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");
			_familia = Format.DataReader.GetString(source, "FAMILIA");
			_precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
			_precio_venta = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
			_ayuda = Format.DataReader.GetDecimal(source, "AYUDA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_bulto = Format.DataReader.GetBool(source, "BULTO");
		}
		internal void CopyValues(ProductoProveedor source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_p_impuestos = source.PImpuestos;
			_oid_familia = source.OidFamilia;
			_familia = source.Familia;
			_producto = source.Producto;
			_precio_venta = source.PrecioVenta;
			_ayuda = source.Ayuda;
		}
		internal void CopyValues(ProductoProveedorInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_p_impuestos = source.PImpuestos;
			_oid_familia = source.OidFamilia;
			_familia = source.Familia;
			_producto = source.Producto;
			_precio_venta = source.PrecioVenta;
			_ayuda = source.Ayuda;
		}

		internal static Decimal GetPrecioProveedor(ProductoProveedorInfo productoProveedor, BatchInfo partida, ProductInfo producto, ETipoFacturacion tipo)
		{
			Decimal precio = 0;
			ETipoFacturacion tipoFacturacion = tipo;

			if (productoProveedor != null)
			{
				precio = (productoProveedor.ETipoDescuento == ETipoDescuento.Precio) ? productoProveedor.Precio : producto.PrecioCompra;
				tipoFacturacion = productoProveedor.ETipoFacturacion;
			}
			else
			{
				precio = producto.PrecioCompra;
				tipoFacturacion = producto.ETipoFacturacion;
			}

			Decimal kilosBulto = (partida != null) ? partida.KilosPorBulto : producto.KilosBulto;
			kilosBulto = (kilosBulto == 0) ? 1 : kilosBulto;

			switch (tipo)
			{
				case ETipoFacturacion.Peso:

					if (tipoFacturacion != ETipoFacturacion.Peso)
						precio = precio * kilosBulto;

					break;

				default:

					if (tipoFacturacion == ETipoFacturacion.Peso)
						precio = precio / kilosBulto;

					break;
			}

			return Decimal.Round(precio, 2);
		}

		internal static Decimal GetDescuentoProveedor(ProductoProveedorInfo productoProveedor, Decimal pDescuento)
		{
			Decimal p_descuento = pDescuento;

			if (productoProveedor != null)
				p_descuento = (productoProveedor.ETipoDescuento == ETipoDescuento.Porcentaje) ? productoProveedor.PDescuento : pDescuento;

			return Decimal.Round(p_descuento, 2);
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class ProductoProveedor : BusinessBaseEx<ProductoProveedor>
	{
	
	    #region Attributes

		public ProductoProveedorBase _base = new ProductoProveedorBase();

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
        
        public virtual long OidAcreedor
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAcreedor;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidAcreedor.Equals(value))
				{
					_base.Record.OidAcreedor = value;
					PropertyHasChanged();
				}
			}
		}		
		public virtual long OidProducto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidProducto;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidProducto.Equals(value))
				{
					_base.Record.OidProducto = value;
					PropertyHasChanged();
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
		public virtual long TipoAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoAcreedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.TipoAcreedor.Equals(value))
				{
					_base.Record.TipoAcreedor = value;
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
		public virtual Decimal PDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return Decimal.Round(_base.Record.PDescuento, 2);
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.PDescuento.Equals(value))
				{
					_base.Record.PDescuento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoDescuento;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.TipoDescuento.Equals(value))
				{
					_base.Record.TipoDescuento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodigoArticuloAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoProductoAcreedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoProductoAcreedor.Equals(value))
				{
					_base.Record.CodigoProductoAcreedor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool FacturacionBulto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FacturacionBulto;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.FacturacionBulto.Equals(value))
				{
					_base.Record.FacturacionBulto = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual bool Automatico
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Automatico;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Automatico.Equals(value))
                {
                    _base.Record.Automatico = value;
                    PropertyHasChanged();
                }
            }
        }

        // Campo no enlazado
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { TipoAcreedor = (long)value; } }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } set { TipoDescuento = (long)value; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual Decimal Ayuda { get { return _base._ayuda; } set { _base._ayuda = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
		public virtual long OidFamilia { get { return _base._oid_familia; } set { _base._oid_familia = value; } }
		public virtual string Familia { get { return _base._familia; } set { _base._familia = value; } }
		public virtual Decimal PrecioVenta { get { return _base._precio_venta; } set { _base._precio_venta = value; } }
		public virtual string Observaciones { get { return _base._observaciones; } }
		public virtual bool Bulto { get { return _base._bulto; } }
		public virtual string Impuesto { get { return _base._impuesto; } set { _base._impuesto = value; } }
		public virtual Decimal PImpuestos { get { return _base._p_impuestos; } set { _base._p_impuestos = value; } }

		#endregion

		#region Business Methods
        
		public virtual void CopyFrom(IAcreedor acreedor, ProductInfo producto)
		{
			if (acreedor == null) return;
			if (producto == null) return;

			OidAcreedor = acreedor.Oid;
			OidImpuesto = producto.OidImpuestoCompra;
			ETipoAcreedor = acreedor.ETipoAcreedor;
			OidProducto = producto.Oid;
			Precio = producto.PrecioCompra;
			Producto = producto.Nombre;

			Impuesto = producto.ImpuestoCompra;
			PImpuestos = producto.PImpuestoCompra;
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PROVEEDOR);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PROVEEDOR);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PROVEEDOR);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PROVEEDOR);
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public ProductoProveedor() 
		{ 
			MarkAsChild();
			Oid = (long)(new Random().Next());
			ETipoDescuento = Store.ModulePrincipal.GetDefaultTipoDescuentoSetting();
		}			
		private ProductoProveedor(ProductoProveedor source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private ProductoProveedor(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static ProductoProveedor NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new ProductoProveedor();
		}		
		public static ProductoProveedor NewChild(Product parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			ProductoProveedor obj = new ProductoProveedor();
			obj.OidProducto = parent.Oid;
			
			return obj;
		}
		public static ProductoProveedor NewChild(IAcreedor acreedor, ProductInfo producto)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			ProductoProveedor obj = new ProductoProveedor();
			obj.CopyFrom(acreedor, producto);

			return obj;
		}

        /// <summary>
        /// Crea un nuevo ProductoProveedor a partir de un ProductoInfo
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        // Creado para que no tengamos que acceder a la base de datos continuamente,
        // para obtener el Oid del producto.
        public static ProductoProveedor NewChild(ProductInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            ProductoProveedor obj = new ProductoProveedor();
            obj.OidProducto = parent.Oid;

            return obj;
        }
		
		internal static ProductoProveedor GetChild(ProductoProveedor source)
		{
			return new ProductoProveedor(source);
		}
		
		internal static ProductoProveedor GetChild(int sessionCode, IDataReader reader)
		{
			return new ProductoProveedor(sessionCode, reader);
		}
		
		public virtual ProductoProveedorInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new ProductoProveedorInfo(this);
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
		public override ProductoProveedor Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
			
		#endregion

		#region Common Data Access

		internal void ExecuteQL(CriteriaEx criteria)
		{
			try
			{
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					ProductoProveedor.DoLOCK(Session());
					nHMng.SQLNativeExecute(criteria.Query, Session());
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

		#region Child Data Access

		private void Fetch(ProductoProveedor source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            this.OidProducto = parent.Oid;

			try
			{	
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			
			try
			{
				SessionCode = parent.SessionCode;
                ProductoProveedorRecord obj = Session().Get<ProductoProveedorRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<ProductoProveedorRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            this.OidAcreedor = parent.Oid;

			try
			{	
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
			
			try
			{
				SessionCode = parent.SessionCode;
                ProductoProveedorRecord obj = Session().Get<ProductoProveedorRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ProductoProveedorRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion

        #region SQL

		public static ProviderBaseInfo.SelectLocalCaller local_caller = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE);
        public static string SELECT(long oid, ETipoAcreedor tipo) { return SELECT(oid, tipo, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT PP.*" +
                    "       ,P.\"NOMBRE\" AS \"PRODUCTO\"" +
                    "       ,P.\"PRECIO_COMPRA\" AS \"PRECIO_COMPRA\"" +
                    "       ,P.\"PRECIO_VENTA\" AS \"PRECIO_VENTA\"" +
                    "       ,P.\"AYUDA_KILO\" AS \"AYUDA\"" +
                    "       ,P.\"OBSERVACIONES\" AS \"OBSERVACIONES\"" +
                    "       ,P.\"BULTO\" AS \"BULTO\"" +
                    "       ,A.\"NOMBRE\" AS \"ACREEDOR\"" +
					"       ,F.\"OID\" AS \"OID_FAMILIA\"" +
                    "       ,F.\"CODIGO\" AS \"FAMILIA\"" +
					"       ,IM.\"PORCENTAJE\" AS \"P_IMPUESTOS\"" +
					"       ,IM.\"NOMBRE\" AS \"IMPUESTO\"";

            return query;
        }

		internal static string INNER_ACREEDOR(ETipoAcreedor tipo)
		{
			string query;

			if (tipo != ETipoAcreedor.Todos)
				query = " INNER JOIN " + ProviderBaseInfo.TABLE(tipo) + " AS A ON A.\"OID\" = PP.\"OID_ACREEDOR\" AND PP.\"TIPO_ACREEDOR\" = " + Convert.ToInt32(tipo).ToString();
			else
                query = " LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Proveedor) + " AS A ON A.\"OID\" = PP.\"OID_ACREEDOR\" AND PP.\"TIPO_ACREEDOR\" = " + Convert.ToInt32(tipo).ToString();

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = " WHERE TRUE";

			if (conditions.ProductoProveedor != null)
				if (conditions.ProductoProveedor.Oid != 0) query += " AND PP.\"OID\" = " + conditions.ProductoProveedor.Oid.ToString();

			if (conditions.Producto != null) query += " AND PP.\"OID_PRODUCTO\" = " + conditions.Producto.Oid.ToString();
			if ((conditions.Acreedor != null) && (conditions.Acreedor.OidAcreedor != 0)) query += " AND PP.\"OID_ACREEDOR\" = " + conditions.Acreedor.OidAcreedor.ToString();
			if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos) query += " AND PP.\"TIPO_ACREEDOR\" = " + Convert.ToInt32(conditions.TipoAcreedor[0]).ToString();

			return query;
		}

        internal static string SELECT_BASE()
        {
            string pp = nHManager.Instance.GetSQLTable(typeof(ProductoProveedorRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            string fa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.FamilyRecord));
            string im = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

            string query;

			query = ProductoProveedor.SELECT_FIELDS() +
					" FROM " + pp + " AS PP" +
					" INNER JOIN " + pr + " AS P ON P.\"OID\" = PP.\"OID_PRODUCTO\"" +
					" INNER JOIN " + fa + " AS F ON F.\"OID\" = P.\"OID_FAMILIA\"" +
					" LEFT JOIN " + im + " AS IM ON IM.\"OID\" = PP.\"OID_IMPUESTO\"";

            return query;
        }

		internal static string SELECT_BASE(QueryConditions conditions, ETipoAcreedor tipo)
		{
			string query;

			query = SELECT_BASE() +
					INNER_ACREEDOR(tipo) +
					WHERE(conditions); 

			return query;
		}

		internal static string SELECT(long oid, ETipoAcreedor providerType, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				ProductoProveedor = ProductoProveedorInfo.New(oid)
			};

			query = SELECT(conditions, lockTable);

			return query;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller, conditions) +
                    " ORDER BY \"OID\"";//Para que respete el orden en el que se han añadido los productos
            //y los muestre en ese mismo orden
			else
			{
                query = SELECT_BASE(conditions, conditions.TipoAcreedor[0]) +
                    " ORDER BY \"OID\"";//Para que respete el orden en el que se han añadido los productos
                //y los muestre en ese mismo orden
				if (lockTable) query += " FOR UPDATE OF PP NOWAIT";
			}

            return query;
        }

		public static string UPDATE_TIPO(QueryConditions conditions)
		{
            string pp = nHManager.Instance.GetSQLTable(typeof(ProductoProveedorRecord));
			
            string query = @"
                UPDATE " + pp + @" AS PP SET ""TIPO_ACREEDOR"" = " + conditions.Acreedor.TipoAcreedor +
                WHERE(conditions);

			return query;
		}

        #endregion
    }
}

