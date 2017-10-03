using System.Drawing;

namespace moleQule.Face.Store
{
	partial class AlmacenUIForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlmacenUIForm));
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.Ficha.SuspendLayout();
			this.TabDatos.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Partidas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Stock)).BeginInit();
			this.TabStock.SuspendLayout();
			this.TabStock_SC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Resumen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).BeginInit();
			this.TabProductos.SuspendLayout();
			this.TabProductos_SC.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// Ficha
			// 
			this.Ficha.Size = new System.Drawing.Size(1466, 840);
			// 
			// TabResumen
			// 
			this.TabResumen.Size = new System.Drawing.Size(1458, 802);
			// 
			// TabDatos
			// 
			this.TabDatos.Size = new System.Drawing.Size(1458, 802);
			this.TabDatos.Controls.SetChildIndex(this.Estado_TB, 0);
			this.TabDatos.Controls.SetChildIndex(this.Estado_BT, 0);
			this.TabDatos.Controls.SetChildIndex(this.Codigo_TB, 0);
			this.TabDatos.Controls.SetChildIndex(this.label2, 0);
			this.TabDatos.Controls.SetChildIndex(this.Ubicacion_TB, 0);
			this.TabDatos.Controls.SetChildIndex(this.Nombre_TB, 0);
			this.TabDatos.Controls.SetChildIndex(this.label14, 0);
			// 
			// TabProductos
			// 
			this.TabProductos.Size = new System.Drawing.Size(1458, 802);
			// 
			// PanelesPestanaGeneral
			// 
			this.TabProductos_SC.Size = new System.Drawing.Size(1458, 802);
			// 
			// Nombre_TB
			// 
			this.Nombre_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Nombre_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Ubicacion_TB
			// 
			this.Ubicacion_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Ubicacion_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// Estado_BT
			// 
			this.Estado_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Estado_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Estado_BT.Click += new System.EventHandler(this.Estado_BT_Click);
			// 
			// Estado_TB
			// 
			this.Estado_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Estado_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Estado_TB.ForeColor = System.Drawing.Color.Black;
			this.Estado_TB.TabStop = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F);
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
			this.Submit_BT.Location = new System.Drawing.Point(82, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(172, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// AlmacenUIForm
			// 
			this.ClientSize = new System.Drawing.Size(515, 233);
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AlmacenUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Almacén";
			this.Ficha.ResumeLayout(false);
			this.TabDatos.ResumeLayout(false);
			this.TabDatos.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Partidas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Stock)).EndInit();
			this.TabStock.ResumeLayout(false);
			this.TabStock_SC.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Resumen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).EndInit();
			this.TabProductos.ResumeLayout(false);
			this.TabProductos_SC.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			
			
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;

    }
}
