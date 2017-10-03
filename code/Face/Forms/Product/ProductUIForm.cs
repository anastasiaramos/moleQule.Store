using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	public partial class ProductUIForm : ProductForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Product _entity;

		public override Product Entity { get { return _entity; } set { _entity = value; } }
		public override ProductInfo EntityInfo	{ get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        public ProductUIForm() 
			: this(null) {}

		public ProductUIForm(Form parent)
			: this(-1, parent) { }

        public ProductUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

		public ProductUIForm(Product product, Form parent)
            : base(parent)
        {
            InitializeComponent();
            _entity = product.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Product temp = _entity.Clone();
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

			Codigo_TB.ReadOnly = Library.Store.ModulePrincipal.GetCodigoProductoAutomaticoSetting();
		}

		#endregion

		#region Source

        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
            
			Datos_Components.DataSource = _entity.Components;
			PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected void SetFamiliaAction()
        {
            FamilySelectForm form = new FamilySelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                FamiliaInfo item = (FamiliaInfo)form.Selected;
                _entity.OidFamilia = item.Oid;
                _entity.Familia = item.Nombre;
                _entity.CodigoFamilia = item.Codigo.ToString();
                _entity.AvisarBeneficioMinimo = item.AvisarBeneficioMinimo;
                _entity.PBeneficioMinimo = item.PBeneficioMinimo;

                Datos.ResetBindings(false);
            }
        }

		protected void SetEstadoAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
				Estado_TB.Text = _entity.EstadoLabel;
			}
		}

		protected void SetSaleMethodAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			form.SetDataSource(moleQule.Common.Structs.EnumText<ETipoFacturacion>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo_venta = form.Selected as ComboBoxSource;
				_entity.ETipoFacturacion = (ETipoFacturacion)tipo_venta.Oid;
				FormaVenta_TB.Text = _entity.TipoFacturacionLabel;
			}
		}

		protected override void AddComponentAction() 
		{
			ProductList list = ProductList.GetKitList(false, false);
			ProductSelectForm form = new ProductSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo item = form.Selected as ProductInfo;

				if (_entity.Components.GetItemByComponent(item.Oid) == null)
					_entity.Components.NewItem(_entity, item);

				Datos_Components.ResetBindings(false);
			}
		}

		protected override void DeleteComponentAction() 
		{
            if (Datos_Components.Current == null) return;

			Kit item = Datos_Components.Current as Kit;

			_entity.Components.Remove(item);
			Datos_Components.ResetBindings(false);
		}

        protected override void ShowProductAction() 
        {
            if (Datos_Components.Current == null) return;

            Kit item = Datos_Components.Current as Kit;

            ProductEditForm form = new ProductEditForm(item.OidProduct, this);

            form.ShowDialog();
        }

        #endregion

        #region Buttons

		private void CuentaContableVenta_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContableVenta = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
		}

		private void CuentaContableCompra_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContableCompra = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
		}

        private void DefectoCompra_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null, ETipoSerie.Compra);
            ImpuestoCompra_TB.Text = _entity.ImpuestoCompra;
        }

        private void DefectoVenta_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null, ETipoSerie.Venta);
            ImpuestoVenta_TB.Text = _entity.ImpuestoVenta;
        }
		
		private void Familia_BT_Click(object sender, EventArgs e)
		{
			SetFamiliaAction();
		}

		private void FormaVenta_BT_Click(object sender, EventArgs e) { SetSaleMethodAction(); }

		private void ImpuestoCompra_BT_Click(object sender, EventArgs e)
		{
			ImpuestoSelectForm form = new ImpuestoSelectForm(this);
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo item = form.Selected as ImpuestoInfo;
				_entity.SetImpuesto(item, ETipoSerie.Compra);
				ImpuestoCompra_TB.Text = _entity.ImpuestoCompra;
			}
		}

		private void ImpuestoVenta_BT_Click(object sender, EventArgs e)
		{
			ImpuestoSelectForm form = new ImpuestoSelectForm(this);
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo item = form.Selected as ImpuestoInfo;
				_entity.SetImpuesto(item, ETipoSerie.Venta);
				ImpuestoVenta_TB.Text = _entity.ImpuestoVenta;
			}
		}

		private void SetEstado_BT_Click(object sender, EventArgs e) { SetEstadoAction(); }


        #endregion       
	}
}

