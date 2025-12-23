using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.IO;

public partial class uploadRGRCDFDDR : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    wp_FilesClientes wpCliente = new wp_FilesClientes();
    wp_FilesClientesDados wpFiles = new wp_FilesClientesDados();
    clsClientes oCliente = new clsClientes();
    clsClienteDados oClientesDados = new clsClienteDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "25");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Excluir == 0)
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
                    intCodigoCliente.Valor = "73";
                    if (oUsuario.Aplicativo == true)
                        menu.Visible = false;
                    else
                        menu.Visible = true;
                    _dt = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
                    Grade.DataSource = _dt; 
                    Grade.DataBind();
                    PermissaoIncluir();
                    if (_dt.Rows[0][4].ToString() != "")
                        txtNomeFantasia.Text = _dt.Rows[0][5].ToString();
                    txtDescricao.MaxLength = wpFiles.PegaTamanhoCampoVarChar("Descricao");
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
    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        if (hifCodigo.Value != "")
            lblMensagem.Text = wpFiles.DadoExiste(Convert.ToInt32(hifCodigo.Value));
        if (lblMensagem.Text.Equals(""))
        {
            lblMensagem.Text = "RGR/DDR/CDF página BROOKS inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            wpFiles.Excluir(Convert.ToInt32(hifCodigo.Value));
            lblMensagem.Text = "RGR/DDR/CDF página BROOKS excluído com sucesso!";

            Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
            Grade.DataBind();
            LimpaCampos();
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "UpLoad RGR/DDR/CDF";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Sequencial upLoad: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Tipo: " + ddlTipo.Text + " \n";
        oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
        oLog.Log = oLog.Log + "Período: " + txtPeriodo.Text + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";
        lblMensagem2.Text = "";
        clsDB oDB = new clsDB();
        if (txtDescricao.Text == "" && UploadDocumento.FileName != "")
            txtDescricao.Text = UploadDocumento.FileName;
        else if (Salvar.Text == "Ok")
        {
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "RGR/DDR/CDF página BROOKS inválido!";
            }
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                wpCliente = AtribuiDadosDoForm(wpCliente);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = wpFiles.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclus");
                    lblMensagem.Text = wpFiles.Incluir
                        (txtDescricao.Text, ddlTipo.Text, Convert.ToInt32(intCodigoCliente.Valor), txtDescricao.Text, txtPeriodo.Text);
                    Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
                    Grade.DataBind();
                    lblMensagem.Text = "RGR/DDR/CDF página BROOKS incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = "";
                    //SalvarLog("Alteração");
                    //msgErr = oDocumentosPaginaDados.Alterar(oDocumentosPagina, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "RGR/DDR/CDF página BROOKS alterado com sucesso!";
                        //Grade.DataSource = oDocumentosPaginaDados.PegaDados(oDocumentosPagina, 0, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;UpLoad RGR/DDR/CDF";
                string savePath = Server.MapPath("").Replace("\\forms", "") + @"\dados\" + Convert.ToInt32(intCodigoCliente.Valor).ToString("000000") + "\\" + UploadDocumento.FileName;
                string criaDir = Server.MapPath("").Replace("\\forms", "") + @"\dados\" + Convert.ToInt32(intCodigoCliente.Valor).ToString("000000");
                try
                {
                    if (UploadDocumento.FileName != "")
                    {
                        if (!Directory.Exists(criaDir))
                            Directory.CreateDirectory(criaDir);
                        UploadDocumento.SaveAs(savePath);
                        lblMensagem2.Text = "Upload realizado com sucesso!";                      
                    }
                    else if (UploadDocumento.FileName.Equals(""))
                    {
                        lblMensagem2.Text = "Upload de arquivo não foi realizado ou arquivo inválido!";
                    }

                }
                catch (Exception ex)
                {
                    lblMensagem2.Text = ex.Message;
                }
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            lblMensagem2.Text = "";
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "RGR/DDR/CDF página BROOKS inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;RGR/DDR/CDF página BROOKS";
                SalvarLog("Exclusão");
                wpFiles.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "RGR/DDR/CDF página BROOKS excluído com sucesso!";

                Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
                Grade.DataBind();
                LimpaCampos();
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
        lblTitulo.Text = "&nbsp;Exclusão de RGR/DDR/CDF página BROOKS";
        ddlTipo.Enabled = false;
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        lblMensagem2.Text = "";
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de RGR/DDR/CDF página BROOKS";
        txtDescricao.Enabled = true;
        ddlTipo.Enabled = true;
        lblMensagem.Text = "";
        lblMensagem2.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Id" && e.CommandArgument.ToString() != "Tipo" &&
            e.CommandArgument.ToString() != "Descricao" && e.CommandArgument.ToString() != "Periodo" &&
            e.CommandArgument.ToString() != "CodigoCliente")
        {
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            intCodigoCliente.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            txtNomeFantasia.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
            ddlTipo.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Text;
            txtDescricao.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text;
            txtPeriodo.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
        }
    }

    protected void LimpaCampos()
    {
        txtDescricao.Text = "";
        hifCodigo.Value = "";
        txtPeriodo.Text = "";
        PermissaoIncluir();
    }
    protected wp_FilesClientes AtribuiDadosDoForm(wp_FilesClientes pFileCliente)
    {
        pFileCliente.CodigoCliente = intCodigoCliente.Valor;
        pFileCliente.Descricao = txtDescricao.Text;
        pFileCliente.Periodo = txtPeriodo.Text;
        pFileCliente.Tipo = ddlTipo.Text;
        return pFileCliente;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;UpLoad RGR/DDR/CDF";
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
            //if (e.Row.Cells[5].Text.Replace(" ", "") == "S")
            //    e.Row.Cells[5].Text = "Sim";
            //else
            //    e.Row.Cells[5].Text = "Não";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";
        Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text , geral.Ordem);
        Grade.DataBind();
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        if (Session["Clientes"] != null)
        {
            oCliente = (clsClientes)Session["Clientes"];
            intCodigoCliente.Valor = oCliente.Codigo.ToString();
            txtNomeFantasia.Text = oCliente.NomeFantasia;
            Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
            Grade.DataBind();
            LimpaCampos();
        }
    }
    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (intCodigoCliente.Valor != "")
        {
            Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
            Grade.DataBind();
            LimpaCampos();
        }
    }
    protected void btnProcura_Click(object sender, EventArgs e)
    {
        if (intCodigoCliente.Valor != "")
        {
            Grade.DataSource = wpFiles.PreencheDt(intCodigoCliente.Valor, ddlTipo.Text);
            Grade.DataBind();
            if (Grade.Rows.Count > 0)
                txtNomeFantasia.Text = Grade.Rows[0].Cells[4].Text;
            else
                txtNomeFantasia.Text = "";
            LimpaCampos();
        }
    }
}