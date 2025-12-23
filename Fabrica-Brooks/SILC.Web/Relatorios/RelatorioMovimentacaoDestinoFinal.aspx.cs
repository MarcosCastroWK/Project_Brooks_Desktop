using System;
using System.Text;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.Web.UI;
using System.Web;

namespace SILC.Web.Relatorios
{
    
    public partial class RelatorioMovimentacaoDestinoFinal : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        //Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        DataTable _dt = new DataTable();    
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "45");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (!IsPostBack)
            {
                string _mesaatual = DateTime.Now.ToString();
    
                //Data1.Data = Convert.ToDateTime(("01/" + Convert.ToDateTime(_mesaatual).Month.ToString() + "/" + Convert.ToDateTime(_mesaatual).Year.ToString())).ToString("dd/MM/yyyy");
                Data1.Data = Convert.ToDateTime(_mesaatual).AddDays(-1).ToString("dd/MM/yyyy");
                Data2.Data = Convert.ToDateTime(_mesaatual).AddDays(-1).ToString("dd/MM/yyyy");
                DESTINOFINAL1.Valor = "";
                DESTINOFINAL1.Texto = "";
                CLIENTE1.Valor = "";
                CLIENTE1.Texto = "";
            }            
        }
    
        private void Relatorio()
        {
            btnConfirmar.Visible = false;
            Panel1.BorderWidth = 1;
    
            int cdDestino = 2;
            if (DESTINOFINAL1.Valor != "")
                cdDestino = Convert.ToInt32(DESTINOFINAL1.Valor);
    
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
    
            AddRow(_table, _row, _cell, "Relatório movimentação de aterro com nº da MTRe       Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 860, false, true);
            AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToString("dd/MM/yy"), 100, true);
            Panel1.Controls.Add(_table);
    
            oDestinoFinalDados = new clsDestinoFinalDados();
            oDestinoFinal = new clsDestinoFinal();
            oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, cdDestino);
    
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            AddRow(_table, _row, _cell, "Destinador: " + "<br />", 63, false, true);
            AddRow(_table, _row, _cell, oDestinoFinal.Nome + "(" + cdDestino + ") " + oDestinoFinal.NomeFantasia + "<br />", 1200, false, true);
            Panel1.Controls.Add(_table);
    
            clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
            if (CLIENTE1.Valor != "")
            {
                if (DESTINOFINAL1.Valor != "")
                    _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), false, chkNaoMostrarSemTicket.Checked);
                else if (DESTINOFINAL1.Valor == "")
                {
                    _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 2, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked);
                    _dt.Merge(oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 13, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked));
                }
            }
            else if (DESTINOFINAL1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, true, chkNaoMostrarSemTicket.Checked);
            else if (DESTINOFINAL1.Valor == "")
            {
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 2, 0, true, chkNaoMostrarSemTicket.Checked);
                _dt.Merge(oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 13, 0, true, chkNaoMostrarSemTicket.Checked));
            }
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();
                _row = new TableRow();
                _cell = new TableCell();
    
                int[] iColWidth = new int[15];
                iColWidth[1] = 50;  //Data
                iColWidth[2] = 50;  //Hora
                iColWidth[3] = 80;  //Local descarga
                iColWidth[4] = 80;  //Nº Ticket
                iColWidth[5] = 90;  //Motorista
                iColWidth[6] = 72;  //Placas
                iColWidth[7] = 80;  //Container
                iColWidth[8] = 70;  //Peso Total Kg
                iColWidth[9] = 88;  //Peso Kg Individual
                iColWidth[10] = 50;  //Código Cliente  
                iColWidth[11] = 200; //Nome Cliente  
                iColWidth[12] = 110; //CNPJ Cliente
                iColWidth[13] = 72;  //Total Grupo
                iColWidth[14] = 66;  //Nº MTR-e
    
    
                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;
                AddRow(_table, _row, _cell, "Data", iColWidth[1], false, true, true);
                AddRow(_table, _row, _cell, "Hora", iColWidth[2], false, true, true);
                AddRow(_table, _row, _cell, "Local Descarga", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "Nº Ticket", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "Motorista", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "Placas", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "Container", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "Peso Total", iColWidth[8], true, true);
                AddRow(_table, _row, _cell, "Peso Individual", iColWidth[9], true, true);
                AddRow(_table, _row, _cell, "Código", iColWidth[10], true, true);
                AddRow(_table, _row, _cell, "Cliente", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "CNPJ Cliente", iColWidth[12], false, true);
                AddRow(_table, _row, _cell, "Peso Grupo", iColWidth[13], true, true);
                AddRow(_table, _row, _cell, "Nº MTR-e", iColWidth[14], false, true);
    
                AddRow(_table, _row, _cell, "──────", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "──────", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "───────────", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "───────────", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "───────", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "──────────────────────────", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "──────────────", iColWidth[12], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[13], false, false);
                AddRow(_table, _row, _cell, "──────────", iColWidth[14], false, false);
                Panel1.Controls.Add(_table);
    
                decimal _vlSbTl = 0;
                decimal _valorTotalGeral = 0;
                string _DataAnterior = "";
                string _NumeroTicketAnterior = "";
                if (_dt.Rows.Count > 0)
                {
                    _DataAnterior = _dt.Rows[0]["Data"].ToString();
                }
                foreach (DataRow dr in _dt.Rows)
                {
                    if (_DataAnterior != dr["Data"].ToString() && _DataAnterior != "")
                    {
                        // adicionar linha com subtotais
                        AdicionaLinhaComSubtotais(_table, _row, _cell, iColWidth, _vlSbTl);
                        _vlSbTl = 0;
                    }
                    if (dr["NumeroTicket"].ToString() == "3444751")
                    {
                        _vlSbTl = _vlSbTl;
                    }
                    if (_NumeroTicketAnterior != dr["NumeroTicket"].ToString())
                    {
                        AddRow(_table, _row, _cell, Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yy"), iColWidth[1], true);
                        AddRow(_table, _row, _cell, dr["Hora"].ToString(), iColWidth[2], true);
                        AddRow(_table, _row, _cell, geral.Left(dr["LocalAterro"].ToString(), 30), iColWidth[3], false);
                        if (dr["NumeroTicket"].ToString() != "")
                            AddRow(_table, _row, _cell, dr["NumeroTicket"].ToString(), iColWidth[4], false);
                        else
                            AddRow(_table, _row, _cell, "-------------------", iColWidth[4], false, false, true);
                        clsAterroSanitario oAtSanit = new clsAterroSanitario();
                        clsAterroSanitarioDados oAtSanitDados = new clsAterroSanitarioDados();
                        if (dr["CodigoMotorista"].ToString() != "")
                            oAtSanit.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        oAtSanitDados.PegaDados(oAtSanit, dr["NumeroTicket"].ToString(), true, oAtSanit.CodigoMotorista);
    
                        if (oAtSanit.NomeMotorista != null)
                            AddRow(_table, _row, _cell, oAtSanit.NomeMotorista.Split(" "[0])[0], iColWidth[5], false);
                        else
                            AddRow(_table, _row, _cell, oAtSanit.NomeMotorista, iColWidth[5], false);
                        AddRow(_table, _row, _cell, oAtSanit.PlacasCaminhao, iColWidth[6], false);
                        string _containeres = oLancamentoMTRDados.PegaContaineres(dr["CodigoCliente"].ToString(), dr["MTRe"].ToString());
                        AddRow(_table, _row, _cell, _containeres, iColWidth[7], false);
    
                        //if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "&nbsp;")
                        //{
                        //    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"), iColWidth[8], true);
                        //    _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["PesoIndividual"]);
                        //    _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["PesoIndividual"]);
                        //}
                        //else
                        //    AddRow(_table, _row, _cell, "", iColWidth[8], true);
    
                        if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "&nbsp;")
                        {
                            AddRow(_table, _row, _cell, Convert.ToDecimal(dr["TotalPeso"]).ToString("N2"), iColWidth[8], true);
                        }
                        else
                            AddRow(_table, _row, _cell, "", iColWidth[8], true);
    
                        if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "&nbsp;")
                        {
                            _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["PesoIndividual"]);
                            _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["PesoIndividual"]);
                        }
                    }
                    else
                    {
                        AddRow(_table, _row, _cell, "", iColWidth[1], true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], true);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        if (dr["NumeroTicket"].ToString() != "")
                            AddRow(_table, _row, _cell, "", iColWidth[4], false);
                        else
                            AddRow(_table, _row, _cell, "-------------------", iColWidth[4], false, false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[5], false);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        string _containeres = oLancamentoMTRDados.PegaContaineres(dr["CodigoCliente"].ToString(), dr["MTRe"].ToString());
                        AddRow(_table, _row, _cell, _containeres, iColWidth[7], false);
                        AddRow(_table, _row, _cell, "", iColWidth[8], true);
                        if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "&nbsp;")
                        {
                            _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["PesoIndividual"]);
                            _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["PesoIndividual"]);
                        }
                    }
                    _NumeroTicketAnterior = dr["NumeroTicket"].ToString();
    
                    if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "&nbsp;")
                    {
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"), iColWidth[9], true);
                        if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                            AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[10], true);
                        else
                            AddRow(_table, _row, _cell, "", iColWidth[10], true);
                        AddRow(_table, _row, _cell, geral.Left(dr["Nome"].ToString(), 30), iColWidth[11], false);
                        AddRow(_table, _row, _cell, dr["CNPJ_CPF"].ToString(), iColWidth[12], false);
                        if (dr["TotalGrupo"].ToString() != "" && dr["TotalGrupo"].ToString() != "0")
                            AddRow(_table, _row, _cell, Convert.ToDecimal(dr["TotalGrupo"]).ToString("N2"), iColWidth[13], true);
                        else
                            AddRow(_table, _row, _cell, "", iColWidth[13], true);
                        AddRow(_table, _row, _cell, dr["MTRe"].ToString(), iColWidth[14], false);
                    }
                    else
                    {
                        AddRow(_table, _row, _cell, "", iColWidth[9], true);
                        AddRow(_table, _row, _cell, "", iColWidth[10], true);
                        AddRow(_table, _row, _cell, "", iColWidth[11], false);
                        AddRow(_table, _row, _cell, "", iColWidth[12], false);
                        AddRow(_table, _row, _cell, "", iColWidth[13], true);
                        AddRow(_table, _row, _cell, "", iColWidth[14], false);
                    }
                    _DataAnterior = dr["Data"].ToString();
    
                }
                // adicionar linha com subtotais
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], false);
                AddRow(_table, _row, _cell, "", iColWidth[6], true);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], false);
                AddRow(_table, _row, _cell, "", iColWidth[10], true);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "Total Peso", iColWidth[12], true);
                AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[13], true);
                AddRow(_table, _row, _cell, "", iColWidth[14], true);
    
                // adicionar linha com subtotais
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], false);
                AddRow(_table, _row, _cell, "", iColWidth[6], true);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], false);
                AddRow(_table, _row, _cell, "", iColWidth[10], true);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "Total Peso", iColWidth[12], true);
                AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[13], true);
                AddRow(_table, _row, _cell, "", iColWidth[14], true);
                // adicionar linha com total geral
    
                Panel1.Controls.Add(_table);
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
            // o e-mail pro fornecedor é obrigatório envio do numero do ticket
            chkNaoMostrarSemTicket.Checked = false;
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            if (DESTINOFINAL1.Valor != "")
            {
                if (DESTINOFINAL1.Valor != "")
                    AddRow(_table, _row, _cell, "Confirme o envio de e-mail para o fornecedor selecionado.", 600, false);
                else
                    AddRow(_table, _row, _cell, "Confirme o envio de e-mail para os fornecedores 2 e 13 do cadastro.", 600, false);
                Panel1.BorderWidth = 0;
                Panel1.Controls.Add(_table);
                btnConfirmar.Visible = true;
            }
            else
            {
                AddRow(_table, _row, _cell, "O relatório está em ajustes pelo desenvolvimento. Favor enviar um a um (destino). Obrigado.", 600, false);
                Panel1.BorderWidth = 0;
                Panel1.Controls.Add(_table);
                btnConfirmar.Visible = false;
            }
        }
    
        private void Criar_Enviar_Relatorio()
        {
            int _pagina = 1;
            var document = new PdfSharp.Pdf.PdfDocument();
            var page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
            var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
            var font = new PdfSharp.Drawing.XFont("Courier New", 7);
    
            StringBuilder _sb = new StringBuilder();
    
            oDestinoFinalDados = new clsDestinoFinalDados();
            oDestinoFinal = new clsDestinoFinal();
            if (DESTINOFINAL1.Valor != "")
                oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
    
            //_sb.Append("Destinador: " + oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") \n\n");
            clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked);
            else
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, true, chkNaoMostrarSemTicket.Checked);
            if (_dt.Rows.Count > 0)
            {
                Cabecalho(_sb, _pagina);
                textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, 20, page.Width - 60, page.Height - 60));
    
                decimal _vlSbTl = 0;
                decimal _valorTotalGeral = 0;
                string _DataAnterior = "";
                string _NumeroTicketAnterior = "";
                if (_dt.Rows.Count > 0)
                {
                    _DataAnterior = _dt.Rows[0]["Data"].ToString();
                }
                int _ln = 60;
                foreach (DataRow dr in _dt.Rows)
                {
                    _sb.Clear();
                    if (_DataAnterior != dr["Data"].ToString() && _DataAnterior != "")
                    {
                        // adicionar linha com subtotais
                        _sb.Append(" ".PadLeft(148) + "Total Peso " + " ".PadLeft(12 - geral.Left(_vlSbTl.ToString("N2"), 11).Length) + _vlSbTl.ToString("N2") + " \n");
                        _ln = _ln + 8;
                        textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, _ln, page.Width - 60, page.Height + 60));
                        _vlSbTl = 0;
                    }
                    _sb.Clear();
                    if (_NumeroTicketAnterior != dr["NumeroTicket"].ToString())
                    {
                        _sb.Append(Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yy"));
                        _sb.Append(" ".PadLeft(1) + dr["Hora"].ToString());
                        _sb.Append(" " + geral.Left(dr["LocalAterro"].ToString(), 14) + " ".PadLeft(15 - geral.Left(dr["LocalAterro"].ToString(), 14).Length));
                        _sb.Append(" " + dr["NumeroTicket"].ToString() + " ".PadLeft(9 - geral.Left(dr["NumeroTicket"].ToString(), 8).Length));
    
                        clsAterroSanitario oAtSanit = new clsAterroSanitario();
                        clsAterroSanitarioDados oAtSanitDados = new clsAterroSanitarioDados();
                        if (dr["CodigoMotorista"].ToString() != "")
                            oAtSanit.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        oAtSanitDados.PegaDados(oAtSanit, dr["NumeroTicket"].ToString(), true, oAtSanit.CodigoMotorista);
                        
                        if (oAtSanit.NomeMotorista != null && oAtSanit.NomeMotorista != "")
                            _sb.Append(oAtSanit.NomeMotorista.Split(" "[0])[0] + " ".PadLeft(12 - geral.Left(oAtSanit.NomeMotorista.Split(" "[0])[0], 11).Length));
                        else if (oAtSanit.NomeMotorista == null || oAtSanit.NomeMotorista == "")
                            _sb.Append(" " + " ".PadLeft(12 - geral.Left(" ", 11).Length));
                        else
                            _sb.Append(oAtSanit.NomeMotorista + " ".PadLeft(12 - geral.Left(oAtSanit.NomeMotorista, 11).Length));
    
                        if (oAtSanit.PlacasCaminhao != null && oAtSanit.PlacasCaminhao != "")
                            _sb.Append(" " + oAtSanit.PlacasCaminhao + " ".PadLeft(9 - geral.Left(oAtSanit.PlacasCaminhao, 8).Length));
                        else
                            _sb.Append(" " + " ".PadLeft(13 - geral.Left(" ", 12).Length));
    
                        _sb.Append(" " + dr["NumeroCaixa"].ToString() + " ".PadLeft(8 - geral.Left(dr["NumeroCaixa"].ToString(), 7).Length));
    
                        if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "0")
                        {
                            _sb.Append(" ".PadLeft(11 - geral.Left(Convert.ToDecimal(dr["TotalPeso"]).ToString("N2"), 10).Length) + Convert.ToDecimal(dr["TotalPeso"]).ToString("N2"));
    
                        }
                        else
                            _sb.Append(" ".PadLeft(11 - geral.Left(0.ToString("N2"), 10).Length) + " ".PadLeft(1));
                        if (dr["PesoIndividual"].ToString() != "")
                        {
                            _sb.Append(" ".PadLeft(16 - geral.Left(Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"), 10).Length) + Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"));
                            _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["PesoIndividual"]);
                            _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["PesoIndividual"]);
                        }
                        else
                            _sb.Append(" ".PadLeft(16) + " ");
                    }
                    else
                    {
                        _sb.Append(" ".PadLeft(69 - dr["NumeroCaixa"].ToString().Length) + dr["NumeroCaixa"].ToString());
                        if (dr["PesoIndividual"].ToString() != "")
                        {
                            _sb.Append(" ".PadLeft(32 - geral.Left(Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"), 31).Length) + Convert.ToDecimal(dr["PesoIndividual"]).ToString("N2"));
                            _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["PesoIndividual"]);
                            _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["PesoIndividual"]);
                        }
                        else
                            _sb.Append(" ".PadLeft(32) + " ");
                    }
    
                    _NumeroTicketAnterior = dr["NumeroTicket"].ToString();
    
                    if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                        _sb.Append(" ".PadLeft(7 - geral.Left(dr["CodigoCliente"].ToString(), 6).Length) + dr["CodigoCliente"].ToString());
                    else
                        _sb.Append(" ".PadLeft(7) + " ");
    
                    _sb.Append(" ".PadLeft(1) + geral.Left(dr["Nome"].ToString(), 30) + " ".PadLeft(31 - geral.Left(dr["Nome"].ToString(), 30).Length));
    
                    _sb.Append(dr["CNPJ_CPF"].ToString() + " ".PadLeft(21 - geral.Left(dr["CNPJ_CPF"].ToString(), 20).Length));
    
                    if (dr["TotalGrupo"].ToString() != "" && dr["TotalGrupo"].ToString() != "0")
                        _sb.Append(" ".PadLeft(10 - Convert.ToDecimal(dr["TotalGrupo"]).ToString("N2").Length) + Convert.ToDecimal(dr["TotalGrupo"]).ToString("N2"));
                    else
                        _sb.Append(" ".PadLeft(10) + "");
    
                    _sb.Append(" ".PadLeft(1) + dr["MTRe"].ToString());
                    _DataAnterior = dr["Data"].ToString();
    
                    font = new PdfSharp.Drawing.XFont("Courier New", 7);
                    _ln = _ln + 8;
                    textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, _ln, page.Width - 60, page.Height + 60));
                    if (_ln > 500)
                    {
                        page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;
                        graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                        textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                        _ln = 80;
                        font = new PdfSharp.Drawing.XFont("Courier New", 7);
                        _sb.Clear();
                        _pagina++;
                        Cabecalho(_sb, _pagina);
                        textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, 20, page.Width - 60, page.Height - 60));
                    }
                }
                _sb.Clear();
                // adicionar linha com ultimo subtotal
                _sb.Append(" ".PadLeft(148) + "Total Peso " + " ".PadLeft(12 - geral.Left(_vlSbTl.ToString("N2"), 11).Length) + _vlSbTl.ToString("N2") + " \n");
                _sb.Append(" ".PadLeft(147) + "Total geral " + " ".PadLeft(12 - geral.Left(_valorTotalGeral.ToString("N2"), 11).Length) + _valorTotalGeral.ToString("N2"));
                _ln = _ln + 8;
                textFormatter.DrawString(_sb.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, _ln, page.Width - 60, page.Height + 60));
            }
            string _sdf = "";
            if (DESTINOFINAL1.Valor != "")
                _sdf = Convert.ToInt32(DESTINOFINAL1.Valor).ToString("0000");
            string _sdtInicioFim = Convert.ToDateTime(Data1.Data).ToString("yyMMdd") + "_" + Convert.ToDateTime(Data2.Data).ToString("yyMMdd");
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            if (System.IO.Directory.Exists(Server.MapPath("/Temp/")))
            {
                try
                {
                    document.Save(Server.MapPath("/Temp/") + "RelAterroMTRe_" + _sdf + "_" + _sdtInicioFim + ".pdf");
                    document.Close();
                }
                finally
                {
                    AddRow(_table, _row, _cell, "Arquivo salvo com sucesso em: " + Server.MapPath("/Temp/") + "RelAterroMTRe_" + _sdf + "_" + _sdtInicioFim + ".pdf", 800, false);
                    Panel1.Controls.Add(_table);
    
                    // enviar e-mail apenas para o destino final selecionado
                    string _stx = geral.Texto_email_DestinoFinal();
                    {
                        EnviaeMail(oDestinoFinal.eMail, Server.MapPath("/Temp/") + "RelAterroMTRe_" + _sdf + "_" + _sdtInicioFim + ".pdf", oDestinoFinal.NomeFantasia);
                    }
                }
            }
            else
                AddRow(_table, _row, _cell, "Não possível gerar Arquivo PDF. Unidade ou mapeamento do servidor inexistente! " + Server.MapPath("/Temp/"), 800, false);
            Panel1.Controls.Add(_table);
        }
    
        private void Cabecalho(StringBuilder pSb, int pPagina)
        {
            pSb.Append("Relatório movimentação de aterro com nº da MTRe     Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
                       Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + "  Emissão: " + DateTime.Now.ToString("dd/MM/yy") + "  Pag." + pPagina.ToString("00")  + " \n\n");
            pSb.Append("Destinador: " + oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") " + oDestinoFinal.NomeFantasia + " \n\n");
            pSb.Append("  Data   Hora     Local Descarga Nº Ticket Motorista    Placas   Container Peso Total Peso Individual Código Cliente                        CNPJ Cliente         Peso Grupo Nº MTR-e    \n");
            pSb.Append("──────── ──────── ────────────── ───────── ──────────── ──────── ───────── ────────── ─────────────── ────── ────────────────────────────── ──────────────────── ────────── ─────────── \n");
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
    
            if (oUsuario.Nome.ToLower() == "teixeira")
            {
                mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com"));
                mail.To.Add(new System.Net.Mail.MailAddress("andre.toro@brooksambiental.com.br"));
            }
            else
            {
                // insere e-mail(s) do cadastro
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
    
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (DESTINOFINAL1.Valor != "" && DESTINOFINAL1.Valor != "0")
                Criar_Enviar_Relatorio();
            else
            {
                try
                {
                    DESTINOFINAL1.Valor = "2";  // Proativa Transbordo                
                }
                finally
                {
                    Criar_Enviar_Relatorio();
                }
                try 
                { 
                    DESTINOFINAL1.Valor = "13"; // Proativa Aterro Tijucas 
                } 
                finally
                { 
                    Criar_Enviar_Relatorio(); 
                }
            }
            btnConfirmar.Visible = false;
        }
        private void AdicionaLinhaComSubtotais(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, decimal _vlSbTl)
        {
            // adicionar linha com subtotais
            AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "", iColWidth[4], false);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], false);
            AddRow(_table, _row, _cell, "", iColWidth[10], true);
            AddRow(_table, _row, _cell, "", iColWidth[11], true);
            AddRow(_table, _row, _cell, "Total Peso", iColWidth[12], true);
            AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[13], true);
            AddRow(_table, _row, _cell, "", iColWidth[14], true);
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
    
            string NomeArq = "Relatorio_MovimentoDestinoFinal.xls";
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
            {
                if (DESTINOFINAL1.Valor != "")
                    _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked);
                else if (DESTINOFINAL1.Valor == "")
                {
                    _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 2, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked);
                    _dt.Merge(oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 13, Convert.ToInt32(CLIENTE1.Valor), true, chkNaoMostrarSemTicket.Checked));
                }
            }
            else if (DESTINOFINAL1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, true, chkNaoMostrarSemTicket.Checked);
            else if (DESTINOFINAL1.Valor == "")
            {
                _dt = oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 2, 0, true, chkNaoMostrarSemTicket.Checked);
                _dt.Merge(oLancamentoMTRDados.PreencheDTAterroSanitario(oLancamentoMTR, Data1.Data, Data2.Data, 13, 0, true, chkNaoMostrarSemTicket.Checked));
            }
    
            _dt.Columns.RemoveAt(7);
            Grade.DataSource = _dt;
            Grade.DataBind();
    
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                    Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);
    
                decimal PesoGrupo = 0;
                string dataAnterior = "";
                if (Grade.Rows.Count > 0)
                    dataAnterior = DateTime.Parse(Grade.Rows[0].Cells[0].Text).ToShortDateString();
    
                string _ticketAnterior = "";
                for (int i = 0; i < Grade.Rows.Count; i++)
                {
                    GridViewRow gvr = Grade.Rows[i];
                    if (gvr.Cells[8].Text != "" && gvr.Cells[8].Text != "&nbsp;")
                        PesoGrupo = PesoGrupo + Convert.ToDecimal(gvr.Cells[8].Text);
                    
                    if (gvr.Cells[0].Text != dataAnterior)
                    {
                        if (i - 1 >= 0)
                        {
                            Grade.Rows[i - 1].Cells[12].Text = PesoGrupo.ToString("N2");
                            PesoGrupo = 0;
                        }
                    }                
                    if (gvr.Cells[0].Text != "" && gvr.Cells[0].Text != "&nbsp;")
                    {
                        gvr.Cells[0].Text = DateTime.Parse(gvr.Cells[0].Text).ToShortDateString();
                    }
                    dataAnterior = gvr.Cells[0].Text;
                    
                    if (gvr.Cells[3].Text == _ticketAnterior)
                    {
                        gvr.Cells[7].Text = "";
                    }
                    _ticketAnterior = gvr.Cells[3].Text;
                }
                Grade.Rows[Grade.Rows.Count - 1].Cells[12].Text = PesoGrupo.ToString("N2");
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
                lblTitulo.Text = "Relatorio Movimento Destino Final - Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
                                                                                      Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + " - Data emissao: " + 
                                                                                      DateTime.Now.ToShortDateString();
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
                lblColuna.Text = "Destinador: " + oDestinoFinal.Nome + " (" + cdDestino + ") " + oDestinoFinal.NomeFantasia;
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
                table.Rows.Add(row);
    
                // Data Hora Local Descarga Nº Ticket Motorista Placas Container Peso Total Peso Individual Código Cliente CNPJ Cliente Peso Grupo Nº MTR-e
                Grade.HeaderRow.Cells[0].Text = "Data";
                Grade.HeaderRow.Cells[1].Text = "Hora";
                Grade.HeaderRow.Cells[2].Text = "Local Descarga";
                Grade.HeaderRow.Cells[3].Text = "No.Ticket";
                Grade.HeaderRow.Cells[4].Text = "Motorista";
                Grade.HeaderRow.Cells[5].Text = "Placas";
                Grade.HeaderRow.Cells[6].Text = "Container";
                Grade.HeaderRow.Cells[7].Text = "Peso Total";
                Grade.HeaderRow.Cells[8].Text = "Peso Individual ";
                Grade.HeaderRow.Cells[9].Text = "Codigo";
                Grade.HeaderRow.Cells[10].Text = "Cliente";
                Grade.HeaderRow.Cells[11].Text = "CNPJ Cliente";
                Grade.HeaderRow.Cells[12].Text = "Peso Grupo";
                Grade.HeaderRow.Cells[13].Text = "No.MTR-e";
    
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
}