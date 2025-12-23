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
    public partial class forms_RetencaoImpostosNF : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsAliquotaImpostos oAliquotaImpostos = new clsAliquotaImpostos();
        clsAliquotaImpostosDados oAliquotaImpostosDados = new clsAliquotaImpostosDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "60");
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
                        Grade.DataSource = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, 0, false);
                        Grade.DataBind();
                        PermissaoIncluir();
                        txtCodigoBROOKS.MaxLength = oAliquotaImpostosDados.PegaTamanhoCampoVarChar("CodigoBROOKS");
                        txtDescricao.MaxLength = oAliquotaImpostosDados.PegaTamanhoCampoVarChar("Descricao");
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            intSituacaoTributaria.Focus();
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
            lblMensagem.Text = oAliquotaImpostosDados.DadoExiste(oAliquotaImpostos.Sequencial);
            if (txtCodigoBROOKS.Text.Equals(""))
            {
                lblMensagem.Text = "Retenção Impostos NF inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oAliquotaImpostosDados.Excluir(oAliquotaImpostos.Sequencial);
                lblMensagem.Text = "Retenção de Imposto da NF excluída com sucesso!";

                Grade.DataSource = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, 0, false);
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
            oLog.LocalOperacao = "Retenções Impostos NF";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código retençao: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Situação tributária: " + intSituacaoTributaria.Valor + " \n";
            oLog.Log = oLog.Log + "Código BROOKS: " + txtCodigoBROOKS.Text + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLog.Log = oLog.Log + "Valo rLimite IR: " + valValorLimiteIR.Valor + " \n";
            oLog.Log = oLog.Log + "Alíquota IR: " + valAliquotaIR.Valor + " \n";
            oLog.Log = oLog.Log + "Valor Limite CRF: " + valValorLimiteCRF.Valor + " \n";
            oLog.Log = oLog.Log + "Alíquota PIS: " + valAliquotaPIS.Valor + " \n";
            oLog.Log = oLog.Log + "Alíquota COFINS: " + valAliquotaCOFINS.Valor + " \n";
            oLog.Log = oLog.Log + "Alíquota CSLL: " + valAliquotaCSLL.Valor + " \n";
            oLog.Log = oLog.Log + "Limite CRF1: " + valLimiteCRF1.Valor + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtCodigoBROOKS.Text.Equals(""))
                {
                    lblMensagem.Text = "Retenção Impostos NF inválida!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oAliquotaImpostos = AtribuiDadosDoForm(oAliquotaImpostos);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oAliquotaImpostosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oAliquotaImpostosDados.Inserir(oAliquotaImpostos);
                        Grade.DataSource = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Retenção Impostos NF incluída com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oAliquotaImpostosDados.Alterar(oAliquotaImpostos, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Retenção Impostos NF alterada com sucesso!";
                            Grade.DataSource = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Retenções NF";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtCodigoBROOKS.Text.Equals(""))
                {
                    lblMensagem.Text = "Retenção Impostos NF inválida!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oAliquotaImpostosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Retenção Impostos NF excluída com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Retenção Impostos NF";
                    Grade.DataSource = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Retenção de Impostos NF";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Retenção de Impostos NF";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "CodigoSituacaoTributaria" &&
                e.CommandArgument.ToString() != "CodigoBROOKS" && e.CommandArgument.ToString() != "Descricao" &&
                e.CommandArgument.ToString() != "AliquotaPIS_Retido" && e.CommandArgument.ToString() != "AliquotaIR_Retido" &&
                e.CommandArgument.ToString() != "AliquotaCOFINS_Retido" && e.CommandArgument.ToString() != "AliquotaContribSocial" &&
                e.CommandArgument.ToString() != "ValorLimiteCRF" && e.CommandArgument.ToString() != "ValorLimiteIR" &&
                e.CommandArgument.ToString() != "TotalCRF")
            {
                txtCodigoBROOKS.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oAliquotaImpostos = oAliquotaImpostosDados.PegaDados(oAliquotaImpostos, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oAliquotaImpostos);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsAliquotaImpostos pAliquotaImpostos)
        {
            intSituacaoTributaria.Valor = pAliquotaImpostos.CodigoSituacaoTributaria.ToString();
            txtCodigoBROOKS.Text = pAliquotaImpostos.CodigoBROOKS;
            txtDescricao.Text = pAliquotaImpostos.Descricao;
            valValorLimiteIR.Valor = pAliquotaImpostos.ValorLimiteIR.ToString();
            valAliquotaIR.Valor = pAliquotaImpostos.AliquotaIR_Retido.ToString();
            valAliquotaPIS.Valor = pAliquotaImpostos.AliquotaPIS_Retido.ToString();
            valAliquotaCOFINS.Valor = pAliquotaImpostos.AliquotaCOFINS_Retido.ToString();
            valAliquotaCSLL.Valor = pAliquotaImpostos.AliquotaContribSocial.ToString();
            valValorLimiteCRF.Valor = pAliquotaImpostos.ValorLimiteCRF.ToString();
        }

        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            intSituacaoTributaria.Valor = "";
            txtCodigoBROOKS.Text = "";
            txtDescricao.Text = "";
            valValorLimiteIR.Valor = "";
            valAliquotaIR.Valor = "";
            valAliquotaPIS.Valor = "";
            valAliquotaCOFINS.Valor = "";
            valAliquotaCSLL.Valor = "";
            valValorLimiteCRF.Valor = "";
            PermissaoIncluir();
        }
        protected clsAliquotaImpostos AtribuiDadosDoForm(clsAliquotaImpostos pAliquotaImpostos)
        {
            pAliquotaImpostos.CodigoSituacaoTributaria = 0;
            pAliquotaImpostos.ValorLimiteIR = 0;
            pAliquotaImpostos.AliquotaIR_Retido = 0;
            pAliquotaImpostos.AliquotaPIS_Retido = 0;
            pAliquotaImpostos.AliquotaCOFINS_Retido = 0;
            pAliquotaImpostos.AliquotaContribSocial = 0;
            pAliquotaImpostos.ValorLimiteCRF = 0;
            if (intSituacaoTributaria.Valor != "")
                pAliquotaImpostos.CodigoSituacaoTributaria = Convert.ToInt16(intSituacaoTributaria.Valor);
            pAliquotaImpostos.CodigoBROOKS = txtCodigoBROOKS.Text;
            pAliquotaImpostos.Descricao = txtDescricao.Text;
            if (valValorLimiteIR.Valor != "")
                pAliquotaImpostos.ValorLimiteIR = Convert.ToDecimal(valValorLimiteIR.Valor);
            if (valAliquotaIR.Valor != "")
                pAliquotaImpostos.AliquotaIR_Retido = Convert.ToDecimal(valAliquotaIR.Valor);
            if (valAliquotaPIS.Valor != "")
                pAliquotaImpostos.AliquotaPIS_Retido = Convert.ToDecimal(valAliquotaPIS.Valor);
            if (valAliquotaCOFINS.Valor != "")
                pAliquotaImpostos.AliquotaCOFINS_Retido = Convert.ToDecimal(valAliquotaCOFINS.Valor);
            if (valAliquotaCSLL.Valor != "")
                pAliquotaImpostos.AliquotaContribSocial = Convert.ToDecimal(valAliquotaCSLL.Valor);
            if (valValorLimiteCRF.Valor != "")
                pAliquotaImpostos.ValorLimiteCRF = Convert.ToDecimal(valValorLimiteCRF.Valor);
            return pAliquotaImpostos;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Retenção Impostos NF";
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

            Grade.DataSource = oAliquotaImpostosDados.PreencheDataTable(geral.Ordem);
            Grade.DataBind();
        }
    }
}