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
    public partial class DTR_Relatorio : System.Web.UI.Page
    {
        clsDTR oDTR = new clsDTR();
        clsDTRDados oDTRDados = new clsDTRDados();
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
                        if (oUsuario.Aplicativo == true)
                            menu.Visible = false;
                        else
                            menu.Visible = true;
                        hifCodigo.Value = Request.QueryString["Sequencial"];
                        hifCodigoResiduo.Value = Request.QueryString["CodigoResiduo"];
                        Grade.DataSource = oDTRDados.PegaDadosArmazenados(hifCodigo.Value,
                                                                          hifCodigoResiduo.Value,
                                                                          "DataSaida asc");
                        Grade.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "DataColeta" &&
                e.CommandArgument.ToString() != "NomeCliente" && e.CommandArgument.ToString() != "Residuo" &&
                e.CommandArgument.ToString() != "Quantidade" && e.CommandArgument.ToString() != "NumeroImpressao" &&
                e.CommandArgument.ToString() != "DataSaida" && e.CommandArgument.ToString() != "DestinoFinal" &&
                e.CommandArgument.ToString() != "Unidade" && e.CommandArgument.ToString() != "UN" &&
                e.CommandArgument.ToString() != "Lote")
            {
                /*
                            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                            hifNumeroLancamento.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text.Split("-"[0])[0];
                            hifNumeroMTR.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text.Split("-"[0])[1];
                            hifCodigoResiduo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text.Split("-"[0])[2];
                            oDTR = oDTRDados.PegaDados(oDTR, Convert.ToInt32(hifCodigo.Value),
                                                             Convert.ToInt32(hifNumeroLancamento.Value),
                                                             Convert.ToInt32(hifNumeroMTR.Value),
                                                             Convert.ToInt32(hifCodigoResiduo.Value)); 
                 */
            }
        }

        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[9].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[9].Text.Replace(" ", "") == "01/01/0100" ||
                    e.Row.Cells[9].Text.Substring(1, 10) == "01-01-0100" || e.Row.Cells[9].Text.Substring(1, 10) == "0100-01-01" ||
                    e.Row.Cells[9].Text.Substring(1, 10) == "0001-01-01")
                    e.Row.Cells[9].Text = "";
                ImageButton x = new ImageButton();
                ImageButton y = new ImageButton();
                x = (ImageButton)e.Row.FindControl("ibnMudar");
                y = (ImageButton)e.Row.FindControl("ibnExcluir");
                if (e.Row.Cells[2].Text != "0")
                {
                    x.Visible = false;
                    y.Visible = false;
                    hifCodigo.Value = e.Row.Cells[2].Text;
                }
                else
                {
                    x.Visible = false;
                    y.Visible = false;
                }
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            hifCodigo.Value = Request.QueryString["Sequencial"];
            hifCodigoResiduo.Value = Request.QueryString["CodigoResiduo"];
            Grade.DataSource = oDTRDados.PegaDadosArmazenados(hifCodigo.Value,
                                                              hifCodigoResiduo.Value,
                                                              geral.Ordem);
            Grade.DataBind();
        }
    }
}