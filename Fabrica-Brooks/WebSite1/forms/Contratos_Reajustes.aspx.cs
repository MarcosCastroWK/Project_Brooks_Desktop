using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class Contratos_Reajustes : System.Web.UI.Page
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
    DataTable _dt = new DataTable();
    DataTable _dtResiduos = new DataTable();
    DataTable _dtPesquisa = new DataTable();
    int  LinhaReajustes = 0;
    bool bExcluirLinha = false;
    bool bMostraResiduos = true;
    bool bSalvarReajuste = false;
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "7");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
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
                    if (oUsuario.Aplicativo == true)
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    try
                    {
                        _dt = oContratosDados.PegaDados(false);
                        Grade.DataSource = _dt;
                        Grade.DataBind();
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
                    finally
                    {
                        TotalContratos();
                        CarregaReajusteSituacao(p_ddlSituacao);
                        CarregaReajusteTipoNegociacao(p_ddlTiposNegociacao);
                        CarregaTiposDeCaixas(p_ddlTiposDeCaixas);
                        CarregaFrequenciaColeta(p_ddlFrequenciaColeta);
                    }
                }
                catch (Exception ex)
                {
                    //lblMensagem.Text = ex.Message;
                }
            }
            string r = Request.QueryString["view"];
        }
        GradeReajustes.Focus();
    }

    protected void ibnSelecionar_Click(object sender, ImageClickEventArgs e)
    {    
        lblTitulo.Text = "&nbsp;Reajustar/Repactuar";
    }
    private void TiraSelecionado()
    {
        bool bInterCor = false;
        for (int i = 0; i < Grade.Rows.Count; i++)
        {
            ImageButton ibnConsultar = (ImageButton)Grade.Rows[i].FindControl("ibnSelecionar");
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
    private void TiraSelecionadoReajustes()
    {
        bool bInterCor = false;
        for (int i = 0; i < GradeReajustes.Rows.Count; i++)
        {
            if (bInterCor)
                bInterCor = false;
            else
                bInterCor = true;
            for (int j = 0; j < GradeReajustes.Columns.Count; j++)
            {
                if (bInterCor)
                    GradeReajustes.Rows[i].Cells[j].BackColor = System.Drawing.Color.White;
                else
                    GradeReajustes.Rows[i].Cells[j].BackColor = System.Drawing.Color.AliceBlue;
            }
            GridViewRow gvr = GradeReajustes.Rows[i];
            forms_DATA _datDataReajuste = (forms_DATA)gvr.Cells[3].FindControl("datDataReajuste");
            DropDownList _ddlTipoNegociacao = (DropDownList)gvr.Cells[4].FindControl("ddlTipoNegociacao");
            forms_MOEDA _moePercentualContrato = (forms_MOEDA)gvr.Cells[5].FindControl("moePercentualContrato");
            forms_MOEDA _moePercentualUnitarios = (forms_MOEDA)gvr.Cells[6].FindControl("moePercentualUnitarios");
            forms_MOEDA _moeValorContrato = (forms_MOEDA)gvr.Cells[7].FindControl("moeValorContrato");
            forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)gvr.Cells[8].FindControl("intNumeroContrato");
            DropDownList _ddlSituacao = (DropDownList)gvr.Cells[9].FindControl("ddlSituacao");           
            forms_DATA _datProximoReajuste = (forms_DATA)gvr.Cells[11].FindControl("ProximoReajuste");
            TextBox _txtObservacao = (TextBox)gvr.Cells[12].FindControl("txtObservacao");
            if (bInterCor)
            {
                _datDataReajuste.BackColor = System.Drawing.Color.White;
                _moePercentualContrato.BackColor = System.Drawing.Color.White;
                _moePercentualUnitarios.BackColor = System.Drawing.Color.White;
                _moeValorContrato.BackColor = System.Drawing.Color.White;
                _intNumeroContrato.BackColor = System.Drawing.Color.White;
                _ddlSituacao.BackColor = System.Drawing.Color.White;
                _ddlTipoNegociacao.BackColor = System.Drawing.Color.White;
                _datProximoReajuste.BackColor = System.Drawing.Color.White;
                _txtObservacao.BackColor = System.Drawing.Color.White;
            }
            else
            {
                _datDataReajuste.BackColor = System.Drawing.Color.AliceBlue;
                _moePercentualContrato.BackColor = System.Drawing.Color.AliceBlue;
                _moePercentualUnitarios.BackColor = System.Drawing.Color.AliceBlue;
                _moeValorContrato.BackColor = System.Drawing.Color.AliceBlue;
                _intNumeroContrato.BackColor = System.Drawing.Color.AliceBlue;
                _ddlSituacao.BackColor = System.Drawing.Color.AliceBlue;
                _ddlTipoNegociacao.BackColor = System.Drawing.Color.AliceBlue;
                _datProximoReajuste.BackColor = System.Drawing.Color.AliceBlue;
                _txtObservacao.BackColor = System.Drawing.Color.AliceBlue;
            }
            if (i > 0)
            {
                _moePercentualContrato.Enabled = false;
                _moePercentualUnitarios.Enabled = false;
            }

        }
    }
    private void MarcaReajusteGrade(int pLinha)
    {
        if (GradeReajustes.Rows.Count > 1)
        {
            for (int i = 3; i < GradeReajustes.Columns.Count; i++)
            {
                GradeReajustes.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
            }
            GridViewRow gvr = GradeReajustes.Rows[pLinha];
            forms_DATA _datDataReajuste = (forms_DATA)gvr.Cells[3].FindControl("datDataReajuste");
            _datDataReajuste.BackColor = System.Drawing.Color.CadetBlue;
            DropDownList _ddlTipoNegociacao = (DropDownList)gvr.Cells[4].FindControl("ddlTipoNegociacao");
            _ddlTipoNegociacao.BackColor = System.Drawing.Color.CadetBlue;
            forms_MOEDA _moePercentualContrato = (forms_MOEDA)gvr.Cells[5].FindControl("moePercentualContrato");
            _moePercentualContrato.BackColor = System.Drawing.Color.CadetBlue;
            forms_MOEDA _moePercentualUnitarios = (forms_MOEDA)gvr.Cells[6].FindControl("moePercentualUnitarios");
            _moePercentualUnitarios.BackColor = System.Drawing.Color.CadetBlue;
            forms_MOEDA _moeValorContrato = (forms_MOEDA)gvr.Cells[7].FindControl("moeValorContrato");
            _moeValorContrato.BackColor = System.Drawing.Color.CadetBlue;
            forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)gvr.Cells[8].FindControl("intNumeroContrato");
            _intNumeroContrato.BackColor = System.Drawing.Color.CadetBlue;
            DropDownList _ddlSituacao = (DropDownList)gvr.Cells[9].FindControl("ddlSituacao");
            _ddlSituacao.BackColor = System.Drawing.Color.CadetBlue;            
            forms_DATA _datProximoReajuste = (forms_DATA)gvr.Cells[11].FindControl("ProximoReajuste");
            _datProximoReajuste.BackColor = System.Drawing.Color.CadetBlue;
            TextBox _txtObservacao = (TextBox)gvr.Cells[12].FindControl("txtObservacao");
            _txtObservacao.BackColor = System.Drawing.Color.CadetBlue;
            lblTituloResiduos.Text = "Resíduos Contratados de " + _datDataReajuste.Data;
        }
    }

    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome" && e.CommandArgument.ToString() != "NomeFantasia" &&
            e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "ValorContrato" && e.CommandArgument.ToString() != "DiaVencimento" &&
            e.CommandArgument.ToString() != "DataReajuste" && e.CommandArgument.ToString() != "IndiceReajuste" && e.CommandArgument.ToString() != "DataTermino" &&
            e.CommandArgument.ToString() != "DataRecisao")
        {
            lblMensagemResiduos.Text = "";
            if (e.CommandArgument.ToString() != "")
            {
                if (Grade.Rows.Count - 1 >= Convert.ToInt32(e.CommandArgument))
                {
                    bMostraResiduos = true;
                    hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                    hifCodigoCliente.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                    // pro casa de haver reajuste
                    hifDataReajuste.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text.Trim();
                    hifDataReajusteSelecionado.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text.Trim();
                    hifValorContrato.Value = "0,00";
                        if (oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text.Trim() != "")
                    hifValorContrato.Value = Convert.ToDecimal(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text.Trim()).ToString("N2").Replace(".", "");
                    TiraSelecionado();
                    MarcaContratoGrade(Convert.ToInt32(e.CommandArgument));
                    MostraGradeReajustes();
                    bMostraResiduos = false;
                    if (GradeReajustes.Rows.Count > 0)
                    {
                        try
                        {
                            hifSequencialReajuste.Value = GradeReajustes.Rows[1].Cells[11].Text;
                            oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);
                            oContratoResiduos.DataReajuste = ((forms_DATA)GradeReajustes.Rows[1].Cells[3].FindControl("datDataReajuste")).Data;
                            hifDataReajuste.Value = oContratoResiduos.DataReajuste;
                            MarcaReajusteGrade(1);

                            if (oContratoResiduos.CodigoContrato > 0 && oContratoResiduos.DataReajuste != "")
                                RefreshGradeResiduos();
                        }
                        catch
                        {
                            lblMensagemResiduos.Text = "Não foi possível mostrar dados!";
                            ClearGradeReajustes();
                            ClearGradeResiduos();
                        }
                    }
                }
            }
        }
    }
    private void MostraGradeReajustes()
    {
        oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(hifCodigoCliente.Value));
        if (hifCodigo.Value != "")
        {
            DataTable _dtReajustes = new DataTable();
            Int32 iInt = 0;
            _dtReajustes.Columns.Add("CodigoContrato", iInt.GetType());
            _dtReajustes.Columns.Add("Data", Type.GetType("System.DateTime"));
            _dtReajustes.Columns.Add("TipoNegociacao");
            _dtReajustes.Columns.Add("Valor", Type.GetType("System.Decimal"));
            _dtReajustes.Columns.Add("NumeroContrato");
            _dtReajustes.Columns.Add("Situacao");
            _dtReajustes.Columns.Add("Sequencial", iInt.GetType());
            _dtReajustes.Columns.Add("ProximoReajuste", Type.GetType("System.DateTime"));
            _dtReajustes.Columns.Add("Observacao");
            _dtReajustes.NewRow();
            _dtReajustes.Rows.Add();
            _dtReajustes.Merge(oReajustesDados.PreencheDataTable("Data desc ", Convert.ToInt32(hifCodigo.Value), 100));
            GradeReajustes.DataSource = _dtReajustes;
            GradeReajustes.DataBind();
        }   
    }
    protected void LimpaCampos()
    {
        Grade.Visible = true;
        GradeReajustes.DataSource = "";
        GradeReajustes.DataBind();
        GradeResiduos.DataSource = "";
        GradeResiduos.DataBind();
    }

    private void CancelarOperacao()
    {
        lblTitulo.Text = "&nbsp;Reajustar/Repactuar";
        LimpaCampos();
        hifCodigo.Value = "";
        GradeReajustes.DataSource = "";
        GradeReajustes.DataBind();
        GradeResiduos.DataSource = "";
        GradeResiduos.DataBind();
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
            for (int i = 0; i <= 10; i++)
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
        ClearGradeResiduos();
        ClearGradeReajustes();
    }
    private void MarcaContratoGrade(int pLinha)
    {
        if (Grade.Rows.Count > 0)
        {
            for (int i = 0; i < Grade.Columns.Count; i++)
            {
                Grade.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
            }
            ImageButton ibnConsultar = (ImageButton)Grade.Rows[pLinha].FindControl("ibnSelecionar");
            if (ibnConsultar != null)
                ibnConsultar.ImageUrl = "~/Images/selecionado.png";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        FazOk(true);
    }

    private void FazOk(bool bRefreshGradeResiduos)
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
            lblMensagemResiduos.Text = "";
            TiraSelecionado();
            MarcaContratoGrade(0);
            hifCodigo.Value = _dt.Rows[0]["Codigo"].ToString();
            hifCodigoCliente.Value = _dt.Rows[0]["CodigoCliente"].ToString();
            hifValorContrato.Value = 0.ToString("N2");
            if (_dt.Rows[0]["ValorContrato"].ToString() != "")
                hifValorContrato.Value = Convert.ToDecimal(_dt.Rows[0]["ValorContrato"]).ToString("N2").Replace(".", "");
            MostraGradeReajustes();
            MarcaReajusteGrade(1);
            oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);
           
            forms_DATA _datDataReajuste;
            try
            {
                _datDataReajuste = (forms_DATA)GradeReajustes.Rows[1].Cells[3].FindControl("datDataReajuste");

                oContratoResiduos.DataReajuste = _datDataReajuste.Data;
                hifDataReajuste.Value = _datDataReajuste.Data;

                if (oContratoResiduos.CodigoContrato > 0 && oContratoResiduos.DataReajuste != "" && bRefreshGradeResiduos)
                    RefreshGradeResiduos();
            }
            catch
            {
                lblMensagemResiduos.Text = "Não foi possível mostrar dados!";
                ClearGradeReajustes();
                ClearGradeResiduos();
            }
        }
        else
        {
            //ClearGradeReajustes();
            //ClearGradeResiduos();
        }
    }
    private void GradeResiduosContratados(GridView pGrade, int pLinha)
    {
        if (bMostraResiduos)
        { 
            LinhaReajustes = pLinha;
            lblMensagemResiduos.Text = "";
            TirarIconeExcluirDaGradeReajustes();
            if (pLinha == 0)
                pLinha++;
            if (pGrade.Rows.Count > pLinha)
            { 
                oContratoResiduos.DataReajuste = ((forms_DATA)pGrade.Rows[pLinha].Cells[3].FindControl("datDataReajuste")).Data;
                hifSequencialReajuste.Value = pGrade.Rows[pLinha].Cells[10].Text;
                oReajustesDados.PegaDados(oReajustes, Convert.ToInt32(hifCodigo.Value), oContratoResiduos.DataReajuste, Convert.ToInt32(pGrade.Rows[pLinha].Cells[10].Text));

                hifValorContrato.Value = oReajustes.Valor.ToString();

                oContratos = oContratosDados.PegaDados(oContratos, 0, Convert.ToInt32(hifCodigoCliente.Value));
                _dtResiduos = new DataTable();
                _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratos.Codigo, oContratoResiduos.DataReajuste, Convert.ToInt32(hifCodigoCliente.Value));
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

                lblTituloResiduos.Text = "Resíduos Contratados de " + oContratoResiduos.DataReajuste;
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
    }
    protected void GradeReajustes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "CodigoContrato" && e.CommandArgument.ToString() != "Data" &&
            e.CommandArgument.ToString() != "NumeroContrato" && e.CommandArgument.ToString() != "Situacao" &&
            e.CommandArgument.ToString() != "TipoNegociacao" && e.CommandArgument.ToString() != "Valor" &&
            e.CommandArgument.ToString() != "Observacao" && e.CommandArgument.ToString() != "Sequencial")
        {
            lblMensagemResiduos.Text = "";
            if (e.CommandArgument.ToString() != "")
            {
                TiraSelecionadoReajustes();
                hifDataReajuste.Value = ((forms_DATA)oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].FindControl("datDataReajuste")).Data;
                if (bSalvarReajuste)
                {
                    SalvarReajuste(Convert.ToInt32(e.CommandArgument));
                    for (int _i = 2; _i < GradeReajustes.Rows.Count; _i++)
                    {
                        GradeReajustes.Rows[_i].Cells[2].Text = "";
                    }
                }
                else
                {
                    MarcaReajusteGrade(Convert.ToInt32(e.CommandArgument));
                    GradeResiduosContratados(oGrade, Convert.ToInt32(e.CommandArgument));
                }                
            }
        }
    }
    private void SalvarLog(string pOperacao, string pLog)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Reajustar/Repactuar-Contratos";
        oLog.Operacao = pOperacao;
        oLog.Log = pLog;
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
    private void SalvarReajuste(int pLinha)
    {
        GridViewRow gvr = GradeReajustes.Rows[pLinha];
        forms_DATA _datDataReajuste = (forms_DATA)gvr.Cells[3].FindControl("datDataReajuste");
        DropDownList _ddlTipoNegociacao = (DropDownList)gvr.Cells[4].FindControl("ddlTipoNegociacao");
        forms_MOEDA _moePercentualContrato = (forms_MOEDA)gvr.Cells[5].FindControl("moePercentualContrato");
        forms_MOEDA _moePercentualUnitarios = (forms_MOEDA)gvr.Cells[6].FindControl("moePercentualUnitarios");
        forms_MOEDA _moeValorContrato = (forms_MOEDA)gvr.Cells[7].FindControl("moeValorContrato");
        forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)gvr.Cells[8].FindControl("intNumeroContrato");
        DropDownList _ddlSituacao = (DropDownList)gvr.Cells[9].FindControl("ddlSituacao");
        forms_DATA _datProximoReajuste = (forms_DATA)gvr.Cells[11].FindControl("ProximoReajuste");
        TextBox _txtObservacao = (TextBox)gvr.Cells[12].FindControl("txtObservacao");
        oReajustes = new clsContratosReajustes();
        if (hifCodigo.Value != "")
            oReajustes.CodigoContrato = Convert.ToInt32(hifCodigo.Value);
        oReajustes.Data = _datDataReajuste.Data;
        oReajustes.NumeroContrato = _intNumeroContrato.Valor;
        if (gvr.Cells[10].Text != "" && gvr.Cells[10].Text != "&nbsp;")
            oReajustes.Sequencial = Convert.ToInt32(gvr.Cells[10].Text);
        oReajustes.Situacao = _ddlSituacao.Text;
        oReajustes.TipoNegociacao = _ddlTipoNegociacao.Text;
        oReajustes.Observacao = _txtObservacao.Text;
        if (_moeValorContrato.Valor != "")
            oReajustes.Valor = Convert.ToDecimal(_moeValorContrato.Valor);
        string sLog = "Sequencial reajuste: " + oReajustes.Sequencial.ToString() + " \n";
        sLog = sLog + "Data reajuste: " + _datDataReajuste.Data + " \n";
        sLog = sLog + "TipoNegociacao: " + _ddlTipoNegociacao.Text + " \n";
        sLog = sLog + "Percentual: " + _moePercentualContrato.Valor + " \n";
        sLog = sLog + "Percentual unitário: " + _moePercentualUnitarios .Valor + "\n";
        sLog = sLog + "ValorContrato: " + _moeValorContrato.Valor +  "\n";
        sLog = sLog + "Nº Contrato: " + _intNumeroContrato.Valor + "\n";
        sLog = sLog + "Situação: " + _ddlSituacao.Text + " \n";
        sLog = sLog + "Próximo reajuste: "+ _datProximoReajuste.Data + " \n";
        sLog = sLog + "Observação: " + _txtObservacao.Text + " \n";
        if (pLinha == 0)
        {
            //Incluir reajuste / repactuacao, no caso de reajuste calcular percentual ao gerar os resíduos contratados anteriormente
            SalvarLog("Inclusão reajuste", sLog);
            Salvar_Residuos_ReajusteNovo(oReajustes);
            MostraGradeReajustes();
        }
        else
        {
            // alterar dados atuais
            SalvarLog("Alteração reajuste", sLog);
            oReajustesDados.Alterar(oReajustes, oReajustes.CodigoContrato, 0, oReajustes.Sequencial);
            if (oReajustes.CodigoContrato > 0)
            {
                oContratosDados.AlterarValorContrato(Convert.ToDecimal(_moeValorContrato.Valor), oReajustes.CodigoContrato);
                mudaValorContratoNaTela(_moeValorContrato.Valor);
            }
        }
    }
    private void mudaValorContratoNaTela(string pValorContrato)
    {
        if (Grade.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in Grade.Rows)
            {
                ImageButton ibnConsultar = (ImageButton)gvr.FindControl("ibnSelecionar");
                if (ibnConsultar != null)
                {
                    if (ibnConsultar.ImageUrl == "~/Images/selecionado.png")
                    {
                        gvr.Cells[6].Text = pValorContrato;
                    }
                }
            }
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
            GradeReajustes.DataSource = oReajustesDados.PreencheDataTable(geral.Ordem, Convert.ToInt32(hifCodigo.Value), 100);
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
                _filtro = "DataRecisao";
                txtFiltro.Text = ">0100-01-01";
            }
            else if (ddlFiltro.Text == "Não Cancelados")
            {
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
            ClearGradeReajustes();
            ClearGradeResiduos();
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
        bMostraResiduos = true;
        bSalvarReajuste = false;
    }
    protected void btnCancelaAlteracaoReajuste_Click(object sender, EventArgs e)
    {
        CancelarOperacao();
    }

    protected void GradeReajustes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ImageButton _ibnSalvarReajuste = new ImageButton();
            _ibnSalvarReajuste.Enabled = true;
            if (oItensMenuPermissoes.Alterar == 0)
            {
                _ibnSalvarReajuste = (ImageButton)e.Row.Cells[2].FindControl("ibnSalvarReajuste");
                _ibnSalvarReajuste.Enabled = false;
            }
            ImageButton _ibnExcluir = new ImageButton();
            _ibnExcluir.Enabled = true;
            if (oItensMenuPermissoes.Excluir == 0)
            {
                _ibnExcluir = (ImageButton)e.Row.Cells[2].FindControl("ibnExcluirReajustes");
                _ibnExcluir.Enabled = false;
            }
            DropDownList _ddlTipoNegociacao = (DropDownList)e.Row.Cells[4].FindControl("ddlTipoNegociacao");
            forms_MOEDA _moePercentualContrato = (forms_MOEDA)e.Row.Cells[5].FindControl("moePercentualContrato");
            forms_MOEDA _moePercentualUnitarios = (forms_MOEDA)e.Row.Cells[6].FindControl("moePercentualUnitarios");
            forms_INTEIRO7 _intNumeroContrato = (forms_INTEIRO7)e.Row.Cells[8].FindControl("intNumeroContrato");
            DropDownList _ddlSituacao = (DropDownList)e.Row.Cells[9].FindControl("ddlSituacao");                      
            forms_DATA _datDataReajuste = new forms_DATA();
            forms_MOEDA _moeValorContrato = new forms_MOEDA();
            forms_DATA _datProximoReajuste = new forms_DATA();
            TextBox _txtObservacao = new TextBox();
            if (e.Row.RowIndex == 0)
            {
                _ibnSalvarReajuste = (ImageButton)e.Row.Cells[1].FindControl("ibnSalvarReajuste");
                if (_ibnSalvarReajuste != null)
                {
                    _ibnSalvarReajuste.ImageUrl = "~/Images/salvar.png";
                    _ibnSalvarReajuste.Enabled = true;
                    _ibnSalvarReajuste.ToolTip = "Salvar novo reajuste/repactuação";
                }
                _ddlSituacao.ForeColor = System.Drawing.Color.Blue;
                _ddlTipoNegociacao.ForeColor = System.Drawing.Color.Blue;
            }
            if (GradeReajustes.Rows.Count > 0)
            {
                _datDataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[3].FindControl("datDataReajuste");
                _moeValorContrato = (forms_MOEDA)GradeReajustes.Rows[0].Cells[7].FindControl("moeValorContrato");
                _datProximoReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[11].FindControl("ProximoReajuste");
                _txtObservacao = (TextBox)GradeReajustes.Rows[0].Cells[12].FindControl("txtObservacao");
                if (e.Row.RowIndex == 1)
                {
                    _datDataReajuste.Data = hifDataReajuste.Value;
                    _moeValorContrato.Valor = hifValorContrato.Value;
                    _datDataReajuste.ForeColor = System.Drawing.Color.Blue;
                    _moeValorContrato.ForeColor = System.Drawing.Color.Blue;
                    _datProximoReajuste.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {

                }
            }
            for (int iii = 0; iii < p_ddlSituacao.Items.Count; iii++)
                _ddlSituacao.Items.Add(p_ddlSituacao.Items[iii].Text);            
            HiddenField _hifSituacao = (HiddenField)e.Row.Cells[9].FindControl("hifSituacao");
            _ddlSituacao.Text = _hifSituacao.Value;
            if (e.Row.RowIndex > 0)
            {
                for (int iii = 0; iii < p_ddlTiposNegociacao.Items.Count; iii++)
                    _ddlTipoNegociacao.Items.Add(p_ddlTiposNegociacao.Items[iii].Text);
            }
            else if (e.Row.RowIndex == 0)
            {
                _ddlTipoNegociacao.Items.Clear();
                _ddlTipoNegociacao.Items.Add("");
                _ddlTipoNegociacao.Items.Add("REAJUSTE");
                _ddlTipoNegociacao.Items.Add("REPACTUAÇÃO");
            }
            HiddenField _hifTipoNegociacao = (HiddenField)e.Row.Cells[4].FindControl("hifTipoNegociacao");
            _ddlTipoNegociacao.Text = _hifTipoNegociacao.Value;
            if (e.Row.RowIndex == 0)
            {
                _ddlTipoNegociacao.Text = "REPACTUAÇÃO";
                e.Row.Cells[0].Text = "";
                e.Row.Cells[2].Text = "";
            }
            else if (e.Row.RowIndex > 1)
            {
                _datDataReajuste = (forms_DATA)e.Row.Cells[3].FindControl("datDataReajuste");
                _moeValorContrato = (forms_MOEDA)e.Row.Cells[7].FindControl("moeValorContrato");
                _datProximoReajuste = (forms_DATA)e.Row.Cells[11].FindControl("ProximoReajuste");
                _txtObservacao = (TextBox)GradeReajustes.Rows[0].Cells[12].FindControl("txtObservacao");
                e.Row.Cells[2].Text = "";
                _datDataReajuste.Enabled = false;
                _datProximoReajuste.Enabled = false;
                _moePercentualContrato.Enabled = false;
                _moePercentualUnitarios.Enabled = false;
                _datDataReajuste.ForeColor = System.Drawing.Color.Red;
                _moeValorContrato.ForeColor = System.Drawing.Color.Red;
                _intNumeroContrato.ForeColor = System.Drawing.Color.Red;
                _ddlSituacao.ForeColor = System.Drawing.Color.Red;
                _ddlTipoNegociacao.ForeColor = System.Drawing.Color.Red;
                for (int i = 0; i < GradeReajustes.Columns.Count; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.Row.RowIndex == 1)
            {
                _datDataReajuste = (forms_DATA)e.Row.Cells[3].FindControl("datDataReajuste");
                _datProximoReajuste = (forms_DATA)e.Row.Cells[11].FindControl("ProximoReajuste");
                _datDataReajuste.Enabled = false;
                _datProximoReajuste.Enabled = false;
                _moePercentualContrato.Enabled = false;
                _moePercentualUnitarios.Enabled = false;
                _datProximoReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[11].FindControl("ProximoReajuste");
                _datDataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[3].FindControl("datDataReajuste");
                _datProximoReajuste.Data = _datDataReajuste.Data;
                _datDataReajuste.Data = DateTime.Now.ToString("dd/MM/yyyy");
                _txtObservacao = (TextBox)GradeReajustes.Rows[0].Cells[12].FindControl("txtObservacao");
            }
        }
    }
    protected void Salvar_Residuos_ReajusteNovo(clsContratosReajustes pReajuste)
    {
        if (geral.IsNumeric(hifCodigo.Value) && hifCodigo.Value != "" && hifValorContrato.Value != "")
        {
            Label lbl = new Label();
            int LinhaContrato = 0;
            // passar dados dos residuos contratados no último reajuste, se existir
            if (oReajustesDados.Inserir(pReajuste))
            {
                for (int i = 0; i < Grade.Rows.Count; i++)
                {
                    ImageButton ibn = new ImageButton();
                    ibn = (ImageButton)Grade.Rows[i].Cells[0].FindControl("ibnSelecionar");
                    if (ibn.ImageUrl.IndexOf("selecionado") > -1)
                    {
                        LinhaContrato = i;
                        break;
                    }
                }

                forms_DATA _dataProximoReajuste = new forms_DATA();
                if (pReajuste.TipoNegociacao.IndexOf("REAJUSTE") > -1)
                {
                    _dataProximoReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[11].FindControl("ProximoReajuste");
                    Grade.Rows[LinhaContrato].Cells[8].Text = _dataProximoReajuste.Data;
                    oContratosDados.AlterarDataProximoReajuste(_dataProximoReajuste.Data, pReajuste.CodigoContrato, Convert.ToInt32(hifCodigoCliente.Value));
                }
                else
                {
                    _dataProximoReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[11].FindControl("ProximoReajuste");
                    Grade.Rows[LinhaContrato].Cells[8].Text = _dataProximoReajuste.Data;
                }

                // pega resíduos do último reajuste ou contrato
                if (GradeReajustes.Rows.Count > 0)
                {
                    GradeResiduos.DataSource = "";
                    GradeResiduos.DataBind();
                    forms_DATA _dataReajusteAnterior = new forms_DATA();
                    forms_DATA _dataReajuste = new forms_DATA();
                    forms_MOEDA _moeNovoValorContrato = new forms_MOEDA();
                    forms_MOEDA _moePercentualContrato = (forms_MOEDA) GradeReajustes.Rows[0].Cells[5].FindControl("moePercentualContrato");
                    forms_MOEDA _moePercentualUnitarios = (forms_MOEDA)GradeReajustes.Rows[0].Cells[6].FindControl("moePercentualUnitarios");
                    if (GradeReajustes.Rows.Count > 1)
                    {
                        _dataReajusteAnterior = (forms_DATA)GradeReajustes.Rows[1].Cells[3].FindControl("datDataReajuste");
                        _dataReajuste = (forms_DATA)GradeReajustes.Rows[0].Cells[3].FindControl("datDataReajuste");
                        _moeNovoValorContrato = (forms_MOEDA)GradeReajustes.Rows[0].Cells[7].FindControl("moeValorContrato");
                        if (_moeNovoValorContrato.Valor != "")
                            hifValorContrato.Value = _moeNovoValorContrato.Valor;
                        hifDataReajuste.Value = _dataReajuste.Data;
                    }
                    if (_dataReajuste.Data != "")
                    { 
                        oContratosDados.AlterarValorContrato(Convert.ToDecimal(hifValorContrato.Value), oReajustes.CodigoContrato, Convert.ToInt32(hifCodigoCliente.Value));
                        _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("CodigoResiduo", oReajustes.CodigoContrato, _dataReajusteAnterior.Data, Convert.ToInt32(hifCodigoCliente.Value));
                        foreach (DataRow _dr in _dtResiduos.Rows)
                        {
                            oContratoResiduos = new clsContratoResiduos();

                            oContratoResiduos.DataReajuste = _dataReajuste.Data;
                            oContratoResiduos.CodigoContrato = oReajustes.CodigoContrato;
                            oContratoResiduos.CodigoResiduo = Convert.ToInt32(_dr["CodigoResiduo"].ToString());
                            oContratoResiduos.CodigoCliente = Convert.ToInt32(hifCodigoCliente.Value);

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
                            oContratoResiduos.Franquia = _dr["QuantidadeFranquia"].ToString();
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
                            if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                                oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
                            if (_moePercentualContrato.Valor != "" && _moePercentualContrato.Valor != "&nbsp;")
                            {
                                oContratoResiduos.ValorUnitario = oContratoResiduos.ValorUnitario * ((Convert.ToDecimal(_moePercentualUnitarios.Valor) / 100) + 1);
                                oContratoResiduos.ValorUnitarioCobrancaMensal = oContratoResiduos.ValorUnitarioCobrancaMensal * ((Convert.ToDecimal(_moePercentualUnitarios.Valor) / 100) + 1);
                                oContratoResiduos.ValorContratoCobrancaMensal = oContratoResiduos.ValorContratoCobrancaMensal * ((Convert.ToDecimal(_moePercentualUnitarios.Valor) / 100) + 1);
                                oContratoResiduos.ValorExcedenteCobrancaMensal = oContratoResiduos.ValorExcedenteCobrancaMensal * ((Convert.ToDecimal(_moePercentualUnitarios.Valor) / 100) + 1);
                            }
                            oContratoResiduosDados.Inserir(oContratoResiduos);
                        }
                        hifDataReajuste.Value = Grade.Rows[LinhaContrato].Cells[8].Text;
                    }
                    else
                        lblMensagemResiduos.Text = "Data reajuste inválida!";
                }
            }
        }
    }

    protected void btnProcurar_Click1(object sender, EventArgs e)
    {
        if (Session["Clientes"] != null)
        {
            CancelarOperacao();
            clsClientes oCl = new clsClientes();
            oCl = (clsClientes)Session["Clientes"];
            hifCodigoCliente.Value = oCl.Codigo.ToString();
        }
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
    protected void GradeResiduos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ImageButton _ibnSalvar = new ImageButton();
            _ibnSalvar.Enabled = true;
            if (oItensMenuPermissoes.Alterar == 0)
            {
                _ibnSalvar = (ImageButton)e.Row.Cells[2].FindControl("ibnSalvar");
                _ibnSalvar.Enabled = false;
            }
            ImageButton _ibnExcluir = new ImageButton();
            _ibnExcluir.Enabled = true;
            if (oItensMenuPermissoes.Excluir == 0)
            {
                _ibnExcluir = (ImageButton)e.Row.Cells[2].FindControl("ibnExcluirResiduo");
                _ibnExcluir.Enabled = false;
            }

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
            //CarregaTiposDeCaixas(_ddlTipoCaixa);
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
            //forms_MOEDA _moeValorContratoCobrancaMensal = (forms_MOEDA)e.Row.Cells[14].FindControl("moeValorContratoCobrancaMensal");
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
            if (LinhaReajustes > 1)
            {
                _txtCodigoResiduo.ForeColor = System.Drawing.Color.Red;
                _txtDescricaoResiduo.ForeColor = System.Drawing.Color.Red;
                _txtCxDisp.ForeColor = System.Drawing.Color.Red;
                _ddlTipoCx.ForeColor = System.Drawing.Color.Red;
                _txtQtFrequenciaColeta.ForeColor = System.Drawing.Color.Red;
                _ddlFrequenciaColeta.ForeColor = System.Drawing.Color.Red;
                _ddlRoteiro.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao1CobrancaMensal.ForeColor = System.Drawing.Color.Red;
                _moeFranquiaCobrancaMensal.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao2CobrancaMensal.ForeColor = System.Drawing.Color.Red;
                _ddlPeriodicidadeCobrancaMensal.ForeColor = System.Drawing.Color.Red;
                e.Row.ForeColor = System.Drawing.Color.Red;
                //_moeValorContratoCobrancaMensal.ForeColor = System.Drawing.Color.Red;
                _moeValorExcedenteCobrancaMensal.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao1CobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _moeValorUnitario.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao2CobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _txtUnidade.ForeColor = System.Drawing.Color.Red;
                _ddlcondicaoCobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _moeFranquiaCobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _txtUnidadeCobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao3CobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _ddlexpressao4CobrancaPeso.ForeColor = System.Drawing.Color.Red;
                _txtOBS.ForeColor = System.Drawing.Color.Red;
                _txtDiasColeta.ForeColor = System.Drawing.Color.Red;
                _txtParticularidade.ForeColor = System.Drawing.Color.Red;
                _txtMesAnoBase.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                _txtCodigoResiduo.ForeColor = System.Drawing.Color.Black;
                _txtDescricaoResiduo.ForeColor = System.Drawing.Color.Black;
                _txtCxDisp.ForeColor = System.Drawing.Color.Black;
                _ddlTipoCx.ForeColor = System.Drawing.Color.Black;
                _txtQtFrequenciaColeta.ForeColor = System.Drawing.Color.Black;
                _ddlFrequenciaColeta.ForeColor = System.Drawing.Color.Black;
                _ddlRoteiro.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao1CobrancaMensal.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao2CobrancaMensal.ForeColor = System.Drawing.Color.Black;
                _ddlPeriodicidadeCobrancaMensal.ForeColor = System.Drawing.Color.Black;
                e.Row.ForeColor = System.Drawing.Color.Black;
                //_moeValorContratoCobrancaMensal.ForeColor = System.Drawing.Color.Black;
                _moeValorExcedenteCobrancaMensal.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao1CobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _moeValorUnitario.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao2CobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _txtUnidade.ForeColor = System.Drawing.Color.Black;
                _ddlcondicaoCobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _moeFranquiaCobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _txtUnidadeCobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao3CobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _ddlexpressao4CobrancaPeso.ForeColor = System.Drawing.Color.Black;
                _txtOBS.ForeColor = System.Drawing.Color.Black;
                _txtDiasColeta.ForeColor = System.Drawing.Color.Black;
                _txtParticularidade.ForeColor = System.Drawing.Color.Black;
                _txtMesAnoBase.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
    protected void ibnMudar1_Click1(object sender, ImageClickEventArgs e)
    {
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
    private void RefreshGradeResiduos()
    {
        _dtResiduos = new DataTable();
        if (oContratoResiduos.CodigoContrato > 0 && oContratoResiduos.DataReajuste != "")
            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste, Convert.ToInt32(hifCodigoCliente.Value));
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

        if (GradeReajustes.Rows.Count > 0)
        {
            TirarIconeExcluirDaGradeReajustes();
        }
    }

    protected void ibnExcluirResiduo_Click(object sender, ImageClickEventArgs e)
    {
        bExcluirLinha = true;
        bSalvarReajuste = false;
        TirarIconeExcluirDaGradeReajustes();
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
                hifCodigoCliente.Value != "" && geral.IsNumeric(hifCodigoCliente.Value))
            {
                oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);        // chave
                oContratoResiduos.CodigoCliente = Convert.ToInt32(hifCodigoCliente.Value);  // chave
                oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                oContratoResiduos.DataReajuste = hifDataReajuste.Value;                     // chave
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
                RefreshGradeResiduos();
            }
            else
            {
                lblMensagemResiduos.Text = "Exclusão inválida!";
            }
        }
    }
    private void ExcluirReajuste(int pLinhaExcluir)
    {
        if (pLinhaExcluir == 1)
        {            
            if (hifCodigo.Value != "" && geral.IsNumeric(hifCodigo.Value) && GradeReajustes.Rows[1].Cells[10].Text != "" && geral.IsNumeric(GradeReajustes.Rows[1].Cells[10].Text))
            {
                oReajustesDados.ExcluirPeloSequencial(Convert.ToInt32(GradeReajustes.Rows[1].Cells[10].Text));
                hifDataReajuste.Value = ((forms_DATA)GradeReajustes.Rows[2].Cells[3].FindControl("datDataReajuste")).Data;
                hifValorContrato.Value = ((forms_MOEDA)GradeReajustes.Rows[2].Cells[7].FindControl("moeValorContrato")).Valor;
                int LinhaContrato = 0;
                for (int i = 0; i < Grade.Rows.Count; i++)
                {
                    ImageButton ibn = new ImageButton();
                    ibn = (ImageButton)Grade.Rows[i].Cells[0].FindControl("ibnSelecionar");
                    if (ibn.ImageUrl.IndexOf("selecionado") > -1)
                    {
                        LinhaContrato = i;
                        break;
                    }
                }
                Grade.Rows[LinhaContrato].Cells[6].Text = Convert.ToDecimal(hifValorContrato.Value).ToString("N2");
                DropDownList _ddlTipoNegociacao = (DropDownList)GradeReajustes.Rows[pLinhaExcluir].Cells[4].FindControl("ddlTipoNegociacao");
                if (_ddlTipoNegociacao.Text.IndexOf("REAJUSTE") > -1)
                    Grade.Rows[LinhaContrato].Cells[8].Text = ((forms_DATA)GradeReajustes.Rows[1].Cells[3].FindControl("datDataReajuste")).Data;
                else
                    Grade.Rows[LinhaContrato].Cells[8].Text = ((forms_DATA)GradeReajustes.Rows[0].Cells[11].FindControl("ProximoReajuste")).Data;
                if (hifCodigoCliente.Value != "" && geral.IsNumeric(hifCodigoCliente.Value))
                {                    
                    oContratosDados.AlterarDataProximoReajuste(Grade.Rows[LinhaContrato].Cells[8].Text, 
                                                               Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(hifCodigoCliente.Value));
                    oContratosDados.AlterarValorContrato(Convert.ToDecimal(hifValorContrato.Value), Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(hifCodigoCliente.Value));
                    lblMensagemResiduos.Text = oContratoResiduosDados.Excluir(Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(hifCodigoCliente.Value),
                                                                              ((forms_DATA)GradeReajustes.Rows[1].Cells[3].FindControl("datDataReajuste")).Data);
                }
                hifDataReajuste.Value = Grade.Rows[LinhaContrato].Cells[8].Text;
                ClearGradeResiduos();
                
            }
            else
            {
                lblMensagemResiduos.Text = "Exclusão inválida!";
            }
        }
    }
    protected void ibnExcluirReajustes_Click(object sender, ImageClickEventArgs e)
    {
        bSalvarReajuste = false;
        ClearGradeResiduos();
        if (GradeReajustes.Rows.Count > 0)
        {
            ImageButton ibnExcluirReajuste = (ImageButton)GradeReajustes.Rows[1].Cells[2].FindControl("ibnExcluirReajustes");
            if (ibnExcluirReajuste != null)
            {
                if (ibnExcluirReajuste.ImageUrl.IndexOf("confirma.png") > -1)
                {
                    ExcluirReajuste(1);
                    ibnExcluirReajuste = null;
                    lblMensagemResiduos.Text = "Reajuste excluído com sucesso!";
                    MostraGradeReajustes();
                }
            }
            if (ibnExcluirReajuste != null)
            {
                ibnExcluirReajuste.ImageUrl = "~/Images/confirma.png";
                ibnExcluirReajuste.ToolTip = "Confirma?";
                lblMensagemResiduos.Text = "";
            }
        }
    }
    private void TirarIconeExcluirDaGradeReajustes()
    {
        if (GradeReajustes.Rows.Count > 0)
        {
            GradeReajustes.Rows[0].Cells[0].Text = "";
            GradeReajustes.Rows[0].Cells[2].Text = "";
            for (int _i = 2; _i < GradeReajustes.Rows.Count; _i++)
            {
                GradeReajustes.Rows[_i].Cells[2].Text = "";
            }
        }
    }

    protected void ibnSalvar_Click(object sender, ImageClickEventArgs e)
    {
        // Salvar registros
        try
        {
            foreach (GridViewRow gvr in GradeResiduos.Rows)
            {
                TextBox _txtCodigoResiduo = (TextBox)gvr.Cells[2].FindControl("txtCodigoResiduo");
                if (hifCodigo.Value != "" && geral.IsNumeric(hifCodigo.Value) && _txtCodigoResiduo.Text != "" && geral.IsNumeric(_txtCodigoResiduo.Text) &&
                    hifCodigoCliente.Value != "" && geral.IsNumeric(hifCodigoCliente.Value))
                {
                    oContratoResiduos = new clsContratoResiduos();
                    oContratoResiduos.CodigoContrato = Convert.ToInt32(hifCodigo.Value);  // chave
                    oContratoResiduos.CodigoCliente = Convert.ToInt32(hifCodigoCliente.Value);  // chave
                    oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                    oContratoResiduos.DataReajuste = hifDataReajuste.Value;                     // chave
                    TextBox _txtCaixaDisponivel = (TextBox)gvr.Cells[4].FindControl("txtCxDisp");
                    if (_txtCaixaDisponivel.Text != "" && geral.IsNumeric(_txtCaixaDisponivel.Text))
                        oContratoResiduos.CaixaDisponivel = Convert.ToInt32(_txtCaixaDisponivel.Text);
                    DropDownList _ddlTipoCaixa = (DropDownList)gvr.Cells[5].FindControl("ddlTipoCx");
                    oContratoResiduos.TipoCaixa = _ddlTipoCaixa.Text;
                    TextBox _txtQtFrequenciaColeta = (TextBox)gvr.Cells[6].FindControl("txtQtFrequenciaColeta");
                    DropDownList _ddlFrequenciaColeta = (DropDownList)gvr.Cells[6].FindControl("ddlFrequenciaColeta");
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
                    DropDownList ddlRoteiro = (DropDownList)gvr.Cells[7].FindControl("ddlRoteiro");
                    oContratoResiduos.Roteiro = ddlRoteiro.Text;
                    DropDownList ddlexpressao1CobrancaMensal = (DropDownList)gvr.Cells[7].FindControl("ddlexpressao1CobrancaMensal");
                    oContratoResiduos.expressao1CobrancaMensal = ddlexpressao1CobrancaMensal.Text;
                    forms_MOEDA moeFranquiaCobrancaMensal = (forms_MOEDA)gvr.Cells[8].FindControl("moeFranquiaCobrancaMensal");
                    oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor;
                    if (moeFranquiaCobrancaMensal.Valor != "")
                        oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(moeFranquiaCobrancaMensal.Valor);
                    DropDownList ddlexpressao2CobrancaMensal = (DropDownList)gvr.Cells[9].FindControl("ddlexpressao2CobrancaMensal");
                    oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
                    DropDownList ddlPeriodicidadeCobrancaMensal = (DropDownList)gvr.Cells[9].FindControl("ddlPeriodicidadeCobrancaMensal");
                    oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;
                    //forms_MOEDA moeValorContratoCobrancaMensal = (forms_MOEDA)gvr.Cells[10].FindControl("moeValorContratoCobrancaMensal");
                    //if (moeValorContratoCobrancaMensal.Valor != "")
                    //    oContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(moeValorContratoCobrancaMensal.Valor);
                    forms_MOEDA moeValorExcedenteCobrancaMensal = (forms_MOEDA)gvr.Cells[11].FindControl("moeValorExcedenteCobrancaMensal");
                    if (moeValorExcedenteCobrancaMensal.Valor != "")
                        oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);
                    DropDownList ddlexpressao1CobrancaPeso = (DropDownList)gvr.Cells[12].FindControl("ddlexpressao1CobrancaPeso");
                    oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
                    forms_MOEDA moeValorUnitario = (forms_MOEDA)gvr.Cells[13].FindControl("moeValorUnitario");
                    if (moeValorUnitario.Valor != "")
                        oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitario.Valor) * 100;
                    DropDownList ddlexpressao2CobrancaPeso = (DropDownList)gvr.Cells[14].FindControl("ddlexpressao2CobrancaPeso");
                    oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
                    TextBox txtUnidade = (TextBox)gvr.Cells[15].FindControl("txtUnidade");
                    oContratoResiduos.Unidade = txtUnidade.Text;
                    DropDownList ddlcondicaoCobrancaPeso = (DropDownList)gvr.Cells[16].FindControl("ddlcondicaoCobrancaPeso");
                    oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
                    forms_MOEDA moeFranquiaCobrancaPeso = (forms_MOEDA)gvr.Cells[17].FindControl("moeFranquiaCobrancaPeso");
                    if (moeFranquiaCobrancaPeso.Valor != "")
                        oContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(moeFranquiaCobrancaPeso.Valor);
                    TextBox txtUnidadeCobrancaPeso = (TextBox)gvr.Cells[18].FindControl("txtUnidadeCobrancaPeso");
                    oContratoResiduos.UnidadeCobrancaPeso = txtUnidadeCobrancaPeso.Text;
                    DropDownList ddlexpressao3CobrancaPeso = (DropDownList)gvr.Cells[19].FindControl("ddlexpressao3CobrancaPeso");
                    oContratoResiduos.expressao3CobrancaPeso = ddlexpressao3CobrancaPeso.Text;
                    DropDownList ddlexpressao4CobrancaPeso = (DropDownList)gvr.Cells[20].FindControl("ddlexpressao4CobrancaPeso");
                    oContratoResiduos.expressao4CobrancaPeso = ddlexpressao4CobrancaPeso.Text;
                    TextBox txtOBS = (TextBox)gvr.Cells[21].FindControl("txtOBS");
                    oContratoResiduos.OBS = txtOBS.Text;
                    TextBox txtDiasColeta = (TextBox)gvr.Cells[22].FindControl("txtDiasColeta");
                    oContratoResiduos.DiasColeta = txtDiasColeta.Text;
                    TextBox txtParticularidade = (TextBox)gvr.Cells[23].FindControl("txtParticularidade");
                    oContratoResiduos.Particularidade = txtParticularidade.Text;
                    TextBox txtMesAnoBase = (TextBox)gvr.Cells[23].FindControl("txtMesAnoBase");
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
                            oContratoResiduos.ValorUnitario = oContratoResiduos.ValorUnitario;
                            oContratoResiduosDados.Inserir(oContratoResiduos);
                        }
                    }
                }
                lblMensagemResiduos.Text = "Dados salvos com sucesso.";
            }
        }
        catch
        {
            lblMensagemResiduos.Text = "Dados inválidos!";
            TirarIconeExcluirDaGradeReajustes();
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
        TirarIconeExcluirDaGradeReajustes();
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
        TirarIconeExcluirDaGradeReajustes();
        this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
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
                if (!geral.IsNumeric(_qtfranquia[0]))
                    dr["FrequenciaColeta"] = _qtfranquia[0];
            }
        }
        DataView view = new DataView(_dt);
        DataTable _dtDistinct = view.ToTable(true, "FrequenciaColeta");
        foreach (DataRow dr in _dtDistinct.Rows)
        {
            if (dr[0].ToString() != "01" && dr[0].ToString() != "A" && geral.Left(dr[0].ToString().ToLower(), 4) != "cole" && dr[0].ToString() != "4")
                pddlFrequenciaColeta.Items.Add(dr[0].ToString());
            if (dr[0].ToString() == "DIARIA")
                pddlFrequenciaColeta.Items.Add("DIÁRIA");
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
        TirarIconeExcluirDaGradeReajustes();
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
    private void ClearGradeResiduos()
    {
        bMostraResiduos = false;
        lblTituloResiduos.Text = "";
        _dtResiduos = new DataTable();
        GradeResiduos.DataSource = _dtResiduos;
        GradeResiduos.DataBind();
        TirarIconeExcluirDaGradeReajustes();
    }
    private void ClearGradeReajustes()
    {
        GradeReajustes.DataSource = new DataTable();
        GradeReajustes.DataBind();
    }
    protected void ibnSalvarReajuste_Click(object sender, ImageClickEventArgs e)
    {
        ClearGradeResiduos();
        bSalvarReajuste = true;
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

    protected void GradeReajustesCriacao(object pGridView)
    {
        GridView HeaderGrid = (GridView)pGridView;
        GridViewRow HeaderGridRow = new GridViewRow(2, 2, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);

        TableCell HeaderCell = new TableCell();
        HeaderCell.Text = "";
        HeaderCell.Height = 24;
        HeaderCell.ColumnSpan = 5;
        HeaderGridRow.Cells.Add(HeaderCell);

        HeaderCell = new TableCell();
        HeaderCell.Style.Add("background-color", "#666666");
        HeaderCell.ForeColor = System.Drawing.Color.White;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderCell.BorderWidth = 1;
        HeaderCell.Text = "% REAJUSTE";
        HeaderCell.Font.Bold = true;
        HeaderCell.Font.Size = 8;
        HeaderCell.ColumnSpan = 2;
        HeaderGridRow.Cells.Add(HeaderCell);

        GradeReajustes.Controls[0].Controls.AddAt(0, HeaderGridRow);
    }


    protected void GradeReajustes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GradeReajustesCriacao(sender);
        }
    }
}