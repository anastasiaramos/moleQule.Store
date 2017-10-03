using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using Csla;
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
    public partial class ProjectUIForm : ProjectForm
    {
        #region Attributes & Properties
        
        protected override int BarSteps { get { return base.BarSteps + 9; } }

        /// <summary>
        /// Se trata de la Expedient actual y que se va a editar.
        /// </summary>
        protected Expedient _entity;

        protected NavieraInfo _naviera;
        protected DespachanteInfo _despachante;
        protected TransporterInfo _trans_origen;
        protected TransporterInfo _trans_destino;
        protected ProveedorInfo _proveedor;

		protected decimal _precio_naviera = 0;
        protected decimal _precio_puerto = 0;

        public override Expedient Entity { get { return _entity; } set { _entity = value; } }
        public override ExpedientInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }
        public override ExpedientInfo EntityInfoNoChilds { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        public ProjectUIForm() 
			: this(-1) {}

		public ProjectUIForm(long oid)
            : this(oid, null) {}

        public ProjectUIForm(long oid, Form parent) 
            : base(oid, true, parent)
        {
            InitializeComponent();
            //if (_entity != null) _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
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
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex.Message);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout
	
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
		}

		protected override void LoadCostes() { LoadCostes(_entity.Oid); }

		protected override void LoadStock()
		{
			if (_entity.Stocks.Count == 0)
			{
				PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
				try
				{
					PgMng.Grow();
                    _entity.LoadChilds(typeof(Stock), true, true);
					PgMng.Grow();
					SelectStockAction(Datos_Productos.Current as ProductInfo);
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

        protected override void LoadWorks(bool reload = false)
        {
            if (_works == null || reload)
            {
                PgMng.Reset(2 + _entity.Relations.Count, 1, Face.Resources.Messages.LOADING_DATA, this);
                try
                {
                    _works = ExpedienteList.GetList(_entity.Relations, false);
                    Works_BS.DataSource = _works;

                    PgMng.Grow(string.Empty);

                    foreach (ExpedientInfo work in _works)
                    {
                        work.LoadChilds(typeof(Expense), false, false);
                        PgMng.Grow(string.Empty);
                    }
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        protected override void LoadWorkReports(bool reload = false)
        {
            if (_work_reports == null || reload)
            {
                PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
                try
                {
                    _work_reports = WorkReportList.GetByExpedientList(_entity.Oid, false);   
                    WorkReport_BS.DataSource = _work_reports;

                    PgMng.Grow(string.Empty);

                    _work_reports_works = WorkReportList.GetByExpedientList(_entity.Relations.ToChildsOidList(), false);

                    PgMng.Grow(string.Empty);  
                    
                    UpdateWorkReportsTotals();

                    PgMng.Grow(string.Empty);
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

 		protected override void RefreshMainData()
		{
			if (_entity == null) return;

			Datos.DataSource = _entity;
			PgMng.Grow(string.Empty, "Datos");

			Stock_BS.DataSource = _entity.Stocks;
			PgMng.Grow(string.Empty, "Stocks");

			Batchs_BS.RaiseListChangedEvents = false;
			Batchs_BS.DataSource = _entity.Partidas;
			Batchs_BS.RaiseListChangedEvents = true;
			PgMng.Grow(string.Empty, "Partidas");

			Datos_Gastos.DataSource = _entity.Gastos;
			UpdateExpensesList();
			PgMng.Grow(string.Empty, "Gastos");

			ExpensesInvoices_BS.DataSource = _entity.Facturas;
			PgMng.Grow(string.Empty, "Facturas de Acreedores");

            Works_BS.DataSource = _works;             
		}

		public override void RefreshSecondaryData()
		{
			Datos_Productos.DataSource = ProductList.GetSortedList(ProductList.GetListByExpediente(_entity.Oid, false),
																		"Nombre",
																		ListSortDirection.Ascending);
			PgMng.Grow(string.Empty, "Productos");

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
        }

        #endregion

		#region Business Methods

		protected override void AddFacturaGastos(InputInvoiceInfo fac) { AddFacturaGastos(_entity, fac); }

		protected void ActualizaGastosStock()
		{
			BeMermas_NTB.Text = _entity.Stocks.TotalGastosMermas(_entity.Partidas).ToString("C2");
			BeSalidas_NTB.Text = _entity.Stocks.TotalGastosSalidas(_entity.Partidas).ToString("C2");

			CalculateBeneficios();
		}

		protected override void EditFacturaGastos(InputInvoiceInfo fac) { EditFacturaGastos(_entity, fac); }

		protected override decimal GetEstimatedIncome()
		{
			return _entity.IngresosEstimados();
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
				ExpedienteList cache = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;
				if (cache != null) cache.Change(_entity.Oid, _entity.GetInfo(false), true);
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

			Stock_BS.ResetBindings(false);
		}

        protected override void AddWorkAction()
        {
            WorkSelectForm form = new WorkSelectForm(this);
            form.ShowDialog(this);

            if (form.ActionResult == DialogResult.OK)
            {
                ExpedientInfo work = form.Selected as ExpedientInfo;

                if (!_entity.Relations.ContainsRelationChild(work))
                {
                    _entity.Relations.NewItem(_entity, work);
                    _works.AddItem(work);

                    LoadData(true);
                }
            }
        }

		protected override void AddWorkReportAction()
		{
			WorkReportAddForm form = new WorkReportAddForm(_entity, this);
			form.ShowDialog(this);

			if (form.ActionResult == DialogResult.OK)
				_work_reports.AddItem(form.Entity.GetInfo());

			WorkReport_BS.ResetBindings(false);
			UpdateWorkReportsTotals();
            LoadCategoriesExpenses(true);
		}

		protected override void DeleteStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock st = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if ((st.OidAlbaran != 0) || (st.Inicial)) return;

			RemoveStock(st);

			Stock_BS.ResetBindings(false);
		}

        protected override void DeleteWorkAction()
        {
            if (Works_DGW.CurrentRow == null) return;
            if (Works_DGW.CurrentRow.Index < 0) return;
            if (Works_DGW.CurrentRow.DataBoundItem == null) return;

            if (PgMng.ShowDeleteConfirmation() == DialogResult.Yes)
            {
                ExpedientInfo item = (ExpedientInfo)Works_DGW.CurrentRow.DataBoundItem;

                Relation exp = _entity.Relations.GetRelationChild(item);
                if (exp != null) _entity.Relations.Remove(exp);

                LoadData(true);
            }
        }

		protected override void DeleteWorkReportAction()
		{
			if (WorkReport_DGW.CurrentRow == null) return;
			if (WorkReport_DGW.CurrentRow.Index < 0) return;
			if (WorkReport_DGW.CurrentRow.DataBoundItem == null) return;

			if (PgMng.ShowDeleteConfirmation() == DialogResult.Yes)
			{
				WorkReportInfo item = (WorkReportInfo)WorkReport_DGW.CurrentRow.DataBoundItem;
				WorkReport.Delete(item.Oid);
				_work_reports.Remove(item);

				WorkReport_BS.ResetBindings(false);
				UpdateWorkReportsTotals();
                LoadCategoriesExpenses(true);
			}
		}

		protected override void EditStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;
			if (Stock_DGW.CurrentRow.Index < 0) return;
			if (Stock_DGW.CurrentRow.DataBoundItem == null) return;

			Stock s = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if ((s.OidAlbaran != 0) ||
				((s.OidAlbaran == 0) && (s.Observaciones == Library.Store.Resources.Defaults.STOCK_INICIAL)))
			{
				PgMng.ShowInfoException(Resources.Messages.STOCK_FACTURADO);
				return;
			}

			EditStockActionForm form = new EditStockActionForm(s, _entity);
			form.ShowDialog(this);

			Stock_BS.ResetBindings(false);
		}

        protected override void EditWorkAction()
        {
            if (Works_DGW.CurrentRow == null) return;
            if (Works_DGW.CurrentRow.Index < 0) return;
            if (Works_DGW.CurrentRow.DataBoundItem == null) return;

            ExpedientInfo item = (ExpedientInfo)Works_DGW.CurrentRow.DataBoundItem;

            WorkEditForm form = new WorkEditForm(item.Oid, this);
            form.ShowDialog(this);

            LoadData(true);
        }

		protected override void EditWorkReportAction()
		{
			if (WorkReport_DGW.CurrentRow == null) return;
			if (WorkReport_DGW.CurrentRow.Index < 0) return;
			if (WorkReport_DGW.CurrentRow.DataBoundItem == null) return;

			WorkReportInfo item = (WorkReportInfo)WorkReport_DGW.CurrentRow.DataBoundItem;

			WorkReportEditForm form = new WorkReportEditForm(item.Oid, this);
			form.ShowDialog(this);

			item.CopyFrom(form.Entity);

			WorkReport_BS.ResetBindings(false);
			UpdateWorkReportsTotals();
            LoadCategoriesExpenses(true);
		}
        
        protected override void EditEmployeeAction() 
        {
            if (WRStaff_BS.Current == null) return;

            WorkReportResourceInfo item = WRStaff_BS.Current as WorkReportResourceInfo;

            EmployeeEditForm form = new EmployeeEditForm(item.OidResource, this);
            form.ShowDialog();
        }

        protected override void EditToolAction()
        {
            if (WRTools_BS.Current == null) return;

            WorkReportResourceInfo item = WRTools_BS.Current as WorkReportResourceInfo;

            ToolEditForm form = new ToolEditForm(item.OidResource, this);
            form.ShowDialog();
        }

        protected override void EditWorkDeliveryAction()
        {
            if (WRDeliveries_BS.Current == null) return;

            WorkReportResourceInfo item = WRDeliveries_BS.Current as WorkReportResourceInfo;

            DeliveryEditForm form = new DeliveryEditForm(item.OidResource, ETipoEntidad.WorkReport, this);
            form.ShowDialog();
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
                _proveedor = (ProveedorInfo)form.Selected;
                _entity.OidProveedor = _proveedor.Oid;
                _entity.Proveedor = _proveedor.Nombre;

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

        protected void SetGastoPrincipalAction(InputInvoiceInfo item)
        {
            _entity.SetGasto(item);
            _entity.UpdateGastosPartidas(true);
        }

        protected override void ReparteFGastoAction() { ReparteGasto(_entity); }

		#endregion

        #region Buttons

        #endregion

        #region Events

		private void ObraUIForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_action_result != DialogResult.OK)
				if (DialogResult.No == ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.CANCEL_CONFIRM))
				{
					e.Cancel = true;
					return;
				}
		}

        #endregion
    }
}