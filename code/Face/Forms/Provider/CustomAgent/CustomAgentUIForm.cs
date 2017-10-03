using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class CustomAgentUIForm : CustomAgentForm
    {
        #region Attributes & Properties
		
        public new const string ID = "DespachanteUIForm";
		public new static Type Type { get { return typeof(CustomAgentUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Despachante _entity;

        public override Despachante Entity { get { return _entity; } set { _entity = value; } }
        public override DespachanteInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected CustomAgentUIForm() 
			: this(-1, null) { }

        public CustomAgentUIForm(long oid) 
			: this(oid, null) { }

        public CustomAgentUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        public CustomAgentUIForm(IAcreedor item, Form parent)
            : base(item, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                Despachante temp = _entity.Clone();
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
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_Productos.DataSource = _entity.Productos;

            PuertoList puertos = Datos_Puertos.DataSource as PuertoList;
            foreach (PuertoInfo pi in puertos)
            {
                if (_entity.PuertoDespachantes.ContainsPuerto(pi.Oid))
                {
                    int index = Puertos_CLB.Items.IndexOf(pi);
                    if (index != -1)
                        Puertos_CLB.SetItemChecked(index, true);
                }
            }
			PgMng.Grow();

            SelectFormaPagoAction();

            base.RefreshMainData();						
        }		
	
        #endregion

        #region Actions

        protected override void SaveAction()
        {
            foreach (PuertoInfo pi in Puertos_CLB.Items)
            {
                if (Puertos_CLB.CheckedItems.IndexOf(pi) != -1)
                {
                    if (!_entity.PuertoDespachantes.ContainsPuerto(pi.Oid))
                    {
                        _entity.PuertoDespachantes.NewItem(pi);
                    }
                }
                else
                {
                    if (_entity.PuertoDespachantes.ContainsPuerto(pi.Oid))
                    {
                        _entity.PuertoDespachantes.RemovePuerto(pi.Oid);
                    }
                }
            }

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
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

		protected void SelectTipoIDAction()
		{
			if (TipoID_CB.SelectedItem == null) return;

			ETipoID tipo = (ETipoID)(long)TipoID_CB.SelectedValue;
			MascaraID_Label.Text = AgenteBase.GetTipoIDMask(tipo);
		}

        protected void SelectFormaPagoAction()
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

        private void CuentaAsociada_BT_Click(object sender, EventArgs e)
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
