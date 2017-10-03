using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ToolMngForm : ToolMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ToolMngForm";
		public static Type Type { get { return typeof(ToolMngForm); } }
		public override Type EntityType { get { return typeof(Tool); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Tool _entity;

		#endregion

		#region Factory Methods

		public ToolMngForm()
			: this(null) { }

		public ToolMngForm(Form parent)
			: this(false, parent, null) { }

		public ToolMngForm(bool isModal, Form parent, ToolList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = ToolList.NewList().GetSortedList();
			SortProperty = OName.DataPropertyName;
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			Code.Width = TextRenderer.MeasureText(moleQule.Library.Store.Resources.Defaults.DEFAULT_CODE_FORMAT, Tabla.DefaultCellStyle.Font).Width + 10;

			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			OName.Tag = 0.3;
			Description.Tag = 0.3;
			Comments.Tag = 0.4;

			cols.Add(OName);
			cols.Add(Description);
			cols.Add(Comments);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			/*ToolInfo item = row.DataBoundItem as ToolInfo ;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);*/
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					//ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					//ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Tool");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ToolList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Tools");
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
						ToolList listA = ToolList.GetList(_filter_results);
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
						ToolList listD = ToolList.GetList(_filter_results);
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
			ToolAddForm form = new ToolAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new ToolViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			ToolEditForm form = new ToolEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Tool.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		/*public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
					AddForm(form);
				}
			}
		}*/

		#endregion

		#region Print

		public override void PrintList()
		{
			/*PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ToolReportMng reportMng = new ToolReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ToolListRpt report = reportMng.GetListReport(ToolList.GetList((IList<ToolInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);*/
		}

		#endregion
	}

	public partial class ToolMngBaseForm : Skin06.EntityMngSkinForm<ToolList, ToolInfo>
	{
		public ToolMngBaseForm()
			: this(false, null, null) { }

		public ToolMngBaseForm(bool isModal, Form parent, ToolList lista)
			: base(isModal, parent, lista) { }
	}
}