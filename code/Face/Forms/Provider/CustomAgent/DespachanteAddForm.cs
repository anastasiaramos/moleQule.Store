using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class DespachanteAddForm : CustomAgentUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "DespachanteAddForm";
		public new static Type Type { get { return typeof(DespachanteAddForm); } }

		#endregion
		
        #region Factory Methods

        public DespachanteAddForm() 
			: this(null) {}

        public DespachanteAddForm(Form parent)
            : base(-1, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public DespachanteAddForm(Despachante source, Form parent)
            : base(-1, parent)
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData(object[] parameters)
        {
            _entity = Despachante.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Buttons

        #endregion
    }
}
