namespace formSILC
{
    partial class frmLocalDTR
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
            this.lblTituloNrLancamento = new System.Windows.Forms.Label();
            this.cboDTRLocal = new System.Windows.Forms.ComboBox();
            this.lblSelecione = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNumeroLancamento
            // 
            this.lblNumeroLancamento.AutoSize = true;
            this.lblNumeroLancamento.Location = new System.Drawing.Point(87, 9);
            this.lblNumeroLancamento.Name = "lblNumeroLancamento";
            this.lblNumeroLancamento.Size = new System.Drawing.Size(49, 13);
            this.lblNumeroLancamento.TabIndex = 1;
            this.lblNumeroLancamento.Text = "0000000";
            // 
            // lblTituloNrLancamento
            // 
            this.lblTituloNrLancamento.AutoSize = true;
            this.lblTituloNrLancamento.Location = new System.Drawing.Point(12, 9);
            this.lblTituloNrLancamento.Name = "lblTituloNrLancamento";
            this.lblTituloNrLancamento.Size = new System.Drawing.Size(69, 13);
            this.lblTituloNrLancamento.TabIndex = 2;
            this.lblTituloNrLancamento.Text = "Lançamento:";
            // 
            // cboDTRLocal
            // 
            this.cboDTRLocal.FormattingEnabled = true;
            this.cboDTRLocal.Location = new System.Drawing.Point(15, 60);
            this.cboDTRLocal.Name = "cboDTRLocal";
            this.cboDTRLocal.Size = new System.Drawing.Size(356, 21);
            this.cboDTRLocal.TabIndex = 11;
            this.cboDTRLocal.Leave += new System.EventHandler(this.cboDTRLocal_Leave);
            // 
            // lblSelecione
            // 
            this.lblSelecione.AutoSize = true;
            this.lblSelecione.Location = new System.Drawing.Point(12, 44);
            this.lblSelecione.Name = "lblSelecione";
            this.lblSelecione.Size = new System.Drawing.Size(80, 13);
            this.lblSelecione.TabIndex = 10;
            this.lblSelecione.Text = "Selecione DTR";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(296, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmLocalDTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 130);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboDTRLocal);
            this.Controls.Add(this.lblSelecione);
            this.Controls.Add(this.lblNumeroLancamento);
            this.Controls.Add(this.lblTituloNrLancamento);
            this.Name = "frmLocalDTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informar Local de armazenamento";
            this.Load += new System.EventHandler(this.frmLocalDTR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label lblTituloNrLancamento;
        private System.Windows.Forms.Label lblSelecione;
        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Label lblNumeroLancamento;
        public System.Windows.Forms.ComboBox cboDTRLocal;
    }
}