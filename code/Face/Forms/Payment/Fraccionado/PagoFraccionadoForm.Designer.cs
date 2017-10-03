namespace moleQule.Face.Store
{
    partial class PagoFraccionadoForm
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
            System.Windows.Forms.Label importeLabel;
            System.Windows.Forms.Label vencimientoLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label tipoLabel;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label cuentaLabel;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagoFraccionadoForm));
            this.Expenses_BS = new System.Windows.Forms.BindingSource(this.components);
            this.Expenses_GB = new System.Windows.Forms.GroupBox();
            this.Expenses_Panel = new System.Windows.Forms.SplitContainer();
            this.Facturas_TS = new System.Windows.Forms.ToolStrip();
            this.AddFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.EditFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Lineas_DGW = new System.Windows.Forms.DataGridView();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new CalendarColumn();
            this.previsionPagoDataGridViewTextBoxColumn = new CalendarColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPagado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acumulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PendienteAsignar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vinculado = new System.Windows.Forms.DataGridViewButtonColumn();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.General_GB = new System.Windows.Forms.GroupBox();
            this.Source_Panel = new System.Windows.Forms.Panel();
            this.Periodicidad_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.NPagos_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.MedioPago_TB = new System.Windows.Forms.TextBox();
            this.Cuenta_TB = new System.Windows.Forms.TextBox();
            this.MedioPago_BT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GastosBancarios_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.NoAsignado_TB = new System.Windows.Forms.TextBox();
            this.Tarjeta_BT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.Tarjeta_TB = new System.Windows.Forms.TextBox();
            this.Vencimiento_DTP = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Importe_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Liberar_BT = new System.Windows.Forms.Button();
            this.Cuenta_BT = new System.Windows.Forms.Button();
            this.Repartir_BT = new System.Windows.Forms.Button();
            this.Main_TLP = new System.Windows.Forms.TableLayoutPanel();
            this.Pagos_GB = new System.Windows.Forms.GroupBox();
            this.Payments_SC = new System.Windows.Forms.SplitContainer();
            this.Payments_TS = new System.Windows.Forms.ToolStrip();
            this.EditPago_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewPago_TI = new System.Windows.Forms.ToolStripButton();
            this.Generar_BT = new System.Windows.Forms.ToolStripButton();
            this.Payments_DGW = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarColumn1 = new CalendarColumn();
            this.MedioPagoLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDMovimientoBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDLineaCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaBancaria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pagado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IDMovimientoContable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PendienteAsignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Payments_BS = new System.Windows.Forms.BindingSource(this.components);
            importeLabel = new System.Windows.Forms.Label();
            vencimientoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            tipoLabel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            cuentaLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Expenses_BS)).BeginInit();
            this.Expenses_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Expenses_Panel)).BeginInit();
            this.Expenses_Panel.Panel1.SuspendLayout();
            this.Expenses_Panel.Panel2.SuspendLayout();
            this.Expenses_Panel.SuspendLayout();
            this.Facturas_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).BeginInit();
            this.General_GB.SuspendLayout();
            this.Source_Panel.SuspendLayout();
            this.Main_TLP.SuspendLayout();
            this.Pagos_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_SC)).BeginInit();
            this.Payments_SC.Panel1.SuspendLayout();
            this.Payments_SC.Panel2.SuspendLayout();
            this.Payments_SC.SuspendLayout();
            this.Payments_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PanelesV.Panel1.Controls.Add(this.Main_TLP);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(1018, 657);
            this.PanelesV.SplitterDistance = 602;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(252, 6);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
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
            this.Paneles2.Size = new System.Drawing.Size(1016, 52);
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(190, 8);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Store.ExpenseInfo);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(330, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1018, 657);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(472, 377);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(472, 292);
            // 
            // importeLabel
            // 
            importeLabel.AutoSize = true;
            importeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            importeLabel.Location = new System.Drawing.Point(42, 57);
            importeLabel.Name = "importeLabel";
            importeLabel.Size = new System.Drawing.Size(49, 13);
            importeLabel.TabIndex = 87;
            importeLabel.Text = "Importe:";
            // 
            // vencimientoLabel
            // 
            vencimientoLabel.AutoSize = true;
            vencimientoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            vencimientoLabel.Location = new System.Drawing.Point(332, 125);
            vencimientoLabel.Name = "vencimientoLabel";
            vencimientoLabel.Size = new System.Drawing.Size(68, 13);
            vencimientoLabel.TabIndex = 86;
            vencimientoLabel.Text = "Vencimiento:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(360, 98);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 85;
            fechaLabel.Text = "Fecha:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(301, 70);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(99, 13);
            label5.TabIndex = 95;
            label5.Text = "Tarjeta de Crédito:";
            // 
            // tipoLabel
            // 
            tipoLabel.AutoSize = true;
            tipoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tipoLabel.Location = new System.Drawing.Point(328, 16);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new System.Drawing.Size(72, 13);
            tipoLabel.TabIndex = 84;
            tipoLabel.Text = "Medio Pago*:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(307, 147);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(93, 13);
            label6.TabIndex = 97;
            label6.Text = "Gastos Bancarios:";
            // 
            // observacionesLabel
            // 
            observacionesLabel.AutoSize = true;
            observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            observacionesLabel.Location = new System.Drawing.Point(662, 13);
            observacionesLabel.Name = "observacionesLabel";
            observacionesLabel.Size = new System.Drawing.Size(82, 13);
            observacionesLabel.TabIndex = 83;
            observacionesLabel.Text = "Observaciones:";
            // 
            // cuentaLabel
            // 
            cuentaLabel.AutoSize = true;
            cuentaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cuentaLabel.Location = new System.Drawing.Point(354, 44);
            cuentaLabel.Name = "cuentaLabel";
            cuentaLabel.Size = new System.Drawing.Size(46, 13);
            cuentaLabel.TabIndex = 82;
            cuentaLabel.Text = "Cuenta:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(8, 149);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(55, 13);
            label4.TabIndex = 103;
            label4.Text = "Nº Pagos:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(141, 147);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(68, 13);
            label7.TabIndex = 105;
            label7.Text = "Periodicidad:";
            // 
            // Expenses_BS
            // 
            this.Expenses_BS.DataSource = typeof(moleQule.Library.Store.Expense);
            // 
            // Expenses_GB
            // 
            this.Expenses_GB.Controls.Add(this.Expenses_Panel);
            this.Expenses_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Expenses_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expenses_GB.Location = new System.Drawing.Point(3, 210);
            this.Expenses_GB.Name = "Expenses_GB";
            this.Expenses_GB.Size = new System.Drawing.Size(1010, 190);
            this.Expenses_GB.TabIndex = 0;
            this.Expenses_GB.TabStop = false;
            this.Expenses_GB.Text = "Gastos Pendientes de Asignación";
            // 
            // Expenses_Panel
            // 
            this.Expenses_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Expenses_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Expenses_Panel.Location = new System.Drawing.Point(3, 17);
            this.Expenses_Panel.Name = "Expenses_Panel";
            this.Expenses_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Expenses_Panel.Panel1
            // 
            this.Expenses_Panel.Panel1.Controls.Add(this.Facturas_TS);
            this.Expenses_Panel.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // Expenses_Panel.Panel2
            // 
            this.Expenses_Panel.Panel2.Controls.Add(this.Lineas_DGW);
            this.Expenses_Panel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Expenses_Panel.Size = new System.Drawing.Size(1004, 170);
            this.Expenses_Panel.SplitterDistance = 38;
            this.Expenses_Panel.SplitterWidth = 1;
            this.Expenses_Panel.TabIndex = 8;
            // 
            // Facturas_TS
            // 
            this.Facturas_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Facturas_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFactura_TI,
            this.EditFactura_TI,
            this.ViewFactura_TI,
            this.DeleteFactura_TI,
            this.toolStripLabel2});
            this.Facturas_TS.Location = new System.Drawing.Point(0, 0);
            this.Facturas_TS.Name = "Facturas_TS";
            this.HelpProvider.SetShowHelp(this.Facturas_TS, true);
            this.Facturas_TS.Size = new System.Drawing.Size(1004, 25);
            this.Facturas_TS.TabIndex = 5;
            // 
            // AddFactura_TI
            // 
            this.AddFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddFactura_TI.Name = "AddFactura_TI";
            this.AddFactura_TI.Size = new System.Drawing.Size(23, 22);
            this.AddFactura_TI.Text = "Nuevo";
            this.AddFactura_TI.Visible = false;
            // 
            // EditFactura_TI
            // 
            this.EditFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditFactura_TI.Enabled = false;
            this.EditFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditFactura_TI.Name = "EditFactura_TI";
            this.EditFactura_TI.Size = new System.Drawing.Size(23, 22);
            this.EditFactura_TI.Text = "Editar";
            // 
            // ViewFactura_TI
            // 
            this.ViewFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewFactura_TI.Enabled = false;
            this.ViewFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewFactura_TI.Name = "ViewFactura_TI";
            this.ViewFactura_TI.Size = new System.Drawing.Size(23, 22);
            this.ViewFactura_TI.Text = "Ver";
            // 
            // DeleteFactura_TI
            // 
            this.DeleteFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteFactura_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteFactura_TI.Name = "DeleteFactura_TI";
            this.DeleteFactura_TI.Size = new System.Drawing.Size(23, 22);
            this.DeleteFactura_TI.Text = "Borrar";
            this.DeleteFactura_TI.Visible = false;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // Lineas_DGW
            // 
            this.Lineas_DGW.AllowUserToAddRows = false;
            this.Lineas_DGW.AllowUserToDeleteRows = false;
            this.Lineas_DGW.AutoGenerateColumns = false;
            this.Lineas_DGW.ColumnHeadersHeight = 36;
            this.Lineas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.DescripcionCol,
            this.fechaDataGridViewTextBoxColumn,
            this.previsionPagoDataGridViewTextBoxColumn,
            this.Total,
            this.TotalPagado,
            this.Pendiente,
            this.Acumulado,
            this.PendienteAsignar,
            this.Asignado,
            this.Vinculado});
            this.Lineas_DGW.DataSource = this.Expenses_BS;
            this.Lineas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lineas_DGW.Location = new System.Drawing.Point(0, 0);
            this.Lineas_DGW.MultiSelect = false;
            this.Lineas_DGW.Name = "Lineas_DGW";
            this.Lineas_DGW.ReadOnly = true;
            this.Lineas_DGW.RowHeadersWidth = 25;
            this.Lineas_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Lineas_DGW.Size = new System.Drawing.Size(1004, 131);
            this.Lineas_DGW.TabIndex = 3;
            this.Lineas_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_CellClick);
            this.Lineas_DGW.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_DoubleClick);
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle20;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "ID";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            this.codigoDataGridViewTextBoxColumn.Width = 45;
            // 
            // DescripcionCol
            // 
            this.DescripcionCol.DataPropertyName = "Descripcion";
            this.DescripcionCol.HeaderText = "Descripción";
            this.DescripcionCol.MinimumWidth = 185;
            this.DescripcionCol.Name = "DescripcionCol";
            this.DescripcionCol.ReadOnly = true;
            this.DescripcionCol.Width = 275;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.Format = "d";
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle21;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            this.fechaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fechaDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.fechaDataGridViewTextBoxColumn.Width = 70;
            // 
            // previsionPagoDataGridViewTextBoxColumn
            // 
            this.previsionPagoDataGridViewTextBoxColumn.DataPropertyName = "PrevisionPago";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Format = "d";
            this.previsionPagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle22;
            this.previsionPagoDataGridViewTextBoxColumn.HeaderText = "Fecha Previsión";
            this.previsionPagoDataGridViewTextBoxColumn.Name = "previsionPagoDataGridViewTextBoxColumn";
            this.previsionPagoDataGridViewTextBoxColumn.ReadOnly = true;
            this.previsionPagoDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.previsionPagoDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.previsionPagoDataGridViewTextBoxColumn.Width = 65;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "N2";
            this.Total.DefaultCellStyle = dataGridViewCellStyle23;
            this.Total.HeaderText = "Importe";
            this.Total.MinimumWidth = 75;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 75;
            // 
            // TotalPagado
            // 
            this.TotalPagado.DataPropertyName = "TotalPagado";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Format = "N2";
            dataGridViewCellStyle24.NullValue = null;
            this.TotalPagado.DefaultCellStyle = dataGridViewCellStyle24;
            this.TotalPagado.HeaderText = "Importe Pagado";
            this.TotalPagado.Name = "TotalPagado";
            this.TotalPagado.ReadOnly = true;
            this.TotalPagado.Width = 70;
            // 
            // Pendiente
            // 
            this.Pendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle25.Format = "N2";
            this.Pendiente.DefaultCellStyle = dataGridViewCellStyle25;
            this.Pendiente.HeaderText = "Importe Pendiente";
            this.Pendiente.Name = "Pendiente";
            this.Pendiente.ReadOnly = true;
            this.Pendiente.Width = 70;
            // 
            // Acumulado
            // 
            this.Acumulado.DataPropertyName = "Acumulado";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Format = "N2";
            dataGridViewCellStyle26.NullValue = null;
            this.Acumulado.DefaultCellStyle = dataGridViewCellStyle26;
            this.Acumulado.HeaderText = "Importe Acumulado";
            this.Acumulado.Name = "Acumulado";
            this.Acumulado.ReadOnly = true;
            this.Acumulado.Width = 75;
            // 
            // PendienteAsignar
            // 
            this.PendienteAsignar.DataPropertyName = "PendienteAsignar";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle27.Format = "N2";
            this.PendienteAsignar.DefaultCellStyle = dataGridViewCellStyle27;
            this.PendienteAsignar.HeaderText = "Pendiente Asignar";
            this.PendienteAsignar.Name = "PendienteAsignar";
            this.PendienteAsignar.ReadOnly = true;
            this.PendienteAsignar.Width = 70;
            // 
            // Asignado
            // 
            this.Asignado.DataPropertyName = "Asignado";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle28.Format = "N2";
            dataGridViewCellStyle28.NullValue = null;
            this.Asignado.DefaultCellStyle = dataGridViewCellStyle28;
            this.Asignado.HeaderText = "Importe Asignado";
            this.Asignado.Name = "Asignado";
            this.Asignado.ReadOnly = true;
            this.Asignado.Width = 70;
            // 
            // Vinculado
            // 
            this.Vinculado.DataPropertyName = "Vinculado";
            this.Vinculado.HeaderText = "Vinculado";
            this.Vinculado.Name = "Vinculado";
            this.Vinculado.ReadOnly = true;
            this.Vinculado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Vinculado.Width = 70;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.miniToolStrip.Location = new System.Drawing.Point(153, 10);
            this.miniToolStrip.Name = "miniToolStrip";
            this.HelpProvider.SetShowHelp(this.miniToolStrip, true);
            this.miniToolStrip.Size = new System.Drawing.Size(1010, 39);
            this.miniToolStrip.TabIndex = 5;
            // 
            // General_GB
            // 
            this.General_GB.Controls.Add(this.Source_Panel);
            this.General_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.General_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.General_GB.Location = new System.Drawing.Point(3, 3);
            this.General_GB.Name = "General_GB";
            this.General_GB.Size = new System.Drawing.Size(1010, 201);
            this.General_GB.TabIndex = 0;
            this.General_GB.TabStop = false;
            this.General_GB.Text = "Datos del Pago";
            // 
            // Source_Panel
            // 
            this.Source_Panel.Controls.Add(label7);
            this.Source_Panel.Controls.Add(this.Periodicidad_NTB);
            this.Source_Panel.Controls.Add(label4);
            this.Source_Panel.Controls.Add(this.NPagos_NTB);
            this.Source_Panel.Controls.Add(this.Codigo_TB);
            this.Source_Panel.Controls.Add(this.MedioPago_TB);
            this.Source_Panel.Controls.Add(this.Cuenta_TB);
            this.Source_Panel.Controls.Add(this.MedioPago_BT);
            this.Source_Panel.Controls.Add(cuentaLabel);
            this.Source_Panel.Controls.Add(this.label2);
            this.Source_Panel.Controls.Add(this.Observaciones_RTB);
            this.Source_Panel.Controls.Add(this.textBox1);
            this.Source_Panel.Controls.Add(observacionesLabel);
            this.Source_Panel.Controls.Add(label6);
            this.Source_Panel.Controls.Add(tipoLabel);
            this.Source_Panel.Controls.Add(this.GastosBancarios_NTB);
            this.Source_Panel.Controls.Add(this.NoAsignado_TB);
            this.Source_Panel.Controls.Add(this.Tarjeta_BT);
            this.Source_Panel.Controls.Add(this.label1);
            this.Source_Panel.Controls.Add(label5);
            this.Source_Panel.Controls.Add(this.Fecha_DTP);
            this.Source_Panel.Controls.Add(this.Tarjeta_TB);
            this.Source_Panel.Controls.Add(fechaLabel);
            this.Source_Panel.Controls.Add(this.Vencimiento_DTP);
            this.Source_Panel.Controls.Add(vencimientoLabel);
            this.Source_Panel.Controls.Add(this.label3);
            this.Source_Panel.Controls.Add(this.Importe_NTB);
            this.Source_Panel.Controls.Add(importeLabel);
            this.Source_Panel.Controls.Add(this.Liberar_BT);
            this.Source_Panel.Controls.Add(this.Cuenta_BT);
            this.Source_Panel.Controls.Add(this.Repartir_BT);
            this.Source_Panel.Location = new System.Drawing.Point(9, 16);
            this.Source_Panel.Name = "Source_Panel";
            this.Source_Panel.Size = new System.Drawing.Size(999, 175);
            this.Source_Panel.TabIndex = 102;
            // 
            // Periodicidad_NTB
            // 
            this.Periodicidad_NTB.Location = new System.Drawing.Point(215, 144);
            this.Periodicidad_NTB.Name = "Periodicidad_NTB";
            this.Periodicidad_NTB.Size = new System.Drawing.Size(54, 21);
            this.Periodicidad_NTB.TabIndex = 104;
            this.Periodicidad_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Periodicidad_NTB.TextIsCurrency = false;
            this.Periodicidad_NTB.TextIsInteger = true;
            // 
            // NPagos_NTB
            // 
            this.NPagos_NTB.Location = new System.Drawing.Point(69, 146);
            this.NPagos_NTB.Name = "NPagos_NTB";
            this.NPagos_NTB.Size = new System.Drawing.Size(54, 21);
            this.NPagos_NTB.TabIndex = 102;
            this.NPagos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NPagos_NTB.TextIsCurrency = false;
            this.NPagos_NTB.TextIsInteger = true;
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.Codigo_TB.Enabled = false;
            this.Codigo_TB.Location = new System.Drawing.Point(63, 13);
            this.Codigo_TB.Name = "Codigo_TB";
            this.Codigo_TB.ReadOnly = true;
            this.Codigo_TB.Size = new System.Drawing.Size(78, 21);
            this.Codigo_TB.TabIndex = 89;
            this.Codigo_TB.TabStop = false;
            this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MedioPago_TB
            // 
            this.MedioPago_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "MedioPagoLabel", true));
            this.MedioPago_TB.Location = new System.Drawing.Point(406, 13);
            this.MedioPago_TB.Name = "MedioPago_TB";
            this.MedioPago_TB.ReadOnly = true;
            this.MedioPago_TB.Size = new System.Drawing.Size(200, 21);
            this.MedioPago_TB.TabIndex = 101;
            this.MedioPago_TB.TabStop = false;
            // 
            // Cuenta_TB
            // 
            this.Cuenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaBancaria", true));
            this.Cuenta_TB.Location = new System.Drawing.Point(406, 40);
            this.Cuenta_TB.Name = "Cuenta_TB";
            this.Cuenta_TB.ReadOnly = true;
            this.Cuenta_TB.Size = new System.Drawing.Size(200, 21);
            this.Cuenta_TB.TabIndex = 72;
            this.Cuenta_TB.TabStop = false;
            // 
            // MedioPago_BT
            // 
            this.MedioPago_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.MedioPago_BT.Location = new System.Drawing.Point(612, 13);
            this.MedioPago_BT.Name = "MedioPago_BT";
            this.MedioPago_BT.Size = new System.Drawing.Size(28, 22);
            this.MedioPago_BT.TabIndex = 3;
            this.MedioPago_BT.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(153, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Tipo:";
            // 
            // Observaciones_RTB
            // 
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_RTB.Location = new System.Drawing.Point(665, 31);
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.Observaciones_RTB.Size = new System.Drawing.Size(320, 131);
            this.Observaciones_RTB.TabIndex = 10;
            this.Observaciones_RTB.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TipoPagoLabel", true));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(190, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(101, 21);
            this.textBox1.TabIndex = 98;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GastosBancarios_NTB
            // 
            this.GastosBancarios_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "GastosBancarios", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.GastosBancarios_NTB.Location = new System.Drawing.Point(406, 144);
            this.GastosBancarios_NTB.Name = "GastosBancarios_NTB";
            this.GastosBancarios_NTB.Size = new System.Drawing.Size(54, 21);
            this.GastosBancarios_NTB.TabIndex = 9;
            this.GastosBancarios_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GastosBancarios_NTB.TextIsCurrency = false;
            this.GastosBancarios_NTB.TextIsInteger = false;
            // 
            // NoAsignado_TB
            // 
            this.NoAsignado_TB.Enabled = false;
            this.NoAsignado_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NoAsignado_TB.Location = new System.Drawing.Point(147, 76);
            this.NoAsignado_TB.Name = "NoAsignado_TB";
            this.NoAsignado_TB.ReadOnly = true;
            this.NoAsignado_TB.Size = new System.Drawing.Size(98, 21);
            this.NoAsignado_TB.TabIndex = 71;
            this.NoAsignado_TB.TabStop = false;
            this.NoAsignado_TB.Text = "0.00";
            this.NoAsignado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Tarjeta_BT
            // 
            this.Tarjeta_BT.Enabled = false;
            this.Tarjeta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Tarjeta_BT.Location = new System.Drawing.Point(612, 66);
            this.Tarjeta_BT.Name = "Tarjeta_BT";
            this.Tarjeta_BT.Size = new System.Drawing.Size(29, 22);
            this.Tarjeta_BT.TabIndex = 5;
            this.Tarjeta_BT.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(144, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "No Asignado:";
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(406, 94);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(145, 21);
            this.Fecha_DTP.TabIndex = 6;
            // 
            // Tarjeta_TB
            // 
            this.Tarjeta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TarjetaCredito", true));
            this.Tarjeta_TB.Location = new System.Drawing.Point(406, 67);
            this.Tarjeta_TB.Name = "Tarjeta_TB";
            this.Tarjeta_TB.ReadOnly = true;
            this.Tarjeta_TB.Size = new System.Drawing.Size(200, 21);
            this.Tarjeta_TB.TabIndex = 93;
            this.Tarjeta_TB.TabStop = false;
            // 
            // Vencimiento_DTP
            // 
            this.Vencimiento_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vencimiento_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vencimiento_DTP.Location = new System.Drawing.Point(406, 121);
            this.Vencimiento_DTP.Name = "Vencimiento_DTP";
            this.Vencimiento_DTP.ShowCheckBox = true;
            this.Vencimiento_DTP.Size = new System.Drawing.Size(145, 21);
            this.Vencimiento_DTP.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "Código:";
            // 
            // Importe_NTB
            // 
            this.Importe_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Importe", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Importe_NTB.Location = new System.Drawing.Point(43, 76);
            this.Importe_NTB.Name = "Importe_NTB";
            this.Importe_NTB.Size = new System.Drawing.Size(98, 21);
            this.Importe_NTB.TabIndex = 0;
            this.Importe_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Importe_NTB.TextIsCurrency = false;
            this.Importe_NTB.TextIsInteger = false;
            // 
            // Liberar_BT
            // 
            this.Liberar_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Liberar_BT.Location = new System.Drawing.Point(74, 103);
            this.Liberar_BT.Name = "Liberar_BT";
            this.Liberar_BT.Size = new System.Drawing.Size(67, 23);
            this.Liberar_BT.TabIndex = 1;
            this.Liberar_BT.Tag = "NO FORMAT";
            this.Liberar_BT.Text = "Ninguno";
            this.Liberar_BT.UseVisualStyleBackColor = true;
            // 
            // Cuenta_BT
            // 
            this.Cuenta_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Cuenta_BT.Location = new System.Drawing.Point(612, 40);
            this.Cuenta_BT.Name = "Cuenta_BT";
            this.Cuenta_BT.Size = new System.Drawing.Size(29, 21);
            this.Cuenta_BT.TabIndex = 4;
            this.Cuenta_BT.UseVisualStyleBackColor = true;
            // 
            // Repartir_BT
            // 
            this.Repartir_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Repartir_BT.Location = new System.Drawing.Point(147, 103);
            this.Repartir_BT.Name = "Repartir_BT";
            this.Repartir_BT.Size = new System.Drawing.Size(67, 23);
            this.Repartir_BT.TabIndex = 2;
            this.Repartir_BT.Tag = "NO FORMAT";
            this.Repartir_BT.Text = "Repartir";
            this.Repartir_BT.UseVisualStyleBackColor = true;
            // 
            // Main_TLP
            // 
            this.Main_TLP.ColumnCount = 1;
            this.Main_TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Main_TLP.Controls.Add(this.General_GB, 0, 0);
            this.Main_TLP.Controls.Add(this.Pagos_GB, 0, 2);
            this.Main_TLP.Controls.Add(this.Expenses_GB, 0, 1);
            this.Main_TLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_TLP.Location = new System.Drawing.Point(0, 0);
            this.Main_TLP.Name = "Main_TLP";
            this.Main_TLP.RowCount = 3;
            this.Main_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Main_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Main_TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Main_TLP.Size = new System.Drawing.Size(1016, 600);
            this.Main_TLP.TabIndex = 1;
            // 
            // Pagos_GB
            // 
            this.Pagos_GB.Controls.Add(this.Payments_SC);
            this.Pagos_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pagos_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pagos_GB.Location = new System.Drawing.Point(3, 406);
            this.Pagos_GB.Name = "Pagos_GB";
            this.Pagos_GB.Size = new System.Drawing.Size(1010, 191);
            this.Pagos_GB.TabIndex = 1;
            this.Pagos_GB.TabStop = false;
            this.Pagos_GB.Text = "Plazos";
            // 
            // Payments_SC
            // 
            this.Payments_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Payments_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Payments_SC.Location = new System.Drawing.Point(3, 17);
            this.Payments_SC.Name = "Payments_SC";
            this.Payments_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Payments_SC.Panel1
            // 
            this.Payments_SC.Panel1.Controls.Add(this.Payments_TS);
            this.Payments_SC.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // Payments_SC.Panel2
            // 
            this.Payments_SC.Panel2.Controls.Add(this.Payments_DGW);
            this.Payments_SC.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Payments_SC.Size = new System.Drawing.Size(1004, 171);
            this.Payments_SC.SplitterDistance = 38;
            this.Payments_SC.SplitterWidth = 1;
            this.Payments_SC.TabIndex = 8;
            // 
            // Payments_TS
            // 
            this.Payments_TS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Payments_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Payments_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditPago_TI,
            this.ViewPago_TI,
            this.Generar_BT});
            this.Payments_TS.Location = new System.Drawing.Point(0, 0);
            this.Payments_TS.Name = "Payments_TS";
            this.HelpProvider.SetShowHelp(this.Payments_TS, true);
            this.Payments_TS.Size = new System.Drawing.Size(1004, 38);
            this.Payments_TS.TabIndex = 5;
            // 
            // EditPago_TI
            // 
            this.EditPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_edit;
            this.EditPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditPago_TI.Name = "EditPago_TI";
            this.EditPago_TI.Size = new System.Drawing.Size(36, 35);
            this.EditPago_TI.Text = "Editar";
            this.EditPago_TI.Click += new System.EventHandler(this.EditPago_TI_Click);
            // 
            // ViewPago_TI
            // 
            this.ViewPago_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewPago_TI.Image = global::moleQule.Face.Store.Properties.Resources.item_view;
            this.ViewPago_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewPago_TI.Name = "ViewPago_TI";
            this.ViewPago_TI.Size = new System.Drawing.Size(36, 35);
            this.ViewPago_TI.Text = "Ver";
            this.ViewPago_TI.Click += new System.EventHandler(this.ViewPago_TI_Click);
            // 
            // Generar_BT
            // 
            this.Generar_BT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Generar_BT.Image = global::moleQule.Face.Store.Properties.Resources.working;
            this.Generar_BT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Generar_BT.Name = "Generar_BT";
            this.Generar_BT.Size = new System.Drawing.Size(36, 35);
            this.Generar_BT.Text = "Generar Pagos";
            this.Generar_BT.Click += new System.EventHandler(this.Generar_BT_Click);
            // 
            // Payments_DGW
            // 
            this.Payments_DGW.AllowUserToAddRows = false;
            this.Payments_DGW.AllowUserToDeleteRows = false;
            this.Payments_DGW.AutoGenerateColumns = false;
            this.Payments_DGW.ColumnHeadersHeight = 36;
            this.Payments_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.calendarColumn1,
            this.MedioPagoLabel,
            this.IDMovimientoBanco,
            this.IDLineaCaja,
            this.Vencimiento,
            this.CuentaBancaria,
            this.Entidad,
            this.Pagado,
            this.IDMovimientoContable,
            this.Usuario,
            this.EstadoLabel,
            this.Observaciones,
            this.Importe,
            this.PendienteAsignacion});
            this.Payments_DGW.DataSource = this.Payments_BS;
            this.Payments_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Payments_DGW.Location = new System.Drawing.Point(0, 0);
            this.Payments_DGW.MultiSelect = false;
            this.Payments_DGW.Name = "Payments_DGW";
            this.Payments_DGW.ReadOnly = true;
            this.Payments_DGW.RowHeadersWidth = 25;
            this.Payments_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Payments_DGW.Size = new System.Drawing.Size(1004, 132);
            this.Payments_DGW.TabIndex = 3;
            this.Payments_DGW.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Pagos_DGW_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Codigo";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 45;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.DataPropertyName = "Fecha";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.Format = "d";
            this.calendarColumn1.DefaultCellStyle = dataGridViewCellStyle30;
            this.calendarColumn1.HeaderText = "Fecha";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.ReadOnly = true;
            this.calendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.calendarColumn1.Width = 70;
            // 
            // MedioPagoLabel
            // 
            this.MedioPagoLabel.DataPropertyName = "MedioPagoLabel";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MedioPagoLabel.DefaultCellStyle = dataGridViewCellStyle31;
            this.MedioPagoLabel.HeaderText = "Medio Pago";
            this.MedioPagoLabel.Name = "MedioPagoLabel";
            this.MedioPagoLabel.ReadOnly = true;
            this.MedioPagoLabel.Width = 70;
            // 
            // IDMovimientoBanco
            // 
            this.IDMovimientoBanco.DataPropertyName = "IDMovimientoBanco";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IDMovimientoBanco.DefaultCellStyle = dataGridViewCellStyle32;
            this.IDMovimientoBanco.HeaderText = "ID Apunte";
            this.IDMovimientoBanco.Name = "IDMovimientoBanco";
            this.IDMovimientoBanco.ReadOnly = true;
            this.IDMovimientoBanco.Width = 60;
            // 
            // IDLineaCaja
            // 
            this.IDLineaCaja.DataPropertyName = "IDLineaCaja";
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IDLineaCaja.DefaultCellStyle = dataGridViewCellStyle33;
            this.IDLineaCaja.HeaderText = "ID Caja";
            this.IDLineaCaja.Name = "IDLineaCaja";
            this.IDLineaCaja.ReadOnly = true;
            this.IDLineaCaja.Width = 60;
            // 
            // Vencimiento
            // 
            this.Vencimiento.DataPropertyName = "Vencimiento";
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle34.Format = "d";
            this.Vencimiento.DefaultCellStyle = dataGridViewCellStyle34;
            this.Vencimiento.HeaderText = "Fecha Vto.";
            this.Vencimiento.Name = "Vencimiento";
            this.Vencimiento.ReadOnly = true;
            this.Vencimiento.Width = 70;
            // 
            // CuentaBancaria
            // 
            this.CuentaBancaria.DataPropertyName = "CuentaBancaria";
            this.CuentaBancaria.HeaderText = "Cuenta Bancaria";
            this.CuentaBancaria.Name = "CuentaBancaria";
            this.CuentaBancaria.ReadOnly = true;
            // 
            // Entidad
            // 
            this.Entidad.DataPropertyName = "Entidad";
            this.Entidad.HeaderText = "Entidad";
            this.Entidad.Name = "Entidad";
            this.Entidad.ReadOnly = true;
            // 
            // Pagado
            // 
            this.Pagado.DataPropertyName = "Pagado";
            this.Pagado.HeaderText = "Pagado";
            this.Pagado.Name = "Pagado";
            this.Pagado.ReadOnly = true;
            this.Pagado.Width = 60;
            // 
            // IDMovimientoContable
            // 
            this.IDMovimientoContable.DataPropertyName = "IDMovimientoContable";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IDMovimientoContable.DefaultCellStyle = dataGridViewCellStyle35;
            this.IDMovimientoContable.HeaderText = "ID Mov. Contable";
            this.IDMovimientoContable.Name = "IDMovimientoContable";
            this.IDMovimientoContable.ReadOnly = true;
            this.IDMovimientoContable.Width = 60;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            this.Usuario.Width = 60;
            // 
            // EstadoLabel
            // 
            this.EstadoLabel.DataPropertyName = "EstadoLabel";
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EstadoLabel.DefaultCellStyle = dataGridViewCellStyle36;
            this.EstadoLabel.HeaderText = "Estado";
            this.EstadoLabel.Name = "EstadoLabel";
            this.EstadoLabel.ReadOnly = true;
            this.EstadoLabel.Width = 70;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            this.Observaciones.Width = 200;
            // 
            // Importe
            // 
            this.Importe.DataPropertyName = "Importe";
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle37.Format = "C2";
            dataGridViewCellStyle37.NullValue = "0";
            this.Importe.DefaultCellStyle = dataGridViewCellStyle37;
            this.Importe.HeaderText = "Importe";
            this.Importe.MinimumWidth = 75;
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 75;
            // 
            // PendienteAsignacion
            // 
            this.PendienteAsignacion.DataPropertyName = "Pendiente";
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle38.Format = "c2";
            dataGridViewCellStyle38.NullValue = "0";
            this.PendienteAsignacion.DefaultCellStyle = dataGridViewCellStyle38;
            this.PendienteAsignacion.HeaderText = "Pendiente Asignar";
            this.PendienteAsignacion.Name = "PendienteAsignacion";
            this.PendienteAsignacion.ReadOnly = true;
            this.PendienteAsignacion.Width = 70;
            // 
            // Payments_BS
            // 
            this.Payments_BS.DataSource = typeof(moleQule.Library.Store.Payment);
            // 
            // PagoFraccionadoForm
            // 
            this.ClientSize = new System.Drawing.Size(1018, 657);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PagoFraccionadoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Pago de Gastos";
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
            ((System.ComponentModel.ISupportInitialize)(this.Expenses_BS)).EndInit();
            this.Expenses_GB.ResumeLayout(false);
            this.Expenses_Panel.Panel1.ResumeLayout(false);
            this.Expenses_Panel.Panel1.PerformLayout();
            this.Expenses_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Expenses_Panel)).EndInit();
            this.Expenses_Panel.ResumeLayout(false);
            this.Facturas_TS.ResumeLayout(false);
            this.Facturas_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).EndInit();
            this.General_GB.ResumeLayout(false);
            this.Source_Panel.ResumeLayout(false);
            this.Source_Panel.PerformLayout();
            this.Main_TLP.ResumeLayout(false);
            this.Pagos_GB.ResumeLayout(false);
            this.Payments_SC.Panel1.ResumeLayout(false);
            this.Payments_SC.Panel1.PerformLayout();
            this.Payments_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Payments_SC)).EndInit();
            this.Payments_SC.ResumeLayout(false);
            this.Payments_TS.ResumeLayout(false);
            this.Payments_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource Expenses_BS;
        private System.Windows.Forms.TableLayoutPanel Main_TLP;
        private System.Windows.Forms.GroupBox General_GB;
        private System.Windows.Forms.Panel Source_Panel;
        private System.Windows.Forms.TextBox Codigo_TB;
        protected System.Windows.Forms.TextBox MedioPago_TB;
        protected System.Windows.Forms.TextBox Cuenta_TB;
        public System.Windows.Forms.Button MedioPago_BT;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.RichTextBox Observaciones_RTB;
        private System.Windows.Forms.TextBox textBox1;
        protected Controls.NumericTextBox GastosBancarios_NTB;
        protected System.Windows.Forms.TextBox NoAsignado_TB;
        protected System.Windows.Forms.Button Tarjeta_BT;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.DateTimePicker Fecha_DTP;
        protected System.Windows.Forms.TextBox Tarjeta_TB;
        protected System.Windows.Forms.DateTimePicker Vencimiento_DTP;
        private System.Windows.Forms.Label label3;
        protected Controls.NumericTextBox Importe_NTB;
        protected System.Windows.Forms.Button Liberar_BT;
        protected System.Windows.Forms.Button Cuenta_BT;
        protected System.Windows.Forms.Button Repartir_BT;
        private System.Windows.Forms.GroupBox Expenses_GB;
        protected System.Windows.Forms.SplitContainer Expenses_Panel;
        protected System.Windows.Forms.ToolStrip Facturas_TS;
        protected System.Windows.Forms.ToolStripButton AddFactura_TI;
        protected System.Windows.Forms.ToolStripButton EditFactura_TI;
        protected System.Windows.Forms.ToolStripButton ViewFactura_TI;
        protected System.Windows.Forms.ToolStripButton DeleteFactura_TI;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        protected System.Windows.Forms.DataGridView Lineas_DGW;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionCol;
        private CalendarColumn fechaDataGridViewTextBoxColumn;
        private CalendarColumn previsionPagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPagado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acumulado;
        private System.Windows.Forms.DataGridViewTextBoxColumn PendienteAsignar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asignado;
        private System.Windows.Forms.DataGridViewButtonColumn Vinculado;
        protected System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.GroupBox Pagos_GB;
        protected System.Windows.Forms.SplitContainer Payments_SC;
        protected System.Windows.Forms.ToolStrip Payments_TS;
        protected System.Windows.Forms.ToolStripButton EditPago_TI;
        protected System.Windows.Forms.ToolStripButton ViewPago_TI;
        protected System.Windows.Forms.DataGridView Payments_DGW;
        protected System.Windows.Forms.BindingSource Payments_BS;
        protected Controls.NumericTextBox NPagos_NTB;
        protected Controls.NumericTextBox Periodicidad_NTB;
        private System.Windows.Forms.ToolStripButton Generar_BT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MedioPagoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDMovimientoBanco;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDLineaCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaBancaria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entidad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pagado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDMovimientoContable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        protected System.Windows.Forms.DataGridViewTextBoxColumn PendienteAsignacion;
		

    }
}
