using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
    public class ExpedientInfo : ReadOnlyBaseEx<ExpedientInfo>, IEntity, IAgenteHipatia
    {
		#region IEntity

		public long EntityType { get { return (long)moleQule.Common.Structs.ETipoEntidad.Expediente; } }

		#endregion

        #region IAgenteHipatia

        public string NombreHipatia { get { return Codigo; } }
        public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(Expedient); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

        #region Attributes

        public ExpedientBase _base = new ExpedientBase();

        protected DateTime _g_desp_fecha;

        protected ExpenseList _gastos = null;
        protected MaquinariaList _maquinarias = null;
        protected BatchList _partidas = null;
        protected StockList _stocks = null;
		protected LineaFomentoList _expedientes_fomento = null;
		protected ExpedienteREAList _expedientes_rea = null;
        
		private InputInvoiceList _facturas = null;
        private RelationList _relations = null;

        #endregion

        #region Propiedades

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long Tipo { get { return _base.Record.TipoExpediente; } set { _base.Record.TipoExpediente = value; } }
        public long OidNaviera { get { return _base.Record.OidNaviera; } }
        public long OidTransOrigen { get { return _base.Record.OidTransOrigen; } }
        public long OidTransDestino { get { return _base.Record.OidTransDestino; } }
        public long OidDespachante { get { return _base.Record.OidDespachante; } }
        public long OidFacturaPro { get { return _base.Record.OidFacturaPro; } }
        public long OidFacturaNav { get { return _base.Record.OidFacturaNav; } }
        public long OidFacturaDes { get { return _base.Record.OidFacturaDes; } }
        public long OidFacturaTor { get { return _base.Record.OidFacturaTor; } }
        public long OidFacturaTde { get { return _base.Record.OidFacturaTde; } }
        public long Serial { get { return _base.Record.Serial; } }
        public string Codigo { get { return _base.Record.Codigo; } }
        public DateTime Fecha { get { return _base.Record.Fecha; } }
        public string PuertoOrigen { get { return _base.Record.PuertoOrigen; } }
        public string PuertoDestino { get { return _base.Record.PuertoDestino; } }
        public string Buque { get { return _base.Record.Buque; } }
        public int Ano { get { return _base.Record.Ano; } }
        public DateTime FechaPedido { get { return _base.Record.FechaPedido; } }
        public DateTime FechaFacProveedor { get { return _base.Record.FechaFacProveedor; } }
        public DateTime FechaEmbarque { get { return _base.Record.FechaEmbarque; } }
        public DateTime FechaLlegadaMuelle { get { return _base.Record.FechaLlegadaMuelle; } }
        public DateTime FechaDespachoDestino { get { return _base.Record.FechaDespachoDestino; } }
        public DateTime FechaSalidaMuelle { get { return _base.Record.FechaSalidaMuelle; } }
        public DateTime FechaRegresoMuelle { get { return _base.Record.FechaRegresoMuelle; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }
        public Decimal FleteNeto { get { return _base.Record.FleteNeto; } }
        public Decimal Baf { get { return _base.Record.Baf; } }
        public bool Teus20 { get { return _base.Record.Teus20; } }
        public bool Teus40 { get { return _base.Record.Teus40; } }
        public Decimal T3Origen { get { return _base.Record.T3Origen; } }
        public Decimal T3Destino { get { return _base.Record.T3Destino; } }
        public Decimal ThcOrigen { get { return _base.Record.ThcOrigen; } }
        public Decimal ThcDestino { get { return _base.Record.ThcDestino; } }
        public Decimal Isps { get { return _base.Record.Isps; } }
        public Decimal TotalImpuestos { get { return _base.Record.TotalImpuestos; } }
        public bool EstimarDespachante { get { return _base.Record.EstimarDespachante; } }
        public bool EstimarNaviera { get { return _base.Record.EstimarNaviera; } }
        public bool EstimarTOrigen { get { return _base.Record.EstimarTorigen; } }
        public bool EstimarTDestino { get { return _base.Record.EstimarTdestino; } }
        public string GTransFac { get { return _base.Record.GTransFac; } }
        public Decimal GTransTotal { get { return _base.Record.GTransTotal; } }
        public string GNavFac { get { return _base.Record.GNavFac; } }
        public Decimal GNavTotal { get { return _base.Record.GNavTotal; } }
        public string GDespFac { get { return _base.Record.GDespFac; } }
        public Decimal GDespTotal { get { return _base.Record.GDespTotal; } }
        public DateTime GDespFecha { get { return _g_desp_fecha; } }
        public Decimal GDespIgic { get { return _base.Record.GDespIgic; } }
        public Decimal GDespIgicServ { get { return _base.Record.GDespIgicServ; } }
        public string GTransDestFac { get { return _base.Record.GTransDestFac; } }
        public Decimal GTransDestTotal { get { return _base.Record.GTransDestTotal; } }
        public Decimal GTransDestIgic { get { return _base.Record.GTransDestIgic; } }
        public string Contenedor { get { return _base.Record.Contenedor; } }
        public long OidProveedor { get { return _base.Record.OidProveedor; } }
        public string GProvFac { get { return _base.Record.GProvFac; } }
        public Decimal GProvTotal { get { return _base.Record.GProvTotal; } }
        public bool Ayuda { get { return _base.Record.Ayuda; } }
        public string TipoMercancia { get { return _base.Record.TipoMercancia; } }
        public string NombreCliente { get { return _base.Record.NombreCliente; } }
        public string CodigoArticulo { get { return _base.Record.CodigoArticulo; } }

        public ExpenseList Gastos { get { return _gastos; } }
        public MaquinariaList Maquinarias { get { return _maquinarias; } }
        public BatchList Partidas { get { return _partidas; } }
        public StockList Stocks { get { return _stocks; } }
		public LineaFomentoList ExpedientesFomento { get { return _expedientes_fomento; } }
		public ExpedienteREAList ExpedientesREA { get { return _expedientes_rea; } }

        //NO ENLAZADOS
        public ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_base.Record.TipoExpediente; } }
		public string ETipoExpedienteLabel { get { return EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }
        public string LFechaEmbarque { get { return (FechaEmbarque == DateTime.MinValue) ? null : FechaEmbarque.ToShortDateString(); } }
        public string LFechaLlegadaMuelle { get { return (FechaLlegadaMuelle == DateTime.MinValue) ? null : FechaLlegadaMuelle.ToShortDateString(); } }
        public string LFechaDespachoDestino { get { return (FechaDespachoDestino == DateTime.MinValue) ? null : FechaDespachoDestino.ToShortDateString(); } }
        public string LFechaSalidaMuelle { get { return (FechaSalidaMuelle == DateTime.MinValue) ? null : FechaSalidaMuelle.ToShortDateString(); } }
        public string LFechaRegresoMuelle { get { return (FechaRegresoMuelle == DateTime.MinValue) ? null : FechaRegresoMuelle.ToShortDateString(); } }

		public string Proveedor { get { return _base.Proveedor; } set { _base.Proveedor = value; } }
        public string Naviera { get { return _base.Naviera; } set { _base.Naviera = value; } }
        public string Despachante { get { return _base.Despachante; } set { _base.Despachante = value; } }
        public string NombreTransDest { get { return _base.NombreTransDest; } set { _base.NombreTransDest = value; } }
        public string NombreTransOrig { get { return _base.NombreTransOrig; } set { _base.NombreTransOrig = value; } }

        public ETipoExpediente ETipo { get { return (ETipoExpediente)_base.Record.TipoExpediente; } }
        public string Description { get { return TipoMercancia; } }
        public decimal Kilos { get { return _base.KilosTotal; } }
        public decimal Bultos { get { return _base.BultosTotal; } }
        public decimal StockKilos
        {
            get
            {
                if (Stocks == null) return _base.StockKilos;

                decimal value = 0;
                foreach (StockInfo item in Stocks)
                    value += item.Kilos;
                return value;
            }
        }
        public decimal StockBultos
        {
            get
            {
                if (Stocks == null) return _base.StockBultos;

                decimal value = 0;
                foreach (StockInfo item in Stocks)
                    value += item.Bultos;
                return value;
            }
        }
        //public decimal AyudaEstimada { get { return (_ayuda) ? _ayuda_estimada : 0; } }
        public decimal AyudaEstimada { get { return Decimal.Round(_base.AyudaEstimada, 2); } }
        public decimal AyudaPendiente { get { return Decimal.Round(_base.AyudaPendiente, 2); } }
        //public decimal AyudaPendiente { get { return (_ayuda) ? ((_ayuda_cobrada > 0) ? 0 : _ayuda_pendiente) : 0; } }
        public decimal AyudaDesestimada { get { return (_base.Record.Ayuda) ? ((_base.AyudaCobrada > 0) ? (_base.AyudaEstimada - _base.AyudaCobrada) : 0) : 0; } }
        public decimal AyudaCobrada { get { return Decimal.Round(_base.AyudaCobrada, 2); } }
		//public decimal AyudaCobrada { get { return (_ayuda) ? _ayuda_cobrada : 0; } }
        public decimal GastosGenerales { get { return _base.GastosGenerales; } }
        public decimal GastoPorKilo { get { return (_base.KilosTotal > 0) ? GastoTotal / _base.KilosTotal : 0; } }
        public decimal GastoAbsoluto { get { return GastoTotal + _base.Record.GProvTotal; } }
		public decimal OtrosGastos { get { return _base.OtrosGastos; } }
		public decimal OtrosGastosFacturas { get { return _base.OtrosGastosFacturas; } }
        public decimal GastosFacturas { get { return GastosGenerales + OtrosGastosFacturas; } }
        public decimal GastoTotal { get { return _base.GastosGenerales + _base.OtrosGastosFacturas + _base.OtrosGastos; } }
        public virtual decimal CosteTotal { get { return GastoAbsoluto - ((_base.AyudaCobrada > 0) ? _base.AyudaCobrada : _base.AyudaEstimada); } }
        public virtual decimal CosteProveedorCalculado { get { return _base.CosteTotalCalculado; }  }
        public virtual decimal CosteTotalCalculado { get { return _base.CosteTotalCalculado + GastoTotal - ((_base.AyudaCobrada > 0) ? _base.AyudaCobrada : _base.AyudaEstimada); } }
        public virtual decimal CosteMedioPorKilo { get { return (_base.KilosTotal > 0) ? (CosteTotalCalculado / _base.KilosTotal) : 0; } }
        //public decimal CosteTotal { get { return GastoTotal - _ayuda_estimada; } }
        //public decimal CosteTotalCalculado { get { return _total_coste_calculado + GastoTotal - _ayuda_estimada; } }

        public decimal NMaquinas { get { return (Maquinarias == null) ? _base.KilosTotal : Maquinarias.Count; } }

        public virtual decimal KilosTotal { get { return _base.KilosTotal; } set { _base.KilosTotal = value; } }
        public virtual decimal AyudaExpediente { get { return Decimal.Round((Ayuda ? ((AyudaCobrada != 0) ? AyudaCobrada : AyudaEstimada) : 0), 2); } }

		public InputInvoiceList Facturas { get { return _facturas; } }
        public RelationList Relations { get { return _relations; } }

        #endregion

        #region Business Methods

        public void CopyFrom(Expedient source) { _base.CopyValues(source); }

        public List<long> GetExpedientsOidList()
        {
            List<long> list = Relations.ToChildsOidList();
            list.Add(Oid);
            return list;
        }

        public virtual string GetStockWarning()
        {
            string warning = string.Empty;

            if (Partidas == null) return warning;

            foreach (BatchInfo item in Partidas)
            {
                if (item.StockKilos < 0 || item.StockBultos < 0)
                    warning += item.Codigo + " - " + item.Producto + Environment.NewLine;
            }

            if (warning != string.Empty)
                warning = Resources.Messages.PARTIDAS_STOCK_NEGATIVO + warning;

            return warning;
        }

		public virtual decimal IngresosEstimados()
		{
			decimal total = 0;
			foreach (BatchInfo pa in Partidas)
			{
				total += pa.PrecioVentaKilo * pa.KilosIniciales;
			}

			return total;
		}

        public virtual void UpdateTotalesProductos(bool estimated = true)
        {
            if (Partidas == null) return;
            
            _base.AyudaEstimada = 0;            
            _base.CosteTotalCalculado = 0;
            _base.KilosTotal = Partidas.Sum(x => x.KilosIniciales);
            _base.BultosTotal = Partidas.Sum(x => x.BultosIniciales);
            _base.AyudaEstimada = Partidas.Where(x => x.Ayuda == true).Sum(x => x.AyudaKilo * x.KilosIniciales);
            _base.CosteTotalCalculado += Partidas.Sum(x => x.PrecioCompraKilo * x.KilosIniciales);

            UpdateGastos(estimated);
        }

		public virtual void UpdateGastos(bool estimated = true)
		{
			if (Gastos == null) return;

			_base.GastosGenerales = 0;
			_base.OtrosGastos = 0;
            _base.Record.GProvTotal = 0;
            _base.Record.GNavTotal = 0;
            _base.Record.GDespTotal = 0;
            _base.Record.GTransDestTotal = 0;
            _base.Record.GTransTotal = 0;

            _base.Record.GProvTotal = Gastos.Where(x => x.ECategoriaGasto == ECategoriaGasto.Stock).Sum(x => x.Total);
           
            _base.Record.GNavTotal = Gastos.Where(x => 
                                        x.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente &&
                                        x.ETipoAcreedor == ETipoAcreedor.Naviera
                                     ).Sum(x => x.Total);

            _base.Record.GDespTotal = Gastos.Where(x =>
                                        x.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente &&
                                        x.ETipoAcreedor == ETipoAcreedor.Despachante
                                     ).Sum(x => x.Total);

            _base.Record.GTransDestTotal = Gastos.Where(x =>
                                        x.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente &&
                                        x.ETipoAcreedor == ETipoAcreedor.TransportistaDestino
                                     ).Sum(x => x.Total);

            _base.Record.GTransTotal = Gastos.Where(x =>
                                        x.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente &&
                                        x.ETipoAcreedor == ETipoAcreedor.TransportistaOrigen
                                     ).Sum(x => x.Total);

            _base.OtrosGastos += Gastos.Where(x =>
                                        ! new List<ECategoriaGasto> { ECategoriaGasto.Stock, ECategoriaGasto.GeneralesExpediente }.Contains(x.ECategoriaGasto) &&
                                        x.OidFactura == 0
                                     ).Sum(x => x.Total);

            _base.OtrosGastosFacturas += Gastos.Where(x =>
                                        !new List<ECategoriaGasto> { ECategoriaGasto.Stock, ECategoriaGasto.GeneralesExpediente }.Contains(x.ECategoriaGasto) &&
                                        x.OidFactura != 0
                                     ).Sum(x => x.Total);

			//Gastos por tipo de acreedor
			/*foreach (GastoInfo item in Gastos)
			{
				switch (item.ECategoriaGasto)
				{
					case ECategoriaGasto.Stock:
                        _base.Record.GProvTotal += item.Total;
						break;

					case ECategoriaGasto.GeneralesExpediente:

						switch (item.ETipoAcreedor)
						{
							case ETipoAcreedor.Naviera:
                                _base.Record.GNavTotal += item.Total;
								break;

							case ETipoAcreedor.Despachante:
                                _base.Record.GDespTotal += item.Total;
								break;

							case ETipoAcreedor.TransportistaDestino:
                                _base.Record.GTransTotal += item.Total;
								break;

							case ETipoAcreedor.TransportistaOrigen:
                                _base.Record.GTransDestTotal += item.Total;
								break;
						}

						break;

					default:
						_base.OtrosGastos += item.Total;
						break;
				}
			}*/

			//Gasto Estimado Naviera
			if (GNavTotal == 0 && estimated)
			{
				NavieraInfo nav = NavieraInfo.Get(OidNaviera, true);
				if (nav != null)
				{
					PrecioTrayectoInfo precio = nav.PrecioTrayectos.GetByPorts(PuertoOrigen, PuertoDestino);
                    _base.Record.GNavTotal = (precio != null) ? precio.Precio : 0;
                    _base.Record.GNavFac = Resources.Defaults.ESTIMADO;
				}
			}

			//Gasto Estimado Tranporte Origen
            if (GTransTotal == 0 && estimated)
			{
				TransporterInfo tr = TransporterInfo.Get(OidTransOrigen, ETipoAcreedor.TransportistaOrigen, true);
				if (tr != null)
				{
					PrecioOrigenInfo precio = tr.PrecioOrigenes.GetByProvAndPort(OidProveedor, PuertoOrigen);
                    _base.Record.GTransTotal = (precio != null) ? precio.Precio : 0;
                    _base.Record.GTransFac = Resources.Defaults.ESTIMADO;
				}
			}

			//Gasto Estimado Despachante
            if (GDespTotal == 0 && estimated)
			{
				DespachanteInfo des = DespachanteInfo.Get(OidDespachante, false);
				if (des != null)
				{
					//PrecioTrayectoInfo precio = nav.PrecioTrayectos.GetByPorts(PuertoOrigen, PuertoDestino);
                    _base.Record.GDespTotal = 50; //(precio != null) ? precio.Precio : 0;
                    _base.Record.GDespFac = Resources.Defaults.ESTIMADO;
				}
			}

			//Gasto Estimado Transporte Destino
            if (GTransDestTotal == 0 && estimated)
			{
				TransporterInfo tr = TransporterInfo.Get(OidTransDestino, ETipoAcreedor.TransportistaDestino, true);
				if (tr != null)
				{
					PrecioDestinoInfo precio = tr.PrecioDestinos.GetByPort(PuertoDestino);
                    _base.Record.GTransDestTotal = (precio != null) ? precio.Precio : 0;
                    _base.Record.GTransDestFac = Resources.Defaults.ESTIMADO;
				}
			}

			_base.GastosGenerales += GNavTotal + GDespTotal + GTransDestTotal + GTransTotal;
		}

        public virtual void UpdateGastosPartidas()
        {
            InputDeliveryLineList conceptos = InputDeliveryLineList.GetByExpedienteStockList(Oid, false, true);

            if (Ayuda)
                if (_expedientes_rea == null)
                    LoadChilds(typeof(REAExpedient), true, false);

            KilosTotal = 0;

            foreach (BatchInfo item in Partidas)
            {
                if (item.OidExpediente != Oid) continue;
                KilosTotal += item.KilosIniciales;
            }

            decimal ayudas = 0;
            decimal kilos = 0;
            decimal ayuda_kilo = 0;

            foreach (BatchInfo item in Partidas)
            {
                if (item.OidExpediente != Oid) continue;

                if (Ayuda)
                {
                    ayudas = ExpedientesREA.GetTotalAyudas(item.CodigoAduanero);
                    kilos = Partidas.GetTotalKilos(item.CodigoAduanero);
                    ayuda_kilo = ayudas / kilos;
                }
                else
                    ayuda_kilo = 0;

                item.CalculaCostes(GastoPorKilo, ayuda_kilo);
            }
        }

        public virtual void UpdateStocks(bool throwStockException)
        {
            if (Partidas == null) return;

            foreach (BatchInfo item in Partidas)
                UpdateStocks(item, throwStockException);
        }

        public virtual void UpdateStocks(BatchInfo p_e, bool throwException)
        {
            List<StockInfo> stocks = _stocks.GetSubList(new FCriteria<long>("OidPartida", p_e.Oid));

            p_e.StockKilos = 0;
            p_e.StockBultos = 0;

            foreach (StockInfo item in stocks)
            {
                p_e.StockKilos += item.Kilos;
                p_e.StockBultos += item.Bultos;

                item.KilosActuales = p_e.StockKilos;
                item.BultosActuales = p_e.StockBultos;

                //Ajuste a 0 por posibles errores de redondeo
                if (item.KilosActuales == 0) item.BultosActuales = 0;
                if (item.BultosActuales == 0) item.KilosActuales = 0;
            }

            //Ajuste a 0 por posibles errores de redondeo
            if (p_e.StockKilos == 0) p_e.StockBultos = 0;
            if (p_e.StockBultos == 0) p_e.StockKilos = 0;

            if (throwException && (p_e.StockKilos < 0 || p_e.StockBultos < 0))
                throw new iQException(Resources.Messages.AVISO_STOCK_NEGATIVO);
        }

        #endregion
        
        #region Factory Methods

        protected ExpedientInfo() { /* require use of factory methods */ }
        private ExpedientInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
       }
        internal ExpedientInfo(Expedient item, bool childs)
        {
            _base.CopyValues(item);

            if (childs)
            {
                if (item.Gastos != null) _gastos = ExpenseList.GetChildList(item.Gastos);
                if (item.Maquinarias != null) _maquinarias = MaquinariaList.GetChildList(item.Maquinarias);
                if (item.Partidas != null) _partidas = BatchList.GetChildList(item.Partidas);
                if (item.Stocks != null) _stocks = StockList.GetChildList(item.Stocks);
				if (item.ExpedientesFomento != null) _expedientes_fomento = LineaFomentoList.GetChildList(item.ExpedientesFomento);
				if (item.ExpedientesREA != null) _expedientes_rea = ExpedienteREAList.GetChildList(item.ExpedientesREA);
                if (item.Relations != null) _relations = RelationList.GetChildList(item.Relations);
            }
        }

        public static ExpedientInfo Get(long oid, bool childs = false)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Expedient.SELECT(oid, false);

            ExpedientInfo obj = DataPortal.Fetch<ExpedientInfo>(criteria);
            obj.CloseSession();

            return obj;
        }
        public static ExpedientInfo Get(long oid, bool childs, bool cache)
        {
			if (oid == 0) return null;

			ExpedientInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(ExpedienteList)))
			{
				ExpedienteList items = ExpedienteList.NewList();

				item = ExpedientInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(ExpedienteList), items);
			}
			else
			{
				ExpedienteList items = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = ExpedientInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(ExpedienteList), items);
				}
			}

            return item;
        }

        /// <summary>
        /// Devuelve un ExpedienteInfo tras consultar la base de datos
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static ExpedientInfo GetAlmacen(bool childs)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Expedient.SELECT(1, false);

            criteria.Childs = childs;
            ExpedientInfo obj = DataPortal.Fetch<ExpedientInfo>(criteria);
            Expedient.CloseSession(criteria.SessionCode);
            return obj;
        }
        public static ExpedientInfo GetAlmacen(bool childs, bool cache)
        {
            ExpedientInfo item;

            if (!Cache.Instance.Contains(typeof(ExpedientInfo)))
            {
                item = ExpedientInfo.GetAlmacen(childs);
                Cache.Instance.Save(typeof(ExpedientInfo), item);
            }
            else
                item = Cache.Instance.Get(typeof(ExpedientInfo)) as ExpedientInfo;

            return item;
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ExpedientInfo Get(int sessionCode, IDataReader reader, bool childs)
        {
            return new ExpedientInfo(sessionCode, reader, childs);
        }

        public ExpedientePrint GetPrintObject() { return ExpedientePrint.New(this); }

        public virtual void LoadChilds(Type type, bool childs, bool throwStockException, bool estimated = true)
		{
			if (type.Equals(typeof(Expense)))
			{
				_gastos = ExpenseList.GetChildList(this, childs);
				_facturas = InputInvoiceList.GetListByExpediente(Oid, true);

				UpdateGastos(estimated);
				UpdateTotalesProductos(estimated);
			}
			if (type.Equals(typeof(Batch)))
			{
				_partidas = BatchList.GetChildList(this, childs);

				switch ((ETipoExpediente)Tipo)
				{
					case ETipoExpediente.Maquinaria:
						_maquinarias = MaquinariaList.GetChildList(this, childs);
						break;
				}

				UpdateGastos(estimated);
				UpdateTotalesProductos(estimated);
			}
			else if (type.Equals(typeof(InputInvoice)))
			{
				_facturas = InputInvoiceList.GetListByExpediente(Oid, true);
			}
			else if (type.Equals(typeof(Stock)))
			{
				_stocks = StockList.GetChildList(this, childs, throwStockException);
                UpdateStocks(throwStockException);
			}
			else if (type.Equals(typeof(LineaFomento)))
			{
				_expedientes_fomento = LineaFomentoList.GetChildList(this, childs);
			}
			else if (type.Equals(typeof(REAExpedient)))
			{
				_expedientes_rea = ExpedienteREAList.GetChildList(this, childs);
			}
		}

        public virtual void LoadExpenses(bool estimated = true)
        {
            _gastos = ExpenseList.GetChildList(this, false);
            _facturas = InputInvoiceList.GetListByExpediente(Oid, true);
            _partidas = BatchList.GetChildList(this, false);

            UpdateTotalesProductos(estimated);
            UpdateGastosPartidas();
        }

		public static ExpedientInfo New(long oid = 0) { return new ExpedientInfo() { Oid = oid }; }

        #endregion

        #region Root Data Access

        // called to retrieve data from db
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

                        switch ((ETipoExpediente)Tipo)
                        {
                            case ETipoExpediente.Maquinaria:
                                {
                                    Maquinaria.DoLOCK(Session());
                                    query = MaquinariaList.SELECT(this);
                                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                    _maquinarias = MaquinariaList.GetChildList(reader);
                                } break;
                        }

                        query = BatchList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_partidas = BatchList.GetChildList(SessionCode, reader, true);

                        query = ExpenseList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _gastos = ExpenseList.GetChildList(reader);
						
						_facturas = InputInvoiceList.GetListByExpediente(Oid, true);

                        query = StockList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _stocks = StockList.GetChildList(reader);

						query = LineaFomentoList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_fomento = LineaFomentoList.GetChildList(SessionCode, reader);

						query = ExpedienteREAList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_expedientes_rea = ExpedienteREAList.GetChildList(SessionCode, reader);

						UpdateTotalesProductos();
						UpdateGastos();
						UpdateStocks(false);
                    }
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region Child Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
                    string query = string.Empty;
                    IDataReader reader;

                    switch ((ETipoExpediente)Tipo)
                    {
                        case ETipoExpediente.Maquinaria:
                            {
                                query = MaquinariaList.SELECT(this);
                                reader = nHManager.Instance.SQLNativeSelect(query, Session());
                                _maquinarias = MaquinariaList.GetChildList(reader);
                            } break;
                    }

                    query = BatchList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_partidas = BatchList.GetChildList(SessionCode, reader, false);

                    query = ExpenseList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _gastos = ExpenseList.GetChildList(reader);

                    query = StockList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = StockList.GetChildList(reader);

					query = LineaFomentoList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_expedientes_fomento = LineaFomentoList.GetChildList(SessionCode, reader);

					query = ExpedienteREAList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_expedientes_rea = ExpedienteREAList.GetChildList(SessionCode, reader);

					UpdateTotalesProductos();
					UpdateGastos();
					UpdateStocks(false);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

        #endregion
    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class ExpedientSerialInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected ExpedientSerialInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static ExpedientSerialInfo Get(ETipoExpediente tipo, int year)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(tipo, year);

            ExpedientSerialInfo obj = DataPortal.Fetch<ExpedientSerialInfo>(criteria);
            Expedient.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(ETipoExpediente tipo, int year)
        {
            return Get(tipo, year).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(ETipoExpediente tipo, int year)
        {
            string e = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string query = string.Empty;

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"";

            query += " FROM " + e + " AS E";
            query += " WHERE \"TIPO_EXPEDIENTE\" = " + (long)tipo;
            query += " AND \"ANO\" = " + year;

            return query;
        }

        #endregion
    }
}





