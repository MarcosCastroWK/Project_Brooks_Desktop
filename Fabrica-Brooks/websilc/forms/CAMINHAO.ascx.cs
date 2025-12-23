using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_CAMINHAO : System.Web.UI.UserControl
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
        set { lblCaminhao.Text = value; }
        get { return lblCaminhao.Text; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Sessao();
    }
    private void Sessao()
    {
        if (Session["Caminhao"] != null)
        {
            clsCaminhoes oCaminhoes = new clsCaminhoes();
            oCaminhoes = (clsCaminhoes)Session["Caminhao"];
            Valor = oCaminhoes.Codigo.ToString();
            Texto = oCaminhoes.Modelo;
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
       // Page _page = new Page();
       // _page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Abrir", "AbrePesquisaCaminhoes()", true);
    }
}