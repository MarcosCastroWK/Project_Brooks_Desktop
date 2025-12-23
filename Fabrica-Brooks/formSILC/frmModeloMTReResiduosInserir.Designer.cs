
namespace formSILC
{
    partial class frmModeloMTReResiduosInserir
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
            this.lblResiduo = new System.Windows.Forms.Label();
            this.txtCodigoResiduoIBAMA = new System.Windows.Forms.TextBox();
            this.lblUnidade = new System.Windows.Forms.Label();
            this.cboUnidade = new System.Windows.Forms.ComboBox();
            this.lblEstadoFisico = new System.Windows.Forms.Label();
            this.cboEstadoFisico = new System.Windows.Forms.ComboBox();
            this.lblClasse = new System.Windows.Forms.Label();
            this.cboClasse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAcondicionamento = new System.Windows.Forms.ComboBox();
            this.lblTecnologia = new System.Windows.Forms.Label();
            this.cboTecnologia = new System.Windows.Forms.ComboBox();
            this.lblNumeroONU = new System.Windows.Forms.Label();
            this.txtNumeroONU = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNomeEmbarque = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClasseRisco = new System.Windows.Forms.TextBox();
            this.lblGrupoEmbalagem = new System.Windows.Forms.Label();
            this.txtGrupoEmbalagem = new System.Windows.Forms.TextBox();
            this.butSalvar = new System.Windows.Forms.Button();
            this.lblDescricaoResiduoIBAMA = new System.Windows.Forms.Label();
            this.lblTituloIBAMA = new System.Windows.Forms.Label();
            this.lblUnidadeSILC = new System.Windows.Forms.Label();
            this.residuo1 = new formSILC.RESIDUO();
            this.lblEstadoFisicoSILC = new System.Windows.Forms.Label();
            this.lblClasseSILC = new System.Windows.Forms.Label();
            this.lblTecnologiaSILC = new System.Windows.Forms.Label();
            this.butOkBuscaResiduoDados = new System.Windows.Forms.Button();
            this.lblColunaSILC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblResiduo
            // 
            this.lblResiduo.Location = new System.Drawing.Point(11, 8);
            this.lblResiduo.Name = "lblResiduo";
            this.lblResiduo.Size = new System.Drawing.Size(61, 31);
            this.lblResiduo.TabIndex = 1;
            this.lblResiduo.Text = "* Resíduo CD IBAMA:";
            this.lblResiduo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoResiduoIBAMA
            // 
            this.txtCodigoResiduoIBAMA.BackColor = System.Drawing.SystemColors.Info;
            this.txtCodigoResiduoIBAMA.Location = new System.Drawing.Point(69, 15);
            this.txtCodigoResiduoIBAMA.Name = "txtCodigoResiduoIBAMA";
            this.txtCodigoResiduoIBAMA.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoResiduoIBAMA.TabIndex = 2;
            // 
            // lblUnidade
            // 
            this.lblUnidade.AutoSize = true;
            this.lblUnidade.Location = new System.Drawing.Point(12, 131);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Size = new System.Drawing.Size(60, 13);
            this.lblUnidade.TabIndex = 3;
            this.lblUnidade.Text = "* Unidade: ";
            // 
            // cboUnidade
            // 
            this.cboUnidade.BackColor = System.Drawing.SystemColors.Info;
            this.cboUnidade.FormattingEnabled = true;
            this.cboUnidade.Location = new System.Drawing.Point(123, 128);
            this.cboUnidade.Name = "cboUnidade";
            this.cboUnidade.Size = new System.Drawing.Size(155, 21);
            this.cboUnidade.TabIndex = 4;
            // 
            // lblEstadoFisico
            // 
            this.lblEstadoFisico.AutoSize = true;
            this.lblEstadoFisico.Location = new System.Drawing.Point(11, 165);
            this.lblEstadoFisico.Name = "lblEstadoFisico";
            this.lblEstadoFisico.Size = new System.Drawing.Size(85, 13);
            this.lblEstadoFisico.TabIndex = 5;
            this.lblEstadoFisico.Text = "* Estado Físico: ";
            // 
            // cboEstadoFisico
            // 
            this.cboEstadoFisico.BackColor = System.Drawing.SystemColors.Info;
            this.cboEstadoFisico.FormattingEnabled = true;
            this.cboEstadoFisico.Location = new System.Drawing.Point(123, 162);
            this.cboEstadoFisico.Name = "cboEstadoFisico";
            this.cboEstadoFisico.Size = new System.Drawing.Size(155, 21);
            this.cboEstadoFisico.TabIndex = 6;
            // 
            // lblClasse
            // 
            this.lblClasse.AutoSize = true;
            this.lblClasse.Location = new System.Drawing.Point(12, 197);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(51, 13);
            this.lblClasse.TabIndex = 7;
            this.lblClasse.Text = "* Classe: ";
            // 
            // cboClasse
            // 
            this.cboClasse.BackColor = System.Drawing.SystemColors.Info;
            this.cboClasse.FormattingEnabled = true;
            this.cboClasse.Location = new System.Drawing.Point(123, 195);
            this.cboClasse.Name = "cboClasse";
            this.cboClasse.Size = new System.Drawing.Size(155, 21);
            this.cboClasse.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "* Acondicionamento: ";
            // 
            // cboAcondicionamento
            // 
            this.cboAcondicionamento.BackColor = System.Drawing.SystemColors.Info;
            this.cboAcondicionamento.FormattingEnabled = true;
            this.cboAcondicionamento.Location = new System.Drawing.Point(123, 225);
            this.cboAcondicionamento.Name = "cboAcondicionamento";
            this.cboAcondicionamento.Size = new System.Drawing.Size(155, 21);
            this.cboAcondicionamento.TabIndex = 10;
            // 
            // lblTecnologia
            // 
            this.lblTecnologia.AutoSize = true;
            this.lblTecnologia.Location = new System.Drawing.Point(12, 260);
            this.lblTecnologia.Name = "lblTecnologia";
            this.lblTecnologia.Size = new System.Drawing.Size(73, 13);
            this.lblTecnologia.TabIndex = 11;
            this.lblTecnologia.Text = "* Tecnologia: ";
            // 
            // cboTecnologia
            // 
            this.cboTecnologia.BackColor = System.Drawing.SystemColors.Info;
            this.cboTecnologia.FormattingEnabled = true;
            this.cboTecnologia.Location = new System.Drawing.Point(123, 260);
            this.cboTecnologia.Name = "cboTecnologia";
            this.cboTecnologia.Size = new System.Drawing.Size(155, 21);
            this.cboTecnologia.TabIndex = 12;
            // 
            // lblNumeroONU
            // 
            this.lblNumeroONU.AutoSize = true;
            this.lblNumeroONU.Location = new System.Drawing.Point(12, 303);
            this.lblNumeroONU.Name = "lblNumeroONU";
            this.lblNumeroONU.Size = new System.Drawing.Size(74, 13);
            this.lblNumeroONU.TabIndex = 13;
            this.lblNumeroONU.Text = "Número ONU:";
            // 
            // txtNumeroONU
            // 
            this.txtNumeroONU.BackColor = System.Drawing.SystemColors.Info;
            this.txtNumeroONU.Location = new System.Drawing.Point(123, 299);
            this.txtNumeroONU.MaxLength = 40;
            this.txtNumeroONU.Name = "txtNumeroONU";
            this.txtNumeroONU.Size = new System.Drawing.Size(155, 20);
            this.txtNumeroONU.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Nome para Embarque:";
            // 
            // txtNomeEmbarque
            // 
            this.txtNomeEmbarque.BackColor = System.Drawing.SystemColors.Info;
            this.txtNomeEmbarque.Location = new System.Drawing.Point(123, 331);
            this.txtNomeEmbarque.MaxLength = 40;
            this.txtNomeEmbarque.Name = "txtNomeEmbarque";
            this.txtNomeEmbarque.Size = new System.Drawing.Size(155, 20);
            this.txtNomeEmbarque.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Classe de Risco:";
            // 
            // txtClasseRisco
            // 
            this.txtClasseRisco.BackColor = System.Drawing.SystemColors.Info;
            this.txtClasseRisco.Location = new System.Drawing.Point(123, 362);
            this.txtClasseRisco.MaxLength = 40;
            this.txtClasseRisco.Name = "txtClasseRisco";
            this.txtClasseRisco.Size = new System.Drawing.Size(155, 20);
            this.txtClasseRisco.TabIndex = 18;
            // 
            // lblGrupoEmbalagem
            // 
            this.lblGrupoEmbalagem.AutoSize = true;
            this.lblGrupoEmbalagem.Location = new System.Drawing.Point(12, 396);
            this.lblGrupoEmbalagem.Name = "lblGrupoEmbalagem";
            this.lblGrupoEmbalagem.Size = new System.Drawing.Size(97, 13);
            this.lblGrupoEmbalagem.TabIndex = 19;
            this.lblGrupoEmbalagem.Text = "Grupo Embalagem:";
            // 
            // txtGrupoEmbalagem
            // 
            this.txtGrupoEmbalagem.BackColor = System.Drawing.SystemColors.Info;
            this.txtGrupoEmbalagem.Location = new System.Drawing.Point(123, 395);
            this.txtGrupoEmbalagem.MaxLength = 40;
            this.txtGrupoEmbalagem.Name = "txtGrupoEmbalagem";
            this.txtGrupoEmbalagem.Size = new System.Drawing.Size(155, 20);
            this.txtGrupoEmbalagem.TabIndex = 20;
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(322, 399);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(64, 26);
            this.butSalvar.TabIndex = 22;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // lblDescricaoResiduoIBAMA
            // 
            this.lblDescricaoResiduoIBAMA.BackColor = System.Drawing.SystemColors.Info;
            this.lblDescricaoResiduoIBAMA.Location = new System.Drawing.Point(71, 48);
            this.lblDescricaoResiduoIBAMA.Name = "lblDescricaoResiduoIBAMA";
            this.lblDescricaoResiduoIBAMA.Size = new System.Drawing.Size(609, 53);
            this.lblDescricaoResiduoIBAMA.TabIndex = 23;
            // 
            // lblTituloIBAMA
            // 
            this.lblTituloIBAMA.Location = new System.Drawing.Point(12, 48);
            this.lblTituloIBAMA.Name = "lblTituloIBAMA";
            this.lblTituloIBAMA.Size = new System.Drawing.Size(60, 32);
            this.lblTituloIBAMA.TabIndex = 24;
            this.lblTituloIBAMA.Text = "Descrição IBAMA:";
            // 
            // lblUnidadeSILC
            // 
            this.lblUnidadeSILC.BackColor = System.Drawing.SystemColors.Info;
            this.lblUnidadeSILC.Location = new System.Drawing.Point(288, 129);
            this.lblUnidadeSILC.Name = "lblUnidadeSILC";
            this.lblUnidadeSILC.Size = new System.Drawing.Size(50, 21);
            this.lblUnidadeSILC.TabIndex = 5;
            this.lblUnidadeSILC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // residuo1
            // 
            this.residuo1.Location = new System.Drawing.Point(173, 15);
            this.residuo1.Name = "residuo1";
            this.residuo1.Size = new System.Drawing.Size(507, 22);
            this.residuo1.TabIndex = 0;
            this.residuo1.TabIndexCodigo = 0;
            this.residuo1.Unidade = null;
            // 
            // lblEstadoFisicoSILC
            // 
            this.lblEstadoFisicoSILC.BackColor = System.Drawing.SystemColors.Info;
            this.lblEstadoFisicoSILC.Location = new System.Drawing.Point(288, 162);
            this.lblEstadoFisicoSILC.Name = "lblEstadoFisicoSILC";
            this.lblEstadoFisicoSILC.Size = new System.Drawing.Size(50, 21);
            this.lblEstadoFisicoSILC.TabIndex = 25;
            this.lblEstadoFisicoSILC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClasseSILC
            // 
            this.lblClasseSILC.BackColor = System.Drawing.SystemColors.Info;
            this.lblClasseSILC.Location = new System.Drawing.Point(288, 195);
            this.lblClasseSILC.Name = "lblClasseSILC";
            this.lblClasseSILC.Size = new System.Drawing.Size(50, 21);
            this.lblClasseSILC.TabIndex = 26;
            this.lblClasseSILC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTecnologiaSILC
            // 
            this.lblTecnologiaSILC.BackColor = System.Drawing.SystemColors.Info;
            this.lblTecnologiaSILC.Location = new System.Drawing.Point(287, 260);
            this.lblTecnologiaSILC.Name = "lblTecnologiaSILC";
            this.lblTecnologiaSILC.Size = new System.Drawing.Size(50, 21);
            this.lblTecnologiaSILC.TabIndex = 27;
            this.lblTecnologiaSILC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // butOkBuscaResiduoDados
            // 
            this.butOkBuscaResiduoDados.Location = new System.Drawing.Point(685, 13);
            this.butOkBuscaResiduoDados.Name = "butOkBuscaResiduoDados";
            this.butOkBuscaResiduoDados.Size = new System.Drawing.Size(32, 23);
            this.butOkBuscaResiduoDados.TabIndex = 28;
            this.butOkBuscaResiduoDados.Text = "Ok";
            this.butOkBuscaResiduoDados.UseVisualStyleBackColor = true;
            this.butOkBuscaResiduoDados.Click += new System.EventHandler(this.butOkBuscaResiduoDados_Click);
            // 
            // lblColunaSILC
            // 
            this.lblColunaSILC.AutoSize = true;
            this.lblColunaSILC.Location = new System.Drawing.Point(286, 111);
            this.lblColunaSILC.Name = "lblColunaSILC";
            this.lblColunaSILC.Size = new System.Drawing.Size(54, 13);
            this.lblColunaSILC.TabIndex = 29;
            this.lblColunaSILC.Text = "COL.SILC";
            // 
            // frmModeloMTReResiduosInserir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 437);
            this.Controls.Add(this.lblColunaSILC);
            this.Controls.Add(this.butOkBuscaResiduoDados);
            this.Controls.Add(this.lblTecnologiaSILC);
            this.Controls.Add(this.lblClasseSILC);
            this.Controls.Add(this.lblEstadoFisicoSILC);
            this.Controls.Add(this.lblUnidadeSILC);
            this.Controls.Add(this.lblTituloIBAMA);
            this.Controls.Add(this.lblDescricaoResiduoIBAMA);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.txtGrupoEmbalagem);
            this.Controls.Add(this.lblGrupoEmbalagem);
            this.Controls.Add(this.txtClasseRisco);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNomeEmbarque);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumeroONU);
            this.Controls.Add(this.lblNumeroONU);
            this.Controls.Add(this.cboTecnologia);
            this.Controls.Add(this.lblTecnologia);
            this.Controls.Add(this.cboAcondicionamento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboClasse);
            this.Controls.Add(this.lblClasse);
            this.Controls.Add(this.cboEstadoFisico);
            this.Controls.Add(this.lblEstadoFisico);
            this.Controls.Add(this.cboUnidade);
            this.Controls.Add(this.lblUnidade);
            this.Controls.Add(this.txtCodigoResiduoIBAMA);
            this.Controls.Add(this.lblResiduo);
            this.Controls.Add(this.residuo1);
            this.MaximizeBox = false;
            this.Name = "frmModeloMTReResiduosInserir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inserir Residuos do Modelo MTRe ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RESIDUO residuo1;
        private System.Windows.Forms.Label lblResiduo;
        private System.Windows.Forms.TextBox txtCodigoResiduoIBAMA;
        private System.Windows.Forms.Label lblUnidade;
        private System.Windows.Forms.ComboBox cboUnidade;
        private System.Windows.Forms.Label lblEstadoFisico;
        private System.Windows.Forms.ComboBox cboEstadoFisico;
        private System.Windows.Forms.Label lblClasse;
        private System.Windows.Forms.ComboBox cboClasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAcondicionamento;
        private System.Windows.Forms.Label lblTecnologia;
        private System.Windows.Forms.ComboBox cboTecnologia;
        private System.Windows.Forms.Label lblNumeroONU;
        private System.Windows.Forms.TextBox txtNumeroONU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNomeEmbarque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClasseRisco;
        private System.Windows.Forms.Label lblGrupoEmbalagem;
        private System.Windows.Forms.TextBox txtGrupoEmbalagem;
        private System.Windows.Forms.Button butSalvar;
        private System.Windows.Forms.Label lblDescricaoResiduoIBAMA;
        private System.Windows.Forms.Label lblTituloIBAMA;
        private System.Windows.Forms.Label lblUnidadeSILC;
        private System.Windows.Forms.Label lblEstadoFisicoSILC;
        private System.Windows.Forms.Label lblClasseSILC;
        private System.Windows.Forms.Label lblTecnologiaSILC;
        private System.Windows.Forms.Button butOkBuscaResiduoDados;
        private System.Windows.Forms.Label lblColunaSILC;
    }
}