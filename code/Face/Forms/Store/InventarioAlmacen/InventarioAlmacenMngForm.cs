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
using moleQule.Library.Store.Reports.Store;

namespace moleQule.Face.Store
{
	public partial class InventarioAlmacenMngForm : Skin06.EntityMngSkinForm<InventarioAlmacenList, InventarioAlmacenInfo>
	{
		#region Attributes & Properties

		public const string ID = "InventarioAlmacenMngForm";
		public static Type Type { get { return null; } }
		public override Type EntityType { get { return typeof(InventarioAlmacen); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected InventarioAlmacen _entity;

		#endregion

		#region Factory Methods

		public InventarioAlmacenMngForm()
			: this(null) { }

		public InventarioAlmacenMngForm(Form parent)
			: this(true, parent, null) { }
	
		public InventarioAlmacenMngForm(bool isModal, Form parent, InventarioAlmacenList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = InventarioAlmacenList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			Nombre.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Nombre);
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

			//InventarioAlmacenInfo item = (InventarioAlmacenInfo)row.DataBoundItem;
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "InventarioAlmacen");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = InventarioAlmacenList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "InventarioAlmacenes");
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
						InventarioAlmacenList listA = InventarioAlmacenList.GetList(_filter_results);
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
						InventarioAlmacenList listD = InventarioAlmacenList.GetList(_filter_results);
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
			InventarioAlmacenAddForm form = new InventarioAlmacenAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			/*if ((ActiveOID == 1) && (ActiveItem.ETipo == ETipoInventarioAlmacen.InventarioAlmacen))
			{
				InventarioAlmacenInventarioAlmacenUIForm em = new InventarioAlmacenInventarioAlmacenUIForm(this);
				em.Show();
			}
			else
			{
				InventarioAlmacenViewForm form = new InventarioAlmacenViewForm(ActiveOID, this, ActiveItem.ETipo);
				AddForm(form);
			}*/
		}

		public override void OpenEditForm()
		{
			InventarioAlmacenEditForm form = new InventarioAlmacenEditForm(ActiveOID, this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void DeleteObject(long oid)
		{
			if (ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				InventarioAlmacen.Delete(oid);
				_action_result = DialogResult.OK;
			}
		}

		#endregion
	}
}
