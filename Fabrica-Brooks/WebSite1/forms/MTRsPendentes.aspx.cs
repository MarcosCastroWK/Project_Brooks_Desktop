using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public partial class MTRsPendentes : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsLancamentos oLancamentos = new clsLancamentos();
    clsLancamentosDados oLancamentosDados = new clsLancamentosDados();
    clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsClientes oCliente = new clsClientes();
    clsClienteDados oClienteDados = new clsClienteDados();
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "18");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            Salvar.Enabled = false;
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

                    Grade.DataSource = oLancamentosDados.PreencheDataTableMTRsPendentes("NomeFantasia");
                    Grade.DataBind();

                    PermissaoAlterar();

                    if (Grade.Rows.Count > 0)
                    {
                        if (Grade.Rows[1].Cells[4].Text != "")
                            hifCodigo.Value = Grade.Rows[1].Cells[4].Text;

                        if (Grade.Rows[1].Cells[2].Text != "")
                            oLancamentos.NumeroLancamento = Convert.ToInt32(Grade.Rows[1].Cells[2].Text);
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        intNumeroMTRe.Focus();
    }
    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
    private void SalvarLog(string pOperacao, string pContinuacaoDoLog)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "MTRs pendentes";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + pContinuacaoDoLog + " \n";
        oLogDados.Inserir(oLog);
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        intNumeroMTRe.Text = intNumeroMTRe.Text.Trim();
        if (intNumeroMTRe.Text.Length < 10)
        {
            lblMensagem.Text = "Quantidade de dígitos no número da MTR-e inválida!";
        }
        else if (Salvar.Text == "Ok")
        {
            //string xjson = "";
            //try
            //{
            //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://mtr.ima.sc.gov.br/mtrservice/verificaSituacaoManifesto");
            //    httpWebRequest.ContentType = "application/json";
            //    httpWebRequest.Method = "POST";

            //    clsMTReLogin oLogin = new clsMTReLogin();
            //    string json = "";
            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
            //        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
            //        int _codigoDestino = oLancamentosMTRDados.PegaCodigoDestinoFinal(intNrLancamento.Valor, intCodigoResiduo.Valor);
            //        oDestinoFinalDados.PegaDados(oDestinoFinal, _codigoDestino);
            //        json = json + "{\"manifestoCodigo\":" + intNumeroMTRe.Text + ", ";
            //        json = json + "\"cnpGerador\":\"" + geral.RetiraCharsCNPJCPF(txtCNPJ_CPF.Text) + "\", ";
            //        if (txtMotorista.Text == "TERCEIROS")
            //            json = json + "\"cnpTransportador\":\"" + geral.RetiraCharsCNPJCPF(oDestinoFinal.CNPJ) + "\", ";
            //        else
            //            json = json + "\"cnpTransportador\":\"" + oLogin.cnpTransportador + "\", ";
            //        json = json + "\"cnpDestinador\":\"" + geral.RetiraCharsCNPJCPF(oDestinoFinal.CNPJ) + "\", ";
            //        json = json + "\"login\":\"" + geral.RetiraCharsCNPJCPF(txtCNPJ_CPF.Text) + "\", ";
            //        json = json + "\"senha\":\"" + txtSenhaAcesso.Text + "\"}";
            //        streamWriter.Write(json);
            //    }
            //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var result = streamReader.ReadToEnd();
            //        xjson = result.ToString();
            //    }
            //    xjson = xjson.Replace("null", "-1");
            //}
            //catch (Exception ex)
            //{
            //    lblMensagem.Text = ex.Message;
            //}
            //var post = JsonConvert.DeserializeObject<clsPostMTReConsulta>(xjson);
            //if (lblMensagem.Text != "") { }
            //else if (post.retornoCodigo > 0)
            //{
            //    lblMensagem.Text = post.retorno + " - codigo: " + post.retornoCodigo.ToString();
            //}
            if (intNrLancamento.Valor.Equals(""))
            {
                lblMensagem.Text = "Número Lançamento inválido!";
            }
            else if (intCodigoCliente.Valor.Equals(""))
            {
                lblMensagem.Text = "Código do Cliente inválido!";
            }
            else if (intNumeroMTR.Text.Equals(""))
            {
                lblMensagem.Text = "Número MTR inválida!";
            }
            else if (intNumeroMTRe.Text.Equals(""))
            {
                lblMensagem.Text = "Número MTR-e inválida!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oLancamentosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Alterar")
                {
                    string slog = "";
                    slog = slog + "Nº Lançamento: " + intNrLancamento.Valor + " MTR: " + intNumeroMTR.Text + " \n";
                    slog = slog + "Código resíduo: " + intCodigoResiduo.Valor + " \n";
                    slog = slog + "Nº MTR-e: " + intNumeroMTRe.Text + " \n";
                    SalvarLog("Alteração", slog);
                    string msgErr = oLancamentosDados.SalvarNumeroMTRe(Convert.ToInt32(intNrLancamento.Valor), Convert.ToInt64(intNumeroMTRe.Text), 
                                                                       Convert.ToInt32(intNumeroMTR.Text),
                                                                       Convert.ToInt32(intCodigoResiduo.Valor));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "MTRs Pendentes alterado com sucesso!";
                        Grade.DataSource = oLancamentosDados.PreencheDataTableMTRsPendentes("NomeFantasia");
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;MTRs Pendentes";
                LimpaCampos();
            }
        }
    }

    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de MTR Pendente";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        txtCNPJ_CPF.Text = "";
        txtSenhaAcesso.Text = "";
        lblNomeCliente.Text = "";
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "NumeroLancamento" && e.CommandArgument.ToString() != "NomeFantasia" && 
            e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "DataRetirada" &&
            e.CommandArgument.ToString() != "NumeroMTRFatima" && e.CommandArgument.ToString() != "DescricaoResiduo" &&
            e.CommandArgument.ToString() != "Motivo" && e.CommandArgument.ToString() != "CodigoResiduo" && 
            e.CommandArgument.ToString() != "NumeroMTR")
        {
            int _NuLanc = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text);
            intNumeroMTR.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[10].Text;
            intCodigoResiduo.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
            oLancamentos = oLancamentosDados.PegaDados(oLancamentos, _NuLanc);
            AtribuiDadosDaClasse(oLancamentos);
            PermissaoAlterar();
            if (oLancamentos.CodigoCliente.ToString() != "")
                hifCodigo.Value = oLancamentos.CodigoCliente.ToString();
            Grade.DataSource = oLancamentosDados.PreencheDataTableMTRsPendentes("NomeFantasia");
            Grade.DataBind();
            if (oLancamentos.CodigoCliente > 0)
            {
                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, oLancamentos.CodigoCliente);
                lblNomeCliente.Text = oCliente.Nome;
                txtCNPJ_CPF.Text = geral.RetiraLetras(geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF));
                txtSenhaAcesso.Text = oCliente.SenhaAcessoFatima;
                txtMotorista.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[12].Text;
            }
        }
    }
    protected void AtribuiDadosDaClasse(clsLancamentos pLancamentos)
    {
        intNrLancamento.Valor = pLancamentos.NumeroLancamento.ToString();
        intCodigoCliente.Valor = pLancamentos.CodigoCliente.ToString();
        datDataRetirada.Data = Convert.ToDateTime(pLancamentos.DataRetirada).ToString("dd/MM/yyyy");
    }
    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        intNrLancamento.Valor = "";
        intCodigoCliente.Valor = "";
        intNumeroMTR.Text = "";
        intCodigoResiduo.Valor = "";
        intNumeroMTRe.Text = "";
        txtCNPJ_CPF.Text = "";
        txtSenhaAcesso.Text = "";
        lblNomeCliente.Text = "";
        PermissaoAlterar();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;MTRs Pendentes";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[5].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[5].Text = "";
            if (e.Row.Cells[12].Text.Length > 0)
            {
                if (e.Row.Cells[12].Text.Split(" "[0]).Length > 0)
                    e.Row.Cells[12].Text = e.Row.Cells[12].Text.Split(" "[0])[0];
            }
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.Replace(" ", "");
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        if (hifCodigo.Value != "")
        {
            Grade.DataSource = oLancamentosDados.PreencheDataTableMTRsPendentes(geral.Ordem);
            Grade.DataBind();
        }
    }
}