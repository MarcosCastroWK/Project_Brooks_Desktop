using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_CLIENTE : System.Web.UI.UserControl
{
    private clsClienteDados oClienteDados = new clsClienteDados();
    private DataTable _dt = new DataTable();
    private int _ln = 0;
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
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        //Page _page = new Page();
        //_page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Abrir", "AbrePesquisaClientes()", true);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        _dt = new DataTable();
        if (txtCodigoNomes.Text != "" && geral.IsNumeric(txtCodigoNomes.Text))
        {
            _dt = oClienteDados.PreencheNomeFantasiaComCodigo(Convert.ToInt32(txtCodigoNomes.Text));
            if (_dt.Rows.Count >= 1)
            {
                txtCodigoNomes.Text = _dt.Rows[0]["Codigo"].ToString() + "|" + _dt.Rows[0]["NomeFantasia"].ToString();
                _dt = new DataTable();
            }
        }
        else if (txtCodigoNomes.Text != "")
        {
            //GradeCliente.DataSource = oClienteDados.PreencheComNomeFantasia(txtCodigoNomes.Text);
            //GradeCliente.DataBind();
        }
    }
}