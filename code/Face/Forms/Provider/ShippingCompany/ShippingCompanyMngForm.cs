using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face.Hipatia;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ShippingCompanyMngForm : ShippingCompanyMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ShippingCompanyMngForm";
		public static Type Type { get { return typeof(ShippingCompanyMngForm); } }
		public override Type EntityType { get { return typeof(Naviera); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Naviera _entity;
		moleQule.Base.EEstado _estado = moleQule.Base.EEstado.Todos;

		protected ETipoAlbaranes _tipo;
		protected ETipoAcreedor _tipo_acreedor;
		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public ShippingCompanyMngForm()
			: this(false, null, moleQule.Base.EEstado.Todos) { }

		public ShippingCompanyMngForm(bool isModal, Form parent, moleQule.Base.EEstado estado)
			: this(isModal, parent, null, estado) { }

		public ShippingCompanyMngForm(bool isModal, Form parent, NavieraList list, moleQule.Base.EEstado estado)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = NavieraList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			Text = Resources.Labels.NAVIERAS;

			_estado = estado;
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

			NavieraInfo item = row.DataBoundItem as NavieraInfo;

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
			PgMng.Grow(string.Empty, "Navieras");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = NavieraList.GetList(_estado, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Navieras");
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
						NavieraList listA = NavieraList.GetList(_filter_results);
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
						NavieraList listD = NavieraList.GetList(_filter_results);
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
			NavieraAddForm form = new NavieraAddForm();
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new NavieraViewForm(ActiveItem.Oid, this)); }
		
		public override void OpenEditForm()
		{
			NavieraEditForm form = new NavieraEditForm(ActiveItem.Oid, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Naviera.Delete(ActiveOID);
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
			/*NavieraReportMng reportMng = new NavieraReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			
			NavieraListRpt report = reportMng.GetListReport(list);

			ShowReport(report);*/
		}

		#endregion
	}

	public partial class ShippingCompanyMngBaseForm : Skin06.EntityMngSkinForm<NavieraList, NavieraInfo>
	{
		public ShippingCompanyMngBaseForm()
			: this(false, null, null) { }

		public ShippingCompanyMngBaseForm(bool isModal, Form parent, NavieraList lista)
			: base(isModal, parent, lista) { }
	}
}