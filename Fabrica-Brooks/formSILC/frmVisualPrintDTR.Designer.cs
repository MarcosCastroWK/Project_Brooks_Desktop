namespace formSILC
{
    partial class frmVisualPrintDTR
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
            this.ppd = new System.Drawing.Printing.PrintDocument();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCopias = new System.Windows.Forms.Label();
            this.intCopias = new formSILC.INTEIRO2();
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.btnIr = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.cboPagina = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ppd
            // 
            this.ppd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ppd_PrintPage);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(3, 2);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnIr);
            this.panel1.Controls.Add(this.lblPagina);
            this.panel1.Controls.Add(this.cboPagina);
            this.panel1.Controls.Add(this.lblCopias);
            this.panel1.Controls.Add(this.intCopias);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 27);
            this.panel1.TabIndex = 2;
            // 
            // lblCopias
            // 
            this.lblCopias.AutoSize = true;
            this.lblCopias.Location = new System.Drawing.Point(95, 7);
            this.lblCopias.Name = "lblCopias";
            this.lblCopias.Size = new System.Drawing.Size(42, 13);
            this.lblCopias.TabIndex = 4;
            this.lblCopias.Text = "Cópias:";
            // 
            // intCopias
            // 
            this.intCopias.Location = new System.Drawing.Point(137, 3);
            this.intCopias.Name = "intCopias";
            this.intCopias.Size = new System.Drawing.Size(28, 22);
            this.intCopias.TabIndex = 3;
            this.intCopias.Leave += new System.EventHandler(this.intCopias_Leave);
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 27);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(672, 418);
            this.printPreviewControl.TabIndex = 3;
            // 
            // btnIr
            // 
            this.btnIr.Location = new System.Drawing.Point(368, 2);
            this.btnIr.Name = "btnIr";
            this.btnIr.Size = new System.Drawing.Size(25, 23);
            this.btnIr.TabIndex = 7;
            this.btnIr.Text = "Ir";
            this.btnIr.UseVisualStyleBackColor = true;
            this.btnIr.Click += new System.EventHandler(this.btnIr_Click);
            // 
            // lblPagina
            // 
            this.lblPagina.AutoSize = true;
            this.lblPagina.Location = new System.Drawing.Point(280, 8);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(43, 13);
            this.lblPagina.TabIndex = 6;
            this.lblPagina.Text = "Página:";
            // 
            // cboPagina
            // 
            this.cboPagina.FormattingEnabled = true;
            this.cboPagina.Location = new System.Drawing.Point(324, 3);
            this.cboPagina.Name = "cboPagina";
            this.cboPagina.Size = new System.Drawing.Size(42, 21);
            this.cboPagina.TabIndex = 5;
            // 
            // frmVisualPrintDTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 445);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.panel1);
            this.Name = "frmVisualPrintDTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Visualizador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVisualPrintDTR_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Drawing.Printing.PrintDocument ppd;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCopias;
        private INTEIRO2 intCopias;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.Button btnIr;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.ComboBox cboPagina;
    }
}