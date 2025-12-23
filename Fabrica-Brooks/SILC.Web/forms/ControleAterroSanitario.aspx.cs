using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class ControleAterroSanitario : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
        clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        clsUsuarios oUsuario = new clsUsuarios();
        clsClientes oClientes = new clsClientes();
        DataTable _dt = new DataTable();

        private decimal dTotalPeso = 0;
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "29");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("sempermissao.aspx");
            if (!IsPostBack)
            {
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
                        txtDataInicial.Data = Convert.ToDateTime(DateTime.Now.AddDays(-2)).ToString("dd/MM/yyyy");
                        txtDataFinal.Data = Convert.ToDateTime(DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy");
                        if (Request.QueryString["DataInicial"] != null && Request.QueryString["DataFinal"] != null)
                        {
                            txtDataInicial.Data = Request.QueryString["DataInicial"];
                            txtDataFinal.Data = Request.QueryString["DataFinal"];
                        }
                        string _codigoAterro = "";
                        if (Request.QueryString["CodigoAterro"] != null)
                        {
                            _codigoAterro = Request.QueryString["CodigoAterro"].ToString();
                            chkTijucas.Checked = false;
                            chkTransbordo.Checked = false;
                            if (_codigoAterro == "2")
                                chkTransbordo.Checked = true;
                            else if (_codigoAterro == "13")
                                chkTijucas.Checked = true;
                        }
                        if (Request.QueryString["CodigoCliente"] != null)
                        {
                            txtCodigoCliente.Text = Request.QueryString["CodigoCliente"].ToString();
                            btnMostraCliente_Click(new object(), EventArgs.Empty);
                            _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, txtCodigoCliente.Text, _codigoAterro);
                        }
                        else
                            _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, "", _codigoAterro);

                        _dt = oAterroSanitarioDados.AdicionaSubTotal(_dt);
                        Grade.DataSource = _dt;
                        Grade.DataBind();

                        txtHora.MaxLength = oAterroSanitarioDados.PegaTamanhoCampoVarChar("Hora");
                        txtTicket.MaxLength = oAterroSanitarioDados.PegaTamanhoCampoVarChar("NumeroTicket");

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
            txtData.Focus();
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oAterroSanitarioDados.DadoExiste(oAterroSanitario.Codigo);
            if (txtData.Data.Equals(""))
            {
                lblMensagem.Text = "Descarga Peso Total inválida!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oAterroSanitarioDados.Excluir(oAterroSanitario.Codigo);
                lblMensagem.Text = "Descarga Peso Total excluída com sucesso!";
                Grade.DataSource = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data);
                Grade.DataBind();
            }
        }
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Controle de Aterro";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código Controle de Aterro: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Data: " + txtData.Data + " \n";
            oLog.Log = oLog.Log + "Hora: " + txtHora.Text + " \n";
            if (chkTransbordo.Checked)
                oLog.Log = oLog.Log + "Local Aterro: Transbordo-2 \n";
            else
                oLog.Log = oLog.Log + "Local Aterro: Tijutas-13 \n";
            oLog.Log = oLog.Log + "Motorista: " + ctlMotorista.Valor + " \n";
            oLog.Log = oLog.Log + "Ticket: " + txtTicket.Text + " \n";
            oLog.Log = oLog.Log + "Caminhão: " + ctlCaminhao.Valor + " \n";
            oLog.Log = oLog.Log + "Total: " + moePesoTotal.Valor + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtData.Data.Equals(""))
                {
                    lblMensagem.Text = "Descarga Peso Total inválida!";
                }
                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oAterroSanitario = AtribuiDadosDoForm(oAterroSanitario);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oAterroSanitarioDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    oAterroSanitario.CodigoResiduo = 999; // sempre total
                    oAterroSanitario.Status = 1; // sempre total
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oAterroSanitarioDados.Inserir(oAterroSanitario);
                        Grade.DataSource = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data);
                        Grade.DataBind();
                        lblMensagem.Text = "Descarga Peso Total incluída com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oAterroSanitarioDados.Alterar(oAterroSanitario, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Descarga Peso Total alterada com sucesso!";
                            Grade.DataSource = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro Descarga Peso Total";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtData.Data.Equals(""))
                {
                    lblMensagem.Text = "Descarga Peso Total inválida!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oAterroSanitarioDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Descarga Peso Total excluída com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro Descarga Peso Total";
                    Grade.DataSource = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data);
                    Grade.DataBind();
                }
            }
            LimpaCampos();
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
        protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = "&nbsp;Exclusão Descarga Peso Total";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração Descarga Peso Total";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Data" &&
                e.CommandArgument.ToString() != "Hora" && e.CommandArgument.ToString() != "LocalAterro" &&
                e.CommandArgument.ToString() != "NumeroTicket" &&
                e.CommandArgument.ToString() != "NumeroMTR" && e.CommandArgument.ToString() != "NumeroCaixa" &&
                e.CommandArgument.ToString() != "TotalPeso" && e.CommandArgument.ToString() != "NomeCliente" &&
                e.CommandArgument.ToString() != "CodigoResiduo" &&
                e.CommandArgument.ToString() != "NomeMotorista" && e.CommandArgument.ToString() != "Modelo")
            {
                txtData.Data = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oAterroSanitario = oAterroSanitarioDados.PegaDados(oAterroSanitario, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oAterroSanitario);
                ctlMotorista.Texto = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
                ctlCaminhao.Texto = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text;
                MostraPesos();
            }
        }
        protected void AtribuiDadosDaClasse(clsAterroSanitario pAterroSanitario)
        {
            if (pAterroSanitario.Data == "01/01/0001" || pAterroSanitario.Data == "01/01/0100" || pAterroSanitario.Data == null)
                txtData.Data = "";
            else
                txtData.Data = Convert.ToDateTime(pAterroSanitario.Data).ToString("dd/MM/yyyy");
            txtHora.Text = pAterroSanitario.Hora;
            chkTransbordo.Checked = false;
            chkTijucas.Checked = false;
            if (pAterroSanitario.CodigoAterro == 2)
                chkTransbordo.Checked = true;
            else if (pAterroSanitario.CodigoAterro == 13)
                chkTijucas.Checked = true;
            ctlCaminhao.Valor = pAterroSanitario.CodigoCaminhao.ToString();
            ctlMotorista.Valor = pAterroSanitario.CodigoMotorista.ToString();
            txtTicket.Text = pAterroSanitario.NumeroTicket;
            moePesoTotal.Valor = pAterroSanitario.TotalPeso.ToString();
        }
        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            txtData.Data = "";
            txtHora.Text = "";
            chkTransbordo.Checked = true;
            chkTijucas.Checked = false;
            ctlCaminhao.Valor = "";
            ctlMotorista.Valor = "";
            txtTicket.Text = "";
            moePesoTotal.Valor = "";
        }
        protected clsAterroSanitario AtribuiDadosDoForm(clsAterroSanitario pAterroSanitario)
        {
            pAterroSanitario.Data = txtData.Data;
            pAterroSanitario.Hora = txtHora.Text;
            if (chkTransbordo.Checked)
                pAterroSanitario.CodigoAterro = 2;
            else if (chkTijucas.Checked)
                pAterroSanitario.CodigoAterro = 13;
            if (ctlCaminhao.Valor != "")
                pAterroSanitario.CodigoCaminhao = Convert.ToInt32(ctlCaminhao.Valor);
            if (ctlMotorista.Valor != "")
                pAterroSanitario.CodigoMotorista = Convert.ToInt32(ctlMotorista.Valor);
            pAterroSanitario.NumeroTicket = txtTicket.Text;
            if (moePesoTotal.Valor != "")
                pAterroSanitario.TotalPeso = Convert.ToDecimal(moePesoTotal.Valor);
            return pAterroSanitario;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro Descarga Peso Total";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Ok";
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                ImageButton x = new ImageButton();
                ImageButton y = new ImageButton();
                x = (ImageButton)e.Row.FindControl("ibnMudar");
                y = (ImageButton)e.Row.FindControl("ibnExcluir");
                if ((e.Row.Cells[11].Text == "&nbsp;" || e.Row.Cells[11].Text == "") && e.Row.Cells[2].Text != "0")
                {
                    x.Visible = true;
                    y.Visible = true;
                }
                else
                {
                    x.Visible = false;
                    y.Visible = false;
                }
                Button btnPesoIndividual = new Button();
                btnPesoIndividual = (Button)e.Row.FindControl("btnPesoIndividual");
                if (btnPesoIndividual != null)
                {
                    btnPesoIndividual.Enabled = false;
                    decimal _PesoTotal = 0;
                    if (e.Row.Cells[7].Text != "" && e.Row.Cells[7].Text != "&nbsp;")
                        _PesoTotal = oAterroSanitarioDados.PegaPesoTotal(e.Row.Cells[6].Text, Convert.ToDateTime(txtDataInicial.Data).Year.ToString());
                    else if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
                    {
                        e.Row.Cells[0].Style.Add("background", "#ffffcc");
                        e.Row.Cells[1].Style.Add("background", "#ffffcc");
                        e.Row.Cells[2].Style.Add("background", "#ffffcc");
                        e.Row.Cells[3].Style.Add("background", "#ffffcc");
                        e.Row.Cells[4].Style.Add("background", "#ffffcc");
                        e.Row.Cells[5].Style.Add("background", "#ffffcc");
                        e.Row.Cells[6].Style.Add("background", "#ffffcc");
                        e.Row.Cells[7].Style.Add("background", "#ffffcc");
                        e.Row.Cells[8].Style.Add("background", "#ffffcc");
                        e.Row.Cells[9].Style.Add("background", "#ffffcc");
                        e.Row.Cells[10].Style.Add("background", "#ffffcc");
                        e.Row.Cells[11].Style.Add("background", "#ffffcc");
                        e.Row.Cells[12].Style.Add("background", "#ffffcc");
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[9].Text = "Total dia";
                    }
                    if (_PesoTotal > 0)
                    {
                        oLancamentoMTRDados = new clsLancamentoMTRDados();
                        btnPesoIndividual.Text = (_PesoTotal / oLancamentoMTRDados.PegaItemsDescarga(e.Row.Cells[6].Text, Convert.ToDateTime(txtDataInicial.Data).Year.ToString())).ToString("N0");
                        btnPesoIndividual.Enabled = true;
                    }
                }
                if (e.Row.Cells[5].Text != "&nbsp;" && e.Row.Cells[5].Text != "")
                {
                    e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("2-", "").Replace("13-", "").Replace("02-", "");
                    if (e.Row.Cells[5].Text.IndexOf("PROACTIVA AT") > -1)
                        e.Row.Cells[5].Text = "Tijucas";
                    else if (e.Row.Cells[5].Text.IndexOf("PROACTIVA TB") > -1)
                        e.Row.Cells[5].Text = "TRANSB 282";
                }
                // TotalPeso
                if (e.Row.Cells[10].Text != "&nbsp;" && e.Row.Cells[10].Text != "" && e.Row.Cells[10].Text != null && e.Row.Cells[7].Text != "" && e.Row.Cells[7].Text != "&nbsp;")
                {
                    dTotalPeso = dTotalPeso + Convert.ToDecimal(e.Row.Cells[10].Text);
                    lblTotalPeso.Text = "Total peso: " + dTotalPeso.ToString();
                }
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            string _codigoAterrro = "";
            if (chkTransbordo.Checked)
                _codigoAterrro = "2";
            else if (chkTijucas.Checked)
                _codigoAterrro = "13";

            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != "&nbsp;")
                _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, txtCodigoCliente.Text, _codigoAterrro, geral.Ordem);
            else
                _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, "", _codigoAterrro, geral.Ordem);
            if (geral.Ordem.IndexOf("Data") > -1 || geral.Ordem.IndexOf("LocalAterro") > -1)
                _dt = oAterroSanitarioDados.AdicionaSubTotal(_dt);
            Grade.DataSource = _dt;
            Grade.DataBind();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            lblTituloTicketPeso.Text = "";
            GradeTicket.DataSource = new DataTable();
            GradeTicket.DataBind();
            string _codigoAterrro = "";
            if (chkTransbordo.Checked)
                _codigoAterrro = "2";
            else if (chkTijucas.Checked)
                _codigoAterrro = "13";

            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != "&nbsp;")
                _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, txtCodigoCliente.Text, _codigoAterrro);
            else
                _dt = oAterroSanitarioDados.PegaDados(oAterroSanitario, 0, false, txtDataInicial.Data, txtDataFinal.Data, "", _codigoAterrro);

            _dt = oAterroSanitarioDados.AdicionaSubTotal(_dt);
            Grade.DataSource = _dt;
            Grade.DataBind();
        }
        protected void ViewStateGetForm()
        {

            ViewState["CodigoCliente"] = txtCodigoCliente.Text;
            ViewState["Data"] = txtData.Data;
            ViewState["Hora"] = txtHora.Text;
            if (chkTransbordo.Checked)
                ViewState["CodigoAterro"] = 2;
            else if (chkTijucas.Checked)
                ViewState["CodigoAterro"] = 13;
            ViewState["CodigoCaminhao"] = ctlCaminhao.Valor;
            ViewState["CodigoMotorista"] = ctlMotorista.Valor;
            ViewState["Ticket"] = txtTicket.Text;
            ViewState["PesoTotal"] = moePesoTotal.Valor;
        }
        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            txtCodigoCliente.Text = "";
            lblNomeCliente.Text = "";
            if (Session["Clientes"] != null)
            {
                oClientes = (clsClientes)Session["Clientes"];
                txtCodigoCliente.Text = oClientes.Codigo.ToString("000000");
                lblNomeCliente.Text = oClientes.NomeFantasia;
            }
            ViewStateGetForm();
        }
        protected void btnMostraCliente_Click(object sender, EventArgs e)
        {
            lblNomeCliente.Text = "";
            clsClienteDados oClienteDados = new clsClienteDados();
            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != null && txtCodigoCliente.Text != "&nbsp;")
            {
                oClientes.Codigo = Convert.ToInt32(txtCodigoCliente.Text);
                oClientes.NomeFantasia = oClienteDados.PegaNomeFantasia(Convert.ToInt32(txtCodigoCliente.Text));
                Session["Clientes"] = oClientes;
                txtCodigoCliente.Text = oClientes.Codigo.ToString("000000");
                lblNomeCliente.Text = oClientes.NomeFantasia;
            }
        }
        protected void ViewStateSetForm()
        {
            txtCodigoCliente.Text = ViewState["CodigoCliente"].ToString();
            txtData.Data = ViewState["Data"].ToString();
            txtHora.Text = ViewState["Hora"].ToString();
            if (Convert.ToInt32(ViewState["CodigoAterro"]) == 2)
                chkTransbordo.Checked = true;
            else if (Convert.ToInt32(ViewState["CodigoAterro"]) == 13)
                chkTijucas.Checked = true;
            ctlCaminhao.Valor = ViewState["CodigoCaminhao"].ToString();
            ctlMotorista.Valor = ViewState["CodigoMotorista"].ToString();
            txtTicket.Text = ViewState["Ticket"].ToString();
            moePesoTotal.Valor = ViewState["PesoTotal"].ToString();
        }

        private void MostraPesos()
        {
            // mostrar grade dos items do ticket
            //Campos:
            //Sequencial - Código - Cliente       - Peso Individual - Container - Data Retirada
            DataTable _dtTicket = new DataTable();
            if (txtTicket.Text != "")
            {
                _dtTicket = oLancamentoMTRDados.PegaListaItemsDescarga(txtTicket.Text, Convert.ToDateTime(txtDataInicial.Data).Year.ToString());
                lblTituloTicketPeso.Text = "Lista de clientes Ticket: " + txtTicket.Text + " - Items: " + (_dtTicket.Rows.Count - 1).ToString();
            }
            GradeTicket.DataSource = _dtTicket;
            GradeTicket.DataBind();
        }
        protected void btnPesoIndividual_Click(object sender, EventArgs e)
        {
            // vai para Grade_DataRowCommand
        }

        protected void GradeTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[5].Text != "&nbsp;" && e.Row.Cells[5].Text != "")
                    e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy");

            }
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string _codigoAterro = "";
            if (chkTransbordo.Checked)
                _codigoAterro = "2";
            else if (chkTijucas.Checked)
                _codigoAterro = "13";

            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != "&nbsp;")
                Response.Redirect("../Relatorios/RelatorioControleAterro.aspx?DataInicial=" + txtDataInicial.Data + "&DataFinal=" + txtDataFinal.Data + "&CodigoCliente=" + txtCodigoCliente.Text +
                                  "&CodigoAterro=" + _codigoAterro);
            else
                Response.Redirect("../Relatorios/RelatorioControleAterro.aspx?DataInicial=" + txtDataInicial.Data + "&DataFinal=" + txtDataFinal.Data +
                                  "&CodigoAterro=" + _codigoAterro);
        }
    }
}