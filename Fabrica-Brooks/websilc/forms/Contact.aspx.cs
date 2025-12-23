using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LibSILC.clsMunicipiosDados oMunicipiosDados = new LibSILC.clsMunicipiosDados();
        geral.Ordem = "Nome asc";
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }
    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        //
    }
    protected void btnfirst_Click(object sender, EventArgs e)
    {

    }
    protected void btnprevious_Click(object sender, EventArgs e)
    {

    }
    protected void btnnext2_Click(object sender, EventArgs e)
    {

    }
}