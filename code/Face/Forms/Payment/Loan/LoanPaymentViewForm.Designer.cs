namespace moleQule.Face.Store
{
    partial class LoanPaymentViewForm
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
			this.Gastos_Panel.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// GastosBancarios_NTB
			// 
			this.GastosBancarios_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.GastosBancarios_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Tarjeta_TB
			// 
			this.Tarjeta_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Tarjeta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Tarjeta_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Cuenta_BT
			// 
			this.Cuenta_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cuenta_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// Importe_NTB
			// 
			this.Importe_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Importe_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// vencimientoDateTimePicker
			// 
			this.Vencimiento_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Vencimiento_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// Fecha_DTP
			// 
			this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// observacionesRichTextBox
			// 
			this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Observaciones_RTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Cuenta_TB
			// 
			this.Cuenta_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Cuenta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Cuenta_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// NoAsignado_TB
			// 
			this.NoAsignado_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.NoAsignado_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Tarjeta_BT
			// 
			this.Tarjeta_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Tarjeta_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
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
			this.Submit_BT.Location = new System.Drawing.Point(352, 10);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(419, 10);
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
			this.Imprimir_Button.Location = new System.Drawing.Point(592, 10);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(502, 10);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// PagoGastoViewForm
			// 
			this.ClientSize = new System.Drawing.Size(1018, 572);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "PagoGastoViewForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Detalle de Pago";
			this.Shown += new System.EventHandler(this.PagoGastoViewForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
			this.Gastos_Panel.ResumeLayout(false);
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
