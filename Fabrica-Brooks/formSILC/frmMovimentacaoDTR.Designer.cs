
namespace formSILC
{
    partial class frmMovimentacaoDTR
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
            this.panOpcao = new System.Windows.Forms.Panel();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoviCxDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoResiduo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panOpcao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // panOpcao
            // 
            this.panOpcao.Controls.Add(this.btnExcluir);
            this.panOpcao.Controls.Add(this.btnSalvar);
            this.panOpcao.Dock = System.Windows.Forms.DockStyle.Top;
            this.panOpcao.Location = new System.Drawing.Point(0, 0);
            this.panOpcao.Name = "panOpcao";
            this.panOpcao.Size = new System.Drawing.Size(319, 41);
            this.panOpcao.TabIndex = 1;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(193, 7);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(119, 28);
            this.btnExcluir.TabIndex = 1;
            this.btnExcluir.Text = "Excluir última linha";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(2, 7);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(68, 28);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // Grade
            // 
            this.Grade.AllowUserToAddRows = false;
            this.Grade.AllowUserToDeleteRows = false;
            this.Grade.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.MoviCxDe,
            this.NumeroLancamento,
            this.CodigoCliente,
            this.CodigoResiduo});
            this.Grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grade.Location = new System.Drawing.Point(0, 41);
            this.Grade.MultiSelect = false;
            this.Grade.Name = "Grade";
            this.Grade.ShowEditingIcon = false;
            this.Grade.Size = new System.Drawing.Size(319, 402);
            this.Grade.TabIndex = 2;
            this.Grade.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Grade_MouseClick);
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 80;
            // 
            // MoviCxDe
            // 
            this.MoviCxDe.DataPropertyName = "MoviCxDe";
            this.MoviCxDe.HeaderText = "De";
            this.MoviCxDe.Name = "MoviCxDe";
            this.MoviCxDe.Width = 90;
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
            this.CodigoCliente.HeaderText = "CodigoCliente";
            this.CodigoCliente.Name = "CodigoCliente";
            this.CodigoCliente.Visible = false;
            // 
            // CodigoResiduo
            // 
            this.CodigoResiduo.DataPropertyName = "CodigoResiduo";
            this.CodigoResiduo.HeaderText = "CodigoResiduo";
            this.CodigoResiduo.Name = "CodigoResiduo";
            this.CodigoResiduo.Visible = false;
            // 
            // frmMovimentacaoDTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 443);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.panOpcao);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMovimentacaoDTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimentação DTR";
            this.Load += new System.EventHandler(this.frmMovimentacaoDTR_Load);
            this.panOpcao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.Panel panOpcao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoviCxDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoResiduo;
        private System.Windows.Forms.Button btnExcluir;
    }
}