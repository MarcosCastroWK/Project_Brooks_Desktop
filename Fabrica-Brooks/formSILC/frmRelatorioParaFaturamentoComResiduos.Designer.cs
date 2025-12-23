namespace formSILC
{
    partial class frmRelatorioParaFaturamentoComResiduos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelatorioParaFaturamentoComResiduos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pdialog = new System.Windows.Forms.PrintDialog();
            this.lblDataInicial = new System.Windows.Forms.Label();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.GradeNFs = new System.Windows.Forms.DataGridView();
            this.GradeContrato = new System.Windows.Forms.DataGridView();
            this.cliente1 = new formSILC.CLIENTE();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeNFs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeContrato)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cliente1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDataFinal);
            this.panel1.Controls.Add(this.dtpDataInicial);
            this.panel1.Controls.Add(this.lblDataInicial);
            this.panel1.Controls.Add(this.btnVisualizar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1255, 44);
            this.panel1.TabIndex = 12;
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.BackColor = System.Drawing.Color.Silver;
            this.btnVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualizar.Image")));
            this.btnVisualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisualizar.Location = new System.Drawing.Point(88, 3);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(84, 33);
            this.btnVisualizar.TabIndex = 160;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVisualizar.UseVisualStyleBackColor = false;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(2, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(84, 33);
            this.btnImprimir.TabIndex = 12;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 407);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1255, 27);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.GradeContrato);
            this.panel3.Controls.Add(this.GradeNFs);
            this.panel3.Controls.Add(this.Grade);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1255, 363);
            this.panel3.TabIndex = 14;
            // 
            // Grade
            // 
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grade.DefaultCellStyle = dataGridViewCellStyle1;
            this.Grade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Grade.Location = new System.Drawing.Point(0, 96);
            this.Grade.MultiSelect = false;
            this.Grade.Name = "Grade";
            this.Grade.ReadOnly = true;
            this.Grade.RowHeadersVisible = false;
            this.Grade.Size = new System.Drawing.Size(1255, 267);
            this.Grade.TabIndex = 11;
            this.Grade.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellContentClick);
            this.Grade.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellDoubleClick);
            this.Grade.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grade_ColumnHeaderMouseClick);
            this.Grade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade_KeyUp);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // ppd
            // 
            this.ppd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppd.ClientSize = new System.Drawing.Size(400, 300);
            this.ppd.Enabled = true;
            this.ppd.Icon = ((System.Drawing.Icon)(resources.GetObject("ppd.Icon")));
            this.ppd.Name = "ppd";
            this.ppd.Visible = false;
            // 
            // pdialog
            // 
            this.pdialog.UseEXDialog = true;
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.AutoSize = true;
            this.lblDataInicial.Location = new System.Drawing.Point(180, 16);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(63, 13);
            this.lblDataInicial.TabIndex = 163;
            this.lblDataInicial.Text = "Data Inicial:";
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(242, 13);
            this.dtpDataInicial.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.dtpDataInicial.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpDataInicial.TabIndex = 0;
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(403, 13);
            this.dtpDataFinal.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.dtpDataFinal.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(101, 20);
            this.dtpDataFinal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(345, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 166;
            this.label1.Text = "Data Final:";
            // 
            // GradeNFs
            // 
            this.GradeNFs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeNFs.Dock = System.Windows.Forms.DockStyle.Right;
            this.GradeNFs.Location = new System.Drawing.Point(845, 0);
            this.GradeNFs.Name = "GradeNFs";
            this.GradeNFs.Size = new System.Drawing.Size(410, 96);
            this.GradeNFs.TabIndex = 12;
            // 
            // GradeContrato
            // 
            this.GradeContrato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GradeContrato.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GradeContrato.Location = new System.Drawing.Point(0, 17);
            this.GradeContrato.Name = "GradeContrato";
            this.GradeContrato.Size = new System.Drawing.Size(845, 79);
            this.GradeContrato.TabIndex = 13;
            // 
            // cliente1
            // 
            this.cliente1.Location = new System.Drawing.Point(508, 13);
            this.cliente1.Name = "cliente1";
            this.cliente1.Size = new System.Drawing.Size(493, 22);
            this.cliente1.TabIndex = 167;
            this.cliente1.TabIndexCodigo = 0;
            // 
            // frmRelatorioParaFaturamentoComResiduos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 434);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmRelatorioParaFaturamentoComResiduos";
            this.Text = "Relatório para Faturamento com Resíduos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRelatorioParaFaturamentoComResiduos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeNFs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradeContrato)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView Grade;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Windows.Forms.PrintDialog pdialog;
        public System.Windows.Forms.Button btnVisualizar;
        private System.Windows.Forms.Label lblDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private CLIENTE cliente1;
        private System.Windows.Forms.DataGridView GradeNFs;
        private System.Windows.Forms.DataGridView GradeContrato;
    }
}