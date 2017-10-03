using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Face.Invoice;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Sales;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpedienteForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public virtual Expedient Entity { get { return null; } set { } }
        public virtual ExpedientInfo EntityInfo { get { return null; } }
        public virtual ExpedientInfo EntityInfoNoChilds { get { return EntityInfo; } }

        protected bool _no_size_changed = true;

		protected OutputInvoiceList _facturas_ingresos_list;
		protected OutputInvoiceLineList _conceptos_ingresos_list;
		protected InputInvoiceList _facturas_costes_list;
		protected InputInvoiceLineList _conceptos_costes_list;

        #endregion

        #region Factory Methods

        public ExpedienteForm() 
			: this(-1, moleQule.Store.Structs.ETipoExpediente.Todos) {}

		public ExpedienteForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo) 
			: this(oid, tipo, true, null) { }

        public ExpedienteForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo, bool is_modal, Form parent)
            : base(oid, new object[1] { tipo }, is_modal, parent)
        {
            InitializeComponent();
            CancelConfirmation = false;
        }

        #endregion

		#region Cache

		protected override void CleanCache()
		{
			Cache.Instance.Remove(typeof(InputInvoiceLineList));
			Cache.Instance.Remove(typeof(InputDeliveryLineList));
		}

		#endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			CurrencyManager cm;

			//Formato de Partidas_DGW
			cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			ProductoPartida.Tag = 1;

			cols.Add(ProductoPartida);

			ControlsMng.MaximizeColumns(Partidas_DGW, cols);

			//Formato de Stock_Grid
			cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			ObservacionesStock.Tag = 1;

			cols.Add(ObservacionesStock);

			ControlsMng.MaximizeColumns(Stock_DGW, cols);

			//Formato de FacturasCostes_DGW
			cols.Clear();
			AcreedorFCoste.Tag = 1;

			cols.Add(AcreedorFCoste);

			ControlsMng.MaximizeColumns(FacturasCostes_DGW, cols);

			//Formato de ConceptosCostes_DGW
			cols.Clear();
			ConceptoConceptoCoste.Tag = 1;

			cols.Add(ConceptoConceptoCoste);

			ControlsMng.MaximizeColumns(ConceptosCostes_DGW, cols);

			//Formato de FGastos_DGW
			cols.Clear();
			AcreedorFGasto.Tag = 1;

			cols.Add(AcreedorFGasto);

			ControlsMng.MaximizeColumns(ExpensesInvoices_DGW , cols);

			//Formato de GastosGenericos_DGW
			cm = (CurrencyManager)BindingContext[InvoicedExpenses_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			DescripcionGastoGenerico.Tag = 1;

			cols.Add(DescripcionGastoGenerico);

			ControlsMng.MaximizeColumns(InvoicedExpenses_DGW, cols);

			//Formato de OtrosGastos_DGW
			cm = (CurrencyManager)BindingContext[Expenses_DGW.DataSource];
			cm.SuspendBinding();

			cols.Clear();
			DescripcionOtroGasto.Tag = 1;

			cols.Add(DescripcionOtroGasto);

			ControlsMng.MaximizeColumns(Expenses_DGW, cols);

			//Formato de FacturasE_DGW
			cm = (CurrencyManager)BindingContext[FacturasE_DGW.DataSource];
			cm.SuspendBinding();

			cols = new List<DataGridViewColumn>();
			ClienteFacturaE.Tag = 1;

			cols.Add(ClienteFacturaE);

			ControlsMng.MaximizeColumns(FacturasE_DGW, cols);

			//Formato de ConceptosE_DGW
			cm = (CurrencyManager)BindingContext[ConceptosE_DGW.DataSource];
			cm.SuspendBinding();

			cols = new List<DataGridViewColumn>();
			DescripcionConceptoE.Tag = 1;

			cols.Add(DescripcionConceptoE);

			ControlsMng.MaximizeColumns(ConceptosE_DGW, cols);

			//Formato de Beneficios_DGW
			cm = (CurrencyManager)BindingContext[Benefits_DGW.DataSource];
			cm.SuspendBinding();

			cols = new List<DataGridViewColumn>();
            BeProduct.Tag = 1;

            cols.Add(BeProduct);
			ControlsMng.MaximizeColumns(Benefits_DGW, cols);
		}

		public override void FormatControls()
        {
            if (Stock_DGW == null) return;
			if (EntityInfoNoChilds == null) return;
		
			ShowAction(molAction.ShowDocuments);

            base.FormatControls();

            SetTabsOrder();

			AutoScroll = true;

			Stock_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			ExpensesInvoices_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Benefits_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			FacturasE_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			ConceptosE_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
			SetBeneficiosFormat();
			SetGastosFormat();
			SetFomentoFormat();
			SetPartidasFormat();
        }

        protected virtual void HideComponentes(TabPage page) {}

		protected virtual void SetBeneficiosFormat() { SetBeneficiosFormat(EntityInfoNoChilds.ETipoExpediente); }
		protected virtual void SetBeneficiosFormat(moleQule.Store.Structs.ETipoExpediente tipo)
		{ 
			BePCUd.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
            //BePCUdSupplier.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
            BePVUd.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

            BePCKg.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			BePCKgSupplier.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
 			BePVKg.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:

					break;

				case moleQule.Store.Structs.ETipoExpediente.Ganado:

					//Tabla Beneficios
                    BePCKg.Visible = false;
                    BePCKgSupplier.Visible = false;
                    BePVKg.Visible = false;

					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					//Tabla Beneficios
                    BePCKg.Visible = false;
                    BePCKgSupplier.Visible = false;
                    BePVKg.Visible = false;

					break;

				case moleQule.Store.Structs.ETipoExpediente.Todos:
					break;
			}
		}

		protected virtual void SetPartidasFormat() { SetPartidasFormat(EntityInfoNoChilds.ETipoExpediente); }
		protected virtual void SetPartidasFormat(moleQule.Store.Structs.ETipoExpediente tipo)
		{
			PCKgPartida.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			PCUdPartida.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			AyudaKgPartida.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			CosteKgPartida.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			GastoKgPartida.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
					PCKgPartida.DefaultCellStyle.Format = "N5";
					PCUdPartida.DefaultCellStyle.Format = "N2";
					AyudaKgPartida.DefaultCellStyle.Format = "N5";
					CosteKgPartida.DefaultCellStyle.Format = "N5";
					GastoKgPartida.DefaultCellStyle.Format = "N5";
                    
					break;

				case moleQule.Store.Structs.ETipoExpediente.Ganado:

					//Tabla Stock

					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					//Tabla Stock

					break;

				case moleQule.Store.Structs.ETipoExpediente.Todos:
					break;
			}
		}

		protected virtual void SetStockFormat() { SetStockFormat(EntityInfoNoChilds.ETipoExpediente); }
		protected virtual void SetStockFormat(moleQule.Store.Structs.ETipoExpediente tipo)
		{
			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:

					Stock_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

					break;

				case moleQule.Store.Structs.ETipoExpediente.Ganado:

					//Tabla Stock


					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					//Tabla Stock

					break;

				case moleQule.Store.Structs.ETipoExpediente.Todos:
					break;
			}
		}
		
		protected virtual void SetGastosFormat() {}

		protected virtual void SetFomentoFormat() {}

        protected virtual void SetTabsOrder() { }

		protected virtual void SetRowFormat(DataGridViewRow row, string columnName, object value)
		{
			/*switch (columnName)
			{
			}*/
		}

		#endregion

		#region Source

		protected virtual void LoadData() 
		{
			/*if (Pestanas.SelectedTab == Productos_TP)
			{
				LoadProductos();
				LoadGastos();
			}*/
			if (Pestanas_TC.SelectedTab == Stock_TP)
			{
				LoadStock();
			}
			if (Pestanas_TC.SelectedTab == Costes_TP)
			{
				LoadCostes();
			}
			else if (Pestanas_TC.SelectedTab == Incomes_TP)
			{
				LoadIncomes();
			}
			else if (Pestanas_TC.SelectedTab == Benefits_TP)
			{
				LoadCostes();
				LoadIncomes();
				LoadStock();
			}

			HideComponentes(Pestanas_TC.SelectedTab);
		}

		protected virtual void LoadIncomes() {}
		protected virtual void LoadIncomes(long oidExpediente)
		{
			if (_facturas_ingresos_list != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				_facturas_ingresos_list = OutputInvoiceList.GetByExpedienteList(oidExpediente, false);
				Datos_FIngresos.DataSource = _facturas_ingresos_list;
				PgMng.Grow();

				TotalPendienteFacturasE_NTB.Text = _facturas_ingresos_list.TotalPendiente().ToString("N2");
				TotalFacturasE_NTB.Text = _facturas_ingresos_list.Total().ToString("N2");
				TotalExpedienteFacturasE_NTB.Text = _facturas_ingresos_list.TotalExpediente().ToString("N2");

				BePurchases_NTB.Text = TotalExpedienteFacturasE_NTB.DecimalValue.ToString("C2");

				_conceptos_ingresos_list = OutputInvoiceLineList.GetByExpedienteList(oidExpediente, false);
				Datos_CIngresos.DataSource = _conceptos_ingresos_list;
				PgMng.Grow();

				CalculateBeneficios();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadCostes() { }
		protected virtual void LoadCostes(long oidExpediente)
		{
			if (_facturas_costes_list != null) return;

			try
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

				_facturas_costes_list = InputInvoiceList.GetCostesByExpedienteList(oidExpediente, false);
				Datos_FCostes.DataSource = _facturas_costes_list;
				PgMng.Grow();

				PendienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalPendiente().ToString("N2");
				TotalFacturasCostes_NTB.Text = _facturas_costes_list.Total().ToString("N2");
				ExpedienteFacturasCostes_NTB.Text = _facturas_costes_list.TotalExpediente().ToString("N2");
				ImpuestosFacturasCostes_NTB.Text = _facturas_costes_list.TotalImpuestos().ToString("N2");

				_conceptos_costes_list = InputInvoiceLineList.GetCostesByExpedienteList(oidExpediente, false, true);
				Datos_CCostes.DataSource = _conceptos_costes_list;
				TotalConceptosCostes_NTB.Text = _conceptos_costes_list.Total().ToString("N2");
				ImpuestosConceptosCostes_NTB.Text = _conceptos_costes_list.TotalImpuestos().ToString("N2");

				BeCostes_NTB.Text = TotalConceptosCostes_NTB.DecimalValue.ToString("C2");

				CalculateBeneficios();

				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected virtual void LoadStock() {}

		protected virtual void SelectConceptosGastos() {}

		protected virtual void UpdateBindings()
		{
			Stock_BS.ResetBindings(false);
			Stock_DGW.Refresh();

			Batchs_BS.ResetBindings(false);
			Benefits_DGW.Refresh();

			ExpensesInvoices_BS.ResetBindings(true);
			ExpensesInvoices_DGW.Refresh();
		}
		protected virtual void UpdateExpensesList() {}

		#endregion

		#region Business Methods

		protected virtual void AddFacturaGastos(InputInvoiceInfo fac) { }
		protected void AddFacturaGastos(Expedient expediente, InputInvoiceInfo fac)
		{
			this.Enabled = false;

			try
			{
				PgMng.Reset(5, 1, "Recalculando gastos y costes del expediente...", this);

				if (fac.Conceptos == null)
					fac.LoadChilds(typeof(InputInvoiceLine), false);

				foreach (InputInvoiceLineInfo item in fac.Conceptos)
				{
					if ((item.OidExpediente != expediente.Oid) && (item.OidExpediente != 0))
						item.IsSelected = false;
					else
					{
						item.IsSelected = true;
						item.OidExpediente = expediente.Oid;
						item.Expediente = expediente.Codigo;
					}
				}
				PgMng.Grow();

				expediente.LoadConceptosAlbaranes(false);
				PgMng.Grow();

				expediente.NuevoGasto(fac, expediente.Conceptos, true);
				PgMng.Grow();

				UpdateExpensesList();
				UpdateBindings();
				PgMng.Grow();
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
			}
			finally
			{
				PgMng.FillUp();
				this.Enabled = true;
			}
		}

		protected virtual void CalculateBeneficios()
		{
			CalculateTotales();

			//REAL
			BeBeneficioReal_NTB.Text = ((decimal)(BeIngresosReal_NTB.DecimalValue - BeTotalGastos_NTB.DecimalValue)).ToString("C2");
			BeBeneficioReal_NTB.ForeColor = (BeBeneficioReal_NTB.DecimalValue >= 0) ? BePurchases_NTB.ForeColor : Color.Red;

			decimal beneficio = (BeIngresosReal_NTB.DecimalValue != 0) ? (BeBeneficioReal_NTB.DecimalValue / BeIngresosReal_NTB.DecimalValue) : 0;

			BePBeneficioReal_NTB.Text = beneficio.ToString("P2");
			BePBeneficioReal_NTB.ForeColor = (BeBeneficioReal_NTB.DecimalValue >= 0) ? BePurchases_NTB.ForeColor : Color.Red;

			//ESTIMADO
			BeBeneficioEstimado_NTB.Text = ((decimal)(BeIngresosEstimado_NTB.DecimalValue - BeTotalGastos_NTB.DecimalValue)).ToString("C2");
			BeBeneficioEstimado_NTB.ForeColor = (BeBeneficioEstimado_NTB.DecimalValue >= 0) ? BePurchases_NTB.ForeColor : Color.Red;

			beneficio = (BeIngresosEstimado_NTB.DecimalValue != 0) ? (BeBeneficioEstimado_NTB.DecimalValue / BeIngresosEstimado_NTB.DecimalValue) : 0;

			BePBeneficioEstimado_NTB.Text = beneficio.ToString("P2");
			BePBeneficioEstimado_NTB.ForeColor = (BeBeneficioEstimado_NTB.DecimalValue >= 0) ? BePurchases_NTB.ForeColor : Color.Red;
		}
		protected virtual void CalculateTotales()
		{
			//REAL
			BeTotalGastos_NTB.Text = GetExpenses().ToString("C2");
			BeIngresosReal_NTB.Text = GetIncome().ToString("C2");
			
			//ESTIMADO (Deberia asignarlo cada formulario heredado para no tener que convertir el objeto en solo lectura que tarda mas)
			BeIngresosEstimado_NTB.Text = GetEstimatedIncome().ToString("C2");
		}
		
		public bool CheckFactura(InputInvoiceInfo fac)
		{
			List<InputInvoiceInfo> lista = ExpensesInvoices_BS.DataSource as List<InputInvoiceInfo>;

			InputInvoiceInfo factura = null;

			foreach (InputInvoiceInfo item in lista)
				if ((item.OidAcreedor == fac.OidAcreedor) && (item.ETipoAcreedor == fac.ETipoAcreedor))
					factura = item;

			return (factura != null);
		}

		protected virtual void EditFacturaGastos(InputInvoiceInfo fac) { }
		protected void EditFacturaGastos(Expedient expediente, InputInvoiceInfo fac)
		{
			this.Enabled = false;

			try
			{
				PgMng.Reset(5, 1, "Recalculando gastos y costes del expediente...", this);
				PgMng.Grow();

				expediente.LoadConceptosAlbaranes(false);
				PgMng.Grow();

                expediente.UpdateGasto(fac, expediente.Conceptos, true);
				PgMng.Grow();

				UpdateExpensesList();
				UpdateBindings();
				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
				this.Enabled = true;
			}
		}

		protected virtual void EditOtroGasto(DataGridViewRow row) { }
        protected void EditOtroGasto(Expedient expedient, Expense expense) 
		{
            expedient.UpdateGasto(expense, true);

			UpdateExpensesList();
		}

		protected virtual decimal GetEstimatedIncome()
		{
			return EntityInfo.IngresosEstimados();
		}
		protected virtual decimal GetExpenses()
		{
			return (decimal)(BeCostes_NTB.DecimalValue + BeExpenses_NTB.DecimalValue);
		}
		protected virtual decimal GetIncome()
		{
			return (decimal)(BePurchases_NTB.DecimalValue);
		}

		protected void NewOtroGasto(Expedient expediente)
		{
			expediente.NuevoGasto(true);

			UpdateBindings();
			UpdateExpensesList();
		}

        protected void RemoveOtroGasto(Expedient expedient, Expense expense) 
		{
            expedient.RemoveGasto(expense, true);

			UpdateBindings();
			UpdateExpensesList();
		}

		protected virtual void RemoveFacturaGastos(InputInvoiceInfo fac) {}
		protected void RemoveFacturaGastos(Expedient expediente, InputInvoiceInfo fac)
		{
			this.Enabled = false;

			try
			{
				PgMng.Reset(5, 1, "Recalculando gastos y costes del expediente...", this);

				if (fac.Conceptos == null)
					fac.LoadChilds(typeof(InputInvoiceLine), false);

				foreach (InputInvoiceLineInfo item in fac.Conceptos)
					item.IsSelected = true;
				PgMng.Grow();

				expediente.LoadConceptosAlbaranes(false);
				PgMng.Grow();

                expediente.RemoveGasto(fac, expediente.Conceptos, true);
				PgMng.Grow();

				UpdateExpensesList();
				Datos.ResetBindings(false);
				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
				this.Enabled = true;
			}
		}

		protected virtual void RemoveStock(Stock stock) { }
		protected bool RemoveStock(Expedient expediente, Stock stock)
		{
            if (((stock.OidAlbaran != 0) || (stock.Inicial))
                && stock.ETipoStock != ETipoStock.Consumo
                && stock.ETipoStock != ETipoStock.MovimientoSalida
                && stock.ETipoStock != ETipoStock.Merma)
			{
				PgMng.ShowInfoException(Resources.Messages.STOCK_FACTURADO);
				return false;
			}

			if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				expediente.RemoveStock(stock);
				return true;
			}

			return false;
		}

		protected void ReparteGasto(Expedient expediente)
		{
            if (!ControlsMng.IsCurrentItemValid(ExpensesInvoices_DGW)) return;

			InputInvoiceInfo fac = ControlsMng.GetCurrentItem(ExpensesInvoices_DGW) as InputInvoiceInfo;

			if (fac.Conceptos == null)
				fac.LoadChilds(typeof(InputInvoiceLine), false);

			FRecibidaSelectGastoForm form = new FRecibidaSelectGastoForm(fac, expediente, this);

			foreach (InputInvoiceLineInfo item in form.EntityInfo.Conceptos)
			{
                Expense expense = expediente.Gastos.GetItemByConceptoFactura(item);
                item.IsSelected = (expense != null);
			}

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				EditFacturaGastos(form.EntityInfo);

				UpdateExpensesList();
				Datos.ResetBindings(false);
			}
		}

		protected void UpdateAyudaPartida(Expedient expediente)
		{
			expediente.UpdateAyudas();
			UpdateBindings();
		}
		
        #endregion

        #region Actions

        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Expedient), EntityInfoNoChilds as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Expedient), EntityInfoNoChilds as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

		protected virtual void SelectClientAction() { }
		protected virtual void SelectProductAction(ProductInfo product) {}
        protected virtual void SelectProductNameAction() { }
		protected virtual void SelectSupplierAction() { }
		protected virtual void SelectStockAction(ProductInfo product) {}
		
		protected virtual void AddBatchAction() { }
		protected virtual void EditBatchAction() { }
		protected virtual void DeleteBatchAction() { } 
	
		protected virtual void AddFGastoAction()
		{
			InputInvoiceList list = InputInvoiceList.GetListNoAsignadas(false);
			InputInvoiceSelectForm form = new InputInvoiceSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				InputInvoiceInfo fac = form.Selected as InputInvoiceInfo;
				AddFacturaGastos(fac);

				UpdateExpensesList();
				Datos.ResetBindings(false);
			}
		}
		protected virtual void RemoveFGastoAction()
		{
            if (!ControlsMng.IsCurrentItemValid(ExpensesInvoices_DGW)) return;

            InputInvoiceInfo fac = ControlsMng.GetCurrentItem(ExpensesInvoices_DGW) as InputInvoiceInfo;

			RemoveFacturaGastos(fac);

			UpdateExpensesList();
			Datos.ResetBindings(false);
		}
		protected virtual void ReparteFGastoAction() { }
		protected virtual void ViewFGastoAction() 
		{
            if (!ControlsMng.IsCurrentItemValid(ExpensesInvoices_DGW)) return;

            InputInvoiceInfo fac = ControlsMng.GetCurrentItem(ExpensesInvoices_DGW) as InputInvoiceInfo;

			InputInvoiceViewForm form = new InputInvoiceViewForm(fac, this);
			form.ShowDialog(this);
		}
		protected virtual void DefaultFGastoAction() { ReparteFGastoAction(); }

		protected virtual void NewOtroGastoAction() { }
		protected virtual void RemoveOtroGastoAction() { }

		protected virtual void AddStockAction() { }
		protected virtual void EditStockAction() { }
		protected virtual void DeleteStockAction() { }
		protected virtual void PrintStockAction() { }
        protected virtual void PrintMermaAction() { }

		protected virtual void ViewFacturaEAction()
		{
            if (!ControlsMng.IsCurrentItemValid(FacturasE_DGW)) return;

            OutputInvoiceInfo invoice = ControlsMng.GetCurrentItem(FacturasE_DGW) as OutputInvoiceInfo;

            InvoiceViewForm form = new InvoiceViewForm(invoice.Oid, this);

            form.ShowDialog(this);
		}
		protected virtual void ViewConceptoFacturaEAction()
		{
            if (!ControlsMng.IsCurrentItemValid(ConceptosE_DGW)) return;

            OutputInvoiceLineInfo line = ControlsMng.GetCurrentItem(ConceptosE_DGW) as OutputInvoiceLineInfo;

            InvoiceViewForm form = new InvoiceViewForm(line.OidFactura, this);

            form.ShowDialog(this);
		}
		protected virtual void ViewFacturaCosteAction()
		{
            if (!ControlsMng.IsCurrentItemValid(FacturasCostes_DGW)) return;

            InputInvoiceInfo invoice = ControlsMng.GetCurrentItem(FacturasCostes_DGW) as InputInvoiceInfo;

            InputInvoiceViewForm form = new InputInvoiceViewForm(invoice.Oid, invoice.ETipoAcreedor, this);

			form.ShowDialog(this);
		}
		protected virtual void ViewConceptoCosteEAction()
		{
            if (!ControlsMng.IsCurrentItemValid(ConceptosCostes_DGW)) return;

            InputInvoiceLineInfo line = ControlsMng.GetCurrentItem(ConceptosCostes_DGW) as InputInvoiceLineInfo;

            InputInvoiceViewForm form = new InputInvoiceViewForm(line.OidFactura, ETipoAcreedor.Proveedor, this);

			form.ShowDialog(this);
		}

		protected virtual void ViewInformeVentasAction()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);
			PgMng.Grow();

			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { Expediente = EntityInfoNoChilds };

			VentasList list = VentasList.GetListByExpediente(conditions, true);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			Library.Invoice.CommonReportMng reportMng = new Library.Invoice.CommonReportMng(AppContext.ActiveSchema, Resources.Labels.VENTAS_EXPEDIENTE_REPORT_TITLE, "Expediente = " + EntityInfoNoChilds.Codigo);
			InformeVentasExpedientesRpt report = reportMng.GetVentasExpedienteReport(list, true);

			PgMng.FillUp();
			ShowReport(report);	
		}

		protected virtual void UpdateAyudaPartidaAction() { }

		#endregion

        #region Buttons

        private void Cliente_BT_Click(object sender, EventArgs e) { SelectClientAction(); }

        private void Producto_BT_Click(object sender, EventArgs e) { SelectProductNameAction(); }

		private void Proveedor_BT_Click(object sender, EventArgs e)	{ SelectSupplierAction(); }

		private void AgregarProducto_BT_Click(object sender, EventArgs e) {	AddBatchAction(); }
		private void EditProducto_TI_Click(object sender, EventArgs e) { EditBatchAction(); }
		private void DeleteProducto_TI_Click(object sender, EventArgs e) { DeleteBatchAction(); }

		private void AddGasto_TI_Click(object sender, EventArgs e) { AddFGastoAction(); }
		private void ViewGasto_TI_Click(object sender, EventArgs e) { ViewFGastoAction(); }
		private void ShareGasto_TI_Click(object sender, EventArgs e) { ReparteFGastoAction(); }
		private void DeleteGasto_TI_Click(object sender, EventArgs e) { RemoveFGastoAction(); }

		private void NewOtroGasto_TI_Click(object sender, EventArgs e) { NewOtroGastoAction(); }
		private void DeleteOtroGasto_TI_Click(object sender, EventArgs e) { RemoveOtroGastoAction(); }

		private void AddStock_TI_Click(object sender, EventArgs e) { AddStockAction(); }
		private void EditStock_TI_Click(object sender, EventArgs e) { EditStockAction(); }
		private void DeleteStock_TI_Click(object sender, EventArgs e) { DeleteStockAction(); }
        private void PrintStock_TI_Click(object sender, EventArgs e) { PrintStockAction(); }
        private void PrintMerma_TI_Click(object sender, EventArgs e) { PrintMermaAction(); }

		private void ViewFacturaE_TI_Click(object sender, EventArgs e)	{ ViewFacturaEAction(); }

		private void ViewConceptoIngreso_TI_Click(object sender, EventArgs e) { ViewConceptoCosteEAction(); }
			
		private void Beneficios_BT_Click(object sender, EventArgs e) { ViewInformeVentasAction(); }

        #endregion

        #region Events

		private void Productos_DGW_SelectionChanged(object sender, EventArgs e)
		{
			if (Datos_Productos.Current == null) return;
			SelectProductAction(Datos_Productos.Current as ProductInfo);
		}

		private void Partidas_DGW_DoubleClick(object sender, EventArgs e) { EditBatchAction(); }

		private void Partidas_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Partidas_DGW.Columns[e.ColumnIndex].Name == AyudaPartida.Name)
			{
				UpdateAyudaPartidaAction();
			}
		}

		private void Pestanas_SelectedIndexChanged(object sender, EventArgs e) { LoadData(); }

		private void FGastos_DGW_SelectionChanged(object sender, EventArgs e) { SelectConceptosGastos(); }

		private void FGastos_DGW_DoubleClick(object sender, EventArgs e) { DefaultFGastoAction(); }
	
		private void FacturasE_DGW_DoubleClick(object sender, EventArgs e) { ViewFacturaEAction();	}
		
		private void ConceptosE_DGW_DoubleClick(object sender, EventArgs e) { ViewConceptoFacturaEAction();	}

		private void Costes_DGW_DoubleClick(object sender, EventArgs e) { ViewFacturaCosteAction(); }

		private void ConceptosCostes_DGW_DoubleClick(object sender, EventArgs e) { ViewConceptoCosteEAction(); }

		private void OtrosGastos_DGW_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			EditOtroGasto(Expenses_DGW.Rows[e.RowIndex]);
		}

        #endregion
	}
}