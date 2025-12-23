using System;
using System.Web.UI;

namespace SILC.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //limpar sessão
            Session["oUsuario"] = null;
            //string strLink = "forms/brooks/";
            //Response.Redirect(strLink, true);

        }
    }
}