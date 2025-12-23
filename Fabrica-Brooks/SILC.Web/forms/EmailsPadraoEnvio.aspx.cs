using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class EmailsPadraoEnvio : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsEmailsPadraoEnvio oEmailsPadraoEnvio = new clsEmailsPadraoEnvio();
        clsEmailsPadraoEnvioDados oEmailsPadraoEnvioDados = new clsEmailsPadraoEnvioDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "59");
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
                        Grade.DataSource = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, 0, false);
                        Grade.DataBind();
                        PermissaoIncluir();

                        txtDescricao.MaxLength = oEmailsPadraoEnvioDados.PegaTamanhoCampoVarChar("Descricao");
                        txtemail.MaxLength = oEmailsPadraoEnvioDados.PegaTamanhoCampoVarChar("email");
                        txtSenha.MaxLength = oEmailsPadraoEnvioDados.PegaTamanhoCampoVarChar("Senha");
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            txtDescricao.Focus();
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
            lblMensagem.Text = oEmailsPadraoEnvioDados.DadoExiste(oEmailsPadraoEnvio.Sequencial);
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "e-mail Padrão para Envio inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oEmailsPadraoEnvioDados.Excluir(oEmailsPadraoEnvio.Sequencial);
                lblMensagem.Text = "e-mail Padrão para Envio excluído com sucesso!";

                Grade.DataSource = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, 0, false);
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
            oLog.LocalOperacao = "Cadastro e-mail padrão";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código e-mail: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLog.Log = oLog.Log + "e-mail: " + txtemail.Text + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtSenha.Text.Length == 0)
                    lblMensagem.Text = "Senha inválida!";
                if (txtSenha.Text != txtConfirmarSenha.Text)
                    lblMensagem.Text = "Senha não confirmada!";
                if (txtDescricao.Text.Equals(""))
                    lblMensagem.Text = "e-mail Padrão para Envio inválido!";
                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oEmailsPadraoEnvio = AtribuiDadosDoForm(oEmailsPadraoEnvio);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oEmailsPadraoEnvioDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oEmailsPadraoEnvioDados.Inserir(oEmailsPadraoEnvio);
                        Grade.DataSource = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "e-mail Padrão para Envio incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oEmailsPadraoEnvioDados.Alterar(oEmailsPadraoEnvio, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "e-mail Padrão para Envio alterado com sucesso!";
                            Grade.DataSource = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;e-mail Padrão para Envio";
                    LimpaCampos();
                }
                else
                {
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;e-mail Padrão para Envio";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtSenha.Text.Length == 0)
                    lblMensagem.Text = "Senha inválida!";
                if (txtSenha.Text != txtConfirmarSenha.Text)
                    lblMensagem.Text = "Senha não confirmada!";
                if (txtDescricao.Text.Equals(""))
                    lblMensagem.Text = "e-mail Padrão para Envio inválido!";
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oEmailsPadraoEnvioDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "e-mail Padrão para Envio excluído com sucesso!";
                    lblTitulo.Text = "&nbsp;e-mail Padrão para Envio";
                    Grade.DataSource = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, 0, false);
                    Grade.DataBind();
                }
                Salvar.Text = "Ok";
                LimpaCampos();
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

        protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = "&nbsp;Exclusão de e-mail Padrão para Envio";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de e-mail Padrão para Envio";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "Descricao" &&
                e.CommandArgument.ToString() != "email")
            {
                txtDescricao.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oEmailsPadraoEnvio = oEmailsPadraoEnvioDados.PegaDados(oEmailsPadraoEnvio, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oEmailsPadraoEnvio);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsEmailsPadraoEnvio pEmailsPadraoEnvio)
        {
            txtDescricao.Text = pEmailsPadraoEnvio.Descricao.ToString();
            txtemail.Text = pEmailsPadraoEnvio.email;
            txtSenha.Text = pEmailsPadraoEnvio.Senha;
        }

        protected void LimpaCampos()
        {
            txtDescricao.Text = "";
            txtemail.Text = "";
            txtSenha.Text = "";
            txtConfirmarSenha.Text = "";
            hifCodigo.Value = "";
            PermissaoIncluir();
        }
        protected clsEmailsPadraoEnvio AtribuiDadosDoForm(clsEmailsPadraoEnvio pEmailsPadraoEnvio)
        {
            pEmailsPadraoEnvio.Descricao = txtDescricao.Text;
            pEmailsPadraoEnvio.email = txtemail.Text;
            pEmailsPadraoEnvio.Senha = txtSenha.Text;
            return pEmailsPadraoEnvio;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;e-mail Padrão para Envio";
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

            Grade.DataSource = oEmailsPadraoEnvioDados.PreencheDataTable(geral.Ordem);
            Grade.DataBind();
        }
    }
}