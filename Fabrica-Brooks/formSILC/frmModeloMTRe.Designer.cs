
namespace formSILC
{
    partial class frmModeloMTRe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModeloMTRe));
            this.grbItem2 = new System.Windows.Forms.GroupBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butClienteArmazenador = new System.Windows.Forms.Button();
            this.butPesquisaDestinoFinal = new System.Windows.Forms.Button();
            this.lblNomeArmazenador = new System.Windows.Forms.Label();
            this.lblNomeTitulo = new System.Windows.Forms.Label();
            this.txtCNPJ_Armazenador = new System.Windows.Forms.TextBox();
            this.lblCNPJ_Armazenador = new System.Windows.Forms.Label();
            this.lblUtilizaArmazenamento = new System.Windows.Forms.Label();
            this.rdbNao = new System.Windows.Forms.RadioButton();
            this.rdbSim = new System.Windows.Forms.RadioButton();
            this.grbItem3 = new System.Windows.Forms.GroupBox();
            this.grvResiduos = new System.Windows.Forms.DataGridView();
            this.CodigoResiduo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIBAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescricaoIBAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoFisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acondicionamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tecnologia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroONU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClasseRisco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeEmbarque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrupoEmbalagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoAcondicionamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoClasse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoEstadoFisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoTecnologia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoUnidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.butInserirResiduo = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.grbItem1 = new System.Windows.Forms.GroupBox();
            this.grvModelos = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeTransportador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbItem4 = new System.Windows.Forms.GroupBox();
            this.int3NossoCodigo = new formSILC.INTEIRO3();
            this.lblTNossoCodigo = new System.Windows.Forms.Label();
            this.butOkResiduos = new System.Windows.Forms.Button();
            this.butOkGerador = new System.Windows.Forms.Button();
            this.butPesquisaClienteGerador = new System.Windows.Forms.Button();
            this.lblNomeGerador = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCNPJ_CPF_Gerador = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.butOkDestinador = new System.Windows.Forms.Button();
            this.butOkTransportador = new System.Windows.Forms.Button();
            this.lblCodigoModelo = new System.Windows.Forms.Label();
            this.lblTituloCodigoModelo = new System.Windows.Forms.Label();
            this.butSalvar = new System.Windows.Forms.Button();
            this.butPesquisaCNPJ_CPF_Destinador = new System.Windows.Forms.Button();
            this.lblNomeDestinador = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCNPJ_CPF_Destinador = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butPesquisaClienteTransportador = new System.Windows.Forms.Button();
            this.butPesquisaDestinoTransportador = new System.Windows.Forms.Button();
            this.lblNomeTransportador = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCNPJ_CPF_Transportador = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNomeModelo = new System.Windows.Forms.TextBox();
            this.lblNomeModelo = new System.Windows.Forms.Label();
            this.butNovo = new System.Windows.Forms.Button();
            this.grbItem2.SuspendLayout();
            this.grbItem3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvResiduos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.grbItem1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvModelos)).BeginInit();
            this.grbItem4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbItem2
            // 
            this.grbItem2.Controls.Add(this.butOk);
            this.grbItem2.Controls.Add(this.butClienteArmazenador);
            this.grbItem2.Controls.Add(this.butPesquisaDestinoFinal);
            this.grbItem2.Controls.Add(this.lblNomeArmazenador);
            this.grbItem2.Controls.Add(this.lblNomeTitulo);
            this.grbItem2.Controls.Add(this.txtCNPJ_Armazenador);
            this.grbItem2.Controls.Add(this.lblCNPJ_Armazenador);
            this.grbItem2.Controls.Add(this.lblUtilizaArmazenamento);
            this.grbItem2.Controls.Add(this.rdbNao);
            this.grbItem2.Controls.Add(this.rdbSim);
            this.grbItem2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbItem2.Location = new System.Drawing.Point(7, 168);
            this.grbItem2.Name = "grbItem2";
            this.grbItem2.Size = new System.Drawing.Size(1271, 71);
            this.grbItem2.TabIndex = 0;
            this.grbItem2.TabStop = false;
            this.grbItem2.Text = "Armazenamento temporário";
            // 
            // butOk
            // 
            this.butOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOk.Location = new System.Drawing.Point(292, 42);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(28, 23);
            this.butOk.TabIndex = 9;
            this.butOk.Text = "Ok";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butClienteArmazenador
            // 
            this.butClienteArmazenador.Image = ((System.Drawing.Image)(resources.GetObject("butClienteArmazenador.Image")));
            this.butClienteArmazenador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butClienteArmazenador.Location = new System.Drawing.Point(370, 42);
            this.butClienteArmazenador.Name = "butClienteArmazenador";
            this.butClienteArmazenador.Size = new System.Drawing.Size(44, 23);
            this.butClienteArmazenador.TabIndex = 8;
            this.butClienteArmazenador.Text = "c";
            this.butClienteArmazenador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butClienteArmazenador.UseVisualStyleBackColor = true;
            this.butClienteArmazenador.Click += new System.EventHandler(this.butClienteArmazenador_Click);
            // 
            // butPesquisaDestinoFinal
            // 
            this.butPesquisaDestinoFinal.Image = ((System.Drawing.Image)(resources.GetObject("butPesquisaDestinoFinal.Image")));
            this.butPesquisaDestinoFinal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPesquisaDestinoFinal.Location = new System.Drawing.Point(324, 42);
            this.butPesquisaDestinoFinal.Name = "butPesquisaDestinoFinal";
            this.butPesquisaDestinoFinal.Size = new System.Drawing.Size(44, 23);
            this.butPesquisaDestinoFinal.TabIndex = 7;
            this.butPesquisaDestinoFinal.Text = "d";
            this.butPesquisaDestinoFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPesquisaDestinoFinal.UseVisualStyleBackColor = true;
            this.butPesquisaDestinoFinal.Click += new System.EventHandler(this.butPesquisaDestinoFinal_Click);
            // 
            // lblNomeArmazenador
            // 
            this.lblNomeArmazenador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomeArmazenador.Location = new System.Drawing.Point(465, 42);
            this.lblNomeArmazenador.Name = "lblNomeArmazenador";
            this.lblNomeArmazenador.Size = new System.Drawing.Size(446, 21);
            this.lblNomeArmazenador.TabIndex = 6;
            // 
            // lblNomeTitulo
            // 
            this.lblNomeTitulo.AutoSize = true;
            this.lblNomeTitulo.Location = new System.Drawing.Point(416, 42);
            this.lblNomeTitulo.Name = "lblNomeTitulo";
            this.lblNomeTitulo.Size = new System.Drawing.Size(49, 17);
            this.lblNomeTitulo.TabIndex = 5;
            this.lblNomeTitulo.Text = "Nome:";
            // 
            // txtCNPJ_Armazenador
            // 
            this.txtCNPJ_Armazenador.Location = new System.Drawing.Point(146, 42);
            this.txtCNPJ_Armazenador.MaxLength = 20;
            this.txtCNPJ_Armazenador.Name = "txtCNPJ_Armazenador";
            this.txtCNPJ_Armazenador.Size = new System.Drawing.Size(143, 23);
            this.txtCNPJ_Armazenador.TabIndex = 4;
            this.txtCNPJ_Armazenador.Enter += new System.EventHandler(this.txtCNPJ_Armazenador_Enter);
            // 
            // lblCNPJ_Armazenador
            // 
            this.lblCNPJ_Armazenador.AutoSize = true;
            this.lblCNPJ_Armazenador.Location = new System.Drawing.Point(3, 42);
            this.lblCNPJ_Armazenador.Name = "lblCNPJ_Armazenador";
            this.lblCNPJ_Armazenador.Size = new System.Drawing.Size(145, 17);
            this.lblCNPJ_Armazenador.TabIndex = 3;
            this.lblCNPJ_Armazenador.Text = "* CNPJ Armazenador:";
            // 
            // lblUtilizaArmazenamento
            // 
            this.lblUtilizaArmazenamento.AutoSize = true;
            this.lblUtilizaArmazenamento.Location = new System.Drawing.Point(3, 19);
            this.lblUtilizaArmazenamento.Name = "lblUtilizaArmazenamento";
            this.lblUtilizaArmazenamento.Size = new System.Drawing.Size(260, 17);
            this.lblUtilizaArmazenamento.TabIndex = 2;
            this.lblUtilizaArmazenamento.Text = "* Utilizará Armazenamento Temporário?";
            // 
            // rdbNao
            // 
            this.rdbNao.AutoSize = true;
            this.rdbNao.Checked = true;
            this.rdbNao.Location = new System.Drawing.Point(327, 19);
            this.rdbNao.Name = "rdbNao";
            this.rdbNao.Size = new System.Drawing.Size(52, 21);
            this.rdbNao.TabIndex = 1;
            this.rdbNao.TabStop = true;
            this.rdbNao.Text = "Não";
            this.rdbNao.UseVisualStyleBackColor = true;
            this.rdbNao.Click += new System.EventHandler(this.rdbNao_Click);
            // 
            // rdbSim
            // 
            this.rdbSim.AutoSize = true;
            this.rdbSim.Location = new System.Drawing.Point(268, 18);
            this.rdbSim.Name = "rdbSim";
            this.rdbSim.Size = new System.Drawing.Size(49, 21);
            this.rdbSim.TabIndex = 0;
            this.rdbSim.Text = "Sim";
            this.rdbSim.UseVisualStyleBackColor = true;
            // 
            // grbItem3
            // 
            this.grbItem3.Controls.Add(this.grvResiduos);
            this.grbItem3.Controls.Add(this.butInserirResiduo);
            this.grbItem3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbItem3.Location = new System.Drawing.Point(5, 245);
            this.grbItem3.Name = "grbItem3";
            this.grbItem3.Size = new System.Drawing.Size(1466, 284);
            this.grbItem3.TabIndex = 3;
            this.grbItem3.TabStop = false;
            this.grbItem3.Text = "Identificação dos Resíduos";
            // 
            // grvResiduos
            // 
            this.grvResiduos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvResiduos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoResiduo,
            this.CodigoIBAMA,
            this.DescricaoIBAMA,
            this.EstadoFisico,
            this.Classe,
            this.Acondicionamento,
            this.Unidade,
            this.Tecnologia,
            this.NumeroONU,
            this.ClasseRisco,
            this.NomeEmbarque,
            this.GrupoEmbalagem,
            this.CodigoAcondicionamento,
            this.CodigoClasse,
            this.CodigoEstadoFisico,
            this.CodigoTecnologia,
            this.CodigoUnidade});
            this.grvResiduos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grvResiduos.Location = new System.Drawing.Point(3, 54);
            this.grvResiduos.Name = "grvResiduos";
            this.grvResiduos.Size = new System.Drawing.Size(1460, 227);
            this.grvResiduos.TabIndex = 2;
            this.grvResiduos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvResiduos_CellClick);
            this.grvResiduos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvResiduos_KeyUp);
            // 
            // CodigoResiduo
            // 
            this.CodigoResiduo.DataPropertyName = "CodigoResiduo";
            this.CodigoResiduo.HeaderText = "Código Resíduo";
            this.CodigoResiduo.Name = "CodigoResiduo";
            this.CodigoResiduo.ReadOnly = true;
            // 
            // CodigoIBAMA
            // 
            this.CodigoIBAMA.DataPropertyName = "CodigoIBAMA";
            this.CodigoIBAMA.HeaderText = "Código IBAMA";
            this.CodigoIBAMA.Name = "CodigoIBAMA";
            this.CodigoIBAMA.ReadOnly = true;
            // 
            // DescricaoIBAMA
            // 
            this.DescricaoIBAMA.DataPropertyName = "DescricaoIBAMA";
            this.DescricaoIBAMA.HeaderText = "Descrição Resíduo";
            this.DescricaoIBAMA.Name = "DescricaoIBAMA";
            this.DescricaoIBAMA.ReadOnly = true;
            this.DescricaoIBAMA.Width = 300;
            // 
            // EstadoFisico
            // 
            this.EstadoFisico.DataPropertyName = "DescricaoEstadoFisico";
            this.EstadoFisico.HeaderText = "Estado Físico";
            this.EstadoFisico.Name = "EstadoFisico";
            // 
            // Classe
            // 
            this.Classe.DataPropertyName = "DescricaoClasse";
            this.Classe.HeaderText = "Classe";
            this.Classe.Name = "Classe";
            // 
            // Acondicionamento
            // 
            this.Acondicionamento.DataPropertyName = "DescricaoAcondicionamento";
            this.Acondicionamento.HeaderText = "Acondicio- namento";
            this.Acondicionamento.Name = "Acondicionamento";
            this.Acondicionamento.ReadOnly = true;
            // 
            // Unidade
            // 
            this.Unidade.DataPropertyName = "DescricaoUnidade";
            this.Unidade.HeaderText = "Unidade";
            this.Unidade.Name = "Unidade";
            // 
            // Tecnologia
            // 
            this.Tecnologia.DataPropertyName = "DescricaoTecnologia";
            this.Tecnologia.HeaderText = "Tecnologia Aplicada";
            this.Tecnologia.Name = "Tecnologia";
            // 
            // NumeroONU
            // 
            this.NumeroONU.DataPropertyName = "NumeroONU";
            this.NumeroONU.HeaderText = "Número ONU";
            this.NumeroONU.Name = "NumeroONU";
            // 
            // ClasseRisco
            // 
            this.ClasseRisco.DataPropertyName = "ClasseRisco";
            this.ClasseRisco.HeaderText = "Classe Risco";
            this.ClasseRisco.Name = "ClasseRisco";
            // 
            // NomeEmbarque
            // 
            this.NomeEmbarque.DataPropertyName = "NomeEmbarque";
            this.NomeEmbarque.HeaderText = "Nome Embarque";
            this.NomeEmbarque.Name = "NomeEmbarque";
            // 
            // GrupoEmbalagem
            // 
            this.GrupoEmbalagem.DataPropertyName = "GrupoEmbalagem";
            this.GrupoEmbalagem.HeaderText = "Grupo Embalagem";
            this.GrupoEmbalagem.Name = "GrupoEmbalagem";
            // 
            // CodigoAcondicionamento
            // 
            this.CodigoAcondicionamento.DataPropertyName = "CodigoAcondicionamento";
            this.CodigoAcondicionamento.HeaderText = "CodigoAcondicionamento";
            this.CodigoAcondicionamento.Name = "CodigoAcondicionamento";
            // 
            // CodigoClasse
            // 
            this.CodigoClasse.DataPropertyName = "CodigoClasse";
            this.CodigoClasse.HeaderText = "CodigoClasse";
            this.CodigoClasse.Name = "CodigoClasse";
            // 
            // CodigoEstadoFisico
            // 
            this.CodigoEstadoFisico.DataPropertyName = "CodigoEstadoFisico";
            this.CodigoEstadoFisico.HeaderText = "CodigoEstadoFisico";
            this.CodigoEstadoFisico.Name = "CodigoEstadoFisico";
            // 
            // CodigoTecnologia
            // 
            this.CodigoTecnologia.DataPropertyName = "CodigoTecnologia";
            this.CodigoTecnologia.HeaderText = "CodigoTecnologia";
            this.CodigoTecnologia.Name = "CodigoTecnologia";
            // 
            // CodigoUnidade
            // 
            this.CodigoUnidade.DataPropertyName = "CodigoUnidade";
            this.CodigoUnidade.HeaderText = "CodigoUnidade";
            this.CodigoUnidade.Name = "CodigoUnidade";
            // 
            // butInserirResiduo
            // 
            this.butInserirResiduo.Location = new System.Drawing.Point(6, 19);
            this.butInserirResiduo.Name = "butInserirResiduo";
            this.butInserirResiduo.Size = new System.Drawing.Size(111, 26);
            this.butInserirResiduo.TabIndex = 1;
            this.butInserirResiduo.Text = "Inserir Resíduo";
            this.butInserirResiduo.UseVisualStyleBackColor = true;
            this.butInserirResiduo.Click += new System.EventHandler(this.butInserirResiduo_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // grbItem1
            // 
            this.grbItem1.Controls.Add(this.grvModelos);
            this.grbItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbItem1.Location = new System.Drawing.Point(0, 0);
            this.grbItem1.Name = "grbItem1";
            this.grbItem1.Size = new System.Drawing.Size(1505, 162);
            this.grbItem1.TabIndex = 4;
            this.grbItem1.TabStop = false;
            this.grbItem1.Text = "Modelo(s) existente(s)";
            this.grbItem1.Enter += new System.EventHandler(this.grbItem1_Enter);
            // 
            // grvModelos
            // 
            this.grvModelos.AllowUserToAddRows = false;
            this.grvModelos.AllowUserToDeleteRows = false;
            this.grvModelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvModelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nome,
            this.NomeTransportador});
            this.grvModelos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvModelos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grvModelos.Location = new System.Drawing.Point(3, 19);
            this.grvModelos.Name = "grvModelos";
            this.grvModelos.Size = new System.Drawing.Size(1499, 140);
            this.grvModelos.TabIndex = 0;
            this.grvModelos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvModelos_CellClick);
            this.grvModelos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvModelos_KeyUp);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome do Modelo";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 400;
            // 
            // NomeTransportador
            // 
            this.NomeTransportador.DataPropertyName = "NomeTransportador";
            this.NomeTransportador.HeaderText = "Transportador";
            this.NomeTransportador.Name = "NomeTransportador";
            this.NomeTransportador.ReadOnly = true;
            this.NomeTransportador.Width = 400;
            // 
            // grbItem4
            // 
            this.grbItem4.Controls.Add(this.int3NossoCodigo);
            this.grbItem4.Controls.Add(this.lblTNossoCodigo);
            this.grbItem4.Controls.Add(this.butOkResiduos);
            this.grbItem4.Controls.Add(this.butOkGerador);
            this.grbItem4.Controls.Add(this.butPesquisaClienteGerador);
            this.grbItem4.Controls.Add(this.lblNomeGerador);
            this.grbItem4.Controls.Add(this.label4);
            this.grbItem4.Controls.Add(this.txtCNPJ_CPF_Gerador);
            this.grbItem4.Controls.Add(this.label8);
            this.grbItem4.Controls.Add(this.butOkDestinador);
            this.grbItem4.Controls.Add(this.butOkTransportador);
            this.grbItem4.Controls.Add(this.lblCodigoModelo);
            this.grbItem4.Controls.Add(this.lblTituloCodigoModelo);
            this.grbItem4.Controls.Add(this.butSalvar);
            this.grbItem4.Controls.Add(this.butPesquisaCNPJ_CPF_Destinador);
            this.grbItem4.Controls.Add(this.lblNomeDestinador);
            this.grbItem4.Controls.Add(this.label5);
            this.grbItem4.Controls.Add(this.txtCNPJ_CPF_Destinador);
            this.grbItem4.Controls.Add(this.label6);
            this.grbItem4.Controls.Add(this.butPesquisaClienteTransportador);
            this.grbItem4.Controls.Add(this.butPesquisaDestinoTransportador);
            this.grbItem4.Controls.Add(this.lblNomeTransportador);
            this.grbItem4.Controls.Add(this.label2);
            this.grbItem4.Controls.Add(this.txtCNPJ_CPF_Transportador);
            this.grbItem4.Controls.Add(this.label3);
            this.grbItem4.Controls.Add(this.txtNomeModelo);
            this.grbItem4.Controls.Add(this.lblNomeModelo);
            this.grbItem4.Controls.Add(this.butNovo);
            this.grbItem4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbItem4.Location = new System.Drawing.Point(5, 535);
            this.grbItem4.Name = "grbItem4";
            this.grbItem4.Size = new System.Drawing.Size(1505, 210);
            this.grbItem4.TabIndex = 4;
            this.grbItem4.TabStop = false;
            this.grbItem4.Text = "Modelo MTR";
            // 
            // int3NossoCodigo
            // 
            this.int3NossoCodigo.Location = new System.Drawing.Point(1152, 136);
            this.int3NossoCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.int3NossoCodigo.Name = "int3NossoCodigo";
            this.int3NossoCodigo.Size = new System.Drawing.Size(45, 21);
            this.int3NossoCodigo.TabIndex = 34;
            // 
            // lblTNossoCodigo
            // 
            this.lblTNossoCodigo.AutoSize = true;
            this.lblTNossoCodigo.Location = new System.Drawing.Point(1054, 138);
            this.lblTNossoCodigo.Name = "lblTNossoCodigo";
            this.lblTNossoCodigo.Size = new System.Drawing.Size(100, 17);
            this.lblTNossoCodigo.TabIndex = 33;
            this.lblTNossoCodigo.Text = "Nosso Código:";
            // 
            // butOkResiduos
            // 
            this.butOkResiduos.Location = new System.Drawing.Point(1374, -1);
            this.butOkResiduos.Name = "butOkResiduos";
            this.butOkResiduos.Size = new System.Drawing.Size(92, 26);
            this.butOkResiduos.TabIndex = 5;
            this.butOkResiduos.Text = "Ok";
            this.butOkResiduos.UseVisualStyleBackColor = true;
            this.butOkResiduos.Click += new System.EventHandler(this.butOkResiduos_Click);
            // 
            // butOkGerador
            // 
            this.butOkGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOkGerador.Location = new System.Drawing.Point(333, 171);
            this.butOkGerador.Name = "butOkGerador";
            this.butOkGerador.Size = new System.Drawing.Size(28, 23);
            this.butOkGerador.TabIndex = 32;
            this.butOkGerador.Text = "Ok";
            this.butOkGerador.UseVisualStyleBackColor = true;
            this.butOkGerador.Click += new System.EventHandler(this.butOkGerador_Click);
            // 
            // butPesquisaClienteGerador
            // 
            this.butPesquisaClienteGerador.Image = ((System.Drawing.Image)(resources.GetObject("butPesquisaClienteGerador.Image")));
            this.butPesquisaClienteGerador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPesquisaClienteGerador.Location = new System.Drawing.Point(360, 171);
            this.butPesquisaClienteGerador.Name = "butPesquisaClienteGerador";
            this.butPesquisaClienteGerador.Size = new System.Drawing.Size(44, 23);
            this.butPesquisaClienteGerador.TabIndex = 31;
            this.butPesquisaClienteGerador.Text = "c";
            this.butPesquisaClienteGerador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPesquisaClienteGerador.UseVisualStyleBackColor = true;
            this.butPesquisaClienteGerador.Click += new System.EventHandler(this.butPesquisaClienteGerador_Click);
            // 
            // lblNomeGerador
            // 
            this.lblNomeGerador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNomeGerador.Location = new System.Drawing.Point(549, 172);
            this.lblNomeGerador.Name = "lblNomeGerador";
            this.lblNomeGerador.Size = new System.Drawing.Size(446, 21);
            this.lblNomeGerador.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Razão Social/Nome:";
            // 
            // txtCNPJ_CPF_Gerador
            // 
            this.txtCNPJ_CPF_Gerador.Location = new System.Drawing.Point(188, 172);
            this.txtCNPJ_CPF_Gerador.MaxLength = 20;
            this.txtCNPJ_CPF_Gerador.Name = "txtCNPJ_CPF_Gerador";
            this.txtCNPJ_CPF_Gerador.Size = new System.Drawing.Size(143, 23);
            this.txtCNPJ_CPF_Gerador.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "CNPJ Gerador:";
            // 
            // butOkDestinador
            // 
            this.butOkDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOkDestinador.Location = new System.Drawing.Point(333, 136);
            this.butOkDestinador.Name = "butOkDestinador";
            this.butOkDestinador.Size = new System.Drawing.Size(28, 23);
            this.butOkDestinador.TabIndex = 25;
            this.butOkDestinador.Text = "Ok";
            this.butOkDestinador.UseVisualStyleBackColor = true;
            this.butOkDestinador.Click += new System.EventHandler(this.butOkDestinador_Click);
            // 
            // butOkTransportador
            // 
            this.butOkTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOkTransportador.Location = new System.Drawing.Point(333, 104);
            this.butOkTransportador.Name = "butOkTransportador";
            this.butOkTransportador.Size = new System.Drawing.Size(28, 23);
            this.butOkTransportador.TabIndex = 24;
            this.butOkTransportador.Text = "Ok";
            this.butOkTransportador.UseVisualStyleBackColor = true;
            this.butOkTransportador.Click += new System.EventHandler(this.butOkTransportador_Click);
            // 
            // lblCodigoModelo
            // 
            this.lblCodigoModelo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoModelo.Location = new System.Drawing.Point(113, 52);
            this.lblCodigoModelo.Name = "lblCodigoModelo";
            this.lblCodigoModelo.Size = new System.Drawing.Size(75, 19);
            this.lblCodigoModelo.TabIndex = 23;
            // 
            // lblTituloCodigoModelo
            // 
            this.lblTituloCodigoModelo.AutoSize = true;
            this.lblTituloCodigoModelo.Location = new System.Drawing.Point(8, 52);
            this.lblTituloCodigoModelo.Name = "lblTituloCodigoModelo";
            this.lblTituloCodigoModelo.Size = new System.Drawing.Size(106, 17);
            this.lblTituloCodigoModelo.TabIndex = 22;
            this.lblTituloCodigoModelo.Text = "Código Modelo:";
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(5, 171);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(64, 26);
            this.butSalvar.TabIndex = 21;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // butPesquisaCNPJ_CPF_Destinador
            // 
            this.butPesquisaCNPJ_CPF_Destinador.Image = ((System.Drawing.Image)(resources.GetObject("butPesquisaCNPJ_CPF_Destinador.Image")));
            this.butPesquisaCNPJ_CPF_Destinador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPesquisaCNPJ_CPF_Destinador.Location = new System.Drawing.Point(360, 136);
            this.butPesquisaCNPJ_CPF_Destinador.Name = "butPesquisaCNPJ_CPF_Destinador";
            this.butPesquisaCNPJ_CPF_Destinador.Size = new System.Drawing.Size(44, 23);
            this.butPesquisaCNPJ_CPF_Destinador.TabIndex = 19;
            this.butPesquisaCNPJ_CPF_Destinador.Text = "d";
            this.butPesquisaCNPJ_CPF_Destinador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPesquisaCNPJ_CPF_Destinador.UseVisualStyleBackColor = true;
            this.butPesquisaCNPJ_CPF_Destinador.Click += new System.EventHandler(this.butPesquisaCNPJ_CPF_Destinador_Click);
            // 
            // lblNomeDestinador
            // 
            this.lblNomeDestinador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNomeDestinador.Location = new System.Drawing.Point(602, 137);
            this.lblNomeDestinador.Name = "lblNomeDestinador";
            this.lblNomeDestinador.Size = new System.Drawing.Size(446, 21);
            this.lblNomeDestinador.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(454, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Razão Social/Nome:";
            // 
            // txtCNPJ_CPF_Destinador
            // 
            this.txtCNPJ_CPF_Destinador.Location = new System.Drawing.Point(188, 137);
            this.txtCNPJ_CPF_Destinador.MaxLength = 20;
            this.txtCNPJ_CPF_Destinador.Name = "txtCNPJ_CPF_Destinador";
            this.txtCNPJ_CPF_Destinador.Size = new System.Drawing.Size(143, 23);
            this.txtCNPJ_CPF_Destinador.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "* CNPJ Destinador:";
            // 
            // butPesquisaClienteTransportador
            // 
            this.butPesquisaClienteTransportador.Image = ((System.Drawing.Image)(resources.GetObject("butPesquisaClienteTransportador.Image")));
            this.butPesquisaClienteTransportador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPesquisaClienteTransportador.Location = new System.Drawing.Point(406, 104);
            this.butPesquisaClienteTransportador.Name = "butPesquisaClienteTransportador";
            this.butPesquisaClienteTransportador.Size = new System.Drawing.Size(44, 23);
            this.butPesquisaClienteTransportador.TabIndex = 14;
            this.butPesquisaClienteTransportador.Text = "c";
            this.butPesquisaClienteTransportador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPesquisaClienteTransportador.UseVisualStyleBackColor = true;
            this.butPesquisaClienteTransportador.Click += new System.EventHandler(this.butPesquisaClienteTransportador_Click);
            // 
            // butPesquisaDestinoTransportador
            // 
            this.butPesquisaDestinoTransportador.Image = ((System.Drawing.Image)(resources.GetObject("butPesquisaDestinoTransportador.Image")));
            this.butPesquisaDestinoTransportador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPesquisaDestinoTransportador.Location = new System.Drawing.Point(360, 104);
            this.butPesquisaDestinoTransportador.Name = "butPesquisaDestinoTransportador";
            this.butPesquisaDestinoTransportador.Size = new System.Drawing.Size(44, 23);
            this.butPesquisaDestinoTransportador.TabIndex = 13;
            this.butPesquisaDestinoTransportador.Text = "d";
            this.butPesquisaDestinoTransportador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPesquisaDestinoTransportador.UseVisualStyleBackColor = true;
            this.butPesquisaDestinoTransportador.Click += new System.EventHandler(this.butPesquisaDestinoTransportador_Click);
            // 
            // lblNomeTransportador
            // 
            this.lblNomeTransportador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNomeTransportador.Location = new System.Drawing.Point(602, 105);
            this.lblNomeTransportador.Name = "lblNomeTransportador";
            this.lblNomeTransportador.Size = new System.Drawing.Size(446, 21);
            this.lblNomeTransportador.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(454, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Razão Social/Nome:";
            // 
            // txtCNPJ_CPF_Transportador
            // 
            this.txtCNPJ_CPF_Transportador.Location = new System.Drawing.Point(188, 105);
            this.txtCNPJ_CPF_Transportador.MaxLength = 20;
            this.txtCNPJ_CPF_Transportador.Name = "txtCNPJ_CPF_Transportador";
            this.txtCNPJ_CPF_Transportador.Size = new System.Drawing.Size(143, 23);
            this.txtCNPJ_CPF_Transportador.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "* CNPJ/CPF Transportador:";
            // 
            // txtNomeModelo
            // 
            this.txtNomeModelo.Location = new System.Drawing.Point(136, 76);
            this.txtNomeModelo.MaxLength = 100;
            this.txtNomeModelo.Name = "txtNomeModelo";
            this.txtNomeModelo.Size = new System.Drawing.Size(461, 23);
            this.txtNomeModelo.TabIndex = 5;
            // 
            // lblNomeModelo
            // 
            this.lblNomeModelo.AutoSize = true;
            this.lblNomeModelo.Location = new System.Drawing.Point(7, 79);
            this.lblNomeModelo.Name = "lblNomeModelo";
            this.lblNomeModelo.Size = new System.Drawing.Size(128, 17);
            this.lblNomeModelo.TabIndex = 4;
            this.lblNomeModelo.Text = "* Nome do Modelo:";
            // 
            // butNovo
            // 
            this.butNovo.Location = new System.Drawing.Point(7, 20);
            this.butNovo.Name = "butNovo";
            this.butNovo.Size = new System.Drawing.Size(111, 26);
            this.butNovo.TabIndex = 0;
            this.butNovo.Text = "Novo modelo";
            this.butNovo.UseVisualStyleBackColor = true;
            this.butNovo.Click += new System.EventHandler(this.butNovo_Click);
            // 
            // frmModeloMTRe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 757);
            this.Controls.Add(this.grbItem4);
            this.Controls.Add(this.grbItem1);
            this.Controls.Add(this.grbItem3);
            this.Controls.Add(this.grbItem2);
            this.MaximizeBox = false;
            this.Name = "frmModeloMTRe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Modelo de MTR Eletrônica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Click += new System.EventHandler(this.frmModeloMTRe_Click);
            this.Resize += new System.EventHandler(this.frmModeloMTRe_Resize);
            this.grbItem2.ResumeLayout(false);
            this.grbItem2.PerformLayout();
            this.grbItem3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvResiduos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.grbItem1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvModelos)).EndInit();
            this.grbItem4.ResumeLayout(false);
            this.grbItem4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbItem2;
        private System.Windows.Forms.Label lblUtilizaArmazenamento;
        private System.Windows.Forms.RadioButton rdbNao;
        private System.Windows.Forms.RadioButton rdbSim;
        private System.Windows.Forms.GroupBox grbItem3;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.GroupBox grbItem1;
        private System.Windows.Forms.GroupBox grbItem4;
        private System.Windows.Forms.Button butClienteArmazenador;
        private System.Windows.Forms.Button butPesquisaDestinoFinal;
        private System.Windows.Forms.Label lblNomeArmazenador;
        private System.Windows.Forms.Label lblNomeTitulo;
        private System.Windows.Forms.TextBox txtCNPJ_Armazenador;
        private System.Windows.Forms.Label lblCNPJ_Armazenador;
        private System.Windows.Forms.Button butNovo;
        private System.Windows.Forms.Button butInserirResiduo;
        private System.Windows.Forms.Button butPesquisaClienteTransportador;
        private System.Windows.Forms.Button butPesquisaDestinoTransportador;
        private System.Windows.Forms.Label lblNomeTransportador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCNPJ_CPF_Transportador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNomeModelo;
        private System.Windows.Forms.Label lblNomeModelo;
        private System.Windows.Forms.Button butPesquisaCNPJ_CPF_Destinador;
        private System.Windows.Forms.Label lblNomeDestinador;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCNPJ_CPF_Destinador;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butSalvar;
        private System.Windows.Forms.Label lblCodigoModelo;
        private System.Windows.Forms.Label lblTituloCodigoModelo;
        private System.Windows.Forms.DataGridView grvResiduos;
        private System.Windows.Forms.DataGridView grvModelos;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butOkDestinador;
        private System.Windows.Forms.Button butOkTransportador;
        private System.Windows.Forms.Button butOkGerador;
        private System.Windows.Forms.Button butPesquisaClienteGerador;
        private System.Windows.Forms.Label lblNomeGerador;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCNPJ_CPF_Gerador;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeTransportador;
        private System.Windows.Forms.Button butOkResiduos;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoResiduo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIBAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoIBAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoFisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acondicionamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tecnologia;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroONU;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClasseRisco;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeEmbarque;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrupoEmbalagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoAcondicionamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoClasse;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEstadoFisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoTecnologia;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoUnidade;
        private System.Windows.Forms.Label lblTNossoCodigo;
        private INTEIRO3 int3NossoCodigo;
    }
}