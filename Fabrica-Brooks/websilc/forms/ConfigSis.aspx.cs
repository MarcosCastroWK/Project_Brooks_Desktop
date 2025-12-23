using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class ConfigSis : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsConfigSis oConfigSis = new clsConfigSis();
    clsConfigSisDados oConfigSisDados = new clsConfigSisDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "53");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
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
                        menu.Visible = false;
                    else
                        menu.Visible = true;
                    Grade.DataSource = oConfigSisDados.PegaDados(oConfigSis, 0, false);
                    Grade.DataBind();
                    PermissaoIncluir();
                    txtCampo.MaxLength = oConfigSisDados.PegaTamanhoCampoVarChar("Campo");
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        txtCampo.Focus();
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
        lblMensagem.Text = oConfigSisDados.DadoExiste(oConfigSis.Codigo);
        if (txtCampo.Text.Equals(""))
        {
            lblMensagem.Text = "Configuração do Sistema inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oConfigSisDados.Excluir(oConfigSis.Codigo);
            lblMensagem.Text = "Configuração do Sistema excluído com sucesso!";

            Grade.DataSource = oConfigSisDados.PegaDados(oConfigSis, 0, false);
            Grade.DataBind();
        }
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (txtCampo.Text.Equals(""))
            {
                lblMensagem.Text = "Configuração do Sistema inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oConfigSis = AtribuiDadosDoForm(oConfigSis);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oConfigSisDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    oConfigSisDados.Inserir(oConfigSis);
                    Grade.DataSource = oConfigSisDados.PegaDados(oConfigSis, 0, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Configuração do Sistema incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = oConfigSisDados.Alterar(oConfigSis, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Configuração do Sistema alterado com sucesso!";
                        Grade.DataSource = oConfigSisDados.PegaDados(oConfigSis, 0, false);
                        Grade.DataBind();
                    }                    
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Configuração do Sistema";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtCampo.Text.Equals(""))
            {
                lblMensagem.Text = "Configuração do Sistema inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                oConfigSisDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "Configuração do Sistema excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Configuração do Sistema";
                Grade.DataSource = oConfigSisDados.PegaDados(oConfigSis, 0, false);
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
        lblTitulo.Text = "&nbsp;Exclusão de Configuração do Sistema";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Configuração do Sistema";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Ativo" &&
            e.CommandArgument.ToString() != "Campo")
        {
            txtCampo.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oConfigSis = oConfigSisDados.PegaDados(oConfigSis, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oConfigSis);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsConfigSis pConfigSis)
    {
        if (pConfigSis.Ativo == 1)
            chkAtivo.Checked = true;
        else
            chkAtivo.Checked = false;
        txtCampo.Text = pConfigSis.Campo;
    }

    protected void LimpaCampos()
    {
        chkAtivo.Checked = false;
        txtCampo.Text = "";
        hifCodigo.Value = "";
        PermissaoIncluir();
    }
    protected clsConfigSis AtribuiDadosDoForm(clsConfigSis pConfigSis)
    {
        if (chkAtivo.Checked)
            pConfigSis.Ativo = 1;
        else if (!chkAtivo.Checked)
            pConfigSis.Ativo = 0;
        
        pConfigSis.Campo = txtCampo.Text;
        return pConfigSis;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Configuração do Sistema";
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

            if (e.Row.Cells[4].Text.Replace(" ", "") == "1")
                e.Row.Cells[4].Text = "Sim";
            else
                e.Row.Cells[4].Text = "Não";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oConfigSisDados.PreencheDataTable(geral.Ordem);
        Grade.DataBind();
    }
}