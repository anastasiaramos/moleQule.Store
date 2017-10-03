using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class AddStockInputForm : Skin01.InputSkinForm
	{

		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "AddStockInputForm";
        public static Type Type { get { return typeof(AddStockInputForm); } }

        private Stock _entity;
        private Expedient _expedient;
		private ExpedientInfo _entry_expedient = null;

        public Stock Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        public Batch PartidaActual { get { return Datos_Partidas.Current as Batch; } }

        #endregion

        #region Factory Methods

        public AddStockInputForm(Expedient exp)
            : this(true, exp) {}

        public AddStockInputForm(bool isModal, Expedient exp)
			: base(isModal)
        {
            InitializeComponent();
            _expedient = exp;
            _entity = _expedient.Stocks.NewItem(exp);
            _entity.ETipoStock = ETipoStock.Merma;
            SetFormData();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            if (Partidas_DGW == null) return;

            base.FormatControls();
            
            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Producto.Tag = 0.5;
			Observaciones.Tag = 0.5;

            cols.Add(Producto);
			cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Partidas_DGW, cols);
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;

            Datos_Partidas.DataSource = _expedient.Partidas;
            _entity.CopyFrom(PartidaActual, ETipoStock.Merma);

            Tipo_CB.SelectedValue = (long)_entity.ETipoStock;
        }

		public override void RefreshSecondaryData()
		{
			ETipoStock[] list = { ETipoStock.Merma, ETipoStock.MovimientoSalida, ETipoStock.Consumo };
			Datos_Tipo.DataSource = moleQule.Store.Structs.EnumText<ETipoStock>.GetList(list, false);
			Tipo_CB.SelectedValue = (long)ETipoStock.Merma;
		}

        #endregion

        #region Actions

        protected override void SubmitAction()
        {
            if (Datos_Partidas.Current == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);

                _action_result = DialogResult.Ignore;
                return;
            }

			_entity.SetSignoStock();

			if (Bultos_CkB.Checked)
			{
				if (_entity.Bultos < 0)
				{
					if (Decimal.Round(PartidaActual.StockBultos, 2) + Decimal.Round(_entity.Bultos, 2) < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.BULTOS_INSUFICIENTES + " " + PartidaActual.StockBultos.ToString("N2"));
						_action_result = DialogResult.Ignore;
						return;
					}
				}
			}
			else
			{
				if (_entity.Kilos < 0)
				{
					if (Decimal.Round(PartidaActual.StockKilos, 2) + Decimal.Round(_entity.Kilos, 2) < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.STOCK_INSUFICIENTE + " " + PartidaActual.StockKilos.ToString("N2"));
						_action_result = DialogResult.Ignore;
						return;
					}
				}
			}

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

				case ETipoStock.MovimientoEntrada:
					msg = "Para dar de alta stock de otro expediente debe hacerlo realizando un Movimiento de Salida desde el expediente en cuestión";
					break;

				case ETipoStock.MovimientoSalida:
					{
						if (_entry_expedient == null)
						{
							PgMng.ShowInfoException("No ha seleccionado el expediente de recepción de la mercancía");

							_action_result = DialogResult.Ignore;
							return;
						}

                        try
                        {
                            PgMng.Reset(4, 1, Face.Resources.Messages.SAVING, this);

                            _entity.Observaciones = String.Format(Library.Store.Resources.Messages.SALIDA_POR_MOVIMIENTO, _entry_expedient.Codigo);

                            PgMng.Grow();

                            _expedient.UpdateStocks(PartidaActual, true);
                            PgMng.Grow();

                            //_expediente.ApplyEdit();
                            //_expediente.SaveAsChild();
                            PgMng.Grow();
                                
                            _expedient.AddMovimientoStock(ETipoStock.MovimientoSalida, _entity, _entry_expedient, _expedient.SessionCode, true);
                            _action_result = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
						finally
						{
							PgMng.FillUp();
						}
						return;
					}

				case ETipoStock.AltaKit:
				case ETipoStock.BajaKit:
					msg = "Las entradas y salidas de Stock de este tipo deben realizarse mediante Mezclas";
                    break;

                case ETipoStock.Consumo:
                    {
                        if (_entry_expedient == null)
                        {
                            PgMng.ShowInfoException("No ha seleccionado el expediente de recepción de la mercancía");

                            _action_result = DialogResult.Ignore;
                            return;
                        }

                        try
                        {
                            PgMng.Reset(4, 1, Face.Resources.Messages.SAVING, this);

                            _entity.Observaciones = String.Format(Library.Store.Resources.Messages.SALIDA_POR_MOVIMIENTO, _entry_expedient.Codigo);

                            PgMng.Grow();

                            _expedient.UpdateStocks(PartidaActual, true);
                            PgMng.Grow();

                            //_expediente.ApplyEdit();
                            //_expediente.SaveAsChild();
                            PgMng.Grow();

                            _expedient.AddMovimientoStock(ETipoStock.Consumo, _entity, _entry_expedient, _expedient.SessionCode, true);
                            _action_result = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            PgMng.FillUp();
                        }
                        return;
                    }
			}

			if (msg != string.Empty)
			{
				PgMng.ShowInfoException(msg);
				
				_action_result = DialogResult.Ignore;
				return;
			}

            _expedient.UpdateStocks(PartidaActual, true);

            _action_result = DialogResult.OK;
        }

        protected override void CancelAction()
        {
            _expedient.Stocks.Remove(_entity.Oid);
            
            _action_result = DialogResult.Cancel;
        }

        #endregion

		#region Buttons

		private void Expediente_BT_Click(object sender, EventArgs e)
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entry_expedient = form.Selected as ExpedientInfo;
				Expediente_TB.Text = _entry_expedient.Codigo;
			}
		}

		#endregion

		#region Events

		private void Tipo_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
            _entity.Tipo = (long)Tipo_CB.SelectedValue;
			_entity.SetSignoStock();
			Expediente_BT.Enabled = ((long)Tipo_CB.SelectedValue == (long)ETipoStock.MovimientoSalida
                || (long)Tipo_CB.SelectedValue == (long)ETipoStock.Consumo);
		}

        private void Bultos_NTB_Validated(object sender, EventArgs e)
        {
			_entity.UpdateUnidades(PartidaActual);        
        }

        private void Kilos_NTB_Validated(object sender, EventArgs e)
        {
			_entity.UpdateBultos(PartidaActual);
        }

		private void Partidas_DGW_SelectionChanged(object sender, EventArgs e)
		{
			if (Partidas_DGW.CurrentRow == null) return;

            Batch item = Partidas_DGW.CurrentRow.DataBoundItem as Batch;

			_entity.CopyFrom(item, _entity.ETipoStock, _expedient);			
		}

		#endregion

    }
}

