using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports;
using moleQule.Library.Store.Reports.Nomina;

namespace moleQule.Face.Store
{
	public partial class PayrollBatchMngForm : PayrollBatchMngBaseForm
    {
        #region Attributes & Properties

		public const string ID = "PayrollBatchMngForm";
		public static Type Type { get { return typeof(PayrollBatchMngForm); } }
		public override Type EntityType { get { return typeof(PayrollBatch); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public PayrollBatch _entity;

		#endregion
		
		#region Factory Methods

		public PayrollBatchMngForm()
            : this(null) {}
			
		public PayrollBatchMngForm(Form parent)
			: this(false, parent) {}
		
		public PayrollBatchMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}

		public PayrollBatchMngForm(bool is_modal, Form parent, PayrollBatchList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
			Datos.DataSource = PayrollBatchList.NewList().GetSortedList();			
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
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
			Descripcion.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Descripcion);
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
            
			PayrollBatchInfo item = (PayrollBatchInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);

					break;

				case molView.Normal:

					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);

					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Payroll");
			
			long oid = ActiveOID;
			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
					List = PayrollBatchList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Nominas");
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
						PayrollBatchList listA = PayrollBatchList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Unlock:
				case molAction.ChangeStateContabilizado:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
						PayrollBatchList listD = PayrollBatchList.GetList(_filter_results);
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

        #region Print

        public override void PrintList()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			PayrollBatchReportMng reportMng = new PayrollBatchReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            RemesaNominasListRpt report = reportMng.GetListReport(List);

            PgMng.FillUp();

            ShowReport(report);
        }

        #endregion

        #region Actions

        public override void OpenAddForm()
        {
			TipoGastoInfo tipo = TipoGastoInfo.Get(Library.Store.ModulePrincipal.GetDefaultNominasSetting(), false);
			if (tipo.Oid == 0) throw new iQException(Library.Store.Resources.Messages.NO_TIPOGASTO_NOMINAS);

			PayrollbatchAddForm form = new PayrollbatchAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{			
			AddForm(new PayrollBatchViewForm(ActiveOID, this));
		}

        public override void OpenEditForm() 
        {             
			PayrollBatchEditForm form = new PayrollBatchEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
            PayrollBatch.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = PayrollBatch.ChangeStatus(ActiveOID, Base.EnumConvert.ToEEstado(estado));
			
            _action_result = DialogResult.OK;
		}

		#endregion
    }

	public partial class PayrollBatchMngBaseForm : Skin06.EntityMngSkinForm<PayrollBatchList, PayrollBatchInfo>
	{
		public PayrollBatchMngBaseForm()
			: this(false, null, null) { }

		public PayrollBatchMngBaseForm(bool isModal, Form parent, PayrollBatchList lista)
			: base(isModal, parent, lista) { }
	}
}
