using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule;
using moleQule.CslaEx;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Sales;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ExpedienteAlmacenForm : ExpedienteForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }
			
        #endregion

        #region Factory Methods

        public ExpedienteAlmacenForm() 
			: this(-1) {}

		public ExpedienteAlmacenForm(long oid) 
			: this(oid, true, null) { }

        public ExpedienteAlmacenForm(long oid, bool is_modal, Form parent)
            : base(oid, moleQule.Store.Structs.ETipoExpediente.Almacen, is_modal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			base.FitColumns();

			/*List<DataGridViewColumn> cols;
			CurrencyManager cm;

			cm = (CurrencyManager)BindingContext[Partidas_DGW.DataSource];
			cm.SuspendBinding();

			cols = new List<DataGridViewColumn>();
			ColumnaPETipo.Tag = 1;

			cols.Add(ColumnaPETipo);

			ControlsMng.MaximizeColumns(Partidas_DGW, cols);*/
		}

		public override void FormatControls()
        {
            if (Stock_DGW == null) return;
			if (EntityInfoNoChilds == null) return;

            ShowStatusBar(Resources.Messages.CAMPOS_EXPEDIENTES);
			
			ShowAction(molAction.ShowDocuments);

            base.MaximizeForm(new Size(1200, 0));
            base.FormatControls();

			this.Icon = Properties.Resources.store;

			Pestanas_TC.TabPages.Remove(Costes_TP);

			Partidas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			Partidas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
       }

		#endregion

		#region Source

		protected override void LoadData()
		{
			base.LoadData();
		}

		#endregion

		#region Business Methods

		protected override void CalculateTotales()
		{
			BeTotalGastos_NTB.Text = ((decimal)(BeCostes_NTB.DecimalValue + BeExpenses_NTB.DecimalValue - BeSalidas_NTB.DecimalValue)).ToString("C2");
			BeIngresosReal_NTB.Text = ((decimal)(BePurchases_NTB.DecimalValue)).ToString("C2");
		}

        #endregion
        
        #region Actions

        #endregion

        #region Buttons

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            SelectProductNameAction();
        }

        #endregion

        #region Events

        #endregion

	}
}

