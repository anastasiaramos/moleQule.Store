using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ClientProductBase
	{
		#region Attributes

		private ClientProductRecord _record = new ClientProductRecord();

		internal string _producto = string.Empty;
		internal long _oid_familia;
		internal long _oid_impuesto;
		internal decimal _p_impuesto;

		#endregion

		#region Properties

		public ClientProductRecord Record { get { return _record; } }

		public ETipoDescuento ETipoDescuento { get { return (ETipoDescuento)_record.TipoDescuento; } }
		public string TipoDescuentoLabel { get { return moleQule.Common.Structs.EnumText<ETipoDescuento>.GetLabel(ETipoDescuento); } }
		public bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		public ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
		public string Producto { get { return _producto; } set { _producto = value; } }
		public long OidFamilia { get { return _oid_familia; } set { _oid_familia = value; } }
		public long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public decimal PImpuesto { get { return _p_impuesto; } set { _p_impuesto = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_producto = Format.DataReader.GetString(source, "PRODUCTO");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_p_impuesto = Format.DataReader.GetDecimal(source, "P_IMPUESTO");
		}
		internal void CopyValues(ProductoCliente source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_producto = source.Producto;
			_oid_familia = source.OidFamilia;
			_oid_impuesto = source.OidImpuesto;
			_p_impuesto = source.PImpuesto;
		}
		internal void CopyValues(ProductoClienteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_producto = source.Producto;
			_oid_familia = source.OidFamilia;
			_oid_impuesto = source.OidImpuesto;
			_p_impuesto = source.PImpuesto;
		}

		internal static Decimal GetPrecioCliente(ProductoClienteInfo productoCliente, BatchInfo partida, ProductInfo producto, ETipoFacturacion tipo)
		{
			Decimal precio = 0;
			ETipoFacturacion tipoFacturacion = tipo;

			if (productoCliente != null)
			{
				precio = (productoCliente.ETipoDescuento == ETipoDescuento.Precio) ? productoCliente.Precio : producto.PrecioVenta;
				tipoFacturacion = productoCliente.ETipoFacturacion;
			}
			else
			{
				precio = producto.PrecioVenta;
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

			return Decimal.Round(precio, Common.ModulePrincipal.GetNDecimalesPreciosSetting());
		}

		internal static Decimal GetDescuentoCliente(ProductoClienteInfo productoCliente, Decimal pDescuento)
		{
			Decimal p_descuento = pDescuento;

			if (productoCliente != null)
				p_descuento = (productoCliente.ETipoDescuento == ETipoDescuento.Porcentaje) ? productoCliente.PDescuento : pDescuento;

			return Decimal.Round(p_descuento, 2);
		}

		#endregion
	}
	
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
    public class ProductoCliente : BusinessBaseEx<ProductoCliente>
	{	
	    #region Attributes

		protected ClientProductBase _base = new ClientProductBase();

        #endregion

        #region Properties

		public ClientProductBase Base { get { return _base; } }

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
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return Decimal.Round(_base.Record.Precio, 5);
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
		public virtual Decimal PrecioCompra
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return Decimal.Round(_base.Record.PrecioCompra, 5);
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrecioCompra.Equals(value))
				{
					_base.Record.PrecioCompra = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Facturar
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Facturar;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Facturar.Equals(value))
				{
					_base.Record.Facturar = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaValidez
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaValidez;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaValidez.Equals(value))
				{
					_base.Record.FechaValidez = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } set { TipoDescuento = (long)value; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual string Producto { get { return _base.Producto; } set { _base.Producto = value; PropertyHasChanged(); } }
		public virtual long OidFamilia { get { return _base.OidFamilia; } set { _base.OidFamilia = value; } }
		public virtual long OidImpuesto { get { return _base.OidImpuesto; } set { _base.OidImpuesto = value; } }
		public virtual decimal PImpuesto { get { return _base.PImpuesto; } set { _base.PImpuesto = value; } }

        #endregion

        #region Business Methods

        protected void CopyFrom(IThirdParty client, ProductInfo producto)
		{
			OidCliente = client.Oid;
			PDescuento = client.PDescuento;

			if (producto != null)
			{
				OidProducto = producto.Oid;
				Producto = producto.Nombre;
			}			
		}

		public static Decimal GetPrecioCliente(ProductoCliente productoCliente, ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo) 
		{ 
			return ClientProductBase.GetPrecioCliente(productoCliente.GetInfo(), partida, producto, tipo); 
		}
		public static Decimal GetDescuentoCliente(ProductoCliente productoCliente, Decimal pDescuento)
		{
			return ClientProductBase.GetDescuentoCliente(productoCliente.GetInfo(), pDescuento); 
		}

		#endregion
		 
	    #region Validation Rules

		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(moleQule.Invoice.Structs.Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(moleQule.Invoice.Structs.Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(moleQule.Invoice.Structs.Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CLIENTE);
		}
		 
		#endregion

		#region Common Factory Methods

		public virtual ProductoClienteInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new ProductoClienteInfo(this);
		}

		#endregion

		#region Root Factory Methods

		public static ProductoCliente Get(long oid)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = ProductoCliente.GetCriteria(ProductoCliente.OpenSession());

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ProductoCliente.SELECT(oid);

			ProductoCliente.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<ProductoCliente>(criteria);
		}

		#endregion

		#region Child Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public ProductoCliente() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
			ETipoDescuento = Store.ModulePrincipal.GetDefaultTipoDescuentoSetting();
		}			
		private ProductoCliente(ProductoCliente source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private ProductoCliente(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		public static ProductoCliente NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new ProductoCliente();
		}		
		public static ProductoCliente NewChild(IThirdParty parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			ProductoCliente obj = new ProductoCliente();
			obj.OidCliente = parent.Oid;
			
			return obj;
		}		
		public static ProductoCliente NewChild(Product parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			ProductoCliente obj = new ProductoCliente();
			obj.OidProducto = parent.Oid;
            obj.Producto = parent.Nombre;

            return obj;
		}
        public static ProductoCliente NewChild(IThirdParty cliente, ProductInfo producto)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			ProductoCliente obj = new ProductoCliente();
			obj.CopyFrom(cliente, producto);

			return obj;
		}
        public static ProductoCliente NewChild(ProductInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            ProductoCliente obj = new ProductoCliente();
            obj.OidProducto = parent.Oid;

            return obj;
        }
		
		internal static ProductoCliente GetChild(ProductoCliente source) { return new ProductoCliente(source); }		
		internal static ProductoCliente GetChild(int sessionCode, IDataReader reader) { return new ProductoCliente(sessionCode, reader); }
		
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
		
		public override ProductoCliente Save()
		{
			throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
	
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(ProductoCliente source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(IThirdParty parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                OidCliente = parent.Oid;
                parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void Update(IThirdParty parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                OidCliente = parent.Oid;

				SessionCode = parent.SessionCode;
				ClientProductRecord obj = Session().Get<ClientProductRecord>(Oid);
			    obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void DeleteSelf(IThirdParty parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ClientProductRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
            
            OidProducto = parent.Oid;

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

		internal void Update(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidProducto = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				ClientProductRecord obj = Session().Get<ClientProductRecord>(Oid);
				obj.CopyValues(Base.Record);
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
				Session().Delete(Session().Get<ClientProductRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion

        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, new QueryConditions(), true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT PC.*" +
                    "       ,PR.\"NOMBRE\" AS \"PRODUCTO\"" +
					"       ,PR.\"OID_FAMILIA\" AS \"OID_FAMILIA\"" +
					"		,IM.\"OID\" AS \"OID_IMPUESTO\"" +
					"		,IM.\"PORCENTAJE\" AS \"P_IMPUESTO\"";

            return query;
        }

        internal static string SELECT(long oid, QueryConditions conditions, bool lock_table)
        {
            string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
			string im = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + pc + " AS PC" +
                    " LEFT JOIN " + pr + " AS PR ON PR.\"OID\" = PC.\"OID_PRODUCTO\"" +
					" LEFT JOIN " + im + " AS IM ON IM.\"OID\" = PR.\"OID_IMPUESTO_VENTA\"" +
                    " WHERE TRUE";

            if (oid > 0) query += " AND PC.\"OID\" = " + oid;

            if (conditions.Client != null) query += " AND PC.\"OID_CLIENTE\" = " + conditions.Client.Oid;
            if (conditions.Producto != null) query += " AND PC.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            if (lock_table) query += " FOR UPDATE OF PC NOWAIT";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lock_table) { return SELECT(0, conditions, lock_table); }

        #endregion	
	}
}