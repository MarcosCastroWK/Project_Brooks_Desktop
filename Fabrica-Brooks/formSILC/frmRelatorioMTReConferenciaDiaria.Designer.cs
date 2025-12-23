namespace formSILC
{
    partial class frmRelatorioMTReConferenciaDiaria
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelatorioMTReConferenciaDiaria));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelecionarArquivos = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.GradeCDFe = new System.Windows.Forms.DataGridView();
            this.CNPJ_Cliente_IMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroMTRe_IMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIbamaIMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qtde_IMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data_IMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF_Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroMTRe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIbama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qtde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Obs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRelatorioMTR = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.lblLadoSILC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GradeCDFe)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 23);
            this.textBox1.TabIndex = 9;
            // 
            // btnSelecionarArquivos
            // 
            this.btnSelecionarArquivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarArquivos.Location = new System.Drawing.Point(6, 4);
            this.btnSelecionarArquivos.Name = "btnSelecionarArquivos";
            this.btnSelecionarArquivos.Size = new System.Drawing.Size(133, 32);
            this.btnSelecionarArquivos.TabIndex = 8;
            this.btnSelecionarArquivos.Text = "Importar arquivo";
            this.btnSelecionarArquivos.UseVisualStyleBackColor = true;
            this.btnSelecionarArquivos.Click += new System.EventHandler(this.btnSelecionarArquivos_Click);
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // GradeCDFe
            // 
            this.GradeCDFe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeCDFe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNPJ_Cliente_IMA,
            this.NumeroMTRe_IMA,
            this.CodigoIbamaIMA,
            this.Qtde_IMA,
            this.Data_IMA,
            this.CNPJ_CPF_Cliente,
            this.NumeroMTRe,
            this.CodigoIbama,
            this.Qtde,
            this.Data,
            this.Obs});
            this.GradeCDFe.Location = new System.Drawing.Point(12, 138);
            this.GradeCDFe.Name = "GradeCDFe";
            this.GradeCDFe.Size = new System.Drawing.Size(1204, 487);
            this.GradeCDFe.TabIndex = 13;
            // 
            // CNPJ_Cliente_IMA
            // 
            this.CNPJ_Cliente_IMA.DataPropertyName = "CNPJ_Cliente_IMA";
            this.CNPJ_Cliente_IMA.HeaderText = "CNPJ_Cliente_IMA";
            this.CNPJ_Cliente_IMA.Name = "CNPJ_Cliente_IMA";
            this.CNPJ_Cliente_IMA.Width = 115;
            // 
            // NumeroMTRe_IMA
            // 
            this.NumeroMTRe_IMA.DataPropertyName = "NumeroMTRe_IMA";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NumeroMTRe_IMA.DefaultCellStyle = dataGridViewCellStyle5;
            this.NumeroMTRe_IMA.HeaderText = "NumeroMTRe_IMA";
            this.NumeroMTRe_IMA.Name = "NumeroMTRe_IMA";
            this.NumeroMTRe_IMA.Width = 80;
            // 
            // CodigoIbamaIMA
            // 
            this.CodigoIbamaIMA.DataPropertyName = "CodigoIbamaIMA";
            this.CodigoIbamaIMA.HeaderText = "CodigoIbamaIMA";
            this.CodigoIbamaIMA.Name = "CodigoIbamaIMA";
            // 
            // Qtde_IMA
            // 
            this.Qtde_IMA.DataPropertyName = "Qtde_IMA";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qtde_IMA.DefaultCellStyle = dataGridViewCellStyle6;
            this.Qtde_IMA.HeaderText = "Qtde_IMA";
            this.Qtde_IMA.Name = "Qtde_IMA";
            this.Qtde_IMA.Width = 60;
            // 
            // Data_IMA
            // 
            this.Data_IMA.DataPropertyName = "Data_IMA";
            this.Data_IMA.HeaderText = "Data_IMA";
            this.Data_IMA.Name = "Data_IMA";
            this.Data_IMA.Width = 70;
            // 
            // CNPJ_CPF_Cliente
            // 
            this.CNPJ_CPF_Cliente.DataPropertyName = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.HeaderText = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.Name = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.Width = 115;
            // 
            // NumeroMTRe
            // 
            this.NumeroMTRe.DataPropertyName = "NumeroMTRe";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NumeroMTRe.DefaultCellStyle = dataGridViewCellStyle7;
            this.NumeroMTRe.HeaderText = "NumeroMTRe";
            this.NumeroMTRe.Name = "NumeroMTRe";
            this.NumeroMTRe.Width = 80;
            // 
            // CodigoIbama
            // 
            this.CodigoIbama.DataPropertyName = "CodigoIbama";
            this.CodigoIbama.HeaderText = "CodigoIbama";
            this.CodigoIbama.Name = "CodigoIbama";
            // 
            // Qtde
            // 
            this.Qtde.DataPropertyName = "Qtde";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qtde.DefaultCellStyle = dataGridViewCellStyle8;
            this.Qtde.HeaderText = "Qtde";
            this.Qtde.Name = "Qtde";
            this.Qtde.Width = 60;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 70;
            // 
            // Obs
            // 
            this.Obs.DataPropertyName = "Obs";
            this.Obs.HeaderText = "Obs";
            this.Obs.Name = "Obs";
            this.Obs.Width = 285;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(349, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Manter todos os campos do arquivo";
            // 
            // lblRelatorioMTR
            // 
            this.lblRelatorioMTR.AutoSize = true;
            this.lblRelatorioMTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelatorioMTR.Location = new System.Drawing.Point(51, 118);
            this.lblRelatorioMTR.Name = "lblRelatorioMTR";
            this.lblRelatorioMTR.Size = new System.Drawing.Size(254, 17);
            this.lblRelatorioMTR.TabIndex = 11;
            this.lblRelatorioMTR.Text = "Dados do relatório da MTRe Importado";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(6, 81);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 10;
            this.lblMensagem.Text = "Mensagem";
            // 
            // ppd
            // 
            this.ppd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppd.ClientSize = new System.Drawing.Size(400, 300);
            this.ppd.Enabled = true;
            this.ppd.Icon = ((System.Drawing.Icon)(resources.GetObject("ppd.Icon")));
            this.ppd.Name = "ppd";
            this.ppd.Visible = false;
            this.ppd.Load += new System.EventHandler(this.ppd_Load);
            // 
            // lblLadoSILC
            // 
            this.lblLadoSILC.AutoSize = true;
            this.lblLadoSILC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLadoSILC.Location = new System.Drawing.Point(481, 118);
            this.lblLadoSILC.Name = "lblLadoSILC";
            this.lblLadoSILC.Size = new System.Drawing.Size(227, 17);
            this.lblLadoSILC.TabIndex = 16;
            this.lblLadoSILC.Text = "Dados MTRe Lançado no WinSILC";
            // 
            // frmRelatorioMTReConferenciaDiaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 638);
            this.Controls.Add(this.lblLadoSILC);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSelecionarArquivos);
            this.Controls.Add(this.GradeCDFe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRelatorioMTR);
            this.Controls.Add(this.lblMensagem);
            this.Name = "frmRelatorioMTReConferenciaDiaria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importação do Relatório MTR-e para Conferêrncia Diária";
            ((System.ComponentModel.ISupportInitialize)(this.GradeCDFe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelecionarArquivos;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.DataGridView GradeCDFe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRelatorioMTR;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.Label lblLadoSILC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_Cliente_IMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroMTRe_IMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIbamaIMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qtde_IMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data_IMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF_Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroMTRe;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIbama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qtde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Obs;
    }
}