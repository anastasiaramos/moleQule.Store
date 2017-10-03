using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.CslaEx;

using moleQule;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Producto;

namespace moleQule.Face.Store
{
	public partial class EscandalloMngForm : EscandalloMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "EscandalloMngForm";
		public static Type Type { get { return typeof(ProductMngForm); } }
		public override Type EntityType { get { return typeof(Product); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Product _entity;

		#endregion

		#region Factory Methods

		public EscandalloMngForm()
			: this(null) { }

		public EscandalloMngForm(Form parent)
			: this(false, parent) { }

		public EscandalloMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public EscandalloMngForm(bool isModal, Form parent, ProductList lista)
			: base(isModal, parent, lista) 
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ProductList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;
			SortDirection = ListSortDirection.Ascending;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.2;
			Observaciones.Tag = 0.8;

			cols.Add(Nombre);
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
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			ProductInfo item = row.DataBoundItem as ProductInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					break;

				case molView.Normal:

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Producto");

			// Guardamos la configuración actual del listado
			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ProductList.GetElaboradosList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Productoes");
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
						ProductList listA = ProductList.GetList(_filter_results);
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
						ProductList listD = ProductList.GetList(_filter_results);
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
			EscandalloAddForm form = new EscandalloAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new EscandalloViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			EscandalloEditForm form = new EscandalloEditForm(ActiveItem.Oid, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Product.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			ProductReportMng reportMng = new ProductReportMng(AppContext.ActiveSchema);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ProductoListRpt report = reportMng.GetListReport(ProductList.GetList((IList<ProductInfo>)Datos.List));

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}

	public partial class EscandalloMngBaseForm : Skin06.EntityMngSkinForm<ProductList, ProductInfo>
	{
		public EscandalloMngBaseForm()
			: this(false, null, null) { }

		public EscandalloMngBaseForm(bool isModal, Form parent, ProductList lista)
			: base(isModal, parent, lista) { }
	}
}

