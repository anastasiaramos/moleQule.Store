namespace moleQule.Face.Store
{
    partial class InventarioAlmacenForm
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
			System.Windows.Forms.Label nombreLabel;
			System.Windows.Forms.Label fechaLabel;
			System.Windows.Forms.Label observacionesLabel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventarioAlmacenForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.Almacen_CB = new System.Windows.Forms.ComboBox();
			this.Datos_Almacen = new System.Windows.Forms.BindingSource(this.components);
			this.observacionesTextBox = new System.Windows.Forms.TextBox();
			this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.nombreTextBox = new System.Windows.Forms.TextBox();
			this.LineaInventario_Grid = new System.Windows.Forms.DataGridView();
			this.conceptoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cantidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datos_LineaInventarios = new System.Windows.Forms.BindingSource(this.components);
			nombreLabel = new System.Windows.Forms.Label();
			fechaLabel = new System.Windows.Forms.Label();
			observacionesLabel = new System.Windows.Forms.Label();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Almacen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LineaInventario_Grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaInventarios)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.Controls.Add(this.splitContainer1);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(592, 266);
			this.PanelesV.SplitterDistance = 220;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(252, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			this.Submit_BT.Size = new System.Drawing.Size(93, 23);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
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
			this.Paneles2.Size = new System.Drawing.Size(590, 43);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(300, 6);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Store.InventarioAlmacen);
			this.Datos.DataSourceChanged += new System.EventHandler(this.Datos_DataSourceChanged);
			// 
			// ProgressBK_Panel
			// 
			// 
			// nombreLabel
			// 
			nombreLabel.AutoSize = true;
			nombreLabel.Location = new System.Drawing.Point(13, 14);
			nombreLabel.Name = "nombreLabel";
			nombreLabel.Size = new System.Drawing.Size(48, 13);
			nombreLabel.TabIndex = 0;
			nombreLabel.Text = "Nombre:";
			// 
			// fechaLabel
			// 
			fechaLabel.AutoSize = true;
			fechaLabel.Location = new System.Drawing.Point(24, 42);
			fechaLabel.Name = "fechaLabel";
			fechaLabel.Size = new System.Drawing.Size(40, 13);
			fechaLabel.TabIndex = 2;
			fechaLabel.Text = "Fecha:";
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Location = new System.Drawing.Point(219, 14);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 4;
			observacionesLabel.Text = "Observaciones:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.Almacen_CB);
			this.splitContainer1.Panel1.Controls.Add(observacionesLabel);
			this.splitContainer1.Panel1.Controls.Add(this.observacionesTextBox);
			this.splitContainer1.Panel1.Controls.Add(fechaLabel);
			this.splitContainer1.Panel1.Controls.Add(this.fechaDateTimePicker);
			this.splitContainer1.Panel1.Controls.Add(nombreLabel);
			this.splitContainer1.Panel1.Controls.Add(this.nombreTextBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.LineaInventario_Grid);
			this.splitContainer1.Size = new System.Drawing.Size(590, 218);
			this.splitContainer1.SplitterDistance = 100;
			this.splitContainer1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Almacén";
			// 
			// Almacen_CB
			// 
			this.Almacen_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "OidAlmacen", true));
			this.Almacen_CB.DataSource = this.Datos_Almacen;
			this.Almacen_CB.DisplayMember = "Nombre";
			this.Almacen_CB.FormattingEnabled = true;
			this.Almacen_CB.Location = new System.Drawing.Point(73, 65);
			this.Almacen_CB.Name = "Almacen_CB";
			this.Almacen_CB.Size = new System.Drawing.Size(121, 21);
			this.Almacen_CB.TabIndex = 6;
			this.Almacen_CB.ValueMember = "Oid";
			// 
			// Datos_Almacen
			// 
			this.Datos_Almacen.DataSource = typeof(moleQule.Library.Store.StoreList);
			// 
			// observacionesTextBox
			// 
			this.observacionesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.observacionesTextBox.Location = new System.Drawing.Point(318, 11);
			this.observacionesTextBox.Multiline = true;
			this.observacionesTextBox.Name = "observacionesTextBox";
			this.observacionesTextBox.Size = new System.Drawing.Size(261, 75);
			this.observacionesTextBox.TabIndex = 5;
			// 
			// fechaDateTimePicker
			// 
			this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
			this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.fechaDateTimePicker.Location = new System.Drawing.Point(73, 38);
			this.fechaDateTimePicker.Name = "fechaDateTimePicker";
			this.fechaDateTimePicker.Size = new System.Drawing.Size(121, 21);
			this.fechaDateTimePicker.TabIndex = 3;
			// 
			// nombreTextBox
			// 
			this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.nombreTextBox.Location = new System.Drawing.Point(73, 11);
			this.nombreTextBox.Name = "nombreTextBox";
			this.nombreTextBox.Size = new System.Drawing.Size(121, 21);
			this.nombreTextBox.TabIndex = 1;
			// 
			// LineaInventario_Grid
			// 
			this.LineaInventario_Grid.AllowUserToOrderColumns = true;
			this.LineaInventario_Grid.AutoGenerateColumns = false;
			this.LineaInventario_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.LineaInventario_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.conceptoDataGridViewTextBoxColumn,
            this.cantidadDataGridViewTextBoxColumn,
            this.Observaciones});
			this.LineaInventario_Grid.DataSource = this.Datos_LineaInventarios;
			this.LineaInventario_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LineaInventario_Grid.Location = new System.Drawing.Point(0, 0);
			this.LineaInventario_Grid.Name = "LineaInventario_Grid";
			this.LineaInventario_Grid.Size = new System.Drawing.Size(590, 114);
			this.LineaInventario_Grid.TabIndex = 0;
			// 
			// conceptoDataGridViewTextBoxColumn
			// 
			this.conceptoDataGridViewTextBoxColumn.DataPropertyName = "Concepto";
			this.conceptoDataGridViewTextBoxColumn.HeaderText = "Concepto";
			this.conceptoDataGridViewTextBoxColumn.Name = "conceptoDataGridViewTextBoxColumn";
			this.conceptoDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// cantidadDataGridViewTextBoxColumn
			// 
			this.cantidadDataGridViewTextBoxColumn.DataPropertyName = "Cantidad";
			this.cantidadDataGridViewTextBoxColumn.HeaderText = "Cantidad";
			this.cantidadDataGridViewTextBoxColumn.Name = "cantidadDataGridViewTextBoxColumn";
			// 
			// Observaciones
			// 
			this.Observaciones.DataPropertyName = "Observaciones";
			this.Observaciones.HeaderText = "Observaciones";
			this.Observaciones.Name = "Observaciones";
			// 
			// Datos_LineaInventarios
			// 
			this.Datos_LineaInventarios.DataSource = typeof(moleQule.Library.Store.LineaInventarios);
			// 
			// InventarioAlmacenForm
			// 
			this.ClientSize = new System.Drawing.Size(592, 266);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InventarioAlmacenForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "InventarioAlmacenForm";
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Almacen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LineaInventario_Grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaInventarios)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ComboBox Almacen_CB;
        protected System.Windows.Forms.BindingSource Datos_Almacen;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.TextBox observacionesTextBox;
        public System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn conceptoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        protected System.Windows.Forms.DataGridView LineaInventario_Grid;
        protected System.Windows.Forms.BindingSource Datos_LineaInventarios;
        public System.Windows.Forms.DateTimePicker fechaDateTimePicker;
		
		

    }
}
