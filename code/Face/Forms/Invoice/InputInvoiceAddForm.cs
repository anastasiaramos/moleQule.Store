using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class InputInvoiceAddForm : InputInvoiceUIForm, IBackGroundLauncher
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        private InputDeliveryInfo _albaran = null;

		#endregion

        #region Factory Methods

        public InputInvoiceAddForm(Form parent) 
			: base(parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		public InputInvoiceAddForm(InputDeliveryInfo albaran, IAcreedorInfo emisor, Form parent)
			: base(new object[2] { albaran, emisor }, parent)
		{
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
            SetEmisor(emisor);
            SetSerie(SerieInfo.Get(albaran.OidSerie, false), true);
            _albaran = albaran;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = InputInvoice.New();
			_entity.BeginEdit();

			try
			{
				//NO SE PUEDE HACER AQUI LO QUE SE HACE EN EL CONSTRUCTOR PORQUE LOS CONTROLES NO ESTAN INICIALIZADOS
				if (parameters[0] != null) _entity.Fecha = ((InputDeliveryInfo)parameters[0]).Fecha;
			}
			catch { }
		}

		#endregion

		#region Actions
        
		private bool AddAlbaran(InputDeliveryInfo albaran)
		{
			List<InputDeliveryInfo> list = new List<InputDeliveryInfo>();
			list.Add(albaran);

			_results = list;

			DoAddAlbaran(null);

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
				Datos.ResetBindings(false);
			}

			return false;
		}

		#endregion

		#region Events

		private void Rectificativa_CkB_CheckedChanged(object sender, EventArgs e)
		{
			_entity.Rectificativa = Rectificativo_CkB.Checked;
			_entity.GetNewCode();
		}

        private void FacturaRecibidaAddForm_Shown(object sender, EventArgs e)
        {
            if (_albaran != null)
            {
                AddAlbaran(_albaran);
                RefreshLineas();
            }
        }

		#endregion
	}
}

