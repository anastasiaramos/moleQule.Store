using System;
using System.Collections.Generic;
using System.Data;

using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class ProductoProveedorInfo : ReadOnlyBaseEx<ProductoProveedorInfo>
	{	
	    #region Attributes

		public ProductoProveedorBase _base = new ProductoProveedorBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidAcreedor { get { return _base.Record.OidAcreedor; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal PDescuento { get { return Decimal.Round(_base.Record.PDescuento, 2); } }
		public long TipoDescuento { get { return _base.Record.TipoDescuento; } }
		public string CodigoArticuloAcreedor { get { return _base.Record.CodigoProductoAcreedor; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
        public bool Automatico { get { return _base.Record.Automatico; } }

        // PROPIEDADES NO ENLAZADAS
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } }
		public string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { _base.Record.FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual Decimal Ayuda { get { return _base._ayuda; } set { _base._ayuda = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
		public virtual long OidFamilia { get { return _base._oid_familia; } set { _base._oid_familia = value; } }
		public virtual string Familia { get { return _base._familia; } set { _base._familia = value; } }
		public virtual Decimal PrecioCompra { get { return _base._precio_compra; } }
		public virtual Decimal PrecioVenta { get { return _base._precio_venta; } set { _base._precio_venta = value; } }
		public virtual string Observaciones { get { return _base._observaciones; } }
		public virtual bool Bulto { get { return _base._bulto; } }
		public virtual string Impuesto { get { return _base._impuesto; } set { _base._impuesto = value; } }
		public virtual Decimal PImpuestos { get { return _base._p_impuestos; } set { _base._p_impuestos = value; } }
		
        #endregion

        #region Business Methods
        
		public static Decimal GetPrecioProveedor(ProductoProveedorInfo productoProveedor, ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			return ProductoProveedorBase.GetPrecioProveedor(productoProveedor, partida, producto, tipo);
		}
		
		public static Decimal GetDescuentoProveedor(ProductoProveedorInfo productoProveedor, Decimal pDescuento)
		{
			return ProductoProveedorBase.GetDescuentoProveedor(productoProveedor, pDescuento);
		}


		#endregion		 

		#region Factory Methods
		 
		protected ProductoProveedorInfo() { /* require use of factory methods */ }
		private ProductoProveedorInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}			
		internal ProductoProveedorInfo(ProductoProveedor source)
		{
            _base.CopyValues(source);
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static ProductoProveedorInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new ProductoProveedorInfo(sessionCode, reader, childs);
		}

		public static ProductoProveedorInfo New(long oid = 0) { return new ProductoProveedorInfo() { Oid = oid }; }
		
		#endregion		 
		 
		#region Data Access
		 
		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
			    _base.CopyValues(source);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
					
		#endregion		
	}
}

