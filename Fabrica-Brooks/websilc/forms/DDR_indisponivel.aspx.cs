using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DDR_indisponivel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMensagem.Text = "Dados indisponíveis para este perfil de cadastro. Solicite informações através do link abaixo. \n";
        lblMensagem.Font.Size = 12;

        lblHome.Text = "<a href='http://www.brooksambiental.com.br/contato'>www.brooksambiental.com.br/contato</a>";
        lblHome.Font.Size = 12;
    }
}