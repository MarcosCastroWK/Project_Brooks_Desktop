using System;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;


public partial class fastcompost : System.Web.UI.Page
{
    private DataSet l_ds = new DataSet();
    private DataTable l_dt = new DataTable();
    private string s;
    private clsUsuarios oUsuario = new clsUsuarios();
    private string rt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensagem.Text = "";
            Session["oUsuario"] = null;
            oUsuario.CodigoEmpresa = 1; 
            geral.CodigoEmpresa = 1;
            geral.BancoUsado = 4;            // 1 - Produção / 2 - Test / 3 - Test localhost - 4- fastcompost
            if (Request.QueryString["db"] == "2")
            {
                geral.BancoUsado = 2;
                lblMensagem.Text = "Banco de dados 2 - Test";
            }
            if (Request.QueryString["db"] == "3")
            {
                geral.BancoUsado = 3; 
                lblMensagem.Text = "Banco de dados 3 - Test locahost";
            }
            if (Request.QueryString["db"] == "4")
            {
                geral.BancoUsado = 4;
                lblMensagem.Text = "Banco de dados 4 - fastcompost";
            }
            System.Web.UI.HtmlControls.HtmlImage _img = (System.Web.UI.HtmlControls.HtmlImage)FindControl("imglogo");
            UserName.Focus();
        }
    }
    
    protected void Login_Click(object sender, EventArgs e)
    {

        string strIPUsuario = Request.UserHostAddress.Replace(".", "");
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        clsClienteDados oClienteDados = new clsClienteDados();
        TextBox _username = (TextBox)this.Page.FindControl("UserName");
        if (_username != null)
        {
            try
            {
                oUsuario.Nome = _username.Text;
                oUsuario.Senha = oUsuarioDados.PegaSenha(oUsuario.Nome, geral.CodigoEmpresa);
                lblMensagem.Text = "";
                oUsuario.CodigoEmpresa = oUsuarioDados.PegaCodigoEmpresa(oUsuario.Nome);
                oUsuario.Codigo = oUsuarioDados.PegaCodigoUsuario(_username.Text, oUsuario.Senha, geral.CodigoEmpresa);
                geral.CodigoUsuarioAtual = oUsuario.Codigo;
                geral.UsuarioAtual = oUsuario.Nome;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = lblMensagem.Text + " \n " + ex.Message + " \n ";
            }
            Session["oUsuario"] = oUsuario;
            Session[strIPUsuario] = oUsuario;
            TextBox _password = (TextBox)this.Page.FindControl("Password");

            string strLink = "";
            if (oUsuario.Senha == _password.Text && (oUsuario.CodigoEmpresa == Convert.ToInt16(rt) || rt == null))
            {
                geral.Demonstracao = false;
                strLink = "../menu.aspx"; 
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
                    l_dt = oClienteDados.PegaNomeFantasiaCodigo(Convert.ToInt32(oUsuario.Nome.Substring(3, 6)));
                    if (l_dt.Rows.Count > 0)
                        s3LetrasNomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString().Substring(0, 3);

                    if (s3LetrasNomeFantasia != oUsuario.Nome.Substring(0, 3))
                        lblMensagem.Text = "Dados inválidos!";
                    else if (sNovaSenhaSite != "" && sNovaSenhaSite != _password.Text)
                    {
                        lblMensagem.Text = "Senha/usuário inválidos!";
                    }
                    else if (oUsuario.Nome == _password.Text || sNovaSenhaSite == _password.Text)
                    {
                        geral.CodigoUsuarioAtual = oUsuario.Codigo;
                        geral.UsuarioAtual = oUsuario.Nome;
                        oUsuario.Senha = _password.Text;
                        Session["oUsuario"] = oUsuario;
                        Response.Redirect("arquivos.aspx", true);
                    }
                    else if (geral.BancoUsado == 4)
                    {
                        oUsuario.CodigoEmpresa = 6;
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