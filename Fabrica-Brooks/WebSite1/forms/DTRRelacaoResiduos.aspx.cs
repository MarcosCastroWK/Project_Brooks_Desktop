using System;
using System.Text;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class DTRRelacaoResiduos : System.Web.UI.Page
{
    clsUsuarios oUsuario = new clsUsuarios();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
    clsDestinoFinal oDestinoFinal = new clsDestinoFinal();

    DataTable _dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        if (oUsuario == null)
        {
            Response.Redirect("brooks/login.aspx", true);
        }
        if (!IsPostBack)
        {
            Relatorio();
        }
    }

    private void Relatorio()
    {
        if (Session["dtRelacaoDTR"] != null)
            _dt = (DataTable)Session["dtRelacaoDTR"];

        Panel1.BorderWidth = 1;

        DateTime _dataEnvio = DateTime.Now;
        string _localentrega = "";
        int _codigoresiduo = 0;
        if (_dt.Rows.Count > 0)
        {
            _dataEnvio = Convert.ToDateTime(_dt.Rows[0][8]);
            _localentrega = _dt.Rows[0][9].ToString();
            _codigoresiduo = Convert.ToInt32(_dt.Rows[0][10]);
        }

        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();

        lblDestinoFinal.Text = oDestinoFinalDados.PegaRazaoSocial(_localentrega);
        AddRow(_table, _row, _cell, "ENVIADO EM: ", 110, false, true);
        AddRow(_table, _row, _cell, _dataEnvio.ToString("dd/MM/yy"), 680, false, true);

        clsResiduos oResiduo = new clsResiduos();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
        oResiduo = oResiduoDados.PegaDados(oResiduo, _codigoresiduo);
        AddRow(_table, _row, _cell, "CÓDIGO RESÍDUO: ", 110, false, true);
        AddRow(_table, _row, _cell, oResiduo.CodigoResiduoManifesto, 680, false, true);

        AddRow(_table, _row, _cell, "TIPO RESÍDUOS: ", 110, false, true);
        AddRow(_table, _row, _cell, oResiduo.DescricaoGrupo + "/" + oResiduo.DescricaoReduzida, 680, false, true);

        Panel1.Controls.Add(_table);

        if (_dt.Rows.Count > 0)
        {
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();

            int[] iColWidth = new int[8];
            iColWidth[1] = 80;  //DATA COLETA
            iColWidth[2] = 50;  //CÓDIGO CLIENTE
            iColWidth[3] = 104; //CNPJ/CPF
            iColWidth[4] = 360; //GERADOR DO RESÍDUO
            iColWidth[5] = 70;  //PESO KG
            iColWidth[6] = 80;  //PERCENTUAL
            iColWidth[7] = 80;  //Nº MTR-e

            int tWidthContratos = 0;
            foreach (int iTW in iColWidth)
                tWidthContratos = tWidthContratos + iTW;
            _table.Width = tWidthContratos + 10;
            AddRow(_table, _row, _cell, "DATA COLETA", iColWidth[1], false, true, true);
            AddRow(_table, _row, _cell, "CÓDIGO CLIENTE", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "CNPJ/CPF", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "GERADOR DO RESÍDUO", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "PESO KG", iColWidth[5], false, true);
            AddRow(_table, _row, _cell, "PERCENTUAL", iColWidth[6], false, true);
            AddRow(_table, _row, _cell, "Nº MTR-e", iColWidth[7], false, true);

            AddRow(_table, _row, _cell, "──────────", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "──────", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "─────────────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "───────────────────────────────────────────────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[5], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[6], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, false);

            decimal _PercentualPeso = 0;
            decimal _PesoKgTotal = 0;
            int _itens = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                _itens++;
                _PesoKgTotal = _PesoKgTotal + Convert.ToDecimal(dr["PesoKg"]);
            }
            foreach (DataRow dr in _dt.Rows)
            {
                AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataColeta"]).ToString("dd/MM/yy"), iColWidth[1], false, false, true);
                AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[2], true);
                AddRow(_table, _row, _cell, geral.Left( dr["CNPJ_CPF"].ToString(), 18), iColWidth[3], false);
                AddRow(_table, _row, _cell, dr["Gerador"].ToString(), iColWidth[4], false);
                _PercentualPeso = Convert.ToDecimal(dr["PesoKg"]) * 100 / _PesoKgTotal;
                AddRow(_table, _row, _cell, dr["PesoKg"].ToString(), iColWidth[5], true);
                AddRow(_table, _row, _cell, _PercentualPeso.ToString("N2") + "%", iColWidth[6], true);
                AddRow(_table, _row, _cell, dr["NumeroMTRe"].ToString(), iColWidth[7], true);
            }
            AddRow(_table, _row, _cell, "──────────", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "──────", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "─────────────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "───────────────────────────────────────────────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[5], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[6], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, false);

            AddRow(_table, _row, _cell, "TOTAL ITENS:", iColWidth[1], false, false, true);
            AddRow(_table, _row, _cell, _itens.ToString(), iColWidth[2], true);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "TOTAL", iColWidth[4], true, true);
            AddRow(_table, _row, _cell, _PesoKgTotal.ToString("N2"), iColWidth[5], true, true);
            AddRow(_table, _row, _cell, 100.ToString("N2") + "%", iColWidth[6], true, true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);

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

    protected void btnSalvarDestino_Click(object sender, EventArgs e)
    {
        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        if (Session["dtRelacaoDTR"] != null)
            _dt = (DataTable)Session["dtRelacaoDTR"];
        foreach (DataRow dr in _dt.Rows)
        {
            oLancamentoMTR = new clsLancamentoMTR();
            oLancamentoMTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"].ToString());
            oLancamentoMTR.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"].ToString());
            oLancamentoMTR.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"].ToString());
            oLancamentoMTR.DataDescarga = dr["DataSaida"].ToString();
            oLancamentoMTR.Deposito = dr["LocalEntrega"].ToString();
            clsDestinoFinalDados oDestinoDados = new clsDestinoFinalDados();
            oLancamentoMTR.CodigoAterroSanitario = Convert.ToInt32(oDestinoDados.PegaCodigo(dr["LocalEntrega"].ToString()));
            oLancamentoMTRDados.SalvarDestinoDataDescarga(oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo, 
                                                          oLancamentoMTR.DataDescarga, oLancamentoMTR.CodigoAterroSanitario, oLancamentoMTR.Deposito);
        }
    }
}