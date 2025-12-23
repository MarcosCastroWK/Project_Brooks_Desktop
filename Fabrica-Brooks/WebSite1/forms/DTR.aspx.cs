using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
public partial class DTR : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsDTR oDTR = new clsDTR();
    clsDTRDados oDTRDados = new clsDTRDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
    string pCodigoResiduo = "";
    private DataTable _dt = new DataTable();
    private decimal dTotalPeso = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CodigoResiduo"] != "" && Request.QueryString["CodigoResiduo"] != null)
            pCodigoResiduo = Request.QueryString["CodigoResiduo"].ToString();

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
        CarregaDestinoFinal(pddlDestinoFinal);
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
                    if (oUsuario.Aplicativo == true)
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    datDataInicial.Data = DateTime.Now.AddDays(-200).ToShortDateString();

                    btnEnviar.Enabled = false;
                    if (oUsuario.Nome.ToLower() != "cris")
                    {
                        btnMTRImprimir.Enabled = false;
                        btnEnviar.Enabled = false;
                        Salvar.Enabled = false;
                    }
                    geral.Ordem = "Residuo, DataColeta";
                    _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, Convert.ToDateTime(datDataInicial.Data).ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
                    Session["dtArmazenados"] = _dt;
                    Grade.DataSource = _dt;
                    Grade.DataBind();

                    lblLote.Text = "Lote: " + (oDTRDados.UltimoRegistro() + 1).ToString();
                    hifLinhasGrade.Value = Grade.Rows.Count.ToString();

                    if (Request.QueryString["Continuar"] == "1")
                    {
                        btnMTRImprimir_Click(new object(), EventArgs.Empty);
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
    }
    private void SalvarLog(string pOperacao, clsDTR pDTR)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "DTR";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Sequencial: " + pDTR.Sequencial + " ";
        oLog.Log = oLog.Log + "Data Coleta: " + pDTR.DataColeta + " ";
        oLog.Log = oLog.Log + "Código Cliente: " + pDTR.CodigoClienteColetado + " ";
        oLog.Log = oLog.Log + "Cliente: " + pDTR.ClienteColetado + " \n";
        oLog.Log = oLog.Log + "Código resíduo: " + pDTR.CodigoTipoResiduo + " \n";
        oLog.Log = oLog.Log + "Resíduo: " + pDTR.TipoResiduo + " \n";        
        oLog.Log = oLog.Log + "Data Saída: " + pDTR.DataSaida + " \n";
        oLog.Log = oLog.Log + "Local de entrega: " + pDTR.LocalEntrega + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Salvar") // Confirma inclusão e alteração de linha
        {
            // varrer linhas e ver se foi colocada DataSaida e ou LocalEntrega, desta forma salvar uma nova linha na tabela DTR, caso não exista, 
            // verificar se a linha existe na tabela DTR, mas se DataSaida e LocalEntrega, nulos excluir
            foreach (GridViewRow _gdr in Grade.Rows)
            {
                oDTR.NumeroLancamento = Convert.ToInt32(_gdr.Cells[8].Text.Split("-"[0])[0]);
                oDTR.NumeroMTR = Convert.ToInt32(_gdr.Cells[8].Text.Split("-"[0])[1]);
                oDTR.CodigoTipoResiduo = Convert.ToInt32(_gdr.Cells[8].Text.Split("-"[0])[2]);
                oDTR.CodigoClienteColetado = Convert.ToInt32(_gdr.Cells[8].Text.Split("-"[0])[3]);
                oDTR.Sequencial = Convert.ToInt32(_gdr.Cells[8].Text.Split("-"[0])[4]);
                if (oDTR.Sequencial <= 0)
                {
                    oDTR.Sequencial = Convert.ToInt32(lblLote.Text.Split(":"[0])[1].Trim());
                }
                oDTR.DataColeta = _gdr.Cells[0].Text;

                clsClienteDados oClienteDado = new clsClienteDados();
                oDTR.ClienteColetado = oClienteDado.PegaNomeFantasia(oDTR.CodigoClienteColetado);

                clsResiduoDados oResiduoDado = new clsResiduoDados();
                oDTR.TipoResiduo = oResiduoDado.PegaDescricao(oDTR.CodigoTipoResiduo);

                if (_gdr.Cells[3].Text != "")
                    oDTR.TotalKg = Convert.ToDecimal(_gdr.Cells[3].Text);
                oDTR.LocalDTR = _gdr.Cells[5].Text;
                oDTR.DataSaida = ((forms_DATA)_gdr.Cells[6].FindControl("datDataSaida")).Data;
                oDTR.LocalEntrega = ((DropDownList)_gdr.Cells[7].FindControl("ddlDestinoFinal")).Text;
                TextBox _txtImprimido = new TextBox();
                _txtImprimido = (TextBox)_gdr.Cells[10].FindControl("txtImprimido");
                oDTR.Imprimido = 0;
                if (_txtImprimido.Text == "I")
                {
                    oDTR.Imprimido = 1;
                }
                if (oDTRDados.DadoExiste(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR) == "Incluir")
                {
                    if (oDTR.DataSaida != "" || oDTR.LocalEntrega != "")
                    {
                        oDTRDados.Inserir(oDTR);
                        lblMensagem.Text = "DTR - Armazenado inserido com sucesso!";
                    }
                }
                else
                {
                    if (oDTR.DataSaida == "" && oDTR.LocalEntrega == "")
                    {
                        SalvarLog("Exclusão", oDTR);
                        // excluir linha
                        oDTRDados.Excluir(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.NumeroMTR, oDTR.CodigoTipoResiduo);
                        lblMensagem.Text = "DTR - Data e Local de Entrega excluídos com sucesso!";
                    }
                    else
                    {
                        SalvarLog("Alteração", oDTR);
                        // alterar data saida e ou local entrega
                        oDTRDados.AlterarArmazenados(oDTR, oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.NumeroMTR, oDTR.CodigoTipoResiduo);
                        lblMensagem.Text = "DTR - Data e Local de Entrega alterados com sucesso!";
                    }
                }
            }
        }
        lblTitulo.Text = "&nbsp;DTR - Armazendados";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Salvar";
        btnEnviar.Enabled = false;
        if (geral.Ordem == "")
            geral.Ordem = "Residuo, DataColeta";
        _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, Convert.ToDateTime(datDataInicial.Data).ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
        Session["dtArmazenados"] = _dt;
        Grade.DataSource = _dt;
        Grade.DataBind();
        lblLote.Text = "Lote: " + (oDTRDados.UltimoRegistro() + 1).ToString();
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
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "DataColeta" && e.CommandArgument.ToString() != "" &&
            e.CommandArgument.ToString() != "NomeCliente" && e.CommandArgument.ToString() != "Residuo" &&
            e.CommandArgument.ToString() != "Quantidade" && e.CommandArgument.ToString() != "NumeroImpressao" &&
            e.CommandArgument.ToString() != "DataSaida" && e.CommandArgument.ToString() != "DestinoFinal" &&
            e.CommandArgument.ToString() != "Unidade" && e.CommandArgument.ToString() != "UN" &&
            e.CommandArgument.ToString() != "DTR" && e.CommandArgument.ToString() != "Lote")
        {
            hifCodigo.Value = "0";
            hifNumeroLancamento.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text.Split("-"[0])[0];
            hifNumeroMTR.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text.Split("-"[0])[1];
            hifCodigoResiduo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text.Split("-"[0])[2];
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
            forms_DATA _datDataSaida = (forms_DATA)e.Row.Cells[5].FindControl("datDataSaida");
            _datDataSaida.Data = geral.DataFormatada(_datDataSaida.Data);

            DropDownList _ddlDestinoFinal = (DropDownList)e.Row.Cells[6].FindControl("ddlDestinoFinal");
            for (int i = 0; i < pddlDestinoFinal.Items.Count; i++)
                _ddlDestinoFinal.Items.Add(pddlDestinoFinal.Items[i].Text);
            _ddlDestinoFinal.Text = ((HiddenField)e.Row.Cells[6].FindControl("hifDestinoFinal")).Value;

            Label _lblSeq = new Label();
            _lblSeq = (Label)e.Row.Cells[9].FindControl("lblSeq");
            if (_lblSeq != null)
                _lblSeq.Text = (e.Row.RowIndex + 1).ToString();

            TextBox _txtImprimido = new TextBox();
            _txtImprimido = (TextBox)e.Row.Cells[10].FindControl("txtImprimido");
            if (_txtImprimido.Text == "1")
            {
                _txtImprimido.Text = "I";
                btnEnviar.Enabled = true;
            }
            else if (_txtImprimido.Text != "1")
                _txtImprimido.Text = "";

            if (oUsuario.Nome.ToLower() != "cris")
            {
                _lblSeq.Visible = false;
                _txtImprimido.Enabled = false;
                _datDataSaida.Enabled = false;
                _ddlDestinoFinal.Enabled = false;
            }
            
            // TotalPeso
            if (e.Row.Cells[3].Text != "&nbsp;" && e.Row.Cells[3].Text != "" && e.Row.Cells[3].Text != null)
            {
                dTotalPeso = dTotalPeso + Convert.ToDecimal(e.Row.Cells[3].Text);
                lblTotal.Text = "Total quantidade: " + dTotalPeso.ToString("N2");
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
        btnEnviar.Enabled = false;
        if (datDataInicial.Data == "" || datDataInicial.Data == null)
            datDataInicial.Data = "01/01/2021";
        DataTable _dtOrdenado = new DataTable();
        if (Session["dtArmazenados"] != null && _dt.Rows.Count == 0)
        {
            try
            {
                _dt = (DataTable)Session["dtArmazenados"];
                DropDownList _ddl = new DropDownList();
                forms_DATA _data = new forms_DATA();
                int i = 0;
                foreach (GridViewRow gvr in Grade.Rows)
                {
                    DataRow dr = _dt.Rows[i++];
                    _ddl = new DropDownList();
                    _data = new forms_DATA();
                    _ddl = (DropDownList)gvr.Cells[7].FindControl("ddlDestinoFinal");
                    if (_ddl.Text != null && _ddl.Text != "")
                        dr["DestinoFinal"] = _ddl.Text;
                    _data = (forms_DATA)gvr.Cells[6].FindControl("datDataSaida");
                    if (_data.Data != null && _data.Data != "")
                        dr["DataSaida"] = _data.Data;
                }

                DataRow[] _drr = _dt.Select("", geral.Ordem);
                foreach (DataColumn dc in _dt.Columns)
                {
                    _dtOrdenado.Columns.Add(dc.ColumnName, dc.DataType);
                }
                foreach (DataRow dr in _drr)
                {
                    _dtOrdenado.NewRow();
                    _dtOrdenado.Rows.Add();
                    foreach (DataColumn dc in _dtOrdenado.Columns)
                    {
                        _dtOrdenado.Rows[_dtOrdenado.Rows.Count - 1][dc.ColumnName] = dr[dc.ColumnName];
                    }
                }
                Session["dtArmazenados"] = _dtOrdenado;
            }
            catch (Exception ex)
            {
                _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, Convert.ToDateTime(datDataInicial.Data).ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
                Session["dtArmazenados"] = _dt;
                Grade.DataSource = _dt;
                Grade.DataBind();
            }
        }

        Grade.DataSource = _dtOrdenado;
        Grade.DataBind();

        lblLote.Text = "&nbsp;Lote: " + (oDTRDados.UltimoRegistro() + 1).ToString();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(datDataInicial.Data).Year < DateTime.Now.Year - 5)
        {
            lblMensagem.Text = "Período inicial superior a 5 anos!";
        }
        else
        {
            lblMensagem.Text = "";
            if (geral.Ordem == "")
                geral.Ordem = "Residuo, DataColeta";
            btnEnviar.Enabled = false;
            _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, Convert.ToDateTime(datDataInicial.Data).ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
            Session["dtArmazenados"] = _dt;
            Grade.DataSource = _dt;
            Grade.DataBind();
            lblLote.Text = "&nbsp;Lote: " + (oDTRDados.UltimoRegistro() + 1).ToString();
        }
    }
    private void CarregaDestinoFinal(DropDownList pddl)
    {
        oDestinoFinalDados = new clsDestinoFinalDados();
        pddl.Items.Clear();
        pddl.Items.Add("");
        foreach (DataRow dr in oDestinoFinalDados.PreencheDataTableAterro("Codigo").Rows)
        {
            pddl.Items.Add(dr["NomeFantasia"].ToString());
        }
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

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        int _NumeroLancamento = 0;
        int _CodigoResiduo = 0;
        int _Sequencial = 0;
        string _NumeroMTR = "";
        string _CodigoDestino = "";
        // salvar como fechado para aparecer nos enviados e não apareceber mais aqui
        // tirar de lancamentomtr do deposito para destino enviado.
        foreach (GridViewRow gvr in Grade.Rows)
        {
            _NumeroLancamento = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[0]);
            _NumeroMTR = gvr.Cells[8].Text.Split("-"[0])[1];
            _CodigoResiduo = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[2]);
            _Sequencial = Convert.ToInt32(gvr.Cells[8].Text.Split("-"[0])[4]);
            _CodigoDestino = oDestinoFinalDados.PegaCodigo(((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text);

            if (((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data != "" && ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text != "" && 
                ((TextBox)gvr.Cells[10].FindControl("txtImprimido")).Text == "I")
            {
                oDTRDados.SalvarComoFechado(_Sequencial, _NumeroLancamento, _CodigoResiduo, ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text, _CodigoDestino, _NumeroMTR);
                clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                oLancamentoMTRDados.SalvarDestinoDataDescarga(_NumeroLancamento, Convert.ToInt32(_NumeroMTR), _CodigoResiduo,
                                                              ((forms_DATA)gvr.Cells[5].FindControl("datDataSaida")).Data,
                                                              Convert.ToInt32(_CodigoDestino), ((DropDownList)gvr.Cells[6].FindControl("ddlDestinoFinal")).Text);
            }
        }
        if (geral.Ordem == "")
            geral.Ordem = "Residuo, DataColeta";
        btnEnviar.Enabled = false;
        _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, Convert.ToDateTime(datDataInicial.Data).ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
        Session["dtArmazenados"] = _dt;
        Grade.DataSource = _dt;
        Grade.DataBind();
        lblLote.Text = "&nbsp;Lote: " + (oDTRDados.UltimoRegistro() + 1).ToString();
    }
}