using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class BatchMngForm : BatchMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "BatchMngForm";
		public static Type Type { get { return typeof(BatchMngForm); } }
        public override Type EntityType { get { return typeof(Batch); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Batch _entity;
		protected SerieInfo _serie = null;

		#endregion

		#region Factory Methods

		public BatchMngForm()
			: this(null) { }

		public BatchMngForm(Form parent)
			: this(false, parent, null) { }

		public BatchMngForm(Form parent, BatchList lista, string title)
			: this(false, parent, null, lista, title) { }

		public BatchMngForm(bool isModal, Form parent, BatchList lista)
			: this(isModal, parent, null, lista) {}
			
		public BatchMngForm(bool isModal, Form parent, SerieInfo serie, BatchList lista)
			: this(isModal, parent, null, lista, string.Empty) {}

		public BatchMngForm(bool isModal, Form parent, SerieInfo serie, BatchList lista, string title)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = BatchList.NewList().GetSortedList();
			SortProperty = TipoMercancia.DataPropertyName;

			_serie = serie;

			if (title != string.Empty) Text = title;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			TipoMercancia.Tag = 1;

			cols.Add(TipoMercancia);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
					
					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.View);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.View);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Partida");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
						List = BatchList.GetList(moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = BatchList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Partidas");
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
						BatchList listA = BatchList.GetList(_filter_results);
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
						BatchList listD = BatchList.GetList(_filter_results);
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
			AddForm(new ProductAddForm(this));
		}

		public override void OpenViewForm()
		{
			AddForm(new ProductViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			ProductEditForm form = new ProductEditForm(ActiveItem.OidProducto, this);
			if (form.Entity != null)
			{
				AddForm(form);
			}
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			PartidaListRpt report = reportMng.GetPartidaListReport(BatchList.GetList(Datos.DataSource as IList<BatchInfo>));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public class PartidaAlimentacionMngForm : BatchMngForm
	{
		public new const string ID = "PartidaAlimentacionMngForm";
		public new static Type Type { get { return typeof(PartidaAlimentacionMngForm); } }

		public PartidaAlimentacionMngForm(Form parent, BatchList list, string title)
			: this(false, parent, list, title) { }

		public PartidaAlimentacionMngForm(bool isModal, Form parent, BatchList list, string title)
			: base(isModal, parent, SerieInfo.Get((long)ESerie.ALIMENTACION, false), list, title) { }
	}

	public class PartidaGanadoMngForm : BatchMngForm
	{
		public new const string ID = "PartidaGanadoMngForm";
		public new static Type Type { get { return typeof(PartidaGanadoMngForm); } }

		public PartidaGanadoMngForm(Form parent, BatchList lista, string title)
			: this(false, parent, lista, title) { }

		public PartidaGanadoMngForm(bool isModal, Form parent, BatchList lista, string title)
			: base(isModal, parent, SerieInfo.Get((long)ESerie.GANADO, false), lista, title) { }
	}

	public class PartidaMaquinariaMngForm : BatchMngForm
	{
		public new const string ID = "PartidaMaquinariaMngForm";

		public new static Type Type { get { return typeof(PartidaMaquinariaMngForm); } }

		public PartidaMaquinariaMngForm(Form parent, BatchList lista, string title)
			: this(false, parent, lista, title) { }

		public PartidaMaquinariaMngForm(bool isModal, Form parent, BatchList lista, string title)
			: base(isModal, parent, SerieInfo.Get((long)ESerie.MAQUINARIA, false), lista, title) { }
	}

    public partial class BatchMngBaseForm : Skin06.EntityMngSkinForm<BatchList, BatchInfo>
    {
        public BatchMngBaseForm()
            : this(false, null, null) { }

        public BatchMngBaseForm(bool isModal, Form parent, BatchList lista)
            : base(isModal, parent, lista) { }
    }
}