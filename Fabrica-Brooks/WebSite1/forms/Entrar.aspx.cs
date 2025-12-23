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

public partial class Account_Entrar : System.Web.UI.Page
{
    private MySqlConnection l_mySqlConnect;
    private MySqlDataAdapter l_myData;
    private DataSet l_ds = new DataSet();
    private DataTable l_dt = new DataTable();
    private string s;
    private clsUsuarios oUsuario = new clsUsuarios();
    private string rt;

    // funções locais para multi banco de dados
    private void ConectaBanco()
    {
        ConectaMySql();
    }
    private void FillDataSet()
    {
        l_ds = new DataSet();
        l_myData = new MySqlDataAdapter(s,l_mySqlConnect);
        l_myData.Fill(l_ds);
        l_dt = l_ds.Tables[0];
    }
    private void DesconectaBanco()
    {
        DesconectaMySql();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        rt = Request.QueryString["Codigo"];
        if (!IsPostBack)
        {
            lblMensagem.Text = "";
            Session["oUsuario"] = null;
            //oUsuario.CodigoEmpresa = 2;      // ewvs
            oUsuario.CodigoEmpresa = 1;      // brooks
            
            if (rt != "")
                oUsuario.CodigoEmpresa = Convert.ToInt16(rt);
            System.Web.UI.HtmlControls.HtmlImage _img = (System.Web.UI.HtmlControls.HtmlImage)FindControl("imglogo");
            if (oUsuario.CodigoEmpresa == 2) // é EWVS
                _img.Src = "../Images/ewvs.png";
            if (oUsuario.CodigoEmpresa == 3) // é FIRST Rent a Car - São Paulo/SP
                _img.Src = "../Images/first.jpg";
            if (oUsuario.CodigoEmpresa == 4) // é VIAMAR Rent a Car - Florianópolis/SC
                _img.Src = "../Images/viamar.jpg";
            UserName.Focus();
        }
    }
    
    protected void Login_Click(object sender, EventArgs e)
    {
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
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
            }
            catch (Exception ex)
            {
                lblMensagem.Text = lblMensagem.Text + " \n " + ex.Message + " \n ";
            }
            Session["oUsuario"] = oUsuario;
            TextBox _password = (TextBox)this.Page.FindControl("Password");

            string strLink = "";
            if (oUsuario.Senha == _password.Text && (oUsuario.CodigoEmpresa == Convert.ToInt16(rt) || rt == null))
            {
                geral.Demonstracao = false;
                strLink = "menu.aspx"; 
                Response.Redirect(strLink, true);
            }
            else
            {
                lblMensagem.Text = lblMensagem.Text + " \n Registro de usuário inválido! \n " ;
            }
        }
    }
    public void ConectaMySql()
    {
        //localhost
        //s = "Persist Security Info=false;server=localhost;uid=root;database=silc;pwd=1234";
        //página locaweb
        //s = "Persist Security Info=false;server=179.188.16.22;password=yes;uid=ewvssilc;database=ewvssilc;pwd=silc4321";
        //uol host 
        s = "Persist Security Info=false;server=200.98.129.47;password=yes;uid=root;database=ewvs;pwd=BR**ks729";
        l_mySqlConnect = new MySqlConnection(s);
        try
        {
            l_mySqlConnect.Open();
        }
        catch (Exception ex)
        {
            s = "Erro db";
        }
    }
    public void DesconectaMySql()
    {
        l_mySqlConnect.Close();
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