

namespace moleQule.Face.Store
{
    partial class PedidoProveedorForm
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
            System.Windows.Forms.Label numeroClienteLabel;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label baseImponibleLabel;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidoProveedorForm));
            this.Datos_Lineas = new System.Windows.Forms.BindingSource(this.components);
            this.Main_SC = new System.Windows.Forms.SplitContainer();
            this.PDescuento_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox2 = new moleQule.Face.Controls.NumericTextBox();
            this.Descuento_TB = new moleQule.Face.Controls.NumericTextBox();
            this.Impuestos_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Base_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Total_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Proveedor_GB = new System.Windows.Forms.GroupBox();
            this.Impuesto_TB = new System.Windows.Forms.TextBox();
            this.Datos_Acreedor = new System.Windows.Forms.BindingSource(this.components);
            this.Emisor_BT = new System.Windows.Forms.Button();
            this.Acreedor_TB = new System.Windows.Forms.TextBox();
            this.IDAcreedor_TB = new System.Windows.Forms.TextBox();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.Impresion_GB = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Observaciones_TB = new System.Windows.Forms.TextBox();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.Pedido_GB = new System.Windows.Forms.GroupBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Almacen_BT = new System.Windows.Forms.Button();
            this.Almacen_TB = new System.Windows.Forms.TextBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.Usuario_BT = new System.Windows.Forms.Button();
            this.Usuario_TB = new System.Windows.Forms.TextBox();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.IDManual_CkB = new System.Windows.Forms.CheckBox();
            this.IDPedido_TB = new System.Windows.Forms.TextBox();
            this.Conceptos_SC = new System.Windows.Forms.SplitContainer();
            this.Conceptos_TS = new System.Windows.Forms.ToolStrip();
            this.AddConcepto_TI = new System.Windows.Forms.ToolStripButton();
            this.Edit_TI = new System.Windows.Forms.ToolStripButton();
            this.View_TI = new System.Windows.Forms.ToolStripButton();
            this.Delete_TI = new System.Windows.Forms.ToolStripButton();
            this.Lineas_DGW = new System.Windows.Forms.DataGridView();
            this.CodigoProductoAcreedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expedient = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FacturacionPeso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LiPieces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pendienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pendienteBultosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pDescuentoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descuentoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PImpuestos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Impuestos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            numeroClienteLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            baseImponibleLabel = new System.Windows.Forms.Label();
            totalLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).BeginInit();
            this.Pie_Panel.Panel1.SuspendLayout();
            this.Pie_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).BeginInit();
            this.Content_Panel.Panel2.SuspendLayout();
            this.Content_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_SC)).BeginInit();
            this.Main_SC.Panel1.SuspendLayout();
            this.Main_SC.Panel2.SuspendLayout();
            this.Main_SC.SuspendLayout();
            this.Proveedor_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Acreedor)).BeginInit();
            this.Impresion_GB.SuspendLayout();
            this.Pedido_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_SC)).BeginInit();
            this.Conceptos_SC.Panel1.SuspendLayout();
            this.Conceptos_SC.Panel2.SuspendLayout();
            this.Conceptos_SC.SuspendLayout();
            this.Conceptos_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).BeginInit();
            this.SuspendLayout();
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
            this.PanelesV.Size = new System.Drawing.Size(1194, 622);
            this.PanelesV.SplitterDistance = 582;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(465, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(598, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Pie_Panel
            // 
            // 
            // Pie_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel1, true);
            // 
            // Pie_Panel.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Pie_Panel, true);
            this.Pie_Panel.Size = new System.Drawing.Size(1192, 37);
            // 
            // Content_Panel
            // 
            // 
            // Content_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel1, true);
            // 
            // Content_Panel.Panel2
            // 
            this.Content_Panel.Panel2.Controls.Add(this.Main_SC);
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Content_Panel, true);
            this.Content_Panel.Size = new System.Drawing.Size(1192, 580);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.PedidoProveedor);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(418, 102);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 622);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 359);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(560, 274);
            // 
            // numeroClienteLabel
            // 
            numeroClienteLabel.AutoSize = true;
            numeroClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numeroClienteLabel.Location = new System.Drawing.Point(19, 32);
            numeroClienteLabel.Name = "numeroClienteLabel";
            numeroClienteLabel.Size = new System.Drawing.Size(50, 13);
            numeroClienteLabel.TabIndex = 35;
            numeroClienteLabel.Text = "Código*:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            codigoLabel.Location = new System.Drawing.Point(13, 59);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(56, 13);
            codigoLabel.TabIndex = 18;
            codigoLabel.Text = "DNI / CIF:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(21, 85);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 24;
            nombreLabel.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(256, 30);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(22, 13);
            label2.TabIndex = 9;
            label2.Text = "ID:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(9, 30);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 8;
            fechaLabel.Text = "Fecha:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(17, 126);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 13);
            label1.TabIndex = 81;
            label1.Text = "Estado:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(9, 87);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(47, 13);
            label4.TabIndex = 84;
            label4.Text = "Usuario:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.Location = new System.Drawing.Point(67, 192);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(51, 13);
            label11.TabIndex = 97;
            label11.Text = "Subtotal:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(245, 192);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(76, 13);
            label10.TabIndex = 87;
            label10.Text = "% Descuento:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(405, 192);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(62, 13);
            label9.TabIndex = 95;
            label9.Text = "Descuento:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(796, 192);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(61, 13);
            label8.TabIndex = 93;
            label8.Text = "Impuestos:";
            // 
            // baseImponibleLabel
            // 
            baseImponibleLabel.AutoSize = true;
            baseImponibleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            baseImponibleLabel.Location = new System.Drawing.Point(585, 192);
            baseImponibleLabel.Name = "baseImponibleLabel";
            baseImponibleLabel.Size = new System.Drawing.Size(83, 13);
            baseImponibleLabel.TabIndex = 92;
            baseImponibleLabel.Text = "Base Imponible:";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalLabel.Location = new System.Drawing.Point(973, 192);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(39, 13);
            totalLabel.TabIndex = 91;
            totalLabel.Text = "Total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(9, 63);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(35, 13);
            label5.TabIndex = 92;
            label5.Text = "Serie:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(11, 113);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(61, 13);
            label6.TabIndex = 47;
            label6.Text = "Impuestos:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(9, 113);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 13);
            label3.TabIndex = 97;
            label3.Text = "Almacén:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(9, 140);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 13);
            label7.TabIndex = 100;
            label7.Text = "Expedient:";
            // 
            // Datos_Lineas
            // 
            this.Datos_Lineas.AllowNew = true;
            this.Datos_Lineas.DataSource = typeof(moleQule.Library.Store.LineaPedidoProveedor);
            // 
            // Main_SC
            // 
            this.Main_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Main_SC.IsSplitterFixed = true;
            this.Main_SC.Location = new System.Drawing.Point(0, 0);
            this.Main_SC.Name = "Main_SC";
            this.Main_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Main_SC.Panel1
            // 
            this.Main_SC.Panel1.Controls.Add(label11);
            this.Main_SC.Panel1.Controls.Add(this.PDescuento_NTB);
            this.Main_SC.Panel1.Controls.Add(label10);
            this.Main_SC.Panel1.Controls.Add(this.numericTextBox2);
            this.Main_SC.Panel1.Controls.Add(label9);
            this.Main_SC.Panel1.Controls.Add(this.Descuento_TB);
            this.Main_SC.Panel1.Controls.Add(label8);
            this.Main_SC.Panel1.Controls.Add(this.Impuestos_NTB);
            this.Main_SC.Panel1.Controls.Add(baseImponibleLabel);
            this.Main_SC.Panel1.Controls.Add(this.Base_NTB);
            this.Main_SC.Panel1.Controls.Add(totalLabel);
            this.Main_SC.Panel1.Controls.Add(this.Total_NTB);
            this.Main_SC.Panel1.Controls.Add(this.Proveedor_GB);
            this.Main_SC.Panel1.Controls.Add(this.Impresion_GB);
            this.Main_SC.Panel1.Controls.Add(this.Pedido_GB);
            this.Main_SC.Panel1MinSize = 220;
            // 
            // Main_SC.Panel2
            // 
            this.Main_SC.Panel2.Controls.Add(this.Conceptos_SC);
            this.Main_SC.Size = new System.Drawing.Size(1192, 539);
            this.Main_SC.SplitterDistance = 220;
            this.Main_SC.SplitterWidth = 1;
            this.Main_SC.TabIndex = 0;
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.Navy;
            this.PDescuento_NTB.Location = new System.Drawing.Point(331, 187);
            this.PDescuento_NTB.Name = "PDescuento_NTB";
            this.PDescuento_NTB.Size = new System.Drawing.Size(54, 23);
            this.PDescuento_NTB.TabIndex = 86;
            this.PDescuento_NTB.TabStop = false;
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PDescuento_NTB.TextIsCurrency = false;
            this.PDescuento_NTB.TextIsInteger = false;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Subtotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.numericTextBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox2.Location = new System.Drawing.Point(124, 187);
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.ReadOnly = true;
            this.numericTextBox2.Size = new System.Drawing.Size(105, 23);
            this.numericTextBox2.TabIndex = 96;
            this.numericTextBox2.TabStop = false;
            this.numericTextBox2.Tag = "NO FORMAT";
            this.numericTextBox2.Text = "0.00";
            this.numericTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox2.TextIsCurrency = false;
            this.numericTextBox2.TextIsInteger = false;
            // 
            // Descuento_TB
            // 
            this.Descuento_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Descuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Descuento_TB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descuento_TB.Location = new System.Drawing.Point(470, 187);
            this.Descuento_TB.Name = "Descuento_TB";
            this.Descuento_TB.ReadOnly = true;
            this.Descuento_TB.Size = new System.Drawing.Size(96, 23);
            this.Descuento_TB.TabIndex = 94;
            this.Descuento_TB.TabStop = false;
            this.Descuento_TB.Tag = "NO FORMAT";
            this.Descuento_TB.Text = "0.00";
            this.Descuento_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Descuento_TB.TextIsCurrency = false;
            this.Descuento_TB.TextIsInteger = false;
            // 
            // Impuestos_NTB
            // 
            this.Impuestos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuestos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Impuestos_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Impuestos_NTB.Location = new System.Drawing.Point(863, 187);
            this.Impuestos_NTB.Name = "Impuestos_NTB";
            this.Impuestos_NTB.ReadOnly = true;
            this.Impuestos_NTB.Size = new System.Drawing.Size(96, 23);
            this.Impuestos_NTB.TabIndex = 89;
            this.Impuestos_NTB.TabStop = false;
            this.Impuestos_NTB.Tag = "NO FORMAT";
            this.Impuestos_NTB.Text = "0.00";
            this.Impuestos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Impuestos_NTB.TextIsCurrency = false;
            this.Impuestos_NTB.TextIsInteger = false;
            // 
            // Base_NTB
            // 
            this.Base_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BaseImponible", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Base_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Base_NTB.Location = new System.Drawing.Point(674, 187);
            this.Base_NTB.Name = "Base_NTB";
            this.Base_NTB.ReadOnly = true;
            this.Base_NTB.Size = new System.Drawing.Size(105, 23);
            this.Base_NTB.TabIndex = 88;
            this.Base_NTB.TabStop = false;
            this.Base_NTB.Tag = "NO FORMAT";
            this.Base_NTB.Text = "0.00";
            this.Base_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Base_NTB.TextIsCurrency = false;
            this.Base_NTB.TextIsInteger = false;
            // 
            // Total_NTB
            // 
            this.Total_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Total_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_NTB.Location = new System.Drawing.Point(1018, 187);
            this.Total_NTB.Name = "Total_NTB";
            this.Total_NTB.ReadOnly = true;
            this.Total_NTB.Size = new System.Drawing.Size(108, 23);
            this.Total_NTB.TabIndex = 90;
            this.Total_NTB.TabStop = false;
            this.Total_NTB.Tag = "NO FORMAT";
            this.Total_NTB.Text = "0.00";
            this.Total_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total_NTB.TextIsCurrency = false;
            this.Total_NTB.TextIsInteger = false;
            // 
            // Proveedor_GB
            // 
            this.Proveedor_GB.Controls.Add(this.Impuesto_TB);
            this.Proveedor_GB.Controls.Add(label6);
            this.Proveedor_GB.Controls.Add(this.Emisor_BT);
            this.Proveedor_GB.Controls.Add(numeroClienteLabel);
            this.Proveedor_GB.Controls.Add(this.Acreedor_TB);
            this.Proveedor_GB.Controls.Add(this.IDAcreedor_TB);
            this.Proveedor_GB.Controls.Add(this.Codigo_TB);
            this.Proveedor_GB.Controls.Add(codigoLabel);
            this.Proveedor_GB.Controls.Add(nombreLabel);
            this.Proveedor_GB.Location = new System.Drawing.Point(442, 7);
            this.Proveedor_GB.Name = "Proveedor_GB";
            this.Proveedor_GB.Size = new System.Drawing.Size(318, 170);
            this.Proveedor_GB.TabIndex = 73;
            this.Proveedor_GB.TabStop = false;
            this.Proveedor_GB.Text = "Datos del Proveedor";
            // 
            // Impuesto_TB
            // 
            this.Impuesto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Acreedor, "Impuesto", true));
            this.Impuesto_TB.Location = new System.Drawing.Point(75, 109);
            this.Impuesto_TB.Name = "Impuesto_TB";
            this.Impuesto_TB.ReadOnly = true;
            this.Impuesto_TB.Size = new System.Drawing.Size(128, 21);
            this.Impuesto_TB.TabIndex = 48;
            this.Impuesto_TB.TabStop = false;
            // 
            // Datos_Acreedor
            // 
            this.Datos_Acreedor.DataSource = typeof(moleQule.Library.Store.ProviderBaseInfo);
            // 
            // Emisor_BT
            // 
            this.Emisor_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Emisor_BT.Location = new System.Drawing.Point(170, 28);
            this.Emisor_BT.Name = "Emisor_BT";
            this.Emisor_BT.Size = new System.Drawing.Size(28, 21);
            this.Emisor_BT.TabIndex = 0;
            this.Emisor_BT.UseVisualStyleBackColor = true;
            // 
            // Acreedor_TB
            // 
            this.Acreedor_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Acreedor, "Nombre", true));
            this.Acreedor_TB.Location = new System.Drawing.Point(75, 82);
            this.Acreedor_TB.Name = "Acreedor_TB";
            this.Acreedor_TB.ReadOnly = true;
            this.Acreedor_TB.Size = new System.Drawing.Size(233, 21);
            this.Acreedor_TB.TabIndex = 1;
            this.Acreedor_TB.TabStop = false;
            // 
            // IDAcreedor_TB
            // 
            this.IDAcreedor_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Acreedor, "Codigo", true));
            this.IDAcreedor_TB.Location = new System.Drawing.Point(75, 28);
            this.IDAcreedor_TB.Name = "IDAcreedor_TB";
            this.IDAcreedor_TB.ReadOnly = true;
            this.IDAcreedor_TB.Size = new System.Drawing.Size(89, 21);
            this.IDAcreedor_TB.TabIndex = 3;
            this.IDAcreedor_TB.TabStop = false;
            this.IDAcreedor_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Acreedor, "ID", true));
            this.Codigo_TB.Location = new System.Drawing.Point(75, 55);
            this.Codigo_TB.Name = "Codigo_TB";
            this.Codigo_TB.ReadOnly = true;
            this.Codigo_TB.Size = new System.Drawing.Size(89, 21);
            this.Codigo_TB.TabIndex = 0;
            this.Codigo_TB.TabStop = false;
            this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Impresion_GB
            // 
            this.Impresion_GB.Controls.Add(this.label14);
            this.Impresion_GB.Controls.Add(this.Observaciones_TB);
            this.Impresion_GB.Controls.Add(this.Estado_TB);
            this.Impresion_GB.Controls.Add(label1);
            this.Impresion_GB.Controls.Add(this.Estado_BT);
            this.Impresion_GB.Location = new System.Drawing.Point(765, 7);
            this.Impresion_GB.Name = "Impresion_GB";
            this.Impresion_GB.Size = new System.Drawing.Size(420, 170);
            this.Impresion_GB.TabIndex = 72;
            this.Impresion_GB.TabStop = false;
            this.Impresion_GB.Text = "Otros Datos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(11, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 52;
            this.label14.Text = "Observaciones:";
            // 
            // Observaciones_TB
            // 
            this.Observaciones_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_TB.Location = new System.Drawing.Point(11, 34);
            this.Observaciones_TB.Multiline = true;
            this.Observaciones_TB.Name = "Observaciones_TB";
            this.Observaciones_TB.Size = new System.Drawing.Size(399, 83);
            this.Observaciones_TB.TabIndex = 3;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(67, 123);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(186, 21);
            this.Estado_TB.TabIndex = 80;
            this.Estado_TB.TabStop = false;
            this.Estado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(259, 123);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(28, 21);
            this.Estado_BT.TabIndex = 79;
            this.Estado_BT.UseVisualStyleBackColor = true;
            this.Estado_BT.Visible = false;
            // 
            // Pedido_GB
            // 
            this.Pedido_GB.Controls.Add(this.Expediente_BT);
            this.Pedido_GB.Controls.Add(label7);
            this.Pedido_GB.Controls.Add(this.Expediente_TB);
            this.Pedido_GB.Controls.Add(this.Almacen_BT);
            this.Pedido_GB.Controls.Add(label3);
            this.Pedido_GB.Controls.Add(this.Almacen_TB);
            this.Pedido_GB.Controls.Add(this.Serie_BT);
            this.Pedido_GB.Controls.Add(this.Serie_TB);
            this.Pedido_GB.Controls.Add(label5);
            this.Pedido_GB.Controls.Add(this.Usuario_BT);
            this.Pedido_GB.Controls.Add(label4);
            this.Pedido_GB.Controls.Add(this.Usuario_TB);
            this.Pedido_GB.Controls.Add(this.Fecha_DTP);
            this.Pedido_GB.Controls.Add(this.IDManual_CkB);
            this.Pedido_GB.Controls.Add(label2);
            this.Pedido_GB.Controls.Add(fechaLabel);
            this.Pedido_GB.Controls.Add(this.IDPedido_TB);
            this.Pedido_GB.Location = new System.Drawing.Point(7, 7);
            this.Pedido_GB.Name = "Pedido_GB";
            this.Pedido_GB.Size = new System.Drawing.Size(430, 170);
            this.Pedido_GB.TabIndex = 71;
            this.Pedido_GB.TabStop = false;
            this.Pedido_GB.Text = "Datos del Pedido";
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(316, 138);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(28, 21);
            this.Expediente_BT.TabIndex = 98;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Expedient", true));
            this.Expediente_TB.Location = new System.Drawing.Point(80, 137);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(230, 21);
            this.Expediente_TB.TabIndex = 99;
            this.Expediente_TB.TabStop = false;
            this.Expediente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Almacen_BT
            // 
            this.Almacen_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Almacen_BT.Location = new System.Drawing.Point(316, 114);
            this.Almacen_BT.Name = "Almacen_BT";
            this.Almacen_BT.Size = new System.Drawing.Size(28, 21);
            this.Almacen_BT.TabIndex = 95;
            this.Almacen_BT.UseVisualStyleBackColor = true;
            // 
            // Almacen_TB
            // 
            this.Almacen_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "IDAlmacenAlmacen", true));
            this.Almacen_TB.Location = new System.Drawing.Point(80, 113);
            this.Almacen_TB.Name = "Almacen_TB";
            this.Almacen_TB.ReadOnly = true;
            this.Almacen_TB.Size = new System.Drawing.Size(230, 21);
            this.Almacen_TB.TabIndex = 96;
            this.Almacen_TB.TabStop = false;
            this.Almacen_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Serie_BT
            // 
            this.Serie_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Serie_BT.Location = new System.Drawing.Point(316, 59);
            this.Serie_BT.Name = "Serie_BT";
            this.Serie_BT.Size = new System.Drawing.Size(28, 21);
            this.Serie_BT.TabIndex = 94;
            this.Serie_BT.UseVisualStyleBackColor = true;
            // 
            // Serie_TB
            // 
            this.Serie_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "NSerieSerie", true));
            this.Serie_TB.Enabled = false;
            this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Serie_TB.Location = new System.Drawing.Point(80, 60);
            this.Serie_TB.Name = "Serie_TB";
            this.Serie_TB.ReadOnly = true;
            this.Serie_TB.Size = new System.Drawing.Size(230, 21);
            this.Serie_TB.TabIndex = 93;
            this.Serie_TB.TabStop = false;
            this.Serie_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Usuario_BT
            // 
            this.Usuario_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Usuario_BT.Location = new System.Drawing.Point(316, 87);
            this.Usuario_BT.Name = "Usuario_BT";
            this.Usuario_BT.Size = new System.Drawing.Size(28, 21);
            this.Usuario_BT.TabIndex = 82;
            this.Usuario_BT.UseVisualStyleBackColor = true;
            // 
            // Usuario_TB
            // 
            this.Usuario_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Usuario", true));
            this.Usuario_TB.Location = new System.Drawing.Point(80, 87);
            this.Usuario_TB.Name = "Usuario_TB";
            this.Usuario_TB.ReadOnly = true;
            this.Usuario_TB.Size = new System.Drawing.Size(230, 21);
            this.Usuario_TB.TabIndex = 83;
            this.Usuario_TB.TabStop = false;
            this.Usuario_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.CalendarTitleForeColor = System.Drawing.Color.Navy;
            this.Fecha_DTP.Checked = false;
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(61, 27);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(142, 21);
            this.Fecha_DTP.TabIndex = 78;
            // 
            // IDManual_CkB
            // 
            this.IDManual_CkB.AutoSize = true;
            this.IDManual_CkB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "IDManual", true));
            this.IDManual_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDManual_CkB.Location = new System.Drawing.Point(359, 28);
            this.IDManual_CkB.Name = "IDManual_CkB";
            this.IDManual_CkB.Size = new System.Drawing.Size(60, 17);
            this.IDManual_CkB.TabIndex = 46;
            this.IDManual_CkB.Text = "Manual";
            this.IDManual_CkB.UseVisualStyleBackColor = true;
            // 
            // IDPedido_TB
            // 
            this.IDPedido_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.IDPedido_TB.Location = new System.Drawing.Point(285, 26);
            this.IDPedido_TB.Name = "IDPedido_TB";
            this.IDPedido_TB.ReadOnly = true;
            this.IDPedido_TB.Size = new System.Drawing.Size(68, 21);
            this.IDPedido_TB.TabIndex = 0;
            this.IDPedido_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Conceptos_SC
            // 
            this.Conceptos_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Conceptos_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Conceptos_SC.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_SC.Name = "Conceptos_SC";
            this.Conceptos_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Conceptos_SC.Panel1
            // 
            this.Conceptos_SC.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Conceptos_SC.Panel1.Controls.Add(this.Conceptos_TS);
            this.Conceptos_SC.Panel1MinSize = 20;
            // 
            // Conceptos_SC.Panel2
            // 
            this.Conceptos_SC.Panel2.Controls.Add(this.Lineas_DGW);
            this.Conceptos_SC.Panel2MinSize = 40;
            this.Conceptos_SC.Size = new System.Drawing.Size(1192, 318);
            this.Conceptos_SC.SplitterDistance = 36;
            this.Conceptos_SC.SplitterWidth = 1;
            this.Conceptos_SC.TabIndex = 4;
            // 
            // Conceptos_TS
            // 
            this.Conceptos_TS.AutoSize = false;
            this.Conceptos_TS.BackColor = System.Drawing.Color.Gainsboro;
            this.Conceptos_TS.GripMargin = new System.Windows.Forms.Padding(0);
            this.Conceptos_TS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Conceptos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Conceptos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddConcepto_TI,
            this.Edit_TI,
            this.View_TI,
            this.Delete_TI});
            this.Conceptos_TS.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_TS.Name = "Conceptos_TS";
            this.Conceptos_TS.Padding = new System.Windows.Forms.Padding(0);
            this.Conceptos_TS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.HelpProvider.SetShowHelp(this.Conceptos_TS, true);
            this.Conceptos_TS.Size = new System.Drawing.Size(1192, 40);
            this.Conceptos_TS.TabIndex = 6;
            // 
            // AddConcepto_TI
            // 
            this.AddConcepto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConcepto_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_add;
            this.AddConcepto_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConcepto_TI.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.AddConcepto_TI.Name = "AddConcepto_TI";
            this.AddConcepto_TI.Size = new System.Drawing.Size(36, 37);
            this.AddConcepto_TI.Text = "Nuevo Concepto sin control de Stock";
            this.AddConcepto_TI.Click += new System.EventHandler(this.AddConcepto_TI_Click);
            // 
            // Edit_TI
            // 
            this.Edit_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Edit_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.Edit_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_TI.Name = "Edit_TI";
            this.Edit_TI.Size = new System.Drawing.Size(36, 37);
            this.Edit_TI.Text = "Editar Concepto";
            this.Edit_TI.Click += new System.EventHandler(this.Edit_TI_Click);
            // 
            // View_TI
            // 
            this.View_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.View_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            this.View_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_TI.Name = "View_TI";
            this.View_TI.Size = new System.Drawing.Size(36, 37);
            this.View_TI.Text = "Ver";
            this.View_TI.Visible = false;
            // 
            // Delete_TI
            // 
            this.Delete_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delete_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_delete;
            this.Delete_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delete_TI.Name = "Delete_TI";
            this.Delete_TI.Size = new System.Drawing.Size(36, 37);
            this.Delete_TI.Text = "Borrar Concepto";
            this.Delete_TI.Click += new System.EventHandler(this.Delete_TI_Click);
            // 
            // Lineas_DGW
            // 
            this.Lineas_DGW.AllowUserToAddRows = false;
            this.Lineas_DGW.AllowUserToDeleteRows = false;
            this.Lineas_DGW.AllowUserToOrderColumns = true;
            this.Lineas_DGW.AutoGenerateColumns = false;
            this.Lineas_DGW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Lineas_DGW.ColumnHeadersHeight = 34;
            this.Lineas_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Lineas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoProductoAcreedor,
            this.Concepto,
            this.Almacen,
            this.Expedient,
            this.FacturacionPeso,
            this.LiPieces,
            this.LiKilos,
            this.pendienteDataGridViewTextBoxColumn,
            this.pendienteBultosDataGridViewTextBoxColumn,
            this.Precio,
            this.Subtotal,
            this.pDescuentoDataGridViewTextBoxColumn,
            this.descuentoDataGridViewTextBoxColumn,
            this.PImpuestos,
            this.Impuestos,
            this.totalDataGridViewTextBoxColumn});
            this.Lineas_DGW.DataSource = this.Datos_Lineas;
            this.Lineas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lineas_DGW.Location = new System.Drawing.Point(0, 0);
            this.Lineas_DGW.MultiSelect = false;
            this.Lineas_DGW.Name = "Lineas_DGW";
            this.Lineas_DGW.RowHeadersWidth = 25;
            this.Lineas_DGW.Size = new System.Drawing.Size(1192, 281);
            this.Lineas_DGW.TabIndex = 3;
            this.Lineas_DGW.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_CellContentClick);
            this.Lineas_DGW.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_CellValidated);
            this.Lineas_DGW.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Lineas_DGW_RowPrePaint);
            // 
            // CodigoProductoAcreedor
            // 
            this.CodigoProductoAcreedor.DataPropertyName = "CodigoProductoAcreedor";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CodigoProductoAcreedor.DefaultCellStyle = dataGridViewCellStyle1;
            this.CodigoProductoAcreedor.HeaderText = "Código Artículo";
            this.CodigoProductoAcreedor.Name = "CodigoProductoAcreedor";
            this.CodigoProductoAcreedor.Width = 60;
            // 
            // Concepto
            // 
            this.Concepto.DataPropertyName = "Concepto";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Concepto.DefaultCellStyle = dataGridViewCellStyle2;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.MinimumWidth = 220;
            this.Concepto.Name = "Concepto";
            this.Concepto.Width = 220;
            // 
            // Almacen
            // 
            this.Almacen.DataPropertyName = "Almacen";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Almacen.DefaultCellStyle = dataGridViewCellStyle3;
            this.Almacen.HeaderText = "Alm.";
            this.Almacen.Name = "Almacen";
            this.Almacen.ReadOnly = true;
            this.Almacen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Almacen.Width = 35;
            // 
            // Expedient
            // 
            this.Expedient.DataPropertyName = "Expedient";
            this.Expedient.HeaderText = "Expediente";
            this.Expedient.Name = "Expedient";
            this.Expedient.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Expedient.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Expedient.Width = 105;
            // 
            // FacturacionPeso
            // 
            this.FacturacionPeso.DataPropertyName = "FacturacionPeso";
            this.FacturacionPeso.HeaderText = "Fac. Peso";
            this.FacturacionPeso.Name = "FacturacionPeso";
            this.FacturacionPeso.Width = 40;
            // 
            // LiPieces
            // 
            this.LiPieces.DataPropertyName = "CantidadBultos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.LiPieces.DefaultCellStyle = dataGridViewCellStyle4;
            this.LiPieces.HeaderText = "Unidades";
            this.LiPieces.Name = "LiPieces";
            this.LiPieces.Width = 55;
            // 
            // LiKilos
            // 
            this.LiKilos.DataPropertyName = "CantidadKilos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.LiKilos.DefaultCellStyle = dataGridViewCellStyle5;
            this.LiKilos.HeaderText = "Kg";
            this.LiKilos.Name = "LiKilos";
            this.LiKilos.Width = 65;
            // 
            // pendienteDataGridViewTextBoxColumn
            // 
            this.pendienteDataGridViewTextBoxColumn.DataPropertyName = "Pendiente";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.pendienteDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.pendienteDataGridViewTextBoxColumn.HeaderText = "Pendiente (Uds.)";
            this.pendienteDataGridViewTextBoxColumn.Name = "pendienteDataGridViewTextBoxColumn";
            this.pendienteDataGridViewTextBoxColumn.ReadOnly = true;
            this.pendienteDataGridViewTextBoxColumn.Width = 55;
            // 
            // pendienteBultosDataGridViewTextBoxColumn
            // 
            this.pendienteBultosDataGridViewTextBoxColumn.DataPropertyName = "PendienteBultos";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.pendienteBultosDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.pendienteBultosDataGridViewTextBoxColumn.HeaderText = "Pendiente (Kg)";
            this.pendienteBultosDataGridViewTextBoxColumn.Name = "pendienteBultosDataGridViewTextBoxColumn";
            this.pendienteBultosDataGridViewTextBoxColumn.ReadOnly = true;
            this.pendienteBultosDataGridViewTextBoxColumn.Width = 65;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle8;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 70;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.Subtotal.DefaultCellStyle = dataGridViewCellStyle9;
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Width = 80;
            // 
            // pDescuentoDataGridViewTextBoxColumn
            // 
            this.pDescuentoDataGridViewTextBoxColumn.DataPropertyName = "PDescuento";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.pDescuentoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.pDescuentoDataGridViewTextBoxColumn.HeaderText = "% Dto.";
            this.pDescuentoDataGridViewTextBoxColumn.Name = "pDescuentoDataGridViewTextBoxColumn";
            this.pDescuentoDataGridViewTextBoxColumn.Width = 40;
            // 
            // descuentoDataGridViewTextBoxColumn
            // 
            this.descuentoDataGridViewTextBoxColumn.DataPropertyName = "Descuento";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.descuentoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.descuentoDataGridViewTextBoxColumn.HeaderText = "Dto.";
            this.descuentoDataGridViewTextBoxColumn.Name = "descuentoDataGridViewTextBoxColumn";
            this.descuentoDataGridViewTextBoxColumn.ReadOnly = true;
            this.descuentoDataGridViewTextBoxColumn.Width = 60;
            // 
            // PImpuestos
            // 
            this.PImpuestos.DataPropertyName = "PImpuestos";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            this.PImpuestos.DefaultCellStyle = dataGridViewCellStyle12;
            this.PImpuestos.HeaderText = "% Imp.";
            this.PImpuestos.Name = "PImpuestos";
            this.PImpuestos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PImpuestos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PImpuestos.Width = 40;
            // 
            // Impuestos
            // 
            this.Impuestos.DataPropertyName = "Impuestos";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = null;
            this.Impuestos.DefaultCellStyle = dataGridViewCellStyle13;
            this.Impuestos.HeaderText = "Imp.";
            this.Impuestos.Name = "Impuestos";
            this.Impuestos.ReadOnly = true;
            this.Impuestos.Width = 60;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.ReadOnly = true;
            this.totalDataGridViewTextBoxColumn.Width = 80;
            // 
            // PedidoProveedorForm
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1194, 622);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PedidoProveedorForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PedidoProveedorForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Pie_Panel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).EndInit();
            this.Pie_Panel.ResumeLayout(false);
            this.Content_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).EndInit();
            this.Content_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
            this.Main_SC.Panel1.ResumeLayout(false);
            this.Main_SC.Panel1.PerformLayout();
            this.Main_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Main_SC)).EndInit();
            this.Main_SC.ResumeLayout(false);
            this.Proveedor_GB.ResumeLayout(false);
            this.Proveedor_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Acreedor)).EndInit();
            this.Impresion_GB.ResumeLayout(false);
            this.Impresion_GB.PerformLayout();
            this.Pedido_GB.ResumeLayout(false);
            this.Pedido_GB.PerformLayout();
            this.Conceptos_SC.Panel1.ResumeLayout(false);
            this.Conceptos_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_SC)).EndInit();
            this.Conceptos_SC.ResumeLayout(false);
            this.Conceptos_TS.ResumeLayout(false);
            this.Conceptos_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource Datos_Lineas;
		private System.Windows.Forms.SplitContainer Main_SC;
		protected System.Windows.Forms.DataGridView Lineas_DGW;
		protected System.Windows.Forms.BindingSource Datos_Acreedor;
		protected System.Windows.Forms.GroupBox Proveedor_GB;
		protected System.Windows.Forms.Button Emisor_BT;
		protected System.Windows.Forms.TextBox Acreedor_TB;
		protected System.Windows.Forms.TextBox IDAcreedor_TB;
		protected System.Windows.Forms.TextBox Codigo_TB;
		protected System.Windows.Forms.GroupBox Impresion_GB;
		protected System.Windows.Forms.Label label14;
		protected System.Windows.Forms.TextBox Observaciones_TB;
		protected System.Windows.Forms.GroupBox Pedido_GB;
		protected System.Windows.Forms.DateTimePicker Fecha_DTP;
		protected System.Windows.Forms.CheckBox IDManual_CkB;
		protected System.Windows.Forms.TextBox IDPedido_TB;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
		private System.Windows.Forms.SplitContainer Conceptos_SC;
		protected System.Windows.Forms.ToolStrip Conceptos_TS;
		protected System.Windows.Forms.ToolStripButton AddConcepto_TI;
		protected System.Windows.Forms.ToolStripButton Edit_TI;
		protected System.Windows.Forms.ToolStripButton View_TI;
		protected System.Windows.Forms.ToolStripButton Delete_TI;
		protected System.Windows.Forms.Button Usuario_BT;
		protected System.Windows.Forms.TextBox Usuario_TB;
		protected Controls.NumericTextBox PDescuento_NTB;
		protected Controls.NumericTextBox numericTextBox2;
		protected Controls.NumericTextBox Descuento_TB;
		protected Controls.NumericTextBox Impuestos_NTB;
		protected Controls.NumericTextBox Base_NTB;
		protected Controls.NumericTextBox Total_NTB;
		protected System.Windows.Forms.Button Serie_BT;
		protected System.Windows.Forms.TextBox Serie_TB;
		protected System.Windows.Forms.TextBox Impuesto_TB;
		protected System.Windows.Forms.Button Almacen_BT;
		protected System.Windows.Forms.TextBox Almacen_TB;
		protected System.Windows.Forms.Button Expediente_BT;
        protected System.Windows.Forms.TextBox Expediente_TB;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoProductoAcreedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Almacen;
        private System.Windows.Forms.DataGridViewButtonColumn Expedient;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FacturacionPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiPieces;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn pendienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pendienteBultosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDescuentoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descuentoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn PImpuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Impuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
		

    }
}
