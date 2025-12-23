using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.Relatorios
{
    
    
    public partial class Relatorios_RelatorioContaineres : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    
        clsUsuarios oUsuario = new clsUsuarios();
        Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsLancamentosDados oLancamentos = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        DataTable _dt = new DataTable();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "30");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            lblTitulo0.Text = "";
            lblTotalContaineres.Text = "";
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grade.Rows.Count == 0)
                    Relatorio();
            }
            finally
            {
                Session["ctrl"] = Panel1;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                    "<script language=javascript>window.open('Imprimir.aspx','Imprimir','height=900px,width=1600px,scrollbars=1');</script>");
            }
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0)
            {
                if (rdbLocadas.Checked)
                {
                    lblTitulo0.Text = "Relatório de Containeres Locados";
                    Grade.HeaderRow.Cells[1].Text = "Lançamento";
                    Grade.HeaderRow.Cells[3].Text = "Código";
                    Grade.HeaderRow.Cells[4].Text = "Data Colocação";
                    Grade.HeaderRow.Cells[2].Width = Unit.Pixel(400);
                }
                else if (rdbDisponiveis.Checked)
                {
                    Grade.Width = Unit.Pixel(300);
                    lblTitulo0.Text = "Relatório de Containeres Disponíveis";
                }
            }
            if (e.Row.RowIndex >= 0)
            {
                if (rdbLocadas.Checked)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].Text = geral.DataFormatada(e.Row.Cells[4].Text);
                }
                else
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }
        protected void rdbDisponiveis_CheckedChanged(object sender, EventArgs e)
        {
            Relatorio();
        }
        protected void rdbLocadas_CheckedChanged(object sender, EventArgs e)
        {
            Relatorio();
        }
        private void Relatorio()
        {
            if (rdbLocadas.Checked)
            {
                lblTitulo0.Text = "Relatório de Containeres Locados";
                if (rdbDataLocacao.Checked)
                    Grade.DataSource = oLancamentos.PreencheDataComContaineresLocados("Data_Colocação");
                else if (rdbNumeroCaixa.Checked)
                    Grade.DataSource = oLancamentos.PreencheDataComContaineresLocados("Container");
                else if (rdbNomeFantasia.Checked)
                    Grade.DataSource = oLancamentos.PreencheDataComContaineresLocados("Cliente");
                Grade.DataBind();
                lblTotalContaineres.Text = "Total Containeres: " + oLancamentos.CountLocados().ToString();
            }
            else if (rdbDisponiveis.Checked)
            {
                lblTitulo0.Text = "Relatório de Containeres Disponíveis";
                if (rdbDataLocacao.Checked)
                    Grade.DataSource = oLancamentos.PreencheDataComContaineresDisponiveis("a.Tipo");
                else if (rdbNumeroCaixa.Checked)
                    Grade.DataSource = oLancamentos.PreencheDataComContaineresDisponiveis("a.Numero");
                Grade.DataBind();
                lblTotalContaineres.Text = "Total Containeres: " + Grade.Rows.Count;
            }    
        }
    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string _script = "<script>window.location.href='../forms/Menu.aspx'</script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "", _script);
        }
    
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Relatorio();
        }
        private string GetAbsoluteUrl(string relativeUrl)
        {
            relativeUrl = relativeUrl.Replace("~/", string.Empty);
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }
    
                return url + relativeUrl;
            }
            return relativeUrl;
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
    
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
    
            Label lblEmpresa = new Label();
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();
    
            lblTitulo.ID = "lblTitulo";
            lblTitulo.Text = "Relatorio de Containeres Locados";
            lblEmBranco.ID = "lblEmBranco";
    
            string NomeArq = "Relatorio_Containeres.xls";
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
    
            lblTitulo.Text = "Relatorio de Containeres Locados";
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
            Image1.Visible = false;
        }
    }
}