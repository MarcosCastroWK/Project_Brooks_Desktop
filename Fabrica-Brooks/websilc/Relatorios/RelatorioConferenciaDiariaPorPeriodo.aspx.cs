using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class Relatorios_RelatorioConferenciaDiariaPorPeriodo : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    //Parametros.Relatorio oRel = new Parametros.Relatorio();
    clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "32");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("~/forms/sempermissao.aspx");

        if (oUsuario == null)
        {
            Response.Redirect("brooks/loginaplicativo.aspx", true);
        }
        if (!IsPostBack)
        {
            lblTitulo0.Text = "";
            Data1.Data = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            Data2.Data = DateTime.Now.ToString("dd/MM/yyyy");
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
            lblTotalMovimentacoes.Text = "";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Data1.Data != "")
        {
            if (Data2.Data == "")
                Data2.Data = Data1.Data;
            Relatorio();
        }
        else
        {
            Grade.DataSource = new DataTable();
            Grade.DataBind();
            lblTotalMovimentacoes.Text = "Data inválida ou formato incorreto!";
        }
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == 0)
        {
            lblTitulo0.Text = "Relatório de Conferência Diária Por Período - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            e.Row.Cells[2].Width = Unit.Pixel(400);
        }
        if (e.Row.RowIndex >= 0)
        {
            e.Row.Cells[4].Text = geral.DataFormatada(e.Row.Cells[4].Text);
            e.Row.Cells[5].Text = geral.DataFormatada(e.Row.Cells[5].Text);
            if (e.Row.Cells[7].Text.IndexOf(" ") > -1)
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.Split(" "[0])[0];
            e.Row.Cells[8].Text = geral.Left(e.Row.Cells[8].Text, 18);
            e.Row.Cells[8].Width = 160;
            if (e.Row.Cells[9].Text != "" && e.Row.Cells[9].Text != "&nbsp;")
                e.Row.Cells[9].Text = Convert.ToDecimal(e.Row.Cells[9].Text).ToString("N2");
            if (e.Row.Cells[11].Text != "" && e.Row.Cells[11].Text != "&nbsp;")
                e.Row.Cells[11].Text = Convert.ToDecimal(e.Row.Cells[11].Text).ToString("N2");
            if (chkComValores.Checked)
            {
                if (e.Row.Cells[12].Text != "" && e.Row.Cells[12].Text != "&nbsp;")
                    e.Row.Cells[12].Text = Convert.ToDecimal(e.Row.Cells[12].Text).ToString("N2");
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            }
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].Width = 160;
            e.Row.Cells[15].Width = 340;
        }
    }
    private void Relatorio()
    {        
        if (CLIENTESCONTROL1.Valor == "")
            CLIENTESCONTROL1.Valor = "0";
        if (rdbOrdemCaminhao.Checked)
            geral.Ordem = "cdCaminhao";
        else
            geral.Ordem = "DataRetirada";
        if (chkComValores.Checked)
            lblTitulo0.Text = "Relatório de Conferência Diária com Valores Por Período      -      Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
        else
            lblTitulo0.Text = "Relatório de Conferência Diária sem Valores Por Período      -      Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
        
        Grade.DataSource = oLancamentoDados.PreencheDadosConferenciaDiariaPorPeriodo(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), geral.Ordem, chkComValores.Checked);
        Grade.DataBind();

        lblTotalMovimentacoes.Text = "Total de movimentações: " + oLancamentoDados.QuantidadePorPeriodo(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor));
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grade.Rows.Count == 0)
                Relatorio();
        }
        finally
        {
            Session["ctrl"] = Panel1;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                "<script language=javascript>window.open('Imprimir.aspx','Imprimir','height=900px,width=1600px,scrollbars=1');</script>");
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        CLIENTESCONTROL1.Valor = "";
        CLIENTESCONTROL1.Texto = "";
        Session["Clientes"] = null;
        string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }
    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (Grade.HeaderRow == null)
        {
            lblTitulo0.Text = "Não há dados!";
            return;
        }

        Table table = new Table();
        TableRow row = new TableRow();

        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);

        Label lblEmpresa = new Label();
        Label lblTitulo = new Label();
        Label lblEmBranco = new Label();

        lblTitulo.ID = "lblTitulo";
        lblTitulo.Text = geral.RemoverAcentos(lblTitulo0.Text);
        lblEmBranco.ID = "lblEmBranco";

        string NomeArq = "Relatorio_ConferenciaPorPeriodo.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;
        for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
            Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);

        row = new TableRow();
        row.Cells.Add(new TableCell());
        lblEmpresa.ID = "lblEmpresa";
        lblEmpresa.Text = geral.NomeEmpresa(geral.CodigoEmpresa);
        row.Cells[0].Controls.Add(lblEmpresa);
        table.Rows.Add(row);

        lblEmBranco.Text = " \n";
        row = new TableRow();
        row.Cells.Add(new TableCell());
        row.Cells[0].Controls.Add(lblEmBranco);
        table.Rows.Add(row);

        Label lblTt = new Label();
        lblTt.ID = "lblTt";
        lblTt.Text = geral.RemoverAcentos(lblTitulo0.Text);
        row = new TableRow();
        row.Cells.Add(new TableCell());
        row.Cells[0].Controls.Add(lblTt);
        table.Rows.Add(row);

        lblEmBranco.Text = " \n";
        row = new TableRow();
        row.Cells.Add(new TableCell());
        row.Cells[0].Controls.Add(lblEmBranco);
        table.Rows.Add(row);

        row = new TableRow();
        row.Cells.Add(new TableCell());
        row.Cells[0].Controls.Add(Grade);
        table.Rows.Add(row);

        lblEmBranco.Text = " \n";
        row = new TableRow();
        row.Cells.Add(new TableCell());
        row.Cells[0].Controls.Add(lblEmBranco);
        table.Rows.Add(row);

        row = new TableRow();
        row.Cells.Add(new TableCell());
        lblTitulo.Text = geral.RemoverAcentos(geral.RemoverAcentos(lblTotalMovimentacoes.Text));
        row.Cells[0].Controls.Add(lblTitulo);
        table.Rows.Add(row);

        table.RenderControl(hw);
        HttpContext.Current.Response.Write(hw.InnerWriter);
        HttpContext.Current.Response.End();

    }
}