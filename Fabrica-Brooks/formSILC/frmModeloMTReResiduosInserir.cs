using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmModeloMTReResiduosInserir : Form
    {
        public int pCodigoModeloMTReResiduo = 0;
        clsModeloMTReResiduos oModeloMTReResiduos = new clsModeloMTReResiduos();
        clsModeloMTReResiduosDados oModeloMTReResiduosDados = new clsModeloMTReResiduosDados();
        clsResiduos oResiduo = new clsResiduos();
        clsResiduoDados oResiduosDados = new clsResiduoDados();
        clsUnidadeDados oUnidadeDados = new clsUnidadeDados();
        clsEstadoFisicoDados oEstadoFisicoDados = new clsEstadoFisicoDados();
        clsClasseDados oClasseDados = new clsClasseDados();
        clsTipoAcondicionamentoDados oTipoAcondicionamentoDados = new clsTipoAcondicionamentoDados();
        clsTecnologiaDados oTecnologiaDados = new clsTecnologiaDados();

        public frmModeloMTReResiduosInserir()
        {
            InitializeComponent();
            CarregaComboUnidades();
            CarregaComboEstadoFisico();
            CarregaComboClasse();
            CarregaComboTipoAcondicionamento();
            CarregaComboTecnologia();
        }

        private void CarregaComboUnidades()
        {
            oUnidadeDados = new clsUnidadeDados();
            cboUnidade.Items.Clear();
            cboUnidade.Items.Add("");
            foreach (DataRow _drUnidade in oUnidadeDados.PreencheDataTable("Codigo").Rows)
            {
                cboUnidade.Items.Add(_drUnidade["Descricao"].ToString());
            }
            cboUnidade.SelectedIndex = 0;
        }

        private void CarregaComboEstadoFisico()
        {
            oEstadoFisicoDados = new clsEstadoFisicoDados();
            cboEstadoFisico.Items.Clear();
            cboEstadoFisico.Items.Add("");
            foreach (DataRow _drEstadoFisico in oEstadoFisicoDados.PreencheDataTable("Codigo").Rows)
            {
               cboEstadoFisico.Items.Add(_drEstadoFisico["Descricao"].ToString());
            }
            cboEstadoFisico.SelectedIndex = 0;
        }
        private void CarregaComboClasse()
        {
            oClasseDados = new clsClasseDados();
            cboClasse.Items.Clear();
            cboClasse.Items.Add("");
            foreach (DataRow _drClasse in oClasseDados.PreencheDataTable("Codigo").Rows)
            {
                cboClasse.Items.Add(_drClasse[1].ToString());
            }
            cboClasse.SelectedIndex = 0;
        }
        private void CarregaComboTipoAcondicionamento()
        {
            oTipoAcondicionamentoDados = new clsTipoAcondicionamentoDados();
            cboAcondicionamento.Items.Clear();
            cboAcondicionamento.Items.Add("");
            foreach (DataRow _drAcondicionamento in oTipoAcondicionamentoDados.PreencheDataTable("Codigo", "").Rows) 
            {
                cboAcondicionamento.Items.Add(_drAcondicionamento["Descricao"].ToString());
            }
            cboAcondicionamento.SelectedIndex = 0;
        }
        private void CarregaComboTecnologia()
        {
            oTecnologiaDados = new clsTecnologiaDados();
            cboTecnologia.Items.Clear();
            cboTecnologia.Items.Add("");
            foreach (DataRow _drTecnologia in oTecnologiaDados.PreencheDataTable("Codigo").Rows)
            {
                cboTecnologia.Items.Add(_drTecnologia["Descricao"].ToString());
            }
            cboTecnologia.SelectedIndex = 0;
        }
        private void MostraDadosIBAMA()
        {
            txtCodigoResiduoIBAMA.Text = "";
            if (residuo1.txtCodigo.Text != "")
            {
                oResiduo = new clsResiduos();
                oResiduosDados = new clsResiduoDados();
                oResiduosDados.PegaDados(oResiduo, Convert.ToInt32(residuo1.txtCodigo.Text));
                lblUnidadeSILC.Text = oResiduo.Unidade;
                lblEstadoFisicoSILC.Text = oResiduo.EstadoFisico;
                lblClasseSILC.Text = oResiduo.Classe;
                lblTecnologiaSILC.Text = oResiduo.TecnologiaAplicada;
                clsIBAMA oIbama = new clsIBAMA();
                clsIBAMADados oIbamaDados = new clsIBAMADados();
                if (oResiduo.CodigoIBAMA > 0)
                {
                    oIbamaDados.PegaDados(oIbama, oResiduo.CodigoIBAMA);
                    txtCodigoResiduoIBAMA.Text = oIbama.CodigoIBAMA;
                    lblDescricaoResiduoIBAMA.Text = oIbama.Descricao.Replace("–", "");   
                }
                else
                {
                    MessageBox.Show("Resíduo sem Código IBAMA!");
                }
            }
        }

        private void butOkBuscaResiduoDados_Click(object sender, EventArgs e)
        {
            LimpaCampos();            
            MostraDadosIBAMA();
        }

        private void LimpaCampos()
        {
            if (residuo1.txtCodigo.Text == "")
            {
                residuo1.txtDescricao.Text = "";
                txtCodigoResiduoIBAMA.Text = "";
            }
            txtClasseRisco.Text = "";
            cboUnidade.SelectedIndex = 0;
            cboAcondicionamento.SelectedIndex = 0;
            cboClasse.SelectedIndex = 0;
            cboEstadoFisico.SelectedIndex = 0;
            cboTecnologia.SelectedIndex = 0;
            txtGrupoEmbalagem.Text = "";
            txtNomeEmbarque.Text = "";
            txtNumeroONU.Text = "";
            lblClasseSILC.Text = "";
            lblEstadoFisicoSILC.Text = "";
            lblDescricaoResiduoIBAMA.Text = "";
            lblEstadoFisicoSILC.Text = "";
            lblTecnologiaSILC.Text = "";
            lblUnidadeSILC.Text = "";
        }

        private void butSalvar_Click(object sender, EventArgs e)
        {
            oModeloMTReResiduos.ClasseRisco = txtClasseRisco.Text;
            oModeloMTReResiduos.CodigoAcondicionamento = cboAcondicionamento.SelectedIndex;
            oModeloMTReResiduos.CodigoClasse = cboClasse.SelectedIndex;
            oModeloMTReResiduos.CodigoEstadoFisico = cboEstadoFisico.SelectedIndex;
            if (residuo1.txtCodigo.Text != "")
                oModeloMTReResiduos.CodigoResiduo = Convert.ToInt32(residuo1.txtCodigo.Text);
            oModeloMTReResiduos.CodigoTecnologia = cboTecnologia.SelectedIndex;
            oModeloMTReResiduos.GrupoEmbalagem = txtGrupoEmbalagem.Text;
            oModeloMTReResiduos.NomeEmbarque = txtNomeEmbarque.Text;
            oModeloMTReResiduos.NumeroONU = txtNumeroONU.Text;
            oModeloMTReResiduos.CodigoModeloMTRe = pCodigoModeloMTReResiduo;
            oModeloMTReResiduos.CodigoUnidade = cboUnidade.SelectedIndex;
            if (residuo1.txtCodigo.Text == "")
            {
                MessageBox.Show("Código Resíduo Inválido!");
            }
            else if (txtCodigoResiduoIBAMA.Text == "")
            {
                MessageBox.Show("Resíduo Código IBAMA Inválido! Selecione.");
            }
            else if (cboUnidade.SelectedIndex == 0)
            {
                MessageBox.Show("Unidade Inválida! Selecione.");
            }
            else if (cboEstadoFisico.SelectedIndex == 0)
            {
                MessageBox.Show("Estado Físico Inválido! Selecione.");
            }
            else if (cboClasse.SelectedIndex == 0)
            {
                MessageBox.Show("Classe inválida! Selecione.");
            }
            else if (cboAcondicionamento.SelectedIndex == 0)
            {
                MessageBox.Show("Tipo Acondicionamento inválido! Selecione.");
            }
            else if (cboTecnologia.SelectedIndex == 0)
            {
                MessageBox.Show("Tecnologia Inválida! Selecione.");
            }
            else if(txtNumeroONU.Text == "")
            {
                MessageBox.Show("Número ONU deve ser informado!");
            }
            else
            {                
                if (oModeloMTReResiduosDados.DadoExiste(pCodigoModeloMTReResiduo, oModeloMTReResiduos.CodigoResiduo) == "Incluir")
                {
                    oModeloMTReResiduosDados.Inserir(oModeloMTReResiduos);
                    this.Close();
                }
                else if (oModeloMTReResiduosDados.DadoExiste(pCodigoModeloMTReResiduo, oModeloMTReResiduos.CodigoResiduo) == "Alterar")
                {
                    oModeloMTReResiduosDados.Alterar(oModeloMTReResiduos, pCodigoModeloMTReResiduo, oModeloMTReResiduos.CodigoResiduo.ToString());
                    this.Close();
                }
            }
        }
    }
}
