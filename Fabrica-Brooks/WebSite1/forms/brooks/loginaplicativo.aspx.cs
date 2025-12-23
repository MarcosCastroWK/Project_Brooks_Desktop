using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.Sql;
using System.Data;

public partial class brooks : System.Web.UI.Page
{
    private DataSet l_ds = new DataSet();
    private DataTable l_dt = new DataTable();
    private string s;
    private clsUsuarios oUsuario = new clsUsuarios();
    private clsClienteDados oClienteDados = new clsClienteDados();
    private string rt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensagem.Text = "";
            Session["oUsuario"] = null;
            oUsuario.CodigoEmpresa = 1;      // brooks
            geral.CodigoEmpresa = 1;
            geral.BancoUsado = 1;            // 1 - Produção / 2 - Test / 3 - Test localhost
            System.Web.UI.HtmlControls.HtmlImage _img = (System.Web.UI.HtmlControls.HtmlImage)FindControl("imglogo");
            UserName.Focus();
        }
    }
    
    protected void Login_Click(object sender, EventArgs e)
    {
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        TextBox _username = (TextBox)this.Page.FindControl("UserName");
        lblMensagem.Text = "";
        if (_username != null)
        {
            try
            {
                oUsuario.Nome = _username.Text;
                oUsuario.Senha = oUsuarioDados.PegaSenha(oUsuario.Nome, geral.CodigoEmpresa);
                lblMensagem.Text = "";
                oUsuario.CodigoEmpresa = oUsuarioDados.PegaCodigoEmpresa(oUsuario.Nome);
                oUsuario.Codigo = oUsuarioDados.PegaCodigoUsuario(_username.Text, oUsuario.Senha, geral.CodigoEmpresa);
            }
            catch (Exception ex)
            {
                lblMensagem.Text = lblMensagem.Text + " \n " + ex.Message + " \n ";
            }
            oUsuario.Aplicativo = true;
            geral.UsuarioAtual = oUsuario.Nome;
            Session["oUsuario"] = oUsuario;
            TextBox _password = (TextBox)this.Page.FindControl("Password");

            string strLink = "";
            if (oUsuario.Senha == _password.Text && (oUsuario.CodigoEmpresa == Convert.ToInt16(rt) || rt == null))
            {
                geral.Demonstracao = false;
                strLink = "../../cabecalho.aspx";
                Response.Redirect(strLink, true);
            }
            else if (oUsuario.Senha != "" && oUsuario.Senha != _password.Text)
            {                
                lblMensagem.Text = "Usuário/senha inválidos!";
            }
            else if (UserName.Text != UserName.Text.ToUpper())
            {
                lblMensagem.Text = "Usuário inválido!";
            }
            else if (_password.Text != _password.Text.ToUpper())
            {
                lblMensagem.Text = "Dados inválidos!";
            }
            else
            {
                // verificar se existe uma senha nova cadastrada no cadastro do cliente                
                oClienteDados = new clsClienteDados();
                string sNovaSenhaSite = "";
                string s3LetrasNomeFantasia = "";
                if (oUsuario.Nome.Length != 9)
                    lblMensagem.Text = "Senha/usuário inválidos!";
                else
                {
                    sNovaSenhaSite = oClienteDados.PegaNovaSenhaNoSite(Convert.ToInt32(oUsuario.Nome.Substring(3, 6)));
                    s3LetrasNomeFantasia = oClienteDados.PegaNomeFantasiaCodigo(Convert.ToInt32(oUsuario.Nome.Substring(3, 6))).Rows[0]["NomeFantasia"].ToString().Substring(0, 3);
                    if (s3LetrasNomeFantasia != oUsuario.Nome.Substring(0, 3))
                        lblMensagem.Text = "Dados inválidos!";
                    else if (sNovaSenhaSite != "" && sNovaSenhaSite != _password.Text)
                    {
                        lblMensagem.Text = "Senha/usuário inválidos!";
                    }
                    else if (oUsuario.Nome == _password.Text || sNovaSenhaSite == _password.Text)
                    {
                        geral.UsuarioAtual = oUsuario.Nome;
                        oUsuario.Senha = _password.Text;
                        Session["oUsuario"] = oUsuario;
                        Response.Redirect("arquivos.aspx", true);
                    }
                    else
                    {
                        lblMensagem.Text = lblMensagem.Text + " \n Registro de usuário inválido! \n ";
                    }
                }

            }
        }
    }
    protected void btnDemonstracao_Click(object sender, EventArgs e)
    {
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        TextBox _username = (TextBox)this.Page.FindControl("UserName");
        if (_username != null)
        {
            oUsuario.Nome = "Demonstração";
            oUsuario.Senha = "123456";
            oUsuario.CodigoEmpresa = oUsuarioDados.PegaCodigoEmpresa(oUsuario.Nome);
            Session["oUsuario"] = oUsuario;
            geral.Demonstracao = true;
            Response.Redirect("menu.aspx", true);
        }

    }
}