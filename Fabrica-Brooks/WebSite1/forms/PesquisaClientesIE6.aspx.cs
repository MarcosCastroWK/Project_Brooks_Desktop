using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class PesquisaClientesIE6 : System.Web.UI.Page
{
    clsClienteDados oClienteDados = new clsClienteDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        // basta que seja diferente, desta forma existe um usuário logado
        if (!IsPostBack)
        {
            //oUsuario = (clsUsuarios)Session["oUsuario"];
            //if (oUsuario == null)
            //{
            //    Response.Redirect("brooks/loginaplicativo.aspx", true);
            //}
            //else
            //{
                try
                {
                    Grade.DataSource = oClienteDados.PreencheDataTable("NomeFantasia asc", true);
                    Grade.DataBind();
                }
                catch (Exception ex)
                {
                    lblErro.Text = ex.Message;
                }
            //}
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oClienteDados.PreencheDataTable(geral.Ordem, true);
        Grade.DataBind();

    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[3].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[3].Text = "";
            if (e.Row.Cells[4].Text.Replace(" ", "") == "1")
                e.Row.Cells[4].Text = "Sim";
            else
                e.Row.Cells[4].Text = "Não";
        }
        */
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeFantasia")
        {
            clsClientes oCliente = new clsClientes();
            oCliente.Codigo = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
            oCliente.NomeFantasia = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oClienteDados.PegaDados(oCliente, oCliente.Codigo);
            Session["Clientes"] = oCliente;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fechar", "window.close();", true);
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Grade.DataSource = oClienteDados.PreencheDataTable(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
        Grade.DataBind();
    }
}