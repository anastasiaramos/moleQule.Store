using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollbatchAddForm : PayrollBatchUIForm
    {
        #region Attributes & Properties

		public const string ID = "PayrollbatchAddForm";
		public static Type Type { get { return typeof(PayrollbatchAddForm); } }

		#endregion
		
        #region Factory Methods

        public PayrollbatchAddForm() 
			: this(null) { }

        public PayrollbatchAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
			_entity = PayrollBatch.New();
			_entity.BeginEdit();
        }

        #endregion
	}
}
