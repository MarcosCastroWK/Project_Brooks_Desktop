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
    public partial class DocumentosPagina : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsDocumentosPagina oDocumentosPagina = new clsDocumentosPagina();
        clsDocumentosPaginaDados oDocumentosPaginaDados = new clsDocumentosPaginaDados();
        clsClienteDados oClientesDados = new clsClienteDados();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "61");
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
                        Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
                        Grade.DataBind();
                        PermissaoIncluir();
                        txtDescricao.MaxLength = oDocumentosPaginaDados.PegaTamanhoTipoVarChar("Descricao");
                        txtPeriodo.MaxLength = oDocumentosPaginaDados.PegaTamanhoTipoVarChar("Periodo");
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            ddlTipo.Focus();
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
            lblMensagem.Text = oDocumentosPaginaDados.DadoExiste(oDocumentosPagina.Id);
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "Documento página BROOKS inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oDocumentosPaginaDados.Excluir(oDocumentosPagina.Id);
                lblMensagem.Text = "Documento página BROOKS excluído com sucesso!";

                Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
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
            oLog.LocalOperacao = "Documentos página";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código documento: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Tipo: " + ddlTipo.Text + " \n";
            oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
            oLog.Log = oLog.Log + "Período: " + txtPeriodo.Text + " \n";
            if (chkBROOKS.Checked)
                oLog.Log = oLog.Log + "É da BROOKS \n";
            else
                oLog.Log = oLog.Log + "Não é da BROOKS \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            lblMensagem2.Text = "";
            clsDB oDB = new clsDB();

            if (Salvar.Text == "Ok")
            {
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "Documento página BROOKS inválido!";
                }
                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oDocumentosPagina = AtribuiDadosDoForm(oDocumentosPagina);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oDocumentosPaginaDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        SalvarLog("Inclusão");
                        oDocumentosPaginaDados.Inserir(oDocumentosPagina);
                        Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Documento página BROOKS incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        SalvarLog("Alteração");
                        string msgErr = oDocumentosPaginaDados.Alterar(oDocumentosPagina, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            lblMensagem.Text = "Documento página BROOKS alterado com sucesso!";
                            Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Documentos gerais (LAO/Álvara/ISO9002)";
                    string savePath = Server.MapPath("").Replace("\\forms", "") + @"\dados\" + UploadDocumento.FileName;
                    //savePath = Server.MapPath("").Replace("\\forms", "") + @"\dados\" + txtDescricao.Text;
                    try
                    {
                        if (UploadDocumento.FileName != "")
                        {
                            UploadDocumento.SaveAs(savePath);
                            lblMensagem2.Text = "Upload realizado com sucesso!";

                            SalvarLog("Exclusão");
                            wp_FilesClientesDados wpFiles = new wp_FilesClientesDados();
                            string merr = wpFiles.Excluir(txtDescricao.Text);

                            DataTable _dt = new DataTable();
                            _dt = oClientesDados.PreencheDataTable("Codigo", true);
                            foreach (DataRow dr in _dt.Rows)
                            {
                                if (dr["Codigo"].ToString() != "")
                                {
                                    merr = wpFiles.Incluir(txtDescricao.Text, ddlTipo.Text, Convert.ToInt32(dr["Codigo"]), txtDescricao.Text, txtPeriodo.Text);
                                    if (merr != "")
                                        break;
                                }
                            }
                            if (merr != "")
                                lblMensagem2.Text = merr;
                            else
                                lblMensagem2.Text = "Documento para todos os clientes distribuido com sucesso!";
                        }
                        if (UploadDocumento.FileName.Equals(""))
                        {
                            lblMensagem2.Text = "Upload de arquivo não foi realizado ou arquivo inválido!";
                        }

                    }
                    catch (Exception ex)
                    {
                        lblMensagem2.Text = ex.Message;
                    }

                    LimpaCampos();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                lblMensagem2.Text = "";
                if (txtDescricao.Text.Equals(""))
                {
                    lblMensagem.Text = "Documento página BROOKS inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    oDocumentosPaginaDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Documento página BROOKS excluído com sucesso!";
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Documento página BROOKS";
                    Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
                    Grade.DataBind();
                    if (txtDescricao.Text != "")
                    {
                        wp_FilesClientesDados wpFiles = new wp_FilesClientesDados();
                        string merr = wpFiles.Excluir(txtDescricao.Text);
                        if (merr != "")
                            lblMensagem2.Text = merr;
                        else
                            lblMensagem2.Text = "Exclusão de documento dos clientes realizado com sucesso!";
                    }
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
            lblTitulo.Text = "&nbsp;Exclusão de Documento página BROOKS";
            txtDescricao.Enabled = false;
            ddlTipo.Enabled = false;
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            lblMensagem2.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Documento página BROOKS";
            txtDescricao.Enabled = true;
            ddlTipo.Enabled = true;
            lblMensagem.Text = "";
            lblMensagem2.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Id" && e.CommandArgument.ToString() != "Tipo" &&
                e.CommandArgument.ToString() != "Descricao" && e.CommandArgument.ToString() != "BROOKS" &&
                e.CommandArgument.ToString() != "Periodo")
            {
                txtDescricao.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oDocumentosPagina = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oDocumentosPagina);
                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsDocumentosPagina pDocumentosPagina)
        {
            ddlTipo.Text = pDocumentosPagina.Tipo;
            txtDescricao.Text = pDocumentosPagina.Descricao;
            if (pDocumentosPagina.BROOKS == 1)
                chkBROOKS.Checked = true;
            else
                chkBROOKS.Checked = false;
            txtPeriodo.Text = pDocumentosPagina.Periodo;
        }

        protected void LimpaCampos()
        {
            chkBROOKS.Checked = false;
            txtDescricao.Text = "";
            //ddlTipo.Text = "";
            chkBROOKS.Checked = false;
            txtPeriodo.Text = "";
            hifCodigo.Value = "";
            PermissaoIncluir();
        }
        protected clsDocumentosPagina AtribuiDadosDoForm(clsDocumentosPagina pDocumentosPagina)
        {
            pDocumentosPagina.Tipo = ddlTipo.Text;
            pDocumentosPagina.Descricao = txtDescricao.Text;
            if (chkBROOKS.Checked)
                pDocumentosPagina.BROOKS = 1;
            else if (!chkBROOKS.Checked)
                pDocumentosPagina.BROOKS = 0;
            pDocumentosPagina.Periodo = txtPeriodo.Text;
            return pDocumentosPagina;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Documentos gerais (LAO/Álvara/ISO9002)";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            lblMensagem2.Text = "";
            Salvar.Text = "Ok";
            txtDescricao.Enabled = true;
            ddlTipo.Enabled = true;
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
                if (e.Row.Cells[5].Text.Replace(" ", "") == "S")
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

            Grade.DataSource = oDocumentosPaginaDados.PreencheDataTable(geral.Ordem);
            Grade.DataBind();
        }
        protected void txtDescricao_TextChanged(object sender, EventArgs e)
        {
            if (txtDescricao.Text == "")
                txtDescricao.Text = UploadDocumento.FileName;
        }
        protected void txtDescricao_Init(object sender, EventArgs e)
        {
            if (txtDescricao.Text == "")
                txtDescricao.Text = UploadDocumento.FileName;
        }
    }
}