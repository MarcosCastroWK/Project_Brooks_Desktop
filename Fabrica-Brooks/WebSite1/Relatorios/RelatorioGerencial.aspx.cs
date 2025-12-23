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

public partial class RelatorioGerencial : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();

    clsResiduos oResiduos = new clsResiduos();
    clsResiduoDados oResiduosDados = new clsResiduoDados();

    clsClientes oCliente = new clsClientes();
    clsClienteDados oClienteDados = new clsClienteDados();

    DataTable _dt = new DataTable();
    DataTable _dtResiduos = new DataTable();
    DataTable _dtClientes = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "41");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("~/forms/sempermissao.aspx");

        if (oUsuario == null)
        {
            Response.Redirect("brooks/loginaplicativo.aspx", true);
        }
        if (!IsPostBack)
        {
            intDiaInicial.Valor = "01";
            intDiaFinal.Valor = "31";
            intAno.Valor = DateTime.Now.Year.ToString();
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
        }
    }

    private void Relatorio()
    {

        if (CLIENTESCONTROL1.Valor == "")
            CLIENTESCONTROL1.Valor = "0";

        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        AddRow(_table, _row, _cell, "Relatório Gerencial", 250, false, true);
        AddRow(_table, _row, _cell, "Período: " + intDiaInicial.Valor + " a " + intDiaFinal.Valor, 200, false, true);
        AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToShortDateString(), 170, true, true);
        Panel1.Controls.Add(_table);

        string sInClientes = "";
        if (Session["RelClientes"] != null)
        {
            _dtClientes = (DataTable)Session["RelClientes"];
            foreach (DataRow _drClientes in _dtClientes.Rows)
                sInClientes = sInClientes + ", " + _drClientes["Codigo"].ToString();
            if (sInClientes.Length > 0)
                sInClientes = sInClientes.Substring(1);
        }

        string sInResiduos = "";
        if (Session["Residuos"] != null)
        {
            _dtResiduos = (DataTable)Session["Residuos"];
            foreach (DataRow _drRes in _dtResiduos.Rows)
                sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
            if (sInResiduos.Length > 0)
                sInResiduos = sInResiduos.Substring(1);
        }

        _table = new Table();
        _row = new TableRow();
        _cell = new TableCell();

        int[] iColWidth = new int[7];

        int iCount = 12;

        iColWidth = new int[3 + iCount];
        iColWidth[1] =  56;  //Tipo de Resíduo
        iColWidth[2] = 496;  //Subgrupo Resíduo
        iColWidth[3] =  80;  //Meses

        int tWidthContratos = 0;
        foreach (int iTW in iColWidth)
            tWidthContratos = tWidthContratos + iTW;
        _table.Width = tWidthContratos + ((iCount + 2) * 77);

        AddRow(_table, _row, _cell, "Código", iColWidth[1], false, true);
        AddRow(_table, _row, _cell, "Descrição do Resíduo", iColWidth[2], false, true);
        AddRow(_table, _row, _cell, "Janeiro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Fevereiro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Março", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Abril", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Maio", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Junho", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Junho", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Agosto", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Setembro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Outubro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Novembro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Desembro", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Total", iColWidth[3], false, true, true);
        AddRow(_table, _row, _cell, "Média(" + (12 - (13 - DateTime.Now.Month)).ToString() + ")", iColWidth[3], false, true, true);

        AddRow(_table, _row, _cell, "───────", iColWidth[1], false, true);
        AddRow(_table, _row, _cell, "─────────────────────────────────────────────────────────────────", iColWidth[2], false, true);
        for (int x = 0; x <= iCount+1; x++)
            AddRow(_table, _row, _cell, "──────────", iColWidth[3], false, true);

        Panel1.Controls.Add(_table);

        string _TipoRelatorio = "Mensal";

        string _Data1 = intDiaInicial.Valor + "/01/" + intAno.Valor;
        string _Data2 = intDiaFinal.Valor + "/12/" + intAno.Valor;
        _dt = oLancamentoDados.PegaTotalResiduos(_TipoRelatorio, _Data1, _Data2, sInClientes, sInResiduos, false, "", "", "Codigo, Unidade");
        foreach (DataRow _dr1 in _dt.Rows)
        {
            if (_dr1["Unidade"].ToString().ToUpper().IndexOf("M³") > -1)
                _dr1["Unidade"] = "M3";
            if (_dr1["Unidade"].ToString().ToLower().IndexOf("kg") > -1)
                _dr1["Unidade"] = "KG";
        }
        if (_dt.Rows.Count > 0)
        {
            decimal _QtTotal = 0;
            decimal[] _qtTotalColunas = new decimal[12];
            string _CodigoResiduoUnidadeAnterior = "";
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr["Codigo"].ToString() == "44")
                    _QtTotal = 0;
                if (_CodigoResiduoUnidadeAnterior != (dr["Codigo"].ToString() + dr["Unidade"].ToString().ToUpper()))
                {
                    _QtTotal = 0;
                    AddRow(_table, _row, _cell, dr["Codigo"].ToString(), iColWidth[1], true);
                    AddRow(_table, _row, _cell, dr["GrupoNome"].ToString() + "/" + dr["DescricaoReduzida"].ToString() + "(" + dr["Unidade"].ToString() + ")", iColWidth[2], false);
                    for (int x = 0; x < iCount; x++)
                    {
                        DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(_Data1).AddMonths(x).ToString("yyyy") + "' and " +
                                                   "Mes = '" + Convert.ToDateTime(_Data1).AddMonths(x).ToString("MM") + "' and " +
                                                   "Codigo = " + dr["Codigo"].ToString() + " and " + 
                                                   "Unidade = '" + dr["Unidade"].ToString() + "'");
                        if (ddr.Length > 0)
                        {
                            if (Convert.ToDateTime(_Data1).AddMonths(x).Year == Convert.ToInt32(ddr[0]["Ano"]) &&
                                Convert.ToDateTime(_Data1).AddMonths(x).Month == Convert.ToInt32(ddr[0]["Mes"]))
                            {
                                _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                _qtTotalColunas[x] = _qtTotalColunas[x] + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                AddRow(_table, _row, _cell, Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2"), iColWidth[3], true);
                            }
                            else
                                AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[3], true);
                        }
                        else
                            AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[3], true);
                    }
                    AddRow(_table, _row, _cell, _QtTotal.ToString("N2"), iColWidth[3], true);
                    if (DateTime.Now.Year == Convert.ToDateTime(_Data1).Year)
                        if ( 12 - (13 - DateTime.Now.Month) > 0)
                            AddRow(_table, _row, _cell, (_QtTotal / (12 - (13 - DateTime.Now.Month))).ToString("N2"), iColWidth[3], true);
                        else
                            AddRow(_table, _row, _cell, _QtTotal.ToString("N2"), iColWidth[3], true);
                    else
                        AddRow(_table, _row, _cell, (_QtTotal / 12).ToString("N2"), iColWidth[3], true);
                }
                _CodigoResiduoUnidadeAnterior = dr["Codigo"].ToString() + dr["Unidade"].ToString().ToUpper();
                 
            }
            AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "TOTAL GERAL", iColWidth[2], false, true);

            _QtTotal = 0;
            for (int x = 0; x < iCount; x++)
            {
                _QtTotal = _QtTotal + _qtTotalColunas[x];
                AddRow(_table, _row, _cell, _qtTotalColunas[x].ToString("N2"), iColWidth[3], true, true);
            }
            AddRow(_table, _row, _cell, _QtTotal.ToString("N2"), iColWidth[3], true, true);
            if (DateTime.Now.Year == Convert.ToDateTime(_Data1).Year)
                if (12 - (13 - DateTime.Now.Month) > 0)
                    AddRow(_table, _row, _cell, (_QtTotal / (12 - (13 - DateTime.Now.Month))).ToString("N2"), iColWidth[3], true);
                else
                    AddRow(_table, _row, _cell, _QtTotal.ToString("N2"), iColWidth[3], true);
            else
                AddRow(_table, _row, _cell, (_QtTotal / 12).ToString("N2"), iColWidth[3], true);
            Panel1.Controls.Add(_table);
        }
    }

    private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, bool pAlinCenter = false)
    {
        Label _lbl = new Label();
        _lbl.Text = pText + "&nbsp;";
        _lbl.Width = pWidth;
        _lbl.Font.Name = "Tahoma";
        _lbl.Font.Size = 8;
        _lbl.Attributes.CssStyle.Add("margin-top", "0");

        if (pNegrito)
        {
            _lbl.Font.Bold = true;
            //cell.Attributes.CssStyle.Add("border", "1px solid black");
        }
        if (pAlinDireita)
            _lbl.Attributes.CssStyle.Add("text-align", "right");
        else if (pAlinCenter)
            _lbl.Attributes.CssStyle.Add("text-align", "center");

        cell.Controls.Add(_lbl);
        row.Cells.Add(cell);

        _table.BorderWidth = 0;
        _table.Rows.Add(row);

    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Relatorio();
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (Session["Residuos"] != null)
            _dtResiduos = (DataTable)Session["Residuos"];
        else
        {
            _dtResiduos = new DataTable();
            _dtResiduos.Columns.Add("Codigo");
            _dtResiduos.Columns.Add("NomeResiduo");
        }
        if (GRUPORESIDUO1.Valor != "")
        {
            bool _bEncontrado = false;
            foreach (DataRow _drRes in _dtResiduos.Rows)
            {
                if (_drRes["Codigo"].ToString() == GRUPORESIDUO1.Valor)
                    _bEncontrado = true;
            }
            if (!_bEncontrado)
            {
                oResiduos = new clsResiduos();
                oResiduosDados.PegaDados(oResiduos, Convert.ToInt32(GRUPORESIDUO1.Valor));
                if (oResiduos.Codigo > 0)
                {
                    GRUPORESIDUO1.Texto = oResiduos.DescricaoReduzida;
                    DataRow _dr = _dtResiduos.NewRow();
                    _dr[0] = oResiduos.Codigo;
                    _dr[1] = oResiduos.DescricaoReduzida;
                    _dtResiduos.Rows.Add(_dr);
                    Session["Residuos"] = _dtResiduos;
                    GradeResiduos.DataSource = _dtResiduos;
                    GradeResiduos.DataBind();
                }
            }
        }
    }

    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        //
    }

    protected void GradeResiduos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeResiduo")
        {
            if (e.CommandArgument.ToString() != "")
            {
                if (Session["Residuos"] != null)
                {
                    _dtResiduos = (DataTable)Session["Residuos"];
                    if (_dtResiduos.Rows.Count > 0)
                    {
                        _dtResiduos.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                        Session["Residuos"] = _dtResiduos;
                        GradeResiduos.DataSource = _dtResiduos;
                        GradeResiduos.DataBind();
                    }
                }
            }
        }
    }

    protected void GradeClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeCliente")
        {
            if (e.CommandArgument.ToString() != "")
            {
                if (Session["RelClientes"] != null)
                {
                    _dtClientes = (DataTable)Session["RelClientes"];
                    if (_dtClientes.Rows.Count > 0)
                    {
                        _dtClientes.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                        Session["RelClientes"] = _dtClientes;
                        GradeClientes.DataSource = _dtClientes;
                        GradeClientes.DataBind();
                    }
                }
            }
        }
    }

    protected void btnAdicionarCliente_Click(object sender, EventArgs e)
    {
        if (Session["RelClientes"] != null)
            _dtClientes = (DataTable)Session["RelClientes"];
        else
        {
            _dtClientes = new DataTable();
            _dtClientes.Columns.Add("Codigo");
            _dtClientes.Columns.Add("NomeCliente");
        }
        if (CLIENTESCONTROL1.Valor != "")
        {
            bool _bEncontrado = false;
            foreach (DataRow _drCliente in _dtClientes.Rows)
            {
                if (_drCliente["Codigo"].ToString() == CLIENTESCONTROL1.Valor)
                    _bEncontrado = true;
            }
            if (!_bEncontrado)
            {
                oCliente = new clsClientes();
                oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));
                if (oCliente.Codigo > 0)
                {
                    CLIENTESCONTROL1.Texto = oCliente.NomeFantasia;
                    DataRow _dr = _dtClientes.NewRow();
                    _dr[0] = oCliente.Codigo;
                    _dr[1] = oCliente.NomeFantasia;
                    _dtClientes.Rows.Add(_dr);
                    Session["RelClientes"] = _dtClientes;
                    GradeClientes.DataSource = _dtClientes;
                    GradeClientes.DataBind();
                }
            }
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["RelClientes"] = null;
        Session["Residuos"] = null;
        CLIENTESCONTROL1.Valor = "";
        CLIENTESCONTROL1.Texto = "";
        GRUPORESIDUO1.Valor = "";
        GRUPORESIDUO1.Texto = "";
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

        lblTitulo.ID = "lblTitulo";
        lblEmBranco.ID = "lblEmBranco";

        GridView Grade = new GridView();

        string NomeArq = "Relatorio_Gerencial.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;

        string sInClientes = "";
        if (Session["RelClientes"] != null)
        {
            _dtClientes = (DataTable)Session["RelClientes"];
            foreach (DataRow _drClientes in _dtClientes.Rows)
                sInClientes = sInClientes + ", " + _drClientes["Codigo"].ToString();
            if (sInClientes.Length > 0)
                sInClientes = sInClientes.Substring(1);
        }

        string sInResiduos = "";
        if (Session["Residuos"] != null)
        {
            _dtResiduos = (DataTable)Session["Residuos"];
            foreach (DataRow _drRes in _dtResiduos.Rows)
                sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
            if (sInResiduos.Length > 0)
                sInResiduos = sInResiduos.Substring(1);
        }

        string _TipoRelatorio = "Mensal";
        string _Data1 = intDiaInicial.Valor + "/01/" + intAno.Valor;
        string _Data2 = intDiaFinal.Valor + "/12/" + intAno.Valor;
        _dt = oLancamentoDados.PegaTotalResiduos(_TipoRelatorio, _Data1, _Data2, sInClientes, sInResiduos, false, "", "", "Codigo, Unidade");
        foreach (DataRow _dr1 in _dt.Rows)
        {
            if (_dr1["Unidade"].ToString().ToUpper().IndexOf("M³") > -1)
                _dr1["Unidade"] = "M3";
            if (_dr1["Unidade"].ToString().ToLower().IndexOf("kg") > -1)
                _dr1["Unidade"] = "KG";
        }
        DataTable dtGrade = new DataTable();
        if (_dt.Rows.Count > 0)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = "Codigo";
            dtGrade.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Descricao";
            dtGrade.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Janeiro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Fevereiro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Marco";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Abril";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Maio";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Junho";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Julho";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Agosto";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Setembro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Outubro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Novembro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Dezembro";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Total";
            dtGrade.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Media";
            dtGrade.Columns.Add(dc);

            decimal _QtTotal = 0;
            decimal[] _qtTotalColunas = new decimal[12];
            string _CodigoResiduoUnidadeAnterior = "";
            int iCount = 12;
            DataRow drGrade;
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr["Codigo"].ToString() == "44")
                    _QtTotal = 0;
                if (_CodigoResiduoUnidadeAnterior != (dr["Codigo"].ToString() + dr["Unidade"].ToString().ToUpper()))
                {
                    _QtTotal = 0;
                    drGrade = dtGrade.NewRow();
                    drGrade[0] = dr["Codigo"].ToString();
                    drGrade[1] = dr["GrupoNome"].ToString() + "/" + dr["DescricaoReduzida"].ToString() + "(" + dr["Unidade"].ToString() + ")";
                    for (int x = 0; x < iCount; x++)
                    {
                        DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(_Data1).AddMonths(x).ToString("yyyy") + "' and " +
                                                   "Mes = '" + Convert.ToDateTime(_Data1).AddMonths(x).ToString("MM") + "' and " +
                                                   "Codigo = " + dr["Codigo"].ToString() + " and " +
                                                   "Unidade = '" + dr["Unidade"].ToString() + "'");
                        if (ddr.Length > 0)
                        {
                            if (Convert.ToDateTime(_Data1).AddMonths(x).Year == Convert.ToInt32(ddr[0]["Ano"]) &&
                                Convert.ToDateTime(_Data1).AddMonths(x).Month == Convert.ToInt32(ddr[0]["Mes"]))
                            {
                                _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                _qtTotalColunas[x] = _qtTotalColunas[x] + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                drGrade[x + 2] = Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2");                                
                            }
                            else
                                drGrade[x + 2] = Convert.ToDecimal(0).ToString("N2");
                        }
                        else
                            drGrade[x + 2] = Convert.ToDecimal(0).ToString("N2");

                    }
                    drGrade[14] = _QtTotal.ToString("N2");
                    if (DateTime.Now.Year == Convert.ToDateTime(_Data1).Year)
                        if (12 - (13 - DateTime.Now.Month) > 0)
                            drGrade[15] = (_QtTotal / (12 - (13 - DateTime.Now.Month))).ToString("N2");
                        else
                            drGrade[15] = _QtTotal.ToString("N2");
                    else
                        drGrade[15] = (_QtTotal / 12).ToString("N2");

                    dtGrade.Rows.Add(drGrade);
                }

                _CodigoResiduoUnidadeAnterior = dr["Codigo"].ToString() + dr["Unidade"].ToString().ToUpper();

            }

            drGrade = dtGrade.NewRow();
            drGrade[0] = "";
            drGrade[1] = "TOTAL GERAL";
            _QtTotal = 0;
            for (int x = 0; x < iCount; x++)
            {
                _QtTotal = _QtTotal + _qtTotalColunas[x];
                drGrade[x + 2] = _qtTotalColunas[x].ToString("N2");
            }
            drGrade[14] = _QtTotal.ToString("N2");
            if (DateTime.Now.Year == Convert.ToDateTime(_Data1).Year)
                if (12 - (13 - DateTime.Now.Month) > 0)
                    drGrade[15] = (_QtTotal / (12 - (13 - DateTime.Now.Month))).ToString("N2");
                else
                    drGrade[15] = _QtTotal.ToString("N2");
            else
                drGrade[15] = (_QtTotal / 12).ToString("N2");
            dtGrade.Rows.Add(drGrade);

            Grade.DataSource = dtGrade;
            Grade.DataBind();

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
            lblTitulo.Text = "Relatorio Gerencial - Periodo: " + intDiaInicial.Valor + " a " + intDiaFinal.Valor + " - Ano: " + intAno.Valor + 
                             " - Emissao: " + DateTime.Now.ToString("dd/MM/yyyy");
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

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