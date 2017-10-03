using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face.Hipatia;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Invoice;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputInvoiceMngForm : InputInvoiceMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "InputInvoiceMngForm";
		public static Type Type { get { return typeof(InputInvoiceMngForm); } }
		public override Type EntityType { get { return typeof(InputInvoice); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected InputInvoice _entity;

		protected ETipoFacturas _tipo;

		#endregion

		#region Factory Methods

		public InputInvoiceMngForm() 
			: this(false, ETipoFacturas.Todas) { }

		public InputInvoiceMngForm(Form parent)
			: this(false, parent, ETipoFacturas.Todas) { }

        public InputInvoiceMngForm(ETipoFacturas invoiceType)
            : this(false, invoiceType) { }

        public InputInvoiceMngForm(bool isModal, Form parent)
            : this(isModal, parent, ETipoFacturas.Todas) { }

        public InputInvoiceMngForm(bool isModal, ETipoFacturas invoiceType)
            : this(isModal, null, invoiceType) { }

        public InputInvoiceMngForm(Form parent, ETipoFacturas invoiceType)
            : this(false, parent, invoiceType) { }

        public InputInvoiceMngForm(bool isModal, Form parent, ETipoFacturas invoiceType)
            : this(isModal, parent, invoiceType, null) { }

        public InputInvoiceMngForm(Form parent, ETipoFacturas invoiceType, InputInvoiceList list)
			: this(false, parent, invoiceType, list) { }

        public InputInvoiceMngForm(bool isModal, Form parent, ETipoFacturas tipo, InputInvoiceList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
            Datos.DataSource = InputInvoiceList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
            SortDirection = ListSortDirection.Descending;

            SetView(molView.Normal);

			_tipo = tipo;

			switch (tipo)
			{
				case ETipoFacturas.Todas:
					this.Text = Resources.Labels.FACTURA_RECIBIDAS_TODAS;
					break;
				case ETipoFacturas.Pagadas:
					this.Text = Resources.Labels.FACTURA_RECIBIDAS_PAGADAS;
					break;
				case ETipoFacturas.Pendientes:
					this.Text = Resources.Labels.FACTURA_RECIBIDAS_PENDIENTES;
					break;
            }
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

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Acreedor.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Acreedor);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.PROVEEDOR_TI, Properties.Resources.proveedor);
			SetActionStyle(molAction.CustomAction2, Resources.Labels.PAGOS_TI, Properties.Resources.pago);
			
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

			InputInvoiceInfo item = row.DataBoundItem as InputInvoiceInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			if (item.Pagada) return;
			/*{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.CobradoStyle;
			}*/
			else if (0 <= item.DiasTranscurridos && item.DiasTranscurridos < 15)
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleA;
			}
			else if (15 <= item.DiasTranscurridos && item.DiasTranscurridos < 31)
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleB;
			}
			else if (31 <= item.DiasTranscurridos && item.DiasTranscurridos < 45)
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleC;
			}
			else if (45 <= item.DiasTranscurridos && item.DiasTranscurridos < 60)
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleD;
			}
			else
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleE;
			}
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateEmitido);
					HideAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);

					Cobrado.Visible = false;
					Pendiente.Visible = false;
					DiasTranscurridos.Visible = false;
					Albaranes.Visible = false;
					IDMovimientoContable.Visible = false;
					EstadoLabel.Visible = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);
					ShowAction(molAction.CustomAction1);
					ShowAction(molAction.CustomAction2);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
            PgMng.Grow(string.Empty, "InputInvoiceMngForm::RefreshMainData INI");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoFacturas.Todas:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputInvoiceList.GetList(ETipoAcreedor.Todos, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = InputInvoiceList.GetList(false);
							break;

						case ETipoFacturas.Pagadas:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputInvoiceList.GetPagadasList(ETipoAcreedor.Todos, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = InputInvoiceList.GetPagadasList(false);
							break;

						case ETipoFacturas.Pendientes:
							if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
								List = InputInvoiceList.GetPendientesList(ETipoAcreedor.Todos, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = InputInvoiceList.GetPendientesList(false);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}

            PgMng.Grow(string.Empty, "InputInvoiceMngForm::RefreshMainData END");
		}

		protected override void RefreshSources()
		{
			base.RefreshSources();

			//Creamos la cache con los facturas
			Cache.Instance.Save(typeof(InputInvoiceList), List);
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
						InputInvoiceList listA = InputInvoiceList.GetList(_filter_results);						
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.ChangeStateContabilizado:
				case molAction.ChangeStateEmitido:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveItem.Oid);
					if (FilterType == IFilterType.Filter)
					{
						InputInvoiceList listD = InputInvoiceList.GetList(_filter_results);						
						listD.RemoveItem(ActiveItem.Oid);
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
			InputInvoiceAddForm form = new InputInvoiceAddForm(this);
			AddForm(form);
			_entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
		}

		public override void OpenViewForm() { AddForm(new InputInvoiceViewForm(ActiveOID, ActiveItem.ETipoAcreedor, this)); }

		public override void OpenEditForm()
		{
			try
			{
				moleQule.Common.EntityBase.CheckEditAllowedEstado(ActiveItem.EEstado, moleQule.Base.EEstado.Abierto);
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
				_action_result = DialogResult.Ignore;
				return;
			}

			InputInvoiceEditForm form = new InputInvoiceEditForm(ActiveOID, ActiveItem.ETipoAcreedor, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
			}
		}

		public override void DeleteAction()
		{
			InputInvoice.Delete(ActiveOID, ActiveItem.ETipoAcreedor);
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

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = InputInvoice.ChangeEstado(ActiveOID, ActiveItem.ETipoAcreedor, moleQule.Base.EnumConvert.ToEEstado(estado));
			
			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { ShowProveedorAction(); }
		public override void CustomAction2() { ShowPagoAction(); }

		public virtual void ShowPagoAction()
		{
			PaymentSummary item = PaymentSummary.Get((ETipoAcreedor)ActiveItem.TipoAcreedor, ActiveItem.OidAcreedor);
			PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidAcreedor, item);
			form.ShowDialog(this);
		}

		public virtual void ShowProveedorAction()
		{
			ProviderMngForm.OpenEditFormAction(ActiveItem.ETipoAcreedor, ActiveItem.OidAcreedor, this);
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            InputInvoiceListRpt report = reportMng.GetListReport(InputInvoiceList.GetList(Datos.DataSource as IList<InputInvoiceInfo>),
																		ProviderBaseList.GetList(false));

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintQRAction()
		{
			/*PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			QRCodeRpt report = reportMng.GetQRCodeReport(InputInvoiceList.GetList(Datos.DataSource as IList<InputInvoiceInfo>));

			PgMng.FillUp();

			ShowReport(report);*/

			if (ActiveItem == null) return;

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

            InputInvoiceInfo item = InputInvoiceInfo.Get(ActiveOID, ActiveItem.ETipoAcreedor, true);

            ReportClass report = reportMng.GetQRCodeReport(item);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);

		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			InputInvoiceReportMng reportMng = new InputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			InputInvoiceInfo item = InputInvoiceInfo.Get(ActiveOID, ActiveItem.ETipoAcreedor, true);
            
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

            ProviderBaseInfo provider = ProviderBaseInfo.Get(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor,false);
            SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);

            conf.nota = ( provider.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
            conf.nota += (conf.nota != string.Empty) ? Environment.NewLine : string.Empty;
            conf.nota += (ActiveItem.Nota ? serie.Cabecera : "");
            conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
            PgMng.Grow();

            ReportClass report = reportMng.GetDetailReport(item, conf);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);
		}

		#endregion

		#region Events

		private void FacturaRecibidaMngForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Cache.Instance.Remove(typeof(InputInvoiceList));
		}

		#endregion
	}

	public class InputInvoiceAllMngForm : InputInvoiceMngForm
	{
		public new const string ID = "InputInvoiceAllMngForm";
		public new static Type Type { get { return typeof(InputInvoiceAllMngForm); } }

		public InputInvoiceAllMngForm() : this(null) { }
		public InputInvoiceAllMngForm(Form parent) : base(parent, ETipoFacturas.Todas) { }
	}

	public class InputInvoiceDueMngForm : InputInvoiceMngForm
	{
		public new const string ID = "InputInvoiceDueMngForm";
		public new static Type Type { get { return typeof(InputInvoiceDueMngForm); } }

		public InputInvoiceDueMngForm() : this(null) { }
		public InputInvoiceDueMngForm(Form parent) : base(parent, ETipoFacturas.Pendientes) { }
	}

	public class InputInvoicePayedMngForm : InputInvoiceMngForm
	{
		public new const string ID = "InputInvoicePayedMngForm";
		public new static Type Type { get { return typeof(InputInvoicePayedMngForm); } }

		public InputInvoicePayedMngForm() : this(null) { }
		public InputInvoicePayedMngForm(Form parent) : base(parent, ETipoFacturas.Pagadas) { }
	}

    public partial class InputInvoiceMngBaseForm : Skin08.EntityMngSkinForm<InputInvoiceList, InputInvoiceInfo, InputInvoice>
	{
		public InputInvoiceMngBaseForm()
			: this(false, null, null) { }

        public InputInvoiceMngBaseForm(bool isModal, Form parent, InputInvoiceList list)
			: base(isModal, parent, list) { }
	}
}