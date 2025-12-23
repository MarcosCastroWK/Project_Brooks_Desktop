using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;

public partial class forms_DESTINOFINAL : System.Web.UI.UserControl
{
    public string Valor
    {
        set { txtCodigo.Text = value; }
        get { return txtCodigo.Text; }
    }
    public short IndiceTab
    {
        set { txtCodigo.TabIndex = value; }
        get { return txtCodigo.TabIndex; }
    }
    public string Texto
    {
        set { lblDestinoFinal.Text = value; }
        get { return lblDestinoFinal.Text; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SessaoDoDestinoFinal();
    }
    private void SessaoDoDestinoFinal()
    {
        if (Session["DestinoFinal"] != null)
        {
            clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
            oDestinoFinal = (clsDestinoFinal)Session["DestinoFinal"];
            Valor = oDestinoFinal.Codigo.ToString();
            Texto = oDestinoFinal.NomeFantasia;
        }
    }

}