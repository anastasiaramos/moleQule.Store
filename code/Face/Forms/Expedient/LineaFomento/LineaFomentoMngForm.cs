using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.CslaEx;

using moleQule;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Face.Hipatia;

namespace moleQule.Face.Store
{
	public partial class LineaFomentoMngForm : LineaFomentoMngBaseForm
    {
        #region Attributes & Properties

		public const string ID = "LineaFomentoMngForm";
		public static Type Type { get { return typeof(LineaFomentoMngForm); } }
		public override Type EntityType { get { return typeof(LineaFomento); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public LineaFomento _entity;

        #endregion
		
		#region Factory Methods

		public LineaFomentoMngForm()
            : this(null) {}
			
		public LineaFomentoMngForm(Form parent)
			: this(false, parent) {}
		
		public LineaFomentoMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}
		
		public LineaFomentoMngForm(bool is_modal, Form parent, LineaFomentoList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();
			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = LineaFomentoList.NewList().GetSortedList();
			SortProperty = FechaConocimiento.DataPropertyName;
        }
		
		#endregion

		#region Authorization

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
				case molAction.ChangeStateContabilizado:

					if ((AppContext.User != null) && (state))
						base.ActivateAction(action, AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE));
					else
						base.ActivateAction(action, state);

					break;

				default:
					base.ActivateAction(action, state);
					break;
			}
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Producto.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Producto);
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
            
			LineaFomentoInfo item = (LineaFomentoInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Delete);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Delete);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "LineaFomento");
			
			long oid = ActiveOID;
			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = LineaFomentoList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de LineaFomentos");
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
                        LineaFomentoList listA = LineaFomentoList.GetList(_filter_results);
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
                        LineaFomentoList listD = LineaFomentoList.GetList(_filter_results);
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

		protected override void DefaultAction() {}

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
            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, string.Empty);
			
			LineaFomentoListRpt report = reportMng.GetLineaFomentoListReport(List);

			ShowReport(report);
		}

		#endregion
    }

    public partial class LineaFomentoMngBaseForm : Skin06.EntityMngSkinForm<LineaFomentoList, LineaFomentoInfo>
    {
        public LineaFomentoMngBaseForm()
            : this(false, null, null) { }

        public LineaFomentoMngBaseForm(bool isModal, Form parent, LineaFomentoList lista)
            : base(isModal, parent, lista) { }
    }
}
