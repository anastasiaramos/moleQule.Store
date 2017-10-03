using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputDeliveryViewForm : InputDeliveryForm
	{
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        private InputDeliveryInfo _entity;

        public override InputDeliveryInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public InputDeliveryViewForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, new object[1] { tipo }, true, parent)
        {
            InitializeComponent();
			SetFormData();
            this.Text = _entity.Codigo;
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];

			_entity = InputDeliveryInfo.Get(oid, tipo, true);
			_deliveryType = _entity.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos;
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

            Rectificativo_CKB.Enabled = false;
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            IDManual_CkB.Visible = false;

			base.FormatControls();
        }

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Lines_BS.DataSource = _entity.ConceptoAlbaranes;
            PgMng.Grow();

            DiasPago_TB.Text = _entity.DiasPago.ToString();
            Fecha_DTP.Value = _entity.Fecha;
			FechaRegistro_DTP.Value = _entity.FechaRegistro;
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidAcreedor != 0) SetAcreedor(ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor, false));
			PgMng.Grow();

			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false));
			PgMng.Grow();

			if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();

			if (_entity.OidExpediente != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpediente, false));
			PgMng.Grow();

			base.RefreshSecondaryData();
		}

        protected override void HideComponentes()
        {
            foreach (DataGridViewRow row in Lines_DGW.Rows)
				if ((row.DataBoundItem as InputDeliveryLineInfo).IsKitComponent)
                    row.Visible = false;
        }

        #endregion

		#region Business Methods

		protected void SetAcreedor(IAcreedorInfo source)
		{
			if (source == null) return;

			_provider = source;

			Providers_BS.DataSource = _provider;
		}

		protected void SetAlmacen(StoreInfo source)
		{
			if (source == null) return;

			_store = source;

			Almacen_TB.Text = _entity.IDAlmacenAlmacen;
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			if (source == null) return;

			_expedient = source;

			Expediente_TB.Text = _expedient.Codigo;
		}

		protected void SetSerie(SerieInfo source)
		{
			if (source == null) return;

			_serie = source;

			Serie_TB.Text = _serie.Nombre;
			Observaciones_TB.Text = _serie.Cabecera;
		}

		#endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion
	}
}

