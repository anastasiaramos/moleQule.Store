using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Sales;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ContenedorForm : ExpedienteForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }
			
        #endregion

        #region Factory Methods

        public ContenedorForm() 
			: this(-1, moleQule.Store.Structs.ETipoExpediente.Todos) {}

		public ContenedorForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo) 
			: this(oid, tipo, true, null) { }

		public ContenedorForm(long oid, moleQule.Store.Structs.ETipoExpediente tipo, bool is_modal, Form parent)
            : base(oid, tipo, is_modal, parent)
        {
            InitializeComponent();
            CancelConfirmation = false;
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			base.FitColumns();

			List<DataGridViewColumn> cols =  new List<DataGridViewColumn>();
			CurrencyManager cm;

			switch (EntityInfoNoChilds.ETipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Ganado:

					cm = (CurrencyManager)BindingContext[LibroGanadero_DGW.DataSource];
					cm.SuspendBinding();

					//Tabla Cabeza
					cols.Clear();
					ObservacionesCabeza.Tag = 1;

					cols.Add(ObservacionesCabeza);

					ControlsMng.MaximizeColumns(LibroGanadero_DGW, cols);

					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					cm = (CurrencyManager)BindingContext[Maquinarias_DGW.DataSource];
					cm.SuspendBinding();

					//Tabla Maquinaria
					cols.Clear();
					ObservacionesMaquina.Tag = 1;

					cols.Add(ObservacionesMaquina);

					ControlsMng.MaximizeColumns(Maquinarias_DGW, cols);

					break;
			}

			cm = (CurrencyManager)BindingContext[InvoicedExpenses_DGW.DataSource];
			cm.SuspendBinding();
		}

		public override void FormatControls()
        {
            if (Stock_DGW == null) return;
			if (EntityInfoNoChilds == null) return;

            ShowStatusBar(Resources.Messages.CAMPOS_EXPEDIENTES);
			
			ShowAction(molAction.ShowDocuments);

            base.MaximizeForm(new Size(1200, 0));
            base.FormatControls();

			switch (EntityInfoNoChilds.ETipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:

					this.Icon = Properties.Resources.alimentacion;

					Partidas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					Partidas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

					Pestanas_TC.TabPages.Remove(Cabezas_TP);
					Pestanas_TC.TabPages.Remove(Maquinaria_TP);

					break;

				case moleQule.Store.Structs.ETipoExpediente.Ganado:

					this.Icon = Properties.Resources.ganado;

					LibroGanadero_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					LibroGanadero_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

					Pestanas_TC.TabPages.Remove(Maquinaria_TP);

                    Productos_TP.Text = "Cabezas";

					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					this.Icon = Properties.Resources.maquinaria;

					Maquinarias_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					Maquinarias_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

					Pestanas_TC.TabPages.Remove(Cabezas_TP);

					break;
			}

			SetExpedienteREAFormat();
		}

		protected override void SetRowFormat(DataGridViewRow row, string columnName, object value)
		{
			if (columnName == FechaSolicitudLFomento.Name)
			{
				row.Cells[FechaSolicitudLFomento.Index].Style = ((DateTime)value == DateTime.MinValue) ? Face.Common.ControlTools.Instance.TransparentStyle : Face.ControlTools.Instance.BasicStyle;
			}
			else if (columnName == FechaDespachoREA.Name)
			{
				row.Cells[FechaDespachoREA.Index].Style.ForeColor = ((DateTime)value == DateTime.MinValue) ? Color.Transparent : row.DefaultCellStyle.ForeColor;
				row.Cells[FechaDespachoREA.Index].Style.SelectionForeColor = ((DateTime)value == DateTime.MinValue) ? Color.Transparent : row.DefaultCellStyle.SelectionForeColor;
			}
			else if (columnName == FechaCobroRea.Name)
			{
				row.Cells[FechaCobroRea.Index].Style.ForeColor = ((DateTime)value == DateTime.MinValue) ? Color.Transparent : row.DefaultCellStyle.ForeColor;
				row.Cells[FechaCobroRea.Index].Style.SelectionForeColor = ((DateTime)value == DateTime.MinValue) ? Color.Transparent : row.DefaultCellStyle.SelectionForeColor;
			}
		}

		protected virtual void SetExpedienteREAFormat() { }
		protected override void SetBeneficiosFormat() { SetBeneficiosFormat(EntityInfoNoChilds.ETipoExpediente); }
		protected override void SetStockFormat() { { SetStockFormat(EntityInfoNoChilds.ETipoExpediente); } }

		#endregion

		#region Source

		protected override void LoadData()
		{
			if (Pestanas_TC.SelectedTab == Fomento_TP)
			{
				LoadFomento();
			}
			else if (Pestanas_TC.SelectedTab == REA_TP)
			{
				LoadAyudas();
			}
			else if (Pestanas_TC.SelectedTab == Cabezas_TP)
			{
				LoadLibroGanadero();
			}
			else if (Pestanas_TC.SelectedTab == Benefits_TP)
			{
				LoadCostes();
				LoadIncomes();
				LoadAyudas();
				LoadStock();
			}
			else
				base.LoadData();
		}

		protected virtual void LoadAyudas() { }
		protected virtual void LoadLibroGanadero() { }
		protected virtual void LoadFomento() { }

		#endregion

		#region Business Methods

		protected override decimal GetExpenses()
		{
			return base.GetExpenses() - BeSalidas_NTB.DecimalValue;
		}
		protected override decimal GetIncome()
		{
			return base.GetIncome() + AyudaTotal_NTB.DecimalValue + AyudaMermas_NTB.DecimalValue;
		}

        #endregion

        #region Actions

		protected virtual void EditCabezaAction() { }

		protected virtual void DeleteREAAction() { }
		protected virtual void NewREAAction() { }

		protected virtual void EditLineaFomentoAction() { }
		protected virtual void DeleteLineaFomentoAction() { }
		protected virtual void NewLineaFomentoAction() { }
		protected virtual void NullLineaFomentoAction() { }
		protected virtual void AttachFacturaNavieraActon() { }
		protected virtual void AdjuntarDocumentosFomentoAction() { }
		protected virtual void SelectEstadoFomentoAction() { }
		protected virtual void SelectTeusFomentoAction(DataGridViewRow row) { }

		protected virtual void SelectEstadoExpedienteREAAction() { }

        #endregion

        #region Buttons

        private void EditCabeza_TI_Click(object sender, EventArgs e) { EditCabezaAction(); }

        private void Producto_BT_Click(object sender, EventArgs e) { SelectProductNameAction(); }
		
		private void DeleteREA_TI_Click(object sender, EventArgs e) { DeleteREAAction(); }
		private void NewREA_TI_Click(object sender, EventArgs e) { NewREAAction(); }

		private void EditLineaFomento_TI_Click(object sender, EventArgs e) { EditLineaFomentoAction(); }
		private void DeleteLineaFomento_TI_Click(object sender, EventArgs e) { DeleteLineaFomentoAction(); }
		private void NewLineaFomento_TI_Click(object sender, EventArgs e) { NewLineaFomentoAction(); }
		private void NullLineaFomento_TI_Click(object sender, EventArgs e) { NullLineaFomentoAction(); }
		private void AsociarFacturaNaviera_TI_Click(object sender, EventArgs e) { AttachFacturaNavieraActon(); }
		private void DocumentosFomento_TI_Click(object sender, EventArgs e) { AdjuntarDocumentosFomentoAction(); }

        #endregion

        #region Events

		private void Datos_DataSourceChanged(object sender, EventArgs e)
		{
			//SetDependentControlSource(Teus20_RB.Name);
			//SetDependentControlSource(Teus40_RB.Name);
		}

		private void Cabeza_DGW_DoubleClick(object sender, EventArgs e) { EditCabezaAction(); }

		private void Fomento_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Fomento_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Fomento_DGW.Columns[e.ColumnIndex].DataPropertyName == EstadoLFomento.DataPropertyName)
			{
				SelectEstadoFomentoAction();
			}
			else if (Fomento_DGW.Columns[e.ColumnIndex].Name == TeusLFomento.Name)
			{
				DataGridViewRow row = Fomento_DGW.CurrentRow;
				SelectTeusFomentoAction(row);
			}
		}

		private void ExpedienteREA_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (ExpedienteREA_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (ExpedienteREA_DGW.Columns[e.ColumnIndex].DataPropertyName == EstadoExpedienteREA.DataPropertyName)
			{
				SelectEstadoExpedienteREAAction();
			}
		}

		private void Maquinaria_DGW_DoubleClick(object sender, EventArgs e) { EditBatchAction(); }		

        #endregion
	}
}

