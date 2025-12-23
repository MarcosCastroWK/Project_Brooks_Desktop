namespace formSILC
{
    partial class frmMaisNFs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GradeNFs = new System.Windows.Forms.DataGridView();
            this.NumeroNF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataEmissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorISS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorPIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorCOFINS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorCSLL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeNFs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNomeCliente);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCodigo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 58);
            this.panel1.TabIndex = 5;
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Enabled = false;
            this.txtNomeCliente.Location = new System.Drawing.Point(53, 27);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(336, 20);
            this.txtNomeCliente.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cliente:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(53, 3);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(73, 20);
            this.txtCodigo.TabIndex = 5;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código:";
            // 
            // GradeNFs
            // 
            this.GradeNFs.AllowUserToAddRows = false;
            this.GradeNFs.AllowUserToDeleteRows = false;
            this.GradeNFs.AllowUserToResizeColumns = false;
            this.GradeNFs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.GradeNFs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeNFs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumeroNF,
            this.DataEmissao,
            this.ValorTotal,
            this.ValorISS,
            this.ValorPIS,
            this.ValorCOFINS,
            this.ValorCSLL,
            this.Situacao,
            this.TipoDocumento});
            this.GradeNFs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradeNFs.Location = new System.Drawing.Point(0, 58);
            this.GradeNFs.Name = "GradeNFs";
            this.GradeNFs.ReadOnly = true;
            this.GradeNFs.RowHeadersVisible = false;
            this.GradeNFs.Size = new System.Drawing.Size(858, 506);
            this.GradeNFs.TabIndex = 6;
            this.GradeNFs.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GradeNFs_CellContentDoubleClick);
            // 
            // NumeroNF
            // 
            this.NumeroNF.DataPropertyName = "NumeroNF";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NumeroNF.DefaultCellStyle = dataGridViewCellStyle1;
            this.NumeroNF.HeaderText = "Nº NF";
            this.NumeroNF.Name = "NumeroNF";
            this.NumeroNF.ReadOnly = true;
            this.NumeroNF.Width = 80;
            // 
            // DataEmissao
            // 
            this.DataEmissao.DataPropertyName = "DataEmissao";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataEmissao.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataEmissao.HeaderText = "Data Emissão";
            this.DataEmissao.Name = "DataEmissao";
            this.DataEmissao.ReadOnly = true;
            // 
            // ValorTotal
            // 
            this.ValorTotal.DataPropertyName = "ValorTotal";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.ValorTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.ValorTotal.HeaderText = "Valor Total";
            this.ValorTotal.Name = "ValorTotal";
            this.ValorTotal.ReadOnly = true;
            // 
            // ValorISS
            // 
            this.ValorISS.DataPropertyName = "ValorISS";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.ValorISS.DefaultCellStyle = dataGridViewCellStyle4;
            this.ValorISS.HeaderText = "Valor ISS";
            this.ValorISS.Name = "ValorISS";
            this.ValorISS.ReadOnly = true;
            // 
            // ValorPIS
            // 
            this.ValorPIS.DataPropertyName = "ValorPIS";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.ValorPIS.DefaultCellStyle = dataGridViewCellStyle5;
            this.ValorPIS.HeaderText = "Valor PIS";
            this.ValorPIS.Name = "ValorPIS";
            this.ValorPIS.ReadOnly = true;
            // 
            // ValorCOFINS
            // 
            this.ValorCOFINS.DataPropertyName = "ValorCOFINS";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.ValorCOFINS.DefaultCellStyle = dataGridViewCellStyle6;
            this.ValorCOFINS.HeaderText = "Valor COFINS";
            this.ValorCOFINS.Name = "ValorCOFINS";
            this.ValorCOFINS.ReadOnly = true;
            // 
            // ValorCSLL
            // 
            this.ValorCSLL.DataPropertyName = "ValorContrSocial";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.ValorCSLL.DefaultCellStyle = dataGridViewCellStyle7;
            this.ValorCSLL.HeaderText = "Valor CSLL";
            this.ValorCSLL.Name = "ValorCSLL";
            this.ValorCSLL.ReadOnly = true;
            // 
            // Situacao
            // 
            this.Situacao.DataPropertyName = "Situacao";
            this.Situacao.HeaderText = "Situação";
            this.Situacao.Name = "Situacao";
            this.Situacao.ReadOnly = true;
            // 
            // TipoDocumento
            // 
            this.TipoDocumento.DataPropertyName = "TipoDocumento";
            this.TipoDocumento.HeaderText = "Tipo Docm";
            this.TipoDocumento.Name = "TipoDocumento";
            this.TipoDocumento.ReadOnly = true;
            this.TipoDocumento.Width = 52;
            // 
            // frmMaisNFs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 564);
            this.Controls.Add(this.GradeNFs);
            this.Controls.Add(this.panel1);
            this.Name = "frmMaisNFs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mais Notas Fiscais do cliente";
            this.Load += new System.EventHandler(this.frmMaisNFs_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GradeNFs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GradeNFs;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroNF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataEmissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorISS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorPIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorCOFINS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorCSLL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocumento;

    }
}