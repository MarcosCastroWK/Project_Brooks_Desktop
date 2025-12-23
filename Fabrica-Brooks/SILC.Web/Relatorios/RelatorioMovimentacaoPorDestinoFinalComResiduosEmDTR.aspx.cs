using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.Relatorios
{
    public partial class RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        // Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        Table _table = new Table();
    
        DataTable _dt = new DataTable();
        System.Text.StringBuilder _sb = new System.Text.StringBuilder();
    
        private decimal _vlSbTl = 0;
        private decimal l_qtSbTlPorMTRe = 0;
        private decimal _valorTotalGeral = 0;
    
        bool bTemNumeroCDFe = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "46");
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
        private void AddLinhaEmBranco(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, decimal _vlSbTl)
        {
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
            AddRow(_table, _row, _cell, "", iColWidth[13], false, true);
            if (chkMostrarCDFe.Checked)
            {
                AddRow(_table, _row, _cell, "", iColWidth[14], false, false);
                AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
            }
        }
        private void AddTotalMTR_e(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, string _MTReAnterior, decimal l_qtSbTlPorMTRe, decimal _qtCDFe)
        {
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
            AddRow(_table, _row, _cell, "Total MTR-e IBAMA", iColWidth[11], true, false, false, false, false, true);
            AddRow(_table, _row, _cell, "", iColWidth[12], false, false, false, false, false, true);
            if (_qtCDFe != l_qtSbTlPorMTRe)
                AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, true);
            else
                AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, false, true);
            AddRow(_table, _row, _cell, "", iColWidth[14], false, true);
            if (_qtCDFe != l_qtSbTlPorMTRe)
                AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false, false, false, true);
            else
                AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false);
            AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
            AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
        }
    
        private void AddLinhaSubTotais(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, decimal _vlSbTl)
        {
            /*
            if (_vlSbTl > 0)
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
                AddRow(_table, _row, _cell, "Peso total p/cliente", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[13], true);
                if (chkMostrarCDFe.Checked)
                {
                    AddRow(_table, _row, _cell, "", iColWidth[14], false, false);
                    AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                    AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
                    AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
                }
                _vlSbTl = 0;
            }
            */
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
            AddRow(_table, _row, _cell, "", iColWidth[13], false, true);
            if (chkMostrarCDFe.Checked)
            {
                AddRow(_table, _row, _cell, "", iColWidth[14], false, false);
                AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
            }
        }
        private void AddLinhaDados(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, DataRow dr, decimal l_qt, decimal _qtCDFe, string _MTReAnterior, DataRow drMTRe, int _i, string _cnpj_cpf, 
                                   string sExisteCodigoIBAMA, bool pEhDiferenteNaLinha = false, bool bTemTotalMTRe = false)
        {
            AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataDescarga"]).ToString("dd/MM/yy"), iColWidth[1], true);
            AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[2], true);
            AddRow(_table, _row, _cell, geral.Left(dr["Nome"].ToString(), 18), iColWidth[3], false);
            AddRow(_table, _row, _cell, geral.RetiraLetras(dr["CNPJ_CPF"].ToString()), iColWidth[4], false);
            AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[5], true);
            AddRow(_table, _row, _cell, geral.Left(dr["DescricaoReduzida"].ToString(), 22), iColWidth[6], false);
            AddRow(_table, _row, _cell, dr["Ticket"].ToString(), iColWidth[7], false);
            AddRow(_table, _row, _cell, dr["NomeMotorista"].ToString().Split(" "[0])[0].ToString(), iColWidth[8], false);
    
            if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                AddRow(_table, _row, _cell, dr["Placas"].ToString(), iColWidth[9], false, false, false, true);
            else
                AddRow(_table, _row, _cell, dr["Placas"].ToString(), iColWidth[9], false);
    
            AddRow(_table, _row, _cell, dr["CodigoIbama"].ToString(), iColWidth[10], false);
    
            AddRow(_table, _row, _cell, geral.Left(dr["Descricao"].ToString().Replace("–", ""), 30), iColWidth[11], false);
            AddRow(_table, _row, _cell, dr["MTRe"].ToString(), iColWidth[12], false);
    
            if (!chkMostrarCDFe.Checked)
            {
                if (dr["QtDescarga"].ToString() != "")
                    AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true);
                else
                    AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[13], true);
            }
            else if (chkMostrarCDFe.Checked)
            {
                if (geral.IsNumeric(dr["QtdeCDFe"].ToString()) && dr["QtdeCDFe"].ToString() != "")
                {
                    if (_qtCDFe == 0)
                        _qtCDFe = Convert.ToDecimal(dr["QtdeCDFe"].ToString());
                }
                if ((l_qt != _qtCDFe && !bTemTotalMTRe) || (pEhDiferenteNaLinha && !bTemTotalMTRe))
                    AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true, false, false, false, true);
                else
                    AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true);
    
                if (dr["NumeroCDFe"].ToString() == "0" || dr["NumeroCDFe"].ToString() == "")
                {
                    string _numeroCDFe = "";
                    // pegar o número da CDFe, possivelmente o código do ibama e numeroMTRe - não combinam - que é o caso do codigo do ibama 15 01 06 - mistura de embalagens
                    clsCDFeDados oCDFeDadosNumero = new clsCDFeDados();
                    if (dr["MTRe"].ToString() != "" && dr["MTRe"].ToString() != "-1" && dr["MTRe"].ToString() != "0")
                    {
                        _numeroCDFe = oCDFeDadosNumero.PegaNumeroCDFe(dr["MTRe"].ToString(), dr["CodigoIBAMA"].ToString());
                    }
                    if (_numeroCDFe == "" || _numeroCDFe == "0")
                        AddRow(_table, _row, _cell, "", iColWidth[14], false, false, false, true);
                    else
                        AddRow(_table, _row, _cell, _numeroCDFe, iColWidth[14], false);
                }
                else
                    AddRow(_table, _row, _cell, dr["NumeroCDFe"].ToString(), iColWidth[14], false);
                drMTRe = _dt.Rows[_i];
                if (_MTReAnterior != (dr["MTRe"].ToString()))
                {
                    if (_i + 1 < _dt.Rows.Count)
                        drMTRe = _dt.Rows[_i + 1];
                    if (drMTRe["MTRe"].ToString() == dr["MTRe"].ToString() && (_i + 1 < _dt.Rows.Count))
                    {
                        AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                    }
                    else
                    {
                        if (bTemTotalMTRe) // quando tem total mtr-e - não mostrar quantidade total da CDFe
                            AddRow(_table, _row, _cell, " ", iColWidth[15], true, false);
                        else
                        {
                            if (l_qt != _qtCDFe)
                                AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false, false, false, true);
                            else
                                AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false);
                        }
                    }
                }
                else
                    AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                _qtCDFe = 0;
                AddRow(_table, _row, _cell, "&nbsp;" + dr["Situacao"].ToString(), iColWidth[16], false, false);
                AddRow(_table, _row, _cell, "&nbsp;" + Convert.ToDateTime(dr["DataRetirada"]).ToString("dd/MM/yy"), iColWidth[17], false, false, true);
            }
        }
    
        private void AddLinhaDadosCondicao2(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, DataRow dr, decimal l_qt, decimal _qtCDFe, string _MTReAnterior, DataRow drMTRe, 
                                            int _i, string _cnpj_cpf, string sExisteCodigoIBAMA, bool pEhDiferenteNaLinha, DataTable _dtTotaisMTRe, decimal _t1, decimal _t2, bool bTemTotalMTRe = false)
        {
            if (_dtTotaisMTRe.Rows.Count > 0)
            {
                if ((dr["ClienteEmissaoMTReRCD"].ToString() == "" || dr["ClienteEmissaoMTReRCD"].ToString() == "0") && dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["CodigoResiduo"].ToString() == "160")
                {
                    _vlSbTl = _vlSbTl + l_qt;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                    _valorTotalGeral = _valorTotalGeral + l_qt;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, bTemTotalMTRe);
                }
                else if (dr["ClienteEmissaoMTReRCD"].ToString() == "1")
                {
                    _vlSbTl = _vlSbTl + l_qt;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                    _valorTotalGeral = _valorTotalGeral + l_qt;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, bTemTotalMTRe);
                }
                else if (dr["CodigoResiduo"].ToString() != "160")
                {
                    _vlSbTl = _vlSbTl + l_qt;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                    _valorTotalGeral = _valorTotalGeral + l_qt;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, bTemTotalMTRe);
                }
            }
            else
            {
                if (dr["ClienteEmissaoMTReRCD"].ToString() == "0" && dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["CodigoResiduo"].ToString() == "160")
                {
                    _vlSbTl = _vlSbTl + _t1;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                    _valorTotalGeral = _valorTotalGeral + _t1;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, bTemTotalMTRe);
                }
                else if (dr["ClienteEmissaoMTReRCD"].ToString() == "1")
                {
                    _vlSbTl = _vlSbTl + _t1;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                    _valorTotalGeral = _valorTotalGeral + _t1;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, bTemTotalMTRe);
                }
                else if (dr["CodigoResiduo"].ToString() != "160")
                {
                    _vlSbTl = _vlSbTl + _t1;
                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                    _valorTotalGeral = _valorTotalGeral + _t1;
                    AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, bTemTotalMTRe);
                }
            }
        }
        private void Relatorio()
        {
            if (DESTINOFINAL1.Valor == "")
                return;
    
            Panel1.BorderWidth = 1;
    
            if (DESTINOFINAL1.Valor == "")
                DESTINOFINAL1.Valor = "0";
    
            oDestinoFinal.Codigo = Convert.ToInt32(DESTINOFINAL1.Valor);
            if (DESTINOFINAL1.Valor != "0")
                oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
    
            Table _table = new Table();
            _table.ID = "table1";
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
    
            AddRow(_table, _row, _cell, "Relatório Recebimento MTR-e e CDF-e por Destino Final - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 860, false, true);
            AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToString("dd/MM/yy"), 100, true);
            Panel1.Controls.Add(_table);
    
            oDestinoFinalDados = new clsDestinoFinalDados();
            oDestinoFinal = new clsDestinoFinal();
            if (DESTINOFINAL1.Valor != "0")
                oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
    
            _table = new Table();
            _table.ID = "table2";
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            if (DESTINOFINAL1.Valor == "0")
            {
                AddRow(_table, _row, _cell, "Destinador: <br />", 63, false, true);
                AddRow(_table, _row, _cell, "DTR - BROOKS <br />", 860, false, true);
            }
            else
            {
                AddRow(_table, _row, _cell, "Destinador: " + "<br />", 63, false, true);
                AddRow(_table, _row, _cell, oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ")<br />", 860, false, true);
            }
            Panel1.Controls.Add(_table);
    
            clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
            DataTable _dtDTR = new DataTable();
            if (CLIENTE1.Valor != "")
            {
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), false, true, true);
                // adiciona os em dtr
                _dtDTR = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, Convert.ToInt32(CLIENTE1.Valor), false, true, true);
            }
            else
            {
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, false, true, true);
                // adiciona os em dtr
                _dtDTR =  oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, 0, false, true, true);
            }
           
            if (_dtDTR.Rows.Count > 0)
            {
                foreach (DataRow _dr2 in _dtDTR.Rows)
                {
                    if (_dr2["Situacao"].ToString() == "" || _dr2["Situacao"].ToString() == "&nbsp;" || _dr2["Situacao"].ToString() == "Em Armazenamento Temporário")
                    {
                        _dr2["Situacao"] = "Em DTR";
                    }
                }
                _dt.Merge(_dtDTR);
            }
            _dt.Select("", "MTRe");
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();
                _table.ID = "tableRelatorio";
                _row = new TableRow();
                _cell = new TableCell();
    
                int _itc = 14;
                if (chkMostrarCDFe.Checked)
                    _itc = 18;
                int[] iColWidth = new int[_itc];
                iColWidth[1] = 51;    //DataDescarga
                iColWidth[2] = 50;    //Código Cliente  
                iColWidth[3] = 144;   //Nome Cliente  
                iColWidth[4] = 120;   //CNPJ Cliente
                iColWidth[5] = 68;    //Quantid Individual
                iColWidth[6] = 201;   //Resíduo
                iColWidth[7] = 81;    //Nº Ticket
                iColWidth[8] = 102;   //Motorista
                iColWidth[9] = 68;    //Placas
                iColWidth[10] = 60;   //Código IBAMA
                iColWidth[11] = 192;  //Descrição IBAMA
                iColWidth[12] = 76;   //Nº MTR-e
                iColWidth[13] = 80;   //Peso Total Kg
                if (chkMostrarCDFe.Checked)
                {
                    iColWidth[14] = 79;   //CDFe número
                    iColWidth[15] = 72;   //Qtde CDFe
                    iColWidth[16] = 90;   //Situação
                    iColWidth[17] = 51;   //DataColeta
                }
    
                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;
                Panel1.Width = tWidthContratos + 10;
                AddRow(_table, _row, _cell, "Data", iColWidth[1], false, true, true);
                AddRow(_table, _row, _cell, "Código", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "Cliente", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "CNPJ", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "Qtdade. Individual", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "Resíduo", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "Nº Ticket", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "Motorista", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "Placas", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "Código", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "Descrição IBAMA", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "Nº MTR-e", iColWidth[12], false, true);
                AddRow(_table, _row, _cell, "Peso Total Kg", iColWidth[13], false, true);
                if (chkMostrarCDFe.Checked)
                {
                    AddRow(_table, _row, _cell, "Nº CDFe", iColWidth[14], false, true);
                    AddRow(_table, _row, _cell, "Qtde CDFe", iColWidth[15], false, true);
                    AddRow(_table, _row, _cell, "Situação", iColWidth[16], false, true);
                    AddRow(_table, _row, _cell, "Dt.Coleta", iColWidth[17], false, true, true);
                }
                AddRow(_table, _row, _cell, "──────", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "─────", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "───────────────────", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "──────────────", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "────────", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "──────────────────────────", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "────────────", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "─────────", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "──────", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "───────────────────────", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "────────", iColWidth[12], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[13], false, false);
                if (chkMostrarCDFe.Checked)
                {
                    AddRow(_table, _row, _cell, "─────────", iColWidth[14], false, false);
                    AddRow(_table, _row, _cell, "─────────", iColWidth[15], false, false);
                    AddRow(_table, _row, _cell, "───────────", iColWidth[16], false, false);
                    AddRow(_table, _row, _cell, "──────", iColWidth[17], false, true);
                }
                Panel1.Controls.Add(_table);
    
                _vlSbTl = 0;
                l_qtSbTlPorMTRe = 0;
                decimal l_qtSbTlPorMTReCDFe = 0;
                _valorTotalGeral = 0;
                decimal l_qt = 0;
                decimal _t1 = 0;
                bTemNumeroCDFe = false;
                bool bTemTotalMTRe = false;
    
                string _MTReAnterior = "";
                string _CDFeAnterior = "-1";  // a 1ª, -1 para não mostrar Total MTRe, pois tem que ter no mínimo duas
                string _codigoIbamaAnterior = "";
                int _i = -1;
                //for (int _i = 0; _i < _dt.Rows.Count; _i++)
                foreach (DataRow dr in _dt.Select("", "MTRe"))
                {                
                    _i++;
                    //DataRow dr = _dt.Rows[_i];
                    string _tst_MTRe = dr["MTRe"].ToString();
                    if (_tst_MTRe == "2401001859")
                    {
                        // teste 
                        _vlSbTl = _vlSbTl + 0;
                    }
                    l_qt = 0;
                    decimal _qtCDFe = 0;
                    _t1 = 0;
                    decimal _t2 = 0;
                    DataTable _dtTotaisMTRe = new DataTable();
                    
                    bTemTotalMTRe = false;
                    string _somtreatual = geral.Left(dr["MTRe"].ToString(), 10);
                    int _regsMTReIguaisAtual = _dt.Select("MTRe = '" + _somtreatual + "'").Length;
                    if (_regsMTReIguaisAtual > 1)
                        bTemTotalMTRe = true;
    
                    string _somtre = geral.Left(_MTReAnterior, 10);
                    int _regsMTReIguais = _dt.Select("MTRe = '" + _somtre + "'").Length;
    
                    if (_MTReAnterior != dr["MTRe"].ToString() && chkMostrarCDFe.Checked)
                    {
                        string _codigoIBAMA = dr["CodigoIBAMA"].ToString();
                        string MTRe2Anterior = "";
                        string AtualMTRe = dr["MTRe"].ToString();
                        if (_i - 2 < _dt.Rows.Count && _i > 1)
                        {
                            //MTRe2Anterior = _dt.Rows[_i - 2]["MTRe"].ToString() + _dt.Rows[_i - 2]["CodigoIBAMA"].ToString();
                            MTRe2Anterior = _dt.Rows[_i - 2]["MTRe"].ToString();
                            if (geral.IsNumeric(_dt.Rows[_i - 2]["QtdeCDFe"].ToString()) && _dt.Rows[_i - 2]["QtdeCDFe"].ToString() != "")
                                _qtCDFe = Convert.ToDecimal(_dt.Rows[_i - 2]["QtdeCDFe"].ToString());
                            _codigoIBAMA = _dt.Rows[_i - 2]["CodigoIBAMA"].ToString();
                        }
    
                        if (_regsMTReIguais > 1)
                        {
                            l_qtSbTlPorMTRe = 0;
                            _qtCDFe = 0;
                            bTemTotalMTRe = true;
                            foreach (DataRow _drST in _dt.Select("MTRe = '" + _somtre + "'"))
                            {
                                if (_drST["QtDescarga"].ToString() != "")
                                {
                                    l_qt = Convert.ToDecimal(_drST["QtDescarga"]);
                                    clsCacambas oCaixa = new clsCacambas();
                                    clsCacambaDados oCaixaDados = new clsCacambaDados();
                                    oCaixaDados.PegaDados(oCaixa, _drST["NumeroCaixa"].ToString());
                                    if (_drST["Unidade"].ToString().ToUpper() == "CX")
                                        l_qt = Convert.ToDecimal(_drST["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drST["QtDescarga"]);
                                    else if (_drST["Unidade"].ToString().ToUpper() == "M3" || _drST["Unidade"].ToString().ToUpper() == "M³")
                                        l_qt = Convert.ToDecimal(_drST["M3PorTon"]) * Convert.ToDecimal(_drST["QtDescarga"]) * 1000;
                                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                                }
                            }
                            clsCDFeDados _oCDFe = new clsCDFeDados();
                            //foreach (DataRow _drST in _dt.Select("MTRe = '" + _somtre + "'"))
                            //{
                            //    if (_drST["QtdeCDFe"].ToString() != "") // && _drST["CodigoIbama"].ToString() == _codigoIbamaAnterior)
                            //        _qtCDFe = _qtCDFe + Convert.ToDecimal(_drST["QtdeCDFe"].ToString());
                            //    //break;
                            //}
                            _qtCDFe = _oCDFe.RetornaTotalCDFe(_somtre, "");
                            AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                            AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                        }
                        l_qtSbTlPorMTRe = 0;
                        _qtCDFe = 0;
                    }
                    if (dr["QtDescarga"].ToString() != "")
                    {
                        l_qt = Convert.ToDecimal(dr["QtDescarga"]);
                        clsCacambas oCaixa = new clsCacambas();
                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                        oCaixaDados.PegaDados(oCaixa, dr["NumeroCaixa"].ToString());
                        if (dr["Unidade"].ToString().ToUpper() == "CX")
                            l_qt = Convert.ToDecimal(dr["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(dr["QtDescarga"]);
                        else if (dr["Unidade"].ToString().ToUpper() == "M3" || dr["Unidade"].ToString().ToUpper() == "M³")
                            l_qt = Convert.ToDecimal(dr["M3PorTon"]) * Convert.ToDecimal(dr["QtDescarga"]) * 1000;
                        //_vlSbTl = _vlSbTl + l_qt;
                        //l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                        //_valorTotalGeral = _valorTotalGeral + l_qt;
                    }
                    if (geral.IsNumeric(dr["QtdeCDFe"].ToString()) && dr["QtdeCDFe"].ToString() != "")
                    {
                        _qtCDFe = Convert.ToDecimal(dr["QtdeCDFe"].ToString());
                    }
                    DataRow drMTRe = _dt.Rows[_i];
                    _dtTotaisMTRe = new DataTable();
                    if (_MTReAnterior != dr["MTRe"].ToString())
                    {
                        if (_i + 1 < _dt.Rows.Count)
                            drMTRe = _dt.Rows[_i + 1];
                    }
                    clsCDFe oCDFe = new clsCDFe();
                    clsCDFeDados oCDFeDados = new clsCDFeDados();
                    string sExisteCodigoIBAMA = "";
                    string _cnpj_cpf = "";
                    sExisteCodigoIBAMA = oCDFeDados.ExisteMTReComMesmoCodigoIbama(dr["MTRe"].ToString(), dr["CodigoIBAMA"].ToString());
                    _cnpj_cpf = oCDFeDados.RetornaMTReExisteEmClienteDiferente(dr["MTRe"].ToString(), dr["CNPJ_CPF"].ToString());
    
                    _dtTotaisMTRe = new DataTable();
                    if (dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["MTRe"].ToString() != "-1")
                        _dtTotaisMTRe = oLancamentoMTRDados.RetornaTotalMTRe(dr["MTRe"].ToString(), "", Data1.Data, Data2.Data, DESTINOFINAL1.Valor);
                    _t1 = 0;
                    _t2 = 0;
                    if (_dtTotaisMTRe.Rows.Count > 0)
                    {
                        if (_dtTotaisMTRe.Rows[0][0].ToString() != "")
                            _t1 = Convert.ToDecimal(_dtTotaisMTRe.Rows[0][0]);
                        if (_dtTotaisMTRe.Rows[0][1].ToString() != "")
                            _t2 = Convert.ToDecimal(_dtTotaisMTRe.Rows[0][1]);
                    }
                    bool bEhDiferenteNaLinha = false;
                    if (l_qt != _qtCDFe && !bTemTotalMTRe)
                        bEhDiferenteNaLinha = true;
                    if (_t1 == _t2)
                        bEhDiferenteNaLinha = false;
                    if (_regsMTReIguaisAtual == 1)
                        bTemTotalMTRe = false;
    
                    // nenhum chacado
                    if (_regsMTReIguaisAtual > 1 && _MTReAnterior.Length > 8 && _MTReAnterior != dr["MTRe"].ToString())
                    {
                        int _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "'", "").Length; //"' and CodigoIBAMA = '" + _codigoIbamaAnterior + "'", "").Length;
                        if (_regsMTReIguaisAtual > 1 && _regsMTReAnteriorIguais <= 1)
                        {
                            AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                        }
                    }
    
                    if (_t1 != _t2 || dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                    {
                        if (_t1 == 0)
                            _t1 = l_qt;
                        if (_t2 == 0)
                            _t2 = _qtCDFe;
                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, bTemTotalMTRe);
                        _qtCDFe = 0;
                    }
                    else if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") == dr["PlacasNaCDFe"].ToString())
                    {
                        // nenhum chacado
                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, bTemTotalMTRe);
                        _qtCDFe = 0;
                    }
    
                    _MTReAnterior = dr["MTRe"].ToString();
                    _CDFeAnterior = dr["NumeroCDFe"].ToString();
                    _codigoIbamaAnterior = dr["CodigoIBAMA"].ToString();
                }
    
                string _somtre2 = geral.Left(_MTReAnterior, 10);
                int _regsMTReIguais2 = _dt.Select("MTRe = '" + _somtre2 + "'").Length;
                if (_regsMTReIguais2 > 1)
                {
                    decimal l_qtSbTlPorMTRe2 = 0;
                    decimal _qtCDFe2 = 0;
                    foreach (DataRow _drST in _dt.Select("MTRe = '" + _somtre2 + "'"))
                    {
                        if (_drST["QtDescarga"].ToString() != "")
                        {
                            l_qt = Convert.ToDecimal(_drST["QtDescarga"]);
                            clsCacambas oCaixa = new clsCacambas();
                            clsCacambaDados oCaixaDados = new clsCacambaDados();
                            oCaixaDados.PegaDados(oCaixa, _drST["NumeroCaixa"].ToString());
                            if (_drST["Unidade"].ToString().ToUpper() == "CX")
                                l_qt = Convert.ToDecimal(_drST["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drST["QtDescarga"]);
                            else if (_drST["Unidade"].ToString().ToUpper() == "M3" || _drST["Unidade"].ToString().ToUpper() == "M³")
                                l_qt = Convert.ToDecimal(_drST["M3PorTon"]) * Convert.ToDecimal(_drST["QtDescarga"]) * 1000;
                            l_qtSbTlPorMTRe2 = l_qtSbTlPorMTRe2 + l_qt;
                        }
                    }
                    foreach (DataRow _drST in _dt.Select("MTRe = '" + _somtre2 + "'"))
                    {
                        if (_drST["QtdeCDFe"].ToString() != "")
                            _qtCDFe2 = _qtCDFe2 + Convert.ToDecimal(_drST["QtdeCDFe"].ToString());
                       //break;
                    }
                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe2, _qtCDFe2);
                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
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
                AddRow(_table, _row, _cell, "Peso total geral", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[13], true);
                // adicionar linha com total geral
                if (chkMostrarCDFe.Checked)
                {
                    AddRow(_table, _row, _cell, "", iColWidth[14], false, false);
                    AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
                    AddRow(_table, _row, _cell, "", iColWidth[16], false, false);
                    AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
                }
                Panel1.Controls.Add(_table);
            }
        }
    
        private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, 
                            bool pAlinCentral = false, bool pBackColorYellou = false, bool pBackColorRed = false, bool pForeColor = false)
        {
            Label _lbl = new Label();
            _lbl.Text = pText + "&nbsp;";
            _lbl.Width = pWidth;
            _lbl.Font.Name = "Tahoma";
            _lbl.Font.Size = 8;
            _lbl.Attributes.CssStyle.Add("margin-top", "0");
            if (pForeColor)
            {
                _lbl.ForeColor = System.Drawing.Color.Black;
                _lbl.Font.Size = 8;
            }
            else
                _lbl.ForeColor = System.Drawing.Color.Black;
            if (pBackColorYellou)
            {
                _lbl.BackColor = System.Drawing.Color.White;
                _lbl.Font.Bold = true;
                if (pWidth == 80)
                    _lbl.ForeColor = System.Drawing.Color.DarkGreen;
                else
                    _lbl.ForeColor = System.Drawing.Color.IndianRed;
                if ((pText == "" || pText == "&nbsp;") && pWidth == 79)
                    _lbl.Text = "-emitir-";
            }
            else if (pBackColorRed)
            {
                _lbl.BackColor = System.Drawing.Color.White;
                _lbl.Font.Bold = true;
                if (pWidth == 80)
                    _lbl.ForeColor = System.Drawing.Color.DarkGreen;
                else
                    _lbl.ForeColor = System.Drawing.Color.IndianRed;
            }
            else
            {
                _lbl.BackColor = System.Drawing.Color.White;
                _lbl.ForeColor = System.Drawing.Color.Black;
            }
    
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
            lblMensagem.Text = "";
            Relatorio();
            Session["Panel1"] = Panel1;
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
    
        private string Cabecalho(int _pagina)
        {
            string sx = "\n\n";
            sx = sx + "  Relatório Recebimento MTR-e e CDF-e por Destino Final - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            sx = sx + "  Emissão: " + DateTime.Now.ToString("dd/MM/yy") + "  -  Pág: " + _pagina.ToString()  +"  \n\n";
            oDestinoFinalDados = new clsDestinoFinalDados();
            oDestinoFinal = new clsDestinoFinal();
            oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
            sx = sx + "  Destinador: " + oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") \n\n";
            sx = sx + "    Data    Código Cliente              CNPJ               Quantidade  Resíduo                   Nº Ticket  Motorista                   Placas   Código    Descrição IBAMA                  Nº MTR-e   Peso Total ";
            if (chkMostrarCDFe.Checked)
                sx = sx + " Nº CDFe   Qtde CDFe Situação \n";
            else
                sx = sx + "\n";
            sx = sx + "                                                           Individual                                                                                                                                  Kg \n";
            sx = sx + " ────────── ────── ──────────────────── ────────────────── ─────────── ───────────────────────── ────────── ─────────────────────────── ──────── ───────── ──────────────────────────────── ────────── ───────────";
            if (chkMostrarCDFe.Checked)
                sx = sx + " ───────── ───────── ──────────── \n";
            else
                sx = sx + "\n";
            return sx;
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
    
            string NomeArq = "Relatorio_MovResiduosPorDestinoFinal.xls";
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
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor));
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo);
            for (int i = 1; i <= 13; i++)
                _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            Grade.DataSource = _dt;
            Grade.DataBind();
    
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                    Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);
                
                decimal PesoTotalCliente = 0;
                string cnpjAnterior = "";
                if (Grade.Rows.Count > 0)
                    PesoTotalCliente = Convert.ToDecimal(Grade.Rows[0].Cells[4].Text);
                if (Grade.Rows.Count > 1)
                    cnpjAnterior = Grade.Rows[1].Cells[3].Text;
                for (int i = 0; i < Grade.Rows.Count; i++)
                {
                    GridViewRow gvr = Grade.Rows[i];
                    gvr.Cells[10].Text = geral.Left(gvr.Cells[10].Text, 30);
    
                    if (gvr.Cells[3].Text != cnpjAnterior)
                    {
                        if (i > 0)
                            Grade.Rows[i - 1].Cells[12].Text = PesoTotalCliente.ToString("N2");
                        else
                            Grade.Rows[i].Cells[12].Text = PesoTotalCliente.ToString("N2");
                        PesoTotalCliente = 0;
                    }
                    if (gvr.Cells[4].Text != "" && gvr.Cells[4].Text != "&nbsp;")
                        PesoTotalCliente = PesoTotalCliente + Convert.ToDecimal(gvr.Cells[4].Text);
    
                    cnpjAnterior = gvr.Cells[3].Text;
                    if (gvr.Cells[0].Text != "" && gvr.Cells[0].Text != "&nbsp;")
                    {
                        gvr.Cells[0].Text = DateTime.Parse(gvr.Cells[0].Text).ToShortDateString();
                    }
                }
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
                lblTitulo.Text = "Relatorio Movimentacao de Residuos por Destino Final - Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " +
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
                lblColuna.Text = "Destinador: " + oDestinoFinal.Nome + " (" + cdDestino + ")";
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
                table.Rows.Add(row);
    
                //Data Código Cliente CNPJ Quantidade Individual Resíduo Nº Ticket Motorista Placas Código Descrição IBAMA Nº MTR - e Peso Total Kg
                Grade.HeaderRow.Cells[0].Text = "Data descarga";
                Grade.HeaderRow.Cells[1].Text = "Codigo";
                Grade.HeaderRow.Cells[2].Text = "Cliente";
                Grade.HeaderRow.Cells[3].Text = "CNPJ Cliente";
                Grade.HeaderRow.Cells[4].Text = "Qtdade individual";
                Grade.HeaderRow.Cells[5].Text = "Descricao residuo";
                Grade.HeaderRow.Cells[6].Text = "No.Ticket";
                Grade.HeaderRow.Cells[7].Text = "Motorista";
                Grade.HeaderRow.Cells[8].Text = "Placas";
                Grade.HeaderRow.Cells[9].Text = "Codigo";
                Grade.HeaderRow.Cells[10].Text = "Descricao IBAMA";
                Grade.HeaderRow.Cells[11].Text = "No.MTR-e";
                Grade.HeaderRow.Cells[12].Text = "Peso total p/cliente";
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                row.Cells[0].Controls.Add(Grade);
                table.Rows.Add(row);
    
            }
            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
        }
    
        protected void btnMostraDestinoFinal_Click(object sender, EventArgs e)
        {
            oDestinoFinal = new clsDestinoFinal();
            oDestinoFinalDados = new clsDestinoFinalDados();
            if (DESTINOFINAL1.Valor != "")
            {
                oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
                if (oDestinoFinal.Ativo == 0)
                    DESTINOFINAL1.Texto = "Destino final inativo!";
                else
                {
                    if (oDestinoFinal.NomeFantasia != "")
                        DESTINOFINAL1.Texto = oDestinoFinal.NomeFantasia;
                    else
                        DESTINOFINAL1.Texto = "Destino final inválivo!";
                }
            }       
        }
    }
}