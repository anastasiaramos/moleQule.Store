namespace moleQule.Face.Store
{
    partial class InventarioValoradoActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventarioValoradoActionForm));
            this.Datos_Expedientes = new System.Windows.Forms.BindingSource(this.components);
            this.Proveedor_GB = new System.Windows.Forms.GroupBox();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TipoExpediente_CB = new System.Windows.Forms.ComboBox();
            this.Datos_Tipos = new System.Windows.Forms.BindingSource(this.components);
            this.TodosExpediente_CkB = new System.Windows.Forms.CheckBox();
            this.Producto_GB = new System.Windows.Forms.GroupBox();
            this.Producto_TB = new System.Windows.Forms.TextBox();
            this.Producto_BT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Stock_CkB = new System.Windows.Forms.CheckBox();
            this.TodosProducto_CkB = new System.Windows.Forms.CheckBox();
            this.Datos_Productos = new System.Windows.Forms.BindingSource(this.components);
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expedientes)).BeginInit();
            this.Proveedor_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipos)).BeginInit();
            this.Producto_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_BT
            // 
            this.Print_BT.Enabled = true;
            this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Print_BT.Location = new System.Drawing.Point(381, 2);
            this.HelpProvider.SetShowHelp(this.Print_BT, true);
            this.Print_BT.Size = new System.Drawing.Size(108, 32);
            this.Print_BT.Text = "Vista &Previa";
            this.Print_BT.Visible = true;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(267, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Size = new System.Drawing.Size(108, 32);
            this.Submit_BT.Visible = false;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(153, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.Size = new System.Drawing.Size(108, 32);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.FFinal_DTP);
            this.Source_GB.Controls.Add(this.label3);
            this.Source_GB.Controls.Add(this.Proveedor_GB);
            this.Source_GB.Controls.Add(this.Producto_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(642, 310);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(644, 352);
            this.PanelesV.SplitterDistance = 312;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(118, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(644, 352);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(290, 227);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(290, 142);
            // 
            // Proveedor_GB
            // 
            this.Proveedor_GB.Controls.Add(this.Expediente_TB);
            this.Proveedor_GB.Controls.Add(this.Expediente_BT);
            this.Proveedor_GB.Controls.Add(this.label2);
            this.Proveedor_GB.Controls.Add(this.label1);
            this.Proveedor_GB.Controls.Add(this.TipoExpediente_CB);
            this.Proveedor_GB.Controls.Add(this.TodosExpediente_CkB);
            this.Proveedor_GB.Location = new System.Drawing.Point(36, 146);
            this.Proveedor_GB.Name = "Proveedor_GB";
            this.Proveedor_GB.Size = new System.Drawing.Size(571, 84);
            this.Proveedor_GB.TabIndex = 25;
            this.Proveedor_GB.TabStop = false;
            this.Proveedor_GB.Text = "Expediente";
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.Location = new System.Drawing.Point(86, 22);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(290, 21);
            this.Expediente_TB.TabIndex = 28;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Enabled = false;
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(382, 21);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(42, 23);
            this.Expediente_BT.TabIndex = 27;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Selección:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Tipo de Expediente:";
            // 
            // TipoExpediente_CB
            // 
            this.TipoExpediente_CB.DataSource = this.Datos_Tipos;
            this.TipoExpediente_CB.DisplayMember = "Texto";
            this.TipoExpediente_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoExpediente_CB.FormattingEnabled = true;
            this.TipoExpediente_CB.Location = new System.Drawing.Point(229, 54);
            this.TipoExpediente_CB.Name = "TipoExpediente_CB";
            this.TipoExpediente_CB.Size = new System.Drawing.Size(223, 21);
            this.TipoExpediente_CB.TabIndex = 16;
            this.TipoExpediente_CB.ValueMember = "Oid";
            // 
            // Datos_Tipos
            // 
            this.Datos_Tipos.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // TodosExpediente_CkB
            // 
            this.TodosExpediente_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosExpediente_CkB.AutoSize = true;
            this.TodosExpediente_CkB.Checked = true;
            this.TodosExpediente_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosExpediente_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosExpediente_CkB.Location = new System.Drawing.Point(456, 24);
            this.TodosExpediente_CkB.Name = "TodosExpediente_CkB";
            this.TodosExpediente_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosExpediente_CkB.TabIndex = 13;
            this.TodosExpediente_CkB.Text = "Mostrar Todos";
            this.TodosExpediente_CkB.UseVisualStyleBackColor = true;
            this.TodosExpediente_CkB.CheckedChanged += new System.EventHandler(this.TodosProveedor_CkB_CheckedChanged);
            // 
            // Producto_GB
            // 
            this.Producto_GB.Controls.Add(this.Producto_TB);
            this.Producto_GB.Controls.Add(this.Producto_BT);
            this.Producto_GB.Controls.Add(this.label4);
            this.Producto_GB.Controls.Add(this.Stock_CkB);
            this.Producto_GB.Controls.Add(this.TodosProducto_CkB);
            this.Producto_GB.Location = new System.Drawing.Point(36, 31);
            this.Producto_GB.Name = "Producto_GB";
            this.Producto_GB.Size = new System.Drawing.Size(571, 86);
            this.Producto_GB.TabIndex = 24;
            this.Producto_GB.TabStop = false;
            this.Producto_GB.Text = "Producto";
            // 
            // Producto_TB
            // 
            this.Producto_TB.Location = new System.Drawing.Point(86, 19);
            this.Producto_TB.Name = "Producto_TB";
            this.Producto_TB.ReadOnly = true;
            this.Producto_TB.Size = new System.Drawing.Size(290, 21);
            this.Producto_TB.TabIndex = 24;
            // 
            // Producto_BT
            // 
            this.Producto_BT.Enabled = false;
            this.Producto_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Producto_BT.Location = new System.Drawing.Point(382, 18);
            this.Producto_BT.Name = "Producto_BT";
            this.Producto_BT.Size = new System.Drawing.Size(42, 23);
            this.Producto_BT.TabIndex = 23;
            this.Producto_BT.UseVisualStyleBackColor = true;
            this.Producto_BT.Click += new System.EventHandler(this.Producto_BT_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Selección:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Stock_CkB
            // 
            this.Stock_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Stock_CkB.AutoSize = true;
            this.Stock_CkB.Enabled = false;
            this.Stock_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stock_CkB.Location = new System.Drawing.Point(215, 52);
            this.Stock_CkB.Name = "Stock_CkB";
            this.Stock_CkB.Size = new System.Drawing.Size(141, 17);
            this.Stock_CkB.TabIndex = 15;
            this.Stock_CkB.Text = "Solo Productos en Stock";
            this.Stock_CkB.UseVisualStyleBackColor = true;
            this.Stock_CkB.Visible = false;
            // 
            // TodosProducto_CkB
            // 
            this.TodosProducto_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosProducto_CkB.AutoSize = true;
            this.TodosProducto_CkB.Checked = true;
            this.TodosProducto_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosProducto_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosProducto_CkB.Location = new System.Drawing.Point(456, 21);
            this.TodosProducto_CkB.Name = "TodosProducto_CkB";
            this.TodosProducto_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosProducto_CkB.TabIndex = 14;
            this.TodosProducto_CkB.Text = "Mostrar Todos";
            this.TodosProducto_CkB.UseVisualStyleBackColor = true;
            this.TodosProducto_CkB.CheckedChanged += new System.EventHandler(this.TodosProducto_CkB_CheckedChanged);
            // 
            // FFinal_DTP
            // 
            this.FFinal_DTP.Checked = false;
            this.FFinal_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFinal_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFinal_DTP.Location = new System.Drawing.Point(288, 258);
            this.FFinal_DTP.Name = "FFinal_DTP";
            this.FFinal_DTP.ShowCheckBox = true;
            this.FFinal_DTP.Size = new System.Drawing.Size(112, 21);
            this.FFinal_DTP.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(242, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Fecha:";
            // 
            // InventarioValoradoActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(644, 352);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InventarioValoradoActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Inventario Valorado";
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expedientes)).EndInit();
            this.Proveedor_GB.ResumeLayout(false);
            this.Proveedor_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipos)).EndInit();
            this.Producto_GB.ResumeLayout(false);
            this.Producto_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Datos_Expedientes;
        private System.Windows.Forms.GroupBox Proveedor_GB;
        private System.Windows.Forms.CheckBox TodosExpediente_CkB;
        private System.Windows.Forms.GroupBox Producto_GB;
        private System.Windows.Forms.CheckBox TodosProducto_CkB;
        private System.Windows.Forms.BindingSource Datos_Productos;
        private moleQule.Face.Controls.mQDateTimePicker FFinal_DTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TipoExpediente_CB;
        private System.Windows.Forms.BindingSource Datos_Tipos;
        private System.Windows.Forms.CheckBox Stock_CkB;
        private System.Windows.Forms.TextBox Producto_TB;
        private System.Windows.Forms.Button Producto_BT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Expediente_TB;
        private System.Windows.Forms.Button Expediente_BT;
        private System.Windows.Forms.Label label2;
    }
}
