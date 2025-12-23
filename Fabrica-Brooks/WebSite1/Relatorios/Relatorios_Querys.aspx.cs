using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.IO;

public partial class Relatorios_Querys : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    clsDB oDB = new clsDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "64");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("~/forms/sempermissao.aspx");

        if (oUsuario == null)
        {
            Response.Redirect("brooks/loginaplicativo.aspx", true);
        }
        if (!IsPostBack)
        {
            lblTitulo0.Text = "";
            CarregaRelatoriosGerador();
            MostraRelatorio();
            lblLinhas.Text = Grade.Rows.Count.ToString();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        
    }
    private void TituloRelatorio()
    {
        lblTitulo0.Text = "Relatório conforme consulta SQL";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == 0)
        {
            TituloRelatorio();
            //e.Row.Cells[2].Width = Unit.Pixel(400);
        }
    }
    private void Relatorio()
    {
        TituloRelatorio();
        if (txtQuery.Text != "")
        {
            Grade.DataSource = oDB.ConsultaSQL(txtQuery.Text);
            Grade.DataBind();
            lblLinhas.Text = Grade.Rows.Count.ToString();
        }
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Relatorio();
        clsGeradorRelatorio oGeradorRelatorio = new clsGeradorRelatorio();
        oGeradorRelatorio.NomeArquivo = txtSalvarComo.Text.Replace("'", "");
        oGeradorRelatorio.SqlConsulta = txtQuery.Text.Replace("'", "");
        clsGeradorRelatorioDados oGeradorRelatorioDado = new clsGeradorRelatorioDados();
        string _sResult = oGeradorRelatorioDado.RelatorioExiste(txtSalvarComo.Text);
        if (_sResult == "Incluir")
        {
            oGeradorRelatorioDado.Inserir(oGeradorRelatorio);
        }
        else if (_sResult == "Alterar")
        {
            oGeradorRelatorioDado.Alterar(oGeradorRelatorio, txtSalvarComo.Text);
        }
        CarregaRelatoriosGerador();
    }

    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        TituloRelatorio();
        if (txtQuery.Text != "")
        {
            Grade.DataSource = oDB.ConsultaSQL(txtQuery.Text);
            Grade.DataBind();
        }

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

        string NomeArq = "Relatorio_Consultas.xls";
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

        lblTitulo.Text = "Relatorio por consulta SQL";
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

    protected void ddlRelatorios_SelectedIndexChanged(object sender, EventArgs e)
    {
        MostraRelatorio();
        lblLinhas.Text = (Grade.Rows.Count).ToString();
    }

    private void MostraRelatorio()
    {
        txtQuery.Text = "";
        lblLinhas.Text = "0";
        clsGeradorRelatorio oGeradorRelatorio = new clsGeradorRelatorio();
        clsGeradorRelatorioDados oGeradorRelatorioDado = new clsGeradorRelatorioDados();
        oGeradorRelatorioDado.PegaDados(oGeradorRelatorio, ddlRelatorios.Text);
        if (oGeradorRelatorio.SqlConsulta != "")
        {
            txtSalvarComo.Text = oGeradorRelatorio.NomeArquivo;
            txtQuery.Text = oGeradorRelatorio.SqlConsulta;
            Grade.DataSource = new DataTable();
            Grade.DataBind();            
        }
    }

    private void CarregaRelatoriosGerador()
    {
        ddlRelatorios.Items.Clear();
        ddlRelatorios.Items.Add("");
        clsGeradorRelatorioDados oRelGeradoDados = new clsGeradorRelatorioDados();
        foreach (DataRow _dr in oRelGeradoDados.PreencheDataTable("NomeArquivo").Rows)
        {
            ddlRelatorios.Items.Add(_dr["NomeArquivo"].ToString());
        }
    }
}