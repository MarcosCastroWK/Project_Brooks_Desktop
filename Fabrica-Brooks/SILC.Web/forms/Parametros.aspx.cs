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
    public partial class Parametros : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsParametros oParametros = new clsParametros();
        clsParametrosDados oParametrosDados = new clsParametrosDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "55");
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

                        if (geral.BancoUsado == 4)
                        {
                            geral.CodigoEmpresa = 6;
                            oUsuario.CodigoEmpresa = 6;
                        }
                        Grade.DataSource = oParametrosDados.PegaDados(oParametros, geral.CodigoEmpresa, false);
                        Grade.DataBind();
                        PermissaoIncluir();
                        txtNome.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Nome");
                        txtEndereco.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Endereco");
                        txtEmail.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Bairro");
                        txtCEP.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("CEP");
                        txtCidade.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Telefones");
                        txtFone.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Fone");
                        txtUF.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("UF");
                        txtCNPJ_CPF.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("CNPJ_CPF");
                        txtIE_RG.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("IE_RG");
                        txtSite.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Site");
                        txtEmail.MaxLength = oParametrosDados.PegaTamanhoCampoVarChar("Email");
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
            lblMensagem.Text = oParametrosDados.DadoExiste(oParametros.Numero);
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "Parâmetro inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oParametrosDados.Excluir(oParametros.Numero, "");
                lblMensagem.Text = "Parâmetro excluído com sucesso!";

                Grade.DataSource = oParametrosDados.PegaDados(oParametros, geral.CodigoEmpresa, false);
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
            oLog.LocalOperacao = "Cadastro Empresa";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Nome/Razão Social: " + txtNome.Text + " \n";
            oLog.Log = oLog.Log + "Endereço: " + txtEndereco.Text + " \n";
            oLog.Log = oLog.Log + "Cidade: " + txtCidade.Text + " \n";
            oLog.Log = oLog.Log + "UF: " + txtUF.Text + " \n";
            oLog.Log = oLog.Log + "CEP: " + txtCEP.Text + " \n";
            oLog.Log = oLog.Log + "CNPJ: " + txtCNPJ_CPF.Text + " \n";
            oLog.Log = oLog.Log + "Inscrição Estadual: " + txtIE_RG.Text + " \n";
            oLog.Log = oLog.Log + "Telefone: " + txtFone.Text + " \n";
            oLog.Log = oLog.Log + "e-mail: " + txtEmail.Text + " \n";
            oLog.Log = oLog.Log + "Site: " + txtSite.Text + " \n";
            oLog.Log = oLog.Log + "Código município: " + txtCodigoMunicipio.Text + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtNome.Text.Equals(""))
                {
                    lblMensagem.Text = "Parâmetro inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oParametros = AtribuiDadosDoForm(oParametros);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oParametrosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oParametrosDados.Inserir(oParametros);
                        Grade.DataSource = oParametrosDados.PegaDados(oParametros, geral.CodigoEmpresa, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Parâmetro incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oParametrosDados.Alterar(oParametros, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Parâmetro alterado com sucesso!";
                            Grade.DataSource = oParametrosDados.PegaDados(oParametros, geral.CodigoEmpresa, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Parâmetros";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtNome.Text.Equals(""))
                {
                    lblMensagem.Text = "Parâmetro inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oParametrosDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                    lblMensagem.Text = "Parâmetro excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Parâmetros";
                    Grade.DataSource = oParametrosDados.PegaDados(oParametros, geral.CodigoEmpresa, false);
                    Grade.DataBind();
                }
            }
            LimpaCampos();
        }
        protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = "&nbsp;Exclusão de Parâmetro";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Parâmetro";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Numero" && e.CommandArgument.ToString() != "Nome")
            {
                txtNome.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oParametros = oParametrosDados.PegaDados(oParametros, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oParametros);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();

            }
        }

        protected void AtribuiDadosDaClasse(clsParametros pParametro)
        {
            txtNome.Text = pParametro.Nome;
            txtCidade.Text = pParametro.Cidade;
            txtEndereco.Text = pParametro.Endereco;
            txtCEP.Text = pParametro.CEP;
            txtFone.Text = pParametro.Telefones;
            txtUF.Text = pParametro.UF;
            txtEmail.Text = pParametro.Email;
            txtIE_RG.Text = pParametro.IE_RG;
            txtCNPJ_CPF.Text = pParametro.CNPJ_CPF;
            txtEmail.Text = pParametro.Email;
            txtSite.Text = pParametro.Site;
            txtCodigoMunicipio.Text = pParametro.CodigoMunicipio;
        }

        protected void LimpaCampos()
        {
            txtNome.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtCEP.Text = "";
            txtFone.Text = "";
            txtUF.Text = "";
            txtEmail.Text = "";
            txtIE_RG.Text = "";
            txtCNPJ_CPF.Text = "";
            txtEmail.Text = "";
            txtSite.Text = "";
            txtCodigoMunicipio.Text = "";
            PermissaoIncluir();
        }
        protected clsParametros AtribuiDadosDoForm(clsParametros pParametro)
        {
            pParametro.Nome = txtNome.Text;
            pParametro.Cidade = txtCidade.Text;
            pParametro.Endereco = txtEndereco.Text;
            pParametro.CEP = txtCEP.Text;
            pParametro.Telefones = txtFone.Text;
            pParametro.UF = txtUF.Text;
            pParametro.Email = txtEmail.Text;
            pParametro.IE_RG = txtIE_RG.Text;
            pParametro.CNPJ_CPF = txtCNPJ_CPF.Text;
            pParametro.Email = txtEmail.Text;
            pParametro.Site = txtSite.Text;
            pParametro.CodigoMunicipio = txtCodigoMunicipio.Text;
            return pParametro;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro de Parametros";
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

            Grade.DataSource = oParametrosDados.PreencheDataTableParametros(geral.Ordem, geral.CodigoEmpresa);
            Grade.DataBind();
        }
    }
}