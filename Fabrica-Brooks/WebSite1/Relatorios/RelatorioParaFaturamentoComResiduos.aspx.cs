using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class RelatorioParaFaturamentoComResiduos : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();

    clsClienteDados oClienteDados = new clsClienteDados();
    clsClientes oCliente = new clsClientes();
    clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
    clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
    clsContratos oContrato = new clsContratos();
    clsContratosDados oContratoDados = new clsContratosDados();
    clsContratosReajustes oContratoReajustes = new clsContratosReajustes();
    clsContratosReajustesDados oContratoReajustesDados = new clsContratosReajustesDados();
    clsEnderecos oEndereco = new clsEnderecos();
    clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
    DataTable _dt = new DataTable();
    private int _totalGeralColetas = 0;

    Table _table = new Table();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "35");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("~/forms/sempermissao.aspx");

        if (oUsuario == null)
        {
            Response.Redirect("brooks/loginaplicativo.aspx", true);
        }
        if (!IsPostBack)
        {
            lblTempoRestante.Visible = false;
            if (oUsuario.Nome.ToLower() == "teixeira")
            {
                lblTempoRestante.Visible = true;
                lblTempoRestante.Text = Session.Timeout.ToString();
            }
            string _ultimodiamesanterior = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year)).AddDays(-1).ToString("dd/MM/yyyy");
            Data1.Data = Convert.ToDateTime(("01/" + Convert.ToDateTime(_ultimodiamesanterior).Month.ToString() + "/" +
                                                     Convert.ToDateTime(_ultimodiamesanterior).Year.ToString())).ToString("dd/MM/yyyy");
            Data2.Data = Convert.ToDateTime(_ultimodiamesanterior).ToString("dd/MM/yyyy");
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
            
        }
    }

    private void Relatorio(bool pTitulo)
    {

        if (CLIENTESCONTROL1.Valor == "")
            CLIENTESCONTROL1.Valor = "0";

        oCliente.Codigo = Convert.ToInt32(CLIENTESCONTROL1.Valor);
        oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));

        
        //_table.Height = Unit.Parse("100%");
        TableRow _row = new TableRow();
        _row.Height = 22;
        TableCell _cell = new TableCell();

        if (pTitulo)
        { 
            AddRow(_table, _row, _cell, "Relatório para Faturamento com Resíduos - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 1030, false, true);
            Panel1.Controls.Add(_table);
        }
        else
        {
            AddRow(_table, _row, _cell, "───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", 1030, false, true);
            Panel1.Controls.Add(_table);
        }

        clsNotasFiscaisDados oNFSDados = new clsNotasFiscaisDados();
        _dt = new DataTable();
        _dt = oNFSDados.PegaDados(Convert.ToInt32(CLIENTESCONTROL1.Valor), Data1.Data, Data2.Data);
        int _CodigoClienteFaturado = 0;
        if (oCliente.CNPJ_Faturamento != "")
        {
            // listar notas fiscais do cnpj de faturamento - através do Código do Cliente
            _CodigoClienteFaturado = oClienteDados.PegaCodigoPeloCNPJ_Faturamento(oCliente.CNPJ_Faturamento);
            if (_CodigoClienteFaturado > 0)
            {
                _dt = new DataTable();
                _dt = oNFSDados.PegaDados(_CodigoClienteFaturado, Data1.Data, Data2.Data);
            }
        }

        if (_dt.Rows.Count > 0)
        {            
            int x = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                _table = new Table();
                _row = new TableRow();
                _row.Height = 22;
                _cell = new TableCell();
                _cell.BorderWidth = 0;
                if (x == 0)
                {
                    AddRow(_table, _row, _cell, "Cliente: " + "<br />", 53, false, true);
                    AddRow(_table, _row, _cell, oCliente.NomeFantasia + "<br />" + oCliente.Nome, 500, false, true);
                    AddRow(_table, _row, _cell, "Data Emissão: ", 100, false, true);
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataEmissao"]).ToString("dd/MM/yy"), 110, false, true);
                    AddRow(_table, _row, _cell, "Nº NF: ", 50, false, true);
                    AddRow(_table, _row, _cell, dr["NumeroNF"].ToString(), 80, false, true);
                    AddRow(_table, _row, _cell, "R$: ", 30, false, true);
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["ValorTotal"]).ToString("N2"), 66, false, true);
                    if (oCliente.CNPJ_Faturamento != "")
                        AddRow(_table, _row, _cell, "Faturado no CNPJ: " + oCliente.CNPJ_Faturamento, 300, false, true);
                }
                else
                {
                    AddRow(_table, _row, _cell, "", 53, false, true);
                    AddRow(_table, _row, _cell, "", 500, false, true);
                    AddRow(_table, _row, _cell, "", 100, false, true);
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataEmissao"]).ToString("dd/MM/yy"), 110, false, true);
                    AddRow(_table, _row, _cell, "", 50, false, true);
                    AddRow(_table, _row, _cell, dr["NumeroNF"].ToString(), 80, false, true);
                    AddRow(_table, _row, _cell, "R$: ", 30, false, true);
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["ValorTotal"]).ToString("N2"), 66, false, true);
                    if (oCliente.CNPJ_Faturamento != "")
                        AddRow(_table, _row, _cell, "Faturado no CNPJ: " + oCliente.CNPJ_Faturamento, 300, false, true);
                }
                Panel1.Controls.Add(_table);
                x++;
            }
        }
        else
        {
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            AddRow(_table, _row, _cell, "Cliente: " + "<br />", 53, false, true);
            AddRow(_table, _row, _cell, oCliente.NomeFantasia + "<br />" + oCliente.Nome, 520, false, true);
            AddRow(_table, _row, _cell, "Data Emissão: ", 100, false, true);
            AddRow(_table, _row, _cell, "____/____/_____", 110, false, true);
            AddRow(_table, _row, _cell, "Nº NF: ", 50, false, true);
            AddRow(_table, _row, _cell, "_________", 80, false, true);
            AddRow(_table, _row, _cell, "R$: ", 30, false, true);
            AddRow(_table, _row, _cell, "___________", 90, false, true);
            if (oCliente.CNPJ_Faturamento != "")
                AddRow(_table, _row, _cell, "Faturado no CNPJ: " + oCliente.CNPJ_Faturamento, 300, false, true);
            Panel1.Controls.Add(_table);
        }
        if (oCliente.Codigo > 0)
        {
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();
            string _DataUltimoBloqueioFinanceiro = oBloqueioFinanceiroDados.PegaDataUltimoBloqueioFinanceiro(oCliente.Codigo);
            if (_DataUltimoBloqueioFinanceiro != "")
            {
                AddRow(_table, _row, _cell, "BLOQUEIO FINANCEIRO DESDE: " + geral.Left(_DataUltimoBloqueioFinanceiro, 10), 1300, true, true);
                Panel1.Controls.Add(_table);
            }
            oContrato = new clsContratos();
            oContrato = oContratoDados.PegaDados(oContrato, 0, oCliente.Codigo);
            if (oContrato.Codigo > 0 && (oContrato.DataRecisao == "" || oContrato.DataRecisao == "01/01/0100" || oContrato.DataRecisao == "01/01/0001" || oContrato.DataRecisao == "1/1/100" || oContrato.DataRecisao == "1/1/0001"))
            {
                _table = new Table();
                _row = new TableRow();
                _cell = new TableCell();
                _cell.BorderWidth = 0;

                AddRow(_table, _row, _cell, "Código: " + oCliente.Codigo.ToString("000000"), 60, false, true);
                AddRow(_table, _row, _cell, "Nº Contrato: " + oContrato.NumeroContrato.ToString("000000"), 120, false, true);
                AddRow(_table, _row, _cell, "Valor R$ " + oContrato.ValorContrato.ToString("N2"), 110, false, true);
                AddRow(_table, _row, _cell, "Dt.Reaj: " + oContrato.DataReajuste, 120, false, true);
                AddRow(_table, _row, _cell, "Dt.Início: " + oContrato.DataInicio, 130, false, true);
                AddRow(_table, _row, _cell, "Indice: " + oContrato.IndiceReajuste, 400, false, true);
                // Pegar na tabela de documentação aplicável
                clsDocumentacaoAplicavelDados oDocumentacaoAplicavel = new clsDocumentacaoAplicavelDados();
                if (oDocumentacaoAplicavel.TemPLANFAT(oCliente.Codigo) > 0)
                    AddRow(_table, _row, _cell, "PLANFAT", 300, false, true);
                Panel1.Controls.Add(_table);

                if (oContrato.Observacao != "")
                {
                    _table = new Table();
                    _row = new TableRow();
                    _cell = new TableCell();
                    _cell.BorderWidth = 0;
                    AddRow(_table, _row, _cell, "OBS: " + oContrato.Observacao, 1300, false);
                    Panel1.Controls.Add(_table);
                }
                _dt = oContratoReajustesDados.PreencheDataTable("Data desc", oContrato.Codigo);
                if (_dt.Rows.Count > 0)
                {
                    string DataUltimoReajuste = _dt.Rows[0]["Data"].ToString();
                    _dt = oContratoResiduosDados.PreencheDataTableContratoResiduos("CodigoResiduo", oContrato.Codigo, DataUltimoReajuste, oContrato.CodigoCliente);

                    _table = new Table();
                    _row = new TableRow();
                    _cell = new TableCell();

                    int[] iColWidth = new int[22];
                    iColWidth[1] = 52;  // Código Residuo
                    iColWidth[2] = 268;  // Descrição residuo
                    iColWidth[3] = 32;   // Cx Disp
                    iColWidth[4] = 32;   // Cx Tipo
                    iColWidth[5] = 138;  // Frequencia de coleta

                    // cobrança - coleta
                    iColWidth[6] = 60;  //franquia de coleta
                    iColWidth[7] = 92;  //expressao 2 - coletas (por)
                    iColWidth[8] = 90;  //periodicidade
                    iColWidth[9] = 106;  //Valor / Excedente

                    // cobrança - peso / volume
                    iColWidth[10] = 54; //expressao 3 - cobrar
                    iColWidth[11] = 54; //valor por peso / volume
                    iColWidth[12] = 34; //por
                    iColWidth[13] = 32; //unidade
                    iColWidth[14] = 84; //condicao 2 - excedente a
                    iColWidth[15] = 60; //franquia 
                    iColWidth[16] = 28; //unidade franquia
                    iColWidth[17] = 28; //expressao 4 - por
                    iColWidth[18] = 48; //expressao 5 

                    iColWidth[19] = 82; //Data Programada
                    iColWidth[20] = 72; //ultima coleta
                    iColWidth[21] = 308;//obeservação
                    int tWidthContratos = 0;
                    foreach (int iTW in iColWidth)
                        tWidthContratos = tWidthContratos + iTW;
                    _table.Width = tWidthContratos + 10;

                    AddRow(_table, _row, _cell, "", tWidthContratos, false, true);

                    // titulo 1
                    AddRow(_table, _row, _cell, "Código", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "Cx", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "Cx", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[5], false, true);

                    // cobrança - coleta
                    AddRow(_table, _row, _cell, "┌────────DADOS&nbsp;PARA&nbsp;COBRANÇA&nbsp;-&nbsp;COLETA&nbsp;──────┐", iColWidth[6] + iColWidth[7] + iColWidth[8] + iColWidth[9], false, true);

                    // cobrança - peso / volume
                    AddRow(_table, _row, _cell, "┌────────DADOS&nbsp;PARA&nbsp;COBRANÇA&nbsp;-&nbsp;PESO&nbsp;/&nbsp;VOLUME────────┐", iColWidth[10] + iColWidth[11] + iColWidth[12] + iColWidth[13] + iColWidth[14] + iColWidth[15] + iColWidth[16] + iColWidth[17] + iColWidth[18], false, true);

                    AddRow(_table, _row, _cell, "Data", iColWidth[19], false, true);
                    AddRow(_table, _row, _cell, "Última", iColWidth[20], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[21], false, true);

                    // titulo 2
                    AddRow(_table, _row, _cell, "Resíduo", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "Descrição Resíduo", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "Disp", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "Tipo", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "Frequência de Coleta", iColWidth[5], false, true);

                    // cobrança - coleta
                    AddRow(_table, _row, _cell, "Franquia", iColWidth[6], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], false, true);
                    AddRow(_table, _row, _cell, "Periodicidade", iColWidth[8], false, true);
                    AddRow(_table, _row, _cell, "Valor/Excedente", iColWidth[9], false, true);

                    // cobrança - peso / volume
                    AddRow(_table, _row, _cell, "", iColWidth[10], false, true); //cobrar
                    AddRow(_table, _row, _cell, "Unitário", iColWidth[11], false, true); 
                    AddRow(_table, _row, _cell, "", iColWidth[12], false, true); //por
                    AddRow(_table, _row, _cell, "Unid", iColWidth[13], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[14], false, true); // excedente a
                    AddRow(_table, _row, _cell, "Franquia", iColWidth[15], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[16], false, true); // unidade
                    AddRow(_table, _row, _cell, "", iColWidth[17], false, true); // por
                    AddRow(_table, _row, _cell, "", iColWidth[18], false, true); // coleta

                    AddRow(_table, _row, _cell, "Programada", iColWidth[19], false, true);
                    AddRow(_table, _row, _cell, "Coleta", iColWidth[20], false, true);
                    AddRow(_table, _row, _cell, "Observação", iColWidth[21], false, true);
                    
                    foreach (DataRow dr in _dt.Rows)
                    {
                        AddRow(_table, _row, _cell, dr["CodigoResiduo"].ToString(), iColWidth[1], true);
                        AddRow(_table, _row, _cell, dr["DescricaoReduzidaResiduo"].ToString(), iColWidth[2], false);
                        AddRow(_table, _row, _cell, dr["CaixaDisponivel"].ToString(), iColWidth[3], false);
                        AddRow(_table, _row, _cell, dr["TipoCaixa"].ToString(), iColWidth[4], false);
                        AddRow(_table, _row, _cell, dr["FrequenciaColeta"].ToString(), iColWidth[5], false);

                        if (dr["QuantidadeFranquia"].ToString() != "")
                            AddRow(_table, _row, _cell, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToDecimal(dr["QuantidadeFranquia"]).ToString("N2"), iColWidth[6], true);
                        else
                            AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                        AddRow(_table, _row, _cell, dr["expressao2CobrancaMensal"].ToString(), iColWidth[7], false);
                        AddRow(_table, _row, _cell, dr["PeriodicidadeCobrancaMensal"].ToString(), iColWidth[8], false);
                        decimal _vue = Convert.ToDecimal(dr["ValorExcedenteCobrancaMensal"]);
                        AddRow(_table, _row, _cell, _vue.ToString("N2"), iColWidth[9], true); 
                        AddRow(_table, _row, _cell, "&nbsp;" + dr["expressao1CobrancaPeso"].ToString(), iColWidth[10], false);                        
                        _vue = Convert.ToDecimal(dr["ValorUnitario"]);
                        _vue = _vue / 100;
                        AddRow(_table, _row, _cell, _vue.ToString("N2"), iColWidth[11], true);
                        AddRow(_table, _row, _cell, dr["expressao2CobrancaPeso"].ToString(), iColWidth[12], false);
                        AddRow(_table, _row, _cell, dr["Unidade"].ToString(), iColWidth[13], false);
                        AddRow(_table, _row, _cell, dr["condicaoCobrancaPeso"].ToString(), iColWidth[14], false);
                        _vue = Convert.ToDecimal(dr["FranquiaCobrancaPeso"]);
                        AddRow(_table, _row, _cell, _vue.ToString("N2"), iColWidth[15], true);
                        AddRow(_table, _row, _cell, dr["UnidadeCobrancaPeso"].ToString(), iColWidth[16], false);

                        AddRow(_table, _row, _cell, dr["expressao3CobrancaPeso"].ToString(), iColWidth[17], false);
                        AddRow(_table, _row, _cell, dr["expressao4CobrancaPeso"].ToString(), iColWidth[18], false);

                        string tst = "";// só pra teste
                        if (Convert.ToInt32(dr["CodigoResiduo"]) == 242)
                            tst = dr["CodigoResiduo"].ToString(); // só pra teste

                        string _dataProximaColeta = oContratoResiduosDados.PegaDataProxColeta(oContrato.CodigoCliente, Convert.ToInt32(dr["CodigoResiduo"]), dr["FrequenciaColeta"].ToString(), Data1.Data);
                        AddRow(_table, _row, _cell, _dataProximaColeta, iColWidth[19], false);

                        string _dataUltimaColeta = "";
                        _dataUltimaColeta = oContratoResiduosDados.PegaDataUltimaColeta(oContrato.CodigoCliente, Convert.ToInt32(dr["CodigoResiduo"]), Data1.Data);
                        AddRow(_table, _row, _cell, _dataUltimaColeta, iColWidth[20], false);

                        AddRow(_table, _row, _cell, dr["Obs"].ToString(), iColWidth[21], false);
                    }

                    Panel1.Controls.Add(_table);
                }
            }
            else
            {
                _table = new Table();
                _row = new TableRow();
                _cell = new TableCell();
                _cell.BorderWidth = 0;
                AddRow(_table, _row, _cell, "Código: " + oCliente.Codigo.ToString("000000"), 60, false, true);
                Panel1.Controls.Add(_table);
            }
            _dt = oLancamentoDados.PreencheDadosParaFaturamento(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), "GrupoResiduo, Residuo, DataRetirada, DataColocacao", true);
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();
                //_table.Attributes.CssStyle.Add("cellspacing", "0");
                //_table.Attributes.CssStyle.Add("cellpadding", "0");
                //_table.Attributes.CssStyle.Add("border", "0");
                //_table.Attributes.CssStyle.Add("margin-bottom", "0");
                //_table.Attributes.CssStyle.Add("margin-top", "0");
                _row = new TableRow();
                _cell = new TableCell();

                int[] iColWidth = new int[16];
                iColWidth[1] = 80;   //NºLançamento
                iColWidth[2] = 66;   //Caminhão 

                iColWidth[3] = 66;   //Nº MTR

                iColWidth[4] = 50;   //NºCaixa
                iColWidth[5] = 70;   //DataColocacao
                iColWidth[6] = 70;   //DataRetirada
                iColWidth[7] = 400;  //GrupoResíduo-Resíduo
                iColWidth[8] = 88;   //Qt.Coletada
                iColWidth[9] = 78;   //Qt.Descarga
                iColWidth[10] = 30;  //Und
                iColWidth[11] = 80;  //Unitário
                iColWidth[12] = 80;  //Total
                iColWidth[13] = 80;  //MTRe
                iColWidth[14] = 208; //Observação
                iColWidth[15] = 120; //Destino Final

                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;

                AddRow(_table, _row, _cell, "", tWidthContratos, false, true);

                AddRow(_table, _row, _cell, "Nº Lanç.", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "Caminhão", iColWidth[2], false, true);

                AddRow(_table, _row, _cell, "Nº MTR", iColWidth[3], false, true);

                AddRow(_table, _row, _cell, "Caixa", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "Dt.Colocac", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "Dt.Coleta", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "Grupo/SubGrupo (Resíduos)", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "Qt.Coletada", iColWidth[8], true, true);
                AddRow(_table, _row, _cell, "Qt.Descarga", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "Und", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "Unitário", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "Total", iColWidth[12], false, true);
                AddRow(_table, _row, _cell, "MTR-e", iColWidth[13], false, true);
                AddRow(_table, _row, _cell, "Observação", iColWidth[14], false, true);
                AddRow(_table, _row, _cell, "Destino Final", iColWidth[15], false, true);

                //AddRow(_table, _row, _cell, "──────────", iColWidth[1], false, true);
                //AddRow(_table, _row, _cell, "────────", iColWidth[2], false, true);
                //AddRow(_table, _row, _cell, "────────", iColWidth[3], false, true);
                //AddRow(_table, _row, _cell, "──────", iColWidth[4], false, true);
                //AddRow(_table, _row, _cell, "────────", iColWidth[5], false, true);
                //AddRow(_table, _row, _cell, "────────", iColWidth[6], false, true);
                //AddRow(_table, _row, _cell, "────────────────────────────────────────────────────────", iColWidth[7], false, true);
                //AddRow(_table, _row, _cell, "────────", iColWidth[8], true, true);
                //AddRow(_table, _row, _cell, "─────────", iColWidth[9], false, true);
                //AddRow(_table, _row, _cell, "───", iColWidth[10], false, true);
                //AddRow(_table, _row, _cell, "──────────", iColWidth[11], false, true);
                //AddRow(_table, _row, _cell, "──────────", iColWidth[12], false, true);
                //AddRow(_table, _row, _cell, "──────────", iColWidth[13], false, true);
                //AddRow(_table, _row, _cell, "───────────────────────────", iColWidth[14], false, true);
                //AddRow(_table, _row, _cell, "────────────", iColWidth[15], false, true);

                decimal _QtSbTlColeta = 0;
                decimal _QtSbTlDescarga = 0;
                decimal _vlSbTl = 0;
                decimal _valorTotalGeral = 0;
                string _grupoEresiduoAnterior = "";
                if (_dt.Rows.Count > 0)
                    _grupoEresiduoAnterior = _dt.Rows[0]["GrupoResiduo"].ToString() + "-" + _dt.Rows[0]["Residuo"].ToString();

                foreach (DataRow dr in _dt.Rows)
                {
                    if (_grupoEresiduoAnterior != dr["GrupoResiduo"].ToString() + "-" + dr["Residuo"].ToString() && _grupoEresiduoAnterior != "")
                    {
                        // adicionar linha com subtotais
                        AddRow(_table, _row, _cell, "", iColWidth[1], true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false);

                        AddRow(_table, _row, _cell, "", iColWidth[3], false);

                        AddRow(_table, _row, _cell, "", iColWidth[4], false);
                        AddRow(_table, _row, _cell, "", iColWidth[5], false);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        AddRow(_table, _row, _cell, "Subtotal Resíduo", iColWidth[7], true);

                        AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[8], true);
                        AddRow(_table, _row, _cell, _QtSbTlDescarga.ToString("N2") + "", iColWidth[9], true);

                        AddRow(_table, _row, _cell, "", iColWidth[10], false);
                        AddRow(_table, _row, _cell, 0.ToString("N2") + "", iColWidth[11], true);

                        AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[12], true);

                        AddRow(_table, _row, _cell, "", iColWidth[13], false);
                        AddRow(_table, _row, _cell, "", iColWidth[14], false);
                        AddRow(_table, _row, _cell, "", iColWidth[15], false);

                        _QtSbTlColeta = 0;
                        _QtSbTlDescarga = 0;
                        _vlSbTl = 0;
                        AddRow(_table, _row, _cell, "", iColWidth[1], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false, true);

                        AddRow(_table, _row, _cell, "", iColWidth[3], false, true);

                        AddRow(_table, _row, _cell, "", iColWidth[4], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[5], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[7], true, true);
                        AddRow(_table, _row, _cell, "", iColWidth[8], true, true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[10], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[11], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[12], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[13], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[14], false, true);
                        AddRow(_table, _row, _cell, "", iColWidth[15], false, true);
                    }
                    _grupoEresiduoAnterior = dr["GrupoResiduo"].ToString() + "-" + dr["Residuo"].ToString();

                    // colocar asterico
                    if (Convert.ToInt32(dr["qtLancsDoMesmo"].ToString()) > 1)
                        AddRow(_table, _row, _cell, "*" + dr["NumeroLancamento"].ToString(), iColWidth[1], true);
                    else
                        AddRow(_table, _row, _cell, "&nbsp;" + dr["NumeroLancamento"].ToString(), iColWidth[1], true);

                    AddRow(_table, _row, _cell, geral.Left(dr["Caminhao"].ToString(), 10), iColWidth[2], false);

                    AddRow(_table, _row, _cell, dr["NumeroMTR"].ToString(), iColWidth[3], false);

                    AddRow(_table, _row, _cell, dr["NumeroCaixa"].ToString(), iColWidth[4], false);

                    if (dr["DataColocacao"].ToString() != "")
                        AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataColocacao"]).ToString("dd/MM/yy"), iColWidth[5], false);
                    else
                        AddRow(_table, _row, _cell, "", iColWidth[5], false);

                    if (dr["DataRetirada"].ToString() != "")
                        AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataRetirada"]).ToString("dd/MM/yy"), iColWidth[6], false);
                    else
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);

                    AddRow(_table, _row, _cell, geral.Left(dr["GrupoResiduo"].ToString(), 20) + "-" + geral.Left(dr["Residuo"].ToString(), 20), iColWidth[7], false);

                    if (dr["QtColetada"].ToString() != "")
                    {
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtColetada"]).ToString("N2"), iColWidth[8], true);
                        _QtSbTlColeta = _QtSbTlColeta + Convert.ToDecimal(dr["QtColetada"]);
                    }
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[8], true);

                    if (dr["QtDescarga"].ToString() != "")
                    {
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[9], true);
                        _QtSbTlDescarga = _QtSbTlDescarga + Convert.ToDecimal(dr["QtDescarga"]);
                    }
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[9], true);

                    AddRow(_table, _row, _cell, dr["Und"].ToString(), iColWidth[10], false);

                    if (dr["Unitario"].ToString() != "")
                    {
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["Unitario"]).ToString("N2"), iColWidth[11], true);
                    }
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[11], true);

                    if (dr["Total"].ToString() != "")
                    {
                        AddRow(_table, _row, _cell, Convert.ToDecimal(dr["Total"]).ToString("N2"), iColWidth[12], true);
                        _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["Total"]);
                        _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["Total"]);
                    }
                    else
                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[12], true);
                    AddRow(_table, _row, _cell, dr["MTRe"].ToString(), iColWidth[13], false);
                    AddRow(_table, _row, _cell, dr["Obs"].ToString(), iColWidth[14], false);
                    AddRow(_table, _row, _cell, dr["Destino"].ToString(), iColWidth[15], false);
                }
                // adicionar linha com subtotal
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], false);
                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                AddRow(_table, _row, _cell, "Subtotal Resíduo", iColWidth[7], true);
                AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[8], true);
                AddRow(_table, _row, _cell, _QtSbTlDescarga.ToString("N2") + "", iColWidth[9], true);
                AddRow(_table, _row, _cell, "", iColWidth[10], false);
                AddRow(_table, _row, _cell, 0.ToString("N2") + "", iColWidth[11], true);
                AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[12], true);
                AddRow(_table, _row, _cell, "", iColWidth[13], false);
                AddRow(_table, _row, _cell, "", iColWidth[14], false);
                AddRow(_table, _row, _cell, "", iColWidth[15], false);

                // adicionar linha com total geral
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], false);
                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                AddRow(_table, _row, _cell, "Total Geral Resíduos", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, "", iColWidth[10], false);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[12], true);
                AddRow(_table, _row, _cell, "", iColWidth[13], false);
                AddRow(_table, _row, _cell, "", iColWidth[14], false);
                AddRow(_table, _row, _cell, "", iColWidth[15], false);

                _dt = new DataTable();
                _dt = oLancamentoDados.PegaDadosDeColetasPorPeriodo(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor));

                int _totalColetas = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (_dr["ehTerceiro"].ToString() == "1")
                    {
                        //NumeroCaixa, count(NumeroCaixa) as QtCaixas, EhTerceiro
                        AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                        AddRow(_table, _row, _cell, _dr["NumeroCaixa"].ToString(), iColWidth[2], false);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        AddRow(_table, _row, _cell, "", iColWidth[4], false);
                        AddRow(_table, _row, _cell, _dr["QtCaixas"].ToString(), iColWidth[5], true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        AddRow(_table, _row, _cell, "", iColWidth[7], true);
                        AddRow(_table, _row, _cell, "", iColWidth[8], true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], true);
                        AddRow(_table, _row, _cell, "", iColWidth[10], false);
                        AddRow(_table, _row, _cell, "", iColWidth[11], true);
                        AddRow(_table, _row, _cell, "", iColWidth[12], true);
                        AddRow(_table, _row, _cell, "", iColWidth[13], false);
                        AddRow(_table, _row, _cell, "", iColWidth[14], false);
                        AddRow(_table, _row, _cell, "", iColWidth[15], false);
                        _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
                    }
                }
                AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                AddRow(_table, _row, _cell, "Terceiros", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, _totalColetas.ToString(), iColWidth[5], true);
                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, "", iColWidth[10], false);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                AddRow(_table, _row, _cell, "", iColWidth[13], false);
                AddRow(_table, _row, _cell, "", iColWidth[14], false);
                AddRow(_table, _row, _cell, "", iColWidth[15], false);

                // linha em branco
                AddRow(_table, _row, _cell, "", iColWidth[1], true);
                AddRow(_table, _row, _cell, "", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, "", iColWidth[5], true);
                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, "", iColWidth[10], false);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                AddRow(_table, _row, _cell, "", iColWidth[13], false);
                AddRow(_table, _row, _cell, "", iColWidth[14], false);
                AddRow(_table, _row, _cell, "", iColWidth[15], false);

                _totalColetas = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (_dr["ehTerceiro"].ToString() == "0")
                    {
                        AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                        AddRow(_table, _row, _cell, _dr["NumeroCaixa"].ToString(), iColWidth[2], false);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        AddRow(_table, _row, _cell, "", iColWidth[4], false);
                        AddRow(_table, _row, _cell, _dr["QtCaixas"].ToString(), iColWidth[5], true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        AddRow(_table, _row, _cell, "", iColWidth[7], true);
                        AddRow(_table, _row, _cell, "", iColWidth[8], true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], true);
                        AddRow(_table, _row, _cell, "", iColWidth[10], false);
                        AddRow(_table, _row, _cell, "", iColWidth[11], true);
                        AddRow(_table, _row, _cell, "", iColWidth[12], true);
                        AddRow(_table, _row, _cell, "", iColWidth[13], false);
                        AddRow(_table, _row, _cell, "", iColWidth[14], false);
                        AddRow(_table, _row, _cell, "", iColWidth[15], false);
                    }
                }
                _totalColetas = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
                    _totalGeralColetas = _totalGeralColetas + Convert.ToInt32(_dr["QtCaixas"]);
                }
                AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                AddRow(_table, _row, _cell, "Cliente", iColWidth[2], false);
                AddRow(_table, _row, _cell, "", iColWidth[3], false);
                AddRow(_table, _row, _cell, "", iColWidth[4], false);
                AddRow(_table, _row, _cell, _totalColetas.ToString(), iColWidth[5], true);
                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                AddRow(_table, _row, _cell, "", iColWidth[7], true);
                AddRow(_table, _row, _cell, "", iColWidth[8], true);
                AddRow(_table, _row, _cell, "", iColWidth[9], true);
                AddRow(_table, _row, _cell, "", iColWidth[10], false);
                AddRow(_table, _row, _cell, "", iColWidth[11], true);
                AddRow(_table, _row, _cell, "", iColWidth[12], true);
                AddRow(_table, _row, _cell, "", iColWidth[13], false);
                AddRow(_table, _row, _cell, "", iColWidth[14], false);
                AddRow(_table, _row, _cell, "", iColWidth[15], false);
                Panel1.Controls.Add(_table);
            }
        }

    }

    private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false)
    {
        Label _lbl = new Label();
        _lbl.Text = pText + "&nbsp;";
        _lbl.Width = pWidth;
        _lbl.Font.Name = "Tahoma";
        //_lbl.Font.Size = FontUnit.Parse("7.9");
        _lbl.Font.Size = FontUnit.Parse("9");
        _lbl.Attributes.CssStyle.Add("margin-top", "0");
        _lbl.Attributes.CssStyle.Add("margin-bottom", "0");
        //_lbl.Attributes.CssStyle.Add("border-left", "1px solid");
        _lbl.Attributes.CssStyle.Add("border-bottom", "0");
        //_lbl.Height = Unit.Parse("1");

        if (pText.IndexOf("DADOS PARA COBRANÇA - COLETA") > -1)
        {
            //_lbl.Attributes.CssStyle.Add("border-bottom", "1px solid black");
        }
        if (pText.IndexOf("DADOS PARA COBRANÇA - PESO") > -1)
        {
            //_lbl.Attributes.CssStyle.Add("border-bottom", "1px solid black");
        }
        if (pNegrito)
        {
            _lbl.Font.Bold = true;
            //cell.Attributes.CssStyle.Add("border", "0px solid black");
        }
        if (pAlinDireita)
            _lbl.Attributes.CssStyle.Add("text-align", "right");
        cell.Controls.Add(_lbl);
        row.Cells.Add(cell);
        _table.BorderWidth = 0;
        _table.CellSpacing = 0;
        _table.CellPadding = 0;

        _table.Rows.Add(row);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (CLIENTESCONTROL1.Valor == "0")
        {
            CLIENTESCONTROL1.Valor = "";
            btnOk.Text = "Ok";
        }
        if (CLIENTESCONTROL1.Valor != "")
        {
            btnOk.Text = "Ok";
            try
            {
                _totalGeralColetas = 0;
            }
            finally
            {
                if (Data1.Data != "")
                {
                    if (Data2.Data == "")
                        Data2.Data = Data1.Data;
                    Relatorio(true);
                    AdicionaTotalGeralColetas();
                }
                else
                    AdicionaErroData();
            }
        }
        else
        {
            if (btnOk.Text == "Gerar todos clientes?")
            {
                try
                {
                    _totalGeralColetas = 0;
                }
                finally
                {
                    if (Data1.Data != "")
                    {
                        if (Data2.Data == "")
                            Data2.Data = Data1.Data;
                        DataTable _dtClientesLancamentos = new DataTable();
                        _dtClientesLancamentos = oLancamentoDados.PreencheDTClientesLancamentosContratos(Data1.Data, Data2.Data);
                        for (int i = 0; i < _dtClientesLancamentos.Rows.Count; i++)
                        {

                            DataRow dr = _dtClientesLancamentos.Rows[i];
                            CLIENTESCONTROL1.Valor = dr["CodigoCliente"].ToString();
                            CLIENTESCONTROL1.Texto = dr["NomeCliente"].ToString();
                            if (CLIENTESCONTROL1.Valor == "2852")
                                CLIENTESCONTROL1.Valor = "2852";
                            if (i == 0)
                                Relatorio(true);
                            else
                                Relatorio(false);
                        }
                        AdicionaTotalGeralColetas();
                    }
                    else
                        AdicionaErroData();
                }
                btnOk.Text = "Ok";
            }
            else
            {
                btnOk.Text = "Gerar todos clientes?";
            }            
        }
    }
    private void AdicionaTotalGeralColetas()
    {
        _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        AddRow(_table, _row, _cell, "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total geral coletas", 146, false);
        AddRow(_table, _row, _cell, "", 66, false);
        AddRow(_table, _row, _cell, "", 50, false);
        AddRow(_table, _row, _cell, _totalGeralColetas.ToString(), 70, true);
        Panel1.Controls.Add(_table);
    }

    private void AdicionaErroData()
    {
        _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        AddRow(_table, _row, _cell, "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Data inválida ou formato incorreto!", 400, false);
        Panel1.Controls.Add(_table);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        CLIENTESCONTROL1.Valor = "";
        CLIENTESCONTROL1.Texto = "";
        Session["Clientes"] = null;
        Session["DestinoFinal"] = null;
        string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }

    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        Label lblColuna = new Label();
        //if (CLIENTESCONTROL1.Valor == "")
        //{
        //    Panel1.Controls.Clear();
        //    lblColuna.Font.Bold = true;
        //    lblColuna.Text = "Código do cliente inválido!";
        //    Panel1.Controls.Add(lblColuna);
        //    return;
        //}
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
        GridView Grade2 = new GridView();
        GridView Grade3 = new GridView();

        Panel1.Controls.Add(Grade);
        string NomeArq = "Relatorio_Faturamento.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;

        if (CLIENTESCONTROL1.Valor != "")
            _dt = oLancamentoDados.PreencheDadosParaFaturamento(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), "GrupoResiduo, Residuo, DataRetirada, DataColocacao", true);
        else
            _dt = oLancamentoDados.PreencheDadosParaFaturamento(Data1.Data, Data2.Data, 0, "GrupoResiduo, Residuo, DataRetirada, DataColocacao", true, true);
        if (CLIENTESCONTROL1.Valor != "")
        {
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        }
        else
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        Grade.DataSource = _dt;
        Grade.DataBind();

        if (_dt.Rows.Count > 0)
        {

            for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);

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
            lblTitulo.Text = "Relatorio para faturamento com residuos - Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            if (CLIENTESCONTROL1.Valor != "")
            {
                DataTable dtTitulo = new DataTable();
                DataColumn dc = new DataColumn();
                dc.ColumnName = "Codigo";
                dc.Caption = "Codigo";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "NomeFantasia";
                dc.Caption = "NomeFantasia";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "Cliente";
                dc.Caption = "Cliente";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "Contrato";
                dc.Caption = "Contrato";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "Valor";
                dc.Caption = "Valor";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "Reajuste";
                dc.Caption = "Reajuste";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "DataInicio";
                dc.Caption = "DataInicio";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "IndiceReajuste";
                dc.Caption = "IndiceReajuste";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "Emissao";
                dc.Caption = "Emissao";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "NumeroNF";
                dc.Caption = "NumeroNF";
                dtTitulo.Columns.Add(dc);

                dc = new DataColumn();
                dc.ColumnName = "ValorNF";
                dc.Caption = "ValorNF";
                dtTitulo.Columns.Add(dc);

                oCliente.Codigo = Convert.ToInt32(CLIENTESCONTROL1.Valor);
                oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));

                oContrato = new clsContratos();
                oContrato = oContratoDados.PegaDados(oContrato, 0, Convert.ToInt16(CLIENTESCONTROL1.Valor));

                DataRow dr = dtTitulo.NewRow();
                dr[0] = CLIENTESCONTROL1.Valor;
                dr[1] = geral.RemoverAcentos(oCliente.NomeFantasia);
                dr[2] = geral.RemoverAcentos(oCliente.Nome);
                dr[3] = oContrato.NumeroContrato;
                dr[4] = oContrato.ValorContrato;
                dr[5] = oContrato.DataReajuste;
                dr[6] = oContrato.DataInicio;
                dr[7] = oContrato.IndiceReajuste;
                dtTitulo.Rows.Add(dr);

                clsNotasFiscaisDados oNFSDados = new clsNotasFiscaisDados();
                _dt = new DataTable();
                _dt = oNFSDados.PegaDados(Convert.ToInt32(CLIENTESCONTROL1.Valor), Data1.Data, Data2.Data);

                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow drNF in _dt.Rows)
                    {
                        dr = dtTitulo.NewRow();
                        dr[8] = Convert.ToDateTime(drNF["DataEmissao"]).ToString("dd/MM/yy");
                        dr[9] = drNF["NumeroNF"].ToString();
                        dr[10] = Convert.ToDecimal(drNF["ValorTotal"]).ToString("N2");
                        dtTitulo.Rows.Add(dr);
                    }
                }

                Grade3.DataSource = dtTitulo;
                Grade3.DataBind();
                Grade3.HeaderRow.Cells[0].Text = "Codigo Cliente";
                Grade3.HeaderRow.Cells[1].Text = "Nome fantasia";
                Grade3.HeaderRow.Cells[2].Text = "Nome/Razao social";
                Grade3.HeaderRow.Cells[3].Text = "Numero contrato";
                Grade3.HeaderRow.Cells[4].Text = "Valor contrato";
                Grade3.HeaderRow.Cells[5].Text = "Data reajuste";
                Grade3.HeaderRow.Cells[6].Text = "Data inicio";
                Grade3.HeaderRow.Cells[7].Text = "Indice de reajuste";
                Grade3.HeaderRow.Cells[8].Text = "Data emissao NF";
                Grade3.HeaderRow.Cells[9].Text = "Numero da NF";
                Grade3.HeaderRow.Cells[10].Text = "Valor da NF";

                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblColuna = new Label();
                lblColuna.ID = "OBS";
                lblColuna.Text = "OBS: " + geral.RemoverAcentos(oContrato.Observacao);
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);


                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
                table.Rows.Add(row);

                row = new TableRow();
                row.Cells.Add(new TableCell());
                row.Cells[0].Controls.Add(Grade3);
                table.Rows.Add(row);

                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
                table.Rows.Add(row);

                if (oContrato.Codigo > 0 && (oContrato.DataRecisao == "" || oContrato.DataRecisao == "01/01/0100" || oContrato.DataRecisao == "01/01/0001" || oContrato.DataRecisao == "1/1/100" || oContrato.DataRecisao == "1/1/0001"))
                {
                    clsDocumentacaoAplicavelDados oDocumentacaoAplicavel = new clsDocumentacaoAplicavelDados();
                    if (oDocumentacaoAplicavel.TemPLANFAT(oCliente.Codigo) > 0)
                        if (oContrato.Observacao != "")
                        {
                            row = new TableRow();
                            row.Cells.Add(new TableCell());
                            lblColuna = new Label();
                            lblColuna.Font.Bold = true;
                            lblColuna.ID = "PLANFAT";
                            lblColuna.Text = "PLANFAT";
                            row.Cells[0].Controls.Add(lblColuna);
                            table.Rows.Add(row);

                            row = new TableRow();
                            row.Cells.Add(new TableCell());
                            lblEmBranco.ID = "lblEmBranco";
                            lblEmBranco.Text = "\n";
                            row.Cells[0].Controls.Add(lblEmBranco);
                            table.Rows.Add(row);

                        }
                    _dt = oContratoReajustesDados.PreencheDataTable("Data desc", oContrato.Codigo);
                    if (_dt.Rows.Count > 0)
                    {
                        string DataUltimoReajuste = _dt.Rows[0]["Data"].ToString();

                        DataTable _dtContResiduos = new DataTable();
                        _dtContResiduos = oContratoResiduosDados.PreencheDTContratoResiduosExcel("CodigoResiduo", oContrato.Codigo, DataUltimoReajuste);
                        foreach (DataRow _drContRes in _dtContResiduos.Rows)
                        {
                            string _dataProximaColeta = oContratoResiduosDados.PegaDataProxColeta(oContrato.CodigoCliente, Convert.ToInt32(_drContRes["CodigoResiduo"]), _drContRes["FrequenciaColeta"].ToString(), Data1.Data);
                            _drContRes["DataProgramada"] = _dataProximaColeta;
                            string _dataUltimaColeta = oContratoResiduosDados.PegaDataUltimaColeta(oContrato.CodigoCliente, Convert.ToInt32(_drContRes["CodigoResiduo"]), Data1.Data);
                            _drContRes["UltimaColeta"] = _dataUltimaColeta;

                            if (_drContRes["ValorUnitario"].ToString() != "")
                                _drContRes["ValorUnitario"] = Convert.ToDecimal(_drContRes["ValorUnitario"]) / 100;

                        }
                        Grade2.DataSource = _dtContResiduos;
                        Grade2.DataBind();
                        Grade2.HeaderRow.Cells[0].Text = "Codigo do residuo";
                        Grade2.HeaderRow.Cells[1].Text = "Descricao do residuo";
                        Grade2.HeaderRow.Cells[2].Text = "Caixa disponivel";
                        Grade2.HeaderRow.Cells[3].Text = "Tipo caixa";
                        Grade2.HeaderRow.Cells[4].Text = "Frequencia de coleta";
                        Grade2.HeaderRow.Cells[5].Text = "QtFranquia";
                        Grade2.HeaderRow.Cells[6].Text = "Periodicidade";
                        Grade2.HeaderRow.Cells[7].Text = "Valor/Excedente";
                        Grade2.HeaderRow.Cells[8].Text = "Valor unitario";
                        Grade2.HeaderRow.Cells[9].Text = "Unidade";
                        Grade2.HeaderRow.Cells[10].Text = "Franquia Volume";
                        Grade2.HeaderRow.Cells[11].Text = "Data Programada";
                        Grade2.HeaderRow.Cells[12].Text = "Ultima Coleta";
                        Grade2.HeaderRow.Cells[13].Text = "Observacao";

                        row = new TableRow();
                        row.Cells.Add(new TableCell());
                        row.Cells[0].Controls.Add(Grade2);
                        table.Rows.Add(row);

                        row = new TableRow();
                        row.Cells.Add(new TableCell());
                        lblEmBranco.ID = "lblEmBranco";
                        lblEmBranco.Text = "\n";
                        row.Cells[0].Controls.Add(lblEmBranco);
                        table.Rows.Add(row);
                    }
                }
            }

            int iColuna = 0;
            if (CLIENTESCONTROL1.Valor == "")
            {
                Grade.HeaderRow.Cells[iColuna++].Text = "Codigo";
                Grade.HeaderRow.Cells[iColuna++].Text = "Nome cliente";
            }
            Grade.HeaderRow.Cells[iColuna++].Text = "Numero lancamento";
            Grade.HeaderRow.Cells[iColuna++].Text = "Caminhao";
            Grade.HeaderRow.Cells[iColuna++].Text = "Numero MTR";
            Grade.HeaderRow.Cells[iColuna++].Text = "Numero container";
            Grade.HeaderRow.Cells[iColuna++].Text = "Data colocacao";
            Grade.HeaderRow.Cells[iColuna++].Text = "Data da coleta";
            Grade.HeaderRow.Cells[iColuna++].Text = "Grupo do residuo";
            Grade.HeaderRow.Cells[iColuna++].Text = "Descricao residuo";
            Grade.HeaderRow.Cells[iColuna++].Text = "Qtde coletada";
            Grade.HeaderRow.Cells[iColuna++].Text = "Qtde descarregada";
            Grade.HeaderRow.Cells[iColuna++].Text = "Unidade";
            Grade.HeaderRow.Cells[iColuna++].Text = "Motorista";
            Grade.HeaderRow.Cells[iColuna++].Text = "Valor unitario";
            Grade.HeaderRow.Cells[iColuna++].Text = "Valor total";
            Grade.HeaderRow.Cells[iColuna++].Text = "Destino final";
            Grade.HeaderRow.Cells[iColuna++].Text = "Observacao";
            Grade.HeaderRow.Cells[iColuna++].Text = "Numero MTR-e";

            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade);
            table.Rows.Add(row);

        }

        _dt = new DataTable();
        if (CLIENTESCONTROL1.Valor != "")
            _dt = oLancamentoDados.PegaDadosDeColetasPorPeriodo(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor));
        else
            _dt = oLancamentoDados.PegaDadosDeColetasPorPeriodo(Data1.Data, Data2.Data, 0);
        int _totalColetas = 0;
        foreach (DataRow _dr in _dt.Rows)
        {
            if (_dr["ehTerceiro"].ToString() == "1")
            {
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblColuna = new Label();
                lblColuna.Font.Bold = true;
                lblColuna.ID = "lblTotalColetas";
                lblColuna.Text = "Total coletas: " + _dr["NumeroCaixa"].ToString() + ": " + _dr["QtCaixas"].ToString();
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);
                _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
            }
        }
        row = new TableRow();
        row.Cells.Add(new TableCell());
        lblColuna = new Label();
        lblColuna.Font.Bold = true;
        lblColuna.ID = "lblTotalColetas2";
        lblColuna.Text = "Total coletas: terceiros: " + _totalColetas.ToString();
        row.Cells[0].Controls.Add(lblColuna);
        table.Rows.Add(row);

        // linha em branco
        _totalColetas = 0;
        foreach (DataRow _dr in _dt.Rows)
        {
            if (_dr["ehTerceiro"].ToString() == "0")
            {
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblColuna = new Label();
                lblColuna.Font.Bold = true;
                lblColuna.ID = "lblTotalColetas3";
                lblColuna.Text = "Total coletas: " + _dr["NumeroCaixa"].ToString() + ": " + _dr["QtCaixas"].ToString();
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);
            }
        }
        _totalColetas = 0;
        foreach (DataRow _dr in _dt.Rows)
        {
            _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
            _totalGeralColetas = _totalGeralColetas + Convert.ToInt32(_dr["QtCaixas"]);
        }
        row = new TableRow();
        row.Cells.Add(new TableCell());
        lblColuna = new Label();
        lblColuna.Font.Bold = true;
        lblColuna.ID = "lblTotalColetas4";
        lblColuna.Text = "Total coletas: Cliente: " + _totalColetas.ToString();
        row.Cells[0].Controls.Add(lblColuna);
        table.Rows.Add(row);

        row = new TableRow();
        row.Cells.Add(new TableCell());
        lblColuna = new Label();
        lblColuna.Font.Bold = true;
        lblColuna.ID = "lblTotalColetas5";
        lblColuna.Text = "Total geral coletas: " + _totalGeralColetas.ToString();
        row.Cells[0].Controls.Add(lblColuna);
        table.Rows.Add(row);

        table.RenderControl(hw);
        HttpContext.Current.Response.Write(hw.InnerWriter);
        HttpContext.Current.Response.End();

    }
 }