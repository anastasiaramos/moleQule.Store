using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using moleQule;
using moleQule.Base;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class InvoicePaymentEditForm : InvoicePaymentUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "InvoicePaymentEditForm";
        public static Type Type { get { return typeof(InvoicePaymentEditForm); } }

        #endregion

        #region Factory Methods

        public InvoicePaymentEditForm(Form parent, IAcreedor acreedor, Payment pago, bool locked)
            : base(parent, acreedor)
        {
            InitializeComponent();

            _locked = locked;
            _entity = pago;
            
            SetFormData();
            this.Text = Resources.Labels.PAGO_DETAIL_TITLE;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            InputInvoiceList list = InputInvoiceList.GetListByPagoAndPendientesByAcreedor(_entity.Oid, _holder.Oid, _holder.ETipoAcreedor, false);
            Datos_Lineas.DataSource = InputInvoiceList.GetSortedList(list, "Prevision", ListSortDirection.Ascending);
            PgMng.Grow();

			base.RefreshMainData();
        }

        protected override ComboBoxList<EMedioPago> GetPaymentMethods()
        {
            switch (_entity.EMedioPago)
            {
                case EMedioPago.ComercioExterior:
                    return moleQule.Common.Structs.EnumText<EMedioPago>.GetList(new EMedioPago[] { EMedioPago.ComercioExterior });

                default:
                    ComboBoxList<EMedioPago> list = moleQule.Common.Structs.EnumText<EMedioPago>.GetList(false);
                    list.Remove((int)EMedioPago.ComercioExterior);
                    return list;
            }
        }

        #endregion

		#region Layout

		public override void FormatControls()
		{
			if ((_entity.EMedioPago == EMedioPago.CompensacionFactura) ||
				(_entity.EMedioPago == EMedioPago.Efectivo))
				MedioPago_BT.Enabled = false;
			else
				MedioPago_BT.Enabled = !_locked;

			Tarjeta_BT.Enabled = (_entity.EMedioPago == EMedioPago.Tarjeta);

            Source_GB.Enabled = !new moleQule.Base.EEstado[] { moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Anulado }.Contains(_entity.EEstado); 

			base.FormatControls();
		}

		#endregion

		#region Business Methods

		#endregion

		#region Actions

		protected override void SubmitAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
				PgMng.ShowInfoException(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!ValidateAllocation()) return;
            if (!ValidateDueDate()) return;

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void CancelAction()
        {
            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion     
    }
}