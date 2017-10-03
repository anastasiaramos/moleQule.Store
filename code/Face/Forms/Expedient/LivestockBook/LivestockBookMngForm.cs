using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports;

namespace moleQule.Face.Store
{
    public partial class LivestockBookMngForm : Skin06.EntityMngSkinForm<LivestockBookList, LivestockBookInfo>
    {
        #region Attributes & Properties

        public const string ID = "LivestockBookMngForm";
		public static Type Type { get { return typeof(LivestockBookMngForm); } }
        public override Type EntityType { get { return typeof(LivestockBook); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        public LivestockBook _entity;

		#endregion
		
		#region Factory Methods

		public LivestockBookMngForm()
            : this(null) {}
			
		public LivestockBookMngForm(Form parent)
			: this(false, parent) {}
		
		public LivestockBookMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) {}

        public LivestockBookMngForm(bool isModal, Form parent, LivestockBookList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
            Datos.DataSource = LivestockBookList.NewList().GetSortedList();
			
			SortProperty = Codigo.DataPropertyName;
        }
		
		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			Observaciones.Tag = 0.7;

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
            if (row.IsNewRow) return;

            LivestockBookInfo item = (LivestockBookInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.PrintDetail);

					break;

				case molView.Normal:

					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.PrintDetail);

					break;
			}
		}
		
		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "LibroGanadero");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = LivestockBookList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de LibroGanaderos");
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
                        LivestockBookList listA = LivestockBookList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Unlock:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        LivestockBookList listD = LivestockBookList.GetList(_filter_results);
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

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList() 
		{
			/*LibroGanaderoReportMng reportMng = new LibroGanaderoReportMng(AppContext.ActiveSchema);
			
			LibroGanaderoListRpt report = reportMng.GetListReport(List);
			
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
}
