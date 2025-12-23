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

namespace SILC.Web.Relatorios
{
    
    public partial class RelatorioReciclaveis : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        Parametros.Relatorio oRel = new Parametros.Relatorio();
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
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "40");
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
                CLIENTESCONTROL1.Valor = "";
                CLIENTESCONTROL1.Texto = "";
            }
        }
    
        private void Relatorio()
        {
            
            if (CLIENTESCONTROL1.Valor == "")
                CLIENTESCONTROL1.Valor = "0";
    
            oCliente.Codigo = Convert.ToInt32(CLIENTESCONTROL1.Valor);
            oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));
    
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            AddRow(_table, _row, _cell, "Relatório de Recicláveis - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 500, false, true);
            Panel1.Controls.Add(_table);
    
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            AddRow(_table, _row, _cell, "Cliente: ", 53, false, true);
            AddRow(_table, _row, _cell, oCliente.NomeFantasia, 400, false, true);
            Panel1.Controls.Add(_table);
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            AddRow(_table, _row, _cell, "", 53, false, true);
            AddRow(_table, _row, _cell, oCliente.Nome, 400, false, false);
            Panel1.Controls.Add(_table);
    
            if (oCliente.Codigo > 0)
            {
                oContrato = oContratoDados.PegaDados(oContrato, 0, oCliente.Codigo);
                if (oContrato.Codigo > 0)
                {
                    _table = new Table();
                    _row = new TableRow();
                    _cell = new TableCell();
                    _cell.BorderWidth = 0;
                    AddRow(_table, _row, _cell, "Código: ", 53, false, false);
                    AddRow(_table, _row, _cell, oCliente.Codigo.ToString("000000"), 47, false, false);
                    AddRow(_table, _row, _cell, "Nº Contrato: " + oContrato.NumeroContrato.ToString("000000"), 130, false, false);
                    AddRow(_table, _row, _cell, "Valor R$ " + oContrato.ValorContrato.ToString("N2"), 130, false, false);
                    AddRow(_table, _row, _cell, "Dt.Reaj: " + oContrato.DataReajuste, 130, false, false);
                    AddRow(_table, _row, _cell, "Dt.Início: " + oContrato.DataInicio, 130, false, false);
                    AddRow(_table, _row, _cell, "Indice: " + oContrato.IndiceReajuste, 400, false, false);
                    Panel1.Controls.Add(_table);
    
                    _dt = oLancamentoDados.PreencheDadosRelatorioReciclaveis(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), "GrupoResiduo, Residuo, DataRetirada");
                    if (_dt.Rows.Count > 0)
                    {
                        _table = new Table();
                        _row = new TableRow();
                        _cell = new TableCell();
    
                        int[] iColWidth = new int[10];
                        iColWidth[1] = 80;   //Nº MTR
                        iColWidth[2] = 50;   //NºCaixa
                        iColWidth[3] = 70;   //Data Coleta
                        iColWidth[4] = 400;  //GrupoResíduo-Resíduo
                        iColWidth[5] = 88;   //Qt.Coletada
                        iColWidth[6] = 30;   //Und
                        iColWidth[7] = 80;   //Unitário
                        iColWidth[8] = 80;   //Total
                        iColWidth[9] = 150;  //Observação
    
                        int tWidthContratos = 0;
                        foreach (int iTW in iColWidth)
                            tWidthContratos = tWidthContratos + iTW;
                        _table.Width = tWidthContratos + 10;
                        AddRow(_table, _row, _cell, "Nº MTR", iColWidth[1], false, true);
                        AddRow(_table, _row, _cell, "NºCaixa", iColWidth[2], false, true);
                        AddRow(_table, _row, _cell, "Dt.Coleta", iColWidth[3], false, true);
                        AddRow(_table, _row, _cell, "Grupo/Subgrupo (Resíduos)", iColWidth[4], false, true);
                        AddRow(_table, _row, _cell, "Qt.Coletada", iColWidth[5], true, true);
                        AddRow(_table, _row, _cell, "Und", iColWidth[6], false, true);
                        AddRow(_table, _row, _cell, "Unitário", iColWidth[7], false, true);
                        AddRow(_table, _row, _cell, "Total", iColWidth[8], false, true);
                        AddRow(_table, _row, _cell, "Observação", iColWidth[9], false, true);
    
                        AddRow(_table, _row, _cell, "──────────", iColWidth[1], false, true);
                        AddRow(_table, _row, _cell, "──────────", iColWidth[2], false, true);
                        AddRow(_table, _row, _cell, "────────", iColWidth[3], false, true);
                        AddRow(_table, _row, _cell, "────────────────────────────────────────────────────────", iColWidth[4], false, true);
                        AddRow(_table, _row, _cell, "────────", iColWidth[5], true, true);
                        AddRow(_table, _row, _cell, "───", iColWidth[6], false, true);
                        AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, true);
                        AddRow(_table, _row, _cell, "──────────", iColWidth[8], false, true);
                        AddRow(_table, _row, _cell, "───────────────────────", iColWidth[9], false, true);
    
                        decimal _QtSbTlColeta = 0;
                        decimal _TotalQtColetas = 0;
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
                                AddRow(_table, _row, _cell, "Subtotais", iColWidth[4], true);
                                AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[5], true);
                                AddRow(_table, _row, _cell, "", iColWidth[6], false);
                                AddRow(_table, _row, _cell, "", iColWidth[7], false);
                                AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[8], true);
                                AddRow(_table, _row, _cell, "", iColWidth[9], false);
    
                                _QtSbTlColeta = 0;
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
    
                            }
                            _grupoEresiduoAnterior = dr["GrupoResiduo"].ToString() + "-" + dr["Residuo"].ToString();
    
                            AddRow(_table, _row, _cell, dr["NumeroMTR"].ToString(), iColWidth[1], true);
                            AddRow(_table, _row, _cell, dr["NumeroCaixa"].ToString(), iColWidth[2], false);
                            if (dr["DataRetirada"].ToString() != "")
                                AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataRetirada"]).ToString("dd/MM/yy"), iColWidth[3], false);
                            else
                                AddRow(_table, _row, _cell, "", iColWidth[3], false);
    
                            AddRow(_table, _row, _cell, geral.Left(dr["GrupoResiduo"].ToString(), 20) + "-" + geral.Left(dr["Residuo"].ToString(), 20), iColWidth[4], false);
    
                            if (dr["QtColetada"].ToString() != "")
                            {
                                AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtColetada"]).ToString("N2"), iColWidth[5], true);
                                _QtSbTlColeta = _QtSbTlColeta + Convert.ToDecimal(dr["QtColetada"]);
                                _TotalQtColetas = _TotalQtColetas + Convert.ToDecimal(dr["QtColetada"]); 
                            }
                            else
                                AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[5], true);
    
                            AddRow(_table, _row, _cell, dr["Und"].ToString(), iColWidth[6], false);
    
                            if (dr["Unitario"].ToString() != "")
                            {
                                AddRow(_table, _row, _cell, Convert.ToDecimal(dr["Unitario"]).ToString("N2"), iColWidth[7], true);                            
                            }
                            else
                                AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[7], true);
    
                            if (dr["Total"].ToString() != "")
                            {
                                AddRow(_table, _row, _cell, Convert.ToDecimal(dr["Total"]).ToString("N2"), iColWidth[8], true);
                                _vlSbTl = _vlSbTl + Convert.ToDecimal(dr["Total"]);
                                _valorTotalGeral = _valorTotalGeral + Convert.ToDecimal(dr["Total"]);
                            }
                            else
                                AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[8], true);
    
                            AddRow(_table, _row, _cell, dr["Obs"].ToString(), iColWidth[9], false);
                        }
                        // adicionar linha com subtotais
                        AddRow(_table, _row, _cell, "", iColWidth[1], true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        AddRow(_table, _row, _cell, "Subtotais", iColWidth[4], true);
                        AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[5], true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        AddRow(_table, _row, _cell, "", iColWidth[7], false);
                        AddRow(_table, _row, _cell, _vlSbTl.ToString("N2") + "", iColWidth[8], true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], false);
    
                        // adicionar linha com total geral
                        AddRow(_table, _row, _cell, "", iColWidth[1], true);
                        AddRow(_table, _row, _cell, "", iColWidth[2], false);
                        AddRow(_table, _row, _cell, "", iColWidth[3], false);
                        AddRow(_table, _row, _cell, "Total geral", iColWidth[4], true);
                        AddRow(_table, _row, _cell, _TotalQtColetas.ToString("N2") + "", iColWidth[5], true);
                        AddRow(_table, _row, _cell, "", iColWidth[6], false);
                        AddRow(_table, _row, _cell, "", iColWidth[7], false);
                        AddRow(_table, _row, _cell, _valorTotalGeral.ToString("N2") + "", iColWidth[8], true);
                        AddRow(_table, _row, _cell, "", iColWidth[9], false);
    
                        Panel1.Controls.Add(_table);
                    }
    
                } 
            }
        }
    
        private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false)
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
            
            cell.Controls.Add(_lbl);
            row.Cells.Add(cell);
    
            _table.BorderWidth = 0;
            _table.Rows.Add(row);
    
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Relatorio();
            //try
            //{
            //    Relatorio();
            //}
            //finally
            //{
            //    Session["ctrl"] = Panel1;
            //    ClientScript.RegisterStartupScript(this.GetType(), "onclick",
            //        "<script language=javascript>window.open('Imprimir.aspx','Imprimir','height=900px,width=1600px,scrollbars=1');</script>");
            //}
        }
    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
            Session["Clientes"] = null;
            string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
        }
        protected void imbExcel_Click(object sender, ImageClickEventArgs e)
        {
            Label lblColuna = new Label();
            if (CLIENTESCONTROL1.Valor == "")
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
            GridView Grade3 = new GridView();
    
            Panel1.Controls.Add(Grade);
            string NomeArq = "Relatorio_Reciclaveis.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
    
            _dt = oLancamentoDados.PreencheDadosRelatorioReciclaveis(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), "GrupoResiduo, Residuo, DataRetirada");
            _dt.Columns.RemoveAt(1);
            _dt.Columns.RemoveAt(1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 2);
    
            foreach (DataRow l_dr in _dt.Rows)
            {
                l_dr[4] = Convert.ToDecimal(l_dr[4]).ToString("N2");
                l_dr[5] = Convert.ToDecimal(l_dr[5]).ToString("N2");
                l_dr[8] = Convert.ToDecimal(l_dr[8]).ToString("N2");
                l_dr[9] = Convert.ToDecimal(l_dr[9]).ToString("N2");
            }
    
            if (_dt.Rows.Count > 0 && CLIENTESCONTROL1.Valor != "")
            {
                decimal _QtSbTlColetado = 0;
                decimal _QtTotalColetado = 0;
                decimal _ValorTotalGeral = 0;
                string _grupoEresiduoAnterior = "";
                if (_dt.Rows.Count > 0)
                {
                    _grupoEresiduoAnterior = _dt.Rows[1]["GrupoResiduo"].ToString() + "-" + _dt.Rows[1]["Residuo"].ToString();
                    if (_dt.Rows[0]["QtColetada"] != null)
                        _QtSbTlColetado = Convert.ToDecimal(_dt.Rows[0][4]);
                }
                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    DataRow dr2 = _dt.Rows[i];
                    dr2[5] = 0.00;
                    if (_grupoEresiduoAnterior != dr2["GrupoResiduo"].ToString() + "-" + dr2["Residuo"].ToString() && _grupoEresiduoAnterior != "")
                    {
                        if (i == 0)
                            dr2[5] = _QtSbTlColetado;
                        else
                            _dt.Rows[i - 1][5] = _QtSbTlColetado;
                        _QtSbTlColetado = 0;
                    }
                    if (_dt.Rows[i]["QtColetada"] != null)
                    {
                        _QtSbTlColetado = _QtSbTlColetado + Convert.ToDecimal(dr2[4]);
                        _QtTotalColetado = _QtTotalColetado + Convert.ToDecimal(dr2[4]);
                    }
                    if (_dt.Rows[i][9] != null)
                        _ValorTotalGeral = _ValorTotalGeral + Convert.ToDecimal(_dt.Rows[i][9]);
                    _grupoEresiduoAnterior = dr2["GrupoResiduo"].ToString() + "-" + dr2["Residuo"].ToString();
                }
                _dt.Rows[_dt.Rows.Count - 1][5] = _QtSbTlColetado.ToString("N2");
                DataRow dr3 = _dt.NewRow();
                dr3[3] = "TOTAL GERAL";
                dr3[4] = _QtTotalColetado.ToString("N2");
                dr3[5] = _QtTotalColetado.ToString("N2");
                dr3[9] = _ValorTotalGeral.ToString("N2");
                _dt.Rows.Add(dr3);
               
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
                lblTitulo.Text = "Relatorio de Reciclaveis -   Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
                row.Cells[0].Controls.Add(lblTitulo);
                table.Rows.Add(row);
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
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
    
                oCliente.Codigo = Convert.ToInt32(CLIENTESCONTROL1.Valor);
                oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));
    
                oContrato = new clsContratos();
                oContrato = oContratoDados.PegaDados(oContrato, 0, Convert.ToInt16(CLIENTESCONTROL1.Valor));
    
                DataRow dr = dtTitulo.NewRow();
                dr[0] = CLIENTESCONTROL1.Valor;
                dr[1] = geral.RemoverAcentos(oCliente.NomeFantasia);
                dr[2] = geral.RemoverAcentos(oCliente.Nome);
                dr[3] = oContrato.NumeroContrato;
                dr[4] = oContrato.ValorContrato.ToString("N2");
                dr[5] = oContrato.DataReajuste;
                dr[6] = oContrato.DataInicio;
                dr[7] = oContrato.IndiceReajuste;
                dtTitulo.Rows.Add(dr);
    
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
    
                Grade.HeaderRow.Cells[0].Text = "Numero MTR";
                Grade.HeaderRow.Cells[1].Text = "Numero container";
                Grade.HeaderRow.Cells[2].Text = "Data da coleta";
                Grade.HeaderRow.Cells[3].Text = "Descricao residuo";
                Grade.HeaderRow.Cells[4].Text = "Qtde coletada";
                Grade.HeaderRow.Cells[5].Text = "Subtotal coletado";
                Grade.HeaderRow.Cells[6].Text = "Unidade";
                Grade.HeaderRow.Cells[7].Text = "Grupo do residuo";
                Grade.HeaderRow.Cells[8].Text = "Valor unitario";
                Grade.HeaderRow.Cells[9].Text = "Valor total";
                Grade.HeaderRow.Cells[10].Text = "Observacao";
    
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