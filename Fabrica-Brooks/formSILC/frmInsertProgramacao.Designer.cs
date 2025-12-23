namespace formSILC
{
    partial class frmInsertProgramacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            this.txtHora = new System.Windows.Forms.TextBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.cboTabMotivosOBS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblObs = new System.Windows.Forms.Label();
            this.lblNrSequencial = new System.Windows.Forms.Label();
            this.lblSequencial = new System.Windows.Forms.Label();
            this.lblSolicitante = new System.Windows.Forms.Label();
            this.txtSolicitante = new System.Windows.Forms.TextBox();
            this.txtResiduo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDataProgramada = new System.Windows.Forms.DateTimePicker();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServicoAExecutar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUnidade = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpProgramacaoAberta = new System.Windows.Forms.DateTimePicker();
            this.cboDestinoFinal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDataRetirar = new System.Windows.Forms.DateTimePicker();
            this.btnInserirRetirar = new System.Windows.Forms.Button();
            this.lblDataRetirar = new System.Windows.Forms.Label();
            this.intQuantidade = new formSILC.INTEIRO();
            this.caminhao1 = new formSILC.CAMINHAO();
            this.funcionario1 = new formSILC.FUNCIONARIO();
            this.residuo1 = new formSILC.RESIDUO();
            this.cliente1 = new formSILC.CLIENTE();
            this.SuspendLayout();
            // 
            // txtHora
            // 
            this.txtHora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHora.Enabled = false;
            this.txtHora.Location = new System.Drawing.Point(132, 51);
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(64, 20);
            this.txtHora.TabIndex = 0;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(21, 51);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(33, 13);
            this.lblHora.TabIndex = 0;
            this.lblHora.Text = "Hora:";
            // 
            // cboTabMotivosOBS
            // 
            this.cboTabMotivosOBS.FormattingEnabled = true;
            this.cboTabMotivosOBS.Location = new System.Drawing.Point(132, 266);
            this.cboTabMotivosOBS.Name = "cboTabMotivosOBS";
            this.cboTabMotivosOBS.Size = new System.Drawing.Size(356, 21);
            this.cboTabMotivosOBS.TabIndex = 8;
            this.cboTabMotivosOBS.Leave += new System.EventHandler(this.cboTabMotivosOBS_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Data:";
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(21, 269);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(68, 13);
            this.lblObs.TabIndex = 0;
            this.lblObs.Text = "Observação:";
            // 
            // lblNrSequencial
            // 
            this.lblNrSequencial.AutoSize = true;
            this.lblNrSequencial.Location = new System.Drawing.Point(128, 9);
            this.lblNrSequencial.Name = "lblNrSequencial";
            this.lblNrSequencial.Size = new System.Drawing.Size(49, 13);
            this.lblNrSequencial.TabIndex = 0;
            this.lblNrSequencial.Text = "0000000";
            // 
            // lblSequencial
            // 
            this.lblSequencial.AutoSize = true;
            this.lblSequencial.Location = new System.Drawing.Point(21, 9);
            this.lblSequencial.Name = "lblSequencial";
            this.lblSequencial.Size = new System.Drawing.Size(63, 13);
            this.lblSequencial.TabIndex = 0;
            this.lblSequencial.Text = "Sequencial:";
            // 
            // lblSolicitante
            // 
            this.lblSolicitante.AutoSize = true;
            this.lblSolicitante.Location = new System.Drawing.Point(21, 73);
            this.lblSolicitante.Name = "lblSolicitante";
            this.lblSolicitante.Size = new System.Drawing.Size(59, 13);
            this.lblSolicitante.TabIndex = 0;
            this.lblSolicitante.Text = "Solicitante:";
            // 
            // txtSolicitante
            // 
            this.txtSolicitante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSolicitante.Location = new System.Drawing.Point(132, 73);
            this.txtSolicitante.MaxLength = 20;
            this.txtSolicitante.Name = "txtSolicitante";
            this.txtSolicitante.Size = new System.Drawing.Size(185, 20);
            this.txtSolicitante.TabIndex = 1;
            // 
            // txtResiduo
            // 
            this.txtResiduo.Location = new System.Drawing.Point(132, 118);
            this.txtResiduo.MaxLength = 50;
            this.txtResiduo.Name = "txtResiduo";
            this.txtResiduo.Size = new System.Drawing.Size(356, 20);
            this.txtResiduo.TabIndex = 3;
            this.txtResiduo.Text = "F2 - Resíduos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Data Programada:";
            // 
            // dtpDataProgramada
            // 
            this.dtpDataProgramada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataProgramada.Location = new System.Drawing.Point(132, 144);
            this.dtpDataProgramada.Name = "dtpDataProgramada";
            this.dtpDataProgramada.Size = new System.Drawing.Size(100, 20);
            this.dtpDataProgramada.TabIndex = 4;
            this.dtpDataProgramada.Leave += new System.EventHandler(this.dtpDataProgramada_Leave);
            // 
            // dtpData
            // 
            this.dtpData.Enabled = false;
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(132, 25);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(100, 20);
            this.dtpData.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Serviço a Executar:";
            // 
            // txtServicoAExecutar
            // 
            this.txtServicoAExecutar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtServicoAExecutar.Location = new System.Drawing.Point(132, 167);
            this.txtServicoAExecutar.MaxLength = 50;
            this.txtServicoAExecutar.Name = "txtServicoAExecutar";
            this.txtServicoAExecutar.Size = new System.Drawing.Size(356, 20);
            this.txtServicoAExecutar.TabIndex = 5;
            this.txtServicoAExecutar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServicoAExecutar_KeyPress);
            this.txtServicoAExecutar.Leave += new System.EventHandler(this.txtServicoAExecutar_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Quantidade:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Unidade:";
            // 
            // txtUnidade
            // 
            this.txtUnidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnidade.Enabled = false;
            this.txtUnidade.Location = new System.Drawing.Point(132, 323);
            this.txtUnidade.Name = "txtUnidade";
            this.txtUnidade.Size = new System.Drawing.Size(64, 20);
            this.txtUnidade.TabIndex = 9;
            this.txtUnidade.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(413, 330);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(33, 363);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.Visible = false;
            // 
            // dtpProgramacaoAberta
            // 
            this.dtpProgramacaoAberta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProgramacaoAberta.Location = new System.Drawing.Point(92, 361);
            this.dtpProgramacaoAberta.Name = "dtpProgramacaoAberta";
            this.dtpProgramacaoAberta.Size = new System.Drawing.Size(100, 20);
            this.dtpProgramacaoAberta.TabIndex = 13;
            this.dtpProgramacaoAberta.TabStop = false;
            this.dtpProgramacaoAberta.Visible = false;
            // 
            // cboDestinoFinal
            // 
            this.cboDestinoFinal.FormattingEnabled = true;
            this.cboDestinoFinal.Location = new System.Drawing.Point(132, 295);
            this.cboDestinoFinal.Name = "cboDestinoFinal";
            this.cboDestinoFinal.Size = new System.Drawing.Size(356, 21);
            this.cboDestinoFinal.TabIndex = 9;
            this.cboDestinoFinal.Leave += new System.EventHandler(this.cboDestinoFinal_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Destino final:";
            // 
            // dtpDataRetirar
            // 
            this.dtpDataRetirar.Enabled = false;
            this.dtpDataRetirar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataRetirar.Location = new System.Drawing.Point(381, 190);
            this.dtpDataRetirar.Name = "dtpDataRetirar";
            this.dtpDataRetirar.Size = new System.Drawing.Size(107, 20);
            this.dtpDataRetirar.TabIndex = 7;
            this.dtpDataRetirar.ValueChanged += new System.EventHandler(this.dtpDataRetirar_ValueChanged);
            // 
            // btnInserirRetirar
            // 
            this.btnInserirRetirar.Location = new System.Drawing.Point(309, 330);
            this.btnInserirRetirar.Name = "btnInserirRetirar";
            this.btnInserirRetirar.Size = new System.Drawing.Size(98, 28);
            this.btnInserirRetirar.TabIndex = 11;
            this.btnInserirRetirar.Text = "Inserir RETIRAR";
            this.btnInserirRetirar.UseVisualStyleBackColor = true;
            this.btnInserirRetirar.Click += new System.EventHandler(this.btnInserirRetirar_Click);
            // 
            // lblDataRetirar
            // 
            this.lblDataRetirar.AutoSize = true;
            this.lblDataRetirar.Location = new System.Drawing.Point(287, 193);
            this.lblDataRetirar.Name = "lblDataRetirar";
            this.lblDataRetirar.Size = new System.Drawing.Size(88, 13);
            this.lblDataRetirar.TabIndex = 16;
            this.lblDataRetirar.Text = "Data para Retirar";
            // 
            // intQuantidade
            // 
            this.intQuantidade.Enabled = false;
            this.intQuantidade.Location = new System.Drawing.Point(88, 189);
            this.intQuantidade.Name = "intQuantidade";
            this.intQuantidade.Size = new System.Drawing.Size(97, 21);
            this.intQuantidade.TabIndex = 6;
            // 
            // caminhao1
            // 
            this.caminhao1.Location = new System.Drawing.Point(18, 212);
            this.caminhao1.Name = "caminhao1";
            this.caminhao1.Size = new System.Drawing.Size(493, 22);
            this.caminhao1.TabIndex = 7;
            this.caminhao1.TabIndexCodigo = 7;
            this.caminhao1.Enter += new System.EventHandler(this.caminhao1_Enter);
            // 
            // funcionario1
            // 
            this.funcionario1.Location = new System.Drawing.Point(18, 240);
            this.funcionario1.Name = "funcionario1";
            this.funcionario1.Size = new System.Drawing.Size(493, 22);
            this.funcionario1.TabIndex = 8;
            this.funcionario1.TabIndexCodigo = 8;
            this.funcionario1.Enter += new System.EventHandler(this.funcionario1_Enter);
            // 
            // residuo1
            // 
            this.residuo1.Location = new System.Drawing.Point(18, 116);
            this.residuo1.Name = "residuo1";
            this.residuo1.Size = new System.Drawing.Size(470, 22);
            this.residuo1.TabIndex = 3;
            this.residuo1.TabIndexCodigo = 3;
            this.residuo1.Unidade = null;
            this.residuo1.Enter += new System.EventHandler(this.residuo1_Enter);
            this.residuo1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.residuo1_KeyUp);
            this.residuo1.Leave += new System.EventHandler(this.residuo1_Leave);
            // 
            // cliente1
            // 
            this.cliente1.Location = new System.Drawing.Point(18, 93);
            this.cliente1.Name = "cliente1";
            this.cliente1.Size = new System.Drawing.Size(470, 22);
            this.cliente1.TabIndex = 2;
            this.cliente1.TabIndexCodigo = 2;
            this.cliente1.Enter += new System.EventHandler(this.cliente1_Enter);
            this.cliente1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cliente1_KeyUp);
            this.cliente1.Leave += new System.EventHandler(this.cliente1_Leave);
            // 
            // frmInsertProgramacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 382);
            this.Controls.Add(this.dtpDataRetirar);
            this.Controls.Add(this.btnInserirRetirar);
            this.Controls.Add(this.lblDataRetirar);
            this.Controls.Add(this.intQuantidade);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDestinoFinal);
            this.Controls.Add(this.dtpProgramacaoAberta);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.caminhao1);
            this.Controls.Add(this.funcionario1);
            this.Controls.Add(this.residuo1);
            this.Controls.Add(this.cliente1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtUnidade);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtServicoAExecutar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.dtpDataProgramada);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtResiduo);
            this.Controls.Add(this.txtSolicitante);
            this.Controls.Add(this.lblSolicitante);
            this.Controls.Add(this.txtHora);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.cboTabMotivosOBS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrSequencial);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmInsertProgramacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inserção de programação";
            this.Load += new System.EventHandler(this.frmInsertProgramacao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtHora;
        public System.Windows.Forms.Label lblHora;
        public System.Windows.Forms.ComboBox cboTabMotivosOBS;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Label lblNrSequencial;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.Label lblSolicitante;
        public System.Windows.Forms.TextBox txtSolicitante;
        public System.Windows.Forms.TextBox txtResiduo;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.DateTimePicker dtpDataProgramada;
        public System.Windows.Forms.DateTimePicker dtpData;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtServicoAExecutar;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtUnidade;
        public System.Windows.Forms.Button btnOk;
        public CLIENTE cliente1;
        public RESIDUO residuo1;
        public FUNCIONARIO funcionario1;
        public CAMINHAO caminhao1;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.DateTimePicker dtpProgramacaoAberta;
        public System.Windows.Forms.ComboBox cboDestinoFinal;
        public System.Windows.Forms.Label label1;
        public INTEIRO intQuantidade;
        private System.Windows.Forms.DateTimePicker dtpDataRetirar;
        private System.Windows.Forms.Button btnInserirRetirar;
        private System.Windows.Forms.Label lblDataRetirar;
    }
}