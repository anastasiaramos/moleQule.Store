using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InputDeliveryLineAddForm : InputDeliveryLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "InputDeliveryLineAddForm";
		public static Type Type { get { return typeof(InputDeliveryLineAddForm); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        #endregion
        
        #region Factory Methods

		public InputDeliveryLineAddForm(InputDelivery delivery, SerieInfo serie, IAcreedorInfo provider, Form parent)
            : base(null, delivery, serie, provider, parent) 
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Layout 

        public override void FormatControls()
        {
            if (Products_DGW == null) return;

            SetStockControl(false);

            base.FormatControls();
        }
        
        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _entity = InputDeliveryLine.NewChild(_delivery);
            _entity.PImpuestos = _serie.PImpuesto;
			PgMng.Grow();

            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        protected override void UpdateLine()
        {
            if (_product == null) return;

            base.UpdateLine();

            _entity.SetTipoFacturacion(_provider, _product); 
            _entity.Purchase(_delivery, _serie, _provider, _product);                       
        }

        private void AddProducto()
        {
            _delivery.Purchase(_product, _entity);
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
			AddProducto();
        }

        protected virtual void SelectBatchAction()
        {
            BatchList list = BatchList.GetByProductStockList(_product.Oid, _provider.OidAcreedor, _provider.ETipoAcreedor, false);

            BatchSelectForm form = new BatchSelectForm(this, _serie, list);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                 BatchInfo batch = form.Selected as BatchInfo;
                _entity.CopyFrom(_delivery, batch);

                SetStore(StoreInfo.Get(_entity.OidAlmacen, false, true));
                SetExpedient(ExpedientInfo.Get(_entity.OidExpediente, false, true));
           }
        }

        protected override void SelectProductAction()
        {
            ProductList list = ProductList.GetListBySerie(_serie.Oid, false, true);
            ProductSelectForm form = new ProductSelectForm(this, list);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if ((form.Selected as ProductInfo).IsKit)
                {
                    PgMng.ShowInfoException(Library.Store.Resources.Messages.KIT_ENTRY_NOT_ALLOWED);
                    return;
                }  

                _product = ProductInfo.Get((form.Selected as ProductInfo).Oid, false); 
                Products_BS.DataSource = _product;

                _entity.CopyFrom(_delivery, _product);

                if (_product.NoStockSale)
                {
                    SetStockControl(false);
                    SetStore(null);
                }
                else
                {
                    if (_delivery.Rectificativo)
                    {
                        SelectBatchAction();
                        SetStockControl(false);
                    }
                    else
                    {
                        SetStockControl(true);
                        SetStore(_delivery_store);
                        SetExpedient(_delivery_expedient);
                    }
                }

                Kilos_NTB.Text = _entity.CantidadKilos.ToString("N2");
				Pieces_NTB.Text = _entity.CantidadBultos.ToString("N2");

				SetPrice();

				if (_entity.FacturacionBulto)
					Pieces_NTB.Focus();
				else
					Kilos_NTB.Focus();

               SaleMethod_BT.Enabled = (_product.ETipoFacturacion != ETipoFacturacion.Unitaria);
            }
        }

        #endregion
	}
}