using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class Account_Usuarios : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "14");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                Response.Redirect("sempermissao.aspx");

            if (!IsPostBack)
            {
                geral.CodigoEmpresa = 1;
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
                    if (oUsuario.Aplicativo == true)
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    Grade.DataSource = oUsuarioDados.PegaDadosEmpresa(oUsuario, 0, false, oUsuario.CodigoEmpresa);
                    Grade.DataBind();
                    UserName.MaxLength = Convert.ToInt16(oUsuarioDados.PegaTamanhoCampoVarChar("Nome"));
                    PermissaoIncluir();
                }
            }
            UserName.Focus();
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
            lblMensagem.Text = "";
            oUsuario.CodigoEmpresa = ((clsUsuarios)Session["oUsuario"]).CodigoEmpresa;
            if (UserName.Text.Equals(""))
            {
                lblMensagem.Text = "Nome de usuário inválido!";
            }
            else if (Password.Text.Equals(""))
            {
                lblMensagem.Text = "Senha inválida!";
            }
            else if (UserName.Text.ToUpper() == Password.Text.ToUpper())
            {
                lblMensagem.Text = "Senha do Usuário inválida!";
            }
            else if (Password.Text.Length < 6)
            {
                lblMensagem.Text = "Número de caracteres da Senha inválido (mínimo 6)!";
            }
            else if (!Password.Text.Equals(ConfirmaSenha.Text))
            {
                lblMensagem.Text = "Senha não foi confirmada!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                if (oUsuarioDados.PegaSenha(UserName.Text, geral.CodigoEmpresa) == Password.Text)
                {
                    oUsuarioDados.Excluir(0, UserName.Text, geral.CodigoEmpresa);
                    lblMensagem.Text = "Usuário excluído com sucesso!";
                    Grade.DataSource = oUsuarioDados.PegaDadosEmpresa(oUsuario, 0, false, oUsuario.CodigoEmpresa);
                    Grade.DataBind();
                }
                else
                {
                    lblMensagem.Text = "Registro de usuário inválido!";
                }
            }
        }
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Cadastro de Usuário";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Nome do usuário: " + UserName.Text + " \n";
            oLogDados.Inserir(oLog);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            oUsuario.CodigoEmpresa = ((clsUsuarios)Session["oUsuario"]).CodigoEmpresa;

            if (Salvar.Text == "Ok")
            {
                if (UserName.Text.Equals(""))
                {
                    lblMensagem.Text = "Nome de usuário inválido!";
                }
                else if (Password.Text.Equals(""))
                {
                    lblMensagem.Text = "Senha inválida!";
                }
                else if (UserName.Text.ToUpper() == Password.Text.ToUpper())
                {
                    lblMensagem.Text = "Senha do Usuário inválida!";
                }
                else if (Password.Text.Length < 6)
                {
                    lblMensagem.Text = "Número de caracteres da Senha inválido (mínimo 6)!";
                }
                else if (!Password.Text.Equals(ConfirmaSenha.Text))
                {
                    lblMensagem.Text = "Senha não foi confirmada!";
                }
                else if (oUsuarioDados.PegaDados(0, UserName.Text, geral.CodigoEmpresa).Equals(UserName.Text) && !lblNovaSenha.Visible && !UserName.Text.Equals(""))
                {
                    lblMensagem.Text = "Usuário já existe!";
                }
                else if (NovaSenha.Text.Equals("") && lblNovaSenha.Visible)
                {
                    lblMensagem.Text = "Nova Senha inválida!";
                }
                else if (ConfirmaNovaSenha.Text.Equals("") && lblNovaSenha.Visible)
                {
                    lblMensagem.Text = "Confirma Nova Senha inválida!";
                }
                else if (!NovaSenha.Text.Equals(ConfirmaNovaSenha.Text) && lblNovaSenha.Visible)
                {
                    lblMensagem.Text = "Nova Senha não foi confirmada!";
                }
                else if (UserName.Text.ToUpper() == NovaSenha.Text.ToUpper() && lblNovaSenha.Visible)
                {
                    lblMensagem.Text = "Nova Senha do Usuário inválida!";
                }
                else if (NovaSenha.Text.Length < 6 && lblNovaSenha.Visible)
                {
                    lblMensagem.Text = "Número de caracteres da Nova Senha inválido (mínimo 6)!";
                }
                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oUsuario.Nome = UserName.Text;
                    oUsuario.Senha = Password.Text;
                    string _usuario = oUsuarioDados.PegaDados(0, UserName.Text, geral.CodigoEmpresa).ToUpper();
                    if (!(_usuario.Equals(UserName.Text.ToUpper())))
                    {
                        SalvarLog("Inclusão");
                        oUsuarioDados.Inserir(oUsuario);
                        Grade.DataSource = oUsuarioDados.PegaDadosEmpresa(oUsuario, 0, false, oUsuario.CodigoEmpresa);
                        Grade.DataBind();
                        NovaSenha.Visible = false;
                        ConfirmaNovaSenha.Visible = false;
                        lblConfirmaNovaSenha.Visible = false;
                        lblNovaSenha.Visible = false;
                        lblMensagem.Text = "Usuário incluído com sucesso!";
                    }
                    else
                    {
                        if (!lblNovaSenha.Visible)
                        {
                            lblMensagem.Text = "Usuário já existe!";
                        }
                        else // mudar senha
                        {
                            // verificar se a senha informada é igual a gravada, caso contrário NÃO salvar senha nova
                            if (oUsuarioDados.PegaSenha(oUsuario.Nome, geral.CodigoEmpresa) == Password.Text)
                            {
                                SalvarLog("Alteração");
                                string msgErr = oUsuarioDados.AlteraSenha(NovaSenha.Text, UserName.Text, geral.CodigoEmpresa);
                                if (msgErr.Length > 0)
                                    lblMensagem.Text = msgErr;
                                else
                                    lblMensagem.Text = "Senha alterada com sucesso!";
                                Salvar.Text = "Ok";
                                lblTitulo.Text = "&nbsp;Cadastro de Usuários";
                                NovaSenha.Visible = false;
                                ConfirmaNovaSenha.Visible = false;
                                lblConfirmaNovaSenha.Visible = false;
                                lblNovaSenha.Visible = false;
                            }
                            else
                                lblMensagem.Text = "Senha não é mesma cadastrada!";
                        }
                    }
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                oUsuario.CodigoEmpresa = ((clsUsuarios)Session["oUsuario"]).CodigoEmpresa;

                if (UserName.Text.Equals(""))
                {
                    lblMensagem.Text = "Nome de usuário inválido!";
                }
                else if (Password.Text.Equals(""))
                {
                    lblMensagem.Text = "Senha inválida!";
                }
                else if (UserName.Text.ToUpper() == Password.Text.ToUpper())
                {
                    lblMensagem.Text = "Senha do Usuário inválida!";
                }
                else if (Password.Text.Length < 6)
                {
                    lblMensagem.Text = "Número de caracteres da Senha inválido (mínimo 6)!";
                }
                else if (!Password.Text.Equals(ConfirmaSenha.Text))
                {
                    lblMensagem.Text = "Senha não foi confirmada!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    oUsuario.CodigoEmpresa = ((clsUsuarios)Session["oUsuario"]).CodigoEmpresa;
                    if (oUsuarioDados.PegaSenha(UserName.Text, geral.CodigoEmpresa) == Password.Text)
                    {
                        SalvarLog("Exclusão");
                        oUsuarioDados.Excluir(0, UserName.Text, geral.CodigoEmpresa);
                        lblMensagem.Text = "Usuário excluído com sucesso!";
                        Salvar.Text = "Ok";
                        lblTitulo.Text = "&nbsp;Cadastro de Usuários";
                        Grade.DataSource = oUsuarioDados.PegaDadosEmpresa(oUsuario, 0, false, oUsuario.CodigoEmpresa);
                        Grade.DataBind();
                    }
                    else
                    {
                        lblMensagem.Text = "Registro de usuário inválido!";
                    }
                }
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
            lblTitulo.Text = "&nbsp;Exclusão de Usuários";
            Salvar.Text = "Confirma";
            NovaSenha.Visible = false;
            ConfirmaNovaSenha.Visible = false;
            lblConfirmaNovaSenha.Visible = false;
            lblNovaSenha.Visible = false;
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            if (!NovaSenha.Visible)
            {
                lblTitulo.Text = "&nbsp;Alteração de senha";
                NovaSenha.Visible = true;
                ConfirmaNovaSenha.Visible = true;
                lblConfirmaNovaSenha.Visible = true;
                lblNovaSenha.Visible = true;
            }
            else
            {
                lblTitulo.Text = "&nbsp;Cadastro de Usuários";
                NovaSenha.Visible = false;
                ConfirmaNovaSenha.Visible = false;
                lblConfirmaNovaSenha.Visible = false;
                lblNovaSenha.Visible = false;
            }
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome")
            {
                UserName.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            Grade.DataSource = oUsuarioDados.PreencheDataTableUsuarios(geral.Ordem, geral.CodigoEmpresa);
            Grade.DataBind();

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro de Usuários";
            lblMensagem.Text = "";
            NovaSenha.Visible = false;
            ConfirmaNovaSenha.Visible = false;
            lblConfirmaNovaSenha.Visible = false;
            lblNovaSenha.Visible = false;
            Salvar.Text = "Ok";
            PermissaoIncluir();
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
    }
}