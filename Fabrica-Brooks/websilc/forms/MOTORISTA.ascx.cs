using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_MOTORISTA : System.Web.UI.UserControl
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
        set { lblMotorista.Text = value; }
        get { return lblMotorista.Text; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Sessao();
    }
    private void Sessao()
    {
        if (Session["Motoristas"] != null)
        {
            clsFuncionarios oFuncionarios = new clsFuncionarios();
            oFuncionarios = (clsFuncionarios)Session["Motoristas"];
            Valor = oFuncionarios.Codigo.ToString();
            Texto = oFuncionarios.Nome;
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
       // Page _page = new Page();
       // _page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Abrir", "AbrePesquisaFuncionarios()", true);
    }
}