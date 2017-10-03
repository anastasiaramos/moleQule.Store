using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class FamiliaForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public virtual Familia Entity { get { return null; } set { } }
		public virtual FamiliaInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public FamiliaForm()  
			: this(-1, null) {}

		public FamiliaForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        #endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
			CuentaContableCompra_TB.Enabled = Familia.CanEditCuentaContable();
			CuentaContableCompra_TB.ReadOnly = !Familia.CanEditCuentaContable();
			CuentaContableCompra_BT.Enabled = Familia.CanEditCuentaContable();
			CuentaContableVenta_TB.Enabled = Familia.CanEditCuentaContable();
			CuentaContableVenta_TB.ReadOnly = !Familia.CanEditCuentaContable();
			CuentaContableVenta_BT.Enabled = Familia.CanEditCuentaContable();
		}

		#endregion

        #region Layout & Source

        public override void FormatControls()
        {

            base.FormatControls();

			CuentaContableVenta_TB.Mask = Library.Invoice.ModuleController.GetCuentasMask();
			CuentaContableCompra_TB.Mask = Library.Invoice.ModuleController.GetCuentasMask();

            //IDE Compatibility
            try
            {
                PBeneficioMinimo_NTB.Text = EntityInfo.PBeneficioMinimo.ToString("N2");
            }
            catch { }
        }

        #endregion
                        
		#region Validation & Format

		#endregion

        #region Buttons

        #endregion

        #region Events

        #endregion
    }
}

