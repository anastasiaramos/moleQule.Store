using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Producto;
using moleQule.Face;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InventarioValoradoActionForm : Skin01.ActionSkinForm
    {

        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "InventarioValoradoActionForm";
        public static Type Type { get { return typeof(InventarioValoradoActionForm); } }

        ProductInfo _producto;
        ExpedientInfo _expediente;

        protected moleQule.Store.Structs.ETipoExpediente ETipoExpediente
        {
            get
            {
                ComboBoxSource item = TipoExpediente_CB.SelectedItem as ComboBoxSource;
                return item != null ? (moleQule.Store.Structs.ETipoExpediente)item.Oid : moleQule.Store.Structs.ETipoExpediente.Todos;
            }
        }

        #endregion

        #region Factory Methods

        public InventarioValoradoActionForm()
            : this(null) { }

        public InventarioValoradoActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

		#region Business Methods

		private string GetFilterValues()
		{
			string filtro = string.Empty;

			if (!TodosProducto_CkB.Checked)
				filtro += "Producto " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _producto.Nombre + "; ";

			if (!TodosExpediente_CkB.Checked)
				filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _expediente.Codigo + "; ";
			else
				filtro += "Tipo Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + TipoExpediente_CB.Text + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		#endregion

        #region Layout & Source

        public override void RefreshSecondaryData()
        {
            Datos_Tipos.DataSource = moleQule.Store.Structs.EnumText<moleQule.Store.Structs.ETipoExpediente>.GetList();
            TipoExpediente_CB.SelectedItem = ComboBoxSourceList.Get(Datos_Tipos.DataSource, (long)moleQule.Store.Structs.ETipoExpediente.Todos);
            Bar.Grow();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            if (!TodosExpediente_CkB.Checked && (_expediente == null)) return;
            if (!TodosProducto_CkB.Checked && (_producto == null)) return;

            ProductInfo producto = TodosProducto_CkB.Checked ? null : _producto;
            moleQule.Store.Structs.ETipoExpediente tipo = !TodosExpediente_CkB.Checked ? _expediente.ETipo : ETipoExpediente;
            ExpedientInfo expediente = TodosExpediente_CkB.Checked ? null : _expediente;
            DateTime fecha = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.Today;

            InventarioValoradoList list;

            string filtro = GetFilterValues();
            PgMng.Grow();

            //if (Stock_CkB.Checked)
                list = InventarioValoradoList.GetListStock(tipo, expediente, fecha);
            //else
                //list = InventarioValoradoList.GetList(producto, tipo, expediente, fecha);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            ProductReportMng reportMng = new ProductReportMng(AppContext.ActiveSchema, string.Empty, filtro);
            InventarioValoradoRpt rpt = reportMng.GetInventarioValoradoReport(list, fecha);
            PgMng.FillUp();

            if (rpt != null)
            {
                ReportViewer.SetReport(rpt);
                ReportViewer.ShowDialog();
            }
            else
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Buttons

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _producto = form.Selected as ProductInfo;
                Producto_TB.Text = _producto.Nombre;
            }
        }

        private void Expediente_BT_Click(object sender, EventArgs e)
        {
            ExpedienteSelectForm form = new ExpedienteSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _expediente = form.Selected as ExpedientInfo;
                Expediente_TB.Text = _expediente.Codigo;
            }
        }

        #endregion

        #region Events

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_TB.Enabled = !TodosProducto_CkB.Checked;
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
        }

        private void TodosProveedor_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Expediente_TB.Enabled = !TodosExpediente_CkB.Checked;
            Expediente_BT.Enabled = !TodosExpediente_CkB.Checked;
            TipoExpediente_CB.Enabled = TodosExpediente_CkB.Checked;
        }

        #endregion

    }
}

