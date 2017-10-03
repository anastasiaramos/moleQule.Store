using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Serie;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ProductBase
	{
		#region Attributes

        private ProductRecord _record = new ProductRecord();

		//NO ENLAZADOS
		internal bool _elaborado = false;
		private string _nombre_familia = string.Empty;
		internal string _numero_familia = string.Empty;
		internal string _impuesto_compra = string.Empty;
		internal decimal _p_impuesto_compra;
		internal string _impuesto_venta = string.Empty;
		internal decimal _p_impuesto_venta;
		internal string _codigo_articulo_acreedor = string.Empty;

		//Trazabilidad
		internal long _oid_producto;
		internal long _oid_proveedor = 0;
		internal string _proveedor;
		internal string _n_expediente;
		internal DateTime _fecha_compra;
		internal string _naviera;
		internal string _trans_ori;
		internal string _trans_dest;
		internal string _cliente;
		internal DateTime _fecha_venta;
		internal decimal _kilos_vendidos;
		internal Decimal _ultimo_precio_compra;
		internal Decimal _ultimo_precio_venta;

		#endregion

		#region Properties

        public ProductRecord Record { get { return _record; } set { _record = value; } }

		//NO ENLAZADOS
		public EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		public string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		public ETipoFacturacion ETipoFacturacion { get { return (ETipoFacturacion)_record.TipoVenta; } set { _record.TipoVenta = (long)value; } }
		public string TipoFacturacionLabel { get { return moleQule.Common.Structs.EnumText<ETipoFacturacion>.GetLabel(ETipoFacturacion); } }
		public string Familia { get { return _nombre_familia; } set { _nombre_familia = value; } }
		internal virtual string ImpuestoCompra { get { return (_record.OidImpuestoCompra != 0) ? _impuesto_compra : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } set { _impuesto_compra = value; } }
		internal virtual string ImpuestoVenta { get { return (_record.OidImpuestoVenta != 0) ? _impuesto_venta : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } set { _impuesto_venta = value; } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

			long query_type = Format.DataReader.GetInt32(source, "QUERY_TYPE");

			switch ((Product.EQueryType)query_type)
			{
				case Product.EQueryType.GENERAL:

					_record.CopyValues(source);

					_codigo_articulo_acreedor = Format.DataReader.GetString(source, "CODIGO_ARTICULO_PROVEEDOR");
					_numero_familia = Format.DataReader.GetString(source, "ID_FAMILIA");
					_nombre_familia = Format.DataReader.GetString(source, "FAMILIA");
					_impuesto_compra = Format.DataReader.GetString(source, "IMPUESTO_COMPRA");
					_p_impuesto_compra = Format.DataReader.GetDecimal(source, "P_IMPUESTO_COMPRA");
					_impuesto_venta = Format.DataReader.GetString(source, "IMPUESTO_VENTA");
					_p_impuesto_venta = Format.DataReader.GetDecimal(source, "P_IMPUESTO_VENTA");
					_ultimo_precio_compra = Format.DataReader.GetDecimal(source, "ULTIMO_PRECIO_COMPRA");
					_ultimo_precio_venta = Format.DataReader.GetDecimal(source, "ULTIMO_PRECIO_VENTA");

					break;

				case Product.EQueryType.CUSTOMCODES:
            
					_record.Oid = Format.DataReader.GetInt64(source, "OID");
					_record.Codigo = Format.DataReader.GetString(source, "CODIGO");
					_record.Nombre = Format.DataReader.GetString(source, "NOMBRE");
					_record.Descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
					_record.CodigoAduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");

					_numero_familia = Format.DataReader.GetString(source, "ID_FAMILIA");
					_nombre_familia = Format.DataReader.GetString(source, "FAMILIA");

					break;
			}
		}
		public void CopyValues(Product source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_nombre_familia = source.Familia;
			_numero_familia = source.CodigoFamilia;
			_elaborado = source.Elaborado;
			_impuesto_compra = source.ImpuestoCompra;
			_p_impuesto_compra = source.PImpuestoCompra;
			_impuesto_venta = source.ImpuestoVenta;
			_p_impuesto_venta = source.PImpuestoVenta;
			_codigo_articulo_acreedor = source.CodigoArticuloAcreedor;
		}
		public void CopyValues(ProductInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_nombre_familia = source.Familia;
			_numero_familia = source.CodigoFamilia;
			_elaborado = source.Elaborado;
			_impuesto_compra = source.ImpuestoCompra;
			_p_impuesto_compra = source.PImpuestoCompra;
			_impuesto_venta = source.ImpuestoVenta;
			_p_impuesto_venta = source.PImpuestoVenta;
			_codigo_articulo_acreedor = source.CodigoArticuloAcreedor;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
	public class Product : BusinessBaseEx<Product>
	{	 
		#region Attributes

		public ProductBase _base = new ProductBase();

		private Kits _components = Kits.NewChildList();
		private ProductoClientes _producto_clientes = ProductoClientes.NewChildList();
		private Batchs _partidas = Batchs.NewChildList();
		private ProductoProveedores _producto_proveedores = ProductoProveedores.NewChildList();

        #endregion

        #region Properties

		public ProductBase Base { get { return _base; } }

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
        public virtual long OidAyuda
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAyuda;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidAyuda .Equals(value))
				{
					_base.Record.OidAyuda = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidFamilia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFamilia;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFamilia.Equals(value))
                {
                    _base.Record.OidFamilia = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidImpuestoCompra
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidImpuestoCompra;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidImpuestoCompra.Equals(value))
                {
                    _base.Record.OidImpuestoCompra = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidImpuestoVenta
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidImpuestoVenta;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidImpuestoVenta.Equals(value))
                {
                    _base.Record.OidImpuestoVenta = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Codigo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return String.Format("{0:" + Resources.Defaults.PRODUCTO_CODE_FORMAT + "}", _base.Record.Codigo);
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Codigo.Equals(value))
                {
                    _base.Record.Codigo = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string ExternalCode
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ExternalCode;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.ExternalCode.Equals(value))
				{
					_base.Record.ExternalCode = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Serial;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Serial.Equals(value))
                {
                    _base.Record.Serial = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Bulto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Bulto;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Bulto .Equals(value))
				{
					_base.Record.Bulto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
					if (value == null) value = string.Empty;
				
				if (!_base.Record.Observaciones .Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
					if (value == null) value = string.Empty;
				
				if (!_base.Record.Nombre .Equals(value))
				{
					_base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Descripcion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Descripcion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Descripcion.Equals(value))
                {
                    _base.Record.Descripcion = value;
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
				return _base.Record.PrecioCompra;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.PrecioCompra .Equals(value))
				{
					_base.Record.PrecioCompra = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PrecioVenta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrecioVenta;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.PrecioVenta .Equals(value))
				{
					_base.Record.PrecioVenta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal KilosBulto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.KilosBulto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.KilosBulto.Equals(value))
				{
					_base.Record.KilosBulto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal StockMinimo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.StockMinimo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.StockMinimo.Equals(value))
				{
					_base.Record.StockMinimo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool AvisarStock
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.AvisarStock;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.AvisarStock.Equals(value))
				{
					_base.Record.AvisarStock = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoFacturacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoVenta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoVenta.Equals(value))
				{
					_base.Record.TipoVenta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal AyudaKilo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.AyudaKilo;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.AyudaKilo .Equals(value))
				{
					_base.Record.AyudaKilo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContableCompra
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContableCompra;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!CanEditCuentaContable()) return;
				if (value == null) value = string.Empty;
				if (!_base.Record.CuentaContableCompra.Equals(value))
				{
					_base.Record.CuentaContableCompra = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContableVenta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContableVenta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!CanEditCuentaContable()) return;
				if (value == null) value = string.Empty;
				if (!_base.Record.CuentaContableVenta.Equals(value))
				{
					_base.Record.CuentaContableVenta = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string CodigoAduanero
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CodigoAduanero;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.CodigoAduanero.Equals(value))
                {
                    _base.Record.CodigoAduanero = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual bool BeneficioCero
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BeneficioCero;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.BeneficioCero.Equals(value))
				{
					_base.Record.BeneficioCero = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual bool AvisarBeneficioMinimo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.AvisarBeneficioMinimo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.AvisarBeneficioMinimo.Equals(value))
                {
                    _base.Record.AvisarBeneficioMinimo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual decimal PBeneficioMinimo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PBeneficioMinimo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.PBeneficioMinimo.Equals(value))
                {
                    _base.Record.PBeneficioMinimo = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual bool IsKit
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IsKit;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.IsKit.Equals(value))
				{
					_base.Record.IsKit = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual bool NoStockSale
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.NoStockSale;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.NoStockSale.Equals(value))
                {
                    _base.Record.NoStockSale = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual Kits Components
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _components;
			}
		}
        public virtual ProductoClientes ProductoClientes
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _producto_clientes;
			}
		}
		public virtual Batchs Partidas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _partidas;
			}
		}
		public virtual ProductoProveedores ProductoProveedores
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _producto_proveedores;
			}
		}
		
        //NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } set { TipoFacturacion = (long)value; } }
		public virtual string TipoFacturacionLabel { get { return _base.TipoFacturacionLabel; } }
        public virtual bool Elaborado { get { return _base._elaborado; } set { _base._elaborado = value; } }
        public virtual Batch Partida { get { return _partidas.Count > 0 ? _partidas[0] : null; } }
        public virtual string CodigoFamilia { get { return _base._numero_familia; } set { _base._numero_familia = value; } }
		public virtual string Familia { get { return _base.Familia; } set { _base.Familia = value; } }
		public virtual string ImpuestoCompra { get { return _base.ImpuestoCompra; } set { _base._impuesto_compra = value; } }
		public virtual decimal PImpuestoCompra { get { return _base._p_impuesto_compra; } set { _base._p_impuesto_compra = value; } }
		public virtual string ImpuestoVenta { get { return _base.ImpuestoVenta; } set { _base._impuesto_venta = value; } }
		public virtual decimal PImpuestoVenta { get { return _base._p_impuesto_venta; } set { _base._p_impuesto_venta = value; } }
		public virtual string CodigoArticuloAcreedor { get { return _base._codigo_articulo_acreedor; } set { _base._codigo_articulo_acreedor = value; } }

        public override bool IsValid
		{
			get { return base.IsValid
						 && _producto_clientes.IsValid
						 && _partidas.IsValid
						 && _producto_proveedores.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _producto_clientes.IsDirty
						 || _partidas.IsDirty
						 || _producto_proveedores.IsDirty ; }
                     }

        #endregion

        #region Business Methods

		public virtual Product CloneAsNew()
		{
			Product clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = Product.OpenSession();
			Product.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.ProductoClientes.MarkAsNew();
			clon.Partidas.MarkAsNew();
			clon.ProductoProveedores.MarkAsNew();
			
			return clon;
		}
			
        public virtual void GetNewCode()
        {
            //Si se ha establecido que los códigos de los productos se generen de forma automática
            if (ModulePrincipal.GetCodigoProductoAutomaticoSetting())
            {
                Serial = SerialInfo.GetNext(typeof(Product));
                Codigo = Serial.ToString(Resources.Defaults.PRODUCTO_CODE_FORMAT);
            }
        }

        public virtual Decimal GetPrecioCompra(ProductoProveedorInfo productoProveedor, BatchInfo batch, ETipoFacturacion tipo)
		{
            return ProductoProveedorInfo.GetPrecioProveedor(productoProveedor, GetInfo(false), batch, tipo);
		}
        public virtual Decimal GetPrecioVenta(ProductoClienteInfo productoCliente, BatchInfo batch, ETipoFacturacion tipo)
		{
            return ProductoClienteInfo.GetPrecioCliente(productoCliente, GetInfo(false), batch, tipo); 
		}

		public virtual Decimal GetDescuentoCompra(ProductoProveedorInfo productoProveedor, Decimal pDescuento)
		{
			return ProductoProveedorInfo.GetDescuentoProveedor(productoProveedor, pDescuento);
		}
		public virtual Decimal GetDescuentoVenta(ProductoClienteInfo productoCliente, Decimal pDescuento)
		{
			return ProductoClienteInfo.GetDescuentoCliente(productoCliente, pDescuento);
		}

        public virtual void CalculaPrecioKit()
        {
            if (Partida.Componentes.Count == 0) return;

            PrecioCompra = 0;
            PrecioVenta = 0;

            decimal costeKilo = 0;

            foreach (Batch partida in Partida.Componentes)
            {
                PrecioCompra += partida.PrecioCompraKilo * partida.Proporcion / 100;
                PrecioVenta += partida.PrecioVentaKilo * partida.Proporcion / 100;
                costeKilo += partida.CosteKilo * partida.Proporcion / 100;
            }

            Partida.PrecioCompraKilo = PrecioCompra;
            Partida.PrecioVentaKilo = PrecioVenta;
            //Partida.PrecioVentaBulto = PrecioCompra * Partida.KilosPorBulto;
            //Partida.CosteKilo = costeKilo;
        }

        public virtual bool CheckStock(ETipoFacturacion saleType, decimal amount, out ProductInfo noStockProduct)
        {
            return GetInfo(true).CheckStock(saleType, amount, out noStockProduct);
        }

        public virtual void SetImpuesto(ImpuestoInfo source, ETipoSerie tipo)
        {
            if (source == null)
            {
                if (tipo == ETipoSerie.Compra)
                {
                    OidImpuestoCompra = 0;
                    ImpuestoCompra = moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto);
                    PImpuestoCompra = 0;
                }
                else
                {
                    OidImpuestoVenta = 0;
                    ImpuestoVenta = moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto);
                    PImpuestoVenta = 0;
                }
            }
            else
            {
                if (tipo == ETipoSerie.Compra)
                {
                    OidImpuestoCompra = source.Oid;
                    ImpuestoCompra = source.Nombre;
                    PImpuestoCompra = source.Porcentaje;
                }
                else
                {
                    OidImpuestoVenta = source.Oid;
                    ImpuestoVenta = source.Nombre;
                    PImpuestoVenta = source.Porcentaje;
                }
            }
        }

        public virtual void UpdatePurchasePrice()
        {
            if (IsKit)
                PrecioCompra = Components.Sum(x => x.PurchasePrice * x.Amount);
        }

        public virtual void UpdatePrecioMix()
        {
            if (Partida.Componentes.Count == 0) return;

            foreach (Batch partida in Partida.Componentes)
            {
                partida.PrecioCompraKilo = PrecioCompra;
                //pe.PrecioCompraBulto = PrecioCompra * pe.KilosPorBulto;
                partida.PrecioVentaKilo = PrecioVenta;
                //pe.PrecioVentaBulto = PrecioVenta * pe.KilosPorBulto;
            }

            Partida.PrecioCompraKilo = PrecioCompra;
            Partida.PrecioVentaKilo = PrecioVenta;
            //Partida.PrecioVentaBulto = PrecioCompra * Partida.KilosPorBulto;
        }

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Nombre
            if (Codigo == string.Empty)
            {
                e.Description = Resources.Messages.NO_ID;
                throw new iQValidationException(e.Description, string.Empty, "ID");
            }

            //Descripcion
            if (Descripcion == string.Empty)
            {
                e.Description = Resources.Messages.NO_DESCRIPCION;
                throw new iQValidationException(e.Description, string.Empty, "Descripcion");
            }

            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PRODUCTO);
		}		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PRODUCTO);
		}		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PRODUCTO);
		}		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PRODUCTO);
		}

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid)
		{
            QueryConditions conditions = new QueryConditions
            {
                Producto = ProductInfo.New(oid),
                Estado = EEstado.NoAnulado,
            };

            InputDeliveryLineList in_deliveries = InputDeliveryLineList.GetList(conditions, false);

            if (in_deliveries.Count > 0)
                throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);
		}

		#endregion

		#region Common Factory Methods

		private Product(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}

		internal static Product Get(int sessionCode, IDataReader reader) { return new Product(sessionCode, reader); }

		public virtual ProductInfo GetInfo(bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new ProductInfo(this, childs);
		}

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(Kit)))
			{
				_components = Kits.GetChildList(this, childs);
			}
		}

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
		/// </summary>
		protected Product() {}

		public static Product New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Product>(new CriteriaCs(-1));
		}

        public static Product NewElaborado()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Product obj = DataPortal.Create<Product>(new CriteriaCs(-1));
            obj.Elaborado = true;
            obj.Partidas.NewItem(obj);
            return obj;
        }

		public static Product Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());

			criteria.Childs = childs;
			criteria.Query = Product.SELECT(oid);
			
			Product.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Product>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}

        private void DeleteKit()
        {
            //Es un Kit. Borramos los productos_expedientes asociados
            if (Partida != null && Partida.Componentes.Count > 0)
            {
                Partidas.Clear();
                Save();
            }
        }

		/// <summary>
		/// Elimina todos los Producto. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Product.OpenSession();
			ISession sess = Product.Session(sessCode);
			ITransaction trans = Product.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from ProductRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Product.CloseSession(sessCode);
			}
		}

		public override Product Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
                throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

                Expedient almacen = null;
                List<long> oid_expedientes = new List<long>();

                //Actualizacion de stocks cuando se trata de Kits
                if ((Partida != null) && (Partida.Oid != 0))
                {
                    oid_expedientes.Add(Partida.Oid);

					Partida.ETipoPartida = ETipoPartida.Kit;

                    // Alta del Kit en el Almacén
                    almacen = Expedient.GetAlmacen(false);
                    almacen.LoadChildsFromList(typeof(Batch), Partida.Oid.ToString(), true, true);
                    almacen.LoadStockByPartida(Partida.Oid, true, true);

                    Stock stock = almacen.Stocks.NewItem(Partida, ETipoStock.AltaKit);
                    stock.OidExpediente = almacen.Oid;
                    stock.Inicial = true;
					stock.Observaciones = "Alta de Kit " + Codigo;
                    almacen.UpdateStocks(Partida, true);

                    foreach (Batch pexp in Partida.Componentes)
                    {
                        oid_expedientes.Add(pexp.Oid);

						pexp.ETipoPartida = ETipoPartida.Componente;

                        // Baja del producto componente. Actualizamos el stock del Expediente asociado al componente. 
                        Expedient exp = Expedient.Get(pexp.OidExpediente, false);
                        exp.LoadChildsFromList(typeof(Batch), pexp.Oid.ToString(), true, true);
                        exp.LoadStockByPartida(pexp.Oid, true, true);
                        Batch partida = exp.Partidas.GetItemByProducto(pexp.OidProducto);

                        stock = exp.Stocks.NewItem(pexp, ETipoStock.MovimientoSalida);
                        stock.OidPartida = partida.Oid;
                        stock.Observaciones = "Salida por Kit " + Codigo;

                        exp.Stocks.UpdateStocks(partida, true);
                        exp.Stocks.Save();

                        // Alta del producto componente. Actualizamos el stock del Almacen para los Kits.
						Stock stock_almacen = almacen.Stocks.NewItem(pexp, ETipoStock.MovimientoEntrada);
                        stock_almacen.OidExpediente = 1;
                        stock_almacen.OidStock = stock.Oid;
                        stock_almacen.Observaciones = "Entrada por Kit " + Codigo;
                        almacen.UpdateStocks(partida, true);

                        exp.CloseSession();
                    }
                }

                //Hay que grabar despues de guardar los stocks para conservar los OidExpediente de cada articulo para el stock
				if (IsKit) _components.Update(this);
				_producto_clientes.Update(this);
                _partidas.Update(this);
                _producto_proveedores.Update(this);

                Transaction().Commit();

                if (Partida != null)
                {
                    //Hay que grabar despues de guardar el producto para actualizar el OidPartida
                    almacen.Stocks.GetItemByBatch(oid_expedientes[0]).OidPartida = Partida.Oid;

                    int i = 1;
                    
                    foreach (Batch pexp in Partida.Componentes)
                    {
                        Stock stock = almacen.Stocks.GetItemByBatch(oid_expedientes[i++]);
                        stock.OidPartida = pexp.Oid;
                        stock.OidKit = Partida.Oid;                        
                    }

                    almacen.Save();
                    almacen.CloseSession();
                }
                return this;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                if (CloseSessions) CloseSession(); 
				else BeginTransaction();
            }
        }
				
		#endregion
		
		#region Common Data Access
		 
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			Random r = new Random();
            Oid = (long)r.Next();
            GetNewCode();
			EEstado = EEstado.Active;
			OidFamilia = 1;
			KilosBulto = 1;
			StockMinimo = 1;
			AvisarStock = true;
            NoStockSale = false;
			ETipoFacturacion = Store.ModulePrincipal.GetDefaultTipoFacturacionSetting();

            try
            {
                FamiliaInfo familia = FamiliaInfo.Get(1);
				Familia = familia.Nombre;
                CodigoFamilia = familia.Codigo.ToString();
            }
            catch { }
		
			_producto_clientes = ProductoClientes.NewChildList();
			_partidas = Batchs.NewChildList();
			_producto_proveedores = ProductoProveedores.NewChildList();
		}
		 
		#endregion
		 
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Product.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						ProductoCliente.DoLOCK(Session());
						query = ProductoClientes.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_producto_clientes = ProductoClientes.GetChildList(reader);
						
						Batch.DoLOCK(Session());
						query = Batchs.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_partidas = Batchs.GetChildList(SessionCode, reader);
						
						ProductoProveedor.DoLOCK(Session());
						query = ProductoProveedores.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_producto_proveedores = ProductoProveedores.GetChildList(SessionCode, reader);
 					}
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
			}
		}
		
		//Fetch independiente de DataPortal para generar un Producto a partir de un IDataReader
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
		 
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                UpdatePurchasePrice();

                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                GetNewCode();
                
                //si hay codigo o serial, hay que obtenerlos aquí
				Session().Save(Base.Record);

                if (Elaborado)
                    Partida.CopyFrom(this);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
                    UpdatePurchasePrice();

					ProductRecord obj = Session().Get<ProductRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
				}
			}
		}
		
		//Deferred deletion
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

                Product producto = Product.Get(criteria.Oid);
                producto.DeleteKit();
                producto.CloseSession();

                CriteriaEx criterio = GetCriteria();
                criterio.AddOidSearch(criteria.Oid);
				Session().Delete(criterio.UniqueResult() as ProductRecord);
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion

        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1, CUSTOMCODES = 2 }

        internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>()
            {
				{
					"Familia", 
                    new ForeignField() {                        
						Property = "Familia", 
                        TableAlias = "FM", 
                        Column = nHManager.Instance.GetTableColumn(typeof(FamilyRecord), "Nombre")
                    } 
                },
            };
        }

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string FIELDS( QueryConditions conditions)
        {
			string query = string.Empty;

			if (conditions.Groups != null)
			{
				if (conditions.Groups.FirstOrDefault(x => x.Property == "CodigoAduanero") != null)
				{
					query = @"
						SELECT " + (long)EQueryType.CUSTOMCODES + @" AS ""QUERY_TYPE""
							,MAX(P.""OID"") AS ""OID""
							,'VARIOS' AS ""CODIGO""
							,MAX(P.""CODIGO_ADUANERO"") AS ""CODIGO_ADUANERO""
							,MAX(P.""NOMBRE"") AS ""NOMBRE""
							,MAX(P.""DESCRIPCION"") AS ""DESCRIPCION""
							,MAX(FM.""CODIGO"") AS ""ID_FAMILIA""
							,MAX(FM.""NOMBRE"") AS ""FAMILIA""";
				}
			}
			else
			{
				query = @"
				SELECT DISTINCT " + (long)EQueryType.GENERAL + @" AS ""QUERY_TYPE""
						,P.*
						,FM.""CODIGO"" AS ""ID_FAMILIA""
						,FM.""NOMBRE"" AS ""FAMILIA""
						,IPC.""NOMBRE"" AS ""IMPUESTO_COMPRA""
						,IPC.""PORCENTAJE"" AS ""P_IMPUESTO_COMPRA""
						,IPV.""NOMBRE"" AS ""IMPUESTO_VENTA""
						,IPV.""PORCENTAJE"" AS ""P_IMPUESTO_VENTA""";

				if (conditions.Client != null)
				{
					query += @"        
						,COALESCE(CFC.""PRECIO"", 0) AS ""ULTIMO_PRECIO_VENTA""";
				}
				else
				{
					query += @"
						,0 AS ""ULTIMO_PRECIO_VENTA""";
				}

				if (conditions.Acreedor != null)
				{
					query += @"
						,COALESCE(PP.""CODIGO_PRODUCTO_ACREEDOR"", '') AS ""CODIGO_ARTICULO_PROVEEDOR""
						,COALESCE(CFP.""PRECIO"", 0) AS ""ULTIMO_PRECIO_COMPRA""";
				}
				else
				{
					query += @"
						,'' AS 	""CODIGO_ARTICULO_PROVEEDOR""
						,0 AS ""ULTIMO_PRECIO_COMPRA""";
				}
			}

			return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            string fm = nHManager.Instance.GetSQLTable(typeof(FamilyRecord));
            string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string pp = nHManager.Instance.GetSQLTable(typeof(ProductoProveedorRecord));
            string sf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.FamilySerieRecord));
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string cfp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            string fc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputInvoiceRecord));
            string cfc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputInvoiceLineRecord));

            string query = 
			FIELDS(conditions) + @"
			FROM " + pr + @" AS P
			INNER JOIN " + fm + @" AS FM ON FM.""OID"" = P.""OID_FAMILIA""
			LEFT JOIN " + ip + @" AS IPC ON IPC.""OID"" = P.""OID_IMPUESTO_COMPRA""
			LEFT JOIN " + ip + @" AS IPV ON IPV.""OID"" = P.""OID_IMPUESTO_VENTA""";

            if (conditions == null) return query;

			if ((conditions.Almacen != null) || (conditions.Expedient != null))
                query += @"
				LEFT JOIN " + pa + @" AS PA ON PA.""OID_PRODUCTO"" = P.""OID""";

            if (conditions.Proveedor != null)
                query += @"
				INNER JOIN " + pp + @" AS PP ON PP.""OID_PRODUCTO"" = P.""OID""";

            if (conditions.Serie != null)
                query += @"
				INNER JOIN " + sf + @" AS SF ON SF.""OID_FAMILIA"" = P.""OID_FAMILIA""";

			if (conditions.Acreedor != null)
			{
				//ProductoProveedor para este acreedor
				query += @"
				LEFT JOIN " + pp + @" AS PP ON (PP.""OID_PRODUCTO"" = P.""OID"" AND PP.""OID_ACREEDOR"" = " + conditions.Acreedor.OidAcreedor + @" AND PP.""TIPO_ACREEDOR"" = " + (long)conditions.TipoAcreedor[0] + ")";

				//Ultimo precio de compra
				query += @"
				LEFT JOIN (SELECT CFP.""OID_PRODUCTO""
								,CFP.""PRECIO""
								,ROW_NUMBER() OVER (ORDER BY FP.""FECHA"") AS ""ROW_NUMBER""
							FROM " + cfp + @" AS CFP
							INNER JOIN " + fp + @" AS FP ON FP.""OID"" = CFP.""OID_FACTURA""
							WHERE FP.""OID_ACREEDOR"" = " + conditions.Acreedor.OidAcreedor + @" FP.""TIPO_ACREEDOR"" = " + (long)conditions.TipoAcreedor[0] + @"
							GROUP BY CFP.""OID_PRODUCTO"", CFP.""PRECIO"", FP.""FECHA"")
					AS CFP ON CFP.""OID_PRODUCTO"" = P.""OID"" AND CFP.""ROW_NUMBER"" = 1";
			}

            if (conditions.Client != null)
            {
                //Ultimo precio de venta
                query += @"
				LEFT JOIN (SELECT CFC.""OID_PRODUCTO""
								,CFC.""PRECIO""
								,ROW_NUMBER() OVER (ORDER BY FP.""FECHA"") AS ""ROW_NUMBER""
							FROM " + cfc + @" AS CFC
							INNER JOIN " + fc + @" AS FC ON FC.""OID"" = CFC.""OID_FACTURA""
							WHERE FC.""OID_CLIENTE"" = " + conditions.Client.Oid + @"
							GROUP BY CFC.""OID_PRODUCTO"", CFC.""PRECIO"", FC.""FECHA"")
					AS CFC ON CFP.""OID_PRODUCTO"" = P.""OID"" AND CFC.""ROW_NUMBER"" = 1";
            }

            query += WHERE(conditions);

			if (conditions.Groups != null)
			{
				query += GROUPBY(conditions.Groups, "P", ForeignFields());
				query += ORDER(conditions.Orders, "P", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}
			else
			{
				query += ORDER(conditions.Orders, "P", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
			if (conditions == null) return string.Empty;

            string query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "P", ForeignFields());
			
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "P");

			if (conditions.Producto != null)
			{
				if (conditions.Producto.Oid > 0)
					query += @"
						AND P.""OID"" = " + conditions.Producto.Oid;

				if (!string.IsNullOrEmpty(conditions.Producto.CodigoAduanero))
					query += @"
						AND P.""CODIGO_ADUANERO"" = '" + conditions.Producto.CodigoAduanero + "'";
			}
			
			if (conditions.Familia != null) 
				query += @"
					AND FM.""OID"" = " + conditions.Familia.Oid;
			
			if (conditions.Almacen != null) 
				query += @"
					AND PA.""OID_ALMACEN"" = " + conditions.Almacen.Oid;
			
			if (conditions.Expedient != null) 
				query += @"
					AND PA.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;
			
			if (conditions.Proveedor != null) 
				query += @"
					AND PP.""OID_ACREEDOR"" = " + conditions.Proveedor.Oid + @" AND PP.""TIPO_ACREEDOR"" = " + (long)ETipoAcreedor.Proveedor;
            
			if (conditions.Serie != null) 
				query += @"
					AND SF.""OID_SERIE"" = " + conditions.Serie.Oid;
            
			if (conditions.TipoFamilia != ETipoFamilia.Todas) 
				query += @"
					AND FM.""OID"" = " + (long)conditions.TipoFamilia;

            return query + " " + conditions.ExtraWhere;
        }

        internal static string LOCK(bool lockTable)
        {
            string query = string.Empty;
            //if (lockTable) query += " FOR UPDATE OF P NOWAIT";
            return query;
        }
        
        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query =
			SELECT_BASE(conditions) +
            LOCK(lockTable);

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

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
            SELECT COUNT(*) AS ""TOTAL_ROWS""" +
			SELECT(conditions) +
			WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query;

			QueryConditions conditions = new QueryConditions { Producto = ProductInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}
		internal static string SELECT(StoreInfo item, bool lockTable)
		{
			string query;

			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Almacen = item };

			query = SELECT_BASE(conditions) +
					LOCK(lockTable);

			return query;
		}
        internal static string SELECT(ExpedientInfo item, bool lockTable)
        {            
            string query;

            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Expedient = item };

            query = SELECT_BASE(conditions) +                 
                    LOCK(lockTable);

            return query;
        }
        internal static string SELECT(ProveedorInfo item, bool lockTable)
        {            
            string query;

            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Proveedor = item };
            
            query = SELECT_BASE(conditions) + 
                    LOCK(lockTable);

            return query;
        }
        internal static string SELECT(SerieInfo item, bool lockTable)
        {            
            string query;

            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Serie = item };

            query = 
			SELECT_BASE(conditions) + @"
            ORDER BY P.""NOMBRE""" +
			LOCK(lockTable);

            return query;
        }

        internal static string SELECT(ETipoFamilia item, bool lockTable)
        {
            string query;

            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { TipoFamilia = item };

            query = 
				SELECT_BASE(conditions) +       
                LOCK(lockTable);

            return query;
        }
        internal static string SELECT(ProveedorInfo item, ETipoFamilia familia, bool lockTable)
        {
            string query = string.Empty;
            
            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
			{ 
				Proveedor = item,
                TipoFamilia = familia 
			};

            query = 
				SELECT_BASE(conditions) +
                LOCK(lockTable);

            return query;
        }

        internal static string SELECT_ELABORADOS(bool lockTable)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

            string query;
            
            query = 
			SELECT_BASE(null) + @"
            INNER JOIN " + pa + @" AS PA ON PA.""OID_PRODUCTO"" = P.""OID""
            INNER JOIN " + pa + @" AS PA2 ON PA2.""OID_KIT"" = PA.""OID""" +
            LOCK(lockTable);

            return query;
        }

		internal static string SELECT_KIT(QueryConditions conditions, bool isKit, bool lockTable)
		{
			conditions.ExtraWhere += @"
				AND P.""IS_KIT"" = " + isKit.ToString();

			string query =
				SELECT(conditions, lockTable);

			return query;
		}
        
		#endregion
    }
}