using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class ContratosIE6 : System.Web.UI.Page
{
    clsContratos oContratos = new clsContratos();
    clsContratosDados oContratosDados = new clsContratosDados();
    clsContratosReajustes oReajustes = new clsContratosReajustes();
    clsContratosReajustesDados oReajustesDados = new clsContratosReajustesDados();
    clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
    clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    DataTable _dtReajustes = new DataTable();
    DataTable _dtResiduos = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            datDataReajuste.Text = DateTime.Now.Date.ToString("01/01/0001");
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
            }
            oUsuario = (clsUsuarios)Session["oUsuario"];
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                try
                {
                    if (oUsuario.Aplicativo == true)
                        menu.Visible = false;
                    else
                        menu.Visible = true;
                    try
                    {
                        _dt = oContratosDados.PegaDados(false);
                        Grade.DataSource = _dt;
                        Grade.DataBind();
                    }
                    finally
                    {
                        txtObservacao.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("Observacao");
                        txtMotivoRecisao.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("MotivoRecisao");
                        txtAniversarioReajuste.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("AniversarioReajuste");
                        txtIndiceReajuste.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("IndiceReajuste");
                        datDataReajuste2.Visible = false;
                        lblDataReajuste2.Visible = false;

                        TotalContratos();
                        PanelResiduos.Visible = false;
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

    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oContratosDados.DadoExiste(oContratos.Codigo);
        if (txtNomeFantasia.Text.Equals(""))
        {
            lblMensagem.Text = "Contratos inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oContratosDados.Excluir(oContratos.Codigo);
            lblMensagem.Text = "Contratos excluído com sucesso!";
            Grade.DataSource = oContratosDados.PreencheDataTableContratos("Codigo");
            Grade.DataBind();
        }
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Ok")
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
                if (lblMensagem.Text == "Incluir")
                {
                    oContratosDados.Inserir(oContratos);
                    lblMensagem.Text = "Contrato incluído com sucesso!";
                    ddlFiltro.Text = "Código Cliente";
                    txtFiltro.Text = oContratos.CodigoCliente.ToString();                                        
                    btnOk_Click(sender, e);
                    if (_dtReajustes.Rows.Count == 0)
                    {
                        // não há reajustes adicionar o primeiro como histórico inicial.
                        datDataReajuste1.Text = datDataInicio.Text;
                        moeValorContrato1.Text = moeValorContrato.Valor;
                        btnOkReajuste_Click(sender, e);
                    }

                }
                else if (lblMensagem.Text == "Alterar")
                {
                    // verificar se a senha informada é igual a gravada, caso contrário NÃO salvar senha nova
                    string msgErr = oContratosDados.Alterar(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        ddlFiltro.Text = "Código Cliente";
                        txtFiltro.Text = oContratos.CodigoCliente.ToString();
                        CancelarOperacao();
                        lblMensagem.Text = "Contrato alterado com sucesso!";
                        btnOk_Click(sender, e);
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Contratos";
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
                oContratosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "Contratos excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Contratos";
                Grade.DataSource = oContratosDados.PreencheDataTableContratos("Codigo asc");
                Grade.DataBind();
            }
        }
        LimpaCampos();
    }

    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Contratos";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {    
    
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Contratos";
        lblMensagem.Text = "";
        PanelReajuste.Visible = false;
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
                    if (hifCodigo.Value != "")
                    {
                        GradeReajustes.DataSource = oReajustesDados.PreencheDataTable("Data desc ", Convert.ToInt32(hifCodigo.Value));
                        GradeReajustes.DataBind();
                        intSequencial.Valor = "";
                        intCodigoCliente1.Valor = intCodigoCliente.Valor;
                        GradeResiduos.DataSource = "";
                        GradeResiduos.DataBind();
                        LimpaCamposContratoResiduos();
                        PanelResiduos.Visible = false;
                        PanelReajuste.Visible = true;
                        LimpaCamposReajustes();
                        datDataReajuste1.Text = datDataReajuste.Text;
                        datDataReajuste2.Text = "";
                        datDataReajuste2.Visible = true;
                        lblDataReajuste2.Visible = true;
                        intNumeroContrato1.Valor = intNumeroContrato.Valor;
                        moePercentualReajuste.Enabled = true;
                        moePercentualReajuste.Text = "";
                        btnOkReajuste.Focus();
                    }
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
    protected void AtribuiDadosDaClasse(clsContratos pContratos)
    {
        txtNome.Text = pContratos.Nome;
        txtCNPJ_CPF.Text = pContratos.CNPJ_CPF;
        txtNomeFantasia.Text = pContratos.NomeFantasia;       
        txtObservacao.Text = pContratos.Observacao;
        if (pContratos.DataRecisao == "01/01/0001" || pContratos.DataRecisao == "01/01/0100" || pContratos.DataRecisao == null || pContratos.DataRecisao == "")
            datDataRecisao.Text = "";
        else
            datDataRecisao.Text = Convert.ToDateTime(pContratos.DataRecisao).ToString("yyyy-MM-dd");
        ddlDocumentoRecisao.Text = pContratos.SituacaoRecisao;
        if (pContratos.DataRegistro == "01/01/0001" || pContratos.DataRegistro == "01/01/0100" || pContratos.DataRegistro == null  || pContratos.DataRegistro == "")
            datDataRegistro.Text = "";
        else
            datDataRegistro.Text = Convert.ToDateTime(pContratos.DataRegistro).ToString("yyyy-MM-dd");
        txtMotivoRecisao.Text = pContratos.MotivoRescisao;
        intNumeroContrato.Valor = pContratos.NumeroContrato.ToString();
        if (pContratos.DataInicio == "01/01/0001" || pContratos.DataInicio == "01/01/0100" || pContratos.DataInicio == null || pContratos.DataInicio == "")
            datDataInicio.Text = "";
        else
            datDataInicio.Text = Convert.ToDateTime(pContratos.DataInicio).ToString("yyyy-MM-dd");
        if (pContratos.DataTermino == "01/01/0001" || pContratos.DataTermino == "01/01/0100" || pContratos.DataTermino == null || pContratos.DataTermino == "")
            datDataTermino.Text = "";
        else
            datDataTermino.Text = Convert.ToDateTime(pContratos.DataTermino).ToString("yyyy-MM-dd");
        if (pContratos.DataReajuste == "01/01/0001" || pContratos.DataReajuste == "01/01/0100" || pContratos.DataReajuste == null || pContratos.DataReajuste == "")
            datDataReajuste.Text = "";
        else
            datDataReajuste.Text = Convert.ToDateTime(pContratos.DataReajuste).ToString("yyyy-MM-dd");
        txtAniversarioReajuste.Text = pContratos.AniversarioReajuste;
        txtIndiceReajuste.Text = pContratos.IndiceReajuste;
        intCaixasLocadas.Valor = pContratos.NumeroCaixasLocadas.ToString();
        moeValorContrato.Valor = pContratos.ValorContrato.ToString();
        hifValorContrato.Value = moeValorContrato.Valor;
        intDiaVencimento.Valor = pContratos.DiaVencimento.ToString();
    }

    protected void LimpaCampos()
    {
        intCodigoCliente.Valor = "";
        datDataReajuste.Text = "";
        txtNome.Text = "";
        txtNomeFantasia.Text = "";
        txtCNPJ_CPF.Text = "";
        txtObservacao.Text = "";
        datDataRecisao.Text = "";
        ddlDocumentoRecisao.Text = "";
        datDataRegistro.Text = "";
        txtMotivoRecisao.Text = "";
        intNumeroContrato.Valor = "";
        datDataInicio.Text = "";
        datDataTermino.Text = "";
        txtAniversarioReajuste.Text = "";
        datDataReajuste.Text = "";
        txtIndiceReajuste.Text = "";
        intCaixasLocadas.Valor = "";
        moeValorContrato.Valor = "";
        hifValorContrato.Value = "";
        intDiaVencimento.Valor = "";
        Grade.Visible = true;
        GradeReajustes.DataSource = "";
        GradeReajustes.DataBind();
        GradeResiduos.DataSource = "";
        GradeResiduos.DataBind();
    }
    protected clsContratos AtribuiDadosDoForm(clsContratos pContratos)
    {
        if (geral.IsNumeric(intCodigoCliente.Valor))
        {
            pContratos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
            pContratos.Observacao = txtObservacao.Text;
            pContratos.DataRecisao = datDataRecisao.Text;
            pContratos.SituacaoRecisao = ddlDocumentoRecisao.Text;
            pContratos.DataRegistro = datDataRegistro.Text;
            pContratos.MotivoRescisao = txtMotivoRecisao.Text;
            if (intNumeroContrato.Valor != "")
                pContratos.NumeroContrato = Convert.ToInt32(intNumeroContrato.Valor);
            pContratos.DataInicio = datDataInicio.Text;
            pContratos.DataTermino = datDataTermino.Text;
            if (txtAniversarioReajuste.Text != "")
                pContratos.AniversarioReajuste = txtAniversarioReajuste.Text;
            pContratos.DataReajuste = datDataReajuste.Text;
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
        ViewState["Data1Cadastro"] = datDataReajuste.Text;
        ViewState["Nome"] = txtNomeFantasia.Text;
        ViewState["Descricao"] = txtObservacao.Text;
    }
    protected void ViewStateSetForm()
    {
        datDataReajuste.Text = ViewState["DataCadastro"].ToString();
        txtNomeFantasia.Text = ViewState["Nome"].ToString();
        txtObservacao.Text = ViewState["Descricao"].ToString();        
    }

    private void CancelarOperacao()
    {
        lblTitulo.Text = "&nbsp;Cadastro de Contratos";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
        GradeReajustes.DataSource = "";
        GradeReajustes.DataBind();
        GradeResiduos.DataSource = "";
        GradeResiduos.DataBind();
        PanelReajuste.Visible = false;
        PanelResiduos.Visible = false;
        //moeValorContrato1.Enabled = true;
        moePercentualReajuste.Enabled = true;
        moePercentualReajuste.Text = "";
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
    }
    protected void btnOk_Click(object sender, EventArgs e)
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
            PanelReajuste.Controls.Clear();
            PanelReajuste.Visible = false;
            if (hifCodigo.Value != "")
            {
                _dtReajustes = new DataTable();
                _dtReajustes = oReajustesDados.PreencheDataTable("Data desc ", Convert.ToInt32(hifCodigo.Value));
                GradeReajustes.DataSource = _dtReajustes;
                GradeReajustes.DataBind();
            }
        }
    }
    
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        ViewStateGetForm();
    }

    protected void GradeReajustes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "CodigoContrato" && e.CommandArgument.ToString() != "Data" && 
            e.CommandArgument.ToString() != "NumeroContrato" && e.CommandArgument.ToString() != "Situacao" && 
            e.CommandArgument.ToString() != "TipoNegociacao" && e.CommandArgument.ToString() != "Valor" && 
            e.CommandArgument.ToString() != "Particularidade" && e.CommandArgument.ToString() != "Sequencial")
        {
            oContratoResiduos.DataReajuste = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            intSequencial.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
            oReajustesDados.PegaDados(oReajustes, Convert.ToInt32(hifCodigo.Value), oContratoResiduos.DataReajuste, Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text));

            intCodigoCliente1.Valor = intCodigoCliente.Valor;
            datDataReajuste1.Text = Convert.ToDateTime(oContratoResiduos.DataReajuste).ToString("yyyy-MM-dd");
            datDataReajuste2.Text = "";
            datDataReajuste2.Visible = false;
            lblDataReajuste2.Visible = false;

            moeValorContrato1.Text = oReajustes.Valor.ToString();
            intNumeroContrato1.Valor = oReajustes.NumeroContrato;
            txtSituacao1.Text = oReajustes.Situacao;
            txtTipoNegociacao1.Text = oReajustes.TipoNegociacao;

            //moeValorContrato1.Enabled = false;
            moePercentualReajuste.Enabled = false;
            moePercentualReajuste.Text = "";

            if (Convert.ToInt32(e.CommandArgument) > 0)
            { 
                intSequencial.ForeColor = System.Drawing.Color.Red;
                intCodigoCliente1.ForeColor = System.Drawing.Color.Red;
                datDataReajuste1.ForeColor = System.Drawing.Color.Red;
                moeValorContrato1.ForeColor = System.Drawing.Color.Red;
                intNumeroContrato1.ForeColor = System.Drawing.Color.Red;
                txtSituacao1.ForeColor = System.Drawing.Color.Red;
                txtTipoNegociacao1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                intSequencial.ForeColor = System.Drawing.Color.Black;
                intCodigoCliente1.ForeColor = System.Drawing.Color.Black;
                datDataReajuste1.ForeColor = System.Drawing.Color.Black;
                moeValorContrato1.ForeColor = System.Drawing.Color.Black;
                intNumeroContrato1.ForeColor = System.Drawing.Color.Black;
                txtSituacao1.ForeColor = System.Drawing.Color.Black;
                txtTipoNegociacao1.ForeColor = System.Drawing.Color.Black;
            }

            oContratos = oContratosDados.PegaDados(oContratos, 0, Convert.ToInt32(intCodigoCliente.Valor));
            _dtResiduos = new DataTable();
            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratos.Codigo, oContratoResiduos.DataReajuste, Convert.ToInt32(intCodigoCliente.Valor));
            decimal _ValorTotalContratoCobrancaMensal = 0;
            foreach(DataRow _dr in _dtResiduos.Rows)
            {
                if (_dr["ValorUnitario"].ToString() != "")
                    if (Convert.ToDecimal(_dr["ValorUnitario"]) > 0)
                        _dr["ValorUnitario"] = Convert.ToDecimal(_dr["ValorUnitario"]) / 100;
                if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                    _ValorTotalContratoCobrancaMensal = _ValorTotalContratoCobrancaMensal + Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
            }
            _dtResiduos.NewRow();
            _dtResiduos.Rows.Add();
            _dtResiduos.Rows[_dtResiduos.Rows.Count-1]["ValorContratoCobrancaMensal"] = _ValorTotalContratoCobrancaMensal;
            GradeResiduos.DataSource = _dtResiduos;
            GradeResiduos.DataBind();

            if (GradeReajustes.Rows.Count > 0)
            {
                for (int i = 0; i < GradeReajustes.Rows.Count - 1; i++)
                {
                    GradeReajustes.Rows[i + 1].Cells[1].Text = "";
                    for (int j = 0; j <= 7; j++)
                    {
                        GradeReajustes.Rows[i+1].Cells[j].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            PanelResiduos.Visible = true;
            intCodigoContrato_R.Valor = oContratos.Codigo.ToString();
            LimpaCamposContratoResiduos();
            lblTituloResiduos.Text = "Inclusão de Resíduos Contratados";
            btnOkResiduos.Text = "Ok";
            btnOkResiduos.Focus();
        }
    }
    protected void GradeReajustes_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        if (hifCodigo.Value != "")
        {
            GradeReajustes.DataSource = oReajustesDados.PreencheDataTable(geral.Ordem, Convert.ToInt32(hifCodigo.Value));
            GradeReajustes.DataBind();
            GradeResiduos.DataSource = "";
            GradeResiduos.DataBind();
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
        }
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
                GradeResiduos.DataSource = _dtResiduos;
                GradeResiduos.DataBind();
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
    protected void ibnMudar1_Click(object sender, ImageClickEventArgs e)
    {
        lblTituloResiduos.Text = "Alteração de Resíduos";
        lblTituloReajustes.Text = "Alteração de Reajuste";
        PanelReajuste.Visible = true;
        btnOkReajuste.Text = "Ok";
    }
    protected void btnCancelaAlteracaoReajuste_Click(object sender, EventArgs e)
    {
        CancelarOperacao();
    }

    protected void GradeReajustes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > 0)
        {
            e.Row.Cells[1].Text = "";
            for (int i = 0; i <= 7; i++)
            {
                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnOkReajuste_Click(object sender, EventArgs e)
    {
        if (geral.IsNumeric(hifCodigo.Value) && hifCodigo.Value != "" && datDataReajuste1.Text != "" && moeValorContrato1.Text != "")
        {
            Label lbl = new Label();
            oContratoResiduos.DataReajuste = datDataReajuste1.Text;
            intCodigoCliente1.Valor = intCodigoCliente.Valor;
            oReajustes.Data = datDataReajuste1.Text;
            oReajustes.Valor = Convert.ToDecimal(moeValorContrato1.Text);
            oReajustes.NumeroContrato = intNumeroContrato1.Valor;
            oReajustes.Situacao = txtSituacao1.Text;
            oReajustes.TipoNegociacao = txtTipoNegociacao1.Text;
            oReajustes.CodigoContrato = Convert.ToInt32(hifCodigo.Value);
            if (intSequencial.Valor == "")
                intSequencial.Valor = "0";
            if (btnOkReajuste.Text == "Ok")
            {
                if (oReajustesDados.DadoExiste(Convert.ToInt32(intSequencial.Valor)) == "Alterar")
                    oReajustesDados.Alterar(oReajustes, 0, 0, Convert.ToInt32(intSequencial.Valor));
                else
                {
                    // passar dados dos residuos contratados no último reajuste, se existir
                    if (oReajustesDados.Inserir(oReajustes))
                    {
                        // pega resíduos do último reajuste ou contrato
                        if (GradeReajustes.Rows.Count > 0)
                        {
                            //datDataReajuste.Text = Convert.ToDateTime(datDataReajuste1.Text).AddYears(1).ToString("yyyy-MM-dd");
                            datDataReajuste.Text = datDataReajuste2.Text;
                            oContratosDados.AlterarDataProximoReajuste(datDataReajuste.Text, oReajustes.CodigoContrato, Convert.ToInt32(intCodigoCliente1.Valor));

                            GradeResiduos.DataSource = "";
                            GradeResiduos.DataBind();
                            string _dataReajuste = GradeReajustes.Rows[0].Cells[2].Text;
                            moeValorContrato.Valor = moeValorContrato1.Text;
                            oContratosDados.AlterarValorContrato(Convert.ToDecimal(moeValorContrato1.Text), oReajustes.CodigoContrato, Convert.ToInt32(intCodigoCliente.Valor));

                            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("CodigoResiduo", oReajustes.CodigoContrato, _dataReajuste, Convert.ToInt32(intCodigoCliente.Valor));
                            foreach(DataRow _dr in _dtResiduos.Rows)
                            {
                                oContratoResiduos = new clsContratoResiduos();
                                
                                oContratoResiduos.DataReajuste = oReajustes.Data;
                                oContratoResiduos.CodigoContrato = oReajustes.CodigoContrato;
                                oContratoResiduos.CodigoResiduo = Convert.ToInt32(_dr["CodigoResiduo"].ToString());
                                oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
                                
                                if (_dr["CaixaDisponivel"].ToString() != "")
                                    oContratoResiduos.CaixaDisponivel = Convert.ToInt32(_dr["CaixaDisponivel"].ToString());
                                oContratoResiduos.DescricaoReduzidaResiduo = _dr["DescricaoReduzidaResiduo"].ToString();
                                oContratoResiduos.condicaoCobrancaPeso = _dr["condicaoCobrancaPeso"].ToString();
                                oContratoResiduos.DiasColeta = _dr["DiasColeta"].ToString();
                                oContratoResiduos.expressao1CobrancaMensal = _dr["expressao1CobrancaMensal"].ToString();
                                oContratoResiduos.expressao1CobrancaPeso = _dr["expressao1CobrancaPeso"].ToString();
                                oContratoResiduos.expressao2CobrancaMensal = _dr["expressao2CobrancaMensal"].ToString();
                                oContratoResiduos.expressao2CobrancaPeso = _dr["expressao2CobrancaPeso"].ToString();
                                oContratoResiduos.expressao3CobrancaPeso = _dr["expressao3CobrancaPeso"].ToString();
                                oContratoResiduos.expressao4CobrancaPeso = _dr["expressao4CobrancaPeso"].ToString();
                                if (_dr["FranquiaCobrancaPeso"].ToString() != "")
                                    oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(_dr["FranquiaCobrancaPeso"].ToString());
                                oContratoResiduos.UnidadeCobrancaPeso = _dr["UnidadeCobrancaPeso"].ToString();
                                oContratoResiduos.Franquia = _dr["Franquia"].ToString();
                                oContratoResiduos.Franquia1CobrancaMensal = _dr["Franquia1CobrancaMensal"].ToString();
                                oContratoResiduos.FrequenciaColeta = _dr["FrequenciaColeta"].ToString();

                                oContratoResiduos.MesAnoBase = _dr["MesAnoBase"].ToString();
                                oContratoResiduos.OBS = _dr["OBS"].ToString();
                                oContratoResiduos.Particularidade = _dr["Particularidade"].ToString();
                                oContratoResiduos.PeriodicidadeCobrancaMensal = _dr["PeriodicidadeCobrancaMensal"].ToString();
                                if (_dr["QuantidadeFranquia"].ToString() != "")
                                    oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(_dr["QuantidadeFranquia"]);
                                oContratoResiduos.Roteiro = _dr["Roteiro"].ToString();
                                oContratoResiduos.TipoCaixa = _dr["TipoCaixa"].ToString();
                                oContratoResiduos.Unidade = _dr["Unidade"].ToString();
                                if (_dr["ValorExcedenteCobrancaMensal"].ToString() != "")
                                    oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(_dr["ValorExcedenteCobrancaMensal"]);
                                if (_dr["ValorUnitario"].ToString() != "")
                                    oContratoResiduos.ValorUnitario = Convert.ToDecimal(_dr["ValorUnitario"]);
                                if (_dr["ValorUnitarioCobrancaMensal"].ToString() != "")
                                    oContratoResiduos.ValorUnitarioCobrancaMensal = Convert.ToDecimal(_dr["ValorUnitarioCobrancaMensal"]);
                                if (moePercentualReajuste.Text != "")
                                {
                                    oContratoResiduos.ValorUnitario = ((Convert.ToDecimal(moePercentualReajuste.Text) / 100) + 1) * oContratoResiduos.ValorUnitario;
                                    oContratoResiduos.ValorUnitarioCobrancaMensal = ((Convert.ToDecimal(moePercentualReajuste.Text) / 100) + 1) * oContratoResiduos.ValorUnitarioCobrancaMensal;
                                    oContratoResiduos.ValorExcedenteCobrancaMensal = ((Convert.ToDecimal(moePercentualReajuste.Text) / 100) + 1) * oContratoResiduos.ValorExcedenteCobrancaMensal;
                                }
                                if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                                    oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"].ToString());

                                oContratoResiduosDados.Inserir(oContratoResiduos);

                            }
                        }
                    }
                }
                moePercentualReajuste.Text = "";
                PanelReajuste.Controls.Clear();
                PanelReajuste.Font.Size = 10;
                lbl.Text = "<br />&nbsp;Salvo com sucesso<br /><br />";
                PanelReajuste.Controls.Add(lbl);
            }
            else if (btnOkReajuste.Text == "Confirma") // exclusão
            {
                PanelReajuste.Controls.Clear();
                PanelReajuste.Font.Size = 10;
                bool bExcluiuTudo = false;
                if (oReajustesDados.ExcluirPeloSequencial(Convert.ToInt32(intSequencial.Valor)))
                {
                    if (oContratoResiduosDados.Excluir(oReajustes.CodigoContrato, Convert.ToInt32(intCodigoCliente.Valor), oReajustes.Data) == "")
                    {
                        bExcluiuTudo = true;
                        moeValorContrato.Valor = GradeReajustes.Rows[1].Cells[3].Text;
                        oContratosDados.AlterarValorContrato(Convert.ToDecimal(moeValorContrato.Valor), oReajustes.CodigoContrato, Convert.ToInt32(intCodigoCliente.Valor));
                        datDataReajuste.Text = Convert.ToDateTime(datDataReajuste1.Text).ToString("yyyy-MM-dd");
                        oContratosDados.AlterarDataProximoReajuste(datDataReajuste.Text, oReajustes.CodigoContrato, Convert.ToInt32(intCodigoCliente1.Valor));
                    }
                }
                if (bExcluiuTudo)
                    lbl.Text = "<br />&nbsp;Exclusão realizada com sucesso<br /><br />";
                if (!bExcluiuTudo)
                    lbl.Text = "<br />&nbsp;Exclusão inválida!<br /><br />";
                moePercentualReajuste.Text = "";
                PanelReajuste.Controls.Add(lbl);
                btnOkReajuste.Text = "Ok";

            }
            if (hifCodigo.Value != "")
            {
                GradeReajustes.DataSource = oReajustesDados.PreencheDataTable("Data desc", Convert.ToInt32(hifCodigo.Value));
                GradeReajustes.DataBind();
                GradeResiduos.DataSource = "";
                GradeResiduos.DataBind();
            }
        }
        else
        {
            Label lbl = new Label();
            lbl.Text = "<br />&nbsp;Reajuste inválido!<br /><br />";
            PanelReajuste.Controls.Add(lbl);
        }
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

    private void LimpaCamposReajustes()
    {
        lblTituloReajustes.Text = "Inclusão de Reajustes";
        intSequencial.Valor = "";
        datDataReajuste1.Text = "";
        moeValorContrato1.Text = "";
        intNumeroContrato1.Valor = "";
        txtSituacao1.Text = "";
        txtTipoNegociacao1.Text = "";

        intSequencial.ForeColor = System.Drawing.Color.Black;
        intCodigoCliente1.ForeColor = System.Drawing.Color.Black;
        datDataReajuste1.ForeColor = System.Drawing.Color.Black;
        moeValorContrato1.ForeColor = System.Drawing.Color.Black;
        intNumeroContrato1.ForeColor = System.Drawing.Color.Black;
        txtSituacao1.ForeColor = System.Drawing.Color.Black;
        txtTipoNegociacao1.ForeColor = System.Drawing.Color.Black;
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
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Style.Add("background-color", "Black");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BorderWidth = 1;
            HeaderCell.Text = "DADOS PARA COBRANÇA MENSAL";
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
            HeaderCell.Text = "DADOS DA COBRANÇA POR PESO/VOLUME";
            HeaderCell.ColumnSpan = 9;
            HeaderCell.Font.Bold = true;
            HeaderGridRow.Cells.Add(HeaderCell);

            GradeResiduos.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void GradeResiduos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[14].Text = "";
            if (e.Row.Cells[13].Text == "" && e.Row.Cells[19].Text.ToUpper() == "CX")
                e.Row.Cells[13].Text = e.Row.Cells[17].Text;
            if ((e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;") && e.Row.Cells[10].Text != "&nbsp;" && e.Row.Cells[10].Text != "" &&
                e.Row.Cells[13].Text != "&nbsp;" && e.Row.Cells[13].Text != "")
            {
                e.Row.Cells[14].Text = (Convert.ToDecimal(e.Row.Cells[10].Text) * Convert.ToDecimal(e.Row.Cells[13].Text)).ToString("N2");
            }
            /*
            if (e.Row.Cells[9].Text == "")
                e.Row.Cells[9].Text = "até";
            if (e.Row.Cells[11].Text == "")
                e.Row.Cells[11].Text = "coleta(s)&nbsp;por";
            if (e.Row.Cells[16].Text == "")
                e.Row.Cells[16].Text = "cobrar";
            if (e.Row.Cells[18].Text == "")
                e.Row.Cells[18].Text = "por";
            if (e.Row.Cells[20].Text == "")
                e.Row.Cells[20].Text = "excedente&nbsp;a";
            if (e.Row.Cells[23].Text == "")
                e.Row.Cells[23].Text = "por";
            */
            if (intCodigoCliente1.ForeColor == System.Drawing.Color.Red)
            {
                for (int i = 2; i <= GradeResiduos.Columns.Count - 1; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnCancelarResiduos_Click(object sender, EventArgs e)
    {
        //moeValorContrato1.Enabled = true;
        moePercentualReajuste.Enabled = true;
        moePercentualReajuste.Text = "";
        LimpaCamposContratoResiduos();
        if (GradeReajustes.Rows.Count > 0)
        {
            for (int i = 0; i < GradeReajustes.Rows.Count - 1; i++)
            {
                GradeReajustes.Rows[i + 1].Cells[1].Text = "";
                for (int j = 0; j <= 7; j++)
                {
                    GradeReajustes.Rows[i + 1].Cells[j].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        lblTituloResiduos.Text = "Inclusão de Resíduos Contratados";
        lblMensagemResiduos.Text = "";
        btnOkResiduos.Text = "Ok";
        btnOkResiduos.Focus();
    }

    protected void ibnMudar1_Click1(object sender, ImageClickEventArgs e)
    {
        PanelResiduos.Visible = true;
        lblTituloResiduos.Text = "Alteração de Resíduos Contratados";
        btnOkResiduos.Focus();
    }

    private void AtribuiDadosContratoResiduosParaClasse()
    {
        oContratoResiduos = new clsContratoResiduos();
        if (intCxDisp.Valor != "")
            oContratoResiduos.CaixaDisponivel = Convert.ToInt32(intCxDisp.Valor);
        oContratoResiduos.CodigoCaminhao = 0;
        if (intCodigoCliente1.Valor != "")
            oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente1.Valor);
        if (intCodigoCliente.Valor != "")
            oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
        if (intCodigoContrato_R.Valor != "")
            oContratoResiduos.CodigoContrato = Convert.ToInt32(intCodigoContrato_R.Valor);
        if (GRUPORESIDUO_R.Valor != "")
            oContratoResiduos.CodigoResiduo = Convert.ToInt32(GRUPORESIDUO_R.Valor);
        
        if (datDataReajuste1.Text != "")
            oContratoResiduos.DataReajuste = datDataReajuste1.Text;
        oContratoResiduos.DescricaoReduzidaResiduo = GRUPORESIDUO_R.Texto;
        oContratoResiduos.DiasColeta = txtDiasColeta.Text;
        oContratoResiduos.expressao1CobrancaMensal = ddlexpressao1CobrancaMensal.Text;
        oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
        oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
        oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
        oContratoResiduos.expressao3CobrancaPeso = ddlexpressao3CobrancaPeso.Text;
        oContratoResiduos.expressao4CobrancaPeso = ddlexpressao4CobrancaPeso.Text;
        if (moeFranquiaCobrancaPeso.Valor != "")
            oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(moeFranquiaCobrancaPeso.Valor);
        oContratoResiduos.UnidadeCobrancaPeso = txtUnidade2CobrancaPeso.Text;
        oContratoResiduos.Franquia = moeFranquiaCobrancaPeso.Valor;        
        oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor.Replace(".", "");        
        oContratoResiduos.MesAnoBase = txtMesAnoBase.Text;
        oContratoResiduos.OBS = txtObservacao_R.Text;
        oContratoResiduos.Particularidade = txtParticularidade.Text;
        oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;
        oContratoResiduos.FrequenciaColeta = intQtFrequenciaColeta.Valor + " " + txtFrequenciaColeta.Text;
        oContratoResiduos.Roteiro = txtRoteiro.Text;
        oContratoResiduos.TipoCaixa = txtTipoCx.Text;
        oContratoResiduos.Unidade = txtUnidadeCobrancaPeso.Text;
        if (moeValorExcedenteCobrancaMensal.Valor != "")
            oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);        
        if (moeValorUnitarioCobrancaPeso.Valor != "")
            oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitarioCobrancaPeso.Valor) * 100;
        if (moeValorUnitarioCobrancaMensal.Valor != "" && moeValorUnitarioCobrancaMensal.Valor.ToLower() != "nan")
            oContratoResiduos.ValorUnitarioCobrancaMensal = Convert.ToDecimal(moeValorUnitarioCobrancaMensal.Valor);
        if (moeValorContratoCobrancaMensal.Valor != "")
            oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(moeValorContratoCobrancaMensal.Valor);
        oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
    }
    private void AtribuiDadosContratoResiduosParaCamposForm(clsContratoResiduos pContratoResiduos)
    {
        intCxDisp.Valor = pContratoResiduos.CaixaDisponivel.ToString();
        if (pContratoResiduos.CodigoCliente > 0)
            intCodigoCliente1.Valor = pContratoResiduos.CodigoCliente.ToString();
        intCodigoContrato_R.Valor = pContratoResiduos.CodigoContrato.ToString();
        GRUPORESIDUO_R.Valor = pContratoResiduos.CodigoResiduo.ToString();
        GRUPORESIDUO_R.WidthLabel(340);
        ddlexpressao4CobrancaPeso.Text = pContratoResiduos.expressao4CobrancaPeso;
        moeFranquiaCobrancaPeso.Valor = pContratoResiduos.FranquiaCobrancaPeso.ToString("N2");
        txtUnidade2CobrancaPeso.Text = pContratoResiduos.UnidadeCobrancaPeso;
        txtDataInicioReajuste.Text = Convert.ToDateTime(pContratoResiduos.DataReajuste).ToString("yyyy-MM-dd");
        GRUPORESIDUO_R.Texto = pContratoResiduos.DescricaoReduzidaResiduo;
        txtDiasColeta.Text = pContratoResiduos.DiasColeta;
        ddlexpressao1CobrancaMensal.Text = pContratoResiduos.expressao1CobrancaMensal;
        ddlexpressao2CobrancaPeso.Text = pContratoResiduos.expressao2CobrancaPeso;
        ddlexpressao2CobrancaMensal.Text = pContratoResiduos.expressao2CobrancaMensal;
        ddlexpressao1CobrancaPeso.Text = pContratoResiduos.expressao1CobrancaPeso;
        ddlcondicaoCobrancaPeso.Text = pContratoResiduos.condicaoCobrancaPeso;
        moeFranquiaCobrancaPeso.Valor = pContratoResiduos.Franquia;
        txtUnidade2CobrancaPeso.Text = pContratoResiduos.UnidadeCobrancaPeso;
        moeFranquiaCobrancaMensal.Valor = pContratoResiduos.Franquia1CobrancaMensal.Replace(".", "");
        txtMesAnoBase.Text = pContratoResiduos.MesAnoBase;
        txtObservacao_R.Text = pContratoResiduos.OBS;
        txtParticularidade.Text = pContratoResiduos.Particularidade;
        ddlPeriodicidadeCobrancaMensal.Text = pContratoResiduos.PeriodicidadeCobrancaMensal;
        if (pContratoResiduos.FrequenciaColeta != "")
        {
            intQtFrequenciaColeta.Valor = "";
            txtFrequenciaColeta.Text = "";
            string[] _qtfranquia = pContratoResiduos.FrequenciaColeta.Split(" "[0]);
            if (_qtfranquia.Length > 0)
                if (geral.IsNumeric(_qtfranquia[0]))
                    intQtFrequenciaColeta.Valor = _qtfranquia[0];
            if (_qtfranquia.Length > 1)
                txtFrequenciaColeta.Text = _qtfranquia[1];
            if (_qtfranquia.Length == 1)
            {
                if (!geral.IsNumeric(_qtfranquia[0]))
                    txtFrequenciaColeta.Text = _qtfranquia[0];
            }
        }
        txtRoteiro.Text = pContratoResiduos.Roteiro;
        txtTipoCx.Text = pContratoResiduos.TipoCaixa;
        ddlexpressao3CobrancaPeso.Text = pContratoResiduos.expressao3CobrancaPeso;
        txtUnidadeCobrancaPeso.Text = pContratoResiduos.Unidade;
        moeValorExcedenteCobrancaMensal.Valor = pContratoResiduos.ValorExcedenteCobrancaMensal.ToString();
        moeValorUnitarioCobrancaPeso.Valor = pContratoResiduos.ValorUnitario.ToString();
        moeValorUnitarioCobrancaMensal.Valor = pContratoResiduos.ValorUnitarioCobrancaMensal.ToString();
        moeValorContratoCobrancaMensal.Valor = pContratoResiduos.ValorContratoCobrancaMensal.ToString();
    }
    private void CorCamposContratoResiduos(System.Drawing.Color pColor)
    {
        intCxDisp.ForeColor = pColor;
        intCodigoCliente1.ForeColor = pColor;
        intCodigoContrato_R.ForeColor = pColor;
        GRUPORESIDUO_R.ForeColor = pColor;
        ddlexpressao4CobrancaPeso.ForeColor = pColor;
        moeFranquiaCobrancaPeso.ForeColor = pColor;
        txtDataInicioReajuste.ForeColor = pColor;
        txtDiasColeta.ForeColor = pColor;
        ddlexpressao1CobrancaMensal.ForeColor = pColor;
        ddlexpressao1CobrancaPeso.ForeColor = pColor;
        ddlexpressao2CobrancaPeso.ForeColor = pColor;
        ddlexpressao2CobrancaMensal.ForeColor = pColor;
        ddlcondicaoCobrancaPeso.ForeColor = pColor;
        ddlexpressao3CobrancaPeso.ForeColor = pColor;
        moeFranquiaCobrancaMensal.ForeColor = pColor;
        txtFrequenciaColeta.ForeColor = pColor;
        txtMesAnoBase.ForeColor = pColor;
        txtObservacao_R.ForeColor = pColor;
        txtParticularidade.ForeColor = pColor;
        ddlPeriodicidadeCobrancaMensal.ForeColor = pColor;
        intQtFrequenciaColeta.ForeColor = pColor;
        txtRoteiro.ForeColor = pColor;
        txtTipoCx.ForeColor = pColor;
        txtUnidadeCobrancaPeso.ForeColor = pColor;
        txtUnidade2CobrancaPeso.ForeColor = pColor;
        moeValorExcedenteCobrancaMensal.ForeColor = pColor;
        moeValorUnitarioCobrancaPeso.ForeColor = pColor;
        moeValorUnitarioCobrancaMensal.ForeColor = pColor;
        moeValorContratoCobrancaMensal.ForeColor = pColor;
    }
    private void LimpaCamposContratoResiduos()
    {        
        intCxDisp.Valor = "";
        GRUPORESIDUO_R.Valor = "";
        GRUPORESIDUO_R.Texto = "";
        Session["GrupoResiduos"] = null;
        ddlexpressao4CobrancaPeso.Text = "";
        moeFranquiaCobrancaPeso.Valor = "";
        txtDataInicioReajuste.Text = datDataReajuste1.Text;
        txtDiasColeta.Text = "";
        ddlexpressao1CobrancaMensal.Text = "";
        ddlexpressao2CobrancaPeso.Text = "";
        ddlexpressao2CobrancaMensal.Text = "";
        ddlcondicaoCobrancaPeso.Text = "";
        ddlexpressao3CobrancaPeso.Text = "";
        moeFranquiaCobrancaMensal.Valor = "";
        txtFrequenciaColeta.Text = "";
        txtMesAnoBase.Text = "";
        txtObservacao_R.Text = "";
        txtParticularidade.Text = "";
        ddlPeriodicidadeCobrancaMensal.Text = "";
        intQtFrequenciaColeta.Valor = "";
        txtRoteiro.Text = "";
        txtTipoCx.Text = "";
        txtUnidadeCobrancaPeso.Text = "";
        txtUnidade2CobrancaPeso.Text = "";
        moeValorExcedenteCobrancaMensal.Valor = "";
        moeValorUnitarioCobrancaPeso.Valor = "";
        moeValorUnitarioCobrancaMensal.Valor = "";
        moeValorContratoCobrancaMensal.Valor = "";
    }
    protected void GradeResiduos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "CodigoContrato" && e.CommandArgument.ToString() != "CodigoResiduo" &&
            e.CommandArgument.ToString() != "DescricaoReduzidaResiduo")
        {
            if (Convert.ToInt32(e.CommandArgument) < oGrade.Rows.Count - 1)
            {
                oContratoResiduos.DataReajuste = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[26].Text;
                if (oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text != "")
                    oContratoResiduos.CodigoContrato = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text);
                if (oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text != "")
                    oContratoResiduos.CodigoResiduo = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text);

                CorCamposContratoResiduos(intSequencial.ForeColor);

                _dtResiduos = new DataTable();
                _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste, oContratos.CodigoCliente);
                DataRow[] _drr = _dtResiduos.Select("DataReajuste = '" + oContratoResiduos.DataReajuste + "' and " +
                                                    "CodigoResiduo = " + oContratoResiduos.CodigoResiduo);
                if (_drr.Length > 0)
                {
                    if (intCodigoCliente.Valor != "")
                        oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
                    if (_drr[0]["CaixaDisponivel"].ToString() != "")
                        oContratoResiduos.CaixaDisponivel = Convert.ToInt32(_drr[0]["CaixaDisponivel"].ToString());

                    oContratoResiduos.DescricaoReduzidaResiduo = _drr[0]["DescricaoReduzidaResiduo"].ToString();
                    oContratoResiduos.condicaoCobrancaPeso = _drr[0]["condicaoCobrancaPeso"].ToString();
                    oContratoResiduos.DiasColeta = _drr[0]["DiasColeta"].ToString();
                    oContratoResiduos.expressao1CobrancaMensal = _drr[0]["expressao1CobrancaMensal"].ToString();
                    oContratoResiduos.expressao1CobrancaPeso = _drr[0]["expressao1CobrancaPeso"].ToString();
                    oContratoResiduos.expressao2CobrancaMensal = _drr[0]["expressao2CobrancaMensal"].ToString();
                    oContratoResiduos.expressao2CobrancaPeso = _drr[0]["expressao2CobrancaPeso"].ToString();
                    oContratoResiduos.expressao3CobrancaPeso = _drr[0]["expressao3CobrancaPeso"].ToString();
                    oContratoResiduos.expressao4CobrancaPeso = _drr[0]["expressao4CobrancaPeso"].ToString();
                    if (_drr[0]["FranquiaCobrancaPeso"].ToString() != "")
                        oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(_drr[0]["FranquiaCobrancaPeso"].ToString());
                    oContratoResiduos.UnidadeCobrancaPeso = _drr[0]["UnidadeCobrancaPeso"].ToString();
                    oContratoResiduos.Franquia = _drr[0]["Franquia"].ToString();
                    oContratoResiduos.Franquia1CobrancaMensal = _drr[0]["Franquia1CobrancaMensal"].ToString();
                    oContratoResiduos.FrequenciaColeta = _drr[0]["FrequenciaColeta"].ToString();

                    oContratoResiduos.MesAnoBase = _drr[0]["MesAnoBase"].ToString();
                    oContratoResiduos.OBS = _drr[0]["OBS"].ToString();
                    oContratoResiduos.Particularidade = _drr[0]["Particularidade"].ToString();
                    oContratoResiduos.PeriodicidadeCobrancaMensal = _drr[0]["PeriodicidadeCobrancaMensal"].ToString();
                    if (_drr[0]["QuantidadeFranquia"].ToString() != "")
                        oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(_drr[0]["QuantidadeFranquia"]) / 100;
                    oContratoResiduos.Roteiro = _drr[0]["Roteiro"].ToString();
                    oContratoResiduos.TipoCaixa = _drr[0]["TipoCaixa"].ToString();
                    oContratoResiduos.Unidade = _drr[0]["Unidade"].ToString();
                    if (_drr[0]["ValorExcedenteCobrancaMensal"].ToString() != "")
                        oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(_drr[0]["ValorExcedenteCobrancaMensal"]);
                    if (_drr[0]["ValorUnitario"].ToString() != "")
                        oContratoResiduos.ValorUnitario = Convert.ToDecimal(_drr[0]["ValorUnitario"]) / 100;
                    if (_drr[0]["ValorUnitarioCobrancaMensal"].ToString() != "")
                        oContratoResiduos.ValorUnitarioCobrancaMensal = Convert.ToDecimal(_drr[0]["ValorUnitarioCobrancaMensal"]);
                    if (_drr[0]["ValorContratoCobrancaMensal"].ToString() != "")
                        oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(_drr[0]["ValorContratoCobrancaMensal"]);

                }
                // limpa a imagem de excluir nos registros seguintes
                for (int i = 1; i < GradeReajustes.Rows.Count; i++)
                {
                    GradeReajustes.Rows[i].Cells[1].Text = "";
                }

                AtribuiDadosContratoResiduosParaCamposForm(oContratoResiduos);
                lblMensagemResiduos.Text = "";
                if (txtFrequenciaColeta.ForeColor == System.Drawing.Color.Red)
                    lblMensagemResiduos.Text = "Tem certeza que deseja fazer a alteração/exclusão!";
                btnOkResiduos.Focus();
            }
        }
    }
    private void RefreshGradeResiduos()
    {
        _dtResiduos = new DataTable();
        _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste, oContratos.CodigoCliente);
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
    }
    protected void btnOkResiduos_Click(object sender, EventArgs e)
    {
        if (btnOkResiduos.Text == "Confirma") // exclusão
        {
            if (txtDiasColeta.Text.Trim() == "" && txtFrequenciaColeta.Text.Trim() == "" && intCodigoContrato_R.Valor != "" && GRUPORESIDUO_R.Valor != "" && intCodigoCliente1.Valor != "")
            {
                
                AtribuiDadosContratoResiduosParaClasse();
                lblMensagemResiduos.Text  = oContratoResiduosDados.Excluir(oContratoResiduos.CodigoContrato, oContratoResiduos.CodigoResiduo, 
                                                                           oContratoResiduos.CodigoCliente, oContratoResiduos.DataReajuste);
                if (lblMensagemResiduos.Text == "")
                    lblMensagemResiduos.Text = "Resíduo excluído com sucesso!";
                RefreshGradeResiduos();
            }
            else
            {
                lblMensagemResiduos.Text = "Existe frequência de coleta e ou dias de coleta. Não é possível excluir!";
            }
            LimpaCamposContratoResiduos();
            btnOkResiduos.Text = "Ok";
            btnOkResiduos.Focus();
        }
        else
        {
            if (intCodigoContrato_R.Valor != "" && GRUPORESIDUO_R.Valor != "" && intCodigoCliente1.Valor != "")
            {
                AtribuiDadosContratoResiduosParaClasse();
                if (oContratoResiduos.CodigoContrato > 0 && oContratoResiduos.CodigoResiduo > 0 && oContratoResiduos.DataReajuste != "" && oContratoResiduos.CodigoCliente > 0)
                {
                    if (oContratoResiduosDados.DadoExiste(oContratoResiduos.CodigoContrato, oContratoResiduos.CodigoResiduo, oContratoResiduos.DataReajuste, oContratoResiduos.CodigoCliente) == "Alterar")
                    {
                        oContratoResiduosDados.Alterar(oContratoResiduos, oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
                    }
                    else
                    {
                        oContratoResiduosDados.Inserir(oContratoResiduos);
                    }
                }
                RefreshGradeResiduos();
                LimpaCamposContratoResiduos();
                lblMensagemResiduos.Text = "Dados salvos com sucesso.";
            }
            else
                lblMensagemResiduos.Text = "Campo Obrigatório: Código Contrato, Código Resíduo e Frequência Coleta!";

            btnOkResiduos.Focus();
        }
    }

    protected void ibnExcluirResiduo_Click(object sender, ImageClickEventArgs e)
    {
        lblTituloResiduos.Text = "Exclusão de resíduo";
        btnOkResiduos.Text = "Confirma";
    }

    protected void ibnExcluirReajustes_Click(object sender, ImageClickEventArgs e)
    {
        lblTituloReajustes.Text = "Exclusão de reajuste";
        btnOkReajuste.Text = "Confirma";
    }
}