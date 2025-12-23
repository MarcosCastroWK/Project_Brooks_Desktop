namespace formSILC
{
    partial class frmSolicitante
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
            this.lblObs = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtSolicitante = new System.Windows.Forms.TextBox();
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
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(12, 44);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(59, 13);
            this.lblObs.TabIndex = 10;
            this.lblObs.Text = "Solicitante:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(201, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtSolicitante
            // 
            this.txtSolicitante.Location = new System.Drawing.Point(73, 43);
            this.txtSolicitante.MaxLength = 20;
            this.txtSolicitante.Name = "txtSolicitante";
            this.txtSolicitante.Size = new System.Drawing.Size(203, 20);
            this.txtSolicitante.TabIndex = 13;
            this.txtSolicitante.Leave += new System.EventHandler(this.txtSolicitante_Leave);
            // 
            // frmSolicitante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 130);
            this.Controls.Add(this.txtSolicitante);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrSequencial);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmSolicitante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitante";
            this.Load += new System.EventHandler(this.frmSolicitante_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNrSequencial;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.TextBox txtSolicitante;
    }
}