using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class EditStockActionForm : Skin01.ActionSkinForm
    {

        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "EditStockActionForm";
        public static Type Type { get { return typeof(EditStockActionForm); } }

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
        private Stock _entity;
        private Expedient _expedient;
        private Batch _current_batch;
        
        public Stock Entity { get { return _entity; } }

		#endregion

        #region Factory Methods

        public EditStockActionForm(Stock stock, Expedient exp)
            : this(true, stock, exp) { }

        public EditStockActionForm(bool IsModal, Stock stock, Expedient exp)
            : base(IsModal)
        {
            InitializeComponent();
            _entity = stock;
            _expedient = exp;
            SetFormData();
            this.Text = Resources.Labels.STOCK_EDIT_TITLE;
        }

        #endregion

        #region Source

		protected override void RefreshMainData()
        {
            _current_batch = _expedient.Partidas.GetItem(_entity.OidPartida);

            Datos.DataSource = _entity;
        }

		public override void RefreshSecondaryData()
		{
			ETipoStock[] list = { ETipoStock.Merma, ETipoStock.Consumo };
			Datos_Tipo.DataSource = moleQule.Store.Structs.EnumText<ETipoStock>.GetList(list, false);
			Tipo_CB.SelectedItem = (long)ETipoStock.Merma;
		}

        #endregion

        #region Buttons

        protected override void SubmitAction()
        {
			string msg = string.Empty;

			switch (_entity.ETipoStock)
			{
				case ETipoStock.Todos:
					msg = "Seleccione un tipo de Movimiento de Stock";
					break;

				case ETipoStock.Compra:
					msg = "Las entradas de Stock por Compra deben realizarse mediante un Albarán Recibido";
					break;

				case ETipoStock.Venta:
					msg = "Las salidas de Stock por Venta deben realizarse mediante un Albarán Emitido";
					break;

				case ETipoStock.AltaKit:
				case ETipoStock.BajaKit:
					msg = "Las entradas y salidas de Stock de este tipo deben realizarse mediante Mezclas";
					break;
			}

			if (msg != string.Empty)
			{
				PgMng.ShowInfoException(msg);

				_action_result = DialogResult.Ignore;
				return;
			}

            _entity.SetSignoStock();

            /*if (_entity.Bultos < 0)
            {
                if (Decimal.Round(_producto_actual.StockBultos, 2) + Decimal.Round(_entity.Bultos, 2) < 0)
                {
                    PgMng.ShowInfoException(Resources.Messages.BULTOS_INSUFICIENTES + " " + _producto_actual.StockBultos.ToString("N2"));

                    _action_result = DialogResult.Ignore;
                    return;
                }
            }

            if (_entity.Kilos < 0)
            {
                if (Decimal.Round(_producto_actual.StockKilos, 2) + Decimal.Round(_entity.Kilos, 2) < 0)
                {
					PgMng.ShowInfoException(Resources.Messages.STOCK_INSUFICIENTE + " " + _producto_actual.StockKilos.ToString("N2"));
                    
                    _action_result = DialogResult.Ignore;
                    return;
                }
            }*/

            _expedient.UpdateStocks(_current_batch, true);

            _action_result = DialogResult.OK;
        }

        protected override void CancelAction()
        {
            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void Bultos_BT_Validated(object sender, EventArgs e)
        {
			_entity.UpdateUnidades(_current_batch);
        }

        private void Kilos_BT_Validated(object sender, EventArgs e)
        {
			_entity.UpdateBultos(_current_batch);
        }

		private void Tipo_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			_entity.SetSignoStock();
		}

        #endregion
    }
}