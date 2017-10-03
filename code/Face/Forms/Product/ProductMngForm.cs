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
using moleQule.Library.Store.Reports.Producto;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ProductMngForm : ProductMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ProductMngForm";
		public static Type Type { get { return typeof(ProductMngForm); } }
		public override Type EntityType { get { return typeof(Product); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Product _entity;
		protected ETipoProducto _tipo;

		#endregion

		#region Factory Methods

		public ProductMngForm()
			: this(null, ETipoProducto.Todos) { }

		public ProductMngForm(Form parent, ETipoProducto tipo)
			: this(false, parent, null, tipo) { }

		public ProductMngForm(bool isModal, Form parent, ProductList list, ETipoProducto tipo)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = ProductList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			_tipo = tipo;
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			Codigo.Width = TextRenderer.MeasureText(moleQule.Library.Store.Resources.Defaults.PRODUCTO_CODE_FORMAT, Tabla.DefaultCellStyle.Font).Width + 10;

			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			Descripcion.Tag = 0.3;
			Observaciones.Tag = 0.4;

			cols.Add(Observaciones);
			cols.Add(Nombre);
			cols.Add(Descripcion);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			//IDE Compatibility
			try
			{
				string pattern = "{0} ({1})";
				PrecioCompra.HeaderText = string.Format(pattern, PrecioCompra.HeaderText, AppControllerBase.Culture.NumberFormat.CurrencySymbol);
				PrecioVenta.HeaderText = string.Format(pattern, PrecioVenta.HeaderText, AppControllerBase.Culture.NumberFormat.CurrencySymbol);
				AyudaKilo.HeaderText = string.Format(pattern, AyudaKilo.HeaderText, AppControllerBase.Culture.NumberFormat.CurrencySymbol);

				int decimal_places = Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
				PrecioCompra.DefaultCellStyle.Format = "N" + decimal_places;
				PrecioVenta.DefaultCellStyle.Format = "N" + decimal_places;
				AyudaKilo.DefaultCellStyle.Format = "N" + decimal_places;
			}
			catch { }
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			/*ProductInfo item = row.DataBoundItem as ProductInfo ;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);*/
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					AyudaKilo.Visible = false;

					ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		public void ShowCodigoProveedor(bool show)
		{
			CodigoArticuloAcreedor.Visible = show;
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Producto");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoProducto.Todos:
							List = ProductList.GetList(false);
							break;

						case ETipoProducto.Kit:
							List = ProductList.GetKitList(true, false);
							break;
					}
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Productos");
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
			ProductAddForm form = new ProductAddForm(this);
            if (_tipo == ETipoProducto.Kit) form.Entity.IsKit = true;
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new ProductViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			ProductEditForm form = new ProductEditForm(ActiveOID, this);
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

		public override void ShowDocumentsAction()
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
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ProductReportMng reportMng = new ProductReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ProductoListRpt report = reportMng.GetListReport(ProductList.GetList((IList<ProductInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public class ProductAllMngForm : ProductMngForm
	{
		public new const string ID = "ProductAllMngForm";
		public new static Type Type { get { return typeof(ProductAllMngForm); } }

		public ProductAllMngForm(Form parent)
			: base(parent, ETipoProducto.Todos)
		{
			this.Text = Resources.Labels.PRODUCTO;
		}
	}

	public class ProductKitMngForm : ProductMngForm
	{
		public new const string ID = "ProductKitMngForm";
		public new static Type Type { get { return typeof(ProductKitMngForm); } }

		public ProductKitMngForm(Form parent)
			: base(parent, ETipoProducto.Kit)
		{
			this.Text = Resources.Labels.KITS;
		}
	}

	public partial class ProductMngBaseForm : Skin06.EntityMngSkinForm<ProductList, ProductInfo>
	{
		public ProductMngBaseForm()
			: this(false, null, null) { }

		public ProductMngBaseForm(bool isModal, Form parent, ProductList lista)
			: base(isModal, parent, lista) { }
	}
}