namespace formSILC
{
    partial class frmLocacoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocacoes));
            this.GradeLancamentosMTR = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.GradeClientes = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMTR = new System.Windows.Forms.Button();
            this.btnMTRe = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtProcuraCliente = new System.Windows.Forms.TextBox();
            this.DataMostragemFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataMostragemInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panMTRlancamentos = new System.Windows.Forms.Panel();
            this.panDigitacao = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnMovContainer = new System.Windows.Forms.Button();
            this.cboMovContainer = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.GradeMovContainer = new System.Windows.Forms.DataGridView();
            this.lblAcoesGradeMTR = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.grbRetirada = new System.Windows.Forms.GroupBox();
            this.btnReplicarLancamentos = new System.Windows.Forms.Button();
            this.btnDescargaPendente = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInformacoesLogistica = new System.Windows.Forms.TextBox();
            this.DataRetirada = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescricaoContainerRetirada = new System.Windows.Forms.Label();
            this.cboContainerRetirada = new System.Windows.Forms.ComboBox();
            this.grbColocacao = new System.Windows.Forms.GroupBox();
            this.DataColocacao = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDescricaoContainerColocacao = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboContainerColocacao = new System.Windows.Forms.ComboBox();
            this.btnTrocar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnRetirar = new System.Windows.Forms.Button();
            this.btnColocar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DataLancamento = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panLancamentos = new System.Windows.Forms.Panel();
            this.GradeLancamentos = new System.Windows.Forms.DataGridView();
            this.funcionarioRetirada = new formSILC.FUNCIONARIO();
            this.caminhaoRetirada = new formSILC.CAMINHAO();
            this.funcionarioColocacao = new formSILC.FUNCIONARIO();
            this.caminhaoColocacao = new formSILC.CAMINHAO();
            this.cliente1 = new formSILC.CLIENTE();
            this.intNumeroLancamento = new formSILC.INTEIRO();
            this.btnMTRe2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GradeLancamentosMTR)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeClientes)).BeginInit();
            this.panel1.SuspendLayout();
            this.panMTRlancamentos.SuspendLayout();
            this.panDigitacao.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeMovContainer)).BeginInit();
            this.grbRetirada.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbColocacao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panLancamentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeLancamentos)).BeginInit();
            this.SuspendLayout();
            // 
            // GradeLancamentosMTR
            // 
            this.GradeLancamentosMTR.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.GradeLancamentosMTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeLancamentosMTR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradeLancamentosMTR.Location = new System.Drawing.Point(0, 0);
            this.GradeLancamentosMTR.Name = "GradeLancamentosMTR";
            this.GradeLancamentosMTR.RowHeadersWidth = 20;
            this.GradeLancamentosMTR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GradeLancamentosMTR.Size = new System.Drawing.Size(1115, 168);
            this.GradeLancamentosMTR.TabIndex = 2;
            this.GradeLancamentosMTR.TabStop = false;
            this.GradeLancamentosMTR.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeLancamentosMTR_CellContentClick);
            this.GradeLancamentosMTR.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeLancamentosMTR_CellContentDoubleClick);
            this.GradeLancamentosMTR.CurrentCellDirtyStateChanged += new System.EventHandler(this.GradeLancamentosMTR_CurrentCellDirtyStateChanged);
            this.GradeLancamentosMTR.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GradeLancamentosMTR_KeyUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 609);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.GradeClientes);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(255, 441);
            this.panel4.TabIndex = 5;
            // 
            // GradeClientes
            // 
            this.GradeClientes.AllowUserToAddRows = false;
            this.GradeClientes.AllowUserToDeleteRows = false;
            this.GradeClientes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.GradeClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradeClientes.Location = new System.Drawing.Point(0, 0);
            this.GradeClientes.Name = "GradeClientes";
            this.GradeClientes.ReadOnly = true;
            this.GradeClientes.RowHeadersVisible = false;
            this.GradeClientes.RowHeadersWidth = 51;
            this.GradeClientes.Size = new System.Drawing.Size(255, 441);
            this.GradeClientes.TabIndex = 2;
            this.GradeClientes.TabStop = false;
            this.GradeClientes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeClientes_CellContentDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMTR);
            this.panel1.Controls.Add(this.btnMTRe);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.txtProcuraCliente);
            this.panel1.Controls.Add(this.DataMostragemFinal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DataMostragemInicial);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 441);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 168);
            this.panel1.TabIndex = 4;
            // 
            // btnMTR
            // 
            this.btnMTR.Location = new System.Drawing.Point(214, 4);
            this.btnMTR.Name = "btnMTR";
            this.btnMTR.Size = new System.Drawing.Size(40, 22);
            this.btnMTR.TabIndex = 8;
            this.btnMTR.Text = "MTR";
            this.btnMTR.UseVisualStyleBackColor = true;
            this.btnMTR.Click += new System.EventHandler(this.btnMTR_Click);
            // 
            // btnMTRe
            // 
            this.btnMTRe.Location = new System.Drawing.Point(173, 4);
            this.btnMTRe.Name = "btnMTRe";
            this.btnMTRe.Size = new System.Drawing.Size(46, 22);
            this.btnMTRe.TabIndex = 7;
            this.btnMTRe.Text = "MTRe";
            this.btnMTRe.UseVisualStyleBackColor = true;
            this.btnMTRe.Click += new System.EventHandler(this.btnMTRe_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(141, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(34, 22);
            this.btnOk.TabIndex = 6;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtProcuraCliente
            // 
            this.txtProcuraCliente.Location = new System.Drawing.Point(3, 5);
            this.txtProcuraCliente.Name = "txtProcuraCliente";
            this.txtProcuraCliente.Size = new System.Drawing.Size(140, 20);
            this.txtProcuraCliente.TabIndex = 5;
            this.txtProcuraCliente.TabStop = false;
            this.txtProcuraCliente.Leave += new System.EventHandler(this.txtProcuraCliente_Leave);
            // 
            // DataMostragemFinal
            // 
            this.DataMostragemFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataMostragemFinal.Location = new System.Drawing.Point(103, 76);
            this.DataMostragemFinal.Name = "DataMostragemFinal";
            this.DataMostragemFinal.Size = new System.Drawing.Size(86, 20);
            this.DataMostragemFinal.TabIndex = 4;
            this.DataMostragemFinal.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data inicial";
            // 
            // DataMostragemInicial
            // 
            this.DataMostragemInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataMostragemInicial.Location = new System.Drawing.Point(11, 76);
            this.DataMostragemInicial.Name = "DataMostragemInicial";
            this.DataMostragemInicial.Size = new System.Drawing.Size(86, 20);
            this.DataMostragemInicial.TabIndex = 1;
            this.DataMostragemInicial.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Período de amostragem ";
            // 
            // panMTRlancamentos
            // 
            this.panMTRlancamentos.Controls.Add(this.GradeLancamentosMTR);
            this.panMTRlancamentos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panMTRlancamentos.Location = new System.Drawing.Point(255, 441);
            this.panMTRlancamentos.Name = "panMTRlancamentos";
            this.panMTRlancamentos.Size = new System.Drawing.Size(1115, 168);
            this.panMTRlancamentos.TabIndex = 7;
            // 
            // panDigitacao
            // 
            this.panDigitacao.BackColor = System.Drawing.Color.LightGray;
            this.panDigitacao.Controls.Add(this.panel2);
            this.panDigitacao.Controls.Add(this.lblAcoesGradeMTR);
            this.panDigitacao.Controls.Add(this.lblDB);
            this.panDigitacao.Controls.Add(this.grbRetirada);
            this.panDigitacao.Controls.Add(this.grbColocacao);
            this.panDigitacao.Controls.Add(this.btnTrocar);
            this.panDigitacao.Controls.Add(this.btnAnular);
            this.panDigitacao.Controls.Add(this.btnExcluir);
            this.panDigitacao.Controls.Add(this.btnAlterar);
            this.panDigitacao.Controls.Add(this.btnRetirar);
            this.panDigitacao.Controls.Add(this.btnColocar);
            this.panDigitacao.Controls.Add(this.btnSalvar);
            this.panDigitacao.Controls.Add(this.btnNovo);
            this.panDigitacao.Controls.Add(this.groupBox1);
            this.panDigitacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDigitacao.Location = new System.Drawing.Point(255, 103);
            this.panDigitacao.Name = "panDigitacao";
            this.panDigitacao.Size = new System.Drawing.Size(1115, 338);
            this.panDigitacao.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.GradeMovContainer);
            this.panel2.Location = new System.Drawing.Point(801, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 330);
            this.panel2.TabIndex = 176;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnMovContainer);
            this.panel5.Controls.Add(this.cboMovContainer);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(496, 34);
            this.panel5.TabIndex = 177;
            // 
            // btnMovContainer
            // 
            this.btnMovContainer.Location = new System.Drawing.Point(239, 2);
            this.btnMovContainer.Name = "btnMovContainer";
            this.btnMovContainer.Size = new System.Drawing.Size(34, 24);
            this.btnMovContainer.TabIndex = 176;
            this.btnMovContainer.TabStop = false;
            this.btnMovContainer.Text = "Ok";
            this.btnMovContainer.UseVisualStyleBackColor = true;
            this.btnMovContainer.Click += new System.EventHandler(this.btnMovContainer_Click);
            // 
            // cboMovContainer
            // 
            this.cboMovContainer.FormattingEnabled = true;
            this.cboMovContainer.Location = new System.Drawing.Point(151, 3);
            this.cboMovContainer.Name = "cboMovContainer";
            this.cboMovContainer.Size = new System.Drawing.Size(82, 21);
            this.cboMovContainer.TabIndex = 175;
            this.cboMovContainer.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 174;
            this.label10.Text = "Movimentação do container:";
            // 
            // GradeMovContainer
            // 
            this.GradeMovContainer.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.GradeMovContainer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeMovContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GradeMovContainer.Location = new System.Drawing.Point(0, 36);
            this.GradeMovContainer.Name = "GradeMovContainer";
            this.GradeMovContainer.ReadOnly = true;
            this.GradeMovContainer.RowHeadersWidth = 20;
            this.GradeMovContainer.Size = new System.Drawing.Size(496, 294);
            this.GradeMovContainer.TabIndex = 176;
            this.GradeMovContainer.TabStop = false;
            this.GradeMovContainer.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeMovContainer_CellContentDoubleClick);
            // 
            // lblAcoesGradeMTR
            // 
            this.lblAcoesGradeMTR.AutoSize = true;
            this.lblAcoesGradeMTR.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAcoesGradeMTR.Location = new System.Drawing.Point(6, 314);
            this.lblAcoesGradeMTR.Name = "lblAcoesGradeMTR";
            this.lblAcoesGradeMTR.Size = new System.Drawing.Size(285, 13);
            this.lblAcoesGradeMTR.TabIndex = 13;
            this.lblAcoesGradeMTR.Text = "GRADE MTR:  INS-Insert  /  Enter - Alterar  /  Del - Apagar";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDB.Location = new System.Drawing.Point(736, 26);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(45, 17);
            this.lblDB.TabIndex = 8;
            this.lblDB.Text = "db: 3";
            // 
            // grbRetirada
            // 
            this.grbRetirada.BackColor = System.Drawing.Color.LightGray;
            this.grbRetirada.Controls.Add(this.btnMTRe2);
            this.grbRetirada.Controls.Add(this.btnReplicarLancamentos);
            this.grbRetirada.Controls.Add(this.btnDescargaPendente);
            this.grbRetirada.Controls.Add(this.groupBox2);
            this.grbRetirada.Controls.Add(this.DataRetirada);
            this.grbRetirada.Controls.Add(this.label9);
            this.grbRetirada.Controls.Add(this.funcionarioRetirada);
            this.grbRetirada.Controls.Add(this.caminhaoRetirada);
            this.grbRetirada.Controls.Add(this.label8);
            this.grbRetirada.Controls.Add(this.lblDescricaoContainerRetirada);
            this.grbRetirada.Controls.Add(this.cboContainerRetirada);
            this.grbRetirada.Location = new System.Drawing.Point(0, 185);
            this.grbRetirada.Name = "grbRetirada";
            this.grbRetirada.Size = new System.Drawing.Size(798, 116);
            this.grbRetirada.TabIndex = 171;
            this.grbRetirada.TabStop = false;
            this.grbRetirada.Text = "RETIRADA";
            // 
            // btnReplicarLancamentos
            // 
            this.btnReplicarLancamentos.Location = new System.Drawing.Point(451, 73);
            this.btnReplicarLancamentos.Name = "btnReplicarLancamentos";
            this.btnReplicarLancamentos.Size = new System.Drawing.Size(105, 37);
            this.btnReplicarLancamentos.TabIndex = 14;
            this.btnReplicarLancamentos.Text = "Replicar Lançamentos";
            this.btnReplicarLancamentos.UseVisualStyleBackColor = true;
            this.btnReplicarLancamentos.Click += new System.EventHandler(this.btnReplicarLancamentos_Click);
            // 
            // btnDescargaPendente
            // 
            this.btnDescargaPendente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargaPendente.Location = new System.Drawing.Point(368, 86);
            this.btnDescargaPendente.Name = "btnDescargaPendente";
            this.btnDescargaPendente.Size = new System.Drawing.Size(75, 23);
            this.btnDescargaPendente.TabIndex = 13;
            this.btnDescargaPendente.TabStop = false;
            this.btnDescargaPendente.Text = "Descarga";
            this.btnDescargaPendente.UseVisualStyleBackColor = true;
            this.btnDescargaPendente.Click += new System.EventHandler(this.btnDescargaPendente_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInformacoesLogistica);
            this.groupBox2.Location = new System.Drawing.Point(612, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 99);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informações logística";
            // 
            // txtInformacoesLogistica
            // 
            this.txtInformacoesLogistica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInformacoesLogistica.Location = new System.Drawing.Point(3, 16);
            this.txtInformacoesLogistica.Multiline = true;
            this.txtInformacoesLogistica.Name = "txtInformacoesLogistica";
            this.txtInformacoesLogistica.Size = new System.Drawing.Size(180, 80);
            this.txtInformacoesLogistica.TabIndex = 9;
            // 
            // DataRetirada
            // 
            this.DataRetirada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataRetirada.Location = new System.Drawing.Point(455, 46);
            this.DataRetirada.Name = "DataRetirada";
            this.DataRetirada.Size = new System.Drawing.Size(96, 20);
            this.DataRetirada.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(365, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Data Retirada";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Container:";
            // 
            // lblDescricaoContainerRetirada
            // 
            this.lblDescricaoContainerRetirada.Location = new System.Drawing.Point(152, 19);
            this.lblDescricaoContainerRetirada.Name = "lblDescricaoContainerRetirada";
            this.lblDescricaoContainerRetirada.Size = new System.Drawing.Size(210, 18);
            this.lblDescricaoContainerRetirada.TabIndex = 2;
            this.lblDescricaoContainerRetirada.Text = "descrição";
            // 
            // cboContainerRetirada
            // 
            this.cboContainerRetirada.FormattingEnabled = true;
            this.cboContainerRetirada.Location = new System.Drawing.Point(62, 19);
            this.cboContainerRetirada.Name = "cboContainerRetirada";
            this.cboContainerRetirada.Size = new System.Drawing.Size(82, 21);
            this.cboContainerRetirada.TabIndex = 5;
            this.cboContainerRetirada.Leave += new System.EventHandler(this.cboContainerRetirada_Leave);
            // 
            // grbColocacao
            // 
            this.grbColocacao.BackColor = System.Drawing.Color.LightGray;
            this.grbColocacao.Controls.Add(this.DataColocacao);
            this.grbColocacao.Controls.Add(this.label7);
            this.grbColocacao.Controls.Add(this.funcionarioColocacao);
            this.grbColocacao.Controls.Add(this.caminhaoColocacao);
            this.grbColocacao.Controls.Add(this.cliente1);
            this.grbColocacao.Controls.Add(this.lblDescricaoContainerColocacao);
            this.grbColocacao.Controls.Add(this.label6);
            this.grbColocacao.Controls.Add(this.cboContainerColocacao);
            this.grbColocacao.Location = new System.Drawing.Point(0, 69);
            this.grbColocacao.Name = "grbColocacao";
            this.grbColocacao.Size = new System.Drawing.Size(795, 110);
            this.grbColocacao.TabIndex = 170;
            this.grbColocacao.TabStop = false;
            this.grbColocacao.Text = "COLOCAÇÃO";
            // 
            // DataColocacao
            // 
            this.DataColocacao.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.DataColocacao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataColocacao.Location = new System.Drawing.Point(456, 67);
            this.DataColocacao.Name = "DataColocacao";
            this.DataColocacao.Size = new System.Drawing.Size(96, 20);
            this.DataColocacao.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(367, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Data Colocação:";
            // 
            // lblDescricaoContainerColocacao
            // 
            this.lblDescricaoContainerColocacao.Location = new System.Drawing.Point(152, 19);
            this.lblDescricaoContainerColocacao.Name = "lblDescricaoContainerColocacao";
            this.lblDescricaoContainerColocacao.Size = new System.Drawing.Size(210, 18);
            this.lblDescricaoContainerColocacao.TabIndex = 3;
            this.lblDescricaoContainerColocacao.Text = "descrição";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Container:";
            // 
            // cboContainerColocacao
            // 
            this.cboContainerColocacao.FormattingEnabled = true;
            this.cboContainerColocacao.Location = new System.Drawing.Point(62, 19);
            this.cboContainerColocacao.Name = "cboContainerColocacao";
            this.cboContainerColocacao.Size = new System.Drawing.Size(82, 21);
            this.cboContainerColocacao.TabIndex = 0;
            this.cboContainerColocacao.Leave += new System.EventHandler(this.cboContainerColocacao_Leave);
            // 
            // btnTrocar
            // 
            this.btnTrocar.BackColor = System.Drawing.Color.Silver;
            this.btnTrocar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrocar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrocar.Location = new System.Drawing.Point(671, 10);
            this.btnTrocar.Name = "btnTrocar";
            this.btnTrocar.Size = new System.Drawing.Size(58, 47);
            this.btnTrocar.TabIndex = 169;
            this.btnTrocar.Text = "F12 Trocar";
            this.btnTrocar.UseVisualStyleBackColor = false;
            this.btnTrocar.Click += new System.EventHandler(this.btnTrocar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Silver;
            this.btnAnular.Image = ((System.Drawing.Image)(resources.GetObject("btnAnular.Image")));
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(485, 10);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(65, 47);
            this.btnAnular.TabIndex = 167;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(415, 10);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(69, 47);
            this.btnExcluir.TabIndex = 166;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.BackColor = System.Drawing.Color.Silver;
            this.btnAlterar.Image = ((System.Drawing.Image)(resources.GetObject("btnAlterar.Image")));
            this.btnAlterar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlterar.Location = new System.Drawing.Point(278, 9);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(70, 48);
            this.btnAlterar.TabIndex = 165;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnRetirar
            // 
            this.btnRetirar.BackColor = System.Drawing.Color.Silver;
            this.btnRetirar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetirar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetirar.Location = new System.Drawing.Point(614, 10);
            this.btnRetirar.Name = "btnRetirar";
            this.btnRetirar.Size = new System.Drawing.Size(56, 47);
            this.btnRetirar.TabIndex = 164;
            this.btnRetirar.Text = "F11 Retirar";
            this.btnRetirar.UseVisualStyleBackColor = false;
            this.btnRetirar.Click += new System.EventHandler(this.btnRetirar_Click);
            // 
            // btnColocar
            // 
            this.btnColocar.BackColor = System.Drawing.Color.Silver;
            this.btnColocar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColocar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColocar.Location = new System.Drawing.Point(550, 10);
            this.btnColocar.Name = "btnColocar";
            this.btnColocar.Size = new System.Drawing.Size(64, 48);
            this.btnColocar.TabIndex = 163;
            this.btnColocar.Text = "F10 Colocar";
            this.btnColocar.UseVisualStyleBackColor = false;
            this.btnColocar.Click += new System.EventHandler(this.btnColocar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.Silver;
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(349, 10);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(65, 47);
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.Silver;
            this.btnNovo.Image = global::formSILC.Properties.Resources.NEW;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(215, 9);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(62, 48);
            this.btnNovo.TabIndex = 10;
            this.btnNovo.Text = "Novo";
            this.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DataLancamento);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.intNumeroLancamento);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 64);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lançamento";
            // 
            // DataLancamento
            // 
            this.DataLancamento.Enabled = false;
            this.DataLancamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataLancamento.Location = new System.Drawing.Point(107, 37);
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.Size = new System.Drawing.Size(96, 20);
            this.DataLancamento.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Data";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Número";
            // 
            // panLancamentos
            // 
            this.panLancamentos.Controls.Add(this.GradeLancamentos);
            this.panLancamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLancamentos.Location = new System.Drawing.Point(255, 0);
            this.panLancamentos.Name = "panLancamentos";
            this.panLancamentos.Size = new System.Drawing.Size(1115, 103);
            this.panLancamentos.TabIndex = 9;
            // 
            // GradeLancamentos
            // 
            this.GradeLancamentos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.GradeLancamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeLancamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradeLancamentos.Location = new System.Drawing.Point(0, 0);
            this.GradeLancamentos.Name = "GradeLancamentos";
            this.GradeLancamentos.ReadOnly = true;
            this.GradeLancamentos.RowHeadersWidth = 20;
            this.GradeLancamentos.Size = new System.Drawing.Size(1115, 103);
            this.GradeLancamentos.TabIndex = 1;
            this.GradeLancamentos.TabStop = false;
            this.GradeLancamentos.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeLancamentos_CellContentDoubleClick);
            this.GradeLancamentos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GradeLancamentos_KeyUp);
            // 
            // funcionarioRetirada
            // 
            this.funcionarioRetirada.Location = new System.Drawing.Point(4, 46);
            this.funcionarioRetirada.Margin = new System.Windows.Forms.Padding(4);
            this.funcionarioRetirada.Name = "funcionarioRetirada";
            this.funcionarioRetirada.Size = new System.Drawing.Size(356, 22);
            this.funcionarioRetirada.TabIndex = 7;
            this.funcionarioRetirada.TabIndexCodigo = 1;
            this.funcionarioRetirada.Enter += new System.EventHandler(this.funcionarioRetirada_Enter);
            this.funcionarioRetirada.Leave += new System.EventHandler(this.funcionarioRetirada_Leave);
            // 
            // caminhaoRetirada
            // 
            this.caminhaoRetirada.Location = new System.Drawing.Point(360, 18);
            this.caminhaoRetirada.Margin = new System.Windows.Forms.Padding(4);
            this.caminhaoRetirada.Name = "caminhaoRetirada";
            this.caminhaoRetirada.Size = new System.Drawing.Size(248, 22);
            this.caminhaoRetirada.TabIndex = 6;
            this.caminhaoRetirada.TabIndexCodigo = 1;
            this.caminhaoRetirada.Enter += new System.EventHandler(this.caminhaoColocacao_Enter);
            this.caminhaoRetirada.Leave += new System.EventHandler(this.caminhaoRetirada_Leave);
            // 
            // funcionarioColocacao
            // 
            this.funcionarioColocacao.Location = new System.Drawing.Point(4, 68);
            this.funcionarioColocacao.Margin = new System.Windows.Forms.Padding(4);
            this.funcionarioColocacao.Name = "funcionarioColocacao";
            this.funcionarioColocacao.Size = new System.Drawing.Size(356, 22);
            this.funcionarioColocacao.TabIndex = 3;
            this.funcionarioColocacao.TabIndexCodigo = 1;
            this.funcionarioColocacao.Load += new System.EventHandler(this.funcionarioColocacao_Load);
            this.funcionarioColocacao.Enter += new System.EventHandler(this.funcionarioColocacao_Enter);
            this.funcionarioColocacao.Leave += new System.EventHandler(this.funcionarioColocacao_Leave);
            // 
            // caminhaoColocacao
            // 
            this.caminhaoColocacao.Location = new System.Drawing.Point(364, 42);
            this.caminhaoColocacao.Margin = new System.Windows.Forms.Padding(4);
            this.caminhaoColocacao.Name = "caminhaoColocacao";
            this.caminhaoColocacao.Size = new System.Drawing.Size(248, 22);
            this.caminhaoColocacao.TabIndex = 2;
            this.caminhaoColocacao.TabIndexCodigo = 1;
            this.caminhaoColocacao.Enter += new System.EventHandler(this.caminhaoColocacao_Enter);
            this.caminhaoColocacao.Leave += new System.EventHandler(this.caminhaoColocacao_Leave);
            // 
            // cliente1
            // 
            this.cliente1.Location = new System.Drawing.Point(4, 42);
            this.cliente1.Margin = new System.Windows.Forms.Padding(4);
            this.cliente1.Name = "cliente1";
            this.cliente1.Size = new System.Drawing.Size(356, 22);
            this.cliente1.TabIndex = 1;
            this.cliente1.TabIndexCodigo = 0;
            this.cliente1.Enter += new System.EventHandler(this.cliente1_Enter);
            this.cliente1.Leave += new System.EventHandler(this.cliente1_Leave);
            // 
            // intNumeroLancamento
            // 
            this.intNumeroLancamento.Enabled = false;
            this.intNumeroLancamento.Location = new System.Drawing.Point(9, 36);
            this.intNumeroLancamento.Margin = new System.Windows.Forms.Padding(4);
            this.intNumeroLancamento.Name = "intNumeroLancamento";
            this.intNumeroLancamento.Size = new System.Drawing.Size(95, 21);
            this.intNumeroLancamento.TabIndex = 1;
            // 
            // btnMTRe2
            // 
            this.btnMTRe2.AutoSize = true;
            this.btnMTRe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTRe2.Location = new System.Drawing.Point(562, 87);
            this.btnMTRe2.Name = "btnMTRe2";
            this.btnMTRe2.Size = new System.Drawing.Size(41, 23);
            this.btnMTRe2.TabIndex = 15;
            this.btnMTRe2.Text = "MTRe";
            this.btnMTRe2.UseVisualStyleBackColor = true;
            this.btnMTRe2.Click += new System.EventHandler(this.btnMTRe2_Click);
            // 
            // frmLocacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 609);
            this.Controls.Add(this.panLancamentos);
            this.Controls.Add(this.panDigitacao);
            this.Controls.Add(this.panMTRlancamentos);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.Name = "frmLocacoes";
            this.Text = "Locações";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLocacoes_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLocacoes_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.GradeLancamentosMTR)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GradeClientes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panMTRlancamentos.ResumeLayout(false);
            this.panDigitacao.ResumeLayout(false);
            this.panDigitacao.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeMovContainer)).EndInit();
            this.grbRetirada.ResumeLayout(false);
            this.grbRetirada.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grbColocacao.ResumeLayout(false);
            this.grbColocacao.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panLancamentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GradeLancamentos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GradeLancamentosMTR;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtProcuraCliente;
        private System.Windows.Forms.DateTimePicker DataMostragemFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DataMostragemInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView GradeClientes;
        private System.Windows.Forms.Panel panMTRlancamentos;
        private System.Windows.Forms.Panel panDigitacao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panLancamentos;
        private System.Windows.Forms.DataGridView GradeLancamentos;
        private System.Windows.Forms.GroupBox groupBox1;
        private INTEIRO intNumeroLancamento;
        private System.Windows.Forms.DateTimePicker DataLancamento;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnAnular;
        public System.Windows.Forms.Button btnExcluir;
        public System.Windows.Forms.Button btnAlterar;
        public System.Windows.Forms.Button btnRetirar;
        public System.Windows.Forms.Button btnColocar;
        public System.Windows.Forms.Button btnSalvar;
        public System.Windows.Forms.Button btnNovo;
        public System.Windows.Forms.Button btnTrocar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grbRetirada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDescricaoContainerRetirada;
        private System.Windows.Forms.ComboBox cboContainerRetirada;
        private System.Windows.Forms.GroupBox grbColocacao;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker DataColocacao;
        public System.Windows.Forms.DateTimePicker DataRetirada;
        public FUNCIONARIO funcionarioRetirada;
        public CAMINHAO caminhaoRetirada;
        public FUNCIONARIO funcionarioColocacao;
        public CAMINHAO caminhaoColocacao;
        public CLIENTE cliente1;
        public System.Windows.Forms.TextBox txtInformacoesLogistica;
        private System.Windows.Forms.Label lblDescricaoContainerColocacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboContainerColocacao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblAcoesGradeMTR;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnMovContainer;
        private System.Windows.Forms.ComboBox cboMovContainer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView GradeMovContainer;
        private System.Windows.Forms.Button btnDescargaPendente;
        private System.Windows.Forms.Button btnReplicarLancamentos;
        private System.Windows.Forms.Button btnMTR;
        private System.Windows.Forms.Button btnMTRe;
        private System.Windows.Forms.Button btnMTRe2;
    }
}