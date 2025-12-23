using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class IBAMA : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsIBAMA oIBAMA = new clsIBAMA();
        clsIBAMADados oIBAMADados = new clsIBAMADados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected bool bEvitarEventos = false;

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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "10");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
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

                        geral.Ordem = "Codigo asc";
                        Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem);
                        Grade.DataBind();
                        txtDescricao.MaxLength = oIBAMADados.PegaTamanhoCampoVarChar("Descricao");

                        PermissaoIncluir();
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
            /*
            lblMensagem.Text = oIBAMADados.DadoExiste(oIBAMA.Codigo);
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "IBAMA inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oIBAMADados.Excluir(oIBAMA.Codigo, "");
                lblMensagem.Text = "IBAMA excluído com sucesso!";

                Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem); 
                Grade.DataBind();
            }
            */
        }
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Cadastro do IBAMA";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código IBAMA: " + hifCodigo.Value + " \n";
            if (chkAtivo.Checked)
                oLog.Log = oLog.Log + "Ativo: Sim \n";
            else if (!chkAtivo.Checked)
                oLog.Log = oLog.Log + "Ativo: Não \n";
            oLog.Log = oLog.Log + "Codigo IBAMA: " + txtCodigoIBAMA.Text + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "IBAMA inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oIBAMA = AtribuiDadosDoForm(oIBAMA);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oIBAMADados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        oIBAMADados.Inserir(oIBAMA);
                        SalvarLog("Inclusão");
                        Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem); //oIBAMADados.PegaDados(oIBAMA, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "IBAMA incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        string msgErr = oIBAMADados.Alterar(oIBAMA, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "IBAMA alterada com sucesso!";
                            SalvarLog("Alteração");
                            Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem); //oIBAMADados.PegaDados(oIBAMA, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de IBAMA";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "IBAMA inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    oIBAMADados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                    lblMensagem.Text = "IBAMA excluído com sucesso!";
                    SalvarLog("Exclusão");
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de IBAMA";
                    Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem); //oIBAMADados.PegaDados(oIBAMA, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de IBAMA";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de IBAMA";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Ativo" && e.CommandArgument.ToString() != "Descricao" &&
                e.CommandArgument.ToString() != "CodigoIBAMA")
            {
                txtDescricao.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oIBAMA = oIBAMADados.PegaDados(oIBAMA, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oIBAMA);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsIBAMA pIBAMA)
        {
            if (pIBAMA.Ativo == 1)
                chkAtivo.Checked = true;
            else
                chkAtivo.Checked = false;
            txtDescricao.Text = pIBAMA.Descricao;
            txtCodigoIBAMA.Text = pIBAMA.CodigoIBAMA.ToString();
        }

        protected void LimpaCampos()
        {
            chkAtivo.Checked = false;
            txtDescricao.Text = "";
            txtCodigoIBAMA.Text = "";
            PermissaoIncluir();
        }
        protected clsIBAMA AtribuiDadosDoForm(clsIBAMA pIBAMA)
        {
            if (chkAtivo.Checked)
                pIBAMA.Ativo = 1;
            else if (!chkAtivo.Checked)
                pIBAMA.Ativo = 0;
            if (txtCodigoIBAMA.Text == "")
                pIBAMA.CodigoIBAMA = "";
            else
                pIBAMA.CodigoIBAMA = txtCodigoIBAMA.Text;

            pIBAMA.Descricao = txtDescricao.Text;
            return pIBAMA;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnVisualizacaoSimples.Text = "Visualização normal";
            btnVisualizacaoSimples_Click(new object(), EventArgs.Empty);
            lblTitulo.Text = "&nbsp;Cadastro de IBAMA";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Ok";
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0 && !bEvitarEventos)
            {
                ImageButton _ibnExcluir = new ImageButton();
                _ibnExcluir.Enabled = true;
                if (oItensMenuPermissoes.Excluir == 0)
                {
                    _ibnExcluir = (ImageButton)e.Row.Cells[1].FindControl("ibnExcluir");
                    _ibnExcluir.Enabled = false;
                }

                if (e.Row.Cells[5].Text.Replace(" ", "") == "1")
                    e.Row.Cells[5].Text = "Sim";
                else
                    e.Row.Cells[5].Text = "Não";
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem);
            Grade.DataBind();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Grade.DataSource = oIBAMADados.PreencheDataTableFiltro(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
            Grade.DataBind();
        }

        protected void btnVisualizacaoSimples_Click(object sender, EventArgs e)
        {
            if (btnVisualizacaoSimples.Text == "Visualização normal")
                bEvitarEventos = false;
            else
                bEvitarEventos = true;
            if (bEvitarEventos)
            {
                btnVisualizacaoSimples.Text = "Visualização normal";
                Grade.Columns[0].Visible = false;
                Grade.Columns[1].Visible = false;
                Grade.Columns[2].Visible = false;
                Grade.Columns[5].Visible = false;
                ddlFiltro.Visible = false;
                txtFiltro.Visible = false;
                btnOk.Visible = false;
                chkAtivo.Visible = false;
                Label1.Visible = false;
                Label3.Visible = false;
                txtDescricao.Visible = false;
                Label2.Visible = false;
                txtCodigoIBAMA.Visible = false;
                Salvar.Visible = false;
            }
            else
            {
                btnVisualizacaoSimples.Text = "Visualização simples";
                Grade.Columns[0].Visible = true;
                Grade.Columns[1].Visible = true;
                Grade.Columns[2].Visible = true;
                Grade.Columns[5].Visible = true;
                ddlFiltro.Visible = true;
                txtFiltro.Visible = true;
                btnOk.Visible = true;
                chkAtivo.Visible = true;
                Label3.Visible = true;
                Label1.Visible = true;
                txtDescricao.Visible = true;
                Label2.Visible = true;
                txtCodigoIBAMA.Visible = true;
                Salvar.Visible = true;
            }
            Grade.DataSource = oIBAMADados.PreencheDataTableOrdem(geral.Ordem);
            Grade.DataBind();
        }
    }
}