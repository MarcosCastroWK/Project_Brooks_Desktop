namespace formSILC
{
    partial class frmServicosRealizados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNomeFantasia = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNrCodigo = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.Grade1 = new System.Windows.Forms.DataGridView();
            this.butOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butOk);
            this.panel1.Controls.Add(this.dtpFinal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpDataInicial);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNomeFantasia);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblNrCodigo);
            this.panel1.Controls.Add(this.lblCodigo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1126, 63);
            this.panel1.TabIndex = 24;
            // 
            // dtpFinal
            // 
            this.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinal.Location = new System.Drawing.Point(644, 33);
            this.dtpFinal.Name = "dtpFinal";
            this.dtpFinal.Size = new System.Drawing.Size(102, 20);
            this.dtpFinal.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(625, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 29;
            this.label2.Text = "a";
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(519, 33);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(102, 20);
            this.dtpDataInicial.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(470, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Período:";
            // 
            // txtNomeFantasia
            // 
            this.txtNomeFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeFantasia.Enabled = false;
            this.txtNomeFantasia.Location = new System.Drawing.Point(172, 33);
            this.txtNomeFantasia.Name = "txtNomeFantasia";
            this.txtNomeFantasia.Size = new System.Drawing.Size(267, 20);
            this.txtNomeFantasia.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Nome Fantasia/Primeiro Nome:";
            // 
            // lblNrCodigo
            // 
            this.lblNrCodigo.AutoSize = true;
            this.lblNrCodigo.Location = new System.Drawing.Point(61, 11);
            this.lblNrCodigo.Name = "lblNrCodigo";
            this.lblNrCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblNrCodigo.TabIndex = 24;
            this.lblNrCodigo.Text = "000000";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 11);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblCodigo.TabIndex = 23;
            this.lblCodigo.Text = "Código:";
            // 
            // Grade1
            // 
            this.Grade1.AllowUserToAddRows = false;
            this.Grade1.AllowUserToDeleteRows = false;
            this.Grade1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Grade1.Location = new System.Drawing.Point(0, 63);
            this.Grade1.Name = "Grade1";
            this.Grade1.ReadOnly = true;
            this.Grade1.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Grade1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grade1.Size = new System.Drawing.Size(1126, 311);
            this.Grade1.TabIndex = 26;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(751, 32);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(29, 23);
            this.butOk.TabIndex = 31;
            this.butOk.Text = "Ok";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // frmServicosRealizados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 376);
            this.Controls.Add(this.Grade1);
            this.Controls.Add(this.panel1);
            this.Name = "frmServicosRealizados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serviços realizados no Cliente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmServicosRealizados_Load);
            this.Resize += new System.EventHandler(this.frmServicosRealizados_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtNomeFantasia;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label lblNrCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.DataGridView Grade1;
        private System.Windows.Forms.DateTimePicker dtpFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butOk;
    }
}