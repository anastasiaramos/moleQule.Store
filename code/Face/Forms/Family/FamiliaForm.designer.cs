namespace moleQule.Face.Store
{
    partial class FamiliaForm
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
            System.Windows.Forms.Label identificadorLabel;
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolForm));
            this.identificadorTextBox = new System.Windows.Forms.TextBox();
            this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.Generales_GB = new System.Windows.Forms.GroupBox();
            this.Observaciones_GB = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CuentaContableVenta_TB = new System.Windows.Forms.MaskedTextBox();
            this.CuentaContableCompra_TB = new System.Windows.Forms.MaskedTextBox();
            this.CuentaContableVenta_BT = new System.Windows.Forms.Button();
            this.CuentaContableCompra_BT = new System.Windows.Forms.Button();
            this.DefectoVenta_BT = new System.Windows.Forms.Button();
            this.Impuesto_BT = new System.Windows.Forms.Button();
            this.Impuesto_TB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PBeneficioMinimo_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            identificadorLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.Generales_GB.SuspendLayout();
            this.Observaciones_GB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.groupBox1);
            this.PanelesV.Panel1.Controls.Add(this.groupBox2);
            this.PanelesV.Panel1.Controls.Add(this.Observaciones_GB);
            this.PanelesV.Panel1.Controls.Add(this.Generales_GB);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(634, 554);
            this.PanelesV.SplitterDistance = 499;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(217, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(306, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Paneles2
            // 
            // 
            // Paneles2.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
            // 
            // Paneles2.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Paneles2, true);
            this.Paneles2.Size = new System.Drawing.Size(632, 52);
            this.Paneles2.SplitterDistance = 27;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(814, 7);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            this.Imprimir_Button.Size = new System.Drawing.Size(87, 23);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(300, 6);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Serie.Familia);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(138, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(634, 554);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(280, 325);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(280, 240);
            // 
            // identificadorLabel
            // 
            identificadorLabel.AutoSize = true;
            identificadorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            identificadorLabel.Location = new System.Drawing.Point(31, 38);
            identificadorLabel.Name = "identificadorLabel";
            identificadorLabel.Size = new System.Drawing.Size(53, 13);
            identificadorLabel.TabIndex = 2;
            identificadorLabel.Text = "Código *:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(185, 38);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 4;
            nombreLabel.Text = "Nombre:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label16.Location = new System.Drawing.Point(20, 41);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(132, 13);
            label16.TabIndex = 125;
            label16.Text = "Cuenta Contable Compra:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(340, 19);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(56, 13);
            label4.TabIndex = 128;
            label4.Text = "Impuesto:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(29, 73);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(123, 13);
            label1.TabIndex = 147;
            label1.Text = "Cuenta Contable Venta:";
            // 
            // identificadorTextBox
            // 
            this.identificadorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.identificadorTextBox.Location = new System.Drawing.Point(90, 35);
            this.identificadorTextBox.Name = "identificadorTextBox";
            this.identificadorTextBox.Size = new System.Drawing.Size(67, 21);
            this.identificadorTextBox.TabIndex = 3;
            this.identificadorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // observacionesRichTextBox
            // 
            this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.observacionesRichTextBox.Location = new System.Drawing.Point(22, 29);
            this.observacionesRichTextBox.Name = "observacionesRichTextBox";
            this.observacionesRichTextBox.Size = new System.Drawing.Size(526, 96);
            this.observacionesRichTextBox.TabIndex = 7;
            this.observacionesRichTextBox.Text = "";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
            this.nombreTextBox.Location = new System.Drawing.Point(239, 35);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(296, 21);
            this.nombreTextBox.TabIndex = 5;
            // 
            // Generales_GB
            // 
            this.Generales_GB.Controls.Add(this.identificadorTextBox);
            this.Generales_GB.Controls.Add(identificadorLabel);
            this.Generales_GB.Controls.Add(this.nombreTextBox);
            this.Generales_GB.Controls.Add(nombreLabel);
            this.Generales_GB.Location = new System.Drawing.Point(33, 25);
            this.Generales_GB.Name = "Generales_GB";
            this.Generales_GB.Size = new System.Drawing.Size(566, 76);
            this.Generales_GB.TabIndex = 8;
            this.Generales_GB.TabStop = false;
            this.Generales_GB.Text = "Datos Generales";
            // 
            // Observaciones_GB
            // 
            this.Observaciones_GB.Controls.Add(this.observacionesRichTextBox);
            this.Observaciones_GB.Location = new System.Drawing.Point(33, 329);
            this.Observaciones_GB.Name = "Observaciones_GB";
            this.Observaciones_GB.Size = new System.Drawing.Size(566, 145);
            this.Observaciones_GB.TabIndex = 9;
            this.Observaciones_GB.TabStop = false;
            this.Observaciones_GB.Text = "Observaciones";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CuentaContableVenta_TB);
            this.groupBox2.Controls.Add(this.CuentaContableCompra_TB);
            this.groupBox2.Controls.Add(this.CuentaContableVenta_BT);
            this.groupBox2.Controls.Add(this.CuentaContableCompra_BT);
            this.groupBox2.Controls.Add(label1);
            this.groupBox2.Controls.Add(this.DefectoVenta_BT);
            this.groupBox2.Controls.Add(this.Impuesto_BT);
            this.groupBox2.Controls.Add(label4);
            this.groupBox2.Controls.Add(this.Impuesto_TB);
            this.groupBox2.Controls.Add(label16);
            this.groupBox2.Location = new System.Drawing.Point(33, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(566, 108);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Financieros / Contables";
            // 
            // CuentaContableVenta_TB
            // 
            this.CuentaContableVenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContableVenta", true));
            this.CuentaContableVenta_TB.Location = new System.Drawing.Point(158, 68);
            this.CuentaContableVenta_TB.Name = "CuentaContableVenta_TB";
            this.CuentaContableVenta_TB.Size = new System.Drawing.Size(126, 21);
            this.CuentaContableVenta_TB.TabIndex = 171;
            this.CuentaContableVenta_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CuentaContableCompra_TB
            // 
            this.CuentaContableCompra_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContableCompra", true));
            this.CuentaContableCompra_TB.Location = new System.Drawing.Point(158, 37);
            this.CuentaContableCompra_TB.Name = "CuentaContableCompra_TB";
            this.CuentaContableCompra_TB.Size = new System.Drawing.Size(126, 21);
            this.CuentaContableCompra_TB.TabIndex = 170;
            this.CuentaContableCompra_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CuentaContableVenta_BT
            // 
            this.CuentaContableVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.CuentaContableVenta_BT.Location = new System.Drawing.Point(294, 68);
            this.CuentaContableVenta_BT.Name = "CuentaContableVenta_BT";
            this.CuentaContableVenta_BT.Size = new System.Drawing.Size(29, 21);
            this.CuentaContableVenta_BT.TabIndex = 169;
            this.CuentaContableVenta_BT.UseVisualStyleBackColor = true;
            // 
            // CuentaContableCompra_BT
            // 
            this.CuentaContableCompra_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.CuentaContableCompra_BT.Location = new System.Drawing.Point(294, 37);
            this.CuentaContableCompra_BT.Name = "CuentaContableCompra_BT";
            this.CuentaContableCompra_BT.Size = new System.Drawing.Size(29, 21);
            this.CuentaContableCompra_BT.TabIndex = 168;
            this.CuentaContableCompra_BT.UseVisualStyleBackColor = true;
            // 
            // DefectoVenta_BT
            // 
            this.DefectoVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.DefectoVenta_BT.Location = new System.Drawing.Point(518, 37);
            this.DefectoVenta_BT.Name = "DefectoVenta_BT";
            this.DefectoVenta_BT.Size = new System.Drawing.Size(29, 21);
            this.DefectoVenta_BT.TabIndex = 145;
            this.DefectoVenta_BT.UseVisualStyleBackColor = true;
            // 
            // Impuesto_BT
            // 
            this.Impuesto_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Impuesto_BT.Location = new System.Drawing.Point(483, 37);
            this.Impuesto_BT.Name = "Impuesto_BT";
            this.Impuesto_BT.Size = new System.Drawing.Size(29, 21);
            this.Impuesto_BT.TabIndex = 133;
            this.Impuesto_BT.UseVisualStyleBackColor = true;
            // 
            // Impuesto_TB
            // 
            this.Impuesto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuesto", true));
            this.Impuesto_TB.Location = new System.Drawing.Point(337, 37);
            this.Impuesto_TB.Name = "Impuesto_TB";
            this.Impuesto_TB.ReadOnly = true;
            this.Impuesto_TB.Size = new System.Drawing.Size(140, 21);
            this.Impuesto_TB.TabIndex = 127;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PBeneficioMinimo_NTB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(33, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Facturación";
            // 
            // PBeneficioMinimo_NTB
            // 
            this.PBeneficioMinimo_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PBeneficioMinimo", true));
            this.PBeneficioMinimo_NTB.Location = new System.Drawing.Point(146, 31);
            this.PBeneficioMinimo_NTB.Name = "PBeneficioMinimo_NTB";
            this.PBeneficioMinimo_NTB.Size = new System.Drawing.Size(59, 21);
            this.PBeneficioMinimo_NTB.TabIndex = 4;
            this.PBeneficioMinimo_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PBeneficioMinimo_NTB.TextIsCurrency = false;
            this.PBeneficioMinimo_NTB.TextIsInteger = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Beneficio Mínimo (%):";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.Datos, "AvisarBeneficioMinimo", true));
            this.checkBox1.Location = new System.Drawing.Point(239, 33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Avisar Beneficio Mínimo";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FamiliaForm
            // 
            this.ClientSize = new System.Drawing.Size(634, 554);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FamiliaForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "FamiliaForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.Generales_GB.ResumeLayout(false);
            this.Generales_GB.PerformLayout();
            this.Observaciones_GB.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridViewTextBoxColumn oidFamiliaDataGridViewTextBoxColumn;
        protected System.Windows.Forms.RichTextBox observacionesRichTextBox;
        protected System.Windows.Forms.TextBox identificadorTextBox;
        protected System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.GroupBox Generales_GB;
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox Observaciones_GB;
        protected System.Windows.Forms.TextBox Impuesto_TB;
        protected System.Windows.Forms.Button Impuesto_BT;
		protected System.Windows.Forms.Button DefectoVenta_BT;
		protected System.Windows.Forms.Button CuentaContableCompra_BT;
		protected System.Windows.Forms.Button CuentaContableVenta_BT;
		protected System.Windows.Forms.MaskedTextBox CuentaContableVenta_TB;
		protected System.Windows.Forms.MaskedTextBox CuentaContableCompra_TB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private Controls.NumericTextBox PBeneficioMinimo_NTB;

    }
}
