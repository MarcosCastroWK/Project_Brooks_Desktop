using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class TicketsPendentes : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsLancamentos oLancamentos = new clsLancamentos();
    clsLancamentosDados oLancamentosDados = new clsLancamentosDados();
    clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "17");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
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

                    datDataRetirada.Data = DateTime.Now.ToString("dd/MM/yyyy");
                    txtHora.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                    Grade.DataSource = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
                    Grade.DataBind();

                    PermissaoAlterar();

                    PreencheDDL_DestinoFinal();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        ddlDestinoFinal.Focus();
    }
    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
    private void PreencheDDL_DestinoFinal()
    {
        oDestinoFinalDados = new clsDestinoFinalDados();
        ddlDestinoFinal.Items.Clear();
        ddlDestinoFinal.Items.Add(" ");
        DataTable _dtdf = oDestinoFinalDados.PreencheDTDestinoSoCodigoNome("Codigo");
        foreach (DataRow dr in _dtdf.Rows)
        {
            if (dr[2].ToString() == "")
                ddlDestinoFinal.Items.Add(Convert.ToInt32(dr[0]).ToString() + '-' + dr[1].ToString().Split(" "[0])[0]);
            else
                ddlDestinoFinal.Items.Add(Convert.ToInt32(dr[0]).ToString() + '-' + dr[2].ToString());
        }
        ddlDestinoFinal.Text = " ";
    }

    private void SalvarLog(string pOperacao, string pContinuacaoDoLog)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Tickets pendentes";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + pContinuacaoDoLog + " \n";
        oLogDados.Inserir(oLog);
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (intNumeroTicket.Text.Equals(""))
            {
                lblMensagem.Text = "Número do Ticket inválido!";
            }
            
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                string msgErr = "";
                _dt = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["CodigoCaminhoRetirada"].ToString() != "")
                    {
                        if (dr["CodigoCaminhoRetirada"].ToString() == CAMINHAO1.Valor)
                        {
                            // salvar
                            clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
                            clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
                            if (geral.RetiraLetras(dr["Deposito"].ToString().Replace("-", "")) != "")
                            {
                                if (geral.IsNumeric(geral.RetiraLetras(dr["Deposito"].ToString().Replace("-", ""))))
                                    oAterroSanitario.CodigoAterro = Convert.ToInt32(geral.RetiraLetras(dr["Deposito"].ToString()).Replace("-", ""));
                            }
                            oAterroSanitario.Data = datDataRetirada.Data;
                            oAterroSanitario.Hora = txtHora.Text;
                            oAterroSanitario.NumeroTicket = intNumeroTicket.Text;
                            if (dr["CodigoCaminhoRetirada"].ToString() != "")
                                oAterroSanitario.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhoRetirada"].ToString());
                            oAterroSanitario.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"].ToString());
                            oAterroSanitario.NumeroCaixa = dr["NumeroCaixa"].ToString();
                            if (dr["Quantidade"].ToString() != "")
                                oAterroSanitario.TotalPeso = Convert.ToDecimal(dr["Quantidade"].ToString());
                            oAterroSanitario.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"].ToString());
                            oAterroSanitario.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"].ToString());
                            oAterroSanitario.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"].ToString());
                            oAterroSanitario.Status = 0;
                            oAterroSanitario.Codigo = 0;
                            if (dr["CodigoMotorista"].ToString() != "")
                                oAterroSanitario.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"].ToString());
                            oAterroSanitarioDados.Inserir(oAterroSanitario);

                            string slog = "";
                            slog = slog + "Nº Lançamento: " + dr["NumeroLancamento"].ToString() + " \n";
                            slog = slog + "Nº Ticket: " + intNumeroTicket.Text + " \n";
                            slog = slog + "Nº MTR: " + dr["NumeroMTR"].ToString() + " \n";
                            slog = slog + "Código resíduo: " + dr["CodigoResiduo"].ToString() + " \n";
                            slog = slog + "Destino final: " + ddlDestinoFinal.Text + " \n";
                            slog = slog + "Código caminhão: " + dr["CodigoCaminhoRetirada"].ToString() + " \n";
                            SalvarLog("Alteração", slog);
                            msgErr = msgErr + oLancamentosDados.SalvarNumeroTicketPendente(Convert.ToInt32(dr["NumeroLancamento"]), intNumeroTicket.Text,
                                                                Convert.ToInt32(dr["NumeroMTR"]), Convert.ToInt32(dr["CodigoResiduo"]),
                                                                ddlDestinoFinal.Text, Convert.ToInt32(dr["CodigoCaminhoRetirada"]), 
                                                                dr["Observacao"].ToString().Replace("FALTA TICKET PESO", ""));
                        }
                    }
                }
                if (msgErr.Length > 0)
                    lblMensagem.Text = msgErr;
                else
                {
                    lblMensagem.Text = "Ticket(s) Pendente(s) alterado(s) com sucesso!";
                    Grade.DataSource = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
                    Grade.DataBind();
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Ticket(s) Pendente(s)";
                LimpaCampos();
            }
        }
    }

    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Tickets Pendente";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "NumeroLancamento" && e.CommandArgument.ToString() != "NomeFantasia" && 
            e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "DataRetirada" &&
            e.CommandArgument.ToString() != "Deposito" && e.CommandArgument.ToString() != "Caminhao" &&
            e.CommandArgument.ToString() != "Motorista" && e.CommandArgument.ToString() != "NumeroCaixa" && 
            e.CommandArgument.ToString() != "Observacao")
        {
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            datDataRetirada.Data = Convert.ToDateTime(oLancamentos.DataRetirada).ToString("dd/MM/yyyy");

            Grade.DataSource = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
            Grade.DataBind();

            PermissaoAlterar();
        }
    }
    protected void LimpaCampos()
    {
        CAMINHAO1.Valor = "";
        CAMINHAO1.Texto = "";
        Session["Caminhao"] = null;
        PreencheDDL_DestinoFinal();
        hifCodigo.Value = "";
        datDataRetirada.Data = DateTime.Now.ToString("dd/MM/yyyy");
        txtHora.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
        intNumeroTicket.Text = "";
        PermissaoAlterar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Ticket(s) Pendente(s)";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
        Grade.DataSource = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
        Grade.DataBind();
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[5].Text = "";

        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oLancamentosDados.PreencheDataTableTicketsPendentes(geral.Ordem);
        Grade.DataBind();
    }

    protected void Aplicar_Click(object sender, EventArgs e)
    {
        if (intNumeroTicket.Text == "")
            lblMensagem.Text = "Ticket inválido!";
        else
        {
            _dt = oLancamentosDados.PreencheDataTableTicketsPendentes("Modelo");
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr["CodigoCaminhoRetirada"].ToString() != "")
                {
                    if (dr["CodigoCaminhoRetirada"].ToString() == CAMINHAO1.Valor)
                    {
                        dr["Deposito"] = ddlDestinoFinal.Text;
                        dr["Observacao"] = intNumeroTicket.Text;
                    }
                    else
                        dr.Delete();
                }
            }
            Grade.DataSource = _dt;
            Grade.DataBind();
            lblMensagem.Text = "Aplicação realizada com sucesso!";
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e) { }
}