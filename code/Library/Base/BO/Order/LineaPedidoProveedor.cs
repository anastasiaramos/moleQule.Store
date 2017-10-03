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

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputOrderLineRecord : RecordBase
    {
        #region Attributes

        private long _oid_pedido;
        private long _oid_producto;
        private long _oid_partida;
        private long _oid_expediente;
        private long _oid_kit;
        private bool _facturacion_bultos = false;
        private Decimal _p_impuestos;
        private Decimal _p_descuento;
        private Decimal _gastos;
        private long _estado;
        private string _concepto = string.Empty;
        private Decimal _cantidad_kilos;
        private Decimal _cantidad_bultos;
        private Decimal _precio;
        private Decimal _subtotal;
        private Decimal _total;
        private string _observaciones = string.Empty;
        private long _oid_almacen;
        private long _oid_impuesto;
        private string _codigo_producto_proveedor = string.Empty;

        #endregion

        #region Properties

        public virtual long OidPedido { get { return _oid_pedido; } set { _oid_pedido = value; } }
        public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
        public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
        public virtual bool FacturacionBultos { get { return _facturacion_bultos; } set { _facturacion_bultos = value; } }
        public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
        public virtual Decimal CantidadKilos { get { return _cantidad_kilos; } set { _cantidad_kilos = value; } }
        public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
        public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
        public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
        public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
        public virtual string CodigoProductoProveedor { get { return _codigo_producto_proveedor; } set { _codigo_producto_proveedor = value; } }

        #endregion

        #region Business Methods

        public InputOrderLineRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_pedido = Format.DataReader.GetInt64(source, "OID_PEDIDO");
            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            _oid_partida = Format.DataReader.GetInt64(source, "OID_PARTIDA");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
            _facturacion_bultos = Format.DataReader.GetBool(source, "FACTURACION_BULTOS");
            _p_impuestos = Format.DataReader.GetDecimal(source, "P_IMPUESTOS");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _gastos = Format.DataReader.GetDecimal(source, "GASTOS");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _concepto = Format.DataReader.GetString(source, "CONCEPTO");
            _cantidad_kilos = Format.DataReader.GetDecimal(source, "CANTIDAD");
            _cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
            _precio = Format.DataReader.GetDecimal(source, "PRECIO");
            _subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
            _codigo_producto_proveedor = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_PROVEEDOR");

        }

        public virtual void CopyValues(InputOrderLineRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_pedido = source.OidPedido;
            _oid_producto = source.OidProducto;
            _oid_partida = source.OidPartida;
            _oid_expediente = source.OidExpediente;
            _oid_kit = source.OidKit;
            _facturacion_bultos = source.FacturacionBultos;
            _p_impuestos = source.PImpuestos;
            _p_descuento = source.PDescuento;
            _gastos = source.Gastos;
            _estado = source.Estado;
            _concepto = source.Concepto;
            _cantidad_kilos = source.CantidadKilos;
            _cantidad_bultos = source.CantidadBultos;
            _precio = source.Precio;
            _subtotal = source.Subtotal;
            _total = source.Total;
            _observaciones = source.Observaciones;
            _oid_almacen = source.OidAlmacen;
            _oid_impuesto = source.OidImpuesto;
            _codigo_producto_proveedor = source.CodigoProductoProveedor;
        }
        #endregion
    }

    [Serializable()]
	public class InputOrderLineBase
    {
        #region Attributes

        private InputOrderLineRecord _record = new InputOrderLineRecord();
        
        internal decimal _pendiente;
        internal decimal _pendiente_bultos;
        internal long _oid_stock;
        internal string _expediente = string.Empty;
        internal string _almacen = string.Empty;

        #endregion

        #region Properties

        public InputOrderLineRecord Record { get { return _record; } set { _record = value; } }

        internal EEstado EEstado { get { return (EEstado)_record.Estado; } }
        internal string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
        internal ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
        internal bool FacturacionPeso { get { return !_record.FacturacionBultos; } }
        internal bool IsKitComponent { get { return _record.OidKit > 0; } }
        internal Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
        internal Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
        internal Decimal Impuestos { get { return Decimal.Round((_record.Subtotal * _record.PImpuestos) / 100, 2); } }
        internal bool IsComplete { get { return (ETipoFacturacion == ETipoFacturacion.Peso) ? (_pendiente == 0) : (_pendiente_bultos == 0); } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            int tipoQuery = Format.DataReader.GetInt32(source, "QUERY");

            _record.CopyValues(source);

            _pendiente = Format.DataReader.GetInt64(source, "CANTIDAD_PENDIENTE");
            _pendiente_bultos = Format.DataReader.GetInt64(source, "CANTIDAD_BULTOS_PENDIENTE");
            _oid_stock = Format.DataReader.GetInt64(source, "OID_STOCK");
            _expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
            _almacen = Format.DataReader.GetString(source, "ALMACEN");
        }
        internal void CopyValues(LineaPedidoProveedor source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _pendiente = source.Pendiente;
            _pendiente_bultos = source.PendienteBultos;
            _oid_stock = source.OidStock;
            _expediente = source.Expediente;
            _almacen = source.Almacen;
        }
        internal void CopyValues(LineaPedidoProveedorInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _pendiente = source.Pendiente;
            _pendiente_bultos = source.PendienteBultos;
            _expediente = source.Expediente;
            _almacen = source.Almacen;
        }

        #endregion
    }

    /// <summary>
    /// Editable Child Business Object
    /// </summary>	
    [Serializable()]
    public class LineaPedidoProveedor : BusinessBaseEx<LineaPedidoProveedor>
    {
        #region Attributes

		public InputOrderLineBase _base = new InputOrderLineBase();

        #endregion

        #region Properties

		public InputOrderLineBase Base { get { return _base; } }

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

        public virtual long OidPedido
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidPedido;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidPedido.Equals(value))
                {
                    _base.Record.OidPedido = value;
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
                return _base.Record.FacturacionBultos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.FacturacionBultos.Equals(value))
                {
                    _base.Record.FacturacionBultos = value;
                    Subtotal = (_base.Record.FacturacionBultos) ? _base.Record.CantidadBultos * _base.Record.Precio : _base.Record.CantidadKilos * _base.Record.Precio;
                    PropertyHasChanged();
                }
            }
        }
        public virtual decimal CantidadKilos
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
                    _base.Record.CantidadKilos = value;
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
                    Descuento = Descuento;
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
                    _base.Record.Subtotal = Decimal.Round(value, 2);
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
                return _base.Record.PImpuestos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PImpuestos.Equals(value))
                {
                    _base.Record.PImpuestos = Decimal.Round(value, 2);
                    Impuestos = Impuestos;
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
                    _base.Record.Total = Decimal.Round(value, 2);
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

        //NO ENLAZADAS
        public virtual EEstado EEstado { get { return _base.EEstado; } set { _base.Record.Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
        public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
        public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
        public virtual long OidStock { get { return _base._oid_stock; } }
        public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
        public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
        public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
        public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
        public virtual Decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
        public virtual Decimal PendienteBultos { get { return _base._pendiente_bultos; } set { _base._pendiente_bultos = value; } }
        public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
        public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
        public virtual bool IsComplete { get { return _base.IsComplete; } }


        #endregion

        #region Business Methods

        /// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual LineaPedidoProveedor CloneAsNew()
        {
            LineaPedidoProveedor clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.SessionCode = LineaPedidoProveedor.OpenSession();
            LineaPedidoProveedor.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }
        
        public virtual void CopyFrom(PedidoProveedor source)
        {
            if (source == null) return;

            OidPedido = source.Oid;
            OidAlmacen = source.OidAlmacen;
            OidExpediente = source.OidExpediente;

            Almacen = source.Almacen;
            Expediente = source.Expediente;
        }
        public virtual void CopyFrom(PedidoProveedor pedido, IAcreedorInfo acreedor, ProductInfo producto)
        {
            OidPedido = pedido.Oid;
            OidProducto = producto.Oid;
            OidKit = 0;
            OidAlmacen = pedido.OidAlmacen;
            OidExpediente = pedido.OidExpediente;
            Concepto = producto.Nombre;
            FacturacionPeso = (producto.ETipoFacturacion == ETipoFacturacion.Peso);
            CodigoProductoAcreedor = producto.CodigoArticuloAcreedor;
            Almacen = pedido.IDAlmacen;
            Expediente = pedido.Expediente;
            CantidadKilos = 1;
            CantidadBultos = 1;

            AjustaCantidad(producto);

            SetPrecio(acreedor, producto);
        }

        public virtual void AjustaCantidad(ProductInfo producto)
        {
            if (producto == null)
            {
                if (FacturacionPeso)
                    CantidadBultos = CantidadKilos;
                else
                    CantidadKilos = CantidadBultos;
            }
            else
            {
                if (FacturacionPeso)
                    CantidadBultos = (producto.KilosBulto == 0) ? CantidadKilos : CantidadKilos / producto.KilosBulto;
                else
                    CantidadKilos = (producto.KilosBulto == 0) ? CantidadBultos : CantidadBultos * producto.KilosBulto;
            }
        }

        public virtual void CalculateTotal()
        {
            Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
            Total = BaseImponible + Impuestos;
        }

        public virtual void SetPrecio(IAcreedorInfo acreedor, ProductInfo producto)
        {
            Precio = acreedor.GetPrecio(producto, null, ETipoFacturacion);
            PDescuento = acreedor.GetDescuento(producto, null);
            CalculateTotal();
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }

        #endregion

        #region Autorization Rules

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

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate.
        /// Debe ser public para que funcionen los DataGridView
        /// </summary>
        public LineaPedidoProveedor()
        {
            // Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
            // y el código que está en el DataPortal_Create debería ir aquí          
            MarkAsChild();
        }
        private LineaPedidoProveedor(LineaPedidoProveedor source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }
        private LineaPedidoProveedor(int sessionCode, IDataReader source, bool childs)
        {
            SessionCode = sessionCode;
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }

        public static LineaPedidoProveedor NewChild(PedidoProveedor parent, IAcreedorInfo acreedor, ProductInfo producto)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            LineaPedidoProveedor obj = DataPortal.Create<LineaPedidoProveedor>(new CriteriaCs(-1));
            obj.CopyFrom(parent, acreedor, producto);

            return obj;
        }

        internal static LineaPedidoProveedor GetChild(LineaPedidoProveedor source) { return new LineaPedidoProveedor(source, false); }
        internal static LineaPedidoProveedor GetChild(LineaPedidoProveedor source, bool retrieve_childs)
        {
            return new LineaPedidoProveedor(source, retrieve_childs);
        }
        internal static LineaPedidoProveedor GetChild(int sessionCode, IDataReader source) { return new LineaPedidoProveedor(sessionCode, source, false); }
        internal static LineaPedidoProveedor GetChild(int sessionCode, IDataReader source, bool childs) { return new LineaPedidoProveedor(sessionCode, source, childs); }

        public virtual LineaPedidoProveedorInfo GetInfo() { return GetInfo(true); }
        public virtual LineaPedidoProveedorInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new LineaPedidoProveedorInfo(this, childs);
        }

        #endregion

        #region Child Factory Methods

        internal static LineaPedidoProveedor NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new LineaPedidoProveedor();
        }
        internal static LineaPedidoProveedor NewChild(PedidoProveedor parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            LineaPedidoProveedor obj = new LineaPedidoProveedor();
            obj.CopyFrom(parent);
            obj.MarkAsChild();

            return obj;
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
        /// No se debe utilizar esta función para guardar. Hace falta el padre, que
        /// debe utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override LineaPedidoProveedor Save()
        {
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Common Data Access

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="criteria">Criterios de consulta</param>
        /// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            EEstado = EEstado.Abierto;
            CantidadKilos = 1;
            CantidadBultos = 1;
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        private void Fetch(LineaPedidoProveedor source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">DataReader fuente</param>
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

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(LineaPedidoProveedores parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

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

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(LineaPedidoProveedores parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                InputOrderLineRecord obj = Session().Get<InputOrderLineRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(LineaPedidoProveedores parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<InputOrderLineRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

        #region Child Data Access

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Insert(PedidoProveedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidPedido = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            MarkOld();
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Update(PedidoProveedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidPedido = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            InputOrderLineRecord obj = parent.Session().Get<InputOrderLineRecord>(Oid);
            obj.CopyValues(this._base.Record);
            parent.Session().Update(obj);

            MarkOld();
        }

        /// <summary>
        /// Borra un registro de la base de datos.
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <remarks>Borrado inmediato<remarks/>
        internal void DeleteSelf(PedidoProveedor parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<InputOrderLineRecord>(Oid));

            MarkNew();
        }

        #endregion

        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, false); }

        internal enum ETipoQuery { BASE = 0, PENDIENTE = 1 }

        internal static string SELECT_FIELDS(ETipoQuery tipoQuery)
        {
            string query;

            query = "SELECT " + (long)tipoQuery + " AS \"QUERY\"" +
                    "		,LP.*" +
                    "		,(LP.\"CANTIDAD\" - COALESCE(CAP.\"CANTIDAD\", 0)) AS \"CANTIDAD_PENDIENTE\"" +
                    "		,(LP.\"CANTIDAD_BULTOS\" - COALESCE(CAP.\"CANTIDAD_BULTOS\",0)) AS \"CANTIDAD_BULTOS_PENDIENTE\"" +
                    "       ,COALESCE(AL.\"CODIGO\", '') AS \"ALMACEN\"" +
                    "       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
			if (conditions == null) return string.Empty;

            string query = string.Empty;

            query += " WHERE TRUE";

            query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "LP");

            if (conditions.LineaPedidoProveedor != null) query += " AND LP.\"OID\" = " + conditions.ConceptoAlbaranProveedor.Oid;
            if (conditions.PedidoProveedor != null) query += " AND LP.\"OID_PEDIDO\" = " + conditions.PedidoProveedor.Oid;
            if (conditions.Producto != null) query += " AND LP.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;
            if (conditions.Almacen != null) query += " AND LP.\"OID_ALMACEN\" = " + conditions.Almacen.Oid;
            if (conditions.Expedient != null) query += " AND LP.\"OID_EXPEDIENTE\" = " + conditions.Expedient.Oid;

            return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions, ETipoQuery tipoQuery)
        {
            string lp = nHManager.Instance.GetSQLTable(typeof(InputOrderLineRecord));
            string cap = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));

            string query;

            query = SELECT_FIELDS(tipoQuery) +
                    " FROM " + lp + " AS LP" +
                    " LEFT JOIN " + al + " AS AL ON AL.\"OID\" = LP.\"OID_ALMACEN\"" +
                    " LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = LP.\"OID_EXPEDIENTE\"";

            //Pendiente de Albarán
            query += " LEFT JOIN (SELECT \"OID_LINEA_PEDIDO\"" +
                     "						,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
                     "						,SUM(\"CANTIDAD_BULTOS\") AS \"CANTIDAD_BULTOS\"" +
                     "						FROM " + cap + " AS CAP" +
                     "						WHERE CAP.\"OID_LINEA_PEDIDO\" != 0" +
                     "						GROUP BY CAP.\"OID_LINEA_PEDIDO\")" +
                     "	AS CAP ON CAP.\"OID_LINEA_PEDIDO\" = LP.\"OID\"";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            query = SELECT_BASE(conditions, ETipoQuery.BASE) +
                    WHERE(conditions);

            if (lockTable) query += " FOR UPDATE OF LP NOWAIT";

            return query;
        }

        internal static string SELECT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions { LineaPedidoProveedor = LineaPedidoProveedor.NewChild().GetInfo() };
            conditions.LineaPedidoProveedor.Oid = oid;

            return SELECT(conditions, lockTable);
        }

        internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lockTable)
        {
            string cap = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));

            string query;

            conditions.Estado = EEstado.NoAnulado;

            query = SELECT_BASE(conditions, ETipoQuery.PENDIENTE);

            query += WHERE(conditions);

            query += "	AND (LP.\"OID\", LP.\"CANTIDAD\") NOT IN (SELECT \"OID_LINEA_PEDIDO\"" +
                     "													,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
                     "												FROM " + cap + " AS CAP" +
                     "												WHERE CAP.\"OID_LINEA_PEDIDO\" != 0" +
                     "												GROUP BY CAP.\"OID_LINEA_PEDIDO\")";

            if (lockTable) query += " FOR UPDATE OF LP NOWAIT";

            return query;
        }

        #endregion
    }
}

