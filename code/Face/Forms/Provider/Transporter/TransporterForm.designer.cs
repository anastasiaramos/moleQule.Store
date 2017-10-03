namespace moleQule.Face.Store
{
    partial class TransporterForm
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
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label diasPagoLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterForm));
            this.Datos_MedioPago = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_FormaPago = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_PrecioOrigen = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_PrecioDestino = new System.Windows.Forms.BindingSource(this.components);
            this.PrecioDestino_TP = new System.Windows.Forms.TabPage();
            this.PreciosDestino_Panel = new System.Windows.Forms.SplitContainer();
            this.Destino_TS = new System.Windows.Forms.ToolStrip();
            this.AddDestino_TI = new System.Windows.Forms.ToolStripButton();
            this.EditDestino_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteDestino_TI = new System.Windows.Forms.ToolStripButton();
            this.PrecioDestino_DGW = new System.Windows.Forms.DataGridView();
            this.PrecioOrigen_TP = new System.Windows.Forms.TabPage();
            this.PreciosOrigen_Panel = new System.Windows.Forms.SplitContainer();
            this.Origen_TS = new System.Windows.Forms.ToolStrip();
            this.AddOrigen_TI = new System.Windows.Forms.ToolStripButton();
            this.EditOrigen_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteOrigen_TI = new System.Windows.Forms.ToolStripButton();
            this.PrecioOrigen_DGW = new System.Windows.Forms.DataGridView();
            this.OrigenProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrigenPuerto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrigenPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion_TP = new System.Windows.Forms.TabPage();
            this.Financieros_GB = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.Swift_TB = new System.Windows.Forms.TextBox();
            this.IRPF_TB = new moleQule.Face.Controls.NumericTextBox();
            this.Tarjeta_BT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TarjetaAsociada_TB = new System.Windows.Forms.TextBox();
            this.CuentaContable_TB = new System.Windows.Forms.MaskedTextBox();
            this.CuentaContable_BT = new System.Windows.Forms.Button();
            this.DefectoVenta_BT = new System.Windows.Forms.Button();
            this.Impuesto_BT = new System.Windows.Forms.Button();
            this.Impuesto_TB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.MedioPago_CB = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CuentaAjena_BT = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.FormaPago_CB = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.DiasPago_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.CuentaAjena_TB = new System.Windows.Forms.TextBox();
            this.CuentaPropia_TB = new System.Windows.Forms.TextBox();
            this.Localizacion_GB = new System.Windows.Forms.GroupBox();
            this.SendMail_BT = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Telefono_TB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Municipio_TB = new System.Windows.Forms.TextBox();
            this.Localidad_BT = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Localidad_TB = new System.Windows.Forms.TextBox();
            this.CodPostal_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Provincia_TB = new System.Windows.Forms.TextBox();
            this.Observaciones_GB = new System.Windows.Forms.GroupBox();
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.Generales_GB = new System.Windows.Forms.GroupBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.ProviderType_CB = new System.Windows.Forms.ComboBox();
            this.ProviderType_BS = new System.Windows.Forms.BindingSource(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.MascaraID_Label = new System.Windows.Forms.Label();
            this.TipoID_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TipoID = new System.Windows.Forms.BindingSource(this.components);
            this.ID_LB = new System.Windows.Forms.Label();
            this.ID_TB = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.Pestanas_TC = new System.Windows.Forms.TabControl();
            this.Productos_TP = new System.Windows.Forms.TabPage();
            this.Productos_Panel = new System.Windows.Forms.SplitContainer();
            this.Productos_TS = new System.Windows.Forms.ToolStrip();
            this.AddProducto_TI = new System.Windows.Forms.ToolStripButton();
            this.EditProducto_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteProducto_TI = new System.Windows.Forms.ToolStripButton();
            this.Productos_DGW = new System.Windows.Forms.DataGridView();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Automatico = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CodigoArticuloAcreedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDescuentoLabel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FacturacionPeso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PrecioProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Impuesto = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PImpuestos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datos_Productos = new System.Windows.Forms.BindingSource(this.components);
            this.DestinoNumeroCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinoNombreCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinoPuerto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label17 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            diasPagoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PrecioOrigen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PrecioDestino)).BeginInit();
            this.PrecioDestino_TP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreciosDestino_Panel)).BeginInit();
            this.PreciosDestino_Panel.Panel1.SuspendLayout();
            this.PreciosDestino_Panel.Panel2.SuspendLayout();
            this.PreciosDestino_Panel.SuspendLayout();
            this.Destino_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrecioDestino_DGW)).BeginInit();
            this.PrecioOrigen_TP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreciosOrigen_Panel)).BeginInit();
            this.PreciosOrigen_Panel.Panel1.SuspendLayout();
            this.PreciosOrigen_Panel.Panel2.SuspendLayout();
            this.PreciosOrigen_Panel.SuspendLayout();
            this.Origen_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrecioOrigen_DGW)).BeginInit();
            this.Informacion_TP.SuspendLayout();
            this.Financieros_GB.SuspendLayout();
            this.Localizacion_GB.SuspendLayout();
            this.Observaciones_GB.SuspendLayout();
            this.Generales_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProviderType_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoID)).BeginInit();
            this.Pestanas_TC.SuspendLayout();
            this.Productos_TP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Productos_Panel)).BeginInit();
            this.Productos_Panel.Panel1.SuspendLayout();
            this.Productos_Panel.Panel2.SuspendLayout();
            this.Productos_Panel.SuspendLayout();
            this.Productos_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Productos_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.Pestanas_TC);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(944, 672);
            this.PanelesV.SplitterDistance = 617;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(178, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(267, 7);
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
            this.Paneles2.Size = new System.Drawing.Size(942, 52);
            this.Paneles2.SplitterDistance = 30;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(356, 7);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(300, 6);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.Transporter);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(293, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(944, 672);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(435, 384);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(435, 299);
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label17.Location = new System.Drawing.Point(131, 25);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(99, 13);
            label17.TabIndex = 40;
            label17.Text = "Tipo de Documento";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(33, 141);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 13);
            label1.TabIndex = 166;
            label1.Text = "Tipo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(248, 141);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(44, 13);
            label7.TabIndex = 174;
            label7.Text = "Estado:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label22.Location = new System.Drawing.Point(539, 147);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(49, 13);
            label22.TabIndex = 174;
            label22.Text = "IRPF(%)";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label18.Location = new System.Drawing.Point(257, 148);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(52, 13);
            label18.TabIndex = 134;
            label18.Text = "Impuesto";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label16.Location = new System.Drawing.Point(43, 148);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(88, 13);
            label16.TabIndex = 123;
            label16.Text = "Cuenta Contable";
            // 
            // diasPagoLabel
            // 
            diasPagoLabel.AutoSize = true;
            diasPagoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            diasPagoLabel.Location = new System.Drawing.Point(345, 27);
            diasPagoLabel.Name = "diasPagoLabel";
            diasPagoLabel.Size = new System.Drawing.Size(54, 13);
            diasPagoLabel.TabIndex = 35;
            diasPagoLabel.Text = "Días Pago";
            // 
            // Datos_MedioPago
            // 
            this.Datos_MedioPago.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // Datos_FormaPago
            // 
            this.Datos_FormaPago.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // Datos_PrecioOrigen
            // 
            this.Datos_PrecioOrigen.DataSource = typeof(moleQule.Library.Store.PrecioOrigen);
            // 
            // Datos_PrecioDestino
            // 
            this.Datos_PrecioDestino.DataSource = typeof(moleQule.Library.Store.PrecioDestino);
            // 
            // PrecioDestino_TP
            // 
            this.PrecioDestino_TP.Controls.Add(this.PreciosDestino_Panel);
            this.PrecioDestino_TP.Location = new System.Drawing.Point(4, 34);
            this.PrecioDestino_TP.Name = "PrecioDestino_TP";
            this.PrecioDestino_TP.Size = new System.Drawing.Size(934, 577);
            this.PrecioDestino_TP.TabIndex = 2;
            this.PrecioDestino_TP.Text = "Precios en Destino";
            this.PrecioDestino_TP.UseVisualStyleBackColor = true;
            // 
            // PreciosDestino_Panel
            // 
            this.PreciosDestino_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreciosDestino_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.PreciosDestino_Panel.Location = new System.Drawing.Point(0, 0);
            this.PreciosDestino_Panel.Name = "PreciosDestino_Panel";
            this.PreciosDestino_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PreciosDestino_Panel.Panel1
            // 
            this.PreciosDestino_Panel.Panel1.AutoScroll = true;
            this.PreciosDestino_Panel.Panel1.Controls.Add(this.Destino_TS);
            this.PreciosDestino_Panel.Panel1MinSize = 39;
            // 
            // PreciosDestino_Panel.Panel2
            // 
            this.PreciosDestino_Panel.Panel2.Controls.Add(this.PrecioDestino_DGW);
            this.PreciosDestino_Panel.Size = new System.Drawing.Size(934, 577);
            this.PreciosDestino_Panel.SplitterDistance = 39;
            this.PreciosDestino_Panel.TabIndex = 0;
            // 
            // Destino_TS
            // 
            this.Destino_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Destino_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddDestino_TI,
            this.EditDestino_TI,
            this.DeleteDestino_TI});
            this.Destino_TS.Location = new System.Drawing.Point(0, 0);
            this.Destino_TS.Name = "Destino_TS";
            this.HelpProvider.SetShowHelp(this.Destino_TS, true);
            this.Destino_TS.Size = new System.Drawing.Size(934, 39);
            this.Destino_TS.TabIndex = 5;
            this.Destino_TS.Text = "Imprimir";
            // 
            // AddDestino_TI
            // 
            this.AddDestino_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddDestino_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddDestino_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddDestino_TI.Name = "AddDestino_TI";
            this.AddDestino_TI.Size = new System.Drawing.Size(36, 36);
            this.AddDestino_TI.Text = "Nuevo";
            this.AddDestino_TI.Click += new System.EventHandler(this.AddDestino_TI_Click);
            // 
            // EditDestino_TI
            // 
            this.EditDestino_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditDestino_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditDestino_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditDestino_TI.Name = "EditDestino_TI";
            this.EditDestino_TI.Size = new System.Drawing.Size(36, 36);
            this.EditDestino_TI.Text = "Editar";
            this.EditDestino_TI.Click += new System.EventHandler(this.EditDestino_TI_Click);
            // 
            // DeleteDestino_TI
            // 
            this.DeleteDestino_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteDestino_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            this.DeleteDestino_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteDestino_TI.Name = "DeleteDestino_TI";
            this.DeleteDestino_TI.Size = new System.Drawing.Size(36, 36);
            this.DeleteDestino_TI.Text = "Borrar";
            this.DeleteDestino_TI.Click += new System.EventHandler(this.DeleteDestino_TI_Click);
            // 
            // PrecioDestino_DGW
            // 
            this.PrecioDestino_DGW.AllowUserToAddRows = false;
            this.PrecioDestino_DGW.AllowUserToDeleteRows = false;
            this.PrecioDestino_DGW.AutoGenerateColumns = false;
            this.PrecioDestino_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DestinoNumeroCliente,
            this.DestinoNombreCliente,
            this.DestinoPuerto,
            this.DestinoPrecio});
            this.PrecioDestino_DGW.DataSource = this.Datos_PrecioDestino;
            this.PrecioDestino_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrecioDestino_DGW.Location = new System.Drawing.Point(0, 0);
            this.PrecioDestino_DGW.Name = "PrecioDestino_DGW";
            this.PrecioDestino_DGW.ReadOnly = true;
            this.PrecioDestino_DGW.Size = new System.Drawing.Size(934, 534);
            this.PrecioDestino_DGW.TabIndex = 1;
            this.PrecioDestino_DGW.DoubleClick += new System.EventHandler(this.PrecioDestino_DG_DoubleClick);
            // 
            // PrecioOrigen_TP
            // 
            this.PrecioOrigen_TP.Controls.Add(this.PreciosOrigen_Panel);
            this.PrecioOrigen_TP.Location = new System.Drawing.Point(4, 34);
            this.PrecioOrigen_TP.Name = "PrecioOrigen_TP";
            this.PrecioOrigen_TP.Padding = new System.Windows.Forms.Padding(3);
            this.PrecioOrigen_TP.Size = new System.Drawing.Size(934, 577);
            this.PrecioOrigen_TP.TabIndex = 1;
            this.PrecioOrigen_TP.Text = "Precios en Origen";
            this.PrecioOrigen_TP.UseVisualStyleBackColor = true;
            // 
            // PreciosOrigen_Panel
            // 
            this.PreciosOrigen_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreciosOrigen_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.PreciosOrigen_Panel.Location = new System.Drawing.Point(3, 3);
            this.PreciosOrigen_Panel.Name = "PreciosOrigen_Panel";
            this.PreciosOrigen_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PreciosOrigen_Panel.Panel1
            // 
            this.PreciosOrigen_Panel.Panel1.AutoScroll = true;
            this.PreciosOrigen_Panel.Panel1.Controls.Add(this.Origen_TS);
            this.PreciosOrigen_Panel.Panel1MinSize = 39;
            // 
            // PreciosOrigen_Panel.Panel2
            // 
            this.PreciosOrigen_Panel.Panel2.Controls.Add(this.PrecioOrigen_DGW);
            this.PreciosOrigen_Panel.Size = new System.Drawing.Size(928, 571);
            this.PreciosOrigen_Panel.SplitterDistance = 39;
            this.PreciosOrigen_Panel.SplitterWidth = 1;
            this.PreciosOrigen_Panel.TabIndex = 0;
            // 
            // Origen_TS
            // 
            this.Origen_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Origen_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddOrigen_TI,
            this.EditOrigen_TI,
            this.DeleteOrigen_TI});
            this.Origen_TS.Location = new System.Drawing.Point(0, 0);
            this.Origen_TS.Name = "Origen_TS";
            this.HelpProvider.SetShowHelp(this.Origen_TS, true);
            this.Origen_TS.Size = new System.Drawing.Size(928, 39);
            this.Origen_TS.TabIndex = 4;
            this.Origen_TS.Text = "Imprimir";
            // 
            // AddOrigen_TI
            // 
            this.AddOrigen_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddOrigen_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddOrigen_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddOrigen_TI.Name = "AddOrigen_TI";
            this.AddOrigen_TI.Size = new System.Drawing.Size(36, 36);
            this.AddOrigen_TI.Text = "Nuevo";
            this.AddOrigen_TI.Click += new System.EventHandler(this.AddOrigen_TI_Click);
            // 
            // EditOrigen_TI
            // 
            this.EditOrigen_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditOrigen_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditOrigen_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditOrigen_TI.Name = "EditOrigen_TI";
            this.EditOrigen_TI.Size = new System.Drawing.Size(36, 36);
            this.EditOrigen_TI.Text = "Editar";
            this.EditOrigen_TI.Click += new System.EventHandler(this.EditOrigen_TI_Click);
            // 
            // DeleteOrigen_TI
            // 
            this.DeleteOrigen_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteOrigen_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            this.DeleteOrigen_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteOrigen_TI.Name = "DeleteOrigen_TI";
            this.DeleteOrigen_TI.Size = new System.Drawing.Size(36, 36);
            this.DeleteOrigen_TI.Text = "Borrar";
            this.DeleteOrigen_TI.Click += new System.EventHandler(this.DeleteOrigen_TI_Click);
            // 
            // PrecioOrigen_DGW
            // 
            this.PrecioOrigen_DGW.AllowUserToAddRows = false;
            this.PrecioOrigen_DGW.AllowUserToDeleteRows = false;
            this.PrecioOrigen_DGW.AutoGenerateColumns = false;
            this.PrecioOrigen_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrigenProveedor,
            this.OrigenPuerto,
            this.OrigenPrecio});
            this.PrecioOrigen_DGW.DataSource = this.Datos_PrecioOrigen;
            this.PrecioOrigen_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrecioOrigen_DGW.Location = new System.Drawing.Point(0, 0);
            this.PrecioOrigen_DGW.Name = "PrecioOrigen_DGW";
            this.PrecioOrigen_DGW.ReadOnly = true;
            this.PrecioOrigen_DGW.Size = new System.Drawing.Size(928, 531);
            this.PrecioOrigen_DGW.TabIndex = 1;
            this.PrecioOrigen_DGW.DoubleClick += new System.EventHandler(this.PrecioOrigen_DG_DoubleClick);
            // 
            // OrigenProveedor
            // 
            this.OrigenProveedor.DataPropertyName = "Proveedor";
            this.OrigenProveedor.HeaderText = "Proveedor";
            this.OrigenProveedor.Name = "OrigenProveedor";
            this.OrigenProveedor.ReadOnly = true;
            this.OrigenProveedor.Width = 75;
            // 
            // OrigenPuerto
            // 
            this.OrigenPuerto.DataPropertyName = "Puerto";
            this.OrigenPuerto.HeaderText = "Puerto";
            this.OrigenPuerto.Name = "OrigenPuerto";
            this.OrigenPuerto.ReadOnly = true;
            // 
            // OrigenPrecio
            // 
            this.OrigenPrecio.DataPropertyName = "Precio";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.OrigenPrecio.DefaultCellStyle = dataGridViewCellStyle5;
            this.OrigenPrecio.HeaderText = "Precio";
            this.OrigenPrecio.Name = "OrigenPrecio";
            this.OrigenPrecio.ReadOnly = true;
            this.OrigenPrecio.Width = 75;
            // 
            // Informacion_TP
            // 
            this.Informacion_TP.AutoScroll = true;
            this.Informacion_TP.Controls.Add(this.Financieros_GB);
            this.Informacion_TP.Controls.Add(this.Localizacion_GB);
            this.Informacion_TP.Controls.Add(this.Observaciones_GB);
            this.Informacion_TP.Controls.Add(this.Generales_GB);
            this.Informacion_TP.Location = new System.Drawing.Point(4, 34);
            this.Informacion_TP.Name = "Informacion_TP";
            this.Informacion_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Informacion_TP.Size = new System.Drawing.Size(934, 577);
            this.Informacion_TP.TabIndex = 0;
            this.Informacion_TP.Text = "Información";
            this.Informacion_TP.UseVisualStyleBackColor = true;
            // 
            // Financieros_GB
            // 
            this.Financieros_GB.Controls.Add(this.label27);
            this.Financieros_GB.Controls.Add(this.Swift_TB);
            this.Financieros_GB.Controls.Add(label22);
            this.Financieros_GB.Controls.Add(this.IRPF_TB);
            this.Financieros_GB.Controls.Add(this.Tarjeta_BT);
            this.Financieros_GB.Controls.Add(this.label4);
            this.Financieros_GB.Controls.Add(this.TarjetaAsociada_TB);
            this.Financieros_GB.Controls.Add(this.CuentaContable_TB);
            this.Financieros_GB.Controls.Add(this.CuentaContable_BT);
            this.Financieros_GB.Controls.Add(this.DefectoVenta_BT);
            this.Financieros_GB.Controls.Add(this.Impuesto_BT);
            this.Financieros_GB.Controls.Add(label18);
            this.Financieros_GB.Controls.Add(this.Impuesto_TB);
            this.Financieros_GB.Controls.Add(label16);
            this.Financieros_GB.Controls.Add(this.label12);
            this.Financieros_GB.Controls.Add(this.MedioPago_CB);
            this.Financieros_GB.Controls.Add(this.label9);
            this.Financieros_GB.Controls.Add(this.CuentaAjena_BT);
            this.Financieros_GB.Controls.Add(this.label11);
            this.Financieros_GB.Controls.Add(this.FormaPago_CB);
            this.Financieros_GB.Controls.Add(this.label23);
            this.Financieros_GB.Controls.Add(this.DiasPago_NTB);
            this.Financieros_GB.Controls.Add(this.CuentaAjena_TB);
            this.Financieros_GB.Controls.Add(diasPagoLabel);
            this.Financieros_GB.Controls.Add(this.CuentaPropia_TB);
            this.Financieros_GB.Location = new System.Drawing.Point(24, 350);
            this.Financieros_GB.Name = "Financieros_GB";
            this.Financieros_GB.Size = new System.Drawing.Size(887, 208);
            this.Financieros_GB.TabIndex = 168;
            this.Financieros_GB.TabStop = false;
            this.Financieros_GB.Text = "Financieros / Contables";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(294, 78);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(31, 13);
            this.label27.TabIndex = 176;
            this.label27.Text = "Swift";
            // 
            // Swift_TB
            // 
            this.Swift_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Swift", true));
            this.Swift_TB.Location = new System.Drawing.Point(297, 94);
            this.Swift_TB.Multiline = true;
            this.Swift_TB.Name = "Swift_TB";
            this.Swift_TB.Size = new System.Drawing.Size(223, 42);
            this.Swift_TB.TabIndex = 175;
            // 
            // IRPF_TB
            // 
            this.IRPF_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PIRPF", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.IRPF_TB.Location = new System.Drawing.Point(542, 163);
            this.IRPF_TB.Name = "IRPF_TB";
            this.IRPF_TB.Size = new System.Drawing.Size(50, 21);
            this.IRPF_TB.TabIndex = 173;
            this.IRPF_TB.TabStop = false;
            this.IRPF_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IRPF_TB.TextIsCurrency = false;
            this.IRPF_TB.TextIsInteger = false;
            // 
            // Tarjeta_BT
            // 
            this.Tarjeta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Tarjeta_BT.Location = new System.Drawing.Point(829, 135);
            this.Tarjeta_BT.Name = "Tarjeta_BT";
            this.Tarjeta_BT.Size = new System.Drawing.Size(29, 22);
            this.Tarjeta_BT.TabIndex = 171;
            this.Tarjeta_BT.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(620, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 13);
            this.label4.TabIndex = 172;
            this.label4.Text = "Tarjeta de Crédito (Cargo en tarjeta)";
            // 
            // TarjetaAsociada_TB
            // 
            this.TarjetaAsociada_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TarjetaAsociada", true));
            this.TarjetaAsociada_TB.Location = new System.Drawing.Point(623, 136);
            this.TarjetaAsociada_TB.Name = "TarjetaAsociada_TB";
            this.TarjetaAsociada_TB.ReadOnly = true;
            this.TarjetaAsociada_TB.Size = new System.Drawing.Size(200, 21);
            this.TarjetaAsociada_TB.TabIndex = 170;
            // 
            // CuentaContable_TB
            // 
            this.CuentaContable_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContable", true));
            this.CuentaContable_TB.Location = new System.Drawing.Point(44, 164);
            this.CuentaContable_TB.Name = "CuentaContable_TB";
            this.CuentaContable_TB.Size = new System.Drawing.Size(126, 21);
            this.CuentaContable_TB.TabIndex = 169;
            this.CuentaContable_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CuentaContable_BT
            // 
            this.CuentaContable_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.CuentaContable_BT.Location = new System.Drawing.Point(176, 163);
            this.CuentaContable_BT.Name = "CuentaContable_BT";
            this.CuentaContable_BT.Size = new System.Drawing.Size(29, 22);
            this.CuentaContable_BT.TabIndex = 168;
            this.CuentaContable_BT.UseVisualStyleBackColor = true;
            // 
            // DefectoVenta_BT
            // 
            this.DefectoVenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.close_16;
            this.DefectoVenta_BT.Location = new System.Drawing.Point(473, 163);
            this.DefectoVenta_BT.Name = "DefectoVenta_BT";
            this.DefectoVenta_BT.Size = new System.Drawing.Size(29, 22);
            this.DefectoVenta_BT.TabIndex = 145;
            this.DefectoVenta_BT.UseVisualStyleBackColor = true;
            // 
            // Impuesto_BT
            // 
            this.Impuesto_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Impuesto_BT.Location = new System.Drawing.Point(438, 163);
            this.Impuesto_BT.Name = "Impuesto_BT";
            this.Impuesto_BT.Size = new System.Drawing.Size(29, 22);
            this.Impuesto_BT.TabIndex = 135;
            this.Impuesto_BT.UseVisualStyleBackColor = true;
            // 
            // Impuesto_TB
            // 
            this.Impuesto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuesto", true));
            this.Impuesto_TB.Location = new System.Drawing.Point(260, 164);
            this.Impuesto_TB.Name = "Impuesto_TB";
            this.Impuesto_TB.ReadOnly = true;
            this.Impuesto_TB.Size = new System.Drawing.Size(171, 21);
            this.Impuesto_TB.TabIndex = 133;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(456, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 121;
            this.label12.Text = "Medio de Pago";
            // 
            // MedioPago_CB
            // 
            this.MedioPago_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "MedioPago", true));
            this.MedioPago_CB.DataSource = this.Datos_MedioPago;
            this.MedioPago_CB.DisplayMember = "Texto";
            this.MedioPago_CB.FormattingEnabled = true;
            this.MedioPago_CB.Location = new System.Drawing.Point(457, 44);
            this.MedioPago_CB.Name = "MedioPago_CB";
            this.MedioPago_CB.Size = new System.Drawing.Size(240, 21);
            this.MedioPago_CB.TabIndex = 2;
            this.MedioPago_CB.ValueMember = "Oid";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(41, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Forma de Pago";
            // 
            // CuentaAjena_BT
            // 
            this.CuentaAjena_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.CuentaAjena_BT.Location = new System.Drawing.Point(829, 93);
            this.CuentaAjena_BT.Name = "CuentaAjena_BT";
            this.CuentaAjena_BT.Size = new System.Drawing.Size(29, 22);
            this.CuentaAjena_BT.TabIndex = 5;
            this.CuentaAjena_BT.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(41, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "IBAN";
            // 
            // FormaPago_CB
            // 
            this.FormaPago_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "FormaPago", true));
            this.FormaPago_CB.DataSource = this.Datos_FormaPago;
            this.FormaPago_CB.DisplayMember = "Texto";
            this.FormaPago_CB.FormattingEnabled = true;
            this.FormaPago_CB.Location = new System.Drawing.Point(44, 44);
            this.FormaPago_CB.Name = "FormaPago_CB";
            this.FormaPago_CB.Size = new System.Drawing.Size(240, 21);
            this.FormaPago_CB.TabIndex = 0;
            this.FormaPago_CB.ValueMember = "Oid";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(620, 78);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(169, 13);
            this.label23.TabIndex = 39;
            this.label23.Text = "Cuenta Asociada (Domiciliaciones)";
            // 
            // DiasPago_NTB
            // 
            this.DiasPago_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "DiasPago", true));
            this.DiasPago_NTB.Location = new System.Drawing.Point(344, 44);
            this.DiasPago_NTB.Name = "DiasPago_NTB";
            this.DiasPago_NTB.Size = new System.Drawing.Size(44, 21);
            this.DiasPago_NTB.TabIndex = 1;
            this.DiasPago_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DiasPago_NTB.TextIsCurrency = false;
            this.DiasPago_NTB.TextIsInteger = true;
            // 
            // CuentaAjena_TB
            // 
            this.CuentaAjena_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaAsociada", true));
            this.CuentaAjena_TB.Location = new System.Drawing.Point(623, 94);
            this.CuentaAjena_TB.Name = "CuentaAjena_TB";
            this.CuentaAjena_TB.ReadOnly = true;
            this.CuentaAjena_TB.Size = new System.Drawing.Size(200, 21);
            this.CuentaAjena_TB.TabIndex = 4;
            // 
            // CuentaPropia_TB
            // 
            this.CuentaPropia_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaBancaria", true));
            this.CuentaPropia_TB.Location = new System.Drawing.Point(44, 93);
            this.CuentaPropia_TB.Multiline = true;
            this.CuentaPropia_TB.Name = "CuentaPropia_TB";
            this.CuentaPropia_TB.Size = new System.Drawing.Size(221, 42);
            this.CuentaPropia_TB.TabIndex = 3;
            // 
            // Localizacion_GB
            // 
            this.Localizacion_GB.Controls.Add(this.SendMail_BT);
            this.Localizacion_GB.Controls.Add(this.textBox3);
            this.Localizacion_GB.Controls.Add(this.label14);
            this.Localizacion_GB.Controls.Add(this.Telefono_TB);
            this.Localizacion_GB.Controls.Add(this.label8);
            this.Localizacion_GB.Controls.Add(this.Municipio_TB);
            this.Localizacion_GB.Controls.Add(this.Localidad_BT);
            this.Localizacion_GB.Controls.Add(this.textBox4);
            this.Localizacion_GB.Controls.Add(this.label15);
            this.Localizacion_GB.Controls.Add(this.textBox2);
            this.Localizacion_GB.Controls.Add(this.label13);
            this.Localizacion_GB.Controls.Add(this.textBox1);
            this.Localizacion_GB.Controls.Add(this.label10);
            this.Localizacion_GB.Controls.Add(this.Localidad_TB);
            this.Localizacion_GB.Controls.Add(this.CodPostal_TB);
            this.Localizacion_GB.Controls.Add(this.label2);
            this.Localizacion_GB.Controls.Add(this.label3);
            this.Localizacion_GB.Controls.Add(this.label6);
            this.Localizacion_GB.Controls.Add(this.label5);
            this.Localizacion_GB.Controls.Add(this.Provincia_TB);
            this.Localizacion_GB.Location = new System.Drawing.Point(478, 18);
            this.Localizacion_GB.Name = "Localizacion_GB";
            this.Localizacion_GB.Size = new System.Drawing.Size(433, 315);
            this.Localizacion_GB.TabIndex = 168;
            this.Localizacion_GB.TabStop = false;
            this.Localizacion_GB.Text = "Localización y Contacto";
            // 
            // SendMail_BT
            // 
            this.SendMail_BT.Image = global::moleQule.Face.Store.Properties.Resources.send_mail_16;
            this.SendMail_BT.Location = new System.Drawing.Point(385, 219);
            this.SendMail_BT.Name = "SendMail_BT";
            this.SendMail_BT.Size = new System.Drawing.Size(29, 22);
            this.SendMail_BT.TabIndex = 49;
            this.SendMail_BT.UseVisualStyleBackColor = true;
            this.SendMail_BT.Click += new System.EventHandler(this.SendMail_BT_Click);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Email", true));
            this.textBox3.Location = new System.Drawing.Point(197, 220);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 21);
            this.textBox3.TabIndex = 46;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(197, 201);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "Email";
            // 
            // Telefono_TB
            // 
            this.Telefono_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Telefono", true));
            this.Telefono_TB.Location = new System.Drawing.Point(18, 220);
            this.Telefono_TB.Name = "Telefono_TB";
            this.Telefono_TB.Size = new System.Drawing.Size(173, 21);
            this.Telefono_TB.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Teléfonos";
            // 
            // Municipio_TB
            // 
            this.Municipio_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Municipio", true));
            this.Municipio_TB.Location = new System.Drawing.Point(18, 128);
            this.Municipio_TB.Name = "Municipio_TB";
            this.Municipio_TB.Size = new System.Drawing.Size(309, 21);
            this.Municipio_TB.TabIndex = 3;
            // 
            // Localidad_BT
            // 
            this.Localidad_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Localidad_BT.Location = new System.Drawing.Point(385, 82);
            this.Localidad_BT.Name = "Localidad_BT";
            this.Localidad_BT.Size = new System.Drawing.Size(29, 21);
            this.Localidad_BT.TabIndex = 2;
            this.Localidad_BT.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Direccion", true));
            this.textBox4.Location = new System.Drawing.Point(18, 42);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(396, 21);
            this.textBox4.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(18, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "Dirección";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Contacto", true));
            this.textBox2.Location = new System.Drawing.Point(18, 269);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(396, 21);
            this.textBox2.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(18, 249);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Persona de Contacto";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Pais", true));
            this.textBox1.Location = new System.Drawing.Point(256, 171);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 21);
            this.textBox1.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(256, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "País";
            // 
            // Localidad_TB
            // 
            this.Localidad_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Localidad", true));
            this.Localidad_TB.Location = new System.Drawing.Point(18, 82);
            this.Localidad_TB.Name = "Localidad_TB";
            this.Localidad_TB.Size = new System.Drawing.Size(361, 21);
            this.Localidad_TB.TabIndex = 1;
            // 
            // CodPostal_TB
            // 
            this.CodPostal_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CodPostal", true));
            this.CodPostal_TB.Location = new System.Drawing.Point(345, 128);
            this.CodPostal_TB.Name = "CodPostal_TB";
            this.CodPostal_TB.Size = new System.Drawing.Size(69, 21);
            this.CodPostal_TB.TabIndex = 4;
            this.CodPostal_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Municipio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Localidad";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Provincia";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(342, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Código Postal";
            // 
            // Provincia_TB
            // 
            this.Provincia_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Provincia", true));
            this.Provincia_TB.Location = new System.Drawing.Point(18, 172);
            this.Provincia_TB.Name = "Provincia_TB";
            this.Provincia_TB.Size = new System.Drawing.Size(224, 21);
            this.Provincia_TB.TabIndex = 5;
            // 
            // Observaciones_GB
            // 
            this.Observaciones_GB.Controls.Add(this.Observaciones_RTB);
            this.Observaciones_GB.Location = new System.Drawing.Point(27, 205);
            this.Observaciones_GB.Name = "Observaciones_GB";
            this.Observaciones_GB.Size = new System.Drawing.Size(432, 128);
            this.Observaciones_GB.TabIndex = 167;
            this.Observaciones_GB.TabStop = false;
            this.Observaciones_GB.Text = "Observaciones";
            // 
            // Observaciones_RTB
            // 
            this.Observaciones_RTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_RTB.Location = new System.Drawing.Point(16, 27);
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.Observaciones_RTB.Size = new System.Drawing.Size(400, 79);
            this.Observaciones_RTB.TabIndex = 0;
            this.Observaciones_RTB.Text = "";
            // 
            // Generales_GB
            // 
            this.Generales_GB.Controls.Add(this.Estado_BT);
            this.Generales_GB.Controls.Add(label7);
            this.Generales_GB.Controls.Add(this.Estado_TB);
            this.Generales_GB.Controls.Add(label1);
            this.Generales_GB.Controls.Add(this.ProviderType_CB);
            this.Generales_GB.Controls.Add(this.label19);
            this.Generales_GB.Controls.Add(this.textBox6);
            this.Generales_GB.Controls.Add(this.MascaraID_Label);
            this.Generales_GB.Controls.Add(label17);
            this.Generales_GB.Controls.Add(this.TipoID_CB);
            this.Generales_GB.Controls.Add(this.ID_LB);
            this.Generales_GB.Controls.Add(this.ID_TB);
            this.Generales_GB.Controls.Add(this.label20);
            this.Generales_GB.Controls.Add(this.textBox7);
            this.Generales_GB.Controls.Add(this.label21);
            this.Generales_GB.Controls.Add(this.textBox8);
            this.Generales_GB.Location = new System.Drawing.Point(26, 18);
            this.Generales_GB.Name = "Generales_GB";
            this.Generales_GB.Size = new System.Drawing.Size(433, 181);
            this.Generales_GB.TabIndex = 125;
            this.Generales_GB.TabStop = false;
            this.Generales_GB.Text = "Datos Generales";
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(387, 137);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(29, 21);
            this.Estado_BT.TabIndex = 175;
            this.Estado_BT.UseVisualStyleBackColor = true;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(298, 137);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(84, 21);
            this.Estado_TB.TabIndex = 173;
            // 
            // ProviderType_CB
            // 
            this.ProviderType_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "TipoAcreedor", true));
            this.ProviderType_CB.DataSource = this.ProviderType_BS;
            this.ProviderType_CB.DisplayMember = "Texto";
            this.ProviderType_CB.FormattingEnabled = true;
            this.ProviderType_CB.Location = new System.Drawing.Point(70, 137);
            this.ProviderType_CB.Name = "ProviderType_CB";
            this.ProviderType_CB.Size = new System.Drawing.Size(160, 21);
            this.ProviderType_CB.TabIndex = 165;
            this.ProviderType_CB.ValueMember = "Oid";
            // 
            // ProviderType_BS
            // 
            this.ProviderType_BS.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(31, 113);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(33, 13);
            this.label19.TabIndex = 164;
            this.label19.Text = "Alias:";
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Alias", true));
            this.textBox6.Location = new System.Drawing.Point(70, 109);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(349, 21);
            this.textBox6.TabIndex = 163;
            // 
            // MascaraID_Label
            // 
            this.MascaraID_Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MascaraID_Label.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.MascaraID_Label.Location = new System.Drawing.Point(307, 65);
            this.MascaraID_Label.Name = "MascaraID_Label";
            this.MascaraID_Label.Size = new System.Drawing.Size(103, 13);
            this.MascaraID_Label.TabIndex = 162;
            this.MascaraID_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TipoID_CB
            // 
            this.TipoID_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "TipoId", true));
            this.TipoID_CB.DataSource = this.Datos_TipoID;
            this.TipoID_CB.DisplayMember = "Texto";
            this.TipoID_CB.FormattingEnabled = true;
            this.TipoID_CB.Location = new System.Drawing.Point(134, 41);
            this.TipoID_CB.Name = "TipoID_CB";
            this.TipoID_CB.Size = new System.Drawing.Size(121, 21);
            this.TipoID_CB.TabIndex = 1;
            this.TipoID_CB.ValueMember = "Oid";
            // 
            // Datos_TipoID
            // 
            this.Datos_TipoID.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // ID_LB
            // 
            this.ID_LB.AutoSize = true;
            this.ID_LB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID_LB.Location = new System.Drawing.Point(295, 24);
            this.ID_LB.Name = "ID_LB";
            this.ID_LB.Size = new System.Drawing.Size(76, 13);
            this.ID_LB.TabIndex = 38;
            this.ID_LB.Text = "Nº Documento";
            // 
            // ID_TB
            // 
            this.ID_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ID", true));
            this.ID_TB.Location = new System.Drawing.Point(298, 41);
            this.ID_TB.Name = "ID_TB";
            this.ID_TB.Size = new System.Drawing.Size(121, 21);
            this.ID_TB.TabIndex = 0;
            this.ID_TB.Tag = "12345678-X";
            this.ID_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(16, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 13);
            this.label20.TabIndex = 36;
            this.label20.Text = "Nombre*:";
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
            this.textBox7.Location = new System.Drawing.Point(70, 82);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(349, 21);
            this.textBox7.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(14, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(18, 13);
            this.label21.TabIndex = 35;
            this.label21.Text = "ID";
            // 
            // textBox8
            // 
            this.textBox8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.textBox8.Location = new System.Drawing.Point(14, 42);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(85, 21);
            this.textBox8.TabIndex = 0;
            this.textBox8.Tag = "NO FORMAT";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Pestanas_TC
            // 
            this.Pestanas_TC.Controls.Add(this.Informacion_TP);
            this.Pestanas_TC.Controls.Add(this.Productos_TP);
            this.Pestanas_TC.Controls.Add(this.PrecioOrigen_TP);
            this.Pestanas_TC.Controls.Add(this.PrecioDestino_TP);
            this.Pestanas_TC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pestanas_TC.ItemSize = new System.Drawing.Size(100, 30);
            this.Pestanas_TC.Location = new System.Drawing.Point(0, 0);
            this.Pestanas_TC.Name = "Pestanas_TC";
            this.Pestanas_TC.SelectedIndex = 0;
            this.Pestanas_TC.Size = new System.Drawing.Size(942, 615);
            this.Pestanas_TC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Pestanas_TC.TabIndex = 9;
            // 
            // Productos_TP
            // 
            this.Productos_TP.Controls.Add(this.Productos_Panel);
            this.Productos_TP.Location = new System.Drawing.Point(4, 34);
            this.Productos_TP.Name = "Productos_TP";
            this.Productos_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Productos_TP.Size = new System.Drawing.Size(934, 577);
            this.Productos_TP.TabIndex = 3;
            this.Productos_TP.Text = "Productos";
            this.Productos_TP.UseVisualStyleBackColor = true;
            // 
            // Productos_Panel
            // 
            this.Productos_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Productos_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Productos_Panel.Location = new System.Drawing.Point(3, 3);
            this.Productos_Panel.Name = "Productos_Panel";
            this.Productos_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Productos_Panel.Panel1
            // 
            this.Productos_Panel.Panel1.Controls.Add(this.Productos_TS);
            this.Productos_Panel.Panel1MinSize = 39;
            // 
            // Productos_Panel.Panel2
            // 
            this.Productos_Panel.Panel2.Controls.Add(this.Productos_DGW);
            this.Productos_Panel.Size = new System.Drawing.Size(928, 571);
            this.Productos_Panel.SplitterDistance = 39;
            this.Productos_Panel.SplitterWidth = 1;
            this.Productos_Panel.TabIndex = 5;
            // 
            // Productos_TS
            // 
            this.Productos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Productos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddProducto_TI,
            this.EditProducto_TI,
            this.DeleteProducto_TI});
            this.Productos_TS.Location = new System.Drawing.Point(0, 0);
            this.Productos_TS.Name = "Productos_TS";
            this.HelpProvider.SetShowHelp(this.Productos_TS, true);
            this.Productos_TS.Size = new System.Drawing.Size(928, 39);
            this.Productos_TS.TabIndex = 5;
            this.Productos_TS.Text = "Imprimir";
            // 
            // AddProducto_TI
            // 
            this.AddProducto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddProducto_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddProducto_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddProducto_TI.Name = "AddProducto_TI";
            this.AddProducto_TI.Size = new System.Drawing.Size(36, 36);
            this.AddProducto_TI.Text = "Nuevo";
            this.AddProducto_TI.Click += new System.EventHandler(this.AddProducto_TI_Click);
            // 
            // EditProducto_TI
            // 
            this.EditProducto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditProducto_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditProducto_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditProducto_TI.Name = "EditProducto_TI";
            this.EditProducto_TI.Size = new System.Drawing.Size(36, 36);
            this.EditProducto_TI.Text = "Editar";
            this.EditProducto_TI.Visible = false;
            this.EditProducto_TI.Click += new System.EventHandler(this.EditProducto_TI_Click);
            // 
            // DeleteProducto_TI
            // 
            this.DeleteProducto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteProducto_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            this.DeleteProducto_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteProducto_TI.Name = "DeleteProducto_TI";
            this.DeleteProducto_TI.Size = new System.Drawing.Size(36, 36);
            this.DeleteProducto_TI.Text = "Borrar";
            this.DeleteProducto_TI.Click += new System.EventHandler(this.DeleteProducto_TI_Click);
            // 
            // Productos_DGW
            // 
            this.Productos_DGW.AllowUserToAddRows = false;
            this.Productos_DGW.AllowUserToDeleteRows = false;
            this.Productos_DGW.AllowUserToOrderColumns = true;
            this.Productos_DGW.AllowUserToResizeRows = false;
            this.Productos_DGW.AutoGenerateColumns = false;
            this.Productos_DGW.ColumnHeadersHeight = 34;
            this.Productos_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Producto,
            this.Automatico,
            this.CodigoArticuloAcreedor,
            this.TipoDescuentoLabel,
            this.FacturacionPeso,
            this.PrecioProducto,
            this.PDescuento,
            this.Impuesto,
            this.PImpuestos});
            this.Productos_DGW.DataSource = this.Datos_Productos;
            this.Productos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Productos_DGW.Location = new System.Drawing.Point(0, 0);
            this.Productos_DGW.Name = "Productos_DGW";
            this.Productos_DGW.RowHeadersWidth = 25;
            this.Productos_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Productos_DGW.Size = new System.Drawing.Size(928, 531);
            this.Productos_DGW.TabIndex = 1;
            this.Productos_DGW.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Productos_DGW_CellClick);
            // 
            // Producto
            // 
            this.Producto.DataPropertyName = "Producto";
            this.Producto.HeaderText = "Producto";
            this.Producto.MinimumWidth = 200;
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 200;
            // 
            // Automatico
            // 
            this.Automatico.DataPropertyName = "Automatico";
            this.Automatico.HeaderText = "Automático";
            this.Automatico.Name = "Automatico";
            this.Automatico.Width = 65;
            // 
            // CodigoArticuloAcreedor
            // 
            this.CodigoArticuloAcreedor.DataPropertyName = "CodigoArticuloAcreedor";
            this.CodigoArticuloAcreedor.HeaderText = "ID Artículo Proveedor";
            this.CodigoArticuloAcreedor.Name = "CodigoArticuloAcreedor";
            // 
            // TipoDescuentoLabel
            // 
            this.TipoDescuentoLabel.DataPropertyName = "TipoDescuentoLabel";
            this.TipoDescuentoLabel.HeaderText = "Tipo Descuento";
            this.TipoDescuentoLabel.Name = "TipoDescuentoLabel";
            this.TipoDescuentoLabel.ReadOnly = true;
            this.TipoDescuentoLabel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TipoDescuentoLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TipoDescuentoLabel.Width = 110;
            // 
            // FacturacionPeso
            // 
            this.FacturacionPeso.DataPropertyName = "FacturacionPeso";
            this.FacturacionPeso.HeaderText = "Fac. Peso";
            this.FacturacionPeso.Name = "FacturacionPeso";
            this.FacturacionPeso.Width = 40;
            // 
            // PrecioProducto
            // 
            this.PrecioProducto.DataPropertyName = "Precio";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C5";
            dataGridViewCellStyle1.NullValue = null;
            this.PrecioProducto.DefaultCellStyle = dataGridViewCellStyle1;
            this.PrecioProducto.HeaderText = "Precio";
            this.PrecioProducto.Name = "PrecioProducto";
            this.PrecioProducto.Width = 90;
            // 
            // PDescuento
            // 
            this.PDescuento.DataPropertyName = "PDescuento";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.PDescuento.DefaultCellStyle = dataGridViewCellStyle2;
            this.PDescuento.HeaderText = "% Dto.";
            this.PDescuento.Name = "PDescuento";
            this.PDescuento.Width = 40;
            // 
            // Impuesto
            // 
            this.Impuesto.DataPropertyName = "Impuesto";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            this.Impuesto.DefaultCellStyle = dataGridViewCellStyle3;
            this.Impuesto.HeaderText = "Impuesto";
            this.Impuesto.MinimumWidth = 150;
            this.Impuesto.Name = "Impuesto";
            this.Impuesto.Width = 150;
            // 
            // PImpuestos
            // 
            this.PImpuestos.DataPropertyName = "PImpuestos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.PImpuestos.DefaultCellStyle = dataGridViewCellStyle4;
            this.PImpuestos.HeaderText = "% Imp.";
            this.PImpuestos.Name = "PImpuestos";
            this.PImpuestos.Width = 40;
            // 
            // Datos_Productos
            // 
            this.Datos_Productos.DataSource = typeof(moleQule.Library.Store.ProductoProveedor);
            // 
            // DestinoNumeroCliente
            // 
            this.DestinoNumeroCliente.DataPropertyName = "CodigoCliente";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DestinoNumeroCliente.DefaultCellStyle = dataGridViewCellStyle6;
            this.DestinoNumeroCliente.HeaderText = "Nº Cliente";
            this.DestinoNumeroCliente.Name = "DestinoNumeroCliente";
            this.DestinoNumeroCliente.ReadOnly = true;
            this.DestinoNumeroCliente.Width = 75;
            // 
            // DestinoNombreCliente
            // 
            this.DestinoNombreCliente.DataPropertyName = "NombreCliente";
            this.DestinoNombreCliente.HeaderText = "Cliente";
            this.DestinoNombreCliente.Name = "DestinoNombreCliente";
            this.DestinoNombreCliente.ReadOnly = true;
            // 
            // DestinoPuerto
            // 
            this.DestinoPuerto.DataPropertyName = "Puerto";
            this.DestinoPuerto.HeaderText = "Puerto Destino";
            this.DestinoPuerto.Name = "DestinoPuerto";
            this.DestinoPuerto.ReadOnly = true;
            // 
            // DestinoPrecio
            // 
            this.DestinoPrecio.DataPropertyName = "Precio";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            this.DestinoPrecio.DefaultCellStyle = dataGridViewCellStyle7;
            this.DestinoPrecio.HeaderText = "Precio";
            this.DestinoPrecio.Name = "DestinoPrecio";
            this.DestinoPrecio.ReadOnly = true;
            this.DestinoPrecio.Width = 75;
            // 
            // TransporterForm
            // 
            this.ClientSize = new System.Drawing.Size(944, 672);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransporterForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "TransportistaForm";
            this.PanelesV.Panel1.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PrecioOrigen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_PrecioDestino)).EndInit();
            this.PrecioDestino_TP.ResumeLayout(false);
            this.PreciosDestino_Panel.Panel1.ResumeLayout(false);
            this.PreciosDestino_Panel.Panel1.PerformLayout();
            this.PreciosDestino_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreciosDestino_Panel)).EndInit();
            this.PreciosDestino_Panel.ResumeLayout(false);
            this.Destino_TS.ResumeLayout(false);
            this.Destino_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrecioDestino_DGW)).EndInit();
            this.PrecioOrigen_TP.ResumeLayout(false);
            this.PreciosOrigen_Panel.Panel1.ResumeLayout(false);
            this.PreciosOrigen_Panel.Panel1.PerformLayout();
            this.PreciosOrigen_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreciosOrigen_Panel)).EndInit();
            this.PreciosOrigen_Panel.ResumeLayout(false);
            this.Origen_TS.ResumeLayout(false);
            this.Origen_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrecioOrigen_DGW)).EndInit();
            this.Informacion_TP.ResumeLayout(false);
            this.Financieros_GB.ResumeLayout(false);
            this.Financieros_GB.PerformLayout();
            this.Localizacion_GB.ResumeLayout(false);
            this.Localizacion_GB.PerformLayout();
            this.Observaciones_GB.ResumeLayout(false);
            this.Generales_GB.ResumeLayout(false);
            this.Generales_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProviderType_BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoID)).EndInit();
            this.Pestanas_TC.ResumeLayout(false);
            this.Productos_TP.ResumeLayout(false);
            this.Productos_Panel.Panel1.ResumeLayout(false);
            this.Productos_Panel.Panel1.PerformLayout();
            this.Productos_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Productos_Panel)).EndInit();
            this.Productos_Panel.ResumeLayout(false);
            this.Productos_TS.ResumeLayout(false);
            this.Productos_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Productos_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource Datos_PrecioOrigen;
        protected System.Windows.Forms.BindingSource Datos_PrecioDestino;
        protected System.Windows.Forms.BindingSource Datos_FormaPago;
		protected System.Windows.Forms.BindingSource Datos_MedioPago;
        protected System.Windows.Forms.TabControl Pestanas_TC;
		protected System.Windows.Forms.TabPage Informacion_TP;
        protected System.Windows.Forms.TabPage PrecioOrigen_TP;
        protected System.Windows.Forms.SplitContainer PreciosOrigen_Panel;
        protected System.Windows.Forms.TabPage PrecioDestino_TP;
		protected System.Windows.Forms.SplitContainer PreciosDestino_Panel;
        protected System.Windows.Forms.DataGridView PrecioOrigen_DGW;
        protected System.Windows.Forms.ToolStrip Origen_TS;
        protected System.Windows.Forms.ToolStripButton AddOrigen_TI;
        protected System.Windows.Forms.ToolStripButton EditOrigen_TI;
        protected System.Windows.Forms.ToolStripButton DeleteOrigen_TI;
        protected System.Windows.Forms.ToolStrip Destino_TS;
        protected System.Windows.Forms.ToolStripButton AddDestino_TI;
        protected System.Windows.Forms.ToolStripButton EditDestino_TI;
        protected System.Windows.Forms.ToolStripButton DeleteDestino_TI;
        protected System.Windows.Forms.DataGridView PrecioDestino_DGW;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrigenProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrigenPuerto;
		private System.Windows.Forms.DataGridViewTextBoxColumn OrigenPrecio;
		protected System.Windows.Forms.GroupBox Generales_GB;
		protected System.Windows.Forms.Label label19;
		protected System.Windows.Forms.TextBox textBox6;
		protected System.Windows.Forms.Label MascaraID_Label;
		protected System.Windows.Forms.ComboBox TipoID_CB;
		protected System.Windows.Forms.Label ID_LB;
		protected System.Windows.Forms.TextBox ID_TB;
		protected System.Windows.Forms.Label label20;
		protected System.Windows.Forms.TextBox textBox7;
		protected System.Windows.Forms.Label label21;
		protected System.Windows.Forms.TextBox textBox8;
		protected System.Windows.Forms.BindingSource Datos_TipoID;
		protected System.Windows.Forms.ComboBox ProviderType_CB;
		protected System.Windows.Forms.BindingSource ProviderType_BS;
		private System.Windows.Forms.GroupBox Observaciones_GB;
		protected System.Windows.Forms.RichTextBox Observaciones_RTB;
		protected System.Windows.Forms.GroupBox Localizacion_GB;
		protected System.Windows.Forms.TextBox Municipio_TB;
		protected System.Windows.Forms.Button Localidad_BT;
		protected System.Windows.Forms.TextBox textBox4;
		protected System.Windows.Forms.Label label15;
		protected System.Windows.Forms.TextBox textBox2;
		protected System.Windows.Forms.Label label13;
		protected System.Windows.Forms.TextBox textBox1;
		protected System.Windows.Forms.Label label10;
		protected System.Windows.Forms.TextBox Localidad_TB;
		protected System.Windows.Forms.TextBox CodPostal_TB;
		protected System.Windows.Forms.Label label2;
		protected System.Windows.Forms.Label label3;
		protected System.Windows.Forms.Label label6;
		protected System.Windows.Forms.Label label5;
		protected System.Windows.Forms.TextBox Provincia_TB;
		private System.Windows.Forms.TabPage Productos_TP;
		protected System.Windows.Forms.SplitContainer Productos_Panel;
		protected System.Windows.Forms.ToolStrip Productos_TS;
		protected System.Windows.Forms.ToolStripButton AddProducto_TI;
		protected System.Windows.Forms.ToolStripButton EditProducto_TI;
		protected System.Windows.Forms.ToolStripButton DeleteProducto_TI;
		protected System.Windows.Forms.BindingSource Datos_Productos;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
		protected System.Windows.Forms.Button SendMail_BT;
		protected System.Windows.Forms.TextBox textBox3;
		protected System.Windows.Forms.Label label14;
		protected System.Windows.Forms.TextBox Telefono_TB;
		protected System.Windows.Forms.Label label8;
        protected System.Windows.Forms.DataGridView Productos_DGW;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Automatico;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoArticuloAcreedor;
        private System.Windows.Forms.DataGridViewButtonColumn TipoDescuentoLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FacturacionPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDescuento;
        private System.Windows.Forms.DataGridViewButtonColumn Impuesto;
		private System.Windows.Forms.DataGridViewTextBoxColumn PImpuestos;
		private System.Windows.Forms.GroupBox Financieros_GB;
		protected System.Windows.Forms.Label label27;
		protected System.Windows.Forms.TextBox Swift_TB;
		protected Controls.NumericTextBox IRPF_TB;
		protected System.Windows.Forms.Button Tarjeta_BT;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.TextBox TarjetaAsociada_TB;
		protected System.Windows.Forms.MaskedTextBox CuentaContable_TB;
		protected System.Windows.Forms.Button CuentaContable_BT;
		protected System.Windows.Forms.Button DefectoVenta_BT;
		protected System.Windows.Forms.Button Impuesto_BT;
		protected System.Windows.Forms.TextBox Impuesto_TB;
		protected System.Windows.Forms.Label label12;
		protected System.Windows.Forms.ComboBox MedioPago_CB;
		protected System.Windows.Forms.Label label9;
		protected System.Windows.Forms.Button CuentaAjena_BT;
		protected System.Windows.Forms.Label label11;
		protected System.Windows.Forms.ComboBox FormaPago_CB;
		protected System.Windows.Forms.Label label23;
		protected Controls.NumericTextBox DiasPago_NTB;
		protected System.Windows.Forms.TextBox CuentaAjena_TB;
		protected System.Windows.Forms.TextBox CuentaPropia_TB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinoNumeroCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinoNombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinoPuerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinoPrecio;

    }
}
