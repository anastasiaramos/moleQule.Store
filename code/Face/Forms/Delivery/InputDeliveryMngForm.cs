using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Albaran;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputDeliveryMngForm : InputDeliveryMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "InputDeliveryMngForm";
		public static Type Type { get { return typeof(InputDeliveryMngForm); } }
		public override Type EntityType { get { return typeof(InputDelivery); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected InputDelivery _entity;

		protected ETipoAlbaranes _tipo;
		protected ETipoAcreedor _tipo_acreedor;
		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public InputDeliveryMngForm()
			: this(null) { }

		public InputDeliveryMngForm(Form parent)
			: this(false, parent) { }

		public InputDeliveryMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, ETipoAlbaranes.Todos, ETipoAcreedor.Todos) { }

		public InputDeliveryMngForm(ETipoAlbaranes tipo, ETipoAcreedor tipo_acreedor)
			: this(false, tipo, tipo_acreedor) { }

		public InputDeliveryMngForm(bool is_modal, ETipoAlbaranes tipo, ETipoAcreedor tipo_acreeedor)
			: this(is_modal, null, tipo, tipo_acreeedor) { }

		public InputDeliveryMngForm(bool is_modal, Form parent, ETipoAlbaranes tipo, ETipoAcreedor tipo_acreedor)
			: this(is_modal, parent, tipo, tipo_acreedor, null) { }

		public InputDeliveryMngForm(bool is_modal, Form parent, ETipoAlbaranes tipo, ETipoAcreedor tipo_acreedor, InputDeliveryList lista)
			: base(is_modal, parent, lista)
		{
			InitializeComponent();
			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = InputDeliveryList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_tipo = tipo;
			_tipo_acreedor = tipo_acreedor;

			this.Text = Resources.Labels.ALBARAN_TODOS;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			NombreCliente.Tag = 0.2;
			Observaciones.Tag = 0.8;

			cols.Add(NombreCliente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.PROVEEDOR_TI, Properties.Resources.proveedor);
			SetActionStyle(molAction.CustomAction2, Resources.Labels.CREAR_FACTURA, Properties.Resources.factura_recibida);
			SetActionStyle(molAction.CustomAction3, Resources.Labels.PAGOS_TI, Properties.Resources.pago);

			try
			{
				string pattern = "{0} ({1})";
				Total.HeaderText = string.Format(pattern, Total.HeaderText, AppControllerBase.Culture.NumberFormat.CurrencySymbol);

				string number_format = "N" + AppControllerBase.Culture.NumberFormat.CurrencyDecimalDigits;
				BaseImponible.DefaultCellStyle.Format = number_format;
				Impuestos.DefaultCellStyle.Format = number_format;
				Total.DefaultCellStyle.Format = number_format;
			}
			catch { }
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			InputDeliveryInfo item = row.DataBoundItem as InputDeliveryInfo;

			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
                    HideAction(molAction.CustomAction4);

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.Copy);
					ShowAction(molAction.Edit);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.CustomAction1);
					ShowAction(molAction.CustomAction2);
					ShowAction(molAction.CustomAction3);
                    HideAction(molAction.CustomAction4);

					Seleccionar.Visible = false;
					Seleccionar.ReadOnly = false;
					EstadoLabel.Visible = false;

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "InputDelivery");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoAlbaranes.Todos:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputDeliveryList.GetList(false, _tipo_acreedor, moleQule.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = InputDeliveryList.GetList(false);
							break;
						case ETipoAlbaranes.Facturados:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputDeliveryList.GetFacturados(false, _tipo_acreedor, moleQule.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = InputDeliveryList.GetFacturados(false);
							break;
						case ETipoAlbaranes.NoFacturados:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputDeliveryList.GetNoFacturados(true, _tipo_acreedor, _oid_cliente, _oid_serie, moleQule.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = InputDeliveryList.GetNoFacturados(true, _tipo_acreedor, _oid_cliente, _oid_serie);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de AlbaranProveedores");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					if (List.GetItem(_entity.Oid) != null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						InputDeliveryList listA = InputDeliveryList.GetList(_filter_results);
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
						InputDeliveryList listD = InputDeliveryList.GetList(_filter_results);
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
			InputDeliveryAddForm form = new InputDeliveryAddForm(this, _tipo);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new InputDeliveryViewForm(ActiveOID, ActiveItem.ETipoAcreedor, this)); }

		public override void OpenEditForm()
		{
			if (ActiveItem.Facturado)
			{
				PgMng.ShowInfoException("No es posible modificar un albarán facturado.");

				_action_result = DialogResult.Ignore;
				return;
			}

			InputDeliveryEditForm form = new InputDeliveryEditForm(ActiveOID, this, ActiveItem.ETipoAcreedor);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			InputDelivery.Delete(ActiveOID, ActiveItem.ETipoAcreedor);
			_action_result = DialogResult.OK;
		}

		public override void CopyObjectAction(long oid)
		{
			InputDeliveryAddForm form = new InputDeliveryAddForm(InputDelivery.CloneAsNew(ActiveItem), this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void CustomAction1() { ShowProveedorAction(); }
		public override void CustomAction2() { CrearFacturaAction(); }
		public override void CustomAction3() { ShowPagosAction(); }

		public virtual void CrearFacturaAction()
		{
			InputDeliverySelectForm form = new InputDeliverySelectForm(this, InputDeliveryList.GetNoFacturados(true));
			form.ShowDialog(this);

			if (form.DialogResult == DialogResult.OK)
			{
				try
				{
					PgMng.Reset(4, 1, Resources.Messages.GENERANDO_FACTURAS, this);
					List<InputDeliveryInfo> deliveries = form.Selected as List<InputDeliveryInfo>;
					PgMng.Grow();

                    InputInvoices invoices = InputInvoices.NewList();
                    invoices.NewItems(deliveries);
					PgMng.Grow();

                    invoices.Save();
					invoices.CloseSession();

					_selected = deliveries;
					_action_result = DialogResult.OK;
				}
				catch (Exception ex)
				{
					PgMng.ShowInfoException(ex);
				}
				finally
				{
					PgMng.FillUp();
					RefreshList();
				}
			}
		}

		public virtual void ShowPagosAction()
		{
			PaymentSummary item = PaymentSummary.Get((ETipoAcreedor)ActiveItem.TipoAcreedor, ActiveItem.OidAcreedor);
			PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidAcreedor, item);
			form.ShowDialog(this);
		}

		public virtual void ShowProveedorAction()
		{
			ProviderMngForm.OpenEditFormAction(ActiveItem.ETipoAcreedor, ActiveItem.OidAcreedor, this);
		}

		public virtual void SelectAction()
		{
			if (Tabla.CurrentRow != null)
                Tabla.CurrentRow.Cells[Seleccionar.Name].Value = "True";
            
			ExecuteAction(molAction.Select); 
		}

		public override void SelectObject()
		{
			Datos.MoveFirst();
			Datos.MoveLast();

			List<InputDeliveryInfo> list = new List<InputDeliveryInfo>();

			foreach (DataGridViewRow row in Tabla.Rows)
			{
				if (row.Cells[Seleccionar.Name].Value != null)
					if (((DataGridViewCheckBoxCell)row.Cells[Seleccionar.Name]).Value.ToString() == "True")
						list.Add(row.DataBoundItem as InputDeliveryInfo);
			}

			_selected = list;
			_action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

			ProviderBaseList acreedores = ProviderBaseList.GetList(false);
			PgMng.Grow();

			SerieList series = SerieList.GetList(false);
			PgMng.Grow();

			InputDeliveryReportMng rptMng = new InputDeliveryReportMng(AppContext.ActiveSchema, Text, FilterValues);

			AlbaranRecibidoListRpt report = rptMng.GetListReport(InputDeliveryList.GetList(Datos.DataSource as IList<InputDeliveryInfo>), 
																series, 
																acreedores);
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			InputDeliveryReportMng reportMng = new InputDeliveryReportMng(AppContext.ActiveSchema, Text, FilterValues);
			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();
			conf.nota = ActiveItem.Nota ? serie.Cabecera : "";

			/*AlbaranProveedorRpt report = reportMng.GetAlbaranProveedorReport(InputDeliveryInfo.Get(ActiveOID, true), conf);

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

    public partial class InputDeliveryMngBaseForm : Skin06.EntityMngSkinForm<InputDeliveryList, InputDeliveryInfo>
    {
        public InputDeliveryMngBaseForm()
            : this(false, null, null) { }

        public InputDeliveryMngBaseForm(bool isModal, Form parent, InputDeliveryList lista)
            : base(isModal, parent, lista) { }
    }

	public class InputDeliveryAllMngForm : InputDeliveryMngForm
	{
		public new const string ID = "InputDeliveryAllMngForm";
		public new static Type Type { get { return typeof(InputDeliveryAllMngForm); } }

		public InputDeliveryAllMngForm(Form parent)
			: base(false, parent, ETipoAlbaranes.Todos, ETipoAcreedor.Todos)
		{
			this.Text = Resources.Labels.ALBARAN_TODOS;
		}
	}

	public class InputDeliveryBilledMngForm : InputDeliveryMngForm
	{
		public new const string ID = "InputDeliveryBilledMngForm";
		public new static Type Type { get { return typeof(InputDeliveryBilledMngForm); } }

		public InputDeliveryBilledMngForm(Form parent)
			: base(false, parent, ETipoAlbaranes.Facturados, ETipoAcreedor.Todos)
		{
			this.Text = Resources.Labels.ALBARAN_FACTURADOS;
		}
	}

	public class InputDeliveryNoBilledMngForm : InputDeliveryMngForm
	{
		public new const string ID = "InputDeliveryNoBilledMngForm";
		public new static Type Type { get { return typeof(InputDeliveryNoBilledMngForm); } }

		public InputDeliveryNoBilledMngForm(Form parent)
			: base(false, parent, ETipoAlbaranes.NoFacturados, ETipoAcreedor.Todos)
		{
			this.Text = Resources.Labels.ALBARAN_NO_FACTURADOS;
		}
	}
}
