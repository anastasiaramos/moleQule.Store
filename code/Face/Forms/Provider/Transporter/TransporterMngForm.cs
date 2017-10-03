using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class TransporterMngForm : TransporterMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "TransporterMngForm";
		public static Type Type { get { return typeof(TransporterMngForm); } }
		public override Type EntityType { get { return typeof(Transporter); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Transporter _entity;
		protected moleQule.Base.EEstado _estado;
		protected ETipoAlbaranes _tipo;
		protected ETipoAcreedor _tipo_acreedor;
		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public TransporterMngForm()
			: this(false) { }

		public TransporterMngForm(bool isModal)
			: this(isModal, null, moleQule.Base.EEstado.Todos) { }

		public TransporterMngForm(bool isModal, Form parent, moleQule.Base.EEstado estado)
			: this(isModal, parent, estado, null) { }

		protected TransporterMngForm(bool isModal, Form parent, moleQule.Base.EEstado estado, TransporterList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = TransporterList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

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

			TransporterInfo item = row.DataBoundItem as TransporterInfo;

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
			PgMng.Grow(string.Empty, "Transporter");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = TransporterList.GetList(_estado, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}

			PgMng.Grow(string.Empty, "Lista de Transportistas");
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
						TransporterList listA = TransporterList.GetList(_filter_results);
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
						TransporterList listD = TransporterList.GetList(_filter_results);
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
			TransporterAddForm form = new TransporterAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new TransporterViewForm(ActiveOID, ActiveItem.ETipoAcreedor, this)); }

		public override void OpenEditForm()
		{
			TransporterEditForm form = new TransporterEditForm(ActiveOID, ActiveItem.ETipoAcreedor, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Transporter.Delete(ActiveOID);
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
			/*TransportistaReportMng reportMng = new TransportistaReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			
			TransportistaListRpt report = reportMng.GetListReport(list);
			
			ShowReport(report);*/
		}

		#endregion
	}

	public partial class TransporterMngBaseForm : Skin06.EntityMngSkinForm<TransporterList, TransporterInfo>
	{
		public TransporterMngBaseForm()
			: this(false, null, null) { }

		public TransporterMngBaseForm(bool isModal, Form parent, TransporterList list)
			: base(isModal, parent, list) { }
	}
}
