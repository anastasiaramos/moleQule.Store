namespace moleQule.Face.Store
{
    partial class PrecioOrigenForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label precioLabel1;
            System.Windows.Forms.Label proveedorLabel;
            this.PuertoOrigen_CB = new System.Windows.Forms.ComboBox();
            this.Datos_PuertoOrigen = new System.Windows.Forms.BindingSource(this.components);
            this.precioNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
            this.proveedorTextBox = new System.Windows.Forms.TextBox();
            this.Proveedor_BT = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            precioLabel1 = new System.Windows.Forms.Label();
            proveedorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.Source_GB.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoOrigen)).BeginInit();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.PrecioOrigen);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(145, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(235, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.Proveedor_BT);
            this.Source_GB.Controls.Add(proveedorLabel);
            this.Source_GB.Controls.Add(this.proveedorTextBox);
            this.Source_GB.Controls.Add(precioLabel1);
            this.Source_GB.Controls.Add(this.precioNumericTextBox);
            this.Source_GB.Controls.Add(label1);
            this.Source_GB.Controls.Add(this.PuertoOrigen_CB);
            this.Source_GB.Location = new System.Drawing.Point(37, 42);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(410, 130);
            this.Source_GB.Text = "";
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
            this.PanelesV.Size = new System.Drawing.Size(484, 256);
            this.PanelesV.SplitterDistance = 216;
            // 
            // ProgressBK_Panel
            // 
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label1.Location = new System.Drawing.Point(29, 36);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(78, 13);
            label1.TabIndex = 4;
            label1.Text = "Puerto Origen:";
            // 
            // precioLabel1
            // 
            precioLabel1.AutoSize = true;
            precioLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            precioLabel1.Location = new System.Drawing.Point(67, 90);
            precioLabel1.Name = "precioLabel1";
            precioLabel1.Size = new System.Drawing.Size(40, 13);
            precioLabel1.TabIndex = 4;
            precioLabel1.Text = "Precio:";
            // 
            // proveedorLabel
            // 
            proveedorLabel.AutoSize = true;
            proveedorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            proveedorLabel.Location = new System.Drawing.Point(46, 63);
            proveedorLabel.Name = "proveedorLabel";
            proveedorLabel.Size = new System.Drawing.Size(61, 13);
            proveedorLabel.TabIndex = 5;
            proveedorLabel.Text = "Proveedor:";
            // 
            // PuertoOrigen_CB
            // 
            this.PuertoOrigen_CB.DataSource = this.Datos_PuertoOrigen;
            this.PuertoOrigen_CB.DisplayMember = "Valor";
            this.PuertoOrigen_CB.Location = new System.Drawing.Point(113, 33);
            this.PuertoOrigen_CB.Name = "PuertoOrigen_CB";
            this.PuertoOrigen_CB.Size = new System.Drawing.Size(240, 21);
            this.PuertoOrigen_CB.TabIndex = 0;
            this.PuertoOrigen_CB.ValueMember = "Valor";
            // 
            // Datos_PuertoOrigen
            // 
            this.Datos_PuertoOrigen.DataSource = typeof(moleQule.Library.Store.PuertoInfo);
            // 
            // precioNumericTextBox
            // 
            this.precioNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.precioNumericTextBox.Location = new System.Drawing.Point(113, 87);
            this.precioNumericTextBox.Name = "precioNumericTextBox";
            this.precioNumericTextBox.Size = new System.Drawing.Size(100, 21);
            this.precioNumericTextBox.TabIndex = 5;
            this.precioNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.precioNumericTextBox.TextIsCurrency = false;
            this.precioNumericTextBox.TextIsInteger = false;
            // 
            // proveedorTextBox
            // 
            this.proveedorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Proveedor", true));
            this.proveedorTextBox.Location = new System.Drawing.Point(113, 60);
            this.proveedorTextBox.Name = "proveedorTextBox";
            this.proveedorTextBox.ReadOnly = true;
            this.proveedorTextBox.Size = new System.Drawing.Size(240, 21);
            this.proveedorTextBox.TabIndex = 6;
            // 
            // Proveedor_BT
            // 
            this.Proveedor_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Proveedor_BT.Location = new System.Drawing.Point(359, 59);
            this.Proveedor_BT.Name = "Proveedor_BT";
            this.Proveedor_BT.Size = new System.Drawing.Size(29, 21);
            this.Proveedor_BT.TabIndex = 7;
            this.Proveedor_BT.UseVisualStyleBackColor = true;
            this.Proveedor_BT.Click += new System.EventHandler(this.Proveedor_BT_Click);
            // 
            // PrecioOrigenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(484, 256);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "PrecioOrigenForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Puerto de Origen";
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoOrigen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox PuertoOrigen_CB;
        private System.Windows.Forms.BindingSource Datos_PuertoOrigen;
        private System.Windows.Forms.TextBox proveedorTextBox;
        private moleQule.Face.Controls.NumericTextBox precioNumericTextBox;
        protected System.Windows.Forms.Button Proveedor_BT;

    }
}
