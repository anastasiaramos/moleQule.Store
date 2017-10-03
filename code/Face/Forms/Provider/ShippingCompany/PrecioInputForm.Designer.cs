namespace moleQule.Face.Store
{
    partial class PrecioTrayectoForm
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
            System.Windows.Forms.Label precioLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.PuertoOrigen_CB = new System.Windows.Forms.ComboBox();
            this.Datos_PuertoOrigen = new System.Windows.Forms.BindingSource(this.components);
            this.PuertoDestino_CB = new System.Windows.Forms.ComboBox();
            this.Datos_PuertoDestino = new System.Windows.Forms.BindingSource(this.components);
            this.Precio_NTB = new moleQule.Face.Controls.NumericTextBox();
            precioLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.Source_GB.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoOrigen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoDestino)).BeginInit();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.PrecioTrayecto);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(145, 7);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(235, 7);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(label2);
            this.Source_GB.Controls.Add(label1);
            this.Source_GB.Controls.Add(precioLabel);
            this.Source_GB.Controls.Add(this.Precio_NTB);
            this.Source_GB.Controls.Add(this.PuertoDestino_CB);
            this.Source_GB.Controls.Add(this.PuertoOrigen_CB);
            this.Source_GB.Location = new System.Drawing.Point(11, 11);
            this.Source_GB.Size = new System.Drawing.Size(444, 164);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Size = new System.Drawing.Size(468, 232);
            this.PanelesV.SplitterDistance = 192;
            // 
            // precioLabel
            // 
            precioLabel.AutoSize = true;
            precioLabel.Location = new System.Drawing.Point(138, 105);
            precioLabel.Name = "precioLabel";
            precioLabel.Size = new System.Drawing.Size(45, 13);
            precioLabel.TabIndex = 2;
            precioLabel.Text = "Precio:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(138, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(88, 13);
            label1.TabIndex = 4;
            label1.Text = "Puerto Origen:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(138, 61);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(94, 13);
            label2.TabIndex = 5;
            label2.Text = "Puerto Destino:";
            // 
            // PuertoOrigen_CB
            // 
            this.PuertoOrigen_CB.DataSource = this.Datos_PuertoOrigen;
            this.PuertoOrigen_CB.DisplayMember = "Valor";
            this.PuertoOrigen_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PuertoOrigen_CB.Location = new System.Drawing.Point(141, 35);
            this.PuertoOrigen_CB.Name = "PuertoOrigen_CB";
            this.PuertoOrigen_CB.Size = new System.Drawing.Size(165, 21);
            this.PuertoOrigen_CB.TabIndex = 0;
            this.PuertoOrigen_CB.ValueMember = "Valor";
            // 
            // Datos_PuertoOrigen
            // 
            this.Datos_PuertoOrigen.DataSource = typeof(moleQule.Library.Store.PuertoInfo);
            // 
            // PuertoDestino_CB
            // 
            this.PuertoDestino_CB.DataSource = this.Datos_PuertoDestino;
            this.PuertoDestino_CB.DisplayMember = "Valor";
            this.PuertoDestino_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PuertoDestino_CB.Location = new System.Drawing.Point(141, 79);
            this.PuertoDestino_CB.Name = "PuertoDestino_CB";
            this.PuertoDestino_CB.Size = new System.Drawing.Size(165, 21);
            this.PuertoDestino_CB.TabIndex = 1;
            this.PuertoDestino_CB.ValueMember = "Valor";
            // 
            // Datos_PuertoDestino
            // 
            this.Datos_PuertoDestino.DataSource = typeof(moleQule.Library.Store.PuertoInfo);
            // 
            // Precio_NTB
            // 
            this.Precio_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true));
            this.Precio_NTB.Location = new System.Drawing.Point(141, 123);
            this.Precio_NTB.Name = "Precio_NTB";
            this.Precio_NTB.Size = new System.Drawing.Size(100, 21);
            this.Precio_NTB.TabIndex = 3;
            this.Precio_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Precio_NTB.TextIsInteger = false;
            // 
            // PrecioTrayectoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(468, 232);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "PrecioTrayectoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoOrigen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PuertoDestino)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private moleQule.Face.Controls.NumericTextBox Precio_NTB;
        private System.Windows.Forms.ComboBox PuertoDestino_CB;
        private System.Windows.Forms.ComboBox PuertoOrigen_CB;
        private System.Windows.Forms.BindingSource Datos_PuertoOrigen;
        private System.Windows.Forms.BindingSource Datos_PuertoDestino;

    }
}
