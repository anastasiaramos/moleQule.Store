namespace moleQule.Face.Store
{
	partial class InputInvoiceUIForm
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Transportista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).BeginInit();
            this.Otros_GB.SuspendLayout();
            this.Factura_GB.SuspendLayout();
            this.Emisor_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Emisor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_SC)).BeginInit();
            this.Conceptos_SC.SuspendLayout();
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
            this.Otros_GB.Controls.SetChildIndex(this.FormaPago_CB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.DiasPago_NTB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.Prevision_TB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.MedioPago_CB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.Cuenta_TB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.label5, 0);
            this.Otros_GB.Controls.SetChildIndex(this.Cuenta_BT, 0);
            this.Otros_GB.Controls.SetChildIndex(this.Albaranes_TB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.label9, 0);
            this.Otros_GB.Controls.SetChildIndex(this.Observaciones_TB, 0);
            this.Otros_GB.Controls.SetChildIndex(this.label14, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Codigo_TB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Fecha_DTP, 0);
            this.Factura_GB.Controls.SetChildIndex(this.NFactura_TB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.FechaRegistro_DTP, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Serie_TB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Rectificativo_CkB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Serie_BT, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Usuario_TB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Usuario_BT, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Expediente_TB, 0);
            this.Factura_GB.Controls.SetChildIndex(this.Expediente_BT, 0);
            this.Factura_GB.Controls.SetChildIndex(this.ExpedienteManual_BT, 0);
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Fecha_DTP.Size = new System.Drawing.Size(115, 21);
            this.Fecha_DTP.ValueChanged += new System.EventHandler(this.Fecha_DTP_ValueChanged);
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Codigo_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Codigo_TB.ForeColor = System.Drawing.Color.Black;
            this.Codigo_TB.Size = new System.Drawing.Size(80, 21);
            this.Emisor_GB.Controls.SetChildIndex(this.IDE_TB, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.CodigoE_TB, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.NombreE_TB, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.Emisor_BT, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.TipoAcreedorE_TB, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.IRPFE_NTB, 0);
            this.Emisor_GB.Controls.SetChildIndex(this.ImpuestosE_TB, 0);
            // 
            // Emisor_BT
            // 
            this.Emisor_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Emisor_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Emisor_BT.Click += new System.EventHandler(this.Emisor_BT_Click);
            // 
            // NombreE_TB
            // 
            this.NombreE_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NombreE_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NombreE_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // CodigoE_TB
            // 
            this.CodigoE_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CodigoE_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CodigoE_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // IDE_TB
            // 
            this.IDE_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IDE_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.IDE_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // MedioPago_CB
            // 
            this.MedioPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MedioPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.MedioPago_CB.SelectedIndexChanged += new System.EventHandler(this.MedioPago_CB_SelectedIndexChanged);
            // 
            // FormaPago_CB
            // 
            this.FormaPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormaPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FormaPago_CB.SelectedIndexChanged += new System.EventHandler(this.FormaPago_CB_SelectedIndexChanged);
            // 
            // Prevision_TB
            // 
            this.Prevision_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Prevision_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Prevision_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // NFactura_TB
            // 
            this.NFactura_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NFactura_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // TipoAcreedorE_TB
            // 
            this.TipoAcreedorE_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TipoAcreedorE_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.TipoAcreedorE_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // FechaRegistro_DTP
            // 
            this.FechaRegistro_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FechaRegistro_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FechaRegistro_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // ImpuestosE_TB
            // 
            this.ImpuestosE_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ImpuestosE_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ImpuestosE_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // IRPFE_NTB
            // 
            this.IRPFE_NTB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IRPFE_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.IRPFE_NTB.ForeColor = System.Drawing.Color.Black;
            this.IRPFE_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PDescuento_NTB.TextChanged += new System.EventHandler(this.Descuento_NTB_TextChanged);
            // 
            // Descuento_NTB
            // 
            this.Descuento_NTB.TextChanged += new System.EventHandler(this.Descuento_NTB_TextChanged_1);
            // 
            // DiasPago_NTB
            // 
            this.DiasPago_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DiasPago_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.DiasPago_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DiasPago_NTB.Validated += new System.EventHandler(this.DiasPago_NTB_TextChanged);
            // 
            // Cuenta_TB
            // 
            this.Cuenta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Cuenta_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // Cuenta_BT
            // 
            this.Cuenta_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cuenta_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Cuenta_BT.Click += new System.EventHandler(this.Cuenta_BT_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // Albaranes_TB
            // 
            this.Albaranes_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Albaranes_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Albaranes_TB.ForeColor = System.Drawing.Color.Black;
            this.Albaranes_TB.TabStop = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // Observaciones_TB
            // 
            this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Observaciones_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // Usuario_BT
            // 
            this.Usuario_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Usuario_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Usuario_BT.Click += new System.EventHandler(this.Usuario_BT_Click);
            // 
            // Usuario_TB
            // 
            this.Usuario_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Usuario_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Usuario_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // Serie_BT
            // 
            this.Serie_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Serie_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Serie_BT.Click += new System.EventHandler(this.Serie_BT_Click);
            // 
            // Serie_TB
            // 
            this.Serie_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Serie_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Expediente_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Expediente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Expediente_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // ExpedienteManual_BT
            // 
            this.ExpedienteManual_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ExpedienteManual_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExpedienteManual_BT.Click += new System.EventHandler(this.ExpedienteManual_BT_Click);
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
            this.PanelesV.Size = new System.Drawing.Size(1194, 447);
            this.PanelesV.SplitterDistance = 392;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(331, 9);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(464, 9);
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
            this.Imprimir_Button.Location = new System.Drawing.Point(730, 9);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Location = new System.Drawing.Point(597, 9);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 447);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 272);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(560, 187);
            // 
            // FacturaRecibidaUIForm
            // 
            this.ClientSize = new System.Drawing.Size(1194, 447);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "FacturaRecibidaUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "FacturaRecibidaUIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Transportista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).EndInit();
            this.Otros_GB.ResumeLayout(false);
            this.Otros_GB.PerformLayout();
            this.Factura_GB.ResumeLayout(false);
            this.Factura_GB.PerformLayout();
            this.Emisor_GB.ResumeLayout(false);
            this.Emisor_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Emisor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_SC)).EndInit();
            this.Conceptos_SC.ResumeLayout(false);
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
