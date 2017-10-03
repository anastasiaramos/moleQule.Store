using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class StockBase
	{
		#region Attributes

		private StockRecord _record = new StockRecord();

		internal long _oid_serie;
		internal long _oid_familia;
		internal string _id_partida = string.Empty;
		internal string _expediente = string.Empty;
        public string _store = string.Empty;
        public string _store_id = string.Empty;
		internal Decimal _kilos_actuales;
		internal Decimal _bultos_actuales;
		internal Decimal _stock_kgs;
		internal Decimal _stock_bultos;
		internal Decimal _stock_acumulado_kgs;
		internal Decimal _stock_acumulado_bultos;
		internal string _producto = string.Empty;
		internal bool _facturacion_bulto = false;
		internal string _n_albaran = string.Empty;
		internal string _n_cliente = string.Empty;
		internal string _cliente = string.Empty;
		internal string _proveedor = string.Empty;
		internal string _n_titular = string.Empty;
		internal string _titular = string.Empty;
		internal string _usuario = string.Empty;
		internal string _n_factura = string.Empty;
        internal decimal _purchase_price;
        internal decimal _avg_purchase_price;
        internal decimal _last_purchase_price;
        internal decimal _sale_price;

        internal bool _entrada = false;
        internal long _oid_cliente;

		#endregion

		#region Properties

		public StockRecord Record { get { return _record; } }

        public long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
        public bool Entrada { get { return _entrada; } set { _entrada = value; } }
        public string Store { get { return _store; } set { _store = value; } }
        public string StoreID { get { return _store_id; } set { _store_id = value; } }
		public ETipoStock ETipoStock { get { return (ETipoStock)_record.Tipo; } set { _record.Tipo = (long)value; } }
		public string TipoStockLabel { get { return EnumText<ETipoStock>.GetLabel(ETipoStock); } }
		internal virtual bool FacturacionPeso { get { return !_facturacion_bulto; } }
		internal ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
        public decimal PurchasePrice { get { return _purchase_price; } set { _purchase_price = value; } }
        public decimal AvgPurchasePrice { get { return _avg_purchase_price; } set { _avg_purchase_price = value; } }
        public decimal LastPurchasePrice { get { return _last_purchase_price; } set { _last_purchase_price = value; } }
        public decimal SalePrice { get { return _sale_price; } set { _sale_price = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			long query = Format.DataReader.GetInt64(source, "QUERY");

			_n_factura = Format.DataReader.GetString(source, "N_FACTURA_ASOCIADA");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");
			_id_partida = Format.DataReader.GetString(source, "ID_PARTIDA");
			_producto = Format.DataReader.GetString(source, "PRODUCTO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "BULTO");
			_n_albaran = Format.DataReader.GetString(source, "N_ALBARAN");
			_n_titular = Format.DataReader.GetString(source, "N_TITULAR");
			_titular = Format.DataReader.GetString(source, "CLIENTE");
			_n_cliente = Format.DataReader.GetString(source, "N_TITULAR");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
            _store = Format.DataReader.GetString(source, "STORE");
            _store_id = Format.DataReader.GetString(source, "STORE_ID");
			_usuario = Format.DataReader.GetString(source, "USUARIO");
            _purchase_price = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
            _sale_price = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
            //_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
            //_entrada = Format.DataReader.GetBool(source, "ENTRADA");

			if (query == (long)Stock.EQueryType.MOVEMENTS)
			{
				_stock_acumulado_kgs = Format.DataReader.GetDecimal(source, "STOCK_ANTERIOR_KG");
				_stock_acumulado_bultos = Format.DataReader.GetDecimal(source, "STOCK_ANTERIOR_BULTO");
			}

			switch (ETipoStock)
			{
				case ETipoStock.Compra:
					_titular = _proveedor;
					_cliente = _proveedor;
					break;

				case ETipoStock.Reserva:
					_n_albaran = Format.DataReader.GetString(source, "N_PEDIDO");
					break;
			}

			if ((_n_albaran != string.Empty) &&
				(_n_albaran.IndexOf("/") >= 0) &&
				(_n_albaran.Substring(_n_albaran.IndexOf("/")).Length == 1)) _n_albaran = string.Empty;
			if ((_n_factura != string.Empty) &&
				(_n_factura.IndexOf("/") >= 0) &&
				(_n_factura.Substring(_n_factura.IndexOf("/")).Length == 1)) _n_factura = string.Empty;

		}
		internal void CopyValues(Stock source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_stock_kgs = source.KilosActuales;
			_stock_bultos = source.BultosActuales;
			_n_factura = source.NFactura;
			//_oid_serie = source.OidSerie;
			_id_partida = source.IDPartida;
			_producto = source.Producto;
			_facturacion_bulto = source.Bulto;
			_n_albaran = source.NAlbaran;
			_n_titular = source.NCliente;
			_titular = source.Cliente;
			_n_cliente = source.NCliente;
			_cliente = source.Cliente;
            _store = source.Store;
            _store_id = source.StoreID;
			_expediente = source.Expediente;
			_usuario = source.Usuario;
            _entrada = source.Entrada;
            _oid_cliente = source.OidCliente;
            _purchase_price = source.PurchasePrice;
            _sale_price = source.SalePrice;
            _avg_purchase_price = source.AvgPurchasePrice;
            _last_purchase_price = source.LastPurchasePrice;
		}
		internal void CopyValues(StockInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_stock_kgs = source.KilosActuales;
			_stock_bultos = source.BultosActuales;
			_n_factura = source.NFactura;
			//_oid_serie = source.OidSerie;
			_id_partida = source.IDPartida;
			_producto = source.Producto;
			_facturacion_bulto = source.Bulto;
			_n_albaran = source.NAlbaran;
			_n_titular = source.NCliente;
			_titular = source.Cliente;
			_n_cliente = source.NCliente;
			_cliente = source.Cliente;
            _store = source.Store;
            _store_id = source.StoreID;
			_expediente = source.Expediente;
            _usuario = source.Usuario;
            _entrada = source.Entrada;
            _oid_cliente = source.OidCliente;
            _purchase_price = source.PurchasePrice;
            _sale_price = source.SalePrice;
            _avg_purchase_price = source.AvgPurchasePrice;
            _last_purchase_price = source.LastPurchasePrice;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class Stock : BusinessBaseEx<Stock>
	{	
	    #region Attributes

		protected StockBase _base = new StockBase();

        #endregion

        #region Properties

		public StockBase Base { get { return _base; } }

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
		public virtual long OidPartida
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPartida;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPartida.Equals(value))
				{
					_base.Record.OidPartida = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidExpediente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpediente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExpediente.Equals(value))
				{
					_base.Record.OidExpediente = value;
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
		public virtual long OidCliente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.OidCliente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.OidCliente.Equals(value))
				{
					_base.OidCliente = value;
					PropertyHasChanged();
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
		public virtual long OidConceptoAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidConceptoAlbaran;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidConceptoAlbaran.Equals(value))
				{
					_base.Record.OidConceptoAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidStock
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidStock;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidStock.Equals(value))
				{
					_base.Record.OidStock = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidKit
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidKit;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidKit.Equals(value))
				{
					_base.Record.OidKit = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Concepto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Concepto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Concepto.Equals(value))
				{
					_base.Record.Concepto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Bultos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Bultos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Bultos.Equals(value))
				{
					_base.Record.Bultos = Decimal.Round(value, 4);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Kilos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Kilos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Kilos.Equals(value))
				{
					_base.Record.Kilos = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Entrada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Entrada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Entrada.Equals(value))
				{
					_base.Entrada = value;
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

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Tipo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Tipo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Tipo.Equals(value))
				{
					_base.Record.Tipo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Inicial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Inicial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Inicial.Equals(value))
				{
					_base.Record.Inicial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidLineaPedido
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidLineaPedido;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidLineaPedido.Equals(value))
				{
					_base.Record.OidLineaPedido = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidAlmacen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlmacen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAlmacen.Equals(value))
				{
					_base.Record.OidAlmacen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual int OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidUsuario.Equals(value))
				{
					_base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidEnlace
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidEnlace;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
				if (!_base.Record.OidEnlace.Equals(value))
                {
					_base.Record.OidEnlace = value;
                    PropertyHasChanged();
                }
            }
        }

        //CAMPOS NO ENLAZADOS
		public virtual ETipoStock ETipoStock { get { return _base.ETipoStock; } set { _base.ETipoStock = value; } }
		public virtual string TipoStockLabel { get { return _base.TipoStockLabel; } }
		public virtual long OidSerie { get { return _base._oid_serie; } }
		public virtual long OidFamilia { get { return _base._oid_familia; } }
		public virtual Decimal KilosActuales { get { return _base._kilos_actuales; } set { _base._kilos_actuales = Decimal.Round(value, 2); } }
		public virtual Decimal BultosActuales { get { return _base._bultos_actuales; } set { _base._bultos_actuales = Decimal.Round(value, 4); } }
		public virtual bool IsKitComponent { get { return OidKit > 0; } }
		public virtual string IDPartida { get { return _base._id_partida; } set { _base._id_partida = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
		public virtual bool Bulto { get { return _base._facturacion_bulto; } set { _base._facturacion_bulto = value; } }
		public virtual string NAlbaran { get { return _base._n_albaran; } set { _base._n_albaran = value; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual string NCliente { get { return _base._n_titular; } set { _base._n_titular = value; } }
		public virtual string Cliente { get { return _base._titular; } set { _base._titular = value; } }
		public virtual string NTitular { get { return _base._n_titular; } set { _base._n_titular = value; } }
		public virtual string Proveedor { get { return _base._proveedor; } set { _base._proveedor = value; } }
        public virtual string Store { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual Decimal KilosAcumulados { get { return _base._stock_acumulado_kgs; } set { _base._stock_acumulado_kgs = value; } }
		public virtual Decimal BultosAcumulados { get { return _base._stock_acumulado_bultos; } set { _base._stock_acumulado_bultos = value; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { Bulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
        public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
        public decimal PurchasePrice { get { return _base.PurchasePrice; } set { _base.PurchasePrice = value; } }
        public decimal SalePrice { get { return _base.SalePrice; } set { _base.SalePrice = value; } }
        public decimal AvgPurchasePrice { get { return _base.AvgPurchasePrice; } set { _base.AvgPurchasePrice = value; } }
        public decimal LastPurchasePrice { get { return _base.LastPurchasePrice; } set { _base.LastPurchasePrice = value; } }

        #endregion

        #region Business Methods

        public virtual void CopyFrom(IStockable parent)
        {
            switch (parent.EEntityType)
            {
                case ETipoEntidad.InputDeliveryLine:

                    ETipoStock = ETipoStock.Compra;
                    Inicial = true;
                    OidAlbaran = parent.OidDelivery;
                    OidConceptoAlbaran = parent.Oid;
                    OidLineaPedido = parent.OidOrderLine;
                    Kilos = parent.Kilos;
			        Bultos = parent.Pieces;

                    break;

                case ETipoEntidad.OutputDeliveryLine:

                    switch (parent.EHolderType)
                    {
                        case ETipoEntidad.Cliente: ETipoStock = ETipoStock.Venta; break;
                        case ETipoEntidad.WorkReport: ETipoStock = ETipoStock.Consumo; break;
                        default: ETipoStock = ETipoStock.Venta; break;
                    }

                    OidAlbaran = parent.OidDelivery;
                    OidConceptoAlbaran = parent.Oid;
                    Kilos = -parent.Kilos;
			        Bultos = -parent.Pieces;

                    break;

                case ETipoEntidad.OutputOrderLine:

                    ETipoStock = ETipoStock.Reserva;
                    OidLineaPedido = parent.Oid;
                    Kilos = -parent.Kilos;
			        Bultos = -parent.Pieces;
                    
                    break;
            }
            
            OidPartida = parent.OidBatch;
            OidAlmacen = parent.OidStore;
            OidExpediente = parent.OidExpedient;
            OidProducto = parent.OidProduct;
            OidKit = parent.OidKit;
            Producto = ProductInfo.Get(parent.OidProduct, false).Nombre;
            Concepto = parent.Concepto;
        }
        
		public virtual void CopyFrom(Batch parent, ETipoStock tipo)
		{
			CopyFrom(parent, null, tipo);
		}

        public virtual void CopyFrom(Batch parent, ETipoStock tipo, Expedient expediente)
        {
            CopyFrom(parent, null, tipo);
            OidConceptoAlbaran = expediente.Stocks.GetInitialStock(OidPartida).OidConceptoAlbaran;
            OidAlbaran = expediente.Stocks.GetInitialStock(OidPartida).OidAlbaran;
        }

        public virtual void CopyFrom(Batch parent, Stock stock, ETipoStock tipo)
        {
			ETipoStock = tipo;
			OidConceptoAlbaran = (stock != null) ? stock.OidConceptoAlbaran : 0;
			OidAlbaran = (stock != null) ? stock.OidAlbaran : 0;
			if (stock != null) Fecha = stock.Fecha;
            OidPartida = parent.Oid;
			OidAlmacen = parent.OidAlmacen;
            OidExpediente = parent.OidExpediente;
            OidProducto = parent.OidProducto;
            Producto = parent.TipoMercancia;
            Concepto = parent.TipoMercancia;
            Store = parent.Almacen;
            StoreID = parent.IDAlmacen;
            Expediente = parent.Expediente;

            switch (ETipoStock)
			{
				case ETipoStock.Compra:
				case ETipoStock.MovimientoEntrada:
				case ETipoStock.AltaKit:
					Kilos = parent.StockKilos;
					Bultos = parent.StockBultos;
					break;

				case ETipoStock.Venta:
				case ETipoStock.MovimientoSalida:
				case ETipoStock.BajaKit:
				case ETipoStock.Merma:
					Kilos = -parent.StockKilos;
					Bultos = -parent.StockBultos;
					break;	
			}

            KilosActuales = parent.StockKilos;
            BultosActuales = parent.StockBultos;

			IDPartida = parent.Codigo;
        }

		public virtual void SetSignoStock()
		{
			switch (ETipoStock)
			{
				case ETipoStock.Venta:
				case ETipoStock.MovimientoSalida:
				case ETipoStock.BajaKit:
				case ETipoStock.Merma:
                case ETipoStock.Consumo:
					if (Kilos > 0) Kilos -= (Kilos * 2);
					if (Bultos > 0) Bultos -= (Bultos * 2);
					break;

				case ETipoStock.Compra:
				case ETipoStock.MovimientoEntrada:
				case ETipoStock.AltaKit:
					if (Kilos < 0) Kilos -= (Kilos * 2);
					if (Bultos < 0) Bultos -= (Bultos * 2);
					break;
			}
		}

		public virtual void UpdateUnidades(Batch pe)
		{
			if (pe == null) return;

			switch (ETipoStock)
			{
				case ETipoStock.Venta:
				case ETipoStock.MovimientoSalida:
				case ETipoStock.BajaKit:
				case ETipoStock.Merma:
					if (Bultos > 0) Bultos -= (Bultos * 2);
					break;

				case ETipoStock.Compra:
				case ETipoStock.MovimientoEntrada:
				case ETipoStock.AltaKit:
					if (Bultos < 0) Bultos -= (Bultos * 2);
					break;
			}

			Kilos = Bultos * pe.KilosPorBulto;
		}

		public virtual void UpdateBultos(Batch pe)
		{
			if (pe == null) return;

			switch (ETipoStock)
			{
				case ETipoStock.Venta:
				case ETipoStock.BajaKit:
				case ETipoStock.MovimientoSalida:
				case ETipoStock.Merma:
					if (Kilos > 0) Kilos -= (Kilos * 2);
					break;

				case ETipoStock.Compra:
				case ETipoStock.AltaKit:
				case ETipoStock.MovimientoEntrada:
					if (Kilos < 0) Kilos -= (Kilos * 2);
					break;
			}

			Bultos = Kilos / pe.KilosPorBulto;
		}

		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(IsNeenedTipoStock, "ETipoStock");
		}

		private bool IsNeenedTipoStock(object target, Csla.Validation.RuleArgs e)
		{
			if (ETipoStock == ETipoStock.Todos)
			{
				e.Description = string.Format(Resources.Messages.NO_FIELD_SELECTED, "Tipo");
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
		}
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EXPEDIENTE);
		}
		 
		#endregion
		 
		#region Child Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public Stock() 
		{ 
			MarkAsChild();
            _base.Record.Oid = (long)(new Random()).Next();
            Fecha = DateTime.Now;
            OidUsuario = AppContext.User != null ? (int)AppContext.User.Oid: 0;
            Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
		}			
		private Stock(Stock source)
		{
			MarkAsChild();
			Fetch(source);
		}
		private Stock(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static Stock NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Stock();
		}
        public static Stock NewChild(IStockable parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Stock obj = new Stock();
            obj.CopyFrom(parent);
            return obj;
        }
        public static Stock NewChild(Batch parent, Stock stock, ETipoStock tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Stock obj = new Stock();
			obj.CopyFrom(parent, stock, tipo);
			return obj;
        }
		public static Stock NewChild(Expedient parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Stock obj = new Stock();
			obj.OidExpediente = parent.Oid;
			
			return obj;
		}		
		public static Stock NewChild(Product parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Stock obj = new Stock();
			obj.OidProducto = parent.Oid;
			
			return obj;
		}
		
		internal static Stock GetChild(Stock source)
		{
			return new Stock(source);
		}		
		internal static Stock GetChild(int sessionCode, IDataReader reader) { return new Stock(sessionCode, reader); }
		
		public virtual StockInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new StockInfo(this, false);
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
		public override Stock Save()
		{
			throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(Stock source)
		{
			_base.CopyValues(source);
			MarkOld();
		}		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}

        internal void Insert(IStockable parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            switch (parent.EEntityType)
            {
                case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:
            
                    OidAlmacen = parent.OidStore;
                    OidConceptoAlbaran = parent.Oid;
                    OidExpediente = parent.OidExpedient;
                    OidProducto = parent.OidProduct;
                    
                    break;
            }

            OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
            Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Update(IStockable parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            switch (parent.EEntityType)
            {
                case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:

                    OidAlmacen = parent.OidStore;
                    OidConceptoAlbaran = parent.Oid;
                    OidExpediente = parent.OidExpedient;
                    OidProducto = parent.OidProduct;

                    break;
            }

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                StockRecord obj = Session().Get<StockRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(IStockable parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<StockRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

		internal void Insert(Almacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidAlmacen = parent.Oid;
            OidUsuario = AppContext.User != null ? (int)AppContext.User.Oid : 0;
            Usuario = AppContext.User != null ?AppContext.User.Name : string.Empty;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(Almacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidAlmacen = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				StockRecord obj = Session().Get<StockRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(Almacen parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<StockRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

		internal void Insert(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidExpediente = parent.Oid;
			OidUsuario = AppContext.User != null ? (int)AppContext.User.Oid : 0;
			Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                long oid_old = this.Oid;
				parent.Session().Save(Base.Record);

                if (ETipoStock == ETipoStock.Merma && parent.ETipoExpediente == ETipoExpediente.Ganado)
                {
                    LivestockBook libro = LivestockBook.Get(1, false, true, parent.SessionCode);
                    LivestockBookLine linea = libro.Lineas.NewItem(libro);

                    linea.ETipo = ETipoLineaLibroGanadero.Muerte;
                    linea.OidConceptoAlbaran = OidConceptoAlbaran;
                    linea.Crotal = Concepto;
                    linea.Causa = Resources.Labels.LIBRO_GANADERO_CAUSA_BAJA_MERMA;
                    linea.Fecha = Fecha;
                    linea.EEstado = EEstado.Baja;

                    linea.OidPartida = OidPartida;

                    libro.SaveAsChild();
                }

                Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;
                if (expedientes != null)
                {
                    bool save = false;
                    foreach (Expedient item in expedientes)
                    {
                        if (item.Oid != parent.Oid)
                        {
                            if (item.Stocks != null)
                            {
                                save = item.UpdateEnlaceStocks(oid_old, this.Oid);
                                item.UpdateStocks(true);
                            }
                        }
                    }

                    if (save) expedientes.SaveAsChild();
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			this.OidExpediente = parent.Oid; 
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				StockRecord obj = Session().Get<StockRecord>(Oid);

                Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;
                if (expedientes != null)
                {
                    bool save = false;
                    foreach (Expedient item in expedientes)
                    {
                        if (item.Oid != parent.Oid)
                        {
                            if (item.Stocks != null)
                            {
                                save = item.UpdateEnlaceStocks(obj.Oid, this.Oid);
                                item.UpdateStocks(true);
                            }
                        }
                    }

                    if (save) expedientes.SaveAsChild();
                }

				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<StockRecord>(Oid));
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
			OidUsuario = AppContext.User != null ? (int)AppContext.User.Oid : 0;
			Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
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
			
			this.OidProducto = parent.Oid; 
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				StockRecord obj = Session().Get<StockRecord>(Oid);
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
				Session().Delete(Session().Get<StockRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

        internal void Insert(Stocks parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
				OidUsuario = AppContext.User != null ? (int)AppContext.User.Oid : 0;
                Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Update(Stocks parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
				StockRecord obj = Session().Get<StockRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(Stocks parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<StockRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

		#endregion

        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1, MOVEMENTS = 2 }

        internal static string FIELDS(EQueryType queryType)
        {
            string query;

            query = @"
				SELECT " + (long)queryType + @" AS ""QUERY""
					,ST.*
					,COALESCE(US.""NAME"", '') AS ""USUARIO""
					,COALESCE(BA.""CODIGO"", '') AS ""ID_PARTIDA""
					,COALESCE(BA.""TIPO_MERCANCIA"", '') AS ""PRODUCTO""
                    ,COALESCE(CP.""PRECIO"", 0) AS ""PRECIO_COMPRA""
                    ,COALESCE(CA.""PRECIO"", 0) AS ""PRECIO_VENTA""
					,COALESCE(CA.""FACTURACION_BULTO"", COALESCE(CP.""FACTURACION_BULTO"", FALSE)) AS ""BULTO""
					,COALESCE(SEC.""IDENTIFICADOR"", COALESCE(SEP.""IDENTIFICADOR"", COALESCE(SEWR.""IDENTIFICADOR"", ''))) || '/' || COALESCE(AB.""CODIGO"", COALESCE(AP.""CODIGO"", COALESCE(ABWR.""CODIGO"", ''))) AS ""N_ALBARAN""
					,COALESCE(SEC.""IDENTIFICADOR"", COALESCE(SEP.""IDENTIFICADOR"", '')) || '/' || COALESCE(FC.""CODIGO"", COALESCE(FP.""CODIGO"", '')) AS ""N_FACTURA_ASOCIADA""
					,COALESCE(SELP.""IDENTIFICADOR"", COALESCE(SELP.""IDENTIFICADOR"", '')) || '/' || COALESCE(PC.""CODIGO"", COALESCE(PC.""CODIGO"", '')) AS ""N_PEDIDO""
					,COALESCE(CL.""CODIGO"", COALESCE(WR.""CODE"", COALESCE(SU.""CODIGO"", COALESCE(CLLP.""CODIGO"", '')))) AS ""N_TITULAR""
					,COALESCE(CL.""NOMBRE"", COALESCE(CLLP.""NOMBRE"", COALESCE(WREX.""CODIGO"", ''))) AS ""CLIENTE""
					,COALESCE(SU.""NOMBRE"", '') AS ""PROVEEDOR""
					,COALESCE(SEC.""OID"", COALESCE (SEP.""OID"", COALESCE (SELP.""OID"", COALESCE(SEWR.""OID"", 0)))) AS ""OID_SERIE""
					,COALESCE(PR.""OID_FAMILIA"", 0) AS ""OID_FAMILIA""
					,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
					,COALESCE(AL.""CODIGO"", '') AS ""STORE_ID""
                    ,COALESCE(AL.""NOMBRE"", '') AS ""STORE""";

            return query;
        }

		internal static string FIELDS_MOVEMENTS()
		{
			string query = 
            FIELDS(EQueryType.MOVEMENTS) + @"
                ,COALESCE(ST1.""STOCK_ANTERIOR_KG"", 0) AS ""STOCK_ANTERIOR_KG""
				,COALESCE(ST1.""STOCK_ANTERIOR_BULTO"", 0) AS ""STOCK_ANTERIOR_BULTO""";

			return query;
		}

		internal static string INNER_BASE(QueryConditions conditions)
		{
			string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
			string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));

			string od = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryRecord));
			string odl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryLineRecord));
			string af = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryInvoiceRecord));
            string fc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputInvoiceRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ClientRecord));

			string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
			string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
			string afp = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

			string lp = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OrderLineRecord));
			string pc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OrderRecord));

			string se = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

			string wr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.WorkReportRecord));

			string tipo_entrada = "(" + (long)ETipoStock.Compra +
									"," + (long)ETipoStock.AltaKit +
									"," + (long)ETipoStock.MovimientoEntrada +
									"," + (long)ETipoStock.Merma + ")";

			string tipo_salida = "(" + (long)ETipoStock.Venta +
								"," + (long)ETipoStock.BajaKit +
								"," + (long)ETipoStock.MovimientoSalida + ")";

			string tipo_reserva = "(" + ((long)ETipoStock.Reserva) + ")";

			string tipo_consumo = "(" + ((long)ETipoStock.Consumo) + ")";
            
            string query = @"
			FROM " + st + @" AS ST
			LEFT JOIN " + us + @" AS US ON US.""OID"" = ST.""OID_USUARIO""
			INNER JOIN " + ba + @" AS BA ON BA.""OID"" = ST.""OID_BATCH""
			LEFT JOIN " + su + @" AS SU ON SU.""OID"" = BA.""OID_PROVEEDOR""
			LEFT JOIN " + pr + @" AS PR ON PR.""OID"" = BA.""OID_PRODUCTO""
			LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = BA.""OID_EXPEDIENTE""
			LEFT JOIN " + al + @" AS AL ON AL.""OID"" = ST.""OID_ALMACEN""";

			//STOCKS DE ENTRADA
			query += @"
			LEFT JOIN " + idl + @" AS CP ON (ST.""OID_CONCEPTO_ALBARAN"" = CP.""OID"" AND ST.""TIPO"" IN " + tipo_entrada + @")
			LEFT JOIN " + id + @" AS AP ON AP.""OID"" = CP.""OID_ALBARAN""
			LEFT JOIN " + se + @" AS SEP ON SEP.""OID"" = AP.""OID_SERIE""
			LEFT JOIN " + afp + @" AS AFP ON AFP.""OID_ALBARAN"" = AP.""OID""
			LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = AFP.""OID_FACTURA""";

			//STOCKS DE SALIDA
			query += @"
			LEFT JOIN " + odl + @" AS CA ON (ST.""OID_CONCEPTO_ALBARAN"" = CA.""OID"" AND ST.""TIPO"" IN " + tipo_salida + @")
			LEFT JOIN " + od + @" AS AB ON CA.""OID_ALBARAN"" = AB.""OID""
			LEFT JOIN " + se + @" AS SEC ON AB.""OID_SERIE"" = SEC.""OID""
			LEFT JOIN " + af + @" AS AF ON AB.""OID"" = AF.""OID_ALBARAN""
			LEFT JOIN " + fc + @" AS FC ON AF.""OID_FACTURA"" = FC.""OID""
			LEFT JOIN " + cl + @" AS CL ON AB.""OID_HOLDER"" = CL.""OID""";

			//STOCKS DE RESERVA
			query += @"
			LEFT JOIN " + lp + @" AS LP ON (ST.""OID_LINEA_PEDIDO"" = LP.""OID"" AND ST.""TIPO"" IN " + tipo_reserva + @")
			LEFT JOIN " + pc + @" AS PC ON LP.""OID_PEDIDO"" = PC.""OID""
			LEFT JOIN " + se + @" AS SELP ON PC.""OID_SERIE"" = SELP.""OID""
			LEFT JOIN " + cl + @" AS CLLP ON PC.""OID_CLIENTE"" = CLLP.""OID""";

			//STOCKS DE CONSUMO EN PARTES DE OBRA
			query += @"
			LEFT JOIN " + odl + @" AS CAWR ON (CAWR.""OID"" = ST.""OID_CONCEPTO_ALBARAN"" AND ST.""TIPO"" IN " + tipo_consumo + @")
			LEFT JOIN " + od + @" AS ABWR ON ABWR.""OID"" = CAWR.""OID_ALBARAN""
			LEFT JOIN " + se + @" AS SEWR ON SEWR.""OID"" = ABWR.""OID_SERIE""
			LEFT JOIN " + wr + @" AS WR ON WR.""OID"" = ABWR.""OID_HOLDER""
			LEFT JOIN " + ex + @" AS WREX ON WREX.""OID"" = WR.""OID_EXPEDIENT""";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @" 
			WHERE (ST.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + @"' AND '" + conditions.FechaFinLabel + @"' OR ST.""FECHA"" ISNULL)";

			if (conditions.Stock != null) 
				if (conditions.Stock.Oid != 0) 
					query += @"
						AND ST.""OID"" = " + conditions.Stock.Oid;

			if (conditions.TipoStock != ETipoStock.Todos) 
				query += @"
					AND ST.""TIPO"" = " + (long)conditions.TipoStock;

			if (conditions.Almacen != null) 
				query += @"
					AND ST.""OID_ALMACEN"" = " + conditions.Almacen.Oid;

			if (conditions.Expedient != null) 
				query += @"
					AND ST.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos) 
				query += @" AND EX.""TIPO_EXPEDIENTE"" = " + (long)conditions.TipoExpediente;

			if (conditions.Partida != null) 
				query += @"
					AND ST.""OID_BATCH"" = " + conditions.Partida.Oid;

			if (conditions.Producto != null)
			{
				if (conditions.Producto.Oid != 0)
					query += @" 
						AND ST.""OID_PRODUCTO"" = " + conditions.Producto.Oid;

				if (!string.IsNullOrEmpty(conditions.Producto.CodigoAduanero))
					query += @" 
						AND PR.""CODIGO_ADUANERO"" = '" + conditions.Producto.CodigoAduanero + "'";
			}

			if (conditions.Serie != null) 
				query += @" 
					AND (SEP.""OID"" = " + conditions.Serie.Oid + @" OR SEC.""OID"" = " + conditions.Serie.Oid + ")";

            if (conditions.IStockable != null)
            {
                switch (conditions.IStockable.EEntityType)
                {
                    case moleQule.Common.Structs.ETipoEntidad.OutputDelivery:
                        
                        query += @"
					        AND ST.""OID_ALBARAN"" = " + conditions.IStockable.Oid;
                        
                        break;

                    case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:
                        
                        query += @"
					        AND ST.""OID_CONCEPTO_ALBARAN"" = " + conditions.IStockable.Oid + @" 
					        AND ST.""TIPO"" = " + (long)ETipoStock.Venta;
                        
                        break;
                }
            }

			if (conditions.InputDelivery != null) 
				query += @"
					AND ST.""OID_ALBARAN"" = " + conditions.InputDelivery.Oid;
			
			if (conditions.ConceptoAlbaranProveedor != null)  
				query += @"
					AND ST.""OID_CONCEPTO_ALBARAN"" = " + conditions.ConceptoAlbaranProveedor.Oid + @" 
					AND ST.""TIPO"" = " + (long)ETipoStock.Compra; 

			return query;
		}

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string query;

			query = FIELDS(EQueryType.GENERAL) +
					INNER_BASE(conditions);
          
            return query;
        }

		internal static string SELECT_MOVEMENTS_BATCH(QueryConditions conditions)
		{
			string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));

			string query = 
            FIELDS_MOVEMENTS() +
			INNER_BASE(conditions) + @"
            LEFT JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""STOCK_ANTERIOR_KG""
                                ,SUM(""BULTOS"") AS ""STOCK_ANTERIOR_BULTO""
                        FROM " + st + @"
                        WHERE ""FECHA"" < '" + conditions.FechaIniLabel + @"'
                        GROUP BY ""OID_BATCH"")
                AS ST1 ON ST1.""OID_BATCH"" = BA.""OID""
            LEFT JOIN (SELECT ""OID_BATCH""
                                ,SUM(""KILOS"") AS ""STOCK_KG""
                                ,SUM(""BULTOS"") AS ""STOCK_BULTO""
                        FROM " + st + @"
                        GROUP BY ""OID_BATCH"")
                AS ST2 ON ST2.""OID_BATCH"" = BA.""OID""" +
			WHERE(conditions);

			return query;
		}

		internal static string SELECT_MOVEMENTS_PRODUCT(QueryConditions conditions)
		{
			string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));

			string query = 
            FIELDS_MOVEMENTS() +
			INNER_BASE(conditions) + @"
            LEFT JOIN (SELECT ""OID_PRODUCTO""
                                ,SUM(""KILOS"") AS ""STOCK_ANTERIOR_KG""
                                ,SUM(""BULTOS"") AS ""STOCK_ANTERIOR_BULTO""
                        FROM " + st + @"
                        WHERE ""FECHA"" < '" + conditions.FechaIniLabel + @"'
                        GROUP BY ""OID_PRODUCTO"")
                AS ST1 ON ST1.""OID_PRODUCTO"" = PR.""OID""
            LEFT JOIN (SELECT ""OID_PRODUCTO""
                                ,SUM(""KILOS"") AS ""STOCK_KG""
                                ,SUM(""BULTOS"") AS ""STOCK_BULTO""
                        FROM " + st + @"
                        GROUP BY ""OID_PRODUCTO"")
                AS ST2 ON ST2.""OID_PRODUCTO"" = PR.""OID""" +
            WHERE(conditions);

			return query;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);					

            //if (lockTable) query += " FOR UPDATE OF ST NOWAIT";

            return query;
        }

		internal static string SELECT(long oid, bool lockTable)
		{
			string query;

			QueryConditions conditions = new QueryConditions { Stock = Stock.NewChild().GetInfo() };
			conditions.Stock.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

        #endregion	
	}
}