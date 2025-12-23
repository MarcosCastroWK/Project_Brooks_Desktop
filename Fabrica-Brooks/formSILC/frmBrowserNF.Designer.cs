namespace formSILC
{
    partial class frmBrowserNF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrowserNF));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnviarParaRadar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRetorno = new System.Windows.Forms.RichTextBox();
            this.butConsultaNF = new System.Windows.Forms.Button();
            this.intNNF = new formSILC.INTEIRO();
            this.butNavegadorChrome = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Númro NF:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butNavegadorChrome);
            this.panel1.Controls.Add(this.btnEnviarParaRadar);
            this.panel1.Controls.Add(this.btnExcluir);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.butConsultaNF);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.intNNF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 42);
            this.panel1.TabIndex = 4;
            // 
            // btnEnviarParaRadar
            // 
            this.btnEnviarParaRadar.BackColor = System.Drawing.Color.Silver;
            this.btnEnviarParaRadar.Location = new System.Drawing.Point(570, 3);
            this.btnEnviarParaRadar.Name = "btnEnviarParaRadar";
            this.btnEnviarParaRadar.Size = new System.Drawing.Size(176, 35);
            this.btnEnviarParaRadar.TabIndex = 173;
            this.btnEnviarParaRadar.TabStop = false;
            this.btnEnviarParaRadar.Text = "NF Conferida - Enviar para Radar";
            this.btnEnviarParaRadar.UseVisualStyleBackColor = false;
            this.btnEnviarParaRadar.Visible = false;
            this.btnEnviarParaRadar.Click += new System.EventHandler(this.btnEnviarParaRadar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(748, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(69, 35);
            this.btnExcluir.TabIndex = 172;
            this.btnExcluir.TabStop = false;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Caminho do arquivo rps.xml:  c:\\eletron\\work";
            // 
            // txtRetorno
            // 
            this.txtRetorno.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtRetorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetorno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRetorno.Location = new System.Drawing.Point(0, 42);
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.Size = new System.Drawing.Size(1011, 730);
            this.txtRetorno.TabIndex = 7;
            this.txtRetorno.Text = "";
            // 
            // butConsultaNF
            // 
            this.butConsultaNF.Location = new System.Drawing.Point(162, 9);
            this.butConsultaNF.Name = "butConsultaNF";
            this.butConsultaNF.Size = new System.Drawing.Size(75, 23);
            this.butConsultaNF.TabIndex = 1;
            this.butConsultaNF.Text = "Consulta NF";
            this.butConsultaNF.UseVisualStyleBackColor = true;
            this.butConsultaNF.Click += new System.EventHandler(this.butConsultaNF_Click);
            // 
            // intNNF
            // 
            this.intNNF.Location = new System.Drawing.Point(64, 9);
            this.intNNF.Name = "intNNF";
            this.intNNF.Size = new System.Drawing.Size(97, 23);
            this.intNNF.TabIndex = 2;
            // 
            // butNavegadorChrome
            // 
            this.butNavegadorChrome.Location = new System.Drawing.Point(834, 9);
            this.butNavegadorChrome.Name = "butNavegadorChrome";
            this.butNavegadorChrome.Size = new System.Drawing.Size(75, 23);
            this.butNavegadorChrome.TabIndex = 174;
            this.butNavegadorChrome.Text = "Abrir Chrome";
            this.butNavegadorChrome.UseVisualStyleBackColor = true;
            this.butNavegadorChrome.Click += new System.EventHandler(this.butNavegadorChrome_Click);
            // 
            // frmBrowserNF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 772);
            this.ControlBox = false;
            this.Controls.Add(this.txtRetorno);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBrowserNF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota Fiscal no IPM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBrowserNF_FormClosing);
            this.Load += new System.EventHandler(this.frmBrowserNF_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public  INTEIRO intNNF;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnExcluir;
        public System.Windows.Forms.Button btnEnviarParaRadar;
        private System.Windows.Forms.RichTextBox txtRetorno;
        public System.Windows.Forms.Button butConsultaNF;
        private System.Windows.Forms.Button butNavegadorChrome;
    }
}