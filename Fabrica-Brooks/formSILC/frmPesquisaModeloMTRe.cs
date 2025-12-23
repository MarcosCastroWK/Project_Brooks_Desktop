using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.IO;

namespace formSILC
{
    public partial class frmPesquisaModeloMTRe : Form
    {
        clsModeloMTRe oModelo = new clsModeloMTRe();
        clsModeloMTReDados oModeloDados = new clsModeloMTReDados();
        clsModeloMTReResiduos oModeloResiduos = new clsModeloMTReResiduos();
        clsModeloMTReResiduosDados oModeloResiduosDados = new clsModeloMTReResiduosDados();
        clsIBAMA oIbama = new clsIBAMA();
        clsIBAMADados oIbamaDados = new clsIBAMADados();
        clsResiduos oResiduo = new clsResiduos();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();

        DataTable _dt = new DataTable();
        private BindingSource bindingSource = new BindingSource();

        public string pLoginClienteIMA;
        public string pSenhaClienteIMA;
        public string pCNPJTransportador;
        public string pCNPJDestinador;
        public string pCNPJ_CPFGerador;
        public string pDataProgramada;
        public int pCodigoMotorista;
        public int pCodigoCaminhao;
        public int pCodigoResiduo;
        public int pCodigoCliente;
        public DataGridView pGrade;
        public int pRowIndex = 0;
        public bool pChamouPelaProgramacao = true;
        public int pNumeroLancamento = 0;
        public frmPesquisaModeloMTRe()
        {
            InitializeComponent();
        }

        protected void frmPesquisaModeloMTRe_Load(object sender, EventArgs e)
        {
            PreencheGridModelos("");
        }

        private void PreencheGridModelos(string pDescricao)
        {
            if (pDescricao == "")
                _dt = oModeloDados.PreencheDataTable(pCodigoResiduo.ToString(), "mr.CodigoResiduo");
            else
                _dt = oModeloDados.PreencheDataTable("mr.CodigoResiduo", pDescricao, "mm.Nome");
            _dt.Columns.Add("CodigoResiduo");
            _dt.Columns.Add("CodigoIBAMA");
            DataTable _dtResiduo = new DataTable();

            foreach (DataRow drModelo in _dt.Rows)
            {
                if (drModelo["Codigo"].ToString() != "")
                {
                    oModeloResiduos = new clsModeloMTReResiduos();
                    oModeloResiduosDados = new clsModeloMTReResiduosDados();
                    oModeloResiduosDados.PegaDados(oModeloResiduos, Convert.ToInt32(drModelo["CodigoModeloMTRe"]));
                    drModelo["CodigoResiduo"] = oModeloResiduos.CodigoResiduo;
                    oIbama = new clsIBAMA();
                    oIbamaDados = new clsIBAMADados();
                    oResiduo = new clsResiduos();
                    oResiduoDados = new clsResiduoDados();
                    oResiduoDados.PegaDados(oResiduo, oModeloResiduos.CodigoResiduo);
                    oIbamaDados.PegaDados(oIbama, oResiduo.CodigoIBAMA);
                    drModelo["CodigoIBAMA"] = oIbama.CodigoIBAMA;
                }
            }
            bindingSource.DataSource = _dt;
            grvModelos.DataSource = bindingSource;
        }

        private void grvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvModelos.CurrentRow == null || e.RowIndex == -1 || e.RowIndex == null)
                return;
            if (grvModelos.CurrentRow.Index >= 0)
            {
                if ((pCNPJ_CPFGerador != pLoginClienteIMA) && pCNPJ_CPFGerador != "" && pLoginClienteIMA != "" && pCodigoCliente != 0)
                {
                    MessageBox.Show("O CNPJ do Gerador diferente do Login ou inválido!");
                }
                else if (pSenhaClienteIMA == "")
                {
                    MessageBox.Show("Senha do Gerador ou Cliente inválido!");
                }
                else
                {
                    frmMTReLancar ofrmMTReLancar = new frmMTReLancar();
                    ofrmMTReLancar.pChamouPelaProgramacao = pChamouPelaProgramacao;
                    ofrmMTReLancar.pCNPJArmazenador = grvModelos.Rows[e.RowIndex].Cells["CNPJ_CPF_Armazenador"].Value.ToString();

                    if (grvModelos.Rows[e.RowIndex].Cells["CNPJ_CPF_Destinador"].Value.ToString() != "")
                    {
                        ofrmMTReLancar.pCNPJDestinador = grvModelos.Rows[e.RowIndex].Cells["CNPJ_CPF_Destinador"].Value.ToString();
                    }
                    if (grvModelos.Rows[e.RowIndex].Cells["NossoCodigoDestinador"].Value.ToString() != "" &&
                        grvModelos.Rows[e.RowIndex].Cells["NossoCodigoDestinador"].Value.ToString() != "0")
                    {
                        ofrmMTReLancar.pCodigoDestinoFinal = Convert.ToInt32(grvModelos.Rows[e.RowIndex].Cells["NossoCodigoDestinador"].Value.ToString());
                    }
                    if (pCNPJTransportador == null || pCNPJTransportador == "")
                        ofrmMTReLancar.pCNPJTransportador = grvModelos.Rows[e.RowIndex].Cells["CNPJ_CPF_Transportador"].Value.ToString();
                    else
                        ofrmMTReLancar.pCNPJTransportador = pCNPJTransportador;

                    ofrmMTReLancar.pCNPJ_CPFGerador = pCNPJ_CPFGerador;
                    ofrmMTReLancar.pCodigoCaminhao = pCodigoCaminhao;
                    ofrmMTReLancar.pCodigoMotorista = pCodigoMotorista;
                    ofrmMTReLancar.pDataProgramada = pDataProgramada;
                    ofrmMTReLancar.pLoginClienteIMA = pLoginClienteIMA;
                    ofrmMTReLancar.pSenhaClienteIMA = pSenhaClienteIMA;
                    ofrmMTReLancar.pCodigoResiduo = pCodigoResiduo;
                    ofrmMTReLancar.pCodigoCliente = pCodigoCliente;
                    ofrmMTReLancar.pCodigoModeloMTRe = grvModelos.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                    ofrmMTReLancar.pGrade = pGrade;
                    ofrmMTReLancar.pRowIndex = pRowIndex;
                    ofrmMTReLancar.pNumeroLancamento = pNumeroLancamento;
                    ofrmMTReLancar.ShowDialog();
                }
            }
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (txtDescricaoResiduo.Text != "")
            {
                PreencheGridModelos(txtDescricaoResiduo.Text);
            }
        }
    }
}
