using System;
using LibSILC;
using System.Web.UI.WebControls;
using SILCNegocios;

namespace SILC.Web.forms
{
    public partial class forms_menu : System.Web.UI.Page
    {
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null && geral.UsuarioAtual == "")
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                if (oUsuario == null)
                {
                    oUsuario = new clsUsuarios();
                    oUsuario.CodigoEmpresa = geral.CodigoEmpresa;
                    oUsuario.Nome = geral.UsuarioAtual;
                    Session["oUsuario"] = oUsuario;
                    cabecalho1.Visible = false;
                }
            }
        }
    }
}