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
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class TransporterUIForm : TransporterForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata de la Transporter actual y que se va a editar.
        /// </summary>
        protected Transporter _entity;

		public override Transporter Entity { get { return _entity; } set { _entity = value; } }
		public override TransporterInfo EntityInfo	{ get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public TransporterUIForm() 
            : this(-1, ETipoAcreedor.Todos) {}

		public TransporterUIForm(long oid, ETipoAcreedor providerType)
            : this(oid, providerType, null) { }

		public TransporterUIForm(long oid,  ETipoAcreedor tipo, Form parent) 
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
        }

        public TransporterUIForm(IAcreedor item, Form parent)
            : base(item, parent)
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

                Transporter temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();

                    //_entity.BeginEdit();
                    return true;
                }
				catch (iQValidationException ex)
				{
					MessageBox.Show(iQExceptionHandler.GetAllMessages(ex) +
									Environment.NewLine + ex.SysMessage,
									Application.ProductName,
									MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
					return false;

				}
				catch (Exception ex)
				{
                    MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine +
									ex.Message,
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

        #region Layout

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			PgMng.Grow();

			Datos_Productos.DataSource = _entity.Productos;
            Datos_PrecioDestino.DataSource = _entity.PrecioDestinos;
            Datos_PrecioOrigen.DataSource = _entity.PrecioOrigenes;

            SelectFormaPagoAction();

			base.RefreshMainData();			
        }

        #endregion

        #region Actions

        protected override void SaveAction()
		{
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
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

        protected override void AgregarPrecioOrigenAction()
        {
            PrecioOrigenForm form = new PrecioOrigenForm(_entity);
            form.ShowDialog(this);
        }

        protected override void EditarPrecioOrigenAction()
        {
            if (Datos_PrecioOrigen.Current == null) return;

            PrecioOrigen precio = (PrecioOrigen)Datos_PrecioOrigen.Current;
            PrecioOrigenForm form = new PrecioOrigenForm(precio);
            form.ShowDialog(this);
        }

        protected override void EliminarPrecioOrigenAction()
        {
            if (Datos_PrecioOrigen.Current == null) return;

            PrecioOrigen precio = (PrecioOrigen)Datos_PrecioOrigen.Current;

            if (MessageBox.Show(moleQule.Face.Resources.Messages.DELETE_CONFIRM,
                    moleQule.Face.Resources.Labels.ADVISE_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                _entity.PrecioOrigenes.Remove(precio);
        }

        protected override void DefaultOrigenAction()
        {
            EditarPrecioOrigenAction();
        }

        protected override void AgregarPrecioDestinoAction()
        {
            PrecioDestinoForm form = new PrecioDestinoForm(_entity);
            form.ShowDialog(this);
        }

        protected override void EditarPrecioDestinoAction()
        {
            if (Datos_PrecioDestino.Current == null) return;

            PrecioDestino precio = (PrecioDestino)Datos_PrecioDestino.Current;
            PrecioDestinoForm form = new PrecioDestinoForm(precio);
            form.ShowDialog(this);
        }

        protected override void EliminarPrecioDestinoAction()
        {
            if (Datos_PrecioDestino.Current == null) return;

            PrecioDestino precio = (PrecioDestino)Datos_PrecioDestino.Current;

            if (MessageBox.Show(moleQule.Face.Resources.Messages.DELETE_CONFIRM,
                    moleQule.Face.Resources.Labels.ADVISE_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                _entity.PrecioDestinos.Remove(precio);
        }

        protected override void DefaultDestinoAction()
        {
            EditarPrecioDestinoAction();
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