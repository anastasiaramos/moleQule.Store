using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class FamilyMngForm : FamilyMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "FamilyMngForm";
		public static Type Type { get { return typeof(PedidoProveedorMngForm); } }
		public override Type EntityType { get { return typeof(Familia); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Familia _entity;

		protected ETipoAlbaranes _tipo;
		protected ETipoAcreedor _tipo_acreedor;
		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public FamilyMngForm()
			: this(null) { }

		public FamilyMngForm(Form parent)
			: this(false, parent, null) { }

		public FamilyMngForm(bool isModal, Form parent, FamiliaList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = FamiliaList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;
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
			PgMng.Grow(string.Empty, "Familia");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = FamiliaList.GetList(false);
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Familias");
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
						FamiliaList listA = FamiliaList.GetList(_filter_results);
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
						FamiliaList listD = FamiliaList.GetList(_filter_results);
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
			FamiliaAddForm form = new FamiliaAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new FamiliaViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			FamiliaEditForm form = new FamiliaEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		/// <summary>
		/// Abre el formulario para borrar item
		/// <returns>void</returns>
		/// </summary>
		public override void DeleteAction()
		{
			Familia.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList()
		{
			/*FamiliaReportMng reportMng = new FamiliaReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			
			FamiliaListRpt report = reportMng.GetListReport(list);
			
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
								moleQule.Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}*/
		}

		#endregion
	}

	public partial class FamilyMngBaseForm : Skin06.EntityMngSkinForm<FamiliaList, FamiliaInfo>
	{
		public FamilyMngBaseForm()
			: this(false, null, null) { }

		public FamilyMngBaseForm(bool isModal, Form parent, FamiliaList lista)
			: base(isModal, parent, lista) { }
	}
}
