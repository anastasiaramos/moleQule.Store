namespace moleQule.Face.Store
{
    partial class EmployeePaymentForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeePaymentForm));
            this.Pagos_GB = new System.Windows.Forms.GroupBox();
            this.Pagos_SC = new System.Windows.Forms.SplitContainer();
            this.Payments_TS = new System.Windows.Forms.ToolStrip();
            this.AddPago_TI = new System.Windows.Forms.ToolStripButton();
            this.EditPago_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewPago_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ChangeState_TI = new System.Windows.Forms.ToolStripSplitButton();
            this.UnlockItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.LockItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.NullItem_TMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPago_TI = new System.Windows.Forms.ToolStripButton();
            this.Pagos_DGW = new System.Windows.Forms.DataGridView();
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
            this.Payments_BS = new System.Windows.Forms.BindingSource(this.components);
            this.NAsociadas_GB = new System.Windows.Forms.GroupBox();
            this.Nominas_DGW = new System.Windows.Forms.DataGridView();
            this.IDRemesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaPrevisionPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPagoFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaDiasTranscurridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaAnteriores = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaAsignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Payrolls_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Empleado_GB = new System.Windows.Forms.GroupBox();
            this.Empleado_BT = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Summary_BS = new System.Windows.Forms.BindingSource(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
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
            this.PteFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PteDiasTranscurridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PtePendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unpaids_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Unpaids_TS = new System.Windows.Forms.ToolStrip();
            this.EditPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.VerPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.PrintListPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.PrintPendiente_TI = new System.Windows.Forms.ToolStripButton();
            this.Content_SC = new System.Windows.Forms.SplitContainer();
            this.Listas_TLP = new System.Windows.Forms.TableLayoutPanel();
            totalFacturadoLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            cobradoLabel = new System.Windows.Forms.Label();
            pendienteLabel = new System.Windows.Forms.Label();
            vencidoNoCobradoLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
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
            this.Payments_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).BeginInit();
            this.NAsociadas_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Nominas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payrolls_BS)).BeginInit();
            this.Empleado_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Summary_BS)).BeginInit();
            this.Pendientes_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_SC)).BeginInit();
            this.Pendientes_SC.Panel1.SuspendLayout();
            this.Pendientes_SC.Panel2.SuspendLayout();
            this.Pendientes_SC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Unpaids_BS)).BeginInit();
            this.Unpaids_TS.SuspendLayout();
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
            this.PanelesV.Panel1.Controls.Add(this.Content_SC);
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
            // Paneles2
            // 
            this.ErrorMng_EP.SetError(this.Paneles2, "F1 Ayuda        ");
            // 
            // Paneles2.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
            // 
            // Paneles2.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Paneles2, true);
            this.Paneles2.Size = new System.Drawing.Size(1092, 52);
            this.Paneles2.SplitterDistance = 27;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Enabled = true;
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(591, 7);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            this.Imprimir_Button.Visible = true;
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(300, 6);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
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
            totalFacturadoLabel.Location = new System.Drawing.Point(50, 126);
            totalFacturadoLabel.Name = "totalFacturadoLabel";
            totalFacturadoLabel.Size = new System.Drawing.Size(61, 13);
            totalFacturadoLabel.TabIndex = 0;
            totalFacturadoLabel.Text = "Total Neto:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(6, 45);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            codigoLabel.Location = new System.Drawing.Point(13, 92);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(44, 13);
            codigoLabel.TabIndex = 6;
            codigoLabel.Text = "Código:";
            // 
            // cobradoLabel
            // 
            cobradoLabel.AutoSize = true;
            cobradoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cobradoLabel.Location = new System.Drawing.Point(64, 152);
            cobradoLabel.Name = "cobradoLabel";
            cobradoLabel.Size = new System.Drawing.Size(47, 13);
            cobradoLabel.TabIndex = 8;
            cobradoLabel.Text = "Pagado:";
            // 
            // pendienteLabel
            // 
            pendienteLabel.AutoSize = true;
            pendienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pendienteLabel.Location = new System.Drawing.Point(52, 179);
            pendienteLabel.Name = "pendienteLabel";
            pendienteLabel.Size = new System.Drawing.Size(59, 13);
            pendienteLabel.TabIndex = 18;
            pendienteLabel.Text = "Pendiente:";
            // 
            // vencidoNoCobradoLabel
            // 
            vencidoNoCobradoLabel.AutoSize = true;
            vencidoNoCobradoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            vencidoNoCobradoLabel.Location = new System.Drawing.Point(13, 204);
            vencidoNoCobradoLabel.Name = "vencidoNoCobradoLabel";
            vencidoNoCobradoLabel.Size = new System.Drawing.Size(98, 13);
            vencidoNoCobradoLabel.TabIndex = 22;
            vencidoNoCobradoLabel.Text = "Efectos Devueltos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(13, 231);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(98, 13);
            label2.TabIndex = 26;
            label2.Text = "Efectos Ptes. Vto.:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(11, 294);
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
            this.Pagos_GB.Size = new System.Drawing.Size(847, 465);
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
            this.Pagos_SC.Panel1.Controls.Add(this.Payments_TS);
            this.Pagos_SC.Panel1MinSize = 39;
            // 
            // Pagos_SC.Panel2
            // 
            this.Pagos_SC.Panel2.Controls.Add(this.Pagos_DGW);
            this.Pagos_SC.Size = new System.Drawing.Size(841, 445);
            this.Pagos_SC.SplitterDistance = 39;
            this.Pagos_SC.SplitterWidth = 1;
            this.Pagos_SC.TabIndex = 6;
            // 
            // Payments_TS
            // 
            this.Payments_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Payments_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPago_TI,
            this.EditPago_TI,
            this.ViewPago_TI,
            this.toolStripLabel2,
            this.ChangeState_TI,
            this.PrintPago_TI});
            this.Payments_TS.Location = new System.Drawing.Point(0, 0);
            this.Payments_TS.Name = "Payments_TS";
            this.HelpProvider.SetShowHelp(this.Payments_TS, true);
            this.Payments_TS.Size = new System.Drawing.Size(841, 39);
            this.Payments_TS.TabIndex = 5;
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
            // ChangeState_TI
            // 
            this.ChangeState_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangeState_TI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UnlockItem_TMI,
            this.LockItem_TMI,
            this.NullItem_TMI});
            this.ChangeState_TI.Image = global::moleQule.Face.Store.Properties.Resources.change_state;
            this.ChangeState_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangeState_TI.Name = "ChangeState_TI";
            this.ChangeState_TI.Size = new System.Drawing.Size(48, 36);
            this.ChangeState_TI.Text = "Cambiar Estado";
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
            this.Pagos_DGW.DataSource = this.Payments_BS;
            this.Pagos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pagos_DGW.Location = new System.Drawing.Point(0, 0);
            this.Pagos_DGW.Name = "Pagos_DGW";
            this.Pagos_DGW.ReadOnly = true;
            this.Pagos_DGW.RowHeadersWidth = 15;
            this.Pagos_DGW.Size = new System.Drawing.Size(841, 405);
            this.Pagos_DGW.TabIndex = 0;
            this.Pagos_DGW.DoubleClick += new System.EventHandler(this.Pagos_DGW_DoubleClick);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "00000";
            this.Codigo.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle2.NullValue = null;
            this.PagoFecha.DefaultCellStyle = dataGridViewCellStyle2;
            this.PagoFecha.HeaderText = "Fecha";
            this.PagoFecha.Name = "PagoFecha";
            this.PagoFecha.ReadOnly = true;
            this.PagoFecha.Width = 95;
            // 
            // PagoVencimiento
            // 
            this.PagoVencimiento.DataPropertyName = "Vencimiento";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.PagoVencimiento.DefaultCellStyle = dataGridViewCellStyle3;
            this.PagoVencimiento.HeaderText = "Vto.";
            this.PagoVencimiento.Name = "PagoVencimiento";
            this.PagoVencimiento.ReadOnly = true;
            this.PagoVencimiento.Width = 65;
            // 
            // EstadoPagoLabel
            // 
            this.EstadoPagoLabel.DataPropertyName = "EstadoPagoLabel";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EstadoPagoLabel.DefaultCellStyle = dataGridViewCellStyle4;
            this.EstadoPagoLabel.HeaderText = "Estado Pago";
            this.EstadoPagoLabel.Name = "EstadoPagoLabel";
            this.EstadoPagoLabel.ReadOnly = true;
            this.EstadoPagoLabel.Width = 60;
            // 
            // PagoImporte
            // 
            this.PagoImporte.DataPropertyName = "Importe";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.PagoImporte.DefaultCellStyle = dataGridViewCellStyle5;
            this.PagoImporte.HeaderText = "Importe";
            this.PagoImporte.Name = "PagoImporte";
            this.PagoImporte.ReadOnly = true;
            this.PagoImporte.Width = 75;
            // 
            // GastosBancarios
            // 
            this.GastosBancarios.DataPropertyName = "GastosBancarios";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            this.GastosBancarios.DefaultCellStyle = dataGridViewCellStyle6;
            this.GastosBancarios.HeaderText = "Gastos Bancarios";
            this.GastosBancarios.Name = "GastosBancarios";
            this.GastosBancarios.ReadOnly = true;
            this.GastosBancarios.Width = 65;
            // 
            // PendienteAsignacion
            // 
            this.PendienteAsignacion.DataPropertyName = "Pendiente";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.PendienteAsignacion.DefaultCellStyle = dataGridViewCellStyle7;
            this.PendienteAsignacion.HeaderText = "Pendiente";
            this.PendienteAsignacion.Name = "PendienteAsignacion";
            this.PendienteAsignacion.ReadOnly = true;
            this.PendienteAsignacion.Width = 65;
            // 
            // PagoCuenta
            // 
            this.PagoCuenta.DataPropertyName = "CuentaBancaria";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PagoCuenta.DefaultCellStyle = dataGridViewCellStyle8;
            this.PagoCuenta.HeaderText = "Cuenta";
            this.PagoCuenta.Name = "PagoCuenta";
            this.PagoCuenta.ReadOnly = true;
            this.PagoCuenta.Width = 140;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Usuario.DefaultCellStyle = dataGridViewCellStyle9;
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
            // Payments_BS
            // 
            this.Payments_BS.DataSource = typeof(moleQule.Library.Store.PaymentInfo);
            // 
            // NAsociadas_GB
            // 
            this.NAsociadas_GB.Controls.Add(this.Nominas_DGW);
            this.NAsociadas_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NAsociadas_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAsociadas_GB.Location = new System.Drawing.Point(3, 474);
            this.NAsociadas_GB.Name = "NAsociadas_GB";
            this.NAsociadas_GB.Size = new System.Drawing.Size(847, 94);
            this.NAsociadas_GB.TabIndex = 1;
            this.NAsociadas_GB.TabStop = false;
            this.NAsociadas_GB.Text = "Nóminas Asociadas al Pago";
            // 
            // Nominas_DGW
            // 
            this.Nominas_DGW.AllowUserToAddRows = false;
            this.Nominas_DGW.AllowUserToDeleteRows = false;
            this.Nominas_DGW.AutoGenerateColumns = false;
            this.Nominas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRemesa,
            this.Descripcion,
            this.FacturaFecha,
            this.FacturaTotal,
            this.FacturaPrevisionPago,
            this.FechaPagoFactura,
            this.FacturaDiasTranscurridos,
            this.FacturaAnteriores,
            this.FacturaAsignado,
            this.FacturaPendiente});
            this.Nominas_DGW.DataSource = this.Payrolls_BS;
            this.Nominas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Nominas_DGW.Enabled = false;
            this.Nominas_DGW.Location = new System.Drawing.Point(3, 17);
            this.Nominas_DGW.MultiSelect = false;
            this.Nominas_DGW.Name = "Nominas_DGW";
            this.Nominas_DGW.ReadOnly = true;
            this.Nominas_DGW.RowHeadersVisible = false;
            this.Nominas_DGW.RowHeadersWidth = 15;
            this.Nominas_DGW.Size = new System.Drawing.Size(841, 74);
            this.Nominas_DGW.TabIndex = 0;
            // 
            // IDRemesa
            // 
            this.IDRemesa.DataPropertyName = "IDRemesa";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IDRemesa.DefaultCellStyle = dataGridViewCellStyle10;
            this.IDRemesa.HeaderText = "ID Remesa";
            this.IDRemesa.Name = "IDRemesa";
            this.IDRemesa.ReadOnly = true;
            this.IDRemesa.Width = 50;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 250;
            // 
            // FacturaFecha
            // 
            this.FacturaFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.FacturaFecha.DefaultCellStyle = dataGridViewCellStyle11;
            this.FacturaFecha.HeaderText = "Fecha";
            this.FacturaFecha.Name = "FacturaFecha";
            this.FacturaFecha.ReadOnly = true;
            this.FacturaFecha.Width = 75;
            // 
            // FacturaTotal
            // 
            this.FacturaTotal.DataPropertyName = "Total";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C2";
            dataGridViewCellStyle12.NullValue = null;
            this.FacturaTotal.DefaultCellStyle = dataGridViewCellStyle12;
            this.FacturaTotal.HeaderText = "Total";
            this.FacturaTotal.Name = "FacturaTotal";
            this.FacturaTotal.ReadOnly = true;
            this.FacturaTotal.Width = 80;
            // 
            // FacturaPrevisionPago
            // 
            this.FacturaPrevisionPago.DataPropertyName = "PrevisionPago";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "d";
            this.FacturaPrevisionPago.DefaultCellStyle = dataGridViewCellStyle13;
            this.FacturaPrevisionPago.HeaderText = "Fecha Previsión";
            this.FacturaPrevisionPago.Name = "FacturaPrevisionPago";
            this.FacturaPrevisionPago.ReadOnly = true;
            this.FacturaPrevisionPago.Width = 75;
            // 
            // FechaPagoFactura
            // 
            this.FechaPagoFactura.DataPropertyName = "FechaPago";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Format = "d";
            this.FechaPagoFactura.DefaultCellStyle = dataGridViewCellStyle14;
            this.FechaPagoFactura.HeaderText = "Fecha Asignación";
            this.FechaPagoFactura.Name = "FechaPagoFactura";
            this.FechaPagoFactura.ReadOnly = true;
            this.FechaPagoFactura.Width = 75;
            // 
            // FacturaDiasTranscurridos
            // 
            this.FacturaDiasTranscurridos.DataPropertyName = "DiasTranscurridos";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Format = "N0";
            dataGridViewCellStyle15.NullValue = null;
            this.FacturaDiasTranscurridos.DefaultCellStyle = dataGridViewCellStyle15;
            this.FacturaDiasTranscurridos.HeaderText = "Días";
            this.FacturaDiasTranscurridos.Name = "FacturaDiasTranscurridos";
            this.FacturaDiasTranscurridos.ReadOnly = true;
            this.FacturaDiasTranscurridos.Width = 40;
            // 
            // FacturaAnteriores
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "C2";
            dataGridViewCellStyle16.NullValue = null;
            this.FacturaAnteriores.DefaultCellStyle = dataGridViewCellStyle16;
            this.FacturaAnteriores.HeaderText = "Pagos Anteriores";
            this.FacturaAnteriores.Name = "FacturaAnteriores";
            this.FacturaAnteriores.ReadOnly = true;
            this.FacturaAnteriores.Width = 80;
            // 
            // FacturaAsignado
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "C2";
            dataGridViewCellStyle17.NullValue = null;
            this.FacturaAsignado.DefaultCellStyle = dataGridViewCellStyle17;
            this.FacturaAsignado.HeaderText = "Asignado";
            this.FacturaAsignado.Name = "FacturaAsignado";
            this.FacturaAsignado.ReadOnly = true;
            this.FacturaAsignado.Width = 80;
            // 
            // FacturaPendiente
            // 
            this.FacturaPendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "C2";
            dataGridViewCellStyle18.NullValue = null;
            this.FacturaPendiente.DefaultCellStyle = dataGridViewCellStyle18;
            this.FacturaPendiente.HeaderText = "Pendiente";
            this.FacturaPendiente.Name = "FacturaPendiente";
            this.FacturaPendiente.ReadOnly = true;
            this.FacturaPendiente.Width = 80;
            // 
            // Payrolls_BS
            // 
            this.Payrolls_BS.DataSource = typeof(moleQule.Library.Store.NominaInfo);
            // 
            // Empleado_GB
            // 
            this.Empleado_GB.Controls.Add(this.Empleado_BT);
            this.Empleado_GB.Controls.Add(label3);
            this.Empleado_GB.Controls.Add(this.textBox3);
            this.Empleado_GB.Controls.Add(label2);
            this.Empleado_GB.Controls.Add(this.textBox2);
            this.Empleado_GB.Controls.Add(vencidoNoCobradoLabel);
            this.Empleado_GB.Controls.Add(this.vencidoNoCobradoTextBox);
            this.Empleado_GB.Controls.Add(pendienteLabel);
            this.Empleado_GB.Controls.Add(this.Pendiente_TB);
            this.Empleado_GB.Controls.Add(cobradoLabel);
            this.Empleado_GB.Controls.Add(this.Pagado_TB);
            this.Empleado_GB.Controls.Add(codigoLabel);
            this.Empleado_GB.Controls.Add(this.codigoTextBox);
            this.Empleado_GB.Controls.Add(nombreLabel);
            this.Empleado_GB.Controls.Add(this.nombreTextBox);
            this.Empleado_GB.Controls.Add(totalFacturadoLabel);
            this.Empleado_GB.Controls.Add(this.Facturado_TB);
            this.Empleado_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Empleado_GB.Location = new System.Drawing.Point(1, 3);
            this.Empleado_GB.Name = "Empleado_GB";
            this.Empleado_GB.Size = new System.Drawing.Size(236, 647);
            this.Empleado_GB.TabIndex = 2;
            this.Empleado_GB.TabStop = false;
            this.Empleado_GB.Text = "Datos del Empleado";
            // 
            // Empleado_BT
            // 
            this.Empleado_BT.Image = global::moleQule.Face.Store.Properties.Resources.empleado;
            this.Empleado_BT.Location = new System.Drawing.Point(167, 20);
            this.Empleado_BT.Name = "Empleado_BT";
            this.Empleado_BT.Size = new System.Drawing.Size(55, 35);
            this.Empleado_BT.TabIndex = 30;
            this.Empleado_BT.UseVisualStyleBackColor = true;
            this.Empleado_BT.Click += new System.EventHandler(this.Empleado_BT_Click);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "Observaciones", true));
            this.textBox3.Location = new System.Drawing.Point(14, 310);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(208, 317);
            this.textBox3.TabIndex = 29;
            this.textBox3.TabStop = false;
            // 
            // Summary_BS
            // 
            this.Summary_BS.DataSource = typeof(moleQule.Library.Store.PaymentSummary);
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "EfectosPendientesVto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox2.Location = new System.Drawing.Point(118, 229);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(104, 21);
            this.textBox2.TabIndex = 27;
            this.textBox2.TabStop = false;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // vencidoNoCobradoTextBox
            // 
            this.vencidoNoCobradoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "EfectosDevueltos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.vencidoNoCobradoTextBox.Location = new System.Drawing.Point(118, 202);
            this.vencidoNoCobradoTextBox.Name = "vencidoNoCobradoTextBox";
            this.vencidoNoCobradoTextBox.ReadOnly = true;
            this.vencidoNoCobradoTextBox.Size = new System.Drawing.Size(104, 21);
            this.vencidoNoCobradoTextBox.TabIndex = 23;
            this.vencidoNoCobradoTextBox.TabStop = false;
            this.vencidoNoCobradoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Pendiente_TB
            // 
            this.Pendiente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "Pendiente", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Pendiente_TB.Location = new System.Drawing.Point(118, 176);
            this.Pendiente_TB.Name = "Pendiente_TB";
            this.Pendiente_TB.ReadOnly = true;
            this.Pendiente_TB.Size = new System.Drawing.Size(105, 21);
            this.Pendiente_TB.TabIndex = 19;
            this.Pendiente_TB.TabStop = false;
            this.Pendiente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Pagado_TB
            // 
            this.Pagado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "Pagado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Pagado_TB.Location = new System.Drawing.Point(118, 149);
            this.Pagado_TB.Name = "Pagado_TB";
            this.Pagado_TB.ReadOnly = true;
            this.Pagado_TB.Size = new System.Drawing.Size(105, 21);
            this.Pagado_TB.TabIndex = 9;
            this.Pagado_TB.TabStop = false;
            this.Pagado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "Codigo", true));
            this.codigoTextBox.Location = new System.Drawing.Point(63, 88);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.ReadOnly = true;
            this.codigoTextBox.Size = new System.Drawing.Size(89, 21);
            this.codigoTextBox.TabIndex = 7;
            this.codigoTextBox.TabStop = false;
            this.codigoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "Nombre", true));
            this.nombreTextBox.Location = new System.Drawing.Point(9, 61);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.ReadOnly = true;
            this.nombreTextBox.Size = new System.Drawing.Size(218, 21);
            this.nombreTextBox.TabIndex = 3;
            this.nombreTextBox.TabStop = false;
            // 
            // Facturado_TB
            // 
            this.Facturado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Summary_BS, "TotalFacturado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Facturado_TB.Location = new System.Drawing.Point(117, 123);
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
            this.Pendientes_GB.Location = new System.Drawing.Point(3, 574);
            this.Pendientes_GB.Name = "Pendientes_GB";
            this.Pendientes_GB.Size = new System.Drawing.Size(847, 144);
            this.Pendientes_GB.TabIndex = 5;
            this.Pendientes_GB.TabStop = false;
            this.Pendientes_GB.Text = "Nóminas Pendientes";
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
            this.Pendientes_SC.Panel2.Controls.Add(this.Unpaids_TS);
            this.Pendientes_SC.Panel2MinSize = 34;
            this.Pendientes_SC.Size = new System.Drawing.Size(841, 124);
            this.Pendientes_SC.SplitterDistance = 803;
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
            this.PteFecha,
            this.PteTotal,
            this.PteDiasTranscurridos,
            this.PtePendiente});
            this.Pendientes_DGW.DataSource = this.Unpaids_BS;
            this.Pendientes_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pendientes_DGW.Enabled = false;
            this.Pendientes_DGW.Location = new System.Drawing.Point(0, 0);
            this.Pendientes_DGW.Name = "Pendientes_DGW";
            this.Pendientes_DGW.ReadOnly = true;
            this.Pendientes_DGW.RowHeadersVisible = false;
            this.Pendientes_DGW.RowHeadersWidth = 15;
            this.Pendientes_DGW.Size = new System.Drawing.Size(803, 124);
            this.Pendientes_DGW.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IDRemesa";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID Remesa";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Descripcion";
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // PteFecha
            // 
            this.PteFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.Format = "d";
            dataGridViewCellStyle20.NullValue = null;
            this.PteFecha.DefaultCellStyle = dataGridViewCellStyle20;
            this.PteFecha.HeaderText = "Fecha";
            this.PteFecha.Name = "PteFecha";
            this.PteFecha.ReadOnly = true;
            this.PteFecha.Width = 75;
            // 
            // PteTotal
            // 
            this.PteTotal.DataPropertyName = "Total";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Format = "C2";
            dataGridViewCellStyle21.NullValue = null;
            this.PteTotal.DefaultCellStyle = dataGridViewCellStyle21;
            this.PteTotal.HeaderText = "Total";
            this.PteTotal.Name = "PteTotal";
            this.PteTotal.ReadOnly = true;
            this.PteTotal.Width = 80;
            // 
            // PteDiasTranscurridos
            // 
            this.PteDiasTranscurridos.DataPropertyName = "DiasTranscurridos";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Format = "N0";
            this.PteDiasTranscurridos.DefaultCellStyle = dataGridViewCellStyle22;
            this.PteDiasTranscurridos.HeaderText = "Días";
            this.PteDiasTranscurridos.Name = "PteDiasTranscurridos";
            this.PteDiasTranscurridos.ReadOnly = true;
            this.PteDiasTranscurridos.Width = 40;
            // 
            // PtePendiente
            // 
            this.PtePendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "C2";
            dataGridViewCellStyle23.NullValue = null;
            this.PtePendiente.DefaultCellStyle = dataGridViewCellStyle23;
            this.PtePendiente.HeaderText = "Pendiente";
            this.PtePendiente.Name = "PtePendiente";
            this.PtePendiente.ReadOnly = true;
            this.PtePendiente.Width = 90;
            // 
            // Unpaids_BS
            // 
            this.Unpaids_BS.DataSource = typeof(moleQule.Library.Store.NominaInfo);
            // 
            // Unpaids_TS
            // 
            this.Unpaids_TS.Dock = System.Windows.Forms.DockStyle.Left;
            this.Unpaids_TS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Unpaids_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Unpaids_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditPendiente_TI,
            this.VerPendiente_TI,
            this.PrintListPendiente_TI,
            this.PrintPendiente_TI});
            this.Unpaids_TS.Location = new System.Drawing.Point(0, 0);
            this.Unpaids_TS.Name = "Unpaids_TS";
            this.HelpProvider.SetShowHelp(this.Unpaids_TS, true);
            this.Unpaids_TS.Size = new System.Drawing.Size(37, 124);
            this.Unpaids_TS.TabIndex = 6;
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
            this.Content_SC.Panel2.Controls.Add(this.Empleado_GB);
            this.Content_SC.Panel2MinSize = 238;
            this.Content_SC.Size = new System.Drawing.Size(1092, 721);
            this.Content_SC.SplitterDistance = 853;
            this.Content_SC.SplitterWidth = 1;
            this.Content_SC.TabIndex = 6;
            // 
            // Listas_TLP
            // 
            this.Listas_TLP.ColumnCount = 1;
            this.Listas_TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Listas_TLP.Controls.Add(this.Pagos_GB, 0, 0);
            this.Listas_TLP.Controls.Add(this.NAsociadas_GB, 0, 1);
            this.Listas_TLP.Controls.Add(this.Pendientes_GB, 0, 2);
            this.Listas_TLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Listas_TLP.Location = new System.Drawing.Point(0, 0);
            this.Listas_TLP.Name = "Listas_TLP";
            this.Listas_TLP.RowCount = 3;
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Listas_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.Listas_TLP.Size = new System.Drawing.Size(853, 721);
            this.Listas_TLP.TabIndex = 6;
            // 
            // EmployeePaymentForm
            // 
            this.ClientSize = new System.Drawing.Size(1094, 778);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeePaymentForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PagoForm";
            this.Shown += new System.EventHandler(this.PagoForm_Shown);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
            this.Paneles2.ResumeLayout(false);
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
            this.Payments_TS.ResumeLayout(false);
            this.Payments_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pagos_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).EndInit();
            this.NAsociadas_GB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Nominas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payrolls_BS)).EndInit();
            this.Empleado_GB.ResumeLayout(false);
            this.Empleado_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Summary_BS)).EndInit();
            this.Pendientes_GB.ResumeLayout(false);
            this.Pendientes_SC.Panel1.ResumeLayout(false);
            this.Pendientes_SC.Panel2.ResumeLayout(false);
            this.Pendientes_SC.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_SC)).EndInit();
            this.Pendientes_SC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pendientes_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Unpaids_BS)).EndInit();
            this.Unpaids_TS.ResumeLayout(false);
            this.Unpaids_TS.PerformLayout();
            this.Content_SC.Panel1.ResumeLayout(false);
            this.Content_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_SC)).EndInit();
            this.Content_SC.ResumeLayout(false);
            this.Listas_TLP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox Pagos_GB;
        protected System.Windows.Forms.GroupBox NAsociadas_GB;
        protected System.Windows.Forms.GroupBox Empleado_GB;
        protected System.Windows.Forms.TextBox Pagado_TB;
        protected System.Windows.Forms.TextBox codigoTextBox;
        protected System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.TextBox Facturado_TB;
        protected System.Windows.Forms.TextBox Pendiente_TB;
        protected System.Windows.Forms.DataGridView Nominas_DGW;
		protected System.Windows.Forms.BindingSource Payrolls_BS;
        protected System.Windows.Forms.DataGridView Pagos_DGW;
        protected System.Windows.Forms.BindingSource Payments_BS;
        protected System.Windows.Forms.BindingSource Summary_BS;
		protected System.Windows.Forms.GroupBox Pendientes_GB;
        protected System.Windows.Forms.BindingSource Unpaids_BS;
        protected System.Windows.Forms.TextBox vencidoNoCobradoTextBox;
        protected System.Windows.Forms.TextBox textBox2;
        protected System.Windows.Forms.TextBox textBox3;
        protected System.Windows.Forms.DataGridView Pendientes_DGW;
		protected System.Windows.Forms.ToolStrip Unpaids_TS;
		protected System.Windows.Forms.ToolStripButton VerPendiente_TI;
		protected System.Windows.Forms.ToolStripButton PrintListPendiente_TI;
        protected System.Windows.Forms.ToolStripButton PrintPendiente_TI;
		protected System.Windows.Forms.SplitContainer Pagos_SC;
		protected System.Windows.Forms.ToolStrip Payments_TS;
		protected System.Windows.Forms.ToolStripButton AddPago_TI;
		protected System.Windows.Forms.ToolStripButton EditPago_TI;
		protected System.Windows.Forms.ToolStripButton ViewPago_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripButton PrintPago_TI;
		private System.Windows.Forms.ToolStripSplitButton ChangeState_TI;
		private System.Windows.Forms.ToolStripMenuItem UnlockItem_TMI;
		private System.Windows.Forms.ToolStripMenuItem LockItem_TMI;
		private System.Windows.Forms.ToolStripMenuItem NullItem_TMI;
        protected System.Windows.Forms.ToolStripButton EditPendiente_TI;
		protected System.Windows.Forms.SplitContainer Pendientes_SC;
		private System.Windows.Forms.TableLayoutPanel Listas_TLP;
        protected System.Windows.Forms.SplitContainer Content_SC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PteFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn PteTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PteDiasTranscurridos;
        private System.Windows.Forms.DataGridViewTextBoxColumn PtePendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRemesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaPrevisionPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPagoFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaDiasTranscurridos;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaAnteriores;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaAsignado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaPendiente;
        private System.Windows.Forms.Button Empleado_BT;
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
