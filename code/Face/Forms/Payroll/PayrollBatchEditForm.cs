using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollBatchEditForm : PayrollBatchUIForm
    {
        #region Attributes & Properties
		
        public const string ID = "PayrollBatchEditForm";
		public static Type Type { get { return typeof(PayrollBatchEditForm); } }

		#endregion
		
        #region Factory Methods

        public PayrollBatchEditForm(long oid_remesa, long oid_nomina)
            : this(oid_remesa, oid_nomina, null) {}

        public PayrollBatchEditForm(long oid_remesa, Form parent)
            : this(oid_remesa, 0, parent) { }

        public PayrollBatchEditForm(long oid_remesa, long oid_nomina, Form parent)
            : base(oid_remesa, oid_nomina, parent)
        {
            InitializeComponent();
            _oid_nomina = oid_nomina;
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = PayrollBatch.Get(oid);
            _entity.BeginEdit();
        }

        #endregion
	}
}
