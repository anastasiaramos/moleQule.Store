using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputDeliveryLineUIForm : Skin01.InputSkinForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		protected InputDeliveryLine _entity;
		protected InputDelivery _delivery;
		protected ProductInfo _product;
		protected ProductoProveedorInfo _ppv;
		protected SerieInfo _serie;
		protected IAcreedorInfo _provider;
		protected ExpedientInfo _expedient;
        protected ExpedientInfo _delivery_expedient;
		protected StoreInfo _store;
        protected StoreInfo _delivery_store;

		#endregion

		#region Factory Methods

		/// <summary>
		/// Constructor
		/// </summary>
		protected InputDeliveryLineUIForm()
			: this(null, null, null, null, null) { }

		/// Constructor
		/// </summary>
		public InputDeliveryLineUIForm(InputDeliveryLine line, InputDelivery delivery, SerieInfo serie, IAcreedorInfo provider, Form parent)
			: base(true, parent)
		{
			InitializeComponent();

            _entity = line;
			_delivery = delivery;
			_serie = serie;
			_provider = provider;
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			if (Products_DGW == null) return;

	        SetStockControl(false);

			base.FormatControls();

            SaleMethod_BT.Enabled = (_product == null || _product.ETipoFacturacion != ETipoFacturacion.Unitaria);
		}

		public void SetStockControl(bool control)
		{
			if (control)
			{
                //Solo Permitimos asociar stock si es un proveedor o acreedor
                if (new List<ETipoAcreedor> { 
                        ETipoAcreedor.Proveedor, 
                        ETipoAcreedor.Acreedor 
                    }.Contains(_delivery.ETipoAcreedor))
                {
                    Almacen_TB.Enabled = true;
                    Almacen_BT.Enabled = true;
                    Ubicacion_TB.Enabled = true;
                }
                else
                {
                    Almacen_TB.Enabled = false;
                    Almacen_BT.Enabled = false;
                    Ubicacion_TB.Enabled = false;
                }
			}
			else
			{
				Almacen_TB.Enabled = false;
				Almacen_BT.Enabled = false;
                Ubicacion_TB.Enabled = false;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
            Kilos_NTB.Text = _entity.CantidadKilos.ToString("N5");
			Pieces_NTB.Text = _entity.CantidadBultos.ToString("N2");
		}

        public override void RefreshSecondaryData()
        {
            if (_delivery.OidAlmacen != 0) _delivery_store = StoreInfo.Get(_delivery.OidAlmacen, false, true);
            PgMng.Grow();

            if (_delivery.OidExpediente != 0) _delivery_expedient = ExpedientInfo.Get(_delivery.OidExpediente, false, true);
            PgMng.Grow();
        }

		#endregion

		#region Business Methods

		protected void SetPrice()
		{
			if (_product == null) return;

			_entity.SetPrice(_provider, _product, null);

			Precio_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioProducto_NTB.Text = _product.PrecioCompra.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

			Datos.ResetBindings(false);
		}

		protected void SetStore(StoreInfo store)
		{
			_store = store;

			if (_store != null)
			{
                _entity.OidAlmacen = _store.Oid;
				_entity.IDAlmacen = _store.Codigo;
                _entity.Almacen = _store.Nombre;				
				Almacen_TB.Text = _store.IDAlmacenAlmacen;
			}
			else
			{
                _entity.OidAlmacen = 0;
                _entity.IDAlmacen = string.Empty;
				_entity.Almacen = string.Empty;
				Almacen_TB.Text = string.Empty;

                _entity.Ubicacion = string.Empty;
                Ubicacion_TB.Text = string.Empty;
			}
		}

		protected void SetExpedient(ExpedientInfo expedient)
		{
			_expedient = expedient;

            if (_expedient != null)
            {
                if (_product != null)
                {
                    switch (_expedient.ETipo)
                    {
                        case moleQule.Store.Structs.ETipoExpediente.Ganado:

                            if (_product.ETipoFacturacion != ETipoFacturacion.Unitaria)
                            {
                                PgMng.ShowInfoException(Resources.Messages.NO_UNITS_ALLOWED);
                                return;
                            }

                            break;
                    }
                }

                _entity.OidExpediente = _expedient.Oid;
                _entity.Expediente = _expedient.Codigo;                
            }
            else
            {
                _entity.OidExpediente = 0;
                _entity.Expediente = string.Empty;
            }

            Expediente_TB.Text = _entity.Expediente;
		}

        protected virtual void UpdateLine()
        {
            PrecioProducto_NTB.Text = _product.PrecioCompra.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
            
            PrecioCliente_NTB.Text = (_provider != null)
                        ? _provider.Productos.GetItemByProducto(_entity.OidProducto) != null
                            ? _provider.Productos.GetItemByProducto(_entity.OidProducto).PrecioCompra.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting())
                            : _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting())
                        : _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
        }

		#endregion

		#region Actions

		protected override void SubmitAction()
		{
			if (_product == null)
			{
				PgMng.ShowInfoException(Resources.Messages.SELECT_PRODUCT);

				_action_result = DialogResult.Ignore;
				return;
			}

            if (!_product.NoStockSale && _entity.OidAlmacen == 0)
            {
                PgMng.ShowInfoException(Library.Store.Resources.Messages.STOCK_PRODUCT_WITHOUT_STORE);
                return;
            }

			if (_expedient != null)
			{
				switch (_expedient.ETipo)
				{
					case moleQule.Store.Structs.ETipoExpediente.Ganado:

						if (_product.ETipoFacturacion != ETipoFacturacion.Unitaria)
						{
							PgMng.ShowInfoException(Resources.Messages.NO_UNITS_ALLOWED);
							return;
						}

						break;
				}
			}

            if ((_entity.CantidadKilos == 0) || _entity.CantidadBultos == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_UNIDADES_OR_BULTOS);

				_action_result = DialogResult.Ignore;
				return;
			}

            if (!_product.NoStockSale && (_product.KilosBulto != 1) && (_entity.CantidadKilos == _entity.CantidadBultos))
            {
                if (DialogResult.No == ProgressInfoMng.ShowQuestion(String.Format(Resources.Messages.SAME_PIECES_AND_KILOS, _product.KilosBulto)))
                {
                    _action_result = DialogResult.Ignore;
                    return;
                }
            }

			if (_delivery.Rectificativo)
			{
                if (_entity.CantidadKilos > 0 && _entity.FacturacionPeso)
                    _entity.CantidadKilos = -_entity.CantidadKilos;
                if (_entity.CantidadBultos > 0 && _entity.FacturacionBulto)
                    _entity.CantidadBultos = -_entity.CantidadBultos;
			}
			else
			{
				/*if (_pci != null)
				{
					if (_entity.Precio < _pci.Precio)
					{
						PgMng.ShowInfoException("El precio de venta es inferior al precio de referencia para este cliente.");
					}
				}
				else if (_entity.Precio < _producto.PrecioVenta)
				{
					PgMng.ShowInfoException("El precio de venta es inferior al precio de referencia de este producto.");
				}

				if (_entity.Beneficio <= 0)
				{
					if (PgMng.ShowInfoException(Resources.Messages.AVISO_PERDIDA) == DialogResult.No)
						return;
				}*/
			}

			DoSubmitAction();

			_action_result = DialogResult.OK;
		}

		protected virtual void DoSubmitAction() { }

		protected override void CancelAction()
		{
			if (!IsModal) _entity.CancelEdit();
		}

		protected virtual void SelectExpedientAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpedient((ExpedientInfo)form.Selected);
			}
		}

		protected virtual void SelectProductAction() {}

        protected virtual void SelectSaleMethodAction()
        {
            if (_product != null && _product.ETipoFacturacion == ETipoFacturacion.Unitaria) return;

            SelectEnumInputForm form = new SelectEnumInputForm(true);
            
            form.SetDataSource(moleQule.Common.Structs.EnumText<ETipoFacturacion>.GetList(false, false, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ComboBoxSource tipo_venta = form.Selected as ComboBoxSource;
                _entity.ETipoFacturacion = (ETipoFacturacion)tipo_venta.Oid;
                SaleMethod_TB.Text = _entity.SaleMethodLabel;
            }
        }

        protected virtual void SelectStoreAction()
        {
            AlmacenSelectForm form = new AlmacenSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                SetStore((StoreInfo)form.Selected);
            }
        }

		protected virtual void SelectTaxAction()
		{
			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				_entity.OidImpuesto = source.Oid;
				_entity.PImpuestos = source.Porcentaje;
				_entity.CalculateTotal();
			}
		}

		#endregion

		#region Buttons

		private void Productos_BT_Click(object sender, EventArgs e)
		{
			SelectProductAction();
		}

		private void Impuestos_BT_Click(object sender, EventArgs e)
		{
			SelectTaxAction();
		}

		private void Detalles_BT_Click(object sender, EventArgs e)
		{
			Detalles_GB.Visible = !Gastos_TB.Visible;
		}

		private void Almacen_BT_Click(object sender, EventArgs e) {	SelectStoreAction(); }

		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedientAction(); }

		private void ResetExpediente_BT_Click(object sender, EventArgs e) { SetExpedient(null);	}

        private void SaleMethod_BT_Click(object sender, EventArgs e) { SelectSaleMethodAction(); }

		#endregion

		#region Events

		private void Kilos_NTB_TextChanged(object sender, EventArgs e)
		{
            if (_entity.ETipoFacturacion != ETipoFacturacion.Peso) return;
		
            _entity.CantidadKilos = Kilos_NTB.DecimalValue;
			_entity.BalanceQuantity(_delivery, _product);
			Pieces_NTB.Text = _entity.CantidadBultos.ToString("N2");
		}

		private void Kilos_NTB_Validated(object sender, EventArgs e)
		{
            if (_delivery.Rectificativo)
            {
                if (_entity.CantidadKilos > 0)
                {
                    _entity.CantidadKilos = -_entity.CantidadKilos;
                }
            }

            _entity.CantidadKilos = Kilos_NTB.DecimalValue;
            Kilos_NTB.Text = _entity.CantidadKilos.ToString("N2");
		}

		private void Bultos_NTB_TextChanged(object sender, EventArgs e)
		{
            if (_entity.ETipoFacturacion == ETipoFacturacion.Peso) return;

			_entity.CantidadBultos = Pieces_NTB.DecimalValue;
			_entity.BalanceQuantity(_delivery, _product);
            Kilos_NTB.Text = _entity.CantidadKilos.ToString("N2");
		}

		private void Bultos_NTB_Validated(object sender, EventArgs e)
		{
            if (_delivery.Rectificativo)
            {
                if (_entity.CantidadBultos > 0)
                    _entity.CantidadBultos = -_entity.CantidadBultos;
            }

            _entity.CantidadBultos = Pieces_NTB.DecimalValue;
			Pieces_NTB.Text = _entity.CantidadBultos.ToString("N2");
		}

		private void Products_BS_CurrentChanged(object sender, EventArgs e)
		{
			UpdateLine();
		}

		#endregion
   }
}