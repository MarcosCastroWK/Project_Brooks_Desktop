namespace formSILC
{
    partial class frmDestinoFinal
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
            this.cboDestinoFinal = new System.Windows.Forms.ComboBox();
            this.lblObs = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.residuo1 = new formSILC.RESIDUO();
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
            // cboDestinoFinal
            // 
            this.cboDestinoFinal.FormattingEnabled = true;
            this.cboDestinoFinal.Location = new System.Drawing.Point(84, 74);
            this.cboDestinoFinal.Name = "cboDestinoFinal";
            this.cboDestinoFinal.Size = new System.Drawing.Size(396, 21);
            this.cboDestinoFinal.TabIndex = 11;
            this.cboDestinoFinal.Leave += new System.EventHandler(this.cboDestinoFinal_Leave);
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Location = new System.Drawing.Point(13, 76);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(68, 13);
            this.lblObs.TabIndex = 10;
            this.lblObs.Text = "Destino final:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(405, 123);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // residuo1
            // 
            this.residuo1.Location = new System.Drawing.Point(10, 38);
            this.residuo1.Name = "residuo1";
            this.residuo1.Size = new System.Drawing.Size(470, 22);
            this.residuo1.TabIndex = 13;
            this.residuo1.TabIndexCodigo = 3;
            this.residuo1.Unidade = null;
            this.residuo1.Enter += new System.EventHandler(this.Residuo1_Enter);
            this.residuo1.Leave += new System.EventHandler(this.Residuo1_Leave);
            // 
            // frmDestinoFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 159);
            this.Controls.Add(this.residuo1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboDestinoFinal);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.lblNrSequencial);
            this.Controls.Add(this.lblSequencial);
            this.Name = "frmDestinoFinal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Destino final do resíduo";
            this.Load += new System.EventHandler(this.frmDestinoFinal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNrSequencial;
        public System.Windows.Forms.Label lblSequencial;
        public System.Windows.Forms.ComboBox cboDestinoFinal;
        public System.Windows.Forms.Label lblObs;
        public System.Windows.Forms.Button btnOk;
        public RESIDUO residuo1;
    }
}