using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TipoGastoAddForm : TipoGastoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TipoGastoAddForm";
		public new static Type Type { get { return typeof(TipoGastoAddForm); } }

		#endregion
		
        #region Factory Methods

        public TipoGastoAddForm() 
			: this(null) { }

        public TipoGastoAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = TipoGasto.New();
            _entity.BeginEdit();
        }

        #endregion
    }
}
