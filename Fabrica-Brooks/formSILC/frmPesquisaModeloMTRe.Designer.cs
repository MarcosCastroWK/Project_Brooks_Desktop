
namespace formSILC
{
    partial class frmPesquisaModeloMTRe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbItem1 = new System.Windows.Forms.GroupBox();
            this.grvModelos = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDescricaoResiduo = new System.Windows.Forms.TextBox();
            this.butOk = new System.Windows.Forms.Button();
            this.lblPesquisaDescricaoResiduo = new System.Windows.Forms.Label();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF_Transportador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF_Destinador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NossoCodigoDestinador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoResiduo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIBAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ_CPF_Armazenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbItem1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvModelos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbItem1
            // 
            this.grbItem1.Controls.Add(this.grvModelos);
            this.grbItem1.Controls.Add(this.panel1);
            this.grbItem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbItem1.Location = new System.Drawing.Point(0, 0);
            this.grbItem1.Name = "grbItem1";
            this.grbItem1.Size = new System.Drawing.Size(1506, 553);
            this.grbItem1.TabIndex = 5;
            this.grbItem1.TabStop = false;
            this.grbItem1.Text = "Modelo(s) existente(s)";
            // 
            // grvModelos
            // 
            this.grvModelos.AllowUserToAddRows = false;
            this.grvModelos.AllowUserToDeleteRows = false;
            this.grvModelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvModelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nome,
            this.CNPJ_CPF_Transportador,
            this.CNPJ_CPF_Destinador,
            this.NossoCodigoDestinador,
            this.CodigoResiduo,
            this.CodigoIBAMA,
            this.CNPJ_CPF_Armazenador});
            this.grvModelos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvModelos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grvModelos.Location = new System.Drawing.Point(3, 61);
            this.grvModelos.Name = "grvModelos";
            this.grvModelos.Size = new System.Drawing.Size(1500, 489);
            this.grvModelos.TabIndex = 5;
            this.grvModelos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvModelos_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDescricaoResiduo);
            this.panel1.Controls.Add(this.butOk);
            this.panel1.Controls.Add(this.lblPesquisaDescricaoResiduo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1500, 42);
            this.panel1.TabIndex = 4;
            // 
            // txtDescricaoResiduo
            // 
            this.txtDescricaoResiduo.Location = new System.Drawing.Point(137, 9);
            this.txtDescricaoResiduo.Name = "txtDescricaoResiduo";
            this.txtDescricaoResiduo.Size = new System.Drawing.Size(222, 23);
            this.txtDescricaoResiduo.TabIndex = 2;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(366, 8);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(35, 26);
            this.butOk.TabIndex = 3;
            this.butOk.Text = "Ok";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // lblPesquisaDescricaoResiduo
            // 
            this.lblPesquisaDescricaoResiduo.AutoSize = true;
            this.lblPesquisaDescricaoResiduo.Location = new System.Drawing.Point(6, 12);
            this.lblPesquisaDescricaoResiduo.Name = "lblPesquisaDescricaoResiduo";
            this.lblPesquisaDescricaoResiduo.Size = new System.Drawing.Size(131, 17);
            this.lblPesquisaDescricaoResiduo.TabIndex = 1;
            this.lblPesquisaDescricaoResiduo.Text = "Descrição Resíduo:";
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Codigo.DefaultCellStyle = dataGridViewCellStyle1;
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 60;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome do Modelo";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 300;
            // 
            // CNPJ_CPF_Transportador
            // 
            this.CNPJ_CPF_Transportador.DataPropertyName = "CNPJ_CPF_Transportador";
            this.CNPJ_CPF_Transportador.HeaderText = "CNPJ_CPF Transportador";
            this.CNPJ_CPF_Transportador.Name = "CNPJ_CPF_Transportador";
            this.CNPJ_CPF_Transportador.ReadOnly = true;
            this.CNPJ_CPF_Transportador.Width = 300;
            // 
            // CNPJ_CPF_Destinador
            // 
            this.CNPJ_CPF_Destinador.DataPropertyName = "CNPJ_CPF_Destinador";
            this.CNPJ_CPF_Destinador.HeaderText = "CNPJ/CPF Destinador";
            this.CNPJ_CPF_Destinador.Name = "CNPJ_CPF_Destinador";
            this.CNPJ_CPF_Destinador.Width = 200;
            // 
            // NossoCodigoDestinador
            // 
            this.NossoCodigoDestinador.DataPropertyName = "CodigoDestinoFinal";
            this.NossoCodigoDestinador.HeaderText = "Nosso Cd.Destinador";
            this.NossoCodigoDestinador.Name = "NossoCodigoDestinador";
            // 
            // CodigoResiduo
            // 
            this.CodigoResiduo.DataPropertyName = "CodigoResiduo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CodigoResiduo.DefaultCellStyle = dataGridViewCellStyle2;
            this.CodigoResiduo.HeaderText = "CodigoResiduo";
            this.CodigoResiduo.Name = "CodigoResiduo";
            this.CodigoResiduo.Width = 120;
            // 
            // CodigoIBAMA
            // 
            this.CodigoIBAMA.DataPropertyName = "CodigoIBAMA";
            this.CodigoIBAMA.HeaderText = "Codigo IBAMA";
            this.CodigoIBAMA.Name = "CodigoIBAMA";
            this.CodigoIBAMA.Width = 120;
            // 
            // CNPJ_CPF_Armazenador
            // 
            this.CNPJ_CPF_Armazenador.DataPropertyName = "CNPJ_CPF_Armazenador";
            this.CNPJ_CPF_Armazenador.HeaderText = "CNPJ_CPF_Armazenador";
            this.CNPJ_CPF_Armazenador.Name = "CNPJ_CPF_Armazenador";
            // 
            // frmPesquisaModeloMTRe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1506, 553);
            this.Controls.Add(this.grbItem1);
            this.Name = "frmPesquisaModeloMTRe";
            this.Text = "Pesquisa Modelo MTR Eletrônica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPesquisaModeloMTRe_Load);
            this.grbItem1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvModelos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbItem1;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.TextBox txtDescricaoResiduo;
        private System.Windows.Forms.Label lblPesquisaDescricaoResiduo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grvModelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF_Transportador;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF_Destinador;
        private System.Windows.Forms.DataGridViewTextBoxColumn NossoCodigoDestinador;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoResiduo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIBAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ_CPF_Armazenador;
    }
}