using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class WorkReportViewForm : WorkReportForm
	{
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        private WorkReportInfo _entity;

        public override WorkReportInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public WorkReportViewForm(long oid, Form parent)
            : base(oid, null, true, parent)
        {
            InitializeComponent();
			SetFormData();
            this.Text = String.Format(Library.Store.Resources.Labels.WORK_REPORT_TITLE, _entity.Code);
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
			_entity = WorkReportInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

		public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            this.Otros_GB.Enabled = true;
            foreach (Control ctl in Otros_GB.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "TextBox":
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = false;
                        break;
                    case "ComboBox":
                        ((ComboBox)ctl).Enabled = true;
                        break;
                    case "Button":
                        ((Button)ctl).Enabled = true;
                        break;
                    default:
                        ctl.Enabled = true;
                        break;
                }
            }

            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;

			base.FormatControls();
        }

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Lines_BS.DataSource = _entity.Lines;
            PgMng.Grow();

            Date_DTP.Value = _entity.Date;
			From_DTP.Value = _entity.From;
			Till_DTP.Value = _entity.Till;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			/*if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();*/

			if (_entity.OidExpedient != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpedient, false));
			PgMng.Grow();

			base.RefreshSecondaryData();
		}

        #endregion

		#region Business Methods

		protected void SetAlmacen(StoreInfo source)
		{
			if (source == null) return;

			_almacen = source;

			//Almacen_TB.Text = _entity.IDAlmacenAlmacen;
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			if (source == null) return;

			_expedient = source;

			Expediente_TB.Text = _expedient.Codigo;
		}

		#endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion
	}
}