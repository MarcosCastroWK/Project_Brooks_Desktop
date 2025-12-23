using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class webResiduos : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsResiduos oResiduos = new clsResiduos();
    clsResiduoDados oResiduosDados = new clsResiduoDados();
    clsTipoAcondicionamentoDados oTipoAcondicionamentoDados = new clsTipoAcondicionamentoDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["oUsuario"] == null)
        {
            menu _menu = (menu)FindControl("menu1");
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "13");
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
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    ddlFiltro.SelectedIndex = 1;

                    Grade.DataSource = oResiduosDados.PreencheDataTableResiduos("Grupo asc", "Ativos");
                    Grade.DataBind();

                    txtDescricaoReduzida.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("DescricaoReduzida");
                    txtTecnologiaAplicada.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("TecnologiaAplicada");
                    txtUnidade.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("Unidade");
                    txtCodigoResiduoManifesto.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("CodigoResiduoManifesto");
                    txtDescricao.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("Descricao");
                    txtEstadoFisico.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("EstadoFisico");
                    txtClasse.MaxLength = oResiduosDados.PegaTamanhoCampoVarChar("Classe");
                    PermissaoIncluir();
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
        if (Session["IBAMA"] != null)
        {
            clsIBAMA oIbama = (clsIBAMA)Session["IBAMA"];
            intCodigoIBAMA.Valor = oIbama.CodigoIBAMA;
            hidCodigoIBAMA.Value = oIbama.Codigo.ToString();
        }
        txtDescricaoReduzida.Focus();
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
    protected void Excluir_Click(object sender, EventArgs e)
    {     
        lblMensagem.Text = oResiduosDados.DadoExiste(oResiduos.Codigo);
        if (txtDescricaoReduzida.Text.Equals(""))
        {
            lblMensagem.Text = "Residuo inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oResiduosDados.Excluir(oResiduos.Codigo, "");
            lblMensagem.Text = "Residuo excluído com sucesso!";
            Grade.DataSource = oResiduosDados.PegaDados(oResiduos, 0, false, false);
            Grade.DataBind();
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro de Resíduos";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código resíduo: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data Cadastro: " + datDataCadastro.Data + " \n";
        if (chkAtivo.Checked)
            oLog.Log = oLog.Log + "Ativo: Sim \n";
        else if (chkAtivo.Checked)
            oLog.Log = oLog.Log + "Ativo: Não \n";
        oLog.Log = oLog.Log + "Descrição: " + txtDescricaoReduzida.Text + " \n";
        oLog.Log = oLog.Log + "Código Manifesto: " + txtCodigoResiduoManifesto.Text + " \n";
        oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
        oLog.Log = oLog.Log + "Tecnologia Aplicada: " + txtTecnologiaAplicada.Text + " \n";
        oLog.Log = oLog.Log + "Unidade: " + txtUnidade.Text + " \n";
        oLog.Log = oLog.Log + "Estado físico: " + txtEstadoFisico.Text + " \n";
        oLog.Log = oLog.Log + "Classe: " + txtClasse.Text + " \n";
        oLog.Log = oLog.Log + "Codigo IBAMA: " + intCodigoIBAMA.Valor + " \n";
        oLog.Log = oLog.Log + "Codigo Grupo: " + GRUPORESIDUO1.Valor + " \n";
        oLog.Log = oLog.Log + "Descrição Grupo: " + GRUPORESIDUO1.Texto + " \n";
        oLog.Log = oLog.Log + "Código Destino Final: " + DESTINOFINAL1.Valor + " \n";
        oLog.Log = oLog.Log + "Descriçao Destino Final: " + DESTINOFINAL1.Texto + " \n";
        oLog.Log = oLog.Log + "M3PorTon: " + moeM3PorTon.Valor + " \n";
        if (chkEhReciclavel.Checked)
            oLog.Log = oLog.Log + "É reciclável: Sim \n";
        else
            oLog.Log = oLog.Log + "É reciclável: Não \n";
        if (chkEhServico.Checked)
            oLog.Log = oLog.Log + "É serviço: Sim \n";
        else
            oLog.Log = oLog.Log + "É serviço: Não \n";
        oLogDados.Inserir(oLog);
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (txtDescricaoReduzida.Text.Equals(""))
            {
                lblMensagem.Text = "Residuo inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oResiduos = AtribuiDadosDoForm(oResiduos);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                {
                    hifCodigo.Value = "-1";
                    lblCodigoDescricao.Text = "";
                }
                lblMensagem.Text = oResiduosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclusão");
                    oResiduosDados.Inserir(oResiduos);
                    Grade.DataSource = oResiduosDados.PegaDados(oResiduos, 0, false, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Residuo incluído com sucesso!";
                    LimpaCampos();
                }
                else if (lblMensagem.Text == "Alterar") 
                {
                    SalvarLog("Alteração");
                    string msgErr = oResiduosDados.Alterar(oResiduos, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Residuo alterada com sucesso!";
                        Grade.DataSource = oResiduosDados.PegaDados(oResiduos, 0, false, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Residuos";
                
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtDescricaoReduzida.Text.Equals(""))
            {
                lblMensagem.Text = "Residuo inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oResiduosDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "Residuo excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Residuos";
                Grade.DataSource = oResiduosDados.PegaDados(oResiduos, 0, false, false);
                Grade.DataBind();
                LimpaCampos();
            }
        }
    }
    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Residuo";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Residuo";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Descricao" && e.CommandArgument.ToString() != "DescricaoReduzida" &&
            e.CommandArgument.ToString() != "Ativo" && e.CommandArgument.ToString() != "Classe" && e.CommandArgument.ToString() != "Grupo" &&
            e.CommandArgument.ToString() != "CodigoIBAMA" && e.CommandArgument.ToString() != "DestinoFinal" && e.CommandArgument.ToString() != "TecnologiaAplicada" &&
            e.CommandArgument.ToString() != "CodigoIBAMA_Analitico" && e.CommandArgument.ToString() != "DataCadastro")
        {
            txtDescricaoReduzida.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oResiduos = oResiduosDados.PegaDados(oResiduos, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oResiduos);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsResiduos pResiduo)
    {
        lblCodigoDescricao.Text = hifCodigo.Value + " " + pResiduo.DescricaoReduzida; 
        if (pResiduo.DataCadastro == "01/01/0001" || pResiduo.DataCadastro == "01/01/0100" || oResiduos.DataCadastro == null || oResiduos.DataCadastro == "")
            datDataCadastro.Data = "";
        else
            datDataCadastro.Data = Convert.ToDateTime(pResiduo.DataCadastro).ToString("dd/MM/yyyy");

        if (pResiduo.Ativo == 1)
            chkAtivo.Checked = true;
        else if (pResiduo.Ativo == 0)
            chkAtivo.Checked = false;
        txtDescricaoReduzida.Text = pResiduo.DescricaoReduzida;
        txtCodigoResiduoManifesto.Text = pResiduo.CodigoResiduoManifesto;
        txtDescricao.Text = pResiduo.Descricao;
        txtTecnologiaAplicada.Text = pResiduo.TecnologiaAplicada;
        txtUnidade.Text = pResiduo.Unidade;
        txtEstadoFisico.Text = pResiduo.EstadoFisico;
        txtClasse.Text = pResiduo.Classe;
        intCodigoIBAMA.Valor = pResiduo.oIbama.CodigoIBAMA;
        hidCodigoIBAMA.Value = pResiduo.CodigoIBAMA.ToString();
        
        clsIBAMA oIbama = new clsIBAMA();
        clsIBAMADados oIbamaDados = new clsIBAMADados();
        oIbamaDados.PegaDados(oIbama, oResiduos.CodigoIBAMA);
        lblDescricaoIBAMA.Text = "";
        if (oResiduos.CodigoIBAMA > 0)
            lblDescricaoIBAMA.Text = oIbama.Codigo.ToString() + " " + oIbama.Descricao;
        GRUPORESIDUO1.Valor = pResiduo.CodigoGrupoResiduo.ToString();
        GRUPORESIDUO1.Texto = pResiduo.DescricaoGrupo;
        DESTINOFINAL1.Valor = pResiduo.CodigoDestinoFinal.ToString();
        DESTINOFINAL1.Texto = pResiduo.DescricaoDestinoFinal;
        moeM3PorTon.Valor = pResiduo.M3PorTon.ToString();
        chkEhReciclavel.Checked = false;
        if (pResiduo.EhReciclavel == 1)
            chkEhReciclavel.Checked = true;
        chkEhServico.Checked = false;
        if (pResiduo.EhServico == 1)
            chkEhServico.Checked = true;
    }

    protected void LimpaCampos()
    {
        datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
        chkAtivo.Checked = true;
        txtDescricaoReduzida.Text = "";
        txtCodigoResiduoManifesto.Text = "";
        txtDescricao.Text = "";
        txtTecnologiaAplicada.Text = "";
        txtUnidade.Text = "";
        txtEstadoFisico.Text = "";
        txtClasse.Text = "";
        intCodigoIBAMA.Valor = "";
        hidCodigoIBAMA.Value = "";
        GRUPORESIDUO1.Valor = "";
        GRUPORESIDUO1.Texto = "";
        DESTINOFINAL1.Valor = "";
        DESTINOFINAL1.Texto = "";
        moeM3PorTon.Valor = "";
        Session["GrupoResiduos"] = null;
        Session["DestinoFinal"] = null;
        PermissaoIncluir();
        chkEhReciclavel.Checked = false;
        chkEhServico.Checked = false;
    }
    protected clsResiduos AtribuiDadosDoForm(clsResiduos pResiduo)
    {
        if (datDataCadastro.Data != "")
            pResiduo.DataCadastro = datDataCadastro.Data;
        if (chkAtivo.Checked)
            pResiduo.Ativo = 1;
        else
            pResiduo.Ativo = 0;
        pResiduo.DescricaoReduzida = txtDescricaoReduzida.Text;
        pResiduo.CodigoResiduoManifesto = txtCodigoResiduoManifesto.Text;
        pResiduo.Descricao = txtDescricao.Text;
        pResiduo.TecnologiaAplicada = txtTecnologiaAplicada.Text;
        pResiduo.Unidade = txtUnidade.Text;
        pResiduo.EstadoFisico = txtEstadoFisico.Text;
        pResiduo.Classe = txtClasse.Text;
        pResiduo.oIbama.CodigoIBAMA = intCodigoIBAMA.Valor;
        if (hidCodigoIBAMA.Value != "")
            pResiduo.CodigoIBAMA = Convert.ToInt32(hidCodigoIBAMA.Value);
        pResiduo.CodigoGrupoResiduo = Convert.ToInt32(GRUPORESIDUO1.Valor);
        pResiduo.CodigoDestinoFinal = Convert.ToInt32(DESTINOFINAL1.Valor);
        if (moeM3PorTon.Valor != "")
            pResiduo.M3PorTon = Convert.ToDecimal(moeM3PorTon.Valor);
        if (chkEhReciclavel.Checked)
            pResiduo.EhReciclavel = 1;
        if (chkEhServico.Checked)
            pResiduo.EhServico = 1;
        return pResiduo;
    }
    protected void ViewStateGetForm()
    {
        ViewState["DataCadastro"] = datDataCadastro.Data;
        if (chkAtivo.Checked) ViewState["Ativo"] = 1; else ViewState["Ativo"] = 0;
        ViewState["DescricaoReduzida"] = txtDescricaoReduzida.Text;
        ViewState["CodigoResiduoManifesto"] = txtCodigoResiduoManifesto.Text;
        ViewState["Descricao"] = txtDescricao.Text;
        ViewState["TecnologiaAplicada"] = txtTecnologiaAplicada.Text;
        ViewState["Unidade"] = txtUnidade.Text;
        ViewState["EstadoFisico"] = txtEstadoFisico.Text;
        ViewState["Classe"] = txtClasse.Text;
        ViewState["objCodigoIBAMA"] = intCodigoIBAMA.Valor;
        ViewState["CodigoIBAMA"] = hidCodigoIBAMA.Value;
        ViewState["CodigoGrupoResiduo"] = GRUPORESIDUO1.Valor;
        ViewState["CodigoDestinoFinal"] = DESTINOFINAL1.Valor;
        ViewState["M3PorTon"] = moeM3PorTon.Valor;
        ViewState["EhResiduo"] = chkEhReciclavel.Checked;
        ViewState["EhServico"] = chkEhServico.Checked;
    }
    protected void ViewStateSetForm()
    {
        datDataCadastro.Data = ViewState["DataCadastro"].ToString();
        if (Convert.ToInt16(ViewState["Ativo"]) == 1) chkAtivo.Checked = true; else chkAtivo.Checked = false;
        txtDescricaoReduzida.Text = ViewState["DescricaoReduzida"].ToString();
        txtCodigoResiduoManifesto.Text = ViewState["CodigoResiduoManifesto"].ToString();
        txtDescricao.Text = ViewState["Descricao"].ToString();
        txtTecnologiaAplicada.Text = ViewState["TecnologiaAplicada"].ToString();
        txtUnidade.Text = ViewState["Unidade"].ToString();
        txtEstadoFisico.Text = ViewState["EstadoFisico"].ToString();
        txtClasse.Text = ViewState["Classe"].ToString();
        intCodigoIBAMA.Valor = ViewState["objCodigoIBAMA"].ToString();
        hidCodigoIBAMA.Value = ViewState["CodigoIBAMA"].ToString();
        //ViewState["CodigoGrupoResiduo"] = GRUPORESIDUO1.Valor;
        //ViewState["CodigoDestinoFinal"] = DESTINOFINAL1.Valor;
        moeM3PorTon.Valor = ViewState["M3PorTon"].ToString();
        chkEhReciclavel.Checked = (bool)ViewState["EhResiduo"];
        chkEhServico.Checked= (bool)ViewState["EhServico"];

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Residuos";
        LimpaCampos();
        hifCodigo.Value = "";
        lblCodigoDescricao.Text = "";
        hidCodigoIBAMA.Value = "";
        lblMensagem.Text = "";
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

            if (e.Row.Cells[7].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[7].Text = "";
            if (e.Row.Cells[8].Text.Replace(" ", "") == "1")
                e.Row.Cells[8].Text = "Sim";
            else
                e.Row.Cells[8].Text = "Não";
        }
        
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oResiduosDados.PreencheDataTableResiduos(geral.Ordem, ddlFiltro.Text);
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string _Campo = "";
        if (ddlFiltro.Text == "Descrição Reduzida")
            _Campo = "DescricaoReduzida";
        else if (ddlFiltro.Text == "Ativos")
            _Campo = "DescricaoReduzida";
        else
            _Campo = ddlFiltro.Text;

        Grade.DataSource = oResiduosDados.PreencheDataTableResiduos(_Campo, txtFiltro.Text, _Campo);
        Grade.DataBind();
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        ViewStateGetForm();
    }
}