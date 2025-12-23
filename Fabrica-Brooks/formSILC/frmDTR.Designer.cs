namespace formSILC
{
    partial class frmDTR
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.datDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLote = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnImprimirMTR = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.DataColeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Residuo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalDTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinoMTRe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataSaida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoResiduo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(98, 624);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(30, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // datDataInicial
            // 
            this.datDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datDataInicial.Location = new System.Drawing.Point(10, 625);
            this.datDataInicial.Name = "datDataInicial";
            this.datDataInicial.Size = new System.Drawing.Size(85, 20);
            this.datDataInicial.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Lote:";
            // 
            // lblLote
            // 
            this.lblLote.AutoSize = true;
            this.lblLote.Location = new System.Drawing.Point(46, 11);
            this.lblLote.Name = "lblLote";
            this.lblLote.Size = new System.Drawing.Size(34, 13);
            this.lblLote.TabIndex = 16;
            this.lblLote.Text = "lbllote";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLote);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 39);
            this.panel1.TabIndex = 17;
            // 
            // Grade
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            this.Grade.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grade.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataColeta,
            this.NomeCliente,
            this.Residuo,
            this.Quantidade,
            this.Unidade,
            this.LocalDTR,
            this.DestinoMTRe,
            this.DataSaida,
            this.Lote,
            this.CodigoResiduo,
            this.NumeroLancamento,
            this.CodigoCliente});
            this.Grade.Dock = System.Windows.Forms.DockStyle.Top;
            this.Grade.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Grade.Location = new System.Drawing.Point(0, 39);
            this.Grade.Name = "Grade";
            this.Grade.Size = new System.Drawing.Size(1370, 560);
            this.Grade.TabIndex = 18;
            this.Grade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade_KeyUp);
            this.Grade.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Grade_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 609);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Período inicial:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(430, 623);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 28);
            this.btnSalvar.TabIndex = 20;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnImprimirMTR
            // 
            this.btnImprimirMTR.Location = new System.Drawing.Point(505, 623);
            this.btnImprimirMTR.Name = "btnImprimirMTR";
            this.btnImprimirMTR.Size = new System.Drawing.Size(82, 28);
            this.btnImprimirMTR.TabIndex = 21;
            this.btnImprimirMTR.Text = "Imprimir MTR";
            this.btnImprimirMTR.UseVisualStyleBackColor = true;
            this.btnImprimirMTR.Click += new System.EventHandler(this.btnImprimirMTR_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(587, 623);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 28);
            this.btnImprimir.TabIndex = 22;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(663, 623);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 28);
            this.btnEnviar.TabIndex = 23;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // DataColeta
            // 
            this.DataColeta.DataPropertyName = "DataColeta";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataColeta.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataColeta.HeaderText = "Data Coleta";
            this.DataColeta.Name = "DataColeta";
            this.DataColeta.ReadOnly = true;
            this.DataColeta.Width = 90;
            // 
            // NomeCliente
            // 
            this.NomeCliente.DataPropertyName = "NomeCliente";
            this.NomeCliente.HeaderText = "Nome ou Razão Social";
            this.NomeCliente.Name = "NomeCliente";
            this.NomeCliente.ReadOnly = true;
            this.NomeCliente.Width = 260;
            // 
            // Residuo
            // 
            this.Residuo.DataPropertyName = "Residuo";
            this.Residuo.HeaderText = "Grupo-Resíduo";
            this.Residuo.Name = "Residuo";
            this.Residuo.ReadOnly = true;
            this.Residuo.Width = 350;
            // 
            // Quantidade
            // 
            this.Quantidade.DataPropertyName = "Quantidade";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantidade.DefaultCellStyle = dataGridViewCellStyle3;
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.ReadOnly = true;
            // 
            // Unidade
            // 
            this.Unidade.DataPropertyName = "Unidade";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Unidade.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unidade.FillWeight = 50F;
            this.Unidade.HeaderText = "Un";
            this.Unidade.Name = "Unidade";
            this.Unidade.ReadOnly = true;
            this.Unidade.Width = 50;
            // 
            // LocalDTR
            // 
            this.LocalDTR.DataPropertyName = "LocalDTR";
            this.LocalDTR.HeaderText = "DTR";
            this.LocalDTR.Name = "LocalDTR";
            this.LocalDTR.ReadOnly = true;
            this.LocalDTR.Width = 80;
            // 
            // DestinoMTRe
            // 
            this.DestinoMTRe.DataPropertyName = "DestinoMTRe";
            this.DestinoMTRe.HeaderText = "Destino MTRe";
            this.DestinoMTRe.Name = "DestinoMTRe";
            this.DestinoMTRe.ReadOnly = true;
            // 
            // DataSaida
            // 
            this.DataSaida.DataPropertyName = "DataSaida";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataSaida.DefaultCellStyle = dataGridViewCellStyle5;
            this.DataSaida.HeaderText = "Data Saída";
            this.DataSaida.Name = "DataSaida";
            this.DataSaida.Width = 90;
            // 
            // Lote
            // 
            this.Lote.DataPropertyName = "Lote";
            this.Lote.HeaderText = "Lote";
            this.Lote.Name = "Lote";
            this.Lote.ReadOnly = true;
            this.Lote.Visible = false;
            // 
            // CodigoResiduo
            // 
            this.CodigoResiduo.DataPropertyName = "CodigoResiduo";
            this.CodigoResiduo.HeaderText = "CodigoResiduo";
            this.CodigoResiduo.Name = "CodigoResiduo";
            this.CodigoResiduo.Visible = false;
            // 
            // NumeroLancamento
            // 
            this.NumeroLancamento.DataPropertyName = "NumeroLancamento";
            this.NumeroLancamento.HeaderText = "NumeroLancamento";
            this.NumeroLancamento.Name = "NumeroLancamento";
            this.NumeroLancamento.Visible = false;
            // 
            // CodigoCliente
            // 
            this.CodigoCliente.DataPropertyName = "CodigoCliente";
            this.CodigoCliente.HeaderText = "Código Cliente";
            this.CodigoCliente.Name = "CodigoCliente";
            this.CodigoCliente.Visible = false;
            // 
            // frmDTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 701);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnImprimirMTR);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.datDataInicial);
            this.Controls.Add(this.btnOk);
            this.Name = "frmDTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DTR  - Depósito Temporário de Resíduos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDTR_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataGridView Grade;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DateTimePicker datDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnSalvar;
        public System.Windows.Forms.Button btnImprimirMTR;
        public System.Windows.Forms.Button btnImprimir;
        public System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Residuo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalDTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinoMTRe;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataSaida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoResiduo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCliente;
    }
}