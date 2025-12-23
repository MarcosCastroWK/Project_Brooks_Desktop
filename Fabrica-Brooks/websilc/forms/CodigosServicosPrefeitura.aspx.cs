using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;


public partial class CodigosServicosPrefeitura : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsCodigoServicosPrefeitura oCodigoServicosPrefeitura = new clsCodigoServicosPrefeitura();
    clsCodigoServicosPrefeituraDados oCodigoServicosPrefeituraDados = new clsCodigoServicosPrefeituraDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "56");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
            }
            oUsuario = (clsUsuarios)Session["oUsuario"];
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                try
                {
                    if (oUsuario.Aplicativo == true)
                        menu.Visible = false;
                    else
                        menu.Visible = true;
                    Grade.DataSource = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, 0, false);
                    Grade.DataBind();
                    PermissaoIncluir();
                    txtDescricao.MaxLength = oCodigoServicosPrefeituraDados.PegaTamanhoCampoVarChar("Descricao");
                    txtNomePrefeitura.MaxLength = oCodigoServicosPrefeituraDados.PegaTamanhoCampoVarChar("NomePrefeitura");
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        intCodigo.Focus();
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
        lblMensagem.Text = oCodigoServicosPrefeituraDados.DadoExiste(oCodigoServicosPrefeitura.Sequencial);
        if (intCodigo.Valor.Equals(""))
        {
            lblMensagem.Text = "Código Serviço da Prefeitura inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oCodigoServicosPrefeituraDados.Excluir(oCodigoServicosPrefeitura.Sequencial);
            lblMensagem.Text = "Código Serviço da Prefeitura excluído com sucesso!";

            Grade.DataSource = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, 0, false);
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
        oLog.LocalOperacao = "Códigos Serviços Prefeitura";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código serviço: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
        oLog.Log = oLog.Log + "Prefeitura: " + txtNomePrefeitura.Text + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Ok")
        {
            if (intCodigo.Valor.Equals(""))
            {
                lblMensagem.Text = "Código Serviço da Prefeitura inválido!";
            }

            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oCodigoServicosPrefeitura = AtribuiDadosDoForm(oCodigoServicosPrefeitura);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oCodigoServicosPrefeituraDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclusão");
                    oCodigoServicosPrefeituraDados.Inserir(oCodigoServicosPrefeitura);
                    Grade.DataSource = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, 0, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Código Serviço da Prefeitura incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    SalvarLog("Alteração");
                    string msgErr = oCodigoServicosPrefeituraDados.Alterar(oCodigoServicosPrefeitura, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Código Serviço da Prefeitura alterado com sucesso!";
                        Grade.DataSource = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, 0, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Código Serviço da Prefeitura";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (intCodigo.Valor.Equals(""))
            {
                lblMensagem.Text = "Código Serviço da Prefeitura inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oCodigoServicosPrefeituraDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "Código Serviço da Prefeitura excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Código Serviço da Prefeitura";
                Grade.DataSource = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, 0, false);
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
        lblTitulo.Text = "&nbsp;Exclusão de Código Serviço da Prefeitura";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Código Serviço da Prefeitura";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "Codigo" &&
            e.CommandArgument.ToString() != "NomePrefeitura" && e.CommandArgument.ToString() != "DescricaoServico")
        {
            intCodigo.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oCodigoServicosPrefeitura = oCodigoServicosPrefeituraDados.PegaDados(oCodigoServicosPrefeitura, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oCodigoServicosPrefeitura);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura)
    {
        intCodigo.Valor = pCodigoServicosPrefeitura.Codigo.ToString();
        txtDescricao.Text = pCodigoServicosPrefeitura.DescricaoServico;
        txtNomePrefeitura.Text = pCodigoServicosPrefeitura.NomePrefeitura;
    }

    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        intCodigo.Valor = "";
        txtDescricao.Text = "";
        txtNomePrefeitura.Text = "";
        PermissaoIncluir();
    }
    protected clsCodigoServicosPrefeitura AtribuiDadosDoForm(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura)
    {
        pCodigoServicosPrefeitura.Codigo = 0;
        pCodigoServicosPrefeitura.DescricaoServico = "";
        pCodigoServicosPrefeitura.NomePrefeitura = "";
        if (intCodigo.Valor != "")
            pCodigoServicosPrefeitura.Codigo = Convert.ToInt16(intCodigo.Valor);
        pCodigoServicosPrefeitura.DescricaoServico = txtDescricao.Text;
        pCodigoServicosPrefeitura.NomePrefeitura = txtNomePrefeitura.Text;
        return pCodigoServicosPrefeitura;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Código Serviço da Prefeitura";
        LimpaCampos();
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
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oCodigoServicosPrefeituraDados.PreencheDataTable(geral.Ordem);
        Grade.DataBind();
    }
}