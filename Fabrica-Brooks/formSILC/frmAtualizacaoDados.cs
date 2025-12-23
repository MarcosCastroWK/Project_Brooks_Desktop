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
using System.Data.OleDb;

namespace formSILC
{
    public partial class frmAtualizacaoDados : Form
    {
        private clsClientes oCliente = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        private clsLancamentos oLancamentos = new clsLancamentos();
        private clsLancamentosDados oLancamentosDados = new clsLancamentosDados();
        private clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
        private clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        private clsCacambas oContaineres = new clsCacambas();
        private clsCacambaDados oContaineresDados = new clsCacambaDados();
        private clsResiduos oResiduos = new clsResiduos();
        private clsResiduoDados oResiduoDados = new clsResiduoDados();
        private clsDestinoFinal oDestino = new clsDestinoFinal();
        private clsDestinoFinalDados oDestinoDados = new clsDestinoFinalDados();
        private clsEnderecos oEndereco = new clsEnderecos();
        private clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        private clsProgramacaoFechada oProgramacaoFechada = new clsProgramacaoFechada();
        private clsProgramacaoFechadaDados oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();

        private OleDbConnection aConnection;

        DataTable _dtAtualizacao = new DataTable();
        private BindingSource bindingSource = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        private int i;

        public frmAtualizacaoDados()
        {
            InitializeComponent();
            if (geral.UsuarioAtual != "teixeira")
                btnAtualizaParticularidade.Enabled = false;
        }

