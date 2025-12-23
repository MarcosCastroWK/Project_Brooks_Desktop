namespace formSILC
{
    partial class frmSistemaFatma
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelecionarArquivos = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.NomeRazaoSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenhaAcesso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Obs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenhaMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCaminhoDBSILC = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 23);
            this.textBox1.TabIndex = 7;
            // 
            // btnSelecionarArquivos
            // 
            this.btnSelecionarArquivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarArquivos.Location = new System.Drawing.Point(12, 7);
            this.btnSelecionarArquivos.Name = "btnSelecionarArquivos";
            this.btnSelecionarArquivos.Size = new System.Drawing.Size(162, 32);
            this.btnSelecionarArquivos.TabIndex = 6;
            this.btnSelecionarArquivos.Text = "Selecionar arquivo";
            this.btnSelecionarArquivos.UseVisualStyleBackColor = true;
            this.btnSelecionarArquivos.Click += new System.EventHandler(this.btnSelecionarArquivos_Click);
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // Grade
            // 
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomeRazaoSocial,
            this.SenhaAcesso,
            this.Contato,
            this.Obs,
            this.CNPJ_CPF,
            this.SenhaMaster});
            this.Grade.Location = new System.Drawing.Point(15, 169);
            this.Grade.Name = "Grade";
            this.Grade.Size = new System.Drawing.Size(1140, 384);
            this.Grade.TabIndex = 11;
            // 
            // NomeRazaoSocial
            // 
            this.NomeRazaoSocial.DataPropertyName = "RazaoSocial";
            this.NomeRazaoSocial.HeaderText = "Nome/Razão Social";
            this.NomeRazaoSocial.Name = "NomeRazaoSocial";
            this.NomeRazaoSocial.Width = 300;
            // 
            // SenhaAcesso
            // 
            this.SenhaAcesso.DataPropertyName = "Obs";
            this.SenhaAcesso.HeaderText = "Observação";
            this.SenhaAcesso.Name = "SenhaAcesso";
            this.SenhaAcesso.Width = 300;
            // 
            // Contato
            // 
            this.Contato.DataPropertyName = "CNPJ_CPF";
            this.Contato.HeaderText = "CNPJ/CPF";
            this.Contato.Name = "Contato";
            this.Contato.Width = 120;
            // 
            // Obs
            // 
            this.Obs.DataPropertyName = "SenhaMaster";
            this.Obs.HeaderText = "Senha Master";
            this.Obs.Name = "Obs";
            // 
            // CNPJ_CPF
            // 
            this.CNPJ_CPF.DataPropertyName = "SenhaAcesso";
            this.CNPJ_CPF.HeaderText = "Senha Acesso";
            this.CNPJ_CPF.Name = "CNPJ_CPF";
            // 
            // SenhaMaster
            // 
            this.SenhaMaster.DataPropertyName = "Contato";
            this.SenhaMaster.HeaderText = "Contato";
            this.SenhaMaster.Name = "SenhaMaster";
            this.SenhaMaster.Width = 150;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(644, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nome/Razão Social    Observação        CNPJ/CPF Cliente   Senha Master   Senha ac" +
    "esso      Contato";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Formato correto do arquivo.txt - Salvar texto em Unicode(*.txt)";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(12, 84);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 8;
            this.lblMensagem.Text = "Mensagem";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(418, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Caminho DB SILC";
            // 
            // txtCaminhoDBSILC
            // 
            this.txtCaminhoDBSILC.Location = new System.Drawing.Point(421, 48);
            this.txtCaminhoDBSILC.Name = "txtCaminhoDBSILC";
            this.txtCaminhoDBSILC.Size = new System.Drawing.Size(167, 20);
            this.txtCaminhoDBSILC.TabIndex = 13;
            this.txtCaminhoDBSILC.Text = "\\\\Servidor\\WinSilc\\SILC.MDB";
            // 
            // frmSistemaFatma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 565);
            this.Controls.Add(this.txtCaminhoDBSILC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSelecionarArquivos);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMensagem);
            this.Name = "frmSistemaFatma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Fatma - Atualização de Senhas";
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelecionarArquivos;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeRazaoSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SenhaAcesso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contato;
        private System.Windows.Forms.DataGridViewTextBoxColumn Obs;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn SenhaMaster;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCaminhoDBSILC;
    }
}