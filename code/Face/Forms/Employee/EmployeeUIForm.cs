using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using moleQule.Common.Structs;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class EmployeeUIForm : EmployeeForm
    {
        #region Business Methods

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Employee _entity;

        public override Employee Entity { get { return _entity; } set { _entity = value; } }
        public override EmployeeInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

		public EmployeeUIForm(long oid, Form parent)
			: base(oid, parent) 
        {
            InitializeComponent();
        }

		public EmployeeUIForm(IAcreedor item, Form parent)
			: base(item, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Employee temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                // Se modifica el nombre de la foto
                if (_entity.Foto != string.Empty)
                {
					Bitmap imagen = new Bitmap(Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH + _entity.Foto);

                    string ext = string.Empty;

                    if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
                        ext = ".jpg";
                    else
                    {
                        if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Bmp.Guid))
                            ext = ".bmp";
                        else
                        {
                            if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Png.Guid))
                                ext = ".png";
                        }
                    }

                    imagen.Dispose();

                    if (_entity.Foto != _entity.Oid.ToString("000") + ext)
                    {
                        File.Copy(Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH + _entity.Foto,
									Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH + _entity.Oid.ToString("000") + ext, true);
						File.Delete(Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH + _entity.Foto);

                        _entity.Foto = _entity.Oid.ToString("000") + ext;
                        _entity.Save();
                    }
                }

                return true;
            }
			catch (Exception ex)
			{
				CleanCache();
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
				PgMng.FillUp();
			}
        }
        
        #endregion

        #region Source

		protected override void RefreshMainData()
		{
			if (_entity == null) return;

			Datos.DataSource = _entity;
			PgMng.Grow();

			Products_BS.DataSource = _entity.Productos;

			SelectFormaPagoAction();

			base.RefreshMainData();
		}

        protected override void SetDependentControlSource(Control control)
        {
            if (control.Name == Perfil_GB.Name)
            {
                Gerente_CB.Checked = (_entity.Perfil >> 8) % 2 == 1;
                Administrador_CB.Checked = (_entity.Perfil >> 9) % 2 == 1;
				Empleado_CB.Checked = (_entity.Perfil >> 10) % 2 == 1;
            }
        }

        #endregion

        #region Actions

		protected override void SaveAction()
		{
			try
			{
				ValidateInput();
			}
			catch (iQValidationException ex)
			{
				MessageBox.Show(ex.Message,
								Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Warning);

				_action_result = DialogResult.Ignore;
				return;
			}

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
		}

        protected override void EditWorkReportAction()
        {
            if (!ControlsMng.IsCurrentItemValid(WorkReport_DGW)) return;

            WorkReportResourceInfo item = ControlsMng.GetCurrentItem(WorkReport_DGW) as WorkReportResourceInfo;

            WorkReportEditForm form = new WorkReportEditForm(item.OidWorkReport, this);
            form.ShowDialog(this);

            if (form.ActionResult == DialogResult.OK)
                LoadWorkReports(true);
        }

		protected void SetStatusAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
                Status_TB.Text = _entity.EstadoLabel;
			}
		}

        protected void SetPayrollMethodAction()
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(moleQule.Store.Structs.EnumText<EPayrollMethod>.GetList(false, false, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ComboBoxSource payroll_method = form.Selected as ComboBoxSource;
                _entity.PayrollMethod = payroll_method.Oid;
                 PayrollMethod_TB.Text = _entity.PayrollMethodLabel;
            }
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

		protected void SelectTipoIDAction()
		{
			if (TipoID_CB.SelectedItem == null) return;

			ETipoID tipo = (ETipoID)(long)TipoID_CB.SelectedValue;
			MascaraID_Label.Text = AgenteBase.GetTipoIDMask(tipo);
		}

		protected override void AddProductoAction()
		{
			ProductSelectForm form = new ProductSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo item = form.Selected as ProductInfo;

				_entity.Productos.NewItem(_entity, item);
				Products_BS.ResetBindings(true);
			}
		}

		protected override void DeleteProductAction()
		{
			if (Products_BS.Current == null) return;

			if (PgMng.ShowDeleteConfirmation() == DialogResult.Yes)
			{
				ProductoProveedor pp = (ProductoProveedor)Products_BS.Current;
				_entity.Productos.Remove(pp.Oid);

				Products_BS.ResetBindings(false);
			}
		}

		protected override void SelectLineTaxAction()
		{
			if (Products_BS.Current == null) return;

			ProductoProveedor item = (ProductoProveedor)Products_BS.Current;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo impuesto = form.Selected as ImpuestoInfo;

				item.OidImpuesto = impuesto.Oid;
				item.Impuesto = impuesto.Nombre;
				item.PImpuestos = impuesto.Porcentaje;

				Products_BS.ResetBindings(false);
			}
		}

		protected override void SelectLineTaxTypeAction()
		{
			ProductoProveedor item = Productos_DGW.CurrentRow.DataBoundItem as ProductoProveedor;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(moleQule.Common.Structs.EnumText<ETipoDescuento>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				item.ETipoDescuento = (ETipoDescuento)tipo.Oid;

				ControlsMng.UpdateBinding(Products_BS);
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

		private void SetEstado_BT_Click(object sender, EventArgs e)
		{
			SetStatusAction();
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

        private void AddFoto_BT_Click(object sender, EventArgs e)
        {
            if (this is EmployeeEditForm)
            {
                try
                {
                    if (Browser.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap imagen = new Bitmap(Browser.FileName);

                        string ext = string.Empty;

                        if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
                            ext = ".jpg";
                        else
                        {
                            if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Bmp.Guid))
                                ext = ".bmp";
                            else
                            {
                                if (imagen.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Png.Guid))
                                    ext = ".png";
                            }
                        }

                        imagen.Dispose();

                        _entity.Foto = _entity.Oid.ToString("000") + ext;
                        File.Copy(Browser.FileName, Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH + _entity.Foto, true);
                    }

					Images.Show(Entity.Foto, Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH, Foto_PB);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Debe guardar la ficha del instructor actual antes de poder insertar una imagen");
        }
		
		private void DeleteFoto_BT_Click(object sender, EventArgs e)
        {
			Images.Delete(Entity.Foto, Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH);
            Entity.Foto = string.Empty;
			Images.Show(Entity.Foto, Library.Store.ModuleController.FOTOS_EMPLEADOS_PATH, Foto_PB);
        }

        private void PayrollMethod_BT_Click(object sender, EventArgs e) { SetPayrollMethodAction(); }

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

		private void Empleado_CB_CheckedChanged(object sender, EventArgs e)
		{
			if (Empleado_CB.Checked)
			{
				if ((Entity.Perfil >> 1) % 2 == 0)
					Entity.Perfil += (long)Perfil.Instructor;
			}
			else Entity.Perfil -= (long)Perfil.Instructor;

			SetDependentControlSource(Perfil_GB);
		}

		private void Gerente_CB_CheckedChanged(object sender, EventArgs e)
		{
			if (Gerente_CB.Checked)
			{
				if ((Entity.Perfil >> 8) % 2 == 0)
					Entity.Perfil += (long)Perfil.Gerente;
			}
			else Entity.Perfil -= (long)Perfil.Gerente;

			SetDependentControlSource(Perfil_GB);
		}

		private void Administrador_CB_CheckedChanged(object sender, EventArgs e)
		{
			if (Administrador_CB.Checked)
			{
				if ((Entity.Perfil >> 9) % 2 == 0)
					Entity.Perfil += (long)Perfil.Administrador;
			}
			else Entity.Perfil -= (long)Perfil.Administrador;

			SetDependentControlSource(Perfil_GB);
		}
        
        #endregion
    }
}
