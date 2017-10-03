using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Invoice;
using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class InputInvoiceForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		public virtual InputInvoice Entity { get { return null; } set { } }
		public virtual InputInvoiceInfo EntityInfo { get { return null; } }

        public CompanyInfo _company;

		protected List<InputInvoiceLineInfo> _conceptos_selected = new List<InputInvoiceLineInfo>();
		public List<InputInvoiceLineInfo> ConceptosSelected { get { return _conceptos_selected; } }


        #endregion

        #region Factory Methods

        public InputInvoiceForm() 
			: this(-1, ETipoAcreedor.Todos) { }

        public InputInvoiceForm(long oid, ETipoAcreedor tipo) 
			: this(oid, tipo, true, null) {}

		public InputInvoiceForm(long oid, InputInvoiceInfo factura, bool isModal, Form parent)
			: this(oid, new object[1] { factura }, isModal, parent) {}

        public InputInvoiceForm(long oid, ETipoAcreedor tipo, bool isModal, Form parent)
			: this(oid, new object[1] { tipo }, isModal, parent) { }

		public InputInvoiceForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			ConceptosConcepto.Tag = 1;

			cols.Add(ConceptosConcepto);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);
		}

        public override void FormatControls()
        {
			MaximizeForm(1200, 0);
			base.FormatControls();

			Fecha_DTP.Checked = true;

			Lineas_DGW.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			Lineas_DGW.AllowUserToResizeRows = true;
			Lineas_DGW.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
			Lineas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
			try
			{
				string number_format = "N" + AppControllerBase.Culture.NumberFormat.CurrencyDecimalDigits;
				Base_NTB.DataBindings[0].FormatString = number_format;
				Impuestos_NTB.DataBindings[0].FormatString = number_format;
				IRPF_NTB.DataBindings[0].FormatString = number_format;
				Total_NTB.DataBindings[0].FormatString = "C" + AppControllerBase.Culture.NumberFormat.CurrencyDecimalDigits;
				LineTax.DefaultCellStyle.Format = number_format;
				LineIRPF.DefaultCellStyle.Format = number_format;
				LineTotal.DefaultCellStyle.Format = number_format;

				string price_format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
				LinePrice.DefaultCellStyle.Format = price_format;
			}
			catch { }

        }

		protected void LockIsSelected(DataGridViewRow row, bool lockSeleccion)
		{
			row.ReadOnly = lockSeleccion;
			row.Cells[IsSelected.Index].ReadOnly = lockSeleccion;
			row.DefaultCellStyle.BackColor = lockSeleccion ? ControlTools.Instance.LineaPendienteStyle.BackColor : LineTotal.DefaultCellStyle.BackColor;
		}

		protected virtual void RefreshLineas()
		{
			Datos_Lineas.ResetBindings(true);
			Lineas_DGW.Refresh();
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		protected void ShowSelectColumn()
		{
			IsSelected.Visible = true;
			IsSelected.ReadOnly = false;
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            _company = CompanyInfo.Get(AppContext.ActiveSchema.Oid);
            PgMng.Grow();
        }

		public override void RefreshSecondaryData()
		{
			Datos_MedioPago.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList(false);
			PgMng.Grow();

            EFormaPago[] formas_pago = { EFormaPago.Contado, EFormaPago.XDiasFechaFactura, EFormaPago.XDiasMes, EFormaPago.Trimestral };
			Datos_FormaPago.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList(formas_pago, false);
			PgMng.Grow();
		}

        protected virtual void HideComponentes() {}

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void PrintAction() { PrintObject(); }

	    public override void PrintObject()
        {
            /*InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema);
            
			FormatConfFacturaProveedorAlbaranReport conf = new FormatConfFacturaProveedorAlbaranReport();
            conf.nota = EntityInfo.Nota ? Nota_TB.Text : "";
            conf.cabecera = Cabecera_CB.SelectedItem == null ? "FACTURA" : Cabecera_CB.SelectedItem.ToString();
            conf.copia = Copia_CKB.Checked ? "COPIA" : "";
            conf.cuenta_bancaria = Cuenta_TB.Text; 
            conf.forma_pago = "";

            ReportViewer.SetReport(reportMng.GetFacturaProveedorReport(EntityInfo, conf));
            ReportViewer.ShowDialog();

            ReportClass report = reportMng.GetQRCodeReport(EntityInfo);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);*/

            InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema);
            
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			ProviderBaseInfo provider = ProviderBaseInfo.Get(EntityInfo.OidAcreedor, EntityInfo.ETipoAcreedor, false);
            SerieInfo serie = SerieInfo.Get(EntityInfo.OidSerie, false);

            conf.nota = (provider.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
            conf.nota += (conf.nota != string.Empty) ? Environment.NewLine : string.Empty;
            conf.nota += (EntityInfo.Nota ? serie.Cabecera : "");
            conf.cuenta_bancaria = EntityInfo.CuentaBancaria;
            PgMng.Grow();

            ReportClass report = reportMng.GetDetailReport(EntityInfo, conf);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);
        }

		protected virtual void EliminarAlbaranAction() {}
		protected virtual void NuevoAlbaranAction() { }
		protected void SelectConceptosAction()
		{
			_conceptos_selected.Clear();

			foreach (DataGridViewRow row in Lineas_DGW.Rows)
				SelectConceptoAction(row);	
		}
		protected void SelectConceptoAction(DataGridViewRow row)
		{
			if (row == null) return;

			InputInvoiceLineInfo concepto = (InputInvoiceLineInfo)row.DataBoundItem;

			if (concepto.IsSelected)
				_conceptos_selected.Add(row.DataBoundItem as InputInvoiceLineInfo);
		}
		protected virtual void SelectExpedienteLineaAction() { }
		protected virtual void ShowAlbaranAction() { }
		
		protected virtual void UpdateFacturaAction() { }

        protected virtual void SetIRPFAction() { }

        #endregion

        #region Buttons

		private void Add_TI_Click(object sender, EventArgs e)
		{
			NuevoAlbaranAction();
		}

		private void Delete_TI_Click(object sender, EventArgs e)
		{
			EliminarAlbaranAction();
		}

        #endregion
        
        #region Events

		private void Conceptos_DGW_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(Lineas_DGW.Rows[e.RowIndex]);
		}

		private void Conceptos_DGW_CellValidated(object sender, DataGridViewCellEventArgs e) { UpdateFacturaAction(); }

		private void Conceptos_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.Columns[e.ColumnIndex].Name == IsSelected.Name)
			{
				if (Lineas_DGW.CurrentRow.ReadOnly) return;

				InputInvoiceLineInfo item = (InputInvoiceLineInfo)Lineas_DGW.CurrentRow.DataBoundItem;
				item.IsSelected = !item.IsSelected;
				item.OidExpediente = item.IsSelected ? item.OidExpediente : 0;
				item.Expediente = item.IsSelected ? item.Expediente : string.Empty;
			}
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == Expedient.Name) SelectExpedienteLineaAction();
		}

        private void Conceptos_DGW_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowAlbaranAction();
        }

        private void PIRPF_NTB_Validated(object sender, EventArgs e)
        {
            SetIRPFAction();
        }

        #endregion
   }
}

