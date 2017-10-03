using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ExpenseEditForm : ExpenseUIForm
    {
        #region Factory Methods

        public ExpenseEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
            _entity = Expense.Get(oid, false);
			_entity.BeginEdit();
		}

        #endregion
	}
}