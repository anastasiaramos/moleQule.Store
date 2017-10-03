using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollBatchViewForm : PayrollBatchForm
    {
        #region Attributes & Properties

		public const string ID = "PayrollBatchViewForm";
		public static Type Type { get { return typeof(PayrollBatchViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        private PayrollBatchInfo _entity;

        public override PayrollBatchInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public PayrollBatchViewForm(long oid_remesa, long oid_nomina) 
			: this(oid_remesa, oid_nomina, null) {}

        public PayrollBatchViewForm(long oid_remesa, Form parent)
            : this(oid_remesa, 0, parent) { }

        public PayrollBatchViewForm(long oid_remesa, long oid_nomina, Form parent)
            : base(oid_remesa, oid_nomina, true, parent)
        {
            InitializeComponent();
            _oid_nomina = oid_nomina;
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = PayrollBatchInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		protected override void SetGridFormat()
		{
			foreach (DataGridViewRow row in Payrolls_DGW.Rows)
			{
				if (row.IsNewRow) return;

				NominaInfo item = (NominaInfo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();


            if (_oid_nomina == 0)
                Payrolls_BS.DataSource = _entity.Nominas;
            else
            {
                FCriteria criteria = new FCriteria<long>("Oid", _oid_nomina, Operation.Equal);
                Payrolls_BS.DataSource = _entity.Nominas.GetSubList(criteria);
            }
			PgMng.Grow();
			
            base.RefreshMainData();
        }
		
        #endregion

        #region Actions

        protected override void SaveAction() { DialogResult = DialogResult.Cancel; }

        #endregion
    }
}
