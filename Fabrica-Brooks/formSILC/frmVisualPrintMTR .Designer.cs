namespace formSILC
{
    partial class frmVisualPrintMTR
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
            this.cboMotorista = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPlacas = new System.Windows.Forms.ComboBox();
            this.lblCopias = new System.Windows.Forms.Label();
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.intCopias = new formSILC.INTEIRO2();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ppd
            // 
            this.ppd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ppd_PrintPage);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(3, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnVisualizar);
            this.panel1.Controls.Add(this.cboMotorista);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboPlacas);
            this.panel1.Controls.Add(this.lblCopias);
            this.panel1.Controls.Add(this.intCopias);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(968, 56);
            this.panel1.TabIndex = 2;
            // 
            // cboMotorista
            // 
            this.cboMotorista.FormattingEnabled = true;
            this.cboMotorista.Location = new System.Drawing.Point(328, 28);
            this.cboMotorista.Name = "cboMotorista";
            this.cboMotorista.Size = new System.Drawing.Size(378, 21);
            this.cboMotorista.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Motorista:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Placa veículo:";
            // 
            // cboPlacas
            // 
            this.cboPlacas.FormattingEnabled = true;
            this.cboPlacas.Location = new System.Drawing.Point(169, 28);
            this.cboPlacas.Name = "cboPlacas";
            this.cboPlacas.Size = new System.Drawing.Size(100, 21);
            this.cboPlacas.TabIndex = 5;
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
            // printPreviewControl
            // 
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 56);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(968, 389);
            this.printPreviewControl.TabIndex = 3;
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.Location = new System.Drawing.Point(3, 26);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(75, 23);
            this.btnVisualizar.TabIndex = 9;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.UseVisualStyleBackColor = true;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // intCopias
            // 
            this.intCopias.Location = new System.Drawing.Point(137, 3);
            this.intCopias.Name = "intCopias";
            this.intCopias.Size = new System.Drawing.Size(28, 22);
            this.intCopias.TabIndex = 3;
            this.intCopias.Leave += new System.EventHandler(this.intCopias_Leave);
            // 
            // frmVisualPrintMTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 445);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.panel1);
            this.Name = "frmVisualPrintMTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Visualizador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVisualPrintMTR_Load);
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
        private System.Windows.Forms.ComboBox cboMotorista;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPlacas;
        private System.Windows.Forms.Button btnVisualizar;
    }
}