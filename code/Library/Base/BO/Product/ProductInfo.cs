using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class ProductInfo : ReadOnlyBaseEx<ProductInfo>, IAgenteHipatia
	{
        #region IAgenteHipatia

        public string NombreHipatia { get { return Nombre; } }
        public string IDHipatia { get { return string.Empty; } }
		public Type TipoEntidad { get { return typeof(Product); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion
	
		#region Attributes

		public ProductBase _base = new ProductBase();

		private KitList _components = null;
		protected ClientProductList _productos_clientes = null;
		protected BatchList _partidas = null;
		protected ProductoProveedorList _productos_proveedores = null;

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidAyuda { get { return _base.Record.OidAyuda; } }
        public long OidFamilia { get { return _base.Record.OidFamilia; } }
        public long OidImpuestoCompra { get { return _base.Record.OidImpuestoCompra; } }
        public long OidImpuestoVenta { get { return _base.Record.OidImpuestoVenta; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return String.Format("{0:" + Resources.Defaults.PRODUCTO_CODE_FORMAT + "}", _base.Record.Codigo); } }
		public string ExternalCode { get { return  _base.Record.ExternalCode; } }
		public bool Bulto { get { return _base.Record.Bulto; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public string Nombre { get { return _base.Record.Nombre; } }
        public string Descripcion { get { return _base.Record.Descripcion; } }
		public Decimal PrecioCompra { get { return _base.Record.PrecioCompra; } }
		public Decimal PrecioVenta { get { return _base.Record.PrecioVenta; } }
		public long TipoFacturacion { get { return _base.Record.TipoVenta; } }
		public Decimal StockMinimo { get { return _base.Record.StockMinimo; } }
		public Decimal KilosBulto { get { return _base.Record.KilosBulto; } }
		public bool AvisarStock { get { return _base.Record.AvisarStock; } }
		public Decimal AyudaKilo { get { return _base.Record.AyudaKilo; } }
		public string CuentaContableCompra { get { return _base.Record.CuentaContableCompra; } }
		public string CuentaContableVenta { get { return _base.Record.CuentaContableVenta; } }
		public string CodigoAduanero { get { return _base.Record.CodigoAduanero; } set { _base.Record.CodigoAduanero = value; } }
		public bool BeneficioCero { get { return _base.Record.BeneficioCero; } }
        public bool AvisarBeneficioMinimo { get { return _base.Record.AvisarBeneficioMinimo; } }
        public decimal PBeneficioMinimo { get { return _base.Record.PBeneficioMinimo; } }
		public bool IsKit { get { return _base.Record.IsKit; } }
        public bool NoStockSale { get { return _base.Record.NoStockSale; } }

		public virtual KitList Components { get { return _components; } }
        public virtual ClientProductList ProductoClientes { get { return _productos_clientes; } }
		public virtual BatchList Partidas { get { return _partidas; } }
		public virtual ProductoProveedorList ProductoProveedores { get { return _productos_proveedores; } }

        //PROPIEDADES NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { _base.EEstado = value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } set { _base.ETipoFacturacion = value; } }
		public virtual string TipoFacturacionLabel { get { return _base.TipoFacturacionLabel; } }

		public virtual bool Elaborado { get { return _base._elaborado; } set { _base._elaborado = value; } }
		public virtual BatchInfo Partida { get { return ((_partidas != null) && (_partidas.Count > 0)) ? _partidas[0] : null; } }
		public virtual string CodigoFamilia { get { return _base._numero_familia; } set { _base._numero_familia = value; } }
		public virtual string Familia { get { return _base.Familia; } set { _base.Familia = value; } }
		public virtual string ImpuestoCompra { get { return _base.ImpuestoCompra; } }
		public virtual decimal PImpuestoCompra { get { return _base._p_impuesto_compra; } }
		public virtual string ImpuestoVenta { get { return _base.ImpuestoVenta; } }
		public virtual decimal PImpuestoVenta { get { return _base._p_impuesto_venta; } }
		public virtual string CodigoArticuloAcreedor { get { return _base._codigo_articulo_acreedor; } set { _base._codigo_articulo_acreedor = value; } }

        //Trazabilidad
        public long OidProducto { get { return _base._oid_producto; } }
        public long OidProveedor { get { return _base._oid_proveedor; } }
        public string Proveedor { get { return _base._proveedor; } }
        public string NExpediente { get { return _base._n_expediente; } }
        public DateTime FechaCompra { get { return _base._fecha_compra; } }
        public string Naviera { get { return _base._naviera; } }
        public string TransporteOri { get { return _base._trans_ori; } }
        public string TrasporteDes { get { return _base._trans_dest; } }
        public string Cliente { get { return _base._cliente; } }
        public DateTime FechaVenta { get { return _base._fecha_venta; } }
        public decimal KilosVendidos { get { return _base._kilos_vendidos; } }

		#endregion
		
		#region Business Methods
		
        public void CopyFrom(Product source) { _base.CopyValues(source); }

        public virtual bool CheckStock(ETipoFacturacion saleType, decimal amount, out ProductInfo noStockProduct)
        {
            noStockProduct = this;

            if (IsKit)
            {
                ProductInfo product = null;
                if (Components == null || Components.Count == 0) LoadChilds(typeof(Kit), false);

                foreach (KitInfo item in Components)
                {
                    product = ProductInfo.Get(item.OidProduct, false);

                    if (!product.CheckStock(saleType, amount * item.Amount, out noStockProduct))
                    {
                        noStockProduct = product;
                        return false;
                    }
                }

                noStockProduct = null;
                return true;
            }
            else
            {
                noStockProduct = this;

                StockList stock = StockList.GetListByProducto(Oid, false, false);
                
                switch (saleType)
                {
                    case ETipoFacturacion.Peso: return (stock.TotalKgs() >= amount);
                    case ETipoFacturacion.Unidad: return (stock.TotalUds() >= amount);
                    case ETipoFacturacion.Unitaria: return (stock.TotalUds() >= amount);
                }

                return false;
            }            
        }

		public virtual Decimal GetPrecioCompra(ProductoProveedorInfo productoProveedor, BatchInfo batch, ETipoFacturacion tipo)
		{
            return ProductoProveedorInfo.GetPrecioProveedor(productoProveedor, this, batch, tipo);
		}
        public virtual Decimal GetPrecioVenta(ProductoClienteInfo productoCliente, BatchInfo batch, ETipoFacturacion tipo)
		{
            return ProductoClienteInfo.GetPrecioCliente(productoCliente, this, batch, tipo); 
		}

		public virtual Decimal GetDescuentoCompra(ProductoProveedorInfo productoProveedor, Decimal pDescuento)
		{
			return ProductoProveedorInfo.GetDescuentoProveedor(productoProveedor, pDescuento);
		}
		public virtual Decimal GetDescuentoVenta(ProductoClienteInfo productoCliente, Decimal pDescuento) 
		{
			return ProductoClienteInfo.GetDescuentoCliente(productoCliente, pDescuento);
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected ProductInfo() { /* require use of factory methods */ }
		private ProductInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal ProductInfo(Product item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_components = (item.Components != null) ? KitList.GetChildList(item.Components) : null;
                _productos_clientes = (item.ProductoClientes != null) ? ClientProductList.GetChildList(item.ProductoClientes) : null;
				_partidas = (item.Partidas != null) ? BatchList.GetChildList(item.Partidas) : null;
				_productos_proveedores = (item.ProductoProveedores != null) ? ProductoProveedorList.GetChildList(item.ProductoProveedores) : null;				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static ProductInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static ProductInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new ProductInfo(sessionCode, reader, childs); }

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(Kit)))
			{
				_components = KitList.GetChildList(this, childs);
			}
		}

        public static ProductInfo New(long oid = 0) { return new ProductInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
        public static ProductInfo Get(long oid) { return Get(oid, false); }
		public static ProductInfo Get(long oid, bool childs)
		{ 
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

			criteria.Query = ProductInfo.SELECT(oid);
	
			ProductInfo obj = DataPortal.Fetch<ProductInfo>(criteria);
			Product.CloseSession(criteria.SessionCode);
			return obj;
		}
        public static ProductInfo Get(long oid, bool childs, bool cache)
        {
            ProductInfo item;

            if (!cache) return Get(oid, childs);

            //No está en la cache de listas
            if (!Cache.Instance.Contains(typeof(ProductList)))
            {
				ProductList items = ProductList.NewList();

				item = ProductInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(ProductList), items);
            }
            else
            {
                ProductList items = Cache.Instance.Get(typeof(ProductList)) as ProductList;
                item = items.GetItem(oid);

                //No está en la lista de la cache de listas
                if (item == null)
                {
                    item = ProductInfo.Get(oid, childs);
                    items.AddItem(item);
                    Cache.Instance.Save(typeof(ProductList), items);
                }
            }

            return item;
        }
		
        #endregion
		
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

                        query = ClientProductList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _productos_clientes = ClientProductList.GetChildList(reader);
						
						query = BatchList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_partidas = BatchList.GetChildList(SessionCode, reader, Childs);
						
						query = ProductoProveedorList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_productos_proveedores = ProductoProveedorList.GetChildList(SessionCode, reader);						
                    }
				}
			}
			catch (Exception ex)
			{
				CloseSession();
				iQExceptionHandler.TreatException(ex);				
			}
		}
		
		#endregion
		
		#region Child Data Access
		
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

                    query = ClientProductList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _productos_clientes = ClientProductList.GetChildList(reader);
					
					query = BatchList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_partidas = BatchList.GetChildList(SessionCode, reader, true);
					
					query = ProductoProveedorList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_productos_proveedores = ProductoProveedorList.GetChildList(SessionCode, reader);					
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return Product.SELECT(oid, false); }

        #endregion
	}
}
