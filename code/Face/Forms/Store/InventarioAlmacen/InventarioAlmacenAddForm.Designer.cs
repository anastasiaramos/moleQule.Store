

namespace moleQule.Face.Store
{
    partial class InventarioAlmacenAddForm
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
            this.CrearInventario_B = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Almacen)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.SuspendLayout();
            // 
            // observacionesTextBox
            // 
            this.observacionesTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.observacionesTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.fechaDateTimePicker.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.ShowCheckBox = true;
            this.fechaDateTimePicker.Size = new System.Drawing.Size(100, 21);
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.nombreTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CrearInventario_B);
            this.splitContainer1.Size = new System.Drawing.Size(590, 223);
            // 
            // PanelesV
            // 
            this.PanelesV.SplitterDistance = 225;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(252, 7);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(348, 7);
            // 
            // Paneles2
            // 
            this.Paneles2.Size = new System.Drawing.Size(590, 38);
            this.Paneles2.SplitterDistance = 37;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Location = new System.Drawing.Point(156, 7);
            // 
            // CrearInventario_B
            // 
            this.CrearInventario_B.Location = new System.Drawing.Point(200, 63);
            this.CrearInventario_B.Name = "CrearInventario_B";
            this.CrearInventario_B.Size = new System.Drawing.Size(112, 23);
            this.CrearInventario_B.TabIndex = 8;
            this.CrearInventario_B.Text = "Crear Inventario";
            this.CrearInventario_B.UseVisualStyleBackColor = true;
            this.CrearInventario_B.Click += new System.EventHandler(this.CrearInventario_B_Click);
            // 
            // InventarioAlmacenAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "InventarioAlmacenAddForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "InventarioAlmacenAddForm";
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Almacen)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CrearInventario_B;
	}
}
