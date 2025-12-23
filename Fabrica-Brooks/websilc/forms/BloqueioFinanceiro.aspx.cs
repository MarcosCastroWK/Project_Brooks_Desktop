using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_BloqueioFinanceiro : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsBloqueioFinanceiro oBloqueioFinanceiro = new clsBloqueioFinanceiro();
    clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "3");

        if (!IsPostBack)
        {
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                Response.Redirect("sempermissao.aspx");

            datDataBloqueio.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                    intCodigoUsuario.Valor = ((clsUsuarios)Session["oUsuario"]).Codigo.ToString();
                    txtNomeUsuario.Text = ((clsUsuarios)Session["oUsuario"]).Nome;
                    geral.Ordem = "DataBloqueio asc";
                    Grade.DataSource = oBloqueioFinanceiroDados.PreencheDataTableBloqueioFinanceiro("");
                    Grade.DataBind();
                    txtObservacao.MaxLength = oBloqueioFinanceiroDados.PegaTamanhoCampoVarChar("Observacao");
                    PermissaoIncluir();
                    SessaoCliente();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        datDataBloqueio.Focus();
    }
    private void SessaoCliente()
    {
        if (Session["Clientes"] != null)
        {
            clsClientes oClientes = new clsClientes();
            oClientes = (clsClientes)Session["Clientes"];
            intCodigoCliente.Valor = oClientes.Codigo.ToString();
            txtNomeFantasiaCliente.Text = oClientes.NomeFantasia;
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Bloqueio Financeiro";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código bloqueio: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Código Cliente: " + intCodigoCliente.Valor + " \n";
        oLog.Log = oLog.Log + "Código Usuário: " + intCodigoUsuario.Valor + " \n";
        oLog.Log = oLog.Log + "Data Bloqueio: " + datDataBloqueio.Data + " \n";
        oLog.Log = oLog.Log + "Data Desbloqueio: " + datDataDesbloqueio.Data + " \n";
        oLog.Log = oLog.Log + "Observação: " + txtObservacao.Text + " \n";
        oLogDados.Inserir(oLog);
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
        lblMensagem.Text = oBloqueioFinanceiroDados.DadoExiste(oBloqueioFinanceiro.Sequencial);
        if (datDataBloqueio.Data.Equals(""))
        {
            lblMensagem.Text = "Bloqueio Financeiro inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oBloqueioFinanceiroDados.Excluir(oBloqueioFinanceiro.Sequencial, "");
            lblMensagem.Text = "Bloqueio Financeiro excluído com sucesso!";

            Grade.DataSource = oBloqueioFinanceiroDados.PegaDados(oBloqueioFinanceiro, 0, false);
            Grade.DataBind();
        }
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Ok")
        {
            if (datDataBloqueio.Data.Equals(""))
            {
                lblMensagem.Text = "Bloqueio Financeiro inválido!";
            }
            if (intCodigoCliente.Valor == "" || intCodigoCliente.Valor == "0")
                lblMensagem.Text = "Código do Cliente inválido!";

            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oBloqueioFinanceiro = AtribuiDadosDoForm(oBloqueioFinanceiro);
                if (hifCodigo.Value == "")
                    hifCodigo.Value = "0";
                lblMensagem.Text = oBloqueioFinanceiroDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    if (oBloqueioFinanceiro.DataDesbloqueio == "" || oBloqueioFinanceiro.DataDesbloqueio == "01/01/0001")
                        oBloqueioFinanceiro.DataDesbloqueio = "01/01/0100";
                    oBloqueioFinanceiroDados.Inserir(oBloqueioFinanceiro);
					SalvarLog("Inclusão");
                    Grade.DataSource = oBloqueioFinanceiroDados.PegaDados(oBloqueioFinanceiro, 0, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Bloqueio Financeiro incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    if (oBloqueioFinanceiro.DataDesbloqueio == "" || oBloqueioFinanceiro.DataDesbloqueio == "01/01/0001")
                        oBloqueioFinanceiro.DataDesbloqueio = "01/01/0100";
                    string msgErr = oBloqueioFinanceiroDados.Alterar(oBloqueioFinanceiro, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Bloqueio Financeiro alterado com sucesso!";
						SalvarLog("Alteração");
                        Grade.DataSource = oBloqueioFinanceiroDados.PegaDados(oBloqueioFinanceiro, 0, false);
                        Grade.DataBind();
                    }
                }
                else
                    //
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Bloqueio Financeiro";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (datDataBloqueio.Data.Equals(""))
            {
                lblMensagem.Text = "Bloqueio Financeiro inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                oBloqueioFinanceiroDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "Bloqueio Financeiro excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Bloqueio Financeiro";
                Grade.DataSource = oBloqueioFinanceiroDados.PegaDados(oBloqueioFinanceiro, 0, false);
                Grade.DataBind();
            }
        }
        LimpaCampos();
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

    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Bloqueio Financeiro";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Bloqueio Financeiro";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "CodigoUsuario" &&
            e.CommandArgument.ToString() != "DataBloqueio" && e.CommandArgument.ToString() != "DataDesbloqueio" && e.CommandArgument.ToString() != "Observacao" &&
            e.CommandArgument.ToString() != "NomeFantasiaCliente" && e.CommandArgument.ToString() != "NomeUsuario")
        {
            datDataBloqueio.Data = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oBloqueioFinanceiro = oBloqueioFinanceiroDados.PegaDados(oBloqueioFinanceiro, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oBloqueioFinanceiro);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();

        }
    }

    protected void AtribuiDadosDaClasse(clsBloqueioFinanceiro pBloqueioFinanceiro)
    {
        datDataBloqueio.Data = "";
        datDataDesbloqueio.Data = "";
        if (pBloqueioFinanceiro.DataBloqueio != "01/01/0100" && pBloqueioFinanceiro.DataBloqueio != "01/01/0001")
            datDataBloqueio.Data = Convert.ToDateTime(pBloqueioFinanceiro.DataBloqueio).ToString("dd/MM/yyyy");
        if (pBloqueioFinanceiro.DataDesbloqueio != "01/01/0100" && pBloqueioFinanceiro.DataDesbloqueio != "01/01/0001")
            datDataDesbloqueio.Data = Convert.ToDateTime(pBloqueioFinanceiro.DataDesbloqueio).ToString("dd/MM/yyyy");
        intCodigoCliente.Valor = pBloqueioFinanceiro.CodigoCliente.ToString();
        txtNomeFantasiaCliente.Text = pBloqueioFinanceiro.NomeFantasiaCliente;
        intCodigoUsuario.Valor = pBloqueioFinanceiro.CodigoUsuario.ToString();
        txtNomeUsuario.Text = pBloqueioFinanceiro.NomeUsuario;
        txtObservacao.Text = pBloqueioFinanceiro.Observacao;
    }

    protected void LimpaCampos()
    {
        datDataBloqueio.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
        datDataDesbloqueio.Data = "";
        intCodigoCliente.Valor = "";
        txtNomeFantasiaCliente.Text = "";
        intCodigoUsuario.Valor = ((clsUsuarios)Session["oUsuario"]).Codigo.ToString();
        txtNomeUsuario.Text = ((clsUsuarios)Session["oUsuario"]).Nome.ToString();
        txtObservacao.Text = "";
        hifCodigo.Value = "";
        PermissaoIncluir();
    }
    protected clsBloqueioFinanceiro AtribuiDadosDoForm(clsBloqueioFinanceiro pBloqueioFinanceiro)
    {
        pBloqueioFinanceiro.DataBloqueio = datDataBloqueio.Data;
        pBloqueioFinanceiro.DataDesbloqueio = datDataDesbloqueio.Data;
        if (intCodigoCliente.Valor != "")
            pBloqueioFinanceiro.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
        pBloqueioFinanceiro.NomeFantasiaCliente = txtNomeFantasiaCliente.Text;
        if (intCodigoUsuario.Valor != "")
        pBloqueioFinanceiro.CodigoUsuario = Convert.ToInt32(intCodigoUsuario.Valor);
        pBloqueioFinanceiro.NomeUsuario = txtNomeUsuario.Text;
        pBloqueioFinanceiro.Observacao = txtObservacao.Text.ToUpper(); 
        return pBloqueioFinanceiro;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Bloqueio Financeiro";
        LimpaCampos();
        Session["Clientes"] = null;
        hifCodigo.Value = "";
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
            if (e.Row.Cells[7].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[7].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[7].Text = "";
            if (e.Row.Cells[8].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[8].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[8].Text = "";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oBloqueioFinanceiroDados.PreencheDataTableBloqueioFinanceiro(geral.Ordem);
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Grade.DataSource = oBloqueioFinanceiroDados.PreencheDataTableBloqueioFinanceiro(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
        Grade.DataBind();
    }

    protected void btnOkCliente_Click(object sender, EventArgs e)
    {
        txtNomeFantasiaCliente.Text = "";
        if (intCodigoCliente.Valor != "")
        {
            clsClienteDados oClienteDados = new clsClienteDados();
            txtNomeFantasiaCliente.Text = oClienteDados.PegaNomeFantasia(Convert.ToInt32(intCodigoCliente.Valor));
            if (txtNomeFantasiaCliente.Text == "")
            {
                intCodigoCliente.Valor = "";
                txtNomeFantasiaCliente.Text = "Cliente inválido!";
            }
        }
    }

    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        SessaoCliente();
    }
}