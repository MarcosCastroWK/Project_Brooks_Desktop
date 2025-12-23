namespace formSILC
{
    partial class frmLocacaoProgramacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocacaoProgramacao));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbDataProgAberta = new System.Windows.Forms.GroupBox();
            this.DataProgAberta = new System.Windows.Forms.DateTimePicker();
            this.lblLinhasGrade2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMTRe = new System.Windows.Forms.Button();
            this.lblCodigoCliente = new System.Windows.Forms.Label();
            this.txtSenhaAcessoFatma = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCNPJ_CPF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cdnmCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grbBotoes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblBloqueadoPeloUsuario = new System.Windows.Forms.Label();
            this.btnDadosContrato = new System.Windows.Forms.Button();
            this.btnServicosCliente = new System.Windows.Forms.Button();
            this.btnDadosCliente = new System.Windows.Forms.Button();
            this.btnExecutado = new System.Windows.Forms.Button();
            this.Grade1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grbDataProgAberta.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.grbDataProgAberta.Text = "Data EXECUTADOS  | Linhas";
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 609);
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
            this.panel2.Size = new System.Drawing.Size(1169, 69);
            this.panel2.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMTRe);
            this.groupBox1.Controls.Add(this.lblCodigoCliente);
            this.groupBox1.Controls.Add(this.txtSenhaAcessoFatma);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCNPJ_CPF);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cdnmCliente);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(828, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 58);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sistema MTR-e Fátima";
            // 
            // btnMTRe
            // 
            this.btnMTRe.AutoSize = true;
            this.btnMTRe.Location = new System.Drawing.Point(203, 7);
            this.btnMTRe.Name = "btnMTRe";
            this.btnMTRe.Size = new System.Drawing.Size(38, 23);
            this.btnMTRe.TabIndex = 8;
            this.btnMTRe.Text = "mtre";
            this.btnMTRe.UseVisualStyleBackColor = true;
            this.btnMTRe.Click += new System.EventHandler(this.btnMTRe_Click);
            // 
            // lblCodigoCliente
            // 
            this.lblCodigoCliente.AutoSize = true;
            this.lblCodigoCliente.Location = new System.Drawing.Point(42, 14);
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
            this.label6.Location = new System.Drawing.Point(245, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Senha de Acesso";
            // 
            // txtCNPJ_CPF
            // 
            this.txtCNPJ_CPF.Location = new System.Drawing.Point(122, 28);
            this.txtCNPJ_CPF.Name = "txtCNPJ_CPF";
            this.txtCNPJ_CPF.Size = new System.Drawing.Size(119, 20);
            this.txtCNPJ_CPF.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "CNPJ/CPF";
            // 
            // cdnmCliente
            // 
            this.cdnmCliente.Location = new System.Drawing.Point(7, 29);
            this.cdnmCliente.Name = "cdnmCliente";
            this.cdnmCliente.Size = new System.Drawing.Size(109, 20);
            this.cdnmCliente.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cliente:";
            // 
            // grbBotoes
            // 
            this.grbBotoes.Controls.Add(this.label1);
            this.grbBotoes.Controls.Add(this.lblUsuario);
            this.grbBotoes.Controls.Add(this.lblBloqueadoPeloUsuario);
            this.grbBotoes.Controls.Add(this.btnDadosContrato);
            this.grbBotoes.Controls.Add(this.btnServicosCliente);
            this.grbBotoes.Controls.Add(this.btnDadosCliente);
            this.grbBotoes.Controls.Add(this.btnExecutado);
            this.grbBotoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbBotoes.Location = new System.Drawing.Point(0, 0);
            this.grbBotoes.Name = "grbBotoes";
            this.grbBotoes.Size = new System.Drawing.Size(1169, 58);
            this.grbBotoes.TabIndex = 5;
            this.grbBotoes.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(116, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "F7 - Visto - Verde claro";
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
            // Grade1
            // 
            this.Grade1.AllowUserToAddRows = false;
            this.Grade1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grade1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grade1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grade1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Grade1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grade1.Location = new System.Drawing.Point(0, 0);
            this.Grade1.Name = "Grade1";
            this.Grade1.RowHeadersVisible = false;
            this.Grade1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.Grade1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Grade1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grade1.Size = new System.Drawing.Size(1169, 540);
            this.Grade1.TabIndex = 16;
            this.Grade1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade1_CellContentClick);
            this.Grade1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade1_CellContentDoubleClick);
            this.Grade1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grade1_ColumnHeaderMouseClick);
            this.Grade1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade1_KeyUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Grade1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(201, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1169, 540);
            this.panel3.TabIndex = 18;
            // 
            // frmLocacaoProgramacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1370, 609);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmLocacaoProgramacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Locação Programação";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProgramacaoDiaria_FormClosed);
            this.Load += new System.EventHandler(this.frmLocacaoProgramacao_Load);
            this.grbDataProgAberta.ResumeLayout(false);
            this.grbDataProgAberta.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbBotoes.ResumeLayout(false);
            this.grbBotoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Label lblBloqueadoPeloUsuario;
        private System.Windows.Forms.Button btnDadosContrato;
        private System.Windows.Forms.Button btnServicosCliente;
        private System.Windows.Forms.Button btnDadosCliente;
        private System.Windows.Forms.Button btnExecutado;
        public System.Windows.Forms.DateTimePicker DataProgAberta;
        private System.Windows.Forms.Label lblCodigoCliente;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView Grade1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMTRe;
        private System.Windows.Forms.Label label1;
    }
}