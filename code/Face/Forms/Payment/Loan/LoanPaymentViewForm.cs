using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LoanPaymentViewForm : LoanPaymentForm
    {
        #region Attributes & Properties

        public new const string ID = "LoanPaymentViewForm";
        public new static Type Type { get { return typeof(LoanPaymentViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private PaymentInfo _entity;

        public override PaymentInfo EntityInfo { get { return _entity; } }
        public LoanInfo _loan = null;
        public LoanList _loans = null;

		#endregion
		
        #region Factory Methods

        public LoanPaymentViewForm() 
			: this(-1, ETipoPago.Prestamo, null) {}

        public LoanPaymentViewForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[2] { tipo, null }, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            ETipoPago tipo = ETipoPago.Prestamo;

            if (parameters[1] != null) _loan = (LoanInfo)parameters[1];

            _entity = PaymentInfo.Get(oid, tipo, true);
            _mf_type = ManagerFormType.MFView;


            _loans = _loan != null ?
                    LoanList.NewList(_loan)
                    : LoanList.GetByPagoAndPendientesList(_entity);

            //if (_prestamo != null)
            {
                PaymentList pagos = PaymentList.GetListByPrestamo(_loans[0], false);

                TransactionPaymentInfo pf = _entity.Operations != null ? _entity.Operations.GetItemByFactura(_loans[0].Oid) : null;
                
                if (pf != null)
                {
                    _loans[0].Asignado = pf.Cantidad;
                    _loans[0].TotalPagado = 0;

                    foreach (PaymentInfo pago in pagos)
                    {
                        if (pago.EEstado == moleQule.Base.EEstado.Anulado) continue;

                        _loans[0].TotalPagado += pago.Importe;
                    }
                }

                _loans[0].Pendiente = _loans[0].Importe - _loans[0].TotalPagado + _loans[0].Asignado;
                _loans[0].PendienteAsignar = _loans[0].Pendiente - _loans[0].Asignado;
            }
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Datos_Lineas.DataSource = _loans.GetSortedList();
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			Vencimiento_DTP.Value = _entity.Vencimiento;

            base.RefreshMainData();
        }
		
        #endregion

        #region Print

        #endregion

		#region Business Methods

		protected override void UpdateAllocated()
		{
			decimal _asignado = 0;

            SortedBindingList <LoanInfo> prestamos = Datos_Lineas.DataSource as SortedBindingList<LoanInfo>;

			foreach (LoanInfo item in prestamos)
				_asignado += item.Asignado;

			if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
			{
				_deallocated = _entity.Importe - _asignado;

				if (_entity.Importe >= 0) _deallocated = (_deallocated < 0) ? 0 : _deallocated;
				else _deallocated = (_deallocated > 0) ? 0 : _deallocated;
			}
			else
			{
				_deallocated = -_asignado;
				_entity.Importe = _asignado;
			}

			NoAsignado_TB.Text = _deallocated.ToString("N2");
			MarkControl(NoAsignado_TB);
		}

		#endregion

		#region Actions

		/// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
			DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Events

		private void PagoGastoViewForm_Shown(object sender, EventArgs e)
		{
			UpdateAllocated();
		}

        #endregion
    }
}