using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expense;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InformeGastosExpedienteActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

		public const string ID = "InformeGastosExpedienteActionForm";
		public static Type Type { get { return typeof(InformeGastosExpedienteActionForm); } }

		ExpedientInfo _expediente;
		ExpedientInfo _expediente_ini;
		ExpedientInfo _expediente_fin;

		ReportFilter _filter = new ReportFilter();

        #endregion
                
        #region Factory Methods

		public InformeGastosExpedienteActionForm()
			: this(null) {}

        public InformeGastosExpedienteActionForm(Form parent)
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
            PgMng.Grow();

            ETipoAcreedor[] list = { ETipoAcreedor.Todos, 
                                    ETipoAcreedor.Proveedor, 
                                    ETipoAcreedor.Despachante, 
                                    ETipoAcreedor.Naviera, 
                                    ETipoAcreedor.TransportistaOrigen, 
                                    ETipoAcreedor.TransportistaDestino };
            Datos_TiposAcreedor.DataSource = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetList(list, false);
            TipoAcreedor_CB.SelectedValue = (long)ETipoAcreedor.Todos;
            PgMng.Grow();
        }

        #endregion

		#region Business Methods

		private string GetFilterValues()
		{
			string filtro = string.Empty;

            if (Seleccion_RB.Checked)
                filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _expediente.Codigo + "; ";
            else if (Rango_RB.Checked)
            {
                filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + _expediente_ini.Codigo + "; ";
                filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + _expediente_fin.Codigo + "; ";
            }
            else
                filtro += "Tipo Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoExpediente_CB.SelectedItem as ComboBoxSource).Texto + "; " +
                    "Tipo Acreedor " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + (TipoAcreedor_CB.SelectedItem as ComboBoxSource).Texto + "; ";

			if (FInicial_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";
            
			return filtro;
		}

		#endregion

        #region Actions

        protected override void PrintAction()
        {
			PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			if (Seleccion_RB.Checked && (_expediente == null)) return;
			if (Rango_RB.Checked && ((_expediente_ini == null) || (_expediente_fin == null))) return;

			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
			{
				Expedient = Seleccion_RB.Checked ? _expediente : null,
				TipoExpediente = TodosExpediente_RB.Checked ? (moleQule.Store.Structs.ETipoExpediente)(long)TipoExpediente_CB.SelectedValue : moleQule.Store.Structs.ETipoExpediente.Todos,
				FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue,
				FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue,
				CategoriaGasto = ECategoriaGasto.Expediente,
                TipoAcreedor = new ETipoAcreedor[1] { (ETipoAcreedor)(long)TipoAcreedor_CB.SelectedValue },
			};

			ExpedientInfo expediente_ini = Rango_RB.Checked ? _expediente_ini : null;
			ExpedientInfo expediente_fin = Rango_RB.Checked ? _expediente_fin : null;

			_filter.SoloIncompletos = Incompletos_CkB.Checked;

			ReportFormat format = new ReportFormat();

			if (Agrupado_RB.Checked)
				format.Vista = EReportVista.Agrupado;
			else if (Lista_RB.Checked)
				format.Vista = EReportVista.ListaCompleta;
			else if (Resumido_RB.Checked)
				format.Vista = EReportVista.Resumido;
			
            string filtro = GetFilterValues();
            PgMng.Grow();

			ExpenseList gastos = null;
			ExpedienteList expedientes = null;
			InputDeliveryLineList conceptos = null;
			if (TodosExpediente_RB.Checked)
            {
                if (format.Vista == EReportVista.Resumido)
                    gastos = ExpenseList.GetByFacturaExpedienteList(conditions, null, null);
                else
                    gastos = ExpenseList.GetList(conditions, null, null);
				expedientes = ExpedienteList.GetList(conditions, null, null);
				conceptos = InputDeliveryLineList.GetByExpedienteList(conditions, null, null);
			}
			else if (Seleccion_RB.Checked)
			{
                if (format.Vista == EReportVista.Resumido)
                    gastos = ExpenseList.GetByFacturaExpedienteList(conditions, null, null);
                else
                    gastos = ExpenseList.GetList(conditions, null, null);
                expedientes = ExpedienteList.GetList(conditions, null, null);
				conceptos = InputDeliveryLineList.GetByExpedienteList(conditions, null, null);
			}
			else if (Rango_RB.Checked)
			{
                if (format.Vista == EReportVista.Resumido)
                    gastos = ExpenseList.GetByFacturaExpedienteList(conditions, expediente_ini, expediente_fin);
                else
                    gastos = ExpenseList.GetList(conditions, expediente_ini, expediente_fin);
                expedientes = ExpedienteList.GetList(conditions, expediente_ini, expediente_fin);
				conceptos = InputDeliveryLineList.GetByExpedienteList(conditions, expediente_ini, expediente_fin);
			}

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            ExpenseReportMng reportMng = new ExpenseReportMng(AppContext.ActiveSchema, this.Text, filtro);

			if (format.Vista == EReportVista.Resumido)
			{
				ExpensesReportVerticalRpt rpt = reportMng.GetInformeGastoVerticalListReport(gastos, expedientes, _filter, conceptos);
				PgMng.FillUp();

				ShowReport(rpt);
			}
			else
			{
				ExpensesReportRpt rpt = reportMng.GetInformeGastoListReport(gastos, expedientes, _filter, format);
				PgMng.FillUp();

				ShowReport(rpt);
			}

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Buttons

		private void Expediente_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente = form.Selected as ExpedientInfo;
				Expediente_TB.Text = _expediente.Codigo;
			}
		}

		private void ExpedienteFin_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente_fin = form.Selected as ExpedientInfo;
				ExpedienteFin_TB.Text = _expediente_fin.Codigo;
			}
		}

		private void ExpedienteIni_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_expediente_ini = form.Selected as ExpedientInfo;
				ExpedienteIni_TB.Text = _expediente_ini.Codigo;
			}
		}

		private void TodosExpediente_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = false;
			RangoExp_GB.Enabled = false;
			TipoExpediente_CB.Enabled = true;
		}

		private void Seleccion_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = true;
			RangoExp_GB.Enabled = false;
			TipoExpediente_CB.Enabled = false;
		}

		private void Rango_RB_Click(object sender, EventArgs e)
		{
			SeleccionExp_GB.Enabled = false;
			RangoExp_GB.Enabled = true;
			TipoExpediente_CB.Enabled = false;
		}

        #endregion

        #region Events

        #endregion
    }
}

