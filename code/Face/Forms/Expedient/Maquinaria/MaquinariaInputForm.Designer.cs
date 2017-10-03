namespace moleQule.Face.Store
{
    partial class MaquinariaInputForm
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
			System.Windows.Forms.Label identificadorLabel;
			System.Windows.Forms.Label descripcionLabel;
			System.Windows.Forms.Label observacionesLabel;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label ayudaRecibidaKiloLabel;
			System.Windows.Forms.Label costeKiloLabel;
			System.Windows.Forms.Label precioKiloLabel;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.identificadorTextBox = new System.Windows.Forms.TextBox();
			this.Datos_Maquinaria = new System.Windows.Forms.BindingSource(this.components);
			this.descripcionTextBox = new System.Windows.Forms.TextBox();
			this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.BeneficioTotal_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PVPKilo_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.ayudaRecibidaKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.PVDKilo_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.costeKiloNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.ProductoTabla = new System.Windows.Forms.DataGridView();
			this.ColumnaProductoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnaProductoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnaPrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ayuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datos_Productos = new System.Windows.Forms.BindingSource(this.components);
			identificadorLabel = new System.Windows.Forms.Label();
			descripcionLabel = new System.Windows.Forms.Label();
			observacionesLabel = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			ayudaRecibidaKiloLabel = new System.Windows.Forms.Label();
			costeKiloLabel = new System.Windows.Forms.Label();
			precioKiloLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Maquinaria)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProductoTabla)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).BeginInit();
			this.SuspendLayout();
			// 
			// Datos
			// 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Batch);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(503, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(593, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(observacionesLabel);
			this.Source_GB.Controls.Add(this.observacionesRichTextBox);
			this.Source_GB.Controls.Add(descripcionLabel);
			this.Source_GB.Controls.Add(this.descripcionTextBox);
			this.Source_GB.Controls.Add(identificadorLabel);
			this.Source_GB.Controls.Add(this.identificadorTextBox);
			this.Source_GB.Location = new System.Drawing.Point(839, 210);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(332, 191);
			this.Source_GB.Text = "Datos Identificativos";
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.ProductoTabla);
			this.PanelesV.Panel1.Controls.Add(this.groupBox1);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(1184, 456);
			this.PanelesV.SplitterDistance = 416;
			// 
			// ProgressBK_Panel
			// 
			// 
			// identificadorLabel
			// 
			identificadorLabel.AutoSize = true;
			identificadorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			identificadorLabel.Location = new System.Drawing.Point(19, 23);
			identificadorLabel.Name = "identificadorLabel";
			identificadorLabel.Size = new System.Drawing.Size(72, 13);
			identificadorLabel.TabIndex = 0;
			identificadorLabel.Text = "Identificador:";
			// 
			// descripcionLabel
			// 
			descripcionLabel.AutoSize = true;
			descripcionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			descripcionLabel.Location = new System.Drawing.Point(26, 50);
			descripcionLabel.Name = "descripcionLabel";
			descripcionLabel.Size = new System.Drawing.Size(65, 13);
			descripcionLabel.TabIndex = 2;
			descripcionLabel.Text = "Descripción:";
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			observacionesLabel.Location = new System.Drawing.Point(9, 77);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 4;
			observacionesLabel.Text = "Observaciones:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label4.Location = new System.Drawing.Point(175, 25);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 13);
			label4.TabIndex = 46;
			label4.Text = "Precio Venta:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label3.Location = new System.Drawing.Point(109, 111);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(54, 13);
			label3.TabIndex = 44;
			label3.Text = "Beneficio:";
			// 
			// ayudaRecibidaKiloLabel
			// 
			ayudaRecibidaKiloLabel.AutoSize = true;
			ayudaRecibidaKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			ayudaRecibidaKiloLabel.Location = new System.Drawing.Point(42, 64);
			ayudaRecibidaKiloLabel.Name = "ayudaRecibidaKiloLabel";
			ayudaRecibidaKiloLabel.Size = new System.Drawing.Size(85, 13);
			ayudaRecibidaKiloLabel.TabIndex = 34;
			ayudaRecibidaKiloLabel.Text = "Ayuda Recibida:";
			// 
			// costeKiloLabel
			// 
			costeKiloLabel.AutoSize = true;
			costeKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			costeKiloLabel.Location = new System.Drawing.Point(174, 64);
			costeKiloLabel.Name = "costeKiloLabel";
			costeKiloLabel.Size = new System.Drawing.Size(39, 13);
			costeKiloLabel.TabIndex = 31;
			costeKiloLabel.Text = "Coste:";
			// 
			// precioKiloLabel
			// 
			precioKiloLabel.AutoSize = true;
			precioKiloLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			precioKiloLabel.Location = new System.Drawing.Point(42, 24);
			precioKiloLabel.Name = "precioKiloLabel";
			precioKiloLabel.Size = new System.Drawing.Size(80, 13);
			precioKiloLabel.TabIndex = 30;
			precioKiloLabel.Text = "Precio Compra:";
			// 
			// identificadorTextBox
			// 
			this.identificadorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Maquinaria, "Identificador", true));
			this.identificadorTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.identificadorTextBox.Location = new System.Drawing.Point(97, 20);
			this.identificadorTextBox.Name = "identificadorTextBox";
			this.identificadorTextBox.Size = new System.Drawing.Size(226, 21);
			this.identificadorTextBox.TabIndex = 0;
			// 
			// Datos_Maquinaria
			// 
			this.Datos_Maquinaria.DataSource = typeof(moleQule.Library.Store.Maquinaria);
			// 
			// descripcionTextBox
			// 
			this.descripcionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Maquinaria, "Descripcion", true));
			this.descripcionTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.descripcionTextBox.Location = new System.Drawing.Point(97, 47);
			this.descripcionTextBox.Name = "descripcionTextBox";
			this.descripcionTextBox.Size = new System.Drawing.Size(226, 21);
			this.descripcionTextBox.TabIndex = 1;
			// 
			// observacionesRichTextBox
			// 
			this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Maquinaria, "Observaciones", true));
			this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.observacionesRichTextBox.Location = new System.Drawing.Point(97, 74);
			this.observacionesRichTextBox.Name = "observacionesRichTextBox";
			this.observacionesRichTextBox.Size = new System.Drawing.Size(226, 96);
			this.observacionesRichTextBox.TabIndex = 2;
			this.observacionesRichTextBox.Text = "Nueva Cabeza de Ganado";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(label4);
			this.groupBox1.Controls.Add(label3);
			this.groupBox1.Controls.Add(this.BeneficioTotal_NTB);
			this.groupBox1.Controls.Add(this.PVPKilo_NTB);
			this.groupBox1.Controls.Add(this.ayudaRecibidaKiloNumericTextBox);
			this.groupBox1.Controls.Add(ayudaRecibidaKiloLabel);
			this.groupBox1.Controls.Add(this.PVDKilo_NTB);
			this.groupBox1.Controls.Add(this.costeKiloNumericTextBox);
			this.groupBox1.Controls.Add(costeKiloLabel);
			this.groupBox1.Controls.Add(precioKiloLabel);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(839, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 164);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Información de Precio y Coste";
			// 
			// BeneficioTotal_NTB
			// 
			this.BeneficioTotal_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioTotalEstimado", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.BeneficioTotal_NTB.Location = new System.Drawing.Point(112, 127);
			this.BeneficioTotal_NTB.Name = "BeneficioTotal_NTB";
			this.BeneficioTotal_NTB.ReadOnly = true;
			this.BeneficioTotal_NTB.Size = new System.Drawing.Size(112, 21);
			this.BeneficioTotal_NTB.TabIndex = 6;
			this.BeneficioTotal_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BeneficioTotal_NTB.TextIsCurrency = false;
			this.BeneficioTotal_NTB.TextIsInteger = false;
			// 
			// PVPKilo_NTB
			// 
			this.PVPKilo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioVentaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.PVPKilo_NTB.Location = new System.Drawing.Point(177, 41);
			this.PVPKilo_NTB.Name = "PVPKilo_NTB";
			this.PVPKilo_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVPKilo_NTB.TabIndex = 1;
			this.PVPKilo_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVPKilo_NTB.TextIsCurrency = false;
			this.PVPKilo_NTB.TextIsInteger = false;
			this.PVPKilo_NTB.TextChanged += new System.EventHandler(this.PVPKilo_NTB_TextChanged);
			// 
			// ayudaRecibidaKiloNumericTextBox
			// 
			this.ayudaRecibidaKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "AyudaRecibidaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.ayudaRecibidaKiloNumericTextBox.Location = new System.Drawing.Point(45, 81);
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
			this.PVDKilo_NTB.BackColor = System.Drawing.SystemColors.Window;
			this.PVDKilo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PrecioCompraKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
			this.PVDKilo_NTB.Location = new System.Drawing.Point(45, 41);
			this.PVDKilo_NTB.Name = "PVDKilo_NTB";
			this.PVDKilo_NTB.Size = new System.Drawing.Size(112, 21);
			this.PVDKilo_NTB.TabIndex = 0;
			this.PVDKilo_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PVDKilo_NTB.TextIsCurrency = false;
			this.PVDKilo_NTB.TextIsInteger = false;
			this.PVDKilo_NTB.TextChanged += new System.EventHandler(this.PVDKilo_NTB_TextChanged);
			// 
			// costeKiloNumericTextBox
			// 
			this.costeKiloNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CosteKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C5"));
			this.costeKiloNumericTextBox.Location = new System.Drawing.Point(177, 81);
			this.costeKiloNumericTextBox.Name = "costeKiloNumericTextBox";
			this.costeKiloNumericTextBox.ReadOnly = true;
			this.costeKiloNumericTextBox.Size = new System.Drawing.Size(112, 21);
			this.costeKiloNumericTextBox.TabIndex = 4;
			this.costeKiloNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.costeKiloNumericTextBox.TextIsCurrency = false;
			this.costeKiloNumericTextBox.TextIsInteger = false;
			// 
			// ProductoTabla
			// 
			this.ProductoTabla.AllowUserToAddRows = false;
			this.ProductoTabla.AllowUserToDeleteRows = false;
			this.ProductoTabla.AllowUserToResizeRows = false;
			this.ProductoTabla.AutoGenerateColumns = false;
			this.ProductoTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnaProductoNombre,
            this.ColumnaProductoPrecio,
            this.ColumnaPrecioVenta,
            this.Ayuda});
			this.ProductoTabla.DataSource = this.Datos_Productos;
			this.ProductoTabla.Location = new System.Drawing.Point(11, 3);
			this.ProductoTabla.Name = "ProductoTabla";
			this.ProductoTabla.ReadOnly = true;
			this.ProductoTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ProductoTabla.Size = new System.Drawing.Size(810, 398);
			this.ProductoTabla.TabIndex = 6;
			// 
			// ColumnaProductoNombre
			// 
			this.ColumnaProductoNombre.DataPropertyName = "Nombre";
			this.ColumnaProductoNombre.HeaderText = "Nombre";
			this.ColumnaProductoNombre.Name = "ColumnaProductoNombre";
			this.ColumnaProductoNombre.ReadOnly = true;
			this.ColumnaProductoNombre.Width = 250;
			// 
			// ColumnaProductoPrecio
			// 
			this.ColumnaProductoPrecio.DataPropertyName = "Precio";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle1.Format = "N2";
			dataGridViewCellStyle1.NullValue = null;
			this.ColumnaProductoPrecio.DefaultCellStyle = dataGridViewCellStyle1;
			this.ColumnaProductoPrecio.HeaderText = "Precio Compra";
			this.ColumnaProductoPrecio.Name = "ColumnaProductoPrecio";
			this.ColumnaProductoPrecio.ReadOnly = true;
			this.ColumnaProductoPrecio.Width = 80;
			// 
			// ColumnaPrecioVenta
			// 
			this.ColumnaPrecioVenta.DataPropertyName = "PrecioVenta";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			this.ColumnaPrecioVenta.DefaultCellStyle = dataGridViewCellStyle2;
			this.ColumnaPrecioVenta.HeaderText = "PrecioVenta";
			this.ColumnaPrecioVenta.Name = "ColumnaPrecioVenta";
			this.ColumnaPrecioVenta.ReadOnly = true;
			this.ColumnaPrecioVenta.Width = 80;
			// 
			// Ayuda
			// 
			this.Ayuda.DataPropertyName = "Ayuda";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle3.Format = "N2";
			this.Ayuda.DefaultCellStyle = dataGridViewCellStyle3;
			this.Ayuda.HeaderText = "Ayuda";
			this.Ayuda.Name = "Ayuda";
			this.Ayuda.ReadOnly = true;
			this.Ayuda.Width = 80;
			// 
			// Datos_Productos
			// 
			this.Datos_Productos.DataSource = typeof(moleQule.Library.Store.ProductoProveedorInfo);
			this.Datos_Productos.CurrentChanged += new System.EventHandler(this.Datos_Productos_CurrentChanged);
			// 
			// MaquinariaInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1184, 456);
			this.ControlBox = false;
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "MaquinariaInputForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Nueva Máquina";
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Maquinaria)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProductoTabla)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox observacionesRichTextBox;
        private System.Windows.Forms.TextBox descripcionTextBox;
        private System.Windows.Forms.TextBox identificadorTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private moleQule.Face.Controls.NumericTextBox BeneficioTotal_NTB;
        private moleQule.Face.Controls.NumericTextBox PVPKilo_NTB;
        private moleQule.Face.Controls.NumericTextBox ayudaRecibidaKiloNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox PVDKilo_NTB;
        private moleQule.Face.Controls.NumericTextBox costeKiloNumericTextBox;
        private System.Windows.Forms.DataGridView ProductoTabla;
        private System.Windows.Forms.BindingSource Datos_Productos;
        private System.Windows.Forms.BindingSource Datos_Maquinaria;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaProductoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaProductoPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaPrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ayuda;

    }
}
