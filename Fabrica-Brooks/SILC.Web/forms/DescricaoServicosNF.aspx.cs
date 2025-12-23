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
    public partial class DescricaoServicosNF : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsDescricaoServicos oDescricaoServicos = new clsDescricaoServicos();
        clsDescricaoServicosDados oDescricaoServicosDados = new clsDescricaoServicosDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "57");
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
                            menu.Visible = false;
                        else
                            menu.Visible = true;
                        Grade.DataSource = oDescricaoServicosDados.PegaDados(oDescricaoServicos, 0, false);
                        Grade.DataBind();
                        PermissaoIncluir();
                        txtDescricao.MaxLength = oDescricaoServicosDados.PegaTamanhoCampoVarChar("Descricao");
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            intCodigo.Focus();
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
            lblMensagem.Text = oDescricaoServicosDados.DadoExiste(oDescricaoServicos.Sequencial);
            if (intCodigo.Valor.Equals(""))
            {
                lblMensagem.Text = "Descrição Serviço para NF inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oDescricaoServicosDados.Excluir(oDescricaoServicos.Sequencial);
                lblMensagem.Text = "Descrição Serviço para NF excluído com sucesso!";

                Grade.DataSource = oDescricaoServicosDados.PegaDados(oDescricaoServicos, 0, false);
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
            oLog.LocalOperacao = "Descrição Serviços NF";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Sequencial Serviço NF: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Código Serviço NF: " + intCodigo.Valor + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLog.Log = oLog.Log + ": " + "" + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (intCodigo.Valor.Equals(""))
                {
                    lblMensagem.Text = "Descrição Serviço para NF inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oDescricaoServicos = AtribuiDadosDoForm(oDescricaoServicos);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oDescricaoServicosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oDescricaoServicosDados.Inserir(oDescricaoServicos);
                        Grade.DataSource = oDescricaoServicosDados.PegaDados(oDescricaoServicos, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Descrição Serviço para NF incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oDescricaoServicosDados.Alterar(oDescricaoServicos, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Descrição Serviço para NF alterado com sucesso!";
                            Grade.DataSource = oDescricaoServicosDados.PegaDados(oDescricaoServicos, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Descrição Serviço para NF";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (intCodigo.Valor.Equals(""))
                {
                    lblMensagem.Text = "Descrição Serviço para NF inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oDescricaoServicosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Descrição Serviço para NF excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Descrição Serviço para NF";
                    Grade.DataSource = oDescricaoServicosDados.PegaDados(oDescricaoServicos, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Descrição Serviço para NF";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Descrição Serviço para NF";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "CodigoServicoPrefeitura" &&
                e.CommandArgument.ToString() != "DescricaoServico")
            {
                intCodigo.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oDescricaoServicos = oDescricaoServicosDados.PegaDados(oDescricaoServicos, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oDescricaoServicos);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsDescricaoServicos pDescricaoServicos)
        {
            intCodigo.Valor = pDescricaoServicos.CodigoServicoPrefeitura.ToString();
            txtDescricao.Text = pDescricaoServicos.DescricaoServico;
        }

        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            intCodigo.Valor = "";
            txtDescricao.Text = "";
            PermissaoIncluir();
        }
        protected clsDescricaoServicos AtribuiDadosDoForm(clsDescricaoServicos pDescricaoServicos)
        {
            pDescricaoServicos.CodigoServicoPrefeitura = 0;
            pDescricaoServicos.DescricaoServico = "";
            if (intCodigo.Valor != "")
                pDescricaoServicos.CodigoServicoPrefeitura = Convert.ToInt16(intCodigo.Valor);
            pDescricaoServicos.DescricaoServico = txtDescricao.Text;
            return pDescricaoServicos;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Descrição Serviço para NF";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Ok";
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
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            Grade.DataSource = oDescricaoServicosDados.PreencheDataTable(geral.Ordem);
            Grade.DataBind();
        }
    }
}