namespace formSILC
{
    partial class frmLocacaoMTR
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
            this.cboDestino = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblObs = new System.Windows.Forms.Label();
            this.lblNrLancamento = new System.Windows.Forms.Label();
            this.lblSequencial = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUnidade = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDataDescarga = new System.Windows.Forms.DateTimePicker();
            this.txtHoraDescarga = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMotivoMTRe = new System.Windows.Forms.TextBox();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.intNumeroMTRe = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDescargaMTRe = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtControleInternoDescarga = new System.Windows.Forms.TextBox();
            this.moeValorTotal = new formSILC.MOEDA();
            this.moeValorUnitario = new formSILC.MOEDA();
            this.residuo1 = new formSILC.RESIDUO();
            this.moeQuantidadeDescarregada = new formSILC.MOEDA();
            this.moeQuantidadeColetada = new formSILC.MOEDA();
            this.intNumeroMTR = new formSILC.INTEIRO();
            this.SuspendLayout();
            // 
            // cboDestino
            // 
            this.cboDestino.FormattingEnabled = true;
            this.cboDestino.Location = new System.Drawing.Point(120, 213);
            this.cboDestino.Name = "cboDestino";
            this.cboDestino.Size = new System.Drawing.Size(191, 21);
            this.cboDestino.TabIndex = 8;
            this.cboDestino.Leave += new System.EventHandler(this.cboDestino_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nº MTR:";
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(22, 192);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(68, 13);
            this.lblObs.TabIndex = 0;
            this.lblObs.Text = "Observação:";
            // 
            // lblNrLancamento
            // 
            this.lblNrLancamento.AutoSize = true;
            this.lblNrLancamento.Location = new System.Drawing.Point(119, 9);
            this.lblNrLancamento.Name = "lblNrLancamento";
            this.lblNrLancamento.Size = new System.Drawing.Size(49, 13);
            this.lblNrLancamento.TabIndex = 0;
            this.lblNrLancamento.Text = "0000000";
            // 
            // lblSequencial
            // 
            this.lblSequencial.AutoSize = true;
            this.lblSequencial.Location = new System.Drawing.Point(21, 9);
            this.lblSequencial.Name = "lblSequencial";
            this.lblSequencial.Size = new System.Drawing.Size(84, 13);
            this.lblSequencial.TabIndex = 0;
            this.lblSequencial.Text = "Nº Lançamento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Data Descarga:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Destino:";
            // 
            // txtObservacao
            // 
            this.txtObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacao.Location = new System.Drawing.Point(120, 189);
            this.txtObservacao.MaxLength = 50;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(368, 20);
            this.txtObservacao.TabIndex = 7;
            this.txtObservacao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtObservacao_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Quantidade Coletada:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Unidade:";
            // 
            // txtUnidade
            // 
            this.txtUnidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnidade.Location = new System.Drawing.Point(71, 75);
            this.txtUnidade.Name = "txtUnidade";
            this.txtUnidade.Size = new System.Drawing.Size(64, 20);
            this.txtUnidade.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(399, 395);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Quantidade Descarregada";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Valor unitário:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Valor total:";
            // 
            // dtpDataDescarga
            // 
            this.dtpDataDescarga.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataDescarga.Location = new System.Drawing.Point(120, 239);
            this.dtpDataDescarga.Name = "dtpDataDescarga";
            this.dtpDataDescarga.Size = new System.Drawing.Size(100, 20);
            this.dtpDataDescarga.TabIndex = 9;
            // 
            // txtHoraDescarga
            // 
            this.txtHoraDescarga.Location = new System.Drawing.Point(122, 266);
            this.txtHoraDescarga.MaxLength = 8;
            this.txtHoraDescarga.Name = "txtHoraDescarga";
            this.txtHoraDescarga.Size = new System.Drawing.Size(64, 20);
            this.txtHoraDescarga.TabIndex = 10;
            this.txtHoraDescarga.Leave += new System.EventHandler(this.txtHoraDescarga_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Hora descarga:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Ticket:";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(122, 292);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(99, 20);
            this.txtTicket.TabIndex = 11;
            this.txtTicket.Leave += new System.EventHandler(this.txtTicket_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 319);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Nº MTR eletrônica:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(228, 246);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Motivo MTR eletrônica:";
            this.label12.Visible = false;
            // 
            // txtMotivoMTRe
            // 
            this.txtMotivoMTRe.Location = new System.Drawing.Point(229, 260);
            this.txtMotivoMTRe.MaxLength = 50;
            this.txtMotivoMTRe.Name = "txtMotivoMTRe";
            this.txtMotivoMTRe.Size = new System.Drawing.Size(261, 20);
            this.txtMotivoMTRe.TabIndex = 13;
            this.txtMotivoMTRe.Visible = false;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // intNumeroMTRe
            // 
            this.intNumeroMTRe.Location = new System.Drawing.Point(123, 317);
            this.intNumeroMTRe.Name = "intNumeroMTRe";
            this.intNumeroMTRe.Size = new System.Drawing.Size(125, 20);
            this.intNumeroMTRe.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 345);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Destino MTR-e:";
            // 
            // txtDescargaMTRe
            // 
            this.txtDescargaMTRe.Location = new System.Drawing.Point(117, 342);
            this.txtDescargaMTRe.MaxLength = 30;
            this.txtDescargaMTRe.Name = "txtDescargaMTRe";
            this.txtDescargaMTRe.Size = new System.Drawing.Size(242, 20);
            this.txtDescargaMTRe.TabIndex = 13;
            this.txtDescargaMTRe.Leave += new System.EventHandler(this.txtDescargaMTRe_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 369);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Cont.Int.Descarga:";
            // 
            // txtControleInternoDescarga
            // 
            this.txtControleInternoDescarga.Location = new System.Drawing.Point(117, 369);
            this.txtControleInternoDescarga.MaxLength = 30;
            this.txtControleInternoDescarga.Name = "txtControleInternoDescarga";
            this.txtControleInternoDescarga.Size = new System.Drawing.Size(242, 20);
            this.txtControleInternoDescarga.TabIndex = 14;
            this.txtControleInternoDescarga.Leave += new System.EventHandler(this.txtControleInternoDescarga_Leave);
            // 
            // moeValorTotal
            // 
            this.moeValorTotal.Location = new System.Drawing.Point(95, 165);
            this.moeValorTotal.Margin = new System.Windows.Forms.Padding(4);
            this.moeValorTotal.Name = "moeValorTotal";
            this.moeValorTotal.Size = new System.Drawing.Size(89, 21);
            this.moeValorTotal.TabIndex = 6;
            // 
            // moeValorUnitario
            // 
            this.moeValorUnitario.Location = new System.Drawing.Point(95, 141);
            this.moeValorUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.moeValorUnitario.Name = "moeValorUnitario";
            this.moeValorUnitario.Size = new System.Drawing.Size(89, 21);
            this.moeValorUnitario.TabIndex = 5;
            this.moeValorUnitario.Leave += new System.EventHandler(this.moeValorUnitario_Leave);
            // 
            // residuo1
            // 
            this.residuo1.Location = new System.Drawing.Point(18, 51);
            this.residuo1.Margin = new System.Windows.Forms.Padding(4);
            this.residuo1.Name = "residuo1";
            this.residuo1.Size = new System.Drawing.Size(493, 22);
            this.residuo1.TabIndex = 1;
            this.residuo1.TabIndexCodigo = 1;
            this.residuo1.Unidade = null;
            this.residuo1.Enter += new System.EventHandler(this.residuo1_Enter);
            this.residuo1.Leave += new System.EventHandler(this.residuo1_Leave);
            // 
            // moeQuantidadeDescarregada
            // 
            this.moeQuantidadeDescarregada.Location = new System.Drawing.Point(159, 119);
            this.moeQuantidadeDescarregada.Margin = new System.Windows.Forms.Padding(4);
            this.moeQuantidadeDescarregada.Name = "moeQuantidadeDescarregada";
            this.moeQuantidadeDescarregada.Size = new System.Drawing.Size(89, 21);
            this.moeQuantidadeDescarregada.TabIndex = 4;
            this.moeQuantidadeDescarregada.Leave += new System.EventHandler(this.moeQuantidadeDescarregada_Leave);
            // 
            // moeQuantidadeColetada
            // 
            this.moeQuantidadeColetada.Location = new System.Drawing.Point(159, 96);
            this.moeQuantidadeColetada.Margin = new System.Windows.Forms.Padding(4);
            this.moeQuantidadeColetada.Name = "moeQuantidadeColetada";
            this.moeQuantidadeColetada.Size = new System.Drawing.Size(89, 21);
            this.moeQuantidadeColetada.TabIndex = 3;
            // 
            // intNumeroMTR
            // 
            this.intNumeroMTR.Location = new System.Drawing.Point(71, 26);
            this.intNumeroMTR.Margin = new System.Windows.Forms.Padding(4);
            this.intNumeroMTR.Name = "intNumeroMTR";
            this.intNumeroMTR.Size = new System.Drawing.Size(97, 21);
            this.intNumeroMTR.TabIndex = 0;
            // 
            // frmLocacaoMTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 429);
            this.Controls.Add(this.txtControleInternoDescarga);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtDescargaMTRe);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.intNumeroMTRe);
            this.Controls.Add(this.moeValorTotal);
            this.Controls.Add(this.moeValorUnitario);
            this.Controls.Add(this.residuo1);
            this.Controls.Add(this.moeQuantidadeDescarregada);
            this.Controls.Add(this.moeQuantidadeColetada);
            this.Controls.Add(this.intNumeroMTR);
            this.Controls.Add(this.txtMotivoMTRe);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtHoraDescarga);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtUnidade);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDataDescarga);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboDestino);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrLancamento);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmLocacaoMTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inserção/Alteração de MTR na Locação";
            this.Load += new System.EventHandler(this.frmLocacaoMTR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cboDestino;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Label lblNrLancamento;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtObservacao;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtUnidade;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker dtpDataDescarga;
        public System.Windows.Forms.TextBox txtHoraDescarga;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTicket;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtMotivoMTRe;
        public INTEIRO intNumeroMTR;
        public MOEDA moeQuantidadeColetada;
        public MOEDA moeQuantidadeDescarregada;
        public RESIDUO residuo1;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        public MOEDA moeValorUnitario;
        public MOEDA moeValorTotal;
        public System.Windows.Forms.TextBox intNumeroMTRe;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtDescargaMTRe;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txtControleInternoDescarga;
    }
}