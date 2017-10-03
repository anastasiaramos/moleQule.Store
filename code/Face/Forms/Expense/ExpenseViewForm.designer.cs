namespace moleQule.Face.Store
{
	partial class ExpenseViewForm
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
			this.TabGeneral.SuspendLayout();
			this.Pestanas.SuspendLayout();
			this.TabObservaciones.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// TabGeneral
			// 
			this.TabGeneral.Size = new System.Drawing.Size(505, 152);
			//this.TabGeneral.Controls.SetChildIndex(this.textBox1, 0);
			// 
			// Pestanas
			// 
			this.Pestanas.Size = new System.Drawing.Size(513, 190);
			// 
			// TabObservaciones
			// 
			this.TabObservaciones.Size = new System.Drawing.Size(609, 525);
			// 
			// observacionesRichTextBox
			// 
			this.observacionesRichTextBox.Size = new System.Drawing.Size(609, 525);
			// 
			// precioCompraNumericTextBox
			// 
			this.Importe_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Importe_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Familia_BT
			// 
			this.Categoria_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Categoria_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// CContableVenta_TB
			// 
			this.CContableVenta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CContableVenta_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
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
			// Submit_BT
			// 
			this.Submit_BT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Submit_BT.Location = new System.Drawing.Point(414, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			this.Submit_BT.Size = new System.Drawing.Size(87, 23);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(500, 7);
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
			this.Paneles2.Size = new System.Drawing.Size(513, 38);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(328, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			this.Imprimir_Button.Size = new System.Drawing.Size(83, 23);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(262, 7);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// GastoViewForm
			// 
			this.CancelButton = this.Submit_BT;
			this.ClientSize = new System.Drawing.Size(515, 233);
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "GastoViewForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Detalle de Gasto";
			this.TabGeneral.ResumeLayout(false);
			this.TabGeneral.PerformLayout();
			this.Pestanas.ResumeLayout(false);
			this.TabObservaciones.ResumeLayout(false);
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
