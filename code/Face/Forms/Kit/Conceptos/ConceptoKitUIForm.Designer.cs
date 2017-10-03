namespace moleQule.Face.Store
{
    partial class ConceptoKitUIForm
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
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label subtotalLabel;
            System.Windows.Forms.Label conceptoLabel;
            System.Windows.Forms.Label cantidadLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PanelExpedientes = new System.Windows.Forms.SplitContainer();
            this.ProductosPanel = new System.Windows.Forms.SplitContainer();
            this.Tabla_Productos = new System.Windows.Forms.DataGridView();
            this.Datos_Partida = new System.Windows.Forms.BindingSource(this.components);
            this.ConceptoAlbaran_GB = new System.Windows.Forms.GroupBox();
            this.PrecioProducto_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Proporcion_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Concepto_TB = new System.Windows.Forms.TextBox();
            this.Kilos_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Productos_BT = new System.Windows.Forms.Button();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpedienteCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVentaKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KilosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BultosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockBultos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KiloPorBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label5 = new System.Windows.Forms.Label();
            subtotalLabel = new System.Windows.Forms.Label();
            conceptoLabel = new System.Windows.Forms.Label();
            cantidadLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.PanelExpedientes.Panel2.SuspendLayout();
            this.PanelExpedientes.SuspendLayout();
            this.ProductosPanel.Panel1.SuspendLayout();
            this.ProductosPanel.Panel2.SuspendLayout();
            this.ProductosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabla_Productos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).BeginInit();
            this.ConceptoAlbaran_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.OutputDeliveryLine);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(503, 8);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(593, 8);
            // 
            // Source_GB
            // 
            this.Source_GB.Enabled = false;
            this.Source_GB.Location = new System.Drawing.Point(3, 3);
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
            this.PanelesV.Size = new System.Drawing.Size(1034, 466);
            this.PanelesV.SplitterDistance = 425;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(597, 48);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(86, 13);
            label5.TabIndex = 27;
            label5.Text = "Precio Producto:";
            // 
            // subtotalLabel
            // 
            subtotalLabel.AutoSize = true;
            subtotalLabel.Location = new System.Drawing.Point(374, 49);
            subtotalLabel.Name = "subtotalLabel";
            subtotalLabel.Size = new System.Drawing.Size(62, 13);
            subtotalLabel.TabIndex = 12;
            subtotalLabel.Text = "Proporción:";
            // 
            // conceptoLabel
            // 
            conceptoLabel.AutoSize = true;
            conceptoLabel.Location = new System.Drawing.Point(22, 18);
            conceptoLabel.Name = "conceptoLabel";
            conceptoLabel.Size = new System.Drawing.Size(65, 13);
            conceptoLabel.TabIndex = 4;
            conceptoLabel.Text = "Descripción:";
            // 
            // cantidadLabel
            // 
            cantidadLabel.AutoSize = true;
            cantidadLabel.Location = new System.Drawing.Point(473, 48);
            cantidadLabel.Name = "cantidadLabel";
            cantidadLabel.Size = new System.Drawing.Size(84, 13);
            cantidadLabel.TabIndex = 0;
            cantidadLabel.Text = "Cantidad en Kg:";
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
            this.PanelExpedientes.Size = new System.Drawing.Size(1032, 423);
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
            this.ProductosPanel.Panel1.Controls.Add(this.Tabla_Productos);
            // 
            // ProductosPanel.Panel2
            // 
            this.ProductosPanel.Panel2.AutoScroll = true;
            this.ProductosPanel.Panel2.Controls.Add(this.ConceptoAlbaran_GB);
            this.ProductosPanel.Panel2.Controls.Add(this.Productos_BT);
            this.ProductosPanel.Size = new System.Drawing.Size(1032, 423);
            this.ProductosPanel.SplitterDistance = 58;
            this.ProductosPanel.TabIndex = 3;
            // 
            // Tabla_Productos
            // 
            this.Tabla_Productos.AllowUserToAddRows = false;
            this.Tabla_Productos.AllowUserToDeleteRows = false;
            this.Tabla_Productos.AllowUserToResizeRows = false;
            this.Tabla_Productos.AutoGenerateColumns = false;
            this.Tabla_Productos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.ExpedienteCol,
            this.PrecioVentaKilo,
            this.KilosIniciales,
            this.StockKilos,
            this.BultosIniciales,
            this.StockBultos,
            this.KiloPorBulto});
            this.Tabla_Productos.DataSource = this.Datos_Partida;
            this.Tabla_Productos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabla_Productos.Enabled = false;
            this.Tabla_Productos.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Productos.MultiSelect = false;
            this.Tabla_Productos.Name = "Tabla_Productos";
            this.Tabla_Productos.ReadOnly = true;
            this.Tabla_Productos.RowHeadersVisible = false;
            this.Tabla_Productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tabla_Productos.Size = new System.Drawing.Size(1032, 58);
            this.Tabla_Productos.TabIndex = 1;
            // 
            // Datos_Partida
            // 
            this.Datos_Partida.DataSource = typeof(moleQule.Library.Store.BatchInfo);
            this.Datos_Partida.CurrentChanged += new System.EventHandler(this.Datos_Partida_CurrentChanged);
            // 
            // ConceptoAlbaran_GB
            // 
            this.ConceptoAlbaran_GB.Controls.Add(label5);
            this.ConceptoAlbaran_GB.Controls.Add(this.PrecioProducto_NTB);
            this.ConceptoAlbaran_GB.Controls.Add(subtotalLabel);
            this.ConceptoAlbaran_GB.Controls.Add(this.Proporcion_NTB);
            this.ConceptoAlbaran_GB.Controls.Add(conceptoLabel);
            this.ConceptoAlbaran_GB.Controls.Add(this.Concepto_TB);
            this.ConceptoAlbaran_GB.Controls.Add(cantidadLabel);
            this.ConceptoAlbaran_GB.Controls.Add(this.Kilos_NTB);
            this.ConceptoAlbaran_GB.Location = new System.Drawing.Point(155, 118);
            this.ConceptoAlbaran_GB.Name = "ConceptoAlbaran_GB";
            this.ConceptoAlbaran_GB.Size = new System.Drawing.Size(723, 125);
            this.ConceptoAlbaran_GB.TabIndex = 4;
            this.ConceptoAlbaran_GB.TabStop = false;
            this.ConceptoAlbaran_GB.Text = "Datos";
            // 
            // PrecioProducto_NTB
            // 
            this.PrecioProducto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Partida, "PrecioVentaKilo", true));
            this.PrecioProducto_NTB.Location = new System.Drawing.Point(600, 64);
            this.PrecioProducto_NTB.Name = "PrecioProducto_NTB";
            this.PrecioProducto_NTB.ReadOnly = true;
            this.PrecioProducto_NTB.Size = new System.Drawing.Size(83, 21);
            this.PrecioProducto_NTB.TabIndex = 26;
            this.PrecioProducto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioProducto_NTB.TextIsCurrency = false;
            this.PrecioProducto_NTB.TextIsInteger = false;
            // 
            // Proporcion_NTB
            // 
            this.Proporcion_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Proporcion", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Proporcion_NTB.Location = new System.Drawing.Point(374, 64);
            this.Proporcion_NTB.Name = "Proporcion_NTB";
            this.Proporcion_NTB.Size = new System.Drawing.Size(55, 21);
            this.Proporcion_NTB.TabIndex = 4;
            this.Proporcion_NTB.Text = "0";
            this.Proporcion_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Proporcion_NTB.TextIsCurrency = false;
            this.Proporcion_NTB.TextIsInteger = false;
            this.Proporcion_NTB.Validating += new System.ComponentModel.CancelEventHandler(this.Proporcion_NTB_Validating);
            // 
            // Concepto_TB
            // 
            this.Concepto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoMercancia", true));
            this.Concepto_TB.ForeColor = System.Drawing.Color.Navy;
            this.Concepto_TB.Location = new System.Drawing.Point(25, 34);
            this.Concepto_TB.Multiline = true;
            this.Concepto_TB.Name = "Concepto_TB";
            this.Concepto_TB.Size = new System.Drawing.Size(301, 73);
            this.Concepto_TB.TabIndex = 0;
            // 
            // Kilos_NTB
            // 
            this.Kilos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "KilosIniciales", true));
            this.Kilos_NTB.ForeColor = System.Drawing.Color.Navy;
            this.Kilos_NTB.Location = new System.Drawing.Point(476, 64);
            this.Kilos_NTB.Name = "Kilos_NTB";
            this.Kilos_NTB.ReadOnly = true;
            this.Kilos_NTB.Size = new System.Drawing.Size(81, 21);
            this.Kilos_NTB.TabIndex = 1;
            this.Kilos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Kilos_NTB.TextIsCurrency = false;
            this.Kilos_NTB.TextIsInteger = false;
            // 
            // Productos_BT
            // 
            this.Productos_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos_BT.Location = new System.Drawing.Point(455, 18);
            this.Productos_BT.Name = "Productos_BT";
            this.Productos_BT.Size = new System.Drawing.Size(123, 23);
            this.Productos_BT.TabIndex = 3;
            this.Productos_BT.Text = "Productos";
            this.Productos_BT.UseVisualStyleBackColor = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "TipoMercancia";
            this.Nombre.HeaderText = "Producto";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 403;
            // 
            // ExpedienteCol
            // 
            this.ExpedienteCol.DataPropertyName = "Expedient";
            this.ExpedienteCol.HeaderText = "Expediente";
            this.ExpedienteCol.Name = "ExpedienteCol";
            this.ExpedienteCol.ReadOnly = true;
            this.ExpedienteCol.Width = 150;
            // 
            // PrecioVentaKilo
            // 
            this.PrecioVentaKilo.DataPropertyName = "PrecioVentaKilo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C5";
            this.PrecioVentaKilo.DefaultCellStyle = dataGridViewCellStyle1;
            this.PrecioVentaKilo.HeaderText = "Precio Venta Kg";
            this.PrecioVentaKilo.Name = "PrecioVentaKilo";
            this.PrecioVentaKilo.ReadOnly = true;
            this.PrecioVentaKilo.Width = 75;
            // 
            // KilosIniciales
            // 
            this.KilosIniciales.DataPropertyName = "KilosIniciales";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.KilosIniciales.DefaultCellStyle = dataGridViewCellStyle2;
            this.KilosIniciales.HeaderText = "Kg Iniciales";
            this.KilosIniciales.Name = "KilosIniciales";
            this.KilosIniciales.ReadOnly = true;
            this.KilosIniciales.Width = 80;
            // 
            // StockKilos
            // 
            this.StockKilos.DataPropertyName = "StockKilos";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.StockKilos.DefaultCellStyle = dataGridViewCellStyle3;
            this.StockKilos.HeaderText = "Stock Kg";
            this.StockKilos.Name = "StockKilos";
            this.StockKilos.ReadOnly = true;
            this.StockKilos.Width = 80;
            // 
            // BultosIniciales
            // 
            this.BultosIniciales.DataPropertyName = "BultosIniciales";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.BultosIniciales.DefaultCellStyle = dataGridViewCellStyle4;
            this.BultosIniciales.HeaderText = "Bultos Iniciales";
            this.BultosIniciales.Name = "BultosIniciales";
            this.BultosIniciales.ReadOnly = true;
            this.BultosIniciales.Width = 80;
            // 
            // StockBultos
            // 
            this.StockBultos.DataPropertyName = "StockBultos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.StockBultos.DefaultCellStyle = dataGridViewCellStyle5;
            this.StockBultos.HeaderText = "Stock Bultos";
            this.StockBultos.Name = "StockBultos";
            this.StockBultos.ReadOnly = true;
            this.StockBultos.Width = 80;
            // 
            // KiloPorBulto
            // 
            this.KiloPorBulto.DataPropertyName = "KilosPorBulto";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.KiloPorBulto.DefaultCellStyle = dataGridViewCellStyle6;
            this.KiloPorBulto.HeaderText = "Kg / Bulto";
            this.KiloPorBulto.Name = "KiloPorBulto";
            this.KiloPorBulto.ReadOnly = true;
            this.KiloPorBulto.Width = 80;
            // 
            // ConceptoKitUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 466);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "ConceptoKitUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Componente para Mezcla";
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.PanelExpedientes.Panel2.ResumeLayout(false);
            this.PanelExpedientes.ResumeLayout(false);
            this.ProductosPanel.Panel1.ResumeLayout(false);
            this.ProductosPanel.Panel2.ResumeLayout(false);
            this.ProductosPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tabla_Productos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).EndInit();
            this.ConceptoAlbaran_GB.ResumeLayout(false);
            this.ConceptoAlbaran_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer PanelExpedientes;
        protected System.Windows.Forms.SplitContainer ProductosPanel;
        protected System.Windows.Forms.BindingSource Datos_Partida;
        protected System.Windows.Forms.DataGridView Tabla_Productos;
        protected System.Windows.Forms.Button Productos_BT;
        private System.Windows.Forms.GroupBox ConceptoAlbaran_GB;
        private moleQule.Face.Controls.NumericTextBox PrecioProducto_NTB;
        private moleQule.Face.Controls.NumericTextBox Proporcion_NTB;
        private System.Windows.Forms.TextBox Concepto_TB;
        private moleQule.Face.Controls.NumericTextBox Kilos_NTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpedienteCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVentaKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn BultosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockBultos;
        private System.Windows.Forms.DataGridViewTextBoxColumn KiloPorBulto;


    }
}
