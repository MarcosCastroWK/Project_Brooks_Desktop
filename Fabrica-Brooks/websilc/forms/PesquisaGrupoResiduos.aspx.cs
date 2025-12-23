using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class webPesquisaGrupoResiduos : System.Web.UI.Page
{
    clsResiduos oResiduos = new clsResiduos();
    clsResiduoDados oResiduosDados = new clsResiduoDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        // basta que seja diferente, desta forma existe um usuário logado
        if (!IsPostBack)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                try
                {
                    Grade.DataSource = oResiduosDados.PreencheDataTableGrupo("Grupo asc", true);
                    Grade.DataBind();
                }
                catch (Exception ex)
                {
                    lblErro.Text = ex.Message;
                }
            }
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oResiduosDados.PreencheDataTableGrupo(geral.Ordem, true);
        Grade.DataBind();

    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[3].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[3].Text = "";
            if (e.Row.Cells[4].Text.Replace(" ", "") == "1")
                e.Row.Cells[4].Text = "Sim";
            else
                e.Row.Cells[4].Text = "Não";
        }

    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Ativo" && 
            e.CommandArgument.ToString() != "Grupo" && e.CommandArgument.ToString() != "DataCadastro")
        {
            clsGrupoResiduos oGrupoResiduos = new clsGrupoResiduos();
            oGrupoResiduos.Codigo = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
            oGrupoResiduos.Grupo = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            Session["GrupoResiduos"] = oGrupoResiduos;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fechar", "FechaJanelaGrupo()", true);
        }
    }
}