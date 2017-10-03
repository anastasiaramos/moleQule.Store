using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ExpenseAddForm : ExpenseUIForm
    {
        #region Factory Methods

        public ExpenseAddForm() 
			: this(null, ECategoriaGasto.Otros) {}

		public ExpenseAddForm(Form parent, ECategoriaGasto categoria)
			: base(-1, parent, categoria)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

        public ExpenseAddForm(Expense entity, Form parent) 
			: base(entity, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			if (parameters[0] == null)
			{
				ECategoriaGasto categoria = (ECategoriaGasto)parameters[1];

                _entity = Expense.New();
				_entity.BeginEdit();
				_entity.ECategoriaGasto = (categoria != ECategoriaGasto.GeneralesExpediente) ? categoria : ECategoriaGasto.Otros;
			}
			else
			{
                _entity = (Expense)parameters[0];
				_entity.BeginEdit();
			}
		}

		#endregion

		#region Buttons

		#endregion	
	}
}