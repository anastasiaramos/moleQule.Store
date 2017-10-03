using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Hipatia;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class CustomAgentMngForm : CustomAgentMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "CustomAgentMngForm";
		public static Type Type { get { return typeof(CustomAgentMngForm); } }
		public override Type EntityType { get { return typeof(Despachante); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Despachante _entity;
		protected moleQule.Base.EEstado _status = moleQule.Base.EEstado.Todos;

		#endregion

		#region Factory Methods

		public CustomAgentMngForm()
			: this(false) { }

		public CustomAgentMngForm(bool isModal)
			: this(isModal, null, moleQule.Base.EEstado.Todos) { }

		public CustomAgentMngForm(bool isModal, Form parent, moleQule.Base.EEstado status)
			: this(isModal, parent, status, null) { }

		public CustomAgentMngForm(bool isModal, Form parent, moleQule.Base.EEstado status, DespachanteList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = DespachanteList.NewList().GetSortedList();
			SortProperty = Codigo.DataPropertyName;

			_status = status;
		}

		#endregion

		#region Layout

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

            SetActionStyle(molAction.CustomAction1, Resources.Labels.PAGOS_TI, Properties.Resources.pago);
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			DespachanteInfo item = row.DataBoundItem as DespachanteInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
                    ShowAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
                    ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Despachante");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = DespachanteList.GetList(_status, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Despachantes");
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
						DespachanteList listA = DespachanteList.GetList(_filter_results);
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
						DespachanteList listD = DespachanteList.GetList(_filter_results);
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
			DespachanteAddForm form = new DespachanteAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new DespachanteViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			DespachanteEditForm form = new DespachanteEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Despachante.Delete(ActiveOID);
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

        public override void CustomAction1() { GotoPagosAction(); }

        public void GotoPagosAction()
        {
            if (ActiveItem.EEstado == moleQule.Base.EEstado.Anulado) return;

            PaymentEditForm form = new PaymentEditForm(this, ActiveOID, PaymentSummary.Get(ActiveItem.ETipoAcreedor, ActiveOID));
            form.ShowDialog(this);
        }

		#endregion

		#region Print

		public override void PrintList()
		{
			/*DespachanteReportMng reportMng = new DespachanteReportMng(AppContext.ActiveSchema);
			
			DespachanteListRpt report = reportMng.GetListReport(list);
			
			ShowReport();*/
		}

		#endregion
	}

	public partial class CustomAgentMngBaseForm : Skin06.EntityMngSkinForm<DespachanteList, DespachanteInfo>
	{
		public CustomAgentMngBaseForm()
			: this(false, null, null) { }

		public CustomAgentMngBaseForm(bool isModal, Form parent, DespachanteList list)
			: base(isModal, parent, list) { }
	}
}
