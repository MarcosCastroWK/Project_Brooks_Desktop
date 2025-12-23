namespace formSILC
{
    partial class frmCDFeImportacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCDFeImportacao));
            this.btnSelecionarArquivos = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GradeCDFe = new System.Windows.Forms.DataGridView();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.GradeInconsistecias = new System.Windows.Forms.DataGridView();
            this.NumeroMTRFatima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF_Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ctlCliente = new formSILC.CLIENTE();
            this.destinofinal1 = new formSILC.DESTINOFINAL();
            this.intAno = new formSILC.INTEIRO2();
            this.intMes = new formSILC.INTEIRO2();
            this.lblDB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GradeCDFe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeInconsistecias)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelecionarArquivos
            // 
            this.btnSelecionarArquivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarArquivos.Location = new System.Drawing.Point(12, 12);
            this.btnSelecionarArquivos.Name = "btnSelecionarArquivos";
            this.btnSelecionarArquivos.Size = new System.Drawing.Size(133, 32);
            this.btnSelecionarArquivos.TabIndex = 6;
            this.btnSelecionarArquivos.Text = "Importar arquivo";
            this.btnSelecionarArquivos.UseVisualStyleBackColor = true;
            this.btnSelecionarArquivos.Click += new System.EventHandler(this.btnSelecionarArquivos_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 23);
            this.textBox1.TabIndex = 4;
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(12, 89);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 2;
            this.lblMensagem.Text = "Mensagem";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Formato correto do arquivo em excel 8.0 ( *.xls)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Manter todos os campos do TXT";
            // 
            // GradeCDFe
            // 
            this.GradeCDFe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeCDFe.Location = new System.Drawing.Point(15, 174);
            this.GradeCDFe.Name = "GradeCDFe";
            this.GradeCDFe.Size = new System.Drawing.Size(779, 390);
            this.GradeCDFe.TabIndex = 5;
            this.GradeCDFe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeCDFe_CellContentClick);
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
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // GradeInconsistecias
            // 
            this.GradeInconsistecias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeInconsistecias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumeroMTRFatima,
            this.CNPJ_CPF_Cliente,
            this.OBS});
            this.GradeInconsistecias.Location = new System.Drawing.Point(15, 343);
            this.GradeInconsistecias.Name = "GradeInconsistecias";
            this.GradeInconsistecias.Size = new System.Drawing.Size(779, 221);
            this.GradeInconsistecias.TabIndex = 6;
            this.GradeInconsistecias.Visible = false;
            // 
            // NumeroMTRFatima
            // 
            this.NumeroMTRFatima.DataPropertyName = "NumeroMTRe";
            this.NumeroMTRFatima.HeaderText = "NumeroMTRe";
            this.NumeroMTRFatima.Name = "NumeroMTRFatima";
            // 
            // CNPJ_CPF_Cliente
            // 
            this.CNPJ_CPF_Cliente.DataPropertyName = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.HeaderText = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.Name = "CNPJ_CPF_Cliente";
            this.CNPJ_CPF_Cliente.Width = 120;
            // 
            // OBS
            // 
            this.OBS.DataPropertyName = "Obs";
            this.OBS.HeaderText = "OBS";
            this.OBS.Name = "OBS";
            this.OBS.Width = 500;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 24);
            this.button1.TabIndex = 7;
            this.button1.Text = "Imprimir Inconsistencias";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(563, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Período";
            // 
            // ctlCliente
            // 
            this.ctlCliente.Location = new System.Drawing.Point(151, 12);
            this.ctlCliente.Name = "ctlCliente";
            this.ctlCliente.Size = new System.Drawing.Size(379, 22);
            this.ctlCliente.TabIndex = 0;
            this.ctlCliente.TabIndexCodigo = 0;
            this.ctlCliente.Enter += new System.EventHandler(this.ctlCliente_Enter);
            this.ctlCliente.Leave += new System.EventHandler(this.ctlCliente_Leave);
            // 
            // destinofinal1
            // 
            this.destinofinal1.Location = new System.Drawing.Point(154, 39);
            this.destinofinal1.Name = "destinofinal1";
            this.destinofinal1.Size = new System.Drawing.Size(382, 22);
            this.destinofinal1.TabIndex = 0;
            this.destinofinal1.TabIndexCodigo = 1;
            this.destinofinal1.Enter += new System.EventHandler(this.destinofinal1_Enter);
            this.destinofinal1.Leave += new System.EventHandler(this.destinofinal1_Leave);
            // 
            // intAno
            // 
            this.intAno.Location = new System.Drawing.Point(573, 38);
            this.intAno.Name = "intAno";
            this.intAno.Size = new System.Drawing.Size(32, 26);
            this.intAno.TabIndex = 3;
            // 
            // intMes
            // 
            this.intMes.Location = new System.Drawing.Point(536, 38);
            this.intMes.Name = "intMes";
            this.intMes.Size = new System.Drawing.Size(32, 26);
            this.intMes.TabIndex = 2;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(630, 43);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(22, 13);
            this.lblDB.TabIndex = 39;
            this.lblDB.Text = "db:";
            // 
            // frmCDFeImportacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 567);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.ctlCliente);
            this.Controls.Add(this.destinofinal1);
            this.Controls.Add(this.intAno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.intMes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GradeInconsistecias);
            this.Controls.Add(this.GradeCDFe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSelecionarArquivos);
            this.Name = "frmCDFeImportacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CDFe Importacao";
            this.Load += new System.EventHandler(this.frmCDFeImportacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GradeCDFe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeInconsistecias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelecionarArquivos;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GradeCDFe;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.DataGridView GradeInconsistecias;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroMTRFatima;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF_Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBS;
        private INTEIRO2 intAno;
        private System.Windows.Forms.Label label3;
        private INTEIRO2 intMes;
        private System.Windows.Forms.Label label4;
        private DESTINOFINAL destinofinal1;
        private CLIENTE ctlCliente;
        private System.Windows.Forms.Label lblDB;
    }
}