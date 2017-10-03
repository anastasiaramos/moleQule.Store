using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

		public override Type EntityType { get { return typeof(PedidoProveedor); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }
		
        public virtual PedidoProveedor Entity { get { return null; } set { } }
        public virtual PedidoProveedorInfo EntityInfo { get { return null; } }
		
        #endregion

        #region Factory Methods

        public PedidoProveedorForm()
			: this(-1) {}

        public PedidoProveedorForm(long oid) 
			: this(oid, ETipoAcreedor.Todos, true, null) {}

		public PedidoProveedorForm(bool isModal) 
			: this(-1, ETipoAcreedor.Todos, isModal, null) {}

        public PedidoProveedorForm(long oid, ETipoAcreedor tipo, bool isModal, Form parent)
            : base(oid, new object[1] {tipo}, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			Concepto.Tag = 1;
			cols.Add(Concepto);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);
		}

        public override void FormatControls()
        {
			MaximizeForm(1200, 0);
            base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.CREAR_ALBARAN, Properties.Resources.albaran_recibido);

			Fecha_DTP.Checked = true;

			Lineas_DGW.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			Lineas_DGW.AllowUserToResizeRows = true;
			Lineas_DGW.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
			Lineas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }

		protected virtual void RefreshLineas()
		{
			Datos_Lineas.ResetBindings(true);
			Lineas_DGW.Refresh();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.CustomAction1);
					break;
			}
		}

		#endregion

        #region Actions

		protected virtual void AddLineaAction() { }
		protected virtual void EditLineaAction() { }
		protected virtual void DeleteLineaAction() { }
		protected virtual void SelectAlmacenLineaAction() { }
		protected virtual void SelectExpedienteLineaAction() { }
		protected virtual void SelectImpuestoLineaAction() { }

		protected virtual void UpdatePedidoAction() { }

        #endregion

        #region Buttons

		private void AddConcepto_TI_Click(object sender, EventArgs e) { AddLineaAction(); }

		private void Edit_TI_Click(object sender, EventArgs e) { EditLineaAction(); }

		private void Delete_TI_Click(object sender, EventArgs e) {	DeleteLineaAction(); }

        #endregion

        #region Events

		private void Lineas_DGW_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(Lineas_DGW.Rows[e.RowIndex]);
		}

		private void Lineas_DGW_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			UpdatePedidoAction();
		}

		private void Lineas_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.Columns[e.ColumnIndex].Name == PImpuestos.Name) SelectImpuestoLineaAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == Almacen.Name) SelectAlmacenLineaAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == Expedient.Name) SelectExpedienteLineaAction();
		}

        #endregion

    }
}
