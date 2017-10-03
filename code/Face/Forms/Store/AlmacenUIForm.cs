using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using moleQule.CslaEx;

using moleQule;
using moleQule.Common;

using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Controls;

namespace moleQule.Face.Store
{
    public partial class AlmacenUIForm : AlmacenForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }
        public const string ID = "AlmacenUIForm";

        /// <summary>
        /// Se trata de la Expedient actual y que se va a editar.
        /// </summary>
		protected Almacen _entity;

        public override Almacen Entity { get { return _entity; } set { _entity = value; } }
		public override StoreInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

		public AlmacenUIForm(long oid, Form parent) 
            : base(oid, parent)
        {
            InitializeComponent();

            if (_entity != null) SetFormData();

            _mf_type = ManagerFormType.MFEdit;
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Almacen temp = _entity.Clone();
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

        protected override void HideComponentes(TabPage page)
        {
            if (TabProductos.Equals(page))
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in Partidas_DGW.Rows)
                {
                    if ((row.DataBoundItem as Batch).IsKitComponent)
                        row.Visible = false;
                    else if ((row.DataBoundItem as Batch).StockKilos == 0)
                        row.Cells["ColumnaPEStockKilos"].Style.ForeColor = Color.Red;
                }
            }
            else if (TabStock.Equals(page))
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[Stock_DGW.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in Stock_DGW.Rows)
                    if ((row.DataBoundItem as Stock).IsKitComponent)
                        row.Visible = false;
            }
            else if (TabResumen.Equals(page))
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[Resumen_DGW.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in Resumen_DGW.Rows)
                    if ((row.DataBoundItem as Batch).IsKitComponent)
                        row.Visible = false;
            }
        }

        #endregion

        #region Source
		
		protected override void LoadPartidas(ProductInfo producto)
		{
			if (producto == null) return;

			try
			{
				PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
				PgMng.Grow();

				_entity.LoadPartidasByProducto(producto.Oid, true);
				PgMng.Grow();

				ActualizaBindings();
				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected override void LoadStock(ProductInfo producto)
		{
			if (producto == null) return;

			try
			{
				PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

				PgMng.Grow();

                _entity.LoadStockByProducto(producto.Oid, true, true);
				SelectStockAction(producto);
				PgMng.Grow();

				ActualizaBindings();
				PgMng.Grow();
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_Partidas.DataSource = _entity.Partidas;
			PgMng.Grow();

            Datos_Stock.DataSource = _entity.Stocks;
			PgMng.Grow();

            Datos_Resumen.DataSource = _entity.Partidas;
			PgMng.Grow();
        }

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		protected override void UpdateStocks() 
		{
            _entity.UpdateStocks(true);
		}
		
		private int DaysToNextMonth(DateTime dt)
		{
			int daysto = 0;
			DateTime aux = new DateTime(dt.Year, dt.Month, dt.Day);
			int month = aux.Month;
			while (month == aux.Month)
			{
				daysto++;
				aux = aux.AddDays((double)1);
			}
			return daysto;
		}

		#endregion

		#region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
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
            //MovimientosStockListPorExpedienteRpt rpt = reportMng.GetMovimientosStockListAgrupado(_entity.GetInfo(true), null, null, filter, format);

			//ShowReport(rpt);
        }

		protected virtual void SelectEstadoAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
				Estado_TB.Text = _entity.EstadoLabel;
			}
		}

        protected override void SelectProductoAction(ProductInfo producto)
        {
            if (producto == null) return;

			LoadPartidas(producto);

            FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal); 
            Datos_Partidas.DataSource = _entity.Partidas.GetSubList(criteria);

			ActualizaBindings();
            //SelectStockAction(producto);
        }

        protected override void SelectStockAction(ProductInfo producto)
        {
            if (producto == null) return;

            FCriteria criteria = new FCriteria<long>("OidProducto", producto.Oid, Operation.Equal);
            Datos_Stock.DataSource = _entity.Stocks.GetSubList(criteria);
			
			ActualizaBindings();
        }

		protected override void EditStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;

			/*Stock s = Stock_DGW.CurrentRow.DataBoundItem as Stock;
			EditStockActionForm form = new EditStockActionForm(s, _entity);
			form.ShowDialog(this);
			ActualizaBindings();*/
		}
		protected override void NuevoStockAction()
		{
			/*if (_entity.Partidas.Count == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_PRODUCTS_ASSOC);
				return;
			}
			AddStockInputForm form = new AddStockInputForm(_entity);
			form.ShowDialog(this);*/

			ActualizaBindings();
		}
		protected override void RemoveStockAction()
		{
			if (Stock_DGW.CurrentRow == null) return;

			Stock stock = (Stock)Stock_DGW.CurrentRow.DataBoundItem;

			if ((stock.OidAlbaran != 0) || (stock.Inicial))
			{
				PgMng.ShowInfoException(Resources.Messages.STOCK_FACTURADO);
				return;
			}

			if (PgMng.ShowDeleteConfirmation() == DialogResult.No)
			{
				return;
			}

			_entity.RemoveStock(stock);
			ActualizaBindings();
		}

        #endregion

        #region Buttons

		private void Estado_BT_Click(object sender, EventArgs e) { SelectEstadoAction(); }

        #endregion

        #region Events

        private void Partidas_DGW_Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Productos_DGW.CurrentRow == null) return;

            if (Partidas_DGW.Columns[e.ColumnIndex].DataPropertyName == "GastoKilo")
            {
                Batch item = Partidas_DGW.CurrentRow.DataBoundItem as Batch;
                item.CalculaCostes(item.GastoKilo, 0);
				ActualizaBindings();
            }
        }

        private void ValidateTextBox(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToDecimal(((TextBox)sender).Text);
                errorProvider1.SetError((Control)sender, "");
            }
            catch
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.INVALID_FORMAT,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                errorProvider1.SetError((Control)sender, "El valor debe ser numerico");
            }
        }

        #endregion

    }
}

