using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Store.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
    public class ProductViewModel : ViewModelBase<Product, ProductInfo>, IViewModel
	{
		#region Attributes

		protected ProductBase _base = new ProductBase();

		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long OidAyuda { get { return _base.Record.OidAyuda; } set { _base.Record.OidAyuda = value; } }
		
		[HiddenInput]
		public long OidFamilia { get { return _base.Record.OidFamilia; } set { _base.Record.OidFamilia = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "SERIAL")]
		public long Serial { get { return _base.Record.Serial; } set { _base.Record.Serial = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "CODE")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PACKAGE")]
		public bool Bulto { get { return _base.Record.Bulto; } set { _base.Record.Bulto = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "NAME")]
		public string Nombre { get { return _base.Record.Nombre; } set { _base.Record.Nombre = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DESCRIPTION")]
		public string Descripcion { get { return _base.Record.Descripcion; } set { _base.Record.Descripcion = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PURCHASE_PRICE")]
		public Decimal PrecioCompra { get { return _base.Record.PrecioCompra; } set { _base.Record.PrecioCompra = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "SALE_PRICE")]
		public Decimal PrecioVenta { get { return _base.Record.PrecioVenta; } set { _base.Record.PrecioVenta = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "HELP_KILO")]
		public Decimal AyudaKilo { get { return _base.Record.AyudaKilo; } set { _base.Record.AyudaKilo = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CUSTOM_CODE")]
		public string CodigoAduanero { get { return _base.Record.CodigoAduanero; } set { _base.Record.CodigoAduanero = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "OBSERVATIONS")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }
		
		[HiddenInput]
		public long OidImpuestoCompra { get { return _base.Record.OidImpuestoCompra; } set { _base.Record.OidImpuestoCompra = value; } }
		
		[HiddenInput]
		public long OidImpuestoVenta { get { return _base.Record.OidImpuestoVenta; } set { _base.Record.OidImpuestoVenta = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PURCHASE_ACCOUNTANT_ACCOUNT")]
		public string CuentaContableCompra { get { return _base.Record.CuentaContableCompra; } set { _base.Record.CuentaContableCompra = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "SALE_ACCOUNTANT_ACCOUNT")]
		public string CuentaContableVenta { get { return _base.Record.CuentaContableVenta; } set { _base.Record.CuentaContableVenta = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "UNITARY")]
		public bool Unitario { get { return _base.Record.Unitario; } set { _base.Record.Unitario = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public long Estado { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PACKAGE_KILOS")]
		public Decimal KilosBulto { get { return _base.Record.KilosBulto; } set { _base.Record.KilosBulto = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "MINIMUM_STOCK")]
		public Decimal StockMinimo { get { return _base.Record.StockMinimo; } set { _base.Record.StockMinimo = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TYPE_SALE")]
		public long TipoVenta { get { return _base.Record.TipoVenta; } set { _base.Record.TipoVenta = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "STOCK_ALERT")]
		public bool AvisarStock { get { return _base.Record.AvisarStock; } set { _base.Record.AvisarStock = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ZERO_PROFIT")]
		public bool BeneficioCero { get { return _base.Record.BeneficioCero; } set { _base.Record.BeneficioCero = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "MINIMUM_PROFIT_ALERT")]
		public bool AvisarBeneficioMinimo { get { return _base.Record.AvisarBeneficioMinimo; } set { _base.Record.AvisarBeneficioMinimo = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "MINIMUM_PROFIT_P")]
		public Decimal PBeneficioMinimo { get { return _base.Record.PBeneficioMinimo; } set { _base.Record.PBeneficioMinimo = value; } }
		
		
		//UNLINKED PROPERTIES
        [HiddenInput]
        public long Status { get { return Estado; } set { Estado = value; } }

		public virtual EEstado EStatus { get { return _base.EEstado; } set { _base.Record.Estado = (long)value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.EstadoLabel; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "FAMILIA")]
		public virtual string Familia { get { return _base.Familia; } set { _base.Familia = value; } }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(Product source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(ProductInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Product dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public ProductViewModel() { }

        public static ProductViewModel New() 
		{
            ProductViewModel obj = new ProductViewModel();
			obj.CopyFrom(ProductInfo.New());
			return obj;
		}
        public static ProductViewModel New(Product source) { return New(source.GetInfo(false)); }
        public static ProductViewModel New(ProductInfo source)
		{
            ProductViewModel obj = new ProductViewModel();
			obj.CopyFrom(source);
			return obj;
		}

        public static ProductViewModel Get(long oid)
		{
            ProductViewModel obj = new ProductViewModel();
			obj.CopyFrom(ProductInfo.Get(oid, false));
			return obj;
		}

        public static void Add(ProductViewModel item)
		{
            Product newItem = Product.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(ProductViewModel source, HttpRequestBase request = null)
		{
            Product item = Product.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
            Product.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class ProductListViewModel : List<ProductViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public ProductListViewModel() { }

		public static ProductListViewModel Get()
		{
			ProductListViewModel list = new ProductListViewModel();

			ProductList sourceList = ProductList.GetList();

			foreach (ProductInfo item in sourceList)
				list.Add(ProductViewModel.New(item));

			return list;
		}
		public static ProductListViewModel Get(ProductList sourceList)
		{
			ProductListViewModel list = new ProductListViewModel();

			foreach (ProductInfo item in sourceList)
				list.Add(ProductViewModel.New(item));

			return list;
		}

		#endregion
	}
}
