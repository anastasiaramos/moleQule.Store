using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InputDeliveryForm : Skin04.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		public override Type EntityType { get { return typeof(InputDelivery); } }

		public virtual InputDelivery Entity { get { return null; } set { } }
		public virtual InputDeliveryInfo EntityInfo { get { return null; } }

		protected IAcreedorInfo _provider = null;
		protected StoreInfo _store = null;
		protected ExpedientInfo _expedient = null;
		protected TransporterInfo _transporter = null;
		protected SerieInfo _serie = null;

        protected ETipoAlbaranes _deliveryType = ETipoAlbaranes.Todos;

        #endregion

        #region Factory Methods

        public InputDeliveryForm() 
			: this(-1, null, true, null ) {}

		public InputDeliveryForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			LineConcepto.Tag = 1;

			cols.Add(LineConcepto);

			ControlsMng.MaximizeColumns(Lines_DGW, cols);

			Lines_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

        public override void FormatControls()
        {
            MaximizeForm(1200,0);
            base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.FACTURAR, Properties.Resources.factura_recibida);

			Fecha_DTP.Checked = true;

			Lines_DGW.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			Lines_DGW.AllowUserToResizeRows = true;
			Lines_DGW.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
			Lines_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
			Lines_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

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

            HideComponentes();
        }

		protected virtual void RefreshLines()
		{
			Lines_BS.ResetBindings(true);
			Lines_DGW.Refresh();
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

		#endregion

		#region Source

		public override void RefreshSecondaryData()
		{
			PaymentMethods_BS.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList(false);
            PgMng.Grow();

            EFormaPago[] formas_pago = { EFormaPago.Contado, 
                                        EFormaPago.XDiasFechaFactura, 
                                        EFormaPago.XDiasMes, 
                                        EFormaPago.Trimestral };

			PaymentWays_BS.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList(formas_pago, false);
			PgMng.Grow();
		}

        protected virtual void HideComponentes() { }

        #endregion

		#region Validation & Format

		#endregion

        #region Print

        public override void PrintObject()
        {
            /*AlbaranReportMng reportMng = new AlbaranReportMng(AppContext.ActiveSchema);
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();
            conf.nota = EntityInfo.Nota ? Nota_TB.Text : "";

            ReportViewer.SetReport(reportMng.GetAlbaranProveedorReport(EntityInfo, conf));
            ReportViewer.ShowDialog();*/
        }

        #endregion

        #region Actions

        protected override void PrintAction() { PrintObject(); }

        protected virtual void RectificativoAction() {}
        protected virtual void NewLineAction(bool stock) {}
        protected virtual void EditLineAction() {}
        protected virtual void DeleteLineAction() {}
		protected virtual void SelectLineStoreAction() { }
		protected virtual void SelectLineExpedientAction() { }
		protected virtual void SelectLineTaxAction() { }
        protected virtual void SetIRPFAction() { }

		protected virtual void UpdateDeliveryAction() { }

        #endregion

        #region Buttons

        private void NuevoConcepto_BT_Click(object sender, EventArgs e) { NewLineAction(false); }

		private void NuevoConceptoStock_TI_Click(object sender, EventArgs e) { NewLineAction(true); }

        private void EditarConcepto_BT_Click(object sender, EventArgs e) { EditLineAction(); }

        private void EliminarConcepto_BT_Click(object sender, EventArgs e) { DeleteLineAction(); }

        #endregion

        #region Events

        private void NAlbaranProveedorManual_CKB_CheckedChanged(object sender, EventArgs e)
        {
            NAlbaranProveedor_TB.ReadOnly = !IDManual_CkB.Checked;
            NAlbaranProveedor_TB.BackColor = NAlbaranProveedor_TB.ReadOnly ? DiasPago_TB.BackColor : Color.White;
			NAlbaranProveedor_TB.ForeColor = NAlbaranProveedor_TB.ReadOnly ? DiasPago_TB.ForeColor : Color.Navy;
        }

		private void Lines_DGW_CellValidated(object sender, DataGridViewCellEventArgs e) { UpdateDeliveryAction(); }

		private void Lines_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.Columns[e.ColumnIndex].Name == LinePTaxes.Name) SelectLineTaxAction();
			else if (Lines_DGW.Columns[e.ColumnIndex].Name == LineStoreID.Name) SelectLineStoreAction();
			else if (Lines_DGW.Columns[e.ColumnIndex].Name == LineExpedient.Name) SelectLineExpedientAction();
		}

        private void Lines_DGW_DoubleClick(object sender, EventArgs e) { DefaultAction(); }
        
        #endregion
    }
}

