using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InvoicePaymentAddForm : InvoicePaymentUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        public const string ID = "InvoicePaymentAddForm";
        public static Type Type { get { return typeof(InvoicePaymentAddForm); } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoicePaymentAddForm(IAcreedor acreedor) 
            : this(null, acreedor) {}
		
        public InvoicePaymentAddForm(Form parent, IAcreedor acreedor)
            : base(parent, acreedor)
        {
            InitializeComponent();
            SetFormData(acreedor);
            this.Text = Resources.Labels.PAGO_ADD_TITLE;
        }

        protected void SetFormData(IAcreedor source)
        {
            _holder.LoadChilds(typeof(Payment), true);
            _entity = _holder.Pagos.NewItem(source, ETipoPago.Factura);
            _entity.CopyFrom(source, ETipoPago.Factura);
            
            base.SetFormData();
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            InputInvoiceList list = InputInvoiceList.GetPendientesList(_holder.Oid, _holder.ETipoAcreedor, false);
            Datos_Lineas.DataSource = InputInvoiceList.GetSortedList(list, "Prevision", ListSortDirection.Ascending);
            PgMng.Grow();

			base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SubmitAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                MessageBox.Show(Resources.Messages.SELECT_PAYMENT_METHOD);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!ValidateAllocation()) return;
            if (!ValidateDueDate()) return;

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void CancelAction()
        {
            _holder.Pagos.Remove(_entity.Oid);

            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion     
    }
}