using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class BlocosMTR : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsBlocosMTR oBlocoMTR = new clsBlocosMTR();
    clsBlocosMTRDados oBlocoMTRDados = new clsBlocosMTRDados();
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
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "20");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            datData.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                    Grade.DataSource = oBlocoMTRDados.PegaDados(oBlocoMTR, 0, false);
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
        lblMensagem.Text = oBlocoMTRDados.DadoExiste(oBlocoMTR.Sequencial);
        if (intNumeroBloco.Valor.Equals(""))
        {
            lblMensagem.Text = "Bloco MTR inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oBlocoMTRDados.Excluir(oBlocoMTR.Sequencial);
            lblMensagem.Text = "Bloco MTR excluído com sucesso!";

            Grade.DataSource = oBlocoMTRDados.PegaDados(oBlocoMTR, 0, false);
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
        oLog.LocalOperacao = "Distribuição Blocos MTR";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código distribuição MTR: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Data: " + datData.Data + " \n";
        oLog.Log = oLog.Log + "Motorista: " + ctlMotorista.Texto + " (" + ctlMotorista.Valor + ") \n";
        oLog.Log = oLog.Log + "Nº Bloco: " + intNumeroBloco.Valor + " \n";
        oLog.Log = oLog.Log + "Nº MTR inicial: " + intNuMTRInicial.Valor + " \n";
        oLog.Log = oLog.Log + "Nº MTR Final: " + intNuMTRFinal.Valor + " \n";
        oLog.Log = oLog.Log + "Coeficiente numeração: " + intCoeficienteNumeracao.Valor + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (intNumeroBloco.Valor.Equals(""))
            {
                lblMensagem.Text = "Bloco MTR inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oBlocoMTR = AtribuiDadosDoForm(oBlocoMTR);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oBlocoMTRDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclusão");
                    oBlocoMTRDados.Inserir(oBlocoMTR);
                    Grade.DataSource = oBlocoMTRDados.PreencheDataTableBlocosMTR(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
                    Grade.DataBind();
                    lblMensagem.Text = "Bloco MTR incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    SalvarLog("Alteração");
                    string msgErr = oBlocoMTRDados.Alterar(oBlocoMTR, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Bloco MTR alterado com sucesso!";
                        Grade.DataSource = oBlocoMTRDados.PreencheDataTableBlocosMTR(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Controle de Blocos MTR";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (intNumeroBloco.Valor.Equals(""))
            {
                lblMensagem.Text = "Bloco MTR inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oBlocoMTRDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                lblMensagem.Text = "Bloco MTR excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Controle de Blocos MTR";
                Grade.DataSource = oBlocoMTRDados.PreencheDataTableBlocosMTR(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
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
        lblTitulo.Text = "&nbsp;Exclusão de Blocos MTR";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Blocos MTR";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Sequencial" && e.CommandArgument.ToString() != "Data" && e.CommandArgument.ToString() != "CodigoMotorista" &&
            e.CommandArgument.ToString() != "NomeMotorista" && e.CommandArgument.ToString() != "NuMTRInicial" && e.CommandArgument.ToString() != "NumeroBloco" &&
            e.CommandArgument.ToString() != "NuMTRFinal" && e.CommandArgument.ToString() != "CoeficienteNumeracao")
        {
            ctlMotorista.Texto = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oBlocoMTR = oBlocoMTRDados.PegaDados(oBlocoMTR, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oBlocoMTR);
            PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsBlocosMTR pBlocoMTR)
    {
        if (pBlocoMTR.Data == "01/01/0001" || pBlocoMTR.Data == "01/01/0100" || pBlocoMTR.Data == null)
            datData.Data = ""; //Convert.ToDateTime("01/00/0001").ToString("dd/MM/yyyy");
        else
            datData.Data = Convert.ToDateTime(pBlocoMTR.Data).ToString("dd/MM/yyyy");
        ctlMotorista.Valor = pBlocoMTR.CodigoMotorista.ToString();
        ctlMotorista.Texto = pBlocoMTR.NomeMotorista;
        intNumeroBloco.Valor = pBlocoMTR.NumeroBloco.ToString();
        intNuMTRInicial.Valor = pBlocoMTR.NuMTRInicial.ToString();
        intNuMTRFinal.Valor = pBlocoMTR.NuMTRFinal.ToString();
        intCoeficienteNumeracao.Valor = pBlocoMTR.CoeficienteNumeracao.ToString();
    }

    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        datData.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
        ctlMotorista.Valor = "";
        ctlMotorista.Texto = "";
        intNumeroBloco.Valor = "";
        intNuMTRInicial.Valor = "";
        intNuMTRFinal.Valor = "";
        intCoeficienteNumeracao.Valor = "";
        PermissaoAlterar();
    }
    protected clsBlocosMTR AtribuiDadosDoForm(clsBlocosMTR pBlocoMTR)
    {
        if (datData.Data != "")
            pBlocoMTR.Data = datData.Data;
        if (ctlMotorista.Valor != "")
            pBlocoMTR.CodigoMotorista = Convert.ToInt32(ctlMotorista.Valor);
        if (intNumeroBloco.Valor != "")
            pBlocoMTR.NumeroBloco = Convert.ToInt32(intNumeroBloco.Valor); 
        if (intNuMTRInicial.Valor != "")
            pBlocoMTR.NuMTRInicial = Convert.ToInt32(intNuMTRInicial.Valor);
        if (intNuMTRFinal.Valor != "")
            pBlocoMTR.NuMTRFinal = Convert.ToInt32(intNuMTRFinal.Valor);
        if (intCoeficienteNumeracao.Valor != "")
            pBlocoMTR.CoeficienteNumeracao = Convert.ToInt32(intCoeficienteNumeracao.Valor);

        return pBlocoMTR;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Controle de Blocos MTR";
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
        }
    }
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oBlocoMTRDados.PreencheDataTableBlocosMTR(geral.Ordem);
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Grade.DataSource = oBlocoMTRDados.PreencheDataTableBlocosMTR(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
        Grade.DataBind();
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        datData.Focus();
    }
}