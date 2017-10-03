using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;

namespace moleQule.Face.Store
{
    public partial class TipoGastoUIForm : TipoGastoForm
    {
        #region Attributes & Properties
		
        public new const string ID = "TipoGastoUIForm";
		public new static Type Type { get { return typeof(TipoGastoUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected TipoGasto _entity;

        public override TipoGasto Entity { get { return _entity; } set { _entity = value; } }
        public override TipoGastoInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected TipoGastoUIForm() 
			: this(null) { }

        public TipoGastoUIForm(Form parent) 
			: this(-1, parent) { }

        public TipoGastoUIForm(long oid) 
			: this(oid, null) { }

        public TipoGastoUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            TipoGasto temp = _entity.Clone();
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
				PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
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
						
            base.RefreshMainData();
        }	
		
        #endregion

        #region Actions

        protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
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

        #endregion

		#region Buttons

		private void CuentaAjena_BT_Click(object sender, EventArgs e)
		{
			BankAccountSelectForm form = new BankAccountSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo item = form.Selected as BankAccountInfo;

				_entity.OidCuentaBAsociada = item.Oid;
				_entity.CuentaAsociada = item.Valor;
			}
		}

		private void DefectoCtaContable_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContable = string.Empty.PadLeft(moleQule.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
		}

		#endregion

		#region Events

		private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectFormaPagoAction();
		}

        #endregion
    }
}
