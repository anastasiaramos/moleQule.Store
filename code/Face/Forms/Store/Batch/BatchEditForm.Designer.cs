namespace moleQule.Face.Store
{
	partial class BatchEditForm
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
			System.Windows.Forms.Label costeKiloLabel;
			System.Windows.Forms.Label beneficioKiloLabel;
			System.Windows.Forms.Label precioKiloLabel;
			System.Windows.Forms.Label ayudaRecibidaKiloLabel;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label stockBultosLabel;
			System.Windows.Forms.Label stockKilosLabel;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchEditForm));
			this.Datos = new System.Windows.Forms.BindingSource(this.components);
			this.tipoMercanciaTextBox = new System.Windows.Forms.TextBox();
			this.bultosInicialesNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.kilosInicialesNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.BeneficioTotal_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.CosteBulto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PrecioProveedor_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PrecioProducto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.costeKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.beneficioKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.ayudaRecibidaKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.stockBultosNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.stockKilosNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.Ubicacion_TB = new System.Windows.Forms.TextBox();
			kilosInicialesLabel = new System.Windows.Forms.Label();
			bultosInicialesLabel = new System.Windows.Forms.Label();
			tipoMercanciaLabel = new System.Windows.Forms.Label();
			costeKiloLabel = new System.Windows.Forms.Label();
			beneficioKiloLabel = new System.Windows.Forms.Label();
			precioKiloLabel = new System.Windows.Forms.Label();
			ayudaRecibidaKiloLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			stockBultosLabel = new System.Windows.Forms.Label();
			stockKilosLabel = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Print_BT
			// 
			this.Print_BT.Location = new System.Drawing.Point(208, 60);
			this.HelpProvider.SetShowHelp(this.Print_BT, true);
			this.Print_BT.Size = new System.Drawing.Size(87, 23);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(134, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(224, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(label4);
			this.Source_GB.Controls.Add(this.Ubicacion_TB);
			this.Source_GB.Controls.Add(this.stockKilosNumericTextBox);
			this.Source_GB.Controls.Add(stockKilosLabel);
			this.Source_GB.Controls.Add(this.stockBultosNumericTextBox);
			this.Source_GB.Controls.Add(stockBultosLabel);
			this.Source_GB.Controls.Add(this.kilosInicialesNumericTextBox);
			this.Source_GB.Controls.Add(this.bultosInicialesNumericTextBox);
			this.Source_GB.Controls.Add(kilosInicialesLabel);
			this.Source_GB.Controls.Add(bultosInicialesLabel);
			this.Source_GB.Controls.Add(tipoMercanciaLabel);
			this.Source_GB.Controls.Add(this.tipoMercanciaTextBox);
			this.Source_GB.Location = new System.Drawing.Point(18, 19);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(409, 210);
			this.Source_GB.Text = "Datos Generales";
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.Controls.Add(this.groupBox1);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(446, 464);
			this.PanelesV.SplitterDistance = 424;
			// 
			// kilosInicialesLabel
			// 
			kilosInicialesLabel.AutoSize = true;
			kilosInicialesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			kilosInicialesLabel.Location = new System.Drawing.Point(236, 60);
			kilosInicialesLabel.Name = "kilosInicialesLabel";
			kilosInicialesLabel.Size = new System.Drawing.Size(73, 13);
			kilosInicialesLabel.TabIndex = 32;
			kilosInicialesLabel.Text = "Kilos Iniciales:";
			// 
			// bultosInicialesLabel
			// 
			bultosInicialesLabel.AutoSize = true;
			bultosInicialesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			bultosInicialesLabel.Location = new System.Drawing.Point(59, 60);
			bultosInicialesLabel.Name = "bultosInicialesLabel";
			bultosInicialesLabel.Size = new System.Drawing.Size(81, 13);
			bultosInicialesLabel.TabIndex = 30;
			bultosInicialesLabel.Text = "Bultos Iniciales:";
			// 
			// tipoMercanciaLabel
			// 
			tipoMercanciaLabel.AutoSize = true;
			tipoMercanciaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			tipoMercanciaLabel.Location = new System.Drawing.Point(17, 19);
			tipoMercanciaLabel.Name = "tipoMercanciaLabel";
			tipoMercanciaLabel.Size = new System.Drawing.Size(59, 13);
			tipoMercanciaLabel.TabIndex = 28;
			tipoMercanciaLabel.Text = "Mercancía:";
			// 
			// costeKiloLabel
			// 
			costeKiloLabel.AutoSize = true;
			costeKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			costeKiloLabel.Location = new System.Drawing.Point(155, 17);
			costeKiloLabel.Name = "costeKiloLabel";
			costeKiloLabel.Size = new System.Drawing.Size(58, 13);
			costeKiloLabel.TabIndex = 57;
			costeKiloLabel.Text = "Coste Kilo:";
			// 
			// beneficioKiloLabel
			// 
			beneficioKiloLabel.AutoSize = true;
			beneficioKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			beneficioKiloLabel.Location = new System.Drawing.Point(279, 68);
			beneficioKiloLabel.Name = "beneficioKiloLabel";
			beneficioKiloLabel.Size = new System.Drawing.Size(73, 13);
			beneficioKiloLabel.TabIndex = 58;
			beneficioKiloLabel.Text = "Beneficio Kilo:";
			// 
			// precioKiloLabel
			// 
			precioKiloLabel.AutoSize = true;
			precioKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			precioKiloLabel.Location = new System.Drawing.Point(17, 17);
			precioKiloLabel.Name = "precioKiloLabel";
			precioKiloLabel.Size = new System.Drawing.Size(103, 13);
			precioKiloLabel.TabIndex = 61;
			precioKiloLabel.Text = "Precio del Producto:";
			// 
			// ayudaRecibidaKiloLabel
			// 
			ayudaRecibidaKiloLabel.AutoSize = true;
			ayudaRecibidaKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			ayudaRecibidaKiloLabel.Location = new System.Drawing.Point(279, 17);
			ayudaRecibidaKiloLabel.Name = "ayudaRecibidaKiloLabel";
			ayudaRecibidaKiloLabel.Size = new System.Drawing.Size(104, 13);
			ayudaRecibidaKiloLabel.TabIndex = 62;
			ayudaRecibidaKiloLabel.Text = "Ayuda Recibida Kilo:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(17, 68);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(110, 13);
			label1.TabIndex = 70;
			label1.Text = "Precio del Proveedor:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label2.Location = new System.Drawing.Point(155, 68);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(66, 13);
			label2.TabIndex = 72;
			label2.Text = "Coste Bulto:";
			// 
			// stockBultosLabel
			// 
			stockBultosLabel.AutoSize = true;
			stockBultosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stockBultosLabel.Location = new System.Drawing.Point(59, 100);
			stockBultosLabel.Name = "stockBultosLabel";
			stockBultosLabel.Size = new System.Drawing.Size(69, 13);
			stockBultosLabel.TabIndex = 68;
			stockBultosLabel.Text = "Stock Bultos:";
			// 
			// stockKilosLabel
			// 
			stockKilosLabel.AutoSize = true;
			stockKilosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stockKilosLabel.Location = new System.Drawing.Point(236, 100);
			stockKilosLabel.Name = "stockKilosLabel";
			stockKilosLabel.Size = new System.Drawing.Size(61, 13);
			stockKilosLabel.TabIndex = 70;
			stockKilosLabel.Text = "Stock Kilos:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label3.Location = new System.Drawing.Point(148, 114);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(127, 13);
			label3.TabIndex = 74;
			label3.Text = "Beneficio Total Estimado:";
			// 
			// Datos
			// 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Batch);
			// 
			// tipoMercanciaTextBox
			// 
			this.tipoMercanciaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoMercancia", true));
			this.tipoMercanciaTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tipoMercanciaTextBox.Location = new System.Drawing.Point(17, 36);
			this.tipoMercanciaTextBox.Name = "tipoMercanciaTextBox";
			this.tipoMercanciaTextBox.Size = new System.Drawing.Size(374, 21);
			this.tipoMercanciaTextBox.TabIndex = 0;
			// 
			// bultosInicialesNumericTextBox
			// 
			this.bultosInicialesNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BultosIniciales", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.bultosInicialesNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bultosInicialesNumericTextBox.ForeColor = System.Drawing.Color.Navy;
			this.bultosInicialesNumericTextBox.Location = new System.Drawing.Point(59, 76);
			this.bultosInicialesNumericTextBox.Name = "bultosInicialesNumericTextBox";
			this.bultosInicialesNumericTextBox.ReadOnly = true;
			this.bultosInicialesNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.bultosInicialesNumericTextBox.TabIndex = 1;
			this.bultosInicialesNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.bultosInicialesNumericTextBox.TextIsCurrency = false;
			this.bultosInicialesNumericTextBox.TextIsInteger = true;
			// 
			// kilosInicialesNumericTextBox
			// 
			this.kilosInicialesNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "KilosIniciales", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.kilosInicialesNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kilosInicialesNumericTextBox.ForeColor = System.Drawing.Color.Navy;
			this.kilosInicialesNumericTextBox.Location = new System.Drawing.Point(238, 76);
			this.kilosInicialesNumericTextBox.Name = "kilosInicialesNumericTextBox";
			this.kilosInicialesNumericTextBox.ReadOnly = true;
			this.kilosInicialesNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.kilosInicialesNumericTextBox.TabIndex = 2;
			this.kilosInicialesNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.kilosInicialesNumericTextBox.TextIsCurrency = false;
			this.kilosInicialesNumericTextBox.TextIsInteger = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(label3);
			this.groupBox1.Controls.Add(this.BeneficioTotal_NTB);
			this.groupBox1.Controls.Add(label2);
			this.groupBox1.Controls.Add(this.CosteBulto_NTB);
			this.groupBox1.Controls.Add(label1);
			this.groupBox1.Controls.Add(this.PrecioProveedor_NTB);
			this.groupBox1.Controls.Add(this.PrecioProducto_NTB);
			this.groupBox1.Controls.Add(this.costeKiloNumericTextBox);
			this.groupBox1.Controls.Add(this.beneficioKiloNumericTextBox);
			this.groupBox1.Controls.Add(this.ayudaRecibidaKiloNumericTextBox);
			this.groupBox1.Controls.Add(ayudaRecibidaKiloLabel);
			this.groupBox1.Controls.Add(precioKiloLabel);
			this.groupBox1.Controls.Add(beneficioKiloLabel);
			this.groupBox1.Controls.Add(costeKiloLabel);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(18, 243);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(409, 162);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Precio y Coste";
			// 
			// BeneficioTotal_NTB
			// 
			this.BeneficioTotal_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioTotalEstimado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.BeneficioTotal_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BeneficioTotal_NTB.Location = new System.Drawing.Point(155, 130);
			this.BeneficioTotal_NTB.Name = "BeneficioTotal_NTB";
			this.BeneficioTotal_NTB.ReadOnly = true;
			this.BeneficioTotal_NTB.Size = new System.Drawing.Size(112, 21);
			this.BeneficioTotal_NTB.TabIndex = 6;
			this.BeneficioTotal_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BeneficioTotal_NTB.TextIsCurrency = false;
			this.BeneficioTotal_NTB.TextIsInteger = false;
			// 
			// CosteBulto_NTB
			// 
			this.CosteBulto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CosteBulto", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.CosteBulto_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CosteBulto_NTB.Location = new System.Drawing.Point(155, 84);
			this.CosteBulto_NTB.Name = "CosteBulto_NTB";
			this.CosteBulto_NTB.ReadOnly = true;
			this.CosteBulto_NTB.Size = new System.Drawing.Size(112, 21);
			this.CosteBulto_NTB.TabIndex = 4;
			this.CosteBulto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CosteBulto_NTB.TextIsCurrency = false;
			this.CosteBulto_NTB.TextIsInteger = false;
			// 
			// PrecioProveedor_NTB
			// 
			this.PrecioProveedor_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioCompraProveedor", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.PrecioProveedor_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PrecioProveedor_NTB.Location = new System.Drawing.Point(17, 84);
			this.PrecioProveedor_NTB.Name = "PrecioProveedor_NTB";
			this.PrecioProveedor_NTB.ReadOnly = true;
			this.PrecioProveedor_NTB.Size = new System.Drawing.Size(119, 21);
			this.PrecioProveedor_NTB.TabIndex = 3;
			this.PrecioProveedor_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PrecioProveedor_NTB.TextIsCurrency = false;
			this.PrecioProveedor_NTB.TextIsInteger = false;
			// 
			// PrecioProducto_NTB
			// 
			this.PrecioProducto_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PrecioProducto_NTB.Location = new System.Drawing.Point(17, 33);
			this.PrecioProducto_NTB.Name = "PrecioProducto_NTB";
			this.PrecioProducto_NTB.ReadOnly = true;
			this.PrecioProducto_NTB.Size = new System.Drawing.Size(119, 21);
			this.PrecioProducto_NTB.TabIndex = 0;
			this.PrecioProducto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PrecioProducto_NTB.TextIsCurrency = false;
			this.PrecioProducto_NTB.TextIsInteger = false;
			// 
			// costeKiloNumericTextBox
			// 
			this.costeKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CosteKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.costeKiloNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.costeKiloNumericTextBox.Location = new System.Drawing.Point(155, 33);
			this.costeKiloNumericTextBox.Name = "costeKiloNumericTextBox";
			this.costeKiloNumericTextBox.ReadOnly = true;
			this.costeKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.costeKiloNumericTextBox.TabIndex = 1;
			this.costeKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.costeKiloNumericTextBox.TextIsCurrency = false;
			this.costeKiloNumericTextBox.TextIsInteger = false;
			// 
			// beneficioKiloNumericTextBox
			// 
			this.beneficioKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.beneficioKiloNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.beneficioKiloNumericTextBox.Location = new System.Drawing.Point(279, 84);
			this.beneficioKiloNumericTextBox.Name = "beneficioKiloNumericTextBox";
			this.beneficioKiloNumericTextBox.ReadOnly = true;
			this.beneficioKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.beneficioKiloNumericTextBox.TabIndex = 5;
			this.beneficioKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.beneficioKiloNumericTextBox.TextIsCurrency = false;
			this.beneficioKiloNumericTextBox.TextIsInteger = false;
			// 
			// ayudaRecibidaKiloNumericTextBox
			// 
			this.ayudaRecibidaKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "AyudaRecibidaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
			this.ayudaRecibidaKiloNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ayudaRecibidaKiloNumericTextBox.Location = new System.Drawing.Point(279, 33);
			this.ayudaRecibidaKiloNumericTextBox.Name = "ayudaRecibidaKiloNumericTextBox";
			this.ayudaRecibidaKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.ayudaRecibidaKiloNumericTextBox.TabIndex = 2;
			this.ayudaRecibidaKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ayudaRecibidaKiloNumericTextBox.TextIsCurrency = false;
			this.ayudaRecibidaKiloNumericTextBox.TextIsInteger = false;
			// 
			// stockBultosNumericTextBox
			// 
			this.stockBultosNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "StockBultos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.stockBultosNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.stockBultosNumericTextBox.Location = new System.Drawing.Point(59, 116);
			this.stockBultosNumericTextBox.Name = "stockBultosNumericTextBox";
			this.stockBultosNumericTextBox.ReadOnly = true;
			this.stockBultosNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.stockBultosNumericTextBox.TabIndex = 3;
			this.stockBultosNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stockBultosNumericTextBox.TextIsCurrency = false;
			this.stockBultosNumericTextBox.TextIsInteger = true;
			// 
			// stockKilosNumericTextBox
			// 
			this.stockKilosNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "StockKilos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.stockKilosNumericTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.stockKilosNumericTextBox.Location = new System.Drawing.Point(238, 116);
			this.stockKilosNumericTextBox.Name = "stockKilosNumericTextBox";
			this.stockKilosNumericTextBox.ReadOnly = true;
			this.stockKilosNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.stockKilosNumericTextBox.TabIndex = 4;
			this.stockKilosNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stockKilosNumericTextBox.TextIsCurrency = false;
			this.stockKilosNumericTextBox.TextIsInteger = false;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label4.Location = new System.Drawing.Point(17, 149);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(56, 13);
			label4.TabIndex = 72;
			label4.Text = "Ubicación:";
			// 
			// Ubicacion_TB
			// 
			this.Ubicacion_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Ubicacion", true));
			this.Ubicacion_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Ubicacion_TB.Location = new System.Drawing.Point(17, 166);
			this.Ubicacion_TB.Name = "Ubicacion_TB";
			this.Ubicacion_TB.Size = new System.Drawing.Size(374, 21);
			this.Ubicacion_TB.TabIndex = 71;
			// 
			// PartidaEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(446, 464);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PartidaEditForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Editar Partida";
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Datos;
        private System.Windows.Forms.TextBox tipoMercanciaTextBox;
        private moleQule.Face.Controls.NumericTextBox kilosInicialesNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox bultosInicialesNumericTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private moleQule.Face.Controls.NumericTextBox stockKilosNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox stockBultosNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox CosteBulto_NTB;
        private moleQule.Face.Controls.NumericTextBox PrecioProveedor_NTB;
        private moleQule.Face.Controls.NumericTextBox PrecioProducto_NTB;
        private moleQule.Face.Controls.NumericTextBox costeKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox beneficioKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox ayudaRecibidaKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox BeneficioTotal_NTB;
		private System.Windows.Forms.TextBox Ubicacion_TB;
    }
}
