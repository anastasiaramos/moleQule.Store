namespace moleQule.Face.Store
{
    partial class MaquinariaEditForm
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
			System.Windows.Forms.Label observacionesLabel;
			System.Windows.Forms.Label descripcionLabel;
			System.Windows.Forms.Label identificadorLabel;
			this.Datos = new System.Windows.Forms.BindingSource(this.components);
			this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
			this.descripcionTextBox = new System.Windows.Forms.TextBox();
			this.identificadorTextBox = new System.Windows.Forms.TextBox();
			observacionesLabel = new System.Windows.Forms.Label();
			descripcionLabel = new System.Windows.Forms.Label();
			identificadorLabel = new System.Windows.Forms.Label();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
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
			this.Submit_BT.Location = new System.Drawing.Point(168, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(258, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(observacionesLabel);
			this.Source_GB.Controls.Add(this.Observaciones_RTB);
			this.Source_GB.Controls.Add(descripcionLabel);
			this.Source_GB.Controls.Add(this.descripcionTextBox);
			this.Source_GB.Controls.Add(identificadorLabel);
			this.Source_GB.Controls.Add(this.identificadorTextBox);
			this.Source_GB.Location = new System.Drawing.Point(11, 3);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(490, 206);
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
			this.PanelesV.Size = new System.Drawing.Size(514, 254);
			this.PanelesV.SplitterDistance = 214;
			// 
			// ProgressBK_Panel
			// 
			// 
			// observacionesLabel
			// 
			observacionesLabel.AutoSize = true;
			observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			observacionesLabel.Location = new System.Drawing.Point(22, 85);
			observacionesLabel.Name = "observacionesLabel";
			observacionesLabel.Size = new System.Drawing.Size(82, 13);
			observacionesLabel.TabIndex = 10;
			observacionesLabel.Text = "Observaciones:";
			// 
			// descripcionLabel
			// 
			descripcionLabel.AutoSize = true;
			descripcionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			descripcionLabel.Location = new System.Drawing.Point(39, 58);
			descripcionLabel.Name = "descripcionLabel";
			descripcionLabel.Size = new System.Drawing.Size(65, 13);
			descripcionLabel.TabIndex = 8;
			descripcionLabel.Text = "Descripción:";
			// 
			// identificadorLabel
			// 
			identificadorLabel.AutoSize = true;
			identificadorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			identificadorLabel.Location = new System.Drawing.Point(26, 31);
			identificadorLabel.Name = "identificadorLabel";
			identificadorLabel.Size = new System.Drawing.Size(78, 13);
			identificadorLabel.TabIndex = 6;
			identificadorLabel.Text = "Identificador*:";
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Store.Maquinaria);
			// 
			// Observaciones_RTB
			// 
			this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Observaciones_RTB.Location = new System.Drawing.Point(110, 82);
			this.Observaciones_RTB.Name = "Observaciones_RTB";
			this.Observaciones_RTB.Size = new System.Drawing.Size(359, 96);
			this.Observaciones_RTB.TabIndex = 2;
			this.Observaciones_RTB.Text = "";
			// 
			// descripcionTextBox
			// 
			this.descripcionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Descripcion", true));
			this.descripcionTextBox.Location = new System.Drawing.Point(110, 55);
			this.descripcionTextBox.Name = "descripcionTextBox";
			this.descripcionTextBox.Size = new System.Drawing.Size(359, 21);
			this.descripcionTextBox.TabIndex = 1;
			// 
			// identificadorTextBox
			// 
			this.identificadorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Identificador", true));
			this.identificadorTextBox.Location = new System.Drawing.Point(110, 28);
			this.identificadorTextBox.Name = "identificadorTextBox";
			this.identificadorTextBox.Size = new System.Drawing.Size(359, 21);
			this.identificadorTextBox.TabIndex = 0;
			// 
			// MaquinariaEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(514, 254);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "MaquinariaEditForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Modificar Máquina";
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Datos;
        private System.Windows.Forms.RichTextBox Observaciones_RTB;
        private System.Windows.Forms.TextBox descripcionTextBox;
        private System.Windows.Forms.TextBox identificadorTextBox;

        
    }
}
