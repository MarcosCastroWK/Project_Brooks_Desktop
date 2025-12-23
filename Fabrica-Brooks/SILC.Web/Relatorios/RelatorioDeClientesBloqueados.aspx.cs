using System;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.Relatorios
{
    
    public partial class Relatorios_RelatorioDeClientesBloqueados : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        clsBloqFinanceiroDados oBloqFinanceiroDados = new clsBloqFinanceiroDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "48");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (!IsPostBack)
            {
                lblTitulo0.Text = "";
                geral.Ordem = " NomeFantasia ";
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            
        }
        private void TituloRelatorio()
        {
            lblTitulo0.Text = "Relatório de Clientes Bloqueados";
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
            Grade.DataSource = oBloqFinanceiroDados.PreencheDataTableParaRelatorio(geral.Ordem);
            Grade.DataBind();
    
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Relatorio();
        }
        protected void imbExcel_Click(object sender, ImageClickEventArgs e)
        {
            Grade.DataSource = oBloqFinanceiroDados.PreencheDataTableParaRelatorio(geral.Ordem);
            Grade.DataBind();
    
            if (Grade.HeaderRow == null)
            {
                lblTitulo0.Text = "Não há dados!";
                return;
            }
    
            Table table = new Table();
            TableRow row = new TableRow();
    
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
    
            Label lblEmpresa = new Label();
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();
    
            lblTitulo.ID = "lblTitulo";
            lblEmBranco.ID = "lblEmBranco";
    
            string NomeArq = "Relatorio_ClientesBloqueados.xls";
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
    
            lblTitulo.Text = "Relatorio de clientes bloqueados";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);
    
            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);
    
            // não ta fazendo 
            Grade.AutoGenerateColumns = false;
            Grade.HeaderRow.Cells[0].Width = Unit.Parse("100px");
            Grade.HeaderRow.Cells[1].Width = Unit.Parse("500px");
            Grade.HeaderRow.Cells[2].Width = Unit.Parse("500px");
            Grade.HeaderRow.Cells[3].Width = Unit.Parse("100px");
                    
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