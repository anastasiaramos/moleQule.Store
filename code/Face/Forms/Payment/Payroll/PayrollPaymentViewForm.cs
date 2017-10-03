using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PayrollPaymentViewForm : PayrollPaymentForm
    {
        #region Attributes & Properties

        public new const string ID = "PayrollPaymentViewForm";
		public new static Type Type { get { return typeof(PayrollPaymentViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private PaymentInfo _entity;

        public override PaymentInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public PayrollPaymentViewForm() 
			: this(-1, ETipoPago.Todos, null) {}

        public PayrollPaymentViewForm(long oid, ETipoPago tipo, Form parent)
            : base(oid, new object[1] { tipo }, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoPago tipo = (ETipoPago)parameters[0];

            _entity = PaymentInfo.Get(oid, tipo, true);
            _mf_type = ManagerFormType.MFView;
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

			Datos_Lineas.DataSource = PayrollList.GetByPagoList(_entity, false).GetSortedList();
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			Vencimiento_DTP.Value = _entity.Vencimiento;

            base.RefreshMainData();
        }
		
        #endregion

		#region Business Methods

		protected override void UpdateAllocated()
		{
			decimal _asignado = 0;

            SortedBindingList<NominaInfo> gastos = Datos_Lineas.DataSource as SortedBindingList<NominaInfo>;

			foreach (NominaInfo item in gastos)
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

		private void PagoNominaViewForm_Shown(object sender, EventArgs e)
		{
			UpdateAllocated();
		}

        #endregion
    }
}