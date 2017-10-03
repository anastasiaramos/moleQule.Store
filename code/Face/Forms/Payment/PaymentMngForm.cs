using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Payment;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class PaymentMngForm : PaymentMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "PagoMngForm";
		public static Type Type { get { return typeof(PaymentMngForm); } }
        public override Type EntityType { get { return typeof(Payment); } }

		protected override int BarSteps { get { return base.BarSteps + 6; } }

        protected Payment _entity;
		protected ETipoPago _tipo; 

		#endregion

		#region Factory Methods

		public PaymentMngForm()
			: this(ETipoPago.Todos) {}

		public PaymentMngForm(ETipoPago tipo) 
			: this(null, tipo) {}

		public PaymentMngForm(Form parent, ETipoPago tipo)
			: this(parent, tipo, null) {}

		public PaymentMngForm(Form parent, ETipoPago tipo, PaymentList list)
			: this(false, parent, tipo, list) { }

		public PaymentMngForm(bool is_modal, Form parent, ETipoPago tipo, PaymentList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();
			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = PaymentList.NewList().GetSortedList();
			OrderByColumn(Vencimiento, ListSortDirection.Descending);

			_tipo = tipo;

			this.Text = String.Format(Resources.Labels.PAGOS_TITLE, moleQule.Store.Structs.EnumText<ETipoPago>.GetLabel(_tipo));		
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
			Agente.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Agente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

        public override void FormatControls()
        {
            base.FormatControls();

            switch (_tipo)
            { 
                case ETipoPago.ExtractoTarjeta:

                    IDPagoLabel.Visible = false;
                    TipoPagoLabel.Width = 120;

                    break;
            }
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			PaymentInfo item = (PaymentInfo)row.DataBoundItem;

            switch (item.EEstado)
            {
                case moleQule.Base.EEstado.Abierto:
                    Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstadoPago);
                    break;

                case moleQule.Base.EEstado.Anulado:
                    Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
                    return;

                default:
                    Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
                    break;
            }

			if (item.Pendiente != 0)
				row.Cells[PendienteAsignacion.Name].Style = Common.ControlTools.Instance.CobradoStyle;
			else
				row.Cells[PendienteAsignacion.Name].Style = row.Cells[Importe.Index].Style;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.PrintListQR);

					break;

				case molView.Normal:

                    ETipoPago tipo = ActiveItem == null ? _tipo : ActiveItem.ETipoPago;

                    if (tipo == ETipoPago.Prestamo)
                        HideAction(molAction.Add);
                    else
					    ShowAction(molAction.Add);

					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ShowDocuments);
					ShowAction(molAction.PrintListQR);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Pago");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:

					if (moleQule.Common.ModulePrincipal.GetUseActiveYear())
						List = PaymentList.GetList(_tipo, moleQule.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = PaymentList.GetList(_tipo, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Pagos");
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
						PaymentList listA = PaymentList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.ChangeStateContabilizado:
				case molAction.ChangeStateAnulado:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
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
            ETipoPago tipo = (_tipo != ETipoPago.Todos)
                                ? _tipo
                                : ActiveItem == null 
                                    ? _tipo 
                                    : ActiveItem.ETipoPago;

			switch (tipo)
			{
				case ETipoPago.Factura:
					{
						OpenEditForm();
						_entity = null;
					}
					break;

				case ETipoPago.Gasto:
					{
						ExpensePaymentAddForm form = new ExpensePaymentAddForm(tipo, this);
						AddForm(form);
						_entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
                    }
                    break;

				case ETipoPago.Nomina:
					{
                        if (ActiveItem.OidAgente == 0)
                        {
                            PayrollPaymentAddForm form = new PayrollPaymentAddForm(tipo, this);
                            AddForm(form);
                            _entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
                        }
                        else
                        {
                            OpenEditForm();
                            _entity = null;
                        }
					}
					break;

                case ETipoPago.Prestamo:
                    {
                        LoanPaymentAddForm form = new LoanPaymentAddForm(tipo, this);
                        AddForm(form);
						_entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
                    }
                    break;

                case ETipoPago.Fraccionado:
                    {
                        PagoFraccionadoAddForm form = new PagoFraccionadoAddForm(tipo, this);
                        AddForm(form);
                        _entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
                    }
                    break;

                case ETipoPago.ExtractoTarjeta:
                    {
                        CreditCardPaymentAddForm form = new CreditCardPaymentAddForm(tipo, this);
                        AddForm(form);
                        _entity = (form.ActionResult == DialogResult.OK) ? form.Entity : null;
                    }
                    break;
			}
		}

		public override void OpenEditForm()
		{
			switch (ActiveItem.ETipoPago)
			{
				case ETipoPago.Factura:
					{
						PaymentSummary item = PaymentSummary.Get((ETipoAcreedor)ActiveItem.TipoAgente, ActiveItem.OidAgente);

						PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidAgente, item);
						if (form.Entity != null)
						{
							form.Select(ActiveItem);
							AddForm(form);
						}
                        item.CloseSession();
					}
                    break;

				case ETipoPago.Nomina:
					{
                        if (ActiveItem.OidAgente == 0)
                        {
                            PayrollPaymentEditForm form = new PayrollPaymentEditForm(ActiveItem.Oid, ETipoPago.Nomina, this);
                            if (form.Entity != null)
                            {
                                AddForm(form);
                                _entity = form.Entity;
                            }
                        }
                        else
                        {
                            PaymentSummary item = PaymentSummary.Get((ETipoAcreedor)ActiveItem.TipoAgente, ActiveItem.OidAgente);

                            EmployeePaymentEditForm form = new EmployeePaymentEditForm(this, ActiveItem.OidAgente, item);
                            if (form.Entity != null)
                            {
                                form.Select(ActiveItem);
                                AddForm(form);
                            }
                            item.CloseSession();
                        }
					}
					break;

                case ETipoPago.Prestamo:
                    {
                        LoanPaymentEditForm form = new LoanPaymentEditForm(ActiveItem.Oid, ETipoPago.Prestamo, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            _entity = form.Entity;
                        }
                    }
                    break;

                case ETipoPago.Fraccionado:
                    {
                        PagoFraccionadoEditForm form = new PagoFraccionadoEditForm(ActiveItem.Oid, ETipoPago.Fraccionado, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            _entity = form.Entity;
                        }
                    }
                    break;

                case ETipoPago.ExtractoTarjeta:
                    {
                        CreditCardPaymentEditForm form = new CreditCardPaymentEditForm(ActiveItem.Oid, ETipoPago.ExtractoTarjeta, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            _entity = form.Entity;
                        }
                    }
                    break;

				default:
					{
						ExpensePaymentEditForm form = new ExpensePaymentEditForm(ActiveItem.Oid, ActiveItem.ETipoPago, this);
						if (form.Entity != null)
						{
							AddForm(form);
							_entity = form.Entity;
						}
					}
					break;
			}

		}

		public override void OpenViewForm()
		{
			switch (ActiveItem.ETipoPago)
			{
				case ETipoPago.Factura:
					OpenEditForm();
					break;

                case ETipoPago.Nomina:
                    {
                        if (ActiveItem.OidAgente == 0)
                            AddForm(new PayrollPaymentViewForm(ActiveOID, ETipoPago.Nomina, this));
                        else
                            OpenEditForm();
                    }
                    break;

                case ETipoPago.Prestamo:
                    AddForm(new LoanPaymentViewForm(ActiveOID, ETipoPago.Prestamo, this));
                    break;

                case ETipoPago.Fraccionado:
                    AddForm(new PagoFraccionadoViewForm(ActiveOID, ETipoPago.Fraccionado, this));
                    break;

                case ETipoPago.ExtractoTarjeta:
                    AddForm(new CreditCardPaymentViewForm(ActiveOID, ETipoPago.ExtractoTarjeta, this));
                    break;

   				default:
					AddForm(new ExpensePaymentViewForm(ActiveOID, ActiveItem.ETipoPago, this));
					break;
			}
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
            _entity = Payment.ChangeEstado(ActiveOID, ActiveItem.ETipoPago, ActiveItem.ETipoAcreedor, Base.EnumConvert.ToEEstado(estado));
		
			_action_result = DialogResult.OK;
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			reportMng.ShowQRCode = false;

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			PaymentListRpt report = reportMng.GetListReport(PaymentList.GetList((IList<PaymentInfo>)Datos.List), null);

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintQRAction()
		{
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
			PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			reportMng.ShowQRCode = true;

			PgMng.Grow();
            TransactionPaymentList pfacturas = TransactionPaymentList.GetList(false);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            PaymentListRpt report = reportMng.GetListReport(PaymentList.GetList((IList<PaymentInfo>)Datos.List), pfacturas);

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}

    public partial class PaymentMngBaseForm : Skin06.EntityMngSkinForm<PaymentList, PaymentInfo>
    {
        public PaymentMngBaseForm()
            : this(false, null, null) { }

        public PaymentMngBaseForm(bool isModal, Form parent, PaymentList lista)
            : base(isModal, parent, lista) { }
    }
}
