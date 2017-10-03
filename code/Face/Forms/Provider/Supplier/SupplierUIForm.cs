using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class SupplierUIForm : SupplierForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Proveedor _entity;

        public override Proveedor Entity { get { return _entity; } set { _entity = value; } }
        public override ProveedorInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }
        
        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected SupplierUIForm() 
			: this(-1, ETipoAcreedor.Proveedor) { }

        public SupplierUIForm(long oid, ETipoAcreedor providerType)
            : this(oid, providerType, null) { }

		public SupplierUIForm(long oid, ETipoAcreedor providerType, Form parent)
            : base(oid, providerType, true, parent)
        {
            InitializeComponent();
        }

        public SupplierUIForm(IAcreedor item, Form parent)
            : base(item, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Proveedor temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                //Decomentar si se va a mantener en memoria
                //_entity.BeginEdit();
                return true;
            }
            catch (Exception ex)
            {
				PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_Productos.DataSource = _entity.Productos;

            SelectFormaPagoAction();

            base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void AddProductoAction()
		{
			ProductSelectForm form = new ProductSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo item = form.Selected as ProductInfo;

				_entity.Productos.NewItem(_entity, item);
				Datos_Productos.ResetBindings(true);
			}
		}

		protected override void DeleteProductoAction()
		{
			if (Datos_Productos.Current == null) return;

			if (PgMng.ShowDeleteConfirmation() == DialogResult.Yes)
			{
				ProductoProveedor pp = (ProductoProveedor)Datos_Productos.Current;
				_entity.Productos.Remove(pp.Oid);

				Datos_Productos.ResetBindings(false);
			}
		}

		protected virtual void SelectFormaPagoAction()
        {
            if (FormaPago_CB.SelectedItem == null) return;

            EFormaPago fPago = (EFormaPago)(long)FormaPago_CB.SelectedValue;
            switch (fPago)
            {
                case EFormaPago.Contado:
                    DiasPago_NTB.Enabled = false;
                    _entity.DiasPago = 0;
                    break;
                case EFormaPago.XDiasFechaFactura:
                    DiasPago_NTB.Enabled = true;
                    break;
                case EFormaPago.XDiasMes:
                    DiasPago_NTB.Enabled = true;
                    break;
            }
        }

		protected override void SelectImpuestoLineaAction()
		{
			if (Datos_Productos.Current == null) return;

			ProductoProveedor item = (ProductoProveedor)Datos_Productos.Current;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo impuesto = form.Selected as ImpuestoInfo;

				item.OidImpuesto = impuesto.Oid;
				item.Impuesto = impuesto.Nombre;
				item.PImpuestos = impuesto.Porcentaje;

				Datos_Productos.ResetBindings(false);
			}
		}

		protected override void SelectTipoDescuentoLineaAction()
		{
			ProductoProveedor item = Productos_DGW.CurrentRow.DataBoundItem as ProductoProveedor;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(moleQule.Common.Structs.EnumText<ETipoDescuento>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				item.ETipoDescuento = (ETipoDescuento)tipo.Oid;

				ControlsMng.UpdateBinding(Datos_Productos);
			}
		}

		protected virtual void SelectTipoIDAction()
        {
            if (TipoID_CB.SelectedItem == null) return;

            ETipoID tipo = (ETipoID)(long)TipoID_CB.SelectedValue;
			MascaraID_Label.Text = AgenteBase.GetTipoIDMask(tipo);
        }

        protected override void SelectTarjetaAsociadaAction()
        {
            CreditCardSelectForm form = new CreditCardSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                CreditCardInfo item = form.Selected as CreditCardInfo;

                _entity.OidTarjetaAsociada = item.Oid;
                _entity.TarjetaAsociada = item.Nombre;
            }
        }

        #endregion

        #region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
			}
		}

		private void CuentaAjena_BT_Click(object sender, EventArgs e)
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.CuentaCorriente, moleQule.Base.EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo item = form.Selected as BankAccountInfo;

				_entity.OidCuentaBAsociada = item.Oid;
				_entity.CuentaAsociada = item.Valor;
            }
        }

        private void Localidad_BT_Click(object sender, EventArgs e)
        {
            MunicipioSelectForm form = new MunicipioSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                MunicipioInfo item = (MunicipioInfo)form.Selected;

                if (item == null) return;

                _entity.Localidad = item.Localidad;
                _entity.CodPostal = item.CodPostal;
                _entity.Municipio = item.Nombre;
                _entity.Provincia = item.Provincia;
				_entity.Pais = item.Pais;
            }
        }

        private void Impuesto_BT_Click(object sender, EventArgs e)
        {
            ImpuestoSelectForm form = new ImpuestoSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ImpuestoInfo item = form.Selected as ImpuestoInfo;
                _entity.SetImpuesto(item);
                Impuesto_TB.Text = _entity.Impuesto;
            }
        }

        private void Defecto_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null);
            Impuesto_TB.Text = _entity.Impuesto;
        }

		private void CuentaContable_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContable = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
		}
		
		private void Tarjeta_BT_Click(object sender, EventArgs e)
		{
			SelectTarjetaAsociadaAction();
		}

        #endregion

        #region Events

		void TipoID_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectTipoIDAction();
		}

        private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFormaPagoAction();
        }

        #endregion
    }
}