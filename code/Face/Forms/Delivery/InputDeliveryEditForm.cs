using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class InputDeliveryEditForm : InputDeliveryUIForm
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		#endregion

		#region Factory Methods

		public InputDeliveryEditForm(long oid, Form parent, ETipoAcreedor providerType)
            : this(oid, new object[1] { providerType }, parent) {}

		public InputDeliveryEditForm(InputDelivery entity, Form parent)
			: this(-1, new object[1] { entity }, parent) {}

		public InputDeliveryEditForm(long oid, object[] parameters, Form parent)
			: base(oid, parameters, true, parent)
		{
			InitializeComponent();
			if (_entity != null) SetFormData();
			_mf_type = ManagerFormType.MFEdit;
		}

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];

			_entity = InputDelivery.Get(oid, tipo);
			_deliveryType = _entity.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos;
			_entity.BeginEdit();
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = (InputDelivery)parameters[0];
			_deliveryType = _entity.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos;
			_entity.BeginEdit();
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

			Almacen_BT.Enabled = false;
            Rectificativo_CKB.Enabled = false;
        }

        #endregion
    }
}

