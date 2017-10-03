using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Payment;

namespace moleQule.Face.Store
{
    public partial class PaymentUIForm : PaymentForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        #endregion

        #region Factory Methods

        public PaymentUIForm() 
			: this(null, -1, null) { }
            
        public PaymentUIForm(Form parent, long oidAgent, PaymentSummary summary)
			: base(parent, oidAgent, summary)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                IAcreedor temp = _entity.IClone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.ISave();
                    _entity.ApplyEdit();

                    return true;
                }
                catch (Exception ex)
                {
					PgMng.ShowInfoException(iQExceptionHandler.GetAllMessages(ex));
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }
        
        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _invoices = InputInvoiceList.GetListByAcreedor(_entity.Oid, _entity.ETipoAcreedor, false);
            PgMng.Grow();

            Datos.DataSource = _entity;
			PgMng.Grow();

            _entity.LoadChilds(typeof(Payment), true);
            Datos_Pago.DataSource = _entity.Pagos;
			PgMng.Grow();

            Datos_Resumen.DataSource = _summary;

            base.RefreshMainData();
        }

        public void Select(Payment pago)
        {
            if (pago == null) return;
            int foundIndex = Datos_Pago.IndexOf(pago);
            Datos_Pago.Position = foundIndex;
        }
        public void Select(PaymentInfo pago)
        {
            Payment item = _entity.Pagos.GetItem(pago.Oid);

            if (pago == null) return;
            int foundIndex = Datos_Pago.IndexOf(item);
            Datos_Pago.Position = foundIndex;
        }

        #endregion

		#region Business Methods

		protected void ChangeState(moleQule.Base.EEstado estado)
		{
			if (Pago == null)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
				return;
			}

			if (Pago.EEstado == moleQule.Base.EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

			switch (estado)
			{
				case moleQule.Base.EEstado.Anulado:
					{
						if (Pago.EEstado == moleQule.Base.EEstado.Contabilizado)
						{
							PgMng.ShowInfoException(moleQule.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
							return;
						}

						if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
						{
							return;
						}
					}
					break;
			}

			try
			{
				_entity.Pagos.ChangeState(estado, Pago, _entity);
				_entity.ApplyEdit();
				_entity.ISave(Pago);
				_entity.BeginEdit();

                Payment current = Pago;
				Datos_Pago.ResetBindings(false);
				_summary.Refresh(_entity);
				Datos_Resumen.ResetBindings(false);
                SetGridColors(Pagos_DGW.Name);
                _invoices = InputInvoiceList.GetListByAcreedor(_entity.Oid, _entity.ETipoAcreedor, false);
				Select(current);
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}

		#endregion

		#region Actions

		protected override void SaveAction()
        {
            if (!_entity.CloseSessions)
                _entity.CloseSession();
            _action_result = DialogResult.OK;
        }

		protected override void PrintAction() { PrintObject(); }

        public override void PrintObject()
        {
			PgMng.Reset(3, 1, Face.Resources.Messages.BUILDING_REPORT);
            
			PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema, Resources.Labels.PAGOS, "Acreedor = " + Entity.Nombre);
			PgMng.Grow();

			PagoAcreedorDetailRpt report = reportMng.GetPagoAcreedorDetailReport(Summary, _entity.Pagos);
			
			PgMng.FillUp();
			
			ShowReport(report);
        }

		protected override void DocumentsAction()
		{
			try
			{
                AgenteEditForm form = new AgenteEditForm(typeof(Payment), Pago.GetInfo() as IAgenteHipatia);
				form.ShowDialog(this);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
                    AgenteAddForm form = new AgenteAddForm(typeof(Payment), Pago.GetInfo() as IAgenteHipatia);
					form.ShowDialog(this);
				}
			}
		}

		protected override void RefreshAction()
		{
            Payment current = Pago;

            _entity.LoadChilds(typeof(Payment), true);

            Datos_Pago.DataSource = _entity.Pagos.GetSortedList("IdPago", ListSortDirection.Descending);
            Pagos_DGW.Refresh();
            Datos_Pago.ResetBindings(true);

            _invoices = InputInvoiceList.GetListByAcreedor(_entity.Oid, _entity.ETipoAcreedor, false);
            UpdatePendientes();

            _summary.Refresh(_entity);
            Datos_Resumen.ResetBindings(false);

			SetGridColors(Pagos_DGW.Name);

            Select(current);
		}

		protected override void AddPagoAction() 
		{
			InvoicePaymentAddForm form = new InvoicePaymentAddForm(this, _entity);
			form.ShowDialog(this);

            //_facturas = InputInvoiceList.GetListByAcreedor(_entity.Oid, _entity.ETipoAcreedor, false);
            //UpdatePendientes();
			//Datos_Pago.ResetBindings(false);
			//Datos_Pago.MoveLast();
			//_resumen.Refresh(_entity);
			//Datos_Resumen.ResetBindings(false);
			//SetGridColors(Pagos_DGW.Name);
            RefreshAction();
            Datos_Pago.MoveFirst();
		}

		protected override void ViewPagoAction() 
		{
			if (Pago == null)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
				return;
			}

			InvoicePaymentEditForm form = new InvoicePaymentEditForm(this, _entity, Pago, false);
			form.SetReadOnly();
			form.ShowDialog(this);
		}
		
		protected override void EditPagoAction() 
		{
			bool locked = false;

			if (Pago == null)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
				return;
			}

			if (Pago.EEstado == moleQule.Base.EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

			InvoicePaymentEditForm form = new InvoicePaymentEditForm(this, _entity, Pago, locked);
			form.MedioPago_BT.Enabled = false;
			form.ShowDialog(this);

            RefreshAction();

			Refresh();
		}

		protected override void DeletePagoAction()
		{
            ChangeState(moleQule.Base.EEstado.Anulado);
		}

		protected override void UnlockPagoAction()
		{
			ChangeState(moleQule.Base.EEstado.Abierto);
		}

		protected override void LockPagoAction()
		{
			ChangeState(moleQule.Base.EEstado.Contabilizado);
		}
		
		protected override void PrintPagoAction() 
		{
			if (Pago == null) return;

			PaymentReportMng reportMng = new PaymentReportMng(AppContext.ActiveSchema);

			PaymentRpt report = reportMng.GetDetailReport(Pago, _entity, _invoices);

			ShowReport(report);
		}
        
		#endregion

        #region Events

        void Datos_Pago_CurrentChanged(object sender, EventArgs e)
        {
            List<InputInvoiceInfo> lista = new List<InputInvoiceInfo>();

            if (Datos_Pago.Current == null)
            {
                Datos_Factura.DataSource = lista;
                return;
            }

            foreach (TransactionPayment item in Pago.Operations)
            {
                InputInvoiceInfo exp = _invoices.GetItem(item.OidOperation);
                lista.Add(exp);
            }

            Datos_Factura.DataSource = lista;
        }

        private void Datos_Factura_DataSourceChanged(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Facturas_DGW.Name);
        }

        #endregion
    }
}

