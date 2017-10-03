namespace moleQule.Face.Store
{
	partial class PuertoUIForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.List_DGW = new System.Windows.Forms.DataGridView();
            this.Local_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.List_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Local_BS)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.List_DGW);
            this.PanelesV.Size = new System.Drawing.Size(494, 476);
            this.PanelesV.SplitterDistance = 435;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(70, 6);
            this.Submit_BT.Size = new System.Drawing.Size(98, 23);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(172, 6);
            this.Cancel_BT.Size = new System.Drawing.Size(98, 23);
            // 
            // Paneles2
            // 
            this.Paneles2.Size = new System.Drawing.Size(492, 38);
            this.Paneles2.SplitterDistance = 37;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Location = new System.Drawing.Point(11, 6);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Location = new System.Drawing.Point(300, 6);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Puertos);
            // 
            // Datos_Grid
            // 
            this.List_DGW.AutoGenerateColumns = false;
            this.List_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.List_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Valor,
            this.Precio});
            this.List_DGW.DataSource = this.Local_BS;
            this.List_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_DGW.Location = new System.Drawing.Point(0, 0);
            this.List_DGW.Name = "Datos_Grid";
            this.List_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.List_DGW.Size = new System.Drawing.Size(492, 433);
            this.List_DGW.TabIndex = 0;
            // 
            // DatosLocal_BS
            // 
            this.Local_BS.DataSource = typeof(moleQule.Library.Store.Puerto);
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Puerto";
            this.Valor.Name = "Valor";
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.Precio.DefaultCellStyle = dataGridViewCellStyle1;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 80;
            // 
            // PuertoUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(494, 476);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "PuertoUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PuertoUIForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.List_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Local_BS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView List_DGW;
        private System.Windows.Forms.BindingSource Local_BS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;

    }
}
