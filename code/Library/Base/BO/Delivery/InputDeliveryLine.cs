using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Serie;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Business Object with Child
    /// Este objeto tiene Stocks solo para consulta, pues es el expediente quien se encarga de actualizar los stocks
    /// </summary>
    [Serializable()]
    public class InputDeliveryLine : BusinessBaseEx<InputDeliveryLine>, IStockable
    {
        #region IStockable

        public ETipoEntidad EEntityType { get { return ETipoEntidad.InputDeliveryLine; } }
        public ETipoEntidad EHolderType { get { return ETipoEntidad.Proveedor; } }
        public decimal Kilos { get { return CantidadKilos; } set { CantidadKilos = value; } }
        public decimal Pieces { get { return CantidadBultos; } set { CantidadBultos = value; } }
        public long OidBatch { get { return OidPartida; } }
        public long OidProduct { get { return OidProducto; } }
        public long OidStore { get { return OidAlmacen; } }
        public long OidExpedient { get { return OidExpediente; } }
        public long OidDelivery { get { return OidAlbaran; } }
        public long OidOrderLine { get { return OidLineaPedido; } }
        public ETipoFacturacion InvoicingMode { get { return ETipoFacturacion; } }
                
        #endregion

        #region Attributes

        public InputDeliveryLineBase _base = new InputDeliveryLineBase();

        private Batchs _partidas = Batchs.NewChildList();
        private Stocks _stocks = Stocks.NewChildList();

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
        public virtual long OidPartida
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidBatch;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidBatch.Equals(value))
                {
                    _base.Record.OidBatch = value;
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
		public virtual string CodigoProductoAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoProductoProveedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoProductoProveedor.Equals(value))
				{
					_base.Record.CodigoProductoProveedor = value;
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
                    Subtotal = (_base.Record.FacturacionBulto) ? _base.Record.CantidadBultos * _base.Record.Precio : _base.Record.CantidadKilos * _base.Record.Precio;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Gastos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Gastos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Gastos.Equals(value))
                {
                    _base.Record.Gastos = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal CantidadKilos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CantidadKilos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.CantidadKilos.Equals(value))
                {
                    _base.Record.CantidadKilos = Decimal.Round(value, 2);
                    if (!_base.Record.FacturacionBulto) CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal CantidadBultos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CantidadBultos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.CantidadBultos.Equals(value))
                {
                    _base.Record.CantidadBultos = Decimal.Round(value, 4);
                    if (_base.Record.FacturacionBulto) CalculateTotal();
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
                    _base.Record.Precio = Decimal.Round(value, 5);
                    CalculateTotal();
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
                return _base.Record.PDescuento;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PDescuento.Equals(value))
                {
                    _base.Record.PDescuento = Decimal.Round(value, 2);
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Subtotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Subtotal;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Subtotal.Equals(value))
                {
                    _base.Record.Subtotal = value;                  
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PImpuestos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PIgic;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PIgic.Equals(value))
                {
                    _base.Record.PIgic = Decimal.Round(value, 2);
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Total
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Total;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Total.Equals(value))
                {
                    _base.Record.Total = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PIRPF
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PIrpf;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PIrpf.Equals(value))
                {
                    _base.Record.PIrpf = Decimal.Round(value, 2);
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        
        public virtual Stocks Stocks
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _stocks;
            }
            set
            {
                _stocks = value;
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

            set
            {
                _partidas = value;
            }
        }

        //Campos no enlazados
        public virtual string Almacen { get { return _base.Almacen; } set { _base.Almacen = value; } }
        public virtual string IDAlmacen { get { return _base.IDAlmacen; } set { _base.IDAlmacen = value; } }
        public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string IDBatch { get { return _base.IDBatch; } set { _base.IDBatch = value; } }
		public virtual string CodigoExpediente { get { return _base._expediente; } set { _base._expediente = value; } } /*DEPRECATED*/
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
        public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } set { _base.AyudaKilo = value; PropertyHasChanged(); } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
        public virtual Batch Partida { get { return _partidas.Count > 0 ? _partidas[0] : null; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
        public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } set { _base.ETipoFacturacion = value; } }
        public virtual string SaleMethodLabel { get { return _base.SaleMethodLabel; } }
        public virtual string Ubicacion { get { return _base._ubicacion; } set { _base._ubicacion = value; } }
		public virtual long OidStock { get { return _base._oid_stock; } }
		public virtual long OidPedido { get { return _base._oid_pedido; } set { _base._oid_pedido = value; } }
        public virtual Decimal IRPF { get { return _base.IRPF; } set { PropertyHasChanged(); } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && _stocks.IsValid && _partidas.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty || _stocks.IsDirty || _partidas.IsDirty;
            }
        }
       
        #endregion

        #region Business Methods
		
		public virtual void CopyFrom(InputDelivery source)
		{
			OidAlbaran = source.Oid;
			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;
			Almacen = source.IDAlmacen;
			Expediente = source.Expediente;
            PIRPF = source.PIRPF;
		}
        public virtual void CopyFrom(InputDelivery delivery, BatchInfo source)
        {
			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;
			OidPartida = source.Oid;
			OidProducto = source.OidProducto;
			Gastos = source.CosteKilo;
			Concepto = source.TipoMercancia;
            CantidadKilos = (delivery.Rectificativo) ? -1 : 1;
            CantidadBultos = (delivery.Rectificativo) ? -1 : 1;

			Almacen = source.Almacen;
            IDAlmacen = source.IDAlmacen;
			CodigoExpediente = source.Expediente;
			AyudaKilo = source.AyudaRecibidaKilo;
        }
        public virtual void CopyFrom(InputDelivery delivery, ProductInfo source)
        {
			OidAlbaran = delivery.Oid;
            OidProducto = source.Oid;
            OidImpuesto = source.OidImpuestoCompra;
            PImpuestos = source.PImpuestoCompra;
            PIRPF = delivery.PIRPF;
            Gastos = source.PrecioCompra;
            Concepto = source.Nombre;
			FacturacionPeso = (source.ETipoFacturacion == ETipoFacturacion.Peso);
			CantidadKilos = (delivery.Rectificativo) ? -1 : 1;
            CantidadBultos = (delivery.Rectificativo) ? -1 : 1;
			AyudaKilo = source.AyudaKilo;
			Almacen = delivery.IDAlmacen;
            CodigoProductoAcreedor = source.CodigoArticuloAcreedor != string.Empty ? source.CodigoArticuloAcreedor : source.Codigo;

            if (OidImpuesto == 0)
            {
                FamiliaInfo familia = FamiliaInfo.Get(source.OidFamilia, false);
                OidImpuesto = familia.OidImpuesto;
                PImpuestos = familia.PImpuesto;
            }

            BalanceQuantity(delivery, source);
        }
		public virtual void CopyFrom(LineaPedidoProveedorInfo line)
		{
			OidLineaPedido = line.Oid;
			OidPedido = line.Oid;
			OidProducto = line.OidProducto;
			OidAlmacen = line.OidAlmacen;
			OidExpediente = line.OidExpediente;
			FacturacionBulto = line.FacturacionBulto;
			CantidadKilos = line.Pendiente;
			CantidadBultos = line.PendienteBultos;
			Precio = line.Precio;
			Concepto = line.Concepto;

			Almacen = line.Almacen;
			CodigoExpediente = line.Expediente;
		}
        public virtual void CopyFrom(InputDelivery delivery, IDocumentLine line, ProductInfo product, decimal currencyRate)
		{
			OidAlbaran = delivery.Oid;
			OidProducto = product.Oid;
			OidAlmacen = (line.OidBatch != 0) ? ModulePrincipal.GetDefaultAlmacenSetting() : 0;
			OidImpuesto = 0;
			PImpuestos = 0;
			Gastos = line.Gastos;
			Concepto = product.Nombre;
			FacturacionPeso = (line.InvoicingMode == ETipoFacturacion.Peso);
            CantidadBultos = line.Pieces;
            CantidadKilos = line.Kilos;
			Precio = line.Precio * currencyRate;
			AyudaKilo = product.AyudaKilo;
			Almacen = delivery.IDAlmacen;
			CodigoProductoAcreedor = product.CodigoArticuloAcreedor != string.Empty ? product.CodigoArticuloAcreedor : product.Codigo;

			if (OidImpuesto == 0)
			{
				FamiliaInfo familia = FamiliaInfo.Get(product.OidFamilia, false);
				OidImpuesto = familia.OidImpuesto;
				PImpuestos = familia.PImpuesto;
			}
		}

		/// <summary>
		/// Ajusta la cantidad de los bultos para que elimine decimales espureos
		/// </summary>
		/// <param name="partida"></param>
		public void BalanceQuantity(InputDelivery delivery, ProductInfo product)
		{
			if (product == null)
			{
				if (FacturacionPeso)
					CantidadBultos = CantidadKilos;
				else
					CantidadKilos = CantidadBultos;
			}
			else
			{
				if (FacturacionPeso)
					CantidadBultos = (product.KilosBulto == 0) ? CantidadBultos : CantidadKilos / product.KilosBulto;
				else
					CantidadKilos = (product.KilosBulto == 0) ? CantidadKilos : CantidadBultos * product.KilosBulto;
			}

            CheckQuantity(delivery);
		}

		private void BalancePieces(BatchInfo batch)
		{
			if (batch.StockKilos == 0) return;

			if (CantidadKilos == batch.StockKilos)
				CantidadBultos = batch.StockBultos;
			else
				CantidadBultos = CantidadKilos / batch.KilosPorBulto;
		}
		private void BalanceKilos(BatchInfo batch)
		{
			if (batch.StockBultos == 0) return;

			if (CantidadBultos == batch.StockBultos)
				CantidadKilos = batch.StockKilos;
			else
				CantidadKilos = CantidadBultos * batch.KilosPorBulto;
		}

        public virtual void CheckQuantity(InputDelivery delivery)
        {
            if (delivery.Rectificativo)
            {
                CantidadKilos = (CantidadKilos > 0) ? -CantidadKilos : CantidadKilos;
                CantidadBultos = (CantidadBultos > 0) ? -CantidadBultos : CantidadBultos;
            }
            else
            {
                CantidadKilos = (CantidadKilos < 0) ? -CantidadKilos : CantidadKilos;
                CantidadBultos = (CantidadBultos < 0) ? -CantidadBultos : CantidadBultos;
            }
        }

        public virtual void CalculateTotal()
        {
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
			Total = BaseImponible + Impuestos - IRPF;

			//Para forzar el refresco en el formulario
			Impuestos = Impuestos;
			Descuento = Descuento;
        }

        public virtual void Purchase(InputDelivery delivery, SerieInfo serie, IAcreedorInfo acreedor, ProductInfo producto) { Purchase(delivery, serie, acreedor, producto, null); }
		public virtual void Purchase(InputDelivery delivery, SerieInfo serie, IAcreedorInfo acreedor, ProductInfo producto, BatchInfo partida)
        {
			if (acreedor == null)
				throw new iQException(Resources.Messages.NO_PROVEEDOR_SELECTED);

			if (acreedor.Productos == null)
				acreedor.LoadChilds(typeof(ProductoProveedor), true);

            ProductoProveedorInfo producto_prov = acreedor.Productos.GetItemByProducto(producto.Oid);

			if (partida == null)
                CopyFrom(delivery, producto);
			else
                CopyFrom(delivery);

            SetTipoFacturacion(producto_prov, producto);
            SetTaxes(serie, acreedor, producto, producto_prov);
			Precio = producto.GetPrecioCompra(producto_prov, partida, ETipoFacturacion);
            PDescuento = ProductoProveedorInfo.GetDescuentoProveedor(producto_prov, 0);
        }

		public virtual void SetTipoFacturacion(IAcreedorInfo acreedor, ProductInfo producto)
		{
			ProductoProveedorInfo producto_prov = acreedor.Productos.GetItemByProducto(producto.Oid);
			SetTipoFacturacion(producto_prov, producto);
		}
		public virtual void SetTipoFacturacion(ProductoProveedorInfo productoProv, ProductInfo producto)
		{
			if (productoProv != null)
				FacturacionBulto = productoProv.FacturacionBulto;
			else if (producto != null)
				FacturacionBulto = !(producto.ETipoFacturacion == ETipoFacturacion.Peso);
			else
				FacturacionBulto = false;
		}

        public virtual void SetTaxes(SerieInfo serie, IAcreedorInfo acreedor, ProductInfo producto, ProductoProveedorInfo pp)
        {
            //Primero el acreedor si está EXENTO
            if ((acreedor != null) && (acreedor.OidImpuesto == 1))
            {
                OidImpuesto = acreedor.OidImpuesto;
                PImpuestos = acreedor.PImpuesto;
            }
			//Luego el producto de proveedor
			else if ((pp != null) && (pp.OidImpuesto != 0))
			{
				OidImpuesto = pp.OidImpuesto;
				PImpuestos = pp.PImpuestos;
			}
            //Luego el producto
            else if (producto.OidImpuestoCompra != 0)
            {
                OidImpuesto = producto.OidImpuestoCompra;
                PImpuestos = producto.PImpuestoCompra;
            }
            else if (serie.OidImpuesto != 0)
            {
                OidImpuesto = serie.OidImpuesto;
                PImpuestos = serie.PImpuesto;
            }
        }

        public virtual void SetPrice(IAcreedorInfo acreedor)
        {
            ProductInfo producto = ProductInfo.Get(OidProducto, false, true);
            BatchInfo partida = BatchInfo.Get(OidPartida, false, true);
            Precio = acreedor.GetPrecio(producto, partida, ETipoFacturacion);
            PDescuento = acreedor.GetDescuento(producto, partida);
            CalculateTotal();
        }
		public virtual void SetPrice(IAcreedorInfo acreedor, ProductInfo producto, BatchInfo partida)
		{
			Precio = acreedor.GetPrecio(producto, partida, ETipoFacturacion);
			PDescuento = acreedor.GetDescuento(producto, partida);
			CalculateTotal();
		}

        #endregion

        #region Validation Rules

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

		#region Common Factory Methods

		private InputDeliveryLine(int sessionCode, IDataReader reader, bool childs = false)
		{
			MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		internal static InputDeliveryLine GetChild(int sessionCode, IDataReader reader, bool childs = true)
		{
			return new InputDeliveryLine(sessionCode, reader, childs);
		}

		public virtual InputDeliveryLineInfo GetInfo() { return new InputDeliveryLineInfo(this); }
		public virtual InputDeliveryLineInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new InputDeliveryLineInfo(this, childs);
		}
        
		#endregion

		#region Child Factory Methods

		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public InputDeliveryLine()
        {
            MarkAsChild();
            Oid = (long)( new Random().Next());
        }
        private InputDeliveryLine(InputDeliveryLine source)
        {
            MarkAsChild();
            Fetch(source);
        }
        private InputDeliveryLine(InputDelivery parent, IDataReader reader)
        {
            MarkAsChild();
			SessionCode = parent.SessionCode;
            Fetch(reader);
        }

        //Por cada padre que tenga la clase
        public static InputDeliveryLine NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new InputDeliveryLine();
        }
        public static InputDeliveryLine NewChild(InputDelivery parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputDeliveryLine obj = new InputDeliveryLine();
            obj.CopyFrom(parent);

            return obj;
        }
		public static InputDeliveryLine NewChild(InputDelivery parent, InputDeliveryLineInfo line)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputDeliveryLine obj = new InputDeliveryLine();
			obj.OidAlbaran = parent.Oid;
			obj._base.CopyValues(line);

            return obj;
        }
        public static InputDeliveryLine NewChild(InputDelivery parent, InputDeliveryLine line)
        {
            return NewChild(parent, line.GetInfo(false));
        }
		public static InputDeliveryLine NewChild(InputDelivery parent, IDocumentLine line, ProductInfo product, decimal currencyRate)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			InputDeliveryLine obj = new InputDeliveryLine();
			obj.CopyFrom(parent, line, product, currencyRate);

			return obj;
		}

        internal static InputDeliveryLine GetChild(InputDeliveryLine source)
        {
            return new InputDeliveryLine(source);
        }
        internal static InputDeliveryLine GetChild(InputDelivery parent, IDataReader reader)
        {
            return new InputDeliveryLine(parent, reader);
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
        public override InputDeliveryLine Save()
        {
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(InputDeliveryLine source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    IDataReader reader;
                    string query = string.Empty;

					Batch.DoLOCK(Session());
                    query = Batch.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _partidas = Batchs.GetChildList(SessionCode, reader, Childs);

					Stock.DoLOCK(Session());
                    query = Stocks.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = Stocks.GetChildList(SessionCode, reader, Childs);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
                    string query;
                    IDataReader reader;

                    Batch.DoLOCK(Session());
                    query = Batch.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _partidas = Batchs.GetChildList(SessionCode, reader);

                    Stock.DoLOCK(Session());
                    query = Stocks.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_stocks = Stocks.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Insert(InputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;
#if TRACE
			ControlerBase.AppControler.Timer.Record("OutputDeliveryLine.Insert()");
#endif
            this.OidAlbaran = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
						
			parent.Session().Save(_base.Record);

            Expedient expedient = (OidExpediente != 0)
                                    ? Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode)
                                    : null;
            Batch batch = null;

            ProductInfo product = ProductInfo.Get(OidProducto, false, true);

            if (parent.Rectificativo)
            {
                if (OidPartida != 0)
                {
                    batch = ReturnFromBatch.Insert(this, parent, expedient);
                }
            }
            else
            {
                if (!product.NoStockSale)
                    batch = PurchaseAutoBatch.Insert(this, parent, expedient);
                else
                    PurchaseWithoutBatch.Insert(this, parent, expedient);
            }

            PurchaseCattle.Insert(this, parent, batch, expedient);
            PurchaseMachine.Insert(this, parent, batch, expedient);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif	
            MarkOld();
        }

        internal void Update(InputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;
#if TRACE
			ControlerBase.AppControler.Timer.Record("OutputDeliveryLine.Update()");
#endif
            this.OidAlbaran = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
            InputDeliveryLineRecord obj = Session().Get<InputDeliveryLineRecord>(Oid);

            //De esta forma, si se modifica el expediente del concepto se modificarán los gastos asociados al mismo
            /*if (obj.OidExpediente != 0 && obj.OidBatch != 0 && obj.OidExpediente != OidExpediente)
               expedient = Store.Expedient.Get(obj.OidExpediente, false, true, parent.SessionCode);*/

            obj.CopyValues(this._base.Record);
            Session().Update(obj);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Update del Concepto de Albarán");
#endif
			Batch batch = null;

            if (OidPartida > 0)
            {
                if (parent.Rectificativo)
                {
                    batch = ReturnFromBatch.Update(this, parent);
                }
                else
                {
                    Expedient expedient = Store.Expedient.Get(obj.OidExpediente, false, true, parent.SessionCode);

                    batch = PurchaseAutoBatch.Update(this, parent, expedient);

                    PurchaseCattle.Update(this, parent, batch, expedient);
                    PurchaseMachine.Update(this, parent, batch, expedient);
                }
            }

            MarkOld();
        }

        internal void DeleteSelf(InputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<InputDeliveryLineRecord>(Oid));
#if TRACE
			ControlerBase.AppControler.Timer.Record("Borrado del Concepto de Albarán");
#endif
            if (OidPartida > 0)
            {
                if (parent.Rectificativo)
                {
                    ReturnFromBatch.Delete(this, parent);
                }
                else
                {
                    PurchaseAutoBatch.Delete(this, parent);
                    PurchaseCattle.Delete(this, parent);
                    PurchaseMachine.Delete(this, parent);
                }
            }

            MarkNew();
        }

        #endregion
    }

    #region Strategies (Strategy Pattern)

    //Purchase management with batch insertion
    public static class PurchaseAutoBatch
    {
        public static void Delete(InputDeliveryLine obj, InputDelivery delivery)
        {
            if (obj.OidPartida == 0) return;
            if (obj.OidAlmacen == 0) return;

            // Eliminamos la partida y el stock asociados

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Obtención del Expediente");
#endif
            store.LoadPartidasByProducto(obj.OidProducto, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Obtención de las Partidas");
#endif
            if (store.Partidas != null && store.Partidas.Count > 0)
            {
                store.LoadStockByPartida(obj.OidPartida, true, true);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Obtención del Stock");
#endif
                store.Partidas.Remove(store, obj.OidPartida);

                //El stock se borra en el Delete de la partida
            }
        }

        public static Batch Insert(InputDeliveryLine obj, InputDelivery delivery, Expedient expedient)
        {
            if (obj.OidAlmacen == 0)
                throw new iQException(Resources.Messages.STOCK_PRODUCT_WITHOUT_STORE);

            // Agregamos la línea de stock y la partida asociados
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Expediente");
#endif
            //Cargamos las partidas del expediente para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);

            //La partida se encarga de crear la linea de stock en el almacen
            Batch batch = store.Partidas.NewItem(store, expedient, delivery, obj);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            //Necesitamos guardar para obtener el OID de la partida
            //El almacen se encarga de guardar el stock y las partidas
            //La partida se encarga de actualizar el OidPartida en el stock asociado
            store.SaveAsChild();
#if TRACE
				ControlerBase.AppControler.Timer.Record("almacen.SaveAsChild()");
#endif
            //Recargamos para obtener el OID tras el Insert
            batch = store.Partidas.GetItem(batch.Oid);

            obj.OidPartida = batch.Oid;

            if (expedient != null)
            {
                if (obj.OidPartida != 0)
                {
                    //Asociamos la partida al expediente
                    if (batch != null) batch.CopyFrom(expedient);
                }
            }

            return batch;

            // No hace falta actualizar hijos porque se encarga el expediente
        }

        public static Batch Update(InputDeliveryLine obj, InputDelivery delivery, Expedient expedient)
        {
            if (obj.OidPartida == 0) return null;

            if (obj.OidAlmacen == 0)
                throw new iQException(Resources.Messages.STOCK_PRODUCT_WITHOUT_STORE);

            // Actualizamos el stock y la partida asociada

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
			    ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            store.LoadStockByPartida(obj.OidPartida, true, true);
            if (obj.Stocks.Count != 0)
            {
                Stock stock = store.Stocks.GetItem(obj.Stocks[0].Oid);
#if TRACE
			    ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
                batch.CopyFrom(store, expedient, delivery, obj);
                stock.CopyFrom(obj);

                store.UpdateStocks(batch, true);
            }
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            // No hace falta actualizar hijos porque se encarga el almacen  

            return batch;
        }
    }

    //Purchase management with batch modification
    //Stock exits
    public static class ReturnFromBatch
    {
        public static void Delete(InputDeliveryLine obj, InputDelivery delivery)
        {
            if (obj.OidPartida == 0) return;
            if (obj.OidAlmacen == 0) return;

            /*En este caso no hay que borrar la partida sino devolver el stock.*/

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Obtención del Expediente");
#endif
            //if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Partida), true);
            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Obtención de las Partidas");
#endif
            if (store.Partidas != null && store.Partidas.Count > 0)
            {
                store.LoadStockByPartida(obj.OidPartida, true, true);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Obtención del Stock");
#endif
                if (obj.Stocks.Count != 0)
                {
                    Stock stock = store.Stocks.GetItem(obj.Stocks[0].Oid);
#if TRACE
		    ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
                    store.Stocks.Remove(obj.Stocks[0].Oid);
                    store.UpdateStocks(batch, true);
                }
            }
        }

        public static Batch Insert(InputDeliveryLine obj, InputDelivery delivery, Expedient expedient)
        {
            if (obj.OidPartida == 0)
                throw new iQException(Resources.Messages.STOCK_RETURN_WITHOUT_BATCH);

            if (obj.OidAlmacen == 0)
                throw new iQException(Resources.Messages.STOCK_PRODUCT_WITHOUT_STORE);

            // Actualizamos el stock del Almacen asociado si no es un concepto libre
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Almacen");
#endif
            //Cargamos las partidas del expediente para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            //La partida se encarga de crear la linea de stock en el almacen
            Batch batch = store.Partidas.GetItem(obj.OidPartida);

            //La partida tiene stock suficiente
            if (!batch.CheckStock(obj, null))
                throw new iQException(Resources.Messages.STOCK_INSUFICIENTE);

            //Cargamos el stock de las partidas del almacen 
            store.LoadStockByProducto(obj.OidProducto, true, true);

            //Añadimos una linea de de devolucion
            Stock stock = store.Stocks.NewItem(obj);
            stock.OidAlmacen = batch.OidAlmacen;
            stock.OidPartida = batch.Oid;
            stock.Fecha = delivery.Fecha;
            stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN_RECTIFICATIVO, delivery.Codigo);

            store.UpdateStocks(batch, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("store.UpdateStocks()");
#endif
            // No hace falta actualizar hijos porque se encarga el expediente

            return batch;
        }

        public static Batch Update(InputDeliveryLine obj, InputDelivery delivery)
        {
            if (obj.OidPartida == 0) return null;
            if (obj.OidAlmacen == 0)
                throw new iQException(Resources.Messages.STOCK_PRODUCT_WITHOUT_STORE);

            /*En este caso no hay que actualizar la partida sino el stock.*/

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
			    ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            store.LoadStockByPartida(obj.OidPartida, true, true);
            if (obj.Stocks.Count != 0)
            {
                Stock stock = store.Stocks.GetItem(obj.Stocks[0].Oid);
#if TRACE
			    ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
                stock.CopyFrom(obj);

                store.UpdateStocks(batch, true);
            }
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            // No hace falta actualizar hijos porque se encarga el almacen  

            return batch;
        }
    }

    public static class PurchaseWithoutBatch
    {
        public static void Insert(InputDeliveryLine obj, InputDelivery delivery, Expedient expedient = null)
        {
        }
    }

    public static class PurchaseMachine
    {
        public static void Delete(InputDeliveryLine obj, InputDelivery delivery)
        {
        }

        public static void Insert(InputDeliveryLine obj, InputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (batch == null) return;
            if (expedient == null || expedient.ETipoExpediente != ETipoExpediente.Maquinaria) return;

            if (expedient.Maquinarias.Count == 0) expedient.LoadChilds(typeof(Maquinaria), true, true);
#if TRACE
	ControlerBase.AppControler.Timer.Record("Carga de las Maquinas");
#endif
        }

        public static void Update(InputDeliveryLine obj, InputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (obj.OidExpediente == 0) return;
            if (obj.OidPartida == 0) return;
            if (batch == null) return;
            if (expedient == null) return;
            if (expedient.ETipoExpediente != ETipoExpediente.Maquinaria) return;

            //Asociamos la partida al expediente
            if (batch != null) batch.CopyFrom(expedient);

            if (expedient.Maquinarias.Count == 0) expedient.LoadChilds(typeof(Maquinaria), true, true);
            Maquinaria maquina = expedient.Maquinarias.GetItemByOidPartida(batch.Oid);

            if (maquina != null) maquina.Observaciones = obj.Concepto;
        }
    }

    public static class PurchaseCattle
    {
        public static void Delete(InputDeliveryLine obj, InputDelivery delivery)
        {
            if (obj.OidExpediente == 0) return;
            Expedient expediente = Store.Expedient.Get(obj.OidExpediente, false, true, delivery.SessionCode);

            if (expediente != null)
            {
                if (expediente.ETipoExpediente == ETipoExpediente.Ganado)
                {
                    LivestockBook libro = LivestockBook.Get(1, true, true, delivery.SessionCode);
                    LivestockBookLine linea = libro.Lineas.GetItemByPartidaByConceptoAlbaran(obj.OidPartida, obj.Oid, ETipoLineaLibroGanadero.Importacion);
                    if (linea != null) libro.Lineas.Remove(linea);
                }
                //Cabezas y Maquinaria se borran por integridad referencial
#if TRACE
			ControlerBase.AppControler.Timer.Record("Libro Ganadero");
#endif
            }
        }

        public static void Insert(InputDeliveryLine obj, InputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (batch == null) return;
            if (expedient == null || expedient.ETipoExpediente != ETipoExpediente.Ganado) return;

            LivestockBook libro = LivestockBook.Get(1, false, true, delivery.SessionCode);
            LivestockBookLine linea = libro.Lineas.NewItem(libro);

            linea.ETipo = ETipoLineaLibroGanadero.Importacion;
            linea.OidConceptoAlbaran = obj.Oid;
            linea.Crotal = obj.Concepto;
            linea.Causa = Resources.Labels.LIBRO_GANADERO_CAUSA_ALTA_DEFECTO;
            if (expedient.FechaDespachoDestino != DateTime.MinValue)
            {
                linea.Fecha = expedient.FechaDespachoDestino;
                linea.EEstado = EEstado.Alta;
            }
            else
            {
                linea.Fecha = delivery.Fecha;
                linea.EEstado = EEstado.Pendiente;
            }

            linea.Fecha = linea.Fecha.AddSeconds(batch.Serial);
            linea.OidPartida = batch.Oid;

            ProveedorInfo proveedor = ProveedorInfo.Get(delivery.OidAcreedor, delivery.ETipoAcreedor, false, true);
            if (proveedor != null) linea.Procedencia = proveedor.Pais;
#if TRACE
		ControlerBase.AppControler.Timer.Record("Inserción en el Libro");
#endif
        }

        public static void Update(InputDeliveryLine obj, InputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (obj.OidExpediente == 0) return;
            if (obj.OidPartida == 0) return;
            if (expedient == null) return;
            if (expedient.ETipoExpediente != ETipoExpediente.Ganado) return;

            LivestockBook libro = LivestockBook.Get(1, false, true, delivery.SessionCode);
            LivestockBookLine linea = libro.Lineas.GetItemByPartidaByConceptoAlbaran(obj.OidPartida, obj.Oid, ETipoLineaLibroGanadero.Importacion);

            if (linea != null)
            {
                linea.OidConceptoAlbaran = obj.Oid;
                linea.Crotal = obj.Concepto;
                if (expedient.FechaDespachoDestino != DateTime.MinValue)
                {
                    linea.Fecha = expedient.FechaDespachoDestino;
                    linea.EEstado = EEstado.Alta;
                }
                else
                {
                    linea.Fecha = delivery.Fecha;
                    linea.EEstado = EEstado.Pendiente;
                }

                linea.Fecha = linea.Fecha.AddSeconds(linea.Serial);

                ProveedorInfo proveedor = ProveedorInfo.Get(delivery.OidAcreedor, delivery.ETipoAcreedor, false, true);
                if (proveedor != null) linea.Procedencia = proveedor.Pais;
            }
            else
                Insert(obj, delivery, batch, expedient);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Libro");
#endif
        }
    }

    #endregion
}