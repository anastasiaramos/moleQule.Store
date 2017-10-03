namespace moleQule.Face.Store
{
    partial class PaymentsControlActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentsControlActionForm));
            this.Acreedores_TB = new System.Windows.Forms.TextBox();
            this.Detalle_BT = new System.Windows.Forms.Button();
            this.TipoFactura_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TiposFactura = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FFacturaIni_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.FFacturaFin_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.TipoAcreedor_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TiposAcreedor = new System.Windows.Forms.BindingSource(this.components);
            this.FPagoFin_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.FPagoIni_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Resumido_RB = new System.Windows.Forms.RadioButton();
            this.Detallado_RB = new System.Windows.Forms.RadioButton();
            this.Datos_TiposExp = new System.Windows.Forms.BindingSource(this.components);
            this.FPrevisionFin_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.FPrevisionIni_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.FechaFactura_GB = new System.Windows.Forms.GroupBox();
            this.FechaPago_GB = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Proveedor_GB = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TipoExpediente_CB = new System.Windows.Forms.ComboBox();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.TodosExpediente_CkB = new System.Windows.Forms.CheckBox();
            this.Source_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposFactura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).BeginInit();
            this.FechaFactura_GB.SuspendLayout();
            this.FechaPago_GB.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Cliente_GB.SuspendLayout();
            this.Proveedor_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // Print_BT
            // 
            this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Print_BT.Location = new System.Drawing.Point(383, 2);
            this.HelpProvider.SetShowHelp(this.Print_BT, true);
            this.Print_BT.Size = new System.Drawing.Size(111, 32);
            this.Print_BT.Text = "Vista &Previa";
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(266, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Size = new System.Drawing.Size(111, 32);
            this.Submit_BT.Visible = false;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(149, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.Size = new System.Drawing.Size(111, 32);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.Proveedor_GB);
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Controls.Add(this.groupBox3);
            this.Source_GB.Controls.Add(this.FechaPago_GB);
            this.Source_GB.Controls.Add(this.FechaFactura_GB);
            this.Source_GB.Controls.Add(this.groupBox1);
            this.Source_GB.Controls.Add(this.label2);
            this.Source_GB.Controls.Add(this.TipoFactura_CB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(643, 459);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(645, 501);
            this.PanelesV.SplitterDistance = 461;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(118, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(645, 501);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(290, 302);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(290, 217);
            // 
            // Acreedores_TB
            // 
            this.Acreedores_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Acreedores_TB.Location = new System.Drawing.Point(150, 56);
            this.Acreedores_TB.Name = "Acreedores_TB";
            this.Acreedores_TB.ReadOnly = true;
            this.Acreedores_TB.Size = new System.Drawing.Size(287, 21);
            this.Acreedores_TB.TabIndex = 0;
            // 
            // Detalle_BT
            // 
            this.Detalle_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Detalle_BT.Location = new System.Drawing.Point(443, 54);
            this.Detalle_BT.Name = "Detalle_BT";
            this.Detalle_BT.Size = new System.Drawing.Size(42, 23);
            this.Detalle_BT.TabIndex = 1;
            this.Detalle_BT.UseVisualStyleBackColor = true;
            this.Detalle_BT.Click += new System.EventHandler(this.Detalle_BT_Click);
            // 
            // TipoFactura_CB
            // 
            this.TipoFactura_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos_TiposFactura, "Oid", true));
            this.TipoFactura_CB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_TiposFactura, "Texto", true));
            this.TipoFactura_CB.DataSource = this.Datos_TiposFactura;
            this.TipoFactura_CB.DisplayMember = "Texto";
            this.TipoFactura_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoFactura_CB.FormattingEnabled = true;
            this.TipoFactura_CB.Location = new System.Drawing.Point(161, 397);
            this.TipoFactura_CB.Name = "TipoFactura_CB";
            this.TipoFactura_CB.Size = new System.Drawing.Size(234, 21);
            this.TipoFactura_CB.TabIndex = 3;
            this.TipoFactura_CB.ValueMember = "Oid";
            // 
            // Datos_TiposFactura
            // 
            this.Datos_TiposFactura.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(107, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mostrar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(323, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Fecha Final:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fecha Inicial:";
            // 
            // FFacturaIni_DTP
            // 
            this.FFacturaIni_DTP.Checked = false;
            this.FFacturaIni_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFacturaIni_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFacturaIni_DTP.Location = new System.Drawing.Point(144, 17);
            this.FFacturaIni_DTP.Name = "FFacturaIni_DTP";
            this.FFacturaIni_DTP.ShowCheckBox = true;
            this.FFacturaIni_DTP.Size = new System.Drawing.Size(106, 21);
            this.FFacturaIni_DTP.TabIndex = 10;
            // 
            // FFacturaFin_DTP
            // 
            this.FFacturaFin_DTP.Checked = false;
            this.FFacturaFin_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFacturaFin_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFacturaFin_DTP.Location = new System.Drawing.Point(394, 17);
            this.FFacturaFin_DTP.Name = "FFacturaFin_DTP";
            this.FFacturaFin_DTP.ShowCheckBox = true;
            this.FFacturaFin_DTP.Size = new System.Drawing.Size(106, 21);
            this.FFacturaFin_DTP.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(113, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tipo:";
            // 
            // TipoAcreedor_CB
            // 
            this.TipoAcreedor_CB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_TiposAcreedor, "Texto", true));
            this.TipoAcreedor_CB.DataSource = this.Datos_TiposAcreedor;
            this.TipoAcreedor_CB.DisplayMember = "Texto";
            this.TipoAcreedor_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoAcreedor_CB.FormattingEnabled = true;
            this.TipoAcreedor_CB.Location = new System.Drawing.Point(150, 20);
            this.TipoAcreedor_CB.Name = "TipoAcreedor_CB";
            this.TipoAcreedor_CB.Size = new System.Drawing.Size(287, 21);
            this.TipoAcreedor_CB.TabIndex = 12;
            this.TipoAcreedor_CB.ValueMember = "Oid";
            this.TipoAcreedor_CB.SelectedIndexChanged += new System.EventHandler(this.TipoAcreedor_CB_SelectedIndexChanged);
            // 
            // Datos_TiposAcreedor
            // 
            this.Datos_TiposAcreedor.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // FPagoFin_DTP
            // 
            this.FPagoFin_DTP.Checked = false;
            this.FPagoFin_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPagoFin_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FPagoFin_DTP.Location = new System.Drawing.Point(394, 17);
            this.FPagoFin_DTP.Name = "FPagoFin_DTP";
            this.FPagoFin_DTP.ShowCheckBox = true;
            this.FPagoFin_DTP.Size = new System.Drawing.Size(106, 21);
            this.FPagoFin_DTP.TabIndex = 17;
            // 
            // FPagoIni_DTP
            // 
            this.FPagoIni_DTP.Checked = false;
            this.FPagoIni_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPagoIni_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FPagoIni_DTP.Location = new System.Drawing.Point(144, 17);
            this.FPagoIni_DTP.Name = "FPagoIni_DTP";
            this.FPagoIni_DTP.ShowCheckBox = true;
            this.FPagoIni_DTP.Size = new System.Drawing.Size(106, 21);
            this.FPagoIni_DTP.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(68, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fecha Inicial:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(323, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Fecha Final:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Resumido_RB);
            this.groupBox1.Controls.Add(this.Detallado_RB);
            this.groupBox1.Location = new System.Drawing.Point(430, 373);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 63);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // Resumido_RB
            // 
            this.Resumido_RB.AutoSize = true;
            this.Resumido_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resumido_RB.Location = new System.Drawing.Point(18, 38);
            this.Resumido_RB.Name = "Resumido_RB";
            this.Resumido_RB.Size = new System.Drawing.Size(71, 17);
            this.Resumido_RB.TabIndex = 1;
            this.Resumido_RB.Text = "Resumido";
            this.Resumido_RB.UseVisualStyleBackColor = true;
            // 
            // Detallado_RB
            // 
            this.Detallado_RB.AutoSize = true;
            this.Detallado_RB.Checked = true;
            this.Detallado_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detallado_RB.Location = new System.Drawing.Point(18, 15);
            this.Detallado_RB.Name = "Detallado_RB";
            this.Detallado_RB.Size = new System.Drawing.Size(70, 17);
            this.Detallado_RB.TabIndex = 0;
            this.Detallado_RB.TabStop = true;
            this.Detallado_RB.Text = "Detallado";
            this.Detallado_RB.UseVisualStyleBackColor = true;
            // 
            // Datos_TiposExp
            // 
            this.Datos_TiposExp.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // FPrevisionFin_DTP
            // 
            this.FPrevisionFin_DTP.Checked = false;
            this.FPrevisionFin_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPrevisionFin_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FPrevisionFin_DTP.Location = new System.Drawing.Point(394, 17);
            this.FPrevisionFin_DTP.Name = "FPrevisionFin_DTP";
            this.FPrevisionFin_DTP.ShowCheckBox = true;
            this.FPrevisionFin_DTP.Size = new System.Drawing.Size(106, 21);
            this.FPrevisionFin_DTP.TabIndex = 24;
            // 
            // FPrevisionIni_DTP
            // 
            this.FPrevisionIni_DTP.Checked = false;
            this.FPrevisionIni_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPrevisionIni_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FPrevisionIni_DTP.Location = new System.Drawing.Point(144, 17);
            this.FPrevisionIni_DTP.Name = "FPrevisionIni_DTP";
            this.FPrevisionIni_DTP.ShowCheckBox = true;
            this.FPrevisionIni_DTP.Size = new System.Drawing.Size(106, 21);
            this.FPrevisionIni_DTP.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(68, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Fecha Inicial:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(323, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Fecha Final:";
            // 
            // FechaFactura_GB
            // 
            this.FechaFactura_GB.Controls.Add(this.FFacturaIni_DTP);
            this.FechaFactura_GB.Controls.Add(this.label4);
            this.FechaFactura_GB.Controls.Add(this.FFacturaFin_DTP);
            this.FechaFactura_GB.Controls.Add(this.label3);
            this.FechaFactura_GB.Location = new System.Drawing.Point(37, 208);
            this.FechaFactura_GB.Name = "FechaFactura_GB";
            this.FechaFactura_GB.Size = new System.Drawing.Size(568, 49);
            this.FechaFactura_GB.TabIndex = 38;
            this.FechaFactura_GB.TabStop = false;
            this.FechaFactura_GB.Text = "Fecha Factura";
            // 
            // FechaPago_GB
            // 
            this.FechaPago_GB.Controls.Add(this.FPagoIni_DTP);
            this.FechaPago_GB.Controls.Add(this.label6);
            this.FechaPago_GB.Controls.Add(this.FPagoFin_DTP);
            this.FechaPago_GB.Controls.Add(this.label7);
            this.FechaPago_GB.Location = new System.Drawing.Point(37, 263);
            this.FechaPago_GB.Name = "FechaPago_GB";
            this.FechaPago_GB.Size = new System.Drawing.Size(568, 49);
            this.FechaPago_GB.TabIndex = 39;
            this.FechaPago_GB.TabStop = false;
            this.FechaPago_GB.Text = "Fecha Pago";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FPrevisionIni_DTP);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.FPrevisionFin_DTP);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(37, 318);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 49);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fecha Previsión";
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.label11);
            this.Cliente_GB.Controls.Add(this.Acreedores_TB);
            this.Cliente_GB.Controls.Add(this.Detalle_BT);
            this.Cliente_GB.Controls.Add(this.TipoAcreedor_CB);
            this.Cliente_GB.Controls.Add(this.label5);
            this.Cliente_GB.Location = new System.Drawing.Point(37, 112);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(568, 90);
            this.Cliente_GB.TabIndex = 41;
            this.Cliente_GB.TabStop = false;
            this.Cliente_GB.Text = "Acreedores";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(83, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "Selección:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Proveedor_GB
            // 
            this.Proveedor_GB.Controls.Add(this.label8);
            this.Proveedor_GB.Controls.Add(this.TipoExpediente_CB);
            this.Proveedor_GB.Controls.Add(this.Expediente_TB);
            this.Proveedor_GB.Controls.Add(this.Expediente_BT);
            this.Proveedor_GB.Controls.Add(this.label12);
            this.Proveedor_GB.Controls.Add(this.TodosExpediente_CkB);
            this.Proveedor_GB.Location = new System.Drawing.Point(37, 22);
            this.Proveedor_GB.Name = "Proveedor_GB";
            this.Proveedor_GB.Size = new System.Drawing.Size(568, 84);
            this.Proveedor_GB.TabIndex = 42;
            this.Proveedor_GB.TabStop = false;
            this.Proveedor_GB.Text = "Expedientes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(113, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Tipo de Expediente:";
            // 
            // TipoExpediente_CB
            // 
            this.TipoExpediente_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos_TiposExp, "Oid", true));
            this.TipoExpediente_CB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_TiposExp, "Texto", true));
            this.TipoExpediente_CB.DataSource = this.Datos_TiposExp;
            this.TipoExpediente_CB.DisplayMember = "Texto";
            this.TipoExpediente_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoExpediente_CB.FormattingEnabled = true;
            this.TipoExpediente_CB.Location = new System.Drawing.Point(222, 21);
            this.TipoExpediente_CB.Name = "TipoExpediente_CB";
            this.TipoExpediente_CB.Size = new System.Drawing.Size(234, 21);
            this.TipoExpediente_CB.TabIndex = 24;
            this.TipoExpediente_CB.ValueMember = "Oid";
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.Location = new System.Drawing.Point(79, 52);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(290, 21);
            this.Expediente_TB.TabIndex = 0;
            this.Expediente_TB.Visible = false;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Enabled = false;
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(375, 50);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(42, 23);
            this.Expediente_BT.TabIndex = 1;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Visible = false;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "Selección:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Visible = false;
            // 
            // TodosExpediente_CkB
            // 
            this.TodosExpediente_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosExpediente_CkB.AutoSize = true;
            this.TodosExpediente_CkB.Checked = true;
            this.TodosExpediente_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosExpediente_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosExpediente_CkB.Location = new System.Drawing.Point(443, 54);
            this.TodosExpediente_CkB.Name = "TodosExpediente_CkB";
            this.TodosExpediente_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosExpediente_CkB.TabIndex = 2;
            this.TodosExpediente_CkB.Text = "Mostrar Todos";
            this.TodosExpediente_CkB.UseVisualStyleBackColor = true;
            this.TodosExpediente_CkB.Visible = false;
            // 
            // ControlPagosActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(645, 501);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaymentsControlActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Control de Pagos";
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposFactura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).EndInit();
            this.FechaFactura_GB.ResumeLayout(false);
            this.FechaFactura_GB.PerformLayout();
            this.FechaPago_GB.ResumeLayout(false);
            this.FechaPago_GB.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            this.Proveedor_GB.ResumeLayout(false);
            this.Proveedor_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TipoFactura_CB;
        private System.Windows.Forms.Button Detalle_BT;
        private System.Windows.Forms.TextBox Acreedores_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private moleQule.Face.Controls.mQDateTimePicker FFacturaFin_DTP;
        private moleQule.Face.Controls.mQDateTimePicker FFacturaIni_DTP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TipoAcreedor_CB;
        private moleQule.Face.Controls.mQDateTimePicker FPagoFin_DTP;
        private moleQule.Face.Controls.mQDateTimePicker FPagoIni_DTP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Resumido_RB;
        private System.Windows.Forms.RadioButton Detallado_RB;
        private moleQule.Face.Controls.mQDateTimePicker FPrevisionFin_DTP;
        private moleQule.Face.Controls.mQDateTimePicker FPrevisionIni_DTP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox FechaFactura_GB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox FechaPago_GB;
        private System.Windows.Forms.GroupBox Cliente_GB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.BindingSource Datos_TiposExp;
        private System.Windows.Forms.BindingSource Datos_TiposAcreedor;
        private System.Windows.Forms.GroupBox Proveedor_GB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox TipoExpediente_CB;
        private System.Windows.Forms.TextBox Expediente_TB;
        private System.Windows.Forms.Button Expediente_BT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox TodosExpediente_CkB;
        private System.Windows.Forms.BindingSource Datos_TiposFactura;
    }
}
