using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class InputInvoiceViewForm : InputInvoiceForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 4; } }
		        
        protected InputInvoiceInfo _entity;

        public override InputInvoiceInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

		public InputInvoiceViewForm()
			: this(-1, ETipoAcreedor.Todos, null) {}

        public InputInvoiceViewForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

		public InputInvoiceViewForm(InputInvoiceInfo factura, Form parent)
			: base(-1, factura, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];
            _entity = InputInvoiceInfo.Get(oid, tipo, true);
        }

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = (InputInvoiceInfo)parameters[0];
			if (_entity.Conceptos == null) _entity.LoadChilds(typeof(InputInvoiceLine), false);
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);

            Otros_GB.Enabled = true;
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;

			base.FormatControls();
        }

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
			Datos_Lineas.DataSource = _entity.Conceptos;
            PgMng.Grow();

            if (_entity.OidAcreedor > 0)
                Datos_Emisor.DataSource = ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor, true);
			PgMng.Grow();

			if (_entity.OidSerie > 0)
			{
				SerieInfo serie = SerieInfo.Get(_entity.OidSerie, false);
				Serie_TB.Text = serie.Nombre;
			}
			PgMng.Grow();

            DiasPago_NTB.Text = _entity.DiasPago.ToString();
            Fecha_DTP.Value = _entity.Fecha;
			FechaRegistro_DTP.Value = _entity.FechaRegistro;
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();

            base.RefreshMainData();
        }
		
        #endregion

		#region Validation & Format

		#endregion

        #region Actions

		protected override void SaveAction() { _action_result = DialogResult.OK; }

        protected override void ShowAlbaranAction()
        {
            InputInvoiceLineInfo concepto = Datos_Lineas.Current as InputInvoiceLineInfo;

            if (concepto != null)
            {
                InputDeliveryLineInfo c_albaran = InputDeliveryLineInfo.Get(concepto.OidConceptoAlbaran, false);
                InputDeliveryViewForm form = new InputDeliveryViewForm(c_albaran.OidAlbaran, ETipoAcreedor.Todos, this);
                form.ShowDialog(this);
            }
        }

        #endregion
	}
}

