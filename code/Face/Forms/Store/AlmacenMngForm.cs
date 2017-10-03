using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Store;

namespace moleQule.Face.Store
{
	public partial class AlmacenMngForm : AlmacenMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "AlmacenMngForm";
		public static Type Type { get { return null; } }
		public override Type EntityType { get { return typeof(Almacen); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Almacen _entity;

		#endregion

		#region Factory Methods

		public AlmacenMngForm()
			: this(false) { }

		//Por defecto se abre Alimentacion.
		public AlmacenMngForm(bool isModal)
			: this(isModal, null) { }

		public AlmacenMngForm(Form parent)
			: this(false, parent) { }

		public AlmacenMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public AlmacenMngForm(StoreList list)
			: this(false, null, list) { }

		public AlmacenMngForm(Form parent, StoreList list)
			: this(false, parent, list) { }
		
		public AlmacenMngForm(bool isModal, Form parent, StoreList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = StoreList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;
		}

		#endregion

		#region Business Methods

		protected override Type GetColumnType(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].ValueType : null;
		}

		protected override string GetColumnProperty(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].DataPropertyName : null;
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			Ubicacion.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Ubicacion);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			this.Icon = Properties.Resources.store;
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			//StoreInfo item = (StoreInfo)row.DataBoundItem;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Almacen");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = StoreList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Almacenes");
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
						StoreList listA = StoreList.GetList(_filter_results);
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
						StoreList listD = StoreList.GetList(_filter_results);
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
			AlmacenAddForm form = new AlmacenAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			/*if ((ActiveOID == 1) && (ActiveItem.ETipo == ETipoAlmacen.Almacen))
			{
				AlmacenAlmacenUIForm em = new AlmacenAlmacenUIForm(this);
				em.Show();
			}
			else
			{
				AlmacenViewForm form = new AlmacenViewForm(ActiveOID, this, ActiveItem.ETipo);
				AddForm(form);
			}*/
		}

		public override void OpenEditForm()
		{
			AlmacenEditForm form = new AlmacenEditForm(ActiveItem.Oid, this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void DeleteObject(long oid)
		{
            //if (ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
            //{
				BatchList partidas = BatchList.GetListByAlmacen(oid, false);

				if (partidas.Count > 0)
				{
					PgMng.ShowInfoException("El almacén tiene productos asociados");

					_action_result = DialogResult.Ignore;
					return;
				}

				Almacen.Delete(oid);
				_action_result = DialogResult.OK;
			//}
		}

		public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch
			{
				AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
		}

		#endregion
	}

	public partial class AlmacenMngBaseForm : Skin06.EntityMngSkinForm<StoreList, StoreInfo>
	{
		public AlmacenMngBaseForm()
			: this(false, null, null) { }

		public AlmacenMngBaseForm(bool isModal, Form parent, StoreList lista)
			: base(isModal, parent, lista) { }
	}
}
