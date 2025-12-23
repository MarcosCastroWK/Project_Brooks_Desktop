using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
public partial class Contratos_Cadastro : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsClientes oClientes = new clsClientes();
    clsClienteDados oClientesDados = new clsClienteDados();
    clsContratos oContratos = new clsContratos();
    clsContratosDados oContratosDados = new clsContratosDados();
    clsContratosReajustes oReajustes = new clsContratosReajustes();
    clsContratosReajustesDados oReajustesDados = new clsContratosReajustesDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
    DataTable _dt = new DataTable();
    DataTable _dtReajustes = new DataTable();
 
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "5");

        if (oItensMenuPermissoes.Incluir == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
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
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    try
                    {
                        HabilitaDesabilitaCampos(false);
                        geral.Ordem = "Codigo desc";
                        ddlFiltro.Text = "Ativos";
                        Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, txtFiltro.Text, ddlFiltro.Text, true);
                    }
                    finally
                    {
                        Grade.DataBind();
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
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Inclusão-Contratos";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Sequencial contrato nº: " + hifCodigo.Value  + " \n";
        oLog.Log = oLog.Log + "Data Registro: " + datDataRegistro.Data + " \n";
        oLog.Log = oLog.Log + "Data Início: " + datDataInicio.Data + " \n";
        oLog.Log = oLog.Log + "Nº Contrato: " + intNumeroContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Valor Contrato: " + moeValorContrato.Valor + " \n";
        oLog.Log = oLog.Log + "Data Término: " + datDataTermino.Data + " \n";
        oLog.Log = oLog.Log + "Situação: " + ddlSituacao.Text + " \n";
        oLog.Log = oLog.Log + "Data Reajuste: " + datDataReajuste.Data + " \n";
        oLog.Log = oLog.Log + "Indice Reajuste: " + txtIndiceReajuste.Text + " \n";
        oLog.Log = oLog.Log + "Aniversario de Reajuste: " + txtAniversarioReajuste.Text + " \n";
        oLog.Log = oLog.Log + "Dia Vencimento: " + intDiaVencimento.Valor + " \n";
        oLog.Log = oLog.Log + "Observação: " + txtObservacao.Text + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (Salvar.Text == "Salvar")
        {
            if (hifNomeFantasia.Value.Equals("") || hifNome.Value.Equals("") || hifCodigo.Value.Equals("") || !geral.IsNumeric(hifCodigo.Value))
            {
                lblMensagem.Text = "É preciso Selecionar um cliente para incluir contrato!";
            }            
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oContratos = AtribuiDadosDoForm(oContratos);
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
                            if (drDoc["BROOKS"].ToString() == "S")
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
                        }
                        int iSeq = oContratosDados.PegaUltimoSequencial(oContratos.CodigoCliente);
                        Label lbl = new Label();
                        if (iSeq > 0)
                        {
                            oReajustes.Data = datDataInicio.Data;
                            oReajustes.Valor = Convert.ToDecimal(moeValorContrato.Valor);
                            oReajustes.NumeroContrato = intNumeroContrato.Valor;
                            oReajustes.Situacao = ddlSituacao.Text;
                            oReajustes.TipoNegociacao = "INÍCIO CONTRATO";
                            oReajustes.CodigoContrato = iSeq;
                            if (oReajustesDados.Inserir(oReajustes))
                            {
                                try
                                {
                                    lblMensagem.Text = "Contrato inserido com sucesso!";
                                    geral.Ordem = "Codigo desc";
                                    ddlFiltro.Text = "Codigo";
                                    txtFiltro.Text = oContratos.CodigoCliente.ToString();
                                    Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, txtFiltro.Text, ddlFiltro.Text, true);
                                    Grade.DataBind();
                                    ImageButton ibnConsultar = (ImageButton)Grade.Rows[0].FindControl("ibnConsultar");
                                    if (ibnConsultar != null)
                                        ibnConsultar.ImageUrl = "~/Images/selecionado.png";
                                    for (int i = 0; i < Grade.Columns.Count; i++)
                                    {
                                        Grade.Rows[0].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                                    }
                                    HabilitaDesabilitaCampos(false);
                                }
                                finally
                                {
                                    Response.Redirect("Contratos_Residuos.aspx?CodigoGerado=" + iSeq.ToString(), true);
                                }
                            }
                            else
                                lblMensagem.Text = "Erro ao inserir início contrato!";
                        }
                        else
                            lblMensagem.Text = "Erro ao inserir início contrato!";
                    }
                    else
                        lblMensagem.Text = "Erro: " + sRet;
                }
                else
                    lblMensagem.Text = "Data de início inválida!";
            }
        }
    }

    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome" && e.CommandArgument.ToString() != "NomeFantasia" &&
            e.CommandArgument.ToString() != "CNPJ_CPF" && e.CommandArgument.ToString() != "DataCadastro" && e.CommandArgument.ToString() != "Existe")
        {
            if (Convert.ToInt32(e.CommandArgument) < 6)
            {
                lblMensagem.Text = "";
                LimpaCampos();
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                oClientes = oClientesDados.PegaDados(oClientes, Convert.ToInt32(hifCodigo.Value));
                hifCNPJ_CPF.Value = oClientes.CNPJ_CPF;
                hifNome.Value = oClientes.Nome;
                hifNomeFantasia.Value = oClientes.NomeFantasia;
                if (oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text == "Não")
                { 
                    HabilitaDesabilitaCampos(true);                    
                    datDataInicio.Data = DateTime.Now.ToString("dd/MM/yyyy");
                    datDataRegistro.Data = DateTime.Now.ToString("dd/MM/yyyy");
                    Salvar.Enabled = true;
                    lblMensagem.Text = "";
                }
                else
                {
                    lblMensagem.Text = "Contrato Existente!";
                    LimpaCampos();
                    HabilitaDesabilitaCampos(false);
                    Salvar.Enabled = false;
                }
                TiraSelecionado();
                ImageButton ibnConsultar = (ImageButton) oGrade.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("ibnConsultar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionado.png";

                for (int i = 0; i < oGrade.Columns.Count; i++)
                {
                    oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                }
                datDataInicio.Focus();
            }
        }
    }
    private void TiraSelecionado()
    {
        bool bInterCor = false;
        for (int i = 0; i < Grade.Rows.Count; i++)
        {
            ImageButton ibnConsultar = (ImageButton)Grade.Rows[i].FindControl("ibnConsultar");
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

        //Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, txtFiltro.Text, ddlFiltro.Text, true);
        //Grade.DataBind();
    }
    protected void AtribuiDadosDaClasse(clsContratos pContratos)
    {
        hifNome.Value = pContratos.Nome;
        hifCNPJ_CPF.Value = pContratos.CNPJ_CPF;
        hifNomeFantasia.Value = pContratos.NomeFantasia;       
        txtObservacao.Text = pContratos.Observacao;
        if (pContratos.DataRegistro == "01/01/0001" || pContratos.DataRegistro == "01/01/0100" || pContratos.DataRegistro == null  || pContratos.DataRegistro == "")
            datDataRegistro.Data = "";
        else
            datDataRegistro.Data = Convert.ToDateTime(pContratos.DataRegistro).ToString("dd/MM/yyyy");;
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
        moeValorContrato.Valor = pContratos.ValorContrato.ToString("N2");
        hifValorContrato.Value = pContratos.ValorContrato.ToString();
        intDiaVencimento.Valor = pContratos.DiaVencimento.ToString();
        ddlSituacao.SelectedIndex = 0;
        if (oContratos.Codigo > 0 && oContratos.DataInicio != "")
        {
            oReajustes = oReajustesDados.PegaDados(oReajustes, oContratos.Codigo, oContratos.DataInicio, 0);
            ddlSituacao.Text = oReajustes.Situacao;
        }
    }

    protected void LimpaCampos()
    {
        datDataReajuste.Data = "";
        hifNome.Value = "";
        hifNomeFantasia.Value = "";
        hifCNPJ_CPF.Value = "";
        txtObservacao.Text = "";
        datDataRegistro.Data = "";
        intNumeroContrato.Valor = "";
        datDataInicio.Data = "";
        ddlSituacao.SelectedIndex = 0;
        datDataTermino.Data = "";
        txtAniversarioReajuste.Text = "";
        datDataReajuste.Data = "";
        txtIndiceReajuste.Text = "";
        moeValorContrato.Valor = "";
        hifValorContrato.Value = "";
        intDiaVencimento.Valor = "";
        Grade.Visible = true;
    }

    protected void HabilitaDesabilitaCampos(bool pHabilita)
    {
        datDataReajuste.Enabled = pHabilita;
        txtObservacao.Enabled = pHabilita;
        datDataRegistro.Enabled = false;
        intNumeroContrato.Enabled = pHabilita;
        datDataInicio.Enabled = pHabilita;
        datDataTermino.Enabled = pHabilita;
        ddlSituacao.Enabled = pHabilita;
        if (!ddlSituacao.Enabled)
            ddlSituacao.BackColor = System.Drawing.Color.Lavender;
        else
            ddlSituacao.BackColor = System.Drawing.Color.White;
        txtAniversarioReajuste.Enabled = pHabilita;
        datDataReajuste.Enabled = pHabilita;
        txtIndiceReajuste.Enabled = pHabilita;
        moeValorContrato.Enabled = pHabilita;
        intDiaVencimento.Enabled = pHabilita;
        Salvar.Enabled = pHabilita;
    }

    protected clsContratos AtribuiDadosDoForm(clsContratos pContratos)
    {
        if (geral.IsNumeric(hifCodigo.Value))
        {
            pContratos.CodigoCliente = Convert.ToInt32(hifCodigo.Value);
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
        ViewState["Nome"] = hifNomeFantasia.Value;
        ViewState["Descricao"] = txtObservacao.Text;
    }
    protected void ViewStateSetForm()
    {
        datDataReajuste.Data = ViewState["DataCadastro"].ToString();
        hifNomeFantasia.Value = ViewState["Nome"].ToString();
        txtObservacao.Text = ViewState["Descricao"].ToString();        
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {  
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[4].Text = "";
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("-", "─");
            if (e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;")
                e.Row.Cells[6].Text = "Não";
            else
                e.Row.Cells[6].Text = "Sim";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, txtFiltro.Text, ddlFiltro.Text, true);
        Grade.DataBind();
        LimpaCampos();
        lblMensagem.Text = "";
        HabilitaDesabilitaCampos(false);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string _filtro = txtFiltro.Text;
        geral.Ordem = ddlFiltro.Text;
        if (ddlFiltro.Text == "Codigo")
        {
            if (!geral.IsNumeric(txtFiltro.Text))
                _filtro = "-1";
        }
        else if (ddlFiltro.Text == "Ativos")
        {
            txtFiltro.Text = "";
            _filtro = "";
            geral.Ordem = "Codigo asc";
        }                    
        Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, _filtro, ddlFiltro.Text, true);
        Grade.DataBind();
        LimpaCampos();
        HabilitaDesabilitaCampos(false);
    }

    protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grade.DataSource = oClientesDados.PreencheDataTable(geral.Ordem, txtFiltro.Text, ddlFiltro.Text, true);
        }
        finally
        {
            Grade.PageIndex = e.NewPageIndex;
            Grade.DataBind();
            lblMensagem.Text = "";
            LimpaCampos();
            HabilitaDesabilitaCampos(false);
        }
    }
    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFiltro.Text = "";
        btnOk_Click(sender, e);
    }
}