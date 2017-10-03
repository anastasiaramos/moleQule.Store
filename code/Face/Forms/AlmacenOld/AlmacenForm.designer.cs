

namespace moleQule.Face.Store
{
    partial class AlmacenForm
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
			System.Windows.Forms.Label observacionesLabel;
			System.Windows.Forms.Label ubicacionLabel;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlmacenForm));
			this.Datos_InventarioAlmacenes = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_LineaAlmacenes = new System.Windows.Forms.BindingSource(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ubicacionTextBox = new System.Windows.Forms.TextBox();
			this.observacionesTextBox = new System.Windows.Forms.TextBox();
			this.nombreTextBox = new System.Windows.Forms.TextBox();
			this.Ficha_TC = new System.Windows.Forms.TabControl();
			this.LineaAlmacenes_TP = new System.Windows.Forms.TabPage();
			this.LineaAlmacenes_Grid = new System.Windows.Forms.DataGridView();
			this.conceptoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cantidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fechaDataGridViewTextBoxColumn = new CalendarColumn();
			this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InventarioAlmacenes_TP = new System.Windows.Forms.TabPage();
			this.InventarioAlmacenes_Grid = new System.Windows.Forms.DataGridView();
			this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fechaDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ObservacionesInventario = new System.Windows.Forms.DataGridViewTextBoxColumn();
			nombreLabel = new System.Windows.Forms.Label();
			observacionesLabel = new System.Windows.Forms.Label();
			ubicacionLabel = new System.Windows.Forms.Label();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_InventarioAlmacenes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaAlmacenes)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.Ficha_TC.SuspendLayout();
			this.LineaAlmacenes_TP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LineaAlmacenes_Grid)).BeginInit();
			this.InventarioAlmacenes_TP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.InventarioAlmacenes_Grid)).BeginInit();
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
			this.PanelesV.Size = new System.Drawing.Size(817, 548);
			this.PanelesV.SplitterDistance = 507;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(361, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			this.Submit_BT.Size = new System.Drawing.Size(93, 23);
			this.Submit_BT.Text = "Aceptar";
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(457, 6);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			this.Cancel_BT.Size = new System.Drawing.Size(87, 23);
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
			this.Paneles2.Size = new System.Drawing.Size(815, 38);
			this.Paneles2.SplitterDistance = 37;
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(265, 6);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			this.Imprimir_Button.Size = new System.Drawing.Size(87, 23);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(300, 6);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Store.Almacen);
			this.Datos.DataSourceChanged += new System.EventHandler(this.Datos_DataSourceChanged);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(199, 192);
			// 
			// nombreLabel
			// 
			nombreLabel.AutoSize = true;
			nombreLabel.Location = new System.Drawing.Point(23, 14);
			nombreLabel.Name = "nombreLabel";
			nombreLabel.Size = new System.Drawing.Size(54, 13);
			nombreLabel.TabIndex = 0;
			nombreLabel.Text = "Nombre*:";
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Location = new System.Drawing.Point(274, 14);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 2;
			observacionesLabel.Text = "Observaciones:";
			// 
			// ubicacionLabel
			// 
			ubicacionLabel.AutoSize = true;
			ubicacionLabel.Location = new System.Drawing.Point(13, 45);
			ubicacionLabel.Name = "ubicacionLabel";
			ubicacionLabel.Size = new System.Drawing.Size(56, 13);
			ubicacionLabel.TabIndex = 4;
			ubicacionLabel.Text = "Ubicación:";
			// 
			// Datos_InventarioAlmacenes
			// 
			this.Datos_InventarioAlmacenes.DataSource = typeof(moleQule.Library.Store.InventarioAlmacenList);
			// 
			// Datos_LineaAlmacenes
			// 
			this.Datos_LineaAlmacenes.DataSource = typeof(moleQule.Library.Store.LineaAlmacenes);
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
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(ubicacionLabel);
			this.splitContainer1.Panel1.Controls.Add(this.ubicacionTextBox);
			this.splitContainer1.Panel1.Controls.Add(observacionesLabel);
			this.splitContainer1.Panel1.Controls.Add(this.observacionesTextBox);
			this.splitContainer1.Panel1.Controls.Add(nombreLabel);
			this.splitContainer1.Panel1.Controls.Add(this.nombreTextBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.Ficha_TC);
			this.splitContainer1.Size = new System.Drawing.Size(815, 505);
			this.splitContainer1.SplitterDistance = 70;
			this.splitContainer1.TabIndex = 0;
			// 
			// ubicacionTextBox
			// 
			this.ubicacionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Ubicacion", true));
			this.ubicacionTextBox.Location = new System.Drawing.Point(83, 42);
			this.ubicacionTextBox.Name = "ubicacionTextBox";
			this.ubicacionTextBox.Size = new System.Drawing.Size(185, 21);
			this.ubicacionTextBox.TabIndex = 5;
			// 
			// observacionesTextBox
			// 
			this.observacionesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.observacionesTextBox.Location = new System.Drawing.Point(373, 11);
			this.observacionesTextBox.Multiline = true;
			this.observacionesTextBox.Name = "observacionesTextBox";
			this.observacionesTextBox.Size = new System.Drawing.Size(437, 56);
			this.observacionesTextBox.TabIndex = 3;
			// 
			// nombreTextBox
			// 
			this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.nombreTextBox.Location = new System.Drawing.Point(83, 11);
			this.nombreTextBox.Name = "nombreTextBox";
			this.nombreTextBox.Size = new System.Drawing.Size(185, 21);
			this.nombreTextBox.TabIndex = 1;
			// 
			// Ficha_TC
			// 
			this.Ficha_TC.Controls.Add(this.LineaAlmacenes_TP);
			this.Ficha_TC.Controls.Add(this.InventarioAlmacenes_TP);
			this.Ficha_TC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Ficha_TC.Location = new System.Drawing.Point(0, 0);
			this.Ficha_TC.Name = "Ficha_TC";
			this.Ficha_TC.SelectedIndex = 0;
			this.Ficha_TC.Size = new System.Drawing.Size(815, 431);
			this.Ficha_TC.TabIndex = 1;
			// 
			// LineaAlmacenes_TP
			// 
			this.LineaAlmacenes_TP.AutoScroll = true;
			this.LineaAlmacenes_TP.Controls.Add(this.LineaAlmacenes_Grid);
			this.LineaAlmacenes_TP.Location = new System.Drawing.Point(4, 22);
			this.LineaAlmacenes_TP.Name = "LineaAlmacenes_TP";
			this.LineaAlmacenes_TP.Padding = new System.Windows.Forms.Padding(3);
			this.LineaAlmacenes_TP.Size = new System.Drawing.Size(807, 405);
			this.LineaAlmacenes_TP.TabIndex = 0;
			this.LineaAlmacenes_TP.Text = "Elementos";
			this.LineaAlmacenes_TP.UseVisualStyleBackColor = true;
			// 
			// LineaAlmacenes_Grid
			// 
			this.LineaAlmacenes_Grid.AllowUserToOrderColumns = true;
			this.LineaAlmacenes_Grid.AutoGenerateColumns = false;
			this.LineaAlmacenes_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.LineaAlmacenes_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.conceptoDataGridViewTextBoxColumn,
            this.Referencia,
            this.cantidadDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.Observaciones});
			this.LineaAlmacenes_Grid.DataSource = this.Datos_LineaAlmacenes;
			this.LineaAlmacenes_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LineaAlmacenes_Grid.Location = new System.Drawing.Point(3, 3);
			this.LineaAlmacenes_Grid.Name = "LineaAlmacenes_Grid";
			this.LineaAlmacenes_Grid.Size = new System.Drawing.Size(801, 399);
			this.LineaAlmacenes_Grid.TabIndex = 3;
			// 
			// conceptoDataGridViewTextBoxColumn
			// 
			this.conceptoDataGridViewTextBoxColumn.DataPropertyName = "Concepto";
			this.conceptoDataGridViewTextBoxColumn.HeaderText = "Concepto";
			this.conceptoDataGridViewTextBoxColumn.Name = "conceptoDataGridViewTextBoxColumn";
			// 
			// Referencia
			// 
			this.Referencia.DataPropertyName = "Referencia";
			this.Referencia.HeaderText = "Referencia";
			this.Referencia.Name = "Referencia";
			// 
			// cantidadDataGridViewTextBoxColumn
			// 
			this.cantidadDataGridViewTextBoxColumn.DataPropertyName = "Cantidad";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.cantidadDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.cantidadDataGridViewTextBoxColumn.HeaderText = "Cantidad";
			this.cantidadDataGridViewTextBoxColumn.Name = "cantidadDataGridViewTextBoxColumn";
			// 
			// fechaDataGridViewTextBoxColumn
			// 
			this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
			this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
			this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
			this.fechaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.fechaDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// Observaciones
			// 
			this.Observaciones.DataPropertyName = "Observaciones";
			this.Observaciones.HeaderText = "Observaciones";
			this.Observaciones.Name = "Observaciones";
			// 
			// InventarioAlmacenes_TP
			// 
			this.InventarioAlmacenes_TP.AutoScroll = true;
			this.InventarioAlmacenes_TP.Controls.Add(this.InventarioAlmacenes_Grid);
			this.InventarioAlmacenes_TP.Location = new System.Drawing.Point(4, 22);
			this.InventarioAlmacenes_TP.Name = "InventarioAlmacenes_TP";
			this.InventarioAlmacenes_TP.Padding = new System.Windows.Forms.Padding(3);
			this.InventarioAlmacenes_TP.Size = new System.Drawing.Size(807, 388);
			this.InventarioAlmacenes_TP.TabIndex = 0;
			this.InventarioAlmacenes_TP.Text = "Inventarios";
			this.InventarioAlmacenes_TP.UseVisualStyleBackColor = true;
			// 
			// InventarioAlmacenes_Grid
			// 
			this.InventarioAlmacenes_Grid.AllowUserToAddRows = false;
			this.InventarioAlmacenes_Grid.AllowUserToDeleteRows = false;
			this.InventarioAlmacenes_Grid.AllowUserToOrderColumns = true;
			this.InventarioAlmacenes_Grid.AutoGenerateColumns = false;
			this.InventarioAlmacenes_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.InventarioAlmacenes_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn1,
            this.ObservacionesInventario});
			this.InventarioAlmacenes_Grid.DataSource = this.Datos_InventarioAlmacenes;
			this.InventarioAlmacenes_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InventarioAlmacenes_Grid.Location = new System.Drawing.Point(3, 3);
			this.InventarioAlmacenes_Grid.Name = "InventarioAlmacenes_Grid";
			this.InventarioAlmacenes_Grid.ReadOnly = true;
			this.InventarioAlmacenes_Grid.Size = new System.Drawing.Size(801, 382);
			this.InventarioAlmacenes_Grid.TabIndex = 3;
			this.InventarioAlmacenes_Grid.DoubleClick += new System.EventHandler(this.InventarioAlmacenes_Grid_DoubleClick);
			// 
			// nombreDataGridViewTextBoxColumn
			// 
			this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Nombre";
			this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
			this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
			this.nombreDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// fechaDataGridViewTextBoxColumn1
			// 
			this.fechaDataGridViewTextBoxColumn1.DataPropertyName = "Fecha";
			this.fechaDataGridViewTextBoxColumn1.HeaderText = "Fecha";
			this.fechaDataGridViewTextBoxColumn1.Name = "fechaDataGridViewTextBoxColumn1";
			this.fechaDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// ObservacionesInventario
			// 
			this.ObservacionesInventario.DataPropertyName = "Observaciones";
			this.ObservacionesInventario.HeaderText = "Observaciones";
			this.ObservacionesInventario.Name = "ObservacionesInventario";
			this.ObservacionesInventario.ReadOnly = true;
			// 
			// AlmacenForm
			// 
			this.ClientSize = new System.Drawing.Size(817, 548);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AlmacenForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "AlmacenForm";
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_InventarioAlmacenes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaAlmacenes)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.Ficha_TC.ResumeLayout(false);
			this.LineaAlmacenes_TP.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LineaAlmacenes_Grid)).EndInit();
			this.InventarioAlmacenes_TP.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.InventarioAlmacenes_Grid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource Datos_InventarioAlmacenes;
        protected System.Windows.Forms.BindingSource Datos_LineaAlmacenes;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.TabControl Ficha_TC;
        protected System.Windows.Forms.TabPage InventarioAlmacenes_TP;
        protected System.Windows.Forms.DataGridView InventarioAlmacenes_Grid;
        protected System.Windows.Forms.TabPage LineaAlmacenes_TP;
        protected System.Windows.Forms.DataGridView LineaAlmacenes_Grid;
        protected System.Windows.Forms.TextBox observacionesTextBox;
        protected System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.TextBox ubicacionTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObservacionesInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn conceptoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Referencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDataGridViewTextBoxColumn;
        private CalendarColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
		

    }
}
