using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmClienteDados : Form
    {
        clsClienteDados oClienteDados = new clsClienteDados();
        clsClientes oCliente = new clsClientes();
        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        clsEnderecos oEnderecos = new clsEnderecos();
        clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
        clsMunicipios oMunicipio = new clsMunicipios();
        int CodigoClienteSelecionado = 0;

        public frmClienteDados()

        {

            InitializeComponent();

            BindingSource bindingSource = new BindingSource();
            clsAliquotaImpostosDados oAliquotaImpostosDados = new clsAliquotaImpostosDados();
            clsBloqFinanceiroDados oBloqFin = new clsBloqFinanceiroDados();

            DataTable _dt = new DataTable();

            _dt = oBloqFin.PreencheDataTableBloqueioFinanceiroClientes("b.DataBloqueio", CodigoClienteSelecionado);
            bindingSource.DataSource = _dt;
            GradeBloqueioFinanceiro.DataSource = bindingSource.DataSource;         

            _dt = oAliquotaImpostosDados.PreencheDataTableRetencoes("CodigoBROOKS");
            foreach (DataRow _dr in _dt.Rows)
            {
                cboRetencoesNF.Items.Add(_dr["CdDesc"].ToString());
            }
            cboRetencoesNF.SelectedIndex = 0;       
        }

        private void MostraDados()
        {

            oCliente = oClienteDados.PegaDados(oCliente, CodigoClienteSelecionado);

            lblNrCodigo.Text = oCliente.Codigo.ToString("000000");
            txtNome.Text = oCliente.Nome;
            txtNomeFantasia.Text = oCliente.NomeFantasia;

            txtDataCadastro.Text = oCliente.DataCadastro;
            txtCodigoAterro.Text = oCliente.CodigoNoAterro.ToString();
            txtDataCadastro.Text = oCliente.DataCadastro;
            txtCNPJCPF.Text = oCliente.CNPJ_CPF;
            txtRGIE.Text = oCliente.RG_IE;
            if (oCliente.Inativo == 1)
                chkInativo.Checked = true;
            else if (oCliente.Inativo == 0)
                chkInativo.Checked = false ;
            cboTipoCadastro.Text = oCliente.TiposDeContrato.ToString();
            if (oCliente.NaoAceitaDiferencaPeso == 1)
                chkNaoAceitaDTRcPesoDiferente.Checked = true;
            else
                chkNaoAceitaDTRcPesoDiferente.Checked = false;
            if (oCliente.EnviarDDR == 1)
                chkDDR.Checked = true;
            else
                chkDDR.Checked = false;
            if (oCliente.EnviarCDF == 1)
                chkCDF.Checked = true;
            else
                chkCDF.Checked = false;

            txtPercentualISS.Text = oCliente.Percentual.ToString("#0.00");
            txtCNPJFat.Text = oCliente.CNPJ_Faturamento;

            for (int i = 0; i <= cboRetencoesNF.Items.Count - 1; i++) 
            {
                if (cboRetencoesNF.Items[i].ToString().Substring(0, 1) == oCliente.CodigoBROOKS_Retencoes)
                    cboRetencoesNF.SelectedIndex = i;
            }

            // dados endereço padrão
            oEnderecos = oEnderecoDados.PegaDados(oEnderecos, CodigoClienteSelecionado, 0, 0);
            txtEndereco.Text = oEnderecos.endereco;
            txtNumero.Text = oEnderecos.Numero;
            txtComplemento.Text = oEnderecos.Complemento;
            txtDDD1.Text = oEnderecos.DDD1.ToString();
            txtDDD2.Text = oEnderecos.DDD2.ToString();
            txtDDDC.Text = oEnderecos.DDD3.ToString();
            txtDDDF.Text = oEnderecos.DDDF.ToString();
            txtFone1.Text = oEnderecos.Fone1;
            txtFone2.Text = oEnderecos.Fone2;
            txtCelular.Text = oEnderecos.Fone3;
            txtFax.Text = oEnderecos.Fax;
            txtBairro.Text = oEnderecos.Bairro;
            txtCEP.Text = oEnderecos.CEP;
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEnderecos.CodigoMunicipio);
            txtCidade.Text = oMunicipio.Nome;
            txtCodigoIBGE.Text = oMunicipio.CodigoIBGE;
            txtUF.Text = oMunicipio.UF;
            txtEmail.Text = oEnderecos.email;
            txtContato.Text = oEnderecos.Contato;

            // dados endereço faturamento
            oEnderecos = oEnderecoDados.PegaDados(oEnderecos, CodigoClienteSelecionado, 1, 0);
            txtEnderecoFat.Text = oEnderecos.endereco;
            txtNumeroFat.Text = oEnderecos.Numero;
            txtComplementoFat.Text = oEnderecos.Complemento;
            txtDDD1Fat.Text = oEnderecos.DDD1.ToString();
            txtDDD2Fat.Text = oEnderecos.DDD2.ToString();
            txtDDDCFat.Text = oEnderecos.DDD3.ToString();
            txtDDDFFat.Text = oEnderecos.DDDF.ToString();
            txtFone1Fat.Text = oEnderecos.Fone1;
            txtFone2Fat.Text = oEnderecos.Fone2;
            txtCelularFat.Text = oEnderecos.Fone3;
            txtFaxFat.Text = oEnderecos.Fax;
            txtBairroFat.Text = oEnderecos.Bairro;
            txtCEPFat.Text = oEnderecos.CEP;
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEnderecos.CodigoMunicipio);
            txtCidadeFat.Text = oMunicipio.Nome;
            txtCodigoIBGEFat.Text = oMunicipio.CodigoIBGE;
            txtUFFat.Text = oMunicipio.UF;
            txtEmailFat.Text = oEnderecos.email;
            txtContatoFat.Text = oEnderecos.Contato;
            txtInstrucoesFat.Text = oEnderecos.InstrucoesFat;

            // dados endereço obra
            oEnderecos = oEnderecoDados.PegaDados(oEnderecos, CodigoClienteSelecionado, 2, 0);
            txtEnderecoObra.Text = oEnderecos.endereco;
            txtNumeroObra.Text = oEnderecos.Numero;
            txtComplementoObra.Text = oEnderecos.Complemento;
            txtDDD1Obra.Text = oEnderecos.DDD1.ToString();
            txtDDD2Obra.Text = oEnderecos.DDD2.ToString();
            txtDDDCObra.Text = oEnderecos.DDD3.ToString();
            txtDDDFObra.Text = oEnderecos.DDDF.ToString();
            txtFone1Obra.Text = oEnderecos.Fone1;
            txtFone2Obra.Text = oEnderecos.Fone2;
            txtCelularObra.Text = oEnderecos.Fone3;
            txtFaxObra.Text = oEnderecos.Fax;
            txtBairroObra.Text = oEnderecos.Bairro;
            txtCEPObra.Text = oEnderecos.CEP;
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEnderecos.CodigoMunicipio);
            txtCidadeObra.Text = oMunicipio.Nome;
            txtCodigoIBGEObra.Text = oMunicipio.CodigoIBGE;
            txtUFObra.Text = oMunicipio.UF;
            txtEmailObra.Text = oEnderecos.email;
            txtContatoObra.Text = oEnderecos.Contato;

            //Informações
            txtKmMedia.Text = oCliente.KmMedia.ToString();
            txtPontoReferencia.Text = oCliente.PontoReferencia;
            txtObservacao.Text = oCliente.OBS;

        }

        private void frmClienteDados_Load(object sender, EventArgs e)
        {
            lblDB.Text = "db: " + geral.BancoUsado.ToString();
            CodigoClienteSelecionado = Convert.ToInt32(lblNrCodigo.Text);
            MostraDados();
        }
    }
}
