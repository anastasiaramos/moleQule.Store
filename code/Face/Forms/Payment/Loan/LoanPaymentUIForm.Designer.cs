namespace moleQule.Face.Store
{
    partial class LoanPaymentUIForm
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
            ((System.ComponentModel.ISupportInitialize)(this.Gastos_Panel)).BeginInit();
            this.Gastos_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
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
            this.Cuenta_BT.Click += new System.EventHandler(this.CuentaAjena_BT_Click);
            // 
            // Repartir_BT
            // 
            this.Repartir_BT.Click += new System.EventHandler(this.Repartir_BT_Click);
            // 
            // Liberar_BT
            // 
            this.Liberar_BT.Click += new System.EventHandler(this.Liberar_BT_Click);
            // 
            // Importe_NTB
            // 
            this.Importe_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Importe_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Importe_NTB.Leave += new System.EventHandler(this.Importe_NTB_Leave);
            this.Importe_NTB.Validated += new System.EventHandler(this.Importe_NTB_Validated);
            // 
            // Vencimiento_DTP
            // 
            this.Vencimiento_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Vencimiento_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Vencimiento_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // Observaciones_RTB
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
            // Gastos_Panel
            // 
            this.Gastos_Panel.Size = new System.Drawing.Size(1010, 290);
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
            this.Tarjeta_BT.Click += new System.EventHandler(this.Tarjeta_BT_Click);
            // 
            // MedioPago_TB
            // 
            this.MedioPago_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MedioPago_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MedioPago_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // MedioPago_BT
            // 
            this.MedioPago_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MedioPago_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MedioPago_BT.Click += new System.EventHandler(this.MedioPago_BT_Click);
            // 
            // EstadoPago_TB
            // 
            this.EstadoPago_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EstadoPago_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.EstadoPago_TB.ForeColor = System.Drawing.Color.Black;
            this.HelpProvider.SetShowHelp(this.EstadoPago_TB, true);
            // 
            // EstadoPago_BT
            // 
            this.EstadoPago_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.EstadoPago_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.HelpProvider.SetShowHelp(this.EstadoPago_BT, true);
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
            this.PanelesV.Size = new System.Drawing.Size(1018, 572);
            this.PanelesV.SplitterDistance = 517;
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
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(330, 106);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1018, 572);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(472, 334);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(472, 249);
            // 
            // LoanPaymentUIForm
            // 
            this.ClientSize = new System.Drawing.Size(1018, 572);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "LoanPaymentUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PagoGastoUIForm";
            this.Shown += new System.EventHandler(this.PagoGastoUIForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gastos_Panel)).EndInit();
            this.Gastos_Panel.ResumeLayout(false);
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
            this.ResumeLayout(false);

        }

        #endregion
	}
}
