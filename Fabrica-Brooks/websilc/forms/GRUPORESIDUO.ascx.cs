using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_GRUPORESIDUO : System.Web.UI.UserControl
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
        set { lblGrupoResiduo.Text = value; }
        get { return lblGrupoResiduo.Text; }
    }
    public void WidthLabel(int pWidth)
    {
        lblGrupoResiduo.Width = pWidth;
    }
    public bool Enabled
    {
        set { txtCodigo.Enabled = value; }
        get { return txtCodigo.Enabled; }
    }

    public System.Drawing.Color ForeColor
    {
        set { txtCodigo.ForeColor = value; lblGrupoResiduo.ForeColor = value; }
        get { return txtCodigo.ForeColor; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SessaoDoGrupoResiduo();
    }
    private void SessaoDoGrupoResiduo()
    {
        if (Session["GrupoResiduos"] != null)
        {
            clsGrupoResiduos oGrupoResiduo = new clsGrupoResiduos();
            oGrupoResiduo = (clsGrupoResiduos)Session["GrupoResiduos"];
            Valor = oGrupoResiduo.Codigo.ToString();
            Texto = oGrupoResiduo.Grupo;
        }
        else if (Session["RelResiduos"] != null)
        {
            clsResiduos oResiduo = new clsResiduos();
            oResiduo = (clsResiduos)Session["RelResiduos"];
            Valor = oResiduo.Codigo.ToString();
            Texto = oResiduo.DescricaoReduzida;
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
       // Page _page = new Page();
       // _page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Abrir", "AbrePesquisaGrupoResiduos()", true);
    }
}