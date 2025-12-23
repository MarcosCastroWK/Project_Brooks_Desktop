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
    
    
    public partial class RelatorioResiduos : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    
        clsUsuarios oUsuario = new clsUsuarios();
        clsResiduoDados oResiduosDados = new clsResiduoDados();
        DataTable _dt = new DataTable();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "30"); // criar codigo para residuos
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("~/forms/sempermissao.aspx");
    
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            lblTitulo0.Text = "";
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
                Grade.Width = Unit.Pixel(1400);
                lblTitulo0.Text = "Relatório de Resíduos"; 
            }
        }
        private void Relatorio()
        {
            lblTitulo0.Text = "Relatório de Resíduos";
            DataTable _dtnovo = new DataTable();
            _dtnovo.Columns.Add("Grupo");
            _dtnovo.Columns.Add("Residuo"); // descricao
            _dtnovo.Columns.Add("CodigoIBAMA");
            _dtnovo.Columns.Add("Classe");
            _dtnovo.Columns.Add("DestinoFinal");
            _dtnovo.Columns.Add("TecnologiaAplicada");
    
            _dt = oResiduosDados.PreencheDataTableResiduos("Grupo asc", "Ativos");
    
            int ix = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                _dtnovo.NewRow();
                _dtnovo.Rows.Add();
                _dtnovo.Rows[ix]["Grupo"] = dr["Grupo"];
                _dtnovo.Rows[ix]["Residuo"] = dr["Descricao"];
                _dtnovo.Rows[ix]["CodigoIBAMA"] = dr["CodigoIBAMA_Analitico"];
                _dtnovo.Rows[ix]["Classe"] = dr["Classe"];
                _dtnovo.Rows[ix]["DestinoFinal"] = dr["DestinoFinal"];
                _dtnovo.Rows[ix]["TecnologiaAplicada"] = dr["TecnologiaAplicada"];
                ix++;
            }
            try
            {
                Grade.DataSource = _dtnovo;
                Grade.DataBind();
            }
            finally
            {
                Grade.Rows[0].Cells[0].Width = Unit.Pixel(300);
                Grade.Rows[0].Cells[1].Width = Unit.Pixel(300);
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
            lblTitulo.Text = "Relatorio de Residuos";
            lblEmBranco.ID = "lblEmBranco";
    
            string NomeArq = "Relatorio_Residuos.xls";
            Grade.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade.EnableViewState = false;
            
            for (int i = 0; i < Grade.HeaderRow.Cells.Count; i++)
                Grade.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade.HeaderRow.Cells[i].Text);
            Grade.HeaderRow.Cells[0].Text = "Código"; 
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
    
            lblTitulo.Text = "Relatorio de Resíduos";
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