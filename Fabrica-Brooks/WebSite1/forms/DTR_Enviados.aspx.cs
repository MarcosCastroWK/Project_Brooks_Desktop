using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.IO;
public partial class DTR_Enviados : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsDTR oDTR = new clsDTR();
    clsDTRDados oDTRDados = new clsDTRDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    bool bImprimir = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["oUsuario"] == null)
        {
            menu _menu = (menu)FindControl("menu1");
            oUsuario.Codigo = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigo")).Value);
            oUsuario.CodigoEmpresa = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigoEmpresa")).Value);
            oUsuario.Nome = ((HiddenField)_menu.FindControl("hifNome")).Value;
            oUsuario.Aplicativo = false;
            Session["oUsuario"] = oUsuario;
        }
        else if (Session["oUsuario"] != null)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
        }
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "28");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("sempermissao.aspx");
        if (!IsPostBack)
        {
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                try
                {
                    geral.Ordem = "";
                    if (oUsuario.Aplicativo == true)
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    txtDataInicial.Data = DateTime.Now.AddDays(-29).ToString("dd/MM/yyyy");
                    txtDataFinal.Data = DateTime.Now.ToString("dd/MM/yyyy");
                    if (geral.Ordem == "")
                        geral.Ordem = "DataColeta desc";
                    Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
                    Grade.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
    }
    private static void MessageBox(Page _page, string Message)
    {
        _page.ClientScript.RegisterStartupScript
        (
            _page.GetType(),
            "MessageBox",
            "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }

    private static void AbreDDR_Relatorio(Page _page, string pPagina)
    {
        _page.ClientScript.RegisterStartupScript
        (
            _page.GetType(),
            "MessageBox",
            "<script language='javascript'>window.open('" + pPagina + "');</script>"
        );
    }
    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        bImprimir = false;
        lblMensagem.Text = "";
        btnConfirma.Visible = true;
    }
    
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {
        bImprimir = false;
        lblTitulo.Text = "&nbsp;Alteração DTR - Enviados";
        lblMensagem.Text = "";
        btnConfirma.Visible = false;
    }

    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial"  && e.CommandArgument.ToString() != "DataColeta" &&
            e.CommandArgument.ToString() != "NomeCliente" && e.CommandArgument.ToString() != "Residuo" &&
            e.CommandArgument.ToString() != "Quantidade"  && e.CommandArgument.ToString() != "Imprimido" &&
            e.CommandArgument.ToString() != "DataSaida"   && e.CommandArgument.ToString() != "DestinoFinal" &&
            e.CommandArgument.ToString() != "Imprimido"   && e.CommandArgument.ToString() != "Lote" &&
            e.CommandArgument.ToString() != "Unidade"     && e.CommandArgument.ToString() != "NumeroImpressao")
        {
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifNumeroLancamento.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[12].Text.Split("-"[0])[0];
            hifNumeroMTR.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[12].Text.Split("-"[0])[1];
            hifCodigoResiduo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[12].Text.Split("-"[0])[2];
            if (bImprimir)
                Imprime();
        }
    }
    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ImageButton x = new ImageButton();
            ImageButton y = new ImageButton();
            x = (ImageButton)e.Row.Cells[0].FindControl("ibnMudar");
            y = (ImageButton)e.Row.Cells[1].FindControl("ibnExcluir");
            if (e.Row.Cells[3].Text != "0")
            {
                x.Visible = true;
                y.Visible = true;
                hifCodigo.Value = e.Row.Cells[3].Text;
                lblLote0.Text = hifCodigo.Value;
            }
            else
            {
                x.Visible = false;
                y.Visible = false;
            }
            if (e.Row.Cells[11].Text == "1")
                e.Row.Cells[11].Text = "Sim";
            else
                e.Row.Cells[11].Text = "";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        bImprimir = false;
        btnConfirma.Visible = false;
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        bImprimir = false;
        btnConfirma.Visible = false;
        if (geral.Ordem == "")
            geral.Ordem = "d.DataSaida asc";
        DataTable _dt = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, txtDataInicial.Data, txtDataFinal.Data);
        if (_dt.Rows.Count > 0)
        {
            Grade.DataSource = _dt;
            Grade.DataBind();
        }
        else
        {
            Grade.DataSource = new DataTable();
            Grade.DataBind();
        }
    }
    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        bImprimir = false;
        btnConfirma.Visible = false;
        if (hifCodigo.Value != "")
        {
            int Seq = Convert.ToInt32(hifCodigo.Value);
            Seq = Seq - 1;
            hifCodigo.Value = Seq.ToString();
            if (Seq > 0)
                Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, Seq, "", "");
            else
                Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
            Grade.DataBind();
        }
        else
        {
            Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
            Grade.DataBind();
        }
    }
    protected void btnProximo_Click(object sender, EventArgs e)
    {
        bImprimir = false;
        btnConfirma.Visible = false;
        if (hifCodigo.Value != "")
        {
            int Seq = Convert.ToInt32(hifCodigo.Value);
            Seq = Seq + 1;
            if (Seq <= oDTRDados.UltimoRegistro())
                Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, Seq, "", "");
            else
                Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 1, "", "");
            Grade.DataBind();
        }
        else
        {
            Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
            Grade.DataBind();
        }
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
        if (hifCodigo.Value != "")
        {
            Image1.Visible = true;
            Image1.ImageUrl = this.GetAbsoluteUrl(Image1.ImageUrl).Replace("/forms", "");

            Table table = new Table();
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Height = 100;
            row.Cells[0].Controls.Add(Image1);
            table.Rows.Add(row);

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            int Seq = Convert.ToInt32(hifCodigo.Value);
            _dt = oDTRDados.PreencheDTExcel(oDTR, Seq);
            DataTable _dt2 = new DataTable();
            for (int i = 0; i < _dt.Columns.Count; i++)
                _dt2.Columns.Add(_dt.Columns[i].ToString());

            DataRow _dr2 = _dt2.NewRow();
            if (_dt.Rows.Count > 0)
            {
                string sDescResiduo = _dt.Rows[0]["Residuo"].ToString();
                decimal TotalPesoResiduo = 0;
                foreach (DataRow _dr in _dt.Rows)
                {                    
                    if (sDescResiduo != _dr["Residuo"].ToString())
                    {
                        // linha em branco
                        _dr2 = _dt2.NewRow();
                        _dr2[0] = "  ";
                        _dt2.Rows.Add(_dr2);
                    }
                    _dr[0] = geral.RetiraLetras(_dr[0].ToString());
                    _dr2 = _dt2.NewRow();
                    for (int i = 0; i < _dt.Columns.Count; i++)
                    {
                        _dr2[i] = _dr[i];
                        if (i == _dt.Columns.Count - 1)
                        {
                            TotalPesoResiduo = oDTRDados.PegaTotalResiduo(Seq, _dr["Residuo"].ToString());
                            if (TotalPesoResiduo > 0)
                                _dr2[i] =  (Convert.ToDecimal(_dr["Quantidade"]) * 100 / TotalPesoResiduo).ToString("N2");
                        }
                    }
                    _dt2.Rows.Add(_dr2);
                    // para diferenciar o resíduo
                    sDescResiduo = _dr["Residuo"].ToString();
                }
            }
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();
            
            lblTitulo.ID = "lblTitulo";
            lblTitulo.Text = "DTR - Deposito Temporario de Residuos";
            lblEmBranco.ID = "lblEmBranco";
             
            GridView dg = new GridView();
            dg.GridLines = GridLines.Vertical;
            dg.AutoGenerateColumns = true;
            dg.DataSource = _dt2;
            dg.DataBind();

            string NomeArq = "DTRExcel.xls";
            dg.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            dg.EnableViewState = false;           
            
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
            row.Cells[0].Controls.Add(dg);
            table.Rows.Add(row);

            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
            Image1.Visible = false;
        }
    }

    protected void ibnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        // imprimir igual do dtr - armazenados
        bImprimir = true;
        btnConfirma.Visible = false;
    }

    private void Imprime()
    {         
        // pegar novos dados e continuar imprimindo
        DataTable _dtRelacao = new DataTable();
        _dtRelacao.Columns.Add("DataColeta");
        _dtRelacao.Columns.Add("CodigoCliente");
        _dtRelacao.Columns.Add("CNPJ_CPF");
        _dtRelacao.Columns.Add("Gerador");
        _dtRelacao.Columns.Add("PesoKg");
        _dtRelacao.Columns.Add("Percentual");
        _dtRelacao.Columns.Add("NumeroMTRe");
        _dtRelacao.Columns.Add("DescricaoResiduo");
        _dtRelacao.Columns.Add("DataSaida");
        _dtRelacao.Columns.Add("LocalEntrega");
        _dtRelacao.Columns.Add("CodigoResiduo");
        _dtRelacao.Columns.Add("Sequencial");
        _dtRelacao.Columns.Add("NumeroLancamento");
        _dtRelacao.Columns.Add("Motorista");
        _dtRelacao.Columns.Add("Placas");
        _dtRelacao.Columns.Add("NumeroMTR");

        clsResiduoDados oResiduoDado = new clsResiduoDados();
        int _NumeroLancamento = 0;
        int _CodigoResiduo = 0;
        int _CodigoCliente = 0;
        string _DescricaoResiduo = "";
        string _NumeroImpressao = "";
        
        // pega o número da impressão, pois têm que ser a mesma
        if (Grade.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in Grade.Rows)
            {
                if (hifCodigoResiduo.Value == gvr.Cells[12].Text.Split("-"[0])[2].ToString() && hifCodigo.Value == gvr.Cells[3].Text)
                {
                    _NumeroImpressao = gvr.Cells[13].Text;
                    break;
                }
            }
        }
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();

        // adiciona todos do primeiro selecionado 
        foreach (GridViewRow gvr in Grade.Rows)
        {
            _CodigoResiduo = Convert.ToInt32(gvr.Cells[12].Text.Split("-"[0])[2]);
            if (hifCodigoResiduo.Value == _CodigoResiduo.ToString() && _NumeroImpressao == gvr.Cells[13].Text && hifCodigo.Value == gvr.Cells[3].Text)
            {
                _CodigoCliente = Convert.ToInt32(gvr.Cells[12].Text.Split("-"[0])[3]);
                oCliente = oClienteDados.PegaDados(oCliente, _CodigoCliente);
                _dtRelacao.NewRow();
                _dtRelacao.Rows.Add();
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataColeta"] = gvr.Cells[4].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoCliente"] = Convert.ToInt32(gvr.Cells[12].Text.Split("-"[0])[3]);
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CNPJ_CPF"] = oCliente.CNPJ_CPF;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Gerador"] = geral.Left(oCliente.Nome, 58);
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["PesoKg"] = gvr.Cells[7].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Percentual"] = 0; // ok
                clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
                _NumeroLancamento = Convert.ToInt32(gvr.Cells[12].Text.Split("-"[0])[0]);
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroMTRe"] = oLancMTRDados.RetornaNumeroMTRe(_NumeroLancamento, _CodigoResiduo);
                _CodigoResiduo = Convert.ToInt32(gvr.Cells[12].Text.Split("-"[0])[2]);
                _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DescricaoResiduo"] = _DescricaoResiduo;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataSaida"] = gvr.Cells[9].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["LocalEntrega"] = gvr.Cells[10].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoResiduo"] = _CodigoResiduo.ToString();
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Sequencial"] = gvr.Cells[12].Text.Split("-"[0])[4];
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroLancamento"] = _NumeroLancamento.ToString();
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Motorista"] = gvr.Cells[14].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Placas"] = gvr.Cells[15].Text;
                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroMTR"] = gvr.Cells[12].Text.Split("-"[0])[1];
            }
        }

        Session["dtRelacaoDTR"] = _dtRelacao;
        Session["NaoSalvarRelacaoDTR"] = "Sim";
        try
        {
            Response.Write("<script>window.open('MTR.aspx', '_blank');</script>");
        }
        finally
        {
            Response.Write("<script>window.open('DTRRelacaoResiduos.aspx', '_blank');</script>");
        }
    }

    protected void btnConfirma_Click(object sender, EventArgs e)
    {
        // salvar Fechado = 0, desta forma retorna para armazenados
        oDTRDados.SalvarComoAberto(hifCodigo.Value, hifNumeroLancamento.Value, hifCodigoResiduo.Value);

        btnConfirma.Visible = false;
        Grade.DataSource = oDTRDados.PreencheDataTableEnviados(geral.Ordem, 0, "", "");
        Grade.DataBind();
    }
}