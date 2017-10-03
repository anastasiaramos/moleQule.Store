using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class LineaFomentoEditForm : LineaFomentoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "LineaFomentoEditForm";
		public new static Type Type { get { return typeof(LineaFomentoEditForm); } }

		#endregion
		
        #region Factory Methods

        public LineaFomentoEditForm(long oid)
            : this(oid, null) {}

        public LineaFomentoEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = LineaFomento.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion

    }
}
