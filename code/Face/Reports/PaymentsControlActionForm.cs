using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Payment;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PaymentsControlActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public const string ID = "PaymentsControlActionForm";
        public static Type Type { get { return typeof(PaymentsControlActionForm); } }

        private ReportFilter _report_filter;

        #endregion
                
        #region Factory Methods

        public PaymentsControlActionForm()
            : this(null) { }

        public PaymentsControlActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Layout & Source

        public override void RefreshSecondaryData()
        {
            Datos_TiposExp.DataSource = moleQule.Store.Structs.EnumText<moleQule.Store.Structs.ETipoExpediente>.GetList(false);
            TipoExpediente_CB.SelectedItem = (long)moleQule.Store.Structs.ETipoExpediente.Todos;
            Bar.Grow();

            Datos_TiposAcreedor.DataSource = moleQule.Store.Structs.EnumText<ETipoInforme>.GetList(false);
            TipoAcreedor_CB.SelectedItem = (long)ETipoInforme.Todos;
            Bar.Grow();

            Datos_TiposFactura.DataSource = moleQule.Store.Structs.EnumText<EPagos>.GetList(false);
            TipoAcreedor_CB.SelectedItem = (long)EPagos.Todos;
            Bar.Grow();
        }

        #endregion

		#region Business Methods

		private string GetFilterValues()
		{
			string filtro = string.Empty;

			filtro += "Tipo Acreedor " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoAcreedor_CB.SelectedItem as ComboBoxSource).Texto + ";";
			filtro += "Tipo Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoExpediente_CB.SelectedItem as ComboBoxSource).Texto + "; ";
			filtro += "Tipo Factura " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoFactura_CB.SelectedItem as ComboBoxSource).Texto + ";";

			if (FFacturaIni_DTP.Checked)
				filtro += "Fecha Factura " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FFacturaIni_DTP.Value.ToShortDateString() + "; ";

			if (FFacturaFin_DTP.Checked)
				filtro += "Fecha Factura " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FFacturaFin_DTP.Value.ToShortDateString() + "; ";

			if (FPagoIni_DTP.Checked)
				filtro += "Fecha Pago " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FPagoIni_DTP.Value.ToShortDateString() + "; ";

			if (FPagoFin_DTP.Checked)
				filtro += "Fecha Pago " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FPagoFin_DTP.Value.ToShortDateString() + "; ";

			if (FPrevisionIni_DTP.Checked)
				filtro += "Fecha Previsión " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FPrevisionIni_DTP.Value.ToShortDateString() + "; ";

			if (FPrevisionFin_DTP.Checked)
				filtro += "Fecha Previsión " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FPrevisionFin_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		#endregion

        #region Actions

        protected override void PrintAction()
        {
			try
			{
				PgMng.Reset(6, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

				if (FFacturaIni_DTP.Checked)
					_report_filter.fecha_fac_inicio = FFacturaIni_DTP.Value;
				else
					_report_filter.fecha_fac_inicio = DateTime.MinValue;

				if (FFacturaFin_DTP.Checked)
					_report_filter.fecha_fac_final = FFacturaFin_DTP.Value;
				else
					_report_filter.fecha_fac_final = DateTime.MaxValue;

				if (FPagoIni_DTP.Checked)
					_report_filter.fecha_pago_inicio = FPagoIni_DTP.Value;
				else
					_report_filter.fecha_pago_inicio = DateTime.MinValue;

				if (FPagoFin_DTP.Checked)
					_report_filter.fecha_pago_final = FPagoFin_DTP.Value;
				else
					_report_filter.fecha_pago_final = DateTime.MaxValue;

				if (FPrevisionIni_DTP.Checked)
					_report_filter.prevision_ini = FPrevisionIni_DTP.Value;
				else
					_report_filter.prevision_ini = DateTime.MinValue;

				if (FPrevisionFin_DTP.Checked)
					_report_filter.prevision_fin = FPrevisionFin_DTP.Value;
				else
					_report_filter.prevision_fin = DateTime.MaxValue;

				_report_filter.exp_final = string.Empty;
				_report_filter.exp_inicial = string.Empty;
				_report_filter.tipo = (EPagos)(long)TipoFactura_CB.SelectedValue;
				_report_filter.tipo_informe = (ETipoInforme)(long)TipoAcreedor_CB.SelectedValue;
				_report_filter.tipo_expediente = (moleQule.Store.Structs.ETipoExpediente)(long)TipoExpediente_CB.SelectedValue;

				ReportFormat format = new ReportFormat();
				format.Vista = (Resumido_RB.Checked) ? EReportVista.Resumido : EReportVista.Detallado;

				string filtro = GetFilterValues();
				PgMng.Grow();

				ExpedienteList expedients = ExpedienteList.GetList(_report_filter.tipo_expediente, false);
				PgMng.Grow();

                InputInvoiceList invoices = InputInvoiceList.GetListByDate(_report_filter.fecha_fac_inicio, _report_filter.fecha_fac_final, false);
				PgMng.Grow();

				PaymentSummaryList summaries = PaymentSummaryList.GetPendientesList();
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, this.Text, filtro);
				PaymentsReportRpt rpt = reportMng.GetInformeControlPagosReport(invoices, expedients, summaries, _report_filter, format);
				PgMng.FillUp();

				ShowReport(rpt);

				_action_result = DialogResult.Ignore;
			}
			catch (Exception ex)
			{
				PgMng.FillUp();
				throw ex;
			}
        }

        #endregion

        #region Buttons

        private void Detalle_BT_Click(object sender, EventArgs e)
        {            
            switch ((ETipoInforme)(long)TipoAcreedor_CB.SelectedValue)
            {
                case ETipoInforme.Despachante:
                    {
                        CustomAgentSelectForm form = new CustomAgentSelectForm(this, moleQule.Base.EEstado.Active);
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            DespachanteInfo d = form.Selected as DespachanteInfo;
                            Acreedores_TB.Text = d.Nombre;
                            _report_filter.objeto_detallado = form.Selected;
                        }
                    }
                    break;
                case ETipoInforme.Naviera:
                    {
                        ShippingCompanySelectForm form = new ShippingCompanySelectForm(this, moleQule.Base.EEstado.Active);
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            NavieraInfo n = form.Selected as NavieraInfo;
                            Acreedores_TB.Text = n.Codigo + " - " + n.Nombre;
                            _report_filter.objeto_detallado = form.Selected;
                        }
                    }
                    break;
                case ETipoInforme.Proveedor:
                    {
						ProveedorList list = ProveedorList.GetList(moleQule.Base.EEstado.Active, false);
                        SupplierSelectForm form = new SupplierSelectForm(this, list);

                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            ProveedorInfo p = form.Selected as ProveedorInfo;
                            Acreedores_TB.Text = p.Codigo + " - " + p.Nombre;
                            _report_filter.objeto_detallado = form.Selected;
                        }
                    }
                    break;
                case ETipoInforme.TransportistaDestino:
                    {
						TransporterSelectForm form = new TransporterSelectForm(this, TransporterList.GetList(ETipoTransportista.Destino, false));
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            TransporterInfo t = (TransporterInfo)form.Selected;
                            Acreedores_TB.Text = t.Codigo + " - " + t.Nombre;
                            _report_filter.objeto_detallado = form.Selected;
                        }
                    }
                    break;
                case ETipoInforme.TransportistaOrigen:
                    {
						TransporterSelectForm form = new TransporterSelectForm(this, TransporterList.GetList(ETipoTransportista.Origen, false));
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            TransporterInfo t = (TransporterInfo)form.Selected;
                            Acreedores_TB.Text = t.Codigo + " - " + t.Nombre;
                            _report_filter.objeto_detallado = form.Selected;
                        }
                    }
                    break;
            }
        }
        
        #endregion

        #region Events

        private void TipoAcreedor_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoAcreedor_CB.SelectedValue == null) return;
            ETipoInforme tipo = (ETipoInforme)(long)TipoAcreedor_CB.SelectedValue;
            Detalle_BT.Enabled = tipo != ETipoInforme.Todos;
            Acreedores_TB.Text = (tipo == ETipoInforme.Todos) ? string.Empty : Acreedores_TB.Text;
        }

        #endregion
    }
}