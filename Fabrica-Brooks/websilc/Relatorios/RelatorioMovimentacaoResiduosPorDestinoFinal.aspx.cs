using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using PdfSharp.Pdf;

public partial class RelatorioMovimentacaoResiduosPorDestinoFinal : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
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
    private int _linhas = 0;

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
            btnEnviarEmail.Enabled = true;
            //if (oUsuario.Nome == "teixeira")
            //    btnEnviarEmail.Enabled = true;
            //else
            //{
            //    btnEnviarEmail.Enabled = false;
            //    btnEnviarEmail.Text = "Enviar e-mail\n Em manutenção.";
            //}
        }
    }
    private void AddTotalMTR_e(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, string _MTReAnterior, decimal l_qtSbTlPorMTRe, decimal _qtCDFe)
    {
        if (!bTemNumeroCDFe)
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
            AddRow(_table, _row, _cell, "", iColWidth[11], true, false, false, false, false, true);
            AddRow(_table, _row, _cell, "Total MTR-e", iColWidth[12], true, false, false, false, false, true);
            if (!chkMostrarCDFe.Checked)
            {
                AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, false, false, true);
            }
            else
            {
                if (Math.Round(_qtCDFe, 1) != Math.Round(l_qtSbTlPorMTRe, 1))
                {
                    if (chkMostrarCDFe.Checked)
                        AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, false, false, true);
                    else
                        AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, true, false, true);
                }
                else
                    AddRow(_table, _row, _cell, l_qtSbTlPorMTRe.ToString("N2"), iColWidth[13], true, false, false, false, false, true, true);
            }
            if (chkMostrarCDFe.Checked)
            {
                AddRow(_table, _row, _cell, "", iColWidth[14], false, true);
                if (Math.Round(_qtCDFe, 1) != Math.Round(l_qtSbTlPorMTRe, 1))
                    AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false, false, false, true);
                else
                    AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
                AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
            }
        }
    }

    private void AddLinhaSubTotais(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, decimal _vlSbTl)
    {
        if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
        {
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
        }
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
    private void AddLinhaTracoNoTotal(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, decimal _vlSbTl)
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
        if (chkMostrarCDFe.Checked)
            AddRow(_table, _row, _cell, "─────────────", iColWidth[13], false, true);
        else
            AddRow(_table, _row, _cell, "───────────", iColWidth[13], false, true);
        if (chkMostrarCDFe.Checked)
        {
            AddRow(_table, _row, _cell, "", iColWidth[14], false, false);
            AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
            AddRow(_table, _row, _cell, "", iColWidth[16], true, false);
            AddRow(_table, _row, _cell, "", iColWidth[17], false, false);
        }
    }
    private void AddLinhaDados(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, DataRow dr, decimal l_qt, decimal _qtCDFe, string _MTReAnterior, 
                               DataRow drMTRe, int _i, string _cnpj_cpf, string sExisteCodigoIBAMA, bool pEhDiferenteNaLinha = false, bool pEhTotalMTRe = false,
                               decimal _t1 = 0)
    {
        _linhas++;
        AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataDescarga"]).ToString("dd/MM/yy"), iColWidth[1], true);
        AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[2], true);
        AddRow(_table, _row, _cell, geral.Left(dr["Nome"].ToString(), 18), iColWidth[3], false);

        if (_cnpj_cpf != "" && chkCNPJ.Checked)
            AddRow(_table, _row, _cell, geral.RetiraLetras(_cnpj_cpf), iColWidth[4], false, false, false, true);
        else
            AddRow(_table, _row, _cell, geral.RetiraLetras(dr["CNPJ_CPF"].ToString()), iColWidth[4], false);

        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2") + " " + geral.Left(dr["Unidade"].ToString(), 2), iColWidth[5], true);
        AddRow(_table, _row, _cell, geral.Left(dr["DescricaoReduzida"].ToString(), 22), iColWidth[6], false);
        AddRow(_table, _row, _cell, dr["Ticket"].ToString(), iColWidth[7], false);
        AddRow(_table, _row, _cell, dr["NomeMotorista"].ToString().Split(" "[0])[0].ToString(), iColWidth[8], false);

        if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString() && chkPlacas.Checked)
            AddRow(_table, _row, _cell, dr["Placas"].ToString(), iColWidth[9], false, false, false, true);
        else
            AddRow(_table, _row, _cell, dr["Placas"].ToString(), iColWidth[9], false);

        clsCDFeDados oCDFeDados = new clsCDFeDados();
        if (!chkMostrarCDFe.Checked)
            AddRow(_table, _row, _cell, dr["CodigoIbama"].ToString(), iColWidth[10], false);
        else if (!oCDFeDados.ExisteMTReCodigoIbamaDiferente(dr["MTRe"].ToString(), dr["CodigoIbama"].ToString()))
            AddRow(_table, _row, _cell, dr["CodigoIbama"].ToString(), iColWidth[10], false, false, false, true);
        else
            AddRow(_table, _row, _cell, dr["CodigoIbama"].ToString(), iColWidth[10], false);

        AddRow(_table, _row, _cell, geral.Left(dr["Descricao"].ToString().Replace("–", ""), 30), iColWidth[11], false);

        string _lnk0 = "";
        _lnk0 = _lnk0 + "<span style='cursor: pointer; color: blue; text-decoration: underline;' onclick=_linkMTRe(";
        _lnk0 = _lnk0 + dr["MTRe"].ToString() + ")";
        _lnk0 = _lnk0 + ">" + dr["MTRe"].ToString() + "</span>";
        AddRow(_table, _row, _cell, _lnk0, iColWidth[12], false);

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
            if (pEhTotalMTRe) // && Math.Round(_t1, 1) == Math.Round(_qtCDFe, 1))
                AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true);
            else if (Math.Round(l_qt, 1) != Math.Round(_qtCDFe, 1) || pEhDiferenteNaLinha)
                AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true, false, false, false, true);
            else
                AddRow(_table, _row, _cell, l_qt.ToString("N2"), iColWidth[13], true);

            if (dr["NumeroCDFe"].ToString() == "0" || dr["NumeroCDFe"].ToString() == "")
                AddRow(_table, _row, _cell, "", iColWidth[14], false, false, false, true);
            else
            {
                string _lnk = "";
                _lnk = _lnk + "<span style='cursor: pointer; color: blue; text-decoration: underline;' onclick=_linkCDFe(";
                _lnk = _lnk + dr["NumeroCDFe"].ToString() + ")";
                _lnk = _lnk + ">" + dr["NumeroCDFe"].ToString() + "</span>";
                AddRow(_table, _row, _cell, _lnk, iColWidth[14], false);
            }
            drMTRe = _dt.Rows[_i];

            if (!pEhTotalMTRe)
            {
                if (Math.Round(l_qt, 1) != Math.Round(_qtCDFe, 1))
                    AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false, false, false, true);
                else
                    AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false);
            }
            else if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()) && pEhTotalMTRe)
            {
                if (_i + 1 < _dt.Rows.Count)
                    drMTRe = _dt.Rows[_i + 1];

                if (drMTRe["MTRe"].ToString() == dr["MTRe"].ToString() && (_i + 1 < _dt.Rows.Count) && !pEhTotalMTRe)
                {
                    if (Math.Round(l_qt, 1) != Math.Round(_qtCDFe, 1))
                        AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false, false, false, true);
                    else
                        AddRow(_table, _row, _cell, _qtCDFe.ToString("N2"), iColWidth[15], true, false);
                }
                else
                {
                    AddRow(_table, _row, _cell, "", iColWidth[15], true, false);
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
                                        int _i, string _cnpj_cpf, string sExisteCodigoIBAMA, bool pEhDiferenteNaLinha, DataTable _dtTotaisMTRe, decimal _t1, decimal _t2,
                                        bool pEhTotalMTRe = false)
    {
        if (_dtTotaisMTRe.Rows.Count > 0)
        {
            if ((dr["ClienteEmissaoMTReRCD"].ToString() == "" || dr["ClienteEmissaoMTReRCD"].ToString() == "0") && dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["CodigoResiduo"].ToString() == "160")
            {
                _vlSbTl = _vlSbTl + l_qt;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                _valorTotalGeral = _valorTotalGeral + l_qt;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, pEhTotalMTRe, _t1);
            }
            else if (dr["ClienteEmissaoMTReRCD"].ToString() == "1")
            {
                _vlSbTl = _vlSbTl + l_qt;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                _valorTotalGeral = _valorTotalGeral + l_qt;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, pEhTotalMTRe, _t1);
            }
            else if (dr["CodigoResiduo"].ToString() != "160")
            {
                _vlSbTl = _vlSbTl + l_qt;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                _valorTotalGeral = _valorTotalGeral + l_qt;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, pEhDiferenteNaLinha, pEhTotalMTRe, _t1);
            }
        }
        else
        {
            if (dr["ClienteEmissaoMTReRCD"].ToString() == "0" && dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["CodigoResiduo"].ToString() == "160")
            {
                _vlSbTl = _vlSbTl + _t1;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                _valorTotalGeral = _valorTotalGeral + _t1;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, pEhTotalMTRe, _t1);
            }
            else if (dr["ClienteEmissaoMTReRCD"].ToString() == "1")
            {
                _vlSbTl = _vlSbTl + _t1;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                _valorTotalGeral = _valorTotalGeral + _t1;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, pEhTotalMTRe, _t1);
            }
            else if (dr["CodigoResiduo"].ToString() != "160")
            {
                _vlSbTl = _vlSbTl + _t1;
                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + _t1;
                _valorTotalGeral = _valorTotalGeral + _t1;
                AddLinhaDados(_table, _row, _cell, iColWidth, dr, _t1, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, false, pEhTotalMTRe, _t1);
            }
        }
    }
    private void Relatorio()
    {
        if (!chkSoEmDTR.Checked && DESTINOFINAL1.Valor == "")
            return;

        btnConfirmar.Visible = false;
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

        AddRow(_table, _row, _cell, "Relatório de movimentação de resíduos por destino final - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 860, false, true);
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
            AddRow(_table, _row, _cell, oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") " + oDestinoFinal.NomeFantasia + " <br />", 860, false, true);
        }
        Panel1.Controls.Add(_table);

        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        if (!chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), false, false, true);
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, false, false, true);
        }
        else if (chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, Convert.ToInt32(CLIENTE1.Valor), false, false, true);
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, 0, false, false, true);
        }
        if (_dt.Rows.Count > 0)
        {
            _linhas = 0;
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
            iColWidth[5] = 68;    //Quantde.Individual
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
                iColWidth[15] = 72;   //Qtde MTRe
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
            AddRow(_table, _row, _cell, "Quantidade Individual", iColWidth[5], false, true);
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
                AddRow(_table, _row, _cell, "Qtde MTR-e", iColWidth[15], false, true);
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

            if (chkCDFe.Checked && !chkPeso.Checked)
            {
                // pra não mostrar os com CDF-e emitida
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (_dr["NumeroCDFe"].ToString() != "")
                        if (_dr["NumeroCDFe"].ToString() != "0")
                            _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                }
            }
            else if (!chkCDFe.Checked && chkPeso.Checked)
            {
                // pra não mostrar Pesos iguais
                decimal tMtrIbama = 0;
                decimal _qtCDFeConvertido = 0;
                // Lembrete: esse algoritmo só funciona se, a ordem for `número MTRe + Código IBAMA`
                foreach (DataRow _dr in _dt.Rows)
                {
                    // 1º - 2306115286
                    // 2º - 2305144184 - não pode aparecer, pois pesos iguais
                    // 2306108518
                    // pegar numero MTR-e e Codigo do Ibama 
                    // gerar um total por esse filtro
                    // verificar se existe mais de um, pois dai tem que acumular qtdescarga, para comparar com QtdeCDFe, caso quantidades total igual não mostrar 

                    if (_dr["MTRe"].ToString() == "2306108518")
                    {
                        string _tst = _dr["MTRe"].ToString();
                        _tst = _tst;
                    }

                    string _f1 = "MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'";
                    _qtCDFeConvertido = 0;
                    if (_dr["QtdeCDFe"].ToString() != "")
                    {
                        _qtCDFeConvertido = Convert.ToDecimal(_dr["QtdeCDFe"].ToString());
                    }
                    int _qtMtrs = _dt.Select(_f1, "").Length;
                    if (_qtMtrs > 1)
                    {
                        tMtrIbama = 0;
                        foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                        {
                            if (_dr2["qtDescarga"].ToString() != "")
                            {
                                tMtrIbama = tMtrIbama + Convert.ToDecimal(_dr2["qtDescarga"].ToString());
                            }
                        }
                        if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                        {
                            foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                            {
                                _dr2["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                            }
                        }
                    }
                    else if (_qtMtrs == 1)
                    {
                        tMtrIbama = 0;
                        if (_dr["qtDescarga"].ToString() != "")
                        {
                            tMtrIbama = Convert.ToDecimal(_dr["qtDescarga"].ToString());
                        }
                        if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                        {
                            _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                        }
                    }
                }
            }
            else if (chkCDFe.Checked && chkPeso.Checked)
            {
                // pra não mostrar Pesos iguais e nem cdfes emitidas com pesos iguais
                decimal tMtrIbama = 0;
                decimal _qtCDFeConvertido = 0;
                // Lembrete: esse algoritmo só funciona se, a ordem for `número MTRe + Código IBAMA`
                foreach (DataRow _dr in _dt.Rows)
                {
                    string _f1 = "MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'";
                    _qtCDFeConvertido = 0;
                    if (_dr["QtdeCDFe"].ToString() != "")
                    {
                        _qtCDFeConvertido = Convert.ToDecimal(_dr["QtdeCDFe"].ToString());
                    }
                    int _qtMtrs = _dt.Select(_f1, "").Length;
                    if (_qtMtrs > 1)
                    {
                        tMtrIbama = 0;
                        foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                        {
                            if (_dr2["qtDescarga"].ToString() != "")
                            {
                                tMtrIbama = tMtrIbama + Convert.ToDecimal(_dr2["qtDescarga"].ToString());
                            }
                        }
                        if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                        {
                            foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                            {
                                if (_dr2["NumeroCDFe"].ToString() != "")
                                    if (_dr2["NumeroCDFe"].ToString() != "0")
                                        _dr2["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                            }
                        }
                    }
                    else if (_qtMtrs == 1)
                    {
                        tMtrIbama = 0;
                        if (_dr["qtDescarga"].ToString() != "")
                        {
                            tMtrIbama = Convert.ToDecimal(_dr["qtDescarga"].ToString());
                        }
                        if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                        {
                            if (_dr["NumeroCDFe"].ToString() != "")
                                if (_dr["NumeroCDFe"].ToString() != "0")
                                    _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                        }
                    }
                }
            }

            _vlSbTl = 0;
            l_qtSbTlPorMTRe = 0;
            decimal l_qtSbTlPorMTReCDFe = 0;
            _valorTotalGeral = 0;
            decimal l_qt = 0;
            decimal _t1 = 0;
            string _CNPJClienteAnterior = "";
            bTemNumeroCDFe = false;

            if (_dt.Rows.Count > 0)
                _CNPJClienteAnterior = _dt.Rows[0]["CNPJ_CPF"].ToString();
            string _MTReAnterior = ""; 
            string _CDFeAnterior = "-1";  
            bool EhLinhaDeTotalMTRe = false;
            for (int _i = 0; _i < _dt.Rows.Count; _i++)
            {
                DataRow dr = _dt.Rows[_i];

                if (dr["DataDescarga"].ToString() != "01/01/0100 00:00:00") // não mostra cdf-e emitida e ou pesos iguais
                {
                    EhLinhaDeTotalMTRe = false;
                    if (dr["MTRe"].ToString() == "2306108518") 
                    {
                        // teste 
                        _vlSbTl = _vlSbTl + 0;
                    }
                    if (dr["Situacao"].ToString().IndexOf("Armazenamento") > -1)
                    {
                        dr["Situacao"] = "Armaz.Temp.";
                    }

                    l_qt = 0;
                    decimal _qtCDFe = 0;
                    _t1 = 0;
                    decimal _t2 = 0;
                    DataTable _dtTotaisMTRe = new DataTable();
                    // tudo com total
                    if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()) && chkMostrarCDFe.Checked)
                    {
                        string _codigoIBAMA = dr["CodigoIBAMA"].ToString();
                        string MTRe2Anterior = "";
                        string AtualMTRe = dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString();
                        if (_i - 2 < _dt.Rows.Count && _i > 1)
                        {
                            MTRe2Anterior = _dt.Rows[_i - 2]["MTRe"].ToString() + _dt.Rows[_i - 2]["CodigoIBAMA"].ToString();
                            if (geral.IsNumeric(_dt.Rows[_i - 2]["QtdeCDFe"].ToString()) && _dt.Rows[_i - 2]["QtdeCDFe"].ToString() != "")
                                _qtCDFe = Convert.ToDecimal(_dt.Rows[_i - 2]["QtdeCDFe"].ToString());
                            _codigoIBAMA = _dt.Rows[_i - 2]["CodigoIBAMA"].ToString();
                        }
                        if (_MTReAnterior != AtualMTRe && MTRe2Anterior == _MTReAnterior && _MTReAnterior != "")
                        {
                            clsCDFeDados _oCDFeDados = new clsCDFeDados();
                            l_qtSbTlPorMTReCDFe = _oCDFeDados.RetornaTotalCDFe(geral.Left(_MTReAnterior, _MTReAnterior.Length - 8), dr["CodigoIBAMA"].ToString());
                            if (chkPeso.Checked)
                            {
                                _dtTotaisMTRe = oLancamentoMTRDados.RetornaTotalMTRe(geral.Left(_MTReAnterior, _MTReAnterior.Length - 8), _codigoIBAMA, Data1.Data, Data2.Data, DESTINOFINAL1.Valor);
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (_dtTotaisMTRe.Rows[0][0].ToString() != "")
                                        _t1 = Convert.ToDecimal(_dtTotaisMTRe.Rows[0][0]);
                                    if (_dtTotaisMTRe.Rows[0][1].ToString() != "")
                                        _t2 = Convert.ToDecimal(_dtTotaisMTRe.Rows[0][1]);
                                }
                                if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                                {
                                    if (geral.IsNumeric(dr["QtdeCDFe"].ToString()) && dr["QtdeCDFe"].ToString() != "")
                                    {
                                        _qtCDFe = Convert.ToDecimal(dr["QtdeCDFe"].ToString());
                                    }
                                    if (_t1 != _t2)
                                    {
                                        int _regsMTReIguais = 0;
                                        int _regsMTReAnteriorIguais = 0;
                                        if (_MTReAnterior.Length > 7)
                                            _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                        _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                                        if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais >= 1)
                                        {
                                            //AddTotalMTR_e(_table, _row, _cell, iColWidth, geral.Left(_MTReAnterior, _MTReAnterior.Length - 8), _t1, _t2);
                                            //AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                        }
                                    }
                                    bTemNumeroCDFe = false;
                                }
                            }
                            l_qtSbTlPorMTReCDFe = 0;
                        }
                        l_qtSbTlPorMTRe = 0;
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
                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt;
                    }
                    if (geral.IsNumeric(dr["QtdeCDFe"].ToString()) && dr["QtdeCDFe"].ToString() != "")
                    {
                        _qtCDFe = Convert.ToDecimal(dr["QtdeCDFe"].ToString());
                    }
                    DataRow drMTRe = _dt.Rows[_i];
                    _dtTotaisMTRe = new DataTable();
                    if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                    {
                        if (_i + 1 < _dt.Rows.Count)
                            drMTRe = _dt.Rows[_i + 1];
                    }
                    clsCDFe oCDFe = new clsCDFe();
                    clsCDFeDados oCDFeDados = new clsCDFeDados();
                    string sExisteCodigoIBAMA = "";
                    string _cnpj_cpf = "";
                    sExisteCodigoIBAMA = ""; // oCDFeDados.ExisteMTReComMesmoCodigoIbama(dr["MTRe"].ToString(), dr["CodigoIBAMA"].ToString());
                    _cnpj_cpf = oCDFeDados.RetornaMTReExisteEmClienteDiferente(dr["MTRe"].ToString(), dr["CNPJ_CPF"].ToString());

                    _dtTotaisMTRe = new DataTable();
                    if (dr["MTRe"].ToString() != "0" && dr["MTRe"].ToString() != "" && dr["MTRe"].ToString() != "-1")
                        _dtTotaisMTRe = oLancamentoMTRDados.RetornaTotalMTRe(dr["MTRe"].ToString(), dr["CodigoIBAMA"].ToString(), Data1.Data, Data2.Data, DESTINOFINAL1.Valor);
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
                    if (l_qt != _qtCDFe || _t1 != _t2)
                        bEhDiferenteNaLinha = true;
                    if (_t1 != _t2 || dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                    {
                        if (_t1 == 0)
                            _t1 = l_qt;
                        if (_t2 == 0)
                            _t2 = _qtCDFe;
                        // só CNPJ
                        if (chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (_cnpj_cpf != "")
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }

                        } // só IBAMA
                        else if (!chkCNPJ.Checked && chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }
                        } // só CDFe
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0")
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }
                        } // só Placas
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }
                        } // só peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                        {
                            _t2 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            if (_MTReAnterior.Length > 7)
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais == 1 && _t1 != _t2)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            EhLinhaDeTotalMTRe = false;
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;
                            if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                // o anterior é diferente do atual, mas só têm 1 registro, então pegar dados do registro atual. 
                                string _filtroMTReCodigoIBAMA = "MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'";
                                //if (_regsMTReIguais == 1)
                                //    _filtroMTReCodigoIBAMA = "MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'";
                                l_qtSbTlPorMTRe = 0;
                                decimal l_qt2 = 0;
                                foreach (DataRow _drtm in _dt.Select(_filtroMTReCodigoIBAMA))
                                {
                                    if (_drtm["QtDescarga"].ToString() != "" && geral.Left(_MTReAnterior, 10) == _drtm["MTRe"].ToString() &&
                                        _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                    {
                                        l_qt2 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                        clsCacambas oCaixa = new clsCacambas();
                                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                                        oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                        if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                        else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt2;
                                    }
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    if (l_qtSbTlPorMTRe != _qtCDFe)
                                    {
                                        AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                        AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    }
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            if (_t1 != _t2) // (l_qtSbTlPorMTRe != _qtCDFe)
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

                        }  // só CDFe e Peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                        {
                            //if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _t1 != _t2)
                            //{
                            //    if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                            //    {
                            //        //AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                            //        _vlSbTl = 0;
                            //    }
                            //    AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            //    _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            //}
                            //else if (dr["NumeroCDFe"].ToString() != "" && dr["NumeroCDFe"].ToString() != "0")
                            //{
                            //    bTemNumeroCDFe = true;
                            //}
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais <= 1)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;
                            if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                // o anterior é diferente do atual, mas só têm 1 registro, então pegar dados do registro atual. 
                                string _filtroMTReCodigoIBAMA = "MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'";
                                //if (_regsMTReIguais == 1)
                                //    _filtroMTReCodigoIBAMA = "MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'";
                                l_qtSbTlPorMTRe = 0;
                                decimal l_qt2 = 0;
                                foreach (DataRow _drtm in _dt.Select(_filtroMTReCodigoIBAMA))
                                {
                                    if (_drtm["QtDescarga"].ToString() != "" && geral.Left(_MTReAnterior, 10) == _drtm["MTRe"].ToString() &&
                                        _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                    {
                                        l_qt2 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                        clsCacambas oCaixa = new clsCacambas();
                                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                                        oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                        if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                        else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt2;
                                    }
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            _t2 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

                        } // só Placas e Peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && chkPlacas.Checked && chkPeso.Checked)
                        {
                            if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString() || _t1 != _t2)
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }
                        } // todos checados
                        else if (chkCNPJ.Checked && chkIBAMA.Checked && chkCDFe.Checked && chkPlacas.Checked && chkPeso.Checked)
                        {
                            if (_t1 != _t2 || dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, true);
                                _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            }
                        } // nenhum checado
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais <= 1)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;
                            if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                // o anterior é diferente do atual, mas só têm 1 registro, então pegar dados do registro atual. 
                                string _filtroMTReCodigoIBAMA = "MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'";
                                //if (_regsMTReIguais == 1)
                                //    _filtroMTReCodigoIBAMA = "MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'";
                                l_qtSbTlPorMTRe = 0;
                                decimal l_qt2 = 0;
                                foreach (DataRow _drtm in _dt.Select(_filtroMTReCodigoIBAMA))
                                {
                                    if (_drtm["QtDescarga"].ToString() != "" && geral.Left(_MTReAnterior, 10) == _drtm["MTRe"].ToString() &&
                                        _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                    {
                                        l_qt2 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                        clsCacambas oCaixa = new clsCacambas();
                                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                                        oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                        if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                        else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt2;
                                    }
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            _t2 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                        }
                        _qtCDFe = 0;
                    }
                    else if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") == dr["PlacasNaCDFe"].ToString())
                    {
                        // só CNPJ
                        if (chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (_cnpj_cpf != "")
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // só IBAMA
                        else if (!chkCNPJ.Checked && chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // só CDFe
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0")
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // só Placas
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && chkPlacas.Checked && !chkPeso.Checked)
                        {
                            if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // só peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                        {
                            _t2 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            if (_MTReAnterior.Length > 7)
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais == 1 && _t1 != _t2)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            EhLinhaDeTotalMTRe = false;
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;
                            if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                // o anterior é diferente do atual, mas só têm 1 registro, então pegar dados do registro atual. 
                                string _filtroMTReCodigoIBAMA = "MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'";
                                //if (_regsMTReIguais == 1)
                                //    _filtroMTReCodigoIBAMA = "MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'";
                                l_qtSbTlPorMTRe = 0;
                                decimal l_qt2 = 0;
                                foreach (DataRow _drtm in _dt.Select(_filtroMTReCodigoIBAMA))
                                {
                                    if (_drtm["QtDescarga"].ToString() != "" && geral.Left(_MTReAnterior, 10) == _drtm["MTRe"].ToString() &&
                                        _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                    {
                                        l_qt2 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                        clsCacambas oCaixa = new clsCacambas();
                                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                                        oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                        if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                        else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                            l_qt2 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qt2;
                                    }
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    if (l_qtSbTlPorMTRe != _qtCDFe)
                                    {
                                        AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                        AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    }
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            if (_t1 != _t2) // (l_qtSbTlPorMTRe != _qtCDFe)
                                AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

                        }  // só CDFe e Peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                        {
                            //if (dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _t1 != _t2)
                            //{
                            //    if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                            //    {
                            //        //AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                            //        _vlSbTl = 0;
                            //    }
                            //    if (_dtTotaisMTRe.Rows.Count > 0)
                            //    {
                            //        if (l_qt != _qtCDFe)
                            //        {
                            //            AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                            //            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (dr["NumeroCDFe"].ToString() != "" && dr["NumeroCDFe"].ToString() != "0")
                            //        bTemNumeroCDFe = true;
                            //}
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais <= 1)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;

                            if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                            {
                                //AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                l_qtSbTlPorMTRe = 0;
                                _regsMTReIguais = 0;
                                foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                {
                                    l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + Convert.ToDecimal(_drtm["qtDescarga"].ToString());
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                    _qtCDFe = 0;
                                    EhLinhaDeTotalMTRe = false;
                                }
                                _vlSbTl = 0;
                            }
                            else if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                l_qtSbTlPorMTRe = 0;
                                _regsMTReIguais = 0;
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    decimal l_qtT = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtDescarga"].ToString() != "")
                                        {
                                            l_qtT = Convert.ToDecimal(_drtm["QtDescarga"]);
                                            clsCacambas oCaixa = new clsCacambas();
                                            clsCacambaDados oCaixaDados = new clsCacambaDados();
                                            oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                            if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                                l_qtT = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                            else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                                l_qtT = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                            l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qtT;
                                        }
                                    }
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            _t2 = 0;
                            foreach (DataRow _drtm in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                    _drtm["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();

                        } // só Placas e Peso
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && chkPlacas.Checked && chkPeso.Checked)
                        {
                            if (dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString() || _t1 != _t2)
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // todos checados
                        else if (chkCNPJ.Checked && chkIBAMA.Checked && chkCDFe.Checked && chkPlacas.Checked && chkPeso.Checked)
                        {
                            if (_t1 != _t2 || dr["NumeroCDFe"].ToString() == "" || dr["NumeroCDFe"].ToString() == "0" || _cnpj_cpf != "" || sExisteCodigoIBAMA != dr["CodigoIBAMA"].ToString() || dr["Placas"].ToString().Replace(" ", "") != dr["PlacasNaCDFe"].ToString())
                            {
                                if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                                {
                                    AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                    _vlSbTl = 0;
                                }
                                if (_dtTotaisMTRe.Rows.Count > 0)
                                {
                                    if (l_qt != _qtCDFe)
                                    {
                                        AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, l_qt, _t2, true);
                                        _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                                    }
                                }
                            }
                        } // nenhum checado
                        else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        {
                            int _regsMTReIguais = 0;
                            int _regsMTReAnteriorIguais = 0;
                            _regsMTReIguais = _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", "").Length;
                            if (_MTReAnterior != (dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString()))
                            {
                                _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais <= 1)
                                {
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                }
                            }
                            if (_regsMTReIguais > 1)
                                EhLinhaDeTotalMTRe = true;

                            if (_CNPJClienteAnterior != dr["CNPJ_CPF"].ToString() && _CNPJClienteAnterior != "")
                            {
                                //AddLinhaSubTotais(_table, _row, _cell, iColWidth, _vlSbTl);
                                l_qtSbTlPorMTRe = 0;
                                _regsMTReIguais = 0;
                                foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                {
                                    decimal l_qtT3 = 0;
                                    if (_drtm["QtDescarga"].ToString() != "")
                                    {
                                        l_qtT3 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                        clsCacambas oCaixa = new clsCacambas();
                                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                                        oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                        if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                            l_qtT3 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                        else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                            l_qtT3 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                        l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qtT3;
                                    }
                                }
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                    _qtCDFe = 0;
                                    //EhLinhaDeTotalMTRe = false;
                                }
                                _vlSbTl = 0;
                            }
                            else if (_MTReAnterior != dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString())
                            {
                                l_qtSbTlPorMTRe = 0;
                                _regsMTReIguais = 0;
                                _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                                if (_regsMTReIguais > 1)
                                {
                                    decimal l_qtT = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtDescarga"].ToString() != "")
                                        {
                                            l_qtT = Convert.ToDecimal(_drtm["QtDescarga"]);
                                            clsCacambas oCaixa = new clsCacambas();
                                            clsCacambaDados oCaixaDados = new clsCacambaDados();
                                            oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                            if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                                l_qtT = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                            else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                                l_qtT = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                            l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qtT;
                                        }
                                    }
                                    _qtCDFe = 0;
                                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                                    {
                                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                        {
                                            _qtCDFe = _qtCDFe + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                            break;
                                        }
                                    }
                                    AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _qtCDFe);
                                    AddLinhaEmBranco(_table, _row, _cell, iColWidth, 0);
                                    l_qtSbTlPorMTRe = 0;
                                }
                            }
                            _t2 = 0;
                            foreach (DataRow _drtm in _dt.Select("MTRe = '" + dr["MTRe"].ToString() + "'")) // and CodigoIBAMA = '" + dr["CodigoIBAMA"].ToString() + "'", ""))
                            {
                                if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                                    _drtm["CodigoIBAMA"].ToString() == dr["CodigoIBAMA"].ToString())
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            AddLinhaDadosCondicao2(_table, _row, _cell, iColWidth, dr, l_qt, _t2, _MTReAnterior, drMTRe, _i, _cnpj_cpf, sExisteCodigoIBAMA, bEhDiferenteNaLinha, _dtTotaisMTRe, _t1, _t2, EhLinhaDeTotalMTRe);
                            _CNPJClienteAnterior = dr["CNPJ_CPF"].ToString();
                        }
                        _qtCDFe = 0;
                    }
                    if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && chkPeso.Checked)
                        _MTReAnterior = dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString();
                    else if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
                        _MTReAnterior = dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString();
                    else
                        _MTReAnterior = dr["MTRe"].ToString() + dr["CodigoIBAMA"].ToString();
                    _CDFeAnterior = dr["NumeroCDFe"].ToString();
                }
            }


            if (!chkCNPJ.Checked && !chkIBAMA.Checked && !chkCDFe.Checked && !chkPlacas.Checked && !chkPeso.Checked)
            {
                if (_vlSbTl > 0 || _t1 > 0 || l_qt > 0)
                {
                    decimal _tt2 = 0;                    
                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "'")) 
                    {
                        if (_drtm["QtdeCDFe"].ToString() != "" && _drtm["QtdeCDFe"].ToString() != "0" &&
                            _drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                        {
                            _tt2 = _tt2 + Convert.ToDecimal(_drtm["QtdeCDFe"].ToString());
                            break;
                        }
                    }
                    l_qtSbTlPorMTRe = 0;
                    int _nlnsMTRe = 0;
                    foreach (DataRow _drtm in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "'"))
                    {
                        if (_drtm["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                        {
                            if (_drtm["QtDescarga"].ToString() != "")
                            {
                                decimal l_qtT2 = Convert.ToDecimal(_drtm["QtDescarga"]);
                                clsCacambas oCaixa = new clsCacambas();
                                clsCacambaDados oCaixaDados = new clsCacambaDados();
                                oCaixaDados.PegaDados(oCaixa, _drtm["NumeroCaixa"].ToString());
                                if (_drtm["Unidade"].ToString().ToUpper() == "CX")
                                    l_qtT2 = Convert.ToDecimal(_drtm["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtm["QtDescarga"]);
                                else if (_drtm["Unidade"].ToString().ToUpper() == "M3" || _drtm["Unidade"].ToString().ToUpper() == "M³")
                                    l_qtT2 = Convert.ToDecimal(_drtm["M3PorTon"]) * Convert.ToDecimal(_drtm["QtDescarga"]) * 1000;
                                l_qtSbTlPorMTRe = l_qtSbTlPorMTRe + l_qtT2;
                            }
                            _nlnsMTRe++;
                        }
                    }
                    if (_nlnsMTRe > 1)
                        AddTotalMTR_e(_table, _row, _cell, iColWidth, _MTReAnterior, l_qtSbTlPorMTRe, _tt2);
                }
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
                        bool pAlinCentral = false, bool pBackColorYellou = false, bool pBackColorRed = false, bool pForeColor = false,
                        bool pOverline = false)
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
            if (pWidth == 60)
                _lbl.BackColor = System.Drawing.Color.Yellow;
            else
            {
                _lbl.BackColor = System.Drawing.Color.White;
                _lbl.Font.Bold = true;
            }
            if (pWidth == 80)
                _lbl.ForeColor = System.Drawing.Color.DarkGreen;
            else if (pWidth != 60)
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
            _lbl.BackColor = System.Drawing.Color.White;

        if (pNegrito)
        {
            _lbl.Font.Bold = true;
        }
        if (pAlinDireita)
            _lbl.Attributes.CssStyle.Add("text-align", "right");

        if (pAlinCentral)
            _lbl.Attributes.CssStyle.Add("text-align", "center");

        if (pOverline)
        {
            _lbl.Font.Overline = true;
        }

        cell.Controls.Add(_lbl);
        row.Cells.Add(cell);
        _table.BorderWidth = 0;
        _table.Rows.Add(row);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        lblLinhas.Text = "";
        lblMensagem.Text = "";
        Relatorio();
        Session["Panel1"] = Panel1;
        lblLinhas.Text = "linhas: " + _linhas.ToString();
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

    protected void btnEnviarEmail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensagem.Text = "";
            //chkMostrarCDFe.Checked = true; // comentado em 01/04/2024.
            //chkCDFe.Checked = false;
            chkCNPJ.Checked = false;
            chkIBAMA.Checked = false;
            //chkPeso.Checked = false;
            chkPlacas.Checked = false;
            chkSoEmDTR.Checked = false;
        }
        finally
        {
            Session["Panel1"] = Panel1;
            lblMensagem.Text = "";
            btnConfirmar.Visible = true;
        }
    }

    private string Cabecalho(int _pagina)
    {
        string sx = "\n\n";
        sx = sx + "  Relatório de movimentação de resíduos por destino final - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
        sx = sx + "  Emissão: " + DateTime.Now.ToString("dd/MM/yy") + "  -  Pág: " + _pagina.ToString()  +"  \n\n";
        oDestinoFinalDados = new clsDestinoFinalDados();
        oDestinoFinal = new clsDestinoFinal();
        oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
        sx = sx + "  Destinador: " + oDestinoFinal.Nome + "(" + oDestinoFinal.Codigo + ") " + oDestinoFinal.NomeFantasia + "\n\n";
        sx = sx + "   Data   Código Cliente              CNPJ               Quantidade Resíduo                   NºTicket Motorista         Placas   Código   Descrição IBAMA                  Nº MTR-e   Peso Total ";
        if (chkMostrarCDFe.Checked)
            sx = sx + " Nº CDFe  Qtde MTRe Situação \n";
        else
            sx = sx + "\n";
        sx = sx + "                                                         Individual                                                                                                                            Kg \n";
        sx = sx + " ──────── ────── ──────────────────── ────────────────── ────────── ───────────────────────── ──────── ───────────────── ──────── ──────── ──────────────────────────────── ────────── ──────────";
        if (chkMostrarCDFe.Checked)
            sx = sx + " ───────── ───────── ──────────── \n";
        else
            sx = sx + "\n";
        return sx;
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

        // quando for o desenvolvedor - mandar para mim.
        if (oUsuario.Nome.ToLower() == "teixeira")
        {
            mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com"));
        }
        else if(oUsuario.Nome.ToLower() != "" && oUsuario.Nome.ToLower() != "teixeira" && oUsuario.Nome != "&nbsp;")
        {
            foreach (string _email in peMail.Replace(",", ";").Split(";"[0]))
            {
                if (_email.Trim() != "")
                    mail.To.Add(new System.Net.Mail.MailAddress(_email, pNomeMostrar_email));
            }
        }
        mail.To.Add(new System.Net.Mail.MailAddress("andre.toro@brooksambiental.com.br"));
        if (oUsuario.Nome.ToLower() != "teixeira")
            mail.To.Add(new System.Net.Mail.MailAddress("logistica1@brooksambiental.com.br"));

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
            AddRow(_table, _row, _cell, "Erro ao enviar e-mail." + erro.Message, 800, false);
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
                    Session["Panel1"] = null;
                }
                finally 
                {
                    System.Threading.Thread.Sleep(1000);
                    Criar_Enviar_Relatorio();
                }                
            }
        }
    }
    private void RemoveColunaDtExcel(string pColuna)
    {
        for (int i = 1; i <= _dt.Columns.Count - 1; i++)
        {
            if (_dt.Columns[i].Caption == pColuna)
                _dt.Columns.RemoveAt(i);
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
        if (!chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor));
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo);
        }
        else if (chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, Convert.ToInt32(CLIENTE1.Valor));
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999);
        }

        RemoveColunaDtExcel("DataRetirada");
        RemoveColunaDtExcel("ClienteEmissaoMTReRCD");
        RemoveColunaDtExcel("PlacasNaCDFe");
        RemoveColunaDtExcel("NumeroLancamento");
        RemoveColunaDtExcel("NumeroCaixa");
        RemoveColunaDtExcel("M3PorTon");
        RemoveColunaDtExcel("CodigoAterro");
        RemoveColunaDtExcel("CodigoResiduo");
        RemoveColunaDtExcel("PesoTotalCliente");

        if (chkCDFe.Checked && !chkPeso.Checked)
        {
            // pra não mostrar os com CDF-e emitida
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["NumeroCDFe"].ToString() != "")
                    if (_dr["NumeroCDFe"].ToString() != "0")
                        _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
            }
        }
        else if (!chkCDFe.Checked && chkPeso.Checked)
        {
            // pra não mostrar Pesos iguais
            decimal tMtrIbama = 0;
            decimal _qtCDFeConvertido = 0;
            // Lembrete: esse algoritmo só funciona se, a ordem for `número MTRe + Código IBAMA`
            foreach (DataRow _dr in _dt.Rows)
            {
                // 1º - 2306115286
                // 2º - 2305144184 - não pode aparecer, pois pesos iguais
                // 2306108518
                // pegar numero MTR-e e Codigo do Ibama 
                // gerar um total por esse filtro
                // verificar se existe mais de um, pois dai tem que acumular qtdescarga, para comparar com QtdeCDFe, caso quantidades total igual não mostrar 

                if (_dr["MTRe"].ToString() == "2306108518")
                {
                    string _tst = _dr["MTRe"].ToString();
                    _tst = _tst;
                }

                string _f1 = "MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'";
                _qtCDFeConvertido = 0;
                if (_dr["QtdeCDFe"].ToString() != "")
                {
                    _qtCDFeConvertido = Convert.ToDecimal(_dr["QtdeCDFe"].ToString());
                }
                int _qtMtrs = _dt.Select(_f1, "").Length;
                if (_qtMtrs > 1)
                {
                    tMtrIbama = 0;
                    foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                    {
                        if (_dr2["qtDescarga"].ToString() != "")
                        {
                            tMtrIbama = tMtrIbama + Convert.ToDecimal(_dr2["qtDescarga"].ToString());
                        }
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                        {
                            _dr2["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                        }
                    }
                }
                else if (_qtMtrs == 1)
                {
                    tMtrIbama = 0;
                    if (_dr["qtDescarga"].ToString() != "")
                    {
                        tMtrIbama = Convert.ToDecimal(_dr["qtDescarga"].ToString());
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                    }
                }
            }
        }
        else if (chkCDFe.Checked && chkPeso.Checked)
        {
            // pra não mostrar Pesos iguais e nem cdfes emitidas com pesos iguais
            decimal tMtrIbama = 0;
            decimal _qtCDFeConvertido = 0;
            // Lembrete: esse algoritmo só funciona se, a ordem for `número MTRe + Código IBAMA`
            foreach (DataRow _dr in _dt.Rows)
            {
                string _f1 = "MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'";
                _qtCDFeConvertido = 0;
                if (_dr["QtdeCDFe"].ToString() != "")
                {
                    _qtCDFeConvertido = Convert.ToDecimal(_dr["QtdeCDFe"].ToString());
                }
                int _qtMtrs = _dt.Select(_f1, "").Length;
                if (_qtMtrs > 1)
                {
                    tMtrIbama = 0;
                    foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                    {
                        if (_dr2["qtDescarga"].ToString() != "")
                        {
                            tMtrIbama = tMtrIbama + Convert.ToDecimal(_dr2["qtDescarga"].ToString());
                        }
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                        {
                            if (_dr2["NumeroCDFe"].ToString() != "")
                                if (_dr2["NumeroCDFe"].ToString() != "0")
                                    _dr2["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                        }
                    }
                }
                else if (_qtMtrs == 1)
                {
                    tMtrIbama = 0;
                    if (_dr["qtDescarga"].ToString() != "")
                    {
                        tMtrIbama = Convert.ToDecimal(_dr["qtDescarga"].ToString());
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        if (_dr["NumeroCDFe"].ToString() != "")
                            if (_dr["NumeroCDFe"].ToString() != "0")
                                _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                    }
                }
            }
        }

        foreach (DataRow dr in _dt.Rows)
        {
            if (dr["DataDescarga"].ToString() == "01/01/0100 00:00:00") // não mostra cdf-e emitida e ou pesos iguais
            {
                for (int i = 0; i < _dt.Columns.Count - 1; i++)
                {
                    if (_dt.Columns[i].DataType.Name == "String")
                        dr[i] = "";
                    if (_dt.Columns[i].DataType.Name == "Decimal")
                        dr[i] = "0,00";
                    if (_dt.Columns[i].DataType.Name == "Int32")
                        dr[i] = "0";
                }
            }
        }


        DataTable _dtNova = new DataTable();
        foreach (DataColumn dcX in _dt.Columns)
        {
            _dtNova.Columns.Add(dcX.ColumnName, dcX.DataType);
        }
        int ix = 0;
        foreach (DataRow drY in _dt.Rows)
        {
            if (drY[2].ToString() != "")
            {
                DataRow drX = _dtNova.NewRow();
                for (int y = 0; y <= _dtNova.Columns.Count - 1; y++)
                    drX[y] = drY[y];
                _dtNova.Rows.Add(drX);
            }
        }
        Grade.DataSource = _dtNova;
        Grade.DataBind();

        if (_dtNova.Rows.Count > 0)
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
            oDestinoFinal = new clsDestinoFinal();
            oDestinoFinalDados.PegaDados(oDestinoFinal, cdDestino);
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

            //Data Código Cliente CNPJ Quantde.Individual Resíduo Nº Ticket Motorista Placas Código Descrição IBAMA Nº MTR - e Peso Total Kg
            Grade.HeaderRow.Cells[0].Text = "Data descarga";
            Grade.HeaderRow.Cells[1].Text = "Codigo";
            Grade.HeaderRow.Cells[2].Text = "Cliente";
            Grade.HeaderRow.Cells[3].Text = "CNPJ Cliente";
            Grade.HeaderRow.Cells[4].Text = "Qtde.individual";
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

    private void Criar_Enviar_Relatorio()
    {
        int _pagina = 1;
        var document = new PdfSharp.Pdf.PdfDocument();
        var page = document.AddPage();
        page.Orientation = PdfSharp.PageOrientation.Landscape;
        var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
        var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
        var font = new PdfSharp.Drawing.XFont("Courier New", 6.2);

        // para inserir pdf link
        //var xrect = new PdfSharp.Drawing.XRect(240, 395, 300, 20);
        //PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
        //var rect =  gfx.Transformer.WorldToDefaultPage(xrect);
        //var pdfrect = new PdfRectangle(rect);
        

        int _ln = 90;
        _sb = new System.Text.StringBuilder();

        int nCampos = 13;
        if (chkMostrarCDFe.Checked)
            nCampos = 16;

        textFormatter.DrawString(Cabecalho(_pagina), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 0, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));

        _sb.Clear();

        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        if (!chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, Convert.ToInt32(CLIENTE1.Valor), false, false, true);
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, oDestinoFinal.Codigo, 0, false, false, true);
        }
        else if (chkSoEmDTR.Checked)
        {
            if (CLIENTE1.Valor != "")
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, Convert.ToInt32(CLIENTE1.Valor), false, false, true);
            else
                _dt = oLancamentoMTRDados.PreencheDTparaRelatorioMovimentacao(oLancamentoMTR, Data1.Data, Data2.Data, 9999, 0, false, false, true);
        }
        bool EhLinhaComTotalMTRe = false;
        _vlSbTl = 0;
        _valorTotalGeral = 0;
        string _nomeAnterior = "";
        string _MTReAnterior = "";
        if (_dt.Rows.Count > 0)
        {
            if (_dt.Rows.Count > 1)
            {
                if (_nomeAnterior == _dt.Rows[1]["Nome"].ToString())
                    _nomeAnterior = _dt.Rows[0]["Nome"].ToString();
                _MTReAnterior = _dt.Rows[0]["MTRe"].ToString() + _dt.Rows[0]["CodigoIBAMA"].ToString();
            }
        }
        if (chkCDFe.Checked)
        {
            // pra não mostrar os com CDF-e emitida
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["NumeroCDFe"].ToString() != "")
                    if (_dr["NumeroCDFe"].ToString() != "0")
                        _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
            }
        }
        if (chkPeso.Checked)
        {
            // pra não mostrar Pesos iguais
            decimal tMtrIbama = 0;
            decimal _qtCDFeConvertido = 0;
            // Lembrete: esse algoritmo só funciona se, a ordem for `número MTRe + Código IBAMA`
            foreach (DataRow _dr in _dt.Rows)
            {
                // 1º - 2311112450
                // 2º - 2311112450 - não pode aparecer, pois pesos iguais
                // 2311112450
                // pegar numero MTR-e e Codigo do Ibama 
                // gerar um total por esse filtro
                // verificar se existe mais de um, pois dai tem que acumular qtdescarga, para comparar com QtdeCDFe, caso quantidades total igual não mostrar 

                if (_dr["MTRe"].ToString() == "2311112450")
                {
                    string _tst = _dr["MTRe"].ToString();
                    _tst = _tst;
                }               

                string _f1 = "MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'";
                _qtCDFeConvertido = 0;
                if (_dr["QtdeCDFe"].ToString() != "")
                {
                    _qtCDFeConvertido = Convert.ToDecimal(_dr["QtdeCDFe"].ToString());
                }
                int _qtMtrs = _dt.Select(_f1, "").Length;
                if (_qtMtrs > 1)
                {
                    tMtrIbama = 0;
                    foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                    {
                        if (_dr2["qtDescarga"].ToString() != "")
                        {
                            tMtrIbama = tMtrIbama + Convert.ToDecimal(_dr2["qtDescarga"].ToString());
                        }
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        foreach (DataRow _dr2 in _dt.Select(_f1, ""))
                        {
                            _dr2["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                        }
                    }
                }
                else if (_qtMtrs == 1)
                {
                    tMtrIbama = 0;
                    if (_dr["qtDescarga"].ToString() != "")
                    {
                        tMtrIbama = Convert.ToDecimal(_dr["qtDescarga"].ToString());
                    }
                    if (Math.Round(tMtrIbama, 1) == Math.Round(_qtCDFeConvertido, 1))
                    {
                        _dr["DataDescarga"] = Convert.ToDateTime("01/01/0100 00:00:00");
                    }
                }
            }
        }
        decimal _t2 = 0;
        decimal _tt1 = 0;
        decimal _ti = 0;
        decimal _ti2 = 0;
        foreach (DataRow _dr in _dt.Rows)
        {
            if (_dr["DataDescarga"].ToString() != "01/01/0100 00:00:00") // não mostra cdf-e emitida e ou pesos iguais
            {
                try
                {
                    int icl = 0;
                    nCampos = _dt.Columns.Count;

                    //  Data    Código Cliente              CNPJ               Quantde     Resíduo                   Nº Ticket  Motorista         Placas   Código
                    //  Descrição IBAMA                  Nº MTR-e   Peso Total 
                    //  Nº CDFe   Qtde MTRe Situação 
                    if (_dr["MTRe"].ToString() == "2312002649") //2311112450
                    {
                        nCampos = nCampos;
                    }
                    int _regsMTReIguais = 0;
                    int _regsMTReAnteriorIguais = 0;
                    _regsMTReIguais = _dt.Select("MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'", "").Length;
                    if (_MTReAnterior != (_dr["MTRe"].ToString() + _dr["CodigoIBAMA"].ToString()))
                    {
                        _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                        if (_regsMTReIguais > 1 && _regsMTReAnteriorIguais <= 1)
                        {
                            // linha em branco
                            textFormatter.DrawString(" ".PadLeft(226), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            _ln = _ln + 10;
                        }
                    }

                    EhLinhaComTotalMTRe = false;
                    if (_dt.Select("MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'", "").Length > 1)
                    {
                        EhLinhaComTotalMTRe = true;
                    }

                    _sb.Clear();
                    _sb.Append(" " + Convert.ToDateTime(_dr["DataDescarga"]).ToString("dd/MM/yy").Replace(" 00:00:00", ""));
                    _sb.Append(" ".PadLeft(7 - _dr["CodigoCliente"].ToString().Length) + _dr["CodigoCliente"].ToString());
                    _sb.Append(" " + geral.Left(_dr["Nome"].ToString(), 20) + " ".PadLeft(21 - geral.Left(_dr["Nome"].ToString(), 20).Length));
                    _sb.Append(geral.Left(_dr["CNPJ_CPF"].ToString(), 18));
                    decimal l_qt = 0;
                    if (_dr["QtDescarga"].ToString() != "")
                    {
                        l_qt = Convert.ToDecimal(_dr["QtDescarga"]);
                        clsCacambas oCaixa = new clsCacambas();
                        clsCacambaDados oCaixaDados = new clsCacambaDados();
                        oCaixaDados.PegaDados(oCaixa, _dr["NumeroCaixa"].ToString());
                        if (_dr["Unidade"].ToString().ToUpper() == "CX")
                            l_qt = Convert.ToDecimal(_dr["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_dr["QtDescarga"]);
                        else if (_dr["Unidade"].ToString().ToUpper() == "M3" || _dr["Unidade"].ToString().ToUpper() == "M³")
                            l_qt = Convert.ToDecimal(_dr["M3PorTon"]) * Convert.ToDecimal(_dr["QtDescarga"]) * 1000;
                        _valorTotalGeral = _valorTotalGeral + l_qt;
                    }
                    _sb.Append(" ".PadLeft(11 - l_qt.ToString("N2").Length) + l_qt.ToString("N2"));
                    _sb.Append(" " + geral.Left(_dr["DescricaoReduzida"].ToString(), 25) + " ".PadLeft(26 - geral.Left(_dr["DescricaoReduzida"].ToString(), 26).Length));
                    _sb.Append(" ".PadLeft(8 - geral.Left(_dr["Ticket"].ToString(), 7).Length) + geral.Left(_dr["Ticket"].ToString(), 7));
                    _sb.Append(" " + geral.Left(_dr["NomeMotorista"].ToString(), 17) + " ".PadLeft(18 - geral.Left(_dr["NomeMotorista"].ToString(), 17).Length));
                    _sb.Append(_dr["Placas"].ToString() + " ".PadLeft(9 - _dr["Placas"].ToString().Length));
                    _sb.Append(geral.Left(_dr["CodigoIBAMA"].ToString(), 10));
                    _sb.Append(" " + geral.Left(_dr["Descricao"].ToString(), 31) + " ".PadLeft(32 - geral.Left(_dr["Descricao"].ToString(), 31).Length));

                    //string _lnk0 = "";
                    //_lnk0 = _lnk0 + "<link onclick=_linkMTRe(";
                    //_lnk0 = _lnk0 + _dr["MTRe"].ToString() + ")";
                    //_lnk0 = _lnk0 + ">" + _dr["MTRe"].ToString() + "</link>";
                    //_sb.Append(" " + _lnk0);
                    _sb.Append(" " + _dr["MTRe"].ToString());

                    _sb.Append(" ".PadLeft(11 - l_qt.ToString("N2").Length) + l_qt.ToString("N2"));

                    if (_dr["NumeroCDFe"].ToString() != "" && _dr["NumeroCDFe"].ToString() != "0")
                        _sb.Append(" ".PadLeft(10 - _dr["NumeroCDFe"].ToString().Length) + _dr["NumeroCDFe"].ToString());
                    else
                        _sb.Append(" ".PadLeft(10 - "-emitir-".Length) + "-emitir-");

                    _t2 = 0;
                    _tt1 = 0;
                    l_qt = 0;
                    _ti = 0;
                    _ti2 = 0;
                    DataRow[] drr = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "'", "");
                    string _CodigoIbama = geral.Right(_MTReAnterior, 8);
                    if (!EhLinhaComTotalMTRe && _dt.Select("MTRe = '" + _dr["MTRe"].ToString() + "' and CodigoIBAMA = '" + _dr["CodigoIBAMA"].ToString() + "'", "").Length == 1)
                    {
                        drr = _dt.Select("MTRe = '" + _dr["MTRe"].ToString() + "'", "");
                        _CodigoIbama = _dr["CodigoIBAMA"].ToString();
                    }
                    foreach (DataRow _drtmX in drr)
                    {
                        if (_drtmX["QtDescarga"].ToString() != "" && _drtmX["QtDescarga"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == _CodigoIbama)
                        {
                            l_qt = Convert.ToDecimal(_drtmX["QtDescarga"]);
                            clsCacambas oCaixa = new clsCacambas();
                            clsCacambaDados oCaixaDados = new clsCacambaDados();
                            oCaixaDados.PegaDados(oCaixa, _drtmX["NumeroCaixa"].ToString());
                            if (_drtmX["Unidade"].ToString().ToUpper() == "CX")
                                l_qt = Convert.ToDecimal(_drtmX["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtmX["QtDescarga"]);
                            else if (_drtmX["Unidade"].ToString().ToUpper() == "M3" || _drtmX["Unidade"].ToString().ToUpper() == "M³")
                                l_qt = Convert.ToDecimal(_drtmX["M3PorTon"]) * Convert.ToDecimal(_drtmX["QtDescarga"]) * 1000;

                            _tt1 = _tt1 + l_qt;
                            _ti = _ti + l_qt;
                        }
                    }
                    foreach (DataRow _drtmX in drr)
                    {
                        if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == _CodigoIbama)
                        {
                            _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                            _ti2 = _ti2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                            break;
                        }
                    }

                    if (EhLinhaComTotalMTRe)
                    {
                        _sb.Append(" ".PadLeft(10));
                    }
                    else
                    {
                        _sb.Append(" ".PadLeft(10 - _t2.ToString("N2").Length) + _t2.ToString("N2"));
                    }
                    if (_dr["Situacao"].ToString().IndexOf("Armazenamento") > -1)
                    {
                        _dr["Situacao"] = "Armaz.Temp.";
                    }
                    _sb.Append(" " + _dr["Situacao"].ToString());

                    _dr["PesoTotalCliente"] = l_qt;

                    // Total MTR-e
                    if (_MTReAnterior != (_dr["MTRe"].ToString() + _dr["CodigoIBAMA"].ToString()))
                    {
                        _regsMTReAnteriorIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                        _regsMTReIguais = _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", "").Length;
                        if (_regsMTReIguais > 1) // && _regsMTReAnteriorIguais <= 1)
                        {
                            _tt1 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                            {
                                if (_drtmX["QtDescarga"].ToString() != "" && _drtmX["QtDescarga"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                {
                                    l_qt = Convert.ToDecimal(_drtmX["QtDescarga"]);
                                    clsCacambas oCaixa = new clsCacambas();
                                    clsCacambaDados oCaixaDados = new clsCacambaDados();
                                    oCaixaDados.PegaDados(oCaixa, _drtmX["NumeroCaixa"].ToString());
                                    if (_drtmX["Unidade"].ToString().ToUpper() == "CX")
                                        l_qt = Convert.ToDecimal(_drtmX["M3PorTon"]) * oCaixa.Capacidade * 1000 * Convert.ToDecimal(_drtmX["QtDescarga"]);
                                    else if (_drtmX["Unidade"].ToString().ToUpper() == "M3" || _drtmX["Unidade"].ToString().ToUpper() == "M³")
                                        l_qt = Convert.ToDecimal(_drtmX["M3PorTon"]) * Convert.ToDecimal(_drtmX["QtDescarga"]) * 1000;
                                    _tt1 = _tt1 + l_qt;
                                }
                            }

                            _t2 = 0;
                            foreach (DataRow _drtmX in _dt.Select("MTRe = '" + geral.Left(_MTReAnterior, 10) + "' and CodigoIBAMA = '" + geral.Right(_MTReAnterior, 8) + "'", ""))
                            {
                                if (_drtmX["QtdeCDFe"].ToString() != "" && _drtmX["QtdeCDFe"].ToString() != "0" && _drtmX["CodigoIBAMA"].ToString() == geral.Right(_MTReAnterior, 8))
                                {
                                    _t2 = _t2 + Convert.ToDecimal(_drtmX["QtdeCDFe"].ToString());
                                    break;
                                }
                            }
                            if (Math.Round(_tt1, 1) != Math.Round(_t2, 1))
                            {
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                                textFormatter.DrawString(" ".PadLeft(171) + "Total MTR-e", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                textFormatter.DrawString(" ".PadLeft(182) + " ".PadLeft(11 - _tt1.ToString("N2").Length) + _tt1.ToString("N2"), font, PdfSharp.Drawing.XBrushes.DarkGreen, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                if (chkMostrarCDFe.Checked)
                                    textFormatter.DrawString(" ".PadLeft(193) + " ".PadLeft(20 - _t2.ToString("N2").Length) + _t2.ToString("N2"), font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                else 
                                    textFormatter.DrawString(" ".PadLeft(193) + " ".PadLeft(20), font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                            else
                            {
                                if (chkMostrarCDFe.Checked)
                                    textFormatter.DrawString(" ".PadLeft(171) + "Total MTR-e" + " ".PadLeft(11 - _tt1.ToString("N2").Length) + _tt1.ToString("N2") + " ".PadLeft(20 - _t2.ToString("N2").Length) + _t2.ToString("N2"), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                else
                                    textFormatter.DrawString(" ".PadLeft(171) + "Total MTR-e" + " ".PadLeft(11 - _tt1.ToString("N2").Length) + _tt1.ToString("N2") + " ".PadLeft(20), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                            font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                            _ln = _ln + 10;
                            // linha em branco
                            textFormatter.DrawString(" ".PadLeft(226), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            _ln = _ln + 10;
                        }
                    }
                    string _texto = geral.Left(_sb.ToString(), 226);
                    _texto = _texto + " ".PadLeft(227 - _texto.Length);
                    if (_texto.IndexOf("-emitir-") > -1)
                    {
                        if (Math.Round(_ti, 1) == Math.Round(_ti2, 1))
                        {
                            textFormatter.DrawString(geral.Left(_texto, _texto.IndexOf("-emitir-")), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));                            
                            if (chkMostrarCDFe.Checked)
                            {
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") - 1) + "-emitir-", font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") + 8) + _texto.Substring(_texto.IndexOf("-emitir-") + 8), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            } 
                        }
                        else
                        {
                            string _texto1 = geral.Left(_texto, _texto.IndexOf("-emitir-") - 12);     // vai até o número MTRe
                            string _texto2 = _texto.Substring(_texto.IndexOf("-emitir-") - 13, 11);   // peso total Kg
                            string _texto3 = _texto.Substring(_texto.IndexOf("-emitir-"), 10);        // número CDFe ou -emitir-
                            string _texto4 = _texto.Substring(_texto.IndexOf("-emitir-") + 8, 10);    // Qtde CDFe
                            string _texto5 = _texto.Substring(_texto.IndexOf("-emitir-") + 19, 12);   // Situação

                            textFormatter.DrawString(_texto1, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                            textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") - 13) + _texto2, font, PdfSharp.Drawing.XBrushes.DarkGreen, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            if (chkMostrarCDFe.Checked)
                            {
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") - 1) + _texto3, font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") + 8) + _texto4, font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") + 19) + _texto5, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                            else
                            {
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") - 1) + " ", font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") + 8) + " ", font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(_texto.IndexOf("-emitir-") + 19) + " ", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                        }
                    }
                    else
                    {

                        if (Math.Round(_ti, 1) == Math.Round(_ti2, 1))
                        {
                            if (chkMostrarCDFe.Checked)
                            {
                                textFormatter.DrawString(geral.Left(_texto, 195), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                textFormatter.DrawString(" ".PadLeft(195 - 1) + _sb.ToString().Substring(196, 8), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(195 + 8) + _texto.Substring(195 + 8), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                            else
                            {
                                textFormatter.DrawString(geral.Left(_texto, 194) + " ".PadLeft(13), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                        }
                        else
                        {
                            string _texto1 = geral.Left(_texto, 195 - 12);     // vai até o número MTRe
                            string _texto2 = _texto.Substring(195 - 13, 11);   // peso total Kg
                            string _texto3 = _texto.Substring(195, 10);        // número CDFe ou -emitir-
                            string _texto4 = _texto.Substring(195 + 8, 10);    // Qtde CDFe
                            string _texto5 = _texto.Substring(195 + 19, 12);   // Situação

                            textFormatter.DrawString(_texto1, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                            textFormatter.DrawString(" ".PadLeft(182) + _texto2, font, PdfSharp.Drawing.XBrushes.DarkGreen, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            if (chkMostrarCDFe.Checked)
                            {
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(193) + _texto3, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                                textFormatter.DrawString(" ".PadLeft(195 + 8) + _texto4, font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(195 + 19) + _texto5, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                            }
                            else
                            {
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(193) + " ", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                                textFormatter.DrawString(" ".PadLeft(195 + 8) + " ", font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                                textFormatter.DrawString(" ".PadLeft(195 + 19) + " ", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                                font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                            }
                        }
                        //textFormatter.DrawString(_texto, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));

                    }
                    _ln = _ln + 10;

                    icl = 0;
                    if (_ln > 540)
                    {
                        page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;
                        graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                        textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                        font = new PdfSharp.Drawing.XFont("Courier New", 6.2);
                        _pagina++;
                        textFormatter.DrawString(Cabecalho(_pagina), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 0, 1494, page.Height + 60));
                        _sb.Clear();
                        _ln = 90;
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = lblMensagem.Text + ex.Message + " - " + DESTINOFINAL1.Valor + Environment.NewLine;
                }

                if (!_dr.IsNull("MTRe"))
                    _MTReAnterior = _dr["MTRe"].ToString() + _dr["CodigoIBAMA"].ToString();
            }
        }

        if (_dt.Rows.Count > 0)
        {
            if (EhLinhaComTotalMTRe)
            {                
                if (_tt1 != _t2)
                {
                    font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
                    textFormatter.DrawString(" ".PadLeft(171) + "Total MTR-e", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                    textFormatter.DrawString(" ".PadLeft(182) + " ".PadLeft(11 - _tt1.ToString("N2").Length) + _tt1.ToString("N2"), font, PdfSharp.Drawing.XBrushes.DarkGreen, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                    textFormatter.DrawString(" ".PadLeft(193) + " ".PadLeft(20 - _t2.ToString("N2").Length) + _t2.ToString("N2"), font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                }
                else
                {
                    textFormatter.DrawString(" ".PadLeft(171) + "Total MTR-e" + " ".PadLeft(11 - _tt1.ToString("N2").Length) + _tt1.ToString("N2") + " ".PadLeft(20 - _t2.ToString("N2").Length) + _t2.ToString("N2"), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
                    _ln = _ln + 16;
                }
            }
            font = new PdfSharp.Drawing.XFont("Courier New", 6.2, PdfSharp.Drawing.XFontStyle.Bold);
            textFormatter.DrawString(" ".PadLeft(163) + "Valor total geral:" + " ".PadLeft(12 - _valorTotalGeral.ToString("N2").Length) + _valorTotalGeral.ToString("N2"), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, _ln - 26, Convert.ToDouble(Panel1.Width.ToString().Replace("px", "")), page.Height + 60));
            string _sdf = "";
            if (DESTINOFINAL1.Valor != "")
                _sdf = Convert.ToInt32(DESTINOFINAL1.Valor).ToString("0000");
            string _sdtInicioFim = Convert.ToDateTime(Data1.Data).ToString("yyMMdd") + "_" + Convert.ToDateTime(Data2.Data).ToString("yyMMdd");
            if (System.IO.Directory.Exists(Server.MapPath("/Temp/")))
            {
                try
                {
                    document.Save(Server.MapPath("/Temp/") + "RELATERRO_" + _sdf + "_" + _sdtInicioFim + ".pdf");
                    document.Close();
                }
                finally
                {
                    lblMensagem.Text = "Arquivo salvo com sucesso em: " + Server.MapPath("/Temp/") + "RELATERRO_" + _sdf + "_" + _sdtInicioFim + ".pdf";
                    // enviar e-mail apenas para o destino final selecionado
                    string _stx = geral.Texto_email_DestinoFinal();
                    if (DESTINOFINAL1.Valor != "" && oDestinoFinal.eMail != "")
                    {
                        EnviaeMail(oDestinoFinal.eMail, Server.MapPath("/Temp/") + "RELATERRO_" + _sdf + "_" + _sdtInicioFim + ".pdf", oDestinoFinal.NomeFantasia);
                    }
                }
            }
            else
                lblMensagem.Text = "Não possível gerar Arquivo PDF. Unidade ou mapeamento do servidor inexistente! " + Server.MapPath("/Temp/");
        }
    }
}