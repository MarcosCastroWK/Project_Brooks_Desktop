using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class Contratos_Alterar : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsContratos oContratos = new clsContratos();
    clsContratosDados oContratosDados = new clsContratosDados();
    clsContratosReajustes oReajustes = new clsContratosReajustes();
    clsContratosReajustesDados oReajustesDados = new clsContratosReajustesDados();
    clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
    clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
    clsResiduoDados oResiduoDados = new clsResiduoDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
    clsClientes oCliente = new clsClientes();
    clsClienteDados oClienteDados = new clsClienteDados();

    DataTable _dt = new DataTable();
    DataTable _dtResiduos = new DataTable();
    DataTable _dtPesquisa = new DataTable();
    private bool bSalvarReajuste = false;
    private bool bExcluirLinha = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["oUsuario"] == null)
        {
            menu _menu = (menu)FindControl("menu1");
            oUsuario.Codigo = Convert.ToInt16(((HiddenField) _menu.FindControl("hifCodigo")).Value);
            oUsuario.CodigoEmpresa = Convert.ToInt16(((HiddenField) _menu.FindControl("hifCodigoEmpresa")).Value);
            oUsuario.Nome = ((HiddenField) _menu.FindControl("hifNome")).Value;
            oUsuario.Aplicativo = false;
            Session["oUsuario"] = oUsuario;
        }
        else if (Session["oUsuario"] != null)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
        }
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "6");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            datDataReajuste.Data = DateTime.Now.Date.ToString("01/01/0001");
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
            }
            if (oUsuario == null)
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
                    try
                    {
                        _dt = oContratosDados.PegaDados(false);
                        Grade.DataSource = _dt;
                        Grade.DataBind();
                    }
                    finally
                    {
                        txtObservacao.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("Observacao");
                        txtAniversarioReajuste.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("AniversarioReajuste");
                        txtIndiceReajuste.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("IndiceReajuste");
                        TotalContratos();
                        CarregaReajusteSituacao(p_ddlSituacao);
                        CarregaReajusteTipoNegociacao(p_ddlTiposNegociacao);
                        CarregaTiposDeCaixas(p_ddlTiposDeCaixas);
                        CarregaFrequenciaColeta(p_ddlFrequenciaColeta);
                        _dtPesquisa = new DataTable();
                        _dtPesquisa = oResiduoDados.PreencheDataTableSoComResiduos("DescricaoReduzida", txtFiltroPesquisa.Text, ddlFiltroPesquisa.Text);
                        GradePesquisa.DataSource = _dtPesquisa;
                        GradePesquisa.DataBind();
                        hifResiduos.Value = "";
                        foreach (DataRow _dr in _dtPesquisa.Rows)
                        {
                            hifResiduos.Value = hifResiduos.Value + ">" + _dr["Codigo"].ToString() + "|" + _dr["DescricaoReduzida"].ToString() + "<--";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
            string r = Request.QueryString["view"];
            if (r != null)
                ViewStateSetForm();
        }
        txtNome.Focus();
    }
    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Alteração-Contratos";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Sequencial contrato nº: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Nome fantasia: " + txtNomeFantasia.Text + "(" + intCodigoCliente.Valor + ") \n";
        oLog.Log = oLog.Log + "Nome: " + txtNome.Text + ") \n";
        oLog.Log = oLog.Log + "CNPJ/CPF: " + txtCNPJ_CPF.Text + ") \n";
        oLog.Log = oLog.Log + "Data Registro: " + datDataRegistro.Data + " \n";
        oLog.Log = oLog.Log + "Data Início: " + datDataInicio.Data + " \n";
        oLog.Log = oLog.Log + "Nº Contrato: " + intNumeroContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Valor Contrato: " + moeValorContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Data Término: " + datDataTermino.Data + " \n";
        oLog.Log = oLog.Log + "Data Reajuste: " + datDataReajuste.Data + " \n";
        oLog.Log = oLog.Log + "Indice Reajuste: " + txtIndiceReajuste.Text + " \n";
        oLog.Log = oLog.Log + "Indice Reajuste: " + txtIndiceReajuste.Text + " \n";        
        oLog.Log = oLog.Log + "Containeres Locados: " + intCaixasLocadas.Valor + " \n";
        oLog.Log = oLog.Log + "Dia Vencimento: " + intDiaVencimento.Valor + " \n";
        oLog.Log = oLog.Log + "Observação: " + txtObservacao.Text + " \n";
        oLogDados.Inserir(oLog);
    }
    private void SalvarLogResiduos(string pOperacao, string pLog)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Contratos-Resíduos";
        oLog.Operacao = pOperacao;
        oLog.Log = pLog;
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Salvar")
        {
            if (txtNomeFantasia.Text.Equals("") || txtNome.Text.Equals("") || intCodigoCliente.Valor.Equals("") || !geral.IsNumeric(intCodigoCliente.Valor))
            {
                lblMensagem.Text = "Contrato inválido!";
            }
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oContratos = AtribuiDadosDoForm(oContratos);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oContratosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Alterar")
                {
                    // verificar se a senha informada é igual a gravada, caso contrário NÃO salvar senha nova
                    SalvarLog("Alteração");
                    string msgErr = oContratosDados.AlterarCamposContrato(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        if (GradeReajustes.Rows.Count > 0)
                            oReajustesDados.AlterarValorContratoUltimoReajuste(moeValorContrato.Valor, Convert.ToInt32(GradeReajustes.Rows[0].Cells[8].Text));
                        try
                        {
                            SalvarReajuste(0);
                            SalvarResiduos();
                            ddlFiltro.Text = "Código Cliente";
                            txtFiltro.Text = oContratos.CodigoCliente.ToString();
                            lblMensagem.Text = "Contrato alterado com sucesso!";
                        }
                        finally
                        {
                            FazOk(false);
                        }
                    }
                }
                Salvar.Text = "Salvar";
                lblTitulo.Text = "&nbsp;Alteração de Contratos";
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtNomeFantasia.Text.Equals(""))
            {
                lblMensagem.Text = "Contratos inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oContratosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "Contratos excluído com sucesso!";
                Salvar.Text = "Salvar";
                lblTitulo.Text = "&nbsp;Alteração de Contratos";
                Grade.DataSource = oContratosDados.PreencheDataTableContratos("Codigo asc");
                Grade.DataBind();
                LimpaCampos();
            }
        }
    }
    private void MostraGradeReajustes()
    {
        oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
        if (hifCodigo.Value != "")
        {
            GradeReajustes.DataSource = oReajustesDados.PreencheDataTable("Data desc ", Convert.ToInt32(hifCodigo.Value), 1);
            GradeReajustes.DataBind();
        }
    }
    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Contratos";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {    
        Salvar.Text = "Salvar";
        lblTitulo.Text = "&nbsp;Alteração de Contratos";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome" && e.CommandArgument.ToString() != "NomeFantasia" &&
            e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "ValorContrato" && e.CommandArgument.ToString() != "DiaVencimento" &&
            e.CommandArgument.ToString() != "DataReajuste" && e.CommandArgument.ToString() != "IndiceReajuste" && e.CommandArgument.ToString() != "DataTermino" &&
            e.CommandArgument.ToString() != "DataRecisao")
        {
            if (e.CommandArgument.ToString() != "")
            {
                if (Grade.Rows.Count - 1 >= Convert.ToInt32(e.CommandArgument))
                {
                    //txtNomeFantasia.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                    hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                    intCodigoCliente.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                    oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                    AtribuiDadosDaClasse(oContratos);
                    if (Salvar.Text != "Confirma")
                        PermissaoAlterar();
                    TiraSelecionado();
                    MarcaContratoGrade(Convert.ToInt32(e.CommandArgument));
                    MostraGradeReajustes();
                    forms_DATA _dataReajuste = new forms_DATA();
                    if (GradeReajustes.Rows.Count > 0)
                    {
                        _dataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[4].FindControl("datDataReajuste");
                        RefreshGradeResiduos(oContratos.Codigo, _dataReajuste.Data);
                        Copiar.Enabled = true;
                    }
                    else
                    {
                        GradeResiduos.DataSource = new DataTable();
                        GradeResiduos.DataBind();
                        lblMensagemResiduos.Text = "";
                    }
                }
            }
        }
    }
    protected void AtribuiDadosDaClasse(clsContratos pContratos)
    {
        txtNome.Text = pContratos.Nome;
        txtCNPJ_CPF.Text = pContratos.CNPJ_CPF;
        txtNomeFantasia.Text = pContratos.NomeFantasia;       
        txtObservacao.Text = pContratos.Observacao;
        if (pContratos.DataRegistro == "01/01/0001" || pContratos.DataRegistro == "01/01/0100" || pContratos.DataRegistro == null  || pContratos.DataRegistro == "")
            datDataRegistro.Data = ""; 
        else
            datDataRegistro.Data = Convert.ToDateTime(pContratos.DataRegistro).ToString("dd/MM/yyyy");
        intNumeroContrato.Valor = pContratos.NumeroContrato.ToString();
        if (pContratos.DataInicio == "01/01/0001" || pContratos.DataInicio == "01/01/0100" || pContratos.DataInicio == null || pContratos.DataInicio == "")
            datDataInicio.Data = "";
        else
            datDataInicio.Data = Convert.ToDateTime(pContratos.DataInicio).ToString("dd/MM/yyyy");
        if (pContratos.DataTermino == "01/01/0001" || pContratos.DataTermino == "01/01/0100" || pContratos.DataTermino == null || pContratos.DataTermino == "")
            datDataTermino.Data = "";
        else
            datDataTermino.Data = Convert.ToDateTime(pContratos.DataTermino).ToString("dd/MM/yyyy");
        if (pContratos.DataReajuste == "01/01/0001" || pContratos.DataReajuste == "01/01/0100" || pContratos.DataReajuste == null || pContratos.DataReajuste == "")
            datDataReajuste.Data = "";
        else
            datDataReajuste.Data = Convert.ToDateTime(pContratos.DataReajuste).ToString("dd/MM/yyyy");
        txtAniversarioReajuste.Text = pContratos.AniversarioReajuste;
        txtIndiceReajuste.Text = pContratos.IndiceReajuste;
        intCaixasLocadas.Valor = pContratos.NumeroCaixasLocadas.ToString();
        moeValorContrato.Valor = pContratos.ValorContrato.ToString("N2");
        hifValorContrato.Value = moeValorContrato.Valor.ToString();
        intDiaVencimento.Valor = pContratos.DiaVencimento.ToString();
    }
    protected void LimpaCampos()
    {
        intCodigoCliente.Valor = "";
        datDataReajuste.Data = "";
        txtNome.Text = "";
        txtNomeFantasia.Text = "";
        txtCNPJ_CPF.Text = "";
        txtObservacao.Text = "";
        datDataRegistro.Data = "";
        intNumeroContrato.Valor = "";
        datDataInicio.Data = "";
        datDataTermino.Data = "";
        txtAniversarioReajuste.Text = "";
        datDataReajuste.Data = "";
        txtIndiceReajuste.Text = "";
        intCaixasLocadas.Valor = "";
        moeValorContrato.Valor = "";
        hifValorContrato.Value = "";
        intDiaVencimento.Valor = "";
        Grade.Visible = true;
        lblMensagem.Text = "";
        lblMensagemResiduos.Text = "";
        hifCodigo.Value = "";
        hifCodigoResiduo.Value = "";
        hifValorContrato.Value = "";
    }
    protected clsContratos AtribuiDadosDoForm(clsContratos pContratos)
    {
        if (geral.IsNumeric(intCodigoCliente.Valor))
        {
            pContratos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
            pContratos.Observacao = txtObservacao.Text;
            pContratos.DataRegistro = datDataRegistro.Data;
            if (intNumeroContrato.Valor != "")
                pContratos.NumeroContrato = Convert.ToInt32(intNumeroContrato.Valor);
            pContratos.DataInicio = datDataInicio.Data;
            pContratos.DataTermino = datDataTermino.Data;
            if (txtAniversarioReajuste.Text != "")
                pContratos.AniversarioReajuste = txtAniversarioReajuste.Text;
            pContratos.DataReajuste = datDataReajuste.Data;
            pContratos.IndiceReajuste = txtIndiceReajuste.Text;
            if (intCaixasLocadas.Valor != "")
                pContratos.NumeroCaixasLocadas = Convert.ToInt32(intCaixasLocadas.Valor);
            if (moeValorContrato.Valor != "")
                pContratos.ValorContrato = Convert.ToDecimal(moeValorContrato.Valor);
            if (intDiaVencimento.Valor != "")
                pContratos.DiaVencimento = Convert.ToInt32(intDiaVencimento.Valor);
        }
        return pContratos;
    }
    protected void ViewStateGetForm()
    {
        ViewState["Data1Cadastro"] = datDataReajuste.Data;
        ViewState["Nome"] = txtNomeFantasia.Text;
        ViewState["Descricao"] = txtObservacao.Text;
    }
    protected void ViewStateSetForm()
    {
        datDataReajuste.Data = ViewState["DataCadastro"].ToString();
        txtNomeFantasia.Text = ViewState["Nome"].ToString();
        txtObservacao.Text = ViewState["Descricao"].ToString();        
    }
    private void CancelarOperacao()
    {
        lblTitulo.Text = "&nbsp;Alteração de Contratos";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Salvar";
        TiraSelecionado();
        ClearGradeReajustes();
        ClearGradeResiduos();
        lblTituloResiduos.Visible = false;
        lblMensagemResiduos.Visible = false;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        CancelarOperacao();
        _dt = oContratosDados.PegaDados(false);
        Grade.DataSource = _dt;
        Grade.DataBind();
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {  
        if (e.Row.RowIndex >= 0)
        {
            ImageButton _ibnExcluir = new ImageButton();
            _ibnExcluir.Enabled = true;
            if (oItensMenuPermissoes.Excluir == 0)
            {
                _ibnExcluir = (ImageButton)e.Row.Cells[1].FindControl("ibnExcluir");
                _ibnExcluir.Enabled = false;
            }
            if (e.Row.Cells[8].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[8].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[8].Text = "";
            if (e.Row.Cells[10].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[10].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[10].Text = "";
            if (e.Row.Cells[11].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[11].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[11].Text = "";
            for (int i = 0; i <= 11; i++)
            {
                if (e.Row.Cells[11].Text == "")
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Black;
                else
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        string _filtro = ddlFiltro.Text;
        if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
            _filtro = "CodigoCliente";
        else if (ddlFiltro.Text == "Cancelados")
        {
            _filtro = "DataRecisao";
            txtFiltro.Text = ">0100-01-01";
        }
        else if (ddlFiltro.Text == "Não Cancelados")
        {
            _filtro = "DataRecisao";
            txtFiltro.Text = "";
        }
        else if (ddlFiltro.Text == "Código Contrato")
        {
            _filtro = "Codigo";
            if (!geral.IsNumeric(txtFiltro.Text))
                txtFiltro.Text = "";
        }
        _dt = oContratosDados.PreencheDataTableContratos(geral.Ordem, txtFiltro.Text, _filtro);
        Grade.DataSource = _dt;
        Grade.DataBind();
        TotalContratos();
        TiraSelecionado();
        ClearGradeReajustes();
    }
    private void FazOk(bool pMostraResiduos)
    {
        Session["Clientes"] = null;
        string _Campo = "";
        string _Ordem = ddlFiltro.Text;
        _Campo = ddlFiltro.Text;
        if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
        {
            _Campo = "CodigoCliente";
            _Ordem = "Codigo desc";
            if (!geral.IsNumeric(txtFiltro.Text))
                txtFiltro.Text = "";
        }
        else if (ddlFiltro.Text == "Cancelados")
        {
            _Campo = "DataRecisao";
            _Ordem = _Campo;
            txtFiltro.Text = ">0100-01-01";
        }
        else if (ddlFiltro.Text == "Não Cancelados")
        {
            _Campo = "DataRecisao";
            _Ordem = _Campo;
            txtFiltro.Text = "";
        }
        else if (ddlFiltro.Text == "Código Contrato")
        {
            _Campo = "Codigo";
            _Ordem = _Campo;
            if (!geral.IsNumeric(txtFiltro.Text))
                txtFiltro.Text = "0";
        }
        _dt = oContratosDados.PreencheDataTableContratos(_Ordem, txtFiltro.Text, _Campo);
        Grade.DataSource = _dt;
        Grade.DataBind();
        TotalContratos();
        if (_dt.Rows.Count > 0)
        {
            hifCodigo.Value = _dt.Rows[0]["Codigo"].ToString();
            intCodigoCliente.Valor = _dt.Rows[0]["CodigoCliente"].ToString();
            oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
            AtribuiDadosDaClasse(oContratos);
            TiraSelecionado();
            MarcaContratoGrade(0);
            try
            {
                MostraGradeReajustes();
            }
            finally
            {
                if (GradeReajustes.Rows.Count > 0)
                {
                    forms_DATA _dataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[4].FindControl("datDataReajuste");
                    if (pMostraResiduos)
                        RefreshGradeResiduos(oContratos.Codigo, _dataReajuste.Data);
                }
            }
        }
        else
        {
            LimpaCampos();
            GradeReajustes.DataSource = "";
            GradeReajustes.DataBind();
            GradeResiduos.DataSource = "";
            GradeResiduos.DataBind();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        lblMensagemResiduos.Text = "";
        FazOk(true);
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        ViewStateGetForm();
    }
    private void TotalContratos()
    {
        lblTotal.Text = "";
        btnContratos.Text = "";
        if (ddlFiltro.SelectedValue.ToString().IndexOf("Cancelados") > -1)
        {
            decimal _valorTotalContratos = 0;
            decimal _ValorTotalContratoCobrancaMensal = 0;
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["ValorContrato"].ToString() != "")
                    _valorTotalContratos = _valorTotalContratos + Convert.ToDecimal(_dr["ValorContrato"]);
            }
            if (_dtResiduos.Rows.Count > 0)
            {
                foreach(DataRow _dr in _dtResiduos.Rows)
                {
                    if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                        _ValorTotalContratoCobrancaMensal = _ValorTotalContratoCobrancaMensal + Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
                }
                _dtResiduos.NewRow();
                _dtResiduos.Rows.Add();
                _dtResiduos.Rows[_dtResiduos.Rows.Count - 1]["ValorContratoCobrancaMensal"] = _ValorTotalContratoCobrancaMensal;
            }
            btnContratos.Text = "Total Contratos";
            lblTotal.Text = _valorTotalContratos.ToString("N2");
        }
    }
    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFiltro.Text = "";
        btnOk_Click(sender, e);
    }
    protected void btnProcurar_Click1(object sender, EventArgs e)
    {
        if (Session["Clientes"] != null)
        {
            CancelarOperacao();
            clsClientes oCl = new clsClientes();
            oCl = (clsClientes)Session["Clientes"];
            intCodigoCliente.Valor = oCl.Codigo.ToString();
            txtNome.Text = oCl.Nome;
            txtNomeFantasia.Text = oCl.NomeFantasia;
            txtCNPJ_CPF.Text = oCl.CNPJ_CPF;
        }
    }
    protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (geral.Ordem == "")
                geral.Ordem = "NomeFantasia";
            string _filtro = ddlFiltro.Text;
            if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
                _filtro = "CodigoCliente";
            else if (ddlFiltro.Text == "Cancelados")
            {
                //geral.Ordem = "DataRecisao";
                _filtro = "DataRecisao";
                txtFiltro.Text = ">0100-01-01";
            }
            else if (ddlFiltro.Text == "Não Cancelados")
            {
                //geral.Ordem = "DataRecisao";
                txtFiltro.Text = "";
                _filtro = "DataRecisao";
            }
            else if (ddlFiltro.Text == "Código Contrato")
            {
                _filtro = "Codigo";
                if (!geral.IsNumeric(txtFiltro.Text))
                    txtFiltro.Text = "";
            }
            _dt = oContratosDados.PreencheDataTableContratos(geral.Ordem, txtFiltro.Text, _filtro);
            Grade.DataSource = _dt;
        }
        finally
        {
            Grade.PageIndex = e.NewPageIndex;
            Grade.DataBind();
            TotalContratos();
            TiraSelecionado();
            ClearGradeReajustes();
            LimpaCampos();
        }
    }
    private void ClearGradeReajustes()
    {
        GradeReajustes.DataSource = new DataTable();
        GradeReajustes.DataBind();
    }

    private void TiraSelecionado()
    {
        bool bInterCor = false;
        for (int i = 0; i < Grade.Rows.Count; i++)
        {
            ImageButton ibnConsultar = (ImageButton)Grade.Rows[i].FindControl("ibnMudar");
            if (ibnConsultar != null)
                ibnConsultar.ImageUrl = "~/Images/selecionar.png";
            if (bInterCor)
                bInterCor = false;
            else
                bInterCor = true;
            for (int j = 0; j < Grade.Columns.Count; j++)
            {
                if (bInterCor)
                    Grade.Rows[i].Cells[j].BackColor = System.Drawing.Color.White;
                else
                    Grade.Rows[i].Cells[j].BackColor = System.Drawing.Color.AliceBlue;
            }
        }
    }
    private void MarcaContratoGrade(int pLinha)
    {
        if (Grade.Rows.Count > 0)
        {
            for (int i = 0; i < Grade.Columns.Count; i++)
            {
                Grade.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
            }
            ImageButton ibnConsultar = (ImageButton)Grade.Rows[pLinha].FindControl("ibnMudar");
            if (ibnConsultar != null)
                ibnConsultar.ImageUrl = "~/Images/selecionado.png";
        }
    }
    protected void GradeReajustes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            if (bSalvarReajuste)
            {
                SalvarReajuste(Convert.ToInt32(e.CommandArgument));
                for (int _i = 2; _i < GradeReajustes.Rows.Count; _i++)
                {
                    GradeReajustes.Rows[_i].Cells[2].Text = "";
                }
            }
        }

    }
    private void SalvarReajuste(int pLinha)
    {
        GridViewRow gvr = GradeReajustes.Rows[pLinha];
        forms_DATA _datDataReajuste = (forms_DATA)gvr.Cells[3].FindControl("datDataReajuste");
        forms_MOEDA _moeValorContrato = (forms_MOEDA)gvr.Cells[4].FindControl("moeValorContrato");
        forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)gvr.Cells[5].FindControl("intNumeroContrato");
        DropDownList _ddlSituacao = (DropDownList)gvr.Cells[6].FindControl("ddlSituacao");
        DropDownList _ddlTipoNegociacao = (DropDownList)gvr.Cells[7].FindControl("ddlTipoNegociacao");
        oReajustes = new clsContratosReajustes();
        if (hifCodigo.Value != "")
            oReajustes.CodigoContrato = Convert.ToInt32(hifCodigo.Value);
        oReajustes.Data = _datDataReajuste.Data;
        oReajustes.NumeroContrato = _intNumeroContrato.Valor;
        if (gvr.Cells[8].Text != "" && gvr.Cells[8].Text != "&nbsp;")
            oReajustes.Sequencial = Convert.ToInt32(gvr.Cells[8].Text);
        oReajustes.Situacao = _ddlSituacao.Text;
        oReajustes.TipoNegociacao = _ddlTipoNegociacao.Text;
        if (_moeValorContrato.Valor != "")
            oReajustes.Valor = Convert.ToDecimal(_moeValorContrato.Valor);

        // alterar dados atuais
        if (oReajustesDados.Alterar(oReajustes, oReajustes.CodigoContrato, 0, oReajustes.Sequencial) == string.Empty)
        {
            if (oReajustes.CodigoContrato > 0)
                oContratosDados.AlterarValorContrato(Convert.ToDecimal(_moeValorContrato.Valor), oReajustes.CodigoContrato);

            ddlFiltro.Text = "Código Cliente";
            txtFiltro.Text = intCodigoCliente.Valor;
            FazOk(false);
        }
    }
    protected void GradeReajustes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            DropDownList _ddlSituacao = (DropDownList)e.Row.Cells[6].FindControl("ddlSituacao");
            DropDownList _ddlTipoNegociacao = (DropDownList)e.Row.Cells[7].FindControl("ddlTipoNegociacao");
            forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)e.Row.Cells[5].FindControl("intNumeroContrato");
            forms_DATA _datDataReajuste = new forms_DATA();
            forms_MOEDA _moeValorContrato = new forms_MOEDA();
            ImageButton _ibnSalvarReajuste = new ImageButton();
            _datDataReajuste = (forms_DATA)e.Row.Cells[3].FindControl("datDataReajuste");
            _moeValorContrato = (forms_MOEDA)e.Row.Cells[4].FindControl("moeValorContrato");
            for (int iii = 0; iii < p_ddlSituacao.Items.Count; iii++)
                _ddlSituacao.Items.Add(p_ddlSituacao.Items[iii].Text);
            HiddenField _hifSituacao = (HiddenField)e.Row.Cells[6].FindControl("hifSituacao");
            _ddlSituacao.Text = _hifSituacao.Value;
            for (int iii = 0; iii < p_ddlTiposNegociacao.Items.Count; iii++)
                _ddlTipoNegociacao.Items.Add(p_ddlTiposNegociacao.Items[iii].Text);
            HiddenField _hifTipoNegociacao = (HiddenField)e.Row.Cells[7].FindControl("hifTipoNegociacao");
            _ddlTipoNegociacao.Text = _hifTipoNegociacao.Value;
            _datDataReajuste = (forms_DATA)e.Row.Cells[3].FindControl("datDataReajuste");
            _moeValorContrato = (forms_MOEDA)e.Row.Cells[4].FindControl("moeValorContrato");
            _datDataReajuste.Enabled = false;
        }
    }
    protected void ibnSalvarReajuste_Click(object sender, ImageClickEventArgs e)
    {
        //ClearGradeResiduos();
        bSalvarReajuste = true;
    }
    private void CarregaReajusteSituacao(DropDownList pddl)
    {
        pddl.Items.Clear();
        pddl.Items.Add("");
        pddl.Items.Add("SEM CONTRATO");
        pddl.Items.Add("EM ANÁLISE/REDAÇÃO");
        pddl.Items.Add("AGUARDANDO ASSINATURA CLIENTE");
        pddl.Items.Add("ASSINADO/ARQUIVADO");
        foreach (DataRow dr in oReajustesDados.PreencheDT_Situacao().Rows)
        {
            if (dr[0].ToString() != "" && dr[0].ToString() != "SEM CONTRATO" &&
                dr[0].ToString() != "EM ANÁLISE/REDAÇÃO" && dr[0].ToString() != "AGUARDANDO ASSINATURA CLIENTE" &&
                dr[0].ToString() != "ASSINADO/ARQUIVADO")
                pddl.Items.Add(dr[0].ToString());
        }
    }
    private void CarregaReajusteTipoNegociacao(DropDownList pddl)
    {
        pddl.Items.Clear();
        pddl.Items.Add("");
        pddl.Items.Add("REAJUSTE");
        pddl.Items.Add("REPACTUAÇÃO");
        foreach (DataRow dr in oReajustesDados.PreencheDT_TipoNegociacao().Rows)
        {
            if (dr[0].ToString() != "" && dr[0].ToString() != "REAJUSTE" && dr[0].ToString() != "REPACTUAÇÃO")
                pddl.Items.Add(dr[0].ToString());
        }
    }
    private void GradeResiduosContratados(GridView pGrade, int pLinha)
    {
        lblMensagemResiduos.Text = "";
        if (pLinha == 0)
            pLinha++;
        if (pGrade.Rows.Count > pLinha)
        {
            oContratoResiduos.DataReajuste = ((forms_DATA)pGrade.Rows[pLinha].Cells[3].FindControl("datDataReajuste")).Data;
            oReajustesDados.PegaDados(oReajustes, Convert.ToInt32(hifCodigo.Value), oContratoResiduos.DataReajuste, Convert.ToInt32(pGrade.Rows[pLinha].Cells[8].Text));

            hifValorContrato.Value = oReajustes.Valor.ToString();

            oContratos = oContratosDados.PegaDados(oContratos, 0, Convert.ToInt32(intCodigoCliente.Valor));
            _dtResiduos = new DataTable();
            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratos.Codigo, oContratoResiduos.DataReajuste, oContratos.CodigoCliente);
            decimal _ValorTotalContratoCobrancaMensal = 0;
            foreach (DataRow _dr in _dtResiduos.Rows)
            {
                if (_dr["ValorUnitario"].ToString() != "")
                    if (Convert.ToDecimal(_dr["ValorUnitario"]) > 0)
                        _dr["ValorUnitario"] = Convert.ToDecimal(_dr["ValorUnitario"]) / 100;
                if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                    _ValorTotalContratoCobrancaMensal = _ValorTotalContratoCobrancaMensal + Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
            }
            _dtResiduos.NewRow();
            _dtResiduos.Rows.Add();
            _dtResiduos.Rows[_dtResiduos.Rows.Count - 1]["ValorContratoCobrancaMensal"] = _ValorTotalContratoCobrancaMensal;
            GradeResiduos.DataSource = _dtResiduos;
            GradeResiduos.DataBind();
            TextBox _txtDescricaoResiduo = (TextBox)GradeResiduos.Rows[_dtResiduos.Rows.Count - 1].Cells[4].FindControl("txtDescricaoResiduo");
            if (_txtDescricaoResiduo != null)
                _txtDescricaoResiduo.Text = "F2 - Pesquisa";

            lblTituloResiduos.Text = "Resíduos Contratados";
            if (GradeReajustes.Rows.Count > 0)
            {
                for (int i = 0; i < GradeReajustes.Rows.Count - 2; i++)
                {
                    GradeReajustes.Rows[i + 2].Cells[2].Text = "";
                    for (int j = 0; j <= 7; j++)
                    {
                        GradeReajustes.Rows[i + 1].Cells[j].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                ClearGradeResiduos();
            }
        }
    }
    private void ClearGradeResiduos()
    {
        lblTituloResiduos.Text = "";
        _dtResiduos = new DataTable();
        GradeResiduos.DataSource = _dtResiduos;
        GradeResiduos.DataBind();
    }
    protected void GradeResiduos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(2, 2, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.Height = 40;
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Style.Add("background-color", "Black");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BorderWidth = 1;
            HeaderCell.Text = "DADOS PARA COBRANÇA - COLETA";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Style.Add("background-color", "#ff9900");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.BorderWidth = 1;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = "DADOS PARA COBRANÇA - PESO/VOLUME";
            HeaderCell.ColumnSpan = 9;
            HeaderCell.Font.Bold = true;
            HeaderGridRow.Cells.Add(HeaderCell);

            GradeResiduos.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    private bool ExisteFrequenciaColeta(DataTable pDt, string pFrequencia)
    {
        bool bRet = false;
        foreach (DataRow dr in pDt.Rows)
        {
            string[] _qtfranquia = dr["FrequenciaColeta"].ToString().Split(" "[0]);
            if (_qtfranquia.Length == 1)
            {
                if (!geral.IsNumeric(_qtfranquia[0]) || (_qtfranquia[0] == "" && pFrequencia == ""))
                {
                    if (pFrequencia == _qtfranquia[0])
                    {
                        bRet = true;
                        break;
                    }
                }
            }
            else if (_qtfranquia.Length > 1)
            {
                if (!geral.IsNumeric(_qtfranquia[1]) || (_qtfranquia[1] == "" && pFrequencia == ""))
                {
                    if (pFrequencia == _qtfranquia[1])
                    {
                        bRet = true;
                        break;
                    }
                }
            }
        }
        return bRet;
    }
    private void CarregaFrequenciaColeta(DropDownList pddlFrequenciaColeta)
    {
        pddlFrequenciaColeta.Items.Clear();
        DataTable _dt = new DataTable();
        _dt = oContratoResiduosDados.PegaFrequenciaColetasDistintas();
        foreach (DataRow dr in _dt.Rows)
        {
            string[] _qtfranquia = dr["FrequenciaColeta"].ToString().Split(" "[0]);
            if (_qtfranquia.Length > 1)
                dr["FrequenciaColeta"] = _qtfranquia[1];
            if (_qtfranquia.Length == 1)
            {
                if (!geral.IsNumeric(_qtfranquia[0]) || _qtfranquia[0] == "")
                    dr["FrequenciaColeta"] = _qtfranquia[0];
            }            
        }
        if (!ExisteFrequenciaColeta(_dt, ""))
            pddlFrequenciaColeta.Items.Add("");
        if (!ExisteFrequenciaColeta(_dt, "DEMANDA"))
            pddlFrequenciaColeta.Items.Add("DEMANDA");
        if (!ExisteFrequenciaColeta(_dt, "DIÁRIA"))
            pddlFrequenciaColeta.Items.Add("DIÁRIA");
        if (!ExisteFrequenciaColeta(_dt, "SEMANAL"))
            pddlFrequenciaColeta.Items.Add("SEMANAL");
        if (!ExisteFrequenciaColeta(_dt, "MENSAL"))
            pddlFrequenciaColeta.Items.Add("MENSAL");
        if (!ExisteFrequenciaColeta(_dt, "BIMESTRAL"))
            pddlFrequenciaColeta.Items.Add("BIMESTRAL");
        if (!ExisteFrequenciaColeta(_dt, "TRIMESTRAL"))
            pddlFrequenciaColeta.Items.Add("TRIMESTRAL");
        if (!ExisteFrequenciaColeta(_dt, "QUADRIMESTRAL"))
            pddlFrequenciaColeta.Items.Add("QUADRIMESTRAL");
        if (!ExisteFrequenciaColeta(_dt, "SEMESTRAL"))
            pddlFrequenciaColeta.Items.Add("SEMESTRAL");
        if (!ExisteFrequenciaColeta(_dt, "ANUAL"))
            pddlFrequenciaColeta.Items.Add("ANUAL");
        DataView view = new DataView(_dt);
        DataTable _dtDistinct = view.ToTable(true, "FrequenciaColeta");
        foreach (DataRow dr in _dtDistinct.Rows)
        {
            if (dr[0].ToString() != "01" && dr[0].ToString() != "A" && geral.Left(dr[0].ToString().ToLower(), 4) != "cole" && dr[0].ToString() != "4")
                pddlFrequenciaColeta.Items.Add(dr[0].ToString());
        }
    }
    private void CarregaTiposDeCaixas(DropDownList pddlTipoCaixa)
    {
        pddlTipoCaixa.Items.Clear();
        pddlTipoCaixa.Items.Add("");
        foreach (DataRow dr in oContratoResiduosDados.PegaConteineresDistintos().Rows)
        {
            pddlTipoCaixa.Items.Add(geral.Left(dr[0].ToString(), 6));
        }
    }
    protected void GradeResiduos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            TextBox _txtCodigoResiduo = (TextBox)e.Row.Cells[3].FindControl("txtCodigoResiduo");
            TextBox _txtCxDisp = (TextBox)e.Row.Cells[5].FindControl("txtCxDisp");
            if (e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[14].Text = "";
            if (e.Row.Cells[13].Text == "" && e.Row.Cells[19].Text.ToUpper() == "CX")
                e.Row.Cells[13].Text = e.Row.Cells[17].Text;
            if ((e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;") && e.Row.Cells[10].Text != "&nbsp;" && e.Row.Cells[10].Text != "" &&
                e.Row.Cells[13].Text != "&nbsp;" && e.Row.Cells[13].Text != "")
            {
                e.Row.Cells[14].Text = (Convert.ToDecimal(e.Row.Cells[10].Text) * Convert.ToDecimal(e.Row.Cells[13].Text)).ToString("N2");
            }
            DropDownList _ddlTipoCaixa = (DropDownList)e.Row.Cells[6].FindControl("ddlTipoCx");
            HiddenField _hifTipoCaixa = (HiddenField)e.Row.Cells[6].FindControl("hifTipoCx");
            for (int iii = 0; iii < p_ddlTiposDeCaixas.Items.Count; iii++)
                _ddlTipoCaixa.Items.Add(p_ddlTiposDeCaixas.Items[iii].Text);
            _ddlTipoCaixa.Text = _hifTipoCaixa.Value;

            TextBox _txtQtFrequenciaColeta = (TextBox)e.Row.Cells[7].FindControl("txtQtFrequenciaColeta");
            DropDownList _ddlFrequenciaColeta = (DropDownList)e.Row.Cells[7].FindControl("ddlFrequenciaColeta");
            for (int iii = 0; iii < p_ddlFrequenciaColeta.Items.Count; iii++)
                _ddlFrequenciaColeta.Items.Add(p_ddlFrequenciaColeta.Items[iii].Text);
            //CarregaFrequenciaColeta(_ddlFrequenciaColeta);
            string[] _qtfranquia = _txtQtFrequenciaColeta.Text.Split(" "[0]);
            if (_qtfranquia.Length > 0)
                if (geral.IsNumeric(_qtfranquia[0]))
                    _txtQtFrequenciaColeta.Text = _qtfranquia[0];
                else
                    _txtQtFrequenciaColeta.Text = "";
            if (_qtfranquia.Length > 1)
                _ddlFrequenciaColeta.Text = _qtfranquia[1];
            if (_qtfranquia.Length == 1)
            {
                if (!geral.IsNumeric(_qtfranquia[0]))
                    _ddlFrequenciaColeta.Text = _qtfranquia[0];
            }
            TextBox _txtDescricaoResiduo = (TextBox)e.Row.Cells[4].FindControl("txtDescricaoResiduo");
            DropDownList _ddlTipoCx = (DropDownList)e.Row.Cells[6].FindControl("ddlTipoCx");
            DropDownList _ddlRoteiro = (DropDownList)e.Row.Cells[8].FindControl("ddlRoteiro");
            _ddlRoteiro.Items.Clear();
            _ddlRoteiro.Items.Add("-");
            _ddlRoteiro.Items.Add("SEMANAL");
            _ddlRoteiro.Items.Add("MENSAL");
            _ddlRoteiro.Items.Add("");
            HiddenField _hifRoteiro = (HiddenField)e.Row.FindControl("hifRoteiro");
            _ddlRoteiro.Text = _hifRoteiro.Value;
            DropDownList _ddlexpressao1CobrancaMensal = (DropDownList)e.Row.Cells[9].FindControl("ddlexpressao1CobrancaMensal");
            forms_MOEDA _moeFranquiaCobrancaMensal = (forms_MOEDA)e.Row.Cells[10].FindControl("moeFranquiaCobrancaMensal");
            DropDownList _ddlexpressao2CobrancaMensal = (DropDownList)e.Row.Cells[11].FindControl("ddlexpressao2CobrancaMensal");
            DropDownList _ddlPeriodicidadeCobrancaMensal = (DropDownList)e.Row.Cells[12].FindControl("ddlPeriodicidadeCobrancaMensal");
            forms_MOEDA _moeValorContratoCobrancaMensal = (forms_MOEDA)e.Row.Cells[14].FindControl("moeValorContratoCobrancaMensal");
            forms_MOEDA _moeValorExcedenteCobrancaMensal = (forms_MOEDA)e.Row.Cells[15].FindControl("moeValorExcedenteCobrancaMensal");
            DropDownList _ddlexpressao1CobrancaPeso = (DropDownList)e.Row.Cells[16].FindControl("ddlexpressao1CobrancaPeso");
            forms_MOEDA _moeValorUnitario = (forms_MOEDA)e.Row.Cells[17].FindControl("moeValorUnitario");
            DropDownList _ddlexpressao2CobrancaPeso = (DropDownList)e.Row.Cells[18].FindControl("ddlexpressao2CobrancaPeso");
            DropDownList _ddlcondicaoCobrancaPeso = (DropDownList)e.Row.Cells[19].FindControl("ddlcondicaoCobrancaPeso");
            TextBox _txtUnidade = (TextBox)e.Row.Cells[20].FindControl("txtUnidade");
            forms_MOEDA _moeFranquiaCobrancaPeso = (forms_MOEDA)e.Row.Cells[20].FindControl("moeFranquiaCobrancaPeso");
            TextBox _txtUnidadeCobrancaPeso = (TextBox)e.Row.Cells[21].FindControl("txtUnidadeCobrancaPeso");
            DropDownList _ddlexpressao3CobrancaPeso = (DropDownList)e.Row.Cells[22].FindControl("ddlexpressao3CobrancaPeso");
            DropDownList _ddlexpressao4CobrancaPeso = (DropDownList)e.Row.Cells[23].FindControl("ddlexpressao4CobrancaPeso");
            TextBox _txtOBS = (TextBox)e.Row.Cells[24].FindControl("txtOBS");
            TextBox _txtDiasColeta = (TextBox)e.Row.Cells[25].FindControl("txtDiasColeta");
            TextBox _txtParticularidade = (TextBox)e.Row.Cells[26].FindControl("txtParticularidade");
            TextBox _txtMesAnoBase = (TextBox)e.Row.Cells[27].FindControl("txtMesAnoBase");
        }
    }
    protected void ibnMudar1_Click1(object sender, ImageClickEventArgs e)
    {
        lblTituloResiduos.Text = "Resíduos Contratados";
        bSalvarReajuste = false;
    }
    protected void GradeResiduos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "CodigoContrato" && e.CommandArgument.ToString() != "CodigoResiduo" &&
            e.CommandArgument.ToString() != "DescricaoReduzidaResiduo")
        {
            if (Convert.ToInt32(e.CommandArgument) < oGrade.Rows.Count - 1)
            {
                if (bExcluirLinha)
                {
                    ImageButton ibnExcluirResiduo = (ImageButton)oGrade.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("ibnExcluirResiduo");
                    if (ibnExcluirResiduo != null)
                    {
                        if (ibnExcluirResiduo.ImageUrl.IndexOf("confirma.png") > -1)
                        {
                            ExcluirResiduo(Convert.ToInt32(e.CommandArgument));
                            bExcluirLinha = false;
                            lblMensagemResiduos.Text = "Resíduo excluído com sucesso!";
                        }
                    }
                    if (ibnExcluirResiduo != null && bExcluirLinha)
                    {
                        ibnExcluirResiduo.ImageUrl = "~/Images/confirma.png";
                        ibnExcluirResiduo.ToolTip = "Confirma?";
                        lblMensagemResiduos.Text = "";
                    }
                }
            }
        }
    }
    private void RefreshGradeResiduos(int pCodigoContrato, string pDataReajuste)
    {
        lblTituloResiduos.Visible = true;
        lblMensagemResiduos.Visible = true;
        _dtResiduos = new DataTable();
        if (pCodigoContrato > 0 && pDataReajuste != "")
        {
            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", pCodigoContrato, pDataReajuste, oContratos.CodigoCliente);
        }
        decimal _ValorTotalContratoCobrancaMensal = 0;
        foreach (DataRow _dr in _dtResiduos.Rows)
        {
            if (_dr["ValorUnitario"].ToString() != "")
            {
                if (Convert.ToDecimal(_dr["ValorUnitario"]) > 0)
                    _dr["ValorUnitario"] = Convert.ToDecimal(_dr["ValorUnitario"]) / 100;
            }
            if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                _ValorTotalContratoCobrancaMensal = _ValorTotalContratoCobrancaMensal + Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
        }
        _dtResiduos.NewRow();
        _dtResiduos.Rows.Add();
        _dtResiduos.Rows[_dtResiduos.Rows.Count - 1]["ValorContratoCobrancaMensal"] = _ValorTotalContratoCobrancaMensal;
        GradeResiduos.DataSource = _dtResiduos;
        GradeResiduos.DataBind();
        TextBox _txtDescricaoResiduo = (TextBox)GradeResiduos.Rows[_dtResiduos.Rows.Count - 1].Cells[4].FindControl("txtDescricaoResiduo");
        if (_txtDescricaoResiduo != null)
            _txtDescricaoResiduo.Text = "F2 - Pesquisa";

        lblTituloResiduos.Text = "Resíduos Contratados";
    }
    protected void ibnExcluirResiduo_Click(object sender, ImageClickEventArgs e)
    {
        bExcluirLinha = true;
        bSalvarReajuste = false;
    }
    private void ExcluirResiduo(int pLinhaExcluir)
    {
        if (bExcluirLinha && pLinhaExcluir >= 0)
        {
            TextBox _txtCodigoResiduo = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[2].FindControl("txtCodigoResiduo");
            oContratoResiduos = new clsContratoResiduos();

            TextBox _txtCaixaDisponivel = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[4].FindControl("txtCxDisp");
            if (_txtCaixaDisponivel.Text != "" && geral.IsNumeric(_txtCaixaDisponivel.Text))
                oContratoResiduos.CaixaDisponivel = Convert.ToInt32(_txtCaixaDisponivel.Text);
            DropDownList _ddlTipoCaixa = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[5].FindControl("ddlTipoCx");
            oContratoResiduos.TipoCaixa = _ddlTipoCaixa.Text;
            TextBox _txtQtFrequenciaColeta = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[6].FindControl("txtQtFrequenciaColeta");
            DropDownList _ddlFrequenciaColeta = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[6].FindControl("ddlFrequenciaColeta");
            string[] _qtfranquia = _txtQtFrequenciaColeta.Text.Split(" "[0]);
            if (_qtfranquia.Length > 0)
                if (geral.IsNumeric(_qtfranquia[0]))
                    _txtQtFrequenciaColeta.Text = _qtfranquia[0];
            if (_qtfranquia.Length > 1)
                _ddlFrequenciaColeta.Text = _qtfranquia[1];
            if (_qtfranquia.Length == 1)
            {
                if (!geral.IsNumeric(_qtfranquia[0]))
                    _ddlFrequenciaColeta.Text = _qtfranquia[0];
            }
            oContratoResiduos.FrequenciaColeta = _txtQtFrequenciaColeta.Text + " " + _ddlFrequenciaColeta.Text;
            DropDownList ddlRoteiro = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[7].FindControl("ddlRoteiro");
            oContratoResiduos.Roteiro = ddlRoteiro.Text;
            DropDownList ddlexpressao1CobrancaMensal = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[7].FindControl("ddlexpressao1CobrancaMensal");
            oContratoResiduos.expressao1CobrancaMensal = ddlexpressao1CobrancaMensal.Text;
            forms_MOEDA moeFranquiaCobrancaMensal = (forms_MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[8].FindControl("moeFranquiaCobrancaMensal");
            oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor;
            DropDownList ddlexpressao2CobrancaMensal = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[9].FindControl("ddlexpressao2CobrancaMensal");
            oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
            DropDownList ddlPeriodicidadeCobrancaMensal = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[9].FindControl("ddlPeriodicidadeCobrancaMensal");
            oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;
            forms_MOEDA moeValorExcedenteCobrancaMensal = (forms_MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[11].FindControl("moeValorExcedenteCobrancaMensal");
            if (moeValorExcedenteCobrancaMensal.Valor != "")
                oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);
            DropDownList ddlexpressao1CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[12].FindControl("ddlexpressao1CobrancaPeso");
            oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
            forms_MOEDA moeValorUnitario = (forms_MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[13].FindControl("moeValorUnitario");
            if (moeValorUnitario.Valor != "")
                oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitario.Valor) * 100;
            DropDownList ddlexpressao2CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[14].FindControl("ddlexpressao2CobrancaPeso");
            oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
            TextBox txtUnidade = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[15].FindControl("txtUnidade");
            oContratoResiduos.Unidade = txtUnidade.Text;
            DropDownList ddlcondicaoCobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[16].FindControl("ddlcondicaoCobrancaPeso");
            oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
            forms_MOEDA moeFranquiaCobrancaPeso = (forms_MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[17].FindControl("moeFranquiaCobrancaPeso");
            if (moeFranquiaCobrancaPeso.Valor != "")
                oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(moeFranquiaCobrancaPeso.Valor);
            TextBox txtUnidadeCobrancaPeso = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[18].FindControl("txtUnidadeCobrancaPeso");
            oContratoResiduos.UnidadeCobrancaPeso = txtUnidadeCobrancaPeso.Text;
            DropDownList ddlexpressao3CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[19].FindControl("ddlexpressao3CobrancaPeso");
            oContratoResiduos.expressao3CobrancaPeso = ddlexpressao3CobrancaPeso.Text;
            DropDownList ddlexpressao4CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[20].FindControl("ddlexpressao4CobrancaPeso");
            oContratoResiduos.expressao4CobrancaPeso = ddlexpressao4CobrancaPeso.Text;
            TextBox txtOBS = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[21].FindControl("txtOBS");
            oContratoResiduos.OBS = txtOBS.Text;
            TextBox txtDiasColeta = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[22].FindControl("txtDiasColeta");
            oContratoResiduos.DiasColeta = txtDiasColeta.Text;
            TextBox txtParticularidade = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[23].FindControl("txtParticularidade");
            oContratoResiduos.Particularidade = txtParticularidade.Text;
            TextBox txtMesAnoBase = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[23].FindControl("txtMesAnoBase");
            oContratoResiduos.MesAnoBase = txtMesAnoBase.Text;

            if (hifCodigo.Value != "" && geral.IsNumeric(hifCodigo.Value) && _txtCodigoResiduo.Text != "" && geral.IsNumeric(_txtCodigoResiduo.Text) &&
                intCodigoCliente.Valor != "" && geral.IsNumeric(intCodigoCliente.Valor))
            {
                oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);        // chave
                oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);  // chave
                oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                forms_DATA _dataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[4].FindControl("datDataReajuste");
                oContratoResiduos.DataReajuste = _dataReajuste.Data;                        // chave
                string sLog = "";
                sLog = sLog + "Contrato: " + oContratoResiduos.CodigoContrato + " Data reajuste: " + oContratoResiduos.DataReajuste + " " +
                              "Cliente: " + oContratoResiduos.CodigoCliente + " Resíduo: " + oContratoResiduos.CodigoResiduo + " \n";
                sLog = sLog + "Caixa disponível: " + oContratoResiduos.CaixaDisponivel + "\n";
                sLog = sLog + "Tipo caixa: " + oContratoResiduos.TipoCaixa + " \n";
                sLog = sLog + "Frequência de coleta: " + oContratoResiduos.FrequenciaColeta + " \n";
                sLog = sLog + "Roteiro: " + oContratoResiduos.Roteiro + " \n";
                sLog = sLog + "Expressão: " + oContratoResiduos.expressao1CobrancaMensal + " \n";
                sLog = sLog + "Franquia: " + oContratoResiduos.FrequenciaColeta + " \n";
                sLog = sLog + "Expressão:" + oContratoResiduos.expressao2CobrancaMensal + " \n";
                sLog = sLog + "Periodicidade:" + oContratoResiduos.PeriodicidadeCobrancaMensal + " \n";
                sLog = sLog + "Valor excedente: " + oContratoResiduos.ValorExcedenteCobrancaMensal + " \n";
                sLog = sLog + "Expressão: " + oContratoResiduos.expressao1CobrancaPeso + " \n";
                sLog = sLog + "Valor unitário: " + oContratoResiduos.ValorUnitario + " \n";
                sLog = sLog + "Expressão: " + oContratoResiduos.expressao2CobrancaPeso + " \n";
                sLog = sLog + "Unidade: " + oContratoResiduos.Unidade + " \n";
                sLog = sLog + "Condição cobranca peso: " + oContratoResiduos.condicaoCobrancaPeso + "\n";
                sLog = sLog + "Franquia peso: " + oContratoResiduos.FranquiaCobrancaPeso + " \n";
                sLog = sLog + "Unidade peso" + oContratoResiduos.UnidadeCobrancaPeso + " \n";
                sLog = sLog + "Expressão: " + oContratoResiduos.expressao3CobrancaPeso + " \n";
                sLog = sLog + "Expressão:" + oContratoResiduos.expressao4CobrancaPeso + " \n";
                sLog = sLog + "Observação: " + oContratoResiduos.OBS + " \n";
                sLog = sLog + "Dias de coleta: " + oContratoResiduos.DiasColeta + " \n";
                sLog = sLog + "Particularidade: " + oContratoResiduos.Particularidade + " \n";
                sLog = sLog + "Mês/Ano base: " + oContratoResiduos.MesAnoBase + " \n";
                SalvarLogResiduos("Exclusão", sLog);

                lblMensagemResiduos.Text = oContratoResiduosDados.Excluir(oContratoResiduos.CodigoContrato, oContratoResiduos.CodigoResiduo,
                                                                          oContratoResiduos.CodigoCliente, oContratoResiduos.DataReajuste);
                if (lblMensagemResiduos.Text == "")
                    lblMensagemResiduos.Text = "Resíduo excluído com sucesso!";
                RefreshGradeResiduos(oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
            }
            else
            {
                lblMensagemResiduos.Text = "Exclusão inválida!";
            }
        }
    }
    protected void GradePesquisa_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string _campo = ddlFiltroPesquisa.Text;
            if (ddlFiltroPesquisa.Text == "Código")
                _campo = "";
            GradePesquisa.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(_campo, txtFiltroPesquisa.Text, _campo);
        }
        finally
        {
            GradePesquisa.PageIndex = e.NewPageIndex;
            GradePesquisa.DataBind();
        }
        this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
    }
    protected void GradePesquisa_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        GradePesquisa.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(geral.Ordem, txtFiltroPesquisa.Text, ddlFiltroPesquisa.Text);
        GradePesquisa.DataBind();
        this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
    }
    protected void GradePesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "DescricaoReduzida" && e.CommandArgument.ToString() != "Grupo" &&
            e.CommandArgument.ToString() != "CodigoIBAMA_Analitico" && e.CommandArgument.ToString() != "Classe")
        {
            if (Convert.ToInt32(e.CommandArgument) < 6)
            {
                hifCodigoResiduo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                TiraSelecionadoPesquisa();
                ImageButton ibnConsultar = (ImageButton)oGrade.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("ibnConsultar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionado.png";
                for (int i = 0; i < oGrade.Columns.Count; i++)
                {
                    oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                }
                TextBox _txtCodigoResiduo = (TextBox)GradeResiduos.Rows[GradeResiduos.Rows.Count - 1].Cells[2].FindControl("txtCodigoResiduo");
                _txtCodigoResiduo.Text = hifCodigoResiduo.Value;
                TextBox _txtDescricaoResiduo = (TextBox)GradeResiduos.Rows[GradeResiduos.Rows.Count - 1].Cells[3].FindControl("txtDescricaoResiduo");
                _txtDescricaoResiduo.Text = HttpUtility.HtmlDecode(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text);
                TextBox _txtCaixaDisponivel = (TextBox)GradeResiduos.Rows[GradeResiduos.Rows.Count - 1].Cells[4].FindControl("txtCxDisp");
                _txtCaixaDisponivel.Focus();
                lblMensagemResiduos.Text = "";
            }
        }
    }
    private void TiraSelecionadoPesquisa()
    {
        bool bInterCor = false;
        for (int i = 0; i < GradePesquisa.Rows.Count; i++)
        {
            ImageButton ibnConsultar = (ImageButton)GradePesquisa.Rows[i].FindControl("ibnSelecionar");
            if (ibnConsultar != null)
                ibnConsultar.ImageUrl = "~/Images/selecionar.png";
            if (bInterCor)
                bInterCor = false;
            else
                bInterCor = true;
            for (int j = 0; j < GradePesquisa.Columns.Count; j++)
            {
                if (bInterCor)
                    GradePesquisa.Rows[i].Cells[j].BackColor = System.Drawing.Color.White;
                else
                    GradePesquisa.Rows[i].Cells[j].BackColor = System.Drawing.Color.AliceBlue;
            }
        }
    }
    protected void btnOkPesquisa_Click(object sender, EventArgs e)
    {
        string _Campo = "";
        if (ddlFiltroPesquisa.Text == "Descrição Reduzida")
            _Campo = "DescricaoReduzida";
        else if (ddlFiltroPesquisa.Text == "Ativos")
            _Campo = "DescricaoReduzida";
        else
            _Campo = ddlFiltroPesquisa.Text;
        GradePesquisa.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(_Campo, txtFiltroPesquisa.Text, _Campo);
        GradePesquisa.DataBind();
        this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
    }
    private void SalvarResiduos(string pDataReajusteNova = "")
    {
        // Salvar registros
        try
        {
            for (int i = 0; GradeResiduos.Rows.Count > i;i++)
            {
                GridViewRow gvrResiduos = GradeResiduos.Rows[i];
                    
                TextBox _txtCodigoResiduo = (TextBox)gvrResiduos.Cells[2].FindControl("txtCodigoResiduo");
                if (hifCodigo.Value != "" && geral.IsNumeric(hifCodigo.Value) && _txtCodigoResiduo.Text != "" && geral.IsNumeric(_txtCodigoResiduo.Text) &&
                    intCodigoCliente.Valor != "" && geral.IsNumeric(intCodigoCliente.Valor))
                {
                    oContratoResiduos = new clsContratoResiduos();
                    oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);  // chave
                    oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);  // chave
                    oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                    forms_DATA _dataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[4].FindControl("datDataReajuste");
                        oContratoResiduos.DataReajuste = _dataReajuste.Data;                     // chave
                    if (pDataReajusteNova != "")
                    {
                        oContratoResiduos.DataReajuste = pDataReajusteNova;                      // chave
                    }
                    TextBox _txtCaixaDisponivel = (TextBox)gvrResiduos.Cells[5].FindControl("txtCxDisp");
                    if (_txtCaixaDisponivel.Text != "" && geral.IsNumeric(_txtCaixaDisponivel.Text))
                        oContratoResiduos.CaixaDisponivel = Convert.ToInt32(_txtCaixaDisponivel.Text);
                    DropDownList _ddlTipoCaixa = (DropDownList)gvrResiduos.Cells[5].FindControl("ddlTipoCx");
                    oContratoResiduos.TipoCaixa = _ddlTipoCaixa.SelectedItem.Text;
                    TextBox _txtQtFrequenciaColeta = (TextBox)gvrResiduos.Cells[6].FindControl("txtQtFrequenciaColeta");
                    DropDownList _ddlFrequenciaColeta = (DropDownList)gvrResiduos.Cells[6].FindControl("ddlFrequenciaColeta");
                    string[] _qtfranquia = _txtQtFrequenciaColeta.Text.Split(" "[0]);
                    if (_qtfranquia.Length > 0)
                        if (geral.IsNumeric(_qtfranquia[0]))
                            _txtQtFrequenciaColeta.Text = _qtfranquia[0];
                    if (_qtfranquia.Length > 1)
                        _ddlFrequenciaColeta.Text = _qtfranquia[1];
                    if (_qtfranquia.Length == 1)
                    {
                        if (!geral.IsNumeric(_qtfranquia[0]))
                            _ddlFrequenciaColeta.Text = _qtfranquia[0];
                    }
                    oContratoResiduos.FrequenciaColeta = _txtQtFrequenciaColeta.Text + " " + _ddlFrequenciaColeta.Text;
                    DropDownList ddlRoteiro = (DropDownList)gvrResiduos.Cells[7].FindControl("ddlRoteiro");
                    oContratoResiduos.Roteiro = ddlRoteiro.Text;
                    DropDownList ddlexpressao1CobrancaMensal = (DropDownList)gvrResiduos.Cells[7].FindControl("ddlexpressao1CobrancaMensal");
                    oContratoResiduos.expressao1CobrancaMensal = ddlexpressao1CobrancaMensal.Text;
                    forms_MOEDA moeFranquiaCobrancaMensal = (forms_MOEDA)gvrResiduos.Cells[8].FindControl("moeFranquiaCobrancaMensal");
                    oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor;
                    if (moeFranquiaCobrancaMensal.Valor != "")
                        oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(moeFranquiaCobrancaMensal.Valor);
                    DropDownList ddlexpressao2CobrancaMensal = (DropDownList)gvrResiduos.Cells[9].FindControl("ddlexpressao2CobrancaMensal");
                    oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
                    DropDownList ddlPeriodicidadeCobrancaMensal = (DropDownList)gvrResiduos.Cells[9].FindControl("ddlPeriodicidadeCobrancaMensal");
                    oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;

                    //forms_MOEDA moeValorContratoCobrancaMensal = (forms_MOEDA)gvrResiduos.Cells[10].FindControl("moeValorContratoCobrancaMensal");
                    //if (moeValorContratoCobrancaMensal.Valor != "")
                    //    oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(moeValorContratoCobrancaMensal.Valor);

                    forms_MOEDA moeValorExcedenteCobrancaMensal = (forms_MOEDA)gvrResiduos.Cells[11].FindControl("moeValorExcedenteCobrancaMensal");
                    if (moeValorExcedenteCobrancaMensal.Valor != "")
                        oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);
                    DropDownList ddlexpressao1CobrancaPeso = (DropDownList)gvrResiduos.Cells[12].FindControl("ddlexpressao1CobrancaPeso");
                    oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
                    forms_MOEDA moeValorUnitario = (forms_MOEDA)gvrResiduos.Cells[13].FindControl("moeValorUnitario");
                    if (moeValorUnitario.Valor != "")
                        oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitario.Valor) * 100;
                    DropDownList ddlexpressao2CobrancaPeso = (DropDownList)gvrResiduos.Cells[14].FindControl("ddlexpressao2CobrancaPeso");
                    oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
                    TextBox txtUnidade = (TextBox)gvrResiduos.Cells[15].FindControl("txtUnidade");
                    oContratoResiduos.Unidade = txtUnidade.Text;
                    DropDownList ddlcondicaoCobrancaPeso = (DropDownList)gvrResiduos.Cells[16].FindControl("ddlcondicaoCobrancaPeso");
                    oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
                    forms_MOEDA moeFranquiaCobrancaPeso = (forms_MOEDA)gvrResiduos.Cells[17].FindControl("moeFranquiaCobrancaPeso");
                    if (moeFranquiaCobrancaPeso.Valor != "")
                        oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(moeFranquiaCobrancaPeso.Valor);
                    TextBox txtUnidadeCobrancaPeso = (TextBox)gvrResiduos.Cells[18].FindControl("txtUnidadeCobrancaPeso");
                    oContratoResiduos.UnidadeCobrancaPeso = txtUnidadeCobrancaPeso.Text;
                    DropDownList ddlexpressao3CobrancaPeso = (DropDownList)gvrResiduos.Cells[19].FindControl("ddlexpressao3CobrancaPeso");
                    oContratoResiduos.expressao3CobrancaPeso = ddlexpressao3CobrancaPeso.Text;
                    DropDownList ddlexpressao4CobrancaPeso = (DropDownList)gvrResiduos.Cells[20].FindControl("ddlexpressao4CobrancaPeso");
                    oContratoResiduos.expressao4CobrancaPeso = ddlexpressao4CobrancaPeso.Text;
                    TextBox txtOBS = (TextBox)gvrResiduos.Cells[21].FindControl("txtOBS");
                    oContratoResiduos.OBS = txtOBS.Text;
                    TextBox txtDiasColeta = (TextBox)gvrResiduos.Cells[22].FindControl("txtDiasColeta");
                    oContratoResiduos.DiasColeta = txtDiasColeta.Text;
                    TextBox txtParticularidade = (TextBox)gvrResiduos.Cells[23].FindControl("txtParticularidade");
                    oContratoResiduos.Particularidade = txtParticularidade.Text;
                    TextBox txtMesAnoBase = (TextBox)gvrResiduos.Cells[23].FindControl("txtMesAnoBase");
                    oContratoResiduos.MesAnoBase = txtMesAnoBase.Text;
                    if (oContratoResiduos.CodigoContrato > 0 && oContratoResiduos.CodigoResiduo > 0 && oContratoResiduos.DataReajuste != "" && oContratoResiduos.CodigoCliente > 0)
                    {
                        string sLog = "";
                        sLog = sLog + "Contrato: " + oContratoResiduos.CodigoContrato + " Data reajuste: " + oContratoResiduos.DataReajuste + " " +
                                      "Cliente: " + oContratoResiduos.CodigoCliente + " Resíduo: " + oContratoResiduos.CodigoResiduo + " \n";
                        sLog = sLog + "Caixa disponível: " + oContratoResiduos.CaixaDisponivel + "\n";
                        sLog = sLog + "Tipo caixa: " + oContratoResiduos.TipoCaixa + " \n";
                        sLog = sLog + "Frequência de coleta: " + oContratoResiduos.FrequenciaColeta + " \n";
                        sLog = sLog + "Roteiro: " + oContratoResiduos.Roteiro + " \n";
                        sLog = sLog + "Expressão: " + oContratoResiduos.expressao1CobrancaMensal + " \n";
                        sLog = sLog + "Franquia: " + oContratoResiduos.FrequenciaColeta + " \n";
                        sLog = sLog + "Expressão:" + oContratoResiduos.expressao2CobrancaMensal + " \n";
                        sLog = sLog + "Periodicidade:" + oContratoResiduos.PeriodicidadeCobrancaMensal + " \n";
                        sLog = sLog + "Valor excedente: " + oContratoResiduos.ValorExcedenteCobrancaMensal + " \n";
                        sLog = sLog + "Expressão: " + oContratoResiduos.expressao1CobrancaPeso + " \n";
                        sLog = sLog + "Valor unitário: " + oContratoResiduos.ValorUnitario + " \n";
                        sLog = sLog + "Expressão: " + oContratoResiduos.expressao2CobrancaPeso + " \n";
                        sLog = sLog + "Unidade: " + oContratoResiduos.Unidade + " \n";
                        sLog = sLog + "Condição cobranca peso: " + oContratoResiduos.condicaoCobrancaPeso + "\n";
                        sLog = sLog + "Franquia peso: " + oContratoResiduos.FranquiaCobrancaPeso + " \n";
                        sLog = sLog + "Unidade peso" + oContratoResiduos.UnidadeCobrancaPeso + " \n";
                        sLog = sLog + "Expressão: " + oContratoResiduos.expressao3CobrancaPeso + " \n";
                        sLog = sLog + "Expressão:" + oContratoResiduos.expressao4CobrancaPeso + " \n";
                        sLog = sLog + "Observação: " + oContratoResiduos.OBS + " \n";
                        sLog = sLog + "Dias de coleta: " + oContratoResiduos.DiasColeta + " \n";
                        sLog = sLog + "Particularidade: " + oContratoResiduos.Particularidade + " \n";
                        sLog = sLog + "Mês/Ano base: " + oContratoResiduos.MesAnoBase + " \n";

                        if (oContratoResiduosDados.DadoExiste(oContratoResiduos.CodigoContrato, oContratoResiduos.CodigoResiduo, oContratoResiduos.DataReajuste, oContratoResiduos.CodigoCliente) == "Alterar")
                        {
                            SalvarLogResiduos("Alteração", sLog);
                            oContratoResiduosDados.Alterar(oContratoResiduos, oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
                        }
                        else
                        {
                            SalvarLogResiduos("Inclusão", sLog);
                            oContratoResiduosDados.Inserir(oContratoResiduos);
                        }
                    }
                }
            }
            RefreshGradeResiduos(oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
            lblMensagemResiduos.Text = "Dados salvos com sucesso.";
        }
        catch
        {
            lblMensagemResiduos.Text = "Dados inválidos!";
        }
    }
    protected void Copiar_Click(object sender, EventArgs e)
    {
        lblMensagemCopiar.Text = "";
        if (txtCodigoClienteCopiado.Text == "" || txtCodigoClienteCopiado.Text == "0")
            lblMensagemCopiar.Text = "Código cliente a ser copiado é inválido!";
        else if (hifCodigo.Value == "" || hifCodigo.Value == "0")
            lblMensagemCopiar.Text = "Selecionar contrato para ser copiado!";
        else
        {
            oCliente = new clsClientes();
            oClienteDados = new clsClienteDados();
            oClienteDados.PegaDados(oCliente, Convert.ToInt32(txtCodigoClienteCopiado.Text));
            if (oCliente.Nome == "" || oCliente.Nome == null)
            {
                lblMensagemCopiar.Text = "Código cliente a ser copiado é inválido (inexistente)!";
            }
            else
            {
                // salvar a partir do copiar
                oContratos = AtribuiDadosDoForm(oContratos);
                if (txtCodigoClienteCopiado.Text != "")
                {
                    oContratos.CodigoCliente = Convert.ToInt32(txtCodigoClienteCopiado.Text);
                    intCodigoCliente.Valor = txtCodigoClienteCopiado.Text;
                }
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                if (oContratos.DataInicio != "")
                {
                    SalvarLog("Inclusão");
                    string sRet = oContratosDados.Inserir(oContratos).ToLower();
                    if (sRet == "inserido")
                    {
                        wp_FilesClientesDados wpFiles = new wp_FilesClientesDados();
                        clsDocumentosPagina oDocumentos = new clsDocumentosPagina();
                        clsDocumentosPaginaDados oDocumentosDados = new clsDocumentosPaginaDados();
                        foreach (DataRow drDoc in oDocumentosDados.PegaDados(oDocumentos, 0, false).Rows)
                        {
                            if (drDoc["Tipo"].ToString() != "" && drDoc["Descricao"].ToString() != "")
                            {
                                if (!wpFiles.DadoExiste(oContratos.CodigoCliente, drDoc["Descricao"].ToString()))
                                {
                                    string sPer = DateTime.Now.Year.ToString();
                                    if (drDoc["Periodo"].ToString() != "")
                                        sPer = drDoc["Periodo"].ToString();
                                    wpFiles.Incluir(drDoc["descricao"].ToString(), drDoc["Tipo"].ToString(), oContratos.CodigoCliente, drDoc["Tipo"].ToString(), sPer);
                                }
                            }
                        }
                        lblMensagemCopiar.Text = "Contrato inserido com sucesso! Cliente: " + txtCodigoClienteCopiado.Text;
                        int iSeq = oContratosDados.PegaUltimoSequencial(Convert.ToInt32(txtCodigoClienteCopiado.Text));
                        Label lbl = new Label();
                        if (iSeq > 0)
                        {
                            oReajustes.Data = datDataInicio.Data;
                            oReajustes.Valor = Convert.ToDecimal(moeValorContrato.Valor);
                            oReajustes.NumeroContrato = intNumeroContrato.Valor;
                            oReajustes.Situacao = "INÍCIO CONTRATO";
                            oReajustes.TipoNegociacao = "";
                            oReajustes.CodigoContrato = iSeq;
                            if (oReajustesDados.Inserir(oReajustes))
                            {
                                intCodigoCliente.Valor = txtCodigoClienteCopiado.Text;
                                hifCodigo.Value = iSeq.ToString();
                                SalvarResiduos(oReajustes.Data);

                                geral.Ordem = "CodigoCliente";
                                ddlFiltro.Text = "Código Cliente";
                                txtFiltro.Text = txtCodigoClienteCopiado.Text;
                                FazOk(true);
                            }
                            else
                                lblMensagem.Text = "Erro ao inserir início contrato!";

                            Copiar.Enabled = false;
                            txtCodigoClienteCopiado.Text = "";
                        }
                    }
                }
            }
        }
    }
    private void SalvarLogDeCopiar(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Inclusão-Contratos";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Sequencial contrato nº: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data Registro: " + datDataRegistro.Data + " \n";
        oLog.Log = oLog.Log + "Data Início: " + datDataInicio.Data + " \n";
        oLog.Log = oLog.Log + "Nº Contrato: " + intNumeroContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Valor Contrato: " + moeValorContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Data Término: " + datDataTermino.Data + " \n";
        oLog.Log = oLog.Log + "Situação: " + "INÍCIO CONTRATO" + " \n";
        oLog.Log = oLog.Log + "Data Reajuste: " + datDataReajuste.Data + " \n";
        oLog.Log = oLog.Log + "Indice Reajuste: " + txtIndiceReajuste.Text + " \n";
        oLog.Log = oLog.Log + "Aniversario de Reajuste: " + txtAniversarioReajuste.Text + " \n";
        oLog.Log = oLog.Log + "Dia Vencimento: " + intDiaVencimento.Valor + " \n";
        oLog.Log = oLog.Log + "Observação: " + txtObservacao.Text + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void btnMostraCliente_Click(object sender, EventArgs e)
    {
        lblNomeCliente.Text = "";
        lblMensagemCopiar.Text = "";
        oClienteDados = new clsClienteDados();
        if (txtCodigoClienteCopiado.Text != "" && txtCodigoClienteCopiado.Text != null && txtCodigoClienteCopiado.Text != "&nbsp;")
        {
            oCliente.Codigo = Convert.ToInt32(txtCodigoClienteCopiado.Text);
            oCliente.NomeFantasia = oClienteDados.PegaNomeFantasia(Convert.ToInt32(txtCodigoClienteCopiado.Text));
            Session["Clientes"] = oCliente;
            if (oCliente.NomeFantasia != "")
            {
                txtCodigoClienteCopiado.Text = oCliente.Codigo.ToString("000000");
                lblNomeCliente.Text = oCliente.NomeFantasia;
            }
            else
            {
                lblMensagemCopiar.Text = "Código do cliente: " + txtCodigoClienteCopiado.Text + " inexistente";
                txtCodigoClienteCopiado.Text = "";
            }
        }
    }
}