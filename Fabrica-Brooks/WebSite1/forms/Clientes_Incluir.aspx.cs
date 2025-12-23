using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class webClientesIncluir : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsClientes oClientes = new clsClientes();
    clsClienteDados oClientesDados = new clsClienteDados();
    clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
    clsFuncionarios oFuncionario = new clsFuncionarios();
    clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();
    clsAliquotaImpostosDados oAliqDados = new clsAliquotaImpostosDados();
    DataTable _dt = new DataTable();
    clsGeral oGeralDados = new clsGeral();

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
            datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
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
                    txtNome.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("Nome");
                    txtNomeFantasia.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("NomeFantasia");
                    txtCNPJ_CPF.MaxLength = 18;
                    txtRG_IE.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("RG_IE");
                    txtCNPJ_CPF_Faturamento.MaxLength = 18;
                    txtNovaSenha.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("NovaSenha");
                    txtSolicitadaPor.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("SolicitadoSenhaPor");

                    txtEndereco0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Endereco");
                    txtComplemento0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Complemento");
                    txtFone10.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone1");
                    txtBairro0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Bairro");
                    txtFone20.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone2");
                    txtCEP0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("CEP");
                    txtCelular0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone3");
                    txtFax0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fax");
                    txtemail0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("email");
                    txtContato0.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Contato");
                    txtUF0.MaxLength = 2;
                    lbtNomeCidade0.MaxLength = 35;
                    txtCodigoIBGE0.MaxLength = 8;

                    txtEndereco1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Endereco");
                    txtComplemento1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Complemento");
                    txtFone11.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone1");
                    txtBairro1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Bairro");
                    txtFone21.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone2");
                    txtCEP1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("CEP");
                    txtCelular1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone3");
                    txtFax1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fax");
                    txtemail1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("email");
                    txtInstrucoesFaturamento.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("InstrucoesFat");
                    txtContato1.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Contato");
                    txtUF1.MaxLength = 2;
                    lbtNomeCidade1.MaxLength = 35;
                    txtCodigoIBGE1.MaxLength = 8;

                    txtEndereco2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Endereco");
                    txtComplemento2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Complemento");
                    txtFone12.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone1");
                    txtBairro2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Bairro");
                    txtFone22.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone2");
                    txtCEP2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("CEP");
                    txtCelular2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fone3");
                    txtFax2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Fax");
                    txtemail2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("email");
                    txtContato2.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("Contato");
                    txtCargoContato.MaxLength = oEnderecoDados.PegaTamanhoCampoVarChar("CargoContato");
                    txtUF2.MaxLength = 2;
                    lbtNomeCidade2.MaxLength = 35;
                    txtCodigoIBGE2.MaxLength = 8;

                    txtSenhaMaster.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("SenhaMasterFatima");
                    txtSenhaAcesso.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("SenhaAcessoFatima");
                    txtObsMTRFatima.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("ObsFatima");
                    txtContatoFatma.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("ContatoFatima");
                    txtFoneFatma.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("TelefoneFatima");
                    txtemailFatma.MaxLength = oClientesDados.PegaTamanhoCampoVarChar("emailFatma");

                    lblMensagem.Text = "";
                    MensagemZeroMaxCampo(ViewDados.Controls);
                    MensagemZeroMaxCampo(ViewEnderecoColeta.Controls);
                    MensagemZeroMaxCampo(ViewEnderecoFaturamento.Controls);
                    MensagemZeroMaxCampo(ViewEnderecoPadrao.Controls);
                    MensagemZeroMaxCampo(ViewSistemaFatima.Controls);

                    PermissaoIncluir();
                    PermissaoAlterar();

                    string _ultimoCodigo = oClientesDados.PegaUltimoCodigoCadastrado(false);
                    lblCodigoCliente.Text = "1";
                    if (_ultimoCodigo != "")
                    {
                        lblCodigoCliente.Text = (Convert.ToInt32(_ultimoCodigo) + 1).ToString();
                    }
                    lblCodigoCliente.Visible = false;
                    lblTituloCodigoCliente.Visible = false;
                    intClassificacao.Valor = "1488";
                    intFilial.Valor = "1";
                    intContaGerencial.Valor = "365";
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
            string r = Request.QueryString["view"];
            if (r != null)
                ViewStateSetForm();

        }
        if (Session["oMunicipio1"] != null)
        {
            intCodigoClidade0.Valor = ((clsMunicipios)Session["oMunicipio1"]).Codigo.ToString();
            lbtNomeCidade0.Text = ((clsMunicipios)Session["oMunicipio1"]).Nome;
            txtCodigoIBGE0.Text = ((clsMunicipios)Session["oMunicipio1"]).CodigoIBGE;
            Session["oMunicipio1"] = null;
        }
        else if (Session["oMunicipio2"] != null)
        {
            intCodigoClidade1.Valor = ((clsMunicipios)Session["oMunicipio2"]).Codigo.ToString();
            lbtNomeCidade1.Text = ((clsMunicipios)Session["oMunicipio2"]).Nome;
            txtCodigoIBGE1.Text = ((clsMunicipios)Session["oMunicipio2"]).CodigoIBGE;
            Session["oMunicipio2"] = null;
        }
        else if (Session["oMunicipio3"] != null)
        {
            intCodigoClidade2.Valor = ((clsMunicipios)Session["oMunicipio3"]).Codigo.ToString();
            lbtNomeCidade2.Text = ((clsMunicipios)Session["oMunicipio3"]).Nome;
            txtCodigoIBGE2.Text = ((clsMunicipios)Session["oMunicipio3"]).CodigoIBGE;
            Session["oMunicipio3"] = null;
        }

        // guardar dados para replicar nas abas de endereço
        hidEndereco0.Value = txtEndereco0.Text;
        hidNumero0.Value = intNumero0.Valor;
        hidComplemento0.Value = txtComplemento0.Text;
        hidDDD1_0.Value = intDDD10.Valor;
        hidFone1_0.Value = txtFone10.Text;
        hidDDD2_0.Value = intDDD20.Valor;
        hidFone2_0.Value = txtFone20.Text;
        hidDDD3_0.Value = intDDDC0.Valor;
        hidFone3_0.Value = txtFax0.Text;
        hidDDD4_0.Value = intDDDF0.Valor;
        hidFone4_0.Value = txtCelular0.Text;
        hidBairro0.Value = txtBairro0.Text;
        hidCEP0.Value = txtCEP0.Text;
        hidUF0.Value = txtUF0.Text;
        hidCodigoIBGE0.Value = txtCodigoIBGE0.Text;
        hidCodigoCidade0.Value = intCodigoClidade0.Valor;
        hidEmail0.Value = txtemail0.Text;
        hidContato0.Value = txtContato0.Text;

        hidEndereco1.Value = txtEndereco1.Text;
        hidNumero1.Value = intNumero1.Valor;
        hidComplemento1.Value = txtComplemento1.Text;
        hidDDD1_1.Value = intDDD11.Valor;
        hidFone1_1.Value = txtFone11.Text;
        hidDDD2_1.Value = intDDD21.Valor;
        hidFone2_1.Value = txtFone21.Text;
        hidDDD3_1.Value = intDDDC1.Valor;
        hidFone3_1.Value = txtFax1.Text;
        hidDDD4_1.Value = intDDDF1.Valor;
        hidFone4_1.Value = txtCelular1.Text;
        hidBairro1.Value = txtBairro1.Text;
        hidCEP1.Value = txtCEP1.Text;
        hidUF1.Value = txtUF1.Text;
        hidCodigoIBGE1.Value = txtCodigoIBGE1.Text;
        hidCodigoCidade1.Value = intCodigoClidade1.Valor;
        hidEmail1.Value = txtemail1.Text;
        hidContato1.Value = txtContato1.Text;

        txtNome.Focus();
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
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Incluir == 0)
        {
            Salvar.Enabled = false;
        }
    }

    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }

    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oClientesDados.DadoExiste(oClientes.Codigo);
        if (txtNomeFantasia.Text.Equals(""))
        {
            lblMensagem.Text = "Cliente inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oClientesDados.Excluir(oClientes.Codigo, "");
            lblMensagem.Text = "Cliente excluído com sucesso!";
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro de Clientes";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código cliente: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data Cadastro: " + datDataCadastro.Data + " \n";
        if (chkNaoAceitaDTRComPesoDiferente.Checked)
            oLog.Log = oLog.Log + "Aceita DTR Com Peso Diferente \n";
        else
            oLog.Log = oLog.Log + "Não Aceita DTR Com Peso Diferente \n";
        oLog.Log = oLog.Log + "Tipo Cadastro: " + ddlTipoCadastro.Text + " \n";
        oLog.Log = oLog.Log + "Código aterro: " + intCodigoAterro.Valor + " \n";
        oLog.Log = oLog.Log + "Código func-coml:" + intCodigoFuncionarioComercial.Valor + " \n";
        oLog.Log = oLog.Log + "Nome: " + txtNome.Text + " \n";
        oLog.Log = oLog.Log + "Nome fantasia: " + txtNomeFantasia.Text + " \n";
        oLog.Log = oLog.Log + "CNPJ/CPF: " + txtCNPJ_CPF.Text + txtAlfaCNPJ_CPF.Text + " \n";
        oLog.Log = oLog.Log + "RG/IE: " + txtRG_IE.Text + " \n";
        oLog.Log = oLog.Log + "Classificação: " + intClassificacao.Valor + " \n";
        oLog.Log = oLog.Log + "Filial: " + intFilial.Valor + " \n";
        oLog.Log = oLog.Log + "Conta gerencial: " + intContaGerencial.Valor + " \n";
        oLog.Log = oLog.Log + "Código Exp NF: " + intCodigoExpNF.Valor + " \n";
        if (chkEmiteDDR.Checked)
            oLog.Log = oLog.Log + "Emite DDR \n";
        else
            oLog.Log = oLog.Log + "Não Emite DDR \n";
        if (chkEmitirCDF.Checked)
            oLog.Log = oLog.Log + "Emitir CDF \n";
        else
            oLog.Log = oLog.Log + "Não Emitir CDF \n";
        oLog.Log = oLog.Log + "Nova senha: " + txtNovaSenha.Text + " \n";
        oLog.Log = oLog.Log + "Data Nova Senha: " + datDataNovaSenha.Data + " \n";
        oLog.Log = oLog.Log + "Solicitado Senha Por: " + txtSolicitadaPor.Text + " \n";

        oLog.Log = oLog.Log + "Km Media: " + intKmMedia.Valor + " \n";
        oLog.Log = oLog.Log + "PontoReferencia: " + txtPontoDeReferencia.Text + " \n";
        oLog.Log = oLog.Log + "Obs: " + txtObservacao.Text + " \n";
        oLog.Log = oLog.Log + "Senha Master IMA: " + txtSenhaMaster.Text + " \n";
        oLog.Log = oLog.Log + "Senha Acesso IMA: " + txtSenhaAcesso.Text + " \n";
        oLog.Log = oLog.Log + "Obs IMA: " + txtObsMTRFatima.Text + " \n";
        oLog.Log = oLog.Log + "Contato IMA: " + txtContatoFatma.Text + " \n";
        oLog.Log = oLog.Log + "Telefone IMA: " + txtFoneFatma.Text + " \n";
        oLog.Log = oLog.Log + "email IMA: " + txtemailFatma.Text + " \n";
        oLog.Log = oLog.Log + "Tipo de cobrança:" + ddlTipoCobranca.Text + " \n";

        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Ok")
        {
            if (txtNomeFantasia.Text.Equals(""))
            {
                lblMensagem.Text = "Cliente inválido!";
                MessageBox(this.Page, lblMensagem.Text);
            }
            else if (txtCNPJ_CPF.Text.Equals("") || txtCNPJ_CPF.Text.Length < 11)
            {
                lblMensagem.Text = "CNPJ ou CPF inválido!";
                MessageBox(this.Page, lblMensagem.Text);
            }
            else if (txtRG_IE.Text.Equals(""))
            {
                lblMensagem.Text = "RG ou Inscrição Estadual inválido(a)!";
                MessageBox(this.Page, lblMensagem.Text);
            }
            else if (intClassificacao.Valor.Equals(""))
            {
                lblMensagem.Text = "Classificação inválido(a)!";
                MessageBox(this.Page, lblMensagem.Text);
            }
            else if (intContaGerencial.Valor.Equals(""))
            {
                lblMensagem.Text = "Conta Gerencial inválido(a)!";
                MessageBox(this.Page, lblMensagem.Text);
            }
            else if (ddlAliquotasFederais.SelectedIndex == 0)
            {
                lblMensagem.Text = "Alíquota do imposto federal inválido!";
                MessageBox(this.Page, lblMensagem.Text);
            }

            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oClientes = AtribuiDadosDoForm(oClientes);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                else
                {
                    if (oClientes.Codigo > 0)
                        lblCodigoCliente.Text = oClientes.Codigo.ToString("");
                    else
                        lblCodigoCliente.Text = hifCodigo.Value;
                }
                lblMensagem.Text = oClientesDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    clsClienteDados oClienteDadosCNPJCPF = new clsClienteDados();
                    if (oClienteDadosCNPJCPF.ExisteCNPJ_CPF(txtCNPJ_CPF.Text + txtAlfaCNPJ_CPF.Text))
                    {
                        lblMensagem.Text = "Tentativa de incluir Cliente com CNPJ/CPF Existente!";
                        MessageBox(this.Page, lblMensagem.Text);
                    }
                    else
                    {
                        SalvarLog("Inclusão");
                        try
                        {
                            oClientesDados.Inserir(oClientes);
                            lblCodigoCliente.Text = oClientesDados.PegaUltimoCodigoCadastrado();
                            hifCodigo.Value = lblCodigoCliente.Text;
                            lblCodigoCliente.Visible = true;
                            lblTituloCodigoCliente.Visible = true;
                        }
                        finally
                        {
                            // essencial pra não adicionar esse codigo novamente
                            lblMensagem.Text = "Cliente incluído com sucesso!";

                            // quando incluir ir para próxima aba
                            MultiView1.ActiveViewIndex++;
                            if (MultiView1.ActiveViewIndex > MultiView1.Views.Count - 1)
                                Menu1.Items[0].Selected = true;
                            else
                                Menu1.Items[MultiView1.ActiveViewIndex].Selected = true;
                            if (Salvar.Text != "Confirma")
                            {
                                PermissaoIncluir();
                                PermissaoAlterar();
                            }
                        }
                    }
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    if (lblCodigoCliente.Text != "")
                        hifCodigo.Value = lblCodigoCliente.Text;

                    string msgErr = oClientesDados.Alterar(oClientes, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                    {
                        lblMensagem.Text = msgErr;
                    }
                    else
                    {
                        lblMensagem.Text = "Cliente alterado com sucesso!"; // cliente já incluído na primeira aba dados
                        // quando incluir ir para próxima aba
                        int _aview = MultiView1.ActiveViewIndex + 1;
                        if (_aview > MultiView1.Views.Count - 1)
                        {
                            MultiView1.ActiveViewIndex = 0;
                            Menu1.Items[0].Selected = true;
                            LimpaCampos();
                            LimpaCamposEnderecos();
                            string _ultimoCodigo = oClientesDados.PegaUltimoCodigoCadastrado(false);
                            lblCodigoCliente.Text = "1";
                            if (_ultimoCodigo != "")
                            {
                                lblCodigoCliente.Text = (Convert.ToInt32(_ultimoCodigo) + 1).ToString();
                                lblMensagem.Text = "";
                            }
                            lblCodigoCliente.Visible = false;
                            lblTituloCodigoCliente.Visible = false;
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex++;
                            Menu1.Items[MultiView1.ActiveViewIndex].Selected = true;
                        }
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Inclusão de Clientes";
            }
        }
        Salvar.Enabled = true;
        PermissaoIncluir();
    }
    protected void AtribuiDadosDaClasse(clsClientes pCliente)
    {
        lblCodigoCliente.Text = pCliente.Codigo.ToString("");
        if (pCliente.DataCadastro == "01/01/0001" || pCliente.DataCadastro == "01/01/0100" || pCliente.DataCadastro == null || pCliente.DataCadastro == "")
            datDataCadastro.Data = "";
        else
            datDataCadastro.Data = Convert.ToDateTime(pCliente.DataCadastro).ToString("dd/MM/yyyy");
        if (pCliente.NaoAceitaDiferencaPeso == 1)
            chkNaoAceitaDTRComPesoDiferente.Checked = true;
        else
            chkNaoAceitaDTRComPesoDiferente.Checked = false;
        ddlTipoCadastro.SelectedIndex = pCliente.TiposDeContrato;
        intCodigoAterro.Valor = pCliente.CodigoNoAterro.ToString();
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
        if (pCliente.Inativo == 1)
        {
            datDataCadastro.Enabled = false;
            chkNaoAceitaDTRComPesoDiferente.Enabled = false;
            ddlTipoCadastro.Enabled = false;
            intCodigoAterro.Enabled = false;
            txtNome.Enabled = false;
            txtNomeFantasia.Enabled = false;
            txtCNPJ_CPF.Enabled = false;
            txtAlfaCNPJ_CPF.Enabled = false;
            txtRG_IE.Enabled = false;
            intClassificacao.Enabled = false;
            intFilial.Enabled = false;
            intContaGerencial.Enabled = false;
            intCodigoExpNF.Enabled = false;
            chkEmiteDDR.Enabled = false;
            chkEmitirCDF.Enabled = false;
            txtNovaSenha.Enabled = false;
            datDataNovaSenha.Enabled = false;
            txtSolicitadaPor.Enabled = false;
            intKmMedia.Enabled = false;
            txtPontoDeReferencia.Enabled = false;
            txtObservacao.Text = pCliente.OBS;
            txtSenhaMaster.Enabled = false;
            txtSenhaAcesso.Enabled = false;
            txtObsMTRFatima.Enabled = false;
            txtContatoFatma.Enabled = false;
            txtFoneFatma.Enabled = false;
            txtemailFatma.Enabled = false;
            intCodigoUnidadeIMA.Enabled = false;
            ddlTipoCobranca.Enabled = false;
            lblNomeFuncionario.Enabled = false;
            lblNomeFuncionario.Enabled = false;
            ddlAliquotasFederais.Enabled = false;
        }
        else
        {
            datDataCadastro.Enabled = true;
            chkNaoAceitaDTRComPesoDiferente.Enabled = true;
            ddlTipoCadastro.Enabled = true;
            intCodigoAterro.Enabled = true;
            txtNome.Enabled = true;
            txtNomeFantasia.Enabled = true;
            txtCNPJ_CPF.Enabled = true;
            txtAlfaCNPJ_CPF.Enabled = true;
            txtRG_IE.Enabled = true;
            intClassificacao.Enabled = true;
            intFilial.Enabled = true;
            intContaGerencial.Enabled = true;
            intCodigoExpNF.Enabled = true;
            chkEmiteDDR.Enabled = true;
            chkEmitirCDF.Enabled = true;
            txtNovaSenha.Enabled = true;
            datDataNovaSenha.Enabled = true;
            txtSolicitadaPor.Enabled = true;
            intKmMedia.Enabled = true;
            txtPontoDeReferencia.Enabled = true;
            txtObservacao.Text = pCliente.OBS;
            txtSenhaMaster.Enabled = true;
            txtSenhaAcesso.Enabled = true;
            txtObsMTRFatima.Enabled = true;
            txtContatoFatma.Enabled = true;
            txtFoneFatma.Enabled = true;
            txtemailFatma.Enabled = true;
            intCodigoUnidadeIMA.Enabled = true;
            ddlTipoCobranca.Enabled = true;
            lblNomeFuncionario.Enabled = true;
            lblNomeFuncionario.Enabled = true;
            ddlAliquotasFederais.Enabled = true;
        }
        MostraEnderecos();
    }

    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");

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
    protected clsClientes AtribuiDadosDoForm(clsClientes pCliente)
    {
        if (datDataCadastro.Data == "")
            pCliente.DataCadastro = "01/01/0001";
        else
            pCliente.DataCadastro = Convert.ToDateTime(datDataCadastro.Data).ToString("dd/MM/yyyy");

        if (chkNaoAceitaDTRComPesoDiferente.Checked)
            pCliente.NaoAceitaDiferencaPeso = 1;
        else
            pCliente.NaoAceitaDiferencaPeso = 0;
        pCliente.TiposDeContrato = ddlTipoCadastro.SelectedIndex;
        if (intCodigoAterro.Valor != "")
            pCliente.CodigoNoAterro = Convert.ToInt32(intCodigoAterro.Valor);
        pCliente.Nome = txtNome.Text.ToUpper();
        pCliente.NomeFantasia = txtNomeFantasia.Text.ToUpper();
        pCliente.CNPJ_CPF = txtCNPJ_CPF.Text + txtAlfaCNPJ_CPF.Text;
        pCliente.RG_IE = txtRG_IE.Text;
        pCliente.CNPJ_Faturamento = txtCNPJ_CPF_Faturamento.Text + txtAlfaCNPJ.Text;
        if (intClassificacao.Valor != "")
            pCliente.Classificacao = Convert.ToInt32(intClassificacao.Valor);
        if (intFilial.Valor != "")
            pCliente.Filial = Convert.ToInt32(intFilial.Valor);
        if (intContaGerencial.Valor != "")
            pCliente.ContaGerencial = Convert.ToInt32(intContaGerencial.Valor);
        if (intCodigoExpNF.Valor != "")
            pCliente.CodigoClienteExportacao = Convert.ToInt32(intCodigoExpNF.Valor);
        if (chkEmiteDDR.Checked)
            pCliente.EnviarDDR = 1;
        else
            pCliente.EnviarDDR = 0;
        if (chkEmitirCDF.Checked)
            pCliente.EnviarCDF = 1;
        else
            pCliente.EnviarCDF = 0;
        pCliente.NovaSenha = txtNovaSenha.Text;
        if (datDataNovaSenha.Data == "")
            pCliente.DataSenha = "01/01/0001";
        else
            pCliente.DataSenha = Convert.ToDateTime(datDataNovaSenha.Data).ToString("dd/MM/yyyy"); ;
        pCliente.SolicitadoSenhaPor = txtSolicitadaPor.Text.ToUpper();

        if (intKmMedia.Valor != "")
            pCliente.KmMedia = Convert.ToInt32(intKmMedia.Valor);
        pCliente.PontoReferencia = txtPontoDeReferencia.Text.ToUpper();
        pCliente.OBS = txtObservacao.Text.ToUpper();

        pCliente.SenhaMasterFatima = txtSenhaMaster.Text;
        pCliente.SenhaAcessoFatima = txtSenhaAcesso.Text;
        pCliente.ObsFatima = geral.Left(txtObsMTRFatima.Text.ToUpper(), 20);
        pCliente.ContatoFatima = txtContatoFatma.Text.ToUpper();
        pCliente.TelefoneFatima = txtFoneFatma.Text.ToUpper();
        pCliente.emailFatma = txtemailFatma.Text;
        if (intCodigoUnidadeIMA.Valor != "" && intCodigoUnidadeIMA.Valor != null)
            pCliente.CodigoUnidadeDoIMA = Convert.ToInt32(intCodigoUnidadeIMA.Valor);
        pCliente.ClienteEmissaoMTReRCD = 0;
        pCliente.CodigoTipoCobranca = Convert.ToInt16((ddlTipoCobranca.SelectedIndex + 1).ToString());
        if (intCodigoFuncionarioComercial.Valor != "" && intCodigoFuncionarioComercial.Valor != null)
            pCliente.CodigoFuncionarioComercial = Convert.ToInt16(intCodigoFuncionarioComercial.Valor);
        pCliente.CodigoBROOKS_Retencoes = geral.Left(ddlAliquotasFederais.Text, 1);
        return pCliente;
    }
    protected clsEnderecos AtribuiDadosDoFormEndereco(clsEnderecos pEndereco, int pTipoEndereco)
    {
        if (pTipoEndereco == 0) //endereço padrão
        {
            pEndereco.endereco = txtEndereco0.Text.ToUpper();
            pEndereco.Numero = intNumero0.Valor;
            pEndereco.Complemento = txtComplemento0.Text.ToUpper();
            if (intDDD10.Valor != "")
                pEndereco.DDD1 = Convert.ToInt16(intDDD10.Valor);
            pEndereco.Fone1 = txtFone10.Text;
            pEndereco.Bairro = txtBairro0.Text.ToUpper();
            if (intDDD20.Valor != "")
                pEndereco.DDD2 = Convert.ToInt16(intDDD20.Valor);
            pEndereco.Fone2 = txtFone20.Text;
            pEndereco.CEP = txtCEP0.Text;
            if (intDDDC0.Valor != "")
                pEndereco.DDD3 = Convert.ToInt16(intDDDC0.Valor);
            pEndereco.Fone3 = txtCelular0.Text;
            if (intCodigoClidade0.Valor != "" && intCodigoClidade0.Valor != null)
                pEndereco.CodigoMunicipio = Convert.ToInt32(intCodigoClidade0.Valor);
            if (intDDDF0.Valor != "")
                pEndereco.DDDF = Convert.ToInt16(intDDDF0.Valor);
            pEndereco.Fax = txtFax0.Text;
            pEndereco.email = txtemail0.Text;
            pEndereco.Contato = txtContato0.Text.ToUpper();
        }
        else if (pTipoEndereco == 1) //endereço faturamento
        {
            pEndereco.endereco = txtEndereco1.Text.ToUpper();
            pEndereco.Numero = intNumero1.Valor;
            pEndereco.Complemento = txtComplemento1.Text.ToUpper();
            if (intDDD11.Valor != "")
                pEndereco.DDD1 = Convert.ToInt16(intDDD11.Valor);
            pEndereco.Fone1 = txtFone11.Text;
            pEndereco.Bairro = txtBairro1.Text.ToUpper();
            if (intDDD21.Valor != "")
                pEndereco.DDD2 = Convert.ToInt16(intDDD21.Valor);
            pEndereco.Fone2 = txtFone21.Text;
            pEndereco.CEP = txtCEP1.Text;
            if (intDDDC1.Valor != "")
                pEndereco.DDD3 = Convert.ToInt16(intDDDC1.Valor);
            pEndereco.Fone3 = txtCelular1.Text;
            if (intCodigoClidade1.Valor != "" && intCodigoClidade1.Valor != null)
                pEndereco.CodigoMunicipio = Convert.ToInt32(intCodigoClidade1.Valor);
            if (intDDDF1.Valor != "")
                pEndereco.DDDF = Convert.ToInt16(intDDDF1.Valor);
            pEndereco.Fax = txtFax1.Text;
            pEndereco.email = txtemail1.Text;
            pEndereco.InstrucoesFat = txtInstrucoesFaturamento.Text;
            pEndereco.Contato = txtContato1.Text.ToUpper();
        }
        else if (pTipoEndereco == 2) //endereço coleta
        {
            pEndereco.endereco = txtEndereco2.Text.ToUpper();
            pEndereco.Numero = intNumero2.Valor;
            pEndereco.Complemento = txtComplemento2.Text.ToUpper();
            if (intDDD12.Valor != "")
                pEndereco.DDD1 = Convert.ToInt16(intDDD12.Valor);
            pEndereco.Fone1 = txtFone12.Text;
            pEndereco.Bairro = txtBairro2.Text.ToUpper();
            if (intDDD22.Valor != "")
                pEndereco.DDD2 = Convert.ToInt16(intDDD22.Valor);
            pEndereco.Fone2 = txtFone22.Text;
            pEndereco.CEP = txtCEP2.Text;
            if (intDDDC2.Valor != "")
                pEndereco.DDD3 = Convert.ToInt16(intDDDC2.Valor);
            pEndereco.Fone3 = txtCelular2.Text;
            if (intCodigoClidade2.Valor != "")
                pEndereco.CodigoMunicipio = Convert.ToInt32(intCodigoClidade2.Valor);
            if (intDDDF2.Valor != "")
                pEndereco.DDDF = Convert.ToInt16(intDDDF2.Valor);
            pEndereco.Fax = txtFax2.Text;
            pEndereco.email = txtemail2.Text;
            pEndereco.Contato = txtContato2.Text.ToUpper();
            pEndereco.CargoContato = txtCargoContato.Text.ToUpper();
        }
        return pEndereco;
    }
    protected void ViewStateGetForm()
    {
        ViewState["DataCadastro"] = datDataCadastro.Data;

        ViewState["Nome"] = txtNomeFantasia.Text;
        ViewState["Descricao"] = txtCNPJ_CPF.Text;
        ViewState["TecnologiaAplicada"] = txtRG_IE.Text;

        //endereço padrão
        ViewState["txtEndereco0"] = txtEndereco0.Text;
        ViewState["intNumero0"] = intNumero0.Valor;
        ViewState["txtComplemento0"] = txtComplemento0.Text;
        ViewState["intDDD10"] = intDDD10.Valor;

        ViewState["txtFone10"] = txtFone10.Text;
        ViewState["txtBairro0"] = txtBairro0.Text;
        ViewState["intDDD20"] = intDDD20.Valor;
        ViewState["txtFone20"] = txtFone20.Text;
        ViewState["txtCEP0"] = txtCEP0.Text;
        ViewState["intDDDC0"] = intDDDC0.Valor;
        ViewState["txtCelular0"] = txtCelular0.Text;

        ViewState["intCodigoClidade0"] = intCodigoClidade0.Valor;
        ViewState["intDDDF0"] = intDDDF0.Valor;
        ViewState["txtFax0"] = txtFax0.Text;
        ViewState["txtemail0"] = txtemail0.Text;
        ViewState["txtContato0"] = txtContato0.Text;

        //endereço faturamento        
        ViewState["txtEndereco1"] = txtEndereco1.Text;
        ViewState["intNumero1"] = intNumero1.Valor;
        ViewState["txtComplemento1"] = txtComplemento1.Text;
        ViewState["intDDD11"] = intDDD11.Valor;
        ViewState["txtFone11"] = txtFone11.Text;
        ViewState["txtBairro1"] = txtBairro1.Text;

        ViewState["intDDD21"] = intDDD21.Valor;
        ViewState["txtFone21"] = txtFone21.Text;
        ViewState["txtCEP1"] = txtCEP1.Text;

        ViewState["intDDDC1"] = intDDDC1.Valor;
        ViewState["txtCelular1"] = txtCelular1.Text;


        ViewState["intCodigoClidade1"] = intCodigoClidade1.Valor;
        ViewState["intDDDF1"] = intDDDF1.Valor;
        ViewState["txtFax1"] = txtFax1.Text;
        ViewState["txtemail1"] = txtemail1.Text;
        ViewState["txtInstrucoesFaturamento"] = txtInstrucoesFaturamento.Text;
        ViewState["txtContato1"] = txtContato1.Text;

        //endereço coleta
        ViewState["txtEndereco2"] = txtEndereco2.Text;
        ViewState["intNumero2"] = intNumero2.Valor;
        ViewState["txtComplemento2"] = txtComplemento2.Text;

        ViewState["intDDD12"] = intDDD12.Valor;
        ViewState["txtFone12"] = txtFone12.Text;
        ViewState["txtBairro2"] = txtBairro2.Text;

        ViewState["intDDD22"] = intDDD22.Valor;
        ViewState["txtFone22"] = txtFone22.Text;
        ViewState["txtCEP2"] = txtCEP2.Text;

        ViewState["intDDDC2"] = intDDDC2.Valor;
        ViewState["txtCelular2"] = txtCelular2.Text;
        ViewState["intCodigoClidade2"] = intCodigoClidade2.Valor;

        ViewState["intDDDF2"] = intDDDF2.Valor;
        ViewState["txtFax2"] = txtFax2.Text;
        ViewState["txtemail2"] = txtemail2.Text;
        ViewState["txtContato2"] = txtContato2.Text;
        ViewState["CargoContato"] = txtCargoContato.Text;
    }

    protected void ViewStateSetForm()
    {
        datDataCadastro.Data = ViewState["DataCadastro"].ToString();

        txtNomeFantasia.Text = ViewState["Nome"].ToString();
        txtCNPJ_CPF.Text = ViewState["Descricao"].ToString();
        txtRG_IE.Text = ViewState["TecnologiaAplicada"].ToString();

        //endereço padrão
        txtEndereco0.Text = ViewState["txtEndereco0"].ToString();
        intNumero0.Valor = ViewState["intNumero0"].ToString();
        txtComplemento0.Text = ViewState["txtComplemento0"].ToString();
        intDDD10.Valor = ViewState["intDDD10"].ToString();

        txtFone10.Text = ViewState["txtFone10"].ToString();
        txtBairro0.Text = ViewState["txtBairro0"].ToString();
        intDDD20.Valor = ViewState["intDDD20"].ToString();
        txtFone20.Text = ViewState["txtFone20"].ToString();
        txtCEP0.Text = ViewState["txtCEP0"].ToString();
        intDDDC0.Valor = ViewState["intDDDC0"].ToString();
        txtCelular0.Text = ViewState["txtCelular0"].ToString();

        intCodigoClidade0.Valor = ViewState["intCodigoClidade0"].ToString();
        intDDDF0.Valor = ViewState["intDDDF0"].ToString();
        txtFax0.Text = ViewState["txtFax0"].ToString();
        txtemail0.Text = ViewState["txtemail0"].ToString();
        txtContato0.Text = ViewState["txtContato0"].ToString();

        //endereço faturamento        
        txtEndereco1.Text = ViewState["txtEndereco1"].ToString();
        intNumero1.Valor = ViewState["intNumero1"].ToString();
        txtComplemento1.Text = ViewState["txtComplemento1"].ToString();
        intDDD11.Valor = ViewState["intDDD11"].ToString();
        txtFone11.Text = ViewState["txtFone11"].ToString();
        txtBairro1.Text = ViewState["txtBairro1"].ToString();

        intDDD21.Valor = ViewState["intDDD21"].ToString();
        txtFone21.Text = ViewState["txtFone21"].ToString();
        txtCEP1.Text = ViewState["txtCEP1"].ToString();

        intDDDC1.Valor = ViewState["intDDDC1"].ToString();
        txtCelular1.Text = ViewState["txtCelular1"].ToString();


        intCodigoClidade1.Valor = ViewState["intCodigoClidade1"].ToString();
        intDDDF1.Valor = ViewState["intDDDF1"].ToString();
        txtFax1.Text = ViewState["txtFax1"].ToString();
        txtemail1.Text = ViewState["txtemail1"].ToString();
        txtInstrucoesFaturamento.Text = ViewState["txtInstrucoesFaturamento"].ToString();
        txtContato1.Text = ViewState["txtContato1"].ToString();

        //endereço coleta
        txtEndereco2.Text = ViewState["txtEndereco2"].ToString();
        intNumero2.Valor = ViewState["intNumero2"].ToString();
        txtComplemento2.Text = ViewState["txtComplemento2"].ToString();

        intDDD12.Valor = ViewState["intDDD12"].ToString();
        txtFone12.Text = ViewState["txtFone12"].ToString();
        txtBairro2.Text = ViewState["txtBairro2"].ToString();

        intDDD22.Valor = ViewState["intDDD22"].ToString();
        txtFone22.Text = ViewState["txtFone22"].ToString();
        txtCEP2.Text = ViewState["txtCEP2"].ToString();

        intDDDC2.Valor = ViewState["intDDDC2"].ToString();
        txtCelular2.Text = ViewState["txtCelular2"].ToString();
        intCodigoClidade2.Valor = ViewState["intCodigoClidade2"].ToString();

        intDDDF2.Valor = ViewState["intDDDF2"].ToString();
        txtFax2.Text = ViewState["txtFax2"].ToString();
        txtemail2.Text = ViewState["txtemail2"].ToString();
        txtContato2.Text = ViewState["txtContato2"].ToString();
        txtCargoContato.Text = ViewState["CargoContato"].ToString();

    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ImageButton _ibnExcluir = new ImageButton();
            _ibnExcluir.Enabled = true;
            if (oItensMenuPermissoes.Excluir == 0)
            {
                _ibnExcluir = (ImageButton)e.Row.Cells[1].FindControl("ibnExcluir");
                _ibnExcluir.Enabled = false;
            }
            if (e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[5].Text = "";

            if (e.Row.Cells[6].Text.Replace(" ", "") == "1")
                e.Row.Cells[6].Text = "Sim";
            else
                e.Row.Cells[6].Text = "Não";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        try
        {
            string _Campo = "";
        }
        finally
        {
            //MostraDadosPrimeiraLinhaGrade();
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        ViewStateGetForm();
    }
    protected void Grade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewStateSetForm();
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        // muda de aba
        lblMensagem.Text = "";
        lblMensagem0.Text = "";
        lblMensagem1.Text = "";
        lblMensagem2.Text = "";
        lblMensagem4.Text = "";
        lblMensagem5.Text = "";
        lblMensagemCopiar.Text = "";
        MultiView1.ActiveViewIndex = Convert.ToInt16(e.Item.Value);
        if (MultiView1.ActiveViewIndex == 1)
            txtEndereco0.Focus();
        else if (MultiView1.ActiveViewIndex == 2)
            txtEndereco1.Focus();
        else if (MultiView1.ActiveViewIndex == 3)
            txtEndereco2.Focus();
        else if (MultiView1.ActiveViewIndex == 4)
            intKmMedia.Focus();
        else if (MultiView1.ActiveViewIndex == 5)
            txtSenhaMaster.Focus();
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
    protected void btnOk0_Click(object sender, EventArgs e)
    {
        lblMensagem0.Text = "";
        if (lblCodigoCliente.Text != "")
            hifCodigo.Value = lblCodigoCliente.Text;

        if (btnOk0.Text == "Ok")
        {
            if (txtNome.Text.Equals(""))
            {
                lblMensagem0.Text = "Cliente inválido!";
            }

            // não ocorreu erro salvar - Salvar Endereço Padrão
            if (lblMensagem0.Text.Equals(""))
            {
                // salvar
                oEnderecoDados = new clsEnderecosDados();
                clsEnderecos oEndereco = new clsEnderecos();
                oEndereco = AtribuiDadosDoFormEndereco(oEndereco, 0);
                oEndereco.TipoCadastro = 0; // Cliente
                oEndereco.TipoEndereco = 0; // padrão
                if (hifCodigo.Value != "" && hifCodigo.Value != "0")
                {
                    lblMensagem0.Text = oEnderecoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 0);
                    if (lblMensagem0.Text == "Incluir")
                    {
                        SalvarLogEndereco("Inclusão", oEndereco);
                        oEnderecoDados.Inserir(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 0);
                        if (!intCodigoClidade0.Equals("") && !intCodigoClidade1.Equals(""))
                            SalvaDadosParaExportacao();
                        lblMensagem0.Text = "Endereço padrão incluído com sucesso!";
                        // passar para o próximo endereço que é do faturamento
                        MultiView1.ActiveViewIndex++;
                        Menu1.Items[2].Selected = true;
                    }
                }
            }
        }
        else
            lblMensagem0.Text = "Cliente não foi selecionado!";
        txtEndereco0.Focus();
    }
    protected void btnOk1_Click(object sender, EventArgs e)
    {
        lblMensagem1.Text = "";
        if (btnOk1.Text == "Ok")
        {
            if (txtNome.Text.Equals(""))
            {
                lblMensagem1.Text = "Cliente inválido!";
            }
            if (intCodigoClidade1.Valor.Equals(""))
            {
                lblMensagem1.Text = "Código do Município de Faturamento inválido!";
            }
            // não ocorreu erro salvar - Salvar Endereço Faturamento
            if (lblMensagem1.Text.Equals(""))
            {
                // salvar
                oEnderecoDados = new clsEnderecosDados();
                clsEnderecos oEndereco = new clsEnderecos();
                oEndereco = AtribuiDadosDoFormEndereco(oEndereco, 1);
                oEndereco.TipoCadastro = 0; // Cliente
                oEndereco.TipoEndereco = 1; // faturamento
                if (hifCodigo.Value != "" && hifCodigo.Value != "0")
                {
                    if (txtCNPJ_CPF_Faturamento.Text.IndexOf(".") == -1 && txtCNPJ_CPF_Faturamento.Text.Length > 12)
                    {
                        txtCNPJ_CPF_Faturamento.Text = geral.Left(txtCNPJ_CPF_Faturamento.Text, 2) + "." +
                                                       txtCNPJ_CPF_Faturamento.Text.Substring(2, 3) + "." +
                                                       txtCNPJ_CPF_Faturamento.Text.Substring(5, 3) + "/" +
                                                       txtCNPJ_CPF_Faturamento.Text.Substring(8, 4) + "-" +
                                                       txtCNPJ_CPF_Faturamento.Text.Substring(12, 2);
                    }
                    lblMensagem1.Text = oEnderecoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 1);
                    if (lblMensagem1.Text == "Incluir")
                    {
                        SalvarLogEndereco("Inclusão", oEndereco);
                        oEnderecoDados.Inserir(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 1);
                        oClientesDados.AlterarCNPJ_Faturamento(txtCNPJ_CPF_Faturamento.Text + txtAlfaCNPJ.Text, Convert.ToInt32(hifCodigo.Value));
                        lblMensagem1.Text = "Endereço faturamento incluído com sucesso!";
                        if (!intCodigoClidade0.Equals("") && !intCodigoClidade1.Equals(""))
                            SalvaDadosParaExportacao();
                        // passar para o endereço da obra
                        MultiView1.ActiveViewIndex++;
                        Menu1.Items[3].Selected = true;
                    }
                    else
                    {
                        SalvarLogEndereco("Alteração", oEndereco);
                        string msgErr = oEnderecoDados.Alterar(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 1);
                        if (msgErr.Length > 0)
                            lblMensagem1.Text = msgErr;
                        else
                        {
                            // salvar CNPJ para Faturamento
                            oClientesDados.AlterarCNPJ_Faturamento(txtCNPJ_CPF_Faturamento.Text + txtAlfaCNPJ.Text, Convert.ToInt32(hifCodigo.Value));
                            lblMensagem1.Text = "Endereço faturamento alterado com sucesso!";
                            if (!intCodigoClidade0.Equals("") && !intCodigoClidade1.Equals(""))
                                SalvaDadosParaExportacao();
                        }
                    }
                }
            }
        }
        else
            lblMensagem1.Text = "Cliente não foi selecionado!";
        txtEndereco1.Focus();
    }
    protected void btnOk2_Click(object sender, EventArgs e)
    {
        lblMensagem2.Text = "";
        if (btnOk2.Text == "Ok")
        {
            if (txtNome.Text.Equals(""))
            {
                lblMensagem2.Text = "Cliente inválido!";
            }

            // não ocorreu erro salvar - Salvar Endereço Coleta
            if (lblMensagem2.Text.Equals(""))
            {
                // salvar
                oEnderecoDados = new clsEnderecosDados();
                clsEnderecos oEndereco = new clsEnderecos();
                oEndereco = AtribuiDadosDoFormEndereco(oEndereco, 2);
                oEndereco.TipoCadastro = 0; // Cliente
                oEndereco.TipoEndereco = 2; // Coleta
                if (hifCodigo.Value != "" && hifCodigo.Value != "0")
                {
                    lblMensagem2.Text = oEnderecoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value), 0, 2);
                    if (lblMensagem2.Text == "Incluir")
                    {
                        SalvarLogEndereco("Inclusão", oEndereco);
                        oEnderecoDados.Inserir(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 2);
                        lblMensagem2.Text = "Endereço coleta incluído com sucesso!";
                        if (!intCodigoClidade0.Equals("") && !intCodigoClidade1.Equals(""))
                            SalvaDadosParaExportacao();
                        // passar para informações
                        MultiView1.ActiveViewIndex++;
                        Menu1.Items[4].Selected = true;
                    }
                    else
                    {
                        SalvarLogEndereco("Alteração", oEndereco);
                        string msgErr = oEnderecoDados.Alterar(oEndereco, Convert.ToInt32(hifCodigo.Value), 0, 2);
                        if (msgErr.Length > 0)
                            lblMensagem2.Text = msgErr;
                        else
                        {
                            lblMensagem2.Text = "Endereço coleta alterado com sucesso!";
                            if (!intCodigoClidade0.Equals("") && !intCodigoClidade1.Equals(""))
                                SalvaDadosParaExportacao();
                        }
                    }
                }
            }
        }
        else
            lblMensagem2.Text = "Cliente não foi selecionado!";
        txtEndereco2.Focus();
    }
    protected void btnDocumentacaoAplicavel_Click(object sender, EventArgs e)
    {
        // Abrir - tela documentação aplicável
        if (hifCodigo.Value != "")
        {
            clsContratos oContrato = new clsContratos();
            clsContratosDados oContratoDados = new clsContratosDados();
            oClientes = oClientesDados.PegaDados(oClientes, Convert.ToInt32(hifCodigo.Value));
            oContrato = oContratoDados.PegaDados(oContrato, 0, oClientes.Codigo);
            if (oClientes.Inativo == 1)
                lblMensagem.Text = "Cliente inativo!";
            else if (oContrato.DataRecisao != "" && oContrato.DataRecisao != "01/01/0100" && oContrato.DataRecisao != "01/01/0001" && oContrato.DataRecisao != null)
                lblMensagem.Text = "Cliente com contrato rescindido!";
            else if (oClientes.TiposDeContrato == 2)
                lblMensagem.Text = "Cliente eventual!";
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.open('../forms/DocumentacaoAplicavel.aspx?CodigoCliente=" + hifCodigo.Value + "');</script>");
        }
        else
            lblMensagem.Text = "Código do cliente inválido!";
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
    protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        if (oUsuario != null)
        {
            Session["oUsuario"] = oUsuario;
        }
    }
    private void SalvaDadosParaExportacao()
    {
        clsExportacaoRadar oExpRadar = new clsExportacaoRadar();
        clsExportacaoRadarDados oExpRadarDados = new clsExportacaoRadarDados();
        if (hifCodigo.Value != "")
            oExpRadar.CodigoNumero = Convert.ToInt32(hifCodigo.Value);
        oExpRadar.DataSolicitacao = DateTime.Now.ToShortDateString();
        oExpRadar.Tabela = "Clientes";
        if (oExpRadar.CodigoNumero > 0)
            oExpRadarDados.Inserir(oExpRadar);
    }
    private void SalvarLogEndereco(string pOperacao, clsEnderecos pEndereco)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro Clientes. Endereço: " + pEndereco.TipoEndereco + " \n";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código cliente: " + hifCodigo.Value + ". Endereço: " + pEndereco.TipoEndereco + " \n";
        oLog.Log = oLog.Log + "Nome: " + txtNome.Text + " \n";
        oLog.Log = oLog.Log + "Nome fantasia: " + txtNomeFantasia.Text + " \n";

        oLog.Log = oLog.Log + "endereco: " + pEndereco.endereco + " Nº: " + pEndereco.Numero + " \n";
        oLog.Log = oLog.Log + "Complemento: " + pEndereco.Complemento + " \n";
        oLog.Log = oLog.Log + "Bairro: " + pEndereco.Bairro + " \n";
        oLog.Log = oLog.Log + "CEP: " + pEndereco.CEP + " \n";
        oLog.Log = oLog.Log + "Código Município: " + pEndereco.CodigoMunicipio + " \n";
        oLog.Log = oLog.Log + "Contato: " + pEndereco.Contato + " \n";
        oLog.Log = oLog.Log + "DDD: " + pEndereco.DDD1 + " Fone: " + pEndereco.Fone1 + " \n";
        oLog.Log = oLog.Log + "DDD: " + pEndereco.DDD2 + " Fone: " + pEndereco.Fone2 + " \n";
        oLog.Log = oLog.Log + "DDD: " + pEndereco.DDD3 + " Fone: " + pEndereco.Fone3 + " \n";
        oLog.Log = oLog.Log + "DDD: " + pEndereco.DDDF + " Fone: " + pEndereco.Fax + " \n";
        oLog.Log = oLog.Log + "Contato: " + pEndereco.Contato + " \n";
        oLog.Log = oLog.Log + "email: " + pEndereco.email + " \n";
        if (pEndereco.TipoEndereco == 2)
            oLog.Log = oLog.Log + "InstrucoesFat: " + pEndereco.InstrucoesFat + " \n";
        oLogDados.Inserir(oLog);
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

    protected void Menu1_MenuItemClick1(object sender, MenuEventArgs e)
    {
        Menu1.Items[MultiView1.ActiveViewIndex].Selected = true;

        //MessageBox(this.Page, "Ação inválida!");
    }
}