namespace formSILC
{
    partial class frmAntecipacao
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
            this.cboTabMotivosOBS = new System.Windows.Forms.ComboBox();
            this.lblObs = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDataReprogramada = new System.Windows.Forms.DateTimePicker();
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
            // cboTabMotivosOBS
            // 
            this.cboTabMotivosOBS.FormattingEnabled = true;
            this.cboTabMotivosOBS.Location = new System.Drawing.Point(83, 64);
            this.cboTabMotivosOBS.Name = "cboTabMotivosOBS";
            this.cboTabMotivosOBS.Size = new System.Drawing.Size(356, 21);
            this.cboTabMotivosOBS.TabIndex = 0;
            this.cboTabMotivosOBS.Leave += new System.EventHandler(this.cboTabMotivosOBS_Leave);
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(12, 66);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(68, 13);
            this.lblObs.TabIndex = 10;
            this.lblObs.Text = "Observação:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(364, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Data antecipação:";
            // 
            // dtpDataReprogramada
            // 
            this.dtpDataReprogramada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataReprogramada.Location = new System.Drawing.Point(115, 35);
            this.dtpDataReprogramada.Name = "dtpDataReprogramada";
            this.dtpDataReprogramada.Size = new System.Drawing.Size(99, 20);
            this.dtpDataReprogramada.TabIndex = 0;
            // 
            // frmAntecipacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 130);
            this.Controls.Add(this.dtpDataReprogramada);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboTabMotivosOBS);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrSequencial);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmAntecipacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F10 - Antecipação";
            this.Load += new System.EventHandler(this.frmReprogramacao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNrSequencial;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.ComboBox cboTabMotivosOBS;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpDataReprogramada;
    }
}