namespace moleQule.Face.Store
{
    partial class TipoGastoUIForm
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
			this.TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.Generales_GB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Categoria)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Observaciones_RTB
			// 
			this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Observaciones_RTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Generales_GB.Controls.SetChildIndex(this.Codigo_TB, 0);
			this.Generales_GB.Controls.SetChildIndex(this.label1, 0);
			this.Generales_GB.Controls.SetChildIndex(this.Nombre_TB, 0);
			this.Generales_GB.Controls.SetChildIndex(this.label7, 0);
			this.Generales_GB.Controls.SetChildIndex(this.Categoria_CB, 0);
			// 
			// Categoria_CB
			// 
			this.Categoria_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Categoria_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// Nombre_TB
			// 
			this.Nombre_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Nombre_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// textBox5
			// 
			this.CuentaContable_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaContable_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// MedioPago_CB
			// 
			this.MedioPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MedioPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// CuentaAjena_BT
			// 
			this.CuentaAjena_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.CuentaAjena_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.CuentaAjena_BT.Click += new System.EventHandler(this.CuentaAjena_BT_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// FormaPago_CB
			// 
			this.FormaPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormaPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// DiasPago_NTB
			// 
			this.DiasPago_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DiasPago_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// CuentaAjena_TB
			// 
			this.CuentaAjena_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CuentaAjena_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaAjena_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// CuentaPropia_TB
			// 
			this.CuentaPropia_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaPropia_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// DefectoCtaContable_BT
			// 
			this.CuentaContable_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.CuentaContable_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.CuentaContable_BT.Click += new System.EventHandler(this.DefectoCtaContable_BT_Click);
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
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(155, 10);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(222, 10);
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
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(395, 10);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(305, 10);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// TipoGastoUIForm
			// 
			this.ClientSize = new System.Drawing.Size(624, 662);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "TipoGastoUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "TipoGastoUIForm";
			this.TabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.Generales_GB.ResumeLayout(false);
			this.Generales_GB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Categoria)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
	}
}