        private void AtualizacaoMunicipiosAliquotas()
        {
            try
            {
                clsMunicipios oMunicipio = new clsMunicipios();
                clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Municipios order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0" && dr["Codigo"].ToString() != "&nbsp;")
                    {
                        oMunicipio = new clsMunicipios();
                        if (dr["Codigo"].ToString() != "")
                            oMunicipio.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["AliquotaISS"].ToString() != "")
                            oMunicipio.AliquotaISS = Convert.ToDecimal(dr["AliquotaISS"]);
                        oMunicipio.Nome = dr["Nome"].ToString();
                        oMunicipio.UF = dr["UF"].ToString();
                        oMunicipio.CodigoIBGE = dr["CodigoIBGE"].ToString();
                        if (dr["CodigoIPM"].ToString() != "")
                            oMunicipio.CodigoIPM = Convert.ToInt32(dr["CodigoIPM"]);
                        if (oMunicipioDados.DadoExiste(oMunicipio.Codigo) == "Incluir")
                        {
                            oMunicipioDados.Inserir(oMunicipio);
                        }
                        else
                        {
                            oMunicipioDados.Alterar(oMunicipio, oMunicipio.Codigo);
                        }
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString() + " Municípios alíquotas";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rever essa necessidade.");
            return;
            i = 0;
            lblMensagem.Text = "";
            foreach (DataGridViewRow _dr in GradeTabelasDB.Rows)
            {
                string s = "";
                if (_dr.Cells[0].Value != null && _dr.Cells[1].Value != null)
                {
                    if (_dr.Cells[0].Value.ToString().ToLower() == "abastecimento")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoAbastecimentos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "aliquotaimpostos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoAliquotasImpostos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "aterro")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoDestinoFinal();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "aterrosanitario") // Controle de Aterro
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoAterroSanitario();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "blocosmtr")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoBlocosMTR();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "bloqueiofinanceiro")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoBloqueioFinanceiro();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "cacambas")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoCacambas();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "caminhoes")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoCaminhoes();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "clientes")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                        {
                            if (cliente1.txtCodigo.Text == "" || cliente1.txtCodigo.Text == "0")
                            {
                                if (geral.UsuarioAtual == "teixeira")
                                    AtualizacaoClientes();
                                else
                                    MessageBox.Show("Todos clientes. Solicitar para usuário administrador.");
                            }
                            else
                                AtualizacaoClientes();
                        }
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "codigoservicosprefeitura")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoCodigoServicosPrefeitura();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "configsis")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoConfigSis();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "contratoresiduos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                        {
                            MessageBox.Show("Desabilitado para usar o cadastro no novo. Só Teixeia.");
                            if (geral.UsuarioAtual == "teixeira")
                                AtualizacaoContratoResiduos();
                        }
                    }
                    else if (_dr.Cells[0].Value.ToString().ToLower() == "contratos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                        {
                            MessageBox.Show("Desabilitado para usar o cadastro no novo. DEFINITIVO.");
                            //if (geral.UsuarioAtual == "teixeira")
                            //    AtualizacaoContratos();
                        }
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "corponotasfiscais")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoCorpoNotasFiscais();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "notasfiscais")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoNotasFiscais();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "dtr")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoDTR();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "descricaoservicos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoDescricaoServicos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "licencaambiental")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoLAOs();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "lancamentos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                        {
                            //MessageBox.Show("Desabilitado para usar o cadastro no novo");
                            if (geral.UsuarioAtual == "teixeira")
                                AtualizacaoLancamentos();
                            AtualizaColocacoes();
                        }
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "programacaodiariaservicos")
                    {
                        //if (_dr.Cells[1].Value.ToString() == "True")
                        //AtualizacaoProgramacaoDiariaServicos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "programacaofechada")
                    {
                        //if (_dr.Cells[1].Value.ToString() == "True")
                        //    AtualizacaoProgramacaoFechada();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "reajustes")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                        {
                            MessageBox.Show("Desabilitado para usar o cadastro no novo");
                            if (geral.UsuarioAtual == "teixeira")
                                AtualizacaoContratosReajustes();
                        }
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "residuos")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoResiduos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "reprogramacaoservicos")
                    {
                        //if (_dr.Cells[1].Value.ToString() == "True")
                        //    AtualizacaoReprogramacaoServicos();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "senhaipm")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoSenhaIPM();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "funcionarios")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoFuncionarios();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "servicosfutura")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoServicosFutura();
                    }
                    if (_dr.Cells[0].Value.ToString().ToLower() == "municipios")
                    {
                        if (_dr.Cells[1].Value.ToString() == "True")
                            AtualizacaoMunicipiosAliquotas();
                    }
                }
            }
        }
        private void AtualizacaoClientes()
        {
            //try
            //{

            //cria a conexão com o banco de dados
            aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
            aConnection.Open();

            string s = "";
            s = s + "select * from Clientes \n";
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
                s = s + "where   Codigo = " + cliente1.txtCodigo.Text;
            else
                s = s + "where   Codigo >= 1 order by Codigo";
            _dtAtualizacao = new DataTable();
            OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
            oDataAdapter.Fill(_dtAtualizacao);
            oDataAdapter.Dispose();
            foreach (DataRow dr in _dtAtualizacao.Rows)
            {
                if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                {
                    oCliente = new clsClientes();
                    if (dr["Codigo"].ToString() != "")
                        oCliente.Codigo = Convert.ToInt32(dr["Codigo"]);
                    if (dr["DataCadastro"].ToString() != "")
                        oCliente.DataCadastro = Convert.ToDateTime(dr["DataCadastro"]).ToShortDateString();
                    oCliente.CEP = dr["CEP"].ToString();
                    if (dr["Classificacao"].ToString() != "")
                        oCliente.Classificacao = Convert.ToInt32(dr["Classificacao"]);
                    oCliente.CNPJ_CPF = dr["CGC_CPF"].ToString();
                    oCliente.CNPJ_Faturamento = dr["CNPJ_Faturamento"].ToString();
                    oCliente.CodigoBROOKS_Retencoes = dr["CodigoBROOKS_Retencoes"].ToString();
                    if (dr["CodigoClienteExportacao"].ToString() != "")
                        oCliente.CodigoClienteExportacao = Convert.ToInt32(dr["CodigoClienteExportacao"]);
                    if (dr["CodigoMunicipioNF"].ToString() != "")
                        oCliente.CodigoMunicipioNF = Convert.ToInt32(dr["CodigoMunicipioNF"]);
                    if (dr["CodigoMunicipioObra"].ToString() != "")
                        oCliente.CodigoMunicipioObra = Convert.ToInt32(dr["CodigoMunicipioObra"]);
                    if (dr["CodigoNoAterro"].ToString() != "")
                        oCliente.CodigoNoAterro = Convert.ToInt32(dr["CodigoNoAterro"]);
                    if (dr["CodigoSituacaoTributaria"].ToString() != "")
                        oCliente.CodigoSituacaoTributaria = Convert.ToInt32(dr["CodigoSituacaoTributaria"]);
                    if (dr["ContaGerencial"].ToString() != "")
                        oCliente.ContaGerencial = Convert.ToInt32(dr["ContaGerencial"]);
                    oCliente.ContatoFatima = dr["ContatoFatima"].ToString();
                    if (dr["dtNasc"].ToString() != "")
                        oCliente.DataNascimento = Convert.ToDateTime(dr["dtNasc"]).ToShortDateString();
                    if (dr["DataSenha"].ToString() != "")
                        oCliente.DataSenha = Convert.ToDateTime(dr["DataSenha"]).ToShortDateString();
                    if (dr["dtRG"].ToString() != "")
                        oCliente.dtRG = Convert.ToDateTime(dr["dtRG"]).ToShortDateString();
                    oCliente.email = dr["email"].ToString();
                    oCliente.emailFatma = dr["emailFatma"].ToString();
                    if (dr["EnviarCDF"].ToString() != "")
                        oCliente.EnviarCDF = Convert.ToInt32(dr["EnviarCDF"]);
                    if (dr["EnviarDDR"].ToString() != "")
                        oCliente.EnviarDDR = Convert.ToInt32(dr["EnviarDDR"]);
                    if (dr["Filial"].ToString() != "")
                        oCliente.Filial = Convert.ToInt16(dr["Filial"]);
                    if (dr["Inativo"].ToString() != "")
                        oCliente.Inativo = Convert.ToInt16(dr["Inativo"]);
                    if (dr["KmMedia"].ToString() != "")
                        oCliente.KmMedia = Convert.ToInt32(dr["KmMedia"]);
                    oCliente.Nacionalidade = dr["Nacionalidade"].ToString();
                    if (dr["NaoAceitaDiferencaPeso"].ToString() != "")
                        oCliente.NaoAceitaDiferencaPeso = Convert.ToInt16(dr["NaoAceitaDiferencaPeso"]);
                    oCliente.Naturalidade = dr["Naturalidade"].ToString();
                    oCliente.Nome = dr["Nome2"].ToString();
                    oCliente.Nome2 = dr["Nome2"].ToString();
                    oCliente.NomeDaEmpresa = dr["NomeDaEmpresa"].ToString();
                    oCliente.NomeFantasia = dr["nmFant"].ToString();
                    oCliente.NovaSenha = dr["NovaSenha"].ToString();
                    oCliente.OBS = dr["OBS"].ToString();
                    oCliente.ObsFatima = dr["ObsFatima"].ToString();
                    if (dr["Percentual"].ToString() != "")
                        oCliente.Percentual = Convert.ToDecimal(dr["Percentual"]);
                    if (dr["Pessoa"].ToString() != "")
                        oCliente.Pessoa = Convert.ToInt16(dr["Pessoa"]);
                    oCliente.PontoReferencia = dr["PontoReferencia"].ToString();
                    oCliente.RG_IE = dr["RG_IE"].ToString();
                    oCliente.RGEmit = dr["RGEmit"].ToString();
                    oCliente.SenhaAcessoFatima = dr["SenhaAcessoFatima"].ToString();
                    oCliente.SenhaMasterFatima = dr["SenhaMasterFatima"].ToString();
                    oCliente.site = dr["site"].ToString();
                    oCliente.SolicitadoSenhaPor = dr["SolicitadoSenhaPor"].ToString();
                    oCliente.TelefoneFatima = dr["TelefoneFatima"].ToString();
                    if (dr["TiposDeContrato"].ToString() != "")
                        oCliente.TiposDeContrato = Convert.ToInt16(dr["TiposDeContrato"]);
                    if (dr["CodigoTipoCobranca"].ToString() != "")
                        oCliente.CodigoTipoCobranca = Convert.ToInt16(dr["CodigoTipoCobranca"]);

                    if (oClienteDados.DadoExiste(oCliente.Codigo) == "Alterar")
                        oClienteDados.Alterar(oCliente, oCliente.Codigo);
                    else
                        oClienteDados.Inserir(oCliente, oCliente.Codigo);

                    DataTable _dtEndereco = new DataTable();
                    s = "";
                    s = s + "select * from Enderecos \n";
                    s = s + "where Codigo = " + oCliente.Codigo;
                    s = s + "and   TipoCadastro = 0 \n";
                    OleDbDataAdapter oDAEndereco = new OleDbDataAdapter(s, aConnection);
                    oDAEndereco.Fill(_dtEndereco);
                    oDAEndereco.Dispose();
                    foreach (DataRow _drEndereco in _dtEndereco.Rows)
                    {
                        oEndereco = new clsEnderecos();
                        oEndereco.Codigo = Convert.ToInt32(_drEndereco["Codigo"]);
                        oEndereco.Bairro = _drEndereco["Bairro"].ToString();
                        oEndereco.CEP = _drEndereco["CEP"].ToString();
                        if (_drEndereco["CodigoMunicipio"].ToString() != "")
                            oEndereco.CodigoMunicipio = Convert.ToInt32(_drEndereco["CodigoMunicipio"]);
                        oEndereco.Complemento = _drEndereco["Complemento"].ToString();
                        oEndereco.Contato = _drEndereco["Contato"].ToString();
                        if (_drEndereco["DDD1"].ToString() != "")
                            oEndereco.DDD1 = Convert.ToInt32(_drEndereco["DDD1"]);
                        if (_drEndereco["DDD2"].ToString() != "")
                            oEndereco.DDD2 = Convert.ToInt32(_drEndereco["DDD2"]);
                        if (_drEndereco["DDD3"].ToString() != "")
                            oEndereco.DDD3 = Convert.ToInt32(_drEndereco["DDD3"]);
                        if (_drEndereco["DDDF"].ToString() != "")
                            oEndereco.DDDF = Convert.ToInt32(_drEndereco["DDDF"]);
                        oEndereco.email = _drEndereco["email"].ToString();
                        oEndereco.endereco = _drEndereco["endereco"].ToString();
                        oEndereco.Fax = _drEndereco["Fax"].ToString();
                        oEndereco.Fone1 = _drEndereco["Fone1"].ToString();
                        oEndereco.Fone2 = _drEndereco["Fone2"].ToString();
                        oEndereco.Fone3 = _drEndereco["Fone3"].ToString();
                        oEndereco.InstrucoesFat = _drEndereco["InstrucoesFat"].ToString();
                        oEndereco.Numero = _drEndereco["Numero"].ToString();
                        oEndereco.TipoCadastro = 0;

                        if (_drEndereco["TipoEndereco"].ToString() != "")
                            oEndereco.TipoEndereco = Convert.ToInt16(_drEndereco["TipoEndereco"]);

                        if (oEnderecoDados.DadoExiste(oEndereco.Codigo, 0, oEndereco.TipoEndereco) == "Alterar")
                            oEnderecoDados.Alterar(oEndereco, oEndereco.Codigo, 0, oEndereco.TipoEndereco);
                        else
                            oEnderecoDados.Inserir(oEndereco, oEndereco.Codigo, 0, oEndereco.TipoEndereco);
                    }

                    i++;
                    lblMensagem.Text = "Atualizado/lido: " + oCliente.Codigo.ToString() + " clientes atualizados";
                    lblMensagem.Refresh();

                }
            }
            //fecha a conexao 
            aConnection.Close();
            aConnection.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    lblMensagem.Text = ex.Message;
            //}
        }

        private void AtualizacaoDestinoFinal()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Aterro order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oDestino = new clsDestinoFinal();
                        if (dr["Codigo"].ToString() != "")
                            oDestino.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["Ativo"].ToString() != "")
                            oDestino.Ativo = Convert.ToInt16(dr["Ativo"]);
                        oDestino.Bairro = dr["Bairro"].ToString();
                        oDestino.Celular = dr["Celular"].ToString();
                        oDestino.CEP = dr["CEP"].ToString();
                        oDestino.Cidade = dr["Cidade"].ToString();
                        oDestino.CNPJ = dr["CNPJ"].ToString();
                        oDestino.DataCadastro = DateTime.Now.ToShortDateString();
                        oDestino.eMail = dr["eMail"].ToString();
                        if (dr["EmiteCDF"].ToString() != "")
                            oDestino.EmiteCDF = Convert.ToInt32(dr["EmiteCDF"]);
                        oDestino.Endereco = dr["Endereco"].ToString();
                        if (dr["Enviar_emailCDF"].ToString() != "")
                            oDestino.Enviar_emailCDF = Convert.ToInt32(dr["Enviar_emailCDF"]);
                        if (dr["Enviar_emailMovResiduos"].ToString() != "")
                            oDestino.Enviar_emailMovResiduos = Convert.ToInt32(dr["Enviar_emailMovResiduos"]);
                        oDestino.Fone = dr["Fone"].ToString();
                        oDestino.LocalAterro = dr["LocalAterro"].ToString();
                        oDestino.Nome = dr["Nome"].ToString();
                        oDestino.NomeArqAss = dr["NomeArqAss"].ToString();
                        oDestino.NomeArqLogo = dr["NomeArqLogo"].ToString();
                        oDestino.NomeFantasia = dr["NomeFantasia"].ToString();
                        oDestino.UF = dr["UF"].ToString();
                        if (oDestinoDados.DadoExiste(oDestino.Codigo) == "Alterar")
                            oDestinoDados.Alterar(oDestino, oDestino.Codigo);
                        else
                            oDestinoDados.Inserir(oDestino, oDestino.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Destino Final ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoResiduos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Residuos order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oResiduos = new clsResiduos();
                        if (dr["Codigo"].ToString() != "")
                            oResiduos.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["Ativo"].ToString() != "")
                            oResiduos.Ativo = Convert.ToInt16(dr["Ativo"]);
                        oResiduos.Classe = dr["Classe"].ToString();
                        if (dr["CodigoDestinoFinal"].ToString() != "")
                            oResiduos.CodigoDestinoFinal = Convert.ToInt32(dr["CodigoDestinoFinal"]);
                        if (dr["CodigoGrupoResiduo"].ToString() != "")
                            oResiduos.CodigoGrupoResiduo = Convert.ToInt32(dr["CodigoGrupoResiduo"]);
                        if (dr["CodigoIBAMA"].ToString() != "")
                            oResiduos.CodigoIBAMA = Convert.ToInt32(dr["CodigoIBAMA"]);
                        oResiduos.CodigoResiduoManifesto = dr["CodigoResiduoManifesto"].ToString();
                        oResiduos.DataCadastro = DateTime.Now.ToShortDateString();
                        oResiduos.Descricao = dr["Descricao"].ToString();
                        oResiduos.DescricaoReduzida = dr["DescricaoReduzida"].ToString();
                        if (dr["EhReciclavel"].ToString() != "")
                            oResiduos.EhReciclavel = Convert.ToInt16(dr["EhReciclavel"]);
                        if (dr["EhServico"].ToString() != "")
                            oResiduos.EhServico = Convert.ToInt16(dr["EhServico"]);
                        oResiduos.EstadoFisico = dr["EstadoFisico"].ToString();
                        oResiduos.TecnologiaAplicada = dr["TecnologiaAplicada"].ToString();
                        if (dr["M3PorTon"].ToString() != "")
                            oResiduos.M3PorTon = Convert.ToDecimal(dr["M3PorTon"]);
                        oResiduos.Unidade = dr["Unidade"].ToString();
                        if (oResiduoDados.DadoExiste(oResiduos.Codigo) == "Alterar")
                            oResiduoDados.Alterar(oResiduos, oResiduos.Codigo);
                        else
                            oResiduoDados.Inserir(oResiduos, oResiduos.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " resíduos ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoCacambas()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Cacambas order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oContaineres = new clsCacambas();
                        if (dr["Codigo"].ToString() != "")
                            oContaineres.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["Capacidade"].ToString() != "")
                            oContaineres.Capacidade = Convert.ToDecimal(dr["Capacidade"]);
                        oContaineres.Cor = dr["Cor"].ToString();
                        oContaineres.DataCadastro = DateTime.Now.ToShortDateString();
                        if (dr["EhLocal"].ToString() != "")
                            oContaineres.EhLocal = Convert.ToInt16(dr["EhLocal"]);
                        if (dr["EhTerceiro"].ToString() != "")
                            oContaineres.EhTerceiro = Convert.ToInt16(dr["EhTerceiro"]);
                        oContaineres.Inativo = 0;
                        oContaineres.Numero = dr["Numero"].ToString();
                        oContaineres.Tipo = dr["Tipo"].ToString();
                        if (oContaineresDados.DadoExiste(oContaineres.Codigo) == "Alterar")
                            oContaineresDados.Alterar(oContaineres, oContaineres.Codigo);
                        else
                            oContaineresDados.Inserir(oContaineres, oContaineres.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " containeres";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoCaminhoes()
        {
            try
            {
                clsCaminhoes oCaminhoes = new clsCaminhoes();
                clsCaminhoesDados oCaminhoesDados = new clsCaminhoesDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Caminhoes order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oCaminhoes = new clsCaminhoes();
                        if (dr["Codigo"].ToString() != "")
                            oCaminhoes.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["AnoFabricacao"].ToString() != "")
                            oCaminhoes.AnoFabricacao = Convert.ToInt32(dr["AnoFabricacao"]);
                        if (dr["AnoModelo"].ToString() != "")
                            oCaminhoes.AnoModelo = Convert.ToInt32(dr["AnoModelo"]);
                        oCaminhoes.Chassi = dr["Chassi"].ToString();
                        oCaminhoes.Cidade = dr["Cidade"].ToString();
                        oCaminhoes.Cor = dr["Cor"].ToString();
                        if (dr["dtVctoSegr"].ToString() != "")
                            oCaminhoes.dtVctoSegr = Convert.ToDateTime(dr["dtVctoSegr"]).ToShortDateString();
                        if (dr["Kilometragem"].ToString() != "")
                            oCaminhoes.Kilometragem = Convert.ToInt32(dr["Kilometragem"]);
                        oCaminhoes.Marca = dr["Marca"].ToString();
                        oCaminhoes.Modelo = dr["Modelo"].ToString();
                        oCaminhoes.Placas = dr["Placas"].ToString();
                        oCaminhoes.Renavam = dr["Renavam"].ToString();
                        if (dr["ValorFranquia"].ToString() != "")
                            oCaminhoes.ValorFranquia = Convert.ToDecimal(dr["ValorFranquia"].ToString());
                        if (dr["VctoIPVA"].ToString() != "")
                            oCaminhoes.VctoIPVA = Convert.ToDateTime(dr["VctoIPVA"]).ToShortDateString();
                        if (dr["VctoLicenciamento"].ToString() != "")
                            oCaminhoes.VctoLicenciamento = Convert.ToDateTime(dr["VctoLicenciamento"]).ToShortDateString();
                        if (dr["dtVctoSegr"].ToString() != "")
                            oCaminhoes.VencimentoSeguroFrota = Convert.ToDateTime(dr["dtVctoSegr"]).ToShortDateString();

                        if (oCaminhoesDados.DadoExiste(oCaminhoes.Codigo) == "Alterar")
                            oCaminhoesDados.Alterar(oCaminhoes, oCaminhoes.Codigo);
                        else
                            oCaminhoesDados.Inserir(oCaminhoes, oCaminhoes.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Caminhões";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizaColocacoes()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Lancamentos \n";
                s = s + "where DtRetr is null \n";
                s = s + "or    DtRetr = #0001-01-01# \n";
                s = s + "or    DtRetr = #0100-01-01# \n";
                s = s + "or    DtRetr = #1900-01-01# \n";

                _dtAtualizacao = new DataTable();
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["nuLanc"].ToString() != "" && dr["nuLanc"].ToString() != "0")
                    {
                        oLancamentos = new clsLancamentos();
                        if (dr["cdCamhColc"].ToString() != "")
                            oLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(dr["cdCamhColc"]);
                        if (dr["cdCamhRetr"].ToString() != "")
                            oLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(dr["cdCamhRetr"]);
                        if (dr["cdClnt"].ToString() != "")
                            oLancamentos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);
                        if (dr["cdEndrObra"].ToString() != "")
                            oLancamentos.CodigoEnderecoObra = Convert.ToInt32(dr["cdEndrObra"]);
                        if (dr["cdMotr1"].ToString() != "")
                            oLancamentos.CodigoMotoristaColocou = Convert.ToInt32(dr["cdMotr1"]);
                        if (dr["cdMotr2"].ToString() != "")
                            oLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(dr["cdMotr2"]);
                        if (dr["dtLanc"].ToString() != "")
                            oLancamentos.Data = Convert.ToDateTime(dr["dtLanc"]).ToShortDateString();
                        if (dr["dtARetr"].ToString() != "")
                            oLancamentos.DataARetirar = Convert.ToDateTime(dr["dtARetr"]).ToShortDateString();
                        if (dr["dtColc"].ToString() != "")
                            oLancamentos.DataColocacao = Convert.ToDateTime(dr["dtColc"]).ToShortDateString();
                        if (dr["dtRetr"].ToString() != "")
                            oLancamentos.DataRetirada = Convert.ToDateTime(dr["dtRetr"]).ToShortDateString();
                        oLancamentos.HoraRetirada = dr["hsRetr"].ToString();
                        if (dr["Horas"].ToString() != "")
                            oLancamentos.Horas = Convert.ToInt32(dr["Horas"]);
                        oLancamentos.HorasARetirar = dr["hsARetr"].ToString();
                        oLancamentos.HorasColocacao = dr["hsColc"].ToString();
                        if (dr["NuLancColocacao"].ToString() != "")
                            oLancamentos.NuLancColocacao = Convert.ToInt32(dr["NuLancColocacao"]);
                        oLancamentos.NumeroCaixa = dr["nuCaxa"].ToString();
                        oLancamentos.OBS = dr["OBS"].ToString();
                        if (dr["TipoOperacao"].ToString() != "")
                            oLancamentos.TipoOperacao = Convert.ToInt32(dr["TipoOperacao"]);
                        if (dr["vlLocc"].ToString() != "")
                            oLancamentos.ValorLocacao = Convert.ToDecimal(dr["vlLocc"]);
                        oLancamentos.NumeroLancamento = Convert.ToInt32(dr["nuLanc"]);

                        if (oLancamentosDados.DadoExiste(oLancamentos.NumeroLancamento) == "Alterar")
                            oLancamentosDados.Alterar(oLancamentos, oLancamentos.NumeroLancamento);
                        else
                            oLancamentosDados.Inserir(oLancamentos, oLancamentos.NumeroLancamento);
                        i++;
                        lblMensagem.Text = "Colocação/lida: " + i.ToString() + " registros atualizados";
                        lblMensagem.Refresh();
                    }
                }
                // fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();
                lblMensagem.Text = "Atualização das Colocacões realizada com sucesso! " + i.ToString("000000") + " registros atualizados";
                bindingSource.DataSource = _dtAtualizacao;
                Grade.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoLancamentos()
        {
            try
            {
                if (int2Mes.VALOR.Text == "" || int2Ano.VALOR.Text == "")
                {
                    MessageBox.Show("Mês/Ano inválido(s)!");
                    return;    
                }
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Lancamentos \n";
                if (int2Mes.VALOR.Text != "" && int2Ano.VALOR.Text != "")
                {
                    s = s + "where  month(dtRetr) = " + int2Mes.VALOR.Text + " \n";
                    s = s + "and    year(dtRetr)  = " + "20" + int2Ano.VALOR.Text + " \n";
                }
                if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
                {
                    s = s + "and   cdClnt = " + cliente1.txtCodigo.Text;
                    oLancamentosMTRDados.ExcluirMesAno(Convert.ToInt32(cliente1.txtCodigo.Text), Convert.ToInt32(int2Mes.VALOR.Text), 2000 + Convert.ToInt32(int2Ano.VALOR.Text));
                }

                _dtAtualizacao = new DataTable();
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["nuLanc"].ToString() != "" && dr["nuLanc"].ToString() != "0")
                    {
                        oLancamentos = new clsLancamentos();
                        if (dr["nuLanc"].ToString() == "222871")
                        {
                            // esta linha é só pra debug
                            oLancamentos.NuLancColocacao = Convert.ToInt32(dr["NuLancColocacao"]);
                        }
                        if (dr["cdCamhColc"].ToString() != "")
                            oLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(dr["cdCamhColc"]);
                        if (dr["cdCamhRetr"].ToString() != "")
                            oLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(dr["cdCamhRetr"]);
                        if (dr["cdClnt"].ToString() != "")
                            oLancamentos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);
                        if (dr["cdEndrObra"].ToString() != "")
                            oLancamentos.CodigoEnderecoObra = Convert.ToInt32(dr["cdEndrObra"]);
                        if (dr["cdMotr1"].ToString() != "")
                            oLancamentos.CodigoMotoristaColocou = Convert.ToInt32(dr["cdMotr1"]);
                        if (dr["cdMotr2"].ToString() != "")
                            oLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(dr["cdMotr2"]);
                        if (dr["dtLanc"].ToString() != "")
                            oLancamentos.Data = Convert.ToDateTime(dr["dtLanc"]).ToShortDateString();
                        if (dr["dtARetr"].ToString() != "")
                            oLancamentos.DataARetirar = Convert.ToDateTime(dr["dtARetr"]).ToShortDateString();
                        if (dr["dtColc"].ToString() != "")
                            oLancamentos.DataColocacao = Convert.ToDateTime(dr["dtColc"]).ToShortDateString();
                        if (dr["dtRetr"].ToString() != "")
                            oLancamentos.DataRetirada = Convert.ToDateTime(dr["dtRetr"]).ToShortDateString();
                        oLancamentos.HoraRetirada = dr["hsRetr"].ToString();
                        if (dr["Horas"].ToString() != "")
                            oLancamentos.Horas = Convert.ToInt32(dr["Horas"]);
                        oLancamentos.HorasARetirar = dr["hsARetr"].ToString();
                        oLancamentos.HorasColocacao = dr["hsColc"].ToString();
                        if (dr["NuLancColocacao"].ToString() != "")
                            oLancamentos.NuLancColocacao = Convert.ToInt32(dr["NuLancColocacao"]);
                        oLancamentos.NumeroCaixa = dr["nuCaxa"].ToString();
                        oLancamentos.OBS = dr["OBS"].ToString();
                        if (dr["TipoOperacao"].ToString() != "")
                            oLancamentos.TipoOperacao = Convert.ToInt32(dr["TipoOperacao"]);
                        if (dr["vlLocc"].ToString() != "")
                            oLancamentos.ValorLocacao = Convert.ToDecimal(dr["vlLocc"]);
                        oLancamentos.NumeroLancamento = Convert.ToInt32(dr["nuLanc"]);

                        if (oLancamentosDados.DadoExiste(oLancamentos.NumeroLancamento) == "Alterar")
                            oLancamentosDados.Alterar(oLancamentos, oLancamentos.NumeroLancamento);
                        else
                            oLancamentosDados.Inserir(oLancamentos, oLancamentos.NumeroLancamento);

                        DataTable _dtLancamentoMTR = new DataTable();
                        s = "";
                        s = "select * from LancamentoMTR where NumeroLancamento = " + oLancamentos.NumeroLancamento;
                        OleDbDataAdapter oDAMTR = new OleDbDataAdapter(s, aConnection);
                        oDAMTR.Fill(_dtLancamentoMTR);
                        oDAMTR.Dispose();
                        foreach (DataRow _drMTR in _dtLancamentoMTR.Rows)
                        {
                            oLancamentoMTR = new clsLancamentoMTR();
                            if (_drMTR["CodigoAterroSanitario"].ToString() != "")
                                oLancamentoMTR.CodigoAterroSanitario = Convert.ToInt32(_drMTR["CodigoAterroSanitario"]);
                            oLancamentoMTR.CodigoResiduo = Convert.ToInt32(_drMTR["CodigoResiduo"]);
                            DataTable _dtAT = new DataTable();
                            s = "";
                            if (Convert.ToInt32(_drMTR["CodigoResiduo"]) > 0 && oLancamentos.NumeroLancamento > 0)
                            {
                                s = s + "select Data from AterroSanitario \n";
                                s = s + "where  NumeroLancamento = " + oLancamentos.NumeroLancamento + " \n";
                                s = s + "and    CodigoResiduo    = " + _drMTR["CodigoResiduo"].ToString();
                                OleDbDataAdapter oDAT = new OleDbDataAdapter(s, aConnection);
                                oDAT.Fill(_dtAT);
                                if (_dtAT.Rows.Count > 0)
                                    oLancamentoMTR.DataDescarga = Convert.ToDateTime(_dtAT.Rows[0]["Data"]).ToShortDateString();
                                else if (_drMTR["DataDescarga"].ToString() != "" && geral.Left(_drMTR["DataDescarga"].ToString(), 10) != "01/01/0100")
                                    oLancamentoMTR.DataDescarga = Convert.ToDateTime(_drMTR["DataDescarga"]).ToShortDateString();
                                oDAT.Dispose();
                            }
                            oLancamentoMTR.Deposito = _drMTR["Deposito"].ToString();
                            oLancamentoMTR.Franquia = Convert.ToDecimal(_drMTR["Franquia"]);
                            oLancamentoMTR.Motivo = _drMTR["Motivo"].ToString();
                            oLancamentoMTR.NumeroLancamento = oLancamentos.NumeroLancamento;
                            oLancamentoMTR.NumeroMTR = Convert.ToInt32(_drMTR["NumeroMTR"]);
                            if (_drMTR["NumeroMTRFatima"].ToString() != "")
                                oLancamentoMTR.NumeroMTRFatima = Convert.ToInt64(_drMTR["NumeroMTRFatima"]);
                            oLancamentoMTR.observacao = _drMTR["observacao"].ToString();
                            oLancamentoMTR.Quantidade = Convert.ToDecimal(_drMTR["Quantidade"]);
                            oLancamentoMTR.Ticket = _drMTR["Ticket"].ToString();
                            oLancamentoMTR.Unidade = _drMTR["Unidade"].ToString();
                            oLancamentoMTR.ValorTotal = Convert.ToDecimal(_drMTR["ValorTotal"]);
                            oLancamentoMTR.ValorUnitario = Convert.ToDecimal(_drMTR["ValorUnitario"]);
                            if (oLancamentosMTRDados.DadoExiste(oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo) == "Alterar")
                                oLancamentosMTRDados.Alterar(oLancamentoMTR, oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo);
                            else
                                oLancamentosMTRDados.Inserir(oLancamentoMTR);
                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " registros atualizados";
                            lblMensagem.Refresh();
                        }

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " registros atualizados";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();
                lblMensagem.Text = "Atualização realizada com sucesso! " + i.ToString("000000") + " registros atualizados";
                bindingSource.DataSource = _dtAtualizacao;
                Grade.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void cliente1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "CLIENTE";
            geral.CodigoCliente = 0;
        }

        private void frmAtualizacaoDados_Load(object sender, EventArgs e)
        {

        }
        private void AtualizacaoAbastecimentos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsAbastecimento oAbastecimento = new clsAbastecimento();
                clsAbastecimentoDados oAbastecimentoDados = new clsAbastecimentoDados();
                string s = "";
                s = s + "select * from Abastecimento order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oAbastecimento = new clsAbastecimento();
                        if (dr["Sequencial"].ToString() != "")
                            oAbastecimento.Sequencial = Convert.ToInt32(dr["Sequencial"]);

                        if (dr["AcumuladoAnterior"].ToString() != "")
                            oAbastecimento.AcumuladoAnterior = Convert.ToDecimal(dr["AcumuladoAnterior"]);

                        if (dr["AcumuladoAtual"].ToString() != "")
                            oAbastecimento.AcumuladoAtual = Convert.ToDecimal(dr["AcumuladoAtual"]);

                        if (dr["CodigoCaminhao"].ToString() != "")
                            oAbastecimento.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);

                        if (dr["CodigoMotorista"].ToString() != "")
                            oAbastecimento.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);

                        if (dr["CompraAculumadaDiesel"].ToString() != "")
                            oAbastecimento.CompraAculumadaDiesel = Convert.ToDecimal(dr["CompraAculumadaDiesel"]);

                        if (dr["CompraDiesel"].ToString() != "")
                            oAbastecimento.CompraDiesel = Convert.ToDecimal(dr["CompraDiesel"]);

                        if (dr["Data"].ToString() != "")
                            oAbastecimento.Data = Convert.ToDateTime(dr["Data"]).ToString("yyyy-MM-dd");

                        if (dr["DiferencaAcumuladaLitros"].ToString() != "")
                            oAbastecimento.DiferencaAcumuladaLitros = Convert.ToDecimal(dr["DiferencaAcumuladaLitros"]);

                        if (dr["DiferencaLitros"].ToString() != "")
                            oAbastecimento.DiferencaLitros = Convert.ToDecimal(dr["DiferencaLitros"]);

                        if (dr["Estoque"].ToString() != "")
                            oAbastecimento.Estoque = Convert.ToDecimal(dr["Estoque"]);

                        if (dr["flagAbastExterno"].ToString() != "")
                            oAbastecimento.flagAbastExterno = Convert.ToInt16(dr["flagAbastExterno"]);

                        oAbastecimento.Hora = dr["Hora"].ToString();

                        if (dr["Km"].ToString() != "")
                            oAbastecimento.Km = Convert.ToInt32(dr["Km"]);

                        if (dr["Litros"].ToString() != "")
                            oAbastecimento.Litros = Convert.ToDecimal(dr["Litros"]);

                        if (dr["PrecoLitro"].ToString() != "")
                            oAbastecimento.PrecoLitro = Convert.ToDecimal(dr["PrecoLitro"]);

                        if (dr["ValorCompraDiesel"].ToString() != "")
                            oAbastecimento.ValorCompraDiesel = Convert.ToDecimal(dr["ValorCompraDiesel"]);


                        if (oAbastecimentoDados.DadoExiste(oAbastecimento.Sequencial) == "Alterar")
                            oAbastecimentoDados.Alterar(oAbastecimento, oAbastecimento.Sequencial);
                        else
                            oAbastecimentoDados.Inserir(oAbastecimento, oAbastecimento.Sequencial);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Abastecimento";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoAliquotasImpostos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsAliquotaImpostos oAliquotaImpostos = new clsAliquotaImpostos();
                clsAliquotaImpostosDados oAliquotaImpostosDados = new clsAliquotaImpostosDados();
                string s = "";
                s = s + "select * from AliquotaImpostos order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oAliquotaImpostos = new clsAliquotaImpostos();
                        if (dr["Sequencial"].ToString() != "")
                            oAliquotaImpostos.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["AliquotaCOFINS_Retido"].ToString() != "")
                            oAliquotaImpostos.AliquotaCOFINS_Retido = Convert.ToDecimal(dr["AliquotaCOFINS_Retido"]);
                        if (dr["AliquotaContribSocial"].ToString() != "")
                            oAliquotaImpostos.AliquotaContribSocial = Convert.ToDecimal(dr["AliquotaContribSocial"]);
                        if (dr["AliquotaIR_Retido"].ToString() != "")
                            oAliquotaImpostos.AliquotaIR_Retido = Convert.ToDecimal(dr["AliquotaIR_Retido"]);
                        if (dr["AliquotaPIS_Retido"].ToString() != "")
                            oAliquotaImpostos.AliquotaPIS_Retido = Convert.ToDecimal(dr["AliquotaPIS_Retido"]);
                        oAliquotaImpostos.CodigoBROOKS = dr["CodigoBROOKS"].ToString();
                        if (dr["CodigoSituacaoTributaria"].ToString() != "")
                            oAliquotaImpostos.CodigoSituacaoTributaria = Convert.ToInt16(dr["CodigoSituacaoTributaria"]);
                        oAliquotaImpostos.Descricao = dr["Descricao"].ToString();
                        if (dr["ValorLimiteCRF"].ToString() != "")
                            oAliquotaImpostos.ValorLimiteCRF = Convert.ToDecimal(dr["ValorLimiteCRF"]);
                        if (dr["ValorLimiteIR"].ToString() != "")
                            oAliquotaImpostos.ValorLimiteIR = Convert.ToDecimal(dr["ValorLimiteIR"]);

                        if (oAliquotaImpostosDados.DadoExiste(oAliquotaImpostos.Sequencial) == "Alterar")
                            oAliquotaImpostosDados.Alterar(oAliquotaImpostos, oAliquotaImpostos.Sequencial);
                        else
                            oAliquotaImpostosDados.Inserir(oAliquotaImpostos, oAliquotaImpostos.Sequencial);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Alíquotas de Impostos";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoAterroSanitario()
        {
            if (int2Ano.VALOR.Text == "")
                MessageBox.Show("Ano inválido!");
            else if (int2Mes.VALOR.Text == "")
                MessageBox.Show("Mês inválido!");
            else
            {
                try
                {
                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();
                    clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
                    clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
                    string s = "";
                    s = s + "select * from AterroSanitario \n";
                    s = s + "where  year(Data) = 20" + int2Ano.VALOR.Text + " \n";
                    s = s + "and    month(Data) = " + int2Mes.VALOR.Text + " \n";
                    s = s + "order  by Codigo  \n";
                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                    _dtAtualizacao = new DataTable();
                    oDataAdapter.Fill(_dtAtualizacao);
                    oDataAdapter.Dispose();

                    oAterroSanitarioDados.ExcluirMesAno(int2Mes.VALOR.Text, int2Ano.VALOR.Text);
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                        {
                            oAterroSanitario = new clsAterroSanitario();
                            if (dr["Codigo"].ToString() != "")
                                oAterroSanitario.Codigo = Convert.ToInt32(dr["Codigo"]);
                            if (dr["CodigoAterro"].ToString() != "")
                                oAterroSanitario.CodigoAterro = Convert.ToInt32(dr["CodigoAterro"]);
                            if (dr["CodigoCaminhao"].ToString() != "")
                                oAterroSanitario.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                            if (dr["CodigoCliente"].ToString() != "")
                                oAterroSanitario.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                            if (dr["CodigoMotorista"].ToString() != "")
                                oAterroSanitario.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                            if (dr["CodigoResiduo"].ToString() != "")
                                oAterroSanitario.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                            oAterroSanitario.ContainerDescricao = dr["ContainerDescricao"].ToString();
                            if (dr["Data"].ToString() != "")
                                oAterroSanitario.Data = Convert.ToDateTime(dr["Data"]).ToShortDateString();
                            if (dr["Excluido"].ToString() != "")
                                oAterroSanitario.Excluido = Convert.ToInt16(dr["Excluido"]);
                            oAterroSanitario.Hora = dr["Hora"].ToString();
                            oAterroSanitario.LocalAterro = dr["LocalAterro"].ToString();
                            oAterroSanitario.NumeroCaixa = dr["NumeroCaixa"].ToString();
                            if (dr["NumeroLancamento"].ToString() != "")
                                oAterroSanitario.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                            if (dr["NumeroMTR"].ToString() != "")
                                oAterroSanitario.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"]);
                            oAterroSanitario.NumeroTicket = dr["NumeroTicket"].ToString();
                            if (dr["Status"].ToString() != "")
                                oAterroSanitario.Status = Convert.ToInt32(dr["Status"]);
                            if (dr["TotalPeso"].ToString() != "")
                                oAterroSanitario.TotalPeso = Convert.ToDecimal(dr["TotalPeso"]);

                            if (oAterroSanitarioDados.DadoExiste(oAterroSanitario.Codigo) == "Alterar")
                                oAterroSanitarioDados.Alterar(oAterroSanitario, oAterroSanitario.Codigo);
                            else
                                oAterroSanitarioDados.Inserir(oAterroSanitario, oAterroSanitario.Codigo);
                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Aterro Sanitário";
                            lblMensagem.Refresh();
                        }
                    }
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();

                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        private void AtualizacaoBlocosMTR()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text.Replace("SILC.MDB", "SILCDB2.MDB"));
                aConnection.Open();
                clsBlocosMTR oBlocosMTR = new clsBlocosMTR();
                clsBlocosMTRDados oBlocosMTRDados = new clsBlocosMTRDados();
                string s = "";
                s = s + "select * from BlocosMTR order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oBlocosMTR = new clsBlocosMTR();
                        if (dr["Sequencial"].ToString() != "")
                            oBlocosMTR.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["Data"].ToString() != "" && dr["Data"].ToString() != "0001-01-01" && dr["Data"].ToString() != "0100-01-01")
                            oBlocosMTR.Data = Convert.ToDateTime(dr["Data"]).ToShortDateString();
                        if (dr["CodigoMotorista"].ToString() != "")
                            oBlocosMTR.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        if (dr["NumeroBloco"].ToString() != "")
                            oBlocosMTR.NumeroBloco = Convert.ToInt32(dr["NumeroBloco"]);
                        if (dr["NuMTRInicial"].ToString() != "")
                            oBlocosMTR.NuMTRInicial = Convert.ToInt32(dr["NuMTRInicial"]);
                        if (dr["NuMTRFinal"].ToString() != "")
                            oBlocosMTR.NuMTRFinal = Convert.ToInt32(dr["NuMTRFinal"]);
                        
                        //no sistema velho não têm esse coeficiente
                        //if (dr["CoeficienteNumeracao"].ToString() != "")
                        //    oBlocosMTR.CoeficienteNumeracao = Convert.ToInt32(dr["CoeficienteNumeracao"]);

                        if (oBlocosMTRDados.DadoExiste(oBlocosMTR.Sequencial) == "Alterar")
                            oBlocosMTRDados.Alterar(oBlocosMTR, oBlocosMTR.Sequencial);
                        else
                            oBlocosMTRDados.Inserir(oBlocosMTR, oBlocosMTR.Sequencial);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Blocos MTR";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoBloqueioFinanceiro()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsBloqueioFinanceiro oBloqueioFinanceiro = new clsBloqueioFinanceiro();
                clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();
                string s = "";
                s = s + "select * from BloqueioFinanceiro order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oBloqueioFinanceiro = new clsBloqueioFinanceiro();
                        if (dr["Sequencial"].ToString() != "")
                            oBloqueioFinanceiro.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oBloqueioFinanceiro.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["CodigoUsuario"].ToString() != "")
                            oBloqueioFinanceiro.CodigoUsuario = Convert.ToInt32(dr["CodigoUsuario"]);
                        if (dr["DataBloqueio"].ToString() != "" && dr["DataBloqueio"].ToString() != "0001-01-01" && dr["DataBloqueio"].ToString() != "0100-01-01")
                            oBloqueioFinanceiro.DataBloqueio = Convert.ToDateTime(dr["DataBloqueio"]).ToShortDateString();
                        if (dr["DataDesbloqueio"].ToString() != "" && dr["DataDesbloqueio"].ToString() != "0001-01-01" && dr["DataDesbloqueio"].ToString() != "0100-01-01")
                            oBloqueioFinanceiro.DataDesbloqueio = Convert.ToDateTime(dr["DataDesbloqueio"]).ToShortDateString();
                        oBloqueioFinanceiro.Observacao = dr["Observacao"].ToString();

                        if (oBloqueioFinanceiroDados.DadoExiste(oBloqueioFinanceiro.Sequencial) == "Alterar")
                            oBloqueioFinanceiroDados.Alterar(oBloqueioFinanceiro, oBloqueioFinanceiro.Sequencial);
                        else
                            oBloqueioFinanceiroDados.Inserir(oBloqueioFinanceiro, oBloqueioFinanceiro.Sequencial);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Bloqueio Financeiro";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoCodigoServicosPrefeitura()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsCodigoServicosPrefeitura oCodigoServicosPrefeitura = new clsCodigoServicosPrefeitura();
                clsCodigoServicosPrefeituraDados oCodigoServicosPrefeituraDados = new clsCodigoServicosPrefeituraDados();
                string s = "";
                s = s + "select * from CodigoServicosPrefeitura order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oCodigoServicosPrefeitura = new clsCodigoServicosPrefeitura();
                        if (dr["Sequencial"].ToString() != "")
                            oCodigoServicosPrefeitura.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["Codigo"].ToString() != "")
                            oCodigoServicosPrefeitura.Codigo = Convert.ToInt32(dr["Codigo"]);
                        oCodigoServicosPrefeitura.DescricaoServico = dr["DescricaoServico"].ToString();
                        oCodigoServicosPrefeitura.NomePrefeitura = dr["NomePrefeitura"].ToString();

                        if (oCodigoServicosPrefeituraDados.DadoExiste(oCodigoServicosPrefeitura.Sequencial) == "Alterar")
                            oCodigoServicosPrefeituraDados.Alterar(oCodigoServicosPrefeitura, oCodigoServicosPrefeitura.Sequencial);
                        else
                            oCodigoServicosPrefeituraDados.Inserir(oCodigoServicosPrefeitura, oCodigoServicosPrefeitura.Sequencial);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Código Serviços Prefeitura ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoConfigSis()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsConfigSis oConfigSis = new clsConfigSis();
                clsConfigSisDados oConfigSisDados = new clsConfigSisDados();
                string s = "";
                s = s + "select * from ConfigSis \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                oConfigSisDados.Excluir();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    oConfigSis = new clsConfigSis();
                    if (dr["Ativo"].ToString() != "")
                        oConfigSis.Ativo = Convert.ToInt32(dr["Ativo"]);
                    oConfigSis.Campo = dr["Campo"].ToString();

                    oConfigSisDados.Inserir(oConfigSis);

                    i++;
                    lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " ConfigSis ";
                    lblMensagem.Refresh();
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoCorpoNotasFiscais()
        {
            try
            {
                if (int2Ano.VALOR.Text == "")
                    MessageBox.Show("Ano inválido!");
                else if (int2Mes.VALOR.Text == "")
                    MessageBox.Show("Mês inválido!");
                else
                {
                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();
                    clsCorpoNotasFiscais oCorpoNotasFiscais = new clsCorpoNotasFiscais();
                    clsCorpoNotasFiscaisDados oCorpoNotasFiscaisDados = new clsCorpoNotasFiscaisDados();
                    string s = "";
                    s = s + "select cnf.* from CorpoNotasFiscais cnf \n";
                    s = s + "inner join NotasFiscais nf on nf.NumeroNotaFiscal = cnf.NumeroNotaFiscal \n";
                    s = s + "where year(DataEmissao) = 20" + int2Ano.VALOR.Text + " \n";
                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                    _dtAtualizacao = new DataTable();
                    oDataAdapter.Fill(_dtAtualizacao);
                    oDataAdapter.Dispose();

                    int iSqlInsert = 0;
                    int iSqlAlter = 0;
                    string sAlter = "";
                    string sInsert = "";
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["NumeroNotaFiscal"].ToString() != "" && dr["NumeroNotaFiscal"].ToString() != "0")
                        {
                            oCorpoNotasFiscais = new clsCorpoNotasFiscais();
                            if (dr["NumeroNotaFiscal"].ToString() != "")
                                oCorpoNotasFiscais.SequencialNotaFiscal = Convert.ToInt32(dr["NumeroNotaFiscal"]);
                            oCorpoNotasFiscais.Descricao = dr["Descricao"].ToString();
                            if (dr["Linha"].ToString() != "")
                                oCorpoNotasFiscais.Linha = Convert.ToInt32(dr["Linha"]);
                            if (dr["PrecoUnitario"].ToString() != "")
                                oCorpoNotasFiscais.PrecoUnitario = Convert.ToDecimal(dr["PrecoUnitario"]);
                            if (dr["Quantidade"].ToString() != "")
                                oCorpoNotasFiscais.Quantidade = Convert.ToDecimal(dr["Quantidade"]);
                            oCorpoNotasFiscais.Unidade = dr["Unidade"].ToString();
                            if (dr["Valor"].ToString() != "")
                                oCorpoNotasFiscais.Valor = Convert.ToDecimal(dr["Valor"]);
                            if (oCorpoNotasFiscaisDados.DadoExiste(oCorpoNotasFiscais.SequencialNotaFiscal, oCorpoNotasFiscais.Linha) == "Alterar")
                            {
                                iSqlAlter++;
                                if (iSqlAlter <= 1000)
                                    sAlter = sAlter + oCorpoNotasFiscaisDados.Alterar(oCorpoNotasFiscais, oCorpoNotasFiscais.SequencialNotaFiscal, oCorpoNotasFiscais.Linha, true);
                                else
                                {
                                    oCorpoNotasFiscaisDados.SqlAlter(sAlter);
                                    iSqlAlter = 0;
                                    sAlter = "";
                                }
                            }
                            else
                            {
                                iSqlInsert++;
                                if (iSqlInsert < 1000)
                                    sInsert = sInsert + oCorpoNotasFiscaisDados.Inserir(oCorpoNotasFiscais, oCorpoNotasFiscais.SequencialNotaFiscal, true);
                                else
                                {
                                    oCorpoNotasFiscaisDados.SqlInsert(sInsert);
                                    sInsert = "";
                                    iSqlInsert = 0;
                                }
                            }

                            i++;
                            //lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Corpo notas fiscais. ";
                            lblMensagem.Text = i.ToString();
                            lblMensagem.Refresh();
                        }
                    }
                    if (iSqlAlter > 0 && sAlter != "")
                        oCorpoNotasFiscaisDados.SqlAlter(sAlter);
                    if (iSqlInsert > 0 && sInsert != "")
                        oCorpoNotasFiscaisDados.SqlInsert(sInsert);

                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();

                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }        
        }
        private void AtualizacaoDTR()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsDTR oDTR = new clsDTR();
                clsDTRDados oDTRDados = new clsDTRDados();
                string s = "";
                s = s + "select * from DTR  \n";
                s = s + "where  year(DataSaida) = 20" + int2Ano.VALOR.Text + " \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                if (int2Ano.VALOR.Text != "")
                {
                    oDTRDados.ExcluirAno(int2Ano.VALOR.Text);
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                        {
                            oDTR = new clsDTR();
                            if (dr["Sequencial"].ToString() != "")
                                oDTR.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                            oDTR.ClienteColetado = dr["ClienteColetado"].ToString();
                            if (dr["CodigoCaminhao"].ToString() != "")
                                oDTR.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                            if (dr["CodigoClienteColetado"].ToString() != "")
                                oDTR.CodigoClienteColetado = Convert.ToInt32(dr["CodigoClienteColetado"]);
                            if (dr["CodigoClienteEmitiuMTR"].ToString() != "")
                                oDTR.CodigoClienteEmitiuMTR = Convert.ToInt32(dr["CodigoClienteEmitiuMTR"]);
                            if (dr["CodigoLocalEntrega"].ToString() != "")
                                oDTR.CodigoLocalEntrega = Convert.ToInt32(dr["CodigoLocalEntrega"]);
                            if (dr["CodigoMotorista"].ToString() != "")
                                oDTR.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                            if (dr["CodigoTipoResiduo"].ToString() != "")
                                oDTR.CodigoTipoResiduo = Convert.ToInt32(dr["CodigoTipoResiduo"]);
                            if (dr["DataColeta"].ToString() != "")
                                oDTR.DataColeta = Convert.ToDateTime(dr["DataColeta"]).ToShortDateString();
                            if (dr["DataSaida"].ToString() != "")
                                oDTR.DataSaida = Convert.ToDateTime(dr["DataSaida"]).ToShortDateString();
                            if (dr["Fechado"].ToString() != "")
                                oDTR.Fechado = Convert.ToInt16(dr["Fechado"]);
                            oDTR.HoraSaida = dr["HoraSaida"].ToString();
                            if (dr["Imprimido"].ToString() != "")
                                oDTR.Imprimido = Convert.ToInt16(dr["Imprimido"]);
                            oDTR.LocalDTR = dr["LocalDTR"].ToString();
                            oDTR.LocalEntrega = dr["LocalEntrega"].ToString();
                            if (dr["NumeroImpressao"].ToString() != "")
                                oDTR.NumeroImpressao = Convert.ToInt32(dr["NumeroImpressao"]);
                            if (dr["NumeroLancamento"].ToString() != "")
                                oDTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                            if (dr["NumeroMTR"].ToString() != "")
                                oDTR.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"]);
                            if (dr["QtNova"].ToString() != "")
                                oDTR.QtNova = Convert.ToDecimal(dr["QtNova"]);
                            oDTR.TipoResiduo = dr["TipoResiduo"].ToString();
                            if (dr["TotalKg"].ToString() != "")
                                oDTR.TotalKg = Convert.ToDecimal(dr["TotalKg"]);
                            if (dr["TotalQtNova"].ToString() != "")
                                oDTR.TotalQtNova = Convert.ToDecimal(dr["TotalQtNova"]);

                            if (oDTRDados.DadoExiste(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR) == "Alterar")
                            {
                                oDTRDados.Alterar(oDTR, oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR);
                            }
                            else
                            {
                                oDTRDados.Inserir(oDTR);
                            }
                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " DTR. ";
                            lblMensagem.Refresh();
                        }
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoContratoResiduos()
        {
            if (cliente1.txtCodigo.Text != "")
            {
                try
                {
                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();
                    clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
                    clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
                    string s = "";
                    s = s + "select * from ContratoResiduos \n";
                    s = s + "where CodigoCliente = " + cliente1.txtCodigo.Text + " \n";
                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                    _dtAtualizacao = new DataTable();
                    oDataAdapter.Fill(_dtAtualizacao);
                    oDataAdapter.Dispose();
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0" &&
                            dr["CodigoResiduo"].ToString() != "" && dr["CodigoResiduo"].ToString() != "0" &&
                            dr["CodigoContrato"].ToString() != "" && dr["CodigoContrato"].ToString() != "0" &&
                            dr["DataReajuste"].ToString() != "")
                        {
                            oContratoResiduos = new clsContratoResiduos();
                            if (dr["CaixaDisponivel"].ToString() != "")
                                oContratoResiduos.CaixaDisponivel = Convert.ToInt32(dr["CaixaDisponivel"]);
                            if (dr["CodigoCaminhao"].ToString() != "")
                                oContratoResiduos.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                            if (dr["CodigoCliente"].ToString() != "")
                                oContratoResiduos.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                            if (dr["CodigoContrato"].ToString() != "")
                                oContratoResiduos.CodigoContrato = Convert.ToInt32(dr["CodigoContrato"]);
                            if (dr["CodigoResiduo"].ToString() != "")
                                oContratoResiduos.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                            if (dr["DataReajuste"].ToString() != "")
                                oContratoResiduos.DataReajuste = Convert.ToDateTime(dr["DataReajuste"]).ToShortDateString();
                            oContratoResiduos.DiasColeta = dr["DiasColeta"].ToString();
                            oContratoResiduos.Franquia = dr["Franquia"].ToString();
                            oContratoResiduos.FrequenciaColeta = dr["FrequenciaColeta"].ToString();
                            oContratoResiduos.MesAnoBase = dr["MesAnoBase"].ToString();
                            oContratoResiduos.OBS = dr["OBS"].ToString();
                            if (dr["QuantidadeFranquia"].ToString() != "")
                                oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(dr["QuantidadeFranquia"]);
                            oContratoResiduos.Roteiro = dr["Roteiro"].ToString();
                            oContratoResiduos.TipoCaixa = dr["TipoCaixa"].ToString();
                            oContratoResiduos.Unidade = dr["Unidade"].ToString();

                            string _s_vu = dr["ValorUnitario"].ToString();
                            // cuidado só funciona assim! não dar replace para nulo no alterar
                            oContratoResiduos.ValorUnitario = Convert.ToDecimal(_s_vu) * 100;
                            if (oContratoResiduosDados.DadoExiste(oContratoResiduos.CodigoContrato, oContratoResiduos.CodigoResiduo,
                                                                  oContratoResiduos.DataReajuste, oContratoResiduos.CodigoCliente) == "Alterar")
                                oContratoResiduosDados.Alterar(oContratoResiduos, oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
                            else
                                oContratoResiduosDados.Inserir(oContratoResiduos);

                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Contrato por resíduos ";
                            lblMensagem.Refresh();
                        }
                    }
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();

                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }

        private void AtualizacaoContratos()
        {
            if (cliente1.txtCodigo.Text != "")
            {
                try
                {

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();
                    clsContratos oContratos = new clsContratos();
                    clsContratosDados oContratosDados = new clsContratosDados();
                    string s = "";
                    s = s + "select * from Precos \n";
                    s = s + "where cdclnt = " + cliente1.txtCodigo.Text + " \n";
                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                    _dtAtualizacao = new DataTable();
                    oDataAdapter.Fill(_dtAtualizacao);
                    oDataAdapter.Dispose();
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["cdClnt"].ToString() != "" && dr["cdClnt"].ToString() != "0" &&
                            dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                        {
                            oContratos = new clsContratos();
                            oContratos.AniversarioReajuste = dr["AniversarioReajuste"].ToString();
                            if (dr["ApuracaoA"].ToString() != "")
                                oContratos.ApuracaoA = Convert.ToInt32(dr["ApuracaoA"]);
                            if (dr["ApuracaoDe"].ToString() != "")
                                oContratos.ApuracaoDe = Convert.ToInt32(dr["ApuracaoDe"]);
                            if (dr["AvisarDiasAntes"].ToString() != "")
                                oContratos.AvisarDiasAntes = Convert.ToInt16(dr["AvisarDiasAntes"]);
                            if (dr["BrooksEnviaPLANFAT"].ToString() != "")
                                oContratos.BrooksEnviaPLANFAT = Convert.ToInt16(dr["BrooksEnviaPLANFAT"]);
                            if (dr["ClienteAprovaPLANFAT"].ToString() != "")
                                oContratos.ClienteAprovaPLANFAT = Convert.ToInt16(dr["ClienteAprovaPLANFAT"]);
                            if (dr["ClienteEnviaOC"].ToString() != "")
                                oContratos.ClienteEnviaOC = Convert.ToInt16(dr["ClienteEnviaOC"]);
                            if (dr["ClienteRecebeNFDocs"].ToString() != "")
                                oContratos.ClienteRecebeNFDocs = Convert.ToInt16(dr["ClienteRecebeNFDocs"]);
                            if (dr["Codigo"].ToString() != "")
                                oContratos.Codigo = Convert.ToInt32(dr["Codigo"]);
                            if (dr["cdClnt"].ToString() != "")
                                oContratos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);
                            if (dr["DataInicio"].ToString() != "" && dr["DataInicio"].ToString() != "0100-01-01")
                                oContratos.DataInicio = Convert.ToDateTime(dr["DataInicio"]).ToShortDateString();
                            if (dr["DataReajuste"].ToString() != "" && dr["DataReajuste"].ToString() != "0100-01-01")
                                oContratos.DataReajuste = Convert.ToDateTime(dr["DataReajuste"]).ToShortDateString();
                            if (dr["DataRecisao"].ToString() != "" && dr["DataRecisao"].ToString() != "0100-01-01")
                                oContratos.DataRecisao = Convert.ToDateTime(dr["DataRecisao"]).ToShortDateString();
                            if (dr["DataRegistro"].ToString() != "" && dr["DataRegistro"].ToString() != "0100-01-01")
                                oContratos.DataRegistro = Convert.ToDateTime(dr["DataRegistro"]).ToShortDateString();
                            if (dr["dtTerm"].ToString() != "" && dr["dtTerm"].ToString() != "0100-01-01")
                                oContratos.DataTermino = Convert.ToDateTime(dr["dtTerm"]).ToShortDateString();
                            oContratos.DescricaoHistorico = dr["deHist"].ToString();
                            if (dr["nuDiasMes"].ToString() != "")
                                oContratos.DiaVencimento = Convert.ToInt16(dr["nuDiasMes"]);
                            if (dr["EmiteMTR"].ToString() != "")
                                oContratos.EmiteMTR = Convert.ToInt16(dr["EmiteMTR"]);
                            if (dr["EnviarCDF"].ToString() != "")
                                oContratos.EnviarCDF = Convert.ToInt16(dr["EnviarCDF"]);
                            if (dr["EnviarDDR"].ToString() != "")
                                oContratos.EnviarDDR = Convert.ToInt16(dr["EnviarDDR"]);
                            if (dr["EnviarPLANFAT"].ToString() != "")
                                oContratos.EnviarPLANFAT = Convert.ToInt16(dr["EnviarPLANFAT"]);
                            if (dr["EnviarRELGER"].ToString() != "")
                                oContratos.EnviarRELGER = Convert.ToInt16(dr["EnviarRELGER"]);
                            oContratos.IndiceReajuste = dr["IndiceReajuste"].ToString();
                            oContratos.MotivoRescisao = dr["MotivoRescisao"].ToString();
                            if (dr["nuCaxaLocd"].ToString() != "")
                                oContratos.NumeroCaixasLocadas = Convert.ToInt16(dr["nuCaxaLocd"]);
                            if (dr["NuCont"].ToString() != "")
                                oContratos.NumeroContrato = Convert.ToInt32(dr["NuCont"]);
                            oContratos.Observacao = dr["Observacao"].ToString();
                            oContratos.SituacaoRecisao = dr["SituacaoRecisao"].ToString();
                            if (dr["vlCont"].ToString() != "")
                                oContratos.ValorContrato = Convert.ToDecimal(dr["vlCont"]);

                            if (oContratosDados.DadoExiste(oContratos.Codigo) == "Alterar")
                                oContratosDados.Alterar(oContratos, oContratos.Codigo, oContratos.CodigoCliente);
                            else
                                oContratosDados.Inserir(oContratos, oContratos.Codigo);
                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Contrato por resíduos ";
                            lblMensagem.Refresh();
                        }
                    }
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();

                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        private void AtualizacaoContratosReajustes()
        {
            if (cliente1.txtCodigo.Text != "")
            {

                //try
                //{
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsContratosReajustes oContratosReajustes = new clsContratosReajustes();
                clsContratosReajustesDados oContratosReajustesDados = new clsContratosReajustesDados();
                string s = "";
                s = s + "select * from Reajustes \n";
                s = s + "inner join Precos on Reajustes.CodigoPreco = Precos.Codigo \n";
                s = s + "where cdclnt = " + cliente1.txtCodigo.Text + " \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["CodigoPreco"].ToString() != "" && dr["CodigoPreco"].ToString() != "0")
                    {
                        oContratosReajustes = new clsContratosReajustes();
                        if (dr["CodigoPreco"].ToString() != "")
                            oContratosReajustes.CodigoContrato = Convert.ToInt32(dr["CodigoPreco"]);
                        if (dr["Data"].ToString() != "")
                            oContratosReajustes.Data = Convert.ToDateTime(dr["Data"]).ToShortDateString();
                        oContratosReajustes.NumeroContrato = dr["NumeroContrato"].ToString();

                        oContratosReajustes.Situacao = dr["Situacao"].ToString();
                        oContratosReajustes.TipoNegociacao = dr["TipoNegociacao"].ToString();
                        if (dr["Valor"].ToString() != "")
                            oContratosReajustes.Valor = Convert.ToDecimal(dr["Valor"]);
                        if (oContratosReajustesDados.DadoExiste(oContratosReajustes.CodigoContrato, oContratosReajustes.Data, oContratosReajustes.NumeroContrato) == "Alterar")
                            oContratosReajustesDados.Alterar(oContratosReajustes, oContratosReajustes.CodigoContrato, 0);
                        else
                            oContratosReajustesDados.Inserir(oContratosReajustes);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Contrato - Reajustes ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

                ///}
                //catch (Exception ex)
                //{
                //    lblMensagem.Text = ex.Message;
                //}
            }
        }
        private void AtualizacaoDescricaoServicos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsDescricaoServicos oDescricaoServicos = new clsDescricaoServicos();
                clsDescricaoServicosDados oDescricaoServicosDados = new clsDescricaoServicosDados();
                string s = "";
                s = s + "select * from DescricaoServicos  \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oDescricaoServicos = new clsDescricaoServicos();
                        if (dr["Sequencial"].ToString() != "")
                            oDescricaoServicos.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["CodigoServicoPrefeitura"].ToString() != "")
                            oDescricaoServicos.CodigoServicoPrefeitura = Convert.ToInt32(dr["CodigoServicoPrefeitura"]);
                        oDescricaoServicos.DescricaoServico = dr["DescricaoServico"].ToString();
                        if (oDescricaoServicosDados.DadoExiste(oDescricaoServicos.Sequencial) == "Alterar")
                        {
                            oDescricaoServicosDados.Alterar(oDescricaoServicos, oDescricaoServicos.Sequencial);
                        }
                        else
                            oDescricaoServicosDados.Inserir(oDescricaoServicos);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Descrição serviços. ";
                        lblMensagem.Refresh();
                    }
                }                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        /*
        private void AtualizacaoDocumentacaoAplicavel()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsDocumentacaoAplicavel oDocumentacaoAplicavel = new clsDocumentacaoAplicavel();
                clsDocumentacaoAplicavelDados oDocumentacaoAplicavelDados = new clsDocumentacaoAplicavelDados();
                string s = "";
                s = s + "select * from DocumentacaoAplicavel \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oDocumentacaoAplicavel = new clsDocumentacaoAplicavel();
                        if (dr["Sequencial"].ToString() != "")
                            oDocumentacaoAplicavel.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["AguardarAprovacaoPlanFat"].ToString() != "")
                            oDocumentacaoAplicavel.AguardarAprovacaoPlanFat = Convert.ToInt16(dr["AguardarAprovacaoPlanFat"]);
                        if (dr["AguardarOrdemCompra"].ToString() != "")
                            oDocumentacaoAplicavel.AguardarOrdemCompra = Convert.ToInt16(dr["AguardarOrdemCompra"]);
                        if (dr["Ano"].ToString() != "")
                            oDocumentacaoAplicavel.Ano = Convert.ToInt16(dr["Ano"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oDocumentacaoAplicavel.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["ConferindoDDR"].ToString() != "")
                            oDocumentacaoAplicavel.ConferindoDDR = Convert.ToInt16(dr["ConferindoDDR"]);
                        if (dr["ConferirDDRAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.ConferirDDRAteDia = Convert.ToInt16(dr["ConferirDDRAteDia"]);
                        if (dr["EnviarCDFBrooks"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarCDFBrooks = Convert.ToInt16(dr["EnviarCDFBrooks"]);
                        if (dr["EnviarPlanFatAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarPlanFatAteDia = Convert.ToInt16(dr["EnviarPlanFatAteDia"]);
                        if (dr["EnviarRelGer"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarRelGer = Convert.ToInt16(dr["EnviarRelGer"]);
                        if (dr["EnviarRGRAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarRGRAteDia = Convert.ToInt16(dr["EnviarRGRAteDia"]);
                        if (dr["Mes"].ToString() != "")
                            oDocumentacaoAplicavel.Mes = Convert.ToInt16(dr["Mes"]);
                        oDocumentacaoAplicavel.PeriodoApuracao = dr["PeriodoApuracao"].ToString();

                        if (oDocumentacaoAplicavelDados.DadoExiste(oDocumentacaoAplicavel.Sequencial) == "Alterar")
                        {
                            oDocumentacaoAplicavelDados.Alterar(oDocumentacaoAplicavel);
                        }
                        else
                            oDocumentacaoAplicavelDados.Inserir(oDocumentacaoAplicavel, oDocumentacaoAplicavel.Sequencial);

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Descrição serviços. ";
                        lblMensagem.Refresh();
                    }
                }                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        */


        private void AtualizacaoLAOs()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsLicencaAmbiental oLicencaAmbiental = new clsLicencaAmbiental();
                clsLicencaAmbientalDados oLicencaAmbientalDados = new clsLicencaAmbientalDados();
                string s = "";
                s = s + "select * from LicencaAmbiental \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["CodigoAterro"].ToString() != "" && dr["CodigoAterro"].ToString() != "0" &&
                        dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {

                        oLicencaAmbiental = new clsLicencaAmbiental();
                        if (dr["CodigoAterro"].ToString() != "")
                            oLicencaAmbiental.CodigoAterro = Convert.ToInt32(dr["CodigoAterro"]);
                        if (dr["Codigo"].ToString() != "")
                            oLicencaAmbiental.Codigo = Convert.ToInt32(dr["Codigo"]);
                        oLicencaAmbiental.CodigoAtividade = dr["CodigoAtividade"].ToString();
                        oLicencaAmbiental.NumeroLicenca = dr["NumeroLicenca"].ToString();
                        oLicencaAmbiental.Obs = dr["Obs"].ToString();
                        if (dr["PrazoValidade"].ToString() != "")
                            oLicencaAmbiental.PrazoValidade = Convert.ToDateTime(dr["PrazoValidade"]).ToString("yyyy-MM-dd");
                        if (oLicencaAmbientalDados.DadoExiste(oLicencaAmbiental.Codigo) == "Alterar")
                            oLicencaAmbientalDados.Alterar(oLicencaAmbiental, oLicencaAmbiental.Codigo);
                        else
                            oLicencaAmbientalDados.Inserir(oLicencaAmbiental, oLicencaAmbiental.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Licença ambiental ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void AtualizacaoNotasFiscais()
        {
            try
            {
                if (int2Ano.VALOR.Text == "")
                    MessageBox.Show("Ano inválido!");
                else if (int2Mes.VALOR.Text == "")
                    MessageBox.Show("Mês inválido!");
                else
                {
                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();
                    clsNotasFiscais oNF = new clsNotasFiscais();
                    clsNotasFiscaisDados oNFdados = new clsNotasFiscaisDados();
                    string s = "";
                    s = s + "select * from NotasFiscais \n";
                    s = s + "where  year(DataEmissao) = 20" + int2Ano.VALOR.Text + " \n";
                    s = s + "and    month(DataEmissao) = " + int2Mes.VALOR.Text + " \n";

                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                    _dtAtualizacao = new DataTable();
                    oDataAdapter.Fill(_dtAtualizacao);
                    oDataAdapter.Dispose();
                    foreach (DataRow dr in _dtAtualizacao.Rows)
                    {
                        if (dr["NumeroNF"].ToString() != "")
                        {
                            oNF = new clsNotasFiscais();
                            if (dr["ArquivoParaImportacaoGerado"].ToString() != "")
                                oNF.ArquivoParaImportacaoGerado = Convert.ToInt32(dr["ArquivoParaImportacaoGerado"]);
                            if (dr["Cancelada"].ToString() != "")
                                oNF.Cancelada = Convert.ToInt32(dr["Cancelada"]);
                            if (dr["Classificacao"].ToString() != "")
                                oNF.Classificacao = Convert.ToInt32(dr["Classificacao"]);
                            oNF.CodigoBROOKS_Impostos = dr["CodigoBROOKS_Impostos"].ToString();
                            if (dr["CodigoCliente"].ToString() != "")
                                oNF.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                            if (dr["ContaGerencial"].ToString() != "")
                                oNF.ContaGerencial = Convert.ToInt32(dr["ContaGerencial"]);
                            if (dr["DataEmissao"].ToString() != "")
                                oNF.DataEmissao = Convert.ToDateTime(dr["DataEmissao"]).ToString("yyyy-MM-dd");
                            if (dr["DataReferencia"].ToString() != "")
                                oNF.DataReferencia = Convert.ToDateTime(dr["DataReferencia"]).ToString("yyyy-MM-dd");
                            if (dr["DiasEntreVctos"].ToString() != "")
                                oNF.DiasEntreVctos = Convert.ToInt32(dr["DiasEntreVctos"]);
                            oNF.Historico = dr["Historico"].ToString();
                            if (dr["NFImpressa"].ToString() != "")
                                oNF.NFImpressa = Convert.ToInt32(dr["NFImpressa"]);
                            if (dr["NumeroNF"].ToString() != "")
                                oNF.NumeroNF = Convert.ToInt32(dr["NumeroNF"]);
                            if (dr["NumeroNotaFiscal"].ToString() != "")
                                oNF.NumeroNotaFiscal = Convert.ToInt32(dr["NumeroNotaFiscal"]);
                            if (dr["NumeroParcelas"].ToString() != "")
                                oNF.NumeroParcelas = Convert.ToInt32(dr["NumeroParcelas"]);
                            if (dr["NumeroRecibo"].ToString() != "")
                                oNF.NumeroRecibo = Convert.ToInt32(dr["NumeroRecibo"]);
                            if (dr["PercentualCRF"].ToString() != "")
                                oNF.PercentualCRF = Convert.ToDecimal(dr["PercentualCRF"]);
                            if (dr["PercentualINSS"].ToString() != "")
                                oNF.PercentualINSS = Convert.ToDecimal(dr["PercentualINSS"]);
                            if (dr["PercentualIRRF"].ToString() != "")
                                oNF.PercentualIRRF = Convert.ToDecimal(dr["PercentualIRRF"]);
                            if (dr["PercentualISS"].ToString() != "")
                                oNF.PercentualISS = Convert.ToDecimal(dr["PercentualISS"]);
                            if (dr["TipoDocumento"].ToString() != "")
                                oNF.TipoDocumento = Convert.ToInt32(dr["TipoDocumento"]);
                            if (dr["ValorBaseINSS"].ToString() != "")
                                oNF.ValorBaseINSS = Convert.ToDecimal(dr["ValorBaseINSS"]);
                            if (dr["ValorCOFINS"].ToString() != "")
                                oNF.ValorCOFINS = Convert.ToDecimal(dr["ValorCOFINS"]);
                            if (dr["ValorContrSocial"].ToString() != "")
                                oNF.ValorContrSocial = Convert.ToDecimal(dr["ValorContrSocial"]);
                            if (dr["ValorDescontoIncondicionado"].ToString() != "")
                                oNF.ValorDescontoIncondicionado = Convert.ToDecimal(dr["ValorDescontoIncondicionado"]);
                            if (dr["ValorISS"].ToString() != "")
                                oNF.ValorISS = Convert.ToDecimal(dr["ValorISS"]);
                            if (dr["ValorPIS"].ToString() != "")
                                oNF.ValorPIS = Convert.ToDecimal(dr["ValorPIS"]);
                            if (dr["ValorTotal"].ToString() != "")
                                oNF.ValorTotal = Convert.ToDecimal(dr["ValorTotal"]);
                            if (dr["Vencimento"].ToString() != "")
                                oNF.Vencimento = Convert.ToDateTime(dr["Vencimento"]).ToString("yyyy-MM-dd");

                            if (oNFdados.DadoExiste(oNF.NumeroNotaFiscal) == "Alterar")
                                oNFdados.Alterar(oNF, oNF.NumeroNotaFiscal);
                            else
                                oNFdados.Inserir(oNF);
                            i++;
                            lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Notas Fiscais ";
                            lblMensagem.Refresh();
                        }
                    }
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void frmAtualizacaoDados_Load_1(object sender, EventArgs e)
        {
            lblDB.Text = "db: " + geral.BancoUsado.ToString();
            MontaGradeTabelas();
        }

        private void MontaGradeTabelas()
        {
            GradeTabelasDB.AutoGenerateColumns = false;
            clsDB oDB = new clsDB();
            DataTable dtTabelas = oDB.TabelasDB();
            dtTabelas.Columns.Add("Regs");
            if (oDB.NomeDB == "silc")
                GradeTabelasDB.Columns[0].DataPropertyName = "Tables_in_silc";
            if (oDB.NomeDB == "ewvs")
                GradeTabelasDB.Columns[0].DataPropertyName = "Tables_in_ewvs";
            if (oDB.NomeDB == "test")
                GradeTabelasDB.Columns[0].DataPropertyName = "Tables_in_test";
            foreach (DataRow _dr in dtTabelas.Rows)
            {
                if (_dr[0].ToString() == "CDFe" || _dr[0].ToString() == "Enderecos" || _dr[0].ToString() == "LancamentoMTR")
                    _dr.Delete();
                else if (_dr[0].ToString() == "Clientes")
                    _dr["Regs"] = oDB.RegistrosTabela(_dr[0].ToString(), "Codigo").ToString();
            }
            bindingSource2 = new BindingSource();
            bindingSource2.DataSource = dtTabelas;
            GradeTabelasDB.DataSource = bindingSource2;
        }

        private void Grade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GradeTabelasDB_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            string l = "";
        }

        private void GradeTabelasDB_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (GradeTabelasDB.CurrentCell is DataGridViewCheckBoxCell)
            {
                GradeTabelasDB.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }
        private void AtualizacaoProgramacaoDiariaServicos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsProgramacaoDiariaServicos oProgramacaoDiariaServicos = new clsProgramacaoDiariaServicos();
                clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();

                DateTime DataUltm = Convert.ToDateTime(oProgramacaoFechadaDados.DataUltimaProgAberta()).AddDays(-70);
                oProgramacaoDados.ExcluirProgramacaoDia(DataUltm.ToString("yyyy-MM-dd").Replace("-", ""), true);

                string s = "";
                s = s + "select distinct *, r.Codigo as CodigoResiduo, p.Unidade as UnidadeProg \n";
                s = s + "from   ProgramacaoDiariaServicos p \n";
                s = s + "left   join Residuos r on p.ExecutarServico = r.DescricaoReduzida \n";
                s = s + "where  AnoMesDia >= " + DataUltm.ToString("yyyy-MM-dd").Replace("-", "") + " \n";
                s = s + "and    (select Count(*) from ProgramacaoDiariaServicos   \n";
                s = s + "        where sequencial = p.sequencial       \n";
                s = s + "        and   AnoMesDia  = p.AnoMesDia        \n";
                s = s + "        and   CodigoCliente = p.CodigoCliente \n";
                s = s + "        ) = 1 \n";
                s = s + "order by p.AnoMesDia \n ";

                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oProgramacaoDiariaServicos = new clsProgramacaoDiariaServicos();
                        if (dr["Sequencial"].ToString() != "")
                            oProgramacaoDiariaServicos.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["AnoMesDia"].ToString() != "")
                            oProgramacaoDiariaServicos.AnoMesDia = Convert.ToInt32(dr["AnoMesDia"]);
                        if (dr["CodigoCaminhao"].ToString() != "")
                            oProgramacaoDiariaServicos.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oProgramacaoDiariaServicos.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["CodigoMotorista"].ToString() != "")
                            oProgramacaoDiariaServicos.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);

                        if (dr["CodigoResiduo"].ToString() != "")
                            oProgramacaoDiariaServicos.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                        oProgramacaoDiariaServicos.ExecutarServico = dr["ExecutarServico"].ToString();
                        oProgramacaoDiariaServicos.CorObservacao = dr["CorObservacao"].ToString();
                        if (dr["Data"].ToString() != "")
                            oProgramacaoDiariaServicos.Data = Convert.ToDateTime(dr["Data"]).ToShortDateString();
                        if (dr["DataProgramada"].ToString() != "")
                            oProgramacaoDiariaServicos.DataProgramada = Convert.ToDateTime(dr["DataProgramada"]).ToShortDateString();
                        oProgramacaoDiariaServicos.Hora = dr["Hora"].ToString();
                        if (dr["Linha"].ToString() != "")
                            oProgramacaoDiariaServicos.Linha = Convert.ToInt32(dr["Linha"]);
                        if (dr["LinhaGrade2"].ToString() != "")
                            oProgramacaoDiariaServicos.LinhaGrade2 = Convert.ToInt32(dr["LinhaGrade2"]);
                        oProgramacaoDiariaServicos.Observacao = dr["Observacao"].ToString();
                        if (dr["OrdemF4"].ToString() != "")
                            oProgramacaoDiariaServicos.OrdemF4 = Convert.ToInt32(dr["OrdemF4"]);
                        oProgramacaoDiariaServicos.Origem = dr["Origem"].ToString();
                        if (dr["Quadro"].ToString() != "")
                            oProgramacaoDiariaServicos.Quadro = Convert.ToInt32(dr["Quadro"]);
                        if (dr["Quantidade"].ToString() != "")
                            oProgramacaoDiariaServicos.Quantidade = Convert.ToInt32(dr["Quantidade"]);
                        if (dr["Quantidade2"].ToString() != "")
                            oProgramacaoDiariaServicos.Quantidade2 = Convert.ToDecimal(dr["Quantidade2"]);
                        oProgramacaoDiariaServicos.RotaMapa = dr["RotaMapa"].ToString();
                        if (dr["SequencialParaQuadro1"].ToString() != "")
                            oProgramacaoDiariaServicos.SequencialParaQuadro1 = Convert.ToInt32(dr["SequencialParaQuadro1"]);
                        oProgramacaoDiariaServicos.ServicoExecutado = dr["HoraProgramada"].ToString();
                        oProgramacaoDiariaServicos.Solicitante = dr["Solicitante"].ToString();
                        oProgramacaoDiariaServicos.StatusCor = dr["StatusCor"].ToString();
                        if (dr["TipoProgramacao"].ToString() != "")
                            oProgramacaoDiariaServicos.TipoProgramacao = Convert.ToInt16(dr["TipoProgramacao"]);
                        oProgramacaoDiariaServicos.Unidade = dr["UnidadeProg"].ToString();
                        oProgramacaoDiariaServicos.Unidade2 = dr["Unidade2"].ToString();
                        oProgramacaoDiariaServicos.NomeMotorista = dr["NomeMotoristaOuDescricao"].ToString();
                        if (oProgramacaoDiariaServicos.Sequencial == 733105)
                            oProgramacaoDiariaServicos.Sequencial = 733105;
                        if (oProgramacaoDados.DadoExiste(oProgramacaoDiariaServicos.Sequencial, oProgramacaoDiariaServicos.Quadro,
                                                         oProgramacaoDiariaServicos.AnoMesDia, oProgramacaoDiariaServicos.CodigoCliente) == "Alterar")
                        {
                            oProgramacaoDados.Alterar(oProgramacaoDiariaServicos, oProgramacaoDiariaServicos.Sequencial);
                        }
                        else
                        {
                            oProgramacaoDados.Inserir(oProgramacaoDiariaServicos);
                        }

                        i++;
                        lblMensagem.Text = i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString();
                        lblMensagem.Refresh();
                    }
                }

                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoProgramacaoFechada()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                oProgramacaoFechada = new clsProgramacaoFechada();
                oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();

                DateTime DataUltm = Convert.ToDateTime(oProgramacaoFechadaDados.DataUltimaProgAberta()).AddDays(-45);

                string s = "";
                s = s + "select * \n";
                s = s + "from   ProgramacaoFechada \n";
                s = s + "where  Data >= #" + DataUltm.ToString("MM/dd/yyyy") + "# \n";

                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oProgramacaoFechada = new clsProgramacaoFechada();
                        if (dr["Codigo"].ToString() != "")
                            oProgramacaoFechada.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["Data"].ToString() != "")
                            oProgramacaoFechada.Data = Convert.ToDateTime(dr["Data"]).ToString("yyyy-MM-dd");
                        if (dr["Fechada"].ToString() != "")
                            oProgramacaoFechada.Fechada = Convert.ToInt32(dr["Fechada"]);
                        oProgramacaoFechada.BloqueadaNomeUsuario = dr["BloqueadoPeloUsuario"].ToString();

                        if (oProgramacaoFechadaDados.DadoExiste(oProgramacaoFechada.Codigo) == "Alterar")
                        {
                            oProgramacaoFechadaDados.Alterar(oProgramacaoFechada, oProgramacaoFechada.Codigo);
                        }
                        else
                        {
                            oProgramacaoFechadaDados.Inserir(oProgramacaoFechada, oProgramacaoFechada.Codigo);
                        }
                        i++;
                        lblMensagem.Text = i.ToString();
                        lblMensagem.Refresh();
                    }
                }

                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoReprogramacaoServicos()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsReprogramacaoServicos oReprogramacaoServicos = new clsReprogramacaoServicos();
                clsReprogramacaoDados oProgramacaoDados = new clsReprogramacaoDados();

                DateTime DataUltm = Convert.ToDateTime(oProgramacaoFechadaDados.DataUltimaProgAberta());

                string s = "";
                s = s + "select distinct *, r.Codigo as CodigoResiduo, p.Unidade as UnidadeProg \n";
                s = s + "from   ReprogramacaoServicos p \n";
                s = s + "left   join Residuos r on p.ExecutarServico = r.DescricaoReduzida \n";
                //s = s + "where  AnoMesDia = 20190309 \n";
                s = s + "where  (AnoMesDia >= " + DataUltm.AddDays(-70).ToString("yyyy-MM-dd").Replace("-", "") + " \n";
                s = s + "        or ( DataProgramada >= '" + DataUltm.AddDays(-70).ToString("yyyy-MM-dd") + "' \n";
                s = s + "             and DataProgramada <= '" + DataUltm.AddDays(+1).ToString("yyyy-MM-dd") + "')) \n";
                s = s + "and    (select Count(*) from ReprogramacaoServicos   \n";
                s = s + "        where sequencial = p.sequencial       \n";
                s = s + "        and   AnoMesDia  = p.AnoMesDia        \n";
                s = s + "        and   CodigoCliente = p.CodigoCliente \n";
                s = s + "        ) = 1 \n";
                //s = s + "and     CodigoCliente = 1910 \n ";

                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();


                oProgramacaoDados.Excluir(DataUltm.AddDays(-10).ToString("yyyy-MM-dd").Replace("-", ""));

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oReprogramacaoServicos = new clsReprogramacaoServicos();
                        if (dr["Sequencial"].ToString() != "")
                            oReprogramacaoServicos.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["AnoMesDia"].ToString() != "")
                            oReprogramacaoServicos.AnoMesDia = Convert.ToInt32(dr["AnoMesDia"]);
                        if (dr["CodigoCaminhao"].ToString() != "")
                            oReprogramacaoServicos.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oReprogramacaoServicos.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["CodigoMotorista"].ToString() != "")
                            oReprogramacaoServicos.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        if (dr["CodigoResiduo"].ToString() != "")
                            oReprogramacaoServicos.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                        oReprogramacaoServicos.ExecutarServico = dr["ExecutarServico"].ToString();
                        if (dr["Data"].ToString() != "")
                            oReprogramacaoServicos.Data = Convert.ToDateTime(dr["Data"]).ToShortDateString();
                        if (dr["DataProgramada"].ToString() != "")
                            oReprogramacaoServicos.DataProgramada = Convert.ToDateTime(dr["DataProgramada"]).ToShortDateString();
                        oReprogramacaoServicos.Hora = dr["Hora"].ToString();
                        if (dr["Linha"].ToString() != "")
                            oReprogramacaoServicos.Linha = Convert.ToInt32(dr["Linha"]);
                        if (dr["SequencialProgramacaoDiaria"].ToString() != "")
                            oReprogramacaoServicos.SequencialProgramacaoDiaria = Convert.ToInt32(dr["SequencialProgramacaoDiaria"]);
                        oReprogramacaoServicos.Observacao = dr["Observacao"].ToString();
                        if (dr["Quantidade"].ToString() != "")
                            oReprogramacaoServicos.Quantidade = Convert.ToDecimal(dr["Quantidade"]);
                        oReprogramacaoServicos.HoraProgramada = dr["HoraProgramada"].ToString();
                        oReprogramacaoServicos.Solicitante = dr["Solicitante"].ToString();
                        oReprogramacaoServicos.StatusCor = dr["StatusCor"].ToString();
                        oReprogramacaoServicos.Unidade = dr["Unidade"].ToString();

                        if (oProgramacaoDados.DadoExiste(oReprogramacaoServicos.Sequencial) == "Alterar")
                        {
                            oProgramacaoDados.Alterar(oReprogramacaoServicos, oReprogramacaoServicos.Sequencial, false);
                        }
                        else
                        {
                            oProgramacaoDados.Inserir(oReprogramacaoServicos, oReprogramacaoServicos.Sequencial, false);
                        }
                        i++;
                        lblMensagem.Text = i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString();
                        lblMensagem.Refresh();
                    }
                }

                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoSenhaIPM()
        {
            try
            {
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsSenhaIPM oSenhaIPM = new clsSenhaIPM();
                clsSenhaIPMDados oSenhaIPMDados = new clsSenhaIPMDados();
                string s = "";
                s = s + "select Sequencial, Descrypt(Senha) \n";
                s = s + "from   SenhaIPM \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();

                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oSenhaIPM = new clsSenhaIPM();
                        if (dr["Sequencial"].ToString() != "")
                            oSenhaIPM.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        oSenhaIPM.Senha = dr["Senha"].ToString();
                        if (oSenhaIPMDados.DadoExiste(oSenhaIPM.Sequencial) == "Alterar")
                            oSenhaIPMDados.Alterar(oSenhaIPM, oSenhaIPM.Sequencial);
                        else
                            oSenhaIPMDados.Inserir(oSenhaIPM, oSenhaIPM.Sequencial);
                        i++;
                        lblMensagem.Text = i.ToString();
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoFuncionarios()
        {
            try
            {
                clsFuncionarios oFuncionarios = new clsFuncionarios();
                clsFuncionarioDados oFuncionariosDados = new clsFuncionarioDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from Funcionarios order by Codigo \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oFuncionarios = new clsFuncionarios();
                        if (dr["Codigo"].ToString() != "")
                            oFuncionarios.Codigo = Convert.ToInt32(dr["Codigo"]);
                        oFuncionarios.Bairro = dr["deBairr"].ToString();
                        oFuncionarios.CEP = dr["nuCEP"].ToString();
                        oFuncionarios.Cidade = dr["deCidd"].ToString();
                        oFuncionarios.CPF = dr["nuCPF"].ToString();
                        if (dr["dtAdms"].ToString() != "")
                            oFuncionarios.DataAdmissao = Convert.ToDateTime(dr["dtAdms"]).ToString("yyyy-MM-dd");
                        if (dr["dtDems"].ToString() != "")
                            oFuncionarios.DataDemissao = Convert.ToDateTime(dr["dtDems"]).ToString("yyyy-MM-dd");
                        oFuncionarios.Endereco = dr["deEndr"].ToString();
                        oFuncionarios.Fone = dr["nuTelf"].ToString();
                        oFuncionarios.Nome = dr["nmFunc"].ToString();
                        oFuncionarios.NumeroCTPS = dr["nuCTPS"].ToString();
                        oFuncionarios.RG = dr["nuRG"].ToString();
                        oFuncionarios.Serie = dr["nuSere"].ToString();
                        oFuncionarios.UF = dr["deUF"].ToString();

                        if (oFuncionariosDados.DadoExiste(oFuncionarios.Codigo) == "Alterar")
                            oFuncionariosDados.Alterar(oFuncionarios, oFuncionarios.Codigo);
                        else
                            oFuncionariosDados.Inserir(oFuncionarios, oFuncionarios.Codigo);
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Motoristas";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoServicosFutura()
        {
            try
            {
                clsServicosFutura oServicosFutura = new clsServicosFutura();
                clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from ServicosFutura order by Sequencial \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                    {
                        oServicosFutura = new clsServicosFutura();
                        oServicosFutura.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                        if (dr["CodigoCaminhao"].ToString() != "")
                            oServicosFutura.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oServicosFutura.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["CodigoMotorista"].ToString() != "")
                            oServicosFutura.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        oServicosFutura.DataProgramada = dr["DataProgramada"].ToString();
                        oServicosFutura.MapaMarcado = 0;
                        if (dr["MapaMarcado"].ToString() != "")
                            oServicosFutura.MapaMarcado = 1;
                        oServicosFutura.Observacao = dr["Obs"].ToString();
                        oServicosFutura.ServicoAExecutar = dr["ServicoAExecutar"].ToString();
                        oServicosFutura.DescricaoResiduo = dr["DescricaoResiduo"].ToString();
                        oServicosFutura.Hora = dr["Hora"].ToString();
                        if (oServicosFuturaDados.DadoExiste(oServicosFutura.CodigoCliente,
                                                            oServicosFutura.CodigoResiduo,
                                                            oServicosFutura.DataProgramada,
                                                            oServicosFutura.DescricaoResiduo, dr["Hora"].ToString()) == "Incluir")
                        {
                            oServicosFuturaDados.Inserir(oServicosFutura);
                        }
                        else
                        {
                            bool bFiltrarCampoHora = false;
                            if (dr["StatusCor"].ToString() == "15000000")
                                bFiltrarCampoHora = true;
                            oServicosFuturaDados.Alterar(oServicosFutura, oServicosFutura.CodigoCliente, oServicosFutura.DataProgramada, dr["Hora"].ToString(), bFiltrarCampoHora);
                        }

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString() + " Servicos programação futura";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void AtualizacaoMovimentacaoDTR()
        {
            try
            {
                clsMovimentacaoDTR oMoviDTR = new clsMovimentacaoDTR();
                clsMovimentacaoDTRDados oMoviDtrDados = new clsMovimentacaoDTRDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select * from MovimentacaoDTR \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oMoviDTR = new clsMovimentacaoDTR();
                        oMoviDTR.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["CodigoCliente"].ToString() != "")
                            oMoviDTR.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                        if (dr["CodigoResiduo"].ToString() != "")
                            oMoviDTR.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                        if (dr["Data"].ToString() != "")
                            oMoviDTR.Data = Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yyyy");
                        oMoviDTR.MoviCxDe = dr["MoviCxDe"].ToString();
                        oMoviDTR.MoviCxPara = dr["MoviCxPara"].ToString();
                        if (dr["NumeroLancamento"].ToString() != "")
                            oMoviDTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);

                        if (oMoviDtrDados.DadoExiste(oMoviDTR.Codigo) == "Incluir")
                        {
                            oMoviDtrDados.Inserir(oMoviDTR);
                        }
                        else
                        {
                            oMoviDtrDados.Alterar(oMoviDTR, oMoviDTR.Codigo);
                        }

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString() + " Movimentação DTR";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            btnCompararQtReajustes2DBs();
            btnCompararResiduosQtsUltimoReajuste();
        }

        private void btnCompararQtReajustes2DBs()
        {
            try
            {
                i = 0;
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsContratos oContratos = new clsContratos();
                clsContratosDados oContratosDados = new clsContratosDados();
                clsContratosReajustesDados oReajustesDados = new clsContratosReajustesDados();
                string s = "";
                s = s + "select * from Precos \n";
                if (cliente1.txtCodigo.Text != "")
                    s = s + "where cdclnt = " + cliente1.txtCodigo.Text + " \n";
                s = s + "order by cdClnt \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                DataTable _dtContratoSILC = new DataTable();
                oDataAdapter.Fill(_dtContratoSILC);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtContratoSILC.Rows)
                {
                    if (dr["cdClnt"].ToString() != "" && dr["cdClnt"].ToString() != "0" &&
                        dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oContratos = new clsContratos();
                        if (dr["Codigo"].ToString() != "")
                            oContratos.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["cdClnt"].ToString() != "")
                            oContratos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);

                        oCliente = oClienteDados.PegaDados(oCliente, oContratos.CodigoCliente);
                        s = "";
                        s = s + "select Count(*) as QtReajustes from Reajustes \n";
                        s = s + "where  CodigoPreco = " + oContratos.Codigo + " \n";
                        OleDbDataAdapter oAdapterReajustesSILC = new OleDbDataAdapter(s, aConnection);
                        DataTable _dtReajustesSILC = new DataTable();
                        oAdapterReajustesSILC.Fill(_dtReajustesSILC);
                        oAdapterReajustesSILC.Dispose();
                        string rr = oReajustesDados.RegistrosDeReajuste(oContratos.Codigo);
                        if (_dtReajustesSILC.Rows[0][0].ToString() != rr && oCliente.Inativo == 0)
                        {
                            MessageBox.Show("Reajustes diferentes Contrato: " + oContratos.Codigo.ToString() + " Cliente: " + oCliente.NomeFantasia + "(" + oCliente.Codigo + ")  no SILC " + _dtReajustesSILC.Rows[0][0].ToString() + " / webSILC " + rr);
                            break;
                        }
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Contratos ";
                        lblMensagem.Refresh();
                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

                lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Ok ";

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        private void btnCompararResiduosQtsUltimoReajuste()
        {
            try
            {
                i = 0;
                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();
                clsContratos oContratos = new clsContratos();
                clsContratosDados oContratosDados = new clsContratosDados();
                clsContratosReajustesDados oReajustesDados = new clsContratosReajustesDados();
                string s = "";
                s = s + "select * from Precos where (DataRecisao is null or DataRecisao = #01/01/0100# or DataRecisao = #01/01/0001# or DataRecisao = #01/01/1900#) \n";
                s = s + "order by cdClnt \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                DataTable _dtContratoSILC = new DataTable();
                oDataAdapter.Fill(_dtContratoSILC);
                oDataAdapter.Dispose();
                foreach (DataRow dr in _dtContratoSILC.Rows)
                {
                    if (dr["cdClnt"].ToString() != "" && dr["cdClnt"].ToString() != "0" &&
                        dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                    {
                        oContratos = new clsContratos();
                        if (dr["Codigo"].ToString() != "")
                            oContratos.Codigo = Convert.ToInt32(dr["Codigo"]);
                        if (dr["cdClnt"].ToString() != "")
                            oContratos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);

                        s = "";
                        s = s + "select top 1 Data, Valor from Reajustes \n";
                        s = s + "where  CodigoPreco = " + oContratos.Codigo + " \n";
                        s = s + "order  by Data desc \n";
                        OleDbDataAdapter oAdapterReajustesSILC = new OleDbDataAdapter(s, aConnection);
                        DataTable _dtReajustesSILC = new DataTable();
                        oAdapterReajustesSILC.Fill(_dtReajustesSILC);
                        oAdapterReajustesSILC.Dispose();

                        oCliente = oClienteDados.PegaDados(oCliente, oContratos.CodigoCliente);
                        
                        if (oCliente.Inativo == 0)
                        {
                            if (_dtReajustesSILC.Rows.Count > 0)
                            {
                                if (_dtReajustesSILC.Rows[0][0].ToString() != "" && _dtReajustesSILC.Rows[0][0].ToString() != "01/01/0100" && 
                                    _dtReajustesSILC.Rows[0][0].ToString() != "01/01/0001" && _dtReajustesSILC.Rows[0][0].ToString() != "01/01/1900")
                                {
                                    clsContratosReajustes oReajuste = new clsContratosReajustes();
                                    oReajustesDados.PegaDados(oReajuste, oContratos.Codigo, _dtReajustesSILC.Rows[0][0].ToString(), 0);

                                    if (oReajuste.Valor.ToString("N2") != Convert.ToDecimal(_dtReajustesSILC.Rows[0]["Valor"]).ToString("N2"))
                                    {
                                        MessageBox.Show("Contrato: " + oContratos.Codigo + " \n Valores Diferentes! Cliente: " + oCliente.NomeFantasia + "(" + oCliente.Codigo + ")  \n no SILC " + _dtReajustesSILC.Rows[0][1].ToString() + " != webSILC " + oReajuste.Valor.ToString());
                                        break;

                                    }
                                    s = "";
                                    s = s + "select * from ContratoResiduos \n";
                                    s = s + "where  CodigoContrato =  " + oContratos.Codigo + " \n";
                                    s = s + "and    CodigoCliente  =  " + oContratos.CodigoCliente + " \n";
                                    s = s + "and    DataReajuste   = #" + Convert.ToDateTime(_dtReajustesSILC.Rows[0][0]).ToString("MM/dd/yyyy") + "# \n";
                                    s = s + "order  by CodigoResiduo desc \n";
                                    OleDbDataAdapter oAdapterResiduosSILC = new OleDbDataAdapter(s, aConnection);
                                    DataTable _dtResiduosSILC = new DataTable();
                                    oAdapterResiduosSILC.Fill(_dtResiduosSILC);
                                    oAdapterResiduosSILC.Dispose();
                                    clsContratoResiduosDados oContratoResiduoDados = new clsContratoResiduosDados();
                                    oContratoResiduoDados = new clsContratoResiduosDados();
                                    DataTable dt2WebSILC = oContratoResiduoDados.PreencheDataTableContratoResiduos("CodigoResiduo", oContratos.Codigo, Convert.ToDateTime(_dtReajustesSILC.Rows[0][0]).ToString("yyyy-MM-dd"), oContratos.CodigoCliente);

                                    if (_dtResiduosSILC.Rows.Count == dt2WebSILC.Rows.Count)
                                    {
                                        foreach (DataRow drDB1 in _dtResiduosSILC.Rows)
                                        {
                                            DataRow[] dr2WebSILC = dt2WebSILC.Select("CodigoResiduo = " + drDB1["CodigoResiduo"] + "and CodigoContrato = " + oContratos.Codigo + " and DataReajuste = '" + Convert.ToDateTime(_dtReajustesSILC.Rows[0][0]).ToString("yyyy-MM-dd") + "'");
                                            if (dr2WebSILC.Length == 0) // não achou no webSILC
                                                MessageBox.Show("Contrato: " + oContratos.Codigo + "\n Não encontrou Resíduo Contratado: " + drDB1["CodigoResiduo"].ToString() + " \n Cliente: " + oCliente.NomeFantasia + "(" + oCliente.Codigo + ")  no SILC " + _dtReajustesSILC.Rows[0][0].ToString() + " / webSILC ");
                                            else
                                            {
                                                if (dr2WebSILC[0]["Franquia"] != drDB1["Franquia"])
                                                {
                                                    MessageBox.Show(" Contrato: " + oContratos.Codigo + "\n Franquia SILC != " + drDB1["Franquia"].ToString() + " \n webSILC " + dr2WebSILC[0]["Franquia"].ToString());
                                                }
                                                if (Convert.ToDecimal(dr2WebSILC[0]["QuantidadeFranquia"]).ToString("N2") != Convert.ToDecimal(drDB1["QuantidadeFranquia"]).ToString("N2"))
                                                {
                                                    MessageBox.Show(" Contrato: " + oContratos.Codigo + "\n QuantidadeFranquia SILC != " + drDB1["QuantidadeFranquia"].ToString() + " \n webSILC " + dr2WebSILC[0]["QuantidadeFranquia"].ToString());
                                                }
                                                if ((Convert.ToDecimal(dr2WebSILC[0]["ValorUnitario"]) / 100).ToString("N4") != (Convert.ToDecimal(drDB1["ValorUnitario"])).ToString("N4"))
                                                {
                                                    MessageBox.Show(" Contrato: " + oContratos.Codigo + "\n ValorUnitario SILC != " + 
                                                                    (Convert.ToDecimal(dr2WebSILC[0]["ValorUnitario"]) / 100).ToString("N4") + 
                                                                    " \n webSILC " + (Convert.ToDecimal(drDB1["ValorUnitario"])).ToString("N4"));
                                                }
                                                if (dr2WebSILC[0]["DiasColeta"].ToString().Trim() != drDB1["DiasColeta"].ToString().Trim())
                                                {
                                                    MessageBox.Show(" Contrato: " + oContratos.Codigo + "\n DiasColeta SILC != " + drDB1["DiasColeta"].ToString() + " \n webSILC " + dr2WebSILC[0]["DiasColeta"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Contrato: " + oContratos.Codigo + " \n Quantidade de resíduos Diferentes! Cliente: " + oCliente.NomeFantasia + "(" + oCliente.Codigo + ")  \n no SILC " + _dtResiduosSILC.Rows.Count.ToString() + " != webSILC " + dt2WebSILC.Rows.Count.ToString());
                                        break;
                                    }
                                }
                            }
                        }
                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Resíduos contratados ";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

                lblMensagem.Text = "Atualizado/lido: " + i.ToString() + " Ok ";

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        private void btnAtualizaParticularidade_Click(object sender, EventArgs e)
        {
            try
            {
                clsContratoResiduosDados oContResDados = new clsContratoResiduosDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select distinct CodigoContrato, CodigoCliente, CodigoResiduo, DataReajuste, Particularidade ";
                s = s + "from   contratoresiduos where particularidade > '' and particularidade <> '.'  order by codigocliente \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                i = 0;
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                    {
                        oContResDados.AlterarParticularidade(Convert.ToInt32(dr["CodigoContrato"]), Convert.ToInt32(dr["CodigoCliente"]), Convert.ToInt32(dr["CodigoResiduo"]), dr["DataReajuste"].ToString(), dr["Particularidade"].ToString());

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString() + " ";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }

        }

        private void butAtualizarAliquotasClientes_Click(object sender, EventArgs e)
        {
            try
            {
                clsClienteDados oClienteDados = new clsClienteDados();

                //cria a conexão com o banco de dados
                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                aConnection.Open();

                string s = "";
                s = s + "select Codigo as CodigoCliente, CodigoBROOKS_Retencoes ";
                s = s + "from   Clientes \n";
                OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                _dtAtualizacao = new DataTable();
                oDataAdapter.Fill(_dtAtualizacao);
                oDataAdapter.Dispose();
                i = 0;
                foreach (DataRow dr in _dtAtualizacao.Rows)
                {
                    if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                    {
                        oClienteDados.AlterarParametroAliquotaFederal(dr["CodigoBROOKS_Retencoes"].ToString(), dr["CodigoCliente"].ToString());

                        i++;
                        lblMensagem.Text = "Atualizado/lido: " + i.ToString() + "/" + _dtAtualizacao.Rows.Count.ToString() + " ";
                        lblMensagem.Refresh();

                    }
                }
                //fecha a conexao 
                aConnection.Close();
                aConnection.Dispose();

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }

        }
    }
}
