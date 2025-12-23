namespace formSILC
{
    partial class frmConfirmaTroca
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
            this.lblNumeroLancamento = new System.Windows.Forms.Label();
            this.lblNoLancamento = new System.Windows.Forms.Label();
            this.lblObs = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.intNumeroLancamentoTroca = new formSILC.INTEIRO();
            this.SuspendLayout();
            // 
            // lblNumeroLancamento
            // 
            this.lblNumeroLancamento.AutoSize = true;
            this.lblNumeroLancamento.Location = new System.Drawing.Point(102, 9);
            this.lblNumeroLancamento.Name = "lblNumeroLancamento";
            this.lblNumeroLancamento.Size = new System.Drawing.Size(49, 13);
            this.lblNumeroLancamento.TabIndex = 1;
            this.lblNumeroLancamento.Text = "0000000";
            // 
            // lblNoLancamento
            // 
            this.lblNoLancamento.AutoSize = true;
            this.lblNoLancamento.Location = new System.Drawing.Point(12, 9);
            this.lblNoLancamento.Name = "lblNoLancamento";
            this.lblNoLancamento.Size = new System.Drawing.Size(84, 13);
            this.lblNoLancamento.TabIndex = 2;
            this.lblNoLancamento.Text = "Nº Lançamento:";
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(12, 44);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(115, 13);
            this.lblObs.TabIndex = 10;
            this.lblObs.Text = "Nº Lançamento Troca:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(362, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // intNumeroLancamentoTroca
            // 
            this.intNumeroLancamentoTroca.Location = new System.Drawing.Point(132, 42);
            this.intNumeroLancamentoTroca.Name = "intNumeroLancamentoTroca";
            this.intNumeroLancamentoTroca.Size = new System.Drawing.Size(97, 21);
            this.intNumeroLancamentoTroca.TabIndex = 13;
            // 
            // frmConfirmaTroca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 130);
            this.Controls.Add(this.intNumeroLancamentoTroca);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNumeroLancamento);
            this.Controls.Add(this.lblNoLancamento);
            this.Name = "frmConfirmaTroca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmação de troca";
            this.Load += new System.EventHandler(this.frmConfirmaTroca_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNumeroLancamento;
        private System.Windows.Forms.Label lblNoLancamento;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Button btnOk;
        public INTEIRO intNumeroLancamentoTroca;
    }
}