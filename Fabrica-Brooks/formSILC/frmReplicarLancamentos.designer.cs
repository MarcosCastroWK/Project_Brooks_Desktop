namespace formSILC
{
    partial class frmReplicarLancamentos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.CodigoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Residuo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataDescarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroMTRFatima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroMTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Franquia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoAterroSanitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deposito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroCaixa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCaminhaoColoca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoMotoristaColocou = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataColocacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataRetirada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoMotoristaRetirou = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCaminhoRetirada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.lblLinhas = new System.Windows.Forms.Label();
            this.lblReplicarLancAtual = new System.Windows.Forms.Label();
            this.btnReplicar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.in2ReplicarVezes = new formSILC.INTEIRO2();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // Grade
            // 
            this.Grade.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoCliente,
            this.NomeFantasia,
            this.Residuo,
            this.DataDescarga,
            this.NumeroMTRFatima,
            this.NumeroMTR,
            this.Motivo,
            this.NumeroLancamento,
            this.Franquia,
            this.CodigoAterroSanitario,
            this.Deposito,
            this.observacao,
            this.Quantidade,
            this.Ticket,
            this.Unidade,
            this.ValorTotal,
            this.ValorUnitario,
            this.NumeroCaixa,
            this.CodigoCaminhaoColoca,
            this.CodigoMotoristaColocou,
            this.DataColocacao,
            this.DataRetirada,
            this.CodigoMotoristaRetirou,
            this.CodigoCaminhoRetirada});
            this.Grade.Location = new System.Drawing.Point(7, 12);
            this.Grade.Name = "Grade";
            this.Grade.RowHeadersWidth = 20;
            this.Grade.Size = new System.Drawing.Size(862, 351);
            this.Grade.TabIndex = 36;
            this.Grade.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grade_CellBeginEdit);
            this.Grade.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellContentClick);
            this.Grade.CurrentCellDirtyStateChanged += new System.EventHandler(this.Grade_CurrentCellDirtyStateChanged);
            // 
            // CodigoCliente
            // 
            this.CodigoCliente.DataPropertyName = "CodigoCliente";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CodigoCliente.DefaultCellStyle = dataGridViewCellStyle1;
            this.CodigoCliente.HeaderText = "Cliente";
            this.CodigoCliente.Name = "CodigoCliente";
            this.CodigoCliente.ReadOnly = true;
            this.CodigoCliente.Width = 70;
            // 
            // NomeFantasia
            // 
            this.NomeFantasia.DataPropertyName = "NomeFantasia";
            this.NomeFantasia.HeaderText = "NomeFantasia";
            this.NomeFantasia.Name = "NomeFantasia";
            this.NomeFantasia.ReadOnly = true;
            this.NomeFantasia.Width = 200;
            // 
            // Residuo
            // 
            this.Residuo.DataPropertyName = "Residuo";
            this.Residuo.HeaderText = "Resíduo";
            this.Residuo.Name = "Residuo";
            this.Residuo.ReadOnly = true;
            this.Residuo.Width = 150;
            // 
            // DataDescarga
            // 
            this.DataDescarga.DataPropertyName = "DataDescarga";
            this.DataDescarga.HeaderText = "Data";
            this.DataDescarga.Name = "DataDescarga";
            this.DataDescarga.ReadOnly = true;
            // 
            // NumeroMTRFatima
            // 
            this.NumeroMTRFatima.DataPropertyName = "NumeroMTRFatima";
            this.NumeroMTRFatima.HeaderText = "MTR-e";
            this.NumeroMTRFatima.Name = "NumeroMTRFatima";
            // 
            // NumeroMTR
            // 
            this.NumeroMTR.DataPropertyName = "NumeroMTR";
            this.NumeroMTR.HeaderText = "MTR";
            this.NumeroMTR.Name = "NumeroMTR";
            // 
            // Motivo
            // 
            this.Motivo.DataPropertyName = "Motivo";
            this.Motivo.HeaderText = "Motivo";
            this.Motivo.Name = "Motivo";
            // 
            // NumeroLancamento
            // 
            this.NumeroLancamento.DataPropertyName = "NumeroLancamento";
            this.NumeroLancamento.HeaderText = "NumeroLancamento";
            this.NumeroLancamento.Name = "NumeroLancamento";
            this.NumeroLancamento.Visible = false;
            // 
            // Franquia
            // 
            this.Franquia.DataPropertyName = "Franquia";
            this.Franquia.HeaderText = "Franquia";
            this.Franquia.Name = "Franquia";
            this.Franquia.Visible = false;
            // 
            // CodigoAterroSanitario
            // 
            this.CodigoAterroSanitario.DataPropertyName = "CodigoAterroSanitario";
            this.CodigoAterroSanitario.HeaderText = "CodigoAterroSanitario";
            this.CodigoAterroSanitario.Name = "CodigoAterroSanitario";
            this.CodigoAterroSanitario.Visible = false;
            // 
            // Deposito
            // 
            this.Deposito.DataPropertyName = "Deposito";
            this.Deposito.HeaderText = "Deposito";
            this.Deposito.Name = "Deposito";
            this.Deposito.Visible = false;
            // 
            // observacao
            // 
            this.observacao.DataPropertyName = "observacao";
            this.observacao.HeaderText = "observacao";
            this.observacao.Name = "observacao";
            this.observacao.Visible = false;
            // 
            // Quantidade
            // 
            this.Quantidade.DataPropertyName = "Quantidade";
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.Visible = false;
            // 
            // Ticket
            // 
            this.Ticket.DataPropertyName = "Ticket";
            this.Ticket.HeaderText = "Ticket";
            this.Ticket.Name = "Ticket";
            this.Ticket.Visible = false;
            // 
            // Unidade
            // 
            this.Unidade.DataPropertyName = "Unidade";
            this.Unidade.HeaderText = "Unidade";
            this.Unidade.Name = "Unidade";
            this.Unidade.Visible = false;
            // 
            // ValorTotal
            // 
            this.ValorTotal.DataPropertyName = "ValorTotal";
            this.ValorTotal.HeaderText = "ValorTotal";
            this.ValorTotal.Name = "ValorTotal";
            this.ValorTotal.Visible = false;
            // 
            // ValorUnitario
            // 
            this.ValorUnitario.DataPropertyName = "ValorUnitario";
            this.ValorUnitario.HeaderText = "ValorUnitario";
            this.ValorUnitario.Name = "ValorUnitario";
            this.ValorUnitario.Visible = false;
            // 
            // NumeroCaixa
            // 
            this.NumeroCaixa.DataPropertyName = "NumeroCaixa";
            this.NumeroCaixa.HeaderText = "NumeroCaixa";
            this.NumeroCaixa.Name = "NumeroCaixa";
            this.NumeroCaixa.Visible = false;
            // 
            // CodigoCaminhaoColoca
            // 
            this.CodigoCaminhaoColoca.DataPropertyName = "CodigoCaminhaoColoca";
            this.CodigoCaminhaoColoca.HeaderText = "CodigoCaminhaoColoca";
            this.CodigoCaminhaoColoca.Name = "CodigoCaminhaoColoca";
            this.CodigoCaminhaoColoca.Visible = false;
            // 
            // CodigoMotoristaColocou
            // 
            this.CodigoMotoristaColocou.DataPropertyName = "CodigoMotoristaColocou";
            this.CodigoMotoristaColocou.HeaderText = "CodigoMotoristaColocou";
            this.CodigoMotoristaColocou.Name = "CodigoMotoristaColocou";
            this.CodigoMotoristaColocou.Visible = false;
            // 
            // DataColocacao
            // 
            this.DataColocacao.DataPropertyName = "DataColocacao";
            this.DataColocacao.HeaderText = "DataColocacao";
            this.DataColocacao.Name = "DataColocacao";
            this.DataColocacao.Visible = false;
            // 
            // DataRetirada
            // 
            this.DataRetirada.DataPropertyName = "DataRetirada";
            this.DataRetirada.HeaderText = "DataRetirada";
            this.DataRetirada.Name = "DataRetirada";
            this.DataRetirada.Visible = false;
            // 
            // CodigoMotoristaRetirou
            // 
            this.CodigoMotoristaRetirou.DataPropertyName = "CodigoMotoristaRetirou";
            this.CodigoMotoristaRetirou.HeaderText = "CodigoMotoristaRetirou";
            this.CodigoMotoristaRetirou.Name = "CodigoMotoristaRetirou";
            this.CodigoMotoristaRetirou.Visible = false;
            // 
            // CodigoCaminhoRetirada
            // 
            this.CodigoCaminhoRetirada.DataPropertyName = "CodigoCaminhoRetirada";
            this.CodigoCaminhoRetirada.HeaderText = "CodigoCaminhoRetirada";
            this.CodigoCaminhoRetirada.Name = "CodigoCaminhoRetirada";
            this.CodigoCaminhoRetirada.Visible = false;
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(12, 410);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 35;
            this.lblMensagem.Text = "Mensagem";
            // 
            // lblLinhas
            // 
            this.lblLinhas.AutoSize = true;
            this.lblLinhas.Location = new System.Drawing.Point(12, 380);
            this.lblLinhas.Name = "lblLinhas";
            this.lblLinhas.Size = new System.Drawing.Size(64, 13);
            this.lblLinhas.TabIndex = 50;
            this.lblLinhas.Text = "linhas grade";
            // 
            // lblReplicarLancAtual
            // 
            this.lblReplicarLancAtual.AutoSize = true;
            this.lblReplicarLancAtual.Location = new System.Drawing.Point(693, 385);
            this.lblReplicarLancAtual.Name = "lblReplicarLancAtual";
            this.lblReplicarLancAtual.Size = new System.Drawing.Size(133, 13);
            this.lblReplicarLancAtual.TabIndex = 52;
            this.lblReplicarLancAtual.Text = "Replicar lançamento atual:";
            // 
            // btnReplicar
            // 
            this.btnReplicar.Location = new System.Drawing.Point(712, 402);
            this.btnReplicar.Name = "btnReplicar";
            this.btnReplicar.Size = new System.Drawing.Size(135, 22);
            this.btnReplicar.TabIndex = 54;
            this.btnReplicar.Text = "Iniciar replicagem";
            this.btnReplicar.UseVisualStyleBackColor = true;
            this.btnReplicar.Click += new System.EventHandler(this.btnReplicar_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.Location = new System.Drawing.Point(742, 424);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(76, 22);
            this.btnGravar.TabIndex = 55;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // in2ReplicarVezes
            // 
            this.in2ReplicarVezes.Location = new System.Drawing.Point(829, 382);
            this.in2ReplicarVezes.Name = "in2ReplicarVezes";
            this.in2ReplicarVezes.Size = new System.Drawing.Size(28, 22);
            this.in2ReplicarVezes.TabIndex = 53;
            // 
            // frmReplicarLancamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 446);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.btnReplicar);
            this.Controls.Add(this.in2ReplicarVezes);
            this.Controls.Add(this.lblReplicarLancAtual);
            this.Controls.Add(this.lblLinhas);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.lblMensagem);
            this.Name = "frmReplicarLancamentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replicar Lançamentos";
            this.Load += new System.EventHandler(this.frmReplicarLancamentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.Label lblLinhas;
        private System.Windows.Forms.Label lblReplicarLancAtual;
        private INTEIRO2 in2ReplicarVezes;
        private System.Windows.Forms.Button btnReplicar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeFantasia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Residuo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataDescarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroMTRFatima;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroMTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Motivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Franquia;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoAterroSanitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deposito;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticket;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroCaixa;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCaminhaoColoca;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoMotoristaColocou;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColocacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataRetirada;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoMotoristaRetirou;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCaminhoRetirada;
    }
}