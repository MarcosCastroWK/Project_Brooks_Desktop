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
    
    public partial class Relatorios_RelatorioConferenciaDiariaPorCliente : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "33");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (!IsPostBack)
            {
                lblTitulo0.Text = "";
    
                string _ultimodiamesanterior = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year)).AddDays(-1).ToString("dd/MM/yyyy");
                Data1.Data = Convert.ToDateTime(("01/" + Convert.ToDateTime(_ultimodiamesanterior).Month.ToString() + "/" +
                                                         Convert.ToDateTime(_ultimodiamesanterior).Year.ToString())).ToString("dd/MM/yyyy");
                Data2.Data = Convert.ToDateTime(_ultimodiamesanterior).ToString("dd/MM/yyyy"); 
    
                CLIENTESCONTROL1.Valor = "";
                CLIENTESCONTROL1.Texto = "";
                lblTotalMovimentacoes.Text = "";
                imglogo.Visible = false;
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (Data1.Data != "")
            {
                if (Data2.Data == "")
                    Data2.Data = Data1.Data;
                Relatorio();
            }
            else
            {
                Grade.DataSource = new DataTable();
                Grade.DataBind();
                lblTotalMovimentacoes.Text = "Data inválida ou formato incorreto!";
            }
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                lblTitulo0.Text = "Relatório de Conferência Diária por Cliente - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
    
                // 0 = Lançamento
                e.Row.Cells[0].Width = Unit.Pixel(60);
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                // 1 = Nome fantasia
                e.Row.Cells[1].Width = Unit.Pixel(300);
                // 2 = Código
                e.Row.Cells[2].Width = Unit.Pixel(60);
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                // 3 = NumeroMTR
                e.Row.Cells[3].Width = Unit.Pixel(60);
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                // 4 = Container
                e.Row.Cells[4].Width = Unit.Pixel(40);
                // 5 = Data Colocação
                e.Row.Cells[5].Width = Unit.Pixel(80);
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                if (geral.Left(e.Row.Cells[5].Text, 10) == "1/1/100" || geral.Left(e.Row.Cells[5].Text, 10) == "01/01/0100" || geral.Left(e.Row.Cells[5].Text, 10) == "01/01/000")
                    e.Row.Cells[5].Text = "";
                else
                    e.Row.Cells[5].Text = geral.Left(e.Row.Cells[5].Text, 10);
                // 6 = Data Retirada
                e.Row.Cells[6].Width = Unit.Pixel(80);
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                if (geral.Left(e.Row.Cells[6].Text, 10) == "1/1/100" || geral.Left(e.Row.Cells[6].Text, 10) == "01/01/0100" || geral.Left(e.Row.Cells[6].Text, 10) == "01/01/000")
                    e.Row.Cells[6].Text = "";
                else
                    e.Row.Cells[6].Text = geral.Left(e.Row.Cells[6].Text, 10);
                // 7 = Descrição Resíduo
                e.Row.Cells[7].Width = Unit.Pixel(300);
                // 8 - Quantidade
                e.Row.Cells[8].Width = Unit.Pixel(80);
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
    
                // 13 - Motorista
                if (e.Row.Cells[12].Text.Split(" "[0]).Length > 0)
                    e.Row.Cells[12].Text = e.Row.Cells[12].Text.Split(" "[0])[0];
            }
        }
    
        private void Relatorio()
        {
            
            if (CLIENTESCONTROL1.Valor == "")
                CLIENTESCONTROL1.Valor = "0";
            if (rdbOrdemCaminhao.Checked)
                geral.Ordem = "cdCaminhao";
            else
                geral.Ordem = "DataRetirada";
            if (chkMTR.Checked)
                lblTitulo0.Text = "Relatório de Conferência Diária por Cliente   Período      -      Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            else
                lblTitulo0.Text = "Relatório de Conferência Diária por Cliente   Período      -      Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
    
            Grade.DataSource = oLancamentoDados.PreencheDadosConferenciaClient(Data1.Data, Data2.Data, Convert.ToInt32(CLIENTESCONTROL1.Valor), geral.Ordem);
            Grade.DataBind();
    
            imglogo.Visible = true;
    
            if (!chkMTR.Checked)
                Grade.Columns[3].Visible = false;
            else
                Grade.Columns[3].Visible = true;
            if (!chkContainer.Checked)
                Grade.Columns[4].Visible = false;
            else
                Grade.Columns[4].Visible = true;
            if (!chkDataColocacao.Checked)
                Grade.Columns[5].Visible = false;
            else
                Grade.Columns[5].Visible = true;
            if (!chkDataRetirada.Checked)
                Grade.Columns[6].Visible = false;
            else
                Grade.Columns[6].Visible = true;
            if (!chkResiduo.Checked)
                Grade.Columns[7].Visible = false;
            else
                Grade.Columns[7].Visible = true;
            if (!chkQuantidade.Checked)
                Grade.Columns[8].Visible = false;
            else
                Grade.Columns[8].Visible = true;
            if (!chkUnidade.Checked)
                Grade.Columns[9].Visible = false;
            else
                Grade.Columns[9].Visible = true;
            if (!chkDestino.Checked)
                Grade.Columns[10].Visible = false;
            else
                Grade.Columns[10].Visible = true;
            if (!chkCaminhao.Checked)
                Grade.Columns[11].Visible = false;
            else
                Grade.Columns[11].Visible = true;
            if (!chkMotorista.Checked)
                Grade.Columns[12].Visible = false;
            else
                Grade.Columns[12].Visible = true;
            if (!chkQtColetada.Checked)
                Grade.Columns[13].Visible = false;
            else
                Grade.Columns[13].Visible = true;
            if (!chkValorUnitario.Checked)
                Grade.Columns[14].Visible = false;
            else
                Grade.Columns[14].Visible = true;
            if (!chkValorTotal.Checked)
                Grade.Columns[15].Visible = false;
            else
                Grade.Columns[15].Visible = true;
            if (!chkObs.Checked)
                Grade.Columns[16].Visible = false;
            else
                Grade.Columns[16].Visible = true;
    
            lblTotalMovimentacoes.Text = "Total de movimentações: " + Grade.Rows.Count.ToString();
    
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
            if (Grade.HeaderRow == null)
            {
                lblTitulo0.Text = "Não há dados!";
                return;
            }
    
            Table table = new Table();
            TableRow row = new TableRow();
    
            System.IO.StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
    
            Label lblEmpresa = new Label();
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();
            lblTitulo.ID = "lblTitulo";
            lblTitulo.Text = geral.RemoverAcentos(lblTitulo0.Text);
            lblEmBranco.ID = "lblEmBranco";
    
            string NomeArq = "Relatorio_ConferenciaDiariaPorCliente.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
            for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);
    
            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmpresa.ID = "lblEmpresa";
            lblEmpresa.Text = geral.NomeEmpresa(geral.CodigoEmpresa);
            row.Cells[0].Controls.Add(lblEmpresa);
            table.Rows.Add(row);
    
            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);
    
            Label lblTt = new Label();
            lblTt.ID = "lblTt";
            lblTt.Text = geral.RemoverAcentos(lblTitulo0.Text);
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblTt);
            table.Rows.Add(row);
    
            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);
    
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade);
            table.Rows.Add(row);
    
            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);
    
            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblTitulo.Text = geral.RemoverAcentos(geral.RemoverAcentos(lblTotalMovimentacoes.Text));
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);
    
            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
    
        }
    }
}