namespace formSILC
{
    partial class frmServicoExecutar
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
            this.lblNrSequencial = new System.Windows.Forms.Label();
            this.lblSequencial = new System.Windows.Forms.Label();
            this.cboServicoExecutar = new System.Windows.Forms.ComboBox();
            this.lblObs = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblDataRetirar = new System.Windows.Forms.Label();
            this.btnInserirRetirar = new System.Windows.Forms.Button();
            this.dtpDataRetirar = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lblNrSequencial
            // 
            this.lblNrSequencial.AutoSize = true;
            this.lblNrSequencial.Location = new System.Drawing.Point(81, 9);
            this.lblNrSequencial.Name = "lblNrSequencial";
            this.lblNrSequencial.Size = new System.Drawing.Size(49, 13);
            this.lblNrSequencial.TabIndex = 1;
            this.lblNrSequencial.Text = "0000000";
            // 
            // lblSequencial
            // 
            this.lblSequencial.AutoSize = true;
            this.lblSequencial.Location = new System.Drawing.Point(12, 9);
            this.lblSequencial.Name = "lblSequencial";
            this.lblSequencial.Size = new System.Drawing.Size(63, 13);
            this.lblSequencial.TabIndex = 2;
            this.lblSequencial.Text = "Sequencial:";
            // 
            // cboServicoExecutar
            // 
            this.cboServicoExecutar.FormattingEnabled = true;
            this.cboServicoExecutar.Location = new System.Drawing.Point(115, 41);
            this.cboServicoExecutar.Name = "cboServicoExecutar";
            this.cboServicoExecutar.Size = new System.Drawing.Size(325, 21);
            this.cboServicoExecutar.TabIndex = 11;
            this.cboServicoExecutar.SelectedIndexChanged += new System.EventHandler(this.cboServicoExecutar_SelectedIndexChanged);
            this.cboServicoExecutar.Leave += new System.EventHandler(this.cboServicoExecutar_Leave);
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(12, 44);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(97, 13);
            this.lblObs.TabIndex = 10;
            this.lblObs.Text = "Serviço a Executar";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(385, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(52, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblDataRetirar
            // 
            this.lblDataRetirar.AutoSize = true;
            this.lblDataRetirar.Location = new System.Drawing.Point(14, 94);
            this.lblDataRetirar.Name = "lblDataRetirar";
            this.lblDataRetirar.Size = new System.Drawing.Size(88, 13);
            this.lblDataRetirar.TabIndex = 13;
            this.lblDataRetirar.Text = "Data para Retirar";
            // 
            // btnInserirRetirar
            // 
            this.btnInserirRetirar.Location = new System.Drawing.Point(283, 90);
            this.btnInserirRetirar.Name = "btnInserirRetirar";
            this.btnInserirRetirar.Size = new System.Drawing.Size(98, 28);
            this.btnInserirRetirar.TabIndex = 14;
            this.btnInserirRetirar.Text = "Inserir RETIRAR";
            this.btnInserirRetirar.UseVisualStyleBackColor = true;
            this.btnInserirRetirar.Click += new System.EventHandler(this.btnInserirRetirar_Click);
            // 
            // dtpDataRetirar
            // 
            this.dtpDataRetirar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataRetirar.Location = new System.Drawing.Point(115, 91);
            this.dtpDataRetirar.Name = "dtpDataRetirar";
            this.dtpDataRetirar.Size = new System.Drawing.Size(107, 20);
            this.dtpDataRetirar.TabIndex = 15;
            this.dtpDataRetirar.ValueChanged += new System.EventHandler(this.dtpDataRetirar_ValueChanged);
            // 
            // frmServicoExecutar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 130);
            this.Controls.Add(this.dtpDataRetirar);
            this.Controls.Add(this.btnInserirRetirar);
            this.Controls.Add(this.lblDataRetirar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboServicoExecutar);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrSequencial);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmServicoExecutar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serviço a Executar";
            this.Load += new System.EventHandler(this.frmObservacao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNrSequencial;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.ComboBox cboServicoExecutar;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblDataRetirar;
        private System.Windows.Forms.Button btnInserirRetirar;
        private System.Windows.Forms.DateTimePicker dtpDataRetirar;
    }
}