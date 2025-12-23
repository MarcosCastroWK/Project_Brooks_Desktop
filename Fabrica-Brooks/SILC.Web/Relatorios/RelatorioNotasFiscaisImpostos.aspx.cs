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
    
    
    public partial class RelatorioNotasFiscaisImpostos : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "43");
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
            }
        }
    
        private void Relatorio(string pOrdem, bool pSoFatura, bool pSoISSPF)
        {
            
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
    
            lblTituloRelatorio.Text = "Relatório de Notas Fiscais e Impostos - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + "&nbsp;Emissão: " + DateTime.Now.ToString("dd/MM/yy");
    
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            clsNotasFiscaisDados oNFDados = new clsNotasFiscaisDados();
            _dt = oNFDados.RetornaNotasFiscais(Data1.Data, Data2.Data, pOrdem, pSoFatura, pSoISSPF);
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();          
                _row = new TableRow();
                _cell = new TableCell();
    
                int[] iColWidth = new int[14];
                iColWidth[1] =   66;  //NF/Fat
                iColWidth[2] =   66;  //Emissão
                iColWidth[3] =   70;  //Sit.Trib
                iColWidth[4] =   70;  //Sit.Nota
                iColWidth[5] =  352;  //Tomador
                iColWidth[6] =   96;  //Vl.Bruto NF/Fat
                iColWidth[7] =   80;  //PIS
                iColWidth[8] =   80;  //Cofins
                iColWidth[9] =   80;  //INSS
                iColWidth[10] =  80;  //IR
                iColWidth[11] =  80;  //C.Social
                iColWidth[12] =  80;  //ISS RF
                iColWidth[13] =  80;  //ISS PF
    
                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;
    
                Panel1.Controls.Add(_table);
    
                decimal vTotalFaturasEmitidas = 0;
                decimal vTotalNFsIPNEmitidas = 0;
                decimal vTotalCanceladas = 0;
                decimal vTotalPIS = 0;
                decimal vTotalCofins = 0;
                decimal vTotalINSS = 0;
                decimal vTotalIR = 0;
                decimal vTotalContrSocial = 0;
                decimal vTotalISSRF = 0;
                decimal vTotalISSPF = 0;
                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    DataRow dr = _dt.Rows[i];
                    string sTipoDoc = "";
                    if (dr["TipoDocumento"].ToString() == "5")
                    {
                        sTipoDoc = "Fatura";
                    }
                    else
                    {
                        if (dr["CNPJ_CPF"].ToString().Length > 15)
                            sTipoDoc = "Retenção";
                        else if (dr["CNPJ_CPF"].ToString().Length > 1)
                            sTipoDoc = "Normal";
                        else
                            sTipoDoc = "--";
                    }
                    decimal vISSPF = 0;
                    if (dr["TipoDocumento"].ToString() == "8" && dr["Cancelada"].ToString() == "0" && dr["CNPJ_CPF"].ToString().Length <= 15)
                    {
                        vISSPF = Convert.ToDecimal(dr["ValorTotal"]) * 5 / 100;
                        vTotalISSPF = vTotalISSPF + vISSPF;
                    }
                    AddDados(_table, _row, _cell, iColWidth, dr["NumeroNF"].ToString(), Convert.ToDateTime(dr["DataEmissao"]).ToString("dd/MM/yy"),
                             sTipoDoc, dr["Cancelada"].ToString(), dr["Cliente"].ToString(), Convert.ToDecimal(dr["ValorTotal"]).ToString("N2"),
                             Convert.ToDecimal(dr["ValorPIS"]).ToString("N2"), Convert.ToDecimal(dr["ValorCofins"]).ToString("N2"),
                             Convert.ToDecimal(dr["ValorINSS"]).ToString("N2"), Convert.ToDecimal(dr["ValorIR"]).ToString("N2"), Convert.ToDecimal(dr["ValorContrSocial"]).ToString("N2"),
                             Convert.ToDecimal(dr["ValorISSRF"]).ToString("N2"), vISSPF.ToString("N2"));
                    vTotalPIS = vTotalPIS + Convert.ToDecimal(dr["ValorPIS"]);
                    vTotalCofins = vTotalCofins + Convert.ToDecimal(dr["ValorCofins"]);
                    if ((dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == ""))
                    {
                        vTotalINSS = vTotalINSS + Convert.ToDecimal(dr["ValorINSS"]);
                        vTotalIR = vTotalIR + Convert.ToDecimal(dr["ValorIR"]);
                    }
                    vTotalContrSocial = vTotalContrSocial + Convert.ToDecimal(dr["ValorContrSocial"]);
                    if ((dr["TipoDocumento"].ToString() == "5" || dr["TipoDocumento"].ToString() == "8") && (dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == "") && dr["CNPJ_CPF"].ToString().Length > 15)
                        vTotalISSRF = vTotalISSRF + Convert.ToDecimal(dr["ValorISSRF"]);
                    if (dr["TipoDocumento"].ToString() == "5")
                    {
                        if ((dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == ""))
                            vTotalFaturasEmitidas = vTotalFaturasEmitidas + Convert.ToDecimal(dr["ValorTotal"]);
                    }
                    else if (dr["TipoDocumento"].ToString() == "8" && (dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == "")) 
                        vTotalNFsIPNEmitidas = vTotalNFsIPNEmitidas + Convert.ToDecimal(dr["ValorTotal"]);
                    if (dr["Cancelada"].ToString() == "1")
                        vTotalCanceladas = vTotalCanceladas + Convert.ToDecimal(dr["ValorTotal"]);
                }
                AddRow(_table, _row, _cell, "────────", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "───────", iColWidth[2], false, true, true);
                AddRow(_table, _row, _cell, "────────", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "────────", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "──────────────────────────────────────────────", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "────────────", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[7], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[8], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[9], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[10], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[11], false, true);
                AddRow(_table, _row, _cell, "──────────", iColWidth[12], false, false);
                AddRow(_table, _row, _cell, "──────────", iColWidth[13], false, false);
    
                // total de faturas emitidas no período
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL FATURAS EMITIDAS", vTotalFaturasEmitidas.ToString("N2"), "", "", "", "", "", "", "");
                // total de notas fiscais emitidas no período
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL NFS IPM EMITIDAS", vTotalNFsIPNEmitidas.ToString("N2"), "", "", "", "", "", "", "");
                // total faturas + notas fiscais IPM
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL FATURAS + NFS IPM", (vTotalFaturasEmitidas + vTotalNFsIPNEmitidas).ToString("N2"), "", "", "", "", "", "", "");
                // total canceladas
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL CANCELADAS", vTotalCanceladas.ToString("N2"), "", "", "", "", "", "", "");
                // total retenções
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL RETENÇÕES", "", vTotalPIS.ToString("N2"), vTotalCofins.ToString("N2"), vTotalINSS.ToString("N2"), vTotalIR.ToString("N2"), vTotalContrSocial.ToString("N2"), vTotalISSRF.ToString("N2"), "");
                // total ISS PF
                AddDados(_table, _row, _cell, iColWidth, "TOTAL", "", "", "", "TOTAL ISS PF", "", "", "", "", "", "", "", vTotalISSPF.ToString("N2"));
                Panel1.Controls.Add(_table);
            }
        }
        private void AddDados(Table _table, TableRow _row, TableCell _cell, int[] iColWidth, string pNumeroNF, string pDataEmissao, 
                              string pRetencaoNormal, string pCanceladaNormal, string pNomeCliente, string pValorTotal, string pValorPIS, 
                              string pValorCofins, string pValorINSS, string pValorIR, string pValorContrSocial,
                              string pValorISSRF, string pValorISSPF)
        {
            bool bNegrito = false;
            if (pNumeroNF.IndexOf("TOTAL") > -1)
            {
                pNumeroNF = "";
                bNegrito = true;
            }
            string sColor = "";
            if (pCanceladaNormal == "1")
                sColor = "Yellow";
            AddRow(_table, _row, _cell, pNumeroNF, iColWidth[1], true, bNegrito, false, sColor);
            AddRow(_table, _row, _cell, pDataEmissao, iColWidth[2], false, bNegrito, true, sColor);
            AddRow(_table, _row, _cell, pRetencaoNormal, iColWidth[3], false, bNegrito, false, sColor);
            if (pCanceladaNormal == "1")
                AddRow(_table, _row, _cell, "Cancelada", iColWidth[4], false, bNegrito, false, sColor);
            else if (pCanceladaNormal == "0")
                AddRow(_table, _row, _cell, "Normal", iColWidth[4], false, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, "", iColWidth[4], false, bNegrito, false, sColor);
            AddRow(_table, _row, _cell, pNomeCliente, iColWidth[5], false, bNegrito, false, sColor);
            if (pValorTotal == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[6], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorTotal, iColWidth[6], true, bNegrito, false, sColor);
            if (pValorPIS == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[7], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorPIS, iColWidth[7], true, bNegrito, false, sColor);
            if (pValorCofins == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[8], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorCofins, iColWidth[8], true, bNegrito, false, sColor);
            if (pValorINSS == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[9], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorINSS, iColWidth[9], true, bNegrito, false, sColor);
            if (pValorIR == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[10], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorIR, iColWidth[10], true, bNegrito, false, sColor);
            if (pValorContrSocial == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[11], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorContrSocial, iColWidth[11], true, bNegrito, false, sColor);
            if (pValorISSRF == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[12], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorISSRF, iColWidth[12], true, bNegrito, false, sColor);
            if (pValorISSPF == "0,00")
                AddRow(_table, _row, _cell, "", iColWidth[13], true, bNegrito, false, sColor);
            else
                AddRow(_table, _row, _cell, pValorISSPF, iColWidth[13], true, bNegrito, false, sColor);
        }
        private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, bool pAlinCentral = false, string pColor = "")
        {
            Label _lbl = new Label();
            _lbl.Text = pText + "&nbsp;";
            _lbl.Width = pWidth;
            _lbl.Font.Name = "Tahoma";
            _lbl.Font.Size = 8;
            _lbl.Attributes.CssStyle.Add("margin-top", "0");
            _lbl.Attributes.CssStyle.Add("margin-bottom", "0");
            if (pColor != "")
                _lbl.BackColor = System.Drawing.ColorTranslator.FromHtml(pColor);
            if (pNegrito)
            {
                _lbl.Font.Bold = true;
                //cell.Attributes.CssStyle.Add("border", "1px solid black");
            }
            if (pAlinDireita)
                _lbl.Attributes.CssStyle.Add("text-align", "right");
    
            if (pAlinCentral)
                _lbl.Attributes.CssStyle.Add("text-align", "center");
    
            cell.Attributes.CssStyle.Add("style", "height: 9px");
            cell.Height = 9;
    
            cell.Controls.Add(_lbl);
            row.Cells.Add(cell);
    
            _table.BorderWidth = 0;
    
            _table.Rows.Add(row);
    
        }
    
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Relatorio("Convert(NumeroNotaFiscal, unsigned), DataEmissao asc", false, false);
        }
    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
        }
    
        protected void Check_Clicked(object sender, EventArgs e)
        {    
            for (int i = 0; i < cklAcao.Items.Count; i++)
            {
                if (cklAcao.Items[i].Selected)
                {
                    // 0 - ordem inversa
                    if (i == 0)
                    {
                        Relatorio("DataEmissao desc", false, false);
                        break;
                    }
                    // 1 - só fatura
                    else if (i == 1)
                    {
                        Relatorio("DataEmissao", true, false);
                        break;
                    }
                    // 2 - só ISS PF
                    if (i == 2)
                    {
                        Relatorio("DataEmissao", false, true);
                        break;
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
    
            string NomeArq = "Relatorio_NotasFiscais.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
    
            clsNotasFiscaisDados oNFDados = new clsNotasFiscaisDados();
            _dt = oNFDados.RetornaNotasFiscais(Data1.Data, Data2.Data, "DataEmissao asc", false, false);
    
            decimal vTotalFaturasEmitidas = 0;
            decimal vTotalNFsIPNEmitidas = 0;
            decimal vTotalCanceladas = 0;
            decimal vTotalPIS = 0;
            decimal vTotalCofins = 0;
            decimal vTotalINSS = 0;
            decimal vTotalIR = 0;
            decimal vTotalContrSocial = 0;
            decimal vTotalISSRF = 0;
            decimal vTotalISSPF = 0;
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                decimal vISSPF = 0;
                if (dr["TipoDocumento"].ToString() == "8" && dr["Cancelada"].ToString() == "0" && dr["CNPJ_CPF"].ToString().Length <= 15)
                {
                    vISSPF = Convert.ToDecimal(dr["ValorTotal"]) * 5 / 100;
                    vTotalISSPF = vTotalISSPF + vISSPF;
                }
                vTotalPIS = vTotalPIS + Convert.ToDecimal(dr["ValorPIS"]);
                vTotalCofins = vTotalCofins + Convert.ToDecimal(dr["ValorCofins"]);
                if ((dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == ""))
                {
                    vTotalINSS = vTotalINSS + Convert.ToDecimal(dr["ValorINSS"]);
                    vTotalIR = vTotalIR + Convert.ToDecimal(dr["ValorIR"]);
                }
                vTotalContrSocial = vTotalContrSocial + Convert.ToDecimal(dr["ValorContrSocial"]);
                if ((dr["TipoDocumento"].ToString() == "5" || dr["TipoDocumento"].ToString() == "8") && (dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == "") && dr["CNPJ_CPF"].ToString().Length > 15)
                    vTotalISSRF = vTotalISSRF + Convert.ToDecimal(dr["ValorISSRF"]);
                if (dr["TipoDocumento"].ToString() == "5")
                {
                    if ((dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == ""))
                        vTotalFaturasEmitidas = vTotalFaturasEmitidas + Convert.ToDecimal(dr["ValorTotal"]);
                }
                else if (dr["TipoDocumento"].ToString() == "8" && (dr["Cancelada"].ToString() == "0" || dr["Cancelada"].ToString() == ""))
                    vTotalNFsIPNEmitidas = vTotalNFsIPNEmitidas + Convert.ToDecimal(dr["ValorTotal"]);
                if (dr["Cancelada"].ToString() == "1")
                    vTotalCanceladas = vTotalCanceladas + Convert.ToDecimal(dr["ValorTotal"]);
            }
            DataRow drNF = _dt.NewRow();
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL FATURAS EMITIDAS";
            drNF[4] = vTotalFaturasEmitidas.ToString("N2");
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL NFS IPM EMITIDAS";
            drNF[4] = vTotalNFsIPNEmitidas.ToString("N2");
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL FATURAS + NFS IPM";
            drNF[4] = (vTotalFaturasEmitidas + vTotalNFsIPNEmitidas).ToString("N2");
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL CANCELADAS";
            drNF[4] = vTotalCanceladas.ToString("N2");
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL RETENÇÕES";
            drNF[6] = vTotalPIS.ToString("N2");
            drNF[7] = vTotalCofins.ToString("N2");
            drNF[8] = vTotalINSS.ToString("N2");
            drNF[9] = vTotalIR.ToString("N2");
            drNF[10] = vTotalContrSocial.ToString("N2");
            drNF[11] = vTotalISSRF.ToString("N2");
            _dt.Rows.Add(drNF);
    
            drNF = _dt.NewRow();
            drNF[3] = "TOTAL ISS PF";
            drNF[12] = vTotalISSPF.ToString("N2");
            _dt.Rows.Add(drNF);
    
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr[1] != null && _dr[1].ToString() != "")
                    _dr[1] = geral.Left(_dr[1].ToString(), 10);
    
                if (_dr[2].ToString() != "0" && _dr[2].ToString() != "TOTAL" && _dr[2].ToString() != "")
                    _dr[2] = "Cancelada";
                else if (_dr[2].ToString() != "TOTAL")
                    _dr[2] = "";
    
                if (_dr[5].ToString() == "8")
                    _dr[5] = "Nota fiscal";
                else if (_dr[5].ToString() == "4")
                    _dr[5] = "Recibo";
                else if (_dr[5].ToString() == "5")
                    _dr[5] = "Fatura";
                else
                    _dr[5] = "";
    
            }        
            Grade.DataSource = _dt;       
            Grade.DataBind();
    
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                    Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);
    
                foreach (GridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells[1].Text != "" && gvr.Cells[1].Text != "&nbsp;")
                        gvr.Cells[1].Text = DateTime.Parse(gvr.Cells[1].Text).ToShortDateString();
                }
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
                lblTitulo.Text = "Relatorio de Notas Fiscais -   Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + 
                                                                                 Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + " - Data emissao: " + DateTime.Now.ToShortDateString();
                row.Cells[0].Controls.Add(lblTitulo);
                table.Rows.Add(row);
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
                table.Rows.Add(row);
                
                // NF / Fat  Emissão Sit.Trib Sit.Nota Tomador Vl.Bruto NF/ Fat PIS Cofins  INSS IR  C.Social ISS RF ISS PF
                Grade.HeaderRow.Cells[0].Text = "NF / Fat";
                Grade.HeaderRow.Cells[1].Text = "Emissao";
                Grade.HeaderRow.Cells[2].Text = "Sit.Nota";
                Grade.HeaderRow.Cells[3].Text = "Tomador";
                Grade.HeaderRow.Cells[4].Text = "Vl.Bruto NF/Fat";
                Grade.HeaderRow.Cells[5].Text = "Tipo Documento";
                Grade.HeaderRow.Cells[6].Text = "PIS";
                Grade.HeaderRow.Cells[7].Text = "Cofins";
                Grade.HeaderRow.Cells[8].Text = "INSS";
                Grade.HeaderRow.Cells[9].Text = "IR";
                Grade.HeaderRow.Cells[10].Text = "C.Social";
                Grade.HeaderRow.Cells[11].Text = "ISS RF";
                Grade.HeaderRow.Cells[12].Text = "ISS PF";
                Grade.HeaderRow.Cells[13].Text = "CNPJ/CPF Cliente";
    
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