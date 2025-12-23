using System;
using System.Text;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class RelatorioControleAterro : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
    clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
    string DataInicial = "";
    string DataFinal = "";

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
            if (Request.QueryString["DataInicial"] != "" && Request.QueryString["DataFinal"] != "")
                Relatorio();
        }            
    }
    private void Relatorio()
    {
        Panel1.BorderWidth = 0;

        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
       
        AddRow(_table, _row, _cell, geral.NomeEmpresa(1) + Environment.NewLine +  "Relatório Controle de Aterro", 160, false, true);
        AddRow(_table, _row, _cell, "Período de: " + Convert.ToDateTime(Request.QueryString["DataInicial"]).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Request.QueryString["DataFinal"]).ToString("dd/MM/yyyy"), 700, false, true);
        AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToString("dd/MM/yy"), 100, true);
        Panel1.Controls.Add(_table);

        clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        string _codigoAterro = "";
        if (Request.QueryString["CodigoAterro"] != null)
            _codigoAterro = Request.QueryString["CodigoAterro"].ToString();
        if (Request.QueryString["CodigoCliente"] != null)
            _dt = oLancamentoMTRDados.PegaDadosRelatorioControleAterro(oLancamentoMTR, Request.QueryString["DataInicial"], Request.QueryString["DataFinal"], Request.QueryString["CodigoCliente"], _codigoAterro);
        else
            _dt = oLancamentoMTRDados.PegaDadosRelatorioControleAterro(oLancamentoMTR, Request.QueryString["DataInicial"], Request.QueryString["DataFinal"], "", _codigoAterro);

        _dt = oAterroSanitarioDados.AdicionaSubTotal(_dt);
        if (_dt.Rows.Count > 0)
        {
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();

            int[] iColWidth = new int[13];
            iColWidth[1] = 50;   //Data
            iColWidth[2] = 50;   //Hora
            iColWidth[3] = 110;  //Local descarga
            iColWidth[4] = 60;   //Nº Ticket
            iColWidth[5] = 160;  //Motorista
            iColWidth[6] = 72;   //Placas
            iColWidth[7] = 80;   //Container
            iColWidth[8] = 70;   //Peso Total Kg
            iColWidth[9] = 50;   //Código Cliente  
            iColWidth[10] = 200; //Nome Cliente / Nome fantasia
            iColWidth[11] = 72;  //Total Grupo
            iColWidth[12] = 80;  //Peso p/Ticket

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
            AddRow(_table, _row, _cell, "Código", iColWidth[9], true, true);
            AddRow(_table, _row, _cell, "Cliente", iColWidth[10], false, true);
            AddRow(_table, _row, _cell, "Peso Grupo", iColWidth[11], true, true);
            AddRow(_table, _row, _cell, "Peso p/Ticket", iColWidth[12], false, true);

            AddRow(_table, _row, _cell, "──────", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "──────", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "─────────────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "───────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "────────────────────", iColWidth[5], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[6], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[8], false, true);
            AddRow(_table, _row, _cell, "───────", iColWidth[9], false, true);
            AddRow(_table, _row, _cell, "──────────────────────────", iColWidth[10], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[11], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[12], false, true);
            Panel1.Controls.Add(_table);

            decimal _vlSbTl = 0;
            decimal _valorTotalGeral = 0;
            string _DataAnterior = "";
            string _NumeroTicketX = "";
            decimal _PesoTicket = 0;
            if (_dt.Rows.Count > 0)
            {
                _DataAnterior = _dt.Rows[0]["Data"].ToString();
            }
            DataRow[] _ddr = _dt.Select();
            if (_ddr.Length > 0)
            {
                if (_ddr.Length > 1)
                    _NumeroTicketX = _ddr[1]["NumeroTicket"].ToString();
            }
            for (int i = 0; i < _ddr.Length; i++)
            {
                DataRow dr = _ddr[i];
                if (_DataAnterior != dr["Data"].ToString() && _DataAnterior != "")
                {
                    // adicionar linha com subtotais
                    //  AdicionaLinhaComSubtotais(_table, _row, _cell, iColWidth, _vlSbTl);
                    _vlSbTl = 0;
                }
                if (dr["Data"].ToString() != "")
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yy"), iColWidth[1], true);
                else
                    AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, dr["Hora"].ToString(), iColWidth[2], true);
                AddRow(_table, _row, _cell, geral.Left(dr["LocalAterro"].ToString(), 30), iColWidth[3], false);
                if (dr["NumeroTicket"].ToString() == "3408059")
                {
                    _vlSbTl = _vlSbTl + 0;
                }
                AddRow(_table, _row, _cell, dr["NumeroTicket"].ToString(), iColWidth[4], false);
                clsAterroSanitario oAtSanit = new clsAterroSanitario();
                clsAterroSanitarioDados oAtSanitDados = new clsAterroSanitarioDados();
                string sNomeMotorista = dr["NomeMotorista"].ToString();
                if (sNomeMotorista.Length > 0)
                {
                    if (sNomeMotorista.Split(" "[0]).Length > 0)
                    {
                        if (sNomeMotorista.Split(" "[0]).Length > 1)
                            AddRow(_table, _row, _cell, sNomeMotorista.Split(" "[0])[0] + " " + sNomeMotorista.Split(" "[0])[1], iColWidth[5], false);
                        else
                            AddRow(_table, _row, _cell, sNomeMotorista, iColWidth[5], false);
                    }
                    else
                        AddRow(_table, _row, _cell, sNomeMotorista, iColWidth[5], false);
                    AddRow(_table, _row, _cell, dr["Placas"].ToString(), iColWidth[6], false);
                }
                else
                {
                    try
                    {
                        if (dr["CodigoMotorista"].ToString() != "")
                            oAtSanit.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                        oAtSanitDados.PegaDados(oAtSanit, dr["NumeroTicket"].ToString(), true, oAtSanit.CodigoMotorista);
                        if (oAtSanit.NomeMotorista.Split(" "[0]).Length > 0)
                        {
                            if (oAtSanit.NomeMotorista.Split(" "[0]).Length > 1)
                                AddRow(_table, _row, _cell, oAtSanit.NomeMotorista.Split(" "[0])[0] + " " + oAtSanit.NomeMotorista.Split(" "[0])[1], iColWidth[5], false);
                            else
                                AddRow(_table, _row, _cell, oAtSanit.NomeMotorista, iColWidth[5], false);
                        }
                        else
                            AddRow(_table, _row, _cell, oAtSanit.NomeMotorista, iColWidth[5], false);
                        AddRow(_table, _row, _cell, oAtSanit.PlacasCaminhao, iColWidth[6], false);
                    }
                    catch
                    {
                        AddRow(_table, _row, _cell, oAtSanit.NomeMotorista, iColWidth[5], false);
                        AddRow(_table, _row, _cell, oAtSanit.PlacasCaminhao, iColWidth[6], false);
                    }
                }
                AddRow(_table, _row, _cell, dr["NumeroCaixa"].ToString(), iColWidth[7], false);
                if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "0" && Request.QueryString["CodigoCliente"] == null)
                {
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                    if (dr["NomeMotorista"].ToString() != "")
                    {
                        _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["TotalPeso"]);
                        _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["TotalPeso"]);
                    }
                }
                else if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "0" && Request.QueryString["CodigoCliente"] != null)
                {
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                    if (dr["NomeMotorista"].ToString() != "")
                    {
                        _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["TotalPeso"]);
                        _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["TotalPeso"]);
                    }
                }
                else
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);

                if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                    AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[9], true);
                else
                    AddRow(_table, _row, _cell, "", iColWidth[9], true);

                if (dr["NomeFantasia"].ToString() == "" && dr["Placas"].ToString() == "" && dr["NomeMotorista"].ToString() == "")
                    AddRow(_table, _row, _cell, "Subtotal", iColWidth[10], false);
                else
                    AddRow(_table, _row, _cell, geral.Left(dr["NomeFantasia"].ToString(), 30), iColWidth[10], false);

                if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "0" && Request.QueryString["CodigoCliente"] == null)
                {
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["TotalPeso"]).ToString("N2"), iColWidth[8], true);
                }
                else if (dr["TotalPeso"].ToString() != "" && dr["TotalPeso"].ToString() != "0" && Request.QueryString["CodigoCliente"] != null)
                {
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                }
                else
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                if (_ddr.Length > i + 1)
                    _NumeroTicketX = _ddr[i + 1]["NumeroTicket"].ToString();

                if (geral.RetiraLetras(dr["NumeroTicket"].ToString()) != geral.RetiraLetras(_NumeroTicketX))
                {
                    if (_PesoTicket == 0)
                    {
                        if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "0")
                        {
                            _PesoTicket = Convert.ToDecimal(dr["PesoIndividual"]);
                        }
                        if (dr["NomeFantasia"].ToString() == "" && dr["Placas"].ToString() == "")
                            AddRow(_table, _row, _cell, "", iColWidth[12], true);
                        else
                            AddRow(_table, _row, _cell, _PesoTicket.ToString("N2"), iColWidth[12], true);
                        _PesoTicket = 0;
                    }
                    else
                    {
                        if (i > 0)
                            if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "0")
                                _PesoTicket = _PesoTicket + Convert.ToDecimal(dr["PesoIndividual"]);
                        AddRow(_table, _row, _cell, _PesoTicket.ToString("N2"), iColWidth[12], true);
                        _PesoTicket = 0;
                    }
                }
                else
                {
                    if (dr["PesoIndividual"].ToString() != "" && dr["PesoIndividual"].ToString() != "0")
                    {
                        _PesoTicket = _PesoTicket + Convert.ToDecimal(dr["PesoIndividual"]);
                    }
                    if (_ddr.Length == i + 1)
                        AddRow(_table, _row, _cell, _PesoTicket.ToString("N2"), iColWidth[12], true);
                    else
                        AddRow(_table, _row, _cell, "", iColWidth[12], true);
                }
                if (dr["NomeFantasia"].ToString() == "" && dr["Placas"].ToString() == "" && dr["NomeMotorista"].ToString() == "")
                {
                    AddRow(_table, _row, _cell, "", iColWidth[1], true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false);
                    AddRow(_table, _row, _cell, "", iColWidth[4], false);
                    AddRow(_table, _row, _cell, "", iColWidth[5], false);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], true);
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                    AddRow(_table, _row, _cell, "", iColWidth[9], true);
                    AddRow(_table, _row, _cell, "", iColWidth[10], true);
                    AddRow(_table, _row, _cell, "", iColWidth[11], true);
                    AddRow(_table, _row, _cell, "", iColWidth[12], true);

                }
                _NumeroTicketX = dr["NumeroTicket"].ToString();
                _DataAnterior = dr["Data"].ToString();
            }
        
            // adicionar linha com subtotais
            /*AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "", iColWidth[4], false);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], true);
            AddRow(_table, _row, _cell, "Total Peso", iColWidth[10], true);
            AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[11], true);
            AddRow(_table, _row, _cell, "", iColWidth[12], true);
            */
            // adicionar linha com subtotais
            AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "", iColWidth[4], false);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], true);
            AddRow(_table, _row, _cell, "Total Peso", iColWidth[10], true);
            AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[11], true);
            AddRow(_table, _row, _cell, "", iColWidth[12], true);
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
        AddRow(_table, _row, _cell, "", iColWidth[9], true);
        AddRow(_table, _row, _cell, "Total Peso", iColWidth[10], true);
        AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[11], true);
        AddRow(_table, _row, _cell, "", iColWidth[12], true);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        string _script = "<script>window.location.href='../forms/ControleAterroSanitario.aspx?DataInicial=" + Request.QueryString["DataInicial"];
        _script = _script + "&DataFinal=" + Request.QueryString["DataFinal"];
        _script = _script + "&CodigoCliente=" + Request.QueryString["CodigoCliente"];
        _script = _script + "&CodigoAterro=" + Request.QueryString["CodigoAterro"];
        _script = _script + "'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }
}