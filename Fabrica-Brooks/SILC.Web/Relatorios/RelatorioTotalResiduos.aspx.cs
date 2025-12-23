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
    
    public partial class RelatorioTotalResiduos : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
    
        clsResiduos oResiduos = new clsResiduos();
        clsResiduoDados oResiduosDados = new clsResiduoDados();
    
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
    
        DataTable _dt = new DataTable();
        DataTable _dtResiduos = new DataTable();
        DataTable _dtClientes = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "51");
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
    
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            AddRow(_table, _row, _cell, "Relatório Total de Resíduos", 250, false, true);
            AddRow(_table, _row, _cell, "Período: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 200, false, true);
            AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToShortDateString(), 170, true, true);
            Panel1.Controls.Add(_table);
    
            string sInClientes = "";
            if (Session["RelClientes"] != null)
            {
                _dtClientes = (DataTable)Session["RelClientes"];
                foreach (DataRow _drClientes in _dtClientes.Rows)
                    sInClientes = sInClientes + ", " + _drClientes["Codigo"].ToString();
                if (sInClientes.Length > 0)
                    sInClientes = sInClientes.Substring(1);
            }
    
            string sInResiduos = "";
            if (Session["Residuos"] != null)
            {
                _dtResiduos = (DataTable)Session["Residuos"];
                foreach (DataRow _drRes in _dtResiduos.Rows)
                    sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
                if (sInResiduos.Length > 0)
                    sInResiduos = sInResiduos.Substring(1);
            }
    
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
    
            int[] iColWidth = new int[7];
    
            int iCount = 0;
            Int64 iDt1 = (Convert.ToDateTime(Data1.Data).Year * 365) + Convert.ToDateTime(Data1.Data).DayOfYear; 
            Int64 iDt2 = (Convert.ToDateTime(Data2.Data).Year * 365) + Convert.ToDateTime(Data2.Data).DayOfYear;  
    
            int iQtdeMedia = 0;
    
            if (rdbDiario.Checked)
            {
                for (Int64 f = iDt1; f <= iDt2; f++)
                {
                    iCount++;
                }
            }
            else if (rdbMensal.Checked)
            {
                iDt1 = (Convert.ToDateTime(Data1.Data).Year * 12) + Convert.ToDateTime(Data1.Data).Month;
                iDt2 = (Convert.ToDateTime(Data2.Data).Year * 12) + Convert.ToDateTime(Data2.Data).Month;
    
                string[] xSeqMesAno = new string[(iDt2 - iDt1 + 1)];
                bool jaTemMesAno = false;
                iCount = 0;
                int iMes = 0;
                DateTime _Mes = Convert.ToDateTime(Data1.Data);
                for (Int64 f = iDt1; f <= iDt2; f++)
                {
                    for (int j = 0; j <= (iDt2 - iDt1); j++)
                    {
                        if (iMes > 0)
                            _Mes = Convert.ToDateTime(Data1.Data).AddMonths(iMes);
                        if (xSeqMesAno[j] != _Mes.ToString("MM/yyyy") )
                        {
                            jaTemMesAno = false;
                            for (int k = 0; k <= (iDt2 - iDt1); k++)
                            {
                                if (xSeqMesAno[k] == _Mes.ToString("MM/yyyy"))
                                {
                                    jaTemMesAno = true;
                                    break;
                                }
                            }
                            if (!jaTemMesAno)
                            {
                                if (iCount < xSeqMesAno.Length)
                                {
                                    xSeqMesAno[iCount] = _Mes.ToString("MM/yyyy");
                                    iCount++;
                                }
                            }
                        }
                        iMes++;
                    }
                }
                for (int ii = 0; ii <= (iDt2 - iDt1); ii++)
                {
                    if (xSeqMesAno[ii] != "")
                        iQtdeMedia = iQtdeMedia + 1;
                }
            }
            else if (rdbAnual.Checked)
            {
                iCount = Convert.ToDateTime(Data2.Data).Year - Convert.ToDateTime(Data1.Data).Year + 1;
                iQtdeMedia = iCount;
            }
            iColWidth = new int[7+iCount];
            iColWidth[1] = 250;  //Tipo de Resíduo
            iColWidth[2] = 250;  //Subgrupo Resíduo
            iColWidth[3] =  70;  //Unidade
            iColWidth[4] =  80;  //Qtde Total 
            iColWidth[5] =  70;  //Média
            iColWidth[6] =  70;  //Periodos
    
            int tWidthContratos = 0;
            foreach (int iTW in iColWidth)
                tWidthContratos = tWidthContratos + iTW;
            _table.Width = tWidthContratos + 80 + (iCount * 70);
            AddRow(_table, _row, _cell, "TIPO DE RESÍDUO", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "SUBGRUPO RESÍDUO", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "UNIDADE", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "QTDE TOTAL", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "MÉDIA", iColWidth[5], false, true);
            for (int x = 0; x < iCount; x++)
            {
                if (rdbDiario.Checked)
                    AddRow(_table, _row, _cell, Convert.ToDateTime(Data1.Data).AddDays(x).ToString("dd/MM/yy"), iColWidth[6], false, true, true);
                else if (rdbMensal.Checked)
                    AddRow(_table, _row, _cell, Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("MM/yyyy"), iColWidth[6], false, true, true);
                else if (rdbAnual.Checked)            
                    AddRow(_table, _row, _cell, Convert.ToDateTime(Data1.Data).AddYears(x).ToString("yyyy"), iColWidth[6], false, true, true);
            }
            AddRow(_table, _row, _cell, "────────────────────────────────", iColWidth[1], false, true);
            AddRow(_table, _row, _cell, "────────────────────────────────", iColWidth[2], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[3], false, true);
            AddRow(_table, _row, _cell, "──────────", iColWidth[4], false, true);
            AddRow(_table, _row, _cell, "─────────", iColWidth[5], false, true);
            for (int x = 0; x < iCount; x++)
                AddRow(_table, _row, _cell, "─────────", iColWidth[6], false, true);
    
            Panel1.Controls.Add(_table);
    
            string _TipoRelatorio = "";
            if (rdbDiario.Checked)
                _TipoRelatorio = "Diario";
            else if (rdbMensal.Checked)
                _TipoRelatorio = "Mensal";
            else if (rdbAnual.Checked)
                _TipoRelatorio = "Anual";
    
            _dt = oLancamentoDados.PegaTotalResiduos(_TipoRelatorio, Data1.Data, Data2.Data, sInClientes, sInResiduos);
            if (_dt.Rows.Count > 0)
            {
                decimal _QtTotal = 0;
                string _DescricaoResiduoAnterio = "";
                foreach (DataRow dr in _dt.Rows)
                {
                    if (_DescricaoResiduoAnterio != dr["DescricaoReduzida"].ToString())
                    {
                        AddRow(_table, _row, _cell, dr["DescricaoReduzida"].ToString(), iColWidth[1], false);
                        AddRow(_table, _row, _cell, dr["GrupoNome"].ToString(), iColWidth[2], false);
                        AddRow(_table, _row, _cell, dr["Unidade"].ToString(), iColWidth[3], false);
    
                        DataRow[] ddrQt = _dt.Select("DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                     "Unidade = '" + dr["Unidade"].ToString() + "'");
                        if (ddrQt.Length > 0)
                        {
                            _QtTotal = 0;
                            foreach (DataRow _drQt in ddrQt)
                            {
                                _QtTotal = _QtTotal + Convert.ToDecimal(_drQt["QtTotal"]);
                            }
                            AddRow(_table, _row, _cell, _QtTotal.ToString("N2"), iColWidth[5], true);
                        }
                        else
                            AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[5], true);
                        if (_QtTotal > 0 && iCount > 0)
                        {
                            AddRow(_table, _row, _cell, (_QtTotal / iCount).ToString("N2"), iColWidth[6], true);
                        }
                        
                        if (rdbDiario.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("DataRetirada = '" + Convert.ToDateTime(Data1.Data).AddDays(x).ToString("dd/MM/yyyy") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddDays(x) == Convert.ToDateTime(ddr[0]["DataRetirada"]))
                                    {
                                        AddRow(_table, _row, _cell, Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2"), iColWidth[6], true);
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                    }
                                    else
                                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                                }
                                else
                                    AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                            }
                        }
                        else if (rdbMensal.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("yyyy") + "' and " +
                                                           "Mes = '" + Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("MM") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddMonths(x).Year == Convert.ToInt32(ddr[0]["Ano"]) &&
                                        Convert.ToDateTime(Data1.Data).AddMonths(x).Month == Convert.ToInt32(ddr[0]["Mes"]))
                                    {
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                        AddRow(_table, _row, _cell, Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2"), iColWidth[6], true);
                                    }
                                    else
                                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                                }
                                else
                                    AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                            }
                        }
                        else if (rdbAnual.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(Data1.Data).AddYears(x).ToString("yyyy") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddYears(x).Year == Convert.ToInt32(ddr[0]["Ano"]))
                                    {
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                        AddRow(_table, _row, _cell, Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2"), iColWidth[6], true);
                                    }
                                    else
                                        AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                                }
                                else
                                    AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
                            }
                        }
                    }
                    _DescricaoResiduoAnterio = dr["DescricaoReduzida"].ToString();
                }
                Panel1.Controls.Add(_table);
            }
        }
    
        private void AddRow(Table _table, TableRow row, TableCell cell, string pText, int pWidth, bool pAlinDireita, bool pNegrito = false, bool pAlinCenter = false)
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
            else if (pAlinCenter)
                _lbl.Attributes.CssStyle.Add("text-align", "center");
    
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
    
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (Session["Residuos"] != null)
                _dtResiduos = (DataTable)Session["Residuos"];
            else
            {
                _dtResiduos = new DataTable();
                _dtResiduos.Columns.Add("Codigo");
                _dtResiduos.Columns.Add("NomeResiduo");
            }
            if (GRUPORESIDUO1.Valor != "")
            {
                bool _bEncontrado = false;
                foreach (DataRow _drRes in _dtResiduos.Rows)
                {
                    if (_drRes["Codigo"].ToString() == GRUPORESIDUO1.Valor)
                        _bEncontrado = true;
                }
                if (!_bEncontrado)
                {
                    oResiduos = new clsResiduos();
                    oResiduosDados.PegaDados(oResiduos, Convert.ToInt32(GRUPORESIDUO1.Valor));
                    if (oResiduos.Codigo > 0)
                    {
                        GRUPORESIDUO1.Texto = oResiduos.DescricaoReduzida;
                        DataRow _dr = _dtResiduos.NewRow();
                        _dr[0] = oResiduos.Codigo;
                        _dr[1] = oResiduos.DescricaoReduzida;
                        _dtResiduos.Rows.Add(_dr);
                        Session["Residuos"] = _dtResiduos;
                        GradeResiduos.DataSource = _dtResiduos;
                        GradeResiduos.DataBind();
                    }
                }
            }
        }
    
        protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            //
        }
    
        protected void GradeResiduos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeResiduo")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    if (Session["Residuos"] != null)
                    {
                        _dtResiduos = (DataTable)Session["Residuos"];
                        if (_dtResiduos.Rows.Count > 0)
                        {
                            _dtResiduos.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                            Session["Residuos"] = _dtResiduos;
                            GradeResiduos.DataSource = _dtResiduos;
                            GradeResiduos.DataBind();
                        }
                    }
                }
            }
        }
    
        protected void GradeClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeCliente")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    if (Session["RelClientes"] != null)
                    {
                        _dtClientes = (DataTable)Session["RelClientes"];
                        if (_dtClientes.Rows.Count > 0)
                        {
                            _dtClientes.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                            Session["RelClientes"] = _dtClientes;
                            GradeClientes.DataSource = _dtClientes;
                            GradeClientes.DataBind();
                        }
                    }
                }
            }
        }
    
        protected void btnAdicionarCliente_Click(object sender, EventArgs e)
        {
            if (Session["RelClientes"] != null)
                _dtClientes = (DataTable)Session["RelClientes"];
            else
            {
                _dtClientes = new DataTable();
                _dtClientes.Columns.Add("Codigo");
                _dtClientes.Columns.Add("NomeCliente");
            }
            if (CLIENTESCONTROL1.Valor != "")
            {
                bool _bEncontrado = false;
                foreach (DataRow _drCliente in _dtClientes.Rows)
                {
                    if (_drCliente["Codigo"].ToString() == CLIENTESCONTROL1.Valor)
                        _bEncontrado = true;
                }
                if (!_bEncontrado)
                {
                    oCliente = new clsClientes();
                    oClienteDados.PegaDados(oCliente, Convert.ToInt32(CLIENTESCONTROL1.Valor));
                    if (oCliente.Codigo > 0)
                    {
                        CLIENTESCONTROL1.Texto = oCliente.NomeFantasia;
                        DataRow _dr = _dtClientes.NewRow();
                        _dr[0] = oCliente.Codigo;
                        _dr[1] = oCliente.NomeFantasia;
                        _dtClientes.Rows.Add(_dr);
                        Session["RelClientes"] = _dtClientes;
                        GradeClientes.DataSource = _dtClientes;
                        GradeClientes.DataBind();
                    }
                }
            }
        }
    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["RelClientes"] = null;
            Session["Residuos"] = null;
            Session["RelResiduos"] = null;
            Session["GrupoResiduos"] = null;
            CLIENTESCONTROL1.Valor = "";
            CLIENTESCONTROL1.Texto = "";
            GRUPORESIDUO1.Valor = "";
            GRUPORESIDUO1.Texto = "";
            string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    
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
    
            lblTitulo.ID = "lblTitulo";
            lblEmBranco.ID = "lblEmBranco";
    
            GridView Grade = new GridView();
    
            string NomeArq = "Relatorio_TotalResiduos.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
    
            string sInClientes = "";
            if (Session["RelClientes"] != null)
            {
                _dtClientes = (DataTable)Session["RelClientes"];
                foreach (DataRow _drClientes in _dtClientes.Rows)
                    sInClientes = sInClientes + ", " + _drClientes["Codigo"].ToString();
                if (sInClientes.Length > 0)
                    sInClientes = sInClientes.Substring(1);
            }
    
            string sInResiduos = "";
            if (Session["Residuos"] != null)
            {
                _dtResiduos = (DataTable)Session["Residuos"];
                foreach (DataRow _drRes in _dtResiduos.Rows)
                    sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
                if (sInResiduos.Length > 0)
                    sInResiduos = sInResiduos.Substring(1);
            }
    
            int iCount = 0;
            Int64 iDt1 = (Convert.ToDateTime(Data1.Data).Year * 365) + Convert.ToDateTime(Data1.Data).DayOfYear;
            Int64 iDt2 = (Convert.ToDateTime(Data2.Data).Year * 365) + Convert.ToDateTime(Data2.Data).DayOfYear;
    
            int iQtdeMedia = 0;
            if (rdbDiario.Checked)
            {
                for (Int64 f = iDt1; f <= iDt2; f++)
                {
                    iCount++;
                }
            }
            else if (rdbMensal.Checked)
            {
                iDt1 = (Convert.ToDateTime(Data1.Data).Year * 12) + Convert.ToDateTime(Data1.Data).Month;
                iDt2 = (Convert.ToDateTime(Data2.Data).Year * 12) + Convert.ToDateTime(Data2.Data).Month;
    
                string[] xSeqMesAno = new string[(iDt2 - iDt1 + 1)];
                bool jaTemMesAno = false;
                iCount = 0;
                int iMes = 0;
                DateTime _Mes = Convert.ToDateTime(Data1.Data);
                for (Int64 f = iDt1; f <= iDt2; f++)
                {
                    for (int j = 0; j <= (iDt2 - iDt1); j++)
                    {
                        if (iMes > 0)
                            _Mes = Convert.ToDateTime(Data1.Data).AddMonths(iMes);
                        if (xSeqMesAno[j] != _Mes.ToString("MM/yyyy"))
                        {
                            jaTemMesAno = false;
                            for (int k = 0; k <= (iDt2 - iDt1); k++)
                            {
                                if (xSeqMesAno[k] == _Mes.ToString("MM/yyyy"))
                                {
                                    jaTemMesAno = true;
                                    break;
                                }
                            }
                            if (!jaTemMesAno)
                            {
                                if (iCount < xSeqMesAno.Length)
                                {
                                    xSeqMesAno[iCount] = _Mes.ToString("MM/yyyy");
                                    iCount++;
                                }
                            }
                        }
                        iMes++;
                    }
                }
                for (int ii = 0; ii <= (iDt2 - iDt1); ii++)
                {
                    if (xSeqMesAno[ii] != "")
                        iQtdeMedia = iQtdeMedia + 1;
                }
            }
            else if (rdbAnual.Checked)
            {
                iCount = Convert.ToDateTime(Data2.Data).Year - Convert.ToDateTime(Data1.Data).Year + 1;
                iQtdeMedia = iCount;
            }
            string _TipoRelatorio = "";
            if (rdbDiario.Checked)
                _TipoRelatorio = "Diario";
            else if (rdbMensal.Checked)
                _TipoRelatorio = "Mensal";
            else if (rdbAnual.Checked)
                _TipoRelatorio = "Anual";
    
            _dt = oLancamentoDados.PegaTotalResiduos(_TipoRelatorio, Data1.Data, Data2.Data, sInClientes, sInResiduos);
            DataTable dtGrade = new DataTable();
    
            if (_dt.Rows.Count > 0)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "TIPO DE RESIDUO";
                dtGrade.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "SUBGRUPO RESIDUO";
                dtGrade.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "UNIDADE";
                dtGrade.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "QTDE TOTAL";
                dtGrade.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "MEDIA";
                dtGrade.Columns.Add(dc);
    
                for (int x = 0; x < iCount; x++)
                {
                    if (rdbDiario.Checked)
                    {
                        dc = new DataColumn();
                        dc.ColumnName = Convert.ToDateTime(Data1.Data).AddDays(x).ToString("dd-MM-yy");
                        dc.Caption = Convert.ToDateTime(Data1.Data).AddDays(x).ToString("dd/MM/yy");
                        dtGrade.Columns.Add(dc);
                    }
                    else if (rdbMensal.Checked)
                    {
                        dc = new DataColumn();
                        dc.ColumnName = Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("MM-yyyy");
                        dc.Caption = Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("MM-yyyy");
                        dtGrade.Columns.Add(dc);
                    }
                    else if (rdbAnual.Checked)
                    {
                        dc = new DataColumn();
                        dc.ColumnName = Convert.ToDateTime(Data1.Data).AddYears(x).ToString("yyyy");
                        dc.Caption = Convert.ToDateTime(Data1.Data).AddYears(x).ToString("yyyy");
                        dtGrade.Columns.Add(dc);
                    }
                }
    
                decimal _QtTotal = 0;
                string _DescricaoResiduoAnterio = "";
                foreach (DataRow dr in _dt.Rows)
                {
                    if (_DescricaoResiduoAnterio != dr["DescricaoReduzida"].ToString())
                    {
                        DataRow drGrade = dtGrade.NewRow();
                        drGrade[0] = dr["DescricaoReduzida"].ToString();
                        drGrade[1] = dr["GrupoNome"].ToString();
                        drGrade[2] = dr["Unidade"].ToString();
    
                        DataRow[] ddrQt = _dt.Select("DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                     "Unidade = '" + dr["Unidade"].ToString() + "'");
                        if (ddrQt.Length > 0)
                        {
                            _QtTotal = 0;
                            foreach (DataRow _drQt in ddrQt)
                            {
                                _QtTotal = _QtTotal + Convert.ToDecimal(_drQt["QtTotal"]);
                            }
                            drGrade[3] = _QtTotal.ToString("N2");
                        }
                        else
                            drGrade[3] = 0.ToString("N2");
                        if (_QtTotal > 0 && iCount > 0)
                        {
                            drGrade[4] = (_QtTotal / iCount).ToString("N2");
                        }
    
                        if (rdbDiario.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("DataRetirada = '" + Convert.ToDateTime(Data1.Data).AddDays(x).ToString("dd/MM/yyyy") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddDays(x) == Convert.ToDateTime(ddr[0]["DataRetirada"]))
                                    {
                                        drGrade[x + 5] = Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2");
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                    }
                                    else
                                        drGrade[x + 5] = 0.ToString("N2");
                                }
                                else
                                    drGrade[x + 5] = 0.ToString("N2");                            
                            }
                            dtGrade.Rows.Add(drGrade);
                        }
                        else if (rdbMensal.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("yyyy") + "' and " +
                                                           "Mes = '" + Convert.ToDateTime(Data1.Data).AddMonths(x).ToString("MM") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddMonths(x).Year == Convert.ToInt32(ddr[0]["Ano"]) &&
                                        Convert.ToDateTime(Data1.Data).AddMonths(x).Month == Convert.ToInt32(ddr[0]["Mes"]))
                                    {
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                        drGrade[x + 5] = Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2");
                                    }
                                    else
                                        drGrade[x + 5] = 0.ToString("N2");
                                }
                                else
                                    drGrade[x + 5] = 0.ToString("N2");                            
                            }
                            dtGrade.Rows.Add(drGrade);
                        }
                        else if (rdbAnual.Checked)
                        {
                            for (int x = 0; x < iCount; x++)
                            {
                                DataRow[] ddr = _dt.Select("Ano = '" + Convert.ToDateTime(Data1.Data).AddYears(x).ToString("yyyy") + "' and " +
                                                           "DescricaoReduzida = '" + dr["DescricaoReduzida"].ToString() + "' and " +
                                                           "Unidade = '" + dr["Unidade"].ToString() + "'");
                                if (ddr.Length > 0)
                                {
                                    if (Convert.ToDateTime(Data1.Data).AddYears(x).Year == Convert.ToInt32(ddr[0]["Ano"]))
                                    {
                                        _QtTotal = _QtTotal + Convert.ToDecimal(ddr[0]["QtTotal"]);
                                        drGrade[x + 5] = Convert.ToDecimal(ddr[0]["QtTotal"]).ToString("N2");
                                    }
                                    else
                                        drGrade[x + 5] = 0.ToString("N2");
                                }
                                else
                                    drGrade[x + 5] = 0.ToString("N2");
                            }
                            dtGrade.Rows.Add(drGrade);
                        }
                    }
                    _DescricaoResiduoAnterio = dr["DescricaoReduzida"].ToString();
                }
    
                Grade.DataSource = dtGrade;
                Grade.DataBind();
    
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
                lblTitulo.Text = "Relatorio Total de Residuos";
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
                lblColuna.Text = "Relatorio Total de Residuos - Periodo: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + 
                                 Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy") + " - Emissao: " + DateTime.Now.ToShortDateString();
                row.Cells[0].Controls.Add(lblColuna);
                table.Rows.Add(row);
    
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