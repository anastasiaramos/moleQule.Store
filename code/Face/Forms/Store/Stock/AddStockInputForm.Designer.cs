namespace moleQule.Face.Store
{
    partial class AddStockInputForm
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
            System.Windows.Forms.Label conceptoLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label kilosLabel;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label bultosLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStockInputForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Bultos_CkB = new System.Windows.Forms.CheckBox();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Tipo_CB = new System.Windows.Forms.ComboBox();
            this.Datos_Tipo = new System.Windows.Forms.BindingSource(this.components);
            this.Kilos_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Bultos_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Concepto_TB = new System.Windows.Forms.RichTextBox();
            this.fechaDateTimePicker = new moleQule.Face.Controls.mQDateTimePicker();
            this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.Datos_Partidas = new System.Windows.Forms.BindingSource(this.components);
            this.Partidas_DGW = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bultosInicialesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kilosInicialesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockBultosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockKilosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            conceptoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            kilosLabel = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            bultosLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Partidas_DGW)).BeginInit();
            this.SuspendLayout();
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(380, 8);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(470, 8);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.Partidas_DGW);
            this.Source_GB.Location = new System.Drawing.Point(11, 7);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(914, 360);
            this.Source_GB.Text = "Partidas";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.groupBox1);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(938, 574);
            this.PanelesV.SplitterDistance = 534;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(265, 41);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(938, 574);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(437, 338);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(437, 253);
            // 
            // conceptoLabel
            // 
            conceptoLabel.AutoSize = true;
            conceptoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            conceptoLabel.Location = new System.Drawing.Point(43, 34);
            conceptoLabel.Name = "conceptoLabel";
            conceptoLabel.Size = new System.Drawing.Size(57, 13);
            conceptoLabel.TabIndex = 18;
            conceptoLabel.Text = "Concepto:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(407, 79);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 16;
            fechaLabel.Text = "Fecha:";
            // 
            // kilosLabel
            // 
            kilosLabel.AutoSize = true;
            kilosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            kilosLabel.Location = new System.Drawing.Point(415, 105);
            kilosLabel.Name = "kilosLabel";
            kilosLabel.Size = new System.Drawing.Size(32, 13);
            kilosLabel.TabIndex = 14;
            kilosLabel.Text = "Kilos:";
            // 
            // observacionesLabel
            // 
            observacionesLabel.AutoSize = true;
            observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            observacionesLabel.Location = new System.Drawing.Point(18, 87);
            observacionesLabel.Name = "observacionesLabel";
            observacionesLabel.Size = new System.Drawing.Size(82, 13);
            observacionesLabel.TabIndex = 12;
            observacionesLabel.Text = "Observaciones:";
            // 
            // bultosLabel
            // 
            bultosLabel.AutoSize = true;
            bultosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bultosLabel.Location = new System.Drawing.Point(581, 105);
            bultosLabel.Name = "bultosLabel";
            bultosLabel.Size = new System.Drawing.Size(40, 13);
            bultosLabel.TabIndex = 10;
            bultosLabel.Text = "Bultos:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(416, 50);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 13);
            label1.TabIndex = 34;
            label1.Text = "Tipo:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Bultos_CkB);
            this.groupBox1.Controls.Add(this.Expediente_TB);
            this.groupBox1.Controls.Add(this.Expediente_BT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.Tipo_CB);
            this.groupBox1.Controls.Add(this.Kilos_NTB);
            this.groupBox1.Controls.Add(this.Bultos_NTB);
            this.groupBox1.Controls.Add(conceptoLabel);
            this.groupBox1.Controls.Add(this.Concepto_TB);
            this.groupBox1.Controls.Add(fechaLabel);
            this.groupBox1.Controls.Add(this.fechaDateTimePicker);
            this.groupBox1.Controls.Add(kilosLabel);
            this.groupBox1.Controls.Add(observacionesLabel);
            this.groupBox1.Controls.Add(this.observacionesRichTextBox);
            this.groupBox1.Controls.Add(bultosLabel);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 373);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // Bultos_CkB
            // 
            this.Bultos_CkB.AutoSize = true;
            this.Bultos_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Bultos_CkB.Location = new System.Drawing.Point(755, 102);
            this.Bultos_CkB.Name = "Bultos_CkB";
            this.Bultos_CkB.Size = new System.Drawing.Size(55, 17);
            this.Bultos_CkB.TabIndex = 38;
            this.Bultos_CkB.Text = "Bultos";
            this.Bultos_CkB.UseVisualStyleBackColor = true;
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.Location = new System.Drawing.Point(726, 46);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(140, 21);
            this.Expediente_TB.TabIndex = 35;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Enabled = false;
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(872, 45);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(25, 23);
            this.Expediente_BT.TabIndex = 36;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(654, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "Expedient:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Tipo_CB
            // 
            this.Tipo_CB.DataSource = this.Datos_Tipo;
            this.Tipo_CB.DisplayMember = "Texto";
            this.Tipo_CB.FormattingEnabled = true;
            this.Tipo_CB.Location = new System.Drawing.Point(453, 46);
            this.Tipo_CB.Name = "Tipo_CB";
            this.Tipo_CB.Size = new System.Drawing.Size(183, 21);
            this.Tipo_CB.TabIndex = 33;
            this.Tipo_CB.ValueMember = "Oid";
            this.Tipo_CB.SelectedIndexChanged += new System.EventHandler(this.Tipo_CB_SelectedIndexChanged);
            // 
            // Datos_Tipo
            // 
            this.Datos_Tipo.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // Kilos_NTB
            // 
            this.Kilos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Kilos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Kilos_NTB.Location = new System.Drawing.Point(453, 102);
            this.Kilos_NTB.Name = "Kilos_NTB";
            this.Kilos_NTB.Size = new System.Drawing.Size(93, 21);
            this.Kilos_NTB.TabIndex = 3;
            this.Kilos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Kilos_NTB.TextIsCurrency = false;
            this.Kilos_NTB.TextIsInteger = false;
            this.Kilos_NTB.Validated += new System.EventHandler(this.Kilos_NTB_Validated);
            // 
            // Bultos_NTB
            // 
            this.Bultos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Bultos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Bultos_NTB.Location = new System.Drawing.Point(627, 102);
            this.Bultos_NTB.Name = "Bultos_NTB";
            this.Bultos_NTB.Size = new System.Drawing.Size(93, 21);
            this.Bultos_NTB.TabIndex = 2;
            this.Bultos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Bultos_NTB.TextIsCurrency = false;
            this.Bultos_NTB.TextIsInteger = true;
            this.Bultos_NTB.Validated += new System.EventHandler(this.Bultos_NTB_Validated);
            // 
            // Concepto_TB
            // 
            this.Concepto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Concepto", true));
            this.Concepto_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Concepto_TB.Location = new System.Drawing.Point(106, 31);
            this.Concepto_TB.Name = "Concepto_TB";
            this.Concepto_TB.Size = new System.Drawing.Size(287, 50);
            this.Concepto_TB.TabIndex = 0;
            this.Concepto_TB.Text = "";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Checked = false;
            this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
            this.fechaDateTimePicker.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(453, 73);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.ShowCheckBox = true;
            this.HelpProvider.SetShowHelp(this.fechaDateTimePicker, false);
            this.fechaDateTimePicker.Size = new System.Drawing.Size(120, 21);
            this.fechaDateTimePicker.TabIndex = 1;
            // 
            // observacionesRichTextBox
            // 
            this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.observacionesRichTextBox.Location = new System.Drawing.Point(106, 87);
            this.observacionesRichTextBox.Name = "observacionesRichTextBox";
            this.observacionesRichTextBox.Size = new System.Drawing.Size(287, 50);
            this.observacionesRichTextBox.TabIndex = 4;
            this.observacionesRichTextBox.Text = "";
            // 
            // Datos_Partidas
            // 
            this.Datos_Partidas.DataSource = typeof(moleQule.Library.Store.Batch);
            // 
            // Partidas_DGW
            // 
            this.Partidas_DGW.AllowUserToAddRows = false;
            this.Partidas_DGW.AllowUserToDeleteRows = false;
            this.Partidas_DGW.AutoGenerateColumns = false;
            this.Partidas_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Partidas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Producto,
            this.Proveedor,
            this.bultosInicialesDataGridViewTextBoxColumn,
            this.kilosInicialesDataGridViewTextBoxColumn,
            this.stockBultosDataGridViewTextBoxColumn,
            this.stockKilosDataGridViewTextBoxColumn,
            this.Observaciones});
            this.Partidas_DGW.DataSource = this.Datos_Partidas;
            this.Partidas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Partidas_DGW.Location = new System.Drawing.Point(3, 17);
            this.Partidas_DGW.Name = "Partidas_DGW";
            this.Partidas_DGW.ReadOnly = true;
            this.Partidas_DGW.RowHeadersWidth = 25;
            this.Partidas_DGW.Size = new System.Drawing.Size(908, 340);
            this.Partidas_DGW.TabIndex = 0;
            this.Partidas_DGW.SelectionChanged += new System.EventHandler(this.Partidas_DGW_SelectionChanged);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Codigo.DefaultCellStyle = dataGridViewCellStyle1;
            this.Codigo.HeaderText = "ID";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 45;
            // 
            // Producto
            // 
            this.Producto.DataPropertyName = "TipoMercancia";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Producto.DefaultCellStyle = dataGridViewCellStyle2;
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 150;
            // 
            // Proveedor
            // 
            this.Proveedor.DataPropertyName = "Proveedor";
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            this.Proveedor.Width = 150;
            // 
            // bultosInicialesDataGridViewTextBoxColumn
            // 
            this.bultosInicialesDataGridViewTextBoxColumn.DataPropertyName = "BultosIniciales";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.bultosInicialesDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.bultosInicialesDataGridViewTextBoxColumn.HeaderText = "Bultos Iniciales";
            this.bultosInicialesDataGridViewTextBoxColumn.Name = "bultosInicialesDataGridViewTextBoxColumn";
            this.bultosInicialesDataGridViewTextBoxColumn.ReadOnly = true;
            this.bultosInicialesDataGridViewTextBoxColumn.Width = 65;
            // 
            // kilosInicialesDataGridViewTextBoxColumn
            // 
            this.kilosInicialesDataGridViewTextBoxColumn.DataPropertyName = "KilosIniciales";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.kilosInicialesDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.kilosInicialesDataGridViewTextBoxColumn.HeaderText = "Kg Iniciales";
            this.kilosInicialesDataGridViewTextBoxColumn.Name = "kilosInicialesDataGridViewTextBoxColumn";
            this.kilosInicialesDataGridViewTextBoxColumn.ReadOnly = true;
            this.kilosInicialesDataGridViewTextBoxColumn.Width = 75;
            // 
            // stockBultosDataGridViewTextBoxColumn
            // 
            this.stockBultosDataGridViewTextBoxColumn.DataPropertyName = "StockBultos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.stockBultosDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.stockBultosDataGridViewTextBoxColumn.HeaderText = "Stock Bultos";
            this.stockBultosDataGridViewTextBoxColumn.Name = "stockBultosDataGridViewTextBoxColumn";
            this.stockBultosDataGridViewTextBoxColumn.ReadOnly = true;
            this.stockBultosDataGridViewTextBoxColumn.Width = 65;
            // 
            // stockKilosDataGridViewTextBoxColumn
            // 
            this.stockKilosDataGridViewTextBoxColumn.DataPropertyName = "StockKilos";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.stockKilosDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.stockKilosDataGridViewTextBoxColumn.HeaderText = "Stock Kg";
            this.stockKilosDataGridViewTextBoxColumn.Name = "stockKilosDataGridViewTextBoxColumn";
            this.stockKilosDataGridViewTextBoxColumn.ReadOnly = true;
            this.stockKilosDataGridViewTextBoxColumn.Width = 70;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Nombre";
            this.dataGridViewTextBoxColumn1.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Proveedor";
            this.dataGridViewTextBoxColumn2.HeaderText = "Proveedor";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "StockKilos";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn3.HeaderText = "Stock Kilos";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 75;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "StockBultos";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.HeaderText = "Stock Bultos";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 75;
            // 
            // AddStockInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(938, 574);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddStockInputForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Nuevo Movimiento de Stock";
            this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
            this.Controls.SetChildIndex(this.PanelesV, 0);
            this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
            this.Controls.SetChildIndex(this.Progress_PB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Partidas_DGW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Partidas_DGW;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox Concepto_TB;
        private System.Windows.Forms.RichTextBox observacionesRichTextBox;
        private System.Windows.Forms.BindingSource Datos_Partidas;
        private moleQule.Face.Controls.NumericTextBox Kilos_NTB;
        private moleQule.Face.Controls.NumericTextBox Bultos_NTB;
		private moleQule.Face.Controls.mQDateTimePicker fechaDateTimePicker;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.ComboBox Tipo_CB;
		private System.Windows.Forms.BindingSource Datos_Tipo;
		private System.Windows.Forms.TextBox Expediente_TB;
		private System.Windows.Forms.Button Expediente_BT;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox Bultos_CkB;
		private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
		private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
		private System.Windows.Forms.DataGridViewTextBoxColumn bultosInicialesDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn kilosInicialesDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn stockBultosDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn stockKilosDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;

    }
}
