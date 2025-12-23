using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILCNegocios;
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