using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Payment;

namespace moleQule.Face.Store
{
	public partial class ProviderPaymentMngForm : ProviderPaymentMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ProviderPaymentMngForm";
		public static Type Type { get { return typeof(ProviderPaymentMngForm); } }
		public override Type EntityType { get { return typeof(PaymentSummary); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected PaymentSummary _entity;

		#endregion

		#region Factory Methods

		public ProviderPaymentMngForm()
			: this(false) { }

		public ProviderPaymentMngForm(bool isModal)
			: this(isModal, null, null) {}

		public ProviderPaymentMngForm(Form parent)
			: this(false, parent, null) { }

		public ProviderPaymentMngForm(bool isModal, Form parent, PaymentSummaryList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = PaymentSummaryList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			this.Text = Resources.Labels.PAGOS + ": Agrupados por Acreedor";
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Nombre);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.PROVEEDOR_TI, Properties.Resources.proveedor);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			PaymentSummary item = row.DataBoundItem as PaymentSummary;

			if (item == null) return;

			if (item.Pendiente > 0)
				row.Cells[Pendiente.Index].Style = Common.ControlTools.Instance.PendienteStyleC;
			else
				row.Cells[Pendiente.Index].Style = Common.ControlTools.Instance.CobradoStyle;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.View);
					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:
					
					HideAction(molAction.View);
					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Pago");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = PaymentSummaryList.GetProvidersList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Pagos");
		}

		public override void UpdateList()
		{
			RefreshMainData();
			RefreshSources();
		}

		#endregion

		#region Actions

		public override void OpenEditForm()
		{
            switch (ActiveItem.ETipoAcreedor)
            {
                case ETipoAcreedor.Empleado:
                    {
                        EmployeePaymentEditForm form = new EmployeePaymentEditForm(this, ActiveItem.OidAgente, ActiveItem);
                        if (form.Entity != null)
                            AddForm(form);
                    }
                    break;

                default:
                    {
                        PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidAgente, ActiveItem);
                        if (form.Entity != null)
                            AddForm(form);
                    }
                    break;
            }
		}

		public override void CustomAction1() { ShowProveedorAction(); }

		public virtual void ShowProveedorAction()
		{
			ProviderMngForm.OpenEditFormAction(ActiveItem.ETipoAcreedor, ActiveItem.OidAgente, this);
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			PagoAcreedorListRpt report = reportMng.GetPagoAcreedorListReport((IList<PaymentSummary>)Datos.List);

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public partial class ProviderPaymentMngBaseForm : Skin06.EntityMngSkinForm<PaymentSummaryList, PaymentSummary>
    {
        public ProviderPaymentMngBaseForm()
            : this(false, null, null) { }

		public ProviderPaymentMngBaseForm(bool isModal, Form parent, PaymentSummaryList list)
            : base(isModal, parent, list) { }
    }
}