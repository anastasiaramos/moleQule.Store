using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class LineaFomentoAddForm : LineaFomentoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "LineaFomentoAddForm";
		public new static Type Type { get { return typeof(LineaFomentoAddForm); } }

		#endregion
		
        #region Factory Methods

        public LineaFomentoAddForm() 
			: this(null) { }

        public LineaFomentoAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public LineaFomentoAddForm(LineaFomento source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = LineaFomento.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion
    }
}
