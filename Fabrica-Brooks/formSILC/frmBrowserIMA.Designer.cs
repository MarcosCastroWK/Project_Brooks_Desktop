namespace formSILC
{
    partial class frmBrowserIMA
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
            this.intMTRe = new formSILC.INTEIRO();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butConsulta
            // 
            this.butConsulta.Location = new System.Drawing.Point(194, 3);
            this.butConsulta.Name = "butConsulta";
            this.butConsulta.Size = new System.Drawing.Size(75, 23);
            this.butConsulta.TabIndex = 1;
            this.butConsulta.Text = "Consultar";
            this.butConsulta.UseVisualStyleBackColor = true;
            this.butConsulta.Click += new System.EventHandler(this.butConsultaNF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "MTR eletrônica:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butConsulta);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.intMTRe);
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
            // intMTRe
            // 
            this.intMTRe.Location = new System.Drawing.Point(92, 3);
            this.intMTRe.Name = "intMTRe";
            this.intMTRe.Size = new System.Drawing.Size(96, 23);
            this.intMTRe.TabIndex = 2;
            // 
            // frmBrowserIMA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 568);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Name = "frmBrowserIMA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMA MTR Eletrônica";
            this.Load += new System.EventHandler(this.frmBrowserNF_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button butConsulta;
        public INTEIRO intMTRe;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}