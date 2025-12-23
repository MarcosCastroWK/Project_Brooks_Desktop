using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class webPesquisaDestinoFinal : System.Web.UI.Page
    {
        clsDestinoFinalDados oDestinoFinalsDados = new clsDestinoFinalDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            // basta que seja diferente, desta forma existe um usuário logado
            if (!IsPostBack)
            {
                if (Request.QueryString["explorer"] != null)
                {
                    oUsuario.Codigo = 10;
                    oUsuario.CodigoEmpresa = 1;
                    oUsuario.Nome = "teixeira";
                    Session["oUsuario"] = oUsuario;
                }
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                else
                {
                    try
                    {
                        Grade.DataSource = oDestinoFinalsDados.PreencheDataTableAterro("NomeFantasia asc", true);
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

            Grade.DataSource = oDestinoFinalsDados.PreencheDataTableAterro(geral.Ordem, true);
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
                {
                    e.Row.Cells[4].Text = "Não";
                }
            }

        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Ativo" &&
                e.CommandArgument.ToString() != "DestinoFinal" && e.CommandArgument.ToString() != "DataCadastro")
            {
                clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
                oDestinoFinal.Codigo = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                oDestinoFinal.NomeFantasia = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                Session["DestinoFinal"] = oDestinoFinal;
                if (Request.QueryString["explorer"] != null)
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "voltar", "VoltarHomeOrigem('" + Request.QueryString["explorer"] + "');", true);
                else
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fechar", "FechaJanela()", true);
            }
        }

    }
}