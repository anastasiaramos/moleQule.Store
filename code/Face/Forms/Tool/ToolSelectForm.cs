using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ToolSelectForm : ToolMngForm
	{
		#region Factory Methods

		public ToolSelectForm(Form parent)
			: this(parent, null) { }

		public ToolSelectForm(Form parent, ToolList list)
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
