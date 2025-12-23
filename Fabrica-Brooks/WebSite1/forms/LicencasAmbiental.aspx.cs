using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_LicencasAmbiental : System.Web.UI.Page
{
    clsLicencaAmbiental oLicencaAmbiental = new clsLicencaAmbiental();
    clsLicencaAmbientalDados oLicencaAmbientalDados = new clsLicencaAmbientalDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        // basta que seja diferente, desta forma existe um usuário logado
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

                    geral.Ordem = "a.NomeDestinoFinal asc";
                    Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental(geral.Ordem);
                    Grade.DataBind();
                    txtObs.MaxLength = oLicencaAmbientalDados.PegaTamanhoCampoVarChar("Obs");
                    txtCodigoAtividade.MaxLength = oLicencaAmbientalDados.PegaTamanhoCampoVarChar("CodigoAtividade");
                    txtNumeroLicenca.MaxLength = oLicencaAmbientalDados.PegaTamanhoCampoVarChar("NumeroLicenca");
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        intCodigoDestinoFinal.Focus();
    }
    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oLicencaAmbientalDados.DadoExiste(oLicencaAmbiental.Codigo);
        if (txtNumeroLicenca.Text.Equals(""))
        {
            lblMensagem.Text = "LicencaAmbiental inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oLicencaAmbientalDados.Excluir(oLicencaAmbiental.Codigo, "");
            lblMensagem.Text = "LicencaAmbiental excluído com sucesso!";

            Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental("Codigo asc");
            Grade.DataBind();
        }
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Ok")
        {
            if (txtNumeroLicenca.Text.Equals(""))
            {
                lblMensagem.Text = "LicencaAmbiental inválido!";
            }

            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oLicencaAmbiental = AtribuiDadosDoForm(oLicencaAmbiental);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oLicencaAmbientalDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    oLicencaAmbientalDados.Inserir(oLicencaAmbiental);
                    Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental("Codigo asc");
                    Grade.DataBind();
                    lblMensagem.Text = "Licença Ambiental incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = oLicencaAmbientalDados.Alterar(oLicencaAmbiental, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Licença Ambiental alterado com sucesso!";
                        Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental("Codigo asc");
                        Grade.DataBind();
                    }
                    
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Licença Ambiental";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtNumeroLicenca.Text.Equals(""))
            {
                lblMensagem.Text = "Licença Ambiental inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                oLicencaAmbientalDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "Licenca Ambiental excluída com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Licença Ambiental";
                Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental("Codigo asc");
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
        lblTitulo.Text = "&nbsp;Exclusão de Licença Ambiental";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Licença Ambiental";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "CodigoUsuario" &&
            e.CommandArgument.ToString() != "DataBloqueio" && e.CommandArgument.ToString() != "DataDesbloqueio" && e.CommandArgument.ToString() != "Observacao" &&
            e.CommandArgument.ToString() != "NomeFantasiaCliente" && e.CommandArgument.ToString() != "NomeUsuario")
        {
            txtNumeroLicenca.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oLicencaAmbiental = oLicencaAmbientalDados.PegaDados(oLicencaAmbiental, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oLicencaAmbiental);
        }
    }

    protected void AtribuiDadosDaClasse(clsLicencaAmbiental pLicencaAmbiental)
    {
        txtNumeroLicenca.Text = "";
        datPrazoValidade.Text = "";        
    }

    protected void LimpaCampos()
    {
        txtNumeroLicenca.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        datPrazoValidade.Text = "";
        intCodigoDestinoFinal.Valor = "";
        txtNomeDestinoFinal.Text = "";
        txtObs.Text = "";

    }
    protected clsLicencaAmbiental AtribuiDadosDoForm(clsLicencaAmbiental pLicencaAmbiental)
    {
        pLicencaAmbiental.Obs = txtObs.Text; 
        return pLicencaAmbiental;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Licença Ambiental";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[6].Text = "";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oLicencaAmbientalDados.PreencheDataTableLicencaAmbiental(geral.Ordem);
        Grade.DataBind();
    }
}