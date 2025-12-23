using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class RelatorioParaFaturamentoParaCliente : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    //Parametros.Relatorio oRel = new Parametros.Relatorio();
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

    DataTable _dt = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "36");
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
            txtCodigo.Text = "";
            if (oUsuario != null)
            {
                if (oUsuario.Nome.ToLower() == "itaguacu_estagio")
                {
                    txtCodigo.Text = "2624";
                    lblRazaoSocial.Text = "SHOPPING CENTER ITAGUAÇU";
                    txtCodigo.Enabled = false;
                    imagem.Enabled = false;
                }
                if (oUsuario.Nome.ToLower() == "floripa_estagio")
                {
                    txtCodigo.Text = "2306";
                    lblRazaoSocial.Text = "FLORIPA AIRPORT";
                    txtCodigo.Enabled = false;
                    imagem.Enabled = false;
                }
                if (oUsuario.Nome.ToLower() == "ceasa_estagio")
                {
                    txtCodigo.Text = "2708";
                    lblRazaoSocial.Text = "CEASA/SC";
                    txtCodigo.Enabled = false;
                    imagem.Enabled = false;
                }
            }
        }
    }

    private void Relatorio()
    {
        
        if (txtCodigo.Text == "")
            txtCodigo.Text = "0";

        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();

        oCliente.Codigo = Convert.ToInt32(txtCodigo.Text);
        oClienteDados.PegaDados(oCliente, Convert.ToInt32(txtCodigo.Text));

        AddRow(_table, _row, _cell, "Relatório para Faturamento - Cliente", 300, false, true);
        AddRow(_table, _row, _cell, "Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 400, false, true);
        AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToShortDateString(), 200, false, true);
        Panel1.Controls.Add(_table);

        _table = new Table();
        _row = new TableRow();
        _cell = new TableCell();
        _cell.BorderWidth = 0;
        if (oCliente.Codigo > 0)
        {
            lblRazaoSocial.Text = oCliente.NomeFantasia;

            AddRow(_table, _row, _cell, "Cliente: " + "<br />", 53, false, true);
            AddRow(_table, _row, _cell, oCliente.NomeFantasia + "<br />" + oCliente.Nome, 900, false, true);
            AddRow(_table, _row, _cell, "Código: ", 53, false, true);
            AddRow(_table, _row, _cell, oCliente.Codigo.ToString("000000"), 47, false, true);
            Panel1.Controls.Add(_table);
        }
        _dt = oLancamentoDados.PreencheDadosParaFaturamento(Data1.Data, Data2.Data, Convert.ToInt32(txtCodigo.Text), "GrupoResiduo, Residuo, DataRetirada", true);
        if (_dt.Rows.Count > 0)
        {
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();

            int[] iColWidth = new int[11];
            iColWidth[1] = 86;   //NºLançamento
            iColWidth[2] = 74;   //Caminhão 
            iColWidth[3] = 56;   //MTR 
            iColWidth[4] = 50;   //NºCaixa
            iColWidth[5] = 74;   //DataColocacao
            iColWidth[6] = 74;   //DataRetirada
            iColWidth[7] = 400;  //GrupoResíduo-Resíduo
            iColWidth[8] = 72;   //Qt.Descarga
            iColWidth[9] = 30;   //Und
            iColWidth[10] = 80; //Nº MTRe

            int tWidthContratos = 0;
            foreach (int iTW in iColWidth)
                tWidthContratos = tWidthContratos + iTW;
            _table.Width = tWidthContratos + 10;
            AddRow(_table, _row, _cell, "NºLançamento", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "Caminhão", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "MTR Nº", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "NºCaixa", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "Dt.Colocação", iColWidth[5], false, true, true);
            AddRow(_table, _row, _cell, "Dt.Retirada", iColWidth[6], false, true, true);
            AddRow(_table, _row, _cell, "GrupoResíduo-Resíduo", iColWidth[7], false, true);
            AddRow(_table, _row, _cell, "Qt.Descarga", iColWidth[8], false, true);
            AddRow(_table, _row, _cell, "Und", iColWidth[9], false, true);
            AddRow(_table, _row, _cell, "Nº MTRe", iColWidth[10], false, true);

            AddRow(_table, _row, _cell, "───────────", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "───────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "──────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[5], false, true, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[6], false, true, true);
            AddRow(_table, _row, _cell, "────────────────────────────────────────────────────", iColWidth[7], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[8], false, true);
            AddRow(_table, _row, _cell, "───", iColWidth[9], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[10], false, true);

            decimal _QtSbTlDescarga = 0;

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
                    AddRow(_table, _row, _cell, "Subtotais", iColWidth[7], true);
                    AddRow(_table, _row, _cell, _QtSbTlDescarga.ToString("N2") + "", iColWidth[8], true);
                    AddRow(_table, _row, _cell, "", iColWidth[9], false);
                    AddRow(_table, _row, _cell, "", iColWidth[10], false);

                    _QtSbTlDescarga = 0;
                    AddRow(_table, _row, _cell, "", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[5], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[8], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[9], false, true);
                    AddRow(_table, _row, _cell, "", iColWidth[10], false, true);

                }
                _grupoEresiduoAnterior = dr["GrupoResiduo"].ToString() + "-" + dr["Residuo"].ToString();

                AddRow(_table, _row, _cell, dr["NumeroLancamento"].ToString(), iColWidth[1], true);
                AddRow(_table, _row, _cell, dr["Caminhao"].ToString(), iColWidth[2], false);
                AddRow(_table, _row, _cell, dr["NumeroMTR"].ToString(), iColWidth[3], true);
                AddRow(_table, _row, _cell, dr["NumeroCaixa"].ToString(), iColWidth[4], false);

                if (dr["DataColocacao"].ToString() != "")
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataColocacao"]).ToString("dd/MM/yy"), iColWidth[5], false, false, true);
                else
                    AddRow(_table, _row, _cell, "", iColWidth[5], false);

                if (dr["DataRetirada"].ToString() != "")
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataRetirada"]).ToString("dd/MM/yy"), iColWidth[6], false, false, true);
                else
                    AddRow(_table, _row, _cell, "", iColWidth[6], false);

                AddRow(_table, _row, _cell, geral.Left(dr["GrupoResiduo"].ToString(), 20) + "-" + geral.Left(dr["Residuo"].ToString(), 20), iColWidth[7], false);

                if (dr["QtDescarga"].ToString() != "")
                {
                    AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[8], true);
                    _QtSbTlDescarga = _QtSbTlDescarga + Convert.ToDecimal(dr["QtDescarga"]);
                }
                else
                    AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[8], true);

                AddRow(_table, _row, _cell, dr["Und"].ToString(), iColWidth[9], false);
                AddRow(_table, _row, _cell, dr["MTRe"].ToString(), iColWidth[10], false);
            }
            // adicionar linha com subtotais
            AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "", iColWidth[4], false);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], false);
            AddRow(_table, _row, _cell, "Subtotais", iColWidth[7], true);
            AddRow(_table, _row, _cell, _QtSbTlDescarga.ToString("N2") + "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], false);
            AddRow(_table, _row, _cell, "", iColWidth[10], false);

            _dt = new DataTable();
            _dt = oLancamentoDados.PegaDadosDeColetasPorPeriodo(Data1.Data, Data2.Data, Convert.ToInt32(txtCodigo.Text));

            int _totalColetas = 0;
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["ehTerceiro"].ToString() == "1")
                {
                    //NumeroCaixa, count(NumeroCaixa) as QtCaixas, EhTerceiro
                    AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                    AddRow(_table, _row, _cell, _dr["NumeroCaixa"].ToString(), iColWidth[2], false);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false);
                    AddRow(_table, _row, _cell, _dr["QtCaixas"].ToString(), iColWidth[4], true);
                    AddRow(_table, _row, _cell, "", iColWidth[5], false);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], true);
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                    AddRow(_table, _row, _cell, "", iColWidth[9], false);
                    AddRow(_table, _row, _cell, "", iColWidth[10], false);

                    _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
                }
            }
            AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
            AddRow(_table, _row, _cell, "Terceiros", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, _totalColetas.ToString(), iColWidth[4], true);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], false);
            AddRow(_table, _row, _cell, "", iColWidth[10], false);

            // linha em branco
            AddRow(_table, _row, _cell, "", iColWidth[1], true);
            AddRow(_table, _row, _cell, "", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, "", iColWidth[4], true);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], false);
            AddRow(_table, _row, _cell, "", iColWidth[10], false);

            _totalColetas = 0;
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["ehTerceiro"].ToString() == "0")
                {
                    AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
                    AddRow(_table, _row, _cell, _dr["NumeroCaixa"].ToString(), iColWidth[2], false);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false);
                    AddRow(_table, _row, _cell, _dr["QtCaixas"].ToString(), iColWidth[4], true);
                    AddRow(_table, _row, _cell, "", iColWidth[5], false);
                    AddRow(_table, _row, _cell, "", iColWidth[6], true);
                    AddRow(_table, _row, _cell, "", iColWidth[7], true);
                    AddRow(_table, _row, _cell, "", iColWidth[8], true);
                    AddRow(_table, _row, _cell, "", iColWidth[9], false);
                    AddRow(_table, _row, _cell, "", iColWidth[10], false);
                }
            }
            _totalColetas = 0;
            foreach (DataRow _dr in _dt.Rows)
            {
                _totalColetas = _totalColetas + Convert.ToInt32(_dr["QtCaixas"]);
            }
            AddRow(_table, _row, _cell, "Total coletas", iColWidth[1], true);
            AddRow(_table, _row, _cell, "Cliente", iColWidth[2], false);
            AddRow(_table, _row, _cell, "", iColWidth[3], false);
            AddRow(_table, _row, _cell, _totalColetas.ToString(), iColWidth[4], true);
            AddRow(_table, _row, _cell, "", iColWidth[5], false);
            AddRow(_table, _row, _cell, "", iColWidth[6], true);
            AddRow(_table, _row, _cell, "", iColWidth[7], true);
            AddRow(_table, _row, _cell, "", iColWidth[8], true);
            AddRow(_table, _row, _cell, "", iColWidth[9], false);
            AddRow(_table, _row, _cell, "", iColWidth[10], false);
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
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Table _table = new Table();
        TableRow _row = new TableRow();
        TableCell _cell = new TableCell();
        oCliente.Codigo = 0;
        if (txtCodigo.Text != "")
            oCliente.Codigo = Convert.ToInt32(txtCodigo.Text);
        if (oCliente.Codigo == 0)
        {
            AddRow(_table, _row, _cell, "Código do cliente inválido!", 300, false, false);
            Panel1.Controls.Add(_table);
        }
        else
            Relatorio();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtCodigo.Text = "";
        Session["Clientes"] = null;
        string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
        ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    }
    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        Label lblColuna = new Label();
        if (txtCodigo.Text == "")
        {
            Panel1.Controls.Clear();
            lblColuna.Font.Bold = true;
            lblColuna.Text = "Código do cliente inválido!";
            Panel1.Controls.Add(lblColuna);
            return;
        }
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

        Panel1.Controls.Add(Grade);
        string NomeArq = "Relatorio_FaturamentoCliente.xls";
        Grade.EnableViewState = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
        Grade.EnableViewState = false;

        _dt = oLancamentoDados.PreencheDadosParaFaturamento(Data1.Data, Data2.Data, Convert.ToInt32(txtCodigo.Text), "GrupoResiduo, Residuo, DataRetirada, DataColocacao", true);
        _dt.Columns.RemoveAt(8);
        _dt.Columns.RemoveAt(10);
        _dt.Columns.RemoveAt(10);
        _dt.Columns.RemoveAt(10);
        _dt.Columns.RemoveAt(10);
        _dt.Columns.RemoveAt(10);
        _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
        _dt.Columns.Add("Subtotal");

        if (_dt.Rows.Count > 0 && txtCodigo.Text != "")
        {
            decimal _QtSbTlDescarga = 0;
            string _grupoEresiduoAnterior = "";
            if (_dt.Rows.Count > 0)
            {
                _grupoEresiduoAnterior = _dt.Rows[1]["GrupoResiduo"].ToString() + "-" + _dt.Rows[1]["Residuo"].ToString();
                if (_dt.Rows[0]["QtDescarga"] != null)
                    _QtSbTlDescarga = Convert.ToDecimal(_dt.Rows[0][8]);
            }
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr2 = _dt.Rows[i];
                if (_grupoEresiduoAnterior != dr2["GrupoResiduo"].ToString() + "-" + dr2["Residuo"].ToString() && _grupoEresiduoAnterior != "")
                {
                    if (i == 0)
                        dr2[_dt.Columns.Count - 1] = _QtSbTlDescarga;
                    else
                        _dt.Rows[i - 1][_dt.Columns.Count - 1] = _QtSbTlDescarga;
                    _QtSbTlDescarga = 0;
                }
                if (_dt.Rows[0]["QtDescarga"] != null) 
                    _QtSbTlDescarga = _QtSbTlDescarga + Convert.ToDecimal(dr2[8]);
                _grupoEresiduoAnterior = dr2["GrupoResiduo"].ToString() + "-" + dr2["Residuo"].ToString();
            }
            _dt.Rows[_dt.Rows.Count - 1][_dt.Columns.Count - 1] = _QtSbTlDescarga;
            Grade.DataSource = _dt;
            Grade.DataBind();

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
            lblTitulo.Text = "Relatorio para faturamento para Cliente  -   Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

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


            oCliente.Codigo = Convert.ToInt32(txtCodigo.Text);
            oClienteDados.PegaDados(oCliente, Convert.ToInt32(txtCodigo.Text));

            oContrato = new clsContratos();
            oContrato = oContratoDados.PegaDados(oContrato, 0, Convert.ToInt16(txtCodigo.Text));

            DataRow dr = dtTitulo.NewRow();
            dr[0] = txtCodigo.Text;
            dr[1] = geral.RemoverAcentos(oCliente.NomeFantasia);
            dr[2] = geral.RemoverAcentos(oCliente.Nome);
            dtTitulo.Rows.Add(dr);


            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmBranco.ID = "lblEmBranco";
            lblEmBranco.Text = "\n";
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);            

            Grade.HeaderRow.Cells[0].Text = "No.lancamento";
            Grade.HeaderRow.Cells[1].Text = "Caminhao";
            Grade.HeaderRow.Cells[2].Text = "No.MTR";
            Grade.HeaderRow.Cells[3].Text = "No.container";
            Grade.HeaderRow.Cells[4].Text = "Data colocacao";
            Grade.HeaderRow.Cells[5].Text = "Data da coleta";
            Grade.HeaderRow.Cells[6].Text = "Grupo do residuo";
            Grade.HeaderRow.Cells[7].Text = "Descricao residuo";
            Grade.HeaderRow.Cells[8].Text = "Qtde descarregada";
            Grade.HeaderRow.Cells[9].Text = "Unidade";
            Grade.HeaderRow.Cells[10].Text = "No.MTR-e";

            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade);
            table.Rows.Add(row);

        }
        table.RenderControl(hw);
        HttpContext.Current.Response.Write(hw.InnerWriter);
        HttpContext.Current.Response.End();
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        if (Session["Clientes"] != null)
        {
            oCliente = (clsClientes)Session["Clientes"];
            txtCodigo.Text = oCliente.Codigo.ToString();
            lblRazaoSocial.Text = oCliente.Nome;
        }
    }
}