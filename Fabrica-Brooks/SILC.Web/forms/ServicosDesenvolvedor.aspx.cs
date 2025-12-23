using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class ServicosDesenvolvedor : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsServicosDesenvolvedor oServicosDesenvolvedor = new clsServicosDesenvolvedor();
        clsServicosDesenvolvedorDados oServicosDesenvolvedorDados = new clsServicosDesenvolvedorDados();
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "63");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
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

                        txtSolicitante.Text = oUsuario.Nome;
                        if (oUsuario.Nome.ToLower() != "teixeira")
                        {
                            chkFeito.Enabled = false;
                            chkExecutando.Enabled = false;
                            txtSolicitante.Enabled = false;
                        }
                        else
                        {
                            chkFeito.Enabled = true;
                            chkExecutando.Enabled = true;
                            txtSolicitante.Enabled = true;
                        }
                        txtData.Data = DateTime.Now.ToString("dd/MM/yyyy");
                        Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT("Feito asc, Prioridade asc, Codigo desc, Data asc");
                        Grade.DataBind();
                        PermissaoIncluir();

                        txtDescricao.MaxLength = oServicosDesenvolvedorDados.PegaTamanhoCampoVarChar("Descricao");
                        txtDetalhamento.MaxLength = oServicosDesenvolvedorDados.PegaTamanhoCampoVarChar("Detalhamento");
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            txtDescricao.Focus();
        }
        private void PermissaoIncluir()
        {
            Salvar.Enabled = true;
            if (oItensMenuPermissoes.Incluir == 0)
                Salvar.Enabled = false;
        }
        private void PermissaoAlterar()
        {
            Salvar.Enabled = true;
            if (oItensMenuPermissoes.Alterar == 0)
                Salvar.Enabled = false;
        }
        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oServicosDesenvolvedorDados.DadoExiste(oServicosDesenvolvedor.Codigo);
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "Serviços desenvolvedor inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                //oServicosDesenvolvedorDados.Excluir(oServicosDesenvolvedor.Codigo);
                //lblMensagem.Text = "Serviços desenvolvedor excluído com sucesso!";

                Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT("Feito asc, Prioridade asc, Codigo desc, Data asc");
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
            oLog.LocalOperacao = "Serviços desenvolvedor";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Serviço desenvolvedor: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLog.Log = oLog.Log + "Detalhamento: " + txtDetalhamento.Text + " \n";
            oLog.Log = oLog.Log + "Data: " + txtData.Data + " \n";
            oLog.Log = oLog.Log + "Prioridade: " + intPrioridade.Valor + " \n";
            if (chkFeito.Checked)
                oLog.Log = oLog.Log + "Feito: Sim \n";
            else
                oLog.Log = oLog.Log + "Feito: Não \n";
            oLog.Log = oLog.Log + "Solicitante: " + txtSolicitante.Text + " \n";
            if (chkFeito.Checked)
                oLog.Log = oLog.Log + "Executando: Ok \n";
            else
                oLog.Log = oLog.Log + "Executando: \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "Serviços desenvolvedor inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oServicosDesenvolvedor = AtribuiDadosDoForm(oServicosDesenvolvedor);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oServicosDesenvolvedorDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oServicosDesenvolvedorDados.Inserir(oServicosDesenvolvedor);
                        if (oUsuario.Nome.ToLower() != "teixeira")
                            EnviaeMail();
                        Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT("Feito asc, Prioridade asc, Codigo desc, Data asc");
                        Grade.DataBind();
                        lblMensagem.Text = "Serviços desenvolvedor incluído com sucesso!";

                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oServicosDesenvolvedorDados.Alterar(oServicosDesenvolvedor, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Serviços desenvolvedor alterado com sucesso!";
                            if (chkFeito.Checked)
                            {
                                EnviaeMailParaSolicitante();
                            }
                            //Grade.DataSource = oServicosDesenvolvedorDados.PegaDados(oServicosDesenvolvedor, 0, false);
                            Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT("Feito asc, Prioridade asc, Codigo desc, Data asc");
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Serviços desenvolvedor";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "Serviços desenvolvedor inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oServicosDesenvolvedorDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Serviços desenvolvedor excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Serviços desenvolvedor";
                    Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT("Feito asc, Prioridade asc, Codigo desc, Data asc");
                    Grade.DataBind();
                }
            }
            lblConfirmacao.Text = "Confirma inclusão";
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
            lblTitulo.Text = "&nbsp;Exclusão de Serviços desenvolvedor";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            lblConfirmacao.Text = "Exclusão";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Serviços desenvolvedor";
            lblMensagem.Text = "";
            lblConfirmacao.Text = "Confirma alteração";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Data" &&
                e.CommandArgument.ToString() != "Detalhamento" && e.CommandArgument.ToString() != "Servico" &&
                e.CommandArgument.ToString() != "Prioridade" && e.CommandArgument.ToString() != "Feito" &&
                e.CommandArgument.ToString() != "Solicitante" && e.CommandArgument.ToString() != "Executando")
            {
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oServicosDesenvolvedor = oServicosDesenvolvedorDados.PegaDados(oServicosDesenvolvedor, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oServicosDesenvolvedor);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsServicosDesenvolvedor pServicosDesenvolvedor)
        {
            txtDescricao.Text = pServicosDesenvolvedor.Servico;
            txtDetalhamento.Text = pServicosDesenvolvedor.Detalhamento;
            txtData.Data = Convert.ToDateTime(pServicosDesenvolvedor.Data).ToString("dd/MM/yyyy");
            intPrioridade.Valor = pServicosDesenvolvedor.Prioridade.ToString();
            chkFeito.Checked = false;
            if (pServicosDesenvolvedor.Feito == 1)
                chkFeito.Checked = true;
            txtSolicitante.Text = pServicosDesenvolvedor.Solicitante;
            chkExecutando.Checked = false;
            if (pServicosDesenvolvedor.Executando == 1)
                chkExecutando.Checked = true;
        }

        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            txtDescricao.Text = "";
            txtDetalhamento.Text = "";
            txtData.Data = DateTime.Now.ToString("dd/MM/yyyy");
            intPrioridade.Valor = "";
            chkFeito.Checked = false;
            txtSolicitante.Text = oUsuario.Nome;
            chkExecutando.Checked = false;
            PermissaoIncluir();
        }
        protected clsServicosDesenvolvedor AtribuiDadosDoForm(clsServicosDesenvolvedor pServicosDesenvolvedor)
        {
            pServicosDesenvolvedor.Codigo = 0;
            pServicosDesenvolvedor.Servico = "";
            pServicosDesenvolvedor.Detalhamento = "";
            pServicosDesenvolvedor.Prioridade = 0;
            pServicosDesenvolvedor.Feito = 0;
            pServicosDesenvolvedor.Solicitante = "";
            pServicosDesenvolvedor.Executando = 0;

            pServicosDesenvolvedor.Servico = txtDescricao.Text;
            pServicosDesenvolvedor.Detalhamento = txtDetalhamento.Text;
            if (intPrioridade.Valor != "")
                pServicosDesenvolvedor.Prioridade = Convert.ToInt16(intPrioridade.Valor);
            if (chkFeito.Checked)
                pServicosDesenvolvedor.Feito = 1;
            pServicosDesenvolvedor.Data = txtData.Data;
            pServicosDesenvolvedor.Solicitante = txtSolicitante.Text;
            if (chkExecutando.Checked)
                pServicosDesenvolvedor.Executando = 1;
            return pServicosDesenvolvedor;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Serviços desenvolvedor";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Ok";
            lblConfirmacao.Text = "Confirma inclusão";
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

                if (e.Row.Cells[5].Text.Replace(" ", "") == "1")
                    e.Row.Cells[5].Text = "Ok";
                else
                    e.Row.Cells[5].Text = "";

                if (e.Row.Cells[6].Text.Trim() == "01/01/0001")
                    e.Row.Cells[6].Text = "";

                if (e.Row.Cells[8].Text.Replace(" ", "") == "1")
                    e.Row.Cells[8].Text = "Sim";
                else
                    e.Row.Cells[8].Text = "Não";
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
            {
                if (geral.Ordem == "Prioridade desc")
                    geral.Ordem = "Prioridade asc, Data asc";
                else if (geral.Ordem == "Feito desc")
                    geral.Ordem = "Feito asc, Prioridade asc, Codigo desc, Data asc";
                else
                    geral.Ordem = e.SortExpression + " asc";

            }
            Grade.DataSource = oServicosDesenvolvedorDados.PreencheDT(geral.Ordem);
            Grade.DataBind();
        }
        private void EnviaeMail()
        {
            string eMailQuemEnvia = "logistica@brooksambiental.com.br";
            string eMailQueLoga = "logistica@brooksambiental.com.br";
            string SenhaQueLoga = "logistic@21";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(eMailQueLoga, SenhaQueLoga);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.Sender = new System.Net.Mail.MailAddress(eMailQueLoga, "BROOKS Serviços");
            mail.From = new System.Net.Mail.MailAddress(eMailQuemEnvia, "BROOKS Serviços");

            mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com"));

            mail.Subject = "Serviços desenvolvedor " + DateTime.Now.ToShortDateString();
            mail.Body = txtDescricao.Text + " \n" + txtDetalhamento.Text + " \n\n Solicitado por: " + oUsuario.Nome;

            //System.Net.Mail.Attachment _arquivoanexo = new System.Net.Mail.Attachment(pArquivoAnexado);
            //mail.Attachments.Add(_arquivoanexo);

            mail.IsBodyHtml = false;
            client.Send(mail);
            mail = null;
        }
        private void EnviaeMailParaSolicitante()
        {
            string eMailQuemEnvia = "logistica@brooksambiental.com.br";
            string eMailQueLoga = "logistica@brooksambiental.com.br";
            string SenhaQueLoga = "logistic@21";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(eMailQueLoga, SenhaQueLoga);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.Sender = new System.Net.Mail.MailAddress(eMailQueLoga, "Serviços Desenvolvedor");
            mail.From = new System.Net.Mail.MailAddress(eMailQuemEnvia, "Serviços Desenvolvedor");

            if (txtSolicitante.Text.ToLower().IndexOf("cris") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("cris@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("andretoro") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("comercial@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("miriam") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("logistica1@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("geferson") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("logistica@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("teixeira") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("sergio") > -1 || txtSolicitante.Text.ToLower().IndexOf("sérgio") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("sergio@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("willian") > -1)
            {
                mail.To.Add(new System.Net.Mail.MailAddress("financeiro@brooksambiental.com.br"));
            }
            else if (txtSolicitante.Text.ToLower().IndexOf("andr") > -1) // andreia ou andrea
            {
                mail.To.Add(new System.Net.Mail.MailAddress("comercial@brooksambiental.com.br"));
            }
            mail.Subject = "Serviço Feito " + DateTime.Now.ToShortDateString();
            mail.Body = txtDescricao.Text + " \n" + txtDetalhamento.Text;

            mail.IsBodyHtml = false;

            client.Send(mail);
            mail = null;
        }
    }
}