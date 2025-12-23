namespace formSILC
{
    partial class frmDocumentacaoAplicavel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentacaoAplicavel));
            this.Grade = new System.Windows.Forms.DataGridView();
            this.Sequencial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeriodoApuracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnviarPlanFatAteDia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AguardarAprovacaoPlanFat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANFATEnviada = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AguardarOrdemCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConferirDDRAteDia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDRConFerida = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DDR_Conferindo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EnviarRGRAteDia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RELGEREnviado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Ok = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimirSelecionados = new System.Windows.Forms.Button();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.btnSelecionarOrdemDDR = new System.Windows.Forms.Button();
            this.btnSelecionarOrdemPlanFat = new System.Windows.Forms.Button();
            this.btnSelecionarRGR = new System.Windows.Forms.Button();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.btnOkNome = new System.Windows.Forms.Button();
            this.cboFiltro = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkMostrarPeriodos = new System.Windows.Forms.CheckBox();
            this.lblLinhas = new System.Windows.Forms.Label();
            this.int2Ano = new formSILC.INTEIRO2();
            this.int2Mes = new formSILC.INTEIRO2();
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // Grade
            // 
            this.Grade.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sequencial,
            this.CodigoCliente,
            this.NomeFantasia,
            this.Mes,
            this.Ano,
            this.PeriodoApuracao,
            this.EnviarPlanFatAteDia,
            this.AguardarAprovacaoPlanFat,
            this.PLANFATEnviada,
            this.AguardarOrdemCompra,
            this.ConferirDDRAteDia,
            this.DDRConFerida,
            this.DDR_Conferindo,
            this.EnviarRGRAteDia,
            this.RELGEREnviado,
            this.Ok});
            this.Grade.Location = new System.Drawing.Point(12, 108);
            this.Grade.Name = "Grade";
            this.Grade.RowHeadersWidth = 20;
            this.Grade.Size = new System.Drawing.Size(1117, 362);
            this.Grade.TabIndex = 36;
            this.Grade.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grade_CellBeginEdit);
            this.Grade.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grade_CellContentClick);
            this.Grade.CurrentCellDirtyStateChanged += new System.EventHandler(this.Grade_CurrentCellDirtyStateChanged);
            // 
            // Sequencial
            // 
            this.Sequencial.DataPropertyName = "Sequencial";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Sequencial.DefaultCellStyle = dataGridViewCellStyle1;
            this.Sequencial.HeaderText = "Sequencial";
            this.Sequencial.Name = "Sequencial";
            this.Sequencial.ReadOnly = true;
            this.Sequencial.Width = 70;
            // 
            // CodigoCliente
            // 
            this.CodigoCliente.DataPropertyName = "CodigoCliente";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CodigoCliente.DefaultCellStyle = dataGridViewCellStyle2;
            this.CodigoCliente.HeaderText = "Código Cliente";
            this.CodigoCliente.Name = "CodigoCliente";
            this.CodigoCliente.ReadOnly = true;
            this.CodigoCliente.Width = 70;
            // 
            // NomeFantasia
            // 
            this.NomeFantasia.DataPropertyName = "NomeFantasia";
            this.NomeFantasia.HeaderText = "Nome fantasia";
            this.NomeFantasia.Name = "NomeFantasia";
            this.NomeFantasia.ReadOnly = true;
            this.NomeFantasia.Width = 200;
            // 
            // Mes
            // 
            this.Mes.DataPropertyName = "Mes";
            this.Mes.HeaderText = "Mês";
            this.Mes.Name = "Mes";
            this.Mes.ReadOnly = true;
            this.Mes.Width = 30;
            // 
            // Ano
            // 
            this.Ano.DataPropertyName = "Ano";
            this.Ano.HeaderText = "Ano";
            this.Ano.Name = "Ano";
            this.Ano.ReadOnly = true;
            this.Ano.Width = 35;
            // 
            // PeriodoApuracao
            // 
            this.PeriodoApuracao.DataPropertyName = "PeriodoApuracao";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PeriodoApuracao.DefaultCellStyle = dataGridViewCellStyle3;
            this.PeriodoApuracao.HeaderText = "Período Apuração";
            this.PeriodoApuracao.Name = "PeriodoApuracao";
            this.PeriodoApuracao.Width = 80;
            // 
            // EnviarPlanFatAteDia
            // 
            this.EnviarPlanFatAteDia.DataPropertyName = "EnviarPlanFatAteDia";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EnviarPlanFatAteDia.DefaultCellStyle = dataGridViewCellStyle4;
            this.EnviarPlanFatAteDia.HeaderText = "Enviar PlanFat até dia";
            this.EnviarPlanFatAteDia.Name = "EnviarPlanFatAteDia";
            this.EnviarPlanFatAteDia.Width = 60;
            // 
            // AguardarAprovacaoPlanFat
            // 
            this.AguardarAprovacaoPlanFat.DataPropertyName = "AguardarAprovacaoPlanFat";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AguardarAprovacaoPlanFat.DefaultCellStyle = dataGridViewCellStyle5;
            this.AguardarAprovacaoPlanFat.HeaderText = "Aguardar Aprovação PlanFat";
            this.AguardarAprovacaoPlanFat.Name = "AguardarAprovacaoPlanFat";
            this.AguardarAprovacaoPlanFat.Width = 60;
            // 
            // PLANFATEnviada
            // 
            this.PLANFATEnviada.DataPropertyName = "PlanFatEnviada";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.NullValue = false;
            this.PLANFATEnviada.DefaultCellStyle = dataGridViewCellStyle6;
            this.PLANFATEnviada.HeaderText = "PlanFat Enviada";
            this.PLANFATEnviada.Name = "PLANFATEnviada";
            this.PLANFATEnviada.Width = 60;
            // 
            // AguardarOrdemCompra
            // 
            this.AguardarOrdemCompra.DataPropertyName = "AguardarOrdemCompra";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AguardarOrdemCompra.DefaultCellStyle = dataGridViewCellStyle7;
            this.AguardarOrdemCompra.HeaderText = "Aguardar Ordem Compra";
            this.AguardarOrdemCompra.Name = "AguardarOrdemCompra";
            this.AguardarOrdemCompra.Width = 60;
            // 
            // ConferirDDRAteDia
            // 
            this.ConferirDDRAteDia.DataPropertyName = "ConferirDDRAteDia";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ConferirDDRAteDia.DefaultCellStyle = dataGridViewCellStyle8;
            this.ConferirDDRAteDia.HeaderText = "Conferir DDR Até dia";
            this.ConferirDDRAteDia.Name = "ConferirDDRAteDia";
            this.ConferirDDRAteDia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ConferirDDRAteDia.Width = 60;
            // 
            // DDRConFerida
            // 
            this.DDRConFerida.DataPropertyName = "DDRConferida";
            this.DDRConFerida.HeaderText = "DDR Conferida";
            this.DDRConFerida.Name = "DDRConFerida";
            this.DDRConFerida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DDRConFerida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DDRConFerida.Width = 60;
            // 
            // DDR_Conferindo
            // 
            this.DDR_Conferindo.DataPropertyName = "DDR_Conferindo";
            this.DDR_Conferindo.HeaderText = "DDR Liberada";
            this.DDR_Conferindo.Name = "DDR_Conferindo";
            this.DDR_Conferindo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DDR_Conferindo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DDR_Conferindo.Width = 70;
            // 
            // EnviarRGRAteDia
            // 
            this.EnviarRGRAteDia.DataPropertyName = "EnviarRGRAteDia";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EnviarRGRAteDia.DefaultCellStyle = dataGridViewCellStyle9;
            this.EnviarRGRAteDia.HeaderText = "Enviar RGR Até dia";
            this.EnviarRGRAteDia.Name = "EnviarRGRAteDia";
            this.EnviarRGRAteDia.Width = 60;
            // 
            // RELGEREnviado
            // 
            this.RELGEREnviado.DataPropertyName = "RelGerEnviado";
            this.RELGEREnviado.HeaderText = "RGR Enviada";
            this.RELGEREnviado.Name = "RELGEREnviado";
            this.RELGEREnviado.Width = 50;
            // 
            // Ok
            // 
            this.Ok.DataPropertyName = "Ok";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(1);
            this.Ok.DefaultCellStyle = dataGridViewCellStyle10;
            this.Ok.HeaderText = "Ok";
            this.Ok.Name = "Ok";
            this.Ok.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ok.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Ok.Text = "Ok";
            this.Ok.Width = 40;
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.Location = new System.Drawing.Point(9, 49);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 35;
            this.lblMensagem.Text = "Mensagem";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(81, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(32, 25);
            this.btnOk.TabIndex = 32;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Período";
            // 
            // btnImprimirSelecionados
            // 
            this.btnImprimirSelecionados.Location = new System.Drawing.Point(568, 18);
            this.btnImprimirSelecionados.Name = "btnImprimirSelecionados";
            this.btnImprimirSelecionados.Size = new System.Drawing.Size(135, 32);
            this.btnImprimirSelecionados.TabIndex = 40;
            this.btnImprimirSelecionados.Text = "Imprimir Selecionados";
            this.btnImprimirSelecionados.UseVisualStyleBackColor = true;
            this.btnImprimirSelecionados.Click += new System.EventHandler(this.btnImprimirSelecionados_Click);
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
            this.ppd.Load += new System.EventHandler(this.ppd_Load);
            // 
            // btnSelecionarOrdemDDR
            // 
            this.btnSelecionarOrdemDDR.Location = new System.Drawing.Point(840, 18);
            this.btnSelecionarOrdemDDR.Name = "btnSelecionarOrdemDDR";
            this.btnSelecionarOrdemDDR.Size = new System.Drawing.Size(131, 32);
            this.btnSelecionarOrdemDDR.TabIndex = 41;
            this.btnSelecionarOrdemDDR.Text = "Selecionar Ordem DDR";
            this.btnSelecionarOrdemDDR.UseVisualStyleBackColor = true;
            this.btnSelecionarOrdemDDR.Click += new System.EventHandler(this.btnSelecionarOrdemDDR_Click);
            // 
            // btnSelecionarOrdemPlanFat
            // 
            this.btnSelecionarOrdemPlanFat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarOrdemPlanFat.Location = new System.Drawing.Point(702, 18);
            this.btnSelecionarOrdemPlanFat.Name = "btnSelecionarOrdemPlanFat";
            this.btnSelecionarOrdemPlanFat.Size = new System.Drawing.Size(139, 32);
            this.btnSelecionarOrdemPlanFat.TabIndex = 42;
            this.btnSelecionarOrdemPlanFat.Text = "Selecionar Ordem PlanFat";
            this.btnSelecionarOrdemPlanFat.UseVisualStyleBackColor = true;
            this.btnSelecionarOrdemPlanFat.Click += new System.EventHandler(this.btnSelecionarOrdemPlanFat_Click);
            // 
            // btnSelecionarRGR
            // 
            this.btnSelecionarRGR.Location = new System.Drawing.Point(970, 18);
            this.btnSelecionarRGR.Name = "btnSelecionarRGR";
            this.btnSelecionarRGR.Size = new System.Drawing.Size(135, 32);
            this.btnSelecionarRGR.TabIndex = 43;
            this.btnSelecionarRGR.Text = "Selecionar Ordem RGR";
            this.btnSelecionarRGR.UseVisualStyleBackColor = true;
            this.btnSelecionarRGR.Click += new System.EventHandler(this.btnSelecionarRGR_Click);
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Location = new System.Drawing.Point(159, 81);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(140, 20);
            this.txtNomeCliente.TabIndex = 45;
            // 
            // btnOkNome
            // 
            this.btnOkNome.Location = new System.Drawing.Point(303, 81);
            this.btnOkNome.Name = "btnOkNome";
            this.btnOkNome.Size = new System.Drawing.Size(34, 25);
            this.btnOkNome.TabIndex = 46;
            this.btnOkNome.Text = "Ok";
            this.btnOkNome.UseVisualStyleBackColor = true;
            this.btnOkNome.Click += new System.EventHandler(this.btnOkNome_Click);
            // 
            // cboFiltro
            // 
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.Items.AddRange(new object[] {
            "Código",
            "Nome",
            "NomeFantasia",
            "CNPJ_CPF"});
            this.cboFiltro.Location = new System.Drawing.Point(47, 81);
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(109, 21);
            this.cboFiltro.TabIndex = 47;
            this.cboFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboFiltro_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Filtro:";
            // 
            // chkMostrarPeriodos
            // 
            this.chkMostrarPeriodos.AutoSize = true;
            this.chkMostrarPeriodos.Location = new System.Drawing.Point(355, 85);
            this.chkMostrarPeriodos.Name = "chkMostrarPeriodos";
            this.chkMostrarPeriodos.Size = new System.Drawing.Size(135, 17);
            this.chkMostrarPeriodos.TabIndex = 49;
            this.chkMostrarPeriodos.Text = "Mostrar todos períodos";
            this.chkMostrarPeriodos.UseVisualStyleBackColor = true;
            // 
            // lblLinhas
            // 
            this.lblLinhas.AutoSize = true;
            this.lblLinhas.Location = new System.Drawing.Point(121, 25);
            this.lblLinhas.Name = "lblLinhas";
            this.lblLinhas.Size = new System.Drawing.Size(64, 13);
            this.lblLinhas.TabIndex = 50;
            this.lblLinhas.Text = "linhas grade";
            // 
            // int2Ano
            // 
            this.int2Ano.Location = new System.Drawing.Point(50, 21);
            this.int2Ano.Name = "int2Ano";
            this.int2Ano.Size = new System.Drawing.Size(32, 26);
            this.int2Ano.TabIndex = 31;
            // 
            // int2Mes
            // 
            this.int2Mes.Location = new System.Drawing.Point(12, 20);
            this.int2Mes.Name = "int2Mes";
            this.int2Mes.Size = new System.Drawing.Size(32, 26);
            this.int2Mes.TabIndex = 30;
            // 
            // frmDocumentacaoAplicavel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 694);
            this.Controls.Add(this.lblLinhas);
            this.Controls.Add(this.chkMostrarPeriodos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboFiltro);
            this.Controls.Add(this.btnOkNome);
            this.Controls.Add(this.txtNomeCliente);
            this.Controls.Add(this.btnSelecionarRGR);
            this.Controls.Add(this.btnSelecionarOrdemPlanFat);
            this.Controls.Add(this.btnSelecionarOrdemDDR);
            this.Controls.Add(this.btnImprimirSelecionados);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.int2Ano);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.int2Mes);
            this.Controls.Add(this.label1);
            this.Name = "frmDocumentacaoAplicavel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documentação Aplicável";
            this.Load += new System.EventHandler(this.frmDocumentacaoAplicavel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.Button btnOk;
        private INTEIRO2 int2Ano;
        private System.Windows.Forms.Label label2;
        private INTEIRO2 int2Mes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Grade;
        private System.Windows.Forms.Button btnImprimirSelecionados;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Windows.Forms.Button btnSelecionarOrdemDDR;
        private System.Windows.Forms.Button btnSelecionarOrdemPlanFat;
        private System.Windows.Forms.Button btnSelecionarRGR;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Button btnOkNome;
        private System.Windows.Forms.ComboBox cboFiltro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequencial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeFantasia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ano;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodoApuracao;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnviarPlanFatAteDia;
        private System.Windows.Forms.DataGridViewTextBoxColumn AguardarAprovacaoPlanFat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PLANFATEnviada;
        private System.Windows.Forms.DataGridViewTextBoxColumn AguardarOrdemCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConferirDDRAteDia;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DDRConFerida;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DDR_Conferindo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnviarRGRAteDia;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RELGEREnviado;
        private System.Windows.Forms.DataGridViewButtonColumn Ok;
        private System.Windows.Forms.CheckBox chkMostrarPeriodos;
        private System.Windows.Forms.Label lblLinhas;
    }
}