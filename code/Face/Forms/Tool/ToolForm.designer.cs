namespace moleQule.Face.Store
{
    partial class ToolForm
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label label5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolForm));
            this.Code_TB = new System.Windows.Forms.TextBox();
            this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.Generales_GB = new System.Windows.Forms.GroupBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Observaciones_GB = new System.Windows.Forms.GroupBox();
            this.Statistics_GB = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cost_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Till_DTP = new System.Windows.Forms.DateTimePicker();
            this.From_DTP = new System.Windows.Forms.DateTimePicker();
            this.Lcoation_TB = new System.Windows.Forms.TextBox();
            this.Description_TB = new System.Windows.Forms.TextBox();
            identificadorLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
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
            this.Statistics_GB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.groupBox2);
            this.PanelesV.Panel1.Controls.Add(this.Statistics_GB);
            this.PanelesV.Panel1.Controls.Add(this.Observaciones_GB);
            this.PanelesV.Panel1.Controls.Add(this.Generales_GB);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(634, 672);
            this.PanelesV.SplitterDistance = 617;
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
            this.ErrorMng_EP.SetError(this.Paneles2, "F1 Ayuda        ");
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
            this.Datos.DataSource = typeof(moleQule.Library.Store.Tool);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(138, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(634, 672);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(280, 384);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(280, 299);
            // 
            // identificadorLabel
            // 
            identificadorLabel.AutoSize = true;
            identificadorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            identificadorLabel.Location = new System.Drawing.Point(25, 34);
            identificadorLabel.Name = "identificadorLabel";
            identificadorLabel.Size = new System.Drawing.Size(53, 13);
            identificadorLabel.TabIndex = 2;
            identificadorLabel.Text = "Código *:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(30, 61);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 4;
            nombreLabel.Text = "Nombre:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(18, 25);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(65, 13);
            label4.TabIndex = 4;
            label4.Text = "Descripción:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(18, 112);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 13);
            label3.TabIndex = 6;
            label3.Text = "Ubicación:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(257, 34);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(44, 13);
            label10.TabIndex = 167;
            label10.Text = "Estado:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(24, 210);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(62, 13);
            fechaLabel.TabIndex = 10;
            fechaLabel.Text = "Fecha Alta:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(334, 210);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(64, 13);
            label5.TabIndex = 12;
            label5.Text = "Fecha Baja:";
            // 
            // Code_TB
            // 
            this.Code_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ID", true));
            this.Code_TB.Location = new System.Drawing.Point(84, 30);
            this.Code_TB.Name = "Code_TB";
            this.Code_TB.ReadOnly = true;
            this.Code_TB.Size = new System.Drawing.Size(67, 21);
            this.Code_TB.TabIndex = 3;
            this.Code_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // observacionesRichTextBox
            // 
            this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Comments", true));
            this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.observacionesRichTextBox.Location = new System.Drawing.Point(20, 29);
            this.observacionesRichTextBox.Name = "observacionesRichTextBox";
            this.observacionesRichTextBox.Size = new System.Drawing.Size(526, 96);
            this.observacionesRichTextBox.TabIndex = 7;
            this.observacionesRichTextBox.Text = "";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Name", true));
            this.nombreTextBox.Location = new System.Drawing.Point(84, 58);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(457, 21);
            this.nombreTextBox.TabIndex = 5;
            // 
            // Generales_GB
            // 
            this.Generales_GB.Controls.Add(this.Estado_BT);
            this.Generales_GB.Controls.Add(label10);
            this.Generales_GB.Controls.Add(this.Estado_TB);
            this.Generales_GB.Controls.Add(this.Code_TB);
            this.Generales_GB.Controls.Add(identificadorLabel);
            this.Generales_GB.Controls.Add(this.nombreTextBox);
            this.Generales_GB.Controls.Add(nombreLabel);
            this.Generales_GB.Location = new System.Drawing.Point(33, 13);
            this.Generales_GB.Name = "Generales_GB";
            this.Generales_GB.Size = new System.Drawing.Size(566, 94);
            this.Generales_GB.TabIndex = 8;
            this.Generales_GB.TabStop = false;
            this.Generales_GB.Text = "Datos Generales";
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(457, 29);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(29, 22);
            this.Estado_BT.TabIndex = 168;
            this.Estado_BT.UseVisualStyleBackColor = true;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "StatusLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(307, 30);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(143, 21);
            this.Estado_TB.TabIndex = 166;
            this.Estado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Observaciones_GB
            // 
            this.Observaciones_GB.Controls.Add(this.observacionesRichTextBox);
            this.Observaciones_GB.Location = new System.Drawing.Point(33, 456);
            this.Observaciones_GB.Name = "Observaciones_GB";
            this.Observaciones_GB.Size = new System.Drawing.Size(566, 145);
            this.Observaciones_GB.TabIndex = 9;
            this.Observaciones_GB.TabStop = false;
            this.Observaciones_GB.Text = "Observaciones";
            // 
            // Statistics_GB
            // 
            this.Statistics_GB.Controls.Add(this.label1);
            this.Statistics_GB.Controls.Add(this.Cost_NTB);
            this.Statistics_GB.Controls.Add(this.label2);
            this.Statistics_GB.Location = new System.Drawing.Point(33, 374);
            this.Statistics_GB.Name = "Statistics_GB";
            this.Statistics_GB.Size = new System.Drawing.Size(566, 76);
            this.Statistics_GB.TabIndex = 9;
            this.Statistics_GB.TabStop = false;
            this.Statistics_GB.Text = "Estadísticas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "(por hora)";
            // 
            // Cost_NTB
            // 
            this.Cost_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Cost", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Cost_NTB.Location = new System.Drawing.Point(74, 31);
            this.Cost_NTB.Name = "Cost_NTB";
            this.Cost_NTB.Size = new System.Drawing.Size(83, 21);
            this.Cost_NTB.TabIndex = 4;
            this.Cost_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Cost_NTB.TextIsCurrency = false;
            this.Cost_NTB.TextIsInteger = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Coste:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Till_DTP);
            this.groupBox2.Controls.Add(label5);
            this.groupBox2.Controls.Add(this.From_DTP);
            this.groupBox2.Controls.Add(fechaLabel);
            this.groupBox2.Controls.Add(this.Lcoation_TB);
            this.groupBox2.Controls.Add(label3);
            this.groupBox2.Controls.Add(this.Description_TB);
            this.groupBox2.Controls.Add(label4);
            this.groupBox2.Location = new System.Drawing.Point(33, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(566, 255);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descripción";
            // 
            // Till_DTP
            // 
            this.Till_DTP.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Till_DTP.CalendarTitleForeColor = System.Drawing.Color.Navy;
            this.Till_DTP.Checked = false;
            this.Till_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Till_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Till", true));
            this.Till_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Till_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Till_DTP.Location = new System.Drawing.Point(404, 206);
            this.Till_DTP.Name = "Till_DTP";
            this.Till_DTP.Size = new System.Drawing.Size(142, 21);
            this.Till_DTP.TabIndex = 11;
            // 
            // From_DTP
            // 
            this.From_DTP.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.From_DTP.CalendarTitleForeColor = System.Drawing.Color.Navy;
            this.From_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.From_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "From", true));
            this.From_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.From_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.From_DTP.Location = new System.Drawing.Point(92, 206);
            this.From_DTP.Name = "From_DTP";
            this.From_DTP.Size = new System.Drawing.Size(142, 21);
            this.From_DTP.TabIndex = 9;
            // 
            // Lcoation_TB
            // 
            this.Lcoation_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Location", true));
            this.Lcoation_TB.Location = new System.Drawing.Point(21, 132);
            this.Lcoation_TB.Multiline = true;
            this.Lcoation_TB.Name = "Lcoation_TB";
            this.Lcoation_TB.Size = new System.Drawing.Size(525, 57);
            this.Lcoation_TB.TabIndex = 7;
            // 
            // Description_TB
            // 
            this.Description_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Description", true));
            this.Description_TB.Location = new System.Drawing.Point(21, 45);
            this.Description_TB.Multiline = true;
            this.Description_TB.Name = "Description_TB";
            this.Description_TB.Size = new System.Drawing.Size(525, 57);
            this.Description_TB.TabIndex = 5;
            // 
            // ToolForm
            // 
            this.ClientSize = new System.Drawing.Size(634, 672);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToolForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "ToolForm";
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
            this.Statistics_GB.ResumeLayout(false);
            this.Statistics_GB.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridViewTextBoxColumn oidFamiliaDataGridViewTextBoxColumn;
        protected System.Windows.Forms.RichTextBox observacionesRichTextBox;
        protected System.Windows.Forms.TextBox Code_TB;
        protected System.Windows.Forms.TextBox nombreTextBox;
		private System.Windows.Forms.GroupBox Generales_GB;
		private System.Windows.Forms.GroupBox Observaciones_GB;
        private System.Windows.Forms.GroupBox Statistics_GB;
		private System.Windows.Forms.Label label2;
        private Controls.NumericTextBox Cost_NTB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		protected System.Windows.Forms.TextBox Description_TB;
		protected System.Windows.Forms.TextBox Lcoation_TB;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
		protected System.Windows.Forms.DateTimePicker Till_DTP;
		protected System.Windows.Forms.DateTimePicker From_DTP;

    }
}
