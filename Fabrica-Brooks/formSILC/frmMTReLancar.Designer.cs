
namespace formSILC
{
    partial class frmMTReLancar
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
            this.grvResiduos = new System.Windows.Forms.DataGridView();
            this.CodigoResiduo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIBAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescricaoIBAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoFisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acondicionamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tecnologia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroONU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClasseRisco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeEmbarque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrupoEmbalagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoEstadoFisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoClasse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoAcondicionamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoUnidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoTecnologia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblIdentifResiduos = new System.Windows.Forms.Label();
            this.lblTObservacoes = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCodigoUnidadeGerador = new System.Windows.Forms.TextBox();
            this.lblTCodigoUnidadeGerador = new System.Windows.Forms.Label();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.txtRespEmissao = new System.Windows.Forms.TextBox();
            this.dtpDataEmissao = new System.Windows.Forms.DateTimePicker();
            this.txtCNPJCPFGerador = new System.Windows.Forms.TextBox();
            this.txtNomeRazaoSocialGerador = new System.Windows.Forms.TextBox();
            this.lblTCargo = new System.Windows.Forms.Label();
            this.lblTRespEmissao = new System.Windows.Forms.Label();
            this.lblTDataEmissao = new System.Windows.Forms.Label();
            this.lblTCNPJ_CPF_Gerador = new System.Windows.Forms.Label();
            this.lblTNomeRazaoSocialGerador = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboCodigoUnidadeTransportador = new System.Windows.Forms.ComboBox();
            this.txtPlacaVeiculo = new System.Windows.Forms.TextBox();
            this.txtNomeMotorista = new System.Windows.Forms.TextBox();
            this.dtpDataTransporte = new System.Windows.Forms.DateTimePicker();
            this.txtNomeRazaoSocialTransportador = new System.Windows.Forms.TextBox();
            this.txtCNPJCPF_Transportador = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTNomeMotorista = new System.Windows.Forms.Label();
            this.lblTPlacaVeiculoTransportador = new System.Windows.Forms.Label();
            this.lblTDataTransporte = new System.Windows.Forms.Label();
            this.lblTNomeRazaoSocialTransportador = new System.Windows.Forms.Label();
            this.lblTCNPJTransportador = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboCodigoUnidadeDestinador = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNomeRazaoSocialDestinador = new System.Windows.Forms.TextBox();
            this.txtCNPJCPFDestinador = new System.Windows.Forms.TextBox();
            this.lblTNomeRazaoSocialDestinador = new System.Windows.Forms.Label();
            this.lblTCNPJDestinador = new System.Windows.Forms.Label();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvResiduos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            this.Quantidade,
            this.Unidade,
            this.Tecnologia,
            this.NumeroONU,
            this.ClasseRisco,
            this.NomeEmbarque,
            this.GrupoEmbalagem,
            this.CodigoEstadoFisico,
            this.CodigoClasse,
            this.CodigoAcondicionamento,
            this.CodigoUnidade,
            this.CodigoTecnologia});
            this.grvResiduos.Location = new System.Drawing.Point(12, 39);
            this.grvResiduos.Name = "grvResiduos";
            this.grvResiduos.Size = new System.Drawing.Size(1457, 225);
            this.grvResiduos.TabIndex = 3;
            this.grvResiduos.CurrentCellDirtyStateChanged += new System.EventHandler(this.grvResiduos_CurrentCellDirtyStateChanged);
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
            this.EstadoFisico.ReadOnly = true;
            // 
            // Classe
            // 
            this.Classe.DataPropertyName = "DescricaoClasse";
            this.Classe.HeaderText = "Classe";
            this.Classe.Name = "Classe";
            this.Classe.ReadOnly = true;
            // 
            // Acondicionamento
            // 
            this.Acondicionamento.DataPropertyName = "DescricaoAcondicionamento";
            this.Acondicionamento.HeaderText = "Acondicio- namento";
            this.Acondicionamento.Name = "Acondicionamento";
            this.Acondicionamento.ReadOnly = true;
            // 
            // Quantidade
            // 
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            // 
            // Unidade
            // 
            this.Unidade.DataPropertyName = "DescricaoUnidade";
            this.Unidade.HeaderText = "Unidade";
            this.Unidade.Name = "Unidade";
            this.Unidade.ReadOnly = true;
            // 
            // Tecnologia
            // 
            this.Tecnologia.DataPropertyName = "DescricaoTecnologia";
            this.Tecnologia.HeaderText = "Tecnologia Aplicada";
            this.Tecnologia.Name = "Tecnologia";
            this.Tecnologia.ReadOnly = true;
            // 
            // NumeroONU
            // 
            this.NumeroONU.DataPropertyName = "NumeroONU";
            this.NumeroONU.HeaderText = "Número ONU";
            this.NumeroONU.Name = "NumeroONU";
            this.NumeroONU.ReadOnly = true;
            // 
            // ClasseRisco
            // 
            this.ClasseRisco.DataPropertyName = "ClasseRisco";
            this.ClasseRisco.HeaderText = "Classe Risco";
            this.ClasseRisco.Name = "ClasseRisco";
            this.ClasseRisco.ReadOnly = true;
            // 
            // NomeEmbarque
            // 
            this.NomeEmbarque.DataPropertyName = "NomeEmbarque";
            this.NomeEmbarque.HeaderText = "Nome Embarque";
            this.NomeEmbarque.Name = "NomeEmbarque";
            this.NomeEmbarque.ReadOnly = true;
            // 
            // GrupoEmbalagem
            // 
            this.GrupoEmbalagem.DataPropertyName = "GrupoEmbalagem";
            this.GrupoEmbalagem.HeaderText = "Grupo Embalagem";
            this.GrupoEmbalagem.Name = "GrupoEmbalagem";
            this.GrupoEmbalagem.ReadOnly = true;
            // 
            // CodigoEstadoFisico
            // 
            this.CodigoEstadoFisico.DataPropertyName = "CodigoEstadoFisico";
            this.CodigoEstadoFisico.HeaderText = "CodigoEstadoFisico";
            this.CodigoEstadoFisico.Name = "CodigoEstadoFisico";
            this.CodigoEstadoFisico.ReadOnly = true;
            // 
            // CodigoClasse
            // 
            this.CodigoClasse.DataPropertyName = "CodigoClasse";
            this.CodigoClasse.HeaderText = "CodigoClasse";
            this.CodigoClasse.Name = "CodigoClasse";
            this.CodigoClasse.ReadOnly = true;
            // 
            // CodigoAcondicionamento
            // 
            this.CodigoAcondicionamento.DataPropertyName = "CodigoAcondicionamento";
            this.CodigoAcondicionamento.HeaderText = "CodigoAcondicionamento";
            this.CodigoAcondicionamento.Name = "CodigoAcondicionamento";
            this.CodigoAcondicionamento.ReadOnly = true;
            // 
            // CodigoUnidade
            // 
            this.CodigoUnidade.DataPropertyName = "CodigoUnidade";
            this.CodigoUnidade.HeaderText = "CodigoUnidade";
            this.CodigoUnidade.Name = "CodigoUnidade";
            this.CodigoUnidade.ReadOnly = true;
            // 
            // CodigoTecnologia
            // 
            this.CodigoTecnologia.DataPropertyName = "CodigoTecnologia";
            this.CodigoTecnologia.HeaderText = "CodigoTecnologia";
            this.CodigoTecnologia.Name = "CodigoTecnologia";
            this.CodigoTecnologia.ReadOnly = true;
            // 
            // lblIdentifResiduos
            // 
            this.lblIdentifResiduos.AutoSize = true;
            this.lblIdentifResiduos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentifResiduos.Location = new System.Drawing.Point(17, 18);
            this.lblIdentifResiduos.Name = "lblIdentifResiduos";
            this.lblIdentifResiduos.Size = new System.Drawing.Size(201, 20);
            this.lblIdentifResiduos.TabIndex = 4;
            this.lblIdentifResiduos.Text = "Identificação dos Resíduos";
            // 
            // lblTObservacoes
            // 
            this.lblTObservacoes.AutoSize = true;
            this.lblTObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTObservacoes.Location = new System.Drawing.Point(18, 606);
            this.lblTObservacoes.Name = "lblTObservacoes";
            this.lblTObservacoes.Size = new System.Drawing.Size(81, 15);
            this.lblTObservacoes.TabIndex = 20;
            this.lblTObservacoes.Text = "Observações:";
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(19, 651);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(84, 27);
            this.btnGerar.TabIndex = 21;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodigoUnidadeGerador);
            this.groupBox1.Controls.Add(this.lblTCodigoUnidadeGerador);
            this.groupBox1.Controls.Add(this.txtCargo);
            this.groupBox1.Controls.Add(this.txtRespEmissao);
            this.groupBox1.Controls.Add(this.dtpDataEmissao);
            this.groupBox1.Controls.Add(this.txtCNPJCPFGerador);
            this.groupBox1.Controls.Add(this.txtNomeRazaoSocialGerador);
            this.groupBox1.Controls.Add(this.lblTCargo);
            this.groupBox1.Controls.Add(this.lblTRespEmissao);
            this.groupBox1.Controls.Add(this.lblTDataEmissao);
            this.groupBox1.Controls.Add(this.lblTCNPJ_CPF_Gerador);
            this.groupBox1.Controls.Add(this.lblTNomeRazaoSocialGerador);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 270);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1130, 109);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identificação do Gerador";
            // 
            // txtCodigoUnidadeGerador
            // 
            this.txtCodigoUnidadeGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCodigoUnidadeGerador.Location = new System.Drawing.Point(134, 79);
            this.txtCodigoUnidadeGerador.Name = "txtCodigoUnidadeGerador";
            this.txtCodigoUnidadeGerador.Size = new System.Drawing.Size(72, 21);
            this.txtCodigoUnidadeGerador.TabIndex = 25;
            // 
            // lblTCodigoUnidadeGerador
            // 
            this.lblTCodigoUnidadeGerador.AutoSize = true;
            this.lblTCodigoUnidadeGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCodigoUnidadeGerador.Location = new System.Drawing.Point(21, 81);
            this.lblTCodigoUnidadeGerador.Name = "lblTCodigoUnidadeGerador";
            this.lblTCodigoUnidadeGerador.Size = new System.Drawing.Size(107, 15);
            this.lblTCodigoUnidadeGerador.TabIndex = 24;
            this.lblTCodigoUnidadeGerador.Text = "* Código Unidade:";
            // 
            // txtCargo
            // 
            this.txtCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCargo.Location = new System.Drawing.Point(556, 52);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(136, 21);
            this.txtCargo.TabIndex = 23;
            // 
            // txtRespEmissao
            // 
            this.txtRespEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRespEmissao.Location = new System.Drawing.Point(134, 52);
            this.txtRespEmissao.Name = "txtRespEmissao";
            this.txtRespEmissao.Size = new System.Drawing.Size(345, 21);
            this.txtRespEmissao.TabIndex = 22;
            // 
            // dtpDataEmissao
            // 
            this.dtpDataEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpDataEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataEmissao.Location = new System.Drawing.Point(802, 23);
            this.dtpDataEmissao.Name = "dtpDataEmissao";
            this.dtpDataEmissao.Size = new System.Drawing.Size(100, 21);
            this.dtpDataEmissao.TabIndex = 21;
            // 
            // txtCNPJCPFGerador
            // 
            this.txtCNPJCPFGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCNPJCPFGerador.Location = new System.Drawing.Point(556, 23);
            this.txtCNPJCPFGerador.Name = "txtCNPJCPFGerador";
            this.txtCNPJCPFGerador.Size = new System.Drawing.Size(136, 21);
            this.txtCNPJCPFGerador.TabIndex = 20;
            // 
            // txtNomeRazaoSocialGerador
            // 
            this.txtNomeRazaoSocialGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeRazaoSocialGerador.Location = new System.Drawing.Point(134, 23);
            this.txtNomeRazaoSocialGerador.Name = "txtNomeRazaoSocialGerador";
            this.txtNomeRazaoSocialGerador.Size = new System.Drawing.Size(345, 21);
            this.txtNomeRazaoSocialGerador.TabIndex = 19;
            // 
            // lblTCargo
            // 
            this.lblTCargo.AutoSize = true;
            this.lblTCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCargo.Location = new System.Drawing.Point(499, 52);
            this.lblTCargo.Name = "lblTCargo";
            this.lblTCargo.Size = new System.Drawing.Size(51, 15);
            this.lblTCargo.TabIndex = 18;
            this.lblTCargo.Text = "* Cargo:";
            // 
            // lblTRespEmissao
            // 
            this.lblTRespEmissao.AutoSize = true;
            this.lblTRespEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTRespEmissao.Location = new System.Drawing.Point(27, 52);
            this.lblTRespEmissao.Name = "lblTRespEmissao";
            this.lblTRespEmissao.Size = new System.Drawing.Size(101, 15);
            this.lblTRespEmissao.TabIndex = 17;
            this.lblTRespEmissao.Text = "* Resp. Emissão:";
            // 
            // lblTDataEmissao
            // 
            this.lblTDataEmissao.AutoSize = true;
            this.lblTDataEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTDataEmissao.Location = new System.Drawing.Point(708, 25);
            this.lblTDataEmissao.Name = "lblTDataEmissao";
            this.lblTDataEmissao.Size = new System.Drawing.Size(94, 15);
            this.lblTDataEmissao.TabIndex = 16;
            this.lblTDataEmissao.Text = "* Data emissão:";
            // 
            // lblTCNPJ_CPF_Gerador
            // 
            this.lblTCNPJ_CPF_Gerador.AutoSize = true;
            this.lblTCNPJ_CPF_Gerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCNPJ_CPF_Gerador.Location = new System.Drawing.Point(485, 25);
            this.lblTCNPJ_CPF_Gerador.Name = "lblTCNPJ_CPF_Gerador";
            this.lblTCNPJ_CPF_Gerador.Size = new System.Drawing.Size(67, 15);
            this.lblTCNPJ_CPF_Gerador.TabIndex = 15;
            this.lblTCNPJ_CPF_Gerador.Text = "CNPJ/CPF:";
            // 
            // lblTNomeRazaoSocialGerador
            // 
            this.lblTNomeRazaoSocialGerador.AutoSize = true;
            this.lblTNomeRazaoSocialGerador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTNomeRazaoSocialGerador.Location = new System.Drawing.Point(8, 25);
            this.lblTNomeRazaoSocialGerador.Name = "lblTNomeRazaoSocialGerador";
            this.lblTNomeRazaoSocialGerador.Size = new System.Drawing.Size(120, 15);
            this.lblTNomeRazaoSocialGerador.TabIndex = 14;
            this.lblTNomeRazaoSocialGerador.Text = "Nome/Razão Social:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboCodigoUnidadeTransportador);
            this.groupBox2.Controls.Add(this.txtPlacaVeiculo);
            this.groupBox2.Controls.Add(this.txtNomeMotorista);
            this.groupBox2.Controls.Add(this.dtpDataTransporte);
            this.groupBox2.Controls.Add(this.txtNomeRazaoSocialTransportador);
            this.groupBox2.Controls.Add(this.txtCNPJCPF_Transportador);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblTNomeMotorista);
            this.groupBox2.Controls.Add(this.lblTPlacaVeiculoTransportador);
            this.groupBox2.Controls.Add(this.lblTDataTransporte);
            this.groupBox2.Controls.Add(this.lblTNomeRazaoSocialTransportador);
            this.groupBox2.Controls.Add(this.lblTCNPJTransportador);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 385);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1099, 114);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Identificação do Transportador";
            // 
            // cboCodigoUnidadeTransportador
            // 
            this.cboCodigoUnidadeTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCodigoUnidadeTransportador.FormattingEnabled = true;
            this.cboCodigoUnidadeTransportador.Location = new System.Drawing.Point(115, 76);
            this.cboCodigoUnidadeTransportador.Name = "cboCodigoUnidadeTransportador";
            this.cboCodigoUnidadeTransportador.Size = new System.Drawing.Size(121, 21);
            this.cboCodigoUnidadeTransportador.TabIndex = 33;
            // 
            // txtPlacaVeiculo
            // 
            this.txtPlacaVeiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPlacaVeiculo.Location = new System.Drawing.Point(807, 46);
            this.txtPlacaVeiculo.Name = "txtPlacaVeiculo";
            this.txtPlacaVeiculo.Size = new System.Drawing.Size(136, 21);
            this.txtPlacaVeiculo.TabIndex = 32;
            // 
            // txtNomeMotorista
            // 
            this.txtNomeMotorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeMotorista.Location = new System.Drawing.Point(115, 49);
            this.txtNomeMotorista.Name = "txtNomeMotorista";
            this.txtNomeMotorista.Size = new System.Drawing.Size(345, 21);
            this.txtNomeMotorista.TabIndex = 31;
            // 
            // dtpDataTransporte
            // 
            this.dtpDataTransporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpDataTransporte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataTransporte.Location = new System.Drawing.Point(807, 20);
            this.dtpDataTransporte.Name = "dtpDataTransporte";
            this.dtpDataTransporte.Size = new System.Drawing.Size(100, 21);
            this.dtpDataTransporte.TabIndex = 30;
            // 
            // txtNomeRazaoSocialTransportador
            // 
            this.txtNomeRazaoSocialTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeRazaoSocialTransportador.Location = new System.Drawing.Point(346, 21);
            this.txtNomeRazaoSocialTransportador.Name = "txtNomeRazaoSocialTransportador";
            this.txtNomeRazaoSocialTransportador.Size = new System.Drawing.Size(345, 21);
            this.txtNomeRazaoSocialTransportador.TabIndex = 29;
            // 
            // txtCNPJCPF_Transportador
            // 
            this.txtCNPJCPF_Transportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCNPJCPF_Transportador.Location = new System.Drawing.Point(79, 22);
            this.txtCNPJCPF_Transportador.Name = "txtCNPJCPF_Transportador";
            this.txtCNPJCPF_Transportador.Size = new System.Drawing.Size(136, 21);
            this.txtCNPJCPF_Transportador.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "* Código Unidade:";
            // 
            // lblTNomeMotorista
            // 
            this.lblTNomeMotorista.AutoSize = true;
            this.lblTNomeMotorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTNomeMotorista.Location = new System.Drawing.Point(9, 50);
            this.lblTNomeMotorista.Name = "lblTNomeMotorista";
            this.lblTNomeMotorista.Size = new System.Drawing.Size(98, 15);
            this.lblTNomeMotorista.TabIndex = 23;
            this.lblTNomeMotorista.Text = "Nome Motorista:";
            // 
            // lblTPlacaVeiculoTransportador
            // 
            this.lblTPlacaVeiculoTransportador.AutoSize = true;
            this.lblTPlacaVeiculoTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTPlacaVeiculoTransportador.Location = new System.Drawing.Point(706, 46);
            this.lblTPlacaVeiculoTransportador.Name = "lblTPlacaVeiculoTransportador";
            this.lblTPlacaVeiculoTransportador.Size = new System.Drawing.Size(101, 15);
            this.lblTPlacaVeiculoTransportador.TabIndex = 22;
            this.lblTPlacaVeiculoTransportador.Text = "Placa do Veículo:";
            // 
            // lblTDataTransporte
            // 
            this.lblTDataTransporte.AutoSize = true;
            this.lblTDataTransporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTDataTransporte.Location = new System.Drawing.Point(709, 22);
            this.lblTDataTransporte.Name = "lblTDataTransporte";
            this.lblTDataTransporte.Size = new System.Drawing.Size(98, 15);
            this.lblTDataTransporte.TabIndex = 21;
            this.lblTDataTransporte.Text = "Data Transporte:";
            // 
            // lblTNomeRazaoSocialTransportador
            // 
            this.lblTNomeRazaoSocialTransportador.AutoSize = true;
            this.lblTNomeRazaoSocialTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTNomeRazaoSocialTransportador.Location = new System.Drawing.Point(224, 22);
            this.lblTNomeRazaoSocialTransportador.Name = "lblTNomeRazaoSocialTransportador";
            this.lblTNomeRazaoSocialTransportador.Size = new System.Drawing.Size(120, 15);
            this.lblTNomeRazaoSocialTransportador.TabIndex = 20;
            this.lblTNomeRazaoSocialTransportador.Text = "Nome/Razão Social:";
            // 
            // lblTCNPJTransportador
            // 
            this.lblTCNPJTransportador.AutoSize = true;
            this.lblTCNPJTransportador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCNPJTransportador.Location = new System.Drawing.Point(9, 22);
            this.lblTCNPJTransportador.Name = "lblTCNPJTransportador";
            this.lblTCNPJTransportador.Size = new System.Drawing.Size(67, 15);
            this.lblTCNPJTransportador.TabIndex = 19;
            this.lblTCNPJTransportador.Text = "CNPJ/CPF:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboCodigoUnidadeDestinador);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtNomeRazaoSocialDestinador);
            this.groupBox3.Controls.Add(this.txtCNPJCPFDestinador);
            this.groupBox3.Controls.Add(this.lblTNomeRazaoSocialDestinador);
            this.groupBox3.Controls.Add(this.lblTCNPJDestinador);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(11, 505);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1207, 69);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Identificação do Destinador";
            // 
            // cboCodigoUnidadeDestinador
            // 
            this.cboCodigoUnidadeDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCodigoUnidadeDestinador.FormattingEnabled = true;
            this.cboCodigoUnidadeDestinador.Location = new System.Drawing.Point(823, 22);
            this.cboCodigoUnidadeDestinador.Name = "cboCodigoUnidadeDestinador";
            this.cboCodigoUnidadeDestinador.Size = new System.Drawing.Size(352, 23);
            this.cboCodigoUnidadeDestinador.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(712, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "* Código Unidade:";
            // 
            // txtNomeRazaoSocialDestinador
            // 
            this.txtNomeRazaoSocialDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeRazaoSocialDestinador.Location = new System.Drawing.Point(361, 25);
            this.txtNomeRazaoSocialDestinador.Name = "txtNomeRazaoSocialDestinador";
            this.txtNomeRazaoSocialDestinador.Size = new System.Drawing.Size(345, 21);
            this.txtNomeRazaoSocialDestinador.TabIndex = 30;
            // 
            // txtCNPJCPFDestinador
            // 
            this.txtCNPJCPFDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCNPJCPFDestinador.Location = new System.Drawing.Point(80, 25);
            this.txtCNPJCPFDestinador.Name = "txtCNPJCPFDestinador";
            this.txtCNPJCPFDestinador.Size = new System.Drawing.Size(136, 21);
            this.txtCNPJCPFDestinador.TabIndex = 23;
            // 
            // lblTNomeRazaoSocialDestinador
            // 
            this.lblTNomeRazaoSocialDestinador.AutoSize = true;
            this.lblTNomeRazaoSocialDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTNomeRazaoSocialDestinador.Location = new System.Drawing.Point(238, 26);
            this.lblTNomeRazaoSocialDestinador.Name = "lblTNomeRazaoSocialDestinador";
            this.lblTNomeRazaoSocialDestinador.Size = new System.Drawing.Size(120, 15);
            this.lblTNomeRazaoSocialDestinador.TabIndex = 22;
            this.lblTNomeRazaoSocialDestinador.Text = "Nome/Razão Social:";
            // 
            // lblTCNPJDestinador
            // 
            this.lblTCNPJDestinador.AutoSize = true;
            this.lblTCNPJDestinador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCNPJDestinador.Location = new System.Drawing.Point(9, 26);
            this.lblTCNPJDestinador.Name = "lblTCNPJDestinador";
            this.lblTCNPJDestinador.Size = new System.Drawing.Size(67, 15);
            this.lblTCNPJDestinador.TabIndex = 21;
            this.lblTCNPJDestinador.Text = "CNPJ/CPF:";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Location = new System.Drawing.Point(101, 580);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Size = new System.Drawing.Size(749, 73);
            this.txtObservacoes.TabIndex = 25;
            // 
            // frmMTReLancar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1481, 752);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.lblTObservacoes);
            this.Controls.Add(this.lblIdentifResiduos);
            this.Controls.Add(this.grvResiduos);
            this.MaximizeBox = false;
            this.Name = "frmMTReLancar";
            this.Text = "MTR - Manifesto de Transporte de Resíduos ";
            this.Load += new System.EventHandler(this.frmMTReLancar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvResiduos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grvResiduos;
        private System.Windows.Forms.Label lblIdentifResiduos;
        private System.Windows.Forms.Label lblTObservacoes;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTCargo;
        private System.Windows.Forms.Label lblTRespEmissao;
        private System.Windows.Forms.Label lblTDataEmissao;
        private System.Windows.Forms.Label lblTCNPJ_CPF_Gerador;
        private System.Windows.Forms.Label lblTNomeRazaoSocialGerador;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTNomeMotorista;
        private System.Windows.Forms.Label lblTPlacaVeiculoTransportador;
        private System.Windows.Forms.Label lblTDataTransporte;
        private System.Windows.Forms.Label lblTNomeRazaoSocialTransportador;
        private System.Windows.Forms.Label lblTCNPJTransportador;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTNomeRazaoSocialDestinador;
        private System.Windows.Forms.Label lblTCNPJDestinador;
        private System.Windows.Forms.TextBox txtObservacoes;
        private System.Windows.Forms.TextBox txtCNPJCPFGerador;
        private System.Windows.Forms.TextBox txtNomeRazaoSocialGerador;
        private System.Windows.Forms.DateTimePicker dtpDataEmissao;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.TextBox txtRespEmissao;
        private System.Windows.Forms.Label lblTCodigoUnidadeGerador;
        private System.Windows.Forms.TextBox txtNomeRazaoSocialTransportador;
        private System.Windows.Forms.TextBox txtCNPJCPF_Transportador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDataTransporte;
        private System.Windows.Forms.TextBox txtNomeMotorista;
        private System.Windows.Forms.TextBox txtPlacaVeiculo;
        private System.Windows.Forms.TextBox txtCNPJCPFDestinador;
        private System.Windows.Forms.TextBox txtNomeRazaoSocialDestinador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoUnidadeGerador;
        private System.Windows.Forms.ComboBox cboCodigoUnidadeTransportador;
        private System.Windows.Forms.ComboBox cboCodigoUnidadeDestinador;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoResiduo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIBAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoIBAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoFisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acondicionamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tecnologia;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroONU;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClasseRisco;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeEmbarque;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrupoEmbalagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEstadoFisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoClasse;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoAcondicionamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoUnidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoTecnologia;
    }
}