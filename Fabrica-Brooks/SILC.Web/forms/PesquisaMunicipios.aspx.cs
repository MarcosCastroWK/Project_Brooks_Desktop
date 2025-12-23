using System;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class PesquisaMunicipios : System.Web.UI.Page
    {
        clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
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
                if (oUsuario == null && Request.QueryString["explorer"] == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                else
                {
                    try
                    {

                        Grade.DataSource = oMunicipioDados.PreencheDataTableFiltro("UF asc", "SC", "UF");
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

            Grade.DataSource = oMunicipioDados.PreencheDataTableOrdem("Nome asc");
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
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome")
            {
                clsMunicipios oMunicipio = new clsMunicipios();
                oMunicipio.Codigo = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                oMunicipio.Nome = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;

                oMunicipioDados.PegaDados(oMunicipio, oMunicipio.Codigo);

                if (Request.QueryString["TipoEndereco"] == "1")
                    Session["oMunicipio1"] = oMunicipio;
                else if (Request.QueryString["TipoEndereco"] == "2")
                    Session["oMunicipio2"] = oMunicipio;
                else if (Request.QueryString["TipoEndereco"] == "3")
                    Session["oMunicipio3"] = oMunicipio;

                if (Request.QueryString["explorer"] != null)
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "voltar", "VoltarHomeOrigem('" + Request.QueryString["explorer"] + "');", true);
                else
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fechar", "FechaJanela()", true);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Grade.DataSource = oMunicipioDados.PreencheDataTableFiltro(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
            Grade.DataBind();
        }
    }
}