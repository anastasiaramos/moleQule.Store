using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expense;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ExpenseMngForm : ExpenseMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "GastoMngForm";
		public static Type Type { get { return typeof(ExpenseMngForm); } }
        public override Type EntityType { get { return typeof(Expense); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Expense _entity;
		protected ECategoriaGasto _categoria;

		#endregion

		#region Factory Methods

		public ExpenseMngForm()
			: this(null, ECategoriaGasto.Todos) {}

		public ExpenseMngForm(ECategoriaGasto categoria)
			: this(null, categoria) { }

		public ExpenseMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, ECategoriaGasto.Todos) { }

		public ExpenseMngForm(Form parent, ECategoriaGasto categoria)
			: this(false, parent, categoria) { }

		public ExpenseMngForm(bool is_modal, Form parent, ECategoriaGasto categoria)
			: this(is_modal, parent, categoria, null) { }

		public ExpenseMngForm(bool is_modal, Form parent, ECategoriaGasto categoria, ExpenseList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = ExpenseList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_categoria = categoria;

			Text = String.Format(Resources.Labels.GASTOS_TITLE, moleQule.Store.Structs.EnumText<ECategoriaGasto>.GetLabel(_categoria));			
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
			Descripcion.Tag = 0.5;
			Observaciones.Tag = 0.5;

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
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			ExpenseInfo item = (ExpenseInfo)row.DataBoundItem;

			if (!(new List<moleQule.Base.EEstado>() { moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Exportado }).Contains(item.EEstado))
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstadoPago);
			else
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			row.Cells[FechaPago.Index].Style = (item.FechaPago == DateTime.MinValue) ? Common.ControlTools.Instance.TransparentStyle  : row.DefaultCellStyle;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.Copy);
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
			PgMng.Grow(string.Empty, "Gasto");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
						List = ExpenseList.GetList(_categoria, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = ExpenseList.GetList(_categoria, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Gastos");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						ExpenseList listA = ExpenseList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						//_filter_results.Clear();
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.ChangeStateContabilizado:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						ExpenseList listD = ExpenseList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						//_filter_results.Clear();
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

		public override void OpenAddForm()
		{
			ExpenseAddForm form = new ExpenseAddForm(this, _categoria);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new ExpenseViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			try
			{
				EntityBase.CheckEditLockedEstado(ActiveItem.EEstado, moleQule.Base.EEstado.Contabilizado);
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
				_action_result = DialogResult.Ignore;
				return;
			}

			if ((ActiveItem.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) ||
				(ActiveItem.ECategoriaGasto == ECategoriaGasto.OtrosExpediente) ||
				(ActiveItem.ECategoriaGasto == ECategoriaGasto.Stock))
			{
				PgMng.ShowInfoException("No es posible editar un gasto asociado a un expediente");

				_action_result = DialogResult.Ignore;
				return;
			}

			ExpenseEditForm form = new ExpenseEditForm(ActiveItem.Oid, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
            Expense.Delete(ActiveOID, moleQule.Store.Structs.EnumConvert.ToETipoPago(ActiveItem.ECategoriaGasto));
			_action_result = DialogResult.OK;
		}

		public override void CopyObjectAction(long oid)
		{
            ExpenseAddForm form = new ExpenseAddForm(Expense.CloneAsNew(ActiveItem), this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
			if ((ActiveItem.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) ||
				(ActiveItem.ECategoriaGasto == ECategoriaGasto.OtrosExpediente) ||
				(ActiveItem.ECategoriaGasto == ECategoriaGasto.Stock))
			{
				PgMng.ShowInfoException(Resources.Messages.GASTO_CON_EXPEDIENTE);
				_action_result = DialogResult.Ignore;
				return;
			}

			if (ActiveItem.OidPago != 0)
			{
				PgMng.ShowInfoException(Resources.Messages.GASTO_CON_PAGO);				
				_action_result = DialogResult.Cancel;
				return;
			}

            _entity = Expense.ChangeEstado(ActiveOID, ActiveItem.ECategoriaGasto, Base.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { GotoPagoAction(); }

		public void GotoPagoAction()
		{
			if (ActiveItem.EEstado == moleQule.Base.EEstado.Anulado) return;
			if (ActiveItem.Total == 0) return;

			if (ActiveItem.OidPago == 0)
			{
                //Se obtiene el registro desde la DB para que actualice el pendiente de pago
                //en el caso de que se haya creado justo antes de esta acción
                Payment pago = Payment.New(ExpenseInfo.Get(ActiveItem.Oid, false), moleQule.Store.Structs.EnumConvert.ToETipoPago(ActiveItem.ECategoriaGasto));
				ExpensePaymentAddForm form = new ExpensePaymentAddForm(pago, this);
				form.ShowDialog(this);

				if (form.ActionResult == DialogResult.OK)
				{
					ActiveItem.CopyFrom(form.Entity);
				}
			}
			else
			{
				ExpensePaymentEditForm form = new ExpensePaymentEditForm(ActiveItem.OidPago, moleQule.Store.Structs.EnumConvert.ToETipoPago(ActiveItem.ECategoriaGasto), this);
				form.ShowDialog(this);
			}
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpenseReportMng reportMng = new ExpenseReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpenseListRpt report = reportMng.GetListReport(ExpenseList.GetList((IList<ExpenseInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public partial class ExpenseMngBaseForm : Skin06.EntityMngSkinForm<ExpenseList, ExpenseInfo>
	{
		public ExpenseMngBaseForm()
			: this(false, null, null) { }

		public ExpenseMngBaseForm(bool isModal, Form parent, ExpenseList lista)
			: base(isModal, parent, lista) { }
	}
}