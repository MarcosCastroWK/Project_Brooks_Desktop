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
    public partial class forms_containeres : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsCacambas oContaineres = new clsCacambas();
        clsCacambaDados oContaineresDados = new clsCacambaDados();
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "4");

            if (!IsPostBack)
            {
                datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                    if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                        Response.Redirect("sempermissao.aspx");

                    try
                    {
                        if (oUsuario.Aplicativo == true)
                            menu1.Visible = false;
                        else
                            menu1.Visible = true;
                        Grade.DataSource = oContaineresDados.PegaDados(oContaineres, 0, false);
                        Grade.DataBind();
                        txtCor.MaxLength = oContaineresDados.PegaTamanhoCampoVarChar("Cor");
                        txtTipo.MaxLength = oContaineresDados.PegaTamanhoCampoVarChar("Tipo");
                        txtNumero.MaxLength = oContaineresDados.PegaTamanhoCampoVarChar("Numero");
                        PermissaoIncluir();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            txtNumero.Focus();
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
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Cadastro Containeres";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código container: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Data do cadastro: " + datDataCadastro.Data + " \n";
            if (chkInativo.Checked)
                oLog.Log = oLog.Log + "Inativo: Sim \n";
            else
                oLog.Log = oLog.Log + "Inativo: Não \n";
            if (chkLocalArmazenamento.Checked)
                oLog.Log = oLog.Log + "É Local Armazenamento: Sim \n";
            else
                oLog.Log = oLog.Log + "Local Armazenamento: Não \n";
            if (chkTerceiro.Checked)
                oLog.Log = oLog.Log + "É de Terceiro: Sim \n";
            else
                oLog.Log = oLog.Log + "É de Terceiro: Não \n";
            oLog.Log = oLog.Log + "Número/Identificação: " + txtNumero.Text + " \n";
            oLog.Log = oLog.Log + "Tipo: " + txtTipo.Text + " \n";
            oLog.Log = oLog.Log + "Capacidade m3: " + valCapacidade.Valor + " \n";
            oLog.Log = oLog.Log + "Cor: " + txtCor.Text + " \n";
            oLogDados.Inserir(oLog);

        }
        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oContaineresDados.DadoExiste(oContaineres.Codigo);
            if (txtTipo.Text.Equals(""))
            {
                lblMensagem.Text = "Tipo do Container inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oContaineresDados.Excluir(oContaineres.Codigo, "");
                lblMensagem.Text = "Container excluído com sucesso!";

                Grade.DataSource = oContaineresDados.PegaDados(oContaineres, 0, false);
                Grade.DataBind();
            }
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtTipo.Text.Equals(""))
                {
                    lblMensagem.Text = "Tipo do Container inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oContaineres = AtribuiDadosDoForm(oContaineres);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oContaineresDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oContaineresDados.Inserir(oContaineres);

                        Grade.DataSource = oContaineresDados.PreencheDataTableCacambas("Codigo desc");
                        Grade.DataBind();
                        lblMensagem.Text = "Container incluído com sucesso!";
                        LimpaCampos();
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        string msgErr = oContaineresDados.Alterar(oContaineres, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            SalvarLog("Alteração");
                            lblMensagem.Text = "Container alterado com sucesso!";
                            if (hifCodigo.Value != "")
                                Grade.DataSource = oContaineresDados.PegaDados(oContaineres, Convert.ToInt32(hifCodigo.Value), false);
                            else
                                Grade.DataSource = oContaineresDados.PegaDados(oContaineres, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Containeres";

                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtTipo.Text.Equals(""))
                {
                    lblMensagem.Text = "Tipo do Container inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oContaineresDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                    lblMensagem.Text = "Container excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Containeres";
                    Grade.DataSource = oContaineresDados.PegaDados(oContaineres, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Container";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Container";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Inativo" && e.CommandArgument.ToString() != "DataCadastro" && e.CommandArgument.ToString() != "Tipo" && e.CommandArgument.ToString() != "Cor" && e.CommandArgument.ToString() != "Capacidade" &&
                e.CommandArgument.ToString() != "EhLocal" && e.CommandArgument.ToString() != "EhTerceiro" && e.CommandArgument.ToString() != "Numero")
            {
                txtTipo.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oContaineres = oContaineresDados.PegaDados(oContaineres, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oContaineres);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsCacambas pContainer)
        {
            if (pContainer.Inativo == 1)
                chkInativo.Checked = true;
            else
                chkInativo.Checked = false;

            if (pContainer.DataCadastro == "01/01/0001" || pContainer.DataCadastro == "01/01/0100" || pContainer.DataCadastro == null)
                datDataCadastro.Data = ""; //Convert.ToDateTime("01/00/0001").ToString("dd/MM/yyyy");
            else
                datDataCadastro.Data = Convert.ToDateTime(pContainer.DataCadastro).ToString("dd/MM/yyyy");
            txtTipo.Text = pContainer.Tipo;
            txtCor.Text = pContainer.Cor;
            valCapacidade.Valor = pContainer.Capacidade.ToString();
            txtNumero.Text = pContainer.Numero;
            if (pContainer.EhLocal == 1)
                chkLocalArmazenamento.Checked = true;
            else
                chkLocalArmazenamento.Checked = false;
            if (pContainer.EhTerceiro == 1)
                chkTerceiro.Checked = true;
            else
                chkTerceiro.Checked = false;
        }

        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
            chkInativo.Checked = false;
            txtTipo.Text = "";
            txtCor.Text = "";
            txtNumero.Text = "";
            valCapacidade.Valor = "";
            chkLocalArmazenamento.Checked = false;
            chkTerceiro.Checked = false;
            PermissaoIncluir();
        }
        protected clsCacambas AtribuiDadosDoForm(clsCacambas pContainer)
        {
            if (datDataCadastro.Data != "")
                pContainer.DataCadastro = datDataCadastro.Data;
            if (chkInativo.Checked)
                pContainer.Inativo = 1;
            else if (!chkInativo.Checked)
                pContainer.Inativo = 0;
            pContainer.Cor = txtCor.Text;
            pContainer.Numero = txtNumero.Text;
            if (valCapacidade.Valor == "")
                pContainer.Capacidade = 0;
            else
                pContainer.Capacidade = Convert.ToDecimal(valCapacidade.Valor);
            pContainer.Tipo = txtTipo.Text;
            if (chkLocalArmazenamento.Checked)
                pContainer.EhLocal = 1;
            else
                pContainer.EhLocal = 0;
            if (chkTerceiro.Checked)
                pContainer.EhTerceiro = 1;
            else
                pContainer.EhTerceiro = 0;
            return pContainer;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro de Containeres";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            chkLocalArmazenamento.Checked = true;
            chkTerceiro.Checked = false;
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
                if (e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[4].Text = "";

                if (e.Row.Cells[5].Text.Replace(" ", "") == "1")
                    e.Row.Cells[5].Text = "Sim";
                else
                    e.Row.Cells[5].Text = "Não";

                if (e.Row.Cells[9].Text.Replace(" ", "") == "1")
                    e.Row.Cells[9].Text = "Sim";
                else
                    e.Row.Cells[9].Text = "Não";

                if (e.Row.Cells[10].Text.Replace(" ", "") == "1")
                    e.Row.Cells[10].Text = "Sim";
                else
                    e.Row.Cells[10].Text = "Não";
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            Grade.DataSource = oContaineresDados.PreencheDataTableCacambas(geral.Ordem);
            Grade.DataBind();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Grade.DataSource = oContaineresDados.PreencheDataTableCacambas(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
            Grade.DataBind();
        }
        protected void datDataCadastro_TextChanged(object sender, EventArgs e)
        {
            //datDataCadastro.DataMode = TextBoxMode.Date;
        }
        protected void datDataCadastro_DataBinding(object sender, EventArgs e)
        {

        }
        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            datDataCadastro.Focus();
        }
        protected void lblErro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}