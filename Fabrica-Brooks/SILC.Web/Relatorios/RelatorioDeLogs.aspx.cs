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
    public partial class Relatorios_RelatorioDeLogs : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        clsLogDados oLogDados = new clsLogDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "37");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (!IsPostBack)
            {
                lblTitulo0.Text = "";
                datDataInicio.Data = DateTime.Now.AddDays(-10).ToString("dd/MM/yyyy");
                datDataFinal.Data = DateTime.Now.ToString("dd/MM/yyyy");
                geral.Ordem = " l.Codigo desc";
            }
        }
       
        private void TituloRelatorio()
        {
            lblTitulo0.Text = "Relatório de Logs";
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Relatorio();
        }
        private void Relatorio()
        {
            TituloRelatorio();
    
            Panel1.BorderWidth = 0;
    
            Table _table = new Table();
            TableRow _row = new TableRow();
            TableCell _cell = new TableCell();
    
            AddRow(_table, _row, _cell, geral.NomeEmpresa(1) + Environment.NewLine + "Relatório de Logs", 160, false, true);
            AddRow(_table, _row, _cell, "Período de: " + datDataInicio.Data + " a " + datDataFinal.Data, 700, false, true);
            AddRow(_table, _row, _cell, "Emissão: " + DateTime.Now.ToString("dd/MM/yy"), 100, true);
            Panel1.Controls.Add(_table);
    
            _dt = oLogDados.PreencheDT(geral.Ordem, datDataInicio.Data, datDataFinal.Data, ddlOpcaoDoMenu.SelectedItem.Text);
            if (_dt.Rows.Count > 0)
            {
                _table = new Table();
                _row = new TableRow();
                _cell = new TableCell();
    
                int[] iColWidth = new int[8];
                iColWidth[1] =  50;  //Codigo
                iColWidth[2] = 150;  //Usuário
                iColWidth[3] =  50;  //Data
                iColWidth[4] =  50;  //Hora
                iColWidth[5] = 700;  //Log
                iColWidth[6] = 100;  //Operação
                iColWidth[7] = 150;  //Local Operação
    
                int tWidthContratos = 0;
                foreach (int iTW in iColWidth)
                    tWidthContratos = tWidthContratos + iTW;
                _table.Width = tWidthContratos + 10;
                AddRow(_table, _row, _cell, "Código", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "Usuário", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "Data", iColWidth[3], false, true, true);
                AddRow(_table, _row, _cell, "Hora", iColWidth[4], false, true, true);
                AddRow(_table, _row, _cell, "Log", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "Operação", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "Local Operação", iColWidth[7], false, true);
     
                AddRow(_table, _row, _cell, "──────", iColWidth[1], false, true);
                AddRow(_table, _row, _cell, "──────────────────", iColWidth[2], false, true);
                AddRow(_table, _row, _cell, "──────", iColWidth[3], false, true);
                AddRow(_table, _row, _cell, "──────", iColWidth[4], false, true);
                AddRow(_table, _row, _cell, "────────────────────────────────────────────────────────────────────────────────────────────", iColWidth[5], false, true);
                AddRow(_table, _row, _cell, "────────────", iColWidth[6], false, true);
                AddRow(_table, _row, _cell, "─────────────────", iColWidth[7], false, true);
                Panel1.Controls.Add(_table);
    
                DataRow[] _ddr = _dt.Select("", "Codigo desc");
                for (int i = 0; i < _ddr.Length; i++)
                {
                    DataRow dr = _ddr[i];
                    AddRow(_table, _row, _cell, dr["Codigo"].ToString(), iColWidth[1], true);
                    AddRow(_table, _row, _cell, dr["Usuario"].ToString(), iColWidth[2], false);
                    AddRow(_table, _row, _cell, Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yy"), iColWidth[3], true);
                    AddRow(_table, _row, _cell, dr["Hora"].ToString(), iColWidth[4], true);
                    string _log1 = dr["Log"].ToString();
                    string[] _item1log = _log1.Split("\n"[0]);
                    string _log = _log1;
                    if (i + 1 < _ddr.Length)
                    {
                        _log = "";
                        string _log2 = _ddr[i + 1]["Log"].ToString();
                        string[] _item2log = _log2.Split("\n"[0]);
                        bool bExiste = false;
                        for (int i2 = i; i2 < _ddr.Length; i2++)
                        {
                            DataRow dr2 = _ddr[i2];
                            if (i2 + 1 < _ddr.Length)
                            {
                                if (_ddr[i]["Log"].ToString().Split("\n"[0])[0] == _ddr[i2 + 1]["Log"].ToString().Split("\n"[0])[0])
                                {
                                    bExiste = true;
                                    _item2log = _ddr[i2 + 1]["Log"].ToString().Split("\n"[0]);
                                    break;
                                }
                            }
                        }
                        if (bExiste)
                        {
                            for (int iLog = 0; iLog < _item1log.Length; iLog++)
                            {
                                if (_item2log.Length > iLog)
                                {
                                    if (_item1log[iLog] != _item2log[iLog])
                                    {
                                        _log = _log + "<b>" + _item1log[iLog] + "</b>\n";
                                    }
                                    else
                                    {
                                        _log = _log + _item1log[iLog] + "\n";
                                    }
                                }
                                else
                                {
                                    _log = _log + _item1log[iLog] + "\n";
                                }
                            }
                        }
                        else
                            _log = _log1;
                    }
                    AddRow(_table, _row, _cell, _log.Replace("\n", "<br />"), iColWidth[5], false);
                    AddRow(_table, _row, _cell, dr["Operacao"].ToString(), iColWidth[6], false);
                    AddRow(_table, _row, _cell, dr["LocalOperacao"].ToString(), iColWidth[7], false);
    
                }           
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
            _lbl.Attributes.CssStyle.Add("vertical-align", "top");
    
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
    }
}