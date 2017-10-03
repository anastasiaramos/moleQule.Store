using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
//using moleQule.Library.Store.Reports.PedidoProveedor;

namespace moleQule.Face.Store
{
	public partial class PedidoProveedorMngForm : PedidoProveedorMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "PedidoProveedorMngForm";
		public static Type Type { get { return typeof(PedidoProveedorMngForm); } }
		public override Type EntityType { get { return typeof(PedidoProveedor); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected PedidoProveedor _entity;
		protected moleQule.Base.EEstado _estado = moleQule.Base.EEstado.Todos;

		#endregion

		#region Factory Methods

		public PedidoProveedorMngForm()
			: this(null) { }

		public PedidoProveedorMngForm(Form parent)
			: this(false, parent, moleQule.Base.EEstado.Todos) { }

		public PedidoProveedorMngForm(bool isModal, Form parent, moleQule.Base.EEstado estado)
			: this(isModal, parent, estado, null) { }

		public PedidoProveedorMngForm(bool isModal, Form parent, moleQule.Base.EEstado estado, PedidoProveedorList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = PedidoProveedorList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_estado = estado;
		}

		#endregion

		#region Layout

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.Refresh);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateEmitido);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.Refresh);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);
					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), Face.ControlTools.Instance.HeaderSelectedStyle);

			Fields_CB.Text = Fecha.HeaderText;
		}

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Acreedor.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Acreedor);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

			PedidoProveedorInfo item = row.DataBoundItem as PedidoProveedorInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "PedidoProveedor");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = PedidoProveedorList.GetList(ETipoAcreedor.Todos, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Pedidos");
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
						PedidoProveedorList listA = PedidoProveedorList.GetList(_filter_results);
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
						PedidoProveedorList listD = PedidoProveedorList.GetList(_filter_results);
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
			PedidoProveedorAddForm form = new PedidoProveedorAddForm(ETipoAcreedor.Proveedor, this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new PedidoProveedorViewForm(ActiveItem.Oid, ActiveItem.ETipoAcreedor, this));
		}

		public override void OpenEditForm()
		{
			PedidoProveedorEditForm form = new PedidoProveedorEditForm(ActiveItem.Oid, ActiveItem.ETipoAcreedor, this);
			if (form.EntityInfo != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			PedidoProveedor.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			/*PedidoProveedorReportMng reportMng = new PedidoProveedorReportMng(AppContext.ActiveSchema);
			
			PedidoProveedorListRpt report = reportMng.GetListReport(List);
			
			ShowReport(report);
			*/
		}

		#endregion
	}

	public partial class PedidoProveedorMngBaseForm : Skin06.EntityMngSkinForm<PedidoProveedorList, PedidoProveedorInfo>
	{
		public PedidoProveedorMngBaseForm()
			: this(false, null, null) { }

		public PedidoProveedorMngBaseForm(bool isModal, Form parent, PedidoProveedorList lista)
			: base(isModal, parent, lista) { }
	}

}
