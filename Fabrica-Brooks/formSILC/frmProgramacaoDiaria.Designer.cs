namespace formSILC
{
    partial class frmProgramacaoDiaria
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Dia");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Mes", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ano", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgramacaoDiaria));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbLegenda = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckbOcultarServicoRealizado = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAntecipacao = new System.Windows.Forms.Label();
            this.lblProgramacaoManual = new System.Windows.Forms.Label();
            this.lblListaReprogramacao = new System.Windows.Forms.Label();
            this.lblCancelada = new System.Windows.Forms.Label();
            this.lblRetornaStatus = new System.Windows.Forms.Label();
            this.lblVermelho = new System.Windows.Forms.Label();
            this.lblFinalizado = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbDataProgAberta = new System.Windows.Forms.GroupBox();
            this.DataProgAberta = new System.Windows.Forms.DateTimePicker();
            this.lblLinhasGrade2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMTRe2 = new System.Windows.Forms.Button();
            this.btnPesquisaCliente = new System.Windows.Forms.Button();
            this.btnMTRe = new System.Windows.Forms.Button();
            this.lblCodigoCliente = new System.Windows.Forms.Label();
            this.txtSenhaAcessoFatma = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCNPJ_CPF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cdnmCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grbBotoes = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNumeroMTRe = new System.Windows.Forms.TextBox();
            this.butAbrir = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnRotaMapa = new System.Windows.Forms.Button();
            this.lblBloqueadoPeloUsuario = new System.Windows.Forms.Label();
            this.btnDadosContrato = new System.Windows.Forms.Button();
            this.btnServicosCliente = new System.Windows.Forms.Button();
            this.btnDadosCliente = new System.Windows.Forms.Button();
            this.btnEncerrarDia = new System.Windows.Forms.Button();
            this.btnProgramado = new System.Windows.Forms.Button();
            this.btnExecutado = new System.Windows.Forms.Button();
            this.Grade2 = new System.Windows.Forms.DataGridView();
            this.Grade1 = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grbLegenda.SuspendLayout();
            this.grbDataProgAberta.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbLegenda
            // 
            this.grbLegenda.BackColor = System.Drawing.Color.LightGray;
            this.grbLegenda.Controls.Add(this.label8);
            this.grbLegenda.Controls.Add(this.ckbOcultarServicoRealizado);
            this.grbLegenda.Controls.Add(this.label7);
            this.grbLegenda.Controls.Add(this.label3);
            this.grbLegenda.Controls.Add(this.lblAntecipacao);
            this.grbLegenda.Controls.Add(this.lblProgramacaoManual);
            this.grbLegenda.Controls.Add(this.lblListaReprogramacao);
            this.grbLegenda.Controls.Add(this.lblCancelada);
            this.grbLegenda.Controls.Add(this.lblRetornaStatus);
            this.grbLegenda.Controls.Add(this.lblVermelho);
            this.grbLegenda.Controls.Add(this.lblFinalizado);
            this.grbLegenda.Controls.Add(this.label2);
            this.grbLegenda.Controls.Add(this.label1);
            this.grbLegenda.Location = new System.Drawing.Point(3, 551);
            this.grbLegenda.Name = "grbLegenda";
            this.grbLegenda.Size = new System.Drawing.Size(191, 208);
            this.grbLegenda.TabIndex = 2;
            this.grbLegenda.TabStop = false;
            this.grbLegenda.Text = "Legenda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Magenta;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Inserção feita a partir do comercial";
            // 
            // ckbOcultarServicoRealizado
            // 
            this.ckbOcultarServicoRealizado.AutoSize = true;
            this.ckbOcultarServicoRealizado.BackColor = System.Drawing.Color.Aquamarine;
            this.ckbOcultarServicoRealizado.Checked = true;
            this.ckbOcultarServicoRealizado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbOcultarServicoRealizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbOcultarServicoRealizado.ForeColor = System.Drawing.Color.Red;
            this.ckbOcultarServicoRealizado.Location = new System.Drawing.Point(3, 162);
            this.ckbOcultarServicoRealizado.Name = "ckbOcultarServicoRealizado";
            this.ckbOcultarServicoRealizado.Size = new System.Drawing.Size(186, 17);
            this.ckbOcultarServicoRealizado.TabIndex = 12;
            this.ckbOcultarServicoRealizado.Text = "Ocultar Antecipados/reprogs feitos";
            this.ckbOcultarServicoRealizado.UseVisualStyleBackColor = false;
            this.ckbOcultarServicoRealizado.Click += new System.EventHandler(this.ckbOcultarServicoRealizado_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "F3 - Ordenação";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGray;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(5, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Insert-Incluir F11-Alterar Progr Manual";
            // 
            // lblAntecipacao
            // 
            this.lblAntecipacao.AutoSize = true;
            this.lblAntecipacao.BackColor = System.Drawing.Color.LightGray;
            this.lblAntecipacao.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.lblAntecipacao.Location = new System.Drawing.Point(6, 132);
            this.lblAntecipacao.Name = "lblAntecipacao";
            this.lblAntecipacao.Size = new System.Drawing.Size(179, 13);
            this.lblAntecipacao.TabIndex = 8;
            this.lblAntecipacao.Text = "F10 - Antecipação Coleta/Del-Exclui";
            // 
            // lblProgramacaoManual
            // 
            this.lblProgramacaoManual.AutoSize = true;
            this.lblProgramacaoManual.BackColor = System.Drawing.Color.LightGray;
            this.lblProgramacaoManual.ForeColor = System.Drawing.Color.DimGray;
            this.lblProgramacaoManual.Location = new System.Drawing.Point(7, 41);
            this.lblProgramacaoManual.Name = "lblProgramacaoManual";
            this.lblProgramacaoManual.Size = new System.Drawing.Size(126, 13);
            this.lblProgramacaoManual.TabIndex = 7;
            this.lblProgramacaoManual.Text = "M - Programação Manual";
            // 
            // lblListaReprogramacao
            // 
            this.lblListaReprogramacao.AutoSize = true;
            this.lblListaReprogramacao.BackColor = System.Drawing.Color.LightGray;
            this.lblListaReprogramacao.ForeColor = System.Drawing.Color.Black;
            this.lblListaReprogramacao.Location = new System.Drawing.Point(6, 106);
            this.lblListaReprogramacao.Name = "lblListaReprogramacao";
            this.lblListaReprogramacao.Size = new System.Drawing.Size(179, 13);
            this.lblListaReprogramacao.TabIndex = 6;
            this.lblListaReprogramacao.Text = "F8 - Reprogramações/Antecipações";
            // 
            // lblCancelada
            // 
            this.lblCancelada.AutoSize = true;
            this.lblCancelada.BackColor = System.Drawing.Color.LightGray;
            this.lblCancelada.ForeColor = System.Drawing.Color.Yellow;
            this.lblCancelada.Location = new System.Drawing.Point(6, 93);
            this.lblCancelada.Name = "lblCancelada";
            this.lblCancelada.Size = new System.Drawing.Size(79, 13);
            this.lblCancelada.TabIndex = 5;
            this.lblCancelada.Text = "F7 - Cancelada";
            // 
            // lblRetornaStatus
            // 
            this.lblRetornaStatus.AutoSize = true;
            this.lblRetornaStatus.BackColor = System.Drawing.Color.LightGray;
            this.lblRetornaStatus.ForeColor = System.Drawing.Color.Black;
            this.lblRetornaStatus.Location = new System.Drawing.Point(6, 80);
            this.lblRetornaStatus.Name = "lblRetornaStatus";
            this.lblRetornaStatus.Size = new System.Drawing.Size(141, 13);
            this.lblRetornaStatus.TabIndex = 4;
            this.lblRetornaStatus.Text = "F6 -  Retorna Status Anterior";
            // 
            // lblVermelho
            // 
            this.lblVermelho.AutoSize = true;
            this.lblVermelho.BackColor = System.Drawing.Color.LightGray;
            this.lblVermelho.ForeColor = System.Drawing.Color.Red;
            this.lblVermelho.Location = new System.Drawing.Point(6, 67);
            this.lblVermelho.Name = "lblVermelho";
            this.lblVermelho.Size = new System.Drawing.Size(145, 13);
            this.lblVermelho.TabIndex = 3;
            this.lblVermelho.Text = "F5 - Reprogramado-Vermelho";
            // 
            // lblFinalizado
            // 
            this.lblFinalizado.AutoSize = true;
            this.lblFinalizado.BackColor = System.Drawing.Color.LightGray;
            this.lblFinalizado.ForeColor = System.Drawing.Color.Navy;
            this.lblFinalizado.Location = new System.Drawing.Point(6, 54);
            this.lblFinalizado.Name = "lblFinalizado";
            this.lblFinalizado.Size = new System.Drawing.Size(104, 13);
            this.lblFinalizado.TabIndex = 2;
            this.lblFinalizado.Text = "F4 - Finalizado - Azul";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(6, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "F9 - Executando - verde";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "F2 - Pesquisa";
            // 
            // grbDataProgAberta
            // 
            this.grbDataProgAberta.BackColor = System.Drawing.Color.LightGray;
            this.grbDataProgAberta.Controls.Add(this.DataProgAberta);
            this.grbDataProgAberta.Controls.Add(this.lblLinhasGrade2);
            this.grbDataProgAberta.Location = new System.Drawing.Point(3, 504);
            this.grbDataProgAberta.Name = "grbDataProgAberta";
            this.grbDataProgAberta.Size = new System.Drawing.Size(191, 41);
            this.grbDataProgAberta.TabIndex = 3;
            this.grbDataProgAberta.TabStop = false;
            this.grbDataProgAberta.Text = "Data Última Prog. Aberta | Ln1|Ln2";
            // 
            // DataProgAberta
            // 
            this.DataProgAberta.Enabled = false;
            this.DataProgAberta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataProgAberta.Location = new System.Drawing.Point(9, 16);
            this.DataProgAberta.Name = "DataProgAberta";
            this.DataProgAberta.Size = new System.Drawing.Size(101, 20);
            this.DataProgAberta.TabIndex = 2;
            // 
            // lblLinhasGrade2
            // 
            this.lblLinhasGrade2.AutoSize = true;
            this.lblLinhasGrade2.Location = new System.Drawing.Point(135, 16);
            this.lblLinhasGrade2.Name = "lblLinhasGrade2";
            this.lblLinhasGrade2.Size = new System.Drawing.Size(13, 13);
            this.lblLinhasGrade2.TabIndex = 1;
            this.lblLinhasGrade2.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.grbDataProgAberta);
            this.panel1.Controls.Add(this.grbLegenda);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 762);
            this.panel1.TabIndex = 8;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Dia";
            treeNode1.Text = "Dia";
            treeNode2.Name = "Mes";
            treeNode2.Text = "Mes";
            treeNode3.Name = "Ano";
            treeNode3.Text = "Ano";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(191, 495);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.grbBotoes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(201, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1314, 69);
            this.panel2.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMTRe2);
            this.groupBox1.Controls.Add(this.btnPesquisaCliente);
            this.groupBox1.Controls.Add(this.btnMTRe);
            this.groupBox1.Controls.Add(this.lblCodigoCliente);
            this.groupBox1.Controls.Add(this.txtSenhaAcessoFatma);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCNPJ_CPF);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cdnmCliente);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(828, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 58);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sistema MTR-e Fátima";
            // 
            // btnMTRe2
            // 
            this.btnMTRe2.AutoSize = true;
            this.btnMTRe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTRe2.Location = new System.Drawing.Point(204, 8);
            this.btnMTRe2.Name = "btnMTRe2";
            this.btnMTRe2.Size = new System.Drawing.Size(41, 23);
            this.btnMTRe2.TabIndex = 10;
            this.btnMTRe2.Text = "MTRe";
            this.btnMTRe2.UseVisualStyleBackColor = true;
            this.btnMTRe2.Click += new System.EventHandler(this.btnMTRe2_Click);
            // 
            // btnPesquisaCliente
            // 
            this.btnPesquisaCliente.AutoSize = true;
            this.btnPesquisaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnPesquisaCliente.Location = new System.Drawing.Point(79, 12);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(46, 19);
            this.btnPesquisaCliente.TabIndex = 9;
            this.btnPesquisaCliente.Text = "Pesquisa";
            this.btnPesquisaCliente.UseVisualStyleBackColor = true;
            this.btnPesquisaCliente.Click += new System.EventHandler(this.btnPesquisaCliente_Click);
            // 
            // btnMTRe
            // 
            this.btnMTRe.AutoSize = true;
            this.btnMTRe.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTRe.Location = new System.Drawing.Point(172, 8);
            this.btnMTRe.Name = "btnMTRe";
            this.btnMTRe.Size = new System.Drawing.Size(37, 23);
            this.btnMTRe.TabIndex = 8;
            this.btnMTRe.Text = "mtre";
            this.btnMTRe.UseVisualStyleBackColor = true;
            this.btnMTRe.Click += new System.EventHandler(this.btnMTRe_Click);
            // 
            // lblCodigoCliente
            // 
            this.lblCodigoCliente.AutoSize = true;
            this.lblCodigoCliente.Location = new System.Drawing.Point(39, 14);
            this.lblCodigoCliente.Name = "lblCodigoCliente";
            this.lblCodigoCliente.Size = new System.Drawing.Size(43, 13);
            this.lblCodigoCliente.TabIndex = 7;
            this.lblCodigoCliente.Text = "000000";
            // 
            // txtSenhaAcessoFatma
            // 
            this.txtSenhaAcessoFatma.Location = new System.Drawing.Point(247, 28);
            this.txtSenhaAcessoFatma.Name = "txtSenhaAcessoFatma";
            this.txtSenhaAcessoFatma.Size = new System.Drawing.Size(91, 20);
            this.txtSenhaAcessoFatma.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Senha de Acesso";
            // 
            // txtCNPJ_CPF
            // 
            this.txtCNPJ_CPF.Location = new System.Drawing.Point(124, 29);
            this.txtCNPJ_CPF.Name = "txtCNPJ_CPF";
            this.txtCNPJ_CPF.Size = new System.Drawing.Size(119, 20);
            this.txtCNPJ_CPF.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "CNPJ/CPF";
            // 
            // cdnmCliente
            // 
            this.cdnmCliente.Enabled = false;
            this.cdnmCliente.Location = new System.Drawing.Point(4, 30);
            this.cdnmCliente.Name = "cdnmCliente";
            this.cdnmCliente.Size = new System.Drawing.Size(118, 20);
            this.cdnmCliente.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cliente:";
            // 
            // grbBotoes
            // 
            this.grbBotoes.Controls.Add(this.label10);
            this.grbBotoes.Controls.Add(this.txtNumeroMTRe);
            this.grbBotoes.Controls.Add(this.butAbrir);
            this.grbBotoes.Controls.Add(this.lblUsuario);
            this.grbBotoes.Controls.Add(this.btnRotaMapa);
            this.grbBotoes.Controls.Add(this.lblBloqueadoPeloUsuario);
            this.grbBotoes.Controls.Add(this.btnDadosContrato);
            this.grbBotoes.Controls.Add(this.btnServicosCliente);
            this.grbBotoes.Controls.Add(this.btnDadosCliente);
            this.grbBotoes.Controls.Add(this.btnEncerrarDia);
            this.grbBotoes.Controls.Add(this.btnProgramado);
            this.grbBotoes.Controls.Add(this.btnExecutado);
            this.grbBotoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbBotoes.Location = new System.Drawing.Point(0, 0);
            this.grbBotoes.Name = "grbBotoes";
            this.grbBotoes.Size = new System.Drawing.Size(1314, 58);
            this.grbBotoes.TabIndex = 5;
            this.grbBotoes.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1169, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Nº Manifesto";
            // 
            // txtNumeroMTRe
            // 
            this.txtNumeroMTRe.Location = new System.Drawing.Point(1171, 28);
            this.txtNumeroMTRe.Name = "txtNumeroMTRe";
            this.txtNumeroMTRe.Size = new System.Drawing.Size(161, 20);
            this.txtNumeroMTRe.TabIndex = 19;
            this.txtNumeroMTRe.Text = "2112240279";
            // 
            // butAbrir
            // 
            this.butAbrir.AutoSize = true;
            this.butAbrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAbrir.Location = new System.Drawing.Point(1238, 6);
            this.butAbrir.Name = "butAbrir";
            this.butAbrir.Size = new System.Drawing.Size(42, 23);
            this.butAbrir.TabIndex = 10;
            this.butAbrir.Text = "Abrir";
            this.butAbrir.UseVisualStyleBackColor = true;
            this.butAbrir.Click += new System.EventHandler(this.butAbrir_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(617, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 8;
            this.lblUsuario.Text = "Usuário";
            // 
            // btnRotaMapa
            // 
            this.btnRotaMapa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRotaMapa.Location = new System.Drawing.Point(773, 10);
            this.btnRotaMapa.Name = "btnRotaMapa";
            this.btnRotaMapa.Size = new System.Drawing.Size(52, 41);
            this.btnRotaMapa.TabIndex = 7;
            this.btnRotaMapa.Text = "Rota Mapa";
            this.btnRotaMapa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRotaMapa.UseVisualStyleBackColor = true;
            this.btnRotaMapa.Click += new System.EventHandler(this.btnRotaMapa_Click);
            // 
            // lblBloqueadoPeloUsuario
            // 
            this.lblBloqueadoPeloUsuario.AutoSize = true;
            this.lblBloqueadoPeloUsuario.Location = new System.Drawing.Point(617, 32);
            this.lblBloqueadoPeloUsuario.Name = "lblBloqueadoPeloUsuario";
            this.lblBloqueadoPeloUsuario.Size = new System.Drawing.Size(125, 13);
            this.lblBloqueadoPeloUsuario.TabIndex = 6;
            this.lblBloqueadoPeloUsuario.Text = "lblBloqueadoPeloUsuario";
            // 
            // btnDadosContrato
            // 
            this.btnDadosContrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDadosContrato.Location = new System.Drawing.Point(515, 12);
            this.btnDadosContrato.Name = "btnDadosContrato";
            this.btnDadosContrato.Size = new System.Drawing.Size(96, 41);
            this.btnDadosContrato.TabIndex = 5;
            this.btnDadosContrato.Text = "Dados do Contrato";
            this.btnDadosContrato.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDadosContrato.UseVisualStyleBackColor = true;
            this.btnDadosContrato.Click += new System.EventHandler(this.btnDadosContrato_Click);
            // 
            // btnServicosCliente
            // 
            this.btnServicosCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnServicosCliente.Location = new System.Drawing.Point(421, 12);
            this.btnServicosCliente.Name = "btnServicosCliente";
            this.btnServicosCliente.Size = new System.Drawing.Size(96, 41);
            this.btnServicosCliente.TabIndex = 4;
            this.btnServicosCliente.Text = "Serviços do Cliente";
            this.btnServicosCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnServicosCliente.UseVisualStyleBackColor = true;
            this.btnServicosCliente.Click += new System.EventHandler(this.btnServicosCliente_Click);
            // 
            // btnDadosCliente
            // 
            this.btnDadosCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDadosCliente.Location = new System.Drawing.Point(327, 12);
            this.btnDadosCliente.Name = "btnDadosCliente";
            this.btnDadosCliente.Size = new System.Drawing.Size(96, 41);
            this.btnDadosCliente.TabIndex = 3;
            this.btnDadosCliente.Text = "Dados Cliente";
            this.btnDadosCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDadosCliente.UseVisualStyleBackColor = true;
            this.btnDadosCliente.Click += new System.EventHandler(this.btnDadosCliente_Click);
            // 
            // btnEncerrarDia
            // 
            this.btnEncerrarDia.Enabled = false;
            this.btnEncerrarDia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEncerrarDia.Location = new System.Drawing.Point(194, 12);
            this.btnEncerrarDia.Name = "btnEncerrarDia";
            this.btnEncerrarDia.Size = new System.Drawing.Size(96, 41);
            this.btnEncerrarDia.TabIndex = 2;
            this.btnEncerrarDia.Text = "Encerrar Dia";
            this.btnEncerrarDia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEncerrarDia.UseVisualStyleBackColor = true;
            this.btnEncerrarDia.Click += new System.EventHandler(this.btnEncerrarDia_Click);
            // 
            // btnProgramado
            // 
            this.btnProgramado.Image = ((System.Drawing.Image)(resources.GetObject("btnProgramado.Image")));
            this.btnProgramado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProgramado.Location = new System.Drawing.Point(100, 12);
            this.btnProgramado.Name = "btnProgramado";
            this.btnProgramado.Size = new System.Drawing.Size(96, 41);
            this.btnProgramado.TabIndex = 1;
            this.btnProgramado.Text = "Programado";
            this.btnProgramado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProgramado.UseVisualStyleBackColor = true;
            this.btnProgramado.Click += new System.EventHandler(this.btnProgramado_Click);
            // 
            // btnExecutado
            // 
            this.btnExecutado.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutado.Image")));
            this.btnExecutado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutado.Location = new System.Drawing.Point(6, 12);
            this.btnExecutado.Name = "btnExecutado";
            this.btnExecutado.Size = new System.Drawing.Size(96, 41);
            this.btnExecutado.TabIndex = 0;
            this.btnExecutado.Text = "Executado";
            this.btnExecutado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExecutado.UseVisualStyleBackColor = true;
            this.btnExecutado.Click += new System.EventHandler(this.btnExecutado_Click);
            // 
            // Grade2
            // 
            this.Grade2.AllowUserToAddRows = false;
            this.Grade2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grade2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grade2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grade2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Grade2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grade2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Grade2.Location = new System.Drawing.Point(201, 316);
            this.Grade2.Name = "Grade2";
            this.Grade2.ReadOnly = true;
            this.Grade2.RowHeadersVisible = false;
            this.Grade2.RowHeadersWidth = 51;
            this.Grade2.Size = new System.Drawing.Size(1314, 446);
            this.Grade2.TabIndex = 19;
            this.Grade2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade2_CellClick);
            this.Grade2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade2_CellContentClick);
            this.Grade2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade2_CellDoubleClick);
            this.Grade2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Grade2_RowPrePaint);
            this.Grade2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade2_KeyUp);
            // 
            // Grade1
            // 
            this.Grade1.AllowUserToAddRows = false;
            this.Grade1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grade1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Grade1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grade1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Grade1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Grade1.Location = new System.Drawing.Point(0, 0);
            this.Grade1.Name = "Grade1";
            this.Grade1.ReadOnly = true;
            this.Grade1.RowHeadersVisible = false;
            this.Grade1.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Grade1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Grade1.Size = new System.Drawing.Size(1314, 234);
            this.Grade1.TabIndex = 16;
            this.Grade1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade1_CellClick);
            this.Grade1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade1_CellDoubleClick);
            this.Grade1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade1_KeyUp);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Maroon;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 234);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1314, 10);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.Grade1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(201, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1314, 247);
            this.panel3.TabIndex = 18;
            // 
            // frmProgramacaoDiaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1515, 762);
            this.Controls.Add(this.Grade2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmProgramacaoDiaria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programação Diária de Serviços";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProgramacaoDiaria_FormClosed);
            this.Load += new System.EventHandler(this.frmProgramacaoDiaria_Load);
            this.grbLegenda.ResumeLayout(false);
            this.grbLegenda.PerformLayout();
            this.grbDataProgAberta.ResumeLayout(false);
            this.grbDataProgAberta.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbBotoes.ResumeLayout(false);
            this.grbBotoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbLegenda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAntecipacao;
        private System.Windows.Forms.Label lblProgramacaoManual;
        private System.Windows.Forms.Label lblListaReprogramacao;
        private System.Windows.Forms.Label lblCancelada;
        private System.Windows.Forms.Label lblRetornaStatus;
        private System.Windows.Forms.Label lblVermelho;
        private System.Windows.Forms.Label lblFinalizado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbDataProgAberta;
        private System.Windows.Forms.Label lblLinhasGrade2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSenhaAcessoFatma;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCNPJ_CPF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox cdnmCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grbBotoes;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnRotaMapa;
        private System.Windows.Forms.Label lblBloqueadoPeloUsuario;
        private System.Windows.Forms.Button btnDadosContrato;
        private System.Windows.Forms.Button btnServicosCliente;
        private System.Windows.Forms.Button btnDadosCliente;
        private System.Windows.Forms.Button btnEncerrarDia;
        private System.Windows.Forms.Button btnProgramado;
        private System.Windows.Forms.Button btnExecutado;
        private System.Windows.Forms.DataGridView Grade2;
        public System.Windows.Forms.DateTimePicker DataProgAberta;
        private System.Windows.Forms.Label lblCodigoCliente;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView Grade1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMTRe;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckbOcultarServicoRealizado;
        private System.Windows.Forms.Button btnPesquisaCliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button butAbrir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNumeroMTRe;
        private System.Windows.Forms.Button btnMTRe2;
    }
}