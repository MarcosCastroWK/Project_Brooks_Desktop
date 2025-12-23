using System;
using SILCNegocios;

public partial class forms_CLIENTESCONTROL : System.Web.UI.UserControl
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
    public bool Enabled
    {
        set { txtCodigo.Enabled = value; }
        get { return txtCodigo.Enabled; }
    }
    public string Texto
    {
        set { lblCliente.Text = value; }
        get { return lblCliente.Text; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Sessao();
    }
    private void Sessao()
    {
        if (Session["Clientes"] != null)
        {
            clsClientes oClientes = new clsClientes();
            oClientes = (clsClientes)Session["Clientes"];
            Valor = oClientes.Codigo.ToString();
            Texto = oClientes.NomeFantasia;
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
       // Page _page = new Page();
       // _page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Abrir", "AbrePesquisaClientes()", true);
    }
}