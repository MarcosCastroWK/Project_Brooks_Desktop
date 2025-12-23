using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.Relatorios
{
    
    public partial class RelatorioReciclaveisPorDestinoFinal : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
    
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsResiduos oResiduos = new clsResiduos();
        clsResiduoDados oResiduosDados = new clsResiduoDados();
    
        DataTable _dt = new DataTable();
        DataTable _dtResiduos = new DataTable();
        
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
                DESTINOFINAL1.Valor = "";
                DESTINOFINAL1.Texto = "";
            }
        }
    
        private void Relatorio()
        {
            oDestinoFinal = new clsDestinoFinal();
            if (DESTINOFINAL1.Valor != "" && DESTINOFINAL1.Valor != "0")
            {
                oDestinoFinal.Codigo = Convert.ToInt32(DESTINOFINAL1.Valor);
                oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
            }
            else
                DESTINOFINAL1.Valor = "0";
    
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
            AddRow(_table, _row, _cell, "Relatório de Recicláveis por Destino Final", 450, false, true);
            AddRow(_table, _row, _cell, "Período: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy"), 200, false, true);
            AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToShortDateString(), 400, true, true);
            Panel1.Controls.Add(_table);
    
            _table = new Table();
            _row = new TableRow();
            _cell = new TableCell();
            _cell.BorderWidth = 0;
            AddRow(_table, _row, _cell, "Código Destino Final: " + DESTINOFINAL1.Valor, 180, false, true);
            AddRow(_table, _row, _cell, oDestinoFinal.Nome, 400, false, true);
            Panel1.Controls.Add(_table);
            if (oDestinoFinal.Codigo > 0 )
            {
                if (Session["Residuos"] != null)
                    _dtResiduos = (DataTable)Session["Residuos"];
                string sInResiduos = "";
                foreach (DataRow _drRes in _dtResiduos.Rows)
                    sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
                if (sInResiduos.Length > 0)
                    sInResiduos = sInResiduos.Substring(1);
                _dt = oLancamentoDados.PreencheDadosRelatorioReciclaveis(Data1.Data, Data2.Data, Convert.ToInt32(DESTINOFINAL1.Valor), sInResiduos, "NomeFantasia, Residuo, DataRetirada");
                if (_dt.Rows.Count > 0)
                {
                    _table = new Table();
                    _row = new TableRow();
                    _cell = new TableCell();
    
                    int[] iColWidth = new int[7];
                    iColWidth[1] = 80;   //Código
                    iColWidth[2] = 300;  //Cliente 
                    iColWidth[3] = 70;   //Data Coleta
                    iColWidth[4] = 80;   //Código
                    iColWidth[5] = 400;  //Resíduo
                    iColWidth[6] = 88;   //Quantidade
    
                    int tWidthContratos = 0;
                    foreach (int iTW in iColWidth)
                        tWidthContratos = tWidthContratos + iTW;
                    _table.Width = tWidthContratos + 10;
                    AddRow(_table, _row, _cell, "Código", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "Cliente", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "Dt.Coleta", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "Código", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "Resíduos", iColWidth[5], false, true);
                    AddRow(_table, _row, _cell, "Quantidade", iColWidth[6], true, true);
    
                    AddRow(_table, _row, _cell, "──────────", iColWidth[1], false, true);
                    AddRow(_table, _row, _cell, "───────────────────────────────────────", iColWidth[2], false, true);
                    AddRow(_table, _row, _cell, "────────", iColWidth[3], false, true);
                    AddRow(_table, _row, _cell, "──────────", iColWidth[4], false, true);
                    AddRow(_table, _row, _cell, "────────────────────────────────────────────────────", iColWidth[5], false, true);
                    AddRow(_table, _row, _cell, "───────────", iColWidth[6], false, true);
    
                    decimal _QtSbTlColeta = 0;
                    decimal _TotalQtColetas = 0;
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
                            AddRow(_table, _row, _cell, "Subtotais", iColWidth[5], true);
                            AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[6], true);
                            
                            _QtSbTlColeta = 0;
                            AddRow(_table, _row, _cell, "", iColWidth[1], false, true);
                            AddRow(_table, _row, _cell, "", iColWidth[2], false, true);
                            AddRow(_table, _row, _cell, "", iColWidth[3], false, true);
                            AddRow(_table, _row, _cell, "", iColWidth[4], false, true);
                            AddRow(_table, _row, _cell, "", iColWidth[5], false, true);
                            AddRow(_table, _row, _cell, "", iColWidth[6], true, true);
                        }
                        _grupoEresiduoAnterior = dr["GrupoResiduo"].ToString() + "-" + dr["Residuo"].ToString();
    
                        AddRow(_table, _row, _cell, dr["CodigoCliente"].ToString(), iColWidth[1], true);
                        AddRow(_table, _row, _cell, dr["NomeFantasia"].ToString(), iColWidth[2], false);
                        if (dr["DataRetirada"].ToString() != "")
                            AddRow(_table, _row, _cell, Convert.ToDateTime(dr["DataRetirada"]).ToString("dd/MM/yy"), iColWidth[3], false);
                        else
                            AddRow(_table, _row, _cell, "", iColWidth[3], false);
    
                        AddRow(_table, _row, _cell, dr["CodigoResiduo"].ToString(), iColWidth[4], true);
                        AddRow(_table, _row, _cell, geral.Left(dr["GrupoResiduo"].ToString(), 20) + "-" + geral.Left(dr["Residuo"].ToString(), 20), iColWidth[5], false);
    
                        if (dr["QtDescarga"].ToString() != "")
                        {
                            AddRow(_table, _row, _cell, Convert.ToDecimal(dr["QtDescarga"]).ToString("N2"), iColWidth[6], true);
                            _QtSbTlColeta = _QtSbTlColeta + Convert.ToDecimal(dr["QtDescarga"]);
                            _TotalQtColetas = _TotalQtColetas + Convert.ToDecimal(dr["QtDescarga"]);
                        }
                        else
                            AddRow(_table, _row, _cell, 0.ToString("N2"), iColWidth[6], true);
    
    
                    }
                    // adicionar linha com subtotais
                    AddRow(_table, _row, _cell, "", iColWidth[1], true);
                    AddRow(_table, _row, _cell, "", iColWidth[2], false);
                    AddRow(_table, _row, _cell, "", iColWidth[3], false);
                    AddRow(_table, _row, _cell, "", iColWidth[4], false);
                    AddRow(_table, _row, _cell, "Subtotais", iColWidth[5], true);
                    AddRow(_table, _row, _cell, _QtSbTlColeta.ToString("N2") + "", iColWidth[6], true);
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
                foreach (DataRow _drdest in _dtResiduos.Rows)
                {
                    if (_drdest["Codigo"].ToString() == GRUPORESIDUO1.Valor)
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
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "NomeDestinoFinal")
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
    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            DESTINOFINAL1.Valor = "";
            DESTINOFINAL1.Texto = "";
            Session["Residuos"] = null;
            Session["DestinoFinal"] = null;
            string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
    
        }
        protected void imbExcel_Click(object sender, ImageClickEventArgs e)
        {
            Label lblColuna = new Label();
    
            oDestinoFinal = new clsDestinoFinal();
            if (DESTINOFINAL1.Valor != "" && DESTINOFINAL1.Valor != "0")
            {
                oDestinoFinal.Codigo = Convert.ToInt32(DESTINOFINAL1.Valor);
                oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(DESTINOFINAL1.Valor));
            }
            else
                DESTINOFINAL1.Valor = "0";
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
            string NomeArq = "Relatorio_ReciclaveisPorDestinoFinal.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
    
            if (Session["Residuos"] != null)
                _dtResiduos = (DataTable)Session["Residuos"];
            string sInResiduos = "";
            foreach (DataRow _drRes in _dtResiduos.Rows)
                sInResiduos = sInResiduos + ", " + _drRes["Codigo"].ToString();
            if (sInResiduos.Length > 0)
                sInResiduos = sInResiduos.Substring(1);
            _dt = oLancamentoDados.PreencheDadosRelatorioReciclaveis(Data1.Data, Data2.Data, Convert.ToInt32(DESTINOFINAL1.Valor), sInResiduos, "NomeFantasia, Residuo, DataRetirada");
            //   Código Cliente Dt.Coleta Código Resíduos Quantidade 
    
            _dt.Columns.RemoveAt(0);
            _dt.Columns.RemoveAt(2);
            _dt.Columns.RemoveAt(5);
            _dt.Columns.RemoveAt(6);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
    
            foreach (DataRow _dr in _dt.Rows)
            {
                _dr[4] = _dr[4].ToString() + "-" + _dr[6].ToString();
            }
            _dt.Columns.RemoveAt(_dt.Columns.Count - 1);
            _dt.Columns.Add("Subtotal");
    
            decimal _QtSbTl = 0;
            string _grupoEresiduoAnterior = "";
            if (_dt.Rows.Count > 0)
            {
                _grupoEresiduoAnterior = _dt.Rows[1][4].ToString();
                if (_dt.Rows[0][5] != null)
                    _QtSbTl = Convert.ToDecimal(_dt.Rows[0][5]);
            }
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr2 = _dt.Rows[i];
                if (_grupoEresiduoAnterior != dr2[4].ToString() && _grupoEresiduoAnterior != "")
                {
                    if (i == 0)
                        dr2[6] = _QtSbTl;
                    else
                        _dt.Rows[i - 1][6] = _QtSbTl;
                    _QtSbTl = 0;
                }
                _QtSbTl = _QtSbTl + Convert.ToDecimal(dr2[5]);
                _grupoEresiduoAnterior = dr2[4].ToString();
            }
            if (_dt.Rows.Count - 1 >= 0)
                _dt.Rows[_dt.Rows.Count - 1][6] = _QtSbTl.ToString("N2");
    
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
                lblTitulo.Text = "Relatorio de Reciclaveis por Destino Final  -  Periodo de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
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
                dtTitulo.Columns.Add(dc);
    
                dc = new DataColumn();
                dc.ColumnName = "DestinoFinal";
                dtTitulo.Columns.Add(dc);
    
                dc = new DataColumn();
                dc.ColumnName = "RazaoSocial";
                dtTitulo.Columns.Add(dc);
    
                DataRow dr = dtTitulo.NewRow();
                dr[0] = DESTINOFINAL1.Valor;
                dr[1] = geral.RemoverAcentos(oDestinoFinal.NomeFantasia);
                dr[2] = geral.RemoverAcentos(oDestinoFinal.Nome);
                dtTitulo.Rows.Add(dr);
    
                Grade3.DataSource = dtTitulo;
                Grade3.DataBind();
    
                Grade3.HeaderRow.Cells[0].Text = "Codigo";
                Grade3.HeaderRow.Cells[1].Text = "Destino final";
                Grade3.HeaderRow.Cells[2].Text = "Razao social";
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                row.Cells[0].Controls.Add(Grade3);
                table.Rows.Add(row);
    
                // CodigoCliente	NomeFantasia	DataRetirada	CodigoResiduo	Residuo	QtDescarga	GrupoResiduo
                Grade.HeaderRow.Cells[0].Text = "Codigo";
                Grade.HeaderRow.Cells[1].Text = "Cliente";
                Grade.HeaderRow.Cells[2].Text = "Data da coleta";
                Grade.HeaderRow.Cells[3].Text = "Codigo";
                Grade.HeaderRow.Cells[4].Text = "Descricao residuo";
                Grade.HeaderRow.Cells[5].Text = "Quantidade";
                Grade.HeaderRow.Cells[6].Text = "Sub total";            
    
                row = new TableRow();
                row.Cells.Add(new TableCell());
                lblEmBranco.ID = "lblEmBranco";
                lblEmBranco.Text = "\n";
                row.Cells[0].Controls.Add(lblEmBranco);
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