using System;
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
    
    public partial class Relatorios_RelatorioDeClientesAReajustar : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        //Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsContratosReajustesDados oContratosReajustesDados = new clsContratosReajustesDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "49");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (!IsPostBack)
            {
                lblTitulo0.Text = "";
                Data1.Data = ""; //DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                Data2.Data = ""; //DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                geral.Ordem = " max(cr.DataReajuste), NomeFantasia asc ";
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            
        }
        private void TituloRelatorio()
        {
            if (Data1.Data != "" && Data2.Data != "")
                lblTitulo0.Text = "Relatório de Clientes A Reajustar - Período de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy") + " a " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            else if (Data1.Data != "" && Data2.Data == "")
                lblTitulo0.Text = "Relatório de Clientes A Reajustar - Período - a partir de: " + Convert.ToDateTime(Data1.Data).ToString("dd/MM/yyyy");
            else if (Data1.Data == "" && Data2.Data != "")
                lblTitulo0.Text = "Relatório de Clientes A Reajustar - Período até: " + Convert.ToDateTime(Data2.Data).ToString("dd/MM/yyyy");
            else
                lblTitulo0.Text = "Relatório de Clientes A Reajustar";
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0)
            {
                TituloRelatorio();
                e.Row.Cells[2].Width = Unit.Pixel(400);
            }
        }
        private void Relatorio()
        {
            TituloRelatorio();
            if (geral.Ordem == "")
                geral.Ordem = " max(cr.DataReajuste), NomeFantasia asc ";
            Grade.DataSource = oContratosReajustesDados.PreencheDataTableReajustes(geral.Ordem, Data1.Data, Data2.Data);
            Grade.DataBind();
    
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Relatorio();
        }
        protected void imbExcel_Click(object sender, ImageClickEventArgs e)
        {
            Grade.DataSource = oContratosReajustesDados.PreencheDataTableReajustes(geral.Ordem, Data1.Data, Data2.Data);
            Grade.DataBind();
    
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
            lblEmBranco.ID = "lblEmBranco";
    
            string NomeArq = "Relatorio_ClientesReajustar.xls";
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
    
            lblTitulo.Text = "Relatorio de clientes a reajustar";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblTitulo);
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
    
            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
    
        }
    }
}