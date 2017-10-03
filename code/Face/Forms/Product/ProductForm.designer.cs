namespace moleQule.Face.Store
{
    partial class ProductForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductForm));
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label precioCompraLabel;
            System.Windows.Forms.Label precioVentaLabel;
            System.Windows.Forms.Label ayudaLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label nombreSerieLabel;
            System.Windows.Forms.Label serieLabel;
            System.Windows.Forms.Label Codigo_LB;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Stock_Panel = new System.Windows.Forms.SplitContainer();
            this.Stock_TS = new System.Windows.Forms.ToolStrip();
            this.AddStock_TI = new System.Windows.Forms.ToolStripButton();
            this.EditStock_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.DeleteStock_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Stock_DGW = new System.Windows.Forms.DataGridView();
            this.TipoStockLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAlbaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STIDBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StFacturacionPeso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Bulto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Bultos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BultosActuales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KilosActuales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StStoreID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StExpedient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Components_Panel = new System.Windows.Forms.SplitContainer();
            this.Components_TS = new System.Windows.Forms.ToolStrip();
            this.AddComponent_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteComponent_TI = new System.Windows.Forms.ToolStripButton();
            this.Components_DGW = new System.Windows.Forms.DataGridView();
            this.CPProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datos_Components = new System.Windows.Forms.BindingSource(this.components);
            this.Parts_TC = new System.Windows.Forms.TabControl();
            this.General_TP = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SetEstado_BT = new System.Windows.Forms.Button();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Otros_GB = new System.Windows.Forms.GroupBox();
            this.ExternalCode_TB = new System.Windows.Forms.TextBox();
            this.KilosBulto_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox1 = new moleQule.Face.Controls.NumericTextBox();
            this.Grant_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Compra_GB = new System.Windows.Forms.GroupBox();
            this.CuentaContableCompra_TB = new System.Windows.Forms.MaskedTextBox();
            this.CuentaContableCompra_BT = new System.Windows.Forms.Button();
            this.PurchasePrice_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.DefectoCompra_BT = new System.Windows.Forms.Button();
            this.ImpuestoCompra_BT = new System.Windows.Forms.Button();
            this.ImpuestoCompra_TB = new System.Windows.Forms.TextBox();
            this.Venta_GB = new System.Windows.Forms.GroupBox();
            this.PBeneficioMinimo_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.NoStockSale_CkB = new System.Windows.Forms.CheckBox();
            this.Beneficio_CkB = new System.Windows.Forms.CheckBox();
            this.Kit_CkB = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CuentaContableVenta_TB = new System.Windows.Forms.MaskedTextBox();
            this.CuentaContableVenta_BT = new System.Windows.Forms.Button();
            this.DefectoVenta_BT = new System.Windows.Forms.Button();
            this.ImpuestoVenta_BT = new System.Windows.Forms.Button();
            this.numericTextBox2 = new moleQule.Face.Controls.NumericTextBox();
            this.Unitario_CkB = new System.Windows.Forms.CheckBox();
            this.FormaVenta_BT = new System.Windows.Forms.Button();
            this.ImpuestoVenta_TB = new System.Windows.Forms.TextBox();
            this.SalePrice_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.FormaVenta_TB = new System.Windows.Forms.TextBox();
            this.Familia_GB = new System.Windows.Forms.GroupBox();
            this.nombreSerieTextBox = new System.Windows.Forms.TextBox();
            this.FamiliaCode_TB = new System.Windows.Forms.TextBox();
            this.Familia_BT = new System.Windows.Forms.Button();
            this.Partidas_TP = new System.Windows.Forms.TabPage();
            this.Partidas_DGW = new System.Windows.Forms.DataGridView();
            this.Batch_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Stock_TP = new System.Windows.Forms.TabPage();
            this.Observaciones_TP = new System.Windows.Forms.TabPage();
            this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.Components_TP = new System.Windows.Forms.TabPage();
            this.IDBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAlbaranPartida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFacturaPartida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaStoreID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expediente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KilosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BultosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KiloPorBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompraKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompraBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GastoKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CosteKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CosteKgAyuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CosteNeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVentaBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeneficioKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeneficioEstimado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            nombreLabel = new System.Windows.Forms.Label();
            precioCompraLabel = new System.Windows.Forms.Label();
            precioVentaLabel = new System.Windows.Forms.Label();
            ayudaLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            nombreSerieLabel = new System.Windows.Forms.Label();
            serieLabel = new System.Windows.Forms.Label();
            Codigo_LB = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).BeginInit();
            this.Pie_Panel.Panel1.SuspendLayout();
            this.Pie_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).BeginInit();
            this.Content_Panel.Panel2.SuspendLayout();
            this.Content_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stock_Panel)).BeginInit();
            this.Stock_Panel.Panel1.SuspendLayout();
            this.Stock_Panel.Panel2.SuspendLayout();
            this.Stock_Panel.SuspendLayout();
            this.Stock_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stock_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Components_Panel)).BeginInit();
            this.Components_Panel.Panel1.SuspendLayout();
            this.Components_Panel.Panel2.SuspendLayout();
            this.Components_Panel.SuspendLayout();
            this.Components_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Components_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Components)).BeginInit();
            this.Parts_TC.SuspendLayout();
            this.General_TP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Otros_GB.SuspendLayout();
            this.Compra_GB.SuspendLayout();
            this.Venta_GB.SuspendLayout();
            this.Familia_GB.SuspendLayout();
            this.Partidas_TP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Partidas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Batch_BS)).BeginInit();
            this.Stock_TP.SuspendLayout();
            this.Observaciones_TP.SuspendLayout();
            this.Components_TP.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
            resources.ApplyResources(this.PanelesV, "PanelesV");
            // 
            // Submit_BT
            // 
            resources.ApplyResources(this.Submit_BT, "Submit_BT");
            this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
            // 
            // Cancel_BT
            // 
            resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
            this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
            // 
            // Pie_Panel
            // 
            // 
            // Pie_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel1, ((bool)(resources.GetObject("Pie_Panel.Panel1.ShowHelp"))));
            // 
            // Pie_Panel.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel2, ((bool)(resources.GetObject("Pie_Panel.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Pie_Panel, ((bool)(resources.GetObject("Pie_Panel.ShowHelp"))));
            resources.ApplyResources(this.Pie_Panel, "Pie_Panel");
            // 
            // Content_Panel
            // 
            // 
            // Content_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel1, ((bool)(resources.GetObject("Content_Panel.Panel1.ShowHelp"))));
            // 
            // Content_Panel.Panel2
            // 
            this.Content_Panel.Panel2.Controls.Add(this.Parts_TC);
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel2, ((bool)(resources.GetObject("Content_Panel.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Content_Panel, ((bool)(resources.GetObject("Content_Panel.ShowHelp"))));
            resources.ApplyResources(this.Content_Panel, "Content_Panel");
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Product);
            // 
            // Progress_Panel
            // 
            resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
            // 
            // ProgressBK_Panel
            // 
            resources.ApplyResources(this.ProgressBK_Panel, "ProgressBK_Panel");
            // 
            // ProgressInfo_PB
            // 
            resources.ApplyResources(this.ProgressInfo_PB, "ProgressInfo_PB");
            // 
            // Progress_PB
            // 
            resources.ApplyResources(this.Progress_PB, "Progress_PB");
            // 
            // nombreLabel
            // 
            resources.ApplyResources(nombreLabel, "nombreLabel");
            nombreLabel.Name = "nombreLabel";
            this.HelpProvider.SetShowHelp(nombreLabel, ((bool)(resources.GetObject("nombreLabel.ShowHelp"))));
            // 
            // precioCompraLabel
            // 
            resources.ApplyResources(precioCompraLabel, "precioCompraLabel");
            precioCompraLabel.Name = "precioCompraLabel";
            this.HelpProvider.SetShowHelp(precioCompraLabel, ((bool)(resources.GetObject("precioCompraLabel.ShowHelp"))));
            // 
            // precioVentaLabel
            // 
            resources.ApplyResources(precioVentaLabel, "precioVentaLabel");
            precioVentaLabel.Name = "precioVentaLabel";
            this.HelpProvider.SetShowHelp(precioVentaLabel, ((bool)(resources.GetObject("precioVentaLabel.ShowHelp"))));
            // 
            // ayudaLabel
            // 
            resources.ApplyResources(ayudaLabel, "ayudaLabel");
            ayudaLabel.Name = "ayudaLabel";
            this.HelpProvider.SetShowHelp(ayudaLabel, ((bool)(resources.GetObject("ayudaLabel.ShowHelp"))));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            this.HelpProvider.SetShowHelp(label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
            // 
            // nombreSerieLabel
            // 
            resources.ApplyResources(nombreSerieLabel, "nombreSerieLabel");
            nombreSerieLabel.Name = "nombreSerieLabel";
            this.HelpProvider.SetShowHelp(nombreSerieLabel, ((bool)(resources.GetObject("nombreSerieLabel.ShowHelp"))));
            // 
            // serieLabel
            // 
            resources.ApplyResources(serieLabel, "serieLabel");
            serieLabel.Name = "serieLabel";
            this.HelpProvider.SetShowHelp(serieLabel, ((bool)(resources.GetObject("serieLabel.ShowHelp"))));
            // 
            // Codigo_LB
            // 
            resources.ApplyResources(Codigo_LB, "Codigo_LB");
            Codigo_LB.Name = "Codigo_LB";
            this.HelpProvider.SetShowHelp(Codigo_LB, ((bool)(resources.GetObject("Codigo_LB.ShowHelp"))));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            this.HelpProvider.SetShowHelp(label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            this.HelpProvider.SetShowHelp(label18, ((bool)(resources.GetObject("label18.ShowHelp"))));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            this.HelpProvider.SetShowHelp(label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            this.HelpProvider.SetShowHelp(label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            this.HelpProvider.SetShowHelp(label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            this.HelpProvider.SetShowHelp(label6, ((bool)(resources.GetObject("label6.ShowHelp"))));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            this.HelpProvider.SetShowHelp(label7, ((bool)(resources.GetObject("label7.ShowHelp"))));
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            this.HelpProvider.SetShowHelp(label9, ((bool)(resources.GetObject("label9.ShowHelp"))));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            this.HelpProvider.SetShowHelp(label11, ((bool)(resources.GetObject("label11.ShowHelp"))));
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            this.HelpProvider.SetShowHelp(label12, ((bool)(resources.GetObject("label12.ShowHelp"))));
            // 
            // Stock_Panel
            // 
            resources.ApplyResources(this.Stock_Panel, "Stock_Panel");
            this.Stock_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Stock_Panel.Name = "Stock_Panel";
            // 
            // Stock_Panel.Panel1
            // 
            this.Stock_Panel.Panel1.Controls.Add(this.Stock_TS);
            this.HelpProvider.SetShowHelp(this.Stock_Panel.Panel1, ((bool)(resources.GetObject("Stock_Panel.Panel1.ShowHelp"))));
            this.Stock_Panel.Panel1Collapsed = true;
            // 
            // Stock_Panel.Panel2
            // 
            this.Stock_Panel.Panel2.Controls.Add(this.Stock_DGW);
            this.HelpProvider.SetShowHelp(this.Stock_Panel.Panel2, ((bool)(resources.GetObject("Stock_Panel.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Stock_Panel, ((bool)(resources.GetObject("Stock_Panel.ShowHelp"))));
            // 
            // Stock_TS
            // 
            this.Stock_TS.GripMargin = new System.Windows.Forms.Padding(0);
            this.Stock_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Stock_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddStock_TI,
            this.EditStock_TI,
            this.toolStripButton3,
            this.DeleteStock_TI,
            this.toolStripLabel2});
            this.Stock_TS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.Stock_TS, "Stock_TS");
            this.Stock_TS.Name = "Stock_TS";
            this.HelpProvider.SetShowHelp(this.Stock_TS, ((bool)(resources.GetObject("Stock_TS.ShowHelp"))));
            // 
            // AddStock_TI
            // 
            this.AddStock_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddStock_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            resources.ApplyResources(this.AddStock_TI, "AddStock_TI");
            this.AddStock_TI.Name = "AddStock_TI";
            // 
            // EditStock_TI
            // 
            this.EditStock_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditStock_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            resources.ApplyResources(this.EditStock_TI, "EditStock_TI");
            this.EditStock_TI.Name = "EditStock_TI";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // DeleteStock_TI
            // 
            this.DeleteStock_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteStock_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            resources.ApplyResources(this.DeleteStock_TI, "DeleteStock_TI");
            this.DeleteStock_TI.Name = "DeleteStock_TI";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            // 
            // Stock_DGW
            // 
            this.Stock_DGW.AllowUserToAddRows = false;
            this.Stock_DGW.AllowUserToDeleteRows = false;
            this.Stock_DGW.AllowUserToOrderColumns = true;
            this.Stock_DGW.AllowUserToResizeRows = false;
            this.Stock_DGW.AutoGenerateColumns = false;
            resources.ApplyResources(this.Stock_DGW, "Stock_DGW");
            this.Stock_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TipoStockLabel,
            this.Cliente,
            this.NAlbaran,
            this.NFactura,
            this.STIDBatch,
            this.StFecha,
            this.StFacturacionPeso,
            this.Bulto,
            this.Bultos,
            this.Kilos,
            this.BultosActuales,
            this.KilosActuales,
            this.StStoreID,
            this.StExpedient,
            this.Observaciones,
            this.StUser});
            this.Stock_DGW.DataSource = this.Stock_BS;
            this.Stock_DGW.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Stock_DGW.MultiSelect = false;
            this.Stock_DGW.Name = "Stock_DGW";
            this.Stock_DGW.ReadOnly = true;
            this.Stock_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HelpProvider.SetShowHelp(this.Stock_DGW, ((bool)(resources.GetObject("Stock_DGW.ShowHelp"))));
            // 
            // TipoStockLabel
            // 
            this.TipoStockLabel.DataPropertyName = "TipoStockLabel";
            resources.ApplyResources(this.TipoStockLabel, "TipoStockLabel");
            this.TipoStockLabel.Name = "TipoStockLabel";
            this.TipoStockLabel.ReadOnly = true;
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "Cliente";
            resources.ApplyResources(this.Cliente, "Cliente");
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // NAlbaran
            // 
            this.NAlbaran.DataPropertyName = "NAlbaran";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NAlbaran.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.NAlbaran, "NAlbaran");
            this.NAlbaran.Name = "NAlbaran";
            this.NAlbaran.ReadOnly = true;
            // 
            // NFactura
            // 
            this.NFactura.DataPropertyName = "NFactura";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NFactura.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.NFactura, "NFactura");
            this.NFactura.Name = "NFactura";
            this.NFactura.ReadOnly = true;
            // 
            // STIDBatch
            // 
            this.STIDBatch.DataPropertyName = "IDPartida";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STIDBatch.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.STIDBatch, "STIDBatch");
            this.STIDBatch.Name = "STIDBatch";
            this.STIDBatch.ReadOnly = true;
            // 
            // StFecha
            // 
            this.StFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle19.NullValue = null;
            this.StFecha.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.StFecha, "StFecha");
            this.StFecha.Name = "StFecha";
            this.StFecha.ReadOnly = true;
            // 
            // StFacturacionPeso
            // 
            this.StFacturacionPeso.DataPropertyName = "FacturacionPeso";
            resources.ApplyResources(this.StFacturacionPeso, "StFacturacionPeso");
            this.StFacturacionPeso.Name = "StFacturacionPeso";
            this.StFacturacionPeso.ReadOnly = true;
            // 
            // Bulto
            // 
            this.Bulto.DataPropertyName = "Bulto";
            resources.ApplyResources(this.Bulto, "Bulto");
            this.Bulto.Name = "Bulto";
            this.Bulto.ReadOnly = true;
            // 
            // Bultos
            // 
            this.Bultos.DataPropertyName = "Bultos";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "N2";
            dataGridViewCellStyle20.NullValue = null;
            this.Bultos.DefaultCellStyle = dataGridViewCellStyle20;
            resources.ApplyResources(this.Bultos, "Bultos");
            this.Bultos.Name = "Bultos";
            this.Bultos.ReadOnly = true;
            // 
            // Kilos
            // 
            this.Kilos.DataPropertyName = "Kilos";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = null;
            this.Kilos.DefaultCellStyle = dataGridViewCellStyle21;
            resources.ApplyResources(this.Kilos, "Kilos");
            this.Kilos.Name = "Kilos";
            this.Kilos.ReadOnly = true;
            // 
            // BultosActuales
            // 
            this.BultosActuales.DataPropertyName = "BultosActuales";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle22.Format = "N2";
            dataGridViewCellStyle22.NullValue = null;
            this.BultosActuales.DefaultCellStyle = dataGridViewCellStyle22;
            resources.ApplyResources(this.BultosActuales, "BultosActuales");
            this.BultosActuales.Name = "BultosActuales";
            this.BultosActuales.ReadOnly = true;
            // 
            // KilosActuales
            // 
            this.KilosActuales.DataPropertyName = "KilosActuales";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "N2";
            dataGridViewCellStyle23.NullValue = null;
            this.KilosActuales.DefaultCellStyle = dataGridViewCellStyle23;
            resources.ApplyResources(this.KilosActuales, "KilosActuales");
            this.KilosActuales.Name = "KilosActuales";
            this.KilosActuales.ReadOnly = true;
            // 
            // StStoreID
            // 
            this.StStoreID.DataPropertyName = "StoreID";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StStoreID.DefaultCellStyle = dataGridViewCellStyle24;
            resources.ApplyResources(this.StStoreID, "StStoreID");
            this.StStoreID.Name = "StStoreID";
            this.StStoreID.ReadOnly = true;
            // 
            // StExpedient
            // 
            this.StExpedient.DataPropertyName = "Expediente";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StExpedient.DefaultCellStyle = dataGridViewCellStyle25;
            resources.ApplyResources(this.StExpedient, "StExpedient");
            this.StExpedient.Name = "StExpedient";
            this.StExpedient.ReadOnly = true;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            resources.ApplyResources(this.Observaciones, "Observaciones");
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            // 
            // StUser
            // 
            this.StUser.DataPropertyName = "Usuario";
            resources.ApplyResources(this.StUser, "StUser");
            this.StUser.Name = "StUser";
            this.StUser.ReadOnly = true;
            // 
            // Stock_BS
            // 
            this.Stock_BS.DataSource = typeof(moleQule.Library.Store.Stock);
            // 
            // Components_Panel
            // 
            resources.ApplyResources(this.Components_Panel, "Components_Panel");
            this.Components_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Components_Panel.Name = "Components_Panel";
            // 
            // Components_Panel.Panel1
            // 
            this.Components_Panel.Panel1.Controls.Add(this.Components_TS);
            this.HelpProvider.SetShowHelp(this.Components_Panel.Panel1, ((bool)(resources.GetObject("Components_Panel.Panel1.ShowHelp"))));
            // 
            // Components_Panel.Panel2
            // 
            this.Components_Panel.Panel2.Controls.Add(this.Components_DGW);
            this.HelpProvider.SetShowHelp(this.Components_Panel.Panel2, ((bool)(resources.GetObject("Components_Panel.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Components_Panel, ((bool)(resources.GetObject("Components_Panel.ShowHelp"))));
            // 
            // Components_TS
            // 
            this.Components_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Components_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddComponent_TI,
            this.DeleteComponent_TI});
            resources.ApplyResources(this.Components_TS, "Components_TS");
            this.Components_TS.Name = "Components_TS";
            this.HelpProvider.SetShowHelp(this.Components_TS, ((bool)(resources.GetObject("Components_TS.ShowHelp"))));
            // 
            // AddComponent_TI
            // 
            this.AddComponent_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddComponent_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            resources.ApplyResources(this.AddComponent_TI, "AddComponent_TI");
            this.AddComponent_TI.Name = "AddComponent_TI";
            this.AddComponent_TI.Click += new System.EventHandler(this.AddComponent_TI_Click);
            // 
            // DeleteComponent_TI
            // 
            this.DeleteComponent_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteComponent_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            resources.ApplyResources(this.DeleteComponent_TI, "DeleteComponent_TI");
            this.DeleteComponent_TI.Name = "DeleteComponent_TI";
            this.DeleteComponent_TI.Click += new System.EventHandler(this.DeleteComponent_TI_Click);
            // 
            // Components_DGW
            // 
            this.Components_DGW.AllowUserToAddRows = false;
            this.Components_DGW.AllowUserToDeleteRows = false;
            this.Components_DGW.AutoGenerateColumns = false;
            resources.ApplyResources(this.Components_DGW, "Components_DGW");
            this.Components_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CPProduct,
            this.CPAmount});
            this.Components_DGW.DataSource = this.Datos_Components;
            this.Components_DGW.Name = "Components_DGW";
            this.HelpProvider.SetShowHelp(this.Components_DGW, ((bool)(resources.GetObject("Components_DGW.ShowHelp"))));
            this.Components_DGW.DoubleClick += new System.EventHandler(this.Components_DGW_DoubleClick);
            // 
            // CPProduct
            // 
            this.CPProduct.DataPropertyName = "Product";
            resources.ApplyResources(this.CPProduct, "CPProduct");
            this.CPProduct.Name = "CPProduct";
            this.CPProduct.ReadOnly = true;
            // 
            // CPAmount
            // 
            this.CPAmount.DataPropertyName = "Amount";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Format = "N2";
            dataGridViewCellStyle26.NullValue = null;
            this.CPAmount.DefaultCellStyle = dataGridViewCellStyle26;
            resources.ApplyResources(this.CPAmount, "CPAmount");
            this.CPAmount.Name = "CPAmount";
            // 
            // Datos_Components
            // 
            this.Datos_Components.DataSource = typeof(moleQule.Library.Store.Kit);
            // 
            // Parts_TC
            // 
            this.Parts_TC.Controls.Add(this.General_TP);
            this.Parts_TC.Controls.Add(this.Partidas_TP);
            this.Parts_TC.Controls.Add(this.Stock_TP);
            this.Parts_TC.Controls.Add(this.Observaciones_TP);
            this.Parts_TC.Controls.Add(this.Components_TP);
            resources.ApplyResources(this.Parts_TC, "Parts_TC");
            this.Parts_TC.Multiline = true;
            this.Parts_TC.Name = "Parts_TC";
            this.Parts_TC.SelectedIndex = 0;
            this.HelpProvider.SetShowHelp(this.Parts_TC, ((bool)(resources.GetObject("Parts_TC.ShowHelp"))));
            this.Parts_TC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Parts_TC.SelectedIndexChanged += new System.EventHandler(this.Ficha_SelectedIndexChanged);
            // 
            // General_TP
            // 
            resources.ApplyResources(this.General_TP, "General_TP");
            this.General_TP.Controls.Add(this.groupBox1);
            this.General_TP.Controls.Add(this.Otros_GB);
            this.General_TP.Controls.Add(this.Compra_GB);
            this.General_TP.Controls.Add(this.Venta_GB);
            this.General_TP.Controls.Add(this.Familia_GB);
            this.General_TP.Name = "General_TP";
            this.HelpProvider.SetShowHelp(this.General_TP, ((bool)(resources.GetObject("General_TP.ShowHelp"))));
            this.General_TP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.SetEstado_BT);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.nombreTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(nombreLabel);
            this.groupBox1.Controls.Add(this.Codigo_TB);
            this.groupBox1.Controls.Add(this.Estado_TB);
            this.groupBox1.Controls.Add(Codigo_LB);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.HelpProvider.SetShowHelp(this.groupBox1, ((bool)(resources.GetObject("groupBox1.ShowHelp"))));
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Descripcion", true));
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.HelpProvider.SetShowHelp(this.textBox1, ((bool)(resources.GetObject("textBox1.ShowHelp"))));
            // 
            // SetEstado_BT
            // 
            this.SetEstado_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            resources.ApplyResources(this.SetEstado_BT, "SetEstado_BT");
            this.SetEstado_BT.Name = "SetEstado_BT";
            this.HelpProvider.SetShowHelp(this.SetEstado_BT, ((bool)(resources.GetObject("SetEstado_BT.ShowHelp"))));
            this.SetEstado_BT.UseVisualStyleBackColor = true;
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
            resources.ApplyResources(this.nombreTextBox, "nombreTextBox");
            this.nombreTextBox.Name = "nombreTextBox";
            this.HelpProvider.SetShowHelp(this.nombreTextBox, ((bool)(resources.GetObject("nombreTextBox.ShowHelp"))));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.HelpProvider.SetShowHelp(this.label8, ((bool)(resources.GetObject("label8.ShowHelp"))));
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            resources.ApplyResources(this.Codigo_TB, "Codigo_TB");
            this.Codigo_TB.Name = "Codigo_TB";
            this.HelpProvider.SetShowHelp(this.Codigo_TB, ((bool)(resources.GetObject("Codigo_TB.ShowHelp"))));
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            resources.ApplyResources(this.Estado_TB, "Estado_TB");
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.Estado_TB, ((bool)(resources.GetObject("Estado_TB.ShowHelp"))));
            // 
            // Otros_GB
            // 
            this.Otros_GB.Controls.Add(this.ExternalCode_TB);
            this.Otros_GB.Controls.Add(label12);
            this.Otros_GB.Controls.Add(label11);
            this.Otros_GB.Controls.Add(label9);
            this.Otros_GB.Controls.Add(this.KilosBulto_NTB);
            this.Otros_GB.Controls.Add(this.numericTextBox1);
            this.Otros_GB.Controls.Add(label1);
            this.Otros_GB.Controls.Add(ayudaLabel);
            this.Otros_GB.Controls.Add(this.Grant_NTB);
            resources.ApplyResources(this.Otros_GB, "Otros_GB");
            this.Otros_GB.Name = "Otros_GB";
            this.HelpProvider.SetShowHelp(this.Otros_GB, ((bool)(resources.GetObject("Otros_GB.ShowHelp"))));
            this.Otros_GB.TabStop = false;
            // 
            // ExternalCode_TB
            // 
            this.ExternalCode_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ExternalCode", true));
            resources.ApplyResources(this.ExternalCode_TB, "ExternalCode_TB");
            this.ExternalCode_TB.Name = "ExternalCode_TB";
            this.HelpProvider.SetShowHelp(this.ExternalCode_TB, ((bool)(resources.GetObject("ExternalCode_TB.ShowHelp"))));
            // 
            // KilosBulto_NTB
            // 
            this.KilosBulto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "KilosBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            resources.ApplyResources(this.KilosBulto_NTB, "KilosBulto_NTB");
            this.KilosBulto_NTB.Name = "KilosBulto_NTB";
            this.HelpProvider.SetShowHelp(this.KilosBulto_NTB, ((bool)(resources.GetObject("KilosBulto_NTB.ShowHelp"))));
            this.KilosBulto_NTB.TextIsCurrency = false;
            this.KilosBulto_NTB.TextIsInteger = false;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CodigoAduanero", true));
            resources.ApplyResources(this.numericTextBox1, "numericTextBox1");
            this.numericTextBox1.Name = "numericTextBox1";
            this.HelpProvider.SetShowHelp(this.numericTextBox1, ((bool)(resources.GetObject("numericTextBox1.ShowHelp"))));
            this.numericTextBox1.TextIsCurrency = false;
            this.numericTextBox1.TextIsInteger = false;
            // 
            // Grant_NTB
            // 
            this.Grant_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "AyudaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
            resources.ApplyResources(this.Grant_NTB, "Grant_NTB");
            this.Grant_NTB.Name = "Grant_NTB";
            this.HelpProvider.SetShowHelp(this.Grant_NTB, ((bool)(resources.GetObject("Grant_NTB.ShowHelp"))));
            this.Grant_NTB.TextIsCurrency = false;
            this.Grant_NTB.TextIsInteger = false;
            // 
            // Compra_GB
            // 
            this.Compra_GB.Controls.Add(this.CuentaContableCompra_TB);
            this.Compra_GB.Controls.Add(label5);
            this.Compra_GB.Controls.Add(this.CuentaContableCompra_BT);
            this.Compra_GB.Controls.Add(this.PurchasePrice_NTB);
            this.Compra_GB.Controls.Add(this.DefectoCompra_BT);
            this.Compra_GB.Controls.Add(precioCompraLabel);
            this.Compra_GB.Controls.Add(this.ImpuestoCompra_BT);
            this.Compra_GB.Controls.Add(this.ImpuestoCompra_TB);
            this.Compra_GB.Controls.Add(label3);
            resources.ApplyResources(this.Compra_GB, "Compra_GB");
            this.Compra_GB.Name = "Compra_GB";
            this.HelpProvider.SetShowHelp(this.Compra_GB, ((bool)(resources.GetObject("Compra_GB.ShowHelp"))));
            this.Compra_GB.TabStop = false;
            // 
            // CuentaContableCompra_TB
            // 
            this.CuentaContableCompra_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContableCompra", true));
            resources.ApplyResources(this.CuentaContableCompra_TB, "CuentaContableCompra_TB");
            this.CuentaContableCompra_TB.Name = "CuentaContableCompra_TB";
            this.HelpProvider.SetShowHelp(this.CuentaContableCompra_TB, ((bool)(resources.GetObject("CuentaContableCompra_TB.ShowHelp"))));
            // 
            // CuentaContableCompra_BT
            // 
            this.CuentaContableCompra_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            resources.ApplyResources(this.CuentaContableCompra_BT, "CuentaContableCompra_BT");
            this.CuentaContableCompra_BT.Name = "CuentaContableCompra_BT";
            this.HelpProvider.SetShowHelp(this.CuentaContableCompra_BT, ((bool)(resources.GetObject("CuentaContableCompra_BT.ShowHelp"))));
            this.CuentaContableCompra_BT.UseVisualStyleBackColor = true;
            // 
            // PurchasePrice_NTB
            // 
            this.PurchasePrice_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioCompra", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
            resources.ApplyResources(this.PurchasePrice_NTB, "PurchasePrice_NTB");
            this.PurchasePrice_NTB.Name = "PurchasePrice_NTB";
            this.HelpProvider.SetShowHelp(this.PurchasePrice_NTB, ((bool)(resources.GetObject("PurchasePrice_NTB.ShowHelp"))));
            this.PurchasePrice_NTB.TextIsCurrency = false;
            this.PurchasePrice_NTB.TextIsInteger = false;
            // 
            // DefectoCompra_BT
            // 
            this.DefectoCompra_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            resources.ApplyResources(this.DefectoCompra_BT, "DefectoCompra_BT");
            this.DefectoCompra_BT.Name = "DefectoCompra_BT";
            this.HelpProvider.SetShowHelp(this.DefectoCompra_BT, ((bool)(resources.GetObject("DefectoCompra_BT.ShowHelp"))));
            this.DefectoCompra_BT.UseVisualStyleBackColor = true;
            // 
            // ImpuestoCompra_BT
            // 
            this.ImpuestoCompra_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            resources.ApplyResources(this.ImpuestoCompra_BT, "ImpuestoCompra_BT");
            this.ImpuestoCompra_BT.Name = "ImpuestoCompra_BT";
            this.HelpProvider.SetShowHelp(this.ImpuestoCompra_BT, ((bool)(resources.GetObject("ImpuestoCompra_BT.ShowHelp"))));
            this.ImpuestoCompra_BT.UseVisualStyleBackColor = true;
            // 
            // ImpuestoCompra_TB
            // 
            this.ImpuestoCompra_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ImpuestoCompra", true));
            resources.ApplyResources(this.ImpuestoCompra_TB, "ImpuestoCompra_TB");
            this.ImpuestoCompra_TB.Name = "ImpuestoCompra_TB";
            this.ImpuestoCompra_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.ImpuestoCompra_TB, ((bool)(resources.GetObject("ImpuestoCompra_TB.ShowHelp"))));
            // 
            // Venta_GB
            // 
            this.Venta_GB.Controls.Add(this.PBeneficioMinimo_NTB);
            this.Venta_GB.Controls.Add(this.NoStockSale_CkB);
            this.Venta_GB.Controls.Add(this.Beneficio_CkB);
            this.Venta_GB.Controls.Add(this.Kit_CkB);
            this.Venta_GB.Controls.Add(this.label10);
            this.Venta_GB.Controls.Add(this.checkBox1);
            this.Venta_GB.Controls.Add(this.CuentaContableVenta_TB);
            this.Venta_GB.Controls.Add(this.CuentaContableVenta_BT);
            this.Venta_GB.Controls.Add(label4);
            this.Venta_GB.Controls.Add(this.DefectoVenta_BT);
            this.Venta_GB.Controls.Add(label6);
            this.Venta_GB.Controls.Add(this.ImpuestoVenta_BT);
            this.Venta_GB.Controls.Add(this.numericTextBox2);
            this.Venta_GB.Controls.Add(label18);
            this.Venta_GB.Controls.Add(this.Unitario_CkB);
            this.Venta_GB.Controls.Add(this.FormaVenta_BT);
            this.Venta_GB.Controls.Add(this.ImpuestoVenta_TB);
            this.Venta_GB.Controls.Add(label7);
            this.Venta_GB.Controls.Add(this.SalePrice_NTB);
            this.Venta_GB.Controls.Add(this.FormaVenta_TB);
            this.Venta_GB.Controls.Add(precioVentaLabel);
            resources.ApplyResources(this.Venta_GB, "Venta_GB");
            this.Venta_GB.Name = "Venta_GB";
            this.HelpProvider.SetShowHelp(this.Venta_GB, ((bool)(resources.GetObject("Venta_GB.ShowHelp"))));
            this.Venta_GB.TabStop = false;
            // 
            // PBeneficioMinimo_NTB
            // 
            this.PBeneficioMinimo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PBeneficioMinimo", true));
            resources.ApplyResources(this.PBeneficioMinimo_NTB, "PBeneficioMinimo_NTB");
            this.PBeneficioMinimo_NTB.Name = "PBeneficioMinimo_NTB";
            this.HelpProvider.SetShowHelp(this.PBeneficioMinimo_NTB, ((bool)(resources.GetObject("PBeneficioMinimo_NTB.ShowHelp"))));
            this.PBeneficioMinimo_NTB.TextIsCurrency = false;
            this.PBeneficioMinimo_NTB.TextIsInteger = false;
            // 
            // NoStockSale_CkB
            // 
            this.NoStockSale_CkB.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "NoStockSale", true));
            resources.ApplyResources(this.NoStockSale_CkB, "NoStockSale_CkB");
            this.NoStockSale_CkB.Name = "NoStockSale_CkB";
            this.HelpProvider.SetShowHelp(this.NoStockSale_CkB, ((bool)(resources.GetObject("NoStockSale_CkB.ShowHelp"))));
            this.NoStockSale_CkB.UseVisualStyleBackColor = true;
            // 
            // Beneficio_CkB
            // 
            this.Beneficio_CkB.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "BeneficioCero", true));
            resources.ApplyResources(this.Beneficio_CkB, "Beneficio_CkB");
            this.Beneficio_CkB.Name = "Beneficio_CkB";
            this.HelpProvider.SetShowHelp(this.Beneficio_CkB, ((bool)(resources.GetObject("Beneficio_CkB.ShowHelp"))));
            this.Beneficio_CkB.UseVisualStyleBackColor = true;
            // 
            // Kit_CkB
            // 
            this.Kit_CkB.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "IsKit", true));
            resources.ApplyResources(this.Kit_CkB, "Kit_CkB");
            this.Kit_CkB.Name = "Kit_CkB";
            this.HelpProvider.SetShowHelp(this.Kit_CkB, ((bool)(resources.GetObject("Kit_CkB.ShowHelp"))));
            this.Kit_CkB.UseVisualStyleBackColor = true;
            this.Kit_CkB.CheckedChanged += new System.EventHandler(this.Kit_CkB_CheckedChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.HelpProvider.SetShowHelp(this.label10, ((bool)(resources.GetObject("label10.ShowHelp"))));
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "AvisarBeneficioMinimo", true));
            this.checkBox1.Name = "checkBox1";
            this.HelpProvider.SetShowHelp(this.checkBox1, ((bool)(resources.GetObject("checkBox1.ShowHelp"))));
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CuentaContableVenta_TB
            // 
            this.CuentaContableVenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContableVenta", true));
            resources.ApplyResources(this.CuentaContableVenta_TB, "CuentaContableVenta_TB");
            this.CuentaContableVenta_TB.Name = "CuentaContableVenta_TB";
            this.HelpProvider.SetShowHelp(this.CuentaContableVenta_TB, ((bool)(resources.GetObject("CuentaContableVenta_TB.ShowHelp"))));
            // 
            // CuentaContableVenta_BT
            // 
            this.CuentaContableVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            resources.ApplyResources(this.CuentaContableVenta_BT, "CuentaContableVenta_BT");
            this.CuentaContableVenta_BT.Name = "CuentaContableVenta_BT";
            this.HelpProvider.SetShowHelp(this.CuentaContableVenta_BT, ((bool)(resources.GetObject("CuentaContableVenta_BT.ShowHelp"))));
            this.CuentaContableVenta_BT.UseVisualStyleBackColor = true;
            // 
            // DefectoVenta_BT
            // 
            this.DefectoVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            resources.ApplyResources(this.DefectoVenta_BT, "DefectoVenta_BT");
            this.DefectoVenta_BT.Name = "DefectoVenta_BT";
            this.HelpProvider.SetShowHelp(this.DefectoVenta_BT, ((bool)(resources.GetObject("DefectoVenta_BT.ShowHelp"))));
            this.DefectoVenta_BT.UseVisualStyleBackColor = true;
            // 
            // ImpuestoVenta_BT
            // 
            this.ImpuestoVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            resources.ApplyResources(this.ImpuestoVenta_BT, "ImpuestoVenta_BT");
            this.ImpuestoVenta_BT.Name = "ImpuestoVenta_BT";
            this.HelpProvider.SetShowHelp(this.ImpuestoVenta_BT, ((bool)(resources.GetObject("ImpuestoVenta_BT.ShowHelp"))));
            this.ImpuestoVenta_BT.UseVisualStyleBackColor = true;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "StockMinimo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            resources.ApplyResources(this.numericTextBox2, "numericTextBox2");
            this.numericTextBox2.Name = "numericTextBox2";
            this.HelpProvider.SetShowHelp(this.numericTextBox2, ((bool)(resources.GetObject("numericTextBox2.ShowHelp"))));
            this.numericTextBox2.TextIsCurrency = false;
            this.numericTextBox2.TextIsInteger = false;
            // 
            // Unitario_CkB
            // 
            this.Unitario_CkB.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "AvisarStock", true));
            resources.ApplyResources(this.Unitario_CkB, "Unitario_CkB");
            this.Unitario_CkB.Name = "Unitario_CkB";
            this.HelpProvider.SetShowHelp(this.Unitario_CkB, ((bool)(resources.GetObject("Unitario_CkB.ShowHelp"))));
            this.Unitario_CkB.UseVisualStyleBackColor = true;
            // 
            // FormaVenta_BT
            // 
            this.FormaVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            resources.ApplyResources(this.FormaVenta_BT, "FormaVenta_BT");
            this.FormaVenta_BT.Name = "FormaVenta_BT";
            this.HelpProvider.SetShowHelp(this.FormaVenta_BT, ((bool)(resources.GetObject("FormaVenta_BT.ShowHelp"))));
            this.FormaVenta_BT.UseVisualStyleBackColor = true;
            // 
            // ImpuestoVenta_TB
            // 
            this.ImpuestoVenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ImpuestoVenta", true));
            resources.ApplyResources(this.ImpuestoVenta_TB, "ImpuestoVenta_TB");
            this.ImpuestoVenta_TB.Name = "ImpuestoVenta_TB";
            this.ImpuestoVenta_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.ImpuestoVenta_TB, ((bool)(resources.GetObject("ImpuestoVenta_TB.ShowHelp"))));
            // 
            // SalePrice_NTB
            // 
            this.SalePrice_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioVenta", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
            resources.ApplyResources(this.SalePrice_NTB, "SalePrice_NTB");
            this.SalePrice_NTB.Name = "SalePrice_NTB";
            this.HelpProvider.SetShowHelp(this.SalePrice_NTB, ((bool)(resources.GetObject("SalePrice_NTB.ShowHelp"))));
            this.SalePrice_NTB.TextIsCurrency = false;
            this.SalePrice_NTB.TextIsInteger = false;
            // 
            // FormaVenta_TB
            // 
            this.FormaVenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoFacturacionLabel", true));
            resources.ApplyResources(this.FormaVenta_TB, "FormaVenta_TB");
            this.FormaVenta_TB.Name = "FormaVenta_TB";
            this.FormaVenta_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.FormaVenta_TB, ((bool)(resources.GetObject("FormaVenta_TB.ShowHelp"))));
            // 
            // Familia_GB
            // 
            this.Familia_GB.Controls.Add(this.nombreSerieTextBox);
            this.Familia_GB.Controls.Add(nombreSerieLabel);
            this.Familia_GB.Controls.Add(this.FamiliaCode_TB);
            this.Familia_GB.Controls.Add(serieLabel);
            this.Familia_GB.Controls.Add(this.Familia_BT);
            resources.ApplyResources(this.Familia_GB, "Familia_GB");
            this.Familia_GB.Name = "Familia_GB";
            this.HelpProvider.SetShowHelp(this.Familia_GB, ((bool)(resources.GetObject("Familia_GB.ShowHelp"))));
            this.Familia_GB.TabStop = false;
            // 
            // nombreSerieTextBox
            // 
            this.nombreSerieTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Familia", true));
            resources.ApplyResources(this.nombreSerieTextBox, "nombreSerieTextBox");
            this.nombreSerieTextBox.Name = "nombreSerieTextBox";
            this.HelpProvider.SetShowHelp(this.nombreSerieTextBox, ((bool)(resources.GetObject("nombreSerieTextBox.ShowHelp"))));
            // 
            // FamiliaCode_TB
            // 
            this.FamiliaCode_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CodigoFamilia", true));
            resources.ApplyResources(this.FamiliaCode_TB, "FamiliaCode_TB");
            this.FamiliaCode_TB.Name = "FamiliaCode_TB";
            this.FamiliaCode_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.FamiliaCode_TB, ((bool)(resources.GetObject("FamiliaCode_TB.ShowHelp"))));
            // 
            // Familia_BT
            // 
            this.Familia_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            resources.ApplyResources(this.Familia_BT, "Familia_BT");
            this.Familia_BT.Name = "Familia_BT";
            this.HelpProvider.SetShowHelp(this.Familia_BT, ((bool)(resources.GetObject("Familia_BT.ShowHelp"))));
            this.Familia_BT.UseVisualStyleBackColor = true;
            // 
            // Partidas_TP
            // 
            this.Partidas_TP.Controls.Add(this.Partidas_DGW);
            resources.ApplyResources(this.Partidas_TP, "Partidas_TP");
            this.Partidas_TP.Name = "Partidas_TP";
            this.HelpProvider.SetShowHelp(this.Partidas_TP, ((bool)(resources.GetObject("Partidas_TP.ShowHelp"))));
            this.Partidas_TP.UseVisualStyleBackColor = true;
            // 
            // Partidas_DGW
            // 
            this.Partidas_DGW.AllowUserToAddRows = false;
            this.Partidas_DGW.AllowUserToDeleteRows = false;
            this.Partidas_DGW.AllowUserToOrderColumns = true;
            this.Partidas_DGW.AutoGenerateColumns = false;
            resources.ApplyResources(this.Partidas_DGW, "Partidas_DGW");
            this.Partidas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDBatch,
            this.NAlbaranPartida,
            this.NFacturaPartida,
            this.BaStoreID,
            this.Expediente,
            this.KilosIniciales,
            this.BultosIniciales,
            this.KiloPorBulto,
            this.PrecioCompraKilo,
            this.PrecioCompraBulto,
            this.GastoKilo,
            this.CosteKilo,
            this.CosteKgAyuda,
            this.CosteNeto,
            this.PrecioVentaBulto,
            this.BeneficioKilo,
            this.BeneficioEstimado,
            this.Proveedor,
            this.BAUbicacion});
            this.Partidas_DGW.DataSource = this.Batch_BS;
            this.Partidas_DGW.MultiSelect = false;
            this.Partidas_DGW.Name = "Partidas_DGW";
            this.Partidas_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HelpProvider.SetShowHelp(this.Partidas_DGW, ((bool)(resources.GetObject("Partidas_DGW.ShowHelp"))));
            this.Partidas_DGW.SelectionChanged += new System.EventHandler(this.Partidas_DGW_SelectionChanged);
            // 
            // Batch_BS
            // 
            this.Batch_BS.DataSource = typeof(moleQule.Library.Store.Batch);
            // 
            // Stock_TP
            // 
            this.Stock_TP.Controls.Add(this.Stock_Panel);
            resources.ApplyResources(this.Stock_TP, "Stock_TP");
            this.Stock_TP.Name = "Stock_TP";
            this.HelpProvider.SetShowHelp(this.Stock_TP, ((bool)(resources.GetObject("Stock_TP.ShowHelp"))));
            this.Stock_TP.UseVisualStyleBackColor = true;
            // 
            // Observaciones_TP
            // 
            this.Observaciones_TP.Controls.Add(this.observacionesRichTextBox);
            resources.ApplyResources(this.Observaciones_TP, "Observaciones_TP");
            this.Observaciones_TP.Name = "Observaciones_TP";
            this.HelpProvider.SetShowHelp(this.Observaciones_TP, ((bool)(resources.GetObject("Observaciones_TP.ShowHelp"))));
            this.Observaciones_TP.UseVisualStyleBackColor = true;
            // 
            // observacionesRichTextBox
            // 
            this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            resources.ApplyResources(this.observacionesRichTextBox, "observacionesRichTextBox");
            this.observacionesRichTextBox.Name = "observacionesRichTextBox";
            this.HelpProvider.SetShowHelp(this.observacionesRichTextBox, ((bool)(resources.GetObject("observacionesRichTextBox.ShowHelp"))));
            // 
            // Components_TP
            // 
            this.Components_TP.Controls.Add(this.Components_Panel);
            resources.ApplyResources(this.Components_TP, "Components_TP");
            this.Components_TP.Name = "Components_TP";
            this.HelpProvider.SetShowHelp(this.Components_TP, ((bool)(resources.GetObject("Components_TP.ShowHelp"))));
            this.Components_TP.UseVisualStyleBackColor = true;
            // 
            // IDBatch
            // 
            this.IDBatch.DataPropertyName = "Codigo";
            resources.ApplyResources(this.IDBatch, "IDBatch");
            this.IDBatch.Name = "IDBatch";
            // 
            // NAlbaranPartida
            // 
            this.NAlbaranPartida.DataPropertyName = "NAlbaran";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NAlbaranPartida.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.NAlbaranPartida, "NAlbaranPartida");
            this.NAlbaranPartida.Name = "NAlbaranPartida";
            this.NAlbaranPartida.ReadOnly = true;
            // 
            // NFacturaPartida
            // 
            this.NFacturaPartida.DataPropertyName = "NFactura";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NFacturaPartida.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.NFacturaPartida, "NFacturaPartida");
            this.NFacturaPartida.Name = "NFacturaPartida";
            this.NFacturaPartida.ReadOnly = true;
            // 
            // BaStoreID
            // 
            this.BaStoreID.DataPropertyName = "IDAlmacen";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BaStoreID.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.BaStoreID, "BaStoreID");
            this.BaStoreID.Name = "BaStoreID";
            this.BaStoreID.ReadOnly = true;
            // 
            // Expediente
            // 
            this.Expediente.DataPropertyName = "Expediente";
            resources.ApplyResources(this.Expediente, "Expediente");
            this.Expediente.Name = "Expediente";
            // 
            // KilosIniciales
            // 
            this.KilosIniciales.DataPropertyName = "KilosIniciales";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.KilosIniciales.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.KilosIniciales, "KilosIniciales");
            this.KilosIniciales.Name = "KilosIniciales";
            this.KilosIniciales.ReadOnly = true;
            // 
            // BultosIniciales
            // 
            this.BultosIniciales.DataPropertyName = "BultosIniciales";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.BultosIniciales.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.BultosIniciales, "BultosIniciales");
            this.BultosIniciales.Name = "BultosIniciales";
            this.BultosIniciales.ReadOnly = true;
            // 
            // KiloPorBulto
            // 
            this.KiloPorBulto.DataPropertyName = "KilosPorBulto";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.KiloPorBulto.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.KiloPorBulto, "KiloPorBulto");
            this.KiloPorBulto.Name = "KiloPorBulto";
            this.KiloPorBulto.ReadOnly = true;
            // 
            // PrecioCompraKilo
            // 
            this.PrecioCompraKilo.DataPropertyName = "PrecioCompraKilo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N5";
            dataGridViewCellStyle7.NullValue = null;
            this.PrecioCompraKilo.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.PrecioCompraKilo, "PrecioCompraKilo");
            this.PrecioCompraKilo.Name = "PrecioCompraKilo";
            this.PrecioCompraKilo.ReadOnly = true;
            // 
            // PrecioCompraBulto
            // 
            this.PrecioCompraBulto.DataPropertyName = "PrecioCompraBulto";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.PrecioCompraBulto.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.PrecioCompraBulto, "PrecioCompraBulto");
            this.PrecioCompraBulto.Name = "PrecioCompraBulto";
            this.PrecioCompraBulto.ReadOnly = true;
            // 
            // GastoKilo
            // 
            this.GastoKilo.DataPropertyName = "GastoKilo";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N5";
            this.GastoKilo.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.GastoKilo, "GastoKilo");
            this.GastoKilo.Name = "GastoKilo";
            this.GastoKilo.ReadOnly = true;
            // 
            // CosteKilo
            // 
            this.CosteKilo.DataPropertyName = "CosteKilo";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N5";
            dataGridViewCellStyle10.NullValue = null;
            this.CosteKilo.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.CosteKilo, "CosteKilo");
            this.CosteKilo.Name = "CosteKilo";
            this.CosteKilo.ReadOnly = true;
            // 
            // CosteKgAyuda
            // 
            this.CosteKgAyuda.DataPropertyName = "CosteNetoKg";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N5";
            this.CosteKgAyuda.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.CosteKgAyuda, "CosteKgAyuda");
            this.CosteKgAyuda.Name = "CosteKgAyuda";
            this.CosteKgAyuda.ReadOnly = true;
            // 
            // CosteNeto
            // 
            this.CosteNeto.DataPropertyName = "CosteNeto";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            this.CosteNeto.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.CosteNeto, "CosteNeto");
            this.CosteNeto.Name = "CosteNeto";
            this.CosteNeto.ReadOnly = true;
            // 
            // PrecioVentaBulto
            // 
            this.PrecioVentaBulto.DataPropertyName = "PrecioVentaBulto";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = null;
            this.PrecioVentaBulto.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.PrecioVentaBulto, "PrecioVentaBulto");
            this.PrecioVentaBulto.Name = "PrecioVentaBulto";
            this.PrecioVentaBulto.ReadOnly = true;
            // 
            // BeneficioKilo
            // 
            this.BeneficioKilo.DataPropertyName = "BeneficioKilo";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N5";
            this.BeneficioKilo.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.BeneficioKilo, "BeneficioKilo");
            this.BeneficioKilo.Name = "BeneficioKilo";
            this.BeneficioKilo.ReadOnly = true;
            // 
            // BeneficioEstimado
            // 
            this.BeneficioEstimado.DataPropertyName = "BeneficioTotalEstimado";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            this.BeneficioEstimado.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.BeneficioEstimado, "BeneficioEstimado");
            this.BeneficioEstimado.Name = "BeneficioEstimado";
            this.BeneficioEstimado.ReadOnly = true;
            // 
            // Proveedor
            // 
            this.Proveedor.DataPropertyName = "Proveedor";
            resources.ApplyResources(this.Proveedor, "Proveedor");
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            // 
            // BAUbicacion
            // 
            this.BAUbicacion.DataPropertyName = "Ubicacion";
            resources.ApplyResources(this.BAUbicacion, "BAUbicacion");
            this.BAUbicacion.Name = "BAUbicacion";
            // 
            // ProductForm
            // 
            resources.ApplyResources(this, "$this");
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.Name = "ProductForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Pie_Panel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).EndInit();
            this.Pie_Panel.ResumeLayout(false);
            this.Content_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).EndInit();
            this.Content_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.Stock_Panel.Panel1.ResumeLayout(false);
            this.Stock_Panel.Panel1.PerformLayout();
            this.Stock_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Stock_Panel)).EndInit();
            this.Stock_Panel.ResumeLayout(false);
            this.Stock_TS.ResumeLayout(false);
            this.Stock_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stock_BS)).EndInit();
            this.Components_Panel.Panel1.ResumeLayout(false);
            this.Components_Panel.Panel1.PerformLayout();
            this.Components_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Components_Panel)).EndInit();
            this.Components_Panel.ResumeLayout(false);
            this.Components_TS.ResumeLayout(false);
            this.Components_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Components_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Components)).EndInit();
            this.Parts_TC.ResumeLayout(false);
            this.General_TP.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Otros_GB.ResumeLayout(false);
            this.Otros_GB.PerformLayout();
            this.Compra_GB.ResumeLayout(false);
            this.Compra_GB.PerformLayout();
            this.Venta_GB.ResumeLayout(false);
            this.Venta_GB.PerformLayout();
            this.Familia_GB.ResumeLayout(false);
            this.Familia_GB.PerformLayout();
            this.Partidas_TP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Partidas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Batch_BS)).EndInit();
            this.Stock_TP.ResumeLayout(false);
            this.Observaciones_TP.ResumeLayout(false);
            this.Components_TP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabPage General_TP;
        protected System.Windows.Forms.TabControl Parts_TC;
        protected System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        protected System.Windows.Forms.DataGridViewTextBoxColumn CertificadoAyuda;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ExpedienteRea;
        protected System.Windows.Forms.DataGridViewTextBoxColumn AyudaKilo;
        protected System.Windows.Forms.DataGridViewTextBoxColumn CuentaRea;
        protected System.Windows.Forms.DataGridViewTextBoxColumn CuentaBancaria;
        protected System.Windows.Forms.TabPage Observaciones_TP;
        protected System.Windows.Forms.RichTextBox observacionesRichTextBox;
        protected moleQule.Face.Controls.NumericTextBox SalePrice_NTB;
        protected moleQule.Face.Controls.NumericTextBox PurchasePrice_NTB;
        private moleQule.Face.Controls.NumericTextBox Grant_NTB;
        private moleQule.Face.Controls.NumericTextBox numericTextBox1;
        private System.Windows.Forms.TextBox FamiliaCode_TB;
        private System.Windows.Forms.TextBox nombreSerieTextBox;
        protected System.Windows.Forms.Button Familia_BT;
        protected System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox Venta_GB;
        private System.Windows.Forms.GroupBox Familia_GB;
        protected System.Windows.Forms.Button ImpuestoVenta_BT;
        protected System.Windows.Forms.TextBox ImpuestoVenta_TB;
        protected System.Windows.Forms.Button DefectoCompra_BT;
        protected System.Windows.Forms.Button ImpuestoCompra_BT;
        protected System.Windows.Forms.TextBox ImpuestoCompra_TB;
        protected System.Windows.Forms.Button DefectoVenta_BT;
        private System.Windows.Forms.GroupBox Compra_GB;
		private System.Windows.Forms.GroupBox Otros_GB;
		private System.Windows.Forms.CheckBox Unitario_CkB;
		protected System.Windows.Forms.Button CuentaContableCompra_BT;
		protected System.Windows.Forms.Button CuentaContableVenta_BT;
		protected System.Windows.Forms.MaskedTextBox CuentaContableCompra_TB;
		protected System.Windows.Forms.MaskedTextBox CuentaContableVenta_TB;
		protected System.Windows.Forms.Button FormaVenta_BT;
		protected System.Windows.Forms.TextBox FormaVenta_TB;
		private Controls.NumericTextBox numericTextBox2;
		protected System.Windows.Forms.Button SetEstado_BT;
		protected System.Windows.Forms.Label label8;
		protected System.Windows.Forms.TextBox Estado_TB;
		private Controls.NumericTextBox KilosBulto_NTB;
		private System.Windows.Forms.TabPage Stock_TP;
		private System.Windows.Forms.TabPage Partidas_TP;
		protected System.Windows.Forms.SplitContainer Stock_Panel;
		protected System.Windows.Forms.ToolStrip Stock_TS;
		protected System.Windows.Forms.ToolStripButton AddStock_TI;
		protected System.Windows.Forms.ToolStripButton EditStock_TI;
		protected System.Windows.Forms.ToolStripButton toolStripButton3;
		protected System.Windows.Forms.ToolStripButton DeleteStock_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		protected System.Windows.Forms.DataGridView Stock_DGW;
		protected System.Windows.Forms.DataGridView Partidas_DGW;
		protected System.Windows.Forms.BindingSource Batch_BS;
		protected System.Windows.Forms.BindingSource Stock_BS;
		private System.Windows.Forms.CheckBox Beneficio_CkB;
        private Controls.NumericTextBox PBeneficioMinimo_NTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
		protected System.Windows.Forms.TextBox Codigo_TB;
		private System.Windows.Forms.TabPage Components_TP;
		protected System.Windows.Forms.SplitContainer Components_Panel;
		protected System.Windows.Forms.ToolStrip Components_TS;
		protected System.Windows.Forms.ToolStripButton AddComponent_TI;
		protected System.Windows.Forms.ToolStripButton DeleteComponent_TI;
		private System.Windows.Forms.DataGridView Components_DGW;
		protected System.Windows.Forms.BindingSource Datos_Components;
		private System.Windows.Forms.CheckBox Kit_CkB;
		protected System.Windows.Forms.TextBox ExternalCode_TB;
		private System.Windows.Forms.DataGridViewTextBoxColumn CPProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPAmount;
        private System.Windows.Forms.CheckBox NoStockSale_CkB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoStockLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAlbaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIDBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn StFecha;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StFacturacionPeso;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Bulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bultos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn BultosActuales;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosActuales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StStoreID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StExpedient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn StUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAlbaranPartida;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFacturaPartida;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaStoreID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expediente;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn BultosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn KiloPorBulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompraKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompraBulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn GastoKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CosteKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CosteKgAyuda;
        private System.Windows.Forms.DataGridViewTextBoxColumn CosteNeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVentaBulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeneficioKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeneficioEstimado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAUbicacion;



    }
}
