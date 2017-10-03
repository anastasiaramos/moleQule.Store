namespace moleQule.Face.Store
{
    partial class EditStockActionForm
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
			System.Windows.Forms.Label conceptoLabel;
			System.Windows.Forms.Label fechaLabel;
			System.Windows.Forms.Label kilosLabel;
			System.Windows.Forms.Label observacionesLabel;
			System.Windows.Forms.Label bultosLabel;
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditStockActionForm));
			this.Datos = new System.Windows.Forms.BindingSource(this.components);
			this.conceptoRichTextBox = new System.Windows.Forms.RichTextBox();
			this.fechaDateTimePicker = new moleQule.Face.Controls.mQDateTimePicker();
			this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
			this.Bultos_BT = new moleQule.Face.Controls.NumericTextBox();
			this.Kilos_BT = new moleQule.Face.Controls.NumericTextBox();
			this.Datos_Tipo = new System.Windows.Forms.BindingSource(this.components);
			this.Tipo_CB = new System.Windows.Forms.ComboBox();
			conceptoLabel = new System.Windows.Forms.Label();
			fechaLabel = new System.Windows.Forms.Label();
			kilosLabel = new System.Windows.Forms.Label();
			observacionesLabel = new System.Windows.Forms.Label();
			bultosLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).BeginInit();
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
			this.Submit_BT.Location = new System.Drawing.Point(135, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(225, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(label1);
			this.Source_GB.Controls.Add(this.Tipo_CB);
			this.Source_GB.Controls.Add(this.Kilos_BT);
			this.Source_GB.Controls.Add(this.Bultos_BT);
			this.Source_GB.Controls.Add(conceptoLabel);
			this.Source_GB.Controls.Add(this.conceptoRichTextBox);
			this.Source_GB.Controls.Add(fechaLabel);
			this.Source_GB.Controls.Add(this.fechaDateTimePicker);
			this.Source_GB.Controls.Add(kilosLabel);
			this.Source_GB.Controls.Add(observacionesLabel);
			this.Source_GB.Controls.Add(this.observacionesRichTextBox);
			this.Source_GB.Controls.Add(bultosLabel);
			this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Source_GB.Location = new System.Drawing.Point(0, 0);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(475, 301);
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
			this.PanelesV.Size = new System.Drawing.Size(477, 343);
			this.PanelesV.SplitterDistance = 303;
			// 
			// conceptoLabel
			// 
			conceptoLabel.AutoSize = true;
			conceptoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			conceptoLabel.Location = new System.Drawing.Point(54, 28);
			conceptoLabel.Name = "conceptoLabel";
			conceptoLabel.Size = new System.Drawing.Size(57, 13);
			conceptoLabel.TabIndex = 28;
			conceptoLabel.Text = "Concepto:";
			// 
			// fechaLabel
			// 
			fechaLabel.AutoSize = true;
			fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			fechaLabel.Location = new System.Drawing.Point(71, 151);
			fechaLabel.Name = "fechaLabel";
			fechaLabel.Size = new System.Drawing.Size(40, 13);
			fechaLabel.TabIndex = 26;
			fechaLabel.Text = "Fecha:";
			// 
			// kilosLabel
			// 
			kilosLabel.AutoSize = true;
			kilosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			kilosLabel.Location = new System.Drawing.Point(322, 118);
			kilosLabel.Name = "kilosLabel";
			kilosLabel.Size = new System.Drawing.Size(32, 13);
			kilosLabel.TabIndex = 24;
			kilosLabel.Text = "Kilos:";
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			observacionesLabel.Location = new System.Drawing.Point(29, 188);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 22;
			observacionesLabel.Text = "Observaciones:";
			// 
			// bultosLabel
			// 
			bultosLabel.AutoSize = true;
			bultosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			bultosLabel.Location = new System.Drawing.Point(313, 151);
			bultosLabel.Name = "bultosLabel";
			bultosLabel.Size = new System.Drawing.Size(40, 13);
			bultosLabel.TabIndex = 20;
			bultosLabel.Text = "Bultos:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(80, 118);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(31, 13);
			label1.TabIndex = 32;
			label1.Text = "Tipo:";
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Store.Stock);
			// 
			// conceptoRichTextBox
			// 
			this.conceptoRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Concepto", true));
			this.conceptoRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.conceptoRichTextBox.ForeColor = System.Drawing.Color.Navy;
			this.conceptoRichTextBox.Location = new System.Drawing.Point(117, 25);
			this.conceptoRichTextBox.Name = "conceptoRichTextBox";
			this.conceptoRichTextBox.Size = new System.Drawing.Size(328, 80);
			this.conceptoRichTextBox.TabIndex = 0;
			this.conceptoRichTextBox.Text = "";
			// 
			// fechaDateTimePicker
			// 
			this.fechaDateTimePicker.CalendarForeColor = System.Drawing.Color.Navy;
			this.fechaDateTimePicker.CalendarTitleForeColor = System.Drawing.Color.Navy;
			this.fechaDateTimePicker.Checked = false;
			this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
			this.fechaDateTimePicker.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.fechaDateTimePicker.Location = new System.Drawing.Point(117, 148);
			this.fechaDateTimePicker.Name = "fechaDateTimePicker";
			this.fechaDateTimePicker.ShowCheckBox = true;
			this.fechaDateTimePicker.Size = new System.Drawing.Size(121, 21);
			this.fechaDateTimePicker.TabIndex = 1;
			// 
			// observacionesRichTextBox
			// 
			this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.observacionesRichTextBox.ForeColor = System.Drawing.Color.Navy;
			this.observacionesRichTextBox.Location = new System.Drawing.Point(117, 185);
			this.observacionesRichTextBox.Name = "observacionesRichTextBox";
			this.observacionesRichTextBox.Size = new System.Drawing.Size(328, 96);
			this.observacionesRichTextBox.TabIndex = 4;
			this.observacionesRichTextBox.Text = "";
			// 
			// Bultos_BT
			// 
			this.Bultos_BT.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Bultos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.Bultos_BT.ForeColor = System.Drawing.Color.Navy;
			this.Bultos_BT.Location = new System.Drawing.Point(359, 149);
			this.Bultos_BT.Name = "Bultos_BT";
			this.Bultos_BT.Size = new System.Drawing.Size(86, 21);
			this.Bultos_BT.TabIndex = 2;
			this.Bultos_BT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Bultos_BT.TextIsCurrency = false;
			this.Bultos_BT.TextIsInteger = true;
			this.Bultos_BT.Validated += new System.EventHandler(this.Bultos_BT_Validated);
			// 
			// Kilos_BT
			// 
			this.Kilos_BT.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Kilos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.Kilos_BT.ForeColor = System.Drawing.Color.Navy;
			this.Kilos_BT.Location = new System.Drawing.Point(359, 116);
			this.Kilos_BT.Name = "Kilos_BT";
			this.Kilos_BT.Size = new System.Drawing.Size(86, 21);
			this.Kilos_BT.TabIndex = 3;
			this.Kilos_BT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Kilos_BT.TextIsCurrency = false;
			this.Kilos_BT.TextIsInteger = false;
			this.Kilos_BT.Validated += new System.EventHandler(this.Kilos_BT_Validated);
			// 
			// Datos_Tipo
			// 
			this.Datos_Tipo.DataSource = typeof(moleQule.ComboBoxSourceList);
			// 
			// Tipo_CB
			// 
			this.Tipo_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "Tipo", true));
			this.Tipo_CB.DataSource = this.Datos_Tipo;
			this.Tipo_CB.DisplayMember = "Texto";
			this.Tipo_CB.FormattingEnabled = true;
			this.Tipo_CB.Location = new System.Drawing.Point(117, 115);
			this.Tipo_CB.Name = "Tipo_CB";
			this.Tipo_CB.Size = new System.Drawing.Size(183, 21);
			this.Tipo_CB.TabIndex = 31;
			this.Tipo_CB.ValueMember = "Oid";
			this.Tipo_CB.SelectedIndexChanged += new System.EventHandler(this.Tipo_CB_SelectedIndexChanged);
			// 
			// EditStockActionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(477, 343);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditStockActionForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Datos;
        private System.Windows.Forms.RichTextBox conceptoRichTextBox;
        private System.Windows.Forms.RichTextBox observacionesRichTextBox;
        private moleQule.Face.Controls.NumericTextBox Kilos_BT;
        private moleQule.Face.Controls.NumericTextBox Bultos_BT;
		private moleQule.Face.Controls.mQDateTimePicker fechaDateTimePicker;
		private System.Windows.Forms.ComboBox Tipo_CB;
		private System.Windows.Forms.BindingSource Datos_Tipo;
    }
}
