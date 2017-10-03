using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Controls;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Face.Invoice;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ContenedorUIForm : ContenedorForm
    {
        #region Attributes & Properties
        
        protected override int BarSteps { get { return base.BarSteps + 7; } }

        protected Expedient _entity;

        protected NavieraInfo _shipping_comnpany;
        protected DespachanteInfo _custom_agency;
        protected TransporterInfo _origin_transporter;
        protected TransporterInfo _destination_transporter;
        protected ProveedorInfo _provider;
        protected LivestockBookLines _livestock_lines;

        protected decimal _precio_naviera = 0;
        protected decimal _precio_puerto = 0;

        public override Expedient Entity { get { return _entity; } set { _entity = value; } }
        public override ExpedientInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }
        public override ExpedientInfo EntityInfoNoChilds { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        public ContenedorUIForm() 
			: this(-1, ETipoExpediente.Todos) {}

		public ContenedorUIForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo)
            : this(oid, tipo, null) {}

        public ContenedorUIForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo, Form parent) 
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
            if (_entity != null) _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

		public override void DisposeForm()
		{
			if (_livestock_lines != null) _livestock_lines.CloseSession();

			base.DisposeForm();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Expedient temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
				if (_livestock_lines != null) _livestock_lines.Save();

				_entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex.Message);
				PgMng.FillUp();
                return false;
            }
            finally
            {
				_entity.BeginEdit();
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Cache

        protected override void CleanCache()
        {
            //Se borran aquí porque se cargan dentro y no se actualizan por lo que luego los pillan los albaranes con datos erroneos
            Cache.Instance.Remove(typeof(Expedients));
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            if (Stock_DGW == null) return;

			base.FormatControls();

			ProductosPartidas_SC.Panel1Collapsed = true;
			REALeft_SC.Panel2.Enabled = (_entity != null) ? _entity.Ayuda : false;

            int index = StockTitulo_LB.Text.IndexOf("(") - 1;

			StockTitulo_LB.Text = (index > 0 ? StockTitulo_LB.Text.Substring(0, index) : StockTitulo_LB.Text) + " (" + _entity.Partidas.Count + ")";            
        }

		protected void MarkAsTeus40(DataGridViewRow row)
		{
			LineaFomento item = row.DataBoundItem as LineaFomento;
			item.Teus = Library.Store.Resources.Labels.TEUS40;
		}
		protected void MarkAsTeus20(DataGridViewRow row)
		{
			LineaFomento item = row.DataBoundItem as LineaFomento;
			item.Teus = Library.Store.Resources.Labels.TEUS20;
		}

		protected override void SetExpedienteREAFormat()
		{
			foreach (DataGridViewRow row in ExpedienteREA_DGW.Rows)
			{
				if (row.IsNewRow) return;

                REAExpedient item = (REAExpedient)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

				SetRowFormat(row, "FechaDespachoREA", item.Fecha);
				SetRowFormat(row, "FechaCobroRea",  item.FechaCobro);
			}
		}

		protected override void SetFomentoFormat()
		{
			foreach (DataGridViewRow row in Fomento_DGW.Rows)
			{
				if (row.IsNewRow) return;

				LineaFomento item = (LineaFomento)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

				SetRowFormat(row, "FechaSolicitudLFomento", item.FechaSolicitud);
			}
		}

		protected override void SetGastosFormat()
		{
			GastoTOringen_NTB.ReadOnly = (_entity.OidFacturaTor != 0);
			GastoNaviera_NTB.ReadOnly = (_entity.OidFacturaNav != 0);
			GastoDespachante_NTB.ReadOnly = (_entity.OidFacturaDes != 0);
			GastoTDestino_NTB.ReadOnly = (_entity.OidFacturaTde != 0);

			GastoTOringen_NTB.ForeColor = (_entity.OidFacturaTor != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoNaviera_NTB.ForeColor = (_entity.OidFacturaNav != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoDespachante_NTB.ForeColor = (_entity.OidFacturaDes != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;
			GastoTDestino_NTB.ForeColor = (_entity.OidFacturaTde != 0) ? GastosGenerales_NTB.ForeColor : Color.Red;

			foreach (DataGridViewRow row in Expenses_DGW.Rows)
			{
                Expense item = row.DataBoundItem as Expense;
				if (item == null) continue;

				row.ReadOnly = (item.OidFactura != 0);
				row.Cells["FechaFacturaOtroGasto"].Style.ForeColor = (item.OidFactura == 0) ? Color.Transparent : row.Cells["TotalOtroGasto"].Style.ForeColor;
				row.Cells["TipoAcreedorOtroGasto"].Style.ForeColor = (item.OidFactura == 0) ? Color.Transparent : row.Cells["TotalOtroGasto"].Style.ForeColor;
			}
		}

		#endregion

		#region Source

		protected override void HideComponentes(TabPage page)
		{
			if (General_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Partidas_DGW.Rows)
                    if ((row.DataBoundItem as Batch).IsKitComponent)
						row.Visible = false;
			}
			else if (Stock_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Stock_DGW.Rows)
					if ((row.DataBoundItem as Stock).IsKitComponent)
						row.Visible = false;
			}
			else if (Benefits_TP.Equals(page))
			{
				CurrencyManager cm = (CurrencyManager)BindingContext[Benefits_DGW.DataSource];
				cm.SuspendBinding();
				foreach (DataGridViewRow row in Benefits_DGW.Rows)
                    if ((row.DataBoundItem as Batch).IsKitComponent)
						row.Visible = false;
			}
		}

		protected override void LoadAyudas()
		{
			if (_entity.ExpedientesREA.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
                    _entity.LoadChilds(typeof(REAExpedient), true, true);
					Datos_REA.DataSource = _entity.ExpedientesREA;
					Datos_REA.ResetBindings(true);
					PgMng.Grow(string.Empty, "REA");
				}
				finally
				{
					PgMng.FillUp();
				}
			}
			else
			{
				Datos_REA.DataSource = _entity.ExpedientesREA;
				Datos_REA.ResetBindings(false);
			}

			AyudaTotal_NTB.Text = _entity.AyudaExpediente.ToString("C2");
			CalculateBeneficios();
			SetExpedienteREAFormat();
		}

		protected override void LoadLibroGanadero() 
		{
			try
			{
				if (_livestock_lines != null) return;

				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

                _livestock_lines = LivestockBookLines.GetByExpedienteList(_entity.Oid, false);
				LivestockBook_BS.DataSource = _livestock_lines;
				PgMng.Grow(string.Empty);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

        protected override void LoadCostes() { LoadCostes(_entity.Oid); }

		protected override void LoadFomento()
		{
			if (_entity.ExpedientesFomento.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(LineaFomento), true, true);
					PgMng.Grow();
					Datos_Fomento.DataSource = _entity.ExpedientesFomento;
					Datos_Fomento.ResetBindings(true);
					SetFomentoFormat();
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

		protected override void LoadIncomes() { LoadIncomes(_entity.Oid); }

		protected override void LoadStock()
		{
			if (_entity.Stocks.Count != 0) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				PgMng.Grow();
                _entity.LoadChilds(typeof(Stock), true, false);

				PgMng.Grow();
				SelectStockAction(Datos_Productos.Current as ProductInfo);
			}
			finally
			{
				PgMng.FillUp();
			}

			ActualizaGastosStock();
		}

		protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;
			PgMng.Grow(string.Empty, "Datos");

            Stock_BS.DataSource = _entity.Stocks;
			PgMng.Grow(string.Empty, "Stocks"); 
            
            switch (_entity.ETipoExpediente)
            {
                case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					Datos_Maquinaria.RaiseListChangedEvents = false;    
                    Datos_Maquinaria.DataSource = _entity.Maquinarias;
					Datos_Maquinaria.RaiseListChangedEvents = false;    
                    break;
            }

			Batchs_BS.RaiseListChangedEvents = false;
			Batchs_BS.DataSource = _entity.Partidas;
			Batchs_BS.RaiseListChangedEvents = true;
			PgMng.Grow(string.Empty, "Partidas"); 

            Datos_Gastos.DataSource = _entity.Gastos;
			UpdateExpensesList();
			PgMng.Grow(string.Empty, "Gastos");

            ExpensesInvoices_BS.DataSource = _entity.Facturas;
            PgMng.Grow(string.Empty, "Facturas de Acreedores");

			Ayuda_CkB.Checked = _entity.Ayuda;
			EstimarDespachante_CkB.Checked = _entity.EstimarDespachante;
			EstimarNaviera_CkB.Checked = _entity.EstimarNaviera;
			EstimarTOrigen_CkB.Checked = _entity.EstimarTOrigen;
			EstimarTDestino_CkB.Checked = _entity.EstimarTDestino;
			
			if (_entity.Teus20)
				Teus20_RB.Checked = _entity.Teus20;
			else
				Teus40_RB.Checked = _entity.Teus40;

			Datos_Fomento.DataSource = _entity.ExpedientesFomento;
        }

        public override void RefreshSecondaryData()
        {
            PuertoList puertos = PuertoList.GetList(false);
            Datos_PuertoOrigen.DataSource = Datos_PuertoDestino.DataSource = puertos;
			PgMng.Grow(string.Empty, "Puertos");

            if (_entity.ETipoExpediente == moleQule.Store.Structs.ETipoExpediente.Ganado)
            {
                Datos_TipoGanado.DataSource = TipoGanadoList.GetList(false);
				PgMng.Grow(string.Empty, "TipoGanado");
            }

			base.RefreshSecondaryData();
        }

		protected override void SelectConceptosGastos() 
		{
			InvoicedExpenses_BS.RaiseListChangedEvents = false;

			if (ExpensesInvoices_BS.Current != null)
			{
				InputInvoiceInfo factura = ExpensesInvoices_BS.Current as InputInvoiceInfo;
				InvoicedExpenses_BS.DataSource = Expenses.GetListAgrupada(_entity.Gastos.GetSubList(factura));
			}
			else
				InvoicedExpenses_BS.DataSource = null;

			InvoicedExpenses_BS.RaiseListChangedEvents = true;

			InvoicedExpenses_BS.ResetBindings(true);
			InvoicedExpenses_DGW.Refresh();
		}

		protected override void SetDependentControlSource(string controlName)
		{
			switch (controlName)
			{
				case "teus20RadioButton":
					{
						Teus20_RB.Checked = _entity.Teus20;
					} break;
				case "teus40RadioButton":
					{
						Teus40_RB.Checked = _entity.Teus40;
					} break;
			}
		}

        #endregion

		#region Business Methods

		protected void ActualizaGastosStock()
		{
			BeMermas_NTB.Text = _entity.Stocks.TotalGastosMermas(_entity.Partidas).ToString("C2");
			BeSalidas_NTB.Text = _entity.Stocks.TotalGastosSalidas(_entity.Partidas).ToString("C2");

			AyudaMermas_NTB.Text = (-_entity.Stocks.TotalAyudaMermas(_entity.Partidas)).ToString("C2");
			AyudaTotal_NTB.Text = _entity.AyudaExpediente.ToString("C2");

			CalculateBeneficios();
		}
		protected override void UpdateExpensesList()
		{
			ExpensesInvoices_BS.RaiseListChangedEvents = false;
			Expenses_BS.RaiseListChangedEvents = false;

			ExpensesInvoices_BS.DataSource = _entity.Facturas;
			Expenses_BS.DataSource = Expenses.GetListAgrupada(_entity.Gastos.GetSubListOtrosGastos());		
			
			ExpensesInvoices_BS.RaiseListChangedEvents = true;
			Expenses_BS.RaiseListChangedEvents = true;
			
			ExpensesInvoices_BS.ResetBindings(true);
			ExpensesInvoices_DGW.Refresh();

			Expenses_BS.ResetBindings(true);
			Expenses_DGW.Refresh();

            SetGastosFormat();
		}

		protected void ChangeEstadoExpedienteREA(DataGridViewRow row, moleQule.Base.EEstado estado)
		{
            REAExpedient item = row.DataBoundItem as REAExpedient;
			item.EEstado = estado;
		}
		protected void ChangeEstadoFomento(DataGridViewRow row, moleQule.Base.EEstado estado)
		{
			LineaFomento item = row.DataBoundItem as LineaFomento;
			item.EEstado = estado;
		}

        public void CheckPrecioNaviera()
        {
            if ((_precio_puerto != _entity.GNavTotal) || (_precio_naviera != _entity.GNavTotal))
            {
				PgMng.ShowInfoException("El precio introducido no coincide con los precios almacenados:" + Environment.NewLine +
                                "   Precio del Puerto: " + _precio_puerto.ToString("C2") + Environment.NewLine +
                                "   Precio de la Naviera: " + _precio_naviera.ToString("C2") + Environment.NewLine +
                                "   Precio del Expediente: " + _entity.GNavTotal.ToString("C2"));
                return;
            }
        }

		protected override void AddFacturaGastos(InputInvoiceInfo fac) { AddFacturaGastos(_entity, fac); }
	
		protected override void EditFacturaGastos(InputInvoiceInfo fac) { EditFacturaGastos(_entity, fac); }
		protected override void EditOtroGasto(DataGridViewRow row) 
		{
			if (row == null) return;
			if (row.DataBoundItem == null) return;

			//Copiamos los datos desde el objeto de la row que es una lista agrupada por lo que 
			//al modificarla en el Grid no se modifica el gasto de la entidad si no lo hacemos aquí
            Expense item = row.DataBoundItem as Expense;
			
			EditOtroGasto(_entity, item);
		}

		protected override decimal GetEstimatedIncome()
		{
			return _entity.IngresosEstimados() + AyudaTotal_NTB.DecimalValue + AyudaMermas_NTB.DecimalValue;
		}

		protected override void RemoveFacturaGastos(InputInvoiceInfo fac) { RemoveFacturaGastos(_entity, fac); }
		protected override void RemoveStock(Stock stock) { RemoveStock(_entity, stock); }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

			if (_action_result == DialogResult.OK)
			{
				//Actualizamos la cache
				ExpedienteList expedientes = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;
				if (expedientes != null) expedientes.Change(_entity.Oid, _entity.GetInfo(false), true);
			}
        }

		protected override void CancelAction() 
		{
			_entity.CancelEdit();
			base.CancelAction();
		}

		protected override void SelectClientAction()
		{
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _entity.NombreCliente = ((ClienteInfo)form.Selected).Nombre;
            }
		}

		protected override void SelectSupplierAction()
		{
			ProveedorList list = ProveedorList.GetList(moleQule.Base.EEstado.Active, ETipoAcreedor.Proveedor, false);
			SupplierSelectForm form = new SupplierSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_provider = (ProveedorInfo)form.Selected;
				_entity.OidProveedor = _provider.Oid;
				_entity.Proveedor = _provider.Nombre;

				_entity.SetCode(ETipoAcreedor.Proveedor);
			}
		}

		protected override void SelectProductAction(ProductInfo producto)
		{
			if (producto == null) return;

			FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
			Batchs_BS.DataSource = _entity.Partidas.GetSubList(criteria);
			Batchs_BS.ResetBindings(true);

			SelectStockAction(producto);
		}

        protected override void SelectProductNameAction()
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ProductInfo producto = (ProductInfo)form.Selected;
                _entity.TipoMercancia = producto.Nombre;
            }
        }

		protected override void SelectStockAction(ProductInfo producto)
		{
			if (_entity.ETipoExpediente == moleQule.Store.Structs.ETipoExpediente.Almacen)
			{
				if (producto == null) return;

				FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
				Stock_BS.DataSource = _entity.Stocks.GetSubList(criteria);
			}
			else
				Stock_BS.DataSource = _entity.Stocks;

			Stock_BS.ResetBindings(true);
		}

		protected override void ReparteFGastoAction() { ReparteGasto(_entity); }

		protected override void NewOtroGastoAction() { NewOtroGasto(_entity); }
		protected override void RemoveOtroGastoAction()
		{
			if (!ControlsMng.IsCurrentItemValid(Expenses_DGW)) return;

            Expense item = ControlsMng.GetCurrentItem(Expenses_DGW) as Expense;
			
            RemoveOtroGasto(_entity, item);
		}

		protected override void NewREAAction()
		{
			BatchList lista = BatchList.GetList(_entity.Partidas);

			BatchSelectForm form = new BatchSelectForm(this, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BatchInfo partida = form.Selected as BatchInfo;

				try
				{
					_entity.ExpedientesREA.NewItem(_entity, partida);
				}
				catch (iQException ex)
				{
					PgMng.ShowInfoException(ex);
				}
			}
		}
		protected override void DeleteREAAction()
		{
			if (ExpedienteREA_DGW.CurrentRow == null) return;
			if (ExpedienteREA_DGW.CurrentRow.DataBoundItem == null) return;

			if (PgMng.ShowDeleteConfirmation() == DialogResult.No) return;

            REAExpedient item = ExpedienteREA_DGW.CurrentRow.DataBoundItem as REAExpedient;

			try
			{
				_entity.ExpedientesREA.RemoveItem(item, _entity);
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}

		protected override void AddStockAction()
		{
			if (_entity.Partidas.Count == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_PRODUCTS_ASSOC);
				return;
			}

			AddStockInputForm form = new AddStockInputForm(_entity);
			form.ShowDialog(this);

			UpdateBindings();

			ActualizaGastosStock();
		}
		protected override void EditStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock s = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if (s.ETipoStock != ETipoStock.Consumo && ((s.OidAlbaran != 0) ||
				((s.OidAlbaran == 0) && (s.Observaciones == Library.Store.Resources.Defaults.STOCK_INICIAL))))
			{
				PgMng.ShowInfoException(Resources.Messages.STOCK_FACTURADO);
				return;
			}

			EditStockActionForm form = new EditStockActionForm(s, _entity);
			form.ShowDialog(this);

			UpdateBindings();

			ActualizaGastosStock();
		}
		protected override void DeleteStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock st = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

            if (st.OidEnlace != 0) return;

			if (((st.OidAlbaran != 0) || (st.Inicial)) 
                && st.ETipoStock != ETipoStock.Consumo
                && st.ETipoStock != ETipoStock.MovimientoSalida
                && st.ETipoStock != ETipoStock.Merma) 
                return;

			RemoveStock(st);

			UpdateBindings();

			ActualizaGastosStock();
		}
		protected override void PrintStockAction()
		{
			Library.Store.ReportFilter filter = new Library.Store.ReportFilter();
			ReportFormat format = new ReportFormat();

			format.Vista = EReportVista.Detallado;

			filter.FechaIni = DateTime.MinValue;
			filter.FechaFin = DateTime.MaxValue;
			filter.SoloMermas = false;
			filter.SoloStock = false;

			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema);
			MovimientosStockListPorExpedienteRpt rpt = reportMng.GetMovimientosStockListAgrupado(_entity.GetInfo(true), null, null, filter, format, false);

			ShowReport(rpt);
		}
        protected override void PrintMermaAction()
        {
            if (Stock_DGW.CurrentRow == null) return;
            if (Stock_DGW.CurrentRow.Index < 0) return;
            if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

            Stock s = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

            if (s.ETipoStock != ETipoStock.Merma) return;
            ReportFormat format = new ReportFormat();

            format.Vista = EReportVista.Detallado;

            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema);
            MermaStockRpt rpt = reportMng.GetMermaDetailReport(s.GetInfo());

            ShowReport(rpt);
        }

		protected override void EditCabezaAction()
		{
            if (!ControlsMng.IsCurrentItemValid(LibroGanadero_DGW)) return;

            LivestockBookLine linea = ControlsMng.GetCurrentItem(LibroGanadero_DGW) as LivestockBookLine;

			LivestockBookLineEditForm form = new LivestockBookLineEditForm(linea, this);
			form.ShowDialog(this);
		}

		protected void SetGastoPrincipalAction(InputInvoiceInfo item)
		{
			_entity.SetGasto(item);
            _entity.UpdateGastosPartidas(true);
		}

		protected override void AdjuntarDocumentosFomentoAction()
		{
			if (Fomento_DGW.CurrentRow == null) return;

			LineaFomento item = (LineaFomento)Fomento_DGW.CurrentRow.DataBoundItem;

			try
			{
				AgenteEditForm form = new AgenteEditForm(typeof(LineaFomento), item as IAgenteHipatia, this);
				form.ShowDialog(this);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(typeof(LineaFomento), item as IAgenteHipatia, this);
					form.ShowDialog(this);
				}
			}
		}
		protected override void AttachFacturaNavieraActon()
		{
			if (Fomento_DGW.CurrentRow == null) return;
			if (Fomento_DGW.CurrentRow.DataBoundItem == null) return;

			LineaFomento linea = Fomento_DGW.CurrentRow.DataBoundItem as LineaFomento;

			InputInvoiceList list = _entity.GetFacturasNaviera();
			InputInvoiceSelectForm form = new InputInvoiceSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				InputInvoiceInfo fac = form.Selected as InputInvoiceInfo;

				_entity.ExpedientesFomento.SetValues(_entity, fac, linea);
			}
		}
		protected override void DeleteLineaFomentoAction()
		{
			if (Fomento_DGW.CurrentRow == null) return;
			if (Fomento_DGW.CurrentRow.DataBoundItem == null) return;

			LineaFomento linea = Fomento_DGW.CurrentRow.DataBoundItem as LineaFomento;

			_entity.ExpedientesFomento.RemoveItem(linea);
		}
		protected override void NewLineaFomentoAction()
		{
			BatchList lista = BatchList.GetList(_entity.Partidas);

			BatchSelectForm form = new BatchSelectForm(this, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BatchInfo partida = form.Selected as BatchInfo;

				partida = lista.GetPartidaAgrupada(partida);

				try
				{
					_entity.ExpedientesFomento.NewItem(_entity, partida);
				}
				catch (iQException ex)
				{
					PgMng.ShowInfoException(ex);
				}
			}
		}
		protected override void NullLineaFomentoAction()
		{
			if (Fomento_DGW.CurrentRow == null) return;
			if (Fomento_DGW.CurrentRow.DataBoundItem == null) return;

			LineaFomento linea = Fomento_DGW.CurrentRow.DataBoundItem as LineaFomento;

			linea.EEstado = moleQule.Base.EEstado.Anulado;

			SetFomentoFormat();
		}
		protected override void SelectEstadoFomentoAction()
		{
			if (Fomento_DGW.CurrentRow == null) return;

			LineaFomento item = (LineaFomento)Fomento_DGW.CurrentRow.DataBoundItem;

			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Abierto, moleQule.Base.EEstado.EnSolicitud, moleQule.Base.EEstado.Solicitado, moleQule.Base.EEstado.Aceptado, moleQule.Base.EEstado.Desestimado, moleQule.Base.EEstado.Charged };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				ChangeEstadoFomento(Fomento_DGW.CurrentRow, (moleQule.Base.EEstado)estado.Oid);

				Fomento_DGW.CurrentCell.Value = estado.Texto;

				SetFomentoFormat();
			}
		}
		protected override void SelectTeusFomentoAction(DataGridViewRow row)
		{
			if (row == null) return;
			if (row.DataBoundItem == null) return;

			if (_entity.Teus20) MarkAsTeus40(row);
			else MarkAsTeus20(row);
		}

		protected override void SelectEstadoExpedienteREAAction()
		{
			if (ExpedienteREA_DGW.CurrentRow == null) return;

            REAExpedient item = (REAExpedient)ExpedienteREA_DGW.CurrentRow.DataBoundItem;

			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Abierto, moleQule.Base.EEstado.Solicitado, moleQule.Base.EEstado.Aceptado, moleQule.Base.EEstado.Desestimado, moleQule.Base.EEstado.Charged };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				ChangeEstadoExpedienteREA(ExpedienteREA_DGW.CurrentRow, (moleQule.Base.EEstado)estado.Oid);

				ExpedienteREA_DGW.CurrentCell.Value = estado.Texto;

				SetExpedienteREAFormat();
			}
		}

		protected override void UpdateAyudaPartidaAction()
		{
			if (Partidas_DGW.CurrentRow == null) return;
			if (Partidas_DGW.CurrentRow.DataBoundItem == null) return;

			Batch item = (Batch)Partidas_DGW.CurrentRow.DataBoundItem;

			if (item == null) return;

			item.Ayuda = !item.Ayuda;

			UpdateAyudaPartida(_entity);
		}

		protected override void ViewInformeVentasAction()
		{
			SaveAction();
			base.ViewInformeVentasAction();
		}

        #endregion

        #region Buttons

		private void PuertoOrigen_BT_Click(object sender, EventArgs e)
        {
            SelectInputForm form = new SelectInputForm(PuertoList.GetList(false));
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                PuertoInfo p = (PuertoInfo)form.Selected;
                _entity.PuertoOrigen = p.Valor;

                _precio_puerto = p.Precio;
            }
        }

        private void Naviera_BT_Click(object sender, EventArgs e)
        {
            if (_entity.PuertoOrigen == string.Empty)
            {
                PgMng.ShowInfoException("Debe seleccionar un puerto de origen.");
                return;
            }

            if (_entity.PuertoDestino == string.Empty)
            {
                PgMng.ShowInfoException("Debe seleccionar un puerto de destino.");
                return;
            }

            ShippingCompanySelectForm form = new ShippingCompanySelectForm(this, moleQule.Base.EEstado.Active);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _shipping_comnpany = (NavieraInfo)form.Selected;
                _entity.OidNaviera = _shipping_comnpany.Oid;
                _entity.Naviera = _shipping_comnpany.Nombre;

                _entity.SetCode(ETipoAcreedor.Naviera);

                _shipping_comnpany = NavieraInfo.Get(_shipping_comnpany.Oid, true);
                _precio_naviera = _shipping_comnpany.GetPrecioPuertoOrigen(_entity.PuertoOrigen, _entity.PuertoDestino);

                if (_precio_puerto != _precio_naviera)
                {
                    PgMng.ShowInfoException("El precio introducido no coincide con el precio estándar del puerto:" + Environment.NewLine +
                                    "   Precio del Puerto: " + _precio_puerto.ToString("C2") + Environment.NewLine +
                                    "   Precio de la Naviera: " + _precio_naviera.ToString("C2"));
                    return;
                }
            }
        }

        private void Despachante_BT_Click(object sender, EventArgs e)
        {
            if (_entity.PuertoDestino == string.Empty)
            {
                PgMng.ShowInfoException("Debe seleccionar un puerto de destino.");
                return;
            }

            PuertoList pl = PuertoList.GetList(true);
            PuertoInfo pi = pl.GetItemByProperty("Valor", _entity.PuertoDestino);

            DespachanteList despachantes = PuertoDespachanteList.GetDespachanteList(pi.Oid);

            CustomAgentSelectForm form = new CustomAgentSelectForm(this, despachantes);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _custom_agency = form.Selected as DespachanteInfo;
                _entity.OidDespachante = _custom_agency.Oid;
                _entity.Despachante = _custom_agency.Nombre;

                Datos.ResetBindings(false);
            }
        }

        private void PuertoDestino_BT_Click(object sender, EventArgs e)
        {
            SelectInputForm form = new SelectInputForm(PuertoList.GetList(false));
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                PuertoInfo p = (PuertoInfo)form.Selected;
                _entity.PuertoDestino = p.Valor;
            }
        }

        private void TransOrig_BT_Click(object sender, EventArgs e)
        {
			TransporterSelectForm form = new TransporterSelectForm(this, TransporterList.GetList(ETipoTransportista.Origen, false));            
            
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _origin_transporter = form.Selected as TransporterInfo;
                _entity.OidTransOrigen = _origin_transporter.Oid;
                _entity.NombreTransOrig = _origin_transporter.Nombre;

                Datos.ResetBindings(false);
            }
        }

        private void TransDestino_BT_Click(object sender, EventArgs e)
        {
			TransporterSelectForm form = new TransporterSelectForm(this, TransporterList.GetList(ETipoTransportista.Destino, false));            
            
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _destination_transporter = form.Selected as TransporterInfo;
                _entity.OidTransDestino = _destination_transporter.Oid;
                _entity.NombreTransDest = _destination_transporter.Nombre;

                Datos.ResetBindings(false);
            }
        }

        #endregion

        #region Events

        private void ContenedorUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
			if (_action_result != DialogResult.OK)
			{
				if (CancelConfirmation)
				{
					if (DialogResult.No == ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.CANCEL_CONFIRM))
					{
						e.Cancel = true;
						return;
					}
				}
			}
        }

        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /*switch (e.PropertyName)
            {
                case "CobradoRea":
                    _entity.UpdateAyudas();
                    ControlsMng.UpdateBinding(Datos_Partidas);
                    ControlsMng.UpdateBinding(Datos_FGastos);
                    ControlsMng.UpdateBinding(Datos);
                    break;
			}*/
		}

		private void Tipo_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			_entity.GetNewCode();
		}

		private void EstimarTOrigen_CkB_CheckedChanged(object sender, EventArgs e)
		{
			_entity.EstimarTOrigen = EstimarTOrigen_CkB.Checked;
            _entity.UpdateGastosPartidas(true);
		}

		private void EstimarNaviera_CkB_CheckedChanged(object sender, EventArgs e)
		{
			_entity.EstimarNaviera = EstimarNaviera_CkB.Checked;
            _entity.UpdateGastosPartidas(true);
		}

		private void EstimarTDestino_CkB_CheckedChanged(object sender, EventArgs e)
		{
			_entity.EstimarTDestino = EstimarTDestino_CkB.Checked;
            _entity.UpdateGastosPartidas(true);
		}

		private void EstimarDespachante_CkB_CheckedChanged(object sender, EventArgs e)
		{
			_entity.EstimarDespachante = EstimarDespachante_CkB.Checked;
            _entity.UpdateGastosPartidas(true);
		}

        private void GastoDespachante_NTB_Validated(object sender, EventArgs e) { _entity.UpdateGastosPartidas(true); }

        private void GastoTOringen_NTB_Validated(object sender, EventArgs e) { _entity.UpdateGastosPartidas(true); }

        private void GastoNaviera_NTB_Validated(object sender, EventArgs e) { _entity.UpdateGastosPartidas(true); }

        private void GastoTDestino_NTB_Validated(object sender, EventArgs e) { _entity.UpdateGastosPartidas(true); }

		private void Ayuda_CkB_CheckedChanged(object sender, EventArgs e)
		{
			REALeft_SC.Panel2.Enabled = Ayuda_CkB.Checked;
			_entity.Ayuda = Ayuda_CkB.Checked;
            try
            {
                _entity.UpdateAyudas(_entity.Ayuda);
            }
            catch { }
            UpdateBindings();
		}

		private void Teus40_RB_CheckedChanged(object sender, EventArgs e)
        {
            _entity.Teus40 = Teus40_RB.Checked;
			_entity.Teus20 = !_entity.Teus40;
			_entity.ExpedientesFomento.UpdateItems(_entity);
			Datos_Fomento.ResetBindings(false);
        }

        private void Teus20_RB_CheckedChanged(object sender, EventArgs e)
        {
            _entity.Teus20 = Teus20_RB.Checked;
			_entity.Teus40 = !Teus20_RB.Checked;
			_entity.ExpedientesFomento.UpdateItems(_entity);
			Datos_Fomento.ResetBindings(false);
        }    

        #endregion
    }
}