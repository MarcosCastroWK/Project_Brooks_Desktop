namespace formSILC
{
    partial class frmProcura
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtNomeDescricao = new System.Windows.Forms.TextBox();
            this.Grade = new System.Windows.Forms.DataGridView();
            this.cboNomeDescricao = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboNomeDescricao);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.txtNomeDescricao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 40);
            this.panel1.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(410, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(54, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtNomeDescricao
            // 
            this.txtNomeDescricao.Location = new System.Drawing.Point(136, 12);
            this.txtNomeDescricao.Name = "txtNomeDescricao";
            this.txtNomeDescricao.Size = new System.Drawing.Size(270, 20);
            this.txtNomeDescricao.TabIndex = 0;
            // 
            // Grade
            // 
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grade.Location = new System.Drawing.Point(0, 40);
            this.Grade.MultiSelect = false;
            this.Grade.Name = "Grade";
            this.Grade.ReadOnly = true;
            this.Grade.RowHeadersVisible = false;
            this.Grade.Size = new System.Drawing.Size(989, 401);
            this.Grade.TabIndex = 8;
            this.Grade.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellContentDoubleClick);
            this.Grade.Sorted += new System.EventHandler(this.Grade_Sorted);
            this.Grade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grade_KeyUp);
            // 
            // cboNomeDescricao
            // 
            this.cboNomeDescricao.FormattingEnabled = true;
            this.cboNomeDescricao.Location = new System.Drawing.Point(4, 11);
            this.cboNomeDescricao.Name = "cboNomeDescricao";
            this.cboNomeDescricao.Size = new System.Drawing.Size(126, 21);
            this.cboNomeDescricao.TabIndex = 7;
            this.cboNomeDescricao.SelectedIndexChanged += new System.EventHandler(this.cboNomeDescricao_SelectedIndexChanged);
            // 
            // frmProcura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 441);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.panel1);
            this.Name = "frmProcura";
            this.Text = "Procura";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProcura_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtNomeDescricao;
        private System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.ComboBox cboNomeDescricao;
    }
}