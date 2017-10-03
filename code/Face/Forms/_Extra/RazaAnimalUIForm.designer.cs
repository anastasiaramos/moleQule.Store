namespace moleQule.Face.Store
{
    partial class RazaAnimalUIForm
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
            this.Datos_Grid = new System.Windows.Forms.DataGridView();
            this.DatosLocal_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosLocal_BS)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.Datos_Grid);
            this.PanelesV.Size = new System.Drawing.Size(494, 476);
            this.PanelesV.SplitterDistance = 435;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(251, 6);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(341, 6);
            // 
            // Paneles2
            // 
            this.Paneles2.Size = new System.Drawing.Size(492, 38);
            this.Paneles2.SplitterDistance = 37;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Location = new System.Drawing.Point(161, 6);
            // 
            // Datos_Grid
            // 
            this.Datos_Grid.AllowUserToOrderColumns = true;
            this.Datos_Grid.AutoGenerateColumns = false;
            this.Datos_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Valor});
            this.Datos_Grid.DataSource = this.DatosLocal_BS;
            this.Datos_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Datos_Grid.Location = new System.Drawing.Point(0, 0);
            this.Datos_Grid.Name = "Datos_Grid";
            this.Datos_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Datos_Grid.Size = new System.Drawing.Size(492, 433);
            this.Datos_Grid.TabIndex = 0;
            // 
            // DatosLocal_BS
            // 
            this.DatosLocal_BS.DataSource = typeof(moleQule.Library.Store.RazaAnimal);
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            // 
            // RazaAnimalUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(494, 476);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "RazaAnimalUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "RazaAnimales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RazaAnimalUIForm_FormClosing);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosLocal_BS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Datos_Grid;
        private System.Windows.Forms.BindingSource DatosLocal_BS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;

    }
}
