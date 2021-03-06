namespace moleQule.Face.Store
{
    partial class LoanPaymentForm
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
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label importeLabel;
            System.Windows.Forms.Label vencimientoLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label tipoLabel;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label cuentaLabel;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanPaymentForm));
            this.Main_SC = new System.Windows.Forms.SplitContainer();
            this.Payment_GB = new System.Windows.Forms.GroupBox();
            this.Source_Panel = new System.Windows.Forms.Panel();
            this.EstadoPago_TB = new System.Windows.Forms.TextBox();
            this.EstadoPago_BT = new System.Windows.Forms.Button();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.MedioPago_TB = new System.Windows.Forms.TextBox();
            this.Cuenta_TB = new System.Windows.Forms.TextBox();
            this.MedioPago_BT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GastosBancarios_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.NoAsignado_TB = new System.Windows.Forms.TextBox();
            this.Tarjeta_BT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.Tarjeta_TB = new System.Windows.Forms.TextBox();
            this.Vencimiento_DTP = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Importe_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Liberar_BT = new System.Windows.Forms.Button();
            this.Cuenta_BT = new System.Windows.Forms.Button();
            this.Repartir_BT = new System.Windows.Forms.Button();
            this.Gastos_GB = new System.Windows.Forms.GroupBox();
            this.Gastos_Panel = new System.Windows.Forms.SplitContainer();
            this.Facturas_TS = new System.Windows.Forms.ToolStrip();
            this.AddFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.EditFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Lineas_DGW = new System.Windows.Forms.DataGridView();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new CalendarColumn();
            this.FechaVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPagado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acumulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObservacionesCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vinculado = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Datos_Lineas = new System.Windows.Forms.BindingSource(this.components);
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            importeLabel = new System.Windows.Forms.Label();
            vencimientoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            tipoLabel = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            cuentaLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Main_SC)).BeginInit();
            this.Main_SC.Panel1.SuspendLayout();
            this.Main_SC.Panel2.SuspendLayout();
            this.Main_SC.SuspendLayout();
            this.Payment_GB.SuspendLayout();
            this.Source_Panel.SuspendLayout();
            this.Gastos_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gastos_Panel)).BeginInit();
            this.Gastos_Panel.Panel1.SuspendLayout();
            this.Gastos_Panel.Panel2.SuspendLayout();
            this.Gastos_Panel.SuspendLayout();
            this.Facturas_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.Main_SC);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(1018, 674);
            this.PanelesV.SplitterDistance = 619;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(252, 6);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
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
            this.Paneles2.Size = new System.Drawing.Size(1016, 52);
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(190, 8);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.ExpenseInfo);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(330, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1018, 674);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(472, 385);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(472, 300);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(522, 148);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(93, 13);
            label6.TabIndex = 97;
            label6.Text = "Gastos Bancarios:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(295, 69);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(99, 13);
            label5.TabIndex = 95;
            label5.Text = "Tarjeta de Crédito:";
            // 
            // importeLabel
            // 
            importeLabel.AutoSize = true;
            importeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            importeLabel.Location = new System.Drawing.Point(36, 73);
            importeLabel.Name = "importeLabel";
            importeLabel.Size = new System.Drawing.Size(49, 13);
            importeLabel.TabIndex = 87;
            importeLabel.Text = "Importe:";
            // 
            // vencimientoLabel
            // 
            vencimientoLabel.AutoSize = true;
            vencimientoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            vencimientoLabel.Location = new System.Drawing.Point(326, 124);
            vencimientoLabel.Name = "vencimientoLabel";
            vencimientoLabel.Size = new System.Drawing.Size(68, 13);
            vencimientoLabel.TabIndex = 86;
            vencimientoLabel.Text = "Vencimiento:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(354, 97);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 85;
            fechaLabel.Text = "Fecha:";
            // 
            // tipoLabel
            // 
            tipoLabel.AutoSize = true;
            tipoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tipoLabel.Location = new System.Drawing.Point(322, 15);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new System.Drawing.Size(72, 13);
            tipoLabel.TabIndex = 84;
            tipoLabel.Text = "Medio Pago*:";
            // 
            // observacionesLabel
            // 
            observacionesLabel.AutoSize = true;
            observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            observacionesLabel.Location = new System.Drawing.Point(658, 14);
            observacionesLabel.Name = "observacionesLabel";
            observacionesLabel.Size = new System.Drawing.Size(82, 13);
            observacionesLabel.TabIndex = 83;
            observacionesLabel.Text = "Observaciones:";
            // 
            // cuentaLabel
            // 
            cuentaLabel.AutoSize = true;
            cuentaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cuentaLabel.Location = new System.Drawing.Point(348, 43);
            cuentaLabel.Name = "cuentaLabel";
            cuentaLabel.Size = new System.Drawing.Size(46, 13);
            cuentaLabel.TabIndex = 82;
            cuentaLabel.Text = "Cuenta:";
            // 
            // label7
            // 
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label7.Location = new System.Drawing.Point(346, 148);
            label7.Name = "label7";
            this.HelpProvider.SetShowHelp(label7, true);
            label7.Size = new System.Drawing.Size(48, 23);
            label7.TabIndex = 107;
            label7.Text = "Estado:";
            // 
            // Main_SC
            // 
            this.Main_SC.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Main_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Main_SC.Location = new System.Drawing.Point(0, 0);
            this.Main_SC.Name = "Main_SC";
            this.Main_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Main_SC.Panel1
            // 
            this.Main_SC.Panel1.AutoScroll = true;
            this.Main_SC.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Main_SC.Panel1.Controls.Add(this.Payment_GB);
            // 
            // Main_SC.Panel2
            // 
            this.Main_SC.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Main_SC.Panel2.Controls.Add(this.Gastos_GB);
            this.Main_SC.Size = new System.Drawing.Size(1016, 617);
            this.Main_SC.SplitterDistance = 201;
            this.Main_SC.TabIndex = 7;
            // 
            // Payment_GB
            // 
            this.Payment_GB.Controls.Add(this.Source_Panel);
            this.Payment_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Payment_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Payment_GB.Location = new System.Drawing.Point(0, 0);
            this.Payment_GB.Name = "Payment_GB";
            this.Payment_GB.Size = new System.Drawing.Size(1016, 201);
            this.Payment_GB.TabIndex = 0;
            this.Payment_GB.TabStop = false;
            this.Payment_GB.Text = "Datos del Pago";
            // 
            // Source_Panel
            // 
            this.Source_Panel.Controls.Add(label7);
            this.Source_Panel.Controls.Add(this.EstadoPago_TB);
            this.Source_Panel.Controls.Add(this.EstadoPago_BT);
            this.Source_Panel.Controls.Add(this.Codigo_TB);
            this.Source_Panel.Controls.Add(this.MedioPago_TB);
            this.Source_Panel.Controls.Add(this.Cuenta_TB);
            this.Source_Panel.Controls.Add(this.MedioPago_BT);
            this.Source_Panel.Controls.Add(cuentaLabel);
            this.Source_Panel.Controls.Add(this.label2);
            this.Source_Panel.Controls.Add(this.Observaciones_RTB);
            this.Source_Panel.Controls.Add(this.textBox1);
            this.Source_Panel.Controls.Add(observacionesLabel);
            this.Source_Panel.Controls.Add(label6);
            this.Source_Panel.Controls.Add(tipoLabel);
            this.Source_Panel.Controls.Add(this.GastosBancarios_NTB);
            this.Source_Panel.Controls.Add(this.NoAsignado_TB);
            this.Source_Panel.Controls.Add(this.Tarjeta_BT);
            this.Source_Panel.Controls.Add(this.label1);
            this.Source_Panel.Controls.Add(label5);
            this.Source_Panel.Controls.Add(this.Fecha_DTP);
            this.Source_Panel.Controls.Add(this.Tarjeta_TB);
            this.Source_Panel.Controls.Add(fechaLabel);
            this.Source_Panel.Controls.Add(this.Vencimiento_DTP);
            this.Source_Panel.Controls.Add(vencimientoLabel);
            this.Source_Panel.Controls.Add(this.label3);
            this.Source_Panel.Controls.Add(this.Importe_NTB);
            this.Source_Panel.Controls.Add(importeLabel);
            this.Source_Panel.Controls.Add(this.Liberar_BT);
            this.Source_Panel.Controls.Add(this.Cuenta_BT);
            this.Source_Panel.Controls.Add(this.Repartir_BT);
            this.Source_Panel.Location = new System.Drawing.Point(9, 16);
            this.Source_Panel.Name = "Source_Panel";
            this.Source_Panel.Size = new System.Drawing.Size(999, 175);
            this.Source_Panel.TabIndex = 102;
            // 
            // EstadoPago_TB
            // 
            this.EstadoPago_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoPagoLabel", true));
            this.EstadoPago_TB.Location = new System.Drawing.Point(400, 145);
            this.EstadoPago_TB.Name = "EstadoPago_TB";
            this.EstadoPago_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.EstadoPago_TB, true);
            this.EstadoPago_TB.Size = new System.Drawing.Size(78, 21);
            this.EstadoPago_TB.TabIndex = 106;
            this.EstadoPago_TB.TabStop = false;
            // 
            // EstadoPago_BT
            // 
            this.EstadoPago_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.EstadoPago_BT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EstadoPago_BT.Location = new System.Drawing.Point(484, 145);
            this.EstadoPago_BT.Name = "EstadoPago_BT";
            this.HelpProvider.SetShowHelp(this.EstadoPago_BT, true);
            this.EstadoPago_BT.Size = new System.Drawing.Size(28, 23);
            this.EstadoPago_BT.TabIndex = 105;
            this.EstadoPago_BT.UseVisualStyleBackColor = true;
            this.EstadoPago_BT.Click += new System.EventHandler(this.EstadoPago_BT_Click);
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.Codigo_TB.Enabled = false;
            this.Codigo_TB.Location = new System.Drawing.Point(68, 11);
            this.Codigo_TB.Name = "Codigo_TB";
            this.Codigo_TB.ReadOnly = true;
            this.Codigo_TB.Size = new System.Drawing.Size(78, 21);
            this.Codigo_TB.TabIndex = 89;
            this.Codigo_TB.TabStop = false;
            this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MedioPago_TB
            // 
            this.MedioPago_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "MedioPagoLabel", true));
            this.MedioPago_TB.Location = new System.Drawing.Point(400, 12);
            this.MedioPago_TB.Name = "MedioPago_TB";
            this.MedioPago_TB.ReadOnly = true;
            this.MedioPago_TB.Size = new System.Drawing.Size(200, 21);
            this.MedioPago_TB.TabIndex = 101;
            this.MedioPago_TB.TabStop = false;
            // 
            // Cuenta_TB
            // 
            this.Cuenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaBancaria", true));
            this.Cuenta_TB.Location = new System.Drawing.Point(400, 39);
            this.Cuenta_TB.Name = "Cuenta_TB";
            this.Cuenta_TB.ReadOnly = true;
            this.Cuenta_TB.Size = new System.Drawing.Size(200, 21);
            this.Cuenta_TB.TabIndex = 72;
            this.Cuenta_TB.TabStop = false;
            // 
            // MedioPago_BT
            // 
            this.MedioPago_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.MedioPago_BT.Location = new System.Drawing.Point(606, 12);
            this.MedioPago_BT.Name = "MedioPago_BT";
            this.MedioPago_BT.Size = new System.Drawing.Size(28, 22);
            this.MedioPago_BT.TabIndex = 3;
            this.MedioPago_BT.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Tipo:";
            // 
            // Observaciones_RTB
            // 
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_RTB.Location = new System.Drawing.Point(661, 32);
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.Observaciones_RTB.Size = new System.Drawing.Size(320, 105);
            this.Observaciones_RTB.TabIndex = 10;
            this.Observaciones_RTB.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoPagoLabel", true));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(195, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(101, 21);
            this.textBox1.TabIndex = 98;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GastosBancarios_NTB
            // 
            this.GastosBancarios_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "GastosBancarios", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.GastosBancarios_NTB.Location = new System.Drawing.Point(621, 145);
            this.GastosBancarios_NTB.Name = "GastosBancarios_NTB";
            this.GastosBancarios_NTB.Size = new System.Drawing.Size(54, 21);
            this.GastosBancarios_NTB.TabIndex = 9;
            this.GastosBancarios_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GastosBancarios_NTB.TextIsCurrency = false;
            this.GastosBancarios_NTB.TextIsInteger = false;
            // 
            // NoAsignado_TB
            // 
            this.NoAsignado_TB.Enabled = false;
            this.NoAsignado_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NoAsignado_TB.Location = new System.Drawing.Point(141, 92);
            this.NoAsignado_TB.Name = "NoAsignado_TB";
            this.NoAsignado_TB.ReadOnly = true;
            this.NoAsignado_TB.Size = new System.Drawing.Size(98, 21);
            this.NoAsignado_TB.TabIndex = 71;
            this.NoAsignado_TB.TabStop = false;
            this.NoAsignado_TB.Text = "0.00";
            this.NoAsignado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Tarjeta_BT
            // 
            this.Tarjeta_BT.Enabled = false;
            this.Tarjeta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Tarjeta_BT.Location = new System.Drawing.Point(606, 65);
            this.Tarjeta_BT.Name = "Tarjeta_BT";
            this.Tarjeta_BT.Size = new System.Drawing.Size(29, 22);
            this.Tarjeta_BT.TabIndex = 5;
            this.Tarjeta_BT.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(138, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "No Asignado:";
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(400, 93);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(145, 21);
            this.Fecha_DTP.TabIndex = 6;
            // 
            // Tarjeta_TB
            // 
            this.Tarjeta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TarjetaCredito", true));
            this.Tarjeta_TB.Location = new System.Drawing.Point(400, 66);
            this.Tarjeta_TB.Name = "Tarjeta_TB";
            this.Tarjeta_TB.ReadOnly = true;
            this.Tarjeta_TB.Size = new System.Drawing.Size(200, 21);
            this.Tarjeta_TB.TabIndex = 93;
            this.Tarjeta_TB.TabStop = false;
            // 
            // Vencimiento_DTP
            // 
            this.Vencimiento_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vencimiento_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vencimiento_DTP.Location = new System.Drawing.Point(400, 120);
            this.Vencimiento_DTP.Name = "Vencimiento_DTP";
            this.Vencimiento_DTP.ShowCheckBox = true;
            this.Vencimiento_DTP.Size = new System.Drawing.Size(145, 21);
            this.Vencimiento_DTP.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "Código:";
            // 
            // Importe_NTB
            // 
            this.Importe_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Importe", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Importe_NTB.Location = new System.Drawing.Point(37, 92);
            this.Importe_NTB.Name = "Importe_NTB";
            this.Importe_NTB.Size = new System.Drawing.Size(98, 21);
            this.Importe_NTB.TabIndex = 0;
            this.Importe_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Importe_NTB.TextIsCurrency = false;
            this.Importe_NTB.TextIsInteger = false;
            // 
            // Liberar_BT
            // 
            this.Liberar_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Liberar_BT.Location = new System.Drawing.Point(68, 119);
            this.Liberar_BT.Name = "Liberar_BT";
            this.Liberar_BT.Size = new System.Drawing.Size(67, 23);
            this.Liberar_BT.TabIndex = 1;
            this.Liberar_BT.Tag = "NO FORMAT";
            this.Liberar_BT.Text = "Ninguno";
            this.Liberar_BT.UseVisualStyleBackColor = true;
            // 
            // Cuenta_BT
            // 
            this.Cuenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Cuenta_BT.Location = new System.Drawing.Point(606, 39);
            this.Cuenta_BT.Name = "Cuenta_BT";
            this.Cuenta_BT.Size = new System.Drawing.Size(29, 21);
            this.Cuenta_BT.TabIndex = 4;
            this.Cuenta_BT.UseVisualStyleBackColor = true;
            // 
            // Repartir_BT
            // 
            this.Repartir_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Repartir_BT.Location = new System.Drawing.Point(141, 119);
            this.Repartir_BT.Name = "Repartir_BT";
            this.Repartir_BT.Size = new System.Drawing.Size(67, 23);
            this.Repartir_BT.TabIndex = 2;
            this.Repartir_BT.Tag = "NO FORMAT";
            this.Repartir_BT.Text = "Repartir";
            this.Repartir_BT.UseVisualStyleBackColor = true;
            // 
            // Gastos_GB
            // 
            this.Gastos_GB.Controls.Add(this.Gastos_Panel);
            this.Gastos_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gastos_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gastos_GB.Location = new System.Drawing.Point(0, 0);
            this.Gastos_GB.Name = "Gastos_GB";
            this.Gastos_GB.Size = new System.Drawing.Size(1016, 412);
            this.Gastos_GB.TabIndex = 0;
            this.Gastos_GB.TabStop = false;
            this.Gastos_GB.Text = "Préstamos Pendientes de Pago";
            // 
            // Gastos_Panel
            // 
            this.Gastos_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gastos_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Gastos_Panel.Location = new System.Drawing.Point(3, 17);
            this.Gastos_Panel.Name = "Gastos_Panel";
            this.Gastos_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Gastos_Panel.Panel1
            // 
            this.Gastos_Panel.Panel1.Controls.Add(this.Facturas_TS);
            this.Gastos_Panel.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // Gastos_Panel.Panel2
            // 
            this.Gastos_Panel.Panel2.Controls.Add(this.Lineas_DGW);
            this.Gastos_Panel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Gastos_Panel.Size = new System.Drawing.Size(1010, 392);
            this.Gastos_Panel.SplitterDistance = 38;
            this.Gastos_Panel.SplitterWidth = 1;
            this.Gastos_Panel.TabIndex = 8;
            // 
            // Facturas_TS
            // 
            this.Facturas_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Facturas_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFactura_TI,
            this.EditFactura_TI,
            this.ViewFactura_TI,
            this.DeleteFactura_TI,
            this.toolStripLabel2});
            this.Facturas_TS.Location = new System.Drawing.Point(0, 0);
            this.Facturas_TS.Name = "Facturas_TS";
            this.HelpProvider.SetShowHelp(this.Facturas_TS, true);
            this.Facturas_TS.Size = new System.Drawing.Size(1010, 39);
            this.Facturas_TS.TabIndex = 5;
            // 
            // AddFactura_TI
            // 
            this.AddFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddFactura_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddFactura_TI.Name = "AddFactura_TI";
            this.AddFactura_TI.Size = new System.Drawing.Size(36, 36);
            this.AddFactura_TI.Text = "Nuevo";
            this.AddFactura_TI.Visible = false;
            // 
            // EditFactura_TI
            // 
            this.EditFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditFactura_TI.Enabled = false;
            this.EditFactura_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditFactura_TI.Name = "EditFactura_TI";
            this.EditFactura_TI.Size = new System.Drawing.Size(36, 36);
            this.EditFactura_TI.Text = "Editar";
            // 
            // ViewFactura_TI
            // 
            this.ViewFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewFactura_TI.Enabled = false;
            this.ViewFactura_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            this.ViewFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewFactura_TI.Name = "ViewFactura_TI";
            this.ViewFactura_TI.Size = new System.Drawing.Size(36, 36);
            this.ViewFactura_TI.Text = "Ver";
            // 
            // DeleteFactura_TI
            // 
            this.DeleteFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteFactura_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            this.DeleteFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteFactura_TI.Name = "DeleteFactura_TI";
            this.DeleteFactura_TI.Size = new System.Drawing.Size(36, 36);
            this.DeleteFactura_TI.Text = "Borrar";
            this.DeleteFactura_TI.Visible = false;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 36);
            // 
            // Lineas_DGW
            // 
            this.Lineas_DGW.AllowUserToAddRows = false;
            this.Lineas_DGW.AllowUserToDeleteRows = false;
            this.Lineas_DGW.AutoGenerateColumns = false;
            this.Lineas_DGW.ColumnHeadersHeight = 36;
            this.Lineas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.DescripcionCol,
            this.fechaDataGridViewTextBoxColumn,
            this.FechaVencimiento,
            this.Total,
            this.TotalPagado,
            this.Pendiente,
            this.Acumulado,
            this.Asignado,
            this.ObservacionesCol,
            this.Vinculado});
            this.Lineas_DGW.DataSource = this.Datos_Lineas;
            this.Lineas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lineas_DGW.Location = new System.Drawing.Point(0, 0);
            this.Lineas_DGW.MultiSelect = false;
            this.Lineas_DGW.Name = "Lineas_DGW";
            this.Lineas_DGW.ReadOnly = true;
            this.Lineas_DGW.RowHeadersWidth = 25;
            this.Lineas_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Lineas_DGW.Size = new System.Drawing.Size(1010, 353);
            this.Lineas_DGW.TabIndex = 3;
            this.Lineas_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gastos_DGW_CellClick);
            this.Lineas_DGW.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gastos_DGW_DoubleClick);
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "ID";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            this.codigoDataGridViewTextBoxColumn.Width = 45;
            // 
            // DescripcionCol
            // 
            this.DescripcionCol.DataPropertyName = "Nombre";
            this.DescripcionCol.HeaderText = "Préstamo";
            this.DescripcionCol.Name = "DescripcionCol";
            this.DescripcionCol.ReadOnly = true;
            this.DescripcionCol.Width = 200;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "d";
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            this.fechaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fechaDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.fechaDataGridViewTextBoxColumn.Width = 70;
            // 
            // FechaVencimiento
            // 
            this.FechaVencimiento.DataPropertyName = "FechaVencimiento";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            this.FechaVencimiento.DefaultCellStyle = dataGridViewCellStyle3;
            this.FechaVencimiento.HeaderText = "Fecha Vto.";
            this.FechaVencimiento.Name = "FechaVencimiento";
            this.FechaVencimiento.ReadOnly = true;
            this.FechaVencimiento.Width = 70;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Importe";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.Total.DefaultCellStyle = dataGridViewCellStyle4;
            this.Total.HeaderText = "Importe";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 80;
            // 
            // TotalPagado
            // 
            this.TotalPagado.DataPropertyName = "TotalPagado";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.TotalPagado.DefaultCellStyle = dataGridViewCellStyle5;
            this.TotalPagado.HeaderText = "Importe Pagado";
            this.TotalPagado.Name = "TotalPagado";
            this.TotalPagado.ReadOnly = true;
            this.TotalPagado.Width = 70;
            // 
            // Pendiente
            // 
            this.Pendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            this.Pendiente.DefaultCellStyle = dataGridViewCellStyle6;
            this.Pendiente.HeaderText = "Importe Pendiente";
            this.Pendiente.Name = "Pendiente";
            this.Pendiente.ReadOnly = true;
            this.Pendiente.Width = 70;
            // 
            // Acumulado
            // 
            this.Acumulado.DataPropertyName = "Acumulado";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Acumulado.DefaultCellStyle = dataGridViewCellStyle7;
            this.Acumulado.HeaderText = "Importe Acumulado";
            this.Acumulado.Name = "Acumulado";
            this.Acumulado.ReadOnly = true;
            this.Acumulado.Width = 75;
            // 
            // Asignado
            // 
            this.Asignado.DataPropertyName = "Asignado";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.Asignado.DefaultCellStyle = dataGridViewCellStyle8;
            this.Asignado.HeaderText = "Importe Asignado";
            this.Asignado.Name = "Asignado";
            this.Asignado.ReadOnly = true;
            this.Asignado.Width = 70;
            // 
            // ObservacionesCol
            // 
            this.ObservacionesCol.DataPropertyName = "Observaciones";
            this.ObservacionesCol.HeaderText = "Observaciones";
            this.ObservacionesCol.Name = "ObservacionesCol";
            this.ObservacionesCol.ReadOnly = true;
            this.ObservacionesCol.Width = 150;
            // 
            // Vinculado
            // 
            this.Vinculado.DataPropertyName = "Vinculado";
            this.Vinculado.HeaderText = "Vinculado";
            this.Vinculado.Name = "Vinculado";
            this.Vinculado.ReadOnly = true;
            this.Vinculado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Vinculado.Width = 70;
            // 
            // Datos_Lineas
            // 
            this.Datos_Lineas.DataSource = typeof(moleQule.Library.Store.Loans);
            // 
            // PagoPrestamoForm
            // 
            this.ClientSize = new System.Drawing.Size(1018, 674);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PagoPrestamoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Pago de Préstamos";
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
            this.Main_SC.Panel1.ResumeLayout(false);
            this.Main_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Main_SC)).EndInit();
            this.Main_SC.ResumeLayout(false);
            this.Payment_GB.ResumeLayout(false);
            this.Source_Panel.ResumeLayout(false);
            this.Source_Panel.PerformLayout();
            this.Gastos_GB.ResumeLayout(false);
            this.Gastos_Panel.Panel1.ResumeLayout(false);
            this.Gastos_Panel.Panel1.PerformLayout();
            this.Gastos_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gastos_Panel)).EndInit();
            this.Gastos_Panel.ResumeLayout(false);
            this.Facturas_TS.ResumeLayout(false);
            this.Facturas_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.BindingSource Datos_Lineas;
        private System.Windows.Forms.SplitContainer Main_SC;
		protected Controls.NumericTextBox GastosBancarios_NTB;
		protected System.Windows.Forms.TextBox Tarjeta_TB;
        private System.Windows.Forms.Label label3;
		protected System.Windows.Forms.Button Cuenta_BT;
		protected System.Windows.Forms.Button Repartir_BT;
		protected System.Windows.Forms.Button Liberar_BT;
		protected Controls.NumericTextBox Importe_NTB;
		protected System.Windows.Forms.DateTimePicker Vencimiento_DTP;
		protected System.Windows.Forms.DateTimePicker Fecha_DTP;
		private System.Windows.Forms.Label label1;
		protected System.Windows.Forms.RichTextBox Observaciones_RTB;
		protected System.Windows.Forms.TextBox Cuenta_TB;
		private System.Windows.Forms.GroupBox Gastos_GB;
		protected System.Windows.Forms.SplitContainer Gastos_Panel;
		protected System.Windows.Forms.ToolStrip Facturas_TS;
		protected System.Windows.Forms.ToolStripButton AddFactura_TI;
		protected System.Windows.Forms.ToolStripButton EditFactura_TI;
		protected System.Windows.Forms.ToolStripButton ViewFactura_TI;
		protected System.Windows.Forms.ToolStripButton DeleteFactura_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		protected System.Windows.Forms.DataGridView Lineas_DGW;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		protected System.Windows.Forms.TextBox NoAsignado_TB;
		protected System.Windows.Forms.Button Tarjeta_BT;
		protected System.Windows.Forms.TextBox MedioPago_TB;
        public System.Windows.Forms.Button MedioPago_BT;
        private System.Windows.Forms.Panel Source_Panel;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionCol;
        private CalendarColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPagado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acumulado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asignado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObservacionesCol;
        private System.Windows.Forms.DataGridViewButtonColumn Vinculado;
        protected System.Windows.Forms.TextBox EstadoPago_TB;
        public System.Windows.Forms.Button EstadoPago_BT;
        private System.Windows.Forms.TextBox Codigo_TB;
        protected System.Windows.Forms.GroupBox Payment_GB;
		

    }
}
