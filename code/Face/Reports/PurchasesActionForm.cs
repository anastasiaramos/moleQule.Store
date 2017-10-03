using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Producto;
using moleQule.Library.Store.Reports.Proveedor;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class PurchasesActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "ComprasActionForm";
        public static Type Type { get { return typeof(PurchasesActionForm); } }

        ProductInfo _producto = null;
        ProveedorInfo _proveedor = null;
        SerieInfo _serie = null;
        ExpedientInfo _expediente = null;
        moleQule.Store.Structs.ETipoExpediente _tipo_expediente;

        #endregion

        #region Factory Methods

        public PurchasesActionForm()
            : this(null) {}

        public PurchasesActionForm(Form parent)
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

			if (!TodosProveedor_CkB.Checked)
				filtro += "Proveedor " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _proveedor.Nombre + "; ";

			if (!TodosProducto_CkB.Checked)
				filtro += "Producto " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _producto.Nombre + "; ";

			if (!TodosExpediente_CkB.Checked)
				filtro += "Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _expediente.Codigo + "; ";
			else
				filtro += "Tipo Expedient " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + moleQule.Store.Structs.EnumText<moleQule.Store.Structs.ETipoExpediente>.GetLabel((ETipoExpediente)_tipo_expediente) + "; ";

			if (!TodosSerie_CkB.Checked)
				filtro += "Serie " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.Equal) + " " + _serie.Nombre + "; ";

			if (FInicial_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + moleQule.CslaEx.EnumText.GetOperator(moleQule.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		#endregion

        #region Layout

        public override void RefreshSecondaryData ()
        {
            Datos_TiposExp.DataSource = moleQule.Store.Structs.EnumText<moleQule.Store.Structs.ETipoExpediente>.GetList(false);
            TipoExpediente_CB.SelectedItem = ComboBoxSourceList.Get(Datos_TiposExp.DataSource, (long)moleQule.Store.Structs.ETipoExpediente.Todos);
            PgMng.Grow();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions();

            conditions.Proveedor = TodosProveedor_CkB.Checked ? null : _proveedor;
			conditions.Producto = TodosProducto_CkB.Checked ? null : _producto;
			conditions.Serie = TodosSerie_CkB.Checked ? null : _serie;
			conditions.Expedient = TodosExpediente_CkB.Checked ? null : _expediente;
			conditions.TipoExpediente = !TodosExpediente_CkB.Checked ? moleQule.Store.Structs.ETipoExpediente.Todos : (moleQule.Store.Structs.ETipoExpediente)(long)TipoExpediente_CB.SelectedValue;
            conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

            string filtro = GetFilterValues();

            if (Proveedor_RB.Checked)
            {
                ComprasList list = ComprasList.GetListByProveedor(conditions);
                PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                ProveedorReportMng reportMng = new ProveedorReportMng(AppContext.ActiveSchema, string.Empty, filtro);
                InformeComprasRpt rpt = reportMng.GetComprasReport(list);
                PgMng.FillUp();

				ShowReport(rpt);
            }
            else 
            {
                ComprasList list = ComprasList.GetListByProducto(conditions);
                PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                ProductReportMng reportMng = new ProductReportMng(AppContext.ActiveSchema, string.Empty, filtro);
                InformeComprasProductosRpt rpt = reportMng.GetComprasProductosReport(list);
                PgMng.FillUp();

				ShowReport(rpt);
			}

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Buttons

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
			ProveedorList list = ProveedorList.GetList(moleQule.Base.EEstado.Active, false);
            SupplierSelectForm form = new SupplierSelectForm(this, list);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _proveedor = form.Selected as ProveedorInfo;
                Proveedor_TB.Text = _proveedor.Nombre;
            }
        }

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _producto = form.Selected as ProductInfo;
                Producto_TB.Text = _producto.Nombre;
            }
        }

        private void Serie_BT_Click(object sender, EventArgs e)
        {
            SerieSelectForm form = new SerieSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _serie = form.Selected as SerieInfo;
                Serie_TB.Text = _serie.Nombre;
            }
        }

        private void Expediente_BT_Click(object sender, EventArgs e)
        {
            ExpedienteSelectForm form = new ExpedienteSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _expediente = form.Selected as ExpedientInfo;
                _tipo_expediente = moleQule.Store.Structs.ETipoExpediente.Todos;
                Expediente_TB.Text = _expediente.Codigo;
            }
        }

        #endregion

        #region Events

        private void Todos_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Proveedor_BT.Enabled = !TodosProveedor_CkB.Checked;
        }

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

        private void TodosExpediente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Expediente_BT.Enabled = !TodosExpediente_CkB.Checked;
            TipoExpediente_CB.Enabled = TodosExpediente_CkB.Checked;
        }

        #endregion
    }
}