namespace formSILC
{
    partial class frmBrowserMTRe_Consulta
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
            this.butConsulta = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lblNumeroMTRe = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butConsulta
            // 
            this.butConsulta.Location = new System.Drawing.Point(154, 2);
            this.butConsulta.Name = "butConsulta";
            this.butConsulta.Size = new System.Drawing.Size(75, 23);
            this.butConsulta.TabIndex = 1;
            this.butConsulta.Text = "Consulta";
            this.butConsulta.UseVisualStyleBackColor = true;
            this.butConsulta.Click += new System.EventHandler(this.butConsultaNF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Número:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNumeroMTRe);
            this.panel1.Controls.Add(this.butConsulta);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 28);
            this.panel1.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 28);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(752, 540);
            this.webBrowser1.TabIndex = 5;
            // 
            // lblNumeroMTRe
            // 
            this.lblNumeroMTRe.AutoSize = true;
            this.lblNumeroMTRe.Location = new System.Drawing.Point(56, 6);
            this.lblNumeroMTRe.Name = "lblNumeroMTRe";
            this.lblNumeroMTRe.Size = new System.Drawing.Size(67, 13);
            this.lblNumeroMTRe.TabIndex = 4;
            this.lblNumeroMTRe.Text = "0000000000";
            // 
            // frmBrowserMTRe_Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 568);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Name = "frmBrowserMTRe_Consulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta MTR eletrônica";
            this.Load += new System.EventHandler(this.frmBrowserNF_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button butConsulta;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.Label lblNumeroMTRe;
    }
}