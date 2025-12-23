using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class MTRCanceladaTransbordo : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsMTRCanceladaTransbordo oMTRCanceladaTransbordo = new clsMTRCanceladaTransbordo();
    clsMTRCanceladaTransbordoDados oMTRCanceladaTransbordoDados = new clsMTRCanceladaTransbordoDados();
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "21");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            datData.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                    Grade.DataSource = oMTRCanceladaTransbordoDados.PegaDados(oMTRCanceladaTransbordo, 0, false);
                    Grade.DataBind();
                    PermissaoAlterar();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        datData.Focus();
    }
    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oMTRCanceladaTransbordoDados.DadoExiste(oMTRCanceladaTransbordo.Sequencial);
        if (intNumeroMTR.Valor.Equals(""))
        {
            lblMensagem.Text = "MTR Cancelada/Transbordo inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oMTRCanceladaTransbordoDados.Excluir(oMTRCanceladaTransbordo.Sequencial);
            lblMensagem.Text = "MTR Cancelada/Transbordo excluído com sucesso!";

            Grade.DataSource = oMTRCanceladaTransbordoDados.PegaDados(oMTRCanceladaTransbordo, 0, false);
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
        oLog.LocalOperacao = "MTR Cancelada/Transbordo";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "MTR Cancelada: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data: " + datData.Data + " \n";
        oLog.Log = oLog.Log + "Motorista: " + ctlMotorista.Texto + " (" + ctlMotorista.Valor + ") \n";
        oLog.Log = oLog.Log + "Nº MTR: " + intNumeroMTR.Valor + " \n";
        if (chkEhTransbordo.Checked)
            oLog.Log = oLog.Log + "É Transbordo: Sim \n";
        else
            oLog.Log = oLog.Log + "É Transbordo: Não \n";
        oLogDados.Inserir(oLog);
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (intNumeroMTR.Valor.Equals(""))
            {
                lblMensagem.Text = "MTR Cancelada/Transbordo inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oMTRCanceladaTransbordo = AtribuiDadosDoForm(oMTRCanceladaTransbordo);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oMTRCanceladaTransbordoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclusão");
                    oMTRCanceladaTransbordoDados.Inserir(oMTRCanceladaTransbordo);
                    Grade.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
                    Grade.DataBind();
                    lblMensagem.Text = "MTR Cancelada/Transbordo incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    SalvarLog("Alteração");
                    string msgErr = oMTRCanceladaTransbordoDados.Alterar(oMTRCanceladaTransbordo, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "MTR Cancelada/Transbordo alterado com sucesso!";
                        Grade.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Lançamento MTR Cancelada/Transbordo";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (intNumeroMTR.Valor.Equals(""))
            {
                lblMensagem.Text = "MTR Cancelada/Transbordo inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oMTRCanceladaTransbordoDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "MTR Cancelada/Transbordo excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;MTR Cancelada/Transbordo";
                Grade.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
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
        lblTitulo.Text = "&nbsp;Exclusão de MTR Cancelada/Transbordo";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de MTR Cancelada/Transbordo";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "Data" && e.CommandArgument.ToString() != "CodigoMotorista" &&
            e.CommandArgument.ToString() != "NomeMotorista" && e.CommandArgument.ToString() != "NumeroMTR" && e.CommandArgument.ToString() != "EhTransbordo")
        {
            ctlMotorista.Texto = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oMTRCanceladaTransbordo = oMTRCanceladaTransbordoDados.PegaDados(oMTRCanceladaTransbordo, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oMTRCanceladaTransbordo);
            PermissaoAlterar();

            Grade2.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo("Sequencial desc",  ctlMotorista.Valor, "CodigoMotorista");
            Grade2.DataBind();
        }
    }

    protected void AtribuiDadosDaClasse(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo)
    {
        if (pMTRCanceladaTransbordo.Data == "01/01/0001" || pMTRCanceladaTransbordo.Data == "01/01/0100" || pMTRCanceladaTransbordo.Data == null)
            datData.Data = ""; //Convert.ToDateTime("01/00/0001").ToString("dd/MM/yyyy");
        else
            datData.Data = Convert.ToDateTime(pMTRCanceladaTransbordo.Data).ToString("dd/MM/yyyy");
        ctlMotorista.Valor = pMTRCanceladaTransbordo.CodigoMotorista.ToString();
        ctlMotorista.Texto = pMTRCanceladaTransbordo.NomeMotorista;
        intNumeroMTR.Valor = pMTRCanceladaTransbordo.NumeroMTR.ToString();
        if (pMTRCanceladaTransbordo.EhTransbordo == 1)
            chkEhTransbordo.Checked = true;
        else
            chkEhTransbordo.Checked = false;
    }

    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        datData.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
        ctlMotorista.Valor = "";
        ctlMotorista.Texto = "";
        intNumeroMTR.Valor = "";
        chkEhTransbordo.Checked = false;
        PermissaoAlterar();
    }
    protected clsMTRCanceladaTransbordo AtribuiDadosDoForm(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo)
    {
        if (datData.Data != "")
            pMTRCanceladaTransbordo.Data = datData.Data;
        if (ctlMotorista.Valor != "")
            pMTRCanceladaTransbordo.CodigoMotorista = Convert.ToInt32(ctlMotorista.Valor);
        if (intNumeroMTR.Valor != "")
            pMTRCanceladaTransbordo.NumeroMTR = Convert.ToInt32(intNumeroMTR.Valor);
        if (chkEhTransbordo.Checked)
            pMTRCanceladaTransbordo.EhTransbordo = 1;
        else
            pMTRCanceladaTransbordo.EhTransbordo = 0;

        return pMTRCanceladaTransbordo;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Controle de MTR Cancelada/Transbordo";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0100")
                e.Row.Cells[4].Text = "";
            if (e.Row.Cells[7].Text == "1")
                e.Row.Cells[7].Text = "Sim";
            else
                e.Row.Cells[7].Text = "Não";
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(geral.Ordem);
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Grade.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
        Grade.DataBind();
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        datData.Focus();
    }
    protected void Grade2_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade2.DataSource = oMTRCanceladaTransbordoDados.PreencheDataTableMTRCanceladaTransbordo(geral.Ordem);
        Grade2.DataBind();
    }
}