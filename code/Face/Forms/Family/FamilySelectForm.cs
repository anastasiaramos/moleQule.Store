using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamilySelectForm : FamilyMngForm
	{
		#region Factory Methods

		public FamilySelectForm(Form parent)
			: this(parent, null) { }

		public FamilySelectForm(Form parent, FamiliaList list)
			: base(true, parent, list)
		{
			InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

		#endregion
	}
}