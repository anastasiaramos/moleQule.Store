using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ToolForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public virtual Tool Entity { get { return null; } set { } }
		public virtual ToolInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public ToolForm()  
			: this(-1, null) {}

        public ToolForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        #endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
		}

		#endregion

		#region Validation & Format

		#endregion

        #region Buttons

        #endregion

        #region Events

        #endregion
    }
}

