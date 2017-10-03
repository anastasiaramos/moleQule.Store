using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.CslaEx;

using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamiliaUIForm : FamiliaForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata de la Familia actual y que se va a editar.
        /// </summary>
        protected Familia _entity;

        public override Familia Entity { get { return _entity; } set { _entity = value; } }
        public override FamiliaInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public FamiliaUIForm()
            : this(-1, null) { }

        public FamiliaUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        public FamiliaUIForm(Familia Familia)
            : base()
        {
            InitializeComponent();
            _entity = Familia.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = Familia.Get(oid);
            _entity.BeginEdit();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                // Comprobamos que no se intente insertar uno con el mismo codigo
                //if (Entity.IsNew && Familia.Exists(Codigo_TB.Text))
                //{
                //    MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Messages.DUPLICATED_CODE);
                //    return false;
                //}

                this.Datos.RaiseListChangedEvents = false;

                Familia temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();

                    // Se modifica el nombre de la foto
                    /*if (_entity.Logo == "00.bmp")
                    {
						Images.Rename(_entity.Logo, _entity.Code + ".bmp", Principal.LogosFamiliasPath);
                        _entity.Logo = _entity.Code + ".bmp";
                        _entity.Save();
                    }/**/

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

        #region Layout & Source
        
        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
            PgMng.Grow();
        }

        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {
            Validator.ValidateInt(identificadorTextBox.Text);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            /*try
            {
                ValidateInput();
            }
            catch (iQValidationException ex)
            {
                MessageBox.Show(ex.Message,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                _action_result = DialogResult.Ignore;
                return;
            }*/
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

        #region Buttons

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

        private void CuentaContableVenta_BT_Click(object sender, EventArgs e)
        {
			_entity.CuentaContableVenta = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
        }

        private void CuentaContableCompra_BT_Click(object sender, EventArgs e)
        {
			_entity.CuentaContableCompra = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
        }

        #endregion
    }
}

