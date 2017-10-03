using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class TipoGastoForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "TipoGastoForm";
		public static Type Type { get { return typeof(TipoGastoForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual TipoGasto Entity { get { return null; } set { } }
        public virtual TipoGastoInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public TipoGastoForm() 
			: this(-1) {}

        public TipoGastoForm(long oid) 
			: this(oid, true, null) {}

		public TipoGastoForm(bool is_modal) 
		: this(-1, is_modal, null) {}

        public TipoGastoForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

		#region Authorization

		protected override void ApplyAuthorizationRules()
		{
			CuentaContable_TB.Enabled = TipoGasto.CanEditCuentaContable();
			CuentaContable_TB.ReadOnly = !ProviderBase.CanEditCuentaContable();
			CuentaContable_BT.Enabled = TipoGasto.CanEditCuentaContable();
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			CuentaContable_TB.Mask = Library.Invoice.ModuleController.GetCuentasMask();
		}

		#endregion

		#region Source

		public override void RefreshSecondaryData()
		{
			Datos_FormaPago.DataSource = moleQule.Common.Structs.EnumText<EFormaPago>.GetList();
			PgMng.Grow();

			Datos_MedioPago.DataSource = moleQule.Common.Structs.EnumText<EMedioPago>.GetList();
			PgMng.Grow();

			ECategoriaGasto[] list = { ECategoriaGasto.Administracion, ECategoriaGasto.Bancario, ECategoriaGasto.Impuesto, ECategoriaGasto.Nomina, ECategoriaGasto.Otros, ECategoriaGasto.SeguroSocial };
			Datos_Categoria.DataSource = moleQule.Store.Structs.EnumText<ECategoriaGasto>.GetList(list, true);
			Categoria_CB.SelectedValue = (long)ECategoriaGasto.Seleccione;
			PgMng.Grow();
		}
		
		#endregion
    }
}
