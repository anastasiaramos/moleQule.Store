namespace moleQule.Face.Store
{
	partial class InputDeliveryLineUIForm
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
            System.Windows.Forms.Label cantidadLabel;
            System.Windows.Forms.Label conceptoLabel;
            System.Windows.Forms.Label precioLabel;
            System.Windows.Forms.Label subtotalLabel;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDeliveryLineUIForm));
            this.PanelExpedientes = new System.Windows.Forms.SplitContainer();
            this.ProductosPanel = new System.Windows.Forms.SplitContainer();
            this.Products_DGW = new System.Windows.Forms.DataGridView();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoFacturacionLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KilosBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Products_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Stock_GB = new System.Windows.Forms.GroupBox();
            this.Almacen_BT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Almacen_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Ubicacion_TB = new System.Windows.Forms.TextBox();
            this.ResetExpediente_BT = new System.Windows.Forms.Button();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Productos_BT = new System.Windows.Forms.Button();
            this.ConceptoAlbaranProveedor_GB = new System.Windows.Forms.GroupBox();
            this.SaleMethod_BT = new System.Windows.Forms.Button();
            this.SaleMethod_TB = new System.Windows.Forms.TextBox();
            this.Impuestos_BT = new System.Windows.Forms.Button();
            this.Pieces_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Detalles_GB = new System.Windows.Forms.GroupBox();
            this.Ayuda_TB = new System.Windows.Forms.TextBox();
            this.Gastos_TB = new System.Windows.Forms.TextBox();
            this.Detalles_BT = new System.Windows.Forms.Button();
            this.PrecioCliente_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.PrecioProducto_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Igic_Aplicado_LB = new System.Windows.Forms.Label();
            this.totalNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
            this.subtotalNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
            this.Precio_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Concepto_TB = new System.Windows.Forms.TextBox();
            this.Kilos_NTB = new moleQule.Face.Controls.NumericTextBox();
            cantidadLabel = new System.Windows.Forms.Label();
            conceptoLabel = new System.Windows.Forms.Label();
            precioLabel = new System.Windows.Forms.Label();
            subtotalLabel = new System.Windows.Forms.Label();
            totalLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelExpedientes)).BeginInit();
            this.PanelExpedientes.Panel2.SuspendLayout();
            this.PanelExpedientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductosPanel)).BeginInit();
            this.ProductosPanel.Panel1.SuspendLayout();
            this.ProductosPanel.Panel2.SuspendLayout();
            this.ProductosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Products_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Products_BS)).BeginInit();
            this.Stock_GB.SuspendLayout();
            this.ConceptoAlbaranProveedor_GB.SuspendLayout();
            this.Detalles_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.InputDeliveryLine);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(428, 8);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(544, 8);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Enabled = false;
            this.Source_GB.Location = new System.Drawing.Point(3, 3);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(95, 58);
            this.Source_GB.Text = "Expedients";
            this.Source_GB.Visible = false;
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.PanelExpedientes);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(1034, 606);
            this.PanelesV.SplitterDistance = 565;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(313, 41);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1034, 606);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(485, 354);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(485, 269);
            // 
            // cantidadLabel
            // 
            cantidadLabel.AutoSize = true;
            cantidadLabel.Location = new System.Drawing.Point(511, 19);
            cantidadLabel.Name = "cantidadLabel";
            cantidadLabel.Size = new System.Drawing.Size(28, 13);
            cantidadLabel.TabIndex = 0;
            cantidadLabel.Text = "Kgs:";
            // 
            // conceptoLabel
            // 
            conceptoLabel.AutoSize = true;
            conceptoLabel.Location = new System.Drawing.Point(45, 17);
            conceptoLabel.Name = "conceptoLabel";
            conceptoLabel.Size = new System.Drawing.Size(65, 13);
            conceptoLabel.TabIndex = 4;
            conceptoLabel.Text = "Descripción:";
            // 
            // precioLabel
            // 
            precioLabel.AutoSize = true;
            precioLabel.Location = new System.Drawing.Point(511, 59);
            precioLabel.Name = "precioLabel";
            precioLabel.Size = new System.Drawing.Size(80, 13);
            precioLabel.TabIndex = 10;
            precioLabel.Text = "Precio Compra:";
            // 
            // subtotalLabel
            // 
            subtotalLabel.AutoSize = true;
            subtotalLabel.Location = new System.Drawing.Point(514, 99);
            subtotalLabel.Name = "subtotalLabel";
            subtotalLabel.Size = new System.Drawing.Size(51, 13);
            subtotalLabel.TabIndex = 12;
            subtotalLabel.Text = "Subtotal:";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Location = new System.Drawing.Point(743, 100);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(35, 13);
            totalLabel.TabIndex = 14;
            totalLabel.Text = "Total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(624, 60);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(86, 13);
            label5.TabIndex = 27;
            label5.Text = "Precio Producto:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(739, 60);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(93, 13);
            label6.TabIndex = 29;
            label6.Text = "Precio Proveedor:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(174, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 13);
            label1.TabIndex = 28;
            label1.Text = "Coste:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(627, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(55, 13);
            label2.TabIndex = 33;
            label2.Text = "Unidades:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(65, 20);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(42, 13);
            label7.TabIndex = 30;
            label7.Text = "Ayuda:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label9.Location = new System.Drawing.Point(739, 19);
            label9.Name = "label9";
            this.HelpProvider.SetShowHelp(label9, true);
            label9.Size = new System.Drawing.Size(87, 13);
            label9.TabIndex = 143;
            label9.Text = "Forma de Venta:";
            // 
            // PanelExpedientes
            // 
            this.PanelExpedientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelExpedientes.Location = new System.Drawing.Point(0, 0);
            this.PanelExpedientes.Name = "PanelExpedientes";
            // 
            // PanelExpedientes.Panel1
            // 
            this.PanelExpedientes.Panel1.AutoScroll = true;
            this.PanelExpedientes.Panel1Collapsed = true;
            // 
            // PanelExpedientes.Panel2
            // 
            this.PanelExpedientes.Panel2.AutoScroll = true;
            this.PanelExpedientes.Panel2.Controls.Add(this.ProductosPanel);
            this.PanelExpedientes.Size = new System.Drawing.Size(1032, 563);
            this.PanelExpedientes.SplitterDistance = 377;
            this.PanelExpedientes.TabIndex = 2;
            // 
            // ProductosPanel
            // 
            this.ProductosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductosPanel.Location = new System.Drawing.Point(0, 0);
            this.ProductosPanel.Name = "ProductosPanel";
            this.ProductosPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ProductosPanel.Panel1
            // 
            this.ProductosPanel.Panel1.Controls.Add(this.Products_DGW);
            // 
            // ProductosPanel.Panel2
            // 
            this.ProductosPanel.Panel2.AutoScroll = true;
            this.ProductosPanel.Panel2.Controls.Add(this.Stock_GB);
            this.ProductosPanel.Panel2.Controls.Add(this.Productos_BT);
            this.ProductosPanel.Panel2.Controls.Add(this.ConceptoAlbaranProveedor_GB);
            this.ProductosPanel.Size = new System.Drawing.Size(1032, 563);
            this.ProductosPanel.SplitterDistance = 75;
            this.ProductosPanel.TabIndex = 3;
            // 
            // Products_DGW
            // 
            this.Products_DGW.AllowUserToAddRows = false;
            this.Products_DGW.AllowUserToDeleteRows = false;
            this.Products_DGW.AllowUserToResizeRows = false;
            this.Products_DGW.AutoGenerateColumns = false;
            this.Products_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.TipoFacturacionLabel,
            this.KilosBulto,
            this.observacionesDataGridViewTextBoxColumn});
            this.Products_DGW.DataSource = this.Products_BS;
            this.Products_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Products_DGW.Enabled = false;
            this.Products_DGW.Location = new System.Drawing.Point(0, 0);
            this.Products_DGW.MultiSelect = false;
            this.Products_DGW.Name = "Products_DGW";
            this.Products_DGW.ReadOnly = true;
            this.Products_DGW.RowHeadersVisible = false;
            this.Products_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Products_DGW.Size = new System.Drawing.Size(1032, 75);
            this.Products_DGW.TabIndex = 1;
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descripcionDataGridViewTextBoxColumn.Width = 500;
            // 
            // TipoFacturacionLabel
            // 
            this.TipoFacturacionLabel.DataPropertyName = "TipoFacturacionLabel";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TipoFacturacionLabel.DefaultCellStyle = dataGridViewCellStyle5;
            this.TipoFacturacionLabel.HeaderText = "Forma Venta";
            this.TipoFacturacionLabel.Name = "TipoFacturacionLabel";
            this.TipoFacturacionLabel.ReadOnly = true;
            // 
            // KilosBulto
            // 
            this.KilosBulto.DataPropertyName = "KilosBulto";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.KilosBulto.DefaultCellStyle = dataGridViewCellStyle6;
            this.KilosBulto.HeaderText = "Kg / Unidad";
            this.KilosBulto.Name = "KilosBulto";
            this.KilosBulto.ReadOnly = true;
            this.KilosBulto.Width = 75;
            // 
            // observacionesDataGridViewTextBoxColumn
            // 
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            this.observacionesDataGridViewTextBoxColumn.ReadOnly = true;
            this.observacionesDataGridViewTextBoxColumn.Width = 254;
            // 
            // Products_BS
            // 
            this.Products_BS.DataSource = typeof(moleQule.Library.Store.ProductInfo);
            this.Products_BS.CurrentChanged += new System.EventHandler(this.Products_BS_CurrentChanged);
            // 
            // Stock_GB
            // 
            this.Stock_GB.Controls.Add(this.Almacen_BT);
            this.Stock_GB.Controls.Add(this.label8);
            this.Stock_GB.Controls.Add(this.Almacen_TB);
            this.Stock_GB.Controls.Add(this.label3);
            this.Stock_GB.Controls.Add(this.Ubicacion_TB);
            this.Stock_GB.Controls.Add(this.ResetExpediente_BT);
            this.Stock_GB.Controls.Add(this.Expediente_BT);
            this.Stock_GB.Controls.Add(this.label4);
            this.Stock_GB.Controls.Add(this.Expediente_TB);
            this.Stock_GB.Location = new System.Drawing.Point(155, 331);
            this.Stock_GB.Name = "Stock_GB";
            this.Stock_GB.Size = new System.Drawing.Size(723, 115);
            this.Stock_GB.TabIndex = 4;
            this.Stock_GB.TabStop = false;
            this.Stock_GB.Text = "Stock";
            // 
            // Almacen_BT
            // 
            this.Almacen_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Almacen_BT.Location = new System.Drawing.Point(515, 21);
            this.Almacen_BT.Name = "Almacen_BT";
            this.Almacen_BT.Size = new System.Drawing.Size(28, 23);
            this.Almacen_BT.TabIndex = 174;
            this.Almacen_BT.UseVisualStyleBackColor = true;
            this.Almacen_BT.Click += new System.EventHandler(this.Almacen_BT_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(176, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 173;
            this.label8.Text = "Almacén:";
            // 
            // Almacen_TB
            // 
            this.Almacen_TB.Location = new System.Drawing.Point(233, 22);
            this.Almacen_TB.Name = "Almacen_TB";
            this.Almacen_TB.Size = new System.Drawing.Size(276, 21);
            this.Almacen_TB.TabIndex = 172;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 171;
            this.label3.Text = "Ubicación:";
            // 
            // Ubicacion_TB
            // 
            this.Ubicacion_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Ubicacion", true));
            this.Ubicacion_TB.Location = new System.Drawing.Point(233, 49);
            this.Ubicacion_TB.Name = "Ubicacion_TB";
            this.Ubicacion_TB.Size = new System.Drawing.Size(276, 21);
            this.Ubicacion_TB.TabIndex = 170;
            // 
            // ResetExpediente_BT
            // 
            this.ResetExpediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.ResetExpediente_BT.Location = new System.Drawing.Point(549, 75);
            this.ResetExpediente_BT.Name = "ResetExpediente_BT";
            this.ResetExpediente_BT.Size = new System.Drawing.Size(28, 23);
            this.ResetExpediente_BT.TabIndex = 169;
            this.ResetExpediente_BT.UseVisualStyleBackColor = true;
            this.ResetExpediente_BT.Click += new System.EventHandler(this.ResetExpediente_BT_Click);
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(515, 75);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(28, 23);
            this.Expediente_BT.TabIndex = 18;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Expediente:";
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Expediente", true));
            this.Expediente_TB.Location = new System.Drawing.Point(233, 76);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.Size = new System.Drawing.Size(276, 21);
            this.Expediente_TB.TabIndex = 0;
            // 
            // Productos_BT
            // 
            this.Productos_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos_BT.Location = new System.Drawing.Point(455, 7);
            this.Productos_BT.Name = "Productos_BT";
            this.Productos_BT.Size = new System.Drawing.Size(123, 40);
            this.Productos_BT.TabIndex = 3;
            this.Productos_BT.Text = "Productos";
            this.Productos_BT.UseVisualStyleBackColor = true;
            this.Productos_BT.Click += new System.EventHandler(this.Productos_BT_Click);
            // 
            // ConceptoAlbaranProveedor_GB
            // 
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.SaleMethod_BT);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(label9);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.SaleMethod_TB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Impuestos_BT);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(label2);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Pieces_NTB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Detalles_GB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Detalles_BT);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(label6);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.PrecioCliente_NTB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(label5);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.PrecioProducto_NTB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Igic_Aplicado_LB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(totalLabel);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.totalNumericTextBox);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(subtotalLabel);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.subtotalNumericTextBox);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(precioLabel);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Precio_NTB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(conceptoLabel);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Concepto_TB);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(cantidadLabel);
            this.ConceptoAlbaranProveedor_GB.Controls.Add(this.Kilos_NTB);
            this.ConceptoAlbaranProveedor_GB.Location = new System.Drawing.Point(67, 53);
            this.ConceptoAlbaranProveedor_GB.Name = "ConceptoAlbaranProveedor_GB";
            this.ConceptoAlbaranProveedor_GB.Size = new System.Drawing.Size(908, 271);
            this.ConceptoAlbaranProveedor_GB.TabIndex = 2;
            this.ConceptoAlbaranProveedor_GB.TabStop = false;
            this.ConceptoAlbaranProveedor_GB.Text = "Datos";
            // 
            // SaleMethod_BT
            // 
            this.SaleMethod_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.SaleMethod_BT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SaleMethod_BT.Location = new System.Drawing.Point(861, 33);
            this.SaleMethod_BT.Name = "SaleMethod_BT";
            this.HelpProvider.SetShowHelp(this.SaleMethod_BT, true);
            this.SaleMethod_BT.Size = new System.Drawing.Size(29, 22);
            this.SaleMethod_BT.TabIndex = 144;
            this.SaleMethod_BT.UseVisualStyleBackColor = true;
            this.SaleMethod_BT.Click += new System.EventHandler(this.SaleMethod_BT_Click);
            // 
            // SaleMethod_TB
            // 
            this.SaleMethod_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "SaleMethodLabel", true));
            this.SaleMethod_TB.Location = new System.Drawing.Point(742, 34);
            this.SaleMethod_TB.Name = "SaleMethod_TB";
            this.SaleMethod_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.SaleMethod_TB, true);
            this.SaleMethod_TB.Size = new System.Drawing.Size(113, 21);
            this.SaleMethod_TB.TabIndex = 142;
            // 
            // Impuestos_BT
            // 
            this.Impuestos_BT.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PImpuestos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Impuestos_BT.Location = new System.Drawing.Point(627, 116);
            this.Impuestos_BT.Name = "Impuestos_BT";
            this.Impuestos_BT.Size = new System.Drawing.Size(100, 23);
            this.Impuestos_BT.TabIndex = 34;
            this.Impuestos_BT.Tag = "NO FORMAT";
            this.Impuestos_BT.UseVisualStyleBackColor = true;
            this.Impuestos_BT.Click += new System.EventHandler(this.Impuestos_BT_Click);
            // 
            // Pieces_NTB
            // 
            this.Pieces_NTB.ForeColor = System.Drawing.Color.Navy;
            this.Pieces_NTB.Location = new System.Drawing.Point(627, 35);
            this.Pieces_NTB.Name = "Pieces_NTB";
            this.Pieces_NTB.Size = new System.Drawing.Size(100, 21);
            this.Pieces_NTB.TabIndex = 32;
            this.Pieces_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Pieces_NTB.TextIsCurrency = false;
            this.Pieces_NTB.TextIsInteger = false;
            this.Pieces_NTB.TextChanged += new System.EventHandler(this.Bultos_NTB_TextChanged);
            this.Pieces_NTB.Validated += new System.EventHandler(this.Bultos_NTB_Validated);
            // 
            // Detalles_GB
            // 
            this.Detalles_GB.Controls.Add(label7);
            this.Detalles_GB.Controls.Add(this.Ayuda_TB);
            this.Detalles_GB.Controls.Add(label1);
            this.Detalles_GB.Controls.Add(this.Gastos_TB);
            this.Detalles_GB.Location = new System.Drawing.Point(514, 176);
            this.Detalles_GB.Name = "Detalles_GB";
            this.Detalles_GB.Size = new System.Drawing.Size(343, 78);
            this.Detalles_GB.TabIndex = 31;
            this.Detalles_GB.TabStop = false;
            this.Detalles_GB.Text = "Detalles";
            // 
            // Ayuda_TB
            // 
            this.Ayuda_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "AyudaKilo", true));
            this.Ayuda_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ayuda_TB.Location = new System.Drawing.Point(68, 36);
            this.Ayuda_TB.Name = "Ayuda_TB";
            this.Ayuda_TB.Size = new System.Drawing.Size(100, 21);
            this.Ayuda_TB.TabIndex = 29;
            this.Ayuda_TB.Text = "0.00000";
            this.Ayuda_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Gastos_TB
            // 
            this.Gastos_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Gastos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
            this.Gastos_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gastos_TB.Location = new System.Drawing.Point(177, 37);
            this.Gastos_TB.Name = "Gastos_TB";
            this.Gastos_TB.ReadOnly = true;
            this.Gastos_TB.Size = new System.Drawing.Size(100, 21);
            this.Gastos_TB.TabIndex = 27;
            this.Gastos_TB.Text = "0.00000";
            this.Gastos_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Detalles_BT
            // 
            this.Detalles_BT.Location = new System.Drawing.Point(627, 144);
            this.Detalles_BT.Name = "Detalles_BT";
            this.Detalles_BT.Size = new System.Drawing.Size(100, 23);
            this.Detalles_BT.TabIndex = 30;
            this.Detalles_BT.Text = "Detalles";
            this.Detalles_BT.UseVisualStyleBackColor = true;
            this.Detalles_BT.Click += new System.EventHandler(this.Detalles_BT_Click);
            // 
            // PrecioCliente_NTB
            // 
            this.PrecioCliente_NTB.Location = new System.Drawing.Point(742, 76);
            this.PrecioCliente_NTB.Name = "PrecioCliente_NTB";
            this.PrecioCliente_NTB.ReadOnly = true;
            this.PrecioCliente_NTB.Size = new System.Drawing.Size(100, 21);
            this.PrecioCliente_NTB.TabIndex = 28;
            this.PrecioCliente_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioCliente_NTB.TextIsCurrency = false;
            this.PrecioCliente_NTB.TextIsInteger = false;
            // 
            // PrecioProducto_NTB
            // 
            this.PrecioProducto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Products_BS, "PrecioCompra", true));
            this.PrecioProducto_NTB.Location = new System.Drawing.Point(627, 76);
            this.PrecioProducto_NTB.Name = "PrecioProducto_NTB";
            this.PrecioProducto_NTB.ReadOnly = true;
            this.PrecioProducto_NTB.Size = new System.Drawing.Size(100, 21);
            this.PrecioProducto_NTB.TabIndex = 26;
            this.PrecioProducto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioProducto_NTB.TextIsCurrency = false;
            this.PrecioProducto_NTB.TextIsInteger = false;
            // 
            // Igic_Aplicado_LB
            // 
            this.Igic_Aplicado_LB.AutoSize = true;
            this.Igic_Aplicado_LB.Location = new System.Drawing.Point(624, 100);
            this.Igic_Aplicado_LB.Name = "Igic_Aplicado_LB";
            this.Igic_Aplicado_LB.Size = new System.Drawing.Size(68, 13);
            this.Igic_Aplicado_LB.TabIndex = 20;
            this.Igic_Aplicado_LB.Text = "% IVA/IGIC:";
            // 
            // totalNumericTextBox
            // 
            this.totalNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.totalNumericTextBox.Location = new System.Drawing.Point(743, 115);
            this.totalNumericTextBox.Name = "totalNumericTextBox";
            this.totalNumericTextBox.ReadOnly = true;
            this.totalNumericTextBox.Size = new System.Drawing.Size(100, 21);
            this.totalNumericTextBox.TabIndex = 5;
            this.totalNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.totalNumericTextBox.TextIsCurrency = false;
            this.totalNumericTextBox.TextIsInteger = false;
            // 
            // subtotalNumericTextBox
            // 
            this.subtotalNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Subtotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.subtotalNumericTextBox.Location = new System.Drawing.Point(514, 114);
            this.subtotalNumericTextBox.Name = "subtotalNumericTextBox";
            this.subtotalNumericTextBox.ReadOnly = true;
            this.subtotalNumericTextBox.Size = new System.Drawing.Size(100, 21);
            this.subtotalNumericTextBox.TabIndex = 4;
            this.subtotalNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtotalNumericTextBox.TextIsCurrency = false;
            this.subtotalNumericTextBox.TextIsInteger = false;
            // 
            // Precio_NTB
            // 
            this.Precio_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N5"));
            this.Precio_NTB.Location = new System.Drawing.Point(514, 75);
            this.Precio_NTB.Name = "Precio_NTB";
            this.Precio_NTB.Size = new System.Drawing.Size(100, 21);
            this.Precio_NTB.TabIndex = 3;
            this.Precio_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Precio_NTB.TextIsCurrency = false;
            this.Precio_NTB.TextIsInteger = false;
            // 
            // Concepto_TB
            // 
            this.Concepto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Concepto", true));
            this.Concepto_TB.ForeColor = System.Drawing.Color.Navy;
            this.Concepto_TB.Location = new System.Drawing.Point(48, 33);
            this.Concepto_TB.Multiline = true;
            this.Concepto_TB.Name = "Concepto_TB";
            this.Concepto_TB.Size = new System.Drawing.Size(440, 219);
            this.Concepto_TB.TabIndex = 0;
            // 
            // Kilos_NTB
            // 
            this.Kilos_NTB.ForeColor = System.Drawing.Color.Navy;
            this.Kilos_NTB.Location = new System.Drawing.Point(514, 35);
            this.Kilos_NTB.Name = "Kilos_NTB";
            this.Kilos_NTB.Size = new System.Drawing.Size(100, 21);
            this.Kilos_NTB.TabIndex = 1;
            this.Kilos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Kilos_NTB.TextIsCurrency = false;
            this.Kilos_NTB.TextIsInteger = false;
            this.Kilos_NTB.TextChanged += new System.EventHandler(this.Kilos_NTB_TextChanged);
            this.Kilos_NTB.Validated += new System.EventHandler(this.Kilos_NTB_Validated);
            // 
            // InputDeliveryLineUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 606);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InputDeliveryLineUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Concepto de Albarán de Compra";
            this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
            this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
            this.Controls.SetChildIndex(this.Progress_PB, 0);
            this.Controls.SetChildIndex(this.PanelesV, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
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
            this.PanelExpedientes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelExpedientes)).EndInit();
            this.PanelExpedientes.ResumeLayout(false);
            this.ProductosPanel.Panel1.ResumeLayout(false);
            this.ProductosPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductosPanel)).EndInit();
            this.ProductosPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Products_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Products_BS)).EndInit();
            this.Stock_GB.ResumeLayout(false);
            this.Stock_GB.PerformLayout();
            this.ConceptoAlbaranProveedor_GB.ResumeLayout(false);
            this.ConceptoAlbaranProveedor_GB.PerformLayout();
            this.Detalles_GB.ResumeLayout(false);
            this.Detalles_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private moleQule.Face.Controls.NumericTextBox subtotalNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox Precio_NTB;
        private System.Windows.Forms.TextBox Concepto_TB;
        private moleQule.Face.Controls.NumericTextBox totalNumericTextBox;
		private System.Windows.Forms.Label Igic_Aplicado_LB;
        private moleQule.Face.Controls.NumericTextBox PrecioProducto_NTB;
        private System.Windows.Forms.Button Detalles_BT;
        private System.Windows.Forms.GroupBox Detalles_GB;
		private System.Windows.Forms.TextBox Gastos_TB;
        protected System.Windows.Forms.SplitContainer PanelExpedientes;
        protected System.Windows.Forms.SplitContainer ProductosPanel;
        protected System.Windows.Forms.GroupBox ConceptoAlbaranProveedor_GB;
        protected System.Windows.Forms.BindingSource Products_BS;
        protected moleQule.Face.Controls.NumericTextBox PrecioCliente_NTB;
        protected System.Windows.Forms.DataGridView Products_DGW;
        protected System.Windows.Forms.Button Productos_BT;
        protected moleQule.Face.Controls.NumericTextBox Kilos_NTB;
		protected Controls.NumericTextBox Pieces_NTB;
		private System.Windows.Forms.GroupBox Stock_GB;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button Expediente_BT;
		private System.Windows.Forms.TextBox Ayuda_TB;
		private System.Windows.Forms.Button Impuestos_BT;
		protected System.Windows.Forms.Button ResetExpediente_BT;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Ubicacion_TB;
		private System.Windows.Forms.Button Almacen_BT;
		private System.Windows.Forms.Label label8;
		protected System.Windows.Forms.TextBox Almacen_TB;
        protected System.Windows.Forms.TextBox Expediente_TB;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoFacturacionLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosBulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
        protected System.Windows.Forms.Button SaleMethod_BT;
        protected System.Windows.Forms.TextBox SaleMethod_TB;


    }
}
