using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class RelatorioVendasPorRepresentantePorPeriodo : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios) Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "43");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("~/forms/sempermissao.aspx");

        if (oUsuario == null)
        {
            Response.Redirect("brooks/loginaplicativo.aspx", true);
        }
        if (!IsPostBack)
        {
            string _ultimodiamesanterior = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year)).AddDays(-1).ToString("dd/MM/yyyy");
            Data1.Data = Convert.ToDateTime(("01/" + Convert.ToDateTime(_ultimodiamesanterior).Month.ToString() + "/" +
                                                     Convert.ToDateTime(_ultimodiamesanterior).Year.ToString())).ToString("dd/MM/yyyy");
            Data2.Data = Convert.ToDateTime(_ultimodiamesanterior).ToString("dd/MM/yyyy");
        }
    }
    private void Relatorio(string pOrdem, bool pSoFatura, bool pSoISSPF)
    {
        
        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();

        lblTituloRelatorio.Text = "Relatório de Vendas por Representante no Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + "&nbsp;Emissão: " + DateTime.Now.ToString("dd/MM/yy");

        _table = new Table();
        _row = new TableRow();
        _cell = new TableCell();
        _cell.BorderWidth = 0;
        clsNotasFiscaisDados oNFDados = new clsNotasFiscaisDados();
        _dt = oNFDados.PegaVendasPorRepresentante(Data1.Data, Data2.Data, intCodigoRepresentante.Valor);
        
        lblNomeRepresentante.Text = "";
        if (intCodigoRepresentante.Valor != "" && intCodigoRepresentante.Valor != "0" && intCodigoRepresentante.Valor != "&nbsp;")
        {
            if (_dt.Rows.Count > 0)
                lblNomeRepresentante.Text = _dt.Rows[0]["NomeFuncionario"].ToString();
        }
        if (_dt.Rows.Count > 0)
        {
            _table = new Table();          
            _row = new TableRow();
            _cell = new TableCell();

            int[] iColWidth = new int[8];
            iColWidth[1] =   70;  //Emissão
            iColWidth[2] =   80;  //Vencimento
            iColWidth[3] =   80;  //Código Cliente
            iColWidth[4] =  352;  //Cliente
            iColWidth[5] =   96;  //Valor Bruto
            iColWidth[6] =   80;  //Código Funcionário
            iColWidth[7] =  352;  //Nome do Funcionário

            int tWidthContratos = 0;
            foreach (int iTW in iColWidth)
                tWidthContratos = tWidthContratos + iTW;
            _table.Width = tWidthContratos + 10;

            Panel1.Controls.Add(_table);

            decimal _valorTotal = 0;
            string _codigoRepresentanteAnterior = intCodigoRepresentante.Valor;
            DataRow dr;
            if (_dt.Rows.Count > 0)
            {
                _codigoRepresentanteAnterior = _dt.Rows[0]["CodigoFuncionarioComercial"].ToString();
            }
            
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                dr = _dt.Rows[i];
                if (_codigoRepresentanteAnterior != dr["CodigoFuncionarioComercial"].ToString())
                {
                    AddRow(_table, _row, _cell, "─────────", iColWidth[1], false, true, true);
                    AddRow(_table, _row, _cell, "─────────", iColWidth[2], false, true, true);
                    AddRow(_table, _row, _cell, "──────────", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "──────────────────────────────────────────────", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "────────────", iColWidth[5], true, true);
                    AddRow(_table, _row, _cell, "──────────", iColWidth[6], true, true);
                    AddRow(_table, _row, _cell, "──────────────────────────────────────────", iColWidth[7], false, true);

                    AddRow(_table, _row, _cell, "", iColWidth[1], false, true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false, true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "Total vendas", iColWidth[4], true, true);
                    AddRow(_table, _row, _cell, _valorTotal.ToString("N2"), iColWidth[5], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], false, true);

                    AddRow(_table, _row, _cell, "", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[4], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[5], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], false, true);
                    _valorTotal = 0;
                }
                AddDados(_table, _row, _cell, iColWidth, Convert.ToDateTime(dr["DataEmissao"]).ToString("dd/MM/yy"),
                          Convert.ToDateTime(dr["Vencimento"]).ToString("dd/MM/yy"),
                          dr["Codigo"].ToString(), dr["Cliente"].ToString(), Convert.ToDecimal(dr["ValorTotal"]).ToString("N2"),
                          dr["CodigoFuncionarioComercial"].ToString(), dr["NomeFuncionario"].ToString()
                         );
                if (dr["ValorTotal"].ToString() != "" && dr["ValorTotal"].ToString() != "&nbsp;" && dr["ValorTotal"].ToString() != "0,00")
                    _valorTotal = _valorTotal + Convert.ToDecimal(dr["ValorTotal"]);

                _codigoRepresentanteAnterior = dr["CodigoFuncionarioComercial"].ToString();
            }
            AddRow(_table, _row, _cell, "─────────", iColWidth[1], false, true, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[2], false, true, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "──────────────────────────────────────────────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "────────────", iColWidth[5], true, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[6], true, true);
            AddRow(_table, _row, _cell, "──────────────────────────────────────────", iColWidth[7], false, true);

            AddRow(_table, _row, _cell, "", iColWidth[1], false, true, true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false, true, true);
            AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "Total vendas", iColWidth[4], true, true);
            AddRow(_table, _row, _cell, _valorTotal.ToString("N2"), iColWidth[5], true, true);
            AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
            AddRow(_table, _row, _cell, "", iColWidth[7], false, true);
            Panel1.Controls.Add(_table);
        }
    }
    private void AddDados(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, string pDataEmissao, string pVencimento, 
                          string pCodigoCliente, string pNomeCliente, string pValorTotal, string pCodigoRepresentante, 
                          string pNomeRepresentante)
    {
        bool bNegrito = false;
        string sColor = "";
        AddRow(_table, _row, _cell, pDataEmissao, iColWidth[1], true, bNegrito, true, sColor);
        AddRow(_table, _row, _cell, pVencimento, iColWidth[2], false, bNegrito, true, sColor);
        AddRow(_table, _row, _cell, pCodigoCliente, iColWidth[3], true, bNegrito, false, sColor);
        AddRow(_table, _row, _cell, pNomeCliente, iColWidth[4], false, bNegrito, false, sColor);
        if (pValorTotal == "0,00")
            AddRow(_table, _row, _cell, "", iColWidth[5], true, bNegrito, false, sColor);
        else
            AddRow(_table, _row, _cell, pValorTotal, iColWidth[5], true, bNegrito, false, sColor);
        AddRow(_table, _row, _cell, pCodigoRepresentante, iColWidth[3], true, bNegrito, false, sColor);
        AddRow(_table, _row, _cell, pNomeRepresentante, iColWidth[4], false, bNegrito, false, sColor);
    }
    private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, bool pAlinCentral = false, string pColor = "")
    {
        Label _lbl = new Label();
        _lbl.Text = pText + "&nbsp;";
        _lbl.Width = pWidth;
        _lbl.Font.Name = "Tahoma";
        _lbl.Font.Size = 8;
        _lbl.Attributes.CssStyle.Add("margin-top", "0");
        _lbl.Attributes.CssStyle.Add("margin-bottom", "0");
        if (pColor != "")
            _lbl.BackColor = System.Drawing.ColorTranslator.FromHtml(pColor);
        if (pNegrito)
        {
            _lbl.Font.Bold = true;
            //cell.Attributes.CssStyle.Add("border", "1px solid black");
        }
        if (pAlinDireita)
            _lbl.Attributes.CssStyle.Add("text-align", "right");

        if (pAlinCentral)
            _lbl.Attributes.CssStyle.Add("text-align", "center");

        cell.Attributes.CssStyle.Add("style", "height: 9px");
        cell.Height = 9;

        cell.Controls.Add(_lbl);
        row.Cells.Add(cell);

        _table.BorderWidth = 0;

        _table.Rows.Add(row);

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Relatorio("DataEmissao asc", false, false);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }

    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        Label lblColuna = new Label();
        Table table = new Table();
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);

        Label lblEmpresa = new Label();
        Label lblTitulo = new Label();
        Label lblEmBranco = new Label();
        lblColuna = new Label();

        lblTitulo.ID = "lblTitulo";
        lblEmBranco.ID = "lblEmBranco";

        GridView Grade = new GridView();

        string NomeArq = "Relatorio_Vendas_Representante.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;

        clsNotasFiscaisDados oNFDados = new clsNotasFiscaisDados();
        _dt = oNFDados.PegaVendasPorRepresentante(Data1.Data, Data2.Data, intCodigoRepresentante.Valor);

        lblNomeRepresentante.Text = "";
        if (intCodigoRepresentante.Valor != "" && intCodigoRepresentante.Valor != "0" && intCodigoRepresentante.Valor != "&nbsp;")
        {
            if (_dt.Rows.Count > 0)
                lblNomeRepresentante.Text = _dt.Rows[0]["NomeFuncionario"].ToString();
        }
        _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        Grade.DataSource = _dt;       
        Grade.DataBind();

        if (_dt.Rows.Count > 0)
        {
            for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);

            foreach (GridViewRow gvr in Grade.Rows)
            {
                if (gvr.Cells[0].Text != "" && gvr.Cells[0].Text != "&nbsp;")
                    gvr.Cells[0].Text = DateTime.Parse(gvr.Cells[0].Text).ToShortDateString();
                if (gvr.Cells[1].Text != "" && gvr.Cells[1].Text != "&nbsp;")
                    gvr.Cells[1].Text = DateTime.Parse(gvr.Cells[1].Text).ToShortDateString();
            }
            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmpresa.ID = "lblEmpresa";
            lblEmpresa.Text = geral.NomeEmpresa(1);
            row.Cells[0].Controls.Add(lblEmpresa);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblTitulo.ID = "lblEmBranco";
            lblTitulo.Text = "Relatorio de Vendas por Representante -   Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + 
                                                                             Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + " - Data emissao: " + DateTime.Now.ToShortDateString();
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);
            
            Grade.HeaderRow.Cells[0].Text = "Emissao";
            Grade.HeaderRow.Cells[1].Text = "Vencimento";
            Grade.HeaderRow.Cells[2].Text = "Codigo";
            Grade.HeaderRow.Cells[3].Text = "Cliente";
            Grade.HeaderRow.Cells[4].Text = "Valor Bruto";
            Grade.HeaderRow.Cells[5].Text = "Codigo";
            Grade.HeaderRow.Cells[6].Width = Unit.Parse("400px"); // no open office nada aconteceu.
            Grade.HeaderRow.Cells[6].Text = "Representante";

            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade);
            table.Rows.Add(row);

        }
        table.RenderControl(hw);
        HttpContext.Current.Response.Write(hw.InnerWriter);
        HttpContext.Current.Response.End();
    }
}