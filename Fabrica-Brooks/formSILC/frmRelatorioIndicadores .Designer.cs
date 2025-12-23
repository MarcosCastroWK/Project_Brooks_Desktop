namespace formSILC
{
    partial class frmRelatorioIndicadores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelatorioIndicadores));
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pdialog = new System.Windows.Forms.PrintDialog();
            this.DataMostragemFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataMostragemInicial = new System.Windows.Forms.DateTimePicker();
            this.rdbMensal = new System.Windows.Forms.RadioButton();
            this.rdpAnual = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.BackColor = System.Drawing.Color.Silver;
            this.btnVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualizar.Image")));
            this.btnVisualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisualizar.Location = new System.Drawing.Point(88, 12);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(78, 39);
            this.btnVisualizar.TabIndex = 159;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVisualizar.UseVisualStyleBackColor = false;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Silver;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 12);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(74, 39);
            this.btnImprimir.TabIndex = 158;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // ppd
            // 
            this.ppd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppd.ClientSize = new System.Drawing.Size(973, 604);
            this.ppd.Enabled = true;
            this.ppd.Icon = ((System.Drawing.Icon)(resources.GetObject("ppd.Icon")));
            this.ppd.Name = "ppd";
            this.ppd.Visible = false;
            // 
            // pdialog
            // 
            this.pdialog.UseEXDialog = true;
            // 
            // DataMostragemFinal
            // 
            this.DataMostragemFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataMostragemFinal.Location = new System.Drawing.Point(222, 62);
            this.DataMostragemFinal.Name = "DataMostragemFinal";
            this.DataMostragemFinal.Size = new System.Drawing.Size(86, 20);
            this.DataMostragemFinal.TabIndex = 163;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 162;
            this.label3.Text = "Data final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 161;
            this.label2.Text = "Data inicial";
            // 
            // DataMostragemInicial
            // 
            this.DataMostragemInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataMostragemInicial.Location = new System.Drawing.Point(72, 60);
            this.DataMostragemInicial.Name = "DataMostragemInicial";
            this.DataMostragemInicial.Size = new System.Drawing.Size(86, 20);
            this.DataMostragemInicial.TabIndex = 160;
            // 
            // rdbMensal
            // 
            this.rdbMensal.AutoSize = true;
            this.rdbMensal.Checked = true;
            this.rdbMensal.Location = new System.Drawing.Point(324, 64);
            this.rdbMensal.Name = "rdbMensal";
            this.rdbMensal.Size = new System.Drawing.Size(59, 17);
            this.rdbMensal.TabIndex = 164;
            this.rdbMensal.TabStop = true;
            this.rdbMensal.Text = "Mensal";
            this.rdbMensal.UseVisualStyleBackColor = true;
            this.rdbMensal.Visible = false;
            // 
            // rdpAnual
            // 
            this.rdpAnual.AutoSize = true;
            this.rdpAnual.Location = new System.Drawing.Point(388, 64);
            this.rdpAnual.Name = "rdpAnual";
            this.rdpAnual.Size = new System.Drawing.Size(52, 17);
            this.rdpAnual.TabIndex = 165;
            this.rdpAnual.Text = "Anual";
            this.rdpAnual.UseVisualStyleBackColor = true;
            this.rdpAnual.Visible = false;
            // 
            // frmRelatorioIndicadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 92);
            this.Controls.Add(this.rdpAnual);
            this.Controls.Add(this.rdbMensal);
            this.Controls.Add(this.DataMostragemFinal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DataMostragemInicial);
            this.Controls.Add(this.btnVisualizar);
            this.Controls.Add(this.btnImprimir);
            this.Name = "frmRelatorioIndicadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Indicadores";
            this.Load += new System.EventHandler(this.FrmRelatorioIndicadores_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnVisualizar;
        public System.Windows.Forms.Button btnImprimir;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Windows.Forms.PrintDialog pdialog;
        private System.Windows.Forms.DateTimePicker DataMostragemFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DataMostragemInicial;
        private System.Windows.Forms.RadioButton rdbMensal;
        private System.Windows.Forms.RadioButton rdpAnual;
    }
}