using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class Contratos_Residuos : System.Web.UI.Page
    {
        bool bExcluirLinha = false;
        clsResiduos oResiduo = new clsResiduos();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
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
                        geral.CodigoUsuarioAtual = oUsuario.Codigo;
                        if (oUsuario.Aplicativo == true)
                            menu1.Visible = false;
                        else
                            menu1.Visible = true;
                        lblMensagemResiduos.Text = "";
                        if (Request.QueryString["CodigoGerado"] == null)
                            Response.Redirect("menu.aspx", true);
                        else
                            intCodigoGerado.Valor = Request.QueryString["CodigoGerado"].ToString();
                        if (intCodigoGerado.Valor != "" && geral.IsNumeric(intCodigoGerado.Valor))
                        {
                            oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(intCodigoGerado.Valor), 0);
                            intCodigoCliente.Valor = oContratos.CodigoCliente.ToString("000000");
                            txtCNPJ_CPF.Text = oContratos.CNPJ_CPF;
                            txtNome.Text = oContratos.Nome;
                            txtNomeFantasia.Text = oContratos.NomeFantasia;
                            datDataInicio.Data = oContratos.DataInicio;
                            intNumeroContrato.Valor = oContratos.NumeroContrato.ToString();
                            moeValorContrato.Valor = oContratos.ValorContrato.ToString("N2");
                            geral.Ordem = "DescricaoReduzida";
                            _dt = oResiduoDados.PreencheDataTableSoComResiduos(geral.Ordem, txtFiltro.Text, ddlFiltro.Text);
                            Grade.DataSource = _dt;
                            Grade.DataBind();
                            hifResiduos.Value = "";
                            foreach (DataRow _dr in _dt.Rows)
                            {
                                hifResiduos.Value = hifResiduos.Value + ">" + _dr["Codigo"].ToString() + "|" + _dr["DescricaoReduzida"].ToString() + "<--";
                            }
                            CarregaTiposDeCaixas(p_ddlTiposDeCaixas);
                            oContratoResiduos.CodigoContrato = Convert.ToInt32(intCodigoGerado.Valor);
                            oContratoResiduos.DataReajuste = datDataInicio.Data;
                            RefreshGradeResiduos();
                        }
                        else
                        {
                            Response.Redirect("menu.aspx", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagemResiduos.Text = ex.Message;
                    }
                }
                string r = Request.QueryString["view"];
                if (r != null)
                    ViewStateSetForm();
            }
            txtNome.Focus();
        }

        protected void AtribuiDadosDaClasse(clsContratos pContratos)
        {
            txtNome.Text = pContratos.Nome;
            txtCNPJ_CPF.Text = pContratos.CNPJ_CPF;
            txtNomeFantasia.Text = pContratos.NomeFantasia;
            if (pContratos.DataInicio == "01/01/0001" || pContratos.DataInicio == "01/01/0100" || pContratos.DataInicio == null || pContratos.DataInicio == "")
                datDataInicio.Data = "";
            else
                datDataInicio.Data = Convert.ToDateTime(pContratos.DataInicio).ToString("dd/MM/yyyy");
            moeValorContrato.Valor = pContratos.ValorContrato.ToString("N2");
            hifValorContrato.Value = pContratos.ValorContrato.ToString();
        }

        protected void LimpaCampos()
        {
            intCodigoCliente.Valor = "";
            txtNome.Text = "";
            txtNomeFantasia.Text = "";
            txtCNPJ_CPF.Text = "";
            datDataInicio.Data = "";
            moeValorContrato.Valor = "";
            hifValorContrato.Value = "";
            GradeResiduos.DataSource = "";
            GradeResiduos.DataBind();
        }
        protected clsContratos AtribuiDadosDoForm(clsContratos pContratos)
        {
            if (geral.IsNumeric(intCodigoCliente.Valor))
            {
                pContratos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
                pContratos.DataInicio = datDataInicio.Data;
                if (moeValorContrato.Valor != "")
                    pContratos.ValorContrato = Convert.ToDecimal(moeValorContrato.Valor);
            }
            return pContratos;
        }
        protected void ViewStateGetForm()
        {
            ViewState["Nome"] = txtNomeFantasia.Text;
        }
        protected void ViewStateSetForm()
        {
            txtNomeFantasia.Text = ViewState["Nome"].ToString();
        }

        private void CancelarOperacao()
        {
            lblTitulo.Text = "&nbsp;Cadastro de Contratos";
            LimpaCampos();
            hifCodigo.Value = "";
            GradeResiduos.DataSource = "";
            GradeResiduos.DataBind();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarOperacao();
            _dt = oContratosDados.PegaDados(false);
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
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string _Campo = "";
            if (ddlFiltro.Text == "Descrição Reduzida")
                _Campo = "DescricaoReduzida";
            else if (ddlFiltro.Text == "Ativos")
                _Campo = "DescricaoReduzida";
            else
                _Campo = ddlFiltro.Text;

            Grade.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(_Campo, txtFiltro.Text, _Campo);
            Grade.DataBind();
            this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
        }

        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            ViewStateGetForm();
        }
        protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (geral.Ordem == "")
                geral.Ordem = "NomeFantasia";
        }
        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk_Click(sender, e);
        }
        protected void ibnMudar1_Click(object sender, ImageClickEventArgs e)
        {
            //lblTituloReajustes.Text = "Alteração de Reajuste";
        }
        protected void btnCancelaAlteracaoReajuste_Click(object sender, EventArgs e)
        {
            CancelarOperacao();
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
        protected void GradeResiduos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(2, 2, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.Height = 30;
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
                TextBox _txtCodigoResiduo = (TextBox)e.Row.FindControl("txtCodigoResiduo");
                TextBox _txtCxDisp = (TextBox)e.Row.FindControl("txtCxDisp");
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
            }
        }
        private void SalvarLog(string pOperacao, string pLog)
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
        protected void ibnSalvar_Click(object sender, ImageClickEventArgs e)
        {
            // Salvar registros
            try
            {
                foreach (GridViewRow gvr in GradeResiduos.Rows)
                {
                    TextBox _txtCodigoResiduo = (TextBox)gvr.Cells[2].FindControl("txtCodigoResiduo");
                    if (intCodigoGerado.Valor != "" && geral.IsNumeric(intCodigoGerado.Valor) && _txtCodigoResiduo.Text != "" && geral.IsNumeric(_txtCodigoResiduo.Text) &&
                        intCodigoCliente.Valor != "" && geral.IsNumeric(intCodigoCliente.Valor))
                    {
                        oContratoResiduos = new clsContratoResiduos();
                        oContratoResiduos.CodigoContrato = Convert.ToInt32(intCodigoGerado.Valor);  // chave
                        oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);  // chave
                        oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                        oContratoResiduos.DataReajuste = datDataInicio.Data;                        // chave
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
                        MOEDA moeFranquiaCobrancaMensal = (MOEDA)gvr.Cells[8].FindControl("moeFranquiaCobrancaMensal");
                        oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor;
                        DropDownList ddlexpressao2CobrancaMensal = (DropDownList)gvr.Cells[9].FindControl("ddlexpressao2CobrancaMensal");
                        oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
                        DropDownList ddlPeriodicidadeCobrancaMensal = (DropDownList)gvr.Cells[9].FindControl("ddlPeriodicidadeCobrancaMensal");
                        oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;
                        MOEDA moeValorExcedenteCobrancaMensal = (MOEDA)gvr.Cells[11].FindControl("moeValorExcedenteCobrancaMensal");
                        if (moeValorExcedenteCobrancaMensal.Valor != "")
                            oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);
                        DropDownList ddlexpressao1CobrancaPeso = (DropDownList)gvr.Cells[12].FindControl("ddlexpressao1CobrancaPeso");
                        oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
                        MOEDA moeValorUnitario = (MOEDA)gvr.Cells[13].FindControl("moeValorUnitario");
                        if (moeValorUnitario.Valor != "")
                            oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitario.Valor);
                        DropDownList ddlexpressao2CobrancaPeso = (DropDownList)gvr.Cells[14].FindControl("ddlexpressao2CobrancaPeso");
                        oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
                        TextBox txtUnidade = (TextBox)gvr.Cells[15].FindControl("txtUnidade");
                        oContratoResiduos.Unidade = txtUnidade.Text;
                        DropDownList ddlcondicaoCobrancaPeso = (DropDownList)gvr.Cells[16].FindControl("ddlcondicaoCobrancaPeso");
                        oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
                        MOEDA moeFranquiaCobrancaPeso = (MOEDA)gvr.Cells[17].FindControl("moeFranquiaCobrancaPeso");
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
                                SalvarLog("Alteração", sLog);
                                oContratoResiduosDados.Alterar(oContratoResiduos, oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste);
                            }
                            else
                            {
                                SalvarLog("Inclusão", sLog);
                                oContratoResiduosDados.Inserir(oContratoResiduos);
                            }
                        }
                    }
                    RefreshGradeResiduos();
                    lblMensagemResiduos.Text = "Dados salvos com sucesso.";
                }
            }
            catch
            {
                lblMensagemResiduos.Text = "Dados inválidos!";
            }
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
            _dtResiduos = oContratoResiduosDados.PreencheDataTableContratoResiduos("DataReajuste desc", oContratoResiduos.CodigoContrato, oContratoResiduos.DataReajuste, Convert.ToInt32(intCodigoCliente.Valor));
            decimal _ValorTotalContratoCobrancaMensal = 0;
            if (_dtResiduos.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dtResiduos.Rows)
                {

                    if (_dr["ValorUnitario"].ToString() != "")
                        if (Convert.ToDecimal(_dr["ValorUnitario"]) > 0)
                            _dr["ValorUnitario"] = Convert.ToDecimal(_dr["ValorUnitario"]);
                    if (_dr["ValorContratoCobrancaMensal"].ToString() != "")
                        _ValorTotalContratoCobrancaMensal = _ValorTotalContratoCobrancaMensal + Convert.ToDecimal(_dr["ValorContratoCobrancaMensal"]);
                }
            }
            _dtResiduos.NewRow();
            _dtResiduos.Rows.Add();
            _dtResiduos.Rows[_dtResiduos.Rows.Count - 1][0] = intCodigoGerado.Valor;
            _dtResiduos.Rows[_dtResiduos.Rows.Count - 1]["ValorContratoCobrancaMensal"] = _ValorTotalContratoCobrancaMensal;
            GradeResiduos.DataSource = _dtResiduos;
            GradeResiduos.DataBind();

            TextBox _txtDescricaoResiduo = (TextBox)GradeResiduos.Rows[_dtResiduos.Rows.Count - 1].Cells[4].FindControl("txtDescricaoResiduo");
            if (_txtDescricaoResiduo != null)
                _txtDescricaoResiduo.Text = "F2 - Pesquisa";

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
                MOEDA moeFranquiaCobrancaMensal = (MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[8].FindControl("moeFranquiaCobrancaMensal");
                oContratoResiduos.Franquia1CobrancaMensal = moeFranquiaCobrancaMensal.Valor;
                DropDownList ddlexpressao2CobrancaMensal = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[9].FindControl("ddlexpressao2CobrancaMensal");
                oContratoResiduos.expressao2CobrancaMensal = ddlexpressao2CobrancaMensal.Text;
                DropDownList ddlPeriodicidadeCobrancaMensal = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[9].FindControl("ddlPeriodicidadeCobrancaMensal");
                oContratoResiduos.PeriodicidadeCobrancaMensal = ddlPeriodicidadeCobrancaMensal.Text;
                MOEDA moeValorExcedenteCobrancaMensal = (MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[11].FindControl("moeValorExcedenteCobrancaMensal");
                if (moeValorExcedenteCobrancaMensal.Valor != "")
                    oContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(moeValorExcedenteCobrancaMensal.Valor);
                DropDownList ddlexpressao1CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[12].FindControl("ddlexpressao1CobrancaPeso");
                oContratoResiduos.expressao1CobrancaPeso = ddlexpressao1CobrancaPeso.Text;
                MOEDA moeValorUnitario = (MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[13].FindControl("moeValorUnitario");
                if (moeValorUnitario.Valor != "")
                    oContratoResiduos.ValorUnitario = Convert.ToDecimal(moeValorUnitario.Valor);
                DropDownList ddlexpressao2CobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[14].FindControl("ddlexpressao2CobrancaPeso");
                oContratoResiduos.expressao2CobrancaPeso = ddlexpressao2CobrancaPeso.Text;
                TextBox txtUnidade = (TextBox)GradeResiduos.Rows[pLinhaExcluir].Cells[15].FindControl("txtUnidade");
                oContratoResiduos.Unidade = txtUnidade.Text;
                DropDownList ddlcondicaoCobrancaPeso = (DropDownList)GradeResiduos.Rows[pLinhaExcluir].Cells[16].FindControl("ddlcondicaoCobrancaPeso");
                oContratoResiduos.condicaoCobrancaPeso = ddlcondicaoCobrancaPeso.Text;
                MOEDA moeFranquiaCobrancaPeso = (MOEDA)GradeResiduos.Rows[pLinhaExcluir].Cells[17].FindControl("moeFranquiaCobrancaPeso");
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

                if (intCodigoGerado.Valor != "" && geral.IsNumeric(intCodigoGerado.Valor) && _txtCodigoResiduo.Text != "" && geral.IsNumeric(_txtCodigoResiduo.Text) &&
                    intCodigoCliente.Valor != "" && geral.IsNumeric(intCodigoCliente.Valor))
                {
                    oContratoResiduos.CodigoContrato = Convert.ToInt32(intCodigoGerado.Valor);  // chave
                    oContratoResiduos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);  // chave
                    oContratoResiduos.CodigoResiduo = Convert.ToInt32(_txtCodigoResiduo.Text);  // chave
                    oContratoResiduos.DataReajuste = datDataInicio.Data;                        // chave
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
        protected void ibnExcluirResiduo_Click(object sender, ImageClickEventArgs e)
        {
            bExcluirLinha = true;
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "DescricaoReduzida" && e.CommandArgument.ToString() != "Grupo" &&
                e.CommandArgument.ToString() != "CodigoIBAMA_Analitico" && e.CommandArgument.ToString() != "Classe")
            {
                if (Convert.ToInt32(e.CommandArgument) < 6)
                {
                    hifCodigoResiduo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                    TiraSelecionado();
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
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";
            Grade.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(geral.Ordem, txtFiltro.Text, ddlFiltro.Text);
            Grade.DataBind();
            this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
        }
        protected void Grade_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Grade.DataSource = oResiduoDados.PreencheDataTableSoComResiduos(geral.Ordem, txtFiltro.Text, ddlFiltro.Text);
            }
            finally
            {
                Grade.PageIndex = e.NewPageIndex;
                Grade.DataBind();
            }
            this.ClientScript.RegisterStartupScript(GetType(), "nãomostrar", "<script>document.getElementById('PanelResiduos').style.visibility = 'visible';</script>");
        }
        private void CarregaTiposDeCaixas(DropDownList pddlTipoCaixa)
        {
            pddlTipoCaixa.Items.Clear();
            pddlTipoCaixa.Items.Add("");
            foreach (DataRow dr in oContratoResiduosDados.PegaConteineresDistintos().Rows)
            {
                pddlTipoCaixa.Items.Add(dr[0].ToString());
            }
        }
    }
}