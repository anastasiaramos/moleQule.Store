using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.CslaEx;

using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;

namespace moleQule.Face.Store
{
	public partial class ToolUIForm : ToolForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		protected Tool _entity;

        public override Tool Entity { get { return _entity; } set { _entity = value; } }
		public override ToolInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public ToolUIForm()
            : this(-1, null) { }

        public ToolUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

		public ToolUIForm(Tool source)
            : base()
        {
            InitializeComponent();
			_entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        protected override void GetFormSourceData(long oid)
        {
			_entity = Tool.Get(oid);
            _entity.BeginEdit();
        }

        protected override bool SaveObject()
        {
			this.Datos.RaiseListChangedEvents = false;

			Tool temp = _entity.Clone();
			temp.ApplyEdit();

			// do the save
			try
			{
				_entity = temp.Save();
				_entity.ApplyEdit();

				return true;
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			From_DTP.Checked = _entity.EStatus == moleQule.Base.EEstado.Active;
			Till_DTP.Checked = _entity.EStatus == moleQule.Base.EEstado.Baja;
		}

		#endregion

        #region Source
        
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
            PgMng.Grow();
        }

        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {
            Validator.ValidateInt(Code_TB.Text);
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

		#region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Status = estado.Oid;

				From_DTP.Checked = _entity.EStatus == moleQule.Base.EEstado.Active;
				Till_DTP.Checked = _entity.EStatus == moleQule.Base.EEstado.Baja;
			}
		}

		#endregion
    }
}

