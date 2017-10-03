namespace moleQule.Face.Store
{
    partial class PrecioDestinoForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label precioLabel;
            System.Windows.Forms.Label nombreClienteLabel;
            this.PuertoDestino_CB = new System.Windows.Forms.ComboBox();
            this.Datos_PuertoDestino = new System.Windows.Forms.BindingSource(this.components);
            this.precioNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
            this.nombreClienteTextBox = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            precioLabel = new System.Windows.Forms.Label();
            nombreClienteLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.Source_GB.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoDestino)).BeginInit();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.PrecioDestino);
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
            this.Source_GB.Controls.Add(this.Cliente_BT);
            this.Source_GB.Controls.Add(nombreClienteLabel);
            this.Source_GB.Controls.Add(this.nombreClienteTextBox);
            this.Source_GB.Controls.Add(precioLabel);
            this.Source_GB.Controls.Add(this.precioNumericTextBox);
            this.Source_GB.Controls.Add(label2);
            this.Source_GB.Controls.Add(this.PuertoDestino_CB);
            this.Source_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source_GB.Location = new System.Drawing.Point(48, 44);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(410, 126);
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(24, 29);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 13);
            label2.TabIndex = 5;
            label2.Text = "Puerto Destino:";
            // 
            // precioLabel
            // 
            precioLabel.AutoSize = true;
            precioLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            precioLabel.Location = new System.Drawing.Point(66, 83);
            precioLabel.Name = "precioLabel";
            precioLabel.Size = new System.Drawing.Size(40, 13);
            precioLabel.TabIndex = 6;
            precioLabel.Text = "Precio:";
            // 
            // nombreClienteLabel
            // 
            nombreClienteLabel.AutoSize = true;
            nombreClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreClienteLabel.Location = new System.Drawing.Point(62, 56);
            nombreClienteLabel.Name = "nombreClienteLabel";
            nombreClienteLabel.Size = new System.Drawing.Size(44, 13);
            nombreClienteLabel.TabIndex = 7;
            nombreClienteLabel.Text = "Cliente:";
            // 
            // PuertoDestino_CB
            // 
            this.PuertoDestino_CB.DataSource = this.Datos_PuertoDestino;
            this.PuertoDestino_CB.DisplayMember = "Valor";
            this.PuertoDestino_CB.Location = new System.Drawing.Point(112, 26);
            this.PuertoDestino_CB.Name = "PuertoDestino_CB";
            this.PuertoDestino_CB.Size = new System.Drawing.Size(240, 21);
            this.PuertoDestino_CB.TabIndex = 1;
            this.PuertoDestino_CB.ValueMember = "Valor";
            // 
            // Datos_PuertoDestino
            // 
            this.Datos_PuertoDestino.DataSource = typeof(moleQule.Library.Store.PuertoInfo);
            // 
            // precioNumericTextBox
            // 
            this.precioNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.precioNumericTextBox.Location = new System.Drawing.Point(112, 80);
            this.precioNumericTextBox.Name = "precioNumericTextBox";
            this.precioNumericTextBox.Size = new System.Drawing.Size(100, 21);
            this.precioNumericTextBox.TabIndex = 7;
            this.precioNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.precioNumericTextBox.TextIsCurrency = false;
            this.precioNumericTextBox.TextIsInteger = false;
            // 
            // nombreClienteTextBox
            // 
            this.nombreClienteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "NombreCliente", true));
            this.nombreClienteTextBox.Location = new System.Drawing.Point(112, 53);
            this.nombreClienteTextBox.Name = "nombreClienteTextBox";
            this.nombreClienteTextBox.ReadOnly = true;
            this.nombreClienteTextBox.Size = new System.Drawing.Size(240, 21);
            this.nombreClienteTextBox.TabIndex = 8;
            // 
            // Cliente_BT
            // 
            this.Cliente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Cliente_BT.Location = new System.Drawing.Point(358, 53);
            this.Cliente_BT.Name = "Cliente_BT";
            this.Cliente_BT.Size = new System.Drawing.Size(29, 21);
            this.Cliente_BT.TabIndex = 9;
            this.Cliente_BT.UseVisualStyleBackColor = true;
            this.Cliente_BT.Click += new System.EventHandler(this.Cliente_BT_Click);
            // 
            // PrecioDestinoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(484, 256);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "PrecioDestinoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Puerto de Destino";
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoDestino)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox PuertoDestino_CB;
        private System.Windows.Forms.BindingSource Datos_PuertoDestino;
        private System.Windows.Forms.TextBox nombreClienteTextBox;
        private moleQule.Face.Controls.NumericTextBox precioNumericTextBox;
        protected System.Windows.Forms.Button Cliente_BT;

    }
}
