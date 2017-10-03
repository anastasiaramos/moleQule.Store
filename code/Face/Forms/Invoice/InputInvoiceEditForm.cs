using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class InputInvoiceEditForm : InputInvoiceUIForm, IBackGroundLauncher
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		#endregion

        #region Factory Methods

		public InputInvoiceEditForm()
			: base(-1, ETipoAcreedor.Todos, null) {}

        public InputInvoiceEditForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, tipo, parent)
		{
			InitializeComponent();
            
            if (_entity != null)
            {
                SetFormData();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];
			_entity = InputInvoice.Get(oid, tipo);
			_entity.BeginEdit();
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
			Rectificativo_CkB.Enabled = false;

			base.FormatControls();
        }

		#endregion 

		#region Source

		protected override void RefreshMainData()
		{
			_albaranes = InputDeliveryList.GetListByFactura(true, _entity.Oid).GetListInfo();
			PgMng.Grow();

			base.RefreshMainData();
		}

		#endregion
	}
}