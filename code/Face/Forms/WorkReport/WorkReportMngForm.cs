using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.WorkReport;

namespace moleQule.Face.Store
{
	public partial class WorkReportMngForm : WorkReportMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "WorkReportMngForm";
		public static Type Type { get { return typeof(WorkReportMngForm); } }
		public override Type EntityType { get { return typeof(WorkReport); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected WorkReport _entity;

		#endregion

		#region Factory Methods

		public WorkReportMngForm()
			: this(null) { }

		public WorkReportMngForm(Form parent)
			: this(false, parent) { }

		public WorkReportMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public WorkReportMngForm(bool isModal, Form parent, WorkReportList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();
			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = WorkReportList.NewList().GetSortedList();
			SortProperty = From.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			CategoryName.Tag = 0.3;
			Comments.Tag = 0.7;

			cols.Add(CategoryName);
			cols.Add(Comments);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			//SetActionStyle(molAction.CustomAction1, Resources.Labels.exp.eE.PROVEEDOR_TI, Properties.Resources.proveedor);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			WorkReportInfo item = row.DataBoundItem as WorkReportInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EStatus);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.CustomAction1);

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.Copy);
					ShowAction(molAction.Edit);
  					ShowAction(molAction.PrintDetail);
					//ShowAction(molAction.CustomAction1);

					Seleccionar.Visible = false;
					Seleccionar.ReadOnly = false;

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "WorkReport");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
						List = WorkReportList.GetList(moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = WorkReportList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de WorkReports");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					if (List.GetItem(_entity.Oid) != null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						WorkReportList listA = WorkReportList.GetList(_filter_results);
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
						WorkReportList listD = WorkReportList.GetList(_filter_results);
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

		public override void OpenAddForm()
		{
			WorkReportAddForm form = new WorkReportAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new WorkReportViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			if (ActiveItem.EStatus == moleQule.Base.EEstado.Closed)
			{
				PgMng.ShowInfoException("No es posible modificar un parte de trabajo cerrado.");

				_action_result = DialogResult.Ignore;
				return;
			}

			WorkReportEditForm form = new WorkReportEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			WorkReport.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void CopyObjectAction(long oid)
		{
			WorkReportAddForm form = new WorkReportAddForm(WorkReport.CloneAsNew(ActiveItem), this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void CustomAction1() { }

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(2, 1, Face.Resources.Messages.LOADING_DATA, this);

			WorkReportReportMng rptMng = new WorkReportReportMng(AppContext.ActiveSchema, Text, FilterValues);

			ReportClass report = rptMng.GetListReport(WorkReportList.GetList(Datos.DataSource as IList<WorkReportInfo>));
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			WorkReportReportMng reportMng = new WorkReportReportMng(AppContext.ActiveSchema, Text, FilterValues);
			ReportClass report = reportMng.GetDetailReport(WorkReportInfo.Get(ActiveOID, true));

            ShowReport(report);
		}

		#endregion
	}

	public partial class WorkReportMngBaseForm : Skin06.EntityMngSkinForm<WorkReportList, WorkReportInfo>
	{
		public WorkReportMngBaseForm()
			: this(false, null, null) { }

		public WorkReportMngBaseForm(bool isModal, Form parent, WorkReportList lista)
			: base(isModal, parent, lista) { }
	}
}
