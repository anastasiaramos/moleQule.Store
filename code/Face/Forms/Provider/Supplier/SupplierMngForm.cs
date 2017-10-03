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
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Proveedor;

namespace moleQule.Face.Store
{
	public partial class SupplierMngForm : SupplierMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ProveedorMngForm";
		public static Type Type { get { return typeof(SupplierMngForm); } }
		public override Type EntityType { get { return typeof(Proveedor); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Proveedor _entity;
		protected ETipoAcreedor _tipo_acreedor;
		moleQule.Base.EEstado _estado = moleQule.Base.EEstado.Todos;

		#endregion

		#region Factory Methods

		public SupplierMngForm()
			: this(false) { }

		public SupplierMngForm(bool isModal)
			: this(isModal, null, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos) { }

		public SupplierMngForm(bool isModal, Form parent)
			: this(isModal, parent, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos, null) { }

		public SupplierMngForm(bool isModal, Form parent, ETipoAcreedor tipo, moleQule.Base.EEstado estado)
			: this(isModal, parent, tipo, estado, null) { }

		public SupplierMngForm(bool isModal, Form parent, ProveedorList lista)
			: this(isModal, parent, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos, lista) { }

		protected SupplierMngForm(bool isModal, Form parent, ETipoAcreedor tipo, moleQule.Base.EEstado estado, ProveedorList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ProveedorList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			Text = (_tipo_acreedor == ETipoAcreedor.Acreedor) ? Resources.Labels.ACREEDORES : Resources.Labels.PROVEEDORES;

			_tipo_acreedor = tipo;
			_estado = estado;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 1;

			cols.Add(Nombre);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			ProveedorInfo item = row.DataBoundItem as ProveedorInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

        public override void FormatControls()
        {
            if (Tabla == null) return;

            base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Resources.Labels.PAGOS_TI, Properties.Resources.pago);
        }
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					MedioPagoLabel.Visible = false;
					CuentaPropia.Visible = false;
					CuentaAjena.Visible = false;
					FormaPago.Visible = false;
					DiasPago.Visible = false;

					ShowAction(molAction.ShowDocuments);
                    ShowAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);
                    ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Proveedor");

			// Guardamos la configuración actual del listado
			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ProveedorList.GetList(_estado, _tipo_acreedor, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Proveedores");
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
						ProveedorList listA = ProveedorList.GetList(_filter_results);
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
						ProveedorList listD = ProveedorList.GetList(_filter_results);
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
			ProveedorAddForm form = new ProveedorAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new ProveedorViewForm(ActiveOID, ActiveItem.ETipoAcreedor, this));
		}

		public override void OpenEditForm()
		{
			ProveedorEditForm form = new ProveedorEditForm(ActiveItem.Oid, ActiveItem.ETipoAcreedor, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Proveedor.Delete(ActiveOID, ActiveItem.ETipoAcreedor);
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
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			
			ProveedorReportMng reportMng = new ProveedorReportMng(AppContext.ActiveSchema);
			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ProveedorListRpt report = reportMng.GetListReport(ProveedorList.GetList((IList<ProveedorInfo>)Datos.List));

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}

	public partial class SupplierMngBaseForm : Skin06.EntityMngSkinForm<ProveedorList, ProveedorInfo>
	{
		public SupplierMngBaseForm()
			: this(false, null, null) { }

		public SupplierMngBaseForm(bool isModal, Form parent, ProveedorList lista)
			: base(isModal, parent, lista) { }
	}
}
