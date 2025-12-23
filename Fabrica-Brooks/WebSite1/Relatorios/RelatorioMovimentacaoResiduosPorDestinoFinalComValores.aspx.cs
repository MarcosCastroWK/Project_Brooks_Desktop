using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class RelatorioMovimentacaoResiduosPorDestinoFinalComValores : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
    clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
    clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();

    DataTable _dt = new DataTable();
    System.Text.StringBuilder _sb = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "47");
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
            Data2.Data = Convert.ToDateTime(_ultimodiamesanterior).ToString("dd/MM/yyyy"); ;
            DESTINOFINAL1.Valor = "";
            DESTINOFINAL1.Texto = "";
        }
    }

    private void Relatorio()
    {
        
        if (DESTINOFINAL1.Valor == "")
            DESTINOFINAL1.Valor = "0";

        oDestinoFinal.Codigo = Convert.ToInt32(DESTINOFINAL1.Valor);
        oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));

        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();

        AddRow(_table, _row, _cell, "Relatório de movimentação de resíduos por destino final com valores - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 860, false, true);
        AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToString("dd/MM/yy"), 100, true);
        Panel1.Controls.Add(_table);

        oDestinoFinalDados = new clsDestinoFinalDados();
        oDestinoFinal = new clsDestinoFinal();
        oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));

        _table = new Table();
        _row = new TableRow();
        _cell = new TableCell();
        _cell.BorderWidth = 0;
        AddRow(_table, _row, _cell, "Destinador: " + "<br />", 63, false, true);
        AddRow(_table, _row, _cell, oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ")<br />", 860, false, true);
        Panel1.Controls.Add(_table);

        if (oDestinoFinal.Codigo > 0)
        {
            clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor));
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo);
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();
                _row = new TableRow();
                _cell = new TableCell();

                int[] iColWidth = new int[13];
                iColWidth[1] =   50;  //DataRetirada
                iColWidth[2] =   50;  //Código Cliente  
                iColWidth[3] =  200;  //Nome Cliente
                iColWidth[4] =   26;  //Unidade
                iColWidth[5] =   70;  //QtColeta
                iColWidth[6] =   70;  //QtDescarga
                iColWidth[7] =  200;  //Resíduo
                iColWidth[8] =  120;  //Nº Ticket
                iColWidth[9] =   70;  //Vl.Unitario
                iColWidth[10] =  70;  //Vl.Total
                iColWidth[11] =  66;  //Nº MTR-e
                iColWidth[12] =  70;  //Total (Kg) MTRe

                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;
                AddRow(_table, _row, _cell, "Data", iColWidth[1], false, true, true);
                AddRow(_table, _row, _cell, "Código", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "Cliente", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "Und", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "Qt.Coleta", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "Qt.Descarga", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "Resíduo", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "Nº Ticket", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "Vl.Unitário", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "Vl.Total", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "Nº MTR-e", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "Tl(Kg)MTRe", iColWidth[12], false, true);

                AddRow(_table, _row, _cell, "──────", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "──────", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "──────────────────────────", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "───", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "──────────────────────────", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "───────────────", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "────────", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[12], false, false);
                Panel1.Controls.Add(_table);

                decimal _qtTl = 0;
                decimal _qtTlDescarga = 0;
                decimal _vlSbTl = 0;
                decimal _valorTotalGeral = 0;
                decimal _valorTotalPorMTRe = 0;
                string _CNPJClienteAnterior = "";
                string _MTReAnterior = "";
                if (_dt.Rows.Count > 0)
                {
                    _CNPJClienteAnterior = _dt.Rows[0]["CNPJ_CPF"].ToString();
                    _MTReAnterior = _dt.Rows[0]["MTRe"].ToString();
                }
                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    DataRow dr = _dt.Rows[i];
                    if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                    {
                        // adicionar linha com subtotais
                        AddRow(_table, _row, _cell, "", iColWidth[1], true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        AddRow(_table, _row, _cell, "", iColWidth[4], false);
                        AddRow(_table, _row, _cell, "", iColWidth[5], true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], true);
                        AddRow(_table, _row, _cell, "", iColWidth[7], true);
                        AddRow(_table, _row, _cell, "Total p/cliente", iColWidth[8], true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], true);
                        AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[10], true);
                        AddRow(_table, _row, _cell, "", iColWidth[11], true);
                        AddRow(_table, _row, _cell, "", iColWidth[12], false);

                        _vlSbTl = 0;
                        AddRow(_table, _row, _cell, "", iColWidth[1], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[4], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[5], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
                        AddRow(_table, _row, _cell, "", iColWidth[7], true, true);
                        AddRow(_table, _row, _cell, "", iColWidth[8], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[10], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[11], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[12], false, true);

                    }
                    _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();                    

                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataDescarga"]).ToString("dd/MM/yy"), iColWidth[1], true);
                    AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[2], true);
                    AddRow(_table, _row, _cell, geral.Left(dr["Nome"].ToString(), 30), iColWidth[3], false);
                    AddRow(_table, _row, _cell, dr["Unidade"].ToString(), iColWidth[4], false);
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["Quantidade"]).ToString("N2"), iColWidth[5], true);
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[6], true);
                    AddRow(_table, _row, _cell, geral.Left(dr["DescricaoReduzida"].ToString(), 22), iColWidth[7], false);
                    AddRow(_table, _row, _cell, dr["Ticket"].ToString(), iColWidth[8], false);
                    if (dr["ValorUnitario"].ToString() != "")
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["ValorUnitario"]).ToString("N2"), iColWidth[9], true);
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[9], true);

                    if (Convert.ToDecimal(dr["QtDescarga"]).ToString() != "")
                        _valorTotalPorMTRe = _valorTotalPorMTRe + Convert.ToDecimal(dr["QtDescarga"]);

                    // Valor Total
                    if (dr["ValorUnitario"].ToString() != "" && dr["QtDescarga"].ToString() != "")
                    {
                        AddRow(_table, _row, _cell, (Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"])).ToString("N2"), iColWidth[10], true);
                        _qtTl = _qtTl + Convert.ToDecimal(dr["Quantidade"]);
                        _qtTlDescarga = _qtTlDescarga + Convert.ToDecimal(dr["QtDescarga"]);
                        _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"]);
                        _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"]);
                    }
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[10], true);

                    AddRow(_table, _row, _cell, dr["MTRe"].ToString(), iColWidth[11], false);

                    _MTReAnterior = dr["MTRe"].ToString();

                    if (_dt.Rows.Count - 1 == 0 && i == 0)
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[12], true);
                    else if (_dt.Rows.Count - 1 > 0 && i == 0)
                    {
                        if (_dt.Rows[1]["MTRe"].ToString() != _MTReAnterior)
                        {
                            AddRow(_table, _row, _cell, _valorTotalPorMTRe.ToString("N2"), iColWidth[12], true);
                            _valorTotalPorMTRe = 0;
                        }
                        else if (_dt.Rows[1]["MTRe"].ToString() == _MTReAnterior)
                            AddRow(_table, _row, _cell, "", iColWidth[12], true);
                    }
                    else if (_dt.Rows.Count - 1 > 0 && i > 0)
                    {
                        if (_dt.Rows.Count - 1 >= i + 1)
                        {
                            if (_dt.Rows[i + 1]["MTRe"].ToString() != _MTReAnterior)
                            {
                                AddRow(_table, _row, _cell, _valorTotalPorMTRe.ToString("N2"), iColWidth[12], true);
                                _valorTotalPorMTRe = 0;
                            }
                            else if (_dt.Rows[i + 1]["MTRe"].ToString() == _MTReAnterior)
                                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                        }
                        else
                            AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[12], true);

                    }
                    else
                        AddRow(_table, _row, _cell, "", iColWidth[12], true);



                }

                // adicionar linha com subtotais
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], false);
                AddRow(_table, _row, _cell, "", iColWidth[6], true);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "Total p/cliente", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[10], true);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);

                // adicionar linha com subtotais
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, _qtTl.ToString("N2"), iColWidth[5], true);
                AddRow(_table, _row, _cell, _qtTlDescarga.ToString("N2"), iColWidth[6], true);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "Total geral no período", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[10], true);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], false);

                Panel1.Controls.Add(_table);
            }
        }
    }

    private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, bool pAlinCentral = false)
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

        if (pAlinCentral)
            _lbl.Attributes.CssStyle.Add("text-align", "center");

        cell.Controls.Add(_lbl);
        row.Cells.Add(cell);

        _table.BorderWidth = 0;
        _table.Rows.Add(row);

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Relatorio();
    }

    protected void btnEnviarEmail_Click(object sender, EventArgs e)
    {
        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        if (DESTINOFINAL1.Valor != "")
            AddRow(_table, _row, _cell, "Confirme o envio de e-mail para o fornecedor selecionado.", 600, false);
        else
            AddRow(_table, _row, _cell, "Confirme o envio de e-mail para todos fornecedores do cadastro.", 600, false);
        Panel1.BorderWidth = 0;
        Panel1.Controls.Add(_table);
        btnConfirmar.Visible = true;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        DESTINOFINAL1.Valor = "";
        DESTINOFINAL1.Texto = "";
        CLIENTE1.Valor = "";
        CLIENTE1.Texto = "";
        Session["Clientes"] = null;
        Session["DestinoFinal"] = null;
        string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }

    private void Criar_Enviar_Relatorio()
    {
        int _pagina = 1;
        var document = new PdfSharp.Pdf.PdfDocument();
        var page = document.AddPage();
        page.Orientation = PdfSharp.PageOrientation.Landscape;
        var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
        var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
        var font = new PdfSharp.Drawing.XFont("Courier New", 6.5);

        _sb = new System.Text.StringBuilder();

        oDestinoFinalDados = new clsDestinoFinalDados();
        oDestinoFinal = new clsDestinoFinal();
        if (DESTINOFINAL1.Valor != "")
            oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));

        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        if (CLIENTE1.Valor != "")
            _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor));
        else
            _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo);
        
        // só manda se houver registros
        if (_dt.Rows.Count > 0)
        {
            Cabecalho(_sb, _pagina);
            textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, 20, page.Width - 20, page.Height - 60));

            decimal _qtTl = 0;
            decimal _qtTlDescarga = 0;
            decimal _vlSubTotal = 0;
            decimal _valorTotalGeral = 0;
            decimal _valorTotalPorMTRe = 0;
            string _CNPJClienteAnterior = "";
            int _ln = 60;
            string _MTReAnterior = "";
            if (_dt.Rows.Count > 0)
            {
                _CNPJClienteAnterior = _dt.Rows[0]["CNPJ_CPF"].ToString();
                _MTReAnterior = _dt.Rows[0]["MTRe"].ToString();
            }
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];

                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                {
                    // adicionar linha com subtotais
                    _sb.Clear();
                    _sb.Append(" ".PadLeft(108) + "Total p/Cliente      " + " ".PadLeft(12 - geral.Left(_vlSubTotal.ToString("N2"), 11).Length) + _vlSubTotal.ToString("N2") + " \n");
                    _ln = _ln + 8;
                    textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, _ln, page.Width - 20, page.Height + 60));
                }
                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

                _sb.Clear();
                _sb.Append(Convert.ToDateTime(dr["DataDescarga"]).ToString("dd/MM/yy"));
                _sb.Append(" " + Convert.ToInt32(dr["CodigoCliente"]).ToString("000000"));
                _sb.Append(" " + geral.Left(dr["Nome"].ToString(), 30) + " ".PadLeft(31 - geral.Left(dr["Nome"].ToString(), 30).Length));
                _sb.Append(dr["Unidade"].ToString() + " ".PadLeft(4 - geral.Left(dr["Unidade"].ToString(), 3).Length));
                _sb.Append(" ".PadLeft(12 - Convert.ToDecimal(dr["Quantidade"]).ToString("N2").Length) + Convert.ToDecimal(dr["Quantidade"]).ToString("N2"));
                _sb.Append(" ".PadLeft(12 - Convert.ToDecimal(dr["QtDescarga"]).ToString("N2").Length) + Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"));
                _sb.Append(" " + geral.Left(dr["DescricaoReduzida"].ToString(), 24) + " ".PadLeft(25 - geral.Left(dr["DescricaoReduzida"].ToString(), 24).Length));
                _sb.Append(" " + dr["Ticket"].ToString() + " ".PadLeft(9 - dr["Ticket"].ToString().Length));

                if (dr["ValorUnitario"].ToString() != "")
                    _sb.Append(" ".PadLeft(15 - Convert.ToDecimal(dr["ValorUnitario"]).ToString("N2").Length) + Convert.ToDecimal(dr["ValorUnitario"]).ToString("N2"));
                else
                    _sb.Append(" ".PadLeft(15 - 0.ToString("N2").Length) + 0.ToString("N2"));

                if (Convert.ToDecimal(dr["QtDescarga"]).ToString() != "")
                    _valorTotalPorMTRe = _valorTotalPorMTRe + Convert.ToDecimal(dr["QtDescarga"]);

                // Valor Total
                if (dr["ValorUnitario"].ToString() != "" && dr["QtDescarga"].ToString() != "")
                {
                    _sb.Append(" ".PadLeft(15 - (Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"])).ToString("N2").Length) + (Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"])).ToString("N2"));
                    _vlSubTotal = _vlSubTotal + (Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"]));
                    _qtTl = _qtTl + Convert.ToDecimal(dr["Quantidade"]);
                    _qtTlDescarga = _qtTlDescarga + Convert.ToDecimal(dr["QtDescarga"]);
                    _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["ValorUnitario"]) * Convert.ToDecimal(dr["QtDescarga"]);
                }
                else
                    _sb.Append(" ".PadLeft(15 - 0.ToString("N2").Length) + 0.ToString("N2"));
                _sb.Append(" " + dr["MTRe"].ToString() + " ".PadLeft(12 - dr["MTRe"].ToString().Length));
                _MTReAnterior = dr["MTRe"].ToString();

                if (_dt.Rows.Count - 1 == 0 && i == 0)
                {
                    _sb.Append(" ".PadLeft(14 - Convert.ToDecimal(dr["QtDescarga"]).ToString("N2").Length) + Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"));
                }
                else if (_dt.Rows.Count - 1 > 0 && i == 0)
                {
                    if (_dt.Rows[1]["MTRe"].ToString() != _MTReAnterior)
                    {
                        _sb.Append(" ".PadLeft(14 - _valorTotalPorMTRe.ToString("N2").Length) + _valorTotalPorMTRe.ToString("N2"));
                        _valorTotalPorMTRe = 0;
                    }
                }
                else if (_dt.Rows.Count - 1 > 0 && i > 0)
                {
                    if (_dt.Rows.Count - 1 >= i + 1)
                    {
                        if (_dt.Rows[i + 1]["MTRe"].ToString() != _MTReAnterior)
                        {
                            _sb.Append(" ".PadLeft(14 - _valorTotalPorMTRe.ToString("N2").Length) + _valorTotalPorMTRe.ToString("N2"));
                            _valorTotalPorMTRe = 0;
                        }
                    }
                    else if (_dt.Rows.Count - 1 == i)
                        _sb.Append(" ".PadLeft(14 - _valorTotalPorMTRe.ToString("N2").Length) + _valorTotalPorMTRe.ToString("N2"));
                }

                font = new PdfSharp.Drawing.XFont("Courier New", 6.5);
                _ln = _ln + 8;
                textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, _ln, page.Width - 20, page.Height + 60));
                if (_ln > 500)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                    _ln = 60;
                    font = new PdfSharp.Drawing.XFont("Courier New", 6.5);
                    _sb.Clear();
                    _pagina++;
                    Cabecalho(_sb, _pagina);
                    textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, 20, page.Width - 20, page.Height + 60));
                }

                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                {
                    // adicionar linha com subtotais
                    _sb.Clear();
                    _sb.Append(" ".PadLeft(105) + "Total p/Cliente      " + " ".PadLeft(12 - geral.Left(_vlSubTotal.ToString("N2"), 11).Length) + _vlSubTotal.ToString("N2") + " \n");
                    _vlSubTotal = 0;
                    _ln = _ln + 8;
                    textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, _ln, page.Width - 20, page.Height + 60));
                    _sb.Clear();
                    _ln = _ln + 8;
                    textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, _ln, page.Width - 20, page.Height + 60));
                }
                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

            }
            _sb.Clear();
            // adicionar linha com ultimo subtotal
            _sb.Append(" ".PadLeft(106) + "Total por Cliente      " + " ".PadLeft(12 - geral.Left(_vlSubTotal.ToString("N2"), 11).Length) + _vlSubTotal.ToString("N2") + " \n");
            _sb.Append(" ".PadLeft(53) + _qtTl.ToString("N2") + " ".PadLeft(11 - geral.Left(_qtTl.ToString("N2"), 10).Length));
            _sb.Append(" ".PadLeft(11 - geral.Left(_qtTlDescarga.ToString("N2"), 10).Length) + _qtTlDescarga.ToString("N2"));
            _sb.Append(" ".PadLeft(31) + "Total Geral no período " + " ".PadLeft(12 - geral.Left(_valorTotalGeral.ToString("N2"), 11).Length) + _valorTotalGeral.ToString("N2"));
            _ln = _ln + 8;
            textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(6, _ln, page.Width - 20, page.Height + 60));

            string _sdf = "";
            if (DESTINOFINAL1.Valor != "")
                _sdf = Convert.ToInt32(DESTINOFINAL1.Valor).ToString("0000");
            string _sdtInicioFim = Convert.ToDateTime(Data1.Data).ToString("yyMMdd") + "_" + Convert.ToDateTime(Data2.Data).ToString("yyMMdd");
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            string _dirArqSalva = "";
            try
            {
                if (System.IO.Directory.Exists("w:\\RELATERRO"))
                {
                    _dirArqSalva = "w:\\RELATERRO\\RelAterroComValores_" + _sdf + "_" + _sdtInicioFim + ".pdf";
                    document.Save(_dirArqSalva);
                }
                else
                {
                    _dirArqSalva = Server.MapPath("/Temp/") + "RelAterroComValores_" + _sdf + "_" + _sdtInicioFim + ".pdf";
                    document.Save(_dirArqSalva);
                }
                document.Close();
            }
            finally
            {
                AddRow(_table, _row, _cell, "Arquivo salvo com sucesso em: " + _dirArqSalva, 800, false);
                Panel1.Controls.Add(_table);

                // enviar e-mail apenas para o destino final selecionado
                string _stx = geral.Texto_email_DestinoFinal();
                if (DESTINOFINAL1.Valor != "" && oDestinoFinal.eMail != "")
                {
                    EnviaeMail(oDestinoFinal.eMail, _dirArqSalva, oDestinoFinal.NomeFantasia);
                }
                Panel1.Controls.Add(_table);
            }
        }
    }

    private void Cabecalho(System.Text.StringBuilder pSb, int pPagina)
    {
        pSb.Append("Relatório de movimentação de resíduos por destino final com valores   Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
                   Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + "  Emissão: " + DateTime.Now.ToString("dd/MM/yy") + "  Pag." + pPagina.ToString("00")  + " \n\n");
        pSb.Append("Destinador: " + oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") \n\n");

        pSb.Append("  Data   Código Cliente                        Und  Qtde Coleta Qt.Descarga Resíduo                   Nº Ticket    Vl.Unitário Valor Total    Nº MTR-e     Total(Kg)MTRe \n");
        pSb.Append("──────── ────── ────────────────────────────── ──── ─────────── ─────────── ───────────────────────── ──────────── ─────────── ────────────── ──────────── ───────────── \n");
    }

    private void EnviaeMail(string peMail, string pArquivoAnexado, string pNomeMostrar_email)
    {
        string eMailQuemEnvia = "logistica@brooksambiental.com.br";
        string eMailQueLoga = "logistica@brooksambiental.com.br";
        string SenhaQueLoga = "logistic@21";

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        client.Port = 587;
        client.Credentials = new System.Net.NetworkCredential(eMailQueLoga, SenhaQueLoga);

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        mail.Sender = new System.Net.Mail.MailAddress(eMailQueLoga, "BROOKS Logística");
        mail.From = new System.Net.Mail.MailAddress(eMailQuemEnvia, "BROOKS Logística");

        // quando for o desenvolvedor - madar teste para mim.
        if (geral.UsuarioAtual.ToLower() == "teixeira")
        {
            mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com"));
        }
        else if (geral.UsuarioAtual != "" && geral.UsuarioAtual.ToLower() != "teixeira" && geral.UsuarioAtual != "&nbsp;")
        {
            foreach (string _email in peMail.Replace(",", ";").Split(";"[0]))
            {
                if (_email.Trim() != "")
                    mail.To.Add(new System.Net.Mail.MailAddress(_email, pNomeMostrar_email));
            }
            mail.To.Add(new System.Net.Mail.MailAddress("cris@brooksambiental.com.br"));
            mail.To.Add(new System.Net.Mail.MailAddress("logistica1@brooksambiental.com.br"));
        }
        mail.Subject = "Relatório de Resíduos - período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
                                                                Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
        mail.Body = geral.Texto_email_DestinoFinal();

        System.Net.Mail.Attachment _arquivoanexo = new System.Net.Mail.Attachment(pArquivoAnexado);

        mail.Attachments.Add(_arquivoanexo);

        mail.IsBodyHtml = true;
        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        try
        {
            client.Send(mail);
            AddRow(_table, _row, _cell, "e-mail enviado com sucesso.", 800, false);
        }
        catch (System.Exception erro)
        {
            AddRow(_table, _row, _cell, "Erro ao envair e-mail." + erro.Message, 800, false);
        }
        finally
        {
            mail = null;
            Panel1.Controls.Add(_table);
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        btnConfirmar.Visible = false;
        if (DESTINOFINAL1.Valor != "")
            Criar_Enviar_Relatorio();
        else
        {
            _dt = oDestinoFinalDados.PegaQQDados("where Enviar_emailMovResiduos = 1");
            foreach (DataRow _dr in _dt.Rows)
            {
                try
                {
                    DESTINOFINAL1.Valor = _dr["Codigo"].ToString();
                }
                finally
                {
                    Criar_Enviar_Relatorio();
                }
            }
        }
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

        string NomeArq = "Relatorio_MovResiduosComValores.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;

        int cdDestino = 2;
        if (DESTINOFINAL1.Valor != "")
            cdDestino = Convert.ToInt32(DESTINOFINAL1.Valor);

        oDestinoFinalDados = new clsDestinoFinalDados();
        oDestinoFinal = new clsDestinoFinal();
        oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, cdDestino);

        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        if (CLIENTE1.Valor != "")
            _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), true);
        else
            _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, true);
        for (int i = 1; i <= 15; i++)
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        Grade.DataSource = _dt;
        Grade.DataBind();

        if (_dt.Rows.Count > 0)
        {
            for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);

            decimal PesoTotalCliente = 0;
            decimal TotalMTRe = 0;
            string codigoAnterior = "";
            string MTReAnterior = "";
            if (Grade.Rows.Count > 0)
            {
                PesoTotalCliente = Convert.ToDecimal(Grade.Rows[0].Cells[5].Text);
                TotalMTRe = Convert.ToDecimal(Grade.Rows[0].Cells[5].Text);
            }
            if (Grade.Rows.Count > 1)
            {
                codigoAnterior = Grade.Rows[1].Cells[1].Text;
                MTReAnterior = Grade.Rows[1].Cells[10].Text;
            }
            for (int i = 0; i < Grade.Rows.Count; i++)
            {
                GridViewRow gvr = Grade.Rows[i];
                if (gvr.Cells[1].Text != codigoAnterior)
                {
                    if (i > 0)
                        Grade.Rows[i - 1].Cells[12].Text = PesoTotalCliente.ToString("N2");
                    else
                        Grade.Rows[i].Cells[12].Text = PesoTotalCliente.ToString("N2");
                    PesoTotalCliente = 0;
                }
                if (gvr.Cells[10].Text != MTReAnterior)
                {
                    if (i > 0)
                        Grade.Rows[i - 1].Cells[11].Text = TotalMTRe.ToString("N2");
                    else
                        Grade.Rows[i].Cells[11].Text = TotalMTRe.ToString("N2");
                    TotalMTRe = 0;
                }
                if (gvr.Cells[5].Text != "" && gvr.Cells[5].Text != "&nbsp;")
                {
                    PesoTotalCliente = PesoTotalCliente + Convert.ToDecimal(gvr.Cells[5].Text);
                    TotalMTRe = TotalMTRe + Convert.ToDecimal(gvr.Cells[5].Text);
                }
                codigoAnterior = gvr.Cells[1].Text;
                MTReAnterior = gvr.Cells[10].Text;
                if (gvr.Cells[0].Text != "" && gvr.Cells[0].Text != "&nbsp;")
                {
                    gvr.Cells[0].Text = DateTime.Parse(gvr.Cells[0].Text).ToShortDateString();
                }
            }
            Grade.Rows[Grade.Rows.Count - 1].Cells[11].Text = TotalMTRe.ToString("N2");
            Grade.Rows[Grade.Rows.Count - 1].Cells[12].Text = PesoTotalCliente.ToString("N2");

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
            lblTitulo.Text = "Relatorio Movimentacao de Residuos por Destino Final Com Valores - Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
                                                                                                                 Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + 
                                                                                                                 " - Data emissao: " + DateTime.Now.ToShortDateString();
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
            lblColuna.ID = "lblDestino";
            lblColuna.Text = "Destinador: " + oDestinoFinal.Nome + " (" + cdDestino + ")";
            row.Cells[0].Controls.Add(lblColuna);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            //Data - Código - Cliente - Und - Qtde Coletada - Qtde Descarga - Residuo - Nº Ticket - Vl.Unitario - Vl.Total - Nº MTR-e - Total(Kg)MTRe
            Grade.HeaderRow.Cells[0].Text = "Data descarga";
            Grade.HeaderRow.Cells[1].Text = "Codigo";
            Grade.HeaderRow.Cells[2].Text = "Cliente";
            Grade.HeaderRow.Cells[3].Text = "Und";
            Grade.HeaderRow.Cells[4].Text = "Qtde coletada";
            Grade.HeaderRow.Cells[5].Text = "Qtde descarga";
            Grade.HeaderRow.Cells[6].Text = "Descricao residuo";
            Grade.HeaderRow.Cells[7].Text = "No.Ticket";
            Grade.HeaderRow.Cells[8].Text = "Valor unitario";
            Grade.HeaderRow.Cells[9].Text = "Valor total";
            Grade.HeaderRow.Cells[10].Text = "No.MTR-e";
            Grade.HeaderRow.Cells[11].Text = "Total MTR-e";
            Grade.HeaderRow.Cells[12].Text = "Total Peso Cliente";

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