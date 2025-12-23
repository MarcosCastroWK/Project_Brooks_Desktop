using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class Account_Entrar : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsLicencaAmbientalDados oLAODados = new clsLicencaAmbientalDados();
    clsLicencaAmbiental oLAO = new clsLicencaAmbiental();
    DataTable _dt = new DataTable();
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "9");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
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
                try
                {
                    if (oUsuario.Aplicativo == true)
                        menucabec1.Visible = false;
                    else
                        menucabec1.Visible = true;
                    Grade.DataSource = oDestinoFinalDados.PegaDados(oDestinoFinal, 0, false);
                    Grade.DataBind();
                    txtBairro.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("Bairro");
                    txtCelular.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("Celular");
                    txtCidade.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("Cidade");
                    txtCNPJ.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("CNPJ");
                    txtEmail.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("eMail");
                    txtEndereco.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("LAO");
                    txtLocalAterro.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("LocalAterro");
                    txtNome.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("Nome");
                    txtNomeArqAss.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("NomeArqAss");
                    txtNomeArqLogo.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("NomeArqLogo");
                    txtNomeFantasia.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("NomeFantasia");
                    txtTelefone.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("Fone");
                    txtUF.MaxLength = oDestinoFinalDados.PegaTamanhoCampoVarChar("UF");

                    txtObs.MaxLength = oLAODados.PegaTamanhoCampoVarChar("Obs");
                    txtCodigoAtividade.MaxLength = oLAODados.PegaTamanhoCampoVarChar("CodigoAtividade");
                    txtNumeroLicenca.MaxLength = oLAODados.PegaTamanhoCampoVarChar("NumeroLicenca");
                    PermissaoIncluir();

                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                    hifCodigo.Value = "";
                }
            }
        }
        txtNome.Focus();
    }
    private void PermissaoIncluir()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Incluir == 0)
            Salvar.Enabled = false;
    }
    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
	private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("dd/MM/yyyy");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro Destino Final";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código Destino: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data do cadastro: " + datDataCadastro.Data + " \n";
		if (chkAtivo.Checked)
			oLog.Log = oLog.Log + "Ativo: Sim \n";
		else
			oLog.Log = oLog.Log + "Ativo: Não \n";			
		oLog.Log = oLog.Log + "Nome/Razão Social: " + txtNome.Text + " \n";
		oLog.Log = oLog.Log + "Nome Fantasia: " + txtNomeFantasia.Text + " \n";
		oLog.Log = oLog.Log + "Endereço: " + txtEndereco.Text + " \n";	
        oLog.Log = oLog.Log + "Bairro: " + txtBairro.Text + " \n";		
		oLog.Log = oLog.Log + "Cidade: " + txtCidade.Text + " \n";	
		oLog.Log = oLog.Log + "UF: " + txtUF.Text + " \n";			
		oLog.Log = oLog.Log + "CEP: " + intCEP.Valor + " \n";
		oLog.Log = oLog.Log + "CNPJ: " + txtCNPJ.Text + " \n";
		oLog.Log = oLog.Log + "Nome do arquivo de logomarca com .jpg/.bmp/.png: " + txtNomeArqLogo.Text + " \n";
		oLog.Log = oLog.Log + "Telefone: " + txtTelefone.Text + " \n";
		oLog.Log = oLog.Log + "Celular: " + txtCelular.Text + " \n";
		oLog.Log = oLog.Log + "Nome do arquivo de assinatura com .jpg/.bmp/.png: " + txtNomeArqAss.Text + " \n";
		oLog.Log = oLog.Log + "e-mail: " + txtEmail.Text + " \n";
		oLog.Log = oLog.Log + "Local Aterro: " + txtLocalAterro.Text + " \n";
		if (chkEnviarEmailCDF.Checked)
			oLog.Log = oLog.Log + "Enviar e-mail de CDF para Fornecedor: Sim \n";
		else
			oLog.Log = oLog.Log + "Enviar e-mail de CDF para Fornecedor: Não \n";		
		if (chkEnviarMovResiduos.Checked)
			oLog.Log = oLog.Log + "Enviar relatório de movimentação de resíduos por e-mail: Sim \n";
		else
			oLog.Log = oLog.Log + "Enviar relatório de movimentação de resíduos por e-mail: Não \n";
		if (chkEmiteCDF.Checked)
			oLog.Log = oLog.Log + "Fornecedor Emite CDF (Brooks não gera CDF): Não \n";
		else
			oLog.Log = oLog.Log + "Fornecedor Emite CDF (Brooks não gera CDF): Sim \n";        
        oLogDados.Inserir(oLog);

    }
    private void SalvarLogLAO(string pOperacao, clsLicencaAmbiental pLAO)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("dd/MM/yyyy");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "DTR";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código: " + oLAO.Codigo + " \n";
        oLog.Log = oLog.Log + "Arquivo: " + pLAO.Arquivo + " \n";
        oLog.Log = oLog.Log + "Código Destino: " + pLAO.CodigoAterro + " \n";
        oLog.Log = oLog.Log + "Código Atividade: " + pLAO.CodigoAtividade + " \n";
        oLog.Log = oLog.Log + "Nome Destino: " + pLAO.NomeDestinoFinal + " \n";
        oLog.Log = oLog.Log + "Número Liçenca: " + pLAO.NumeroLicenca + " \n";
        oLog.Log = oLog.Log + "Obs: " + pLAO.Obs + " \n";
        oLog.Log = oLog.Log + "Prazo de Validade: " + pLAO.PrazoValidade + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "Destino Final inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oDestinoFinal = AtribuiDadosDoForm(oDestinoFinal);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oDestinoFinalDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
					SalvarLog("Inclusão");
                    oDestinoFinalDados.Inserir(oDestinoFinal);
                    Grade.DataSource = oDestinoFinalDados.PegaDados(oDestinoFinal, 0, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Destino Final incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = oDestinoFinalDados.Alterar(oDestinoFinal, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
						SalvarLog("Alteração");
                        lblMensagem.Text = "Destino Final alterado com sucesso!";
                        Grade.DataSource = oDestinoFinalDados.PegaDados(oDestinoFinal, 0, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Destino Final";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "DestinoFinal inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                oDestinoFinalDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "DestinoFinal excluído com sucesso!";
				SalvarLog("Exclusão");
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Destino Final";
                Grade.DataSource = oDestinoFinalDados.PegaDados(oDestinoFinal, 0, false);
                Grade.DataBind();
            }
        }
        LimpaCampos();
    }

    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Destino Final";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Destino Final";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LimpaCampos();
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome" && e.CommandArgument.ToString() != "NomeFantasia" && e.CommandArgument.ToString() != "Cidade"
            && e.CommandArgument.ToString() != "UF" && e.CommandArgument.ToString() != "DataCadastro" && e.CommandArgument.ToString() != "Ativo")
        {
            txtNome.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oDestinoFinal);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();

            oLAODados = new clsLicencaAmbientalDados();
            GradeLAO.DataSource = oLAODados.PegaDados(Convert.ToInt32(hifCodigo.Value));
            GradeLAO.DataBind();
        }
    }

    protected void AtribuiDaClasseLAO(clsLicencaAmbiental pLAO)
    {
        
        txtNumeroLicenca.Text = pLAO.NumeroLicenca;
        if (pLAO.PrazoValidade == "01/01/0001" || pLAO.PrazoValidade == "01/01/0100" || pLAO.PrazoValidade == null)
            datPrazoValidade.Data = "";
        else
            datPrazoValidade.Data = Convert.ToDateTime(pLAO.PrazoValidade).ToString("dd/MM/yyyy");
        txtCodigoAtividade.Text = pLAO.CodigoAtividade;
        intCodigoDestinoFinal.Valor = pLAO.CodigoAterro.ToString();
        txtNomeDestinoFinal.Text = pLAO.NomeDestinoFinal;
        txtObs.Text = pLAO.Obs;
    }

    protected void AtribuiDadosDaClasse(clsDestinoFinal pDestinoFinal)
    {
        if (pDestinoFinal.DataCadastro == "01/01/0001" || pDestinoFinal.DataCadastro == "01/01/0100" || pDestinoFinal.DataCadastro == null)
            datDataCadastro.Data = "";
        else
            datDataCadastro.Data = Convert.ToDateTime(pDestinoFinal.DataCadastro).ToString("dd/MM/yyyy");
        txtEmail.Text = pDestinoFinal.eMail;
        txtCidade.Text = pDestinoFinal.Cidade;
        txtCelular.Text = pDestinoFinal.Celular;
        txtUF.Text = pDestinoFinal.UF;
        intCEP.Valor = pDestinoFinal.CEP;
        txtNomeFantasia.Text = pDestinoFinal.NomeFantasia;
        txtCNPJ.Text = pDestinoFinal.CNPJ;
        txtNome.Text = pDestinoFinal.Nome;
        txtTelefone.Text = pDestinoFinal.Fone;
        txtLocalAterro.Text = pDestinoFinal.LocalAterro;
        txtEndereco.Text = pDestinoFinal.Endereco;
        txtBairro.Text = pDestinoFinal.Bairro;
        txtCidade.Text = pDestinoFinal.Cidade;
        txtNomeArqLogo.Text = pDestinoFinal.NomeArqLogo;
        if (pDestinoFinal.EmiteCDF == 1)
            chkEmiteCDF.Checked = true;
        else if (pDestinoFinal.EmiteCDF == 0)
            chkEmiteCDF.Checked = false;
        if (pDestinoFinal.Ativo == 1)
            chkAtivo.Checked = true;
        else if (pDestinoFinal.Ativo == 0)
            chkAtivo.Checked = false;
        if (pDestinoFinal.Enviar_emailCDF == 1)
            chkEnviarEmailCDF.Checked = true;
        else if (pDestinoFinal.Enviar_emailCDF == 0)
            chkEnviarEmailCDF.Checked = false;
        if (pDestinoFinal.Enviar_emailMovResiduos == 1)
            chkEnviarMovResiduos.Checked = true;
        else if (pDestinoFinal.Enviar_emailMovResiduos == 0)
            chkEnviarMovResiduos.Checked = false;
        intCodUnidadeIMA.Valor = pDestinoFinal.CodigoUnidadeDoIMA.ToString();
    }

    protected void LimpaCampos()
    {
        datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtEmail.Text = "";
        txtCidade.Text = "";
        txtCelular.Text = "";
        txtUF.Text = "";
        intCEP.Valor = "";
        txtNomeFantasia.Text = "";
        txtCNPJ.Text = "";
        txtNome.Text = "";
        txtTelefone.Text = "";
        txtLocalAterro.Text = "";
        txtEndereco.Text = "";
        txtBairro.Text = "";
        txtCidade.Text = "";
        txtNomeArqLogo.Text = "";
        chkEmiteCDF.Checked = false;
        chkAtivo.Checked = true;
        chkEnviarEmailCDF.Checked = true;
        chkEnviarMovResiduos.Checked = true;
        intCodUnidadeIMA.Valor = "";

        txtNumeroLicenca.Text = "";
        datPrazoValidade.Data = "";
        txtCodigoAtividade.Text = "";
        intCodigoDestinoFinal.Valor = "";
        txtNomeDestinoFinal.Text = "";
        txtObs.Text = "";

        hifCodigo.Value = "";
        hifCodigoLAO.Value = "";

        PermissaoIncluir();

    }

    protected clsLicencaAmbiental AtribuiLAOdoForm(clsLicencaAmbiental pLAO)
    {
        pLAO.NumeroLicenca = txtNumeroLicenca.Text;
        pLAO.PrazoValidade = datPrazoValidade.Data;
        pLAO.CodigoAtividade = txtCodigoAtividade.Text;
        if (intCodigoDestinoFinal.Valor != "")
            pLAO.CodigoAterro = Convert.ToInt32(intCodigoDestinoFinal.Valor);
        pLAO.NomeDestinoFinal = txtNomeDestinoFinal.Text;
        pLAO.Obs = txtObs.Text;
        pLAO.Arquivo = UploadDocumento.FileName;
        return pLAO;
    }
    
    protected clsDestinoFinal AtribuiDadosDoForm(clsDestinoFinal pDestinoFinal)
    {
        if (datDataCadastro.Data != "")
            pDestinoFinal.DataCadastro = datDataCadastro.Data;
        pDestinoFinal.eMail = txtEmail.Text;
        pDestinoFinal.Cidade = txtCidade.Text;
        pDestinoFinal.Celular = txtCelular.Text;
        pDestinoFinal.UF = txtUF.Text;
        pDestinoFinal.CEP = intCEP.Valor;
        pDestinoFinal.NomeFantasia = txtNomeFantasia.Text;
        pDestinoFinal.CNPJ = txtCNPJ.Text;
        pDestinoFinal.Nome = txtNome.Text;
        pDestinoFinal.Fone = txtTelefone.Text;
        pDestinoFinal.LocalAterro = txtLocalAterro.Text;
        pDestinoFinal.Endereco = txtEndereco.Text;
        pDestinoFinal.Bairro = txtBairro.Text;
        pDestinoFinal.Cidade = txtCidade.Text;
        pDestinoFinal.NomeArqLogo = txtNomeArqLogo.Text;

        if (chkEmiteCDF.Checked)
            pDestinoFinal.EmiteCDF = 1;
        else
            pDestinoFinal.EmiteCDF = 0;

        if (chkAtivo.Checked)
            pDestinoFinal.Ativo = 1;
        else
            pDestinoFinal.Ativo = 0;

        if (chkEnviarEmailCDF.Checked)
            pDestinoFinal.Enviar_emailCDF = 1;
        else
            pDestinoFinal.Enviar_emailCDF = 0;

        if (chkEnviarMovResiduos.Checked)
            pDestinoFinal.Enviar_emailMovResiduos = 1;
        else
            pDestinoFinal.Enviar_emailMovResiduos = 0;
        if (intCodUnidadeIMA.Valor != "")
            pDestinoFinal.CodigoUnidadeDoIMA = Convert.ToInt32(intCodUnidadeIMA.Valor);

        return pDestinoFinal;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Destino Final";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        chkEmiteCDF.Checked = true;
        chkAtivo.Checked = true;
        Salvar.Text = "Ok";
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
            else if (e.Row.Cells[6].Text.Replace(" ", "") == "0" || e.Row.Cells[6].Text.Replace(" ", "") == "" || e.Row.Cells[6].Text.Replace(" ", "") == "&nbsp;")
                e.Row.Cells[6].Text = "Não";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)        
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
            
        Grade.DataSource = oDestinoFinalDados.PreencheDataTableAterro(geral.Ordem);
        Grade.DataBind();
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Convert.ToInt16(e.Item.Value);
        if (MultiView1.ActiveViewIndex == 0)
            txtNome.Focus();
        else if (MultiView1.ActiveViewIndex == 1)
        {
            intCodigoDestinoFinal.Focus();
        }
    }
    protected void GradeLAO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "CodigoAterro" && e.CommandArgument.ToString() != "CodigoAtividade" && 
            e.CommandArgument.ToString() != "NomeDestinoFinal" && e.CommandArgument.ToString() != "NumeroLicenca" && e.CommandArgument.ToString() != "Obs" &&
            e.CommandArgument.ToString() != "Arquivo" && e.CommandArgument.ToString() != "PrazoValidade")
        {
            hifCodigoLAO.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oLAO = oLAODados.PegaDados(oLAO, Convert.ToInt32(hifCodigoLAO.Value));
            AtribuiDaClasseLAO(oLAO);
            oLAODados = new clsLicencaAmbientalDados();

            if (hifCodigo.Value == "")
                lblMensagemLAO.Text = "Selecione um Destino Final!";
            else
                GradeLAO.DataSource = oLAODados.PegaDados(Convert.ToInt32(hifCodigo.Value));
            GradeLAO.DataBind();
        }
    }
    protected void btnOk0_Click(object sender, EventArgs e)
    {
        lblMensagemLAO.Text = "";
        if (btnOk0.Text == "Ok")
        {
            if (intCodigoDestinoFinal.Valor == "" || intCodigoDestinoFinal.Valor == "0")
            {
                lblMensagemLAO.Text = "Destino final inválido!";
            }
            if (txtNome.Text.Equals(""))
            {
                lblMensagemLAO.Text = "Destino final inválido!";
            }
            // não ocorreu erro salvar - Salvar Endereço Padrão
            if (lblMensagemLAO.Text.Equals(""))
            {
                // salvar
                oLAODados = new clsLicencaAmbientalDados();
                oLAO = new clsLicencaAmbiental();
                oLAO = AtribuiLAOdoForm(oLAO);
                oLAO.Codigo = 0;
                oLAO.CodigoAterro = 0;
                if (hifCodigoLAO.Value != "" && hifCodigoLAO.Value != "0")
                    oLAO.Codigo = Convert.ToInt32(hifCodigoLAO.Value);
                if (hifCodigo.Value != "" && hifCodigo.Value != "0") // Código do aterro não pode ser zero
                {
                    try
                    {
                        oLAO.CodigoAterro = Convert.ToInt32(hifCodigo.Value);
                        lblMensagemLAO.Text = oLAODados.DadoExiste(oLAO.Codigo);
                        if (lblMensagemLAO.Text == "Incluir")
                        {
                            SalvarLogLAO("Inclusão", oLAO);
                            if (oLAODados.Inserir(oLAO))
                                lblMensagemLAO.Text = "Licença Ambiental incluída com sucesso!";
                            else
                                lblMensagemLAO.Text = "Erro ao inserir Licença Ambiental!";
                        }
                        else if (lblMensagemLAO.Text == "Alterar")
                        {
                            SalvarLogLAO("Alteração", oLAO);
                            string msgErr = oLAODados.Alterar(oLAO, oLAO.Codigo);
                            if (msgErr.Length > 0)
                                lblMensagemLAO.Text = msgErr;
                            else
                            {
                                lblMensagemLAO.Text = "Licença Ambiental alterada com sucesso!";
                            }
                        }
                        if (UploadDocumento.FileName != "")
                        {
                            string savePath = Server.MapPath("").Replace("\\forms", "") + @"\dados\" + UploadDocumento.FileName;
                            UploadDocumento.SaveAs(savePath);
                            lblMensagemLAO.Text = lblMensagemLAO.Text + "   Upload realizado com sucesso!";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagemLAO.Text = lblMensagemLAO.Text + " - " + ex.Message;
                    }
                }
                GradeLAO.DataSource = oLAODados.PegaDados(oLAO.CodigoAterro);
                GradeLAO.DataBind();
                hifCodigo.Value = "";
                hifCodigoLAO.Value = "";
            }
        }
        if (btnOk0.Text == "Confirma")
        {
            if (hifCodigo.Value != "")
                oLAO.Codigo = Convert.ToInt32(hifCodigo.Value);
            oLAO.CodigoAtividade = txtCodigoAtividade.Text;
            oLAO.NumeroLicenca = txtNumeroLicenca.Text;
            SalvarLogLAO("Exclusão", oLAO);
            lblMensagemLAO.Text = oLAODados.Excluir(oLAO.Codigo, txtCodigoAtividade.Text, txtNumeroLicenca.Text);
            if (lblMensagemLAO.Text == "")
                lblMensagemLAO.Text = "LAO Excluída com sucesso!";
            oLAODados = new clsLicencaAmbientalDados();
            GradeLAO.DataSource = oLAODados.PegaDados(Convert.ToInt32(hifCodigo.Value));
            GradeLAO.DataBind();
        }
        intCodigoDestinoFinal.Focus();
    }
    protected void GradeLAO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0100")
            e.Row.Cells[6].Text = "";
        if (e.Row.Cells[8].Text.Replace(" ", "") != "")
            e.Row.Cells[8].Text = "<a target='_blank' href='..\\dados\\" + e.Row.Cells[8].Text + "'>" + e.Row.Cells[8].Text + "</a>";

    }
    protected void ibnExcluirLao_Click(object sender, ImageClickEventArgs e)
    {
        btnOk0.Text = "Confirma";
    }
    protected void GradeLAO_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        if (hifCodigo.Value != "")
        {
            oLAODados = new clsLicencaAmbientalDados();
            GradeLAO.DataSource = oLAODados.PreencheDataTableLicencaAmbiental(geral.Ordem, Convert.ToInt32(hifCodigo.Value));
            GradeLAO.DataBind();
        }
    }

    protected void Grade_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}