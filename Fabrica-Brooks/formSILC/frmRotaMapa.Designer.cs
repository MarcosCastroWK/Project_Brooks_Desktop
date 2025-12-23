namespace formSILC
{
    partial class frmRotaMapa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRotaMapa));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkImprimirFranquia = new System.Windows.Forms.CheckBox();
            this.btnLimpaOrdem = new System.Windows.Forms.Button();
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMensagemMotorista = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pdialog = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkImprimirFranquia);
            this.panel1.Controls.Add(this.btnLimpaOrdem);
            this.panel1.Controls.Add(this.btnVisualizar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1255, 44);
            this.panel1.TabIndex = 12;
            // 
            // chkImprimirFranquia
            // 
            this.chkImprimirFranquia.AutoSize = true;
            this.chkImprimirFranquia.Location = new System.Drawing.Point(215, 12);
            this.chkImprimirFranquia.Name = "chkImprimirFranquia";
            this.chkImprimirFranquia.Size = new System.Drawing.Size(102, 17);
            this.chkImprimirFranquia.TabIndex = 162;
            this.chkImprimirFranquia.Text = "Imprimir franquia";
            this.chkImprimirFranquia.UseVisualStyleBackColor = true;
            // 
            // btnLimpaOrdem
            // 
            this.btnLimpaOrdem.Location = new System.Drawing.Point(1177, 15);
            this.btnLimpaOrdem.Name = "btnLimpaOrdem";
            this.btnLimpaOrdem.Size = new System.Drawing.Size(75, 23);
            this.btnLimpaOrdem.TabIndex = 161;
            this.btnLimpaOrdem.Text = "Limpa ordem";
            this.btnLimpaOrdem.UseVisualStyleBackColor = true;
            this.btnLimpaOrdem.Visible = false;
            this.btnLimpaOrdem.Click += new System.EventHandler(this.btnLimpaOrdem_Click);
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.BackColor = System.Drawing.Color.Silver;
            this.btnVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualizar.Image")));
            this.btnVisualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisualizar.Location = new System.Drawing.Point(91, 3);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(84, 33);
            this.btnVisualizar.TabIndex = 160;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVisualizar.UseVisualStyleBackColor = false;
            this.btnVisualizar.Visible = false;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(0, 3);
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
            this.panel2.Controls.Add(this.txtMensagemMotorista);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1255, 70);
            this.panel2.TabIndex = 13;
            // 
            // txtMensagemMotorista
            // 
            this.txtMensagemMotorista.BackColor = System.Drawing.Color.Khaki;
            this.txtMensagemMotorista.Location = new System.Drawing.Point(151, 24);
            this.txtMensagemMotorista.Name = "txtMensagemMotorista";
            this.txtMensagemMotorista.Size = new System.Drawing.Size(763, 20);
            this.txtMensagemMotorista.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mensagem para o motorista:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Grade);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1255, 301);
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
            this.Grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grade.Location = new System.Drawing.Point(0, 0);
            this.Grade.MultiSelect = false;
            this.Grade.Name = "Grade";
            this.Grade.ReadOnly = true;
            this.Grade.RowHeadersVisible = false;
            this.Grade.Size = new System.Drawing.Size(1255, 301);
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
            // frmRotaMapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 415);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmRotaMapa";
            this.Text = "Rota Mapa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRotaMapa_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
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
        private System.Windows.Forms.Button btnLimpaOrdem;
        private System.Windows.Forms.TextBox txtMensagemMotorista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkImprimirFranquia;
    }
}