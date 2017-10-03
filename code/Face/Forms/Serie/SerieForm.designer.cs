namespace moleQule.Face.Store
{
    partial class SerieForm
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
			System.Windows.Forms.Label cabeceraLabel;
			System.Windows.Forms.Label identificadorLabel;
			System.Windows.Forms.Label nombreLabel;
			System.Windows.Forms.Label observacionesLabel;
			System.Windows.Forms.Label resumenLabel;
			System.Windows.Forms.Label tipoSerieLabel;
			System.Windows.Forms.Label label18;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerieForm));
			this.cabeceraTextBox = new System.Windows.Forms.TextBox();
			this.identificadorTextBox = new System.Windows.Forms.TextBox();
			this.nombreTextBox = new System.Windows.Forms.TextBox();
			this.observacionesRichTextBox = new System.Windows.Forms.RichTextBox();
			this.resumenCheckBox = new System.Windows.Forms.CheckBox();
			this.TipoSerie_CMB = new System.Windows.Forms.ComboBox();
			this.Datos_Tipo = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_Familias = new System.Windows.Forms.BindingSource(this.components);
			this.Parts_TC = new System.Windows.Forms.TabControl();
			this.Main_TP = new System.Windows.Forms.TabPage();
			this.DefectoVenta_BT = new System.Windows.Forms.Button();
			this.Impuesto_BT = new System.Windows.Forms.Button();
			this.Impuesto_TB = new System.Windows.Forms.TextBox();
			this.Families_TP = new System.Windows.Forms.TabPage();
			this.Precios_Panel = new System.Windows.Forms.SplitContainer();
			this.Tool_Strip = new System.Windows.Forms.ToolStrip();
			this.Add_TI = new System.Windows.Forms.ToolStripButton();
			this.Edit_TI = new System.Windows.Forms.ToolStripButton();
			this.Delete_TI = new System.Windows.Forms.ToolStripButton();
			this.Familias_DG = new System.Windows.Forms.DataGridView();
			this.Familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cabeceraLabel = new System.Windows.Forms.Label();
			identificadorLabel = new System.Windows.Forms.Label();
			nombreLabel = new System.Windows.Forms.Label();
			observacionesLabel = new System.Windows.Forms.Label();
			resumenLabel = new System.Windows.Forms.Label();
			tipoSerieLabel = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Familias)).BeginInit();
			this.Parts_TC.SuspendLayout();
			this.Main_TP.SuspendLayout();
			this.Families_TP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Precios_Panel)).BeginInit();
			this.Precios_Panel.Panel1.SuspendLayout();
			this.Precios_Panel.Panel2.SuspendLayout();
			this.Precios_Panel.SuspendLayout();
			this.Tool_Strip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Familias_DG)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.Parts_TC);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(619, 546);
			this.PanelesV.SplitterDistance = 491;
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
			this.Paneles2.Size = new System.Drawing.Size(617, 52);
			this.Paneles2.SplitterDistance = 37;
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Imprimir_Button.Location = new System.Drawing.Point(814, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Docs_BT.Location = new System.Drawing.Point(300, 6);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Serie.Serie);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(130, 96);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(619, 546);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(272, 321);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(272, 236);
			// 
			// cabeceraLabel
			// 
			cabeceraLabel.AutoSize = true;
			cabeceraLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			cabeceraLabel.Location = new System.Drawing.Point(63, 161);
			cabeceraLabel.Name = "cabeceraLabel";
			cabeceraLabel.Size = new System.Drawing.Size(141, 13);
			cabeceraLabel.TabIndex = 0;
			cabeceraLabel.Text = "Nota (Reseña en Facturas):";
			// 
			// identificadorLabel
			// 
			identificadorLabel.AutoSize = true;
			identificadorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			identificadorLabel.Location = new System.Drawing.Point(63, 23);
			identificadorLabel.Name = "identificadorLabel";
			identificadorLabel.Size = new System.Drawing.Size(31, 13);
			identificadorLabel.TabIndex = 2;
			identificadorLabel.Text = "ID *:";
			// 
			// nombreLabel
			// 
			nombreLabel.AutoSize = true;
			nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			nombreLabel.Location = new System.Drawing.Point(286, 23);
			nombreLabel.Name = "nombreLabel";
			nombreLabel.Size = new System.Drawing.Size(48, 13);
			nombreLabel.TabIndex = 4;
			nombreLabel.Text = "Nombre:";
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			observacionesLabel.Location = new System.Drawing.Point(62, 286);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 6;
			observacionesLabel.Text = "Observaciones:";
			// 
			// resumenLabel
			// 
			resumenLabel.AutoSize = true;
			resumenLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			resumenLabel.Location = new System.Drawing.Point(448, 124);
			resumenLabel.Name = "resumenLabel";
			resumenLabel.Size = new System.Drawing.Size(55, 13);
			resumenLabel.TabIndex = 8;
			resumenLabel.Text = "Resumen:";
			resumenLabel.Visible = false;
			// 
			// tipoSerieLabel
			// 
			tipoSerieLabel.AutoSize = true;
			tipoSerieLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			tipoSerieLabel.Location = new System.Drawing.Point(63, 85);
			tipoSerieLabel.Name = "tipoSerieLabel";
			tipoSerieLabel.Size = new System.Drawing.Size(73, 13);
			tipoSerieLabel.TabIndex = 10;
			tipoSerieLabel.Text = "Tipo de Serie:";
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label18.Location = new System.Drawing.Point(62, 124);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(56, 13);
			label18.TabIndex = 137;
			label18.Text = "Impuesto:";
			// 
			// cabeceraTextBox
			// 
			this.cabeceraTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Cabecera", true));
			this.cabeceraTextBox.Location = new System.Drawing.Point(66, 177);
			this.cabeceraTextBox.Multiline = true;
			this.cabeceraTextBox.Name = "cabeceraTextBox";
			this.cabeceraTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.cabeceraTextBox.Size = new System.Drawing.Size(481, 93);
			this.cabeceraTextBox.TabIndex = 1;
			// 
			// identificadorTextBox
			// 
			this.identificadorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Identificador", true));
			this.identificadorTextBox.Location = new System.Drawing.Point(66, 39);
			this.identificadorTextBox.Name = "identificadorTextBox";
			this.identificadorTextBox.Size = new System.Drawing.Size(100, 21);
			this.identificadorTextBox.TabIndex = 3;
			this.identificadorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nombreTextBox
			// 
			this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.nombreTextBox.Location = new System.Drawing.Point(289, 39);
			this.nombreTextBox.Name = "nombreTextBox";
			this.nombreTextBox.Size = new System.Drawing.Size(257, 21);
			this.nombreTextBox.TabIndex = 5;
			// 
			// observacionesRichTextBox
			// 
			this.observacionesRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.observacionesRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.observacionesRichTextBox.Location = new System.Drawing.Point(65, 303);
			this.observacionesRichTextBox.Name = "observacionesRichTextBox";
			this.observacionesRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.observacionesRichTextBox.Size = new System.Drawing.Size(481, 96);
			this.observacionesRichTextBox.TabIndex = 7;
			this.observacionesRichTextBox.Text = "";
			// 
			// resumenCheckBox
			// 
			this.resumenCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "Resumen", true));
			this.resumenCheckBox.Location = new System.Drawing.Point(509, 119);
			this.resumenCheckBox.Name = "resumenCheckBox";
			this.resumenCheckBox.Size = new System.Drawing.Size(19, 24);
			this.resumenCheckBox.TabIndex = 9;
			this.resumenCheckBox.Visible = false;
			// 
			// TipoSerie_CMB
			// 
			this.TipoSerie_CMB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "Tipo", true));
			this.TipoSerie_CMB.DataSource = this.Datos_Tipo;
			this.TipoSerie_CMB.DisplayMember = "Texto";
			this.TipoSerie_CMB.FormattingEnabled = true;
			this.TipoSerie_CMB.Location = new System.Drawing.Point(142, 82);
			this.TipoSerie_CMB.Name = "TipoSerie_CMB";
			this.TipoSerie_CMB.Size = new System.Drawing.Size(192, 21);
			this.TipoSerie_CMB.TabIndex = 11;
			this.TipoSerie_CMB.ValueMember = "Oid";
			// 
			// Datos_Tipo
			// 
			this.Datos_Tipo.DataSource = typeof(moleQule.ComboBoxSourceList);
			// 
			// Datos_Familias
			// 
			this.Datos_Familias.DataSource = typeof(moleQule.Serie.SerieFamilia);
			// 
			// Parts_TC
			// 
			this.Parts_TC.Controls.Add(this.Main_TP);
			this.Parts_TC.Controls.Add(this.Families_TP);
			this.Parts_TC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Parts_TC.ItemSize = new System.Drawing.Size(100, 30);
			this.Parts_TC.Location = new System.Drawing.Point(0, 0);
			this.Parts_TC.Name = "Parts_TC";
			this.Parts_TC.SelectedIndex = 0;
			this.Parts_TC.Size = new System.Drawing.Size(617, 489);
			this.Parts_TC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.Parts_TC.TabIndex = 65;
			// 
			// Main_TP
			// 
			this.Main_TP.Controls.Add(this.DefectoVenta_BT);
			this.Main_TP.Controls.Add(this.Impuesto_BT);
			this.Main_TP.Controls.Add(label18);
			this.Main_TP.Controls.Add(this.Impuesto_TB);
			this.Main_TP.Controls.Add(this.observacionesRichTextBox);
			this.Main_TP.Controls.Add(this.cabeceraTextBox);
			this.Main_TP.Controls.Add(cabeceraLabel);
			this.Main_TP.Controls.Add(this.identificadorTextBox);
			this.Main_TP.Controls.Add(identificadorLabel);
			this.Main_TP.Controls.Add(this.nombreTextBox);
			this.Main_TP.Controls.Add(nombreLabel);
			this.Main_TP.Controls.Add(observacionesLabel);
			this.Main_TP.Controls.Add(tipoSerieLabel);
			this.Main_TP.Controls.Add(this.resumenCheckBox);
			this.Main_TP.Controls.Add(this.TipoSerie_CMB);
			this.Main_TP.Controls.Add(resumenLabel);
			this.Main_TP.Location = new System.Drawing.Point(4, 34);
			this.Main_TP.Name = "Main_TP";
			this.Main_TP.Padding = new System.Windows.Forms.Padding(3);
			this.Main_TP.Size = new System.Drawing.Size(609, 451);
			this.Main_TP.TabIndex = 0;
			this.Main_TP.Text = "General";
			this.Main_TP.UseVisualStyleBackColor = true;
			// 
			// DefectoVenta_BT
			// 
			this.DefectoVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
			this.DefectoVenta_BT.Location = new System.Drawing.Point(305, 121);
			this.DefectoVenta_BT.Name = "DefectoVenta_BT";
			this.DefectoVenta_BT.Size = new System.Drawing.Size(29, 21);
			this.DefectoVenta_BT.TabIndex = 146;
			this.DefectoVenta_BT.UseVisualStyleBackColor = true;
			// 
			// Impuesto_BT
			// 
			this.Impuesto_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
			this.Impuesto_BT.Location = new System.Drawing.Point(270, 121);
			this.Impuesto_BT.Name = "Impuesto_BT";
			this.Impuesto_BT.Size = new System.Drawing.Size(29, 21);
			this.Impuesto_BT.TabIndex = 138;
			this.Impuesto_BT.UseVisualStyleBackColor = true;
			// 
			// Impuesto_TB
			// 
			this.Impuesto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuesto", true));
			this.Impuesto_TB.Location = new System.Drawing.Point(124, 121);
			this.Impuesto_TB.Name = "Impuesto_TB";
			this.Impuesto_TB.ReadOnly = true;
			this.Impuesto_TB.Size = new System.Drawing.Size(140, 21);
			this.Impuesto_TB.TabIndex = 136;
			// 
			// Families_TP
			// 
			this.Families_TP.Controls.Add(this.Precios_Panel);
			this.Families_TP.Location = new System.Drawing.Point(4, 34);
			this.Families_TP.Name = "Families_TP";
			this.Families_TP.Padding = new System.Windows.Forms.Padding(3);
			this.Families_TP.Size = new System.Drawing.Size(609, 451);
			this.Families_TP.TabIndex = 1;
			this.Families_TP.Text = "Familias";
			this.Families_TP.UseVisualStyleBackColor = true;
			// 
			// Precios_Panel
			// 
			this.Precios_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Precios_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Precios_Panel.Location = new System.Drawing.Point(3, 3);
			this.Precios_Panel.Name = "Precios_Panel";
			this.Precios_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Precios_Panel.Panel1
			// 
			this.Precios_Panel.Panel1.Controls.Add(this.Tool_Strip);
			this.Precios_Panel.Panel1MinSize = 39;
			// 
			// Precios_Panel.Panel2
			// 
			this.Precios_Panel.Panel2.Controls.Add(this.Familias_DG);
			this.Precios_Panel.Size = new System.Drawing.Size(603, 445);
			this.Precios_Panel.SplitterDistance = 39;
			this.Precios_Panel.SplitterWidth = 1;
			this.Precios_Panel.TabIndex = 1;
			// 
			// Tool_Strip
			// 
			this.Tool_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Tool_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_TI,
            this.Edit_TI,
            this.Delete_TI});
			this.Tool_Strip.Location = new System.Drawing.Point(0, 0);
			this.Tool_Strip.Name = "Tool_Strip";
			this.HelpProvider.SetShowHelp(this.Tool_Strip, true);
			this.Tool_Strip.Size = new System.Drawing.Size(603, 39);
			this.Tool_Strip.TabIndex = 3;
			this.Tool_Strip.Text = "Imprimir";
			// 
			// Add_TI
			// 
			this.Add_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Add_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
			this.Add_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Add_TI.Name = "Add_TI";
			this.Add_TI.Size = new System.Drawing.Size(36, 36);
			this.Add_TI.Text = "Nuevo";
			this.Add_TI.Click += new System.EventHandler(this.Add_Button_Click);
			// 
			// Edit_TI
			// 
			this.Edit_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Edit_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
			this.Edit_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Edit_TI.Name = "Edit_TI";
			this.Edit_TI.Size = new System.Drawing.Size(36, 36);
			this.Edit_TI.Text = "Editar";
			this.Edit_TI.Visible = false;
			// 
			// Delete_TI
			// 
			this.Delete_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Delete_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
			this.Delete_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Delete_TI.Name = "Delete_TI";
			this.Delete_TI.Size = new System.Drawing.Size(36, 36);
			this.Delete_TI.Text = "Borrar";
			this.Delete_TI.Click += new System.EventHandler(this.Delete_Button_Click);
			// 
			// Familias_DG
			// 
			this.Familias_DG.AllowUserToAddRows = false;
			this.Familias_DG.AllowUserToDeleteRows = false;
			this.Familias_DG.AutoGenerateColumns = false;
			this.Familias_DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Familias_DG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Familia});
			this.Familias_DG.DataSource = this.Datos_Familias;
			this.Familias_DG.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Familias_DG.Location = new System.Drawing.Point(0, 0);
			this.Familias_DG.Name = "Familias_DG";
			this.Familias_DG.ReadOnly = true;
			this.Familias_DG.Size = new System.Drawing.Size(603, 405);
			this.Familias_DG.TabIndex = 0;
			// 
			// Familia
			// 
			this.Familia.DataPropertyName = "Familia";
			this.Familia.HeaderText = "Familia";
			this.Familia.Name = "Familia";
			this.Familia.ReadOnly = true;
			// 
			// SerieForm
			// 
			this.ClientSize = new System.Drawing.Size(619, 546);
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(625, 574);
			this.Name = "SerieForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "SerieForm";
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Familias)).EndInit();
			this.Parts_TC.ResumeLayout(false);
			this.Main_TP.ResumeLayout(false);
			this.Main_TP.PerformLayout();
			this.Families_TP.ResumeLayout(false);
			this.Precios_Panel.Panel1.ResumeLayout(false);
			this.Precios_Panel.Panel1.PerformLayout();
			this.Precios_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Precios_Panel)).EndInit();
			this.Precios_Panel.ResumeLayout(false);
			this.Tool_Strip.ResumeLayout(false);
			this.Tool_Strip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Familias_DG)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.CheckBox resumenCheckBox;
        protected System.Windows.Forms.RichTextBox observacionesRichTextBox;
        protected System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.TextBox identificadorTextBox;
        protected System.Windows.Forms.TextBox cabeceraTextBox;
        protected System.Windows.Forms.ComboBox TipoSerie_CMB;
        protected System.Windows.Forms.BindingSource Datos_Tipo;
		protected System.Windows.Forms.BindingSource Datos_Familias;
        private System.Windows.Forms.TabControl Parts_TC;
        private System.Windows.Forms.TabPage Main_TP;
        private System.Windows.Forms.TabPage Families_TP;
        protected System.Windows.Forms.SplitContainer Precios_Panel;
        protected System.Windows.Forms.ToolStrip Tool_Strip;
        protected System.Windows.Forms.ToolStripButton Add_TI;
        protected System.Windows.Forms.ToolStripButton Edit_TI;
        protected System.Windows.Forms.ToolStripButton Delete_TI;
        protected System.Windows.Forms.Button Impuesto_BT;
        protected System.Windows.Forms.TextBox Impuesto_TB;
        private System.Windows.Forms.DataGridView Familias_DG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Familia;
		protected System.Windows.Forms.Button DefectoVenta_BT;

    }
}
