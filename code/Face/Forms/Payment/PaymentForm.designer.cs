namespace moleQule.Face.Store
{
    partial class PaymentForm
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
            System.Windows.Forms.Label totalFacturadoLabel;
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label cobradoLabel;
            System.Windows.Forms.Label pendienteLabel;
            System.Windows.Forms.Label vencidoNoCobradoLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentForm));
            this.Pagos_GB = new System.Windows.Forms.GroupBox();
            this.Pagos_SC = new System.Windows.Forms.SplitContainer();
            this.Cobros_TS = new System.Windows.Forms.ToolStrip();
            this.AddPago_TI = new System.Windows.Forms.ToolStripButton();
            this.EditPago_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewPago_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.PGChangeState_TI = new System.Windows.Forms.ToolStripSplitButton();
            this.UnlockItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.LockItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.NullItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPago_TI = new System.Windows.Forms.ToolStripButton();
            this.Pagos_DGW = new System.Windows.Forms.DataGridView();
            this.Datos_Pago = new System.Windows.Forms.BindingSource(this.components);
            this.FAsociadas_GB = new System.Windows.Forms.GroupBox();
            this.Facturas_DGW = new System.Windows.Forms.DataGridView();
            this.CodigoExpediente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NSerie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaPrevisionPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPagoFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaDiasTranscurridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaAnteriores = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaAsignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datos_Factura = new System.Windows.Forms.BindingSource(this.components);
            this.Acreedor_GB = new System.Windows.Forms.GroupBox();
            this.Proveedor_BT = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Datos_Resumen = new System.Windows.Forms.BindingSource(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.vencidoNoCobradoTextBox = new System.Windows.Forms.TextBox();
            this.Pendiente_TB = new System.Windows.Forms.TextBox();
            this.Pagado_TB = new System.Windows.Forms.TextBox();
            this.codigoTextBox = new System.Windows.Forms.TextBox();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.Facturado_TB = new System.Windows.Forms.TextBox();
            this.Pendientes_GB = new System.Windows.Forms.GroupBox();
            this.Pendientes_SC = new System.Windows.Forms.SplitContainer();
            this.Pendientes_DGW = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteNFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PtePrevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteDiasTranscurridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PtePendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datos_Pendientes = new System.Windows.Forms.BindingSource(this.components);
            this.Pendientes_TS = new System.Windows.Forms.ToolStrip();
            this.EditPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.VerPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.PrintListPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.PrintPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.Content_SC = new System.Windows.Forms.SplitContainer();
            this.Listas_TLP = new System.Windows.Forms.TableLayoutPanel();
            this.NPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MedioPagoLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PagoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PagoVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoPagoLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PagoImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GastosBancarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PendienteAsignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PagoCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            totalFacturadoLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            cobradoLabel = new System.Windows.Forms.Label();
            pendienteLabel = new System.Windows.Forms.Label();
            vencidoNoCobradoLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
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
            this.Pagos_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_SC)).BeginInit();
            this.Pagos_SC.Panel1.SuspendLayout();
            this.Pagos_SC.Panel2.SuspendLayout();
            this.Pagos_SC.SuspendLayout();
            this.Cobros_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Pago)).BeginInit();
            this.FAsociadas_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Factura)).BeginInit();
            this.Acreedor_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Resumen)).BeginInit();
            this.Pendientes_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_SC)).BeginInit();
            this.Pendientes_SC.Panel1.SuspendLayout();
            this.Pendientes_SC.Panel2.SuspendLayout();
            this.Pendientes_SC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Pendientes)).BeginInit();
            this.Pendientes_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_SC)).BeginInit();
            this.Content_SC.Panel1.SuspendLayout();
            this.Content_SC.Panel2.SuspendLayout();
            this.Content_SC.SuspendLayout();
            this.Listas_TLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(1094, 778);
            this.PanelesV.SplitterDistance = 723;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(455, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Text = "Cerrar";
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(998, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.Visible = false;
            // 
            // Pie_Panel
            // 
            // 
            // Pie_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel1, true);
            // 
            // Pie_Panel.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Pie_Panel, true);
            this.Pie_Panel.Size = new System.Drawing.Size(1092, 52);
            // 
            // Content_Panel
            // 
            // 
            // Content_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel1, true);
            // 
            // Content_Panel.Panel2
            // 
            this.Content_Panel.Panel2.Controls.Add(this.Content_SC);
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Content_Panel, true);
            this.Content_Panel.Size = new System.Drawing.Size(1092, 721);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.Cliente);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(368, 117);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1094, 778);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(510, 437);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(510, 352);
            // 
            // totalFacturadoLabel
            // 
            totalFacturadoLabel.AutoSize = true;
            totalFacturadoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalFacturadoLabel.Location = new System.Drawing.Point(24, 144);
            totalFacturadoLabel.Name = "totalFacturadoLabel";
            totalFacturadoLabel.Size = new System.Drawing.Size(87, 13);
            totalFacturadoLabel.TabIndex = 0;
            totalFacturadoLabel.Text = "Total Facturado:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(6, 49);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            codigoLabel.Location = new System.Drawing.Point(13, 96);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(44, 13);
            codigoLabel.TabIndex = 6;
            codigoLabel.Text = "Código:";
            // 
            // cobradoLabel
            // 
            cobradoLabel.AutoSize = true;
            cobradoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cobradoLabel.Location = new System.Drawing.Point(64, 198);
            cobradoLabel.Name = "cobradoLabel";
            cobradoLabel.Size = new System.Drawing.Size(47, 13);
            cobradoLabel.TabIndex = 8;
            cobradoLabel.Text = "Pagado:";
            // 
            // pendienteLabel
            // 
            pendienteLabel.AutoSize = true;
            pendienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pendienteLabel.Location = new System.Drawing.Point(52, 225);
            pendienteLabel.Name = "pendienteLabel";
            pendienteLabel.Size = new System.Drawing.Size(59, 13);
            pendienteLabel.TabIndex = 18;
            pendienteLabel.Text = "Pendiente:";
            // 
            // vencidoNoCobradoLabel
            // 
            vencidoNoCobradoLabel.AutoSize = true;
            vencidoNoCobradoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            vencidoNoCobradoLabel.Location = new System.Drawing.Point(13, 250);
            vencidoNoCobradoLabel.Name = "vencidoNoCobradoLabel";
            vencidoNoCobradoLabel.Size = new System.Drawing.Size(98, 13);
            vencidoNoCobradoLabel.TabIndex = 22;
            vencidoNoCobradoLabel.Text = "Efectos Devueltos:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(30, 171);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(81, 13);
            label1.TabIndex = 24;
            label1.Text = "Total Estimado:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(13, 277);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(98, 13);
            label2.TabIndex = 26;
            label2.Text = "Efectos Ptes. Vto.:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(11, 310);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(82, 13);
            label3.TabIndex = 28;
            label3.Text = "Observaciones:";
            // 
            // Pagos_GB
            // 
            this.Pagos_GB.Controls.Add(this.Pagos_SC);
            this.Pagos_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pagos_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pagos_GB.Location = new System.Drawing.Point(3, 3);
            this.Pagos_GB.Name = "Pagos_GB";
            this.Pagos_GB.Size = new System.Drawing.Size(574, 344);
            this.Pagos_GB.TabIndex = 0;
            this.Pagos_GB.TabStop = false;
            this.Pagos_GB.Text = "Pagos";
            // 
            // Pagos_SC
            // 
            this.Pagos_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pagos_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Pagos_SC.Location = new System.Drawing.Point(3, 17);
            this.Pagos_SC.Name = "Pagos_SC";
            this.Pagos_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Pagos_SC.Panel1
            // 
            this.Pagos_SC.Panel1.Controls.Add(this.Cobros_TS);
            this.Pagos_SC.Panel1MinSize = 39;
            // 
            // Pagos_SC.Panel2
            // 
            this.Pagos_SC.Panel2.Controls.Add(this.Pagos_DGW);
            this.Pagos_SC.Size = new System.Drawing.Size(568, 324);
            this.Pagos_SC.SplitterDistance = 39;
            this.Pagos_SC.SplitterWidth = 1;
            this.Pagos_SC.TabIndex = 6;
            // 
            // Cobros_TS
            // 
            this.Cobros_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Cobros_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPago_TI,
            this.EditPago_TI,
            this.ViewPago_TI,
            this.toolStripLabel2,
            this.PGChangeState_TI,
            this.PrintPago_TI});
            this.Cobros_TS.Location = new System.Drawing.Point(0, 0);
            this.Cobros_TS.Name = "Cobros_TS";
            this.HelpProvider.SetShowHelp(this.Cobros_TS, true);
            this.Cobros_TS.Size = new System.Drawing.Size(568, 39);
            this.Cobros_TS.TabIndex = 5;
            // 
            // AddPago_TI
            // 
            this.AddPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddPago_TI.Name = "AddPago_TI";
            this.AddPago_TI.Size = new System.Drawing.Size(36, 36);
            this.AddPago_TI.Text = "Nuevo Pago";
            this.AddPago_TI.Click += new System.EventHandler(this.AddPago_TI_Click);
            // 
            // EditPago_TI
            // 
            this.EditPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditPago_TI.Name = "EditPago_TI";
            this.EditPago_TI.Size = new System.Drawing.Size(36, 36);
            this.EditPago_TI.Text = "Editar Pago";
            this.EditPago_TI.Click += new System.EventHandler(this.EditPago_TI_Click);
            // 
            // ViewPago_TI
            // 
            this.ViewPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            this.ViewPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewPago_TI.Name = "ViewPago_TI";
            this.ViewPago_TI.Size = new System.Drawing.Size(36, 36);
            this.ViewPago_TI.Text = "Ver Pago";
            this.ViewPago_TI.Click += new System.EventHandler(this.ViewPago_TI_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 36);
            // 
            // PGChangeState_TI
            // 
            this.PGChangeState_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PGChangeState_TI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UnlockItem_TMI,
            this.LockItem_TMI,
            this.NullItem_TMI});
            this.PGChangeState_TI.Image = global::moleQule.Face.Store.Properties.Resources.change_state;
            this.PGChangeState_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PGChangeState_TI.Name = "PGChangeState_TI";
            this.PGChangeState_TI.Size = new System.Drawing.Size(48, 36);
            this.PGChangeState_TI.Text = "Cambiar Estado";
            // 
            // UnlockItem_TMI
            // 
            this.UnlockItem_TMI.Image = global::moleQule.Face.Store.Properties.Resources.state_open;
            this.UnlockItem_TMI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnlockItem_TMI.Name = "UnlockItem_TMI";
            this.UnlockItem_TMI.Size = new System.Drawing.Size(163, 38);
            this.UnlockItem_TMI.Text = "Abierto";
            this.UnlockItem_TMI.Click += new System.EventHandler(this.UnlockItem_TMI_Click);
            // 
            // LockItem_TMI
            // 
            this.LockItem_TMI.Image = global::moleQule.Face.Store.Properties.Resources.state_lock;
            this.LockItem_TMI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LockItem_TMI.Name = "LockItem_TMI";
            this.LockItem_TMI.Size = new System.Drawing.Size(163, 38);
            this.LockItem_TMI.Text = "Contabilizado";
            this.LockItem_TMI.Click += new System.EventHandler(this.LockItem_TMI_Click);
            // 
            // NullItem_TMI
            // 
            this.NullItem_TMI.Image = global::moleQule.Face.Store.Properties.Resources.state_null;
            this.NullItem_TMI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NullItem_TMI.Name = "NullItem_TMI";
            this.NullItem_TMI.Size = new System.Drawing.Size(163, 38);
            this.NullItem_TMI.Text = "Anulado";
            this.NullItem_TMI.Click += new System.EventHandler(this.NullItem_TMI_Click);
            // 
            // PrintPago_TI
            // 
            this.PrintPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_print;
            this.PrintPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPago_TI.Name = "PrintPago_TI";
            this.PrintPago_TI.Size = new System.Drawing.Size(36, 36);
            this.PrintPago_TI.Text = "Imprimir Pago";
            this.PrintPago_TI.Click += new System.EventHandler(this.PrintPago_TI_Click);
            // 
            // Pagos_DGW
            // 
            this.Pagos_DGW.AllowUserToAddRows = false;
            this.Pagos_DGW.AllowUserToDeleteRows = false;
            this.Pagos_DGW.AutoGenerateColumns = false;
            this.Pagos_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NPago,
            this.Codigo,
            this.MedioPagoLabel,
            this.PagoFecha,
            this.PagoVencimiento,
            this.EstadoPagoLabel,
            this.PagoImporte,
            this.GastosBancarios,
            this.PendienteAsignacion,
            this.PagoCuenta,
            this.Usuario,
            this.Observaciones});
            this.Pagos_DGW.DataSource = this.Datos_Pago;
            this.Pagos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pagos_DGW.Location = new System.Drawing.Point(0, 0);
            this.Pagos_DGW.Name = "Pagos_DGW";
            this.Pagos_DGW.ReadOnly = true;
            this.Pagos_DGW.RowHeadersWidth = 15;
            this.Pagos_DGW.Size = new System.Drawing.Size(568, 284);
            this.Pagos_DGW.TabIndex = 0;
            this.Pagos_DGW.DoubleClick += new System.EventHandler(this.Pagos_DGW_DoubleClick);
            // 
            // Datos_Pago
            // 
            this.Datos_Pago.AllowNew = false;
            this.Datos_Pago.DataSource = typeof(moleQule.Library.Store.PaymentInfo);
            // 
            // FAsociadas_GB
            // 
            this.FAsociadas_GB.Controls.Add(this.Facturas_DGW);
            this.FAsociadas_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FAsociadas_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FAsociadas_GB.Location = new System.Drawing.Point(3, 353);
            this.FAsociadas_GB.Name = "FAsociadas_GB";
            this.FAsociadas_GB.Size = new System.Drawing.Size(574, 144);
            this.FAsociadas_GB.TabIndex = 1;
            this.FAsociadas_GB.TabStop = false;
            this.FAsociadas_GB.Text = "Facturas Asociadas al Pago";
            // 
            // Facturas_DGW
            // 
            this.Facturas_DGW.AllowUserToAddRows = false;
            this.Facturas_DGW.AllowUserToDeleteRows = false;
            this.Facturas_DGW.AutoGenerateColumns = false;
            this.Facturas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoExpediente,
            this.NSerie,
            this.FacturaCodigo,
            this.FacturaFecha,
            this.FacturaTotal,
            this.FacturaPrevisionPago,
            this.FechaPagoFactura,
            this.FacturaDiasTranscurridos,
            this.FacturaAnteriores,
            this.FacturaAsignado,
            this.FacturaPendiente});
            this.Facturas_DGW.DataSource = this.Datos_Factura;
            this.Facturas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Facturas_DGW.Enabled = false;
            this.Facturas_DGW.Location = new System.Drawing.Point(3, 17);
            this.Facturas_DGW.MultiSelect = false;
            this.Facturas_DGW.Name = "Facturas_DGW";
            this.Facturas_DGW.ReadOnly = true;
            this.Facturas_DGW.RowHeadersVisible = false;
            this.Facturas_DGW.RowHeadersWidth = 15;
            this.Facturas_DGW.Size = new System.Drawing.Size(568, 124);
            this.Facturas_DGW.TabIndex = 0;
            this.Facturas_DGW.DoubleClick += new System.EventHandler(this.Facturas_DGW_DoubleClick);
            // 
            // CodigoExpediente
            // 
            this.CodigoExpediente.DataPropertyName = "CodigoExpediente";
            this.CodigoExpediente.HeaderText = "Expediente";
            this.CodigoExpediente.Name = "CodigoExpediente";
            this.CodigoExpediente.ReadOnly = true;
            this.CodigoExpediente.Width = 90;
            // 
            // NSerie
            // 
            this.NSerie.DataPropertyName = "NSerie";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NSerie.DefaultCellStyle = dataGridViewCellStyle11;
            this.NSerie.HeaderText = "Nº Serie";
            this.NSerie.Name = "NSerie";
            this.NSerie.ReadOnly = true;
            this.NSerie.Width = 45;
            // 
            // FacturaCodigo
            // 
            this.FacturaCodigo.DataPropertyName = "NFactura";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "00000";
            this.FacturaCodigo.DefaultCellStyle = dataGridViewCellStyle12;
            this.FacturaCodigo.HeaderText = "Nº Factura";
            this.FacturaCodigo.Name = "FacturaCodigo";
            this.FacturaCodigo.ReadOnly = true;
            this.FacturaCodigo.Width = 95;
            // 
            // FacturaFecha
            // 
            this.FacturaFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "d";
            dataGridViewCellStyle13.NullValue = null;
            this.FacturaFecha.DefaultCellStyle = dataGridViewCellStyle13;
            this.FacturaFecha.HeaderText = "Fecha";
            this.FacturaFecha.Name = "FacturaFecha";
            this.FacturaFecha.ReadOnly = true;
            this.FacturaFecha.Width = 75;
            // 
            // FacturaTotal
            // 
            this.FacturaTotal.DataPropertyName = "Total";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "C2";
            dataGridViewCellStyle14.NullValue = null;
            this.FacturaTotal.DefaultCellStyle = dataGridViewCellStyle14;
            this.FacturaTotal.HeaderText = "Total";
            this.FacturaTotal.Name = "FacturaTotal";
            this.FacturaTotal.ReadOnly = true;
            this.FacturaTotal.Width = 80;
            // 
            // FacturaPrevisionPago
            // 
            this.FacturaPrevisionPago.DataPropertyName = "Prevision";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Format = "d";
            this.FacturaPrevisionPago.DefaultCellStyle = dataGridViewCellStyle15;
            this.FacturaPrevisionPago.HeaderText = "Fecha Previsión";
            this.FacturaPrevisionPago.Name = "FacturaPrevisionPago";
            this.FacturaPrevisionPago.ReadOnly = true;
            this.FacturaPrevisionPago.Width = 75;
            // 
            // FechaPagoFactura
            // 
            this.FechaPagoFactura.DataPropertyName = "FechaPagoFactura";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.Format = "d";
            this.FechaPagoFactura.DefaultCellStyle = dataGridViewCellStyle16;
            this.FechaPagoFactura.HeaderText = "Fecha Asignación";
            this.FechaPagoFactura.Name = "FechaPagoFactura";
            this.FechaPagoFactura.ReadOnly = true;
            this.FechaPagoFactura.Width = 75;
            // 
            // FacturaDiasTranscurridos
            // 
            this.FacturaDiasTranscurridos.DataPropertyName = "DiasTranscurridos";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.Format = "N0";
            dataGridViewCellStyle17.NullValue = null;
            this.FacturaDiasTranscurridos.DefaultCellStyle = dataGridViewCellStyle17;
            this.FacturaDiasTranscurridos.HeaderText = "Días";
            this.FacturaDiasTranscurridos.Name = "FacturaDiasTranscurridos";
            this.FacturaDiasTranscurridos.ReadOnly = true;
            this.FacturaDiasTranscurridos.Width = 40;
            // 
            // FacturaAnteriores
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "C2";
            dataGridViewCellStyle18.NullValue = null;
            this.FacturaAnteriores.DefaultCellStyle = dataGridViewCellStyle18;
            this.FacturaAnteriores.HeaderText = "Pagos Anteriores";
            this.FacturaAnteriores.Name = "FacturaAnteriores";
            this.FacturaAnteriores.ReadOnly = true;
            this.FacturaAnteriores.Width = 80;
            // 
            // FacturaAsignado
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "C2";
            dataGridViewCellStyle19.NullValue = null;
            this.FacturaAsignado.DefaultCellStyle = dataGridViewCellStyle19;
            this.FacturaAsignado.HeaderText = "Asignado";
            this.FacturaAsignado.Name = "FacturaAsignado";
            this.FacturaAsignado.ReadOnly = true;
            this.FacturaAsignado.Width = 80;
            // 
            // FacturaPendiente
            // 
            this.FacturaPendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "C2";
            dataGridViewCellStyle20.NullValue = null;
            this.FacturaPendiente.DefaultCellStyle = dataGridViewCellStyle20;
            this.FacturaPendiente.HeaderText = "Pendiente";
            this.FacturaPendiente.Name = "FacturaPendiente";
            this.FacturaPendiente.ReadOnly = true;
            this.FacturaPendiente.Width = 80;
            // 
            // Datos_Factura
            // 
            this.Datos_Factura.DataSource = typeof(moleQule.Library.Store.InputInvoiceInfo);
            // 
            // Acreedor_GB
            // 
            this.Acreedor_GB.Controls.Add(this.Proveedor_BT);
            this.Acreedor_GB.Controls.Add(label3);
            this.Acreedor_GB.Controls.Add(this.textBox3);
            this.Acreedor_GB.Controls.Add(label2);
            this.Acreedor_GB.Controls.Add(this.textBox2);
            this.Acreedor_GB.Controls.Add(label1);
            this.Acreedor_GB.Controls.Add(this.textBox1);
            this.Acreedor_GB.Controls.Add(vencidoNoCobradoLabel);
            this.Acreedor_GB.Controls.Add(this.vencidoNoCobradoTextBox);
            this.Acreedor_GB.Controls.Add(pendienteLabel);
            this.Acreedor_GB.Controls.Add(this.Pendiente_TB);
            this.Acreedor_GB.Controls.Add(cobradoLabel);
            this.Acreedor_GB.Controls.Add(this.Pagado_TB);
            this.Acreedor_GB.Controls.Add(codigoLabel);
            this.Acreedor_GB.Controls.Add(this.codigoTextBox);
            this.Acreedor_GB.Controls.Add(nombreLabel);
            this.Acreedor_GB.Controls.Add(this.nombreTextBox);
            this.Acreedor_GB.Controls.Add(totalFacturadoLabel);
            this.Acreedor_GB.Controls.Add(this.Facturado_TB);
            this.Acreedor_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Acreedor_GB.Location = new System.Drawing.Point(1, 4);
            this.Acreedor_GB.Name = "Acreedor_GB";
            this.Acreedor_GB.Size = new System.Drawing.Size(236, 647);
            this.Acreedor_GB.TabIndex = 2;
            this.Acreedor_GB.TabStop = false;
            this.Acreedor_GB.Text = "Datos del Acreedor";
            // 
            // Proveedor_BT
            // 
            this.Proveedor_BT.Image = global::moleQule.Face.Store.Properties.Resources.proveedor;
            this.Proveedor_BT.Location = new System.Drawing.Point(162, 17);
            this.Proveedor_BT.Name = "Proveedor_BT";
            this.Proveedor_BT.Size = new System.Drawing.Size(60, 38);
            this.Proveedor_BT.TabIndex = 30;
            this.Proveedor_BT.UseVisualStyleBackColor = true;
            this.Proveedor_BT.Click += new System.EventHandler(this.Proveedor_BT_Click);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "Observaciones", true));
            this.textBox3.Location = new System.Drawing.Point(14, 333);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(208, 294);
            this.textBox3.TabIndex = 29;
            this.textBox3.TabStop = false;
            // 
            // Datos_Resumen
            // 
            this.Datos_Resumen.DataSource = typeof(moleQule.Library.Store.PaymentSummary);
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "EfectosPendientesVto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox2.Location = new System.Drawing.Point(118, 275);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(104, 21);
            this.textBox2.TabIndex = 27;
            this.textBox2.TabStop = false;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "TotalEstimado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox1.Location = new System.Drawing.Point(118, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(105, 21);
            this.textBox1.TabIndex = 25;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // vencidoNoCobradoTextBox
            // 
            this.vencidoNoCobradoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "EfectosDevueltos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.vencidoNoCobradoTextBox.Location = new System.Drawing.Point(118, 248);
            this.vencidoNoCobradoTextBox.Name = "vencidoNoCobradoTextBox";
            this.vencidoNoCobradoTextBox.ReadOnly = true;
            this.vencidoNoCobradoTextBox.Size = new System.Drawing.Size(104, 21);
            this.vencidoNoCobradoTextBox.TabIndex = 23;
            this.vencidoNoCobradoTextBox.TabStop = false;
            this.vencidoNoCobradoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Pendiente_TB
            // 
            this.Pendiente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "Pendiente", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Pendiente_TB.Location = new System.Drawing.Point(118, 222);
            this.Pendiente_TB.Name = "Pendiente_TB";
            this.Pendiente_TB.ReadOnly = true;
            this.Pendiente_TB.Size = new System.Drawing.Size(105, 21);
            this.Pendiente_TB.TabIndex = 19;
            this.Pendiente_TB.TabStop = false;
            this.Pendiente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Pagado_TB
            // 
            this.Pagado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "Pagado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Pagado_TB.Location = new System.Drawing.Point(118, 195);
            this.Pagado_TB.Name = "Pagado_TB";
            this.Pagado_TB.ReadOnly = true;
            this.Pagado_TB.Size = new System.Drawing.Size(105, 21);
            this.Pagado_TB.TabIndex = 9;
            this.Pagado_TB.TabStop = false;
            this.Pagado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "Codigo", true));
            this.codigoTextBox.Location = new System.Drawing.Point(63, 92);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.ReadOnly = true;
            this.codigoTextBox.Size = new System.Drawing.Size(89, 21);
            this.codigoTextBox.TabIndex = 7;
            this.codigoTextBox.TabStop = false;
            this.codigoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "Nombre", true));
            this.nombreTextBox.Location = new System.Drawing.Point(9, 66);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.ReadOnly = true;
            this.nombreTextBox.Size = new System.Drawing.Size(218, 21);
            this.nombreTextBox.TabIndex = 3;
            this.nombreTextBox.TabStop = false;
            // 
            // Facturado_TB
            // 
            this.Facturado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Resumen, "TotalFacturado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Facturado_TB.Location = new System.Drawing.Point(117, 141);
            this.Facturado_TB.Name = "Facturado_TB";
            this.Facturado_TB.ReadOnly = true;
            this.Facturado_TB.Size = new System.Drawing.Size(105, 21);
            this.Facturado_TB.TabIndex = 1;
            this.Facturado_TB.TabStop = false;
            this.Facturado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Pendientes_GB
            // 
            this.Pendientes_GB.Controls.Add(this.Pendientes_SC);
            this.Pendientes_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pendientes_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pendientes_GB.Location = new System.Drawing.Point(3, 503);
            this.Pendientes_GB.Name = "Pendientes_GB";
            this.Pendientes_GB.Size = new System.Drawing.Size(574, 174);
            this.Pendientes_GB.TabIndex = 5;
            this.Pendientes_GB.TabStop = false;
            this.Pendientes_GB.Text = "Facturas Pendientes";
            // 
            // Pendientes_SC
            // 
            this.Pendientes_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pendientes_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Pendientes_SC.IsSplitterFixed = true;
            this.Pendientes_SC.Location = new System.Drawing.Point(3, 17);
            this.Pendientes_SC.Name = "Pendientes_SC";
            // 
            // Pendientes_SC.Panel1
            // 
            this.Pendientes_SC.Panel1.Controls.Add(this.Pendientes_DGW);
            this.Pendientes_SC.Panel1MinSize = 50;
            // 
            // Pendientes_SC.Panel2
            // 
            this.Pendientes_SC.Panel2.Controls.Add(this.Pendientes_TS);
            this.Pendientes_SC.Panel2MinSize = 34;
            this.Pendientes_SC.Size = new System.Drawing.Size(568, 154);
            this.Pendientes_SC.SplitterDistance = 468;
            this.Pendientes_SC.TabIndex = 0;
            // 
            // Pendientes_DGW
            // 
            this.Pendientes_DGW.AllowUserToAddRows = false;
            this.Pendientes_DGW.AllowUserToDeleteRows = false;
            this.Pendientes_DGW.AutoGenerateColumns = false;
            this.Pendientes_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.PteNFactura,
            this.PteFecha,
            this.PteTotal,
            this.PtePrevision,
            this.PteDiasTranscurridos,
            this.PtePendiente});
            this.Pendientes_DGW.DataSource = this.Datos_Pendientes;
            this.Pendientes_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pendientes_DGW.Enabled = false;
            this.Pendientes_DGW.Location = new System.Drawing.Point(0, 0);
            this.Pendientes_DGW.Name = "Pendientes_DGW";
            this.Pendientes_DGW.ReadOnly = true;
            this.Pendientes_DGW.RowHeadersVisible = false;
            this.Pendientes_DGW.RowHeadersWidth = 15;
            this.Pendientes_DGW.Size = new System.Drawing.Size(468, 154);
            this.Pendientes_DGW.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CodigoExpediente";
            this.dataGridViewTextBoxColumn1.HeaderText = "Expediente";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NSerie";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nº Serie";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 45;
            // 
            // PteNFactura
            // 
            this.PteNFactura.DataPropertyName = "NFactura";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PteNFactura.DefaultCellStyle = dataGridViewCellStyle22;
            this.PteNFactura.HeaderText = "Nº Factura";
            this.PteNFactura.Name = "PteNFactura";
            this.PteNFactura.ReadOnly = true;
            this.PteNFactura.Width = 95;
            // 
            // PteFecha
            // 
            this.PteFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Format = "d";
            dataGridViewCellStyle23.NullValue = null;
            this.PteFecha.DefaultCellStyle = dataGridViewCellStyle23;
            this.PteFecha.HeaderText = "Fecha";
            this.PteFecha.Name = "PteFecha";
            this.PteFecha.ReadOnly = true;
            this.PteFecha.Width = 75;
            // 
            // PteTotal
            // 
            this.PteTotal.DataPropertyName = "Total";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Format = "C2";
            dataGridViewCellStyle24.NullValue = null;
            this.PteTotal.DefaultCellStyle = dataGridViewCellStyle24;
            this.PteTotal.HeaderText = "Total";
            this.PteTotal.Name = "PteTotal";
            this.PteTotal.ReadOnly = true;
            this.PteTotal.Width = 80;
            // 
            // PtePrevision
            // 
            this.PtePrevision.DataPropertyName = "Prevision";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.Format = "d";
            this.PtePrevision.DefaultCellStyle = dataGridViewCellStyle25;
            this.PtePrevision.HeaderText = "Fecha Previsión";
            this.PtePrevision.Name = "PtePrevision";
            this.PtePrevision.ReadOnly = true;
            this.PtePrevision.Width = 75;
            // 
            // PteDiasTranscurridos
            // 
            this.PteDiasTranscurridos.DataPropertyName = "DiasTranscurridos";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.Format = "N0";
            this.PteDiasTranscurridos.DefaultCellStyle = dataGridViewCellStyle26;
            this.PteDiasTranscurridos.HeaderText = "Días";
            this.PteDiasTranscurridos.Name = "PteDiasTranscurridos";
            this.PteDiasTranscurridos.ReadOnly = true;
            this.PteDiasTranscurridos.Width = 40;
            // 
            // PtePendiente
            // 
            this.PtePendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle27.Format = "C2";
            dataGridViewCellStyle27.NullValue = null;
            this.PtePendiente.DefaultCellStyle = dataGridViewCellStyle27;
            this.PtePendiente.HeaderText = "Pendiente";
            this.PtePendiente.Name = "PtePendiente";
            this.PtePendiente.ReadOnly = true;
            this.PtePendiente.Width = 90;
            // 
            // Datos_Pendientes
            // 
            this.Datos_Pendientes.DataSource = typeof(moleQule.Library.Store.InputInvoiceInfo);
            // 
            // Pendientes_TS
            // 
            this.Pendientes_TS.Dock = System.Windows.Forms.DockStyle.Left;
            this.Pendientes_TS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Pendientes_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Pendientes_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditPendiente_TI,
            this.VerPendiente_TI,
            this.PrintListPendiente_TI,
            this.PrintPendiente_TI});
            this.Pendientes_TS.Location = new System.Drawing.Point(0, 0);
            this.Pendientes_TS.Name = "Pendientes_TS";
            this.HelpProvider.SetShowHelp(this.Pendientes_TS, true);
            this.Pendientes_TS.Size = new System.Drawing.Size(37, 154);
            this.Pendientes_TS.TabIndex = 6;
            this.Pendientes_TS.Tag = "NO FORMAT";
            // 
            // EditPendiente_TI
            // 
            this.EditPendiente_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditPendiente_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditPendiente_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditPendiente_TI.Name = "EditPendiente_TI";
            this.EditPendiente_TI.Size = new System.Drawing.Size(34, 36);
            this.EditPendiente_TI.Text = "Editar Factura";
            this.EditPendiente_TI.Click += new System.EventHandler(this.EditPendiente_TI_Click);
            // 
            // VerPendiente_TI
            // 
            this.VerPendiente_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VerPendiente_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            this.VerPendiente_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VerPendiente_TI.Name = "VerPendiente_TI";
            this.VerPendiente_TI.Size = new System.Drawing.Size(34, 36);
            this.VerPendiente_TI.Text = "Ver Factura";
            this.VerPendiente_TI.Click += new System.EventHandler(this.VerPendiente_TI_Click);
            // 
            // PrintListPendiente_TI
            // 
            this.PrintListPendiente_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintListPendiente_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_print;
            this.PrintListPendiente_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintListPendiente_TI.Name = "PrintListPendiente_TI";
            this.PrintListPendiente_TI.Size = new System.Drawing.Size(34, 36);
            this.PrintListPendiente_TI.Text = "Imprimir Factura";
            this.PrintListPendiente_TI.Click += new System.EventHandler(this.PrintListPendiente_TI_Click);
            // 
            // PrintPendiente_TI
            // 
            this.PrintPendiente_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintPendiente_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_print_detail;
            this.PrintPendiente_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPendiente_TI.Name = "PrintPendiente_TI";
            this.PrintPendiente_TI.Size = new System.Drawing.Size(36, 36);
            this.PrintPendiente_TI.Text = "Imprimir Lista";
            this.PrintPendiente_TI.Visible = false;
            this.PrintPendiente_TI.Click += new System.EventHandler(this.PrintPendiente_TI_Click);
            // 
            // Content_SC
            // 
            this.Content_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Content_SC.Location = new System.Drawing.Point(0, 0);
            this.Content_SC.Name = "Content_SC";
            // 
            // Content_SC.Panel1
            // 
            this.Content_SC.Panel1.Controls.Add(this.Listas_TLP);
            // 
            // Content_SC.Panel2
            // 
            this.Content_SC.Panel2.Controls.Add(this.Acreedor_GB);
            this.Content_SC.Panel2MinSize = 238;
            this.Content_SC.Size = new System.Drawing.Size(1092, 680);
            this.Content_SC.SplitterDistance = 580;
            this.Content_SC.SplitterWidth = 1;
            this.Content_SC.TabIndex = 6;
            // 
            // Listas_TLP
            // 
            this.Listas_TLP.ColumnCount = 1;
            this.Listas_TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Listas_TLP.Controls.Add(this.Pagos_GB, 0, 0);
            this.Listas_TLP.Controls.Add(this.FAsociadas_GB, 0, 1);
            this.Listas_TLP.Controls.Add(this.Pendientes_GB, 0, 2);
            this.Listas_TLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Listas_TLP.Location = new System.Drawing.Point(0, 0);
            this.Listas_TLP.Name = "Listas_TLP";
            this.Listas_TLP.RowCount = 3;
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.Listas_TLP.Size = new System.Drawing.Size(580, 680);
            this.Listas_TLP.TabIndex = 6;
            // 
            // NPago
            // 
            this.NPago.DataPropertyName = "IdPago";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "0000";
            this.NPago.DefaultCellStyle = dataGridViewCellStyle1;
            this.NPago.HeaderText = "Nº";
            this.NPago.Name = "NPago";
            this.NPago.ReadOnly = true;
            this.NPago.Width = 35;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "00000";
            this.Codigo.DefaultCellStyle = dataGridViewCellStyle2;
            this.Codigo.HeaderText = "ID";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 50;
            // 
            // MedioPagoLabel
            // 
            this.MedioPagoLabel.DataPropertyName = "MedioPagoLabel";
            this.MedioPagoLabel.HeaderText = "Medio Pago";
            this.MedioPagoLabel.Name = "MedioPagoLabel";
            this.MedioPagoLabel.ReadOnly = true;
            this.MedioPagoLabel.Width = 120;
            // 
            // PagoFecha
            // 
            this.PagoFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle3.NullValue = null;
            this.PagoFecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.PagoFecha.HeaderText = "Fecha";
            this.PagoFecha.Name = "PagoFecha";
            this.PagoFecha.ReadOnly = true;
            this.PagoFecha.Width = 95;
            // 
            // PagoVencimiento
            // 
            this.PagoVencimiento.DataPropertyName = "Vencimiento";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            this.PagoVencimiento.DefaultCellStyle = dataGridViewCellStyle4;
            this.PagoVencimiento.HeaderText = "Vto.";
            this.PagoVencimiento.Name = "PagoVencimiento";
            this.PagoVencimiento.ReadOnly = true;
            this.PagoVencimiento.Width = 65;
            // 
            // EstadoPagoLabel
            // 
            this.EstadoPagoLabel.DataPropertyName = "EstadoPagoLabel";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EstadoPagoLabel.DefaultCellStyle = dataGridViewCellStyle5;
            this.EstadoPagoLabel.HeaderText = "Estado Pago";
            this.EstadoPagoLabel.Name = "EstadoPagoLabel";
            this.EstadoPagoLabel.ReadOnly = true;
            this.EstadoPagoLabel.Width = 70;
            // 
            // PagoImporte
            // 
            this.PagoImporte.DataPropertyName = "Importe";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.PagoImporte.DefaultCellStyle = dataGridViewCellStyle6;
            this.PagoImporte.HeaderText = "Importe";
            this.PagoImporte.Name = "PagoImporte";
            this.PagoImporte.ReadOnly = true;
            this.PagoImporte.Width = 75;
            // 
            // GastosBancarios
            // 
            this.GastosBancarios.DataPropertyName = "GastosBancarios";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            this.GastosBancarios.DefaultCellStyle = dataGridViewCellStyle7;
            this.GastosBancarios.HeaderText = "Gastos Bancarios";
            this.GastosBancarios.Name = "GastosBancarios";
            this.GastosBancarios.ReadOnly = true;
            this.GastosBancarios.Width = 65;
            // 
            // PendienteAsignacion
            // 
            this.PendienteAsignacion.DataPropertyName = "Pendiente";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.PendienteAsignacion.DefaultCellStyle = dataGridViewCellStyle8;
            this.PendienteAsignacion.HeaderText = "Pendiente";
            this.PendienteAsignacion.Name = "PendienteAsignacion";
            this.PendienteAsignacion.ReadOnly = true;
            this.PendienteAsignacion.Width = 65;
            // 
            // PagoCuenta
            // 
            this.PagoCuenta.DataPropertyName = "CuentaBancaria";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PagoCuenta.DefaultCellStyle = dataGridViewCellStyle9;
            this.PagoCuenta.HeaderText = "Cuenta";
            this.PagoCuenta.Name = "PagoCuenta";
            this.PagoCuenta.ReadOnly = true;
            this.PagoCuenta.Width = 140;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Usuario.DefaultCellStyle = dataGridViewCellStyle10;
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            this.Usuario.Width = 70;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            this.Observaciones.Width = 80;
            // 
            // PagoForm
            // 
            this.ClientSize = new System.Drawing.Size(1094, 778);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PagoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PagoForm";
            this.Shown += new System.EventHandler(this.PagoForm_Shown);
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
            this.Pagos_GB.ResumeLayout(false);
            this.Pagos_SC.Panel1.ResumeLayout(false);
            this.Pagos_SC.Panel1.PerformLayout();
            this.Pagos_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_SC)).EndInit();
            this.Pagos_SC.ResumeLayout(false);
            this.Cobros_TS.ResumeLayout(false);
            this.Cobros_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Pago)).EndInit();
            this.FAsociadas_GB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Factura)).EndInit();
            this.Acreedor_GB.ResumeLayout(false);
            this.Acreedor_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Resumen)).EndInit();
            this.Pendientes_GB.ResumeLayout(false);
            this.Pendientes_SC.Panel1.ResumeLayout(false);
            this.Pendientes_SC.Panel2.ResumeLayout(false);
            this.Pendientes_SC.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_SC)).EndInit();
            this.Pendientes_SC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Pendientes)).EndInit();
            this.Pendientes_TS.ResumeLayout(false);
            this.Pendientes_TS.PerformLayout();
            this.Content_SC.Panel1.ResumeLayout(false);
            this.Content_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_SC)).EndInit();
            this.Content_SC.ResumeLayout(false);
            this.Listas_TLP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox Pagos_GB;
        protected System.Windows.Forms.GroupBox FAsociadas_GB;
        protected System.Windows.Forms.GroupBox Acreedor_GB;
        protected System.Windows.Forms.TextBox Pagado_TB;
        protected System.Windows.Forms.TextBox codigoTextBox;
        protected System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.TextBox Facturado_TB;
        protected System.Windows.Forms.TextBox Pendiente_TB;
        protected System.Windows.Forms.DataGridView Facturas_DGW;
		protected System.Windows.Forms.BindingSource Datos_Factura;
        protected System.Windows.Forms.DataGridView Pagos_DGW;
        protected System.Windows.Forms.BindingSource Datos_Pago;
        protected System.Windows.Forms.BindingSource Datos_Resumen;
		protected System.Windows.Forms.GroupBox Pendientes_GB;
        protected System.Windows.Forms.BindingSource Datos_Pendientes;
        protected System.Windows.Forms.TextBox vencidoNoCobradoTextBox;
        protected System.Windows.Forms.TextBox textBox1;
        protected System.Windows.Forms.TextBox textBox2;
		protected System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.DataGridViewTextBoxColumn CodigoExpediente;
		private System.Windows.Forms.DataGridViewTextBoxColumn NSerie;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaCodigo;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaFecha;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaTotal;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaPrevisionPago;
		private System.Windows.Forms.DataGridViewTextBoxColumn FechaPagoFactura;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaDiasTranscurridos;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaAnteriores;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaAsignado;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacturaPendiente;
		protected System.Windows.Forms.DataGridView Pendientes_DGW;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn PteNFactura;
		private System.Windows.Forms.DataGridViewTextBoxColumn PteFecha;
		private System.Windows.Forms.DataGridViewTextBoxColumn PteTotal;
		private System.Windows.Forms.DataGridViewTextBoxColumn PtePrevision;
		private System.Windows.Forms.DataGridViewTextBoxColumn PteDiasTranscurridos;
		private System.Windows.Forms.DataGridViewTextBoxColumn PtePendiente;
		protected System.Windows.Forms.ToolStrip Pendientes_TS;
		protected System.Windows.Forms.ToolStripButton VerPendiente_TI;
		protected System.Windows.Forms.ToolStripButton PrintListPendiente_TI;
        protected System.Windows.Forms.ToolStripButton PrintPendiente_TI;
		protected System.Windows.Forms.SplitContainer Pagos_SC;
		protected System.Windows.Forms.ToolStrip Cobros_TS;
		protected System.Windows.Forms.ToolStripButton AddPago_TI;
		protected System.Windows.Forms.ToolStripButton EditPago_TI;
		protected System.Windows.Forms.ToolStripButton ViewPago_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripButton PrintPago_TI;
		private System.Windows.Forms.ToolStripSplitButton PGChangeState_TI;
		private System.Windows.Forms.ToolStripMenuItem UnlockItem_TMI;
		private System.Windows.Forms.ToolStripMenuItem LockItem_TMI;
		private System.Windows.Forms.ToolStripMenuItem NullItem_TMI;
        protected System.Windows.Forms.ToolStripButton EditPendiente_TI;
		protected System.Windows.Forms.SplitContainer Pendientes_SC;
		private System.Windows.Forms.TableLayoutPanel Listas_TLP;
        protected System.Windows.Forms.SplitContainer Content_SC;
        private System.Windows.Forms.Button Proveedor_BT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MedioPagoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PagoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn PagoVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoPagoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PagoImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn GastosBancarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn PendienteAsignacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn PagoCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;

    }
}
