namespace moleQule.Face.Store
{
    partial class BatchNewForm
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
			System.Windows.Forms.Label kilosInicialesLabel;
			System.Windows.Forms.Label bultosInicialesLabel;
			System.Windows.Forms.Label tipoMercanciaLabel;
			System.Windows.Forms.Label ayudaRecibidaKiloLabel;
			System.Windows.Forms.Label costeKiloLabel;
			System.Windows.Forms.Label precioKiloLabel;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label beneficioKiloLabel;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.PanelesH = new System.Windows.Forms.SplitContainer();
			this.ProductoTabla = new System.Windows.Forms.DataGridView();
			this.ColumnaProductoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PrecioProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnaProductoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bultoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Datos_ProductoProveedor = new System.Windows.Forms.BindingSource(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PVDBulto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PVPBulto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.BeneficioTotal_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.beneficioKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.CosteBulto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PVPKilo_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.ayudaRecibidaKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.PVDKilo_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.costeKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.Partida_GB = new System.Windows.Forms.GroupBox();
			this.numericTextBox1 = new moleQule.Face.Controls.NumericTextBox();
			this.KilosIniciales_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.BultosIniciales_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.tipoMercanciaTextBox = new System.Windows.Forms.TextBox();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			kilosInicialesLabel = new System.Windows.Forms.Label();
			bultosInicialesLabel = new System.Windows.Forms.Label();
			tipoMercanciaLabel = new System.Windows.Forms.Label();
			ayudaRecibidaKiloLabel = new System.Windows.Forms.Label();
			costeKiloLabel = new System.Windows.Forms.Label();
			precioKiloLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			beneficioKiloLabel = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.PanelesH.Panel1.SuspendLayout();
			this.PanelesH.Panel2.SuspendLayout();
			this.PanelesH.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProductoTabla)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_ProductoProveedor)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.Partida_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// Datos
			// 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Batch);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(453, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(543, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Enabled = false;
			this.Source_GB.Location = new System.Drawing.Point(3, 3);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(42, 32);
			this.Source_GB.Text = "Accion";
			this.Source_GB.Visible = false;
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.PanelesH);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(1084, 513);
			this.PanelesV.SplitterDistance = 473;
			// 
			// ProgressBK_Panel
			// 
			// 
			// kilosInicialesLabel
			// 
			kilosInicialesLabel.AutoSize = true;
			kilosInicialesLabel.Location = new System.Drawing.Point(114, 91);
			kilosInicialesLabel.Name = "kilosInicialesLabel";
			kilosInicialesLabel.Size = new System.Drawing.Size(64, 13);
			kilosInicialesLabel.TabIndex = 4;
			kilosInicialesLabel.Text = "Kg Iniciales:";
			// 
			// bultosInicialesLabel
			// 
			bultosInicialesLabel.AutoSize = true;
			bultosInicialesLabel.Location = new System.Drawing.Point(12, 91);
			bultosInicialesLabel.Name = "bultosInicialesLabel";
			bultosInicialesLabel.Size = new System.Drawing.Size(81, 13);
			bultosInicialesLabel.TabIndex = 2;
			bultosInicialesLabel.Text = "Bultos Iniciales:";
			// 
			// tipoMercanciaLabel
			// 
			tipoMercanciaLabel.AutoSize = true;
			tipoMercanciaLabel.Location = new System.Drawing.Point(12, 48);
			tipoMercanciaLabel.Name = "tipoMercanciaLabel";
			tipoMercanciaLabel.Size = new System.Drawing.Size(82, 13);
			tipoMercanciaLabel.TabIndex = 0;
			tipoMercanciaLabel.Text = "Tipo Mercancía:";
			// 
			// ayudaRecibidaKiloLabel
			// 
			ayudaRecibidaKiloLabel.AutoSize = true;
			ayudaRecibidaKiloLabel.Location = new System.Drawing.Point(168, 102);
			ayudaRecibidaKiloLabel.Name = "ayudaRecibidaKiloLabel";
			ayudaRecibidaKiloLabel.Size = new System.Drawing.Size(100, 13);
			ayudaRecibidaKiloLabel.TabIndex = 34;
			ayudaRecibidaKiloLabel.Text = "Ayuda Recibida Kg:";
			// 
			// costeKiloLabel
			// 
			costeKiloLabel.AutoSize = true;
			costeKiloLabel.Location = new System.Drawing.Point(168, 20);
			costeKiloLabel.Name = "costeKiloLabel";
			costeKiloLabel.Size = new System.Drawing.Size(54, 13);
			costeKiloLabel.TabIndex = 31;
			costeKiloLabel.Text = "Coste Kg:";
			// 
			// precioKiloLabel
			// 
			precioKiloLabel.AutoSize = true;
			precioKiloLabel.Location = new System.Drawing.Point(29, 20);
			precioKiloLabel.Name = "precioKiloLabel";
			precioKiloLabel.Size = new System.Drawing.Size(58, 13);
			precioKiloLabel.TabIndex = 30;
			precioKiloLabel.Text = "PC por Kg:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(30, 144);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(75, 13);
			label1.TabIndex = 37;
			label1.Text = "PVP por Bulto:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(171, 61);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(66, 13);
			label2.TabIndex = 39;
			label2.Text = "Coste Bulto:";
			// 
			// beneficioKiloLabel
			// 
			beneficioKiloLabel.AutoSize = true;
			beneficioKiloLabel.Location = new System.Drawing.Point(169, 144);
			beneficioKiloLabel.Name = "beneficioKiloLabel";
			beneficioKiloLabel.Size = new System.Drawing.Size(69, 13);
			beneficioKiloLabel.TabIndex = 41;
			beneficioKiloLabel.Text = "Beneficio Kg:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(97, 189);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(127, 13);
			label3.TabIndex = 44;
			label3.Text = "Beneficio Total Estimado:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(30, 103);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(63, 13);
			label4.TabIndex = 46;
			label4.Text = "PVP por Kg:";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(29, 59);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(70, 13);
			label5.TabIndex = 48;
			label5.Text = "PC por Bulto:";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(210, 91);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(57, 13);
			label6.TabIndex = 6;
			label6.Text = "Kg / Bulto:";
			// 
			// PanelesH
			// 
			this.PanelesH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelesH.Location = new System.Drawing.Point(0, 0);
			this.PanelesH.Name = "PanelesH";
			// 
			// PanelesH.Panel1
			// 
			this.PanelesH.Panel1.AutoScroll = true;
			this.PanelesH.Panel1.Controls.Add(this.ProductoTabla);
			// 
			// PanelesH.Panel2
			// 
			this.PanelesH.Panel2.AutoScroll = true;
			this.PanelesH.Panel2.Controls.Add(this.groupBox1);
			this.PanelesH.Panel2.Controls.Add(this.Partida_GB);
			this.PanelesH.Size = new System.Drawing.Size(1082, 471);
			this.PanelesH.SplitterDistance = 739;
			this.PanelesH.TabIndex = 2;
			// 
			// ProductoTabla
			// 
			this.ProductoTabla.AllowUserToAddRows = false;
			this.ProductoTabla.AllowUserToDeleteRows = false;
			this.ProductoTabla.AllowUserToResizeRows = false;
			this.ProductoTabla.AutoGenerateColumns = false;
			this.ProductoTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnaProductoNombre,
            this.PrecioProducto,
            this.ColumnaProductoPrecio,
            this.bultoDataGridViewCheckBoxColumn});
			this.ProductoTabla.DataSource = this.Datos_ProductoProveedor;
			this.ProductoTabla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProductoTabla.Location = new System.Drawing.Point(0, 0);
			this.ProductoTabla.Name = "ProductoTabla";
			this.ProductoTabla.ReadOnly = true;
			this.ProductoTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ProductoTabla.Size = new System.Drawing.Size(739, 471);
			this.ProductoTabla.TabIndex = 0;
			// 
			// ColumnaProductoNombre
			// 
			this.ColumnaProductoNombre.DataPropertyName = "Nombre";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.ColumnaProductoNombre.DefaultCellStyle = dataGridViewCellStyle1;
			this.ColumnaProductoNombre.HeaderText = "Producto";
			this.ColumnaProductoNombre.Name = "ColumnaProductoNombre";
			this.ColumnaProductoNombre.ReadOnly = true;
			// 
			// PrecioProducto
			// 
			this.PrecioProducto.DataPropertyName = "PrecioCompra";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.Format = "N5";
			dataGridViewCellStyle2.NullValue = null;
			this.PrecioProducto.DefaultCellStyle = dataGridViewCellStyle2;
			this.PrecioProducto.HeaderText = "PC Kg Producto";
			this.PrecioProducto.Name = "PrecioProducto";
			this.PrecioProducto.ReadOnly = true;
			this.PrecioProducto.Width = 75;
			// 
			// ColumnaProductoPrecio
			// 
			this.ColumnaProductoPrecio.DataPropertyName = "Precio";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle3.Format = "C5";
			dataGridViewCellStyle3.NullValue = null;
			this.ColumnaProductoPrecio.DefaultCellStyle = dataGridViewCellStyle3;
			this.ColumnaProductoPrecio.HeaderText = "PC Kg Proveedor";
			this.ColumnaProductoPrecio.Name = "ColumnaProductoPrecio";
			this.ColumnaProductoPrecio.ReadOnly = true;
			this.ColumnaProductoPrecio.Width = 75;
			// 
			// bultoDataGridViewCheckBoxColumn
			// 
			this.bultoDataGridViewCheckBoxColumn.DataPropertyName = "Bulto";
			this.bultoDataGridViewCheckBoxColumn.HeaderText = "Bulto";
			this.bultoDataGridViewCheckBoxColumn.Name = "bultoDataGridViewCheckBoxColumn";
			this.bultoDataGridViewCheckBoxColumn.ReadOnly = true;
			this.bultoDataGridViewCheckBoxColumn.Width = 50;
			// 
			// Datos_ProductoProveedor
			// 
			this.Datos_ProductoProveedor.DataSource = typeof(moleQule.Library.Store.ProductoProveedorInfo);
			this.Datos_ProductoProveedor.CurrentChanged += new System.EventHandler(this.Datos_ProductoProveedor_CurrentChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.PVDBulto_NTB);
			this.groupBox1.Controls.Add(label5);
			this.groupBox1.Controls.Add(this.PVPBulto_NTB);
			this.groupBox1.Controls.Add(label4);
			this.groupBox1.Controls.Add(label3);
			this.groupBox1.Controls.Add(this.BeneficioTotal_NTB);
			this.groupBox1.Controls.Add(beneficioKiloLabel);
			this.groupBox1.Controls.Add(this.beneficioKiloNumericTextBox);
			this.groupBox1.Controls.Add(this.CosteBulto_NTB);
			this.groupBox1.Controls.Add(this.PVPKilo_NTB);
			this.groupBox1.Controls.Add(label2);
			this.groupBox1.Controls.Add(label1);
			this.groupBox1.Controls.Add(this.ayudaRecibidaKiloNumericTextBox);
			this.groupBox1.Controls.Add(ayudaRecibidaKiloLabel);
			this.groupBox1.Controls.Add(this.PVDKilo_NTB);
			this.groupBox1.Controls.Add(this.costeKiloNumericTextBox);
			this.groupBox1.Controls.Add(costeKiloLabel);
			this.groupBox1.Controls.Add(precioKiloLabel);
			this.groupBox1.Location = new System.Drawing.Point(15, 213);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(313, 241);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Información de Precio y Coste";
			// 
			// PVDBulto_NTB
			// 
			this.PVDBulto_NTB.BackColor = System.Drawing.SystemColors.Control;
			this.PVDBulto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioCompraBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.PVDBulto_NTB.Location = new System.Drawing.Point(32, 76);
			this.PVDBulto_NTB.Name = "PVDBulto_NTB";
			this.PVDBulto_NTB.ReadOnly = true;
			this.PVDBulto_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVDBulto_NTB.TabIndex = 47;
			this.PVDBulto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVDBulto_NTB.TextIsCurrency = false;
			this.PVDBulto_NTB.TextIsInteger = false;
			// 
			// PVPBulto_NTB
			// 
			this.PVPBulto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioVentaBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.PVPBulto_NTB.Location = new System.Drawing.Point(32, 161);
			this.PVPBulto_NTB.Name = "PVPBulto_NTB";
			this.PVPBulto_NTB.ReadOnly = true;
			this.PVPBulto_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVPBulto_NTB.TabIndex = 45;
			this.PVPBulto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVPBulto_NTB.TextIsCurrency = false;
			this.PVPBulto_NTB.TextIsInteger = false;
			// 
			// BeneficioTotal_NTB
			// 
			this.BeneficioTotal_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioTotalEstimado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.BeneficioTotal_NTB.Location = new System.Drawing.Point(100, 205);
			this.BeneficioTotal_NTB.Name = "BeneficioTotal_NTB";
			this.BeneficioTotal_NTB.ReadOnly = true;
			this.BeneficioTotal_NTB.Size = new System.Drawing.Size(112, 21);
			this.BeneficioTotal_NTB.TabIndex = 6;
			this.BeneficioTotal_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BeneficioTotal_NTB.TextIsCurrency = false;
			this.BeneficioTotal_NTB.TextIsInteger = false;
			// 
			// beneficioKiloNumericTextBox
			// 
			this.beneficioKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.beneficioKiloNumericTextBox.Location = new System.Drawing.Point(171, 161);
			this.beneficioKiloNumericTextBox.Name = "beneficioKiloNumericTextBox";
			this.beneficioKiloNumericTextBox.ReadOnly = true;
			this.beneficioKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.beneficioKiloNumericTextBox.TabIndex = 3;
			this.beneficioKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.beneficioKiloNumericTextBox.TextIsCurrency = false;
			this.beneficioKiloNumericTextBox.TextIsInteger = false;
			// 
			// CosteBulto_NTB
			// 
			this.CosteBulto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CosteBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.CosteBulto_NTB.Location = new System.Drawing.Point(171, 80);
			this.CosteBulto_NTB.Name = "CosteBulto_NTB";
			this.CosteBulto_NTB.ReadOnly = true;
			this.CosteBulto_NTB.Size = new System.Drawing.Size(112, 21);
			this.CosteBulto_NTB.TabIndex = 5;
			this.CosteBulto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CosteBulto_NTB.TextIsCurrency = false;
			this.CosteBulto_NTB.TextIsInteger = false;
			// 
			// PVPKilo_NTB
			// 
			this.PVPKilo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioVentaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.PVPKilo_NTB.Location = new System.Drawing.Point(32, 119);
			this.PVPKilo_NTB.Name = "PVPKilo_NTB";
			this.PVPKilo_NTB.ReadOnly = true;
			this.PVPKilo_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVPKilo_NTB.TabIndex = 1;
			this.PVPKilo_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVPKilo_NTB.TextIsCurrency = false;
			this.PVPKilo_NTB.TextIsInteger = false;
			// 
			// ayudaRecibidaKiloNumericTextBox
			// 
			this.ayudaRecibidaKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "AyudaRecibidaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.ayudaRecibidaKiloNumericTextBox.Location = new System.Drawing.Point(171, 119);
			this.ayudaRecibidaKiloNumericTextBox.Name = "ayudaRecibidaKiloNumericTextBox";
			this.ayudaRecibidaKiloNumericTextBox.ReadOnly = true;
			this.ayudaRecibidaKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.ayudaRecibidaKiloNumericTextBox.TabIndex = 2;
			this.ayudaRecibidaKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ayudaRecibidaKiloNumericTextBox.TextIsCurrency = false;
			this.ayudaRecibidaKiloNumericTextBox.TextIsInteger = false;
			// 
			// PVDKilo_NTB
			// 
			this.PVDKilo_NTB.BackColor = System.Drawing.Color.White;
			this.PVDKilo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioCompraKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.PVDKilo_NTB.Location = new System.Drawing.Point(32, 37);
			this.PVDKilo_NTB.Name = "PVDKilo_NTB";
			this.PVDKilo_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVDKilo_NTB.TabIndex = 0;
			this.PVDKilo_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVDKilo_NTB.TextIsCurrency = false;
			this.PVDKilo_NTB.TextIsInteger = false;
			// 
			// costeKiloNumericTextBox
			// 
			this.costeKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CosteKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.costeKiloNumericTextBox.Location = new System.Drawing.Point(171, 37);
			this.costeKiloNumericTextBox.Name = "costeKiloNumericTextBox";
			this.costeKiloNumericTextBox.ReadOnly = true;
			this.costeKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.costeKiloNumericTextBox.TabIndex = 4;
			this.costeKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.costeKiloNumericTextBox.TextIsCurrency = false;
			this.costeKiloNumericTextBox.TextIsInteger = false;
			// 
			// Partida_GB
			// 
			this.Partida_GB.Controls.Add(this.numericTextBox1);
			this.Partida_GB.Controls.Add(label6);
			this.Partida_GB.Controls.Add(this.KilosIniciales_NTB);
			this.Partida_GB.Controls.Add(this.BultosIniciales_NTB);
			this.Partida_GB.Controls.Add(kilosInicialesLabel);
			this.Partida_GB.Controls.Add(bultosInicialesLabel);
			this.Partida_GB.Controls.Add(tipoMercanciaLabel);
			this.Partida_GB.Controls.Add(this.tipoMercanciaTextBox);
			this.Partida_GB.Location = new System.Drawing.Point(13, 11);
			this.Partida_GB.Name = "Partida_GB";
			this.Partida_GB.Size = new System.Drawing.Size(315, 177);
			this.Partida_GB.TabIndex = 2;
			this.Partida_GB.TabStop = false;
			this.Partida_GB.Text = "Datos";
			// 
			// numericTextBox1
			// 
			this.numericTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "KilosPorBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.numericTextBox1.ForeColor = System.Drawing.Color.Navy;
			this.numericTextBox1.Location = new System.Drawing.Point(210, 111);
			this.numericTextBox1.Name = "numericTextBox1";
			this.numericTextBox1.ReadOnly = true;
			this.numericTextBox1.Size = new System.Drawing.Size(90, 21);
			this.numericTextBox1.TabIndex = 5;
			this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericTextBox1.TextIsCurrency = false;
			this.numericTextBox1.TextIsInteger = false;
			// 
			// KilosIniciales_NTB
			// 
			this.KilosIniciales_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "KilosIniciales", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.KilosIniciales_NTB.ForeColor = System.Drawing.Color.Navy;
			this.KilosIniciales_NTB.Location = new System.Drawing.Point(114, 111);
			this.KilosIniciales_NTB.Name = "KilosIniciales_NTB";
			this.KilosIniciales_NTB.Size = new System.Drawing.Size(90, 21);
			this.KilosIniciales_NTB.TabIndex = 2;
			this.KilosIniciales_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.KilosIniciales_NTB.TextIsCurrency = false;
			this.KilosIniciales_NTB.TextIsInteger = false;
			// 
			// BultosIniciales_NTB
			// 
			this.BultosIniciales_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BultosIniciales", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.BultosIniciales_NTB.ForeColor = System.Drawing.Color.Navy;
			this.BultosIniciales_NTB.Location = new System.Drawing.Point(12, 111);
			this.BultosIniciales_NTB.Name = "BultosIniciales_NTB";
			this.BultosIniciales_NTB.Size = new System.Drawing.Size(96, 21);
			this.BultosIniciales_NTB.TabIndex = 1;
			this.BultosIniciales_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BultosIniciales_NTB.TextIsCurrency = false;
			this.BultosIniciales_NTB.TextIsInteger = true;
			// 
			// tipoMercanciaTextBox
			// 
			this.tipoMercanciaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoMercancia", true));
			this.tipoMercanciaTextBox.ForeColor = System.Drawing.Color.Navy;
			this.tipoMercanciaTextBox.Location = new System.Drawing.Point(12, 64);
			this.tipoMercanciaTextBox.Name = "tipoMercanciaTextBox";
			this.tipoMercanciaTextBox.Size = new System.Drawing.Size(288, 21);
			this.tipoMercanciaTextBox.TabIndex = 0;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Nombre";
			this.dataGridViewTextBoxColumn1.HeaderText = "Nombre";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Precio";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle4.Format = "C5";
			dataGridViewCellStyle4.NullValue = null;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn2.HeaderText = "Precio Compra";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.HeaderText = "Observaciones";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Nombre";
			this.dataGridViewTextBoxColumn4.HeaderText = "Nombre";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "OidProveedor";
			this.dataGridViewTextBoxColumn5.HeaderText = "OidProveedor";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "OidProducto";
			this.dataGridViewTextBoxColumn6.HeaderText = "OidProducto";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Precio";
			this.dataGridViewTextBoxColumn7.HeaderText = "Precio";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "Oid";
			this.dataGridViewTextBoxColumn8.HeaderText = "Oid";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "SessionCode";
			this.dataGridViewTextBoxColumn9.HeaderText = "SessionCode";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "nHMng";
			this.dataGridViewTextBoxColumn10.HeaderText = "nHMng";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			// 
			// PartidaNewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1084, 513);
			this.ControlBox = false;
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "PartidaNewForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Selección de Productos";
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.PanelesH.Panel1.ResumeLayout(false);
			this.PanelesH.Panel2.ResumeLayout(false);
			this.PanelesH.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ProductoTabla)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_ProductoProveedor)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.Partida_GB.ResumeLayout(false);
			this.Partida_GB.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer PanelesH;
        private System.Windows.Forms.GroupBox Partida_GB;
        private System.Windows.Forms.TextBox tipoMercanciaTextBox;
        private System.Windows.Forms.DataGridView ProductoTabla;
        private System.Windows.Forms.BindingSource Datos_ProductoProveedor;
        private moleQule.Face.Controls.NumericTextBox KilosIniciales_NTB;
        private moleQule.Face.Controls.NumericTextBox BultosIniciales_NTB;
        private System.Windows.Forms.GroupBox groupBox1;
        private moleQule.Face.Controls.NumericTextBox ayudaRecibidaKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox PVDKilo_NTB;
        private moleQule.Face.Controls.NumericTextBox costeKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox CosteBulto_NTB;
        private moleQule.Face.Controls.NumericTextBox PVPKilo_NTB;
        private moleQule.Face.Controls.NumericTextBox beneficioKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox BeneficioTotal_NTB;
        private moleQule.Face.Controls.NumericTextBox PVDBulto_NTB;
        private moleQule.Face.Controls.NumericTextBox PVPBulto_NTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private moleQule.Face.Controls.NumericTextBox numericTextBox1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaProductoNombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn PrecioProducto;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaProductoPrecio;
		private System.Windows.Forms.DataGridViewCheckBoxColumn bultoDataGridViewCheckBoxColumn;

    }
}
