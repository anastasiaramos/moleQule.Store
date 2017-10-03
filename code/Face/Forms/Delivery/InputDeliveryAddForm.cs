using System;
using System.Collections.Generic;
using System.Windows.Forms;

using moleQule;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputDeliveryAddForm : InputDeliveryUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        #endregion

        #region Factory Methods

        public InputDeliveryAddForm() 
			: this(null, ETipoAlbaranes.Todos) {}

		public InputDeliveryAddForm(IAcreedorInfo acreedor, PedidoProveedorInfo pedido, Form parent)
			: this(parent, ETipoAlbaranes.Todos)
		{
			SetProvider(acreedor);
			SetSerie(SerieInfo.Get(pedido.OidSerie, false), true);
			AddPedidoAction(pedido);
		}

		public InputDeliveryAddForm(Form parent, ETipoAlbaranes tipo)
			: this(new object[2] { null, tipo }, parent) {}

		public InputDeliveryAddForm(InputDelivery entity, Form parent)
			: this(new object [1] { entity }, parent) {}

		public InputDeliveryAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

        public override void DisposeForm()
        {
            base.DisposeForm();
        }

		protected override void GetFormSourceData(object[] parameters)
		{
			if (parameters[0] == null)
			{
				_deliveryType = (ETipoAlbaranes)parameters[1];
				_entity = InputDelivery.New(_deliveryType);
				_entity.BeginEdit();
			}
			else
			{
				_entity = (InputDelivery)parameters[0];
				_entity.BeginEdit();
			}
		}

		#endregion

        #region Business Methods

        protected override void SetProvider(IAcreedorInfo source)
        {
            if (source == null) return;

            base.SetProvider(source);

			if (_entity.Conceptos.Count == 0)
				_entity.AddProductosAcreedor(_provider, _serie);

            Lines_BS.ResetBindings(false);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
		{
			base.RefreshMainData();

			if (_provider != null)
			{
				_entity.DiasPago = _provider.DiasPago;
				DiasPago_TB.Text = _entity.DiasPago.ToString("00");
			}
		}

        #endregion	

		#region Actions
        
		private void AddPedidoAction(PedidoProveedorInfo albaran)
		{
			List<PedidoProveedorInfo> list = new List<PedidoProveedorInfo>();
			list.Add(albaran);

			AddOrderAction(list);
		}

        protected override void RectificativoAction()
        {
            _entity.Rectificativo = Rectificativo_CKB.Checked;
            if (_entity.Rectificativo) _entity.Contado = false;

            _entity.GetNewCode(_deliveryType);

            _entity.CalculateTotal();
        }

		#endregion	
    }
}