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
    public partial class Municipios : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsMunicipios oMunicipios = new clsMunicipios();
        clsMunicipiosDados oMunicipiosDados = new clsMunicipiosDados();
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "12");
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

                        geral.Ordem = "Nome asc";
                        Grade.DataSource = oMunicipiosDados.PreencheDataTableOrdem(geral.Ordem);
                        Grade.DataBind();
                        txtNome.MaxLength = oMunicipiosDados.PegaTamanhoCampoVarChar("Nome");
                        txtUF.MaxLength = oMunicipiosDados.PegaTamanhoCampoVarChar("UF");
                        txtCodigoIBGE.MaxLength = oMunicipiosDados.PegaTamanhoCampoVarChar("CodigoIBGE");
                        PermissaoIncluir();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            txtNome.Focus();
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

            lblMensagem.Text = oMunicipiosDados.DadoExiste(oMunicipios.Codigo);
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "Nome do Municipios inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oMunicipiosDados.Excluir(oMunicipios.Codigo, "");
                lblMensagem.Text = "Municipios excluído com sucesso!";

                Grade.DataSource = oMunicipiosDados.PegaDados(oMunicipios, 0, false);
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
            oLog.LocalOperacao = "Cadastro de Municípios";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código Município: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Nome: " + txtNome.Text + " \n";
            oLog.Log = oLog.Log + "CodigoIBGE: " + txtCodigoIBGE.Text + " \n";
            oLog.Log = oLog.Log + "UF: " + txtUF.Text + " \n";
            oLog.Log = oLog.Log + "Aliquota ISS: " + moeAliquotaISS.Valor + " \n";
            oLog.Log = oLog.Log + "CodigoIPM: " + intCodigoIPM.Valor + " \n";
            oLogDados.Inserir(oLog);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtNome.Text.Equals(""))
                {
                    lblMensagem.Text = "Nome do Municipios inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oMunicipios = AtribuiDadosDoForm(oMunicipios);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oMunicipiosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oMunicipiosDados.Inserir(oMunicipios);
                        Grade.DataSource = oMunicipiosDados.PegaDados(oMunicipios, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Municipios incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oMunicipiosDados.Alterar(oMunicipios, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Municipios alterada com sucesso!";
                            Grade.DataSource = oMunicipiosDados.PegaDados(oMunicipios, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Municípios";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtNome.Text.Equals(""))
                {
                    lblMensagem.Text = "Nome do Município inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oMunicipiosDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                    lblMensagem.Text = "Municípios excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Municípios";
                    Grade.DataSource = oMunicipiosDados.PegaDados(oMunicipios, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Município";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Municípios";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "CodigoIBGE" &&
                e.CommandArgument.ToString() != "AliquotaISS" && e.CommandArgument.ToString() != "UF" &&
                e.CommandArgument.ToString() != "CodigoIPM" && e.CommandArgument.ToString() != "Nome")
            {
                if (Convert.ToInt32(e.CommandArgument) < 6)
                {
                    txtNome.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                    hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                    oMunicipios = oMunicipiosDados.PegaDados(oMunicipios, Convert.ToInt32(hifCodigo.Value));
                    AtribuiDadosDaClasse(oMunicipios);
                    if (Salvar.Text != "Confirma")
                        PermissaoAlterar();
                }
            }
        }

        protected void AtribuiDadosDaClasse(clsMunicipios pMunicipios)
        {
            txtNome.Text = pMunicipios.Nome;
            txtCodigoIBGE.Text = pMunicipios.CodigoIBGE;
            txtUF.Text = pMunicipios.UF;
            moeAliquotaISS.Valor = pMunicipios.AliquotaISS.ToString();
            intCodigoIPM.Valor = pMunicipios.CodigoIPM.ToString();
        }

        protected void LimpaCampos()
        {
            txtNome.Text = "";
            txtCodigoIBGE.Text = "";
            txtUF.Text = "";
            moeAliquotaISS.Valor = "";
            intCodigoIPM.Valor = "";
            PermissaoIncluir();
        }
        protected clsMunicipios AtribuiDadosDoForm(clsMunicipios pMunicipios)
        {
            pMunicipios.Nome = txtNome.Text;
            pMunicipios.CodigoIBGE = txtCodigoIBGE.Text;
            pMunicipios.UF = txtUF.Text;
            if (moeAliquotaISS.Valor != "")
                pMunicipios.AliquotaISS = Convert.ToDecimal(moeAliquotaISS.Valor);
            if (intCodigoIPM.Valor != "")
                pMunicipios.CodigoIPM = Convert.ToInt32(intCodigoIPM.Valor);
            return pMunicipios;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro de Municipios";
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

            Grade.DataSource = oMunicipiosDados.PreencheDataTableOrdem(geral.Ordem);
            Grade.DataBind();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Grade.DataSource = oMunicipiosDados.PreencheDataTableFiltro(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
            Grade.DataBind();
        }
        protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grade.DataSource = oMunicipiosDados.PreencheDataTableOrdem(geral.Ordem);
            Grade.PageIndex = e.NewPageIndex;
            Grade.DataBind();
        }
    }
}