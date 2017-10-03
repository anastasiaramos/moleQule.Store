using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class SerieMngForm : SerieMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "SerieMngForm";
		public static Type Type { get { return typeof(SerieMngForm); } }
		public override Type EntityType { get { return typeof(moleQule.Serie.Serie); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Serie.Serie _entity;

		#endregion

		#region Factory Methods

		public SerieMngForm()
			: this(null) { }

		public SerieMngForm(Form parent)
			: this(false, parent) { }

		public SerieMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public SerieMngForm(bool isModal, Form parent, SerieList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = SerieList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 0.5;
			Cabecera.Tag = 0.5;

			cols.Add(Observaciones);
			cols.Add(Cabecera);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Serie");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = SerieList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Series");
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
						SerieList lista = SerieList.GetList(_filter_results);
						lista.AddItem(_entity.GetInfo(false));
						_filter_results = lista.GetSortedList();
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
						SerieList listD = SerieList.GetList(_filter_results);
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
			SerieAddForm form = new SerieAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new SerieViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			SerieEditForm form = new SerieEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
            moleQule.Serie.Serie.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList()
		{
			/*SerieReportMng reportMng = new SerieReportMng(AppContext.ActiveSchema);
			
			SerieListRpt report = reportMng.GetListReport(list);
			
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

	public partial class SerieMngBaseForm : Skin06.EntityMngSkinForm<SerieList, SerieInfo>
	{
		public SerieMngBaseForm()
			: this(false, null, null) { }

		public SerieMngBaseForm(bool isModal, Form parent, SerieList lista)
			: base(isModal, parent, lista) { }
	}
}
