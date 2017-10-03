using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ExpenseViewForm : ExpenseForm
	{
        #region Business Methods

        /// <summary>
        /// Se trata de la Gasto actual y que se va a editar.
        /// </summary>
        private ExpenseInfo _entity;

        public override ExpenseInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public ExpenseViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = ExpenseInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            //this.Size = new System.Drawing.Size(833, 325);
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			Bar.Grow();

			base.RefreshMainData();
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
	}
}