using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class NotasFiscais : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsNotasFiscais oNotasFiscais = new clsNotasFiscais();
    clsNotasFiscaisDados oNotasFiscaisDados = new clsNotasFiscaisDados();
    clsCorpoNotasFiscais oCorpoNotasFiscais = new clsCorpoNotasFiscais();
    clsCorpoNotasFiscaisDados oCorpoNotasFiscaisDados = new clsCorpoNotasFiscaisDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsClienteDados oClienteDados = new clsClienteDados();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        oUsuario = (clsUsuarios)Session["oUsuario"];
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "19");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
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

                    Grade.DataSource = oNotasFiscaisDados.PegaDados(oNotasFiscais, 0, true);
                    Grade.DataBind();
                  
                    if (Grade.Rows[1].Cells[1].Text != "")
                        oNotasFiscais.NumeroNF = Convert.ToInt32(Grade.Rows[1].Cells[1].Text);

                    if (Grade.Rows[1].Cells[8].Text != "")
                        oNotasFiscais.NumeroNotaFiscal = Convert.ToInt32(Grade.Rows[1].Cells[8].Text);

                    if (Grade.Rows[1].Cells[3].Text != "")
                        hifCodigo.Value = Grade.Rows[1].Cells[3].Text;
 
                    GraceItensNF.DataSource = oCorpoNotasFiscaisDados.PegaDados(oCorpoNotasFiscais, oNotasFiscais.NumeroNotaFiscal, true);
                    GraceItensNF.DataBind();

                    GradeClientes.DataSource = oClienteDados.PreencheDataTable("NomeFantasia asc", true);
                    GradeClientes.DataBind();
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
        lblTitulo.Text = "&nbsp;Nota Fiscal";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "NumeroNF" && e.CommandArgument.ToString() != "NomeCliente" && e.CommandArgument.ToString() != "CodigoCliente" &&
            e.CommandArgument.ToString() != "DataEmissao" && e.CommandArgument.ToString() != "CNPJ_CPF" && e.CommandArgument.ToString() != "ValorTotal" &&
            e.CommandArgument.ToString() != "Situacao" && e.CommandArgument.ToString() != "Observacao" && e.CommandArgument.ToString() != "NumeroNotaFiscal")
        {
            int _NumeroNF = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
            if (oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text != "")
            {
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                oNotasFiscais.CodigoCliente = Convert.ToInt32(hifCodigo.Value);
            }
            oNotasFiscais = oNotasFiscaisDados.PegaDados(oNotasFiscais, oNotasFiscais.CodigoCliente, _NumeroNF, false);

            Grade.DataSource = oNotasFiscaisDados.PegaDados(oNotasFiscais,  Convert.ToInt32(hifCodigo.Value), false);
            Grade.DataBind();

            GraceItensNF.DataSource = oCorpoNotasFiscaisDados.PegaDados(oCorpoNotasFiscais , oNotasFiscais.NumeroNotaFiscal, false);
            GraceItensNF.DataBind();
        }
    }

    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[5].Text = "";

            if (e.Row.Cells[7].Text == "1")
                e.Row.Cells[7].Text = "Cancelada";
            else
                e.Row.Cells[7].Text = "";
       }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        Grade.DataSource = oNotasFiscaisDados.PreencheDataTableOrdem(geral.Ordem, Convert.ToInt32(hifCodigo.Value));
        Grade.DataBind();
    }
    protected void GradeClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "NomeFantasia" && e.CommandArgument.ToString() != "Codigo")
        {
            string CodigoCliente = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            hifCodigo.Value = CodigoCliente;
            Grade.DataSource = oNotasFiscaisDados.PegaDados(oNotasFiscais, Convert.ToInt32(CodigoCliente), false);
            Grade.DataBind();
        }
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        GradeClientes.DataSource = oClienteDados.PreencheDataTable("NomeFantasia asc", txtBuscar.Text, "NomeFantasia");
        GradeClientes.DataBind();
    }
    protected void Grade_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GradeClientes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GraceItensNF_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}