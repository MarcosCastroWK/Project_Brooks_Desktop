using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class Relatorios_RelatorioConferenciaDiaria : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "31");
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
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
            lblTotalMovimentacoes.Text = "";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Data1.Data != "")
            Relatorio();
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
            lblTitulo0.Text = "Relatório de Conferência Diária      -      Data emissão: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy");
            e.Row.Cells[2].Width = Unit.Pixel(300);
        }
        // reconfigura colunas
        e.Row.Cells[2].Style.Add("width", "12px");
        e.Row.Cells[6].Style.Add("width", "12px");
        e.Row.Cells[7].Style.Add("width", "20px");
        e.Row.Cells[14].Style.Add("width", "12px");
        e.Row.Cells[16].Style.Add("width", "20px");
        e.Row.Cells[18].Style.Add("width", "12px");
        e.Row.Cells[19].Style.Add("width", "12px");
        e.Row.Cells[20].Style.Add("visibility", "hidden");
        e.Row.Cells[20].Style.Add("width", "1px");
        e.Row.Cells[20].Text = "";
        e.Row.Cells[21].Style.Add("visibility", "hidden");
        e.Row.Cells[21].Style.Add("width", "1px");
        e.Row.Cells[21].Text = "";
        if (e.Row.RowIndex >= 0)
        {           
            if (e.Row.Cells[8].Text != "" && e.Row.Cells[8].Text != "&nbsp;")
                e.Row.Cells[8].Text = Convert.ToDecimal(e.Row.Cells[8].Text).ToString("N2");
            if (e.Row.Cells[9].Text != "" && e.Row.Cells[9].Text != "&nbsp;")
                e.Row.Cells[9].Text = Convert.ToDecimal(e.Row.Cells[9].Text).ToString("N2");
            if (e.Row.Cells[10].Text != "" && e.Row.Cells[10].Text != "&nbsp;")
                e.Row.Cells[10].Text = Convert.ToDecimal(e.Row.Cells[10].Text).ToString("N2");
            if (chkComValores.Checked)
            {
                if (e.Row.Cells[12].Text != "" && e.Row.Cells[12].Text != "&nbsp;")
                    e.Row.Cells[12].Text = Convert.ToDecimal(e.Row.Cells[12].Text).ToString("N2");
                if (e.Row.Cells[13].Text != "" && e.Row.Cells[13].Text != "&nbsp;")
                    e.Row.Cells[13].Text = Convert.ToDecimal(e.Row.Cells[13].Text).ToString("N2");
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            }
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            if (e.Row.Cells[5].Text.IndexOf("─") > -1)
            {
                e.Row.Cells[0].Text = "──────";     // MTR nº
                e.Row.Cells[1].Text = "─────";  // Código Cliente
                e.Row.Cells[2].Text = "──────────────────";  // Nome fantasia	
                e.Row.Cells[3].Text = "──────";  // Retirada	
                e.Row.Cells[4].Text = "───────";  // Colocada	
                e.Row.Cells[5].Text = "────────";       // Caminhão	
                e.Row.Cells[6].Text = "──────────";  // Motorista	
                e.Row.Cells[7].Text = "──────────────────";  // Resíduo		
                e.Row.Cells[8].Text = "───────";  // Qt.Coleta
                e.Row.Cells[9].Text = "───────";  // QtDescarga
                e.Row.Cells[10].Text = "──────";  // Diferença	
                e.Row.Cells[11].Text = "────";  // Und	 
                e.Row.Cells[12].Text = "───────";  // Valor
                e.Row.Cells[13].Text = "─────────";  // Valor total
                e.Row.Cells[14].Text = "────────────";  // destino
                e.Row.Cells[15].Text = "───────";     // nº lançamento
                e.Row.Cells[16].Text = "─────────────────";  // obs
                e.Row.Cells[17].Text = "─────────";     // nº mtr e
                e.Row.Cells[18].Text = "────────────";     // Destino mtr e
                e.Row.Cells[19].Text = "────────────";     // Controle Interno Descarga
            }
            else
            {
                //e.Row.Cells[0].Style.Add("border", "1");
                //e.Row.Cells[1].BorderWidth = 1;
            }
        }
    }
    private void Relatorio()
    {   
        if (CLIENTESCONTROL1.Valor == "")
            CLIENTESCONTROL1.Valor = "0";
        
        if (chkComValores.Checked)
            lblTitulo0.Text = "Relatório de Conferência Diária com Valores      -       Data emissão: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy");
        else
            lblTitulo0.Text = "Relatório de Conferência Diária sem Valores      -       Data emissão: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy");

        DataTable dt = new DataTable();
        dt = oLancamentoDados.PreencheDadosConferenciaDiaria(Data1.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), "", chkComValores.Checked);
        string sNumeroLancamentoAnterior = "";
        string sClienteAnterior = "";
        string sRetirada = "";
        string sColocada = "";
        string sCodigoResiduo = "";
        string sCaminhaoAnterior = "";
        int _cdCaminhaoAnterior = 0;
        int iContagem = 0;
        int iRegs = dt.Rows.Count;
        if (iRegs > 0)
        {
            DataTable dtNovo = new DataTable();
            DataRow[] dr2 = dt.Select("", "cdCaminhao, Caminhão");
            for (int i = 0; i < iRegs; i++)
            {
                DataRow dr = dt.Rows[i];
                if (_cdCaminhaoAnterior != Convert.ToInt32(dr["cdCaminhao"]) && i > 0)
                {
                    dt.NewRow();
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["cdCaminhao"] = _cdCaminhaoAnterior;
                    dt.Rows[dt.Rows.Count - 1]["Caminhão"] = sCaminhaoAnterior + "z";
                }
                if (sNumeroLancamentoAnterior == dr["NºLanç"].ToString() && sRetirada == dr["Retirada"].ToString())
                {
                    dr["Retirada"] = "";
                }
                if (sNumeroLancamentoAnterior == dr["NºLanç"].ToString() && sColocada == dr["Colocada"].ToString())
                {
                    dr["Colocada"] = "";
                }
                if (sClienteAnterior == dr["Código Cliente"].ToString())
                {
                    dr["Nome fantasia"] = "";
                }
                if (sNumeroLancamentoAnterior == dr["NºLanç"].ToString() && sRetirada == "" && sColocada == "")
                {
                    dr["Retirada"] = "";
                }
                if (sClienteAnterior == dr["Código Cliente"].ToString())
                {
                    dr["Nome fantasia"] = "";
                    dr["Código Cliente"] = "";
                }
                if (dr["Retirada"].ToString() != "" || dr["Colocada"].ToString() != "")
                {
                    if (dr["Retirada"].ToString() != null || dr["Colocada"].ToString() != null)
                    {
                        iContagem++;
                    }
                }
                sNumeroLancamentoAnterior = dr["NºLanç"].ToString();
                sClienteAnterior = dr["Código Cliente"].ToString();
                sRetirada = dr["Retirada"].ToString();
                sColocada = dr["Colocada"].ToString();
                sCodigoResiduo = dr["Resíduo"].ToString();
                sCaminhaoAnterior = dr["Caminhão"].ToString();
                _cdCaminhaoAnterior = Convert.ToInt32(dr["cdCaminhao"]);
            }
            dtNovo = new DataTable();
            dr2 = dt.Select("", "cdCaminhao, Caminhão");

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                DataColumn dc = dt.Columns[j];
                dtNovo.Columns.Add(dc.Caption, dc.DataType);
            }
            for (int i = 0; i < dr2.Length; i++)
            {
                dtNovo.NewRow();
                dtNovo.Rows.Add();
                for (int j = 0; j < dtNovo.Columns.Count; j++)
                {
                    dtNovo.Rows[dtNovo.Rows.Count - 1][j] = dr2[i][j];
                    if (dr2[i]["Caminhão"].ToString().IndexOf("z") > -1)
                    {
                        dtNovo.Rows[dtNovo.Rows.Count - 1]["Caminhão"] = "────────";
                    }
                }
            }
            dtNovo.NewRow();
            dtNovo.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["cdCaminhao"] = _cdCaminhaoAnterior;
            dt.Rows[dt.Rows.Count - 1]["Caminhão"] = sCaminhaoAnterior + "z";
            for (int j = 0; j < dtNovo.Columns.Count; j++)
            {
                dtNovo.Rows[dtNovo.Rows.Count - 1]["Caminhão"] = "────────";
            }
            Grade.DataSource = dtNovo;
            Grade.DataBind();
            Grade.HeaderRow.Cells[1].Text = "Código";
            Grade.HeaderRow.Cells[2].Text = "Cliente";
            Grade.HeaderRow.Cells[3].Text = "Caixa Retirada";
            Grade.HeaderRow.Cells[4].Text = "Caixa Colocada";
            Grade.HeaderRow.Cells[5].Text = "Caminhão";
            Grade.HeaderRow.Cells[6].Text = "Motorista";
            Grade.HeaderRow.Cells[7].Text = "Resíduo";
            Grade.HeaderRow.Cells[8].Text = "Qtde Coletada";
            Grade.HeaderRow.Cells[9].Text = "Qtde Descarga";
            Grade.HeaderRow.Cells[10].Text = "% Dif";
            Grade.HeaderRow.Cells[11].Text = "Un";
            Grade.HeaderRow.Cells[12].Text = "Valor";
            Grade.HeaderRow.Cells[13].Text = "Valor total";
            Grade.HeaderRow.Cells[14].Text = "Destino";
            Grade.HeaderRow.Cells[15].Text = "Nr.Lanc";
            Grade.HeaderRow.Cells[16].Text = "Observação";
            Grade.HeaderRow.Cells[17].Text = "MTR-e Nº";
            Grade.HeaderRow.Cells[18].Text = "Destino MTR-e";
            Grade.HeaderRow.Cells[19].Text = "Controle Interno Descarga";
            for (int i = 0; i < Grade.Rows.Count - 1; i++)
                Grade.Rows[i].Height = 22;
            lblTotalMovimentacoes.Text = "Total de movimentações: " + iContagem.ToString();
        }
        else
        {
            Grade.DataSource = new DataTable();
            Grade.DataBind();
            lblTotalMovimentacoes.Text = "";
            lblTotalMovimentacoes.Text = "Não há dados!";
        }
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        if (Data1.Data != "")
        {
            try
            {
                Relatorio();
            }
            finally
            {
                Session["ctrl"] = Panel1;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                    "<script language=javascript>window.open('Imprimir.aspx','Imprimir','height=900px,width=1600px,scrollbars=1');</script>");
            }
        }
        else
        {
            Grade.DataSource = new DataTable();
            Grade.DataBind();
            lblTotalMovimentacoes.Text = "Data inválida ou formato incorreto!";
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

        string NomeArq = "Relatorio_ConferenciaDiaria.xls";
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