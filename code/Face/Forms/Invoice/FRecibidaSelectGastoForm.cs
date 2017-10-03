using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class FRecibidaSelectGastoForm : InputInvoiceViewForm
	{
		#region Attributes & Properties

		public virtual InputInvoiceInfo Factura { get; set; }

		protected Expedient _expediente;

		#endregion

        #region Factory Methods

		public FRecibidaSelectGastoForm()
			: this(null, null, null) {}

		/*public FRecibidaSelectGastoForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, tipo, parent)
        {
            InitializeComponent();
		}*/

		public FRecibidaSelectGastoForm(InputInvoiceInfo factura, Expedient expediente, Form parent)
			: base(factura, parent)
		{
			InitializeComponent();

			_expediente = expediente;
		}

        #endregion

		#region Layout

		public override void FormatControls()
		{
			HideAction(molAction.Print);
			
			ShowSelectColumn();

			base.FormatControls();

			Lineas_DGW.Enabled = true;
			Lineas_DGW.ReadOnly = false;
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

			InputInvoiceLineInfo item = (InputInvoiceLineInfo)row.DataBoundItem;

			bool reserved = (item.OidExpediente != 0) && (item.OidExpediente != _expediente.Oid);

			LockIsSelected(row, reserved);
		}

		#endregion

		#region Business Methods

		private void CopyFactura()
		{
			Factura = InputInvoice.New(_entity).GetInfo(true);

			List<InputInvoiceLineInfo> conceptosOut = new List<InputInvoiceLineInfo>();

			foreach (InputInvoiceLineInfo item in Factura.Conceptos)
			{
				bool isIn = false;

				foreach (InputInvoiceLineInfo cp in _conceptos_selected)
					if (cp.Oid == item.Oid)
					{
						isIn = true;
						continue;
					}

				if (!isIn) conceptosOut.Add(item);
			}

			foreach (InputInvoiceLineInfo item in conceptosOut)
				Factura.Conceptos.RemoveItem(item.Oid);

			Factura.CalculateTotal();
		}

		#endregion

		#region Actions

		protected override void SaveAction() 
		{
			Factura = EntityInfo;

			//SelectConceptosAction();
			//CopyFactura();

			_action_result = DialogResult.OK;
			DialogResult = DialogResult.OK;
		}

		protected override void ShowAlbaranAction()
		{
			_conceptos_selected.Clear();

			SelectConceptoAction(Lineas_DGW.CurrentRow);
			CopyFactura();

			_action_result = DialogResult.OK;
			Cerrar();
		}

		protected override void CancelAction()
		{
			_action_result = DialogResult.Cancel;
		}

		#endregion
	}
}
