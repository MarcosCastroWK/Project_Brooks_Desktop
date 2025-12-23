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
    public partial class Lancamentos : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsLancamentos oLancamentos = new clsLancamentos();
        clsLancamentosDados oLancamentosDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
        clsUsuarios oUsuario = new clsUsuarios();
        clsClienteDados oClienteDados = new clsClienteDados();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "16");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("sempermissao.aspx");
            if (!IsPostBack)
            {
                datData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                Salvar.Enabled = false;
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
                            menu.Visible = false;
                        else
                            menu.Visible = true;

                        txtDataInicial.Text = Convert.ToDateTime("01/08/2017").ToString("yyyy-MM-dd");

                        Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, true, 0, txtDataInicial.Text, true);
                        Grade.DataBind();

                        if (Grade.Rows[1].Cells[4].Text != "")
                            hifCodigo.Value = Grade.Rows[1].Cells[4].Text;

                        if (Grade.Rows[1].Cells[2].Text != "")
                            oLancamentos.NumeroLancamento = Convert.ToInt32(Grade.Rows[1].Cells[2].Text);

                        GradeMTR.DataSource = oLancamentosMTRDados.PreencheDataTableOrdem("CodigoResiduo asc", oLancamentos.NumeroLancamento,
                                                                                          Convert.ToInt32(hifCodigo.Value), txtDataInicial.Text, "");
                        GradeMTR.DataBind();

                        GradeClientes.DataSource = oClienteDados.PreencheDataTable("NomeFantasia asc", true);
                        GradeClientes.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            intNrLancamento.Focus();
        }
        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oLancamentosDados.DadoExiste(oLancamentos.NumeroLancamento);
            if (intNrLancamento.Valor.Equals(""))
            {
                lblMensagem.Text = "Locação inválida!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oLancamentosDados.Excluir(oLancamentos.NumeroLancamento);
                lblMensagem.Text = "Locação excluída com sucesso!";

                Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, false, 10, txtDataInicial.Text, true);
                Grade.DataBind();

                GradeMTR.DataSource = oLancamentosMTRDados.PreencheDataTableOrdem("CodigoResiduo asc", oLancamentos.NumeroLancamento,
                                                                      Convert.ToInt32(hifCodigo.Value), txtDataInicial.Text, "");

            }
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (intNrLancamento.Valor.Equals(""))
                {
                    lblMensagem.Text = "Locação inválida!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oLancamentos = AtribuiDadosDoForm(oLancamentos);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oLancamentosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        oLancamentosDados.Inserir(oLancamentos);
                        Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, false, 10, txtDataInicial.Text, true);
                        Grade.DataBind();
                        lblMensagem.Text = "Locação incluída com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        string msgErr = oLancamentosDados.Alterar(oLancamentos, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Locações alterado com sucesso!";
                            Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, false, 10, txtDataInicial.Text, true);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Locações";
                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (intNrLancamento.Valor.Equals(""))
                {
                    lblMensagem.Text = "Locação inválida!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    oLancamentosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Locações excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Locações ";
                    Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, false, 10, txtDataInicial.Text, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Locação";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Locação";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "NumeroLancamento" && e.CommandArgument.ToString() != "NomeCliente" && e.CommandArgument.ToString() != "CodigoCliente" &&
                e.CommandArgument.ToString() != "NumeroCaixa" && e.CommandArgument.ToString() != "DataColocacao" && e.CommandArgument.ToString() != "DataRetirada" &&
                e.CommandArgument.ToString() != "NuLancColocacao" && e.CommandArgument.ToString() != "NumeroMTR" && e.CommandArgument.ToString() != "DescricaoResiduo" &&
                e.CommandArgument.ToString() != "Quantidade" && e.CommandArgument.ToString() != "Unidade" && e.CommandArgument.ToString() != "Observacao")
            {
                int _NuLanc = Convert.ToInt32(oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text);
                oLancamentos = oLancamentosDados.PegaDados(oLancamentos, _NuLanc);
                AtribuiDadosDaClasse(oLancamentos);
                if (oLancamentos.CodigoCliente.ToString() != "")
                    hifCodigo.Value = oLancamentos.CodigoCliente.ToString();
                Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, true, Convert.ToInt32(hifCodigo.Value), txtDataInicial.Text, false);
                Grade.DataBind();

                GradeMTR.DataSource = oLancamentosMTRDados.PreencheDataTableOrdem("CodigoResiduo asc", _NuLanc,
                                                                                  Convert.ToInt32(hifCodigo.Value), txtDataInicial.Text, "");
                GradeMTR.DataBind();

            }
        }

        protected void AtribuiDadosDaClasse(clsLancamentos pLancamentos)
        {
            datData.Text = Convert.ToDateTime(pLancamentos.Data).ToString("yyyy-MM-dd");
            intNrLancamento.Valor = pLancamentos.NumeroLancamento.ToString();
            intCodigoCliente.Valor = pLancamentos.CodigoCliente.ToString();
            txtContainer.Text = pLancamentos.NumeroCaixa;
            datDataColocacao.Text = Convert.ToDateTime(pLancamentos.DataColocacao).ToString("yyyy-MM-dd");
            datDataRetirada.Text = Convert.ToDateTime(pLancamentos.DataRetirada).ToString("yyyy-MM-dd");
            intNrLancTroca.Valor = pLancamentos.NuLancColocacao.ToString();
        }

        protected void LimpaCampos()
        {
            hifCodigo.Value = "";
            datData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            intNrLancamento.Valor = "";
            intCodigoCliente.Valor = "";
            txtContainer.Text = "";
            datDataColocacao.Text = "";
            datDataRetirada.Text = "";
            intNrLancTroca.Valor = "";
        }
        protected clsLancamentos AtribuiDadosDoForm(clsLancamentos pLancamentos)
        {
            /*
            if (datDataCadastro.Text != "")
                pLancamentos.DataCadastro = datDataCadastro.Text;
            if (chkInativo.Checked)
                pLancamentos.Inativo = 1;
            else if (!chkInativo.Checked)
                pLancamentos.Inativo = 0;
            pLancamentos.Cor = txtCor.Text;
            pLancamentos.Numero = txtNumero.Text;
            if (valCapacidade.Valor == "")
                pLancamentos.Capacidade = 0;
            else
                pLancamentos.Capacidade = Convert.ToDecimal(valCapacidade.Valor);
            pLancamentos.Tipo = txtTipo.Text;
            */
            return pLancamentos;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Locações";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Ok";
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                /*            if (e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0100")
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
                 */
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
                Grade.DataSource = oLancamentosDados.PreencheDataTable(geral.Ordem, Convert.ToInt32(hifCodigo.Value), txtDataInicial.Text, "");
                Grade.DataBind();
            }
        }
        protected void datDataCadastro_TextChanged(object sender, EventArgs e)
        {
            datData.TextMode = TextBoxMode.Date;
        }
        protected void datDataCadastro_DataBinding(object sender, EventArgs e)
        {

        }
        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            if (datData.TextMode != TextBoxMode.Date)
            {
                datData.TextMode = TextBoxMode.Date;
                datData.Focus();
            }

        }
        protected void lblErro_TextChanged(object sender, EventArgs e)
        {

        }
        protected void GradeClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "NomeFantasia" && e.CommandArgument.ToString() != "Codigo")
            {
                string CodigoCliente = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;

                Grade.DataSource = oLancamentosDados.PegaDados(oLancamentos, 0, true, Convert.ToInt32(CodigoCliente), txtDataInicial.Text, false);
                Grade.DataBind();

                GradeMTR.DataSource = oLancamentosMTRDados.PreencheDataTableOrdem("CodigoResiduo asc", oLancamentos.NumeroLancamento,
                                                                                  Convert.ToInt32(CodigoCliente), txtDataInicial.Text, "");
                GradeMTR.DataBind();
            }
        }
        protected void Grade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnBusca_Click(object sender, EventArgs e)
        {
            GradeClientes.DataSource = oClienteDados.PreencheDataTable("NomeFantasia asc", txtBuscar.Text, "NomeFantasia");
            GradeClientes.DataBind();
        }
    }
}