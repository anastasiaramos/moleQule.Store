using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Nomina;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class PayrollMngForm : PayrollMngBaseForm
    {
        #region Attributes & Properties

		public const string ID = "PayRollMngForm";
		public static Type Type { get { return typeof(PayrollMngForm); } }
		public override Type EntityType { get { return typeof(Payroll); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public Payroll _entity;

		#endregion
		
		#region Factory Methods

		public PayrollMngForm()
            : this(null) {}
			
		public PayrollMngForm(Form parent)
			: this(false, parent) {}
		
		public PayrollMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) {}

		public PayrollMngForm(bool isModal, Form parent, PayrollList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
			Datos.DataSource = PayrollList.NewList().GetSortedList();			
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
        }
		
		#endregion

		#region Authorization

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
				case molAction.ChangeStateContabilizado:

					if ((AppContext.User != null) && (state))
						base.ActivateAction(action, AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE));
					else
						base.ActivateAction(action, state);

					break;

				default:
					base.ActivateAction(action, state);
					break;
			}
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Descripcion.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Descripcion);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
            if (Tabla == null) return;
			
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.PAGO_GASTO_TI, Properties.Resources.pago);
		}
		
		protected override void SetRowFormat(DataGridViewRow row)
        {
			if (!row.Displayed) return;
            if (row.IsNewRow) return;
            
			NominaInfo item = (NominaInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Payroll");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
					List = PayrollList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Nominas");
		}

        public override void UpdateList()
        {
            switch (_current_action)
            {
                case molAction.Add:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo(false));
                    if (FilterType == IFilterType.Filter)
                    {
						PayrollList listA = PayrollList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Unlock:
				case molAction.ChangeStateContabilizado:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
						PayrollList listD = PayrollList.GetList(_filter_results);
                        listD.RemoveItem(ActiveOID);
                        _filter_results = listD.GetSortedList();
                    }
                    break;
            }

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
        }	

		#endregion

        #region Actions

		public override void OpenViewForm()
		{			
			AddForm(new PayrollBatchViewForm(ActiveItem.OidRemesa, ActiveOID));
		}

        public override void OpenEditForm() 
        {             
			PayrollBatchEditForm form = new PayrollBatchEditForm(ActiveItem.OidRemesa, ActiveOID);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = null;
				ExecuteAction(molAction.Refresh);
			}
		}

		public override void DeleteAction() { OpenEditForm(); }

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
			switch (estado)
			{
				case EEstadoItem.Unlock:
					_entity = Payroll.ChangeEstado(ActiveOID, moleQule.Base.EEstado.Abierto);
					break;

				case EEstadoItem.Contabilizado:
					_entity = Payroll.ChangeEstado(ActiveOID, moleQule.Base.EEstado.Contabilizado);
					break;

				case EEstadoItem.Emitido:
					_entity = Payroll.ChangeEstado(ActiveOID, moleQule.Base.EEstado.Emitido);
					break;
			}

			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { ShowPagoAction(); }

		public void ShowPagoAction()
		{
			if (ActiveItem.EEstado == moleQule.Base.EEstado.Anulado) return;

			if (ActiveItem.OidPago == 0)
			{
                Payment pago = Payment.New(ActiveItem);
				PayrollPaymentAddForm form = new PayrollPaymentAddForm(pago, this);
				form.ShowDialog(this);

				if (form.ActionResult == DialogResult.OK)
				{
					ActiveItem.CopyFrom(form.Entity);
					ExecuteAction(molAction.Refresh);
				}
			}
			else
			{
				PayrollPaymentEditForm form = new PayrollPaymentEditForm(ActiveItem.OidPago, moleQule.Store.Structs.EnumConvert.ToETipoPago(ECategoriaGasto.Nomina), this);
				form.ShowDialog(this);
				ExecuteAction(molAction.Refresh);
			}
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			NominaReportMng reportMng = new NominaReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			NominaListRpt report = reportMng.GetListReport(List);

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion

    }

	public partial class PayrollMngBaseForm : Skin06.EntityMngSkinForm<PayrollList, NominaInfo>
	{
		public PayrollMngBaseForm()
			: this(false, null, null) { }

		public PayrollMngBaseForm(bool isModal, Form parent, PayrollList lista)
			: base(isModal, parent, lista) { }
	}
}
