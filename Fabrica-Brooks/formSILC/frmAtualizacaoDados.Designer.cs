namespace formSILC
{
    partial class frmAtualizacaoDados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCaminhoDBSILC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.GradeTabelasDB = new System.Windows.Forms.DataGridView();
            this.Tables_in_ewvssilc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Atualizar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Regs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.cliente1 = new formSILC.CLIENTE();
            this.int2Ano = new formSILC.INTEIRO2();
            this.int2Mes = new formSILC.INTEIRO2();
            this.lblDB = new System.Windows.Forms.Label();
            this.btnComparar = new System.Windows.Forms.Button();
            this.btnAtualizaParticularidade = new System.Windows.Forms.Button();
            this.butAtualizarAliquotasClientes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeTabelasDB)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCaminhoDBSILC
            // 
            this.txtCaminhoDBSILC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCaminhoDBSILC.Location = new System.Drawing.Point(560, 26);
            this.txtCaminhoDBSILC.Name = "txtCaminhoDBSILC";
            this.txtCaminhoDBSILC.Size = new System.Drawing.Size(167, 20);
            this.txtCaminhoDBSILC.TabIndex = 4;
            this.txtCaminhoDBSILC.Text = "\\\\SERVIDOR\\WINSILC\\SILC.MDB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Caminho DB SILC MDB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Mês      Ano";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "/";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(733, 23);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(32, 25);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Grade
            // 
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Location = new System.Drawing.Point(15, 97);
            this.Grade.Name = "Grade";
            this.Grade.Size = new System.Drawing.Size(818, 384);
            this.Grade.TabIndex = 29;
            this.Grade.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellContentClick);
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(90, 67);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 28;
            this.lblMensagem.Text = "Mensagem";
            // 
            // GradeTabelasDB
            // 
            this.GradeTabelasDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeTabelasDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tables_in_ewvssilc,
            this.Atualizar,
            this.Regs});
            this.GradeTabelasDB.Location = new System.Drawing.Point(839, 97);
            this.GradeTabelasDB.Name = "GradeTabelasDB";
            this.GradeTabelasDB.RowHeadersWidth = 24;
            this.GradeTabelasDB.Size = new System.Drawing.Size(316, 384);
            this.GradeTabelasDB.TabIndex = 35;
            this.GradeTabelasDB.CurrentCellDirtyStateChanged += new System.EventHandler(this.GradeTabelasDB_CurrentCellDirtyStateChanged);
            this.GradeTabelasDB.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.GradeTabelasDB_RowsAdded);
            // 
            // Tables_in_ewvssilc
            // 
            this.Tables_in_ewvssilc.DataPropertyName = "Tables_in_ewvssilc";
            this.Tables_in_ewvssilc.HeaderText = "Nome tabela";
            this.Tables_in_ewvssilc.Name = "Tables_in_ewvssilc";
            this.Tables_in_ewvssilc.Width = 170;
            // 
            // Atualizar
            // 
            this.Atualizar.HeaderText = "Atualizar";
            this.Atualizar.Name = "Atualizar";
            this.Atualizar.Width = 50;
            // 
            // Regs
            // 
            this.Regs.DataPropertyName = "Regs";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Regs.DefaultCellStyle = dataGridViewCellStyle2;
            this.Regs.HeaderText = "Regs";
            this.Regs.Name = "Regs";
            this.Regs.Width = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "Registros:";
            // 
            // cliente1
            // 
            this.cliente1.Location = new System.Drawing.Point(86, 25);
            this.cliente1.Name = "cliente1";
            this.cliente1.Size = new System.Drawing.Size(468, 20);
            this.cliente1.TabIndex = 2;
            this.cliente1.TabIndexCodigo = 0;
            this.cliente1.Enter += new System.EventHandler(this.cliente1_Enter);
            // 
            // int2Ano
            // 
            this.int2Ano.Location = new System.Drawing.Point(53, 25);
            this.int2Ano.Name = "int2Ano";
            this.int2Ano.Size = new System.Drawing.Size(32, 26);
            this.int2Ano.TabIndex = 1;
            // 
            // int2Mes
            // 
            this.int2Mes.Location = new System.Drawing.Point(15, 25);
            this.int2Mes.Name = "int2Mes";
            this.int2Mes.Size = new System.Drawing.Size(32, 26);
            this.int2Mes.TabIndex = 0;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(782, 29);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(25, 13);
            this.lblDB.TabIndex = 37;
            this.lblDB.Text = "db: ";
            // 
            // btnComparar
            // 
            this.btnComparar.Location = new System.Drawing.Point(986, 29);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(169, 62);
            this.btnComparar.TabIndex = 38;
            this.btnComparar.Text = "Comparar Contratos nos 2 DBs Reajustes e Resíduos Contratados";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // btnAtualizaParticularidade
            // 
            this.btnAtualizaParticularidade.Location = new System.Drawing.Point(785, 64);
            this.btnAtualizaParticularidade.Name = "btnAtualizaParticularidade";
            this.btnAtualizaParticularidade.Size = new System.Drawing.Size(163, 23);
            this.btnAtualizaParticularidade.TabIndex = 39;
            this.btnAtualizaParticularidade.Text = "atualizar particulariadade";
            this.btnAtualizaParticularidade.UseVisualStyleBackColor = true;
            this.btnAtualizaParticularidade.Click += new System.EventHandler(this.btnAtualizaParticularidade_Click);
            // 
            // butAtualizarAliquotasClientes
            // 
            this.butAtualizarAliquotasClientes.Location = new System.Drawing.Point(430, 65);
            this.butAtualizarAliquotasClientes.Name = "butAtualizarAliquotasClientes";
            this.butAtualizarAliquotasClientes.Size = new System.Drawing.Size(349, 23);
            this.butAtualizarAliquotasClientes.TabIndex = 40;
            this.butAtualizarAliquotasClientes.Text = "atualizar parametros aliquotas federais no cadastro de clientes";
            this.butAtualizarAliquotasClientes.UseVisualStyleBackColor = true;
            this.butAtualizarAliquotasClientes.Click += new System.EventHandler(this.butAtualizarAliquotasClientes_Click);
            // 
            // frmAtualizacaoDados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 491);
            this.Controls.Add(this.butAtualizarAliquotasClientes);
            this.Controls.Add(this.btnAtualizaParticularidade);
            this.Controls.Add(this.btnComparar);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GradeTabelasDB);
            this.Controls.Add(this.cliente1);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.int2Ano);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.int2Mes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCaminhoDBSILC);
            this.Controls.Add(this.label3);
            this.Name = "frmAtualizacaoDados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização de Dados - WinSILC Novo";
            this.Load += new System.EventHandler(this.frmAtualizacaoDados_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeTabelasDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCaminhoDBSILC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private INTEIRO2 int2Mes;
        private System.Windows.Forms.Label label2;
        private INTEIRO2 int2Ano;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.Label lblMensagem;
        public CLIENTE cliente1;
        private System.Windows.Forms.DataGridView GradeTabelasDB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tables_in_ewvssilc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Atualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regs;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Button btnComparar;
        private System.Windows.Forms.Button btnAtualizaParticularidade;
        private System.Windows.Forms.Button butAtualizarAliquotasClientes;
    }
}