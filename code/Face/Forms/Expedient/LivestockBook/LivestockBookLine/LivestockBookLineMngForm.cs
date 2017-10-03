using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Base;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class LivestockBookLineMngForm : LivestockBookLineMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "LivestockBookLineMngForm";
		public static Type Type { get { return null; } }
        public override Type EntityType { get { return typeof(LivestockBookLine); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected LivestockBookLine _entity;
		protected long _book_oid;

		#endregion

		#region Factory Methods

		public LivestockBookLineMngForm()
			: this(null, 1) { }

		public LivestockBookLineMngForm(Form parent, long oidLibro)
			: this(false, parent, oidLibro) { }

		public LivestockBookLineMngForm(bool isModal, Form parent, long oidLibro)
			: this(isModal, parent, null, oidLibro) { }

        public LivestockBookLineMngForm(bool isModal, Form parent, LivestockBookLineList list, long oidBook)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
            Datos.DataSource = LivestockBookLineList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_book_oid = oidBook;
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

            SetActionStyle(molAction.CustomAction1, Resources.Labels.EXTERNAL_HEAD, Properties.Resources.external_head.ToBitmap());
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

            LivestockBookLineInfo item = (LivestockBookLineInfo)row.DataBoundItem;

			SetRowColor(row, item.EEstado);

            if (!item.Explotacion)     
                row.DefaultCellStyle = Face.Common.ControlTools.Instance.PendienteStyleA;
		}

		public void SetRowColor(DataGridViewRow row, moleQule.Base.EEstado estado)
		{
			switch (estado)
			{
				case moleQule.Base.EEstado.Alta:
					row.DefaultCellStyle = Face.Common.ControlTools.Instance.AbiertoStyle;
					break;

				case moleQule.Base.EEstado.Baja:
					row.DefaultCellStyle = Face.Common.ControlTools.Instance.CerradoStyle;
					break;

				case moleQule.Base.EEstado.Pendiente:
					row.DefaultCellStyle = Face.Common.ControlTools.Instance.ExportadoStyle;
					break;

				case moleQule.Base.EEstado.Anulado:
					row.DefaultCellStyle = Face.Common.ControlTools.Instance.AnuladoStyle;
					break;
			}
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
                    ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Expedient");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
                    List = LivestockBookLineList.GetList(_book_oid, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Expedients");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					List.NewItem(_entity.GetInfo(false));
                    List.UpdateBalance();
					if (FilterType == IFilterType.Filter)
					{
                        LivestockBookLineList listA = LivestockBookLineList.GetList(_filter_results);
						listA.NewItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
                case molAction.CustomAction1:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					List.UpdateBalance();
					if (FilterType == IFilterType.Filter)
					{
                        LivestockBookLineList list = LivestockBookLineList.GetList(_filter_results);
						list.UpdateBalance();
						_filter_results = list.GetSortedList();
					}
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
                    List.UpdateBalance();
					if (FilterType == IFilterType.Filter)
					{
                        LivestockBookLineList listD = LivestockBookLineList.GetList(_filter_results);
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
			LivestockBookLineAddForm form = new LivestockBookLineAddForm(this);
			AddForm(form);
            if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenEditForm()
		{
			LivestockBookLineEditForm form = new LivestockBookLineEditForm(ActiveItem.Oid, ActiveItem.ETipo, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void OpenViewForm()
		{
			LivestockBookLineViewForm form = new LivestockBookLineViewForm(ActiveItem.Oid, this);
			AddForm(form);
		}

		public override void DeleteAction()
		{
            ETipoLineaLibroGanadero[] line_Types = new ETipoLineaLibroGanadero[] { 
                                                    ETipoLineaLibroGanadero.Nacimiento,
                                                    ETipoLineaLibroGanadero.Muerte,
                                                    ETipoLineaLibroGanadero.TraspasoExplotacion
                                                };

            if (!line_Types.Contains(ActiveItem.ETipo))
            {
                PgMng.ShowInfoException(Library.Store.Resources.Messages.DELETE_LIVESTOCK_LINE_NOT_ALLOWED);
                return;
            }

            LivestockBookLine.Delete(ActiveItem.Oid);
			_action_result = DialogResult.OK;
		}

        public override void CustomAction1() { SetAsExternalAction(); }

        public void SetAsExternalAction()
        {
            if (ActiveItem == null) return;

            LivestockBookLine.SetAsExternalHead(ActiveOID, ActiveItem.ETipo);

            //SetRowFormat(Tabla.CurrentRow);
        }
        
        #endregion

        #region Print

        public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            LivestockBookReportMng reportMng = new LivestockBookReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            ReportClass report = reportMng.GetListReport(LivestockBookLineList.GetList((IList<LivestockBookLineInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

    public partial class LivestockBookLineMngBaseForm : Skin06.EntityMngSkinForm<LivestockBookLineList, LivestockBookLineInfo>
    {
        public LivestockBookLineMngBaseForm()
            : this(false, null, null) { }

        public LivestockBookLineMngBaseForm(bool isModal, Form parent, LivestockBookLineList lista)
            : base(isModal, parent, lista) { }
    }
}
