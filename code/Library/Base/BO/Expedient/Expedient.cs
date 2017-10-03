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
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ExpedientBase
    {
        #region Attributes

        private ExpedientRecord _record = new ExpedientRecord();

        //INNER JOIN
        private Decimal _total_kilos = 0;
        private Decimal _ayuda_estimada = 0;
        private Decimal _ayuda_pendiente = 0;
        private Decimal _ayuda_cobrada = 0;
        private Decimal _total_coste_calculado = 0;
        private Decimal _gastos_generales = 0;
        private Decimal _otros_gastos = 0;
        private Decimal _otros_gastos_facturas = 0;
        private string _proveedor = string.Empty;
        private string _despachante = string.Empty;
        private string _nombre_trans_orig = string.Empty;
        private string _nombre_trans_dest = string.Empty;
        private string _naviera = string.Empty;
        private string _almacen = string.Empty;
        protected Decimal _total_bultos = 0;
        protected Decimal _stock_kilos = 0;
        protected Decimal _stock_bultos = 0;

        #endregion

        #region Properties

        public ExpedientRecord Record { get { return _record; } set { _record = value; } }

        //CAMPOS NO ENLAZADOS
        public virtual ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_record.TipoExpediente; } }
        public virtual string ETipoExpedienteLabel { get { return EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }
        public virtual string Proveedor { get { return _proveedor; } set { _proveedor = value; } }
        public virtual string Almacen { get { return _almacen; } set { _almacen = value; } }
        public virtual string Naviera { get { return _naviera; } set { _naviera = value; } }
        public virtual string Despachante { get { return _despachante; } set { _despachante = value; } }
        public virtual string NombreTransDest { get { return _nombre_trans_dest; } set { _nombre_trans_dest = value; } }
        public virtual string NombreTransOrig { get { return _nombre_trans_orig; } set { _nombre_trans_orig = value; } }
        public virtual decimal KilosTotal { get { return _total_kilos; } set { _total_kilos = value; } }
        public virtual decimal BultosTotal { get { return _total_bultos; } set { _total_kilos = value; } }
        public virtual decimal StockKilos { get { return _stock_kilos; } set { _stock_kilos = value; } }
        public virtual decimal StockBultos { get { return _stock_bultos; } set { _stock_bultos = value; } }
        public virtual decimal AyudaEstimada { get { return Decimal.Round(_ayuda_estimada, 2); } set { _ayuda_estimada = value; } }
        public virtual decimal AyudaPendiente { get { return Decimal.Round(_ayuda_pendiente, 2); } set { _ayuda_pendiente = value; } }
        public virtual decimal AyudaCobrada { get { return Decimal.Round(_ayuda_cobrada, 2); } set { _ayuda_cobrada = value; } }
        public virtual decimal AyudaExpediente { get { return Decimal.Round((_record.Ayuda ? ((AyudaCobrada != 0) ? AyudaCobrada : AyudaEstimada) : 0), 2); } }
        public virtual decimal GastoPorKilo { get { return (_total_kilos > 0) ? GastoTotal / _total_kilos : 0; } }
        public virtual decimal GastosGenerales { get { return _gastos_generales; } set { _gastos_generales = value; } }
        public virtual decimal OtrosGastosFacturas { get { return Decimal.Round(_otros_gastos_facturas, 2); } set { _otros_gastos_facturas = value; } }
        public virtual decimal GastosFacturas { get { return Decimal.Round(GastosGenerales + OtrosGastosFacturas, 2); } }
        public virtual decimal OtrosGastos { get { return _otros_gastos; } set { _otros_gastos = value; } }
        public virtual decimal GastoTotal { get { return _gastos_generales + _otros_gastos_facturas + _otros_gastos; } }
        public virtual decimal GastoAbsoluto { get { return GastoTotal + _record.GProvTotal; } }
        public virtual decimal CosteTotal { get { return GastoAbsoluto - ((_ayuda_cobrada > 0) ? _ayuda_cobrada : _ayuda_estimada); } }
        public virtual decimal CosteProveedorCalculado { get { return _total_coste_calculado; } set { _total_coste_calculado = value; } }
        public virtual decimal CosteTotalCalculado { get { return _total_coste_calculado; } set { _total_coste_calculado = value; } }
        public virtual decimal CosteMedioPorKilo { get { return (_total_kilos > 0) ? (CosteTotalCalculado / _total_kilos) : 0; } }
		
        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;
            
            _record.CopyValues(source);

            _total_kilos = Format.DataReader.GetDecimal(source, "STOCK_K_INICIAL");
            _total_bultos = Format.DataReader.GetDecimal(source, "STOCK_B_INICIAL");
            _stock_kilos = Format.DataReader.GetDecimal(source, "STOCK_K");
            _stock_bultos = Format.DataReader.GetDecimal(source, "STOCK_B");
            _ayuda_estimada = Format.DataReader.GetDecimal(source, "TOTAL_REA_ESTIMADA");
            _ayuda_cobrada = Format.DataReader.GetDecimal(source, "TOTAL_REA_COBRADA");
            _ayuda_pendiente = Format.DataReader.GetDecimal(source, "TOTAL_REA_PENDIENTE");

            if (_stock_kilos == 0) _stock_bultos = 0;
            if (_stock_bultos == 0) _stock_kilos = 0;

            _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
            _naviera = Format.DataReader.GetString(source, "NAVIERA");
            _despachante = Format.DataReader.GetString(source, "DESPACHANTE");
            _nombre_trans_dest = Format.DataReader.GetString(source, "TRANS_DESTINO");
            _nombre_trans_orig = Format.DataReader.GetString(source, "TRANS_ORIGEN");
        }
        internal void CopyValues(Expedient source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _ayuda_estimada = source.AyudaEstimada;
            _ayuda_cobrada = source.AyudaCobrada;
            _ayuda_pendiente = source.AyudaPendiente;
            _total_coste_calculado = source.CosteProveedorCalculado;
            _gastos_generales = source.GastosGenerales;
            _otros_gastos = source.OtrosGastos;
            _total_kilos = source.KilosTotal;
            _proveedor = source.Proveedor;
            _naviera = source.Naviera;
            _despachante = source.Despachante;
            _nombre_trans_dest = source.NombreTransDest;
            _nombre_trans_orig = source.NombreTransOrig;
        }
        internal void CopyValues(ExpedientInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _ayuda_estimada = source.AyudaEstimada;
            _ayuda_cobrada = source.AyudaCobrada;
            _ayuda_pendiente = source.AyudaPendiente;
            _total_coste_calculado = source.CosteProveedorCalculado;
            _gastos_generales = source.GastosGenerales;
            _otros_gastos = source.OtrosGastos;
            _total_kilos = source.KilosTotal;
            _proveedor = source.Proveedor;
            _naviera = source.Naviera;
            _despachante = source.Despachante;
            _nombre_trans_dest = source.NombreTransDest;
            _nombre_trans_orig = source.NombreTransOrig;
        }

        #endregion
    }

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Expedient : BusinessBaseEx<Expedient>, IEntity
	{
		#region IEntity

		public long EntityType { get { return (long)moleQule.Common.Structs.ETipoEntidad.Expediente; } }

		#endregion

		#region Attributes

		public ExpedientBase _base = new ExpedientBase();

		private Relations _relations = Relations.NewChildList();
        private Expenses _gastos = Expenses.NewChildList();
        private Maquinarias _maquinarias = Maquinarias.NewChildList();
        private Batchs _partidas = Batchs.NewChildList();
        private Stocks _stocks = Stocks.NewChildList();
		private LineasFomento _expedientes_fomento = LineasFomento.NewChildList();
		private REAExpedients _expedientes_rea = REAExpedients.NewChildList();
        
		private InputInvoiceList _facturas = null;
		private InputDeliveryLineList _conceptos = null;

        #endregion

        #region Propiedades

        public ExpedientBase Base { get { return _base; } }

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
        public virtual long Tipo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoExpediente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.TipoExpediente.Equals(value))
                {
                    _base.Record.TipoExpediente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidNaviera
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidNaviera;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidNaviera.Equals(value))
                {
                    _base.Record.OidNaviera = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidTransOrigen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidTransOrigen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidTransOrigen.Equals(value))
                {
                    _base.Record.OidTransOrigen = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidTransDestino
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidTransDestino;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidTransDestino.Equals(value))
                {
                    _base.Record.OidTransDestino = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidDespachante
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidDespachante;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidDespachante.Equals(value))
                {
                    _base.Record.OidDespachante = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidFacturaPro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFacturaPro;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFacturaPro.Equals(value))
                {
                    _base.Record.OidFacturaPro = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidFacturaNav
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFacturaNav;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFacturaNav.Equals(value))
                {
                    _base.Record.OidFacturaNav = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidFacturaDes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFacturaDes;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFacturaDes.Equals(value))
                {
                    _base.Record.OidFacturaDes = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidFacturaTor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFacturaTor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFacturaTor.Equals(value))
                {
                    _base.Record.OidFacturaTor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidFacturaTde
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFacturaTde;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidFacturaTde.Equals(value))
                {
                    _base.Record.OidFacturaTde = value;
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
                    _base.Record.Fecha= value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string PuertoOrigen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PuertoOrigen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.PuertoOrigen.Equals(value))
                {
                    _base.Record.PuertoOrigen = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string PuertoDestino
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PuertoDestino;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.PuertoDestino.Equals(value))
                {
                    _base.Record.PuertoDestino = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Buque
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Buque;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Buque.Equals(value))
                {
                    _base.Record.Buque = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual int Ano
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Ano;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Ano.Equals(value))
                {
                    _base.Record.Ano = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaPedido
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaPedido;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaPedido.Equals(value))
                {
                    _base.Record.FechaPedido = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaFacProveedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaFacProveedor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaFacProveedor.Equals(value))
                {
                    _base.Record.FechaFacProveedor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaEmbarque
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaEmbarque;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaEmbarque.Equals(value))
                {
                    _base.Record.FechaEmbarque = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaLlegadaMuelle
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaLlegadaMuelle;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaLlegadaMuelle.Equals(value))
                {
                    _base.Record.FechaLlegadaMuelle = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaDespachoDestino
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaDespachoDestino;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaDespachoDestino.Equals(value))
                {
                    _base.Record.FechaDespachoDestino = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaSalidaMuelle
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaSalidaMuelle;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaSalidaMuelle.Equals(value))
                {
                    _base.Record.FechaSalidaMuelle= value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaRegresoMuelle
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaRegresoMuelle;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaRegresoMuelle.Equals(value))
                {
                    _base.Record.FechaRegresoMuelle = value;
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
        public virtual Decimal FleteNeto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FleteNeto;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FleteNeto.Equals(value))
                {
                    _base.Record.FleteNeto = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Baf
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Baf;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Baf.Equals(value))
                {
                    _base.Record.Baf = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Teus20
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Teus20;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Teus20.Equals(value))
                {
                    _base.Record.Teus20 = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Teus40
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Teus40;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Teus40.Equals(value))
                {
                    _base.Record.Teus40 = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal T3Origen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.T3Origen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.T3Origen.Equals(value))
                {
                    _base.Record.T3Origen = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal T3Destino
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.T3Destino;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.T3Destino.Equals(value))
                {
                    _base.Record.T3Destino = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal ThcOrigen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.ThcOrigen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.ThcOrigen.Equals(value))
                {
                    _base.Record.ThcOrigen = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal ThcDestino
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.ThcDestino;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.ThcDestino.Equals(value))
                {
                    _base.Record.ThcDestino = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Isps
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Isps;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Isps.Equals(value))
                {
                    _base.Record.Isps = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal TotalImpuestos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TotalImpuestos;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.TotalImpuestos.Equals(value))
                {
                    _base.Record.TotalImpuestos = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual bool EstimarDespachante
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.EstimarDespachante;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.EstimarDespachante.Equals(value))
				{
                    _base.Record.EstimarDespachante = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool EstimarNaviera
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.EstimarNaviera;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.EstimarNaviera.Equals(value))
				{
                    _base.Record.EstimarNaviera = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool EstimarTOrigen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.EstimarTorigen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.EstimarTorigen.Equals(value))
				{
                    _base.Record.EstimarTorigen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool EstimarTDestino
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.EstimarTdestino;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.EstimarTdestino.Equals(value))
				{
                    _base.Record.EstimarTdestino = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string GNavFac
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.GNavFac;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

                if (!_base.Record.GNavFac.Equals(value))
				{
                    _base.Record.GNavFac = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal GNavTotal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return Decimal.Round(_base.Record.GNavTotal, 2);
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.GNavTotal.Equals(value))
				{
                    _base.Record.GNavTotal = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string GTransFac
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GTransFac;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.GTransFac.Equals(value))
                {
                    _base.Record.GTransFac = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GTransTotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return Decimal.Round(_base.Record.GTransTotal, 2);
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GTransTotal.Equals(value))
                {
                    _base.Record.GTransTotal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string GDespFac
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GDespFac;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.GDespFac.Equals(value))
                {
                    _base.Record.GDespFac = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GDespTotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return Decimal.Round(_base.Record.GDespTotal, 2);
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GDespTotal.Equals(value))
                {
                    _base.Record.GDespTotal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GDespIgic
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GDespIgic;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GDespIgic.Equals(value))
                {
                    _base.Record.GDespIgic = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GDespIgicServ
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GDespIgicServ;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GDespIgicServ.Equals(value))
                {
                    _base.Record.GDespIgicServ = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string GTransDestFac
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GTransDestFac;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.GTransDestFac.Equals(value))
                {
                    _base.Record.GTransDestFac = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GTransDestTotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return Decimal.Round(_base.Record.GTransDestTotal, 2);
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GTransDestTotal.Equals(value))
                {
                    _base.Record.GTransDestTotal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GTransDestIgic
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GTransDestIgic;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GTransDestIgic.Equals(value))
                {
                    _base.Record.GTransDestIgic = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Contenedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Contenedor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Contenedor.Equals(value))
                {
                    _base.Record.Contenedor = value;
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
        public virtual string GProvFac
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GProvFac;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.GProvFac.Equals(value))
                {
                    _base.Record.GProvFac = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal GProvTotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GProvTotal;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GProvTotal.Equals(value))
                {
                    _base.Record.GProvTotal = value;
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
        public virtual string NombreCliente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.NombreCliente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.NombreCliente.Equals(value))
                {
                    _base.Record.NombreCliente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CodigoArticulo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CodigoArticulo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.CodigoArticulo.Equals(value))
                {
                    _base.Record.CodigoArticulo = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual Relations Relations
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _relations;
			}
		}
		public virtual Expenses Gastos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _gastos;
            }
        }
        public virtual Maquinarias Maquinarias
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _maquinarias;
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
        public virtual Stocks Stocks
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _stocks;
            }
        }
		public virtual LineasFomento ExpedientesFomento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _expedientes_fomento;
			}
		}
		public virtual REAExpedients ExpedientesREA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _expedientes_rea;
			}
		}

        //CAMPOS NO ENLAZADOS
        public virtual ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_base.Record.TipoExpediente; } }
		public virtual string ETipoExpedienteLabel { get { return EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }
        public string Description { get { return TipoMercancia; } set { TipoMercancia = value; } }
		public virtual string Proveedor { get { return _base.Proveedor; } set { _base.Proveedor = value; PropertyHasChanged(); } }
		public virtual string Almacen { get { return _base.Almacen; } set { _base.Almacen = value; PropertyHasChanged(); } }
		public virtual string Naviera { get { return _base.Naviera; } set { _base.Naviera = value; PropertyHasChanged(); } }
		public virtual string Despachante { get { return _base.Despachante; } set { _base.Despachante = value; PropertyHasChanged(); } }
		public virtual string NombreTransDest { get { return _base.NombreTransDest; } set { _base.NombreTransDest = value; PropertyHasChanged(); } }
		public virtual string NombreTransOrig { get { return _base.NombreTransOrig; } set { _base.NombreTransOrig = value; PropertyHasChanged(); } }
		public virtual decimal KilosTotal { get { return _base.KilosTotal; } 
            set { _base.KilosTotal = value; } }
		public virtual decimal AyudaEstimada { get { return Decimal.Round(_base.AyudaEstimada, 2); } 
            set { _base.AyudaEstimada = value; PropertyHasChanged(); } }
		public virtual decimal AyudaPendiente { get { return Decimal.Round(_base.AyudaPendiente, 2); } set { _base.AyudaPendiente = value; PropertyHasChanged(); } }
		public virtual decimal AyudaCobrada { get { return Decimal.Round(_base.AyudaCobrada, 2); } set { _base.AyudaCobrada = value; PropertyHasChanged(); } }
		public virtual decimal AyudaExpediente { get { return Decimal.Round((Ayuda ? ((AyudaCobrada != 0) ? AyudaCobrada : AyudaEstimada) : 0), 2); } }
		public virtual decimal GastoPorKilo { get { return (_base.KilosTotal > 0) ? GastoTotal / _base.KilosTotal : 0; } }
		public virtual decimal GastosGenerales { get { return _base.GastosGenerales; } set { _base.GastosGenerales = value; PropertyHasChanged(); } }
		public virtual decimal OtrosGastosFacturas { get { return Decimal.Round(_base.OtrosGastosFacturas, 2); } set { _base.OtrosGastosFacturas = value; PropertyHasChanged(); } }
		public virtual decimal GastosFacturas { get { return Decimal.Round(GastosGenerales + OtrosGastosFacturas, 2); } }
		public virtual decimal OtrosGastos { get { return _base.OtrosGastos; } set { _base.OtrosGastos = value; PropertyHasChanged(); } }
		public virtual decimal GastoTotal { get { return _base.GastosGenerales + _base.OtrosGastosFacturas + _base.OtrosGastos; } }
        public virtual decimal GastoAbsoluto { get { return GastoTotal + _base.Record.GProvTotal; } }
		public virtual decimal CosteTotal { get { return GastoAbsoluto - ((_base.AyudaCobrada > 0) ? _base.AyudaCobrada : _base.AyudaEstimada); } }
        public virtual decimal CosteProveedorCalculado { get { return _base.CosteProveedorCalculado; } set { _base.CosteProveedorCalculado = value; PropertyHasChanged(); } }
		public virtual decimal CosteTotalCalculado { get { return _base.CosteTotalCalculado + GastoTotal - ((_base.AyudaCobrada > 0) ? _base.AyudaCobrada : _base.AyudaEstimada); } }
        public virtual decimal CosteMedioPorKilo { get { return (_base.KilosTotal > 0) ? (CosteTotalCalculado / _base.KilosTotal) : 0; } }
		
		public virtual InputInvoiceList Facturas { get { return _facturas; } }
		public virtual InputDeliveryLineList Conceptos { get { return _conceptos; } set { _conceptos = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                       //&& _cabezas.IsValid
                       && _gastos.IsValid
                       && _maquinarias.IsValid
                       && _partidas.IsValid
                       && _stocks.IsValid
					   && _expedientes_fomento.IsValid
					   && _expedientes_rea.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                       //|| _cabezas.IsDirty
                       || _gastos.IsDirty
                       || _maquinarias.IsDirty
                       || _partidas.IsDirty
                       || _stocks.IsDirty
					   || _expedientes_fomento.IsDirty
					   || _expedientes_rea.IsDirty;
            }
        }
        
        #endregion

        #region Business Methods

        public virtual Expedient CloneAsNew()
        {
            Expedient clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.GetNewCode();
            clon.SessionCode = Expedient.OpenSession();
            Expedient.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            //clon.Cabezas.MarkAsNew();
            clon.Gastos.MarkAsNew();
            clon.Maquinarias.MarkAsNew();
            clon.Partidas.MarkAsNew();
            clon.Stocks.MarkAsNew();
			clon.ExpedientesFomento.MarkAsNew();
			clon.ExpedientesREA.MarkAsNew();

            return clon;
        }
        
        public virtual void GetNewCode()
        {
            switch (ETipoExpediente)
            {
				case ETipoExpediente.Almacen:
					Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
					Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.ALMACEN_CODE_FORMAT) + "#ALM";
					break;

                case ETipoExpediente.Alimentacion:
                    Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
                    Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.EXPEDIENTE_CODE_FORMAT) + "____";
                    break;

                case ETipoExpediente.Ganado:
                    Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
                    Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.EXPEDIENTE_CODE_FORMAT) + "#GAN";
                    break;

                case ETipoExpediente.Maquinaria:
                    Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
                    Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.EXPEDIENTE_CODE_FORMAT) + "#MAQ";
                    break;

				case ETipoExpediente.Work:
					Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
					Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.EXPEDIENTE_CODE_FORMAT) + "#OBR";
					break;

				case ETipoExpediente.Project:
					Serial = ExpedientSerialInfo.GetNext(ETipoExpediente, DateTime.Now.Year);
					Codigo = "(" + DateTime.Today.Year.ToString() + ") " + Serial.ToString(Resources.Defaults.EXPEDIENTE_CODE_FORMAT) + "#PRO";
					break;
            }

            SetCode(ETipoAcreedor.Proveedor);
            SetCode(ETipoAcreedor.Naviera);
        }

        public virtual void SetCode(ETipoAcreedor tipo)
        {
			if (ETipoExpediente == ETipoExpediente.Almacen) return;

            switch (tipo)
            {
                case ETipoAcreedor.Proveedor:

                    if (Codigo == string.Empty) return;
                    if (Proveedor == string.Empty) return;

                    if (Codigo[Codigo.Length - 3] == '_' &&
                        Codigo[Codigo.Length - 4] == '_')
                    {
                        string codigo = Codigo;
                        codigo = codigo.Substring(0, codigo.Length - 4);
                        codigo += Char.ToUpper(Proveedor[0]);
                        codigo += Char.ToUpper(Proveedor[1]);
                        codigo += Codigo[Codigo.Length - 2];
                        codigo += Codigo[Codigo.Length - 1];
                        Codigo = codigo;
                    }
                    break;

                case ETipoAcreedor.Naviera:

                    if (Codigo == string.Empty) return;
                    if (Naviera == string.Empty) return;

                    string code = Codigo;
                    code = code.Substring(0, code.Length - 2);
                    code += Char.ToUpper(Naviera[0]);
                    code += Char.ToUpper(Naviera[1]);
                    Codigo = code;

                    break;
            }
        }

        public virtual void SetGasto(InputInvoice fac)
        {
            switch (fac.ETipoAcreedor)
            {
                case ETipoAcreedor.Proveedor:

                    OidFacturaPro = fac.Oid;
                    FechaFacProveedor = fac.Fecha;
                    GProvFac = fac.NFactura;
                    GProvTotal = fac.BaseImponible;
                     break;

                case ETipoAcreedor.Naviera:

                    OidFacturaNav = fac.Oid;
                    GNavFac = fac.NFactura;
                    GNavTotal = fac.BaseImponible;
                    break;

                case ETipoAcreedor.TransportistaOrigen:

                    OidFacturaTor = fac.Oid;
                    GTransFac = fac.NFactura;
                    GTransTotal = fac.BaseImponible;
                    break;

                case ETipoAcreedor.TransportistaDestino:

                    OidFacturaTde = fac.Oid;
                    GTransDestFac = fac.NFactura;
                    GTransDestTotal = fac.BaseImponible;
                    GTransDestIgic = fac.Impuestos;
                    break;

                case ETipoAcreedor.Despachante:

                    OidFacturaDes = fac.Oid;
                    GDespFac = fac.NFactura;
                    GDespTotal = fac.BaseImponible;
                    GDespIgicServ = fac.Impuestos;
                    break;
            }
        }
        public virtual void SetGasto(InputInvoiceInfo fac)
        {
            switch (fac.ETipoAcreedor)
            {
                case ETipoAcreedor.Proveedor:

                    OidFacturaPro = fac.Oid;
                    FechaFacProveedor = fac.Fecha;
                    GProvFac = fac.NFactura;
                    GProvTotal = fac.BaseImponible;
                    break;

                case ETipoAcreedor.Naviera:

                    OidFacturaNav = fac.Oid;
                    GNavFac = fac.NFactura;
                    GNavTotal = fac.BaseImponible;
                    break;

                case ETipoAcreedor.TransportistaOrigen:

                    OidFacturaTor = fac.Oid;
                    GTransFac = fac.NFactura;
                    GTransTotal = fac.BaseImponible;
                    break;

                case ETipoAcreedor.TransportistaDestino:

                    OidFacturaTde = fac.Oid;
                    GTransDestFac = fac.NFactura;
                    GTransDestTotal = fac.BaseImponible;
                    GTransDestIgic = fac.Impuestos;
                    break;

                case ETipoAcreedor.Despachante:

                    OidFacturaDes = fac.Oid;
                    GDespFac = fac.NFactura;
                    GDespTotal = fac.BaseImponible;
                    GDespIgicServ = fac.Impuestos;
                    break;
            }
        }

        public virtual decimal GastosTotalesConIGIC()
        {
            decimal suma = 0;

            suma = GDespIgic +
                    GDespIgicServ +
                    GTransDestIgic +
                    GastosGenerales +
					OtrosGastos +
					GProvTotal;                   

            return suma;
        }

        public virtual decimal Beneficios()
        {
            decimal beneficios = 0;
            foreach (Batch pe in Partidas)
            {
                beneficios += pe.PrecioVentaKilo * pe.KilosIniciales;
            }

            beneficios = beneficios - _base.GastosGenerales + _base.AyudaEstimada;

            return beneficios;
        }

		public virtual decimal IngresosEstimados()
		{
			decimal total = 0;
			foreach (Batch pa in Partidas)
			{
				total += pa.PrecioVentaKilo * pa.KilosIniciales;
			}

			return total;
		}
		public virtual decimal BeneficioEstimados()
		{
			decimal total = 0;
			foreach (Batch pa in Partidas)
			{
				total += pa.PrecioVentaKilo * pa.KilosIniciales;
			}

			total = total - _base.GastosGenerales + _base.AyudaEstimada;

			return total;
		}

        public virtual decimal CalculateTotalImpuestos()
        {
            return  ThcDestino +
                    ThcOrigen +
                    T3Destino +
                    T3Origen +
                    Isps +
                    Baf;
        }

		public static void DeleteStock(List<IStockable> lines)
		{
#if TRACE
			ControlerBase.AppControler.Timer.Record("Borrado del Concepto de Albarán");
#endif
			//Creamos la cache si no existe
			if (!Cache.Instance.Contains(typeof(Expedients)))
				Cache.Instance.Save(typeof(Expedients), Library.Store.Expedients.NewList());

			Expedient exp;

			foreach (IStockable item in lines)
			{
				if (item.OidExpedient > 0)
				{
					exp = Expedient.Get(item.OidExpedient, true, true);

					// Eliminamos el stock del Expediente asociado
					exp.RemoveStock(exp.Stocks.GetItem(item.OidStock));
#if TRACE
					ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
				}
			}
		}

        /// <summary>
        /// Borra una linea de Stock y actualiza el stock de la partida asociada
        /// </summary>
        /// <param name="item"></param>
        public virtual void RemoveStock(Stock item)
        {
			if (item == null) return;
			RemoveStock(item.Oid, item.OidPartida);
        }
		public virtual void RemoveStock(long oidStock, long oidPartida)
		{
            Stocks.Remove(oidStock);

            Expedients expedientes = Expedients.GetListByOidEnlaceStock(oidStock, true, SessionCode);
            if (expedientes != null)
            {
                bool save = false;
                foreach (Expedient item in expedientes)
                {
                    if (item.Oid != this.Oid)
                    {
                        if (item.Stocks != null)
                        {
                            save = true;
                            item.Stocks.RemoveStocksByOidEnlace(item, oidStock);
                            Batch pt = Partidas.GetItem(oidPartida);
                            item.UpdateStocks(pt, true);
                        }
                    }
                }

                if (save)
                    expedientes.SaveAsChild();
            }

			Batch pe = Partidas.GetItem(oidPartida);
			UpdateStocks(pe, true);
		}

        public virtual void UpdateGasto(Expense gasto, bool throwStockException)
        {
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);
            if (Conceptos == null) LoadConceptosAlbaranes(false);

            switch (ETipoExpediente)
            {
                case ETipoExpediente.Alimentacion:
                case ETipoExpediente.Ganado:
                case ETipoExpediente.Maquinaria:
                    Store.Contenedor.ReparteGasto(this, gasto, _conceptos);
                    break;

                case ETipoExpediente.Work:
                    Obra.ReparteGasto(this, gasto, _conceptos);
                    break;

                default:
                    Obra.ReparteGasto(this, gasto, _conceptos);
                    break;
            }

            UpdateGastosPartidas(throwStockException);
        }
        public virtual void UpdateGasto(InputInvoice fac, InputDeliveryLineList conceptos, bool throwStockException) { UpdateGasto(fac.GetInfo(true), conceptos, throwStockException); }
        public virtual void UpdateGasto(InputInvoiceInfo fac, InputDeliveryLineList conceptos, bool throwStockException)
        {
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);

            switch (ETipoExpediente)
            {
                case ETipoExpediente.Alimentacion:
                case ETipoExpediente.Ganado:
                case ETipoExpediente.Maquinaria:
                    Store.Contenedor.EditaGasto(this, fac, conceptos, throwStockException);
                    break;

                case ETipoExpediente.Work:
                    Obra.EditaGasto(this, fac, conceptos);
                    break;

                default:
                    Obra.EditaGasto(this, fac, conceptos);
                    break;
            }

            UpdateGastosPartidas(throwStockException);
        }

		public virtual void NuevoGasto(InputInvoice fac, InputDeliveryLineList conceptos, bool throwStockException) { NuevoGasto(fac.GetInfo(true), conceptos, throwStockException); }
		public virtual void NuevoGasto(InputInvoiceInfo fac, InputDeliveryLineList conceptos, bool throwStockException)
		{
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);

			if (fac.Conceptos == null)
				fac.LoadChilds(typeof(InputInvoiceLine), false);

			if (fac.Conceptos.Count == 0)
				throw new iQException(Resources.Messages.FACTURA_SIN_CONCEPTOS);

			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
                    Store.Contenedor.NuevoGasto(this, fac, conceptos, throwStockException);
					break;

				case ETipoExpediente.Work:
					Obra.NuevoGasto(this, fac, conceptos);
					break;

				default:
					Obra.NuevoGasto(this, fac, conceptos);
					break;
			}

			if (Facturas.GetItem(fac.Oid) == null) Facturas.AddItem(fac);

            UpdateGastosPartidas(throwStockException);
		}
        public virtual void NuevoGasto(bool throwStockException)
		{
            NuevoGasto(Expense.NewChild(this, ECategoriaGasto.OtrosExpediente), throwStockException);
		}
        public virtual void NuevoGasto(Expense gasto, bool throwStockException)
		{
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);
			if (Conceptos == null) LoadConceptosAlbaranes(false);

			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
					Store.Contenedor.NuevoGasto(this, gasto, _conceptos);
					break;

				case ETipoExpediente.Work:
					Obra.NuevoGasto(this, gasto, _conceptos);
					break;

				default:
					Obra.NuevoGasto(this, gasto, _conceptos);
					break;
			}

            UpdateGastosPartidas(throwStockException);
		}

        public virtual void RemoveGasto(Expense gasto, bool throwStockException)
		{
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);
			if (Conceptos == null) LoadConceptosAlbaranes(false);

			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
					Store.Contenedor.RemoveGasto(this, gasto, _conceptos);
					break;

				case ETipoExpediente.Work:
					Obra.RemoveGasto(this, gasto, _conceptos);
					break;

				default:
					Obra.RemoveGasto(this, gasto, _conceptos);
					break;
			}

            UpdateGastosPartidas(throwStockException);
		}
        public virtual void RemoveGasto(InputInvoice fac, InputDeliveryLineList conceptos, bool throwStockException) { RemoveGasto(fac.GetInfo(true), conceptos, throwStockException); }
        public virtual void RemoveGasto(InputInvoiceInfo fac, InputDeliveryLineList conceptos, bool throwStockException) 
		{
            if (Gastos.Count == 0) LoadChilds(typeof(Expense), false, throwStockException);

			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
                    Store.Contenedor.RemoveGasto(this, fac, conceptos, throwStockException);
					break;

				case ETipoExpediente.Work:
					Obra.RemoveGasto(this, fac, conceptos);
					break;

				default:
					Obra.RemoveGasto(this, fac, conceptos);
					break;
			}

			Facturas.RemoveItem(fac.Oid);

            UpdateGastosPartidas(throwStockException);
		}
        public virtual void RemoveGasto(ExpenseInfo gasto)
        {
            Expense item = Gastos.GetItem(gasto.Oid);
            if (item != null) Gastos.Remove(this, item);
        }

        public virtual void UpdateGastosPartidas(bool throwStockException) { UpdateGastosPartidas(_partidas, throwStockException); }
        private void UpdateGastosPartidas(Batchs partidas, bool throwStockException)
		{
			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
					Store.Contenedor.UpdateTotalesGastos(this);
                    Store.Contenedor.UpdateGastosPartidas(this, partidas, throwStockException);
					break;

				case ETipoExpediente.Work:
					Obra.UpdateTotalesGastos(this);
					break;

				default:
					Obra.UpdateTotalesGastos(this);
					break;
			}
		}

		/// <summary>
		/// Actualiza el Stock de las partidas del Expediente
		/// </summary>
        public virtual void UpdateStocks(bool throwStockException)
		{
            foreach (Batch item in Partidas)
                UpdateStocks(item, throwStockException);
		}
        public virtual void UpdateStocks(Batch partida, bool throwStockException)
		{
            _stocks.UpdateStocks(partida, throwStockException);
		}

        public virtual string GetStockWarning()
        {
            return GetInfo(true).GetStockWarning();
        }

		public virtual void UpdateAyudas(bool ayuda)
		{
			foreach (Batch item in Partidas) item.Ayuda = ayuda;
			UpdateAyudas();
		}
		public virtual void UpdateAyudas()
		{
			switch (ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
					Store.Contenedor.UpdateAyudas(this);
					break;

				case ETipoExpediente.Work:
					break;

				default:
					break;
			}
		}

		public virtual void UpdateAyudaEstimada()
		{
			_base.AyudaEstimada = 0;

			if (Partidas == null) return;

			if (Ayuda)
			{
				foreach (Batch item in Partidas)
                    _base.AyudaEstimada += (Ayuda) ? item.AyudaRecibidaKilo * item.KilosIniciales : 0;
			}

            AyudaEstimada = _base.AyudaEstimada;
		}

		public virtual void UpdateTotalCostesCompensables()
		{
			FleteNeto = 0;
			Isps = 0;
			Baf = 0;
			ThcDestino = 0;
			ThcOrigen = 0;
			T3Destino = 0;
			T3Origen = 0;

			foreach (LineaFomento item in ExpedientesFomento)
			{
				FleteNeto += item.FleteNeto;
				Isps += item.ISPS;
				Baf += item.BAF;
				ThcDestino += item.THCDestino;
				ThcOrigen += item.THCOrigen;
				T3Destino += item.T3Destino;
				T3Origen += item.T3Origen;
			}

            TotalImpuestos = _base.Record.FleteNeto +
                               _base.Record.T3Origen +
                               _base.Record.T3Destino +
                               _base.Record.ThcOrigen +
                               _base.Record.ThcDestino +
                               _base.Record.Isps +
                               _base.Record.Baf;
		}

        public virtual void UpdateTotalesProductos(Batchs partidas, bool throwStockException)
		{
			AyudaEstimada = 0;
			_base.CosteTotalCalculado = 0;
			KilosTotal = 0;

			foreach (Batch item in partidas)
			{
				if (item.OidExpediente != Oid) continue;

				_base.KilosTotal += item.KilosIniciales;
				_base.AyudaEstimada += (Ayuda) ? item.AyudaRecibidaKilo * item.KilosIniciales : 0;
				_base.CosteTotalCalculado += item.PrecioCompraKilo * item.KilosIniciales;
			}

			AyudaEstimada = _base.AyudaEstimada;
			CosteProveedorCalculado = _base.CosteTotalCalculado;

            UpdateGastosPartidas(partidas, throwStockException);
		}

        public virtual bool UpdateEnlaceStocks(long oidOld, long oidNew)
        {
            bool enlazado = false;

            foreach (Stock st in _stocks)
            {
                if (st.OidEnlace == oidOld)
                {
                    enlazado = true;
                    st.OidEnlace = oidNew;
                }
            }

            return enlazado;
        }

		public virtual bool CheckAcreedor(long oid_acreedor, ETipoAcreedor tipo)
        {
            switch (tipo)
            {
                case ETipoAcreedor.Proveedor:
                    return (OidProveedor == oid_acreedor);

                case ETipoAcreedor.Naviera:
                    return (OidNaviera == oid_acreedor);

                case ETipoAcreedor.TransportistaOrigen:
                    return (OidTransOrigen == oid_acreedor);

                case ETipoAcreedor.TransportistaDestino:
                    return (OidTransDestino == oid_acreedor);

                case ETipoAcreedor.Despachante:
                    return (OidDespachante == oid_acreedor);
            }
            
            return false;
        }

        public virtual bool ExistsAcreedor(ETipoAcreedor tipo)
        {
            switch (tipo)
            {
                case ETipoAcreedor.Proveedor:
                    return (OidProveedor != 0);

                case ETipoAcreedor.Naviera:
                    return (OidNaviera != 0);

                case ETipoAcreedor.TransportistaOrigen:
                    return (OidTransOrigen != 0);

                case ETipoAcreedor.TransportistaDestino:
                    return (OidTransDestino != 0);

                case ETipoAcreedor.Despachante:
                    return (OidDespachante != 0);
            }

            return false;
        }

        public virtual bool CheckFacturaAcreedor(long oid_factura, ETipoAcreedor tipo)
        {
            switch (tipo)
            {
                case ETipoAcreedor.Proveedor:
                    return (OidFacturaPro == oid_factura);

                case ETipoAcreedor.Naviera:
                    return (OidFacturaNav == oid_factura);

                case ETipoAcreedor.TransportistaOrigen:
                    return (OidFacturaTor == oid_factura);

                case ETipoAcreedor.TransportistaDestino:
                    return (OidFacturaTde == oid_factura);

                case ETipoAcreedor.Despachante:
                    return (OidFacturaDes == oid_factura);
            }

            return false;
        }
        
        public virtual bool ExistsGastoPrincipalAcreedor(ETipoAcreedor tipo)
        {
            Expense gasto = null;

            switch (tipo)
            {
                case ETipoAcreedor.Proveedor:
                    gasto = Gastos.GetItemByFactura(OidFacturaPro, ECategoriaGasto.Stock);
                    break;

                case ETipoAcreedor.Naviera:
					gasto = Gastos.GetItemByFactura(OidFacturaNav, ECategoriaGasto.NoStock);
                    break;

                case ETipoAcreedor.TransportistaOrigen:
					gasto = Gastos.GetItemByFactura(OidFacturaTor, ECategoriaGasto.NoStock);
                    break;

                case ETipoAcreedor.TransportistaDestino:
					gasto = Gastos.GetItemByFactura(OidFacturaTde, ECategoriaGasto.NoStock);
                    break;

                case ETipoAcreedor.Despachante:
					gasto = Gastos.GetItemByFactura(OidFacturaDes, ECategoriaGasto.NoStock);
                    break;
            }

            return (gasto != null);
        }

		public virtual InputInvoiceList GetFacturasNaviera()
		{
			List<InputInvoiceInfo> lista = new List<InputInvoiceInfo>();

			foreach (InputInvoiceInfo item in _facturas)
			{
				if (item.ETipoAcreedor == ETipoAcreedor.Naviera)
					lista.Add(item);
			}

			return InputInvoiceList.GetList(lista);
		}

        public virtual void AddMovimientoStock(ETipoStock tipo, Stock stock, ExpedientInfo expediente, int sessionCode, bool throwStockException)
		{
			switch (tipo)
			{
				case ETipoStock.MovimientoSalida:
					{
						Expedient exp_entrada = Expedient.Get(expediente.Oid, false, true, sessionCode);
                        exp_entrada.LoadChilds(typeof(Stock), true, throwStockException);
                        exp_entrada.LoadChilds(typeof(Batch), true, throwStockException);
						exp_entrada.BeginEdit();

                        Batch partida = exp_entrada.Partidas.NewItem(Partidas.GetItem(stock.OidPartida), stock, exp_entrada, ETipoStock.MovimientoEntrada);
						Stock stock_entrada = exp_entrada.Stocks.NewItem(partida, stock, ETipoStock.MovimientoEntrada);
						stock_entrada.Observaciones = String.Format(Resources.Messages.ENTRADA_POR_MOVIMIENTO, Codigo);
						stock_entrada.Inicial = true;
                        stock_entrada.OidEnlace = stock.Oid;
						stock_entrada.OidConceptoAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidConceptoAlbaran;
						stock_entrada.OidAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidAlbaran;

						exp_entrada.ApplyEdit();
						exp_entrada.SaveAsChild();
						//exp_entrada.CloseSession();
					}
                    break;
                case ETipoStock.Consumo:
                    {
                        Expedient exp_entrada = Expedient.Get(expediente.Oid, false, true, sessionCode);
                        exp_entrada.LoadChilds(typeof(Stock), true, throwStockException);
                        exp_entrada.LoadChilds(typeof(Batch), true, throwStockException);
                        exp_entrada.BeginEdit();

                        Batch partida = exp_entrada.Partidas.NewItem(Partidas.GetItem(stock.OidPartida), stock, exp_entrada, ETipoStock.MovimientoEntrada);
                        //Movimiento de entrada en el expediente de ganado
                        Stock stock_entrada = exp_entrada.Stocks.NewItem(partida, stock, ETipoStock.MovimientoEntrada);
                        stock_entrada.Observaciones = String.Format(Resources.Messages.ENTRADA_POR_MOVIMIENTO, Codigo);
                        stock_entrada.Inicial = true;
                        stock_entrada.OidEnlace = stock.Oid;
                        stock_entrada.OidConceptoAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidConceptoAlbaran;
                        stock_entrada.OidAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidAlbaran;
                        //Merma en el expediente de ganado (consumo)
                        Stock stock_merma = exp_entrada.Stocks.NewItem(partida, stock, ETipoStock.Merma);
                        stock_merma.Observaciones = Resources.Messages.SALIDA_POR_MERMA;
                        stock_merma.Inicial = false;
                        stock_merma.OidEnlace = stock.Oid;
                        stock_merma.OidConceptoAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidConceptoAlbaran;
                        stock_merma.OidAlbaran = _stocks.GetInitialStock(_partidas.GetItem(stock.OidPartida).Oid).OidAlbaran;

                        exp_entrada.ApplyEdit();
                        exp_entrada.SaveAsChild();
                        //exp_entrada.CloseSession();
                    }
                    break;
			}
		}

		protected void UpdateLineasLibroGanadero()
		{
			if (ETipoExpediente != ETipoExpediente.Ganado) return;

			LivestockBookLines lineas = LivestockBookLines.GetByExpedienteList(Oid, false);

			foreach (LivestockBookLine item in lineas)
			{
				item.EEstado = EEstado.Alta;
				item.Fecha = FechaDespachoDestino;
			}

			lineas.Save();
		}

		#endregion

        #region Validation Rules

        //región a rellenar si hay campos requeridos

        #endregion

        #region Autorization Rules

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
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
        protected Expedient() {}
		private Expedient(Expedient source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
			SessionCode = source.SessionCode;
            Fetch(source);
        }
		private Expedient(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

        /// <summary>
        /// Crea un nuevo objeto
        /// </summary>
        /// <returns>Nuevo objeto creado</returns>
        /// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
        public static Expedient NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<Expedient>(new CriteriaCs(-1));
        }

		public static Expedient GetChild(long oid, bool childs)
		{
			Expedient obj = Get(oid, childs);
			obj.MarkAsChild();

			return obj;
		}
        internal static Expedient GetChild(Expedient source)
        {
            return new Expedient(source, false);
        }
		internal static Expedient GetChild(Expedient source, bool childs)
        {
            return new Expedient(source, childs);
        }
		internal static Expedient GetChild(int sessionCode, IDataReader source) { return GetChild(sessionCode, source, false); }
		internal static Expedient GetChild(int sessionCode, IDataReader reader, bool childs) { return new Expedient(sessionCode, reader, childs); }
		
		public static Expedient GetChild(int sessionCode, long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Expedient.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Expedient.SELECT(oid);

			Expedient obj = DataPortal.Fetch<Expedient>(criteria);
			obj.MarkAsChild();

			return obj;
		}

		public virtual ExpedientInfo GetInfo() { return GetInfo(true); }
		public virtual ExpedientInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new ExpedientInfo(this, childs);
		}

        public virtual void LoadChilds(Type type, bool getChilds, bool throwStockException)
		{
			if (IsNew) return;

			if (type.Equals(typeof(Batch)))
			{
				if (_partidas.Count > 0) return;

				_partidas = Batchs.GetChildList(this, getChilds);

				switch ((ETipoExpediente)Tipo)
				{
					case ETipoExpediente.Maquinaria:
						_maquinarias = Maquinarias.GetChildList(this, getChilds);
						break;
				}
			}
			else if (type.Equals(typeof(Stock)))
			{
				if (_stocks.Count > 0) return;

                _stocks = Stocks.GetChildList(this, getChilds, throwStockException);
                UpdateStocks(throwStockException);
			}
			else if (type.Equals(typeof(Expense)))
			{
				if (_gastos.Count > 0) return;

				_gastos = Expenses.GetChildList(this, getChilds);
				_facturas = InputInvoiceList.GetListByExpediente(Oid, getChilds);
			}
			else if (type.Equals(typeof(LineaFomento)))
			{
				if (_expedientes_fomento.Count > 0) return;

				_expedientes_fomento = LineasFomento.GetChildList(this, getChilds);
			}
			else if (type.Equals(typeof(REAExpedient)))
			{
				if (_expedientes_rea.Count > 0) return;

				_expedientes_rea = REAExpedients.GetChildList(this, getChilds);
				UpdateAyudas();
			}
            else if (type.Equals(typeof(InputDeliveryLine)))
            {
                if (_conceptos != null && _conceptos.Count > 0) return;

                _conceptos = InputDeliveryLineList.GetByExpedienteList(this.Oid, getChilds);
            }
			else if (type.Equals(typeof(Expedient)) || type.Equals(typeof(Relation)))
			{
				if (_relations != null && _relations.Count > 0) return;

				_relations = Relations.GetChildList(this, getChilds);
			}
		}

        public virtual void LoadChildsFromList(Type type, string list, bool get_childs, bool throwStockException)
		{
			if (type.Equals(typeof(Batch)))
			{
				_partidas = Batchs.GetChildListFromList(this, list, get_childs);
			}
			else if (type.Equals(typeof(Stock)))
			{
				_stocks = Stocks.GetChildListByPartidaFromList(this, list, get_childs);
                _stocks.UpdateStocks(this, throwStockException);
			}
		}

        public virtual void LoadStockByPartida(long oid, bool childs, bool throwStockException)
		{
			if (_stocks.GetItemByBatch(oid) == null)
			{
				Stocks stocks = Stocks.GetChildListByPartida(this, oid, childs);

				foreach (Stock item in stocks)
					_stocks.AddItem(item);

                _stocks.UpdateStocks(Partidas.GetItem(oid), throwStockException);
			}
		}

		public virtual void LoadConceptosAlbaranes(bool childs)
		{
			if (Conceptos == null)
			{
				Conceptos = InputDeliveryLineList.GetByExpedienteList(Oid, false);
			}
		}

        public virtual void LoadConceptosStockAlbaranes(bool childs)
        {
            if (Conceptos == null)
            {
                Conceptos = InputDeliveryLineList.GetByExpedienteStockList(Oid, false);
            }
        }

		#endregion

		#region Root Factory Methods

		public static Expedient New() { return New(ETipoExpediente.Todos); }
        public static Expedient New(ETipoExpediente tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            Expedient exp = DataPortal.Create<Expedient>(new CriteriaCs(-1));
			exp.Tipo = (long)tipo;
			exp.GetNewCode();
			if (tipo == ETipoExpediente.Almacen) exp.FechaSalidaMuelle = DateTime.Now;
            return exp;
        }

        public static Expedient Get(long oid) { return Get(oid, true); }
        public static Expedient Get(long oid, bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Expedient.SELECT(oid);

            Expedient.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<Expedient>(criteria);
        }
        public static Expedient Get(long oid, bool childs, bool cache) 
        {
            Expedient item;

            //No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(Expedients)))
            {
				Expedients items = Library.Store.Expedients.NewList();

                 item = Expedient.GetChild(oid, childs);
                 items.AddItem(item);
				 items.SessionCode = item.SessionCode;
                 Cache.Instance.Save(typeof(Expedients), items);
            }
			else
			{
			    Expedients items = Cache.Instance.Get(typeof(Expedients)) as Expedients;
                item = items.GetItem(oid);

                //No está en la lista de la cache de listas
                if (item == null)
                {
					item = Expedient.GetChild(items.SessionCode, oid, childs);
                    items.AddItem(item);
                    Cache.Instance.Save(typeof(Expedients), items);
                }
			}

            return item;
        }
		public static Expedient Get(long oid, bool childs, bool cache, int sessionCode)
		{
			Expedient item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(Expedients)))
			{
				Expedients items = Library.Store.Expedients.NewList();

				item = Expedient.GetChild(sessionCode, oid, childs);
				items.AddItem(item);
				items.SessionCode = item.SessionCode;
				Cache.Instance.Save(typeof(Expedients), items);
			}
			else
			{
				Expedients items = Cache.Instance.Get(typeof(Expedients)) as Expedients;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = Expedient.GetChild(items.SessionCode != -1 ? items.SessionCode : sessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(Expedients), items);
				}
			}

			return item;
		}

        public static Expedient GetAll(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Expedient.SELECT(oid);
            else
                criteria.AddOidSearch(oid);

            Expedient.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<Expedient>(criteria.Query);
        }

        /// <summary>
        /// Devuelve el Expediente que hace de Almacen
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static Expedient GetAlmacen() { return Expedient.Get(1, true); }
        public static Expedient GetAlmacen(bool childs) { return Expedient.Get(1, childs); }

        public static Expedient GetByPartida(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Expedient.SELECT_BY_PARTIDA(oid);
            else
                criteria.AddOidSearch(oid);

            Expedient.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<Expedient>(criteria);
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

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todos los Expediente. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Expedient.OpenSession();
            ISession sess = Expedient.Session(sessCode);
            ITransaction trans = Expedient.BeginTransaction(sessCode);

            try
            {
				sess.Delete("from ExpedientRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                Expedient.CloseSession(sessCode);
            }
        }

        public override Expedient Save()
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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
                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                base.Save();

                if (_save_childs)
                {
                    _partidas.Update(this);
                    _maquinarias.Update(this);
                    _stocks.Update(this);
                    _gastos.Update(this);
					_expedientes_fomento.Update(this);
					_expedientes_rea.Update(this);
                    _relations.Update(this);

                    UpdateFacturas(true);
                }

                Transaction().Commit();
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
                Cache.Instance.Remove(typeof(LivestockBooks));

                if (CloseSessions) CloseSession(); 
				else BeginTransaction();
            }
        }

		public override Expedient SaveAsChild()
		{
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				base.SaveAsChild();

				if (_save_childs)
				{
					_partidas.Update(this);
					//_cabezas.Update(this);
					_maquinarias.Update(this);
					_stocks.Update(this);
					_gastos.Update(this);
					_expedientes_fomento.Update(this);
				}

				return this;
			}
			catch (Exception ex)
			{
				//if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return null;
			}
			finally	{}
		}

        private void UpdateFacturas(bool throwStockException)
        {
            if (Facturas == null || Facturas.Count == 0) return;

            List<long> list = new List<long>();

            foreach(InputInvoiceInfo f in Facturas)
                list.Add(f.Oid);

            QueryConditions conditions = new QueryConditions();
            conditions.OidList = list;
            InputInvoices facturas = InputInvoices.GetList(conditions, true, SessionCode);

            if (facturas != null)
            {
                foreach (InputInvoice fac in facturas)
                {
                    foreach (InputInvoiceLine cf in fac.Conceptos)
                    {
                        InputInvoiceInfo item = Facturas.GetItem(fac.Oid);

                        if (item.Conceptos == null || item.Conceptos.Count == 0)
                            item.LoadChilds(typeof(InputInvoiceLine), false);

                        if (!item.Conceptos.GetItem(cf.Oid).IsSelected)
                            cf.OidExpediente = 0;
                        else
                        {
                            if (cf.OidExpediente != Oid)
                                cf.OidExpediente = Oid;
                        }
                    }
                }

                facturas.SaveAsChild();
            }
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            _base.Record.Oid = (long)(new Random()).Next();
            Ano = DateTime.Now.Year;
            FechaFacProveedor = DateTime.MinValue;
			Ayuda = true;
			FechaSalidaMuelle = DateTime.MaxValue;
			EstimarDespachante = true;
			EstimarNaviera = true;
			EstimarTOrigen = true;
			EstimarTDestino = true;
        }

        private void Fetch(Expedient source)
        {
            try
            {
                SessionCode = source.SessionCode;

				_base.CopyValues(source);

                if (Childs)
                {
                    if (nHMng.UseDirectSQL)
                    {
                        IDataReader reader;
                        string query;

                        Batch.DoLOCK(Session());
                        query = Batchs.SELECT(this, true);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_partidas = Batchs.GetChildList(SessionCode, reader);

                        Expense.DoLOCK(Session());
                        query = Expenses.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_gastos = Expenses.GetChildList(SessionCode, reader);

                        Stock.DoLOCK(Session());
                        query = Stocks.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_stocks = Stocks.GetChildList(SessionCode, reader);

						LineaFomento.DoLOCK(Session());
						query = LineasFomento.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_fomento = LineasFomento.GetChildList(reader);

						REAExpedient.DoLOCK(Session());
						query = REAExpedients.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_rea = REAExpedients.GetChildList(SessionCode, reader);

                        UpdateGastosPartidas(true);
                        UpdateTotalesProductos(_partidas, true);
                        UpdateStocks(true);
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
                    IDataReader reader;
                    string query;

                    Batch.DoLOCK(Session());
                    query = Batchs.SELECT(this, true);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_partidas = Batchs.GetChildList(SessionCode, reader);

                    Expense.DoLOCK(Session());
                    query = Expenses.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_gastos = Expenses.GetChildList(SessionCode, reader);

                    Stock.DoLOCK(Session());
                    query = Stocks.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_stocks = Stocks.GetChildList(SessionCode, reader);

					LineaFomento.DoLOCK(Session());
					query = LineasFomento.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_expedientes_fomento = LineasFomento.GetChildList(reader);

					REAExpedient.DoLOCK(Session());
					query = REAExpedients.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_expedientes_rea = REAExpedients.GetChildList(SessionCode, reader);

                    UpdateGastosPartidas(true);
                    UpdateTotalesProductos(_partidas, true);
                    UpdateStocks(true);
                }
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
        internal void Insert(Expedients parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);

                if (SaveChilds)
                {
                    _maquinarias.Update(this);
                    _partidas.Update(this);
                    _stocks.Update(this);
                    _gastos.Update(this);
					_expedientes_fomento.Update(this);
                }
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
        internal void Update(Expedients parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                ExpedientRecord obj = Session().Get<ExpedientRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);

                if (SaveChilds)
                {
                    _maquinarias.Update(this);
                    _partidas.Update(this);
                    _stocks.Update(this);
                    _gastos.Update(this);
                    _expedientes_fomento.Update(this);
                }
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
        internal void DeleteSelf(Expedients parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<ExpedientRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
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
                    Expedient.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        switch ((ETipoExpediente)Tipo)
                        {
                            case ETipoExpediente.Ganado:
                                {
                                   /* Cabeza.DoLOCK(Session());
                                    query = Cabezas.SELECT(this);
                                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                    _cabezas = Cabezas.GetChildList(reader);*/
                                } break;

                            case ETipoExpediente.Maquinaria:
                                {
                                    Maquinaria.DoLOCK(Session());
                                    query = Maquinarias.SELECT(this);
                                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                    _maquinarias = Maquinarias.GetChildList(reader);
                                } break;
                        }

                        Batch.DoLOCK(Session());
                        query = Batchs.SELECT(this, true);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_partidas = Batchs.GetChildList(SessionCode, reader);

                        Expense.DoLOCK(Session());
                        query = Expenses.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_gastos = Expenses.GetChildList(SessionCode, reader);

                        Stock.DoLOCK(Session());
                        query = Stocks.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _stocks = Stocks.GetChildList(SessionCode, reader);

						LineaFomento.DoLOCK(Session());
						query = LineasFomento.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_fomento = LineasFomento.GetChildList(reader);

						REAExpedient.DoLOCK(Session());
						query = REAExpedients.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_rea = REAExpedients.GetChildList(SessionCode, reader);

                        UpdateTotalesProductos(_partidas, true);
                        UpdateStocks(true);
						UpdateTotalCostesCompensables();
                    }
                }

                MarkOld();
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {
            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                //GetNewCode(); //Se le da el nuevo código al hacer el New del Expediente, si se vuelve a hacer aquí se elimina el nombre que se le haya dado
                Session().Save(Base.Record);
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
                    ExpedientRecord obj = Session().Get<ExpedientRecord>(Oid);
                
					if ((obj.FechaDespachoDestino != FechaDespachoDestino) && (FechaDespachoDestino != DateTime.MinValue))
						UpdateLineasLibroGanadero();
					
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

                //Si no hay integridad referencial, aquí se deben borrar las listas hijo
                // TODO: Se puede borrar un expediente que no se ha facturado, pero tiene toda esta info?.

                //Si no hay integridad referencial, aquí se deben borrar las listas hijo
                Session().Delete(Session().Get<ExpedientRecord>(criteria.Oid));

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

        internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() { };
        }

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT_BY_PARTIDA(long oid_partida)
		{
			string pe = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Partida = Batch.NewChild().GetInfo(false) };
			conditions.Partida.Oid = oid_partida;

			query = JOIN(conditions) +
					" INNER JOIN " + pe + " AS PE2 ON PE2.\"OID_EXPEDIENTE\" = E.\"OID\"" +
					WHERE(conditions);

			query += " AND PE2.\"OID\" = " + oid_partida.ToString();

			return query;
		}

        internal static string FIELDS()
        {
            return FIELDS(new QueryConditions {});
        }
        internal static string FIELDS(QueryConditions conditions)
        {
            string query;

            query = @"
            SELECT 0 AS ""QUERY""
                    ,E.*";

            switch (conditions.TipoAcreedor[0])
            {
                case ETipoAcreedor.Proveedor:
                    query += @"
                        ,'' AS ""NAVIERA""
                        ,'' AS ""TRANS_ORIGEN""
                        ,'' AS ""TRANS_DESTINO""
                        ,P.""NOMBRE"" AS ""PROVEEDOR""
                        ,'' AS ""DESPACHANTE""";
                    break;

                case ETipoAcreedor.Naviera:
                    query += @"
                        ,NAV.""NOMBRE"" AS ""NAVIERA""
                        ,'' AS ""TRANS_ORIGEN""
                        ,'' AS ""TRANS_DESTINO""
                        ,'' AS ""PROVEEDOR""
                        ,'' AS ""DESPACHANTE""";
                    break;

                case ETipoAcreedor.Despachante:
                    query += @"
                        ,'' AS ""NAVIERA""
                        ,'' AS ""TRANS_ORIGEN""
                        ,'' AS ""TRANS_DESTINO""
                        ,'' AS ""PROVEEDOR""
                        ,D.""NOMBRE"" AS ""DESPACHANTE""";
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                    query += @"
                        ,'' AS ""NAVIERA""
                        ,T_ORIG.""NOMBRE"" AS ""TRANS_ORIGEN""
                        ,'' AS ""TRANS_DESTINO""
                        ,'' AS ""PROVEEDOR""
                        ,'' AS ""DESPACHANTE""";
                    break;

                case ETipoAcreedor.TransportistaDestino:
                    query += @"
                        ,'' AS ""NAVIERA""
                        ,'' AS ""TRANS_ORIGEN""
                        ,T_DEST.""NOMBRE"" AS ""TRANS_DESTINO""
                        ,'' AS ""PROVEEDOR""
                        ,'' AS ""DESPACHANTE""";
                    break;

                case ETipoAcreedor.Todos:
                    query += @"
                        ,COALESCE(NAV.""NOMBRE"", '') AS ""NAVIERA""
                        ,COALESCE(T_ORIG.""NOMBRE"", '') AS ""TRANS_ORIGEN""
                        ,COALESCE(T_DEST.""NOMBRE"", '') AS ""TRANS_DESTINO""
                        ,COALESCE(P.""NOMBRE"", '') AS ""PROVEEDOR""
                        ,COALESCE(D.""NOMBRE"", '') AS ""DESPACHANTE""";
                    break;

                default:
                    query += @"
                        ,'' AS ""NAVIERA""
                        ,'') AS ""TRANS_ORIGEN""
                        ,'' AS ""TRANS_DESTINO""
                        ,'' AS ""PROVEEDOR""
                        ,'' AS ""DESPACHANTE""";
                    break;
            }
            
            query += @"
                ,PE.""TOTAL_REA_ESTIMADA""
                ,CR.""TOTAL_REA_COBRADA""
                ,(COALESCE(PE.""TOTAL_REA_ESTIMADA"", 0) - COALESCE(CR.""TOTAL_REA_COBRADA"", 0)) AS ""TOTAL_REA_PENDIENTE""
                ,PE.""STOCK_K_INICIAL"" AS ""STOCK_K_INICIAL""
                ,PE.""STOCK_B_INICIAL"" AS ""STOCK_B_INICIAL""
                ,S.""STOCK_K"" AS ""STOCK_K""
                ,S.""STOCK_B"" AS ""STOCK_B""";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @" 
            WHERE (E.""FECHA_EMBARQUE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"' OR E.""FECHA_EMBARQUE"" IS NULL OR E.""FECHA_EMBARQUE"" = '-infinity')";

			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "E");

			if ((conditions.Expedient != null) && (conditions.Expedient.Oid != 0)) 
                query += @"
                    AND E.""OID"" = " + conditions.Expedient.Oid;
			
            if (conditions.TipoExpediente != ETipoExpediente.Todos) 
                query += @"
                    AND E.""TIPO_EXPEDIENTE"" = " + (long)conditions.TipoExpediente;
			
            if (conditions.Naviera != null) 
                query += @"
                    AND E.""OID_NAVIERA"" = " + conditions.Naviera.Oid;
			
            if (conditions.Proveedor != null) 
                query += @"
                    AND E.""OID_PROVEEDOR"" = " + conditions.Proveedor.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
            string cu = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.CustomAgentRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.TransporterRecord));
            string sh = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ShippingCompanyRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.REAChargeRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));			

			string query = @"
            FROM " + ex + " AS E";

            switch (conditions.TipoAcreedor[0])
            {
                case ETipoAcreedor.Proveedor:
                    query += @"
                    LEFT JOIN " + su + @" AS P ON P.""OID"" = E.""OID_PROVEEDOR""";
                    break;

                case ETipoAcreedor.Despachante:
                    query += @"
                    LEFT JOIN " + cu + @" AS D ON D.""OID"" = E.""OID_DESPACHANTE""";
                    break;

                case ETipoAcreedor.TransportistaDestino:
                    query += @"
                    LEFT JOIN " + tr + @" AS T_ORIG ON T_ORIG.""OID"" = E.""OID_TRANS_ORIGEN""";
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                    query += @"
                    LEFT JOIN " + tr + @" AS T_DEST ON T_DEST.""OID"" = E.""OID_TRANS_DESTINO""";
                    break;

                case ETipoAcreedor.Naviera:
                    query += @"
                    LEFT JOIN " + sh + @" AS NAV ON NAV.""OID"" = E.""OID_NAVIERA""";
                    break;

                default:
                    query += @"
                    LEFT JOIN " + su + @" AS P ON P.""OID"" = E.""OID_PROVEEDOR""
                    LEFT JOIN " + cu + @" AS D ON D.""OID"" = E.""OID_DESPACHANTE""
                    LEFT JOIN " + tr + @" AS T_ORIG ON T_ORIG.""OID"" = E.""OID_TRANS_ORIGEN""
                    LEFT JOIN " + tr + @" AS T_DEST ON T_DEST.""OID"" = E.""OID_TRANS_DESTINO""
                    LEFT JOIN " + sh + @" AS NAV ON NAV.""OID"" = E.""OID_NAVIERA""";
                    break;
            }

            query += @"
            LEFT JOIN (SELECT ""OID_EXPEDIENTE""
                            ,SUM(""KILOS_INICIALES"") AS ""STOCK_K_INICIAL""
                            ,SUM(""BULTOS_INICIALES"") AS ""STOCK_B_INICIAL""
                            ,SUM(""AYUDA_RECIBIDA_KILO"" * ""KILOS_INICIALES"") AS ""TOTAL_REA_ESTIMADA""
                        FROM " + ba + @" GROUP BY ""OID_EXPEDIENTE"")
                AS PE ON PE.""OID_EXPEDIENTE"" = E.""OID""
            LEFT JOIN (SELECT ""OID_EXPEDIENTE""
                            ,SUM(""KILOS"") AS ""STOCK_K""
                            ,SUM(""BULTOS"") AS ""STOCK_B""
                        FROM " + st + @" GROUP BY ""OID_EXPEDIENTE"")
                AS S ON S.""OID_EXPEDIENTE"" = E.""OID""
            LEFT JOIN (SELECT ""OID_EXPEDIENTE""
                            ,MAX(""FECHA"") AS ""FECHA_COBRO_REA""
                            ,MAX(CB.""VALOR"") AS ""CUENTA_COBRO_REA""
                            ,SUM(""CANTIDAD"") AS ""TOTAL_REA_COBRADA""
                        FROM " + cr + @" AS CR1
                        INNER JOIN " + ch + @" AS C ON C.""OID"" = CR1.""OID_COBRO"" AND C.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                        LEFT JOIN " + bk + @" AS CB ON CB.""OID"" = C.""OID_CUENTA_BANCARIA""
                        GROUP BY ""OID_EXPEDIENTE"")
                AS CR ON E.""OID"" = CR.""OID_EXPEDIENTE""";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Expedient = ExpedientInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query =
                FIELDS(conditions) +
                JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "E", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

            //query += Common.EntityBase.LOCK("E", lockTable);

            return query;
		}

		internal static string SELECT_BY_REA(ETipoExpediente t_exp, ECobro e_cobro)
		{
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.REAChargeRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string cbn = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));

			string query =

			query = " SELECT 1 AS \"QUERY\"" +
					"		,EX.*" +
					"		,ER.\"OID\" AS \"OID_EXPEDIENTE_REA\"" +
					"		,ER.\"COBRADO\" AS \"COBRADO_REA\"" +
					"       ,CR.\"FECHA_COBRO_REA\"" +
					"       ,CR.\"CUENTA_COBRO_REA\"" +
					"       ,COALESCE(PA.\"TOTAL_REA_ESTIMADA\", 0)" +
					"       ,CR.\"TOTAL_REA_COBRADA\"" +
					"       ,(COALESCE(PA.\"TOTAL_REA_ESTIMADA\", 0) - COALESCE(CR.\"TOTAL_REA_COBRADA\", 0)) AS \"TOTAL_REA_PENDIENTE\"" +
					" FROM " + ex + " AS EX" +
					" LEFT JOIN " + er + " AS ER ON ER.\"OID_EXPEDIENTE\" = EX.\"OID\"" +
					" LEFT JOIN (SELECT PA1.\"OID_EXPEDIENTE\", PR.\"CODIGO_ADUANERO\"" +
					"					,SUM(PA1.\"AYUDA_RECIBIDA_KILO\" * PA1.\"KILOS_INICIALES\") AS \"TOTAL_REA_ESTIMADA\"" +
					"             FROM " + pa + " AS PA1" +
					"			  INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PA1.\"OID_PRODUCTO\"" +
					"             GROUP BY \"OID_EXPEDIENTE\", \"CODIGO_ADUANERO\")" +
					"      AS PA ON E.\"OID\" = PA.\"OID_EXPEDIENTE\" AND PA.\"CODIGO_ADUANERO\" = ER.\"CODIGO_ADUANERO\" AND EX.\"TIPO_EXPEDIENTE\" != " + ((long)ETipoExpediente.Almacen) +
					" LEFT JOIN (SELECT CR1.\"OID_EXPEDIENTE_REA\"," +
					"                   MAX(C.\"FECHA\") AS \"FECHA_COBRO_REA\"," +
					"                   MAX(CB.\"VALOR\") AS \"CUENTA_COBRO_REA\"," +
					"                   SUM(CR1.\"CANTIDAD\") AS \"TOTAL_REA_COBRADA\"" +
					"               FROM " + cr + " AS CR1" +
					"               INNER JOIN " + cb + " AS C ON C.\"OID\" = CR1.\"OID_COBRO\" AND C.\"TIPO_COBRO\" = " + (long)ETipoCobro.REA +
					"               LEFT JOIN " + cbn + " AS CB ON CB.\"OID\" = C.\"OID_CUENTA_BANCARIA\"" +
					"               GROUP BY \"OID_EXPEDIENTE_REA\")" +
					"      AS CR ON CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\"" +
					" WHERE EX.\"AYUDA\" = TRUE";

			switch (e_cobro)
			{
				case ECobro.Cobrado:
                    query += " AND (CR.\"TOTAL_REA_COBRADA\" = PA.\"TOTAL_REA_ESTIMADA\" OR ER.\"ESTADO\" IN (" + (long)EEstado.Charged + ", " + (long)EEstado.Exportado + ")";
					break;

				case ECobro.Pendiente:
                    query += " AND ((CR.\"TOTAL_REA_COBRADA\" < PA.\"TOTAL_REA_ESTIMADA\" OR CR.\"TOTAL_REA_COBRADA\" IS NULL) AND ER.\"ESTADO\" NOT IN (" + (long)EEstado.Charged + ", " + (long)EEstado.Exportado + ")";
					break;
			}

			if (t_exp != ETipoExpediente.Todos)
				query += " AND E.\"TIPO_EXPEDIENTE\" = " + ((long)t_exp).ToString();

			return query;
		}

        internal static string SELECT_BY_STOCKS_ENLAZADOS(long oidEnlaceStock, bool lockTable = true)
        {
            string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));

            QueryConditions conditions = new QueryConditions();

            conditions.ExtraJoin = @"
			INNER JOIN ( SELECT ""OID_EXPEDIENTE""
                            ,""OID_ENLACE""
                        FROM " + st + @" 
                        GROUP BY ""OID_EXPEDIENTE"", ""OID_ENLACE"")
                AS ST ON ST.""OID_EXPEDIENTE"" = E.""OID""";
            
            conditions.ExtraWhere = @"
                AND ST.""OID_ENLACE"" = " + oidEnlaceStock;

            return SELECT(conditions, lockTable);
                
        }

        #endregion
    }

	public static class Obra
	{
        public static void EditaGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines)
		{
            ReparteGasto(expedient, inInvoice, deliveryLines);
		}

        public static void NuevoGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines)
		{
            ReparteGasto(expedient, inInvoice, deliveryLines);
		}
        public static void NuevoGasto(Expedient expedient, Expense expense, InputDeliveryLineList deliveryLines)
		{
            ReparteGasto(expedient, expense, deliveryLines);
		}

        public static void RemoveGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines)
		{
            Expense expense = expedient.Gastos.GetItemByFactura(inInvoice);

            if (expense != null) expedient.Gastos.Remove(expedient, expense);
		}
        public static void RemoveGasto(Expedient expedient, Expense expense, InputDeliveryLineList deliveryLines)
		{
            if (expense != null) expedient.Gastos.Remove(expedient, expense);
		}
		
		public static void ReparteGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines) 
		{
			foreach (InputInvoiceLineInfo cf in inInvoice.Conceptos)
			{
				if ((cf.IsSelected) || (cf.OidExpediente == expedient.Oid))
				{
                     expedient.Gastos.NewItem(expedient, inInvoice, cf, null);
 				}
				else
				{
					Expense expense = expedient.Gastos.GetItemByConceptoFactura(cf);
					if (expense != null)
						expedient.Gastos.Remove(expedient, expense);
				}
			}

		}
        public static void ReparteGasto(Expedient expedient, Expense expense, InputDeliveryLineList deliveryLines)
		{
			expedient.Gastos.NewItem(expedient, expense, null);
		}

        public static void UpdateGastosPartidas(Expedient expedient, Batchs batchs) { }

		public static void UpdateTotalesGastos(Expedient expedient)
		{
			expedient.GastosGenerales = 0;
			expedient.OtrosGastos = 0;
			expedient.GProvTotal = 0;
			expedient.GNavTotal = 0;
			expedient.GDespTotal = 0;
			expedient.GTransTotal = 0;
			expedient.GTransDestTotal = 0;
			expedient.OtrosGastosFacturas = 0;

			foreach (Expense item in expedient.Gastos)
			{
				if (item.OidFactura == 0)
					expedient.OtrosGastos += item.Total;
				else
					expedient.OtrosGastosFacturas += item.Total;
			}
		}
	}

	public static class Contenedor
	{
		public static void EditaGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines, bool throwStockException)
		{
			switch (inInvoice.ETipoAcreedor)
			{
				case ETipoAcreedor.Naviera:

					if (expedient.OidFacturaNav == inInvoice.Oid)
					{
						expedient.GNavTotal = inInvoice.BaseImponible;

						InputInvoiceLineInfo cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultFleteSetting());
						expedient.FleteNeto = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultT3OrigenSetting());
						expedient.T3Origen = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultT3DestinoSetting());
						expedient.T3Destino = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultTHCOrigenSetting());
						expedient.ThcOrigen = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultTHCDestinoSetting());
						expedient.ThcDestino = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultISPSSetting());
						expedient.Isps = (cfp != null) ? cfp.BaseImponible : 0;

						cfp = inInvoice.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultBAFSetting());
						expedient.Baf = (cfp != null) ? cfp.BaseImponible : 0;

						expedient.UpdateTotalCostesCompensables();
					}
					break;
			}

			NuevoGasto(expedient, inInvoice, deliveryLines, throwStockException);
            //ReparteGasto(expediente, fac, conceptos);
		}

        public static void NuevoGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines, bool throwStockException)
		{
			bool principal;
			
			/// Existe gasto de NAVIERA asociado al expediente
			if (inInvoice.ETipoAcreedor == ETipoAcreedor.Naviera)
				principal = !expedient.ExistsGastoPrincipalAcreedor(ETipoAcreedor.Naviera);
			else
				principal = true;

			if (principal)
			{
				switch (inInvoice.ETipoAcreedor)
				{
					case ETipoAcreedor.Proveedor:

						if (expedient.OidFacturaPro == 0)
						{
							expedient.Proveedor = inInvoice.Acreedor;
							expedient.OidProveedor = inInvoice.OidAcreedor;
							expedient.OidFacturaPro = inInvoice.Oid;
							expedient.FechaFacProveedor = inInvoice.Fecha;
						}

						break;

					case ETipoAcreedor.Naviera:

						expedient.EstimarNaviera = false;

						expedient.Naviera = inInvoice.Acreedor;
						expedient.OidNaviera = inInvoice.OidAcreedor;
						expedient.OidFacturaNav = inInvoice.Oid;
						expedient.GNavFac = inInvoice.NFactura;
						expedient.GNavTotal = inInvoice.BaseImponible;

						//Datos de Fomento
						if (inInvoice.Conceptos == null) inInvoice.LoadChilds(typeof(InputInvoiceLine), false);

						if (expedient.ExpedientesFomento.Count == 0) expedient.LoadChilds(typeof(LineaFomento), false, throwStockException);
						expedient.ExpedientesFomento.SetValues(expedient, inInvoice);

						break;

					case ETipoAcreedor.TransportistaOrigen:

						expedient.EstimarTOrigen = false;

						if (expedient.OidFacturaTor == 0)
						{
							expedient.NombreTransOrig = inInvoice.Acreedor;
							expedient.OidTransOrigen = inInvoice.OidAcreedor;
							expedient.OidFacturaTor = inInvoice.Oid;
							expedient.GTransFac = inInvoice.NFactura;
						}

						break;

					case ETipoAcreedor.TransportistaDestino:

						expedient.EstimarTDestino = false;

						if (expedient.OidFacturaTde == 0)
						{
							expedient.NombreTransDest = inInvoice.Acreedor;
							expedient.OidTransDestino = inInvoice.OidAcreedor;
							expedient.OidFacturaTde = inInvoice.Oid;
							expedient.GTransDestFac = inInvoice.NFactura;
							expedient.GTransDestIgic = inInvoice.Impuestos;
						}

						break;

					case ETipoAcreedor.Despachante:

						expedient.EstimarDespachante = false;

						if (expedient.OidFacturaDes == 0)
						{
							expedient.Despachante = inInvoice.Acreedor;
							expedient.OidDespachante = inInvoice.OidAcreedor;
							expedient.OidFacturaDes = inInvoice.Oid;
							expedient.GDespFac = inInvoice.NFactura;

							//Datos de Expedientes REA
                            if (expedient.ExpedientesREA.Count == 0) expedient.LoadChilds(typeof(REAExpedients), false, throwStockException);
							expedient.ExpedientesREA.SetValues(inInvoice);
						}

						break;
				}

				expedient.SetCode(inInvoice.ETipoAcreedor);
			}

			ReparteGasto(expedient, inInvoice, deliveryLines);
		}
		public static void NuevoGasto(Expedient expedient, Expense expense, InputDeliveryLineList conceptos)
		{
			ReparteGasto(expedient, expense, conceptos);
		}

		public static void RemoveGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines, bool throwStockException)
		{
			switch (expedient.ETipoExpediente)
			{
				case ETipoExpediente.Alimentacion:
				case ETipoExpediente.Ganado:
				case ETipoExpediente.Maquinaria:
					{
						switch (inInvoice.ETipoAcreedor)
						{
							case ETipoAcreedor.Proveedor:

								if (expedient.OidFacturaPro == inInvoice.Oid)
								{
									expedient.OidFacturaPro = 0;
									expedient.FechaFacProveedor = DateTime.MinValue;
									expedient.GProvFac = "";
									expedient.GProvTotal = 0;
								}
								break;

							case ETipoAcreedor.Naviera:

								if (expedient.OidFacturaNav == inInvoice.Oid)
								{
									expedient.EstimarNaviera = true;

									expedient.OidFacturaNav = 0;
									expedient.GNavFac = "";
									expedient.GNavTotal = 0;

									expedient.FleteNeto = 0;
									expedient.T3Origen = 0;
									expedient.T3Destino = 0;
									expedient.ThcOrigen = 0;
									expedient.ThcDestino = 0;
									expedient.Isps = 0;
									expedient.Baf = 0;

									expedient.UpdateTotalCostesCompensables();
								}
								break;

							case ETipoAcreedor.TransportistaOrigen:

								if (expedient.OidFacturaTor == inInvoice.Oid)
								{
									expedient.EstimarTOrigen = true;

									expedient.OidFacturaTor = 0;
									expedient.GTransFac = "";
									expedient.GTransTotal = 0;
								}
								break;

							case ETipoAcreedor.TransportistaDestino:

								if (expedient.OidFacturaTde == inInvoice.Oid)
								{
									expedient.EstimarTDestino = true;

									expedient.OidFacturaTde = 0;
									expedient.GTransDestFac = "";
									expedient.GTransDestTotal = 0;
								}
								break;

							case ETipoAcreedor.Despachante:

								if (expedient.OidFacturaDes == inInvoice.Oid)
								{
									expedient.EstimarDespachante = true;

									expedient.OidFacturaDes = 0;
									expedient.GDespFac = "";
									expedient.GDespTotal = 0;
									expedient.GDespIgicServ = 0;

									//Datos de Expedientes REA
                                    if (expedient.ExpedientesREA.Count == 0) expedient.LoadChilds(typeof(REAExpedients), false, throwStockException);
									expedient.ExpedientesREA.SetValues(null);
								}
								break;
						}
					}
					break;
			}

			foreach (InputInvoiceLineInfo cf in inInvoice.Conceptos)
			{
				foreach (InputDeliveryLineInfo ca in deliveryLines)
				{
					Expense gasto = expedient.Gastos.GetItemByConceptos(cf, ca);
					if (gasto != null) expedient.Gastos.Remove(expedient, gasto);
				}
			}

			Expense gastoFactura = expedient.Gastos.GetItemByFactura(inInvoice, expedient);
			while (gastoFactura != null)
			{
				expedient.Gastos.Remove(expedient, gastoFactura);
				gastoFactura = expedient.Gastos.GetItemByFactura(inInvoice, expedient);
			}
		}
		public static void RemoveGasto(Expedient expedient, Expense expense, InputDeliveryLineList deliveryLines)
		{
			foreach (InputDeliveryLineInfo ca in deliveryLines)
			{
				Expense item = expedient.Gastos.GetItem(expense, ca);
				if (item != null) expedient.Gastos.Remove(expedient, item);
			}
		}

		public static void ReparteGasto(Expedient expedient, InputInvoiceInfo inInvoice, InputDeliveryLineList deliveryLines)
		{
            expedient.LoadConceptosStockAlbaranes(false);

            if (inInvoice.Conceptos == null)
                inInvoice.LoadChilds(typeof(InputInvoiceLine), false);

			foreach (InputInvoiceLineInfo cf in inInvoice.Conceptos)
			{
				Decimal gasto_kilo = (expedient.KilosTotal > 0) ? cf.BaseImponible / expedient.KilosTotal : 0;

				if (((cf.IsSelected) || (cf.OidExpediente == expedient.Oid)) && (cf.OidPartida == 0))
				{
					foreach (InputDeliveryLineInfo ca in deliveryLines)
					{
                        //Si esto ocurre es porque se están pasando conceptos erróneos que no tienen stock asociado
                        if (ca.OidPartida == 0) continue;

						Expense gasto = expedient.Gastos.GetItemByConceptos(cf, ca);

						//No hay gasto previo para esta combinación
						if (gasto == null)
							gasto = expedient.Gastos.NewItem(expedient, inInvoice, cf, ca);
						else
							gasto.CopyFrom(expedient, inInvoice, cf, ca);

						gasto.Total = gasto_kilo * ca.CantidadKilos;
					}

                    foreach (InputDeliveryLineInfo ce in expedient.Conceptos)
                    {
                        if (!deliveryLines.Contains(ce.Oid))
                        {
                            Expense gasto = expedient.Gastos.GetItemByConceptos(cf, ce);
                            if (gasto != null)
                                expedient.Gastos.Remove(expedient, gasto);
                        }
                    }
				}
				else
				{
					foreach (InputDeliveryLineInfo ca in deliveryLines)
					{
						Expense gasto = expedient.Gastos.GetItemByConceptos(cf, ca);
						if (gasto != null)
							expedient.Gastos.Remove(expedient, gasto);
					}
				}
			}
		}
		public static void ReparteGasto(Expedient expedient, Expense expense, InputDeliveryLineList deliveryLines)
		{
			Decimal gasto_kilo = (expedient.KilosTotal > 0) ? expense.Total / expedient.KilosTotal : 0;

			foreach (InputDeliveryLineInfo ca in deliveryLines)
			{
				Expense gastoConcepto = expedient.Gastos.GetItem(expense, ca);

				//No hay gasto previo para esta combinación
				if (gastoConcepto == null)
					gastoConcepto = expedient.Gastos.NewItem(expedient, expense, ca);
				else
					gastoConcepto.CopyFrom(expedient, expense, ca);

				gastoConcepto.Total = gasto_kilo * ca.CantidadKilos;
			}
		}

		public static void UpdateAyudas(Expedient expedient)
		{
			bool cobrado = (expedient.ExpedientesREA.Count > 0);

			expedient.AyudaCobrada = 0;

			foreach (REAExpedient item in expedient.ExpedientesREA)
			{
				expedient.AyudaCobrada += item.AyudaCobrada;
			}

			expedient.AyudaEstimada = 0;

			foreach (Batch item in expedient.Partidas)
			{
				if (!item.Ayuda) continue;
				expedient.AyudaEstimada += item.AyudaKiloEstimada * item.KilosIniciales;
			}

			expedient.AyudaPendiente = (expedient.AyudaEstimada - expedient.AyudaCobrada) > 0 ? (expedient.AyudaEstimada - expedient.AyudaCobrada) : 0;
			expedient.AyudaPendiente = (cobrado) ? 0 : expedient.AyudaPendiente;
		}

        public static void UpdateGastosPartidas(Expedient expedient, Batchs batchs, bool throwStockException)
		{
			InputDeliveryLineList conceptos = InputDeliveryLineList.GetByExpedienteStockList(expedient.Oid, false, true);
			
			if (expedient.Ayuda)
				if (expedient.ExpedientesREA.Count == 0)
                    expedient.LoadChilds(typeof(REAExpedient), true, throwStockException);

			expedient.KilosTotal = 0;

			foreach (Batch item in batchs)
			{
				if (item.OidExpediente != expedient.Oid) continue;
				expedient.KilosTotal += item.KilosIniciales;
			}

			decimal ayudas = 0;
			decimal kilos = 0;
			decimal ayuda_kilo = 0;

			foreach (Batch item in batchs)
			{
				if (item.OidExpediente != expedient.Oid) continue;
                
				if (expedient.Ayuda)
				{
					ayudas = expedient.ExpedientesREA.GetTotalAyudas(item.CodigoAduanero);
					kilos = batchs.GetTotalKilos(item.CodigoAduanero);
					ayuda_kilo = ayudas / kilos;
				}
				else
					ayuda_kilo = 0;

				item.CalculaCostes(expedient.GastoPorKilo, ayuda_kilo);
			}
		}

		public static void UpdateTotalesGastos(Expedient expedient)
		{
            //decimal gastos_naviera = expediente.GNavTotal;
            //decimal gastos_despachante = expediente.GDespTotal;
            //decimal gastos_torigen = expediente.GTransTotal;
            //decimal gastos_tdestino = expediente.GTransDestTotal;

			expedient.GastosGenerales = 0;
			expedient.OtrosGastos = 0;
			expedient.GProvTotal = 0;
			expedient.GNavTotal = 0;
			expedient.GDespTotal = 0;
			expedient.GTransTotal = 0;
			expedient.GTransDestTotal = 0;
			expedient.OtrosGastosFacturas = 0;
            
			//Gastos por tipo de acreedor
			foreach (Expense item in expedient.Gastos)
			{
				switch (item.ECategoriaGasto)
				{
					case ECategoriaGasto.GeneralesExpediente:

						switch (item.ETipoAcreedor)
						{
                            case ETipoAcreedor.Naviera:
                                {
                                    if (expedient.OidFacturaNav == 0)
                                    {
                                        expedient.EstimarNaviera = false;
                                        expedient.OidFacturaNav = item.OidFactura;
                                    }
                                    expedient.GNavTotal += item.Total;
                                }
                                break;

                            case ETipoAcreedor.Despachante:
                                {
                                    if (expedient.OidFacturaDes == 0)
                                    {
                                        expedient.EstimarDespachante = false;
                                        expedient.OidFacturaDes = item.OidFactura;
                                    }
                                    expedient.GDespTotal += item.Total;
                                }
								break;

                            case ETipoAcreedor.TransportistaDestino:
                                {
                                    if (expedient.OidFacturaTde == 0)
                                    {
                                        expedient.EstimarTDestino = false;
                                        expedient.OidFacturaTde = item.OidFactura;
                                    }
                                    expedient.GTransDestTotal += item.Total;
                                }
								break;

                            case ETipoAcreedor.TransportistaOrigen:
                                {
                                    if (expedient.OidFacturaTor == 0)
                                    {
                                        expedient.EstimarTOrigen = false;
                                        expedient.OidFacturaTor = item.OidFactura;
                                    }
                                    expedient.GTransTotal += item.Total;
                                }
								break;
						}

						break;

					default:

						if (item.OidFactura == 0)
							expedient.OtrosGastos += item.Total;
						else
							expedient.OtrosGastosFacturas += item.Total;

						break;
				}
			}

			//Gasto Estimado Naviera
			if (expedient.GNavTotal == 0)
			{
				if (expedient.EstimarNaviera)
				{
					NavieraInfo nav = NavieraInfo.Get(expedient.OidNaviera, true);
					if (nav != null)
					{
						PrecioTrayectoInfo precio = nav.PrecioTrayectos.GetByPorts(expedient.PuertoOrigen, expedient.PuertoDestino);
						expedient.GNavTotal = (precio != null) ? precio.Precio : 0;
						expedient.GNavFac = Resources.Defaults.ESTIMADO;
					}
				}
                //else
                //    expediente.GNavTotal = gastos_naviera;
			}

			//Gasto Estimado Tranporte Origen
			if (expedient.GTransTotal == 0)
			{
				if (expedient.EstimarTOrigen)
				{
					TransporterInfo tr = TransporterInfo.Get(expedient.OidTransOrigen, ETipoAcreedor.TransportistaOrigen, true);
					if (tr != null)
					{
						PrecioOrigenInfo precio = tr.PrecioOrigenes.GetByProvAndPort(expedient.OidProveedor, expedient.PuertoOrigen);
						expedient.GTransTotal = (precio != null) ? precio.Precio : 0;
						expedient.GTransFac = Resources.Defaults.ESTIMADO;
					}
				}
                //else
                //    expediente.GTransTotal = gastos_torigen;
			}

			//Gasto Estimado Despachante
			if (expedient.GDespTotal == 0)
			{
				if (expedient.EstimarDespachante)
				{
					DespachanteInfo des = DespachanteInfo.Get(expedient.OidDespachante, false);
					if (des != null)
					{
						//PrecioTrayectoInfo precio = nav.PrecioTrayectos.GetByPorts(PuertoOrigen, PuertoDestino);
						expedient.GDespTotal = 50; //(precio != null) ? precio.Precio : 0;
						expedient.GDespFac = Resources.Defaults.ESTIMADO;
					}
				}
                //else
                //    expediente.GDespTotal = gastos_despachante;
			}

			//Gasto Estimado Transporte Destino
			if (expedient.GTransDestTotal == 0)
			{
				if (expedient.EstimarTDestino)
				{
					TransporterInfo tr = TransporterInfo.Get(expedient.OidTransDestino, ETipoAcreedor.TransportistaDestino, true);
					if (tr != null)
					{
						PrecioDestinoInfo precio = tr.PrecioDestinos.GetByPort(expedient.PuertoDestino);
						expedient.GTransDestTotal = (precio != null) ? precio.Precio : 0;
						expedient.GTransDestFac = Resources.Defaults.ESTIMADO;
					}
				}
                //else
                //    expediente.GTransDestTotal = gastos_tdestino;
			}

			expedient.GastosGenerales += expedient.GNavTotal + expedient.GDespTotal + expedient.GTransDestTotal + expedient.GTransTotal;
		}
	}
}