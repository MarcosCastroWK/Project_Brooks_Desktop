using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class MovimentacaoDTR : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsDTR oDTR = new clsDTR();
    clsUsuarios oUsuario = new clsUsuarios();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();

    private clsMovimentacaoDTR oMovDTR = new clsMovimentacaoDTR();
    private clsMovimentacaoDTRDados oMovDTRdados = new clsMovimentacaoDTRDados();
    private clsCacambaDados oContainerDados = new clsCacambaDados();
    string pNumeroLancamento = "";
    string pCodigoResiduo = "";
    string pCodigoCliente = "";
    string pContainerLocal = "";

    private DataTable _dt = new DataTable();
    private decimal dTotalPeso = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NumeroLancamento"] != "" && Request.QueryString["NumeroLancamento"] != null)
            pNumeroLancamento = Request.QueryString["NumeroLancamento"].ToString();
        if (Request.QueryString["CodigoResiduo"] != "" && Request.QueryString["CodigoResiduo"] != null)
            pCodigoResiduo = Request.QueryString["CodigoResiduo"].ToString();
        if (Request.QueryString["CodigoCliente"] != "" && Request.QueryString["CodigoCliente"] != null)
            pCodigoCliente = Request.QueryString["CodigoCliente"].ToString();
        if (Request.QueryString["Local"] != "" && Request.QueryString["Local"] != null)
            pContainerLocal = Request.QueryString["Local"].ToString();

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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "27");
        if (oItensMenuPermissoes.Consultar == 0)
            Response.Redirect("sempermissao.aspx");
        if (!IsPostBack)
        {
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
            }
            if (oUsuario == null && Request.QueryString["Continuar"] != "1")
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                try
                {
                    menu1.Visible = false;

                    CarregaContaineres(ddlContaineres);
                    geral.Ordem = "Data";
                    _dt = oMovDTRdados.PreencheDataTableMovimentacaoDTR(geral.Ordem, pNumeroLancamento, pCodigoResiduo, pCodigoCliente);
                    NovaLinhaParaDt();
                    Grade.DataSource = _dt;
                    Grade.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
    }
    private void NovaLinhaParaDt()
    {
        _dt.NewRow();
        _dt.Rows.Add();
        _dt.Rows[_dt.Rows.Count - 1]["Codigo"] = 0;
        _dt.Rows[_dt.Rows.Count - 1]["Data"] = DateTime.Now.ToString("dd/MM/yyyy");
        _dt.Rows[_dt.Rows.Count - 1]["MoviCxDe"] = pContainerLocal;
        _dt.Rows[_dt.Rows.Count - 1]["MoviCxPara"] = "";
        _dt.Rows[_dt.Rows.Count - 1]["NumeroLancamento"] = pNumeroLancamento;
        _dt.Rows[_dt.Rows.Count - 1]["CodigoResiduo"] = pCodigoResiduo;
        _dt.Rows[_dt.Rows.Count - 1]["CodigoCliente"] = pCodigoCliente;
    }

    private void CarregaContaineres(DropDownList pddlContaineres)
    {
        pddlContaineres.Items.Clear();
        pddlContaineres.Items.Add("");
        foreach (DataRow dr in oContainerDados.PreencheDataTableCacambas("Numero").Rows)
        {
            pddlContaineres.Items.Add(dr["Numero"].ToString());
        }
    }
    private void SalvarLog(string pOperacao, clsMovimentacaoDTR pMovimentoDTR)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Movimentação DTR";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Codigo: " + pMovimentoDTR.Codigo + " \n";
        oLog.Log = oLog.Log + "Data .: " + pMovimentoDTR.Data + " \n";
        oLog.Log = oLog.Log + "NumeroLancamento: " + pMovimentoDTR.NumeroLancamento + " \n";
        oLog.Log = oLog.Log + "Código Cliente .: " + pMovimentoDTR.CodigoCliente + " \n";
        oLog.Log = oLog.Log + "Código Resíduo .: " + pMovimentoDTR.CodigoResiduo + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        GridViewRow _gdr = Grade.Rows[Grade.Rows.Count - 1];
        oMovDTR.MoviCxPara = ((DropDownList)_gdr.Cells[2].FindControl("ddlMoviCxPara")).Text;
        if (Salvar.Text == "Salvar" && oMovDTR.MoviCxPara != "" && oMovDTR.MoviCxPara != "&nbsp;") // Confirma inclusão e alteração de linha
        {
            oMovDTR.Data = DateTime.Now.ToString("dd/MM/yyyy");
            oMovDTR.MoviCxDe = ((DropDownList)_gdr.Cells[1].FindControl("ddlMoviCxDe")).Text;
            oMovDTR.NumeroLancamento = Convert.ToInt32(_gdr.Cells[3].Text);
            oMovDTR.CodigoResiduo = Convert.ToInt32(_gdr.Cells[4].Text);
            oMovDTR.CodigoCliente = Convert.ToInt32(_gdr.Cells[5].Text);
            oMovDTRdados.Inserir(oMovDTR);

            Response.Redirect("DTR.aspx?CodigoResiduo=" + oMovDTR.CodigoResiduo);

        }
        else
        {
            lblTitulo.Text = "&nbsp;Movimentação DTR";
            lblMensagem.Text = "Container inválido!";
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
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            DropDownList _ddlDe = (DropDownList)e.Row.Cells[1].FindControl("ddlMoviCxDe");
            foreach (ListItem liDe in ddlContaineres.Items)
                _ddlDe.Items.Add(liDe.Text);
            HiddenField _hifDe = (HiddenField)e.Row.Cells[1].FindControl("hifMoviCxDe");
            _ddlDe.Text = _hifDe.Value;

            DropDownList _ddlPara = (DropDownList)e.Row.Cells[2].FindControl("ddlMoviCxPara");
            foreach (ListItem liPara in ddlContaineres.Items)
                _ddlPara.Items.Add(liPara.Text);
            HiddenField _hifPara = (HiddenField)e.Row.Cells[2].FindControl("hifMoviCxPara");
            _ddlPara.Text = _hifPara.Value;

            _ddlDe.Enabled = false;
            _ddlPara.Enabled = false;
            if ((_dt.Rows.Count - 1) == e.Row.RowIndex)
            {
                _ddlPara.Enabled = true;
            }
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == "DTR")
            e.SortExpression = "LocalDTR";
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        DataTable _dtOrdenado = new DataTable();

        Grade.DataSource = _dtOrdenado;
        Grade.DataBind();
    }

    protected void btnMTRImprimir_Click(object sender, EventArgs e)
    {
        Salvar_Click(new object(), EventArgs.Empty);
        //pegar primeiro residuo com a mesma descricao, data saida, destino. Montar o relatório e verificar se tem mais e continuar imprimindo, caso usuario desejar
        string _ResiduoDataSaidaLocalEntrega = "";
        string _ResiduoDataSaidaLocalEntregaAnterior = "0";

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

        clsResiduoDados oResiduoDado = new clsResiduoDados();
        int _NumeroLancamento = 0;
        int _CodigoResiduo = 0;
        int _CodigoCliente = 0;
        string _DescricaoResiduo = "";
        bool _EncontrouItemsParaImprimir = false;
        // pegar o primeiro residuo com datasaida e local entrega
        foreach (GridViewRow gvr in Grade.Rows)
        {
            _CodigoResiduo = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[2]);
            _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
            _ResiduoDataSaidaLocalEntrega = _DescricaoResiduo + "-" + ((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data + "-" + ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text;
            if (((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data != "" && ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text != "" &&
                _ResiduoDataSaidaLocalEntregaAnterior != _ResiduoDataSaidaLocalEntrega && ((TextBox)gvr.Cells[10].FindControl("txtImprimido")).Text != "I")
            {
                _EncontrouItemsParaImprimir = true;
                break;
            }

        }
        if (_EncontrouItemsParaImprimir)
        {
            clsClientes oCliente = new clsClientes();
            clsClienteDados oClienteDados = new clsClienteDados();
            // adiciona todos do primeiro selecionado 
            foreach (GridViewRow gvr in Grade.Rows)
            {
                _NumeroLancamento = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[0]);
                _CodigoResiduo = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[2]);
                _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                if (_ResiduoDataSaidaLocalEntrega == (_DescricaoResiduo + "-" + ((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data + "-" + ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text))
                {
                    _CodigoCliente = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[3]);
                    oCliente = oClienteDados.PegaDados(oCliente, _CodigoCliente);
                    _dtRelacao.NewRow();
                    _dtRelacao.Rows.Add();
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataColeta"] = gvr.Cells[0].Text;
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoCliente"] = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[3]);
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CNPJ_CPF"] = oCliente.CNPJ_CPF;
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Gerador"] = geral.Left(oCliente.Nome, 58);
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["PesoKg"] = gvr.Cells[3].Text;
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Percentual"] = 0; // ok
                    clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroMTRe"] = oLancMTRDados.RetornaNumeroMTRe(_NumeroLancamento, _CodigoResiduo);
                    _CodigoResiduo = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[2]);
                    _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DescricaoResiduo"] = _DescricaoResiduo;
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataSaida"] = geral.DataFormatada(((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data);
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["LocalEntrega"] = ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text;
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoResiduo"] = _CodigoResiduo.ToString();
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Sequencial"] = gvr.Cells[8].Text.Split("-"[0])[4];
                    _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroLancamento"] = _NumeroLancamento.ToString();
                }
            }
            Session["dtRelacaoDTR"] = _dtRelacao;
            Session["NaoSalvarRelacaoDTR"] = null; // salvar

            try
            {
                Response.Write("<script>window.open('MTR.aspx', '_blank');</script>");
                _ResiduoDataSaidaLocalEntregaAnterior = _ResiduoDataSaidaLocalEntrega;
                // pegar o primeiro residuo com datasaida e local entrega
                foreach (GridViewRow gvr in Grade.Rows)
                {
                    if (_ResiduoDataSaidaLocalEntregaAnterior != _ResiduoDataSaidaLocalEntrega)
                    {
                        break;
                    }
                    _CodigoResiduo = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[2]);
                    _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                    _ResiduoDataSaidaLocalEntrega = _DescricaoResiduo + "-" + ((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data + "-" + ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text;
                }
            }
            finally
            {
                Response.Write("<script>window.open('DTRRelacaoResiduos.aspx', '_blank');</script>");
                Response.Write("<script>window.close();</script>");
            }
        }
        else if (!_EncontrouItemsParaImprimir)
        {
            Response.Write("<script>alert('Não há itens selecionados!');</script>");
        }
    }
}