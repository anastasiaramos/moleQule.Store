using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using moleQule.Serie;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class BatchBase
    {
        #region Attributes

        private BatchRecord _record = new BatchRecord();

        //Campo no enlazado
        internal long _oid_albaran;
        internal string _n_albaran;
        internal string _n_factura;
        internal string _proveedor = string.Empty;
        internal string _codigo_producto = string.Empty;
        internal string _producto = string.Empty;
        private string _store = string.Empty;
        private string _store_id = string.Empty;
        private string _expedient = string.Empty;
        internal string _family = string.Empty;
        internal string _obs_expediente = string.Empty;
        internal decimal _proporcion;
        internal decimal _precio_compra_proveedor;
        internal decimal _precio_compra_producto;
        internal decimal _ayuda_kilo_estimada;
        internal long _oid_impuesto_venta;
        internal decimal _p_impuesto_venta;
        internal long _oid_serie;
        internal string _codigo_aduanero = string.Empty;
        internal decimal _stock_minimo;
        private Decimal _stock_bultos;
        private Decimal _stock_kilos;
        private long _tipo;

        #endregion

        #region Properties

        public BatchRecord Record { get { return _record; } set { _record = value; } }

        public virtual string Store { get { return _store; } set { _store = value; } }
        public virtual string StoreID { get { return _store_id; } set { _store_id = value; } }
        public virtual string Family { get { return _family; } set { _family = value; } }
        public virtual string Expedient { get { return _expedient; } set { _expedient = value; } }
        public virtual bool IsKitComponent { get { return _record.OidKit > 0; } }
        internal virtual decimal KilosPorBulto
        {
            get { return (_record.BultosIniciales > 0) ? Decimal.Round(_record.KilosIniciales / _record.BultosIniciales, 4) : _record.KilosIniciales; }
        }
        internal virtual Decimal PrecioCompraBulto
        {
            get { return _record.PrecioCompraKilo * KilosPorBulto; }
        }
        internal virtual Decimal PrecioVentaBulto
        {
            get { return _record.PrecioVentaBulto * KilosPorBulto; }
        }
        internal virtual Decimal PrecioCompraKgAyuda
        {
            get { return _record.PrecioCompraKilo - AyudaKilo; }
        }
        internal virtual Decimal PrecioCompraUdAyuda
        {
            get { return (_record.PrecioCompraKilo - AyudaKilo) * KilosPorBulto; }
        }
        internal virtual Decimal CosteKilo { get { return Decimal.Round(_record.PrecioCompraKilo + _record.GastoKilo, 5); } }
        internal virtual Decimal BeneficioKilo { get { return Decimal.Round(_record.PrecioVentaKilo + AyudaKilo - _record.PrecioCompraKilo - _record.GastoKilo); } }
        internal virtual Decimal BeneficioTotalEstimado { get { return (_record.PrecioVentaKilo + AyudaKilo - _record.PrecioCompraKilo - _record.GastoKilo) * _record.KilosIniciales; } }
        internal virtual Decimal CosteBulto { get { return CosteKilo * KilosPorBulto; } }
        internal virtual Decimal CosteTotal { get { return StockKilos * (_record.PrecioCompraKilo + _record.GastoKilo - _record.AyudaRecibidaKilo); } }
        //Coste Neto = Coste + Gasto - Ayudas
        internal virtual Decimal CosteNetoKg { get { return CosteKilo - AyudaKilo; } }
        internal virtual Decimal CosteNetoUd { get { return CosteNetoKg * KilosPorBulto; } }
        internal virtual Decimal CosteNeto { get { return CosteNetoKg * _record.KilosIniciales; } }
        internal virtual Decimal ValoracionTotal { get { return StockKilos * _record.PrecioVentaKilo; } }
        internal virtual Decimal AyudaKilo { get { return (_record.Ayuda) ? ((_record.AyudaRecibidaKilo != 0) ? _record.AyudaRecibidaKilo : _ayuda_kilo_estimada) : 0; } set { _ayuda_kilo_estimada = value; } }
        internal virtual decimal StockMinimo { get { return _stock_minimo; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual Decimal StockBultos { get { return _stock_bultos; } set { _stock_bultos = value; } }
        public virtual Decimal StockKilos { get { return _stock_kilos; } set { _stock_kilos = value; } }        

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            long tipo = Format.DataReader.GetInt64(source, "TIPO");

            switch ((Batch.ETipoQuery)tipo)
            {
                case Batch.ETipoQuery.GENERAL:

                    _record.CopyValues(source);

                    _oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
                    _n_albaran = Format.DataReader.GetString(source, "N_ALBARAN");
                    _n_factura = Format.DataReader.GetString(source, "N_FACTURA");
                    _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
                    _codigo_producto = Format.DataReader.GetString(source, "CODIGO_PRODUCTO");
                    _producto = Format.DataReader.GetString(source, "PRODUCTO");
                    _expedient = Format.DataReader.GetString(source, "EXPEDIENTE");
                    _obs_expediente = Format.DataReader.GetString(source, "OBS_EXPEDIENTE");
                    _oid_impuesto_venta = Format.DataReader.GetInt64(source, "OID_IMPUESTO_VENTA");
                    _p_impuesto_venta = Format.DataReader.GetDecimal(source, "P_IMPUESTO_VENTA");
                    _store = Format.DataReader.GetString(source, "ALMACEN");
                    _store_id = Format.DataReader.GetString(source, "STORE_ID");
                    _codigo_aduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");
                    _tipo = Format.DataReader.GetInt64(source, "TIPO");
                    _stock_bultos = Format.DataReader.GetDecimal(source, "STOCK_B");
                    _stock_kilos = Format.DataReader.GetDecimal(source, "STOCK_K");

                    _family = Format.DataReader.GetString(source, "FAMILY");
                    _precio_compra_producto = Format.DataReader.GetDecimal(source, "PRECIO_PRODUCTO");
                    _precio_compra_proveedor = Format.DataReader.GetDecimal(source, "PRECIO_PROVEEDOR");
                    _ayuda_kilo_estimada = Format.DataReader.GetDecimal(source, "AYUDA_ESTIMADA_PRODUCTO");
                    _stock_minimo = Format.DataReader.GetDecimal(source, "STOCK_MINIMO");

                    break;

                case Batch.ETipoQuery.EXPEDIENTE:

                    _record.CopyValues(source);

                    _oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
                    _n_albaran = Format.DataReader.GetString(source, "N_ALBARAN");
                    _n_factura = Format.DataReader.GetString(source, "N_FACTURA");
                    _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
                    _codigo_producto = Format.DataReader.GetString(source, "CODIGO_PRODUCTO");
                    _producto = Format.DataReader.GetString(source, "PRODUCTO");
                    _expedient = Format.DataReader.GetString(source, "EXPEDIENTE");
                    _obs_expediente = Format.DataReader.GetString(source, "OBS_EXPEDIENTE");
                    _oid_impuesto_venta = Format.DataReader.GetInt64(source, "OID_IMPUESTO_VENTA");
                    _p_impuesto_venta = Format.DataReader.GetDecimal(source, "P_IMPUESTO_VENTA");
                    _tipo = Format.DataReader.GetInt64(source, "TIPO");
                    _stock_bultos = Format.DataReader.GetDecimal(source, "STOCK_B");
                    _stock_kilos = Format.DataReader.GetDecimal(source, "STOCK_K");

                    _family = Format.DataReader.GetString(source, "FAMILY");
                    _precio_compra_producto = Format.DataReader.GetDecimal(source, "PRECIO_PRODUCTO");
                    _precio_compra_proveedor = Format.DataReader.GetDecimal(source, "PRECIO_PROVEEDOR");
                    _ayuda_kilo_estimada = Format.DataReader.GetDecimal(source, "AYUDA_ESTIMADA_PRODUCTO");
                    _stock_minimo = Format.DataReader.GetDecimal(source, "STOCK_MINIMO");

                    break;

                case Batch.ETipoQuery.CLUSTERED:

                    _record.Oid = Format.DataReader.GetInt64(source, "OID");
                    //_serial = Format.DataReader.GetInt64(source, "SERIAL");
                    //_codigo = Format.DataReader.GetString(source, "CODIGO");
                    _record.TipoMercancia = Format.DataReader.GetString(source, "TIPO_MERCANCIA");
                    //_precio_venta_bulto = Format.DataReader.GetDecimal(source, "PRECIO_VENTA_BULTO");
                    _record.PrecioVentaKilo = Format.DataReader.GetDecimal(source, "PRECIO_VENTA_KILO");
                    _stock_bultos = Format.DataReader.GetDecimal(source, "STOCK_B");
                    _stock_kilos = Format.DataReader.GetDecimal(source, "STOCK_K");
                    _record.BultosIniciales = Format.DataReader.GetDecimal(source, "BULTOS_INICIALES");
                    _record.KilosIniciales = Format.DataReader.GetDecimal(source, "KILOS_INICIALES");
                    _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
                    _expedient = Format.DataReader.GetString(source, "EXPEDIENTE");
                    //_almacen = Format.DataReader.GetString(source, "ALMACEN");
                    _stock_minimo = Format.DataReader.GetDecimal(source, "STOCK_MINIMO");

                    break;
            }
        }
        internal void CopyValues(Batch source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _store = source.Almacen;
            _store_id = source.IDAlmacen;
            _oid_albaran = source.OidAlbaran;
            _n_albaran = source.NAlbaran;
            _proveedor = source.Proveedor;
            _producto = source.Producto;
            _codigo_producto = source.CodigoProducto;
            _expedient = source.Expediente;
            _obs_expediente = source.Observaciones;
            _oid_impuesto_venta = source.OidImpuestoVenta;
            _p_impuesto_venta = source.PImpuestoVenta;
            _codigo_aduanero = source.CodigoAduanero;
            _stock_minimo = source.StockMinimo;
            _stock_bultos = source.StockBultos;
            _stock_kilos = source.StockKilos;
            _tipo = source.Tipo;
            _family = source.Family;
        }
        internal void CopyValues(BatchInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _store = source.Almacen;
            _store_id = source.IDAlmacen;
            _oid_albaran = source.OidAlbaran;
            _n_albaran = source.NAlbaran;
            _proveedor = source.Proveedor;
            _producto = source.Producto;
            _codigo_producto = source.CodigoProducto;
            _expedient = source.Expediente;
            _obs_expediente = source.Observaciones;
            _oid_impuesto_venta = source.OidImpuestoVenta;
            _p_impuesto_venta = source.PImpuestoVenta;
            _codigo_aduanero = source.CodigoAduanero;
            _stock_minimo = source.StockMinimo;
            _stock_bultos = source.StockBultos;
            _stock_kilos = source.StockKilos;
            _tipo = source.Tipo;
            _family = source.Family;
        }

        #endregion
    }

    /// <summary>
    /// Editable Child Business Object
    /// </summary>
    [Serializable()]
    public class Batch : BusinessBaseEx<Batch>
    {
        #region Attributes

        public BatchBase _base = new BatchBase();

		private Maquinaria _machine = null;

        private Batchs _componentes = Batchs.NewChildList();

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
        public virtual long OidProveedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidProveedor;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidProveedor.Equals(value))
                {
                    _base.Record.OidProveedor = value;
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
        public virtual string Codigo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Codigo;
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
        public virtual long Tipo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Tipo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Tipo.Equals(value))
                {
                    _base.Tipo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaCompra
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaCompra;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaCompra.Equals(value))
                {
                    _base.Record.FechaCompra = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string TipoMercancia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoMercancia;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.TipoMercancia.Equals(value))
                {
                    _base.Record.TipoMercancia = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal BultosIniciales
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.BultosIniciales;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.BultosIniciales.Equals(value))
                {
                    _base.Record.BultosIniciales = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal KilosIniciales
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.KilosIniciales;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.KilosIniciales.Equals(value))
                {
                    _base.Record.KilosIniciales = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal StockBultos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.StockBultos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.StockBultos.Equals(value))
                {
                    _base.StockBultos = Decimal.Round(value, 4);
                    //PropertyHasChanged();
                }
            }
        }
        public virtual Decimal StockKilos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.StockKilos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.StockKilos.Equals(value))
                {
                    _base.StockKilos = value;
                    //PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PrecioCompraKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PrecioCompraKilo;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PrecioCompraKilo.Equals(value))
                {
                    _base.Record.PrecioCompraKilo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PrecioVentaKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PrecioVentaKilo;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PrecioVentaKilo.Equals(value))
                {
                    _base.Record.PrecioVentaKilo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PrecioVentaBulto
        {
            get { return _base.PrecioVentaBulto; }
        }
        public virtual Decimal BeneficioKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.BeneficioKilo;
            }
            //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            //set
            //{
            //    //CanWriteProperty(true);
            //    if (!_base._beneficio_kilo.Equals(value))
            //    {
            //        _base._beneficio_kilo = value;
            //        PropertyHasChanged();
            //    }
            //}
        }
        //Precio del proveedor + Gasto por kilo
        public virtual Decimal CosteKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.CosteKilo;
            }
            //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            //set
            //{
            //    //CanWriteProperty(true);
            //    if (!_base._coste_kilo.Equals(value))
            //    {
            //        _base._coste_kilo = value;
            //        PropertyHasChanged();
            //    }
            //}
        }
        //Parte proporcional de los gastos del contenedor por kilo
        public virtual Decimal GastoKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GastoKilo;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.GastoKilo.Equals(value))
                {
                    _base.Record.GastoKilo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime ReaFechaCobro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.ReaFechaCobro;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.ReaFechaCobro.Equals(value))
                {
                    _base.Record.ReaFechaCobro = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool ReaCobrada
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.ReaCobrada;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.ReaCobrada.Equals(value))
                {
                    _base.Record.ReaCobrada = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Ayuda
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Ayuda;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Ayuda.Equals(value))
                {
                    _base.Record.Ayuda = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal AyudaRecibidaKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.AyudaRecibidaKilo;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.AyudaRecibidaKilo.Equals(value))
                {
                    _base.Record.AyudaRecibidaKilo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Proporcion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base._proporcion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base._proporcion.Equals(value))
                {
                    _base._proporcion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Ubicacion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Ubicacion;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.Ubicacion.Equals(value))
                {
                    _base.Record.Ubicacion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string ObservacionesPE
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

        public virtual Batchs Componentes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _componentes;
            }
        }

        //Campo no enlazado
        public virtual ETipoPartida ETipoPartida { get { return (ETipoPartida)_base.Tipo; } set { _base.Tipo = (long)value; } }
        public virtual string TipoPartidaLabel { get { return moleQule.Store.Structs.EnumText<ETipoPartida>.GetLabel(ETipoPartida); } }
        public virtual long OidAlbaran { get { return _base._oid_albaran; } set { _base._oid_albaran = value; } }
        public virtual string NAlbaran { get { return _base._n_albaran; } set { _base._n_albaran = value; } }
        public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
        public virtual string Proveedor { get { return _base._proveedor; } set { _base._proveedor = value; } }
        public virtual string CodigoProducto { get { return _base._codigo_producto; } set { _base._codigo_producto = value; } }
        public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
        public virtual string Family { get { return _base.Family; } set { _base.Family = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Expediente { get { return _base.Expedient; } set { _base.Expedient = value; } }
        public virtual string Observaciones { get { return _base._obs_expediente; } set { _base._obs_expediente = value; } }
        public virtual decimal KilosPorBulto { get { return _base.KilosPorBulto; } }
        public virtual decimal PrecioCompraProducto
        {
            get
            {
                return _base._precio_compra_producto;
            }
            set
            {
                if (!_base._precio_compra_producto.Equals(value))
                {
                    _base._precio_compra_producto = value;
                }
            }
        }
        public virtual decimal PrecioCompraProveedor
        {
            get
            {
                return _base._precio_compra_proveedor;
            }
            set
            {
                if (!_base._precio_compra_proveedor.Equals(value))
                {
                    _base._precio_compra_proveedor = value;
                }
            }
        }
        public virtual Decimal PrecioCompraBulto { get { return _base.PrecioCompraBulto; } }
        public virtual Decimal PrecioCompraKgAyuda { get { return _base.PrecioCompraKgAyuda; } }
        public virtual Decimal AyudaKiloEstimada { get { return _base._ayuda_kilo_estimada; } set { _base._ayuda_kilo_estimada = value; } }
        public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } set { _base._ayuda_kilo_estimada = value; } }
        public virtual Decimal BeneficioTotalEstimado { get { return _base.BeneficioTotalEstimado; } }
        public virtual Decimal CosteBulto { get { return _base.CosteBulto; } }
        //Coste Neto = Coste + Gasto - Ayudas
        public virtual Decimal CosteNetoKg { get { return _base.CosteNetoKg; } }
        public virtual Decimal CosteNetoUd { get { return _base.CosteNetoUd; } }
        public virtual Decimal CosteNeto { get { return _base.CosteNeto; } }
        public virtual Decimal KilosMezcla
        {
            get
            {
                decimal value = 0;
                foreach (Batch item in Componentes)
                    value += item.KilosIniciales;
                return value;
            }
        }
        public virtual bool IsKit { get { return Componentes != null ? Componentes.Count > 0 : false; } }
        public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
        public virtual long OidImpuestoVenta { get { return _base._oid_impuesto_venta; } }
        public virtual decimal PImpuestoVenta { get { return _base._p_impuesto_venta; } }
        public virtual long OidSerie { get { return _base._oid_serie; } set { _base._oid_serie = value; } }
        public virtual string CodigoAduanero { get { return _base._codigo_aduanero; } set { _base._codigo_aduanero = value; } }
        public virtual decimal CosteTotal { get { return _base.CosteTotal; } }
        public virtual decimal ValoracionTotal { get { return _base.ValoracionTotal; } }
        public virtual decimal StockMinimo { get { return _base._stock_minimo; } }

		//To store the associated machine for updating the OidPartida in the Insert action
		public Maquinaria Machine { get { return _machine; } set { _machine = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                       && _componentes.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                       || _componentes.IsDirty;
            }
        }

        #endregion

        #region Business Methods
        
        public void CopyFrom(ProductoProveedorInfo producto_prov, Expedient expediente, bool throwStockException)
        {
            if (producto_prov == null) return;
            if (expediente == null) return;

            FechaCompra = expediente.FechaFacProveedor;

            OidProveedor = producto_prov.OidAcreedor;
            OidProducto = producto_prov.OidProducto;
            StockBultos = BultosIniciales;
            StockKilos = KilosIniciales;
            Proveedor = producto_prov.Acreedor;

            UpdateStock(expediente, throwStockException);

            PrecioCompraProducto = producto_prov.PrecioCompra;
            PrecioCompraProveedor = producto_prov.Precio;
        }
        public void CopyFrom(Maquinaria maquina, ProductoProveedorInfo producto_prov, Expedient expediente, bool throwStockException)
        {
            if (maquina == null) return;
            if (producto_prov == null) return;

            FechaCompra = expediente.FechaFacProveedor;

            OidProducto = producto_prov.OidProducto;
            OidProveedor = producto_prov.OidAcreedor;
            TipoMercancia = producto_prov.Producto + " - ID: " + maquina.Identificador;
            KilosIniciales = 1;
            BultosIniciales = 1;
            StockKilos = 1;
            StockBultos = 1;
            PrecioCompraKilo = producto_prov.Precio;
            PrecioVentaKilo = producto_prov.PrecioVenta;
            //PrecioVentaBulto = PrecioVentaKilo;
            AyudaRecibidaKilo = producto_prov.Ayuda;

            CalculaCostes(expediente.GastoPorKilo, 0);

            maquina.OidPartida = Oid;
            maquina.Observaciones = producto_prov.Producto;
            maquina.PrecioCompra = PrecioCompraKilo;
            maquina.PrecioVenta = PrecioVentaKilo;
            maquina.Ayuda = AyudaRecibidaKilo;
            maquina.Coste = CosteKilo;
            maquina.Stock = StockKilos;

            UpdateStock(expediente, throwStockException);

            PrecioCompraProducto = producto_prov.PrecioCompra;
            PrecioCompraProveedor = producto_prov.Precio;
        }
        public void CopyFrom(Product producto)
        {
            if (producto == null) return;

            FechaCompra = DateTime.Today;

            OidProveedor = 0;
            OidProducto = producto.Oid;
            OidPartida = 0;
            TipoMercancia = producto.Nombre;
            StockBultos = 1;
            StockKilos = KilosIniciales;
            PrecioVentaKilo = producto.PrecioVenta;
            KilosIniciales = KilosIniciales;
            BultosIniciales = 1;
            AyudaRecibidaKilo = producto.AyudaKilo;
            AyudaKiloEstimada = producto.AyudaKilo;

            Producto = producto.Nombre;
            CodigoProducto = producto.Codigo;
        }
        public void CopyFrom(Batch source)
        {
            if (source == null) return;

            FechaCompra = source.FechaCompra;

            //Oid de la partida de la que proviene
            OidPartida = source.Oid;

            OidAlmacen = source.OidAlmacen;
            OidExpediente = source.OidExpediente;
            OidProveedor = source.OidProveedor;
            OidProducto = source.OidProducto;
            TipoMercancia = source.TipoMercancia;
            PrecioCompraKilo = source.PrecioCompraKilo;
            PrecioVentaKilo = source.PrecioVentaKilo;
            //PrecioVentaBulto = source.PrecioVentaBulto;
            //CosteKilo = source.CosteKilo;
            //BeneficioKilo = source.BeneficioKilo;
            AyudaRecibidaKilo = source.AyudaRecibidaKilo;
            ObservacionesPE = source.ObservacionesPE;
            Ubicacion = source.Ubicacion;
            Ayuda = source.Ayuda;

            AyudaKiloEstimada = source.AyudaRecibidaKilo;
            Proveedor = source.Proveedor;
            CodigoProducto = source.CodigoProducto;
            Producto = source.Producto;
            CodigoAduanero = source.CodigoAduanero;
        }
        public void CopyFrom(BatchInfo source)
        {
            if (source == null) return;

            FechaCompra = source.FechaCompra;

            //Oid del producto_expediente del que proviene
            OidPartida = source.Oid;

            OidAlmacen = source.OidAlmacen;
            OidExpediente = source.OidExpediente;
            OidProveedor = source.OidProveedor;
            OidProducto = source.OidProducto;
            TipoMercancia = source.TipoMercancia;
            PrecioCompraKilo = source.PrecioCompraKilo;
            PrecioVentaKilo = source.PrecioVentaKilo;
            //PrecioVentaBulto = source.PrecioVentaBulto;
            //CosteKilo = source.CosteKilo;
            //BeneficioKilo = source.BeneficioKilo;
            AyudaRecibidaKilo = source.AyudaRecibidaKilo;
            ObservacionesPE = source.ObservacionesPE;
            Ubicacion = source.Ubicacion;

            Proveedor = source.Proveedor;
            CodigoProducto = source.CodigoProducto;
            Producto = source.Producto;
            CodigoAduanero = source.CodigoAduanero;
        }
        public void CopyFrom(Almacen parent, Expedient expediente, InputDelivery albaran, InputDeliveryLine source)
        {
            if (parent == null) return;
            if (source == null) return;

            OidAlmacen = parent.Oid;
            OidProveedor = albaran.OidAcreedor;
            OidProducto = source.OidProducto;
            Almacen = parent.Nombre;

            FechaCompra = albaran.Fecha;
            TipoMercancia = source.Concepto;
            PrecioCompraKilo = source.Precio;
            KilosIniciales = source.CantidadKilos;
            BultosIniciales = source.CantidadBultos;
            AyudaRecibidaKilo = source.AyudaKilo;
            AyudaKiloEstimada = source.AyudaKilo;
            Ubicacion = source.Ubicacion;
            ObservacionesPE = String.Format(Resources.Messages.ENTRADA_POR_ALBARAN, albaran.NumeroAlbaran);

            ProductInfo pr = ProductInfo.Get(OidProducto, false, true);
            PrecioVentaKilo = pr.PrecioVenta;
            CodigoProducto = pr.Codigo;
            Producto = pr.Nombre;

            OidSerie = albaran.OidSerie;

            if (expediente != null) CopyFrom(expediente);
        }
        public void CopyFrom(Batch partida, Stock stock, Expedient expediente, ETipoStock tipo)
        {
            if (partida == null) return;
            if (stock == null) return;
            if (expediente == null) return;

            CopyFrom(partida);

            OidAlmacen = partida.OidAlmacen;
            OidAlbaran = partida.OidAlbaran;
            OidExpediente = expediente.Oid;
            FechaCompra = stock.Fecha;
            PrecioCompraKilo = partida.CosteKilo;
            KilosIniciales = Math.Abs(stock.Kilos);
            BultosIniciales = Math.Abs(stock.Bultos);
            StockKilos = KilosIniciales;
            StockBultos = BultosIniciales;
            AyudaRecibidaKilo = partida.AyudaRecibidaKilo;
            AyudaKiloEstimada = partida.AyudaKiloEstimada;
            ObservacionesPE = String.Format(Resources.Messages.ENTRADA_POR_MOVIMIENTO, expediente.Codigo);

            PrecioVentaKilo = partida.PrecioVentaKilo;
            CodigoProducto = partida.CodigoProducto;
            Producto = partida.Producto;

            OidSerie = partida.OidSerie;

            Almacen = partida.Almacen;
            Expediente = expediente.Codigo;
        }
        public void CopyFrom(Expedient source)
        {
            if (source == null) return;

            OidExpediente = source.Oid;
            Expediente = source.Codigo;
            Ayuda = source.Ayuda;

            ProductInfo producto = ProductInfo.Get(OidProducto, false, true);
            AyudaKiloEstimada = producto.AyudaKilo;

            CalculaCostes(source.GastoPorKilo, 0);
        }

        public void GetNewCode(long oidProduct)
        {
			Serial = BatchSerialInfo.GetNext(oidProduct, FechaCompra.Year);
            Codigo = Serial.ToString(Resources.Defaults.PARTIDA_CODE_FORMAT) + "/" + FechaCompra.Year.ToString().Substring(2,2);
        }

        public void CalculaValores(Expedient ex, ProductInfo pi, ProductoProveedorInfo ppi)
        {
            TipoMercancia = ppi.Producto;
            PrecioCompraKilo = ppi.Precio;
            PrecioVentaKilo = pi.PrecioVenta;
            AyudaKiloEstimada = pi.AyudaKilo;

            CalculaCostes(ex.GastoPorKilo, 0);
        }

        public void CalculaCostes(decimal gastoPorKilo, decimal ayudaKilo)
        {
            GastoKilo = gastoPorKilo;
            AyudaRecibidaKilo = ayudaKilo;

            ///if (KilosIniciales > 0)
            //{
                //CosteKilo = PrecioCompraKilo + GastoKilo;
                //BeneficioKilo = PrecioVentaKilo + AyudaKilo - PrecioCompraKilo - GastoKilo;
            //}
            //else
            //{
                //CosteKilo = 0;
                //BeneficioKilo = 0;
            //}

            //PrecioVentaBulto = PrecioVentaKilo * KilosPorBulto;
        }

        public void CalculaResumen(decimal totalGastos)
        {
            if (KilosIniciales > 0)
            {
                GastoKilo = Decimal.Round(totalGastos / KilosIniciales, 5);
                //CosteKilo = Decimal.Round(PrecioCompraKilo + (totalGastos / KilosIniciales), 5);
                //BeneficioKilo = PrecioVentaKilo - CosteKilo + AyudaRecibidaKilo;
            }
            //else
            //{
                //CosteKilo = 0;
                //BeneficioKilo = 0;
            //}
        }

        public bool CheckStock(IStockable source) { return CheckStock(source, null, null); }
        public bool CheckStock(IStockable source, Stock stockReserva) { return CheckStock(source, null, stockReserva); }
        public bool CheckStock(IStockable source, Stock stock, Stock stockReserva)
        {
            switch (source.EEntityType)
            {
                case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:
                    return CheckStockOutputDeliveryLine(source, stock, stockReserva);

                case moleQule.Common.Structs.ETipoEntidad.OutputOrderLine:
                    return CheckStockOrderLine(source, stock);

                case moleQule.Common.Structs.ETipoEntidad.InputDeliveryLine:
                    return CheckStockInputDeliveryLine(source, stock, stockReserva);

                default:
                    throw new iQImplementationException(string.Format("CheckStock para {0}", source.EEntityType.ToString()));
            }
        }
        private bool CheckStockOrderLine(IStockable source, Stock stock)
        {
            decimal stockBultos = (stock != null) ? stock.Bultos : 0;
            decimal stockKilos = (stock != null) ? stock.Kilos : 0;

            //Si es un componente de kit no controlamos el stock porque ya lo hace el 
            //concepto asociado al kit
            if (!IsKitComponent)
            {
                if (source.InvoicingMode == ETipoFacturacion.Peso)
                {
                    if (StockKilos - stockKilos - source.Kilos < 0)
                        throw new Exception("No hay suficientes kg para vender.");
                    else if (source.Kilos == (StockKilos - stockKilos))
                        source.Pieces = StockBultos;
                }
                else
                {
                    //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                    if (StockBultos - stockBultos - source.Pieces < 0)
                        throw new Exception("No hay suficientes bultos para vender.");
                    else if (source.Pieces == (StockBultos - stockBultos))
                        source.Kilos = StockBultos;
                }
            }

            return true;
        }
        private bool CheckStockOutputDeliveryLine(IStockable source, Stock stock, Stock stockReserva)
        {
            decimal stockBultos = (stock != null) ? stock.Bultos : 0;
            decimal stockKilos = (stock != null) ? stock.Kilos : 0;
            decimal stockReservaBultos = (stockReserva != null) ? stockReserva.Bultos : 0;
            decimal stockReservaKilos = (stockReserva != null) ? stockReserva.Kilos : 0;

            //Si es un componente de kit no controlamos el stock porque ya lo hace el 
            //concepto asociado al kit
            if (!IsKitComponent)
            {
                if (source.InvoicingMode == ETipoFacturacion.Peso)
                {
                    if (StockKilos - stockKilos - stockReservaKilos - source.Kilos < 0)
                        return false;
                }
                else
                {
                    //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                    if (StockBultos - stockBultos - stockReservaBultos - source.Pieces < 0)
                        return false;
                }
            }
            return true;
        }
        private bool CheckStockInputDeliveryLine(IStockable source, Stock stock, Stock stockReserva)
        {
            decimal stockBultos = (stock != null) ? stock.Bultos : 0;
            decimal stockKilos = (stock != null) ? stock.Kilos : 0;
            decimal stockReservaBultos = (stockReserva != null) ? stockReserva.Bultos : 0;
            decimal stockReservaKilos = (stockReserva != null) ? stockReserva.Kilos : 0;

            //Si es un componente de kit no controlamos el stock porque ya lo hace el 
            //concepto asociado al kit
            if (!IsKitComponent)
            {
                if (source.InvoicingMode == ETipoFacturacion.Peso)
                {
                    if (StockKilos - stockKilos - stockReservaKilos - source.Kilos < 0)
                        return false;
                }
                else
                {
                    //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                    if (StockBultos - stockBultos - stockReservaBultos - source.Pieces < 0)
                        return false;
                }
            }
            return true;
        }

        public decimal ExtractStock(IStockable source, Stock stock, Stock stockReserva)
        {
            switch (source.EEntityType)
            {
                case moleQule.Common.Structs.ETipoEntidad.OutputDeliveryLine:
                    return ExtractStockOutputDeliveryLine(source, stock, stockReserva);

                case moleQule.Common.Structs.ETipoEntidad.InputDeliveryLine:
                    return ExtractStockInputDeliveryLine(source, stock, stockReserva);

                default:
                    throw new iQImplementationException(string.Format("ExtractStock para {0}", source.EEntityType.ToString()));
            }
        }
        private decimal ExtractStockOutputDeliveryLine(IStockable source, Stock stock, Stock stockReserva)
        {
            decimal stockBultos = (stock != null) ? stock.Bultos : 0;
            decimal stockKilos = (stock != null) ? stock.Kilos : 0;
            decimal stockReservaBultos = (stockReserva != null) ? stockReserva.Bultos : 0;
            decimal stockReservaKilos = (stockReserva != null) ? stockReserva.Kilos : 0;

            if (source.InvoicingMode == ETipoFacturacion.Peso)
            {
                if (StockKilos - stockKilos - stockReservaKilos - source.Kilos < 0)
                    return StockKilos;
                else
                    return source.Pieces;
            }
            else
            {
                //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                if (StockBultos - stockBultos - stockReservaBultos - source.Pieces < 0)
                    return StockBultos;
                else
                    return source.Pieces;
            }
        }
        private decimal ExtractStockInputDeliveryLine(IStockable source, Stock stock, Stock stockReserva)
        {
            decimal stockBultos = (stock != null) ? stock.Bultos : 0;
            decimal stockKilos = (stock != null) ? stock.Kilos : 0;
            decimal stockReservaBultos = (stockReserva != null) ? stockReserva.Bultos : 0;
            decimal stockReservaKilos = (stockReserva != null) ? stockReserva.Kilos : 0;

            if (source.InvoicingMode == ETipoFacturacion.Peso)
            {
                if (StockKilos - stockKilos - stockReservaKilos - source.Kilos < 0)
                    return StockKilos;
                else
                    return source.Kilos;
            }
            else
            {
                //stock.Bultos es negativo por eso se resta para calcular si hay suficiente
                if (StockBultos - stockBultos - stockReservaBultos - source.Pieces < 0)
                    return StockBultos;
                else
                    return source.Pieces;
            }
        }

        //Actualiza el stock inicial asociado a este producto
        private void UpdateStock(Expedient parent, bool throwStockException)
        {
            // Actualizamos el stock del Expediente asociado
            Stock stock = parent.Stocks.GetInitialStock(Oid);
            if (stock == null) return;

            stock.CopyFrom(this, stock.ETipoStock);
            parent.UpdateStocks(this, throwStockException);
        }
        private void UpdateStock(Almacen parent, bool throwStockException)
        {
            // Actualizamos el stock del Expediente asociado
            Stock stock = parent.Stocks.GetInitialStock(Oid);
            if (stock == null) return;

            stock.CopyFrom(this, stock.ETipoStock);
            parent.UpdateStocks(this, throwStockException);
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //KilosIniciales
            if (KilosIniciales < 1)
            {
                e.Description = Resources.Messages.NO_KILOS_INICIALES;
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

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public Batch()
        {
            _base.Record.Oid = (long)(new Random()).Next();
            _componentes = Batchs.NewChildList();
        }
        private Batch(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            SessionCode = sessionCode;
            Fetch(source);
        }

        internal static Batch GetChild(int sessionCode, IDataReader source, bool childs) { return new Batch(sessionCode, source, childs); }

        public virtual BatchInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new BatchInfo(this, childs);
        }

        #endregion

        #region Child Factory Methods

        private Batch(Batch source)
        {
            MarkAsChild();
            Fetch(source);
        }

        public static Batch NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();

            return obj;
        }
        public static Batch NewChild(Almacen parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.OidAlmacen = parent.Oid;

            return obj;
        }
        public static Batch NewChild(Expedient parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.OidExpediente = parent.Oid;

            return obj;
        }
        public static Batch NewChild(Product parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.OidProducto = parent.Oid;
            //Lo asignamos al almacén
            obj.OidExpediente = ExpedientInfo.GetAlmacen(false).Oid;

            return obj;
        }
        public static Batch NewChild(Batch parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.OidKit = parent.Oid;
            obj.OidProducto = 0;
            obj.OidExpediente = parent.OidExpediente;
            obj.ETipoPartida = ETipoPartida.Componente;

            parent.ETipoPartida = ETipoPartida.Kit;

            return obj;
        }
        public static Batch NewChild(Almacen parent, Expedient expediente, InputDelivery albaran, InputDeliveryLine source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.CopyFrom(parent, expediente, albaran, source);

            return obj;
        }
        public static Batch NewChild(Batch partida, Stock stock, Expedient expediente, ETipoStock tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.CopyFrom(partida, stock, expediente, tipo);
            obj.GetNewCode(partida.OidProducto);
            return obj;
        }

        /// <summary>
        /// Crea un nuevo ProductoCliente a partir de un ProductoInfo
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        // Creado para que no tengamos que acceder a la base de datos continuamente,
        // para obtener el Oid del producto.
        public static Batch NewChild(ProductInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Batch obj = new Batch();
            obj.MarkAsChild();
            obj.OidProducto = parent.Oid;

            return obj;
        }

        internal static Batch GetChild(Batch source) { return new Batch(source); }

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
        public override Batch Save()
        {
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(Batch source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    string query;
                    IDataReader reader;

                    //Obtenemos los componentes del kit
                    if (ETipoPartida == ETipoPartida.Kit)
                    {
                        Batch.DoLOCK(Session());
                        query = Batchs.SELECT_BY_KIT(Oid);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _componentes = Batchs.GetChildList(SessionCode, reader, Childs);
                    }
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

                    //Obtenemos los componentes del kit
                    if (ETipoPartida == ETipoPartida.Kit)
                    {
                        Batch.DoLOCK(Session());
                        query = Batchs.SELECT_BY_KIT(Oid);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _componentes = Batchs.GetChildList(SessionCode, reader, Childs);
                    }
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Insert(Almacen parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidAlmacen = parent.Oid;

            try
            {				
                Stock stock = parent.Stocks.GetInitialStock(Oid);

                parent.Session().Save(_base.Record);

                //Actualizamos el OidPartida en la maquina asociada
                if (_machine != null)
                {
                    _machine.OidPartida = Oid;
                }

				// Actualizamos el OidPartida de la linea de stock asociada
                if (stock != null)
                {
                    stock.OidPartida = Oid;
                }
                else
                {
                    throw new iQException("Partida::Insert(): No se ha encontrado la línea de stock asociada a este producto");
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }
        internal void Insert(Expedient parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidExpediente = parent.Oid;

            try
            {
                long oldOid = Oid;
                //Cabeza cabeza = parent.Cabezas.GetItem(new FCriteria<long>("OidPartida", _oid));
                Maquinaria maquina = parent.Maquinarias.GetItem(new FCriteria<long>("OidPartida", Oid));
                Stock stock = parent.Stocks.GetInitialStock(Oid);

                parent.Session().Save(_base.Record);

                //Actualizamos el OID del Producto_Expediente en la Cabeza
                /*if (cabeza != null)
                {
                    cabeza.OidPartida = Oid;
                }*/

                //Actualizamos el OID del Producto_Expediente en la Maquina
                if (maquina != null)
                {
                    maquina.OidPartida = Oid;
                }

                // Actualizamos el OID de la Partida en el Stock del Expediente asociado
                if (stock != null)
                {
                    foreach (Stock item in parent.Stocks)
                    {
                        if (item.OidPartida == oldOid)
                            item.OidPartida = Oid;
                    }
                }
                else
                {
                    throw new iQException("Partida::Insert(): No se ha encontrado la línea de stock asociada a este producto");
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }
		internal void Insert(Batch parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidAlmacen = parent.OidAlmacen;
			OidKit = parent.Oid;

			try
			{
				GetNewCode(OidProducto);

				OidExpediente = ExpedientInfo.GetAlmacen(false).Oid;
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}
		internal void Insert(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidProducto = parent.Oid;

			try
			{
				GetNewCode(OidProducto);

				parent.Session().Save(_base.Record);
				_componentes.Update(this);

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
				SessionCode = parent.SessionCode;
				BatchRecord obj = Session().Get<BatchRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
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

            OidExpediente = parent.Oid;

            try
            {
                SessionCode = parent.SessionCode;
                BatchRecord obj = Session().Get<BatchRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);
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
				BatchRecord obj = Session().Get<BatchRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);

				_componentes.Update(this);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}
		internal void Update(Batch parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidAlmacen = parent.OidAlmacen;
			OidKit = parent.Oid;

			try
			{
				SessionCode = parent.SessionCode;
				BatchRecord obj = Session().Get<BatchRecord>(Oid);
				obj.CopyValues(this._base.Record);
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

			SessionCode = parent.SessionCode;

			//Borramos el stock INICIAL asociado
			List<Stock> stocks = parent.Stocks.GetSubList(new FCriteria<long>("OidPartida", Oid));
			Stocks peStocks = Stocks.GetChildList(stocks);

			if (peStocks.ContainsNotOnlyFirstEntry(Oid))
				throw new iQException("No se puede eliminar un concepto que tiene asociados movimientos de stock");

			//Stock, Cabezas y Maquinaria se borran por integridad referencial

			Session().Delete(Session().Get<BatchRecord>(Oid));

			MarkNew();
		}
        internal void DeleteSelf(Expedient parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;

            //Borramos el stock INICIAL asociado
            List<Stock> stocks = parent.Stocks.GetSubList(new FCriteria<long>("OidPartida", Oid));
            Stocks peStocks = Stocks.GetChildList(stocks);

            if (peStocks.ContainsNotOnlyFirstEntry(Oid) && peStocks.ContainsNotOnlyAlliedEntries(Oid))
                throw new iQException("No se puede eliminar un concepto que tiene asociados movimientos de stock");

            //Stock, Cabezas y Maquinaria se borran por integridad referencial

            Session().Delete(Session().Get<BatchRecord>(Oid));

            MarkNew();
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
                Session().Delete(Session().Get<BatchRecord>(Oid));

                if (IsKit)
                {
                    //Borramos el stock asociado
                    /*Expediente almacen = moleQule.Library.Store.Expediente.Get(this.OidExpediente);
                    List<Stock> stocks = almacen.Stocks.GetSubList(new FCriteria<long>("OidPartida", Oid));

                    foreach (Stock st in stocks)
                        almacen.Stocks.Remove(st.Oid);

                    almacen.Save();
                    almacen.CloseSession();*/

                    //Stock, Cabezas y Maquinaria se borran por integridad referencial

                    _componentes.Clear();
                    _componentes.Update(this);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }       
        internal void DeleteSelf(Batch parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                //Borramos el stock directo asociado y
                //las lineas de stock originales de baja del producto en el expediente
                /*Expediente almacen = Library.Store.Expediente.Get(this.OidExpediente);
                List<Stock> stocks = almacen.Stocks.GetSubList(new FCriteria<long>("OidPartida", Oid));

                foreach (Stock st in stocks)
                {
                    //Buscamos el expediente asociado al OidPartida del que viene éste
                    Expediente exp = Library.Store.Expediente.GetByPartida(OidPartida);
                    
                    if (st.OidStock != 0)
                        exp.Stocks.Remove(st.OidStock);

                    exp.UpdateStocks();
                    exp.Save();
                    exp.CloseSession();

                    almacen.Stocks.Remove(st.Oid);
                }

                almacen.Save();
                almacen.CloseSession();     */

                //Stock, Cabezas y Maquinaria se borran por integridad referencial
                //Hay que actualizar el stock de los expedientes implicados donde corresponda

                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<BatchRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

        #region SQL

        internal enum ETipoQuery { GENERAL = 0, EXPEDIENTE = 1, CLUSTERED = 2 };

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
			{
				{ 
					"StockKilos", 
					new ForeignField() {                        
						Property = "STOCK_K", 
						TableAlias = String.Empty, 
						Column = null
					}
				}
			};
		}

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        internal static string SELECT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions { Partida = BatchInfo.New(oid) };

            string query = SELECT(conditions, false);

            return query;
        }

        internal static string SELECT_FIELDS(ETipoQuery tipo, bool kit)
        {
            string query = string.Empty;

            switch (tipo)
            {
                case ETipoQuery.GENERAL:
                    {
                        query = @"
                        SELECT " + (long)tipo + @" AS ""TIPO""
                            ,BA.*
                            ,COALESCE(IPC.""OID"", 0) AS ""OID_IMPUESTO_COMPRA""
                            ,COALESCE(IPC.""PORCENTAJE"", 0) AS ""P_IMPUESTO_COMPRA""
                            ,COALESCE(IPV.""OID"", 0) AS ""OID_IMPUESTO_VENTA""
                            ,COALESCE(IPV.""PORCENTAJE"", 0) AS ""P_IMPUESTO_VENTA""
                            ,COALESCE(FM.""NOMBRE"", '') AS ""FAMILY""
                            ,COALESCE(AL.""CODIGO"", '') AS ""STORE_ID""
                            ,COALESCE(AL.""NOMBRE"", '') AS ""ALMACEN""
                            ,COALESCE(E.""CODIGO"", '') AS ""EXPEDIENTE""
                            ,COALESCE(E.""OBSERVACIONES"", '') AS ""OBS_EXPEDIENTE""
                            ,COALESCE(PR.""PRECIO_COMPRA"", 0) AS ""PRECIO_PRODUCTO""
                            ,COALESCE(PP.""PRECIO"", 0) AS ""PRECIO_PROVEEDOR""
                            ,COALESCE(S.""STOCK_K"", 0) AS ""STOCK_K""
                            ,COALESCE(S.""STOCK_B"", 0) AS ""STOCK_B""
                            ,COALESCE(SU.""NOMBRE"", '') AS ""PROVEEDOR""
                            ,COALESCE(PR.""CODIGO"", '') AS ""CODIGO_PRODUCTO""
                            ,COALESCE(PR.""NOMBRE"", '') AS ""PRODUCTO""
                            ,COALESCE(PR.""AYUDA_KILO"", 0) AS ""AYUDA_ESTIMADA_PRODUCTO""
                            ,CASE WHEN PR.""AVISAR_STOCK"" THEN PR.""STOCK_MINIMO"" ELSE 0 END AS ""STOCK_MINIMO""
                            ,COALESCE(PR.""CODIGO_ADUANERO"", '') AS ""CODIGO_ADUANERO""
                            ,COALESCE(CAP.""OID_ALBARAN"", 0) AS ""OID_ALBARAN""
                            ,COALESCE(CAP.""DELIVERY_CODE"", '') AS ""N_ALBARAN""
                            ,COALESCE(FP.""CODIGO"", '') AS ""N_FACTURA""";
                    }
                    break;

                case ETipoQuery.EXPEDIENTE:
                    {
                        query = @"
                        SELECT " + ((kit) ? " DISTINCT " : string.Empty) + (long)tipo + @" AS ""TIPO""
                            ,BA.*
                            ,0 AS ""OID_IMPUESTO_COMPRA""
                            ,0 AS ""P_IMPUESTO_COMPRA""
                            ,0 AS ""OID_IMPUESTO_VENTA""
                            ,0 AS ""P_IMPUESTO_VENTA""
                            ,COALESCE(FM.""CODIGO"", '') AS ""FAMILY""
                            ,COALESCE(AL.""CODIGO"", '') AS ""STORE_ID""
                            ,COALESCE(AL.""NOMBRE"", '') AS ""ALMACEN""
                            ,COALESCE(E.""CODIGO"", '') AS ""EXPEDIENTE""
                            ,COALESCE(E.""OBSERVACIONES"", '') AS ""OBS_EXPEDIENTE""
                            ,0 AS ""PRECIO_PRODUCTO""
                            ,0 AS ""AYUDA_ESTIMADA_PRODUCTO""
                            ,0 AS ""PRECIO_PROVEEDOR""
                            ,COALESCE(S.""STOCK_K"", 0) AS ""STOCK_K""
                            ,COALESCE(S.""STOCK_B"", 0) AS ""STOCK_B""
                            ," + ((kit) ? @"'VARIOS PROVEEDORES'" : @"COALESCE(SU.""NOMBRE"", '')") + @" AS ""PROVEEDOR""
                            ,COALESCE(PR.""CODIGO"", '') AS ""CODIGO_PRODUCTO""
                            ,COALESCE(PR.""NOMBRE"", '') AS ""PRODUCTO""
                            ,CASE WHEN PR.""AVISAR_STOCK"" THEN PR.""STOCK_MINIMO"" ELSE 0 END AS ""STOCK_MINIMO""
                            ,COALESCE(CAP.""OID_ALBARAN"", 0) AS ""OID_ALBARAN""
                            ,COALESCE(CAP.""DELIVERY_CODE"", '') AS ""N_ALBARAN""
                            ,COALESCE(FP.""CODIGO"", '') AS ""N_FACTURA""";
                    }
                    break;

                case ETipoQuery.CLUSTERED:
                    {
                        query = @"
                        SELECT " + (long)tipo + @" AS ""TIPO""
                            ,PR.""OID"" AS ""OID""
                            ,COALESCE(PR.""NOMBRE"", '') AS ""TIPO_MERCANCIA""
                            ,CASE WHEN PR.""AVISAR_STOCK"" THEN PR.""STOCK_MINIMO"" ELSE 0 END AS ""STOCK_MINIMO""
                            ,AVG(BA.""PRECIO_VENTA_KILO"") AS ""PRECIO_VENTA_KILO""
                            ,AVG(BA.""PRECIO_VENTA_BULTO"") AS ""PRECIO_VENTA_BULTO""
                            ,COALESCE(SUM(S.""STOCK_K""), 0) AS ""STOCK_K""
                            ,COALESCE(SUM(S.""STOCK_B""), 0) AS ""STOCK_B""
                            ,SUM(BA.""KILOS_INICIALES"") AS ""KILOS_INICIALES""
                            ,SUM(BA.""BULTOS_INICIALES"") AS ""BULTOS_INICIALES""
                            ,'VARIOS' AS ""EXPEDIENTE""
                            ,'VARIOS' AS ""PROVEEDOR""";
                    }
                    break;
            }

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
			if (conditions == null) return string.Empty;

            string query = @" 
			WHERE (BA.""FECHA_COMPRA"" BETWEEN '" + conditions.FechaIniLabel + @"' AND '" + conditions.FechaFinLabel + "')";

            if (conditions.Partida != null) 
				query += @" 
					AND BA.""OID"" = " + conditions.Partida.Oid;
            
			if (conditions.Producto != null) 
				query += @"
					AND BA.""OID_PRODUCTO"" = " + conditions.Producto.Oid;
            
			if (conditions.Almacen != null) 
				query += @"
					AND BA.""OID_ALMACEN"" = " + conditions.Almacen.Oid;
            
			if (conditions.Expedient != null) 
				query += @"
					AND BA.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;
            
			if (conditions.Familia != null) 
				query += @"
					AND SF.""OID_FAMILIA"" = " + conditions.Familia.Oid + @" AND BA.""OID_KIT"" = 0";
            
			if (conditions.Serie != null) 
				query += @"
					AND SF.""OID_SERIE"" = " + conditions.Serie.Oid + @" AND BA.""OID_KIT"" = 0";
            
			if (conditions.Almacen != null) 
				query += @"
					AND BA.""OID_ALMACEN"" = " + conditions.Almacen.Oid;
            
			if (conditions.InputDelivery != null) 
				query += @"
					AND AP.""OID"" = " + conditions.InputDelivery.Oid;

            if (conditions.Acreedor != null)
            {
                query += @"
					AND CAP.""OID_ACREEDOR"" = " + conditions.Acreedor.Oid + @"
                    AND CAP.""TIPO_ACREEDOR"" = " + conditions.Acreedor.TipoAcreedor;
            }

            return query + " " + conditions.ExtraWhere;
        }

        internal static string INNER_DELIVERY_LINE()
        {
            string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));

            string query = @"
            LEFT JOIN (SELECT IDL.""OID_ALBARAN""
		                    ,IDL.""OID_BATCH""
		                    ,ID.""CODIGO"" AS ""DELIVERY_CODE""
                            ,ID.""OID_ACREEDOR"" AS ""OID_ACREEDOR""
                            ,ID.""TIPO_ACREEDOR"" AS ""TIPO_ACREEDOR""
		                FROM " + idl + @" AS IDL
		                INNER JOIN " + id + @" AS ID ON ID.""OID"" = IDL.""OID_ALBARAN"" AND ID.""RECTIFICATIVO"" = FALSE)
	            AS CAP ON BA.""OID"" = CAP.""OID_BATCH""";

            return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions) { return SELECT_BASE(conditions, false); }
        internal static string SELECT_BASE(QueryConditions conditions, bool clustered)
        {
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string fm = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.FamilyRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            string pp = nHManager.Instance.GetSQLTable(typeof(ProductoProveedorRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
            string tx = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
            string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
            string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

            string query;

            query = (clustered ? SELECT_FIELDS(ETipoQuery.CLUSTERED, false) : SELECT_FIELDS(ETipoQuery.GENERAL, false)) + @"
			FROM " + ba + @" AS BA
            INNER JOIN " + al + @" AS AL ON AL.""OID"" = BA.""OID_ALMACEN""
            LEFT JOIN " + ex + @" AS E ON E.""OID"" = BA.""OID_EXPEDIENTE""
            LEFT JOIN " + su + @" AS SU ON SU.""OID"" = BA.""OID_PROVEEDOR""
            INNER JOIN " + pr + @" AS PR ON BA.""OID_PRODUCTO"" = PR.""OID""
            LEFT JOIN " + fm + @" AS FM ON FM.""OID"" = PR.""OID_FAMILIA""
            LEFT JOIN " + tx + @" AS IPC ON PR.""OID_IMPUESTO_COMPRA"" = IPC.""OID""
            LEFT JOIN " + tx + @" AS IPV ON PR.""OID_IMPUESTO_VENTA"" = IPV.""OID""" +
            INNER_DELIVERY_LINE() + @"
            LEFT JOIN " + idi + @" AS AFP ON AFP.""OID_ALBARAN"" = CAP.""OID_ALBARAN""
            LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = AFP.""OID_FACTURA""
            LEFT JOIN " + pp + @" AS PP ON PP.""OID_ACREEDOR"" = SU.""OID"" AND PP.""OID_PRODUCTO"" = PR.""OID"" AND PP.""TIPO_ACREEDOR"" = SU.""TIPO""
            LEFT JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""STOCK_K"", SUM(""BULTOS"") AS ""STOCK_B""
						FROM " + st + @"
						GROUP BY ""OID_BATCH"")
				AS S ON S.""OID_BATCH"" = BA.""OID""";

            if (conditions.Familia != null)
            {
                string sf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.FamilySerieRecord));
                query += @"
				INNER JOIN " + sf + @" AS SF ON (PR.""OID_FAMILIA"" = SF.""OID_FAMILIA"")";
            }

            if (conditions.Serie != null)
            {
                string sf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.FamilySerieRecord));
                query += @"
				INNER JOIN " + sf + @" AS SF ON (PR.""OID_FAMILIA"" = SF.""OID_FAMILIA"")";
            }

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            query = SELECT_BASE(conditions) +
                    WHERE(conditions);

			if (conditions.Orders == null)
			{
				conditions.Orders = new OrderList();
				conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Ascending, typeof(Batch)));
				conditions.Orders.Add(FilterMng.BuildOrderItem("FechaCompra", ListSortDirection.Descending, typeof(Batch)));
			}

			query += ORDER(conditions.Orders, string.Empty, ForeignFields());
			query += LIMIT(conditions.PagingInfo);

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT";

            return query;
        }        

        internal static string SELECT_BY_FAMILY(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { Familia = FamiliaInfo.New(oid) };

			conditions.Orders = new OrderList();
			conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Descending, typeof(Batch)));
			conditions.Orders.Add(FilterMng.BuildOrderItem("StockKilos", ListSortDirection.Descending, typeof(Batch)));

            query = SELECT(conditions, false);

            //query += " ORDER BY BA.\"TIPO_MERCANCIA\", \"STOCK_K\"";

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        internal static string SELECT_BY_STORE(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { Almacen = Store.StoreInfo.New(oid) };

			conditions.Orders = new OrderList();
			conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Descending, typeof(Batch)));
			conditions.Orders.Add(FilterMng.BuildOrderItem("StockKilos", ListSortDirection.Descending, typeof(Batch)));

			query = SELECT(conditions, false);

            //query += " ORDER BY BA.\"TIPO_MERCANCIA\", \"STOCK_K\"";

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        internal static string SELECT_BY_SERIE(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { Serie = (oid != 0) ? SerieInfo.New(oid) : null };

			conditions.Orders = new OrderList();
			conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Descending, typeof(Batch)));
			conditions.Orders.Add(FilterMng.BuildOrderItem("StockKilos", ListSortDirection.Descending, typeof(Batch)));

            query = SELECT(conditions, false);

            //query += " ORDER BY BA.\"TIPO_MERCANCIA\", \"STOCK_K\"";

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        internal static string SELECT_BY_FAMILY_AND_STOCK(long oidFamily, bool lockTable)
        {
            string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Familia = FamiliaInfo.New(oidFamily) };
			
			conditions.ExtraWhere = @" 
                AND ""STOCK_K"" > 0";

			conditions.Orders = new OrderList();
			conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Descending, typeof(Batch)));
			conditions.Orders.Add(FilterMng.BuildOrderItem("StockKilos", ListSortDirection.Descending, typeof(Batch)));

			query = SELECT(conditions, false);

            //query += " ORDER BY BA.\"TIPO_MERCANCIA\", \"STOCK_K\"";

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        internal static string SELECT_BY_SERIE_AND_STOCK(long oidSerie, bool clustered, bool available, bool noStock, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions();

            if (oidSerie != 0)
                conditions.Serie = SerieInfo.New(oidSerie);

			conditions.Orders = new OrderList();

			query = 
            SELECT_BASE(conditions, clustered) +
            WHERE(conditions);

			query += @"
				AND (""STOCK_K"" > 0 AND ""STOCK_B"" > 0)";

            if (available) 
				query += @"
					AND (((E.""FECHA_SALIDA_MUELLE"" IS NOT NULL) AND (E.""FECHA_SALIDA_MUELLE"" <= '" + QueryConditions.GetFechaMaxLabel(DateTime.Today) + @"'))
						OR BA.""OID_EXPEDIENTE"" = 0 
						OR E.""TIPO_EXPEDIENTE"" = " + (long)ETipoExpediente.Almacen + ")";

			if (clustered) 
				query += @"
					GROUP BY PR.""OID"", PR.""NOMBRE"", PR.""AVISAR_STOCK"", PR.""STOCK_MINIMO""";

            if (noStock)
                query = @"
				SELECT * FROM (" + query + @") AS P
                WHERE P.""STOCK_MINIMO"" >= P.""STOCK_K""";

            query += @"
			ORDER BY ""TIPO_MERCANCIA"", ""STOCK_K""";

            //if (lockTable) query += " FOR UPDATE OF PE NOWAIT;";

            return query;
        }

        internal static string SELECT_STOCK(QueryConditions conditions, bool lockTable)
        {
            string query = string.Empty;

            conditions.ExtraWhere = @" 
                AND ""STOCK_K"" > 0";

            conditions.Orders = new OrderList();
            conditions.Orders.Add(FilterMng.BuildOrderItem("TipoMercancia", ListSortDirection.Descending, typeof(Batch)));
            conditions.Orders.Add(FilterMng.BuildOrderItem("StockKilos", ListSortDirection.Descending, typeof(Batch)));

            query = SELECT(conditions, false);

            return query;
        }

        internal static string SELECT(Almacen item, bool get_kit_components, bool lockTable)
        {
            string query;

            if (item.Oid != 1)
            {
				QueryConditions conditions = new Library.Store.QueryConditions { Almacen = item.GetInfo() };
				conditions.Orders = new OrderList();
                query = SELECT(conditions, lockTable);
                query += @"
                ORDER BY BA.""TIPO_MERCANCIA""";
                //query += " FOR UPDATE OF PE NOWAIT;";
            }
            else
            {
                string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
                string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
                string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
                string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
                string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
                string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
                string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
                string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

                if (!get_kit_components)
                {
                    //Los productos independientes que no sean componentes
                    query = 
                    SELECT_FIELDS(ETipoQuery.EXPEDIENTE, false) + 
                    " FROM " + ba + " AS " +
                    " INNER JOIN " + al + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                    " LEFT JOIN " + ex + " AS E ON E.\"OID\" = BA.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN " + su + " AS SU ON SU.\"OID\" = BA.\"OID_PROVEEDOR\"" +
                    " INNER JOIN " + pr + " AS PR ON PR.\"OID\" = BA.\"OID_PRODUCTO\"" +
                    INNER_DELIVERY_LINE() + 
                    " LEFT JOIN " + idi + " AS AFP ON AFP.\"OID_ALBARAN\" = CAP.\"OID_ALBARAN\"" +
                    " LEFT JOIN " + fp + " AS FP ON FP.\"OID\" = AFP.\"OID_FACTURA\"" +
                    " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                    "               FROM " + st + "" +
                    "               GROUP BY \"OID_BATCH\")" +
                    "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                    " WHERE BA.\"OID_KIT\" = 0" +
                    " AND BA.\"OID_ALMACEN\" = " + item.Oid +
                    " UNION " +
                //Los productos elaborados
                    Batch.SELECT_FIELDS(ETipoQuery.EXPEDIENTE, true) +
                    " FROM " + ba + " AS BA" +
                    " INNER JOIN " + al + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                    " LEFT JOIN " + ex + " AS E ON E.\"OID\" = BA.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN " + ba + " AS BA2 ON BA2.\"OID_KIT\" = BA.\"OID\"" +
                    " INNER JOIN " + pr + " AS PR ON BA.\"OID_PRODUCTO\" = PR.\"OID\"" +
                    " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                    "               FROM " + st + "" +
                    "               GROUP BY \"OID_BATCH\")" +
                    "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                    " WHERE BA.\"OID_KIT\" = 0" +
                    " AND BA.\"OID_ALMACEN\" = " + item.Oid;
                }
                else
                {
                    //Todos los productos independientes
                    query =
                    SELECT_FIELDS(ETipoQuery.EXPEDIENTE, false) + @"
                    FROM " + ba + @" AS BA
                    INNER JOIN " + al + @" AS AL ON BA.""OID_ALMACEN"" = AL.""OID""
                    LEFT JOIN " + ex + @" AS E ON BA.""OID_EXPEDIENTE"" = E.""OID""
                    INNER JOIN " + su + @" AS SU ON SU.""OID"" = BA.""OID_PROVEEDOR"" AND BA.""OID_ALMACEN"" = " + item.Oid + @"
                    INNER JOIN " + pr + @" AS PR ON PR.""OID"" = BA.""OID_PRODUCTO"" AND BA.""OID_ALMACEN"" = " + item.Oid +
                    INNER_DELIVERY_LINE() + @"
                    LEFT JOIN " + idi + @" AS AFP ON AFP.""OID_ALBARAN"" = CAP.""OID_ALBARAN""
                    LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = AFP.""OID_FACTURA""
                    LEFT JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""STOCK_K"", SUM(""BULTOS"") AS ""STOCK_B""
                                FROM " + st + @"
                                GROUP BY ""OID_BATCH"")
                        AS S ON S.""OID_BATCH"" = BA.""OID""";

                    //Los productos elaborados  
                    query += @"
                    UNION " +
                    SELECT_FIELDS(ETipoQuery.EXPEDIENTE, true) + @"
                    FROM " + ba + @" AS BA
                    INNER JOIN " + al + @" AS AL ON BA.""OID_ALMACEN"" = AL.""OID""
                    LEFT JOIN " + ex + @" AS E ON BA.""OID_EXPEDIENTE"" = E.""OID""
                    INNER JOIN " + ba + @" AS BA2 ON BA2.""OID_KIT"" = BA.""OID""
                    INNER JOIN " + pr + @" AS PR ON BA.""OID_PRODUCTO"" = PR.""OID""" +
                    INNER_DELIVERY_LINE() + @"
                    LEFT JOIN " + idi + @" AS AFP ON AFP.""OID_ALBARAN"" = CAP.""OID_ALBARAN""
                    LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = AFP.""OID_FACTURA""
                    LEFT JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""STOCK_K"", SUM(""BULTOS"") AS ""STOCK_B""
                                FROM " + st + @"
                                GROUP BY ""OID_BATCH"")
                        AS S ON S.""OID_BATCH"" = BA.""OID""
                    WHERE BA.""OID_KIT"" = 0
                        AND BA.""OID_ALMACEN"" = " + item.Oid;
                }

                query += @"
                ORDER BY ""TIPO_MERCANCIA""";
            }

            return query;
        }

        internal static string SELECT(Expedient item, bool get_kit_components, bool lockTable)
        {
            string query;

            if (item.Oid != 1)
            {
				QueryConditions conditions = new Library.Store.QueryConditions { Expedient = item.GetInfo() };
				conditions.Orders = new OrderList();
				query = SELECT(conditions, lockTable);
                query += @"
                ORDER BY BA.""TIPO_MERCANCIA""";
                //query += " FOR UPDATE OF PE NOWAIT;";
            }
            else
            {
                string sr = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
                string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
                string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
                string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
                string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
                string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
                string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
                string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

                if (!get_kit_components)
                {
                    //Los productos independientes que no sean componentes
                    query = SELECT_FIELDS(ETipoQuery.EXPEDIENTE, false) +
                            " FROM " + ba + " AS BA" +
                            " INNER JOIN " + sr + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                            " LEFT JOIN " + ex + " AS E ON E.\"OID\" = BA.\"OID_EXPEDIENTE\"" +
                            " INNER JOIN " + su + " AS SU ON SU.\"OID\" = BA.\"OID_PROVEEDOR\"" +
                            " INNER JOIN " + pr + " AS PR ON PR.\"OID\" = BA.\"OID_PRODUCTO\"" +
                            INNER_DELIVERY_LINE() +
                            " LEFT JOIN " + idi + " AS AFP ON AFP.\"OID_ALBARAN\" = CAP.\"OID_ALBARAN\"" +
                            " LEFT JOIN " + ii + " AS FP ON FP.\"OID\" = AFP.\"OID_FACTURA\"" +
                            " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                            "               FROM " + st + "" +
                            "               GROUP BY \"OID_BATCH\")" +
                            "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                            " WHERE BA.\"OID_KIT\" = 0" +
                            " AND BA.\"OID_EXPEDIENTE\" = " + item.Oid +
                            " UNION " +
                        //Los productos elaborados
                            Batch.SELECT_FIELDS(ETipoQuery.EXPEDIENTE, true) +
                            " FROM " + ba + " AS BA" +
                            " INNER JOIN " + sr + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                            " LEFT JOIN " + ex + " AS E ON BA.\"OID_EXPEDIENTE\" = E.\"OID\"" +
                            " INNER JOIN " + ba + " AS BA2 ON BA2.\"OID_KIT\" = BA.\"OID\"" +
                            " INNER JOIN " + pr + " AS PR ON BA.\"OID_PRODUCTO\" = PR.\"OID\"" +
                            " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                            "               FROM " + st + "" +
                            "               GROUP BY \"OID_BATCH\")" +
                            "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                            " WHERE BA.\"OID_KIT\" = 0" +
                            " AND BA.\"OID_EXPEDIENTE\" = " + item.Oid;
                }
                else
                {
                    //Todos los productos independientes
                    query = Batch.SELECT_FIELDS(ETipoQuery.EXPEDIENTE, false) +
                            " FROM " + ba + " AS BA" +
                            " INNER JOIN " + sr + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                            " LEFT JOIN " + ex + " AS E ON BA.\"OID_EXPEDIENTE\" = E.\"OID\"" +
                            " INNER JOIN " + su + " AS SU ON SU.\"OID\" = BA.\"OID_PROVEEDOR\"" +
                            "                           AND BA.\"OID_EXPEDIENTE\" = " + item.Oid +
                            " INNER JOIN " + pr + " AS PR ON PR.\"OID\" = BA.\"OID_PRODUCTO\"" +
                            INNER_DELIVERY_LINE() +
                            " LEFT JOIN " + idi + " AS AFP ON AFP.\"OID_ALBARAN\" = CAP.\"OID_ALBARAN\"" +
                            " LEFT JOIN " + ii + " AS FP ON FP.\"OID\" = AFP.\"OID_FACTURA\"" +
                            " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                            "           FROM " + st + 
                            "           GROUP BY \"OID_BATCH\")" +
                            "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                            " UNION " +
                        //Los productos elaborados
                            Batch.SELECT_FIELDS(ETipoQuery.EXPEDIENTE, true) +
                            " FROM " + ba + " AS BA" +
                            " INNER JOIN " + sr + " AS AL ON AL.\"OID\" = BA.\"OID_ALMACEN\"" +
                            " LEFT JOIN " + ex + " AS E ON BA.\"OID_EXPEDIENTE\" = E.\"OID\"" +
                            " INNER JOIN " + ba + " AS PE2 ON PE2.\"OID_KIT\" = BA.\"OID\"" +
                            " INNER JOIN " + pr + " AS PR ON BA.\"OID_PRODUCTO\" = PR.\"OID\"" +
                            INNER_DELIVERY_LINE() +
                            " LEFT JOIN " + idi + " AS AFP ON AFP.\"OID_ALBARAN\" = CAP.\"OID_ALBARAN\"" +
                            " LEFT JOIN " + ii + " AS FP ON FP.\"OID\" = AFP.\"OID_FACTURA\"" +
                            " LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
                            "           FROM " + st + 
                            "           GROUP BY \"OID_BATCH\")" +
                            "   AS S ON S.\"OID_BATCH\" = BA.\"OID\"" +
                            " WHERE BA.\"OID_KIT\" = 0" +
                            " AND BA.\"OID_EXPEDIENTE\" = " + item.Oid;
                }

                query += @"
                ORDER BY ""TIPO_MERCANCIA""";
            }

            return query;
        }

        #endregion
    }
}