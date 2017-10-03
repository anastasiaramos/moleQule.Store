using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;

namespace moleQule.Face.Store
{
	public partial class ExpedienteREAMngForm : ExpedienteREABaseMngForm
	{
		#region Attributes & Properties

		public const string ID = "ExpedienteREAMngForm";
		public static Type Type { get { return null; } }
        public override Type EntityType { get { return typeof(REAExpedient); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected REAExpedient _entity;

		#endregion

		#region Factory Methods

		public ExpedienteREAMngForm()
			: this(false) { }

		public ExpedienteREAMngForm(bool isModal)
			: this(isModal, null) { }

		public ExpedienteREAMngForm(Form parent)
			: this(false, parent) { }

		public ExpedienteREAMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public ExpedienteREAMngForm(ExpedienteREAList list)
			: this(false, null, list) { }

		public ExpedienteREAMngForm(bool isModal, Form parent, ExpedienteREAList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ExpedienteREAList.NewList().GetSortedList();
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
			Observaciones.Tag = 1;

			cols.Add(Observaciones);
			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

			ExpedienteREAInfo item = (ExpedienteREAInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			row.Cells[FechaCobro.Index].Style = (item.FechaCobro != DateTime.MinValue) ? BasicStyle : HideStyle;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					ShowAction(molAction.Print);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);

					break;

				case molView.Normal:
					
					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					ShowAction(molAction.Print);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Expedient");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ExpedienteREAList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Expedients");
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
						ExpedienteREAList listA = ExpedienteREAList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						ExpedienteREAList listD = ExpedienteREAList.GetList(_filter_results);
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
			ContenedorViewForm form = new ContenedorViewForm(ActiveItem.OidExpediente, this);
			AddForm(form);
		}

		public override void OpenEditForm()
		{
			ContenedorEditForm form = new ContenedorEditForm(ActiveItem.OidExpediente, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity.ExpedientesREA.GetItem(ActiveItem.Oid);
			}
		}

        public override void PrintList()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            ExpedienteREAListRpt report = reportMng.GetExpedienteREAListReport(ExpedienteREAList.GetList((IList<ExpedienteREAInfo>)Datos.List));

            PgMng.FillUp();

            ShowReport(report);
        }

		#endregion
	}

    public partial class ExpedienteREABaseMngForm : Skin06.EntityMngSkinForm<ExpedienteREAList, ExpedienteREAInfo>
    {
        public ExpedienteREABaseMngForm()
            : this(false, null, null) { }

        public ExpedienteREABaseMngForm(bool isModal, Form parent, ExpedienteREAList list)
            : base(isModal, parent, list) { }
    }
}
