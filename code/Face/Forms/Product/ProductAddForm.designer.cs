namespace moleQule.Face.Store
{
    partial class ProductAddForm
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
			this.General_TP.SuspendLayout();
			this.Parts_TC.SuspendLayout();
			this.Observaciones_TP.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Pie_Panel.Panel1.SuspendLayout();
			this.Pie_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Pestanas
			// 
			this.Parts_TC.Size = new System.Drawing.Size(513, 190);
			// 
			// TabObservaciones
			// 
			this.Observaciones_TP.Size = new System.Drawing.Size(609, 525);
			// 
			// observacionesRichTextBox
			// 
			this.observacionesRichTextBox.Size = new System.Drawing.Size(609, 525);
			// 
			// textBox2
			// 
			this.CuentaContableCompra_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaContableCompra_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// CContableVenta_TB
			// 
			this.CuentaContableVenta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaContableVenta_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
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
			this.PanelesV.Size = new System.Drawing.Size(515, 233);
			this.PanelesV.SplitterDistance = 192;
			// 
			// ProgressBK_Panel
			// 
			// 
			// ProductoAddForm
			// 
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "ProductoAddForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Nuevo Artículo/Servicio";
			this.General_TP.ResumeLayout(false);
			this.General_TP.PerformLayout();
			this.Parts_TC.ResumeLayout(false);
			this.Observaciones_TP.ResumeLayout(false);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Pie_Panel.Panel1.ResumeLayout(false);
			this.Pie_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}
