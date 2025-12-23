namespace formSILC
{
    partial class frmDistribuicaoDescargaPendente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistribuicaoDescargaPendente));
            this.Grade = new System.Windows.Forms.DataGridView();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.lblData = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblTicket = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPesoTotalColetado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTDifPerc = new System.Windows.Forms.Label();
            this.lblDiferenca = new System.Windows.Forms.Label();
            this.lblDiferencaPercentual = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalDescargaCalculada = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.moePesoTicket = new formSILC.MOEDA();
            this.funcionario1 = new formSILC.FUNCIONARIO();
            this.caminhao1 = new formSILC.CAMINHAO();
            this.intSegundo = new formSILC.INTEIRO2();
            this.intMinuto = new formSILC.INTEIRO2();
            this.intHora = new formSILC.INTEIRO2();
            this.destinofinal1 = new formSILC.DESTINOFINAL();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // Grade
            // 
            this.Grade.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Location = new System.Drawing.Point(335, 0);
            this.Grade.MultiSelect = false;
            this.Grade.Name = "Grade";
            this.Grade.ReadOnly = true;
            this.Grade.RowHeadersVisible = false;
            this.Grade.Size = new System.Drawing.Size(1062, 606);
            this.Grade.TabIndex = 9;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.Silver;
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(6, 12);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(65, 47);
            this.btnSalvar.TabIndex = 163;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(10, 174);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(82, 20);
            this.dtpData.TabIndex = 165;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(7, 158);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(33, 13);
            this.lblData.TabIndex = 166;
            this.lblData.Text = "Data:";
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(110, 158);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(23, 13);
            this.lblHora.TabIndex = 167;
            this.lblHora.Text = "HH";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 171;
            this.label1.Text = "MM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 172;
            this.label2.Text = "ss";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 173;
            this.label3.Text = "Local:";
            // 
            // lblLocal
            // 
            this.lblLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblLocal.Location = new System.Drawing.Point(10, 124);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(277, 18);
            this.lblLocal.TabIndex = 174;
            // 
            // lblTicket
            // 
            this.lblTicket.AutoSize = true;
            this.lblTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTicket.Location = new System.Drawing.Point(7, 216);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(45, 15);
            this.lblTicket.TabIndex = 175;
            this.lblTicket.Text = "Ticket";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(10, 235);
            this.txtTicket.MaxLength = 12;
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(128, 20);
            this.txtTicket.TabIndex = 176;
            this.txtTicket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTicket_KeyPress);
            this.txtTicket.Leave += new System.EventHandler(this.txtTicket_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 179;
            this.label4.Text = "a) Peso Descarga (Ticket):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 181;
            this.label5.Text = "b) Peso Total Coletado:";
            // 
            // lblPesoTotalColetado
            // 
            this.lblPesoTotalColetado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPesoTotalColetado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblPesoTotalColetado.Location = new System.Drawing.Point(175, 405);
            this.lblPesoTotalColetado.Name = "lblPesoTotalColetado";
            this.lblPesoTotalColetado.Size = new System.Drawing.Size(89, 18);
            this.lblPesoTotalColetado.TabIndex = 182;
            this.lblPesoTotalColetado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 183;
            this.label6.Text = "c) Diferença:";
            // 
            // lblTDifPerc
            // 
            this.lblTDifPerc.AutoSize = true;
            this.lblTDifPerc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTDifPerc.Location = new System.Drawing.Point(31, 450);
            this.lblTDifPerc.Name = "lblTDifPerc";
            this.lblTDifPerc.Size = new System.Drawing.Size(99, 13);
            this.lblTDifPerc.TabIndex = 184;
            this.lblTDifPerc.Text = "Diferença em %:";
            // 
            // lblDiferenca
            // 
            this.lblDiferenca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiferenca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblDiferenca.Location = new System.Drawing.Point(175, 428);
            this.lblDiferenca.Name = "lblDiferenca";
            this.lblDiferenca.Size = new System.Drawing.Size(89, 18);
            this.lblDiferenca.TabIndex = 185;
            this.lblDiferenca.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiferencaPercentual
            // 
            this.lblDiferencaPercentual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiferencaPercentual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblDiferencaPercentual.Location = new System.Drawing.Point(175, 450);
            this.lblDiferencaPercentual.Name = "lblDiferencaPercentual";
            this.lblDiferencaPercentual.Size = new System.Drawing.Size(89, 18);
            this.lblDiferencaPercentual.TabIndex = 186;
            this.lblDiferencaPercentual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(55, 517);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(102, 60);
            this.btnCalcular.TabIndex = 187;
            this.btnCalcular.Text = "Calcular Quantidades Descarga";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(170, 517);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 43);
            this.label7.TabIndex = 188;
            this.label7.Text = "Total Descarga Calculada (Somada)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTotalDescargaCalculada
            // 
            this.lblTotalDescargaCalculada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalDescargaCalculada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblTotalDescargaCalculada.Location = new System.Drawing.Point(175, 559);
            this.lblTotalDescargaCalculada.Name = "lblTotalDescargaCalculada";
            this.lblTotalDescargaCalculada.Size = new System.Drawing.Size(89, 18);
            this.lblTotalDescargaCalculada.TabIndex = 189;
            this.lblTotalDescargaCalculada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(70, 12);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(117, 47);
            this.btnExcluir.TabIndex = 190;
            this.btnExcluir.Text = "Excluir descarga";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // moePesoTicket
            // 
            this.moePesoTicket.Location = new System.Drawing.Point(175, 381);
            this.moePesoTicket.Name = "moePesoTicket";
            this.moePesoTicket.Size = new System.Drawing.Size(89, 21);
            this.moePesoTicket.TabIndex = 180;
            // 
            // funcionario1
            // 
            this.funcionario1.Location = new System.Drawing.Point(3, 321);
            this.funcionario1.Name = "funcionario1";
            this.funcionario1.Size = new System.Drawing.Size(324, 22);
            this.funcionario1.TabIndex = 178;
            this.funcionario1.TabIndexCodigo = 1;
            this.funcionario1.Enter += new System.EventHandler(this.funcionario1_Enter);
            // 
            // caminhao1
            // 
            this.caminhao1.Location = new System.Drawing.Point(3, 294);
            this.caminhao1.Name = "caminhao1";
            this.caminhao1.Size = new System.Drawing.Size(277, 22);
            this.caminhao1.TabIndex = 177;
            this.caminhao1.TabIndexCodigo = 1;
            this.caminhao1.Enter += new System.EventHandler(this.caminhao1_Enter);
            // 
            // intSegundo
            // 
            this.intSegundo.Location = new System.Drawing.Point(169, 174);
            this.intSegundo.Name = "intSegundo";
            this.intSegundo.Size = new System.Drawing.Size(28, 22);
            this.intSegundo.TabIndex = 170;
            // 
            // intMinuto
            // 
            this.intMinuto.Location = new System.Drawing.Point(139, 174);
            this.intMinuto.Name = "intMinuto";
            this.intMinuto.Size = new System.Drawing.Size(28, 22);
            this.intMinuto.TabIndex = 169;
            // 
            // intHora
            // 
            this.intHora.Location = new System.Drawing.Point(110, 174);
            this.intHora.Name = "intHora";
            this.intHora.Size = new System.Drawing.Size(28, 22);
            this.intHora.TabIndex = 168;
            // 
            // destinofinal1
            // 
            this.destinofinal1.Location = new System.Drawing.Point(3, 75);
            this.destinofinal1.Name = "destinofinal1";
            this.destinofinal1.Size = new System.Drawing.Size(326, 22);
            this.destinofinal1.TabIndex = 164;
            this.destinofinal1.TabIndexCodigo = 1;
            this.destinofinal1.Enter += new System.EventHandler(this.destinofinal1_Enter);
            this.destinofinal1.Leave += new System.EventHandler(this.destinofinal1_Leave);
            // 
            // frmDistribuicaoDescargaPendente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 606);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.lblTotalDescargaCalculada);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.lblDiferencaPercentual);
            this.Controls.Add(this.lblDiferenca);
            this.Controls.Add(this.lblTDifPerc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblPesoTotalColetado);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.moePesoTicket);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.funcionario1);
            this.Controls.Add(this.caminhao1);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.lblTicket);
            this.Controls.Add(this.lblLocal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intSegundo);
            this.Controls.Add(this.intMinuto);
            this.Controls.Add(this.intHora);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.destinofinal1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.Grade);
            this.Name = "frmDistribuicaoDescargaPendente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Disbribuição de descargas pendentes";
            this.Load += new System.EventHandler(this.frmListaReprogramacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Grade;
        public System.Windows.Forms.Button btnSalvar;
        private DESTINOFINAL destinofinal1;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblHora;
        private INTEIRO2 intHora;
        private INTEIRO2 intMinuto;
        private INTEIRO2 intSegundo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.TextBox txtTicket;
        private CAMINHAO caminhao1;
        private FUNCIONARIO funcionario1;
        private System.Windows.Forms.Label label4;
        private MOEDA moePesoTicket;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPesoTotalColetado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTDifPerc;
        private System.Windows.Forms.Label lblDiferenca;
        private System.Windows.Forms.Label lblDiferencaPercentual;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalDescargaCalculada;
        public System.Windows.Forms.Button btnExcluir;
    }
}