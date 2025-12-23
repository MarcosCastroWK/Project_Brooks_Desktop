using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;


namespace SILC.Web.forms
{
    public partial class webClientesHistorico : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsClientes oClientes = new clsClientes();
        clsClienteDados oClientesDados = new clsClienteDados();
        clsEnderecos oEndereco = new clsEnderecos();
        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        clsFuncionarios oFuncionario = new clsFuncionarios();
        clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();
        clsAliquotaImpostosDados oAliqDados = new clsAliquotaImpostosDados();
        DataTable _dt = new DataTable();
        clsGeral oGeralDados = new clsGeral();
        clsMunicipios oMunicipio = new clsMunicipios();
        clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
        clsClientesHistorico oClienteHistorico = new clsClientesHistorico();
        clsClienteHistoricoDados oClienteHistoricoDados = new clsClienteHistoricoDados();
        System.Drawing.Color _BackColorLadoEsquerdo = new System.Drawing.Color();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["oUsuario"] == null)
            {
                menu _menu = (menu)FindControl("menucabec1");
                oUsuario.Codigo = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigo")).Value);
                oUsuario.CodigoEmpresa = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigoEmpresa")).Value);
                oUsuario.Nome = ((HiddenField)_menu.FindControl("hifNome")).Value;
                oUsuario.Aplicativo = false;
                Session["oUsuario"] = oUsuario;
            }
            else if (Session["oUsuario"] != null)
            {
                oUsuario = (clsUsuarios)Session["oUsuario"];
            }
            if (Request.QueryString["CodigoCliente"] != "" && Request.QueryString["CodigoCliente"] != null)
            {
                hifCodigo.Value = Request.QueryString["CodigoCliente"];
                lblCodigoCliente.Text = hifCodigo.Value.ToString();
            }
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "2");

            if (!IsPostBack)
            {
                ddlAliquotasFederais.Items.Add(" ");
                foreach (DataRow drAliq in oAliqDados.PreencheDataTable("CodigoBROOKS").Rows)
                {
                    ddlAliquotasFederais.Items.Add(drAliq["CodigoBROOKS"].ToString() + "-" + drAliq["Descricao"].ToString());
                }
                ddlAliquotasFederais0.Items.Add(" ");
                foreach (DataRow drAliq in oAliqDados.PreencheDataTable("CodigoBROOKS").Rows)
                {
                    ddlAliquotasFederais0.Items.Add(drAliq["CodigoBROOKS"].ToString() + "-" + drAliq["Descricao"].ToString());
                }
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                else
                {
                    if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                        Response.Redirect("sempermissao.aspx");
                    try
                    {
                        if (oUsuario.Aplicativo == true)
                            menucabec1.Visible = false;
                        else
                            menucabec1.Visible = true;

                        MostraDadosPrimeiraLinhaGradeLadoEsquerdo();
                        //MostraHistoricoDeAlteracao(); 

                        PermissaoIncluir();
                        PermissaoAlterar();
                        HabilitaDesabilitaCampos(false);
                        HabilitaDesabilitaEnderecoPadrao(false);
                        HabilitaDesabilitaEnderecoFaturamento(false);
                        HabilitaDesabilitaEnderecoColeta(false);
                        HabilitaDesabilitaInformacoes(false);
                        HabilitaDesabilitaSistemaIMA(false);

                        HabilitaDesabilitaCamposQuadro2(false);
                        HabilitaDesabilitaEnderecoPadraoQuadro2(false);
                        HabilitaDesabilitaEnderecoFaturamentoQuadro2(false);
                        HabilitaDesabilitaEnderecoColetaQuadro2(false);
                        HabilitaDesabilitaInformacoesQuadro2(false);
                        HabilitaDesabilitaSistemaIMAQuadro2(false);

                        try
                        {
                            AtribuiDaClasseClienteHistoricoAbaDadosLadoDireito();
                            AtribuiDaClasseClienteHistoricoAbaEnderecoPadrao();
                            AtribuiDaClasseClienteHistoricoAbaEnderecoFaturamento();
                            AtribuiDaClasseClienteHistoricoAbaEnderecoColeta();

                            // especifica a cor de cada campo do lado esquerda que está diferente do campo do lado direito.
                            _BackColorLadoEsquerdo = System.Drawing.Color.LightGreen;
                        }
                        finally
                        {
                            MostraDiferencasAbaDados();
                            MostraDiferencasAbaEnderecoPadrao();
                            MostraDiferencasAbaEnderecoFaturamento();
                            MostraDiferencasAbaEnderecoColeta();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
        }
        private void HabilitaDesabilitaCampos(bool bHabilitar)
        {
            datDataCadastro.Enabled = bHabilitar;
            txtNome.Enabled = bHabilitar;
            txtNomeFantasia.Enabled = bHabilitar;
            imbInativo.Enabled = bHabilitar;
            ddlTipoCadastro.Enabled = bHabilitar;
            intCodigoAterro.Enabled = bHabilitar;
            intCodigoFuncionarioComercial.Enabled = bHabilitar;

            datDataNovaSenha.Enabled = bHabilitar;
            txtCNPJ_CPF.Enabled = bHabilitar;
            txtAlfaCNPJ.Enabled = bHabilitar;
            txtAlfaCNPJ_CPF.Enabled = bHabilitar;
            txtRG_IE.Enabled = bHabilitar;
            txtCNPJ_CPF_Faturamento.Enabled = bHabilitar;
            txtNovaSenha.Enabled = bHabilitar;
            txtSolicitadaPor.Enabled = bHabilitar;
            intClassificacao.Enabled = bHabilitar;
            intFilial.Enabled = bHabilitar;
            intContaGerencial.Enabled = bHabilitar;
            ddlAliquotasFederais.Enabled = bHabilitar;
            intCodigoExpNF.Enabled = bHabilitar;
        }
        private void HabilitaDesabilitaCamposQuadro2(bool bHabilitar)
        {
            datDataCadastro0.Enabled = bHabilitar;
            txtNome0.Enabled = bHabilitar;
            txtNomeFantasia0.Enabled = bHabilitar;
            imbInativo0.Enabled = bHabilitar;
            ddlTipoCadastro0.Enabled = bHabilitar;
            intCodigoAterro0.Enabled = bHabilitar;
            intCodigoFuncionarioComercial0.Enabled = bHabilitar;

            datDataNovaSenha0.Enabled = bHabilitar;
            txtCNPJ_CPF0.Enabled = bHabilitar;
            txtAlfaCNPJ0.Enabled = bHabilitar;
            txtAlfaCNPJ_CPF0.Enabled = bHabilitar;
            txtRG_IE0.Enabled = bHabilitar;
            txtCNPJ_CPF_Faturamento0.Enabled = bHabilitar;
            txtNovaSenha0.Enabled = bHabilitar;
            txtSolicitadaPor0.Enabled = bHabilitar;
            intClassificacao0.Enabled = bHabilitar;
            intFilial0.Enabled = bHabilitar;
            intContaGerencial0.Enabled = bHabilitar;
            ddlAliquotasFederais.Enabled = bHabilitar;
            intCodigoExpNF0.Enabled = bHabilitar;
            chkEmiteDDR0.Enabled = bHabilitar;
            chkEmitirCDF0.Enabled = bHabilitar;
            ddlAliquotasFederais0.Enabled = bHabilitar;
        }
        private void MensagemZeroMaxCampo(ControlCollection pView)
        {
            foreach (Control controle in pView)
            {
                if (controle.GetType() == typeof(TextBox))
                {
                    if (((TextBox)controle).MaxLength == 0)
                    {
                        lblMensagem.Text = lblMensagem.Text + "*MaxLength zerada: " + ((TextBox)controle).ID;
                    }
                }
            }
        }
        private void PermissaoIncluir()
        {
            if (oItensMenuPermissoes.Incluir == 0)
            {
            }
        }
        private void PermissaoAlterar()
        {
        }
        protected void AtribuiDadosDaClasse(clsClientes pCliente)
        {
            if (pCliente.Codigo > 0)
                lblCodigoCliente.Text = pCliente.Codigo.ToString("");
            if (pCliente.DataCadastro == "01/01/0001" || pCliente.DataCadastro == "01/01/0100" || pCliente.DataCadastro == null || pCliente.DataCadastro == "")
                datDataCadastro.Data = "";
            else
                datDataCadastro.Data = Convert.ToDateTime(pCliente.DataCadastro).ToString("dd/MM/yyyy");
            if (imbInativo != null)
            {
                if (pCliente.Inativo == 1)
                    imbInativo.ImageUrl = "~/Images/selecionado.png";
                else
                    imbInativo.ImageUrl = "~/Images/selecionar.png";
            }
            if (pCliente.NaoAceitaDiferencaPeso == 1)
                chkNaoAceitaDTRComPesoDiferente.Checked = true;
            else
                chkNaoAceitaDTRComPesoDiferente.Checked = false;
            ddlTipoCadastro.SelectedIndex = pCliente.TiposDeContrato;
            intCodigoAterro.Valor = "";
            if (pCliente.CodigoNoAterro > 0)
                intCodigoAterro.Valor = pCliente.CodigoNoAterro.ToString();
            intCodigoFuncionarioComercial.Valor = "";
            if (pCliente.CodigoFuncionarioComercial > 0)
                intCodigoFuncionarioComercial.Valor = pCliente.CodigoFuncionarioComercial.ToString();
            txtNome.Text = pCliente.Nome;
            txtNomeFantasia.Text = pCliente.NomeFantasia;
            if (pCliente.CNPJ_CPF.Length < 18)
                txtCNPJ_CPF.Text = geral.Left(pCliente.CNPJ_CPF, 14);
            else
                txtCNPJ_CPF.Text = geral.Left(pCliente.CNPJ_CPF, 18);
            txtAlfaCNPJ_CPF.Text = "";
            if (!geral.IsNumeric(geral.Right(pCliente.CNPJ_CPF, 1)))
                txtAlfaCNPJ_CPF.Text = geral.Right(pCliente.CNPJ_CPF, 1);
            txtRG_IE.Text = pCliente.RG_IE;
            if (pCliente.CNPJ_Faturamento.Length < 18)
                txtCNPJ_CPF_Faturamento.Text = geral.Left(pCliente.CNPJ_Faturamento, 14);
            else
                txtCNPJ_CPF_Faturamento.Text = geral.Left(pCliente.CNPJ_Faturamento, 18);
            txtAlfaCNPJ.Text = "";
            if (!geral.IsNumeric(geral.Right(pCliente.CNPJ_Faturamento, 1)))
                txtAlfaCNPJ.Text = geral.Right(pCliente.CNPJ_Faturamento, 1);
            intClassificacao.Valor = pCliente.Classificacao.ToString();
            intFilial.Valor = pCliente.Filial.ToString();
            intContaGerencial.Valor = pCliente.ContaGerencial.ToString();
            intCodigoExpNF.Valor = pCliente.CodigoClienteExportacao.ToString();
            if (pCliente.EnviarDDR == 1)
                chkEmiteDDR.Checked = true;
            else
                chkEmiteDDR.Checked = false;
            if (pCliente.EnviarCDF == 1)
                chkEmitirCDF.Checked = true;
            else
                chkEmitirCDF.Checked = false;
            txtNovaSenha.Text = pCliente.NovaSenha;
            if (pCliente.DataSenha == "01/01/0001" || pCliente.DataSenha == "01/01/0100" || pCliente.DataSenha == null)
                datDataNovaSenha.Data = "";
            else
                datDataNovaSenha.Data = Convert.ToDateTime(pCliente.DataSenha).ToString("dd/MM/yyyy");
            txtSolicitadaPor.Text = pCliente.SolicitadoSenhaPor;

            intKmMedia.Valor = pCliente.KmMedia.ToString();
            txtPontoDeReferencia.Text = pCliente.PontoReferencia;
            txtObservacao.Text = pCliente.OBS;

            txtSenhaMaster.Text = pCliente.SenhaMasterFatima;
            txtSenhaAcesso.Text = pCliente.SenhaAcessoFatima;
            txtObsMTRFatima.Text = geral.Left(pCliente.ObsFatima, 20);
            txtContatoFatma.Text = pCliente.ContatoFatima;
            txtFoneFatma.Text = pCliente.TelefoneFatima;
            txtemailFatma.Text = pCliente.emailFatma;
            intCodigoUnidadeIMA.Valor = pCliente.CodigoUnidadeDoIMA.ToString();
            chkClienteExigeMTReParaRCD.Checked = false;
            if (pCliente.ClienteEmissaoMTReRCD == 1)
                chkClienteExigeMTReParaRCD.Checked = true;
            if (pCliente.CodigoTipoCobranca > 0)
                ddlTipoCobranca.SelectedIndex = pCliente.CodigoTipoCobranca - 1;
            else
                ddlTipoCobranca.SelectedIndex = 0;
            intCodigoFuncionarioComercial.Valor = "";
            lblNomeFuncionario.Text = "";
            if (pCliente.CodigoFuncionarioComercial > 0)
            {
                intCodigoFuncionarioComercial.Valor = pCliente.CodigoFuncionarioComercial.ToString();
                oFuncionario = oFuncionarioDados.PegaDados(oFuncionario, pCliente.CodigoFuncionarioComercial);
                lblNomeFuncionario.Text = oFuncionario.Nome;
            }
            if (pCliente.CodigoBROOKS_Retencoes != "")
            {
                foreach (ListItem lista in ddlAliquotasFederais.Items)
                {
                    if (geral.Left(lista.Value, 1) == pCliente.CodigoBROOKS_Retencoes)
                    {
                        ddlAliquotasFederais.Text = lista.Value;
                    }
                }
            }
            else
            {
                ddlAliquotasFederais.SelectedIndex = 0;
            }
            MostraEnderecos();
        }
        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");

            //chkAtivo.Checked = false;
            imbInativo.ImageUrl = "~/Images/selecionar.png";

            chkNaoAceitaDTRComPesoDiferente.Checked = true;
            ddlTipoCadastro.SelectedIndex = 0;
            intCodigoAterro.Valor = "";
            txtNome.Text = "";
            txtNomeFantasia.Text = "";
            txtCNPJ_CPF.Text = "";
            txtAlfaCNPJ_CPF.Text = "";
            txtRG_IE.Text = "";
            txtCNPJ_CPF_Faturamento.Text = "";
            txtAlfaCNPJ.Text = "";
            intClassificacao.Valor = "1488";
            intFilial.Valor = "1";
            intContaGerencial.Valor = "365";
            intCodigoExpNF.Valor = "";
            chkEmiteDDR.Checked = true;
            chkEmitirCDF.Checked = true;
            txtNovaSenha.Text = "";
            datDataNovaSenha.Data = "";
            txtSolicitadaPor.Text = "";

            intKmMedia.Valor = "";
            txtPontoDeReferencia.Text = "";
            txtObservacao.Text = "";

            txtSenhaMaster.Text = "";
            txtSenhaAcesso.Text = "";
            txtObsMTRFatima.Text = "";
            txtContatoFatma.Text = "";
            txtFoneFatma.Text = "";
            txtemailFatma.Text = "";
            intCodigoUnidadeIMA.Valor = "";
            chkClienteExigeMTReParaRCD.Checked = false;
            ddlTipoCobranca.SelectedIndex = 0;
            lblNomeFuncionario.Text = "";
            ddlAliquotasFederais.SelectedIndex = 0;

            LimpaCamposEnderecos();

            PermissaoIncluir();
        }
        private void LimpaCamposEnderecos()
        {
            // Endereço padrão
            txtEndereco0.Text = "";
            intNumero0.Valor = "";
            txtComplemento0.Text = "";
            intDDD10.Valor = "";
            txtFone10.Text = "";
            txtBairro0.Text = "";
            intDDD20.Valor = "";
            txtFone20.Text = "";
            txtCEP0.Text = "";
            intDDDC0.Valor = "";
            txtCelular0.Text = "";
            intCodigoClidade0.Valor = "";
            intDDDF0.Valor = "";
            txtFax0.Text = "";
            txtemail0.Text = "";
            txtContato0.Text = "";
            txtUF0.Text = "";
            txtCodigoIBGE0.Text = "";
            lbtNomeCidade0.Text = "";

            // Endereço Faturamento = 1
            txtEndereco1.Text = "";
            intNumero1.Valor = "";
            txtComplemento1.Text = "";
            intDDD11.Valor = "";
            txtFone11.Text = "";
            txtBairro1.Text = "";
            intDDD21.Valor = "";
            txtFone21.Text = "";
            txtCEP1.Text = "";
            intDDDC1.Valor = "";
            txtCelular1.Text = "";
            intCodigoClidade1.Valor = "";
            intDDDF1.Valor = "";
            txtFax1.Text = "";
            txtemail1.Text = "";
            txtInstrucoesFaturamento.Text = "";
            txtContato1.Text = "";
            txtUF1.Text = "";
            txtCodigoIBGE1.Text = "";
            lbtNomeCidade1.Text = "";

            // Endereço Coleta = 2
            txtEndereco2.Text = "";
            intNumero2.Valor = "";
            txtComplemento2.Text = "";
            intDDD12.Valor = "";
            txtFone12.Text = "";
            txtBairro2.Text = "";
            intDDD22.Valor = "";
            txtFone22.Text = "";
            txtCEP2.Text = "";
            intDDDC2.Valor = "";
            txtCelular2.Text = "";
            intCodigoClidade2.Valor = "";
            intDDDF2.Valor = "";
            txtFax2.Text = "";
            txtemail2.Text = "";
            txtContato2.Text = "";
            txtCargoContato.Text = "";
            txtUF2.Text = "";
            txtCodigoIBGE2.Text = "";
            lbtNomeCidade2.Text = "";
        }

        private void MostraDadosPrimeiraLinhaGradeLadoEsquerdo()
        {
            // quando não têm historico pegar os dados atuais do cadastro do cliente
            if (Request.QueryString["CodigoHistoricoMaisNovo"] == "")
            {
                oClientes = oClientesDados.PegaDados(oClientes, Convert.ToInt32(hifCodigo.Value));
                lblCodigoCliente.Text = hifCodigo.Value;
                AtribuiDadosDaClasse(oClientes);
            }
            else if (Request.QueryString["CodigoHistoricoMaisNovo"] != "")
            {
                oClienteHistorico = new clsClientesHistorico();
                oClienteHistoricoDados.PegaDados(oClienteHistorico, Convert.ToInt32(Request.QueryString["CodigoHistoricoMaisNovo"]));
                oClientes = new clsClientes();
                oClientes.CEP = oClienteHistorico.CEP;
                oClientes.Classificacao = oClientes.Classificacao;
                oClientes.ClienteEmissaoMTReRCD = oClienteHistorico.ClienteEmissaoMTReRCD;
                oClientes.CNPJ_CPF = oClienteHistorico.CNPJ_CPF;
                oClientes.CNPJ_Faturamento = oClienteHistorico.CNPJ_Faturamento;
                oClientes.CodigoBROOKS_Retencoes = oClienteHistorico.CodigoBROOKS_Retencoes;
                oClientes.CodigoClienteExportacao = oClienteHistorico.CodigoClienteExportacao;
                oClientes.CodigoFuncionarioComercial = oClienteHistorico.CodigoFuncionarioComercial;
                oClientes.CodigoMunicipioNF = oClienteHistorico.CodigoMunicipioNF;
                oClientes.CodigoMunicipioObra = oClienteHistorico.CodigoMunicipioObra;
                oClientes.CodigoNoAterro = oClienteHistorico.CodigoNoAterro;
                oClientes.CodigoSituacaoTributaria = oClienteHistorico.CodigoSituacaoTributaria;
                oClientes.CodigoTipoCobranca = oClienteHistorico.CodigoTipoCobranca;
                oClientes.CodigoUnidadeDoIMA = oClienteHistorico.CodigoUnidadeDoIMA;
                oClientes.ContaGerencial = oClienteHistorico.ContaGerencial;
                oClientes.ContatoFatima = oClienteHistorico.ContatoFatima;
                oClientes.DataCadastro = oClienteHistorico.DataCadastro;
                oClientes.DataNascimento = oClienteHistorico.DataNascimento;
                oClientes.DataSenha = oClienteHistorico.DataSenha;
                oClientes.dtRG = oClienteHistorico.dtRG;
                oClientes.email = oClienteHistorico.email;
                oClientes.emailFatma = oClienteHistorico.emailFatma;
                oClientes.EnviarCDF = oClienteHistorico.EnviarCDF;
                oClientes.EnviarDDR = oClienteHistorico.EnviarDDR;
                oClientes.Filial = oClienteHistorico.Filial;
                oClientes.Inativo = oClienteHistorico.Inativo;
                oClientes.KmMedia = oClienteHistorico.KmMedia;
                oClientes.Nacionalidade = oClienteHistorico.Nacionalidade;
                oClientes.NaoAceitaDiferencaPeso = oClienteHistorico.NaoAceitaDiferencaPeso;
                oClientes.Naturalidade = oClienteHistorico.Naturalidade;
                oClientes.Nome = oClienteHistorico.Nome;
                oClientes.Nome2 = oClienteHistorico.Nome2;
                oClientes.NomeDaEmpresa = oClienteHistorico.NomeDaEmpresa;
                oClientes.NomeFantasia = oClienteHistorico.NomeFantasia;
                oClientes.NovaSenha = oClienteHistorico.NovaSenha;
                oClientes.OBS = oClienteHistorico.OBS;
                oClientes.ObsFatima = oClienteHistorico.ObsFatima;
                oClientes.Percentual = oClienteHistorico.Percentual;
                oClientes.Pessoa = oClienteHistorico.Pessoa;
                oClientes.PontoReferencia = oClienteHistorico.PontoReferencia;
                oClientes.RGEmit = oClienteHistorico.RGEmit;
                oClientes.RG_IE = oClienteHistorico.RG_IE;
                oClientes.SenhaAcessoFatima = oClienteHistorico.SenhaAcessoFatima;
                oClientes.SenhaMasterFatima = oClienteHistorico.SenhaMasterFatima;
                oClientes.site = oClienteHistorico.site;
                oClientes.SolicitadoSenhaPor = oClienteHistorico.SolicitadoSenhaPor;
                oClientes.TelefoneFatima = oClienteHistorico.TelefoneFatima;
                oClientes.TiposDeContrato = oClienteHistorico.TiposDeContrato;
                AtribuiDadosDaClasse(oClientes);
            }
        }
        private void MostraHistoricoDeAlteracao()
        {
            if (lblCodigoCliente.Text != "")
            {
                GradeHistorico.DataSource = oGeralDados.ConsultaQQ("select DataAlteracao, Status, Usuario from ClientesHistorico where CodigoCliente = " + lblCodigoCliente.Text +
                                                                   " order by codigo Desc");
                GradeHistorico.DataBind();
            }
        }
        private void MostraEnderecos()
        {
            // Endereço padrão = 0
            oEnderecoDados = new clsEnderecosDados();
            clsEnderecos oEndereco = new clsEnderecos();
            oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 0);
            txtEndereco0.Text = oEndereco.endereco;
            intNumero0.Valor = oEndereco.Numero;
            txtComplemento0.Text = oEndereco.Complemento;
            intDDD10.Valor = oEndereco.DDD1.ToString();
            txtFone10.Text = oEndereco.Fone1;
            txtBairro0.Text = oEndereco.Bairro;
            intDDD20.Valor = oEndereco.DDD2.ToString();
            txtFone20.Text = oEndereco.Fone2;
            txtCEP0.Text = oEndereco.CEP;
            intDDDC0.Valor = oEndereco.DDD3.ToString();
            txtCelular0.Text = oEndereco.Fone3;
            intCodigoClidade0.Valor = oEndereco.CodigoMunicipio.ToString();
            intDDDF0.Valor = oEndereco.DDDF.ToString();
            txtFax0.Text = oEndereco.Fax;
            txtemail0.Text = oEndereco.email;
            txtContato0.Text = oEndereco.Contato;
            clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
            clsMunicipios oMunicipios = new clsMunicipios();
            oMunicipioDados.PegaDados(oMunicipios, oEndereco.CodigoMunicipio);
            txtUF0.Text = oMunicipios.UF;
            txtCodigoIBGE0.Text = oMunicipios.CodigoIBGE;
            lbtNomeCidade0.Text = oMunicipios.Nome;

            // Endereço Faturamento = 1
            oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 1, 0);
            txtEndereco1.Text = oEndereco.endereco;
            intNumero1.Valor = oEndereco.Numero;
            txtComplemento1.Text = oEndereco.Complemento;
            intDDD11.Valor = oEndereco.DDD1.ToString();
            txtFone11.Text = oEndereco.Fone1;
            txtBairro1.Text = oEndereco.Bairro;
            intDDD21.Valor = oEndereco.DDD2.ToString();
            txtFone21.Text = oEndereco.Fone2;
            txtCEP1.Text = oEndereco.CEP;
            intDDDC1.Valor = oEndereco.DDD3.ToString();
            txtCelular1.Text = oEndereco.Fone3;
            intCodigoClidade1.Valor = oEndereco.CodigoMunicipio.ToString();
            intDDDF1.Valor = oEndereco.DDDF.ToString();
            txtFax1.Text = oEndereco.Fax;
            txtemail1.Text = oEndereco.email;
            txtInstrucoesFaturamento.Text = oEndereco.InstrucoesFat;
            txtContato1.Text = oEndereco.Contato;
            oMunicipioDados = new clsMunicipiosDados();
            oMunicipios = new clsMunicipios();
            oMunicipioDados.PegaDados(oMunicipios, oEndereco.CodigoMunicipio);
            txtUF1.Text = oMunicipios.UF;
            txtCodigoIBGE1.Text = oMunicipios.CodigoIBGE;
            lbtNomeCidade1.Text = oMunicipios.Nome;

            // Endereço Coleta = 2
            oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 2, 0);
            txtEndereco2.Text = oEndereco.endereco;
            intNumero2.Valor = oEndereco.Numero;
            txtComplemento2.Text = oEndereco.Complemento;
            intDDD12.Valor = oEndereco.DDD1.ToString();
            txtFone12.Text = oEndereco.Fone1;
            txtBairro2.Text = oEndereco.Bairro;
            intDDD22.Valor = oEndereco.DDD2.ToString();
            txtFone22.Text = oEndereco.Fone2;
            txtCEP2.Text = oEndereco.CEP;
            intDDDC2.Valor = oEndereco.DDD3.ToString();
            txtCelular2.Text = oEndereco.Fone3;
            intCodigoClidade2.Valor = oEndereco.CodigoMunicipio.ToString();
            intDDDF2.Valor = oEndereco.DDDF.ToString();
            txtFax2.Text = oEndereco.Fax;
            txtemail2.Text = oEndereco.email;
            txtContato2.Text = oEndereco.Contato;
            txtCargoContato.Text = oEndereco.CargoContato;

            oMunicipioDados = new clsMunicipiosDados();
            oMunicipios = new clsMunicipios();
            oMunicipioDados.PegaDados(oMunicipios, oEndereco.CodigoMunicipio);
            txtUF2.Text = oMunicipios.UF;
            txtCodigoIBGE2.Text = oMunicipios.CodigoIBGE;
            lbtNomeCidade2.Text = oMunicipios.Nome;

            //if (chkAtivo.Checked)
            if (imbInativo.ImageUrl.ToLower().IndexOf("selecionado") > -1)
            {
                // Endereço padrão = 0
                txtEndereco0.Enabled = false;
                intNumero0.Enabled = false;
                txtComplemento0.Enabled = false;
                //intDDD10.Enabled = false;
                txtFone10.Enabled = false;
                txtBairro0.Enabled = false;
                //intDDD20.Enabled = false;
                txtFone20.Enabled = false;
                txtCEP0.Enabled = false;
                //intDDDC0.Enabled = false;
                txtCelular0.Enabled = false;
                intCodigoClidade0.Enabled = false;
                //intDDDF0.Enabled = false;
                txtFax0.Enabled = false;
                txtemail0.Enabled = false;
                txtContato0.Enabled = false;
                txtUF0.Enabled = false;
                txtCodigoIBGE0.Enabled = false;
                lbtNomeCidade0.Enabled = false;

                // Endereço Faturamento = 1
                txtEndereco1.Enabled = false;
                intNumero1.Enabled = false;
                txtComplemento1.Enabled = false;
                //intDDD11.Enabled = false;
                txtFone11.Enabled = false;
                txtBairro1.Enabled = false;
                //intDDD21.Enabled = false;
                txtFone21.Enabled = false;
                txtCEP1.Enabled = false;
                //intDDDC1.Enabled = false;
                txtCelular1.Enabled = false;
                intCodigoClidade1.Enabled = false;
                //intDDDF1.Enabled = false;
                txtFax1.Enabled = false;
                txtemail1.Enabled = false;
                txtInstrucoesFaturamento.Enabled = false;
                txtContato1.Enabled = false;
                txtUF1.Enabled = false;
                txtCodigoIBGE1.Enabled = false;
                lbtNomeCidade1.Enabled = false;

                // Endereço Coleta = 2
                txtEndereco2.Enabled = false;
                intNumero2.Enabled = false;
                txtComplemento2.Enabled = false;
                txtFone12.Enabled = false;
                txtBairro2.Enabled = false;
                txtFone22.Enabled = false;
                txtCEP2.Enabled = false;
                txtCelular2.Enabled = false;
                intCodigoClidade2.Enabled = false;
                txtFax2.Enabled = false;
                txtemail2.Enabled = false;
                txtContato2.Enabled = false;
                txtCargoContato.Enabled = false;
                txtUF2.Enabled = false;
                txtCodigoIBGE2.Enabled = false;
                lbtNomeCidade2.Enabled = false;
                txtObservacao.Enabled = false;
            }
            else
            {
                // Endereço padrão = 0
                txtEndereco0.Enabled = true;
                intNumero0.Enabled = true;
                txtComplemento0.Enabled = true;
                //intDDD10.Enabled = true;
                txtFone10.Enabled = true;
                txtBairro0.Enabled = true;
                //intDDD20.Enabled = true;
                txtFone20.Enabled = true;
                txtCEP0.Enabled = true;
                //intDDDC0.Enabled = true;
                txtCelular0.Enabled = true;
                intCodigoClidade0.Enabled = true;
                //intDDDF0.Enabled = true;
                txtFax0.Enabled = true;
                txtemail0.Enabled = true;
                txtContato0.Enabled = true;
                txtUF0.Enabled = true;
                txtCodigoIBGE0.Enabled = true;
                lbtNomeCidade0.Enabled = true;

                // Endereço Faturamento = 1
                txtEndereco1.Enabled = true;
                intNumero1.Enabled = true;
                txtComplemento1.Enabled = true;
                //intDDD11.Enabled = true;
                txtFone11.Enabled = true;
                txtBairro1.Enabled = true;
                //intDDD21.Enabled = true;
                txtFone21.Enabled = true;
                txtCEP1.Enabled = true;
                //intDDDC1.Enabled = true;
                txtCelular1.Enabled = true;
                intCodigoClidade1.Enabled = true;
                //intDDDF1.Enabled = true;
                txtFax1.Enabled = true;
                txtemail1.Enabled = true;
                txtInstrucoesFaturamento.Enabled = true;
                txtContato1.Enabled = true;
                txtUF1.Enabled = true;
                txtCodigoIBGE1.Enabled = true;
                lbtNomeCidade1.Enabled = true;

                // Endereço Coleta = 2
                txtEndereco2.Enabled = true;
                intNumero2.Enabled = true;
                txtComplemento2.Enabled = true;
                txtFone12.Enabled = true;
                txtBairro2.Enabled = true;
                txtFone22.Enabled = true;
                txtCEP2.Enabled = true;
                txtCelular2.Enabled = true;
                intCodigoClidade2.Enabled = true;
                txtFax2.Enabled = true;
                txtemail2.Enabled = true;
                txtContato2.Enabled = true;
                txtCargoContato.Enabled = true;
                txtUF2.Enabled = true;
                txtCodigoIBGE2.Enabled = true;
                lbtNomeCidade2.Enabled = true;
                txtObservacao.Enabled = true;

            }
        }
        private static void MessageBox(Page _page, string Message)
        {
            _page.ClientScript.RegisterStartupScript
            (
                _page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
        private void LimparEnderecoColeta()
        {
            // Endereço Coleta = 2
            txtEndereco2.Text = "";
            intNumero2.Valor = "";
            txtComplemento2.Text = "";
            intDDD12.Valor = "";
            txtFone12.Text = "";
            txtBairro2.Text = "";
            intDDD22.Valor = "";
            txtFone22.Text = "";
            txtCEP2.Text = "";
            intDDDC2.Valor = "";
            txtCelular2.Text = "";
            intCodigoClidade2.Valor = "";
            intDDDF2.Valor = "";
            txtFax2.Text = "";
            txtemail2.Text = "";
            txtContato2.Text = "";
            txtCargoContato.Text = "";
            txtUF2.Text = "";
            txtCodigoIBGE2.Text = "";
            lbtNomeCidade2.Text = "";
        }
        protected void imbInativo_Click1(object sender, ImageClickEventArgs e)
        {
            lblMensagem.Text = "";
            if (imbInativo != null)
            {
                clsContratosDados oContratoDados = new clsContratosDados();
                if (imbInativo.ImageUrl.ToLower().IndexOf("selecionar") > -1)
                {
                    if (lblCodigoCliente.Text != "")
                    {
                        bool bExisteContratoEmAberto = false;
                        bExisteContratoEmAberto = oContratoDados.ExisteContratoEmAberto(Convert.ToInt32(lblCodigoCliente.Text));
                        if (bExisteContratoEmAberto)
                            lblMensagem.Text = "Não é possível inativar Cliente, pois existe contrato em aberto!";
                        else
                            imbInativo.ImageUrl = "~/Images/selecionado.png";
                    }
                }
                else
                {
                    imbInativo.ImageUrl = "~/Images/selecionar.png";
                }
            }
        }

        protected void GradeHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[1].Text != "" && e.Row.Cells[1].Text != "&nbsp;" && e.Row.Cells[1].Text != "Data")
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yy");
        }

        protected void GradeHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Status" && e.CommandArgument.ToString() != "Usuario")
            {
                if (Convert.ToInt32(e.CommandArgument) < 10)
                {
                    TiraCorHistoricoAlteracao();
                    MudaCorHistoricoAlteracao(Convert.ToInt32(e.CommandArgument));
                    // abrir tela contendo histórico de alteração colocando em cor diferente

                }
            }
        }
        private void MudaCorHistoricoAlteracao(int pLinha)
        {
            for (int i = 0; i <= 3; i++)
            {
                GradeHistorico.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.LightGray;
            }
        }
        private void TiraCorHistoricoAlteracao()
        {
            for (int ir = 0; ir <= GradeHistorico.Rows.Count - 1; ir++)
            {
                for (int i = 0; i <= 3; i++)
                    GradeHistorico.Rows[ir].Cells[i].BackColor = System.Drawing.Color.White;
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            HabilitaDesabilitaCampos(true);
        }

        protected void btnAlterar0_Click(object sender, EventArgs e)
        {
            // habilitar campos do primeiro endereco padrao
            HabilitaDesabilitaEnderecoPadrao(true);
            lblMensagem0.Text = "";
        }
        private void HabilitaDesabilitaEnderecoPadrao(bool bDesHabilita)
        {
            // Endereço padrão
            txtEndereco0.Enabled = bDesHabilita;
            intNumero0.Enabled = bDesHabilita;
            txtComplemento0.Enabled = bDesHabilita;
            intDDD10.Enabled = bDesHabilita;
            txtFone10.Enabled = bDesHabilita;
            txtBairro0.Enabled = bDesHabilita;
            txtUF0.Enabled = bDesHabilita;
            intDDD20.Enabled = bDesHabilita;
            txtFone20.Enabled = bDesHabilita;
            txtCEP0.Enabled = bDesHabilita;
            intDDDC0.Enabled = bDesHabilita;
            txtCelular0.Enabled = bDesHabilita;
            intCodigoClidade0.Enabled = bDesHabilita;
            intDDDF0.Enabled = bDesHabilita;
            txtFax0.Enabled = bDesHabilita;
            txtemail0.Enabled = bDesHabilita;
            txtContato0.Enabled = bDesHabilita;
            txtCodigoIBGE0.Enabled = bDesHabilita;
            lbtNomeCidade0.Enabled = bDesHabilita;

        }
        private void HabilitaDesabilitaEnderecoPadraoQuadro2(bool bDesHabilita)
        {
            // Endereço padrão
            txtEndereco3.Enabled = bDesHabilita;
            intNumero3.Enabled = bDesHabilita;
            txtComplemento3.Enabled = bDesHabilita;
            intDDD23.Enabled = bDesHabilita;
            txtFone23.Enabled = bDesHabilita;
            txtBairro3.Enabled = bDesHabilita;
            txtUF3.Enabled = bDesHabilita;
            intDDD24.Enabled = bDesHabilita;
            txtFone24.Enabled = bDesHabilita;
            txtCEP3.Enabled = bDesHabilita;
            intDDDC3.Enabled = bDesHabilita;
            txtCelular3.Enabled = bDesHabilita;
            intCodigoClidade3.Enabled = bDesHabilita;
            intDDDF3.Enabled = bDesHabilita;
            intDDDF4.Enabled = bDesHabilita;
            txtFax3.Enabled = bDesHabilita;
            txtemail3.Enabled = bDesHabilita;
            txtContato3.Enabled = bDesHabilita;
            txtCodigoIBGE3.Enabled = bDesHabilita;
            lbtNomeCidade3.Enabled = bDesHabilita;
        }
        private void HabilitaDesabilitaEnderecoFaturamento(bool bHabilitar)
        {
            //endereço faturamento
            txtEndereco1.Enabled = bHabilitar;
            intNumero1.Enabled = bHabilitar;
            txtComplemento1.Enabled = bHabilitar;
            intDDD11.Enabled = bHabilitar;
            txtFone11.Enabled = bHabilitar;
            txtBairro1.Enabled = bHabilitar;

            txtUF1.Enabled = bHabilitar;
            txtCodigoIBGE1.Enabled = bHabilitar;
            lbtNomeCidade1.Enabled = bHabilitar;
            intDDD21.Enabled = bHabilitar;
            txtFone21.Enabled = bHabilitar;
            txtCEP1.Enabled = bHabilitar;

            intDDDC1.Enabled = bHabilitar;
            txtCelular1.Enabled = bHabilitar;

            intCodigoClidade1.Enabled = bHabilitar;
            intDDDF1.Enabled = bHabilitar;
            txtFax1.Enabled = bHabilitar;
            txtemail1.Enabled = bHabilitar;
            txtInstrucoesFaturamento.Enabled = bHabilitar;
            txtContato1.Enabled = bHabilitar;
            txtCNPJ_CPF_Faturamento.Enabled = bHabilitar;
            txtAlfaCNPJ.Enabled = bHabilitar;

            ddlTipoCobranca.Enabled = bHabilitar;

        }

        private void HabilitaDesabilitaEnderecoFaturamentoQuadro2(bool bHabilitar)
        {
            //endereço faturamento
            txtEndereco4.Enabled = bHabilitar;
            intNumero5.Enabled = bHabilitar;
            txtComplemento4.Enabled = bHabilitar;
            intDDD25.Enabled = bHabilitar;
            txtFone25.Enabled = bHabilitar;
            txtBairro4.Enabled = bHabilitar;

            txtUF4.Enabled = bHabilitar;
            txtCodigoIBGE4.Enabled = bHabilitar;
            lbtNomeCidade4.Enabled = bHabilitar;
            intDDD26.Enabled = bHabilitar;
            txtFone26.Enabled = bHabilitar;
            txtCEP4.Enabled = bHabilitar;

            intDDDC3.Enabled = bHabilitar;
            txtCelular4.Enabled = bHabilitar;

            intCodigoClidade4.Enabled = bHabilitar;
            intDDDF5.Enabled = bHabilitar;
            txtFax4.Enabled = bHabilitar;
            txtemail4.Enabled = bHabilitar;
            txtInstrucoesFaturamento0.Enabled = bHabilitar;
            txtContato4.Enabled = bHabilitar;
            txtCNPJ_CPF_Faturamento0.Enabled = bHabilitar;
            txtAlfaCNPJ0.Enabled = bHabilitar;

            ddlTipoCobranca0.Enabled = bHabilitar;

        }
        private void HabilitaDesabilitaEnderecoColeta(bool bHabilitar)
        {
            intCodigoClidade2.Enabled = bHabilitar;
            intNumero2.Enabled = bHabilitar;
            txtEndereco2.Enabled = bHabilitar;
            txtComplemento2.Enabled = bHabilitar;
            intDDD12.Enabled = bHabilitar;
            txtFone12.Enabled = bHabilitar;
            txtBairro2.Enabled = bHabilitar;
            intDDD22.Enabled = bHabilitar;
            txtFone22.Enabled = bHabilitar;
            txtCEP2.Enabled = bHabilitar;
            intDDDC2.Enabled = bHabilitar;
            txtCelular2.Enabled = bHabilitar;
            intDDDF2.Enabled = bHabilitar;
            txtFax2.Enabled = bHabilitar;
            txtemail2.Enabled = bHabilitar;
            txtContato2.Enabled = bHabilitar;
            txtCargoContato.Enabled = bHabilitar;
            txtUF2.Enabled = bHabilitar;
            lbtNomeCidade2.Enabled = bHabilitar;
            txtCodigoIBGE2.Enabled = bHabilitar;
        }

        private void HabilitaDesabilitaEnderecoColetaQuadro2(bool bHabilitar)
        {
            intCodigoClidade5.Enabled = bHabilitar;
            intNumero6.Enabled = bHabilitar;
            txtEndereco5.Enabled = bHabilitar;
            txtComplemento5.Enabled = bHabilitar;
            intDDD27.Enabled = bHabilitar;
            txtFone27.Enabled = bHabilitar;
            txtBairro5.Enabled = bHabilitar;
            intDDD28.Enabled = bHabilitar;
            txtFone28.Enabled = bHabilitar;
            txtCEP5.Enabled = bHabilitar;
            txtemail5.Enabled = bHabilitar;
            txtContato5.Enabled = bHabilitar;
            txtCargoContato0.Enabled = bHabilitar;
            txtUF6.Enabled = bHabilitar;
            lbtNomeCidade5.Enabled = bHabilitar;
            txtCodigoIBGE6.Enabled = bHabilitar;
            intDDDC4.Enabled = bHabilitar;
            txtCelular5.Enabled = bHabilitar;
            intCodigoClidade5.Enabled = bHabilitar;
            intDDDF6.Enabled = bHabilitar;
            txtFax5.Enabled = bHabilitar;
        }
        private void HabilitaDesabilitaInformacoes(bool bHabilitar)
        {
            intKmMedia.Enabled = bHabilitar;
            txtPontoDeReferencia.Enabled = bHabilitar;
            txtObservacao.Enabled = bHabilitar;
            chkIncluirAtualizarDados.Enabled = bHabilitar;
        }
        private void HabilitaDesabilitaInformacoesQuadro2(bool bHabilitar)
        {
            intKmMedia0.Enabled = bHabilitar;
            txtPontoDeReferencia0.Enabled = bHabilitar;
            txtObservacao0.Enabled = bHabilitar;
            chkIncluirAtualizarDados0.Enabled = bHabilitar;
        }
        private void HabilitaDesabilitaSistemaIMA(bool bHabilitar)
        {
            txtSenhaMaster.Enabled = bHabilitar;
            txtSenhaAcesso.Enabled = bHabilitar;
            txtObsMTRFatima.Enabled = bHabilitar;
            txtContatoFatma.Enabled = bHabilitar;
            txtFoneFatma.Enabled = bHabilitar;
            txtemailFatma.Enabled = bHabilitar;
            intCodigoUnidadeIMA.Enabled = bHabilitar;
        }
        private void HabilitaDesabilitaSistemaIMAQuadro2(bool bHabilitar)
        {
            txtSenhaMaster0.Enabled = bHabilitar;
            txtSenhaAcesso0.Enabled = bHabilitar;
            txtObsMTRFatima0.Enabled = bHabilitar;
            txtContatoFatma0.Enabled = bHabilitar;
            txtFoneFatma0.Enabled = bHabilitar;
            txtemailFatma0.Enabled = bHabilitar;
            intCodigoUnidadeIMA0.Enabled = bHabilitar;
            chkClienteExigeMTReParaRCD0.Enabled = bHabilitar;
        }

        private void AtribuiDaClasseClienteHistoricoAbaDadosLadoDireito()
        {
            if (Request.QueryString["CodigoHistorico"] != "")
            {
                oClienteHistorico = new clsClientesHistorico();
                oClienteHistoricoDados = new clsClienteHistoricoDados();

                oClienteHistoricoDados.PegaDados(oClienteHistorico, Convert.ToInt32(Request.QueryString["CodigoHistorico"]));
                if (oClienteHistorico.Nome == "")
                {
                    // É em branco ? Quando não tem histórico é em branco
                    oClienteHistorico = new clsClientesHistorico();
                }

                // Aba Dados
                if (oClienteHistorico.DataCadastro == "01/01/0100" && oClienteHistorico.DataCadastro == "1/1/100")
                    datDataCadastro0.Data = "";
                else
                    datDataCadastro0.Data = oClienteHistorico.DataCadastro;
                txtNome0.Text = oClienteHistorico.Nome;
                txtNomeFantasia0.Text = oClienteHistorico.NomeFantasia;

                if (oClienteHistorico.Inativo == 1)
                    imbInativo0.ImageUrl = "../Images/Selecionado.png";
                else
                    imbInativo0.ImageUrl = "../Images/Selecionar.png";

                ddlTipoCadastro0.SelectedIndex = oClienteHistorico.TiposDeContrato;

                intCodigoAterro0.Valor = "";
                if (oClienteHistorico.CodigoNoAterro > 0)
                    intCodigoAterro0.Valor = oClienteHistorico.CodigoNoAterro.ToString();

                intCodigoFuncionarioComercial0.Valor = "";
                if (oClienteHistorico.CodigoFuncionarioComercial > 0)
                    intCodigoFuncionarioComercial0.Valor = oClienteHistorico.CodigoFuncionarioComercial.ToString();

                if (oClienteHistorico.DataSenha == "01/01/0100" || oClienteHistorico.DataSenha == "1/1/100" || oClienteHistorico.DataSenha == "01/01/0001" || oClienteHistorico.DataSenha == "1/1/0001")
                    datDataNovaSenha0.Data = "";
                else
                    datDataNovaSenha0.Data = oClienteHistorico.DataSenha;
                txtCNPJ_CPF0.Text = geral.RetiraLetras(oClienteHistorico.CNPJ_CPF);

                txtAlfaCNPJ0.Text = "";
                if ("ABCDEFGHIJKLMNOPQRSTUVVWXYZ".IndexOf(geral.Right(oClienteHistorico.CNPJ_CPF, 1)[0]) > -1)
                    txtAlfaCNPJ0.Text = geral.Right(oClienteHistorico.CNPJ_CPF, 1);

                txtAlfaCNPJ_CPF0.Text = "";
                if ("ABCDEFGHIJKLMNOPQRSTUVVWXYZ".IndexOf(geral.Right(oClienteHistorico.CNPJ_CPF, 1)[0]) > -1)
                    txtAlfaCNPJ_CPF0.Text = geral.Right(oClienteHistorico.CNPJ_CPF, 1);

                txtRG_IE0.Text = oClienteHistorico.RG_IE;
                txtCNPJ_CPF_Faturamento0.Text = oClienteHistorico.CNPJ_Faturamento;
                txtNovaSenha0.Text = oClienteHistorico.NovaSenha;
                txtSolicitadaPor0.Text = oClienteHistorico.SolicitadoSenhaPor;
                intClassificacao0.Valor = oClienteHistorico.Classificacao.ToString();
                intFilial0.Valor = oClienteHistorico.Filial.ToString();
                intContaGerencial0.Valor = oClienteHistorico.ContaGerencial.ToString();
                if (oClienteHistorico.CodigoBROOKS_Retencoes != "")
                {
                    foreach (ListItem lista in ddlAliquotasFederais0.Items)
                    {
                        if (geral.Left(lista.Value, 1) == oClienteHistorico.CodigoBROOKS_Retencoes)
                        {
                            ddlAliquotasFederais0.Text = lista.Value;
                        }
                    }
                }
                else
                {
                    ddlAliquotasFederais0.SelectedIndex = 0;
                }

                intCodigoExpNF0.Valor = oClienteHistorico.CodigoClienteExportacao.ToString();
                if (oClienteHistorico.EnviarDDR == 1)
                    chkEmiteDDR0.Checked = true;
                else
                    chkEmiteDDR0.Checked = false;
                if (oClienteHistorico.EnviarCDF == 1)
                    chkEmitirCDF0.Checked = true;
                else
                    chkEmitirCDF0.Checked = false;

                intKmMedia0.Valor = oClienteHistorico.KmMedia.ToString();
                txtPontoDeReferencia0.Text = oClienteHistorico.PontoReferencia;
                txtObservacao0.Text = oClienteHistorico.OBS;

                intCodigoUnidadeIMA0.Valor = oClienteHistorico.CodigoUnidadeDoIMA.ToString();
                txtSenhaMaster0.Text = oClienteHistorico.SenhaMasterFatima;
                txtSenhaAcesso0.Text = oClienteHistorico.SenhaAcessoFatima;
                txtObsMTRFatima0.Text = oClienteHistorico.ObsFatima;
                txtContatoFatma0.Text = oClienteHistorico.ContatoFatima;
                txtFoneFatma0.Text = oClienteHistorico.emailFatma;

                intCodigoUnidadeIMA0.Valor = oClienteHistorico.CodigoUnidadeDoIMA.ToString();

                if (oClienteHistorico.ClienteEmissaoMTReRCD == 1)
                    chkClienteExigeMTReParaRCD0.Checked = true;
                else
                    chkClienteExigeMTReParaRCD0.Checked = false;

            }
        }
        private void AtribuiDaClasseClienteHistoricoAbaEnderecoPadrao()
        {
            // Endereço padrão
            if (Request.QueryString["CodigoHistorico"] != "")
            {
                clsEnderecosHistoricoCadastro oEnderecoHistorico = new clsEnderecosHistoricoCadastro();
                clsEnderecosHistoricoDados oEnderecoHistoricoDados = new clsEnderecosHistoricoDados();

                if (oEnderecoHistoricoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 0, Convert.ToInt32(Request.QueryString["CodigoHistorico"])) == "Incluir" &&
                    Request.QueryString["CodigoHistoricoMaisNovo"].ToString() == "")
                {
                    oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 0);
                    txtEndereco3.Text = oEndereco.endereco;
                    intNumero3.Valor = oEndereco.Numero;
                    txtComplemento3.Text = oEndereco.Complemento;
                    intDDD23.Valor = oEndereco.DDD1.ToString();
                    txtFone23.Text = oEndereco.Fone1;
                    txtBairro3.Text = oEndereco.Bairro;
                    intDDD24.Valor = oEndereco.DDD2.ToString();
                    txtFone24.Text = oEndereco.Fone2;
                    txtCEP3.Text = oEndereco.CEP;
                    intDDDC3.Valor = oEndereco.DDD3.ToString();
                    txtCelular3.Text = oEndereco.Fone3;
                    intDDDF3.Valor = oEndereco.DDDF.ToString();
                    intDDDF4.Valor = oEndereco.DDDF.ToString();
                    txtFax3.Text = oEndereco.Fax;
                    txtemail3.Text = oEndereco.email;
                    txtContato3.Text = oEndereco.Contato;

                    intCodigoClidade3.Valor = oEndereco.CodigoMunicipio.ToString();
                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                    txtUF3.Text = oMunicipio.UF;
                    txtCodigoIBGE3.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade3.Text = oMunicipio.Nome;
                }
                else
                {
                    oEnderecoHistoricoDados.PegaDados(oEnderecoHistorico, Convert.ToInt32(Request.QueryString["CodigoHistorico"]), 0, 0); // 0 - endereco padrao

                    txtEndereco3.Text = oEnderecoHistorico.Endereco;
                    intNumero3.Valor = oEnderecoHistorico.Numero;
                    txtComplemento3.Text = oEnderecoHistorico.Complemento;
                    intDDD23.Valor = oEnderecoHistorico.DDD1.ToString();
                    txtFone23.Text = oEnderecoHistorico.Fone1;
                    txtBairro3.Text = oEnderecoHistorico.Bairro;
                    intDDD24.Valor = oEnderecoHistorico.DDD2.ToString();
                    txtFone24.Text = oEnderecoHistorico.Fone2;
                    txtCEP3.Text = oEnderecoHistorico.CEP;
                    intDDDC3.Valor = oEnderecoHistorico.DDD3.ToString();
                    txtCelular3.Text = oEnderecoHistorico.Fone3;
                    intDDDF3.Valor = oEnderecoHistorico.DDDF.ToString();
                    intDDDF4.Valor = oEnderecoHistorico.DDDF.ToString();
                    txtFax3.Text = oEnderecoHistorico.Fax;
                    txtemail3.Text = oEnderecoHistorico.email;
                    txtContato3.Text = oEnderecoHistorico.Contato;

                    intCodigoClidade3.Valor = oEnderecoHistorico.CodigoMunicipio.ToString();
                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEnderecoHistorico.CodigoMunicipio);
                    txtUF3.Text = oMunicipio.UF;
                    txtCodigoIBGE3.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade3.Text = oMunicipio.Nome;
                }
            }
        }
        private void AtribuiDaClasseClienteHistoricoAbaEnderecoFaturamento()
        {
            // Endereço Faturamento
            if (Request.QueryString["CodigoHistorico"] != "")
            {
                clsEnderecosHistoricoCadastro oEnderecoHistorico = new clsEnderecosHistoricoCadastro();
                clsEnderecosHistoricoDados oEnderecoHistoricoDados = new clsEnderecosHistoricoDados();

                if (oEnderecoHistoricoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 1, Convert.ToInt32(Request.QueryString["CodigoHistorico"])) == "Incluir" &&
                    Request.QueryString["CodigoHistoricoMaisNovo"].ToString() == "")
                {
                    oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 1, 0);

                    //endereço faturamento
                    txtEndereco4.Text = oEndereco.endereco;
                    intNumero5.Valor = oEndereco.Numero;
                    txtComplemento4.Text = oEndereco.Complemento;
                    intDDD25.Valor = oEndereco.DDD1.ToString();
                    txtFone25.Text = oEndereco.Fone1;
                    txtBairro4.Text = oEndereco.Bairro;

                    intDDD26.Valor = oEndereco.DDD2.ToString();
                    txtFone26.Text = oEndereco.Fone2;
                    txtCEP4.Text = oEndereco.CEP;

                    intDDDC3.Valor = oEndereco.DDD3.ToString();
                    txtCelular4.Text = oEndereco.Fone3;

                    intDDDF5.Valor = oEndereco.DDDF.ToString();
                    txtFax4.Text = oEndereco.Fax;

                    txtemail4.Text = oEndereco.email;
                    txtInstrucoesFaturamento0.Text = oEndereco.InstrucoesFat;
                    txtContato4.Text = oEndereco.Contato;

                    txtUF4.Text = "";
                    txtCodigoIBGE4.Text = "";
                    lbtNomeCidade4.Text = "";
                    intCodigoClidade4.Valor = "";
                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);

                    intCodigoClidade4.Valor = oEndereco.CodigoMunicipio.ToString();
                    txtUF4.Text = oMunicipio.UF;
                    txtCodigoIBGE4.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade4.Text = oMunicipio.Nome;
                }
                else
                {
                    oEnderecoHistoricoDados.PegaDados(oEnderecoHistorico, Convert.ToInt32(Request.QueryString["CodigoHistorico"]), 1, 0); // 1 - endereco faturamento

                    //endereço faturamento
                    txtEndereco4.Text = oEnderecoHistorico.Endereco;
                    intNumero5.Valor = oEnderecoHistorico.Numero;
                    txtComplemento4.Text = oEnderecoHistorico.Complemento;
                    intDDD25.Valor = oEnderecoHistorico.DDD1.ToString();
                    txtFone25.Text = oEnderecoHistorico.Fone1;
                    txtBairro4.Text = oEnderecoHistorico.Bairro;

                    intDDD26.Valor = oEnderecoHistorico.DDD2.ToString();
                    txtFone26.Text = oEnderecoHistorico.Fone2;
                    txtCEP4.Text = oEnderecoHistorico.CEP;

                    intDDDC3.Valor = oEnderecoHistorico.DDD3.ToString();
                    txtCelular4.Text = oEnderecoHistorico.Fone3;

                    intDDDF5.Valor = oEnderecoHistorico.DDDF.ToString();
                    txtFax4.Text = oEnderecoHistorico.Fax;

                    txtemail4.Text = oEnderecoHistorico.email;
                    txtInstrucoesFaturamento0.Text = oEnderecoHistorico.InstrucoesFat;
                    txtContato4.Text = oEnderecoHistorico.Contato;
                    //txtCNPJ_CPF_Faturamento0.Text = oEnderecoHistorico.
                    //txtAlfaCNPJ0.Enabled = bHabilitar;
                    //ddlTipoCobranca0.SelectedIndex = oEnderecoHistorico.tipo

                    txtUF4.Text = "";
                    txtCodigoIBGE4.Text = "";
                    lbtNomeCidade4.Text = "";
                    intCodigoClidade4.Valor = "";
                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEnderecoHistorico.CodigoMunicipio);

                    intCodigoClidade4.Valor = oEnderecoHistorico.CodigoMunicipio.ToString();
                    txtUF4.Text = oMunicipio.UF;
                    txtCodigoIBGE4.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade4.Text = oMunicipio.Nome;
                }
            }
        }
        private void AtribuiDaClasseClienteHistoricoAbaEnderecoColeta()
        {
            // Endereço Coleta
            if (Request.QueryString["CodigoHistorico"] != "")
            {
                clsEnderecosHistoricoCadastro oEnderecoHistorico = new clsEnderecosHistoricoCadastro();
                clsEnderecosHistoricoDados oEnderecoHistoricoDados = new clsEnderecosHistoricoDados();

                if (oEnderecoHistoricoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 2, Convert.ToInt32(Request.QueryString["CodigoHistorico"])) == "Incluir" &&
                    Request.QueryString["CodigoHistoricoMaisNovo"].ToString() == "")
                {
                    oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(hifCodigo.Value), 2, 0); // 2 - endereco coleta
                    intNumero6.Valor = oEndereco.Numero;
                    txtEndereco5.Text = oEndereco.endereco;
                    txtComplemento5.Text = oEndereco.Complemento;
                    intDDD27.Valor = oEndereco.DDD1.ToString();
                    txtFone27.Text = oEndereco.Fone1;
                    txtBairro5.Text = oEndereco.Bairro;
                    intDDD28.Valor = oEndereco.DDD2.ToString();
                    txtFone28.Text = oEndereco.Fone2;
                    txtCEP5.Text = oEndereco.CEP;

                    intDDDC4.Valor = oEndereco.DDD3.ToString();
                    txtCelular5.Text = oEndereco.Fone3;
                    intDDDF6.Valor = oEndereco.DDDF.ToString();
                    txtFax5.Text = oEndereco.Fax;
                    txtemail5.Text = oEndereco.email;
                    txtContato5.Text = oEndereco.Contato;
                    txtCargoContato0.Text = oEndereco.CargoContato;

                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);

                    intCodigoClidade5.Valor = oEndereco.CodigoMunicipio.ToString();
                    txtUF6.Text = oMunicipio.UF;
                    txtCodigoIBGE6.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade5.Text = oMunicipio.Nome;
                }
                else
                {
                    oEnderecoHistoricoDados.PegaDados(oEnderecoHistorico, Convert.ToInt32(Request.QueryString["CodigoHistorico"]), 2, 0); // 2 - endereco coleta

                    intNumero6.Valor = oEnderecoHistorico.Numero;
                    txtEndereco5.Text = oEnderecoHistorico.Endereco;
                    txtComplemento5.Text = oEnderecoHistorico.Complemento;
                    intDDD27.Valor = oEnderecoHistorico.DDD1.ToString();
                    txtFone27.Text = oEnderecoHistorico.Fone1;
                    txtBairro5.Text = oEnderecoHistorico.Bairro;
                    intDDD28.Valor = oEnderecoHistorico.DDD2.ToString();
                    txtFone28.Text = oEnderecoHistorico.Fone2;
                    txtCEP5.Text = oEnderecoHistorico.CEP;

                    intDDDC4.Valor = oEnderecoHistorico.DDD3.ToString();
                    txtCelular5.Text = oEnderecoHistorico.Fone3;
                    intDDDF6.Valor = oEnderecoHistorico.DDDF.ToString();
                    txtFax5.Text = oEnderecoHistorico.Fax;
                    txtemail5.Text = oEnderecoHistorico.email;
                    txtContato5.Text = oEnderecoHistorico.Contato;
                    txtCargoContato0.Text = oEnderecoHistorico.CargoContato;

                    oMunicipio = new clsMunicipios();
                    oMunicipioDados = new clsMunicipiosDados();
                    oMunicipioDados.PegaDados(oMunicipio, oEnderecoHistorico.CodigoMunicipio);

                    intCodigoClidade5.Valor = oEnderecoHistorico.CodigoMunicipio.ToString();
                    txtUF6.Text = oMunicipio.UF;
                    txtCodigoIBGE6.Text = oMunicipio.CodigoIBGE;
                    lbtNomeCidade5.Text = oMunicipio.Nome;
                }
            }
        }

        private void MostraDiferencasAbaDados()
        {
            // Aba Dados
            if (datDataCadastro.Data != datDataCadastro0.Data)
            {
                datDataCadastro.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtNome.Text != txtNome0.Text)
            {
                txtNome.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtNomeFantasia.Text != txtNomeFantasia0.Text)
            {
                txtNomeFantasia.BackColor = _BackColorLadoEsquerdo;
            }
            if (imbInativo.ImageUrl != imbInativo0.ImageUrl)
            {
                if (imbInativo.ImageUrl.IndexOf("SelecionadoAquamarine") > -1)
                    imbInativo.ImageUrl = "../Images/selecionadoAquamarine.png";
            }
            if (ddlTipoCadastro.SelectedIndex != ddlTipoCadastro0.SelectedIndex)
            {
                ddlTipoCadastro.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoAterro.Valor != intCodigoAterro0.Valor)
            {
                intCodigoAterro.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoFuncionarioComercial.Valor != intCodigoFuncionarioComercial0.Valor)
            {
                intCodigoFuncionarioComercial.BackColor = _BackColorLadoEsquerdo;
            }
            if (datDataNovaSenha.Data != datDataNovaSenha0.Data)
            {
                datDataNovaSenha.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCNPJ_CPF.Text != txtCNPJ_CPF0.Text)
            {
                txtCNPJ_CPF.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtAlfaCNPJ.Text != txtAlfaCNPJ0.Text)
            {
                txtAlfaCNPJ.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtRG_IE.Text != txtRG_IE0.Text)
            {
                txtRG_IE.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCNPJ_CPF_Faturamento.Text != txtCNPJ_CPF_Faturamento0.Text)
            {
                txtCNPJ_CPF_Faturamento.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtNovaSenha.Text != txtNovaSenha0.Text)
            {
                txtNovaSenha.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtSolicitadaPor.Text != txtSolicitadaPor0.Text)
            {
                txtSolicitadaPor.BackColor = _BackColorLadoEsquerdo;
            }
            if (intClassificacao.Valor != intClassificacao0.Valor)
            {
                intClassificacao.BackColor = _BackColorLadoEsquerdo;
            }
            if (intClassificacao.Valor != intClassificacao0.Valor)
            {
                intClassificacao.BackColor = _BackColorLadoEsquerdo;
            }
            if (intFilial.Valor != intFilial0.Valor)
            {
                intFilial.BackColor = _BackColorLadoEsquerdo;
            }
            if (intContaGerencial.Valor != intContaGerencial0.Valor)
            {
                intContaGerencial.BackColor = _BackColorLadoEsquerdo;
            }
            if (ddlAliquotasFederais.Text != ddlAliquotasFederais0.Text)
            {
                ddlAliquotasFederais.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoExpNF.Valor != intCodigoExpNF0.Valor)
            {
                intCodigoExpNF.BackColor = _BackColorLadoEsquerdo;
            }
            if (chkEmiteDDR.Checked != chkEmiteDDR0.Checked)
            {
                chkEmiteDDR.BackColor = _BackColorLadoEsquerdo;
            }
            if (chkEmitirCDF.Checked != chkEmitirCDF0.Checked)
            {
                chkEmitirCDF.BackColor = _BackColorLadoEsquerdo;
            }
            if (intKmMedia.Valor != intKmMedia0.Valor)
            {
                intKmMedia.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtPontoDeReferencia.Text != txtPontoDeReferencia0.Text)
            {
                txtPontoDeReferencia.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtObservacao.Text != txtObservacao0.Text)
            {
                txtObservacao.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoUnidadeIMA.Valor != intCodigoUnidadeIMA0.Valor)
            {
                intCodigoUnidadeIMA.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtSenhaMaster.Text != txtSenhaMaster0.Text)
            {
                txtSenhaMaster.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtSenhaAcesso.Text != txtSenhaAcesso0.Text)
            {
                txtSenhaAcesso.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtObsMTRFatima.Text != txtObsMTRFatima0.Text)
            {
                txtObsMTRFatima.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtContatoFatma.Text != txtContatoFatma0.Text)
            {
                txtContatoFatma.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFoneFatma.Text != txtFoneFatma0.Text)
            {
                txtFoneFatma.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoUnidadeIMA.Valor != intCodigoUnidadeIMA0.Valor)
            {
                intCodigoUnidadeIMA.BackColor = _BackColorLadoEsquerdo;
            }
            if (chkClienteExigeMTReParaRCD.Checked != chkClienteExigeMTReParaRCD0.Checked)
            {
                chkClienteExigeMTReParaRCD.BackColor = _BackColorLadoEsquerdo;
            }
        }
        private void MostraDiferencasAbaEnderecoPadrao()
        {
            if (txtEndereco0.Text != txtEndereco3.Text)
            {
                txtEndereco0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intNumero0.Valor != intNumero3.Valor)
            {
                intNumero0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtComplemento0.Text != txtComplemento3.Text)
            {
                txtComplemento0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD10.Valor != intDDD23.Valor)
            {
                intDDD10.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone10.Text != txtFone23.Text)
            {
                txtFone10.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtBairro0.Text != txtBairro3.Text)
            {
                txtBairro0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD20.Valor != intDDD24.Valor)
            {
                intDDD20.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone20.Text != txtFone24.Text)
            {
                txtFone20.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCEP0.Text != txtCEP3.Text)
            {
                txtCEP0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDC0.Valor != intDDDF3.Valor)
            {
                intDDDC0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCelular0.Text != txtCelular3.Text)
            {
                txtCelular0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDF0.Valor != intDDDF4.Valor)
            {
                intDDDF0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFax0.Text != txtFax3.Text)
            {
                txtFax0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtemail3.Text != txtemail3.Text)
            {
                txtemail0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtContato0.Text != txtContato3.Text)
            {
                txtContato0.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoClidade0.Valor != intCodigoClidade3.Valor)
            {
                intCodigoClidade0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtUF0.Text != txtUF3.Text)
            {
                txtUF0.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCodigoIBGE0.Text != txtCodigoIBGE3.Text)
            {
                txtCodigoIBGE0.BackColor = _BackColorLadoEsquerdo;
            }
            if (lbtNomeCidade0.Text != lbtNomeCidade3.Text)
            {
                lbtNomeCidade0.BackColor = _BackColorLadoEsquerdo;
            }
        }
        private void MostraDiferencasAbaEnderecoFaturamento()
        {
            if (txtEndereco1.Text != txtEndereco4.Text)
            {
                txtEndereco1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intNumero1.Valor != intNumero5.Valor)
            {
                intNumero1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtComplemento1.Text != txtComplemento4.Text)
            {
                txtComplemento1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD11.Valor != intDDD25.Valor)
            {
                intDDD11.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone11.Text != txtFone25.Text)
            {
                txtFone11.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtBairro1.Text != txtBairro4.Text)
            {
                txtBairro1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD21.Valor != intDDD26.Valor)
            {
                intDDD21.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone21.Text != txtFone26.Text)
            {
                txtFone21.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCEP1.Text != txtCEP4.Text)
            {
                txtCEP1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDC1.Valor != intDDDC3.Valor)
            {
                intDDDC1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCelular1.Text != txtCelular4.Text)
            {
                txtCelular1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDF1.Valor != intDDDF5.Valor)
            {
                intDDDF1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFax1.Text != txtFax4.Text)
            {
                txtFax1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtemail1.Text != txtemail4.Text)
            {
                txtemail1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtInstrucoesFaturamento.Text != txtInstrucoesFaturamento0.Text)
            {
                txtInstrucoesFaturamento.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtContato1.Text != txtContato4.Text)
            {
                txtContato1.BackColor = _BackColorLadoEsquerdo;
            }
            if (ddlTipoCobranca.SelectedIndex != ddlTipoCobranca0.SelectedIndex)
            {
                ddlTipoCobranca.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCNPJ_CPF_Faturamento.Text != txtCNPJ_CPF_Faturamento0.Text)
            {
                txtCNPJ_CPF_Faturamento.BackColor = _BackColorLadoEsquerdo;
                txtAlfaCNPJ.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtUF1.Text != txtUF4.Text)
            {
                txtUF1.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCodigoIBGE1.Text != txtCodigoIBGE4.Text)
            {
                txtCodigoIBGE1.BackColor = _BackColorLadoEsquerdo;
            }
            if (lbtNomeCidade1.Text != lbtNomeCidade4.Text)
            {
                lbtNomeCidade1.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoClidade1.Valor != intCodigoClidade4.Valor)
            {
                intCodigoClidade1.BackColor = _BackColorLadoEsquerdo;
            }
        }
        private void MostraDiferencasAbaEnderecoColeta()
        {
            if (intNumero2.Valor != intNumero6.Valor)
            {
                intNumero2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtEndereco2.Text != txtEndereco5.Text)
            {
                txtEndereco2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtComplemento2.Text != txtComplemento5.Text)
            {
                txtComplemento2.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD12.Valor != intDDD27.Valor)
            {
                intDDD12.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone12.Text != txtFone27.Text)
            {
                txtFone12.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtBairro2.Text != txtBairro5.Text)
            {
                txtBairro2.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDD22.Valor != intDDD28.Valor)
            {
                intDDD22.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFone22.Text != txtFone28.Text)
            {
                txtFone22.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCEP2.Text != txtCEP5.Text)
            {
                txtCEP2.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDC2.Valor != intDDDC4.Valor)
            {
                intDDDC2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCelular2.Text != txtCelular5.Text)
            {
                txtCelular2.BackColor = _BackColorLadoEsquerdo;
            }
            if (intDDDF2.Valor != intDDDF6.Valor)
            {
                intDDDF2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtFax2.Text != txtFax5.Text)
            {
                txtFax2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtemail2.Text != txtemail5.Text)
            {
                txtemail2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtContato2.Text != txtContato5.Text)
            {
                txtContato2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCargoContato.Text != txtCargoContato0.Text)
            {
                txtCargoContato.BackColor = _BackColorLadoEsquerdo;
            }
            if (intCodigoClidade2.Valor != intCodigoClidade5.Valor)
            {
                intCodigoClidade2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtUF2.Text != txtUF6.Text)
            {
                txtUF2.BackColor = _BackColorLadoEsquerdo;
            }
            if (txtCodigoIBGE2.Text != txtCodigoIBGE6.Text)
            {
                txtCodigoIBGE2.BackColor = _BackColorLadoEsquerdo;
            }
            if (lbtNomeCidade2.Text != lbtNomeCidade5.Text)
            {
                lbtNomeCidade2.BackColor = _BackColorLadoEsquerdo;
            }
        }
    }
}