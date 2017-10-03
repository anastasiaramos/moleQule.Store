namespace moleQule.Face.Store
{
	partial class InformeGastosExpedienteActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformeGastosExpedienteActionForm));
            this.Datos_TiposExp = new System.Windows.Forms.BindingSource(this.components);
            this.Proveedor_GB = new System.Windows.Forms.GroupBox();
            this.MostrarExpediente_GB = new System.Windows.Forms.GroupBox();
            this.Rango_RB = new System.Windows.Forms.RadioButton();
            this.Seleccion_RB = new System.Windows.Forms.RadioButton();
            this.TodosExpediente_RB = new System.Windows.Forms.RadioButton();
            this.RangoExp_GB = new System.Windows.Forms.GroupBox();
            this.ExpedienteIni_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpedienteFin_TB = new System.Windows.Forms.TextBox();
            this.ExpedienteFin_BT = new System.Windows.Forms.Button();
            this.ExpedienteIni_BT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SeleccionExp_GB = new System.Windows.Forms.GroupBox();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.TipoExpediente_CB = new System.Windows.Forms.ComboBox();
            this.Otros_GB = new System.Windows.Forms.GroupBox();
            this.Incompletos_CkB = new System.Windows.Forms.CheckBox();
            this.Formato_GB = new System.Windows.Forms.GroupBox();
            this.Vista_GB = new System.Windows.Forms.GroupBox();
            this.Resumido_RB = new System.Windows.Forms.RadioButton();
            this.Agrupado_RB = new System.Windows.Forms.RadioButton();
            this.Lista_RB = new System.Windows.Forms.RadioButton();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TipoAcreedor_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TiposAcreedor = new System.Windows.Forms.BindingSource(this.components);
            this.Source_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).BeginInit();
            this.Proveedor_GB.SuspendLayout();
            this.MostrarExpediente_GB.SuspendLayout();
            this.RangoExp_GB.SuspendLayout();
            this.SeleccionExp_GB.SuspendLayout();
            this.Otros_GB.SuspendLayout();
            this.Formato_GB.SuspendLayout();
            this.Vista_GB.SuspendLayout();
            this.Fechas_GB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_BT
            // 
            this.Print_BT.Enabled = true;
            this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Print_BT.Location = new System.Drawing.Point(383, 2);
            this.HelpProvider.SetShowHelp(this.Print_BT, true);
            this.Print_BT.Size = new System.Drawing.Size(112, 32);
            this.Print_BT.Text = "Vista &Previa";
            this.Print_BT.Visible = true;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(265, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Size = new System.Drawing.Size(112, 32);
            this.Submit_BT.Visible = false;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(147, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.Size = new System.Drawing.Size(112, 32);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.groupBox1);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.Formato_GB);
            this.Source_GB.Controls.Add(this.Otros_GB);
            this.Source_GB.Controls.Add(this.Proveedor_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(643, 546);
            this.Source_GB.Text = "";
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
            this.PanelesV.Size = new System.Drawing.Size(645, 588);
            this.PanelesV.SplitterDistance = 548;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(118, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(645, 588);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(290, 345);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(290, 260);
            // 
            // Datos_TiposExp
            // 
            this.Datos_TiposExp.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // Proveedor_GB
            // 
            this.Proveedor_GB.Controls.Add(this.MostrarExpediente_GB);
            this.Proveedor_GB.Controls.Add(this.RangoExp_GB);
            this.Proveedor_GB.Controls.Add(this.SeleccionExp_GB);
            this.Proveedor_GB.Controls.Add(this.label12);
            this.Proveedor_GB.Controls.Add(this.TipoExpediente_CB);
            this.Proveedor_GB.Location = new System.Drawing.Point(37, 15);
            this.Proveedor_GB.Name = "Proveedor_GB";
            this.Proveedor_GB.Size = new System.Drawing.Size(568, 220);
            this.Proveedor_GB.TabIndex = 26;
            this.Proveedor_GB.TabStop = false;
            this.Proveedor_GB.Text = "Expediente";
            // 
            // MostrarExpediente_GB
            // 
            this.MostrarExpediente_GB.Controls.Add(this.Rango_RB);
            this.MostrarExpediente_GB.Controls.Add(this.Seleccion_RB);
            this.MostrarExpediente_GB.Controls.Add(this.TodosExpediente_RB);
            this.MostrarExpediente_GB.Location = new System.Drawing.Point(460, 49);
            this.MostrarExpediente_GB.Name = "MostrarExpediente_GB";
            this.MostrarExpediente_GB.Size = new System.Drawing.Size(98, 99);
            this.MostrarExpediente_GB.TabIndex = 34;
            this.MostrarExpediente_GB.TabStop = false;
            this.MostrarExpediente_GB.Text = "Mostrar";
            // 
            // Rango_RB
            // 
            this.Rango_RB.AutoSize = true;
            this.Rango_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rango_RB.Location = new System.Drawing.Point(15, 66);
            this.Rango_RB.Name = "Rango_RB";
            this.Rango_RB.Size = new System.Drawing.Size(56, 17);
            this.Rango_RB.TabIndex = 2;
            this.Rango_RB.Text = "Rango";
            this.Rango_RB.UseVisualStyleBackColor = true;
            this.Rango_RB.Click += new System.EventHandler(this.Rango_RB_Click);
            // 
            // Seleccion_RB
            // 
            this.Seleccion_RB.AutoSize = true;
            this.Seleccion_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Seleccion_RB.Location = new System.Drawing.Point(15, 42);
            this.Seleccion_RB.Name = "Seleccion_RB";
            this.Seleccion_RB.Size = new System.Drawing.Size(69, 17);
            this.Seleccion_RB.TabIndex = 1;
            this.Seleccion_RB.Text = "Selección";
            this.Seleccion_RB.UseVisualStyleBackColor = true;
            this.Seleccion_RB.Click += new System.EventHandler(this.Seleccion_RB_Click);
            // 
            // TodosExpediente_RB
            // 
            this.TodosExpediente_RB.AutoSize = true;
            this.TodosExpediente_RB.Checked = true;
            this.TodosExpediente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosExpediente_RB.Location = new System.Drawing.Point(15, 19);
            this.TodosExpediente_RB.Name = "TodosExpediente_RB";
            this.TodosExpediente_RB.Size = new System.Drawing.Size(54, 17);
            this.TodosExpediente_RB.TabIndex = 0;
            this.TodosExpediente_RB.TabStop = true;
            this.TodosExpediente_RB.Text = "Todos";
            this.TodosExpediente_RB.UseVisualStyleBackColor = true;
            this.TodosExpediente_RB.Click += new System.EventHandler(this.TodosExpediente_RB_Click);
            // 
            // RangoExp_GB
            // 
            this.RangoExp_GB.Controls.Add(this.ExpedienteIni_TB);
            this.RangoExp_GB.Controls.Add(this.label1);
            this.RangoExp_GB.Controls.Add(this.ExpedienteFin_TB);
            this.RangoExp_GB.Controls.Add(this.ExpedienteFin_BT);
            this.RangoExp_GB.Controls.Add(this.ExpedienteIni_BT);
            this.RangoExp_GB.Controls.Add(this.label8);
            this.RangoExp_GB.Enabled = false;
            this.RangoExp_GB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.RangoExp_GB.Location = new System.Drawing.Point(10, 80);
            this.RangoExp_GB.Name = "RangoExp_GB";
            this.RangoExp_GB.Size = new System.Drawing.Size(439, 83);
            this.RangoExp_GB.TabIndex = 33;
            this.RangoExp_GB.TabStop = false;
            this.RangoExp_GB.Text = "Rango";
            // 
            // ExpedienteIni_TB
            // 
            this.ExpedienteIni_TB.Location = new System.Drawing.Point(80, 18);
            this.ExpedienteIni_TB.Name = "ExpedienteIni_TB";
            this.ExpedienteIni_TB.ReadOnly = true;
            this.ExpedienteIni_TB.Size = new System.Drawing.Size(290, 21);
            this.ExpedienteIni_TB.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Inicial:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExpedienteFin_TB
            // 
            this.ExpedienteFin_TB.Location = new System.Drawing.Point(80, 45);
            this.ExpedienteFin_TB.Name = "ExpedienteFin_TB";
            this.ExpedienteFin_TB.ReadOnly = true;
            this.ExpedienteFin_TB.Size = new System.Drawing.Size(290, 21);
            this.ExpedienteFin_TB.TabIndex = 29;
            // 
            // ExpedienteFin_BT
            // 
            this.ExpedienteFin_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.ExpedienteFin_BT.Location = new System.Drawing.Point(376, 44);
            this.ExpedienteFin_BT.Name = "ExpedienteFin_BT";
            this.ExpedienteFin_BT.Size = new System.Drawing.Size(42, 23);
            this.ExpedienteFin_BT.TabIndex = 27;
            this.ExpedienteFin_BT.UseVisualStyleBackColor = true;
            this.ExpedienteFin_BT.Click += new System.EventHandler(this.ExpedienteFin_BT_Click);
            // 
            // ExpedienteIni_BT
            // 
            this.ExpedienteIni_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.ExpedienteIni_BT.Location = new System.Drawing.Point(376, 17);
            this.ExpedienteIni_BT.Name = "ExpedienteIni_BT";
            this.ExpedienteIni_BT.Size = new System.Drawing.Size(42, 23);
            this.ExpedienteIni_BT.TabIndex = 30;
            this.ExpedienteIni_BT.UseVisualStyleBackColor = true;
            this.ExpedienteIni_BT.Click += new System.EventHandler(this.ExpedienteIni_BT_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "Final:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SeleccionExp_GB
            // 
            this.SeleccionExp_GB.Controls.Add(this.Expediente_TB);
            this.SeleccionExp_GB.Controls.Add(this.label2);
            this.SeleccionExp_GB.Controls.Add(this.Expediente_BT);
            this.SeleccionExp_GB.Enabled = false;
            this.SeleccionExp_GB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.SeleccionExp_GB.Location = new System.Drawing.Point(10, 21);
            this.SeleccionExp_GB.Name = "SeleccionExp_GB";
            this.SeleccionExp_GB.Size = new System.Drawing.Size(439, 50);
            this.SeleccionExp_GB.TabIndex = 32;
            this.SeleccionExp_GB.TabStop = false;
            this.SeleccionExp_GB.Text = "Selección";
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.Location = new System.Drawing.Point(80, 20);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(290, 21);
            this.Expediente_TB.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Expediente:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Image = global::moleQule.Face.Store.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(376, 19);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(42, 23);
            this.Expediente_BT.TabIndex = 1;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(117, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Tipo de Expediente:";
            // 
            // TipoExpediente_CB
            // 
            this.TipoExpediente_CB.DataSource = this.Datos_TiposExp;
            this.TipoExpediente_CB.DisplayMember = "Texto";
            this.TipoExpediente_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoExpediente_CB.FormattingEnabled = true;
            this.TipoExpediente_CB.Location = new System.Drawing.Point(226, 179);
            this.TipoExpediente_CB.Name = "TipoExpediente_CB";
            this.TipoExpediente_CB.Size = new System.Drawing.Size(223, 21);
            this.TipoExpediente_CB.TabIndex = 24;
            this.TipoExpediente_CB.ValueMember = "Oid";
            // 
            // Otros_GB
            // 
            this.Otros_GB.Controls.Add(this.Incompletos_CkB);
            this.Otros_GB.Location = new System.Drawing.Point(37, 352);
            this.Otros_GB.Name = "Otros_GB";
            this.Otros_GB.Size = new System.Drawing.Size(568, 55);
            this.Otros_GB.TabIndex = 41;
            this.Otros_GB.TabStop = false;
            this.Otros_GB.Text = "Otros Filtros";
            // 
            // Incompletos_CkB
            // 
            this.Incompletos_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Incompletos_CkB.AutoSize = true;
            this.Incompletos_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Incompletos_CkB.Location = new System.Drawing.Point(206, 22);
            this.Incompletos_CkB.Name = "Incompletos_CkB";
            this.Incompletos_CkB.Size = new System.Drawing.Size(167, 17);
            this.Incompletos_CkB.TabIndex = 4;
            this.Incompletos_CkB.Text = "Solo expedientes incompletos";
            this.Incompletos_CkB.UseVisualStyleBackColor = true;
            // 
            // Formato_GB
            // 
            this.Formato_GB.Controls.Add(this.Vista_GB);
            this.Formato_GB.Location = new System.Drawing.Point(37, 413);
            this.Formato_GB.Name = "Formato_GB";
            this.Formato_GB.Size = new System.Drawing.Size(568, 126);
            this.Formato_GB.TabIndex = 42;
            this.Formato_GB.TabStop = false;
            this.Formato_GB.Text = "Formato";
            // 
            // Vista_GB
            // 
            this.Vista_GB.Controls.Add(this.Resumido_RB);
            this.Vista_GB.Controls.Add(this.Agrupado_RB);
            this.Vista_GB.Controls.Add(this.Lista_RB);
            this.Vista_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vista_GB.Location = new System.Drawing.Point(213, 17);
            this.Vista_GB.Name = "Vista_GB";
            this.Vista_GB.Size = new System.Drawing.Size(142, 103);
            this.Vista_GB.TabIndex = 3;
            this.Vista_GB.TabStop = false;
            this.Vista_GB.Text = "Vista";
            // 
            // Resumido_RB
            // 
            this.Resumido_RB.AutoSize = true;
            this.Resumido_RB.Checked = true;
            this.Resumido_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resumido_RB.Location = new System.Drawing.Point(24, 66);
            this.Resumido_RB.Name = "Resumido_RB";
            this.Resumido_RB.Size = new System.Drawing.Size(71, 17);
            this.Resumido_RB.TabIndex = 2;
            this.Resumido_RB.TabStop = true;
            this.Resumido_RB.Text = "Resumido";
            this.Resumido_RB.UseVisualStyleBackColor = true;
            // 
            // Agrupado_RB
            // 
            this.Agrupado_RB.AutoSize = true;
            this.Agrupado_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Agrupado_RB.Location = new System.Drawing.Point(24, 20);
            this.Agrupado_RB.Name = "Agrupado_RB";
            this.Agrupado_RB.Size = new System.Drawing.Size(72, 17);
            this.Agrupado_RB.TabIndex = 0;
            this.Agrupado_RB.Text = "Agrupado";
            this.Agrupado_RB.UseVisualStyleBackColor = true;
            // 
            // Lista_RB
            // 
            this.Lista_RB.AutoSize = true;
            this.Lista_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lista_RB.Location = new System.Drawing.Point(24, 43);
            this.Lista_RB.Name = "Lista_RB";
            this.Lista_RB.Size = new System.Drawing.Size(95, 17);
            this.Lista_RB.TabIndex = 1;
            this.Lista_RB.Text = "Lista Completa";
            this.Lista_RB.UseVisualStyleBackColor = true;
            // 
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Location = new System.Drawing.Point(37, 295);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 49);
            this.Fechas_GB.TabIndex = 43;
            this.Fechas_GB.TabStop = false;
            this.Fechas_GB.Text = "Fecha";
            // 
            // FInicial_DTP
            // 
            this.FInicial_DTP.Checked = false;
            this.FInicial_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FInicial_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FInicial_DTP.Location = new System.Drawing.Point(146, 18);
            this.FInicial_DTP.Name = "FInicial_DTP";
            this.FInicial_DTP.ShowCheckBox = true;
            this.FInicial_DTP.Size = new System.Drawing.Size(112, 21);
            this.FInicial_DTP.TabIndex = 0;
            // 
            // FFinal_DTP
            // 
            this.FFinal_DTP.Checked = false;
            this.FFinal_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFinal_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFinal_DTP.Location = new System.Drawing.Point(383, 18);
            this.FFinal_DTP.Name = "FFinal_DTP";
            this.FFinal_DTP.ShowCheckBox = true;
            this.FFinal_DTP.Size = new System.Drawing.Size(112, 21);
            this.FFinal_DTP.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(315, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Fecha Final:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(73, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Fecha Inicial:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TipoAcreedor_CB);
            this.groupBox1.Location = new System.Drawing.Point(37, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 49);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Acreedor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(127, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Tipo de Acreedor:";
            // 
            // TipoAcreedor_CB
            // 
            this.TipoAcreedor_CB.DataSource = this.Datos_TiposAcreedor;
            this.TipoAcreedor_CB.DisplayMember = "Texto";
            this.TipoAcreedor_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoAcreedor_CB.FormattingEnabled = true;
            this.TipoAcreedor_CB.Location = new System.Drawing.Point(226, 20);
            this.TipoAcreedor_CB.Name = "TipoAcreedor_CB";
            this.TipoAcreedor_CB.Size = new System.Drawing.Size(223, 21);
            this.TipoAcreedor_CB.TabIndex = 26;
            this.TipoAcreedor_CB.ValueMember = "Oid";
            // 
            // Datos_TiposAcreedor
            // 
            this.Datos_TiposAcreedor.DataSource = typeof(moleQule.ComboBoxSourceList);
            // 
            // InformeGastosExpedienteActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(645, 588);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InformeGastosExpedienteActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Gastos de Expedients";
            this.Source_GB.ResumeLayout(false);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).EndInit();
            this.Proveedor_GB.ResumeLayout(false);
            this.Proveedor_GB.PerformLayout();
            this.MostrarExpediente_GB.ResumeLayout(false);
            this.MostrarExpediente_GB.PerformLayout();
            this.RangoExp_GB.ResumeLayout(false);
            this.RangoExp_GB.PerformLayout();
            this.SeleccionExp_GB.ResumeLayout(false);
            this.SeleccionExp_GB.PerformLayout();
            this.Otros_GB.ResumeLayout(false);
            this.Otros_GB.PerformLayout();
            this.Formato_GB.ResumeLayout(false);
            this.Vista_GB.ResumeLayout(false);
            this.Vista_GB.PerformLayout();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.BindingSource Datos_TiposExp;
		private System.Windows.Forms.GroupBox Proveedor_GB;
		private System.Windows.Forms.GroupBox MostrarExpediente_GB;
		private System.Windows.Forms.RadioButton Rango_RB;
		private System.Windows.Forms.RadioButton Seleccion_RB;
		private System.Windows.Forms.RadioButton TodosExpediente_RB;
		private System.Windows.Forms.GroupBox RangoExp_GB;
		private System.Windows.Forms.TextBox ExpedienteIni_TB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ExpedienteFin_TB;
		private System.Windows.Forms.Button ExpedienteFin_BT;
		private System.Windows.Forms.Button ExpedienteIni_BT;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox SeleccionExp_GB;
		private System.Windows.Forms.TextBox Expediente_TB;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button Expediente_BT;
		private System.Windows.Forms.Label label12;
		public System.Windows.Forms.ComboBox TipoExpediente_CB;
		private System.Windows.Forms.GroupBox Otros_GB;
		private System.Windows.Forms.CheckBox Incompletos_CkB;
		private System.Windows.Forms.GroupBox Formato_GB;
		private System.Windows.Forms.GroupBox Vista_GB;
		private System.Windows.Forms.RadioButton Agrupado_RB;
		private System.Windows.Forms.RadioButton Lista_RB;
		private System.Windows.Forms.RadioButton Resumido_RB;
		private System.Windows.Forms.GroupBox Fechas_GB;
		private Controls.mQDateTimePicker FInicial_DTP;
		private Controls.mQDateTimePicker FFinal_DTP;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox TipoAcreedor_CB;
        private System.Windows.Forms.BindingSource Datos_TiposAcreedor;
    }
}
